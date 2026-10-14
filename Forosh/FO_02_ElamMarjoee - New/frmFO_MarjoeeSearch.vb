Imports System.Data.SqlClient

Public Class frmFO_MarjoeeSearch

    Private _dt As DataTable
    Private _selectedId As Integer = 0
    Private _ccMoshtary As Integer = 0
    Private _setting As Boolean = False

    Private WithEvents txtShomareh As New TextBox()
    Private WithEvents txtCodeMoshtary As New TextBox()
    Private lblNameMoshtary As New Label()
    Private cmbForoshandeh As New ComboBox()
    Private mskAz As New MaskedTextBox()
    Private mskTa As New MaskedTextBox()
    Private cmbNoe As New ComboBox()
    Private WithEvents btnSearch As New Button()
    Private WithEvents btnClear As New Button()

    Public ReadOnly Property SelectedId() As Integer
        Get
            Return _selectedId
        End Get
    End Property

#Region "ساخت پنل فیلتر"
    Private Sub frmFO_MarjoeeSearch_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            'BuildFilterUi()
            'LoadForoshandeh()
            'LoadList()
            'GridEXList.Focus()
            LoadList()
            GridEXList.Dock = DockStyle.Fill
            GridEXList.BringToFront()
            GridEXList.Focus()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub AddLbl(ByVal t As TableLayoutPanel, ByVal text As String, ByVal col As Integer)
        Dim l As New Label()
        l.Text = text
        l.Dock = DockStyle.Fill
        l.TextAlign = ContentAlignment.BottomRight
        t.Controls.Add(l, col, 0)
    End Sub

    Private Sub BuildFilterUi()
        Dim t As New TableLayoutPanel()
        t.Dock = DockStyle.Top
        t.Height = 64
        t.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        t.ColumnCount = 9
        t.RowCount = 2
        For Each w As Integer In New Integer() {80, 100, 170, 170, 95, 95, 160, 80, 80}
            t.ColumnStyles.Add(New ColumnStyle(SizeType.Absolute, w))
        Next
        t.RowStyles.Add(New RowStyle(SizeType.Absolute, 22))
        t.RowStyles.Add(New RowStyle(SizeType.Absolute, 32))

        AddLbl(t, "شماره", 0)
        AddLbl(t, "کد مشتری", 1)
        AddLbl(t, "نام مشتری", 2)
        AddLbl(t, "فروشنده", 3)
        AddLbl(t, "از تاریخ", 4)
        AddLbl(t, "تا تاریخ", 5)
        AddLbl(t, "وضعیت", 6)

        txtShomareh.Dock = DockStyle.Fill
        txtCodeMoshtary.Dock = DockStyle.Fill
        lblNameMoshtary.Dock = DockStyle.Fill
        lblNameMoshtary.TextAlign = ContentAlignment.MiddleRight
        cmbForoshandeh.Dock = DockStyle.Fill
        cmbForoshandeh.DropDownStyle = ComboBoxStyle.DropDownList

        For Each m As MaskedTextBox In New MaskedTextBox() {mskAz, mskTa}
            m.Mask = "0000/00/00"
            m.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals
            m.Dock = DockStyle.Fill
        Next

        cmbNoe.Dock = DockStyle.Fill
        cmbNoe.DropDownStyle = ComboBoxStyle.DropDownList
        cmbNoe.Items.AddRange(New Object() {"همه (بجز ارسال‌شده)", "بدون عملیات", "تایید شده", "تایید نشده", "ارسال شده"})
        cmbNoe.SelectedIndex = 1                      ' پیش‌فرض: بدون عملیات (قابل ویرایش)

        btnSearch.Text = "جستجو"
        btnSearch.Dock = DockStyle.Fill
        btnClear.Text = "پاک کردن"
        btnClear.Dock = DockStyle.Fill

        t.Controls.Add(txtShomareh, 0, 1)
        t.Controls.Add(txtCodeMoshtary, 1, 1)
        t.Controls.Add(lblNameMoshtary, 2, 1)
        t.Controls.Add(cmbForoshandeh, 3, 1)
        t.Controls.Add(mskAz, 4, 1)
        t.Controls.Add(mskTa, 5, 1)
        t.Controls.Add(cmbNoe, 6, 1)
        t.Controls.Add(btnSearch, 7, 1)
        t.Controls.Add(btnClear, 8, 1)

        ' ترتیب Dock: پنل بالا، لیست پر می‌کند
        Me.Controls.Add(t)
        t.SendToBack()
        GridEXList.Dock = DockStyle.Fill
        GridEXList.BringToFront()

        ' Enter در هر فیلتر = جستجو
        For Each c As Control In New Control() {txtShomareh, txtCodeMoshtary, cmbForoshandeh, mskAz, mskTa, cmbNoe}
            AddHandler c.KeyDown, AddressOf Filter_KeyDown
        Next
        AddHandler txtShomareh.KeyPress, AddressOf DigitsOnly
    End Sub

    Private Sub LoadForoshandeh()
        Dim dt As New DataTable
        Dim s As String = "Select ccForoshandeh,LN From qryFO_Foroshandeh where ccForoshandeh <> 0 AND CodeMahal = " & CodeMahalFaal
        s &= " AND Not exists (Select * From tblGL_SecurityData where namekarbar= '" & UserName & "' and CodeSubSystem = 628 and pk = qryFO_Foroshandeh.ccForoshandeh) order by LName"
        Using da As New SqlDataAdapter(s, ConnectionString)
            da.Fill(dt)
        End Using
        Dim r As DataRow = dt.NewRow()
        r("ccForoshandeh") = 0
        r("LN") = "همه"
        dt.Rows.InsertAt(r, 0)

        cmbForoshandeh.DataSource = dt
        cmbForoshandeh.DisplayMember = "LN"
        cmbForoshandeh.ValueMember = "ccForoshandeh"
        cmbForoshandeh.SelectedIndex = 0
    End Sub
