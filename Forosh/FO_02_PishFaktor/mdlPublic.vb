Module mdlPublic
    '---Global----------------------------------------------------
    Public objTools As New UD_Dll.mdlUtility
    Public objTarikh As New UD_Dll.Tarikh
    Public objSec As New UD_Dll.Security
    Public ObjCode As New UD_Dll.Code

    ' Public objSearch As New mdlSearch
    Public ccKalaMoshabeh As Integer = 0
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
    Public BarcodeKala As Double

    '---Search Faktor---------------------------------------------
    Public tFaktorShomareh As String
    Public tFaktorCodeDoreh As String
    Public tFaktorccForoshandeh As String
    Public tFaktorccMoshtary As String
    Public Sub GetMojodyDarHalForosh_Bach(ByVal ccKala As Long, ByVal ccAnbar As Integer,ByRef TedadPishFaktor As Double, ByRef MojodiFely As Double, ByRef MojodiGhabelForosh As Double, ByVal ShomarehBach As String)


        Dim strSqL As String = ""
        Dim cmSQL As SqlCommand
        Dim cnSQL As SqlConnection

        Try

            cnSQL = New SqlConnection(ConnectionString)

            strSqL = " SELECT [dbo].[fnAN_GetMojodiDarHalForosh_bach] (" & ccAnbar & ", " & ccKala & ", " & CodeMahalFaal & ", " & CodeDoreh & ", " & TarikhEmrooz & ",replace('" & ShomarehBach & "',' ','')) "

            cmSQL = New SqlCommand(strSqL, cnSQL)
            cnSQL.Open()

            MojodiGhabelForosh = cmSQL.ExecuteScalar


            cnSQL.Close() : cnSQL = Nothing


        Catch ex As Exception


            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Public Sub GetMojodyDarHalForosh(ByVal ccKala As Long, ByVal ccAnbar As Integer,
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


            p = New SqlParameter("ccAnbar", SqlDbType.Int)
            p.Value = CType(ccAnbar, Integer)
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

            p = New SqlParameter("ccAnbar", SqlDbType.Int)
            p.Value = ccAnbar
            cmSQL.Parameters.Add(p)

            TedadPishFaktor = cmSQL.ExecuteScalar

            TedadAnbar = MojodiAnbar(ccAnbar, Str(CodeDoreh).Trim + "0101", TarikhEmrooz, ccKala)
            MojodiFely = TedadAnbar.ToString
            MojodiGhabelForosh = (TedadAnbar - TedadPishFaktor)
            MojodiGhabelForoshKOL = (TedadAnbar - (TedadPishFaktor + TedadMarjoeeTaminKonandeh + TedadEshantion))
            cnSQL.Close() : cnSQL = Nothing

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Public Function MojodiAnbar(ByVal ccAnbar As Integer, ByVal AzTarikh As String, ByVal TaTarikh As String, ByVal ccKala As Integer) As Double
        Dim cnSQL As SqlConnection
        Dim cmSQL As SqlCommand
        Dim strSQL As String

        cnSQL = New SqlConnection(ConnectionString)
        cnSQL.Open()
        Try
            strSQL = "Global.spUD_Dll_Code_MojodiAnbar "

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            cmSQL.Parameters.AddWithValue("ccAnbar", ccAnbar)
            cmSQL.Parameters.AddWithValue("AzTarikh", AzTarikh)
            cmSQL.Parameters.AddWithValue("TaTarikh", TaTarikh)
            cmSQL.Parameters.AddWithValue("ccKala", ccKala)

            MojodiAnbar = objTools.ConvertNulls(cmSQL.ExecuteScalar(), 0)

            cnSQL.Close() : cnSQL = Nothing
        Catch ex As Exception

        End Try
        Return MojodiAnbar
    End Function
    Public Sub GetBarcode(ByVal ccKala As Integer, ByVal ccMoshtary As Integer)

        BarcodeKala = 0

        Dim cnSQL As SqlConnection
        Dim cmSQL As SqlCommand
        Dim strSQL As String

        Dim sNoeMoshtary As Integer = objTools.ConvertNulls(objTools.DLookup("sNoeMoshtary", "tblFO_Moshtary", "ccMoshtary=" & ccMoshtary), 0)

        cnSQL = New SqlConnection(ConnectionString)
        cnSQL.Open()

        strSQL = "Global.spUD_Dll_GetBarcodeKala "

        cmSQL = New SqlCommand(strSQL, cnSQL)
        cmSQL.CommandType = CommandType.StoredProcedure
        cmSQL.Parameters.Clear()

        cmSQL.Parameters.AddWithValue("ccKala", ccKala)
        cmSQL.Parameters.AddWithValue("sNoeMoshtary", sNoeMoshtary)

        BarcodeKala = objTools.ConvertNulls(cmSQL.ExecuteScalar(), 0)

        cnSQL.Close()
        cmSQL = Nothing : cnSQL = Nothing
    End Sub


    Public Function GetTedadCheckPassNashodeh(ByVal ccMoshtary As Long, ByVal CodeDoreh As Integer) As Double
        Dim cnSQL As SqlConnection
        Dim cmSQL As SqlCommand
        Dim strSQL As String

        cnSQL = New SqlConnection(ConnectionString)
        cnSQL.Open()

        strSQL = "Global.spUD_Dll_GetTedadCheckPassNashodeh "


        cmSQL = New SqlCommand(strSQL, cnSQL)
        cmSQL.CommandType = CommandType.StoredProcedure
        cmSQL.Parameters.Clear()

        cmSQL.Parameters.AddWithValue("AzTarikh", "13800101")
        cmSQL.Parameters.AddWithValue("TaTarikh", TarikhEmrooz)
        cmSQL.Parameters.AddWithValue("ccMoshtary", ccMoshtary)

        GetTedadCheckPassNashodeh = objTools.ConvertNulls(cmSQL.ExecuteScalar(), 0)

        cnSQL.Close() : cnSQL = Nothing
        Return GetTedadCheckPassNashodeh
    End Function



    Function DLookupFunction(ByVal fldName As String, ByVal tblName As String, ByVal criteria As String) As Object
        Dim db As SqlConnection
        Dim StrSql As String
        Dim cmSQL As SqlCommand
        Dim ConStr As String

        DLookupFunction = Convert.DBNull
        ConStr = ConnectionString
        db = New SqlConnection(ConStr)
        db.Open()
        Try
            If Len(criteria) = 0 Then
                StrSql = "Select " & fldName & "" & tblName
            Else
                StrSql = "Select " & fldName & " From " & tblName & "  Where " & criteria
            End If
            cmSQL = New SqlCommand(StrSql, db)
            DLookupFunction = cmSQL.ExecuteScalar

        Catch ex As SqlException
            MsgBox(ex.Message, MsgBoxStyle.Information, "Message")
        Finally
            db.Close()
            cmSQL = Nothing
            db = Nothing
        End Try

    End Function
End Module




