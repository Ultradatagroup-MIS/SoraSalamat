Module mdlPublic
    '---Global----------------------------------------------------
    Public objTools As New UD_Dll.mdlUtility
    Public objTarikh As New UD_Dll.Tarikh
    Public objSec As New ud_dll.Security
    Public ObjCode As New UD_Dll.Code

    'Public objSearch As New mdlSearch

    Public ConnectionString As String = objTools.GetConnectionString
    Public UserName As String
    Public UserPassWord As String
    Public UserCode As Long  'CodeFard
    Public PersonelName As String
    Public PersonelCode As Long  'ShomarehPersonely
    Public TarikhEmrooz As String = objTarikh.Mi2Sh(Today)
    Public EmSal As Integer = objTarikh.ShamsiYear(TarikhEmrooz)
    Public CodeDoreh As Long = 0 'objTools.ConvertNulls(objTools.DLookup("CodeDoreh", "tblGL_DorehFaal", "UserName = '" & UserName & "'"), 0)
    Public CodeDorehAval As Long = objTools.ConvertNulls(objTools.DLookup(" Top 1 CodeDoreh", "tblGL_Doreh", ""), 0)
    Public CodeMahalFaal As Long = 0 'objTools.ConvertNulls(objTools.DLookup("CodeMahal", "tblGL_MahalFaal", "UserName = '" & UserName & "'"), 1)
    Public NameMahalFaal As String = "" 'objTools.ConvertNulls(objTools.DLookup("NameMahal", "tblGL_MarkazPakhsh", "CodeMahal = " & CodeMahalFaal), "")
    Public appPath As String = Application.StartupPath
    Public CodeSherkat As Long = 1
    Public rptPath As String = objTools.ConvertNulls(objTools.DLookup("ReportPath", "tblGL_Sherkat", "CodeSherkat = " & CodeSherkat), "")
    Public imgPath As String = objTools.ConvertNulls(objTools.DLookup("ImagePath", "tblGL_Sherkat", "CodeSherkat = " & CodeSherkat), "")
    Public NameSherkat As String = objTools.ConvertNulls(objTools.DLookup("NameSherkat", "tblGL_Sherkat", "CodeSherkat = " & CodeSherkat), "")
    Public TarikhRoozGhabl As String = ""

    Public azTarikh As String
    Public taTarikh As String

    Public flg_Kala As Boolean
    Public ccKala As Integer
    Public NameKala As String
    Public Codekala As String
    Public Vahed As String

    Public SearchItem As String
    Public MultiSelection As Boolean
End Module
