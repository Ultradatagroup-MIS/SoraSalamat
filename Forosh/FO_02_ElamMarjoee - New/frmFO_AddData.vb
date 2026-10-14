Imports System.Data.SqlClient

Public Class frmFO_AddData

#Region "Fields"
    Private dsForm As New DataSet
    Private ErrPro As New ErrorProvider

    Private ccElamMarjoee As Integer = 0
    Private ccFaktorTitr As Integer = 0
    Private ccMoshtary As Integer = 0

    Private _loading As Boolean = False          ' جلوگیری از اجرای TextChanged موقع پر کردن با کد
    Private _locked As Boolean = False           ' اعلام مرجوعیِ غیرقابل ویرایش
    Private _isCalculating As Boolean = False
    Private _gridStructureReady As Boolean = False
    Private dt_SearchSatr As DataTable

    Private AllowMarjoeeWithoutFaktor As Boolean = False
    Private AllowChangeFeeMarjoee As Boolean = False
    Private IsMalyatAvarezTakhfif As Boolean = False

    Private _lastElat As Integer = 0             ' آخرین علت/نوع انتخاب‌شده، برای سطر بعد پیشنهاد می‌شود
    Private _lastNoe As Integer = 0
#End Region

#Region "Load / Combo / Clear"
    Private Sub frmFO_AddData_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            AllowMarjoeeWithoutFaktor = objTools.ConvertNulls(objTools.DLookup("AllowMarjoeeWithoutFaktor", "tblGL_SysConfig", "CodeMahal = " & CodeMahalFaal), False)
            AllowChangeFeeMarjoee = objTools.ConvertNulls(objTools.DLookup("AllowChangeFeeMarjoee", "tblGL_SysConfig", "CodeMahal = " & CodeMahalFaal), False)
            IsMalyatAvarezTakhfif = objTools.ConvertNulls(objTools.DLookup("IsMalyatAvarezTakhfif", "tblGL_SysConfig", "CodeMahal = " & CodeMahalFaal), False)

            LoadCombo()
            ClearForm()

            With GridEXSatr
                .UpdateMode = Janus.Windows.GridEX.UpdateMode.RowUpdate
                .AllowEdit = Janus.Windows.GridEX.InheritableBoolean.True
                .AllowAddNew = Janus.Windows.GridEX.InheritableBoolean.True
                .TabKeyBehavior = Janus.Windows.GridEX.TabKeyBehavior.ColumnNavigation
                .Enabled = False
            End With
            txtSearchNumber.Focus()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->Load")
        End Try
    End Sub

    Private Sub LoadCombo()
        Dim daSQL As SqlDataAdapter

        ' مامور پخش
        Using cn As New SqlConnection(ConnectionString)
            Using cm As New SqlCommand("Global.spMamorPakhsh_LoadCombo", cn)
                cm.CommandType = CommandType.StoredProcedure
                cm.Parameters.AddWithValue("CodeMahal", CodeMahalFaal)
                cm.Parameters.AddWithValue("sSemat", "," & UD_Dll.Enums.GL_Semat.MamorPakhsh & "," & UD_Dll.Enums.GL_Semat.Ranandeh & "," & UD_Dll.Enums.GL_Semat.Foroshandeh_Sayar & ",")
                daSQL = New SqlDataAdapter(cm)
                daSQL.Fill(dsForm, "tblMamorPakhsh")
            End Using
        End Using
        cmbMamorPakhsh.DataSource = Nothing
        cmbMamorPakhsh.DataSource = dsForm.Tables("tblMamorPakhsh").DefaultView
        cmbMamorPakhsh.DisplayMember = "FN"
        cmbMamorPakhsh.ValueMember = "CodeFard"
        cmbMamorPakhsh.SelectedIndex = -1

        ' فروشنده
        Dim s As String = "Select ccForoshandeh,LN From qryFO_Foroshandeh where ccForoshandeh <> 0 AND CodeMahal = " & CodeMahalFaal
        s &= " AND Not exists (Select * From tblGL_SecurityData where namekarbar= '" & UserName & "' and CodeSubSystem = 628 and pk = qryFO_Foroshandeh.ccForoshandeh) order by LName"
        daSQL = New SqlDataAdapter(s, ConnectionString)
        daSQL.Fill(dsForm, "tblForoshandeh")
        cmbBazaryab.DataSource = Nothing
        cmbBazaryab.DataSource = dsForm.Tables("tblForoshandeh").DefaultView
        cmbBazaryab.ValueMember = "ccForoshandeh"
        cmbBazaryab.DisplayMember = "LN"
        cmbBazaryab.SelectedIndex = -1

        ' علت مرجوعی (برای ستون کشویی گرید)
        daSQL = New SqlDataAdapter("Select Code, Sharh From tblGL_ShenasehOmomi Where CodeAsli = 63 And CodeFarei <> 0 order by Sharh", ConnectionString)
        daSQL.Fill(dsForm, "tblElatMarjoee")
    End Sub

    Private Sub ClearForm()
        _loading = True
        Try
            ccElamMarjoee = 0
            ccFaktorTitr = 0
            ccMoshtary = 0
            _locked = False

            mskTarikh.Text = TarikhEmrooz
            mskTarikh.Enabled = False
            txtSearchNumber.Text = ""
            txtCodeMoshtary.Text = ""
            txtCodeMoshtary.Tag = 0
            lblNameMoshtary.Text = ""
            txtShomarehFaktor.Text = ""
            txtShomarehFaktor.Tag = 0
            cmbBazaryab.SelectedIndex = -1
            cmbMamorPakhsh.SelectedIndex = -1
            rbBargashtAzForosh.Checked = False
            rbBargashtAzTozi.Checked = False
            TxtSharh.Text = ""
            ErrPro.Clear()
        Finally
            _loading = False
        End Try
    End Sub

    Private Sub LockHeader(ByVal locked As Boolean)
        txtCodeMoshtary.Enabled = Not locked
        txtShomarehFaktor.Enabled = Not locked
        cmbBazaryab.Enabled = Not locked
        cmbMamorPakhsh.Enabled = Not locked
        TxtSharh.Enabled = Not locked
        rbBargashtAzForosh.Enabled = Not locked
        rbBargashtAzTozi.Enabled = Not locked
        btnSaveSanad.Enabled = Not locked
    End Sub

    Private Sub NewMarjoee()
        ClearForm()
        _gridStructureReady = False
        LockHeader(False)
        dt_SearchSatr = New DataTable
        GridEXSatr.SetDataBinding(Nothing, "")
        GridEXSatr.Enabled = False
        txtCodeMoshtary.Focus()
    End Sub

    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        NewMarjoee()
    End Sub
#End Region

