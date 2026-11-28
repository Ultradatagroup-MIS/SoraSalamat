Imports Janus.Windows.GridEX

Public Class frmVosolKoli
#Region "Variable AND Constant Declration"
    Const FormTableName = "tblDP_Amalyat"
    Const FormViewName = "qryDP_Amalyat"
    Const FormAddSize = 100
    Const FormOrgSize = 215
    Const GridAddSize = 230

    Dim ErrPro As New ErrorProvider
    Dim Mode As UD_Dll.Enums.GL_ModeForms = UD_Dll.Enums.GL_ModeForms.AddNewRecord
    Dim dsForm As New DataSet
    Dim cmForm As CurrencyManager
    Dim dvForm As DataView
    Dim txtCaption As String
    Private WithEvents BS As New UD_Dll.PassString
    Dim cntCodeSubSystem As Long = 958 '857
    Private SN As Integer

    Dim Flag As Boolean = False

    Dim s As Integer
    Dim tTarikhDP As String
    Dim tSh As Long
    Dim tEbtal As Boolean
    Dim tVazeiat As Long
    Dim tTarikhVazeiat As String
    Dim tSaatVazeiat As String
    Dim tShHEntry As String

    Public DorehFaktor_Varizi As Integer

    Dim flg As Boolean = False ' flag grid
    Private _loading As Boolean = False
    '=-------------------
    Public ccMoshtary As Long = 0
    Public ccFaktorTitr As Long = 0
