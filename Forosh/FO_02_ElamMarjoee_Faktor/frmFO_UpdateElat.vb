Public Class frmFO_UpdateElat

#Region "Variable AND Constant Declration"
    Dim dsForm As New DataSet
    Public PK As Integer
    Dim Elat As Integer
#End Region

    Private Sub frmFO_UpdateElat_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        LoadCombo()
        Elat = objTools.DLookup("sElat", "tblFO_ElamMarjoee", "ccElamMarjoee = " & PK)

        cmbElatMarjoee.SelectedValue = Elat
        cmbElatMarjoee.SelectedValue = Elat
    End Sub
    Private Sub LoadCombo()
        Dim cnSQL As New SqlConnection
        Dim cmSQL As New SqlCommand
        Dim daSQL As New SqlDataAdapter
        Dim strSQL As String = ""

        Try

            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()

            strSQL = "Global.spElatMarjoee_LoadCombo "

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            daSQL = New SqlDataAdapter(cmSQL)
            daSQL.Fill(dsForm, "tblElatMarjoee")

            cmbElatMarjoee.DataSource = Nothing
            cmbElatMarjoee.Items.Clear()
            cmbElatMarjoee.DataSource = dsForm.Tables("tblElatMarjoee").DefaultView
            cmbElatMarjoee.DisplayMember = "Sharh"
            cmbElatMarjoee.ValueMember = "Code"

            cmSQL = Nothing : daSQL = Nothing
            cnSQL.Close()

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> LoadCombo ")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> LoadCombo ")
        End Try
    End Sub
    Private Sub btnTaeed_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTaeed.Click
        If cmbElatMarjoee.SelectedValue = Elat Then
            MsgBox("علت انتخاب شده با علت مرجوعی یکسان است .", MsgBoxStyle.Information + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.OkOnly, "")
            Exit Sub
        ElseIf MsgBox("آیا به روز رسانی صورت پذیرد ؟", MsgBoxStyle.Question + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.YesNo, "") = MsgBoxResult.Yes Then
            UpdateElatMaroee()
            Elat = cmbElatMarjoee.SelectedValue
        Else
            Exit Sub
        End If
    End Sub
    Private Sub UpdateElatMaroee()
        Dim cnSQL As New SqlConnection
        Dim cmSQL As New SqlCommand
        Dim strSQL As String = ""

        Try
            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()

            strSQL = "Sales.spElamMarjoeeFaktor_UpdateElatMarjoee "

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            cmSQL.Parameters.AddWithValue("ccElamMarjoee", PK)
            cmSQL.Parameters.AddWithValue("sElatMarjoee", cmbElatMarjoee.SelectedValue)

            cmSQL.ExecuteNonQuery()

            cmSQL = Nothing
            cnSQL.Close()

            MsgBox("به روز رسانی انجام شد .", MsgBoxStyle.Information + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.OkOnly, "به روز رسانی")

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> UpdateElatMaroee ")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> UpdateElatMaroee ")
        End Try
    End Sub
    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

#Region "Stored Procedure"
    '' Global.spElatMarjoee_LoadCombo
    '' Sales.spElamMarjoeeFaktor_UpdateElatMarjoee
#End Region

End Class