#Region "هدر: مشتری و فاکتور"
    Private Sub txtCodeMoshtary_TextChanged(sender As Object, e As EventArgs) Handles txtCodeMoshtary.TextChanged
        If _loading Then Exit Sub
        Dim crit As String = "CodeMahal=" & CodeMahalFaal & " AND sVazeiat = " & UD_Dll.Enums.FO_VaziatMoshtary.Faal
        crit &= " AND Not exists (Select * From tblGL_SecurityData where namekarbar= '" & UserName & "' and CodeSubSystem = 614 and pk = qryFO_Moshtary.ccMoshtary) "
        crit &= " And CodeMoshtary = '" & txtCodeMoshtary.Text.Replace("'", "") & "'"

        lblNameMoshtary.Text = objTools.ConvertNulls(objTools.DLookup("NameMoshtary", "qryFO_Moshtary", crit), "")
        txtCodeMoshtary.Tag = objTools.ConvertNulls(objTools.DLookup("ccMoshtary", "qryFO_Moshtary", crit), 0)
        ccMoshtary = Convert.ToInt32(txtCodeMoshtary.Tag)

        ' با عوض شدن مشتری، فاکتور قبلی معتبر نیست
        _loading = True
        txtShomarehFaktor.Text = ""
        _loading = False
        ccFaktorTitr = 0
    End Sub

    Private Sub txtCodeMoshtary_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtCodeMoshtary.KeyPress
        Try
            If (Asc(e.KeyChar) < 48 Or Asc(e.KeyChar) > 57) And (Asc(e.KeyChar) <> 8) And e.KeyChar <> " "c Then e.Handled = True
            If e.KeyChar = " "c Then
                e.Handled = True
                Dim objMoshtary As New Forms_dll.frmFO_MoshtarySearch
                Dim StrSql As String = "Select * from qryFO_Moshtary Where CodeMahal=" & CodeMahalFaal & " AND sVazeiat = " & UD_Dll.Enums.FO_VaziatMoshtary.Faal
                StrSql &= " AND Not exists (Select * From tblGL_SecurityData where namekarbar= '" & UserName & "' and CodeSubSystem = 614 and pk = qryFO_Moshtary.ccMoshtary) "
                StrSql &= " AND Not exists (Select * From tblGL_SecurityData where namekarbar= '" & UserName & "' and CodeSubSystem = 10000 and pk = qryFO_Moshtary.sNoeMoshtary) "
                StrSql &= " order by sMantagheh,sMahaleh,NameMoshtary"

                tCodeMoshtary = ""
                tNameMoshtary = ""
                tccMoshtary = ""
                If txtCodeMoshtary.Text.Length <> 0 Then tCodeMoshtary = txtCodeMoshtary.Text

                objMoshtary.MultiSelection = False
                SearchItem = "CodeMoshtary"
                objMoshtary.SetForm(StrSql)
                objMoshtary.ShowDialog()

                _loading = True
                txtCodeMoshtary.Text = objMoshtary.tCodeMoshtary
                txtCodeMoshtary.Tag = objMoshtary.tccMoshtary
                lblNameMoshtary.Text = objMoshtary.tNameMoshtary
                _loading = False
                ccMoshtary = If(IsNumeric(objMoshtary.tccMoshtary), Convert.ToInt32(objMoshtary.tccMoshtary), 0)
                ccFaktorTitr = 0
                txtShomarehFaktor.Text = ""
                MultiSelection = False
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->txtCodeMoshtary_KeyPress")
        End Try
    End Sub

    Private Sub txtShomarehFaktor_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtShomarehFaktor.KeyPress
        Try
            If e.KeyChar = " "c Then
                e.Handled = True
                Dim objFaktor As New Forms_dll.FaktorSearch
                Dim StrSql As String

                StrSql = "SELECT FaktorShomareh, b.ccForoshandeh, a.ccMoshtary, a.CodeDoreh, ccFaktorTitr, b.PishFaktorShomareh AS ShomarehPishFaktor,"
                StrSql &= " c.NameMoshtary, d.FName + ' ' + d.LName AS NameMamorPakhsh, g.Sharh AS txtNoePardakht,"
                StrSql &= " f.FName + ' ' + f.LName AS NameForoshandeh, dbo.SetDateSlash(FaktorTarikh) AS FaktorTarikhSlash"
                StrSql &= " FROM tblFO_Faktor AS a WITH(NOLOCK) LEFT OUTER JOIN"
                StrSql &= " tblFO_PishFaktor AS b WITH(NOLOCK) ON a.ccPishFaktor = b.ccPishFaktorTitr LEFT OUTER JOIN"
                StrSql &= " tblFO_Moshtary AS c WITH(NOLOCK) ON a.ccMoshtary = c.ccMoshtary LEFT OUTER JOIN"
                StrSql &= " tblGL_MoshakhasatFardi AS d WITH(NOLOCK) ON a.ccMamorPakhsh = d.CodeFard LEFT OUTER JOIN"
                StrSql &= " tblFO_Foroshandeh AS e WITH(NOLOCK) ON b.ccForoshandeh = e.ccForoshandeh LEFT OUTER JOIN"
                StrSql &= " tblGL_MoshakhasatFardi AS f WITH(NOLOCK) ON e.CodeFard = f.CodeFard LEFT OUTER JOIN"
                StrSql &= " tblGL_ShenasehOmomi AS g WITH(NOLOCK) ON a.sNoePardakht = g.Code"
                StrSql &= " WHERE a.sVazeiat = 4261 And a.CodeMahal = " & CodeMahalFaal
                If ccMoshtary <> 0 Then StrSql &= " AND a.ccMoshtary = " & ccMoshtary

                tFaktorShomareh = ""
                SearchItem = ""
                If txtShomarehFaktor.Text <> "" Then
                    tFaktorShomareh = txtShomarehFaktor.Text
                    SearchItem = "FaktorShomareh"
                End If

                objFaktor.SetForm(StrSql)
                objFaktor.TopMost = True
                objFaktor.ShowDialog()

                If objFaktor.tFaktorccFaktor <> 0 Then
                    _loading = True
                    Try
                        ccFaktorTitr = objFaktor.tFaktorccFaktor
                        txtShomarehFaktor.Text = objFaktor.tFaktorShomareh
                        ccMoshtary = objFaktor.tFaktorccMoshtary
                        txtCodeMoshtary.Tag = ccMoshtary
                        txtCodeMoshtary.Text = Convert.ToString(objTools.ConvertNulls(objTools.DLookup("CodeMoshtary", "tblFO_Moshtary", "ccMoshtary = " & ccMoshtary), ""))
                        lblNameMoshtary.Text = Convert.ToString(objTools.ConvertNulls(objTools.DLookup("NameMoshtary", "tblFO_Moshtary", "ccMoshtary = " & ccMoshtary), ""))
                        cmbBazaryab.SelectedValue = objFaktor.tFaktorccForoshandeh
                    Finally
                        _loading = False
                    End Try
                    txtCodeMoshtary.Enabled = False
                    cmbBazaryab.Enabled = False
                End If
            ElseIf (Asc(e.KeyChar) < 48 Or Asc(e.KeyChar) > 57) And (Asc(e.KeyChar) <> 8) Then
                e.Handled = True
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->txtShomarehFaktor_KeyPress")
        End Try
    End Sub

    ' پاک کردن شماره فاکتور = برگشت به حالت بدون فاکتور
    Private Sub txtShomarehFaktor_TextChanged(sender As Object, e As EventArgs) Handles txtShomarehFaktor.TextChanged
        If _loading Then Exit Sub
        If txtShomarehFaktor.Text.Trim() = "" Then
            ccFaktorTitr = 0
            txtCodeMoshtary.Enabled = True
            cmbBazaryab.Enabled = True
        End If
    End Sub
