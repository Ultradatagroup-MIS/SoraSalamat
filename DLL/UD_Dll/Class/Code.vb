
Imports System.Runtime.InteropServices
Public Class Code

    '#Region " Global Variable "
    Public objTools As New mdlUtility
    Public objTarikh As New Tarikh
    Public objSec As New Security

    Public ConnectionString As String = objTools.GetConnectionString
    Public UserName As String
    Public UserPassWord As String
    Public UserCode As Long  'CodeFard
    Public PersonelName As String
    Public PersonelCode As Long  'ShomarehPersonely
    Public TarikhEmrooz As String = objTarikh.Mi2Sh(Today)
    Public CodeDoreh As Long = objTools.ConvertNulls(objTools.DLookup("CodeDoreh", "tblGL_DorehFaal", "UserName = '" & UserName & "'"), 1387)
    Public CodeMahalFaal As Long = objTools.ConvertNulls(objTools.DLookup("CodeMahal", "tblGL_MahalFaal", "UserName = '" & UserName & "'"), 1)
    Public NameMahalFaal As String = objTools.ConvertNulls(objTools.DLookup("NameMahal", "tblGL_MarkazPakhsh", "CodeMahal = " & CodeMahalFaal), "")
    'Public appPath As String = Application.StartupPath
    Public CodeSherkat As Long = 1
    Public CodeDorehAval As Long = objTools.ConvertNulls(objTools.DLookup(" Top 1 CodeDoreh", "tblGL_Doreh", ""), 0)
    <DllImport("user32.dll", SetLastError:=True, CharSet:=CharSet.Auto)> _
    Public Shared Function FindWindow(ByVal lpClassName As String, ByVal lpWindowName As String) As IntPtr
    End Function
#Region " SysConfig "
    Public Function CheckTarikhMasraf() As Byte
        Dim CheckField As Byte
        CheckField = objTools.ConvertNulls(objTools.DLookup("TarikhMasraf", "tblGL_SysConfig", "CodeMahal=" & CodeMahalFaal), 0)

        Return CheckField
    End Function
    Public Function CheckShomarehBach() As Byte
        Dim CheckField As Byte
        CheckField = objTools.ConvertNulls(objTools.DLookup("ShomarehBach", "tblGL_SysConfig", "CodeMahal=" & CodeMahalFaal), 0)

        Return CheckField
    End Function
    Public Function CheckAddKalaNotInFaktorToElamMarjoee() As Byte
        Dim CheckField As Byte
        CheckField = objTools.ConvertNulls(objTools.DLookup("AddKalaBeElamMarjoeeNotInFaktor", "tblGL_SysConfig", "CodeMahal=" & CodeMahalFaal), 0)

        Return CheckField
    End Function
    Public Function CheckEnableFee() As Byte
        Dim CheckField As Byte

        Select Case CheckCompany()
            Case 1
                CheckField = objTools.ConvertNulls(objTools.DLookup("EnableFee", "tblGL_SysConfig", "CodeMahal=" & CodeMahalFaal), 0)
            Case 2
                If UserName.ToUpper <> "ADMINISTRATOR" Then
                    Dim HaveUserPermission As Integer
                    Dim SN As Integer
                    SN = objSec.GetSecurityNumber(946, UserName)
                    HaveUserPermission = objTools.ConvertNulls(objTools.DLookup("SecurityNumber", "tblGL_Security", "CodeSubSystem = " & 946 & "  AND NameKarbar = '" & UserName.ToLower & "'"), 0)

                    If HaveUserPermission = 0 Then
                        CheckField = 2
                    Else
                        CheckField = 1
                    End If
                Else
                    CheckField = 1
                End If
        End Select

        Return CheckField
    End Function
    Public Function CheckMoshahedehGheymatInRepAnbar() As Byte
        Dim CheckField As Byte
        CheckField = objTools.ConvertNulls(objTools.DLookup("ShowGheymatInRepAnbar", "tblGL_SysConfig", "CodeMahal=" & CodeMahalFaal), 0)

        Return CheckField
    End Function
    Public Function MoshahedehGheymatInAnbar() As Byte
        Dim CheckField As Byte
        CheckField = objTools.ConvertNulls(objTools.DLookup("ShowGheymatInAnbar", "tblGL_SysConfig", "CodeMahal=" & CodeMahalFaal), 0)

        Return CheckField
    End Function
    Public Function CheckElamMarjoeeRoyeFaktor() As Byte
        Dim CheckField As Byte
        CheckField = objTools.ConvertNulls(objTools.DLookup("ElamMarjoeeRoyeFaktor", "tblGL_SysConfig", "CodeMahal=" & CodeMahalFaal), 0)

        Return CheckField
    End Function
    Public Function CheckMoshahedehBachInBargeTafkik() As Byte
        Dim CheckField As Byte
        CheckField = objTools.ConvertNulls(objTools.DLookup("ShowShBachInBargeTafkik", "tblGL_SysConfig", "CodeMahal=" & CodeMahalFaal), 0)

        Return CheckField
    End Function
    Public Function CheckSodorSanadVazeiat() As Integer
        Select Case objTools.ConvertNulls(objTools.DLookup("SodorSanadVazeiat", "tblGL_SysConfig", "CodeMahal=" & CodeMahalFaal), 0)
            Case 1
                Return 3942
            Case 2
                Return 3961
        End Select
    End Function
    Public Function CheckSodorSanadVazeiatShomarehSanad(ByVal CodeMahalFaal As Integer, ByVal CodeDoreh As Integer) As Integer
        Select Case objTools.ConvertNulls(objTools.DLookup("SodorSanadVazeiat", "tblGL_SysConfig", "CodeMahal=" & CodeMahalFaal), 0)
            Case 1
                Return 0
            Case 2
                Return objTools.ConvertNulls(objTools.DMax("ShomarehSanad", "tblHE_SanadHesabdaryTitr", "CodeMahal=" & CodeMahalFaal & " AND CodeDoreh=" & CodeDoreh), 0) + 1
        End Select
    End Function
    Public Function CheckEtebarMoshtary() As Integer
        Dim CheckField As Byte
        CheckField = objTools.ConvertNulls(objTools.DLookup("EtebarMoshtary", "tblGL_SysConfig", "CodeMahal=" & CodeMahalFaal), 0)

        Return CheckField
    End Function
    Public Function CheckPishFaktorTaeedModir() As Integer
        Dim CheckField As Byte
        CheckField = objTools.ConvertNulls(objTools.DLookup("PishFaktorTaeedModir", "tblGL_SysConfig", "CodeMahal=" & CodeMahalFaal), 0)

        Return CheckField
    End Function
    Public Function CheckFaktorTaeedModir() As Integer
        Dim CheckField As Byte
        CheckField = objTools.ConvertNulls(objTools.DLookup("FaktorTaeedModir", "tblGL_SysConfig", "CodeMahal=" & CodeMahalFaal), 0)

        Return CheckField
    End Function
    Public Function CheckCompany() As Integer
        Dim CheckField As Byte
        CheckField = objTools.ConvertNulls(objTools.DLookup("CompanyName", "tblGL_SysConfig", "CodeMahal=" & CodeMahalFaal), 0)

        Return CheckField
    End Function
    Public Function CheckTedadTafkik() As Integer
        Dim CheckField As Byte
        CheckField = objTools.ConvertNulls(objTools.DLookup("TedadTafkik", "tblGL_SysConfig", "CodeMahal=" & CodeMahalFaal), 0)

        Return CheckField
    End Function
    Public Function CheckBargehTafkik() As Integer
        Dim CheckField As Byte
        CheckField = objTools.ConvertNulls(objTools.DLookup("BargehTafkik", "tblGL_SysConfig", "CodeMahal=" & CodeMahalFaal), 0)

        Return CheckField
    End Function
    Public Function CheckNoeMohasebeTakhfif() As Integer
        Dim CheckField As Byte
        CheckField = objTools.ConvertNulls(objTools.DLookup("NoeMohasebeTakhfifFaktor", "tblGL_SysConfig", "CodeMahal=" & CodeMahalFaal), 0)

        Return CheckField
    End Function
    Public Function CheckNoeNegahdaryKala() As Integer
        Dim CheckField As Byte
        CheckField = objTools.ConvertNulls(objTools.DLookup("NoeNegahdaryKala", "tblGL_SysConfig", "CodeMahal=" & CodeMahalFaal), 0)

        Return CheckField
    End Function
    Public Function CheckMojodiAlert() As Integer
        Dim CheckField As Byte
        CheckField = objTools.ConvertNulls(objTools.DLookup("MojodiAlert", "tblGL_SysConfig", "CodeMahal=" & CodeMahalFaal), 0)

        Return CheckField
    End Function
