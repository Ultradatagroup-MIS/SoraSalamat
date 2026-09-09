Public Class frmFO_GozareshPeygiriFaktor
#Region "Variable AND Constant Declration"
    Const cntCodeSubSystem As Long = 1000105
    Dim Mode As UD_Dll.Enums.GL_ModeForms = UD_Dll.Enums.GL_ModeForms.None
    Private SN As Integer
    Public dsForm As New DataSet
    Dim dvForm As DataView
    Dim txtCaption As String
    Private WithEvents BS As New UD_Dll.PassString
    Public dvTitr As DataView
    Public cmTitr
    Dim AzShomarehPeygiri As Integer = 0
    Dim TaShomarehPeygiri As Integer = 0
    Dim AzShomarehFaktor As Integer = 0
    Dim TaShomarehFaktor As Integer = 0
    Dim ErrPro As New ErrorProvider
#End Region
#Region "Form Event Code"
    Private Sub frmFO_GozareshPeygiriFaktor_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetParameter()
        SN = objSec.GetSecurityNumber(cntCodeSubSystem, UserName)
        mskAzTarikh.Text = TarikhEmrooz
        mskTaTarikh.Text = TarikhEmrooz
        LoadCombo()
        cmbMamorPakhshS.SelectedValue = 0
        cmbVazeiatS.SelectedIndex = 1
        cmbVazeiatS.SelectedIndex = 1
        cmbDoreh.SelectedValue = 0
    End Sub
    Private Sub txtCodeMoshtaryS_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCodeMoshtaryS.KeyPress
        Try
            If (Asc(e.KeyChar()) < 48 Or Asc(e.KeyChar()) > 57) And (Asc(e.KeyChar()) <> 8) Then
                e.Handled = True
            End If
            If e.KeyChar = Chr(Keys.Space) Then
                Dim objMoshtary As New Forms_dll.frmFO_MoshtarySearch
                Dim StrSql As String

                StrSql = "Select * from qryFO_Moshtary Where CodeMahal=" & CodeMahalFaal & " AND sVazeiat = 3980 "
                StrSql &= " AND Not exists (Select * From tblGL_SecurityData where namekarbar= '" & UserName & "' and CodeSubSystem = 614 and pk = qryFO_Moshtary.ccMoshtary) order by NameMoshtary"

                'tCodeMoshtary = ""
                'tNameMoshtary = ""
                'tccMoshtary = ""
                'SearchItem = "CodeMoshtary"
                'If txtCodeMoshtaryS.Text.Length <> 0 Then
                '    tCodeMoshtary = txtCodeMoshtaryS.Text
                'End If
                objMoshtary.MultiSelection = False
                objMoshtary.SetForm(StrSql)
                objMoshtary.ShowDialog()
                txtCodeMoshtaryS.Tag = IIf(IsNothing(objMoshtary.tccMoshtary), 0, objMoshtary.tccMoshtary)
                txtCodeMoshtaryS.Text = IIf(IsNothing(objMoshtary.tCodeMoshtary), "", objMoshtary.tCodeMoshtary)
                lblNameMoshtaryS.Text = IIf(IsNothing(objMoshtary.tNameMoshtary), "", objMoshtary.tNameMoshtary)

            End If
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> txtCodeMoshtaryS_KeyPress ")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> txtCodeMoshtaryS_KeyPress ")
        End Try
    End Sub
    Private Sub txtCodeMoshtaryS_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCodeMoshtaryS.TextChanged
        Try
            If txtCodeMoshtaryS.Text = "" Then
                Exit Sub
            End If

            Dim Criteria As String = ""

            Criteria = "CodeMahal = " & CodeMahalFaal
            Criteria &= " AND NOT EXISTS (SELECT * FROM tblGL_SecurityData WHERE NameKarbar = '" & UserName & "' AND CodeSubSystem = 614 AND pk = tblFO_Moshtary.ccMoshtary) "
            Criteria &= " AND CodeMoshtary = '" & IIf(IsNothing(Me.txtCodeMoshtaryS.Text), 0, Me.txtCodeMoshtaryS.Text) & "'"
            Criteria &= " AND sVazeiat = " & UD_Dll.Enums.FO_VaziatMoshtary.Faal
            lblNameMoshtaryS.Text = objTools.ConvertNulls(objTools.DLookup("NameMoshtary", "tblFO_Moshtary", Criteria), "")
            txtCodeMoshtaryS.Tag = objTools.ConvertNulls(objTools.DLookup("ccMoshtary", "tblFO_Moshtary", Criteria), "")

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> txtCodeMoshtaryS_TextChanged ")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> txtCodeMoshtaryS_TextChanged ")
        End Try
    End Sub
