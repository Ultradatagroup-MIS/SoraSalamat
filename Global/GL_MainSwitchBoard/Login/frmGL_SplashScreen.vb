Public NotInheritable Class frmGL_SplashScreen
    Private Sub SplashScreen_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Timer1.Enabled = True
    End Sub
    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Timer1.Enabled = False
        Me.Hide()
        frmGL_Login.ShowDialog()
        'frmGL_Vorod.ShowDialog()
        Me.Close()
    End Sub
End Class
