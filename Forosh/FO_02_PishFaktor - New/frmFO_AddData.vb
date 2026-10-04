


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
    Dim ccPishfaktorTitr As Integer = 0
    Dim ccMoshtaryAddress As Integer = 0
    Dim CodeFard As Integer = 0

#End Region
    Private Sub frmFO_AddData_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        With GridEXSatr
            .AllowEdit = Janus.Windows.GridEX.InheritableBoolean.True
            .AllowAddNew = Janus.Windows.GridEX.InheritableBoolean.True
            .UpdateMode = Janus.Windows.GridEX.UpdateMode.RowUpdate
            .TabKeyBehavior = Janus.Windows.GridEX.TabKeyBehavior.ControlNavigation
            .Enabled = False          ' تا قبل از ذخیره‌ی هدر غیرفعال
        End With
    End Sub

    Private Sub SetGridSatr()


        Try
            With GridEXSatr
                .DataSource = Nothing
                .DataSource = dt_SearchSatr.DefaultView
                .SetDataBinding(dt_SearchSatr.DefaultView, "")
                .RetrieveStructure()

            End With
            For i As Integer = 0 To GridEXSatr.CurrentTable.Columns.Count - 1
                GridEXSatr.CurrentTable.Columns.Item(i).Width = 150
                GridEXSatr.CurrentTable.Columns.Item("ccKala").Visible = False
                GridEXSatr.CurrentTable.Columns.Item("ccPishFaktorSatr").Visible = False
            Next



        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->SetGridStyle")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->SetGridStyle")
        End Try

    End Sub

    Private Sub btnSaveSanad_Click(sender As Object, e As EventArgs) Handles btnSaveSanad.Click
        AddNewRecord()
        If ccPishfaktorTitr = 0 Then Exit Sub
        SearchSatr()
        GridEXSatr.Enabled = True
        GridEXSatr.Focus()
        GridEXSatr.MoveToNewRecord()         ' می‌ره روی ردیف خالی آخر
        GridEXSatr.Col = 0

    End Sub

    Private Sub AddNewRecord()


        Dim da As SqlDataAdapter = New SqlDataAdapter

        Using cn As New SqlConnection(ConnectionString)
            Using cm As SqlCommand = cn.CreateCommand()
                cn.Open()
                cm.Parameters.Clear()
                cm.CommandType = CommandType.StoredProcedure
                cm.CommandText = "[Sales].[spPishFaktorVorodKoli_InsertTitr]"
                cm.Parameters.AddWithValue("CodeMahal", CodeMahalFaal)
                cm.Parameters.AddWithValue("CodeDoreh", CodeDoreh)
                cm.Parameters.AddWithValue("PishFaktorTarikh", mskTarikh.Text)
                cm.Parameters.AddWithValue("ccMoshtary", txtCodeMoshtary.Tag)
                cm.Parameters.AddWithValue("ccMoshtaryAddress", ccMoshtaryAddress)
                cm.Parameters.AddWithValue("ccForoshandeh", cmbBazaryab.SelectedValue)
                cm.Parameters.AddWithValue("CodeFard", CodeFard)
                cm.Parameters.AddWithValue("sNoePardakht", cmbNoePardakht.SelectedValue)
                cm.Parameters.AddWithValue("ModatCheck", Val(txtModatCheck.Text))
                cm.Parameters.AddWithValue("Malyat", 1)
                cm.Parameters.AddWithValue("PishFaktorAmani", 0)
                cm.Parameters.AddWithValue("PishFaktorGheireGhateei", 0)
                cm.Parameters.AddWithValue("Tozihat", txtTozihat.Text.TrimEnd)
                cm.Parameters.AddWithValue("NoeVorod", 1)
                cm.Parameters.AddWithValue("ccAnbar", 1)
                cm.Parameters.AddWithValue("UserName", UserName)

                Dim pOut As SqlParameter = cm.Parameters.Add("@ccPishFaktorTitr", SqlDbType.Int)
                pOut.Direction = ParameterDirection.Output
                cm.ExecuteNonQuery()

                If pOut.Value IsNot DBNull.Value Then
                    ccPishfaktorTitr = Convert.ToInt32(pOut.Value)
                End If
            End Using
        End Using

        SetGridSatr()
    End Sub
    Private Sub SearchSatr()



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



                StrSqlKala = "Select  CodeKala,NameKala,ccKala,txtsVahedeShomaresh,sVahedeShomaresh,NameBrand,RadifBrand,0 as IsSabadKala "

                StrSqlKala &= " from qryAN_Kala"
                StrSqlKala &= " Where Faal = 1  AND ccKala in (Select ccKala from tblAN_KalaGheymat where ccKala =qryAN_Kala.ccKala )  "




                MultiSelection = False
                SearchItem = "CodeKala"
                objKala.SetForm(StrSqlKala)
                objKala.ShowDialog()

                GridEXSatr.CurrentRow.Cells("CodeKala").Value = objKala.tcodeKala
                GridEXSatr.CurrentRow.Cells("ccKala").Value = objKala.tccKala

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




        End If

    End Sub





    Private Sub GridEXSatr_AddingRecord(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles GridEXSatr.AddingRecord
        If Not SaveRow(GridEXSatr.GetRow(), True) Then e.Cancel = True
    End Sub
    Private Sub GridEX1_UpdatingRecord(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles GridEXSatr.UpdatingRecord
        If Not SaveRow(GridEXSatr.GetRow(), False) Then e.Cancel = True
    End Sub

    Private Function SaveRow(row As Janus.Windows.GridEX.GridEXRow, isNew As Boolean) As Boolean
        ' اعتبارسنجی
        If row.Cells("ccKala").Value Is Nothing OrElse IsDBNull(row.Cells("ccKala").Value) Then
            MessageBox.Show("کالا را انتخاب کنید")
            Return False
        End If

        Try
            Using con As New SqlConnection(ConnectionString)
                Using cmd As SqlCommand = con.CreateCommand()
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.CommandText = "[Sales].[spPishFaktorVorodKoli_InsertSatr]"
                    cmd.Parameters.AddWithValue("ccPishfaktorTitr", ccPishfaktorTitr)
                    cmd.Parameters.AddWithValue("ccPishfaktorSatr", If(row.Cells("ccPishfaktorSatr").Value, DBNull.Value))
                    cmd.Parameters.AddWithValue("ccKala", row.Cells("ccKala").Value)
                    cmd.Parameters.AddWithValue("Tedad3", If(row.Cells("Tedad3").Value, DBNull.Value))
                    cmd.Parameters.AddWithValue("Fee", If(row.Cells("Fee").Value, DBNull.Value))
                    cmd.Parameters.AddWithValue("DarsadTakhfif", If(row.Cells("DarsadTakhfif").Value, DBNull.Value))



                    Dim outId As New SqlParameter("@NewccPishfaktorSatr", SqlDbType.Int) With {
                        .Direction = ParameterDirection.Output}
                    cmd.Parameters.Add(outId)

                    con.Open()
                    cmd.ExecuteNonQuery()

                    If isNew AndAlso Not IsDBNull(outId.Value) Then
                        row.Cells("ccPishfaktorSatr").Value = outId.Value
                    End If
                End Using
            End Using
            dt_SearchSatr.AcceptChanges()
            Return True
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Return False
        End Try
    End Function


    Private Sub AddNewSatr()


        Using cn As New SqlConnection(ConnectionString)
            Using cm As SqlCommand = cn.CreateCommand()
                cn.Open()
                cm.Parameters.Clear()
                cm.CommandType = CommandType.StoredProcedure
                cm.CommandText = "[Sales].[spPishFaktorVorodKoli_InsertSatr]"
                cm.Parameters.AddWithValue("ccPishFaktorTitr", ccPishfaktorTitr)
                cm.Parameters.AddWithValue("ccKala", GridEXSatr.CurrentRow.Cells("ccKala").Value)
                cm.Parameters.AddWithValue("Tedad3", GridEXSatr.CurrentRow.Cells("Tedad3").Value)
                cm.Parameters.AddWithValue("Fee", GridEXSatr.CurrentRow.Cells("Fee").Value)
                cm.Parameters.AddWithValue("DarsadTakhfif", GridEXSatr.CurrentRow.Cells("DarsadTakhfif").Value)

                cm.CommandTimeout = 999999
                cm.ExecuteNonQuery()
            End Using
        End Using
    End Sub


End Class