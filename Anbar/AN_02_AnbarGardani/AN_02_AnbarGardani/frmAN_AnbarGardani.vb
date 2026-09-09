Public Class frmAN_AnbarGardani
#Region "Variable AND Constant Declration"
    Const cntCodeSubSystem As Long = 100089
    Const TitrGridSize As Integer = 55
    Const SatrGridSize As Integer = 28
    Const TitrOrgSize As Integer = 409
    Const SatrOrgSize As Integer = 482
    Dim Mode As UD_Dll.Enums.GL_ModeForms = UD_Dll.Enums.GL_ModeForms.None
    Dim cmTitr As CurrencyManager
    Dim cmSatr As CurrencyManager
    Dim dvTitr As DataView
    Dim dvSatr As DataView
    Dim dvSatr2 As DataView
    Dim ErrPro As New ErrorProvider
    Dim dsForm As New DataSet
    Dim dvForm As DataView
    Private SN As Integer
    Dim tPos As Integer
    Dim txtCaption As String
    Dim Flag As Integer = 0
    Dim FlagIsValid As Integer = 0
    Public ccAnbarGardanyTitr As Integer = 0
    Public ccAnbarGardanySatr As Integer = 0
    Public ccanbar As Integer = 0
    Public TarikhAvalDoreh As String = ""
    Public AzTarikh As String = ""
    Public TaTarikh As String = ""
    Private WithEvents BS As New UD_Dll.PassString
    Public flgPrint As Boolean = False
#End Region
#Region "Form Event Code"
    Private Sub frmAN_AnbarGardani_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            SetParameter()
            SN = objSec.GetSecurityNumber(cntCodeSubSystem, UserName)
            Mode = UD_Dll.Enums.GL_ModeForms.None
            ' mskAzTarikh.Text = TarikhEmrooz
            mskAzTarikhSearch.Text = CType(CodeDoreh, String) + "0101"
            mskTaTarikhSearch.Text = CType(CodeDoreh, String) + "1229"
            SetFormData()
            SetGroupBox()

            LoadCombo()
            cmbNameAnbarSearch.SelectedValue = 0

            cmbNoeAnbargardani.SelectedIndex = 1
            cmbDorehSearch.SelectedValue = CodeDoreh

            Search()

            btnNext.Enabled = True
            btnBack.Enabled = False
            btnTaeed.Enabled = True

            objTools.ChangeKbd(UD_Dll.mdlUtility.KBD.Farsi)
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> frmAN_AnbarGardani_Load")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> frmAN_AnbarGardani_Load")
        End Try
        mskAzTarikh.Focus()
    End Sub
    Private Sub LoadCombo()
        Try
            Dim Strsql As String
            Dim daSQL As SqlDataAdapter
            Dim cn As New SqlConnection
            Dim cm As New SqlCommand
            Dim p As New SqlParameter
            Dim dr As DataRow



            cn = New SqlConnection(ConnectionString)
            cn.Open()
            Strsql = "Global.spCodeDoreh_LoadCombo "
            cm = New SqlCommand(Strsql, cn)
            cm.CommandType = CommandType.StoredProcedure
            cm.Parameters.Clear()

            daSQL = New SqlDataAdapter(cm)
            daSQL.Fill(dsForm, "tblCodeDoreh")
            cmbDoreh.DataSource = Nothing
            cmbDoreh.Items.Clear()
            cmbDoreh.DataSource = dsForm.Tables("tblCodeDoreh").DefaultView
            cmbDoreh.DisplayMember = "CodeDoreh"
            cmbDoreh.ValueMember = "CodeDoreh"
            cmbDoreh.SelectedText = CodeMahalFaal

            cmbDorehSearch.DataSource = Nothing
            cmbDorehSearch.Items.Clear()
            cmbDorehSearch.DataSource = dsForm.Tables("tblCodeDoreh").DefaultView
            cmbDorehSearch.DisplayMember = "CodeDoreh"
            cmbDorehSearch.ValueMember = "CodeDoreh"
            cmbDorehSearch.SelectedText = CodeMahalFaal

            cm = Nothing

            Strsql = "WareHouse.spAnbargardani_NameAnbar_LoadCombo"

            cm = New SqlCommand(Strsql, cn)
            cm.CommandType = CommandType.StoredProcedure
            cm.Parameters.Clear()


            daSQL = New SqlDataAdapter(cm)
            daSQL.Fill(dsForm, "tblNameAnbar")
            cmbNameAnbar.DataSource = Nothing
            cmbNameAnbar.Items.Clear()
            cmbNameAnbar.DataSource = dsForm.Tables("tblNameAnbar").DefaultView
            cmbNameAnbar.DisplayMember = "NameAnbar"
            cmbNameAnbar.ValueMember = "CodeAnbar"

            cm = Nothing

            Strsql = "WareHouse.spAnbargardani_NameAnbarSearch_LoadCombo"

            cm = New SqlCommand(Strsql, cn)
            cm.CommandType = CommandType.StoredProcedure
            cm.Parameters.Clear()

            daSQL = New SqlDataAdapter(cm)
            daSQL.Fill(dsForm, "tblNameAnbarSearch")

            dr = dsForm.Tables("tblNameAnbarSearch").NewRow()
            dr("CodeAnbar") = 0
            dr("NameAnbar") = "همه"
            dsForm.Tables("tblNameAnbarSearch").Rows.Add(dr)

            cmbNameAnbarSearch.DataSource = Nothing
            cmbNameAnbarSearch.Items.Clear()
            cmbNameAnbarSearch.DataSource = dsForm.Tables("tblNameAnbarSearch").DefaultView
            cmbNameAnbarSearch.DisplayMember = "NameAnbar"
            cmbNameAnbarSearch.ValueMember = "CodeAnbar"

            cm = Nothing

            cmbNoeAnbargardani.Items.Add("همه کالاها")
            cmbNoeAnbargardani.Items.Add("کالاهای مغایرت دار ")
            cmbNoeAnbargardani.Items.Add("کالاهای مغایرت دار دو شمارش قبل")

            daSQL = Nothing
            cn.Close()

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> LoadCombo")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> LoadCombo")
        End Try
    End Sub
    Private Sub GridEXTitr_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GridEXTitr.DoubleClick
        Try
            If GridEXTitr.CurrentRow.RowType = Janus.Windows.GridEX.RowType.FilterRow Then
                Exit Sub
            End If

            If objTools.DLookup("Vazeiat", "tblAN_AnbarGardany", "ccAnbarGardanyTitr = " & Val(GridEXTitr.CurrentRow.Cells("ccAnbarGardanyTitr").Text.Replace(",", ""))) = True Then
                MsgBox("انبارگردانی تایید شده است، امکان ویرایش ندارید", MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " پیام")
                Exit Sub
            End If

            Mode = UD_Dll.Enums.GL_ModeForms.UpdateRecord
            SetFormTitrData()
            SetFormData()
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> GridEXTitr_DoubleClick")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> GridEXTitr_DoubleClick")
        End Try
    End Sub
    Private Sub cmTitr_ItemChanged(ByVal sender As Object, ByVal e As ItemChangedEventArgs)
        Try
            ' Search(True)
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> cmTitr_ItemChanged")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> cmTitr_ItemChanged")
        End Try

    End Sub
    Private Sub cmTitr_PositionChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim cm As New SqlCommand
        Try
            'RefreshSatrData(cm)

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> cmTitr_PositionChanged")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> cmTitr_PositionChanged")
        End Try

    End Sub
    Private Sub cmSatr_ItemChanged(ByVal sender As Object, ByVal e As ItemChangedEventArgs)
        Try

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> cmSatr_ItemChanged")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> cmSatr_ItemChanged")
        End Try

    End Sub
    Private Sub cmSatr_PositionChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> cmSatr_PositionChanged")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> cmSatr_PositionChanged")
        End Try

    End Sub
    Private Sub GridEXTitr_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GridEXTitr.Click
        Dim cm As New SqlCommand

        Try
            If GridEXTitr.CurrentRow.RowType = Janus.Windows.GridEX.RowType.FilterRow Then
                Exit Sub
            End If

            cmTitr = CType(BindingContext(GridEXTitr.DataSource), CurrencyManager)
            AddHandler cmTitr.ItemChanged, AddressOf cmTitr_ItemChanged
            AddHandler cmTitr.PositionChanged, AddressOf cmTitr_PositionChanged

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> GridEXTitr_Click")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> GridEXTitr_Click")
        End Try
    End Sub
    Private Sub GridEXSatr_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GridEXSatr.Click
        Dim cm As New SqlCommand

        Try
            If GridEXSatr.CurrentRow.RowType = Janus.Windows.GridEX.RowType.FilterRow Then
                Exit Sub
            End If

            cmSatr = CType(BindingContext(GridEXSatr.DataSource), CurrencyManager)
            AddHandler cmSatr.ItemChanged, AddressOf cmSatr_ItemChanged
            AddHandler cmSatr.PositionChanged, AddressOf cmSatr_PositionChanged

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> GridEXSatr_Click")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> GridEXSatr_Click")
        End Try
    End Sub
    Private Sub GridEXTitr_RowCheckStateChanging(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.RowCheckStateChangingEventArgs) Handles GridEXTitr.RowCheckStateChanging
        If GridEXTitr.GetCheckedRows().Length > 0 Then
            For i As Integer = 0 To GridEXTitr.GetCheckedRows().Length - 1
                If GridEXTitr.GetCheckedRows(i).CheckState = Janus.Windows.GridEX.RowCheckState.Checked Then
                    GridEXTitr.GetCheckedRows(i).CheckState = Janus.Windows.GridEX.RowCheckState.Unchecked
                End If
            Next
        End If
    End Sub
    Private Sub GridEXSatr_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GridEXSatr.DoubleClick
        Try
            If GridEXSatr.CurrentRow.RowType = Janus.Windows.GridEX.RowType.FilterRow Then
                Exit Sub
            End If

            Mode = UD_Dll.Enums.GL_ModeForms.UpdateRow
            SetFormSatrData()
            SetFormData()
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> GridEXSatr_DoubleClick")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> GridEXSatr_DoubleClick")
        End Try
    End Sub
    Private Sub GridEXSatr_RowCheckStateChanging(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.RowCheckStateChangingEventArgs) Handles GridEXSatr.RowCheckStateChanging
        If GridEXSatr.GetCheckedRows().Length > 0 Then
            For i As Integer = 0 To GridEXSatr.GetCheckedRows().Length - 1
                If GridEXSatr.GetCheckedRows(i).CheckState = Janus.Windows.GridEX.RowCheckState.Checked Then
                    GridEXSatr.GetCheckedRows(i).CheckState = Janus.Windows.GridEX.RowCheckState.Unchecked
                End If
            Next
        End If
    End Sub
