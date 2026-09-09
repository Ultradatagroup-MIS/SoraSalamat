Public Class frmGL_EditDataEnteghali
#Region "Variable AND Constant Declration"
    Const cntCodeSubSystem As Long = 1000145
    Dim dvForm As DataView
    Dim ErrPro As New ErrorProvider
    Dim dsForm As New DataSet
    Private SN As Integer
    Dim tPos As Integer
    Dim txtCaption As String
    Private WithEvents BS As New UD_Dll.PassString
    Dim Flg_Load As Boolean = False
#End Region
    Private Sub frmGL_EditDataEnteghali_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetParameter()
        LoadCombo()
        SetForm()

        cmbNoeAmalyat.SelectedIndex = 0
        cmbMarkazPakhsh.SelectedValue = CodeMahalFaal
        cmbDoreh.SelectedValue = CodeDoreh
        mskAzTarikh.Text = TarikhEmrooz
        mskTaTarikh.Text = TarikhEmrooz

        Flg_Load = True

        objTools.ChangeKbd(UD_Dll.mdlUtility.KBD.Farsi)
    End Sub
    Private Sub SetParameter()

        Dim commands As String = Microsoft.VisualBasic.Command()
        If commands.Length = 0 Then

            UserName = "Administrator"
            UserPassWord = "66998833"
            NameMahalFaal = "تهران"
            CodeMahalFaal = "1"
            PersonelCode = "1"
            PersonelName = "Administrator"
            CodeDoreh = "1394"
            txtCaption = "ویرایش اطلاعات انتقالی"
            Me.Text = txtCaption
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
            objCode.UserName = UserName
        End If

    End Sub
    Private Sub LoadCombo()
        Dim cnSQL As New SqlConnection
        Dim cmSQL As New SqlCommand
        Dim daSQL As New SqlDataAdapter
        Dim dr As DataRow
        Dim strSQL As String = ""

        Try
            '_________________ Load Amalyat __________________
            cmbNoeAmalyat.Items.Add("همــه مــوارد")
            cmbNoeAmalyat.Items.Add("حـوالــه به انبــــار")
            cmbNoeAmalyat.Items.Add("رسیـــــــــــــــــد")
            cmbNoeAmalyat.Items.Add("مرجوعی به تامین کننده")
            cmbNoeAmalyat.Items.Add("رسیــــــــد اول دوره")
            cmbNoeAmalyat.Items.Add("مرجـــوعی از مشتـــری")
            cmbNoeAmalyat.Items.Add("اشـانتیــــــــــــون")
            cmbNoeAmalyat.Items.Add("کسر و اضافه از انبـار ")
            cmbNoeAmalyat.Items.Add("انبـــار به انبـــار")
            cmbNoeAmalyat.Items.Add("پیــش فاکتــــور")
            '_________________ Load Amalyat __________________

            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()

            '_________________ Load Markaz Pakhsh __________________
            strSQL = "Global.spMarkazPakhsh_LoadCombo "

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            cmSQL.Parameters.AddWithValue("CodeMahal", 0)

            daSQL = New SqlDataAdapter(cmSQL)
            daSQL.Fill(dsForm, "tbl_MarkazPakhsh")

            'dr = dsForm.Tables("tbl_MarkazPakhsh").NewRow
            'dr("NameMahal") = "همـه"
            'dr("CodeMahal") = 0
            'dsForm.Tables("tbl_MarkazPakhsh").Rows.Add(dr)

            cmbMarkazPakhsh.DataSource = Nothing
            cmbMarkazPakhsh.Items.Clear()
            cmbMarkazPakhsh.DataSource = dsForm.Tables("tbl_MarkazPakhsh").DefaultView
            cmbMarkazPakhsh.DisplayMember = "NameMahal"
            cmbMarkazPakhsh.ValueMember = "CodeMahal"
            '_________________ Load Markaz Pakhsh __________________

            '_________________ Load Doreh __________________
            strSQL = "Global.spCodeDoreh_LoadCombo "

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            daSQL = New SqlDataAdapter(cmSQL)
            daSQL.Fill(dsForm, "tbl_Doreh")

            'dr = dsForm.Tables("tbl_Doreh").NewRow
            'dr("txtDoreh") = "همـه"
            'dr("CodeDoreh") = 0
            'dsForm.Tables("tbl_Doreh").Rows.Add(dr)

            cmbDoreh.DataSource = Nothing
            cmbDoreh.Items.Clear()
            cmbDoreh.DataSource = dsForm.Tables("tbl_Doreh").DefaultView
            cmbDoreh.DisplayMember = "txtDoreh"
            cmbDoreh.ValueMember = "CodeDoreh"
            '_________________ Load Doreh __________________
            '_________________ Load Tamin Konandeh __________________
            strSQL = "Global.spTaminKonandeh_LoadCombo "

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            daSQL = New SqlDataAdapter(cmSQL)
            daSQL.Fill(dsForm, "tbl_TaminKonandeh")

            'dr = dsForm.Tables("tbl_TaminKonandeh").NewRow
            'dr("NameTaminKonandeh") = "همـه"
            'dr("ccTaminKonandeh") = 0
            'dsForm.Tables("tbl_TaminKonandeh").Rows.Add(dr)

            cmbTaminKonandeh.DataSource = Nothing
            cmbTaminKonandeh.Items.Clear()
            cmbTaminKonandeh.DataSource = dsForm.Tables("tbl_TaminKonandeh").DefaultView
            cmbTaminKonandeh.DisplayMember = "NameTaminKonandeh"
            cmbTaminKonandeh.ValueMember = "ccTaminKonandeh"
            '_________________ Load Tamin Konandeh __________________

            '_________________ Load Tamin Brand __________________
            strSQL = "Global.spBrand_LoadCombo "

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            daSQL = New SqlDataAdapter(cmSQL)
            daSQL.Fill(dsForm, "tbl_Brand")

            'dr = dsForm.Tables("tbl_Brand").NewRow
            'dr("NameBrand") = "همـه"
            'dr("ccBrand") = 0
            'dsForm.Tables("tbl_Brand").Rows.Add(dr)

            cmbBrand.DataSource = Nothing
            cmbBrand.Items.Clear()
            cmbBrand.DataSource = dsForm.Tables("tbl_Brand").DefaultView
            cmbBrand.DisplayMember = "NameBrand"
            cmbBrand.ValueMember = "ccBrand"
            '_________________ Load Tamin Brand __________________

            '_________________ Load Goroh Kala __________________
            '----------------- Goroh 1 -----------------
            strSQL = "Global.spGorohKala_1_LoadCombo "

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            daSQL = New SqlDataAdapter(cmSQL)
            daSQL.Fill(dsForm, "tbl_Goroh1")

            '----------------- Goroh 2 -----------------
            strSQL = "Global.spGorohKala_2_LoadCombo "

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            cmSQL.Parameters.AddWithValue("CodeLink", 0)

            daSQL = New SqlDataAdapter(cmSQL)
            daSQL.Fill(dsForm, "tbl_Goroh2")

            '----------------- Goroh 3 -----------------
            strSQL = "Global.spGorohKala_3_LoadCombo "

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            cmSQL.Parameters.AddWithValue("CodeLink", 0)

            daSQL = New SqlDataAdapter(cmSQL)
            daSQL.Fill(dsForm, "tbl_Goroh3")

            '----------------- Goroh 4 -----------------
            strSQL = "Global.spGorohKala_4_LoadCombo "

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            cmSQL.Parameters.AddWithValue("CodeLink", 0)

            daSQL = New SqlDataAdapter(cmSQL)
            daSQL.Fill(dsForm, "tbl_Goroh4")

            '----------------- Goroh 5 -----------------
            strSQL = "Global.spGorohKala_5_LoadCombo "

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            cmSQL.Parameters.AddWithValue("CodeLink", 0)

            daSQL = New SqlDataAdapter(cmSQL)
            daSQL.Fill(dsForm, "tbl_Goroh5")

            '----------------- ******* -----------------

            cmbGoroh1.DataSource = Nothing
            cmbGoroh1.Items.Clear()
            cmbGoroh1.DataSource = dsForm.Tables("tbl_Goroh1").DefaultView
            cmbGoroh1.DisplayMember = "Sharh"
            cmbGoroh1.ValueMember = "Code"
            cmbGoroh1.SelectedIndex = -1
            cmbGoroh1.SelectedIndex = -1
            '_________________ Load Goroh Kala __________________

            cmSQL = Nothing : daSQL = Nothing
            cnSQL.Close()

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "Error IN DataBase ----> LoadCombo")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, "Error IN ----> LoadCombo")
        End Try
    End Sub
    Private Sub txtCodeKala_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtCodeKala.KeyPress
        Try
            If (Asc(e.KeyChar()) < 48 Or Asc(e.KeyChar()) > 57) And (Asc(e.KeyChar()) <> 8) Then
                e.Handled = True
            End If

            If e.KeyChar = Chr(Keys.Space) Then
                Dim objKala As New Forms_dll.frmAN_KalaSearch
                Dim StrSql As String

                StrSql = "SELECT  CodeKala,NameKala,ccKala,txtsVahedeShomaresh,sVahedeShomaresh,NameBrand,RadifBrand "
                StrSql &= " FROM qryAN_Kala WHERE Faal = 1 "
                StrSql &= " ORDER BY RadifBrand,txtsG1,CodeKala"

                If txtCodeKala.Text.Length <> 0 Then
                    objKala.tcodeKala = sender.Text
                End If

                MultiSelection = False
                SearchItem = "CodeKala"
                objKala.SetForm(StrSql)
                objKala.ShowDialog()

                sender.Tag = objKala.tccKala
                sender.Text = objKala.tcodeKala
                lblNameKala.Text = objKala.tNameKala

                MultiSelection = False
            End If
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> txtCodeKala_KeyPress")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> txtCodeKala_KeyPress")
        End Try
    End Sub
    Private Sub txtCodeKala_TextChanged(sender As Object, e As EventArgs) Handles txtCodeKala.TextChanged
        Try
            If Trim(sender.Text) = "" Then

                Me.lblNameKala.Text = ""

                sender.Tag = 0
                Exit Sub
            End If

            Me.lblNameKala.Text = objTools.ConvertNulls(objTools.DLookup("NameKala", "tblAN_Kala", "CodeKala = " & sender.Text), "")

            sender.Tag = objTools.ConvertNulls(objTools.DLookup("ccKala", "tblAn_Kala", "CodeKala=" & sender.Text), 0)
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> txtCodeKala_TextChanged")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> txtCodeKala_TextChanged")
        End Try
    End Sub
    Private Sub cmbGoroh1_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles cmbGoroh1.SelectionChangeCommitted
        LoadComboGoroh2()
    End Sub
    Private Sub cmbGoroh2_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles cmbGoroh2.SelectionChangeCommitted
        LoadComboGoroh3()
    End Sub
    Private Sub LoadComboGoroh2()
        If Flg_Load = False Then Exit Sub
        If dsForm.Tables("tbl_Goroh2").Rows.Count = 0 Then Exit Sub
        Try
            Dim dvGoroh2 As New DataView(dsForm.Tables("tbl_Goroh2"), "CodeLink = " & IIf(cmbGoroh1.SelectedIndex = -1, -1, cmbGoroh1.SelectedValue), "", DataViewRowState.OriginalRows)
            With cmbGoroh3
                .DataSource = Nothing
                .Items.Clear()
            End With
            With cmbGoroh2
                .DataSource = Nothing
                .Items.Clear()
                .DataSource = dvGoroh2
                .DisplayMember = "Sharh"
                .ValueMember = "Code"
                .SelectedIndex = -1
            End With
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "Error IN DataBase ----> LoadComboGoroh2")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, "Error IN ----> LoadComboGoroh2")
        End Try

    End Sub
    Private Sub LoadComboGoroh3()
        If Flg_Load = False Then Exit Sub
        If dsForm.Tables("tbl_Goroh3").Rows.Count = 0 Then Exit Sub
        Try
            Dim dvGoroh3 As New DataView(dsForm.Tables("tbl_Goroh3"), "CodeLink = " & IIf(cmbGoroh2.SelectedIndex = -1, -1, cmbGoroh2.SelectedValue), "", DataViewRowState.OriginalRows)
            'With cmbGoroh4
            '    .DataSource = Nothing
            '    .Items.Clear()
            'End With
            With cmbGoroh3
                .DataSource = Nothing
                .Items.Clear()
                .DataSource = dvGoroh3
                .DisplayMember = "Sharh"
                .ValueMember = "Code"
                .SelectedIndex = -1
            End With
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "Error IN DataBase ----> LoadComboGoroh3")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, "Error IN ----> LoadComboGoroh3")
        End Try

    End Sub
    Private Sub SetForm()
        If rbKala.Checked Then
            txtCodeKala.Enabled = True
            cmbTaminKonandeh.Enabled = False
            cmbBrand.Enabled = False
            cmbGoroh1.Enabled = False
            cmbGoroh2.Enabled = False
            cmbGoroh3.Enabled = False

            txtCodeKala.Text = ""
            txtCodeKala.Tag = 0
            lblNameKala.Text = ""
            cmbTaminKonandeh.SelectedIndex = -1
            cmbBrand.SelectedIndex = -1
            cmbGoroh1.SelectedIndex = -1
        ElseIf rbTaminKonanadeh.Checked Then
            txtCodeKala.Enabled = False
            cmbTaminKonandeh.Enabled = True
            cmbBrand.Enabled = False
            cmbGoroh1.Enabled = False
            cmbGoroh2.Enabled = False
            cmbGoroh3.Enabled = False

            txtCodeKala.Text = ""
            txtCodeKala.Tag = 0
            lblNameKala.Text = ""
            cmbTaminKonandeh.SelectedIndex = 0
            cmbBrand.SelectedIndex = -1
            cmbGoroh1.SelectedIndex = -1
        ElseIf rbBrand.Checked Then
            txtCodeKala.Enabled = False
            cmbTaminKonandeh.Enabled = False
            cmbBrand.Enabled = True
            cmbGoroh1.Enabled = False
            cmbGoroh2.Enabled = False
            cmbGoroh3.Enabled = False

            txtCodeKala.Text = ""
            txtCodeKala.Tag = 0
            lblNameKala.Text = ""
            cmbTaminKonandeh.SelectedIndex = -1
            cmbBrand.SelectedIndex = 0
            cmbGoroh1.SelectedIndex = -1
        ElseIf rbGorohKala.Checked Then
            txtCodeKala.Enabled = False
            cmbTaminKonandeh.Enabled = False
            cmbBrand.Enabled = False
            cmbGoroh1.Enabled = True
            cmbGoroh2.Enabled = True
            cmbGoroh3.Enabled = True

            txtCodeKala.Text = ""
            txtCodeKala.Tag = 0
            lblNameKala.Text = ""
            cmbTaminKonandeh.SelectedIndex = -1
            cmbBrand.SelectedIndex = -1
            cmbGoroh1.SelectedIndex = 0
        End If
    End Sub
    Private Sub rbKala_CheckedChanged(sender As Object, e As EventArgs) Handles rbKala.CheckedChanged
        If rbKala.Checked Then
            SetForm()
        End If
    End Sub
    Private Sub rbTaminKonanadeh_CheckedChanged(sender As Object, e As EventArgs) Handles rbTaminKonanadeh.CheckedChanged
        If rbTaminKonanadeh.Checked Then
            SetForm()
        End If
    End Sub
    Private Sub rbBrand_CheckedChanged(sender As Object, e As EventArgs) Handles rbBrand.CheckedChanged
        If rbBrand.Checked Then
            SetForm()
        End If
    End Sub
    Private Sub rbGorohKala_CheckedChanged(sender As Object, e As EventArgs) Handles rbGorohKala.CheckedChanged
        If rbGorohKala.Checked Then
            SetForm()
        End If
    End Sub
    Private Sub CheckIsNumeric(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles _
     txtAzShomareh.KeyPress, txtTaShomareh.KeyPress
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
    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        If IsValid() = False Then
            Exit Sub
        End If

        delete()

    End Sub
    Private Function IsValid() As Boolean
        IsValid = False
        Try
            If Me.mskAzTarikh.Text = "" Then
                ErrPro.SetError(Me.mskAzTarikh, "از تاریخ را وارد نمایید !")
                MsgBox("از تاریخ را وارد نمایید !", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Critical, "خطا")
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
                MsgBox("تا تاریخ را وارد نمایید !", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Critical, "خطا")
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
                MsgBox(".تا تاریخ نباید پیش از شروع آن باشد", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Critical, "خطا")
                mskTaTarikh.Focus()
                Exit Function
            End If
            ErrPro.SetError(Me.mskAzTarikh, "")

            If rbKala.Checked Then
                If txtCodeKala.Text.Trim = "" Or txtCodeKala.Tag = 0 Then
                    ErrPro.SetError(Me.txtCodeKala, "ابتدا باید یک کالا انتخاب نمایید !")
                    MsgBox("ابتدا باید یک کالا انتخاب نمایید !", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Critical, "خطا")
                    txtCodeKala.Focus()
                End If
                ErrPro.SetError(Me.txtCodeKala, "")
            ElseIf rbTaminKonanadeh.Checked Then
                If cmbTaminKonandeh.SelectedIndex = -1 Or cmbTaminKonandeh.SelectedIndex = Nothing Then
                    ErrPro.SetError(Me.cmbTaminKonandeh, "ابتدا باید یک تامین کننده انتخاب نمایید !")
                    MsgBox("ابتدا باید یک تامین کننده انتخاب نمایید !", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Critical, "خطا")
                    cmbTaminKonandeh.Focus()
                End If
                ErrPro.SetError(Me.cmbTaminKonandeh, "")
            ElseIf rbBrand.Checked Then
                If cmbBrand.SelectedIndex = -1 Or cmbBrand.SelectedIndex = Nothing Then
                    ErrPro.SetError(Me.cmbBrand, "ابتدا باید یک برنــد انتخاب نمایید !")
                    MsgBox("ابتدا باید یک برنــد انتخاب نمایید !", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Critical, "خطا")
                    cmbBrand.Focus()
                End If
                ErrPro.SetError(Me.cmbTaminKonandeh, "")
            ElseIf rbGorohKala.Checked Then
                If cmbGoroh1.SelectedIndex = -1 Or cmbGoroh1.SelectedIndex = Nothing Then
                    ErrPro.SetError(Me.cmbGoroh1, "ابتدا باید یک گـروه کالا انتخاب نمایید !")
                    MsgBox("ابتدا باید یک گـروه کالا انتخاب نمایید !", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Critical, "خطا")
                    cmbGoroh1.Focus()
                End If
                ErrPro.SetError(Me.cmbTaminKonandeh, "")
            End If

            Return True
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> IsValid")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> IsValid")
        End Try
    End Function
    Private Sub delete()
        Dim cnSQL As New SqlConnection
        Dim cmSQL As New SqlCommand
        Dim strSQL As String = ""

        Try
            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()

            strSQL = "Global.spEditDataEnteghali "

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            cmSQL.Parameters.AddWithValue("NoeAmalyat", cmbNoeAmalyat.SelectedIndex)
            If rbDelete.Checked Then
                cmSQL.Parameters.AddWithValue("NoeDelete", 1)
            ElseIf rbNotDelete.Checked Then
                cmSQL.Parameters.AddWithValue("NoeDelete", 2)
            End If
            cmSQL.Parameters.AddWithValue("CodeMahal", cmbMarkazPakhsh.SelectedValue)
            cmSQL.Parameters.AddWithValue("CodeDoreh", cmbDoreh.SelectedValue)
            cmSQL.Parameters.AddWithValue("AzTarikh", mskAzTarikh.Text)
            cmSQL.Parameters.AddWithValue("TaTarikh", mskTaTarikh.Text)
            cmSQL.Parameters.AddWithValue("AzShomareh", IIf(txtAzShomareh.Text.Trim = "", 0, txtAzShomareh.Text.Trim))
            cmSQL.Parameters.AddWithValue("TaShomareh", IIf(txtTaShomareh.Text.Trim = "", 0, txtTaShomareh.Text.Trim))
            cmSQL.Parameters.AddWithValue("ccKala", IIf(rbKala.Checked, txtCodeKala.Tag, 0))
            cmSQL.Parameters.AddWithValue("ccTaminkonandeh", IIf(rbTaminKonanadeh.Checked, cmbTaminKonandeh.SelectedValue, 0))
            cmSQL.Parameters.AddWithValue("ccBrand", IIf(rbBrand.Checked, cmbBrand.SelectedValue, 0))
            cmSQL.Parameters.AddWithValue("sG1", IIf(rbGorohKala.Checked, cmbGoroh1.SelectedValue, 0))
            cmSQL.Parameters.AddWithValue("sG2", IIf(rbGorohKala.Checked, IIf(cmbGoroh2.SelectedIndex = -1 Or cmbGoroh2.SelectedIndex = Nothing, 0, cmbGoroh2.SelectedValue), 0))
            cmSQL.Parameters.AddWithValue("sG3", IIf(rbGorohKala.Checked, IIf(cmbGoroh3.SelectedIndex = -1 Or cmbGoroh3.SelectedIndex = Nothing, 0, cmbGoroh3.SelectedValue), 0))

            cmSQL.ExecuteNonQuery()

            cmSQL = Nothing
            cnSQL.Close()

            MsgBox("عملیات با موفقیت صورت پذیرفت !", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "")

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "Error IN DataBase ----> delete")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, "Error IN ----> delete")
        End Try
    End Sub
    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub
End Class
