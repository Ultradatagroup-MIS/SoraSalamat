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

    Private Sub btnNewRecord_Click(sender As Object, e As EventArgs) Handles btnNewRecord.Click
        If Not IsValidForm("All") Then Exit Sub
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
            'If CodeDoreh <> objTarikh.ShamsiYear(mskTarikhDP.Text) Then
            '    ErrPro.SetError(Me.mskTarikhDP, "تاريخ دریافت با دوره يکی نيست.")
            '    MsgBox("تاريخ دریافت با دوره يکی نيست.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
            '    mskTarikhDP.Focus()
            '    Exit Function
            'End If
            'If objTarikh.ShamsiYear(mskTarikhDP.Text) <> DorehFaktor_Varizi Then
            '    ErrPro.SetError(Me.mskTarikhDP, "تاريخ دریافت بايد در سال ورود اطلاعات باشد.")
            '    MsgBox("تاريخ دریافت بايد در سال ورود اطلاعات باشد.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
            '    mskTarikhDP.Focus()
            '    Exit Function
            'End If
            If mskTarikhDP.Text > TarikhEmrooz Then
                ErrPro.SetError(Me.mskTarikhDP, "تاريخ دریافت نقدی نمی تواند جلوتر از تاریخ روز باشد.")
                MsgBox("تاريخ دریافت نقدی نمی تواند جلوتر از تاریخ روز باشد.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
                mskTarikhDP.Focus()
                Exit Function
            End If
            ErrPro.SetError(Me.mskTarikhDP, "")
        End If
        If CheckField = "mskTarikhSanad" Or CheckField = "All" Then
            If mskTarikhSanad.Text.Length <> 0 And mskTarikhSanad.Text < mskTarikhDP.Text Then
                ErrPro.SetError(mskTarikhSanad, "تاریخ حواله نمیتواند پیش از تاریخ دریافت باشد.")
                MsgBox("تاریخ حواله نمیتواند پیش از تاریخ دریافت باشد.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
                mskTarikhSanad.Focus()
                Exit Function
            End If
            ErrPro.SetError(Me.mskTarikhSanad, "")
        End If

        If CheckField = "txtMablagh" Or CheckField = "All" Then
            If txtMablagh.Text.Length = 0 Then
                ErrPro.SetError(txtMablagh, "مبلغ را وارد کنيد")
                MsgBox("مبلغ را وارد کنيد", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
                txtMablagh.Focus()
                Exit Function
            End If
            ErrPro.SetError(Me.txtMablagh, "")
        End If

        If Val(txtMablagh.Text) < Val(txtMablaghPFaktor.Text) Then
            ErrPro.SetError(txtMablaghPFaktor, "مبلغ پرداختی برای این فاکتور نمی تواند از کل وجه دریافتی بیشتر باشد.")
            MsgBox("مبلغ پرداختی برای این فاکتور نمی تواند از کل وجه دریافتی بیشتر باشد.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
            txtMablaghPFaktor.Focus()
            Exit Function
        End If
        ErrPro.SetError(Me.txtMablaghPFaktor, "")

        If Val(txtMablaghPFaktor.Text) = 0 Then
            ErrPro.SetError(txtMablaghPFaktor, "مانده فاکتور 0 است .")
            MsgBox("مانده فاکتور 0 است.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
            txtMablaghPFaktor.Focus()
            Exit Function
        End If
        ErrPro.SetError(Me.txtMablaghPFaktor, "")

        Dim MandehFak As Double = (Val(objTools.DLookup("JamKol", "tblFO_Faktor", "ccFaktorTitr=" & ccFaktorTitr)) - Val(objTools.ConvertNulls(objTools.DSum("Mablagh", "qryDP_AmalyatFaktor", "ccFaktorTitr=" & ccFaktorTitr & " AND Ebtal=0"), 0)))
        If Val(txtMablaghPFaktor.Text) > MandehFak Then
            ErrPro.SetError(txtMablaghPFaktor, "مبلغ پرداختی برای این فاکتور نباید از " & MandehFak & " بیشتر باشد.")
            MsgBox("مبلغ پرداختی برای این فاکتور نباید از " & MandehFak & " بیشتر باشد.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
            txtMablaghPFaktor.Focus()
            Exit Function
        End If
        ErrPro.SetError(Me.txtMablaghPFaktor, "")


        If CheckField = "txtMablaghPFaktor" Or CheckField = "All" Then
            If txtMablaghPFaktor.Text.Length = 0 Then
                ErrPro.SetError(txtMablaghPFaktor, "مبلغ پرداختی برای این فاکتور را وارد کنید.")
                MsgBox("مبلغ پرداختی برای این فاکتور را وارد کنید.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
                txtMablaghPFaktor.Focus()
                Exit Function
            End If
            ErrPro.SetError(Me.txtMablaghPFaktor, "")
        End If

        Return True
    End Function

    Private Sub frmVosolKoli_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadCombo()
        'ClearForm(False)
        objTools.ChangeKbd(UD_Dll.mdlUtility.KBD.Latin)
    End Sub
    Private Sub LoadCombo()
        Dim cmd As New SqlCommand()
        Dim cnSQL As SqlConnection
        Dim daSQL As SqlDataAdapter
        Dim Strsql As String
        Dim cmsql As SqlCommand

        cnSQL = New SqlConnection(ConnectionString)
        cmd.Connection = cnSQL
        cmd.CommandType = CommandType.StoredProcedure
        cmd.CommandText = "sp_DP_Select_ShomarehHesab"
        cmd.Parameters.Add("@CodeMahal", SqlDbType.Int).Value = CodeMahalFaal

        daSQL = New SqlDataAdapter(cmd)
        daSQL = New SqlDataAdapter(cmd)
        daSQL.Fill(dsForm, "tblBankMaghsad")
        cmbShomarehHesab.DataSource = Nothing
        cmbShomarehHesab.Items.Clear()
        cmbShomarehHesab.DataSource = dsForm.Tables("tblBankMaghsad").DefaultView
        cmbShomarehHesab.DisplayMember = "ShomarehHesabKamel"
        cmbShomarehHesab.ValueMember = "ccShomarehHesab"
        cmbShomarehHesab.SelectedIndex = -1

        Strsql = "Select * From tblGL_ShenasehOmomi Where CodeAsli = 69 and CodeFarei <> 0 Or Code = 0 order by Sharh"
        daSQL = New SqlDataAdapter(Strsql, ConnectionString)
        daSQL.Fill(dsForm, "ComboNoeCheck")
        cmbNoe2.DataSource = Nothing
        cmbNoe2.Items.Clear()
        cmbNoe2.DataSource = dsForm.Tables("ComboNoeCheck").DefaultView
        cmbNoe2.DisplayMember = "Sharh"
        cmbNoe2.ValueMember = "Code"

        cnSQL = New SqlConnection(ConnectionString)
        cnSQL.Open()

        If dsForm.Tables("tblBankMaghsad").DefaultView.Count = 0 Then
            MsgBox("برای استفاده از این فرم ابتدا باید شماره حساب های شرکت را تعریف کنید.", MsgBoxStyle.Information Or MsgBoxStyle.MsgBoxRtlReading, "پیام")
            Me.Close()
        Else
            cmbShomarehHesab.SelectedIndex = 0
            tShHEntry = cmbShomarehHesab.SelectedValue
        End If

        Strsql = "Select * From qryGL_MoshakhasatFardi Where CodeMahal = " & CodeMahalFaal & " AND sVazeiatEstekhdam = 4188  order by FN"
        daSQL = New SqlDataAdapter(Strsql, ConnectionString)
        daSQL.Fill(dsForm, "tblFard")
        cmbFard.DataSource = Nothing
        cmbFard.Items.Clear()
        cmbFard.DataSource = dsForm.Tables("tblFard").DefaultView
        cmbFard.DisplayMember = "FN"
        cmbFard.ValueMember = "CodeFard"
        cmbFard.SelectedIndex = -1

        cnSQL = New SqlConnection(ConnectionString)
        cnSQL.Open()

        Strsql = "Global.spCheckBargashtiJaygozin"

        cmsql = New SqlCommand(Strsql, cnSQL)
        cmsql.CommandType = CommandType.StoredProcedure
        cmsql.Parameters.Clear()

        cmsql.Parameters.AddWithValue("CodeMahal", CodeMahalFaal)

        daSQL = New SqlDataAdapter(cmsql)
        daSQL.Fill(dsForm, "tblBargashti")
        cmbCheckBargashti.DataSource = Nothing
        cmbCheckBargashti.Items.Clear()
        cmbCheckBargashti.DataSource = dsForm.Tables("tblBargashti").DefaultView
        cmbCheckBargashti.DisplayMember = "Sharh"
        cmbCheckBargashti.ValueMember = "CodeAmalyat"
        cmbCheckBargashti.SelectedIndex = -1
        cmbCheckBargashti.SelectedIndex = -1

        cmsql = Nothing : daSQL = Nothing
        cnSQL.Close()

    End Sub
End Class