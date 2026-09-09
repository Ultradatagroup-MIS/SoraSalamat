Public Class frmAN_AddNewHadaf
#Region "Variable AND Constant Declration"
    Const cntCodeSubSystem As Long = 1000141
    Dim cmTitr As CurrencyManager
    Dim dvTitr As DataView
    Dim tCodeCounter As Long
    Dim ErrPro As New ErrorProvider
    Dim dsForm As New DataSet
    Dim dvForm As DataView
    Private SN As Integer
    Dim txtCaption As String
    Private WithEvents BS As New UD_Dll.PassString
    Public ModeForm As UD_Dll.Enums.GL_ModeForms = UD_Dll.Enums.GL_ModeForms.None
    Public ccHadaf As Integer = 0
    Dim Flg_Load As Boolean = False

#End Region
#Region "Form Event Code"
    Private Sub frmFO_AddNewHadaf_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetParameter()
        SN = objSec.GetSecurityNumber(cntCodeSubSystem, UserName)
        LoadCombo()
        ClearForm()
        Flg_Load = True


        If ModeForm = UD_Dll.Enums.GL_ModeForms.UpdateRecord Then
            SetFormForUpdate()
            txtNoeForm.Text = "ویـرایش هـــــدف خـــــریـــد"
        Else
            txtNoeForm.Text = "تعــریف هـــــدف خـــــریـــد"
        End If

        objTools.ChangeKbd(UD_Dll.mdlUtility.KBD.Farsi)
    End Sub
    Private Sub CheckIsNumeric(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles _
     txtRialHadaf.KeyPress, txtTedadHadaf.KeyPress
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
    Private Sub cmbNoeKalaeiHadaf_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbNoeKalaeiHadaf.SelectedIndexChanged
        SetNoeKalaeiHadaf(cmbNoeKalaeiHadaf.SelectedIndex)
    End Sub
    Private Sub cmbNoeMohasebeHadaf_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbNoeMohasebeHadaf.SelectedIndexChanged
        SetNoeMohasebehHadaf(cmbNoeMohasebeHadaf.SelectedIndex)
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
            CodeDoreh = "1394"
            txtCaption = " تعریف و ویرایش هدف خرید"
            ObjCode.UserName = UserName
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
    Private Sub LoadCombo()
        Dim cnSQL As New SqlConnection
        Dim cmSQL As New SqlCommand
        Dim daSQL As New SqlDataAdapter
        Dim strSQL As String = ""

        Try
            '_________________ Load Tamin Konandeh __________________
            cmbNoeKalaeiHadaf.Items.Add("خرید از تامین کننده")
            cmbNoeKalaeiHadaf.Items.Add("یک کالای خـاص")
            cmbNoeKalaeiHadaf.Items.Add("یک برنـد خـاص")
            cmbNoeKalaeiHadaf.Items.Add("گــروه کالا")
            '_________________ Load Tamin Konandeh __________________

            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()

            '_________________ Load Tamin Konandeh __________________
            strSQL = "Global.spTaminKonandeh_LoadCombo "

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            daSQL = New SqlDataAdapter(cmSQL)
            daSQL.Fill(dsForm, "tbl_TaminKonandeh")

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

            '_________________ Load Noe Mohasebe __________________
            cmbNoeMohasebeHadaf.Items.Add("--------")
            cmbNoeMohasebeHadaf.Items.Add("هــدف ریـالی")
            cmbNoeMohasebeHadaf.Items.Add("هــدف تعدادی")
            '_________________ Load Noe Mohasebe __________________

            '_________________ Load Noe Mohasebe __________________
            cmbNoeBasehBandy.Items.Add("--------")
            cmbNoeBasehBandy.Items.Add("عـــدد")
            cmbNoeBasehBandy.Items.Add("کارتـن")
            '_________________ Load Noe Mohasebe __________________

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "Error IN DataBase ----> LoadCombo")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, "Error IN ----> LoadCombo")
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
    Private Sub ClearForm()
        cmbNoeKalaeiHadaf.SelectedIndex = 0
        txtSharhHadaf.Text = ""
        cmbTaminKonandeh.SelectedIndex = -1
        cmbNoeMohasebeHadaf.SelectedIndex = 0
        txtRialHadaf.Text = ""
        txtTedadHadaf.Text = ""
        cmbNoeBasehBandy.SelectedIndex = 0

        Dim AzTarikh As String = ""
        Dim TaTarikh As String = ""
        Dim ccMah As String = ""

        ccMah = TarikhEmrooz.Substring(4, 2)

        mskAzTarikh.Text = CodeDoreh.ToString + ccMah + "01"

        If ccMah <= "06" Then
            mskTaTarikh.Text = CodeDoreh.ToString + ccMah + "31"
        ElseIf ccMah <= "11" Then
            mskTaTarikh.Text = CodeDoreh.ToString + ccMah + "30"
        Else
            If objTarikh.LeapYearShamsi(CodeDoreh) Then
                mskTaTarikh.Text = CodeDoreh.ToString + ccMah + "30"
            Else
                mskTaTarikh.Text = CodeDoreh.ToString + ccMah + "29"
            End If
        End If

        txtSharhHadaf.Focus()
    End Sub
    Private Sub SetNoeKalaeiHadaf(ByVal Type As Integer)
        txtCodeKala.Text = ""
        txtCodeKala.Tag = 0
        txtCodeKala.Enabled = False
        lblNameKala.Text = ""

        cmbBrand.SelectedIndex = -1
        cmbBrand.Enabled = False

        cmbGoroh1.SelectedIndex = -1
        cmbGoroh2.SelectedIndex = -1
        cmbGoroh3.SelectedIndex = -1
        cmbGoroh4.SelectedIndex = -1
        cmbGoroh5.SelectedIndex = -1
        cmbGoroh1.Enabled = False
        cmbGoroh2.Enabled = False
        cmbGoroh3.Enabled = False
        cmbGoroh4.Enabled = False
        cmbGoroh5.Enabled = False

        If Type = 1 Then
            txtCodeKala.Enabled = True
        ElseIf Type = 2 Then
            cmbBrand.Enabled = True
        ElseIf Type = 3 Then
            cmbGoroh1.Enabled = True
            cmbGoroh2.Enabled = True
            cmbGoroh3.Enabled = True
            cmbGoroh4.Enabled = True
            cmbGoroh5.Enabled = True
        End If
    End Sub
    Private Sub SetNoeMohasebehHadaf(ByVal Type As Integer)
        txtRialHadaf.Text = ""
        txtRialHadaf.Enabled = False

        txtTedadHadaf.Text = ""
        txtTedadHadaf.Enabled = False
        cmbNoeBasehBandy.SelectedIndex = 0
        cmbNoeBasehBandy.Enabled = False

        If Type = 1 Then
            txtRialHadaf.Enabled = True
        ElseIf Type = 2 Then
            txtTedadHadaf.Enabled = True
            cmbNoeBasehBandy.Enabled = True
        End If
    End Sub
    Private Function IsValid() As Boolean
        IsValid = False
        Try
            If cmbNoeKalaeiHadaf.SelectedIndex = -1 Then
                MsgBox("لطفا نوع کالایی هـدف را مشخص نمایید !", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, "خطا")
                ErrPro.SetError(cmbNoeKalaeiHadaf, "لطفا نوع کالایی هـدف را مشخص نمایید !")
                cmbNoeKalaeiHadaf.Focus()
                Exit Function
            End If
            ErrPro.SetError(cmbNoeKalaeiHadaf, "")


            If Me.mskAzTarikh.Text = "" Then
                ErrPro.SetError(Me.mskAzTarikh, "از تاریخ را وارد نمایید !")
                MsgBox("از تاریخ را وارد نمایید !", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
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
                MsgBox("تا تاریخ را وارد نمایید !", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
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
                mskTaTarikh.Focus()
                Exit Function
            End If
            ErrPro.SetError(Me.mskTaTarikh, "")


            If cmbTaminKonandeh.SelectedIndex = -1 Then
                MsgBox("لطفا تامین کننده را مشخص نمایید !", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, "خطا")
                ErrPro.SetError(cmbTaminKonandeh, "لطفا تامین کننده را مشخص نمایید !")
                cmbTaminKonandeh.Focus()
                Exit Function
            End If
            ErrPro.SetError(cmbTaminKonandeh, "")


            If txtSharhHadaf.Text.Trim = "" Then
                MsgBox("لطفا برای هدف شرح وارد نمایید !", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, "خطا")
                ErrPro.SetError(txtSharhHadaf, "لطفا برای هدف شرح وارد نمایید !")
                txtSharhHadaf.Focus()
                Exit Function
            End If
            ErrPro.SetError(txtSharhHadaf, "")


            If cmbNoeKalaeiHadaf.SelectedIndex = 1 Then
                If txtCodeKala.Tag = 0 Then
                    ErrPro.SetError(Me.txtCodeKala, "باید یک کالا انتخاب نمایید !")
                    MsgBox("باید یک کالا انتخاب نمایید !", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
                    txtCodeKala.Focus()
                    Exit Function
                End If
                ErrPro.SetError(Me.txtCodeKala, "")
            ElseIf cmbNoeKalaeiHadaf.SelectedIndex = 2 Then
                If cmbBrand.SelectedIndex = -1 Then
                    MsgBox("لطفا یک برنـد انتخاب نمایید !", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, "خطا")
                    ErrPro.SetError(cmbBrand, "لطفا یک برنـد انتخاب نمایید !")
                    cmbBrand.Focus()
                    Exit Function
                End If
                ErrPro.SetError(cmbBrand, "")
            ElseIf cmbNoeKalaeiHadaf.SelectedIndex = 3 Then
                If cmbGoroh1.SelectedIndex = -1 Then
                    MsgBox("حداقل باید یک گروه کالایی انتخاب نمایید !", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, "خطا")
                    ErrPro.SetError(cmbGoroh1, "حداقل باید یک گروه کالایی انتخاب نمایید !")
                    cmbGoroh1.Focus()
                    Exit Function
                End If
                ErrPro.SetError(cmbGoroh1, "")
            End If


            If cmbNoeMohasebeHadaf.SelectedIndex = -1 Or cmbNoeMohasebeHadaf.SelectedIndex = 0 Then
                MsgBox("لطفا نوع محاسبه هدف را مشخص نمایید !", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, "خطا")
                ErrPro.SetError(cmbNoeMohasebeHadaf, "لطفا نوع محاسبه هدف را مشخص نمایید !")
                cmbNoeMohasebeHadaf.Focus()
                Exit Function
            End If
            ErrPro.SetError(cmbNoeMohasebeHadaf, "")


            If cmbNoeMohasebeHadaf.SelectedIndex = 1 Then
                If txtRialHadaf.Text.Trim = "" Then
                    MsgBox("لطفا ریـال هدف را مشخص نمایید !", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, "خطا")
                    ErrPro.SetError(txtRialHadaf, "لطفا ریـال هدف را مشخص نمایید !")
                    txtRialHadaf.Focus()
                    Exit Function
                End If
                ErrPro.SetError(txtRialHadaf, "")
            ElseIf cmbNoeMohasebeHadaf.SelectedIndex = 2 Then
                If txtTedadHadaf.Text.Trim = "" Then
                    MsgBox("لطفا تعداد هدف را مشخص نمایید !", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, "خطا")
                    ErrPro.SetError(txtTedadHadaf, "لطفا تعداد هدف را مشخص نمایید !")
                    txtTedadHadaf.Focus()
                    Exit Function
                End If
                ErrPro.SetError(txtTedadHadaf, "")

                If cmbNoeBasehBandy.SelectedIndex = -1 Or cmbNoeBasehBandy.SelectedIndex = 0 Then
                    MsgBox("لطفا نوع هدف تعدادی را مشخص نمایید !", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, "خطا")
                    ErrPro.SetError(cmbNoeBasehBandy, "لطفا نوع هدف تعدادی را مشخص نمایید !")
                    cmbNoeBasehBandy.Focus()
                    Exit Function
                End If
                ErrPro.SetError(cmbNoeBasehBandy, "")
            End If

            If ValidRecord() = True Then
                MsgBox("در بازه انتخاب شده هدف مشابه وجود دارد !", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, "خطا")
                Exit Function
            End If

            Return True
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "Error IN DataBase ----> IsValid")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, "Error IN ----> IsValid")
        End Try
    End Function
    Private Function ValidRecord() As Boolean
        ValidRecord = False

        Dim cnSQL As New SqlConnection
        Dim cmSQL As New SqlCommand
        Dim strSQL As String = ""

        Try
            strSQL = "WareHouse.spHadafKharid_ValidRecord "

            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            cmSQL.Parameters.AddWithValue("CodeDoreh", CodeDoreh)
            cmSQL.Parameters.AddWithValue("NoeKalaeiHadaf", cmbNoeKalaeiHadaf.SelectedIndex)
            cmSQL.Parameters.AddWithValue("AzTarikh", mskAzTarikh.Text)
            cmSQL.Parameters.AddWithValue("TaTarikh", mskTaTarikh.Text)
            cmSQL.Parameters.AddWithValue("ccTaminKonandeh", cmbTaminKonandeh.SelectedValue)
            cmSQL.Parameters.AddWithValue("ccKala", IIf(cmbNoeKalaeiHadaf.SelectedIndex <> 1, 0, txtCodeKala.Tag))
            cmSQL.Parameters.AddWithValue("ccBrand", IIf(cmbNoeKalaeiHadaf.SelectedIndex <> 2, 0, cmbBrand.SelectedValue))
            cmSQL.Parameters.AddWithValue("sG1", IIf(cmbNoeKalaeiHadaf.SelectedIndex <> 3, 0, cmbGoroh1.SelectedValue))
            cmSQL.Parameters.AddWithValue("sG2", IIf(cmbNoeKalaeiHadaf.SelectedIndex <> 3, 0, IIf(cmbGoroh2.SelectedValue = Nothing, 0, cmbGoroh2.SelectedValue)))
            cmSQL.Parameters.AddWithValue("sG3", IIf(cmbNoeKalaeiHadaf.SelectedIndex <> 3, 0, IIf(cmbGoroh3.SelectedValue = Nothing, 0, cmbGoroh3.SelectedValue)))
            cmSQL.Parameters.AddWithValue("sG4", IIf(cmbNoeKalaeiHadaf.SelectedIndex <> 3, 0, IIf(cmbGoroh4.SelectedValue = Nothing, 0, cmbGoroh4.SelectedValue)))
            cmSQL.Parameters.AddWithValue("sG5", IIf(cmbNoeKalaeiHadaf.SelectedIndex <> 3, 0, IIf(cmbGoroh5.SelectedValue = Nothing, 0, cmbGoroh5.SelectedValue)))
            cmSQL.Parameters.AddWithValue("NoeMohasebeHadaf", cmbNoeMohasebeHadaf.SelectedIndex)
            cmSQL.Parameters.AddWithValue("NoeBasehBandy", IIf(cmbNoeMohasebeHadaf.SelectedIndex <> 2, 0, cmbNoeBasehBandy.SelectedIndex))
            cmSQL.Parameters.AddWithValue("ccHadaf", ccHadaf)
            cmSQL.Parameters.AddWithValue("Valid", ValidRecord)
            cmSQL.Parameters("Valid").Direction = ParameterDirection.Output

            cmSQL.ExecuteNonQuery()

            ValidRecord = cmSQL.Parameters("Valid").Value

            cmSQL = Nothing
            cnSQL.Close()

            Return ValidRecord
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "Error IN DataBase ----> ValidRecord")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, "Error IN ----> ValidRecord")
        End Try
    End Function
    Private Function SaveHadafKharid() As Boolean
        SaveHadafKharid = False

        Dim cnSQL As New SqlConnection
        Dim cmSQL As New SqlCommand
        Dim strSQL As String = ""

        Try
            strSQL = "WareHouse.spHadafKharid_Insert "

            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            cmSQL.Parameters.AddWithValue("CodeDoreh", CodeDoreh)
            cmSQL.Parameters.AddWithValue("SharhHadaf", txtSharhHadaf.Text.Trim)
            cmSQL.Parameters.AddWithValue("NoeKalaeiHadaf", cmbNoeKalaeiHadaf.SelectedIndex)
            cmSQL.Parameters.AddWithValue("AzTarikh", mskAzTarikh.Text)
            cmSQL.Parameters.AddWithValue("TaTarikh", mskTaTarikh.Text)
            cmSQL.Parameters.AddWithValue("ccTaminKonandeh", cmbTaminKonandeh.SelectedValue)
            cmSQL.Parameters.AddWithValue("ccKala", IIf(cmbNoeKalaeiHadaf.SelectedIndex <> 1, 0, txtCodeKala.Tag))
            cmSQL.Parameters.AddWithValue("ccBrand", IIf(cmbNoeKalaeiHadaf.SelectedIndex <> 2, 0, cmbBrand.SelectedValue))
            cmSQL.Parameters.AddWithValue("sG1", IIf(cmbNoeKalaeiHadaf.SelectedIndex <> 3, 0, cmbGoroh1.SelectedValue))
            cmSQL.Parameters.AddWithValue("sG2", IIf(cmbNoeKalaeiHadaf.SelectedIndex <> 3, 0, IIf(cmbGoroh2.SelectedValue = Nothing, 0, cmbGoroh2.SelectedValue)))
            cmSQL.Parameters.AddWithValue("sG3", IIf(cmbNoeKalaeiHadaf.SelectedIndex <> 3, 0, IIf(cmbGoroh3.SelectedValue = Nothing, 0, cmbGoroh3.SelectedValue)))
            cmSQL.Parameters.AddWithValue("sG4", IIf(cmbNoeKalaeiHadaf.SelectedIndex <> 3, 0, IIf(cmbGoroh4.SelectedValue = Nothing, 0, cmbGoroh4.SelectedValue)))
            cmSQL.Parameters.AddWithValue("sG5", IIf(cmbNoeKalaeiHadaf.SelectedIndex <> 3, 0, IIf(cmbGoroh5.SelectedValue = Nothing, 0, cmbGoroh5.SelectedValue)))
            cmSQL.Parameters.AddWithValue("NoeMohasebeHadaf", cmbNoeMohasebeHadaf.SelectedIndex)
            cmSQL.Parameters.AddWithValue("RialHadaf", IIf(cmbNoeMohasebeHadaf.SelectedIndex <> 1, 0, ObjCode.DigitSepratorRemover(txtRialHadaf.Text.Trim)))
            cmSQL.Parameters.AddWithValue("TedadHadaf", IIf(cmbNoeMohasebeHadaf.SelectedIndex <> 2, 0, ObjCode.DigitSepratorRemover(txtTedadHadaf.Text.Trim)))
            cmSQL.Parameters.AddWithValue("NoeBasehBandy", IIf(cmbNoeMohasebeHadaf.SelectedIndex <> 2, 0, cmbNoeBasehBandy.SelectedIndex))

            cmSQL.ExecuteNonQuery()

            cmSQL = Nothing
            cnSQL.Close()

            Return True
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "Error IN DataBase ----> SaveHadafKharid")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, "Error IN ----> SaveHadafKharid")
        End Try
    End Function
    Private Function UpdateHadafKharid() As Boolean
        UpdateHadafKharid = False

        Dim cnSQL As New SqlConnection
        Dim cmSQL As New SqlCommand
        Dim strSQL As String = ""

        Try
            strSQL = "WareHouse.spHadafKharid_Update "

            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            cmSQL.Parameters.AddWithValue("CodeDoreh", CodeDoreh)
            cmSQL.Parameters.AddWithValue("SharhHadaf", txtSharhHadaf.Text.Trim)
            cmSQL.Parameters.AddWithValue("NoeKalaeiHadaf", cmbNoeKalaeiHadaf.SelectedIndex)
            cmSQL.Parameters.AddWithValue("AzTarikh", mskAzTarikh.Text)
            cmSQL.Parameters.AddWithValue("TaTarikh", mskTaTarikh.Text)
            cmSQL.Parameters.AddWithValue("ccTaminKonandeh", cmbTaminKonandeh.SelectedValue)
            cmSQL.Parameters.AddWithValue("ccKala", IIf(cmbNoeKalaeiHadaf.SelectedIndex <> 1, 0, txtCodeKala.Tag))
            cmSQL.Parameters.AddWithValue("ccBrand", IIf(cmbNoeKalaeiHadaf.SelectedIndex <> 2, 0, cmbBrand.SelectedValue))
            cmSQL.Parameters.AddWithValue("sG1", IIf(cmbNoeKalaeiHadaf.SelectedIndex <> 3, 0, cmbGoroh1.SelectedValue))
            cmSQL.Parameters.AddWithValue("sG2", IIf(cmbNoeKalaeiHadaf.SelectedIndex <> 3, 0, IIf(cmbGoroh2.SelectedValue = Nothing, 0, cmbGoroh2.SelectedValue)))
            cmSQL.Parameters.AddWithValue("sG3", IIf(cmbNoeKalaeiHadaf.SelectedIndex <> 3, 0, IIf(cmbGoroh3.SelectedValue = Nothing, 0, cmbGoroh3.SelectedValue)))
            cmSQL.Parameters.AddWithValue("sG4", IIf(cmbNoeKalaeiHadaf.SelectedIndex <> 3, 0, IIf(cmbGoroh4.SelectedValue = Nothing, 0, cmbGoroh4.SelectedValue)))
            cmSQL.Parameters.AddWithValue("sG5", IIf(cmbNoeKalaeiHadaf.SelectedIndex <> 3, 0, IIf(cmbGoroh5.SelectedValue = Nothing, 0, cmbGoroh5.SelectedValue)))
            cmSQL.Parameters.AddWithValue("NoeMohasebeHadaf", cmbNoeMohasebeHadaf.SelectedIndex)
            cmSQL.Parameters.AddWithValue("RialHadaf", IIf(cmbNoeMohasebeHadaf.SelectedIndex <> 1, 0, ObjCode.DigitSepratorRemover(txtRialHadaf.Text.Trim)))
            cmSQL.Parameters.AddWithValue("TedadHadaf", IIf(cmbNoeMohasebeHadaf.SelectedIndex <> 2, 0, ObjCode.DigitSepratorRemover(txtTedadHadaf.Text.Trim)))
            cmSQL.Parameters.AddWithValue("NoeBasehBandy", IIf(cmbNoeMohasebeHadaf.SelectedIndex <> 2, 0, cmbNoeBasehBandy.SelectedIndex))
            cmSQL.Parameters.AddWithValue("ccHadafKharid", ccHadaf)

            cmSQL.ExecuteNonQuery()

            cmSQL = Nothing
            cnSQL.Close()

            Return True
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "Error IN DataBase ----> UpdateHadafKharid")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, "Error IN ----> UpdateHadafKharid")
        End Try
    End Function
    Private Sub SetFormForUpdate()
        Dim NoeKalaeiHadaf As Integer = 0
        NoeKalaeiHadaf = objTools.DLookup("NoeKalaeiHadaf", "WareHouse.HadafKharid", "ccHadafKharid = " & ccHadaf)

        cmbNoeKalaeiHadaf.SelectedIndex = NoeKalaeiHadaf
        mskAzTarikh.Text = objTools.DLookup("AzTarikh", "WareHouse.HadafKharid", "ccHadafKharid = " & ccHadaf)
        mskTaTarikh.Text = objTools.DLookup("TaTarikh", "WareHouse.HadafKharid", "ccHadafKharid = " & ccHadaf)
        txtSharhHadaf.Text = objTools.DLookup("SharhHadaf", "WareHouse.HadafKharid", "ccHadafKharid = " & ccHadaf)
        cmbTaminKonandeh.SelectedValue = objTools.DLookup("ccTaminKonandeh", "WareHouse.HadafKharid", "ccHadafKharid = " & ccHadaf)

        If NoeKalaeiHadaf = 1 Then
            Dim ccKala As Integer = objTools.DLookup("ccKala", "WareHouse.HadafKharid", "ccHadafKharid = " & ccHadaf)
            txtCodeKala.Text = objTools.DLookup("Codekala", "tblAN_Kala", "ccKala = " & ccKala)
        ElseIf NoeKalaeiHadaf = 2 Then
            cmbBrand.SelectedValue = objTools.DLookup("ccBrand", "WareHouse.HadafKharid", "ccHadafKharid = " & ccHadaf)
        ElseIf NoeKalaeiHadaf = 3 Then
            Dim sG1 As Integer = 0
            Dim sG2 As Integer = 0
            Dim sG3 As Integer = 0
            Dim sG4 As Integer = 0
            Dim sG5 As Integer = 0

            sG1 = objTools.DLookup("sG1", "WareHouse.HadafKharid", "ccHadafKharid = " & ccHadaf)
            sG2 = objTools.DLookup("sG2", "WareHouse.HadafKharid", "ccHadafKharid = " & ccHadaf)
            sG3 = objTools.DLookup("sG3", "WareHouse.HadafKharid", "ccHadafKharid = " & ccHadaf)
            sG4 = objTools.DLookup("sG4", "WareHouse.HadafKharid", "ccHadafKharid = " & ccHadaf)
            sG5 = objTools.DLookup("sG5", "WareHouse.HadafKharid", "ccHadafKharid = " & ccHadaf)

            cmbGoroh1.SelectedValue = sG1
            If sG2 <> 0 Then
                LoadComboGoroh2()
                cmbGoroh2.SelectedValue = sG2
                If sG3 <> 0 Then
                    LoadComboGoroh3()
                    cmbGoroh3.SelectedValue = sG3
                    If sG4 <> 0 Then
                        LoadComboGoroh4()
                        cmbGoroh4.SelectedValue = sG4
                        If sG5 <> 0 Then
                            LoadComboGoroh5()
                            cmbGoroh5.SelectedValue = sG5
                        Else
                            cmbGoroh5.SelectedIndex = -1
                        End If
                    Else
                        cmbGoroh4.SelectedIndex = -1
                        cmbGoroh5.SelectedIndex = -1
                    End If
                Else
                    cmbGoroh3.SelectedIndex = -1
                    cmbGoroh4.SelectedIndex = -1
                    cmbGoroh5.SelectedIndex = -1
                End If
            Else
                cmbGoroh2.SelectedIndex = -1
                cmbGoroh3.SelectedIndex = -1
                cmbGoroh4.SelectedIndex = -1
                cmbGoroh5.SelectedIndex = -1
            End If
        End If

        Dim NoeMohasebeHadaf As Integer = 0
        NoeMohasebeHadaf = objTools.DLookup("NoeMohasebeHadaf", "WareHouse.HadafKharid", "ccHadafKharid = " & ccHadaf)

        cmbNoeMohasebeHadaf.SelectedIndex = NoeMohasebeHadaf

        If NoeMohasebeHadaf = 1 Then
            txtRialHadaf.Text = objTools.DLookup("RialHadaf", "WareHouse.HadafKharid", "ccHadafKharid = " & ccHadaf)
        ElseIf NoeMohasebeHadaf = 2 Then
            txtTedadHadaf.Text = objTools.DLookup("TedadHadaf", "WareHouse.HadafKharid", "ccHadafKharid = " & ccHadaf)
            cmbNoeBasehBandy.SelectedIndex = objTools.DLookup("NoeBasehBandy", "WareHouse.HadafKharid", "ccHadafKharid = " & ccHadaf)
        End If
    End Sub
