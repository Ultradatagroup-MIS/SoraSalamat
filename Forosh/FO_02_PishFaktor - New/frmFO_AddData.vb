Public Class frmFO_AddData
    Private Sub frmFO_AddData_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub SetGridSatr()


        Try
            With GridEXSatr
                .DataSource = Nothing
                .DataSource = dt_SearchSatr.DefaultView
                .SetDataBinding(dt_SearchSatr.DefaultView, "")
                .RetrieveStructure()
                .AllowEdit = Janus.Windows.GridEX.InheritableBoolean.True
                .AllowAddNew = Janus.Windows.GridEX.InheritableBoolean.True
                .AllowDelete = Janus.Windows.GridEX.InheritableBoolean.True
                .NewRowPosition = Janus.Windows.GridEX.NewRowPosition.BottomRow
            End With
            For i As Integer = 0 To GridEXSatr.CurrentTable.Columns.Count - 1
                GridEXSatr.CurrentTable.Columns.Item(i).Width = 150

            Next



        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->SetGridStyle")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->SetGridStyle")
        End Try

    End Sub

    Private Sub btnSaveSanad_Click(sender As Object, e As EventArgs) Handles btnSaveSanad.Click
        'AddNewRecord()
        SearchSatr()

    End Sub
    Private Sub SearchSatr()

        Dim ccPishFaktorTitr As Integer = 1

        Dim da As SqlDataAdapter = New SqlDataAdapter

        Using cn As New SqlConnection(ConnectionString)
            Using cm As SqlCommand = cn.CreateCommand()
                cn.Open()
                cm.Parameters.Clear()
                cm.CommandType = CommandType.StoredProcedure
                cm.CommandText = "[Sales].[spPishFaktorVorodKoli_SearchSatr]"
                cm.Parameters.AddWithValue("ccPishFaktorTitr", ccPishFaktorTitr)
                da.SelectCommand = cm
                cm.CommandTimeout = 999999
                dt_SearchSatr = New DataTable
                da.Fill(dt_SearchSatr)
            End Using
        End Using

        SetGridSatr()
    End Sub
End Class