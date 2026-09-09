Public Class frmDP_VosolFromTabletPC

#Region "Variable AND Constant Declration"
    ' Security 
    Const cntCodeSubSystem As Long = 100088
    Private SN As Integer

    Const GridOrgSize As Integer = 225
    Const GridSize As Integer = 190
    Dim dvTitr As DataView
    Dim dvSatr As DataView
    Dim tCodeCounter As Long
    Const FormAddSize = 0
    Const GridAddSize = 0
    Dim cmTitr As CurrencyManager
    Dim cmSatr As CurrencyManager

    Dim ErrPro As New ErrorProvider
    Dim Mode As UD_Dll.Enums.GL_ModeForms = UD_Dll.Enums.GL_ModeForms.AddNewRecord
    Dim dsForm As New DataSet
    Dim cmForm As CurrencyManager
    Dim dvForm As DataView
    Dim flg As Boolean = False
    Dim txtCaption As String
    Dim tSaatVazeiat As String
    Dim tTarikhDP As String
    Dim tSh As Long
    Dim tEbtal As Boolean
    Dim tVazeiat As Long
    Dim tTarikhVazeiat As String
    Dim tShHEntry As String
    Private WithEvents BS As New UD_Dll.PassString
#End Region

    Private Sub frmDP_VosolFromTabletPC_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetParameter()
        SN = objSec.GetSecurityNumber(cntCodeSubSystem, UserName)
        mskAzTarikh.Text = ObjCode.DecDay(TarikhEmrooz)
        mskTaTarikh.Text = TarikhEmrooz
        Search()
        btnSave.Enabled = False
    End Sub
    Private Sub SetParameter()
        Dim commands As String = Microsoft.VisualBasic.Command()
        If commands.Length = 0 Then

            UserName = "Administrator"
            UserPassWord = "66998833"
            NameMahalFaal = "تهران"
            CodeMahalFaal = "1"
            PersonelCode = "0"
            PersonelName = "Administrator"
            CodeDoreh = "1391"
            txtCaption = "وصولی های تبلت"
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

        End If
    End Sub
    Private Sub Search()
        Try
            Dim StrSql As String

            StrSql = "Treasury.spVosolFromTabletPC_SearchTitr "

            RefreshTitrdata(StrSql)
            RefreshSatrData()
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->Search")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->Search")
        End Try
    End Sub
    Private Sub RefreshTitrdata(ByVal strSql As String)
        Try
            Dim cnSQL As New SqlConnection
            Dim cmSQL As New SqlCommand
            Dim daSQL As SqlDataAdapter

            If dsForm.Tables.Contains("Treasury_VosolFromTabletPC") Then
                dsForm.Tables.Remove("Treasury_VosolFromTabletPC")
            End If

            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()

            cmSQL = New SqlCommand(strSql, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            cmSQL.Parameters.AddWithValue("AzTarikh", IIf(mskAzTarikh.Text = "", ObjCode.DecDay(TarikhEmrooz), mskAzTarikh.Text))
            cmSQL.Parameters.AddWithValue("TaTarikh", IIf(mskTaTarikh.Text = "", TarikhEmrooz, mskTaTarikh.Text))

            daSQL = New SqlDataAdapter(cmSQL)
            daSQL.Fill(dsForm, "Treasury_VosolFromTabletPC")

            dvTitr = New DataView(dsForm.Tables("Treasury_VosolFromTabletPC"), "", "TarikhSanad ASC", DataViewRowState.CurrentRows)
            dvTitr.AllowNew = False
            dvTitr.AllowDelete = False
            dvTitr.AllowEdit = False

            cmSQL.Connection.Close()
            cnSQL.Close()
            cmSQL = Nothing
            daSQL = Nothing

            GridEXTitr.DataSource = Nothing
            GridEXTitr.DataSource = dvTitr
            BoundCurrencyManagerTitr()
            'SetTitrButton()

            If dvTitr.Count = 0 Then
                GridEXSatr.DataSource = Nothing
            End If

            SetGridStyle()

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->RefreshTitrdata")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->RefreshTitrdata")
        End Try

    End Sub
    Private Sub RefreshSatrData()
        Try
            If dvTitr.Count = 0 Then
                Exit Sub
            End If

            Dim Strsql As String
            Dim cnSQL As New SqlConnection
            Dim cmSQL As New SqlCommand
            Dim daSQL As SqlDataAdapter

            If dsForm.Tables.Contains("Treasury_VosolFromTabletPCsatr") Then
                dsForm.Tables.Remove("Treasury_VosolFromTabletPCsatr")
            End If

            Strsql = "Treasury.spVosolFromTabletPC_SearchSatr "

            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()

            cmSQL = New SqlCommand(Strsql, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            cmSQL.Parameters.AddWithValue("ccImportPaymentTablet", dvTitr(cmTitr.Position)("ccImportPaymentTablet"))

            daSQL = New SqlDataAdapter(cmSQL)
            daSQL.Fill(dsForm, "Treasury_VosolFromTabletPCsatr")

            dvSatr = New DataView(dsForm.Tables("Treasury_VosolFromTabletPCsatr"))

            dvSatr.AllowNew = False
            dvSatr.AllowDelete = False
            dvSatr.AllowEdit = True

            daSQL = Nothing
            GridEXSatr.DataSource = Nothing
            GridEXSatr.DataSource = dvSatr
            cnSQL.Close()
            BoundCurrencyManagerSatr()
            'SetSatrButton()
            SetGridStyleSatr()

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->RefreshSatrData")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->RefreshSatrData")
        End Try
    End Sub
    Private Sub BoundCurrencyManagerTitr()
        Try
            cmTitr = CType(BindingContext(GridEXTitr.DataSource), CurrencyManager)
            AddHandler cmTitr.ItemChanged, AddressOf cmTitr_ItemChanged
            AddHandler cmTitr.PositionChanged, AddressOf cmTitr_PositionChanged
            RefreshSatrData()
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->BoundCurrencyManagerTitr")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->BoundCurrencyManagerTitr")
        End Try
    End Sub
    Private Sub BoundCurrencyManagerSatr()
        Try
            cmSatr = CType(BindingContext(GridEXSatr.DataSource), CurrencyManager)
            AddHandler cmSatr.ItemChanged, AddressOf cmSatr_ItemChanged
            AddHandler cmSatr.PositionChanged, AddressOf cmSatr_PositionChanged
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->BoundCurrencyManagerSatr")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->BoundCurrencyManagerSatr")
        End Try
    End Sub
    Private Sub cmTitr_ItemChanged(ByVal sender As Object, ByVal e As ItemChangedEventArgs)
        Try
            Search()
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->cmTitr_ItemChanged")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->cmTitr_ItemChanged")
        End Try
    End Sub
    Private Sub cmTitr_PositionChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            'SetFormTitrData()
            'RefreshSatrData()
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->cmTitr_PositionChanged")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->cmTitr_PositionChanged")
        End Try
    End Sub
    Private Sub cmSatr_ItemChanged(ByVal sender As Object, ByVal e As ItemChangedEventArgs)
        Try
            'SetSatrButton()
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->cmSatr_ItemChanged")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->cmSatr_ItemChanged")
        End Try
    End Sub
    Private Sub cmSatr_PositionChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            'SetSatrButton()
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->cmSatr_PositionChanged")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->cmSatr_PositionChanged")
        End Try
    End Sub
    'Private Sub SetGridStyle()
    '    Try
    '        'Titr
    '        '=========================================================================================='
    '        With GridEXTitr
    '            .DataSource = Nothing
    '            .DataSource = dsForm.Tables("Treasury_VosolFromTabletPC").DefaultView
    '            .SetDataBinding(dsForm.Tables("Treasury_VosolFromTabletPC").DefaultView, "")
    '            .RetrieveStructure()
    '        End With

    '        For i As Integer = 0 To GridEXTitr.CurrentTable.Columns.Count - 1
    '            GridEXTitr.CurrentTable.Columns.Item(i).Visible = False
    '        Next

    '        GridEXTitr.CurrentTable.Columns.Item("Taeed").Caption = "انتخاب"
    '        GridEXTitr.CurrentTable.Columns.Item("Taeed").HeaderStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
    '        GridEXTitr.CurrentTable.Columns.Item("Taeed").Visible = True
    '        GridEXTitr.CurrentTable.Columns.Item("Taeed").Width = 50
    '        GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
    '        GridEXTitr.CurrentTable.Columns.Item("Taeed").Position = 0
    '        GridEXTitr.CurrentTable.Columns.Item("Taeed").Selectable = True
    '        GridEXTitr.CurrentTable.Columns.Item("Taeed").ActAsSelector = True
    '        GridEXTitr.CurrentTable.Columns.Item("Taeed").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

    '        GridEXTitr.CurrentTable.Columns.Item("CodeForoshandeh").Caption = "کد فروشنده"
    '        GridEXTitr.CurrentTable.Columns.Item("CodeForoshandeh").HeaderStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
    '        GridEXTitr.CurrentTable.Columns.Item("CodeForoshandeh").Visible = True
    '        GridEXTitr.CurrentTable.Columns.Item("CodeForoshandeh").Width = 100
    '        GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
    '        GridEXTitr.CurrentTable.Columns.Item("CodeForoshandeh").Position = 1
    '        GridEXTitr.CurrentTable.Columns.Item("CodeForoshandeh").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

    '        GridEXTitr.CurrentTable.Columns.Item("NameForoshandeh").Caption = "نام فروشنده"
    '        GridEXTitr.CurrentTable.Columns.Item("NameForoshandeh").HeaderStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
    '        GridEXTitr.CurrentTable.Columns.Item("NameForoshandeh").Visible = True
    '        GridEXTitr.CurrentTable.Columns.Item("NameForoshandeh").Width = 100
    '        GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
    '        GridEXTitr.CurrentTable.Columns.Item("NameForoshandeh").Position = 2
    '        GridEXTitr.CurrentTable.Columns.Item("NameForoshandeh").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

    '        GridEXTitr.CurrentTable.Columns.Item("txtNoeSanad").Caption = "نوع سنـد"
    '        GridEXTitr.CurrentTable.Columns.Item("txtNoeSanad").HeaderStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
    '        GridEXTitr.CurrentTable.Columns.Item("txtNoeSanad").Visible = True
    '        GridEXTitr.CurrentTable.Columns.Item("txtNoeSanad").Width = 70
    '        GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
    '        GridEXTitr.CurrentTable.Columns.Item("txtNoeSanad").Position = 3
    '        GridEXTitr.CurrentTable.Columns.Item("txtNoeSanad").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

    '        GridEXTitr.CurrentTable.Columns.Item("Mablagh").Caption = "مبلغ سنـد"
    '        GridEXTitr.CurrentTable.Columns.Item("Mablagh").HeaderStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
    '        GridEXTitr.CurrentTable.Columns.Item("Mablagh").Visible = True
    '        GridEXTitr.CurrentTable.Columns.Item("Mablagh").Width = 90
    '        GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
    '        GridEXTitr.CurrentTable.Columns.Item("Mablagh").Position = 4
    '        GridEXTitr.CurrentTable.Columns.Item("Mablagh").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

    '        GridEXTitr.CurrentTable.Columns.Item("NameBank").Caption = "بانک سنـد"
    '        GridEXTitr.CurrentTable.Columns.Item("NameBank").HeaderStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
    '        GridEXTitr.CurrentTable.Columns.Item("NameBank").Visible = True
    '        GridEXTitr.CurrentTable.Columns.Item("NameBank").Width = 60
    '        GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
    '        GridEXTitr.CurrentTable.Columns.Item("NameBank").Position = 5
    '        GridEXTitr.CurrentTable.Columns.Item("NameBank").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

    '        GridEXTitr.CurrentTable.Columns.Item("SeriHarfi").Caption = "سری حرفی"
    '        GridEXTitr.CurrentTable.Columns.Item("SeriHarfi").HeaderStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
    '        GridEXTitr.CurrentTable.Columns.Item("SeriHarfi").Visible = True
    '        GridEXTitr.CurrentTable.Columns.Item("SeriHarfi").Width = 50
    '        GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
    '        GridEXTitr.CurrentTable.Columns.Item("SeriHarfi").Position = 6
    '        GridEXTitr.CurrentTable.Columns.Item("SeriHarfi").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

    '        GridEXTitr.CurrentTable.Columns.Item("SeriAdadi").Caption = "سری عـددی"
    '        GridEXTitr.CurrentTable.Columns.Item("SeriAdadi").HeaderStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
    '        GridEXTitr.CurrentTable.Columns.Item("SeriAdadi").Visible = True
    '        GridEXTitr.CurrentTable.Columns.Item("SeriAdadi").Width = 100
    '        GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
    '        GridEXTitr.CurrentTable.Columns.Item("SeriAdadi").Position = 7
    '        GridEXTitr.CurrentTable.Columns.Item("SeriAdadi").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

    '        GridEXTitr.CurrentTable.Columns.Item("ShomarehSanad").Caption = "شماره سنـد"
    '        GridEXTitr.CurrentTable.Columns.Item("ShomarehSanad").HeaderStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
    '        GridEXTitr.CurrentTable.Columns.Item("ShomarehSanad").Visible = True
    '        GridEXTitr.CurrentTable.Columns.Item("ShomarehSanad").Width = 100
    '        GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
    '        GridEXTitr.CurrentTable.Columns.Item("ShomarehSanad").Position = 8
    '        GridEXTitr.CurrentTable.Columns.Item("ShomarehSanad").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

    '        GridEXTitr.CurrentTable.Columns.Item("TarikhSanad").Caption = "تاریخ سنـد"
    '        GridEXTitr.CurrentTable.Columns.Item("TarikhSanad").HeaderStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
    '        GridEXTitr.CurrentTable.Columns.Item("TarikhSanad").Visible = True
    '        GridEXTitr.CurrentTable.Columns.Item("TarikhSanad").Width = 100
    '        GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
    '        GridEXTitr.CurrentTable.Columns.Item("TarikhSanad").Position = 9
    '        GridEXTitr.CurrentTable.Columns.Item("TarikhSanad").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

    '        GridEXTitr.CurrentTable.Columns.Item("ShomarehHesabSanad").Caption = "شماره حساب سند"
    '        GridEXTitr.CurrentTable.Columns.Item("ShomarehHesabSanad").HeaderStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
    '        GridEXTitr.CurrentTable.Columns.Item("ShomarehHesabSanad").Visible = True
    '        GridEXTitr.CurrentTable.Columns.Item("ShomarehHesabSanad").Width = 110
    '        GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
    '        GridEXTitr.CurrentTable.Columns.Item("ShomarehHesabSanad").Position = 10
    '        GridEXTitr.CurrentTable.Columns.Item("ShomarehHesabSanad").EditType = Janus.Windows.GridEX.EditType.NoEdit
    '        GridEXTitr.CurrentTable.Columns.Item("ShomarehHesabSanad").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

    '        GridEXTitr.CurrentTable.Columns.Item("CodeMoshtary").Caption = "کـد مشتـری"
    '        GridEXTitr.CurrentTable.Columns.Item("CodeMoshtary").HeaderStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
    '        GridEXTitr.CurrentTable.Columns.Item("CodeMoshtary").Visible = True
    '        GridEXTitr.CurrentTable.Columns.Item("CodeMoshtary").Width = 100
    '        GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
    '        GridEXTitr.CurrentTable.Columns.Item("CodeMoshtary").Position = 11
    '        GridEXTitr.CurrentTable.Columns.Item("CodeMoshtary").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

    '        GridEXTitr.CurrentTable.Columns.Item("NameMoshtary").Caption = "نام مشتـری"
    '        GridEXTitr.CurrentTable.Columns.Item("NameMoshtary").HeaderStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
    '        GridEXTitr.CurrentTable.Columns.Item("NameMoshtary").Visible = True
    '        GridEXTitr.CurrentTable.Columns.Item("NameMoshtary").Width = 110
    '        GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
    '        GridEXTitr.CurrentTable.Columns.Item("NameMoshtary").Position = 12
    '        GridEXTitr.CurrentTable.Columns.Item("NameMoshtary").EditType = Janus.Windows.GridEX.EditType.NoEdit
    '        GridEXTitr.CurrentTable.Columns.Item("NameMoshtary").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

    '        GridEXTitr.CurrentTable.Columns.Item("Tozihat").Caption = "توضیحـات"
    '        GridEXTitr.CurrentTable.Columns.Item("Tozihat").HeaderStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
    '        GridEXTitr.CurrentTable.Columns.Item("Tozihat").Visible = True
    '        GridEXTitr.CurrentTable.Columns.Item("Tozihat").Width = 100
    '        GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
    '        GridEXTitr.CurrentTable.Columns.Item("Tozihat").Position = 13
    '        GridEXTitr.CurrentTable.Columns.Item("Tozihat").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

    '        For i As Integer = 0 To GridEXTitr.RootTable.Columns.Count - 1
    '            If GridEXTitr.RootTable.Columns(i).Type.IsValueType Then
    '                GridEXTitr.RootTable.Columns(i).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
    '                GridEXTitr.RootTable.Columns(i).FormatMode = Janus.Windows.GridEX.FormatMode.UseIFormattable
    '                GridEXTitr.RootTable.Columns(i).FormatString = "N"
    '                GridEXTitr.RootTable.Columns(i).TotalFormatMode = Janus.Windows.GridEX.FormatMode.UseIFormattable
    '                GridEXTitr.RootTable.Columns(i).TotalFormatString = "N"
    '            End If
    '        Next

    '        GridEXTitr.Visible = True
    '        'Satr=========================================================================================='
    '        If dvTitr.Count = 0 Then
    '            Exit Try
    '        End If

    '        If dvSatr.Count = 0 Then
    '            Exit Try
    '        End If

    '        With GridEXSatr
    '            .DataSource = Nothing
    '            .DataSource = dsForm.Tables("Treasury_VosolFromTabletPCsatr").DefaultView
    '            .SetDataBinding(dsForm.Tables("Treasury_VosolFromTabletPCsatr").DefaultView, "")
    '            .RetrieveStructure()
    '        End With

    '        For i As Integer = 0 To GridEXSatr.CurrentTable.Columns.Count - 1
    '            GridEXSatr.CurrentTable.Columns.Item(i).Visible = False
    '        Next

    '        GridEXSatr.CurrentTable.Columns.Item("ShomarehFaktor").Caption = "شماره فاکتور"
    '        GridEXSatr.CurrentTable.Columns.Item("ShomarehFaktor").HeaderStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
    '        GridEXSatr.CurrentTable.Columns.Item("ShomarehFaktor").Visible = True
    '        GridEXSatr.CurrentTable.Columns.Item("ShomarehFaktor").Width = 90
    '        GridEXSatr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
    '        GridEXSatr.CurrentTable.Columns.Item("ShomarehFaktor").Position = 0
    '        GridEXSatr.CurrentTable.Columns.Item("ShomarehFaktor").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

    '        GridEXSatr.CurrentTable.Columns.Item("FaktorTarikh").Caption = "تاریـخ فاکتور"
    '        GridEXSatr.CurrentTable.Columns.Item("FaktorTarikh").HeaderStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
    '        GridEXSatr.CurrentTable.Columns.Item("FaktorTarikh").Visible = True
    '        GridEXSatr.CurrentTable.Columns.Item("FaktorTarikh").Width = 90
    '        GridEXSatr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
    '        GridEXSatr.CurrentTable.Columns.Item("FaktorTarikh").Position = 1
    '        GridEXSatr.CurrentTable.Columns.Item("FaktorTarikh").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

    '        GridEXSatr.CurrentTable.Columns.Item("MablaghFaktor").Caption = "مبلغ کل فاکتور"
    '        GridEXSatr.CurrentTable.Columns.Item("MablaghFaktor").HeaderStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
    '        GridEXSatr.CurrentTable.Columns.Item("MablaghFaktor").Visible = True
    '        GridEXSatr.CurrentTable.Columns.Item("MablaghFaktor").Width = 225
    '        GridEXSatr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
    '        GridEXSatr.CurrentTable.Columns.Item("MablaghFaktor").Position = 2
    '        GridEXSatr.CurrentTable.Columns.Item("MablaghFaktor").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

    '        GridEXSatr.CurrentTable.Columns.Item("Mablagh").Caption = "مبلغ پرداختی"
    '        GridEXSatr.CurrentTable.Columns.Item("Mablagh").HeaderStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
    '        GridEXSatr.CurrentTable.Columns.Item("Mablagh").Visible = True
    '        GridEXSatr.CurrentTable.Columns.Item("Mablagh").Width = 100
    '        GridEXSatr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
    '        GridEXSatr.CurrentTable.Columns.Item("Mablagh").Position = 3
    '        GridEXSatr.CurrentTable.Columns.Item("Mablagh").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

    '        For i As Integer = 0 To GridEXSatr.RootTable.Columns.Count - 1
    '            If GridEXSatr.RootTable.Columns(i).Type.IsValueType Then
    '                GridEXSatr.RootTable.Columns(i).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
    '                GridEXSatr.RootTable.Columns(i).FormatMode = Janus.Windows.GridEX.FormatMode.UseIFormattable
    '                GridEXSatr.RootTable.Columns(i).FormatString = "N"
    '                GridEXSatr.RootTable.Columns(i).TotalFormatMode = Janus.Windows.GridEX.FormatMode.UseIFormattable
    '                GridEXSatr.RootTable.Columns(i).TotalFormatString = "N"
    '            End If
    '        Next

    '        GridEXSatr.Visible = True

    '    Catch sqlExc As SqlException
    '        MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->SetGridStyle")
    '    Catch ex As Exception
    '        MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->SetGridStyle")
    '    End Try
    'End Sub
    Private Sub SetGridStyle()
        Try
            'Titr
            '=========================================================================================='
            With GridEXTitr
                .DataSource = Nothing
                .DataSource = dsForm.Tables("Treasury_VosolFromTabletPC").DefaultView
                .SetDataBinding(dsForm.Tables("Treasury_VosolFromTabletPC").DefaultView, "")
                .RetrieveStructure()
            End With

            For i As Integer = 0 To GridEXTitr.CurrentTable.Columns.Count - 1
                GridEXTitr.CurrentTable.Columns.Item(i).Visible = False
            Next

            GridEXTitr.CurrentTable.Columns.Item("Taeed").Caption = "انتخاب"
            GridEXTitr.CurrentTable.Columns.Item("Taeed").HeaderStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXTitr.CurrentTable.Columns.Item("Taeed").Visible = True
            GridEXTitr.CurrentTable.Columns.Item("Taeed").Width = 50
            GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXTitr.CurrentTable.Columns.Item("Taeed").Position = 0
            GridEXTitr.CurrentTable.Columns.Item("Taeed").Selectable = True
            GridEXTitr.CurrentTable.Columns.Item("Taeed").ActAsSelector = True
            GridEXTitr.CurrentTable.Columns.Item("Taeed").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXTitr.CurrentTable.Columns.Item("CodeForoshandeh").Caption = "کد فروشنده"
            GridEXTitr.CurrentTable.Columns.Item("CodeForoshandeh").HeaderStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXTitr.CurrentTable.Columns.Item("CodeForoshandeh").Visible = True
            GridEXTitr.CurrentTable.Columns.Item("CodeForoshandeh").Width = 80
            GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXTitr.CurrentTable.Columns.Item("CodeForoshandeh").Position = 1
            GridEXTitr.CurrentTable.Columns.Item("CodeForoshandeh").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXTitr.CurrentTable.Columns.Item("NameForoshandeh").Caption = "نام فروشنده"
            GridEXTitr.CurrentTable.Columns.Item("NameForoshandeh").HeaderStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXTitr.CurrentTable.Columns.Item("NameForoshandeh").Visible = True
            GridEXTitr.CurrentTable.Columns.Item("NameForoshandeh").Width = 180
            GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXTitr.CurrentTable.Columns.Item("NameForoshandeh").Position = 2
            GridEXTitr.CurrentTable.Columns.Item("NameForoshandeh").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXTitr.CurrentTable.Columns.Item("CodeMoshtary").Caption = "کـد مشتـری"
            GridEXTitr.CurrentTable.Columns.Item("CodeMoshtary").HeaderStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXTitr.CurrentTable.Columns.Item("CodeMoshtary").Visible = True
            GridEXTitr.CurrentTable.Columns.Item("CodeMoshtary").Width = 100
            GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXTitr.CurrentTable.Columns.Item("CodeMoshtary").Position = 3
            GridEXTitr.CurrentTable.Columns.Item("CodeMoshtary").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXTitr.CurrentTable.Columns.Item("NameMoshtary").Caption = "نام مشتـری"
            GridEXTitr.CurrentTable.Columns.Item("NameMoshtary").HeaderStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXTitr.CurrentTable.Columns.Item("NameMoshtary").Visible = True
            GridEXTitr.CurrentTable.Columns.Item("NameMoshtary").Width = 180
            GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXTitr.CurrentTable.Columns.Item("NameMoshtary").Position = 4
            GridEXTitr.CurrentTable.Columns.Item("NameMoshtary").EditType = Janus.Windows.GridEX.EditType.NoEdit
            GridEXTitr.CurrentTable.Columns.Item("NameMoshtary").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXTitr.CurrentTable.Columns.Item("txtNoeSanad").Caption = "نوع سنـد"
            GridEXTitr.CurrentTable.Columns.Item("txtNoeSanad").HeaderStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXTitr.CurrentTable.Columns.Item("txtNoeSanad").Visible = True
            GridEXTitr.CurrentTable.Columns.Item("txtNoeSanad").Width = 90
            GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXTitr.CurrentTable.Columns.Item("txtNoeSanad").Position = 5
            GridEXTitr.CurrentTable.Columns.Item("txtNoeSanad").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXTitr.CurrentTable.Columns.Item("Mablagh").Caption = "مبلغ سنـد"
            GridEXTitr.CurrentTable.Columns.Item("Mablagh").HeaderStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXTitr.CurrentTable.Columns.Item("Mablagh").Visible = True
            GridEXTitr.CurrentTable.Columns.Item("Mablagh").Width = 90
            GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXTitr.CurrentTable.Columns.Item("Mablagh").Position = 6
            GridEXTitr.CurrentTable.Columns.Item("Mablagh").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXTitr.CurrentTable.Columns.Item("NameBank").Caption = "بانک سنـد"
            GridEXTitr.CurrentTable.Columns.Item("NameBank").HeaderStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXTitr.CurrentTable.Columns.Item("NameBank").Visible = True
            GridEXTitr.CurrentTable.Columns.Item("NameBank").Width = 100
            GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXTitr.CurrentTable.Columns.Item("NameBank").Position = 7
            GridEXTitr.CurrentTable.Columns.Item("NameBank").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXTitr.CurrentTable.Columns.Item("SeriHarfi").Caption = "سری حرفی"
            GridEXTitr.CurrentTable.Columns.Item("SeriHarfi").HeaderStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXTitr.CurrentTable.Columns.Item("SeriHarfi").Visible = True
            GridEXTitr.CurrentTable.Columns.Item("SeriHarfi").Width = 100
            GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXTitr.CurrentTable.Columns.Item("SeriHarfi").Position = 8
            GridEXTitr.CurrentTable.Columns.Item("SeriHarfi").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXTitr.CurrentTable.Columns.Item("SeriAdadi").Caption = "سری عـددی"
            GridEXTitr.CurrentTable.Columns.Item("SeriAdadi").HeaderStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXTitr.CurrentTable.Columns.Item("SeriAdadi").Visible = True
            GridEXTitr.CurrentTable.Columns.Item("SeriAdadi").Width = 100
            GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXTitr.CurrentTable.Columns.Item("SeriAdadi").Position = 9
            GridEXTitr.CurrentTable.Columns.Item("SeriAdadi").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXTitr.CurrentTable.Columns.Item("ShomarehSanad").Caption = "شماره سنـد"
            GridEXTitr.CurrentTable.Columns.Item("ShomarehSanad").HeaderStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXTitr.CurrentTable.Columns.Item("ShomarehSanad").Visible = True
            GridEXTitr.CurrentTable.Columns.Item("ShomarehSanad").Width = 100
            GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXTitr.CurrentTable.Columns.Item("ShomarehSanad").Position = 10
            GridEXTitr.CurrentTable.Columns.Item("ShomarehSanad").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXTitr.CurrentTable.Columns.Item("TarikhSanad").Caption = "تاریخ سنـد"
            GridEXTitr.CurrentTable.Columns.Item("TarikhSanad").HeaderStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXTitr.CurrentTable.Columns.Item("TarikhSanad").Visible = True
            GridEXTitr.CurrentTable.Columns.Item("TarikhSanad").Width = 80
            GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXTitr.CurrentTable.Columns.Item("TarikhSanad").Position = 11
            GridEXTitr.CurrentTable.Columns.Item("TarikhSanad").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXTitr.CurrentTable.Columns.Item("ShomarehHesabSanad").Caption = "شماره حساب"
            GridEXTitr.CurrentTable.Columns.Item("ShomarehHesabSanad").HeaderStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXTitr.CurrentTable.Columns.Item("ShomarehHesabSanad").Visible = True
            GridEXTitr.CurrentTable.Columns.Item("ShomarehHesabSanad").Width = 120
            GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXTitr.CurrentTable.Columns.Item("ShomarehHesabSanad").Position = 12
            GridEXTitr.CurrentTable.Columns.Item("ShomarehHesabSanad").EditType = Janus.Windows.GridEX.EditType.NoEdit
            GridEXTitr.CurrentTable.Columns.Item("ShomarehHesabSanad").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXTitr.CurrentTable.Columns.Item("Tozihat").Caption = "توضیحـات"
            GridEXTitr.CurrentTable.Columns.Item("Tozihat").HeaderStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXTitr.CurrentTable.Columns.Item("Tozihat").Visible = True
            GridEXTitr.CurrentTable.Columns.Item("Tozihat").Width = 300
            GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXTitr.CurrentTable.Columns.Item("Tozihat").Position = 13
            GridEXTitr.CurrentTable.Columns.Item("Tozihat").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXTitr.CurrentTable.Columns.Item("ccImportPaymentTablet").Caption = "cc"
            GridEXTitr.CurrentTable.Columns.Item("ccImportPaymentTablet").HeaderStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXTitr.CurrentTable.Columns.Item("ccImportPaymentTablet").Visible = False
            GridEXTitr.CurrentTable.Columns.Item("ccImportPaymentTablet").Width = 0
            GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXTitr.CurrentTable.Columns.Item("ccImportPaymentTablet").Position = 14
            GridEXTitr.CurrentTable.Columns.Item("ccImportPaymentTablet").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXTitr.CurrentTable.Columns.Item("TarikhErsal").Caption = "TarikhErsal"
            GridEXTitr.CurrentTable.Columns.Item("TarikhErsal").HeaderStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXTitr.CurrentTable.Columns.Item("TarikhErsal").Visible = False
            GridEXTitr.CurrentTable.Columns.Item("TarikhErsal").Width = 0
            GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXTitr.CurrentTable.Columns.Item("TarikhErsal").Position = 15
            GridEXTitr.CurrentTable.Columns.Item("TarikhErsal").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXTitr.CurrentTable.Columns.Item("ccForoshandeh").Caption = "ccForoshandeh"
            GridEXTitr.CurrentTable.Columns.Item("ccForoshandeh").HeaderStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXTitr.CurrentTable.Columns.Item("ccForoshandeh").Visible = False
            GridEXTitr.CurrentTable.Columns.Item("ccForoshandeh").Width = 0
            GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXTitr.CurrentTable.Columns.Item("ccForoshandeh").Position = 16
            GridEXTitr.CurrentTable.Columns.Item("ccForoshandeh").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXTitr.CurrentTable.Columns.Item("ccMoshtary").Caption = "ccMoshtary"
            GridEXTitr.CurrentTable.Columns.Item("ccMoshtary").HeaderStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXTitr.CurrentTable.Columns.Item("ccMoshtary").Visible = False
            GridEXTitr.CurrentTable.Columns.Item("ccMoshtary").Width = 0
            GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXTitr.CurrentTable.Columns.Item("ccMoshtary").Position = 17
            GridEXTitr.CurrentTable.Columns.Item("ccMoshtary").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            For i As Integer = 0 To GridEXTitr.RootTable.Columns.Count - 1
                If GridEXTitr.RootTable.Columns(i).Type.IsValueType Then
                    GridEXTitr.RootTable.Columns(i).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                    GridEXTitr.RootTable.Columns(i).FormatMode = Janus.Windows.GridEX.FormatMode.UseIFormattable
                    GridEXTitr.RootTable.Columns(i).FormatString = "N"
                    GridEXTitr.RootTable.Columns(i).TotalFormatMode = Janus.Windows.GridEX.FormatMode.UseIFormattable
                    GridEXTitr.RootTable.Columns(i).TotalFormatString = "N"
                End If
            Next

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->SetGridStyle")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->SetGridStyle")
        End Try
    End Sub
    Private Sub SetGridStyleSatr()
        Try
            'Satr=========================================================================================='
            If dvTitr.Count = 0 Then
                Exit Try
            End If

            If dvSatr.Count = 0 Then
                Exit Try
            End If

            With GridEXSatr
                .DataSource = Nothing
                .DataSource = dsForm.Tables("Treasury_VosolFromTabletPCsatr").DefaultView
                .SetDataBinding(dsForm.Tables("Treasury_VosolFromTabletPCsatr").DefaultView, "")
                .RetrieveStructure()
            End With

            For i As Integer = 0 To GridEXSatr.CurrentTable.Columns.Count - 1
                GridEXSatr.CurrentTable.Columns.Item(i).Visible = False
            Next

            GridEXSatr.CurrentTable.Columns.Item("ShomarehFaktor").Caption = "شماره فاکتور"
            GridEXSatr.CurrentTable.Columns.Item("ShomarehFaktor").HeaderStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXSatr.CurrentTable.Columns.Item("ShomarehFaktor").Visible = True
            GridEXSatr.CurrentTable.Columns.Item("ShomarehFaktor").Width = 90
            GridEXSatr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXSatr.CurrentTable.Columns.Item("ShomarehFaktor").Position = 0
            GridEXSatr.CurrentTable.Columns.Item("ShomarehFaktor").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXSatr.CurrentTable.Columns.Item("FaktorTarikh").Caption = "تاریـخ فاکتور"
            GridEXSatr.CurrentTable.Columns.Item("FaktorTarikh").HeaderStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXSatr.CurrentTable.Columns.Item("FaktorTarikh").Visible = True
            GridEXSatr.CurrentTable.Columns.Item("FaktorTarikh").Width = 90
            GridEXSatr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXSatr.CurrentTable.Columns.Item("FaktorTarikh").Position = 1
            GridEXSatr.CurrentTable.Columns.Item("FaktorTarikh").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXSatr.CurrentTable.Columns.Item("MablaghFaktor").Caption = "مبلغ کل فاکتور"
            GridEXSatr.CurrentTable.Columns.Item("MablaghFaktor").HeaderStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXSatr.CurrentTable.Columns.Item("MablaghFaktor").Visible = True
            GridEXSatr.CurrentTable.Columns.Item("MablaghFaktor").Width = 225
            GridEXSatr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXSatr.CurrentTable.Columns.Item("MablaghFaktor").Position = 2
            GridEXSatr.CurrentTable.Columns.Item("MablaghFaktor").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXSatr.CurrentTable.Columns.Item("Mablagh").Caption = "مبلغ پرداختی"
            GridEXSatr.CurrentTable.Columns.Item("Mablagh").HeaderStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXSatr.CurrentTable.Columns.Item("Mablagh").Visible = True
            GridEXSatr.CurrentTable.Columns.Item("Mablagh").Width = 100
            GridEXSatr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXSatr.CurrentTable.Columns.Item("Mablagh").Position = 3
            GridEXSatr.CurrentTable.Columns.Item("Mablagh").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            For i As Integer = 0 To GridEXSatr.RootTable.Columns.Count - 1
                If GridEXSatr.RootTable.Columns(i).Type.IsValueType Then
                    GridEXSatr.RootTable.Columns(i).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                    GridEXSatr.RootTable.Columns(i).FormatMode = Janus.Windows.GridEX.FormatMode.UseIFormattable
                    GridEXSatr.RootTable.Columns(i).FormatString = "N"
                    GridEXSatr.RootTable.Columns(i).TotalFormatMode = Janus.Windows.GridEX.FormatMode.UseIFormattable
                    GridEXSatr.RootTable.Columns(i).TotalFormatString = "N"
                End If
            Next

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->SetGridStyle")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->SetGridStyle")
        End Try
    End Sub
    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Close()
    End Sub

    Private Sub GridEXTitr_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GridEXTitr.Click
        BoundCurrencyManagerTitr()
        SetGridStyleSatr()
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Search()
    End Sub

    Private Sub GridEXTitr_RowCheckStateChanged(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.RowCheckStateChangeEventArgs) Handles GridEXTitr.RowCheckStateChanged
        If GridEXTitr.GetCheckedRows().Length = 0 Then
            btnSave.Enabled = False
        Else
            btnSave.Enabled = True
        End If
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click

        If MsgBox("آیا به انتخاب / انتخاب های خود اطمینان دارید ؟", MsgBoxStyle.YesNo + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Question, "") = MsgBoxResult.No Then
            Exit Sub
        End If

        For i As Integer = 0 To GridEXTitr.GetCheckedRows().Length - 1

            Dim TedadFaktor As Integer = objTools.DCount("ImportPaymentOrderTablet", "TabletPC.ImportPaymentOrderFromTablet", "ccImportPaymentTablet = " & GridEXTitr.GetCheckedRows(i).Cells("ccImportPaymentTablet").Value)

            Dim cn As New SqlConnection
            Dim cm As New SqlCommand
            Dim da As New SqlDataAdapter
            Dim dt As New DataTable
            Dim p As New SqlParameter
            Dim strSQL As String = ""
            Dim ccDariaftPardakht As Integer = 0
            Dim ccDariaftPardakhtDarkhastFaktor As Integer = 0

            tCodeCounter = 0

            strSQL = "TabletPC.ImportPayment_Tablet "

            cn = New SqlConnection(ConnectionString)
            cn.Open()

            cm = New SqlCommand(strSQL, cn)
            cm.CommandType = CommandType.StoredProcedure
            cm.Parameters.Clear()

            cm.Parameters.AddWithValue("ccForoshandeh", GridEXTitr.GetCheckedRows(i).Cells("ccForoshandeh").Value)
            cm.Parameters.AddWithValue("CodeNoeSanad", GridEXTitr.GetCheckedRows(i).Cells("CodeNoeSanad").Value)
            cm.Parameters.AddWithValue("Mablagh", GridEXTitr.GetCheckedRows(i).Cells("Mablagh").Value)
            cm.Parameters.AddWithValue("ccBankSanad", GridEXTitr.GetCheckedRows(i).Cells("ccBankSanad").Value)
            cm.Parameters.AddWithValue("SeriHarfi", GridEXTitr.GetCheckedRows(i).Cells("SeriHarfi").Value)
            cm.Parameters.AddWithValue("SeriAdadi", GridEXTitr.GetCheckedRows(i).Cells("SeriAdadi").Value)
            cm.Parameters.AddWithValue("ShomarehSanad", GridEXTitr.GetCheckedRows(i).Cells("ShomarehSanad").Value)
            cm.Parameters.AddWithValue("PosTransactionNumber", GridEXTitr.GetCheckedRows(i).Cells("PosTransactionNumber").Value)
            cm.Parameters.AddWithValue("PosTerrminalNumber", GridEXTitr.GetCheckedRows(i).Cells("PosTerrminalNumber").Value)
            cm.Parameters.AddWithValue("TarikhSanad", GridEXTitr.GetCheckedRows(i).Cells("TarikhSanad").Value)
            cm.Parameters.AddWithValue("ShomarehHesabSanad", GridEXTitr.GetCheckedRows(i).Cells("ShomarehHesabSanad").Value)
            cm.Parameters.AddWithValue("ccMoshtary", GridEXTitr.GetCheckedRows(i).Cells("ccMoshtary").Value)
            cm.Parameters.AddWithValue("Tozihat", GridEXTitr.GetCheckedRows(i).Cells("Tozihat").Value)
            cm.Parameters.AddWithValue("TarikhErsal", GridEXTitr.GetCheckedRows(i).Cells("TarikhErsal").Value)
            cm.Parameters.AddWithValue("ccDariaftPardakht", ccDariaftPardakht)
            cm.Parameters("ccDariaftPardakht").Direction = ParameterDirection.Output

            cm.ExecuteNonQuery()

            tCodeCounter = cm.Parameters("ccDariaftPardakht").Value

            If TedadFaktor <> 0 Then

                Dim drv As DataRowView
                Dim PardakhtiFaktor As Integer

                For Each drv In dvSatr

                    PardakhtiFaktor = objTools.DSum("Mablagh", "tblDP_AmalyatFaktor", "ccFaktorTitr = " & drv("ccDarKhastFaktor") & " AND CodeAmalyat NOT IN (Select CodeAmalyat From tblDP_Amalyat Where Ebtal = 1)")

                    If PardakhtiFaktor + drv("Mablagh") > drv("MablaghFaktor") Then
                        If MsgBox("امکان ثبت این پرداختی برای فاکتور شماره " & drv("ShomarehFaktor") & " وجود ندارد . زیرا مجموع پرداختی های این فکتور بیشتر از مبلغ کل فاکتور می شود. " & vbCrLf & " آیا این پرداختی در خزانه به عنوان بستانکاری مشتری ذخیره گردد ؟", MsgBoxStyle.YesNo + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Question, "") = MsgBoxResult.Yes Then
                            MsgBox("ثبت با موفقیت انجام گرفت .", MsgBoxStyle.YesNo + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Question, "")
                            Exit Sub
                        Else
                            strSQL = "Treasury.spVosolFromTabletPC_DeleteAmalyat"

                            cm = New SqlCommand(strSQL, cn)
                            cm.CommandType = CommandType.StoredProcedure
                            cm.Parameters.Clear()

                            cm.Parameters.AddWithValue("CodeAmalyat", tCodeCounter)

                            cm.ExecuteNonQuery()
                            Exit Sub
                        End If
                    End If

                    strSQL = "TabletPC.ImportPaymentOrder_Tablet "

                    cm = New SqlCommand(strSQL, cn)
                    cm.CommandType = CommandType.StoredProcedure
                    cm.Parameters.Clear()

                    cm.Parameters.AddWithValue("ccDariaftPardakht", tCodeCounter)
                    cm.Parameters.AddWithValue("ccDarKhastFaktor", drv("ccDarKhastFaktor"))
                    cm.Parameters.AddWithValue("Sal", drv("Sal"))
                    cm.Parameters.AddWithValue("ShomarehFaktor", drv("ShomarehFaktor"))
                    cm.Parameters.AddWithValue("Mablagh", drv("Mablagh"))
                    cm.Parameters.AddWithValue("ccDariaftPardakhtDarkhastFaktor", ccDariaftPardakhtDarkhastFaktor)

                    cm.ExecuteNonQuery()
                Next

            End If

            UpdateVazeiatImportPayment(GridEXTitr.GetCheckedRows(i).Cells("ccImportPaymentTablet").Value)

            cm.Connection.Close()
            cn.Close()
            cm = Nothing
            da = Nothing

        Next

        Search()
    End Sub
    Private Sub UpdateVazeiatImportPayment(ByVal ccImportPaymentTablet As Integer)
        Dim cn As New SqlConnection
        Dim cm As New SqlCommand
        Dim strSQL As String = ""

        strSQL = "Treasury.spVosolFromTabletPC_Update "

        cn = New SqlConnection(ConnectionString)
        cn.Open()

        cm = New SqlCommand(strSQL, cn)
        cm.CommandType = CommandType.StoredProcedure
        cm.Parameters.Clear()

        cm.Parameters.AddWithValue("ccImportPaymentTablet", ccImportPaymentTablet)

        cm.ExecuteNonQuery()

        cm.Connection.Close()
        cn.Close()

    End Sub
End Class