#End Region
#Region " Global "
    Public Sub exportExcel(ByVal grdView As DataGridView, ByVal fileName As String, _
        ByVal fileExtension As String, ByVal filePath As String, ByVal File As String)

        ' Choose the path, name, and extension for the Excel file
        'Dim myFile As String = filePath & "\" & fileName & fileExtension
        Dim myFile As String = File

        ' Open the file and write the headers
        Dim fs As New IO.StreamWriter(File, False)
        fs.WriteLine("<?xml version=""1.0""?>")
        fs.WriteLine("<?mso-application progid=""Excel.Sheet""?>")
        fs.WriteLine("<ss:Workbook xmlns:ss=""urn:schemas-microsoft-com:office:spreadsheet"">")

        ' Create the styles for the worksheet
        fs.WriteLine("  <ss:Styles>")
        ' Style for the column headers
        fs.WriteLine("    <ss:Style ss:ID=""1"">")
        fs.WriteLine("      <ss:Font ss:Bold=""1""/>")
        fs.WriteLine("      <ss:Alignment ss:Horizontal=""Center"" ss:Vertical=""Center"" " & _
            "ss:WrapText=""1""/>")
        fs.WriteLine("      <ss:Interior ss:Color=""#C0C0C0"" ss:Pattern=""Solid""/>")
        fs.WriteLine("    </ss:Style>")
        ' Style for the column information
        fs.WriteLine("    <ss:Style ss:ID=""2"">")
        fs.WriteLine("      <ss:Alignment ss:Vertical=""Center"" ss:WrapText=""1""/>")
        fs.WriteLine("    </ss:Style>")
        fs.WriteLine("  </ss:Styles>")

        ' Write the worksheet contents
        fs.WriteLine("<ss:Worksheet ss:Name=""Sheet1"">")
        fs.WriteLine("  <ss:Table>")
        For i As Integer = 0 To grdView.Columns.Count - 1
            If grdView.Columns.Item(i).Visible Then
                fs.WriteLine(String.Format("    <ss:Column ss:Width=""{0}""/>", _
                grdView.Columns.Item(i).Width))
            End If
        Next
        fs.WriteLine("    <ss:Row>")
        For i As Integer = 0 To grdView.Columns.Count - 1
            If grdView.Columns.Item(i).Visible Then
                fs.WriteLine(String.Format("      <ss:Cell ss:StyleID=""1"">" & _
                    "<ss:Data ss:Type=""String"">{0}</ss:Data></ss:Cell>", _
                    grdView.Columns.Item(i).HeaderText))
            End If
        Next
        fs.WriteLine("    </ss:Row>")

        ' Check for an empty row at the end due to Adding allowed on the DataGridView
        Dim subtractBy As Integer, cellText As String
        If grdView.AllowUserToAddRows = True Then subtractBy = 2 Else subtractBy = 1
        ' Write contents for each cell
        For i As Integer = 0 To grdView.RowCount - subtractBy
            fs.WriteLine(String.Format("    <ss:Row ss:Height=""{0}"">", _
                    grdView.Rows(i).Height))
            For intCol As Integer = 0 To grdView.Columns.Count - 1
                If grdView.Columns.Item(intCol).Visible Then
                    cellText = grdView.Item(intCol, i).Value
                    ' Check for null cell and change it to empty to avoid error
                    If cellText = vbNullString Then cellText = ""
                    fs.WriteLine(String.Format("      <ss:Cell ss:StyleID=""2"">" & _
                            "<ss:Data ss:Type=""String"">{0}</ss:Data></ss:Cell>", _
                            cellText.ToString))
                End If

            Next
            fs.WriteLine("    </ss:Row>")
        Next

        ' Close up the document
        fs.WriteLine("  </ss:Table>")
        fs.WriteLine("</ss:Worksheet>")
        fs.WriteLine("</ss:Workbook>")
        fs.Close()

        ' Open the file in Microsoft Excel
        ' 10 = SW_SHOWDEFAULT

    End Sub
    Public Function GetNameSherkat() As String
        GetNameSherkat = "سیستم های جامع شرکت " & NameSherkat
        Return GetNameSherkat
    End Function
    Public Function GetMablaghAvarez(ByVal MablaghKhales As Double) As Double
        GetMablaghAvarez = (MablaghKhales * objTools.DLookupOne("DarsadAvarez", "tblFO_MalyatAvarez ", "", "  AzTarikh desc")) / 100
    End Function
    Public Function GetMablaghMalyat(ByVal MablaghKhales As Double) As Double
        GetMablaghMalyat = (MablaghKhales * objTools.DLookupOne("DarsadMalyat", "tblFO_MalyatAvarez ", "", "  AzTarikh desc")) / 100
    End Function
    Public Function ConvertHarfYeDeCode(ByVal str As String) As String
        ConvertHarfYeDeCode = str.Replace("ي", "ی")
    End Function
    Public Function ConvertHarfYeCode(ByVal str As String) As String
        ConvertHarfYeCode = str.Replace("ی", "ي")
    End Function
    Public Sub InsertVorodKhorojLog(ByVal Username As String, ByVal Password As String, ByVal NoeAmalyat As String)
        Dim cnSQL As SqlConnection
        Dim cmSQL As SqlCommand
        Dim strSQL As String

        cnSQL = New SqlConnection(ConnectionString)
        cnSQL.Open()

        strSQL = "Global.spUD_Dll_InsertVorodKhorojLog "

        cmSQL = New SqlCommand(strSQL, cnSQL)
        cmSQL.CommandType = CommandType.StoredProcedure
        cmSQL.Parameters.Clear()

        cmSQL.Parameters.AddWithValue("CodeMahal", CodeMahalFaal)
        cmSQL.Parameters.AddWithValue("UserName", Username)
        cmSQL.Parameters.AddWithValue("Password", Password)
        cmSQL.Parameters.AddWithValue("Tarikh", TarikhEmrooz)
        cmSQL.Parameters.AddWithValue("Saat", Format(TimeOfDay, "HH:mm:ss"))
        cmSQL.Parameters.AddWithValue("NoeAmalyat", NoeAmalyat)

        cmSQL.ExecuteNonQuery()

        cnSQL.Close()
        cmSQL = Nothing : cnSQL = Nothing
    End Sub
    Public Sub InsertKala_Log(ByVal ccKala As Integer, ByVal MablaghKharid As Double, ByVal MablaghForosh As Double, ByVal MablaghMasrafKonandeh As Double)
        Dim cnSQL As SqlConnection
        Dim cmSQL As SqlCommand
        Dim strSQL As String

        cnSQL = New SqlConnection(ConnectionString)
        cnSQL.Open()

        strSQL = "Global.spUD_Dll_InsertKala_Log "

        cmSQL = New SqlCommand(strSQL, cnSQL)
        cmSQL.CommandType = CommandType.StoredProcedure
        cmSQL.Parameters.Clear()

        cmSQL.Parameters.AddWithValue("ccKala", ccKala)
        cmSQL.Parameters.AddWithValue("MablaghForosh", MablaghForosh)
        cmSQL.Parameters.AddWithValue("MablaghKharid", MablaghKharid)
        cmSQL.Parameters.AddWithValue("MablaghMasrafKonandeh", MablaghMasrafKonandeh)
        cmSQL.Parameters.AddWithValue("UserName", UserName)
        cmSQL.Parameters.AddWithValue("Tarikh", TarikhEmrooz)
        cmSQL.Parameters.AddWithValue("Saat", Format(TimeOfDay, "HH:mm:ss"))

        cmSQL.ExecuteNonQuery()

        cnSQL.Close()
        cmSQL = Nothing : cnSQL = Nothing
    End Sub
    Public Function CheckPermission(ByVal CodeSubSystem As Integer) As Boolean
        Dim HaveUserPermission As Integer
        Dim SN As Integer
        SN = objSec.GetSecurityNumber(CodeSubSystem, UserName)
        HaveUserPermission = objTools.ConvertNulls(objTools.DLookup("SecurityNumber", "tblGL_Security", "CodeSubSystem = " & CodeSubSystem & "  AND NameKarbar = '" & UserName.ToLower & "'"), 0)
        If HaveUserPermission = 0 Then
            MsgBox(".شما دسترسی کار با این قسمت را ندارید", MsgBoxStyle.Critical, "دسترسی کاربران")
            Exit Function
        End If
        Return True
    End Function
    Public Function AddBeHarf(ByVal tAdd As Long) As String
        AddBeHarf = ""
        Dim str_SadTaNohsad(10) As String
        Dim str_SefrtaNavadonoh(100) As String
        str_SadTaNohsad(1) = " صد "
        str_SadTaNohsad(2) = " دويست "
        str_SadTaNohsad(3) = " سيصد "
        str_SadTaNohsad(4) = " چهارصد "
        str_SadTaNohsad(5) = " پانصد "
        str_SadTaNohsad(6) = " ششصد "
        str_SadTaNohsad(7) = " هفتصد "
        str_SadTaNohsad(8) = " هشتصد "
        str_SadTaNohsad(9) = " نهصد "

        str_SefrtaNavadonoh(1) = " يك "
        str_SefrtaNavadonoh(10) = " ده "
        str_SefrtaNavadonoh(11) = " يازده "
        str_SefrtaNavadonoh(12) = " دوازده "
        str_SefrtaNavadonoh(13) = " سيزده "
        str_SefrtaNavadonoh(14) = " چهارده "
        str_SefrtaNavadonoh(15) = " پانزده "
        str_SefrtaNavadonoh(16) = " شانزده "
        str_SefrtaNavadonoh(17) = " هفده "
        str_SefrtaNavadonoh(18) = " هجده "
        str_SefrtaNavadonoh(19) = " نوزده "
        str_SefrtaNavadonoh(2) = " دو "
        str_SefrtaNavadonoh(20) = " بيست "
        str_SefrtaNavadonoh(21) = " بيست ويك "
        str_SefrtaNavadonoh(22) = " بيست و دو "
        str_SefrtaNavadonoh(23) = " بيست و سه "
        str_SefrtaNavadonoh(24) = " بيست و چهار "
        str_SefrtaNavadonoh(25) = " بيست و پنج "
        str_SefrtaNavadonoh(26) = " بيست و شش "
        str_SefrtaNavadonoh(27) = " بيست و هفت "
        str_SefrtaNavadonoh(28) = " بيست و هشت "
        str_SefrtaNavadonoh(29) = " بيست و نه "
        str_SefrtaNavadonoh(3) = " سه "
        str_SefrtaNavadonoh(30) = " سي "
        str_SefrtaNavadonoh(31) = " سي و يك "
        str_SefrtaNavadonoh(32) = " سي و دو "
        str_SefrtaNavadonoh(33) = " سي و سه "
        str_SefrtaNavadonoh(34) = " سي و چهار "
        str_SefrtaNavadonoh(35) = " سي و پنج "
        str_SefrtaNavadonoh(36) = " سي و شش "
        str_SefrtaNavadonoh(37) = " سي و هفت "
        str_SefrtaNavadonoh(38) = " سي و هشت "
        str_SefrtaNavadonoh(39) = " سي و نه "
        str_SefrtaNavadonoh(4) = " چهار "
        str_SefrtaNavadonoh(40) = " چهل "
        str_SefrtaNavadonoh(41) = " چهل و يك "
        str_SefrtaNavadonoh(42) = " چهل و دو "
        str_SefrtaNavadonoh(43) = " چهل و سه "
        str_SefrtaNavadonoh(44) = " چهل و چهار "
        str_SefrtaNavadonoh(45) = " چهل و پنج "
        str_SefrtaNavadonoh(46) = " چهل و شش "
        str_SefrtaNavadonoh(47) = " چهل و هفت "
        str_SefrtaNavadonoh(48) = " چهل و هشت "
        str_SefrtaNavadonoh(49) = " چهل و نه "
        str_SefrtaNavadonoh(5) = " پنج "
        str_SefrtaNavadonoh(50) = " پنجاه "
        str_SefrtaNavadonoh(51) = " پنجاه و يك "
        str_SefrtaNavadonoh(52) = " پنجاه و دو "
        str_SefrtaNavadonoh(53) = " پنجاه و سه "
        str_SefrtaNavadonoh(54) = " پنجاه و چهار "
        str_SefrtaNavadonoh(55) = " پنجاه و پنج "
        str_SefrtaNavadonoh(56) = " پنجاه و شش "
        str_SefrtaNavadonoh(57) = " پنجاه و هفت "
        str_SefrtaNavadonoh(58) = " پنجاه و هشت "
        str_SefrtaNavadonoh(59) = " پنجاه و نه "
        str_SefrtaNavadonoh(6) = " شش "
        str_SefrtaNavadonoh(60) = " شصت "
        str_SefrtaNavadonoh(61) = " شصت و يك "
        str_SefrtaNavadonoh(62) = " شصت و دو "
        str_SefrtaNavadonoh(63) = " شصت و سه "
        str_SefrtaNavadonoh(64) = " شصت و چهار "
        str_SefrtaNavadonoh(65) = " شصت و پنج "
        str_SefrtaNavadonoh(66) = " شصت و شش "
        str_SefrtaNavadonoh(67) = " شصت و هفت "
        str_SefrtaNavadonoh(68) = " شصت و هشت "
        str_SefrtaNavadonoh(69) = " شصت و نه "
        str_SefrtaNavadonoh(7) = " هفت "
        str_SefrtaNavadonoh(70) = " هفتاد "
        str_SefrtaNavadonoh(71) = " هفتادو يك "
        str_SefrtaNavadonoh(72) = " هفتادو دو "
        str_SefrtaNavadonoh(73) = " هفتادو سه "
        str_SefrtaNavadonoh(74) = " هفتادو چهار "
        str_SefrtaNavadonoh(75) = " هفتادو پنج "
        str_SefrtaNavadonoh(76) = " هفتادو شش "
        str_SefrtaNavadonoh(77) = " هفتادو هفت "
        str_SefrtaNavadonoh(78) = " هفتادو هشت "
        str_SefrtaNavadonoh(79) = " هفتادو نه "
        str_SefrtaNavadonoh(8) = " هشت "
        str_SefrtaNavadonoh(80) = " هشتاد "
        str_SefrtaNavadonoh(81) = " هشتاد و يك "
        str_SefrtaNavadonoh(82) = " هشتاد و دو "
        str_SefrtaNavadonoh(83) = " هشتاد و سه "
        str_SefrtaNavadonoh(84) = " هشتاد و چهار "
        str_SefrtaNavadonoh(85) = " هشتاد و پنج "
        str_SefrtaNavadonoh(86) = " هشتاد و شش "
        str_SefrtaNavadonoh(87) = " هشتاد و هفت "
        str_SefrtaNavadonoh(88) = " هشتاد و هشت "
        str_SefrtaNavadonoh(89) = " هشتاد و نه "
        str_SefrtaNavadonoh(9) = " نه "
        str_SefrtaNavadonoh(90) = " نود "
        str_SefrtaNavadonoh(91) = " نود و يك "
        str_SefrtaNavadonoh(92) = " نود و دو "
        str_SefrtaNavadonoh(93) = " نود و سه "
        str_SefrtaNavadonoh(94) = " نود و چهار "
        str_SefrtaNavadonoh(95) = " نود و پنج "
        str_SefrtaNavadonoh(96) = " نود و شش "
        str_SefrtaNavadonoh(97) = " نود و هفت "
        str_SefrtaNavadonoh(98) = " نود و هشت "
        str_SefrtaNavadonoh(99) = " نود و نه "
        Dim rial As String
        Dim TempStr As String = ""
        Dim LeftRead As String
        Dim Horof As String
        Dim LenTempStr As Long
        Dim gLenTempStr As Long
        Dim ModLenTempStr As Long
        Dim chk As Boolean
        Dim kharej As String
        Dim ghabl As String

        rial = " ريال "
        chk = True
        If tAdd > 0 Then
            TempStr = tAdd.ToString
        Else
            chk = False
        End If

        TempStr = Replace(TempStr, ",", "")
        Horof = ""
        gLenTempStr = Len(TempStr)

        While chk = True

            LenTempStr = Len(TempStr)
            ModLenTempStr = LenTempStr Mod 3
            If ModLenTempStr = 0 Then ModLenTempStr = 3
            LeftRead = Left(TempStr, ModLenTempStr)

            kharej = LeftRead
            If Len(LeftRead) = 0 Then
                chk = False
            Else

                TempStr = Right(TempStr, Len(TempStr) - ModLenTempStr)

                Select Case Len(LeftRead)
                    Case 1, 2
                        Horof = Horof & str_SefrtaNavadonoh(Val(LeftRead))
                    Case 3
                        If Val(Left(LeftRead, 1)) > 0 Then
                            Horof = Horof & str_SadTaNohsad(Val(Left(LeftRead, 1)))
                        End If
                        ghabl = LeftRead
                        LeftRead = Right(LeftRead, 2)
                        If Val(LeftRead) <> 0 Then
                            If Val(Left(ghabl, 1)) <> 0 Then
                                Horof = Horof & " و "
                                Horof = Horof & str_SefrtaNavadonoh(Val(LeftRead))
                            Else
                                Horof = Horof & str_SefrtaNavadonoh(Val(LeftRead))
                            End If
                        End If
                End Select
                If Val(kharej) <> 0 Then

                    If Val(TempStr) = 0 Then

                        Select Case gLenTempStr 'tAdd.ToString.Length 'gLenTempStr
                            Case 4, 5, 6
                                Horof = Horof & " هزار "
                            Case 7, 8, 9
                                Horof = Horof & " میلیون  "
                            Case 10, 11, 12
                                Horof = Horof & " میلیارد "
                            Case 13, 14, 15
                                Horof = Horof & " تريليون "
                            Case 16, 17, 18
                                Horof = Horof & " كاتريليون  "
                        End Select

                    Else

                        Select Case gLenTempStr 'tAdd.ToString.Length
                            Case 4, 5, 6
                                Horof = Horof & " هزار و "
                            Case 7, 8, 9
                                Horof = Horof & " ميليون و "
                            Case 10, 11, 12
                                Horof = Horof & " ميليارد و "
                            Case 13, 14, 15
                                Horof = Horof & " تريليون و "
                            Case 16, 17, 18
                                Horof = Horof & " كاتريليون و "
                        End Select
                    End If

                End If

                gLenTempStr = gLenTempStr - 3
            End If
        End While
        AddBeHarf = Horof & " " & rial
    End Function
    Public Sub SabteTaghirat(ByVal CodeMahal As Integer, ByVal NoeTaghir As Integer, _
                             ByVal NameTable As String, ByVal Pk As String, ByVal Shomareh As String, ByVal Sharh As String)
        Dim cnSQL As SqlConnection : Dim cmSQL As SqlCommand
        Dim strSQL As String

        Try
            Windows.Forms.Cursor.Current = Cursors.WaitCursor
            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()

            strSQL = "Global.spUD_Dll_SabteTaghirat "

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            cmSQL.Parameters.AddWithValue("CodeMahal", CodeMahal)
            cmSQL.Parameters.AddWithValue("NoeTaghir", NoeTaghir)
            cmSQL.Parameters.AddWithValue("NameTable", NameTable)
            cmSQL.Parameters.AddWithValue("Pk", Pk)
            cmSQL.Parameters.AddWithValue("Shomareh", Shomareh)
            cmSQL.Parameters.AddWithValue("Sharh", Sharh)
            If UserName Is Nothing Then
                cmSQL.Parameters.AddWithValue("UserName", "")
            Else
                cmSQL.Parameters.AddWithValue("UserName", UserName)
            End If

            cmSQL.Parameters.AddWithValue("Tarikh", TarikhEmrooz)
            cmSQL.Parameters.AddWithValue("Saat", Format(TimeOfDay, "HH:mm:ss"))

            cmSQL.ExecuteNonQuery()

            cnSQL.Close()
            cmSQL = Nothing
            cnSQL = Nothing

        Catch sqlExc As SqlException
            If (sqlExc.Number = 2627) Or (sqlExc.Number = 229) Then
                If Microsoft.VisualBasic.Left(sqlExc.Message, 1) = "I" Then
                    MsgBox("خطا در اضافه کردن رکورد جديد ,ثبت انجام نشد", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "خطای بانک تغییرات")
                ElseIf Microsoft.VisualBasic.Left(sqlExc.Message, 1) = "V" Then
                    MsgBox("خطا در اضافه کردن رکورد جديد ,رکورد در بانک موجود است ,ثبت انجام نشد", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "خطای بانک تغییرات")
                End If
            Else
                MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "خطای بانک تغییرات")
            End If
        Catch e As Exception
            MsgBox(e.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "خطای بانک تغییرات")
        Finally
            Windows.Forms.Cursor.Current = Cursors.Default
        End Try
    End Sub
    Public Sub UpdateGheymatMiangin()
        Dim cnSQL As SqlConnection
        Dim cmSQL As SqlCommand
        Dim strSQL As String
        Dim daSQL As SqlDataAdapter
        Dim dsForm As New DataSet

        Dim TedadMojodi As Long = 0
        Dim RialMojodi As Double = 0
        Dim GheymatMiangin As Double = 0
        Dim RialVaredeh As Double = 0

        Dim TedadMojodiOnline As Long = 0

        Dim dt As DateTime
        Dim tRozGhabl As String = ""

        cnSQL = New SqlConnection(ConnectionString)
        cnSQL.Open()

        strSQL = "Select * from qryAN_MablaghMianginSet "
        strSQL &= "  order by TarikhForm,ccKardexTitr"

        If dsForm.Tables.Contains("tbl") Then
            dsForm.Tables.Remove("tbl")
        End If

        daSQL = New SqlDataAdapter(strSQL, ConnectionString)
        daSQL.Fill(dsForm, "tbl")

        For Each dr As DataRow In dsForm.Tables("tbl").Rows
            dt = objTarikh.SetDateSlash(objTarikh.Sh2Mi(dr("TarikhForm")))
            tRozGhabl = objTarikh.Mi2Sh(dt.AddDays(-1))

            'If dr("ccKardexSatr") = 1288 Then
            '    MsgBox("")
            'End If

            TedadMojodi = ObjCode.MojodiAnbar(dr("ccAnbar"), "13860101", tRozGhabl, dr("ccKala"))
            ' insert mojodi fely dar tblmojodi
            If objTools.ConvertNulls(objTools.DLookup("Tedad", "tblAN_Mojody", "ccKala=" & dr("ccKala") & " AND ccAnbar=" & dr("ccAnbar")), -4000) = -4000 Then
                strSQL = "Insert Into dbo.tblAN_Mojody (ccKala,ccAnbar,Tedad) Values ("
                strSQL &= dr("ccKala") & ","
                strSQL &= dr("ccAnbar") & ","
                strSQL &= TedadMojodi & ")"
                cmSQL = New SqlCommand(strSQL, cnSQL)
                cmSQL.ExecuteNonQuery()
            End If

            If dr("tbl") = 1 Then

                'update mojodi online
                strSQL = "Update dbo.tblAN_Mojody Set"
                strSQL &= " Tedad =Tedad-" & dr("Tedad3")
                strSQL &= " Where ccKala=" & dr("ccKala") & " AND " & " ccAnbar =" & dr("ccAnbar")
                cmSQL = New SqlCommand(strSQL, cnSQL)
                cmSQL.ExecuteNonQuery()

            ElseIf dr("tbl") = 2 Then
                GheymatMiangin = objTools.ConvertNulls(objTools.DLookup("GheymatMiangin", "tblAN_KalaGheymatMiangin", "ccKala=" & dr("ccKala") & " AND ccAnbar=" & dr("ccAnbar")), 0)
                TedadMojodiOnline = objTools.ConvertNulls(objTools.DLookup("Tedad", "tblAN_Mojody", "ccKala=" & dr("ccKala") & " AND ccAnbar=" & dr("ccAnbar")), 0)
                RialMojodi = TedadMojodiOnline * GheymatMiangin

                strSQL = "select top 1  MablaghKharid from qryAN_MablaghMianginGetMablaghKharid"
                strSQL &= " where ccKala=" & dr("ccKala") & " and TarikhForm<='" & dr("TarikhForm") & "'"
                strSQL &= " and ccKardexSatr =" & dr("ccKardexSatr")
                strSQL &= " order by TarikhForm Desc, ccKardexSatr desc"
                cmSQL = New SqlCommand(strSQL, cnSQL)
                RialVaredeh = objTools.ConvertNulls(cmSQL.ExecuteScalar(), 0)

                RialVaredeh = RialVaredeh * dr("Tedad3")
                GheymatMiangin = (RialMojodi + RialVaredeh) / (TedadMojodiOnline + dr("Tedad3"))

                ' insert or update mablagh miangin
                If objTools.ConvertNulls(objTools.DLookup("GheymatMiangin", "tblAN_KalaGheymatMiangin", "ccKala=" & dr("ccKala") & " AND ccAnbar=" & dr("ccAnbar")), -4000) = -4000 Then
                    strSQL = "Insert Into dbo.tblAN_KalaGheymatMiangin (ccKala,ccAnbar,gheymatMianGin) Values ("
                    strSQL &= dr("ccKala") & ","
                    strSQL &= dr("ccAnbar") & ","
                    strSQL &= CDbl(GheymatMiangin) & ")"
                    cmSQL = New SqlCommand(strSQL, cnSQL)
                    cmSQL.ExecuteNonQuery()
                Else
                    strSQL = "Update dbo.tblAN_KalaGheymatMiangin Set"
                    strSQL &= " gheymatMianGin =" & CDbl(GheymatMiangin).ToString.Replace(",", ".")
                    strSQL &= " Where ccKala=" & dr("ccKala") & " AND " & " ccAnbar =" & dr("ccAnbar")
                    cmSQL = New SqlCommand(strSQL, cnSQL)
                    cmSQL.ExecuteNonQuery()
                End If

                strSQL = "Update dbo.tblAN_KdxResidSatr Set"
                strSQL &= " MablaghKharid =" & CDbl(GheymatMiangin).ToString.Replace(",", ".")
                strSQL &= " Where ccKardexSatr=" & dr("ccKardexSatr") & " AND ccKardexTitr=" & dr("ccKardexTitr")
                cmSQL = New SqlCommand(strSQL, cnSQL)
                cmSQL.ExecuteNonQuery()

                'update mojodi online
                strSQL = "Update dbo.tblAN_Mojody Set"
                strSQL &= " Tedad =Tedad+" & dr("Tedad3")
                strSQL &= " Where ccKala=" & dr("ccKala") & " AND " & " ccAnbar =" & dr("ccAnbar")
                cmSQL = New SqlCommand(strSQL, cnSQL)
                cmSQL.ExecuteNonQuery()

            ElseIf dr("tbl") = 4 Then
                GheymatMiangin = objTools.ConvertNulls(objTools.DLookup("GheymatMiangin", "tblAN_KalaGheymatMiangin", "ccKala=" & dr("ccKala") & " AND ccAnbar=" & dr("ccAnbar")), 0)

                strSQL = "Update dbo.tblAN_kdxHavalehSatr Set"
                strSQL &= " Mablagh  =" & CDbl(GheymatMiangin).ToString.Replace(",", ".")
                strSQL &= " Where ccKardexSatr=" & dr("ccKardexSatr") & " AND ccKardexTitr=" & dr("ccKardexTitr")
                cmSQL = New SqlCommand(strSQL, cnSQL)
                cmSQL.ExecuteNonQuery()

                'update mojodi online
                strSQL = "Update dbo.tblAN_Mojody Set"
                strSQL &= " Tedad =Tedad-" & dr("Tedad3")
                strSQL &= " Where ccKala=" & dr("ccKala") & " AND " & " ccAnbar =" & dr("ccAnbar")
                cmSQL = New SqlCommand(strSQL, cnSQL)
                cmSQL.ExecuteNonQuery()

            ElseIf dr("tbl") = 3 Then

                GheymatMiangin = objTools.ConvertNulls(objTools.DLookup("GheymatMiangin", "tblAN_KalaGheymatMiangin", "ccKala=" & dr("ccKala") & " AND ccAnbar=" & dr("ccAnbar")), 0)
                TedadMojodiOnline = objTools.ConvertNulls(objTools.DLookup("Tedad", "tblAN_Mojody", "ccKala=" & dr("ccKala") & " AND ccAnbar=" & dr("ccAnbar")), 0)
                RialMojodi = TedadMojodiOnline * GheymatMiangin

                If dr("MablaghKharid") = 0 Then
                    strSQL = "update tblAN_MarjoeeAzMoshtarySatr set "

                    If GheymatMiangin = 0 Then
                        GheymatMiangin = objTools.ConvertNulls(objTools.DLookupOne("Mablagh", "tblAN_KdxHavalehSatr", "cckala =" & dr("cckala"), "cckardextitr asc"), 0)
                        strSQL &= " fee= " & GheymatMiangin
                    Else
                        strSQL &= " fee= " & GheymatMiangin
                    End If

                    strSQL &= " where ccMarjoeeAzMoshtarySatr = " & dr("ccKardexSatr")
                    cmSQL = New SqlCommand(strSQL, cnSQL)
                    cmSQL.ExecuteNonQuery()

                    RialVaredeh = GheymatMiangin * dr("Tedad3")
                    GheymatMiangin = (RialMojodi + RialVaredeh) / (TedadMojodiOnline + dr("Tedad3"))
                Else
                    RialVaredeh = dr("MablaghKharid") * dr("Tedad3")
                    GheymatMiangin = (RialMojodi + RialVaredeh) / (TedadMojodiOnline + dr("Tedad3"))
                End If

                If objTools.ConvertNulls(objTools.DLookup("GheymatMiangin", "tblAN_KalaGheymatMiangin", "ccKala=" & dr("ccKala") & " AND ccAnbar=" & dr("ccAnbar")), -4000) = -4000 Then
                    strSQL = "Insert Into dbo.tblAN_KalaGheymatMiangin (ccKala,ccAnbar,gheymatMianGin) Values ("
                    strSQL &= dr("ccKala") & ","
                    strSQL &= dr("ccAnbar") & ","
                    strSQL &= CDbl(GheymatMiangin) & ")"
                    cmSQL = New SqlCommand(strSQL, cnSQL)
                    cmSQL.ExecuteNonQuery()
                Else
                    strSQL = "Update dbo.tblAN_KalaGheymatMiangin Set"
                    strSQL &= " gheymatMianGin =" & CDbl(GheymatMiangin).ToString.Replace(",", ".")
                    strSQL &= " Where ccKala=" & dr("ccKala") & " AND " & " ccAnbar =" & dr("ccAnbar")
                    cmSQL = New SqlCommand(strSQL, cnSQL)
                    cmSQL.ExecuteNonQuery()
                End If

                'update mojodi online
                strSQL = "Update dbo.tblAN_Mojody Set"
                strSQL &= " Tedad =Tedad+" & dr("Tedad3")
                strSQL &= " Where ccKala=" & dr("ccKala") & " AND " & " ccAnbar =" & dr("ccAnbar")
                cmSQL = New SqlCommand(strSQL, cnSQL)
                cmSQL.ExecuteNonQuery()

            End If
        Next
    End Sub
    Function DecDay(ByVal pDate As String) As String
        Dim y As String = pDate.Substring(0, 4)
        Dim m As String = pDate.Substring(4, 2)
        Dim d As String = pDate.Substring(6, 2)
        Dim dt As New Date(y, m, d, New Globalization.PersianCalendar)
        dt = dt.AddDays(-1)
        Dim pc As New Globalization.PersianCalendar
        Return Format(pc.GetYear(dt), "0000") & Format(pc.GetMonth(dt), "00") & Format(pc.GetDayOfMonth(dt), "00")
    End Function
    Public Function GetFormText(ByVal CodeSubSystem As Integer) As String
        Return objTools.DLookup("NameSubSystem", "tblGL_NameSystemSub", "CodeSubSystem= " & CodeSubSystem)
    End Function
    Public Function DigitSeprator(ByVal Str As String) As String
        If Str.Length <= 3 Then
            Return Str
            Exit Function
        End If
        For i As Integer = 2 To Str.Length Step 4
            If Str.Length - i - 1 <> 0 Then
                Str = Str.Insert(Str.Length - i - 1, ".")
            End If
        Next

        Return Str
    End Function
    Public Function DigitSepratorRemover(ByVal Str As String) As String
        Return Str.ToString.Replace(".", "")
    End Function