#End Region

#Region "فیلترها"
    Private Sub DigitsOnly(ByVal sender As Object, ByVal e As KeyPressEventArgs)
        If Not Char.IsDigit(e.KeyChar) AndAlso e.KeyChar <> ChrW(8) Then e.Handled = True
    End Sub

    Private Sub Filter_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs)
        If e.KeyCode = Keys.Enter Then
            e.Handled = True
            e.SuppressKeyPress = True
            If sender Is txtCodeMoshtary Then ResolveMoshtary()
            LoadList()
        End If
    End Sub

    ' کد مشتری -> ccMoshtary
    Private Sub ResolveMoshtary()
        If txtCodeMoshtary.Text.Trim() = "" Then
            _ccMoshtary = 0
            lblNameMoshtary.Text = ""
            Exit Sub
        End If
        Dim crit As String = "CodeMahal=" & CodeMahalFaal & " AND CodeMoshtary = '" & txtCodeMoshtary.Text.Trim().Replace("'", "") & "'"
        crit &= " AND Not exists (Select * From tblGL_SecurityData where namekarbar= '" & UserName & "' and CodeSubSystem = 614 and pk = qryFO_Moshtary.ccMoshtary) "
        _ccMoshtary = Convert.ToInt32(objTools.ConvertNulls(objTools.DLookup("ccMoshtary", "qryFO_Moshtary", crit), 0))
        lblNameMoshtary.Text = Convert.ToString(objTools.ConvertNulls(objTools.DLookup("NameMoshtary", "qryFO_Moshtary", crit), ""))
        If _ccMoshtary = 0 Then MessageBox.Show("مشتری پیدا نشد")
    End Sub

    Private Sub txtCodeMoshtary_TextChanged(sender As Object, e As EventArgs) Handles txtCodeMoshtary.TextChanged
        If _setting Then Exit Sub
        _ccMoshtary = 0
        lblNameMoshtary.Text = ""
    End Sub

    Private Sub txtCodeMoshtary_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtCodeMoshtary.KeyPress
        Try
            If e.KeyChar = " "c Then
                e.Handled = True
                Dim objMoshtary As New Forms_dll.frmFO_MoshtarySearch
                Dim StrSql As String = "Select * from qryFO_Moshtary Where CodeMahal=" & CodeMahalFaal & " AND sVazeiat = " & UD_Dll.Enums.FO_VaziatMoshtary.Faal
                StrSql &= " AND Not exists (Select * From tblGL_SecurityData where namekarbar= '" & UserName & "' and CodeSubSystem = 614 and pk = qryFO_Moshtary.ccMoshtary) "
                StrSql &= " AND Not exists (Select * From tblGL_SecurityData where namekarbar= '" & UserName & "' and CodeSubSystem = 10000 and pk = qryFO_Moshtary.sNoeMoshtary) "
                StrSql &= " order by sMantagheh,sMahaleh,NameMoshtary"

                tCodeMoshtary = ""
                tNameMoshtary = ""
                tccMoshtary = ""
                If txtCodeMoshtary.Text.Length <> 0 Then tCodeMoshtary = txtCodeMoshtary.Text

                objMoshtary.MultiSelection = False
                SearchItem = "CodeMoshtary"
                objMoshtary.SetForm(StrSql)
                objMoshtary.ShowDialog()

                _setting = True
                Try
                    txtCodeMoshtary.Text = Convert.ToString(objMoshtary.tCodeMoshtary)
                    lblNameMoshtary.Text = Convert.ToString(objMoshtary.tNameMoshtary)
                Finally
                    _setting = False
                End Try
                _ccMoshtary = If(IsNumeric(objMoshtary.tccMoshtary), Convert.ToInt32(objMoshtary.tccMoshtary), 0)
                MultiSelection = False
            ElseIf Not Char.IsDigit(e.KeyChar) AndAlso e.KeyChar <> ChrW(8) Then
                e.Handled = True
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        If txtCodeMoshtary.Text.Trim() <> "" AndAlso _ccMoshtary = 0 Then ResolveMoshtary()
        LoadList()
    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        _setting = True
        Try
            txtShomareh.Text = ""
            txtCodeMoshtary.Text = ""
            lblNameMoshtary.Text = ""
        Finally
            _setting = False
        End Try
        _ccMoshtary = 0
        cmbForoshandeh.SelectedIndex = 0
        mskAz.Text = ""
        mskTa.Text = ""
        cmbNoe.SelectedIndex = 1
        LoadList()
    End Sub

    ' لیست وضعیت‌ها مثل cmbNoe فرم قدیمی
    Private Function VazeiatList() As String
        Select Case cmbNoe.SelectedIndex
            Case 1
                Return CInt(UD_Dll.Enums.FO_VaziatElamMarjoee.BedoneAmalyat).ToString()
            Case 0
                Return CInt(UD_Dll.Enums.FO_VaziatElamMarjoee.TaeedNashodeh).ToString() & "," &
                       CInt(UD_Dll.Enums.FO_VaziatElamMarjoee.BedoneAmalyat).ToString() & "," &
                       CInt(UD_Dll.Enums.FO_VaziatElamMarjoee.TaeedShodeh).ToString() & ",-1"
            Case 2
                Return CInt(UD_Dll.Enums.FO_VaziatElamMarjoee.TaeedShodeh).ToString()
            Case 3
                Return CInt(UD_Dll.Enums.FO_VaziatElamMarjoee.TaeedNashodeh).ToString()
            Case Else
                Return CInt(UD_Dll.Enums.FO_VaziatElamMarjoee.Ersalshodeh).ToString()
        End Select
    End Function

    Private Function DateOrNull(ByVal m As MaskedTextBox) As Object
        Dim s As String = m.Text.Replace("/", "").Trim()
        If s.Length = 8 Then Return s
        Return DBNull.Value
    End Function
