Module mdlPublic
    '---Global----------------------------------------------------
    Public objTools As New UD_Dll.mdlUtility
    Public objTarikh As New UD_Dll.Tarikh
    Public objSec As New UD_Dll.Security
    Public objSearch As New UD_Dll.Search
    Public ObjCode As New UD_Dll.Code

    Public UserPassWord As String
    Public UserCode As Long  'CodeFard
    Public ConnectionString As String = objTools.GetConnectionString
    Public UserName As String
    Public TarikhEmrooz As String = objTarikh.Mi2Sh(Today)
    Public EmSal As Integer = objTarikh.ShamsiYear(TarikhEmrooz)
    Public CodeDoreh As Long = objTools.ConvertNulls(objTools.DLookup("CodeDoreh", "tblGL_DorehFaal", "UserName = '" & UserName & "'"), 0)
    Public CodeDorehAval As Long = objTools.ConvertNulls(objTools.DLookup(" Top 1 CodeDoreh", "tblGL_Doreh", ""), 0)
    Public CodeMahalFaal As Long = objTools.ConvertNulls(objTools.DLookup("CodeMahal", "tblGL_MahalFaal", "UserName = '" & UserName & "'"), 1)
    Public NameMahalFaal As String = objTools.ConvertNulls(objTools.DLookup("NameMahal", "tblGL_MarkazPakhsh", "CodeMahal = " & CodeMahalFaal), "")
    Public appPath As String = Application.StartupPath
    Public CodeSherkat As Long = 1
    Public rptPath As String = objTools.ConvertNulls(objTools.DLookup("ReportPath", "tblGL_Sherkat", "CodeSherkat = " & CodeSherkat), "")
    Public imgPath As String = objTools.ConvertNulls(objTools.DLookup("ImagePath", "tblGL_Sherkat", "CodeSherkat = " & CodeSherkat), "")
    Public NameSherkat As String = objTools.ConvertNulls(objTools.DLookup("NameSherkat", "tblGL_Sherkat", "CodeSherkat = " & CodeSherkat), "")

    ''---Search Moshtary-------------------------------------------
    Public SearchItem As String
    Public MultiSelection As Boolean



    Public azTarikh, taTarikh, TarikhRoozGhabl As String


    Public sOstan As Integer
    Public txtOstan As String
    Public sShahr As Integer
    Public txtShahr As String
    Public sMantagheh As Integer
    Public txtMantagheh As String
    Public sMahaleh As Integer
    Public txtMahaleh As String
    Public ccMoshtary As Long
    Public NameMoshtary As String

    Public BedOstan As Long
    Public BesOstan As Long
    Public MandehOstan As Long

    Public BedShahr As Long
    Public BesShahr As Long
    Public MandehShahr As Long

    Public BedMantagheh As Long
    Public BesMantagheh As Long
    Public MandehMantagheh As Long

    Public BedMahaleh As Long
    Public BesMahaleh As Long
    Public MandehMahaleh As Long

    Public BedMoshtary As Long
    Public BesMoshtary As Long
    Public MandehMoshtary As Long


    Public flg_moshtary As Boolean

    Public tCodeMoshtary As String
    Public tccMoshtary As String
    Public tNameMoshtary As String

    Public StrtxtNoeSenf As String
    Public StrNoeSenf As String

    Public StrNoeMoshtary As String

    Public StrMahaleh As String
    Public StrMasir As String



    Public PersonelName As String
    Public PersonelCode As Long  'ShomarehPersonely
   


End Module
