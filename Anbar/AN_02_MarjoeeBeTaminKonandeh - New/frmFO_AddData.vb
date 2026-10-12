Imports System.Data.SqlClient
Public Class Form1

#Region "Fields"
    Const cntCodeSubSystem As Long = 669
    Const FormTableName As String = "tblAN_KdxMarjoeeBeTaminKonandeh"

    Public TarikhResid As String
    Public CheckTarikhResid As String

    Dim dsForm As New DataSet
    Dim ErrPro As New ErrorProvider
    Dim tCodeCounter As Long
    Private SN As Integer

    ' طبق فرم قدیمی: اگر روشن باشد، مرجوعی حتما باید به یک رسید وصل باشد
    Private AllowMarjoeeWithoutResid As Boolean = False

    Private _isCalculating As Boolean = False
    Private ccKardexTitr As Integer = 0          ' مرجوعیِ در حال کار (0 = هنوز ذخیره نشده)
    Private ccResid As Integer = 0               ' رسید انتخاب‌شده (0 = بدون رسید)  
    Private dt_SearchSatr As DataTable
    Private _gridStructureReady As Boolean = False
    Public ShomarehResid1 As Integer
    Public TaminKonandeh1 As Integer
    Dim flg1 As Boolean = False

#End Region


    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            'istarikhmiladi = objTools.ConvertNulls(objTools.DLookup("IsTarikhMiladi", "tblGL_SysConfig", "CodeMahal = " & CodeMahalFaal), False)
            SN = objSec.GetSecurityNumber(cntCodeSubSystem, UserName)
            AllowMarjoeeWithoutResid = objTools.ConvertNulls(objTools.DLookup("AllowMarjoeeWithoutResid", "tblGL_SysConfig", "CodeMahal = " & CodeMahalFaal), False)
            flg1 = True
            LoadCombo()
            ClearForm()

            With GridEXSatr
                .UpdateMode = Janus.Windows.GridEX.UpdateMode.RowUpdate
                .AllowEdit = Janus.Windows.GridEX.InheritableBoolean.True
                .AllowAddNew = Janus.Windows.GridEX.InheritableBoolean.True
                .TabKeyBehavior = Janus.Windows.GridEX.TabKeyBehavior.ColumnNavigation
                .Enabled = False          ' تا قبل از ذخیره‌ی هدر
            End With

            'WireOptionalButton("btnErsal", AddressOf btnErsal_Click)
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->Load")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->Load")
        End Try
    End Sub

    Private Sub LoadCombo()
        Try
            Dim Strsql As String
            Dim daSQL As SqlDataAdapter

            ' ---------------- تامین‌کننده ----------------
            Strsql = "Select NameTaminKonandeh,ccTaminKonandeh From tblFO_TaminKonandeh where Faal=1 AND CodeTafsily1 in (select codetafsily from tblHE_CodeTafsily where codemahal = " & CodeMahalFaal & ")"
            Strsql &= " AND Not exists (Select * From tblGL_SecurityData where namekarbar= '" & UserName & "' and CodeSubSystem = 654 and pk = tblFO_TaminKonandeh.ccTaminKonandeh) order by ccTaminKonandeh"
            daSQL = New SqlDataAdapter(Strsql, ConnectionString)
            daSQL.Fill(dsForm, "tblTaminKonandeh")
            cmbTaminKonandeh.DataSource = Nothing
            cmbTaminKonandeh.Items.Clear()
            cmbTaminKonandeh.DataSource = dsForm.Tables("tblTaminKonandeh").DefaultView
            cmbTaminKonandeh.DisplayMember = "NameTaminKonandeh"
            cmbTaminKonandeh.ValueMember = "ccTaminKonandeh"

            ' ---------------- انبار ----------------
            Strsql = "Select codeAnbar,NameAnbar From qryAN_Anbar where CodeMahal=" & CodeMahalFaal & " AND Faal=1  AND "
            Strsql &= " Not exists (Select * From tblGL_SecurityData where namekarbar= '" & UserName & "' and CodeSubSystem = 633 and pk = qryAN_Anbar.CodeAnbar) order by qryAN_Anbar.Radif "
            daSQL = New SqlDataAdapter(Strsql, ConnectionString)
            daSQL.Fill(dsForm, "tblAnbar")
            cmbAnbar.DataSource = Nothing
            cmbAnbar.Items.Clear()
            cmbAnbar.DataSource = dsForm.Tables("tblAnbar").DefaultView
            cmbAnbar.DisplayMember = "NameAnbar"
            cmbAnbar.ValueMember = "codeAnbar"

            ' ---------------- ماشین حمل (اختیاری) ----------------
            Dim cmbMashin As ComboBox = TryCast(FindCtl("cmbMashinHaml"), ComboBox)
            If cmbMashin IsNot Nothing Then
                Strsql = "Select * From qryFO_Mashin Where CodeMahal = " & CodeMahalFaal & " AND sVazeiat = 4073"
                daSQL = New SqlDataAdapter(Strsql, ConnectionString)
                daSQL.Fill(dsForm, "tblMashin")
                cmbMashin.DataSource = Nothing
                cmbMashin.Items.Clear()
                cmbMashin.DataSource = dsForm.Tables("tblMashin").DefaultView
                cmbMashin.DisplayMember = "MashinFull"
                cmbMashin.ValueMember = "ccMashin"
                cmbMashin.SelectedIndex = -1
            End If

            'Strsql = "Select CodeDoreh From tblAn_KdxResid group by CodeDoreh"
            'daSQL = New SqlDataAdapter(Strsql, ConnectionString)
            'daSQL.Fill(dsForm, "tblCodeDoreh")
            'cmbsCodeDorehResid.DataSource = Nothing
            'cmbsCodeDorehResid.Items.Clear()
            'cmbsCodeDorehResid.DataSource = dsForm.Tables("tblCodeDoreh").DefaultView
            'cmbsCodeDorehResid.DisplayMember = "CodeDoreh"
            'cmbsCodeDorehResid.ValueMember = "CodeDoreh"
            'cmbsCodeDorehResid.SelectedIndex = -1


            daSQL = Nothing
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->LoadCombo")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->LoadCombo")
        End Try
    End Sub

    Private Sub ClearForm()
        Try
            cmbTaminKonandeh.SelectedIndex = -1
            txtSearchT.Text = ""

            cmbAnbar.SelectedIndex = -1
            cmbAnbar.SelectedValue = objTools.ConvertNulls(objTools.DLookup("CodeAnbar", "tblAN_Anbar", "AnbarAsly=1"), 0)

            ' مثل فرم قدیمی مرجوعی: تاریخ امروز
            mskTarikh.Text = TarikhEmrooz
            If objCode.CheckCompany = 24 Or objCode.CheckCompany = 1 Then
                mskTarikh.Enabled = False
            End If

            txtTozihat.Text = ""
            txtShomarehResid.Text = ""
            txtshomarehForm.Text = ""
            ccResid = 0

            CtlSet("cmbMashinHaml", "SelectedIndex", -1)
            CtlSet("ChkApplyMalyat", "Checked", False)
            CtlSet("txtBargashtJayezeh", "Text", "")
            CtlSet("txtBargashtTakhfif", "Text", "")

            ErrPro.Clear()
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->ClearForm")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->ClearForm")
        End Try
    End Sub


#Region "Helpers - کنترل‌های اختیاری"
    Private Function FindCtl(ByVal name As String) As Control
        Dim arr() As Control = Me.Controls.Find(name, True)
        If arr.Length > 0 Then Return arr(0)
        Return Nothing
    End Function

    Private Function CtlGet(ByVal name As String, ByVal prop As String) As Object
        Dim c As Control = FindCtl(name)
        If c Is Nothing Then Return Nothing
        Try
            Return CallByName(c, prop, CallType.Get)
        Catch
            Return Nothing
        End Try
    End Function

    Private Sub CtlSet(ByVal name As String, ByVal prop As String, ByVal value As Object)
        Dim c As Control = FindCtl(name)
        If c Is Nothing Then Exit Sub
        Try
            CallByName(c, prop, CallType.Let, value)
        Catch
        End Try
    End Sub

    Private Function CtlText(ByVal name As String) As String
        Return Convert.ToString(CtlGet(name, "Text"))
    End Function

    Private Function CtlChecked(ByVal name As String) As Boolean
        Dim v As Object = CtlGet(name, "Checked")
        If v Is Nothing OrElse IsDBNull(v) Then Return False
        Try
            Return Convert.ToBoolean(v)
        Catch
            Return False
        End Try
    End Function

    ' مقدار انتخاب‌شده‌ی یک کمبو؛ اگر نبود DBNull
    Private Function CtlSelectedOrDbNull(ByVal name As String) As Object
        Dim v As Object = CtlGet(name, "SelectedValue")
        If v Is Nothing OrElse IsDBNull(v) Then Return DBNull.Value
        Return v
    End Function

    Private Function ApplyMalyat() As Boolean
        Return CtlChecked("ChkApplyMalyat")
    End Function

    Private Sub WireOptionalButton(ByVal name As String, ByVal handler As EventHandler)
        Dim c As Control = FindCtl(name)
        If c IsNot Nothing Then AddHandler c.Click, handler
    End Sub

    Private Shared Function RGetStr(ByVal r As DataRow, ByVal col As String) As String
        If Not r.Table.Columns.Contains(col) Then Return ""
        Dim v As Object = r(col)
        If v Is Nothing OrElse IsDBNull(v) Then Return ""
        Return v.ToString()
    End Function

    Private Shared Function RGetInt(ByVal r As DataRow, ByVal col As String) As Integer
        If Not r.Table.Columns.Contains(col) Then Return 0
        Dim v As Object = r(col)
        If v Is Nothing OrElse IsDBNull(v) Then Return 0
        Return Convert.ToInt32(v)
    End Function

    Private Shared Function RGetBool(ByVal r As DataRow, ByVal col As String) As Boolean
        If Not r.Table.Columns.Contains(col) Then Return False
        Dim v As Object = r(col)
        If v Is Nothing OrElse IsDBNull(v) Then Return False
        Return Convert.ToBoolean(v)
    End Function
