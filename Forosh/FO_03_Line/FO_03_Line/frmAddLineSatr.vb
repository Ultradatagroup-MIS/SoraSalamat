Public Class frmAddLineSatr
#Region "Variable AND Constant Declration"
    Dim chk As Boolean = False
    Dim dsForm As New DataSet
    Dim dvKala, dvMoshtary, dvForoshandeh As DataView
    Dim tblName As String = ""
    Public ccLine As Integer = 0
#End Region
#Region "Form Event Code"
    Private Sub frmAddLineSatr_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadCombo()
        chk = True
        ClearForm()
        objTools.ChangeKbd(UD_Dll.mdlUtility.KBD.Farsi)
        cmbNoeSatr.SelectedIndex = 0
        cmbNoeSatr.SelectedIndex = 0
    End Sub
    Private Sub cmbNoeSatr_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbNoeSatr.SelectedIndexChanged
        If cmbNoeSatr.SelectedIndex = 0 Then
            grbKala.Visible = False
            grbMoshtary.Visible = False
            grbForoshandeh.Visible = False

            GridEXKala.Visible = False
            GridEXMoshtary.Visible = False
            GridEXForoshandeh.Visible = False
        ElseIf cmbNoeSatr.SelectedIndex = 1 Then
            grbKala.Visible = True
            grbMoshtary.Visible = False
            grbForoshandeh.Visible = False

            GridEXKala.Visible = True
            GridEXMoshtary.Visible = False
            GridEXForoshandeh.Visible = False


        ElseIf cmbNoeSatr.SelectedIndex = 2 Then
            grbKala.Visible = False
            grbMoshtary.Visible = True
            grbForoshandeh.Visible = False

            GridEXKala.Visible = False
            GridEXMoshtary.Visible = True
            GridEXForoshandeh.Visible = False
        ElseIf cmbNoeSatr.SelectedIndex = 3 Then
            grbKala.Visible = False
            grbMoshtary.Visible = False
            grbForoshandeh.Visible = True

            GridEXKala.Visible = False
            GridEXMoshtary.Visible = False
            GridEXForoshandeh.Visible = True
        End If
        ClearForm()
        Search(cmbNoeSatr.SelectedIndex)
    End Sub
    Private Sub cmbGoroh1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbGoroh1.SelectedIndexChanged
        LoadComboGoroh2()
    End Sub
    Private Sub cmbG2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbG2.SelectedIndexChanged
        LoadComboGoroh3()
    End Sub
    Private Sub cmbOstan_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbOstan.SelectedIndexChanged
        LoadComboShahr()
    End Sub
    Private Sub cmbShahr_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbShahr.SelectedIndexChanged
        LoadComboManategh()
    End Sub
