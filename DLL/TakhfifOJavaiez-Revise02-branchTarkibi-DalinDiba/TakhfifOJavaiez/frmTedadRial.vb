Public Class frmTedadRial
    Public Enum MarjoeeTypes
        Tedady
        Rialy
    End Enum

    Private _MarjoeeType As MarjoeeTypes = MarjoeeTypes.Tedady
    Public Property MarjoeeType() As MarjoeeTypes
        Get
            Return _MarjoeeType
        End Get
        Set(ByVal value As MarjoeeTypes)
            _MarjoeeType = value
        End Set
    End Property

    Private Sub btnTaeed_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTaeed.Click

    End Sub

    Private Sub rbRial_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbRial.CheckedChanged
        If rbRial.Checked Then
            Me.MarjoeeType = MarjoeeTypes.Rialy
        Else
            Me.MarjoeeType = MarjoeeTypes.Tedady
        End If
    End Sub
End Class