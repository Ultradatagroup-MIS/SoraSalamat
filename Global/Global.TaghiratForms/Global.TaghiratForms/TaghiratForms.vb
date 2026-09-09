Imports System.Data
Imports System.Data.SqlClient
Imports Microsoft.VisualBasic
Imports System.IO
Public Class frm_TaghiratForm
    Public ccTaghirat As Integer
    Private Sub frm_TaghiratForms_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetParameter()
        LoadCombo()
        ClearForm()
    End Sub
    Private Sub ClearForm()
        lblTitle.Text = "لطفا برای سهولت در کار اطلاعات را کامل و با دقت وارد کنید."
        cmbPersonel.SelectedIndex = -1
        txtNameForm.Text = ""
        txtTaghiratForm.Text = ""
        txtTaghiratDatabase.Text = ""
        txtNameForm.Text = ""
        cmbLoadNameForm.SelectedIndex = -1
        txtNameForm.Enabled = True
        txtTaghiratForm.Enabled = False
        txtTaghiratDatabase.Enabled = False
        Mode = 0
        GridTaghiratForm.SendToBack()
        GridTaghiratForm.Visible = False
        cmbPersonel.Enabled = True
        cmbPersonel.Focus()
        btnTaeed.SendToBack()

    End Sub
    Private Sub LoadCombo()

        Dim dt As Data.DataTable = Nothing
        Dim da As SqlDataAdapter = New SqlDataAdapter
        Try
            '-------------- Load Combo Personel
            Using cn As New SqlConnection(ConnectionString)
                Using cm As SqlCommand = cn.CreateCommand()
                    cn.Open()
                    cm.Parameters.Clear()
                    cm.CommandType = CommandType.StoredProcedure
                    cm.CommandText = "[Global].[spTaghiratForm_LoadPersonel]"
                    da.SelectCommand = cm
                    dt = New DataTable
                    da.Fill(dt)
                End Using
            End Using
            cmbPersonel.DataSource = dt
            cmbPersonel.DisplayMember = "NamePersonel"
            cmbPersonel.ValueMember = "ccPersonel"

            dt = Nothing

            '-------------- Load Combo Name Form
            Using cn As New SqlConnection(ConnectionString)
                Using cm As SqlCommand = cn.CreateCommand()
                    cn.Open()
                    cm.Parameters.Clear()
                    cm.CommandType = CommandType.StoredProcedure
                    cm.CommandText = "[Global].[spTaghiratForm_LoadNameForm]"
                    cm.Parameters.AddWithValue("@str", txtNameForm.Text.Trim)
                    da.SelectCommand = cm
                    dt = New DataTable
                    da.Fill(dt)
                End Using
            End Using
            cmbLoadNameForm.DataSource = dt
            cmbLoadNameForm.DisplayMember = "NameSubSystem"
            cmbLoadNameForm.ValueMember = "CodeSubSystem"

        Catch ex As Exception
            Throw New Exception("Error In--> Load Combo : " & ex.Message)
        Finally
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
            CodeDoreh = "1396"

        Else
            UserName = commands.Split(";")(0)
            UserCode = commands.Split(";")(1)
            UserPassWord = commands.Split(";")(2)
            NameMahalFaal = commands.Split(";")(3)
            CodeMahalFaal = commands.Split(";")(4)
            PersonelCode = commands.Split(";")(5)
            PersonelName = commands.Split(";")(6)
            CodeDoreh = commands.Split(";")(7)

        End If
    End Sub

    Private Sub btnAddPersonel_Click(sender As Object, e As EventArgs) Handles btnAddPersonel.Click
        Dim frm As New AddPersonel
        Me.Hide()
        frm.ShowDialog()
        Me.ShowDialog()
        LoadCombo()
        ClearForm()
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        GridTaghiratForm.Visible = True
        GridTaghiratForm.BringToFront()
        btnTaeed.BringToFront()
        Search()
        'lblTitle.Text = "برای مشاهده کامل جزییات روس سطر مورد نظر دوبار کلیک کنید."
    End Sub
    Private Sub Search()
        Dim dtForm As New DataTable
        Dim dt As Data.DataTable = Nothing
        Dim da As SqlDataAdapter = New SqlDataAdapter

        Try
            Using cn As New SqlConnection(ConnectionString)
                Using cm As SqlCommand = cn.CreateCommand()
                    cn.Open()
                    cm.Parameters.Clear()
                    cm.CommandType = CommandType.StoredProcedure
                    cm.CommandText = "[Global].[spTaghiratForm_Load]"
                    da.SelectCommand = cm
                    dt = New DataTable
                    da.Fill(dt)
                End Using
            End Using
            dtForm = dt
            SetGridStyle(dtForm)

        Catch ex As Exception
            Throw New Exception("Error In--> Search Grid : " & ex.Message)
        Finally
        End Try
    End Sub
    Private Sub SetGridStyle(dtForm As DataTable)
        With GridTaghiratForm
            .DataSource = Nothing
            .DataSource = dtForm
            .SetDataBinding(dtForm, "")
            .RetrieveStructure()
        End With

        For i As Integer = 0 To GridTaghiratForm.CurrentTable.Columns.Count - 1
            GridTaghiratForm.CurrentTable.Columns.Item(i).Visible = False
        Next

        GridTaghiratForm.CurrentTable.Columns.Item("ccTaghirat").Caption = "ccTaghirat"
        GridTaghiratForm.CurrentTable.Columns.Item("ccTaghirat").Visible = False
        GridTaghiratForm.CurrentTable.Columns.Item("ccTaghirat").Width = 130
        GridTaghiratForm.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
        GridTaghiratForm.CurrentTable.Columns.Item("ccTaghirat").Position = 0
        GridTaghiratForm.CurrentTable.Columns.Item("ccTaghirat").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center


        GridTaghiratForm.CurrentTable.Columns.Item("ccPersonel").Caption = "ccPersonel"
        GridTaghiratForm.CurrentTable.Columns.Item("ccPersonel").Visible = False
        GridTaghiratForm.CurrentTable.Columns.Item("ccPersonel").Width = 130
        GridTaghiratForm.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
        GridTaghiratForm.CurrentTable.Columns.Item("ccPersonel").Position = 1
        GridTaghiratForm.CurrentTable.Columns.Item("ccPersonel").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

        GridTaghiratForm.CurrentTable.Columns.Item("Tarikh").Caption = "تاریخ"
        GridTaghiratForm.CurrentTable.Columns.Item("Tarikh").Visible = True
        GridTaghiratForm.CurrentTable.Columns.Item("Tarikh").Width = 100
        GridTaghiratForm.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
        GridTaghiratForm.CurrentTable.Columns.Item("Tarikh").Position = 2
        GridTaghiratForm.CurrentTable.Columns.Item("Tarikh").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

        GridTaghiratForm.CurrentTable.Columns.Item("txtVazeiat").Caption = "تایید"
        GridTaghiratForm.CurrentTable.Columns.Item("txtVazeiat").Visible = True
        GridTaghiratForm.CurrentTable.Columns.Item("txtVazeiat").Width = 100
        GridTaghiratForm.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
        GridTaghiratForm.CurrentTable.Columns.Item("txtVazeiat").Position = 3
        GridTaghiratForm.CurrentTable.Columns.Item("txtVazeiat").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

        GridTaghiratForm.CurrentTable.Columns.Item("sVazeiat").Caption = "sVazeiat"
        GridTaghiratForm.CurrentTable.Columns.Item("sVazeiat").Visible = False
        GridTaghiratForm.CurrentTable.Columns.Item("sVazeiat").Width = 100
        GridTaghiratForm.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
        GridTaghiratForm.CurrentTable.Columns.Item("sVazeiat").Position = 4
        GridTaghiratForm.CurrentTable.Columns.Item("sVazeiat").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

        GridTaghiratForm.CurrentTable.Columns.Item("NamePersonel").Caption = "نام پرسنل"
        GridTaghiratForm.CurrentTable.Columns.Item("NamePersonel").Visible = True
        GridTaghiratForm.CurrentTable.Columns.Item("NamePersonel").Width = 150
        GridTaghiratForm.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
        GridTaghiratForm.CurrentTable.Columns.Item("NamePersonel").Position = 5
        GridTaghiratForm.CurrentTable.Columns.Item("NamePersonel").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

        GridTaghiratForm.CurrentTable.Columns.Item("NameForm").Caption = "نام فرم"
        GridTaghiratForm.CurrentTable.Columns.Item("NameForm").Visible = True
        GridTaghiratForm.CurrentTable.Columns.Item("NameForm").Width = 250
        GridTaghiratForm.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
        GridTaghiratForm.CurrentTable.Columns.Item("NameForm").Position = 6
        GridTaghiratForm.CurrentTable.Columns.Item("NameForm").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

        GridTaghiratForm.CurrentTable.Columns.Item("TaghirateForm").Caption = "تغییرات فرم"
        GridTaghiratForm.CurrentTable.Columns.Item("TaghirateForm").Visible = True
        GridTaghiratForm.CurrentTable.Columns.Item("TaghirateForm").Width = 300
        GridTaghiratForm.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
        GridTaghiratForm.CurrentTable.Columns.Item("TaghirateForm").Position = 7
        GridTaghiratForm.CurrentTable.Columns.Item("TaghirateForm").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

        GridTaghiratForm.CurrentTable.Columns.Item("TaghirateDatabase").Caption = "تغییرات دیتابیس"
        GridTaghiratForm.CurrentTable.Columns.Item("TaghirateDatabase").Visible = True
        GridTaghiratForm.CurrentTable.Columns.Item("TaghirateDatabase").Width = 300
        GridTaghiratForm.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
        GridTaghiratForm.CurrentTable.Columns.Item("TaghirateDatabase").Position = 8
        GridTaghiratForm.CurrentTable.Columns.Item("TaghirateDatabase").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

        GridTaghiratForm.CurrentTable.Columns.Item("CodeForm").Caption = "CodeForm"
        GridTaghiratForm.CurrentTable.Columns.Item("CodeForm").Visible = False
        GridTaghiratForm.CurrentTable.Columns.Item("CodeForm").Width = 250
        GridTaghiratForm.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
        GridTaghiratForm.CurrentTable.Columns.Item("CodeForm").Position = 9
        GridTaghiratForm.CurrentTable.Columns.Item("CodeForm").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        ClearForm()
    End Sub

    Private Sub cmbPersonel_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbPersonel.SelectedIndexChanged
        If cmbPersonel.SelectedIndex = -1 Then
            txtNameForm.Enabled = False
            txtTaghiratForm.Enabled = False
            txtTaghiratDatabase.Enabled = False
        Else
            txtNameForm.Enabled = True
            txtTaghiratForm.Enabled = True
            txtTaghiratDatabase.Enabled = True
        End If
    End Sub

    Private Sub GridTaghiratForm_MouseClick(sender As Object, e As MouseEventArgs) Handles GridTaghiratForm.MouseClick
        If GridTaghiratForm.SelectedItems.Count = 0 Then
            ' MsgBox("تامین کننده ثبت نشده است", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRtlReading + vbMsgBoxRight, "")
            Exit Sub
        Else
            ccTaghirat = Val(Me.GridTaghiratForm.CurrentRow.Cells("ccTaghirat").Text.Replace(",", "").Trim)
        End If
    End Sub

    Private Sub GridTaghiratForm_DoubleClick(sender As Object, e As EventArgs) Handles GridTaghiratForm.DoubleClick

        Dim dtForm As New DataTable
        Dim dt As Data.DataTable = Nothing
        Dim da As SqlDataAdapter = New SqlDataAdapter

        Try
            Using cn As New SqlConnection(ConnectionString)
                Using cm As SqlCommand = cn.CreateCommand()
                    cn.Open()
                    cm.Parameters.Clear()
                    cm.CommandType = CommandType.StoredProcedure
                    cm.CommandText = "[Global].[spTaghiratForm_Load_Row]"
                    cm.Parameters.AddWithValue("ccTaghirat", ccTaghirat)
                    da.SelectCommand = cm
                    dt = New DataTable
                    da.Fill(dt)
                End Using
            End Using
            dtForm = dt

            cmbPersonel.SelectedValue = dtForm.Rows(0)("ccPersonel")
            txtNameForm.Text = dtForm.Rows(0)("NameForm").ToString
            txtTaghiratForm.Text = dtForm.Rows(0)("TaghirateForm").ToString
            txtTaghiratDatabase.Text = dtForm.Rows(0)("TaghirateDataBase").ToString
            GridTaghiratForm.SendToBack()
            GridTaghiratForm.Visible = False
            btnTaeed.SendToBack()
            Mode = 1

            If dtForm.Rows(0)("sVazeiat") = 1 Then
                txtNameForm.Enabled = False
                txtTaghiratForm.Enabled = False
                txtTaghiratDatabase.Enabled = False
                cmbPersonel.Enabled = False
                txtNameForm.Text = ""
                txtNameForm.Enabled = False
            ElseIf dtForm.Rows(0)("sVazeiat") = 0 Then
                txtNameForm.Enabled = True
                txtTaghiratForm.Enabled = True
                txtTaghiratDatabase.Enabled = True
                cmbPersonel.Enabled = True
                txtNameForm.Text = ""
                txtNameForm.Enabled = True
            End If

        Catch ex As Exception
            Throw New Exception("Error In--> Search Edit Row : " & ex.Message)
        Finally
        End Try
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If IsValid() = False Then Exit Sub
        If Mode = 0 Then
            If txtTaghiratDatabase.Text = "" Or txtTaghiratForm.Text = "" Then
                If MsgBox("تغییرات فرم یا تغییرات دیتابیس کامل وارد نشده است ،آیا میخواهید ادامه دهید؟", MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.YesNo, "هشدار") = MsgBoxResult.Yes Then
                    InsertTaghirat()
                    ClearForm()
                    Exit Sub
                Else
                    Exit Sub
                End If
            End If

            InsertTaghirat()
            ClearForm()

        ElseIf Mode = 1 Then

            If txtTaghiratDatabase.Text = "" Or txtTaghiratForm.Text = "" Then
                If MsgBox("تغییرات فرم یا تغییرات دیتابیس کامل وارد نشده است ،آیا میخواهید ادامه دهید؟", MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.YesNo, "هشدار") = MsgBoxResult.Yes Then
                    UpdateTaghirat()
                    ClearForm()
                    Exit Sub
                Else
                    Exit Sub
                End If
            End If

            UpdateTaghirat()
            ClearForm()
        End If

    End Sub
    Private Function IsValid() As Boolean
        If cmbPersonel.SelectedIndex = -1 Then
            MsgBox("ابتدا یک پرسنل را انتخاب کنید", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Critical, "خطا")
            Return False
        End If
        If cmbLoadNameForm.SelectedIndex = -1 Then
            MsgBox("نام فرم را انتخاب کنید", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Critical, "خطا")
            Return False
        End If
        Return True
    End Function
    Private Sub UpdateTaghirat()
        Using cn As New SqlConnection(ConnectionString)
            Using cm As SqlCommand = cn.CreateCommand()
                cm.CommandType = CommandType.StoredProcedure
                cm.CommandText = "[Global].[spTaghiratForm_Update]"
                cm.Parameters.AddWithValue("ccTaghirat", ccTaghirat)
                cm.Parameters.AddWithValue("ccPersonel", cmbPersonel.SelectedValue)
                cm.Parameters.AddWithValue("CodeForm", cmbLoadNameForm.SelectedValue)
                cm.Parameters.AddWithValue("TaghirateForm", txtTaghiratForm.Text.Trim)
                cm.Parameters.AddWithValue("TaghirateDataBase", txtTaghiratDatabase.Text.Trim)
                cm.Connection.Open()
                cm.ExecuteNonQuery()
                cm.Connection.Close()
                cm.Parameters.Clear()
            End Using
        End Using
    End Sub

    Private Sub InsertTaghirat()
        Using cn As New SqlConnection(ConnectionString)
            Using cm As SqlCommand = cn.CreateCommand()
                cm.CommandType = CommandType.StoredProcedure
                cm.CommandText = "[Global].[spTaghiratForm_Insert]"
                cm.Parameters.AddWithValue("ccPersonel", cmbPersonel.SelectedValue)
                cm.Parameters.AddWithValue("CodeForm", cmbLoadNameForm.SelectedValue)
                cm.Parameters.AddWithValue("TaghirateForm", txtTaghiratForm.Text.Trim)
                cm.Parameters.AddWithValue("TaghirateDataBase", txtTaghiratDatabase.Text.Trim)
                cm.Connection.Open()
                cm.ExecuteNonQuery()
                cm.Connection.Close()
                cm.Parameters.Clear()
            End Using
        End Using
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        If GridTaghiratForm.SelectedItems.Count = 0 Then
            MsgBox("ابتدا یک سطر را انتخاب کنید", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Critical, "خطا")
            Exit Sub
        ElseIf Val(Me.GridTaghiratForm.CurrentRow.Cells("sVazeiat").Text.Replace(",", "").Trim) = 1 Then
            MsgBox("این سطر تایید شده است و اجازه حذف آنرا ندارید.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Critical, "خطا")
            Exit Sub
        ElseIf MsgBox("آیا سطر انتخابی حذف شود؟", MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.YesNo, "هشدار") = MsgBoxResult.Yes Then
            objTools.DDelete("Global.TaghiratForm", "ccTaghirat = " & ccTaghirat)
            Search()
            Exit Sub
        Else
        End If
    End Sub

    Private Sub btnTaeed_Click(sender As Object, e As EventArgs) Handles btnTaeed.Click
        If GridTaghiratForm.SelectedItems.Count = 0 Then
            MsgBox("ابتدا یک سطر را انتخاب کنید", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Critical, "خطا")
            Exit Sub
        ElseIf MsgBox("آیا سطر انتخابی تایید شود؟", MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.YesNo, "هشدار") = MsgBoxResult.Yes Then
            objTools.DUpdate("sVazeiat", "Global.Taghiratform", "1", "ccTaghirat = " & ccTaghirat)
            Search()
            Exit Sub
        Else
        End If
    End Sub

    Private Sub txtNameForm_TextChanged(sender As Object, e As EventArgs) Handles txtNameForm.TextChanged
        If txtNameForm.Text <> "" Then
            Dim dt As Data.DataTable = Nothing
            Dim da As SqlDataAdapter = New SqlDataAdapter
 
                Using cn As New SqlConnection(ConnectionString)
                    Using cm As SqlCommand = cn.CreateCommand()
                        cn.Open()
                        cm.Parameters.Clear()
                        cm.CommandType = CommandType.StoredProcedure
                    cm.CommandText = "[Global].[spTaghiratForm_LoadNameForm]"
                    cm.Parameters.AddWithValue("@str", txtNameForm.Text.Trim)
                        da.SelectCommand = cm
                        dt = New DataTable
                        da.Fill(dt)
                    End Using
                End Using
                cmbLoadNameForm.DataSource = dt
                cmbLoadNameForm.DisplayMember = "NameSubSystem"
            cmbLoadNameForm.ValueMember = "CodeSubSystem"
            If dt.Rows.Count = 0 Then
                cmbLoadNameForm.SelectedIndex = -1
            Else
                cmbLoadNameForm.SelectedIndex = 0
            End If
        End If
    End Sub
End Class
