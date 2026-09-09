Imports System.Windows.Forms

Public Class frm_SelectTafsily
    Dim dsForm As New DataSet

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        If IsNothing(lstMoshtary.SelectedValue) Then
            Exit Sub
        End If

        Me.Tag = lstMoshtary.SelectedValue
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub
    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub
    Private Sub lstMoshtary_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstMoshtary.DoubleClick
        Me.Tag = lstMoshtary.SelectedIndex + 1
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub lstMoshtary_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstMoshtary.SelectedIndexChanged

    End Sub
End Class
