


Public Class frmFO_AddData

#Region "Variable AND Constant Declration"
    'Const cntCodeSubSystem As Long = 625

    Dim cmTitr As CurrencyManager
    Dim ErrPro As New ErrorProvider
    Dim dsForm As New DataSet
    Dim dtP As New DataTable
    Dim dr As DataRow
    Dim dvForm As DataTable
    Private SN As Integer
    Dim txtCaption As String
    Private WithEvents BS As New UD_Dll.PassString

    Private LastRowIndex As Integer = -1

#End Region
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
                GridEXSatr.CurrentTable.Columns.Item("ccKala").Visible = False
                GridEXSatr.CurrentTable.Columns.Item("MKOL3").Visible = False
                'GridEXTitr.CurrentTable.Columns.Item("ccPishFaktor").Visible = False
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

    Private Sub AddNewRecord()



        Dim da As SqlDataAdapter = New SqlDataAdapter

        'Using cn As New SqlConnection(ConnectionString)
        '    Using cm As SqlCommand = cn.CreateCommand()
        '        cn.Open()
        '        cm.Parameters.Clear()
        '        cm.CommandType = CommandType.StoredProcedure
        '        cm.CommandText = "[Sales].[spPishFaktorVorodKoli_InsertTitr]"
        '        cm.Parameters.AddWithValue("ccPishFaktorTitr", ccPishFaktorTitr)
        '        cm.Parameters.AddWithValue("ccPishFaktorTitr", ccPishFaktorTitr)
        '        cm.Parameters.AddWithValue("ccPishFaktorTitr", ccPishFaktorTitr)
        '        cm.Parameters.AddWithValue("ccPishFaktorTitr", ccPishFaktorTitr)
        '        cm.Parameters.AddWithValue("ccPishFaktorTitr", ccPishFaktorTitr)
        '        cm.Parameters.AddWithValue("ccPishFaktorTitr", ccPishFaktorTitr)
        '        cm.Parameters.AddWithValue("ccPishFaktorTitr", ccPishFaktorTitr)
        '        cm.Parameters.AddWithValue("ccPishFaktorTitr", ccPishFaktorTitr)
        '        cm.Parameters.AddWithValue("ccPishFaktorTitr", ccPishFaktorTitr)
        '        cm.Parameters.AddWithValue("ccPishFaktorTitr", ccPishFaktorTitr)
        '        cm.Parameters.AddWithValue("ccPishFaktorTitr", ccPishFaktorTitr)
        '        da.SelectCommand = cm
        '        cm.CommandTimeout = 999999
        '        dt_SearchSatr = New DataTable
        '        da.Fill(dt_SearchSatr)
        '    End Using
        'End Using

        SetGridSatr()
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

    Private Sub GridEXSatr_KeyDown(sender As Object, e As KeyEventArgs) Handles GridEXSatr.KeyDown
        If e.KeyCode = Keys.F2 Then

            If GridEXSatr.CurrentColumn Is Nothing Then Exit Sub

            If GridEXSatr.CurrentColumn.Key = "CodeKala" Then

                Dim objKala As New Forms_dll.frmAN_KalaSearch
                Dim StrSqlKala As String



                'If objTools.DLookup("PishFaktorAmani", "tblFO_PishFaktor", "ccPishFaktorTitr = " & Val(GridEXTitr.CurrentRow.Cells("ccPishFaktorTitr").Text.Replace(",", ""))) = True Then
                '    ccAnbarForosh = objTools.DLookup("ccAnbar", "tblFO_PishFaktor", "ccPishFaktorTitr = " & Val(GridEXTitr.CurrentRow.Cells("ccPishFaktorTitr").Text.Replace(",", "")))
                'Else
                '    ccAnbarForosh = objTools.ConvertNulls(objTools.DLookup("CodeAnbar", "tblAN_Anbar", "AnbarAsly = 1 And CodeMahal = " & CodeMahalFaal), 0)
                'End If

                'Dim ccLine As Integer = 0
                'ccLine = objTools.ConvertNulls(objTools.DLookup("ccLine", "Sales.LineSatr", "Type = 3 AND PK = " & Val(GridEXTitr.CurrentRow.Cells("ccForoshandeh").Text.Replace(",", ""))), 0)

                StrSqlKala = "Select  CodeKala,NameKala,ccKala,txtsVahedeShomaresh,sVahedeShomaresh,NameBrand,RadifBrand,0 as IsSabadKala "

                StrSqlKala &= " from qryAN_Kala"
                StrSqlKala &= " Where Faal = 1  AND ccKala in (Select ccKala from tblAN_KalaGheymat where ccKala =qryAN_Kala.ccKala )  "




                MultiSelection = False
                SearchItem = "CodeKala"
                objKala.SetForm(StrSqlKala)
                objKala.ShowDialog()

                GridEXSatr.CurrentRow.Cells("CodeKala").Value = objKala.tcodeKala
                GridEXSatr.CurrentRow.Cells("ccKala").Value = objKala.tccKala
                'GridEXSatr.CurrentRow.Cells("Fee").Value = ObjCode.GetMablaghForosh_NoePardakht(objKala.tccKala, GridEXTitr.CurrentRow.Cells("PishFaktorTarikh").Text _
                '          , CodeMahalFaal, GridEXTitr.CurrentRow.Cells("ccMoshtary").Value, GridEXTitr.CurrentRow.Cells("sNoePardakht").Value)
                GridEXSatr.CurrentRow.Cells("Fee").Value = 50000
                MultiSelection = False


            End If

        End If
        If e.KeyCode = Keys.Tab Then

            If GridEXSatr.CurrentRow Is Nothing Then Exit Sub
            If GridEXSatr.CurrentColumn.Key = "CodeKala" Then


                Dim ccKala = GridEXSatr.CurrentRow.Cells("ccKala").Value


                If ccKala Is Nothing OrElse ccKala.ToString().Trim() = "" Then
                    MessageBox.Show("کد کالا را وارد کنید")
                    e.Handled = True
                    Exit Sub
                End If
            End If

            'If GridEXSatr.CurrentColumn.Key = "Tedad3" Then
            '    Dim tedad = GridEXSatr.CurrentRow.Cells("Tedad3").Value.

            '    If tedad Is Nothing OrElse tedad.ToString().Trim() = "" Then
            '        MessageBox.Show("تعداد را وارد کنید")
            '        e.Handled = True
            '        Exit Sub
            '    End If
            'End If


        End If

    End Sub

    Private Sub GridEXSatr_CurrentCellChanged(sender As Object, e As EventArgs) Handles GridEXSatr.CurrentCellChanged
        'Dim CurrentRowIndex As Integer = GridEXSatr.CurrentRow.RowIndex

        ''اگر ردیف عوض شده
        'If LastRowIndex <> -1 AndAlso CurrentRowIndex <> LastRowIndex Then

        '    'اینجا Validation ردیف قبلی
        '    Dim OldRow As Janus.Windows.GridEX.GridEXRow =
        '        GridEXSatr.GetRow(LastRowIndex)

        '    'فعلاً برای تست
        '    MessageBox.Show("Validation ردیف " & LastRowIndex)

        'End If

        ''ردیف فعلی را ذخیره کن
        'LastRowIndex = CurrentRowIndex


        If GridEXSatr.CurrentRow.Selected = False Then
            ' میره ردیف بدی
        End If

        If GridEXSatr.CurrentRow.Selected = True Then
            If GridEXSatr.CurrentRow.RowType = Janus.Windows.GridEX.RowType.NewRecord Then
                If GridEXSatr.CurrentColumn.Index = 3 And GridEXSatr.CurrentRow.Cells(2).Value = 0 Then
                    MessageBox.Show("کد کالا را وارد کنید")
                    Exit Sub
                End If

            End If
        End If


        'If GridEXSatr.CurrentRow.RowType = Janus.Windows.GridEX.RowType.Record Then
        '    MessageBox.Show("فعلی ردیف " & LastRowIndex)
        'End If
    End Sub

End Class