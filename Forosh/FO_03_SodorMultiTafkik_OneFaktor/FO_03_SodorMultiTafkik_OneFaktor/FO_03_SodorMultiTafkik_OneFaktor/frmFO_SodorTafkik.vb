Public Class frmFO_SodorTafkik
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

    Public TypeSearch As Boolean  ' False : PishFaktor // True : Faktor
    Public AzTarikh As String
    Public TaTarikh As String
    Public ccForoshandeh As Integer
    Public ccAnbar As Integer
    Public sShahr As Integer
    Public strMahaleh As String

    Structure StrTaeedOdat
        Public strTaeed As String
        Public CTaeed As Integer
    End Structure
#End Region
    Private Sub LoadCombo()
        Dim cnSQL As New SqlConnection
        Dim cmSQL As New SqlCommand
        Dim daSQL As SqlDataAdapter
        Dim strSQL As String

        cnSQL = New SqlConnection(ConnectionString)
        cnSQL.Open()

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

        cmSQL = Nothing : daSQL = Nothing
        cnSQL.Close()
    End Sub
    Private Sub Search()
        Dim cnSQL As New SqlConnection
        Dim cmSQL As New SqlCommand
        Dim daSQL As New SqlDataAdapter
        Dim strSQL As String = ""

        If dsForm.Tables.Contains("TblSearch") Then
            dsForm.Tables.Remove("TblSearch")
        End If

        Try
            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()

            strSQL = "Sales.spMultiTafkik_OneFaktor_Search "

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            cmSQL.Parameters.AddWithValue("TypeSearch", TypeSearch)
            cmSQL.Parameters.AddWithValue("CodeDoreh", CodeDoreh)
            cmSQL.Parameters.AddWithValue("CodeMahal", CodeMahalFaal)
            cmSQL.Parameters.AddWithValue("AzTarikh", AzTarikh)
            cmSQL.Parameters.AddWithValue("TaTarikh", TaTarikh)
            cmSQL.Parameters.AddWithValue("ccForoshandeh", ccForoshandeh)
            cmSQL.Parameters.AddWithValue("ccAnbar", ccAnbar)
            cmSQL.Parameters.AddWithValue("sShahr", sShahr)
            cmSQL.Parameters.AddWithValue("strMahaleh", IIf(strMahaleh = "", "", "," & strMahaleh & ","))
            cmSQL.Parameters.AddWithValue("UserName", UserName)

            daSQL = New SqlDataAdapter(cmSQL)
            daSQL.Fill(dsForm, "TblSearch")

            dvTitr = New DataView(dsForm.Tables("TblSearch"))

            dvTitr.AllowNew = False
            dvTitr.AllowDelete = False
            dvTitr.AllowEdit = False

            GridEXTitr.DataSource = Nothing
            GridEXTitr.DataSource = dvTitr

            SetGridTitr()
            BoundCurrencyManagerTitr()

            cmSQL = Nothing : daSQL = Nothing
            cnSQL.Close()

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> Search ")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> Search ")
        End Try

    End Sub
    Private Sub RefreshSatrData(ByVal ccFaktor As Integer)
        Dim cnSQL As New SqlConnection
        Dim cmSQL As New SqlCommand
        Dim daSQL As New SqlDataAdapter
        Dim strSQL As String = ""

        If dsForm.Tables.Contains("TblSearch_Satr") Then
            dsForm.Tables.Remove("TblSearch_Satr")
        End If

        Try
            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()

            strSQL = "Sales.spMultiTafkik_OneFaktor_SearchSatr "

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            cmSQL.Parameters.AddWithValue("ccFaktorTitr", ccFaktor)

            daSQL = New SqlDataAdapter(cmSQL)
            daSQL.Fill(dsForm, "TblSearch_Satr")

            dvSatr = New DataView(dsForm.Tables("TblSearch_Satr"))

            dvSatr.AllowNew = False
            dvSatr.AllowDelete = False
            dvSatr.AllowEdit = False

            GridEXSatr.DataSource = Nothing
            GridEXSatr.DataSource = dvSatr

            SetGridSatr()

            cmSQL = Nothing : daSQL = Nothing
            cnSQL.Close()

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> Search ")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> Search ")
        End Try
    End Sub
    Private Sub BoundCurrencyManagerTitr()
        Try
            cmTitr = CType(BindingContext(GridEXTitr.DataSource), CurrencyManager)
            AddHandler cmTitr.PositionChanged, AddressOf cmTitr_PositionChanged

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->BoundCurrencyManagerTitr")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->BoundCurrencyManagerTitr")
        End Try
    End Sub
    Private Sub cmTitr_PositionChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->cmTitr_PositionChanged")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->cmTitr_PositionChanged")
        End Try

    End Sub
    Private Sub SetGridTitr()
        Try
            If dvTitr.Count = 0 Then
                Exit Sub
            End If

            With GridEXTitr
                .DataSource = Nothing
                .DataSource = dsForm.Tables("TblSearch").DefaultView
                .SetDataBinding(dsForm.Tables("TblSearch").DefaultView, "")
                .RetrieveStructure()
            End With

            For i As Integer = 0 To GridEXTitr.CurrentTable.Columns.Count - 1
                GridEXTitr.CurrentTable.Columns.Item(i).Visible = False
            Next

            GridEXTitr.CurrentTable.Columns.Item("Radif").Caption = "ردیف"
            GridEXTitr.CurrentTable.Columns.Item("Radif").Visible = True
            GridEXTitr.CurrentTable.Columns.Item("Radif").Width = 50
            GridEXTitr.CurrentTable.Columns.Item("Radif").EditType = Janus.Windows.GridEX.EditType.NoEdit
            GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXTitr.CurrentTable.Columns.Item("Radif").Position = 0
            GridEXTitr.CurrentTable.Columns.Item("Radif").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXTitr.CurrentTable.Columns.Item("FaktorShomareh").Caption = "شماره فاکتور"
            GridEXTitr.CurrentTable.Columns.Item("FaktorShomareh").Visible = True
            GridEXTitr.CurrentTable.Columns.Item("FaktorShomareh").Width = 60
            GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXTitr.CurrentTable.Columns.Item("FaktorShomareh").Position = 1
            GridEXTitr.CurrentTable.Columns.Item("FaktorShomareh").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXTitr.CurrentTable.Columns.Item("NameForoshandeh").Caption = "نام فروشنده"
            GridEXTitr.CurrentTable.Columns.Item("NameForoshandeh").Visible = True
            GridEXTitr.CurrentTable.Columns.Item("NameForoshandeh").Width = 100
            GridEXTitr.CurrentTable.Columns.Item("NameForoshandeh").EditType = Janus.Windows.GridEX.EditType.NoEdit
            GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXTitr.CurrentTable.Columns.Item("NameForoshandeh").Position = 2
            GridEXTitr.CurrentTable.Columns.Item("NameForoshandeh").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXTitr.CurrentTable.Columns.Item("CodeMoshtary").Caption = "کـد مشتـری"
            GridEXTitr.CurrentTable.Columns.Item("CodeMoshtary").Visible = True
            GridEXTitr.CurrentTable.Columns.Item("CodeMoshtary").Width = 70
            GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXTitr.CurrentTable.Columns.Item("CodeMoshtary").EditType = Janus.Windows.GridEX.EditType.NoEdit
            GridEXTitr.CurrentTable.Columns.Item("CodeMoshtary").Position = 3
            GridEXTitr.CurrentTable.Columns.Item("CodeMoshtary").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXTitr.CurrentTable.Columns.Item("NameMoshtary").Caption = "نام مشتـری"
            GridEXTitr.CurrentTable.Columns.Item("NameMoshtary").Visible = True
            GridEXTitr.CurrentTable.Columns.Item("NameMoshtary").Width = 100
            GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXTitr.CurrentTable.Columns.Item("NameMoshtary").EditType = Janus.Windows.GridEX.EditType.NoEdit
            GridEXTitr.CurrentTable.Columns.Item("NameMoshtary").Position = 4
            GridEXTitr.CurrentTable.Columns.Item("NameMoshtary").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXTitr.CurrentTable.Columns.Item("Address").Caption = "آدرس"
            GridEXTitr.CurrentTable.Columns.Item("Address").Visible = True
            GridEXTitr.CurrentTable.Columns.Item("Address").Width = 100
            GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXTitr.CurrentTable.Columns.Item("Address").EditType = Janus.Windows.GridEX.EditType.NoEdit
            GridEXTitr.CurrentTable.Columns.Item("Address").Position = 5
            GridEXTitr.CurrentTable.Columns.Item("Address").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXTitr.CurrentTable.Columns.Item("Tozihat").Caption = "توضیحـات"
            GridEXTitr.CurrentTable.Columns.Item("Tozihat").Visible = True
            GridEXTitr.CurrentTable.Columns.Item("Tozihat").Width = 100
            GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXTitr.CurrentTable.Columns.Item("Tozihat").Position = 6
            GridEXTitr.CurrentTable.Columns.Item("Tozihat").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXTitr.CurrentTable.Columns.Item("JamKol").Caption = "مبلغ کل"
            GridEXTitr.CurrentTable.Columns.Item("JamKol").Visible = True
            GridEXTitr.CurrentTable.Columns.Item("JamKol").Width = 80
            GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXTitr.CurrentTable.Columns.Item("JamKol").Position = 7
            GridEXTitr.CurrentTable.Columns.Item("JamKol").FormatString = "###,###.##"
            GridEXTitr.CurrentTable.Columns.Item("JamKol").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXTitr.CurrentTable.Columns.Item("txtMantagheh").Caption = "نام منطقه"
            GridEXTitr.CurrentTable.Columns.Item("txtMantagheh").Visible = True
            GridEXTitr.CurrentTable.Columns.Item("txtMantagheh").Width = 100
            GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOn
            GridEXTitr.CurrentTable.Columns.Item("txtMantagheh").Position = 8
            GridEXTitr.CurrentTable.Columns.Item("txtMantagheh").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXTitr.CurrentTable.Columns.Item("txtMahaleh").Caption = "نام محله"
            GridEXTitr.CurrentTable.Columns.Item("txtMahaleh").Visible = True
            GridEXTitr.CurrentTable.Columns.Item("txtMahaleh").Width = 100
            GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXTitr.CurrentTable.Columns.Item("txtMahaleh").EditType = Janus.Windows.GridEX.EditType.NoEdit
            GridEXTitr.CurrentTable.Columns.Item("txtMahaleh").Position = 9
            GridEXTitr.CurrentTable.Columns.Item("txtMahaleh").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXTitr.CurrentTable.Columns.Item("txtNoePardakht").Caption = "تسویه"
            GridEXTitr.CurrentTable.Columns.Item("txtNoePardakht").Visible = True
            GridEXTitr.CurrentTable.Columns.Item("txtNoePardakht").Width = 60
            GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXTitr.CurrentTable.Columns.Item("txtNoePardakht").EditType = Janus.Windows.GridEX.EditType.NoEdit
            GridEXTitr.CurrentTable.Columns.Item("txtNoePardakht").Position = 10
            GridEXTitr.CurrentTable.Columns.Item("txtNoePardakht").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXTitr.CurrentTable.Columns.Item("NameMasir").Caption = "مسیـر"
            GridEXTitr.CurrentTable.Columns.Item("NameMasir").Visible = True
            GridEXTitr.CurrentTable.Columns.Item("NameMasir").Width = 100
            GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXTitr.CurrentTable.Columns.Item("NameMasir").EditType = Janus.Windows.GridEX.EditType.NoEdit
            GridEXTitr.CurrentTable.Columns.Item("NameMasir").Position = 11
            GridEXTitr.CurrentTable.Columns.Item("NameMasir").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXTitr.CurrentTable.Columns.Item("PishFaktorShomareh").Caption = "شماره پیش فاکتور"
            GridEXTitr.CurrentTable.Columns.Item("PishFaktorShomareh").Visible = True
            GridEXTitr.CurrentTable.Columns.Item("PishFaktorShomareh").Width = 100
            GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXTitr.CurrentTable.Columns.Item("PishFaktorShomareh").EditType = Janus.Windows.GridEX.EditType.NoEdit
            GridEXTitr.CurrentTable.Columns.Item("PishFaktorShomareh").Position = 12
            GridEXTitr.CurrentTable.Columns.Item("PishFaktorShomareh").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXTitr.CurrentTable.Columns.Item("PishFaktorTarikhSlash").Caption = "تاریخ پیش فاکتور"
            GridEXTitr.CurrentTable.Columns.Item("PishFaktorTarikhSlash").Visible = True
            GridEXTitr.CurrentTable.Columns.Item("PishFaktorTarikhSlash").Width = 100
            GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXTitr.CurrentTable.Columns.Item("PishFaktorTarikhSlash").EditType = Janus.Windows.GridEX.EditType.NoEdit
            GridEXTitr.CurrentTable.Columns.Item("PishFaktorTarikhSlash").Position = 13
            GridEXTitr.CurrentTable.Columns.Item("PishFaktorTarikhSlash").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXTitr.CurrentTable.Columns.Item("FaktorTarikhSlash").Caption = "تاریخ فاکتور"
            GridEXTitr.CurrentTable.Columns.Item("FaktorTarikhSlash").Visible = True
            GridEXTitr.CurrentTable.Columns.Item("FaktorTarikhSlash").Width = 80
            GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXTitr.CurrentTable.Columns.Item("FaktorTarikhSlash").EditType = Janus.Windows.GridEX.EditType.NoEdit
            GridEXTitr.CurrentTable.Columns.Item("FaktorTarikhSlash").Position = 14
            GridEXTitr.CurrentTable.Columns.Item("FaktorTarikhSlash").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXTitr.CurrentTable.Columns.Item("txtShahr").Caption = "شهـر"
            GridEXTitr.CurrentTable.Columns.Item("txtShahr").Visible = True
            GridEXTitr.CurrentTable.Columns.Item("txtShahr").Width = 100
            GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOn
            GridEXTitr.CurrentTable.Columns.Item("txtShahr").Position = 15
            GridEXTitr.CurrentTable.Columns.Item("txtShahr").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXTitr.CurrentTable.Columns.Item("txtMantaghehShahrdary").Caption = "منطقه شهرداری"
            GridEXTitr.CurrentTable.Columns.Item("txtMantaghehShahrdary").Visible = True
            GridEXTitr.CurrentTable.Columns.Item("txtMantaghehShahrdary").Width = 150
            GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOn
            GridEXTitr.CurrentTable.Columns.Item("txtMantaghehShahrdary").Position = 16
            GridEXTitr.CurrentTable.Columns.Item("txtMantaghehShahrdary").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXTitr.CurrentTable.Columns.Item("JamFaktor").Caption = "جمع مبلغ"
            GridEXTitr.CurrentTable.Columns.Item("JamFaktor").Visible = True
            GridEXTitr.CurrentTable.Columns.Item("JamFaktor").Width = 85
            GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOn
            GridEXTitr.CurrentTable.Columns.Item("JamFaktor").Position = 17
            GridEXTitr.CurrentTable.Columns.Item("JamFaktor").FormatString = "###,###.##"
            GridEXTitr.CurrentTable.Columns.Item("JamFaktor").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXTitr.CurrentTable.Columns.Item("Takhfif").Caption = "تخفیف"
            GridEXTitr.CurrentTable.Columns.Item("Takhfif").Visible = True
            GridEXTitr.CurrentTable.Columns.Item("Takhfif").Width = 100
            GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOn
            GridEXTitr.CurrentTable.Columns.Item("Takhfif").Position = 18
            GridEXTitr.CurrentTable.Columns.Item("Takhfif").FormatString = "###,###.##"
            GridEXTitr.CurrentTable.Columns.Item("Takhfif").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXTitr.CurrentTable.Columns.Item("JamTakhfifKala").Caption = "جمع تخفیف کالا"
            GridEXTitr.CurrentTable.Columns.Item("JamTakhfifKala").Visible = True
            GridEXTitr.CurrentTable.Columns.Item("JamTakhfifKala").Width = 100
            GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOn
            GridEXTitr.CurrentTable.Columns.Item("JamTakhfifKala").Position = 19
            GridEXTitr.CurrentTable.Columns.Item("JamTakhfifKala").FormatString = "###,###.##"
            GridEXTitr.CurrentTable.Columns.Item("JamTakhfifKala").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXTitr.CurrentTable.Columns.Item("JamJayezeh").Caption = "جمع جوائز"
            GridEXTitr.CurrentTable.Columns.Item("JamJayezeh").Visible = True
            GridEXTitr.CurrentTable.Columns.Item("JamJayezeh").Width = 100
            GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOn
            GridEXTitr.CurrentTable.Columns.Item("JamJayezeh").Position = 20
            GridEXTitr.CurrentTable.Columns.Item("JamJayezeh").FormatString = "###,###.##"
            GridEXTitr.CurrentTable.Columns.Item("JamJayezeh").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXTitr.CurrentTable.Columns.Item("JamMalyatAvarez").Caption = "مالیات و عوارض"
            GridEXTitr.CurrentTable.Columns.Item("JamMalyatAvarez").Visible = True
            GridEXTitr.CurrentTable.Columns.Item("JamMalyatAvarez").Width = 110
            GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOn
            GridEXTitr.CurrentTable.Columns.Item("JamMalyatAvarez").Position = 21
            GridEXTitr.CurrentTable.Columns.Item("JamMalyatAvarez").FormatString = "###,###.##"
            GridEXTitr.CurrentTable.Columns.Item("JamMalyatAvarez").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXTitr.CurrentTable.Columns.Item("Telephone").Caption = "تلفن"
            GridEXTitr.CurrentTable.Columns.Item("Telephone").Visible = True
            GridEXTitr.CurrentTable.Columns.Item("Telephone").Width = 70
            GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOn
            GridEXTitr.CurrentTable.Columns.Item("Telephone").Position = 22
            GridEXTitr.CurrentTable.Columns.Item("Telephone").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXTitr.CurrentTable.Columns.Item("Saat").Caption = "ساعت"
            GridEXTitr.CurrentTable.Columns.Item("Saat").Visible = True
            GridEXTitr.CurrentTable.Columns.Item("Saat").Width = 70
            GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOn
            GridEXTitr.CurrentTable.Columns.Item("Saat").Position = 23
            GridEXTitr.CurrentTable.Columns.Item("Saat").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXTitr.CurrentTable.Columns.Item("UserName").Caption = "نام کاربر"
            GridEXTitr.CurrentTable.Columns.Item("UserName").Visible = True
            GridEXTitr.CurrentTable.Columns.Item("UserName").Width = 100
            GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOn
            GridEXTitr.CurrentTable.Columns.Item("UserName").Position = 24
            GridEXTitr.CurrentTable.Columns.Item("UserName").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXTitr.CurrentTable.Columns.Item("ccFaktorTitr").Caption = "ccFaktorTitr"
            GridEXTitr.CurrentTable.Columns.Item("ccFaktorTitr").Visible = False
            GridEXTitr.CurrentTable.Columns.Item("ccFaktorTitr").Width = 0
            GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOn
            GridEXTitr.CurrentTable.Columns.Item("ccFaktorTitr").Position = 25
            GridEXTitr.CurrentTable.Columns.Item("ccFaktorTitr").FormatString = "###,###.##"
            GridEXTitr.CurrentTable.Columns.Item("ccFaktorTitr").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            For i As Integer = 0 To GridEXTitr.RootTable.Columns.Count - 1
                If GridEXTitr.RootTable.Columns(i).Type.IsValueType Then
                    GridEXTitr.RootTable.Columns(i).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                    GridEXTitr.RootTable.Columns(i).FormatMode = Janus.Windows.GridEX.FormatMode.UseIFormattable
                    GridEXTitr.RootTable.Columns(i).FormatString = "###,###.##"
                    GridEXTitr.RootTable.Columns(i).TotalFormatMode = Janus.Windows.GridEX.FormatMode.UseIFormattable
                    GridEXTitr.RootTable.Columns(i).TotalFormatString = "###,###.##"
                End If
            Next

            GridEXTitr.Visible = True
            GridEXTitr.HeaderFormatStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> SetGridTitr ")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> SetGridTitr ")
        End Try

    End Sub
    Private Sub SetGridSatr()
        Try
            If dvTitr.Count = 0 Then
                Exit Sub
            End If

            If dvSatr.Count = 0 Then
                Exit Sub
            End If

            With GridEXSatr
                .DataSource = Nothing
                .DataSource = dsForm.Tables("TblSearch_Satr").DefaultView
                .SetDataBinding(dsForm.Tables("TblSearch_Satr").DefaultView, "")
                .RetrieveStructure()
            End With

            For i As Integer = 0 To GridEXSatr.CurrentTable.Columns.Count - 1
                GridEXSatr.CurrentTable.Columns.Item(i).Visible = False
            Next

            GridEXSatr.CurrentTable.Columns.Item("CodeKala").Caption = "کـد کالا"
            GridEXSatr.CurrentTable.Columns.Item("CodeKala").Visible = True
            GridEXSatr.CurrentTable.Columns.Item("CodeKala").Width = 60
            GridEXSatr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXSatr.CurrentTable.Columns.Item("CodeKala").Position = 0
            GridEXSatr.CurrentTable.Columns.Item("CodeKala").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXSatr.CurrentTable.Columns.Item("NameKala").Caption = "نام کالا"
            GridEXSatr.CurrentTable.Columns.Item("NameKala").Visible = True
            GridEXSatr.CurrentTable.Columns.Item("NameKala").Width = 100
            GridEXSatr.CurrentTable.Columns.Item("NameKala").EditType = Janus.Windows.GridEX.EditType.NoEdit
            GridEXSatr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXSatr.CurrentTable.Columns.Item("NameKala").Position = 1
            GridEXSatr.CurrentTable.Columns.Item("NameKala").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXSatr.CurrentTable.Columns.Item("TedadFaktor").Caption = "تعداد فاکتور"
            GridEXSatr.CurrentTable.Columns.Item("TedadFaktor").Visible = True
            GridEXSatr.CurrentTable.Columns.Item("TedadFaktor").Width = 70
            GridEXSatr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXSatr.CurrentTable.Columns.Item("TedadFaktor").EditType = Janus.Windows.GridEX.EditType.NoEdit
            GridEXSatr.CurrentTable.Columns.Item("TedadFaktor").Position = 2
            GridEXSatr.CurrentTable.Columns.Item("TedadFaktor").FormatString = "###,###.##"
            GridEXSatr.CurrentTable.Columns.Item("TedadFaktor").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXSatr.CurrentTable.Columns.Item("TedadTafkikShodeh").Caption = "تعداد تفکیک شده"
            GridEXSatr.CurrentTable.Columns.Item("TedadTafkikShodeh").Visible = True
            GridEXSatr.CurrentTable.Columns.Item("TedadTafkikShodeh").Width = 80
            GridEXSatr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXSatr.CurrentTable.Columns.Item("TedadTafkikShodeh").EditType = Janus.Windows.GridEX.EditType.NoEdit
            GridEXSatr.CurrentTable.Columns.Item("TedadTafkikShodeh").Position = 3
            GridEXSatr.CurrentTable.Columns.Item("TedadTafkikShodeh").FormatString = "###,###.##"
            GridEXSatr.CurrentTable.Columns.Item("TedadTafkikShodeh").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXSatr.CurrentTable.Columns.Item("TedadMandeh").Caption = "تعداد مانده"
            GridEXSatr.CurrentTable.Columns.Item("TedadMandeh").Visible = True
            GridEXSatr.CurrentTable.Columns.Item("TedadMandeh").Width = 80
            GridEXSatr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXSatr.CurrentTable.Columns.Item("TedadMandeh").EditType = Janus.Windows.GridEX.EditType.NoEdit
            GridEXSatr.CurrentTable.Columns.Item("TedadMandeh").Position = 4
            GridEXSatr.CurrentTable.Columns.Item("TedadMandeh").FormatString = "###,###.##"
            GridEXSatr.CurrentTable.Columns.Item("TedadMandeh").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXSatr.CurrentTable.Columns.Item("Tedad").Caption = "تعداد"
            GridEXSatr.CurrentTable.Columns.Item("Tedad").Visible = True
            GridEXSatr.CurrentTable.Columns.Item("Tedad").Width = 80
            GridEXSatr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXSatr.CurrentTable.Columns.Item("Tedad").Position = 5
            GridEXSatr.CurrentTable.Columns.Item("Tedad").FormatString = "###,###.##"
            GridEXSatr.CurrentTable.Columns.Item("Tedad").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXSatr.CurrentTable.Columns.Item("ccKala").Caption = "ccKala"
            GridEXSatr.CurrentTable.Columns.Item("ccKala").Visible = False
            GridEXSatr.CurrentTable.Columns.Item("ccKala").Width = 0
            GridEXSatr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXSatr.CurrentTable.Columns.Item("ccKala").Position = 6
            GridEXSatr.CurrentTable.Columns.Item("ccKala").FormatString = "###,###.##"
            GridEXSatr.CurrentTable.Columns.Item("ccKala").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            For i As Integer = 0 To GridEXTitr.RootTable.Columns.Count - 1
                If GridEXTitr.RootTable.Columns(i).Type.IsValueType Then
                    GridEXTitr.RootTable.Columns(i).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                    GridEXTitr.RootTable.Columns(i).FormatMode = Janus.Windows.GridEX.FormatMode.UseIFormattable
                    GridEXTitr.RootTable.Columns(i).FormatString = "###,###.##"
                    GridEXTitr.RootTable.Columns(i).TotalFormatMode = Janus.Windows.GridEX.FormatMode.UseIFormattable
                    GridEXTitr.RootTable.Columns(i).TotalFormatString = "###,###.##"
                End If
            Next

            GridEXTitr.Visible = True
            GridEXTitr.HeaderFormatStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> SetGridSatr ")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> SetGridSatr ")
        End Try

    End Sub
    Private Sub frmFO_SodorTafkik_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetParameter()
        LoadCombo()

        Search()
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

    Private Sub GridEXTitr_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GridEXTitr.Click
        RefreshSatrData(GridEXTitr.CurrentRow.Cells("ccFaktorTitr").Text.Replace(",", ""))
    End Sub
End Class