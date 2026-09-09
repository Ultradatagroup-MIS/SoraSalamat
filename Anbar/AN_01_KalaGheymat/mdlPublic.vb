Module mdlPublic
    '---Global----------------------------------------------------
    Public objTools As New UD_Dll.mdlUtility
    Public objTarikh As New UD_Dll.Tarikh
    Public objSec As New ud_dll.security
    Public objSearch As New UD_Dll.Search
    Public ObjCode As New UD_Dll.Code

    ' Public ConnectionString As String = "server=172.16.0.60;initial catalog=DB_PAKHSH;user id =sa;pwd =DrwebDDc1"
    Public ConnectionString As String = objTools.GetConnectionString
    ' Input Parameter 
    Public UserName As String '1
    Public UserCode As Long  '2   CodeFard
    Public UserPassWord As String   '3
    Public NameMahalFaal As String = "" '4 
    Public CodeMahalFaal As Long  '5    
    Public PersonelCode As Long  '6       ShomarehPersonely
    Public PersonelName As String  '7
    Public CodeDoreh As Long  '8    

    Public TarikhEmrooz As String = objTarikh.Mi2Sh(Today)
    Public CodeSherkat As Long = 1
    Public NameSherkat As String = objTools.ConvertNulls(objTools.DLookup("NameSherkat", "tblGL_Sherkat", "CodeSherkat = " & CodeSherkat), "")

    Public rptPath As String = objTools.ConvertNulls(objTools.DLookup("ReportPath", "tblGL_Sherkat", "CodeSherkat = " & CodeSherkat), "")
    '---Search Moshtary-------------------------------------------
    Public SearchItem As String
    Public MultiSelection As Boolean

    Public tCodeMoshtary As String
    Public tccMoshtary As String
    Public tNameMoshtary As String

    Friend WithEvents CRV As CrystalDecisions.Windows.Forms.CrystalReportViewer
End Module
