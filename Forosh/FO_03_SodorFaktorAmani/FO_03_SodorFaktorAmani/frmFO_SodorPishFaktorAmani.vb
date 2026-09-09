Public Class frmFO_SodorFaktorAmani
#Region "Variable AND Constant Declration"
    Const cntCodeSubSystem As Long = 1000121
    Dim Mode As UD_Dll.Enums.GL_ModeForms = UD_Dll.Enums.GL_ModeForms.None
    Dim cmTitr As CurrencyManager
    Dim cmSatr As CurrencyManager
    Dim dvTitr As DataView
    Dim dvSatr As DataView
    Dim ErrPro As New ErrorProvider
    Dim dsForm As New DataSet
    Dim dvForm As DataView
    Private SN As Integer
    Dim tPos As Integer
    Dim txtCaption As String
    Dim Flag As Integer = 0
    Private WithEvents BS As New UD_Dll.PassString
    Public SaveVaziatTaeed, SaveVaziatOdat As Integer
    Public VaziatLoadSanad As String
    Public ccAnbarMarjoee As Integer = 0
    Dim ccPishFaktorTitr As Integer = 0
    Dim PishFaktorShomareh As Integer = 0
    Dim flgTafkik As Boolean
    Dim TarikhFaktorAmani As Boolean
#End Region

    Private Sub frmFO_SodorFaktorAmani_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetParameter()
        SN = objSec.GetSecurityNumber(cntCodeSubSystem, UserName)
        SaveVaziatTaeed = UD_Dll.Enums.FO_VaziatPishFaktor.TaeedShodeh
        SaveVaziatOdat = UD_Dll.Enums.FO_VaziatPishFaktor.TaeedNashodeh
        VaziatLoadSanad = UD_Dll.Enums.FO_VaziatPishFaktor.BedoneAmalyat
        LoadCombo()
        ClearForm()
        Flag = True
        Search(True)
        cmbForoshandehS.Focus()
        grbPishFaktorSearch.Visible = True
        grbKalaSelect.Visible = False
        cmbNamayeshKala.SelectedIndex = 0
    End Sub
    Private Sub SetParameter()
        Dim commands As String = Microsoft.VisualBasic.Command()
        If commands.Length = 0 Then

            UserName = "Administrator"
            UserPassWord = "66998833"
            NameMahalFaal = "تهران"
            CodeMahalFaal = "2060"
            PersonelCode = "0"
            PersonelName = "Administrator"
            CodeDoreh = "1396"
            txtCaption = "صدور فاکتور امانی"
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
            objCode.UserName = UserName
        End If
    End Sub
    Private Sub LoadCombo()

        Dim Strsql As String
        Dim cnSQL As New SqlConnection
        Dim cmSQL As New SqlCommand
        Dim p As New SqlParameter
        Dim daSQL As SqlDataAdapter
        Dim dr As DataRow

        cnSQL = New SqlConnection(ConnectionString)
        cnSQL.Open()

        '----- Load Combo tblAnbar
        Strsql = "Global.spAnbarSalem_LoadCombo "

        cmSQL = New SqlCommand(Strsql, cnSQL)
        cmSQL.CommandType = CommandType.StoredProcedure
        cmSQL.Parameters.Clear()

        p = New SqlParameter("CodeMahal", SqlDbType.Int)
        p.Value = CType(CodeMahalFaal, Integer)
        cmSQL.Parameters.Add(p)
        'cmSQL.Parameters.AddWithValue("UserName", UserName)

        daSQL = New SqlDataAdapter(cmSQL)
        daSQL.Fill(dsForm, "tblAnbar")
        cmbAnbar.DataSource = Nothing
        cmbAnbar.Items.Clear()
        cmbAnbar.DataSource = dsForm.Tables("tblAnbar").DefaultView
        cmbAnbar.DisplayMember = "NameAnbar"
        cmbAnbar.ValueMember = "codeAnbar"

        daSQL = Nothing

        '----- Load Combo Foroshandeh
        Strsql = "Global.spForoshandeh_LoadCombo "

        cmSQL = New SqlCommand(Strsql, cnSQL)
        cmSQL.CommandType = CommandType.StoredProcedure
        cmSQL.Parameters.Clear()

        p = New SqlParameter("CodeMahal", SqlDbType.Int)
        p.Value = CType(CodeMahalFaal, Integer)
        cmSQL.Parameters.Add(p)

        p = New SqlParameter("sVazeiat", SqlDbType.Int)
        p.Value = UD_Dll.Enums.FO_VaziatForoshandeh.NoFaal
        cmSQL.Parameters.Add(p)


        p = New SqlParameter("UserName", SqlDbType.NVarChar, 20)
        p.Value = UserName
        cmSQL.Parameters.Add(p)

        daSQL = New SqlDataAdapter(cmSQL)
        daSQL.Fill(dsForm, "tblForoshandeh")

        dr = dsForm.Tables("tblForoshandeh").NewRow()
        dr("ccForoshandeh") = 0
        dr("NameForoshandeh") = "----"
        dsForm.Tables("tblForoshandeh").Rows.Add(dr)

        cmbForoshandehS.DataSource = Nothing
        cmbForoshandehS.Items.Clear()
        cmbForoshandehS.DataSource = dsForm.Tables("tblForoshandeh").DefaultView
        cmbForoshandehS.DisplayMember = "NameForoshandeh"
        cmbForoshandehS.ValueMember = "ccForoshandeh"

        daSQL = Nothing

        '-----  Load Combo Mamor Pakhsh
        Strsql = "Global.spMamorPakhsh_LoadCombo "

        cmSQL = New SqlCommand(Strsql, cnSQL)
        cmSQL.CommandType = CommandType.StoredProcedure
        cmSQL.Parameters.Clear()

        p = New SqlParameter("CodeMahal", SqlDbType.Int)
        p.Value = CType(CodeMahalFaal, Integer)
        cmSQL.Parameters.Add(p)

        p = New SqlParameter("sSemat", SqlDbType.NVarChar, 20)
        p.Value = "," & UD_Dll.Enums.GL_Semat.MamorPakhsh & "," & UD_Dll.Enums.GL_Semat.Ranandeh & "," & UD_Dll.Enums.GL_Semat.Foroshandeh_Sayar & ","
        cmSQL.Parameters.Add(p)

        daSQL = New SqlDataAdapter(cmSQL)
        daSQL.Fill(dsForm, "tblMamorPakhsh")
        cmbMamorPakhsh.DataSource = Nothing
        cmbMamorPakhsh.Items.Clear()
        cmbMamorPakhsh.DataSource = dsForm.Tables("tblMamorPakhsh").DefaultView
        cmbMamorPakhsh.DisplayMember = "FN"
        cmbMamorPakhsh.ValueMember = "CodeFard"

        cmSQL = Nothing : daSQL = Nothing
        cnSQL.Close()

        '----- Load Combo Namayesh Kala

        cmbNamayeshKala.Items.Add("نمایش کالاهای مانده دار")
        cmbNamayeshKala.Items.Add("نمایش کالاهای فروش رفته")
        cmbNamayeshKala.Items.Add("نمایش تمام کالاها")

    End Sub

