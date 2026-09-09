
Imports System.Data.SqlClient
Imports System.Data.SqlClient.SqlConnection
Imports System.Data.Common
Imports System.Data

Public Class MinMaxSefaresh

#Region "variable And constant Declration"
    Private objCode As New UD_Dll.Code
    Const FormAddSize = 15
    Const FormAddWidthSize = 70
    Const FormOrgSize = 15
    Const FormOrgWidthSize = 70
    Const GridAddSize = 120
    Const GridAddWidthSize = 70

    Const FormTableName = "tblAN_MinMaxSefaresh"

    Private _CodeCounters As String = ""
    Dim dvForm As DataView
    Dim dvTitr As DataView
    Dim dsForm As New DataSet
    Dim cmtitrform As CurrencyManager
    'Public ccTakhfifJayzehTakhsis As Integer

    Dim ErrPro As New ErrorProvider
    Dim Mode As UD_Dll.Enums.GL_ModeForms = UD_Dll.Enums.GL_ModeForms.AddNewRecord
    Dim cmForm As CurrencyManager
    Private SN As Integer
    Const cntCodeSubSystem As Long = 885
    Dim txtCaption As String
    Private WithEvents BS As New UD_Dll.PassString
    Dim flg As Boolean = False
    Dim flgCancel As Boolean = True
    Dim flgSearch As Boolean = False
    Dim a As Integer

