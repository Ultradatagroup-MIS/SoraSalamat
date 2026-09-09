Public Class frmAddLineTitr
    Public CodeMahal As Integer
    Dim dsForm As New DataSet
    Dim ErrPro As New ErrorProvider
    Private Sub frmAddLineTitr_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadComboSarparast()
    End Sub
    Private Sub LoadComboSarparast()
        Dim cnSQL As New SqlConnection
        Dim cmSQL As New SqlCommand
        Dim daSQL As New SqlDataAdapter
        Dim strSQL As String = ""

        Try
            strSQL = "Sales.spLine_LoadComboSarparast "

            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            cmSQL.Parameters.AddWithValue("CodeMahal", CodeMahalFaal)

            daSQL = New SqlDataAdapter(cmSQL)
            daSQL.Fill(dsForm, "tblSarparast")

            cmbSarparast.DataSource = Nothing
            cmbSarparast.DataSource = dsForm.Tables("tblSarparast").DefaultView
            cmbSarparast.ValueMember = "CodeFard"
            cmbSarparast.DisplayMember = "NameSarparast"

            cmSQL = Nothing
            daSQL = Nothing
            cnSQL.Close()

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> LoadComboSarparast")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> LoadComboSarparast")
        End Try
    End Sub
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If Not Save() Then
            Exit Sub
        Else
            frmFO_Line.AddNewLine = True
            Me.Close()
        End If
    End Sub
    Private Function Save() As Boolean
        Save = False

        If Not IsValid() Then
            Exit Function
        End If

        Dim cnSQL As New SqlConnection
        Dim cmSQL As New SqlCommand
        Dim daSQL As New SqlDataAdapter
        Dim strSQL As String = ""

        Try
            strSQL = "Sales.spLine_InsertTitr "

            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            cmSQL.Parameters.AddWithValue("CodeMahal", CodeMahalFaal)
            cmSQL.Parameters.AddWithValue("NameLine", txtNameLine.Text.Trim)
            cmSQL.Parameters.AddWithValue("CodeFardSarparast", cmbSarparast.SelectedValue)

            cmSQL.ExecuteNonQuery()

            cmSQL = Nothing
            cnSQL.Close()

            Save = True

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> Save")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> Save")
        End Try
    End Function
    Private Function IsValid() As Boolean
        IsValid = False

        Try
            If txtNameLine.Text.Trim = "" Then
                MsgBox("لطفا نام لاین را وارد نمایید .", MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.OkOnly, "")
                ErrPro.SetError(txtNameLine, "لطفا نام لاین را وارد نمایید .")
                txtNameLine.Focus()
                Exit Function
            End If
            ErrPro.SetError(txtNameLine, "")

            If cmbSarparast.SelectedIndex = -1 Then
                MsgBox("لطفا سرپرست لاین را انتخاب نمایید .", MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.OkOnly, "")
                ErrPro.SetError(cmbSarparast, "لطفا سرپرست لاین را انتخاب نمایید .")
                cmbSarparast.Focus()
                Exit Function
            End If
            ErrPro.SetError(cmbSarparast, "")

            IsValid = True

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> IsValid")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> IsValid")
        End Try
    End Function
    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub
End Class