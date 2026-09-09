Public Class frmFO_SelectAnbar

    Dim dsForm As DataSet
    Private Sub frmFO_SelectAnbar_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadCombo()
    End Sub
    Private Sub LoadCombo()
            Dim Strsql As String
            Dim cn As SqlConnection
            Dim cm As SqlCommand
            Dim daSQL As SqlDataAdapter

            Try

                If dsForm.Tables.Contains("tblAnbar") Then
                    dsForm.Tables.Remove("tblAnbar")
                End If

                Strsql = "Global.spAnbar_LoadCombo"

                cn = New SqlConnection(ConnectionString)
                cn.Open()

                cm = New SqlCommand(Strsql, cn)
                cm.CommandType = CommandType.StoredProcedure
                cm.Parameters.Clear()

                cm.Parameters.AddWithValue("CodeMahal", CodeMahalFaal)

                daSQL = New SqlDataAdapter(cm)
                daSQL.Fill(dsForm, "tblAnbar")

                cmbAnbar.DataSource = Nothing
                cmbAnbar.Items.Clear()
                cmbAnbar.DataSource = dsForm.Tables("tblAnbar").DefaultView
                cmbAnbar.DisplayMember = "NameAnbar"
                cmbAnbar.ValueMember = "CodeAnbar"

                cm = Nothing
                daSQL = Nothing

                cn.Close()

            Catch sqlExc As SqlException
                MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> LoadComboAnbar ")
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> LoadComboAnbar ")
            End Try
    End Sub
    Private Sub btnTaeed_Click(sender As Object, e As EventArgs) Handles btnTaeed.Click
        If MsgBox(" آیا از انتخاب خود مطمئن هستید ؟ ", MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2 + MsgBoxStyle.Question + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.MsgBoxRight, "خروج") = MsgBoxResult.No Then
            Exit Sub
        Else
            frmFO_SodorFaktorAmani.ccAnbarMarjoee = cmbAnbar.SelectedValue
            Me.Close()
        End If
    End Sub
    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        If MsgBox(" در صورت خروج مرجوعی ثبت نخواهد شد . آیا میخواهید خارج شوید ؟ ", MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2 + MsgBoxStyle.Question + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.MsgBoxRight, "خروج") = MsgBoxResult.No Then
            Exit Sub
        Else
            Me.Close()
        End If
    End Sub
End Class