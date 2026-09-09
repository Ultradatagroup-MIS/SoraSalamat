Public Class frmFO_PrintTafkikJozSatrKoli

#Region "Variable AND Constant Declration"
    Dim cmTitr As CurrencyManager
    Dim cmSatr As CurrencyManager
    Dim dvTitr As DataView
    Dim dvSatr As DataView
    Dim dsForm As New DataSet
    Dim dvForm As DataView
    Dim txtCaption As String
    Private WithEvents BS As New UD_Dll.PassString
    Dim posColSelect As Byte = 0 ' Position Of Select Col
    Public ccTafkikJoze As Integer
    Public ShomarehTafkik As String
    Public Ranandeh As String
#End Region
    Private Sub PrintTafkikJozSatrKoli_Closed(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Closed
        dsForm = Nothing
        dvForm = Nothing
    End Sub
    Private Sub PrintTafkikJozSatrKoli_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        LoadCombo()

        ShomarehTafkik = objTools.DLookup("ShomarehTafkik", "tblFO_TafkikJoze", "ccTafkikJoze = " & ccTafkikJoze)
        Ranandeh = objTools.DLookup("RanandehHaml", "qryFO_TafkikJoze", "ccTafkikJoze = " & ccTafkikJoze)

        lblShomarehTafkik.Text = ShomarehTafkik
        lblRanandeh.Text = Ranandeh

    End Sub
    Private Sub LoadCombo()
        Dim Strsql As String
        Dim daSQL As SqlDataAdapter

        Strsql = "SELECT Code, Sharh FROM tblGL_ShenasehOmomi WHERE CodeAsli = 200 AND CodeFarei <> 0"
        daSQL = New SqlDataAdapter(Strsql, ConnectionString)
        daSQL.Fill(dsForm, "tblsG1")
        chksG1.DataSource = Nothing
        chksG1.Items.Clear()
        chksG1.DataSource = dsForm.Tables("tblsG1").DefaultView
        chksG1.DisplayMember = "sharh"
        chksG1.ValueMember = "code"

    End Sub
    Private Sub ClearForm()
        For I As Integer = 0 To chksG1.Items.Count - 1
            chksG1.SetItemChecked(I, False)
        Next
    End Sub
    Private Sub btnReport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReport.Click
        Try
            Dim cnSQL As SqlConnection
            Dim strSQL As String
            Dim cmSQL As SqlCommand
            Dim daSQL As SqlDataAdapter
            Dim p As New SqlParameter
            Dim strsG1 As String = ""

            If dsForm.Tables.Contains("tblGozaresh") Then
                dsForm.Tables.Remove("tblGozaresh")
            End If

            If chksG1.CheckedItems.Count = 0 Then
                If chksG1.CheckedIndices.Count = 0 Then
                    For I As Integer = 0 To chksG1.Items.Count - 1
                        chksG1.SetItemChecked(I, True)
                    Next
                End If
            End If

            If chksG1.CheckedItems.Count <> 0 Then
                For I As Integer = 0 To chksG1.Items.Count - 1
                    If chksG1.GetItemChecked(I) Then
                        chksG1.SelectedIndex = I
                        strsG1 &= chksG1.SelectedValue.ToString & ","
                    End If
                Next
                strsG1 = strsG1.Remove(strsG1.Length - 1, 1)
            End If

            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()

            strSQL = "Sales.spBargeTafkik_PrintTafkikJozSatrKoli "

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            p = New SqlParameter("ccTafkikJoze", SqlDbType.Int)
            p.Value = ccTafkikJoze
            cmSQL.Parameters.Add(p)

            p = New SqlParameter("sG1", SqlDbType.NVarChar, 50)
            p.Value = "," & strsG1 & ","
            cmSQL.Parameters.Add(p)

            daSQL = New SqlDataAdapter(cmSQL)
            daSQL.SelectCommand.CommandTimeout = 99999
            daSQL.Fill(dsForm, "tblGozaresh")

            If dsForm.Tables("tblGozaresh").Rows.Count = 0 Then
                MsgBox("هیـــــچ رکوردی برای گزارش پیدا نشد.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "پیام")
                ClearForm()
                Exit Sub
            End If

            Dim rpt As New ReportDocument
            Dim rpttables As Tables
            Dim rptformula As FormulaFieldDefinitions
            Dim frm As New Forms_dll.frmGL_Gozaresh

            rpt.Load(rptPath & "\rptFO_GozareshFaktor_Kh_Tafkik.rpt")

            rpttables = rpt.Database.Tables
            rpttables.Item(0).SetDataSource(dsForm.Tables("tblGozaresh"))

            rptformula = rpt.DataDefinition.FormulaFields
            With rptformula
                .Item("Group_Sanad").Text = "{mydata.FaktorShomareh}"
                .Item("Sh").Text = "{mydata.FaktorShomareh}"
                .Item("Tarikh").Text = "{mydata.FaktorTarikh}"
                .Item("Moshtary").Text = "{mydata.NameMoshtary}"
                .Item("CodeMoshtary").Text = "{mydata.CodeMoshtary}"
                .Item("Bazaryab").Text = "{mydata.NameForoshandeh}"
                .Item("txtNoePardakht").Text = "{mydata.txtNoePardakht}"
                .Item("CodeKala").Text = "{mydata.CodeKala}"
                .Item("NameKala").Text = "{mydata.NameKala}"
                .Item("Tedad").Text = "{mydata.Tedad3}"
                .Item("Fee").Text = "{mydata.Fee}"
                .Item("DarsadTakhfif").Text = "{mydata.DarsadTakhfif}"
                .Item("TakhfifKala").Text = "{mydata.TakhfifKala}"
                .Item("Mkol3").Text = "{mydata.Mkol3}"
                .Item("JamTakhfif").Text = "{mydata.JamTakhfif}"
                .Item("Takhfif").Text = "{mydata.Takhfif}"
                .Item("Title").Text = "'" & "فاکـــتورهای برگه تفکیک" & "'"
                .Item("Title2").Text = "'" & NameSherkat & "'"
                .Item("Title3").Text = "{mydata.NameRanandeh}"
                '.Item("Title3").Text = "'" & "نام راننده : " & Ranandeh & "'"
            End With

            rpt.Refresh()

            frm.Text = txtCaption

            With frm.CRV
                .ReportSource = rpt
                .DisplayGroupTree = False
                .ShowGroupTreeButton = False
                'If Not objSec.HaveUserPermission(SN, UD_Dll.Security.ePermissions.Print) Then .ShowPrintButton = False
                'If Not objSec.HaveUserPermission(SN, UD_Dll.Security.ePermissions.Export) Then .ShowExportButton = False
            End With


            Me.Hide()
            frm.ShowDialog(Me)
            frm = Nothing
            daSQL = Nothing
            rpt = Nothing
            Me.Show()

            ClearForm()

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->Print PishFaktor")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->Print PishFaktor")
        End Try
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub
End Class