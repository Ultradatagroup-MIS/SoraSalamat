Public Class BonusEventArg

    Public Enum ReturnProductPrizeModes
        Tedady
        Rialy
    End Enum

    Public Sub New(ByVal ReturnProductPrize As Boolean)
        _RequireProductPrize = ReturnProductPrize
    End Sub


    Private _RequireProductPrize As Boolean
    Public ReadOnly Property RequireProductPrize() As Boolean
        Get
            Return _RequireProductPrize
        End Get
    End Property


    Private _ReturnProductPrizeMode As ReturnProductPrizeModes
    Public Property ReturnProductPrizeMode() As ReturnProductPrizeModes
        Get
            Return _ReturnProductPrizeMode
        End Get
        Set(ByVal value As ReturnProductPrizeModes)
            _ReturnProductPrizeMode = value
        End Set
    End Property

End Class
