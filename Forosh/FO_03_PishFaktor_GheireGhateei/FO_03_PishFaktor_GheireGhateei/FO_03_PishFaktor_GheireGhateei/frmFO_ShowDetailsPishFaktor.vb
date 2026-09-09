Public Class frmFO_ShowDetailsPishFaktor
#Region "Variable AND Constant Declration"
    Dim dvForm As DataView
    Dim ErrPro As New ErrorProvider
    Dim dsForm As New DataSet
    Public ccTitr As Integer = 0
#End Region
    Private Sub frmFO_ShowDetailsPishFaktor_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SearchSatr()
    End Sub
    Private Sub SearchSatr()
        Dim cnSQL As New SqlConnection
        Dim cmSQL As New SqlCommand
        Dim daSQl As New SqlDataAdapter
        Dim strSQL As String = ""

        If dsForm.Tables.Contains("tblSatr") Then
            dsForm.Tables.Remove("tblSatr")
        End If

        Try
            strSQL = "Sales.spPishFaktorGheireGhateei_SearchKalaPishFaktor "

            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            cmSQL.Parameters.AddWithValue("ccPishFaktorTitr", ccTitr)

            daSQl = New SqlDataAdapter(cmSQL)
            daSQl.Fill(dsForm, "tblSatr")

            dvForm = New DataView(dsForm.Tables("tblSatr"))
            dvForm.Sort = "CodeKala ASC"

            dvForm.AllowNew = False
            dvForm.AllowDelete = False
            dvForm.AllowEdit = True

            cmSQL = Nothing : daSQl = Nothing
            cnSQL.Close()

            SetGridSatr()
            With GridEXSatr
                .Visible = True
                .DataSource = Nothing
                .DataSource = dvForm
            End With

            Me.CenterToScreen()
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> SearchSatr ")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> SearchSatr ")
        End Try
    End Sub
    Private Sub SetGridSatr()
        If dvForm.Count = 0 Then
            Exit Sub
        End If

        Try
            With GridEXSatr
                .DataSource = Nothing
                .DataSource = dsForm.Tables("tblSatr").DefaultView
                .SetDataBinding(dsForm.Tables("tblSatr").DefaultView, "")
                .RetrieveStructure()
            End With

            For i As Integer = 0 To GridEXSatr.CurrentTable.Columns.Count - 1
                GridEXSatr.CurrentTable.Columns.Item(i).Visible = False
            Next

            GridEXSatr.CurrentTable.Columns.Item("CodeKala").Caption = "کد کالا"
            GridEXSatr.CurrentTable.Columns.Item("CodeKala").Visible = True
            GridEXSatr.CurrentTable.Columns.Item("CodeKala").Width = 150
            GridEXSatr.CurrentTable.Columns.Item("CodeKala").EditType = EditType.NoEdit
            GridEXSatr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXSatr.CurrentTable.Columns.Item("CodeKala").Position = 0
            GridEXSatr.CurrentTable.Columns.Item("CodeKala").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXSatr.CurrentTable.Columns.Item("CodeKala").HeaderAlignment = TextAlignment.Center

            GridEXSatr.CurrentTable.Columns.Item("NameKala").Caption = "نام کالا"
            GridEXSatr.CurrentTable.Columns.Item("NameKala").Visible = True
            GridEXSatr.CurrentTable.Columns.Item("NameKala").Width = 600
            GridEXSatr.CurrentTable.Columns.Item("NameKala").EditType = EditType.NoEdit
            GridEXSatr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXSatr.CurrentTable.Columns.Item("NameKala").Position = 1
            GridEXSatr.CurrentTable.Columns.Item("NameKala").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXSatr.CurrentTable.Columns.Item("NameKala").HeaderAlignment = TextAlignment.Center

            GridEXSatr.CurrentTable.Columns.Item("Tedad").Caption = "تعداد"
            GridEXSatr.CurrentTable.Columns.Item("Tedad").Visible = True
            GridEXSatr.CurrentTable.Columns.Item("Tedad").Width = 150
            GridEXSatr.CurrentTable.Columns.Item("Tedad").EditType = EditType.NoEdit
            GridEXSatr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXSatr.CurrentTable.Columns.Item("Tedad").Position = 2
            GridEXSatr.CurrentTable.Columns.Item("Tedad").FormatString = "###,###.##"
            GridEXSatr.CurrentTable.Columns.Item("Tedad").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXSatr.CurrentTable.Columns.Item("Tedad").HeaderAlignment = TextAlignment.Center

            For i As Integer = 0 To GridEXSatr.RootTable.Columns.Count - 1
                If GridEXSatr.RootTable.Columns(i).Type.IsValueType Then
                    GridEXSatr.RootTable.Columns(i).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                    GridEXSatr.RootTable.Columns(i).FormatMode = Janus.Windows.GridEX.FormatMode.UseIFormattable
                    GridEXSatr.RootTable.Columns(i).FormatString = "G"
                    GridEXSatr.RootTable.Columns(i).TotalFormatMode = Janus.Windows.GridEX.FormatMode.UseIFormattable
                    GridEXSatr.RootTable.Columns(i).TotalFormatString = "G"
                End If
            Next

            Me.CenterToScreen()
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> SetGridSatr ")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> SetGridSatr ")
        End Try
    End Sub
End Class