#End Region
#Region "Global Form Code"
    Private Sub Search()
        Dim cn As New SqlConnection
        Dim cm As New SqlCommand
        Dim da As New SqlDataAdapter
        Dim strsql As String = ""

        If dsForm.Tables.Contains("tblAN_AnbarGardani") Then
            dsForm.Tables("tblAN_AnbarGardani").Clear()
        End If

        Try

            cn = New SqlConnection(ConnectionString)
            cn.Open()

            strsql = "WareHouse.spAnbarGardani_SearchTitr "

            cm = New SqlCommand(strsql, cn)
            cm.CommandType = CommandType.StoredProcedure
            cm.Parameters.Clear()

            cm.Parameters.AddWithValue("ccAnbar", cmbNameAnbarSearch.SelectedValue)
            cm.Parameters.AddWithValue("CodeDoreh", cmbDorehSearch.SelectedValue)
            cm.Parameters.AddWithValue("TarikhShoro", mskAzTarikhSearch.Text)
            cm.Parameters.AddWithValue("TarikhPayan", mskTaTarikhSearch.Text)

            da = New SqlDataAdapter(cm)
            da.Fill(dsForm, "tblAN_AnbarGardani")
            dvTitr = New DataView
            dvTitr = dsForm.Tables("tblAN_AnbarGardani").DefaultView

            setGridStyleTitr()

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> Search")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> Search")
        End Try

    End Sub
    Private Sub SearchSatr()
        Dim cn As New SqlConnection
        Dim cm As New SqlCommand
        Dim da As New SqlDataAdapter
        Dim strsql As String = ""

        If dsForm.Tables.Contains("tblAN_AnbarGardaniSatr") Then
            dsForm.Tables("tblAN_AnbarGardaniSatr").Clear()
        End If

        Try

            cn = New SqlConnection(ConnectionString)
            cn.Open()

            strsql = "WareHouse.spAnbarGardani_SearchSatr"

            cm = New SqlCommand(strsql, cn)
            cm.CommandType = CommandType.StoredProcedure
            cm.Parameters.Clear()

            cm.Parameters.AddWithValue("ccAnbarGardanyTitr", ccAnbarGardanyTitr)


            da = New SqlDataAdapter(cm)
            da.Fill(dsForm, "tblAN_AnbarGardaniSatr")
            dvSatr = New DataView
            dvSatr = dsForm.Tables("tblAN_AnbarGardaniSatr").DefaultView

            setGridStyleSatr()

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> SearchSatr")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> SearchSatr")
        End Try

    End Sub
    Private Sub SearchSatr2()
        Dim cn As New SqlConnection
        Dim cm As New SqlCommand
        Dim da As New SqlDataAdapter
        Dim strsql As String = ""

        If dsForm.Tables.Contains("tblAN_AnbarGardaniSatr2") Then
            dsForm.Tables("tblAN_AnbarGardaniSatr2").Clear()
        End If

        Try

            cn = New SqlConnection(ConnectionString)
            cn.Open()

            strsql = "WareHouse.spAnbarGardani_SearchSatr2"

            cm = New SqlCommand(strsql, cn)
            cm.CommandType = CommandType.StoredProcedure
            cm.Parameters.Clear()

            cm.Parameters.AddWithValue("ccAnbarGardanySatr", ccAnbarGardanySatr)

            da = New SqlDataAdapter(cm)
            da.Fill(dsForm, "tblAN_AnbarGardaniSatr2")
            dvSatr2 = New DataView
            dvSatr2 = dsForm.Tables("tblAN_AnbarGardaniSatr2").DefaultView

            setGridStyleSatr2()

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> SearchSatr2")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> SearchSatr2")
        End Try

    End Sub
    Private Sub SetGroupBox()
        Try
            Select Case Flag
                Case 0
                    grbAnbarGardani.Visible = True
                    grbShomaresh.Visible = False
                    grbShomareshKala.Visible = False
                Case 1
                    grbAnbarGardani.Visible = False
                    grbShomaresh.Visible = True
                    grbShomareshKala.Visible = False
                Case 2
                    grbAnbarGardani.Visible = False
                    grbShomaresh.Visible = False
                    grbShomareshKala.Visible = True
            End Select

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> SetGroupBox")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> SetGroupBox")
        End Try
    End Sub
    Private Sub SetFormData()
        Try
            GridEXTitr.Enabled = True
            GridEXSatr.Enabled = True
            GridEXTitr.Height = TitrOrgSize
            GridEXSatr.Height = SatrOrgSize
            btnNewTitr.Enabled = True
            btnSaveTitr.Enabled = False
            btnRemoveTitr.Enabled = True
            btnCancelTitr.Enabled = False
            btnNewSatr.Enabled = True
            btnSaveSatr.Enabled = False
            btnDeleteSatr.Enabled = True
            btnCancelSatr.Enabled = False
            btnSaveSatr2.Enabled = False
            Select Case Mode
                Case UD_Dll.Enums.GL_ModeForms.AddNewRecord
                    GridEXTitr.Height = TitrOrgSize - TitrGridSize
                    GridEXTitr.Enabled = False
                    GridEXSatr.Enabled = False
                    btnNewTitr.Enabled = False
                    btnSaveTitr.Enabled = True
                    btnRemoveTitr.Enabled = False
                    btnCancelTitr.Enabled = True
                    btnNewSatr.Enabled = False
                    btnDeleteSatr.Enabled = False
                Case UD_Dll.Enums.GL_ModeForms.UpdateRecord
                    GridEXTitr.Height = TitrOrgSize - TitrGridSize
                    GridEXTitr.Enabled = False
                    GridEXSatr.Enabled = False
                    btnNewTitr.Enabled = False
                    btnSaveTitr.Enabled = True
                    btnRemoveTitr.Enabled = False
                    btnCancelTitr.Enabled = True
                    btnDeleteSatr.Enabled = False
                    btnNewSatr.Enabled = False
                Case UD_Dll.Enums.GL_ModeForms.AddNewRow
                    GridEXSatr.Height = SatrOrgSize - SatrGridSize
                    GridEXTitr.Enabled = False
                    GridEXSatr.Enabled = False
                    btnNewSatr.Enabled = False
                    btnSaveSatr.Enabled = True
                    btnDeleteSatr.Enabled = False
                    btnCancelSatr.Enabled = True
                Case UD_Dll.Enums.GL_ModeForms.UpdateRow
                    GridEXSatr.Height = SatrOrgSize - SatrGridSize
                    GridEXTitr.Enabled = False
                    GridEXSatr.Enabled = False
                    btnNewSatr.Enabled = False
                    btnSaveSatr.Enabled = True
                    btnCancelSatr.Enabled = True
                    btnDeleteSatr.Enabled = False
            End Select
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> SetFormData")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> SetFormData")
        End Try
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
            CodeDoreh = "1402"
            txtCaption = "انبارگردانی"
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
    Private Sub ClearForm()
        cmbNameAnbar.SelectedIndex = 0
        cmbDoreh.SelectedText = CodeMahalFaal
        mskAzTarikh.Text = TarikhEmrooz
        mskTaTarikh.Text = ""
        txtSharh.Text = ""
        cmbNoeAnbargardani.SelectedIndex = 0
        mskAzTarikhSatr.Text = TarikhEmrooz
        mskTaTarikhSatr.Text = ""
    End Sub
    Private Sub SetFormTitrData()
        Try
            If dvTitr.Count = 0 Or cmTitr.Position = -1 Then
                Mode = UD_Dll.Enums.GL_ModeForms.None
                Exit Sub
            End If

            Dim drvTemp As DataRowView
            drvTemp = dvTitr(cmTitr.Position)
            cmbNameAnbar.SelectedValue = drvTemp("ccAnbar")
            cmbDoreh.SelectedValue = drvTemp("CodeDoreh")
            mskAzTarikh.Text = drvTemp("TarikhShoro")
            mskTaTarikh.Text = drvTemp("TarikhPayan")
            txtSharh.Text = drvTemp("Sharh")

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> SetFormTitrData")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> SetFormTitrData")
        End Try
    End Sub
    Private Sub SetFormSatrData()
        Try
            If objTools.DLookup("Vazeiat", "tblAN_AnbarGardany", "ccAnbarGardanyTitr = " & ccAnbarGardanyTitr) = True Then
                MsgBox("انبارگردانی تایید شده است، امکان ویرایش ندارید", MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " پیام")
                Exit Sub
            End If
            If dvSatr.Count = 0 Or cmSatr.Position = -1 Then
                Mode = UD_Dll.Enums.GL_ModeForms.None
                Exit Sub
            End If

            Dim drvTemp As DataRowView
            drvTemp = dvSatr(cmSatr.Position)
            cmbNoeAnbargardani.SelectedValue = drvTemp("NoeAnbarGardani")
            mskAzTarikhSatr.Text = drvTemp("TarikhShoro")
            mskTaTarikhSatr.Text = drvTemp("TarikhPayan")
            lblShomaresh.Text = drvTemp("Shomaresh")

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> SetFormSatrData")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> SetFormSatrData")
        End Try
    End Sub
    Private Sub AddNewRecord()

        Dim cn As New SqlConnection
        Dim cm As New SqlCommand
        Dim strSQL As String = ""

        Try

            If Not IsValidTitr("All") Then
                Exit Sub
            End If

            cn = New SqlConnection(ConnectionString)
            cn.Open()

            strSQL = "WareHouse.spAnbarGardani_InsertTitr"

            cm = New SqlCommand(strSQL, cn)
            cm.CommandType = CommandType.StoredProcedure
            cm.Parameters.Clear()

            cm.Parameters.AddWithValue("ccAnbar", cmbNameAnbar.SelectedValue)
            cm.Parameters.AddWithValue("CodeDoreh", cmbDoreh.SelectedValue)
            cm.Parameters.AddWithValue("TarikhShoro", mskAzTarikh.Text)
            cm.Parameters.AddWithValue("TarikhPayan", mskTaTarikh.Text)
            cm.Parameters.AddWithValue("Sharh", txtSharh.Text)
            cm.Parameters.AddWithValue("UserName", UserName)
            cm.Parameters.AddWithValue("Vazeiat", 0)

            cm.CommandTimeout = 9999999
            cm.ExecuteNonQuery()

            cn.Close()
            cm = Nothing : cn = Nothing

            Update_VazeiatAnbar(cmbNameAnbar.SelectedValue, 0)

            FlagIsValid = 1

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> AddNewRecord")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> AddNewRecord")
        End Try
    End Sub
    Private Sub Update_VazeiatAnbar(ByVal CodeAnbar As Integer, ByVal Faal As Integer)

        Dim cn As New SqlConnection
        Dim cm As New SqlCommand
        Dim strSQL As String = ""

        Try
            cn = New SqlConnection(ConnectionString)
            cn.Open()

            strSQL = "Warehouse.spAnbarGardani_UpdateVazeiatAnabar"

            cm = New SqlCommand(strSQL, cn)
            cm.CommandType = CommandType.StoredProcedure
            cm.Parameters.Clear()

            cm.Parameters.AddWithValue("ccAnbar", CodeAnbar)
            cm.Parameters.AddWithValue("Faal", Faal)

            cm.CommandTimeout = 999999
            cm.ExecuteNonQuery()

            cn.Close()
            cm = Nothing : cn = Nothing

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> Update_VazeiatAnbar")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> Update_VazeiatAnbar")
        End Try
    End Sub
    Private Sub UpdateRecord()
        Dim cn As New SqlConnection
        Dim cm As New SqlCommand
        Dim p As New SqlParameter
        Dim strSQL As String = ""

        Try

            If Not IsValidTitr("All") Then
                Exit Sub
            End If

            cn = New SqlConnection(ConnectionString)
            cn.Open()

            strSQL = "WareHouse.spAnbarGardani_UpdateTitr "

            cm = New SqlCommand(strSQL, cn)
            cm.CommandType = CommandType.StoredProcedure
            cm.Parameters.Clear()

            cm.Parameters.AddWithValue("ccAnbar", cmbNameAnbar.SelectedValue)
            cm.Parameters.AddWithValue("CodeDoreh", cmbDoreh.SelectedText)
            cm.Parameters.AddWithValue("TarikhShoro", mskAzTarikh.Text)
            cm.Parameters.AddWithValue("TarikhPayan", mskTaTarikh.Text)
            cm.Parameters.AddWithValue("Sharh", txtSharh.Text)
            cm.Parameters.AddWithValue("UserName", UserName)
            cm.Parameters.AddWithValue("Vazeiat", 0)
            cm.Parameters.AddWithValue("ccAnbarGardanyTitr", Val(GridEXTitr.CurrentRow.Cells("ccAnbarGardanyTitr").Text.Replace(",", "")))

            cm.CommandTimeout = 999999
            cm.ExecuteNonQuery()

            cm = Nothing
            cm.Connection.Close()
            cn.Close()

            FlagIsValid = 1

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> UpdateRecord")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> UpdateRecord")
        End Try
    End Sub
    Private Sub AddNewRow()
        Dim cn As New SqlConnection
        Dim cm As SqlCommand
        Dim strsql As String = ""

        Try
            If Not IsValidSatr("All") Then
                Exit Sub
            End If
            cn = New SqlConnection(ConnectionString)
            cn.Open()

            strsql = "WareHouse.spAnbarGardani_InsertSatr"

            cm = New SqlCommand(strsql, cn)
            cm.CommandType = CommandType.StoredProcedure
            cm.Parameters.Clear()

            cm.Parameters.AddWithValue("ccAnbarGardanyTitr", ccAnbarGardanyTitr)
            cm.Parameters.AddWithValue("TarikhShoro", mskAzTarikhSatr.Text)
            cm.Parameters.AddWithValue("TarikhPayan", mskTaTarikhSatr.Text)
            cm.Parameters.AddWithValue("Shomaresh", lblShomaresh.Text)
            cm.Parameters.AddWithValue("UserName", UserName)
            cm.Parameters.AddWithValue("NoeAnbarGardani", cmbNoeAnbargardani.SelectedIndex)
            cm.Parameters.AddWithValue("ccAnbarGardanySatr", ccAnbarGardanySatr)
            cm.Parameters("ccAnbarGardanySatr").Direction = ParameterDirection.Output

            cm.CommandTimeout = 999999
            cm.ExecuteNonQuery()

            ccAnbarGardanySatr = cm.Parameters("ccAnbarGardanySatr").Value

            cm = Nothing
            cn.Close()

            Mode = UD_Dll.Enums.GL_ModeForms.None
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> AddNewRow")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> AddNewRow")
        End Try
    End Sub
    Private Sub AddNewSatr2(ByVal noeAnbarGardany As Integer)
        Select Case noeAnbarGardany
            Case 0
                InsertSatr2_AllKala(ccAnbarGardanySatr, ccanbar)
            Case 1
                Dim ShomareshGhabl As Integer = objTools.DLookup("Shomaresh", "tblAN_AnbarGardanySatr", "ccAnbarGardanySatr = " & ccAnbarGardanySatr) - 1
                Dim ccAnbarGardanySatrGhabl As Integer = objTools.DLookup("ccAnbarGardanySatr", "tblAN_AnbarGardanySatr", "ccAnbarGardanyTitr = " & ccAnbarGardanyTitr & " AND Shomaresh = " & ShomareshGhabl)
                InsertSatr2_KalahayeMoghayeratDar(ccAnbarGardanySatr, ccAnbarGardanySatrGhabl, ccanbar)
            Case 2
                Dim ShomareshGhabl As Integer = objTools.DLookup("Shomaresh", "tblAN_AnbarGardanySatr", "ccAnbarGardanySatr = " & ccAnbarGardanySatr) - 1
                Dim ccAnbarGardanySatrGhabl As Integer = objTools.DLookup("ccAnbarGardanySatr", "tblAN_AnbarGardanySatr", "ccAnbarGardanyTitr = " & ccAnbarGardanyTitr & " AND Shomaresh = " & ShomareshGhabl)
                Dim ccAnbarGardanySatrGhabl2 As Integer = objTools.DLookup("ccAnbarGardanySatr", "tblAN_AnbarGardanySatr", "ccAnbarGardanyTitr = " & ccAnbarGardanyTitr & " AND Shomaresh = " & ShomareshGhabl - 1)
                InsertSatr2_KalahayeMoghayeratDar2ShomaresheGhabl(ccAnbarGardanySatr, ccAnbarGardanySatrGhabl, ccAnbarGardanySatrGhabl2, ccanbar)
        End Select
    End Sub
    Private Sub UpdateRow()
        Dim cn As New SqlConnection
        Dim cm As New SqlCommand
        Dim strsql As String = ""

        Try
            cn = New SqlConnection(ConnectionString)
            cn.Open()

            strsql = "WareHouse.spAnbarGardani_UpdateSatr"

            cm = New SqlCommand(strsql, cn)
            cm.CommandType = CommandType.StoredProcedure
            cm.Parameters.Clear()

            cm.Parameters.AddWithValue("TarikhShoro", mskAzTarikhSatr.Text)
            cm.Parameters.AddWithValue("TarikhPayan", mskTaTarikhSatr.Text)
            cm.Parameters.AddWithValue("UserName", UserName)
            cm.Parameters.AddWithValue("NoeAnbarGardani", cmbNoeAnbargardani.SelectedIndex)
            cm.Parameters.AddWithValue("ccAnbarGardanySatr", Val(GridEXSatr.CurrentRow.Cells("ccAnbarGardanySatr").Text.Replace(",", "")))

            cm.CommandTimeout = 999999
            cm.ExecuteNonQuery()

            cn.Close()
            cm = Nothing : cn = Nothing

            FlagIsValid = 1

            Mode = UD_Dll.Enums.GL_ModeForms.None
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> UpdateRow")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> UpdateRow")
        End Try

    End Sub
    Private Sub setGridStyleTitr()
        Try
            With GridEXTitr
                .DataSource = Nothing
                .DataSource = dsForm.Tables("tblAN_AnbarGardani").DefaultView
                .SetDataBinding(dsForm.Tables("tblAN_AnbarGardani").DefaultView, "")
                .RetrieveStructure()
            End With

            For i As Integer = 0 To GridEXTitr.CurrentTable.Columns.Count - 1
                GridEXTitr.CurrentTable.Columns.Item(i).Visible = False
            Next

            GridEXTitr.CurrentTable.Columns.Item("Taeed").Caption = "انتخاب"
            GridEXTitr.CurrentTable.Columns.Item("Taeed").Visible = True
            GridEXTitr.CurrentTable.Columns.Item("Taeed").Width = 30
            GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXTitr.CurrentTable.Columns.Item("Taeed").Position = 0
            GridEXTitr.CurrentTable.Columns.Item("Taeed").Selectable = True
            GridEXTitr.CurrentTable.Columns.Item("Taeed").ActAsSelector = True
            GridEXTitr.CurrentTable.Columns.Item("Taeed").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXTitr.CurrentTable.Columns.Item("ccAnbarGardanyTitr").Caption = "ccAnbarGardanyTitr"
            GridEXTitr.CurrentTable.Columns.Item("ccAnbarGardanyTitr").Visible = False
            GridEXTitr.CurrentTable.Columns.Item("ccAnbarGardanyTitr").Width = 0
            GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXTitr.CurrentTable.Columns.Item("ccAnbarGardanyTitr").Position = 1
            GridEXTitr.CurrentTable.Columns.Item("ccAnbarGardanyTitr").FormatString = "N"
            GridEXTitr.CurrentTable.Columns.Item("ccAnbarGardanyTitr").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXTitr.CurrentTable.Columns.Item("ccAnbar").Caption = "ccAnbar"
            GridEXTitr.CurrentTable.Columns.Item("ccAnbar").Visible = False
            GridEXTitr.CurrentTable.Columns.Item("ccAnbar").Width = 0
            GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXTitr.CurrentTable.Columns.Item("ccAnbar").Position = 2
            GridEXTitr.CurrentTable.Columns.Item("ccAnbar").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXTitr.CurrentTable.Columns.Item("NameAnbar").Caption = "نام انبار"
            GridEXTitr.CurrentTable.Columns.Item("NameAnbar").Visible = True
            GridEXTitr.CurrentTable.Columns.Item("NameAnbar").Width = 190
            GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXTitr.CurrentTable.Columns.Item("NameAnbar").Position = 3
            GridEXTitr.CurrentTable.Columns.Item("NameAnbar").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXTitr.CurrentTable.Columns.Item("CodeDoreh").Caption = "دوره"
            GridEXTitr.CurrentTable.Columns.Item("CodeDoreh").Visible = True
            GridEXTitr.CurrentTable.Columns.Item("CodeDoreh").Width = 60
            GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXTitr.CurrentTable.Columns.Item("CodeDoreh").Position = 4
            GridEXTitr.CurrentTable.Columns.Item("CodeDoreh").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXTitr.CurrentTable.Columns.Item("TarikhShoroSlash").Caption = "تاریخ شروع"
            GridEXTitr.CurrentTable.Columns.Item("TarikhShoroSlash").Visible = True
            GridEXTitr.CurrentTable.Columns.Item("TarikhShoroSlash").Width = 80
            GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXTitr.CurrentTable.Columns.Item("TarikhShoroSlash").Position = 5
            GridEXTitr.CurrentTable.Columns.Item("TarikhShoroSlash").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXTitr.CurrentTable.Columns.Item("TarikhShoro").Caption = "TarikhShoro"
            GridEXTitr.CurrentTable.Columns.Item("TarikhShoro").Visible = False
            GridEXTitr.CurrentTable.Columns.Item("TarikhShoro").Width = 0
            GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXTitr.CurrentTable.Columns.Item("TarikhShoro").Position = 6
            GridEXTitr.CurrentTable.Columns.Item("TarikhShoro").FormatString = "N"
            GridEXTitr.CurrentTable.Columns.Item("TarikhShoro").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXTitr.CurrentTable.Columns.Item("TarikhPayanSlash").Caption = "تاریخ پایان"
            GridEXTitr.CurrentTable.Columns.Item("TarikhPayanSlash").Visible = True
            GridEXTitr.CurrentTable.Columns.Item("TarikhPayanSlash").Width = 80
            GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXTitr.CurrentTable.Columns.Item("TarikhPayanSlash").Position = 7
            GridEXTitr.CurrentTable.Columns.Item("TarikhPayanSlash").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXTitr.CurrentTable.Columns.Item("TarikhPayan").Caption = "TarikhPayan"
            GridEXTitr.CurrentTable.Columns.Item("TarikhPayan").Visible = False
            GridEXTitr.CurrentTable.Columns.Item("TarikhPayan").Width = 0
            GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXTitr.CurrentTable.Columns.Item("TarikhPayan").Position = 8
            GridEXTitr.CurrentTable.Columns.Item("TarikhPayan").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXTitr.CurrentTable.Columns.Item("Sharh").Caption = "شــــرح"
            GridEXTitr.CurrentTable.Columns.Item("Sharh").Visible = True
            GridEXTitr.CurrentTable.Columns.Item("Sharh").Width = 140
            GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXTitr.CurrentTable.Columns.Item("Sharh").Position = 9
            GridEXTitr.CurrentTable.Columns.Item("Sharh").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXTitr.CurrentTable.Columns.Item("txtVazeiat").Caption = "وضعیت"
            GridEXTitr.CurrentTable.Columns.Item("txtVazeiat").Visible = True
            GridEXTitr.CurrentTable.Columns.Item("txtVazeiat").Width = 70
            GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXTitr.CurrentTable.Columns.Item("txtVazeiat").Position = 10
            GridEXTitr.CurrentTable.Columns.Item("txtVazeiat").FormatString = "N"
            GridEXTitr.CurrentTable.Columns.Item("txtVazeiat").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXTitr.CurrentTable.Columns.Item("Vazeiat").Caption = "Vazeiat"
            GridEXTitr.CurrentTable.Columns.Item("Vazeiat").Visible = False
            GridEXTitr.CurrentTable.Columns.Item("Vazeiat").Width = 0
            GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXTitr.CurrentTable.Columns.Item("Vazeiat").Position = 11
            GridEXTitr.CurrentTable.Columns.Item("Vazeiat").FormatString = "N"
            GridEXTitr.CurrentTable.Columns.Item("Vazeiat").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            For i As Integer = 0 To GridEXTitr.RootTable.Columns.Count - 1
                If GridEXTitr.RootTable.Columns(i).Type.IsValueType Then
                    GridEXTitr.RootTable.Columns(i).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                    GridEXTitr.RootTable.Columns(i).FormatMode = Janus.Windows.GridEX.FormatMode.UseIFormattable
                    GridEXTitr.RootTable.Columns(i).FormatString = "N"
                    GridEXTitr.RootTable.Columns(i).TotalFormatMode = Janus.Windows.GridEX.FormatMode.UseIFormattable
                    GridEXTitr.RootTable.Columns(i).TotalFormatString = "N"
                End If
            Next

            GridEXTitr.Visible = True
            GridEXTitr.HeaderFormatStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> setGridStyleTitr")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> setGridStyleTitr")
        End Try
    End Sub
    Private Sub setGridStyleSatr()
        Try
            If dvSatr.Count = 0 Then
                Exit Sub
            End If
            With GridEXSatr
                .DataSource = Nothing
                .DataSource = dsForm.Tables("tblAN_AnbarGardaniSatr").DefaultView
                .SetDataBinding(dsForm.Tables("tblAN_AnbarGardaniSatr").DefaultView, "")
                .RetrieveStructure()
            End With

            For i As Integer = 0 To GridEXSatr.CurrentTable.Columns.Count - 1
                GridEXSatr.CurrentTable.Columns.Item(i).Visible = False
            Next

            GridEXSatr.CurrentTable.Columns.Item("Taeed").Caption = "انتخاب"
            GridEXSatr.CurrentTable.Columns.Item("Taeed").Visible = True
            GridEXSatr.CurrentTable.Columns.Item("Taeed").Width = 40
            GridEXSatr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXSatr.CurrentTable.Columns.Item("Taeed").Position = 0
            GridEXSatr.CurrentTable.Columns.Item("Taeed").Selectable = True
            GridEXSatr.CurrentTable.Columns.Item("Taeed").ActAsSelector = True
            GridEXSatr.CurrentTable.Columns.Item("Taeed").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXSatr.CurrentTable.Columns.Item("ccAnbarGardanySatr").Caption = "ccAnbarGardanySatr"
            GridEXSatr.CurrentTable.Columns.Item("ccAnbarGardanySatr").Visible = False
            GridEXSatr.CurrentTable.Columns.Item("ccAnbarGardanySatr").Width = 0
            GridEXSatr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXSatr.CurrentTable.Columns.Item("ccAnbarGardanySatr").Position = 1
            GridEXSatr.CurrentTable.Columns.Item("ccAnbarGardanySatr").FormatString = "N"
            GridEXSatr.CurrentTable.Columns.Item("ccAnbarGardanySatr").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXSatr.CurrentTable.Columns.Item("Shomaresh").Caption = "شمارش"
            GridEXSatr.CurrentTable.Columns.Item("Shomaresh").Visible = True
            GridEXSatr.CurrentTable.Columns.Item("Shomaresh").Width = 100
            GridEXSatr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXSatr.CurrentTable.Columns.Item("Shomaresh").Position = 2
            GridEXSatr.CurrentTable.Columns.Item("Shomaresh").FormatString = "N"
            GridEXSatr.CurrentTable.Columns.Item("Shomaresh").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXSatr.CurrentTable.Columns.Item("txtNoeAnbarGardani").Caption = "نوع انبارگردانی"
            GridEXSatr.CurrentTable.Columns.Item("txtNoeAnbarGardani").Visible = True
            GridEXSatr.CurrentTable.Columns.Item("txtNoeAnbarGardani").Width = 300
            GridEXSatr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXSatr.CurrentTable.Columns.Item("txtNoeAnbarGardani").Position = 3
            GridEXSatr.CurrentTable.Columns.Item("txtNoeAnbarGardani").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXSatr.CurrentTable.Columns.Item("TarikhShoroSlash").Caption = "تاریخ شروع"
            GridEXSatr.CurrentTable.Columns.Item("TarikhShoroSlash").Visible = True
            GridEXSatr.CurrentTable.Columns.Item("TarikhShoroSlash").Width = 110
            GridEXSatr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXSatr.CurrentTable.Columns.Item("TarikhShoroSlash").Position = 4
            GridEXSatr.CurrentTable.Columns.Item("TarikhShoroSlash").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXSatr.CurrentTable.Columns.Item("TarikhShoro").Caption = "TarikhShoro"
            GridEXSatr.CurrentTable.Columns.Item("TarikhShoro").Visible = False
            GridEXSatr.CurrentTable.Columns.Item("TarikhShoro").Width = 0
            GridEXSatr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXSatr.CurrentTable.Columns.Item("TarikhShoro").Position = 5
            GridEXSatr.CurrentTable.Columns.Item("TarikhShoro").FormatString = "N"
            GridEXSatr.CurrentTable.Columns.Item("TarikhShoro").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXSatr.CurrentTable.Columns.Item("TarikhPayanSlash").Caption = "تاریخ پایان"
            GridEXSatr.CurrentTable.Columns.Item("TarikhPayanSlash").Visible = True
            GridEXSatr.CurrentTable.Columns.Item("TarikhPayanSlash").Width = 110
            GridEXSatr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXSatr.CurrentTable.Columns.Item("TarikhPayanSlash").Position = 6
            GridEXSatr.CurrentTable.Columns.Item("TarikhPayanSlash").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXSatr.CurrentTable.Columns.Item("TarikhPayan").Caption = "TarikhPayan"
            GridEXSatr.CurrentTable.Columns.Item("TarikhPayan").Visible = False
            GridEXSatr.CurrentTable.Columns.Item("TarikhPayan").Width = 0
            GridEXSatr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXSatr.CurrentTable.Columns.Item("TarikhPayan").Position = 7
            GridEXSatr.CurrentTable.Columns.Item("TarikhPayan").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXSatr.CurrentTable.Columns.Item("NoeAnbarGardani").Caption = "NoeAnbarGardani"
            GridEXSatr.CurrentTable.Columns.Item("NoeAnbarGardani").Visible = False
            GridEXSatr.CurrentTable.Columns.Item("NoeAnbarGardani").Width = 0
            GridEXSatr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXSatr.CurrentTable.Columns.Item("NoeAnbarGardani").Position = 8
            GridEXSatr.CurrentTable.Columns.Item("NoeAnbarGardani").FormatString = "N"
            GridEXSatr.CurrentTable.Columns.Item("NoeAnbarGardani").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            For i As Integer = 0 To GridEXSatr.RootTable.Columns.Count - 1
                If GridEXSatr.RootTable.Columns(i).Type.IsValueType Then
                    GridEXSatr.RootTable.Columns(i).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                    GridEXSatr.RootTable.Columns(i).FormatMode = Janus.Windows.GridEX.FormatMode.UseIFormattable
                    GridEXSatr.RootTable.Columns(i).FormatString = "N"
                    GridEXSatr.RootTable.Columns(i).TotalFormatMode = Janus.Windows.GridEX.FormatMode.UseIFormattable
                    GridEXSatr.RootTable.Columns(i).TotalFormatString = "N"
                End If
            Next

            GridEXSatr.Visible = True
            GridEXSatr.HeaderFormatStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> setGridStyleSatr")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> setGridStyleSatr")
        End Try
    End Sub
    Private Sub setGridStyleSatr2()
        Try
            If dvSatr2.Count = 0 Then
                Exit Sub
            End If
            With GridEXSatr2
                .DataSource = Nothing
                .DataSource = dsForm.Tables("tblAN_AnbarGardaniSatr2").DefaultView
                .SetDataBinding(dsForm.Tables("tblAN_AnbarGardaniSatr2").DefaultView, "")
                .RetrieveStructure()
            End With

            For i As Integer = 0 To GridEXSatr2.CurrentTable.Columns.Count - 1
                GridEXSatr2.CurrentTable.Columns.Item(i).Visible = False
            Next

            GridEXSatr2.CurrentTable.Columns.Item("Taeed").Caption = "انتخاب"
            GridEXSatr2.CurrentTable.Columns.Item("Taeed").Visible = False
            GridEXSatr2.CurrentTable.Columns.Item("Taeed").Width = 0
            GridEXSatr2.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXSatr2.CurrentTable.Columns.Item("Taeed").Position = 0
            GridEXSatr2.CurrentTable.Columns.Item("Taeed").Selectable = True
            GridEXSatr2.CurrentTable.Columns.Item("Taeed").ActAsSelector = True
            GridEXSatr2.CurrentTable.Columns.Item("Taeed").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXSatr2.CurrentTable.Columns.Item("ccAnbarGardanySatr2").Caption = "ccAnbarGardanySatr2"
            GridEXSatr2.CurrentTable.Columns.Item("ccAnbarGardanySatr2").Visible = False
            GridEXSatr2.CurrentTable.Columns.Item("ccAnbarGardanySatr2").Width = 0
            GridEXSatr2.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXSatr2.CurrentTable.Columns.Item("ccAnbarGardanySatr2").Position = 0
            GridEXSatr2.CurrentTable.Columns.Item("ccAnbarGardanySatr2").FormatString = "N"
            GridEXSatr2.CurrentTable.Columns.Item("ccAnbarGardanySatr2").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXSatr2.CurrentTable.Columns.Item("ccKala").Caption = "ccKala"
            GridEXSatr2.CurrentTable.Columns.Item("ccKala").Visible = False
            GridEXSatr2.CurrentTable.Columns.Item("ccKala").Width = 0
            GridEXSatr2.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXSatr2.CurrentTable.Columns.Item("ccKala").Position = 1
            GridEXSatr2.CurrentTable.Columns.Item("ccKala").FormatString = "N"
            GridEXSatr2.CurrentTable.Columns.Item("ccKala").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXSatr2.CurrentTable.Columns.Item("CodeKala").Caption = "کــد کالا"
            GridEXSatr2.CurrentTable.Columns.Item("CodeKala").Visible = True
            GridEXSatr2.CurrentTable.Columns.Item("CodeKala").Width = 70
            GridEXSatr2.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXSatr2.CurrentTable.Columns.Item("CodeKala").Position = 2
            'GridEXSatr2.CurrentTable.Columns.Item("CodeKala").EditType = Janus.Windows.GridEX.EditType.NoEdit
            GridEXSatr2.CurrentTable.Columns.Item("CodeKala").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

           

            GridEXSatr2.CurrentTable.Columns.Item("NameKala").Caption = "نـــام کالا"
            GridEXSatr2.CurrentTable.Columns.Item("NameKala").Visible = True
            GridEXSatr2.CurrentTable.Columns.Item("NameKala").Width = 240
            GridEXSatr2.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXSatr2.CurrentTable.Columns.Item("NameKala").Position = 3
            'GridEXSatr2.CurrentTable.Columns.Item("NameKala").EditType = Janus.Windows.GridEX.EditType.NoEdit
            GridEXSatr2.CurrentTable.Columns.Item("NameKala").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center


            GridEXSatr2.CurrentTable.Columns.Item("ShomarehBach").Caption = "شماره بچ"
            GridEXSatr2.CurrentTable.Columns.Item("ShomarehBach").Visible = True
            GridEXSatr2.CurrentTable.Columns.Item("ShomarehBach").Width = 150
            GridEXSatr2.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXSatr2.CurrentTable.Columns.Item("ShomarehBach").Position = 4
            GridEXSatr2.CurrentTable.Columns.Item("ShomarehBach").EditType = Janus.Windows.GridEX.EditType.NoEdit
            GridEXSatr2.CurrentTable.Columns.Item("ShomarehBach").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXSatr2.CurrentTable.Columns.Item("TedadComputer").Caption = "موجودی"
            GridEXSatr2.CurrentTable.Columns.Item("TedadComputer").Visible = True
            GridEXSatr2.CurrentTable.Columns.Item("TedadComputer").Width = 90
            GridEXSatr2.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXSatr2.CurrentTable.Columns.Item("TedadComputer").Position = 5
            GridEXSatr2.CurrentTable.Columns.Item("TedadComputer").FormatString = "G"
            GridEXSatr2.CurrentTable.Columns.Item("TedadComputer").EditType = Janus.Windows.GridEX.EditType.NoEdit
            GridEXSatr2.CurrentTable.Columns.Item("TedadComputer").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXSatr2.CurrentTable.Columns.Item("TedadAnbar").Caption = "تعداد شمارش"
            GridEXSatr2.CurrentTable.Columns.Item("TedadAnbar").Visible = True
            GridEXSatr2.CurrentTable.Columns.Item("TedadAnbar").Width = 90
            GridEXSatr2.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXSatr2.CurrentTable.Columns.Item("TedadAnbar").Position = 6
            GridEXSatr2.CurrentTable.Columns.Item("TedadAnbar").FormatString = "G"
            GridEXSatr2.CurrentTable.Columns.Item("TedadAnbar").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXSatr2.CurrentTable.Columns.Item("Moghayerat").Caption = "مغایرت"
            GridEXSatr2.CurrentTable.Columns.Item("Moghayerat").Visible = True
            GridEXSatr2.CurrentTable.Columns.Item("Moghayerat").Width = 80
            GridEXSatr2.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXSatr2.CurrentTable.Columns.Item("Moghayerat").Position = 7
            GridEXSatr2.CurrentTable.Columns.Item("Moghayerat").FormatString = "G"
            GridEXSatr2.CurrentTable.Columns.Item("Moghayerat").EditType = Janus.Windows.GridEX.EditType.NoEdit
            GridEXSatr2.CurrentTable.Columns.Item("Moghayerat").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            For i As Integer = 0 To GridEXSatr2.RootTable.Columns.Count - 1
                If GridEXSatr2.RootTable.Columns(i).Type.IsValueType Then
                    GridEXSatr2.RootTable.Columns(i).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                    GridEXSatr2.RootTable.Columns(i).FormatMode = Janus.Windows.GridEX.FormatMode.UseIFormattable
                    GridEXSatr2.RootTable.Columns(i).FormatString = "G"
                    GridEXSatr2.RootTable.Columns(i).TotalFormatMode = Janus.Windows.GridEX.FormatMode.UseIFormattable
                    GridEXSatr2.RootTable.Columns(i).TotalFormatString = "G"
                End If
            Next

            GridEXSatr2.Visible = True
            GridEXSatr2.HeaderFormatStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> setGridStyleSatr2")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> setGridStyleSatr2")
        End Try
    End Sub
    Private Sub BoundCurrencyManagerTitr()
        Try
            cmTitr = CType(BindingContext(GridEXTitr.DataSource), CurrencyManager)
            AddHandler cmTitr.PositionChanged, AddressOf cmTitr_PositionChanged

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> BoundCurrencyManagerTitr")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> BoundCurrencyManagerTitr")
        End Try
    End Sub
    Private Sub BoundCurrencyManagerSatr()
        Try
            cmSatr = CType(BindingContext(GridEXSatr.DataSource), CurrencyManager)
            AddHandler cmSatr.PositionChanged, AddressOf cmSatr_PositionChanged

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> BoundCurrencyManagerSatr")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> BoundCurrencyManagerSatr")
        End Try

    End Sub
    Private Function IsValidTitr(ByVal CheckField As String) As Boolean
        Try
            IsValidTitr = False

            If CheckField = "cmbNameAnbar" Or CheckField = "All" Then
                If cmbNameAnbar.SelectedIndex = -1 And cmbNameAnbar.SelectedValue = 0 Then
                    ErrPro.SetError(Me.cmbNameAnbar, "انبار را انتخاب کنید.")
                    MsgBox("انبار را انتخاب کنيد.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
                    cmbNameAnbar.Focus()
                    Exit Function
                End If
                ErrPro.SetError(Me.cmbNameAnbar, "")
            End If

            If CheckField = "cmbNameAnbar" Or CheckField = "All" Then
                If objTools.DCount("ccAnbarGardanyTitr", "tblAN_AnbarGardany", "Vazeiat = 0 AND ccAnbar = " & cmbNameAnbar.SelectedValue & " AND CodeDoreh = " & cmbDoreh.SelectedValue) > 0 Then
                    ErrPro.SetError(Me.cmbNameAnbar, "برای این انبار در دوره انتخابی انبارگردانی تایید نشده وجود دارد .")
                    MsgBox("برای این انبار در دوره انتخابی انبارگردانی تایید نشده وجود دارد .", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
                    cmbNameAnbar.Focus()
                    Exit Function
                End If
                ErrPro.SetError(Me.cmbNameAnbar, "")
            End If

            If CheckField = "cmbDoreh" Or CheckField = "All" Then
                If cmbDoreh.SelectedIndex = -1 And cmbDoreh.SelectedValue = 0 Then
                    ErrPro.SetError(Me.cmbDoreh, "دوره را انتخاب کنید.")
                    MsgBox("دوره را انتخاب کنید.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
                    cmbDoreh.Focus()
                    Exit Function
                End If
                ErrPro.SetError(Me.cmbDoreh, "")
            End If

            If CheckField = "mskAzTarikh" Or CheckField = "All" Then
                If Len(mskAzTarikh.Text.ToString) <> 0 Then
                    If Not objTarikh.IsShDate(mskAzTarikh.Text.ToString) Then
                        mskAzTarikh.Focus()
                        Exit Function
                    End If
                Else
                    ErrPro.SetError(Me.mskAzTarikh, "تاريخ شروع انبارگردانی را وارد کنيد.")
                    MsgBox("تاريخ شروع انبارگردانی را وارد کنيد.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
                    mskAzTarikh.Focus()
                    Exit Function
                End If
                If mskAzTarikh.Text.Substring(0, 4) <> cmbDoreh.SelectedValue Then
                    MsgBox("تاريخ شروع انبارگردانی با دوره مالی انتخاب شده یکی نيست.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
                    mskAzTarikh.Focus()
                    Exit Function
                End If
                ErrPro.SetError(Me.mskAzTarikh, "")
            End If

            If CheckField = "mskTaTarikh" Or CheckField = "All" Then
                If Len(mskTaTarikh.Text.ToString) <> 0 Then
                    If Not objTarikh.IsShDate(mskTaTarikh.Text.ToString) Then
                        mskTaTarikh.Focus()
                        Exit Function
                    End If
                Else
                    ErrPro.SetError(Me.mskTaTarikh, "تاريخ پایان انبارگردانی را وارد کنيد.")
                    MsgBox("تاريخ پایان انبارگردانی را وارد کنيد.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
                    mskTaTarikh.Focus()
                    Exit Function
                End If
                If mskTaTarikh.Text.Substring(0, 4) <> cmbDoreh.SelectedValue Then
                    MsgBox("تاريخ پایان انبارگردانی با دوره مالی انتخاب شده یکی نيست.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
                    mskTaTarikh.Focus()
                    Exit Function
                End If
                'If mskTaTarikh.Text > TarikhEmrooz Then
                '    MsgBox("تاريخ پایان انبارگردانی نمی تواند از تاریخ امروز جلوتر باشد.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
                '    mskTaTarikh.Focus()
                '    Exit Function
                'End If
                ErrPro.SetError(Me.mskTaTarikh, "")
            End If

            If CheckField = "All" Then
                If CType(mskTaTarikh.Text.ToString, Integer) < CType(mskAzTarikh.Text.ToString, Integer) Then
                    ErrPro.SetError(Me.mskTaTarikh, "تاريخ پایان انبارگردانی نمی تواند از تاریخ شروع انبارگردانی کوچکتر باشد.")
                    MsgBox("تاريخ پایان انبارگردانی نمی تواند از تاریخ شروع انبارگردانی کوچکتر باشد.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
                    mskTaTarikh.Focus()
                    Exit Function
                End If
                ErrPro.SetError(Me.mskTaTarikh, "")
            End If

            Return True
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> IsValidTitr")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> IsValidTitr")
        End Try
    End Function
    Private Function IsValidSatr(ByVal CheckField As String) As Boolean
        Try
            IsValidSatr = False

            Dim IsValidShomaresh As Integer = objTools.DCount("ccAnbarGardanySatr", "tblAN_AnbarGardanySatr", "ccAnbarGardanyTitr = " & ccAnbarGardanyTitr)

            If CheckField = "cmbNoeAnbargar" Or CheckField = "All" Then
                If (cmbNoeAnbargardani.SelectedIndex = 1 Or cmbNoeAnbargardani.SelectedIndex = 2) And IsValidShomaresh < 1 Then
                    MsgBox("در شمارش اول تنها می توان شمارش تمام کالاها را انجام داد .", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "خطا")
                    cmbNoeAnbargardani.Focus()
                    Exit Function
                ElseIf cmbNoeAnbargardani.SelectedIndex = 2 And IsValidShomaresh < 2 Then
                    MsgBox("برای این انبار گردانی تنها 1 شمارش ثبت شده است . امکان انجام شمارش برای کالاهای مغایرت دار 2 شمارش قبل وجود ندارد .", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "خطا")
                    cmbNoeAnbargardani.Focus()
                    Exit Function
                End If
                ErrPro.SetError(Me.cmbNoeAnbargardani, "")
            End If

            If CheckField = "mskAzTarikhSatr" Or CheckField = "All" Then
                If Len(mskAzTarikhSatr.Text.ToString) <> 0 Then
                    If Not objTarikh.IsShDate(mskAzTarikhSatr.Text.ToString) Then
                        mskAzTarikhSatr.Focus()
                        Exit Function
                    End If
                Else
                    ErrPro.SetError(Me.mskAzTarikhSatr, "تاريخ شروع شمارش را وارد کنيد.")
                    MsgBox("تاريخ شروع شمارش را وارد کنيد.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
                    mskAzTarikhSatr.Focus()
                    Exit Function
                End If
                If Not AzTarikh <= CType(mskAzTarikhSatr.Text.ToString, Integer) <= TaTarikh Then
                    MsgBox("تاريخ شروع شمارش باید دربازه زمانی انبارگردانی تعریف گردد.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
                    mskAzTarikhSatr.Focus()
                    Exit Function
                End If
                ErrPro.SetError(Me.mskAzTarikhSatr, "")
            End If

            If CheckField = "mskTaTarikhSatr" Or CheckField = "All" Then
                If Len(mskTaTarikhSatr.Text.ToString) <> 0 Then
                    If Not objTarikh.IsShDate(mskTaTarikhSatr.Text.ToString) Then
                        mskTaTarikhSatr.Focus()
                        Exit Function
                    End If
                Else
                    ErrPro.SetError(Me.mskTaTarikhSatr, "تاريخ پایان شمارش را وارد کنيد.")
                    MsgBox("تاريخ پایان شمارش را وارد کنيد.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
                    mskTaTarikhSatr.Focus()
                    Exit Function
                End If
                If Not AzTarikh <= CType(mskTaTarikhSatr.Text.ToString, Integer) <= TaTarikh Then
                    MsgBox("تاريخ پایان شمارش باید دربازه زمانی انبارگردانی تعریف گردد.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
                    mskTaTarikhSatr.Focus()
                    Exit Function
                End If
                ErrPro.SetError(Me.mskTaTarikhSatr, "")
            End If

            If CheckField = "All" Then
                If CType(mskTaTarikhSatr.Text.ToString, Integer) < CType(mskAzTarikhSatr.Text.ToString, Integer) Then
                    ErrPro.SetError(Me.mskTaTarikhSatr, "تاريخ پایان شمارش نمی تواند از تاریخ شروع شمارش کوچکتر باشد.")
                    MsgBox("تاريخ پایان شمارش نمی تواند از تاریخ شروع شمارش کوچکتر باشد.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
                    mskTaTarikhSatr.Focus()
                    Exit Function
                End If
                ErrPro.SetError(Me.mskTaTarikhSatr, "")
            End If

            Return True
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> IsValidSatr")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> IsValidSatr")
        End Try
    End Function
    Private Sub InsertSatr2_AllKala(ByVal ccAnbarGardaniSatr As Integer, ByVal ccAnbar As Integer)
        Dim cn As New SqlConnection
        Dim cm As New SqlCommand
        Dim strSQL As String = ""

        Try
            cn = New SqlConnection(ConnectionString)
            cn.Open()

            strSQL = "WareHouse.spAnbarGardani_InsertSatr2_AllKala "

            cm = New SqlCommand(strSQL, cn)
            cm.CommandType = CommandType.StoredProcedure
            cm.Parameters.Clear()

            cm.Parameters.AddWithValue("ccAnbarGardanySatr", ccAnbarGardanySatr)
            cm.Parameters.AddWithValue("ccAnbar", ccAnbar)
            cm.Parameters.AddWithValue("TarikhAvaleDoreh", TarikhAvalDoreh)
            cm.Parameters.AddWithValue("AzTarikh", mskAzTarikhSatr.Text)
            cm.Parameters.AddWithValue("TaTarikh", mskTaTarikhSatr.Text)
            cm.Parameters.AddWithValue("UserName", UserName)
            cm.Parameters.AddWithValue("Tarikh", TarikhEmrooz)
            cm.Parameters.AddWithValue("Saat", Format(TimeOfDay, "HH:mm:ss"))

            cm.CommandTimeout = 999999
            cm.ExecuteNonQuery()

            cn.Close()
            cm = Nothing : cn = Nothing

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> InsertSatr2_AllKala")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> InsertSatr2_AllKala")
        End Try
    End Sub
    Private Sub InsertSatr2_KalahayeMoghayeratDar(ByVal ccAnbarGardaniSatr As Integer, ByVal ccAnbarGardanySatrGhabl As Integer, ByVal ccAnbar As Integer)
        Dim cn As New SqlConnection
        Dim cm As New SqlCommand
        Dim strSQL As String = ""

        Try
            cn = New SqlConnection(ConnectionString)
            cn.Open()

            strSQL = "WareHouse.spAnbarGardani_InsertSatr2_KalahayeMoghayeratDar "

            cm = New SqlCommand(strSQL, cn)
            cm.CommandType = CommandType.StoredProcedure
            cm.Parameters.Clear()

            cm.Parameters.AddWithValue("ccAnbarGardanySatr", ccAnbarGardanySatr)
            cm.Parameters.AddWithValue("ccAnbarGardanySatrGhabl", ccAnbarGardanySatrGhabl)
            cm.Parameters.AddWithValue("ccAnbar", ccAnbar)
            cm.Parameters.AddWithValue("TarikhAvaleDoreh", TarikhAvalDoreh)
            cm.Parameters.AddWithValue("AzTarikh", mskAzTarikhSatr.Text)
            cm.Parameters.AddWithValue("TaTarikh", mskTaTarikhSatr.Text)
            cm.Parameters.AddWithValue("UserName", UserName)
            cm.Parameters.AddWithValue("Tarikh", TarikhEmrooz)
            cm.Parameters.AddWithValue("Saat", Format(TimeOfDay, "HH:mm:ss"))

            cm.CommandTimeout = 999999
            cm.ExecuteNonQuery()

            cn.Close()
            cm = Nothing : cn = Nothing

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> InsertSatr2_KalahayeMoghayeratDar")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> InsertSatr2_KalahayeMoghayeratDar")
        End Try
    End Sub
    Private Sub InsertSatr2_KalahayeMoghayeratDar2ShomaresheGhabl(ByVal ccAnbarGardaniSatr As Integer, ByVal ccAnbarGardanySatrGhabl As Integer, ByVal ccAnbarGardanySatrGhabl2 As Integer, ByVal ccAnbar As Integer)
        Dim cn As New SqlConnection
        Dim cm As New SqlCommand
        Dim strSQL As String = ""

        Try
            cn = New SqlConnection(ConnectionString)
            cn.Open()

            strSQL = "WareHouse.spAnbarGardani_InsertSatr2_KalahayeMoghayeratDar2ShomaresheGhabl "

            cm = New SqlCommand(strSQL, cn)
            cm.CommandType = CommandType.StoredProcedure
            cm.Parameters.Clear()

            cm.Parameters.AddWithValue("ccAnbarGardanySatr", ccAnbarGardanySatr)
            cm.Parameters.AddWithValue("ccAnbarGardanySatrGhabl", ccAnbarGardanySatrGhabl)
            cm.Parameters.AddWithValue("ccAnbarGardanySatrGhabl2", ccAnbarGardanySatrGhabl2)
            cm.Parameters.AddWithValue("ccAnbar", ccAnbar)
            cm.Parameters.AddWithValue("TarikhAvaleDoreh", TarikhAvalDoreh)
            cm.Parameters.AddWithValue("AzTarikh", mskAzTarikhSatr.Text)
            cm.Parameters.AddWithValue("TaTarikh", mskTaTarikhSatr.Text)
            cm.Parameters.AddWithValue("UserName", UserName)
            cm.Parameters.AddWithValue("Tarikh", TarikhEmrooz)
            cm.Parameters.AddWithValue("Saat", Format(TimeOfDay, "HH:mm:ss"))

            cm.CommandTimeout = 999999
            cm.ExecuteNonQuery()

            cn.Close()
            cm = Nothing : cn = Nothing

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> InsertSatr2_KalahayeMoghayeratDar2ShomaresheGhabl")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> InsertSatr2_KalahayeMoghayeratDar2ShomaresheGhabl")
        End Try
    End Sub
    Private Sub UpdateSatr_AfterSave(ByVal ccAnbarGardaniSatr As Integer)
        Dim cn As New SqlConnection
        Dim cm As New SqlCommand
        Dim strSQL As String = ""

        Try
            cn = New SqlConnection(ConnectionString)
            cn.Open()

            strSQL = "WareHouse.spAnbarGardani_UpdateSatr_UpDated "

            cm = New SqlCommand(strSQL, cn)
            cm.CommandType = CommandType.StoredProcedure
            cm.Parameters.Clear()

            cm.Parameters.AddWithValue("ccAnbarGardanySatr", ccAnbarGardanySatr)

            cm.CommandTimeout = 999999
            cm.ExecuteNonQuery()

            cn.Close()
            cm = Nothing : cn = Nothing

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> UpdateSatr_AfterSave")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> UpdateSatr_AfterSave")
        End Try
    End Sub
    Private Sub UpdateSatr2(ByVal AnbarGardanySatr As Integer, ByVal AnbarGardanySatr2 As Integer, ByVal TedadAnbar As Double)
        Dim cn As New SqlConnection
        Dim cm As New SqlCommand
        Dim strSQL As String

        Try
            cn = New SqlConnection(ConnectionString)
            cn.Open()

            strSQL = "WareHouse.spAnbarGardany_UpdateSatr2"

            cm = New SqlCommand(strSQL, cn)
            cm.CommandType = CommandType.StoredProcedure
            cm.Parameters.Clear()

            cm.Parameters.AddWithValue("ccAnbarGardanySatr2", AnbarGardanySatr2)
            cm.Parameters.AddWithValue("ccAnbarGardanySatr", AnbarGardanySatr)
            cm.Parameters.AddWithValue("TedadAnbar", TedadAnbar)

            cm.CommandTimeout = 999999
            cm.ExecuteNonQuery()

            cn.Close()
            cm = Nothing : cn = Nothing

            btnSaveSatr2.Enabled = False

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> UpdateSatr2")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> UpdateSatr2")
        End Try
    End Sub
    Private Sub EzafeBeAnbar(ByVal ccAnbarGardanyTitrForEzafe As Integer, ByVal ccAnbarForEzafe As Integer)
        Dim cn As New SqlConnection
        Dim cm As New SqlCommand
        Dim strSQL As String = ""
        Dim ccEzafe As Integer = 0
        Dim ccAkharinAnbarGardanySatr = 0

        Try
            cn = New SqlConnection(ConnectionString)
            cn.Open()

            strSQL = "WareHouse.spAnbarGardany_Kasr_va_EzafeAnbar_InsertTitr "

            cm = New SqlCommand(strSQL, cn)
            cm.CommandType = CommandType.StoredProcedure
            cm.Parameters.Clear()

            cm.Parameters.AddWithValue("CodeMahal", CodeMahalFaal)
            cm.Parameters.AddWithValue("CodeDoreh", CodeDoreh)
            cm.Parameters.AddWithValue("ShomarehForm", objTools.ConvertNulls(objTools.DMax("ShomarehForm", "tblAN_KasrEzafeh", "CodeMahal = " & CodeMahalFaal & " AND CodeDoreh = " & CodeDoreh), 0) + 1)
            cm.Parameters.AddWithValue("TarikhForm", TarikhEmrooz)
            cm.Parameters.AddWithValue("NoeForm", 2)
            cm.Parameters.AddWithValue("ccAnbar", ccAnbarForEzafe)
            cm.Parameters.AddWithValue("Sharh", "")
            cm.Parameters.AddWithValue("UserName", UserName)
            cm.Parameters.AddWithValue("Tarikh", TarikhEmrooz)
            cm.Parameters.AddWithValue("Saat", Format(TimeOfDay, "HH:mm:ss"))
            cm.Parameters.AddWithValue("ccKasrEzafeh", ccEzafe)
            cm.Parameters("ccKasrEzafeh").Direction = ParameterDirection.Output

            cm.CommandTimeout = 999999
            cm.ExecuteNonQuery()

            ccEzafe = cm.Parameters("ccKasrEzafeh").Value

            ccAkharinAnbarGardanySatr = objTools.DLookup("ccAnbarGardanySatr", "tblAN_AnbarGardanySatr", "ccAnbarGardanyTitr = " & ccAnbarGardanyTitrForEzafe & " ORDER BY Shomaresh DESC")

            cn.Close()
            cm = Nothing : cn = Nothing

            EzafeBeAnbar_Satr(ccEzafe, ccAkharinAnbarGardanySatr)

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> EzafeBeAnbar")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> EzafeBeAnbar")
        End Try
    End Sub
    Private Sub EzafeBeAnbar_Satr(ByVal ccEzafe As Integer, ByVal ccAkharinAnbarGardanySatr As Integer)
        Dim cn As New SqlConnection
        Dim cm As New SqlCommand
        Dim strSQL As String = ""

        Try
            cn = New SqlConnection(ConnectionString)
            cn.Open()

            strSQL = "WareHouse.spAnbarGardany_Kasr_va_EzafeAnbar_InsertSatrForEzafe "

            cm = New SqlCommand(strSQL, cn)
            cm.CommandType = CommandType.StoredProcedure
            cm.Parameters.Clear()

            cm.Parameters.AddWithValue("ccKasrEzafeh", ccEzafe)
            cm.Parameters.AddWithValue("ccAnbarGardanySatr", ccAkharinAnbarGardanySatr)
            cm.Parameters.AddWithValue("Tarikh", TarikhEmrooz)

            cm.CommandTimeout = 999999
            cm.ExecuteNonQuery()

            cn.Close()
            cm = Nothing : cn = Nothing

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> EzafeBeAnbar_Satr")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> EzafeBeAnbar_Satr")
        End Try
    End Sub
    Private Sub KasrAzAnbar(ByVal ccAnbarGardanyTitrForKasr As Integer, ByVal ccAnbarForKasr As Integer)
        Dim cn As New SqlConnection
        Dim cm As New SqlCommand
        Dim strSQL As String = ""
        Dim ccKasr As Integer = 0
        Dim ccAkharinAnbarGardanySatr = 0

        Try
            cn = New SqlConnection(ConnectionString)
            cn.Open()

            strSQL = "WareHouse.spAnbarGardany_Kasr_va_EzafeAnbar_InsertTitr "

            cm = New SqlCommand(strSQL, cn)
            cm.CommandType = CommandType.StoredProcedure
            cm.Parameters.Clear()

            cm.Parameters.AddWithValue("CodeMahal", CodeMahalFaal)
            cm.Parameters.AddWithValue("CodeDoreh", CodeDoreh)
            cm.Parameters.AddWithValue("ShomarehForm", objTools.ConvertNulls(objTools.DMax("ShomarehForm", "tblAN_KasrEzafeh", "CodeMahal = " & CodeMahalFaal & " AND CodeDoreh = " & CodeDoreh), 0) + 1)
            cm.Parameters.AddWithValue("TarikhForm", TarikhEmrooz)
            cm.Parameters.AddWithValue("NoeForm", 1)
            cm.Parameters.AddWithValue("ccAnbar", ccAnbarForKasr)
            cm.Parameters.AddWithValue("Sharh", "")
            cm.Parameters.AddWithValue("UserName", UserName)
            cm.Parameters.AddWithValue("Tarikh", TarikhEmrooz)
            cm.Parameters.AddWithValue("Saat", Format(TimeOfDay, "HH:mm:ss"))
            cm.Parameters.AddWithValue("ccKasrEzafeh", ccKasr)
            cm.Parameters("ccKasrEzafeh").Direction = ParameterDirection.Output

            cm.CommandTimeout = 999999
            cm.ExecuteNonQuery()

            ccKasr = cm.Parameters("ccKasrEzafeh").Value

            ccAkharinAnbarGardanySatr = objTools.DLookup("ccAnbarGardanySatr", "tblAN_AnbarGardanySatr", "ccAnbarGardanyTitr = " & ccAnbarGardanyTitrForKasr & " ORDER BY Shomaresh DESC")

            cn.Close()
            cm = Nothing : cn = Nothing

            KasrAzAnbar_Satr(ccKasr, ccAkharinAnbarGardanySatr)

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> KasrAzAnbar")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> KasrAzAnbar")
        End Try
    End Sub
    Private Sub KasrAzAnbar_Satr(ByVal ccKasr As Integer, ByVal ccAkharinAnbarGardanySatr As Integer)
        Dim cn As New SqlConnection
        Dim cm As New SqlCommand
        Dim strSQL As String = ""

        Try
            cn = New SqlConnection(ConnectionString)
            cn.Open()

            strSQL = "WareHouse.spAnbarGardany_Kasr_va_EzafeAnbar_InsertSatrForKasr "

            cm = New SqlCommand(strSQL, cn)
            cm.CommandType = CommandType.StoredProcedure
            cm.Parameters.Clear()

            cm.Parameters.AddWithValue("ccKasrEzafeh", ccKasr)
            cm.Parameters.AddWithValue("ccAnbarGardanySatr", ccAkharinAnbarGardanySatr)
            cm.Parameters.AddWithValue("Tarikh", TarikhEmrooz)

            cm.CommandTimeout = 999999
            cm.ExecuteNonQuery()

            cn.Close()
            cm = Nothing : cn = Nothing

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> KasrAzAnbar_Satr")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> KasrAzAnbar_Satr")
        End Try
    End Sub
#End Region
#Region "From Buttons"
    Private Sub btnNewTitr_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNewTitr.Click
        If Not objSec.HaveUserPermission(SN, UD_Dll.Security.ePermissions.Add) Then Exit Sub
        Try

            Mode = UD_Dll.Enums.GL_ModeForms.AddNewRecord
            ClearForm()

            SetFormData()
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> btnNewTitr_Click")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> btnNewTitr_Click")
        End Try
    End Sub
    Private Sub btnSaveTitr_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveTitr.Click

        Try

            Select Case Mode
                Case UD_Dll.Enums.GL_ModeForms.AddNewRecord
                    If Not objSec.HaveUserPermission(SN, UD_Dll.Security.ePermissions.Add) Then Exit Sub
                    FlagIsValid = 0
                    AddNewRecord()
                    If FlagIsValid = 0 Then
                        Exit Sub
                    End If
                Case UD_Dll.Enums.GL_ModeForms.UpdateRecord
                    If Not objSec.HaveUserPermission(SN, UD_Dll.Security.ePermissions.Update) Then Exit Sub
                    If objTools.DLookup("Vazeiat", "tblAN_AnbarGardany", "ccAnbarGardanyTitr = " & Val(GridEXTitr.CurrentRow.Cells("ccAnbarGardanyTitr").Text.Replace(",", ""))) = True Then
                        MsgBox("این رکورد تایید شده است، امکان ویرایش آن را ندارید .", MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " پیام")
                        Exit Sub
                    End If
                    tPos = cmTitr.Position
                    FlagIsValid = 0
                    UpdateRecord()
                    If FlagIsValid = 0 Then
                        Exit Sub
                    End If
            End Select

            Mode = UD_Dll.Enums.GL_ModeForms.None

            Search()

            SetFormData()
            btnNewTitr.Focus()

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> btnSaveTitr_Click")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> btnSaveTitr_Click")
        End Try
    End Sub
    Private Sub btnRemoveTitr_Click(sender As Object, e As EventArgs) Handles btnRemoveTitr.Click
        Try
            If GridEXTitr.CurrentRow.RowType = Janus.Windows.GridEX.RowType.FilterRow Then
                Exit Sub
            End If

            If dvTitr.Count = 0 Then
                Exit Sub
            End If

            If objTools.DLookup("Vazeiat", "tblAN_AnbarGardany", "ccAnbarGardanyTitr = " & Val(GridEXTitr.CurrentRow.Cells("ccAnbarGardanyTitr").Text.Replace(",", ""))) = True Then
                MsgBox("انبارگردانی تایید شده است، امکان حـــذف آن را ندارید", MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " پیام")
                Exit Sub
            End If

            If MsgBox("آیا رکورد انتخاب شده حـــذف گـــردد ؟ ", MsgBoxStyle.Question + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.YesNo, "") = MsgBoxResult.Yes Then
                objTools.DUpdate("Faal", "tblAN_Anbar", "1", "CodeAnbar = " & Val(GridEXTitr.CurrentRow.Cells("ccAnbar").Text.Replace(",", "")))
                objTools.DDelete("tblAN_AnbarGardany", "ccAnbarGardanyTitr = " & Val(GridEXTitr.CurrentRow.Cells("ccAnbarGardanyTitr").Text.Replace(",", "")))
                Update_VazeiatAnbar(Val(GridEXTitr.CurrentRow.Cells("ccAnbar").Text.Replace(",", "")), 1)
                Search()

            End If
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> btnRemoveTitr_Click")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> btnRemoveTitr_Click")
        End Try
    End Sub
    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Search()
    End Sub
    Private Sub btnCancelTitr_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelTitr.Click
        Mode = UD_Dll.Enums.GL_ModeForms.None
        SetFormData()
        ClearForm()
    End Sub
    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub
    Private Sub btnNext_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNext.Click
        If Flag = 0 Then
            If GridEXTitr.GetCheckedRows().Length > 1 Then
                MsgBox("شما برای رفتن به مرحله بعد، تنها می توانید یک رکورد را انتخاب نمایید .", MsgBoxStyle.OkOnly + MsgBoxStyle.Information + MsgBoxStyle.MsgBoxRtlReading, "خطا")
                Exit Sub
            End If

            If GridEXTitr.GetCheckedRows().Length = 0 Then
                MsgBox("شما برای رفتن به مرحله بعد، حتماً باید یک رکورد را انتخاب نمایید .", MsgBoxStyle.OkOnly + MsgBoxStyle.Information + MsgBoxStyle.MsgBoxRtlReading, "خطا")
                Exit Sub
            End If

            For i As Integer = 0 To GridEXTitr.GetCheckedRows().Length - 1
                If GridEXTitr.GetCheckedRows(i).CheckState = Janus.Windows.GridEX.RowCheckState.Checked Then
                    If objTools.DLookup("Vazeiat", "tblAN_AnbarGardany", "ccAnbarGardanyTitr = " & Val(GridEXTitr.GetCheckedRows(i).Cells("ccAnbarGardanyTitr").Text.Replace(",", ""))) = True Then
                        MsgBox("انبارگردانی تایید شده است، امکان ویرایش ندارید", MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " پیام")
                        Exit Sub
                    End If
                    ccAnbarGardanyTitr = Val(GridEXTitr.GetCheckedRows(i).Cells("ccAnbarGardanyTitr").Text.Replace(",", ""))
                    ccanbar = GridEXTitr.GetCheckedRows(i).Cells("ccAnbar").Value
                    TarikhAvalDoreh = GridEXTitr.GetCheckedRows(i).Cells("CodeDoreh").Text & "0101"
                    AzTarikh = GridEXTitr.GetCheckedRows(i).Cells("TarikhShoro").Text
                    TaTarikh = GridEXTitr.GetCheckedRows(i).Cells("TarikhPayan").Text
                End If
            Next

            Flag = 1
            btnBack.Enabled = True
            btnTaeed.Visible = False
            SearchSatr()
        ElseIf Flag = 1 Then
            If GridEXSatr.GetCheckedRows().Length > 1 Then
                MsgBox("شما برای رفتن به مرحله بعد، تنها می توانید یک رکورد را انتخاب نمایید .", MsgBoxStyle.OkOnly + MsgBoxStyle.Information + MsgBoxStyle.MsgBoxRtlReading, "خطا")
                Exit Sub
            End If

            If GridEXSatr.GetCheckedRows().Length = 0 Then
                MsgBox("شما برای رفتن به مرحله بعد، حتماً باید یک رکورد را انتخاب نمایید .", MsgBoxStyle.OkOnly + MsgBoxStyle.Information + MsgBoxStyle.MsgBoxRtlReading, "خطا")
                Exit Sub
            End If

            For i As Integer = 0 To GridEXSatr.GetCheckedRows().Length - 1
                If GridEXSatr.GetCheckedRows(i).CheckState = Janus.Windows.GridEX.RowCheckState.Checked Then
                    ccAnbarGardanySatr = Val(GridEXSatr.GetCheckedRows(i).Cells("ccAnbarGardanySatr").Text.Replace(",", ""))
                End If
            Next

            Flag = 2
            btnNext.Enabled = False
            btnSaveSatr2.Enabled = True
            SearchSatr2()

            GridEXSatr2.CheckAllRecords()
            GridEXSatr2.CheckAllRecords()

            If objTools.DLookup("Updated", "tblAN_AnbarGardanySatr", "ccAnbarGardanySatr = " & ccAnbarGardanySatr) = False Then
                btnSaveSatr2.Enabled = True
            Else
                btnSaveSatr2.Enabled = False
            End If
        End If
        SetGroupBox()
    End Sub
    Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
        If Flag = 2 Then
            Flag = 1
            btnNext.Enabled = True
            If GridEXSatr.GetCheckedRows.Length > 0 Then
                For i As Integer = 0 To GridEXSatr.GetCheckedRows.Length - 1
                    If GridEXSatr.GetCheckedRows(i).CheckState = Janus.Windows.GridEX.RowCheckState.Checked Then
                        GridEXSatr.GetCheckedRows(i).CheckState = Janus.Windows.GridEX.RowCheckState.Unchecked
                    End If
                Next
            End If
            ccAnbarGardanySatr = 0
        ElseIf Flag = 1 Then
            Flag = 0
            btnBack.Enabled = False
            btnTaeed.Visible = True
            If GridEXTitr.GetCheckedRows.Length > 0 Then
                For i As Integer = 0 To GridEXTitr.GetCheckedRows.Length - 1
                    If GridEXTitr.GetCheckedRows(i).CheckState = Janus.Windows.GridEX.RowCheckState.Checked Then
                        GridEXTitr.GetCheckedRows(i).CheckState = Janus.Windows.GridEX.RowCheckState.Unchecked
                    End If
                Next
            End If
            ccAnbarGardanyTitr = 0
            ccanbar = 0
            TarikhAvalDoreh = ""
            AzTarikh = ""
            TaTarikh = ""
        End If
        SetGroupBox()
    End Sub
    Private Sub btnNewSatr_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNewSatr.Click
        If Not objSec.HaveUserPermission(SN, UD_Dll.Security.ePermissions.Add) Then Exit Sub
        Try
            lblShomaresh.Text = objTools.DCount("ccAnbarGardanySatr", "tblAN_AnbarGardanySatr", "ccAnbarGardanyTitr = " & ccAnbarGardanyTitr) + 1
            Mode = UD_Dll.Enums.GL_ModeForms.AddNewRow
            ClearForm()

            SetFormData()
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> btnNewSatr_Click")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> btnNewSatr_Click")
        End Try
    End Sub
    Private Sub btnSaveSatr_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveSatr.Click
        Try
            Select Case Mode
                Case UD_Dll.Enums.GL_ModeForms.AddNewRow
                    If Not objSec.HaveUserPermission(SN, UD_Dll.Security.ePermissions.Add) Then Exit Sub
                    tPos = cmTitr.Position

                    If Val(lblShomaresh.Text) <> 1 Then
                        If objTools.DCount("ccAnbarGardanySatr", "tblAN_AnbarGardanySatr", "ccAnbarGardanyTitr = " & ccAnbarGardanyTitr & " AND Updated = 0") <> 0 Then
                            MsgBox("برای این انبارگردانی، شمارش تایید نشده وجود دارد . ابتدا وضعیت آن را مشخص نمایید .", MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, "خطا")
                            Exit Sub
                        End If
                    End If

                    ccAnbarGardanySatr = 0
                    AddNewRow()
                    If ccAnbarGardanySatr = 0 Then
                        Exit Sub
                    End If
                    AddNewSatr2(cmbNoeAnbargardani.SelectedIndex)
                Case UD_Dll.Enums.GL_ModeForms.UpdateRow
                    If Not objSec.HaveUserPermission(SN, UD_Dll.Security.ePermissions.Update) Then Exit Sub
                    If objTools.DLookup("Vazeiat", "tblAN_AnbarGardany", "ccAnbarGardanyTitr = " & Val(GridEXTitr.CurrentRow.Cells("ccAnbarGardanyTitr").Text.Replace(",", ""))) = True Then
                        MsgBox("این رکورد تایید شده است، امکان ویرایش وجود ندارد.", MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " پیام")
                        Exit Sub
                    End If
                    tPos = cmTitr.Position
                    FlagIsValid = 0
                    UpdateRow()
                    If FlagIsValid = 0 Then
                        Exit Sub
                    End If
            End Select

            SearchSatr()
            cmTitr.Position = tPos

            SetFormData()
            btnNewTitr.Focus()

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> btnSaveSatr_Click_1")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> btnSaveSatr_Click_1")
        End Try
    End Sub
    Private Sub btnCancelSatr_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelSatr.Click
        Mode = UD_Dll.Enums.GL_ModeForms.None
        SetFormData()
        ClearForm()
        SearchSatr()
    End Sub
    Private Sub btnTaeed_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTaeed.Click
        If GridEXTitr.GetCheckedRows().Length = 0 Then
            MsgBox("جهت تایید باید حداقل یک انبارگردانی انتخاب شود .", MsgBoxStyle.OkOnly + MsgBoxStyle.Information + MsgBoxStyle.MsgBoxRtlReading, "خطا")
            Exit Sub
        End If

        If GridEXTitr.GetCheckedRows().Length > 1 Then
            MsgBox("در هر مرتبه تنها یک انبارگردانی قابل تایید است .", MsgBoxStyle.OkOnly + MsgBoxStyle.Information + MsgBoxStyle.MsgBoxRtlReading, "خطا")
            Exit Sub
        End If

        Try

            For i As Integer = 0 To GridEXTitr.GetCheckedRows().Length - 1
                If objTools.DLookup("Vazeiat", "tblAN_AnbarGardany", "ccAnbarGardanyTitr = " & Val(GridEXTitr.GetCheckedRows(i).Cells("ccAnbarGardanyTitr").Text.Replace(",", ""))) = True Then
                    MsgBox("انبارگردانی تایید شده است، امکان تایید مجدد وجود ندارید", MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " پیام")
                    Exit Sub
                End If

                If GridEXTitr.GetCheckedRows(i).CheckState = Janus.Windows.GridEX.RowCheckState.Checked Then
                    If objTools.DCount("ccAnbarGardanySatr", "tblAN_AnbarGardanySatr", "ccAnbarGardanyTitr = " & Val(GridEXTitr.GetCheckedRows(i).Cells("ccAnbarGardanyTitr").Text.Replace(",", ""))) = 0 Then
                        MsgBox("برای این انبارگـردانی هیچ شمارشی ثبت نشده است  .", MsgBoxStyle.OkOnly + MsgBoxStyle.Information + MsgBoxStyle.MsgBoxRtlReading, "خطا")
                        Exit Sub
                    End If

                    If objTools.DLookup("TOP 1 Updated", "tblAN_AnbarGardanySatr", "ccAnbarGardanyTitr = " & Val(GridEXTitr.GetCheckedRows(i).Cells("ccAnbarGardanyTitr").Text.Replace(",", "")) & " ORDER BY Shomaresh DESC") = 0 Then
                        MsgBox("آخرین شمارش این انبارگــردانی تاییــد نشده است . ابتدا باید آنرا تایید نمایید .", MsgBoxStyle.OkOnly + MsgBoxStyle.Information + MsgBoxStyle.MsgBoxRtlReading, "خطا")
                        Exit Sub
                    End If

                    If MsgBox("آیا مایلید انبارگردانی تایید گــردد ؟", MsgBoxStyle.Question + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.YesNo, "تایید") = MsgBoxResult.No Then
                        Exit Sub
                    End If

                    EzafeBeAnbar(Val(GridEXTitr.GetCheckedRows(i).Cells("ccAnbarGardanyTitr").Text.Replace(",", "")), Val(GridEXTitr.GetCheckedRows(i).Cells("ccAnbar").Text.Replace(",", "")))
                    KasrAzAnbar(Val(GridEXTitr.GetCheckedRows(i).Cells("ccAnbarGardanyTitr").Text.Replace(",", "")), Val(GridEXTitr.GetCheckedRows(i).Cells("ccAnbar").Text.Replace(",", "")))

                    Update_VazeiatAnbar(Val(GridEXTitr.GetCheckedRows(i).Cells("ccAnbar").Text.Replace(",", "")), 1)


                    objTools.DUpdate("Vazeiat", "tblAN_AnbarGardany", "1", "ccAnbarGardanyTitr = " & Val(GridEXTitr.GetCheckedRows(i).Cells("ccAnbarGardanyTitr").Text.Replace(",", "")))
                    Search()
                End If
            Next

            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

            Flag = 0
            btnBack.Enabled = False
            btnTaeed.Visible = True
            If GridEXTitr.GetCheckedRows.Length > 0 Then
                For i As Integer = 0 To GridEXTitr.GetCheckedRows.Length - 1
                    If GridEXTitr.GetCheckedRows(i).CheckState = Janus.Windows.GridEX.RowCheckState.Checked Then
                        GridEXTitr.GetCheckedRows(i).CheckState = Janus.Windows.GridEX.RowCheckState.Unchecked
                    End If
                Next
            End If
            ccAnbarGardanyTitr = 0
            ccanbar = 0
            TarikhAvalDoreh = ""
            AzTarikh = ""
            TaTarikh = ""

            SetGroupBox()

            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> btnTaeed_Click")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> btnTaeed_Click")
        End Try

    End Sub
    Private Sub btnSaveSatr2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveSatr2.Click

        If MsgBox("آیا ورودی ها ذخیره گــردد ؟", MsgBoxStyle.Question + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.YesNo, "ذخیره سازی") = MsgBoxResult.No Then
            Exit Sub
        End If

        For i As Integer = 0 To GridEXSatr2.RowCount - 1
            UpdateSatr2(ccAnbarGardanySatr, Val(GridEXSatr2.GetRow(i).Cells("ccAnbarGardanySatr2").Text.Replace(",", "")), Val(GridEXSatr2.GetRow(i).Cells("TedadAnbar").Text.Replace(",", "")))
        Next

        UpdateSatr_AfterSave(ccAnbarGardanySatr)
        SearchSatr2()

        MsgBox("ذخیره سازی با موفقیت انجام شد .", MsgBoxStyle.Information + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " پیام")

    End Sub