#End Region
#Region "From Buttons "
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If IsValid() = False Then
            Exit Sub
        End If

        If ModeForm = UD_Dll.Enums.GL_ModeForms.AddNewRecord Then
            If SaveHadafKharid() Then
                MsgBox("عملیات ثبت با موفقیت انجام گردید .", MsgBoxStyle.Information + MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, "ذخیـره")
                Me.Close()
                Exit Sub
            End If
        ElseIf ModeForm = UD_Dll.Enums.GL_ModeForms.UpdateRecord Then
            If UpdateHadafKharid() Then
                MsgBox("هــدف انتخاب شده با موفقیت ویرایش گردید .", MsgBoxStyle.Information + MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, "ذخیـره")
                Me.Close()
                Exit Sub
            End If
        End If
    End Sub
    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        ClearForm()
    End Sub
    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub
#End Region
#Region "Stored Procedures"
    '' Global.spTaminKonandeh_LoadCombo
    '' Global.spBrand_LoadCombo
    '' Global.spGorohKala_1_LoadCombo
    '' Global.spGorohKala_2_LoadCombo
    '' Global.spGorohKala_3_LoadCombo
    '' Global.spGorohKala_4_LoadCombo
    '' Global.spGorohKala_5_LoadCombo

    '' WareHouse.spHadafKharid_ValidRecord
    '' WareHouse.spHadafKharid_Insert
    '' WareHouse.spHadafKharid_Update
#End Region
End Class