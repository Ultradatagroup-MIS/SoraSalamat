Imports System.Data.SqlClient

Public Class frmFO_KalaSearch_Faktor
    Inherits System.Windows.Forms.Form

    Private ReadOnly _cs As String
    Private ReadOnly _ccFaktor As Integer
    Private WithEvents grd As DataGridView
    Private WithEvents txtFilter As TextBox
    Private dt As DataTable

    Public SelectedCcKala As Integer = 0
    Public SelectedCodeKala As String = ""
    Public SelectedNameKala As String = ""
    Public SelectedBach As String = ""
    Public SelectedTarikhTolid As String = ""
    Public SelectedTarikhEngheza As String = ""
    Public SelectedFee As Double = 0
    Public SelectedBaghi As Double = 0
    Public SelectedIsJayezeh As Boolean = False
    Public SelectedIRC As String = ""
    Public SelectedGTIN As String = ""

    Public Sub New(ByVal cs As String, ByVal ccFaktor As Integer)
        _cs = cs
        _ccFaktor = ccFaktor

        Me.Text = "کالاهای فاکتور"
        Me.StartPosition = FormStartPosition.CenterParent
        Me.Size = New Size(1000, 500)
        Me.RightToLeft = RightToLeft.Yes
        Me.RightToLeftLayout = True
        Me.KeyPreview = True
        Me.TopMost = True

        txtFilter = New TextBox()
        txtFilter.Dock = DockStyle.Top
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

    Private Sub frmFO_KalaSearch_Faktor_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadData()
        txtFilter.Focus()
    End Sub

    Private Sub LoadData()
        Try
            ' مرجوعی‌های قبلی (هر وضعیتی جز عدم تایید) به ترتیب سطر فاکتور روی سطرها پخش می‌شوند
            Dim sql As String =
             "WITH F0 AS ( " &
             "  SELECT f.ccFaktorSatr, f.ccKala, f.Tedad3, ISNULL(f.IsJayezeh,0) AS IsJay, f.Fee, " &
             "         f.ShomarehBach, f.TarikhTolid, f.TarikhEngheza, " &
             "         REPLACE(LTRIM(RTRIM(ISNULL(f.ShomarehBach,''))),' ','') AS BachK " &
             "  FROM tblFO_FaktorSatr f WHERE f.ccFaktorTitr = @f AND ISNULL(f.IsSabadKala,0) = 0 ), " &
             "F AS ( " &
             "  SELECT F0.*, SUM(F0.Tedad3) OVER (PARTITION BY F0.ccKala, F0.IsJay, F0.BachK " &
             "                                    ORDER BY F0.ccFaktorSatr ROWS UNBOUNDED PRECEDING) AS CumEnd FROM F0 ), " &
             "M AS ( " &
             "  SELECT s.ccKala, ISNULL(s.IsJayezehMarjoee,0) AS IsJay, " &
             "         REPLACE(LTRIM(RTRIM(ISNULL(s.ShomarehBach,''))),' ','') AS BachK, SUM(s.Tedad3) AS Used " &
             "  FROM tblFO_ElamMarjoeeSatr s JOIN tblFO_ElamMarjoee t ON t.ccElamMarjoee = s.ccMarjoee " &
             "  WHERE s.ccFaktorTitr = @f AND ISNULL(t.sVazeiat,0) <> 4354 " &
             "  GROUP BY s.ccKala, ISNULL(s.IsJayezehMarjoee,0), REPLACE(LTRIM(RTRIM(ISNULL(s.ShomarehBach,''))),' ','') ), " &
             "X AS ( " &
             "  SELECT F.*, CASE WHEN ISNULL(M.Used,0) <= F.CumEnd - F.Tedad3 THEN F.Tedad3 " &
             "                   WHEN ISNULL(M.Used,0) >= F.CumEnd THEN 0 " &
             "                   ELSE F.CumEnd - ISNULL(M.Used,0) END AS Baghi " &
             "  FROM F LEFT JOIN M ON M.ccKala = F.ccKala AND M.IsJay = F.IsJay AND M.BachK = F.BachK ) " &
             "SELECT X.ccFaktorSatr, k.ccKala, k.CodeKala AS [کد کالا], k.NameKala AS [نام کالا], " &
             "       LTRIM(RTRIM(ISNULL(X.ShomarehBach,''))) AS [شماره بچ], X.TarikhTolid, X.TarikhEngheza, " &
             "       b.IRC AS [IRC], b.GTIN AS [GTIN], X.Fee AS [فی], " &
             "       CASE WHEN X.IsJay = 1 THEN N'جایزه' ELSE N'' END AS [نوع], " &
             "       CAST(X.IsJay AS BIT) AS IsJayezeh, " &
             "       X.Tedad3 AS [تعداد فاکتور], X.Baghi AS [باقی‌مانده] " &
             "FROM X JOIN tblAN_Kala k ON k.ccKala = X.ccKala " &
             "OUTER APPLY ( SELECT TOP 1 sb.IRC, sb.GTIN FROM tblGL_ShomarehBach sb " &
             "              WHERE sb.ccKala = X.ccKala AND LTRIM(RTRIM(sb.ShomarehBach)) = LTRIM(RTRIM(ISNULL(X.ShomarehBach,''))) " &
             "              ORDER BY sb.ccShomarehBach DESC ) b " &
             "WHERE X.Baghi > 0 " &
             "ORDER BY X.ccFaktorSatr"

            dt = New DataTable
            Using cn As New SqlConnection(_cs)
                Using da As New SqlDataAdapter(sql, cn)
                    da.SelectCommand.Parameters.AddWithValue("@f", _ccFaktor)
                    da.Fill(dt)
                End Using
            End Using

            grd.DataSource = dt.DefaultView
            For Each c As String In New String() {"ccFaktorSatr", "ccKala", "TarikhTolid", "TarikhEngheza", "IsJayezeh"}
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
                    " OR [نام کالا] LIKE '%" & t & "%' OR [شماره بچ] LIKE '%" & t & "%'"
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

        SelectedCcKala = Convert.ToInt32(r("ccKala"))
        SelectedCodeKala = Convert.ToString(r("کد کالا"))
        SelectedNameKala = Convert.ToString(r("نام کالا"))
        SelectedBach = Convert.ToString(r("شماره بچ"))
        SelectedTarikhTolid = If(IsDBNull(r("TarikhTolid")), "", Convert.ToString(r("TarikhTolid")))
        SelectedTarikhEngheza = If(IsDBNull(r("TarikhEngheza")), "", Convert.ToString(r("TarikhEngheza")))
        SelectedFee = If(IsDBNull(r("فی")), 0, Convert.ToDouble(r("فی")))
        SelectedBaghi = Convert.ToDouble(r("باقی‌مانده"))
        SelectedIsJayezeh = Not IsDBNull(r("IsJayezeh")) AndAlso Convert.ToBoolean(r("IsJayezeh"))
        SelectedIRC = If(IsDBNull(r("IRC")), "", Convert.ToString(r("IRC")).Trim())
        SelectedGTIN = If(IsDBNull(r("GTIN")), "", Convert.ToString(r("GTIN")).Trim())

        Me.DialogResult = DialogResult.OK
        Me.Close()
    End Sub

    Private Sub grd_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles grd.CellDoubleClick
        If e.RowIndex >= 0 Then PickCurrent()
    End Sub

    Private Sub frmFO_KalaSearch_Faktor_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
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
End Class