#End Region

#Region "لیست"
    Private Sub LoadList()
        _dt = New DataTable()
        Using cn As New SqlConnection(ConnectionString)
            Using cm As SqlCommand = cn.CreateCommand()
                cm.CommandType = CommandType.StoredProcedure
                cm.CommandText = "[Sales].[spElamMarjoeeVorodKoli_SearchTitr]"
                cm.CommandTimeout = 999999
                cm.Parameters.AddWithValue("@ccElamMarjoee", DBNull.Value)
                cm.Parameters.AddWithValue("@CodeMahal", CodeMahalFaal)
                cm.Parameters.AddWithValue("@CodeDoreh", CInt(CodeDoreh))
                cm.Parameters.AddWithValue("@ShomarehElamMarjoee", DBNull.Value)
                cm.Parameters.AddWithValue("@ccMoshtary", DBNull.Value)
                cm.Parameters.AddWithValue("@ccForoshandeh", DBNull.Value)
                cm.Parameters.AddWithValue("@AzTarikh", DBNull.Value)
                cm.Parameters.AddWithValue("@TaTarikh", DBNull.Value)
                cm.Parameters.AddWithValue("@sVazeiatList", 0)
                'cm.Parameters.AddWithValue("@ShomarehElamMarjoee", If(txtShomareh.Text.Trim() = "", DBNull.Value, CObj(CInt(Val(txtShomareh.Text)))))
                'cm.Parameters.AddWithValue("@ccMoshtary", If(_ccMoshtary > 0, CObj(_ccMoshtary), DBNull.Value))
                'cm.Parameters.AddWithValue("@ccForoshandeh", If(Convert.ToInt32(cmbForoshandeh.SelectedValue) > 0, CObj(Convert.ToInt32(cmbForoshandeh.SelectedValue)), DBNull.Value))
                'cm.Parameters.AddWithValue("@AzTarikh", DateOrNull(mskAz))
                'cm.Parameters.AddWithValue("@TaTarikh", DateOrNull(mskTa))
                'cm.Parameters.AddWithValue("@sVazeiatList", VazeiatList())
                Using da As New SqlDataAdapter(cm)
                    da.Fill(_dt)
                End Using
            End Using
        End Using

        ' تاریخ با اسلش برای نمایش (اگر view خودش ستون Slash ندارد)
        If _dt.Columns.Contains("TarikhElamMarjoee") AndAlso Not _dt.Columns.Contains("TarikhElamMarjoeeSlash") _
           AndAlso Not _dt.Columns.Contains("TarikhSlash") Then
            _dt.Columns.Add("TarikhSlash", GetType(String))
            For Each r As DataRow In _dt.Rows
                Dim s As String = Convert.ToString(r("TarikhElamMarjoee")).Replace("/", "")
                r("TarikhSlash") = If(s.Length = 8, s.Substring(0, 4) & "/" & s.Substring(4, 2) & "/" & s.Substring(6, 2), s)
            Next
            _dt.AcceptChanges()
        End If

        With GridEXList
            .AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False
            .AllowAddNew = Janus.Windows.GridEX.InheritableBoolean.False
            .AllowDelete = Janus.Windows.GridEX.InheritableBoolean.False
            .SetDataBinding(_dt.DefaultView, "")
            .RetrieveStructure()
            For Each c As Janus.Windows.GridEX.GridEXColumn In .RootTable.Columns
                c.Visible = False
            Next
            .FilterMode = Janus.Windows.GridEX.FilterMode.Automatic        ' ردیف فیلتر؛ اگر خطا داد این خط را پاک کن
        End With

        ShowCol("RadifT", "ردیف", 45, 1)
        ShowCol("ShomarehElamMarjoee", "شماره", 80, 2)
        ShowCol("TarikhElamMarjoeeSlash", "تاریخ", 85, 3)
        ShowCol("TarikhSlash", "تاریخ", 85, 3)
        ShowCol("ShomarehFaktor", "شماره فاکتور", 90, 4)
        ShowCol("CodeMoshtary", "کد مشتری", 80, 5)
        ShowCol("NameMoshtary", "نام مشتری", 250, 6)
        ShowCol("NameForoshandeh", "فروشنده", 150, 7)
        ShowCol("NameMamorPakhsh", "مامور پخش", 150, 8)
        ShowCol("txtVazeiat", "وضعیت", 90, 9)
        ShowCol("Tozihat", "توضیحات", 250, 10)

        ' اگر اسم ستون‌های view فرق داشت: همه‌ی ستون‌ها (به‌جز شناسه‌ها) نشان داده شود
        Dim visibleCount As Integer = 0
        For Each c As Janus.Windows.GridEX.GridEXColumn In GridEXList.RootTable.Columns
            If c.Visible Then visibleCount += 1
        Next
        If visibleCount <= 2 Then
            For Each c As Janus.Windows.GridEX.GridEXColumn In GridEXList.RootTable.Columns
                If Not c.Key.StartsWith("cc", StringComparison.OrdinalIgnoreCase) Then
                    c.Visible = True
                    c.Caption = c.Key
                    c.Width = 110
                End If
            Next
        End If
    End Sub

    Private Sub ShowCol(ByVal key As String, ByVal caption As String, ByVal width As Integer, ByVal pos As Integer)
        If Not GridEXList.RootTable.Columns.Contains(key) Then Exit Sub
        Dim c As Janus.Windows.GridEX.GridEXColumn = GridEXList.RootTable.Columns(key)
        c.Caption = caption
        c.Visible = True
        c.Width = width
        c.Position = pos
        c.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
    End Sub
#End Region

#Region "انتخاب"
    Private Sub GridEXList_DoubleClick(ByVal sender As Object, ByVal e As EventArgs) Handles GridEXList.DoubleClick
        PickCurrent()
    End Sub

    Private Sub GridEXList_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles GridEXList.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.Handled = True
            PickCurrent()
        ElseIf e.KeyCode = Keys.Escape Then
            Me.DialogResult = DialogResult.Cancel
            Me.Close()
        End If
    End Sub

    Private Sub PickCurrent()
        Dim row As Janus.Windows.GridEX.GridEXRow = GridEXList.CurrentRow
        If row Is Nothing OrElse row.RowType <> Janus.Windows.GridEX.RowType.Record Then Exit Sub

        Dim v As Object = row.Cells("ccElamMarjoee").Value
        If v Is Nothing OrElse IsDBNull(v) Then Exit Sub

        _selectedId = Convert.ToInt32(v)
        Me.DialogResult = DialogResult.OK
        Me.Close()
    End Sub
#End Region

End Class