#End Region
    Private Sub txtCodeMoshtaryS_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCodeMoshtaryF.KeyPress
        Try
            If (Asc(e.KeyChar()) < 48 Or Asc(e.KeyChar()) > 57) And (Asc(e.KeyChar()) <> 8) Then
                e.Handled = True
            End If
            If e.KeyChar = Chr(Keys.Space) Then
                Dim objMoshtary As New Forms_dll.frmFO_MoshtarySearch
                Dim StrSql As String

                StrSql = "Select * from qryFO_Moshtary Where CodeMahal=" & CodeMahalFaal & " AND sVazeiat = 3980 "
                StrSql &= " AND Not exists (Select * From tblGL_SecurityData where namekarbar= '" & UserName & "' and CodeSubSystem = 614 and pk = qryFO_Moshtary.ccMoshtary) order by NameMoshtary"

                tCodeMoshtary = ""
                tNameMoshtary = ""
                tccMoshtary = ""
                SearchItem = "CodeMoshtary"
                If txtCodeMoshtaryF.Text.Length <> 0 Then
                    tCodeMoshtary = txtCodeMoshtaryF.Text
                End If
                objMoshtary.MultiSelection = False
                objMoshtary.SetForm(StrSql)
                objMoshtary.ShowDialog()
                txtCodeMoshtaryF.Tag = IIf(IsNothing(objMoshtary.tccMoshtary), 0, objMoshtary.tccMoshtary)
                txtCodeMoshtaryF.Text = IIf(IsNothing(objMoshtary.tCodeMoshtary), "", objMoshtary.tCodeMoshtary)
                lblNameMoshtary.Text = IIf(IsNothing(objMoshtary.tNameMoshtary), "", objMoshtary.tNameMoshtary)

            End If
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->txtCodeMoshtaryS_KeyPress")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->txtCodeMoshtaryS_KeyPress")
        End Try

    End Sub
    Private _mandehDaryafti As Double = 0
    Private Sub btnNewRecord_Click(sender As Object, e As EventArgs) Handles btnNewRecord.Click
        If Not IsValidForm("All") Then Exit Sub

        Try
            _codeAmalyat = InsertDaryafti()
        Catch ex As Exception
            MsgBox("خطا در ثبت دریافتی: " & ex.Message,
                   MsgBoxStyle.Critical Or MsgBoxStyle.MsgBoxRtlReading, "خطا")
            Return
        End Try

        _mandehDaryafti = CDbl(txtMablagh.Text.Replace(",", ""))
        Label8.Text = _mandehDaryafti.ToString("#,##0")
        GroupBox1.Enabled = False      ' TODO: اسم GroupBox سمت راست
        btnNewRecord.Enabled = False
        btnTasvieh.Enabled = True
        LoadFaktorMandehDar()

    End Sub
    Private _codeAmalyat As Long = 0

    Private Function InsertDaryafti() As Long
        Dim noeCode As Integer = CInt(cmbsNoeSanad.SelectedValue)

        Using cn As New SqlConnection(ConnectionString)
            Using cmd As New SqlCommand("[Treasury].[spVosolFaktor_DaryaftKoli_Insert]", cn)
                cmd.CommandType = CommandType.StoredProcedure

                cmd.Parameters.AddWithValue("@CodeMahal", CodeMahalFaal)
                cmd.Parameters.AddWithValue("@CodeDoreh", CodeDoreh)                 ' TODO: اسم متغیر عمومی
                cmd.Parameters.AddWithValue("@NoeAmalyat", 1)
                cmd.Parameters.AddWithValue("@ccMoshtary", txtCodeMoshtaryF.Tag)
                cmd.Parameters.AddWithValue("@sNoeSanad", noeCode)

                If noeCode = 65 Then
                    cmd.Parameters.AddWithValue("@ccShomarehHesab", CInt(cmbShomarehHesab_B.SelectedValue))
                Else
                    cmd.Parameters.AddWithValue("@ccShomarehHesab", CInt(cmbShomarehHesab_S.SelectedValue))
                End If


                cmd.Parameters.AddWithValue("@CodeFard", IIf(IsNothing(cmbFard.SelectedValue), 0, cmbFard.SelectedValue))


                cmd.Parameters.AddWithValue("@ShomarehResidMovaghat", 0.0)
                cmd.Parameters.AddWithValue("@Noe2", 4417)
                cmd.Parameters.AddWithValue("@TarikhDP", mskTarikhDP.Text)

                If mskTarikhSanad.Text.Trim().Length = 0 Then
                    cmd.Parameters.AddWithValue("@TarikhSanad", DBNull.Value)
                Else
                    cmd.Parameters.AddWithValue("@TarikhSanad", mskTarikhSanad.Text)
                End If

                ' ---- اطلاعات چک (فقط 64) ----
                If noeCode = 64 Then

                    cmd.Parameters.AddWithValue("@ShomarehSanad", IIf(Len(txtShomarehSanad.Text) = 0, "0", txtShomarehSanad.Text))
                    cmd.Parameters.AddWithValue("@NameTarafSanad", "")

                    Dim bankVal As Integer = 0
                    If Not IsNothing(cmbBankSanad.SelectedValue) AndAlso Not IsDBNull(cmbBankSanad.SelectedValue) Then
                        bankVal = CInt(cmbBankSanad.SelectedValue)
                    End If
                    cmd.Parameters.AddWithValue("@sBankSanad", bankVal)
                    cmd.Parameters.AddWithValue("@ShomarehHesabSanad", IIf(Len(txtShomarehHesabSanad.Text) = 0, 0, txtShomarehHesabSanad.Text))
                    cmd.Parameters.AddWithValue("@ShobehSanad", IIf(Len(txtShobehSanad.Text) = 0, 0, txtShobehSanad.Text))
                    cmd.Parameters.AddWithValue("@CodeShobehSanad", IIf(Len(txtCodeShobehSanad.Text) = 0, 0, txtCodeShobehSanad.Text))
                    cmd.Parameters.AddWithValue("@SeriAdadi", IIf(Len(mskSeriAdadi.Text) = 0, 0, mskSeriAdadi.Text))
                    cmd.Parameters.AddWithValue("@ShomarehSayadi", IIf(Len(txtSayadi.Text) = 0, 0, txtSayadi.Text))
                    cmd.Parameters.AddWithValue("@IsElectronic", IIf(chkElectronic.Checked, 1, 0))
                Else
                    cmd.Parameters.AddWithValue("@ShomarehSanad", "")
                    cmd.Parameters.AddWithValue("@NameTarafSanad", "")
                    cmd.Parameters.AddWithValue("@sBankSanad", 0)
                    cmd.Parameters.AddWithValue("@ShomarehHesabSanad", "")
                    cmd.Parameters.AddWithValue("@ShobehSanad", "")
                    cmd.Parameters.AddWithValue("@CodeShobehSanad", "")
                    cmd.Parameters.AddWithValue("@SeriAdadi", "")
                    cmd.Parameters.AddWithValue("@ShomarehSayadi", "")
                    cmd.Parameters.AddWithValue("@IsElectronic", 0)
                End If

                cmd.Parameters.AddWithValue("@TarikhVazeiat", TarikhEmrooz)
                cmd.Parameters.AddWithValue("@Sharh", txtTozihat.Text)                 ' TODO: اسم کنترل شرح
                cmd.Parameters.AddWithValue("@UserName", UserName)                   ' TODO: اسم متغیر عمومی

                ' پارامتر خروجی: برای خروجی ناچار باید نوع و جهت مشخص بشه
                Dim pOut As SqlParameter = cmd.Parameters.Add("@CodeAmalyat", SqlDbType.BigInt)
                pOut.Direction = ParameterDirection.Output

                cn.Open()
                cmd.ExecuteNonQuery()

                Return CLng(pOut.Value)
            End Using
        End Using
    End Function

    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnNewRecord.Click
        If Not IsValidForm("All") Then Return

        Try
            _codeAmalyat = InsertDaryafti()
        Catch ex As Exception
            MsgBox("خطا در ثبت دریافتی: " & ex.Message,
               MsgBoxStyle.Critical Or MsgBoxStyle.MsgBoxRtlReading, "خطا")
            Return
        End Try

        ' ثبت شد: سمت راست غیرفعال شود (فرم پاک نشود)
        GroupBox1.Enabled = False      ' TODO: اسم GroupBox سمت راست
        btnNewRecord.Enabled = False

        ' قدم بعدی: لود فاکتورهای مانده‌دار
    End Sub
    Private Function IsValidForm(ByVal CheckField As String) As Boolean
        IsValidForm = False


        ' ---------- مشتری (اول از همه) ----------
        If CheckField = "txtCodeMoshtaryF" Or CheckField = "All" Then
            If IsNothing(txtCodeMoshtaryF.Tag) OrElse txtCodeMoshtaryF.Tag.ToString().Trim().Length = 0 Then
                ErrPro.SetError(Me.txtCodeMoshtaryF, "مشتری را انتخاب کنید")
                MsgBox("مشتری را انتخاب کنید", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
                txtCodeMoshtaryF.Focus()
                Exit Function
            End If
            ErrPro.SetError(Me.txtCodeMoshtaryF, "")
        End If
        ' ---------- نوع دریافتی ----------
        If CheckField = "cmbsNoeSanad" Or CheckField = "All" Then
            If IsNothing(cmbsNoeSanad.SelectedValue) Then
                ErrPro.SetError(Me.cmbsNoeSanad, "نوع دریافتی را انتخاب کنید")
                MsgBox("نوع دریافتی را انتخاب کنید", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
                cmbsNoeSanad.Focus()
                Exit Function
            End If
            ErrPro.SetError(Me.cmbsNoeSanad, "")
        End If

        Dim noeCode As Integer = 0
        If Not IsNothing(cmbsNoeSanad.SelectedValue) Then
            noeCode = CInt(cmbsNoeSanad.SelectedValue)
        End If

        ' ---------- صندوق: نقد و چک (63 و 64) ----------
        If CheckField = "cmbShomarehHesab_S" Or CheckField = "All" Then
            If (noeCode = 63 Or noeCode = 64) AndAlso IsNothing(cmbShomarehHesab_S.SelectedValue) Then
                ErrPro.SetError(Me.cmbShomarehHesab_S, "صندوق را وارد کنيد")
                MsgBox("صندوق را وارد کنيد", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
                cmbShomarehHesab_S.Focus()
                Exit Function
            End If
            ErrPro.SetError(Me.cmbShomarehHesab_S, "")
        End If

        ' ---------- شماره حساب بانکی: حواله/پوز (65) ----------
        If CheckField = "cmbShomarehHesab_B" Or CheckField = "All" Then
            If noeCode = 65 AndAlso IsNothing(cmbShomarehHesab_B.SelectedValue) Then
                ErrPro.SetError(Me.cmbShomarehHesab_B, "شماره حساب را وارد کنيد")
                MsgBox("شماره حساب را وارد کنيد", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
                cmbShomarehHesab_B.Focus()
                Exit Function
            End If
            ErrPro.SetError(Me.cmbShomarehHesab_B, "")
        End If

        ' ---------- تاریخ دریافت (برای هر سه نوع) ----------
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

            If mskTarikhDP.Text > TarikhEmrooz Then
                ErrPro.SetError(Me.mskTarikhDP, "تاريخ دریافت نمی تواند جلوتر از تاریخ روز باشد.")
                MsgBox("تاريخ دریافت نمی تواند جلوتر از تاریخ روز باشد.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
                mskTarikhDP.Focus()
                Exit Function
            End If
            ErrPro.SetError(Me.mskTarikhDP, "")
        End If

        ' ---------- تاریخ سررسید: چک اجباری، حواله/پوز اختیاری ----------
        If CheckField = "mskTarikhSanad" Or CheckField = "All" Then
            If noeCode = 64 Then
                If Len(mskTarikhSanad.Text.ToString) <> 0 Then
                    If Not objTarikh.IsShDate(mskTarikhSanad.Text.ToString) Then
                        mskTarikhSanad.Focus()
                        Exit Function
                    End If
                Else
                    ErrPro.SetError(Me.mskTarikhSanad, "تاريخ سر رسید " & " راوارد کنيد")
                    MsgBox("تاريخ سر رسید  " & " راوارد کنيد", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
                    mskTarikhSanad.Focus()
                    Exit Function
                End If
            End If

            If (noeCode = 64 Or noeCode = 65) AndAlso mskTarikhSanad.Text.Length <> 0 AndAlso mskTarikhSanad.Text < mskTarikhDP.Text Then
                ErrPro.SetError(mskTarikhSanad, "تاریخ سررسید نمیتواند پیش از تاریخ دریافت باشد.")
                MsgBox("تاریخ سررسید نمیتواند پیش از تاریخ دریافت باشد.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
                mskTarikhSanad.Focus()
                Exit Function
            End If
            ErrPro.SetError(Me.mskTarikhSanad, "")
        End If

        ' ---------- مبلغ ----------
        If CheckField = "txtMablagh" Or CheckField = "All" Then
            If txtMablagh.Text.Length = 0 Then
                ErrPro.SetError(txtMablagh, "مبلغ را وارد کنيد")
                MsgBox("مبلغ را وارد کنيد", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
                txtMablagh.Focus()
                Exit Function
            End If
            ErrPro.SetError(Me.txtMablagh, "")
        End If
        If noeCode = 64 Then
            If CheckField = "txtShomarehSanad" Or CheckField = "All" Then
                If txtShomarehSanad.Text.Length = 0 Then
                    ErrPro.SetError(txtShomarehSanad, "شماره را وارد کنيد")
                    MsgBox("شماره را وارد کنيد", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
                    txtShomarehSanad.Focus()
                    Exit Function
                End If
                ErrPro.SetError(Me.txtShomarehSanad, "")
            End If

            If objTools.ConvertNulls(objTools.DCount("ShomarehSanad", "tblDP_Amalyat", " ShomarehSanad= '" & txtShomarehSanad.Text & "' "), 0) >= 1 Then
                If MsgBox("شماره چک تکراری وارد شده است، رکورد ذخیره شود ؟", MsgBoxStyle.YesNo + MsgBoxStyle.Question + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.DefaultButton2, "تایید") = MsgBoxResult.No Then
                    txtShomarehSanad.Focus()
                    ErrPro.SetError(Me.txtShomarehSanad, "شماره چک تکراری وارد شده است، رکورد ذخیره شود ؟")
                    Exit Function
                End If
            Else
                ErrPro.SetError(Me.txtShomarehSanad, "")
            End If



        End If

        Return True
    End Function

    Private Sub frmVosolKoli_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        _loading = True
        LoadCombo()
        _loading = False

        ClearForm()

        objTools.ChangeKbd(UD_Dll.mdlUtility.KBD.Latin)
    End Sub
    Private Sub LoadCombo()
        Try
            Using cn As New SqlConnection(ConnectionString)
                cn.Open()

                ' --- شماره حساب‌ها ---
                Using cmd As New SqlCommand("dbo.sp_DP_Select_ShomarehHesab", cn)
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.Parameters.Add("@CodeMahal", SqlDbType.Int).Value = CodeMahalFaal
                    If dsForm.Tables.Contains("tblBank") Then dsForm.Tables("tblBank").Clear()
                    Using da As New SqlDataAdapter(cmd)
                        da.Fill(dsForm, "tblBank")
                    End Using
                End Using

                If dsForm.Tables("tblBank").Rows.Count = 0 Then
                    MsgBox("برای استفاده از این فرم ابتدا باید شماره حساب های شرکت را تعریف کنید.",
                       MsgBoxStyle.Information Or MsgBoxStyle.MsgBoxRtlReading, "پیام")
                    Me.Close()
                    Return
                End If

                cmbShomarehHesab_B.DataSource = dsForm.Tables("tblBank").DefaultView
                cmbShomarehHesab_B.DisplayMember = "ShomarehHesabKamel"
                cmbShomarehHesab_B.ValueMember = "ccShomarehHesab"
                cmbShomarehHesab_B.SelectedIndex = -1

                ' --- نوع دریافتی ---
                Using da As New SqlDataAdapter(
                "SELECT Code, Sharh FROM tblGL_ShenasehOmomi " &
                "WHERE CodeAsli = 13 AND CodeFarei = 1 AND Code IN (4, 63, 64, 65)", cn)
                    If dsForm.Tables.Contains("NoeDaryafti") Then dsForm.Tables("NoeDaryafti").Clear()
                    da.Fill(dsForm, "NoeDaryafti")
                End Using
                cmbsNoeSanad.DataSource = dsForm.Tables("NoeDaryafti").DefaultView
                cmbsNoeSanad.DisplayMember = "Sharh"
                cmbsNoeSanad.ValueMember = "Code"
                cmbsNoeSanad.SelectedIndex = -1

                ' --- افراد ---
                Using da As New SqlDataAdapter(
                "SELECT * FROM qryGL_MoshakhasatFardi " &
                "WHERE CodeMahal = @cm AND sVazeiatEstekhdam = 4188 ORDER BY FN", cn)
                    da.SelectCommand.Parameters.Add("@cm", SqlDbType.Int).Value = CodeMahalFaal
                    If dsForm.Tables.Contains("tblFard") Then dsForm.Tables("tblFard").Clear()
                    da.Fill(dsForm, "tblFard")
                End Using
                cmbFard.DataSource = dsForm.Tables("tblFard").DefaultView
                cmbFard.DisplayMember = "FN"
                cmbFard.ValueMember = "CodeFard"
                cmbFard.SelectedIndex = -1

                ' --- صندوق ---
                Using cmd As New SqlCommand("dbo.sp_DP_Select_ShomarehHesabSandogh", cn)
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.Parameters.Add("@CodeMahal", SqlDbType.Int).Value = CodeMahalFaal
                    If dsForm.Tables.Contains("tblSandogh") Then dsForm.Tables("tblSandogh").Clear()
                    Using da As New SqlDataAdapter(cmd)
                        da.Fill(dsForm, "tblSandogh")
                    End Using
                End Using
                cmbShomarehHesab_S.DataSource = dsForm.Tables("tblSandogh").DefaultView
                cmbShomarehHesab_S.DisplayMember = "Sharh"
                cmbShomarehHesab_S.ValueMember = "Code"
                cmbShomarehHesab_S.SelectedIndex = -1

                ' --- بانک سند (با ردیف خالی در ابتدا) ---
                Using da As New SqlDataAdapter(
                "SELECT CAST(NULL AS int) AS Code, N'' AS Sharh, 0 AS SortKey " &
                "UNION ALL " &
                "SELECT Code, Sharh, 1 AS SortKey FROM tblGL_ShenasehOmomi " &
                "WHERE (CodeAsli = 6 AND CodeFarei <> 0) OR (Code = 0 AND Dideh = 1) " &
                "ORDER BY SortKey, Sharh", cn)
                    If dsForm.Tables.Contains("ComboBank") Then dsForm.Tables("ComboBank").Clear()
                    da.Fill(dsForm, "ComboBank")
                End Using
                cmbBankSanad.DataSource = dsForm.Tables("ComboBank").DefaultView
                cmbBankSanad.DisplayMember = "Sharh"
                cmbBankSanad.ValueMember = "Code"
                cmbBankSanad.SelectedIndex = 0
            End Using

        Catch ex As Exception
            MsgBox("خطا در بارگذاری اطلاعات اولیه: " & ex.Message,
               MsgBoxStyle.Critical Or MsgBoxStyle.MsgBoxRtlReading, "خطا")
        End Try
    End Sub


    Private Sub txtCodeMoshtary_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCodeMoshtaryF.TextChanged
        Dim Criteria As String = ""
        Dim Tozihat As String = objTools.DLookup("Tozihat", "tblFO_Moshtary", "ccMoshtary = " & ccMoshtary)
        Criteria = "CodeMahal=" & CodeMahalFaal
        Criteria &= " AND Not exists (Select * From tblGL_SecurityData where namekarbar= '" & UserName & "' and CodeSubSystem = 614 and pk = qryFO_Moshtary.ccMoshtary) "
        Criteria &= " And CodeMoshtary = '" & IIf(IsNothing(Me.txtCodeMoshtaryF.Text), 0, Me.txtCodeMoshtaryF.Text) & "'"
        Criteria &= " AND sVazeiat = " & UD_Dll.Enums.FO_VaziatMoshtary.Faal
        Me.lblNameMoshtary.Text = objTools.ConvertNulls(objTools.DLookup("NameMoshtary", "qryFO_Moshtary", Criteria), "")
        Me.txtCodeMoshtaryF.Tag = objTools.ConvertNulls(objTools.DLookup("ccMoshtary", "qryFO_Moshtary", Criteria), "")
        lblNameMoshtary.Visible = True

    End Sub


    Private Sub SetVisibilityByNoeSanad()
        Dim noeCode As Integer = 0
        If Not IsNothing(cmbsNoeSanad.SelectedValue) Then
            If IsNumeric(cmbsNoeSanad.SelectedValue) Then
                noeCode = CInt(cmbsNoeSanad.SelectedValue)
            End If
        End If

        Dim showSandogh As Boolean = (noeCode = 63 Or noeCode = 64)
        Dim showBank As Boolean = (noeCode = 65)
        Dim showCheck As Boolean = (noeCode = 64)

        cmbShomarehHesab_S.Visible = showSandogh


        cmbShomarehHesab_B.Visible = showBank


        GroupBox3.Visible = showCheck

        ' مقدار کنترل‌های مخفی‌شده پاک بشه تا اشتباهاً ذخیره نشه
        If Not showSandogh Then cmbShomarehHesab_S.SelectedIndex = -1
        If Not showBank Then cmbShomarehHesab_B.SelectedIndex = -1
        If Not showCheck Then ClearCheckFields()
    End Sub
    Private Sub cmbsNoeSanad_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbsNoeSanad.SelectedIndexChanged
        If _loading Then Return
        SetVisibilityByNoeSanad()
    End Sub
    Private Sub ClearCheckFields()
        Dim c As Control
        For Each c In GroupBox3.Controls
            If TypeOf c Is TextBox Then
                CType(c, TextBox).Text = ""
            ElseIf TypeOf c Is MaskedTextBox Then
                CType(c, MaskedTextBox).Text = ""
            ElseIf TypeOf c Is ComboBox Then
                CType(c, ComboBox).SelectedIndex = -1
            ElseIf TypeOf c Is CheckBox Then
                CType(c, CheckBox).Checked = False
            End If
        Next
    End Sub
    Private Sub ClearForm()
        _loading = True

        ' ---------- مشتری ----------
        txtCodeMoshtaryF.Text = ""
        txtCodeMoshtaryF.Tag = Nothing
        lblNameMoshtary.Text = ""

        ' ---------- ثبت دریافتی ----------
        cmbsNoeSanad.SelectedIndex = -1
        cmbShomarehHesab_S.SelectedIndex = -1
        cmbShomarehHesab_B.SelectedIndex = -1
        cmbFard.SelectedIndex = -1
        If cmbBankSanad.Items.Count > 0 Then cmbBankSanad.SelectedIndex = 0

        mskTarikhDP.Text = ""
        mskTarikhSanad.Text = ""
        txtMablagh.Text = ""

        ' ---------- فیلدهای چک ----------
        ClearCheckFields()
        GroupBox3.Visible = False

        ' ---------- پاک کردن پیام‌های خطا ----------
        ErrPro.SetError(txtCodeMoshtaryF, "")
        ErrPro.SetError(cmbsNoeSanad, "")
        ErrPro.SetError(cmbShomarehHesab_S, "")
        ErrPro.SetError(cmbShomarehHesab_B, "")
        ErrPro.SetError(mskTarikhDP, "")
        ErrPro.SetError(mskTarikhSanad, "")
        ErrPro.SetError(txtMablagh, "")

        _loading = False

        SetVisibilityByNoeSanad()
        txtCodeMoshtaryF.Focus()
    End Sub
    Private _dtFaktor As DataTable

    Private Sub LoadFaktorMandehDar()
        Try
            _dtFaktor = New DataTable("tblFaktorMandehDar")

            Using cn As New SqlConnection(ConnectionString)
                Using cmd As New SqlCommand("[Treasury].[spVosolFaktor_DaryaftKoli_LoadFaktorMandehDar]", cn)
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.Parameters.AddWithValue("@ccMoshtary", CInt(txtCodeMoshtaryF.Tag))
                    Using da As New SqlDataAdapter(cmd)
                        da.Fill(_dtFaktor)
                    End Using
                End Using
            End Using
            ' ستون تایید (محلی، در دیتابیس نیست)
            Dim cTaeed As New DataColumn("Taeed", GetType(Boolean))
            cTaeed.DefaultValue = False
            _dtFaktor.Columns.Add(cTaeed)

            SetGrid()
            GridEXFaktor.DataSource = _dtFaktor


        Catch ex As Exception
            MsgBox("خطا در بارگذاری فاکتورهای مانده‌دار: " & ex.Message,
               MsgBoxStyle.Critical Or MsgBoxStyle.MsgBoxRtlReading, "خطا")
        End Try
    End Sub

    Private Sub SetGrid()
        Dim col As GridEXColumn

        With GridEXFaktor
            .RootTable.Columns.Clear()
            .AllowEdit = InheritableBoolean.True
            .AllowAddNew = InheritableBoolean.False
            .AllowDelete = InheritableBoolean.False
            .RightToLeft = Windows.Forms.RightToLeft.Yes
            .GroupByBoxVisible = False
            .SelectionMode = SelectionMode.SingleSelection
        End With

        ' ---- ستون تایید (چک‌باکس) ----
        col = GridEXFaktor.RootTable.Columns.Add("Taeed", ColumnType.CheckBox)
        col.Caption = "تایید"
        col.DataMember = "Taeed"
        col.EditType = EditType.CheckBox
        col.Width = 50
        col.AllowSort = False

        ' ---- شماره فاکتور ----
        col = GridEXFaktor.RootTable.Columns.Add("FaktorShomareh", ColumnType.Text)
        col.Caption = "شماره فاکتور"
        col.DataMember = "FaktorShomareh"
        col.Width = 100
        col.EditType = EditType.NoEdit

        ' ---- تاریخ فاکتور ----
        col = GridEXFaktor.RootTable.Columns.Add("FaktorTarikh", ColumnType.Text)
        col.Caption = "تاریخ فاکتور"
        col.DataMember = "FaktorTarikh"
        col.Width = 90
        col.EditType = EditType.NoEdit

        ' ---- مبلغ کل ----
        col = GridEXFaktor.RootTable.Columns.Add("JamKol", ColumnType.Text)
        col.Caption = "مبلغ کل"
        col.DataMember = "JamKol"
        col.Width = 110
        col.EditType = EditType.NoEdit
        col.FormatString = "#,##0"
        col.TextAlignment = TextAlignment.Far

        ' ---- مانده فاکتور ----
        col = GridEXFaktor.RootTable.Columns.Add("MandehFaktor", ColumnType.Text)
        col.Caption = "مانده فاکتور"
        col.DataMember = "MandehFaktor"
        col.Width = 110
        col.EditType = EditType.NoEdit
        col.FormatString = "#,##0"
        col.TextAlignment = TextAlignment.Far
    End Sub
    Private _dtPardakhti As DataTable

    Private Sub btnTasvieh_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnTasvieh.Click
        If _codeAmalyat = 0 Then
            MsgBox("ابتدا دریافتی را ذخیره کنید.", MsgBoxStyle.Exclamation Or MsgBoxStyle.MsgBoxRtlReading, "پیام")
            Return
        End If

        ' رشته‌ی فاکتورهای تیک‌خورده
        Dim strFaktor As String = ""
        Dim cnt As Integer = 0
        Dim r As DataRow
        For Each r In _dtFaktor.Rows
            If Not IsDBNull(r("Taeed")) AndAlso CBool(r("Taeed")) Then
                If cnt > 0 Then strFaktor &= ","
                strFaktor &= r("ccFaktorTitr").ToString()
                cnt += 1
            End If
        Next

        If cnt = 0 Then
            MsgBox("حداقل یک فاکتور را تیک بزنید.", MsgBoxStyle.Exclamation Or MsgBoxStyle.MsgBoxRtlReading, "پیام")
            Return
        End If

        Dim oldFaktor As Integer = 0
        If chkOldFaktor.Checked Then oldFaktor = 1

        Try
            ' 1) تسویه
            Using cn As New SqlConnection(ConnectionString)
                Using cmd As New SqlCommand("[Treasury].[spVosolFaktor_DaryaftKoli_Tasvieh]", cn)   ' TODO: اسم SP
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.Parameters.AddWithValue("@CodeAmalyat", _codeAmalyat)
                    cmd.Parameters.AddWithValue("@StrFaktor", strFaktor)
                    cmd.Parameters.AddWithValue("@OldFaktor", oldFaktor)
                    cn.Open()
                    cmd.ExecuteNonQuery()
                End Using
            End Using

            ' 2) مانده‌ی دریافتی -> Label8
            LoadMandeh()

            ' 3) پرداختی‌های این دریافتی -> گرید پایین
            LoadPardakhti()

            ' 4) فاکتورهای مانده‌دار دوباره لود شوند
            LoadFaktorMandehDar()

        Catch ex As Exception
            MsgBox("خطا در تسویه: " & ex.Message, MsgBoxStyle.Critical Or MsgBoxStyle.MsgBoxRtlReading, "خطا")
        End Try
    End Sub
    Private Sub LoadMandeh()
        Dim dt As DataTable = ExecSP("[Treasury].[spVosolFaktor_DaryaftKoli_Mandeh]",
                                     New SqlParameter("@CodeAmalyat", _codeAmalyat))     ' TODO: اسم SP و پارامتر
        If dt.Rows.Count > 0 Then
            ' TODO: اسم ستون مانده در خروجی SP
            Label8.Text = CDbl(dt.Rows(0)("Mandeh")).ToString("#,##0")
        End If
    End Sub

    Private Sub LoadPardakhti()
        _dtPardakhti = ExecSP("[Treasury].[spVosolFaktor_DaryaftKoli_LoadPardakhti]",
                              New SqlParameter("@CodeAmalyat", _codeAmalyat))            ' TODO: اسم SP و پارامتر
        SetGridPardakhti()
        GridEXPardakhti.DataSource = _dtPardakhti
    End Sub
    Private Function ExecSP(ByVal spName As String, ByVal ParamArray p() As SqlParameter) As DataTable
        Dim dt As New DataTable()
        Using cn As New SqlConnection(ConnectionString)
            Using cmd As New SqlCommand(spName, cn)
                cmd.CommandType = CommandType.StoredProcedure
                Dim x As SqlParameter
                For Each x In p
                    cmd.Parameters.Add(x)
                Next
                Using da As New SqlDataAdapter(cmd)
                    da.Fill(dt)
                End Using
            End Using
        End Using
        Return dt
    End Function
    Private Sub SetGridPardakhti()
        Dim col As GridEXColumn

        With GridEXPardakhti
            .RootTable.Columns.Clear()
            .AllowEdit = InheritableBoolean.False
            .AllowAddNew = InheritableBoolean.False
            .AllowDelete = InheritableBoolean.False
            .RightToLeft = Windows.Forms.RightToLeft.Yes
            .GroupByBoxVisible = False
        End With

        ' ---- شماره فاکتور ----
        col = GridEXPardakhti.RootTable.Columns.Add("FaktorShomareh", ColumnType.Text)
        col.Caption = "شماره فاکتور"
        col.DataMember = "FaktorShomareh"
        col.Width = 100
        col.EditType = EditType.NoEdit

        ' ---- تاریخ فاکتور ----
        col = GridEXPardakhti.RootTable.Columns.Add("FaktorTarikh", ColumnType.Text)
        col.Caption = "تاریخ فاکتور"
        col.DataMember = "FaktorTarikh"
        col.Width = 90
        col.EditType = EditType.NoEdit

        ' ---- مبلغ کل فاکتور ----
        col = GridEXPardakhti.RootTable.Columns.Add("JamKol", ColumnType.Text)
        col.Caption = "مبلغ کل"
        col.DataMember = "JamKol"
        col.Width = 110
        col.EditType = EditType.NoEdit
        col.FormatString = "#,##0"
        col.TextAlignment = TextAlignment.Far

        ' ---- مبلغ بسته‌شده ----
        col = GridEXPardakhti.RootTable.Columns.Add("MablaghBastehShodeh", ColumnType.Text)
        col.Caption = "مبلغ بسته‌شده"
        col.DataMember = "MablaghBastehShodeh"
        col.Width = 110
        col.EditType = EditType.NoEdit
        col.FormatString = "#,##0"
        col.TextAlignment = TextAlignment.Far

        ' ---- نوع دریافتی ----
        col = GridEXPardakhti.RootTable.Columns.Add("NoeDaryafti", ColumnType.Text)
        col.Caption = "نوع دریافتی"
        col.DataMember = "NoeDaryafti"
        col.Width = 110
        col.EditType = EditType.NoEdit
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        ' اگه دریافتی ثبت شده و هنوز مبلغی از اون تخصیص داده نشده، هشدار بده
        If _codeAmalyat <> 0 AndAlso _mandehDaryafti > 0 Then
            Dim res As MsgBoxResult = MsgBox(
            "مبلغ " & _mandehDaryafti.ToString("#,##0") & " از این دریافتی هنوز به فاکتوری بسته نشده است." & vbCrLf &
            "آیا مایل به خروج هستید؟",
            MsgBoxStyle.YesNo Or MsgBoxStyle.Question Or MsgBoxStyle.MsgBoxRtlReading Or MsgBoxStyle.MsgBoxRight, "تایید")
            If res = MsgBoxResult.No Then Return
        End If

        ResetAll()
    End Sub

    Private Sub ResetAll()
        ' وضعیت دریافتی
        _codeAmalyat = 0
        _mandehDaryafti = 0
        Label8.Text = ""

        ' گریدها
        _dtFaktor = Nothing
        _dtPardakhti = Nothing
        GridEXFaktor.DataSource = Nothing
        GridEXPardakhti.DataSource = Nothing

        chkOldFaktor.Checked = False

        ' فعال کردن دوباره‌ی سمت راست
        GroupBox1.Enabled = True       ' TODO: اسم GroupBox سمت راست
        btnNewRecord.Enabled = True
        btnTasvieh.Enabled = False

        ClearForm()
    End Sub
End Class