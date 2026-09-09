Public Class frmFO_GozareshAnbarForosh

#Region "Variable AND Constant Declration"
    ' Quary And Table Names
    Const FormViewName = "qryFO_PishFaktorTitrSatr"
    ' Security 
    Const cntCodeSubSystem As Long = 1000116
    Private SN As Integer

    Dim ErrPro As New ErrorProvider
    Dim dsForm As New DataSet
    Dim dr As DataRow
    Dim dvForm As DataView
    Dim txtCaption As String

    Private WithEvents BS As New UD_Dll.PassString
#End Region
    Private Sub frmFO_GozareshAnbarForosh_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetParameter()
        SN = objSec.GetSecurityNumber(cntCodeSubSystem, UserName)
        objTools.ChangeKbd(UD_Dll.mdlUtility.KBD.Farsi)
        mskAzTarikh.Text = TarikhEmrooz.Substring(0, 6) + "01"
        mskTaTarikh.Text = TarikhEmrooz
        LoadCombo()
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
            ObjCode.UserName = UserName
        End If

    End Sub
    Private Sub LoadCombo()
        Dim strSQL As String
        Dim cnSQL As SqlConnection
        Dim cmSQL As SqlCommand
        Dim daSQL As SqlDataAdapter
        Dim dr As DataRow
        Try
            strSQL = "Global.spAnbar_ForoshSard_LoadCombo "

            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            cmSQL.Parameters.AddWithValue("CodeMahal", CodeMahalFaal)

            daSQL = New SqlDataAdapter(cmSQL)
            daSQL.Fill(dsForm, "tbl_Anbar")

            cmbAnbar.DataSource = Nothing
            cmbAnbar.Items.Clear()
            cmbAnbar.DataSource = dsForm.Tables("tbl_Anbar").DefaultView
            cmbAnbar.DisplayMember = "NameAnbar"
            cmbAnbar.ValueMember = "CodeAnbar"

            daSQL = Nothing
            cmSQL = Nothing
            cnSQL.Close()

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->LoadCombo")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->LoadCombo")
        End Try
    End Sub
    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub btnReport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReport.Click
        If Not objSec.HaveUserPermission(SN, UD_Dll.Security.ePermissions.Preview) Then Exit Sub
        Try
            Me.TopMost = False
            SetReport()
            Me.TopMost = True
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->btnPrintM_Click")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->btnPrintM_Click")
        End Try
    End Sub
    Private Sub SetReport()
        Try
            Dim cnSQL As SqlConnection
            Dim cmSQL As SqlCommand
            Dim strSQL As String = ""
            Dim daSQL As SqlDataAdapter
            Dim Str As String = ""
            Dim strMahaleh As String = ""
            Dim strMasir As String = ""

            If Not IsValidField("All") Then
                Exit Sub
            End If

            Windows.Forms.Cursor.Current = Cursors.WaitCursor

            If dsForm.Tables.Contains("tblGozaresh") Then
                dsForm.Tables.Remove("tblGozaresh")
            End If

            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()

            strSQL = "Report.spGozareshAnbar_Forosh_AfraShir "

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            cmSQL.Parameters.AddWithValue("ccAnbar", cmbAnbar.SelectedValue)
            cmSQL.Parameters.AddWithValue("CodeDoreh", CodeDoreh)
            cmSQL.Parameters.AddWithValue("CodeMahal", CodeMahalFaal)
            cmSQL.Parameters.AddWithValue("AzTarikh", mskAzTarikh.Text)
            cmSQL.Parameters.AddWithValue("TaTarikh", mskTaTarikh.Text)

            daSQL = New SqlDataAdapter(cmSQL)
            daSQL.SelectCommand.CommandTimeout = 99999
            daSQL.Fill(dsForm, "tblGozaresh")

            If dsForm.Tables("tblGozaresh").Rows.Count = 0 Then
                MsgBox("هیـــــچ رکوردی برای گزارش پیدا نشد.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "پیام")
                Exit Sub
            End If

            Dim rpt As New CrystalDecisions.CrystalReports.Engine.ReportDocument
            Dim rpttables As CrystalDecisions.CrystalReports.Engine.Tables
            Dim rptformula As CrystalDecisions.CrystalReports.Engine.FormulaFieldDefinitions
            Dim frm As New Forms_dll.frmGL_Gozaresh

            rpt.Load(rptPath & "\rptFO_GozareshAnbar_Forosh_AfraShir.rpt")

            rpttables = rpt.Database.Tables
            rpttables.Item(0).SetDataSource(dsForm.Tables("tblGozaresh"))

            rptformula = rpt.DataDefinition.FormulaFields
            With rptformula

                .Item("Group_Sanad").Text = "{mydata.NameAnbar}"
                .Item("NameAnbar").Text = "{mydata.NameAnbar}"
                .Item("AzTarikh").Text = "{mydata.AzTarikh}"
                .Item("TaTarikh").Text = "{mydata.TaTarikh}"
                .Item("CodeKala").Text = "{mydata.CodeKala}"
                .Item("NameKala").Text = "{mydata.NameKala}"
                .Item("ResidAzTaminKonandeh").Text = "{mydata.ResidAzTaminKonandeh}"
                .Item("ResidAvaleDoreh").Text = "{mydata.ResidAvaleDoreh}"
                .Item("VorodAzAnbarGharantineh").Text = "{mydata.VorodAzAnbarGharantineh}"
                .Item("KhorojBeAnbarGharantineh").Text = "{mydata.KhorojBeAnbarGharantineh}"
                .Item("VorodAzAnbarZayeat").Text = "{mydata.VorodAzAnbarZayeat}"
                .Item("KhorojBeAnbarZayeat").Text = "{mydata.KhorojBeAnbarZayeat}"
                .Item("KasrAzAnbar").Text = "{mydata.KasrAzAnbar}"
                .Item("EzafehBeAnbar").Text = "{mydata.EzafehBeAnbar}"
                .Item("ForoshSard").Text = "{mydata.ForoshSard}"
                .Item("ForoshGarm").Text = "{mydata.ForoshGarm}"
                .Item("MarjoeeSalem").Text = "{mydata.MarjoeeSalem}"
                .Item("MarjoeeKharab").Text = "{mydata.MarjoeeKharab}"
                .Item("MarjoeeMoredDar").Text = "{mydata.MarjoeeMoredDar}"
                .Item("HavalehBeAnbarak").Text = "{mydata.HavalehBeAnbarak}"
                .Item("MojodiAnbarakHa").Text = "{mydata.MojodiAnbarakHa}"
                .Item("Title").Text = "' گـزارش انبـار - فـروش '"
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
                .DisplayGroupTree = True
                .ShowGroupTreeButton = True
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
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> SetReport")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> SetReport")
        End Try
    End Sub
    Private Function IsValidField(ByVal chkField As String) As Boolean
        Try
            IsValidField = False

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

            Return True

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->IsValidRow")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->IsValidRow")
        End Try
    End Function
End Class