Public Class frmFO_SabadGorohKala

#Region "Variable AND Constant Declration"
    ' Security 
    Const cntCodeSubSystem As Long = 100074
    Private SN As Integer

    Const TitrGridSize As Integer = 190
    Const SatrGridSize As Integer = 150
    Const TitrOrgSize As Integer = 220
    Const SatrOrgSize As Integer = 240
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
    Dim ccGorohKala As Integer
    Dim txtCaption As String
    Private WithEvents BS As New UD_Dll.PassString
#End Region
#Region "Form Event Code"
    Private Sub GridEXSatr_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GridEXSatr.DoubleClick
        Try
            If Mode = UD_Dll.Enums.GL_ModeForms.None Then
                Mode = UD_Dll.Enums.GL_ModeForms.UpdateRow
                SetFormSatrData(cmSatr.Position)
                SetFormObject()
            End If
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->dbgSatr_DoubleClick")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->dbgSatr_DoubleClick")
        End Try
    End Sub
    Private Sub frmFO_SabadKala_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles MyBase.Paint
        Try
            Me.TopMost = True
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->frmSanadHesabdary_Paint")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->frmSanadHesabdary_Paint")
        End Try
    End Sub
    Private Sub frmFO_SabadKala_Closed(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Closed
        ' "Receive" parameter is the caption of destination window
        Dim hwnd As Long = UD_Dll.Code.FindWindow(vbNullString, ObjCode.GetNameSherkat)
        If hwnd <> 0 Then
            BS.PostString(hwnd, &H400, 0, txtCaption)
        End If

        dsForm = Nothing
        dvForm = Nothing
    End Sub
    Private Sub GridEXTitr_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GridEXTitr.Click
        BoundCurrencyManagerTitr()
        SetGridStyle()
    End Sub
    Private Sub GridEXTitr_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GridEXTitr.DoubleClick
        Try
            Mode = UD_Dll.Enums.GL_ModeForms.UpdateRecord
            SetFormTitrData()
            SetFormObject()
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->dbgTitr_DoubleClick")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->dbgTitr_DoubleClick")
        End Try
    End Sub
    Private Sub frmFO_SabadKala_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            SetParameter()
            SN = objSec.GetSecurityNumber(cntCodeSubSystem, UserName)

            Mode = UD_Dll.Enums.GL_ModeForms.None
            flg = False
            LoadCombo()
            ClearForm()
            Search(False)
            flg = True
            SetFormObject()
            cmbGoroh1.SelectedIndex = -1

            objTools.ChangeKbd(UD_Dll.mdlUtility.KBD.Farsi)
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->frmSanadHesabdary_Load")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->frmSanadHesabdary_Load")
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
            CodeDoreh = "1391"
            txtCaption = "سبد گروه کالا"
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
#End Region
#Region "Global Form Code"
    Private Sub LoadCombo()
        Try
            Dim cnSQL As New SqlConnection
            Dim cmSQL As New SqlCommand
            Dim p As New SqlParameter
            Dim strSQL As String
            Dim daSQL As New SqlDataAdapter

            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()

            '---------------Load Combo Goroh 1--------------------

            strSQL = "Global.spGorohKala_1_LoadCombo "

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            If dsForm.Tables.Contains("tbl_Goroh11") Then
                dsForm.Tables("tbl_Goroh11").Clear()
            End If

            daSQL = New SqlDataAdapter(cmSQL)
            daSQL.Fill(dsForm, "tbl_Goroh11")

            '---------------Load Combo Goroh 2--------------------

            strSQL = "Global.spGorohKala_2_LoadCombo "

            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            p = New SqlParameter("CodeLink", SqlDbType.Int)
            p.Value = IIf(cmbGoroh1.SelectedIndex = -1, 0, cmbGoroh1.SelectedValue)
            cmSQL.Parameters.Add(p)

            If dsForm.Tables.Contains("tbl_Goroh12") Then
                dsForm.Tables("tbl_Goroh12").Clear()
            End If

            daSQL = New SqlDataAdapter(cmSQL)
            daSQL.Fill(dsForm, "tbl_Goroh12")

            '---------------Load Combo Goroh 3--------------------

            strSQL = "Global.spGorohKala_3_LoadCombo "

            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            p = New SqlParameter("CodeLink", SqlDbType.Int)
            p.Value = IIf(cmbGoroh2.SelectedIndex = -1, 0, cmbGoroh2.SelectedValue)
            cmSQL.Parameters.Add(p)

            If dsForm.Tables.Contains("tbl_Goroh13") Then
                dsForm.Tables("tbl_Goroh13").Clear()
            End If

            daSQL = New SqlDataAdapter(cmSQL)
            daSQL.Fill(dsForm, "tbl_Goroh13")

            '---------------Load Combo Goroh 4--------------------

            strSQL = "Global.spGorohKala_4_LoadCombo "

            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            p = New SqlParameter("CodeLink", SqlDbType.Int)
            p.Value = IIf(cmbGoroh3.SelectedIndex = -1, 0, cmbGoroh3.SelectedValue)
            cmSQL.Parameters.Add(p)

            If dsForm.Tables.Contains("tbl_Goroh14") Then
                dsForm.Tables("tbl_Goroh14").Clear()
            End If

            daSQL = New SqlDataAdapter(cmSQL)
            daSQL.Fill(dsForm, "tbl_Goroh14")

            '---------------Load Combo Goroh 5--------------------

            strSQL = "Global.spGorohKala_5_LoadCombo "

            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            p = New SqlParameter("CodeLink", SqlDbType.Int)
            p.Value = IIf(cmbGoroh4.SelectedIndex = -1, 0, cmbGoroh4.SelectedValue)
            cmSQL.Parameters.Add(p)

            If dsForm.Tables.Contains("tbl_Goroh15") Then
                dsForm.Tables("tbl_Goroh15").Clear()
            End If

            daSQL = New SqlDataAdapter(cmSQL)
            daSQL.Fill(dsForm, "tbl_Goroh15")

            '--------------------------------------------

            cmbGoroh1.DataSource = Nothing
            cmbGoroh1.Items.Clear()
            cmbGoroh1.DataSource = dsForm.Tables("tbl_Goroh11").DefaultView
            cmbGoroh1.DisplayMember = "Sharh"
            cmbGoroh1.ValueMember = "Code"

            daSQL = Nothing
            cmSQL = Nothing

            cnSQL.Close()


        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "Error IN DataBase ---->" & "LoadCombo")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, "Error IN ---->" & "LoadCombo")
        End Try
    End Sub
    Private Sub LoadComboGoroh2()
        Try
            Dim dvGoroh2 As New DataView(dsForm.Tables("tbl_Goroh12"), "CodeLink = " & IIf(IsNothing(cmbGoroh1.SelectedValue), -1, cmbGoroh1.SelectedValue), "", DataViewRowState.OriginalRows)
            cmbGoroh2.DataSource = Nothing
            cmbGoroh2.Items.Clear()
            cmbGoroh2.DataSource = dvGoroh2
            cmbGoroh2.DisplayMember = "Sharh"
            cmbGoroh2.ValueMember = "Code"
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "Error IN DataBase ---->" & "LoadComboOstan")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, "Error IN ---->" & "LoadComboOstan")
        End Try
    End Sub
    Private Sub LoadComboGoroh3()
        Try
            Dim dvGoroh3 As New DataView(dsForm.Tables("tbl_Goroh13"), "CodeLink = " & IIf(IsNothing(cmbGoroh2.SelectedValue), -1, cmbGoroh2.SelectedValue), "", DataViewRowState.OriginalRows)
            cmbGoroh3.DataSource = Nothing
            cmbGoroh3.Items.Clear()
            cmbGoroh3.DataSource = dvGoroh3
            cmbGoroh3.DisplayMember = "Sharh"
            cmbGoroh3.ValueMember = "Code"
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "Error IN DataBase ---->" & "LoadComboOstan")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, "Error IN ---->" & "LoadComboOstan")
        End Try
    End Sub
    Private Sub LoadComboGoroh4()
        Try
            Dim dvGoroh4 As New DataView(dsForm.Tables("tbl_Goroh14"), "CodeLink = " & IIf(IsNothing(cmbGoroh3.SelectedValue), -1, cmbGoroh3.SelectedValue), "", DataViewRowState.OriginalRows)
            cmbGoroh4.DataSource = Nothing
            cmbGoroh4.Items.Clear()
            cmbGoroh4.DataSource = dvGoroh4
            cmbGoroh4.DisplayMember = "Sharh"
            cmbGoroh4.ValueMember = "Code"
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "Error IN DataBase ---->" & "LoadComboOstan")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, "Error IN ---->" & "LoadComboOstan")
        End Try
    End Sub
    Private Sub LoadComboGoroh5()
        Try
            Dim dvGoroh5 As New DataView(dsForm.Tables("tbl_Goroh15"), "CodeLink = " & IIf(IsNothing(cmbGoroh4.SelectedValue), -1, cmbGoroh4.SelectedValue), "", DataViewRowState.OriginalRows)
            cmbGoroh5.DataSource = Nothing
            cmbGoroh5.Items.Clear()
            cmbGoroh5.DataSource = dvGoroh5
            cmbGoroh5.DisplayMember = "Sharh"
            cmbGoroh5.ValueMember = "Code"
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "Error IN DataBase ---->" & "LoadComboOstan")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, "Error IN ---->" & "LoadComboOstan")
        End Try
    End Sub
    Private Sub ClearForm()
        Try
            txtNameSabadGorohKala.Text = ""
            cmbGoroh1.SelectedIndex = -1
            cmbGoroh2.DataSource = Nothing
            cmbGoroh3.DataSource = Nothing
            cmbGoroh4.DataSource = Nothing
            cmbGoroh5.DataSource = Nothing
            mskTedadKala.Text = ""
            mskHadeAghalKaridDarFaktor.Text = ""

            chkFaal.Checked = False

            ErrPro.Dispose()

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->ClearForm")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->ClearForm")
        End Try

    End Sub
    Private Sub SetFormObject()
        Try
            SetTitrButton()
            SetSatrButton()
            If UserName.ToUpper = "ADMINISTRATOR" Then
                btnFullDelete.Visible = True
            End If
            Select Case Mode
                Case UD_Dll.Enums.GL_ModeForms.None
                    GridEXTitr.Height = TitrOrgSize
                    GridEXSatr.Height = SatrOrgSize
                    GridEXTitr.Enabled = True
                    GridEXSatr.Enabled = True
                Case UD_Dll.Enums.GL_ModeForms.AddNewRecord
                    GridEXTitr.Height = TitrGridSize
                    GridEXTitr.Enabled = False
                    GridEXSatr.Enabled = False
                    Me.txtNameSabadGorohKala.Focus()
                Case UD_Dll.Enums.GL_ModeForms.AddNewRow
                    GridEXSatr.Height = SatrGridSize
                    GridEXTitr.Enabled = False
                    GridEXSatr.Enabled = False
                Case UD_Dll.Enums.GL_ModeForms.UpdateRow
                    GridEXSatr.Height = SatrGridSize
                    GridEXTitr.Enabled = False
                    GridEXSatr.Enabled = False
                Case UD_Dll.Enums.GL_ModeForms.UpdateRecord
                    GridEXTitr.Height = TitrGridSize
                    GridEXTitr.Enabled = False
                    GridEXSatr.Enabled = False
                    Me.txtNameSabadGorohKala.Focus()
            End Select
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->SetFormObject")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->SetFormObject")
        End Try
    End Sub
    Private Sub SetTitrButton()
        Try
            Select Case Mode
                Case UD_Dll.Enums.GL_ModeForms.AddNewRecord
                    btnNewTitr.Visible = False
                    btnEditTitr.Visible = False
                    btnDeleteTitr.Visible = False
                    btnSaveTitr.Visible = True
                    btnCancelTitr.Visible = True
                Case UD_Dll.Enums.GL_ModeForms.UpdateRecord
                    btnNewTitr.Visible = False
                    btnEditTitr.Visible = False
                    btnDeleteTitr.Visible = False
                    btnSaveTitr.Visible = True
                    btnCancelTitr.Visible = True
                Case UD_Dll.Enums.GL_ModeForms.None
                    btnNewTitr.Visible = True
                    btnSaveTitr.Visible = False
                    btnCancelTitr.Visible = False
                    If dvTitr.Count > 0 Then
                        btnEditTitr.Visible = True
                        btnDeleteTitr.Visible = True
                    Else
                        btnEditTitr.Visible = False
                        btnDeleteTitr.Visible = False
                    End If
                Case Else
                    btnNewTitr.Visible = False
                    btnEditTitr.Visible = False
                    btnDeleteTitr.Visible = False
            End Select
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->SetTitrButton")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->SetTitrButton")
        End Try

    End Sub
    Private Sub SetSatrButton()
        Try
            Select Case Mode
                Case UD_Dll.Enums.GL_ModeForms.AddNewRow
                    btnNewRow.Visible = False
                    btnDeleteRow.Visible = False
                    btnEditRow.Visible = False
                    btnSaveRow.Visible = True
                    btnCancelRow.Visible = True
                Case UD_Dll.Enums.GL_ModeForms.UpdateRow
                    btnNewRow.Visible = False
                    btnDeleteRow.Visible = False
                    btnEditRow.Visible = False
                    btnSaveRow.Visible = True
                    btnCancelRow.Visible = True
                Case UD_Dll.Enums.GL_ModeForms.None
                    If dvTitr.Count > 0 Then
                        btnNewRow.Visible = True
                    Else
                        btnNewRow.Visible = False
                    End If
                    btnSaveRow.Visible = False
                    btnCancelRow.Visible = False
                    If Not dvSatr Is Nothing Then
                        If dvSatr.Count > 0 Then
                            btnDeleteRow.Visible = True
                            btnEditRow.Visible = True
                        End If
                    Else
                        btnDeleteRow.Visible = False
                        btnEditRow.Visible = False
                    End If
                Case Else
                    btnNewRow.Visible = False
                    btnEditRow.Visible = False
                    btnDeleteRow.Visible = False
            End Select
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->SetSatrButton")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->SetSatrButton")
        End Try
    End Sub
    Private Sub SetCombo(ByVal ccGoroh As Integer)
        Dim sG1, sG2, sG3, sG4 As Integer
        Dim noeGoroh As Integer = objTools.DLookup("CodeAsli", "tblGL_ShenasehOmomi", "Code = " & ccGoroh)

        Select Case noeGoroh
            Case 200
                cmbGoroh1.SelectedValue = ccGoroh
            Case 201
                sG1 = objTools.DLookup("CodeLink", "tblGL_ShenasehOmomi", "Code = " & ccGoroh)
                cmbGoroh1.SelectedValue = sG1
                cmbGoroh2.SelectedValue = ccGoroh
            Case 202
                sG2 = objTools.DLookup("CodeLink", "tblGL_ShenasehOmomi", "Code = " & ccGoroh)
                sG1 = objTools.DLookup("CodeLink", "tblGL_ShenasehOmomi", "Code = " & sG2)
                cmbGoroh1.SelectedValue = sG1
                cmbGoroh2.SelectedValue = sG2
                cmbGoroh3.SelectedValue = ccGoroh
            Case 203
                sG3 = objTools.DLookup("CodeLink", "tblGL_ShenasehOmomi", "Code = " & ccGoroh)
                sG2 = objTools.DLookup("CodeLink", "tblGL_ShenasehOmomi", "Code = " & sG3)
                sG1 = objTools.DLookup("CodeLink", "tblGL_ShenasehOmomi", "Code = " & sG2)
                cmbGoroh1.SelectedValue = sG1
                cmbGoroh2.SelectedValue = sG2
                cmbGoroh3.SelectedValue = sG3
                cmbGoroh4.SelectedValue = ccGoroh
            Case 204
                sG4 = objTools.DLookup("CodeLink", "tblGL_ShenasehOmomi", "Code = " & ccGoroh)
                sG3 = objTools.DLookup("CodeLink", "tblGL_ShenasehOmomi", "Code = " & sG4)
                sG2 = objTools.DLookup("CodeLink", "tblGL_ShenasehOmomi", "Code = " & sG3)
                sG1 = objTools.DLookup("CodeLink", "tblGL_ShenasehOmomi", "Code = " & sG2)
                cmbGoroh1.SelectedValue = sG1
                cmbGoroh2.SelectedValue = sG2
                cmbGoroh3.SelectedValue = sG3
                cmbGoroh4.SelectedValue = sG4
                cmbGoroh5.SelectedValue = ccGoroh
        End Select
    End Sub
    Private Sub Search(ByVal WithCriteria As Boolean)
        Try
            Dim StrSql As String

            StrSql = "Sales.spSabadGorohKala_SearchTitr "

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
            Dim p As New SqlParameter

            If dsForm.Tables.Contains("Sales_SabadGorohKala") Then
                dsForm.Tables.Remove("Sales_SabadGorohKala")
            End If

            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()

            cmSQL = New SqlCommand(strSql, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            daSQL = New SqlDataAdapter(cmSQL)
            daSQL.Fill(dsForm, "Sales_SabadGorohKala")

            dvTitr = New DataView(dsForm.Tables("Sales_SabadGorohKala"), "", "ccSabadGorohKala ASC", DataViewRowState.CurrentRows)
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
            SetTitrButton()

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
            Search(False)
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->cmTitr_ItemChanged")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->cmTitr_ItemChanged")
        End Try

    End Sub
    Private Sub cmTitr_PositionChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            SetFormTitrData()
            RefreshSatrData()
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->cmTitr_PositionChanged")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->cmTitr_PositionChanged")
        End Try

    End Sub
    Private Sub cmSatr_ItemChanged(ByVal sender As Object, ByVal e As ItemChangedEventArgs)
        Try
            SetSatrButton()
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->cmSatr_ItemChanged")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->cmSatr_ItemChanged")
        End Try

    End Sub
    Private Sub cmSatr_PositionChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            SetSatrButton()
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->cmSatr_PositionChanged")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->cmSatr_PositionChanged")
        End Try

    End Sub
    Private Sub SetFormTitrData()
        Try
            If dvTitr.Count = 0 Or cmTitr.Position = -1 Then
                Mode = UD_Dll.Enums.GL_ModeForms.None
                Exit Sub
            End If

            Dim drvTemp As DataRowView
            drvTemp = dvTitr(cmTitr.Position)
            txtNameSabadGorohKala.Text = drvTemp("NameSabadGorohKala")
            chkFaal.Checked = drvTemp("Faal")

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->SetFormTitrData")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->SetFormTitrData")
        End Try

    End Sub
    Private Sub SetFormSatrData(ByVal Pos As Integer, Optional ByVal strField As String = "")
        Try
            If dvSatr.Count = 0 Or cmTitr.Position = -1 Then
                Mode = UD_Dll.Enums.GL_ModeForms.None
                Exit Sub
            End If

            Dim drvTemp As DataRowView
            drvTemp = dvSatr(Pos)
            If Mode = UD_Dll.Enums.GL_ModeForms.UpdateRow Then
                drvTemp = dvSatr(cmSatr.Position)
                SetCombo(drvTemp("ccGorohKala"))
                mskTedadKala.Text = drvTemp("Tedad")
                mskHadeAghalKaridDarFaktor.Text = drvTemp("HadeAghlKharidDarFaktor")
            End If

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->SetFormSatrData")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->SetFormSatrData")
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
            Dim p As New SqlParameter

            If dsForm.Tables.Contains("Sales_SabadGorohKalaSatr") Then
                dsForm.Tables.Remove("Sales_SabadGorohKalaSatr")
            End If

            Strsql = "Sales.spSabadGorohKala_SearchSatr "

            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()

            cmSQL = New SqlCommand(Strsql, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            p = New SqlParameter("ccSabadGorohKala", SqlDbType.Int)
            p.Value = dvTitr(cmTitr.Position)("ccSabadGorohKala")
            cmSQL.Parameters.Add(p)

            daSQL = New SqlDataAdapter(cmSQL)
            daSQL.Fill(dsForm, "Sales_SabadGorohKalaSatr")

            dvSatr = New DataView(dsForm.Tables("Sales_SabadGorohKalaSatr"))

            dvSatr.AllowNew = False
            dvSatr.AllowDelete = False
            dvSatr.AllowEdit = True

            daSQL = Nothing
            GridEXSatr.DataSource = Nothing
            GridEXSatr.DataSource = dvSatr
            cnSQL.Close()
            BoundCurrencyManagerSatr()
            SetSatrButton()

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->RefreshSatrData")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->RefreshSatrData")
        End Try

    End Sub
    Private Sub SetGridStyle()
        Try
            'Titr
            '=========================================================================================='
            With GridEXTitr
                .DataSource = Nothing
                .DataSource = dsForm.Tables("Sales_SabadGorohKala").DefaultView
                .SetDataBinding(dsForm.Tables("Sales_SabadGorohKala").DefaultView, "")
                .RetrieveStructure()
            End With

            For i As Integer = 0 To GridEXTitr.CurrentTable.Columns.Count - 1
                GridEXTitr.CurrentTable.Columns.Item(i).Visible = False
            Next


            GridEXTitr.CurrentTable.Columns.Item("ccSabadGorohKala").Caption = "کــد سیستمی"
            GridEXTitr.CurrentTable.Columns.Item("ccSabadGorohKala").Visible = True
            GridEXTitr.CurrentTable.Columns.Item("ccSabadGorohKala").Width = 110
            GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXTitr.CurrentTable.Columns.Item("ccSabadGorohKala").Position = 0
            GridEXTitr.CurrentTable.Columns.Item("ccSabadGorohKala").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXTitr.CurrentTable.Columns.Item("NameSabadGorohKala").Caption = "نام سبــــد گــــروه کــالا"
            GridEXTitr.CurrentTable.Columns.Item("NameSabadGorohKala").Visible = True
            GridEXTitr.CurrentTable.Columns.Item("NameSabadGorohKala").Width = 400
            GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXTitr.CurrentTable.Columns.Item("NameSabadGorohKala").Position = 1
            GridEXTitr.CurrentTable.Columns.Item("NameSabadGorohKala").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXTitr.CurrentTable.Columns.Item("txtFaal").Caption = "وضعیت"
            GridEXTitr.CurrentTable.Columns.Item("txtFaal").Visible = True
            GridEXTitr.CurrentTable.Columns.Item("txtFaal").Width = 130
            GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXTitr.CurrentTable.Columns.Item("txtFaal").Position = 2
            GridEXTitr.CurrentTable.Columns.Item("txtFaal").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

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
            'Satr=========================================================================================='
            If dvTitr.Count = 0 Then
                Exit Try
            End If

            If dvSatr.Count = 0 Then
                Exit Try
            End If

            With GridEXSatr
                .DataSource = Nothing
                .DataSource = dsForm.Tables("Sales_SabadGorohKalaSatr").DefaultView
                .SetDataBinding(dsForm.Tables("Sales_SabadGorohKalaSatr").DefaultView, "")
                .RetrieveStructure()
            End With

            For i As Integer = 0 To GridEXSatr.CurrentTable.Columns.Count - 1
                GridEXSatr.CurrentTable.Columns.Item(i).Visible = False
            Next


            GridEXSatr.CurrentTable.Columns.Item("ccSabadGorohKalaSatr").Caption = "کد سیستمی"
            GridEXSatr.CurrentTable.Columns.Item("ccSabadGorohKalaSatr").Visible = True
            GridEXSatr.CurrentTable.Columns.Item("ccSabadGorohKalaSatr").Width = 100
            GridEXSatr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXSatr.CurrentTable.Columns.Item("ccSabadGorohKalaSatr").Position = 0
            GridEXSatr.CurrentTable.Columns.Item("ccSabadGorohKalaSatr").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXSatr.CurrentTable.Columns.Item("ccGorohKala").Caption = "کــد گروه کالا"
            GridEXSatr.CurrentTable.Columns.Item("ccGorohKala").Visible = True
            GridEXSatr.CurrentTable.Columns.Item("ccGorohKala").Width = 80
            GridEXSatr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXSatr.CurrentTable.Columns.Item("ccGorohKala").Position = 1
            'GridEXTitr.CurrentTable.Columns.Item("ccGorohKala").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXSatr.CurrentTable.Columns.Item("NameGoroh").Caption = "نــام گروه کــالا"
            GridEXSatr.CurrentTable.Columns.Item("NameGoroh").Visible = True
            GridEXSatr.CurrentTable.Columns.Item("NameGoroh").Width = 250
            GridEXSatr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXSatr.CurrentTable.Columns.Item("NameGoroh").Position = 2
            GridEXSatr.CurrentTable.Columns.Item("NameGoroh").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXSatr.CurrentTable.Columns.Item("Tedad").Caption = "تعــداد"
            GridEXSatr.CurrentTable.Columns.Item("Tedad").Visible = True
            GridEXSatr.CurrentTable.Columns.Item("Tedad").Width = 70
            GridEXSatr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXSatr.CurrentTable.Columns.Item("Tedad").Position = 3
            GridEXSatr.CurrentTable.Columns.Item("Tedad").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXSatr.CurrentTable.Columns.Item("HadeAghlKharidDarFaktor").Caption = "حداقـل تعداد خرید در فاکتــور"
            GridEXSatr.CurrentTable.Columns.Item("HadeAghlKharidDarFaktor").Visible = True
            GridEXSatr.CurrentTable.Columns.Item("HadeAghlKharidDarFaktor").Width = 175
            GridEXSatr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXSatr.CurrentTable.Columns.Item("HadeAghlKharidDarFaktor").Position = 4
            GridEXSatr.CurrentTable.Columns.Item("HadeAghlKharidDarFaktor").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

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

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->SetGridStyle")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->SetGridStyle")
        End Try

    End Sub
    Private Function AddNewَTitr() As Boolean
        Try
            AddNewَTitr = False

            If Not IsValidBeforSaveTitr("All") Then
                Exit Function
                Return False
            End If
            '
            cmTitr.Position = 0

            Dim cnSQL As SqlConnection
            Dim cmSQL As SqlCommand
            Dim p As New SqlParameter
            Dim strSQL As String

            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()

            strSQL = "Sales.spSabadGorohKala_InsertTitr "

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            p = New SqlParameter("NameSabadGorohKala", SqlDbType.NVarChar, 50)
            p.Value = txtNameSabadGorohKala.Text
            cmSQL.Parameters.Add(p)

            p = New SqlParameter("Faal", SqlDbType.Bit)
            p.Value = chkFaal.Checked
            cmSQL.Parameters.Add(p)

            p = New SqlParameter("ccSabadGorohKala", SqlDbType.Int)
            p.Direction = ParameterDirection.Output
            cmSQL.Parameters.Add(p)

            cmSQL.ExecuteNonQuery()

            tCodeCounter = cmSQL.Parameters("ccSabadGorohKala").Value

            cnSQL.Close()
            cmSQL = Nothing : cnSQL = Nothing

            Mode = UD_Dll.Enums.GL_ModeForms.None
            Return True

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->AddNewSanad")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->AddNewSanad")
        End Try

    End Function
    Private Sub AddNewRow()
        Try
            If Not IsValidRow("All") Then
                Exit Sub
            End If

            Dim cnSQL As SqlConnection
            Dim cmSQL As SqlCommand
            Dim p As New SqlParameter
            Dim strSQL As String

            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()

            strSQL = "Sales.spSabadGorohKala_InsertSatr "

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            p = New SqlParameter("ccSabadGorohKala", SqlDbType.Int)
            p.Value = dvTitr(cmTitr.Position)("ccSabadGorohKala")
            cmSQL.Parameters.Add(p)

            p = New SqlParameter("ccGorohKala", SqlDbType.Int)
            p.Value = ccGorohKala
            cmSQL.Parameters.Add(p)

            p = New SqlParameter("Tedad", SqlDbType.Float)
            p.Value = CType(mskTedadKala.Text, Double)
            cmSQL.Parameters.Add(p)

            p = New SqlParameter("HadeAghlKharidDarFaktor", SqlDbType.Float)
            p.Value = CType(mskHadeAghalKaridDarFaktor.Text, Double)
            cmSQL.Parameters.Add(p)

            p = New SqlParameter("ccSabadGorohKalaSatr", SqlDbType.Int)
            p.Direction = ParameterDirection.Output
            cmSQL.Parameters.Add(p)

            cmSQL.ExecuteNonQuery()

            cnSQL.Close()
            cmSQL = Nothing : cnSQL = Nothing

            ClearForm()
            RefreshSatrData()
            Mode = UD_Dll.Enums.GL_ModeForms.None
            SetFormObject()

        Catch sqlExc As SqlException
            If (sqlExc.Number = 2627) Or (sqlExc.Number = 229) Then
                If Microsoft.VisualBasic.Left(sqlExc.Message, 1) = "I" Then
                    MsgBox("خطا در اضافه کردن رکورد جديد ,ثبت انجام نشد", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "خطاي بانک")
                ElseIf Microsoft.VisualBasic.Left(sqlExc.Message, 1) = "V" Then
                    MsgBox("خطا در اضافه کردن رکورد جديد ,رکورد در بانک موجود است ,ثبت انجام نشد", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "خطاي بانک")
                End If
            Else
                MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "خطاي بانک")
            End If
        Catch e As Exception
            MsgBox(e.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "خطا")
        End Try
    End Sub
    Private Function UpdateTitr() As Boolean
        Try
            UpdateTitr = False
            If Not IsValidBeforSaveTitr("All") Then
                Exit Function
                Return False
            End If
            '
            Dim cnSQL As SqlConnection
            Dim cmSQL As SqlCommand
            Dim p As New SqlParameter
            Dim strSqlTitr

            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()

            ObjCode.SabteTaghirat(CodeMahalFaal, UD_Dll.Enums.GL_NoeTaghir.UpdateRecord, "Sales.SabadGorohKala", dvTitr(cmTitr.Position)("ccSabadGorohKala"), dvTitr(cmTitr.Position)("ccSabadGorohKala"), "به روز رسانی ")

            strSqlTitr = "Sales.spSabadGorohKala_UpdateTitr "

            cmSQL = New SqlCommand(strSqlTitr, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            p = New SqlParameter("NameSabadGorohKala", SqlDbType.NVarChar, 50)
            p.Value = txtNameSabadGorohKala.Text
            cmSQL.Parameters.Add(p)

            p = New SqlParameter("Faal", SqlDbType.Bit)
            p.Value = chkFaal.Checked
            cmSQL.Parameters.Add(p)

            p = New SqlParameter("ccSabadGorohKala", SqlDbType.Int)
            p.Value = dvTitr(cmTitr.Position)("ccSabadGorohKala")
            cmSQL.Parameters.Add(p)

            cmSQL.ExecuteNonQuery()

            cnSQL.Close()
            cmSQL = Nothing : cnSQL = Nothing

            Mode = UD_Dll.Enums.GL_ModeForms.None
            Return True
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->UpdateSanad")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->UpdateSanad")
        End Try

    End Function
    Private Sub UpdateRow()
        Try
            If Not IsValidRow("All") Then
                Exit Sub
            End If


            Dim cnSQL As SqlConnection
            Dim cmSQL As SqlCommand
            Dim p As New SqlParameter
            Dim strSQL As String

            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()

            strSQL = "Sales.spSabadGorohKala_UpdateSatr "

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            p = New SqlParameter("ccGorohKala", SqlDbType.Int)
            p.Value = ccGorohKala
            cmSQL.Parameters.Add(p)

            p = New SqlParameter("Tedad", SqlDbType.Float)
            p.Value = mskTedadKala.Text
            cmSQL.Parameters.Add(p)

            p = New SqlParameter("HadeAghlKharidDarFaktor", SqlDbType.Float)
            p.Value = mskHadeAghalKaridDarFaktor.Text
            cmSQL.Parameters.Add(p)

            p = New SqlParameter("ccSabadGorohKalaSatr", SqlDbType.Int)
            p.Value = dvSatr(cmSatr.Position)("ccSabadGorohKalaSatr")
            cmSQL.Parameters.Add(p)

            cmSQL.ExecuteNonQuery()

            cnSQL.Close()
            cmSQL = Nothing
            cnSQL = Nothing
            ClearForm()
            RefreshSatrData()
            Mode = UD_Dll.Enums.GL_ModeForms.None
            SetFormObject()
        Catch sqlExc As SqlException
            If (sqlExc.Number = 2627) Or (sqlExc.Number = 229) Then
                If Microsoft.VisualBasic.Left(sqlExc.Message, 1) = "I" Then
                    MsgBox("خطا در اضافه کردن رکورد جديد ,ثبت انجام نشد", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "خطاي بانک")
                ElseIf Microsoft.VisualBasic.Left(sqlExc.Message, 1) = "V" Then
                    MsgBox("خطا در اضافه کردن رکورد جديد ,رکورد در بانک موجود است ,ثبت انجام نشد", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "خطاي بانک")
                End If
            Else
                MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "خطاي بانک")
            End If
        Catch e As Exception
            MsgBox(e.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "خطا")
        End Try

    End Sub
    Private Sub DeleteTitr()
        Try
            Dim cnSQL As SqlConnection
            Dim cmSQL As SqlCommand
            Dim p As New SqlParameter
            Dim strSQLRow As String
            Dim strSQLTitr As String


            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()


            strSQLRow = "ccSabadGorohKala = " & dvTitr(cmTitr.Position)("ccSabadGorohKala")

            If objTools.ConvertNulls(objTools.DLookup("ccSabadGorohKala", "Sales.SabadGorohKalaSatr", strSQLRow), "-1") = -1 Then
                strSQLTitr = "Sales.spSabadGorohKala_DeleteTitr "

                cmSQL = New SqlCommand(strSQLTitr, cnSQL)
                cmSQL.CommandType = CommandType.StoredProcedure
                cmSQL.Parameters.Clear()

                p = New SqlParameter("ccSabadGorohKala", SqlDbType.Int)
                p.Value = dvTitr(cmTitr.Position)("ccSabadGorohKala")
                cmSQL.Parameters.Add(p)

                cmSQL.ExecuteNonQuery()
                cnSQL.Close()
                cmSQL = Nothing : cnSQL = Nothing

                Mode = UD_Dll.Enums.GL_ModeForms.None

            Else
                MsgBox("اين رکورد دارای اطلاعات ميباشد.ابتدا آنها را حذف کنيد.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "خطاي بانک")
            End If
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->DeleteTitr")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->DeleteTitr")
        End Try

    End Sub
    Private Sub DeleteRow()
        Try
            Dim cnSQL As SqlConnection
            Dim cmSQL As SqlCommand
            Dim p As New SqlParameter
            Dim strSQL As String

            If cmSatr.Position < 0 Then
                MsgBox("هیچ سطری برای حذف وجود ندارد.", MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, "پیام")
                Exit Sub
            End If

            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()

            strSQL = "Sales.spSabadGorohKala_DeleteSatr "

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            p = New SqlParameter("ccSabadGorohKalaSatr", SqlDbType.Int)
            p.Value = dvSatr(cmSatr.Position)("ccSabadGorohKalaSatr")
            cmSQL.Parameters.Add(p)

            cmSQL.ExecuteNonQuery()

            cnSQL.Close()
            cmSQL = Nothing : cnSQL = Nothing

            Mode = UD_Dll.Enums.GL_ModeForms.None
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->DeleteRow")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->DeleteRow")
        End Try
    End Sub
    Private Sub DeleteFull()
        Try
            Dim cnSQL As SqlConnection
            Dim cmSQL As SqlCommand
            Dim strSQLRow As String
            Dim p As New SqlParameter
            Dim strSQLTitr As String


            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()

            strSQLRow = "Sales.spSabadGorohKala_DeleteFullSatr "

            cmSQL = New SqlCommand(strSQLRow, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            p = New SqlParameter("ccSabadGorohKala", SqlDbType.Int)
            p.Value = dvTitr(cmTitr.Position)("ccSabadGorohKala")
            cmSQL.Parameters.Add(p)

            cmSQL.ExecuteNonQuery()

            strSQLTitr = "Sales.spSabadGorohKala_DeleteTitr "

            cmSQL = New SqlCommand(strSQLTitr, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            p = New SqlParameter("ccSabadGorohKala", SqlDbType.Int)
            p.Value = dvTitr(cmTitr.Position)("ccSabadGorohKala")
            cmSQL.Parameters.Add(p)

            cmSQL.ExecuteNonQuery()

            cnSQL.Close()
            cmSQL = Nothing
            cnSQL = Nothing

            Mode = UD_Dll.Enums.GL_ModeForms.None
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->DeleteTitr")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->DeleteTitr")
        End Try
    End Sub
    Private Sub PrintSabadgorohKala()
        Try
            Dim cnSQL As SqlConnection
            Dim cmSQL As New SqlCommand
            Dim p As New SqlParameter
            Dim strSQL As String

            Windows.Forms.Cursor.Current = Cursors.WaitCursor
            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()

            strSQL = "Sales.spSabadGorohKala_Print "

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            p = New SqlParameter("ccSabadGorohKala", SqlDbType.Int)
            p.Value = dvTitr(cmTitr.Position)("ccSabadGorohKala")
            cmSQL.Parameters.Add(p)

            If dsForm.Tables.Contains("tblSabadGorohKala_Print") Then
                dsForm.Tables.Remove("tblSabadGorohKala_Print")
            End If

            Dim daSQL As SqlDataAdapter
            daSQL = New SqlDataAdapter(cmSQL)
            daSQL.Fill(dsForm, "tblSabadGorohKala_Print")

            Dim rpt As New ReportDocument
            Dim rpttables As Tables
            Dim rptformula As FormulaFieldDefinitions
            Dim frm As New Forms_dll.frmGL_Gozaresh

            rpt.Load(rptPath & "\rptFO_GozareshSabadGorohKala.rpt")

            rpttables = rpt.Database.Tables
            rpttables.Item(0).SetDataSource(dsForm.Tables("tblSabadGorohKala_Print"))

            rptformula = rpt.DataDefinition.FormulaFields
            With rptformula

                .Item("Group").Text = "{mydata.ccSabadGorohKala}"
                .Item("NameSabadGorohKala").Text = "{mydata.NameSabadGorohKala}"
                .Item("Vazeiat").Text = "{mydata.txtFaal}"
                .Item("CodeGorohKala").Text = "{mydata.CodeGorohKala}"
                .Item("NameGorohKala").Text = "{mydata.txtGorohKala}"
                .Item("Tedad").Text = "{mydata.Tedad}"
                .Item("HadeAghlKharidDarFaktor").Text = "{mydata.HadeAghlKharidDarFaktor}"
                .Item("Title").Text = "'" & "سبد گــروه کـــالا" & "'"
                .Item("Title2").Text = "'" & NameSherkat & "'"
                .Item("Title3").Text = "'" & NameMahalFaal & "'"
                .Item("KarbarGozaresh").Text = "'" & PersonelName & "'"
                .Item("TarikhGozaresh").Text = "'" & objTarikh.SetDateSlash(TarikhEmrooz) & "'"
                .Item("SaatGozaresh").Text = "'" & Format(TimeOfDay, "HH:mm:ss") & "'"
            End With
            rpt.Refresh()

            frm.Text = "سبد گــروه کـــالا"

            With frm.CRV
                .ReportSource = rpt
                .DisplayGroupTree = False
                .ShowGroupTreeButton = False
                If Not objSec.HaveUserPermission(SN, UD_Dll.Security.ePermissions.Print) Then .ShowPrintButton = False
                If Not objSec.HaveUserPermission(SN, UD_Dll.Security.ePermissions.Export) Then .ShowExportButton = False
            End With


            Me.Hide()
            frm.ShowDialog(Me)
            frm = Nothing
            daSQL = Nothing
            rpt = Nothing
            Me.Show()
            Windows.Forms.Cursor.Current = Cursors.Default
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->Print PishFaktor")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->Print PishFaktor")
        End Try
    End Sub

    Private Function IsValidBeforSaveTitr(ByVal CheckField As String) As Boolean
        Try
            IsValidBeforSaveTitr = False

            If CheckField = "NameSabadGorohKala" Or CheckField = "All" Then
                If txtNameSabadGorohKala.Text = "" Then
                    ErrPro.SetError(Me.txtNameSabadGorohKala, "نام سبد گـــروه کالا را وارد کنید.")
                    MsgBox("نام سبد گـــروه کالا را وارد کنید.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
                    txtNameSabadGorohKala.Focus()
                    Exit Function
                End If
                ErrPro.SetError(Me.txtNameSabadGorohKala, "")
            End If

            If Mode = UD_Dll.Enums.GL_ModeForms.AddNewRecord Then
                If objTools.ConvertNulls(objTools.DCount("NameSabadGorohKala", "Sales.SabadGorohKala", "NameSabadGorohKala = '" & txtNameSabadGorohKala.Text & "'"), 0) > 0 Then
                    MsgBox("نام مورد نظر تکـــراری است ! ", MsgBoxStyle.Information + MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, "خطا در افزودن رکورد جدید ")
                    Exit Function
                End If
            End If

            Return True
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->IsValidBeforSaveTitr")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->IsValidBeforSaveTitr")
        End Try

    End Function
    Private Function IsValidRow(ByVal chkField As String) As Boolean
        Try
            IsValidRow = False

            If chkField = "ccGorohKala" Or chkField = "All" Then
                If cmbGoroh1.SelectedIndex = -1 Then
                    ErrPro.SetError(Me.cmbGoroh1, "گروه کالا را مشخص نمایید.")
                    MsgBox("کــد کالا را وارد کنید.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
                    cmbGoroh1.Focus()
                    Exit Function
                End If
                ErrPro.SetError(Me.cmbGoroh1, "")
            End If

            If chkField = "mskTedadKala" Or chkField = "All" Then
                If mskTedadKala.Text = "" Then
                    ErrPro.SetError(Me.mskTedadKala, "تعـــداد کالا را وارد کنید.")
                    MsgBox("تعـــداد کالا را وارد کنید.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
                    mskTedadKala.Focus()
                    Exit Function
                End If
                ErrPro.SetError(Me.mskTedadKala, "")
            End If

            If chkField = "mskHadeAghalKaridDarFaktor" Or chkField = "All" Then
                If mskHadeAghalKaridDarFaktor.Text = "" Then
                    ErrPro.SetError(Me.mskHadeAghalKaridDarFaktor, "حداقل تعداد خرید از این کالا در هر فاکتور را مشخص نمایید.")
                    MsgBox("حداقل تعداد خرید از این کالا در هر فاکتور را مشخص نمایید.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
                    mskHadeAghalKaridDarFaktor.Focus()
                    Exit Function
                End If
                ErrPro.SetError(Me.mskHadeAghalKaridDarFaktor, "")
            End If

            If Mode = UD_Dll.Enums.GL_ModeForms.AddNewRow Then
                If objTools.ConvertNulls(objTools.DCount("ccGorohKala", "Sales.SabadGorohKalaSatr", "ccSabadGorohKala = " & dvTitr(cmTitr.Position)("ccSabadGorohKala") & " and ccGorohKala = " & ccGorohKala), 0) > 0 Then
                    MsgBox("این گروه کالا قبلاً وارد شده است ! ", MsgBoxStyle.Information + MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, "خطا در افزودن رکورد جدید ")
                    Exit Function
                End If
            End If

            Return True

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->IsValidRow")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->IsValidRow")
        End Try

    End Function
#End Region
#Region "From Buttons"
    Private Sub btnEditRow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEditRow.Click
        If Not objSec.HaveUserPermission(SN, UD_Dll.Security.ePermissions.UpdateSatr) Then Exit Sub
        Try
            Mode = UD_Dll.Enums.GL_ModeForms.UpdateRow
            SetFormSatrData(cmSatr.Position)
            SetFormObject()
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->btnEdit_Click")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->btnEdit_Click")
        End Try
    End Sub
    Private Sub btnDeleteRow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeleteRow.Click
        If Not objSec.HaveUserPermission(SN, UD_Dll.Security.ePermissions.Delete) Then Exit Sub
        Try
            If MsgBox("آيا رکورد حذف شود؟", MsgBoxStyle.YesNo + MsgBoxStyle.Question + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.DefaultButton2, "حذف رکورد") = MsgBoxResult.Yes Then
                DeleteRow()
                RefreshSatrData()
                SetFormObject()
            End If
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->btnDelete_Click")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->btnDelete_Click")
        End Try
    End Sub
    Private Sub btnFullDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFullDelete.Click
        If Not objSec.HaveUserPermission(SN, UD_Dll.Security.ePermissions.DeleteSatr) Then Exit Sub
        If Not objSec.HaveUserPermission(SN, UD_Dll.Security.ePermissions.Delete) Then Exit Sub

        Try
            If MsgBox("آيا حاضريد کل اطلاعات مربوط به این رکورد حذف شود؟", MsgBoxStyle.YesNo + MsgBoxStyle.Question + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.DefaultButton2, "حذف رکورد") = MsgBoxResult.No Then
                Exit Sub
            End If
            If MsgBox("آيا حاضريد کل اطلاعات مربوط به این رکورد حذف شود؟", MsgBoxStyle.YesNo + MsgBoxStyle.Question + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.DefaultButton2, "حذف رکورد") = MsgBoxResult.Yes Then
                DeleteFull()
                Search(True)
            End If
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->btnDeleteTitr_Click")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->btnDeleteTitr_Click")
        End Try
    End Sub
    Private Sub btnEditTitr_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEditTitr.Click
        If Not objSec.HaveUserPermission(SN, UD_Dll.Security.ePermissions.Update) Then Exit Sub
        Try
            Mode = UD_Dll.Enums.GL_ModeForms.UpdateRecord
            SetFormTitrData()
            SetFormObject()
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->btnEditSanad_Click")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->btnEditSanad_Click")
        End Try

    End Sub
    Private Sub btnDeleteTitr_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeleteTitr.Click
        If Not objSec.HaveUserPermission(SN, UD_Dll.Security.ePermissions.Delete) Then Exit Sub
        Try
            If MsgBox("آيا تيتر جاري حذف شود؟", MsgBoxStyle.YesNo + MsgBoxStyle.Question + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.DefaultButton2, "حذف رکورد") = MsgBoxResult.Yes Then
                DeleteTitr()
                Search(False)
            End If
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->btnDeleteTitr_Click")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->btnDeleteTitr_Click")
        End Try

    End Sub
    Private Sub btnNewTitr_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNewTitr.Click
        Try
            If Not objSec.HaveUserPermission(SN, UD_Dll.Security.ePermissions.Add) Then Exit Sub
            Search(False)
            GridEXSatr.DataSource = Nothing
            ClearForm()
            Mode = UD_Dll.Enums.GL_ModeForms.AddNewRecord
            SetFormObject()
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->btnNewSanad_Click")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->btnNewSanad_Click")
        End Try
    End Sub
    Private Sub btnNewRow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNewRow.Click
        Try
            If Not objSec.HaveUserPermission(SN, UD_Dll.Security.ePermissions.AddSatr) Then Exit Sub
            Mode = UD_Dll.Enums.GL_ModeForms.AddNewRow
            SetFormObject()
            'Search(True)
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->btnNewRow_Click")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->btnNewRow_Click")
        End Try

    End Sub
    Private Sub btnCancelTitr_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelTitr.Click
        Try
            Mode = UD_Dll.Enums.GL_ModeForms.None
            ClearForm()
            SetFormObject()
            Search(True)
            SetFormTitrData()
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->btnCancelSanad_Click")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->btnCancelSanad_Click")
        End Try

    End Sub
    Private Sub btnCancelRow_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelRow.Click
        Try
            Mode = UD_Dll.Enums.GL_ModeForms.None
            ClearForm()
            SetFormObject()
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->btnCancel_Click")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->btnCancel_Click")
        End Try
    End Sub
    Private Sub btnSaveTitr_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSaveTitr.Click
        Try

            Select Case Mode
                Case UD_Dll.Enums.GL_ModeForms.AddNewRecord
                    If Not objSec.HaveUserPermission(SN, UD_Dll.Security.ePermissions.Add) Then Exit Sub
                    If AddNewَTitr() = False Then
                        Exit Sub
                    Else
                    End If
                Case UD_Dll.Enums.GL_ModeForms.UpdateRecord
                    If Not objSec.HaveUserPermission(SN, UD_Dll.Security.ePermissions.Update) Then Exit Sub
                    If UpdateTitr() = False Then
                        Exit Sub
                    End If
            End Select
            Search(False)
            ClearForm()
            SetFormObject()
            SetFormTitrData()
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->btnSaveSanad_Click")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->btnSaveSanad_Click")
        End Try
    End Sub
    Private Sub btnSaveRow_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSaveRow.Click
        Try
            If cmbGoroh5.SelectedIndex = -1 Then
                If cmbGoroh4.SelectedIndex = -1 Then
                    If cmbGoroh3.SelectedIndex = -1 Then
                        If cmbGoroh2.SelectedIndex = -1 Then
                            ccGorohKala = cmbGoroh1.SelectedValue
                        Else
                            ccGorohKala = cmbGoroh2.SelectedValue
                        End If
                    Else
                        ccGorohKala = cmbGoroh3.SelectedValue
                    End If
                Else
                    ccGorohKala = cmbGoroh4.SelectedValue
                End If
            Else
                ccGorohKala = cmbGoroh5.SelectedValue
            End If

            Select Case Mode
                Case UD_Dll.Enums.GL_ModeForms.AddNewRow
                    If Not objSec.HaveUserPermission(SN, UD_Dll.Security.ePermissions.AddSatr) Then Exit Sub
                    AddNewRow()
                Case UD_Dll.Enums.GL_ModeForms.UpdateRow
                    If Not objSec.HaveUserPermission(SN, UD_Dll.Security.ePermissions.UpdateSatr) Then Exit Sub
                    UpdateRow()
            End Select
            SetGridStyle()
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->btnSave_Click")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->btnSave_Click")
        End Try
    End Sub
    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        If Not objSec.HaveUserPermission(SN, UD_Dll.Security.ePermissions.Preview) Then Exit Sub
        Try
            If dvTitr.Count > 0 Then
                Me.TopMost = False
                PrintSabadgorohKala()
                Me.TopMost = True
            End If
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->btnPrintM_Click")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->btnPrintM_Click")
        End Try
    End Sub
    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Try
            Me.Close()
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->btnExit_Click")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->btnExit_Click")
        End Try

    End Sub