#End Region
#Region "Form Event Code"
    Private Sub GridEX1_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GridEX1.DoubleClick

        EditRecord()
        GridEX1.Visible = False
        GridEX1.Height = GridEX1.Height - GridAddSize

        Mode = UD_Dll.Enums.GL_ModeForms.UpdateRecord


        SetButton()

    End Sub
    Private Sub GridEX1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim cm As New SqlCommand

        Try
            cmForm = CType(BindingContext(GridEX1.DataSource), CurrencyManager)
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->BoundCurrencyManagerTitr")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->BoundCurrencyManagerTitr")
        End Try
    End Sub
    Private Sub GridEX1_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GridEX1.Click
        Dim cm As New SqlCommand

        Try
            cmForm = CType(BindingContext(GridEX1.DataSource), CurrencyManager)
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->BoundCurrencyManagerTitr")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->BoundCurrencyManagerTitr")
        End Try
    End Sub
    Private Sub MinMaxSefaresh_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetParameter()
        SN = objSec.GetSecurityNumber(cntCodeSubSystem, UserName)
        LoadCombo()
        flg = True
        ClearForm()
        objTools.ChangeKbd(UD_Dll.mdlUtility.KBD.Farsi)
    End Sub
    Private Sub txtCodeKala_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCodeKala.TextChanged
        If Mode = UD_Dll.Enums.GL_ModeForms.UpdateRecord Then Exit Sub
        If Trim(Me.txtCodeKala.Text) = "" Then Exit Sub
        If Not flg Then Exit Sub


        Me.lblNameKala.Text = objTools.ConvertNulls(objTools.DLookup("NameKala", "tblAN_Kala", "CodeKala='" & Me.txtCodeKala.Text & "'"), "")
        Me.txtCodeKala.Tag = objTools.ConvertNulls(objTools.DLookup("ccKala", "tblAn_Kala", "CodeKala='" & Me.txtCodeKala.Text & "'"), 0)


        
      
    End Sub
    Private Sub txtCodeKala_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCodeKala.KeyPress
        Try
            If (Asc(e.KeyChar()) < 48 Or Asc(e.KeyChar()) > 57) And (Asc(e.KeyChar()) <> 8) Then
                e.Handled = True
            End If
            If e.KeyChar = Chr(Keys.Space) Then
                Dim objKala As New Forms_dll.frmAN_KalaSearch
                Dim StrSql As String

                StrSql = "Select  CodeKala,NameKala,ccKala,txtsVahedeShomaresh,sVahedeShomaresh,NameBrand,Radif from qryAN_Kala Where Faal=1"

                MultiSelection = False
                SearchItem = "CodeKala"
                objKala.SetForm(StrSql)
                objKala.ShowDialog()

                'If txtCodeKala.Text.Length <> 0 Then
                '    objKala.tcodeKala = txtCodeKala.Text
                'End If

                Me.txtCodeKala.Tag = objKala.tccKala
                Me.txtCodeKala.Text = objKala.tcodeKala
                Me.lblNameKala.Text = objKala.tNameKala

                MultiSelection = False
            End If
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->txtCodeMoshtaryS_KeyPress")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->txtCodeMoshtaryS_KeyPress")
        End Try

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
            CodeDoreh = "1388"
            txtCaption = "جوایزروی کالا"
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
    Private Sub LoadCombo()
        Dim Strsql As String
        Dim daSQL As New SqlDataAdapter
        Try

            ' Load Combo tblAnbar
            Strsql = "Select codeAnbar,NameAnbar From qryAN_Anbar where CodeMahal=" & CodeMahalFaal & " AND Faal=1  AND "
            Strsql &= " Not exists (Select * From tblGL_SecurityData where namekarbar= '" & UserName & "' and CodeSubSystem = 633 and pk = qryAN_Anbar.CodeAnbar) "
            Strsql &= " order by NameAnbar "
            daSQL = New SqlDataAdapter(Strsql, ConnectionString)
            daSQL.Fill(dsForm, "tblAnbar")
            cmbNameAnbar.DataSource = Nothing
            cmbNameAnbar.Items.Clear()
            cmbNameAnbar.DataSource = dsForm.Tables("tblAnbar").DefaultView
            cmbNameAnbar.DisplayMember = "NameAnbar"
            cmbNameAnbar.ValueMember = "codeAnbar"

            daSQL = Nothing

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "Error IN DataBase ---->" & "LoadCombo")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, "Error IN ---->" & "LoadCombo")
        End Try
    End Sub
    Private Sub ClearForm()

        Mode = UD_Dll.Enums.GL_ModeForms.AddNewRecord

        cmbNameAnbar.SelectedIndex = -1
        txtCodeKala.Text = ""
        lblNameKala.Text = ""
        txtMin.Text = ""
        txtMax.Text = ""
        txtSefaresh.Text = ""

        ErrPro.Dispose()

    End Sub
    Private Sub AddNewRecord()
        Try
            If Not IsValidForm("All") Then
                Exit Sub
            End If

            Dim cnSQL As New SqlConnection
            Dim cmSQL As New SqlCommand
            Dim strSQL As String
            Dim P As New SqlParameter

            strSQL = "WareHouse.spMinMaxSefaresh_Insert"

            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()
            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            cmSQL.Parameters.AddWithValue("ccAnbar", cmbNameAnbar.SelectedValue)
            cmSQL.Parameters.AddWithValue("ccKala", txtCodeKala.Tag)
            cmSQL.Parameters.AddWithValue("Min", Val(txtMin.Text))
            cmSQL.Parameters.AddWithValue("Max", Val(txtMax.Text))
            cmSQL.Parameters.AddWithValue("Sefaresh", Val(txtSefaresh.Text))
            cmSQL.Parameters.AddWithValue("ccMinMax", ParameterDirection.Output)

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
        Try
            If Not IsValidForm("All") Then
                Exit Sub
            End If
            Dim cnSQL As New SqlConnection
            Dim cmSQL As New SqlCommand
            Dim strSQL As String
            Dim P As New SqlParameter
            Dim dr As DataRow
            dr = dvForm.Item(cmForm.Position).Row

            Me.txtCodeKala.Tag = objTools.ConvertNulls(objTools.DLookup("ccKala", "tblAn_Kala", "CodeKala='" & Me.txtCodeKala.Text & "'" & ""), 0)

            objCode.SabteTaghirat(CodeMahalFaal, UD_Dll.Enums.GL_NoeTaghir.UpdateRecord, "tblAN_MinMaxSefaresh", dr("ccMinMax"), dr("ccMinMax"), "بروز رساني ")


            strSQL = "WareHouse.spMinMaxSefaresh_Update"


            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()
            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            cmSQL.Parameters.AddWithValue("ccAnbar", cmbNameAnbar.SelectedValue)
            cmSQL.Parameters.AddWithValue("ccKala", txtCodeKala.Tag)
            cmSQL.Parameters.AddWithValue("Min", txtMin.Text.Trim)
            cmSQL.Parameters.AddWithValue("Max", txtMax.Text.Trim)
            cmSQL.Parameters.AddWithValue("Sefaresh", txtSefaresh.Text.Trim)
            cmSQL.Parameters.AddWithValue("ccMinMax", dr("ccMinMax"))

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
    Private Function IsValidForm(ByVal CheckField As String) As Boolean
        IsValidForm = False

        If Me.cmbNameAnbar.Text.Length = 0 Then
            MsgBox("نام انبار را وارد نمایید", MsgBoxStyle.Critical, "خطا")
            Me.cmbNameAnbar.Focus()
            Me.cmbNameAnbar.SelectAll()
            Exit Function
        End If


        If txtCodeKala.Text.Length = 0 Then
            MsgBox("کد کالا را وارد کنید", MsgBoxStyle.Critical, "خطا")
            Me.txtCodeKala.Focus()
            Me.txtCodeKala.SelectAll()
            Exit Function
        End If

        'If txtMin.Text.Length = 0 Then
        '    MsgBox(" مینیمم را وارد نمایید", MsgBoxStyle.Critical, "خطا")
        '    Me.txtMin.Focus()
        '    Me.txtMin.SelectAll()
        '    Exit Function
        'End If
        'If txtMax.Text.Length = 0 Then
        '    MsgBox("ماکزیمم را وارد نمایید", MsgBoxStyle.Critical, "خطا")
        '    Me.txtMax.Focus()
        '    Me.txtMax.SelectAll()
        '    Exit Function
        'End If
        'If txtSefaresh.Text.Length = 0 Then
        '    MsgBox(" نقطه سفارش را وارد نمایید", MsgBoxStyle.Critical, "خطا")
        '    Me.txtSefaresh.Focus()
        '    Me.txtSefaresh.SelectAll()
        '    Exit Function
        'End If
        IsValidForm = True
    End Function
    Private Sub CancelForm()
        ClearForm()
    End Sub
    Private Sub SetButton()
        Me.btnSave.Enabled = False
        Me.btnDelete.Enabled = False
        '  Me.btnPrint.Enabled = False
        '   Me.btnSearch.Enabled = False
        Me.btnCancel.Enabled = False
        Me.btnExit.Enabled = True
        Me.btnEdit.Enabled = False

        If Mode = UD_Dll.Enums.GL_ModeForms.AddNewRecord Then
            Me.btnSave.Enabled = True
            '    Me.btnSearch.Enabled = True
            Me.btnDelete.Enabled = True
            Me.btnCancel.Enabled = True
            Me.btnEdit.Enabled = True
            If flg = True Then
                Me.GridEX1.Visible = False
            End If
        ElseIf Mode = UD_Dll.Enums.GL_ModeForms.None Then
            Me.btnSave.Enabled = True
            '  Me.btnSearch.Enabled = True
            Me.btnDelete.Enabled = True
            Me.btnCancel.Enabled = True
            Me.btnEdit.Enabled = True
            Me.GridEX1.Visible = False
        ElseIf Mode = UD_Dll.Enums.GL_ModeForms.Delete Then
            Me.btnSave.Enabled = True
            '  Me.btnSearch.Enabled = True
            Me.btnDelete.Enabled = True
            Me.btnCancel.Enabled = True
            Me.btnEdit.Enabled = True
            If flg = True Then
                Me.GridEX1.Visible = False
            End If

        ElseIf Mode = UD_Dll.Enums.GL_ModeForms.UpdateRecord Then
            Me.btnSave.Enabled = True
            Me.btnDelete.Enabled = True
            '    Me.btnPrint.Enabled = True
            Me.btnCancel.Enabled = True
            Me.GridEX1.Visible = False
        ElseIf Mode = UD_Dll.Enums.GL_ModeForms.Search Then
            Me.btnSave.Enabled = True
            Me.btnDelete.Enabled = True
            Me.btnCancel.Enabled = True
            '  Me.btnSearch.Enabled = True
            Me.btnEdit.Enabled = True
            Me.GridEX1.Visible = False
        End If
    End Sub
    Private Sub EditRecord()
        SetFormData()
    End Sub
    Private Sub DeleteRecord()
        Try
            Mode = UD_Dll.Enums.GL_ModeForms.Delete
            Dim cnSQL As SqlConnection
            Dim cmSQL As SqlCommand
            Dim strSQL As String
            Dim P As New SqlParameter
            Dim dr As DataRow
            dr = dvForm.Item(cmForm.Position).Row


            strSQL = "WareHouse.spMinMaxSefaresh_Delete"

            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()
            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            cmSQL.Parameters.AddWithValue("ccMinMax", dr("ccMinMax"))

            cmSQL.ExecuteNonQuery()
            cnSQL.Close()
            cmSQL = Nothing : cnSQL = Nothing

            Search(False, "")

        Catch sqlExc As SqlException
            Select Case sqlExc.Number
                Case 229
                    If Microsoft.VisualBasic.Left(sqlExc.Message, 1) = "ِD" Then
                        MsgBox(".خطا در حذف رکورد,حذف انجام نشد ", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "خطاي بانک")
                    End If
                Case 547
                    MsgBox(".براي اين رکورد اطلاعات ديگري وجود دارد.ابتدا آنها را حذف کنيد ", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "خطاي بانک")
                Case Else
                    MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "خطاي بانک")
            End Select
        Catch e As Exception
            MsgBox(e.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "خطا")
        End Try
    End Sub
    Private Sub Search(ByVal WithCriteria As Boolean, ByVal FldName As String)
        Dim cnSQl As New SqlConnection
        Dim cmSQL As New SqlCommand
        Dim P As New SqlParameter
        Dim StrSql As String

        Try

            StrSql = "WareHouse.spMinMaxSefaresh_Search"
            RefreshFormData(StrSql)

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "Error IN DataBase ---->" & "search")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, "Error IN ---->" & "search")
        End Try
        flgSearch = True
    End Sub
    Private Sub RefreshFormData(ByVal strsql As String)
        Dim daSQL As SqlDataAdapter
        Dim cnSQL As New SqlConnection
        Dim cmSql As New SqlCommand
        Dim p As New SqlParameter

        Try
            If dsForm.Tables.Contains(FormTableName) = True Then
                dsForm.Tables.Remove(FormTableName)
            End If

            cnSQL.ConnectionString = ConnectionString
            cnSQL.Open()
            cmSql = New SqlCommand(strsql, cnSQL)
            cmSql.CommandType = CommandType.StoredProcedure
            cmSql.Parameters.Clear()

            daSQL = New SqlDataAdapter(cmSql)
            daSQL.Fill(dsForm, FormTableName)
            dvForm = New DataView
            dvForm = dsForm.Tables(FormTableName).DefaultView
            dvForm.Sort = "ccMinMax"
            dvForm.AllowDelete = True
            dvForm.AllowEdit = False
            dvForm.AllowNew = False
            daSQL = Nothing

            SetGridStyle()
            If Mode <> UD_Dll.Enums.GL_ModeForms.Delete Then
                If flg = True Then
                    If dvForm.Count = 1 Then
                        EditRecord()
                    ElseIf dvForm.Count = 0 Then
                        'MsgBox("رکوردي پيدا نشد", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "پيام")
                        CancelForm()
                    Else
                        Mode = UD_Dll.Enums.GL_ModeForms.Search
                    End If


                End If
            End If

            cnSQL.Close()
            cnSQL = Nothing
            cmSql = Nothing


        Catch e As SqlException
            MsgBox(e.Message, MsgBoxStyle.Information, "خطا در عمليات بانک")
        Catch e As Exception
            MsgBox(e.Message, MsgBoxStyle.Information, "خطا")
        End Try
    End Sub
    Private Sub SetGridStyle()

        GridEX1.Visible = True
        With GridEX1
            GridEX1.DataSource = Nothing
            GridEX1.DataSource = dsForm.Tables(FormTableName).DefaultView
            GridEX1.SetDataBinding(dsForm.Tables(FormTableName).DefaultView, "")
            GridEX1.RetrieveStructure()
        End With

        For i As Integer = 0 To GridEX1.CurrentTable.Columns.Count - 1
            GridEX1.CurrentTable.Columns.Item(i).Visible = False
        Next


        GridEX1.CurrentTable.Columns.Item("NameAnbar").Caption = "نام انبار"
        GridEX1.CurrentTable.Columns.Item("NameAnbar").Visible = True
        GridEX1.CurrentTable.Columns.Item("NameAnbar").Width = 100
        GridEX1.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
        GridEX1.CurrentTable.Columns.Item("NameAnbar").Position = 0

        GridEX1.CurrentTable.Columns.Item("CodeKala").Caption = "کد کالا"
        GridEX1.CurrentTable.Columns.Item("CodeKala").Visible = True
        GridEX1.CurrentTable.Columns.Item("CodeKala").Width = 80
        GridEX1.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
        GridEX1.CurrentTable.Columns.Item("CodeKala").Position = 1

        GridEX1.CurrentTable.Columns.Item("Min").Caption = "نقطه مینیمم"
        GridEX1.CurrentTable.Columns.Item("Min").Visible = True
        GridEX1.CurrentTable.Columns.Item("Min").Width = 80
        GridEX1.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
        GridEX1.CurrentTable.Columns.Item("Min").Position = 2

        GridEX1.CurrentTable.Columns.Item("Max").Caption = "نقطه ماکزیمم"
        GridEX1.CurrentTable.Columns.Item("Max").Visible = True
        GridEX1.CurrentTable.Columns.Item("Max").Width = 80
        GridEX1.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
        GridEX1.CurrentTable.Columns.Item("Max").Position = 3

        GridEX1.CurrentTable.Columns.Item("Sefaresh").Caption = "نقطه سفارش"
        GridEX1.CurrentTable.Columns.Item("Sefaresh").Visible = True
        GridEX1.CurrentTable.Columns.Item("Sefaresh").Width = 100
        GridEX1.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
        GridEX1.CurrentTable.Columns.Item("Sefaresh").Position = 4

        For i As Integer = 0 To GridEX1.RootTable.Columns.Count - 1
            If GridEX1.RootTable.Columns(i).Type.IsValueType Then
                GridEX1.RootTable.Columns(i).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                GridEX1.RootTable.Columns(i).FormatMode = Janus.Windows.GridEX.FormatMode.UseIFormattable
                GridEX1.RootTable.Columns(i).FormatString = "N"
                GridEX1.RootTable.Columns(i).TotalFormatMode = Janus.Windows.GridEX.FormatMode.UseIFormattable
                GridEX1.RootTable.Columns(i).TotalFormatString = "N"
            End If
        Next
        If Mode <> UD_Dll.Enums.GL_ModeForms.Delete Then
            With GridEX1
                .Visible = True
                .DataSource = dvForm
                .Height = .Height + GridAddSize
                .Width = .Width + GridAddWidthSize
            End With

            Me.Width = Me.Width + FormAddWidthSize
        End If

        'If dvForm.Count > 1 Then`
        '    Me.Height = Me.Height + FormAddSize
        'End If

        BoundCurrencyManager()

        Me.CenterToScreen()


    End Sub
    Private Sub SetFormData()

      
        'DisableGrid()
        Mode = UD_Dll.Enums.GL_ModeForms.UpdateRecord
        Dim dr As DataRowView
        If cmForm.Count = 0 Then ClearForm() : Exit Sub
        dr = dvForm.Item(cmForm.Position)

        cmbNameAnbar.Text = dr("NameAnbar")
        txtCodeKala.Text = dr("CodeKala")
        txtMin.Text = dr("Min")
        txtMax.Text = dr("Max")
        txtSefaresh.Text = dr("Sefaresh")


       
        

        GridEX1.Height = GridEX1.Height - GridAddSize
        GridEX1.Width = GridEX1.Width - GridAddWidthSize
        Me.Width = Me.Width - FormAddWidthSize

        SetButton()
    End Sub
    Private Sub BoundCurrencyManager()
        If dvForm.Count > 1 Then Mode = UD_Dll.Enums.GL_ModeForms.Search
        If dvForm.Count = 1 Then Mode = UD_Dll.Enums.GL_ModeForms.UpdateRecord
        If Mode = UD_Dll.Enums.GL_ModeForms.UpdateRecord Or Mode = UD_Dll.Enums.GL_ModeForms.Search Then
            cmForm = CType(BindingContext(GridEX1.DataSource), CurrencyManager)
            ' AddHandler cmForm.ItemChanged, AddressOf cmForm_ItemChanged
            '  AddHandler cmForm.PositionChanged, AddressOf cmForm_PositionChanged
            ' DisplayPosition()
        End If
    End Sub
