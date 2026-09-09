Module mdlPublic
    Public objTools As New UD_Dll.mdlUtility
    Public objTarikh As New UD_Dll.Tarikh
    Public ConnectionString As String = objTools.GetConnectionString
    Public TarikhEmrooz As String = objTarikh.GetDateSlash(objTarikh.Mi2Sh(Today))
End Module
