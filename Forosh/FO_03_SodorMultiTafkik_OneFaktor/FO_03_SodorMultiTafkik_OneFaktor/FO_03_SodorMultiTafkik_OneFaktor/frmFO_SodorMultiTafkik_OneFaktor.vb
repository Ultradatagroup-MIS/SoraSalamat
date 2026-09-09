Public Class frmFO_SodorMultiTafkik_OneFaktor

#Region "Variable AND Constant Declration"
    Const cntCodeSubSystem As Long = 1000120
    Private SN As Integer
    Public dsForm As New DataSet
    Dim dvForm As DataView
    Public dvTitr As DataView
    Public dvSatr As DataView
    Public cmTitr As CurrencyManager
    Public cmSatr As CurrencyManager
    Public cmOdat, cmTaeed, cmElat As CurrencyManager
    Public SaveVaziatTaeed, SaveVaziatOdat As Integer
    Public VaziatLoadSanad As String
    Dim F As Boolean = True
    Dim Flag As Boolean = False
    Dim ErrPro As New ErrorProvider
    Dim txtCaption As String
    Private WithEvents BS As New UD_Dll.PassString
    Const posTaeed As Byte = 0
    Dim FirstInsert As Boolean = False
    Structure StrTaeedOdat
        Public strTaeed As String
        Public CTaeed As Integer
    End Structure
#End Region
#Region "Form Event Code"
    Private Sub frmFO_Brand_Closed(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Closed
        ' "Receive" parameter is the caption of destination window
        Dim hwnd As Long = UD_Dll.Code.FindWindow(vbNullString, ObjCode.GetNameSherkat)
        If hwnd <> 0 Then
            BS.PostString(hwnd, &H400, 0, txtCaption)
        End If

        dsForm = Nothing
        dvForm = Nothing
    End Sub
    Private Sub frmFO_PishFaktorBaresi_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetParameter()
        SN = objSec.GetSecurityNumber(cntCodeSubSystem, UserName)

        SaveVaziatTaeed = UD_Dll.Enums.FO_VaziatPishFaktor.TaeedShodeh
        SaveVaziatOdat = UD_Dll.Enums.FO_VaziatPishFaktor.TaeedNashodeh
        VaziatLoadSanad = UD_Dll.Enums.FO_VaziatPishFaktor.BedoneAmalyat

        Select Case ObjCode.CheckBargehTafkik()
            Case 2
                MsgBox("با توجه به تنظیمات انجام شده، شما قادر به صدور برگه تفکیک نیستید..", MsgBoxStyle.MsgBoxRtlReading Or MsgBoxStyle.MsgBoxRight Or MsgBoxStyle.Information, "ورود")
                Me.Close()
                Exit Sub
        End Select

        LoadCombo()
        ClearForm()
        Flag = True
        Search(True)
        cmbForoshandehS.Focus()
    End Sub
    Private Sub txtShomarehSanad_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtShomarehS.KeyPress
        If (Asc(e.KeyChar()) < 48 Or Asc(e.KeyChar()) > 57) And (Asc(e.KeyChar()) <> 8) Then
            e.Handled = True
        End If
    End Sub
    Private Sub frmFO_PishFaktorBaresi_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles MyBase.Paint
        Me.TopMost = True
    End Sub
    Private Sub cmbMashinTozie_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbMashinTozie.SelectedIndexChanged
        If Not Flag Then Exit Sub
        If cmbMashinTozie.SelectedIndex <> -1 And cmbMashinTozie.SelectedValue <> 0 Then
            Me.cmbRanandehTozie.SelectedValue = objTools.ConvertNulls(objTools.DLookup("CodeFard_Ranandeh", "tblFO_Mashin", "ccMashin = " & Me.cmbMashinTozie.SelectedValue), 0)
        End If
    End Sub
    Private Sub frmFO_PishFaktorBaresi_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.F1 Then
            Dim HelpWindow As New Forms_dll.frmGL_HelpWindow
            HelpWindow.CurrentCodeSubSystem = cntCodeSubSystem
            HelpWindow.Show()
            HelpWindow.TopMost = True
        ElseIf e.KeyCode = Keys.Enter Then
            SendKeys.Send("{tab}")
        End If
    End Sub
    Private Sub MaskSelect(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mskAzTarikh.Enter, mskAzTarikh.Click, mskTarikhPishbiniErsal.Enter, mskTarikhPishbiniErsal.Click, mskTaTarikh.Enter, mskTaTarikh.Click
        Try
            SendKeys.Send("{HOME}+{END}")
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->MaskSelect")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->MaskSelect")
        End Try
    End Sub
    Private Sub CheckIsNumeric(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles _
      txtShomarehS.KeyPress
        Try
            If (Asc(e.KeyChar()) < 46 Or Asc(e.KeyChar()) > 57) And (Asc(e.KeyChar()) <> 8) Then
                e.Handled = True
            End If
            If Asc(e.KeyChar()) = 47 Then
                e.Handled = True
            End If
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "Error IN DataBase ---->" & "CheckIsNumeric")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, "Error IN ---->" & "CheckIsNumeric")
        End Try
    End Sub