#End Region
#Region "Form Buttons"
    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If Mode = UD_Dll.Enums.GL_ModeForms.AddNewRecord Then
            If Not objSec.HaveUserPermission(SN, UD_Dll.Security.ePermissions.Add) Then Exit Sub
            AddNewRecord()


        ElseIf Mode = UD_Dll.Enums.GL_ModeForms.UpdateRecord Then
            If Not objSec.HaveUserPermission(SN, UD_Dll.Security.ePermissions.Update) Then Exit Sub
            UpdateRecord()
        End If

        cmbNameAnbar.Focus()
    End Sub
    Private Sub btnCansel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        CancelForm()
    End Sub
    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        If Me.GridEX1.CurrentRow.RowType = Janus.Windows.GridEX.RowType.Record Then
            If Not objSec.HaveUserPermission(SN, UD_Dll.Security.ePermissions.Delete) Then Exit Sub
            If MsgBox("آيا رکورد حذف شود؟", MsgBoxStyle.YesNo + MsgBoxStyle.Question + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.DefaultButton2, "حذف رکورد") = MsgBoxResult.Yes Then
                DeleteRecord()
                CancelForm()
                SetButton()
                flg = False
            End If
        End If
    End Sub
    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub
    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click

        If Not objSec.HaveUserPermission(SN, UD_Dll.Security.ePermissions.Search) Then Exit Sub
        Try
            flgSearch = True
            Search(False, "")
            flg = False
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "Error IN DataBase ---->" & "btnSearch_Click")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, "Error IN ---->" & "btnSearch_Click")
        End Try
    End Sub
    Private Sub btnEdit_click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEdit.Click

        flgSearch = True
        Search(False, "")
        flg = False
    End Sub

#End Region


    
  
    Private Sub GroupBox1_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GroupBox1.Enter

    End Sub
End Class