#End Region
#Region "Global Form Code"
    Private Sub SetParameter()

        Dim commands As String = Microsoft.VisualBasic.Command()
        If commands.Length = 0 Then

            UserName = "administrator"
            UserPassWord = "66998833"
            NameMahalFaal = "تهران"
            CodeMahalFaal = "1"
            PersonelCode = "0"
            PersonelName = "Administrator"
            CodeDoreh = "1392"
        Else

            UserName = commands.Split(";")(0)
            UserCode = commands.Split(";")(1)
            UserPassWord = commands.Split(";")(2)
            NameMahalFaal = commands.Split(";")(3)
            CodeMahalFaal = commands.Split(";")(4)
            PersonelCode = commands.Split(";")(5)
            PersonelName = commands.Split(";")(6)
            CodeDoreh = commands.Split(";")(7)
            txtCaption = commands.Split(";")(8)
            Me.Text = txtCaption
            objCode.UserName = UserName

        End If

    End Sub
    Private Sub LoadCombo()
        Dim strSQL As String
        Dim cnSQL As New SqlConnection
        Dim cmSQL As New SqlCommand
        Dim daSQL As SqlDataAdapter
        Dim dr As DataRow

        Try

            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()

            strSQL = "Global.spMamorPakhsh_LoadCombo "

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            cmSQL.Parameters.AddWithValue("CodeMahal", CodeMahalFaal)
            cmSQL.Parameters.AddWithValue("sSemat", "," & UD_Dll.Enums.GL_Semat.MamorPakhsh & "," & UD_Dll.Enums.GL_Semat.Ranandeh & "," & UD_Dll.Enums.GL_Semat.Foroshandeh_Sayar & "," & UD_Dll.Enums.GL_Semat.Foroshandeh & "," & 4843 & ",") '-- 4843 تحصیل دار

            daSQL = New SqlDataAdapter(cmSQL)
            daSQL.Fill(dsForm, "tbl_MamorPakhshS")
            dr = dsForm.Tables("tbl_MamorPakhshS").NewRow
            dr("FN") = "همه"
            dr("CodeFard") = 0
            dsForm.Tables("tbl_MamorPakhshS").Rows.Add(dr)
            cmbMamorPakhshS.DataSource = Nothing
            cmbMamorPakhshS.Items.Clear()
            cmbMamorPakhshS.DataSource = dsForm.Tables("tbl_MamorPakhshS").DefaultView
            cmbMamorPakhshS.DisplayMember = "FN"
            cmbMamorPakhshS.ValueMember = "CodeFard"

            ' --------------------------------

            strSQL = "Global.spCodeDoreh_LoadCombo "

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            daSQL = New SqlDataAdapter(cmSQL)
            daSQL.Fill(dsForm, "tbl_Doreh")
            dr = dsForm.Tables("tbl_Doreh").NewRow
            dr("txtDoreh") = "همـــه"
            dr("Codedoreh") = 0
            dsForm.Tables("tbl_Doreh").Rows.Add(dr)

            cmbDoreh.DataSource = Nothing
            cmbDoreh.Items.Clear()
            cmbDoreh.DataSource = dsForm.Tables("tbl_Doreh").DefaultView
            cmbDoreh.DisplayMember = "txtDoreh"
            cmbDoreh.ValueMember = "CodeDoreh"

            ' --------------------------------

            cmbVazeiatS.Items.Add("همـــه")
            cmbVazeiatS.Items.Add("بدون وضعیت")
            cmbVazeiatS.Items.Add("ارسال شده")
            cmbVazeiatS.Items.Add("تایید شده")

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> LoadCombo")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> LoadCombo")
        End Try
    End Sub
    Private Sub Print()
        Try
            Dim cnSQL As SqlConnection
            Dim cmSQL As New SqlCommand
            Dim strSQL As String
            Dim strPeygiri As String = ","

            If dsForm.Tables.Contains("tbl_GozareshPeygiriFaktor") Then
                dsForm.Tables.Remove("tbl_GozareshPeygiriFaktor")
            End If

            If Not IsValidReport("All") Then
                Exit Sub
            End If

            '---------------------------------------------

            If mskAzShomarehPeygiri.Text.Trim = "" Then
                AzShomarehPeygiri = objTools.ConvertNulls(objTools.DLookupOne("ShomarehPeygiri", "Sales.PeygiriFaktor", "CodeMahal = " & CodeMahalFaal & " AND CodeDoreh = " & CodeDoreh, "ShomarehPeygiri ASC"), 0)
            Else
                AzShomarehPeygiri = Val(mskAzShomarehPeygiri.Text.Trim)
            End If

            If mskTaShomarehPeygiri.Text.Trim = "" Then
                TaShomarehPeygiri = objTools.ConvertNulls(objTools.DLookupOne("ShomarehPeygiri", "Sales.PeygiriFaktor", "CodeMahal = " & CodeMahalFaal & " AND CodeDoreh = " & CodeDoreh, "ShomarehPeygiri DESC"), 0)
            Else
                TaShomarehPeygiri = Val(mskTaShomarehPeygiri.Text.Trim)
            End If

            '---------------------------------------------

            If mskFaktorShomarehAz.Text.Trim = "" Then
                If cmbDoreh.SelectedValue = 0 Then
                    AzShomarehFaktor = objTools.ConvertNulls(objTools.DLookupOne("FaktorShomareh", "tblFO_Faktor", "CodeMahal = " & CodeMahalFaal, "FaktorShomareh ASC"), 0)
                Else
                    AzShomarehFaktor = objTools.ConvertNulls(objTools.DLookupOne("FaktorShomareh", "tblFO_Faktor", "CodeMahal = " & CodeMahalFaal & " AND CodeDoreh = " & cmbDoreh.SelectedValue, "FaktorShomareh ASC"), 0)
                End If
            Else
                AzShomarehFaktor = Val(mskFaktorShomarehAz.Text.Trim)
            End If

            If mskFaktorShomarehTa.Text.Trim = "" Then
                If cmbDoreh.SelectedValue = 0 Then
                    TaShomarehFaktor = objTools.ConvertNulls(objTools.DLookupOne("FaktorShomareh", "tblFO_Faktor", "CodeMahal = " & CodeMahalFaal, "FaktorShomareh DESC"), 0)
                Else
                    TaShomarehFaktor = objTools.ConvertNulls(objTools.DLookupOne("FaktorShomareh", "tblFO_Faktor", "CodeMahal = " & CodeMahalFaal & " AND CodeDoreh = " & cmbDoreh.SelectedValue, "FaktorShomareh DESC"), 0)
                End If
            Else
                TaShomarehFaktor = Val(mskFaktorShomarehTa.Text.Trim)
            End If

            '---------------------------------------------

            strSQL = "Report.spPeygiriFaktor_Print "

            Windows.Forms.Cursor.Current = Cursors.WaitCursor
            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            cmSQL.Parameters.AddWithValue("CodeMahal", CodeMahalFaal)
            cmSQL.Parameters.AddWithValue("CodeDoreh", CodeDoreh)
            cmSQL.Parameters.AddWithValue("CodeDorehFaktor", cmbDoreh.SelectedValue)
            cmSQL.Parameters.AddWithValue("CodeFard_MamorPakhsh", cmbMamorPakhshS.SelectedValue)
            cmSQL.Parameters.AddWithValue("Vazeiat", cmbVazeiatS.SelectedIndex)
            cmSQL.Parameters.AddWithValue("AzTarikhPeygiri", mskAzTarikh.Text)
            cmSQL.Parameters.AddWithValue("TaTarikhPeygiri", mskTaTarikh.Text)
            cmSQL.Parameters.AddWithValue("AzShomarehPeygiri", AzShomarehPeygiri)
            cmSQL.Parameters.AddWithValue("TaShomarehPeygiri", TaShomarehPeygiri)
            cmSQL.Parameters.AddWithValue("AzShomarehFaktor", AzShomarehFaktor)
            cmSQL.Parameters.AddWithValue("TaShomarehFaktor", TaShomarehFaktor)
            cmSQL.Parameters.AddWithValue("ccMoshtary", IIf(txtCodeMoshtaryS.Text.Trim = "", 0, txtCodeMoshtaryS.Tag))

            Dim daSQL As SqlDataAdapter
            daSQL = New SqlDataAdapter(cmSQL)
            daSQL.Fill(dsForm, "tbl_GozareshPeygiriFaktor")

            If dsForm.Tables("tbl_GozareshPeygiriFaktor").Rows.Count = 0 Then
                MsgBox("هیـــــچ رکوردی برای گزارش پیدا نشد.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "پیام")
                Exit Sub
            End If

            Dim rpt As New CrystalDecisions.CrystalReports.Engine.ReportDocument
            Dim rpttables As CrystalDecisions.CrystalReports.Engine.Tables
            Dim rptformula As CrystalDecisions.CrystalReports.Engine.FormulaFieldDefinitions
            Dim frm As New Forms_dll.frmGL_Gozaresh

            rpt.Load(rptPath & "\rptFO_GozareshPeigiryFaktor_Print.rpt")

            rpttables = rpt.Database.Tables
            rpttables.Item(0).SetDataSource(dsForm.Tables("tbl_GozareshPeygiriFaktor"))

            rptformula = rpt.DataDefinition.FormulaFields
            With rptformula

                .Item("FaktorShomareh").Text = "{mydata.ShomarehFaktor}"
                .Item("TarikhFaktor").Text = "{mydata.TarikhFaktor}"
                .Item("ShomarehPeygiri").Text = "{mydata.ShomarehPeygiri}"
                .Item("TarikhPeygiri").Text = "{mydata.TarikhPeygiri}"
                .Item("NameMoshtary").Text = "{mydata.NameMoshtary}"
                .Item("NameMamorPakhsh").Text = "{mydata.NameMamorPakhsh}"
                .Item("CodeMoshtary").Text = "{mydata.CodeMoshtary}"
                .Item("MablaghFaktor").Text = "{mydata.MablaghFaktor}"
                .Item("MablaghDaryafti").Text = "{mydata.MablaghDaryafti}"
                .Item("MandehFaktor").Text = "{mydata.MandehFaktor}"
                .Item("Tozihat").Text = "{mydata.Tozihat}"
                .Item("NatijehPeygiri").Text = "{mydata.NatijehPeygiri}"
                .Item("Title").Text = "'" & "پیگیــری فاکتــور" & "'"
                .Item("Title2").Text = "'" & NameSherkat & "'"
                .Item("Title3").Text = "'" & NameMahalFaal & "'"
                .Item("KarbarGozaresh").Text = "'" & PersonelName & "'"
                .Item("TarikhGozaresh").Text = "'" & objTarikh.SetDateSlash(TarikhEmrooz) & "'"
                .Item("SaatGozaresh").Text = "'" & Format(TimeOfDay, "HH:mm:ss") & "'"

            End With
            rpt.Refresh()

            frm.Text = txtCaption

            With frm.CRV
                .ReportSource = rpt
                .DisplayGroupTree = False
                .ShowGroupTreeButton = False
                .Zoom(100)
                If Not objSec.HaveUserPermission(SN, UD_Dll.Security.ePermissions.Print) Then .ShowPrintButton = False
                If Not objSec.HaveUserPermission(SN, UD_Dll.Security.ePermissions.Export) Then .ShowExportButton = False
            End With


            Me.Hide()
            frm.ShowDialog(Me)
            frm = Nothing
            daSQL = Nothing
            rpt = Nothing
            Me.Show()
            Windows.Forms.Cursor.Current = Cursors.Default
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> Print ")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> Print ")
        End Try
    End Sub
    Private Function IsValidReport(ByVal chkField As String) As Boolean
        IsValidReport = False

        Try
            If chkField = "mskAzTarikh" Or chkField = "All" Then
                If Len(mskAzTarikh.Text.ToString) <> 0 Then
                    If Not objTarikh.IsShDate(mskAzTarikh.Text.ToString) Then
                        mskAzTarikh.Focus()
                        Exit Function
                    End If
                Else
                    ErrPro.SetError(Me.mskAzTarikh, "از تاریخ را وارد کنید.")
                    MsgBox("از تاریخ را وارد کنید.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
                    mskAzTarikh.Focus()
                    Exit Function
                End If
                ErrPro.SetError(Me.mskAzTarikh, "")
            End If

            If chkField = "mskTaTarikh" Or chkField = "All" Then
                If Len(mskTaTarikh.Text.ToString) <> 0 Then
                    If Not objTarikh.IsShDate(mskTaTarikh.Text.ToString) Then
                        mskTaTarikh.Focus()
                        Exit Function
                    End If
                Else
                    ErrPro.SetError(Me.mskTaTarikh, "تا تاریخ را وارد کنید.")
                    MsgBox("تا تاریخ را وارد کنید.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
                    mskTaTarikh.Focus()
                    Exit Function
                End If
                ErrPro.SetError(Me.mskTaTarikh, "")
            End If

            If Val(mskTaTarikh.Text) < Val(mskAzTarikh.Text) Then
                MsgBox("بازه تاریخی وارد شده نا معتبر است . در وارد نمودن تاریخ ها دقت نمایید .", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخیره")
                Exit Function
            End If

            'If chkField = "txtTaShomarehFaktor" Or chkField = "All" Then
            '    If Len(mskFaktorShomarehTa.Text.ToString) = 0 Then
            '        If MsgBox("قسمت « تا شماره فاکتور » پر نشده است،  آیا می خواهید آن را وارد کنید؟", MsgBoxStyle.YesNo + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخیره") = MsgBoxResult.Yes Then
            '            Me.mskFaktorShomarehTa.Focus()
            '            Exit Function
            '        End If
            '    End If
            'End If

            If mskAzShomarehPeygiri.Text <> "" And mskTaShomarehPeygiri.Text <> "" Then
                If Val(mskAzShomarehPeygiri.Text) > Val(mskTaShomarehPeygiri.Text) Then
                    MsgBox("بازه وارد شده برای شماره پیگیری نا معتبر است . لطفاَ اصلاح نمایید .", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخیره")
                    Exit Function
                End If
            End If

            If mskFaktorShomarehAz.Text <> "" And mskFaktorShomarehTa.Text <> "" Then
                If Val(mskFaktorShomarehAz.Text) > Val(mskFaktorShomarehTa.Text) Then
                    MsgBox("بازه وارد شده برای شماره فاکتور نا معتبر است . لطفاَ اصلاح نمایید .", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخیره")
                    Exit Function
                End If
            End If

            Return True
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> IsValidReport ")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> IsValidReport ")
        End Try
    End Function
#End Region
#Region "From Buttons "
    Private Sub btnView_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnView.Click
        If Not objSec.HaveUserPermission(SN, UD_Dll.Security.ePermissions.Preview) Then Exit Sub
        Try
            Me.TopMost = False
            Print()
            Me.TopMost = True
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> btnView_Click ")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> btnView_Click ")
        End Try
    End Sub
    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub
#End Region
End Class