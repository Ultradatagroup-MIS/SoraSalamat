Imports System.Data.SqlClient
Imports System.Data.SqlClient.SqlConnection
Imports System.Data.Common
Imports System.Data
Public Class TakhfifatForosh
    Private Sub TakhfifatForosh_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            SetReport()
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->frmFO_ReportForoshKala_Load")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->frmFO_ReportForoshKala_Load")
        End Try
    End Sub
    Private Sub SetReport()
        Try
            Dim strsql As String
            Dim cnsql As SqlConnection
            Dim cmsql As SqlCommand

            cnsql = New SqlConnection(ConnectionString)
            cnsql.Open()

            strsql = "Report.spGozareshMoghayerat_Forosh"

            cmsql = New SqlCommand(strsql, cnsql)
            cmsql.CommandType = CommandType.StoredProcedure
            cmsql.Parameters.Clear()

            cmsql.Parameters.AddWithValue("AzTarikh", KhazanehForoshHesabdari.mskAzTarikh.Text)
            cmsql.Parameters.AddWithValue("TaTarikh", KhazanehForoshHesabdari.mskTaTarikh.Text)

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->frmFO_ReportForoshKala_Load")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->frmFO_ReportForoshKala_Load")
        End Try
    End Sub
End Class