Public Class frmFO_NoePardakht
#Region "Variable AND Constant Declration"
    Dim dsForm As New DataSet
    Private WithEvents BS As New UD_Dll.PassString
    Public frm_ccPishFaktor As Integer
    Dim MeghdarAdadi As Integer
    Dim Flg As Boolean = False
#End Region
    Private Sub frmFO_NoePardakht_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadCombo()
        Flg = True
    End Sub
    Private Sub LoadCombo()

        Dim Strsql As String
        Dim cnSQL As New SqlConnection
        Dim cmSQL As New SqlCommand
        Dim daSQL As SqlDataAdapter

        cnSQL = New SqlConnection(ConnectionString)
        cnSQL.Open()

        '----- Load Combo Noe Pardakht
        Strsql = "Sales.spPishFaktorGheireGhateei_LoadMenuNoePardakht "

        cmSQL = New SqlCommand(Strsql, cnSQL)
        cmSQL.CommandType = CommandType.StoredProcedure
        cmSQL.Parameters.Clear()

        cmSQL.Parameters.AddWithValue("ccPishFaktorTitr", frm_ccPishFaktor)

        daSQL = New SqlDataAdapter(cmSQL)
        daSQL.Fill(dsForm, "tblNoePardakht")

        cmbNoePardakht.DataSource = Nothing
        cmbNoePardakht.Items.Clear()
        cmbNoePardakht.DataSource = dsForm.Tables("tblNoePardakht").DefaultView
        cmbNoePardakht.DisplayMember = "txtNoePardakht"
        cmbNoePardakht.ValueMember = "sNoePardakht"

        cmSQL = Nothing : daSQL = Nothing
        cnSQL.Close()

    End Sub
    Private Sub btnSabt_Click(sender As Object, e As EventArgs) Handles btnSabt.Click
        objTools.DUpdate("sNoePardakht", "tblFO_PishFaktor", cmbNoePardakht.SelectedValue, "ccPishFaktorTitr = " & frm_ccPishFaktor)
        objTools.DUpdate("ModatCheck", "tblFO_PishFaktor", IIf(txtModatCheck.Text.Trim = "", 0, txtModatCheck.Text.Trim), "ccPishFaktorTitr = " & frm_ccPishFaktor)
        Me.Close()
    End Sub
    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub cmbNoePardakht_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbNoePardakht.SelectedIndexChanged
        If Flg Then
            MeghdarAdadi = ObjCode.GetMeghdarAdadi(cmbNoePardakht.SelectedValue)

            Dim ccMoshtary As Integer = objTools.DLookup("ccMoshtary", "tblFO_PishFaktor", "ccPishFaktorTitr = " & frm_ccPishFaktor)
            If MeghdarAdadi < 4 Then
                txtModatCheck.Enabled = True
                lblModatCheck.Visible = True
                txtModatCheck.Text = objTools.ConvertNulls(objTools.DLookup("ModateChek", "tblFO_Moshtary", "ccMoshtary = " & ccMoshtary), 0)
            Else
                txtModatCheck.Enabled = False
                txtModatCheck.Text = ""
            End If
        End If
    End Sub
End Class