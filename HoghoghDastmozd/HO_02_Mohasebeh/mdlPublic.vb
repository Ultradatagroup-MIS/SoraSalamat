Module mdlPublic
    '---Global----------------------------------------------------

    Public objTools As New ud_dll.mdlUtility
    Public objTarikh As New UD_Dll.Tarikh
    Public objSec As New UD_Dll.Security
    Public objSearch As New UD_Dll.Search
    Public ObjCode As New UD_Dll.Code

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
    Public TarikhEmrooz As String = objTarikh.Mi2Sh(Today)
    Public rptPath As String = objTools.ConvertNulls(objTools.DLookup("ReportPath", "tblGL_Sherkat", "CodeSherkat =1"), "")
    Public NameSherkat As String = objTools.ConvertNulls(objTools.DLookup("NameSherkat", "tblGL_Sherkat", "CodeSherkat = 1"), "")

    Function GetTedadRoozMah(ByVal Sal As Integer, ByVal Mah As Byte) As Byte
        If Mah < 7 Then
            Return 31
        ElseIf Mah < 12 Then
            Return 30
        ElseIf Sal Mod 4 = 3 Then
            Return 30
        Else
            Return 29
        End If
    End Function
    Function GetTedadRoozAzAvalSalTaMah(ByVal Sal As Integer, ByVal Mah As Byte) As Integer
        If Mah < 7 Then
            Return Mah * 31
        ElseIf Mah < 12 Then
            Return (6 * 31) + ((Mah - 6) * 30)
        ElseIf Sal Mod 4 = 3 Then
            Return 366
        Else
            Return 365
        End If
    End Function
    Function GetLastccPersonelHokmKargozinyPardakhtHoghogh(ByVal ccAfrad As Integer, ByVal Sal As String, ByVal Mah As String, _
              ByVal cm As Data.SqlClient.SqlCommand) As Long
        Dim p As SqlParameter = Nothing
        Dim strSQL As String = String.Empty
        Try
            strSQL &= " SELECT isnull(a.ccPersonelHokmKargoziny,0)"
            strSQL &= " FROM Payroll.MohasebehHoghogh a  "
            strSQL &= " where a.ccAfrad = @ccAfrad And a.SalMah < @SalMah And CodeNoeMohasebeh = 1"
            strSQL &= " Order by a.SalMah Desc "

            cm.Parameters.Clear()
            p = New SqlParameter("SalMah", SqlDbType.NChar, 6)
            p.Value = Sal & Mah '.ToString.PadLeft(2, "0")
            cm.Parameters.Add(p)

            p = New SqlParameter("ccAfrad", SqlDbType.Int)
            p.Value = ccAfrad
            cm.Parameters.Add(p)

            cm.CommandText = strSQL
            GetLastccPersonelHokmKargozinyPardakhtHoghogh = cm.ExecuteScalar()

        Catch ex As SqlException
            MsgBox(ex.Message)
        Finally
        End Try
    End Function 'GetLastccPersonelHokmKargozinyPardakhtHoghogh
    Public Function GetSQLCommandForTransaction() As SqlCommand
        Dim cm As SqlCommand = New SqlCommand

        cm.Connection = New SqlConnection
        cm.Connection.ConnectionString = ConnectionString
        cm.Connection.Open()
        cm.Transaction = cm.Connection.BeginTransaction

        Return cm
    End Function
    Function GetTarikhMilady(ByVal DateShamsi As String) As String
        Dim cm As SqlCommand = New SqlCommand
        Dim cn As SqlConnection = New SqlConnection
        Dim p As SqlParameter = Nothing
        Dim strSQL As String = String.Empty
        Try

            strSQL = "Select [dbo].[fnGL_ConvertToMiladi](@DateShamsi)"

            p = New SqlParameter("DateShamsi", SqlDbType.NVarChar, 50)
            p.Value = DateShamsi
            cm.Parameters.Add(p)

            cn.ConnectionString = ConnectionString
            cn.Open()
            cm.CommandText = strSQL
            cm.Connection = cn


        Catch ex As SqlException
            MsgBox(ex.Message)
        Finally
            GetTarikhMilady = cm.ExecuteScalar

            cn.Close()
            cn.Dispose()
            cm.Dispose()
        End Try
    End Function 'GetTarikhMilady
    Function GetTarikhShamsi(ByVal DateMiladi As Date) As String
        Dim cm As SqlCommand = New SqlCommand
        Dim cn As SqlConnection = New SqlConnection
        Dim p As SqlParameter = Nothing
        Dim strSQL As String = String.Empty
        Try

            strSQL = "Select [dbo].[fnGL_ConvertToShamsiWithoutSlash](@DateShamsi)"

            p = New SqlParameter("DateShamsi", SqlDbType.DateTime)
            p.Value = DateMiladi
            cm.Parameters.Add(p)

            cn.ConnectionString = ConnectionString
            cn.Open()
            cm.CommandText = strSQL
            cm.Connection = cn


        Catch ex As SqlException
            MsgBox(ex.Message)
        Finally
            GetTarikhShamsi = cm.ExecuteScalar

            cn.Close()
            cn.Dispose()
            cm.Dispose()
        End Try
    End Function 'GetTarikhMilady



End Module