#End Region
#Region " Forosh "
    Public Function GetMablaghForosh(ByVal ccKala As Integer, ByVal Tarikh As String, _
        ByVal ccMarkazPakhsh As Integer, ByVal ccMoshtary As Integer) As Double

        GetMablaghForosh = 0

        Dim cnSQL As SqlConnection
        Dim cmSQL As SqlCommand
        Dim strSQL As String

        Dim sNoeMoshtary As Integer = objTools.ConvertNulls(objTools.DLookup("sNoeMoshtary", "tblFO_Moshtary", "ccMoshtary=" & ccMoshtary), 0)

        cnSQL = New SqlConnection(ConnectionString)
        cnSQL.Open()

        strSQL = "Global.spUD_Dll_GetMablaghForosh "

        cmSQL = New SqlCommand(strSQL, cnSQL)
        cmSQL.CommandType = CommandType.StoredProcedure
        cmSQL.Parameters.Clear()

        cmSQL.Parameters.AddWithValue("ccKala", ccKala)
        cmSQL.Parameters.AddWithValue("Tarikh", Tarikh)
        cmSQL.Parameters.AddWithValue("ccMarkazPakhsh", ccMarkazPakhsh)
        cmSQL.Parameters.AddWithValue("ccMoshtary", ccMoshtary)
        cmSQL.Parameters.AddWithValue("sNoeMoshtary", sNoeMoshtary)

        GetMablaghForosh = objTools.ConvertNulls(cmSQL.ExecuteScalar(), 0)

        cnSQL.Close()
        cmSQL = Nothing : cnSQL = Nothing
    End Function
    Public Function GetMeghdarAdadi(ByVal code As Integer) As Integer
        GetMeghdarAdadi = objTools.ConvertNulls(objTools.DLookup("MeghdarAdadi", "tblGL_ShenasehOmomi", "Code=" & code), 0)
    End Function
    Public Sub RefreshJayezehPishFaktor(ByVal ccPishFaktorTitr As String, ByVal MoshtaryID As Long, ByVal TarikhPishFaktor As Long)
        Dim dt As DataTable
        Dim ds As New DataSet

        Dim strSQL As String = String.Empty
        Dim cnSQL As SqlConnection
        Dim cmSQL As SqlCommand
        Dim daSQL As SqlDataAdapter

        Try

            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()

            strSQL = "Global.spUD_Dll_RefreshJayezehPishFaktor_UpdateJamJayezeh "

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            cmSQL.Parameters.AddWithValue("TarikhPishFaktor", TarikhPishFaktor)
            cmSQL.Parameters.AddWithValue("MoshtaryID", MoshtaryID)
            cmSQL.Parameters.AddWithValue("ccPishFaktorTitr", ccPishFaktorTitr)
            cmSQL.Parameters.AddWithValue("NoeJayezeh", 1)

            cmSQL.ExecuteNonQuery()
            cmSQL = Nothing

            ''-------------*-------------*-------------*-------------''

            strSQL = "Global.spUD_Dll_RefreshJayezehPishFaktor_DeletePishFaktorSatr "

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            cmSQL.Parameters.AddWithValue("TarikhPishFaktor", TarikhPishFaktor)
            cmSQL.Parameters.AddWithValue("MoshtaryID", MoshtaryID)
            cmSQL.Parameters.AddWithValue("ccPishFaktorTitr", ccPishFaktorTitr)

            cmSQL.ExecuteNonQuery()
            cmSQL = Nothing

            ''-------------*-------------*-------------*-------------''

            strSQL = "Global.spUD_Dll_RefreshJayezehPishFaktor_SearchPishFaktorSatr_Kala "

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            cmSQL.Parameters.AddWithValue("TarikhPishFaktor", TarikhPishFaktor)
            cmSQL.Parameters.AddWithValue("MoshtaryID", MoshtaryID)
            cmSQL.Parameters.AddWithValue("ccPishFaktorTitr", ccPishFaktorTitr)

            If ds.Tables.Contains("tblSatrKala") Then
                ds.Tables.Remove("tblSatrKala")
            End If
            daSQL = New SqlDataAdapter(cmSQL)
            daSQL.Fill(ds, "tblSatrKala")
            cmSQL = Nothing

            ''-------------*-------------*-------------*-------------''

            strSQL = "Global.spUD_Dll_RefreshJayezehPishFaktor_SearchPishFaktorSatr_GorohKala "

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            cmSQL.Parameters.AddWithValue("TarikhPishFaktor", TarikhPishFaktor)
            cmSQL.Parameters.AddWithValue("MoshtaryID", MoshtaryID)
            cmSQL.Parameters.AddWithValue("ccPishFaktorTitr", ccPishFaktorTitr)

            If ds.Tables.Contains("tblSatrGorohKala") Then
                ds.Tables.Remove("tblSatrGorohKala")
            End If
            daSQL = New SqlDataAdapter(cmSQL)
            daSQL.Fill(ds, "tblSatrGorohKala")
            cmSQL = Nothing

            ''-------------*-------------*-------------*-------------''

            strSQL = "Global.spUD_Dll_RefreshJayezehPishFaktor_SearchPishFaktorSatr_Brand "

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            cmSQL.Parameters.AddWithValue("TarikhPishFaktor", TarikhPishFaktor)
            cmSQL.Parameters.AddWithValue("MoshtaryID", MoshtaryID)
            cmSQL.Parameters.AddWithValue("ccPishFaktorTitr", ccPishFaktorTitr)

            If ds.Tables.Contains("tblSatrBrand") Then
                ds.Tables.Remove("tblSatrBrand")
            End If
            daSQL = New SqlDataAdapter(cmSQL)
            daSQL.Fill(ds, "tblSatrBrand")
            cmSQL = Nothing

            ''-------------*-------------*-------------*-------------''

            For Each dr1 As DataRow In ds.Tables("tblSatrKala").Rows
                dt = GetJayezehKala(TarikhPishFaktor, dr1("Tedad"), dr1("ccKala"), ccMoshtary)
                For Each dr2 As DataRow In dt.Rows
                    strSQL = "Global.spUD_Dll_RefreshJayezehPishFaktor_InsertPishFaktorSatr "

                    cmSQL = New SqlCommand(strSQL, cnSQL)
                    cmSQL.CommandType = CommandType.StoredProcedure
                    cmSQL.Parameters.Clear()

                    cmSQL.Parameters.AddWithValue("ccPishFaktorTitr", ccPishFaktorTitr)
                    cmSQL.Parameters.AddWithValue("Radif", 0)
                    cmSQL.Parameters.AddWithValue("ccKala", dr2("ccKalaJayezeh"))
                    cmSQL.Parameters.AddWithValue("Tedad1", dr2("TedadJayezeh").ToString.Replace(",", "."))
                    cmSQL.Parameters.AddWithValue("Tedad2", dr2("TedadJayezeh").ToString.Replace(",", "."))
                    cmSQL.Parameters.AddWithValue("Tedad3", dr2("TedadJayezeh").ToString.Replace(",", "."))
                    cmSQL.Parameters.AddWithValue("sVahed", dr2("sVahed"))
                    cmSQL.Parameters.AddWithValue("Fee", dr2("Fee").ToString.Replace(",", "."))
                    cmSQL.Parameters.AddWithValue("TakhfifKala", 0)
                    cmSQL.Parameters.AddWithValue("DarsadTakhfif", 0)
                    cmSQL.Parameters.AddWithValue("IsJayezeh", 1)
                    cmSQL.Parameters.AddWithValue("Noe", 1)

                    cmSQL.ExecuteNonQuery()
                Next
                dt = Nothing
            Next

            ''-------------*-------------*-------------*-------------''

            For Each dr1 As DataRow In ds.Tables("tblSatrGorohKala").Rows
                dt = GetJayezehGorohKala(TarikhPishFaktor, dr1("Tedad"), dr1("sG1"), ccMoshtary)
                For Each dr2 As DataRow In dt.Rows
                    strSQL = "Global.spUD_Dll_RefreshJayezehPishFaktor_InsertPishFaktorSatr "

                    cmSQL = New SqlCommand(strSQL, cnSQL)
                    cmSQL.CommandType = CommandType.StoredProcedure
                    cmSQL.Parameters.Clear()

                    cmSQL.Parameters.AddWithValue("ccPishFaktorTitr", ccPishFaktorTitr)
                    cmSQL.Parameters.AddWithValue("Radif", objTools.DMax("Radif", "tblFO_PishFaktorSatr", " ccPishFaktorTitr = " & ccPishFaktorTitr) + 1)
                    cmSQL.Parameters.AddWithValue("ccKala", dr2("ccKalaJayezeh"))
                    cmSQL.Parameters.AddWithValue("Tedad1", dr2("TedadJayezeh").ToString.Replace(",", "."))
                    cmSQL.Parameters.AddWithValue("Tedad2", dr2("TedadJayezeh").ToString.Replace(",", "."))
                    cmSQL.Parameters.AddWithValue("Tedad3", dr2("TedadJayezeh").ToString.Replace(",", "."))
                    cmSQL.Parameters.AddWithValue("sVahed", dr2("sVahed"))
                    cmSQL.Parameters.AddWithValue("Fee", dr2("Fee").ToString.Replace(",", "."))
                    cmSQL.Parameters.AddWithValue("TakhfifKala", 0)
                    cmSQL.Parameters.AddWithValue("DarsadTakhfif", 0)
                    cmSQL.Parameters.AddWithValue("IsJayezeh", 1)
                    cmSQL.Parameters.AddWithValue("Noe", 2)

                    cmSQL.ExecuteNonQuery()
                    
                Next
                dt = Nothing
            Next

            ''-------------*-------------*-------------*-------------''

            For Each dr1 As DataRow In ds.Tables("tblSatrBrand").Rows
                dt = GetJayezehBrand(TarikhPishFaktor, dr1("Tedad"), dr1("ccBrand"), ccMoshtary)
                For Each dr2 As DataRow In dt.Rows
                    strSQL = "Global.spUD_Dll_RefreshJayezehPishFaktor_InsertPishFaktorSatr "

                    cmSQL = New SqlCommand(strSQL, cnSQL)
                    cmSQL.CommandType = CommandType.StoredProcedure
                    cmSQL.Parameters.Clear()

                    cmSQL.Parameters.AddWithValue("ccPishFaktorTitr", ccPishFaktorTitr)
                    cmSQL.Parameters.AddWithValue("Radif", objTools.DMax("Radif", "tblFO_PishFaktorSatr", " ccPishFaktorTitr = " & ccPishFaktorTitr) + 1)
                    cmSQL.Parameters.AddWithValue("ccKala", dr2("ccKalaJayezeh"))
                    cmSQL.Parameters.AddWithValue("Tedad1", dr2("TedadJayezeh").ToString.Replace(",", "."))
                    cmSQL.Parameters.AddWithValue("Tedad2", dr2("TedadJayezeh").ToString.Replace(",", "."))
                    cmSQL.Parameters.AddWithValue("Tedad3", dr2("TedadJayezeh").ToString.Replace(",", "."))
                    cmSQL.Parameters.AddWithValue("sVahed", dr2("sVahed"))
                    cmSQL.Parameters.AddWithValue("Fee", dr2("Fee").ToString.Replace(",", "."))
                    cmSQL.Parameters.AddWithValue("TakhfifKala", 0)
                    cmSQL.Parameters.AddWithValue("DarsadTakhfif", 0)
                    cmSQL.Parameters.AddWithValue("IsJayezeh", 1)
                    cmSQL.Parameters.AddWithValue("Noe", 3)

                    cmSQL.ExecuteNonQuery()

                Next
                dt = Nothing
            Next

            ''-------------*-------------*-------------*-------------''

            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()

            strSQL = "Global.spUD_Dll_RefreshJayezehPishFaktor_UpdateJamJayezeh "

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            cmSQL.Parameters.AddWithValue("TarikhPishFaktor", TarikhPishFaktor)
            cmSQL.Parameters.AddWithValue("MoshtaryID", MoshtaryID)
            cmSQL.Parameters.AddWithValue("ccPishFaktorTitr", ccPishFaktorTitr)
            cmSQL.Parameters.AddWithValue("NoeJayezeh", 0)

            cmSQL.ExecuteNonQuery()

            cnSQL.Close()
            cmSQL = Nothing : cnSQL = Nothing

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->RefreshJayezeh")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->RefreshJayezeh")
        Finally

        End Try
    End Sub
    Public Sub RefreshJayezehFaktor(ByVal ccTitr As Integer, ByVal ccMoshtary As Integer, ByVal TarikhPishFaktor As String)
        Dim dt As DataTable
        Dim ds As New DataSet

        Dim strSQL As String = String.Empty
        Dim cnSQL As SqlConnection
        Dim cmSQL As SqlCommand
        Dim daSQL As SqlDataAdapter

        Try

            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()

            strSQL = "Global.spUD_Dll_RefreshJayezehFaktor_UpdateJamJayezeh "

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            cmSQL.Parameters.AddWithValue("ccTitr", ccTitr)
            cmSQL.Parameters.AddWithValue("NoeJayezeh", 1)

            cmSQL.ExecuteNonQuery()
            cmSQL = Nothing

            ''-------------*-------------*-------------*-------------''

            strSQL = "Global.spUD_Dll_RefreshJayezehFaktor_DeleteFaktorSatr "

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            cmSQL.Parameters.AddWithValue("ccTitr", ccTitr)

            cmSQL.ExecuteNonQuery()
            cmSQL = Nothing

            ''-------------*-------------*-------------*-------------''

            strSQL = "Global.spUD_Dll_RefreshJayezehFaktor_SearchFaktorSatr_Kala "

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            cmSQL.Parameters.AddWithValue("ccTitr", ccTitr)

            If ds.Tables.Contains("tblSatrKala") Then
                ds.Tables.Remove("tblSatrKala")
            End If
            daSQL = New SqlDataAdapter(cmSQL)
            daSQL.Fill(ds, "tblSatrKala")
            cmSQL = Nothing

            ''-------------*-------------*-------------*-------------''

            strSQL = "Global.spUD_Dll_RefreshJayezehFaktor_SearchFaktorSatr_GorohKala "

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            cmSQL.Parameters.AddWithValue("ccTitr", ccTitr)

            If ds.Tables.Contains("tblSatrGorohKala") Then
                ds.Tables.Remove("tblSatrGorohKala")
            End If
            daSQL = New SqlDataAdapter(cmSQL)
            daSQL.Fill(ds, "tblSatrGorohKala")
            cmSQL = Nothing

            ''-------------*-------------*-------------*-------------''

            strSQL = "Global.spUD_Dll_RefreshJayezehFaktor_SearchFaktorSatr_Brand "

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            cmSQL.Parameters.AddWithValue("ccTitr", ccTitr)

            If ds.Tables.Contains("tblSatrBrand") Then
                ds.Tables.Remove("tblSatrBrand")
            End If
            daSQL = New SqlDataAdapter(cmSQL)
            daSQL.Fill(ds, "tblSatrBrand")
            cmSQL = Nothing

            ''-------------*-------------*-------------*-------------''

            For Each dr1 As DataRow In ds.Tables("tblSatrKala").Rows
                dt = GetJayezehKala(TarikhPishFaktor, dr1("Tedad"), dr1("ccKala"), ccMoshtary)
                For Each dr2 As DataRow In dt.Rows
                    strSQL = "Global.spUD_Dll_RefreshJayezehFaktor_InsertFaktorSatr "

                    cmSQL = New SqlCommand(strSQL, cnSQL)
                    cmSQL.CommandType = CommandType.StoredProcedure
                    cmSQL.Parameters.Clear()

                    cmSQL.Parameters.AddWithValue("ccFaktorTitr", ccTitr)
                    cmSQL.Parameters.AddWithValue("Radif", objTools.DMax("Radif", "tblFO_FaktorSatr", " ccFaktorTitr=" & ccTitr) + 1)
                    cmSQL.Parameters.AddWithValue("ccKala", dr2("ccKalaJayezeh"))
                    cmSQL.Parameters.AddWithValue("Tedad1", dr2("TedadJayezeh").ToString.Replace(",", "."))
                    cmSQL.Parameters.AddWithValue("Tedad2", dr2("TedadJayezeh").ToString.Replace(",", "."))
                    cmSQL.Parameters.AddWithValue("Tedad3", dr2("TedadJayezeh").ToString.Replace(",", "."))
                    cmSQL.Parameters.AddWithValue("sVahed", dr2("sVahed"))
                    cmSQL.Parameters.AddWithValue("Fee", dr2("Fee").ToString.Replace(",", "."))
                    cmSQL.Parameters.AddWithValue("TakhfifKala", 0)
                    cmSQL.Parameters.AddWithValue("DarsadTakhfif", 0)
                    cmSQL.Parameters.AddWithValue("IsJayezeh", 1)

                    cmSQL.ExecuteNonQuery()
                Next
                dt = Nothing
            Next

            For Each dr1 As DataRow In ds.Tables("tblSatrGorohKala").Rows
                dt = GetJayezehGorohKala(TarikhPishFaktor, dr1("Tedad"), dr1("sG1"), ccMoshtary)
                For Each dr2 As DataRow In dt.Rows
                    strSQL = "Global.spUD_Dll_RefreshJayezehFaktor_InsertFaktorSatr "

                    cmSQL = New SqlCommand(strSQL, cnSQL)
                    cmSQL.CommandType = CommandType.StoredProcedure
                    cmSQL.Parameters.Clear()

                    cmSQL.Parameters.AddWithValue("ccFaktorTitr", ccTitr)
                    cmSQL.Parameters.AddWithValue("Radif", objTools.DMax("Radif", "tblFO_FaktorSatr", " ccFaktorTitr=" & ccTitr) + 1)
                    cmSQL.Parameters.AddWithValue("ccKala", dr2("ccKalaJayezeh"))
                    cmSQL.Parameters.AddWithValue("Tedad1", dr2("TedadJayezeh").ToString.Replace(",", "."))
                    cmSQL.Parameters.AddWithValue("Tedad2", dr2("TedadJayezeh").ToString.Replace(",", "."))
                    cmSQL.Parameters.AddWithValue("Tedad3", dr2("TedadJayezeh").ToString.Replace(",", "."))
                    cmSQL.Parameters.AddWithValue("sVahed", dr2("sVahed"))
                    cmSQL.Parameters.AddWithValue("Fee", dr2("Fee").ToString.Replace(",", "."))
                    cmSQL.Parameters.AddWithValue("TakhfifKala", 0)
                    cmSQL.Parameters.AddWithValue("DarsadTakhfif", 0)
                    cmSQL.Parameters.AddWithValue("IsJayezeh", 1)

                    cmSQL.ExecuteNonQuery()
                Next
                dt = Nothing
            Next

            For Each dr1 As DataRow In ds.Tables("tblSatrBrand").Rows
                dt = GetJayezehBrand(TarikhPishFaktor, dr1("Tedad"), dr1("ccBrand"), ccMoshtary)
                For Each dr2 As DataRow In dt.Rows
                    strSQL = "Global.spUD_Dll_RefreshJayezehFaktor_InsertFaktorSatr "

                    cmSQL = New SqlCommand(strSQL, cnSQL)
                    cmSQL.CommandType = CommandType.StoredProcedure
                    cmSQL.Parameters.Clear()

                    cmSQL.Parameters.AddWithValue("ccFaktorTitr", ccTitr)
                    cmSQL.Parameters.AddWithValue("Radif", objTools.DMax("Radif", "tblFO_FaktorSatr", " ccFaktorTitr=" & ccTitr) + 1)
                    cmSQL.Parameters.AddWithValue("ccKala", dr2("ccKalaJayezeh"))
                    cmSQL.Parameters.AddWithValue("Tedad1", dr2("TedadJayezeh").ToString.Replace(",", "."))
                    cmSQL.Parameters.AddWithValue("Tedad2", dr2("TedadJayezeh").ToString.Replace(",", "."))
                    cmSQL.Parameters.AddWithValue("Tedad3", dr2("TedadJayezeh").ToString.Replace(",", "."))
                    cmSQL.Parameters.AddWithValue("sVahed", dr2("sVahed"))
                    cmSQL.Parameters.AddWithValue("Fee", dr2("Fee").ToString.Replace(",", "."))
                    cmSQL.Parameters.AddWithValue("TakhfifKala", 0)
                    cmSQL.Parameters.AddWithValue("DarsadTakhfif", 0)
                    cmSQL.Parameters.AddWithValue("IsJayezeh", 1)

                    cmSQL.ExecuteNonQuery()
                Next
                dt = Nothing
            Next

            ''-------------*-------------*-------------*-------------''

            strSQL = "Global.spUD_Dll_RefreshJayezehFaktor_UpdateJamJayezeh "

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            cmSQL.Parameters.AddWithValue("ccTitr", ccTitr)
            cmSQL.Parameters.AddWithValue("NoeJayezeh", 0)

            cmSQL.ExecuteNonQuery()
            cmSQL = Nothing

            cnSQL.Close()
            cmSQL = Nothing : cnSQL = Nothing

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->RefreshJayezeh")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->RefreshJayezeh")
        Finally

        End Try
    End Sub
    Public Sub RefreshTakhfifFaktor(ByVal dr As DataRow)
        Dim ds As New DataSet

        Dim strSQL As String = String.Empty
        Dim cnSQL As SqlConnection
        Dim cmSQL As SqlCommand
        Try

            Dim sNoeMoshtary As Integer = objTools.ConvertNulls(objTools.DLookup("sNoeMoshtary", "tblFO_Moshtary", "ccMoshtary=" & dr("ccMoshtary")), 0)
            Dim sNoeSenf As Integer = objTools.ConvertNulls(objTools.DLookup("sNoeSenf", "tblFO_Moshtary", "ccMoshtary=" & dr("ccMoshtary")), 0)
            Dim CodeMahal As Integer = objTools.ConvertNulls(objTools.DLookup("CodeMahal", "tblFO_Moshtary", "ccMoshtary=" & dr("ccMoshtary")), 0)

            Dim Takhfif As Double = 0
            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()

            strSQL = "Global.spUD_Dll_RefreshTakhfifFaktor_InsertFaktorSatr "

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            cmSQL.Parameters.AddWithValue("CheckNoeMohasebeTakhfif", CheckNoeMohasebeTakhfif())
            cmSQL.Parameters.AddWithValue("MablaghMarjoee", dr("MablaghMarjoee"))
            cmSQL.Parameters.AddWithValue("FaktorTarikh", dr("FaktorTarikh"))
            cmSQL.Parameters.AddWithValue("ccMoshtary", dr("ccMoshtary"))
            cmSQL.Parameters.AddWithValue("sNoePardakht", dr("sNoePardakht"))
            cmSQL.Parameters.AddWithValue("JamFaktor", dr("JamFaktor").ToString.Replace(",", "."))
            cmSQL.Parameters.AddWithValue("sNoeMoshtary", sNoeMoshtary)
            cmSQL.Parameters.AddWithValue("sNoeSenf", sNoeSenf)
            cmSQL.Parameters.AddWithValue("CodeMahal", CodeMahal)
            cmSQL.Parameters.AddWithValue("ccFaktorTitr", dr("ccFaktorTitr"))

            Takhfif = objTools.ConvertNulls(cmSQL.ExecuteScalar(), 0)
            cmSQL = Nothing

            strSQL = "Global.spUD_Dll_RefreshTakhfifFaktor_UpdateTakhfif "

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            cmSQL.Parameters.AddWithValue("Takhfif", Takhfif.ToString.Replace(",", "."))
            cmSQL.Parameters.AddWithValue("ccFaktorTitr", dr("ccFaktorTitr"))

            cmSQL.ExecuteNonQuery()

            cnSQL.Close()
            cmSQL = Nothing : cnSQL = Nothing

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->RefreshJayezeh")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->RefreshJayezeh")
        Finally

        End Try
    End Sub
    'Public Sub RefreshTakhfifPishFaktor(ByVal dr As DataRow)
    '    Dim ds As New DataSet

    '    Dim strSQL As String = String.Empty
    '    Dim cnSQL As SqlConnection
    '    Dim cmSQL As SqlCommand
    '    Try

    '        Dim sNoeMoshtary As Integer = objTools.ConvertNulls(objTools.DLookup("sNoeMoshtary", "tblFO_Moshtary", "ccMoshtary=" & dr("ccMoshtary")), 0)
    '        Dim sNoeSenf As Integer = objTools.ConvertNulls(objTools.DLookup("sNoeSenf", "tblFO_Moshtary", "ccMoshtary=" & dr("ccMoshtary")), 0)
    '        Dim CodeMahal As Integer = objTools.ConvertNulls(objTools.DLookup("CodeMahal", "tblFO_Moshtary", "ccMoshtary=" & dr("ccMoshtary")), 0)


    '        Dim Takhfif As Double = 0
    '        cnSQL = New SqlConnection(ConnectionString)
    '        cnSQL.Open()
    '        strSQL = " Select Sum(MKOL3) * (Select dbo.fnFO_GetTakhfif"
    '        strSQL &= " ('" & dr("PishFaktorTarikh") & "'," & dr("ccMoshtary") & "," & dr("sNoePardakht")
    '        strSQL &= " ," & dr("JamPishFaktor").ToString.Replace(",", ".") & "," & sNoeMoshtary & "," & sNoeSenf & "," & CodeMahal & ")) / 100 "
    '        strSQL &= " from tblFO_PishFaktorSatr Where ccPishFaktorTitr =" & dr("ccPishFaktorTitr")
    '        strSQL &= " and IsJayezeh=0"
    '        strSQL &= " And ccKala Not in (select cckala from dbo.tblFO_KalaBedoneTakhfif"
    '        strSQL &= " where Faal=1 and '" & dr("PishFaktorTarikh") & "' >= azTarikh)"
    '        cmSQL = New SqlCommand(strSQL, cnSQL)
    '        Takhfif = objTools.ConvertNulls(cmSQL.ExecuteScalar(), 0)

    '        strSQL = " UPDATE tblFO_PishFaktor SET "
    '        strSQL &= " Takhfif = " & Takhfif.ToString.Replace(",", ".")
    '        strSQL &= " WHERE ccPishFaktorTitr = " & dr("ccPishFaktorTitr")

    '        cmSQL = New SqlCommand(strSQL, cnSQL)
    '        cmSQL.ExecuteNonQuery()

    '        cnSQL.Close()
    '        cmSQL = Nothing : cnSQL = Nothing

    '    Catch sqlExc As SqlException
    '        MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->RefreshJayezeh")
    '    Catch ex As Exception
    '        MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->RefreshJayezeh")
    '    Finally

    '    End Try
    'End Sub
    Public Function FindDarsadTakhfif(ByVal NoePardakht As Long, ByVal Mablagh As Long, _
                                       ByVal Tarikh As String, ByVal ccMoshtary As Integer) As Double

        FindDarsadTakhfif = 0

        Dim cnSQL As SqlConnection
        Dim cmSQL As SqlCommand
        Dim strSQL As String
        Dim sNoeMoshtary As Integer = objTools.ConvertNulls(objTools.DLookup("sNoeMoshtary", "tblFO_Moshtary", "ccMoshtary=" & ccMoshtary), 0)
        Dim sNoeSenf As Integer = objTools.ConvertNulls(objTools.DLookup("sNoeSenf", "tblFO_Moshtary", "ccMoshtary=" & ccMoshtary), 0)
        Dim CodeMahal As Integer = objTools.ConvertNulls(objTools.DLookup("CodeMahal", "tblFO_Moshtary", "ccMoshtary=" & ccMoshtary), 0)

        cnSQL = New SqlConnection(ConnectionString)
        cnSQL.Open()

        strSQL = "Global.spUD_Dll_FindDarsadTakhfif "

        cmSQL = New SqlCommand(strSQL, cnSQL)
        cmSQL.CommandType = CommandType.StoredProcedure
        cmSQL.Parameters.Clear()

        cmSQL.Parameters.AddWithValue("Tarikh", Tarikh)
        cmSQL.Parameters.AddWithValue("ccMoshtary", ccMoshtary)
        cmSQL.Parameters.AddWithValue("NoePardakht", NoePardakht)
        cmSQL.Parameters.AddWithValue("Mablagh", Mablagh)
        cmSQL.Parameters.AddWithValue("sNoeMoshtary", sNoeMoshtary)
        cmSQL.Parameters.AddWithValue("sNoeSenf", sNoeSenf)
        cmSQL.Parameters.AddWithValue("CodeMahal", CodeMahal)

        'strSQL = "Select Top 1 DarsadTakhfif From tblFO_Takhfif Where sNoePardakht = " & NoePardakht & " AND azTarikh <= '" & Tarikh & "' And TaMablagh <= " & Mablagh & " Order by azTarikh DESC,azSaat DESC,TaMablagh DESC"

        FindDarsadTakhfif = objTools.ConvertNulls(cmSQL.ExecuteScalar(), 0)

        cnSQL.Close()
        cmSQL = Nothing : cnSQL = Nothing

        Return FindDarsadTakhfif
    End Function
    Public Function FindDarsadTakhfifRow(ByVal Tarikh As String, ByVal NoePardakht As Long, _
                                        ByVal Mablagh As Long, ByVal ccMoshtary As Long, _
                                        ByVal ccKala As Integer) As Double

        Try

            FindDarsadTakhfifRow = 0

            Dim cnSQL As SqlConnection
            Dim cmSQL As SqlCommand
            Dim strSQL As String
            Dim Darsad As Double

            Dim sNoeMoshtary As Integer = objTools.ConvertNulls(objTools.DLookup("sNoeMoshtary", "tblFO_Moshtary", "ccMoshtary=" & ccMoshtary), 0)
            Dim sNoeSenf As Integer = objTools.ConvertNulls(objTools.DLookup("sNoeSenf", "tblFO_Moshtary", "ccMoshtary=" & ccMoshtary), 0)
            Dim ccBrand As Integer = objTools.ConvertNulls(objTools.DLookup("ccBrand", "tblAN_Kala", "ccKala=" & ccKala), 0)
            Dim codeGorohKala As Integer = objTools.ConvertNulls(objTools.DLookup("sG1", "tblAN_Kala", "ccKala=" & ccKala), 0)
            Dim codeGorohKala2 As Integer = objTools.ConvertNulls(objTools.DLookup("sG2", "tblAN_Kala", "ccKala=" & ccKala), 0)
            Dim codeGorohKala3 As Integer = objTools.ConvertNulls(objTools.DLookup("sG3", "tblAN_Kala", "ccKala=" & ccKala), 0)
            Dim CodeMahal As Integer = objTools.ConvertNulls(objTools.DLookup("CodeMahal", "tblFO_Moshtary", "ccMoshtary=" & ccMoshtary), 0)

            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()

            strSQL = "Global.spUD_Dll_FindDarsadTakhfifRow "

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            cmSQL.Parameters.AddWithValue("Tarikh", Tarikh)
            cmSQL.Parameters.AddWithValue("NoePardakht", NoePardakht)
            cmSQL.Parameters.AddWithValue("Mablagh", Mablagh)
            cmSQL.Parameters.AddWithValue("ccMoshtary", ccMoshtary)
            cmSQL.Parameters.AddWithValue("sNoeSenf", sNoeSenf)
            cmSQL.Parameters.AddWithValue("sNoeMoshtary", sNoeMoshtary)
            cmSQL.Parameters.AddWithValue("ccKala", ccKala)
            cmSQL.Parameters.AddWithValue("codeGorohKala", codeGorohKala)
            cmSQL.Parameters.AddWithValue("codeGorohKala2", codeGorohKala2)
            cmSQL.Parameters.AddWithValue("codeGorohKala3", codeGorohKala3)
            cmSQL.Parameters.AddWithValue("ccBrand", ccBrand)

            Darsad = objTools.ConvertNulls(cmSQL.ExecuteScalar(), 0)

            FindDarsadTakhfifRow = Darsad
            Return Darsad

            cnSQL.Close()
            cnSQL = Nothing

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->FindDarsadTakhfifRow")
        End Try
    End Function
    Public Sub RefreshTakhfifPishFaktor(ByVal dr As DataRow)
        Dim ds As New DataSet
        Dim strSqlTitr
        Dim strSQL As String = String.Empty
        Dim cnSQL As SqlConnection
        Dim cmSQL As SqlCommand
        Try

            Dim sNoeMoshtary As Integer = objTools.ConvertNulls(objTools.DLookup("sNoeMoshtary", "tblFO_Moshtary", "ccMoshtary=" & dr("ccMoshtary")), 0)
            Dim sNoeSenf As Integer = objTools.ConvertNulls(objTools.DLookup("sNoeSenf", "tblFO_Moshtary", "ccMoshtary=" & dr("ccMoshtary")), 0)
            Dim CodeMahal As Integer = objTools.ConvertNulls(objTools.DLookup("CodeMahal", "tblFO_Moshtary", "ccMoshtary=" & dr("ccMoshtary")), 0)
            Dim sNoePardakht As Integer = objTools.ConvertNulls(objTools.DLookup("sNoePardakht", "tblFO_PishFaktor", "ccPishFaktorTitr = " & dr("ccPishFaktorTitr")), 0)

            ' Faktor -------------------------------------
            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()

            strSQL = "Global.spUD_Dll_RefreshTakhfifPishFaktor_SearchTakhfif "

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            cmSQL.Parameters.AddWithValue("CodeMahal", CodeMahal)
            cmSQL.Parameters.AddWithValue("sNoeSenf", sNoeSenf)
            cmSQL.Parameters.AddWithValue("sNoeMoshtary", sNoeMoshtary)
            cmSQL.Parameters.AddWithValue("sNoePardakht", sNoePardakht)
            cmSQL.Parameters.AddWithValue("Noe", 0)

            Dim daSQL As SqlDataAdapter
            If ds.Tables.Contains("tblTakhfifFaktor") Then
                ds.Tables.Remove("tblTakhfifFaktor")
            End If
            daSQL = New SqlDataAdapter(cmSQL)
            daSQL.Fill(ds, "tblTakhfifFaktor")
            cmSQL = Nothing

            For Each drr As DataRow In ds.Tables("tblTakhfifFaktor").Rows
                Dim SumG1 As Integer = 0
                Dim TakhfifFaktor As Integer = 0

                strSQL = "Global.spUD_Dll_RefreshTakhfifPishFaktor_CalcSumMkol3 "

                cmSQL = New SqlCommand(strSQL, cnSQL)
                cmSQL.CommandType = CommandType.StoredProcedure
                cmSQL.Parameters.Clear()

                cmSQL.Parameters.AddWithValue("CodeMahal", CodeMahal)
                cmSQL.Parameters.AddWithValue("PishFaktorTarikh", dr("PishFaktorTarikh"))
                cmSQL.Parameters.AddWithValue("ccPishFaktorTitr", dr("ccPishFaktorTitr"))
                cmSQL.Parameters.AddWithValue("sNoePardakht", sNoePardakht)
                cmSQL.Parameters.AddWithValue("sG1", 0)
                cmSQL.Parameters.AddWithValue("sG2", 0)
                cmSQL.Parameters.AddWithValue("sG3", 0)
                cmSQL.Parameters.AddWithValue("Noe", 0)

                SumG1 = objTools.ConvertNulls(cmSQL.ExecuteScalar(), 0)

                If SumG1 >= drr("AzMablagh") And SumG1 <= drr("TaMablagh") Then

                    TakhfifFaktor = drr("DarsadTakhfif") / 100 * SumG1

                    cnSQL = New SqlConnection(ConnectionString)
                    cnSQL.Open()

                    strSqlTitr = "Global.spUD_Dll_RefreshTakhfifPishFaktor_UpdateTakhfif "

                    cmSQL = New SqlCommand(strSqlTitr, cnSQL)
                    cmSQL.CommandType = CommandType.StoredProcedure
                    cmSQL.Parameters.Clear()

                    cmSQL.Parameters.AddWithValue("Takhfif", TakhfifFaktor)
                    cmSQL.Parameters.AddWithValue("ccPishFaktorTitr", dr("ccPishFaktorTitr"))

                    cmSQL.ExecuteNonQuery()
                    cmSQL = Nothing
                    cnSQL.Close()

                Else

                    cnSQL = New SqlConnection(ConnectionString)
                    cnSQL.Open()

                    strSqlTitr = "Global.spUD_Dll_RefreshTakhfifPishFaktor_UpdateTakhfif "

                    cmSQL = New SqlCommand(strSqlTitr, cnSQL)
                    cmSQL.CommandType = CommandType.StoredProcedure
                    cmSQL.Parameters.Clear()

                    cmSQL.Parameters.AddWithValue("Takhfif", 0)
                    cmSQL.Parameters.AddWithValue("ccPishFaktorTitr", dr("ccPishFaktorTitr"))

                    cmSQL.ExecuteNonQuery()
                    cnSQL.Close()
                End If
            Next


            ' Gorohe Kala1 -------------------------------------

            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()

            strSQL = "Global.spUD_Dll_RefreshTakhfifPishFaktor_UpdateDarsadTakhfif "

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            cmSQL.Parameters.AddWithValue("DarsadTakhfif", 0)
            cmSQL.Parameters.AddWithValue("ccPishFaktorTitr", dr("ccPishFaktorTitr"))
            cmSQL.Parameters.AddWithValue("sG1", 0)
            cmSQL.Parameters.AddWithValue("sG2", 0)
            cmSQL.Parameters.AddWithValue("sG3", 0)
            cmSQL.Parameters.AddWithValue("Noe", 0)

            cmSQL.ExecuteNonQuery()

            '------------*------------*

            strSQL = "Global.spUD_Dll_RefreshTakhfifPishFaktor_SearchTakhfif "

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            cmSQL.Parameters.AddWithValue("CodeMahal", CodeMahal)
            cmSQL.Parameters.AddWithValue("sNoeSenf", sNoeSenf)
            cmSQL.Parameters.AddWithValue("sNoeMoshtary", sNoeMoshtary)
            cmSQL.Parameters.AddWithValue("sNoePardakht", sNoePardakht)
            cmSQL.Parameters.AddWithValue("Noe", 1)

            If ds.Tables.Contains("tblTakhfifGKala") Then
                ds.Tables.Remove("tblTakhfifGKala")
            End If
            daSQL = New SqlDataAdapter(cmSQL)
            daSQL.Fill(ds, "tblTakhfifGKala")


            For Each drr As DataRow In ds.Tables("tblTakhfifGKala").Rows
                Dim SumG1 As Integer = 0

                strSQL = "Global.spUD_Dll_RefreshTakhfifPishFaktor_CalcSumMkol3 "

                cmSQL = New SqlCommand(strSQL, cnSQL)
                cmSQL.CommandType = CommandType.StoredProcedure
                cmSQL.Parameters.Clear()

                cmSQL.Parameters.AddWithValue("CodeMahal", CodeMahal)
                cmSQL.Parameters.AddWithValue("PishFaktorTarikh", dr("PishFaktorTarikh"))
                cmSQL.Parameters.AddWithValue("ccPishFaktorTitr", dr("ccPishFaktorTitr"))
                cmSQL.Parameters.AddWithValue("sNoePardakht", 0)
                cmSQL.Parameters.AddWithValue("sG1", drr("sG1"))
                cmSQL.Parameters.AddWithValue("sG2", 0)
                cmSQL.Parameters.AddWithValue("sG3", 0)
                cmSQL.Parameters.AddWithValue("Noe", 1)

                SumG1 = objTools.ConvertNulls(cmSQL.ExecuteScalar(), 0)

                If SumG1 >= drr("AzMablagh") And SumG1 <= drr("TaMablagh") Then
                    strSQL = "Global.spUD_Dll_RefreshTakhfifPishFaktor_UpdateDarsadTakhfif "

                    cmSQL = New SqlCommand(strSQL, cnSQL)
                    cmSQL.CommandType = CommandType.StoredProcedure
                    cmSQL.Parameters.Clear()

                    cmSQL.Parameters.AddWithValue("DarsadTakhfif", drr("DarsadTakhfif"))
                    cmSQL.Parameters.AddWithValue("ccPishFaktorTitr", dr("ccPishFaktorTitr"))
                    cmSQL.Parameters.AddWithValue("sG1", drr("sG1"))
                    cmSQL.Parameters.AddWithValue("sG2", 0)
                    cmSQL.Parameters.AddWithValue("sG3", 0)
                    cmSQL.Parameters.AddWithValue("Noe", 1)

                    cmSQL.ExecuteNonQuery()
                End If
            Next

            ' Gorohe Kala2 -------------------------------------

            strSQL = "Global.spUD_Dll_RefreshTakhfifPishFaktor_SearchTakhfif "

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            cmSQL.Parameters.AddWithValue("CodeMahal", CodeMahal)
            cmSQL.Parameters.AddWithValue("sNoeSenf", sNoeSenf)
            cmSQL.Parameters.AddWithValue("sNoeMoshtary", sNoeMoshtary)
            cmSQL.Parameters.AddWithValue("sNoePardakht", sNoePardakht)
            cmSQL.Parameters.AddWithValue("Noe", 2)

            If ds.Tables.Contains("tblTakhfifGKala") Then
                ds.Tables.Remove("tblTakhfifGKala")
            End If
            daSQL = New SqlDataAdapter(cmSQL)
            daSQL.Fill(ds, "tblTakhfifGKala")


            For Each drr As DataRow In ds.Tables("tblTakhfifGKala").Rows
                Dim SumG2 As Integer = 0

                strSQL = "Global.spUD_Dll_RefreshTakhfifPishFaktor_CalcSumMkol3 "

                cmSQL = New SqlCommand(strSQL, cnSQL)
                cmSQL.CommandType = CommandType.StoredProcedure
                cmSQL.Parameters.Clear()

                cmSQL.Parameters.AddWithValue("CodeMahal", CodeMahal)
                cmSQL.Parameters.AddWithValue("PishFaktorTarikh", dr("PishFaktorTarikh"))
                cmSQL.Parameters.AddWithValue("ccPishFaktorTitr", dr("ccPishFaktorTitr"))
                cmSQL.Parameters.AddWithValue("sNoePardakht", 0)
                cmSQL.Parameters.AddWithValue("sG1", drr("sG1"))
                cmSQL.Parameters.AddWithValue("sG2", drr("sG2"))
                cmSQL.Parameters.AddWithValue("sG3", 0)
                cmSQL.Parameters.AddWithValue("Noe", 2)

                SumG2 = objTools.ConvertNulls(cmSQL.ExecuteScalar(), 0)

                If SumG2 >= drr("AzMablagh") And SumG2 <= drr("TaMablagh") Then

                    strSQL = "Global.spUD_Dll_RefreshTakhfifPishFaktor_UpdateDarsadTakhfif "

                    cmSQL = New SqlCommand(strSQL, cnSQL)
                    cmSQL.CommandType = CommandType.StoredProcedure
                    cmSQL.Parameters.Clear()

                    cmSQL.Parameters.AddWithValue("DarsadTakhfif", drr("DarsadTakhfif"))
                    cmSQL.Parameters.AddWithValue("ccPishFaktorTitr", dr("ccPishFaktorTitr"))
                    cmSQL.Parameters.AddWithValue("sG1", drr("sG1"))
                    cmSQL.Parameters.AddWithValue("sG2", drr("sG2"))
                    cmSQL.Parameters.AddWithValue("sG3", 0)
                    cmSQL.Parameters.AddWithValue("Noe", 2)

                    cmSQL.ExecuteNonQuery()
                End If
            Next

            ' Gorohe Kala3 -------------------------------------

            strSQL = "Global.spUD_Dll_RefreshTakhfifPishFaktor_SearchTakhfif "

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            cmSQL.Parameters.AddWithValue("CodeMahal", CodeMahal)
            cmSQL.Parameters.AddWithValue("sNoeSenf", sNoeSenf)
            cmSQL.Parameters.AddWithValue("sNoeMoshtary", sNoeMoshtary)
            cmSQL.Parameters.AddWithValue("sNoePardakht", sNoePardakht)
            cmSQL.Parameters.AddWithValue("Noe", 3)

            If ds.Tables.Contains("tblTakhfifGKala") Then
                ds.Tables.Remove("tblTakhfifGKala")
            End If
            daSQL = New SqlDataAdapter(cmSQL)
            daSQL.Fill(ds, "tblTakhfifGKala")


            For Each drr As DataRow In ds.Tables("tblTakhfifGKala").Rows
                Dim SumG3 As Integer = 0

                strSQL = "Global.spUD_Dll_RefreshTakhfifPishFaktor_CalcSumMkol3 "

                cmSQL = New SqlCommand(strSQL, cnSQL)
                cmSQL.CommandType = CommandType.StoredProcedure
                cmSQL.Parameters.Clear()

                cmSQL.Parameters.AddWithValue("CodeMahal", CodeMahal)
                cmSQL.Parameters.AddWithValue("PishFaktorTarikh", dr("PishFaktorTarikh"))
                cmSQL.Parameters.AddWithValue("ccPishFaktorTitr", dr("ccPishFaktorTitr"))
                cmSQL.Parameters.AddWithValue("sNoePardakht", 0)
                cmSQL.Parameters.AddWithValue("sG1", drr("sG1"))
                cmSQL.Parameters.AddWithValue("sG2", drr("sG2"))
                cmSQL.Parameters.AddWithValue("sG3", drr("sG3"))
                cmSQL.Parameters.AddWithValue("Noe", 3)

                SumG3 = objTools.ConvertNulls(cmSQL.ExecuteScalar(), 0)

                If SumG3 >= drr("AzMablagh") And SumG3 <= drr("TaMablagh") Then

                    strSQL = "Global.spUD_Dll_RefreshTakhfifPishFaktor_UpdateDarsadTakhfif "

                    cmSQL = New SqlCommand(strSQL, cnSQL)
                    cmSQL.CommandType = CommandType.StoredProcedure
                    cmSQL.Parameters.Clear()

                    cmSQL.Parameters.AddWithValue("DarsadTakhfif", drr("DarsadTakhfif"))
                    cmSQL.Parameters.AddWithValue("ccPishFaktorTitr", dr("ccPishFaktorTitr"))
                    cmSQL.Parameters.AddWithValue("sG1", drr("sG1"))
                    cmSQL.Parameters.AddWithValue("sG2", drr("sG2"))
                    cmSQL.Parameters.AddWithValue("sG3", drr("sG3"))
                    cmSQL.Parameters.AddWithValue("Noe", 3)

                    cmSQL.ExecuteNonQuery()
                End If
            Next


            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()

            If ds.Tables.Contains("HESatr") Then
                ds.Tables.Remove("HESatr")
            End If

            strSQL = "Global.spUD_Dll_RefreshTakhfifPishFaktor_SearchSatr "

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            cmSQL.Parameters.AddWithValue("ccPishFaktorTitr", dr("ccPishFaktorTitr"))

            daSQL = New SqlDataAdapter(cmSQL)
            daSQL.Fill(ds, "HESatr")

            For Each drrr As DataRow In ds.Tables("HESatr").Rows
                Dim TakhfifKala As Double
                'If Mode = UD_Dll.Enums.GL_ModeForms.AddNewRow Then
                TakhfifKala = ObjCode.FindDarsadTakhfifRow( _
                                                       dr("ccPishFaktorTitr"), _
                                                       dr("sNoePardakht"), _
                                                       (drrr("Tedad3") * drrr("Fee")), _
                                                       dr("ccMoshtary"), _
                                                       drrr("ccKala"))
                'End If

                strSQL = "Global.spUD_Dll_RefreshTakhfifPishFaktor_UpdateTakhfif_DarsadTakhfif "

                cmSQL = New SqlCommand(strSQL, cnSQL)
                cmSQL.CommandType = CommandType.StoredProcedure
                cmSQL.Parameters.Clear()

                cmSQL.Parameters.AddWithValue("TakhfifKala", (((drrr("Fee") * drrr("Tedad3")) * (drrr("DarsadTakhfif") + TakhfifKala)) / 100).ToString.Replace(",", "."))
                cmSQL.Parameters.AddWithValue("DarsadTakhfif", (drrr("DarsadTakhfif") + TakhfifKala).ToString.Replace(",", "."))
                cmSQL.Parameters.AddWithValue("ccPishFaktorSatr", drrr("ccPishFaktorSatr"))

                cmSQL.ExecuteNonQuery()
            Next
            cnSQL.Close()
            cmSQL = Nothing : cnSQL = Nothing

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->RefreshJayezeh")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->RefreshJayezeh")
        Finally

        End Try
    End Sub
    Public Function GetJayezehKala(ByVal Tarikh As String, ByVal Tedad As Integer, _
                                      ByVal ccKala As Integer, ByVal ccMoshtary As Long) As DataTable
        Dim cnSQL As SqlConnection
        Dim strSQL As String
        Dim ds As New DataSet
        Dim daSQL As SqlDataAdapter

        Dim sNoeMoshtary As Integer = objTools.ConvertNulls(objTools.DLookup("sNoeMoshtary", "tblFO_Moshtary", "ccMoshtary=" & ccMoshtary), 0)

        cnSQL = New SqlConnection(ConnectionString)
        cnSQL.Open()



        strSQL = "select ccKalaJayezeh,Sum(TedadJayezeh) as TedadJayezeh,"
        strSQL &= "isnull((Select dbo.[GetMablaghKharid_BeduneTaminKonandeh]( Main.ccKalaJayezeh ,'" & Tarikh & "')),0) as Fee,"
        strSQL &= "(Select sVahedeShomaresh from tblAN_Kala where ccKala=Main.ccKalaJayezeh) as sVahed"
        strSQL &= " From dbo.fnFO_GetJayezehKala('" & Tarikh & "'," & Tedad & "," & ccMoshtary & "," & _
                                                sNoeMoshtary & "," & ccKala & ") Main"

        strSQL &= " Group By ccKalaJayezeh"

        daSQL = New SqlDataAdapter(strSQL, ConnectionString)
        daSQL.Fill(ds, "tblJayezeh")

        cnSQL.Close()
        cnSQL = Nothing

        GetJayezehKala = ds.Tables("tblJayezeh")
        Return ds.Tables("tblJayezeh")
    End Function
    Public Function GetJayezehKalaDarsad(ByVal Tarikh As String, ByVal Tedad As Integer, _
                                   ByVal ccKala As Integer, ByVal ccMoshtary As Long) As Double

        Dim cnSQL As SqlConnection
        Dim cmSQL As SqlCommand
        Dim strSQL As String

        Dim sNoeMoshtary As Integer = objTools.ConvertNulls(objTools.DLookup("sNoeMoshtary", "tblFO_Moshtary", "ccMoshtary=" & ccMoshtary), 0)

        cnSQL = New SqlConnection(ConnectionString)
        cnSQL.Open()

        strSQL = "Global.spUD_Dll_GetJayezehKalaDarsad"

        cmSQL = New SqlCommand(strSQL, cnSQL)
        cmSQL.CommandType = CommandType.StoredProcedure
        cmSQL.Parameters.Clear()

        cmSQL.Parameters.AddWithValue("Tarikh", Tarikh)
        cmSQL.Parameters.AddWithValue("Tedad", Tedad)
        cmSQL.Parameters.AddWithValue("ccMoshtary", ccMoshtary)
        cmSQL.Parameters.AddWithValue("sNoeMoshtary", sNoeMoshtary)
        cmSQL.Parameters.AddWithValue("ccKala", ccKala)

        GetJayezehKalaDarsad = objTools.ConvertNulls(cmSQL.ExecuteScalar(), 0)

        cnSQL.Close()
        cmSQL = Nothing : cnSQL = Nothing
    End Function
    Public Function GetJayezehGorohKala(ByVal Tarikh As String, ByVal Tedad As Integer, _
                                   ByVal sG1 As Integer, ByVal ccMoshtary As Long) As DataTable
        Dim cnSQL As SqlConnection
        Dim strSQL As String
        Dim ds As New DataSet
        Dim daSQL As SqlDataAdapter

        Dim sNoeMoshtary As Integer = objTools.ConvertNulls(objTools.DLookup("sNoeMoshtary", "tblFO_Moshtary", "ccMoshtary=" & ccMoshtary), 0)

        cnSQL = New SqlConnection(ConnectionString)
        cnSQL.Open()

        strSQL = "select ccKalaJayezeh,Sum(TedadJayezeh) as TedadJayezeh,"
        strSQL &= "isnull((Select dbo.[GetMablaghKharid_BeduneTaminKonandeh]( Main.ccKalaJayezeh ,'" & Tarikh & "')),0) as Fee,"
        strSQL &= "(Select sVahedeShomaresh from tblAN_Kala where ccKala=Main.ccKalaJayezeh) as sVahed"
        strSQL &= " From dbo.fnFO_GetJayezehGorohKala('" & Tarikh & "'," & Tedad & "," & ccMoshtary & "," & sNoeMoshtary & "," & _
                                                sG1 & ") Main"
        strSQL &= " Group By ccKalaJayezeh"

        daSQL = New SqlDataAdapter(strSQL, ConnectionString)
        daSQL.Fill(ds, "tblJayezeh")

        cnSQL.Close()
        cnSQL = Nothing

        GetJayezehGorohKala = ds.Tables("tblJayezeh")
        Return ds.Tables("tblJayezeh")
    End Function
    Public Function GetJayezehGorohKalaDarsad(ByVal Tarikh As String, ByVal Tedad As Integer, _
                                   ByVal sG1 As Integer, ByVal ccMoshtary As Long) As Double
        Dim cnSQL As SqlConnection
        Dim cmSQL As SqlCommand
        Dim strSQL As String


        Dim sNoeMoshtary As Integer = objTools.ConvertNulls(objTools.DLookup("sNoeMoshtary", "tblFO_Moshtary", "ccMoshtary=" & ccMoshtary), 0)

        cnSQL = New SqlConnection(ConnectionString)
        cnSQL.Open()

        strSQL = "select ccKalaJayezeh,Sum(TedadJayezeh) as TedadJayezeh,"
        strSQL &= "isnull((Select dbo.[GetMablaghKharid_BeduneTaminKonandeh]( Main.ccKalaJayezeh ,'" & Tarikh & "')),0) as Fee,"
        strSQL &= "(Select sVahedeShomaresh from tblAN_Kala where ccKala=Main.ccKalaJayezeh) as sVahed"
        strSQL &= " From dbo.fnFO_GetJayezehGorohKala('" & Tarikh & "'," & Tedad & "," & ccMoshtary & "," & sNoeMoshtary & "," & _
                                                sG1 & ") Main"
        strSQL &= " Group By ccKalaJayezeh"

        cmSQL = New SqlCommand(strSQL, cnSQL)
        GetJayezehGorohKalaDarsad = objTools.ConvertNulls(cmSQL.ExecuteScalar(), 0)

        cnSQL.Close()
        cmSQL = Nothing : cnSQL = Nothing
    End Function
    Public Function GetJayezehBrand(ByVal Tarikh As String, ByVal Tedad As Integer, _
                               ByVal ccBrand As Integer, ByVal ccMoshtary As Long) As DataTable
        Dim cnSQL As SqlConnection
        Dim strSQL As String
        Dim ds As New DataSet
        Dim daSQL As SqlDataAdapter

        Dim sNoeMoshtary As Integer = objTools.ConvertNulls(objTools.DLookup("sNoeMoshtary", "tblFO_Moshtary", "ccMoshtary=" & ccMoshtary), 0)

        cnSQL = New SqlConnection(ConnectionString)
        cnSQL.Open()


        strSQL = "select ccKalaJayezeh,Sum(TedadJayezeh) as TedadJayezeh,"
        strSQL &= "isnull((Select dbo.[GetMablaghKharid_BeduneTaminKonandeh]( Main.ccKalaJayezeh ,'" & Tarikh & "')),0) as Fee,"
        strSQL &= "(Select sVahedeShomaresh from tblAN_Kala where ccKala=Main.ccKalaJayezeh) as sVahed"
        strSQL &= " From dbo.fnFO_GetJayezehBrand('" & Tarikh & "'," & Tedad & "," & ccMoshtary & "," & sNoeMoshtary & "," & _
                                                ccBrand & ") Main"

        strSQL &= " Group By ccKalaJayezeh"

        daSQL = New SqlDataAdapter(strSQL, ConnectionString)
        daSQL.Fill(ds, "tblJayezeh")

        cnSQL.Close()
        cnSQL = Nothing

        GetJayezehBrand = ds.Tables("tblJayezeh")
        Return ds.Tables("tblJayezeh")
    End Function
    Public Function GetJayezehBrandDarsad(ByVal Tarikh As String, ByVal Tedad As Integer, _
                               ByVal ccBrand As Integer, ByVal ccMoshtary As Long) As DataTable

        Dim cnSQL As SqlConnection
        Dim cmSQL As SqlCommand
        Dim strSQL As String

        Dim sNoeMoshtary As Integer = objTools.ConvertNulls(objTools.DLookup("sNoeMoshtary", "tblFO_Moshtary", "ccMoshtary=" & ccMoshtary), 0)

        cnSQL = New SqlConnection(ConnectionString)
        cnSQL.Open()


        strSQL = "select ccKalaJayezeh,Sum(TedadJayezeh) as TedadJayezeh,"
        strSQL &= "isnull((Select dbo.[GetMablaghKharid_BeduneTaminKonandeh]( Main.ccKalaJayezeh ,'" & Tarikh & "')),0) as Fee,"
        strSQL &= "(Select sVahedeShomaresh from tblAN_Kala where ccKala=Main.ccKalaJayezeh) as sVahed"
        strSQL &= " From dbo.fnFO_GetJayezehBrand('" & Tarikh & "'," & Tedad & "," & ccMoshtary & "," & sNoeMoshtary & "," & _
                                                ccBrand & ") Main"

        strSQL &= " Group By ccKalaJayezeh"


        cmSQL = New SqlCommand(strSQL, cnSQL)
        GetJayezehBrandDarsad = objTools.ConvertNulls(cmSQL.ExecuteScalar(), 0)

        cnSQL.Close()
        cmSQL = Nothing : cnSQL = Nothing
    End Function
    Public Function KalaBedoneTakhfif(ByVal Tarikh As String) As DataView
        Dim cnSQL As SqlConnection
        Dim cmSQL As SqlCommand
        Dim strSQL As String
        Dim ds As New DataSet
        Dim daSQL As SqlDataAdapter

        cnSQL = New SqlConnection(ConnectionString)
        cnSQL.Open()

        strSQL = "Global.spUD_Dll_KalaBedoneTakhfif "

        cmSQL = New SqlCommand(strSQL, cnSQL)
        cmSQL.CommandType = CommandType.StoredProcedure
        cmSQL.Parameters.Clear()

        cmSQL.Parameters.AddWithValue("Tarikh", Tarikh)

        daSQL = New SqlDataAdapter(cmSQL)
        daSQL.Fill(ds, "tblKalaBedoneTakhfif")

        cnSQL.Close() : cnSQL = Nothing

        Return ds.Tables("tblKalaBedoneTakhfif").DefaultView
    End Function
    Public Function IsFaktorExists(ByVal CodeMahal As Long, ByVal CodeDoreh As Long, ByVal FaktorShomareh As Long) As Long
        IsFaktorExists = objTools.ConvertNulls(objTools.DLookup("ccFaktorTitr", "tblFO_Faktor", "CodeMahal = " & CodeMahalFaal & " AND CodeDoreh = " & CodeDoreh & " AND FaktorShomareh = " & FaktorShomareh), 0)
    End Function
    Public Function IsFaktorMoshtaryExists(ByVal CodeMahal As Long, ByVal CodeDoreh As Long, ByVal FaktorShomareh As Long, ByVal ccMoshtary As Long) As Long
        IsFaktorMoshtaryExists = objTools.ConvertNulls(objTools.DLookup("ccFaktorTitr", "tblFO_Faktor", "CodeMahal = " & CodeMahalFaal & " AND CodeDoreh = " & CodeDoreh & " AND FaktorShomareh = " & FaktorShomareh & " and ccMoshtary In (" & ccMoshtaryLinks(ccMoshtary) & ccMoshtary & ")"), 0)
    End Function
    Public Function ccMoshtaryLinks(ByVal ccMoshtary As Long) As String
        Dim strMoshtary As String = ""

        If objTools.ConvertNulls(objTools.DLookup("MoshtaryAsly", "tblFO_Moshtary", "ccMoshtary=" & ccMoshtary), 0) = 1 Then
            For Each dr As DataRow In ObjCode.ListMoshtaryLink(ccMoshtary).Table.Rows
                strMoshtary &= dr("ccMoshtary").ToString + ","
            Next
        End If

        Return strMoshtary
    End Function
    Public Function GetMablaghFaktor(ByVal CodeMahal As Long, ByVal CodeDoreh As Long, ByVal FaktorShomareh As Long) As Long
        GetMablaghFaktor = objTools.ConvertNulls(objTools.DLookup("JamFaktor", "tblFO_Faktor", "CodeMahal = " & CodeMahal & " AND CodeDoreh = " & CodeDoreh & " AND FaktorShomareh = " & FaktorShomareh), 0)
    End Function
    Public Function GetMablaghFaktorJayezeh(ByVal CodeMahal As Long, ByVal CodeDoreh As Long, ByVal FaktorShomareh As Long) As Long
        GetMablaghFaktorJayezeh = objTools.ConvertNulls(objTools.DLookup("JamJayezeh", "tblFO_Faktor", "CodeMahal = " & CodeMahal & " AND CodeDoreh = " & CodeDoreh & " AND FaktorShomareh = " & FaktorShomareh), 0)
    End Function
    Public Function GetTakhfifFaktor(ByVal CodeMahal As Long, ByVal CodeDoreh As Long, ByVal FaktorShomareh As Long) As Long
        GetTakhfifFaktor = objTools.ConvertNulls(objTools.DLookup("JamTakhfif", "qryFO_Faktor", "CodeMahal = " & CodeMahal & " AND CodeDoreh = " & CodeDoreh & " AND FaktorShomareh = " & FaktorShomareh), 0)
    End Function
    Public Function GetMarjoeeFaktor(ByVal CodeMahal As Long, ByVal CodeDoreh As Long, ByVal FaktorShomareh As Long) As Long
        GetMarjoeeFaktor = objTools.ConvertNulls(objTools.DLookup("MablaghMarjoee", "qryFO_Faktor", "CodeMahal = " & CodeMahal & " AND CodeDoreh = " & CodeDoreh & " AND FaktorShomareh = " & FaktorShomareh), 0)
    End Function
    Public Function GetForoshandehMasirNobatVizit(ByVal tTarikh As String, ByVal strccForoshandeh As String) As DataTable
        Dim cnSQL As SqlConnection = Nothing
        Dim cmSQL As SqlCommand = Nothing
        Dim daSQL As SqlDataAdapter = Nothing
        Dim strSQL As String = ""

        Dim tbl As DataTable = Nothing
        GetForoshandehMasirNobatVizit = tbl
        Try
            tbl = New DataTable
            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()

            strSQL = "Global.spUD_Dll_GetForoshandehMasirNobatVizit "

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            cmSQL.Parameters.AddWithValue("Tarikh", tTarikh)
            cmSQL.Parameters.AddWithValue("strccForoshandeh", strccForoshandeh)

            daSQL = New SqlDataAdapter(cmSQL)
            daSQL.SelectCommand.CommandTimeout = 999999
            daSQL.Fill(tbl)
            GetForoshandehMasirNobatVizit = tbl
            Return tbl
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->Print NobatVisit")
        Finally
            cnSQL.Close()
            cnSQL = Nothing
        End Try
    End Function
    Public Function SaghfEtebarMoshtary(ByVal ccMoshtary As Long) As Double
        Return objTools.ConvertNulls(objTools.DLookup("EtebarRial", "tblFO_Moshtary", "ccMoshtary =" & ccMoshtary), 0)
    End Function
    Public Function TedadFaktorBazMoshtary(ByVal ccMoshtary As Integer) As Double
        Return objTools.ConvertNulls(objTools.DLookup("EtebarTedady", "tblFO_Moshtary", "ccMoshtary =" & ccMoshtary), 0)
    End Function
    Public Function GetBedehy(ByVal ccMoshtary As Long, ByVal CodeDoreh As Integer) As Double
        Dim cnSQL As SqlConnection
        Dim cmSQL As SqlCommand
        Dim strSQL As String

        cnSQL = New SqlConnection(ConnectionString)
        cnSQL.Open()

        strSQL = "Global.spUD_Dll_GetBedehy "

        cmSQL = New SqlCommand(strSQL, cnSQL)
        cmSQL.CommandType = CommandType.StoredProcedure
        cmSQL.Parameters.Clear()

        cmSQL.Parameters.AddWithValue("CodeDorehAval", CodeDorehAval)
        cmSQL.Parameters.AddWithValue("TarikhEmrooz", TarikhEmrooz)
        cmSQL.Parameters.AddWithValue("ccMoshtary", ccMoshtary)

        GetBedehy = objTools.ConvertNulls(cmSQL.ExecuteScalar(), 0)

        cnSQL.Close() : cnSQL = Nothing
        Return GetBedehy
    End Function
    Public Function GetSoodVaZian(ByVal Tarikh As String, ByVal CodeDoreh As Integer) As Double
        Dim cnSQL As SqlConnection
        Dim cmSQL As SqlCommand
        Dim strSQL As String

        cnSQL = New SqlConnection(ConnectionString)
        cnSQL.Open()

        strSQL = "Select dbo.GetSoodVaZian('" & Str(CodeDoreh).Trim + "0101','" & Tarikh & "')"

        cmSQL = New SqlCommand(strSQL, cnSQL)
        GetSoodVaZian = objTools.ConvertNulls(cmSQL.ExecuteScalar(), 0)

        cnSQL.Close() : cnSQL = Nothing
        Return GetSoodVaZian
    End Function
    Public Function GetBedehiVaSarmayeh(ByVal TaTarikh As String, ByVal CodeDoreh As Integer) As Double
        Dim cnSQL As SqlConnection
        Dim cmSQL As SqlCommand
        Dim strSQL As String

        cnSQL = New SqlConnection(ConnectionString)
        cnSQL.Open()

        strSQL = "Global.spUD_Dll_GetBedehiVaSarmayeh "

        cmSQL = New SqlCommand(strSQL, cnSQL)
        cmSQL.CommandType = CommandType.StoredProcedure
        cmSQL.Parameters.Clear()

        cmSQL.Parameters.AddWithValue("AzTarikh", Str(CodeDoreh).Trim & "0101")
        cmSQL.Parameters.AddWithValue("TaTarikh", TaTarikh)

        GetBedehiVaSarmayeh = objTools.ConvertNulls(cmSQL.ExecuteScalar(), 0)

        cnSQL.Close() : cnSQL = Nothing
        Return GetBedehiVaSarmayeh
    End Function
    Public Function GetDaraie(ByVal TaTarikh As String, ByVal CodeDoreh As Integer) As Double
        Dim cnSQL As SqlConnection
        Dim cmSQL As SqlCommand
        Dim strSQL As String

        cnSQL = New SqlConnection(ConnectionString)
        cnSQL.Open()

        strSQL = "Global.spUD_Dll_GetDaraie "

        cmSQL = New SqlCommand(strSQL, cnSQL)
        cmSQL.CommandType = CommandType.StoredProcedure
        cmSQL.Parameters.Clear()

        cmSQL.Parameters.AddWithValue("AzTarikh", Str(CodeDoreh).Trim & "0101")
        cmSQL.Parameters.AddWithValue("TaTarikh", TaTarikh)

        cmSQL = New SqlCommand(strSQL, cnSQL)
        GetDaraie = objTools.ConvertNulls(cmSQL.ExecuteScalar(), 0)

        cnSQL.Close() : cnSQL = Nothing
        Return GetDaraie
    End Function
    Public Function GetTakhfif(ByVal Tarikh As String, ByVal ccMoshtary As Integer, ByVal NoePardakht As Integer, ByVal Mablagh As Integer, ByVal NoeMoshtary As Integer, ByVal NoeSenf As Integer) As Double

        Dim Takhfif As Double

        Dim cnSQL As SqlConnection
        Dim cmSQL As SqlCommand
        Dim strSQL As String

        cnSQL = New SqlConnection(ConnectionString)
        cnSQL.Open()

        strSQL = "Global.spUD_Dll_GetTakhfif "

        cmSQL = New SqlCommand(strSQL, cnSQL)
        cmSQL.CommandType = CommandType.StoredProcedure
        cmSQL.Parameters.Clear()

        cmSQL.Parameters.AddWithValue("Tarikh", Tarikh)
        cmSQL.Parameters.AddWithValue("ccMoshtary", ccMoshtary)
        cmSQL.Parameters.AddWithValue("NoePardakht", NoePardakht)
        cmSQL.Parameters.AddWithValue("Mablagh", Mablagh)
        cmSQL.Parameters.AddWithValue("NoeMoshtary", NoeMoshtary)
        cmSQL.Parameters.AddWithValue("NoeSenf", NoeSenf)
        strSQL = " Select sum(DarsadTakhfif) from "

        Takhfif = objTools.ConvertNulls(cmSQL.ExecuteScalar(), 0)

        cnSQL.Close() : cnSQL = Nothing
        Return GetTakhfif

    End Function
    Public Function GetTakhfifKala(ByVal Tarikh As String, ByVal NoePardakht As Integer, ByVal Mablagh As Integer, ByVal ccMoshtary As Integer, ByVal NoeMoshtary As Integer, ByVal ccKala As Integer, ByVal codeGorohKala As Integer, ByVal ccBrand As Integer) As Double

        Dim Takhfif As Double

        Dim cnSQL As SqlConnection
        Dim cmSQL As SqlCommand
        Dim strSQL As String

        cnSQL = New SqlConnection(ConnectionString)
        cnSQL.Open()

        strSQL = "Global.spUD_Dll_GetTakhfifKala "

        cmSQL = New SqlCommand(strSQL, cnSQL)
        cmSQL.CommandType = CommandType.StoredProcedure
        cmSQL.Parameters.Clear()

        cmSQL.Parameters.AddWithValue("Tarikh", Tarikh)
        cmSQL.Parameters.AddWithValue("NoePardakht", NoePardakht)
        cmSQL.Parameters.AddWithValue("Mablagh", Mablagh)
        cmSQL.Parameters.AddWithValue("ccMoshtary", ccMoshtary)
        cmSQL.Parameters.AddWithValue("NoeMoshtary", NoeMoshtary)
        cmSQL.Parameters.AddWithValue("ccKala", ccKala)
        cmSQL.Parameters.AddWithValue("codeGorohKala", codeGorohKala)
        cmSQL.Parameters.AddWithValue("ccBrand", ccBrand)

        Takhfif = objTools.ConvertNulls(cmSQL.ExecuteScalar(), 0)

        cnSQL.Close() : cnSQL = Nothing
        Return GetTakhfifKala

    End Function
    Public Function GetCheckPassNashodeh(ByVal ccMoshtary As Long, ByVal CodeDoreh As Integer) As Double
        Dim cnSQL As SqlConnection
        Dim cmSQL As SqlCommand
        Dim strSQL As String

        cnSQL = New SqlConnection(ConnectionString)
        cnSQL.Open()

        strSQL = "Global.spUD_Dll_GetCheckPassNashodeh "

        cmSQL = New SqlCommand(strSQL, cnSQL)
        cmSQL.CommandType = CommandType.StoredProcedure
        cmSQL.Parameters.Clear()

        cmSQL.Parameters.AddWithValue("AzTarikh", "13800101")
        cmSQL.Parameters.AddWithValue("TaTarikh", TarikhEmrooz)
        cmSQL.Parameters.AddWithValue("ccMoshtary", ccMoshtary)

        GetCheckPassNashodeh = objTools.ConvertNulls(cmSQL.ExecuteScalar(), 0)

        cnSQL.Close() : cnSQL = Nothing
        Return GetCheckPassNashodeh
    End Function
    Public Function GetMablaghFaktorBaz(ByVal ccMoshtary As Long) As Double

        Dim cnSQL As SqlConnection
        Dim cmSQL As SqlCommand
        Dim strSQL As String

        cnSQL = New SqlConnection(ConnectionString)
        cnSQL.Open()

        strSQL = "Global.spUD_Dll_GetMablaghFaktorBaz "

        cmSQL = New SqlCommand(strSQL, cnSQL)
        cmSQL.CommandType = CommandType.StoredProcedure
        cmSQL.Parameters.Clear()

        cmSQL.Parameters.AddWithValue("ccMoshtary", ccMoshtary)

        GetMablaghFaktorBaz = objTools.ConvertNulls(cmSQL.ExecuteScalar(), 0)

        cnSQL.Close() : cnSQL = Nothing
        Return GetMablaghFaktorBaz
    End Function
    Public Function GetTedadFaktorBaz(ByVal ccMoshtary As Long) As Double
        Dim cnSQL As SqlConnection
        Dim cmSQL As SqlCommand
        Dim strSQL As String

        cnSQL = New SqlConnection(ConnectionString)
        cnSQL.Open()

        strSQL = "Global.spUD_Dll_GetTedadFaktorBaz "

        cmSQL = New SqlCommand(strSQL, cnSQL)
        cmSQL.CommandType = CommandType.StoredProcedure
        cmSQL.Parameters.Clear()

        cmSQL.Parameters.AddWithValue("ccMoshtary", ccMoshtary)

        GetTedadFaktorBaz = objTools.ConvertNulls(cmSQL.ExecuteScalar(), 0)

        cnSQL.Close() : cnSQL = Nothing
        Return GetTedadFaktorBaz
    End Function
    Public Function GetEtebarTedadMojaz_Foroshandeh(ByVal CodeFard As Integer, ByVal RoozMojazForoshandeh As Integer) As Double
        Dim cnSQL As SqlConnection
        Dim cmSQL As SqlCommand
        Dim strSQL As String

        cnSQL = New SqlConnection(ConnectionString)
        cnSQL.Open()

        strSQL = "Global.spUD_Dll_GetEtebarTedadMojaz_Foroshandeh "

        cmSQL = New SqlCommand(strSQL, cnSQL)
        cmSQL.CommandType = CommandType.StoredProcedure
        cmSQL.Parameters.Clear()

        cmSQL.Parameters.AddWithValue("CodeFard", CodeFard)
        cmSQL.Parameters.AddWithValue("RoozMojazForoshandeh", RoozMojazForoshandeh)

        GetEtebarTedadMojaz_Foroshandeh = objTools.ConvertNulls(cmSQL.ExecuteScalar(), 0)

        cnSQL.Close() : cnSQL = Nothing
        Return GetEtebarTedadMojaz_Foroshandeh
    End Function
    Public Function ListMoshtaryLink(ByVal ccMoshtary As String) As DataView
        Dim cnSQL As SqlConnection
        Dim strSQL As String
        Dim cmSQL As SqlCommand
        Dim dvMoshtary As DataView
        Dim dsMoshtary As New DataSet
        Dim daSQL As SqlDataAdapter

        cnSQL = New SqlConnection(ConnectionString)
        cnSQL.Open()

        strSQL = "Global.spUD_Dll_ListMoshtaryLink "

        cmSQL = New SqlCommand(strSQL, cnSQL)
        cmSQL.CommandType = CommandType.StoredProcedure
        cmSQL.Parameters.Clear()

        cmSQL.Parameters.AddWithValue("ccMoshtary_Link", ccMoshtary)
        cmSQL.Parameters.AddWithValue("sVazeiat", Enums.FO_VaziatMoshtary.Faal)

        daSQL = New SqlDataAdapter(cmSQL)
        daSQL.Fill(dsMoshtary, "tblListMoshtary")

        dvMoshtary = New DataView(dsMoshtary.Tables("tblListMoshtary"))

        cnSQL.Close() : cnSQL = Nothing

        Return dvMoshtary
    End Function
    Public Function GetHaveMarjoeeFilter(ByVal ccMoshtary As Long, ByVal sNoeTasvieh As Integer) As Integer
        Dim TedadMarjoee As Integer
        TedadMarjoee = objTools.ConvertNulls(objTools.DCount("ShomarehElamMarjoee", "tblFO_ElamMarjoee", _
            "ccMoshtary = " & ccMoshtary & _
            " AND ShomarehElamMarjoee not in (select ShomarehElamMarjoee from tblAN_MarjoeeAzMoshtary where CodeMahal=" & CodeMahalFaal & " AND CodeDoreh =" & CodeDoreh & ")" & _
            " AND sVazeiat = " & Enums.FO_VaziatElamMarjoee.TaeedShodeh & " AND ccFaktorM=0" & _
            " AND sNoeTasvieh = " & sNoeTasvieh & _
            " AND CodeMahal = " & CodeMahalFaal & " AND CodeDoreh = " & CodeDoreh), 0)
        Return TedadMarjoee
    End Function
    Public Function GetHaveMarjoee(ByVal ccMoshtary As Long) As Integer
        Dim TedadMarjoee As Integer
        TedadMarjoee = objTools.ConvertNulls(objTools.DCount("ShomarehElamMarjoee", "tblFO_ElamMarjoee", _
            "ccMoshtary = " & ccMoshtary & _
            " AND ShomarehElamMarjoee not in (select ShomarehElamMarjoee from tblAN_MarjoeeAzMoshtary where CodeMahal=" & CodeMahalFaal & " AND CodeDoreh =" & CodeDoreh & ")" & _
            " AND sVazeiat = " & Enums.FO_VaziatElamMarjoee.TaeedShodeh & " AND ccFaktorM=0" & _
            " AND CodeMahal = " & CodeMahalFaal & " AND CodeDoreh = " & CodeDoreh), 0)
        Return TedadMarjoee
    End Function
    Public Function GetMKOLMarjoee(ByVal ccMoshtary As Long, ByVal ccElamMarjoee As Long) As Long
        Dim MKOLMarjoee As Long

        Dim cnSQL As SqlConnection
        Dim cmSQL As SqlCommand
        Dim strSQL As String

        cnSQL = New SqlConnection(ConnectionString)
        cnSQL.Open()
        Try

            strSQL = "Global.spUD_Dll_GetMKOLMarjoee "

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            cmSQL.Parameters.AddWithValue("ccMarjoee", ccElamMarjoee)
            cmSQL.Parameters.AddWithValue("ccMoshtary", ccMoshtary)
            cmSQL.Parameters.AddWithValue("sVazeiat", Enums.FO_VaziatElamMarjoee.TaeedShodeh)
            cmSQL.Parameters.AddWithValue("CodeMahal", CodeMahalFaal)
            cmSQL.Parameters.AddWithValue("CodeDoreh", CodeDoreh)

            MKOLMarjoee = objTools.ConvertNulls(cmSQL.ExecuteScalar(), 0)

            cnSQL.Close()
            cmSQL = Nothing : cnSQL = Nothing
        Catch ex As Exception

        End Try

        Return MKOLMarjoee
    End Function
    Public Function GetccElamMarjoee(ByVal ccMoshtary As Long)
        Dim ccElamMarjoee As Long

        Dim cnSQL As SqlConnection
        Dim cmSQL As SqlCommand
        Dim strSQL As String

        cnSQL = New SqlConnection(ConnectionString)
        cnSQL.Open()

        strSQL = "Global.spUD_Dll_GetccElamMarjoee "

        cmSQL = New SqlCommand(strSQL, cnSQL)
        cmSQL.CommandType = CommandType.StoredProcedure
        cmSQL.Parameters.Clear()

        cmSQL.Parameters.AddWithValue("ccMoshtary", ccMoshtary)
        cmSQL.Parameters.AddWithValue("codeDoreh", CodeDoreh)
        cmSQL.Parameters.AddWithValue("CodeMahal", CodeMahalFaal)
        cmSQL.Parameters.AddWithValue("sVazeiat", Enums.FO_VaziatElamMarjoee.TaeedShodeh)

        ccElamMarjoee = objTools.ConvertNulls(cmSQL.ExecuteScalar(), 0)

        cnSQL.Close()
        cmSQL = Nothing : cnSQL = Nothing

        Return ccElamMarjoee
    End Function
    Public Function ElamMarjoeeBaz(ByVal ccMoshtary As Long)
        Dim CountMarjoee As Integer

        Dim cnSQL As SqlConnection
        Dim cmSQL As SqlCommand
        Dim strSQL As String

        cnSQL = New SqlConnection(ConnectionString)
        cnSQL.Open()

        strSQL = "Global.spUD_Dll_Code_ElamMarjoeeBaz "

        cmSQL = New SqlCommand(strSQL, cnSQL)
        cmSQL.CommandType = CommandType.StoredProcedure
        cmSQL.Parameters.Clear()

        cmSQL.Parameters.AddWithValue("ccMoshtary", ccMoshtary)
        cmSQL.Parameters.AddWithValue("CodeMahal", CodeMahalFaal)
        cmSQL.Parameters.AddWithValue("CodeDoreh", CodeDoreh)
        cmSQL.Parameters.AddWithValue("sVazeiatTaeedShodeh", Enums.FO_VaziatElamMarjoee.TaeedShodeh)
        cmSQL.Parameters.AddWithValue("sVazeiatTaeedNashodeh", Enums.FO_VaziatElamMarjoee.TaeedNashodeh)

        CountMarjoee = objTools.ConvertNulls(cmSQL.ExecuteScalar(), 0)

        cnSQL.Close()
        cmSQL = Nothing : cnSQL = Nothing

        Return CountMarjoee
    End Function
    Public Sub UpdateClearCCFaktorM(ByVal ccFaktorTitr As Long)
        Dim strSQL As String
        Dim cnSQL As SqlConnection
        Dim cmSQL As SqlCommand

        cnSQL = New SqlConnection(ConnectionString)
        cnSQL.Open()

        Dim ccElamMarjoee As Long
        ccElamMarjoee = objTools.ConvertNulls(objTools.DLookup("ccElamMarjoee", "tblFO_ElamMarjoee", "ccFaktorM = " & ccFaktorTitr), 0)

        strSQL = "Global.spUD_Dll_Code_UpdateClearCCFaktorM "

        cmSQL = New SqlCommand(strSQL, cnSQL)
        cmSQL.CommandType = CommandType.StoredProcedure
        cmSQL.Parameters.Clear()

        cmSQL.Parameters.AddWithValue("ccElamMarjoee", ccElamMarjoee)

        cmSQL.ExecuteNonQuery()

        cnSQL.Close()
        cmSQL = Nothing : cnSQL = Nothing
    End Sub
    Public Sub UpdateMablaghMarjoee(ByVal ccFaktorTitr As Long, ByVal ccMoshtary As Long)
        Dim cnSQL As SqlConnection
        Dim cmSQL As SqlCommand
        Dim strSQL As String
        cnSQL = New SqlConnection(ConnectionString)
        cnSQL.Open()
        Dim ds As New DataSet
        Try
            Dim MablaghMarjoee As Long = objTools.ConvertNulls(GetMKOLMarjoee(ccMoshtary, GetccElamMarjoee(ccMoshtary)), 0)

            strSQL = "Global.spUD_Dll_Code_UpdateMablaghMarjoee "

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            cmSQL.Parameters.AddWithValue("MablaghMarjoee", MablaghMarjoee)
            cmSQL.Parameters.AddWithValue("ccFaktorTitr", ccFaktorTitr)

            cmSQL.ExecuteNonQuery()

        Catch ex As Exception

        Finally
            cnSQL.Close()
            cmSQL = Nothing : cnSQL = Nothing
        End Try
    End Sub
    Public Sub UPDATEccFaktorMarjoee(ByVal ccFaktorM As Long, ByVal ccMoshtary As Long)
        Dim cnSQL As SqlConnection
        Dim cmSQL As SqlCommand
        Dim strSQL As String

        Dim ccElamMarjoee As Long = GetccElamMarjoee(ccMoshtary)
        If ccElamMarjoee = 0 Then Exit Sub
        Dim sNoeTasvieh As Integer = objTools.ConvertNulls(objTools.DLookup("sNoeTasvieh", "tblFO_ElamMarjoee", "ccElamMarjoee = " & ccElamMarjoee), 0)

        cnSQL = New SqlConnection(ConnectionString)
        cnSQL.Open()

        strSQL = "Global.spUD_Dll_Code_UpdateCCFaktorMarjoee "

        cmSQL = New SqlCommand(strSQL, cnSQL)
        cmSQL.CommandType = CommandType.StoredProcedure
        cmSQL.Parameters.Clear()

        cmSQL.Parameters.AddWithValue("ccFaktorM", ccFaktorM)
        cmSQL.Parameters.AddWithValue("ccElamMarjoee", ccElamMarjoee)

        If sNoeTasvieh = Enums.FO_NoeTasviehMarjoee.TasviehBaFaktor Then
            UpdateMablaghMarjoee(ccFaktorM, ccMoshtary)
        End If

        cmSQL.ExecuteNonQuery()

        cnSQL.Close()
        cmSQL = Nothing : cnSQL = Nothing
    End Sub
