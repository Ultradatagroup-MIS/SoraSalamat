Public Class frmRPT_Peigiry
#Region "Variable AND Constant Declration"
    Const cntCodeSubSystem As Long = 100099
    Private SN As Integer
    Public dsForm As New DataSet
    Dim dvForm As DataView
    Dim txtCaption As String
    Private WithEvents BS As New UD_Dll.PassString
    Dim ErrPro As New ErrorProvider
#End Region
#Region "Form Event Code"
    Private Sub rptMoshtaryAnalysis_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetParameter()
        SN = objSec.GetSecurityNumber(cntCodeSubSystem, UserName)
        mskAzTarikhRpt.Text = TarikhEmrooz
    End Sub
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
    Private Function IsValid(ByVal CheckField As String) As Boolean
        IsValid = False

        If CheckField = "mskTarikhPeigiryBady" Or CheckField = "All" Then
            If Me.mskAzTarikhRpt.Text = "" Then
                ErrPro.SetError(Me.mskAzTarikhRpt, "تاریخ پی گیری را وارد کنید.")
                MsgBox("تاریخ پی گیری را وارد کنید.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
                mskAzTarikhRpt.Focus()
                Exit Function
            End If
            ErrPro.SetError(Me.mskAzTarikhRpt, "")
        End If
        If mskAzTarikhRpt.Text.Length <> 0 Then
            If Not objTarikh.IsShDate(mskAzTarikhRpt.Text) Then
                ErrPro.SetError(Me.mskAzTarikhRpt, "تاریخ وارد شده صحیح نمی باشد .")
                Exit Function
            End If
        End If

        Return True
    End Function
#End Region
#Region "From Buttons"
    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub
    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        If IsValid("All") = False Then
            Exit Sub
        End If
        Print()
    End Sub
#End Region
#Region "Global Form Code"
    Private Sub Print()
        Dim strSQL As String
        Dim cnSQL As New SqlConnection
        Dim cmSQL As New SqlCommand
        Dim daSQL As New SqlDataAdapter
        Dim frm As New Forms_dll.frmGL_Gozaresh

        If dsForm.Tables.Contains("tbl_Report") Then
            dsForm.Tables.Remove("tbl_Report")
        End If

        Try
            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()

            strSQL = "Sales.spMoshtaryAnalysis_PeigiryFaktor_Print "

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            cmSQL.Parameters.AddWithValue("CodeMahal", CodeMahalFaal)
            cmSQL.Parameters.AddWithValue("CodeDoreh", CodeDoreh)
            cmSQL.Parameters.AddWithValue("Tarikh", mskAzTarikhRpt.Text)

            daSQL = New SqlDataAdapter(cmSQL)
            daSQL.Fill(dsForm, "tbl_Report")

            If dsForm.Tables("tbl_Report").Rows.Count = 0 Then
                MsgBox("هیـــــچ رکوردی برای گزارش پیدا نشد.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "پیام")
                Exit Sub
            End If

            Dim Rpt As New ReportDocument
            Dim RptTables As Tables
            Dim RptFormula As FormulaFieldDefinitions

            Rpt.Load(rptPath & "\rptFO_GozareshPeigiryFaktor.rpt")

            RptTables = Rpt.Database.Tables
            RptTables.Item(0).SetDataSource(dsForm.Tables("tbl_Report"))

            RptFormula = Rpt.DataDefinition.FormulaFields

            With RptFormula
                .Item("CodeMoshtary").Text = "{mydata.CodeMoshtary}"
                .Item("NameMoshtary").Text = "{mydata.NameMoshtary}"
                .Item("FaktorTarikhSlash").Text = "{mydata.FaktorTarikhSlash}"
                .Item("FaktorShomareh").Text = "{mydata.FaktorShomareh}"
                .Item("Modat").Text = "{mydata.Modat}"
                .Item("MablaghFaktor").Text = "{mydata.MablaghFaktor}"
                .Item("PardakhtiForFaktor").Text = "{mydata.PardakhtiForFaktor}"
                .Item("MandehFaktor").Text = "{mydata.MandehFaktor}"
                .Item("TarikhPeigiryNextSlash").Text = "{mydata.TarikhPeigiryNextSlash}"
                .Item("Title").Text = "'" & "گزارش پیگیری فاکتور" & "'"
                .Item("Title2").Text = "'" & NameSherkat & "'"
                .Item("Title3").Text = "'" & NameMahalFaal & "'"
                .Item("KarbarGozaresh").Text = "'" & PersonelName & "'"
                .Item("TarikhGozaresh").Text = "'" & objTarikh.SetDateSlash(TarikhEmrooz) & "'"
                .Item("SaatGozaresh").Text = "'" & Format(TimeOfDay, "HH:mm:ss") & "'"
            End With

            Rpt.Refresh()

            frm.Text = "گزارش پیگیری فاکتور"
            With frm.CRV
                .ReportSource = Rpt
                .DisplayGroupTree = False
                .ShowGroupTreeButton = False
                If Not objSec.HaveUserPermission(SN, UD_Dll.Security.ePermissions.Print) Then .ShowPrintButton = False
                If Not objSec.HaveUserPermission(SN, UD_Dll.Security.ePermissions.Export) Then .ShowExportButton = False
            End With

            Me.Hide()
            frm.ShowDialog(Me)
            frm = Nothing
            daSQL = Nothing
            Rpt = Nothing
            RptTables = Nothing
            RptFormula = Nothing

            cnSQL.Close()
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "Error IN DataBase ----> Print ")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, "Error IN ----> Print ")
        End Try
    End Sub
#End Region
End Class