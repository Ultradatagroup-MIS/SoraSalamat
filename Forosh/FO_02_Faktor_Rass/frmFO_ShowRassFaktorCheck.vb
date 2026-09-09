Public Class frmFO_ShowRassFaktorCheck
#Region "Variable AND Constant Declration"
    Dim dvForm As DataView
    Dim ErrPro As New ErrorProvider
    Dim dsForm As New DataSet
    Dim dt11 As DataTable = Nothing
    Dim dt11_Satr As DataTable = Nothing
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
                    cm.CommandText = "[Treasury].[RaasGiri_SearchCheckFaktor_Nahaee]"
                    cm.Parameters.AddWithValue("CheckTaeed", CheckTaeed)
                    cm.Parameters.AddWithValue("CodeFard", UserCode)
                    cm.Parameters.AddWithValue("Noe", 1)
                    cm.Parameters.AddWithValue("cc", 0)
                    da.SelectCommand = cm
                    dt11 = New DataTable
                    da.Fill(dt11)
                End Using
            End Using



            SetGridSatr()

            'With GridEXSatr
            '    .Visible = True
            '    .DataSource = Nothing
            '    .DataSource = dt11.DefaultView
            'End With

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




            GridEXSatr.CurrentTable.Columns.Item("NameMoshtary").Caption = " نام مشتری"
            GridEXSatr.CurrentTable.Columns.Item("NameMoshtary").Visible = True
            GridEXSatr.CurrentTable.Columns.Item("NameMoshtary").Width = 150
            'GridEXSatr.CurrentTable.Columns.Item("NameMoshtary").EditType = EditType.NoEdit
            GridEXSatr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXSatr.CurrentTable.Columns.Item("NameMoshtary").Position = 2
            GridEXSatr.CurrentTable.Columns.Item("NameMoshtary").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXSatr.CurrentTable.Columns.Item("CodeMoshtary").Caption = " کد مشتری"
            GridEXSatr.CurrentTable.Columns.Item("CodeMoshtary").Visible = True
            GridEXSatr.CurrentTable.Columns.Item("CodeMoshtary").Width = 150
            'GridEXSatr.CurrentTable.Columns.Item("CodeMoshtary").EditType = EditType.NoEdit
            GridEXSatr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXSatr.CurrentTable.Columns.Item("CodeMoshtary").Position = 2
            GridEXSatr.CurrentTable.Columns.Item("CodeMoshtary").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            'GridEXSatr.CurrentTable.Columns.Item("FaktorShomareh").Caption = " شماره فاکتور"
            'GridEXSatr.CurrentTable.Columns.Item("FaktorShomareh").Visible = True
            'GridEXSatr.CurrentTable.Columns.Item("FaktorShomareh").Width = 150
            ''GridEXSatr.CurrentTable.Columns.Item("FaktorShomareh").EditType = EditType.NoEdit
            'GridEXSatr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            'GridEXSatr.CurrentTable.Columns.Item("FaktorShomareh").Position = 2
            'GridEXSatr.CurrentTable.Columns.Item("FaktorShomareh").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXSatr.CurrentTable.Columns.Item("StrDetailFaktor").Caption = " اطلاعات فاکتورها"
            GridEXSatr.CurrentTable.Columns.Item("StrDetailFaktor").Visible = True
            GridEXSatr.CurrentTable.Columns.Item("StrDetailFaktor").Width = 500
            'GridEXSatr.CurrentTable.Columns.Item("FaktorTarikh").EditType = EditType.NoEdit
            GridEXSatr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXSatr.CurrentTable.Columns.Item("StrDetailFaktor").Position = 2
            GridEXSatr.CurrentTable.Columns.Item("StrDetailFaktor").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center


            GridEXSatr.CurrentTable.Columns.Item("cc").Caption = "cc"
            GridEXSatr.CurrentTable.Columns.Item("cc").Visible = False
            GridEXSatr.CurrentTable.Columns.Item("cc").Width = 0
            'GridEXSatr.CurrentTable.Columns.Item("FaktorTarikh").EditType = EditType.NoEdit
            GridEXSatr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXSatr.CurrentTable.Columns.Item("cc").Position = 3
            GridEXSatr.CurrentTable.Columns.Item("cc").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

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

    Private Sub SetGridSatr1()
        If dt11.Rows.Count = 0 Then
            Exit Sub
        End If

        Try
            With GridEXSatr1
                .DataSource = Nothing
                .DataSource = dt11_Satr.DefaultView
                .SetDataBinding(dt11_Satr.DefaultView, "")
                .RetrieveStructure()
            End With

            For i As Integer = 0 To GridEXSatr1.CurrentTable.Columns.Count - 1
                GridEXSatr1.CurrentTable.Columns.Item(i).Visible = False
            Next

            GridEXSatr1.CurrentTable.Columns.Item("Mablagh").Caption = "مبلغ چک"
            GridEXSatr1.CurrentTable.Columns.Item("Mablagh").Visible = True
            GridEXSatr1.CurrentTable.Columns.Item("Mablagh").Width = 270
            'GridEXSatr1.CurrentTable.Columns.Item("Mablagh").EditType = EditType.NoEdit
            GridEXSatr1.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXSatr1.CurrentTable.Columns.Item("Mablagh").Position = 0
            GridEXSatr1.CurrentTable.Columns.Item("Mablagh").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            'GridEXSatr1.CurrentTable.Columns.Item("Mablagh").HeaderAlignment = TextAlignment.Center

            GridEXSatr1.CurrentTable.Columns.Item("TarikhSanad").Caption = "تاریخ سررسید چک"
            GridEXSatr1.CurrentTable.Columns.Item("TarikhSanad").Visible = True
            GridEXSatr1.CurrentTable.Columns.Item("TarikhSanad").Width = 230
            'GridEXSatr1.CurrentTable.Columns.Item("TarikhSanad").EditType = EditType.NoEdit
            GridEXSatr1.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXSatr1.CurrentTable.Columns.Item("TarikhSanad").Position = 1
            GridEXSatr1.CurrentTable.Columns.Item("TarikhSanad").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            'GridEXSatr1.CurrentTable.Columns.Item("TarikhSanad").HeaderAlignment = TextAlignment.Center

            GridEXSatr1.CurrentTable.Columns.Item("ShomarehSanad").Caption = "شماره چک"
            GridEXSatr1.CurrentTable.Columns.Item("ShomarehSanad").Visible = True
            GridEXSatr1.CurrentTable.Columns.Item("ShomarehSanad").Width = 230
            'GridEXSatr1.CurrentTable.Columns.Item("ShomarehSanad").EditType = EditType.NoEdit
            GridEXSatr1.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXSatr1.CurrentTable.Columns.Item("ShomarehSanad").Position = 2
            GridEXSatr1.CurrentTable.Columns.Item("ShomarehSanad").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            'GridEXSatr1.CurrentTable.Columns.Item("ShomarehSanad").HeaderAlignment = TextAlignment.Center

            GridEXSatr1.CurrentTable.Columns.Item("ccTitr").Caption = "ccTitr"
            GridEXSatr1.CurrentTable.Columns.Item("ccTitr").Visible = False
            GridEXSatr1.CurrentTable.Columns.Item("ccTitr").Width = 0
            'GridEXSatr1.CurrentTable.Columns.Item("ccTitr").EditType = EditType.NoEdit
            GridEXSatr1.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            'GridEXSatr1.CurrentTable.Columns.Item("ccTitr").Position = 4
            GridEXSatr1.CurrentTable.Columns.Item("ccTitr").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            'GridEXSatr1.CurrentTable.Columns.Item("ccTitr").HeaderAlignment = TextAlignment.Cente


            For i As Integer = 0 To GridEXSatr1.RootTable.Columns.Count - 1
                If GridEXSatr1.RootTable.Columns(i).Type.IsValueType Then
                    GridEXSatr1.RootTable.Columns(i).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                    GridEXSatr1.RootTable.Columns(i).FormatMode = Janus.Windows.GridEX.FormatMode.UseIFormattable
                    GridEXSatr1.RootTable.Columns(i).FormatString = "G"
                    GridEXSatr1.RootTable.Columns(i).TotalFormatMode = Janus.Windows.GridEX.FormatMode.UseIFormattable
                    GridEXSatr1.RootTable.Columns(i).TotalFormatString = "G"
                End If
            Next

            Me.CenterToScreen()
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> SetGridSatr ")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> SetGridSatr ")
        End Try
    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub GridEXSatr_DoubleClick(sender As Object, e As EventArgs) Handles GridEXSatr.DoubleClick
        If GridEXSatr.RowCount = 0 Then Exit Sub
        If (GridEXSatr.CurrentRow.Cells("ccTitr").Value) = 0 Then Exit Sub
        If (GridEXSatr.CurrentRow.Cells("ccTitr").Value) <> 0 Then
            ccTitr = (GridEXSatr.CurrentRow.Cells("ccTitr").Value)
            strCCFaktor = (GridEXSatr.CurrentRow.Cells("StrccFaktor").Value)
            Me.Close()
        End If

    End Sub

    Private Sub GridEXSatr_Click(sender As Object, e As EventArgs) Handles GridEXSatr.Click
        If GridEXSatr.RowCount = 0 Then Exit Sub
        Dim cnSQL As New SqlConnection
        Dim cmSQL As New SqlCommand
        Dim da As New SqlDataAdapter
        Dim strSQL As String = ""


        Using cn As New SqlConnection(ConnectionString)
            Using cm As SqlCommand = cn.CreateCommand()
                cn.Open()
                cm.Parameters.Clear()
                cm.CommandType = CommandType.StoredProcedure
                cm.CommandText = "[Treasury].[RaasGiri_SearchCheckFaktor_Nahaee]"
                cm.Parameters.AddWithValue("CheckTaeed", CheckTaeed)
                cm.Parameters.AddWithValue("CodeFard", UserCode)
                cm.Parameters.AddWithValue("Noe", 2)
                cm.Parameters.AddWithValue("cc", GridEXSatr.CurrentRow.Cells("cc").Value)
                da.SelectCommand = cm
                dt11_Satr = New DataTable
                da.Fill(dt11_Satr)
            End Using
        End Using
        SetGridSatr1()
    End Sub
End Class