#End Region
#Region " Anbar "
    Public Function GetMablaghMarjoeeBeTaminKonnandeh(ByVal CodeMahal As Long, ByVal CodeDoreh As Long, ByVal ShomarehForm As Long) As Long
        GetMablaghMarjoeeBeTaminKonnandeh = objTools.ConvertNulls(objTools.DLookup("JamMablagh", "tblAN_kdxMarjoeeBeTaminKonandeh", "CodeMahal = " & CodeMahal & " AND CodeDoreh = " & CodeDoreh & " AND ShomarehForm = " & ShomarehForm), 0)
    End Function
    Public Function GetMablaghMianginMarjoeeBeTaminKonnandeh(ByVal CodeMahal As Long, ByVal CodeDoreh As Long, ByVal ShomarehForm As Long) As Long
        GetMablaghMianginMarjoeeBeTaminKonnandeh = objTools.ConvertNulls(objTools.DLookup("JamMablaghMiangin", "tblAN_kdxMarjoeeBeTaminKonandeh", "CodeMahal = " & CodeMahal & " AND CodeDoreh = " & CodeDoreh & " AND ShomarehForm = " & ShomarehForm), 0)
    End Function
    Public Function GetMablaghResidAzTaminKonandeh(ByVal ShomarehForm As Long) As Long
        GetMablaghResidAzTaminKonandeh = objTools.ConvertNulls(objTools.DLookup("JamMablagh", "tblAN_kdxResid", " ShomarehForm = " & ShomarehForm), 0)
    End Function
    Public Function GetMablaghEshantion(ByVal ShomarehForm As Long) As Long
        GetMablaghEshantion = objTools.ConvertNulls(objTools.DLookup("JamMablagh", "tblAN_Eshantion", " ShomarehEshantion = " & ShomarehForm), 0)
    End Function
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
    Public Function MojodiAnbarMavadAvalieh(ByVal ccAnbar As Integer, ByVal AzTarikh As String, ByVal TaTarikh As String, ByVal ccKala As Integer) As Double
        Dim cnSQL As SqlConnection
        Dim cmSQL As SqlCommand
        Dim strSQL As String

        cnSQL = New SqlConnection(ConnectionString)
        cnSQL.Open()
        Try
            strSQL = "Global.spUD_Dll_Code_MojodiAnbarMavadAvalieh "

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            cmSQL.Parameters.AddWithValue("ccAnbar", ccAnbar)
            cmSQL.Parameters.AddWithValue("AzTarikh", AzTarikh)
            cmSQL.Parameters.AddWithValue("TaTarikh", TaTarikh)
            cmSQL.Parameters.AddWithValue("ccKala", ccKala)

            MojodiAnbarMavadAvalieh = objTools.ConvertNulls(cmSQL.ExecuteScalar(), 0)

            cnSQL.Close() : cnSQL = Nothing
        Catch ex As Exception

        End Try
        Return MojodiAnbarMavadAvalieh
    End Function
    Public Function GetNoeGheimat(ByVal ccKala As Integer) As Integer
        Return objTools.ConvertNulls(objTools.DLookup("sNoeGheymat", "tblAN_Kala", " ccKala = " & ccKala), 0)
    End Function
    Public Function GetMablaghForosh(ByVal ccKala As Integer) As Double
        Return objTools.ConvertNulls(objTools.DLookup("MablaghForosh", "tblAN_Kala", "ccKala = " & ccKala), 0)
    End Function
    Public Function GetMablaghKharid(ByVal ccKala As Integer, ByVal Tarikh As String, ByVal ccMarkazPakhsh As Integer, ByVal ccTaminKonandeh As Integer) As Double
        Dim cnSQL As SqlConnection
        Dim cmSQL As SqlCommand
        Dim strSQL As String

        cnSQL = New SqlConnection(ConnectionString)
        cnSQL.Open()

        strSQL = "Global.spUD_Dll_Code_GetMablaghKharid "

        cmSQL = New SqlCommand(strSQL, cnSQL)
        cmSQL.CommandType = CommandType.StoredProcedure
        cmSQL.Parameters.Clear()

        cmSQL.Parameters.AddWithValue("ccKala", ccKala)
        cmSQL.Parameters.AddWithValue("Tarikh", Tarikh)
        cmSQL.Parameters.AddWithValue("ccMarkazPakhsh", ccMarkazPakhsh)
        cmSQL.Parameters.AddWithValue("ccTaminKonandeh", ccTaminKonandeh)

        GetMablaghKharid = objTools.ConvertNulls(cmSQL.ExecuteScalar(), 0)

        cnSQL.Close() : cnSQL = Nothing
        Return GetMablaghKharid
    End Function
    Public Function GetMablaghKharid_BeduneTaminKonandeh(ByVal ccKala As Integer, ByVal Tarikh As String) As Double
        Dim cnSQL As SqlConnection
        Dim cmSQL As SqlCommand
        Dim strSQL As String

        cnSQL = New SqlConnection(ConnectionString)
        cnSQL.Open()

        strSQL = "Global.spUD_Dll_Code_GetMablaghKharid_BeduneTaminKonandeh "

        cmSQL = New SqlCommand(strSQL, cnSQL)
        cmSQL.CommandType = CommandType.StoredProcedure
        cmSQL.Parameters.Clear()

        cmSQL.Parameters.AddWithValue("ccKala", ccKala)
        cmSQL.Parameters.AddWithValue("Tarikh", Tarikh)

        GetMablaghKharid_BeduneTaminKonandeh = objTools.ConvertNulls(cmSQL.ExecuteScalar(), 0)

        cnSQL.Close() : cnSQL = Nothing
        Return GetMablaghKharid_BeduneTaminKonandeh
    End Function
    Public Function GetMablaghMiangin(ByVal ccKala As Integer, ByVal ccAnbar As String) As Double
        Dim cnSQL As SqlConnection
        Dim cmSQL As SqlCommand
        Dim strSQL As String

        cnSQL = New SqlConnection(ConnectionString)
        cnSQL.Open()

        strSQL = "Global.spUD_Dll_Code_GetMablaghMiangin "

        cmSQL = New SqlCommand(strSQL, cnSQL)
        cmSQL.CommandType = CommandType.StoredProcedure
        cmSQL.Parameters.Clear()

        cmSQL.Parameters.AddWithValue("ccKala", ccKala)
        cmSQL.Parameters.AddWithValue("ccAnbar", ccAnbar)

        GetMablaghMiangin = objTools.ConvertNulls(cmSQL.ExecuteScalar(), 0)

        cnSQL.Close() : cnSQL = Nothing
    End Function
    Public Function GetMablaghMiangin_Tarikh(ByVal ccKala As Integer, ByVal ccAnbar As String, ByVal Tarikh As String) As Double
        Dim cnSQL As SqlConnection
        Dim cmSQL As SqlCommand
        Dim strSQL As String

        cnSQL = New SqlConnection(ConnectionString)
        cnSQL.Open()

        strSQL = "Global.spUD_Dll_Code_GetMablaghMiangin_Tarikh "

        cmSQL = New SqlCommand(strSQL, cnSQL)
        cmSQL.CommandType = CommandType.StoredProcedure
        cmSQL.Parameters.Clear()

        cmSQL.Parameters.AddWithValue("ccKala", ccKala)
        cmSQL.Parameters.AddWithValue("ccAnbar", ccAnbar)
        cmSQL.Parameters.AddWithValue("Tarikh", Tarikh)

        GetMablaghMiangin_Tarikh = objTools.ConvertNulls(cmSQL.ExecuteScalar(), 0)

        cnSQL.Close() : cnSQL = Nothing
    End Function
    Public Function ReturnTedad(ByVal ccKala As Integer, ByVal _Tedad As Double, ByVal _TedadBasteh As Double, _
                                    ByVal _TedadKarton As Double) As Double
        'Dim IsFastFood As Boolean = objTools.ConvertNulls(objTools.DLookup("IsFastFood", "dbo.tblGL_SysConfig", "CodeMahal  = " & CodeMahalFaal), False)
        Dim IsFastFood As Boolean = objTools.ConvertNulls(objTools.DLookup("IsFastFood", "dbo.tblAN_Kala", "ccKala  = " & ccKala), False)
        If Not IsFastFood = True Then

            Dim TedadBasteh As Double = objTools.ConvertNulls(objTools.DLookup("TedadDarBasteh", "tblAN_Kala", "ccKala = " & ccKala), 0)
            Dim TedadKarton As Double = objTools.ConvertNulls(objTools.DLookup("TedadDarKarton", "tblAN_Kala", "ccKala = " & ccKala), 0)
            ReturnTedad = _Tedad + (TedadBasteh * _TedadBasteh) + (TedadKarton * _TedadKarton)
        Else
            Dim VaznKhales As Double = objTools.ConvertNulls(objTools.DLookup("VaznKhales", "tblAN_Kala", "ccKala = " & ccKala), 0)
            ReturnTedad = _Tedad * VaznKhales

        End If

    End Function