#End Region

#Region "هدر: اعتبارسنجی و ذخیره"
    Private Function ValidateTitr() As Boolean
        If Len(mskTarikh.Text) = 0 Then
            MsgBox("تاريخ را وارد کنيد.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
            mskTarikh.Focus()
            Return False
        End If
        If Not objTarikh.IsShDate(mskTarikh.Text) Then mskTarikh.Focus() : Return False
        If mskTarikh.Text.Substring(0, 4) <> CodeDoreh Then
            MsgBox("تاريخ با دوره مالی فعال يکی نيست.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
            Return False
        End If
        If mskTarikh.Text > TarikhEmrooz Then
            MsgBox("تاریخ اعلام مرجوعی نباید جلوتر از تاریخ امروز باشد.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
            Return False
        End If
        If ObjCode.CheckCompany <> 5 AndAlso mskTarikh.Text < TarikhEmrooz Then
            MsgBox("تاریخ اعلام مرجوعی نباید پیش از تاریخ امروز باشد.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
            Return False
        End If

        If Not rbBargashtAzForosh.Checked AndAlso Not rbBargashtAzTozi.Checked Then
            MsgBox("نوع مرجوعی را مشخص کنید.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
            Return False
        End If

        If Not AllowMarjoeeWithoutFaktor Then
            If ccFaktorTitr = 0 Then
                MsgBox("شماره فاکتور را وارد کنید (Space برای لیست).", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
                txtShomarehFaktor.Focus()
                Return False
            End If
        ElseIf ccFaktorTitr = 0 AndAlso ccMoshtary = 0 Then
            MsgBox("مشتری را وارد کنید.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
            txtCodeMoshtary.Focus()
            Return False
        End If

        If ccFaktorTitr <> 0 AndAlso objTools.DCount("ccFaktorTitr", "tblFO_ElamMarjoee", "ccFaktorTitr = " & ccFaktorTitr & " AND IsFaktorMarjoee = 1 and sVazeiat=4355") <> 0 Then
            MsgBox("برای این شماره فاکتور قبلاً فاکتور مرجوعی صادر شده است!", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information)
            Return False
        End If

        If cmbBazaryab.SelectedIndex < 0 OrElse IsDBNull(cmbBazaryab.SelectedValue) Then
            MessageBox.Show("فروشنده را انتخاب کنید")
            cmbBazaryab.Focus()
            Return False
        End If
        If cmbMamorPakhsh.SelectedIndex < 0 OrElse IsDBNull(cmbMamorPakhsh.SelectedValue) Then
            MessageBox.Show("مامور پخش را انتخاب کنید")
            cmbMamorPakhsh.Focus()
            Return False
        End If
        Return True
    End Function

    Private Sub btnSaveSanad_Click(sender As Object, e As EventArgs) Handles btnSaveSanad.Click
        If ccElamMarjoee <> 0 Then Exit Sub
        If Not ValidateTitr() Then Exit Sub
        AddNewRecord()
        If ccElamMarjoee = 0 Then Exit Sub

        LockHeader(True)
        SearchSatr()
        GridEXSatr.Enabled = True
        GridEXSatr.Focus()
        GridEXSatr.MoveToNewRecord()
        GridEXSatr.Col = 0
    End Sub

    Private Sub AddNewRecord()
        Try
            Using cn As New SqlConnection(ConnectionString)
                Using cm As SqlCommand = cn.CreateCommand()
                    cn.Open()
                    cm.CommandType = CommandType.StoredProcedure
                    cm.CommandText = "[Sales].[spMarjoeeVorodKoli_InsertTitr]"
                    cm.Parameters.AddWithValue("CodeMahal", CodeMahalFaal)
                    cm.Parameters.AddWithValue("CodeDoreh", CodeDoreh)
                    cm.Parameters.AddWithValue("TarikhElamMarjoee", mskTarikh.Text)
                    cm.Parameters.AddWithValue("ccFaktorTitr", ccFaktorTitr)
                    cm.Parameters.AddWithValue("ccMoshtary", ccMoshtary)
                    cm.Parameters.AddWithValue("ccForoshandeh", cmbBazaryab.SelectedValue)
                    cm.Parameters.AddWithValue("CodeFard_MamorPakhsh", cmbMamorPakhsh.SelectedValue)
                    cm.Parameters.AddWithValue("NoeMarjoeeFT", If(rbBargashtAzForosh.Checked, 1, 2))
                    cm.Parameters.AddWithValue("Tozihat", TxtSharh.Text.TrimEnd)
                    cm.Parameters.AddWithValue("UserName", UserName)
                    Dim pOut As SqlParameter = cm.Parameters.Add("@ccElamMarjoee", SqlDbType.Int)
                    pOut.Direction = ParameterDirection.Output
                    cm.ExecuteNonQuery()
                    If pOut.Value IsNot DBNull.Value Then ccElamMarjoee = Convert.ToInt32(pOut.Value)
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
#End Region

#Region "نمایش اعلام مرجوعی ذخیره‌شده"
    Private Shared Function RStr(ByVal r As DataRow, ByVal col As String) As String
        If Not r.Table.Columns.Contains(col) Then Return ""
        If IsDBNull(r(col)) Then Return ""
        Return r(col).ToString()
    End Function

    Private Shared Function RInt(ByVal r As DataRow, ByVal col As String) As Integer
        If Not r.Table.Columns.Contains(col) Then Return 0
        If IsDBNull(r(col)) Then Return 0
        Return Convert.ToInt32(r(col))
    End Function

    Private Sub txtSearchNumber_KeyDown(sender As Object, e As KeyEventArgs) Handles txtSearchNumber.KeyDown
        If e.KeyCode = Keys.Space Then
            e.Handled = True
            e.SuppressKeyPress = True
            Dim frm As New frmFO_MarjoeeSearch()
            If frm.ShowDialog() = DialogResult.OK AndAlso frm.SelectedId > 0 Then ShowMarjoee(frm.SelectedId)
        ElseIf e.KeyCode <> Keys.Tab AndAlso e.KeyCode <> Keys.ShiftKey Then
            e.Handled = True
            e.SuppressKeyPress = True
        End If
    End Sub

    Private Sub txtSearchNumber_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtSearchNumber.KeyPress
        e.Handled = True
    End Sub

    Private Sub ShowMarjoee(ByVal id As Integer)
        Dim dt As New DataTable
        Try
            Using cn As New SqlConnection(ConnectionString)
                Using da As New SqlDataAdapter("SELECT * FROM qryFO_ElamMarjoee WHERE ccElamMarjoee = @id", cn)
                    da.SelectCommand.Parameters.AddWithValue("@id", id)
                    da.Fill(dt)
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
        If dt.Rows.Count = 0 Then
            MessageBox.Show("اطلاعات اعلام مرجوعی پیدا نشد")
            Exit Sub
        End If

        NewMarjoee()
        Dim r As DataRow = dt.Rows(0)

        _loading = True
        Try
            ccElamMarjoee = id
            ccFaktorTitr = RInt(r, "ccFaktorTitr")
            ccMoshtary = RInt(r, "ccMoshtary")

            txtSearchNumber.Text = RStr(r, "ShomarehElamMarjoee")
            mskTarikh.Text = RStr(r, "TarikhElamMarjoee")
            txtCodeMoshtary.Text = RStr(r, "CodeMoshtary")
            txtCodeMoshtary.Tag = ccMoshtary
            lblNameMoshtary.Text = RStr(r, "NameMoshtary")
            txtShomarehFaktor.Text = If(RInt(r, "ShomarehFaktor") > 0, RStr(r, "ShomarehFaktor"), "")
            cmbBazaryab.SelectedValue = RInt(r, "ccForoshandeh")
            cmbMamorPakhsh.SelectedValue = RInt(r, "CodeFard_MamorPakhsh")
            If RInt(r, "NoeMarjoeeFT") = 1 Then rbBargashtAzForosh.Checked = True
            If RInt(r, "NoeMarjoeeFT") = 2 Then rbBargashtAzTozi.Checked = True
            TxtSharh.Text = RStr(r, "Tozihat")
        Finally
            _loading = False
        End Try

        _locked = (RInt(r, "sVazeiat") <> 0)
        LockHeader(True)
        If _locked Then MessageBox.Show("این اعلام مرجوعی ارسال/تایید شده و قابل ویرایش نیست.")

        SearchSatr()
        GridEXSatr.Enabled = True
        BeginInvoke(New MethodInvoker(AddressOf FocusNewRow))
    End Sub

    Private Sub FocusNewRow()
        GridEXSatr.Focus()
        If Not _locked Then
            GridEXSatr.MoveToNewRecord()
            GridEXSatr.Col = GridEXSatr.RootTable.Columns("CodeKala").Position
        End If
    End Sub
#End Region

#Region "گرید: بارگذاری و ستون‌ها"
    Private Sub SearchSatr()
        Dim dt As New DataTable
        Using cn As New SqlConnection(ConnectionString)
            Using cm As SqlCommand = cn.CreateCommand()
                cm.CommandType = CommandType.StoredProcedure
                cm.CommandText = "[dbo].[spMarjoee_SearchSatr]"
                cm.Parameters.AddWithValue("@ccMarjoee", ccElamMarjoee)
                Using da As New SqlDataAdapter(cm)
                    da.Fill(dt)
                End Using
            End Using
        End Using
        For Each col As DataColumn In dt.Columns
            col.ReadOnly = False
        Next
        dt.AcceptChanges()
        dt.DefaultView.AllowEdit = True
        dt_SearchSatr = dt
        SetGridSatr()
    End Sub

    Private Sub SetCol(ByVal key As String, ByVal caption As String, ByVal width As Integer,
                       ByVal pos As Integer, ByVal editable As Boolean, Optional ByVal fmt As String = "")
        Dim c As Janus.Windows.GridEX.GridEXColumn = GridEXSatr.RootTable.Columns(key)
        c.Caption = caption
        c.Visible = True
        c.Width = width
        c.Position = pos
        c.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
        c.EditType = If(editable, Janus.Windows.GridEX.EditType.TextBox, Janus.Windows.GridEX.EditType.NoEdit)
        If fmt <> "" Then c.FormatString = fmt
    End Sub

    Private Sub FillValueList(ByVal key As String, ByVal items As String(,))
        Dim c As Janus.Windows.GridEX.GridEXColumn = GridEXSatr.RootTable.Columns(key)
        c.EditType = Janus.Windows.GridEX.EditType.DropDownList
        c.HasValueList = True
        c.ValueList.Clear()
        For i As Integer = 0 To items.GetLength(0) - 1
            c.ValueList.Add(Convert.ToInt32(items(i, 0)), items(i, 1))
        Next
    End Sub

    Private Sub SetGridSatr()
        Try
            With GridEXSatr
                .SetDataBinding(dt_SearchSatr.DefaultView, "")
                If Not _gridStructureReady Then
                    .RetrieveStructure()
                    _gridStructureReady = True
                End If
                .UpdateMode = Janus.Windows.GridEX.UpdateMode.RowUpdate
                .AllowEdit = If(_locked, Janus.Windows.GridEX.InheritableBoolean.False, Janus.Windows.GridEX.InheritableBoolean.True)
                .AllowAddNew = If(_locked, Janus.Windows.GridEX.InheritableBoolean.False, Janus.Windows.GridEX.InheritableBoolean.True)
                .AllowDelete = If(_locked, Janus.Windows.GridEX.InheritableBoolean.False, Janus.Windows.GridEX.InheritableBoolean.True)
                .TabKeyBehavior = Janus.Windows.GridEX.TabKeyBehavior.ColumnNavigation
            End With

            For Each c As Janus.Windows.GridEX.GridEXColumn In GridEXSatr.RootTable.Columns
                c.Visible = False
            Next

            SetCol("Radif", "ردیف", 40, 1, False)
            SetCol("CodeKala", "کد کالا", 90, 2, True)
            SetCol("NameKala", "نام کالا", 260, 3, False)
            SetCol("ShomarehBach", "شماره بچ", 100, 4, True)
            SetCol("Tedad3", "تعداد", 80, 5, True, "###,###.##")
            SetCol("IsJayezeh", "جایزه", 50, 6, True)
            With GridEXSatr.RootTable.Columns("IsJayezeh")
                .ColumnType = Janus.Windows.GridEX.ColumnType.CheckBox
                .EditType = Janus.Windows.GridEX.EditType.CheckBox
            End With
            SetCol("Fee", "فی", 90, 7, True, "###,###")
            SetCol("MKol", "مبلغ کل", 100, 8, False, "###,###")
            SetCol("sElatMarjoee", "علت مرجوعی", 150, 9, True)
            SetCol("NoeMarjoee", "نوع مرجوعی", 90, 10, True)
            SetCol("TarikhTolid", "تاریخ تولید", 85, 11, False)
            SetCol("TarikhEngheza", "تاریخ انقضا", 85, 12, False)
            SetCol("IRC", "IRC", 100, 13, False)
            SetCol("GTIN", "GTIN", 100, 14, False)

            ' ستون‌های کشویی
            Dim dtE As DataTable = dsForm.Tables("tblElatMarjoee")
            Dim arr(dtE.Rows.Count - 1, 1) As String
            For i As Integer = 0 To dtE.Rows.Count - 1
                arr(i, 0) = Convert.ToString(dtE.Rows(i)("Code"))
                arr(i, 1) = Convert.ToString(dtE.Rows(i)("Sharh"))
            Next
            FillValueList("sElatMarjoee", arr)
            FillValueList("NoeMarjoee", New String(,) {{"1", "خراب"}, {"2", "سالم"}})

            With GridEXSatr
                .AlternatingColors = True
                .RowFormatStyle.BackColor = Color.White
                .AlternatingRowFormatStyle.BackColor = Color.Gainsboro
                .GridLines = Janus.Windows.GridEX.GridLines.Both
            End With
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error in SetGridSatr")
        End Try
    End Sub
#End Region

#Region "گرید: قفل سلول‌ها و Enter"
    Private Function IsBonusRow(ByVal row As Janus.Windows.GridEX.GridEXRow) As Boolean
        Try
            Dim v As Object = row.Cells("IsJayezeh").Value
            If v Is Nothing OrElse IsDBNull(v) Then Return False
            Return Convert.ToBoolean(v)
        Catch
            Return False
        End Try
    End Function

    Private Function IsCellLocked(ByVal row As Janus.Windows.GridEX.GridEXRow, ByVal key As String) As Boolean
        If _locked Then Return True
        If row Is Nothing Then Return False

        Select Case key
            Case "Radif", "NameKala", "MKol", "TarikhTolid", "TarikhEngheza", "IRC", "GTIN"
                Return True
        End Select

        ' سطر ذخیره‌شده: فقط تعداد، فی (با شرط)، علت و نوع
        If row.RowType = Janus.Windows.GridEX.RowType.Record Then
            Select Case key
                Case "Tedad3", "sElatMarjoee", "NoeMarjoee"
                    Return False
                Case "Fee"
                    Return IsBonusRow(row) OrElse (ccFaktorTitr <> 0 AndAlso Not AllowChangeFeeMarjoee)
                Case Else
                    Return True
            End Select
        End If

        ' سطر جدید با فاکتور: کالا، بچ و تیک جایزه فقط از انتخاب سطر فاکتور (F2)
        If ccFaktorTitr <> 0 Then
            Select Case key
                Case "CodeKala", "ShomarehBach", "IsJayezeh"
                    Return True
                Case "Fee"
                    Return Not AllowChangeFeeMarjoee
            End Select
        End If
        Return False
    End Function

    Private Sub GridEXSatr_EditingCell(sender As Object, e As Janus.Windows.GridEX.EditingCellEventArgs) Handles GridEXSatr.EditingCell
        If GridEXSatr.CurrentRow Is Nothing OrElse e.Column Is Nothing Then Exit Sub
        If IsCellLocked(GridEXSatr.CurrentRow, e.Column.Key) Then e.Cancel = True
    End Sub

    Private Sub GoNextCol()
        Dim g As Janus.Windows.GridEX.GridEX = GridEXSatr
        If g.CurrentRow Is Nothing Then Exit Sub

        Dim cols As New List(Of Janus.Windows.GridEX.GridEXColumn)
        For Each c As Janus.Windows.GridEX.GridEXColumn In g.RootTable.Columns
            If c.Visible AndAlso c.EditType <> Janus.Windows.GridEX.EditType.NoEdit _
               AndAlso Not IsCellLocked(g.CurrentRow, c.Key) Then cols.Add(c)
        Next
        If cols.Count = 0 Then Exit Sub
        cols.Sort(Function(a, b) a.Position.CompareTo(b.Position))

        Dim idx As Integer = -1
        If g.CurrentColumn IsNot Nothing Then idx = cols.IndexOf(g.CurrentColumn)

        If idx = -1 Then
            g.Col = cols(0).Position
        ElseIf idx < cols.Count - 1 Then
            g.Col = cols(idx + 1).Position
        Else
            g.UpdateData()
        End If
    End Sub

    Protected Overrides Function ProcessCmdKey(ByRef msg As Message, keyData As Keys) As Boolean
        If keyData = Keys.Enter AndAlso GridEXSatr.Enabled AndAlso GridEXSatr.ContainsFocus Then
            GoNextCol()
            Return True
        End If
        Return MyBase.ProcessCmdKey(msg, keyData)
    End Function
#End Region

#Region "گرید: کمکی‌ها"
    Private Function TryGetNumber(ByVal row As Janus.Windows.GridEX.GridEXRow,
                                  ByVal colKey As String, ByRef result As Double) As Boolean
        result = 0
        Dim v As Object = row.Cells(colKey).Value
        If v Is Nothing OrElse IsDBNull(v) Then Return False
        If v.ToString().Trim().Length = 0 Then Return False
        Return Double.TryParse(v.ToString().Trim(), result)
    End Function

    Private Function CellStr(ByVal row As Janus.Windows.GridEX.GridEXRow, ByVal key As String) As String
        Dim v As Object = row.Cells(key).Value
        If v Is Nothing OrElse IsDBNull(v) Then Return ""
        Return Convert.ToString(v).Trim()
    End Function

    Private Sub FocusCol(ByVal colKey As String)
        GridEXSatr.Col = GridEXSatr.RootTable.Columns(colKey).Position
    End Sub

    Private Function IsRowEmpty(ByVal row As Janus.Windows.GridEX.GridEXRow) As Boolean
        For Each key As String In New String() {"CodeKala", "ccKala", "Tedad3"}
            If CellStr(row, key).Length > 0 Then Return False
        Next
        Return True
    End Function

    Private Sub CalcRow(ByVal row As Janus.Windows.GridEX.GridEXRow)
        If row Is Nothing Then Exit Sub
        Dim tedad, fee As Double
        TryGetNumber(row, "Tedad3", tedad)
        TryGetNumber(row, "Fee", fee)
        _isCalculating = True
        Try
            row.Cells("MKol").Value = If(IsBonusRow(row), 0, tedad * fee)
        Finally
            _isCalculating = False
        End Try
    End Sub

    ' ccKala از روی کد تایپ‌شده؛ اگر نبود 0
    Private Function FindCcKala(ByVal code As String) As Integer
        Dim c As String = code.Trim()
        If Not IsNumeric(c) Then Return 0
        Return Convert.ToInt32(objTools.ConvertNulls(objTools.DLookup("ccKala", "tblAn_Kala", "Faal = 1 AND CodeKala = " & c), 0))
    End Function

    Private Sub FillKalaRow(ByVal row As Janus.Windows.GridEX.GridEXRow, ByVal ccKala As Integer,
                            ByVal codeKala As Object, ByVal nameKala As Object)
        _isCalculating = True
        Try
            row.Cells("CodeKala").Value = codeKala
            row.Cells("ccKala").Value = If(ccKala > 0, CObj(ccKala), DBNull.Value)
            row.Cells("NameKala").Value = nameKala
            row.Cells("Fee").Value = 0
            row.Cells("ShomarehBach").Value = DBNull.Value
            row.Cells("TarikhTolid").Value = DBNull.Value
            row.Cells("TarikhEngheza").Value = DBNull.Value
            row.Cells("IRC").Value = DBNull.Value
            row.Cells("GTIN").Value = DBNull.Value
            row.Cells("IsJayezeh").Value = False
            If _lastElat > 0 Then row.Cells("sElatMarjoee").Value = _lastElat
            If _lastNoe > 0 Then row.Cells("NoeMarjoee").Value = _lastNoe
        Finally
            _isCalculating = False
        End Try
        CalcRow(row)
    End Sub

    ' پر کردن سطر از انتخاب سطر فاکتور
    Private Sub ApplyFaktorPick(ByVal row As Janus.Windows.GridEX.GridEXRow, ByVal f As frmFO_KalaSearch_Faktor)
        _isCalculating = True
        Try
            With row
                .Cells("ShomarehBach").Value = f.SelectedBach
                .Cells("TarikhTolid").Value = f.SelectedTarikhTolid
                .Cells("TarikhEngheza").Value = f.SelectedTarikhEngheza
                .Cells("IRC").Value = f.SelectedIRC
                .Cells("GTIN").Value = f.SelectedGTIN
                .Cells("Fee").Value = If(f.SelectedIsJayezeh, 0, f.SelectedFee)
                .Cells("Tedad3").Value = f.SelectedBaghi
                .Cells("IsJayezeh").Value = f.SelectedIsJayezeh
            End With
        Finally
            _isCalculating = False
        End Try
        CalcRow(row)
    End Sub

    ' قیمت فروش بدون فاکتور
    Private Function GetFeeNoFaktor(ByVal ccKala As Integer) As Double
        Dim noe As Object = objTools.ConvertNulls(objTools.DLookup("sNoeMoshtary", "qryFO_Moshtary", "ccMoshtary = " & ccMoshtary), 0)
        Using cn As New SqlConnection(ConnectionString)
            Using cm As New SqlCommand("SELECT ISNULL(dbo.GetMablaghFrosh(@k,@t,@m,@c,@n),0)", cn)
                cm.Parameters.AddWithValue("@k", ccKala)
                cm.Parameters.AddWithValue("@t", Convert.ToString(TarikhEmrooz))
                cm.Parameters.AddWithValue("@m", CodeMahalFaal)
                cm.Parameters.AddWithValue("@c", ccMoshtary)
                cm.Parameters.AddWithValue("@n", noe)
                cn.Open()
                Dim r As Object = cm.ExecuteScalar()
                If r Is Nothing OrElse IsDBNull(r) Then Return 0
                Return Convert.ToDouble(r)
            End Using
        End Using
    End Function

    ' بدون فاکتور: با تایپ بچ، تاریخ‌ها و IRC/GTIN از جدول بچ پر می‌شود
    Private Sub AutoFillBach(ByVal row As Janus.Windows.GridEX.GridEXRow)
        Dim ccKalaObj As Object = row.Cells("ccKala").Value
        Dim bach As String = CellStr(row, "ShomarehBach")
        If ccKalaObj Is Nothing OrElse IsDBNull(ccKalaObj) OrElse bach = "" Then Exit Sub

        Dim dt As New DataTable
        Using cn As New SqlConnection(ConnectionString)
            'Dim sql As String = "SELECT TOP 1 dbo.SetDateSlash(TarikhTolid) AS T, dbo.SetDateSlash(TarikhEngheza) AS E, IRC, GTIN " &
            '                    "FROM tblGL_ShomarehBach WHERE ccKala = @k AND LTRIM(RTRIM(ShomarehBach)) = @b ORDER BY ccShomarehBach DESC"
            Dim sql As String = "SELECT TOP 1 TarikhTolid AS T, TarikhEngheza AS E, IRC, GTIN " &
                    "FROM tblGL_ShomarehBach WHERE ccKala = @k AND LTRIM(RTRIM(ShomarehBach)) = @b ORDER BY ccShomarehBach DESC"
            Using da As New SqlDataAdapter(sql, cn)
                da.SelectCommand.Parameters.AddWithValue("@k", Convert.ToInt32(ccKalaObj))
                da.SelectCommand.Parameters.AddWithValue("@b", bach)
                da.Fill(dt)
            End Using
        End Using

        _isCalculating = True
        Try
            If dt.Rows.Count = 0 Then
                MessageBox.Show("این شماره بچ برای این کالا وجود ندارد. با F5 از لیست انتخاب کنید.")
                row.Cells("ShomarehBach").Value = DBNull.Value
                row.Cells("TarikhTolid").Value = DBNull.Value
                row.Cells("TarikhEngheza").Value = DBNull.Value
            Else
                Dim r As DataRow = dt.Rows(0)
                row.Cells("TarikhTolid").Value = Convert.ToString(r("T"))
                row.Cells("TarikhEngheza").Value = Convert.ToString(r("E"))
                row.Cells("IRC").Value = If(IsDBNull(r("IRC")), "", Convert.ToString(r("IRC")))
                row.Cells("GTIN").Value = If(IsDBNull(r("GTIN")), "", Convert.ToString(r("GTIN")))
                If Not IsBonusRow(row) Then row.Cells("Fee").Value = GetFeeNoFaktor(Convert.ToInt32(ccKalaObj))
            End If
        Finally
            _isCalculating = False
        End Try
        CalcRow(row)
    End Sub

    Private Sub GridEXSatr_CellUpdated(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles GridEXSatr.CellUpdated
        If _isCalculating Then Exit Sub
        If e.Column Is Nothing OrElse GridEXSatr.CurrentRow Is Nothing Then Exit Sub
        Dim row = GridEXSatr.CurrentRow
        Dim isNew As Boolean = (row.RowType = Janus.Windows.GridEX.RowType.NewRecord)

        Select Case e.Column.Key
            Case "CodeKala"
                If isNew AndAlso ccFaktorTitr = 0 Then
                    Dim typed As String = CellStr(row, "CodeKala")
                    If typed <> "" Then
                        Dim cc As Integer = FindCcKala(typed)
                        If cc = 0 Then
                            MessageBox.Show("کالایی با این کد پیدا نشد. برای جستجو F2 بزنید.")
                            FillKalaRow(row, 0, DBNull.Value, DBNull.Value)
                        Else
                            FillKalaRow(row, cc, typed, objTools.DLookup("NameKala", "tblAn_Kala", "ccKala = " & cc))
                        End If
                    End If
                End If
            Case "ShomarehBach"
                If isNew AndAlso ccFaktorTitr = 0 Then AutoFillBach(row)
            Case "IsJayezeh"
                If IsBonusRow(row) Then
                    _isCalculating = True
                    Try
                        row.Cells("Fee").Value = 0
                    Finally
                        _isCalculating = False
                    End Try
                End If
                CalcRow(row)
            Case "Tedad3", "Fee"
                CalcRow(row)
        End Select
    End Sub
#End Region

#Region "گرید: F2 / F5"
    Private Sub GridEXSatr_KeyDown(sender As Object, e As KeyEventArgs) Handles GridEXSatr.KeyDown
        If e.KeyCode <> Keys.F2 AndAlso e.KeyCode <> Keys.F5 Then Exit Sub
        If _locked Then Exit Sub
        If GridEXSatr.CurrentRow Is Nothing OrElse GridEXSatr.CurrentColumn Is Nothing Then Exit Sub
        If GridEXSatr.CurrentRow.RowType <> Janus.Windows.GridEX.RowType.NewRecord Then Exit Sub

        ' ---------- F2 : کالا ----------
        If e.KeyCode = Keys.F2 AndAlso GridEXSatr.CurrentColumn.Key = "CodeKala" Then
            If ccFaktorTitr <> 0 Then
                Dim f As New frmFO_KalaSearch_Faktor(ConnectionString, ccFaktorTitr)
                If f.ShowDialog() <> DialogResult.OK OrElse f.SelectedCcKala = 0 Then Exit Sub
                FillKalaRow(GridEXSatr.CurrentRow, f.SelectedCcKala, f.SelectedCodeKala, f.SelectedNameKala)
                ApplyFaktorPick(GridEXSatr.CurrentRow, f)
                FocusCol("Tedad3")
            Else
                Dim objKala As New frmAN_KalaSearch
                Dim StrSqlKala As String = "Select 0 as ccFaktorSatr, CodeKala,NameKala,ccKala,txtsVahedeShomaresh,sVahedeShomaresh,NameBrand,RadifBrand,0 as IsSabadKala "
                StrSqlKala &= " from qryAN_Kala Where Faal = 1"
                MultiSelection = False
                SearchItem = "CodeKala"
                objKala.SetForm(StrSqlKala)
                objKala.ShowDialog()
                If Val(objKala.tccKala) = 0 Then Exit Sub
                FillKalaRow(GridEXSatr.CurrentRow, CInt(objKala.tccKala), objKala.tcodeKala, objKala.tNameKala)
                FocusCol("ShomarehBach")
            End If
            e.Handled = True
            Exit Sub
        End If

        ' ---------- F5 : شماره بچ (فقط بدون فاکتور) ----------
        If e.KeyCode = Keys.F5 AndAlso GridEXSatr.CurrentColumn.Key = "ShomarehBach" Then
            If ccFaktorTitr <> 0 Then
                e.Handled = True
                Exit Sub
            End If

            Dim ccKalaObj As Object = GridEXSatr.CurrentRow.Cells("ccKala").Value
            If ccKalaObj Is Nothing OrElse IsDBNull(ccKalaObj) OrElse Val(ccKalaObj) = 0 Then
                MessageBox.Show("ابتدا کالا را انتخاب کنید")
                FocusCol("CodeKala")
                Exit Sub
            End If

            Dim objBach As New Forms_dll.ShomarehBachSearch
            Dim StrSql As String = "Select ShomarehBach,TarikhTolidSlash,TarikhEnghezaSlash,ccShomarehBach, "
            StrSql &= " NameTaminKonandeh,IsNull(NameTolidKonandeh,'---'),NameKala, CAST(0 AS float) As MojodiDarHalForoshBach, IRC, GTIN "
            StrSql &= " From QryGL_ShomarehBach as Q Where ccKala = " & Val(ccKalaObj)

            objBach.SearchItem = "ShomarehBach"
            objBach.SetForm(StrSql)
            objBach.ShowDialog()
            If String.IsNullOrEmpty(objBach.tShomarehBach) Then Exit Sub

            Dim row = GridEXSatr.CurrentRow
            _isCalculating = True
            Try
                row.Cells("ShomarehBach").Value = objBach.tShomarehBach
                row.Cells("TarikhTolid").Value = objBach.tTarikhTolid
                row.Cells("TarikhEngheza").Value = objBach.tTarikhEngheza
                row.Cells("IRC").Value = objBach.tiRC
                row.Cells("GTIN").Value = objBach.tGTIN
                If Not IsBonusRow(row) Then row.Cells("Fee").Value = GetFeeNoFaktor(CInt(ccKalaObj))
            Finally
                _isCalculating = False
            End Try
            CalcRow(row)
            e.Handled = True
        End If
    End Sub
#End Region

#Region "گرید: اعتبارسنجی و ذخیره"
    Private Function ValidateRow(ByVal row As Janus.Windows.GridEX.GridEXRow, ByVal isNew As Boolean) As Boolean
        Dim num As Double

        Dim ccKala As Object = row.Cells("ccKala").Value
        If ccKala Is Nothing OrElse IsDBNull(ccKala) OrElse ccKala.ToString().Trim() = "" Then
            MessageBox.Show("کالا را انتخاب کنید")
            FocusCol("CodeKala")
            Return False
        End If
        If Not TryGetNumber(row, "Tedad3", num) OrElse num <= 0 Then
            MessageBox.Show("تعداد را وارد کنید")
            FocusCol("Tedad3")
            Return False
        End If
        If CellStr(row, "ShomarehBach") = "" Then
            MessageBox.Show("شماره بچ را وارد کنید")
            FocusCol("ShomarehBach")
            Return False
        End If
        If Not IsBonusRow(row) Then
            If Not TryGetNumber(row, "Fee", num) OrElse num <= 0 Then
                MessageBox.Show("قیمت (فی) را وارد کنید")
                FocusCol("Fee")
                Return False
            End If
        End If
        If CellStr(row, "TarikhTolid") = "" OrElse CellStr(row, "TarikhEngheza") = "" Then
            MessageBox.Show("تاریخ تولید و انقضا خالی است. بچ را از لیست انتخاب کنید.")
            FocusCol("ShomarehBach")
            Return False
        End If
        If Not TryGetNumber(row, "sElatMarjoee", num) OrElse num <= 0 Then
            MessageBox.Show("علت مرجوعی را انتخاب کنید")
            FocusCol("sElatMarjoee")
            Return False
        End If
        If Not TryGetNumber(row, "NoeMarjoee", num) OrElse num <= 0 Then
            MessageBox.Show("نوع مرجوعی (خراب/سالم) را انتخاب کنید")
            FocusCol("NoeMarjoee")
            Return False
        End If

        ' فاکتور: باقی‌مانده (موجودی انبار اینجا مهم نیست)
        If ccFaktorTitr <> 0 Then
            Dim idSatr As Object = If(isNew, 0, row.Cells("ccMarjoeeSatr").Value)
            Dim baghi As Double = GetBaghi(Convert.ToInt32(ccKala), CellStr(row, "ShomarehBach"), IsBonusRow(row), idSatr)
            TryGetNumber(row, "Tedad3", num)
            If num > baghi Then
                MessageBox.Show("تعداد از باقی‌مانده‌ی فاکتور بیشتر است." & vbCrLf &
                                "شماره بچ: " & CellStr(row, "ShomarehBach") & vbCrLf &
                                If(IsBonusRow(row), "نوع: جایزه" & vbCrLf, "") &
                                "حداکثر مجاز: " & baghi.ToString(),
                                "باقی‌مانده‌ی فاکتور", MessageBoxButtons.OK, MessageBoxIcon.Warning,
                                MessageBoxDefaultButton.Button1,
                                MessageBoxOptions.RightAlign Or MessageBoxOptions.RtlReading)
                FocusCol("Tedad3")
                Return False
            End If
        End If
        Return True
    End Function

    Private Function GetBaghi(ByVal ccKala As Integer, ByVal bach As String, ByVal isJay As Boolean, ByVal excludeSatr As Object) As Double
        Using cn As New SqlConnection(ConnectionString)
            Using cm As New SqlCommand("SELECT dbo.Marjoee_Baghi(@f,@k,@b,@j,@ex)", cn)
                cm.Parameters.AddWithValue("@f", ccFaktorTitr)
                cm.Parameters.AddWithValue("@k", ccKala)
                cm.Parameters.AddWithValue("@b", bach)
                cm.Parameters.AddWithValue("@j", isJay)
                cm.Parameters.AddWithValue("@ex", If(excludeSatr Is Nothing OrElse IsDBNull(excludeSatr), 0, excludeSatr))
                cn.Open()
                Dim r As Object = cm.ExecuteScalar()
                If r Is Nothing OrElse IsDBNull(r) Then Return 0
                Return Convert.ToDouble(r)
            End Using
        End Using
    End Function

    Private Function SaveRow(ByVal row As Janus.Windows.GridEX.GridEXRow, ByVal isNew As Boolean) As Boolean
        If row Is Nothing Then Return False
        If isNew AndAlso IsRowEmpty(row) Then Return False
        If Not ValidateRow(row, isNew) Then Return False

        Try
            Dim tedad, fee, elat, noe As Double
            TryGetNumber(row, "Tedad3", tedad)
            TryGetNumber(row, "Fee", fee)
            TryGetNumber(row, "sElatMarjoee", elat)
            TryGetNumber(row, "NoeMarjoee", noe)

            Using con As New SqlConnection(ConnectionString)
                Using cmd As SqlCommand = con.CreateCommand()
                    cmd.CommandType = CommandType.StoredProcedure
                    If isNew Then
                        cmd.CommandText = "[dbo].[spFO_Marjoee_InsertSatr]"
                        cmd.Parameters.AddWithValue("@ccMarjoee", ccElamMarjoee)
                        cmd.Parameters.AddWithValue("@ccKala", Convert.ToInt32(row.Cells("ccKala").Value))
                        cmd.Parameters.AddWithValue("@Tedad3", tedad)
                        cmd.Parameters.AddWithValue("@IsJayezeh", IsBonusRow(row))
                        cmd.Parameters.AddWithValue("@Fee", fee)
                        cmd.Parameters.AddWithValue("@TarikhTolid", CellStr(row, "TarikhTolid"))
                        cmd.Parameters.AddWithValue("@TarikhEngheza", CellStr(row, "TarikhEngheza"))
                        cmd.Parameters.AddWithValue("@ShomarehBach", CellStr(row, "ShomarehBach"))
                        cmd.Parameters.AddWithValue("@sElatMarjoee", CInt(elat))
                        cmd.Parameters.AddWithValue("@NoeMarjoee", CInt(noe))
                        cmd.Parameters.AddWithValue("@UserName", UserName)
                        Dim outId As New SqlParameter("@ccMarjoeeSatr", SqlDbType.Int) With {.Direction = ParameterDirection.Output}
                        cmd.Parameters.Add(outId)
                    Else
                        cmd.CommandText = "[dbo].[spFO_Marjoee_UpdateSatr]"
                        cmd.Parameters.AddWithValue("@ccMarjoeeSatr", row.Cells("ccMarjoeeSatr").Value)
                        cmd.Parameters.AddWithValue("@Tedad3", tedad)
                        cmd.Parameters.AddWithValue("@Fee", fee)
                        cmd.Parameters.AddWithValue("@sElatMarjoee", CInt(elat))
                        cmd.Parameters.AddWithValue("@NoeMarjoee", CInt(noe))
                        cmd.Parameters.AddWithValue("@UserName", UserName)
                    End If
                    con.Open()
                    cmd.ExecuteNonQuery()
                End Using
            End Using

            _lastElat = CInt(elat)
            _lastNoe = CInt(noe)
            RecalcMalyat()
            Return True
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Return False
        End Try
    End Function

    Private Sub GridEXSatr_AddingRecord(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles GridEXSatr.AddingRecord
        If Not SaveRow(GridEXSatr.CurrentRow, True) Then e.Cancel = True
    End Sub

    Private Sub GridEXSatr_UpdatingRecord(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles GridEXSatr.UpdatingRecord
        If Not SaveRow(GridEXSatr.CurrentRow, False) Then e.Cancel = True
    End Sub

    Private Sub GridEXSatr_RecordAdded(sender As Object, e As EventArgs) Handles GridEXSatr.RecordAdded
        BeginInvoke(New MethodInvoker(AddressOf ReloadSatr))
    End Sub

    Private Sub GridEXSatr_RecordUpdated(sender As Object, e As EventArgs) Handles GridEXSatr.RecordUpdated
        BeginInvoke(New MethodInvoker(AddressOf ReloadSatr))
    End Sub

    Private Sub ReloadSatr()
        SearchSatr()
        If Not _locked Then GridEXSatr.MoveToNewRecord()
        GridEXSatr.Col = 0
        GridEXSatr.Focus()
    End Sub

    Private Sub GridEXSatr_DeletingRecord(sender As Object, e As Janus.Windows.GridEX.RowActionCancelEventArgs) Handles GridEXSatr.DeletingRecord
        e.Cancel = True
        If _locked Then Exit Sub

        Dim row As Janus.Windows.GridEX.GridEXRow = GridEXSatr.CurrentRow
        If row Is Nothing OrElse row.RowType <> Janus.Windows.GridEX.RowType.Record Then Exit Sub
        Dim id As Object = row.Cells("ccMarjoeeSatr").Value
        If id Is Nothing OrElse IsDBNull(id) Then Exit Sub

        If MessageBox.Show("آیا مایل به حذف ردیف هستید؟", "حذف", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                           MessageBoxDefaultButton.Button2,
                           MessageBoxOptions.RightAlign Or MessageBoxOptions.RtlReading) <> DialogResult.Yes Then Exit Sub

        Try
            Using con As New SqlConnection(ConnectionString)
                Using cmd As SqlCommand = con.CreateCommand()
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.CommandText = "[Sales].[spElamMarjoee_DeleteSatr]"
                    cmd.Parameters.AddWithValue("ccMarjoeeSatr", id)
                    con.Open()
                    cmd.ExecuteNonQuery()
                End Using
            End Using
            RecalcMalyat()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
        BeginInvoke(New MethodInvoker(AddressOf ReloadSatr))
    End Sub
#End Region

#Region "مالیات و عوارض / ارسال"
    Private Sub RecalcMalyat()
        If ccElamMarjoee = 0 Then Exit Sub
        Try
            If ccFaktorTitr <> 0 Then
                Using con As New SqlConnection(ConnectionString)
                    Using cmd As SqlCommand = con.CreateCommand()
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.CommandText = "[dbo].[spFO_Marjoee_RecalcMalyat]"
                        cmd.Parameters.AddWithValue("@ccMarjoee", ccElamMarjoee)
                        cmd.Parameters.AddWithValue("@TakhfifMode", IsMalyatAvarezTakhfif)
                        con.Open()
                        cmd.ExecuteNonQuery()
                    End Using
                End Using
            Else
                ' بدون فاکتور: با توابع سیستم
                Dim dt As New DataTable
                Using cn As New SqlConnection(ConnectionString)
                    cn.Open()
                    Using da As New SqlDataAdapter("SELECT ccMarjoeeSatr, MKOL FROM tblFO_ElamMarjoeeSatr WHERE ccMarjoee = @m AND ISNULL(IsJayezehMarjoee,0) = 0 " &
                            "AND ccKala IN (SELECT ccKala FROM tblAN_Kala WHERE MashmuleAvarez = 1 AND MashmuleMaliyat = 1)", cn)
                        da.SelectCommand.Parameters.AddWithValue("@m", ccElamMarjoee)
                        da.Fill(dt)
                    End Using
                    For Each dr As DataRow In dt.Rows
                        Dim m As Double = Math.Round(ObjCode.GetMablaghMalyat(dr("MKOL")) + ObjCode.GetMablaghAvarez(dr("MKOL")), 0)
                        Using cm As New SqlCommand("UPDATE tblFO_ElamMarjoeeSatr SET MablaghMaliatAvarez = @v WHERE ccMarjoeeSatr = @id", cn)
                            cm.Parameters.AddWithValue("@v", m)
                            cm.Parameters.AddWithValue("@id", dr("ccMarjoeeSatr"))
                            cm.ExecuteNonQuery()
                        End Using
                    Next
                End Using
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub


#End Region

End Class