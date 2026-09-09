Module mdlPublic
    '---Global----------------------------------------------------

    Public objTools As New ud_dll.mdlUtility
    Public objTarikh As New UD_Dll.Tarikh
    Public objSec As New UD_Dll.Security
    Public objSearch As New UD_Dll.Search
    Public ObjCode As New UD_Dll.Code

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
    Public TarikhEmrooz As String = objTarikh.Mi2Sh(Today)


    Public drKala As DataRowView


End Module