#End Region
#Region " Khazaneh "
    Public Function ChkSanad(ByVal ccAmalyat As Long, ByVal noe As Byte) As Boolean
        ChkSanad = True

        Select Case noe
            Case 1 ' naghd
                If objTools.ConvertNulls(objTools.DLookup("CodeAmalyat", "qryDP_ShomarehSanadNaghdy", "CodeAmalyat = " & ccAmalyat), 0) = 0 Then
                    ChkSanad = False
                End If
            Case 2 ' Check
                If objTools.ConvertNulls(objTools.DLookup("CodeAmalyat", "qryDP_ShomarehSanadCheck", "CodeAmalyat = " & ccAmalyat), 0) = 0 Then
                    ChkSanad = False
                End If
        End Select

        Return ChkSanad
    End Function
    Public Function GetMojodiNaghdiSandogh(ByVal ShHesab As Integer) As Double
        Dim cnSQL As SqlConnection
        Dim cmSQL As SqlCommand
        Dim strSQL As String

        cnSQL = New SqlConnection(ConnectionString)
        cnSQL.Open()
        Try
            strSQL = "Select "
            strSQL &= "(isnull(Daryaft,0)+isnull(FeeAvalDoreh,0)-isnull(Pardakht,0)) as MojodiNaghdi"
            strSQL &= " From [dbo].[fnDP_TarazSandoghNaghdy]"
            strSQL &= "('" & Str(CodeDorehAval).Trim & "0101'"
            strSQL &= ",'" & TarikhEmrooz & "') where ccShomarehHesab =" & ShHesab

            cmSQL = New SqlCommand(strSQL, cnSQL)
            GetMojodiNaghdiSandogh = objTools.ConvertNulls(cmSQL.ExecuteScalar(), 0)

            cnSQL.Close() : cnSQL = Nothing
        Catch ex As Exception

        End Try

        Return GetMojodiNaghdiSandogh
    End Function
    Public Function GetMojodiHesab(ByVal ShHesab As String) As Double
        Dim cnSQL As SqlConnection
        Dim cmSQL As SqlCommand
        Dim strSQL As String

        cnSQL = New SqlConnection(ConnectionString)
        cnSQL.Open()
        Try
            Dim dt As DateTime = objTarikh.SetDateSlash(objTarikh.Sh2Mi(TarikhEmrooz))
            Dim TarikhRoozGhabl As String = objTarikh.Mi2Sh(dt.AddDays(-1))

            strSQL = "Select (MandehAzGhabl+Daryaft-Pardakht) as Mojodi From "
            strSQL &= "[fnDP_TarazBankMain]('" & TarikhEmrooz & "','" & TarikhEmrooz & "',"
            strSQL &= "'" & Str(CodeDoreh).Trim & "0101','" & TarikhRoozGhabl & "')"
            strSQL &= " Where ShomarehHesab = ('" & ShHesab & "')"

            cmSQL = New SqlCommand(strSQL, cnSQL)
            GetMojodiHesab = objTools.ConvertNulls(cmSQL.ExecuteScalar(), 0)

            cnSQL.Close() : cnSQL = Nothing
        Catch ex As Exception

        End Try

        Return GetMojodiHesab
    End Function
    Public Function GetPardakhtFaktor(ByVal CodeMahal As Long, ByVal CodeDoreh As Long, ByVal FaktorShomareh As Long) As Long
        GetPardakhtFaktor = objTools.ConvertNulls(objTools.DSum("Mablagh", "qryDP_AmalyatFaktor", "CodeMahal = " & CodeMahal & " AND CodeDoreh = " & CodeDoreh & " AND FaktorShomareh = " & FaktorShomareh), 0)
    End Function
    Public Function GetMandehFaktor(ByVal CodeMahal As Long, ByVal CodeDoreh As Long, ByVal FaktorShomareh As Long) As Long
        GetMandehFaktor = GetMablaghFaktor(CodeMahal, CodeDoreh, FaktorShomareh) - GetPardakhtFaktor(CodeMahal, CodeDoreh, FaktorShomareh)
    End Function
    Public Function GetTafsilyShomarehHesab(ByVal ccShomarehHesab As Integer) As Integer
        GetTafsilyShomarehHesab = objTools.DLookup("CodeTafsily2", "tblDP_ShomarehHesab", "ccShomarehHesab = " & ccShomarehHesab)
        Return GetTafsilyShomarehHesab
    End Function
    Public Function GetTafsilySandoghDar(ByVal ccShomarehHesab As Integer) As Integer
        Return objTools.ConvertNulls(objTools.DLookup("CodeTafsilySandoghDar", "qryDP_ShomarehHesab", "ccShomarehHesab = " & ccShomarehHesab), 0)
    End Function
    Public Function IsSandoghTankhah(ByVal ccShomarehHesab As Integer) As Boolean
        Return objTools.DLookup("IsTankhah", "tblDP_ShomarehHesab", "ccShomarehHesab=" & ccShomarehHesab)
    End Function
    Public Function GetBL_DPVazeiat(ByVal CodeAmalyat As Long) As Long

        Dim cnSQL As SqlConnection
        Dim cmSQL As SqlCommand
        Dim strSQL As String

        cnSQL = New SqlConnection(ConnectionString)
        cnSQL.Open()

        strSQL = "select ccShomarehHesab from qryDP_AmalyatVazeiat where ccVazeiat=("
        strSQL &= "SELECT Top 1 ccVazeiat From qryDP_AmalyatVazeiat where Ebtal <> 1 "
        strSQL &= "AND (sNoeSanad =" & Enums.DP_AnvaeSanadDP.D_CheckBeSandogh & ")  "
        'strSQL &= " and ccVazeiat < (SELECT Top 1 ccVazeiat FROM qryDP_AmalyatVazeiat A  "
        'strSQL &= "WHERE(A.CodeAmalyat = qryDP_AmalyatVazeiat.CodeAmalyat) ORDER BY ccVazeiat DESC)"
        strSQL &= "and CodeAmalyat =" & CodeAmalyat & " ORDER BY ccVazeiat DESC)"

        cmSQL = New SqlCommand(strSQL, cnSQL)
        GetBL_DPVazeiat = objTools.ConvertNulls(cmSQL.ExecuteScalar(), 0)

        cnSQL.Close() : cnSQL = Nothing

        Return GetBL_DPVazeiat
    End Function
    Public Function GetModatCheckPardakht(ByVal ccVazeiat As Long, ByVal NoeCheck As Byte)
        GetModatCheckPardakht = 0

        ' Noe Entekhabi Check 
        Select Case NoeCheck
            Case 0 ' auto select
                Dim MTarikhDP As Date = objTarikh.SetDateSlash(objTarikh.Sh2Mi(objTools.ConvertNulls(objTools.DLookup("TarikhDP", "qryDP_AmalyatVazeiat", "ccVazeiat = " & ccVazeiat), 0)))
                Dim MTarikhSanad As Date = objTarikh.SetDateSlash(objTarikh.Sh2Mi(objTools.ConvertNulls(objTools.DLookup("TarikhSanad", "qryDP_AmalyatVazeiat", "ccVazeiat = " & ccVazeiat), 0)))


                ' age 2 sal bashe moshkel

                Dim Modat As Integer = DateDiff(DateInterval.Day, MTarikhDP, MTarikhSanad, FirstDayOfWeek.Sunday, FirstWeekOfYear.System)

                If Modat <= objTools.ConvertNulls(objTools.DLookup("ModateCheckRooz", "tblGL_Sherkat", "CodeSherkat = " & CodeSherkat), 0) Then
                    ' Check Roooz
                    GetModatCheckPardakht = 1
                ElseIf Modat <= objTools.ConvertNulls(objTools.DLookup("ModateCheckKotahModate", "tblGL_Sherkat", "CodeSherkat = " & CodeSherkat), 0) Then
                    ' Kotah Modat
                    GetModatCheckPardakht = 2
                Else
                    ' Boland Modat
                    GetModatCheckPardakht = 3
                End If
            Case 1 ' rooz
                GetModatCheckPardakht = 1
            Case 2 ' modat
                GetModatCheckPardakht = 2
            Case 3 ' modat
                GetModatCheckPardakht = 3
        End Select

        Return GetModatCheckPardakht
    End Function
    'Public Function GetModatCheckRoozPardakhty(ByVal ccVazeiat As Long)
    '    Dim MTarikhDP As Date = objTarikh.SetDateSlash(objTarikh.Sh2Mi(objTools.ConvertNulls(objTools.DLookup("TarikhDP", "qryDP_AmalyatVazeiat", "ccVazeiat = " & ccVazeiat), 0)))
    '    Dim MTarikhSanad As Date = objTarikh.SetDateSlash(objTarikh.Sh2Mi(objTools.ConvertNulls(objTools.DLookup("TarikhSanad", "qryDP_AmalyatVazeiat", "ccVazeiat = " & ccVazeiat), 0)))

    '    Dim Modat As Integer = MTarikhSanad.DayOfYear - MTarikhDP.DayOfYear
    '    Modat = Date.Compare(MTarikhSanad, MTarikhDP)

    '    If Modat = objTools.ConvertNulls(objTools.DLookup("ModateCheckRooz", "tblGL_Sherkat", "CodeSherkat = " & CodeSherkat), 0) Then
    '        Return 5 ' CheckRooz
    '    Else
    '        Return 5 ' CheckRooz
    '    End If
    'End Function
    Public Function GetNoeCheckPardakht(ByVal ccVazeiat As Integer, ByVal T1 As Integer) As Byte
        ' This Function return Noe Check Pardakht ---> Tejary Or Gheyre Tejary
        Dim Noe As Byte = objTools.DLookup("NoeModatCheck", "tblDP_AmalyatVazeiat", "ccVazeiat = " & ccVazeiat)

        Select Case GetModatCheckPardakht(ccVazeiat, Noe)
            Case 1

                Return 5
            Case 2
                If GetNoeTafsilyTejariAndGheyr(T1) Then
                    Return 1
                Else
                    Return 2
                End If
            Case 3
                If GetNoeTafsilyTejariAndGheyr(T1) Then
                    Return 3
                Else
                    Return 4
                End If
        End Select
    End Function
    Public Function GetCodeHesabVasetBank() As String
        Return objTools.DLookup("Moeen48", "tblGL_Sherkat", "CodeSherkat=" & CodeSherkat)
    End Function
    Public Function GetCodeHesabVasetSandogh() As String
        Return objTools.DLookup("Moeen49", "tblGL_Sherkat", "CodeSherkat=" & CodeSherkat)
    End Function
    Public Function GetCodeHesabFaktorMoshtary(ByVal ccFaktor As Integer) As String
        Dim strCoding As String = String.Empty

        Dim noeTafsily As Integer = GetNoeTafsilyFaktor(ccFaktor)
        Select Case noeTafsily
            Case 3
                strCoding = objTools.ConvertNulls(objTools.DLookup("Moeen46", "tblGL_Sherkat", "CodeSherkat = " & CodeSherkat), 0)
            Case 5
                strCoding = objTools.ConvertNulls(objTools.DLookup("Moeen13", "tblGL_Sherkat", "CodeSherkat = " & CodeSherkat), 0)
            Case 6
                strCoding = objTools.ConvertNulls(objTools.DLookup("Moeen12", "tblGL_Sherkat", "CodeSherkat = " & CodeSherkat), 0)
            Case 4
                strCoding = objTools.ConvertNulls(objTools.DLookup("Moeen51", "tblGL_Sherkat", "CodeSherkat = " & CodeSherkat), 0)
            Case 7
                strCoding = objTools.ConvertNulls(objTools.DLookup("Moeen50", "tblGL_Sherkat", "CodeSherkat = " & CodeSherkat), 0)
        End Select

        Return strCoding
    End Function
