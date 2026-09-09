Module mdlPublic
    '---Global----------------------------------------------------
    Public objTools As New UD_Dll.mdlUtility
    Public objTarikh As New UD_Dll.Tarikh
    Public objSec As New ud_dll.Security
    Public objSearch As New UD_Dll.Search
    Public ObjCode As New UD_Dll.Code

    Public ConnectionString As String = objTools.GetConnectionString


    ' Input Parameter 
    Public UserName As String '1
    Public UserCode As Long  '2   CodeFard
    Public UserPassWord As String   '3
    Public CodeDoreh As Long = objTools.ConvertNulls(objTools.DLookup("CodeDoreh", "tblGL_DorehFaal", "UserName = '" & UserName & "'"), 0)
    Public CodeDorehAval As Long = objTools.ConvertNulls(objTools.DLookup(" Top 1 CodeDoreh", "tblGL_Doreh", ""), 0)
    Public CodeMahalFaal As Long = objTools.ConvertNulls(objTools.DLookup("CodeMahal", "tblGL_MahalFaal", "UserName = '" & UserName & "'"), 1)
    Public NameMahalFaal As String = objTools.ConvertNulls(objTools.DLookup("NameMahal", "tblGL_MarkazPakhsh", "CodeMahal = " & CodeMahalFaal), "")
    Public PersonelCode As Long  '6       ShomarehPersonely
    Public PersonelName As String  '7


    Public TarikhEmrooz As String = objTarikh.Mi2Sh(Today)
    Public EmSal As Integer = objTarikh.ShamsiYear(TarikhEmrooz)
    Public CodeSherkat As Long = 1

    '---Hesabdary-------------------------------------------------
    'Codeing
    Public NoeTafsily As UD_Dll.Enums.HE_Tafsily
    Public LastCodeNoeTafsily As Long = 0
    Public CodeGoroh As String = ""
    Public GstrCodGoroh As String = ""
    Public CodeKol As String = ""
    Public GstrCodekol As String = ""
    Public CodeSanad As Long = 0

    ''Manabe

    ''---Daryaft Pardakht------------------------------------------
    Public Const cntSandogh As Long = 54



End Module
