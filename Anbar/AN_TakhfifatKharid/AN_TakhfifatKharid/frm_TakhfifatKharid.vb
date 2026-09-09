Imports System.Data
Imports System.Data.SqlClient
Imports Microsoft.VisualBasic
Imports System.IO
Public Class frm_TakhfifatKharid
    Dim dsForm As New DataSet

    Private Sub frm_TakhfifatKharid_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetParameters()
        LoadCombo()
        ClearForm()

    End Sub
    Private Sub SetParameters()
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
    Private Sub ClearForm()

        For i As Integer = 0 To chlMarkazPakhsh.Items.Count - 1
            chlMarkazPakhsh.SetItemChecked(i, False)
        Next

        For i As Integer = 0 To chlTaaminKonandeh.Items.Count - 1
            chlTaaminKonandeh.SetItemChecked(i, False)
        Next

        For i As Integer = 0 To chlKala.Items.Count - 1
            chlKala.SetItemChecked(i, False)
        Next

        cmbGoroh1.SelectedIndex = -1
        cmbGoroh2.SelectedIndex = -1
        cmbGoroh3.SelectedIndex = -1
        chlMarkazPakhsh.SetSelected(0, True)
        chlTaaminKonandeh.SetSelected(0, True)
        chlKala.SetSelected(0, True)
        mskAzTarikh.Text = ""
        mskTaTarikh.Text = ""
        txtDarsad.Text = ""
        grid.Visible = False
        grid.SendToBack()
        chk = True

    End Sub
    Private Sub LoadCombo()

        Dim Strsql As String
        Dim daSQL As SqlDataAdapter
        Dim dt As New Data.DataTable

        '--------------------------- Load MarkazPakhsh

        Strsql = "Select CodeMahal,NameMahal From tblGL_MarkazPakhsh where CodeMahal <> 0 "
        If CodeMahalFaal <> 1 Then
            Strsql &= " and CodeMahal = " & CodeMahalFaal
        End If
        daSQL = New SqlDataAdapter(Strsql, ConnectionString)
        daSQL.Fill(dt)
        chlMarkazPakhsh.DataSource = Nothing
        chlMarkazPakhsh.Items.Clear()
        chlMarkazPakhsh.DataSource = dt
        chlMarkazPakhsh.DisplayMember = "NameMahal"
        chlMarkazPakhsh.ValueMember = "CodeMahal"

        '--------------------------- Load TaaminKonandeh

        Strsql = "Select NameTaminKonandeh,ccTaminKonandeh From tblFO_TaminKonandeh where Faal=1 AND"
        Strsql &= " Not exists (Select * From tblGL_SecurityData where namekarbar= '" & UserName & "' and CodeSubSystem = 654 and pk = tblFO_TaminKonandeh.ccTaminKonandeh) order by NameTaminKonandeh"
        daSQL = New SqlDataAdapter(Strsql, ConnectionString)
        dt = New Data.DataTable
        daSQL.Fill(dt)
        chlTaaminKonandeh.DataSource = Nothing
        chlTaaminKonandeh.Items.Clear()
        chlTaaminKonandeh.DataSource = dt
        chlTaaminKonandeh.DisplayMember = "NameTaminKonandeh"
        chlTaaminKonandeh.ValueMember = "ccTaminKonandeh"

        '--------------------------- Load Kala

        Strsql = "Select (CAST(CodeKala AS nVarchar)+' _ '+ NameKala) as NameKala,ccKala From tblAn_Kala where Faal = 1 Order By CodeKala"
        daSQL = New SqlDataAdapter(Strsql, ConnectionString)
        dt = New Data.DataTable
        daSQL.Fill(dt)
        chlKala.DataSource = Nothing
        chlKala.Items.Clear()
        chlKala.DataSource = dt
        chlKala.DisplayMember = "NameKala"
        chlKala.ValueMember = "ccKala"

        '_________________ Load Goroh1 Goroh2 Goroh3 __________________

        Strsql = "Select * From tblGL_ShenasehOmomi Where CodeAsli = 200 And CodeFarei<>0  order by Code"
        daSQL = New SqlDataAdapter(Strsql, ConnectionString)
        daSQL.Fill(dsForm, "tbl_Goroh1")

        Strsql = "Select * From tblGL_ShenasehOmomi Where CodeAsli = 201 And CodeFarei<>0  order by Code"
        daSQL = New SqlDataAdapter(Strsql, ConnectionString)
        daSQL.Fill(dsForm, "tbl_Goroh2")

        Strsql = "Select * From tblGL_ShenasehOmomi Where CodeAsli = 202 And CodeFarei<>0  order by Code"
        daSQL = New SqlDataAdapter(Strsql, ConnectionString)
        daSQL.Fill(dsForm, "tbl_Goroh3")

        Dim dt1 As New DataTable
        dt1 = dsForm.Tables("tbl_Goroh1").Copy
        dt1.TableName = "tbl_Goroh1-1"
        dsForm.Tables.Add(dt1)

        cmbGoroh1.DataSource = Nothing
        cmbGoroh1.Items.Clear()
        cmbGoroh1.DataSource = dsForm.Tables("tbl_Goroh1-1").DefaultView
        cmbGoroh1.DisplayMember = "Sharh"
        cmbGoroh1.ValueMember = "Code"
        chk = True
    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        ClearForm()
    End Sub

    Private Sub btnSeach_Click(sender As Object, e As EventArgs) Handles btnSeach.Click
        grid.Visible = True
        grid.BringToFront()
        Search()
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Dim CodeMahalTaminKonandeh As Integer = 0
        Dim isGorohKala As Boolean = False
        Dim countKala As Integer = 0

        If IsValid() = False Then Exit Sub

        For l As Integer = 0 To chlKala.Items.Count - 1
            chlKala.SetSelected(l, True)
            If chlKala.GetItemChecked(l) Then
                countKala += 1
            End If
        Next

        If countKala = 0 Then
            If MsgBox("آیا میخواهید تمام کالاهای این گروه کالا ذخیره شود؟", MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.YesNo + MsgBoxStyle.Information, "پیام") = MsgBoxResult.Yes Then
                'For l As Integer = 0 To chlKala.Items.Count - 1
                '    chlKala.SetItemChecked(l, True)
                'Next
                isGorohKala = True
            Else
                MsgBox("حداقل یک کالا را انتخاب کنید.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "پیام")
                isGorohKala = False
                Exit Sub
            End If
        End If

        For i As Integer = 0 To chlMarkazPakhsh.Items.Count - 1
            chlMarkazPakhsh.SetSelected(i, True)
            If chlMarkazPakhsh.GetItemChecked(i) Then
                For j As Integer = 0 To chlTaaminKonandeh.Items.Count - 1
                    chlTaaminKonandeh.SetSelected(j, True)
                    If chlTaaminKonandeh.GetItemChecked(j) Then

                        If isGorohKala Then
                            CodeMahalTaminKonandeh = objTools.DLookup("CodeMahal", "tblfo_TaminKonandeh", "ccTaminKonandeh = " & chlTaaminKonandeh.SelectedValue)

                            If CodeMahalTaminKonandeh = chlMarkazPakhsh.SelectedValue Then
                                Using cn As New SqlConnection(ConnectionString)
                                    Using cm As SqlCommand = cn.CreateCommand()
                                        cm.CommandType = CommandType.StoredProcedure
                                        cm.CommandText = "[dbo].[sp_TakhfifatKharid_Insert]"
                                        cm.Parameters.AddWithValue("AzTarikh", mskAzTarikh.Text.Replace("/", ""))
                                        cm.Parameters.AddWithValue("TaTarikh", mskTaTarikh.Text.Replace("/", ""))
                                        cm.Parameters.AddWithValue("CodeMahal", chlMarkazPakhsh.SelectedValue)
                                        cm.Parameters.AddWithValue("ccTaaminKonandeh", chlTaaminKonandeh.SelectedValue)
                                        cm.Parameters.AddWithValue("cckala", -1)
                                        cm.Parameters.AddWithValue("Darsad", txtDarsad.Text.Trim)
                                        cm.Parameters.AddWithValue("UserName", UserName)
                                        cm.Parameters.AddWithValue("CodeDoreh", CodeDoreh)
                                        cm.Parameters.AddWithValue("sg1", IIf(cmbGoroh1.SelectedIndex = -1, -1, cmbGoroh1.SelectedValue))
                                        cm.Parameters.AddWithValue("sg2", IIf(cmbGoroh2.SelectedIndex = -1, -1, cmbGoroh2.SelectedValue))
                                        cm.Parameters.AddWithValue("sg3", IIf(cmbGoroh3.SelectedIndex = -1, -1, cmbGoroh3.SelectedValue))
                                        cm.Connection.Open()
                                        cm.ExecuteNonQuery()
                                        cm.Connection.Close()
                                        cm.Parameters.Clear()
                                    End Using
                                End Using
                            End If
                        Else
                            For k As Integer = 0 To chlKala.Items.Count - 1
                                chlKala.SetSelected(k, True)
                                If chlKala.GetItemChecked(k) Then

                                    CodeMahalTaminKonandeh = objTools.DLookup("CodeMahal", "tblfo_TaminKonandeh", "ccTaminKonandeh = " & chlTaaminKonandeh.SelectedValue)

                                    If CodeMahalTaminKonandeh = chlMarkazPakhsh.SelectedValue Then
                                        Using cn As New SqlConnection(ConnectionString)
                                            Using cm As SqlCommand = cn.CreateCommand()
                                                cm.CommandType = CommandType.StoredProcedure
                                                cm.CommandText = "[dbo].[sp_TakhfifatKharid_Insert]"
                                                cm.Parameters.AddWithValue("AzTarikh", mskAzTarikh.Text.Replace("/", ""))
                                                cm.Parameters.AddWithValue("TaTarikh", mskTaTarikh.Text.Replace("/", ""))
                                                cm.Parameters.AddWithValue("CodeMahal", chlMarkazPakhsh.SelectedValue)
                                                cm.Parameters.AddWithValue("ccTaaminKonandeh", chlTaaminKonandeh.SelectedValue)
                                                cm.Parameters.AddWithValue("cckala", chlKala.SelectedValue)
                                                cm.Parameters.AddWithValue("Darsad", txtDarsad.Text.Trim)
                                                cm.Parameters.AddWithValue("UserName", UserName)
                                                cm.Parameters.AddWithValue("CodeDoreh", CodeDoreh)
                                                cm.Parameters.AddWithValue("sg1", IIf(cmbGoroh1.SelectedIndex = -1, -1, cmbGoroh1.SelectedValue))
                                                cm.Parameters.AddWithValue("sg2", IIf(cmbGoroh2.SelectedIndex = -1, -1, cmbGoroh2.SelectedValue))
                                                cm.Parameters.AddWithValue("sg3", IIf(cmbGoroh3.SelectedIndex = -1, -1, cmbGoroh3.SelectedValue))
                                                cm.Connection.Open()
                                                cm.ExecuteNonQuery()
                                                cm.Connection.Close()
                                                cm.Parameters.Clear()
                                            End Using
                                        End Using
                                    End If

                                End If
                            Next
                        End If

                    End If
                Next
            End If      
        Next
       
        MsgBox("با موفقیت ذخیره شد", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "پیام")

        ClearForm()

    End Sub

    Private Function IsValid() As Boolean
        Dim count = 0
        For i As Integer = 0 To chlMarkazPakhsh.Items.Count - 1
            If chlMarkazPakhsh.GetItemChecked(i) = True Then
                count += 1               
            End If
        Next
        If count = 0 Then
            MsgBox("ابتدا یک مرکز پخش را انتخاب کنید", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Critical, "خطا")
            Return False
        End If
        count = 0
        For i As Integer = 0 To chlTaaminKonandeh.Items.Count - 1
            If chlTaaminKonandeh.GetItemChecked(i) = True Then
                count += 1
            End If
        Next
        If count = 0 Then
            MsgBox("ابتدا یک تامین کننده را انتخاب کنید", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Critical, "خطا")
            Return False
        End If

        If cmbGoroh1.SelectedIndex = -1 Then
            count = 0
            For i As Integer = 0 To chlKala.Items.Count - 1
                If chlKala.GetItemChecked(i) = True Then
                    count += 1
                End If
            Next
            If count = 0 Then
                MsgBox("حداقل یک کالا یا یک گروه کالا را انتخاب کنید.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Critical, "خطا")
                Return False
            End If
        End If


        If txtDarsad.Text = "" Then
            MsgBox("درصد را وارد کنید", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Critical, "خطا")
            Return False
        End If
        If mskAzTarikh.Text = "" Or mskTaTarikh.Text = "" Then
            MsgBox("تاریخ را درست وارد کنید", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Critical, "خطا")
            Return False
        End If
        Return True
    End Function

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
                    cm.CommandText = "[dbo].[sp_TakhfifatKharid_Search]"
                    cm.Parameters.AddWithValue("CodeDoreh", CodeDoreh)
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
        With grid
            .DataSource = Nothing
            .DataSource = dtForm
            .SetDataBinding(dtForm, "")
            .RetrieveStructure()
        End With

        For i As Integer = 0 To grid.CurrentTable.Columns.Count - 1
            grid.CurrentTable.Columns.Item(i).Visible = False
        Next

        grid.CurrentTable.Columns.Item("ccTakhfifat").Caption = "ccTakhfifat"
        grid.CurrentTable.Columns.Item("ccTakhfifat").Visible = False
        grid.CurrentTable.Columns.Item("ccTakhfifat").Width = 50
        grid.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
        grid.CurrentTable.Columns.Item("ccTakhfifat").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

        grid.CurrentTable.Columns.Item("AzTarikhSlash").Caption = "از تاریخ"
        grid.CurrentTable.Columns.Item("AzTarikhSlash").Visible = True
        grid.CurrentTable.Columns.Item("AzTarikhSlash").Width = 100
        grid.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
        grid.CurrentTable.Columns.Item("AzTarikhSlash").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

        grid.CurrentTable.Columns.Item("TaTarikhSlash").Caption = "تا تاریخ"
        grid.CurrentTable.Columns.Item("TaTarikhSlash").Visible = True
        grid.CurrentTable.Columns.Item("TaTarikhSlash").Width = 100
        grid.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
        grid.CurrentTable.Columns.Item("TaTarikhSlash").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

        grid.CurrentTable.Columns.Item("NameMahal").Caption = "مرکز پخش"
        grid.CurrentTable.Columns.Item("NameMahal").Visible = True
        grid.CurrentTable.Columns.Item("NameMahal").Width = 100
        grid.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
        grid.CurrentTable.Columns.Item("NameMahal").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

        grid.CurrentTable.Columns.Item("NameTaminKonandeh").Caption = "نام تامین کننده"
        grid.CurrentTable.Columns.Item("NameTaminKonandeh").Visible = True
        grid.CurrentTable.Columns.Item("NameTaminKonandeh").Width = 200
        grid.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
        grid.CurrentTable.Columns.Item("NameTaminKonandeh").TextAlignment = Janus.Windows.GridEX.TextAlignment.Near

        grid.CurrentTable.Columns.Item("NameKala").Caption = "نام کالا"
        grid.CurrentTable.Columns.Item("NameKala").Visible = True
        grid.CurrentTable.Columns.Item("NameKala").Width = 400
        grid.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
        grid.CurrentTable.Columns.Item("NameKala").TextAlignment = Janus.Windows.GridEX.TextAlignment.Near

        grid.CurrentTable.Columns.Item("txtG1").Caption = "گروه کالا ۱"
        grid.CurrentTable.Columns.Item("txtG1").Visible = True
        grid.CurrentTable.Columns.Item("txtG1").Width = 200
        grid.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
        grid.CurrentTable.Columns.Item("txtG1").TextAlignment = Janus.Windows.GridEX.TextAlignment.Near

        grid.CurrentTable.Columns.Item("txtG2").Caption = "گروه کالا ۲"
        grid.CurrentTable.Columns.Item("txtG2").Visible = True
        grid.CurrentTable.Columns.Item("txtG2").Width = 200
        grid.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
        grid.CurrentTable.Columns.Item("txtG2").TextAlignment = Janus.Windows.GridEX.TextAlignment.Near

        grid.CurrentTable.Columns.Item("txtG3").Caption = "گروه کالا ۳"
        grid.CurrentTable.Columns.Item("txtG3").Visible = True
        grid.CurrentTable.Columns.Item("txtG3").Width = 200
        grid.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
        grid.CurrentTable.Columns.Item("txtG3").TextAlignment = Janus.Windows.GridEX.TextAlignment.Near

        grid.CurrentTable.Columns.Item("NameKala").Caption = "نام کالا"
        grid.CurrentTable.Columns.Item("NameKala").Visible = True
        grid.CurrentTable.Columns.Item("NameKala").Width = 400
        grid.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
        grid.CurrentTable.Columns.Item("NameKala").TextAlignment = Janus.Windows.GridEX.TextAlignment.Near

        grid.CurrentTable.Columns.Item("Darsad").Caption = "درصد"
        grid.CurrentTable.Columns.Item("Darsad").Visible = True
        grid.CurrentTable.Columns.Item("Darsad").Width = 60
        grid.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
        grid.CurrentTable.Columns.Item("Darsad").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

        grid.CurrentTable.Columns.Item("UserName").Caption = "نام کاربر"
        grid.CurrentTable.Columns.Item("UserName").Visible = True
        grid.CurrentTable.Columns.Item("UserName").Width = 140
        grid.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
        grid.CurrentTable.Columns.Item("UserName").FormatString = "###,###"
        grid.CurrentTable.Columns.Item("UserName").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center


    End Sub

    Private Sub cmbGoroh1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbGoroh1.SelectedIndexChanged
        Try
            LoadComboGoroh2()
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "Error IN DataBase ---->" & "cmbGoroh1_SelectedIndexChanged")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, "Error IN ---->" & "cmbGoroh1_SelectedIndexChanged")
        End Try
    End Sub

    Private Sub cmbGoroh2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbGoroh2.SelectedIndexChanged
        Try
            LoadComboGoroh3()
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "Error IN DataBase ---->" & "cmbGoroh2_SelectedIndexChanged")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, "Error IN ---->" & "cmbGoroh2_SelectedIndexChanged")
        End Try
    End Sub
    Private Sub LoadComboGoroh2()
        If Not chk Then Exit Sub
        If dsForm.Tables("tbl_Goroh2").Rows.Count = 0 Then Exit Sub
        Try
            Dim dvGoroh2 As New DataView(dsForm.Tables("tbl_Goroh2"), "CodeLink = " & IIf(IsNothing(cmbGoroh1.SelectedValue), -1, cmbGoroh1.SelectedValue), "", DataViewRowState.OriginalRows)
            cmbGoroh2.DataSource = Nothing
            cmbGoroh2.Items.Clear()
            cmbGoroh2.DataSource = dvGoroh2
            cmbGoroh2.DisplayMember = "Sharh"
            cmbGoroh2.ValueMember = "Code"
            cmbGoroh2.SelectedIndex = -1
            cmbGoroh2.SelectedIndex = -1
            loadKala()
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "Error IN DataBase ---->" & "LoadComboGoroh2")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, "Error IN ---->" & "LoadComboGoroh2")
        End Try

    End Sub
    Private Sub LoadComboGoroh3()
        If Not chk Then Exit Sub
        If dsForm.Tables("tbl_Goroh3").Rows.Count = 0 Then Exit Sub
        Try
            Dim dvGoroh3 As New DataView(dsForm.Tables("tbl_Goroh3"), "CodeLink = " & IIf(IsNothing(cmbGoroh2.SelectedValue), -1, cmbGoroh2.SelectedValue), "", DataViewRowState.OriginalRows)
            cmbGoroh3.DataSource = Nothing
            cmbGoroh3.Items.Clear()
            chk = False
            cmbGoroh3.DataSource = dvGoroh3
            cmbGoroh3.DisplayMember = "Sharh"
            cmbGoroh3.ValueMember = "Code"
            cmbGoroh3.SelectedIndex = -1
            cmbGoroh3.SelectedIndex = -1
            loadKala()
            chk = True
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "Error IN DataBase ---->" & "LoadComboGoroh3")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, "Error IN ---->" & "LoadComboGoroh3")
        End Try
    End Sub
    Private Sub loadKala()
        Dim Strsql As String
        Dim daSQL As SqlDataAdapter

        If dsForm.Tables.Contains("Kala") = True Then
            dsForm.Tables.Remove("Kala")
        End If

        Strsql = "Select CAST(CodeKala AS NVARCHAR(40))+ ' _ ' + NameKala as NameKala , ccKala  From qryAn_Kala "
        Strsql &= " Where 1=1 "
        If cmbGoroh1.SelectedIndex <> -1 Then
            Strsql &= " AND sg1 =" & cmbGoroh1.SelectedValue
        End If
        If cmbGoroh2.SelectedIndex <> -1 Then
            Strsql &= " AND sg2 =" & cmbGoroh2.SelectedValue
        End If
        If cmbGoroh3.SelectedIndex <> -1 Then
            Strsql &= " AND sg3 =" & cmbGoroh3.SelectedValue
        End If

        Strsql &= "  order by ccKala"
        daSQL = New SqlDataAdapter(Strsql, ConnectionString)
        daSQL.Fill(dsForm, "Kala")
        chlKala.DataSource = dsForm.Tables("Kala").DefaultView
        chlKala.DisplayMember = "NameKala"
        chlKala.ValueMember = "ccKala"
        If chlKala.Items.Count > 0 Then
            chlKala.SetSelected(0, True)
        End If
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs)
        Dim Strsql As String
        Dim daSQL As SqlDataAdapter

        If dsForm.Tables.Contains("Kala") = True Then
            dsForm.Tables.Remove("Kala")
        End If

        Strsql = "Select CAST(CodeKala AS NVARCHAR(40))+ ' _ ' + NameKala as NameKala , ccKala  From qryAn_Kala "
        Strsql &= " Where 1=1 "
        If cmbGoroh1.SelectedIndex <> -1 Then
            Strsql &= " AND sg1 =" & cmbGoroh1.SelectedValue
        End If
        If cmbGoroh2.SelectedIndex <> -1 Then
            Strsql &= " AND sg2 =" & cmbGoroh2.SelectedValue
        End If
        If cmbGoroh3.SelectedIndex <> -1 Then
            Strsql &= " AND sg3 =" & cmbGoroh3.SelectedValue
        End If

        Strsql &= "  order by ccKala"
        daSQL = New SqlDataAdapter(Strsql, ConnectionString)
        daSQL.Fill(dsForm, "Kala")
        chlKala.DataSource = dsForm.Tables("Kala").DefaultView
        chlKala.DisplayMember = "NameKala"
        chlKala.ValueMember = "ccKala"
    End Sub

    Private Sub cmbGoroh3_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbGoroh3.SelectedIndexChanged
        loadKala()
    End Sub
End Class