#End Region
#Region " Hesabdary "
    Public Function GetNoeTafsilyTejariAndGheyr(ByVal Tafsily As Long) As Boolean
        Dim cnSQL As SqlConnection
        Dim cmSQL As SqlCommand
        Dim strSQL As String

        cnSQL = New SqlConnection(ConnectionString)
        cnSQL.Open()

        strSQL = "Global.spUD_Dll_Code_GetNoeTafsilyTejariAndGheyr "

        cmSQL = New SqlCommand(strSQL, cnSQL)
        cmSQL.CommandType = CommandType.StoredProcedure
        cmSQL.Parameters.Clear()

        cmSQL.Parameters.AddWithValue("CodeTafsily", Tafsily)

        Select Case objTools.ConvertNulls(cmSQL.ExecuteScalar(), 0)
            Case 5, 6, 1, 2, 0
                Return True
            Case Else
                Return False
        End Select

        cnSQL.Close() : cnSQL = Nothing
    End Function
    Public Function GetNoeTafsilyFaktor(ByVal ccFaktor As Long)
        Dim cnSQL As SqlConnection
        Dim cmSQL As SqlCommand
        Dim strSQL As String

        cnSQL = New SqlConnection(ConnectionString)
        cnSQL.Open()

        strSQL = "Global.spUD_Dll_Code_GetNoeTafsilyFaktor "

        cmSQL = New SqlCommand(strSQL, cnSQL)
        cmSQL.CommandType = CommandType.StoredProcedure
        cmSQL.Parameters.Clear()

        cmSQL.Parameters.AddWithValue("ccFaktortitr", ccFaktor)

        GetNoeTafsilyFaktor = objTools.ConvertNulls(cmSQL.ExecuteScalar(), 0)

        cnSQL.Close() : cnSQL = Nothing

        Return GetNoeTafsilyFaktor
    End Function
    Public Function GetNoeTafsilyTaminKonandeh(ByVal ccTaminKonandeh As Long)
        Dim cnSQL As SqlConnection
        Dim cmSQL As SqlCommand
        Dim strSQL As String

        cnSQL = New SqlConnection(ConnectionString)
        cnSQL.Open()

        strSQL = "Global.spUD_Dll_Code_GetNoeTafsilyTaminKonandeh "

        cmSQL = New SqlCommand(strSQL, cnSQL)
        cmSQL.CommandType = CommandType.StoredProcedure
        cmSQL.Parameters.Clear()

        cmSQL.Parameters.AddWithValue("ccTaminKonandeh", ccTaminKonandeh)

        GetNoeTafsilyTaminKonandeh = objTools.ConvertNulls(cmSQL.ExecuteScalar(), 0)

        cnSQL.Close() : cnSQL = Nothing

        Return GetNoeTafsilyTaminKonandeh
    End Function
    Public Function GetNoeTafsilyElamMarjoee(ByVal CodeTafsily As Long)
        Dim cnSQL As SqlConnection
        Dim cmSQL As SqlCommand
        Dim strSQL As String

        cnSQL = New SqlConnection(ConnectionString)
        cnSQL.Open()

        strSQL = "Global.spUD_Dll_Code_GetNoeTafsilyElamMarjoee "

        cmSQL = New SqlCommand(strSQL, cnSQL)
        cmSQL.CommandType = CommandType.StoredProcedure
        cmSQL.Parameters.Clear()

        cmSQL.Parameters.AddWithValue("CodeTafsily", CodeTafsily)

        GetNoeTafsilyElamMarjoee = objTools.ConvertNulls(cmSQL.ExecuteScalar(), 0)

        cnSQL.Close() : cnSQL = Nothing

        Return GetNoeTafsilyElamMarjoee
    End Function
    Public Function GetTafsilyBarnameh() As String
        Dim NumTafsily = objTools.ConvertNulls(objTools.DLookup("TafsilyMoeen44", "tblGL_Sherkat", "CodeSherkat = " & CodeSherkat), 0)
        Return objTools.ConvertNulls(objTools.DLookup("CodeTafsily", "tblHE_CodeTafsily", "NumTafsily = " & NumTafsily), 0)
    End Function
    Public Function GetTafsilyEshantion() As String
        Dim NumTafsily = objTools.DLookup("TafsilyMoeen41", "tblGL_Sherkat", "CodeSherkat = " & CodeSherkat)
        Return objTools.DLookup("CodeTafsily", "tblHE_CodeTafsily", "NumTafsily = " & NumTafsily)
    End Function
    Public Function CodeSanadEftetahie() As Long
        Return objTools.ConvertNulls(objTools.DLookup("CodeTitrSanad", "tblHE_SanadHesabdaryTitr", "sNoeSanad = " & Enums.HE_AnvaeSanadMaly.SanadEftetahieh & " AND CodeMahal = " & CodeMahalFaal), 0)
    End Function
    Public Function GetTafsilyPersonel(ByVal ccFard As Long)
        'Dim cnSQL As SqlConnection
        'Dim cmSQL As SqlCommand
        'Dim strSQL As String

        'cnSQL = New SqlConnection(ConnectionString)
        'cnSQL.Open()

        'strSQL = " select CodeTafsily from qryGL_MoshakhasatFardi where CodeFard = " & ccFard

        'cmSQL = New SqlCommand(strSQL, cnSQL)
        'GetTafsilyPersonel = objTools.ConvertNulls(cmSQL.ExecuteScalar(), 0)

        'cnSQL.Close() : cnSQL = Nothing

        'Return GetTafsilyPersonel
        Return objTools.ConvertNulls(objTools.DLookup("CodeTafsily", "qryGL_MoshakhasatFardi", "CodeFard = " & ccFard), 0)
    End Function
    'Public Function GetSoodVaZian() As Double
    '    Dim cnSQL As SqlConnection
    '    Dim strSQL As String

    '    cnSQL = New SqlConnection(ConnectionString)
    '    cnSQL.Open()

    '    strSQL = " SELECT Daramad - FeeTamamShodeh - Hazineh from "
    '    strSQL &= " ("
    '    strSQL &= " select "
    '    strSQL &= " [6] as Daramad ,"
    '    strSQL &= " [7] as FeeTamamShodeh ,"
    '    strSQL &= " [8] as Hazineh"
    '    strSQL &= " from "
    '    strSQL &= " (select Sum(Mandeh) as Mandeh, CodeGoroh  from dbo.[fnHE_SoodoZianNameh]('13860101','13861218')"
    '    strSQL &= " group by CodeGoroh ) as p"
    '    strSQL &= " PIVOT  (SUM (Mandeh) FOR CodeGoroh  IN ([6], [7], [8])) AS chld ) tbl"

    '    Dim ds As New DataSet
    '    Dim daSQL As SqlDataAdapter
    '    daSQL = New SqlDataAdapter(strSQL, ConnectionString)
    '    daSQL.Fill(ds, "tblKalaBedoneTakhfif")

    '    GetSoodVaZian = ds.Tables("tblKalaBedoneTakhfif").Rows(0)(0)

    '    cnSQL.Close() : cnSQL = Nothing
    '    Return GetSoodVaZian
    'End Function
#End Region
End Class