#Region "Global Form Code"
    Private Sub ClearForm()
        If objTools.DLookup("BargehTafkik", "tblGL_SysConfig", "CodeMahal=" & CodeMahalFaal) = 1 Then
            cmbMamorPakhsh.Visible = False
            lblMamurpakhsh.Visible = False
            flgTafkik = False
        Else
            lblMamurpakhsh.Visible = True
            flgTafkik = True
        End If

        cmbForoshandehS.SelectedValue = 0
        mskAzTarikh.Text = objTarikh.Mi2Sh(Today.AddDays(-2))
        mskTaTarikh.Text = TarikhEmrooz
        Me.txtShomarehS.Text = ""
        cmbForoshandehS.SelectedValue = 0
        MskErsal.Text = TarikhEmrooz


        TarikhFaktorAmani = objTools.DLookup("ISNULL(TarikhFaktorAmani, 0) ", " tblGL_SysConfig", "CodeMahal = " & CodeMahalFaal & "")
        If TarikhFaktorAmani = False Then
            mskTarikhSodorFaktor.Text = TarikhEmrooz
            mskTarikhSodorFaktor.Enabled = False
        Else
            mskTarikhSodorFaktor.Enabled = True

        End If

        ErrPro.Dispose()
    End Sub
    Private Sub Search(ByVal WithCriteria As Boolean)
        Dim StrSql As String

        StrSql = "Sales.spSodorFaktorAmani_SearchTitr "
        RefreshTitrdata(StrSql)
    End Sub
    Private Sub RefreshTitrdata(ByVal strSql As String)
        Dim cnSQL As New SqlConnection
        Dim cmSQL As New SqlCommand
        Dim p As New SqlParameter
        Dim daSQL As SqlDataAdapter

        If dsForm.Tables.Contains("HETitr") Then
            dsForm.Tables.Remove("HETitr")
        End If

        cnSQL = New SqlConnection(ConnectionString)
        cnSQL.Open()

        cmSQL = New SqlCommand(strSql, cnSQL)
        cmSQL.CommandType = CommandType.StoredProcedure
        cmSQL.Parameters.Clear()

        p = New SqlParameter("CodeMahal", SqlDbType.Int)
        p.Value = CType(CodeMahalFaal, Integer)
        cmSQL.Parameters.Add(p)

        p = New SqlParameter("CodeDoreh", SqlDbType.Int)
        p.Value = CType(CodeDoreh, Integer)
        cmSQL.Parameters.Add(p)

        p = New SqlParameter("PishFaktorShomareh", SqlDbType.Int)
        p.Value = IIf(txtShomarehS.Text.Length > 0, txtShomarehS.Text, 0)
        cmSQL.Parameters.Add(p)

        p = New SqlParameter("AzTarikh", SqlDbType.NVarChar, 8)
        p.Value = IIf(mskAzTarikh.Text <> "", mskAzTarikh.Text, CodeDoreh & "0101")
        cmSQL.Parameters.Add(p)

        p = New SqlParameter("TaTarikh", SqlDbType.NVarChar, 8)
        p.Value = IIf(mskTaTarikh.Text <> "", mskTaTarikh.Text, TarikhEmrooz)
        cmSQL.Parameters.Add(p)

        p = New SqlParameter("ccForoshandeh", SqlDbType.Int)
        p.Value = IIf(cmbForoshandehS.SelectedValue <> 0, cmbForoshandehS.SelectedValue, 0)
        cmSQL.Parameters.Add(p)

        p = New SqlParameter("UserName", SqlDbType.NVarChar, 20)
        p.Value = UserName
        cmSQL.Parameters.Add(p)

        daSQL = New SqlDataAdapter(cmSQL)
        daSQL.Fill(dsForm, "HETitr")
        '------------Adding Columns------------
        'dsForm.Tables("HETitr").Columns.Add("Odat", GetType(Boolean))
        dsForm.Tables("HETitr").Columns.Add("Taeed", GetType(Boolean))
        '-----------------------------------------
        Dim dr As DataRow

        For Each dr In dsForm.Tables("HETitr").Rows
            dr("Taeed") = False
        Next

        dvTitr = New DataView(dsForm.Tables("HETitr"))
        dvTitr.Sort = "PishFaktorShomareh Desc"

        dvTitr.AllowNew = False
        dvTitr.AllowDelete = False
        dvTitr.AllowEdit = True

        cnSQL.Close()
        cmSQL = Nothing
        daSQL = Nothing
        SetTitrGrid()
        With GridEXTitr
            .Visible = True
            .DataSource = Nothing
            .DataSource = dvTitr
        End With
        BoundCurrencyManagerTitr()
        Me.CenterToScreen()
    End Sub
    Private Sub RefreshSatrData(ByVal ccPishFaktor As Integer, ByVal Noe As Integer)
        If dvTitr.Count = 0 Then
            GridEXSatr.DataSource = Nothing
            Exit Sub
        End If
        Dim Strsql As String
        Dim cnSQL As New SqlConnection
        Dim cmSQL As New SqlCommand
        Dim p As New SqlParameter
        Dim daSQL As SqlDataAdapter

        If dsForm.Tables.Contains("HESatr") Then
            dsForm.Tables.Remove("HESatr")
        End If

        Strsql = "Sales.spSodorFaktorAmani_SearchSatr "

        cnSQL = New SqlConnection(ConnectionString)
        cnSQL.Open()

        cmSQL = New SqlCommand(Strsql, cnSQL)
        cmSQL.CommandType = CommandType.StoredProcedure
        cmSQL.Parameters.Clear()

        p = New SqlParameter("ccPishFaktorTitr", SqlDbType.Int)
        p.Value = ccPishFaktor
        cmSQL.Parameters.Add(p)

        p = New SqlParameter("NoeSearch", SqlDbType.Int)
        p.Value = Noe
        cmSQL.Parameters.Add(p)

        daSQL = New SqlDataAdapter(cmSQL)
        daSQL.Fill(dsForm, "HESatr")

        dvSatr = New DataView(dsForm.Tables("HESatr"))
        dvSatr.Sort = "RadifShow ASC"

        dvSatr.AllowNew = False
        dvSatr.AllowDelete = False
        dvSatr.AllowEdit = True

        cnSQL.Close()
        cmSQL = Nothing
        daSQL = Nothing
        SetSatrGrid()

        With GridEXSatr
            .Visible = True
            .DataSource = Nothing
            .DataSource = dvSatr
        End With
        BoundCurrencyManagerSatr()
    End Sub
    Private Sub SetTitrGrid()
        With GridEXTitr
            .DataSource = Nothing
            .DataSource = dsForm.Tables("HETitr").DefaultView
            .SetDataBinding(dsForm.Tables("HETitr").DefaultView, "")
            .RetrieveStructure()
        End With

        For i As Integer = 0 To GridEXTitr.CurrentTable.Columns.Count - 1
            GridEXTitr.CurrentTable.Columns.Item(i).Visible = False
        Next


        'GridEXTitr.CurrentTable.Columns.Item("Taeed").Caption = "تاييد"
        'GridEXTitr.CurrentTable.Columns.Item("Taeed").Visible = True
        'GridEXTitr.CurrentTable.Columns.Item("Taeed").Width = 50
        'GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
        'GridEXTitr.CurrentTable.Columns.Item("Taeed").Position = 0
        'GridEXTitr.CurrentTable.Columns.Item("Taeed").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

        GridEXTitr.CurrentTable.Columns.Item("PishFaktorShomareh").Caption = "شماره"
        GridEXTitr.CurrentTable.Columns.Item("PishFaktorShomareh").Visible = True
        GridEXTitr.CurrentTable.Columns.Item("PishFaktorShomareh").Width = 50
        GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
        GridEXTitr.CurrentTable.Columns.Item("PishFaktorShomareh").Position = 0
        GridEXTitr.CurrentTable.Columns.Item("PishFaktorShomareh").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
        GridEXTitr.CurrentTable.Columns.Item("PishFaktorShomareh").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

        GridEXTitr.CurrentTable.Columns.Item("PishFaktorTarikhSlash").Caption = "تاريخ"
        GridEXTitr.CurrentTable.Columns.Item("PishFaktorTarikhSlash").Visible = True
        GridEXTitr.CurrentTable.Columns.Item("PishFaktorTarikhSlash").Width = 80
        GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
        GridEXTitr.CurrentTable.Columns.Item("PishFaktorTarikhSlash").Position = 1
        GridEXTitr.CurrentTable.Columns.Item("PishFaktorTarikhSlash").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
        GridEXTitr.CurrentTable.Columns.Item("PishFaktorTarikhSlash").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

        GridEXTitr.CurrentTable.Columns.Item("NameMoshtary").Caption = "نام مشتری"
        GridEXTitr.CurrentTable.Columns.Item("NameMoshtary").Visible = True
        GridEXTitr.CurrentTable.Columns.Item("NameMoshtary").Width = 150
        GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
        GridEXTitr.CurrentTable.Columns.Item("NameMoshtary").Position = 2
        GridEXTitr.CurrentTable.Columns.Item("NameMoshtary").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
        GridEXTitr.CurrentTable.Columns.Item("NameMoshtary").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

        GridEXTitr.CurrentTable.Columns.Item("NameForoshandeh").Caption = "نام فروشنده"
        GridEXTitr.CurrentTable.Columns.Item("NameForoshandeh").Visible = True
        GridEXTitr.CurrentTable.Columns.Item("NameForoshandeh").Width = 150
        GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
        GridEXTitr.CurrentTable.Columns.Item("NameForoshandeh").Position = 3
        GridEXTitr.CurrentTable.Columns.Item("NameForoshandeh").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
        GridEXTitr.CurrentTable.Columns.Item("NameForoshandeh").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

        GridEXTitr.CurrentTable.Columns.Item("txtNoePardakht").Caption = "تسویه"
        GridEXTitr.CurrentTable.Columns.Item("txtNoePardakht").Visible = True
        GridEXTitr.CurrentTable.Columns.Item("txtNoePardakht").Width = 50
        GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
        GridEXTitr.CurrentTable.Columns.Item("txtNoePardakht").Position = 4
        GridEXTitr.CurrentTable.Columns.Item("txtNoePardakht").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
        GridEXTitr.CurrentTable.Columns.Item("txtNoePardakht").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

        GridEXTitr.CurrentTable.Columns.Item("JamPishFaktor").Caption = "جمع مبلغ"
        GridEXTitr.CurrentTable.Columns.Item("JamPishFaktor").Visible = True
        GridEXTitr.CurrentTable.Columns.Item("JamPishFaktor").Width = 85
        GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
        GridEXTitr.CurrentTable.Columns.Item("JamPishFaktor").Position = 5
        GridEXTitr.CurrentTable.Columns.Item("JamPishFaktor").FormatString = "N"
        GridEXTitr.CurrentTable.Columns.Item("JamPishFaktor").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
        GridEXTitr.CurrentTable.Columns.Item("JamPishFaktor").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

        'GridEXTitr.CurrentTable.Columns.Item("Takhfif").Caption = "تخفیف روی پ ف"
        'GridEXTitr.CurrentTable.Columns.Item("Takhfif").Visible = True
        'GridEXTitr.CurrentTable.Columns.Item("Takhfif").Width = 110
        'GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
        'GridEXTitr.CurrentTable.Columns.Item("Takhfif").Position = 8
        'GridEXTitr.CurrentTable.Columns.Item("Takhfif").FormatString = "N"
        'GridEXTitr.CurrentTable.Columns.Item("Takhfif").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

        'GridEXTitr.CurrentTable.Columns.Item("JamTakhfifKala").Caption = "جمع تخفیف کالا"
        'GridEXTitr.CurrentTable.Columns.Item("JamTakhfifKala").Visible = True
        'GridEXTitr.CurrentTable.Columns.Item("JamTakhfifKala").Width = 100
        'GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
        'GridEXTitr.CurrentTable.Columns.Item("JamTakhfifKala").Position = 9
        'GridEXTitr.CurrentTable.Columns.Item("JamTakhfifKala").FormatString = "N"
        'GridEXTitr.CurrentTable.Columns.Item("JamTakhfifKala").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

        'GridEXTitr.CurrentTable.Columns.Item("JamJayezeh").Caption = "جمع جوائز"
        'GridEXTitr.CurrentTable.Columns.Item("JamJayezeh").Visible = True
        'GridEXTitr.CurrentTable.Columns.Item("JamJayezeh").Width = 100
        'GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
        'GridEXTitr.CurrentTable.Columns.Item("JamJayezeh").Position = 10
        'GridEXTitr.CurrentTable.Columns.Item("JamJayezeh").FormatString = "N"
        'GridEXTitr.CurrentTable.Columns.Item("JamJayezeh").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

        GridEXTitr.CurrentTable.Columns.Item("JamMalyatAvarez").Caption = "مالیات و عوارض"
        GridEXTitr.CurrentTable.Columns.Item("JamMalyatAvarez").Visible = True
        GridEXTitr.CurrentTable.Columns.Item("JamMalyatAvarez").Width = 110
        GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
        GridEXTitr.CurrentTable.Columns.Item("JamMalyatAvarez").Position = 6
        GridEXTitr.CurrentTable.Columns.Item("JamMalyatAvarez").FormatString = "N"
        GridEXTitr.CurrentTable.Columns.Item("JamMalyatAvarez").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
        GridEXTitr.CurrentTable.Columns.Item("JamMalyatAvarez").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

        GridEXTitr.CurrentTable.Columns.Item("MablaghKol").Caption = "مبلغ کل"
        GridEXTitr.CurrentTable.Columns.Item("MablaghKol").Visible = True
        GridEXTitr.CurrentTable.Columns.Item("MablaghKol").Width = 120
        GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
        GridEXTitr.CurrentTable.Columns.Item("MablaghKol").Position = 7
        GridEXTitr.CurrentTable.Columns.Item("MablaghKol").FormatString = "N"
        GridEXTitr.CurrentTable.Columns.Item("MablaghKol").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
        GridEXTitr.CurrentTable.Columns.Item("MablaghKol").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

        GridEXTitr.CurrentTable.Columns.Item("Address").Caption = "آدرس"
        GridEXTitr.CurrentTable.Columns.Item("Address").Visible = True
        GridEXTitr.CurrentTable.Columns.Item("Address").Width = 250
        GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
        GridEXTitr.CurrentTable.Columns.Item("Address").Position = 8
        GridEXTitr.CurrentTable.Columns.Item("Address").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
        GridEXTitr.CurrentTable.Columns.Item("Address").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

        GridEXTitr.CurrentTable.Columns.Item("Makan").Caption = "مکان"
        GridEXTitr.CurrentTable.Columns.Item("Makan").Visible = True
        GridEXTitr.CurrentTable.Columns.Item("Makan").Width = 200
        GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
        GridEXTitr.CurrentTable.Columns.Item("Makan").Position = 9
        GridEXTitr.CurrentTable.Columns.Item("Makan").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
        GridEXTitr.CurrentTable.Columns.Item("Makan").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

        GridEXTitr.CurrentTable.Columns.Item("Telephone").Caption = "تلفن"
        GridEXTitr.CurrentTable.Columns.Item("Telephone").Visible = True
        GridEXTitr.CurrentTable.Columns.Item("Telephone").Width = 70
        GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
        GridEXTitr.CurrentTable.Columns.Item("Telephone").Position = 10
        GridEXTitr.CurrentTable.Columns.Item("Telephone").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
        GridEXTitr.CurrentTable.Columns.Item("Telephone").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

        GridEXTitr.CurrentTable.Columns.Item("TarikhSlash").Caption = "تاریخ"
        GridEXTitr.CurrentTable.Columns.Item("TarikhSlash").Visible = True
        GridEXTitr.CurrentTable.Columns.Item("TarikhSlash").Width = 70
        GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
        GridEXTitr.CurrentTable.Columns.Item("TarikhSlash").Position = 11
        GridEXTitr.CurrentTable.Columns.Item("TarikhSlash").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
        GridEXTitr.CurrentTable.Columns.Item("TarikhSlash").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

        GridEXTitr.CurrentTable.Columns.Item("Saat").Caption = "ساعت"
        GridEXTitr.CurrentTable.Columns.Item("Saat").Visible = True
        GridEXTitr.CurrentTable.Columns.Item("Saat").Width = 70
        GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
        GridEXTitr.CurrentTable.Columns.Item("Saat").Position = 12
        GridEXTitr.CurrentTable.Columns.Item("Saat").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
        GridEXTitr.CurrentTable.Columns.Item("Saat").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

        GridEXTitr.CurrentTable.Columns.Item("txtNoeVorod").Caption = "نوع ورود"
        GridEXTitr.CurrentTable.Columns.Item("txtNoeVorod").Visible = True
        GridEXTitr.CurrentTable.Columns.Item("txtNoeVorod").Width = 70
        GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
        GridEXTitr.CurrentTable.Columns.Item("txtNoeVorod").Position = 13
        GridEXTitr.CurrentTable.Columns.Item("txtNoeVorod").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
        GridEXTitr.CurrentTable.Columns.Item("txtNoeVorod").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

        GridEXTitr.CurrentTable.Columns.Item("UserName").Caption = "نام کاربر"
        GridEXTitr.CurrentTable.Columns.Item("UserName").Visible = True
        GridEXTitr.CurrentTable.Columns.Item("UserName").Width = 100
        GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
        GridEXTitr.CurrentTable.Columns.Item("UserName").Position = 14
        GridEXTitr.CurrentTable.Columns.Item("UserName").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
        GridEXTitr.CurrentTable.Columns.Item("UserName").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

        GridEXTitr.CurrentTable.Columns.Item("Tozihat").Caption = "توضـیحات"
        GridEXTitr.CurrentTable.Columns.Item("Tozihat").Visible = True
        GridEXTitr.CurrentTable.Columns.Item("Tozihat").Width = 250
        GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
        GridEXTitr.CurrentTable.Columns.Item("Tozihat").Position = 15
        GridEXTitr.CurrentTable.Columns.Item("Tozihat").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
        GridEXTitr.CurrentTable.Columns.Item("Tozihat").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

        GridEXTitr.CurrentTable.Columns.Item("ccPishFaktorTitr").Caption = "ccPishFaktorTitr"
        GridEXTitr.CurrentTable.Columns.Item("ccPishFaktorTitr").Visible = False
        GridEXTitr.CurrentTable.Columns.Item("ccPishFaktorTitr").Width = 0
        GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
        GridEXTitr.CurrentTable.Columns.Item("ccPishFaktorTitr").Position = 16
        GridEXTitr.CurrentTable.Columns.Item("ccPishFaktorTitr").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

        For i As Integer = 0 To GridEXTitr.RootTable.Columns.Count - 1
            If GridEXTitr.RootTable.Columns(i).Type.IsValueType Then
                GridEXTitr.RootTable.Columns(i).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                GridEXTitr.RootTable.Columns(i).FormatMode = Janus.Windows.GridEX.FormatMode.UseIFormattable
                GridEXTitr.RootTable.Columns(i).FormatString = "N"
                GridEXTitr.RootTable.Columns(i).TotalFormatMode = Janus.Windows.GridEX.FormatMode.UseIFormattable
                GridEXTitr.RootTable.Columns(i).TotalFormatString = "N"
            End If
        Next

        Me.CenterToScreen()
    End Sub
    Private Sub SetSatrGrid()

        With GridEXSatr
            .DataSource = Nothing
            .DataSource = dsForm.Tables("HESatr").DefaultView
            .SetDataBinding(dsForm.Tables("HESatr").DefaultView, "")
            .RetrieveStructure()
        End With

        For i As Integer = 0 To GridEXSatr.CurrentTable.Columns.Count - 1
            GridEXSatr.CurrentTable.Columns.Item(i).Visible = False
        Next

        GridEXSatr.CurrentTable.Columns.Item("Taeed").Caption = "انتخاب"
        GridEXSatr.CurrentTable.Columns.Item("Taeed").Visible = True
        GridEXSatr.CurrentTable.Columns.Item("Taeed").Width = 20
        GridEXSatr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
        GridEXSatr.CurrentTable.Columns.Item("Taeed").Position = 0
        GridEXSatr.CurrentTable.Columns.Item("Taeed").Selectable = True
        GridEXSatr.CurrentTable.Columns.Item("Taeed").ActAsSelector = True
        GridEXSatr.CurrentTable.Columns.Item("Taeed").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

        GridEXSatr.CurrentTable.Columns.Item("RadifShow").Caption = "ردیف"
        GridEXSatr.CurrentTable.Columns.Item("RadifShow").Visible = True
        GridEXSatr.CurrentTable.Columns.Item("RadifShow").Width = 50
        GridEXSatr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
        GridEXSatr.CurrentTable.Columns.Item("RadifShow").Position = 1
        GridEXSatr.CurrentTable.Columns.Item("RadifShow").EditType = Janus.Windows.GridEX.EditType.NoEdit
        GridEXSatr.CurrentTable.Columns.Item("RadifShow").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
        GridEXSatr.CurrentTable.Columns.Item("RadifShow").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

        GridEXSatr.CurrentTable.Columns.Item("BarCodeKala").Caption = "بار کـد کالا"
        GridEXSatr.CurrentTable.Columns.Item("BarCodeKala").Visible = True
        GridEXSatr.CurrentTable.Columns.Item("BarCodeKala").Width = 120
        GridEXSatr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
        GridEXSatr.CurrentTable.Columns.Item("BarCodeKala").Position = 2
        GridEXSatr.CurrentTable.Columns.Item("BarCodeKala").EditType = Janus.Windows.GridEX.EditType.NoEdit
        GridEXSatr.CurrentTable.Columns.Item("BarCodeKala").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
        GridEXSatr.CurrentTable.Columns.Item("BarCodeKala").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

        GridEXSatr.CurrentTable.Columns.Item("CodeKala").Caption = "کـد کالا"
        GridEXSatr.CurrentTable.Columns.Item("CodeKala").Visible = True
        GridEXSatr.CurrentTable.Columns.Item("CodeKala").Width = 120
        GridEXSatr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
        GridEXSatr.CurrentTable.Columns.Item("CodeKala").Position = 3
        GridEXSatr.CurrentTable.Columns.Item("CodeKala").FormatString = "G"
        GridEXSatr.CurrentTable.Columns.Item("CodeKala").EditType = Janus.Windows.GridEX.EditType.NoEdit
        GridEXSatr.CurrentTable.Columns.Item("CodeKala").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
        GridEXSatr.CurrentTable.Columns.Item("CodeKala").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center


        GridEXSatr.CurrentTable.Columns.Item("NameKala").Caption = "نام کالا"
        GridEXSatr.CurrentTable.Columns.Item("NameKala").Visible = True
        GridEXSatr.CurrentTable.Columns.Item("NameKala").Width = 250
        GridEXSatr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
        GridEXSatr.CurrentTable.Columns.Item("NameKala").Position = 4
        GridEXSatr.CurrentTable.Columns.Item("NameKala").EditType = Janus.Windows.GridEX.EditType.NoEdit
        GridEXSatr.CurrentTable.Columns.Item("NameKala").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
        GridEXSatr.CurrentTable.Columns.Item("NameKala").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

        GridEXSatr.CurrentTable.Columns.Item("Tedad3").Caption = "تعداد در پیش فاکتور"
        GridEXSatr.CurrentTable.Columns.Item("Tedad3").Visible = True
        GridEXSatr.CurrentTable.Columns.Item("Tedad3").Width = 120
        GridEXSatr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
        GridEXSatr.CurrentTable.Columns.Item("Tedad3").Position = 5
        GridEXSatr.CurrentTable.Columns.Item("Tedad3").FormatString = "G"
        GridEXSatr.CurrentTable.Columns.Item("Tedad3").EditType = Janus.Windows.GridEX.EditType.NoEdit
        GridEXSatr.CurrentTable.Columns.Item("Tedad3").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
        GridEXSatr.CurrentTable.Columns.Item("Tedad3").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

        GridEXSatr.CurrentTable.Columns.Item("Fee").Caption = "فی"
        GridEXSatr.CurrentTable.Columns.Item("Fee").Visible = True
        GridEXSatr.CurrentTable.Columns.Item("Fee").Width = 100
        GridEXSatr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
        GridEXSatr.CurrentTable.Columns.Item("Fee").Position = 6
        GridEXSatr.CurrentTable.Columns.Item("Fee").FormatString = "N"
        GridEXSatr.CurrentTable.Columns.Item("Fee").EditType = Janus.Windows.GridEX.EditType.NoEdit
        GridEXSatr.CurrentTable.Columns.Item("Fee").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
        GridEXSatr.CurrentTable.Columns.Item("Fee").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

        GridEXSatr.CurrentTable.Columns.Item("MKol3").Caption = "جمع مبلغ"
        GridEXSatr.CurrentTable.Columns.Item("MKol3").Visible = True
        GridEXSatr.CurrentTable.Columns.Item("MKol3").Width = 120
        GridEXSatr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
        GridEXSatr.CurrentTable.Columns.Item("MKol3").Position = 7
        GridEXSatr.CurrentTable.Columns.Item("MKol3").FormatString = "N"
        GridEXSatr.CurrentTable.Columns.Item("MKol3").EditType = Janus.Windows.GridEX.EditType.NoEdit
        GridEXSatr.CurrentTable.Columns.Item("MKol3").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
        GridEXSatr.CurrentTable.Columns.Item("MKol3").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

        GridEXSatr.CurrentTable.Columns.Item("TedadEstefadehShodeh").Caption = "تعداد استفاده شده"
        GridEXSatr.CurrentTable.Columns.Item("TedadEstefadehShodeh").Visible = True
        GridEXSatr.CurrentTable.Columns.Item("TedadEstefadehShodeh").Width = 120
        GridEXSatr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOn
        GridEXSatr.CurrentTable.Columns.Item("TedadEstefadehShodeh").Position = 8
        GridEXSatr.CurrentTable.Columns.Item("TedadEstefadehShodeh").FormatString = "G"
        GridEXSatr.CurrentTable.Columns.Item("TedadEstefadehShodeh").EditType = Janus.Windows.GridEX.EditType.NoEdit
        GridEXSatr.CurrentTable.Columns.Item("TedadEstefadehShodeh").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
        GridEXSatr.CurrentTable.Columns.Item("TedadEstefadehShodeh").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

        GridEXSatr.CurrentTable.Columns.Item("TedadMandeh").Caption = "تعداد مانــده"
        GridEXSatr.CurrentTable.Columns.Item("TedadMandeh").Visible = True
        GridEXSatr.CurrentTable.Columns.Item("TedadMandeh").Width = 90
        GridEXSatr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOn
        GridEXSatr.CurrentTable.Columns.Item("TedadMandeh").Position = 9
        GridEXSatr.CurrentTable.Columns.Item("TedadMandeh").FormatString = "G"
        GridEXSatr.CurrentTable.Columns.Item("TedadMandeh").EditType = Janus.Windows.GridEX.EditType.NoEdit
        GridEXSatr.CurrentTable.Columns.Item("TedadMandeh").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
        GridEXSatr.CurrentTable.Columns.Item("TedadMandeh").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

        GridEXSatr.CurrentTable.Columns.Item("TedadForFaktor").Caption = "تعداد برای فاکتور"
        GridEXSatr.CurrentTable.Columns.Item("TedadForFaktor").Visible = True
        GridEXSatr.CurrentTable.Columns.Item("TedadForFaktor").Width = 110
        GridEXSatr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOn
        GridEXSatr.CurrentTable.Columns.Item("TedadForFaktor").Position = 10
        GridEXSatr.CurrentTable.Columns.Item("TedadForFaktor").FormatString = "G"
        GridEXSatr.CurrentTable.Columns.Item("TedadForFaktor").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
        GridEXSatr.CurrentTable.Columns.Item("TedadForFaktor").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

        'GridEXSatr.CurrentTable.Columns.Item("DarsadTakhfif").Caption = "درصد تخفیف"
        'GridEXSatr.CurrentTable.Columns.Item("DarsadTakhfif").Visible = True
        'GridEXSatr.CurrentTable.Columns.Item("DarsadTakhfif").Width = 70
        'GridEXSatr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
        'GridEXSatr.CurrentTable.Columns.Item("DarsadTakhfif").Position = 6
        'GridEXSatr.CurrentTable.Columns.Item("DarsadTakhfif").FormatString = "G"
        'GridEXSatr.CurrentTable.Columns.Item("DarsadTakhfif").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

        'GridEXSatr.CurrentTable.Columns.Item("TakhfifKala").Caption = "تخفیف کالا"
        'GridEXSatr.CurrentTable.Columns.Item("TakhfifKala").Visible = True
        'GridEXSatr.CurrentTable.Columns.Item("TakhfifKala").Width = 100
        'GridEXSatr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
        'GridEXSatr.CurrentTable.Columns.Item("TakhfifKala").Position = 7
        'GridEXSatr.CurrentTable.Columns.Item("TakhfifKala").FormatString = "N"
        'GridEXSatr.CurrentTable.Columns.Item("TakhfifKala").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

        'GridEXSatr.CurrentTable.Columns.Item("FeeKol").Caption = "مبلغ نهایی"
        'GridEXSatr.CurrentTable.Columns.Item("FeeKol").Visible = True
        'GridEXSatr.CurrentTable.Columns.Item("FeeKol").Width = 120
        'GridEXSatr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
        'GridEXSatr.CurrentTable.Columns.Item("FeeKol").Position = 8
        'GridEXSatr.CurrentTable.Columns.Item("FeeKol").FormatString = "N"
        'GridEXSatr.CurrentTable.Columns.Item("FeeKol").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

        GridEXSatr.CurrentTable.Columns.Item("MablaghMalyat").Caption = "مالیات"
        GridEXSatr.CurrentTable.Columns.Item("MablaghMalyat").Visible = True
        GridEXSatr.CurrentTable.Columns.Item("MablaghMalyat").Width = 100
        GridEXSatr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
        GridEXSatr.CurrentTable.Columns.Item("MablaghMalyat").Position = 11
        GridEXSatr.CurrentTable.Columns.Item("MablaghMalyat").FormatString = "N"
        GridEXSatr.CurrentTable.Columns.Item("MablaghMalyat").EditType = Janus.Windows.GridEX.EditType.NoEdit
        GridEXSatr.CurrentTable.Columns.Item("MablaghMalyat").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
        GridEXSatr.CurrentTable.Columns.Item("MablaghMalyat").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

        GridEXSatr.CurrentTable.Columns.Item("MablaghAvarez").Caption = "عوارض"
        GridEXSatr.CurrentTable.Columns.Item("MablaghAvarez").Visible = True
        GridEXSatr.CurrentTable.Columns.Item("MablaghAvarez").Width = 100
        GridEXSatr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
        GridEXSatr.CurrentTable.Columns.Item("MablaghAvarez").Position = 12
        GridEXSatr.CurrentTable.Columns.Item("MablaghAvarez").FormatString = "N"
        GridEXSatr.CurrentTable.Columns.Item("MablaghAvarez").EditType = Janus.Windows.GridEX.EditType.NoEdit
        GridEXSatr.CurrentTable.Columns.Item("MablaghAvarez").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
        GridEXSatr.CurrentTable.Columns.Item("MablaghAvarez").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

        GridEXTitr.CurrentTable.Columns.Item("Taeed").Caption = "انتخاب"
        GridEXTitr.CurrentTable.Columns.Item("Taeed").Visible = True
        GridEXTitr.CurrentTable.Columns.Item("Taeed").Width = 30
        GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
        GridEXTitr.CurrentTable.Columns.Item("Taeed").Position = 13
        GridEXTitr.CurrentTable.Columns.Item("Taeed").Selectable = True
        GridEXTitr.CurrentTable.Columns.Item("Taeed").ActAsSelector = True
        GridEXTitr.CurrentTable.Columns.Item("Taeed").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

        GridEXSatr.CurrentTable.Columns.Item("ccKala").Caption = "ccKala"
        GridEXSatr.CurrentTable.Columns.Item("ccKala").Visible = False
        GridEXSatr.CurrentTable.Columns.Item("ccKala").Width = 0
        GridEXSatr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOn
        GridEXSatr.CurrentTable.Columns.Item("ccKala").Position = 14
        GridEXSatr.CurrentTable.Columns.Item("ccKala").FormatString = "G"
        GridEXSatr.CurrentTable.Columns.Item("ccKala").EditType = Janus.Windows.GridEX.EditType.NoEdit
        GridEXSatr.CurrentTable.Columns.Item("ccKala").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
        GridEXSatr.CurrentTable.Columns.Item("ccKala").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

        GridEXSatr.CurrentTable.Columns.Item("IsSabadKala").Caption = "IsSabadKala"
        GridEXSatr.CurrentTable.Columns.Item("IsSabadKala").Visible = False
        GridEXSatr.CurrentTable.Columns.Item("IsSabadKala").Width = 0
        GridEXSatr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOn
        GridEXSatr.CurrentTable.Columns.Item("IsSabadKala").Position = 15
        GridEXSatr.CurrentTable.Columns.Item("IsSabadKala").FormatString = "G"
        GridEXSatr.CurrentTable.Columns.Item("IsSabadKala").EditType = Janus.Windows.GridEX.EditType.NoEdit
        GridEXSatr.CurrentTable.Columns.Item("IsSabadKala").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
        GridEXSatr.CurrentTable.Columns.Item("IsSabadKala").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

        For i As Integer = 0 To GridEXSatr.RootTable.Columns.Count - 1
            If GridEXSatr.RootTable.Columns(i).Type.IsValueType Then
                GridEXSatr.RootTable.Columns(i).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                GridEXSatr.RootTable.Columns(i).FormatMode = Janus.Windows.GridEX.FormatMode.UseIFormattable
                GridEXSatr.RootTable.Columns(i).FormatString = "N"
                GridEXSatr.RootTable.Columns(i).TotalFormatMode = Janus.Windows.GridEX.FormatMode.UseIFormattable
                GridEXSatr.RootTable.Columns(i).TotalFormatString = "N"
            End If
        Next

        Me.CenterToScreen()

    End Sub
    Private Sub BoundCurrencyManagerTitr()
        cmTitr = CType(BindingContext(GridEXTitr.DataSource), CurrencyManager)
    End Sub
    Private Sub BoundCurrencyManagerSatr()
        cmSatr = CType(BindingContext(GridEXSatr.DataSource), CurrencyManager)
    End Sub
