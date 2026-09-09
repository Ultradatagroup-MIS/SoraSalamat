Public Class frmFO_MasirPopup

#Region "Variable AND Constant Declration"
    Public dt As DataTable
    Public strMahale As String
    Public ccMasir As Integer
    Public NameMasir As String
    Public strNoeMasirMoshtary As String
    Dim txtCaption As String
    Dim dsForm As New DataSet
    Dim dvForm As DataView
    Private WithEvents BS As New UD_Dll.PassString
#End Region
#Region "Form Event Code"
    Private Sub frmFO_MasirPopup_Closed(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Closed
        dsForm = Nothing
        dvForm = Nothing
    End Sub
    Private Sub frmFO_MasirPopup_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing

        If Me.cmbMasir.SelectedValue <> 0 And cmbMasir.SelectedIndex <> -1 Then
            ccMasir = Me.cmbMasir.SelectedValue
            NameMasir = Me.cmbMasir.Text
        Else
            MessageBox.Show(".مسیر مربوطه را انتخاب کنید", ":توجه", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign, False)
            e.Cancel = True
        End If

    End Sub
    Private Sub frmFO_TafkikJozeBaresiPopup_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Me.Text = "انتخاب مسیر"
        Label1.Text = " از آنجاییکه محله " & " " & strMahale & " --- " & "در" & " " & dt.Rows.Count.ToString & " " & "مسیر " & " " & strNoeMasirMoshtary & " " & "قرار گرفته است" & vbCrLf & vbCrLf
        Label1.Text &= ".لطفاً یکی از مسیرهای مربوطه را برای مشتری مذکور انتخاب کنید"

        Dim dr As DataRow
        dr = dt.NewRow
        dr.Item("ccMasir") = 0
        dr.Item("NameMasir") = "-----"
        dt.Rows.Add(dr)

        Me.cmbMasir.DataSource = dt
        Me.cmbMasir.ValueMember = "ccMasir"
        Me.cmbMasir.DisplayMember = "NameMasir"
        Me.cmbMasir.SelectedValue = 0

    End Sub
#End Region
#Region "Form Buttons"
    Private Sub btnTaeed_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTaeed.Click
        Me.Close()
    End Sub
#End Region

End Class