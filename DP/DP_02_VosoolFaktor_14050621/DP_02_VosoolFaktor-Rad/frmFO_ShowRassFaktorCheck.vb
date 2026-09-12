

Public Class frmFO_ShowRassFaktorCheck
    Public Property SelectedCheck As New CheckInfo
#Region "Variable AND Constant Declration"
    Dim dvForm As DataView
    Dim ErrPro As New ErrorProvider
    Dim dsForm As New DataSet
    Public ccFaktorTitr_Search As Integer = 0
    Dim dt11 As DataTable = Nothing

#End Region
    Private Sub frmFO_ShowDetailsPishFaktor_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SearchRaasGiri()
    End Sub
    Private Sub SearchRaasGiri()
        Dim cnSQL As New SqlConnection
        Dim cmSQL As New SqlCommand
        Dim daSQl As New SqlDataAdapter
        Dim strSQL As String = ""

        If dsForm.Tables.Contains("tblSatr") Then
            dsForm.Tables.Remove("tblSatr")
        End If

        Try
            Dim da As SqlDataAdapter = New SqlDataAdapter

            Using cn As New SqlConnection(ConnectionString)
                Using cm As SqlCommand = cn.CreateCommand()
                    cn.Open()
                    cm.Parameters.Clear()
                    cm.CommandType = CommandType.StoredProcedure
                    cm.CommandText = "[Treasury].[VosolFaktor_SearchCheckFaktor]"
                    cm.Parameters.AddWithValue("ccFaktorTitr", ccFaktorTitr_Search)
                    cm.Parameters.AddWithValue("ccMoshtary", ccMoshtary)
                    da.SelectCommand = cm
                    dt11 = New DataTable
                    da.Fill(dt11)
                End Using
            End Using




            SetGridSatr()
            With GridEXSatr
                .Visible = True
                .DataSource = Nothing
                .DataSource = dt11.DefaultView
            End With

            Me.CenterToScreen()
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> SearchSatr ")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> SearchSatr ")
        End Try
    End Sub
    Private Sub SetGridSatr()
        If dt11.Rows.Count = 0 Then
            Exit Sub
        End If

        Try
            With GridEXSatr
                .DataSource = Nothing
                .DataSource = dt11.DefaultView
                .SetDataBinding(dt11.DefaultView, "")
                .RetrieveStructure()
            End With

            For i As Integer = 0 To GridEXSatr.CurrentTable.Columns.Count - 1
                GridEXSatr.CurrentTable.Columns.Item(i).Visible = False
            Next

            GridEXSatr.CurrentTable.Columns.Item("Mablagh").Caption = "مبلغ چک"
            GridEXSatr.CurrentTable.Columns.Item("Mablagh").Visible = True
            GridEXSatr.CurrentTable.Columns.Item("Mablagh").Width = 150
            'GridEXSatr.CurrentTable.Columns.Item("Mablagh").EditType = EditType.NoEdit
            GridEXSatr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXSatr.CurrentTable.Columns.Item("Mablagh").Position = 0
            GridEXSatr.CurrentTable.Columns.Item("Mablagh").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            'GridEXSatr.CurrentTable.Columns.Item("Mablagh").HeaderAlignment = TextAlignment.Center

            GridEXSatr.CurrentTable.Columns.Item("TarikhSanadSlash").Caption = "تاریخ سررسید چک"
            GridEXSatr.CurrentTable.Columns.Item("TarikhSanadSlash").Visible = True
            GridEXSatr.CurrentTable.Columns.Item("TarikhSanadSlash").Width = 120
            'GridEXSatr.CurrentTable.Columns.Item("TarikhSanadSlash").EditType = EditType.NoEdit
            GridEXSatr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXSatr.CurrentTable.Columns.Item("TarikhSanadSlash").Position = 1
            GridEXSatr.CurrentTable.Columns.Item("TarikhSanadSlash").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            'GridEXSatr.CurrentTable.Columns.Item("TarikhSanad").HeaderAlignment = TextAlignment.Center

            GridEXSatr.CurrentTable.Columns.Item("ShomarehSanad").Caption = "شماره چک"
            GridEXSatr.CurrentTable.Columns.Item("ShomarehSanad").Visible = True
            GridEXSatr.CurrentTable.Columns.Item("ShomarehSanad").Width = 120
            'GridEXSatr.CurrentTable.Columns.Item("ShomarehSanad").EditType = EditType.NoEdit
            GridEXSatr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXSatr.CurrentTable.Columns.Item("ShomarehSanad").Position = 2
            GridEXSatr.CurrentTable.Columns.Item("ShomarehSanad").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            'GridEXSatr.CurrentTable.Columns.Item("ShomarehSanad").HeaderAlignment = TextAlignment.Center


            GridEXSatr.CurrentTable.Columns.Item("NameMoshtary").Caption = " نام مشتری"
            GridEXSatr.CurrentTable.Columns.Item("NameMoshtary").Visible = True
            GridEXSatr.CurrentTable.Columns.Item("NameMoshtary").Width = 150
            'GridEXSatr.CurrentTable.Columns.Item("NameMoshtary").EditType = EditType.NoEdit
            GridEXSatr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXSatr.CurrentTable.Columns.Item("NameMoshtary").Position = 3
            GridEXSatr.CurrentTable.Columns.Item("NameMoshtary").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXSatr.CurrentTable.Columns.Item("CodeMoshtary").Caption = " کد مشتری"
            GridEXSatr.CurrentTable.Columns.Item("CodeMoshtary").Visible = True
            GridEXSatr.CurrentTable.Columns.Item("CodeMoshtary").Width = 150
            'GridEXSatr.CurrentTable.Columns.Item("CodeMoshtary").EditType = EditType.NoEdit
            GridEXSatr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXSatr.CurrentTable.Columns.Item("CodeMoshtary").Position = 4
            GridEXSatr.CurrentTable.Columns.Item("CodeMoshtary").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            'GridEXSatr.CurrentTable.Columns.Item("FaktorShomareh").Caption = " شماره فاکتور"
            'GridEXSatr.CurrentTable.Columns.Item("FaktorShomareh").Visible = True
            'GridEXSatr.CurrentTable.Columns.Item("FaktorShomareh").Width = 150
            ''GridEXSatr.CurrentTable.Columns.Item("FaktorShomareh").EditType = EditType.NoEdit
            'GridEXSatr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            'GridEXSatr.CurrentTable.Columns.Item("FaktorShomareh").Position = 5
            'GridEXSatr.CurrentTable.Columns.Item("FaktorShomareh").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            'GridEXSatr.CurrentTable.Columns.Item("FaktorTarikh").Caption = " تاریخ فاکتور"
            'GridEXSatr.CurrentTable.Columns.Item("FaktorTarikh").Visible = True
            'GridEXSatr.CurrentTable.Columns.Item("FaktorTarikh").Width = 150
            ''GridEXSatr.CurrentTable.Columns.Item("FaktorTarikh").EditType = EditType.NoEdit
            'GridEXSatr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            'GridEXSatr.CurrentTable.Columns.Item("FaktorTarikh").Position = 6
            'GridEXSatr.CurrentTable.Columns.Item("FaktorTarikh").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXSatr.CurrentTable.Columns.Item("FN_AvarandehVajh").Caption = " آورنده وجه"
            GridEXSatr.CurrentTable.Columns.Item("FN_AvarandehVajh").Visible = True
            GridEXSatr.CurrentTable.Columns.Item("FN_AvarandehVajh").Width = 150
            'GridEXSatr.CurrentTable.Columns.Item("FaktorTarikh").EditType = EditType.NoEdit
            GridEXSatr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXSatr.CurrentTable.Columns.Item("FN_AvarandehVajh").Position = 7
            GridEXSatr.CurrentTable.Columns.Item("FN_AvarandehVajh").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center


            GridEXSatr.CurrentTable.Columns.Item("cc").Caption = "cc"
            GridEXSatr.CurrentTable.Columns.Item("cc").Visible = False
            GridEXSatr.CurrentTable.Columns.Item("cc").Width = 0
            'GridEXSatr.CurrentTable.Columns.Item("FaktorTarikh").EditType = EditType.NoEdit
            GridEXSatr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXSatr.CurrentTable.Columns.Item("cc").Position = 8
            GridEXSatr.CurrentTable.Columns.Item("cc").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center


            GridEXSatr.CurrentTable.Columns.Item("sBankSanad").Caption = "sBankSanad"
            GridEXSatr.CurrentTable.Columns.Item("sBankSanad").Visible = False
            GridEXSatr.CurrentTable.Columns.Item("sBankSanad").Width = 0
            'GridEXSatr.CurrentTable.Columns.Item("FaktorTarikh").EditType = EditType.NoEdit
            GridEXSatr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXSatr.CurrentTable.Columns.Item("sBankSanad").Position = 9
            GridEXSatr.CurrentTable.Columns.Item("sBankSanad").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

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

    Private Sub GridEXSatr_DoubleClick(sender As Object, e As EventArgs) Handles GridEXSatr.DoubleClick
        If GridEXSatr.RowCount = 0 Then Exit Sub

        With SelectedCheck
            .TarikhSanad = GridEXSatr.CurrentRow.Cells("TarikhSanad").Value.ToString()
            .TarikhDP = GridEXSatr.CurrentRow.Cells("TarikhDP").Value.ToString()
            .ShomarehSanad = GridEXSatr.CurrentRow.Cells("ShomarehSanad").Value.ToString()
            .Mablagh = (GridEXSatr.CurrentRow.Cells("Mablagh").Value)
            .CodeFard = GridEXSatr.CurrentRow.Cells("CodeFard_Avarandeh").Value
            .cc = GridEXSatr.CurrentRow.Cells("cc").Value
            .sBank = GridEXSatr.CurrentRow.Cells("sBankSanad").Value
        End With

        Me.DialogResult = DialogResult.OK
        Me.Close()

    End Sub


End Class