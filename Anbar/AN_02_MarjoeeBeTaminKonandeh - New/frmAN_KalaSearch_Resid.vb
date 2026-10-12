Imports System.Data.SqlClient

Public Class frmAN_KalaSearch_Resid
    Inherits System.Windows.Forms.Form

    Private ReadOnly _cs As String
    Private ReadOnly _ccResid As Integer

    Private WithEvents grd As DataGridView
    Private WithEvents txtFilter As TextBox
    Private dt As DataTable

    Public SelectedCcKala As Integer = 0
    Public SelectedCodeKala As String = ""
    Public SelectedNameKala As String = ""
    Public SelectedBach As String = ""
    Public SelectedTarikhTolid As String = ""
    Public SelectedTarikhEngheza As String = ""
    Public SelectedMablaghKharid As Double = 0
    Public SelectedTedadMandeh As Double = 0
    Public SelectedIRC As String = ""
    Public SelectedGTIN As String = ""
    Public InitialFilter As String = ""
    Public SelectedIsJayezeh As Boolean = False

    Public Sub New(ByVal cs As String, ByVal ccResid As Integer)
        _cs = cs
        _ccResid = ccResid

        Me.Text = "کالاهای رسید"
        Me.StartPosition = FormStartPosition.CenterParent
        Me.Size = New Size(950, 500)
        Me.RightToLeft = RightToLeft.Yes
        Me.RightToLeftLayout = True
        Me.KeyPreview = True

        txtFilter = New TextBox()
        txtFilter.Dock = DockStyle.Top
        txtFilter.Height = 24
        Me.Controls.Add(txtFilter)

        grd = New DataGridView()
        grd.Dock = DockStyle.Fill
        grd.ReadOnly = True
        grd.AllowUserToAddRows = False
        grd.AllowUserToDeleteRows = False
        grd.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        grd.MultiSelect = False
        grd.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        grd.RowHeadersVisible = False
        Me.Controls.Add(grd)
        grd.BringToFront()
    End Sub


    Private Sub LoadData()
        Try
            ' هر ترکیب کالا + بچ در رسید، با مقدار باقی‌مانده بعد از مرجوعی‌های قبلی
            Dim sql As String =
            "WITH R AS ( " &
            "  SELECT r.ccKardexSatr, r.Radif, r.ccKala, r.Tedad3, ISNULL(r.IsJayezeh,0) AS IsJay, " &
            "         REPLACE(LTRIM(RTRIM(ISNULL(r.ShomarehBach,''))),' ','') AS BachK, " &
            "         r.ShomarehBach, r.TarikhTolid, r.TarikhEngheza, r.MablaghKharid, " &
            "         SUM(r.Tedad3) OVER (PARTITION BY r.ccKala, ISNULL(r.IsJayezeh,0), REPLACE(LTRIM(RTRIM(ISNULL(r.ShomarehBach,''))),' ','') " &
            "                             ORDER BY r.Radif ROWS UNBOUNDED PRECEDING) AS CumEnd " &
            "  FROM tblAN_KdxResidSatr r WHERE r.ccKardexTitr = @r ), " &
            "M AS ( " &
            "  SELECT s.ccKala, ISNULL(s.IsJayezeh,0) AS IsJay, REPLACE(LTRIM(RTRIM(ISNULL(s.ShomarehBach,''))),' ','') AS BachK, " &
            "         SUM(s.Tedad3) AS Used " &
            "  FROM tblAN_KdxMarjoeeBeTaminKonandehSatr s " &
            "  JOIN tblAN_KdxMarjoeeBeTaminKonandeh t ON t.ccKardexTitr = s.ccKardexTitr " &
            "  WHERE t.ccKardexTitrR = @r " &
            "  GROUP BY s.ccKala, ISNULL(s.IsJayezeh,0), REPLACE(LTRIM(RTRIM(ISNULL(s.ShomarehBach,''))),' ','') ), " &
            "X AS ( " &
            "  SELECT R.*, " &
            "         CASE WHEN ISNULL(M.Used,0) <= R.CumEnd - R.Tedad3 THEN R.Tedad3 " &
            "              WHEN ISNULL(M.Used,0) >= R.CumEnd THEN 0 " &
            "              ELSE R.CumEnd - ISNULL(M.Used,0) END AS Baghi " &
            "  FROM R LEFT JOIN M ON M.ccKala = R.ccKala AND M.IsJay = R.IsJay AND M.BachK = R.BachK ) " &
            "SELECT X.ccKardexSatr, X.Radif AS [ردیف], k.ccKala, k.CodeKala AS [کد کالا], k.NameKala AS [نام کالا], " &
            "       LTRIM(RTRIM(ISNULL(X.ShomarehBach,''))) AS [شماره بچ], X.TarikhTolid, X.TarikhEngheza, " &
            "       b.IRC AS [IRC], b.GTIN AS [GTIN], " &
            "       X.MablaghKharid AS [مبلغ خرید], " &
            "       CASE WHEN X.IsJay=1 THEN N'جایزه' ELSE N'' END AS [نوع], " &
            "       CAST(X.IsJay AS BIT) AS IsJayezeh, " &
            "       X.Tedad3 AS [تعداد رسید], " &
            "       X.Baghi AS [باقی‌مانده] " &
            "FROM X " &
            "JOIN qryAN_Kala k ON k.ccKala = X.ccKala " &
            "OUTER APPLY ( SELECT TOP 1 sb.IRC, sb.GTIN FROM tblGL_ShomarehBach sb " &
            "              WHERE sb.ccKala = X.ccKala AND LTRIM(RTRIM(sb.ShomarehBach)) = LTRIM(RTRIM(ISNULL(X.ShomarehBach,''))) " &
            "              ORDER BY sb.ccShomarehBach DESC ) b " &
            "WHERE X.Baghi > 0 " &
            "ORDER BY X.Radif"

            dt = New DataTable
            Using cn As New SqlConnection(_cs)
                Using da As New SqlDataAdapter(sql, cn)
                    da.SelectCommand.Parameters.AddWithValue("@r", _ccResid)
                    da.Fill(dt)
                End Using
            End Using

            grd.DataSource = dt.DefaultView
            If InitialFilter <> "" Then txtFilter.Text = InitialFilter
            For Each c As String In New String() {"ccKardexSatr", "ccKala", "TarikhTolid", "TarikhEngheza", "IsJayezeh"}
                If grd.Columns.Contains(c) Then grd.Columns(c).Visible = False
            Next
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub txtFilter_TextChanged(sender As Object, e As EventArgs) Handles txtFilter.TextChanged
        If dt Is Nothing Then Exit Sub
        Dim t As String = txtFilter.Text.Trim().Replace("'", "''")
        Try
            If t = "" Then
                dt.DefaultView.RowFilter = ""
            Else
                dt.DefaultView.RowFilter =
                  "Convert([کد کالا], 'System.String') LIKE '%" & t & "%'" &
                   " OR [نام کالا] LIKE '%" & t & "%'" &
                   " OR [شماره بچ] LIKE '%" & t & "%'"
            End If
        Catch
            dt.DefaultView.RowFilter = ""
        End Try
    End Sub

    Private Sub PickCurrent()
        If grd.CurrentRow Is Nothing Then Exit Sub
        Dim drv As DataRowView = TryCast(grd.CurrentRow.DataBoundItem, DataRowView)
        If drv Is Nothing Then Exit Sub
        Dim r As DataRow = drv.Row
        For Each c As String In New String() {"ccKardexSatr", "ccKala", "TarikhTolid", "TarikhEngheza", "IsJayezeh"}
            If grd.Columns.Contains(c) Then grd.Columns(c).Visible = False
        Next
        SelectedCcKala = Convert.ToInt32(r("ccKala"))
        SelectedCodeKala = Convert.ToString(r("کد کالا"))
        SelectedNameKala = Convert.ToString(r("نام کالا"))
        SelectedBach = Convert.ToString(r("شماره بچ"))
        SelectedTarikhTolid = Convert.ToString(r("TarikhTolid"))
        SelectedTarikhEngheza = Convert.ToString(r("TarikhEngheza"))
        If Not IsDBNull(r("مبلغ خرید")) Then SelectedMablaghKharid = Convert.ToDouble(r("مبلغ خرید"))
        If Not IsDBNull(r("باقی‌مانده")) Then SelectedTedadMandeh = Convert.ToDouble(r("باقی‌مانده"))
        SelectedIsJayezeh = Not IsDBNull(r("IsJayezeh")) AndAlso Convert.ToBoolean(r("IsJayezeh"))
        SelectedIRC = If(IsDBNull(r("IRC")), "", Convert.ToString(r("IRC")).Trim())
        SelectedGTIN = If(IsDBNull(r("GTIN")), "", Convert.ToString(r("GTIN")).Trim())

        Me.DialogResult = DialogResult.OK
        Me.Close()
    End Sub

    Private Sub grd_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles grd.CellDoubleClick
        If e.RowIndex >= 0 Then PickCurrent()
    End Sub

    Private Sub frmResidKalaSearch_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.Handled = True
            e.SuppressKeyPress = True
            PickCurrent()
        ElseIf e.KeyCode = Keys.Escape Then
            Me.DialogResult = DialogResult.Cancel
            Me.Close()
        ElseIf e.KeyCode = Keys.Down AndAlso txtFilter.Focused Then
            grd.Focus()
        End If
    End Sub

    Private Sub frmAN_KalaSearch_Resid_Load_1(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadData()
        txtFilter.Focus()
    End Sub
End Class