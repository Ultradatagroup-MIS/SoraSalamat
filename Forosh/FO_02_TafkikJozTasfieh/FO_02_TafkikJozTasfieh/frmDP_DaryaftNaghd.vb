Public Class frmDP_DaryaftNaghd

#Region "Variable AND Constant Declration"
    'Const FormTableName = "tblDP_Amalyat"
    'Const FormViewName = "qryDP_Amalyat"
    Const FormAddSize = 92
    Const FormOrgSize = 190
    Const GridAddSize = 200

    Dim ErrPro As New ErrorProvider
    Dim Mode As UD_Dll.Enums.GL_ModeForms = UD_Dll.Enums.GL_ModeForms.AddNewRecord
    Dim dsForm As New DataSet
    Dim cmForm As CurrencyManager
    Dim dvForm As DataView
    Dim txtCaption As String
    Private WithEvents BS As New UD_Dll.PassString
    Dim cntCodeSubSystem As Long = 958
    Private SN As Integer

    Dim Flag As Boolean = False

    Dim ccTafkikJozeTasfiehSatr_Naghd As Integer = 0

    Dim flg As Boolean = False ' flag grid

    '=-------------------
    Public ccMoshtary As Long = 0
    Public ccFaktorTitr As Long = 0
    Public ccMamorPakhsh As Long = 0
    Public MablaghFaktor As Long = 0
    Public ccTafkikJozeTasfiehSatr As Long = 0
    Public MandehFaktor As Long = 0