#End Region
#Region "Global Form Code"
    Private Sub LoadCombo()
        cmbNoeSatr.Items.Add("-------")
        cmbNoeSatr.Items.Add("کـــــالا")
        cmbNoeSatr.Items.Add("مشتـــری")
        cmbNoeSatr.Items.Add("فروشنـده")

        Dim Strsql As String
        Dim daSQL As SqlDataAdapter

        Strsql = "Select * From tblGL_ShenasehOmomi Where CodeAsli= 152 And CodeFarei<>0 order by Sharh"
        daSQL = New SqlDataAdapter(Strsql, ConnectionString)
        daSQL.Fill(dsForm, "sNoeMoshtary")
        cmbNoeMoshtary.DataSource = Nothing
        cmbNoeMoshtary.Items.Clear()
        cmbNoeMoshtary.DataSource = dsForm.Tables("sNoeMoshtary").DefaultView
        cmbNoeMoshtary.DisplayMember = "Sharh"
        cmbNoeMoshtary.ValueMember = "Code"


        '_________________ Load Ostan Shahr Mantagheh __________________

        Strsql = "Select * From tblGL_ShenasehOmomi Where CodeAsli = 47 And CodeFarei<>0 Or Code = 0 order by Sharh"
        daSQL = New SqlDataAdapter(Strsql, ConnectionString)
        daSQL.Fill(dsForm, "tbl_Ostan")

        Strsql = "Select * From tblGL_ShenasehOmomi Where CodeAsli = 23 And CodeFarei<>0 or Code =0 order by Sharh"
        daSQL = New SqlDataAdapter(Strsql, ConnectionString)
        daSQL.Fill(dsForm, "tbl_Shahr")

        Strsql = "Select * From tblGL_ShenasehOmomi Where CodeAsli= 21 And CodeFarei<>0 Or Code = 0 order by Sharh"
        daSQL = New SqlDataAdapter(Strsql, ConnectionString)
        daSQL.Fill(dsForm, "tbl_Mantagheh")

        Dim dt2 As New DataTable
        dt2 = dsForm.Tables("tbl_Ostan").Copy
        dt2.TableName = "tbl_Ostan1"
        dsForm.Tables.Add(dt2)

        cmbOstan.DataSource = Nothing
        cmbOstan.Items.Clear()
        cmbOstan.DataSource = dsForm.Tables("tbl_Ostan1").DefaultView
        cmbOstan.DisplayMember = "Sharh"
        cmbOstan.ValueMember = "Code"

        '_________________ Load Goroh1 Goroh2 Goroh3 Goroh4 Goroh5 __________________

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

        '__________________________________________________________________________________________________________

        Strsql = "Select * From tblFO_Brand "
        daSQL = New SqlDataAdapter(Strsql, ConnectionString)
        daSQL.Fill(dsForm, "tblBrand")
        cmbBrand.DataSource = Nothing
        cmbBrand.Items.Clear()
        cmbBrand.DataSource = dsForm.Tables("tblBrand").DefaultView
        cmbBrand.DisplayMember = "NameBrand"
        cmbBrand.ValueMember = "ccBrand"

        Strsql = "Select * From qryFO_GorohForosh Where Codemahal = " & CodeMahalFaal & "  And ccGorohForosh <> 0 And Faal = 1"
        daSQL = New SqlDataAdapter(Strsql, ConnectionString)
        daSQL.Fill(dsForm, "tbl_GorohForosh")
        cmbGorohForosh.DataSource = Nothing
        cmbGorohForosh.Items.Clear()
        cmbGorohForosh.DataSource = dsForm.Tables("tbl_GorohForosh").DefaultView
        cmbGorohForosh.DisplayMember = "sharhGorohForosh"
        cmbGorohForosh.ValueMember = "ccGorohForosh"
    End Sub
    Private Sub LoadComboGoroh2()
        If Not chk Then Exit Sub
        If dsForm.Tables("tbl_Goroh2").Rows.Count = 0 Then Exit Sub
        Try
            Dim dvGoroh2 As New DataView(dsForm.Tables("tbl_Goroh2"), "CodeLink = " & IIf(IsNothing(cmbGoroh1.SelectedValue), -1, cmbGoroh1.SelectedValue), "", DataViewRowState.OriginalRows)
            cmbG2.DataSource = Nothing
            cmbG2.Items.Clear()
            cmbG2.DataSource = dvGoroh2
            cmbG2.DisplayMember = "Sharh"
            cmbG2.ValueMember = "Code"
            cmbG2.SelectedIndex = -1

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
            Dim dvGoroh3 As New DataView(dsForm.Tables("tbl_Goroh3"), "CodeLink = " & IIf(IsNothing(cmbG2.SelectedValue), -1, cmbG2.SelectedValue), "", DataViewRowState.OriginalRows)
            cmbG3.DataSource = Nothing
            cmbG3.Items.Clear()
            chk = False
            cmbG3.DataSource = dvGoroh3
            cmbG3.DisplayMember = "Sharh"
            cmbG3.ValueMember = "Code"
            cmbG3.SelectedIndex = -1

            chk = True
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "Error IN DataBase ---->" & "LoadComboGoroh3")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, "Error IN ---->" & "LoadComboGoroh3")
        End Try
    End Sub
    Private Sub LoadComboShahr()
        If Not chk Then Exit Sub
        If dsForm.Tables("tbl_Shahr").Rows.Count = 0 Then Exit Sub
        Try
            Dim dvShahr As New DataView(dsForm.Tables("tbl_Shahr"), "CodeLink = " & IIf(IsNothing(cmbOstan.SelectedValue), -1, cmbOstan.SelectedValue), "", DataViewRowState.OriginalRows)
            cmbShahr.DataSource = Nothing
            cmbShahr.Items.Clear()
            cmbShahr.DataSource = dvShahr
            cmbShahr.DisplayMember = "Sharh"
            cmbShahr.ValueMember = "Code"
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "Error IN DataBase ---->" & "LoadComboShahr")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, "Error IN ---->" & "LoadComboShahr")
        End Try
    End Sub
    Private Sub LoadComboManategh()
        If Not chk Then Exit Sub
        If dsForm.Tables("tbl_Mantagheh").Rows.Count = 0 Then Exit Sub
        Try
            Dim dvMantagheh As New DataView(dsForm.Tables("tbl_Mantagheh"), "CodeLink = " & IIf(IsNothing(cmbShahr.SelectedValue), -1, cmbShahr.SelectedValue), "", DataViewRowState.OriginalRows)
            cmbMantagheh.DataSource = Nothing
            cmbMantagheh.Items.Clear()
            cmbMantagheh.DataSource = dvMantagheh
            cmbMantagheh.DisplayMember = "Sharh"
            cmbMantagheh.ValueMember = "Code"
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "Error IN DataBase ---->" & "LoadComboManategh")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, "Error IN ---->" & "LoadComboManategh")
        End Try
    End Sub
    Private Sub ClearForm()

        cmbBrand.SelectedIndex = -1
        cmbBrand.SelectedIndex = -1

        cmbGoroh1.SelectedIndex = -1
        cmbGoroh1.SelectedIndex = -1

        cmbG2.SelectedIndex = -1
        cmbG2.SelectedIndex = -1

        cmbG3.SelectedIndex = -1
        cmbG3.SelectedIndex = -1

        cmbNoeSenf.SelectedIndex = -1
        cmbNoeSenf.SelectedIndex = -1

        cmbNoeMoshtary.SelectedIndex = -1
        cmbNoeMoshtary.SelectedIndex = -1

        cmbOstan.SelectedIndex = -1
        cmbOstan.SelectedIndex = -1

        cmbShahr.SelectedIndex = -1
        cmbShahr.SelectedIndex = -1

        cmbMantagheh.SelectedIndex = -1
        cmbMantagheh.SelectedIndex = -1

        cmbGorohForosh.SelectedIndex = -1
        cmbGorohForosh.SelectedIndex = -1

    End Sub
    Private Sub Search(ByVal Type As Integer)
        '' Type ---> 1 : Kala , 2 : Moshtary , 3 : Foroshandeh
        If Type = 0 Then
            Exit Sub
        End If

        Dim cnSQL As New SqlConnection
        Dim cmSQL As New SqlCommand
        Dim daSQL As New SqlDataAdapter
        Dim strSQL As String = ""

        If Type = 1 Then
            tblName = "tblKala"
        ElseIf Type = 2 Then
            tblName = "tblMoshtary"
        ElseIf Type = 3 Then
            tblName = "tblForoshandeh"
        End If

        If dsForm.Tables.Contains(tblName) Then
            dsForm.Tables.Remove(tblName)
        End If

        Try
            strSQL = "Sales.spLine_InsertSatr_Search "

            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            cmSQL.Parameters.AddWithValue("ccLine", ccLine)
            cmSQL.Parameters.AddWithValue("CodeMahal", CodeMahalFaal)
            cmSQL.Parameters.AddWithValue("Type", cmbNoeSatr.SelectedIndex)
            cmSQL.Parameters.AddWithValue("sG1", IIf(cmbGoroh1.SelectedIndex = -1, 0, cmbGoroh1.SelectedValue))
            cmSQL.Parameters.AddWithValue("sG2", IIf(cmbG2.SelectedIndex = -1, 0, cmbG2.SelectedValue))
            cmSQL.Parameters.AddWithValue("sG3", IIf(cmbG3.SelectedIndex = -1, 0, cmbG3.SelectedValue))
            cmSQL.Parameters.AddWithValue("ccBrand", IIf(cmbBrand.SelectedIndex = -1, 0, cmbBrand.SelectedValue))
            cmSQL.Parameters.AddWithValue("sNoeMoshtary", IIf(cmbNoeMoshtary.SelectedIndex = -1, 0, cmbNoeMoshtary.SelectedValue))
            cmSQL.Parameters.AddWithValue("sNoeSenf", IIf(cmbNoeSenf.SelectedIndex = -1, 0, cmbNoeSenf.SelectedValue))
            cmSQL.Parameters.AddWithValue("sOstan", IIf(cmbOstan.SelectedIndex = -1, 0, cmbOstan.SelectedValue))
            cmSQL.Parameters.AddWithValue("sShahr", IIf(cmbShahr.SelectedIndex = -1, 0, cmbShahr.SelectedValue))
            cmSQL.Parameters.AddWithValue("sMantagheh", IIf(cmbMantagheh.SelectedIndex = -1, 0, cmbMantagheh.SelectedValue))
            cmSQL.Parameters.AddWithValue("ccGorohForosh", IIf(cmbGorohForosh.SelectedIndex = -1, 0, cmbGorohForosh.SelectedValue))

            daSQL = New SqlDataAdapter(cmSQL)
            daSQL.Fill(dsForm, tblName)

            '------------Adding Columns------------
            dsForm.Tables(tblName).Columns.Add("Taeed", GetType(Boolean))
            '--------------------------------------

            Dim dr As DataRow
            For Each dr In dsForm.Tables(tblName).Rows
                dr("Taeed") = False
            Next

            If Type = 1 Then
                dvKala = New DataView(dsForm.Tables(tblName))
                dvKala.Sort = "CodeKala ASC"

                dvKala.AllowNew = False
                dvKala.AllowDelete = False
                dvKala.AllowEdit = True
            ElseIf Type = 2 Then
                dvMoshtary = New DataView(dsForm.Tables(tblName))
                dvMoshtary.Sort = "CodeMoshtary ASC"

                dvMoshtary.AllowNew = False
                dvMoshtary.AllowDelete = False
                dvMoshtary.AllowEdit = True
            ElseIf Type = 3 Then
                dvForoshandeh = New DataView(dsForm.Tables(tblName))
                dvForoshandeh.Sort = "CodeForoshandeh ASC"

                dvForoshandeh.AllowNew = False
                dvForoshandeh.AllowDelete = False
                dvForoshandeh.AllowEdit = True
            End If

            cmSQL = Nothing
            daSQL = Nothing
            cnSQL.Close()

            If Type = 1 Then
                SetGridKala()
                With GridEXKala
                    .Visible = True
                    .DataSource = Nothing
                    .DataSource = dvKala
                End With
            ElseIf Type = 2 Then
                SetGridMoshtary()
                With GridEXMoshtary
                    .Visible = True
                    .DataSource = Nothing
                    .DataSource = dvMoshtary
                End With
            ElseIf Type = 3 Then
                SetGridForoshandeh()
                With GridEXForoshandeh
                    .Visible = True
                    .DataSource = Nothing
                    .DataSource = dvForoshandeh
                End With
            End If

            Me.CenterToScreen()
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> Search ")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> Search ")
        End Try
    End Sub
    Private Sub SetGridKala()
        If dvKala.Count = 0 Then
            Exit Sub
        End If

        Try
            With GridEXKala
                .DataSource = Nothing
                .DataSource = dsForm.Tables(tblName).DefaultView
                .SetDataBinding(dsForm.Tables(tblName).DefaultView, "")
                .RetrieveStructure()
            End With

            For i As Integer = 0 To GridEXKala.CurrentTable.Columns.Count - 1
                GridEXKala.CurrentTable.Columns.Item(i).Visible = False
            Next

            GridEXKala.CurrentTable.Columns.Item("Taeed").Caption = "تاييد"
            GridEXKala.CurrentTable.Columns.Item("Taeed").Visible = True
            GridEXKala.CurrentTable.Columns.Item("Taeed").Width = 40
            GridEXKala.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXKala.CurrentTable.Columns.Item("Taeed").Selectable = True
            GridEXKala.CurrentTable.Columns.Item("Taeed").ActAsSelector = True
            GridEXKala.CurrentTable.Columns.Item("Taeed").Position = 0
            GridEXKala.CurrentTable.Columns.Item("Taeed").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXKala.CurrentTable.Columns.Item("Taeed").HeaderAlignment = TextAlignment.Center

            GridEXKala.CurrentTable.Columns.Item("CodeKala").Caption = "کد کالا"
            GridEXKala.CurrentTable.Columns.Item("CodeKala").Visible = True
            GridEXKala.CurrentTable.Columns.Item("CodeKala").Width = 100
            GridEXKala.CurrentTable.Columns.Item("CodeKala").EditType = EditType.NoEdit
            GridEXKala.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXKala.CurrentTable.Columns.Item("CodeKala").Position = 1
            GridEXKala.CurrentTable.Columns.Item("CodeKala").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXKala.CurrentTable.Columns.Item("CodeKala").HeaderAlignment = TextAlignment.Center

            GridEXKala.CurrentTable.Columns.Item("NameKala").Caption = "نام کالا"
            GridEXKala.CurrentTable.Columns.Item("NameKala").Visible = True
            GridEXKala.CurrentTable.Columns.Item("NameKala").Width = 250
            GridEXKala.CurrentTable.Columns.Item("NameKala").EditType = EditType.NoEdit
            GridEXKala.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXKala.CurrentTable.Columns.Item("NameKala").Position = 2
            GridEXKala.CurrentTable.Columns.Item("NameKala").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXKala.CurrentTable.Columns.Item("NameKala").HeaderAlignment = TextAlignment.Center

            GridEXKala.CurrentTable.Columns.Item("txtsG1").Caption = "گروه 1 کالا"
            GridEXKala.CurrentTable.Columns.Item("txtsG1").Visible = True
            GridEXKala.CurrentTable.Columns.Item("txtsG1").Width = 150
            GridEXKala.CurrentTable.Columns.Item("txtsG1").EditType = EditType.NoEdit
            GridEXKala.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXKala.CurrentTable.Columns.Item("txtsG1").Position = 3
            GridEXKala.CurrentTable.Columns.Item("txtsG1").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXKala.CurrentTable.Columns.Item("txtsG1").HeaderAlignment = TextAlignment.Center

            GridEXKala.CurrentTable.Columns.Item("txtsG2").Caption = "گروه 2 کالا"
            GridEXKala.CurrentTable.Columns.Item("txtsG2").Visible = True
            GridEXKala.CurrentTable.Columns.Item("txtsG2").Width = 150
            GridEXKala.CurrentTable.Columns.Item("txtsG2").EditType = EditType.NoEdit
            GridEXKala.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXKala.CurrentTable.Columns.Item("txtsG2").Position = 4
            GridEXKala.CurrentTable.Columns.Item("txtsG2").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXKala.CurrentTable.Columns.Item("txtsG2").HeaderAlignment = TextAlignment.Center

            GridEXKala.CurrentTable.Columns.Item("txtsG3").Caption = "گروه 3 کالا"
            GridEXKala.CurrentTable.Columns.Item("txtsG3").Visible = True
            GridEXKala.CurrentTable.Columns.Item("txtsG3").Width = 150
            GridEXKala.CurrentTable.Columns.Item("txtsG3").EditType = EditType.NoEdit
            GridEXKala.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXKala.CurrentTable.Columns.Item("txtsG3").Position = 5
            GridEXKala.CurrentTable.Columns.Item("txtsG3").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXKala.CurrentTable.Columns.Item("txtsG3").HeaderAlignment = TextAlignment.Center

            GridEXKala.CurrentTable.Columns.Item("NameBrand").Caption = "نام برند"
            GridEXKala.CurrentTable.Columns.Item("NameBrand").Visible = True
            GridEXKala.CurrentTable.Columns.Item("NameBrand").Width = 120
            GridEXKala.CurrentTable.Columns.Item("NameBrand").EditType = EditType.NoEdit
            GridEXKala.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXKala.CurrentTable.Columns.Item("NameBrand").Position = 6
            GridEXKala.CurrentTable.Columns.Item("NameBrand").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXKala.CurrentTable.Columns.Item("NameBrand").HeaderAlignment = TextAlignment.Center

            GridEXKala.CurrentTable.Columns.Item("ccKala").Caption = "ccKala"
            GridEXKala.CurrentTable.Columns.Item("ccKala").Visible = False
            GridEXKala.CurrentTable.Columns.Item("ccKala").Width = 0
            GridEXKala.CurrentTable.Columns.Item("ccKala").EditType = EditType.NoEdit
            GridEXKala.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXKala.CurrentTable.Columns.Item("ccKala").Position = 7
            GridEXKala.CurrentTable.Columns.Item("ccKala").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXKala.CurrentTable.Columns.Item("ccKala").HeaderAlignment = TextAlignment.Center

            For i As Integer = 0 To GridEXKala.RootTable.Columns.Count - 1
                If GridEXKala.RootTable.Columns(i).Type.IsValueType Then
                    GridEXKala.RootTable.Columns(i).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                    GridEXKala.RootTable.Columns(i).FormatMode = Janus.Windows.GridEX.FormatMode.UseIFormattable
                    GridEXKala.RootTable.Columns(i).FormatString = "G"
                    GridEXKala.RootTable.Columns(i).TotalFormatMode = Janus.Windows.GridEX.FormatMode.UseIFormattable
                    GridEXKala.RootTable.Columns(i).TotalFormatString = "G"
                End If
            Next

            Me.CenterToScreen()
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> SetGridKala ")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> SetGridKala ")
        End Try
    End Sub
    Private Sub SetGridMoshtary()
        If dvMoshtary.Count = 0 Then
            Exit Sub
        End If

        Try
            With GridEXMoshtary
                .DataSource = Nothing
                .DataSource = dsForm.Tables(tblName).DefaultView
                .SetDataBinding(dsForm.Tables(tblName).DefaultView, "")
                .RetrieveStructure()
            End With

            For i As Integer = 0 To GridEXMoshtary.CurrentTable.Columns.Count - 1
                GridEXMoshtary.CurrentTable.Columns.Item(i).Visible = False
            Next

            GridEXMoshtary.CurrentTable.Columns.Item("Taeed").Caption = "تاييد"
            GridEXMoshtary.CurrentTable.Columns.Item("Taeed").Visible = True
            GridEXMoshtary.CurrentTable.Columns.Item("Taeed").Width = 40
            GridEXMoshtary.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXMoshtary.CurrentTable.Columns.Item("Taeed").Selectable = True
            GridEXMoshtary.CurrentTable.Columns.Item("Taeed").ActAsSelector = True
            GridEXMoshtary.CurrentTable.Columns.Item("Taeed").Position = 0
            GridEXMoshtary.CurrentTable.Columns.Item("Taeed").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXMoshtary.CurrentTable.Columns.Item("Taeed").HeaderAlignment = TextAlignment.Center

            GridEXMoshtary.CurrentTable.Columns.Item("CodeMoshtary").Caption = "کد مشتری"
            GridEXMoshtary.CurrentTable.Columns.Item("CodeMoshtary").Visible = True
            GridEXMoshtary.CurrentTable.Columns.Item("CodeMoshtary").Width = 100
            GridEXMoshtary.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXMoshtary.CurrentTable.Columns.Item("CodeMoshtary").Position = 1
            GridEXMoshtary.CurrentTable.Columns.Item("CodeMoshtary").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXMoshtary.CurrentTable.Columns.Item("CodeMoshtary").HeaderAlignment = TextAlignment.Center

            GridEXMoshtary.CurrentTable.Columns.Item("NameMoshtary").Caption = "نام مشتـری"
            GridEXMoshtary.CurrentTable.Columns.Item("NameMoshtary").Visible = True
            GridEXMoshtary.CurrentTable.Columns.Item("NameMoshtary").Width = 200
            GridEXMoshtary.CurrentTable.Columns.Item("NameMoshtary").EditType = EditType.NoEdit
            GridEXMoshtary.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXMoshtary.CurrentTable.Columns.Item("NameMoshtary").Position = 2
            GridEXMoshtary.CurrentTable.Columns.Item("NameMoshtary").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXMoshtary.CurrentTable.Columns.Item("NameMoshtary").HeaderAlignment = TextAlignment.Center

            GridEXMoshtary.CurrentTable.Columns.Item("txtNoeMoshtary").Caption = "نوع مشتری"
            GridEXMoshtary.CurrentTable.Columns.Item("txtNoeMoshtary").Visible = True
            GridEXMoshtary.CurrentTable.Columns.Item("txtNoeMoshtary").Width = 130
            GridEXMoshtary.CurrentTable.Columns.Item("txtNoeMoshtary").EditType = EditType.NoEdit
            GridEXMoshtary.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXMoshtary.CurrentTable.Columns.Item("txtNoeMoshtary").Position = 3
            GridEXMoshtary.CurrentTable.Columns.Item("txtNoeMoshtary").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXMoshtary.CurrentTable.Columns.Item("txtNoeMoshtary").HeaderAlignment = TextAlignment.Center

            GridEXMoshtary.CurrentTable.Columns.Item("txtNoeSenf").Caption = "نوع صنـف"
            GridEXMoshtary.CurrentTable.Columns.Item("txtNoeSenf").Visible = True
            GridEXMoshtary.CurrentTable.Columns.Item("txtNoeSenf").Width = 130
            GridEXMoshtary.CurrentTable.Columns.Item("txtNoeSenf").EditType = EditType.NoEdit
            GridEXMoshtary.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXMoshtary.CurrentTable.Columns.Item("txtNoeSenf").Position = 4
            GridEXMoshtary.CurrentTable.Columns.Item("txtNoeSenf").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXMoshtary.CurrentTable.Columns.Item("txtNoeSenf").HeaderAlignment = TextAlignment.Center

            GridEXMoshtary.CurrentTable.Columns.Item("txtOstan").Caption = "استــان"
            GridEXMoshtary.CurrentTable.Columns.Item("txtOstan").Visible = True
            GridEXMoshtary.CurrentTable.Columns.Item("txtOstan").Width = 100
            GridEXMoshtary.CurrentTable.Columns.Item("txtOstan").EditType = EditType.NoEdit
            GridEXMoshtary.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXMoshtary.CurrentTable.Columns.Item("txtOstan").Position = 5
            GridEXMoshtary.CurrentTable.Columns.Item("txtOstan").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXMoshtary.CurrentTable.Columns.Item("txtOstan").HeaderAlignment = TextAlignment.Center

            GridEXMoshtary.CurrentTable.Columns.Item("txtShahr").Caption = "شهــر"
            GridEXMoshtary.CurrentTable.Columns.Item("txtShahr").Visible = True
            GridEXMoshtary.CurrentTable.Columns.Item("txtShahr").Width = 100
            GridEXMoshtary.CurrentTable.Columns.Item("txtShahr").EditType = EditType.NoEdit
            GridEXMoshtary.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXMoshtary.CurrentTable.Columns.Item("txtShahr").Position = 6
            GridEXMoshtary.CurrentTable.Columns.Item("txtShahr").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXMoshtary.CurrentTable.Columns.Item("txtShahr").HeaderAlignment = TextAlignment.Center

            GridEXMoshtary.CurrentTable.Columns.Item("txtMantagheh").Caption = "منطقــه"
            GridEXMoshtary.CurrentTable.Columns.Item("txtMantagheh").Visible = True
            GridEXMoshtary.CurrentTable.Columns.Item("txtMantagheh").Width = 160
            GridEXMoshtary.CurrentTable.Columns.Item("txtMantagheh").EditType = EditType.NoEdit
            GridEXMoshtary.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXMoshtary.CurrentTable.Columns.Item("txtMantagheh").Position = 7
            GridEXMoshtary.CurrentTable.Columns.Item("txtMantagheh").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXMoshtary.CurrentTable.Columns.Item("txtMantagheh").HeaderAlignment = TextAlignment.Center

            GridEXMoshtary.CurrentTable.Columns.Item("ccMoshtary").Caption = "ccMoshtary"
            GridEXMoshtary.CurrentTable.Columns.Item("ccMoshtary").Visible = False
            GridEXMoshtary.CurrentTable.Columns.Item("ccMoshtary").Width = 0
            GridEXMoshtary.CurrentTable.Columns.Item("ccMoshtary").EditType = EditType.NoEdit
            GridEXMoshtary.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXMoshtary.CurrentTable.Columns.Item("ccMoshtary").Position = 8
            GridEXMoshtary.CurrentTable.Columns.Item("ccMoshtary").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXMoshtary.CurrentTable.Columns.Item("ccMoshtary").HeaderAlignment = TextAlignment.Center

            For i As Integer = 0 To GridEXMoshtary.RootTable.Columns.Count - 1
                If GridEXMoshtary.RootTable.Columns(i).Type.IsValueType Then
                    GridEXMoshtary.RootTable.Columns(i).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                    GridEXMoshtary.RootTable.Columns(i).FormatMode = Janus.Windows.GridEX.FormatMode.UseIFormattable
                    GridEXMoshtary.RootTable.Columns(i).FormatString = "G"
                    GridEXMoshtary.RootTable.Columns(i).TotalFormatMode = Janus.Windows.GridEX.FormatMode.UseIFormattable
                    GridEXMoshtary.RootTable.Columns(i).TotalFormatString = "G"
                End If
            Next

            Me.CenterToScreen()
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> SetGridMoshtary ")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> SetGridMoshtary ")
        End Try
    End Sub
    Private Sub SetGridForoshandeh()
        If dvForoshandeh.Count = 0 Then
            Exit Sub
        End If

        Try
            With GridEXForoshandeh
                .DataSource = Nothing
                .DataSource = dsForm.Tables(tblName).DefaultView
                .SetDataBinding(dsForm.Tables(tblName).DefaultView, "")
                .RetrieveStructure()
            End With

            For i As Integer = 0 To GridEXForoshandeh.CurrentTable.Columns.Count - 1
                GridEXForoshandeh.CurrentTable.Columns.Item(i).Visible = False
            Next

            GridEXForoshandeh.CurrentTable.Columns.Item("Taeed").Caption = "تاييد"
            GridEXForoshandeh.CurrentTable.Columns.Item("Taeed").Visible = True
            GridEXForoshandeh.CurrentTable.Columns.Item("Taeed").Width = 40
            GridEXForoshandeh.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXForoshandeh.CurrentTable.Columns.Item("Taeed").Selectable = True
            GridEXForoshandeh.CurrentTable.Columns.Item("Taeed").ActAsSelector = True
            GridEXForoshandeh.CurrentTable.Columns.Item("Taeed").Position = 0
            GridEXForoshandeh.CurrentTable.Columns.Item("Taeed").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXForoshandeh.CurrentTable.Columns.Item("Taeed").HeaderAlignment = TextAlignment.Center

            GridEXForoshandeh.CurrentTable.Columns.Item("CodeForoshandeh").Caption = "کـد فروشنده"
            GridEXForoshandeh.CurrentTable.Columns.Item("CodeForoshandeh").Visible = True
            GridEXForoshandeh.CurrentTable.Columns.Item("CodeForoshandeh").Width = 100
            GridEXForoshandeh.CurrentTable.Columns.Item("CodeForoshandeh").EditType = EditType.NoEdit
            GridEXForoshandeh.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXForoshandeh.CurrentTable.Columns.Item("CodeForoshandeh").Position = 1
            GridEXForoshandeh.CurrentTable.Columns.Item("CodeForoshandeh").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXForoshandeh.CurrentTable.Columns.Item("CodeForoshandeh").HeaderAlignment = TextAlignment.Center

            GridEXForoshandeh.CurrentTable.Columns.Item("NameForoshandeh").Caption = "نام فروشنده"
            GridEXForoshandeh.CurrentTable.Columns.Item("NameForoshandeh").Visible = True
            GridEXForoshandeh.CurrentTable.Columns.Item("NameForoshandeh").Width = 150
            GridEXForoshandeh.CurrentTable.Columns.Item("NameForoshandeh").EditType = EditType.NoEdit
            GridEXForoshandeh.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXForoshandeh.CurrentTable.Columns.Item("NameForoshandeh").Position = 2
            GridEXForoshandeh.CurrentTable.Columns.Item("NameForoshandeh").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXForoshandeh.CurrentTable.Columns.Item("NameForoshandeh").HeaderAlignment = TextAlignment.Center

            GridEXForoshandeh.CurrentTable.Columns.Item("SharhGorohForosh").Caption = "نام گروه فروش"
            GridEXForoshandeh.CurrentTable.Columns.Item("SharhGorohForosh").Visible = True
            GridEXForoshandeh.CurrentTable.Columns.Item("SharhGorohForosh").Width = 210
            GridEXForoshandeh.CurrentTable.Columns.Item("SharhGorohForosh").EditType = EditType.NoEdit
            GridEXForoshandeh.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXForoshandeh.CurrentTable.Columns.Item("SharhGorohForosh").Position = 3
            GridEXForoshandeh.CurrentTable.Columns.Item("SharhGorohForosh").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXForoshandeh.CurrentTable.Columns.Item("SharhGorohForosh").HeaderAlignment = TextAlignment.Center

            GridEXForoshandeh.CurrentTable.Columns.Item("NameSarparast").Caption = "نام سرپـرست"
            GridEXForoshandeh.CurrentTable.Columns.Item("NameSarparast").Visible = True
            GridEXForoshandeh.CurrentTable.Columns.Item("NameSarparast").Width = 210
            GridEXForoshandeh.CurrentTable.Columns.Item("NameSarparast").EditType = EditType.NoEdit
            GridEXForoshandeh.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXForoshandeh.CurrentTable.Columns.Item("NameSarparast").Position = 4
            GridEXForoshandeh.CurrentTable.Columns.Item("NameSarparast").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXForoshandeh.CurrentTable.Columns.Item("NameSarparast").HeaderAlignment = TextAlignment.Center

            GridEXForoshandeh.CurrentTable.Columns.Item("ccForoshandeh").Caption = "ccForoshandeh"
            GridEXForoshandeh.CurrentTable.Columns.Item("ccForoshandeh").Visible = False
            GridEXForoshandeh.CurrentTable.Columns.Item("ccForoshandeh").Width = 0
            GridEXForoshandeh.CurrentTable.Columns.Item("ccForoshandeh").EditType = EditType.NoEdit
            GridEXForoshandeh.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXForoshandeh.CurrentTable.Columns.Item("ccForoshandeh").Position = 5
            GridEXForoshandeh.CurrentTable.Columns.Item("ccForoshandeh").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXForoshandeh.CurrentTable.Columns.Item("ccForoshandeh").HeaderAlignment = TextAlignment.Center

            For i As Integer = 0 To GridEXForoshandeh.RootTable.Columns.Count - 1
                If GridEXForoshandeh.RootTable.Columns(i).Type.IsValueType Then
                    GridEXForoshandeh.RootTable.Columns(i).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                    GridEXForoshandeh.RootTable.Columns(i).FormatMode = Janus.Windows.GridEX.FormatMode.UseIFormattable
                    GridEXForoshandeh.RootTable.Columns(i).FormatString = "G"
                    GridEXForoshandeh.RootTable.Columns(i).TotalFormatMode = Janus.Windows.GridEX.FormatMode.UseIFormattable
                    GridEXForoshandeh.RootTable.Columns(i).TotalFormatString = "G"
                End If
            Next

            Me.CenterToScreen()
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> SetGridForoshandeh ")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> SetGridForoshandeh ")
        End Try
    End Sub
    Private Function Save(ByVal PK As Integer) As Boolean
        Save = False

        Dim cnSQL As New SqlConnection
        Dim cmSQL As New SqlCommand
        Dim strSQL As String = ""

        Try
            strSQL = "Sales.spLine_InsertSatr "

            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            cmSQL.Parameters.AddWithValue("ccLine", ccLine)
            cmSQL.Parameters.AddWithValue("Type", cmbNoeSatr.SelectedIndex)
            cmSQL.Parameters.AddWithValue("PK", PK)

            cmSQL.ExecuteNonQuery()

            cmSQL = Nothing
            cnSQL.Close()

            Save = True
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> SetGridForoshandeh ")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> SetGridForoshandeh ")
        End Try
    End Function
