Module mdlPublic

    '---Global----------------------------------------------------
    Public objTools As New ud_dll.mdlUtility
    Public objTarikh As New UD_Dll.Tarikh
    Public objSec As New ud_dll.security
    Public objCode As New UD_Dll.Code
    Public mdlEnum As New UD_Dll.Enums
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
    Public MojodiGhabelForoshKOL As Double = 0
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

    Public Sub GetMojodyDarHalForosh(ByVal ccKala As Long, ByVal ccAnbar As Integer, _
        ByRef TedadPishFaktor As Double, ByRef MojodiFely As Double, ByRef MojodiGhabelForosh As Double)


        Dim strSqL As String = ""
        Dim cmSQL As SqlCommand
        Dim cnSQL As SqlConnection
        Dim TedadEshantion As Double = 0
        Dim TedadMarjoeeTaminKonandeh As Double = 0
        Dim TedadAnbar As Double

        Try
            cnSQL = New SqlConnection(ConnectionString)

            strSqL = " Select isnull(Sum(Tedad3),0) As 'TedadKol' From dbo.qryFO_PishFaktorTitrSatr "
            strSqL &= " Where ccKala = " & ccKala & " And CodeMahal = " & CodeMahalFaal
            strSqL &= " AND ccPishFaktorTitr Not in (Select IsNull(ccPishFaktor,0) From tblFO_Faktor) AND svazeiat not in(4010,4338,4009)"
            cmSQL = New SqlCommand(strSqL, cnSQL)
            cnSQL.Open()

            TedadPishFaktor = cmSQL.ExecuteScalar


            cnSQL.Close() : cnSQL = Nothing

            ''----------------------------------
            cnSQL = New SqlConnection(ConnectionString)

            strSqL = " Select isnull(Sum(Tedad3),0) As 'TedadKol' From dbo.qryAN_EshantionTitrSatr "
            strSqL &= " Where ccKala = " & ccKala & " And CodeMahal = " & CodeMahalFaal
            strSqL &= " AND svazeiat <> 1 "

            cmSQL = New SqlCommand(strSqL, cnSQL)
            cnSQL.Open()

            TedadEshantion = cmSQL.ExecuteScalar


            cnSQL.Close() : cnSQL = Nothing


            ''----------------------------------
            cnSQL = New SqlConnection(ConnectionString)

            strSqL = " Select isnull(Sum(Tedad3),0) As 'TedadKol' From dbo.qryAN_kdxMarjoeeBeTaminKonandehTitrSatr "
            strSqL &= " Where ccKala = " & ccKala & " And CodeMahal = " & CodeMahalFaal
            strSqL &= " AND svazeiat <> 1 And ccAnbar = " & ccAnbar

            cmSQL = New SqlCommand(strSqL, cnSQL)
            cnSQL.Open()

            TedadMarjoeeTaminKonandeh = cmSQL.ExecuteScalar


            cnSQL.Close() : cnSQL = Nothing



            TedadAnbar = objCode.MojodiAnbar(ccAnbar, Str(CodeDoreh).Trim + "0101", TarikhEmrooz, ccKala)
            MojodiFely = TedadAnbar.ToString
            MojodiGhabelForoshKOL = (TedadAnbar - (TedadPishFaktor + TedadMarjoeeTaminKonandeh + TedadEshantion))
        Catch ex As Exception


            MessageBox.Show(ex.Message)
        End Try
    End Sub


End Module
