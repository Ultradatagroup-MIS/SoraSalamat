Public Class frmNoeBankVagozari
    Dim dsForm As New DataSet
    Private Sub frmNoeBankVagozari_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        LoadCombo()
    End Sub
    Private Sub LoadCombo()
        Dim Strsql As String : Dim daSQL As SqlDataAdapter
        Strsql = "Select * From tblGL_ShenasehOmomi Where CodeAsli = 6 and CodeFarei <> 0 Or Code = 0 and Dideh = 1 order by Sharh"
        daSQL = New SqlDataAdapter(Strsql, ConnectionString)
        daSQL.Fill(dsForm, "ComboBank")
        cmbBankSanad.DataSource = Nothing
        cmbBankSanad.Items.Clear()
        cmbBankSanad.DataSource = dsForm.Tables("ComboBank").DefaultView
        cmbBankSanad.DisplayMember = "Sharh"
        cmbBankSanad.ValueMember = "MeghdarAdadi"
        daSQL = Nothing
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub btnTaed_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTaed.Click
        Try
            If cmbBankSanad.SelectedIndex = 21 Then
                Me.TopMost = False
                Dim dt As DateTime = objTarikh.SetDateSlash(objTarikh.Sh2Mi(frmDP_TaghirVazeiatCheckha.mskAzTarikh.Text))

                Dim frm As New frmDP_VagozariBank
                Me.Hide()
                frm.ShowDialog(Me)
                frm = Nothing
                Me.Show()
                Me.TopMost = True
            End If
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->btnPrintM_Click")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->btnPrintM_Click")
        End Try
    End Sub
End Class