#End Region
#Region "From Buttons "
    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        ClearForm()
        Search(cmbNoeSatr.SelectedIndex)
    End Sub
    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        Search(cmbNoeSatr.SelectedIndex)
    End Sub
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If cmbNoeSatr.SelectedIndex = 1 Then
            For i As Integer = 0 To GridEXKala.RowCount - 1
                If GridEXKala.GetRows(i).Cells("Taeed").Value = True Then
                    Save(GridEXKala.GetRows(i).Cells("ccKala").Text.Replace(",", ""))
                End If
            Next
        ElseIf cmbNoeSatr.SelectedIndex = 2 Then
            For i As Integer = 0 To GridEXMoshtary.RowCount - 1
                If GridEXMoshtary.GetRows(i).Cells("Taeed").Value = True Then
                    Save(GridEXMoshtary.GetRows(i).Cells("ccMoshtary").Text.Replace(",", ""))
                End If
            Next
        ElseIf cmbNoeSatr.SelectedIndex = 3 Then
            For i As Integer = 0 To GridEXForoshandeh.RowCount - 1
                If GridEXForoshandeh.GetRows(i).Cells("Taeed").Value = True Then
                    Save(GridEXForoshandeh.GetRows(i).Cells("ccForoshandeh").Text.Replace(",", ""))
                End If
            Next
        End If

        ClearForm()
        Search(cmbNoeSatr.SelectedIndex)

    End Sub
    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub
#End Region
#Region "From Stored Procedures and Reports "
    '' Sales.spLine_InsertSatr_Search
    '' Sales.spLine_InsertSatr
#End Region
End Class