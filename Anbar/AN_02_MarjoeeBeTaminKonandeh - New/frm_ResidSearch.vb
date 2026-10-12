Imports System.Data.SqlClient

Public Class frm_ResidSearch
    ' ---------------- خروجی‌ها ----------------
    Public SelectedId As Integer = 0                 ' ccKardexTitr رسید
    Public SelectedShomareh As Integer = 0           ' شماره‌ی رسید
    Public SelectedTaminKonandeh As Integer = 0
    Public SelectedAnbar As Integer = 0
    Public SelectedTarikh As String = ""

    Private Const MaxRows As Integer = 300

    Private WithEvents txtShomareh As New TextBox()
    Private WithEvents txtTarikh As New TextBox()
    Private WithEvents dgv As New DataGridView()
    Private WithEvents tmrSearch As New System.Windows.Forms.Timer()
    Private WithEvents btnOk As New Button()
    Private WithEvents btnCancel As New Button()
    Private lblInfo As New Label()

    Public Sub New()
        InitializeComponent()
        BuildUi()
    End Sub

#Region "ساخت فرم"
    Private Sub BuildUi()
        Me.Text = "جستجوی رسید"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.RightToLeftLayout = True
        Me.StartPosition = FormStartPosition.CenterParent
        Me.Size = New System.Drawing.Size(880, 540)
        Me.MinimumSize = New System.Drawing.Size(640, 360)
        Me.TopMost = True

        btnCancel.DialogResult = DialogResult.Cancel
        Me.CancelButton = btnCancel

        Dim tbl As New TableLayoutPanel()
        tbl.Dock = DockStyle.Fill
        tbl.ColumnCount = 1
        tbl.RowCount = 3
        tbl.RowStyles.Add(New RowStyle(SizeType.Absolute, 46))
        tbl.RowStyles.Add(New RowStyle(SizeType.Percent, 100))
        tbl.RowStyles.Add(New RowStyle(SizeType.Absolute, 46))

        ' ---------- ردیف بالا: جستجو ----------
        Dim pnlTop As New FlowLayoutPanel()
        pnlTop.Dock = DockStyle.Fill
        pnlTop.Padding = New Padding(6, 8, 6, 4)

        Dim lblS As New Label()
        lblS.Text = "شماره فرم:"
        lblS.AutoSize = True
        lblS.Margin = New Padding(3, 6, 3, 0)

        txtShomareh.Width = 120
        txtShomareh.Margin = New Padding(3, 2, 3, 0)

        Dim lblT As New Label()
        lblT.Text = "تاریخ فرم:"
        lblT.AutoSize = True
        lblT.Margin = New Padding(20, 6, 3, 0)

        txtTarikh.Width = 120
        txtTarikh.Margin = New Padding(3, 2, 3, 0)

        Dim lblD As New Label()
        lblD.Text = "دوره: " & CodeDoreh
        lblD.AutoSize = True
        lblD.Margin = New Padding(20, 6, 3, 0)

        pnlTop.Controls.Add(lblS)
        pnlTop.Controls.Add(txtShomareh)
        pnlTop.Controls.Add(lblT)
        pnlTop.Controls.Add(txtTarikh)
        pnlTop.Controls.Add(lblD)

        ' ---------- ردیف وسط: لیست ----------
        dgv.Dock = DockStyle.Fill
        dgv.ReadOnly = True
        dgv.AllowUserToAddRows = False
        dgv.AllowUserToDeleteRows = False
        dgv.AllowUserToResizeRows = False
        dgv.MultiSelect = False
        dgv.RowHeadersVisible = False
        dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgv.AutoGenerateColumns = False
        dgv.BackgroundColor = Color.White

        AddCol("ShomarehForm", "شماره رسید", 12)
        AddCol("TarikhForm", "تاریخ", 14)
        AddCol("NameTaminKonandeh", "تامین‌کننده", 34)
        AddCol("Tozihat", "توضیحات", 40)

        ' ---------- ردیف پایین: دکمه‌ها ----------
        Dim pnlBottom As New FlowLayoutPanel()
        pnlBottom.Dock = DockStyle.Fill
        pnlBottom.Padding = New Padding(6, 6, 6, 4)

        btnOk.Text = "انتخاب"
        btnOk.Width = 90
        btnCancel.Text = "انصراف"
        btnCancel.Width = 90

        lblInfo.AutoSize = True
        lblInfo.Margin = New Padding(20, 8, 3, 0)

        pnlBottom.Controls.Add(btnOk)
        pnlBottom.Controls.Add(btnCancel)
        pnlBottom.Controls.Add(lblInfo)

        tbl.Controls.Add(pnlTop, 0, 0)
        tbl.Controls.Add(dgv, 0, 1)
        tbl.Controls.Add(pnlBottom, 0, 2)
        Me.Controls.Add(tbl)

        tmrSearch.Interval = 350
    End Sub

    Private Sub AddCol(ByVal prop As String, ByVal header As String, ByVal weight As Single)
        Dim c As New DataGridViewTextBoxColumn()
        c.Name = prop
        c.DataPropertyName = prop
        c.HeaderText = header
        c.FillWeight = weight
        c.ReadOnly = True
        dgv.Columns.Add(c)
    End Sub
