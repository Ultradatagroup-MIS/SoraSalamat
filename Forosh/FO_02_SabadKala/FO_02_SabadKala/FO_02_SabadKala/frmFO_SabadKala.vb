Public Class frmFO_SabadKala

#Region "Variable AND Constant Declration"
    ' Security 
    Const cntCodeSubSystem As Long = 100073
    Private SN As Integer

    Const TitrGridSize As Integer = 190
    Const SatrGridSize As Integer = 210
    Const TitrOrgSize As Integer = 225
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
    Dim IsSabadKala As Boolean = False
    Dim txtCaption As String
    Private WithEvents BS As New UD_Dll.PassString
#End Region
#Region "Form Event Code"
    Private Sub GridEXSatr_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GridEXSatr.DoubleClick
        Try
            'If IsUsed(Val(GridEXTitr.CurrentRow.Cells("ccSabadKala").Text.Replace(",", ""))) Then
            '    MsgBox("از این سبد کالا در فاکتور استفاده شده است . امکان ویرایش کالاهای آن وجود ندارد .", MsgBoxStyle.Information + MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " خطا ")
            '    Exit Sub
            'End If

            If objTools.DCount("ccSabadKalaSatr", "Sales.SabadKalaSatr", "ccSabadKala_Link = " & Val(GridEXTitr.CurrentRow.Cells("ccSabadKala").Text.Replace(",", ""))) <> 0 Then
                MsgBox(" از این سبد، در سبدی دیگر استفاده شده است . اجازه ویرایش آن را ندارید .", MsgBoxStyle.Information + MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, "")
                Exit Sub
            End If

            If objTools.ConvertNulls(Val(GridEXSatr.CurrentRow.Cells("ccSabadKala_Link").Text.Replace(",", "")), 0) <> 0 Then
                MsgBox("این کالا مربوط به سبد استفاده شده در این سبد است . جهت ویرایش ابتدا کل سبد را حذف نموده و مجدداً وارد نمایید .", MsgBoxStyle.Information + MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, "")
                Exit Sub
            End If

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
        If GridEXTitr.CurrentRow.RowType <> Janus.Windows.GridEX.RowType.FilterRow Then
            BoundCurrencyManagerTitr()
            ''SetGridStyle()
            SetFormTitrData()
            RefreshSatrData()
            SetGridStyleSatr()
        End If
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
    Private Sub mskCodeKala_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCodeKala.KeyPress
        Try
            If (Asc(e.KeyChar()) < 48 Or Asc(e.KeyChar()) > 57) And (Asc(e.KeyChar()) <> 8) Then
                e.Handled = True
            End If
            If e.KeyChar = Chr(Keys.Space) Then
                Dim objKala As New Forms_dll.frmAN_KalaSearch
                Dim StrSql As String

                StrSql = " SELECT CodeKala,NameKala,ccKala,txtsVahedeShomaresh,sVahedeShomaresh,NameBrand,RadifBrand,0 as IsSabadKala"
                StrSql &= " FROM qryAN_Kala Where Faal=1 "

                StrSql &= " UNION ALL "

                StrSql &= " SELECT CodeSabad, NameSabadKala, ccSabadKala, 'پــک' , 0, "
                StrSql &= " (CASE sNoeMoshtary WHEN 1 THEN b.Sharh ELSE 'همه' END), 0, 1 as IsSabadKala"
                StrSql &= " FROM Sales.SabadKala AS a WITH(NOLOCK) LEFT OUTER JOIN "
                StrSql &= " tblGL_ShenasehOmomi AS b WITH(NOLOCK) ON a.sNoeMoshtary = b.Code"
                StrSql &= " WHERE Faal = 1"

                If txtCodeKala.Text.Length <> 0 Then
                    objKala.tcodeKala = txtCodeKala.Text
                End If

                MultiSelection = False
                SearchItem = "CodeKala"
                objKala.SetForm(StrSql)
                objKala.ShowDialog()

                IsSabadKala = objKala.tIsSabadKala
                Me.txtCodeKala.Tag = objKala.tccKala
                Me.txtCodeKala.Text = objKala.tcodeKala
                Me.txtNameKala.Text = objKala.tNameKala

                If IsSabadKala = True Then
                    mskFee.Text = 0
                    mskFee.Enabled = False
                Else
                    mskFee.Enabled = True
                End If

                MultiSelection = False
            End If
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->txtCodeMoshtaryS_KeyPress")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->txtCodeMoshtaryS_KeyPress")
        End Try
    End Sub
    Private Sub txtCodeKala_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCodeKala.TextChanged
        If txtCodeKala.Text <> "" Then
            If IsSabadKala = 0 Then
                txtNameKala.Text = objTools.DLookup("NameKala", "tblAN_Kala", "CodeKala = " & txtCodeKala.Text)
                Me.txtCodeKala.Tag = objTools.DLookup("ccKala", "tblAN_Kala", "CodeKala = " & txtCodeKala.Text)
            End If
        End If
    End Sub
    Private Sub frmFO_SabadKala_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            SetParameter()
            SN = objSec.GetSecurityNumber(cntCodeSubSystem, UserName)
            LoadCombo()
            cmbSNoeMoshtary.SelectedValue = 0
            cmbSNoeMoshtary.SelectedValue = 0
            Mode = UD_Dll.Enums.GL_ModeForms.None
            flg = False
            ClearForm()
            Search(False)
            flg = True
            SetFormObject()
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
            CodeDoreh = "1392"
            txtCaption = "سبد کالا"
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
    Private Sub ClearForm()
        Try
            txtNameSabadKala.Text = ""
            txtNameSabad_Copy.Text = ""
            txtCodeKala.Text = ""
            txtNameKala.Text = ""
            mskTedadKala.Text = ""
            mskHadeAghalKaridDarFaktor.Text = 1
            mskFee.Text = ""
            mskFee.Enabled = True
            mskCodeSabadKala.Text = ""
            mskCodeSabad_Copy.Text = ""
            mskCcSabad_CopyFrom.Text = ""
            chkFaal.Checked = False
            chkCopy.Visible = True
            chkCopy.Checked = False
            cmbSNoeMoshtary.SelectedValue = 0
            grbInsertPublic.Visible = True
            grbCopyForOtherSabad.Visible = False
            ErrPro.Dispose()

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->ClearForm")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->ClearForm")
        End Try

    End Sub
    Private Sub LoadCombo()

        Dim Strsql As String
        Dim daSQL As SqlDataAdapter
        Dim dr As DataRow

        Strsql = "Select * From tblGL_ShenasehOmomi Where CodeAsli= 152 And CodeFarei<>0 order by MeghdarAdadi"
        daSQL = New SqlDataAdapter(Strsql, ConnectionString)
        daSQL.Fill(dsForm, "sNoeMoshtary")
        dr = dsForm.Tables("sNoeMoshtary").NewRow
        dr("Sharh") = "همه"
        dr("Code") = 0
        dsForm.Tables("sNoeMoshtary").Rows.Add(dr)
        cmbSNoeMoshtary.DataSource = Nothing
        cmbSNoeMoshtary.Items.Clear()
        cmbSNoeMoshtary.DataSource = dsForm.Tables("sNoeMoshtary").DefaultView
        cmbSNoeMoshtary.DisplayMember = "Sharh"
        cmbSNoeMoshtary.ValueMember = "Code"

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
                    Me.txtNameSabadKala.Focus()
                Case UD_Dll.Enums.GL_ModeForms.AddNewRow
                    GridEXSatr.Height = SatrGridSize
                    GridEXTitr.Enabled = False
                    GridEXSatr.Enabled = False
                    Me.txtCodeKala.Focus()
                Case UD_Dll.Enums.GL_ModeForms.UpdateRow
                    GridEXSatr.Height = SatrGridSize
                    GridEXTitr.Enabled = False
                    GridEXSatr.Enabled = False
                    Me.txtCodeKala.Focus()
                Case UD_Dll.Enums.GL_ModeForms.UpdateRecord
                    GridEXTitr.Height = TitrGridSize
                    GridEXTitr.Enabled = False
                    GridEXSatr.Enabled = False
                    Me.txtNameSabadKala.Focus()
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
    Private Sub Search(ByVal WithCriteria As Boolean)
        Try
            Dim StrSql As String

            StrSql = "Sales.spSabadKala_SearchTitr "

            RefreshTitrdata(StrSql)
            'RefreshSatrData()
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

            If dsForm.Tables.Contains("Sales_SabadKala") Then
                dsForm.Tables.Remove("Sales_SabadKala")
            End If

            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()

            cmSQL = New SqlCommand(strSql, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            daSQL = New SqlDataAdapter(cmSQL)
            daSQL.Fill(dsForm, "Sales_SabadKala")

            dvTitr = New DataView(dsForm.Tables("Sales_SabadKala"), "", "ccSabadKala ASC", DataViewRowState.CurrentRows)
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
            'RefreshSatrData()
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
            SetGridStyleSatr()
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

            txtNameSabadKala.Text = GridEXTitr.CurrentRow.Cells("NameSabadKala").Text
            mskCodeSabadKala.Text = Val(GridEXTitr.CurrentRow.Cells("CodeSabad").Text.Replace(",", ""))
            chkFaal.Checked = GridEXTitr.CurrentRow.Cells("Faal").Value
            chkCopy.Visible = False

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

            If Mode = UD_Dll.Enums.GL_ModeForms.UpdateRow Then
                txtCodeKala.Text = GridEXSatr.CurrentRow.Cells("CodeKala").Value
                txtNameKala.Text = GridEXSatr.CurrentRow.Cells("NameKala").Text
                mskTedadKala.Text = GridEXSatr.CurrentRow.Cells("Tedad").Value
                mskFee.Text = GridEXSatr.CurrentRow.Cells("Fee").Value
                mskHadeAghalKaridDarFaktor.Text = GridEXSatr.CurrentRow.Cells("HadeAghlKharidDarFaktor").Value
                txtCodeKala.Tag = objTools.DLookup("ccKala", "tblAN_Kala", "CodeKala = " & txtCodeKala.Text)
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

            If dsForm.Tables.Contains("Sales_SabadKalaSatr") Then
                dsForm.Tables.Remove("Sales_SabadKalaSatr")
            End If

            Strsql = "Sales.spSabadKala_SearchSatr "

            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()

            cmSQL = New SqlCommand(Strsql, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            cmSQL.Parameters.AddWithValue("ccSabadKala", GridEXTitr.CurrentRow.Cells("ccSabadKala").Value)

            daSQL = New SqlDataAdapter(cmSQL)
            daSQL.Fill(dsForm, "Sales_SabadKalaSatr")

            dvSatr = New DataView(dsForm.Tables("Sales_SabadKalaSatr"))

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
                .DataSource = dsForm.Tables("Sales_SabadKala").DefaultView
                .SetDataBinding(dsForm.Tables("Sales_SabadKala").DefaultView, "")
                .RetrieveStructure()
            End With

            For i As Integer = 0 To GridEXTitr.CurrentTable.Columns.Count - 1
                GridEXTitr.CurrentTable.Columns.Item(i).Visible = False
            Next


            GridEXTitr.CurrentTable.Columns.Item("ccSabadKala").Caption = "کــد سیستمی"
            GridEXTitr.CurrentTable.Columns.Item("ccSabadKala").Visible = True
            GridEXTitr.CurrentTable.Columns.Item("ccSabadKala").Width = 100
            GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXTitr.CurrentTable.Columns.Item("ccSabadKala").Position = 0
            GridEXTitr.CurrentTable.Columns.Item("ccSabadKala").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXTitr.CurrentTable.Columns.Item("CodeSabad").Caption = "کد سبــــد کــالا"
            GridEXTitr.CurrentTable.Columns.Item("CodeSabad").Visible = True
            GridEXTitr.CurrentTable.Columns.Item("CodeSabad").Width = 100
            GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXTitr.CurrentTable.Columns.Item("CodeSabad").Position = 1
            GridEXTitr.CurrentTable.Columns.Item("CodeSabad").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXTitr.CurrentTable.Columns.Item("NameSabadKala").Caption = "نام سبــــد کــالا"
            GridEXTitr.CurrentTable.Columns.Item("NameSabadKala").Visible = True
            GridEXTitr.CurrentTable.Columns.Item("NameSabadKala").Width = 280
            GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXTitr.CurrentTable.Columns.Item("NameSabadKala").Position = 2
            GridEXTitr.CurrentTable.Columns.Item("NameSabadKala").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXTitr.CurrentTable.Columns.Item("txtNoeMoshtary").Caption = "نوع مشتری"
            GridEXTitr.CurrentTable.Columns.Item("txtNoeMoshtary").Visible = True
            GridEXTitr.CurrentTable.Columns.Item("txtNoeMoshtary").Width = 150
            GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXTitr.CurrentTable.Columns.Item("txtNoeMoshtary").Position = 3
            GridEXTitr.CurrentTable.Columns.Item("txtNoeMoshtary").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXTitr.CurrentTable.Columns.Item("txtFaal").Caption = "وضعیت"
            GridEXTitr.CurrentTable.Columns.Item("txtFaal").Visible = True
            GridEXTitr.CurrentTable.Columns.Item("txtFaal").Width = 90
            GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXTitr.CurrentTable.Columns.Item("txtFaal").Position = 4
            GridEXTitr.CurrentTable.Columns.Item("txtFaal").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXTitr.CurrentTable.Columns.Item("Mkol").Caption = "بهاء کل"
            GridEXTitr.CurrentTable.Columns.Item("Mkol").Visible = True
            GridEXTitr.CurrentTable.Columns.Item("Mkol").Width = 130
            GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXTitr.CurrentTable.Columns.Item("Mkol").Position = 5
            GridEXTitr.CurrentTable.Columns.Item("Mkol").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXTitr.CurrentTable.Columns.Item("Faal").Caption = "Faal"
            GridEXTitr.CurrentTable.Columns.Item("Faal").Visible = False
            GridEXTitr.CurrentTable.Columns.Item("Faal").Width = 0
            GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXTitr.CurrentTable.Columns.Item("Faal").Position = 6
            GridEXTitr.CurrentTable.Columns.Item("Faal").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

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
                .DataSource = dsForm.Tables("Sales_SabadKalaSatr").DefaultView
                .SetDataBinding(dsForm.Tables("Sales_SabadKalaSatr").DefaultView, "")
                .RetrieveStructure()
            End With

            For i As Integer = 0 To GridEXSatr.CurrentTable.Columns.Count - 1
                GridEXSatr.CurrentTable.Columns.Item(i).Visible = False
            Next


            GridEXSatr.CurrentTable.Columns.Item("ccSabadKalaSatr").Caption = "کد سیستمی"
            GridEXSatr.CurrentTable.Columns.Item("ccSabadKalaSatr").Visible = True
            GridEXSatr.CurrentTable.Columns.Item("ccSabadKalaSatr").Width = 100
            GridEXSatr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXSatr.CurrentTable.Columns.Item("ccSabadKalaSatr").Position = 0
            GridEXSatr.CurrentTable.Columns.Item("ccSabadKalaSatr").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXSatr.CurrentTable.Columns.Item("CodeKala").Caption = "کــد کالا"
            GridEXSatr.CurrentTable.Columns.Item("CodeKala").Visible = True
            GridEXSatr.CurrentTable.Columns.Item("CodeKala").Width = 80
            GridEXSatr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXSatr.CurrentTable.Columns.Item("CodeKala").Position = 1
            'GridEXTitr.CurrentTable.Columns.Item("CodeKala").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXSatr.CurrentTable.Columns.Item("NameKala").Caption = "نــام کــالا"
            GridEXSatr.CurrentTable.Columns.Item("NameKala").Visible = True
            GridEXSatr.CurrentTable.Columns.Item("NameKala").Width = 250
            GridEXSatr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXSatr.CurrentTable.Columns.Item("NameKala").Position = 2
            GridEXSatr.CurrentTable.Columns.Item("NameKala").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXSatr.CurrentTable.Columns.Item("Tedad").Caption = "تعــداد"
            GridEXSatr.CurrentTable.Columns.Item("Tedad").Visible = True
            GridEXSatr.CurrentTable.Columns.Item("Tedad").Width = 70
            GridEXSatr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXSatr.CurrentTable.Columns.Item("Tedad").Position = 3
            GridEXSatr.CurrentTable.Columns.Item("Tedad").FormatString = "###,###.##"
            GridEXSatr.CurrentTable.Columns.Item("Tedad").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXSatr.CurrentTable.Columns.Item("Fee").Caption = "قیمت واحد"
            GridEXSatr.CurrentTable.Columns.Item("Fee").Visible = True
            GridEXSatr.CurrentTable.Columns.Item("Fee").Width = 100
            GridEXSatr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXSatr.CurrentTable.Columns.Item("Fee").Position = 4
            GridEXSatr.CurrentTable.Columns.Item("Fee").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXSatr.CurrentTable.Columns.Item("Mkol").Caption = "جمع کل"
            GridEXSatr.CurrentTable.Columns.Item("Mkol").Visible = True
            GridEXSatr.CurrentTable.Columns.Item("Mkol").Width = 100
            GridEXSatr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXSatr.CurrentTable.Columns.Item("Mkol").Position = 5
            GridEXSatr.CurrentTable.Columns.Item("Mkol").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXSatr.CurrentTable.Columns.Item("HadeAghlKharidDarFaktor").Caption = "حداقـل تعداد خرید در فاکتــور"
            GridEXSatr.CurrentTable.Columns.Item("HadeAghlKharidDarFaktor").Visible = True
            GridEXSatr.CurrentTable.Columns.Item("HadeAghlKharidDarFaktor").Width = 170
            GridEXSatr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXSatr.CurrentTable.Columns.Item("HadeAghlKharidDarFaktor").Position = 6
            GridEXSatr.CurrentTable.Columns.Item("HadeAghlKharidDarFaktor").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXSatr.CurrentTable.Columns.Item("ccSabadKala_Link").Caption = "ccSabadKala_Link"
            GridEXSatr.CurrentTable.Columns.Item("ccSabadKala_Link").Visible = False
            GridEXSatr.CurrentTable.Columns.Item("ccSabadKala_Link").Width = 0
            GridEXSatr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXSatr.CurrentTable.Columns.Item("ccSabadKala_Link").Position = 7
            GridEXSatr.CurrentTable.Columns.Item("ccSabadKala_Link").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            For i As Integer = 0 To GridEXSatr.RootTable.Columns.Count - 1
                If GridEXSatr.RootTable.Columns(i).Type.IsValueType Then
                    GridEXSatr.RootTable.Columns(i).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                    GridEXSatr.RootTable.Columns(i).FormatMode = Janus.Windows.GridEX.FormatMode.UseIFormattable
                    GridEXSatr.RootTable.Columns(i).FormatString = "###,###.##"
                    GridEXSatr.RootTable.Columns(i).TotalFormatMode = Janus.Windows.GridEX.FormatMode.UseIFormattable
                    GridEXSatr.RootTable.Columns(i).TotalFormatString = "###,###.##"
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

            If chkCopy.Checked = False Then
                If Not IsValidBeforSaveTitr("All") Then
                    Exit Function
                    Return False
                End If

                InsertTitr_Public()

            Else

                InsertTitr_Copy()

            End If

            Mode = UD_Dll.Enums.GL_ModeForms.None
            Return True

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->AddNewSanad")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->AddNewSanad")
        End Try

    End Function
    Private Sub InsertTitr_Public()
        Try
            cmTitr.Position = 0

            Dim cnSQL As SqlConnection
            Dim cmSQL As SqlCommand
            Dim p As New SqlParameter
            Dim strSQL As String

            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()

            strSQL = "Sales.spSabadKala_InsertTitr "

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            p = New SqlParameter("NameSabadKala", SqlDbType.NVarChar, 50)
            p.Value = txtNameSabadKala.Text
            cmSQL.Parameters.Add(p)

            p = New SqlParameter("Faal", SqlDbType.Bit)
            p.Value = chkFaal.Checked
            cmSQL.Parameters.Add(p)

            p = New SqlParameter("CodeSabadKala", SqlDbType.Int)
            p.Value = mskCodeSabadKala.Text
            cmSQL.Parameters.Add(p)


            p = New SqlParameter("sNoeMoshtary", SqlDbType.Int)
            p.Value = IIf(cmbSNoeMoshtary.SelectedValue = 0, DBNull.Value, cmbSNoeMoshtary.SelectedValue)
            cmSQL.Parameters.Add(p)


            p = New SqlParameter("ccSabadKala", SqlDbType.Int)
            p.Direction = ParameterDirection.Output
            cmSQL.Parameters.Add(p)


            cmSQL.ExecuteNonQuery()

            tCodeCounter = cmSQL.Parameters("ccSabadKala").Value

            cnSQL.Close()
            cmSQL = Nothing : cnSQL = Nothing

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> InsertTitr_Public ")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> InsertTitr_Public ")
        End Try
    End Sub
    Private Sub InsertTitr_Copy()
        Try
            cmTitr.Position = 0

            Dim cnSQL As SqlConnection
            Dim cmSQL As SqlCommand
            Dim p As New SqlParameter
            Dim strSQL As String

            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()

            strSQL = "Sales.spSabadKala_InsertTitr_FromOtherSabad "

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            p = New SqlParameter("NameSabadKala", SqlDbType.NVarChar, 50)
            p.Value = txtNameSabad_Copy.Text
            cmSQL.Parameters.Add(p)

            p = New SqlParameter("CodeSabadKala", SqlDbType.Int)
            p.Value = mskCodeSabad_Copy.Text
            cmSQL.Parameters.Add(p)

            p = New SqlParameter("sNoeMoshtary", SqlDbType.Int)
            p.Value = IIf(cmbSNoeMoshtary.SelectedValue = 0, DBNull.Value, cmbSNoeMoshtary.SelectedValue)
            cmSQL.Parameters.Add(p)

            p = New SqlParameter("ccSabadKalaFrom", SqlDbType.Int)
            p.Value = mskCcSabad_CopyFrom.Text
            cmSQL.Parameters.Add(p)

            cmSQL.ExecuteNonQuery()

            cnSQL.Close()
            cmSQL = Nothing : cnSQL = Nothing

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> InsertTitr_Copy ")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> InsertTitr_Copy ")
        End Try
    End Sub
    Private Sub AddNewRow()
        Try
            Dim ccKalaTekrary As Integer = objTools.DLookup("Count(ccSabadKalaSatr)", "sales.SabadKalaSatr", "ccKala = " & txtCodeKala.Tag & " AND ccSabadKala = " & Val(GridEXTitr.CurrentRow.Cells("ccSabadKala").Text.Replace(",", "")))

            If ccKalaTekrary <> 0 Then
                MsgBox("این کالا در این سبد ذخیره شده است امکان ثبت تکراری وجود ندارد.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "خطاي بانک")
                Exit Sub
            End If

            If IsSabadKala = True Then
                InsertKalaFromSabad(Val(GridEXTitr.CurrentRow.Cells("ccSabadKala").Text.Replace(",", "")), Me.txtCodeKala.Tag)
            Else

                If Not IsValidRow("All") Then
                    Exit Sub
                End If

                Dim cnSQL As SqlConnection
                Dim cmSQL As SqlCommand
                Dim p As New SqlParameter
                Dim strSQL As String

                cnSQL = New SqlConnection(ConnectionString)
                cnSQL.Open()

                strSQL = "Sales.spSabadKala_InsertSatr"

                cmSQL = New SqlCommand(strSQL, cnSQL)
                cmSQL.CommandType = CommandType.StoredProcedure
                cmSQL.Parameters.Clear()

                p = New SqlParameter("ccSabadKala", SqlDbType.Int)
                p.Value = Val(GridEXTitr.CurrentRow.Cells("ccSabadKala").Text.Replace(",", ""))
                cmSQL.Parameters.Add(p)

                p = New SqlParameter("ccKala", SqlDbType.Int)
                p.Value = txtCodeKala.Tag
                cmSQL.Parameters.Add(p)

                p = New SqlParameter("Fee", SqlDbType.Float)
                p.Value = CType(mskFee.Text, Integer)
                cmSQL.Parameters.Add(p)

                p = New SqlParameter("Tedad", SqlDbType.Float)
                p.Value = CType(mskTedadKala.Text, Double)
                cmSQL.Parameters.Add(p)

                p = New SqlParameter("HadeAghlKharidDarFaktor", SqlDbType.Float)
                p.Value = CType(mskHadeAghalKaridDarFaktor.Text, Double)
                cmSQL.Parameters.Add(p)

                p = New SqlParameter("ccSabadKalaSatr", SqlDbType.Int)
                p.Direction = ParameterDirection.Output
                cmSQL.Parameters.Add(p)

                cmSQL.ExecuteNonQuery()

                cnSQL.Close()
                cmSQL = Nothing : cnSQL = Nothing

            End If

            ClearForm()
            Dim pos As Integer = Val(GridEXTitr.CurrentRow.Cells("ccSabadKala").Text.Replace(",", ""))
            'RefreshTitrdata("Sales.spSabadKala_SearchTitr ")
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

            Dim SabadIsUsed As Boolean = False
            If objTools.DCount("ccSabadKala", "tblFO_PishFaktorSatr", "ccSabadKala = " & Val(GridEXTitr.CurrentRow.Cells("ccSabadKala").Text.Replace(",", ""))) > 0 Then
                SabadIsUsed = True
                MsgBox("از این سبد استفاده شده است، تنها می توانید وضعیت آن را ویرایش نمایید .", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "")
            End If

            Dim cnSQL As SqlConnection
            Dim cmSQL As SqlCommand
            Dim p As New SqlParameter
            Dim strSqlTitr

            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()

            strSqlTitr = "Sales.spSabadKala_UpdateTitr "

            cmSQL = New SqlCommand(strSqlTitr, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            If SabadIsUsed = True Then
                p = New SqlParameter("NameSabadKala", SqlDbType.NVarChar, 50)
                p.Value = objTools.DLookup("NameSabadKala", "Sales.SabadKala", "ccSabadKala = " & Val(GridEXTitr.CurrentRow.Cells("ccSabadKala").Text.Replace(",", "")))
                cmSQL.Parameters.Add(p)
            Else
                p = New SqlParameter("NameSabadKala", SqlDbType.NVarChar, 50)
                p.Value = txtNameSabadKala.Text
                cmSQL.Parameters.Add(p)
            End If

            p = New SqlParameter("Faal", SqlDbType.Bit)
            p.Value = chkFaal.Checked
            cmSQL.Parameters.Add(p)

            p = New SqlParameter("ccSabadKala", SqlDbType.Int)
            p.Value = Val(GridEXTitr.CurrentRow.Cells("ccSabadKala").Text.Replace(",", ""))
            cmSQL.Parameters.Add(p)

            cmSQL.ExecuteNonQuery()

            cnSQL.Close()
            cmSQL = Nothing : cnSQL = Nothing

            ObjCode.SabteTaghirat(CodeMahalFaal, UD_Dll.Enums.GL_NoeTaghir.UpdateRecord, "Sales.SabadKala", Val(GridEXTitr.CurrentRow.Cells("ccSabadKala").Text.Replace(",", "")), Val(GridEXTitr.CurrentRow.Cells("CodeSabad").Text.Replace(",", "")), UserName + " - " + TarikhEmrooz)

            Mode = UD_Dll.Enums.GL_ModeForms.None
            Return True
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> UpdateSanad ")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> UpdateSanad ")
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


            '-----------------------------------asadi-13960722
            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()

            strSQL = "Sales.spSabadKala_InsertSabadKalaSatrMablagh "

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            p = New SqlParameter("ccsabad ", SqlDbType.Int)
            p.Value = Val(GridEXTitr.CurrentRow.Cells("ccSabadKala").Text.Replace(",", ""))
            cmSQL.Parameters.Add(p)

            p = New SqlParameter("ccSabadkalaSatr", SqlDbType.Int)
            p.Value = Val(GridEXSatr.CurrentRow.Cells("ccSabadKalasatr").Text.Replace(",", ""))
            cmSQL.Parameters.Add(p)

            p = New SqlParameter("cckala", SqlDbType.Int)
            p.Value = txtCodeKala.Tag
            cmSQL.Parameters.Add(p)

            p = New SqlParameter("fee", SqlDbType.Int)
            p.Value = Val(GridEXSatr.CurrentRow.Cells("fee").Text.Replace(",", ""))
            cmSQL.Parameters.Add(p)

            p = New SqlParameter("Tarikh", SqlDbType.NVarChar, 8)
            p.Value = TarikhEmrooz
            cmSQL.Parameters.Add(p)


            'p = New SqlParameter("ccSabadKalaMablagh", SqlDbType.Int)
            'p.Direction = ParameterDirection.Output
            'cmSQL.Parameters.Add(p)

            cmSQL.ExecuteNonQuery()

            cnSQL.Close()
            cmSQL = Nothing
            cnSQL = Nothing
            '-----------------------------------

            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()

            strSQL = "Sales.spSabadKala_UpdateSatr "

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            p = New SqlParameter("ccKala", SqlDbType.Int)
            p.Value = txtCodeKala.Tag
            cmSQL.Parameters.Add(p)

            p = New SqlParameter("Tedad", SqlDbType.Float)
            p.Value = mskTedadKala.Text
            cmSQL.Parameters.Add(p)

            p = New SqlParameter("Fee", SqlDbType.Float)
            p.Value = mskFee.Text
            cmSQL.Parameters.Add(p)

            p = New SqlParameter("HadeAghlKharidDarFaktor", SqlDbType.Float)
            p.Value = mskHadeAghalKaridDarFaktor.Text
            cmSQL.Parameters.Add(p)

            p = New SqlParameter("ccSabadKalaSatr", SqlDbType.Int)
            p.Value = Val(GridEXSatr.CurrentRow.Cells("ccSabadKalaSatr").Text.Replace(",", ""))
            cmSQL.Parameters.Add(p)

            cmSQL.ExecuteNonQuery()

            cnSQL.Close()
            cmSQL = Nothing
            cnSQL = Nothing

            ObjCode.SabteTaghirat(CodeMahalFaal, UD_Dll.Enums.GL_NoeTaghir.UpdateRecord, "Sales.SabadKalaSatr", Val(GridEXSatr.CurrentRow.Cells("ccSabadKalaSatr").Text.Replace(",", "")), Val(GridEXSatr.CurrentRow.Cells("CodeKala").Text.Replace(",", "")), UserName + " - " + TarikhEmrooz)

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


            strSQLRow = "ccSabadKala = " & Val(GridEXTitr.CurrentRow.Cells("ccSabadKala").Text.Replace(",", ""))

            'If objTools.ConvertNulls(objTools.DLookup("ccSabadKala", "Sales.SabadKalaSatr", strSQLRow), "-1") = -1 Then

            strSQLTitr = "Sales.spSabadKala_DeleteTitr "

            cmSQL = New SqlCommand(strSQLTitr, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            p = New SqlParameter("ccSabadKala", SqlDbType.Int)
            p.Value = Val(GridEXTitr.CurrentRow.Cells("ccSabadKala").Text.Replace(",", ""))
            cmSQL.Parameters.Add(p)

            cmSQL.ExecuteNonQuery()
            cnSQL.Close()
            cmSQL = Nothing : cnSQL = Nothing

            ObjCode.SabteTaghirat(CodeMahalFaal, UD_Dll.Enums.GL_NoeTaghir.DeleteRecord, "Sales.SabadKala", Val(GridEXTitr.CurrentRow.Cells("ccSabadKala").Text.Replace(",", "")), Val(GridEXTitr.CurrentRow.Cells("CodeSabad").Text.Replace(",", "")), UserName + " - " + TarikhEmrooz)

            Mode = UD_Dll.Enums.GL_ModeForms.None

            'Else
            '    MsgBox("اين رکورد دارای اطلاعات ميباشد.ابتدا آنها را حذف کنيد.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "خطاي بانک")
            'End If
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
            Dim FlgDelete As Boolean = False

            If cmSatr.Position < 0 Then
                MsgBox("هیچ سطری برای حذف وجود ندارد.", MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, "پیام")
                Exit Sub
            End If

            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()

            If objTools.ConvertNulls(Val(GridEXSatr.CurrentRow.Cells("ccSabadKala_Link").Text.Replace(",", "")), 0) <> 0 Then

                If MsgBox("کالای انتخاب شده جزئی از یک سبد دیگر است . در صورت حذف، کل سبد حذف خواهد شد. آیا حذف انجام شود ؟.", MsgBoxStyle.Question + MsgBoxStyle.YesNo + MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, "") = MsgBoxResult.Yes Then

                    strSQL = "Sales.spSabadKala_DeleteSatr_FromOtherSabad "

                    cmSQL = New SqlCommand(strSQL, cnSQL)
                    cmSQL.CommandType = CommandType.StoredProcedure
                    cmSQL.Parameters.Clear()

                    p = New SqlParameter("ccSabadKala", SqlDbType.Int)
                    p.Value = Val(GridEXTitr.CurrentRow.Cells("ccSabadKala").Text.Replace(",", ""))
                    cmSQL.Parameters.Add(p)

                    p = New SqlParameter("ccSabadKala_Link", SqlDbType.Int)
                    p.Value = Val(GridEXSatr.CurrentRow.Cells("ccSabadKala_Link").Text.Replace(",", ""))
                    cmSQL.Parameters.Add(p)

                    cmSQL.ExecuteNonQuery()

                    ObjCode.SabteTaghirat(CodeMahalFaal, UD_Dll.Enums.GL_NoeTaghir.DeleteRecord, "Sales.SabadKalaSatr", Val(GridEXSatr.CurrentRow.Cells("ccSabadKalaSatr").Text.Replace(",", "")), Val(GridEXSatr.CurrentRow.Cells("CodeKala").Text.Replace(",", "")), UserName + " - " + TarikhEmrooz + " حذف سبد")

                Else
                    Exit Sub
                End If

            Else
                strSQL = "Sales.spSabadKala_DeleteSatr "

                cmSQL = New SqlCommand(strSQL, cnSQL)
                cmSQL.CommandType = CommandType.StoredProcedure
                cmSQL.Parameters.Clear()

                p = New SqlParameter("ccSabadKalaSatr", SqlDbType.Int)
                p.Value = Val(GridEXSatr.CurrentRow.Cells("ccSabadKalaSatr").Text.Replace(",", ""))
                cmSQL.Parameters.Add(p)

                cmSQL.ExecuteNonQuery()

                ObjCode.SabteTaghirat(CodeMahalFaal, UD_Dll.Enums.GL_NoeTaghir.DeleteRecord, "Sales.SabadKalaSatr", Val(GridEXSatr.CurrentRow.Cells("ccSabadKalaSatr").Text.Replace(",", "")), Val(GridEXSatr.CurrentRow.Cells("CodeKala").Text.Replace(",", "")), UserName + " - " + TarikhEmrooz + " حذف کالا")
            End If

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

            strSQLRow = "Sales.spSabadKala_DeleteFullSatr "

            cmSQL = New SqlCommand(strSQLRow, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            p = New SqlParameter("ccSabadKala", SqlDbType.Int)
            p.Value = Val(GridEXTitr.CurrentRow.Cells("ccSabadKala").Text.Replace(",", ""))
            cmSQL.Parameters.Add(p)

            cmSQL.ExecuteNonQuery()

            strSQLTitr = "Sales.spSabadKala_DeleteTitr "

            cmSQL = New SqlCommand(strSQLTitr, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            p = New SqlParameter("ccSabadKala", SqlDbType.Int)
            p.Value = Val(GridEXTitr.CurrentRow.Cells("ccSabadKala").Text.Replace(",", ""))
            cmSQL.Parameters.Add(p)

            cmSQL.ExecuteNonQuery()

            cnSQL.Close()
            cmSQL = Nothing
            cnSQL = Nothing

            ObjCode.SabteTaghirat(CodeMahalFaal, UD_Dll.Enums.GL_NoeTaghir.DeleteRecord, "Sales.SabadKala", Val(GridEXTitr.CurrentRow.Cells("ccSabadKala").Text.Replace(",", "")), Val(GridEXTitr.CurrentRow.Cells("CodeSabad").Text.Replace(",", "")), UserName + " - " + TarikhEmrooz + " حذف کل سبد")

            Mode = UD_Dll.Enums.GL_ModeForms.None
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->DeleteTitr")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->DeleteTitr")
        End Try
    End Sub
    Private Sub InsertKalaFromSabad(ByVal ccSabadKalaTitr As Integer, ByVal ccSabadSelected As Integer)
        Try
            Dim cnSQL As New SqlConnection
            Dim cmSQL As New SqlCommand
            Dim strSQL As String = ""

            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()

            strSQL = "Sales.spSabadKala_InsertSatr_FromOtherSabad "

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            cmSQL.Parameters.AddWithValue("ccSabadKala", ccSabadKalaTitr)
            cmSQL.Parameters.AddWithValue("ccSabad", ccSabadSelected)
            cmSQL.Parameters.AddWithValue("Tedad", mskTedadKala.Text)
            cmSQL.Parameters.AddWithValue("HadeAghlKharidDarFaktor", mskHadeAghalKaridDarFaktor.Text)

            cmSQL.ExecuteNonQuery()

            cmSQL = Nothing
            cnSQL.Close()

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> InsertKalaFromSabad ")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> InsertKalaFromSabad ")
        End Try
    End Sub
    Private Sub PrintSabadKala()
        Try
            Dim cnSQL As SqlConnection
            Dim cmSQL As New SqlCommand
            Dim p As New SqlParameter
            Dim strSQL As String

            Windows.Forms.Cursor.Current = Cursors.WaitCursor
            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()

            strSQL = "Sales.spSabadKala_Print "

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            p = New SqlParameter("ccSabadKala", SqlDbType.Int)
            p.Value = Val(GridEXTitr.CurrentRow.Cells("ccSabadKala").Text.Replace(",", ""))
            cmSQL.Parameters.Add(p)

            If dsForm.Tables.Contains("tblSabadKala_Print") Then
                dsForm.Tables.Remove("tblSabadKala_Print")
            End If

            Dim daSQL As SqlDataAdapter
            daSQL = New SqlDataAdapter(cmSQL)
            daSQL.Fill(dsForm, "tblSabadKala_Print")

            Dim rpt As New ReportDocument
            Dim rpttables As Tables
            Dim rptformula As FormulaFieldDefinitions
            Dim frm As New Forms_dll.frmGL_Gozaresh

            rpt.Load(rptPath & "\rptFO_GozareshSabadKala.rpt")

            rpttables = rpt.Database.Tables
            rpttables.Item(0).SetDataSource(dsForm.Tables("tblSabadKala_Print"))

            rptformula = rpt.DataDefinition.FormulaFields
            With rptformula

                .Item("Group").Text = "{mydata.ccSabadKala}"
                .Item("NameSabadKala").Text = "{mydata.NameSabadKala}"
                .Item("NoeMoshtary").Text = "{mydata.txtNoeMoshtary}"
                .Item("Vazeiat").Text = "{mydata.txtFaal}"
                .Item("CodeKala").Text = "{mydata.CodeKala}"
                .Item("NameKala").Text = "{mydata.NameKala}"
                .Item("Tedad").Text = "{mydata.Tedad}"
                .Item("Fee").Text = "{mydata.Fee}"
                .Item("mkol").Text = "{mydata.mkol}"
                .Item("HadeAghlKharidDarFaktor").Text = "{mydata.HadeAghlKharidDarFaktor}"
                .Item("Title").Text = "'" & "سبــد کـــالا" & "'"
                .Item("Title2").Text = "'" & NameSherkat & "'"
                .Item("Title3").Text = "'" & NameMahalFaal & "'"
                .Item("KarbarGozaresh").Text = "'" & PersonelName & "'"
                .Item("TarikhGozaresh").Text = "'" & objTarikh.SetDateSlash(TarikhEmrooz) & "'"
                .Item("SaatGozaresh").Text = "'" & Format(TimeOfDay, "HH:mm:ss") & "'"
            End With
            rpt.Refresh()

            frm.Text = "سبــد کـــالا"

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

            If chkCopy.Checked = False Then

                If CheckField = "txtNameSabadKala" Or CheckField = "All" Then
                    If txtNameSabadKala.Text = "" Then
                        ErrPro.SetError(Me.txtNameSabadKala, "نام سبد کالا را وارد کنید.")
                        MsgBox("نام سبد کالا را وارد کنید.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
                        txtNameSabadKala.Focus()
                        Exit Function
                    End If
                    ErrPro.SetError(Me.txtNameSabadKala, "")
                End If

                If Mode = UD_Dll.Enums.GL_ModeForms.AddNewRecord Then
                    If objTools.ConvertNulls(objTools.DCount("NameSabadKala", "Sales.SabadKala", "NameSabadKala = '" & txtNameSabadKala.Text & "'"), 0) > 0 Then
                        MsgBox("نام مورد نظر تکـــراری است ! ", MsgBoxStyle.Information + MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, "خطا در افزودن رکورد جدید ")
                        Exit Function
                    End If

                    If objTools.ConvertNulls(objTools.DCount("CodeSabad", "Sales.SabadKala", "CodeSabad = " & mskCodeSabadKala.Text), 0) > 0 Then
                        MsgBox("کد مورد نظر تکـــراری است ! ", MsgBoxStyle.Information + MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, "خطا در افزودن رکورد جدید ")
                        Exit Function
                    End If
                ElseIf Mode = UD_Dll.Enums.GL_ModeForms.UpdateRecord Then
                    If objTools.ConvertNulls(objTools.DCount("NameSabadKala", "Sales.SabadKala", "NameSabadKala = '" & txtNameSabadKala.Text & "' AND ccSabadKala <> " & Val(GridEXTitr.CurrentRow.Cells("ccSabadKala").Text.Replace(",", ""))), 0) > 0 Then
                        MsgBox("نام مورد نظر تکـــراری است ! ", MsgBoxStyle.Information + MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, "خطا در افزودن رکورد جدید ")
                        Exit Function
                    End If

                    If objTools.ConvertNulls(objTools.DCount("CodeSabad", "Sales.SabadKala", "CodeSabad = " & mskCodeSabadKala.Text & " AND ccSabadKala <> " & Val(GridEXTitr.CurrentRow.Cells("ccSabadKala").Text.Replace(",", ""))), 0) > 0 Then
                        MsgBox("کد مورد نظر تکـــراری است ! ", MsgBoxStyle.Information + MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, "خطا در افزودن رکورد جدید ")
                        Exit Function
                    End If
                End If

                Dim Code As Integer = 0
                Code = objTools.ConvertNulls(objTools.DLookup("CodeKala", "tblAn_kala", "CodeKala=" & mskCodeSabadKala.Text), 0)
                If Code <> 0 Then
                    MsgBox("کد سبد مشابه کد کالا است ! ", MsgBoxStyle.Information + MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, "خطا در افزودن رکورد جدید ")
                    Exit Function
                End If
            Else

                If CheckField = "txtNameSabad_Copy" Or CheckField = "All" Then
                    If txtNameSabad_Copy.Text = "" Then
                        ErrPro.SetError(Me.txtNameSabad_Copy, "نام سبد کالا را وارد کنید.")
                        MsgBox("نام سبد کالا را وارد کنید.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
                        txtNameSabad_Copy.Focus()
                        Exit Function
                    End If
                    ErrPro.SetError(Me.txtNameSabad_Copy, "")
                End If

                If Mode = UD_Dll.Enums.GL_ModeForms.AddNewRecord Then
                    If objTools.ConvertNulls(objTools.DCount("NameSabadKala", "Sales.SabadKala", "NameSabadKala = '" & txtNameSabad_Copy.Text & "'"), 0) > 0 Then
                        MsgBox("نام مورد نظر تکـــراری است ! ", MsgBoxStyle.Information + MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, "خطا در افزودن رکورد جدید ")
                        Exit Function
                    End If

                    If objTools.ConvertNulls(objTools.DCount("CodeSabad", "Sales.SabadKala", "CodeSabad = " & mskCodeSabad_Copy.Text), 0) > 0 Then
                        MsgBox("کد مورد نظر تکـــراری است ! ", MsgBoxStyle.Information + MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, "خطا در افزودن رکورد جدید ")
                        Exit Function
                    End If
                End If

                Dim Code As Integer = 0
                Code = objTools.ConvertNulls(objTools.DLookup("CodeKala", "tblAn_kala", "CodeKala = " & mskCodeSabad_Copy.Text), 0)
                If Code <> 0 Then
                    MsgBox("کد سبد مشابه کد کالا است ! ", MsgBoxStyle.Information + MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, "خطا در افزودن رکورد جدید ")
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

            If chkField = "txtCodeKala" Or chkField = "All" Then
                If txtCodeKala.Text = "" Then
                    ErrPro.SetError(Me.txtCodeKala, "کــد کالا را وارد کنید.")
                    MsgBox("کــد کالا را وارد کنید.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
                    txtCodeKala.Focus()
                    Exit Function
                End If
                ErrPro.SetError(Me.txtCodeKala, "")
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

            Dim ccKala As Integer = objTools.DLookup("ccKala", "tblAN_Kala", "CodeKala = " & txtCodeKala.Text)
            If Mode = UD_Dll.Enums.GL_ModeForms.AddNewRow Then
                If objTools.ConvertNulls(objTools.DCount("ccKala", "Sales.SabadKalaSatr", "ccSabadKala = " & Val(GridEXTitr.CurrentRow.Cells("ccSabadKala").Text.Replace(",", "")) & " and ccKala = " & ccKala), 0) > 0 Then
                    MsgBox("این کالا قبلاً وارد شده است ! ", MsgBoxStyle.Information + MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, "خطا در افزودن رکورد جدید ")
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
            If IsUsed(Val(GridEXTitr.CurrentRow.Cells("ccSabadKala").Text.Replace(",", ""))) Then
                MsgBox("از این سبد کالا در فاکتور استفاده شده است . امکان ویرایش کالاهای آن وجود ندارد .", MsgBoxStyle.Information + MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " خطا ")
                Exit Sub
            End If

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
            If IsUsed(Val(GridEXTitr.CurrentRow.Cells("ccSabadKala").Text.Replace(",", ""))) Then
                MsgBox("از این سبد کالا در فاکتور استفاده شده است . امکان حذف کالاهای آن وجود ندارد .", MsgBoxStyle.Information + MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " خطا ")
                Exit Sub
            End If

            If objTools.DCount("ccSabadKalaSatr", "Sales.SabadKalaSatr", "ccSabadKala_Link = " & Val(GridEXTitr.CurrentRow.Cells("ccSabadKala").Text.Replace(",", ""))) <> 0 Then
                MsgBox(" از این سبد، در سبدی دیگر استفاده شده است . اجازه حـذف کالاهای آن را ندارید .", MsgBoxStyle.Information + MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, "")
                Exit Sub
            End If

            If MsgBox("آيا رکورد حذف شود؟", MsgBoxStyle.YesNo + MsgBoxStyle.Question + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.DefaultButton2, "حذف رکورد") = MsgBoxResult.Yes Then
                DeleteRow()
                RefreshSatrData()
                SetFormObject()
                GridEXTitr.CurrentRow.Cells("MKOL").Text = objTools.DLookup("MKOL", "Sales.SabadKala", "ccSabadKala = " & Val(GridEXTitr.CurrentRow.Cells("ccSabadKala").Text.Replace(",", "")))
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
            If IsUsed(Val(GridEXTitr.CurrentRow.Cells("ccSabadKala").Text.Replace(",", ""))) Then
                MsgBox("از این سبد کالا در فاکتور استفاده شده است . امکان حذف آن وجود ندارد .", MsgBoxStyle.Information + MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " خطا ")
                Exit Sub
            End If

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
            If IsUsed(Val(GridEXTitr.CurrentRow.Cells("ccSabadKala").Text.Replace(",", ""))) Then
                MsgBox("از این سبد کالا در فاکتور استفاده شده است . امکان حذف آن وجود ندارد .", MsgBoxStyle.Information + MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " خطا ")
                Exit Sub
            End If

            If objTools.DCount("ccSabadKalaSatr", "Sales.SabadKalaSatr", "ccSabadKala_Link = " & Val(GridEXTitr.CurrentRow.Cells("ccSabadKala").Text.Replace(",", ""))) <> 0 Then
                MsgBox(" از این سبد، در سبدی دیگر استفاده شده است . اجازه حـذف آن را ندارید .", MsgBoxStyle.Information + MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, "")
                Exit Sub
            End If

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
            'Search(True)
            'SetFormTitrData()
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
            Select Case Mode
                Case UD_Dll.Enums.GL_ModeForms.AddNewRow
                    If Not objSec.HaveUserPermission(SN, UD_Dll.Security.ePermissions.AddSatr) Then Exit Sub
                    If IsUsed(Val(GridEXTitr.CurrentRow.Cells("ccSabadKala").Text.Replace(",", ""))) Then
                        MsgBox("از این سبد کالا در فاکتور استفاده شده است . امکان ویرایش آن وجود ندارد .", MsgBoxStyle.Information + MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " خطا ")
                        Exit Sub
                    End If

                    AddNewRow()
                Case UD_Dll.Enums.GL_ModeForms.UpdateRow
                    If Not objSec.HaveUserPermission(SN, UD_Dll.Security.ePermissions.UpdateSatr) Then Exit Sub
                    UpdateRow()
            End Select
            SetGridStyle()


            GridEXTitr.CurrentRow.Cells("MKOL").Text = objTools.DLookup("MKOL", "Sales.SabadKala", "ccSabadKala = " & Val(GridEXTitr.CurrentRow.Cells("ccSabadKala").Text.Replace(",", "")))
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->btnSave_Click")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->btnSave_Click")
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

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        If Not objSec.HaveUserPermission(SN, UD_Dll.Security.ePermissions.Preview) Then Exit Sub
        Try
            If dvTitr.Count > 0 Then
                Me.TopMost = False
                PrintSabadKala()
                Me.TopMost = True
            End If
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->btnPrintM_Click")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->btnPrintM_Click")
        End Try
    End Sub
    Private Sub GridEXSatr_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GridEXSatr.Click
        BoundCurrencyManagerSatr()
    End Sub
    Private Function IsUsed(ByVal ccSabad As Integer) As Boolean
        IsUsed = False

        Dim cnSQL As New SqlConnection
        Dim cmSQL As New SqlCommand
        Dim strSQL As String = ""
        Dim TedadFaktor As Integer = 0

        Try
            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()

            strSQL = "Sales.spSabadKala_CountSabadUsedInFaktor "

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            cmSQL.Parameters.AddWithValue("ccSabadKala", ccSabad)
            cmSQL.Parameters.AddWithValue("CountFaktor", TedadFaktor)
            cmSQL.Parameters("CountFaktor").Direction = ParameterDirection.Output

            cmSQL.ExecuteNonQuery()

            TedadFaktor = cmSQL.Parameters("CountFaktor").Value

            If TedadFaktor <> 0 Then
                IsUsed = True
            End If

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> IsUsed")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> IsUsed")
        End Try
    End Function

    Private Sub chkCopy_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkCopy.CheckedChanged
        If chkCopy.Checked = False Then
            grbInsertPublic.Visible = True
            grbCopyForOtherSabad.Visible = False
        Else
            grbInsertPublic.Visible = False
            grbCopyForOtherSabad.Visible = True
        End If
    End Sub
End Class
