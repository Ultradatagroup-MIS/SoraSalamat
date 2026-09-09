Public Class frmAN_KalaBarCodeSearch
#Region " Variable AND Constant Declration "
    Friend cmForm As CurrencyManager
    Friend dv As DataView
    Friend dsTree As New DataSet
    Friend strDesc As New ArrayList
    Friend strValue As New ArrayList
    Public tBarCodeKala As String
    Public tCodeKala As String
    Public tccKala As Integer
    Public tNameKala As String
    Public tVahedShomaresh As String
    Dim gstrSql As String

    Public SearchItem As String
#End Region
#Region " Global Codes "
    'Public Sub KeypressEvent()
    '    If dv.Count <> 0 Then
    '        If dsTree.Tables(0).Columns.Contains("CodeKala") = True Then
    '            Me.tcodeKala = IIf(IsDBNull(dv(dgvSearch.CurrentRow.Index)("CodeKala")), "0", dv(dgvSearch.CurrentRow.Index)("CodeKala"))
    '        End If
    '        If dsTree.Tables(0).Columns.Contains("NameKala") = True Then
    '            Me.tNameKala = IIf(IsDBNull(dv(dgvSearch.CurrentRow.Index)("NameKala")), "", dv(dgvSearch.CurrentRow.Index)("NameKala"))
    '        End If
    '        If dsTree.Tables(0).Columns.Contains("sVahedeShomaresh") = True Then
    '            tVahedShomaresh = IIf(IsDBNull(dv(dgvSearch.CurrentRow.Index)("sVahedeShomaresh")), "", dv(dgvSearch.CurrentRow.Index)("sVahedeShomaresh"))
    '        End If
    '        If dsTree.Tables(0).Columns.Contains("ccKala") = True Then
    '            tccKala = IIf(IsDBNull(dv(dgvSearch.CurrentRow.Index)("ccKala")), "", dv(dgvSearch.CurrentRow.Index)("ccKala"))
    '        End If
    '    End If
    '    Me.Close()
    'End Sub
    Public Sub KeypressEvent()
        If MultiSelection Then
            Dim drv As DataRowView
            For Each drv In dv
                If drv("Selection") = True Then
                    If dsTree.Tables(0).Columns.Contains("BarCode") = True Then
                        tBarCodeKala += drv("BarCode") & ","
                    End If
                    If dsTree.Tables(0).Columns.Contains("CodeKala") = True Then
                        tCodeKala += drv("CodeKala") & ","
                    End If
                    If dsTree.Tables(0).Columns.Contains("NameKala") = True Then
                        tNameKala += drv("NameKala") & ","
                    End If
                    If dsTree.Tables(0).Columns.Contains("txtVahedeShomaresh") = True Then
                        tVahedShomaresh += drv("txtVahedeShomaresh") & ","
                    End If
                    If dsTree.Tables(0).Columns.Contains("ccKala") = True Then
                        tccKala += drv("ccKala") & ","
                    End If
                End If
            Next
        Else
            If dv.Count <> 0 Then
                If dsTree.Tables(0).Columns.Contains("BarCode") = True Then
                    Me.tBarCodeKala = IIf(IsDBNull(dv(dgvsearch.CurrentRow.Index)("BarCode")), "0", dv(dgvsearch.CurrentRow.Index)("BarCode"))
                End If
                If dsTree.Tables(0).Columns.Contains("CodeKala") = True Then
                    Me.tCodeKala = IIf(IsDBNull(dv(dgvsearch.CurrentRow.Index)("CodeKala")), "0", dv(dgvsearch.CurrentRow.Index)("CodeKala"))
                End If
                If dsTree.Tables(0).Columns.Contains("NameKala") = True Then
                    Me.tNameKala = IIf(IsDBNull(dv(dgvSearch.CurrentRow.Index)("NameKala")), "", dv(dgvSearch.CurrentRow.Index)("NameKala"))
                End If
                If dsTree.Tables(0).Columns.Contains("txtVahedeShomaresh") = True Then
                    tVahedShomaresh = IIf(IsDBNull(dv(dgvsearch.CurrentRow.Index)("txtVahedeShomaresh")), "", dv(dgvsearch.CurrentRow.Index)("txtVahedeShomaresh"))
                End If
                If dsTree.Tables(0).Columns.Contains("ccKala") = True Then
                    tccKala = IIf(IsDBNull(dv(dgvSearch.CurrentRow.Index)("ccKala")), "", dv(dgvSearch.CurrentRow.Index)("ccKala"))
                End If
            End If
        End If
        Me.Close()
    End Sub
    Private Sub SetComboFileds()
        cmbFldSearch.Items.Clear()
        cmbFldSearch.SelectedIndex = -1
        cmbFldSearch.SelectedIndex = -1
        strDesc.Clear() : strValue.Clear()

        If dsTree.Tables(0).Columns.Contains("NameKala") = True Then
            strDesc.Add("نام کالا")
            strValue.Add("NameKala")
            cmbFldSearch.Items.Add(strDesc(strDesc.Count - 1))
        End If
        If dsTree.Tables(0).Columns.Contains("BarCode") = True Then
            strDesc.Add("بار کــد کالا")
            strValue.Add("BarCode")
            cmbFldSearch.Items.Add(strDesc(strDesc.Count - 1))
        End If
        If dsTree.Tables(0).Columns.Contains("txtVahedeShomaresh") = True Then
            strDesc.Add("واحد شمارش")
            strValue.Add("sVahedeShomaresh")
            cmbFldSearch.Items.Add(strDesc(strDesc.Count - 1))
        End If
        If dsTree.Tables(0).Columns.Contains("Radif") = True Then
            strDesc.Add("ردیف")
            strValue.Add("Radif")
            cmbFldSearch.Items.Add(strDesc(strDesc.Count - 1))
        End If

        cmbFldSearch.SelectedIndex = 1

    End Sub
    Private Sub BoundCurrencyManager()
        cmForm = CType(BindingContext(dgvSearch.DataSource), CurrencyManager)
        AddHandler cmForm.ItemChanged, AddressOf cmForm_ItemChangedSatr
        AddHandler cmForm.PositionChanged, AddressOf cmForm_PositionChangedSatr
    End Sub
    Private Sub cmForm_ItemChangedSatr(ByVal sender As Object, ByVal e As ItemChangedEventArgs)
        If dsTree.Tables(0).Columns.Contains("NameKala") = True Then
            Me.Text = " جستجو در " & dv(dgvSearch.CurrentRow.Index)("NameKala")
        Else
            Me.Text = ""
        End If
    End Sub
    Private Sub cmForm_PositionChangedSatr(ByVal sender As Object, ByVal e As System.EventArgs)
        'If dsTree.Tables(0).Columns.Contains("NameKala") = True Then
        '    Me.Text = " جستجو در " & dv(dgvsearch.CurrentRow.Index)("NameKala")
        'Else
        '    Me.Text = ""
        'End If
    End Sub
    Private Sub LoadComboSearch()
        Dim Strsql As String = ""
        Dim daSql As SqlDataAdapter

        Strsql = "Select * From tblGL_ShenasehOmomi Where CodeAsli = 22 And CodeFarei <> 0"


        daSql = New SqlDataAdapter(Strsql, ConnectionString)
        If dsTree.Tables.Contains("cmbNoeSearch") Then
            dsTree.Tables.Remove("cmbNoeSearch")
        End If
        daSql.Fill(dsTree, "cmbNoeSearch")
        CmbNoeSearch.DataSource = Nothing
        CmbNoeSearch.Items.Clear()
        CmbNoeSearch.DataSource = dsTree.Tables("cmbNoeSearch").DefaultView
        CmbNoeSearch.DisplayMember = "Sharh"
        CmbNoeSearch.ValueMember = "Code"

        CmbNoeSearch.SelectedIndex = 0
    End Sub
    Private Sub SetTable(ByVal StrSql As String)
        Dim daSQL As SqlDataAdapter
        'StrSql &= " Order By Radif"
        daSQL = New SqlDataAdapter(StrSql, ConnectionString)
        If dsTree.Tables.Contains("Search") Then
            dsTree.Tables.Remove("Search")
        End If
        If MultiSelection Then
            dsTree.Tables("Search").Columns.Add("Selection", GetType(Boolean))
            Dim dr As DataRow
            For Each dr In dsTree.Tables("Search").Rows
                dr("Selection") = False
            Next
        End If
        daSQL.Fill(dsTree, "Search")
        daSQL = Nothing
    End Sub
    Private Sub LoadData(Optional ByVal StrShart As String = "")
        dv = New DataView(dsTree.Tables("Search"))
        dv.Sort = SearchItem
        dv.RowFilter = StrShart
        dv.AllowDelete = False
        dv.AllowNew = False
        SetGrid()
    End Sub
    Private Sub SetGrid()

        dgvsearch.DataSource = dv
        For i As Integer = 0 To dgvsearch.ColumnCount - 1
            dgvsearch.Columns(i).Visible = False
        Next
        If CType(dgvsearch.DataSource, DataView).Table.Columns.IndexOf("BarCode") <> -1 Then
            dgvsearch.Columns("BarCode").Width = 150
            dgvsearch.Columns("BarCode").HeaderText = "بار کــد کالا"
            dgvsearch.Columns("BarCode").Visible = True
            dgvsearch.Columns("BarCode").ReadOnly = True
        End If

        If CType(dgvsearch.DataSource, DataView).Table.Columns.IndexOf("NameKala") <> -1 Then
            dgvsearch.Columns("NameKala").Width = 200
            dgvsearch.Columns("NameKala").HeaderText = "نام کالا"
            dgvsearch.Columns("NameKala").Visible = True
            dgvsearch.Columns("NameKala").ReadOnly = True
        End If

        If CType(dgvsearch.DataSource, DataView).Table.Columns.IndexOf("txtVahedeShomaresh") <> -1 Then
            dgvsearch.Columns("txtVahedeShomaresh").Width = 130
            dgvsearch.Columns("txtVahedeShomaresh").HeaderText = "واحد شمارش"
            dgvsearch.Columns("txtVahedeShomaresh").Visible = True
            dgvsearch.Columns("txtVahedeShomaresh").ReadOnly = True
        End If

        If CType(dgvsearch.DataSource, DataView).Table.Columns.IndexOf("NameBrand") <> -1 Then
            dgvsearch.Columns("NameBrand").Width = 130
            dgvsearch.Columns("NameBrand").HeaderText = "نام برند"
            dgvsearch.Columns("NameBrand").Visible = True
            dgvsearch.Columns("NameBrand").ReadOnly = True
        End If

        With dgvsearch
            .Visible = True
            .RowHeadersWidth = 20
            .DataSource = dv
        End With
        BoundCurrencyManager()
        Me.CenterToScreen()

    End Sub
    Public Sub SetForm(ByVal StrSql As String)
        Dim Rowidx As Integer
        Me.txtSharhSearch.Text = ""
        gstrSql = StrSql
        SetTable(StrSql)
        LoadData()

        Select Case SearchItem
            Case Is = "BarCode"
                Rowidx = IIf(dv.Find(tBarCodeKala) < 0, 0, dv.Find(tBarCodeKala))
            Case Is = "ccKala"
                Rowidx = IIf(dv.Find(tccKala) < 0, 0, dv.Find(tccKala))
        End Select

        If dv.Count > 0 Then
            dgvsearch.Rows(Rowidx).Selected = True
            'SendKeys.Send("{tab}")

            If dsTree.Tables(0).Columns.Contains("NameKala") = True Then
                Me.Text = " جستجو در " & dv(dgvsearch.CurrentRow.Index)("NameKala")
            Else
                Me.Text = ""
            End If
        End If

        LoadComboSearch() : SetComboFileds()
    End Sub
    Private Function SetShart() As String
        Dim Str As String = ""
        If Type.GetTypeCode(dsTree.Tables("Search").Columns(strValue(cmbFldSearch.SelectedIndex)).DataType) = TypeCode.String Then
            Str = objSearch.SetSqlCritriaText(strValue(cmbFldSearch.SelectedIndex), Me.txtSharhSearch.Text.ToString.Replace("ي", "ی"), CmbNoeSearch.SelectedValue)
        End If
        If Type.GetTypeCode(dsTree.Tables("Search").Columns(strValue(cmbFldSearch.SelectedIndex)).DataType) = TypeCode.Double Then
            Str = objSearch.SetSqlCritriaNumber(strValue(cmbFldSearch.SelectedIndex), Val(txtSharhSearch.Text), Val(txtSharhSearch.Text), Val(txtSharhSearch.Text))
        End If
        If Type.GetTypeCode(dsTree.Tables("Search").Columns(strValue(cmbFldSearch.SelectedIndex)).DataType) = TypeCode.Int32 Then
            Str = objSearch.SetSqlCritriaNumber(strValue(cmbFldSearch.SelectedIndex), Val(txtSharhSearch.Text), Val(txtSharhSearch.Text), Val(txtSharhSearch.Text))
        End If
        Return Str
    End Function
