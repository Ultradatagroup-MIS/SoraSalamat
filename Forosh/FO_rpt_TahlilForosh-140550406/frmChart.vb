Imports System.Windows.Forms

Public Class frmChart
    Public dv As DataView
    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub
    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub
    Private Sub frmChart_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Chart1.Series("Series1").Points.DataBindXY(dv, "SharhGorohForosh", dv, "Tedad")
        'Chart1.DataSource = dv
    End Sub

End Class
