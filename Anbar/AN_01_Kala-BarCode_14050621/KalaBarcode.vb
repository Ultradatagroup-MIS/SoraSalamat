Public Class KalaBarcode

#Region " Variable AND Constant Declration "
    Const FormTableName = "tblAN_KalaBarcode"
    Dim ErrPro As New ErrorProvider
    Dim Mode As UD_Dll.Enums.GL_ModeForms = UD_Dll.Enums.GL_ModeForms.AddNewRecord
    Dim dsForm As New DataSet
    Dim dvForm As DataView

    Const FormAddSize = 200
    Const FormOrgSize = 200
    Const GridAddSize = 300

    Private SN As Integer
    Dim cmForm As CurrencyManager
    Dim txtCaption As String

#End Region
#Region " Form Event Code "
    Private Sub KalaBarcode_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        SetParameter()
        txtNameKala.Text = NameKala
        txtCodeKala.Text = CodeKala
        LoadCombo()
        ClearForm()
        DisableGrid()
    End Sub
    Private Sub KalaBarcode_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
        Try
            dsForm = Nothing
            dvForm = Nothing
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "Error IN DataBase ---->" & "KalaBarcode_Closed")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, "Error IN ---->" & "KalaBarcode_Closed")
        End Try
    End Sub
    Private Sub CheckIsNumeric(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtBarCode.KeyPress
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
    Private Sub GridEX_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GridEX.DoubleClick
        If GridEX.CurrentRow.Cells("NameKala").Text <> txtNameKala.Text Then
            MsgBox("شما قادر به انتخاب این کالا نمی باشید .", MsgBoxStyle.Information + MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, "پیام")
            Exit Sub
        Else
            EditRecord()
        End If
    End Sub
    Private Sub GridEX_DeletingRecord(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.RowActionCancelEventArgs) Handles GridEX.DeletingRecord
        If Me.GridEX.CurrentRow.RowType = Janus.Windows.GridEX.RowType.Record Then
            If Not objSec.HaveUserPermission(SN, UD_Dll.Security.ePermissions.Delete) Then Exit Sub
            If MsgBox("آيا رکورد حذف شود؟", MsgBoxStyle.YesNo + MsgBoxStyle.Question + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.DefaultButton2, "حذف رکورد") = MsgBoxResult.Yes Then
                DeleteRecord()
            End If
        End If
    End Sub
#End Region
#Region " Global Form Code "
    Private Sub LoadCombo()
        Dim cnSQL As New SqlConnection
        Dim cmSQL As New SqlCommand
        Dim daSQL As New SqlDataAdapter
        Dim strSQL As String
        Dim dr As DataRow

        dsForm = New DataSet

        Try
            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()

            strSQL = "Global.spNoeMoshtary_LoadCombo"

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            daSQL = New SqlDataAdapter(cmSQL)
            daSQL.Fill(dsForm, "tblNoeMoshtary")
            '---------------------------------------
            dr = dsForm.Tables("tblNoeMoshtary").NewRow
            dr("sharh") = "همه"
            dr("code") = 0
            dsForm.Tables("tblNoeMoshtary").Rows.Add(dr)
            '---------------------------------------------------------
            cmbNoeMoshtary.DataSource = Nothing
            cmbNoeMoshtary.Items.Clear()
            cmbNoeMoshtary.DataSource = dsForm.Tables("tblNoeMoshtary").DefaultView
            cmbNoeMoshtary.DisplayMember = "Sharh"
            cmbNoeMoshtary.ValueMember = "Code"

            cmSQL = Nothing : daSQL = Nothing
            cnSQL.Close()

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "Error IN DataBase ---->" & "LoadCombo")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, "Error IN ---->" & "LoadCombo")
        End Try
    End Sub
    Private Sub ClearForm()
        Mode = UD_Dll.Enums.GL_ModeForms.AddNewRecord

        cmbNoeMoshtary.SelectedIndex = -1
        'cmbNoeMoshtary.SelectedValue = 0

        txtBarCode.Text = ""
        SetButton()
    End Sub
    Private Sub AddNewRecord()
        Try
            Dim cnSQL As SqlConnection
            Dim cmSQL As SqlCommand
            Dim strSQL As String

            'strSQL = "INSERT into tblAN_KalaBarcode"
            'strSQL = strSQL & "(ccKala,BarCode,sNoeMoshtary,Faal,UserName,Tarikh,Saat)"
            'strSQL = strSQL & " VALUES ("
            'strSQL = strSQL & cckala & ","
            'strSQL = strSQL & txtBarCode.Text & ","
            'strSQL = strSQL & cmbNoeMoshtary.SelectedValue & ","
            'strSQL = strSQL & 1 & ","
            'strSQL = strSQL & "'" & UserName & "',"
            'strSQL = strSQL & "'" & TarikhEmrooz & "',"
            'strSQL = strSQL & "'" & Format(TimeOfDay, "HH:mm:ss") & "')"

            strSQL = "WareHouse.spKala_KalaBarCode_Insert"

            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()


            cmSQL.Parameters.AddWithValue("ccKala", cckala)
            cmSQL.Parameters.AddWithValue("barCode", txtBarCode.Text)
            cmSQL.Parameters.AddWithValue("sNoeMoshtary", cmbNoeMoshtary.SelectedValue)
            cmSQL.Parameters.AddWithValue("Faal", 1)
            cmSQL.Parameters.AddWithValue("UserName", UserName)
           

            cmSQL.ExecuteNonQuery()

            cmSQL = Nothing
            cnSQL.Close()

            MsgBox("ذخیــره با موفقیت انجام شد .", MsgBoxStyle.Information + MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, "ذخیــره")

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
        End Try

    End Sub
    Private Sub UpdateRecord()
        Dim cnSQL As SqlConnection
        Dim cmSQL As SqlCommand
        Dim strSQL As String


        strSQL = "WareHouse.spKala_KalaBarCode_Update"
        cnSQL = New SqlConnection(ConnectionString)
        cnSQL.Open()

        cmSQL = New SqlCommand(strSQL, cnSQL)
        cmSQL.CommandType = CommandType.StoredProcedure
        cmSQL.Parameters.Clear()

        cmSQL.Parameters.AddWithValue("BarCode", txtBarCode.Text.Trim)
        cmSQL.Parameters.AddWithValue("ccKala", cckala)
        cmSQL.Parameters.AddWithValue("sNoeMoshtary", cmbNoeMoshtary.SelectedValue)

        cmSQL.ExecuteNonQuery()

        cmSQL = Nothing
        cnSQL.Close()

        MsgBox("به روز رسانی با موفقیت انجام شد .", MsgBoxStyle.Information + MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, "به روز رسانی")

    End Sub
    Private Sub CancelForm()
        ClearForm()
        DisableGrid()
    End Sub
    Private Sub DisableGrid()
        GridEX.Visible = False
        Me.Height = FormOrgSize
        If GridEX.Height > 0 Then
            GridEX.Height = GridEX.Height - GridAddSize
        End If
        Me.CenterToScreen()
    End Sub
    Private Sub DeleteRecord()
        Dim cnSQL As SqlConnection
        Dim cmSQL As SqlCommand
        Dim strSQL As String
        'Dim intRowsAffected As Integer
        Try
            'Dim dr As DataRow
            'If cmForm.Position = -1 Then Exit Sub
            'dr = dvForm.Item(cmForm.Position).Row

            strSQL = "WareHouse.spKala_KalaBarCode_Delete"

            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()
            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            cmSQL.Parameters.AddWithValue("cckalaBarCode", GridEX.CurrentRow.Cells("cckalaBarCode").Value)
            cmSQL.ExecuteNonQuery()
            MsgBox("حذف با موفقیت انجام شد .", MsgBoxStyle.Information + MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, "حذف")

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
                        MsgBox(".خطا در حذف رکورد,حذف انجام نشد", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "خطاي بانک")
                    End If
                Case 547
                    MsgBox("براي اين رکورد اطلاعات ديگري وجود دارد.ابتدا آنها را حذف کنيد.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "خطاي بانک")
                Case Else
                    MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "خطاي بانک")
            End Select

        Catch e As Exception
            MsgBox(e.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "خطا")
        End Try
    End Sub
    Private Sub Search(ByVal WithCriteria As Boolean)
        Dim StrSql As String
        Try
            StrSql = "WareHouse.spKala_KalaBarCode_Search "

            RefreshFormData(StrSql, WithCriteria)
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "Error IN DataBase ---->" & "search")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, "Error IN ---->" & "search")
        End Try
    End Sub
    Private Sub RefreshFormData(ByVal strsql As String, ByVal WithCriteria As Boolean)
        Dim cnSQL As New SqlConnection
        Dim cmSQL As New SqlCommand
        Dim daSQL As SqlDataAdapter
        Try
            If dsForm.Tables.Contains(FormTableName) = True Then
                dsForm.Tables.Remove(FormTableName)
            End If

            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()
            Me.txtCodeKala.Tag = objTools.ConvertNulls(objTools.DLookup("ccKala", "tblAn_Kala", "CodeKala='" & txtCodeKala.Text & "'" & ""), 0)
            cmSQL = New SqlCommand(strsql, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            cmSQL.Parameters.AddWithValue("ccKala", IIf(WithCriteria, txtCodeKala.Tag, 0))

            daSQL = New SqlDataAdapter(cmSQL)
            daSQL.Fill(dsForm, FormTableName)

            '--------------------------------------
            dvForm = New DataView
            dvForm = dsForm.Tables(FormTableName).DefaultView
            'dvForm.Sort = "CodeKala"
            dvForm.AllowDelete = True
            dvForm.AllowEdit = True
            dvForm.AllowNew = False
            daSQL = Nothing

            'If dvForm.Count = 1 Then
            '    EditRecord()
            'ElseIf dvForm.Count = 0 Then
            '    MsgBox("برای این کالا، بارکـدی ثبت نشـده است", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "پيام")
            '    CancelForm()
            'Else
            '    Mode = UD_Dll.Enums.GL_ModeForms.Search
            '    SetGridStyle()
            'End If

            'If dvForm.Count <> 1 Then
            Mode = UD_Dll.Enums.GL_ModeForms.Search
            SetGridStyle()
            If dvForm.Count = 0 Then
                MsgBox("برای این کالا، بارکـدی ثبت نشـده است", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "پيام")
                CancelForm()
            ElseIf dvForm.Count = 1 Then
                EditRecord()
            End If

            cmSQL = Nothing : daSQL = Nothing
            cnSQL.Close()

        Catch ex As Exception

        End Try
    End Sub
    Private Sub SetGridStyle()
        Try
            With GridEX
                .DataSource = Nothing
                .DataSource = dsForm.Tables(FormTableName).DefaultView
                .SetDataBinding(dsForm.Tables(FormTableName).DefaultView, "")
                .RetrieveStructure()
            End With

            For i As Integer = 0 To GridEX.CurrentTable.Columns.Count - 1
                GridEX.CurrentTable.Columns.Item(i).Visible = False
            Next

            GridEX.CurrentTable.Columns.Item("codekala").Caption = "کـــد کالا"
            GridEX.CurrentTable.Columns.Item("codekala").Visible = True
            GridEX.CurrentTable.Columns.Item("codekala").Width = 130
            GridEX.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEX.CurrentTable.Columns.Item("codekala").Position = 0
            GridEX.CurrentTable.Columns.Item("codekala").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEX.CurrentTable.Columns.Item("NameKala").Caption = "نام کالا"
            GridEX.CurrentTable.Columns.Item("NameKala").Visible = True
            GridEX.CurrentTable.Columns.Item("NameKala").Width = 246
            GridEX.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEX.CurrentTable.Columns.Item("NameKala").Position = 1
            GridEX.CurrentTable.Columns.Item("NameKala").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center


            GridEX.CurrentTable.Columns.Item("NoeMoshtary").Caption = "نوع مشتری"
            GridEX.CurrentTable.Columns.Item("NoeMoshtary").Visible = True
            GridEX.CurrentTable.Columns.Item("NoeMoshtary").Width = 240
            GridEX.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEX.CurrentTable.Columns.Item("NoeMoshtary").Position = 2
            GridEX.CurrentTable.Columns.Item("NoeMoshtary").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEX.CurrentTable.Columns.Item("BarCode").Caption = "بارکـــد"
            GridEX.CurrentTable.Columns.Item("BarCode").Visible = True
            GridEX.CurrentTable.Columns.Item("BarCode").Width = 180
            GridEX.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEX.CurrentTable.Columns.Item("BarCode").Position = 3
            GridEX.CurrentTable.Columns.Item("BarCode").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEX.CurrentTable.Columns.Item("cckalaBarcode").Caption = "cckalaBarcode"
            GridEX.CurrentTable.Columns.Item("cckalaBarcode").Visible = False
            GridEX.CurrentTable.Columns.Item("cckalaBarcode").Width = 0
            GridEX.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEX.CurrentTable.Columns.Item("cckalaBarcode").Position = 4
            GridEX.CurrentTable.Columns.Item("cckalaBarcode").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center


            For i As Integer = 0 To GridEX.RootTable.Columns.Count - 1
                If GridEX.RootTable.Columns(i).Type.IsValueType Then
                    GridEX.RootTable.Columns(i).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                    GridEX.RootTable.Columns(i).FormatMode = Janus.Windows.GridEX.FormatMode.UseIFormattable
                    GridEX.RootTable.Columns(i).FormatString = "G"
                    GridEX.RootTable.Columns(i).TotalFormatMode = Janus.Windows.GridEX.FormatMode.UseIFormattable
                    GridEX.RootTable.Columns(i).TotalFormatString = "G"

                End If
            Next

          
            With GridEX
                .Visible = True
                .DataSource = dvForm
                .Height = .Height + GridAddSize
            End With
            If dvForm.Count > 1 Then
                Me.Height = Me.Height + FormAddSize
            End If
            'GridEX.Visible = True
            'GridEX.HeaderFormatStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center


            BoundCurrencyManager()
            SetButton()
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> SetGridEx")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> SetGridEx")
        End Try

    End Sub
    Private Sub BoundCurrencyManager()
        If dvForm.Count > 1 Then Mode = UD_Dll.Enums.GL_ModeForms.Search
        If dvForm.Count = 1 Then Mode = UD_Dll.Enums.GL_ModeForms.UpdateRecord
        If Mode = UD_Dll.Enums.GL_ModeForms.UpdateRecord Or Mode = UD_Dll.Enums.GL_ModeForms.Search Then
            cmForm = CType(BindingContext(GridEX.DataSource), CurrencyManager)
            AddHandler cmForm.ItemChanged, AddressOf cmForm_ItemChanged
            AddHandler cmForm.PositionChanged, AddressOf cmForm_PositionChanged
            'DisplayPosition()
        End If
    End Sub
    Private Sub cmForm_PositionChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        'DisplayPosition()
    End Sub
    Private Sub cmForm_ItemChanged(ByVal sender As Object, ByVal e As ItemChangedEventArgs)
        'DisplayPosition()
    End Sub
    Private Sub EditRecord()
        SetFormData()
    End Sub
    Private Sub SetFormData()
        DisableGrid()
        Mode = UD_Dll.Enums.GL_ModeForms.UpdateRecord
        'Dim dr As DataRowView
        If cmForm.Count = 0 Then ClearForm() : Exit Sub
        
        'dr = dvForm.Item(cmForm.Position)
        'txtNameKala.Text = dr("NameKala")
        'txtCodeKala.Text = dr("CodeKala")
        'txtBarCode.Text = dr("BarCode")
        'cmbNoeMoshtary.SelectedValue = dr("sNoeMoshtary")

        txtNameKala.Text = GridEX.CurrentRow.Cells("NameKala").Value()
        txtCodeKala.Text = GridEX.CurrentRow.Cells("CodeKala").Value()
        txtBarCode.Text = GridEX.CurrentRow.Cells("BarCode").Value()
        cmbNoeMoshtary.SelectedValue = GridEX.CurrentRow.Cells("sNoeMoshtary").Value()
        SetButton()
    End Sub
    Private Sub SetButton()
        Me.btnUpdate.Enabled = False
        Me.btnDelete.Enabled = False
        Me.btnSearch.Enabled = False
        Me.btnCancel.Enabled = False
        Me.btnExit.Enabled = True
        Me.btnRefresh.Enabled = False
        Me.grbMain.Visible = False
      
        If Mode = UD_Dll.Enums.GL_ModeForms.AddNewRecord Then
            Me.btnUpdate.Enabled = True
            Me.btnSearch.Enabled = True
            Me.btnCancel.Enabled = True
            Me.btnRefresh.Enabled = True
            Me.grbMain.Visible = True
            '  Me.btnDelete.Enabled = False
           
        ElseIf Mode = UD_Dll.Enums.GL_ModeForms.UpdateRecord Then
            Me.btnUpdate.Enabled = True
            Me.btnDelete.Enabled = True
            Me.btnCancel.Enabled = True
            Me.btnRefresh.Enabled = False
            Me.btnSearch.Enabled = True
            Me.grbMain.Visible = True

           
          
        ElseIf Mode = UD_Dll.Enums.GL_ModeForms.Search Then
            Me.btnDelete.Enabled = True
            Me.btnCancel.Enabled = True
            Me.grbMain.Visible = False
          
        End If

    End Sub
    Private Function IsValidform(ByVal CheckField As String) As Boolean
        IsValidform = False
        If txtBarCode.Text = "" Then
            ErrPro.SetError(txtBarCode, ".بارکد را وارد کنید ")
            MsgBox(".بارکد را وارد کنید ", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
            txtBarCode.Focus()
            Exit Function
        End If
        ErrPro.SetError(Me.txtBarCode, "")

        If cmbNoeMoshtary.SelectedIndex = -1 Then
            ErrPro.SetError(cmbNoeMoshtary, ".نوع مشتری را مشخص کنید ")
            MsgBox(".نوع مشتری را مشخص کنید ", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
            cmbNoeMoshtary.Focus()
            Exit Function
        End If
        ErrPro.SetError(Me.cmbNoeMoshtary, "")
        Return True
    End Function
    Private Sub SetParameter()
        Dim commands As String = Microsoft.VisualBasic.Command()
        If commands.Length = 0 Then

            UserName = "Administrator"
            UserPassWord = "66998833"
            NameMahalFaal = "تهران"
            CodeMahalFaal = "1"
            PersonelCode = "0"
            PersonelName = "Administrator"
            CodeDoreh = "1393"
            txtCaption = "کالا بارکــد"
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
#End Region
#Region " From Buttons "
    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub
    Private Sub btnUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdate.Click

        If Not IsValidform("All") Then
            Exit Sub
        End If

        If objTools.DCount("ccKalaBarCode", "tblAN_KalaBarcode", "ccKala = " & cckala & " AND sNoeMoshtary = " & cmbNoeMoshtary.SelectedValue) = 0 Then
            AddNewRecord()
        Else
            If MsgBox("برای این کالا و نوع مشتری انتخاب شده ، بارکد تعریف شده است . آیا مایلید آنرا به روز رسانی نمایید ؟", MsgBoxStyle.Question + MsgBoxStyle.YesNo + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, "") = MsgBoxResult.Yes Then
                UpdateRecord()
            End If
        End If

        ClearForm()
    End Sub
    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        CancelForm()
    End Sub
    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        'If Not objSec.HaveUserPermission(SN, UD_Dll.Security.ePermissions.Delete) Then Exit Sub
        If MsgBox("آيا رکورد حذف شود؟", MsgBoxStyle.YesNo + MsgBoxStyle.Question + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.DefaultButton2, "حذف رکورد") = MsgBoxResult.Yes Then
            DeleteRecord()
        End If
    End Sub
    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        'If Not objSec.HaveUserPermission(SN, UD_Dll.Security.ePermissions.Search) Then Exit Sub
        Search(False)
    End Sub
    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            Search(True)
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "Error IN DataBase ---->" & "btnSearch_Click")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, "Error IN ---->" & "btnSearch_Click")

        End Try

    End Sub
#End Region
#Region "Stored Procedures"
    'Global.spNoeMoshtary_LoadCombo
    'spKala_KalaBarCode_Delete
    'spKala_KalaBarCode_Insert
    'spKala_KalaBarCode_Search
    'spKala_KalaBarCode_Update
#End Region

End Class