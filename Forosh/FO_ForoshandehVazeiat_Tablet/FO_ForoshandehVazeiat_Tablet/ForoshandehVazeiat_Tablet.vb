Imports System.Data
Imports System.Data.SqlClient
Imports Microsoft.VisualBasic
Imports System.IO
Public Class frm_ForoshandehVazeiat_Tablet

    Private Sub frm_ForoshandehVazeiat_Tablet_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ClearForm()
        LoadCombo()

    End Sub
    Private Sub LoadCombo()
        Dim dt As Data.DataTable = Nothing
        Dim da As SqlDataAdapter = New SqlDataAdapter
        Try
            Using cn As New SqlConnection(ConnectionString)
                Using cm As SqlCommand = cn.CreateCommand()
                    cn.Open()
                    cm.Parameters.Clear()
                    cm.CommandType = CommandType.StoredProcedure
                    cm.CommandText = "[Global].[spForoshandehVazeiat_LoadCombo]"
                    da.SelectCommand = cm
                    dt = New DataTable
                    da.Fill(dt)
                End Using
            End Using

            lstForoshandeh.DataSource = dt
            lstForoshandeh.DisplayMember = "NameForoshandeh"
            lstForoshandeh.ValueMember = "ccForoshandeh"

        Catch ex As Exception
            Throw New Exception("Error In--> Load Foroshandeh : " & ex.Message)
        Finally
        End Try
    End Sub
    Private Sub ClearForm()
        lstForoshandeh.DataSource = Nothing
        rbFaal.Checked = False
        rbGheyrFaal.Checked = False
    End Sub

    Private Sub lstForoshandeh_MouseClick(sender As Object, e As MouseEventArgs) Handles lstForoshandeh.MouseClick
        If lstForoshandeh.SelectedItems.Count = 0 Then Exit Sub

        Vazeiat = objTools.DLookup("ISNULL(RM,0)", "tblFo_Foroshandeh", "ccForoshandeh = " & lstForoshandeh.SelectedValue)

        If Vazeiat = 99 Then
            rbFaal.Checked = True
        ElseIf Vazeiat = 100 Then
            rbGheyrFaal.Checked = True
        Else
            rbFaal.Checked = False
            rbGheyrFaal.Checked = False
        End If

    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If lstForoshandeh.SelectedItems.Count = 0 Then
            MsgBox("ابتدا یک فروشنده را انتخاب کنید", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRtlReading + vbMsgBoxRight, "")
        Else
            If rbFaal.Checked = True Then
                VazeiatInsert = 99
            ElseIf rbGheyrFaal.Checked = True Then
                VazeiatInsert = 100
            End If

            objTools.DUpdate("RM", "tblFO_Foroshandeh", VazeiatInsert.ToString, "ccForoshandeh = " & lstForoshandeh.SelectedValue)
            MsgBox("عملیات با موفقیت انجام شد.", MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.OkOnly + MsgBoxStyle.Information, "")

            ClearForm()
            LoadCombo()
        End If
    End Sub
End Class
