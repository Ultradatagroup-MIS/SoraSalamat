Imports System.Windows.Forms

Public Class frmFee
    Public fee As Double
    Public NameKala As String
    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        fee = Val(txtFee.Text)

        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub
    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub frmFee_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        lblNameKala.Text = NameKala
    End Sub
End Class
