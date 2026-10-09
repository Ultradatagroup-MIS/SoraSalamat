Public Class frmFO_PishFaktorSearch

    Private _dt As DataTable
    Private _selectedId As Integer = 0

    Public ReadOnly Property SelectedId() As Integer
        Get
            Return _selectedId
        End Get
    End Property
    Private Sub frmFO_PishFaktorSearch_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadList()
    End Sub
    Private Sub LoadList()
        _dt = New DataTable()
        Using cn As New SqlConnection(ConnectionString)
            Using cm As SqlCommand = cn.CreateCommand()
                cm.CommandType = CommandType.StoredProcedure
                cm.CommandText = "[Sales].[spPishFaktorVorodKoli_SearchTitr]"
                cm.CommandTimeout = 999999
                cm.Parameters.AddWithValue("@ccPishFaktorTitr", DBNull.Value)
                Dim da As New SqlDataAdapter(cm)
                da.Fill(_dt)
            End Using
        End Using

        With GridEXList
            .AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False
            .AllowAddNew = Janus.Windows.GridEX.InheritableBoolean.False
            .AllowDelete = Janus.Windows.GridEX.InheritableBoolean.False
            .SetDataBinding(_dt.DefaultView, "")
            .RetrieveStructure()
            For i As Integer = 0 To .RootTable.Columns.Count - 1
                .RootTable.Columns(i).Visible = False
            Next
            If .RootTable.Columns.Contains("ccPishFaktorTitr") Then
                .RootTable.Columns("ccPishFaktorTitr").Visible = False
            End If

            'GridEXList.CurrentTable.Columns.Item("Taeed").Caption = "انتخاب"
            'GridEXList.CurrentTable.Columns.Item("Taeed").Visible = True
            'GridEXList.CurrentTable.Columns.Item("Taeed").Width = 20
            'GridEXList.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            'GridEXList.CurrentTable.Columns.Item("Taeed").Position = 0
            'GridEXList.CurrentTable.Columns.Item("Taeed").Selectable = True
            'GridEXList.CurrentTable.Columns.Item("Taeed").ActAsSelector = True
            'GridEXList.CurrentTable.Columns.Item("Taeed").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXList.CurrentTable.Columns.Item("RadifT").Caption = "ردیف"
            GridEXList.CurrentTable.Columns.Item("RadifT").Visible = True
            GridEXList.CurrentTable.Columns.Item("RadifT").Width = 40
            GridEXList.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXList.CurrentTable.Columns.Item("RadifT").Position = 1
            GridEXList.CurrentTable.Columns.Item("RadifT").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXList.CurrentTable.Columns.Item("PishFaktorTarikhSlash").Caption = "تاريخ"
            GridEXList.CurrentTable.Columns.Item("PishFaktorTarikhSlash").Visible = True
            GridEXList.CurrentTable.Columns.Item("PishFaktorTarikhSlash").Width = 80
            GridEXList.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXList.CurrentTable.Columns.Item("PishFaktorTarikhSlash").Position = 2
            GridEXList.CurrentTable.Columns.Item("PishFaktorTarikhSlash").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXList.CurrentTable.Columns.Item("PishFaktorShomareh").Caption = "شماره پ فاکتور"
            GridEXList.CurrentTable.Columns.Item("PishFaktorShomareh").Visible = True
            GridEXList.CurrentTable.Columns.Item("PishFaktorShomareh").Width = 70
            GridEXList.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXList.CurrentTable.Columns.Item("PishFaktorShomareh").Position = 3
            GridEXList.CurrentTable.Columns.Item("PishFaktorShomareh").FormatString = "N"
            GridEXList.CurrentTable.Columns.Item("PishFaktorShomareh").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXList.CurrentTable.Columns.Item("NameForoshandeh").Caption = "نام فروشنده"
            GridEXList.CurrentTable.Columns.Item("NameForoshandeh").Visible = True
            GridEXList.CurrentTable.Columns.Item("NameForoshandeh").Width = 150
            GridEXList.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXList.CurrentTable.Columns.Item("NameForoshandeh").Position = 4
            GridEXList.CurrentTable.Columns.Item("NameForoshandeh").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXList.CurrentTable.Columns.Item("CodeMoshtary").Caption = "کد مشتری"
            GridEXList.CurrentTable.Columns.Item("CodeMoshtary").Visible = True
            GridEXList.CurrentTable.Columns.Item("CodeMoshtary").Width = 80
            GridEXList.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXList.CurrentTable.Columns.Item("CodeMoshtary").Position = 5
            GridEXList.CurrentTable.Columns.Item("CodeMoshtary").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXList.CurrentTable.Columns.Item("NameMoshtary").Caption = "نام مشتری"
            GridEXList.CurrentTable.Columns.Item("NameMoshtary").Visible = True
            GridEXList.CurrentTable.Columns.Item("NameMoshtary").Width = 170
            GridEXList.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXList.CurrentTable.Columns.Item("NameMoshtary").Position = 6
            GridEXList.CurrentTable.Columns.Item("NameMoshtary").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXList.CurrentTable.Columns.Item("NameTablo").Caption = "نام تابلو"
            GridEXList.CurrentTable.Columns.Item("NameTablo").Visible = True
            GridEXList.CurrentTable.Columns.Item("NameTablo").Width = 150
            GridEXList.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXList.CurrentTable.Columns.Item("NameTablo").Position = 7
            GridEXList.CurrentTable.Columns.Item("NameTablo").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXList.CurrentTable.Columns.Item("txtNoePardakht").Caption = "تسویه"
            GridEXList.CurrentTable.Columns.Item("txtNoePardakht").Visible = True
            GridEXList.CurrentTable.Columns.Item("txtNoePardakht").Width = 50
            GridEXList.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXList.CurrentTable.Columns.Item("txtNoePardakht").Position = 8
            GridEXList.CurrentTable.Columns.Item("txtNoePardakht").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXList.CurrentTable.Columns.Item("ModatCheck").Caption = "مدت چک"
            GridEXList.CurrentTable.Columns.Item("ModatCheck").Visible = True
            GridEXList.CurrentTable.Columns.Item("ModatCheck").Width = 70
            GridEXList.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXList.CurrentTable.Columns.Item("ModatCheck").Position = 9
            GridEXList.CurrentTable.Columns.Item("ModatCheck").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
        End With
    End Sub

    Private Sub GridEXList_DoubleClick(ByVal sender As Object, ByVal e As EventArgs) Handles GridEXList.DoubleClick
        PickCurrent()
    End Sub

    Private Sub GridEXList_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles GridEXList.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.Handled = True
            PickCurrent()
        End If
    End Sub

    Private Sub PickCurrent()
        Dim row As Janus.Windows.GridEX.GridEXRow = GridEXList.CurrentRow
        If row Is Nothing OrElse row.RowType <> Janus.Windows.GridEX.RowType.Record Then Exit Sub

        Dim v As Object = row.Cells("ccPishFaktorTitr").Value
        If v Is Nothing OrElse IsDBNull(v) Then Exit Sub

        _selectedId = Convert.ToInt32(v)
        Me.DialogResult = DialogResult.OK
        Me.Close()
    End Sub
End Class