#End Region
    Private Sub frmDP_DaryaftNaghd_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Mode = UD_Dll.Enums.GL_ModeForms.AddNewRecord
        LoadForm()
        CancelForm()
        objTools.ChangeKbd(UD_Dll.mdlUtility.KBD.Farsi)
    End Sub
    Private Sub LoadForm()
        Dim strSQL As String
        Dim cnSQL As New SqlConnection
        Dim cmSQL As New SqlCommand
        Dim daSQL As SqlDataAdapter

        Try

            lblShomarehFaktor.Text = objTools.DLookup("FaktorShomareh", "tblFO_Faktor", "ccFaktorTitr = " & ccFaktorTitr)
            lblNameMoshtary.Text = objTools.DLookup("NameMoshtary", "tblFO_Moshtary", "ccMoshtary = " & ccMoshtary)
            lblAvarandehVajh.Text = objTools.DLookup("FName + ' ' + LName", "tblGL_MoshakhasatFardi", "CodeFard = " & ccMamorPakhsh)
            lblMablaghFaktor.Text = objTools.DLookup("JamKol", "tblFO_Faktor", "ccFaktorTitr = " & ccFaktorTitr)
            lblMandehFaktor.Text = MandehFaktor
            lblShomarehFaktor.TextAlign = ContentAlignment.MiddleCenter
            lblNameMoshtary.TextAlign = ContentAlignment.MiddleCenter
            lblAvarandehVajh.TextAlign = ContentAlignment.MiddleCenter
            lblMablaghFaktor.TextAlign = ContentAlignment.MiddleCenter
            mskTarikhDP.Text = TarikhEmrooz

            '-------------------------------------------
            strSQL = "Global.spShomarehHesab_LoadCombo "

            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            cmSQL.Parameters.AddWithValue("CodeMahal", CodeMahalFaal)

            daSQL = New SqlDataAdapter(cmSQL)
            daSQL.Fill(dsForm, "ComboShomarehHesab")
            cmbShomarehHesab.DataSource = Nothing
            cmbShomarehHesab.Items.Clear()
            cmbShomarehHesab.DataSource = dsForm.Tables("ComboShomarehHesab").DefaultView
            cmbShomarehHesab.DisplayMember = "ShomarehHesab"
            cmbShomarehHesab.ValueMember = "ccShomarehHesab"
            If dsForm.Tables("ComboShomarehHesab").DefaultView.Count = 0 Then
                MsgBox("برای استفاده از این فرم ابتدا باید شماره حساب های شرکت را تعریف کنید.", MsgBoxStyle.Information Or MsgBoxStyle.MsgBoxRtlReading, "پیام")
                Me.Close()
            Else
                cmbShomarehHesab.SelectedIndex = 0
                'tShHEntry = cmbShomarehHesab.SelectedValue
            End If
            cmbShomarehHesab.SelectedIndex = -1
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> LoadForm ")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> LoadForm ")
        End Try
    End Sub
    Public Sub CancelForm()
        Mode = UD_Dll.Enums.GL_ModeForms.AddNewRecord
        ClearForm()
        DisableGrid()
        SetButton()
    End Sub
    Private Sub ClearForm()
        cmbShomarehHesab.SelectedIndex = -1
        mskTarikhDP.Text = ""
        txtSharh.Text = ""
        txtResidMovaghat.Text = ""
        txtMablaghKol.Text = ""
        txtMablaghPFaktor.Text = ""
        ccTafkikJozeTasfiehSatr_Naghd = 0
    End Sub
    Private Sub DisableGrid()
        GridEXNaghd.Visible = False
        'Me.Height = FormOrgSize
        If GridEXNaghd.Height >= GridAddSize Then
            GridEXNaghd.Height = GridEXNaghd.Height - GridAddSize
        End If
        Me.CenterToScreen()
    End Sub
    Public Sub SetButton()
        Me.btnSave.Enabled = False
        Me.btnDelete.Enabled = False
        Me.btnCancel.Enabled = False
        Me.btnExit.Enabled = True
        Me.btnRefresh.Enabled = False
        If Mode = UD_Dll.Enums.GL_ModeForms.AddNewRecord Then
            Me.btnSave.Enabled = True
            Me.btnCancel.Enabled = True
            Me.btnRefresh.Enabled = True
        ElseIf Mode = UD_Dll.Enums.GL_ModeForms.UpdateRecord Then
            Me.btnSave.Enabled = True
            Me.btnCancel.Enabled = True
            Me.btnDelete.Enabled = True
        ElseIf Mode = UD_Dll.Enums.GL_ModeForms.Search Then
            Me.btnCancel.Enabled = True
            Me.btnDelete.Enabled = True
        End If
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If Mode = UD_Dll.Enums.GL_ModeForms.AddNewRecord Then
            AddNewRecord()
        ElseIf Mode = UD_Dll.Enums.GL_ModeForms.UpdateRecord Then
            UpdateRecord()
        End If
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        If MsgBox("عملیات حـــذف انجام شود ؟", MsgBoxStyle.Question + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.YesNo, "") = MsgBoxResult.Yes Then
            DeleteRecord()
            search()
        End If
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        Mode = UD_Dll.Enums.GL_ModeForms.Search
        search()
        SetButton()
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        CancelForm()
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        frmFO_TafkikJozTasfieh.mskNaghdSatr.Text = objTools.ConvertNulls(objTools.DSum("PardakhtyInFaktor", "Sales.TafkikJozeTasfiehSatrNaghd", "ccFaktor = " & ccFaktorTitr), 0)
        Close()
    End Sub
    Private Sub search()
        Try
            Dim StrSql As String

            StrSql = "Sales.spTafkikJozTasfiehSatrNaghd_Search "

            RefreshTitrdata(StrSql)
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->Search")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->Search")
        End Try
    End Sub
    Private Sub RefreshTitrdata(ByVal StrSql As String)
        Try
            Dim cn As SqlConnection
            Dim cm As SqlCommand
            Dim da As SqlDataAdapter

            If dsForm.Tables.Contains("tblTafkikJozTasfiehSatrNaghd") Then
                dsForm.Tables.Remove("tblTafkikJozTasfiehSatrNaghd")
            End If

            cn = New SqlConnection(ConnectionString)
            cn.Open()

            cm = New SqlCommand(StrSql, cn)
            cm.CommandType = CommandType.StoredProcedure
            cm.Parameters.Clear()

            cm.Parameters.AddWithValue("ccFaktor", ccFaktorTitr)

            da = New SqlDataAdapter(cm)
            da.Fill(dsForm, "tblTafkikJozTasfiehSatrNaghd")

            dvForm = New DataView(dsForm.Tables("tblTafkikJozTasfiehSatrNaghd"), "", "FaktorShomareh ASC", DataViewRowState.CurrentRows)
            dvForm.AllowNew = False
            dvForm.AllowDelete = False
            dvForm.AllowEdit = False

            cm.Connection.Close()
            cn.Close()
            cm = Nothing
            da = Nothing

            GridEXNaghd.DataSource = Nothing
            GridEXNaghd.DataSource = dvForm

            SetGridStyle()

            'If dvForm.Count = 0 Then
            '    CancelForm()
            'Else
            '    Mode = UD_Dll.Enums.GL_ModeForms.Search
            'End If

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->Search")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->Search")
        End Try
    End Sub
    Private Sub AddNewRecord()
        Try
            If Not IsValidForm("All") Then Exit Sub

            Dim cn As SqlConnection
            Dim cm As SqlCommand
            Dim strSQL As String = ""

            strSQL = "Sales.spTafkikJozTasfiehSatrNaghd_Insert "

            cn = New SqlConnection(ConnectionString)
            cn.Open()

            cm = New SqlCommand(strSQL, cn)
            cm.CommandType = CommandType.StoredProcedure
            cm.Parameters.Clear()

            cm.Parameters.AddWithValue("ccTafkikJozeTasfiehSatr", ccTafkikJozeTasfiehSatr)
            cm.Parameters.AddWithValue("ccFaktor", ccFaktorTitr)
            cm.Parameters.AddWithValue("ccMoshtary", ccMoshtary)
            cm.Parameters.AddWithValue("ccShomarehHesab", cmbShomarehHesab.SelectedValue)
            cm.Parameters.AddWithValue("ccMamorPakhsh", ccMamorPakhsh)
            cm.Parameters.AddWithValue("MablaghKolNaghd", txtMablaghKol.Text)
            cm.Parameters.AddWithValue("PardakhtyInFaktor", txtMablaghPFaktor.Text)
            cm.Parameters.AddWithValue("TarikhDP", mskTarikhDP.Text)
            cm.Parameters.AddWithValue("TarikhSanad", mskTarikhDP.Text)
            cm.Parameters.AddWithValue("ShomarehResidMovaghat", txtResidMovaghat.Text)
            cm.Parameters.AddWithValue("Sharh", txtSharh.Text)

            cm.ExecuteNonQuery()

            cm.Connection.Close()
            cn.Close()
            cm = Nothing

            MsgBox("عملیات ثبت با موفقیت انجام شد .", MsgBoxStyle.Information + MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, "")

            ClearForm()

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> AddNewRecord ")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> AddNewRecord ")
        End Try
    End Sub
    Private Sub UpdateRecord()
        Try
            If Not IsValidForm("All") Then Exit Sub

            Dim cn As SqlConnection
            Dim cm As SqlCommand
            Dim strSQL As String = ""

            strSQL = "Sales.spTafkikJozTasfiehSatrNaghd_Update "

            cn = New SqlConnection(ConnectionString)
            cn.Open()

            cm = New SqlCommand(strSQL, cn)
            cm.CommandType = CommandType.StoredProcedure
            cm.Parameters.Clear()

            cm.Parameters.AddWithValue("ccShomarehHesab", cmbShomarehHesab.SelectedValue)
            cm.Parameters.AddWithValue("MablaghKolNaghd", txtMablaghKol.Text)
            cm.Parameters.AddWithValue("PardakhtyInFaktor", txtMablaghPFaktor.Text)
            cm.Parameters.AddWithValue("TarikhDP", mskTarikhDP.Text)
            cm.Parameters.AddWithValue("TarikhSanad", mskTarikhDP.Text)
            cm.Parameters.AddWithValue("ShomarehResidMovaghat", txtResidMovaghat.Text.Trim)
            cm.Parameters.AddWithValue("Sharh", txtSharh.Text.Trim)
            cm.Parameters.AddWithValue("ccTafkikJozeTasfiehSatr_Naghd", ccTafkikJozeTasfiehSatr_Naghd)

            cm.ExecuteNonQuery()

            cm.Connection.Close()
            cn.Close()
            cm = Nothing

            MsgBox("عملیات به روز رسانی با موفقیت انجام شد .", MsgBoxStyle.Information + MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, "")

            ClearForm()

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> UpdateRecord ")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> UpdateRecord ")
        End Try
    End Sub
    Private Sub DeleteRecord()
        Try
            Dim cn As SqlConnection
            Dim cm As SqlCommand
            Dim strSQL As String = "Sales.spTafkikJozTasfiehSatrNaghd_Delete "

            Dim ccTJTS_N As Integer = 0
            If Mode = UD_Dll.Enums.GL_ModeForms.Search Then
                ccTJTS_N = Val(GridEXNaghd.CurrentRow.Cells("ccTafkikJozeTasfiehSatr_Naghd").Text.Replace(",", ""))
            ElseIf Mode = UD_Dll.Enums.GL_ModeForms.UpdateRecord Then
                ccTJTS_N = ccTafkikJozeTasfiehSatr_Naghd
            End If

            cn = New SqlConnection(ConnectionString)
            cn.Open()

            cm = New SqlCommand(strSQL, cn)
            cm.CommandType = CommandType.StoredProcedure
            cm.Parameters.Clear()

            cm.Parameters.AddWithValue("ccTafkikJozeTasfiehSatr_Naghd", ccTJTS_N)

            cm.ExecuteNonQuery()

            cm.Connection.Close()
            cn.Close()
            cm = Nothing

            MsgBox("عملیات حـــذف با موفقیت انجام شد .", MsgBoxStyle.Information + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.OkOnly, "")

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> DeleteRecord ")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> DeleteRecord ")
        End Try

    End Sub
    Private Function IsValidForm(ByVal CheckField As String) As Boolean
        IsValidForm = False

        If CheckField = "cmbShomarehHesab" Or CheckField = "All" Then
            If (IsNothing(cmbShomarehHesab.SelectedValue)) Then
                ErrPro.SetError(Me.cmbShomarehHesab, "شماره حساب را وارد کنيد")
                MsgBox("شماره حساب را وارد کنيد", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
                cmbShomarehHesab.Focus()
                Exit Function
            End If
            ErrPro.SetError(Me.cmbShomarehHesab, "")
        End If

        If CheckField = "mskTarikhDP" Or CheckField = "All" Then
            If Len(mskTarikhDP.Text.ToString) <> 0 Then
                If Not objTarikh.IsShDate(mskTarikhDP.Text.ToString) Then
                    mskTarikhDP.Focus()
                    Exit Function
                End If
            Else
                ErrPro.SetError(Me.mskTarikhDP, "تاريخ " & " راوارد کنيد")
                MsgBox("تاريخ " & " راوارد کنيد", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
                mskTarikhDP.Focus()
                Exit Function
            End If
            If CodeDoreh <> objTarikh.ShamsiYear(mskTarikhDP.Text) Then
                ErrPro.SetError(Me.mskTarikhDP, "تاريخ با دوره يکی نيست.")
                MsgBox("تاريخ با دوره يکی نيست.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
                mskTarikhDP.Focus()
                Exit Function
            End If
            'If objTarikh.ShamsiYear(mskTarikhDP.Text) <> objTarikh.ShamsiYear(TarikhEmrooz) Then
            '    ErrPro.SetError(Me.mskTarikhDP, "تاريخ بايد در سال ورود اطلاعات باشد.")
            '    MsgBox("تاريخ بايد در سال ورود اطلاعات باشد.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
            '    mskTarikhDP.Focus()
            '    Exit Function
            'End If
            If mskTarikhDP.Text > TarikhEmrooz Then
                ErrPro.SetError(Me.mskTarikhDP, "تاريخ نمی تواند جلوتر از تاریخ روز باشد.")
                MsgBox("تاريخ نمی تواند جلوتر از تاریخ روز باشد.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
                mskTarikhDP.Focus()
                Exit Function
            End If
            ErrPro.SetError(Me.mskTarikhDP, "")
        End If
        

        '??
        'If CheckField = "mskTarikhSanad" Or CheckField = "All" Then
        '    If mskTarikhSanad.Text.Length <> 0 And mskTarikhSanad.Text < mskTarikhDP.Text Then
        '        ErrPro.SetError(mskTarikhSanad, "تاریخ سررسید نمیتواند پیش از تاریخ دریافت باشد.")
        '        MsgBox("تاریخ سررسید نمیتواند پیش از تاریخ دریافت باشد.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
        '        mskTarikhSanad.Focus()
        '        Exit Function
        '    End If
        '    ErrPro.SetError(Me.mskTarikhSanad, "")
        'End If


        If CheckField = "txtMablaghKol" Or CheckField = "All" Then
            If txtMablaghKol.Text.Length = 0 Then
                ErrPro.SetError(txtMablaghKol, "مبلغ کل را وارد کنيد")
                MsgBox("مبلغ را وارد کنيد", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
                txtMablaghKol.Focus()
                Exit Function
            End If
            ErrPro.SetError(Me.txtMablaghKol, "")
        End If

        If CheckField = "txtMablaghPFaktor" Or CheckField = "All" Then
            If txtMablaghPFaktor.Text.Length = 0 Then
                ErrPro.SetError(txtMablaghPFaktor, "مبلغ پرداختی برای این فاکتور را وارد کنید.")
                MsgBox("مبلغ پرداختی برای این فاکتور را وارد کنید.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
                txtMablaghPFaktor.Focus()
                Exit Function
            End If
            ErrPro.SetError(Me.txtMablaghPFaktor, "")
        End If

        If Val(txtMablaghKol.Text) < Val(txtMablaghPFaktor.Text) Then
            ErrPro.SetError(txtMablaghPFaktor, "مبلغ پرداختی برای این فاکتور نمی تواند از کل وجه دریافتی بیشتر باشد.")
            MsgBox("مبلغ پرداختی برای این فاکتور نمی تواند از کل وجه دریافتی بیشتر باشد.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
            txtMablaghPFaktor.Focus()
            Exit Function
        End If
        ErrPro.SetError(Me.txtMablaghPFaktor, "")

        'Dim MandehFak As Double = (Val(objTools.DLookup("JamKol", "tblFO_Faktor", "ccFaktorTitr=" & ccFaktorTitr)) - Val(objTools.ConvertNulls(objTools.DSum("Mablagh", "qryDP_AmalyatFaktor", "ccFaktorTitr=" & ccFaktorTitr & " AND Ebtal=0"), 0)))
        If Val(txtMablaghPFaktor.Text) > MandehFaktor Then
            ErrPro.SetError(txtMablaghPFaktor, "مبلغ پرداختی برای این فاکتور نباید از " & MandehFaktor & " بیشتر باشد.")
            MsgBox("مبلغ پرداختی برای این فاکتور نباید از " & MandehFaktor & " بیشتر باشد.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
            txtMablaghPFaktor.Focus()
            Exit Function
        End If
        ErrPro.SetError(Me.txtMablaghPFaktor, "")

        Return True
    End Function
    Private Sub SetGridStyle()
        Try
            'Titr
            '=========================================================================================='

            If dvForm.Count = 0 And Mode <> UD_Dll.Enums.GL_ModeForms.Search Then
                MsgBox("هیچ رکوردی یافت نشد .", MsgBoxStyle.Information + MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, "")
                Mode = UD_Dll.Enums.GL_ModeForms.AddNewRecord
                Exit Sub
            End If
            With GridEXNaghd
                .DataSource = Nothing
                .DataSource = dsForm.Tables("tblTafkikJozTasfiehSatrNaghd").DefaultView
                .SetDataBinding(dsForm.Tables("tblTafkikJozTasfiehSatrNaghd").DefaultView, "")
                .RetrieveStructure()
            End With

            For i As Integer = 0 To GridEXNaghd.CurrentTable.Columns.Count - 1
                GridEXNaghd.CurrentTable.Columns.Item(i).Visible = False
            Next

            GridEXNaghd.CurrentTable.Columns.Item("FaktorShomareh").Caption = "شماره فاکتور"
            GridEXNaghd.CurrentTable.Columns.Item("FaktorShomareh").Visible = True
            GridEXNaghd.CurrentTable.Columns.Item("FaktorShomareh").Width = 90
            GridEXNaghd.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXNaghd.CurrentTable.Columns.Item("FaktorShomareh").Position = 0
            GridEXNaghd.CurrentTable.Columns.Item("FaktorShomareh").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXNaghd.CurrentTable.Columns.Item("NameMoshtary").Caption = "نام مشتری"
            GridEXNaghd.CurrentTable.Columns.Item("NameMoshtary").Visible = True
            GridEXNaghd.CurrentTable.Columns.Item("NameMoshtary").Width = 220
            GridEXNaghd.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXNaghd.CurrentTable.Columns.Item("NameMoshtary").Position = 1
            GridEXNaghd.CurrentTable.Columns.Item("NameMoshtary").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXNaghd.CurrentTable.Columns.Item("AvarandehVajh").Caption = "آورنده وجــه"
            GridEXNaghd.CurrentTable.Columns.Item("AvarandehVajh").Visible = True
            GridEXNaghd.CurrentTable.Columns.Item("AvarandehVajh").Width = 100
            GridEXNaghd.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXNaghd.CurrentTable.Columns.Item("AvarandehVajh").Position = 2
            GridEXNaghd.CurrentTable.Columns.Item("AvarandehVajh").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXNaghd.CurrentTable.Columns.Item("SharhShomarehHesab").Caption = "نام صنــدوق"
            GridEXNaghd.CurrentTable.Columns.Item("SharhShomarehHesab").Visible = True
            GridEXNaghd.CurrentTable.Columns.Item("SharhShomarehHesab").Width = 150
            GridEXNaghd.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXNaghd.CurrentTable.Columns.Item("SharhShomarehHesab").Position = 3
            GridEXNaghd.CurrentTable.Columns.Item("SharhShomarehHesab").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXNaghd.CurrentTable.Columns.Item("ShomarehResidMovaghat").Caption = "رسید موقت"
            GridEXNaghd.CurrentTable.Columns.Item("ShomarehResidMovaghat").Visible = True
            GridEXNaghd.CurrentTable.Columns.Item("ShomarehResidMovaghat").Width = 90
            GridEXNaghd.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXNaghd.CurrentTable.Columns.Item("ShomarehResidMovaghat").Position = 4
            GridEXNaghd.CurrentTable.Columns.Item("ShomarehResidMovaghat").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXNaghd.CurrentTable.Columns.Item("TarikhDPSlash").Caption = "تاریخ دریافت"
            GridEXNaghd.CurrentTable.Columns.Item("TarikhDPSlash").Visible = True
            GridEXNaghd.CurrentTable.Columns.Item("TarikhDPSlash").Width = 110
            GridEXNaghd.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXNaghd.CurrentTable.Columns.Item("TarikhDPSlash").Position = 5
            GridEXNaghd.CurrentTable.Columns.Item("TarikhDPSlash").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXNaghd.CurrentTable.Columns.Item("MablaghKolNaghd").Caption = "مبلغ نقــد"
            GridEXNaghd.CurrentTable.Columns.Item("MablaghKolNaghd").Visible = True
            GridEXNaghd.CurrentTable.Columns.Item("MablaghKolNaghd").Width = 100
            GridEXNaghd.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXNaghd.CurrentTable.Columns.Item("MablaghKolNaghd").Position = 6
            GridEXNaghd.CurrentTable.Columns.Item("MablaghKolNaghd").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXNaghd.CurrentTable.Columns.Item("PardakhtyInFaktor").Caption = "پرداختی برای این فاکتور"
            GridEXNaghd.CurrentTable.Columns.Item("PardakhtyInFaktor").Visible = True
            GridEXNaghd.CurrentTable.Columns.Item("PardakhtyInFaktor").Width = 130
            GridEXNaghd.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXNaghd.CurrentTable.Columns.Item("PardakhtyInFaktor").Position = 7
            GridEXNaghd.CurrentTable.Columns.Item("PardakhtyInFaktor").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXNaghd.CurrentTable.Columns.Item("Sharh").Caption = "شـــرح"
            GridEXNaghd.CurrentTable.Columns.Item("Sharh").Visible = True
            GridEXNaghd.CurrentTable.Columns.Item("Sharh").Width = 100
            GridEXNaghd.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXNaghd.CurrentTable.Columns.Item("Sharh").Position = 8
            GridEXNaghd.CurrentTable.Columns.Item("Sharh").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXNaghd.CurrentTable.Columns.Item("ccTafkikJozeTasfiehSatr_Naghd").Caption = "ccTafkikJozeTasfiehSatr_Naghd"
            GridEXNaghd.CurrentTable.Columns.Item("ccTafkikJozeTasfiehSatr_Naghd").Visible = False
            GridEXNaghd.CurrentTable.Columns.Item("ccTafkikJozeTasfiehSatr_Naghd").Width = 0
            GridEXNaghd.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXNaghd.CurrentTable.Columns.Item("ccTafkikJozeTasfiehSatr_Naghd").Position = 9
            GridEXNaghd.CurrentTable.Columns.Item("ccTafkikJozeTasfiehSatr_Naghd").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXNaghd.CurrentTable.Columns.Item("ccShomarehHesab").Caption = "ccShomarehHesab"
            GridEXNaghd.CurrentTable.Columns.Item("ccShomarehHesab").Visible = False
            GridEXNaghd.CurrentTable.Columns.Item("ccShomarehHesab").Width = 0
            GridEXNaghd.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXNaghd.CurrentTable.Columns.Item("ccShomarehHesab").Position = 10
            GridEXNaghd.CurrentTable.Columns.Item("ccShomarehHesab").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXNaghd.CurrentTable.Columns.Item("TarikhDP").Caption = "TarikhDP"
            GridEXNaghd.CurrentTable.Columns.Item("TarikhDP").Visible = False
            GridEXNaghd.CurrentTable.Columns.Item("TarikhDP").Width = 0
            GridEXNaghd.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXNaghd.CurrentTable.Columns.Item("TarikhDP").Position = 11
            GridEXNaghd.CurrentTable.Columns.Item("TarikhDP").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            For i As Integer = 0 To GridEXNaghd.RootTable.Columns.Count - 1
                If GridEXNaghd.RootTable.Columns(i).Type.IsValueType Then
                    GridEXNaghd.RootTable.Columns(i).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                    GridEXNaghd.RootTable.Columns(i).FormatMode = Janus.Windows.GridEX.FormatMode.UseIFormattable
                    GridEXNaghd.RootTable.Columns(i).FormatString = "###,###.##"
                    GridEXNaghd.RootTable.Columns(i).TotalFormatMode = Janus.Windows.GridEX.FormatMode.UseIFormattable
                    GridEXNaghd.RootTable.Columns(i).TotalFormatString = "###,###.##"
                End If
            Next

            GridEXNaghd.Visible = True
            If GridEXNaghd.Height <= GridAddSize Then
                GridEXNaghd.Height = GridEXNaghd.Height + GridAddSize
            End If

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> SetGridStyle ")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> SetGridStyle ")
        End Try

    End Sub
    Private Sub GridEXNaghd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GridEXNaghd.Click
        BoundCurrencyManagerTitr()
    End Sub
    Private Sub BoundCurrencyManagerTitr()
        Try
            cmForm = CType(BindingContext(GridEXNaghd.DataSource), CurrencyManager)
            AddHandler cmForm.ItemChanged, AddressOf cmForm_ItemChanged
            AddHandler cmForm.PositionChanged, AddressOf cmForm_PositionChanged
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> BoundCurrencyManagerTitr ")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> BoundCurrencyManagerTitr ")
        End Try

    End Sub
    Private Sub cmForm_ItemChanged(ByVal sender As Object, ByVal e As ItemChangedEventArgs)
        Try

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> cmTitr_ItemChanged ")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> cmTitr_ItemChanged ")
        End Try

    End Sub
    Private Sub cmForm_PositionChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> cmTitr_PositionChanged ")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> cmTitr_PositionChanged ")
        End Try

    End Sub

    Private Sub GridEXNaghd_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GridEXNaghd.DoubleClick
        Mode = UD_Dll.Enums.GL_ModeForms.UpdateRecord
        DisableGrid()
        SetButton()
        SetFormData()
    End Sub
    Private Sub SetFormData()
        Dim drvTemp As DataRowView
        drvTemp = dvForm(cmForm.Position)

        cmbShomarehHesab.SelectedValue = Val(GridEXNaghd.CurrentRow.Cells("ccShomarehHesab").Text.Replace(",", ""))
        mskTarikhDP.Text = GridEXNaghd.CurrentRow.Cells("TarikhDP").Text
        txtResidMovaghat.Text = Val(GridEXNaghd.CurrentRow.Cells("ShomarehResidMovaghat").Text.Replace(",", ""))
        txtMablaghKol.Text = Val(GridEXNaghd.CurrentRow.Cells("MablaghKolNaghd").Text.Replace(",", ""))
        txtMablaghPFaktor.Text = Val(GridEXNaghd.CurrentRow.Cells("PardakhtyInFaktor").Text.Replace(",", ""))
        txtSharh.Text = GridEXNaghd.CurrentRow.Cells("Sharh").Text
        ccTafkikJozeTasfiehSatr_Naghd = Val(GridEXNaghd.CurrentRow.Cells("ccTafkikJozeTasfiehSatr_Naghd").Text.Replace(",", ""))

    End Sub

    Private Sub frmDP_DaryaftNaghd_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        frmFO_TafkikJozTasfieh.mskNaghdSatr.Text = objTools.ConvertNulls(objTools.DSum("PardakhtyInFaktor", "Sales.TafkikJozeTasfiehSatrNaghd", "ccFaktor = " & ccFaktorTitr), 0)
    End Sub
End Class