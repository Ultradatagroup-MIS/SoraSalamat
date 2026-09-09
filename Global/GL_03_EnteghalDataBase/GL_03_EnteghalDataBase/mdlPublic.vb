Module mdlPublic
    '---Global----------------------------------------------------
    Public objTools As New UD_Dll.mdlUtility
    Public objTarikh As New UD_Dll.Tarikh
    Public objSec As New UD_Dll.Security
    Public ObjCode As New UD_Dll.Code

    ' Public objSearch As New mdlSearch

    Public ConnectionString As String = objTools.GetConnectionString
    Public UserName As String
    Public UserPassWord As String
    Public UserCode As Long  'CodeFard
    Public PersonelName As String
    Public PersonelCode As Long  'ShomarehPersonely
    Public TarikhEmrooz As String = objTarikh.Mi2Sh(Today)
    Public EmSal As Integer = objTarikh.ShamsiYear(TarikhEmrooz)
    Public CodeDoreh As Long = objTools.ConvertNulls(objTools.DLookup("CodeDoreh", "tblGL_DorehFaal", "UserName = '" & UserName & "'"), 0)
    Public CodeDorehAval As Long = objTools.ConvertNulls(objTools.DLookup(" Top 1 CodeDoreh", "tblGL_Doreh", ""), 0)
    Public CodeMahalFaal As Long = 0 'objTools.ConvertNulls(objTools.DLookup("CodeMahal", "tblGL_MahalFaal", "UserName = '" & UserName & "'"), 1)
    Public NameMahalFaal As String = "" 'objTools.ConvertNulls(objTools.DLookup("NameMahal", "tblGL_MarkazPakhsh", "CodeMahal = " & CodeMahalFaal), "")
    Public appPath As String = Application.StartupPath
    Public MojodiGhabelForoshKOL As Double = 0
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

    'Public NoeTafsily As HE_Tafsily
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

    Public tCodeMoshtary As String
    Public tccMoshtary As String
    Public tNameMoshtary As String

    '---Search Faktor---------------------------------------------
    Public tFaktorShomareh As String
    Public tFaktorCodeDoreh As String
    Public tFaktorccForoshandeh As String
    Public tFaktorccMoshtary As String
    Public Sub GetMojodyDarHalForosh(ByVal ccKala As Long, ByVal ccAnbar As Integer, _
    ByRef TedadPishFaktor As Double, ByRef MojodiFely As Double, ByRef MojodiGhabelForosh As Double)

        Dim strSqL As String = ""
        Dim cmSQL As SqlCommand
        Dim cnSQL As SqlConnection
        Dim p As New SqlParameter
        Dim TedadMarjoeeTaminKonandeh As Double = 0
        Dim TedadEshantion As Double = 0

        Dim TedadAnbar As Double

        Try

            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()

            strSqL = "Global.spTedadKalaRezervInPishFaktor "

            cmSQL = New SqlCommand(strSqL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            p = New SqlParameter("ccKala", SqlDbType.Int)
            p.Value = ccKala
            cmSQL.Parameters.Add(p)

            p = New SqlParameter("CodeMahal", SqlDbType.Int)
            p.Value = CType(CodeMahalFaal, Integer)
            cmSQL.Parameters.Add(p)

            TedadPishFaktor = cmSQL.ExecuteScalar


            cnSQL.Close() : cnSQL = Nothing

            ''----------------------------------

            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()

            strSqL = "Global.spTedadKalaRezervInEshantion "

            cmSQL = New SqlCommand(strSqL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            p = New SqlParameter("ccKala", SqlDbType.Int)
            p.Value = ccKala
            cmSQL.Parameters.Add(p)

            p = New SqlParameter("CodeMahal", SqlDbType.Int)
            p.Value = CType(CodeMahalFaal, Integer)
            cmSQL.Parameters.Add(p)

            p = New SqlParameter("ccAnbar", SqlDbType.Int)
            p.Value = ccAnbar
            cmSQL.Parameters.Add(p)

            TedadEshantion = cmSQL.ExecuteScalar

            cnSQL.Close() : cnSQL = Nothing

            ''----------------------------------

            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()

            strSqL = "Global.spTedadKalaRezervInMarjoeeTaminKonandeh "

            cmSQL = New SqlCommand(strSqL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            p = New SqlParameter("ccKala", SqlDbType.Int)
            p.Value = ccKala
            cmSQL.Parameters.Add(p)

            p = New SqlParameter("CodeMahal", SqlDbType.Int)
            p.Value = CType(CodeMahalFaal, Integer)
            cmSQL.Parameters.Add(p)

            p = New SqlParameter("ccAnbar", SqlDbType.Int)
            p.Value = ccAnbar
            cmSQL.Parameters.Add(p)

            TedadMarjoeeTaminKonandeh = cmSQL.ExecuteScalar

            cnSQL.Close() : cnSQL = Nothing

            '-----------------------------------------------------

            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()

            strSqL = "Global.spTedadKalaRezervInPishFaktorWithCodeDoreh "

            cmSQL = New SqlCommand(strSqL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            p = New SqlParameter("ccKala", SqlDbType.Int)
            p.Value = ccKala
            cmSQL.Parameters.Add(p)

            p = New SqlParameter("CodeMahal", SqlDbType.Int)
            p.Value = CType(CodeMahalFaal, Integer)
            cmSQL.Parameters.Add(p)

            p = New SqlParameter("CodeDoreh", SqlDbType.Int)
            p.Value = CType(CodeDoreh, Integer)
            cmSQL.Parameters.Add(p)

            TedadPishFaktor = cmSQL.ExecuteScalar

            TedadAnbar = ObjCode.MojodiAnbar(ccAnbar, Str(CodeDoreh).Trim + "0101", TarikhEmrooz, ccKala)
2:          MojodiFely = TedadAnbar.ToString
            MojodiGhabelForosh = (TedadAnbar - TedadPishFaktor)
            MojodiGhabelForoshKOL = (TedadAnbar - (TedadPishFaktor + TedadMarjoeeTaminKonandeh + TedadEshantion))
            cnSQL.Close() : cnSQL = Nothing

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
 

End Module
