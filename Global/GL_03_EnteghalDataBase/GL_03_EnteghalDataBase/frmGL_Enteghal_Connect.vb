Public Class frmGL_EntaghalDataBase
#Region "Variable AND Constant Declration"
    Const cntCodeSubSystem As Long = 1000144
    Dim FormMode As Integer = 0 '' 0 = Connection / 1 = Enteghal
    Dim dvForm As DataView
    Dim ErrPro As New ErrorProvider
    Dim dsForm As New DataSet
    Private SN As Integer
    Dim tPos As Integer
    Dim txtCaption As String
    Private WithEvents BS As New UD_Dll.PassString
    Dim ConnectionString_Server1 As String = "", ConnectionString_Server2 As String = ""
    Dim Flg_Load As Boolean = False
#End Region
    Private Sub frmGL_EntaghalDataBase_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetForm()

        mskAzTarikh_AN.Text = TarikhEmrooz
        mskTaTarikh_AN.Text = TarikhEmrooz
        mskAzTarikh_FO.Text = TarikhEmrooz
        mskTaTarikh_FO.Text = TarikhEmrooz
    End Sub
    Private Sub cmbGoroh1_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles cmbGoroh1.SelectionChangeCommitted
        LoadComboGoroh2()
    End Sub
    Private Sub cmbGoroh2_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles cmbGoroh2.SelectionChangeCommitted
        LoadComboGoroh3()
    End Sub
    Private Sub cmbGoroh3_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles cmbGoroh3.SelectionChangeCommitted
        LoadComboGoroh4()
    End Sub
    Private Sub cmbGoroh4_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles cmbGoroh4.SelectionChangeCommitted
        LoadComboGoroh5()
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
    Private Sub txtCodeMoshtary_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCodeMoshtary.KeyPress
        Try
            If (Asc(e.KeyChar()) < 48 Or Asc(e.KeyChar()) > 57) And (Asc(e.KeyChar()) <> 8) Then
                e.Handled = True
            End If
            If e.KeyChar = Chr(Keys.Space) Then
                Dim objMoshtary As New Forms_dll.frmFO_MoshtarySearch
                Dim StrSql As String = ""

                StrSql = "SELECT * FROM qryFO_Moshtary WHERE (CodeMahal = " & IIf(cmbMarkazPakhsh_FO.SelectedIndex = -1, 0, cmbMarkazPakhsh_FO.SelectedValue) & " OR " & IIf(cmbMarkazPakhsh_FO.SelectedIndex = -1, 0, cmbMarkazPakhsh_FO.SelectedValue) & " = 0) AND sVazeiat = " & UD_Dll.Enums.FO_VaziatMoshtary.Faal
                StrSql &= " ORDER BY sMantagheh,sMahaleh,NameMoshtary"

                tCodeMoshtary = ""
                tNameMoshtary = ""
                tccMoshtary = ""

                If txtCodeMoshtary.Text.Length <> 0 Then
                    tCodeMoshtary = txtCodeMoshtary.Text
                End If

                MultiSelection = False
                SearchItem = "CodeMoshtary"
                objMoshtary.SetForm(StrSql)
                objMoshtary.ShowDialog()
                txtCodeMoshtary.Tag = IIf(IsNothing(objMoshtary.tccMoshtary), 0, objMoshtary.tccMoshtary)
                txtCodeMoshtary.Text = IIf(IsNothing(objMoshtary.tCodeMoshtary), "", objMoshtary.tCodeMoshtary)
                lblNameMoshtary.Text = IIf(IsNothing(objMoshtary.tNameMoshtary), "", objMoshtary.tNameMoshtary)

                MultiSelection = False

            End If
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->txtCodeMoshtary_KeyPress")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->txtCodeMoshtary_KeyPress")
        End Try
    End Sub
    Private Sub txtCodeMoshtary_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCodeMoshtary.TextChanged
        Me.lblNameMoshtary.Text = objTools.ConvertNulls(objTools.DLookup("NameMoshtary", "tblFO_Moshtary", "sVazeiat = 3980 AND (CodeMahal = " & IIf(cmbMarkazPakhsh_FO.SelectedIndex = -1, 0, cmbMarkazPakhsh_FO.SelectedValue) & " OR " & IIf(cmbMarkazPakhsh_FO.SelectedIndex = -1, 0, cmbMarkazPakhsh_FO.SelectedValue) & " = 0) AND CodeMoshtary = " & IIf(txtCodeMoshtary.Text.Trim = "", 0, Val(txtCodeMoshtary.Text.Trim))), "")
        Me.txtCodeMoshtary.Tag = objTools.ConvertNulls(objTools.DLookup("ccMoshtary", "tblFO_Moshtary", "sVazeiat = 3980 AND (CodeMahal = " & IIf(cmbMarkazPakhsh_FO.SelectedIndex = -1, 0, cmbMarkazPakhsh_FO.SelectedValue) & " OR " & IIf(cmbMarkazPakhsh_FO.SelectedIndex = -1, 0, cmbMarkazPakhsh_FO.SelectedValue) & " = 0) AND CodeMoshtary = " & IIf(txtCodeMoshtary.Text.Trim = "", 0, Val(txtCodeMoshtary.Text.Trim))), 0)
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
            cmbNoeAmalyat.Items.Add("کسـر و اضافـه انبــار ")
            cmbNoeAmalyat.Items.Add("انتقال نبـار به انبـار")
            '_________________ Load Amalyat __________________

            cnSQL = New SqlConnection(ConnectionString_Server1)
            cnSQL.Open()

            '_________________ Load Markaz Pakhsh __________________
            strSQL = "Global.spMarkazPakhsh_LoadCombo "

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            cmSQL.Parameters.AddWithValue("CodeMahal", 0)

            daSQL = New SqlDataAdapter(cmSQL)
            daSQL.Fill(dsForm, "tbl_MarkazPakhsh")

            dr = dsForm.Tables("tbl_MarkazPakhsh").NewRow
            dr("NameMahal") = "همـه"
            dr("CodeMahal") = 0
            dsForm.Tables("tbl_MarkazPakhsh").Rows.Add(dr)

            cmbMarkazPakhsh_AN.DataSource = Nothing
            cmbMarkazPakhsh_AN.Items.Clear()
            cmbMarkazPakhsh_AN.DataSource = dsForm.Tables("tbl_MarkazPakhsh").DefaultView
            cmbMarkazPakhsh_AN.DisplayMember = "NameMahal"
            cmbMarkazPakhsh_AN.ValueMember = "CodeMahal"

            cmbMarkazPakhsh_FO.DataSource = Nothing
            cmbMarkazPakhsh_FO.Items.Clear()
            cmbMarkazPakhsh_FO.DataSource = dsForm.Tables("tbl_MarkazPakhsh").DefaultView
            cmbMarkazPakhsh_FO.DisplayMember = "NameMahal"
            cmbMarkazPakhsh_FO.ValueMember = "CodeMahal"
            '_________________ Load Markaz Pakhsh __________________

            '_________________ Load Doreh __________________
            strSQL = "Global.spCodeDoreh_LoadCombo "

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            daSQL = New SqlDataAdapter(cmSQL)
            daSQL.Fill(dsForm, "tbl_Doreh")

            dr = dsForm.Tables("tbl_Doreh").NewRow
            dr("txtDoreh") = "همـه"
            dr("CodeDoreh") = 0
            dsForm.Tables("tbl_Doreh").Rows.Add(dr)

            cmbDoreh_AN.DataSource = Nothing
            cmbDoreh_AN.Items.Clear()
            cmbDoreh_AN.DataSource = dsForm.Tables("tbl_Doreh").DefaultView
            cmbDoreh_AN.DisplayMember = "txtDoreh"
            cmbDoreh_AN.ValueMember = "CodeDoreh"

            cmbDoreh_FO.DataSource = Nothing
            cmbDoreh_FO.Items.Clear()
            cmbDoreh_FO.DataSource = dsForm.Tables("tbl_Doreh").DefaultView
            cmbDoreh_FO.DisplayMember = "txtDoreh"
            cmbDoreh_FO.ValueMember = "CodeDoreh"
            '_________________ Load Doreh __________________

            '_________________ Load Noe Moshtary __________________
            strSQL = "Global.spNoeMoshtary_LoadCombo "

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            daSQL = New SqlDataAdapter(cmSQL)
            daSQL.Fill(dsForm, "tbl_NoeMoshtary")

            dr = dsForm.Tables("tbl_NoeMoshtary").NewRow
            dr("Sharh") = "همـه"
            dr("Code") = 0
            dsForm.Tables("tbl_NoeMoshtary").Rows.Add(dr)

            cmbNoeMoshtary.DataSource = Nothing
            cmbNoeMoshtary.Items.Clear()
            cmbNoeMoshtary.DataSource = dsForm.Tables("tbl_NoeMoshtary").DefaultView
            cmbNoeMoshtary.DisplayMember = "Sharh"
            cmbNoeMoshtary.ValueMember = "Code"
            '_________________ Load Noe Moshtary __________________

            '_________________ Load Noe Senf __________________
            strSQL = "Global.spNoeSenf_LoadCombo "

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            daSQL = New SqlDataAdapter(cmSQL)
            daSQL.Fill(dsForm, "tbl_NoeSenf")

            dr = dsForm.Tables("tbl_NoeSenf").NewRow
            dr("Sharh") = "همـه"
            dr("Code") = 0
            dsForm.Tables("tbl_NoeSenf").Rows.Add(dr)

            cmbNoeSenf.DataSource = Nothing
            cmbNoeSenf.Items.Clear()
            cmbNoeSenf.DataSource = dsForm.Tables("tbl_NoeSenf").DefaultView
            cmbNoeSenf.DisplayMember = "Sharh"
            cmbNoeSenf.ValueMember = "Code"
            '_________________ Load Noe Senf __________________
            '_________________ Load Tamin Konandeh __________________
            strSQL = "Global.spTaminKonandeh_LoadCombo "

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            daSQL = New SqlDataAdapter(cmSQL)
            daSQL.Fill(dsForm, "tbl_TaminKonandeh")

            dr = dsForm.Tables("tbl_TaminKonandeh").NewRow
            dr("NameTaminKonandeh") = "همـه"
            dr("ccTaminKonandeh") = 0
            dsForm.Tables("tbl_TaminKonandeh").Rows.Add(dr)

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

            dr = dsForm.Tables("tbl_Brand").NewRow
            dr("NameBrand") = "همـه"
            dr("ccBrand") = 0
            dsForm.Tables("tbl_Brand").Rows.Add(dr)

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
    Private Sub LoadAnbar()
        If Flg_Load = False Then Exit Sub

        Dim cnSQL As New SqlConnection
        Dim cmSQL As New SqlCommand
        Dim daSQL As New SqlDataAdapter
        Dim dr As DataRow
        Dim strSQL As String = ""

        If dsForm.Tables.Contains("tbl_Anbar") Then
            dsForm.Tables.Remove("tbl_Anbar")
        End If

        Try
            cnSQL = New SqlConnection(ConnectionString_Server1)
            cnSQL.Open()

            '_________________ Load Anbar __________________
            strSQL = "Global.spAnbar_LoadCombo "

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            cmSQL.Parameters.AddWithValue("CodeMahal", cmbMarkazPakhsh_AN.SelectedValue)

            daSQL = New SqlDataAdapter(cmSQL)
            daSQL.Fill(dsForm, "tbl_Anbar")

            dr = dsForm.Tables("tbl_Anbar").NewRow
            dr("NameAnbar") = "همـه"
            dr("CodeAnbar") = 0
            dsForm.Tables("tbl_Anbar").Rows.Add(dr)

            cmbAnbar.DataSource = Nothing
            cmbAnbar.Items.Clear()
            cmbAnbar.DataSource = dsForm.Tables("tbl_Anbar").DefaultView
            cmbAnbar.DisplayMember = "NameAnbar"
            cmbAnbar.ValueMember = "CodeAnbar"
            '_________________ Load Anbar __________________
            cmSQL = Nothing : daSQL = Nothing
            cnSQL.Close()

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "Error IN DataBase ----> LoadAnbar")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, "Error IN ----> LoadAnbar")
        End Try
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
            With cmbGoroh4
                .DataSource = Nothing
                .Items.Clear()
            End With
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
    Private Sub LoadComboGoroh4()
        If Flg_Load = False Then Exit Sub
        If dsForm.Tables("tbl_Goroh4").Rows.Count = 0 Then Exit Sub
        Try
            Dim dvGoroh4 As New DataView(dsForm.Tables("tbl_Goroh4"), "CodeLink = " & IIf(cmbGoroh3.SelectedIndex = -1, -1, cmbGoroh3.SelectedValue), "", DataViewRowState.OriginalRows)
            With cmbGoroh5
                .DataSource = Nothing
                .Items.Clear()
            End With
            With cmbGoroh4
                .DataSource = Nothing
                .Items.Clear()
                .DataSource = dvGoroh4
                .DisplayMember = "Sharh"
                .ValueMember = "Code"
                .SelectedIndex = -1
            End With
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "Error IN DataBase ----> LoadComboGoroh4")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, "Error IN ----> LoadComboGoroh4")
        End Try

    End Sub
    Private Sub LoadComboGoroh5()
        If Flg_Load = False Then Exit Sub
        If dsForm.Tables("tbl_Goroh5").Rows.Count = 0 Then Exit Sub
        Try
            Dim dvGoroh5 As New DataView(dsForm.Tables("tbl_Goroh5"), "CodeLink = " & IIf(cmbGoroh4.SelectedIndex = -1, -1, cmbGoroh4.SelectedValue), "", DataViewRowState.OriginalRows)
            With cmbGoroh5
                .DataSource = Nothing
                .Items.Clear()
                .DataSource = dvGoroh5
                .DisplayMember = "Sharh"
                .ValueMember = "Code"
                .SelectedIndex = -1
            End With
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "Error IN DataBase ----> LoadComboGoroh5")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, "Error IN ----> LoadComboGoroh5")
        End Try
    End Sub
    Private Sub SetForm()
        If FormMode = 0 Then
            lblConnection.Font = New Font(lblConnection.Font, FontStyle.Bold)
            lblEnteghal.Font = New Font(lblEnteghal.Font, FontStyle.Regular)
            grbConnection.Visible = True
            grbEnteghal.Visible = False
            btnPrv.Enabled = False
            btnNext.Enabled = True
            cmbNoeAmalyat.Items.Clear()
            btnChangeUserPass.Visible = True
        ElseIf FormMode = 1 Then
            lblConnection.Font = New Font(lblConnection.Font, FontStyle.Regular)
            lblEnteghal.Font = New Font(lblEnteghal.Font, FontStyle.Bold)
            grbConnection.Visible = False
            grbEnteghal.Visible = True
            btnPrv.Enabled = True
            btnNext.Enabled = False
            btnChangeUserPass.Visible = False
        End If
    End Sub
    Private Sub btnPrv_Click(sender As Object, e As EventArgs) Handles btnPrv.Click
        FormMode -= 1
        SetForm()
        Flg_Load = False
        dsForm.Clear()
    End Sub
    Private Sub btnNext_Click(sender As Object, e As EventArgs) Handles btnNext.Click
        If cmbdataBaseServer1.SelectedIndex = -1 Then
            MsgBox("برای سرور اول باید یک پایگاه داده مشخص نمایید !", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, "خطا")
            ErrPro.SetError(cmbdataBaseServer1, "برای سرور اول باید یک پایگاه داده مشخص نمایید !")
            cmbdataBaseServer1.Focus()
            Exit Sub
        End If
        ErrPro.SetError(cmbdataBaseServer1, "")

        If cmbdataBaseServer2.SelectedIndex = -1 Then
            MsgBox("برای سرور دوم باید یک پایگاه داده مشخص نمایید !", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, "خطا")
            ErrPro.SetError(cmbdataBaseServer2, "برای سرور دوم باید یک پایگاه داده مشخص نمایید !")
            cmbdataBaseServer2.Focus()
            Exit Sub
        End If
        ErrPro.SetError(cmbdataBaseServer2, "")

        If ValidTableIndataBase(ConnectionString_Server1, cmbdataBaseServer1.Text, "tblFO_Faktor") = False Then
            MsgBox("جداول مربوطه در پایگاه داده انتخاب شده وجود ندارد !", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, "خطا")
            ErrPro.SetError(cmbdataBaseServer1, "جداول مربوطه در پایگاه داده انتخاب شده وجود ندارد !")
            cmbdataBaseServer1.Focus()
            Exit Sub
        End If
        ErrPro.SetError(cmbdataBaseServer1, "")

        If ValidTableIndataBase(ConnectionString_Server2, cmbdataBaseServer2.Text, "tblFO_Faktor") = False Then
            MsgBox("جداول مربوطه در پایگاه داده انتخاب شده وجود ندارد !", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, "خطا")
            ErrPro.SetError(cmbdataBaseServer2, "جداول مربوطه در پایگاه داده انتخاب شده وجود ندارد !")
            cmbdataBaseServer2.Focus()
            Exit Sub
        End If
        ErrPro.SetError(cmbdataBaseServer2, "")

        If (txtServerIP1.Text.Trim = txtServerIP2.Text.Trim) AndAlso (cmbdataBaseServer1.Text = cmbdataBaseServer2.Text) Then
            MsgBox("پایگاه داده مبدا و مقصد نمی تواند یکسان باشد !", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, "خطا")
            ErrPro.SetError(cmbdataBaseServer2, "پایگاه داده مبدا و مقصد نمی تواند یکسان باشد !")
            cmbdataBaseServer2.Focus()
            Exit Sub
        End If
        ErrPro.SetError(cmbdataBaseServer2, "")

        ConnectionString_Server1 = "Server=" & txtServerIP1.Text.Trim & ";Database=" & cmbdataBaseServer1.Text & ";User Id=" & My.Settings.UserSQL1 & ";Password=" & My.Settings.PassSQL1
        ConnectionString_Server2 = "Server=" & txtServerIP2.Text.Trim & ";Database=" & cmbdataBaseServer2.Text & ";User Id=" & My.Settings.UserSQL2 & ";Password=" & My.Settings.PassSQL2
        ConnectionString = ConnectionString_Server1

        FormMode += 1
        SetForm()

        LoadCombo()

        Flg_Load = True
    End Sub
    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub
    Private Sub lblShowDataBaseServer1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lblShowDataBaseServer1.LinkClicked
        'ConnectionString_Server1 = "Server=" & txtServerIP1.Text.Trim & ";Database=Master;User Id=sa;Password=aKstt"
        ConnectionString_Server1 = "Server=" & txtServerIP1.Text.Trim & ";Database=DB_Pakhsh;User Id=" & My.Settings.UserSQL1 & ";Password=" & My.Settings.PassSQL1

        If CheckConnection(ConnectionString_Server1) Then
            LoadComboDataBaseServer(ConnectionString_Server1, 1)
        Else
            MsgBox("ارتباط برقرار نشد !", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, "خطا")
            cmbdataBaseServer1.DataSource = Nothing
        End If
    End Sub
    Private Sub lblShowDataBaseServer2_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lblShowDataBaseServer2.LinkClicked
        'ConnectionString_Server2 = "Server=" & txtServerIP2.Text.Trim & ";Database=Master;User Id=sa;Password=DrwebDDc1"
        ConnectionString_Server2 = "Server=" & txtServerIP2.Text.Trim & ";Database=DB_Pakhsh;User Id=" & My.Settings.UserSQL2 & ";Password=" & My.Settings.PassSQL2

        If CheckConnection(ConnectionString_Server2) Then
            LoadComboDataBaseServer(ConnectionString_Server2, 2)
        Else
            MsgBox("ارتباط برقرار نشد !", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, "خطا")
            cmbdataBaseServer2.DataSource = Nothing
        End If
    End Sub
    Private Function CheckConnection(ByVal strCn As String) As Boolean
        Dim cn As New Data.SqlClient.SqlConnection(strCn)
        Try
            cn.Open()
            Return True
        Catch ex As Exception
            Return False
        Finally
            If cn.State = ConnectionState.Open Then
                cn.Close()
            End If
        End Try
    End Function
    Private Sub LoadComboDataBaseServer(ByVal strConnection As String, ByVal Type As Integer)
        '' Type --> 1 = DataBase1 / 2 = DataBase2
        Dim cnSQL As New SqlConnection
        Dim daSQL As New SqlDataAdapter
        Dim strSQL As String = ""
        Dim tblName As String = ""

        If Type = 1 Then
            tblName = "tbl_Database1"
        ElseIf Type = 2 Then
            tblName = "tbl_Database2"
        End If

        If dsForm.Tables.Contains(tblName) Then
            dsForm.Tables.Remove(tblName)
        End If

        cnSQL = New SqlConnection(strConnection)
        cnSQL.Open()

        strSQL = "SELECT Name FROM sys.sysdatabases WHERE Name LIKE N'%DB_%'"

        daSQL = New SqlDataAdapter(strSQL, cnSQL)
        daSQL.Fill(dsForm, tblName)

        daSQL = Nothing
        cnSQL.Close()

        If Type = 1 Then
            cmbdataBaseServer1.DataSource = Nothing
            cmbdataBaseServer1.Items.Clear()
            cmbdataBaseServer1.DataSource = dsForm.Tables(tblName).DefaultView
            cmbdataBaseServer1.DisplayMember = "Name"
            cmbdataBaseServer1.ValueMember = "Name"
        ElseIf Type = 2 Then
            cmbdataBaseServer2.DataSource = Nothing
            cmbdataBaseServer2.Items.Clear()
            cmbdataBaseServer2.DataSource = dsForm.Tables(tblName).DefaultView
            cmbdataBaseServer2.DisplayMember = "Name"
            cmbdataBaseServer2.ValueMember = "Name"
        End If
    End Sub
    Private Function ValidTableIndataBase(ByVal strConnection As String, ByVal DataBase As String, ByVal TableName As String) As Boolean
        ValidTableIndataBase = False
        '' Type --> 1 = DataBase1 / 2 = DataBase2
        Dim cnSQL As New SqlConnection
        Dim cmSQL As New SqlCommand
        Dim strSQL As String = ""
        Dim CountRec As Integer = 0

        cnSQL = New SqlConnection(strConnection)
        cnSQL.Open()

        strSQL = "SELECT @CountRec = COUNT(*) FROM " & DataBase & ".sys.tables WHERE Name = @TableName "

        cmSQL = New SqlCommand(strSQL, cnSQL)
        cmSQL.Parameters.AddWithValue("CountRec", CountRec)
        cmSQL.Parameters.AddWithValue("TableName", TableName)
        cmSQL.Parameters("CountRec").Direction = ParameterDirection.Output

        cmSQL.ExecuteNonQuery()

        CountRec = cmSQL.Parameters("CountRec").Value

        cmSQL = Nothing
        cnSQL.Close()

        If CountRec <> 0 Then
            Return True
        End If

    End Function
    Private Sub cmbMarkazPakhsh_AN_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbMarkazPakhsh_AN.SelectedIndexChanged
        LoadAnbar()
    End Sub

    Private Sub btnEnteghal_FO_Click(sender As Object, e As EventArgs) Handles btnEnteghal_FO.Click
        If IsValid() = False Then
            Exit Sub
        End If

        If Enteghal() Then
            MsgBox("انتقال اطلاعات فـــروش با موفقیت صورت پذیرفت !", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "انتقال")
        End If
    End Sub
    Private Sub btnEnteghal_AN_Click(sender As Object, e As EventArgs) Handles btnEnteghal_AN.Click
        If IsValid() = False Then
            Exit Sub
        End If

        If Enteghal() Then
            MsgBox("انتقال اطلاعات انبــار با موفقیت صورت پذیرفت !", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "انتقال")
        End If
    End Sub
    Private Function IsValid() As Boolean
        IsValid = False
        Try
            If tcEnteghal.SelectedIndex = 0 Then
                If Me.mskAzTarikh_AN.Text = "" Then
                    ErrPro.SetError(Me.mskAzTarikh_AN, "از تاریخ را وارد نمایید !")
                    MsgBox("از تاریخ را وارد نمایید !", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
                    mskAzTarikh_AN.Focus()
                    Exit Function
                Else
                    If Not objTarikh.IsShDate(mskAzTarikh_AN.Text.ToString) Then
                        mskAzTarikh_AN.Focus()
                        Exit Function
                    End If
                End If
                ErrPro.SetError(Me.mskAzTarikh_AN, "")


                If Me.mskTaTarikh_AN.Text = "" Then
                    ErrPro.SetError(Me.mskTaTarikh_AN, "تا تاریخ را وارد نمایید !")
                    MsgBox("تا تاریخ را وارد نمایید !", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
                    mskTaTarikh_AN.Focus()
                    Exit Function
                Else
                    If Not objTarikh.IsShDate(mskTaTarikh_AN.Text.ToString) Then
                        mskTaTarikh_AN.Focus()
                        Exit Function
                    End If
                End If
                ErrPro.SetError(Me.mskTaTarikh_AN, "")


                If mskAzTarikh_AN.Text > mskTaTarikh_AN.Text Then
                    ErrPro.SetError(Me.mskTaTarikh_AN, ".تا تاریخ نباید پیش از شروع آن باشد")
                    mskTaTarikh_AN.Focus()
                    Exit Function
                End If
                ErrPro.SetError(Me.mskAzTarikh_AN, "")

                If txtAzShomareh_AN.Text.Trim <> "" And txtTaShomareh_AN.Text.Trim <> "" Then
                    If Val(txtAzShomareh_AN.Text.Trim) > Val(txtTaShomareh_AN.Text.Trim) Then
                        ErrPro.SetError(Me.txtAzShomareh_AN, "از شماره نمی تواند بزرگتر از تا شماره باشد !")
                        txtAzShomareh_AN.Focus()
                        Exit Function
                    End If
                End If
                ErrPro.SetError(Me.mskTaTarikh_FO, "")
            ElseIf tcEnteghal.SelectedIndex = 1 Then
                If Me.mskAzTarikh_FO.Text = "" Then
                    ErrPro.SetError(Me.mskAzTarikh_FO, "از تاریخ را وارد نمایید !")
                    MsgBox("از تاریخ را وارد نمایید !", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
                    mskAzTarikh_FO.Focus()
                    Exit Function
                Else
                    If Not objTarikh.IsShDate(mskAzTarikh_FO.Text.ToString) Then
                        mskAzTarikh_FO.Focus()
                        Exit Function
                    End If
                End If
                ErrPro.SetError(Me.mskAzTarikh_FO, "")


                If Me.mskTaTarikh_FO.Text = "" Then
                    ErrPro.SetError(Me.mskTaTarikh_FO, "تا تاریخ را وارد نمایید !")
                    MsgBox("تا تاریخ را وارد نمایید !", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
                    mskTaTarikh_FO.Focus()
                    Exit Function
                Else
                    If Not objTarikh.IsShDate(mskTaTarikh_FO.Text.ToString) Then
                        mskTaTarikh_FO.Focus()
                        Exit Function
                    End If
                End If
                ErrPro.SetError(Me.mskTaTarikh_FO, "")


                If mskAzTarikh_FO.Text > mskTaTarikh_FO.Text Then
                    ErrPro.SetError(Me.mskTaTarikh_FO, ".تا تاریخ نباید پیش از شروع آن باشد")
                    mskTaTarikh_FO.Focus()
                    Exit Function
                End If
                ErrPro.SetError(Me.mskTaTarikh_FO, "")
            End If

            If txtAzShomareh_FO.Text.Trim <> "" And txtTaShomareh_FO.Text.Trim <> "" Then
                If Val(txtAzShomareh_FO.Text.Trim) > Val(txtTaShomareh_FO.Text.Trim) Then
                    ErrPro.SetError(Me.txtAzShomareh_FO, "از شماره نمی تواند بزرگتر از تا شماره باشد !")
                    txtAzShomareh_FO.Focus()
                    Exit Function
                End If
                ErrPro.SetError(Me.txtAzShomareh_FO, "")
            End If

            Return True
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> IsValid")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> IsValid")
        End Try
    End Function
    Private Function Enteghal() As Boolean
        Enteghal = False

        Dim cnSQL As New SqlConnection
        Dim cmSQL As New SqlCommand
        Dim strSQL As String = ""

        Try
            cnSQL = New SqlConnection(ConnectionString_Server1)
            cnSQL.Open()

            If txtServerIP1.Text.Trim = txtServerIP2.Text.Trim Then
                strSQL = "Global.spEnteghalDataForServer2 "
            Else
                strSQL = "Global.spEnteghalDataForServer2_WithDifferentIP "
            End If


            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            If txtServerIP1.Text.Trim <> txtServerIP2.Text.Trim Then
                cmSQL.Parameters.AddWithValue("IpServer1", txtServerIP1.Text.Trim)
                cmSQL.Parameters.AddWithValue("DbServer1", cmbdataBaseServer1.Text)
            End If
            cmSQL.Parameters.AddWithValue("IpServer2", txtServerIP2.Text.Trim)
            cmSQL.Parameters.AddWithValue("DbServer2", cmbdataBaseServer2.Text)
            If tcEnteghal.SelectedIndex = 0 Then
                cmSQL.Parameters.AddWithValue("CodeMahal", cmbMarkazPakhsh_AN.SelectedValue)
                cmSQL.Parameters.AddWithValue("CodeDoreh", cmbDoreh_AN.SelectedValue)
                cmSQL.Parameters.AddWithValue("AzTarikh", mskAzTarikh_AN.Text)
                cmSQL.Parameters.AddWithValue("TaTarikh", mskTaTarikh_AN.Text)
                cmSQL.Parameters.AddWithValue("AzShomareh", IIf(txtAzShomareh_AN.Text.Trim = "", 0, txtAzShomareh_AN.Text.Trim))
                cmSQL.Parameters.AddWithValue("TaShomareh", IIf(txtTaShomareh_AN.Text.Trim = "", 0, txtTaShomareh_AN.Text.Trim))
            ElseIf tcEnteghal.SelectedIndex = 1 Then
                cmSQL.Parameters.AddWithValue("CodeMahal", cmbMarkazPakhsh_FO.SelectedValue)
                cmSQL.Parameters.AddWithValue("CodeDoreh", cmbDoreh_FO.SelectedValue)
                cmSQL.Parameters.AddWithValue("AzTarikh", mskAzTarikh_FO.Text)
                cmSQL.Parameters.AddWithValue("TaTarikh", mskTaTarikh_FO.Text)
                cmSQL.Parameters.AddWithValue("AzShomareh", IIf(txtAzShomareh_FO.Text.Trim = "", 0, txtAzShomareh_FO.Text.Trim))
                cmSQL.Parameters.AddWithValue("TaShomareh", IIf(txtTaShomareh_FO.Text.Trim = "", 0, txtTaShomareh_FO.Text.Trim))
            End If
            cmSQL.Parameters.AddWithValue("ccAnbar", cmbAnbar.SelectedValue)
            cmSQL.Parameters.AddWithValue("ccTaminkonandeh", cmbTaminKonandeh.SelectedValue)
            If tcEnteghal.SelectedIndex = 0 Then
                cmSQL.Parameters.AddWithValue("ccMoshtary", 0)
                cmSQL.Parameters.AddWithValue("sNoeMoshtary", 0)
                cmSQL.Parameters.AddWithValue("sNoeSenf", 0)
            ElseIf tcEnteghal.SelectedIndex = 1 Then
                cmSQL.Parameters.AddWithValue("ccMoshtary", IIf(txtCodeMoshtary.Text.Trim = "", 0, txtCodeMoshtary.Tag))
                cmSQL.Parameters.AddWithValue("sNoeMoshtary", cmbNoeMoshtary.SelectedValue)
                cmSQL.Parameters.AddWithValue("sNoeSenf", cmbNoeSenf.SelectedValue)
            End If
            cmSQL.Parameters.AddWithValue("ccBrand", cmbBrand.SelectedValue)
            cmSQL.Parameters.AddWithValue("ccKala", IIf(txtCodeKala.Text.Trim = "", 0, txtCodeKala.Tag))
            cmSQL.Parameters.AddWithValue("sG1", IIf(cmbGoroh1.SelectedValue Is Nothing Or cmbGoroh1.SelectedIndex = -1, 0, cmbGoroh1.SelectedValue))
            cmSQL.Parameters.AddWithValue("sG2", IIf(cmbGoroh2.SelectedValue Is Nothing Or cmbGoroh2.SelectedIndex = -1, 0, cmbGoroh2.SelectedValue))
            cmSQL.Parameters.AddWithValue("sG3", IIf(cmbGoroh3.SelectedValue Is Nothing Or cmbGoroh3.SelectedIndex = -1, 0, cmbGoroh3.SelectedValue))
            cmSQL.Parameters.AddWithValue("sG4", IIf(cmbGoroh4.SelectedValue Is Nothing Or cmbGoroh4.SelectedIndex = -1, 0, cmbGoroh4.SelectedValue))
            cmSQL.Parameters.AddWithValue("sG5", IIf(cmbGoroh5.SelectedValue Is Nothing Or cmbGoroh5.SelectedIndex = -1, 0, cmbGoroh5.SelectedValue))
            cmSQL.Parameters.AddWithValue("NoeEnteghal", tcEnteghal.SelectedIndex)
            If tcEnteghal.SelectedIndex = 0 Then
                cmSQL.Parameters.AddWithValue("NoeAmalyat", cmbNoeAmalyat.SelectedIndex)
                cmSQL.Parameters.AddWithValue("InsertAmalyatFaktor", 0)
            ElseIf tcEnteghal.SelectedIndex = 1 Then
                cmSQL.Parameters.AddWithValue("NoeAmalyat", 11)
                cmSQL.Parameters.AddWithValue("InsertAmalyatFaktor", IIf(chkEnteghalDP.Checked = True, True, False))
            End If

            cmSQL.ExecuteNonQuery()

            cmSQL = Nothing
            cnSQL.Close()

            Return True
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> Enteghal")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> Enteghal")
        End Try
    End Function

    Private Sub btnChangeUserPass_Click(sender As Object, e As EventArgs) Handles btnChangeUserPass.Click
        Dim frm_UP As New frmGL_ChangeUserPass

        Me.Hide()
        frm_UP.ShowDialog()
        Me.Show()

    End Sub
    Private Sub CheckIsNumeric(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles _
     txtAzShomareh_AN.KeyPress, txtTaShomareh_AN.KeyPress, txtAzShomareh_FO.KeyPress, txtTaShomareh_FO.KeyPress
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

    Private Sub cmbNoeAmalyat_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbNoeAmalyat.SelectedIndexChanged
        If Flg_Load = False Then Exit Sub

        If cmbNoeAmalyat.SelectedIndex = 0 Then
            lblAzShomareh_AN.Visible = False
            txtAzShomareh_AN.Visible = False
            txtAzShomareh_AN.Text = ""
            lblTaShomareh_AN.Visible = False
            txtTaShomareh_AN.Visible = False
            txtTaShomareh_AN.Text = ""
        Else
            lblAzShomareh_AN.Visible = True
            txtAzShomareh_AN.Visible = True
            txtAzShomareh_AN.Text = ""
            lblTaShomareh_AN.Visible = True
            txtTaShomareh_AN.Visible = True
            txtTaShomareh_AN.Text = ""
        End If
    End Sub
End Class
