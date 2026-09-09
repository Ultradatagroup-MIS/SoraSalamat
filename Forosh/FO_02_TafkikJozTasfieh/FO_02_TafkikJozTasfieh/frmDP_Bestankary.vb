Public Class frmDP_Bestankary
#Region "Variable AND Constant Declration"
    ' Security 
    Dim dvBestanKary As DataView
    Dim dvPardakhty As DataView
    Dim tCodeCounter As Long
    
    Dim cmTitr As CurrencyManager
    Dim cmSatr As CurrencyManager
    Dim cmPardakhty As CurrencyManager
    Dim cmBestankary As CurrencyManager

    Dim ErrPro As New ErrorProvider
    Dim dsForm As New DataSet

    Dim txtCaption As String
    Public B_ccTafkikJozeTasfiehSatr As Integer
    Public B_ccFaktor As Integer
    Public B_FaktorShomareh As String
    Public B_FaktorTarikh As String
    Public B_MablaghFaktor As Integer
    Public B_PardakhtiFaktor As Integer
    Public B_MandehFaktor As Integer
    Public B_ccMoshtary As Integer
    Public B_NameMoshtary As String

    Private WithEvents BS As New UD_Dll.PassString
#End Region
    Private Sub frmDP_Bestankary_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetParameter()
        LoadForm()
        RefreshPardakhty()
        RefreshBestankary()
        Me.Text = Me.Text + " " + B_NameMoshtary
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
            CodeDoreh = "1392"
            txtCaption = " بستانکاری های "
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
    Private Sub LoadForm()
        txtShomarehFaktor.Text = B_FaktorShomareh
        txtTarikhFaktor.Text = B_FaktorTarikh
        txtMablaghFaktor.Text = ObjCode.DigitSeprator(B_MablaghFaktor)
        txtPardakhtiFaktor.Text = ObjCode.DigitSeprator(B_PardakhtiFaktor)
        txtMandehFaktor.Text = ObjCode.DigitSeprator(B_MandehFaktor)
    End Sub
    Private Sub RefreshPardakhty()
        Try
            Dim StrSQL As String
            Dim cnSQL As New SqlConnection
            Dim cmSQL As New SqlCommand
            Dim daSQL As SqlDataAdapter

            If dsForm.Tables.Contains("Sales_Pardakhty") Then
                dsForm.Tables.Remove("Sales_Pardakhty")
            End If

            StrSQL = "Sales.spTafkikJozTasfieh_SearchPardakhtyFromBestankary "

            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()

            cmSQL = New SqlCommand(Strsql, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            cmSQL.Parameters.AddWithValue("ccFaktorTitr", B_ccFaktor)

            daSQL = New SqlDataAdapter(cmSQL)
            daSQL.Fill(dsForm, "Sales_Pardakhty")

            dvPardakhty = New DataView(dsForm.Tables("Sales_Pardakhty"))

            dvPardakhty.AllowNew = False
            dvPardakhty.AllowDelete = True
            dvPardakhty.AllowEdit = False

            daSQL = Nothing
            GridEXPardakhty.DataSource = Nothing
            GridEXPardakhty.DataSource = dvPardakhty
            cnSQL.Close()
            BoundCurrencyManagerPardakhty()
            SetGridStylePardakhty()

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> RefreshPardakhty ")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> RefreshPardakhty ")
        End Try
    End Sub
    Private Sub RefreshBestankary()
        Try
            Dim Strsql As String
            Dim cnSQL As New SqlConnection
            Dim cmSQL As New SqlCommand
            Dim daSQL As SqlDataAdapter

            If dsForm.Tables.Contains("Sales_Bestankary") Then
                dsForm.Tables.Remove("Sales_Bestankary")
            End If

            Strsql = "Sales.spTafkikJozTasfieh_SearchBestankary "

            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()

            cmSQL = New SqlCommand(Strsql, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            cmSQL.Parameters.AddWithValue("ccMoshtary", B_ccMoshtary)

            daSQL = New SqlDataAdapter(cmSQL)
            daSQL.Fill(dsForm, "Sales_Bestankary")

            dvBestanKary = New DataView(dsForm.Tables("Sales_Bestankary"))

            dvBestanKary.AllowNew = False
            dvBestanKary.AllowDelete = False
            dvBestanKary.AllowEdit = True

            daSQL = Nothing
            GridEXBestankary.DataSource = Nothing
            GridEXBestankary.DataSource = dvBestanKary
            cnSQL.Close()
            BoundCurrencyManagerBestankary()
            SetGridStyleBestanKari()

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> RefreshBestankary ")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> RefreshBestankary ")
        End Try

    End Sub
    Private Sub BoundCurrencyManagerPardakhty()
        Try
            cmPardakhty = CType(BindingContext(GridEXPardakhty.DataSource), CurrencyManager)
            AddHandler cmPardakhty.ItemChanged, AddressOf cmPardakhty_ItemChanged
            AddHandler cmPardakhty.PositionChanged, AddressOf cmPardakhty_PositionChanged
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> BoundCurrencyManagerPardakhty ")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> BoundCurrencyManagerPardakhty ")
        End Try

    End Sub
    Private Sub BoundCurrencyManagerBestankary()
        Try
            cmBestankary = CType(BindingContext(GridEXBestankary.DataSource), CurrencyManager)
            AddHandler cmBestankary.ItemChanged, AddressOf cmBestankary_ItemChanged
            AddHandler cmBestankary.PositionChanged, AddressOf cmBestankary_PositionChanged
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->BoundCurrencyManagerBestankary")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->BoundCurrencyManagerBestankary")
        End Try

    End Sub
    Private Sub cmPardakhty_ItemChanged(ByVal sender As Object, ByVal e As ItemChangedEventArgs)
        Try
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> cmPardakhty_ItemChanged ")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> cmPardakhty_ItemChanged ")
        End Try

    End Sub
    Private Sub cmBestankary_ItemChanged(ByVal sender As Object, ByVal e As ItemChangedEventArgs)
        Try
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->cmBestankary_ItemChanged")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->cmBestankary_ItemChanged")
        End Try

    End Sub
    Private Sub cmPardakhty_PositionChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->cmBestankary_PositionChanged")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->cmBestankary_PositionChanged")
        End Try

    End Sub
    Private Sub cmBestankary_PositionChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->cmBestankary_PositionChanged")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->cmBestankary_PositionChanged")
        End Try

    End Sub
    Private Sub SetGridStylePardakhty()
        Try
            If dvPardakhty.Count = 0 Then
                Exit Try
            End If

            With GridEXPardakhty
                .DataSource = Nothing
                .DataSource = dsForm.Tables("Sales_Pardakhty").DefaultView
                .SetDataBinding(dsForm.Tables("Sales_Pardakhty").DefaultView, "")
                .RetrieveStructure()
            End With

            For i As Integer = 0 To GridEXPardakhty.CurrentTable.Columns.Count - 1
                GridEXPardakhty.CurrentTable.Columns.Item(i).Visible = False
            Next

            GridEXPardakhty.CurrentTable.Columns.Item("txtNoeSanad").Caption = " نــوع سنـــد"
            GridEXPardakhty.CurrentTable.Columns.Item("txtNoeSanad").HeaderStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXPardakhty.CurrentTable.Columns.Item("txtNoeSanad").Visible = True
            GridEXPardakhty.CurrentTable.Columns.Item("txtNoeSanad").Width = 190
            GridEXPardakhty.CurrentTable.Columns.Item("txtNoeSanad").EditType = Janus.Windows.GridEX.EditType.NoEdit
            GridEXPardakhty.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXPardakhty.CurrentTable.Columns.Item("txtNoeSanad").Position = 0
            GridEXPardakhty.CurrentTable.Columns.Item("txtNoeSanad").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXPardakhty.CurrentTable.Columns.Item("txtNoeAmalyat").Caption = "نــوع عملیــات"
            GridEXPardakhty.CurrentTable.Columns.Item("txtNoeAmalyat").HeaderStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXPardakhty.CurrentTable.Columns.Item("txtNoeAmalyat").Visible = True
            GridEXPardakhty.CurrentTable.Columns.Item("txtNoeAmalyat").Width = 190
            GridEXPardakhty.CurrentTable.Columns.Item("txtNoeAmalyat").EditType = Janus.Windows.GridEX.EditType.NoEdit
            GridEXPardakhty.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXPardakhty.CurrentTable.Columns.Item("txtNoeAmalyat").Position = 1
            GridEXPardakhty.CurrentTable.Columns.Item("txtNoeAmalyat").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXPardakhty.CurrentTable.Columns.Item("TarikhDPSlash").Caption = "تاریخ دریافت"
            GridEXPardakhty.CurrentTable.Columns.Item("TarikhDPSlash").HeaderStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXPardakhty.CurrentTable.Columns.Item("TarikhDPSlash").Visible = True
            GridEXPardakhty.CurrentTable.Columns.Item("TarikhDPSlash").Width = 110
            GridEXPardakhty.CurrentTable.Columns.Item("TarikhDPSlash").EditType = Janus.Windows.GridEX.EditType.NoEdit
            GridEXPardakhty.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXPardakhty.CurrentTable.Columns.Item("TarikhDPSlash").Position = 2
            GridEXPardakhty.CurrentTable.Columns.Item("TarikhDPSlash").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXPardakhty.CurrentTable.Columns.Item("ShomarehSanad").Caption = "شماره سنــد"
            GridEXPardakhty.CurrentTable.Columns.Item("ShomarehSanad").HeaderStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXPardakhty.CurrentTable.Columns.Item("ShomarehSanad").Visible = True
            GridEXPardakhty.CurrentTable.Columns.Item("ShomarehSanad").Width = 110
            GridEXPardakhty.CurrentTable.Columns.Item("ShomarehSanad").EditType = Janus.Windows.GridEX.EditType.NoEdit
            GridEXPardakhty.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXPardakhty.CurrentTable.Columns.Item("ShomarehSanad").Position = 3
            GridEXPardakhty.CurrentTable.Columns.Item("ShomarehSanad").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXPardakhty.CurrentTable.Columns.Item("txtAkharinVazeiat").Caption = "آخرین وضعیت"
            GridEXPardakhty.CurrentTable.Columns.Item("txtAkharinVazeiat").HeaderStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXPardakhty.CurrentTable.Columns.Item("txtAkharinVazeiat").Visible = True
            GridEXPardakhty.CurrentTable.Columns.Item("txtAkharinVazeiat").Width = 150
            GridEXPardakhty.CurrentTable.Columns.Item("txtAkharinVazeiat").EditType = Janus.Windows.GridEX.EditType.NoEdit
            GridEXPardakhty.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXPardakhty.CurrentTable.Columns.Item("txtAkharinVazeiat").Position = 4
            GridEXPardakhty.CurrentTable.Columns.Item("txtAkharinVazeiat").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXPardakhty.CurrentTable.Columns.Item("MablaghVajh").Caption = "مبلغ سند"
            GridEXPardakhty.CurrentTable.Columns.Item("MablaghVajh").HeaderStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXPardakhty.CurrentTable.Columns.Item("MablaghVajh").Visible = True
            GridEXPardakhty.CurrentTable.Columns.Item("MablaghVajh").Width = 140
            GridEXPardakhty.CurrentTable.Columns.Item("MablaghVajh").EditType = Janus.Windows.GridEX.EditType.NoEdit
            GridEXPardakhty.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXPardakhty.CurrentTable.Columns.Item("MablaghVajh").Position = 5
            GridEXPardakhty.CurrentTable.Columns.Item("MablaghVajh").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXPardakhty.CurrentTable.Columns.Item("Mablagh").Caption = "پرداختی برای فاکتور"
            GridEXPardakhty.CurrentTable.Columns.Item("Mablagh").HeaderStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXPardakhty.CurrentTable.Columns.Item("Mablagh").Visible = True
            GridEXPardakhty.CurrentTable.Columns.Item("Mablagh").Width = 140
            GridEXPardakhty.CurrentTable.Columns.Item("Mablagh").EditType = Janus.Windows.GridEX.EditMode.EditOff
            GridEXPardakhty.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOn
            GridEXPardakhty.CurrentTable.Columns.Item("Mablagh").Position = 6
            GridEXPardakhty.CurrentTable.Columns.Item("Mablagh").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXPardakhty.CurrentTable.Columns.Item("ccAmalyatFaktor").Caption = "ccAmalyatFaktor"
            GridEXPardakhty.CurrentTable.Columns.Item("ccAmalyatFaktor").HeaderStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXPardakhty.CurrentTable.Columns.Item("ccAmalyatFaktor").Visible = False
            GridEXPardakhty.CurrentTable.Columns.Item("ccAmalyatFaktor").Width = 0
            GridEXPardakhty.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXPardakhty.CurrentTable.Columns.Item("ccAmalyatFaktor").Position = 7
            GridEXPardakhty.CurrentTable.Columns.Item("ccAmalyatFaktor").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            For i As Integer = 0 To GridEXPardakhty.RootTable.Columns.Count - 1
                If GridEXPardakhty.RootTable.Columns(i).Type.IsValueType Then
                    GridEXPardakhty.RootTable.Columns(i).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                    GridEXPardakhty.RootTable.Columns(i).FormatMode = Janus.Windows.GridEX.FormatMode.UseIFormattable
                    GridEXPardakhty.RootTable.Columns(i).FormatString = "N"
                    GridEXPardakhty.RootTable.Columns(i).TotalFormatMode = Janus.Windows.GridEX.FormatMode.UseIFormattable
                    GridEXPardakhty.RootTable.Columns(i).TotalFormatString = "N"
                End If
            Next

            GridEXPardakhty.Visible = True

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> SetGridStylePardakhty ")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> SetGridStylePardakhty ")
        End Try

    End Sub
    Private Sub SetGridStyleBestanKari()
        Try
            If dvBestanKary.Count = 0 Then
                Exit Try
            End If

            With GridEXBestankary
                .DataSource = Nothing
                .DataSource = dsForm.Tables("Sales_Bestankary").DefaultView
                .SetDataBinding(dsForm.Tables("Sales_Bestankary").DefaultView, "")
                .RetrieveStructure()
            End With

            For i As Integer = 0 To GridEXBestankary.CurrentTable.Columns.Count - 1
                GridEXBestankary.CurrentTable.Columns.Item(i).Visible = False
            Next

            GridEXBestankary.CurrentTable.Columns.Item("RowNumber").Caption = "ردیف"
            GridEXBestankary.CurrentTable.Columns.Item("RowNumber").HeaderStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXBestankary.CurrentTable.Columns.Item("RowNumber").Visible = True
            GridEXBestankary.CurrentTable.Columns.Item("RowNumber").Width = 50
            GridEXBestankary.CurrentTable.Columns.Item("RowNumber").EditType = Janus.Windows.GridEX.EditType.NoEdit
            GridEXBestankary.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXBestankary.CurrentTable.Columns.Item("RowNumber").Position = 0
            GridEXBestankary.CurrentTable.Columns.Item("RowNumber").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXBestankary.CurrentTable.Columns.Item("txtsNoeSanad").Caption = "نــــوع"
            GridEXBestankary.CurrentTable.Columns.Item("txtsNoeSanad").HeaderStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXBestankary.CurrentTable.Columns.Item("txtsNoeSanad").Visible = True
            GridEXBestankary.CurrentTable.Columns.Item("txtsNoeSanad").Width = 200
            GridEXBestankary.CurrentTable.Columns.Item("txtsNoeSanad").EditType = Janus.Windows.GridEX.EditType.NoEdit
            GridEXBestankary.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXBestankary.CurrentTable.Columns.Item("txtsNoeSanad").Position = 1
            GridEXBestankary.CurrentTable.Columns.Item("txtsNoeSanad").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXBestankary.CurrentTable.Columns.Item("TarikhDPSlash").Caption = "تاریخ دریافت"
            GridEXBestankary.CurrentTable.Columns.Item("TarikhDPSlash").HeaderStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXBestankary.CurrentTable.Columns.Item("TarikhDPSlash").Visible = True
            GridEXBestankary.CurrentTable.Columns.Item("TarikhDPSlash").Width = 100
            GridEXBestankary.CurrentTable.Columns.Item("TarikhDPSlash").EditType = Janus.Windows.GridEX.EditType.NoEdit
            GridEXBestankary.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXBestankary.CurrentTable.Columns.Item("TarikhDPSlash").Position = 2
            GridEXBestankary.CurrentTable.Columns.Item("TarikhDPSlash").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXBestankary.CurrentTable.Columns.Item("ShomarehSanad").Caption = "شماره چک"
            GridEXBestankary.CurrentTable.Columns.Item("ShomarehSanad").HeaderStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXBestankary.CurrentTable.Columns.Item("ShomarehSanad").Visible = True
            GridEXBestankary.CurrentTable.Columns.Item("ShomarehSanad").Width = 150
            GridEXBestankary.CurrentTable.Columns.Item("ShomarehSanad").EditType = Janus.Windows.GridEX.EditType.NoEdit
            GridEXBestankary.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXBestankary.CurrentTable.Columns.Item("ShomarehSanad").Position = 3
            GridEXBestankary.CurrentTable.Columns.Item("ShomarehSanad").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXBestankary.CurrentTable.Columns.Item("txtAkharinVazeiat").Caption = "آخرین وضعیت"
            GridEXBestankary.CurrentTable.Columns.Item("txtAkharinVazeiat").HeaderStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXBestankary.CurrentTable.Columns.Item("txtAkharinVazeiat").Visible = True
            GridEXBestankary.CurrentTable.Columns.Item("txtAkharinVazeiat").Width = 170
            GridEXBestankary.CurrentTable.Columns.Item("txtAkharinVazeiat").EditType = Janus.Windows.GridEX.EditType.NoEdit
            GridEXBestankary.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXBestankary.CurrentTable.Columns.Item("txtAkharinVazeiat").Position = 4
            GridEXBestankary.CurrentTable.Columns.Item("txtAkharinVazeiat").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXBestankary.CurrentTable.Columns.Item("MablaghVajh").Caption = "مبلغ سند"
            GridEXBestankary.CurrentTable.Columns.Item("MablaghVajh").HeaderStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXBestankary.CurrentTable.Columns.Item("MablaghVajh").Visible = True
            GridEXBestankary.CurrentTable.Columns.Item("MablaghVajh").Width = 130
            GridEXBestankary.CurrentTable.Columns.Item("MablaghVajh").EditType = Janus.Windows.GridEX.EditType.NoEdit
            GridEXBestankary.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXBestankary.CurrentTable.Columns.Item("MablaghVajh").Position = 5
            GridEXBestankary.CurrentTable.Columns.Item("MablaghVajh").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXBestankary.CurrentTable.Columns.Item("Mandeh").Caption = "مبلغ مانده"
            GridEXBestankary.CurrentTable.Columns.Item("Mandeh").HeaderStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXBestankary.CurrentTable.Columns.Item("Mandeh").Visible = True
            GridEXBestankary.CurrentTable.Columns.Item("Mandeh").Width = 130
            GridEXBestankary.CurrentTable.Columns.Item("Mandeh").EditType = Janus.Windows.GridEX.EditType.NoEdit
            GridEXBestankary.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXBestankary.CurrentTable.Columns.Item("Mandeh").Position = 6
            GridEXBestankary.CurrentTable.Columns.Item("Mandeh").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXBestankary.CurrentTable.Columns.Item("MablaghPardakhty").Caption = "پرداختی برای فاکتور"
            GridEXBestankary.CurrentTable.Columns.Item("MablaghPardakhty").HeaderStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXBestankary.CurrentTable.Columns.Item("MablaghPardakhty").Visible = True
            GridEXBestankary.CurrentTable.Columns.Item("MablaghPardakhty").Width = 130
            GridEXBestankary.CurrentTable.Columns.Item("MablaghPardakhty").EditType = Janus.Windows.GridEX.EditMode.EditOff
            GridEXBestankary.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOn
            GridEXBestankary.CurrentTable.Columns.Item("MablaghPardakhty").Position = 7
            GridEXBestankary.CurrentTable.Columns.Item("MablaghPardakhty").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXBestankary.CurrentTable.Columns.Item("CodeAmalyat").Caption = "CodeAmalyat"
            GridEXBestankary.CurrentTable.Columns.Item("CodeAmalyat").HeaderStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXBestankary.CurrentTable.Columns.Item("CodeAmalyat").Visible = False
            GridEXBestankary.CurrentTable.Columns.Item("CodeAmalyat").Width = 0
            GridEXBestankary.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXBestankary.CurrentTable.Columns.Item("CodeAmalyat").Position = 8
            GridEXBestankary.CurrentTable.Columns.Item("CodeAmalyat").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            For i As Integer = 0 To GridEXBestankary.RootTable.Columns.Count - 1
                If GridEXBestankary.RootTable.Columns(i).Type.IsValueType Then
                    GridEXBestankary.RootTable.Columns(i).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                    GridEXBestankary.RootTable.Columns(i).FormatMode = Janus.Windows.GridEX.FormatMode.UseIFormattable
                    GridEXBestankary.RootTable.Columns(i).FormatString = "N"
                    GridEXBestankary.RootTable.Columns(i).TotalFormatMode = Janus.Windows.GridEX.FormatMode.UseIFormattable
                    GridEXBestankary.RootTable.Columns(i).TotalFormatString = "N"
                End If
            Next

            GridEXBestankary.Visible = True

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> SetGridStyleBestanKari")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> SetGridStyleBestanKari")
        End Try

    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub btnSaveBestankary_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveBestankary.Click
        Dim cmSQL As SqlCommand
        Dim cnSQL As SqlConnection
        Dim strSQL As String = ""
        Dim intRowsAffected As Integer = 0

        Try
            If dsForm.HasChanges(DataRowState.Modified) Then
                If dsForm.Tables("Sales_Bestankary").GetChanges(DataRowState.Modified).Rows.Count <> 0 Then

                    cnSQL = New SqlConnection(ConnectionString)
                    cnSQL.Open()

                    For Each dr As DataRow In dsForm.Tables("Sales_Bestankary").GetChanges(DataRowState.Modified).Rows
                        If dr("MablaghPardakhty") <= 0 Then Continue For
                        If dr("MablaghPardakhty") > dr("Mandeh") Then
                            MsgBox("مبلغ پرداختی ردیف " & dr("RowNumber") & " از مانده بیشتر است. ", MsgBoxStyle.MsgBoxRtlReading Or MsgBoxStyle.MsgBoxRight Or MsgBoxStyle.Information, "ذخيره")
                            Continue For
                        End If

                        If dr("MablaghPardakhty") > Val(ObjCode.DigitSepratorRemover(txtMandehFaktor.Text)) Then
                            MsgBox("مبلغ پرداختی ردیف " & dr("RowNumber") & " از مانده فاکتور بیشتر است. ", MsgBoxStyle.MsgBoxRtlReading Or MsgBoxStyle.MsgBoxRight Or MsgBoxStyle.Information, "ذخيره")
                            Continue For
                        End If

                        strSQL = "Sales.spTafkikJozTasfieh_InsertFromBestankary "

                        cmSQL = New SqlCommand(strSQL, cnSQL)
                        cmSQL.CommandType = CommandType.StoredProcedure
                        cmSQL.Parameters.Clear()

                        cmSQL.Parameters.AddWithValue("CodeAmalyat", dr("CodeAmalyat"))
                        cmSQL.Parameters.AddWithValue("ccFaktorTitr", B_ccFaktor)
                        cmSQL.Parameters.AddWithValue("Mablagh", dr("MablaghPardakhty"))
                        cmSQL.Parameters.AddWithValue("ccTafkikJozeTasfiehSatr", B_ccTafkikJozeTasfiehSatr)

                        cmSQL.ExecuteNonQuery()
                        intRowsAffected &= 1

                        B_PardakhtiFaktor = objTools.DLookup("MablaghNaghd + MablaghChek + MablaghResid + MablaghKartKhan + MablaghMarjoee + MablaghTakhfif + MablaghBestankary", "Sales.TafkikJozeTasfiehSatr", "ccTafkikJozeTasfiehSatr = " & B_ccTafkikJozeTasfiehSatr)
                        B_MandehFaktor = B_MablaghFaktor - B_PardakhtiFaktor

                    Next

                    If intRowsAffected > 0 Then
                        MsgBox("تعداد " & intRowsAffected & " سطر با موفقیت اضافه شد.", MsgBoxStyle.MsgBoxRtlReading Or MsgBoxStyle.MsgBoxRight Or MsgBoxStyle.Information, "ذخيره")
                        dsForm.Tables("Sales_Bestankary").AcceptChanges()
                    End If

                    LoadForm()

                End If
            End If

            RefreshPardakhty()
            RefreshBestankary()

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> btnSaveBestankary_Click ")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> btnSaveBestankary_Click ")
        End Try
    End Sub

    Private Sub tsmiDeletePardakhty_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsmiDeletePardakhty.Click
        Dim cmSQL As New SqlCommand
        Dim cnSQL As New SqlConnection
        Dim strSQL As String = ""

        Try
            strSQL = "Sales.spTafkikJozTasfieh_DeletePardakhtyFromBestankary "

            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            cmSQL.Parameters.AddWithValue("ccAmalyatFaktor", Val(GridEXPardakhty.CurrentRow.Cells("ccAmalyatFaktor").Text.Replace(",", "")))
            cmSQL.Parameters.AddWithValue("ccTafkikJozeTasfiehSatr", B_ccTafkikJozeTasfiehSatr)

            cmSQL.ExecuteNonQuery()

            cmSQL = Nothing
            cnSQL.Close()

            B_PardakhtiFaktor = objTools.DLookup("MablaghNaghd + MablaghChek + MablaghResid + MablaghKartKhan + MablaghMarjoee + MablaghTakhfif + MablaghBestankary", "Sales.TafkikJozeTasfiehSatr", "ccTafkikJozeTasfiehSatr = " & B_ccTafkikJozeTasfiehSatr)
            B_MandehFaktor = B_MablaghFaktor + B_PardakhtiFaktor

            RefreshPardakhty()
            RefreshBestankary()
            LoadForm()

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> tsmiDeletePardakhty_Click ")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> tsmiDeletePardakhty_Click ")
        End Try
    End Sub
End Class