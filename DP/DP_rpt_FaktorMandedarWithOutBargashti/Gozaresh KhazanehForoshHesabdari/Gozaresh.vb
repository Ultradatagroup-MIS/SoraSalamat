Imports System.Data.SqlClient
Imports System.Data.SqlClient.SqlConnection
Imports System.Data.Common
Imports System.Data
Public Class Gozaresh
#Region "Variable AND Constant Declration"
    Dim cntCodeSubSystem As Long = 100102
    Dim dvTitr_Sandogh As DataView
    Dim dvTitr_Bank As DataView
    Dim ErrPro As New ErrorProvider
    Dim dsForm As New DataSet()
    Dim cmForm As CurrencyManager
    Dim dvForm As DataView
    Private SN As Integer
    Dim txtCaption As String
    Private WithEvents BS As New UD_Dll.PassString
    Dim flag As Boolean = False
#End Region
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

            strsql = "Sales.spFaktorMandedarBargashti"

            RefreshFormData(strsql)

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->frmFO_ReportForoshKala_Load")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->frmFO_ReportForoshKala_Load")
        End Try
    End Sub
    Private Sub RefreshFormData(ByVal strSql As String)

        Dim cnsql As SqlConnection
        Dim cmsql As SqlCommand
        Dim dasql As SqlDataAdapter
        Try

            cnsql = New SqlConnection(ConnectionString)
            cnsql.Open()

            cmsql = New SqlCommand(strSql, cnsql)
            cmsql.CommandType = CommandType.StoredProcedure
            cmsql.Parameters.Clear()

            cmsql.Parameters.AddWithValue("AzTarikh", FaktorMandedarBedoneBargashti.mskAzTarikh.Text)
            cmsql.Parameters.AddWithValue("TaTarikh", FaktorMandedarBedoneBargashti.mskTaTarikh.Text)
            cmsql.Parameters.AddWithValue("CodeMahal", CodeMahalFaal)


            If dsForm.Tables.Contains("tbl_Sandogh") Then
                dsForm.Tables.Remove("tbl_Sandogh")
            End If

            dasql = New SqlDataAdapter(cmsql)
            dasql.Fill(dsForm, "tbl_Sandogh")

            dvForm = New DataView
            dvForm = dsForm.Tables("tbl_Sandogh").DefaultView
            dvForm.Sort = "CodeAmalyat ASC"
            dvForm.AllowDelete = True
            dvForm.AllowEdit = False
            dvForm.AllowNew = False
            dasql = Nothing

            dvTitr_Sandogh = New DataView(dsForm.Tables("tbl_Sandogh"), "", "CodeAmalyat ASC", DataViewRowState.CurrentRows)
            dvTitr_Sandogh.AllowNew = False
            dvTitr_Sandogh.AllowDelete = False
            dvTitr_Sandogh.AllowEdit = False

            GridEx.DataSource = Nothing
            GridEx.DataSource = dvTitr_Sandogh

            If dvTitr_Sandogh.Count <> 0 Then
                SetGridStyle_Sandogh()
            End If


        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->RefreshTitrdata")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->RefreshTitrdata")
        Finally
            cmsql = Nothing : dasql = Nothing
            cnsql.Close()
        End Try

    End Sub
End Class