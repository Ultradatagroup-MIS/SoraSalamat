Module mdlPublic
    Public objTarikh As New UD_Dll.Tarikh
    Public objtools As New UD_Dll.mdlUtility
    Public ConnectionString As String = objtools.GetConnectionString
    Public ObjCode As New UD_Dll.Code

    ' Input Parameter 

    Public UserName As String '1
    Public UserCode As Long  '2   CodeFard
    Public UserPassWord As String   '3
    Public NameMahalFaal As String = "" '4 
    Public CodeMahalFaal As Long  '5    
    Public PersonelCode As Long  '6       ShomarehPersonely
    Public PersonelName As String  '7
    Public TarikhEmrooz As String = objTarikh.Mi2Sh(Today)
    Public CodeDoreh As Long  '8 

End Module
