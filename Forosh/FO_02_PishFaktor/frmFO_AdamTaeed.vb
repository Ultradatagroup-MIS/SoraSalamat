Public Class frmFO_AdamTaeed

    Private dsForm As New DataSet
    Public ccPishFaktorForAdamTaeed As Integer
    Public Flg_Adam As Boolean
    Private Sub frmFO_AdamTaeed_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        LoadCombo()
        Flg_Adam = False
    End Sub
    Private Sub LoadCombo()
        Dim cnSQL As New SqlConnection
        Dim cmSQL As New SqlCommand
        Dim daSQL As New SqlDataAdapter
        Dim strSQL As String

        Try
            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()

            strSQL = "Global.spElatAdamTaeedPishFaktor_LoadCombo "

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            daSQL = New SqlDataAdapter(cmSQL)
            daSQL.Fill(dsForm, "tblElat")

            cmbElatAdamTaeed.DataSource = Nothing
            cmbElatAdamTaeed.Items.Clear()
            cmbElatAdamTaeed.DataSource = dsForm.Tables("tblElat").DefaultView
            cmbElatAdamTaeed.DisplayMember = "Elat"
            cmbElatAdamTaeed.ValueMember = "Code"

            cmSQL = Nothing : daSQL = Nothing
            cnSQL.Close()

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> LoadComboElatAdamTaeed ")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> LoadComboElatAdamTaeed ")
        End Try
    End Sub
    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub btnAdamTaeed_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdamTaeed.Click
        If cmbElatAdamTaeed.SelectedIndex = -1 Then
            MsgBox("علت عــدم تاییــد را مشخص نمایید .", MsgBoxStyle.Information + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " پیام")
            Exit Sub
        End If

        Try
            AdamTaeed(ccPishFaktorForAdamTaeed)
            Flg_Adam = True
            Me.Close()
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> btnAdamTaeed_Click ")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> btnAdamTaeed_Click ")
        End Try
    End Sub
    Private Sub AdamTaeed(ByVal ccPishFaktor As Integer)
        If MsgBox("در صورت عدم تایید نمودن پیش فاکتور، امکان تغییر وضعیت آن وجود ندارد." & Chr(13) & Chr(10) & _
                            "آیا پیش فاکتور عــدم تایید شود ؟ ", MsgBoxStyle.Question + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.YesNo + MsgBoxStyle.MsgBoxRtlReading, " پیام") = MsgBoxResult.No Then
            Exit Sub
        End If

        Try
            Dim cnSQL As New SqlConnection
            Dim cmSQL As New SqlCommand
            Dim strSQL As String

            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()

            strSQL = "Sales.spPishFaktor_AdamTaeed "

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            cmSQL.Parameters.AddWithValue("ccPishFaktorTitr", ccPishFaktor)
            cmSQL.Parameters.AddWithValue("sElat", cmbElatAdamTaeed.SelectedValue)

            cmSQL.ExecuteNonQuery()

            cmSQL = Nothing
            cnSQL.Close()

            MsgBox("عملیات با موفقیت انجام شد .", MsgBoxStyle.Information + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " پیام")
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> AdamTaeed ")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> AdamTaeed ")
        End Try
    End Sub

End Class