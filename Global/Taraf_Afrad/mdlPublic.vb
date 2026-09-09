Module mdlPublic
    '---Global----------------------------------------------------
    Public objTools As New ud_dll.mdlUtility
    Public objTarikh As New UD_Dll.Tarikh
    Public objSec As New ud_dll.security
    Public objSearch As New ud_dll.security
    Public ObjCode As New UD_Dll.Code

    Public ConnectionString As String = objTools.GetConnectionString

    Public TarikhEmrooz As String = objTarikh.Mi2Sh(Today)
    Public appPath As String = Application.StartupPath
    Public CodeSherkat As Long = 1
    Public Taraf_afrad As Long
    Public rptPath As String = objTools.ConvertNulls(objTools.DLookup("ReportPath", "tblGL_Sherkat", "CodeSherkat = " & CodeSherkat), "")
    Public imgPath As String = objTools.ConvertNulls(objTools.DLookup("ImagePath", "tblGL_Sherkat", "CodeSherkat = " & CodeSherkat), "")

    Public ccMoshtary As Long
    Public NameMoshtary As String
    Public ccTaminKonandeh As Integer
    Public NameTaminKonandeh As String

    Public UserName As String '1
    Public UserCode As Long  '2   CodeFard
    Public UserPassWord As String   '3
    Public NameMahalFaal As String = "" '4 
    Public CodeMahalFaal As Long '5    
    Public PersonelCode As Long  '6       ShomarehPersonely
    Public PersonelName As String  '7
    Public CodeDoreh As Long = 0 '8    

End Module
