Module mdlPublic

    '---Global----------------------------------------------------
    Public objTools As New UD_Dll.mdlUtility
    Public objTarikh As New UD_Dll.Tarikh
    Public objSec As New ud_dll.security
    Public objCode As New UD_Dll.Code
    Public MdlEnum As New UD_Dll.Enums
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

    Public MojodiGhabelForoshKOL As Integer

    Public TarikhEmrooz As String = objTarikh.Mi2Sh(Today)
    Public EmSal As Integer = objTarikh.ShamsiYear(TarikhEmrooz)
    Public appPath As String = Application.StartupPath
    Public CodeSherkat As Long = 1
    Public rptPath As String = objTools.ConvertNulls(objTools.DLookup("ReportPath", "tblGL_Sherkat", "CodeSherkat = " & CodeSherkat), "")
    Public imgPath As String = objTools.ConvertNulls(objTools.DLookup("ImagePath", "tblGL_Sherkat", "CodeSherkat = " & CodeSherkat), "")
    Public NameSherkat As String = objTools.ConvertNulls(objTools.DLookup("NameSherkat", "tblGL_Sherkat", "CodeSherkat = " & CodeSherkat), "")

    '---Search Moshtary-------------------------------------------
    Public SearchItem As String
    Public MultiSelection As Boolean

    Public tCodeMoshtary As String
    Public tccMoshtary As String
    Public tNameMoshtary As String

    Public Sub GetMojodyDarHalForosh(ByVal ccKala As Long, ByVal ccAnbar As Integer,
      ByRef TedadPishFaktor As Double, ByRef MojodiFely As Double, ByRef MojodiGhabelForosh As Double, ByVal ShomarehBach As String)

        Dim strSqL As String = ""
        Dim cmSQL As SqlCommand
        Dim cnSQL As SqlConnection


        Try
            cnSQL = New SqlConnection(ConnectionString)


            strSqL = " SELECT [dbo].[fnAN_GetMojodiDarHalForosh_bach] (" & ccAnbar & ", " & ccKala & ", " & CodeMahalFaal & ", " & CodeDoreh & ", " & TarikhEmrooz & ",'" & ShomarehBach & "') "

            cmSQL = New SqlCommand(strSqL, cnSQL)
            cnSQL.Open()

            MojodiGhabelForoshKOL = cmSQL.ExecuteScalar


            cnSQL.Close() : cnSQL = Nothing


        Catch ex As Exception

            MessageBox.Show(ex.Message)
        End Try
    End Sub
End Module
