Public Class frmAN_SerialNumber
#Region "Variable AND Constant Declration"

    Const cntCodeSubSystem As Long = 1000142
    Dim cmTitr, cmSatr As CurrencyManager
    Dim dvTitr, dvSatr As DataView
    Dim tCodeCounter As Long
    Dim ErrPro As New ErrorProvider
    Dim dsForm As New DataSet
    Private SN As Integer
    Dim txtCaption As String
    Private WithEvents BS As New UD_Dll.PassString
    Public ModeForm As UD_Dll.Enums.GL_ModeForms = UD_Dll.Enums.GL_ModeForms.None
    Public ccHadaf As Integer = 0
    Dim Flg_Load As Boolean = False
#End Region
#Region "Form Event Code"
    Private Sub frmAN_SerialNumber_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetParameter()
        SN = objSec.GetSecurityNumber(cntCodeSubSystem, UserName)
        LoadCombo()
        cmbNoeAmalyat.SelectedIndex = 0
        mskAzTarikh.Text = CodeDoreh.ToString + "0101"
        mskTaTarikh.Text = TarikhEmrooz
        cmbNoeAmalyat.Focus()
    End Sub
    Private Sub cmbNoeAmalyat_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbNoeAmalyat.SelectedIndexChanged
        txtNoeAmalyat.Text = cmbNoeAmalyat.SelectedItem
        If cmbNoeAmalyat.SelectedIndex <> -1 And cmbNoeAmalyat.SelectedIndex <> 0 Then
            Search()
        Else
            GridEXTitr.DataSource = Nothing
            GridEXSatr.DataSource = Nothing
        End If
    End Sub
    Private Sub GridEXTitr_Click(sender As Object, e As EventArgs) Handles GridEXTitr.Click
        If GridEXTitr.RowCount = 0 Then Exit Sub
        SearchSatr()
    End Sub
    Private Sub GridEXTitr_SelectionChanged(sender As Object, e As EventArgs) Handles GridEXTitr.SelectionChanged
        SearchSatr()
    End Sub
    Private Sub GridEXSatr_DoubleClick(sender As Object, e As EventArgs) Handles GridEXSatr.DoubleClick
        If GridEXTitr.RowCount = 0 Then Exit Sub
        If GridEXSatr.RowCount = 0 Then Exit Sub

        Dim frm As New frmAN_InsertSerialNumber

        frm.Type = Val(GridEXTitr.CurrentRow.Cells("Type").Text.Replace(",", ""))
        frm.ccKardexTitr = Val(GridEXTitr.CurrentRow.Cells("ccKardexTitr").Text.Replace(",", ""))
        frm.ccKardexSatr = Val(GridEXSatr.CurrentRow.Cells("ccKardexSatr").Text.Replace(",", ""))
        frm.ccKala = Val(GridEXSatr.CurrentRow.Cells("ccKala").Text.Replace(",", ""))
        frm.CodeKala = GridEXSatr.CurrentRow.Cells("CodeKala").Text.Replace(",", "")
        frm.NameKala = GridEXSatr.CurrentRow.Cells("NameKala").Text
        frm.TedadKalayeForm = Val(GridEXSatr.CurrentRow.Cells("TedadKala").Text.Replace(",", ""))
        frm.EtelaatForm = cmbNoeAmalyat.SelectedItem + "   :   فرم شماره   " + GridEXTitr.CurrentRow.Cells("ShomarehForm").Text.Replace(",", "") + "     به تاریخ     " + GridEXTitr.CurrentRow.Cells("Tarikh").Text.Replace(",", "")

        Me.Hide()
        frm.ShowDialog()
        Me.Show()

        SearchSatr()
    End Sub
    Private Sub GridEXSatr_MouseEnter(sender As Object, e As EventArgs) Handles GridEXSatr.MouseEnter
        If GridEXSatr.RowCount = 0 Then Exit Sub
        lblSharhSatr.Visible = True
    End Sub
    Private Sub GridEXSatr_MouseLeave(sender As Object, e As EventArgs) Handles GridEXSatr.MouseLeave
        If GridEXSatr.RowCount = 0 Then Exit Sub
        lblSharhSatr.Visible = False
    End Sub