#End Region
#Region "Global Form Code"
    Private Sub LoadCombo()
        Dim cnSQL As New SqlConnection
        Dim cmSQL As New SqlCommand
        Dim daSQL As SqlDataAdapter
        Dim strSQL As String
        Dim dr As DataRow

        cnSQL = New SqlConnection(ConnectionString)
        cnSQL.Open()

        ' -------------------- Load Combo Anbar
        strSQL = "Global.spAnbar_LoadCombo "

        cmSQL = New SqlCommand(strSQL, cnSQL)
        cmSQL.CommandType = CommandType.StoredProcedure
        cmSQL.Parameters.Clear()

        cmSQL.Parameters.AddWithValue("CodeMahal", CodeMahalFaal)

        daSQL = New SqlDataAdapter(cmSQL)
        daSQL.Fill(dsForm, "tblAnbar")

        cmbAnbar.DataSource = Nothing
        cmbAnbar.Items.Clear()
        cmbAnbar.DataSource = dsForm.Tables("tblAnbar").DefaultView
        cmbAnbar.DisplayMember = "NameAnbar"
        cmbAnbar.ValueMember = "codeAnbar"
        cmbAnbar.SelectedIndex = -1

        cmSQL = Nothing

        ' -------------------- Load Mashin Tozie
        strSQL = "Global.spMashin_Tozie_LoadCombo "

        cmSQL = New SqlCommand(strSQL, cnSQL)
        cmSQL.CommandType = CommandType.StoredProcedure
        cmSQL.Parameters.Clear()

        cmSQL.Parameters.AddWithValue("CodeMahal", CodeMahalFaal)

        daSQL = New SqlDataAdapter(cmSQL)
        daSQL.Fill(dsForm, "tblMashin2")

        cmbMashinTozie.DataSource = Nothing
        cmbMashinTozie.Items.Clear()
        cmbMashinTozie.DataSource = dsForm.Tables("tblMashin2").DefaultView
        cmbMashinTozie.DisplayMember = "MashinFull"
        cmbMashinTozie.ValueMember = "ccMashin"
        cmbMashinTozie.SelectedIndex = -1
        cmbMashinTozie.SelectedIndex = -1

        cmSQL = Nothing

        ' -------------------- Load Combo Ranandeh Tozie
        strSQL = "Global.spRanandeh_Haml_Tozie_LoadCombo "

        cmSQL = New SqlCommand(strSQL, cnSQL)
        cmSQL.CommandType = CommandType.StoredProcedure
        cmSQL.Parameters.Clear()

        cmSQL.Parameters.AddWithValue("CodeMahal", CodeMahalFaal)
        cmSQL.Parameters.AddWithValue("sSemat", "," & UD_Dll.Enums.GL_Semat.MamorPakhsh & "," & UD_Dll.Enums.GL_Semat.Ranandeh & "," & UD_Dll.Enums.GL_Semat.Foroshandeh_Sayar & ",")

        daSQL = New SqlDataAdapter(cmSQL)
        daSQL.Fill(dsForm, "tblRanandeh2")

        cmbRanandehTozie.DataSource = Nothing
        cmbRanandehTozie.Items.Clear()
        cmbRanandehTozie.DataSource = dsForm.Tables("tblRanandeh2").DefaultView
        cmbRanandehTozie.DisplayMember = "FN"
        cmbRanandehTozie.ValueMember = "CodeFard"

        cmSQL = Nothing

        ' -------------------- Load Combo Mamor Pakhsh
        strSQL = "Global.spMamorPakhsh_LoadCombo "

        cmSQL = New SqlCommand(strSQL, cnSQL)
        cmSQL.CommandType = CommandType.StoredProcedure
        cmSQL.Parameters.Clear()

        cmSQL.Parameters.AddWithValue("CodeMahal", CodeMahalFaal)
        cmSQL.Parameters.AddWithValue("sSemat", "," & UD_Dll.Enums.GL_Semat.MamorPakhsh & "," & UD_Dll.Enums.GL_Semat.Ranandeh & "," & UD_Dll.Enums.GL_Semat.Foroshandeh_Sayar & ",")

        daSQL = New SqlDataAdapter(cmSQL)
        daSQL.Fill(dsForm, "tblMamorPakhsh")

        cmbMamorPakhsh.DataSource = Nothing
        cmbMamorPakhsh.Items.Clear()
        cmbMamorPakhsh.DataSource = dsForm.Tables("tblMamorPakhsh").DefaultView
        cmbMamorPakhsh.DisplayMember = "FN"
        cmbMamorPakhsh.ValueMember = "CodeFard"

        cmSQL = Nothing

        ' -------------------- Load Combo Foroshandeh
        strSQL = "Global.spForoshandeh_LoadCombo "

        cmSQL = New SqlCommand(strSQL, cnSQL)
        cmSQL.CommandType = CommandType.StoredProcedure
        cmSQL.Parameters.Clear()

        cmSQL.Parameters.AddWithValue("CodeMahal", CodeMahalFaal)
        cmSQL.Parameters.AddWithValue("sVazeiat", UD_Dll.Enums.FO_VaziatForoshandeh.NoFaal)
        cmSQL.Parameters.AddWithValue("UserName", UserName)

        daSQL = New SqlDataAdapter(cmSQL)
        daSQL.Fill(dsForm, "tblForoshandehS")

        dr = dsForm.Tables("tblForoshandehS").NewRow
        dr("NameForoshandeh") = "همه"
        dr("ccForoshandeh") = 0
        dsForm.Tables("tblForoshandehS").Rows.Add(dr)

        cmbForoshandehS.DataSource = Nothing
        cmbForoshandehS.Items.Clear()
        cmbForoshandehS.DataSource = dsForm.Tables("tblForoshandehS").DefaultView
        cmbForoshandehS.DisplayMember = "NameForoshandeh"
        cmbForoshandehS.ValueMember = "ccForoshandeh"
        cmbForoshandehS.SelectedValue = 0

        ' -------------------- Load Combo Ostan
        strSQL = "Global.spOstan_LoadCombo "

        cmSQL = New SqlCommand(strSQL, cnSQL)
        cmSQL.CommandType = CommandType.StoredProcedure
        cmSQL.Parameters.Clear()

        If dsForm.Tables.Contains("tbl_Ostan") = True Then
            dsForm.Tables.Remove("tbl_Ostan")
        End If

        daSQL = New SqlDataAdapter(cmSQL)
        daSQL.Fill(dsForm, "tbl_Ostan")

        'dr = dsForm.Tables("tbl_Ostan").NewRow()
        'dr("Code") = 0
        'dr("Sharh") = "----"
        'dsForm.Tables("tbl_Ostan").Rows.Add(dr)

        cmbOstan.DataSource = Nothing
        cmbOstan.Items.Clear()
        cmbOstan.DataSource = dsForm.Tables("tbl_Ostan").DefaultView
        cmbOstan.DisplayMember = "Sharh"
        cmbOstan.ValueMember = "Code"
        cmbOstan.SelectedValue = 0

        ' -------------------- Load Combo Elat
        Strsql = "Select * From tblGL_ShenasehOmomi Where CodeAsli=168 AND CodeFarei <> 0 AND Dideh=1  or Code = 0"
        daSQL = New SqlDataAdapter(Strsql, ConnectionString)
        daSQL.Fill(dsForm, "tblElat")

        cnSQL.Close()
    End Sub
    Private Sub ClearForm()
        cmbForoshandehS.SelectedValue = 0
        cmbAnbar.SelectedIndex = -1
        Me.cmbRanandehTozie.SelectedIndex = -1
        Me.cmbRanandehTozie.SelectedIndex = -1
        Me.cmbMamorPakhsh.SelectedIndex = -1
        Me.cmbMamorPakhsh.SelectedIndex = -1
        Me.cmbMashinTozie.SelectedIndex = -1
        Me.cmbMashinTozie.SelectedIndex = -1
        Me.mskTarikhPishbiniErsal.Text = objTarikh.Mi2Sh(Today) 'objTarikh.Mi2Sh(Today.AddDays(1))
        mskAzTarikh.Text = objTarikh.Mi2Sh(Today.AddDays(-5))
        mskTaTarikh.Text = TarikhEmrooz
        Me.txtShomarehS.Text = ""
        cmbForoshandehS.SelectedValue = 0
        rbFaktor.Checked = True
        rbPishFaktor.Checked = False
    End Sub
    Private Sub Search(ByVal WithCriteria As Boolean)
        lblJamTedad.Text = ""
        lblJamRial.Text = ""
        lblKarton.Text = ""
        Dim StrSql As String
        Dim WhereStr As String
        Dim strMasir As String = ""

        If chkMantagheh.CheckedItems.Count <> 0 Then
            If chkMasir.CheckedItems.Count = 0 Then
                If chkMasir.CheckedIndices.Count = 0 Then
                    For I As Integer = 0 To chkMasir.Items.Count - 1
                        chkMasir.SetItemChecked(I, True)
                    Next
                End If
            End If
        End If

        If chkMasir.CheckedItems.Count <> 0 Then
            For I As Integer = 0 To chkMasir.Items.Count - 1
                If chkMasir.GetItemChecked(I) Then
                    chkMasir.SelectedIndex = I
                    strMasir &= chkMasir.SelectedValue.ToString & ","
                End If
            Next
            strMasir = strMasir.Remove(strMasir.Length - 1, 1)
        End If

        StrSql = "SELECT  ROW_NUMBER() OVER(ORDER BY ccFaktorTitr ASC) AS Radif, * FROM qryFO_Faktor Where (ccTafkik=0 or ccTafkik is null) AND IsForoshGarm <> 1 AND "
        WhereStr = ""
        If WithCriteria Then
            If rbFaktor.Checked = True Then
                If txtShomarehS.Text.Length > 0 Then
                    WhereStr = "FaktorShomareh = " & txtShomarehS.Text & " AND "
                End If
                If mskAzTarikh.Text <> "" Then
                    WhereStr &= "FaktorTarikh >= '" & mskAzTarikh.Text & "' AND "
                End If
                If mskTaTarikh.Text <> "" Then
                    WhereStr &= "FaktorTarikh <= '" & mskTaTarikh.Text & "' AND "
                End If
            End If
            If rbPishFaktor.Checked = True Then
                If txtShomarehS.Text.Length > 0 Then
                    WhereStr = "PishFaktorShomareh = " & txtShomarehS.Text & " AND "
                End If
                If mskAzTarikh.Text <> "" Then
                    WhereStr &= "PishFaktorTarikh >= '" & mskAzTarikh.Text & "' AND "
                End If
                If mskTaTarikh.Text <> "" Then
                    WhereStr &= "PishFaktorTarikh <= '" & mskTaTarikh.Text & "' AND "
                End If
            End If

            If cmbForoshandehS.SelectedValue <> 0 Then
                WhereStr &= "ccForoshandeh = " & cmbForoshandehS.SelectedValue & " AND "
            End If
            If cmbAnbar.SelectedValue <> 0 Then
                WhereStr &= "ccAnbar = " & cmbAnbar.SelectedValue & " AND "
            End If
            If cmbShahr.SelectedValue <> 0 Then
                WhereStr &= "sShahr = " & cmbShahr.SelectedValue & " AND "
            End If
            If strMasir <> "" Then
                WhereStr &= " sMahaleh in (" & strMasir & ") AND "
            End If

        End If

        StrSql = StrSql & WhereStr

        StrSql &= " sVazeiat = 4261 AND "
        StrSql &= " CodeDoreh = " & CodeDoreh & " AND CodeMahal = " & CodeMahalFaal
        StrSql &= " AND Not exists (Select * From tblGL_SecurityData where namekarbar= '" & UserName & "' and "
        StrSql &= " CodeSubSystem = 628 and pk = qryFO_Faktor.ccForoshandeh)  "
        StrSql &= " AND "
        StrSql &= " Not exists (Select * From tblGL_SecurityData where namekarbar= '" & UserName & "' and "
        StrSql &= " CodeSubSystem = 614 and pk = qryFO_Faktor.ccMoshtary) "

        RefreshTitrdata(StrSql)
    End Sub
    Private Sub RefreshTitrdata(ByVal strSql As String)
        Dim daSQL As SqlDataAdapter

        If dsForm.Tables.Contains("HETitr") Then
            dsForm.Tables.Remove("HETitr")
        End If

        daSQL = New SqlDataAdapter(strSql, ConnectionString)
        daSQL.Fill(dsForm, "HETitr")
        '------------Adding Columns------------
        dsForm.Tables("HETitr").Columns.Add("Taeed", GetType(Boolean))
        '-----------------------------------------
        Dim dr As DataRow

        For Each dr In dsForm.Tables("HETitr").Rows
            dr("Taeed") = False
        Next

        dvTitr = New DataView(dsForm.Tables("HETitr"))
        dvTitr.Sort = "sShahr,sMantagheh,sMahaleh,FaktorShomareh Desc"

        dvTitr.AllowNew = False
        dvTitr.AllowDelete = False
        dvTitr.AllowEdit = True

        daSQL = Nothing
        SetTitrGrid()
        With dbgTitr
            .Visible = True
            .RowHeaderWidth = 20
            .AllowSorting = False
            .DataSource = Nothing
            .DataSource = dvTitr
        End With
        BoundCurrencyManagerTitr()
        Me.CenterToScreen()

    End Sub
    Private Sub RefreshSatrData()
        If dvTitr.Count = 0 Then
            dbgSatr.DataSource = Nothing
            Exit Sub
        End If
        Dim Strsql As String
        Dim daSQL As SqlDataAdapter

        If dsForm.Tables.Contains("HESatr") Then
            dsForm.Tables.Remove("HESatr")
        End If

        Strsql = "Select *,ROW_NUMBER() OVER(ORDER BY ccFaktorSatr  ASC) As  RadifShow"
        Strsql &= " From qryFO_FaktorSatr Where ccFaktorTitr = " & dvTitr(cmTitr.Position)("ccFaktorTitr")
        Strsql &= " AND IsJayezeh<>1 "

        daSQL = New SqlDataAdapter(Strsql, ConnectionString)
        daSQL.Fill(dsForm, "HESatr")

        dvSatr = New DataView(dsForm.Tables("HESatr"))
        dvSatr.Sort = "Radif ASC"

        dvSatr.AllowNew = False
        dvSatr.AllowDelete = False
        dvSatr.AllowEdit = True

        daSQL = Nothing
        SetSatrGrid()
        With dbgSatr
            .Visible = True
            .RowHeaderWidth = 20
            .AllowSorting = False
            .DataSource = Nothing
            .DataSource = dvSatr
        End With
        BoundCurrencyManagerSatr()
    End Sub
    Private Sub SetTitrGrid()
        Dim tableStyle As New DataGridTableStyle
        tableStyle.MappingName = "HETitr"
        tableStyle.RowHeaderWidth = 20
        tableStyle.AllowSorting = True
        dbgTitr.BorderStyle = BorderStyle.Fixed3D

        Dim TextCol09 As New DataGridBoolColumn
        With TextCol09
            .MappingName = "Taeed"
            .AllowNull = False
            .HeaderText = "تاييد"
            .Width = 30
            .ReadOnly = True
            .Alignment = HorizontalAlignment.Center
        End With
        tableStyle.GridColumnStyles.Add(TextCol09)

        Dim TextCol010 As New DataGridTextBoxColumn
        With TextCol010
            .MappingName = "Radif"
            .HeaderText = "ردیف"
            .Width = 50
            .ReadOnly = True
            .Alignment = HorizontalAlignment.Center
        End With
        tableStyle.GridColumnStyles.Add(TextCol010)

        Dim TextCol01 As New DataGridTextBoxColumn
        With TextCol01
            .MappingName = "FaktorShomareh"
            .HeaderText = "شماره فاکتور"
            .Width = 50
            .ReadOnly = True
            .Alignment = HorizontalAlignment.Center
        End With
        tableStyle.GridColumnStyles.Add(TextCol01)

        Dim TextCol03 As New DataGridTextBoxColumn
        With TextCol03
            .MappingName = "NameForoshandeh"
            .HeaderText = "فروشنده"
            .Width = 100
            .ReadOnly = True
            .Alignment = HorizontalAlignment.Left
        End With
        tableStyle.GridColumnStyles.Add(TextCol03)

        Dim TextCol030 As New DataGridTextBoxColumn
        With TextCol030
            .MappingName = "CodeMoshtary"
            .HeaderText = "کــد مشتری"
            .Width = 70
            .ReadOnly = True
            .Alignment = HorizontalAlignment.Left
        End With
        tableStyle.GridColumnStyles.Add(TextCol030)

        Dim TextCol04 As New DataGridTextBoxColumn
        With TextCol04
            .MappingName = "NameMoshtary"
            .HeaderText = "نام مشتری"
            .Width = 100
            .ReadOnly = True
            .Alignment = HorizontalAlignment.Left
        End With
        tableStyle.GridColumnStyles.Add(TextCol04)

        Dim TextCol47 As New DataGridTextBoxColumn
        With TextCol47
            .MappingName = "Address"
            .HeaderText = "آدرس"
            .Width = 320
            .ReadOnly = True
            .Alignment = HorizontalAlignment.Left
        End With
        tableStyle.GridColumnStyles.Add(TextCol47)

        Dim TextCol70 As New DataGridTextBoxColumn
        With TextCol70
            .MappingName = "Tozihat"
            .HeaderText = "توضیحات"
            .Width = 200
            .ReadOnly = True
            .Alignment = HorizontalAlignment.Left
        End With
        tableStyle.GridColumnStyles.Add(TextCol70)

        Dim TextCol27 As New DataGridTextBoxColumn
        With TextCol27
            .MappingName = "JamKol"
            .HeaderText = "مبلغ کل"
            .Width = 80
            .Format = "N"
            .ReadOnly = True
            .Alignment = HorizontalAlignment.Left
        End With
        tableStyle.GridColumnStyles.Add(TextCol27)

        Dim TextCol007 As New DataGridTextBoxColumn
        With TextCol007
            .MappingName = "txtMantagheh"
            .HeaderText = "نام منطقه"
            .Width = 100
            .ReadOnly = True
            .Alignment = HorizontalAlignment.Left
        End With
        tableStyle.GridColumnStyles.Add(TextCol007)

        Dim TextCol008 As New DataGridTextBoxColumn
        With TextCol008
            .MappingName = "txtMahaleh"
            .HeaderText = "نام محلـه"
            .Width = 100
            .ReadOnly = True
            .Alignment = HorizontalAlignment.Left
        End With
        tableStyle.GridColumnStyles.Add(TextCol008)

        Dim TextCol06 As New DataGridTextBoxColumn
        With TextCol06
            .MappingName = "txtNoePardakht"
            .HeaderText = "تسویه"
            .Width = 60
            .ReadOnly = True
            .Alignment = HorizontalAlignment.Left
        End With
        tableStyle.GridColumnStyles.Add(TextCol06)

        Dim TextCol140 As New DataGridTextBoxColumn
        With TextCol140
            .MappingName = "NameMasirFO"
            .HeaderText = "مسیر"
            .Width = 100
            .ReadOnly = True
            .Alignment = HorizontalAlignment.Left
        End With
        tableStyle.GridColumnStyles.Add(TextCol140)

        Dim TextCol14111 As New DataGridTextBoxColumn
        With TextCol14111
            .MappingName = "NameMasir"
            .HeaderText = "مسیر پخش"
            .Width = 110
            .ReadOnly = True
            .Alignment = HorizontalAlignment.Left
        End With
        tableStyle.GridColumnStyles.Add(TextCol14111)

        Dim TextCo222 As New DataGridTextBoxColumn
        With TextCo222
            .MappingName = "PishFaktorTarikhSlash"
            .HeaderText = "تاریخ پیش فاکتور"
            .Width = 100
            .ReadOnly = True
            .Alignment = HorizontalAlignment.Center
        End With
        tableStyle.GridColumnStyles.Add(TextCo222)


        Dim TextCol11 As New DataGridTextBoxColumn
        With TextCol11
            .MappingName = "PishFaktorShomareh"
            .HeaderText = "شماره پیش فاکتور"
            .Width = 100
            .ReadOnly = True
            .Alignment = HorizontalAlignment.Center
        End With
        tableStyle.GridColumnStyles.Add(TextCol11)

        Dim TextCol02 As New DataGridTextBoxColumn
        With TextCol02
            .MappingName = "FaktorTarikhSlash"
            .HeaderText = "تاريخ "
            .Width = 70
            .ReadOnly = True
            .Alignment = HorizontalAlignment.Center
        End With
        tableStyle.GridColumnStyles.Add(TextCol02)

        Dim TextCol0073 As New DataGridTextBoxColumn
        With TextCol0073
            .MappingName = "txtShahr"
            .HeaderText = "شهر"
            .Width = 100
            .ReadOnly = True
            .Alignment = HorizontalAlignment.Left
        End With
        tableStyle.GridColumnStyles.Add(TextCol0073)

        Dim TextCol0072 As New DataGridTextBoxColumn
        With TextCol0072
            .MappingName = "txtMantaghehShahrdary"
            .HeaderText = "منطقه شهرداری"
            .Width = 150
            .ReadOnly = True
            .Alignment = HorizontalAlignment.Left
        End With
        tableStyle.GridColumnStyles.Add(TextCol0072)

        Dim TextCol24 As New DataGridTextBoxColumn
        With TextCol24
            .MappingName = "Makan"
            .HeaderText = "مکان"
            .Width = 120
            .ReadOnly = True
            .Alignment = HorizontalAlignment.Left
        End With
        tableStyle.GridColumnStyles.Add(TextCol24)

        Dim TextCol05 As New DataGridTextBoxColumn
        With TextCol05
            .MappingName = "JamFaktor"
            .HeaderText = "جمع مبلغ"
            .Width = 85
            .Format = "N"
            .ReadOnly = True
            .Alignment = HorizontalAlignment.Left
        End With
        tableStyle.GridColumnStyles.Add(TextCol05)

        Dim TextCol07 As New DataGridTextBoxColumn
        With TextCol07
            .MappingName = "Takhfif"
            .HeaderText = "تخفیف"
            .Width = 100
            .Format = "N"
            .ReadOnly = False
            .Alignment = HorizontalAlignment.Left
        End With
        tableStyle.GridColumnStyles.Add(TextCol07)

        Dim TextCol070 As New DataGridTextBoxColumn
        With TextCol070
            .MappingName = "JamTakhfifKala"
            .HeaderText = "جمع تخفیف کالا"
            .Width = 100
            .Format = "N"
            .ReadOnly = True
            .Alignment = HorizontalAlignment.Left
        End With
        tableStyle.GridColumnStyles.Add(TextCol070)

        Dim TextCol071 As New DataGridTextBoxColumn
        With TextCol071
            .MappingName = "JamJayezeh"
            .HeaderText = "جمع جوائز"
            .Width = 100
            .Format = "N"
            .ReadOnly = True
            .Alignment = HorizontalAlignment.Left
        End With
        tableStyle.GridColumnStyles.Add(TextCol071)

        Dim TextCol13 As New DataGridTextBoxColumn
        With TextCol13
            .MappingName = "JamMalyatAvarez"
            .HeaderText = "مالیات و عوارض"
            .Width = 110
            .Format = "N"
            .ReadOnly = True
            .Alignment = HorizontalAlignment.Left
        End With
        tableStyle.GridColumnStyles.Add(TextCol13)

        Dim TextCol57 As New DataGridTextBoxColumn
        With TextCol57
            .MappingName = "Telephone"
            .HeaderText = "تلفن"
            .Width = 70
            .ReadOnly = True
            .Alignment = HorizontalAlignment.Left
        End With
        tableStyle.GridColumnStyles.Add(TextCol57)

        Dim TextCol507 As New DataGridTextBoxColumn
        With TextCol507
            .MappingName = "TarikhSlash"
            .HeaderText = "تاریخ"
            .Width = 70
            .ReadOnly = True
            .Alignment = HorizontalAlignment.Left
        End With
        tableStyle.GridColumnStyles.Add(TextCol507)

        Dim TextCol057 As New DataGridTextBoxColumn
        With TextCol057
            .MappingName = "Saat"
            .HeaderText = "ساعت"
            .Width = 70
            .ReadOnly = True
            .Alignment = HorizontalAlignment.Left
        End With
        tableStyle.GridColumnStyles.Add(TextCol057)

        Dim TextCol0570 As New DataGridTextBoxColumn
        With TextCol0570
            .MappingName = "txtNoeVorod"
            .HeaderText = "نوع ورود"
            .Width = 70
            .ReadOnly = True
            .Alignment = HorizontalAlignment.Left
        End With
        tableStyle.GridColumnStyles.Add(TextCol0570)

        Dim TextCol05700 As New DataGridTextBoxColumn
        With TextCol05700
            .MappingName = "UserName"
            .HeaderText = "نام کاربر"
            .Width = 100
            .ReadOnly = True
            .Alignment = HorizontalAlignment.Left
        End With
        tableStyle.GridColumnStyles.Add(TextCol05700)

        With dbgTitr
            .TableStyles.Clear()
            .TableStyles.Add(tableStyle)
        End With
        Me.CenterToScreen()
    End Sub
    Private Sub SetSatrGrid()
        Dim tableStyle As New DataGridTableStyle
        tableStyle.MappingName = "HESatr"
        tableStyle.RowHeaderWidth = 20
        tableStyle.AllowSorting = False
        dbgSatr.BorderStyle = BorderStyle.Fixed3D

        Dim TextCol01 As New DataGridTextBoxColumn
        With TextCol01
            .MappingName = "RadifShow"
            .HeaderText = "رديف"
            .Width = 50
            .ReadOnly = True
            .Alignment = HorizontalAlignment.Center
        End With
        tableStyle.GridColumnStyles.Add(TextCol01)
        Dim TextCol02 As New DataGridTextBoxColumn
        With TextCol02
            .MappingName = "NameKala"
            .HeaderText = "نام کالا"
            .Width = 200
            .ReadOnly = True
            .Alignment = HorizontalAlignment.Left
        End With
        tableStyle.GridColumnStyles.Add(TextCol02)

        Dim TextCol45 As New DataGridTextBoxColumn
        With TextCol45
            .MappingName = "Tedad3"
            .HeaderText = "تعداد"
            .Width = 90
            .Format = "N"
            .ReadOnly = False
            .Alignment = HorizontalAlignment.Left
        End With
        tableStyle.GridColumnStyles.Add(TextCol45)

        Dim TextCol422 As New DataGridTextBoxColumn
        With TextCol422
            .MappingName = "TedadKarton"
            .HeaderText = "تعداد کارتن"
            .Width = 90
            .Format = "N"
            .ReadOnly = False
            .Alignment = HorizontalAlignment.Left
        End With
        tableStyle.GridColumnStyles.Add(TextCol422)

        Dim TextCol50 As New DataGridTextBoxColumn
        With TextCol50
            .MappingName = "Fee"
            .HeaderText = "فی"
            .Width = 120
            .Format = "N"
            .ReadOnly = True
            .Alignment = HorizontalAlignment.Left
        End With
        tableStyle.GridColumnStyles.Add(TextCol50)

        Dim TextCol65 As New DataGridTextBoxColumn
        With TextCol65
            .MappingName = "MKol3"
            .HeaderText = "جمع مبلغ"
            .Width = 120
            .ReadOnly = True
            .Format = "N"
            .Alignment = HorizontalAlignment.Left
        End With
        tableStyle.GridColumnStyles.Add(TextCol65)

        Dim TextCol5000 As New DataGridTextBoxColumn
        With TextCol5000
            .MappingName = "DarsadTakhfif"
            .HeaderText = "درصد تخفیف"
            .Width = 70
            .ReadOnly = False
            .Format = "G"
            .Alignment = HorizontalAlignment.Left
        End With
        tableStyle.GridColumnStyles.Add(TextCol5000)

        Dim TextCol500 As New DataGridTextBoxColumn
        With TextCol500
            .MappingName = "TakhfifKala"
            .HeaderText = "تخفیف کالا"
            .Width = 100
            .ReadOnly = True
            .Format = "N"
            .Alignment = HorizontalAlignment.Left
        End With
        tableStyle.GridColumnStyles.Add(TextCol500)

        Dim TextCol512 As New DataGridTextBoxColumn
        With TextCol512
            .MappingName = "FeeKol"
            .HeaderText = "مبلغ نهایی"
            .Width = 120
            .ReadOnly = True
            .Format = "N"
            .Alignment = HorizontalAlignment.Left
        End With
        tableStyle.GridColumnStyles.Add(TextCol512)

        Dim TextCol5002 As New DataGridTextBoxColumn
        With TextCol5002
            .MappingName = "MablaghMalyat"
            .HeaderText = "مالیات"
            .Width = 100
            .ReadOnly = True
            .Format = "N"
            .Alignment = HorizontalAlignment.Left
        End With
        tableStyle.GridColumnStyles.Add(TextCol5002)

        Dim TextCol5010 As New DataGridTextBoxColumn
        With TextCol5010
            .MappingName = "MablaghAvarez"
            .HeaderText = "عوارض"
            .Width = 100
            .ReadOnly = True
            .Format = "N"
            .Alignment = HorizontalAlignment.Left
        End With
        tableStyle.GridColumnStyles.Add(TextCol5010)

        With dbgSatr
            .TableStyles.Clear()
            .TableStyles.Add(tableStyle)
        End With
        Me.CenterToScreen()

    End Sub
    Private Sub BoundCurrencyManagerTitr()
        cmTitr = CType(BindingContext(dbgTitr.DataSource), CurrencyManager)
        AddHandler cmTitr.PositionChanged, AddressOf cmTitr_PositionChanged
        RefreshSatrData()
    End Sub
    Private Sub BoundCurrencyManagerSatr()
        cmSatr = CType(BindingContext(dbgSatr.DataSource), CurrencyManager)
    End Sub
    Private Sub cmTitr_PositionChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        RefreshSatrData()
    End Sub
    Private Function InitSelect(ByVal cnSql As SqlConnection) As StrTaeedOdat
        Dim strShomarehSanad As String
        Dim drv As DataRowView
        Dim C As Integer

        'String For Taeed
        C = 0

        strShomarehSanad = " ccFaktorTitr in ("

        For Each drv In dvTitr
            If drv("Taeed") = True Then
                strShomarehSanad &= drv("ccFaktorTitr") & ","
                C += 1
            End If
        Next

        strShomarehSanad = Microsoft.VisualBasic.Mid(strShomarehSanad, 1, strShomarehSanad.Length - 1) & ")"


        InitSelect.CTaeed = C
        If C > 0 Then
            InitSelect.strTaeed = strShomarehSanad
        Else
            InitSelect.strTaeed = ""
        End If
    End Function
    Private Function CreateTafkikJozeTitr() As Long
        Dim tCodeCounter As Long
        Try
            CreateTafkikJozeTitr = 0

            Dim cnSQL As SqlConnection
            Dim cmSQL As SqlCommand
            Dim strSQL As String

            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()

            strSQL = "Insert Into tblFO_TafkikJoze "
            strSQL &= "(CodeMahal,CodeDoreh,ShomarehTafkik,TarikhTafkik,TarikhPishBiniErsal,ccMashin_Haml,"
            strSQL &= "ccRanandeh_Haml,ccMashin_Tozie,ccRanandeh_Tozie,ccMamorPakhsh,TarikhErsal,SaaRaft,"
            strSQL &= "SaaBargasht,sVazeiat,UserName,Tarikh,Saat) Values ("
            strSQL &= CodeMahalFaal & ","
            strSQL &= CodeDoreh & ","
            strSQL &= objTools.ConvertNulls(objTools.DMax("ShomarehTafkik", "tblFO_TafkikJoze", "CodeMahal = " & CodeMahalFaal & " AND CodeDoreh = " & CodeDoreh), 0) + 1 & ","
            strSQL &= "'" & TarikhEmrooz & "',"
            strSQL &= "'" & Me.mskTarikhPishbiniErsal.Text & "',"
            strSQL &= IIf(IsNothing(cmbMashinTozie.SelectedValue), 0, cmbMashinTozie.SelectedValue) & ","
            strSQL &= IIf(IsNothing(cmbRanandehTozie.SelectedValue), 0, cmbRanandehTozie.SelectedValue) & ","
            strSQL &= IIf(IsNothing(cmbMashinTozie.SelectedValue), 0, cmbMashinTozie.SelectedValue) & ","
            strSQL &= IIf(IsNothing(cmbRanandehTozie.SelectedValue), 0, cmbRanandehTozie.SelectedValue) & ","
            strSQL &= IIf(IsNothing(cmbMamorPakhsh.SelectedValue), 0, cmbMamorPakhsh.SelectedValue) & ","
            strSQL &= "'" & Me.mskTarikhPishbiniErsal.Text & "',"  'TarikhErsal
            strSQL &= "'" & "" & "',"  'SaaRaft
            strSQL &= "'" & "" & "',"  'SaaBargasht
            strSQL &= UD_Dll.Enums.FO_VazieatBargeTafkik.ErsalNashodeh & ","
            strSQL &= "'" & UserName & "',"
            strSQL &= "'" & TarikhEmrooz & "',"
            strSQL &= "'" & Format(TimeOfDay, "HH:mm:ss") & "')"

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.ExecuteNonQuery()
            tCodeCounter = objTools.GetScopeIdentity(cnSQL)

            cnSQL.Close()
            cmSQL = Nothing : cnSQL = Nothing

            Return tCodeCounter
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->CreateTafkikJoze")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->CreateTafkikJoze")
        End Try
    End Function
    Private Sub CreateTafkikJozeSatr(ByVal ccTitr As Long)
        Try

            Dim cnSQL As SqlConnection
            Dim cmSQL As SqlCommand
            Dim strSQL As String = ""

            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()

            ' Check Kardan Sys Config  baraye vared kardan ya nakardane Shomareh Bach Va Tarikh Tolid Va Engheza
            'Dar Jadvale FO_TafkikJozeSatr
            Select Case ObjCode.CheckMoshahedehBachInBargeTafkik
                Case UD_Dll.Enums.GL_SysConfigShowBachInBargeTafkik.Faal
                    strSQL = " Insert Into tblFO_TafkikJozeSatr(ccTafkikJoze,ccKala,Tedad,Tedad1,ShomarehSerial,"
                    strSQL &= "	TarikhTolid,TarikhEngheza)"

                    strSQL &= " Select " & ccTitr & " as ccTafkik,ccKala,Sum(Tedad3) as Tedad,Sum(Tedad3) as Tedad1,"
                    If ObjCode.CheckTarikhMasraf <> 2 Then
                        strSQL &= " (Select Top 1 Shomarehbach From dbo.tblGL_ShomarehBach "
                        strSQL &= " Where dbo.tblGL_ShomarehBach.ccKala = qryFO_FaktorTitrSatr.CcKala"
                        strSQL &= " and TarikhTolid="
                        strSQL &= " (select max(TarikhTolid) from dbo.tblGL_ShomarehBach "
                        strSQL &= " Where dbo.tblGL_ShomarehBach.ccKala = qryFO_FaktorTitrSatr.CcKala)),"

                        strSQL &= " (Select Max(TarikhTolid) From dbo.tblGL_ShomarehBach "
                        strSQL &= " Where dbo.tblGL_ShomarehBach.ccKala = qryFO_FaktorTitrSatr.CcKala),"

                        strSQL &= " (Select Top 1 TarikhEngheza From dbo.tblGL_ShomarehBach "
                        strSQL &= " Where dbo.tblGL_ShomarehBach.ccKala = qryFO_FaktorTitrSatr.CcKala"
                        strSQL &= " and TarikhTolid="
                        strSQL &= "(select max(TarikhTolid) from dbo.tblGL_ShomarehBach"
                        strSQL &= " Where dbo.tblGL_ShomarehBach.ccKala = qryFO_FaktorTitrSatr.CcKala))"
                    Else
                        strSQL &= 0 & "," & "''" & "," & "''"
                    End If

                    strSQL &= " From qryFO_FaktorTitrSatr "
                    strSQL &= " Group by ccTafkik,ccKala  "
                    strSQL &= "  having ccTafkik = " & ccTitr & " AND Sum(Tedad3) <> 0 "

                    cmSQL = New SqlCommand(strSQL, cnSQL)
                    cmSQL.ExecuteNonQuery()
                Case UD_Dll.Enums.GL_SysConfigShowBachInBargeTafkik.NoFaal

                    strSQL = "Insert Into tblFO_TafkikJozeSatr "
                    strSQL &= "(ccTafkikJoze,ccKala,Tedad,Tedad1,Mablagh,SumBTKF) "
                    strSQL &= " Select " & ccTitr & " as ccTafkik,ccKala,Sum(Tedad3) as Tedad,Sum(Tedad3) as Tedad1,sum(mkol3),sum(tedad3*BTKF)"
                    strSQL &= " From qryFO_FaktorTitrSatr Group by ccTafkik,ccKala "
                    strSQL &= " having ccTafkik = " & ccTitr & " AND Sum(Tedad3) <> 0 "
                    cmSQL = New SqlCommand(strSQL, cnSQL)
                    cmSQL.ExecuteNonQuery()

                Case Else
                    MsgBox("وارد منوی تنظیمات سیستم شده و تعیین کنید آیا می خواهید شماره بچ در برگه تفکیک نمایش داده شود یا خیر", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "توجه:")
            End Select

            cnSQL.Close()
            cmSQL = Nothing : cnSQL = Nothing

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->CreateTafkikJozeSatr")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->CreateTafkikJozeSatr")
        End Try
    End Sub
    Private Sub CreateTafkikJozeSatr2(ByVal ccTitr As Long)
        Try

            Dim cnSQL As SqlConnection
            Dim cmSQL As SqlCommand
            Dim strSQL As String

            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()

            strSQL = "Insert Into tblFO_TafkikJozeSatr2 "
            strSQL &= "(ccTafkikJoze,ccFaktorTitr) "
            strSQL &= " Select " & ccTitr & " as ccTafkik,ccFaktorTitr "
            strSQL &= " From qryFO_Faktor "
            strSQL &= " Where ccTafkik = " & ccTitr

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.ExecuteNonQuery()

            cnSQL.Close()
            cmSQL = Nothing : cnSQL = Nothing

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->CreateTafkikJozeSatr2")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->CreateTafkikJozeSatr2")
        End Try
    End Sub
    Private Function Taeed(ByVal cnSql As SqlConnection, ByVal Tablename As String) As Boolean
        Dim cmSQL As SqlCommand
        Dim strSql As String
        Dim Radif As Integer = 0
        Try
            Dim tCdoeTafkikTitr As Long = CreateTafkikJozeTitr()

            Dim drv As DataRowView

            For Each drv In dvTitr
                If objTools.ConvertNulls(objTools.DLookup("ccFaktorTitr", "tblFO_TafkikJozeSatr2", "ccFaktorTitr =" & drv("ccFaktorTitr")), 0) = 0 Then
                    If drv("Taeed") = True Then
                        strSql = "Update tblFO_Faktor Set "
                        strSql &= " ccTafkik = " & tCdoeTafkikTitr & ","
                        strSql &= " ccMamorPakhsh = " & IIf(IsNothing(cmbMamorPakhsh.SelectedValue), 0, cmbMamorPakhsh.SelectedValue) & ","
                        strSql &= " ccMashin_Haml = " & IIf(IsNothing(cmbMashinTozie.SelectedValue), 0, cmbMashinTozie.SelectedValue) & ","
                        strSql &= " ccMashin_Tozie = " & IIf(IsNothing(cmbMashinTozie.SelectedValue), 0, cmbMashinTozie.SelectedValue) & ","
                        strSql &= " ccRanandeh_Tozie = " & IIf(IsNothing(cmbRanandehTozie.SelectedValue), 0, cmbRanandehTozie.SelectedValue) & ","
                        strSql &= " ccRanandeh_Haml = " & IIf(IsNothing(cmbRanandehTozie.SelectedValue), 0, cmbRanandehTozie.SelectedValue)
                        strSql &= " Where ccFaktorTitr = " & drv("ccFaktorTitr")

                        cmSQL = New SqlCommand(strSql, cnSql)
                        cmSQL.ExecuteNonQuery()
                        cmSQL = Nothing

                        ' Insert To Log Table
                        ObjCode.SabteTaghirat(CodeMahalFaal, UD_Dll.Enums.GL_NoeTaghir.UpdateRecord, "tblFO_Faktor", drv("ccFaktorTitr"), drv("FaktorShomareh"), "تایید")
                    End If
                End If
            Next

            CreateTafkikJozeSatr(tCdoeTafkikTitr)
            CreateTafkikJozeSatr2(tCdoeTafkikTitr)

            MsgBox("عملیات صدور برگه تفکیک با موفقیت انجام شد." & vbCrLf & "شماره برگه تفکیک = " & objTools.DLookup("ShomarehTafkik", "tblFO_TafkikJoze", "ccTafkikJoze = " & tCdoeTafkikTitr), MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "صدور برگه تفکیک")

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->Sodor Tafkik")
        Finally
            cnSql.Close()
            cnSql = Nothing
        End Try
    End Function
    Private Function TaeedSanad() As Boolean
        Dim cnSQL As SqlConnection
        Dim stSelect As New StrTaeedOdat
        Dim TableName As String = "tblTaeed"

        TaeedSanad = False

        cnSQL = New SqlConnection(ConnectionString)
        cnSQL.Open()

        stSelect = InitSelect(cnSQL)

        If stSelect.CTaeed = 0 Then
            Return True
        End If
        If Not IsValidTaeed("All") Then
            Exit Function
        End If

        Taeed(cnSQL, TableName)
        Return True
    End Function
    Private Function IsValidTaeed(ByVal chk As String) As Boolean
        IsValidTaeed = False

        If chk = "cmbMashinTozie" Or chk = "All" Then
            If cmbMashinTozie.SelectedIndex = -1 And cmbMashinTozie.SelectedValue = 0 Then
                ErrPro.SetError(Me.cmbMashinTozie, "ماشین توزیع را وارد کنيد.")
                MsgBox("ماشین توزیع را وارد کنيد.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
                cmbMashinTozie.Focus()
                Exit Function
            End If
            ErrPro.SetError(Me.cmbMashinTozie, "")
        End If

        If chk = "cmbRanandehTozie" Or chk = "All" Then
            If cmbRanandehTozie.SelectedIndex = -1 And cmbRanandehTozie.SelectedValue = 0 Then
                ErrPro.SetError(Me.cmbMashinTozie, "راننده توزیع را وارد کنيد.")
                MsgBox("راننده توزیع را وارد کنيد.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
                cmbRanandehTozie.Focus()
                Exit Function
            End If
            ErrPro.SetError(Me.cmbRanandehTozie, "")
        End If

        If chk = "cmbMamorPakhsh" Or chk = "All" Then
            If cmbMamorPakhsh.SelectedIndex = -1 And cmbMamorPakhsh.SelectedValue = 0 Then
                ErrPro.SetError(Me.cmbMamorPakhsh, "مامور پخش را وارد کنيد.")
                MsgBox("مامور پخش را وارد کنيد.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
                cmbMamorPakhsh.Focus()
                Exit Function
            End If
            ErrPro.SetError(Me.cmbMamorPakhsh, "")
        End If

        If chk = "mskTarikhPishbiniErsal" Or chk = "All" Then
            If mskTarikhPishbiniErsal.Text.Length = 0 Then
                ErrPro.SetError(Me.mskTarikhPishbiniErsal, "تاریخ پیش بینی ارسال را وارد کنيد.")
                MsgBox("تاریخ پیش بینی ارسال را وارد کنيد.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
                mskTarikhPishbiniErsal.Focus()
                Exit Function
            Else
                If Not objTarikh.IsShDate(mskTarikhPishbiniErsal.Text.ToString) Then
                    mskTarikhPishbiniErsal.Focus()
                    Exit Function
                End If
                If mskTarikhPishbiniErsal.Text < TarikhEmrooz Then
                    MsgBox("تاریخ وارد شده از تاریخ امروز کوچکتر است.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
                    mskTarikhPishbiniErsal.Focus()
                    Exit Function
                End If
            End If
            ErrPro.SetError(Me.mskTarikhPishbiniErsal, "")
        End If

        Dim HajmMashin As Double = 0
        Dim VaznMashin As Double = 0
        Dim checkHajmKala As Boolean = False
        Dim checkVaznKala As Boolean = False

        checkHajmKala = objTools.ConvertNulls(objTools.DLookup("CheckHajmMashin", "tblGL_SysConfig", "CodeMahal =" & CodeMahalFaal), 0)
        If checkHajmKala Then
            HajmMashin = objTools.ConvertNulls(objTools.DLookup("Tol", "qryFO_Mashin", "ccMashin =" & cmbMashinTozie.SelectedValue), 1) * _
                                     objTools.ConvertNulls(objTools.DLookup("Arz", "qryFO_Mashin", "ccMashin =" & cmbMashinTozie.SelectedValue), 1) * _
                                         objTools.ConvertNulls(objTools.DLookup("Ertefa", "qryFO_Mashin", "ccMashin =" & cmbMashinTozie.SelectedValue), 1)
            If Val(lblHajm.Text) > HajmMashin Then
                MsgBox("حجم اجناس انتخاب شده از حجم ماشین بیشتر است." & vbCrLf & " حجم ماشین :" & HajmMashin, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
                Exit Function
            End If
        End If

        checkVaznKala = objTools.ConvertNulls(objTools.DLookup("CheckVaznMashin", "tblGL_SysConfig", "CodeMahal =" & CodeMahalFaal), 0)
        If checkVaznKala Then
            VaznMashin = objTools.ConvertNulls(objTools.DLookup("Vazn", "qryFO_Mashin", "ccMashin =" & cmbMashinTozie.SelectedValue), 1)
            If Val(lblJamVazn.Text) > VaznMashin Then
                VaznMashin = VaznMashin / 1000
                Dim JamVaznkala As Double = Convert.ToDouble(lblJamVazn.Text) / 1000
                MsgBox("وزن اجناس انتخاب شده از وزن قابل حمل ماشین بیشتر است." & vbCrLf & " وزن  قابل حمل ماشین : " & VaznMashin & " کیلوگرم " & vbCrLf & " جمع وزن کالاها  : " & JamVaznkala & " کیلوگرم ", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
                Exit Function
            End If
        End If

        Return True
    End Function
    Private Sub SUMJKol()
        lblJamTedad.Text = ""
        lblJamRial.Text = ""
        lblJamVazn.Text = ""
        lblHajm.Text = ""
        lblKarton.Text = ""
        lblTedadFaktor.Text = ""
        grbShowFaktor.Text = "تعداد فاکتورها انتخاب شده : " & 0

        Dim strSQL As String
        Dim drv As DataRowView
        Dim Count As Integer = 0

        Dim strCCFaktor As String = ""
        For Each drv In dvTitr
            If drv("Taeed") = True Then
                Count += 1
                strCCFaktor &= drv("ccFaktorTitr") & ","
            End If
        Next
        If strCCFaktor.Length > 0 Then
            If strCCFaktor.Substring(strCCFaktor.Length - 1, 1) = "," Then
                strCCFaktor = strCCFaktor.Remove(strCCFaktor.Length - 1, 1)
            End If
        Else
            Exit Sub
        End If

        strSQL = " Select ccKala,Sum(Tedad3) as Tedad"
        strSQL &= " From qryFO_FaktorTitrSatr"
        strSQL &= " where  ccFaktorTitr in (" & strCCFaktor & ")  "
        strSQL &= " Group by ccKala"

        Dim daSQL As SqlDataAdapter
        If dsForm.Tables.Contains("tblKalaSum") Then
            dsForm.Tables.Remove("tblKalaSum")
        End If
        daSQL = New SqlDataAdapter(strSQL, ConnectionString)
        daSQL.Fill(dsForm, "tblKalaSum")

        Dim vazn As Double = 0
        Dim Hajm As Double = 0
        Dim tedadKarton As Double = 0
        For Each drv In dsForm.Tables("tblKalaSum").DefaultView
            Hajm += objTools.ConvertNulls(objTools.DLookup("Tol", "tblAN_Kala", "ccKala =" & drv("ccKala")), 1) * _
                        objTools.ConvertNulls(objTools.DLookup("Arz", "tblAN_Kala", "ccKala =" & drv("ccKala")), 1) * _
                            objTools.ConvertNulls(objTools.DLookup("Ertefa", "tblAN_Kala", "ccKala =" & drv("ccKala")), 1) * _
                                 drv("Tedad") \ objTools.ConvertNulls(objTools.DLookup("TedadDarKarton", "tblAN_Kala", "ccKala =" & drv("ccKala")), 1)
            tedadKarton += drv("Tedad") \ objTools.ConvertNulls(objTools.DLookup("TedadDarKarton", "tblAN_Kala", "ccKala =" & drv("ccKala")), 1)
            vazn += objTools.DLookup("VaznKhales", "tblAN_Kala", "ccKala =" & drv("ccKala")) * drv("Tedad")
        Next

        lblHajm.Text = Hajm
        lblJamVazn.Text = vazn
        lblJamTedad.Text = dsForm.Tables("tblKalaSum").Compute("sum(tedad)", "")
        lblJamRial.Text = ObjCode.DigitSeprator(objTools.DSum("JamKol", "tblFO_Faktor", "ccFaktorTitr in (" & strCCFaktor & ")"))
        lblKarton.Text = tedadKarton
        lblTedadFaktor.Text = Count
        grbShowFaktor.Text = "تعداد فاکتورها انتخاب شده : " & Count

    End Sub
    Private Sub SetParameter()
        Dim commands As String = Microsoft.VisualBasic.Command()
        If commands.Length = 0 Then

            UserName = "administrator"
            UserPassWord = "66998833"
            NameMahalFaal = "تهران"
            CodeMahalFaal = "1"
            PersonelCode = "0"
            PersonelName = "Administrator"
            CodeDoreh = "1392"
            ObjCode.UserName = UserName

        Else

            UserName = commands.Split(";")(0)
            UserCode = commands.Split(";")(1)
            UserPassWord = commands.Split(";")(2)
            NameMahalFaal = commands.Split(";")(3)
            CodeMahalFaal = commands.Split(";")(4)
            PersonelCode = commands.Split(";")(5)
            PersonelName = commands.Split(";")(6)
            CodeDoreh = commands.Split(";")(7)
            txtCaption = commands.Split(";")(8)
            Me.Text = txtCaption

            ObjCode.UserName = UserName
            ObjCode.CodeDoreh = CodeDoreh
            ObjCode.CodeMahalFaal = CodeMahalFaal

        End If
    End Sub
#End Region
#Region "From Buttons"
    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub
    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Dim frm As New frmFO_SodorTafkik

        Dim strMasir As String = ""

        If chkMantagheh.CheckedItems.Count <> 0 Then
            If chkMasir.CheckedItems.Count = 0 Then
                If chkMasir.CheckedIndices.Count = 0 Then
                    For I As Integer = 0 To chkMasir.Items.Count - 1
                        chkMasir.SetItemChecked(I, True)
                    Next
                End If
            End If
        End If

        If chkMasir.CheckedItems.Count <> 0 Then
            For I As Integer = 0 To chkMasir.Items.Count - 1
                If chkMasir.GetItemChecked(I) Then
                    chkMasir.SelectedIndex = I
                    strMasir &= chkMasir.SelectedValue.ToString & ","
                End If
            Next
            strMasir = strMasir.Remove(strMasir.Length - 1, 1)
        End If

        Me.Hide()
        If rbPishFaktor.Checked Then
            frm.TypeSearch = False
        ElseIf rbFaktor.Checked Then
            frm.TypeSearch = True
        End If

        frm.AzTarikh = mskAzTarikh.Text
        frm.TaTarikh = mskTaTarikh.Text
        frm.ccForoshandeh = cmbForoshandehS.SelectedValue
        frm.ccAnbar = IIf(cmbAnbar.SelectedIndex = -1, 0, cmbAnbar.SelectedValue)
        frm.sShahr = cmbShahr.SelectedValue
        frm.strMahaleh = strMasir
        frm.ShowDialog()

        Me.Show()

        Search(True)
    End Sub
    Private Sub btnTaeed_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTaeed.Click
        If MsgBox("آيا از انتخاب خود مطمئن هستيد ؟", MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2 + MsgBoxStyle.Question + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.MsgBoxRight, "خطا") = MsgBoxResult.No Then
            Exit Sub
        End If
        If TaeedSanad() = False Then Exit Sub
        ClearForm()
        Search(True)
    End Sub

#End Region
#Region "Check Box In Grid"


    Private Sub ChngCkhState(ByVal mRow As Integer, ByVal mCol As Integer)
        If dvTitr.Count = 0 Then Exit Sub
        If mCol = posTaeed Then ' taeed
            If dbgTitr.Item(mRow, mCol) = False Then
                dvTitr(cmTitr.Position)("Taeed") = True
                dbgTitr.Item(mRow, mCol) = True
            Else
                dvTitr(cmTitr.Position)("Taeed") = False
                dbgTitr.Item(mRow, mCol) = False
            End If

            SUMJKol()
        End If
    End Sub
#End Region
#Region "Menu"
    'Private Sub MenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItem2.Click
    '    ' 784 frmFO_MoshtaryMoreInfo
    '    If UserName.ToUpper <> "ADMINISTRATOR" Then
    '        If Not ObjCode.CheckPermission(784) Then Exit Sub
    '    End If

    '    If cmTitr.Position <> -1 Then
    '        Dim objVazeiat As New Forms_dll.frmFO_MoshtaryMoreInfo
    '        objVazeiat.ccMoshtary = dvTitr(cmTitr.Position)("ccMoshtary")
    '        objVazeiat.ShowDialog()
    '    End If
    'End Sub
    'Private Sub MenuItem3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItem3.Click
    '    ' 868 frmAN_KalaMoreInfo
    '    If UserName.ToUpper <> "ADMINISTRATOR" Then
    '        If Not ObjCode.CheckPermission(868) Then Exit Sub
    '    End If

    '    If cmTitr.Position <> -1 Then
    '        Dim objVazeiat As New Forms_dll.frmAN_KalaMoreInfo
    '        objVazeiat.UserName = UserName
    '        objVazeiat.ccKala = dvSatr(cmSatr.Position)("ccKala")
    '        objVazeiat.ccMoshtary = dvTitr(cmTitr.Position)("ccMoshtary")
    '        objVazeiat.IsThereMoshtary = True
    '        objVazeiat.ShowDialog()
    '    End If
    'End Sub
    'Private Sub MenuItem4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItem4.Click
    '    If UserName.ToUpper <> "ADMINISTRATOR" Then
    '        If Not ObjCode.CheckPermission(930) Then Exit Sub
    '    End If

    '    If cmTitr.Position <> -1 Then
    '        Dim objJavayez As New Forms_dll.frmFO_MoshahedehJavayez
    '        objJavayez.ccTitr = dvTitr(cmTitr.Position)("ccFaktorTitr")
    '        objJavayez.NoeForm = 1
    '        objJavayez.ShowDialog()
    '    End If
    'End Sub
#End Region

    Private Sub cmbShahr_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbShahr.SelectedIndexChanged
        Dim Strsql As String
        Dim daSQL As New SqlDataAdapter

        If dsForm.Tables.Contains("tblMantagheh") Then
            dsForm.Tables("tblMantagheh").Clear()
        End If

        Strsql = "select sMantagheh,txtMantagheh from qryFO_MasirSatr AS MS LEFT OUTER JOIN"
        Strsql &= " tblFO_Masir AS M ON MS.ccMasir  = M.ccMasir"
        Strsql &= " WHERE sShahr = " & IIf(cmbShahr.SelectedIndex = 0 Or cmbShahr.SelectedIndex = -1, 0, cmbShahr.SelectedValue)
        Strsql &= " group by sMantagheh,txtMantagheh, sShahr"
        daSQL = New SqlDataAdapter(Strsql, ConnectionString)
        daSQL.Fill(dsForm, "tblMantagheh")
        chkMantagheh.DataSource = Nothing
        chkMantagheh.Items.Clear()
        chkMantagheh.DataSource = dsForm.Tables("tblMantagheh").DefaultView
        chkMantagheh.DisplayMember = "txtMantagheh"
        chkMantagheh.ValueMember = "sMantagheh"

        chkMasir.DataSource = Nothing

    End Sub

    Private Sub cmbOstan_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbOstan.SelectedIndexChanged
        Dim Strsql As String
        Dim daSQL As SqlDataAdapter
        Dim dr As DataRow

        Strsql = "Select * From tblGL_ShenasehOmomi "
        Strsql &= " Where CodeAsli = 23 And CodeFarei<>0 AND CodeLink = " & IIf(cmbOstan.SelectedIndex = 0, 0, cmbOstan.SelectedValue) & " Or Code = 0  order by Sharh"
        If dsForm.Tables.Contains("tbl_Shahr") = True Then
            dsForm.Tables.Remove("tbl_Shahr")
        End If
        daSQL = New SqlDataAdapter(Strsql, ConnectionString)
        daSQL.Fill(dsForm, "tbl_Shahr")
        dr = dsForm.Tables("tbl_Shahr").NewRow()
        dr("Code") = 0
        dr("Sharh") = "----"
        dsForm.Tables("tbl_Shahr").Rows.Add(dr)
        cmbShahr.DataSource = Nothing
        cmbShahr.Items.Clear()
        cmbShahr.DataSource = dsForm.Tables("tbl_Shahr").DefaultView
        cmbShahr.DisplayMember = "Sharh"
        cmbShahr.ValueMember = "Code"
        cmbShahr.SelectedValue = 0
    End Sub

    Private Sub btnNamayeshMasir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNamayeshMasir.Click
        Dim Strsql As String
        Dim strMantagheh As String = ""
        Dim daSQL As SqlDataAdapter

        If dsForm.Tables.Contains("tblMasir") Then
            dsForm.Tables.Remove("tblMasir")
        End If

        If chkMantagheh.CheckedItems.Count = 0 Then
            If chkMantagheh.CheckedIndices.Count = 0 Then
                For I As Integer = 0 To chkMantagheh.Items.Count - 1
                    chkMantagheh.SetItemChecked(I, True)
                Next
            End If
        End If

        If chkMantagheh.CheckedItems.Count <> 0 Then
            For I As Integer = 0 To chkMantagheh.Items.Count - 1
                If chkMantagheh.GetItemChecked(I) Then
                    chkMantagheh.SelectedIndex = I
                    strMantagheh &= chkMantagheh.SelectedValue.ToString & ","
                End If
            Next
            strMantagheh = strMantagheh.Remove(strMantagheh.Length - 1, 1)
        End If

        If strMantagheh = "" Then
            strMantagheh = "0"
        End If

        Strsql = "select M.ccMasir, NameMasir + ' -- ' + txtMantagheh +' -- ' + txtMahaleh  AS NameMasir, sMantagheh, sMahaleh from qryFO_MasirSatr AS MS LEFT OUTER JOIN"
        Strsql &= " tblFO_Masir AS M ON MS.ccMasir  = M.ccMasir"
        Strsql &= " where sMantagheh in (" & strMantagheh & ")"
        Strsql &= " group by M.ccMasir, NameMasir, sMantagheh, sMahaleh, txtMantagheh, txtMahaleh"
        daSQL = New SqlDataAdapter(Strsql, ConnectionString)
        daSQL.Fill(dsForm, "tblMasir")
        chkMasir.DataSource = Nothing
        chkMasir.Items.Clear()
        chkMasir.DataSource = dsForm.Tables("tblMasir").DefaultView
        chkMasir.DisplayMember = "NameMasir"
        chkMasir.ValueMember = "sMahaleh"

        strMantagheh = ""
    End Sub

    'Private Sub chk_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkSelectAll.CheckedChanged
    '    Dim drv As DataRowView


    '    If chkSelectAll.Checked = True Then
    '        For Each drv In dvTitr
    '            drv("Taeed") = True
    '        Next
    '    ElseIf chkSelectAll.Checked = False Then
    '        For Each drv In dvTitr
    '            drv("Taeed") = False
    '        Next
    '    End If

    'End Sub
End Class
