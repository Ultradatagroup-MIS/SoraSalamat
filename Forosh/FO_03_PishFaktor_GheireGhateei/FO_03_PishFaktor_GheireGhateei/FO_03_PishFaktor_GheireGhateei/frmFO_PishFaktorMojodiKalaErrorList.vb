Public Class frmFO_PishFaktorMojodiKalaErrorList
    Public dvPrint As DataView
    Public flg As Boolean = False
    Private Sub frmFO_PishFaktorMojodiKalaErrorList_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetGridStyle()
        Me.CenterToScreen()
    End Sub
    Public Sub SetGridStyle()

        Try
            dgvTitr.AllowUserToAddRows = False
            dgvTitr.AllowUserToDeleteRows = False
            dgvTitr.AllowUserToOrderColumns = False

            dgvTitr.Columns(0).HeaderText = "کـد کـالا"
            dgvTitr.Columns(0).Width = 80
            dgvTitr.Columns(1).HeaderText = "نام کـــــالا"
            dgvTitr.Columns(1).Width = 200
            dgvTitr.Columns(2).HeaderText = "تعـــــــداد"
            dgvTitr.Columns(2).Width = 80
            dgvTitr.Columns(3).HeaderText = "تعداد کــسری"
            dgvTitr.Columns(3).Width = 90
            dgvTitr.Columns(4).HeaderText = "پیغـــام"
            dgvTitr.Columns(4).Width = 180

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->SetGridStyle")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->SetGridStyle")
        End Try

    End Sub

    Private Sub btnTaeed_Click(sender As Object, e As EventArgs) Handles btnTaeed.Click
        Me.Close()
    End Sub

    Private Sub GroupBox3_Enter(sender As Object, e As EventArgs) Handles GroupBox3.Enter

    End Sub
End Class