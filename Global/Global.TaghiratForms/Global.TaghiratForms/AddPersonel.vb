Imports System.Data
Imports System.Data.SqlClient
Imports Microsoft.VisualBasic
Imports System.IO
Public Class AddPersonel
    Public ccPersonel As Integer
    Private Sub AddPersonel_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Search()
        ClearForm()
    End Sub
    Private Sub Search()
        Dim dtForm As New DataTable
        Dim dt As Data.DataTable = Nothing
        Dim da As SqlDataAdapter = New SqlDataAdapter

        Try
            Using cn As New SqlConnection(ConnectionString)
                Using cm As SqlCommand = cn.CreateCommand()
                    cn.Open()
                    cm.Parameters.Clear()
                    cm.CommandType = CommandType.StoredProcedure
                    cm.CommandText = "[Global].[spTaghiratForm_LoadPersonel]"
                    da.SelectCommand = cm
                    dt = New DataTable
                    da.Fill(dt)
                End Using
            End Using
            dtForm = dt
            SetGridStyle(dtForm)

        Catch ex As Exception
            Throw New Exception("Error In--> Search Grid : " & ex.Message)
        Finally
        End Try
    End Sub
    Private Sub ClearForm()
        txtFName.Text = ""
        txtLName.Text = ""
        Mode = 0
    End Sub
    Private Sub SetGridStyle(dtForm As DataTable)
        With GridPersonel
            .DataSource = Nothing
            .DataSource = dtForm
            .SetDataBinding(dtForm, "")
            .RetrieveStructure()
        End With

        For i As Integer = 0 To GridPersonel.CurrentTable.Columns.Count - 1
            GridPersonel.CurrentTable.Columns.Item(i).Visible = False
        Next

        GridPersonel.CurrentTable.Columns.Item("ccPersonel").Caption = "کد سیستمی"
        GridPersonel.CurrentTable.Columns.Item("ccPersonel").Visible = True
        GridPersonel.CurrentTable.Columns.Item("ccPersonel").Width = 130
        GridPersonel.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
        GridPersonel.CurrentTable.Columns.Item("ccPersonel").Position = 0
        GridPersonel.CurrentTable.Columns.Item("ccPersonel").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

        GridPersonel.CurrentTable.Columns.Item("NamePersonel").Caption = "نام پرسنل"
        GridPersonel.CurrentTable.Columns.Item("NamePersonel").Visible = True
        GridPersonel.CurrentTable.Columns.Item("NamePersonel").Width = 305
        GridPersonel.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
        GridPersonel.CurrentTable.Columns.Item("NamePersonel").Position = 1
        GridPersonel.CurrentTable.Columns.Item("NamePersonel").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

    End Sub

    Private Sub GridPersonel_MouseClick(sender As Object, e As MouseEventArgs) Handles GridPersonel.MouseClick
        If GridPersonel.SelectedItems.Count = 0 Then
            ' MsgBox("تامین کننده ثبت نشده است", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRtlReading + vbMsgBoxRight, "")
            Exit Sub
        Else
            ccPersonel = Val(Me.GridPersonel.CurrentRow.Cells("ccPersonel").Text.Replace(",", "").Trim)
        End If
    End Sub

    Private Sub GridPersonel_DoubleClick(sender As Object, e As EventArgs) Handles GridPersonel.DoubleClick
        Dim dtForm As New DataTable
        Dim dt As Data.DataTable = Nothing
        Dim da As SqlDataAdapter = New SqlDataAdapter

        Try
            Using cn As New SqlConnection(ConnectionString)
                Using cm As SqlCommand = cn.CreateCommand()
                    cn.Open()
                    cm.Parameters.Clear()
                    cm.CommandType = CommandType.StoredProcedure
                    cm.CommandText = "[Global].[spTaghiratForm_LoadPersonel_Row]"
                    cm.Parameters.AddWithValue("ccPersonel", ccPersonel)
                    da.SelectCommand = cm
                    dt = New DataTable
                    da.Fill(dt)
                End Using
            End Using
            dtForm = dt

            txtFName.Text = dtForm.Rows(0)("FName").ToString
            txtLName.Text = dtForm.Rows(0)("LName").ToString
            Mode = 1

        Catch ex As Exception
            Throw New Exception("Error In--> Search Edit Row : " & ex.Message)
        Finally
        End Try
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If IsValid() = False Then Exit Sub
        If Mode = 0 Then
            InsertPersonel()
        ElseIf Mode = 1 Then
            UpdatePersonel()
        End If
        ClearForm()
        Search()
    End Sub
    Private Function IsValid() As Boolean
        If txtFName.Text = "" Then
            MsgBox("نام را وارد کنید", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Critical, "خطا")
            Return False
        End If
        If txtLName.Text = "" Then
            MsgBox("نام خانوادگی را وارد کنید", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Critical, "خطا")
            Return False
        End If
        Return True
    End Function
    Private Sub InsertPersonel()
        Using cn As New SqlConnection(ConnectionString)
            Using cm As SqlCommand = cn.CreateCommand()
                cm.CommandType = CommandType.StoredProcedure
                cm.CommandText = "[Global].[spTaghiratForm_InsertPersonel]"
                cm.Parameters.AddWithValue("FName", txtFName.Text.Trim)
                cm.Parameters.AddWithValue("LName", txtLName.Text.Trim.Trim)
                cm.Connection.Open()
                cm.ExecuteNonQuery()
                cm.Connection.Close()
                cm.Parameters.Clear()
            End Using
        End Using
    End Sub

    Private Sub UpdatePersonel()

        Using cn As New SqlConnection(ConnectionString)
            Using cm As SqlCommand = cn.CreateCommand()
                cm.CommandType = CommandType.StoredProcedure
                cm.CommandText = "[Global].[spTaghiratForm_UpdatePersonel]"
                cm.Parameters.AddWithValue("FName", txtFName.Text.Trim)
                cm.Parameters.AddWithValue("LName", txtLName.Text.Trim.Trim)
                cm.Parameters.AddWithValue("ccPersonel", ccPersonel)
                cm.Connection.Open()
                cm.ExecuteNonQuery()
                cm.Connection.Close()
                cm.Parameters.Clear()
            End Using
        End Using

    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        ClearForm()
    End Sub
End Class