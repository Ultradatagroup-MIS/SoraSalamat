Module mdlPublic

    '---Global----------------------------------------------------
    Public objTools As New UD_Dll.mdlUtility
    Public objTarikh As New UD_Dll.Tarikh
    Public objSec As New ud_dll.security
    Public objCode As New UD_Dll.Code
    Public searc As New UD_Dll.Enums
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


    Public istarikhmiladi As Boolean = 0
    'Public TarikhEmrooz As String = objTarikh.Mi2Sh(Today)
    'Public EmSal As Integer = objTarikh.ShamsiYear(TarikhEmrooz)
    Public appPath As String = Application.StartupPath
    Public CodeSherkat As Long = 1
    Public rptPath As String = objTools.ConvertNulls(objTools.DLookup("ReportPath", "tblGL_Sherkat", "CodeSherkat = " & CodeSherkat), "")
    Public imgPath As String = objTools.ConvertNulls(objTools.DLookup("ImagePath", "tblGL_Sherkat", "CodeSherkat = " & CodeSherkat), "")
    Public NameSherkat As String = objTools.ConvertNulls(objTools.DLookup("NameSherkat", "tblGL_Sherkat", "CodeSherkat = " & CodeSherkat), "")

    Public TarikhEmrooz As String = objTarikh.Mi2Sh(Today)

    '---Search Moshtary-------------------------------------------
    Public SearchItem As String
    Public MultiSelection As Boolean

    Public tCodeMoshtary As String
    Public tccMoshtary As String
    Public tNameMoshtary As String

    Public Function FnTarikhEmrooz() As String
        If istarikhmiladi Then
            Dim TarikhEmrooz As String = DateTime.Now.ToString("yyyyMMdd")
            Return TarikhEmrooz
        Else
            Dim TarikhEmrooz As String = objTarikh.Mi2Sh(Today)
            Return TarikhEmrooz
        End If

    End Function
    Public Function FnEmSal() As String
        If istarikhmiladi Then
            Dim EmSal As Integer = System.DateTime.Now.Year
            Return EmSal
        Else
            Dim EmSal As Integer = objTarikh.ShamsiYear(FnTarikhEmrooz())
            Return EmSal
        End If

    End Function

    Public Function DecDay(ByVal Number As Integer) As String
        If istarikhmiladi Then
            Dim tarikh As String = Today.AddDays(Number).ToString("yyyyMMdd")
            Return tarikh
        Else
            Dim tarikh As Integer = objTarikh.Mi2Sh(Today.AddDays(Number))
            Return tarikh
        End If

    End Function
End Module
