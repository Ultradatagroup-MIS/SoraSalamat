Imports System.Data.SqlClient

Public Class FrmFO_TolidKonandeh

#Region "Variable And Constant Declaration"
    Const cntCodeSubSystem As Long = 881
    Const FormViewName = "qryFO_TolidKonandeh"
    Const FormTableName = "tblFO_TolidKonandeh"

    Const GridAddSize = 184
    Const FormAddSize = 80
    Const FormOrgSize = 201

    Dim ErrPro As New ErrorProvider
    Dim Mode As UD_Dll.Enums.GL_ModeForms = UD_Dll.Enums.GL_ModeForms.AddNewRecord
    Dim txtCaption As String
    Private WithEvents BS As New UD_Dll.PassString
    Dim dsForm As New DataSet
    Dim cmForm As CurrencyManager
    Dim dvForm As DataView
    Private SN As Integer
    Dim flg As Boolean = False
#End Region
#Region "Form Event Code"
    Private Sub FrmFO_TolidKonandeh_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetParameter()
        SN = objSec.GetSecurityNumber(cntCodeSubSystem, UserName)
        LoadCombo()
        ClearForm()
        objTools.ChangeKbd(UD_Dll.mdlUtility.KBD.Farsi)
        flg = True
        txtNameTolidKonandeh.Focus()
    End Sub
    Private Sub frmFo_TolidKonandeh_Closed(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Closed
        ' "Receive" parameter is the caption of destination window
        Dim hwnd As Long = UD_Dll.Code.FindWindow(vbNullString, ObjCode.GetNameSherkat)
        If hwnd <> 0 Then
            BS.PostString(hwnd, &H400, 0, txtCaption)
        End If
        dsForm = Nothing
        dvForm = Nothing
    End Sub
    Private Sub cmbsKeshvar_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbsKeshvar.SelectedIndexChanged
        LoadComboOstan()
    End Sub
    Private Sub frmFo_TolidKonandeh_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles MyBase.Paint
        Me.TopMost = True
    End Sub
    Private Sub dbgTitr_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles dbgTitr.DoubleClick
        EditRecord()
    End Sub
    Private Sub FrmFO_TolidKonandeh_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.F1 Then
            Dim HelpWindow As New Forms_dll.frmGL_HelpWindow
            HelpWindow.CurrentCodeSubSystem = cntCodeSubSystem
            HelpWindow.Show()
            HelpWindow.TopMost = True
        ElseIf e.KeyCode = Keys.Enter Then
            SendKeys.Send("{tab}")
        End If
    End Sub
    Private Sub CheckIsNumeric(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles _
    txtCodeTolidKonandeh.KeyPress
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
#Region "Gloabl Form Code"
    Private Sub LoadCombo()
        Dim Strsql As String
        Dim daSQL As SqlDataAdapter

        Strsql = "Select * From tblGL_ShenasehOmomi Where CodeAsli = 25 And CodeFarei<>0 Or Code = 0 order by Sharh"
        daSQL = New SqlDataAdapter(Strsql, ConnectionString)
        daSQL.Fill(dsForm, "tbl_Keshvar")

        Strsql = "Select * From tblGL_ShenasehOmomi Where CodeAsli = 47 And CodeFarei<>0 Or Code = 0  order by Sharh"
        daSQL = New SqlDataAdapter(Strsql, ConnectionString)
        daSQL.Fill(dsForm, "tbl_Ostan")

        Strsql = "Select * From tblGL_ShenasehOmomi Where CodeAsli = 23 And CodeFarei<>0 Or Code = 0  order by Sharh"
        daSQL = New SqlDataAdapter(Strsql, ConnectionString)
        daSQL.Fill(dsForm, "tbl_Shahr")

        Dim dt1 As New DataTable
        dt1 = dsForm.Tables("tbl_Keshvar").Copy
        dt1.TableName = "tbl_KeshvarTavalod"
        dsForm.Tables.Add(dt1)

        cmbsKeshvar.DataSource = Nothing
        cmbsKeshvar.Items.Clear()
        cmbsKeshvar.DataSource = dsForm.Tables("tbl_Keshvar").DefaultView
        cmbsKeshvar.DisplayMember = "Sharh"
        cmbsKeshvar.ValueMember = "Code"
    End Sub
    Private Sub ClearForm()
        Mode = UD_Dll.Enums.GL_ModeForms.AddNewRecord
        Me.txtCodeTolidKonandeh.Text = ""
        Me.txtNameTolidKonandeh.Text = ""

        Me.cmbsKeshvar.SelectedIndex = -1
        Me.cmbsOstan.SelectedIndex = -1
        Me.cmbsShahr.SelectedIndex = -1

        ErrPro.Dispose()
        SetButtons()
        Me.txtCodeTolidKonandeh.Focus()
    End Sub
    Private Sub SetFormData()
        Mode = UD_Dll.Enums.GL_ModeForms.UpdateRecord
        DisableGrid()
        Dim dr As DataRowView
        If cmForm.Count = 0 Then ClearForm() : Exit Sub
        dr = dvForm.Item(cmForm.Position)
        txtCodeTolidKonandeh.Text = dr("CodeTolidKonandeh")
        txtNameTolidKonandeh.Text = dr("NameTolidKonandeh")
        '------------------------
        'کمبوباکسها درست کار نمیکنند 
        cmbsKeshvar.SelectedValue = dr("sKeshvar")
        LoadComboOstan()
        cmbsOstan.SelectedValue = dr("sOstan")
        LoadComboShahr()
        cmbsShahr.SelectedValue = dr("sShahr")
        '---------------------------
        SetButtons()
    End Sub
    Private Sub RefreshFormData(ByVal strsql As String)
        Dim daSQL As SqlDataAdapter
        Try
            If dsForm.Tables.Contains(FormViewName) = True Then
                dsForm.Tables.Remove(FormViewName)
            End If

            daSQL = New SqlDataAdapter(strsql, ConnectionString)
            daSQL.Fill(dsForm, FormViewName)
            dvForm = New DataView
            dvForm = dsForm.Tables(FormViewName).DefaultView
            dvForm.Sort = "NameTolidKonandeh"
            dvForm.AllowDelete = True
            dvForm.AllowEdit = False
            dvForm.AllowNew = False
            daSQL = Nothing

            SetGridStyle()

            If dvForm.Count = 1 Then
                EditRecord()
            ElseIf dvForm.Count = 0 Then

                'MsgBox("رکوردي پيدا نشد", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "پيام")
                CancelForm()
            Else
                Mode = UD_Dll.Enums.GL_ModeForms.Search

            End If

        Catch e As SqlException
            MsgBox(e.Message, MsgBoxStyle.Information, "خطا در عمليات بانک")
        Catch e As Exception
            MsgBox(e.Message, MsgBoxStyle.Information, "خطا")
        End Try
    End Sub
    Private Sub Search(ByVal WithCriteria As Boolean)
        Dim StrSql As String
        Dim WhereStr As String
        Try
            StrSql = "SELECT * FROM " & FormViewName & " Where "
            WhereStr = ""
            If WithCriteria Then
                If Me.txtNameTolidKonandeh.Text.Length > 0 Then
                    WhereStr &= " NameTolidKonandeh like '%" & Me.txtNameTolidKonandeh.Text & "%' AND "
                End If

                If Me.txtCodeTolidKonandeh.Text.Length > 0 Then
                    WhereStr &= " CodeTolidKonandeh= " & Me.txtCodeTolidKonandeh.Text & " AND "
                End If

                If cmbsKeshvar.SelectedIndex <> -1 And cmbsKeshvar.SelectedValue <> 0 Then
                    WhereStr &= " sKeshvar = " & cmbsKeshvar.SelectedValue & " AND "
                End If

                If cmbsOstan.SelectedIndex <> -1 And cmbsOstan.SelectedValue <> 0 Then
                    WhereStr &= " sOstan = " & cmbsOstan.SelectedValue & " AND "
                End If
                If cmbsShahr.SelectedIndex <> -1 And cmbsShahr.SelectedValue <> 0 Then
                    WhereStr &= " sShahr = " & cmbsShahr.SelectedValue & " AND "
                End If

                If Len(WhereStr) = 0 Then
                    MsgBox("شرط جستجو را وارد کنيد", MsgBoxStyle.OkOnly + MsgBoxStyle.Information + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, "جستجو")
                    Exit Sub
                End If
            End If
            StrSql = StrSql & WhereStr
            StrSql = StrSql & " ccTolidKonandeh <> 0 "
            RefreshFormData(StrSql)
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "Error IN DataBase ---->" & "search")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, "Error IN ---->" & "search")
        End Try
    End Sub
    Private Sub SetGridStyle()
        Dim TextCol As DataGridTextBoxColumn
        Dim tableStyle As New DataGridTableStyle
        tableStyle.MappingName = FormViewName
        dbgTitr.CaptionText = "تولید کننده"
        dbgTitr.BorderStyle = BorderStyle.Fixed3D

        TextCol = New DataGridTextBoxColumn
        With TextCol
            .MappingName = "NameTolidKonandeh"
            .HeaderText = "نام تولید کننده"
            .Width = 140
            .Alignment = HorizontalAlignment.Left
        End With
        tableStyle.GridColumnStyles.Add(TextCol)

        TextCol = New DataGridTextBoxColumn
        With TextCol
            .MappingName = "CodeTolidKonandeh"
            .HeaderText = "کد تولید کننده "
            .Width = 120
            .Alignment = HorizontalAlignment.Left
        End With
        tableStyle.GridColumnStyles.Add(TextCol)

        TextCol = New DataGridTextBoxColumn
        With TextCol
            .MappingName = "TxtKeshvar"
            .HeaderText = "نام کشور "
            .Width = 140
            .Alignment = HorizontalAlignment.Left
        End With
        tableStyle.GridColumnStyles.Add(TextCol)
        TextCol = New DataGridTextBoxColumn
        With TextCol
            .MappingName = "TxtOstan"
            .HeaderText = "نام استان "
            .Width = 140
            .Alignment = HorizontalAlignment.Left
        End With
        tableStyle.GridColumnStyles.Add(TextCol)

        TextCol = New DataGridTextBoxColumn
        With TextCol
            .MappingName = "TxtShahr"
            .HeaderText = "نام شهر "
            .Width = 140
            .Alignment = HorizontalAlignment.Left
        End With
        tableStyle.GridColumnStyles.Add(TextCol)
        With dbgTitr
            .TableStyles.Clear()
            .TableStyles.Add(tableStyle)
            .Visible = True
            .DataSource = dvForm
            .Height = .Height + GridAddSize
        End With

        If dvForm.Count > 1 Then
            Me.Height = Me.Height + FormAddSize
        End If
        BoundCurrencyManager()
        SetButtons()
        Me.CenterToScreen()
    End Sub
    Private Sub BoundCurrencyManager()
        If dvForm.Count > 1 Then Mode = UD_Dll.Enums.GL_ModeForms.Search
        If dvForm.Count = 1 Then Mode = UD_Dll.Enums.GL_ModeForms.UpdateRecord
        If Mode = UD_Dll.Enums.GL_ModeForms.UpdateRecord Or Mode = UD_Dll.Enums.GL_ModeForms.Search Then
            cmForm = CType(BindingContext(dbgTitr.DataSource), CurrencyManager)
            AddHandler cmForm.ItemChanged, AddressOf cmForm_ItemChanged
            AddHandler cmForm.PositionChanged, AddressOf cmForm_PositionChanged
            DisplayPosition()
        End If
    End Sub
    Private Sub DisplayPosition()
        If Mode = UD_Dll.Enums.GL_ModeForms.UpdateRecord Or Mode = UD_Dll.Enums.GL_ModeForms.Search Then
            dbgTitr.CaptionText = txtCaption & "  رکورد " & cmForm.Position + 1 & " از " & cmForm.Count
        Else
            dbgTitr.CaptionText = txtCaption
        End If
        Dim TedadKol As Long
        Dim StrCaption As String
        TedadKol = objTools.DCount("*", FormViewName, "ccTolidKonandeh <> 0")
        StrCaption = "                            تعداد کل رکورد : " & TedadKol
        dbgTitr.CaptionText = dbgTitr.CaptionText & StrCaption
    End Sub
    Private Sub cmForm_ItemChanged(ByVal sender As Object, ByVal e As ItemChangedEventArgs)
        DisplayPosition()
    End Sub
    Private Sub cmForm_PositionChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        DisplayPosition()
    End Sub
    Private Sub DisableGrid()
        dbgTitr.Visible = False
        Me.Height = FormOrgSize
        dbgTitr.Height = dbgTitr.Height - GridAddSize
        Me.CenterToScreen()
    End Sub
    Private Sub CancelForm()
        ClearForm()
        DisableGrid()
    End Sub
    Private Sub EditRecord()
        SetFormData()
    End Sub
    Private Function IsValidForm(ByVal CheckField As String) As Boolean
        IsValidForm = False

        If CheckField = "txtNameTamin" Or CheckField = "All" Then
            If Me.txtNameTolidKonandeh.Text = "" Then
                ErrPro.SetError(Me.txtNameTolidKonandeh, "نام تولید کننده را وارد نمایید")
                MsgBox("نام تولید کننده را وارد نمایید", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
                txtNameTolidKonandeh.Focus()
                Exit Function
            End If
            ErrPro.SetError(Me.txtNameTolidKonandeh, "")
        End If

        If CheckField = "txtCodeTamin" Or CheckField = "All" Then
            If Me.txtCodeTolidKonandeh.Text = "" Then
                ErrPro.SetError(Me.txtCodeTolidKonandeh, "کد تولید کننده را وارد نمایید")
                MsgBox("کد تولید کننده را وارد نمایید", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
                txtCodeTolidKonandeh.Focus()
                Exit Function
            End If
            ErrPro.SetError(Me.txtCodeTolidKonandeh, "")
        End If

        'If CheckField = "cmbKeshvar" Or CheckField = "All" Then
        '    If cmbsKeshvar.SelectedIndex = -1 And cmbsKeshvar.SelectedValue = 0 Then
        '        ErrPro.SetError(Me.cmbsKeshvar, "کشور را وارد کنيد.")
        '        MsgBox("کشور را وارد کنيد.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
        '        cmbsKeshvar.Focus()
        '        Exit Function
        '    End If
        '    ErrPro.SetError(Me.cmbsKeshvar, "")
        'End If

        'If CheckField = "cmbOstan" Or CheckField = "All" Then
        '    If cmbsOstan.SelectedIndex = -1 And cmbsOstan.SelectedValue = 0 Then
        '        ErrPro.SetError(Me.cmbsOstan, "استان را وارد کنيد.")
        '        MsgBox("استان را وارد کنيد.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
        '        cmbsOstan.Focus()
        '        Exit Function
        '    End If
        '    ErrPro.SetError(Me.cmbsOstan, "")
        'End If

        'If CheckField = "cmbShahr" Or CheckField = "All" Then
        '    If cmbsShahr.SelectedIndex = -1 And cmbsShahr.SelectedValue = 0 Then
        '        ErrPro.SetError(Me.cmbsShahr, "شهر را وارد کنيد.")
        '        MsgBox("شهر را وارد کنيد.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
        '        cmbsShahr.Focus()
        '        Exit Function
        '    End If
        '    ErrPro.SetError(Me.cmbsShahr, "")
        'End If
        Return True
    End Function
    Private Sub AddNewRecord()

        If Not IsValidForm("All") Then
            Exit Sub
        End If

        Dim cnSQL As SqlConnection
        Dim cmSQL As SqlCommand
        Dim strSQL As String

        Try
            strSQL = "INSERT into " & FormTableName
            strSQL &= "(CodeTolidKonandeh, NameTolidKonandeh, sKeshvar, sOstan, sShahr,  "
            strSQL &= " UserName, Tarikh, Saat)"
            strSQL &= " VALUES ('" & Me.txtCodeTolidKonandeh.Text & "', '" & Me.txtNameTolidKonandeh.Text & "',"
            strSQL &= IIf(IsNothing(cmbsKeshvar.SelectedValue), 0, cmbsKeshvar.SelectedValue) & "," 'sKeshvar
            strSQL &= IIf(IsNothing(cmbsOstan.SelectedValue), 0, cmbsOstan.SelectedValue) & "," 'sOstan
            strSQL &= IIf(IsNothing(cmbsShahr.SelectedValue), 0, cmbsShahr.SelectedValue) & "," 'sShahr
            strSQL &= "'" & UserName & "',"
            strSQL &= "'" & TarikhEmrooz & "',"
            strSQL &= "'" & Format(TimeOfDay, "HH:mm:ss") & "')"

            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()
            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.ExecuteNonQuery()

            cnSQL.Close()
            cmSQL = Nothing : cnSQL = Nothing
            ClearForm()
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
    Private Sub UpdateRecord()

        If Not IsValidForm("All") Then
            Exit Sub
        End If

        Dim cnSQL As SqlConnection
        Dim cmSQL As SqlCommand
        Dim strSQL As String

        Try
            Dim dr As DataRow
            dr = dvForm.Item(cmForm.Position).Row

            ObjCode.SabteTaghirat(CodeMahalFaal, UD_Dll.Enums.GL_NoeTaghir.UpdateRecord, "tblFO_TolidKonandeh", dr("ccTolidKonandeh"), dr("ccTolidKonandeh"), "بروز رساني ")

            strSQL = "Update " & FormTableName & " Set "
            strSQL &= "CodeTolidKonandeh= '" & txtCodeTolidKonandeh.Text & "',"
            strSQL &= "NameTolidKonandeh = '" & txtNameTolidKonandeh.Text & "',"
            strSQL &= "sKeshvar= " & IIf(IsNothing(cmbsKeshvar.SelectedValue), 0, cmbsKeshvar.SelectedValue) & ","
            strSQL &= "sOstan= " & IIf(IsNothing(cmbsOstan.SelectedValue), 0, cmbsOstan.SelectedValue) & ","
            strSQL &= "sShahr= " & IIf(IsNothing(cmbsShahr.SelectedValue), 0, cmbsShahr.SelectedValue)

            strSQL &= " Where  ccTolidKonandeh = " & dr("ccTolidKonandeh")

            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()
            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.ExecuteNonQuery()
            cnSQL.Close()
            cmSQL = Nothing : cnSQL = Nothing

            ClearForm()
        Catch sqlExc As SqlException

            If (sqlExc.Number = 229) Then
                If Microsoft.VisualBasic.Left(sqlExc.Message, 1) = "U" Then
                    MsgBox("خطا در اصلاح رکورد,اصلاح انجام نشد", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "خطاي بانک")
                End If
            Else
                MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "خطاي بانک")
            End If
        Catch e As Exception
            MsgBox(e.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "خطا")
        End Try
    End Sub
    Private Sub DeleteRecord()
        Dim cnSQL As SqlConnection
        Dim cmSQL As SqlCommand
        Dim strSQL As String
        Dim intRowsAffected As Integer

        Try
            Dim dr As DataRow
            If cmForm.Position = -1 Then Exit Sub
            dr = dvForm.Item(cmForm.Position).Row

            strSQL = "DELETE FROM " & FormTableName & " WHERE ccTolidKonandeh = " & dr("ccTolidKonandeh")
            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()
            cmSQL = New SqlCommand(strSQL, cnSQL)
            intRowsAffected = cmSQL.ExecuteNonQuery()
            If intRowsAffected < 1 Then ' changed by asha 
                MsgBox("عمليات حذف رکورد با موفقيت انجام نشد.رکورد پيدا نشد", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "حذف رکورد")
            End If

            ' Close and Clean up objects
            cnSQL.Close()
            cmSQL = Nothing : cnSQL = Nothing

            If Mode <> UD_Dll.Enums.GL_ModeForms.Search Then
                ClearForm()
            Else
                dvForm.Item(cmForm.Position).Delete()
            End If
        Catch sqlExc As SqlException
            Select Case sqlExc.Number
                Case 229
                    If Microsoft.VisualBasic.Left(sqlExc.Message, 1) = "ِD" Then
                        MsgBox("خطا در حذف رکورد,حذف انجام نشد", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "خطاي بانک")
                    Else
                        MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "خطا")
                    End If
                Case 547
                    MsgBox("ويژگيهاي مربوط به اين شخص تعريف شده است ابتدا اين مشخصه ها را حذف کنيد", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "خطاي بانک")
                Case Else
                    MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "خطاي بانک")
            End Select
        Catch e As Exception
            MsgBox(e.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "خطا")
        End Try
    End Sub
    Private Sub SetButtons()
        Me.btnDelete.Enabled = False
        Me.btnSearch.Enabled = False
        Me.btnCancel.Enabled = False
        Me.btnExit.Enabled = True
        Me.btnRefresh.Enabled = False
        If Mode = UD_Dll.Enums.GL_ModeForms.AddNewRecord Then
            Me.btnUpdate.Enabled = True
            Me.btnSearch.Enabled = True
            Me.btnCancel.Enabled = True
            Me.btnRefresh.Enabled = True
        ElseIf Mode = UD_Dll.Enums.GL_ModeForms.UpdateRecord Then
            Me.btnUpdate.Enabled = True
            Me.btnDelete.Enabled = True
            Me.btnCancel.Enabled = True
        ElseIf Mode = UD_Dll.Enums.GL_ModeForms.Search Then
            Me.btnDelete.Enabled = True
            Me.btnCancel.Enabled = True
        End If
    End Sub
    Private Sub cmbsOstan_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbsOstan.SelectedIndexChanged
        Try
            LoadComboShahr()
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "Error IN DataBase ---->" & "cmbSOstanTavalod_Validated")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, "Error IN ---->" & "cmbSOstanTavalod_Validated")
        End Try
    End Sub
    Private Sub LoadComboOstan()
        If flg = False Then Exit Sub
        Try
            flg = False
            Dim dvOstan As New DataView(dsForm.Tables("tbl_Ostan"), "CodeLink = " & IIf(IsNothing(cmbsKeshvar.SelectedValue), -1, cmbsKeshvar.SelectedValue), "", DataViewRowState.OriginalRows)
            cmbsOstan.DataSource = Nothing
            cmbsOstan.Items.Clear()
            cmbsOstan.DataSource = dvOstan
            cmbsOstan.DisplayMember = "Sharh"
            cmbsOstan.ValueMember = "Code"
            cmbsShahr.SelectedIndex = -1
            flg = True
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "Error IN DataBase ---->" & "LoadComboOstanTavalod")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, "Error IN ---->" & "LoadComboOstanTavalod")
        End Try
    End Sub
    Private Sub LoadComboShahr()
        If flg = False Then Exit Sub
        Try
            flg = False
            Dim dvShahr As New DataView(dsForm.Tables("tbl_Shahr"), "CodeLink = " & IIf(IsNothing(cmbsOstan.SelectedValue), -1, cmbsOstan.SelectedValue), "", DataViewRowState.OriginalRows)
            cmbsShahr.DataSource = Nothing
            cmbsShahr.Items.Clear()
            cmbsShahr.DataSource = dvShahr
            cmbsShahr.DisplayMember = "Sharh"
            cmbsShahr.ValueMember = "Code"
            flg = True
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "Error IN DataBase ---->" & "LoadComboMahalSodor")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, "Error IN ---->" & "LoadComboMahalSodor")
        End Try
    End Sub
    Private Sub SetParameter()

        Dim commands As String = Microsoft.VisualBasic.Command()
        If commands.Length = 0 Then

            UserName = "Administrator"
            UserPassWord = "66998833"
            NameMahalFaal = "ÊåÑÇä"
            CodeMahalFaal = "1"
            PersonelCode = "0"
            PersonelName = "Administrator"
            CodeDoreh = "1388"

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
#Region "Form Buttons"
    Private Sub btnUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdate.Click
        If Mode = UD_Dll.Enums.GL_ModeForms.AddNewRecord Then
            If Not objSec.HaveUserPermission(SN, UD_Dll.Security.ePermissions.Add) Then Exit Sub
            AddNewRecord()
        ElseIf Mode = UD_Dll.Enums.GL_ModeForms.UpdateRecord Then
            If Not objSec.HaveUserPermission(SN, UD_Dll.Security.ePermissions.Update) Then Exit Sub
            UpdateRecord()
        End If
    End Sub
    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub
    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Search(True)
    End Sub
    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        CancelForm()
    End Sub
    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Dim dr As DataRowView
        dr = dvForm.Item(cmForm.Position)
        If MsgBox("آيا رکورد حذف شود؟", MsgBoxStyle.YesNo + MsgBoxStyle.Question + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.DefaultButton2, "حذف رکورد") = MsgBoxResult.Yes Then
            DeleteRecord()
        End If
    End Sub
    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        If Not objSec.HaveUserPermission(SN, UD_Dll.Security.ePermissions.Delete) Then Exit Sub
        Search(False)
    End Sub
#End Region

End Class