#End Region

#Region "جستجو"
    Private Sub frm_ResidSearch_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        DoSearch()
        txtShomareh.Focus()
    End Sub

    Private Sub DoSearch()
        Try
            Dim dt As New DataTable()
            Using cn As New SqlConnection(ConnectionString)
                Using cm As SqlCommand = cn.CreateCommand()
                    cm.CommandType = CommandType.StoredProcedure
                    cm.CommandText = "[dbo].[spAN_KdxMarjoee_SearchResid]"
                    cm.Parameters.AddWithValue("@CodeMahal", CodeMahalFaal)
                    cm.Parameters.AddWithValue("@CodeDoreh", CInt(CodeDoreh))      ' همیشه دوره‌ی فعال
                    cm.Parameters.AddWithValue("@ShomarehForm", txtShomareh.Text.Trim())
                    cm.Parameters.AddWithValue("@TarikhForm", txtTarikh.Text.Trim())
                    cm.Parameters.AddWithValue("@Top", MaxRows)
                    Using da As New SqlDataAdapter(cm)
                        da.Fill(dt)
                    End Using
                End Using
            End Using

            dgv.DataSource = dt
            lblInfo.Text = dt.Rows.Count.ToString() & " رسید" &
                           If(dt.Rows.Count >= MaxRows, "  (فقط " & MaxRows.ToString() & " مورد اول؛ جستجو را دقیق‌تر کنید)", "")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub txtFilter_TextChanged(sender As Object, e As EventArgs) Handles txtShomareh.TextChanged, txtTarikh.TextChanged
        tmrSearch.Stop()
        tmrSearch.Start()
    End Sub

    Private Sub tmrSearch_Tick(sender As Object, e As EventArgs) Handles tmrSearch.Tick
        tmrSearch.Stop()
        DoSearch()
    End Sub

    Private Sub txtFilter_KeyDown(sender As Object, e As KeyEventArgs) Handles txtShomareh.KeyDown, txtTarikh.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.Handled = True
            e.SuppressKeyPress = True
            tmrSearch.Stop()
            DoSearch()
            If dgv.Rows.Count = 1 Then
                PickCurrent()                  ' یک نتیجه: مستقیم انتخاب
            ElseIf dgv.Rows.Count > 1 Then
                dgv.Focus()
            End If
        ElseIf e.KeyCode = Keys.Down Then
            e.Handled = True
            e.SuppressKeyPress = True
            dgv.Focus()
        End If
    End Sub
#End Region

#Region "انتخاب"
    ' تاریخ را به شکل 1405/07/17 نشان می‌دهد
    Private Sub dgv_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles dgv.CellFormatting
        If e.RowIndex < 0 OrElse e.Value Is Nothing Then Exit Sub
        If dgv.Columns(e.ColumnIndex).Name = "TarikhForm" Then
            Dim s As String = Convert.ToString(e.Value).Replace("/", "")
            If s.Length = 8 Then
                e.Value = s.Substring(0, 4) & "/" & s.Substring(4, 2) & "/" & s.Substring(6, 2)
                e.FormattingApplied = True
            End If
        End If
    End Sub

    Private Sub PickCurrent()
        If dgv.CurrentRow Is Nothing Then Exit Sub
        Dim r As DataRowView = TryCast(dgv.CurrentRow.DataBoundItem, DataRowView)
        If r Is Nothing Then Exit Sub

        SelectedId = Convert.ToInt32(r("ccKardexTitr"))
        SelectedShomareh = Convert.ToInt32(r("ShomarehForm"))
        SelectedTaminKonandeh = If(IsDBNull(r("ccTaminKonandeh")), 0, Convert.ToInt32(r("ccTaminKonandeh")))
        SelectedAnbar = If(IsDBNull(r("ccAnbar")), 0, Convert.ToInt32(r("ccAnbar")))
        SelectedTarikh = If(IsDBNull(r("TarikhForm")), "", Convert.ToString(r("TarikhForm")))

        Me.DialogResult = DialogResult.OK
        Me.Close()
    End Sub

    Private Sub dgv_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgv.CellDoubleClick
        If e.RowIndex >= 0 Then PickCurrent()
    End Sub

    Private Sub dgv_KeyDown(sender As Object, e As KeyEventArgs) Handles dgv.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.Handled = True
            e.SuppressKeyPress = True
            PickCurrent()
        End If
    End Sub

    Private Sub btnOk_Click(sender As Object, e As EventArgs) Handles btnOk.Click
        PickCurrent()
    End Sub
#End Region

End Class