#End Region

    Private Sub cmbGoroh1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbGoroh1.SelectedIndexChanged
        If flg Then
            Try
                flg = False
                LoadComboGoroh2()
                flg = True
                cmbGoroh3.DataSource = Nothing
                cmbGoroh2.SelectedIndex = -1
            Catch sqlExc As SqlException
                MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "Error IN DataBase ---->" & "cmbSKeshvar_Validated")
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, "Error IN ---->" & "cmbSKeshvar_Validated")
            End Try
        End If
    End Sub

    Private Sub cmbGoroh2_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbGoroh2.SelectedIndexChanged
        If flg Then
            Try
                flg = False
                LoadComboGoroh3()
                flg = True
                cmbGoroh4.DataSource = Nothing
                cmbGoroh3.SelectedIndex = -1
            Catch sqlExc As SqlException
                MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "Error IN DataBase ---->" & "cmbSKeshvar_Validated")
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, "Error IN ---->" & "cmbSKeshvar_Validated")
            End Try
        End If
    End Sub

    Private Sub cmbGoroh3_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbGoroh3.SelectedIndexChanged
        If flg Then
            Try
                flg = False
                LoadComboGoroh4()
                flg = True
                cmbGoroh5.DataSource = Nothing
                cmbGoroh4.SelectedIndex = -1
            Catch sqlExc As SqlException
                MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "Error IN DataBase ---->" & "cmbSKeshvar_Validated")
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, "Error IN ---->" & "cmbSKeshvar_Validated")
            End Try
        End If
    End Sub

    Private Sub cmbGoroh4_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbGoroh4.SelectedIndexChanged
        If flg Then
            Try
                LoadComboGoroh5()
                cmbGoroh5.SelectedIndex = -1
            Catch sqlExc As SqlException
                MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "Error IN DataBase ---->" & "cmbSKeshvar_Validated")
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, "Error IN ---->" & "cmbSKeshvar_Validated")
            End Try
        End If
    End Sub

    
End Class