#End Region
#Region "Global Form Code"
    Private Sub SetParameter()
        Dim commands As String = Microsoft.VisualBasic.Command()
        If commands.Length = 0 Then
            UserName = "Administrator"
            UserPassWord = "66998833"
            NameMahalFaal = "تهران"
            CodeMahalFaal = "1"
            PersonelCode = "0"
            PersonelName = "Administrator"
            CodeDoreh = "1394"
            txtCaption = "شماره سریال"
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
        End If
    End Sub
    Private Sub LoadCombo()
        Try
            cmbNoeAmalyat.Items.Add("--- یک عملیات انتخاب نمایید ---")
            cmbNoeAmalyat.Items.Add("حـوالــه به انبــــار")
            cmbNoeAmalyat.Items.Add("رسیـــــــــــــــــد")
            cmbNoeAmalyat.Items.Add("مرجوعی به تامین کننده")
            cmbNoeAmalyat.Items.Add("رسیــــــــد اول دوره")
            cmbNoeAmalyat.Items.Add("مرجـــوعی از مشتـــری")
            cmbNoeAmalyat.Items.Add("اشـانتیــــــــــــون")
            cmbNoeAmalyat.Items.Add("کســــر از انبــــار ")
            cmbNoeAmalyat.Items.Add("اضافــه بـه انبــــار")
            cmbNoeAmalyat.Items.Add("انبار به انبار خروجـی")
            cmbNoeAmalyat.Items.Add("انبار به انبـار ورودی")
            cmbNoeAmalyat.Items.Add("فاکتـــــور فـــــروش")

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "Error IN DataBase ----> LoadCombo")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, "Error IN ----> LoadCombo")
        End Try
    End Sub
    Private Sub Search()
        Dim cnSQL As New SqlConnection
        Dim cmSQL As New SqlCommand
        Dim daSQL As New SqlDataAdapter
        Dim strSQL As String = ""

        If dsForm.Tables.Contains("tbl_SerialTitr") Then
            dsForm.Tables.Remove("tbl_SerialTitr")
        End If

        Try
            strSQL = "WareHouse.spSerialNumber_Search "

            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            cmSQL.Parameters.AddWithValue("Type", cmbNoeAmalyat.SelectedIndex)
            cmSQL.Parameters.AddWithValue("CodeMahal", CodeMahalFaal)
            cmSQL.Parameters.AddWithValue("CodeDoreh", CodeDoreh)
            cmSQL.Parameters.AddWithValue("AzTarikh", mskAzTarikh.Text)
            cmSQL.Parameters.AddWithValue("TaTarikh", mskTaTarikh.Text)

            daSQL = New SqlDataAdapter(cmSQL)
            daSQL.Fill(dsForm, "tbl_SerialTitr")

            dvTitr = New DataView(dsForm.Tables("tbl_SerialTitr"))
            dvTitr.Sort = "ShomarehForm ASC"

            dvTitr.AllowNew = False
            dvTitr.AllowDelete = False
            dvTitr.AllowEdit = True

            cmSQL = Nothing : daSQL = Nothing
            cnSQL.Close()

            SetGrid()
            With GridEXTitr
                .Visible = True
                .DataSource = Nothing
                .DataSource = dvTitr
            End With

            BoundCurrencyManagerTitr()

            Me.CenterToScreen()
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "Error IN DataBase ----> Search")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, "Error IN ----> Search")
        End Try
    End Sub
    Private Sub SetGrid()
        If dvTitr.Count = 0 Then
            Exit Sub
        End If

        Try
            With GridEXTitr
                .DataSource = Nothing
                .DataSource = dsForm.Tables("tbl_SerialTitr").DefaultView
                .SetDataBinding(dsForm.Tables("tbl_SerialTitr").DefaultView, "")
                .RetrieveStructure()
            End With

            For i As Integer = 0 To GridEXTitr.CurrentTable.Columns.Count - 1
                GridEXTitr.CurrentTable.Columns.Item(i).Visible = False
            Next

            GridEXTitr.CurrentTable.Columns.Item("ShomarehForm").Caption = "شماره فرم"
            GridEXTitr.CurrentTable.Columns.Item("ShomarehForm").Visible = True
            GridEXTitr.CurrentTable.Columns.Item("ShomarehForm").Width = 100
            GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXTitr.CurrentTable.Columns.Item("ShomarehForm").Position = 0
            GridEXTitr.CurrentTable.Columns.Item("ShomarehForm").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXTitr.CurrentTable.Columns.Item("ShomarehForm").HeaderAlignment = TextAlignment.Center

            GridEXTitr.CurrentTable.Columns.Item("Tarikh").Caption = "تاریخ فـرم"
            GridEXTitr.CurrentTable.Columns.Item("Tarikh").Visible = True
            GridEXTitr.CurrentTable.Columns.Item("Tarikh").Width = 80
            GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXTitr.CurrentTable.Columns.Item("Tarikh").Position = 1
            GridEXTitr.CurrentTable.Columns.Item("Tarikh").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXTitr.CurrentTable.Columns.Item("Tarikh").HeaderAlignment = TextAlignment.Center

            GridEXTitr.CurrentTable.Columns.Item("NameAnbar").Caption = "نام انبـار"
            GridEXTitr.CurrentTable.Columns.Item("NameAnbar").Visible = True
            GridEXTitr.CurrentTable.Columns.Item("NameAnbar").Width = 200
            GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXTitr.CurrentTable.Columns.Item("NameAnbar").Position = 2
            GridEXTitr.CurrentTable.Columns.Item("NameAnbar").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXTitr.CurrentTable.Columns.Item("NameAnbar").HeaderAlignment = TextAlignment.Center

            GridEXTitr.CurrentTable.Columns.Item("Tozihat").Caption = "شــــرح"
            GridEXTitr.CurrentTable.Columns.Item("Tozihat").Visible = True
            GridEXTitr.CurrentTable.Columns.Item("Tozihat").Width = 550
            GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXTitr.CurrentTable.Columns.Item("Tozihat").Position = 3
            GridEXTitr.CurrentTable.Columns.Item("Tozihat").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXTitr.CurrentTable.Columns.Item("Tozihat").HeaderAlignment = TextAlignment.Center

            GridEXTitr.CurrentTable.Columns.Item("ccKardexTitr").Caption = "ccKardexTitr"
            GridEXTitr.CurrentTable.Columns.Item("ccKardexTitr").Visible = False
            GridEXTitr.CurrentTable.Columns.Item("ccKardexTitr").Width = 0
            GridEXTitr.CurrentTable.Columns.Item("ccKardexTitr").EditType = EditType.NoEdit
            GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXTitr.CurrentTable.Columns.Item("ccKardexTitr").Position = 4
            GridEXTitr.CurrentTable.Columns.Item("ccKardexTitr").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXTitr.CurrentTable.Columns.Item("ccKardexTitr").HeaderAlignment = TextAlignment.Center

            GridEXTitr.CurrentTable.Columns.Item("Type").Caption = "Type"
            GridEXTitr.CurrentTable.Columns.Item("Type").Visible = False
            GridEXTitr.CurrentTable.Columns.Item("Type").Width = 0
            GridEXTitr.CurrentTable.Columns.Item("Type").EditType = EditType.NoEdit
            GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXTitr.CurrentTable.Columns.Item("Type").Position = 5
            GridEXTitr.CurrentTable.Columns.Item("Type").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXTitr.CurrentTable.Columns.Item("Type").HeaderAlignment = TextAlignment.Center

            For i As Integer = 0 To GridEXTitr.RootTable.Columns.Count - 1
                If GridEXTitr.RootTable.Columns(i).Type.IsValueType Then
                    GridEXTitr.RootTable.Columns(i).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                    GridEXTitr.RootTable.Columns(i).FormatMode = Janus.Windows.GridEX.FormatMode.UseIFormattable
                    GridEXTitr.RootTable.Columns(i).FormatString = "G"
                    GridEXTitr.RootTable.Columns(i).TotalFormatMode = Janus.Windows.GridEX.FormatMode.UseIFormattable
                    GridEXTitr.RootTable.Columns(i).TotalFormatString = "G"
                End If
            Next

            Me.CenterToScreen()
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> SetGrid ")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> SetGrid ")
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
        Dim cm As New SqlCommand
        Try
            'RefreshSatrData(cm)
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->cmTitr_PositionChanged")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->cmTitr_PositionChanged")
        End Try
    End Sub
    Private Sub SearchSatr()
        Dim cnSQL As New SqlConnection
        Dim cmSQL As New SqlCommand
        Dim daSQL As New SqlDataAdapter
        Dim strSQL As String = ""

        If dsForm.Tables.Contains("tbl_SerialSatr") Then
            dsForm.Tables.Remove("tbl_SerialSatr")
        End If

        Try
            strSQL = "WareHouse.spSerialNumber_SearchSatr "

            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            cmSQL.Parameters.AddWithValue("Type", cmbNoeAmalyat.SelectedIndex)
            cmSQL.Parameters.AddWithValue("ccKardexTitr", Val(GridEXTitr.CurrentRow.Cells("ccKardexTitr").Text.Replace(",", "")))

            daSQL = New SqlDataAdapter(cmSQL)
            daSQL.Fill(dsForm, "tbl_SerialSatr")

            dvSatr = New DataView(dsForm.Tables("tbl_SerialSatr"))
            dvSatr.Sort = "CodeKala ASC"

            dvSatr.AllowNew = False
            dvSatr.AllowDelete = False
            dvSatr.AllowEdit = True

            cmSQL = Nothing : daSQL = Nothing
            cnSQL.Close()

            SetGridSatr()
            With GridEXSatr
                .Visible = True
                .DataSource = Nothing
                .DataSource = dvSatr
            End With

            Me.CenterToScreen()
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "Error IN DataBase ----> SearchSatr")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, "Error IN ----> SearchSatr")
        End Try
    End Sub
    Private Sub SetGridSatr()
        If dvTitr.Count = 0 Then
            Exit Sub
        End If

        If dvSatr.Count = 0 Then
            Exit Sub
        End If

        Try
            With GridEXSatr
                .DataSource = Nothing
                .DataSource = dsForm.Tables("tbl_SerialSatr").DefaultView
                .SetDataBinding(dsForm.Tables("tbl_SerialSatr").DefaultView, "")
                .RetrieveStructure()
            End With

            For i As Integer = 0 To GridEXSatr.CurrentTable.Columns.Count - 1
                GridEXSatr.CurrentTable.Columns.Item(i).Visible = False
            Next

            GridEXSatr.CurrentTable.Columns.Item("CodeKala").Caption = "کــــد کالا"
            GridEXSatr.CurrentTable.Columns.Item("CodeKala").Visible = True
            GridEXSatr.CurrentTable.Columns.Item("CodeKala").Width = 150
            GridEXSatr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXSatr.CurrentTable.Columns.Item("CodeKala").Position = 0
            GridEXSatr.CurrentTable.Columns.Item("CodeKala").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXSatr.CurrentTable.Columns.Item("CodeKala").HeaderAlignment = TextAlignment.Center

            GridEXSatr.CurrentTable.Columns.Item("NameKala").Caption = "نــــــــــام کالا"
            GridEXSatr.CurrentTable.Columns.Item("NameKala").Visible = True
            GridEXSatr.CurrentTable.Columns.Item("NameKala").Width = 440
            GridEXSatr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXSatr.CurrentTable.Columns.Item("NameKala").Position = 1
            GridEXSatr.CurrentTable.Columns.Item("NameKala").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXSatr.CurrentTable.Columns.Item("NameKala").HeaderAlignment = TextAlignment.Center

            GridEXSatr.CurrentTable.Columns.Item("TedadKala").Caption = "تعــداد کالا"
            GridEXSatr.CurrentTable.Columns.Item("TedadKala").Visible = True
            GridEXSatr.CurrentTable.Columns.Item("TedadKala").Width = 170
            GridEXSatr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXSatr.CurrentTable.Columns.Item("TedadKala").Position = 2
            GridEXSatr.CurrentTable.Columns.Item("TedadKala").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXSatr.CurrentTable.Columns.Item("TedadKala").HeaderAlignment = TextAlignment.Center

            GridEXSatr.CurrentTable.Columns.Item("TedadSerialSabtShodeh").Caption = "تعـداد سریال ثبت شـده"
            GridEXSatr.CurrentTable.Columns.Item("TedadSerialSabtShodeh").Visible = True
            GridEXSatr.CurrentTable.Columns.Item("TedadSerialSabtShodeh").Width = 170
            GridEXSatr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXSatr.CurrentTable.Columns.Item("TedadSerialSabtShodeh").Position = 3
            GridEXSatr.CurrentTable.Columns.Item("TedadSerialSabtShodeh").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXSatr.CurrentTable.Columns.Item("TedadSerialSabtShodeh").HeaderAlignment = TextAlignment.Center

            GridEXSatr.CurrentTable.Columns.Item("ccKardexSatr").Caption = "ccKardexSatr"
            GridEXSatr.CurrentTable.Columns.Item("ccKardexSatr").Visible = False
            GridEXSatr.CurrentTable.Columns.Item("ccKardexSatr").Width = 0
            GridEXSatr.CurrentTable.Columns.Item("ccKardexSatr").EditType = EditType.NoEdit
            GridEXSatr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXSatr.CurrentTable.Columns.Item("ccKardexSatr").Position = 4
            GridEXSatr.CurrentTable.Columns.Item("ccKardexSatr").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXSatr.CurrentTable.Columns.Item("ccKardexSatr").HeaderAlignment = TextAlignment.Center

            GridEXSatr.CurrentTable.Columns.Item("ccKala").Caption = "ccKala"
            GridEXSatr.CurrentTable.Columns.Item("ccKala").Visible = False
            GridEXSatr.CurrentTable.Columns.Item("ccKala").Width = 0
            GridEXSatr.CurrentTable.Columns.Item("ccKala").EditType = EditType.NoEdit
            GridEXSatr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXSatr.CurrentTable.Columns.Item("ccKala").Position = 5
            GridEXSatr.CurrentTable.Columns.Item("ccKala").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXSatr.CurrentTable.Columns.Item("ccKala").HeaderAlignment = TextAlignment.Center

            For i As Integer = 0 To GridEXSatr.RootTable.Columns.Count - 1
                If GridEXSatr.RootTable.Columns(i).Type.IsValueType Then
                    GridEXSatr.RootTable.Columns(i).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                    GridEXSatr.RootTable.Columns(i).FormatMode = Janus.Windows.GridEX.FormatMode.UseIFormattable
                    GridEXSatr.RootTable.Columns(i).FormatString = "G"
                    GridEXSatr.RootTable.Columns(i).TotalFormatMode = Janus.Windows.GridEX.FormatMode.UseIFormattable
                    GridEXSatr.RootTable.Columns(i).TotalFormatString = "G"
                End If
            Next

            Me.CenterToScreen()
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> SetGridSatr ")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> SetGridSatr ")
        End Try
    End Sub
    Private Function IsValid() As Boolean
        IsValid = False
        Try
            If cmbNoeAmalyat.SelectedIndex = -1 Or cmbNoeAmalyat.SelectedIndex = 0 Then
                MsgBox("لطفا نوع عملیات را مشخص نمایید !", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, "خطا")
                ErrPro.SetError(cmbNoeAmalyat, "لطفا نوع عملیات را مشخص نمایید !")
                cmbNoeAmalyat.Focus()
                Exit Function
            End If
            ErrPro.SetError(cmbNoeAmalyat, "")


            If Me.mskAzTarikh.Text = "" Then
                ErrPro.SetError(Me.mskAzTarikh, "از تاریخ را وارد نمایید !")
                MsgBox("از تاریخ را وارد نمایید !", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
                mskAzTarikh.Focus()
                Exit Function
            Else
                If Not objTarikh.IsShDate(mskAzTarikh.Text.ToString) Then
                    mskAzTarikh.Focus()
                    Exit Function
                End If
            End If
            ErrPro.SetError(Me.mskAzTarikh, "")


            If Me.mskTaTarikh.Text = "" Then
                ErrPro.SetError(Me.mskTaTarikh, "تا تاریخ را وارد نمایید !")
                MsgBox("تا تاریخ را وارد نمایید !", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
                mskTaTarikh.Focus()
                Exit Function
            Else
                If Not objTarikh.IsShDate(mskTaTarikh.Text.ToString) Then
                    mskTaTarikh.Focus()
                    Exit Function
                End If
            End If
            ErrPro.SetError(Me.mskTaTarikh, "")


            If mskAzTarikh.Text > mskTaTarikh.Text Then
                ErrPro.SetError(Me.mskTaTarikh, ".تا تاریخ نباید پیش از شروع آن باشد")
                mskTaTarikh.Focus()
                Exit Function
            End If
            ErrPro.SetError(Me.mskTaTarikh, "")

            Return True
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "Error IN DataBase ----> IsValid")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, "Error IN ----> IsValid")
        End Try
    End Function
#End Region
#Region "From Buttons "
    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        If IsValid() = False Then
            Exit Sub
        End If

        Search()
    End Sub
#End Region
#Region "Stored Procedures"
    '' WareHouse.spSerialNumber_Search
    '' WareHouse.spSerialNumber_SearchSatr
#End Region
End Class
