Module mdlPublic
    '---Global----------------------------------------------------
    Public objTools As New UD_Dll.mdlUtility
    Public objTarikh As New UD_Dll.Tarikh
    Public objSec As New ud_dll.Security
    Public ObjCode As New UD_Dll.Code

    'Public objSearch As New mdlSearch
    Public CodeShomarehHesab As Integer = 0
    'Public SharhShomarehHesab As String
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
    Public IsRemote As Boolean = False
    'Public CodeSystemReport As Long = 0
    'Public NameSystemReport As String = ""
    Public Taraf_afrad As Long ' 1 Moshtary , 2 TaminKonandeh
    '---Hesabdary-------------------------------------------------
    'Codeing
    'Public NoeTafsily As ud_dll.Enums.UD_Dll.Enums.Ud_dll.Enums.HE_Tafsily
    Public LastCodeNoeTafsily As Long = 0
    Public CodeGoroh As String = ""
    Public GstrCodGoroh As String = ""
    Public CodeKol As String = ""
    Public GstrCodekol As String = ""
    Public CodeSanad As Long = 0
    Public ccTitrSanad As Long = 0
    'Manabe
    Public tNoeManba As Long
    '---Daryaft Pardakht------------------------------------------
    Public Const cntSandogh As Long = 54
    Public tCheck_Safteh As Long = 0

    Public ccAmalyat As Long
    Public MablaghAmalyat As Long

    'Khazaneh - Naghd Be Sandogh
    Public AvarandehVajh As Integer
    Public ManabeDaryaft As Integer
    Public CodeHesab As Integer
    Public ShomarehResid As String
    Public ShomarehH As Integer

    ' Naghdo check -- Faktor
    ' 1 Naghd --- 2 Check
    Public NoePardakht As Byte

    Public azTarikh As String
    Public taTarikh As String
    Public TarikhRoozGhabl As String

    '---Forosh----------------------------------------------------
    ' Moshtary
    Public ccMoshtary As Long
    Public NameMoshtary As String
    Public ccShomarehHesab As Integer
    Public ShomarehHesab As String
    'TaminKonandeh
    Public ccTaminKonandeh As Integer
    Public NameTaminKonandeh As String
    'Masir
    Public ccMasir As Integer
    Public NameForoshandeh As String
    '---Personely-------------------------------------------------
    Public tCodeFard As Long

    '---Search Moshtary-------------------------------------------
    Public SearchItem As String
    Public MultiSelection As Boolean

    Public tccShomarehHesab As String
    Public tccMoshtary As String
    Public tNameMoshtary As String

    '---Search Faktor---------------

    Public BedBank As Long
    Public BesBAnk As Long
    Public MandehBank As Long

    '------------------------------
    Public tFaktorShomareh As String
    Public tFaktorCodeDoreh As String
    Public tFaktorccForoshandeh As String
    Public tFaktorccMoshtary As String


End Module