#End Region
#Region " Events "
    Private Sub frmAN_KalaBarCodeSearch_Closed(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Closed
        dsTree = Nothing : dv = Nothing
    End Sub
    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        LoadData(SetShart())
    End Sub
    Private Sub txtSharhSearch_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtSharhSearch.KeyPress
        If e.KeyChar <> Chr(Keys.Enter) Then
            LoadData(SetShart())
        End If
    End Sub
    Private Sub txtSharhSearch_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSharhSearch.TextChanged
        LoadData(SetShart())
    End Sub
    Private Sub btnTaeed_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTaeed.Click
        KeypressEvent()
    End Sub
    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        SetForm(gstrSql)
    End Sub
    Private Sub dgvsearch_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dgvsearch.DoubleClick
        If Not MultiSelection Then KeypressEvent()
    End Sub
    Private Sub dgvsearch_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles dgvsearch.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.Handled = True
            KeypressEvent()
        End If
    End Sub
    Private Sub dgvsearch_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles dgvsearch.KeyPress
        'If e.KeyChar = Chr(13) Then
        'If Not MultiSelection Then KeypressEvent()
        If Convert.ToInt32(e.KeyChar) = 9 Then
            e.Handled = True
            txtSharhSearch.Select()
            dgvsearch.ClearSelection()
        End If
    End Sub
    Private Sub frmAN_KalaBarCodeSearch_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        objTools.ChangeKbd(UD_Dll.mdlUtility.KBD.Farsi)
        txtSharhSearch.Focus()
    End Sub
#End Region
End Class