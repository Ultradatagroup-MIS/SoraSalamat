Module mdlPublic

    '---Global----------------------------------------------------
    Public objTools As New UD_Dll.mdlUtility
    Public objTarikh As New UD_Dll.Tarikh
    Public objSec As New ud_dll.security
    Public objSearch As New UD_Dll.Search
    Public Objcode As New UD_Dll.Code

    Public ConnectionString As String = objTools.GetConnectionString

    ' Input Parameter 
    Public UserName As String '1
    Public UserCode As Long  '2   CodeFard
    Public UserPassWord As String   '3
    Public NameMahalFaal As String = "" '4 
    Public CodeMahalFaal As Long = 0 '5    
    Public PersonelCode As Long  '6       ShomarehPersonely
    Public PersonelName As String  '7
    Public CodeDoreh As Long = 0 '8    


    Public SearchItem As String
    Public MultiSelection As Boolean
    Public TarikhEmrooz As String = objTarikh.Mi2Sh(Today)

End Module
