Public Class frmSelectHadaf

    Private Sub btnTopDown_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTopDown.Click

        Me.Hide()
        Dim frm1 As New frmFO_HadafForosh
        frm1.ShowDialog()

    End Sub
    Private Sub btnButtonUp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnButtonUp.Click

        Me.Hide()
        Dim frm4 As New frmFO_HadafForosh_ButtonUp
        frm4.ShowDialog()

    End Sub
    Private Sub frmSelectHadaf_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Me.Close()
    End Sub

End Class