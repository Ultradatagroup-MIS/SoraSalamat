Imports UD_Dll

Public Class frmShowFaktor
#Region "Variable AND Constant Declration"
    Dim dvForm As DataView
    Dim ErrPro As New ErrorProvider
    Dim dsForm As New DataSet
    Dim dt11 As DataTable = Nothing
    Dim dt11_Faktor As DataTable = Nothing

    Dim dtFaktorForAdd As DataTable = Nothing

    Public AzTarikh As String
    Public TaTarikh As String
#End Region
    Private Sub frmFO_ShowDetailsPishFaktor_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SearchFaktorForAdd()
    End Sub

    Private Sub SearchFaktorForAdd()


        Dim da As SqlDataAdapter = New SqlDataAdapter
        Try
            Using cn As New SqlConnection(ConnectionString)
                Using cm As SqlCommand = cn.CreateCommand()
                    cn.Open()
                    cm.Parameters.Clear()
                    cm.CommandType = CommandType.StoredProcedure
                    cm.CommandText = "[Treasury].[RaasGiri_SearchFaktor_ForAdd]"
                    cm.Parameters.AddWithValue("CodeMahal", CodeMahalFaal)
                    cm.Parameters.AddWithValue("ccTitr", ccTitr)
                    cm.Parameters.AddWithValue("AzTarikh", AzTarikh)
                    cm.Parameters.AddWithValue("TaTarikh", TaTarikh)
                    da.SelectCommand = cm
                    dtFaktorForAdd = New DataTable
                    da.Fill(dtFaktorForAdd)
                End Using
            End Using
            SetGridStyleFaktorForAdd(dtFaktorForAdd.DefaultView)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Search")
        Finally
        End Try
    End Sub

    Private Sub SetGridStyleFaktorForAdd(ByVal dv As DataView)
        Try
            With GridEXFaktor
                .DataSource = Nothing
                .DataSource = dtFaktorForAdd
                .SetDataBinding(dtFaktorForAdd, "")
                .RetrieveStructure()
            End With
            For i As Integer = 0 To GridEXFaktor.CurrentTable.Columns.Count - 1
                GridEXFaktor.CurrentTable.Columns.Item(i).Visible = False
            Next

            'GridEXFaktor.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False
            GridEXFaktor.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic



            'GridEXFaktor.CurrentTable.Columns.Item("Taeed").Caption = "انتخاب"
            'GridEXFaktor.CurrentTable.Columns.Item("Taeed").Visible = True
            'GridEXFaktor.CurrentTable.Columns.Item("Taeed").Width = 50
            'GridEXFaktor.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            'GridEXFaktor.CurrentTable.Columns.Item("Taeed").Position = 0
            'GridEXFaktor.CurrentTable.Columns.Item("Taeed").Selectable = True
            'GridEXFaktor.CurrentTable.Columns.Item("Taeed").ActAsSelector = True
            'GridEXFaktor.CurrentTable.Columns.Item("Taeed").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXFaktor.CurrentTable.Columns.Item("FaktorTarikhSlash").Caption = "تاریخ فاکتور"
            GridEXFaktor.CurrentTable.Columns.Item("FaktorTarikhSlash").Visible = True
            GridEXFaktor.CurrentTable.Columns.Item("FaktorTarikhSlash").Width = 100
            GridEXFaktor.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXFaktor.CurrentTable.Columns.Item("FaktorTarikhSlash").Position = 1
            GridEXFaktor.CurrentTable.Columns.Item("FaktorTarikhSlash").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXFaktor.CurrentTable.Columns.Item("FaktorShomareh").Caption = "شماره فاکتور"
            GridEXFaktor.CurrentTable.Columns.Item("FaktorShomareh").Visible = True
            GridEXFaktor.CurrentTable.Columns.Item("FaktorShomareh").Width = 100
            GridEXFaktor.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXFaktor.CurrentTable.Columns.Item("FaktorShomareh").Position = 2
            GridEXFaktor.CurrentTable.Columns.Item("FaktorShomareh").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center


            GridEXFaktor.RootTable.Columns.Item("NameMoshtary").Caption = "نام مشتری"
            GridEXFaktor.RootTable.Columns.Item("NameMoshtary").Visible = True
            GridEXFaktor.RootTable.Columns.Item("NameMoshtary").Width = 120
            GridEXFaktor.RootTable.Columns.Item("NameMoshtary").Position = 3
            GridEXFaktor.RootTable.Columns.Item("NameMoshtary").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXFaktor.RootTable.Columns("NameMoshtary").FilterEditType = Janus.Windows.GridEX.FilterEditType.Combo



            GridEXFaktor.CurrentTable.Columns.Item("CodeMoshtary").Caption = "کد مشتری"
            GridEXFaktor.CurrentTable.Columns.Item("CodeMoshtary").Visible = True
            GridEXFaktor.CurrentTable.Columns.Item("CodeMoshtary").Width = 100
            GridEXFaktor.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXFaktor.CurrentTable.Columns.Item("CodeMoshtary").Position = 4
            GridEXFaktor.CurrentTable.Columns.Item("CodeMoshtary").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center



            GridEXFaktor.CurrentTable.Columns.Item("Modatcheck").Caption = "مدت چک"
            GridEXFaktor.CurrentTable.Columns.Item("Modatcheck").Visible = True
            GridEXFaktor.CurrentTable.Columns.Item("Modatcheck").Width = 150
            GridEXFaktor.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXFaktor.CurrentTable.Columns.Item("Modatcheck").Position = 5
            GridEXFaktor.RootTable.Columns.Item("Modatcheck").FormatString = "N"
            GridEXFaktor.CurrentTable.Columns.Item("Modatcheck").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXFaktor.CurrentTable.Columns.Item("MandehF").Caption = "مبلغ مانده فاکتور"
            GridEXFaktor.CurrentTable.Columns.Item("MandehF").Visible = True
            GridEXFaktor.CurrentTable.Columns.Item("MandehF").Width = 150
            GridEXFaktor.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXFaktor.CurrentTable.Columns.Item("MandehF").Position = 6
            GridEXFaktor.RootTable.Columns.Item("MandehF").FormatString = "N"
            GridEXFaktor.CurrentTable.Columns.Item("MandehF").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center



            GridEXFaktor.CurrentTable.Columns.Item("JamKol").Caption = "مبلغ خالص فاکتور"
            GridEXFaktor.CurrentTable.Columns.Item("JamKol").Visible = True
            GridEXFaktor.CurrentTable.Columns.Item("JamKol").Width = 150
            GridEXFaktor.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXFaktor.CurrentTable.Columns.Item("JamKol").Position = 7
            GridEXFaktor.RootTable.Columns.Item("JamKol").FormatString = "N"
            GridEXFaktor.CurrentTable.Columns.Item("JamKol").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center



            GridEXFaktor.CurrentTable.Columns.Item("ccFaktorTitr").Caption = "ccFaktorTitr"
            GridEXFaktor.CurrentTable.Columns.Item("ccFaktorTitr").Visible = False
            GridEXFaktor.CurrentTable.Columns.Item("ccFaktorTitr").Width = 0
            GridEXFaktor.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            'GridEXFaktor.CurrentTable.Columns.Item("ccFaktorTitr").Position = 13
            GridEXFaktor.CurrentTable.Columns.Item("ccFaktorTitr").TextAlignment = Janus.Windows.GridEX.FilterMode.Automatic


            For i As Integer = 0 To GridEXFaktor.RootTable.Columns.Count - 1
                If GridEXFaktor.RootTable.Columns(i).Type.IsValueType Then
                    GridEXFaktor.RootTable.Columns(i).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                    GridEXFaktor.RootTable.Columns(i).FormatMode = Janus.Windows.GridEX.FormatMode.UseIFormattable
                    GridEXFaktor.RootTable.Columns(i).FormatString = "G"
                    GridEXFaktor.RootTable.Columns(i).TotalFormatMode = Janus.Windows.GridEX.FormatMode.UseIFormattable
                    GridEXFaktor.RootTable.Columns(i).TotalFormatString = "G"
                End If
            Next

            GridEXFaktor.Visible = True
            GridEXFaktor.HeaderFormatStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->SetGridStyle")
        End Try
    End Sub

    Private Sub GridEXFaktor_DoubleClick(sender As Object, e As EventArgs) Handles GridEXFaktor.DoubleClick
        If GridEXFaktor.RowCount = 0 Then
            MessageBox.Show("هیچ فاکتوری وجود ندارد.")

            Exit Sub
        Else



            Dim da As SqlDataAdapter = New SqlDataAdapter
            Using cn As New SqlConnection(ConnectionString)
                Using cm As SqlCommand = cn.CreateCommand()
                    cn.Open()
                    cm.Parameters.Clear()
                    cm.CommandType = CommandType.StoredProcedure
                    cm.CommandText = "[Treasury].[RaasGiri_AddFaktor]"
                    cm.Parameters.AddWithValue("ccTitr", ccTitr)
                    cm.Parameters.AddWithValue("ccFaktorTitr", GridEXFaktor.CurrentRow.Cells("ccFaktorTitr").Value)
                    cm.ExecuteNonQuery()
                End Using
            End Using
        End If
        Me.Close()
    End Sub
End Class