#End Region
    Private Sub btnDeleteSatr_Click(sender As Object, e As EventArgs) Handles btnDeleteSatr.Click
        Try
            If GridEXSatr.CurrentRow.RowType = Janus.Windows.GridEX.RowType.FilterRow Then
                Exit Sub
            End If

            If dvSatr.Count = 0 Then
                Exit Sub
            End If

            If objTools.DLookup("Vazeiat", "tblAN_AnbarGardany", "ccAnbarGardanyTitr = " & Val(GridEXTitr.CurrentRow.Cells("ccAnbarGardanyTitr").Text.Replace(",", ""))) = 1 Then
                MsgBox("انبارگردانی تایید شده است، امکان حـــذف آن را ندارید", MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " پیام")
                Exit Sub
            End If

            If MsgBox("آیا رکورد انتخاب شده حـــذف گـــردد ؟ ", MsgBoxStyle.Question + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.YesNo, "") = MsgBoxResult.Yes Then
                objTools.DDelete("tblAN_AnbarGardanySatr", "ccAnbarGardanySatr = " & Val(GridEXSatr.CurrentRow.Cells("ccAnbarGardanySatr").Text.Replace(",", "")))
                SearchSatr()

            End If
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> btnRemoveTitr_Click")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> btnRemoveTitr_Click")
        End Try
    End Sub
    Private Sub btnSaveMovaghat_Click(sender As Object, e As EventArgs) Handles btnSaveMovaghat.Click
        If MsgBox("آیا ورودی ها به صورت موقت ذخیره گــردد ؟", MsgBoxStyle.Question + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.YesNo, "ذخیره سازی") = MsgBoxResult.No Then
            Exit Sub
        End If

        For i As Integer = 0 To GridEXSatr2.RowCount - 1
            If Val(GridEXSatr2.GetRow(i).Cells("TedadAnbar").Text.Replace(",", "")) <> 0 Then
                UpdateSatr2_Movaghat(ccAnbarGardanySatr, Val(GridEXSatr2.GetRow(i).Cells("ccAnbarGardanySatr2").Text.Replace(",", "")), Val(GridEXSatr2.GetRow(i).Cells("TedadAnbar").Text.Replace(",", "")))
            End If
        Next

        SearchSatr2()

        MsgBox("ذخیره موقت اطلاعات با موفقیت انجام شد .", MsgBoxStyle.Information + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " پیام")

    End Sub
    Private Sub UpdateSatr2_Movaghat(ByVal AnbarGardanySatr As Integer, ByVal AnbarGardanySatr2 As Integer, ByVal TedadShomareshMovaghat As Double)
        Dim cn As New SqlConnection
        Dim cm As New SqlCommand
        Dim strSQL As String

        Try
            cn = New SqlConnection(ConnectionString)
            cn.Open()

            strSQL = "WareHouse.spAnbarGardany_UpdateSatr2_TedadShomareshMovaghat"

            cm = New SqlCommand(strSQL, cn)
            cm.CommandType = CommandType.StoredProcedure
            cm.Parameters.Clear()

            cm.Parameters.AddWithValue("ccAnbarGardanySatr2", AnbarGardanySatr2)
            cm.Parameters.AddWithValue("ccAnbarGardanySatr", AnbarGardanySatr)
            cm.Parameters.AddWithValue("TedadShomareshMovaghat", TedadShomareshMovaghat)

            cm.CommandTimeout = 999999
            cm.ExecuteNonQuery()

            cn.Close()
            cm = Nothing : cn = Nothing

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> UpdateSatr2")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> UpdateSatr2")
        End Try
    End Sub

   
    Private Sub Print()

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


              strsql = "WareHouse.spAnbarGardani_SearchSatr2"

            Windows.Forms.Cursor.Current = Cursors.WaitCursor
            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()
            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()


            p = New SqlParameter("ccAnbarGardanySatr", SqlDbType.Int)
            p.Value = ccAnbarGardanySatr
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

            If flgPrint = False Then
                rpt.Load(rptPath & "\rptAN_GozareshAnbargardani_Satr2.rpt")
            ElseIf flgPrint = True Then
                rpt.Load(rptPath & "\rptAN_GozareshAnbargardani_Satr2new.rpt")
            End If

            rpttables = rpt.Database.Tables
            rpttables.Item(0).SetDataSource(dsForm.Tables("tblGozaresh"))

            rptformula = rpt.DataDefinition.FormulaFields
            With rptformula
                If flgPrint = True Then
                    .Item("Group_Sanad").Text = "{mydata.ccKala}"
                    .Item("NameKala").Text = "{mydata.NameKala}"
                    .Item("CodeKala").Text = "{mydata.CodeKala}"
                    .Item("NameAnbar").Text = "{mydata.NameAnbar}"
                    .Item("NameAnbar").Text = "{mydata.NameAnbar}"
                    .Item("Title2").Text = "'" & NameSherkat & "'"
                    '.Item("Title3").Text = "'" & NameMahalFaal & "'"
                    .Item("Vahed").Text = "{mydata.vahed}"
                    .Item("GTIN").Text = "{mydata.GTIN}"
                    .Item("TedadDarBasteh").Text = "{mydata.TedadDarBasteh}"
                    .Item("TedadDarKarton").Text = "{mydata.TedadDarKarton}"
                    .Item("ShomarehBach").Text = "{mydata.ShomarehBach}"


                Else
                    .Item("NameKala").Text = "{mydata.NameKala}"
                    .Item("CodeKala").Text = "{mydata.CodeKala}"
                    .Item("Moghayerat").Text = "{mydata.Moghayerat}"
                    .Item("TedadAnbar").Text = "{mydata.TedadAnbar}"
                    .Item("TedadComputer").Text = "{mydata.TedadComputer}"
                    .Item("Title").Text = "'" & "گـزارش انبارگردانی" & "'"
                    .Item("Title2").Text = "'" & NameSherkat & "'"
                    .Item("Title3").Text = "'" & NameMahalFaal & "'"
                    .Item("KarbarGozaresh").Text = "'" & PersonelName & "'"
                    .Item("TarikhGozaresh").Text = "'" & objTarikh.SetDateSlash(TarikhEmrooz) & "'"
                    .Item("SaatGozaresh").Text = "'" & Format(TimeOfDay, "HH:mm:ss") & "'"
                    .Item("GTIN").Text = "{mydata.GTIN}"
                    .Item("TedadDarBasteh").Text = "{mydata.TedadDarBasteh}"
                    .Item("TedadDarKarton").Text = "{mydata.TedadDarKarton}"
                    .Item("ShomarehBach").Text = "{mydata.ShomarehBach}"

                End If
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

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            flgPrint = False
            If dvTitr.Count > 0 Then
                Me.TopMost = False

                Print()
                Me.TopMost = True
            End If
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->btnPrintM_Click")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->btnPrintM_Click")
        End Try
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles btnNewPrint.Click
        Try
            flgPrint = True
            If dvTitr.Count > 0 Then
                Me.TopMost = False

                Print()
                Me.TopMost = True
            End If
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->btnPrintM_Click")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->btnPrintM_Click")
        End Try
    End Sub
End Class
