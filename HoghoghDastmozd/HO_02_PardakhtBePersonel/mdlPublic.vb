Imports System.Data.SqlClient
Module mdlPublic

    '---Global----------------------------------------------------
    Public objTools As New UD_Dll.mdlUtility
    Public objTarikh As New UD_Dll.Tarikh
    Public objSec As New UD_Dll.Security
    Public objCode As New UD_Dll.Code

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
    Public EmSal As Integer = objTarikh.ShamsiYear(TarikhEmrooz)
    Public appPath As String = Application.StartupPath
    Public CodeSherkat As Long = 1
    Public rptPath As String = objTools.ConvertNulls(objTools.DLookup("ReportPath", "tblGL_Sherkat", "CodeSherkat = " & CodeSherkat), "")
    Public imgPath As String = objTools.ConvertNulls(objTools.DLookup("ImagePath", "tblGL_Sherkat", "CodeSherkat = " & CodeSherkat), "")
    Public NameSherkat As String = objTools.ConvertNulls(objTools.DLookup("NameSherkat", "tblGL_Sherkat", "CodeSherkat = " & CodeSherkat), "")
    Public ccAmalyat As Long
    Public ccPardakhtBePersonel As Long
    Public MablaghAmalyat As Long
    Public ccMoshtary As Long
    Public NameMoshtary As String
    Public Taraf_afrad As Long = 1 ' 1 Moshtary , 2 TaminKonandeh


    Function GetTarikhMilady(ByVal DateShamsi As String) As String
        Dim cm As SqlCommand = New SqlCommand
        Dim cn As SqlConnection = New SqlConnection
        Dim p As SqlParameter = Nothing
        Dim strSQL As String = String.Empty
        Try

            strSQL = "Select [dbo].[ufnConvertToMilady](@DateShamsi)"

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

End Module
