Public Class Havale_Search

    Public str As String
    Dim ds As New DataSet
    Dim dvForm As New DataView
    Dim cmTitr As CurrencyManager
    Private Sub Havale_Search_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        search()
    End Sub
    Private Sub search()
        Dim cn As New SqlConnection
        Dim cm As New SqlCommand
        Dim da As New SqlDataAdapter
        Dim strSQL As String

        strSQL = "Sales.spDarkhastForoshandehSayar_SearchHavaleh "

        cn = New SqlConnection(ConnectionString)

        cm = New SqlCommand(strSQL, cn)
        cm.CommandType = CommandType.StoredProcedure
        cm.Parameters.Clear()

        cm.Parameters.AddWithValue("CodeMahal", CodeMahalFaal)
        cm.Parameters.AddWithValue("CodeDoreh", CodeDoreh)
        cm.Parameters.AddWithValue("ccForoshandehSayar", frmFO_PishFaktorForoshandehSayar.cmbBazaryab.SelectedValue)

        da = New SqlDataAdapter(cm)
        da.Fill(ds, "HETitr")
        dvForm = New DataView
        dvForm = ds.Tables("HETitr").DefaultView
        dvForm.Sort = "ShomarehForm"
        dvForm.AllowDelete = True
        dvForm.AllowEdit = False
        dvForm.AllowNew = False

        SetGridStyle()
    End Sub
    Private Sub SetGridStyle()

        Try
            If dvForm.Count = 0 Then
                MsgBox("برای این فروشنده حواله ای وجود ندارد .", MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " پیام")
                Me.Close()
            End If

            With GridEXTitr
                .DataSource = Nothing
                .DataSource = ds.Tables("HETitr").DefaultView
                .SetDataBinding(ds.Tables("HETitr").DefaultView, "")
                .RetrieveStructure()
            End With

            For i As Integer = 0 To GridEXTitr.CurrentTable.Columns.Count - 1
                GridEXTitr.CurrentTable.Columns.Item(i).Visible = False
            Next

            GridEXTitr.CurrentTable.Columns.Item("NameForoshandeh").Caption = "نام فروشنده"
            GridEXTitr.CurrentTable.Columns.Item("NameForoshandeh").Visible = True
            GridEXTitr.CurrentTable.Columns.Item("NameForoshandeh").Width = 200
            GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXTitr.CurrentTable.Columns.Item("NameForoshandeh").Position = 0
            GridEXTitr.CurrentTable.Columns.Item("NameForoshandeh").FormatString = "N"
            GridEXTitr.CurrentTable.Columns.Item("NameForoshandeh").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXTitr.CurrentTable.Columns.Item("ShomarehForm").Caption = "شماره حوالــه"
            GridEXTitr.CurrentTable.Columns.Item("ShomarehForm").Visible = True
            GridEXTitr.CurrentTable.Columns.Item("ShomarehForm").Width = 100
            GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXTitr.CurrentTable.Columns.Item("ShomarehForm").Position = 1
            GridEXTitr.CurrentTable.Columns.Item("ShomarehForm").FormatString = "###,###"
            GridEXTitr.CurrentTable.Columns.Item("ShomarehForm").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXTitr.CurrentTable.Columns.Item("TarikhForm").Caption = "تاريخ حوالــه"
            GridEXTitr.CurrentTable.Columns.Item("TarikhForm").Visible = True
            GridEXTitr.CurrentTable.Columns.Item("TarikhForm").Width = 80
            GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXTitr.CurrentTable.Columns.Item("TarikhForm").Position = 2
            GridEXTitr.CurrentTable.Columns.Item("TarikhForm").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXTitr.CurrentTable.Columns.Item("JamDarkhast").Caption = "مبلغ حوالــه"
            GridEXTitr.CurrentTable.Columns.Item("JamDarkhast").Visible = True
            GridEXTitr.CurrentTable.Columns.Item("JamDarkhast").Width = 120
            GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXTitr.CurrentTable.Columns.Item("JamDarkhast").Position = 3
            GridEXTitr.CurrentTable.Columns.Item("JamDarkhast").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXTitr.CurrentTable.Columns.Item("ccDarkhastForoshandehSayarTitr").Caption = "مبلغ حوالــه"
            GridEXTitr.CurrentTable.Columns.Item("ccDarkhastForoshandehSayarTitr").Visible = False
            GridEXTitr.CurrentTable.Columns.Item("ccDarkhastForoshandehSayarTitr").Width = 150
            GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXTitr.CurrentTable.Columns.Item("ccDarkhastForoshandehSayarTitr").Position = 4
            GridEXTitr.CurrentTable.Columns.Item("ccDarkhastForoshandehSayarTitr").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            For i As Integer = 0 To GridEXTitr.RootTable.Columns.Count - 1
                If GridEXTitr.RootTable.Columns(i).Type.IsValueType Then
                    GridEXTitr.RootTable.Columns(i).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                    GridEXTitr.RootTable.Columns(i).FormatMode = Janus.Windows.GridEX.FormatMode.UseIFormattable
                    GridEXTitr.RootTable.Columns(i).FormatString = "N"
                    GridEXTitr.RootTable.Columns(i).TotalFormatMode = Janus.Windows.GridEX.FormatMode.UseIFormattable
                    GridEXTitr.RootTable.Columns(i).TotalFormatString = "N"
                End If
            Next

            GridEXTitr.Visible = True
            GridEXTitr.HeaderFormatStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->SetGridStyle")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->SetGridStyle")
        End Try

    End Sub

    Private Sub Havale_Search_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
        frmFO_PishFaktorForoshandehSayar.Show()
    End Sub

    Private Sub GridEXTitr_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GridEXTitr.DoubleClick
        cmTitr = CType(BindingContext(GridEXTitr.DataSource), CurrencyManager)
        frmFO_PishFaktorForoshandehSayar.txtShomarehHavaleh.Text = dvForm(cmTitr.Position)("ShomarehForm")
        frmFO_PishFaktorForoshandehSayar.txtShomarehHavaleh.Tag = dvForm(cmTitr.Position)("ccDarkhastForoshandehSayarTitr")
        Me.Close()
    End Sub
End Class