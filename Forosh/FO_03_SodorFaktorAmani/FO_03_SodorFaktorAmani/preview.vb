Public Class preview
#Region "Variable AND Constant Declration"
    'Const cntCodeSubSystem As Long = 17
    Dim dsForm As New DataSet
    Dim dvForm As DataView
    Dim txtCaption As String
    Private SN As Integer
    Dim ErrPro As New ErrorProvider
    Dim flg As Boolean = False
    Dim dvTitr_PishFaktor As DataView
   
    Public ccPishFaktorAmani_Vaset As Integer
    Public CountTafkikSelected As Integer
#End Region

    Private Sub preview_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ReportFaktor(ccPishFaktorAmani_Vaset)
    End Sub
    Private Sub ReportFaktor(ByVal ccPishFaktorAmani_Vaset As Integer)
        Dim cnSQL As New SqlConnection
        Dim cmSQL As New SqlCommand
        Dim daSQl As New SqlDataAdapter
        Dim strSQL As String = ""

    

        If dsForm.Tables.Contains("tbl_PishNamayesh") Then
            dsForm.Tables.Remove("tbl_PishNamayesh")
        End If

        Try
            strSQL = "Sales.spRpt_preview_PrintAmani "

            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            cmSQL.Parameters.AddWithValue("ccPishFaktorAmani_Vaset", ccPishFaktorAmani_Vaset)
         

            daSQl = New SqlDataAdapter(cmSQL)
            daSQl.Fill(dsForm, "tbl_PishNamayesh")

            dvForm = New DataView
            dvForm = dsForm.Tables("tbl_PishNamayesh").DefaultView
            dvForm.Sort = "CodeKala ASC"
            dvForm.AllowDelete = False
            dvForm.AllowEdit = False
            dvForm.AllowNew = False

         
        
            cmSQL = Nothing : daSQl = Nothing

            dvTitr_PishFaktor = New DataView(dsForm.Tables("tbl_PishNamayesh"), "", "CodeKala ASC", DataViewRowState.CurrentRows)
            dvTitr_PishFaktor.AllowNew = False
            dvTitr_PishFaktor.AllowDelete = False
            dvTitr_PishFaktor.AllowEdit = False


            GridEX.DataSource = Nothing
            GridEX.DataSource = dvTitr_PishFaktor
            cnSQL.Close()

            SetGridPreview()
            With GridEX
                .Visible = True
                .DataSource = Nothing
                .DataSource = dvTitr_PishFaktor
            End With

            Me.CenterToScreen()
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> SearchPishFaktor ")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> SearchPishFaktor ")
        End Try
    End Sub

    Private Sub SetGridPreview()

        If dvTitr_PishFaktor.Count = 0 Then
            Exit Sub
        End If
        Try
            With GridEX
                .DataSource = Nothing
                .DataSource = dsForm.Tables("tbl_PishNamayesh").DefaultView
                .SetDataBinding(dsForm.Tables("tbl_PishNamayesh").DefaultView, "")
                .RetrieveStructure()
            End With

            For i As Integer = 0 To GridEX.CurrentTable.Columns.Count - 1
                GridEX.CurrentTable.Columns.Item(i).Visible = False
            Next


            GridEX.CurrentTable.Columns.Item("codekala").Caption = "کـــد کالا"
            GridEX.CurrentTable.Columns.Item("codekala").Visible = True
            GridEX.CurrentTable.Columns.Item("codekala").Width = 130
            GridEX.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEX.CurrentTable.Columns.Item("codekala").Position = 0
            GridEX.CurrentTable.Columns.Item("codekala").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEX.CurrentTable.Columns.Item("NameKala").Caption = "نام کالا"
            GridEX.CurrentTable.Columns.Item("NameKala").Visible = True
            GridEX.CurrentTable.Columns.Item("NameKala").Width = 246
            GridEX.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEX.CurrentTable.Columns.Item("NameKala").Position = 1
            GridEX.CurrentTable.Columns.Item("NameKala").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center




            GridEX.CurrentTable.Columns.Item("NameMoshtary").Caption = "نام مشتری"
            GridEX.CurrentTable.Columns.Item("NameMoshtary").Visible = True
            GridEX.CurrentTable.Columns.Item("NameMoshtary").Width = 240
            GridEX.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEX.CurrentTable.Columns.Item("NameMoshtary").Position = 2
            GridEX.CurrentTable.Columns.Item("NameMoshtary").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center



            GridEX.CurrentTable.Columns.Item("CodeMoshtary").Caption = "کد مشتری"
            GridEX.CurrentTable.Columns.Item("CodeMoshtary").Visible = True
            GridEX.CurrentTable.Columns.Item("CodeMoshtary").Width = 100
            GridEX.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEX.CurrentTable.Columns.Item("CodeMoshtary").Position = 3
            GridEX.CurrentTable.Columns.Item("CodeMoshtary").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center



            GridEX.CurrentTable.Columns.Item("Tedad").Caption = "تعداد"
            GridEX.CurrentTable.Columns.Item("Tedad").Visible = True
            GridEX.CurrentTable.Columns.Item("Tedad").Width = 100
            GridEX.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEX.CurrentTable.Columns.Item("Tedad").Position = 4
            GridEX.CurrentTable.Columns.Item("Tedad").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center



            GridEX.CurrentTable.Columns.Item("TedadKarton").Caption = "تعداد کارتن"
            GridEX.CurrentTable.Columns.Item("TedadKarton").Visible = True
            GridEX.CurrentTable.Columns.Item("TedadKarton").Width = 100
            GridEX.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEX.CurrentTable.Columns.Item("TedadKarton").Position = 5
            GridEX.CurrentTable.Columns.Item("TedadKarton").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEX.CurrentTable.Columns.Item("Mkol").Caption = "قیمت"
            GridEX.CurrentTable.Columns.Item("Mkol").Visible = True
            GridEX.CurrentTable.Columns.Item("Mkol").Width = 180
            GridEX.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEX.CurrentTable.Columns.Item("Mkol").Position = 6
            GridEX.CurrentTable.Columns.Item("Mkol").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center





            GridEX.CurrentTable.Columns.Item("Fee").Caption = "فی"
            GridEX.CurrentTable.Columns.Item("Fee").Visible = True
            GridEX.CurrentTable.Columns.Item("Fee").Width = 100
            GridEX.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEX.CurrentTable.Columns.Item("Fee").Position = 7
            GridEX.CurrentTable.Columns.Item("Fee").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center




            GridEX.CurrentTable.Columns.Item("TakhfifKala").Caption = "تخفیف کالا"
            GridEX.CurrentTable.Columns.Item("TakhfifKala").Visible = True
            GridEX.CurrentTable.Columns.Item("TakhfifKala").Width = 100
            GridEX.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEX.CurrentTable.Columns.Item("TakhfifKala").Position = 8
            GridEX.CurrentTable.Columns.Item("TakhfifKala").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center




            GridEX.CurrentTable.Columns.Item("MablaghMalyat").Caption = "مبلغ مالیات"
            GridEX.CurrentTable.Columns.Item("MablaghMalyat").Visible = True
            GridEX.CurrentTable.Columns.Item("MablaghMalyat").Width = 100
            GridEX.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEX.CurrentTable.Columns.Item("MablaghMalyat").Position = 9
            GridEX.CurrentTable.Columns.Item("MablaghMalyat").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center



            GridEX.CurrentTable.Columns.Item("MablaghAvarez").Caption = " مبلغ عوارض"
            GridEX.CurrentTable.Columns.Item("MablaghAvarez").Visible = True
            GridEX.CurrentTable.Columns.Item("MablaghAvarez").Width = 100
            GridEX.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEX.CurrentTable.Columns.Item("MablaghAvarez").Position = 10
            GridEX.CurrentTable.Columns.Item("MablaghAvarez").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            For i As Integer = 0 To GridEX.RootTable.Columns.Count - 1
                If GridEX.RootTable.Columns(i).Type.IsValueType Then
                    GridEX.RootTable.Columns(i).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                    GridEX.RootTable.Columns(i).FormatMode = Janus.Windows.GridEX.FormatMode.UseIFormattable
                    GridEX.RootTable.Columns(i).FormatString = "G"
                    GridEX.RootTable.Columns(i).TotalFormatMode = Janus.Windows.GridEX.FormatMode.UseIFormattable
                    GridEX.RootTable.Columns(i).TotalFormatString = "G"

                End If
            Next


         
            Me.CenterToScreen()
            'GridEX.Visible = True
            'GridEX.HeaderFormatStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center


        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> SetGridEx")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> SetGridEx")
        End Try

    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
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
        Dim frm As New Forms_dll.frmGL_Gozaresh
        Try
            Windows.Forms.Cursor.Current = Cursors.WaitCursor

            Dim rpt As New CrystalDecisions.CrystalReports.Engine.ReportDocument
            Dim rpttables As CrystalDecisions.CrystalReports.Engine.Tables
            Dim rptformula As CrystalDecisions.CrystalReports.Engine.FormulaFieldDefinitions

            rpt.Load(rptPath & "\rptFO_GozareshAmani_preview.rpt")

            rpttables = rpt.Database.Tables
            rpttables.Item(0).SetDataSource(dsForm.Tables("tbl_PishNamayesh"))

            rptformula = rpt.DataDefinition.FormulaFields

            With rptformula

                .Item("ccPishFaktorAmani_Vaset").Text = "{mydata.ccPishFaktorAmani_Vaset}"
                .Item("NameMoshtary").Text = "{mydata.NameMoshtary}"
                .Item("CodeMoshtary").Text = "{mydata.CodeMoshtary}"
                .Item("CodeKala").Text = "{mydata.CodeKala}"
                .Item("NameKala").Text = "{mydata.NameKala}"
                .Item("fee").Text = "{mydata.fee}"
                .Item("Mkol").Text = "{mydata.Mkol}"
                .Item("Tedad").Text = "{mydata.Tedad}"
                .Item("TedadKarton").Text = "{mydata.TedadKarton}"

                .Item("MablaghMalyat").Text = "{mydata.MablaghMalyat}"
                .Item("MablaghAvarez").Text = "{mydata.MablaghAvarez}"
                .Item("TakhfifKala").Text = "{mydata.TakhfifKala}"

                .Item("Title").Text = "'" & "گزارش پیش نمایش فاکتور امانی" & "'"
                .Item("Title2").Text = "'" & NameSherkat & "'"
                .Item("Title3").Text = "'" & NameMahalFaal & "'"

                .Item("KarbarGozaresh").Text = "'" & PersonelName & "'"
                .Item("TarikhGozaresh").Text = "'" & objTarikh.SetDateSlash(TarikhEmrooz) & "'"
                .Item("SaatGozaresh").Text = "'" & Format(TimeOfDay, "HH:mm:ss") & "'"

            End With

            rpt.Refresh()

            frm.Text = "گزارش پیش نمایش فاکتور امانی"

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
            rpt = Nothing
            rpttables = Nothing
            rptformula = Nothing

            Me.Show()
            Windows.Forms.Cursor.Current = Cursors.Default

            'CancelForm()
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->Print PishFaktor")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->Print PishFaktor")
        End Try
    End Sub

   

End Class