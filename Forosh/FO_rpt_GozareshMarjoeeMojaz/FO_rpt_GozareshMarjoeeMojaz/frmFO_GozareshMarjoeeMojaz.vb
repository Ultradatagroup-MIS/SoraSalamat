Public Class frmFO_GozareshMarjoeeMojaz

#Region "Variable AND Constant Declration"
    ' Quary And Table Names
    Const FormViewName = "qryFO_PishFaktorTitrSatr"
    ' Security 
    Const cntCodeSubSystem As Long = 1000118
    Private SN As Integer

    Dim ErrPro As New ErrorProvider
    Dim dsForm As New DataSet
    Dim dr As DataRow
    Dim dvForm, dvTitr As DataView
    Dim txtCaption As String

    Private WithEvents BS As New UD_Dll.PassString
#End Region

    Private Sub frmFO_GozareshMarjoeeMojaz_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetParameter()
        SN = objSec.GetSecurityNumber(cntCodeSubSystem, UserName)
        objTools.ChangeKbd(UD_Dll.mdlUtility.KBD.Farsi)
        mskAzTarikh.Text = TarikhEmrooz.Substring(0, 6) + "01"
        mskTaTarikh.Text = TarikhEmrooz
        LoadCombo()
        cmbForoshandeh.SelectedValue = 0
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
            strSQL = "Global.spForoshandeh_ForoshGarm_LoadCombo "

            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            cmSQL.Parameters.AddWithValue("CodeMahal", CodeMahalFaal)

            daSQL = New SqlDataAdapter(cmSQL)
            daSQL.Fill(dsForm, "tbl_Foroshandeh")

            dr = dsForm.Tables("tbl_Foroshandeh").NewRow()
            dr("ccForoshandeh") = 0
            dr("NameForoshandeh") = "همه"
            dsForm.Tables("tbl_Foroshandeh").Rows.Add(dr)

            cmbForoshandeh.DataSource = Nothing
            cmbForoshandeh.Items.Clear()
            cmbForoshandeh.DataSource = dsForm.Tables("tbl_Foroshandeh").DefaultView
            cmbForoshandeh.DisplayMember = "NameForoshandeh"
            cmbForoshandeh.ValueMember = "ccForoshandeh"

            daSQL = Nothing
            cmSQL = Nothing
            cnSQL.Close()

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->LoadCombo")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->LoadCombo")
        End Try
    End Sub
    Private Sub Search()
        Try
            GridEXTitr.DataSource = Nothing

            If Not IsValidField("All") Then
                Exit Sub
            End If

            Dim StrSql As String

            StrSql = "Report.spGozareshMarjoeeMojaz_AfraShir "

            RefreshFormData(StrSql)

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> Search ")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> Search ")
        End Try
    End Sub
    Private Sub RefreshFormData(ByVal strSql As String)
        Dim cnSQL As New SqlConnection
        Dim cmSQL As New SqlCommand
        Dim daSQL As SqlDataAdapter

        Try

            cnSQL.ConnectionString = ConnectionString
            cnSQL.Open()

            cmSQL = New SqlCommand(strSql, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            cmSQL.Parameters.AddWithValue("CodeDoreh", CodeDoreh)
            cmSQL.Parameters.AddWithValue("CodeMahal", CodeMahalFaal)
            cmSQL.Parameters.AddWithValue("AzTarikh", mskAzTarikh.Text)
            cmSQL.Parameters.AddWithValue("TaTarikh", mskTaTarikh.Text)
            cmSQL.Parameters.AddWithValue("ccForoshandeh", cmbForoshandeh.SelectedValue)

            If dsForm.Tables.Contains("tbl_MarjoeeMojaz") Then
                dsForm.Tables.Remove("tbl_MarjoeeMojaz")
            End If

            daSQL = New SqlDataAdapter(cmSQL)
            daSQL.Fill(dsForm, "tbl_MarjoeeMojaz")

            dvForm = New DataView
            dvForm = dsForm.Tables("tbl_MarjoeeMojaz").DefaultView
            dvForm.Sort = "CodeForoshandeh ASC"
            dvForm.AllowDelete = True
            dvForm.AllowEdit = False
            dvForm.AllowNew = False
            daSQL = Nothing

            dvTitr = New DataView(dsForm.Tables("tbl_MarjoeeMojaz"), "", "CodeForoshandeh ASC, Tarikh ASC", DataViewRowState.CurrentRows)
            dvTitr.AllowNew = False
            dvTitr.AllowDelete = False
            dvTitr.AllowEdit = False

            GridEXTitr.DataSource = Nothing
            GridEXTitr.DataSource = dvTitr

            If dvTitr.Count <> 0 Then
                SetGridStyle()
            End If

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> RefreshFormData ")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> RefreshFormData ")
        Finally
            cmSQL = Nothing : daSQL = Nothing
            cnSQL.Close()
        End Try
    End Sub
    Private Sub SetGridStyle()
        Try
            With GridEXTitr
                .DataSource = Nothing
                .DataSource = dsForm.Tables("tbl_MarjoeeMojaz").DefaultView
                .SetDataBinding(dsForm.Tables("tbl_MarjoeeMojaz").DefaultView, "")
                .RetrieveStructure()
            End With

            For i As Integer = 0 To GridEXTitr.CurrentTable.Columns.Count - 1
                GridEXTitr.CurrentTable.Columns.Item(i).Visible = False
            Next

            GridEXTitr.CurrentTable.Columns.Item("CodeForoshandeh").Caption = "کـد فـروشنــده"
            GridEXTitr.CurrentTable.Columns.Item("CodeForoshandeh").Visible = True
            GridEXTitr.CurrentTable.Columns.Item("CodeForoshandeh").Width = 90
            GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXTitr.CurrentTable.Columns.Item("CodeForoshandeh").Position = 0
            GridEXTitr.CurrentTable.Columns.Item("CodeForoshandeh").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXTitr.CurrentTable.Columns.Item("NameForoshandeh").Caption = "نـام فـروشنــده"
            GridEXTitr.CurrentTable.Columns.Item("NameForoshandeh").Visible = True
            GridEXTitr.CurrentTable.Columns.Item("NameForoshandeh").Width = 200
            GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXTitr.CurrentTable.Columns.Item("NameForoshandeh").Position = 1
            GridEXTitr.CurrentTable.Columns.Item("NameForoshandeh").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXTitr.CurrentTable.Columns.Item("Tarikh").Caption = "تـاریـخ"
            GridEXTitr.CurrentTable.Columns.Item("Tarikh").Visible = True
            GridEXTitr.CurrentTable.Columns.Item("Tarikh").Width = 80
            GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXTitr.CurrentTable.Columns.Item("Tarikh").Position = 2
            GridEXTitr.CurrentTable.Columns.Item("Tarikh").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXTitr.CurrentTable.Columns.Item("MablaghFaktor").Caption = "مبلـغ فــــروش"
            GridEXTitr.CurrentTable.Columns.Item("MablaghFaktor").Visible = True
            GridEXTitr.CurrentTable.Columns.Item("MablaghFaktor").Width = 120
            GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXTitr.CurrentTable.Columns.Item("MablaghFaktor").FormatString = "G"
            GridEXTitr.CurrentTable.Columns.Item("MablaghFaktor").Position = 3
            GridEXTitr.CurrentTable.Columns.Item("MablaghFaktor").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXTitr.CurrentTable.Columns.Item("DarsadMarjoeeMojaz").Caption = "درصد مرجوعی مجاز"
            GridEXTitr.CurrentTable.Columns.Item("DarsadMarjoeeMojaz").Visible = True
            GridEXTitr.CurrentTable.Columns.Item("DarsadMarjoeeMojaz").Width = 130
            GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXTitr.CurrentTable.Columns.Item("DarsadMarjoeeMojaz").FormatString = "G"
            GridEXTitr.CurrentTable.Columns.Item("DarsadMarjoeeMojaz").Position = 4
            GridEXTitr.CurrentTable.Columns.Item("DarsadMarjoeeMojaz").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXTitr.CurrentTable.Columns.Item("DarsadMarjoee").Caption = "درصد مرجوعی"
            GridEXTitr.CurrentTable.Columns.Item("DarsadMarjoee").Visible = True
            GridEXTitr.CurrentTable.Columns.Item("DarsadMarjoee").Width = 100
            GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXTitr.CurrentTable.Columns.Item("DarsadMarjoee").FormatString = "G"
            GridEXTitr.CurrentTable.Columns.Item("DarsadMarjoee").Position = 5
            GridEXTitr.CurrentTable.Columns.Item("DarsadMarjoee").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXTitr.CurrentTable.Columns.Item("MablaghMarjoeeMojaz").Caption = "مبلغ مرجوعی مجاز"
            GridEXTitr.CurrentTable.Columns.Item("MablaghMarjoeeMojaz").Visible = True
            GridEXTitr.CurrentTable.Columns.Item("MablaghMarjoeeMojaz").Width = 130
            GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXTitr.CurrentTable.Columns.Item("MablaghMarjoeeMojaz").Position = 6
            GridEXTitr.CurrentTable.Columns.Item("MablaghMarjoeeMojaz").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXTitr.CurrentTable.Columns.Item("MablaghMarjoee").Caption = "مبلغ مرجوعی"
            GridEXTitr.CurrentTable.Columns.Item("MablaghMarjoee").Visible = True
            GridEXTitr.CurrentTable.Columns.Item("MablaghMarjoee").Width = 100
            GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXTitr.CurrentTable.Columns.Item("MablaghMarjoee").FormatString = "G"
            GridEXTitr.CurrentTable.Columns.Item("MablaghMarjoee").Position = 7
            GridEXTitr.CurrentTable.Columns.Item("MablaghMarjoee").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXTitr.CurrentTable.Columns.Item("Tafavot").Caption = "مبلغ تفاوت"
            GridEXTitr.CurrentTable.Columns.Item("Tafavot").Visible = True
            GridEXTitr.CurrentTable.Columns.Item("Tafavot").Width = 100
            GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXTitr.CurrentTable.Columns.Item("Tafavot").FormatString = "G"
            GridEXTitr.CurrentTable.Columns.Item("Tafavot").Position = 8
            GridEXTitr.CurrentTable.Columns.Item("Tafavot").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXTitr.CurrentTable.Columns.Item("MarjoeeGhabeleGhabol").Caption = "مبلغ قابل قبـول"
            GridEXTitr.CurrentTable.Columns.Item("MarjoeeGhabeleGhabol").Visible = True
            GridEXTitr.CurrentTable.Columns.Item("MarjoeeGhabeleGhabol").Width = 120
            GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXTitr.CurrentTable.Columns.Item("MarjoeeGhabeleGhabol").FormatString = "G"
            GridEXTitr.CurrentTable.Columns.Item("MarjoeeGhabeleGhabol").Position = 9
            GridEXTitr.CurrentTable.Columns.Item("MarjoeeGhabeleGhabol").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            For i As Integer = 0 To GridEXTitr.RootTable.Columns.Count - 1
                If GridEXTitr.RootTable.Columns(i).Type.IsValueType Then
                    GridEXTitr.RootTable.Columns(i).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                    GridEXTitr.RootTable.Columns(i).FormatMode = Janus.Windows.GridEX.FormatMode.UseIFormattable
                    GridEXTitr.RootTable.Columns(i).FormatString = "###,###.##"
                    GridEXTitr.RootTable.Columns(i).TotalFormatMode = Janus.Windows.GridEX.FormatMode.UseIFormattable
                    GridEXTitr.RootTable.Columns(i).TotalFormatString = "###,###.##"
                End If
            Next

            GridEXTitr.Visible = True
            GridEXTitr.HeaderFormatStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center


        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> SetGridStyle")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> SetGridStyle")
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
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> IsValidField ")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> IsValidField ")
        End Try
    End Function
    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Search()
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        If Not objSec.HaveUserPermission(SN, UD_Dll.Security.ePermissions.Preview) Then Exit Sub
        Try
            Me.TopMost = False
            SetReport()
            Me.TopMost = True
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> btnPrint_Click ")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> btnPrint_Click ")
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

            strSQL = "Report.spGozareshMarjoeeMojaz_AfraShir "

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            cmSQL.Parameters.AddWithValue("CodeDoreh", CodeDoreh)
            cmSQL.Parameters.AddWithValue("CodeMahal", CodeMahalFaal)
            cmSQL.Parameters.AddWithValue("AzTarikh", mskAzTarikh.Text)
            cmSQL.Parameters.AddWithValue("TaTarikh", mskTaTarikh.Text)
            cmSQL.Parameters.AddWithValue("ccForoshandeh", cmbForoshandeh.SelectedValue)

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

            rpt.Load(rptPath & "\rptFO_GozareshMarjoeeMojaz.rpt")

            rpttables = rpt.Database.Tables
            rpttables.Item(0).SetDataSource(dsForm.Tables("tblGozaresh"))

            rptformula = rpt.DataDefinition.FormulaFields
            With rptformula

                .Item("Group_Sanad").Text = "{mydata.CodeDoreh}"
                .Item("AzTarikh").Text = "{mydata.AzTarikh}"
                .Item("TaTarikh").Text = "{mydata.TaTarikh}"
                .Item("CodeForoshandeh").Text = "{mydata.CodeForoshandeh}"
                .Item("NameForoshandeh").Text = "{mydata.NameForoshandeh}"
                .Item("Tarikh").Text = "{mydata.Tarikh}"
                .Item("MablaghFaktor").Text = "{mydata.MablaghFaktor}"
                .Item("DarsadMarjoeeMojaz").Text = "{mydata.DarsadMarjoeeMojaz}"
                .Item("DarsadMarjoee").Text = "{mydata.DarsadMarjoee}"
                .Item("MablaghMarjoeeMojaz").Text = "{mydata.MablaghMarjoeeMojaz}"
                .Item("MablaghMarjoee").Text = "{mydata.MablaghMarjoee}"
                .Item("Tafavot").Text = "{mydata.Tafavot}"
                .Item("MarjoeeGhabeleGhabol").Text = "{mydata.MarjoeeGhabeleGhabol}"
                .Item("Title").Text = "' گـزارش مـرجـوعی مجــاز '"
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

    Private Sub btnSaveDarsadMarjoeeMojazForoshandeh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveDarsadMarjoeeMojazForoshandeh.Click
        Dim frmNewSaveDarsadMarjoeeMojazForoshandeh As New frmFO_SaveDarsadMarjoeeMojazForoshandeh

        Me.Hide()
        frmNewSaveDarsadMarjoeeMojazForoshandeh.ShowDialog()
        Me.Show()
    End Sub
End Class