#End Region
    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Search(True)
    End Sub
    Private Sub cmsVazeiatMoshtary_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmsVazeiatMoshtary.Click
        ' 784 frmFO_MoshtaryMoreInfo
        If UserName.ToUpper <> "ADMINISTRATOR" Then
            If Not objCode.CheckPermission(784) Then Exit Sub
        End If

        If cmTitr.Position <> -1 Then
            Dim objVazeiat As New Forms_dll.frmFO_MoshtaryMoreInfo
            objVazeiat.ccMoshtary = dvTitr(cmTitr.Position)("ccMoshtary")
            objVazeiat.ShowDialog()
        End If
    End Sub
    Private Sub cmsVModir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmsVModir.Click
        If cmTitr.Position <> -1 Then

            If objTools.DCount("ccFaktorTitr", "tblFO_Faktor", "ccPishFaktor = " & Val(GridEXTitr.CurrentRow.Cells("ccPishFaktorTitr").Text.Replace(",", ""))) <> 0 Then
                MsgBox("برای این پیش فاکتور، فاکتور صادر شده است، امکان تغییر وضعیت آن وجود ندارد . ", MsgBoxStyle.OkOnly + MsgBoxStyle.Information + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.MsgBoxRight, "خطا")
                Exit Sub
            End If

            If objCode.CheckPishFaktorTaeedModir = 1 Then
                If MsgBox("آیا مایلید این پیش فاکتور تغییر وضعیت پیدا کند؟      در صورت تایید پیش فاکتور مورد نظر را به فرم (پیش فاکتور تایید مدیر) باز خواهد گشت ", MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2 + MsgBoxStyle.Question + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.MsgBoxRight, "خطا") = MsgBoxResult.No Then
                    Exit Sub
                End If
                Try
                    Dim cnSQL As SqlConnection
                    Dim cmSQL As SqlCommand
                    Dim p As New SqlParameter
                    Dim strSqlTitr

                    cnSQL = New SqlConnection(ConnectionString)
                    cnSQL.Open()

                    strSqlTitr = "Sales.spPishFaktorBeFaktor_BazgashtBeFormhayeGhabl "

                    cmSQL = New SqlCommand(strSqlTitr, cnSQL)
                    cmSQL.CommandType = CommandType.StoredProcedure
                    cmSQL.Parameters.Clear()

                    p = New SqlParameter("sVazeiat", SqlDbType.SmallInt)
                    p.Value = UD_Dll.Enums.FO_VaziatPishFaktor.BedoneAmalyatTaeedModir
                    cmSQL.Parameters.Add(p)

                    p = New SqlParameter("ccPishFaktorTitr", SqlDbType.BigInt)
                    p.Value = Val(GridEXTitr.CurrentRow.Cells("ccPishFaktorTitr").Text.Replace(",", ""))
                    cmSQL.Parameters.Add(p)

                    cmSQL.ExecuteNonQuery()
                    Search(True)

                    cnSQL.Close()
                    cmSQL = Nothing : cnSQL = Nothing

                Catch sqlExc As SqlException
                    MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> تغییر به وضعیت کارتابل مدیر")
                Catch ex As Exception
                    MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> تغییر به وضعیت کارتابل مدیر")
                End Try
            End If
        End If
    End Sub
    Private Sub cmsVAvalieh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmsVAvalieh.Click
        If objTools.DCount("ccFaktorTitr", "tblFO_Faktor", "ccPishFaktor = " & Val(GridEXTitr.CurrentRow.Cells("ccPishFaktorTitr").Text.Replace(",", ""))) <> 0 Then
            MsgBox("برای این پیش فاکتور، فاکتور صادر شده است، امکان تغییر وضعیت آن وجود ندارد . ", MsgBoxStyle.OkOnly + MsgBoxStyle.Information + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.MsgBoxRight, "خطا")
            Exit Sub
        End If

        If MsgBox(" آیا مایلید این پیش فاکتور تغییر وضعیت پیدا کند؟     در صورت تایید پیش فاکتور مورد نظر را به فرم (پیش فاکتور) باز خواهد گشت ", MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2 + MsgBoxStyle.Question + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.MsgBoxRight, "خطا") = MsgBoxResult.No Then
            Exit Sub
        End If
        Try
            Dim cnSQL As SqlConnection
            Dim cmSQL As SqlCommand
            Dim p As New SqlParameter
            Dim strSqlTitr

            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()

            strSqlTitr = "Sales.spPishFaktorBeFaktor_BazgashtBeFormhayeGhabl "

            cmSQL = New SqlCommand(strSqlTitr, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            p = New SqlParameter("sVazeiat", SqlDbType.SmallInt)
            p.Value = -2
            cmSQL.Parameters.Add(p)

            p = New SqlParameter("ccPishFaktorTitr", SqlDbType.BigInt)
            p.Value = Val(GridEXTitr.CurrentRow.Cells("ccPishFaktorTitr").Text.Replace(",", ""))
            cmSQL.Parameters.Add(p)

            cmSQL.ExecuteNonQuery()
            Search(True)

            cnSQL.Close()
            cmSQL = Nothing : cnSQL = Nothing

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> تغییر به وضعیت پیش فاکتور")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> تغییر به وضعیت پیش فاکتور")
        End Try
    End Sub
    Private Sub cmsMarjoee_Click(sender As Object, e As EventArgs) Handles cmsMarjoee.Click
        If objTools.DCount("ccFaktorTitr", "tblFO_Faktor", "ccPishFaktor = " & Val(GridEXTitr.CurrentRow.Cells("ccPishFaktorTitr").Text.Replace(",", ""))) = 0 Then
            MsgBox("برای این پیش فاکتور، فاکتور صادر نشده است، امکان تغییر وضعیت آن وجود دارد . ", MsgBoxStyle.OkOnly + MsgBoxStyle.Information + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.MsgBoxRight, "خطا")
            Exit Sub
        End If

        Try
            If MsgBox(" آیا مایلید کالاهای مانده دار این پیش فاکتور مرجوع گردد ؟ ", MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2 + MsgBoxStyle.Question + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.MsgBoxRight, "خطا") = MsgBoxResult.No Then
                Exit Sub
            Else
                If MsgBox(" کالاهای مانده دار به انباری که پیش فاکتور از آن صادر گردیده است مرجوع خواهد شد . " & vbCrLf & " آیا مایلید انبار دیگری را جهت مرجوعی انتخاب نمایید ؟", MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2 + MsgBoxStyle.Question + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.MsgBoxRight, "خطا") = MsgBoxResult.No Then
                    ccAnbarMarjoee = objTools.DLookup("ccAnbar", "tblFO_PishFaktor", "ccPishFaktorTitr = " & Val(GridEXTitr.CurrentRow.Cells("ccPishFaktorTitr").Text.Replace(",", "")))
                Else
                    Dim frmAnbar As New frmFO_SelectAnbar

                    Me.Hide()
                    frmFO_SelectAnbar.ShowDialog()
                    Me.Show()

                    If ccAnbarMarjoee = 0 Then
                        Exit Sub
                    End If
                End If


            End If
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> مرجوع کردن کالاهای مانده دار پیش فاکتور")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> مرجوع کردن کالاهای مانده دار پیش فاکتور")
        End Try
    End Sub
    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub
    Private Sub btnExit2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit2.Click
        Me.Close()
    End Sub
    Private Sub btnNext_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNext.Click
        If dvTitr.Count <> 0 Then
            grbPishFaktorSearch.Visible = False
            grbKalaSelect.Visible = True

            ccPishFaktorTitr = Val(GridEXTitr.CurrentRow.Cells("ccPishFaktorTitr").Text.Replace(",", ""))
            PishFaktorShomareh = Val(GridEXTitr.CurrentRow.Cells("PishFaktorShomareh").Text.Replace(",", ""))
            RefreshSatrData(ccPishFaktorTitr, cmbNamayeshKala.SelectedIndex)
        End If
    End Sub
    Private Sub btnReturn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReturn.Click
        grbPishFaktorSearch.Visible = True
        grbKalaSelect.Visible = False

        ccPishFaktorTitr = 0
        PishFaktorShomareh = 0
        cmbNamayeshKala.SelectedIndex = 0
    End Sub
    Private Sub btnSodorFaktorAmani_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSodorFaktorAmani.Click

        Dim Flg1Row As Boolean = False

        If cmbNamayeshKala.SelectedIndex <> 0 Then
            MsgBox("صدور فاکتور تنها در حالت نمایش کالاهای مانده دار امکان پذیر است .", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "خطا")
            Exit Sub
        End If

        'If GridEXSatr.GetCheckedRows().Length = 0 Then
        '    For i As Integer = 0 To GridEXSatr.RowCount - 1
        '        GridEXSatr.GetCheckedRows(i).CheckState = Janus.Windows.GridEX.RowCheckState.Checked
        '    Next
        'Else
        '    Flg1Row = True
        'End If

        'If Flg1Row = False Then
        If GridEXSatr.GetCheckedRows().Length = 0 Then
            Dim RowsEditedCount As Integer = 0
            'For i As Integer = 0 To GridEXSatr.GetCheckedRows().Length - 1
            For i As Integer = 0 To GridEXSatr.RowCount - 1
                'If Val(GridEXSatr.GetCheckedRows(i).Cells("TedadForFaktor").Text.Replace(",", "")) > 0 Then
                If Val(GridEXSatr.GetRows(i).Cells("TedadForFaktor").Text.Replace(",", "")) > 0 Then
                    RowsEditedCount += 1
                End If
            Next

            If RowsEditedCount = 0 Then
                MsgBox("تعدادی جهت صدور فاکتور وارد نشده است . لطفا بررسی نمایید .", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "خطا")
                Exit Sub
            End If

            For i As Integer = 0 To GridEXSatr.RowCount - 1
                If Val(GridEXSatr.GetRows(i).Cells("TedadForFaktor").Text.Replace(",", "")) > Val(GridEXSatr.GetRows(i).Cells("TedadMandeh").Text.Replace(",", "")) Then
                    MsgBox("تعداد وارد شده برای کالای « " & GridEXSatr.GetRows(i).Cells("NameKala").Text & " » بیشتر از تعداد مانده این کالا می باشد، اصلاح نمایید .", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "خطا")
                    Exit Sub
                End If
            Next

        Else

            If Val(GridEXSatr.CurrentRow.Cells("TedadForFaktor").Text.Replace(",", "")) = 0 Then
                MsgBox("تعدادی جهت صدور فاکتور وارد نشده است . لطفا بررسی نمایید .", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "خطا")
                Exit Sub
            End If

            If Val(GridEXSatr.CurrentRow.Cells("TedadForFaktor").Text.Replace(",", "")) > Val(GridEXSatr.CurrentRow.Cells("TedadMandeh").Text.Replace(",", "")) Then
                MsgBox("تعداد وارد شده برای کالای « " & GridEXSatr.CurrentRow.Cells("NameKala").Text & " » بیشتر از تعداد مانده این کالا می باشد، اصلاح نمایید .", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "خطا")
                Exit Sub
            End If

        End If

        If MsgBox("آيا از انتخاب خود مطمئن هستيد ؟", MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2 + MsgBoxStyle.Question + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.MsgBoxRight, "تایید") = MsgBoxResult.No Then
            Exit Sub
        End If

        If mskTarikhSodorFaktor.Text = "" Then
            ErrPro.SetError(Me.mskTarikhSodorFaktor, "تاریخ صدور فاکتور را وارد کنید.")
            MsgBox("تاریخ صدور فاکتور را وارد کنید.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
            mskTarikhSodorFaktor.Focus()
            Exit Sub
        End If

        If Len(mskTarikhSodorFaktor.Text.ToString) <> 0 Then
            If Not objTarikh.IsShDate(mskTarikhSodorFaktor.Text.ToString) Then
                mskTarikhSodorFaktor.Focus()
                Exit Sub
            End If

            If TarikhFaktorAmani = True Then
                If mskTarikhSodorFaktor.Text < objTarikh.DecDay(TarikhEmrooz, 30) Then
                    MsgBox("تاریخ صدور فاکتور بیشتر از 30 روز می باشد  .", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
                    mskTarikhSodorFaktor.Focus()
                    Exit Sub
                End If
            End If

            If Microsoft.VisualBasic.Left(mskTarikhSodorFaktor.Text, 4) <> mdlPublic.CodeDoreh Then
                MsgBox("تاريخ مورد نظر با دوره انتخاب شده مغايرت دارد.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Critical, "خطا")
                ErrPro.SetError(mskTaTarikh, "تاريخ مورد نظر با دوره انتخاب شده مغايرت دارد.")
                Exit Sub
            End If
            If TarikhEmrooz < mskTarikhSodorFaktor.Text Then
                MsgBox("تاریخ صدور فاکتور از تاریخ روز جلوتر است.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Critical, "خطا")
                ErrPro.SetError(mskTaTarikh, "تاریخ صدور فاکتور از تاریخ روز جلوتر است.")
                Exit Sub
            End If
        Else
            ErrPro.SetError(Me.mskTarikhSodorFaktor, " تاریخ را وارد کنید.")
            MsgBox(" تاریخ را وارد کنید.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
            mskTarikhSodorFaktor.Focus()
            Exit Sub
        End If
        ErrPro.SetError(Me.mskTarikhSodorFaktor, "")

        If cmbAnbar.SelectedValue = 0 Then
            ErrPro.SetError(Me.cmbAnbar, "انبار را انتخاب کنید.")
            MsgBox("انبار را انتخاب کنید.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
            cmbAnbar.Focus()
            Exit Sub
        End If

        If TaeedSanad() = False Then Exit Sub
        ClearForm()
        RefreshSatrData(ccPishFaktorTitr, 0)
    End Sub
    Private Function TaeedSanad() As Boolean
        Dim cnSQL As SqlConnection
        Dim TableName As String = "tblTaeed"

        TaeedSanad = False

        cnSQL = New SqlConnection(ConnectionString)
        cnSQL.Open()

        Taeed(cnSQL, TableName)
        Return True
    End Function
    Private Function Taeed(ByVal cnSql As SqlConnection, ByVal Tablename As String) As Boolean
        Dim Radif As Integer = 0
        Dim AllowPishFaktorTakhfifDasty = objTools.ConvertNulls(objTools.DLookup("AllowPishFaktorTakhfifDasty", "tblGL_SysConfig", "CodeMahal = " & CodeMahalFaal), False)
        Dim ccFaktorTitr As Long = 0

        '' Check Kardan Mojodi
        'If Not IsValidMojodi() Then
        '    Exit Function
        'End If

        '' Check Kardan Anbar Gardani
        'If objTools.ConvertNulls(objTools.DLookup("ccAnbar", "tblAN_AnbarGardany", "ccAnbar = " & dvTitr(cmTitr.Position)("ccAnbar")), 0) <> 0 Then
        '    MsgBox("این انبار درحال انبارگردانی است.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
        '    Exit Function
        'End If

        'Me.Cursor = Cursors.WaitCursor
        'btnSodorFaktorAmani.Enabled = False
        'For Each drv As DataRowView In dsForm.Tables("HETitr").DefaultView
        '    If drv("Taeed") = True Then
        '        If Not AllowPishFaktorTakhfifDasty Then
        '            Dim TJ As New TakhfifOJavaiez.TakhfifJayezeh(CInt(TarikhEmrooz), TakhfifOJavaiez.TakhfifJayezeh.ApplyOnTypes.PishFaktor)
        '            TJ.ApplyTakhfifJayezeh()
        '        End If
        '    End If
        'Next

        'Me.Cursor = Cursors.Default

        Try
            ccFaktorTitr = CreateTitrFaktor()

            If GridEXSatr.GetCheckedRows().Length <> 0 Then
                For i As Integer = 0 To GridEXSatr.RowCount - 1
                    If Val(GridEXSatr.GetRows(i).Cells("TedadForFaktor").Text.Replace(",", "")) <> 0 Then
                        CreateSatrFaktor(ccPishFaktorTitr, ccFaktorTitr, Val(GridEXSatr.GetRows(i).Cells("ccKala").Text.Replace(",", "")), Val(GridEXSatr.GetRows(i).Cells("TedadForFaktor").Text.Replace(",", "")), Val(GridEXSatr.GetRows(i).Cells("IsSabadKala").Text))
                    End If
                Next
            Else
                CreateSatrFaktor(ccPishFaktorTitr, ccFaktorTitr, Val(GridEXSatr.CurrentRow.Cells("ccKala").Text.Replace(",", "")), Val(GridEXSatr.CurrentRow.Cells("TedadForFaktor").Text.Replace(",", "")), Val(GridEXSatr.CurrentRow.Cells("IsSabadKala").Text))
            End If

            If objTools.ConvertNulls(objTools.DLookup("Malyat", "tblFO_Faktor", "ccFaktorTitr = " & ccFaktorTitr), False) = True Then
                ApplyMalyatAvarez(ccFaktorTitr)
            End If

            UpdateTaeedFaktor(ccFaktorTitr)
            '----------------------------------------------SMS-----------------------------------
            'If drv("Mobile") <> "" Then
            '    Dim MessageStatus = objTools.ConvertNulls(objTools.DLookup("MessageStatus", "tblGl_Sysconfig", "CodeMahal=" & CodeMahalFaal), 0)
            '    If MessageStatus <> 0 Then
            '        Dim MessageMarjoee = objTools.ConvertNulls(objTools.DLookup("MessageForFaktor", "tblGl_Sysconfig", "CodeMahal=" & CodeMahalFaal), 0)
            '        If MessageMarjoee <> 0 Then
            '            Dim phone As String() = {drv("Mobile")}
            '            Dim MessageName As String = objTools.ConvertNulls(objTools.DLookup("MessageName", "tblGl_Sherkat", "CodeSherkat = 1"), "")
            '            Dim MessagePass As String = objTools.ConvertNulls(objTools.DLookup("MessagePass", "tblGl_Sherkat", "CodeSherkat = 1"), "")
            '            Dim MessageNumber As String = objTools.ConvertNulls(objTools.DLookup("MessageNumber", "tblGl_Sherkat", "CodeSherkat = 1"), "")
            '            Dim MessageText As String = objTools.ConvertNulls(objTools.DLookup("TextMessageForFaktor", "tblGl_Sysconfig", "CodeMahal=" & CodeMahalFaal), "") + drv("MablaghKol")
            '            If MessageStatus = 1 Then
            '                If MessageText <> "" Then
            '                    If MsgBox("آیا مایلید پیام کوتاه " & drv("NameMoshtary") & " ارسال شود؟", MsgBoxStyle.YesNo + MsgBoxStyle.Question + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.DefaultButton2, "SMS") = MsgBoxResult.Yes Then
            '                        objSms.SentSms(phone, MessageText)
            '                    End If
            '                End If
            '            ElseIf MessageStatus = 2 Then
            '                If MessageText <> "" Then
            '                    objSms.SentSms(phone, MessageText)
            '                End If
            '            End If
            '        End If
            '    End If
            'End If
            ''---------------------------------------------------------------------
            'If drv("Mobile") <> "" Then
            '    Dim MessageStatus = objTools.ConvertNulls(objTools.DLookup("MessageStatus", "tblGl_Sysconfig", "CodeMahal=" & CodeMahalFaal), 0)
            '    If MessageStatus <> 0 Then
            '        Dim MessageForFaktor = objTools.ConvertNulls(objTools.DLookup("MessageForFaktor", "tblGl_Sysconfig", "CodeMahal=" & CodeMahalFaal), 0)
            '        If MessageForFaktor <> 0 Then
            '            Dim phone As String() = {drv("Mobile")}

            '            Dim MessageProvider As String = objTools.ConvertNulls(objTools.DLookup("SmsProvider", "tblGl_Sherkat", "CodeSherkat = 1"), "")
            '            If MessageProvider = 2 Then 'Atie

            '                'Dim MessageText2 As String() = {drv("MablaghKol")}
            '                'Dim MessageText(MessageText1.Length + MessageText2.Length - 1) As String
            '                'Array.Copy(MessageText1, MessageText, MessageText1.Length)
            '                'Array.Copy(MessageText2, 0, MessageText, MessageText1.Length, MessageText2.Length)
            '                'Dim MessageText As String = objTools.ConvertNulls(objTools.DLookup("TextMessageForVosol", "tblGL_SysConfig", ""), "") + " نقد " + " مبلغ " + txtMablagh.Text

            '                'ReDim Preserve MessageText(MessageText.Length + MessageText1.Length - 1)

            '                'Array.Copy(MessageText1, 0, MessageText, MessageText1.Length - MessageText.Length, MessageText.Length)

            '                Dim MessageText As String() = {objTools.ConvertNulls(objTools.DLookup("TextMessageForFaktor", "tblGL_SysConfig", ""), "") + txtMablagh.Text}


            '                If MessageStatus = 1 Then
            '                    If MessageText.ToString <> "" Then
            '                        If MsgBox("آیا مایلید پیام ثبت در سیستم برای مشتری ارسال شود؟", MsgBoxStyle.YesNo + MsgBoxStyle.Question + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.DefaultButton2, "SMS") = MsgBoxResult.Yes Then
            '                            objsmsParsa.SentSmsParsa(phone, MessageText)
            '                        End If
            '                    End If
            '                ElseIf MessageStatus = 2 Then
            '                    If MessageText.ToString <> "" Then
            '                        objsmsParsa.SentSmsParsa(phone, MessageText)
            '                    End If
            '                End If
            '            ElseIf MessageProvider = 1 Then 'Atie
            '                'Dim MessageText As String() = {objTools.ConvertNulls(objTools.DLookup("TextMessageForVosol", "tblGL_SysConfig", ""), "") + " نقد " + " مبلغ " + txtMablagh.Text}

            '                Dim MessageText As String = objTools.ConvertNulls(objTools.DLookup("TextMessageForMoshtary", "tblGL_SysConfig", ""), "") + drv("MablaghKol")

            '                If MessageStatus = 1 Then
            '                    If MessageText <> "" Then
            '                        If MsgBox("آیا مایلید پیام ثبت در سیستم برای مشتری ارسال شود؟", MsgBoxStyle.YesNo + MsgBoxStyle.Question + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.DefaultButton2, "SMS") = MsgBoxResult.Yes Then
            '                            objSms.SentSms(phone, MessageText)
            '                        End If
            '                    End If
            '                ElseIf MessageStatus = 2 Then
            '                    If MessageText <> "" Then
            '                        objSms.SentSms(phone, MessageText)
            '                    End If
            '                End If
            '            End If
            '        End If
            '    End If
            'End If
            ''---------------------------------------------------------------------------------------

            ' Insert To Log Table
            objCode.SabteTaghirat(CodeMahalFaal, UD_Dll.Enums.GL_NoeTaghir.UpdateRecord, "tblFO_PishFaktor", ccPishFaktorTitr, PishFaktorShomareh, "تایید")

            btnSodorFaktorAmani.Enabled = True
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            cnSql.Close()
            cnSql = Nothing
        End Try
    End Function
    Private Function CreateTitrFaktor() As Long
        CreateTitrFaktor = 0
        Dim tCodeCounter As Long
        Try
            Dim cnSQL As SqlConnection
            Dim cmSQL As SqlCommand
            Dim p As New SqlParameter
            Dim strSQL As String

            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()

            strSQL = "Sales.spSodorFaktorAmani_CreateTitrFaktor "

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            p = New SqlParameter("CodeMahal", SqlDbType.Int)
            p.Value = CType(CodeMahalFaal, Integer)
            cmSQL.Parameters.Add(p)

            p = New SqlParameter("CodeDoreh", SqlDbType.Int)
            p.Value = CType(CodeDoreh, Integer)
            cmSQL.Parameters.Add(p)

            p = New SqlParameter("FaktorShomareh", SqlDbType.Int)
            p.Value = objTools.ConvertNulls(objTools.DMax("FaktorShomareh", "tblFO_Faktor", "CodeMahal = " & CodeMahalFaal & " AND CodeDoreh = " & CodeDoreh), 0) + 1
            cmSQL.Parameters.Add(p)

            p = New SqlParameter("FaktorTarikh", SqlDbType.NVarChar, 8)
            p.Value = mskTarikhSodorFaktor.Text
            cmSQL.Parameters.Add(p)

            p = New SqlParameter("ccMamorPakhsh", SqlDbType.Int)
            If flgTafkik Then
                p.Value = IIf(IsNothing(cmbMamorPakhsh.SelectedValue), 0, cmbMamorPakhsh.SelectedValue)
            Else
                p.Value = 0
            End If
            cmSQL.Parameters.Add(p)

            p = New SqlParameter("ccPishFaktor", SqlDbType.BigInt)
            p.Value = ccPishFaktorTitr
            cmSQL.Parameters.Add(p)

            p = New SqlParameter("sVazeiat", SqlDbType.Int)
            p.Value = UD_Dll.Enums.FO_VaziatFaktor.BedoneAmalyat
            cmSQL.Parameters.Add(p)

            p = New SqlParameter("NoeVorod", SqlDbType.TinyInt)
            p.Value = UD_Dll.Enums.GL_NoeVorod.Dasty
            cmSQL.Parameters.Add(p)

            p = New SqlParameter("ccAnbar", SqlDbType.Int)
            p.Value = cmbAnbar.SelectedValue
            cmSQL.Parameters.Add(p)

            p = New SqlParameter("IsForoshGarm", SqlDbType.Bit)
            p.Value = 0
            cmSQL.Parameters.Add(p)

            p = New SqlParameter("TarikhErsal", SqlDbType.NVarChar, 8)
            p.Value = MskErsal.Text
            cmSQL.Parameters.Add(p)

            p = New SqlParameter("UserName", SqlDbType.NVarChar, 20)
            p.Value = UserName
            cmSQL.Parameters.Add(p)

            p = New SqlParameter("Tarikh", SqlDbType.NVarChar, 8)
            p.Value = TarikhEmrooz
            cmSQL.Parameters.Add(p)

            p = New SqlParameter("Saat", SqlDbType.NVarChar, 8)
            p.Value = Format(TimeOfDay, "HH:mm:ss")
            cmSQL.Parameters.Add(p)

            p = New SqlParameter("ccFaktorTitr", SqlDbType.Int)
            p.Direction = ParameterDirection.Output
            cmSQL.Parameters.Add(p)

            cmSQL.ExecuteNonQuery()
            tCodeCounter = cmSQL.Parameters("ccFaktorTitr").Value

            cnSQL.Close()
            cmSQL = Nothing : cnSQL = Nothing

            Return tCodeCounter
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->CreateTitrFaktor")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->CreateTitrFaktor")
        End Try
    End Function
    Private Sub CreateSatrFaktor(ByVal ccPishFaktor As Long, ByVal ccFaktor As Long, ByVal ccKala As Long, ByVal Tedad As Double, ByVal IsSabad As Integer)
        Try

            Dim cnSQL As SqlConnection
            Dim cmSQL As SqlCommand
            Dim p As New SqlParameter
            Dim strSQL As String

            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()

            strSQL = "Sales.spSodorFaktorAmani_CreateSatrFaktor "

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            p = New SqlParameter("ccFaktor", SqlDbType.BigInt)
            p.Value = ccFaktor
            cmSQL.Parameters.Add(p)

            p = New SqlParameter("ccAnbar", SqlDbType.Int)
            p.Value = cmbAnbar.SelectedValue
            cmSQL.Parameters.Add(p)

            p = New SqlParameter("ccKala", SqlDbType.Int)
            p.Value = ccKala
            cmSQL.Parameters.Add(p)

            p = New SqlParameter("Tedad", SqlDbType.Int)
            p.Value = Tedad
            cmSQL.Parameters.Add(p)

            p = New SqlParameter("IsSabad", SqlDbType.Bit)
            p.Value = IsSabad
            cmSQL.Parameters.Add(p)

            p = New SqlParameter("ccPishFaktor", SqlDbType.BigInt)
            p.Value = ccPishFaktor
            cmSQL.Parameters.Add(p)

            cmSQL.ExecuteNonQuery()

            '----- Update Vazeiat PishFaktor Be Faktor Pass Az Etmame Kala'ha

            Dim TedadKalaInPishFaktor As Double = 0
            TedadKalaInPishFaktor = objTools.ConvertNulls(objTools.DSum("Tedad3", "tblFO_PishFaktorSatr", "ccPishFaktorTitr = " & ccPishFaktor), 0)

            Dim TedadKalaInFaktor As Double = 0
            TedadKalaInFaktor = objTools.ConvertNulls(objTools.DSum("Tedad3", "tblFO_FaktorSatr", "ccFaktorTitr = " & ccFaktor), 0)

            If TedadKalaInPishFaktor = TedadKalaInFaktor Then
                strSQL = "Sales.spPishFaktorBeFaktor_UpdateVazeiatPishFaktorAfterEndKala "

                cmSQL = New SqlCommand(strSQL, cnSQL)
                cmSQL.CommandType = CommandType.StoredProcedure
                cmSQL.Parameters.Clear()

                p = New SqlParameter("sVazeiat", SqlDbType.SmallInt)
                p.Value = CType(UD_Dll.Enums.FO_VaziatPishFaktor.FaktorSaderShodeh, Int16)
                cmSQL.Parameters.Add(p)

                p = New SqlParameter("ccPishFaktorTitr", SqlDbType.BigInt)
                p.Value = ccPishFaktor
                cmSQL.Parameters.Add(p)

                cmSQL.ExecuteNonQuery()

                cnSQL.Close()
                cmSQL = Nothing : cnSQL = Nothing
            End If

        Catch ex As SqlException
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->CreateSatrFaktor")
            objTools.DDelete("tblFO_Faktor", "ccFaktorTitr=" & ccFaktor)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->CreateSatrFaktor")
            objTools.DDelete("tblFO_Faktor", "ccFaktorTitr=" & ccFaktor)
        End Try
    End Sub
    Private Sub ApplyMalyatAvarez(ByVal ccFaktorTitr As Double)
        Dim strSQL As String = ""
        Dim cn As New SqlConnection(ConnectionString)
        Dim da As SqlDataAdapter = Nothing
        Dim dt As New DataTable
        Dim cm As SqlCommand = Nothing
        Dim p As New SqlParameter

        Dim IsMalyatAvarezTakhfif As Boolean = objTools.ConvertNulls(objTools.DLookup("IsMalyatAvarezTakhfif", "tblGL_SysConfig", "CodeMahal = " & CodeMahalFaal), False)
        Dim Malyat As Double = 0
        Dim Avarez As Double = 0

        Try
            strSQL = "Sales.spSodorFaktorAmani_ApplyMalyatAvarez "

            cn.Open()
            cm = New SqlCommand(strSQL, cn)
            cm.CommandType = CommandType.StoredProcedure
            cm.Parameters.Clear()

            cm.Parameters.AddWithValue("ccFaktorTitr", ccFaktorTitr)
            cm.Parameters.AddWithValue("IsMalyatAvarezTakhfif", IsMalyatAvarezTakhfif)

            cm.ExecuteNonQuery()

            cm = Nothing
            cn.Close()

            'strSQL = "Sales.spSodorFaktorAmani_InsertKoli_ApplyMalyatAvarez_Search "

            'cn.Open()
            'cm = New SqlCommand(strSQL, cn)
            'cm.CommandType = CommandType.StoredProcedure
            'cm.Parameters.Clear()

            'p = New SqlParameter("ccFaktorTitr", SqlDbType.Int)
            'p.Value = ccFaktorTitr
            'cm.Parameters.Add(p)

            'da = New SqlDataAdapter(cm)

            'Try
            '    da.Fill(dt)
            '    For Each dr As DataRow In dt.Rows

            '        Dim a As Double = objTools.ConvertNulls(objTools.DLookup("TakhfifKala", "tblFO_Faktorsatr", "ccFaktorsatr = " & dr("ccFaktorSatr")), 0)

            '        Malyat = Math.Round(objCode.GetMablaghMalyat(dr("MKOL3") - a), 0)
            '        Avarez = Math.Round(objCode.GetMablaghAvarez(dr("MKOL3") - a), 0)

            '        Dim TakhfifMalyatAvarez As Double = Malyat + Avarez

            '        strSQL = "Sales.spSodorFaktorAmani_InsertKoli_ApplyMalyatAvarez_UpdateMablaghMalyatAndAvarez "

            '        cm = New SqlCommand(strSQL, cn)
            '        cm.CommandType = CommandType.StoredProcedure
            '        cm.Parameters.Clear()

            '        p = New SqlParameter("ccFaktorSatr", SqlDbType.Int)
            '        p.Value = dr("ccFaktorSatr")
            '        cm.Parameters.Add(p)

            '        p = New SqlParameter("MablaghMalyat", SqlDbType.Float)
            '        p.Value = Malyat
            '        cm.Parameters.Add(p)

            '        p = New SqlParameter("MablaghAvarez", SqlDbType.Float)
            '        p.Value = Avarez
            '        cm.Parameters.Add(p)

            '        cm.ExecuteNonQuery()

            '        '----------------------------------------------------------------------------------------


            '        strSQL = "Sales.spSodorFaktorAmani_InsertKoli_ApplyMalyatAvarez_UpdateTakhfifMalyatAvarez "

            '        cm = New SqlCommand(strSQL, cn)
            '        cm.CommandType = CommandType.StoredProcedure
            '        cm.Parameters.Clear()

            '        p = New SqlParameter("ccFaktorSatr", SqlDbType.Int)
            '        p.Value = dr("ccFaktorSatr")
            '        cm.Parameters.Add(p)

            '        p = New SqlParameter("TakhfifMalyatAvarez", SqlDbType.Float)
            '        p.Value = 0
            '        cm.Parameters.Add(p)

            '        cm.ExecuteNonQuery()

            '        '----------------------------------------------------------------------------------------

            '        If IsMalyatAvarezTakhfif = True Then
            '            strSQL = "Sales.spSodorFaktorAmani_InsertKoli_ApplyMalyatAvarez_UpdateTakhfifMalyatAvarez "

            '            cm = New SqlCommand(strSQL, cn)
            '            cm.CommandType = CommandType.StoredProcedure
            '            cm.Parameters.Clear()

            '            p = New SqlParameter("ccFaktorSatr", SqlDbType.Int)
            '            p.Value = dr("ccFaktorSatr")
            '            cm.Parameters.Add(p)

            '            p = New SqlParameter("TakhfifMalyatAvarez", SqlDbType.Float)
            '            p.Value = TakhfifMalyatAvarez
            '            cm.Parameters.Add(p)

            '            cm.ExecuteNonQuery()

            '        End If

            '    Next

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub UpdateTaeedFaktor(ByVal ccFaktorTitr As Long)
        Try
            Dim cnSQL As SqlConnection
            Dim cmSQL As SqlCommand
            Dim p As New SqlParameter
            Dim strSQL

            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()

            strSQL = "Sales.spSodorFaktorAmani_UpdateTaeedFaktor "

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            p = New SqlParameter("ccFaktorTitr", SqlDbType.BigInt)
            p.Value = ccFaktorTitr
            cmSQL.Parameters.Add(p)

            cmSQL.ExecuteNonQuery()

            cnSQL.Close()
            cmSQL = Nothing : cnSQL = Nothing

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->UpdateTaeedFaktor")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->UpdateTaeedFaktor")
        End Try
    End Sub
    Private Sub cmbNamayeshKala_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbNamayeshKala.SelectedIndexChanged
        RefreshSatrData(ccPishFaktorTitr, cmbNamayeshKala.SelectedIndex)
    End Sub

    Private Sub btnSodorKoli_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSodorKoli.Click
        Dim frm_SodorKoli As New frmFO_SodorKoli

        Me.Hide()
        frm_SodorKoli.ShowDialog()
        Me.Show()

        ClearForm()
        Search(True)
    End Sub

    
    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click

        Try
            If dvTitr.Count > 0 Then
                Me.TopMost = False
                RefreshSatrData(ccPishFaktorTitr, 0)
                Print(ccPishFaktorTitr, 0)
                Me.TopMost = True
            End If
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->btnPrintM_Click")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->btnPrintM_Click")
        End Try
    End Sub
    Private Sub Print(ByVal ccPishFaktor As Integer, ByVal Noe As Integer)

        Try

            Dim cnSQL As New SqlConnection
            Dim cmSQL As New SqlCommand
            Dim daSQL As New SqlDataAdapter
            Dim p As New SqlParameter


            Dim strSQL As String = ""

            If dsForm.Tables.Contains("tblGozaresh") Then
                dsForm.Tables.Remove("tblGozaresh")
            End If

            Windows.Forms.Cursor.Current = Cursors.WaitCursor

            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()

            Dim ShomarehPishFaktor As Integer = objTools.DLookup("PishFaktorshomareh", "tblFO_PishFaktor", " ccPishFaktorTitr = " & ccPishFaktor)

            strSQL = "Sales.spSodorFaktorAmani_SearchSatr "

            Windows.Forms.Cursor.Current = Cursors.WaitCursor
            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()
            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()




            p = New SqlParameter("ccPishFaktorTitr", SqlDbType.Int)
            p.Value = ccPishFaktor
            cmSQL.Parameters.Add(p)

            p = New SqlParameter("NoeSearch", SqlDbType.Int)
            p.Value = Noe
            cmSQL.Parameters.Add(p)




            daSQL = New SqlDataAdapter(cmSQL)
            daSQL.Fill(dsForm, "tblGozaresh")



            If dsForm.Tables("tblGozaresh").Rows.Count = 0 Then
                MsgBox("هیـــــچ رکوردی برای گزارش پیدا نشد.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "پیام")
                Exit Sub
            End If

            Dim rpt As New CrystalDecisions.CrystalReports.Engine.ReportDocument
            Dim rpttables As CrystalDecisions.CrystalReports.Engine.Tables
            Dim rptformula As CrystalDecisions.CrystalReports.Engine.FormulaFieldDefinitions
            Dim frm As New Forms_dll.frmGL_Gozaresh

            rpt.Load(rptPath & "\rptFO_GozareshFaktorAmani_Satr.rpt")

            rpttables = rpt.Database.Tables
            rpttables.Item(0).SetDataSource(dsForm.Tables("tblGozaresh"))

            rptformula = rpt.DataDefinition.FormulaFields
            With rptformula

                .Item("Group_Sanad").Text = "{mydata.ccPishFaktorTitr}"
                .Item("ShomarehPishFaktor").Text = "" & ShomarehPishFaktor & ""
                .Item("BarCodeKala").Text = "{mydata.BarCodeKala}"
                .Item("NameKala").Text = "{mydata.NameKala}"
                .Item("Tedad3").Text = "{mydata.Tedad3}"
                .Item("Fee").Text = "{mydata.Fee}"
                .Item("MKol3").Text = "{mydata.MKol3}"
                .Item("TedadEstefadehShodeh").Text = "{mydata.TedadEstefadehShodeh}"
                .Item("TedadMandeh").Text = "{mydata.TedadMandeh}"
                .Item("TedadForFaktor").Text = "{mydata.TedadForFaktor}"
                
                .Item("Title").Text = "'" & "گـزارش صدور فاکتور امانی" & "'"
                .Item("Title2").Text = "'" & NameSherkat & "'"
                .Item("Title3").Text = "'" & NameMahalFaal & "'"
                .Item("KarbarGozaresh").Text = "'" & PersonelName & "'"
                .Item("TarikhGozaresh").Text = "'" & objTarikh.SetDateSlash(TarikhEmrooz) & "'"
                .Item("SaatGozaresh").Text = "'" & Format(TimeOfDay, "HH:mm:ss") & "'"
            End With
            rpt.Refresh()

            frm.Text = txtCaption

            With frm.CRV
                .ReportSource = rpt
                .DisplayGroupTree = False
                .ShowGroupTreeButton = False
            End With


            Me.Hide()
            frm.ShowDialog(Me)
            frm = Nothing
            daSQL = Nothing
            rpt = Nothing
            Me.Show()

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->Print SetReport")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->Print SetReport")
        End Try
    End Sub
End Class
