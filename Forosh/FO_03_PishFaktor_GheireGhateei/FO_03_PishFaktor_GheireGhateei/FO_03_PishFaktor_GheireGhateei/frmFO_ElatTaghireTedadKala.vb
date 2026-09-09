Public Class frmFO_ElatTaghireTedadKala
#Region "Variable AND Constant Declration"
    Dim dsForm As New DataSet
    Private WithEvents BS As New UD_Dll.PassString
    Public IsKoli As Boolean
    Public frm_ccKala, frm_ccTafkikSatr_GG As Integer
    Public Flg As Boolean = False
#End Region
    Private Sub frmFO_ElatTaghireTedadKala_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadCombo()

        'cmbElatTaghirTedadKala.SelectedValue = 0
        'cmbElatTaghirTedadKala.SelectedValue = 0
    End Sub
    Private Sub LoadCombo()

        Dim Strsql As String
        Dim cnSQL As New SqlConnection
        Dim cmSQL As New SqlCommand
        Dim daSQL As SqlDataAdapter

        cnSQL = New SqlConnection(ConnectionString)
        cnSQL.Open()

        '----- Load Combo Noe Pardakht
        Strsql = "Sales.spPishFaktorGheireGhateei_LoadComboElatTaghireTedadKala "

        cmSQL = New SqlCommand(Strsql, cnSQL)
        cmSQL.CommandType = CommandType.StoredProcedure
        cmSQL.Parameters.Clear()

        daSQL = New SqlDataAdapter(cmSQL)
        daSQL.Fill(dsForm, "tblElat")

        'Dim dr As DataRow = dsForm.Tables("tblElat").NewRow
        'dr("sElat") = 0
        'dr("txtElatTaghireTedad") = " -------"
        'dsForm.Tables("tblElat").Rows.Add(dr)

        cmbElatTaghirTedadKala.DataSource = Nothing
        cmbElatTaghirTedadKala.Items.Clear()
        cmbElatTaghirTedadKala.DataSource = dsForm.Tables("tblElat").DefaultView
        cmbElatTaghirTedadKala.DisplayMember = "txtElatTaghireTedad"
        cmbElatTaghirTedadKala.ValueMember = "sElat"

        cmSQL = Nothing : daSQL = Nothing
        cnSQL.Close()

    End Sub
    Private Sub btnSabt_Click(sender As Object, e As EventArgs) Handles btnSabt.Click
        If IsKoli = False Then
            objTools.DUpdate("sElatTaghirTedad", "Sales.TafkikJozeSatr_PishFaktor_Kala", cmbElatTaghirTedadKala.SelectedValue, "ccTafkikSatr_GG = " & frm_ccTafkikSatr_GG & " AND ccKala = " & frm_ccKala)
        Else
            objTools.DUpdate("sElatTaghirTedad", "Sales.TafkikJozeSatr_PishFaktor_Kala", cmbElatTaghirTedadKala.SelectedValue, "ccTafkikSatr_GG = " & frm_ccTafkikSatr_GG)
            Flg = True
        End If
        Me.Close()
    End Sub
    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub
End Class