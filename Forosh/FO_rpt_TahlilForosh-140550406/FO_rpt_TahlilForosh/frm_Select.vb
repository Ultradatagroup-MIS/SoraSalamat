Imports System.Windows.Forms

Public Class frm_Select
    Dim dsForm As New DataSet
    Public test As String = ""

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        If IsNothing(lst.SelectedValue) Then
            Exit Sub
        End If
        test = lst.Text
        frm_Report.RepTitle += lst.Text
        Me.Tag = lst.SelectedValue
        frmFO_ReportTahlilForosh.dtTitle.Rows.Add("(" & lst.Text & ")")
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub
    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub
    Private Sub frm_MarkazPakhsh_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Dim Strsql As String
            Dim daSQL As SqlDataAdapter

            Strsql = "select ccGozaresh,txtGozaresh from tblGL_Gozaresh where Show=1 and ccGozaresh_Link = " & Me.Tag
            daSQL = New SqlDataAdapter(Strsql, ConnectionString)
            daSQL.Fill(dsForm, "tbl")
            lst.DataSource = Nothing
            lst.Items.Clear()
            lst.DataSource = dsForm.Tables("tbl").DefaultView
            lst.DisplayMember = "txtGozaresh"
            lst.ValueMember = "ccGozaresh"

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "Error IN DataBase ---->" & "SetFormData")
        End Try
    End Sub
    Private Sub lst_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lst.DoubleClick
        If IsNothing(lst.SelectedValue) Then
            Exit Sub
        End If

        Me.Tag = lst.SelectedValue
        frmFO_ReportTahlilForosh.dtTitle.Rows.Add("(" & lst.Text & ")")
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

End Class