#End Region

    ' کنترل‌های هدر را فعال یا غیرفعال می‌کند
    Private Sub SetHeaderEnabled(ByVal en As Boolean)
        cmbTaminKonandeh.Enabled = en
        txtSearchT.Enabled = en
        cmbAnbar.Enabled = en
        mskTarikh.Enabled = en
        txtTozihat.Enabled = en
        txtShomarehResid.Enabled = en

        CtlSet("cmbMashinHaml", "Enabled", en)
        CtlSet("ChkApplyMalyat", "Enabled", en)
        CtlSet("txtBargashtJayezeh", "Enabled", en)
        CtlSet("txtBargashtTakhfif", "Enabled", en)
    End Sub

    Private Sub txtSearchT_TextChanged(sender As Object, e As EventArgs) Handles txtSearchT.TextChanged
        If dsForm.Tables.Contains("tblTaminKonandeh") Then
            If cmbTaminKonandeh.DataSource Is dsForm.Tables("tblTaminKonandeh").DefaultView Then
                Me.dsForm.Tables("tblTaminKonandeh").DefaultView.Sort = "ccTaminKonandeh"
                Me.dsForm.Tables("tblTaminKonandeh").DefaultView.RowFilter = "NameTaminKonandeh Like  '%" & txtSearchT.Text.TrimEnd & "%'"
            End If
        End If
        If txtSearchT.Text = "" Then
            cmbTaminKonandeh.SelectedIndex = -1
        End If
    End Sub
    ' ---------------- شماره‌ی رسید ----------------
    ' اطلاعات رسید را از اس‌پی می‌گیرد (با شماره‌ی رسید یا با ccKardexTitr)
    Private Sub LoadResidInfo(ByVal shomareh As Integer, ByVal id As Integer)
        Dim dt As New DataTable
        Try
            Using cn As New SqlConnection(ConnectionString)
                Using cm As SqlCommand = cn.CreateCommand()
                    cm.CommandType = CommandType.StoredProcedure
                    cm.CommandText = "[dbo].[spAN_KdxMarjoee_GetResid]"
                    cm.Parameters.AddWithValue("@CodeMahal", CodeMahalFaal)
                    cm.Parameters.AddWithValue("@CodeDoreh", CInt(CodeDoreh))
                    cm.Parameters.AddWithValue("@ShomarehForm", shomareh)
                    cm.Parameters.AddWithValue("@ccKardexTitr", id)
                    Using da As New SqlDataAdapter(cm)
                        da.Fill(dt)
                    End Using
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        If dt.Rows.Count = 0 Then
            ccResid = 0
            If shomareh > 0 OrElse id > 0 Then MessageBox.Show("رسید پیدا نشد (رسید باید نهایی‌شده باشد)")
            txtShomarehResid.Text = ""
            cmbTaminKonandeh.Enabled = True
            cmbAnbar.Enabled = True
            Exit Sub
        End If

        Dim r As DataRow = dt.Rows(0)
        ccResid = Convert.ToInt32(r("ccKardexTitr"))
        txtShomarehResid.Text = Convert.ToString(r("ShomarehForm"))
        cmbTaminKonandeh.SelectedValue = Convert.ToInt32(r("ccTaminKonandeh"))
        cmbAnbar.SelectedValue = Convert.ToInt32(r("ccAnbar"))

        ' تامین‌کننده و انبار از روی رسید می‌آیند
        cmbTaminKonandeh.Enabled = False
        cmbAnbar.Enabled = False
    End Sub



    'Private Sub txtShomarehResid_Validated(ByVal sender As Object, ByVal e As EventArgs) Handles txtShomarehResid.Validated
    '    If txtShomarehResid.Text.Trim = "" Then
    '        ccResid = 0
    '        cmbTaminKonandeh.Enabled = True
    '        cmbAnbar.Enabled = True
    '    ElseIf Val(txtShomarehResid.Text) <> 0 Then
    '        LoadResidInfo(CInt(Val(txtShomarehResid.Text)), 0)
    '    End If
    'End Sub

    ' ---------------- اعتبارسنجی هدر ----------------
    Private Function IsValidBeforSaveTitr() As Boolean
        Try
            ' تاریخ
            If Len(mskTarikh.Text.ToString) = 0 Then
                ErrPro.SetError(Me.mskTarikh, "تاريخ را وارد کنيد.")
                MsgBox("تاريخ را وارد کنيد.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
                mskTarikh.Focus()
                Return False
            End If

            'If istarikhmiladi Then
            '    If Not objTarikh.IsMiDate(mskTarikhForm.Text.ToString) Then
            '        mskTarikhForm.Focus()
            '        Return False
            '    End If
            'Else
            '    If Not objTarikh.IsShDate(mskTarikh.Text.ToString) Then
            '        mskTarikh.Focus()
            '        Return False
            '    End If
            'End If

            If mskTarikh.Text.Substring(0, 4) <> CodeDoreh Then
                MsgBox("تاريخ با دوره مالی فعال يکی نيست.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
                mskTarikh.Focus()
                Return False
            End If
            ErrPro.SetError(Me.mskTarikh, "")

            ' انبار
            If cmbAnbar.SelectedIndex = -1 Then
                ErrPro.SetError(Me.cmbAnbar, "نام انبار را انتخاب کنيد.")
                MsgBox("نام انبار را انتخاب کنيد.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
                cmbAnbar.Focus()
                Return False
            End If
            ErrPro.SetError(Me.cmbAnbar, "")

            ' رسید / تامین‌کننده (مثل فرم قدیمی)
            If AllowMarjoeeWithoutResid Then
                If ccResid = 0 Then
                    ErrPro.SetError(Me.txtShomarehResid, "شماره رسید را وارد کنید.")
                    MsgBox("شماره رسید را وارد کنید.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
                    txtShomarehResid.Focus()
                    Return False
                End If
                ErrPro.SetError(Me.txtShomarehResid, "")
            Else
                If cmbTaminKonandeh.SelectedIndex = -1 Then
                    ErrPro.SetError(Me.cmbTaminKonandeh, "نام تامین کننده را انتخاب کنيد.")
                    MsgBox("نام تامین کننده را انتخاب کنيد.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
                    cmbTaminKonandeh.Focus()
                    Return False
                End If
                ErrPro.SetError(Me.cmbTaminKonandeh, "")
            End If

            Return True
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->IsValidBeforSaveTitr")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->IsValidBeforSaveTitr")
        End Try
        Return False
    End Function
    Private Sub btnSaveSanad_Click(sender As Object, e As EventArgs) Handles btnSaveSanad.Click
        If ccKardexTitr <> 0 Then Exit Sub                 ' جلوگیری از ذخیره‌ی دوباره
        If Not objSec.HaveUserPermission(SN, UD_Dll.Security.ePermissions.Add) Then Exit Sub
        If Not AddNewRecord() Then Exit Sub

        If tCodeCounter <= 0 Then
            MessageBox.Show("ذخیره انجام نشد")
            Exit Sub
        End If

        ccKardexTitr = CInt(tCodeCounter)

        ' بعد از ثبت هدر، این‌ها نباید عوض شوند (موجودی، قیمت و بچ بر اساس آن‌ها می‌آید)
        SetHeaderEnabled(False)

        SearchSatr()
        btnSaveSanad.Enabled = False

        GridEXSatr.Enabled = True
        GridEXSatr.Focus()
        GridEXSatr.MoveToNewRecord()
        GridEXSatr.Col = 0
    End Sub

    Private Function AddNewRecord() As Boolean
        AddNewRecord = False
        Try
            If Not IsValidBeforSaveTitr() Then Exit Function

            Dim shomarehForm As Integer = 0

            Using cn As New SqlConnection(ConnectionString)
                Using cm As SqlCommand = cn.CreateCommand()
                    cn.Open()
                    cm.CommandType = CommandType.StoredProcedure
                    cm.CommandText = "[dbo].[spAN_KdxMarjoee_Insert]"
                    cm.Parameters.AddWithValue("@CodeMahal", CodeMahalFaal)
                    cm.Parameters.AddWithValue("@CodeDoreh", CodeDoreh)
                    cm.Parameters.AddWithValue("@ccAnbar", If(IsNothing(cmbAnbar.SelectedValue), 0, cmbAnbar.SelectedValue))
                    cm.Parameters.AddWithValue("@TarikhForm", mskTarikh.Text)
                    cm.Parameters.AddWithValue("@ccTaminKonandeh", If(IsNothing(cmbTaminKonandeh.SelectedValue), 0, cmbTaminKonandeh.SelectedValue))
                    cm.Parameters.AddWithValue("@sVazeiat", CInt(UD_Dll.Enums.AN_VazeiatKDXMarjoeeBeTaminKonandeh.BedoneAmalyat))
                    cm.Parameters.AddWithValue("@ccMashin", CtlSelectedOrDbNull("cmbMashinHaml"))
                    cm.Parameters.AddWithValue("@Tozihat", txtTozihat.Text)
                    cm.Parameters.AddWithValue("@ShomarehResid", CInt(Val(txtShomarehResid.Text)))
                    cm.Parameters.AddWithValue("@ccKardexTitrR", ccResid)
                    cm.Parameters.AddWithValue("@Malyat", ApplyMalyat())
                    cm.Parameters.AddWithValue("@MablaghBargashtJayezeh", Val(CtlText("txtBargashtJayezeh")))
                    cm.Parameters.AddWithValue("@MablaghBargashtTakhfif", Val(CtlText("txtBargashtTakhfif")))
                    cm.Parameters.AddWithValue("@UserName", UserName)

                    Dim dt As New DataTable
                    Using da As New SqlDataAdapter(cm)
                        da.Fill(dt)
                    End Using

                    If dt.Rows.Count > 0 Then
                        tCodeCounter = Convert.ToInt32(dt.Rows(0)("tCodeCounter"))
                        shomarehForm = Convert.ToInt32(dt.Rows(0)("ShomarehForm"))
                        txtshomarehForm.Text = shomarehForm.ToString()
                    End If
                End Using
            End Using

            objCode.SabteTaghirat(CodeMahalFaal, UD_Dll.Enums.GL_NoeTaghir.AddNewRecord, FormTableName, tCodeCounter, shomarehForm, "ذخیره")
            Return True

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->AddNewRecord")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->AddNewRecord")
        End Try
    End Function

    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        NewMarjoee()
    End Sub

    Private Sub NewMarjoee()
        ' ۱) ریست وضعیت
        ccKardexTitr = 0
        tCodeCounter = 0
        ccResid = 0
        _gridStructureReady = False      ' چون DataSource خالی می‌شود، ساختار باید دوباره ساخته شود

        ' ۲) هدر: فعال و خالی (ClearForm قاعده‌ی غیرفعال بودن تاریخ در بعضی شرکت‌ها را خودش اعمال می‌کند)
        SetHeaderEnabled(True)
        ClearForm()
        txtSearchNumber.Text = ""
        btnSaveSanad.Enabled = True

        ' ۳) گرید: خالی و غیرفعال
        dt_SearchSatr = New DataTable
        GridEXSatr.SetDataBinding(Nothing, "")
        GridEXSatr.Enabled = False

        ' ۴) فوکوس
        cmbTaminKonandeh.Focus()
    End Sub

    ' ---------------- جستجوی مرجوعی (فاصله = لیست) ----------------
    Private Sub txtSearchNumber_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles txtSearchNumber.KeyDown
        If e.KeyCode = Keys.Space Then
            e.Handled = True
            e.SuppressKeyPress = True

            ' فقط مرجوعی‌های بدون عملیات (قابل ویرایش)
            Dim frm As New frmMarjoeeSearch(ConnectionString, CodeMahalFaal, CInt(CodeDoreh),
                                            CInt(UD_Dll.Enums.AN_VazeiatKDXMarjoeeBeTaminKonandeh.BedoneAmalyat))
            If frm.ShowDialog() = DialogResult.OK AndAlso frm.SelectedId > 0 Then
                ShowMarjoee(frm.SelectedId)
            End If

        ElseIf e.KeyCode <> Keys.Tab AndAlso e.KeyCode <> Keys.ShiftKey Then
            e.Handled = True
            e.SuppressKeyPress = True
        End If
    End Sub
    Private Sub txtShomarehResid_KeyPress(ByVal sender As Object, ByVal e As KeyPressEventArgs) Handles txtShomarehResid.KeyPress
        Try
            If e.KeyChar = Chr(Keys.Space) Then
                e.Handled = True
                ' دوره = 0 یعنی رسیدهای همه‌ی دوره‌ها (مثل کد قدیمی که فیلتر دوره را کامنت کرده بودی)
                Dim frm As New frm_ResidSearch()
                If frm.ShowDialog() = DialogResult.OK AndAlso frm.SelectedId > 0 Then
                    LoadResidInfo(0, frm.SelectedId)
                End If

            ElseIf (Asc(e.KeyChar()) < 48 Or Asc(e.KeyChar()) > 57) And (Asc(e.KeyChar()) <> 8) Then
                e.Handled = True
            End If
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->txtShomarehResid_KeyPress")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->txtShomarehResid_KeyPress")
        End Try
    End Sub

    ' اگر شماره‌ی رسید را تایپ کرد (بدون فاصله)، با دوره‌ی فعلی پیدا می‌شود
    Private Sub txtShomarehResid_Validated(ByVal sender As Object, ByVal e As EventArgs) Handles txtShomarehResid.Validated
        If txtShomarehResid.Text.Trim = "" Then
            ccResid = 0
            cmbTaminKonandeh.Enabled = True
            cmbAnbar.Enabled = True
        ElseIf Val(txtShomarehResid.Text) <> 0 AndAlso ccResid = 0 Then
            LoadResidInfo(CInt(Val(txtShomarehResid.Text)), 0)
        End If
    End Sub


    Private Sub txtSearchNumber_KeyPress(ByVal sender As Object, ByVal e As KeyPressEventArgs) Handles txtSearchNumber.KeyPress
        e.Handled = True
    End Sub

    ' مرجوعی انتخاب‌شده را در فرم نشان می‌دهد و گرید را برای ادامه‌ی کار فعال می‌کند
    Private Sub ShowMarjoee(ByVal id As Integer)
        Dim dt As New DataTable
        Try
            Using cn As New SqlConnection(ConnectionString)
                Using cm As SqlCommand = cn.CreateCommand()
                    cm.CommandType = CommandType.StoredProcedure
                    cm.CommandText = "[dbo].[spAN_KdxMarjoee_LoadTitr]"
                    cm.Parameters.AddWithValue("@ccKardexTitr", id)
                    Using da As New SqlDataAdapter(cm)
                        da.Fill(dt)
                    End Using
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        If dt.Rows.Count = 0 Then
            MessageBox.Show("اطلاعات مرجوعی پیدا نشد")
            Exit Sub
        End If

        Dim r As DataRow = dt.Rows(0)

        NewMarjoee()                          ' اول همه‌چیز را تمیز کن

        ccKardexTitr = id
        txtSearchNumber.Text = RGetStr(r, "ShomarehForm")
        txtshomarehForm.Text = RGetStr(r, "ShomarehForm")

        mskTarikh.Text = RGetStr(r, "TarikhForm")
        cmbTaminKonandeh.SelectedValue = RGetInt(r, "ccTaminKonandeh")
        cmbAnbar.SelectedValue = RGetInt(r, "ccAnbar")
        txtTozihat.Text = RGetStr(r, "Tozihat")

        ccResid = RGetInt(r, "ccKardexTitrR")
        If RGetInt(r, "ShomarehResid") > 0 Then txtShomarehResid.Text = RGetStr(r, "ShomarehResid")

        CtlSet("cmbMashinHaml", "SelectedValue", RGetInt(r, "ccMashin"))
        CtlSet("ChkApplyMalyat", "Checked", RGetBool(r, "Malyat"))
        CtlSet("txtBargashtJayezeh", "Text", RGetStr(r, "MablaghBargashtJayezeh"))
        CtlSet("txtBargashtTakhfif", "Text", RGetStr(r, "MablaghBargashtTakhfif"))

        ' هدر ثبت‌شده است: قفل و ذخیره‌ی هدر غیرفعال
        SetHeaderEnabled(False)
        btnSaveSanad.Enabled = False

        SearchSatr()
        GridEXSatr.Enabled = True
        GridEXSatr.Focus()
        GridEXSatr.MoveToNewRecord()
        GridEXSatr.Col = 0
    End Sub

    ' ---------------- ارسال (مثل btnErsal فرم قدیمی) ----------------
    Private Sub btnErsal_Click(ByVal sender As Object, ByVal e As EventArgs)
        If ccKardexTitr = 0 Then
            MessageBox.Show("ابتدا مرجوعی را ذخیره یا انتخاب کنید")
            Exit Sub
        End If
        If Not objSec.HaveUserPermission(SN, UD_Dll.Security.ePermissions.Export) Then Exit Sub

        If MsgBox("آیا مرجوعی ارسال شود؟ بعد از ارسال امکان ویرایش ندارید.",
                  MsgBoxStyle.YesNo + MsgBoxStyle.Question + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.DefaultButton2,
                  "ارسال") <> MsgBoxResult.Yes Then Exit Sub

        Try
            objCode.SabteTaghirat(CodeMahalFaal, UD_Dll.Enums.GL_NoeTaghir.UpdateRecord, FormTableName, ccKardexTitr, Val(txtshomarehForm.Text), "بروز رساني ")

            ' مالیات و عوارض آخرین بار حساب شود
            RollBackMalyatAvarez(ccKardexTitr, ApplyMalyat(), 0)

            Using con As New SqlConnection(ConnectionString)
                Using cmd As SqlCommand = con.CreateCommand()
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.CommandText = "[dbo].[spAN_KdxMarjoee_Ersal]"
                    cmd.Parameters.AddWithValue("@ccKardexTitr", ccKardexTitr)
                    con.Open()
                    cmd.ExecuteNonQuery()
                End Using
            End Using

            MsgBox("عمليــات با موفقیت انجــام شـــد .", MsgBoxStyle.MsgBoxRtlReading Or MsgBoxStyle.MsgBoxRight Or MsgBoxStyle.Information, "ذخيره")
            NewMarjoee()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub RollBackMalyatAvarez(ByVal ccKardexTitr As Double, ByVal ApplyOn As Boolean, ByVal Taeed As Integer)
        Dim strSQL As String = ""
        Dim cn As New SqlConnection(ConnectionString)
        Dim da As SqlDataAdapter = Nothing
        Dim dt As New DataTable
        Dim cm As SqlCommand = Nothing
        Dim Malyat As Double = 0
        Dim Avarez As Double = 0

        cn.Open()
        Try

            If ApplyOn = True Then


                strSQL = " UPDATE tblAN_KdxMarjoeeBeTaminKonandehSatr SET "
                strSQL &= " MablaghMalyat = " & 0
                strSQL &= " ,MablaghAvarez = " & 0
                strSQL &= " WHERE ccKardexTitr = " & ccKardexTitr

                cm = New SqlCommand(strSQL, cn)
                cm.ExecuteNonQuery()

                strSQL = "SELECT  MKOL,ccKardexSatr FROM tblAN_KdxMarjoeeBeTaminKonandehSatr WHERE ccKardexTitr = " & ccKardexTitr & " AND IsJayezeh = 0 AND "
                If Taeed = 0 Then
                    strSQL &= " FlagMalyatAvarez = 1 and "
                End If

                strSQL &= "ccKala In (Select ccKala From tblAN_Kala where MashmuleAvarez = 1 AND MashmuleMaliyat = 1)"


                da = New SqlDataAdapter(strSQL, cn)

                da.Fill(dt)
                For Each dr As DataRow In dt.Rows




                    Malyat = Math.Round(objCode.GetMablaghMalyat(dr("MKOL")), 0)
                    Avarez = Math.Round(objCode.GetMablaghAvarez(dr("MKOL")), 0)


                    strSQL = "UPDATE tblAN_KdxMarjoeeBeTaminKonandehSatr SET "
                    strSQL &= "MablaghMalyat=" & Malyat
                    If Taeed = 1 Then
                        strSQL &= " ,FlagMalyatAvarez = " & 1
                    End If
                    strSQL &= " WHERE ccKardexSatr = " & dr("ccKardexSatr")

                    cm = New SqlCommand(strSQL, cn)
                    cm.ExecuteNonQuery()

                    strSQL = "UPDATE tblAN_KdxMarjoeeBeTaminKonandehSatr SET "
                    strSQL &= "MablaghAvarez=" & Avarez
                    If Taeed = 1 Then
                        strSQL &= " ,FlagMalyatAvarez = " & 1
                    End If
                    strSQL &= " WHERE ccKardexSatr = " & dr("ccKardexSatr")

                    cm = New SqlCommand(strSQL, cn)
                    cm.ExecuteNonQuery()

                Next


            Else
                strSQL = "UPDATE tblAN_KdxMarjoeeBeTaminKonandehSatr SET "
                strSQL &= "MablaghAvarez=" & 0
                strSQL &= ",MablaghMalyat=" & 0
                strSQL &= " ,FlagMalyatAvarez = " & 0
                strSQL &= " WHERE ccKardexTitr = " & ccKardexTitr

                cm = New SqlCommand(strSQL, cn)
                cm.ExecuteNonQuery()

            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

#Region "Grid - سطرها"

    ' ---------- Enter = حرکت ستون به ستون ----------
    Private Sub GoNextCol()
        Dim g As Janus.Windows.GridEX.GridEX = GridEXSatr
        If g.CurrentRow Is Nothing Then Exit Sub

        Dim isNewRow As Boolean = (g.CurrentRow.RowType = Janus.Windows.GridEX.RowType.NewRecord)

        Dim cols As New List(Of Janus.Windows.GridEX.GridEXColumn)

        For Each c As Janus.Windows.GridEX.GridEXColumn In g.RootTable.Columns
            If c.Visible AndAlso c.EditType <> Janus.Windows.GridEX.EditType.NoEdit _
               AndAlso Not IsCellLocked(g.CurrentRow, c.Key) Then cols.Add(c)
            'For Each c As Janus.Windows.GridEX.GridEXColumn In g.RootTable.Columns
            '    If c.Visible AndAlso c.EditType <> Janus.Windows.GridEX.EditType.NoEdit Then
            '        If isNewRow OrElse c.Key <> "CodeKala" Then cols.Add(c)
            '    End If
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

    ' ---------- بارگذاری ----------
    Private Sub SearchSatr()
        Dim dt As New DataTable
        Using cn As New SqlConnection(ConnectionString)
            Using cm As SqlCommand = cn.CreateCommand()
                cm.CommandType = CommandType.StoredProcedure
                cm.CommandText = "[dbo].[spAN_KdxMarjoee_SearchSatr]"
                cm.Parameters.AddWithValue("@ccKardexTitr", ccKardexTitr)
                Using da As New SqlDataAdapter(cm)
                    da.Fill(dt)
                End Using
            End Using
        End Using

        If dt.Columns.Contains("IsJayezeh") AndAlso dt.Columns("IsJayezeh").DataType IsNot GetType(Boolean) Then
            Dim nc As New DataColumn("IsJayezehB", GetType(Boolean))
            dt.Columns.Add(nc)
            For Each r As DataRow In dt.Rows
                r("IsJayezehB") = Not IsDBNull(r("IsJayezeh")) AndAlso Convert.ToInt32(r("IsJayezeh")) <> 0
            Next
            dt.Columns.Remove("IsJayezeh")
            nc.ColumnName = "IsJayezeh"
        End If

        For Each col As DataColumn In dt.Columns
            col.ReadOnly = False
        Next

        If Not dt.Columns.Contains("MablaghKol") Then
            dt.Columns.Add("MablaghKol", GetType(Double))

            If dt.Columns.Contains("MKOL") Then
                For Each r As DataRow In dt.Rows
                    r("MablaghKol") = r("MKOL")
                Next
            End If
        End If
        dt.AcceptChanges()
        dt.DefaultView.AllowEdit = True
        dt_SearchSatr = dt

        SetGridSatr()
    End Sub

    Private Sub SetCol(ByVal key As String, ByVal caption As String, ByVal width As Integer,
                       ByVal pos As Integer, ByVal editable As Boolean,
                       Optional ByVal fmt As String = "")
        Dim c As Janus.Windows.GridEX.GridEXColumn = GridEXSatr.RootTable.Columns(key)
        c.Caption = caption
        c.Visible = True
        c.Width = width
        c.Position = pos
        c.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
        c.EditType = If(editable, Janus.Windows.GridEX.EditType.TextBox, Janus.Windows.GridEX.EditType.NoEdit)
        If fmt <> "" Then c.FormatString = fmt
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
                .AllowEdit = Janus.Windows.GridEX.InheritableBoolean.True
                .AllowAddNew = Janus.Windows.GridEX.InheritableBoolean.True
                .AllowDelete = Janus.Windows.GridEX.InheritableBoolean.True
                .TabKeyBehavior = Janus.Windows.GridEX.TabKeyBehavior.ColumnNavigation
            End With

            For Each c As Janus.Windows.GridEX.GridEXColumn In GridEXSatr.RootTable.Columns
                c.Visible = False
            Next

            SetCol("Radif", "ردیف", 40, 1, False)
            SetCol("CodeKala", "کد کالا", 120, 2, True)
            SetCol("NameKala", "نام کالا", 300, 3, False)
            SetCol("ShomarehBatch", "شماره بچ", 100, 4, True)
            SetCol("Tedad3", "تعداد", 200, 5, True, "###,###.##")
            SetCol("IsJayezeh", "جایزه", 60, 6, True)
            With GridEXSatr.RootTable.Columns("IsJayezeh")
                .ColumnType = Janus.Windows.GridEX.ColumnType.CheckBox
                .EditType = Janus.Windows.GridEX.EditType.CheckBox
            End With
            SetCol("MablaghKharid", "مبلغ خرید", 200, 7, True)
            SetCol("MablaghKol", "مبلغ کل", 200, 8, False, "###,###")
            SetCol("TarikhTolid", "تاریخ تولید", 90, 9, True)
            SetCol("TarikhEngheza", "تاریخ انقضا", 90, 10, True)
            SetCol("IRC", "IRC", 110, 11, False)
            SetCol("GTIN", "GTIN", 110, 12, False)

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

    ' ---------- ذخیره ----------
    Private Function TryGetNumber(ByVal row As Janus.Windows.GridEX.GridEXRow,
                                  ByVal colKey As String, ByRef result As Double) As Boolean
        result = 0
        Dim v As Object = row.Cells(colKey).Value
        If v Is Nothing OrElse IsDBNull(v) Then Return False
        If v.ToString().Trim().Length = 0 Then Return False
        Return Double.TryParse(v.ToString().Trim(), result)
    End Function

    Private Sub FocusCol(ByVal colKey As String)
        GridEXSatr.Col = GridEXSatr.RootTable.Columns(colKey).Position
    End Sub

    Private Function ToDbStr(v As Object) As Object
        If v Is Nothing OrElse IsDBNull(v) OrElse v.ToString().Trim() = "" Then Return DBNull.Value
        Return v.ToString().Trim()
    End Function

    Private Function IsRowEmpty(row As Janus.Windows.GridEX.GridEXRow) As Boolean
        For Each key As String In New String() {"CodeKala", "ccKala", "Tedad3"}
            Dim v As Object = row.Cells(key).Value
            If v IsNot Nothing AndAlso Not IsDBNull(v) Then
                If Convert.ToString(v).Trim().Length > 0 Then Return False
            End If
        Next
        Return True
    End Function

    Private Function IsBonusRow(ByVal row As Janus.Windows.GridEX.GridEXRow) As Boolean
        Try
            Dim v As Object = row.Cells("IsJayezeh").Value
            If v Is Nothing OrElse IsDBNull(v) Then Return False
            Return Convert.ToBoolean(v)
        Catch
            Return False
        End Try
    End Function

    ' تاریخ شمسی/میلادی را به میلادی ۸ رقمی تبدیل می‌کند (مثل فرم رسید)
    Private Function CallSh2Mi(ByVal s As String) As String
        Try
            Dim r As Object = objTarikh.Sh2Mi(s)
            If r Is Nothing Then Return ""
            If TypeOf r Is DateTime Then
                Return CDate(r).ToString("yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture)
            End If
            Return Convert.ToString(r).Replace("/", "").Replace("-", "").Trim()
        Catch
            Return ""
        End Try
    End Function

    Private Function IsValidMiladi8(ByVal s As String) As Boolean
        If s Is Nothing Then Return False
        If Not System.Text.RegularExpressions.Regex.IsMatch(s, "^\d{8}$") Then Return False
        Dim dt As DateTime
        If Not DateTime.TryParseExact(s, "yyyyMMdd",
                System.Globalization.CultureInfo.InvariantCulture,
                System.Globalization.DateTimeStyles.None, dt) Then Return False
        Return dt.Year >= 2000
    End Function

    Private Function TryToMiladi(ByVal v As Object, ByRef result As String) As Boolean
        result = ""
        If v Is Nothing OrElse IsDBNull(v) Then Return False
        Dim digits As String = v.ToString().Trim().Replace("/", "").Replace("-", "").Replace(" ", "")
        If Not System.Text.RegularExpressions.Regex.IsMatch(digits, "^\d{8}$") Then Return False

        Dim res As String
        If CInt(digits.Substring(0, 4)) < 2000 Then
            res = CallSh2Mi(digits)
            If Not IsValidMiladi8(res) Then
                res = CallSh2Mi(digits.Substring(0, 4) & "/" & digits.Substring(4, 2) & "/" & digits.Substring(6, 2))
            End If
        Else
            res = digits
        End If
        If Not IsValidMiladi8(res) Then Return False
        result = res
        Return True
    End Function

    Private Function ToMiladi(v As Object) As Object
        Dim r As String = ""
        If TryToMiladi(v, r) Then Return r
        Return DBNull.Value
    End Function

    Private Function ValidateRow(ByVal row As Janus.Windows.GridEX.GridEXRow) As Boolean
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

        Dim sb As Object = row.Cells("ShomarehBatch").Value
        If sb Is Nothing OrElse IsDBNull(sb) OrElse sb.ToString().Trim() = "" Then
            MessageBox.Show("شماره بچ را وارد کنید")
            FocusCol("ShomarehBatch")
            Return False
        End If

        ' جایزه مبلغ ندارد؛ بقیه باید مبلغ داشته باشند
        If Not IsBonusRow(row) Then
            If Not TryGetNumber(row, "MablaghKharid", num) OrElse num <= 0 Then
                MessageBox.Show("مبلغ خرید را وارد کنید")
                FocusCol("MablaghKharid")
                Return False
            End If
        End If

        Dim tolid As String = "", engheza As String = ""
        If Not TryToMiladi(row.Cells("TarikhTolid").Value, tolid) Then
            MessageBox.Show("تاریخ تولید خالی یا نامعتبر است")
            FocusCol("TarikhTolid")
            Return False
        End If
        If Not TryToMiladi(row.Cells("TarikhEngheza").Value, engheza) Then
            MessageBox.Show("تاریخ انقضا خالی یا نامعتبر است")
            FocusCol("TarikhEngheza")
            Return False
        End If






        Return True
    End Function

    Private Function SaveRow(row As Janus.Windows.GridEX.GridEXRow, isNew As Boolean) As Boolean
        If row Is Nothing Then Return False
        If isNew AndAlso IsRowEmpty(row) Then Return False
        If Not ValidateRow(row) Then Return False
        If Not IsValidTedad(row, isNew) Then Return False

        Try
            Dim tedad3, price As Double
            TryGetNumber(row, "Tedad3", tedad3)
            TryGetNumber(row, "MablaghKharid", price)

            Using con As New SqlConnection(ConnectionString)
                Using cmd As SqlCommand = con.CreateCommand()
                    cmd.CommandType = CommandType.StoredProcedure

                    If isNew Then
                        cmd.CommandText = "[dbo].[spAN_KdxMarjoee_InsertSatr]"
                        cmd.Parameters.AddWithValue("@ccKardexTitr", ccKardexTitr)
                        cmd.Parameters.AddWithValue("@ccKala", Convert.ToInt32(row.Cells("ccKala").Value))
                        cmd.Parameters.AddWithValue("@Tedad3", tedad3)
                        cmd.Parameters.AddWithValue("@IsJayezeh", IsBonusRow(row))
                        cmd.Parameters.AddWithValue("@MablaghKharid", price)
                        cmd.Parameters.AddWithValue("@MablaghMiangin", 0)
                        cmd.Parameters.AddWithValue("@ShomarehBatch", Convert.ToString(row.Cells("ShomarehBatch").Value).Trim())
                        cmd.Parameters.AddWithValue("@TarikhTolid", ToMiladi(row.Cells("TarikhTolid").Value))
                        cmd.Parameters.AddWithValue("@TarikhEngheza", ToMiladi(row.Cells("TarikhEngheza").Value))
                        cmd.Parameters.AddWithValue("@UserName", UserName)
                        Dim outId As New SqlParameter("@ccKardexSatr", SqlDbType.Int) With {.Direction = ParameterDirection.Output}
                        cmd.Parameters.Add(outId)
                    Else
                        cmd.CommandText = "[dbo].[spAN_KdxMarjoee_UpdateSatr]"
                        cmd.Parameters.AddWithValue("@ccKardexSatr", row.Cells("ccKardexSatr").Value)
                        cmd.Parameters.AddWithValue("@Tedad3", tedad3)
                        cmd.Parameters.AddWithValue("@MablaghKharid", price)
                        cmd.Parameters.AddWithValue("@UserName", UserName)
                    End If

                    con.Open()
                    cmd.ExecuteNonQuery()
                End Using
            End Using
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
        GridEXSatr.MoveToNewRecord()
        GridEXSatr.Col = 0
        GridEXSatr.Focus()
    End Sub

    Private Sub GridEXSatr_EditingCell(sender As Object, e As Janus.Windows.GridEX.EditingCellEventArgs) Handles GridEXSatr.EditingCell
        'Dim row = GridEXSatr.CurrentRow
        'If row Is Nothing Then Exit Sub
        'If row.RowType = Janus.Windows.GridEX.RowType.Record Then
        '    ' در سطر ذخیره‌شده فقط تعداد و مبلغ قابل ویرایش است
        '    Select Case e.Column.Key
        '        Case "Tedad3", "MablaghKharid"
        '            If e.Column.Key = "MablaghKharid" AndAlso IsBonusRow(row) Then e.Cancel = True
        '        Case Else
        '            e.Cancel = True
        '    End Select
        'End If
        Dim row = GridEXSatr.CurrentRow
        If row Is Nothing OrElse e.Column Is Nothing Then Exit Sub
        If IsCellLocked(row, e.Column.Key) Then e.Cancel = True
    End Sub

    ' ---------- محاسبه‌ی مبلغ کل ----------
    Private Sub CalcRow(ByVal row As Janus.Windows.GridEX.GridEXRow)
        If row Is Nothing Then Exit Sub
        Dim tedad, fee As Double
        TryGetNumber(row, "Tedad3", tedad)
        TryGetNumber(row, "MablaghKharid", fee)
        _isCalculating = True
        Try
            row.Cells("MablaghKol").Value = If(IsBonusRow(row), 0, tedad * fee)
        Finally
            _isCalculating = False
        End Try
    End Sub

    Private Sub GridEXSatr_CellUpdated(ByVal sender As Object, ByVal e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles GridEXSatr.CellUpdated
        If _isCalculating Then Exit Sub
        If e.Column Is Nothing OrElse GridEXSatr.CurrentRow Is Nothing Then Exit Sub
        Select Case e.Column.Key
            Case "CodeKala"
                If GridEXSatr.CurrentRow.RowType = Janus.Windows.GridEX.RowType.NewRecord Then FillKalaFromCode(GridEXSatr.CurrentRow)
            Case "Tedad3", "MablaghKharid", "IsJayezeh"
                CalcRow(GridEXSatr.CurrentRow)
        End Select
    End Sub

    ' ---------- کالا و بچ ----------
    Private Sub FillKalaRow(ByVal row As Janus.Windows.GridEX.GridEXRow, ByVal ccKala As Integer,
                            ByVal codeKala As Object, ByVal nameKala As Object)
        _isCalculating = True
        Try
            row.Cells("CodeKala").Value = codeKala
            row.Cells("ccKala").Value = If(ccKala > 0, CObj(ccKala), DBNull.Value)
            row.Cells("NameKala").Value = nameKala
            row.Cells("MablaghKharid").Value = 0
            row.Cells("ShomarehBatch").Value = DBNull.Value
            row.Cells("TarikhTolid").Value = DBNull.Value
            row.Cells("TarikhEngheza").Value = DBNull.Value
            row.Cells("IsJayezeh").Value = False
        Finally
            _isCalculating = False
        End Try
        CalcRow(row)
    End Sub

    Private Sub FillKalaFromCode(ByVal row As Janus.Windows.GridEX.GridEXRow)
        Dim code As String = Convert.ToString(row.Cells("CodeKala").Value).Trim()
        Dim codeNum As Long
        If code = "" OrElse Not Long.TryParse(code, codeNum) Then
            FillKalaRow(row, 0, code, DBNull.Value)
            Exit Sub
        End If

        Dim ccKala As Integer = 0
        Dim nameKala As Object = DBNull.Value
        Using cn As New SqlConnection(ConnectionString)
            Dim sql As String = "SELECT TOP 1 ccKala, NameKala FROM qryAN_Kala WHERE Faal = 1 AND CodeKala = @c"
            If ccResid > 0 Then sql &= " AND ccKala IN (SELECT ccKala FROM tblAN_KdxResidSatr WHERE ccKardexTitr = " & ccResid & ")"
            Using cm As New SqlCommand(sql, cn)
                cm.Parameters.AddWithValue("@c", codeNum)
                cn.Open()
                Using rd As SqlDataReader = cm.ExecuteReader()
                    If rd.Read() Then
                        ccKala = Convert.ToInt32(rd("ccKala"))
                        nameKala = rd("NameKala")
                    End If
                End Using
            End Using
        End Using
        FillKalaRow(row, ccKala, code, nameKala)
    End Sub

    ' قیمت بر اساس بچ (از رسید اگر وصل است، وگرنه از لیست قیمت)
    Private Function GetPriceForBach(ByVal ccKala As Integer, ByVal bach As String) As Double
        Using cn As New SqlConnection(ConnectionString)
            cn.Open()
            If ccResid > 0 Then
                Using cm As New SqlCommand("SELECT TOP 1 MablaghKharid FROM tblAN_KdxResidSatr WHERE ccKardexTitr = @r AND ccKala = @k AND ISNULL(IsJayezeh,0) = 0 AND REPLACE(LTRIM(RTRIM(ISNULL(ShomarehBach,''))),' ','') = @b", cn)
                    cm.Parameters.AddWithValue("@r", ccResid)
                    cm.Parameters.AddWithValue("@k", ccKala)
                    cm.Parameters.AddWithValue("@b", bach.Replace(" ", ""))
                    Dim r As Object = cm.ExecuteScalar()
                    If r IsNot Nothing AndAlso Not IsDBNull(r) Then Return Convert.ToDouble(r)
                End Using
            End If
            Using cm As New SqlCommand("SELECT dbo.GetMablaghKharid_Bach(@ccKala,@Tarikh,@CodeMahal,@ccTamin,@Bach)", cn)
                cm.Parameters.AddWithValue("@ccKala", ccKala)
                cm.Parameters.AddWithValue("@Tarikh", mskTarikh.Text.Replace("/", ""))
                cm.Parameters.AddWithValue("@CodeMahal", CodeMahalFaal)
                cm.Parameters.AddWithValue("@ccTamin", If(IsNothing(cmbTaminKonandeh.SelectedValue), 0, cmbTaminKonandeh.SelectedValue))
                cm.Parameters.AddWithValue("@Bach", bach)
                Dim r As Object = cm.ExecuteScalar()
                If r Is Nothing OrElse IsDBNull(r) Then Return 0
                Return Convert.ToDouble(r)
            End Using
        End Using
    End Function

    Private Sub GridEXSatr_KeyDown(sender As Object, e As KeyEventArgs) Handles GridEXSatr.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.Handled = True
            e.SuppressKeyPress = True
            GoNextCol()
            Exit Sub
        End If

        If e.KeyCode <> Keys.F2 AndAlso e.KeyCode <> Keys.F5 Then Exit Sub
        If GridEXSatr.CurrentRow Is Nothing OrElse GridEXSatr.CurrentColumn Is Nothing Then Exit Sub
        If GridEXSatr.CurrentRow.RowType <> Janus.Windows.GridEX.RowType.NewRecord Then Exit Sub

        ' ---------- F2 : کالا ----------
        'If e.KeyCode = Keys.F2 AndAlso GridEXSatr.CurrentColumn.Key = "CodeKala" Then
        '    Dim objKala As New Forms_dll.frmAN_KalaSearch
        '    Dim StrSqlKala As String = "Select CodeKala,NameKala,ccKala,txtsVahedeShomaresh,sVahedeShomaresh,NameBrand,Radif from qryAN_Kala Where Faal = 1"
        '    If ccResid > 0 Then
        '        StrSqlKala &= " and ccKala in (select ccKala from tblAN_KdxResidSatr where ccKardexTitr = " & ccResid & ")"
        '    End If
        '    MultiSelection = False
        '    SearchItem = "CodeKala"
        '    objKala.SetForm(StrSqlKala)
        '    objKala.ShowDialog()
        '    If Val(objKala.tccKala) = 0 Then Exit Sub
        '    FillKalaRow(GridEXSatr.CurrentRow, CInt(objKala.tccKala), objKala.tcodeKala, objKala.tNameKala)
        '    e.Handled = True
        '    Exit Sub
        'End If
        ' ---------- F2 : کالا ----------
        If e.KeyCode = Keys.F2 AndAlso GridEXSatr.CurrentColumn.Key = "CodeKala" Then

            If ccResid > 0 Then
                ' مرجوعی با رسید: کالا + بچ از روی رسید
                Dim f As New frmAN_KalaSearch_Resid(ConnectionString, ccResid)
                If f.ShowDialog() <> DialogResult.OK OrElse f.SelectedCcKala = 0 Then Exit Sub

                FillKalaRow(GridEXSatr.CurrentRow, f.SelectedCcKala, f.SelectedCodeKala, f.SelectedNameKala)
                ApplyResidPick(GridEXSatr.CurrentRow, f)
                FocusCol("Tedad3")

                _isCalculating = True
                Try
                    With GridEXSatr.CurrentRow
                        .Cells("ShomarehBatch").Value = f.SelectedBach
                        .Cells("TarikhTolid").Value = f.SelectedTarikhTolid
                        .Cells("TarikhEngheza").Value = f.SelectedTarikhEngheza
                        .Cells("MablaghKharid").Value = f.SelectedMablaghKharid
                        .Cells("Tedad3").Value = f.SelectedTedadMandeh
                    End With
                Finally
                    _isCalculating = False
                End Try
                'CalcRow(GridEXSatr.CurrentRow)
                'FocusCol("Tedad3")
                FillKalaRow(GridEXSatr.CurrentRow, f.SelectedCcKala, f.SelectedCodeKala, f.SelectedNameKala)
                ApplyResidPick(GridEXSatr.CurrentRow, f)
                FocusCol("Tedad3")
            Else
                ' مرجوعی بدون رسید: جستجوی عادی کالا
                Dim objKala As New Forms_dll.frmAN_KalaSearch
                Dim StrSqlKala As String = "Select CodeKala,NameKala,ccKala,txtsVahedeShomaresh,sVahedeShomaresh,NameBrand,Radif from qryAN_Kala Where Faal = 1"
                MultiSelection = False
                SearchItem = "CodeKala"
                objKala.SetForm(StrSqlKala)
                objKala.ShowDialog()
                If Val(objKala.tccKala) = 0 Then Exit Sub
                FillKalaRow(GridEXSatr.CurrentRow, CInt(objKala.tccKala), objKala.tcodeKala, objKala.tNameKala)
            End If

            e.Handled = True
            Exit Sub
        End If

        ' ---------- F5 : شماره بچ ----------
        'If e.KeyCode = Keys.F5 AndAlso GridEXSatr.CurrentColumn.Key = "ShomarehBatch" Then
        If e.KeyCode = Keys.F5 AndAlso GridEXSatr.CurrentColumn.Key = "ShomarehBatch" Then

            ' با رسید، بچ فقط از سطر رسید (F2) می‌آید
            If ccResid > 0 Then
                e.Handled = True
                Exit Sub
            End If

            Dim ccKalaObj As Object = GridEXSatr.CurrentRow.Cells("ccKala").Value
            If ccKalaObj Is Nothing OrElse IsDBNull(ccKalaObj) OrElse Val(ccKalaObj) = 0 Then
                MessageBox.Show("ابتدا کالا را انتخاب کنید")
                FocusCol("CodeKala")
                Exit Sub
            End If

            Dim objShomarehBach As New Forms_dll.ShomarehBachSearch
            Dim StrSql As String
            StrSql = "Select ShomarehBach,TarikhTolidSlash,TarikhEnghezaSlash,ccShomarehBach, "
            StrSql &= " NameTaminKonandeh,IsNull(NameTolidKonandeh,'---'),NameKala"
            StrSql &= " ,(SELECT [dbo].[fnAN_GetMojodiDarHalForosh_bachTarikh] (" & cmbAnbar.SelectedValue & ", Q.ccKala, Q.TarikhTolid, Q.TarikhEngheza, " & CodeMahalFaal & ", " & CodeDoreh & ", " & TarikhEmrooz & ", replace(Q.ShomarehBach,' ',''))) As MojodiDarHalForoshBach, IRC, GTIN "
            StrSql &= " From QryGL_ShomarehBach as Q "
            StrSql &= " Where ccKala = " & Val(ccKalaObj)
            If Not IsNothing(cmbTaminKonandeh.SelectedValue) Then
                StrSql &= " And ccTaminKonandeh = " & cmbTaminKonandeh.SelectedValue
            End If

            objShomarehBach.SearchItem = "ShomarehBach"
            objShomarehBach.SetForm(StrSql)
            objShomarehBach.ShowDialog()

            If String.IsNullOrEmpty(objShomarehBach.tShomarehBach) Then Exit Sub

            With GridEXSatr.CurrentRow
                .Cells("ShomarehBatch").Value = objShomarehBach.tShomarehBach
                .Cells("TarikhTolid").Value = objShomarehBach.tTarikhTolid
                .Cells("TarikhEngheza").Value = objShomarehBach.tTarikhEngheza
                .Cells("IRC").Value = objShomarehBach.tiRC
                .Cells("GTIN").Value = objShomarehBach.tGTIN
            End With

            Dim price As Double = GetPriceForBach(CInt(ccKalaObj), Convert.ToString(objShomarehBach.tShomarehBach).Trim())
            _isCalculating = True
            Try
                GridEXSatr.CurrentRow.Cells("MablaghKharid").Value = price
            Finally
                _isCalculating = False
            End Try
            CalcRow(GridEXSatr.CurrentRow)
            e.Handled = True
        End If
    End Sub

    ' ---------- حذف ----------
    Private Sub GridEXSatr_DeletingRecord(sender As Object, e As Janus.Windows.GridEX.RowActionCancelEventArgs) Handles GridEXSatr.DeletingRecord
        e.Cancel = True

        Dim row As Janus.Windows.GridEX.GridEXRow = GridEXSatr.CurrentRow
        If row Is Nothing OrElse row.RowType <> Janus.Windows.GridEX.RowType.Record Then Exit Sub

        Dim id As Object = row.Cells("ccKardexSatr").Value
        If id Is Nothing OrElse IsDBNull(id) Then Exit Sub

        If MessageBox.Show("آیا از حذف این ردیف مطمئن هستید؟", "حذف",
                           MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2,
                           MessageBoxOptions.RightAlign Or MessageBoxOptions.RtlReading) <> DialogResult.Yes Then Exit Sub

        Try
            Using con As New SqlConnection(ConnectionString)
                Using cmd As SqlCommand = con.CreateCommand()
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.CommandText = "[dbo].[spAN_KdxMarjoee_DeleteSatr]"
                    cmd.Parameters.AddWithValue("@ccKardexSatr", id)
                    con.Open()
                    cmd.ExecuteNonQuery()
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        BeginInvoke(New MethodInvoker(AddressOf ReloadSatr))
    End Sub

#End Region

    ' تعداد باقی‌مانده‌ی کالا/بچ در رسید انتخاب‌شده
    Private Function GetTedadResid(ByVal ccKala As Integer, ByVal bach As String,
                                   ByVal excludeSatr As Object) As Double
        Dim sql As String =
            "SELECT ISNULL((SELECT SUM(Tedad3) FROM tblAN_KdxResidSatr " &
            "               WHERE ccKardexTitr = @r AND ccKala = @k " &
            "                 AND REPLACE(LTRIM(RTRIM(ISNULL(ShomarehBach,''))),' ','') = @b),0) " &
            "     - ISNULL((SELECT SUM(s.Tedad3) FROM tblAN_KdxMarjoeeBeTaminKonandehSatr s " &
            "               JOIN tblAN_KdxMarjoeeBeTaminKonandeh t ON t.ccKardexTitr = s.ccKardexTitr " &
            "               WHERE t.ccKardexTitrR = @r AND s.ccKala = @k " &
            "                 AND s.ccKardexSatr <> @ex " &
            "                 AND REPLACE(LTRIM(RTRIM(ISNULL(s.ShomarehBach,''))),' ','') = @b),0)"
        Using cn As New SqlConnection(ConnectionString)
            Using cm As New SqlCommand(sql, cn)
                cm.Parameters.AddWithValue("@r", ccResid)
                cm.Parameters.AddWithValue("@k", ccKala)
                cm.Parameters.AddWithValue("@b", bach.Replace(" ", ""))
                cm.Parameters.AddWithValue("@ex", If(excludeSatr Is Nothing OrElse IsDBNull(excludeSatr), 0, excludeSatr))
                cn.Open()
                Dim r As Object = cm.ExecuteScalar()
                If r Is Nothing OrElse IsDBNull(r) Then Return 0
                Return Convert.ToDouble(r)
            End Using
        End Using
    End Function

    Private Function IsValidTedad(ByVal row As Janus.Windows.GridEX.GridEXRow, ByVal isNew As Boolean) As Boolean
        Dim tedad As Double = 0
        TryGetNumber(row, "Tedad3", tedad)

        Dim ccKala As Integer = Convert.ToInt32(row.Cells("ccKala").Value)
        Dim bach As String = Convert.ToString(row.Cells("ShomarehBatch").Value).Trim()
        Dim isJay As Boolean = IsBonusRow(row)
        Dim idSatr As Object = If(isNew, Nothing, row.Cells("ccKardexSatr").Value)

        Dim tolid As String = "", engh As String = ""
        TryToMiladi(row.Cells("TarikhTolid").Value, tolid)
        TryToMiladi(row.Cells("TarikhEngheza").Value, engh)

        Dim optMsg As MessageBoxOptions = MessageBoxOptions.RightAlign Or MessageBoxOptions.RtlReading

        ' ---------- ۱) سقف باقی‌مانده‌ی رسید ----------
        If ccResid > 0 Then
            Dim baghi As Double = GetBaghiMandeh(ccKala, bach, isJay, idSatr)
            If tedad > baghi Then
                MessageBox.Show("تعداد از باقی‌مانده‌ی رسید بیشتر است." & vbCrLf &
                            "شماره بچ: " & bach & vbCrLf &
                            If(isJay, "نوع: جایزه" & vbCrLf, "") &
                            "حداکثر مجاز: " & baghi.ToString(),
                            "باقی‌مانده‌ی رسید", MessageBoxButtons.OK, MessageBoxIcon.Warning,
                            MessageBoxDefaultButton.Button1, optMsg)
                FocusCol("Tedad3")
                Return False
            End If
        End If

        ' ---------- ۲) سقف موجودی قابل فروش (مثل پیش‌فاکتور) ----------
        Dim oldTedad As Double = 0
        If Not isNew Then oldTedad = GetOldTedad(idSatr)

        Dim maxMojaz As Double = GetMojodiBach(ccKala, tolid, engh, bach) + oldTedad
        If tedad > maxMojaz Then
            MessageBox.Show("تعداد از موجودی قابل فروش بیشتر است." & vbCrLf &
                        "شماره بچ: " & bach & vbCrLf &
                        "حداکثر مجاز: " & maxMojaz.ToString(),
                        "موجودی ناکافی", MessageBoxButtons.OK, MessageBoxIcon.Warning,
                        MessageBoxDefaultButton.Button1, optMsg)
            FocusCol("Tedad3")
            Return False
        End If

        Return True
    End Function
    ' موجودی قابل برگشت این کالا/بچ را از SQL می‌گیرد
    Private Function GetMojodiBach(ByVal ccKala As Integer, ByVal tolid As String,
                                   ByVal engheza As String, ByVal bach As String) As Double
        Using cn As New SqlConnection(ConnectionString)
            Dim sql As String = "SELECT dbo.fnAN_GetMojodiDarHalForosh_bachTarikh(@anbar,@kala,@tolid,@engh,@mahal,@doreh,@emrooz,@bach)"
            Using cm As New SqlCommand(sql, cn)
                cm.Parameters.AddWithValue("@anbar", If(IsNothing(cmbAnbar.SelectedValue), 0, cmbAnbar.SelectedValue))
                cm.Parameters.AddWithValue("@kala", ccKala)
                cm.Parameters.AddWithValue("@tolid", tolid)
                cm.Parameters.AddWithValue("@engh", engheza)
                cm.Parameters.AddWithValue("@mahal", CodeMahalFaal)
                cm.Parameters.AddWithValue("@doreh", CodeDoreh)
                cm.Parameters.AddWithValue("@emrooz", Convert.ToString(TarikhEmrooz))
                cm.Parameters.AddWithValue("@bach", bach.Replace(" ", ""))
                cn.Open()
                Dim r As Object = cm.ExecuteScalar()
                If r Is Nothing OrElse IsDBNull(r) Then Return 0
                Return Convert.ToDouble(r)
            End Using
        End Using
    End Function

    ' تعداد قبلی همین سطر در دیتابیس (برای ویرایش)
    Private Function GetOldTedad(ByVal ccKardexSatr As Object) As Double
        If ccKardexSatr Is Nothing OrElse IsDBNull(ccKardexSatr) Then Return 0
        Using cn As New SqlConnection(ConnectionString)
            Using cm As New SqlCommand("SELECT ISNULL(Tedad3,0) FROM tblAN_KdxMarjoeeBeTaminKonandehSatr WHERE ccKardexSatr = @id", cn)
                cm.Parameters.AddWithValue("@id", ccKardexSatr)
                cn.Open()
                Dim r As Object = cm.ExecuteScalar()
                If r Is Nothing OrElse IsDBNull(r) Then Return 0
                Return Convert.ToDouble(r)
            End Using
        End Using
    End Function

    Private Sub FillSingleBachFromResid(ByVal row As Janus.Windows.GridEX.GridEXRow, ByVal ccKala As Integer)
        Dim cnt As Integer = 0
        Using cn As New SqlConnection(ConnectionString)
            Using cm As New SqlCommand("SELECT COUNT(DISTINCT LTRIM(RTRIM(ISNULL(ShomarehBach,'')))) FROM tblAN_KdxResidSatr WHERE ccKardexTitr = @r AND ccKala = @k", cn)
                cm.Parameters.AddWithValue("@r", ccResid)
                cm.Parameters.AddWithValue("@k", ccKala)
                cn.Open()
                cnt = Convert.ToInt32(cm.ExecuteScalar())
            End Using
        End Using
        If cnt = 0 Then Exit Sub

        ' هم برای یک بچ و هم چند بچ: لیست باز شود (با یک بچ هم فقط یک ردیف می‌بیند و Enter می‌زند)
        Dim f As New frmAN_KalaSearch_Resid(ConnectionString, ccResid)
        f.InitialFilter = Convert.ToString(row.Cells("CodeKala").Value).Trim()
        If f.ShowDialog() <> DialogResult.OK OrElse f.SelectedCcKala <> ccKala Then Exit Sub

        _isCalculating = True
        Try
            With row
                .Cells("ShomarehBatch").Value = f.SelectedBach
                .Cells("TarikhTolid").Value = f.SelectedTarikhTolid
                .Cells("TarikhEngheza").Value = f.SelectedTarikhEngheza
                .Cells("IRC").Value = f.SelectedIRC
                .Cells("GTIN").Value = f.SelectedGTIN
                .Cells("MablaghKharid").Value = f.SelectedMablaghKharid
                .Cells("Tedad3").Value = f.SelectedTedadMandeh
            End With
        Finally
            _isCalculating = False
        End Try
        ApplyResidPick(row, f)
    End Sub

    Private Sub ApplyResidPick(ByVal row As Janus.Windows.GridEX.GridEXRow, ByVal f As frmAN_KalaSearch_Resid)
        _isCalculating = True
        Try
            With row
                .Cells("ShomarehBatch").Value = f.SelectedBach
                .Cells("TarikhTolid").Value = f.SelectedTarikhTolid
                .Cells("TarikhEngheza").Value = f.SelectedTarikhEngheza
                .Cells("IRC").Value = f.SelectedIRC
                .Cells("GTIN").Value = f.SelectedGTIN
                .Cells("MablaghKharid").Value = f.SelectedMablaghKharid
                .Cells("Tedad3").Value = f.SelectedTedadMandeh
                .Cells("IsJayezeh").Value = f.SelectedIsJayezeh      ' تیک جایزه
            End With
        Finally
            _isCalculating = False
        End Try
        CalcRow(row)
    End Sub

    ' آیا این سلول در این سطر نباید ویرایش شود؟
    Private Function IsCellLocked(ByVal row As Janus.Windows.GridEX.GridEXRow, ByVal key As String) As Boolean
        If row Is Nothing Then Return False

        If row.RowType = Janus.Windows.GridEX.RowType.Record Then
            ' سطر ذخیره‌شده: فقط تعداد (و مبلغ، اگر رسید ندارد)
            If key = "Tedad3" Then Return False
            If key = "MablaghKharid" Then Return (ccResid > 0)
            Return True
        End If

        ' سطر جدید با رسید: بچ، تاریخ‌ها، تیک جایزه و مبلغ فقط از انتخاب سطر رسید می‌آیند
        If ccResid > 0 Then
            Select Case key
                Case "ShomarehBatch", "TarikhTolid", "TarikhEngheza", "IsJayezeh", "MablaghKharid"
                    Return True
            End Select
        End If
        Return False
    End Function

    Private Function GetBaghiMandeh(ByVal ccKala As Integer, ByVal bach As String,
                                ByVal isJay As Boolean, ByVal excludeSatr As Object) As Double
        Dim sql As String =
            "SELECT ISNULL((SELECT SUM(Tedad3) FROM tblAN_KdxResidSatr " &
            "               WHERE ccKardexTitr = @r AND ccKala = @k AND ISNULL(IsJayezeh,0) = @j " &
            "                 AND REPLACE(LTRIM(RTRIM(ISNULL(ShomarehBach,''))),' ','') = @b),0) " &
            "     - ISNULL((SELECT SUM(s.Tedad3) FROM tblAN_KdxMarjoeeBeTaminKonandehSatr s " &
            "               JOIN tblAN_KdxMarjoeeBeTaminKonandeh t ON t.ccKardexTitr = s.ccKardexTitr " &
            "               WHERE t.ccKardexTitrR = @r AND s.ccKala = @k AND ISNULL(s.IsJayezeh,0) = @j " &
            "                 AND s.ccKardexSatr <> @ex " &
            "                 AND REPLACE(LTRIM(RTRIM(ISNULL(s.ShomarehBach,''))),' ','') = @b),0)"
        Using cn As New SqlConnection(ConnectionString)
            Using cm As New SqlCommand(sql, cn)
                cm.Parameters.AddWithValue("@r", ccResid)
                cm.Parameters.AddWithValue("@k", ccKala)
                cm.Parameters.AddWithValue("@j", If(isJay, 1, 0))
                cm.Parameters.AddWithValue("@b", bach.Replace(" ", ""))
                cm.Parameters.AddWithValue("@ex", If(excludeSatr Is Nothing OrElse IsDBNull(excludeSatr), 0, excludeSatr))
                cn.Open()
                Dim r As Object = cm.ExecuteScalar()
                If r Is Nothing OrElse IsDBNull(r) Then Return 0
                Return Convert.ToDouble(r)
            End Using
        End Using
    End Function

End Class