Public Class frmFO_ElamMarjoee
#Region "Variable AND Constant Declration"
    Const cntCodeSubSystem As Long = 1000126
    Const OrgSizeGrid As Integer = 251
    Const SizeGrid_EditMode As Integer = 186
    Dim Mode As UD_Dll.Enums.GL_ModeForms = UD_Dll.Enums.GL_ModeForms.None
    Dim cmElamMarjoee, cmElamMarjoee_Kartabl, cmMarjoeeAzMoshtary As CurrencyManager
    Dim dvElamMarjoee, dvElamMarjoeeSatr, dvElamMarjoee_Kartabl, dvElamMarjoeeSatr_Kartabl, dvMarjoeeAzMoshtary, dvMarjoeeAzMoshtary_Satr As DataView
    Dim ErrPro As New ErrorProvider
    Dim dsForm As New DataSet
    Private SN As Integer
    Dim tPos As Integer
    Dim txtCaption As String
    Private WithEvents BS As New UD_Dll.PassString
    Dim AllowMarjoeeWithoutFaktor As Boolean = objTools.ConvertNulls(objTools.DLookup("AllowMarjoeeWithoutFaktor", "tblGL_SysConfig", "CodeMahal = " & CodeMahalFaal), False)
    Dim AllowChangeFeeMarjoee As Boolean = objTools.ConvertNulls(objTools.DLookup("AllowChangeFeeMarjoee", "dbo.tblGL_SysConfig", "CodeMahal = " & CodeMahalFaal), False)
    Dim AllowPishFaktorTakhfifDasty As Boolean = objTools.ConvertNulls(objTools.DLookup("AllowPishFaktorTakhfifDasty", "tblGL_SysConfig", ""), False)

    Public ccTafkik_GG As Integer
    Dim ccMoshtary As Integer
    Dim ccForoshandeh As Integer = 0
    Dim ccFaktorTitr As Integer
    Dim FaktorShomareh As Integer
    Dim CodeDorehFaktor As Integer
    Dim flg As Boolean = False
    Dim ShomarehPishFaktor As Integer = 0

    Dim WithEvents TJ As TakhfifOJavaiez.TakhfifJayezeh
#End Region

    Private Sub frmFO_ElamMarjoee_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetParameter()
        SN = objSec.GetSecurityNumber(cntCodeSubSystem, UserName)
        AllowMarjoeeWithoutFaktor = objTools.ConvertNulls(objTools.DLookup("AllowMarjoeeWithoutFaktor", "tblGL_SysConfig", "CodeMahal = " & CodeMahalFaal), False)
        ClearForm()
        LoadCombo()
        Search(tbMain.SelectedIndex)
    End Sub
    Private Sub SetParameter()
        Dim commands As String = Microsoft.VisualBasic.Command()
        If commands.Length = 0 Then
            UserName = "Administrator"
            UserPassWord = "66998833"
            NameMahalFaal = "تهران"
            CodeMahalFaal = "2049"
            PersonelCode = "0"
            PersonelName = "Administrator"
            CodeDoreh = "1396"
            txtCaption = "اعلام مرجوعی"
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
        Try
            Dim cnSQL As New SqlConnection
            Dim cmSQL As New SqlCommand
            Dim daSQL As SqlDataAdapter
            Dim strSQL As String

            ' --------------------------------------- Load Combo -----------------
            ' Load Combo Doreh
            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()

            strSQL = "Global.spCodeDorehFaktor_LoadCombo "

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            daSQL = New SqlDataAdapter(cmSQL)
            daSQL.Fill(dsForm, "tblCodeDoreh")
            cmbsCodeDorehFaktor.DataSource = Nothing
            cmbsCodeDorehFaktor.Items.Clear()
            cmbsCodeDorehFaktor.DataSource = dsForm.Tables("tblCodeDoreh").DefaultView
            cmbsCodeDorehFaktor.DisplayMember = "CodeDoreh"
            cmbsCodeDorehFaktor.ValueMember = "CodeDoreh"

            ' Load Combo sElatMarjoee
            strSQL = "Global.spElatMarjoee_LoadCombo"

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            daSQL = New SqlDataAdapter(cmSQL)
            daSQL.Fill(dsForm, "tblElatMarjoee")

            cmbElatFaktorMarjoee.DataSource = Nothing
            cmbElatFaktorMarjoee.Items.Clear()
            cmbElatFaktorMarjoee.DataSource = dsForm.Tables("tblElatMarjoee").DefaultView
            cmbElatFaktorMarjoee.DisplayMember = "Sharh"
            cmbElatFaktorMarjoee.ValueMember = "Code"

            cmbsElatMarjoee.DataSource = Nothing
            cmbsElatMarjoee.Items.Clear()
            cmbsElatMarjoee.DataSource = dsForm.Tables("tblElatMarjoee").DefaultView
            cmbsElatMarjoee.DisplayMember = "Sharh"
            cmbsElatMarjoee.ValueMember = "Code"

            strSQL = "Global.spForoshandeh_LoadCombo"

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            cmSQL.Parameters.AddWithValue("CodeMahal", CodeMahalFaal)
            cmSQL.Parameters.AddWithValue("sVazeiat", UD_Dll.Enums.FO_VaziatForoshandeh.NoFaal)
            cmSQL.Parameters.AddWithValue("UserName", UserName)

            daSQL = New SqlDataAdapter(cmSQL)
            daSQL.Fill(dsForm, "tblForoshandeh")

            cmbForoshandeh.DataSource = Nothing
            cmbForoshandeh.Items.Clear()
            cmbForoshandeh.DataSource = dsForm.Tables("tblForoshandeh").DefaultView
            cmbForoshandeh.ValueMember = "ccForoshandeh"
            cmbForoshandeh.DisplayMember = "LN"
            cmbForoshandeh.SelectedIndex = -1

            cmSQL = Nothing : daSQL = Nothing
            cnSQL.Close()

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> LoadCombo ")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> LoadCombo ")
        End Try
    End Sub
    Private Sub ClearForm()
        Mode = UD_Dll.Enums.GL_ModeForms.None

        If tbMain.SelectedIndex = 0 Then
            ccFaktorTitr = 0
            CodeDorehFaktor = 0
            FaktorShomareh = 0
            ccForoshandeh = 0
            ccMoshtary = 0

            txtCodeMoshtary.Tag = 0
            txtCodeMoshtary.Text = ""
            lblNameMoshtary.Text = ""

            cmbsCodeDorehFaktor.SelectedIndex = -1
            cmbsCodeDorehFaktor.SelectedIndex = -1

            chkFaktorMarjoee.Checked = False

            txtMJayeze.Text = 0
            txtMTakhfif.Text = 0
            txtMJayeze.Enabled = False
            txtMTakhfif.Enabled = False

            chkFaktorMarjoee.Checked = False

            lblElatFaktorMarjoee.Visible = False
            cmbElatFaktorMarjoee.Visible = False
            cmbElatFaktorMarjoee.SelectedIndex = -1
            cmbElatFaktorMarjoee.SelectedIndex = -1

            cmbsElatMarjoee.SelectedIndex = -1
            cmbsElatMarjoee.SelectedIndex = -1

            txtCodeKala.Text = ""
            txtCodeKala.Tag = ""
            lblNameKala.Text = ""

            mskTarikhElamMarjoee.Text = TarikhEmrooz

            txtFee.Text = ""
            txtTedadKala.Text = ""

            txtShomarehFaktor.Text = ""
            txtTedadKala.Text = ""

            cmbForoshandeh.SelectedIndex = -1
            cmbForoshandeh.SelectedIndex = -1
            cmbsCodeDorehFaktor.Text = CodeDoreh
            cmbForoshandeh.Enabled = True

            rbKharab.Checked = False
            rbSalem.Checked = False
        End If

        SetButton()
    End Sub
    Private Sub SetButton()
        If tbMain.SelectedIndex = 0 Then
            If Mode = UD_Dll.Enums.GL_ModeForms.None Then
                btnAddTitr.Enabled = True
                btnRemoveTitr.Enabled = True
                btnSaveTitr.Enabled = False
                btnCancelTitr.Enabled = False
                btnErsal.Enabled = True

                btnAddSatr.Enabled = True
                btnRemoveSatr.Enabled = True
                btnSaveSatr.Enabled = False
                btnCancelSatr.Enabled = False

                GridEXElamMarjoee.Height = OrgSizeGrid
                GridEXElamMarjoeeSatr.Height = OrgSizeGrid

                GridEXElamMarjoee.Enabled = True
                GridEXElamMarjoeeSatr.Enabled = True

            ElseIf Mode = UD_Dll.Enums.GL_ModeForms.AddNewRecord Or Mode = UD_Dll.Enums.GL_ModeForms.UpdateRecord Then
                btnAddTitr.Enabled = False
                btnRemoveTitr.Enabled = False
                btnSaveTitr.Enabled = True
                btnCancelTitr.Enabled = True
                btnErsal.Enabled = False

                btnAddSatr.Enabled = False
                btnRemoveSatr.Enabled = False
                btnSaveSatr.Enabled = False
                btnCancelSatr.Enabled = False

                GridEXElamMarjoee.Height = SizeGrid_EditMode
                GridEXElamMarjoeeSatr.Enabled = False
            ElseIf Mode = UD_Dll.Enums.GL_ModeForms.AddNewRow Or Mode = UD_Dll.Enums.GL_ModeForms.UpdateRow Then
                btnAddTitr.Enabled = False
                btnRemoveTitr.Enabled = False
                btnSaveTitr.Enabled = False
                btnCancelTitr.Enabled = False
                btnErsal.Enabled = False

                btnAddSatr.Enabled = False
                btnRemoveSatr.Enabled = False
                btnSaveSatr.Enabled = True
                btnCancelSatr.Enabled = True

                GridEXElamMarjoee.Enabled = False
                GridEXElamMarjoeeSatr.Height = SizeGrid_EditMode

            End If
        ElseIf tbMain.SelectedIndex = 1 Then

        ElseIf tbMain.SelectedIndex = 2 Then

        End If
    End Sub
    Private Sub Search(ByVal NoeForm As Integer)
        '' Noe ---> 1 : ElamMarjoee // 2 : Kartabl // 3 : Marjoee Az Moshtary

        Dim cnSQL As New SqlConnection
        Dim cmSQL As New SqlCommand
        Dim daSQL As New SqlDataAdapter
        Dim strSQL As String = ""

        Try
            strSQL = "Sales.spPishFaktorGheireGhateei_ElamMarjoee_Search "

            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            cmSQL.Parameters.AddWithValue("ccTafkik_GG", ccTafkik_GG)
            cmSQL.Parameters.AddWithValue("SearchType", NoeForm)

            daSQL = New SqlDataAdapter(cmSQL)

            If NoeForm = 0 Then
                If dsForm.Tables.Contains("tblElamMarjoee") Then
                    dsForm.Tables.Remove("tblElamMarjoee")
                End If

                daSQL.Fill(dsForm, "tblElamMarjoee")

                '------------Adding Columns------------
                dsForm.Tables("tblElamMarjoee").Columns.Add("Taeed", GetType(Boolean))
                '--------------------------------------

                Dim dr As DataRow
                For Each dr In dsForm.Tables("tblElamMarjoee").Rows
                    dr("Taeed") = False
                Next

                dvElamMarjoee = New DataView(dsForm.Tables("tblElamMarjoee"))
                dvElamMarjoee.Sort = "ShomarehElamMarjoee ASC"

                dvElamMarjoee.AllowNew = False
                dvElamMarjoee.AllowDelete = False
                dvElamMarjoee.AllowEdit = True
            ElseIf NoeForm = 1 Then
                If dsForm.Tables.Contains("tblElamMarjoee_Kartabl") Then
                    dsForm.Tables.Remove("tblElamMarjoee_Kartabl")
                End If

                daSQL.Fill(dsForm, "tblElamMarjoee_Kartabl")

                '------------Adding Columns------------
                dsForm.Tables("tblElamMarjoee_Kartabl").Columns.Add("Taeed", GetType(Boolean))
                '--------------------------------------

                Dim dr As DataRow
                For Each dr In dsForm.Tables("tblElamMarjoee_Kartabl").Rows
                    dr("Taeed") = False
                Next

                dvElamMarjoee_Kartabl = New DataView(dsForm.Tables("tblElamMarjoee_Kartabl"))
                dvElamMarjoee_Kartabl.Sort = "ShomarehElamMarjoee ASC"

                dvElamMarjoee_Kartabl.AllowNew = False
                dvElamMarjoee_Kartabl.AllowDelete = False
                dvElamMarjoee_Kartabl.AllowEdit = True
            ElseIf NoeForm = 2 Then
                If dsForm.Tables.Contains("tblMarjoeeAzMoshtary") Then
                    dsForm.Tables.Remove("tblMarjoeeAzMoshtary")
                End If

                daSQL.Fill(dsForm, "tblMarjoeeAzMoshtary")

                '------------Adding Columns------------
                dsForm.Tables("tblMarjoeeAzMoshtary").Columns.Add("Taeed", GetType(Boolean))
                '--------------------------------------

                Dim dr As DataRow
                For Each dr In dsForm.Tables("tblMarjoeeAzMoshtary").Rows
                    dr("Taeed") = False
                Next

                dvMarjoeeAzMoshtary = New DataView(dsForm.Tables("tblMarjoeeAzMoshtary"))
                dvMarjoeeAzMoshtary.Sort = "ShomarehElamMarjoee ASC"

                dvMarjoeeAzMoshtary.AllowNew = False
                dvMarjoeeAzMoshtary.AllowDelete = False
                dvMarjoeeAzMoshtary.AllowEdit = True
            End If

            cmSQL = Nothing : daSQL = Nothing
            cnSQL.Close()

            If NoeForm = 0 Then
                SetGrid_ElamMarjoee()
                With GridEXElamMarjoee
                    .Visible = True
                    .DataSource = Nothing
                    .DataSource = dvElamMarjoee
                End With
            ElseIf NoeForm = 1 Then
                SetGrid_ElamMarjoee_Kartabl()
                With GridEXElamMarjoee_Kartabl
                    .Visible = True
                    .DataSource = Nothing
                    .DataSource = dvElamMarjoee_Kartabl
                End With
            ElseIf NoeForm = 2 Then
                SetGrid_MarjoeeAzMoshtary()
                With GridEXMarjoeeAzMoshtary
                    .Visible = True
                    .DataSource = Nothing
                    .DataSource = dvMarjoeeAzMoshtary
                End With
            End If

            SearchSatr(tbMain.SelectedIndex)

            Me.CenterToScreen()
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> Search ")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> Search ")
        End Try
    End Sub
    Private Function SaveTitr() As Boolean
        SaveTitr = False

        Dim cnSQL As New SqlConnection
        Dim cmSQL As New SqlCommand
        Dim strSQL As String = ""

        If IsValidBeforSaveTitr("All") = False Then
            Exit Function
        End If

        Try
            strSQL = "Sales.spPishFaktorGheireGhateei_ElamMarjoee_InsertTitr "

            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            cmSQL.Parameters.AddWithValue("ccTafkik_GG", ccTafkik_GG)
            cmSQL.Parameters.AddWithValue("CodeMahal", CodeMahalFaal)
            cmSQL.Parameters.AddWithValue("CodeDoreh", CodeDoreh)
            cmSQL.Parameters.AddWithValue("ccForoshandeh", cmbForoshandeh.SelectedValue)
            cmSQL.Parameters.AddWithValue("ccMoshtary", ccMoshtary)
            cmSQL.Parameters.AddWithValue("TarikhElamMarjoee", mskTarikhElamMarjoee.Text)
            cmSQL.Parameters.AddWithValue("MablaghTakhfifDasti", IIf(txtMTakhfif.Text.Trim.Length = 0, 0, txtMTakhfif.Text))
            cmSQL.Parameters.AddWithValue("MablaghJayezeDasti", IIf(txtMJayeze.Text.Trim.Length = 0, 0, txtMJayeze.Text))
            cmSQL.Parameters.AddWithValue("ccFaktorTitr", ccFaktorTitr)
            cmSQL.Parameters.AddWithValue("IsFaktorMarjoee", chkFaktorMarjoee.Checked)
            cmSQL.Parameters.AddWithValue("sElatFaktorMarjoee", IIf(chkFaktorMarjoee.Checked = False, 0, cmbElatFaktorMarjoee.SelectedValue))
            cmSQL.Parameters.AddWithValue("UserName", UserName)
            cmSQL.Parameters.AddWithValue("Tarikh", TarikhEmrooz)
            cmSQL.Parameters.AddWithValue("Saat", Format(TimeOfDay, "HH:mm:ss"))

            cmSQL.ExecuteNonQuery()

            cnSQL.Close()

            If chkFaktorMarjoee.Checked = True Then
                Search(tbMain.SelectedIndex)
            End If

            SaveTitr = True
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> SaveTitr ")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> SaveTitr ")
        End Try
    End Function
    Private Function IsValidBeforSaveTitr(ByVal CheckField As String) As Boolean
        Try
            IsValidBeforSaveTitr = False

            Dim checkMoshtaryFaktor As Integer
            If txtShomarehFaktor.Text.Length <> 0 Then
                checkMoshtaryFaktor = objTools.ConvertNulls(objTools.DLookup("ccMoshtary", "tblFO_Faktor", "FaktorShomareh = " & txtShomarehFaktor.Text), 0)
            End If

            If CheckField = "mskTarikhElamMarjoee" Or CheckField = "All" Then
                If Len(mskTarikhElamMarjoee.Text.ToString) <> 0 Then
                    If Not objTarikh.IsShDate(mskTarikhElamMarjoee.Text.ToString) Then
                        mskTarikhElamMarjoee.Focus()
                        Exit Function
                    End If
                    If mskTarikhElamMarjoee.Text > TarikhEmrooz Then
                        ErrPro.SetError(Me.mskTarikhElamMarjoee, "تاریخ اعلام مرجوعی نباید جلوتر از تاریخ امروز  باشد.")
                        MsgBox("تاریخ اعلام مرجوعی نباید جلوتر از تاریخ امروز  باشد.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
                        mskTarikhElamMarjoee.Focus()
                        Exit Function
                    End If
                    If ObjCode.CheckCompany <> 5 Then
                        If mskTarikhElamMarjoee.Text < TarikhEmrooz Then
                            ErrPro.SetError(Me.mskTarikhElamMarjoee, "تاریخ اعلام مرجوعی نباید پیش از تاریخ امروز  باشد.")
                            MsgBox("تاریخ اعلام مرجوعی نباید پیش از تاریخ امروز  باشد.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
                            mskTarikhElamMarjoee.Focus()
                            Exit Function
                        End If
                    End If

                Else
                    ErrPro.SetError(Me.mskTarikhElamMarjoee, "تاريخ را وارد کنيد.")
                    MsgBox("تاريخ را وارد کنيد.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
                    mskTarikhElamMarjoee.Focus()
                    Exit Function
                End If
                ErrPro.SetError(Me.mskTarikhElamMarjoee, "")
            End If

            If Not AllowMarjoeeWithoutFaktor Then
                If ccFaktorTitr = 0 Then
                    ErrPro.SetError(Me.txtShomarehFaktor, "شماره فاکتور را وارد کنید.")
                    MsgBox("شماره فاکتور را وارد کنید.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
                    Exit Function
                End If
                ErrPro.SetError(Me.txtShomarehFaktor, "")
            Else
                If ccFaktorTitr = 0 AndAlso ccMoshtary = 0 Then
                    ErrPro.SetError(Me.txtCodeMoshtary, "مشتری را وارد کنید.")
                    MsgBox("مشتری را وارد کنید.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
                    Exit Function
                End If
                ErrPro.SetError(Me.txtCodeMoshtary, "")
            End If

            If txtShomarehFaktor.Text.Length <> 0 And checkMoshtaryFaktor = 0 Then
                MsgBox("این شماره فاکتور برای این مشتری نمی باشد!.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, )
                Exit Function
            End If

            If objTools.DCount("ccFaktorTitr", "tblFO_ElamMarjoee", "ccFaktorTitr = " & ccFaktorTitr & " AND IsFaktorMarjoee = 1") <> 0 Then
                MsgBox("برای این شماره فاکتور قبلاً فاکتور مرجوعی صادر شده است !.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, )
                Exit Function
            End If

            If chkFaktorMarjoee.Checked = True Then
                If objTools.DCount("ccFaktorTitr", "tblFO_ElamMarjoee", "ccFaktorTitr = " & ccFaktorTitr & " AND IsFaktorMarjoee = 0") <> 0 Then
                    MsgBox("برای این شماره فاکتور قبلاً مرجوعی جزئی صادر شده است !.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, )
                    Exit Function
                End If
            End If

            If cmbForoshandeh.SelectedIndex = -1 Then
                MsgBox("فروشنده را انتخاب کنید.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "")
                Exit Function
            End If
            If mskTarikhElamMarjoee.Text.Length <> 0 Then
                If mskTarikhElamMarjoee.Text < TarikhEmrooz Then
                    ErrPro.SetError(Me.mskTarikhElamMarjoee, "تاريخ نمی تواند از تاریخ روز عقب تر باشد.")
                    MsgBox("تاريخ نمی تواند از تاریخ روز عقب تر باشد.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
                    Exit Function
                End If
            End If
            If cmbsCodeDorehFaktor.SelectedIndex > -1 Then
                If Val(txtMJayeze.Text) + Val(txtMTakhfif.Text) > objTools.ConvertNulls(objTools.DLookup("JamKol", "tblFO_Faktor", "ccFaktorTitr=" & ccFaktorTitr), 0) Then
                    ErrPro.SetError(Me.txtMTakhfif, "جمع کل تخفیف و جایزه دستی نباید از کل مبلغ فاکتور بیشتر باشد.")
                    ErrPro.SetError(Me.txtMJayeze, "جمع کل تخفیف و جایزه دستی نباید از کل مبلغ فاکتور بیشتر باشد.")
                    MsgBox("جمع کل تخفیف و جایزه دستی نباید از کل مبلغ فاکتور بیشتر باشد.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
                    txtMTakhfif.Focus()
                    Exit Function
                End If
            Else
                If Val(txtMJayeze.Text) + Val(txtMTakhfif.Text) > Val(GridEXElamMarjoee.CurrentRow.Cells("JamMablagh").Text.Replace(",", "")) Then
                    ErrPro.SetError(Me.txtMTakhfif, "جمع کل تخفیف و جایزه دستی نباید از کل مبلغ فاکتور بیشتر باشد.")
                    ErrPro.SetError(Me.txtMJayeze, "جمع کل تخفیف و جایزه دستی نباید از کل مبلغ فاکتور بیشتر باشد.")
                    MsgBox("جمع کل تخفیف و جایزه دستی نباید از کل مبلغ فاکتور بیشتر باشد.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
                    txtMTakhfif.Focus()
                    Exit Function
                End If
            End If

            ErrPro.SetError(Me.txtMTakhfif, "")
            ErrPro.SetError(Me.txtMJayeze, "")

            IsValidBeforSaveTitr = True
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> IsValidBeforSaveTitr ")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> IsValidBeforSaveTitr ")
        End Try
    End Function
    Private Sub SearchSatr(ByVal NoeForm As Integer)
        '' Noe ---> 0 : ElamMarjoee // 1 : Kartabl // 2 : Marjoee Az Moshtary

        Dim cnSQL As New SqlConnection
        Dim cmSQL As New SqlCommand
        Dim daSQL As New SqlDataAdapter
        Dim strSQL As String = ""
        Dim PK As Integer = 0

        If NoeForm = 0 Then
            If dvElamMarjoee.Count = 0 Then
                PK = 0
            Else
                PK = Val(GridEXElamMarjoee.CurrentRow.Cells("ccElamMarjoee").Text.Replace(",", ""))
            End If
        ElseIf NoeForm = 1 Then
            If dvElamMarjoee_Kartabl.Count = 0 Then
                PK = 0
            Else
                PK = Val(GridEXElamMarjoee_Kartabl.CurrentRow.Cells("ccElamMarjoee").Text.Replace(",", ""))
            End If
        ElseIf NoeForm = 2 Then
            If dvMarjoeeAzMoshtary.Count = 0 Then
                PK = 0
            Else
                PK = Val(GridEXMarjoeeAzMoshtary.CurrentRow.Cells("ccElamMarjoee").Text.Replace(",", ""))
            End If
        End If

            Try
                strSQL = "Sales.spPishFaktorGheireGhateei_ElamMarjoee_SearchSatr "

                cnSQL = New SqlConnection(ConnectionString)
                cnSQL.Open()

                cmSQL = New SqlCommand(strSQL, cnSQL)
                cmSQL.CommandType = CommandType.StoredProcedure
                cmSQL.Parameters.Clear()

                cmSQL.Parameters.AddWithValue("ccElamMarjoee", PK)

                daSQL = New SqlDataAdapter(cmSQL)

                If NoeForm = 0 Then
                    If dsForm.Tables.Contains("tblElamMarjoeeSatr") Then
                        dsForm.Tables.Remove("tblElamMarjoeeSatr")
                    End If

                    daSQL.Fill(dsForm, "tblElamMarjoeeSatr")

                    dvElamMarjoeeSatr = New DataView(dsForm.Tables("tblElamMarjoeeSatr"))
                    dvElamMarjoeeSatr.Sort = "CodeKala ASC"

                    dvElamMarjoeeSatr.AllowNew = False
                    dvElamMarjoeeSatr.AllowDelete = False
                    dvElamMarjoeeSatr.AllowEdit = True
                ElseIf NoeForm = 1 Then
                    If dsForm.Tables.Contains("tblElamMarjoeeSatr_Kartabl") Then
                        dsForm.Tables.Remove("tblElamMarjoeeSatr_Kartabl")
                    End If

                    daSQL.Fill(dsForm, "tblElamMarjoeeSatr_Kartabl")

                    dvElamMarjoeeSatr_Kartabl = New DataView(dsForm.Tables("tblElamMarjoeeSatr_Kartabl"))
                    dvElamMarjoeeSatr_Kartabl.Sort = "CodeKala ASC"

                    dvElamMarjoeeSatr_Kartabl.AllowNew = False
                    dvElamMarjoeeSatr_Kartabl.AllowDelete = False
                    dvElamMarjoeeSatr_Kartabl.AllowEdit = True
                ElseIf NoeForm = 2 Then
                    If dsForm.Tables.Contains("tblMarjoeeAzMoshtarySatr") Then
                        dsForm.Tables.Remove("tblMarjoeeAzMoshtarySatr")
                    End If

                    daSQL.Fill(dsForm, "tblMarjoeeAzMoshtarySatr")

                    dvMarjoeeAzMoshtary_Satr = New DataView(dsForm.Tables("tblMarjoeeAzMoshtarySatr"))
                    dvMarjoeeAzMoshtary_Satr.Sort = "CodeKala ASC"

                    dvMarjoeeAzMoshtary_Satr.AllowNew = False
                    dvMarjoeeAzMoshtary_Satr.AllowDelete = False
                    dvMarjoeeAzMoshtary_Satr.AllowEdit = True
                End If

                cmSQL = Nothing : daSQL = Nothing
                cnSQL.Close()

                If NoeForm = 0 Then
                    SetGrid_ElamMarjoeeSatr()
                    With GridEXElamMarjoeeSatr
                        .Visible = True
                        .DataSource = Nothing
                        .DataSource = dvElamMarjoeeSatr
                    End With
                ElseIf NoeForm = 1 Then
                    SetGrid_ElamMarjoeeSatr_Kartabl()
                    With GridEXElamMarjoeeSatr_Kartabl
                        .Visible = True
                        .DataSource = Nothing
                        .DataSource = dvElamMarjoeeSatr_Kartabl
                    End With
                ElseIf NoeForm = 2 Then
                    SetGrid_MarjoeeAzMoshtarySatr()
                    With GridEXMarjoeeAzMoshtarySatr
                        .Visible = True
                        .DataSource = Nothing
                        .DataSource = dvMarjoeeAzMoshtary_Satr
                    End With
                End If

                Me.CenterToScreen()
            Catch sqlExc As SqlException
                MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> SearchSatr ")
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> SearchSatr ")
            End Try
    End Sub
    Private Sub SetGrid_ElamMarjoee()

        Try
            With GridEXElamMarjoee
                .DataSource = Nothing
                .DataSource = dsForm.Tables("tblElamMarjoee").DefaultView
                .SetDataBinding(dsForm.Tables("tblElamMarjoee").DefaultView, "")
                .RetrieveStructure()
            End With

            For i As Integer = 0 To GridEXElamMarjoee.CurrentTable.Columns.Count - 1
                GridEXElamMarjoee.CurrentTable.Columns.Item(i).Visible = False
            Next

            GridEXElamMarjoee.CurrentTable.Columns.Item("Taeed").Caption = "تاييد"
            GridEXElamMarjoee.CurrentTable.Columns.Item("Taeed").Visible = True
            GridEXElamMarjoee.CurrentTable.Columns.Item("Taeed").Width = 40
            GridEXElamMarjoee.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXElamMarjoee.CurrentTable.Columns.Item("Taeed").Position = 0
            GridEXElamMarjoee.CurrentTable.Columns.Item("Taeed").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXElamMarjoee.CurrentTable.Columns.Item("Taeed").HeaderAlignment = TextAlignment.Center

            GridEXElamMarjoee.CurrentTable.Columns.Item("ShomarehElamMarjoee").Caption = "ش مرجوعی"
            GridEXElamMarjoee.CurrentTable.Columns.Item("ShomarehElamMarjoee").Visible = True
            GridEXElamMarjoee.CurrentTable.Columns.Item("ShomarehElamMarjoee").Width = 80
            GridEXElamMarjoee.CurrentTable.Columns.Item("ShomarehElamMarjoee").EditType = EditType.NoEdit
            GridEXElamMarjoee.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXElamMarjoee.CurrentTable.Columns.Item("ShomarehElamMarjoee").Position = 1
            GridEXElamMarjoee.CurrentTable.Columns.Item("ShomarehElamMarjoee").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXElamMarjoee.CurrentTable.Columns.Item("ShomarehElamMarjoee").HeaderAlignment = TextAlignment.Center

            GridEXElamMarjoee.CurrentTable.Columns.Item("TarikhElamMarjoee").Caption = "تاریخ"
            GridEXElamMarjoee.CurrentTable.Columns.Item("TarikhElamMarjoee").Visible = True
            GridEXElamMarjoee.CurrentTable.Columns.Item("TarikhElamMarjoee").Width = 80
            GridEXElamMarjoee.CurrentTable.Columns.Item("TarikhElamMarjoee").EditType = EditType.NoEdit
            GridEXElamMarjoee.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXElamMarjoee.CurrentTable.Columns.Item("TarikhElamMarjoee").Position = 2
            GridEXElamMarjoee.CurrentTable.Columns.Item("TarikhElamMarjoee").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXElamMarjoee.CurrentTable.Columns.Item("TarikhElamMarjoee").HeaderAlignment = TextAlignment.Center

            GridEXElamMarjoee.CurrentTable.Columns.Item("CodeMoshtary").Caption = "کد مشتری"
            GridEXElamMarjoee.CurrentTable.Columns.Item("CodeMoshtary").Visible = True
            GridEXElamMarjoee.CurrentTable.Columns.Item("CodeMoshtary").Width = 80
            GridEXElamMarjoee.CurrentTable.Columns.Item("CodeMoshtary").EditType = EditType.NoEdit
            GridEXElamMarjoee.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXElamMarjoee.CurrentTable.Columns.Item("CodeMoshtary").Position = 3
            GridEXElamMarjoee.CurrentTable.Columns.Item("CodeMoshtary").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXElamMarjoee.CurrentTable.Columns.Item("CodeMoshtary").HeaderAlignment = TextAlignment.Center

            GridEXElamMarjoee.CurrentTable.Columns.Item("NameMoshtary").Caption = "نام مشتـری"
            GridEXElamMarjoee.CurrentTable.Columns.Item("NameMoshtary").Visible = True
            GridEXElamMarjoee.CurrentTable.Columns.Item("NameMoshtary").Width = 150
            GridEXElamMarjoee.CurrentTable.Columns.Item("NameMoshtary").EditType = EditType.NoEdit
            GridEXElamMarjoee.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXElamMarjoee.CurrentTable.Columns.Item("NameMoshtary").Position = 4
            GridEXElamMarjoee.CurrentTable.Columns.Item("NameMoshtary").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXElamMarjoee.CurrentTable.Columns.Item("NameMoshtary").HeaderAlignment = TextAlignment.Center

            GridEXElamMarjoee.CurrentTable.Columns.Item("NameForoshandeh").Caption = "فروشنـده"
            GridEXElamMarjoee.CurrentTable.Columns.Item("NameForoshandeh").Visible = True
            GridEXElamMarjoee.CurrentTable.Columns.Item("NameForoshandeh").Width = 150
            GridEXElamMarjoee.CurrentTable.Columns.Item("NameForoshandeh").EditType = EditType.NoEdit
            GridEXElamMarjoee.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXElamMarjoee.CurrentTable.Columns.Item("NameForoshandeh").Position = 5
            GridEXElamMarjoee.CurrentTable.Columns.Item("NameForoshandeh").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXElamMarjoee.CurrentTable.Columns.Item("NameForoshandeh").HeaderAlignment = TextAlignment.Center

            GridEXElamMarjoee.CurrentTable.Columns.Item("JamMablaghKol").Caption = "جمع کل مرجوعی"
            GridEXElamMarjoee.CurrentTable.Columns.Item("JamMablaghKol").Visible = True
            GridEXElamMarjoee.CurrentTable.Columns.Item("JamMablaghKol").Width = 120
            GridEXElamMarjoee.CurrentTable.Columns.Item("JamMablaghKol").EditType = EditType.NoEdit
            GridEXElamMarjoee.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXElamMarjoee.CurrentTable.Columns.Item("JamMablaghKol").Position = 6
            GridEXElamMarjoee.CurrentTable.Columns.Item("JamMablaghKol").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXElamMarjoee.CurrentTable.Columns.Item("JamMablaghKol").HeaderAlignment = TextAlignment.Center

            GridEXElamMarjoee.CurrentTable.Columns.Item("MablaghJayezeDasti").Caption = "مبلغ برگشتی جایزه"
            GridEXElamMarjoee.CurrentTable.Columns.Item("MablaghJayezeDasti").Visible = True
            GridEXElamMarjoee.CurrentTable.Columns.Item("MablaghJayezeDasti").Width = 120
            GridEXElamMarjoee.CurrentTable.Columns.Item("MablaghJayezeDasti").EditType = EditType.NoEdit
            GridEXElamMarjoee.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOn
            GridEXElamMarjoee.CurrentTable.Columns.Item("MablaghJayezeDasti").Position = 7
            GridEXElamMarjoee.CurrentTable.Columns.Item("MablaghJayezeDasti").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXElamMarjoee.CurrentTable.Columns.Item("MablaghJayezeDasti").HeaderAlignment = TextAlignment.Center

            GridEXElamMarjoee.CurrentTable.Columns.Item("MablaghTakhfifDasti").Caption = "مبلغ برگشتی تخفیف"
            GridEXElamMarjoee.CurrentTable.Columns.Item("MablaghTakhfifDasti").Visible = True
            GridEXElamMarjoee.CurrentTable.Columns.Item("MablaghTakhfifDasti").Width = 130
            GridEXElamMarjoee.CurrentTable.Columns.Item("MablaghTakhfifDasti").EditType = EditType.NoEdit
            GridEXElamMarjoee.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOn
            GridEXElamMarjoee.CurrentTable.Columns.Item("MablaghTakhfifDasti").Position = 8
            GridEXElamMarjoee.CurrentTable.Columns.Item("MablaghTakhfifDasti").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXElamMarjoee.CurrentTable.Columns.Item("MablaghTakhfifDasti").HeaderAlignment = TextAlignment.Center

            GridEXElamMarjoee.CurrentTable.Columns.Item("MKolMaliatAvarez").Caption = "مالیات و عوارض"
            GridEXElamMarjoee.CurrentTable.Columns.Item("MKolMaliatAvarez").Visible = True
            GridEXElamMarjoee.CurrentTable.Columns.Item("MKolMaliatAvarez").Width = 120
            GridEXElamMarjoee.CurrentTable.Columns.Item("MKolMaliatAvarez").EditType = EditType.NoEdit
            GridEXElamMarjoee.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXElamMarjoee.CurrentTable.Columns.Item("MKolMaliatAvarez").Position = 9
            GridEXElamMarjoee.CurrentTable.Columns.Item("MKolMaliatAvarez").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXElamMarjoee.CurrentTable.Columns.Item("MKolMaliatAvarez").HeaderAlignment = TextAlignment.Center

            GridEXElamMarjoee.CurrentTable.Columns.Item("JamMablagh").Caption = "مبلغ مرجوعی"
            GridEXElamMarjoee.CurrentTable.Columns.Item("JamMablagh").Visible = True
            GridEXElamMarjoee.CurrentTable.Columns.Item("JamMablagh").Width = 120
            GridEXElamMarjoee.CurrentTable.Columns.Item("JamMablagh").EditType = EditType.NoEdit
            GridEXElamMarjoee.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXElamMarjoee.CurrentTable.Columns.Item("JamMablagh").Position = 10
            GridEXElamMarjoee.CurrentTable.Columns.Item("JamMablagh").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXElamMarjoee.CurrentTable.Columns.Item("JamMablagh").HeaderAlignment = TextAlignment.Center

            GridEXElamMarjoee.CurrentTable.Columns.Item("ShomarehFaktor").Caption = "ش فاکتور"
            GridEXElamMarjoee.CurrentTable.Columns.Item("ShomarehFaktor").Visible = True
            GridEXElamMarjoee.CurrentTable.Columns.Item("ShomarehFaktor").Width = 80
            GridEXElamMarjoee.CurrentTable.Columns.Item("ShomarehFaktor").EditType = EditType.NoEdit
            GridEXElamMarjoee.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXElamMarjoee.CurrentTable.Columns.Item("ShomarehFaktor").Position = 11
            GridEXElamMarjoee.CurrentTable.Columns.Item("ShomarehFaktor").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXElamMarjoee.CurrentTable.Columns.Item("ShomarehFaktor").HeaderAlignment = TextAlignment.Center

            GridEXElamMarjoee.CurrentTable.Columns.Item("DorehFaktor").Caption = "دوره فاکتور"
            GridEXElamMarjoee.CurrentTable.Columns.Item("DorehFaktor").Visible = True
            GridEXElamMarjoee.CurrentTable.Columns.Item("DorehFaktor").Width = 80
            GridEXElamMarjoee.CurrentTable.Columns.Item("DorehFaktor").EditType = EditType.NoEdit
            GridEXElamMarjoee.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXElamMarjoee.CurrentTable.Columns.Item("DorehFaktor").Position = 12
            GridEXElamMarjoee.CurrentTable.Columns.Item("DorehFaktor").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXElamMarjoee.CurrentTable.Columns.Item("DorehFaktor").HeaderAlignment = TextAlignment.Center

            GridEXElamMarjoee.CurrentTable.Columns.Item("txtElatFaktorMarjoee").Caption = "علت مرجوعی"
            GridEXElamMarjoee.CurrentTable.Columns.Item("txtElatFaktorMarjoee").Visible = True
            GridEXElamMarjoee.CurrentTable.Columns.Item("txtElatFaktorMarjoee").Width = 150
            GridEXElamMarjoee.CurrentTable.Columns.Item("txtElatFaktorMarjoee").EditType = EditType.NoEdit
            GridEXElamMarjoee.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXElamMarjoee.CurrentTable.Columns.Item("txtElatFaktorMarjoee").Position = 13
            GridEXElamMarjoee.CurrentTable.Columns.Item("txtElatFaktorMarjoee").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXElamMarjoee.CurrentTable.Columns.Item("txtElatFaktorMarjoee").HeaderAlignment = TextAlignment.Center

            GridEXElamMarjoee.CurrentTable.Columns.Item("ccElamMarjoee").Caption = "ccElamMarjoee"
            GridEXElamMarjoee.CurrentTable.Columns.Item("ccElamMarjoee").Visible = False
            GridEXElamMarjoee.CurrentTable.Columns.Item("ccElamMarjoee").Width = 0
            GridEXElamMarjoee.CurrentTable.Columns.Item("ccElamMarjoee").EditType = EditType.NoEdit
            GridEXElamMarjoee.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXElamMarjoee.CurrentTable.Columns.Item("ccElamMarjoee").Position = 14
            GridEXElamMarjoee.CurrentTable.Columns.Item("ccElamMarjoee").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXElamMarjoee.CurrentTable.Columns.Item("ccElamMarjoee").HeaderAlignment = TextAlignment.Center

            GridEXElamMarjoee.CurrentTable.Columns.Item("ccFaktorTitr").Caption = "ccFaktorTitr"
            GridEXElamMarjoee.CurrentTable.Columns.Item("ccFaktorTitr").Visible = False
            GridEXElamMarjoee.CurrentTable.Columns.Item("ccFaktorTitr").Width = 0
            GridEXElamMarjoee.CurrentTable.Columns.Item("ccFaktorTitr").EditType = EditType.NoEdit
            GridEXElamMarjoee.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXElamMarjoee.CurrentTable.Columns.Item("ccFaktorTitr").Position = 15
            GridEXElamMarjoee.CurrentTable.Columns.Item("ccFaktorTitr").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXElamMarjoee.CurrentTable.Columns.Item("ccFaktorTitr").HeaderAlignment = TextAlignment.Center

            GridEXElamMarjoee.CurrentTable.Columns.Item("IsFaktorMarjoee").Caption = "IsFaktorMarjoee"
            GridEXElamMarjoee.CurrentTable.Columns.Item("IsFaktorMarjoee").Visible = False
            GridEXElamMarjoee.CurrentTable.Columns.Item("IsFaktorMarjoee").Width = 0
            GridEXElamMarjoee.CurrentTable.Columns.Item("IsFaktorMarjoee").EditType = EditType.NoEdit
            GridEXElamMarjoee.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXElamMarjoee.CurrentTable.Columns.Item("IsFaktorMarjoee").Position = 16
            GridEXElamMarjoee.CurrentTable.Columns.Item("IsFaktorMarjoee").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXElamMarjoee.CurrentTable.Columns.Item("IsFaktorMarjoee").HeaderAlignment = TextAlignment.Center

            GridEXElamMarjoee.CurrentTable.Columns.Item("sElat").Caption = "sElat"
            GridEXElamMarjoee.CurrentTable.Columns.Item("sElat").Visible = False
            GridEXElamMarjoee.CurrentTable.Columns.Item("sElat").Width = 0
            GridEXElamMarjoee.CurrentTable.Columns.Item("sElat").EditType = EditType.NoEdit
            GridEXElamMarjoee.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXElamMarjoee.CurrentTable.Columns.Item("sElat").Position = 17
            GridEXElamMarjoee.CurrentTable.Columns.Item("sElat").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXElamMarjoee.CurrentTable.Columns.Item("sElat").HeaderAlignment = TextAlignment.Center

            For i As Integer = 0 To GridEXElamMarjoee.RootTable.Columns.Count - 1
                If GridEXElamMarjoee.RootTable.Columns(i).Type.IsValueType Then
                    GridEXElamMarjoee.RootTable.Columns(i).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                    GridEXElamMarjoee.RootTable.Columns(i).FormatMode = Janus.Windows.GridEX.FormatMode.UseIFormattable
                    GridEXElamMarjoee.RootTable.Columns(i).FormatString = "G"
                    GridEXElamMarjoee.RootTable.Columns(i).TotalFormatMode = Janus.Windows.GridEX.FormatMode.UseIFormattable
                    GridEXElamMarjoee.RootTable.Columns(i).TotalFormatString = "G"
                End If
            Next

            Me.CenterToScreen()
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> SetGrid_ElamMarjoee ")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> SetGrid_ElamMarjoee ")
        End Try
    End Sub
    Private Sub SetGrid_ElamMarjoeeSatr()
        If dvElamMarjoee.Count = 0 Then
            Exit Sub
        End If

        Try
            With GridEXElamMarjoeeSatr
                .DataSource = Nothing
                .DataSource = dsForm.Tables("tblElamMarjoeeSatr").DefaultView
                .SetDataBinding(dsForm.Tables("tblElamMarjoeeSatr").DefaultView, "")
                .RetrieveStructure()
            End With

            For i As Integer = 0 To GridEXElamMarjoeeSatr.CurrentTable.Columns.Count - 1
                GridEXElamMarjoeeSatr.CurrentTable.Columns.Item(i).Visible = False
            Next

            GridEXElamMarjoeeSatr.CurrentTable.Columns.Item("CodeKala").Caption = "کـد کالا"
            GridEXElamMarjoeeSatr.CurrentTable.Columns.Item("CodeKala").Visible = True
            GridEXElamMarjoeeSatr.CurrentTable.Columns.Item("CodeKala").Width = 80
            GridEXElamMarjoeeSatr.CurrentTable.Columns.Item("CodeKala").EditType = EditType.NoEdit
            GridEXElamMarjoeeSatr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXElamMarjoeeSatr.CurrentTable.Columns.Item("CodeKala").Position = 0
            GridEXElamMarjoeeSatr.CurrentTable.Columns.Item("CodeKala").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXElamMarjoeeSatr.CurrentTable.Columns.Item("CodeKala").HeaderAlignment = TextAlignment.Center

            GridEXElamMarjoeeSatr.CurrentTable.Columns.Item("NameKala").Caption = "نام کالا"
            GridEXElamMarjoeeSatr.CurrentTable.Columns.Item("NameKala").Visible = True
            GridEXElamMarjoeeSatr.CurrentTable.Columns.Item("NameKala").Width = 300
            GridEXElamMarjoeeSatr.CurrentTable.Columns.Item("NameKala").EditType = EditType.NoEdit
            GridEXElamMarjoeeSatr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXElamMarjoeeSatr.CurrentTable.Columns.Item("NameKala").Position = 1
            GridEXElamMarjoeeSatr.CurrentTable.Columns.Item("NameKala").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXElamMarjoeeSatr.CurrentTable.Columns.Item("NameKala").HeaderAlignment = TextAlignment.Center

            GridEXElamMarjoeeSatr.CurrentTable.Columns.Item("Tedad").Caption = "تعــداد"
            GridEXElamMarjoeeSatr.CurrentTable.Columns.Item("Tedad").Visible = True
            GridEXElamMarjoeeSatr.CurrentTable.Columns.Item("Tedad").Width = 80
            GridEXElamMarjoeeSatr.CurrentTable.Columns.Item("Tedad").EditType = EditType.NoEdit
            GridEXElamMarjoeeSatr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXElamMarjoeeSatr.CurrentTable.Columns.Item("Tedad").Position = 2
            GridEXElamMarjoeeSatr.CurrentTable.Columns.Item("Tedad").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXElamMarjoeeSatr.CurrentTable.Columns.Item("Tedad").HeaderAlignment = TextAlignment.Center

            GridEXElamMarjoeeSatr.CurrentTable.Columns.Item("Fee").Caption = "قیمت"
            GridEXElamMarjoeeSatr.CurrentTable.Columns.Item("Fee").Visible = True
            GridEXElamMarjoeeSatr.CurrentTable.Columns.Item("Fee").Width = 100
            GridEXElamMarjoeeSatr.CurrentTable.Columns.Item("Fee").EditType = EditType.NoEdit
            GridEXElamMarjoeeSatr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXElamMarjoeeSatr.CurrentTable.Columns.Item("Fee").Position = 3
            GridEXElamMarjoeeSatr.CurrentTable.Columns.Item("Fee").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXElamMarjoeeSatr.CurrentTable.Columns.Item("Fee").HeaderAlignment = TextAlignment.Center

            GridEXElamMarjoeeSatr.CurrentTable.Columns.Item("MKOL").Caption = "قیمت کل"
            GridEXElamMarjoeeSatr.CurrentTable.Columns.Item("MKOL").Visible = True
            GridEXElamMarjoeeSatr.CurrentTable.Columns.Item("MKOL").Width = 120
            GridEXElamMarjoeeSatr.CurrentTable.Columns.Item("MKOL").EditType = EditType.NoEdit
            GridEXElamMarjoeeSatr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXElamMarjoeeSatr.CurrentTable.Columns.Item("MKOL").Position = 4
            GridEXElamMarjoeeSatr.CurrentTable.Columns.Item("MKOL").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXElamMarjoeeSatr.CurrentTable.Columns.Item("MKOL").HeaderAlignment = TextAlignment.Center

            GridEXElamMarjoeeSatr.CurrentTable.Columns.Item("txtNoeMarjoee").Caption = "نـوع مرجوعی"
            GridEXElamMarjoeeSatr.CurrentTable.Columns.Item("txtNoeMarjoee").Visible = True
            GridEXElamMarjoeeSatr.CurrentTable.Columns.Item("txtNoeMarjoee").Width = 100
            GridEXElamMarjoeeSatr.CurrentTable.Columns.Item("txtNoeMarjoee").EditType = EditType.NoEdit
            GridEXElamMarjoeeSatr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXElamMarjoeeSatr.CurrentTable.Columns.Item("txtNoeMarjoee").Position = 5
            GridEXElamMarjoeeSatr.CurrentTable.Columns.Item("txtNoeMarjoee").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXElamMarjoeeSatr.CurrentTable.Columns.Item("txtNoeMarjoee").HeaderAlignment = TextAlignment.Center

            GridEXElamMarjoeeSatr.CurrentTable.Columns.Item("txtElatMarjoee").Caption = "علت مرجوعی"
            GridEXElamMarjoeeSatr.CurrentTable.Columns.Item("txtElatMarjoee").Visible = True
            GridEXElamMarjoeeSatr.CurrentTable.Columns.Item("txtElatMarjoee").Width = 300
            GridEXElamMarjoeeSatr.CurrentTable.Columns.Item("txtElatMarjoee").EditType = EditType.NoEdit
            GridEXElamMarjoeeSatr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOn
            GridEXElamMarjoeeSatr.CurrentTable.Columns.Item("txtElatMarjoee").Position = 6
            GridEXElamMarjoeeSatr.CurrentTable.Columns.Item("txtElatMarjoee").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXElamMarjoeeSatr.CurrentTable.Columns.Item("txtElatMarjoee").HeaderAlignment = TextAlignment.Center

            GridEXElamMarjoeeSatr.CurrentTable.Columns.Item("ccMarjoeeSatr").Caption = "ccMarjoeeSatr"
            GridEXElamMarjoeeSatr.CurrentTable.Columns.Item("ccMarjoeeSatr").Visible = False
            GridEXElamMarjoeeSatr.CurrentTable.Columns.Item("ccMarjoeeSatr").Width = 0
            GridEXElamMarjoeeSatr.CurrentTable.Columns.Item("ccMarjoeeSatr").EditType = EditType.NoEdit
            GridEXElamMarjoeeSatr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOn
            GridEXElamMarjoeeSatr.CurrentTable.Columns.Item("ccMarjoeeSatr").Position = 7
            GridEXElamMarjoeeSatr.CurrentTable.Columns.Item("ccMarjoeeSatr").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXElamMarjoeeSatr.CurrentTable.Columns.Item("ccMarjoeeSatr").HeaderAlignment = TextAlignment.Center

            GridEXElamMarjoeeSatr.CurrentTable.Columns.Item("sElatMarjoee").Caption = "sElatMarjoee"
            GridEXElamMarjoeeSatr.CurrentTable.Columns.Item("sElatMarjoee").Visible = False
            GridEXElamMarjoeeSatr.CurrentTable.Columns.Item("sElatMarjoee").Width = 0
            GridEXElamMarjoeeSatr.CurrentTable.Columns.Item("sElatMarjoee").EditType = EditType.NoEdit
            GridEXElamMarjoeeSatr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOn
            GridEXElamMarjoeeSatr.CurrentTable.Columns.Item("sElatMarjoee").Position = 8
            GridEXElamMarjoeeSatr.CurrentTable.Columns.Item("sElatMarjoee").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXElamMarjoeeSatr.CurrentTable.Columns.Item("sElatMarjoee").HeaderAlignment = TextAlignment.Center

            GridEXElamMarjoeeSatr.CurrentTable.Columns.Item("IsJayezehMarjoee").Caption = "IsJayezehMarjoee"
            GridEXElamMarjoeeSatr.CurrentTable.Columns.Item("IsJayezehMarjoee").Visible = False
            GridEXElamMarjoeeSatr.CurrentTable.Columns.Item("IsJayezehMarjoee").Width = 0
            GridEXElamMarjoeeSatr.CurrentTable.Columns.Item("IsJayezehMarjoee").EditType = EditType.NoEdit
            GridEXElamMarjoeeSatr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOn
            GridEXElamMarjoeeSatr.CurrentTable.Columns.Item("IsJayezehMarjoee").Position = 9
            GridEXElamMarjoeeSatr.CurrentTable.Columns.Item("IsJayezehMarjoee").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXElamMarjoeeSatr.CurrentTable.Columns.Item("IsJayezehMarjoee").HeaderAlignment = TextAlignment.Center

            GridEXElamMarjoeeSatr.CurrentTable.Columns.Item("NoeMarjoee").Caption = "NoeMarjoee"
            GridEXElamMarjoeeSatr.CurrentTable.Columns.Item("NoeMarjoee").Visible = False
            GridEXElamMarjoeeSatr.CurrentTable.Columns.Item("NoeMarjoee").Width = 0
            GridEXElamMarjoeeSatr.CurrentTable.Columns.Item("NoeMarjoee").EditType = EditType.NoEdit
            GridEXElamMarjoeeSatr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOn
            GridEXElamMarjoeeSatr.CurrentTable.Columns.Item("NoeMarjoee").Position = 10
            GridEXElamMarjoeeSatr.CurrentTable.Columns.Item("NoeMarjoee").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXElamMarjoeeSatr.CurrentTable.Columns.Item("NoeMarjoee").HeaderAlignment = TextAlignment.Center

            For i As Integer = 0 To GridEXElamMarjoeeSatr.RootTable.Columns.Count - 1
                If GridEXElamMarjoeeSatr.RootTable.Columns(i).Type.IsValueType Then
                    GridEXElamMarjoeeSatr.RootTable.Columns(i).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                    GridEXElamMarjoeeSatr.RootTable.Columns(i).FormatMode = Janus.Windows.GridEX.FormatMode.UseIFormattable
                    GridEXElamMarjoeeSatr.RootTable.Columns(i).FormatString = "G"
                    GridEXElamMarjoeeSatr.RootTable.Columns(i).TotalFormatMode = Janus.Windows.GridEX.FormatMode.UseIFormattable
                    GridEXElamMarjoeeSatr.RootTable.Columns(i).TotalFormatString = "G"
                End If
            Next

            Me.CenterToScreen()
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> SetGrid_ElamMarjoeeSatr ")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> SetGrid_ElamMarjoeeSatr ")
        End Try
    End Sub
    Private Sub SetGrid_ElamMarjoee_Kartabl()

        Try
            With GridEXElamMarjoee_Kartabl
                .DataSource = Nothing
                .DataSource = dsForm.Tables("tblElamMarjoee_Kartabl").DefaultView
                .SetDataBinding(dsForm.Tables("tblElamMarjoee_Kartabl").DefaultView, "")
                .RetrieveStructure()
            End With

            For i As Integer = 0 To GridEXElamMarjoee_Kartabl.CurrentTable.Columns.Count - 1
                GridEXElamMarjoee_Kartabl.CurrentTable.Columns.Item(i).Visible = False
            Next

            GridEXElamMarjoee_Kartabl.CurrentTable.Columns.Item("Taeed").Caption = "تاييد"
            GridEXElamMarjoee_Kartabl.CurrentTable.Columns.Item("Taeed").Visible = True
            GridEXElamMarjoee_Kartabl.CurrentTable.Columns.Item("Taeed").Width = 40
            GridEXElamMarjoee_Kartabl.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXElamMarjoee_Kartabl.CurrentTable.Columns.Item("Taeed").Position = 0
            GridEXElamMarjoee_Kartabl.CurrentTable.Columns.Item("Taeed").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXElamMarjoee_Kartabl.CurrentTable.Columns.Item("Taeed").HeaderAlignment = TextAlignment.Center

            GridEXElamMarjoee_Kartabl.CurrentTable.Columns.Item("ShomarehElamMarjoee").Caption = "ش مرجوعی"
            GridEXElamMarjoee_Kartabl.CurrentTable.Columns.Item("ShomarehElamMarjoee").Visible = True
            GridEXElamMarjoee_Kartabl.CurrentTable.Columns.Item("ShomarehElamMarjoee").Width = 80
            GridEXElamMarjoee_Kartabl.CurrentTable.Columns.Item("ShomarehElamMarjoee").EditType = EditType.NoEdit
            GridEXElamMarjoee_Kartabl.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXElamMarjoee_Kartabl.CurrentTable.Columns.Item("ShomarehElamMarjoee").Position = 1
            GridEXElamMarjoee_Kartabl.CurrentTable.Columns.Item("ShomarehElamMarjoee").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXElamMarjoee_Kartabl.CurrentTable.Columns.Item("ShomarehElamMarjoee").HeaderAlignment = TextAlignment.Center

            GridEXElamMarjoee_Kartabl.CurrentTable.Columns.Item("TarikhElamMarjoee").Caption = "تاریخ"
            GridEXElamMarjoee_Kartabl.CurrentTable.Columns.Item("TarikhElamMarjoee").Visible = True
            GridEXElamMarjoee_Kartabl.CurrentTable.Columns.Item("TarikhElamMarjoee").Width = 80
            GridEXElamMarjoee_Kartabl.CurrentTable.Columns.Item("TarikhElamMarjoee").EditType = EditType.NoEdit
            GridEXElamMarjoee_Kartabl.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXElamMarjoee_Kartabl.CurrentTable.Columns.Item("TarikhElamMarjoee").Position = 2
            GridEXElamMarjoee_Kartabl.CurrentTable.Columns.Item("TarikhElamMarjoee").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXElamMarjoee_Kartabl.CurrentTable.Columns.Item("TarikhElamMarjoee").HeaderAlignment = TextAlignment.Center

            GridEXElamMarjoee_Kartabl.CurrentTable.Columns.Item("CodeMoshtary").Caption = "کد مشتری"
            GridEXElamMarjoee_Kartabl.CurrentTable.Columns.Item("CodeMoshtary").Visible = True
            GridEXElamMarjoee_Kartabl.CurrentTable.Columns.Item("CodeMoshtary").Width = 80
            GridEXElamMarjoee_Kartabl.CurrentTable.Columns.Item("CodeMoshtary").EditType = EditType.NoEdit
            GridEXElamMarjoee_Kartabl.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXElamMarjoee_Kartabl.CurrentTable.Columns.Item("CodeMoshtary").Position = 3
            GridEXElamMarjoee_Kartabl.CurrentTable.Columns.Item("CodeMoshtary").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXElamMarjoee_Kartabl.CurrentTable.Columns.Item("CodeMoshtary").HeaderAlignment = TextAlignment.Center

            GridEXElamMarjoee_Kartabl.CurrentTable.Columns.Item("NameMoshtary").Caption = "نام مشتـری"
            GridEXElamMarjoee_Kartabl.CurrentTable.Columns.Item("NameMoshtary").Visible = True
            GridEXElamMarjoee_Kartabl.CurrentTable.Columns.Item("NameMoshtary").Width = 150
            GridEXElamMarjoee_Kartabl.CurrentTable.Columns.Item("NameMoshtary").EditType = EditType.NoEdit
            GridEXElamMarjoee_Kartabl.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXElamMarjoee_Kartabl.CurrentTable.Columns.Item("NameMoshtary").Position = 4
            GridEXElamMarjoee_Kartabl.CurrentTable.Columns.Item("NameMoshtary").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXElamMarjoee_Kartabl.CurrentTable.Columns.Item("NameMoshtary").HeaderAlignment = TextAlignment.Center

            GridEXElamMarjoee_Kartabl.CurrentTable.Columns.Item("NameForoshandeh").Caption = "فروشنـده"
            GridEXElamMarjoee_Kartabl.CurrentTable.Columns.Item("NameForoshandeh").Visible = True
            GridEXElamMarjoee_Kartabl.CurrentTable.Columns.Item("NameForoshandeh").Width = 150
            GridEXElamMarjoee_Kartabl.CurrentTable.Columns.Item("NameForoshandeh").EditType = EditType.NoEdit
            GridEXElamMarjoee_Kartabl.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXElamMarjoee_Kartabl.CurrentTable.Columns.Item("NameForoshandeh").Position = 5
            GridEXElamMarjoee_Kartabl.CurrentTable.Columns.Item("NameForoshandeh").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXElamMarjoee_Kartabl.CurrentTable.Columns.Item("NameForoshandeh").HeaderAlignment = TextAlignment.Center

            GridEXElamMarjoee_Kartabl.CurrentTable.Columns.Item("JamMablaghKol").Caption = "جمع کل مرجوعی"
            GridEXElamMarjoee_Kartabl.CurrentTable.Columns.Item("JamMablaghKol").Visible = True
            GridEXElamMarjoee_Kartabl.CurrentTable.Columns.Item("JamMablaghKol").Width = 120
            GridEXElamMarjoee_Kartabl.CurrentTable.Columns.Item("JamMablaghKol").EditType = EditType.NoEdit
            GridEXElamMarjoee_Kartabl.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXElamMarjoee_Kartabl.CurrentTable.Columns.Item("JamMablaghKol").Position = 6
            GridEXElamMarjoee_Kartabl.CurrentTable.Columns.Item("JamMablaghKol").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXElamMarjoee_Kartabl.CurrentTable.Columns.Item("JamMablaghKol").HeaderAlignment = TextAlignment.Center

            GridEXElamMarjoee_Kartabl.CurrentTable.Columns.Item("MablaghJayezeDasti").Caption = "مبلغ برگشتی جایزه"
            GridEXElamMarjoee_Kartabl.CurrentTable.Columns.Item("MablaghJayezeDasti").Visible = True
            GridEXElamMarjoee_Kartabl.CurrentTable.Columns.Item("MablaghJayezeDasti").Width = 120
            GridEXElamMarjoee_Kartabl.CurrentTable.Columns.Item("MablaghJayezeDasti").EditType = EditType.NoEdit
            GridEXElamMarjoee_Kartabl.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOn
            GridEXElamMarjoee_Kartabl.CurrentTable.Columns.Item("MablaghJayezeDasti").Position = 7
            GridEXElamMarjoee_Kartabl.CurrentTable.Columns.Item("MablaghJayezeDasti").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXElamMarjoee_Kartabl.CurrentTable.Columns.Item("MablaghJayezeDasti").HeaderAlignment = TextAlignment.Center

            GridEXElamMarjoee_Kartabl.CurrentTable.Columns.Item("MablaghTakhfifDasti").Caption = "مبلغ برگشتی تخفیف"
            GridEXElamMarjoee_Kartabl.CurrentTable.Columns.Item("MablaghTakhfifDasti").Visible = True
            GridEXElamMarjoee_Kartabl.CurrentTable.Columns.Item("MablaghTakhfifDasti").Width = 130
            GridEXElamMarjoee_Kartabl.CurrentTable.Columns.Item("MablaghTakhfifDasti").EditType = EditType.NoEdit
            GridEXElamMarjoee_Kartabl.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOn
            GridEXElamMarjoee_Kartabl.CurrentTable.Columns.Item("MablaghTakhfifDasti").Position = 8
            GridEXElamMarjoee_Kartabl.CurrentTable.Columns.Item("MablaghTakhfifDasti").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXElamMarjoee_Kartabl.CurrentTable.Columns.Item("MablaghTakhfifDasti").HeaderAlignment = TextAlignment.Center

            GridEXElamMarjoee_Kartabl.CurrentTable.Columns.Item("MKolMaliatAvarez").Caption = "مالیات و عوارض"
            GridEXElamMarjoee_Kartabl.CurrentTable.Columns.Item("MKolMaliatAvarez").Visible = True
            GridEXElamMarjoee_Kartabl.CurrentTable.Columns.Item("MKolMaliatAvarez").Width = 120
            GridEXElamMarjoee_Kartabl.CurrentTable.Columns.Item("MKolMaliatAvarez").EditType = EditType.NoEdit
            GridEXElamMarjoee_Kartabl.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXElamMarjoee_Kartabl.CurrentTable.Columns.Item("MKolMaliatAvarez").Position = 9
            GridEXElamMarjoee_Kartabl.CurrentTable.Columns.Item("MKolMaliatAvarez").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXElamMarjoee_Kartabl.CurrentTable.Columns.Item("MKolMaliatAvarez").HeaderAlignment = TextAlignment.Center

            GridEXElamMarjoee_Kartabl.CurrentTable.Columns.Item("JamMablagh").Caption = "مبلغ مرجوعی"
            GridEXElamMarjoee_Kartabl.CurrentTable.Columns.Item("JamMablagh").Visible = True
            GridEXElamMarjoee_Kartabl.CurrentTable.Columns.Item("JamMablagh").Width = 120
            GridEXElamMarjoee_Kartabl.CurrentTable.Columns.Item("JamMablagh").EditType = EditType.NoEdit
            GridEXElamMarjoee_Kartabl.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXElamMarjoee_Kartabl.CurrentTable.Columns.Item("JamMablagh").Position = 10
            GridEXElamMarjoee_Kartabl.CurrentTable.Columns.Item("JamMablagh").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXElamMarjoee_Kartabl.CurrentTable.Columns.Item("JamMablagh").HeaderAlignment = TextAlignment.Center

            GridEXElamMarjoee_Kartabl.CurrentTable.Columns.Item("ShomarehFaktor").Caption = "ش فاکتور"
            GridEXElamMarjoee_Kartabl.CurrentTable.Columns.Item("ShomarehFaktor").Visible = True
            GridEXElamMarjoee_Kartabl.CurrentTable.Columns.Item("ShomarehFaktor").Width = 80
            GridEXElamMarjoee_Kartabl.CurrentTable.Columns.Item("ShomarehFaktor").EditType = EditType.NoEdit
            GridEXElamMarjoee_Kartabl.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXElamMarjoee_Kartabl.CurrentTable.Columns.Item("ShomarehFaktor").Position = 11
            GridEXElamMarjoee_Kartabl.CurrentTable.Columns.Item("ShomarehFaktor").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXElamMarjoee_Kartabl.CurrentTable.Columns.Item("ShomarehFaktor").HeaderAlignment = TextAlignment.Center

            GridEXElamMarjoee_Kartabl.CurrentTable.Columns.Item("DorehFaktor").Caption = "دوره فاکتور"
            GridEXElamMarjoee_Kartabl.CurrentTable.Columns.Item("DorehFaktor").Visible = False
            GridEXElamMarjoee_Kartabl.CurrentTable.Columns.Item("DorehFaktor").Width = 80
            GridEXElamMarjoee_Kartabl.CurrentTable.Columns.Item("DorehFaktor").EditType = EditType.NoEdit
            GridEXElamMarjoee_Kartabl.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXElamMarjoee_Kartabl.CurrentTable.Columns.Item("DorehFaktor").Position = 12
            GridEXElamMarjoee_Kartabl.CurrentTable.Columns.Item("DorehFaktor").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXElamMarjoee_Kartabl.CurrentTable.Columns.Item("DorehFaktor").HeaderAlignment = TextAlignment.Center

            GridEXElamMarjoee_Kartabl.CurrentTable.Columns.Item("ccElamMarjoee").Caption = "ccElamMarjoee"
            GridEXElamMarjoee_Kartabl.CurrentTable.Columns.Item("ccElamMarjoee").Visible = False
            GridEXElamMarjoee_Kartabl.CurrentTable.Columns.Item("ccElamMarjoee").Width = 0
            GridEXElamMarjoee_Kartabl.CurrentTable.Columns.Item("ccElamMarjoee").EditType = EditType.NoEdit
            GridEXElamMarjoee_Kartabl.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXElamMarjoee_Kartabl.CurrentTable.Columns.Item("ccElamMarjoee").Position = 13
            GridEXElamMarjoee_Kartabl.CurrentTable.Columns.Item("ccElamMarjoee").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXElamMarjoee_Kartabl.CurrentTable.Columns.Item("ccElamMarjoee").HeaderAlignment = TextAlignment.Center

            GridEXElamMarjoee_Kartabl.CurrentTable.Columns.Item("ccFaktorTitr").Caption = "ccFaktorTitr"
            GridEXElamMarjoee_Kartabl.CurrentTable.Columns.Item("ccFaktorTitr").Visible = False
            GridEXElamMarjoee_Kartabl.CurrentTable.Columns.Item("ccFaktorTitr").Width = 0
            GridEXElamMarjoee_Kartabl.CurrentTable.Columns.Item("ccFaktorTitr").EditType = EditType.NoEdit
            GridEXElamMarjoee_Kartabl.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXElamMarjoee_Kartabl.CurrentTable.Columns.Item("ccFaktorTitr").Position = 14
            GridEXElamMarjoee_Kartabl.CurrentTable.Columns.Item("ccFaktorTitr").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXElamMarjoee_Kartabl.CurrentTable.Columns.Item("ccFaktorTitr").HeaderAlignment = TextAlignment.Center

            For i As Integer = 0 To GridEXElamMarjoee_Kartabl.RootTable.Columns.Count - 1
                If GridEXElamMarjoee_Kartabl.RootTable.Columns(i).Type.IsValueType Then
                    GridEXElamMarjoee_Kartabl.RootTable.Columns(i).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                    GridEXElamMarjoee_Kartabl.RootTable.Columns(i).FormatMode = Janus.Windows.GridEX.FormatMode.UseIFormattable
                    GridEXElamMarjoee_Kartabl.RootTable.Columns(i).FormatString = "G"
                    GridEXElamMarjoee_Kartabl.RootTable.Columns(i).TotalFormatMode = Janus.Windows.GridEX.FormatMode.UseIFormattable
                    GridEXElamMarjoee_Kartabl.RootTable.Columns(i).TotalFormatString = "G"
                End If
            Next

            Me.CenterToScreen()
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> SetGrid_ElamMarjoee_Kartabl ")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> SetGrid_ElamMarjoee_Kartabl ")
        End Try
    End Sub
    Private Sub SetGrid_ElamMarjoeeSatr_Kartabl()
        If dvElamMarjoee_Kartabl.Count = 0 Then
            Exit Sub
        End If

        Try
            With GridEXElamMarjoeeSatr_Kartabl
                .DataSource = Nothing
                .DataSource = dsForm.Tables("tblElamMarjoeeSatr_Kartabl").DefaultView
                .SetDataBinding(dsForm.Tables("tblElamMarjoeeSatr_Kartabl").DefaultView, "")
                .RetrieveStructure()
            End With

            For i As Integer = 0 To GridEXElamMarjoeeSatr_Kartabl.CurrentTable.Columns.Count - 1
                GridEXElamMarjoeeSatr_Kartabl.CurrentTable.Columns.Item(i).Visible = False
            Next

            GridEXElamMarjoeeSatr_Kartabl.CurrentTable.Columns.Item("CodeKala").Caption = "کـد کالا"
            GridEXElamMarjoeeSatr_Kartabl.CurrentTable.Columns.Item("CodeKala").Visible = True
            GridEXElamMarjoeeSatr_Kartabl.CurrentTable.Columns.Item("CodeKala").Width = 80
            GridEXElamMarjoeeSatr_Kartabl.CurrentTable.Columns.Item("CodeKala").EditType = EditType.NoEdit
            GridEXElamMarjoeeSatr_Kartabl.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXElamMarjoeeSatr_Kartabl.CurrentTable.Columns.Item("CodeKala").Position = 0
            GridEXElamMarjoeeSatr_Kartabl.CurrentTable.Columns.Item("CodeKala").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXElamMarjoeeSatr_Kartabl.CurrentTable.Columns.Item("CodeKala").HeaderAlignment = TextAlignment.Center

            GridEXElamMarjoeeSatr_Kartabl.CurrentTable.Columns.Item("NameKala").Caption = "نام کالا"
            GridEXElamMarjoeeSatr_Kartabl.CurrentTable.Columns.Item("NameKala").Visible = True
            GridEXElamMarjoeeSatr_Kartabl.CurrentTable.Columns.Item("NameKala").Width = 300
            GridEXElamMarjoeeSatr_Kartabl.CurrentTable.Columns.Item("NameKala").EditType = EditType.NoEdit
            GridEXElamMarjoeeSatr_Kartabl.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXElamMarjoeeSatr_Kartabl.CurrentTable.Columns.Item("NameKala").Position = 1
            GridEXElamMarjoeeSatr_Kartabl.CurrentTable.Columns.Item("NameKala").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXElamMarjoeeSatr_Kartabl.CurrentTable.Columns.Item("NameKala").HeaderAlignment = TextAlignment.Center

            GridEXElamMarjoeeSatr_Kartabl.CurrentTable.Columns.Item("Tedad").Caption = "تعــداد"
            GridEXElamMarjoeeSatr_Kartabl.CurrentTable.Columns.Item("Tedad").Visible = True
            GridEXElamMarjoeeSatr_Kartabl.CurrentTable.Columns.Item("Tedad").Width = 80
            GridEXElamMarjoeeSatr_Kartabl.CurrentTable.Columns.Item("Tedad").EditType = EditType.NoEdit
            GridEXElamMarjoeeSatr_Kartabl.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXElamMarjoeeSatr_Kartabl.CurrentTable.Columns.Item("Tedad").Position = 2
            GridEXElamMarjoeeSatr_Kartabl.CurrentTable.Columns.Item("Tedad").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXElamMarjoeeSatr_Kartabl.CurrentTable.Columns.Item("Tedad").HeaderAlignment = TextAlignment.Center

            GridEXElamMarjoeeSatr_Kartabl.CurrentTable.Columns.Item("Fee").Caption = "قیمت"
            GridEXElamMarjoeeSatr_Kartabl.CurrentTable.Columns.Item("Fee").Visible = True
            GridEXElamMarjoeeSatr_Kartabl.CurrentTable.Columns.Item("Fee").Width = 100
            GridEXElamMarjoeeSatr_Kartabl.CurrentTable.Columns.Item("Fee").EditType = EditType.NoEdit
            GridEXElamMarjoeeSatr_Kartabl.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXElamMarjoeeSatr_Kartabl.CurrentTable.Columns.Item("Fee").Position = 3
            GridEXElamMarjoeeSatr_Kartabl.CurrentTable.Columns.Item("Fee").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXElamMarjoeeSatr_Kartabl.CurrentTable.Columns.Item("Fee").HeaderAlignment = TextAlignment.Center

            GridEXElamMarjoeeSatr_Kartabl.CurrentTable.Columns.Item("MKOL").Caption = "قیمت کل"
            GridEXElamMarjoeeSatr_Kartabl.CurrentTable.Columns.Item("MKOL").Visible = True
            GridEXElamMarjoeeSatr_Kartabl.CurrentTable.Columns.Item("MKOL").Width = 120
            GridEXElamMarjoeeSatr_Kartabl.CurrentTable.Columns.Item("MKOL").EditType = EditType.NoEdit
            GridEXElamMarjoeeSatr_Kartabl.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXElamMarjoeeSatr_Kartabl.CurrentTable.Columns.Item("MKOL").Position = 4
            GridEXElamMarjoeeSatr_Kartabl.CurrentTable.Columns.Item("MKOL").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXElamMarjoeeSatr_Kartabl.CurrentTable.Columns.Item("MKOL").HeaderAlignment = TextAlignment.Center

            GridEXElamMarjoeeSatr_Kartabl.CurrentTable.Columns.Item("txtNoeMarjoee").Caption = "نـوع مرجوعی"
            GridEXElamMarjoeeSatr_Kartabl.CurrentTable.Columns.Item("txtNoeMarjoee").Visible = True
            GridEXElamMarjoeeSatr_Kartabl.CurrentTable.Columns.Item("txtNoeMarjoee").Width = 100
            GridEXElamMarjoeeSatr_Kartabl.CurrentTable.Columns.Item("txtNoeMarjoee").EditType = EditType.NoEdit
            GridEXElamMarjoeeSatr_Kartabl.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXElamMarjoeeSatr_Kartabl.CurrentTable.Columns.Item("txtNoeMarjoee").Position = 5
            GridEXElamMarjoeeSatr_Kartabl.CurrentTable.Columns.Item("txtNoeMarjoee").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXElamMarjoeeSatr_Kartabl.CurrentTable.Columns.Item("txtNoeMarjoee").HeaderAlignment = TextAlignment.Center

            GridEXElamMarjoeeSatr_Kartabl.CurrentTable.Columns.Item("txtElatMarjoee").Caption = "علت مرجوعی"
            GridEXElamMarjoeeSatr_Kartabl.CurrentTable.Columns.Item("txtElatMarjoee").Visible = True
            GridEXElamMarjoeeSatr_Kartabl.CurrentTable.Columns.Item("txtElatMarjoee").Width = 300
            GridEXElamMarjoeeSatr_Kartabl.CurrentTable.Columns.Item("txtElatMarjoee").EditType = EditType.NoEdit
            GridEXElamMarjoeeSatr_Kartabl.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOn
            GridEXElamMarjoeeSatr_Kartabl.CurrentTable.Columns.Item("txtElatMarjoee").Position = 6
            GridEXElamMarjoeeSatr_Kartabl.CurrentTable.Columns.Item("txtElatMarjoee").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXElamMarjoeeSatr_Kartabl.CurrentTable.Columns.Item("txtElatMarjoee").HeaderAlignment = TextAlignment.Center

            GridEXElamMarjoeeSatr_Kartabl.CurrentTable.Columns.Item("ccMarjoeeSatr").Caption = "ccMarjoeeSatr"
            GridEXElamMarjoeeSatr_Kartabl.CurrentTable.Columns.Item("ccMarjoeeSatr").Visible = False
            GridEXElamMarjoeeSatr_Kartabl.CurrentTable.Columns.Item("ccMarjoeeSatr").Width = 0
            GridEXElamMarjoeeSatr_Kartabl.CurrentTable.Columns.Item("ccMarjoeeSatr").EditType = EditType.NoEdit
            GridEXElamMarjoeeSatr_Kartabl.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOn
            GridEXElamMarjoeeSatr_Kartabl.CurrentTable.Columns.Item("ccMarjoeeSatr").Position = 7
            GridEXElamMarjoeeSatr_Kartabl.CurrentTable.Columns.Item("ccMarjoeeSatr").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXElamMarjoeeSatr_Kartabl.CurrentTable.Columns.Item("ccMarjoeeSatr").HeaderAlignment = TextAlignment.Center

            For i As Integer = 0 To GridEXElamMarjoeeSatr_Kartabl.RootTable.Columns.Count - 1
                If GridEXElamMarjoeeSatr_Kartabl.RootTable.Columns(i).Type.IsValueType Then
                    GridEXElamMarjoeeSatr_Kartabl.RootTable.Columns(i).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                    GridEXElamMarjoeeSatr_Kartabl.RootTable.Columns(i).FormatMode = Janus.Windows.GridEX.FormatMode.UseIFormattable
                    GridEXElamMarjoeeSatr_Kartabl.RootTable.Columns(i).FormatString = "G"
                    GridEXElamMarjoeeSatr_Kartabl.RootTable.Columns(i).TotalFormatMode = Janus.Windows.GridEX.FormatMode.UseIFormattable
                    GridEXElamMarjoeeSatr_Kartabl.RootTable.Columns(i).TotalFormatString = "G"
                End If
            Next

            Me.CenterToScreen()
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> SetGrid_ElamMarjoeeSatr_Kartabl ")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> SetGrid_ElamMarjoeeSatr_Kartabl ")
        End Try
    End Sub
    Private Sub SetGrid_MarjoeeAzMoshtary()
        Try
            With GridEXMarjoeeAzMoshtary
                .DataSource = Nothing
                .DataSource = dsForm.Tables("tblMarjoeeAzMoshtary").DefaultView
                .SetDataBinding(dsForm.Tables("tblMarjoeeAzMoshtary").DefaultView, "")
                .RetrieveStructure()
            End With

            For i As Integer = 0 To GridEXMarjoeeAzMoshtary.CurrentTable.Columns.Count - 1
                GridEXMarjoeeAzMoshtary.CurrentTable.Columns.Item(i).Visible = False
            Next

            GridEXMarjoeeAzMoshtary.CurrentTable.Columns.Item("Taeed").Caption = "تاييد"
            GridEXMarjoeeAzMoshtary.CurrentTable.Columns.Item("Taeed").Visible = True
            GridEXMarjoeeAzMoshtary.CurrentTable.Columns.Item("Taeed").Width = 40
            GridEXMarjoeeAzMoshtary.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXMarjoeeAzMoshtary.CurrentTable.Columns.Item("Taeed").Position = 0
            GridEXMarjoeeAzMoshtary.CurrentTable.Columns.Item("Taeed").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXMarjoeeAzMoshtary.CurrentTable.Columns.Item("Taeed").HeaderAlignment = TextAlignment.Center

            GridEXMarjoeeAzMoshtary.CurrentTable.Columns.Item("ShomarehElamMarjoee").Caption = "ش مرجوعی"
            GridEXMarjoeeAzMoshtary.CurrentTable.Columns.Item("ShomarehElamMarjoee").Visible = True
            GridEXMarjoeeAzMoshtary.CurrentTable.Columns.Item("ShomarehElamMarjoee").Width = 80
            GridEXMarjoeeAzMoshtary.CurrentTable.Columns.Item("ShomarehElamMarjoee").EditType = EditType.NoEdit
            GridEXMarjoeeAzMoshtary.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXMarjoeeAzMoshtary.CurrentTable.Columns.Item("ShomarehElamMarjoee").Position = 1
            GridEXMarjoeeAzMoshtary.CurrentTable.Columns.Item("ShomarehElamMarjoee").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXMarjoeeAzMoshtary.CurrentTable.Columns.Item("ShomarehElamMarjoee").HeaderAlignment = TextAlignment.Center

            GridEXMarjoeeAzMoshtary.CurrentTable.Columns.Item("TarikhElamMarjoee").Caption = "تاریخ"
            GridEXMarjoeeAzMoshtary.CurrentTable.Columns.Item("TarikhElamMarjoee").Visible = True
            GridEXMarjoeeAzMoshtary.CurrentTable.Columns.Item("TarikhElamMarjoee").Width = 80
            GridEXMarjoeeAzMoshtary.CurrentTable.Columns.Item("TarikhElamMarjoee").EditType = EditType.NoEdit
            GridEXMarjoeeAzMoshtary.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXMarjoeeAzMoshtary.CurrentTable.Columns.Item("TarikhElamMarjoee").Position = 2
            GridEXMarjoeeAzMoshtary.CurrentTable.Columns.Item("TarikhElamMarjoee").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXMarjoeeAzMoshtary.CurrentTable.Columns.Item("TarikhElamMarjoee").HeaderAlignment = TextAlignment.Center

            GridEXMarjoeeAzMoshtary.CurrentTable.Columns.Item("CodeMoshtary").Caption = "کد مشتری"
            GridEXMarjoeeAzMoshtary.CurrentTable.Columns.Item("CodeMoshtary").Visible = True
            GridEXMarjoeeAzMoshtary.CurrentTable.Columns.Item("CodeMoshtary").Width = 80
            GridEXMarjoeeAzMoshtary.CurrentTable.Columns.Item("CodeMoshtary").EditType = EditType.NoEdit
            GridEXMarjoeeAzMoshtary.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXMarjoeeAzMoshtary.CurrentTable.Columns.Item("CodeMoshtary").Position = 3
            GridEXMarjoeeAzMoshtary.CurrentTable.Columns.Item("CodeMoshtary").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXMarjoeeAzMoshtary.CurrentTable.Columns.Item("CodeMoshtary").HeaderAlignment = TextAlignment.Center

            GridEXMarjoeeAzMoshtary.CurrentTable.Columns.Item("NameMoshtary").Caption = "نام مشتـری"
            GridEXMarjoeeAzMoshtary.CurrentTable.Columns.Item("NameMoshtary").Visible = True
            GridEXMarjoeeAzMoshtary.CurrentTable.Columns.Item("NameMoshtary").Width = 150
            GridEXMarjoeeAzMoshtary.CurrentTable.Columns.Item("NameMoshtary").EditType = EditType.NoEdit
            GridEXMarjoeeAzMoshtary.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXMarjoeeAzMoshtary.CurrentTable.Columns.Item("NameMoshtary").Position = 4
            GridEXMarjoeeAzMoshtary.CurrentTable.Columns.Item("NameMoshtary").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXMarjoeeAzMoshtary.CurrentTable.Columns.Item("NameMoshtary").HeaderAlignment = TextAlignment.Center

            GridEXMarjoeeAzMoshtary.CurrentTable.Columns.Item("NameForoshandeh").Caption = "فروشنـده"
            GridEXMarjoeeAzMoshtary.CurrentTable.Columns.Item("NameForoshandeh").Visible = True
            GridEXMarjoeeAzMoshtary.CurrentTable.Columns.Item("NameForoshandeh").Width = 150
            GridEXMarjoeeAzMoshtary.CurrentTable.Columns.Item("NameForoshandeh").EditType = EditType.NoEdit
            GridEXMarjoeeAzMoshtary.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXMarjoeeAzMoshtary.CurrentTable.Columns.Item("NameForoshandeh").Position = 5
            GridEXMarjoeeAzMoshtary.CurrentTable.Columns.Item("NameForoshandeh").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXMarjoeeAzMoshtary.CurrentTable.Columns.Item("NameForoshandeh").HeaderAlignment = TextAlignment.Center

            GridEXMarjoeeAzMoshtary.CurrentTable.Columns.Item("JamMablaghKol").Caption = "جمع کل مرجوعی"
            GridEXMarjoeeAzMoshtary.CurrentTable.Columns.Item("JamMablaghKol").Visible = True
            GridEXMarjoeeAzMoshtary.CurrentTable.Columns.Item("JamMablaghKol").Width = 120
            GridEXMarjoeeAzMoshtary.CurrentTable.Columns.Item("JamMablaghKol").EditType = EditType.NoEdit
            GridEXMarjoeeAzMoshtary.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXMarjoeeAzMoshtary.CurrentTable.Columns.Item("JamMablaghKol").Position = 6
            GridEXMarjoeeAzMoshtary.CurrentTable.Columns.Item("JamMablaghKol").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXMarjoeeAzMoshtary.CurrentTable.Columns.Item("JamMablaghKol").HeaderAlignment = TextAlignment.Center

            GridEXMarjoeeAzMoshtary.CurrentTable.Columns.Item("MablaghJayezeDasti").Caption = "مبلغ برگشتی جایزه"
            GridEXMarjoeeAzMoshtary.CurrentTable.Columns.Item("MablaghJayezeDasti").Visible = True
            GridEXMarjoeeAzMoshtary.CurrentTable.Columns.Item("MablaghJayezeDasti").Width = 120
            GridEXMarjoeeAzMoshtary.CurrentTable.Columns.Item("MablaghJayezeDasti").EditType = EditType.NoEdit
            GridEXMarjoeeAzMoshtary.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOn
            GridEXMarjoeeAzMoshtary.CurrentTable.Columns.Item("MablaghJayezeDasti").Position = 7
            GridEXMarjoeeAzMoshtary.CurrentTable.Columns.Item("MablaghJayezeDasti").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXMarjoeeAzMoshtary.CurrentTable.Columns.Item("MablaghJayezeDasti").HeaderAlignment = TextAlignment.Center

            GridEXMarjoeeAzMoshtary.CurrentTable.Columns.Item("MablaghTakhfifDasti").Caption = "مبلغ برگشتی تخفیف"
            GridEXMarjoeeAzMoshtary.CurrentTable.Columns.Item("MablaghTakhfifDasti").Visible = True
            GridEXMarjoeeAzMoshtary.CurrentTable.Columns.Item("MablaghTakhfifDasti").Width = 130
            GridEXMarjoeeAzMoshtary.CurrentTable.Columns.Item("MablaghTakhfifDasti").EditType = EditType.NoEdit
            GridEXMarjoeeAzMoshtary.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOn
            GridEXMarjoeeAzMoshtary.CurrentTable.Columns.Item("MablaghTakhfifDasti").Position = 8
            GridEXMarjoeeAzMoshtary.CurrentTable.Columns.Item("MablaghTakhfifDasti").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXMarjoeeAzMoshtary.CurrentTable.Columns.Item("MablaghTakhfifDasti").HeaderAlignment = TextAlignment.Center

            GridEXMarjoeeAzMoshtary.CurrentTable.Columns.Item("MKolMaliatAvarez").Caption = "مالیات و عوارض"
            GridEXMarjoeeAzMoshtary.CurrentTable.Columns.Item("MKolMaliatAvarez").Visible = True
            GridEXMarjoeeAzMoshtary.CurrentTable.Columns.Item("MKolMaliatAvarez").Width = 120
            GridEXMarjoeeAzMoshtary.CurrentTable.Columns.Item("MKolMaliatAvarez").EditType = EditType.NoEdit
            GridEXMarjoeeAzMoshtary.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXMarjoeeAzMoshtary.CurrentTable.Columns.Item("MKolMaliatAvarez").Position = 9
            GridEXMarjoeeAzMoshtary.CurrentTable.Columns.Item("MKolMaliatAvarez").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXMarjoeeAzMoshtary.CurrentTable.Columns.Item("MKolMaliatAvarez").HeaderAlignment = TextAlignment.Center

            GridEXMarjoeeAzMoshtary.CurrentTable.Columns.Item("JamMablagh").Caption = "مبلغ مرجوعی"
            GridEXMarjoeeAzMoshtary.CurrentTable.Columns.Item("JamMablagh").Visible = True
            GridEXMarjoeeAzMoshtary.CurrentTable.Columns.Item("JamMablagh").Width = 120
            GridEXMarjoeeAzMoshtary.CurrentTable.Columns.Item("JamMablagh").EditType = EditType.NoEdit
            GridEXMarjoeeAzMoshtary.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXMarjoeeAzMoshtary.CurrentTable.Columns.Item("JamMablagh").Position = 10
            GridEXMarjoeeAzMoshtary.CurrentTable.Columns.Item("JamMablagh").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXMarjoeeAzMoshtary.CurrentTable.Columns.Item("JamMablagh").HeaderAlignment = TextAlignment.Center

            GridEXMarjoeeAzMoshtary.CurrentTable.Columns.Item("ShomarehFaktor").Caption = "ش فاکتور"
            GridEXMarjoeeAzMoshtary.CurrentTable.Columns.Item("ShomarehFaktor").Visible = True
            GridEXMarjoeeAzMoshtary.CurrentTable.Columns.Item("ShomarehFaktor").Width = 80
            GridEXMarjoeeAzMoshtary.CurrentTable.Columns.Item("ShomarehFaktor").EditType = EditType.NoEdit
            GridEXMarjoeeAzMoshtary.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXMarjoeeAzMoshtary.CurrentTable.Columns.Item("ShomarehFaktor").Position = 11
            GridEXMarjoeeAzMoshtary.CurrentTable.Columns.Item("ShomarehFaktor").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXMarjoeeAzMoshtary.CurrentTable.Columns.Item("ShomarehFaktor").HeaderAlignment = TextAlignment.Center

            GridEXMarjoeeAzMoshtary.CurrentTable.Columns.Item("DorehFaktor").Caption = "دوره فاکتور"
            GridEXMarjoeeAzMoshtary.CurrentTable.Columns.Item("DorehFaktor").Visible = False
            GridEXMarjoeeAzMoshtary.CurrentTable.Columns.Item("DorehFaktor").Width = 80
            GridEXMarjoeeAzMoshtary.CurrentTable.Columns.Item("DorehFaktor").EditType = EditType.NoEdit
            GridEXMarjoeeAzMoshtary.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXMarjoeeAzMoshtary.CurrentTable.Columns.Item("DorehFaktor").Position = 12
            GridEXMarjoeeAzMoshtary.CurrentTable.Columns.Item("DorehFaktor").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXMarjoeeAzMoshtary.CurrentTable.Columns.Item("DorehFaktor").HeaderAlignment = TextAlignment.Center

            GridEXMarjoeeAzMoshtary.CurrentTable.Columns.Item("ccElamMarjoee").Caption = "ccElamMarjoee"
            GridEXMarjoeeAzMoshtary.CurrentTable.Columns.Item("ccElamMarjoee").Visible = False
            GridEXMarjoeeAzMoshtary.CurrentTable.Columns.Item("ccElamMarjoee").Width = 0
            GridEXMarjoeeAzMoshtary.CurrentTable.Columns.Item("ccElamMarjoee").EditType = EditType.NoEdit
            GridEXMarjoeeAzMoshtary.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXMarjoeeAzMoshtary.CurrentTable.Columns.Item("ccElamMarjoee").Position = 13
            GridEXMarjoeeAzMoshtary.CurrentTable.Columns.Item("ccElamMarjoee").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXMarjoeeAzMoshtary.CurrentTable.Columns.Item("ccElamMarjoee").HeaderAlignment = TextAlignment.Center

            GridEXMarjoeeAzMoshtary.CurrentTable.Columns.Item("ccFaktorTitr").Caption = "ccFaktorTitr"
            GridEXMarjoeeAzMoshtary.CurrentTable.Columns.Item("ccFaktorTitr").Visible = False
            GridEXMarjoeeAzMoshtary.CurrentTable.Columns.Item("ccFaktorTitr").Width = 0
            GridEXMarjoeeAzMoshtary.CurrentTable.Columns.Item("ccFaktorTitr").EditType = EditType.NoEdit
            GridEXMarjoeeAzMoshtary.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXMarjoeeAzMoshtary.CurrentTable.Columns.Item("ccFaktorTitr").Position = 14
            GridEXMarjoeeAzMoshtary.CurrentTable.Columns.Item("ccFaktorTitr").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXMarjoeeAzMoshtary.CurrentTable.Columns.Item("ccFaktorTitr").HeaderAlignment = TextAlignment.Center

            For i As Integer = 0 To GridEXMarjoeeAzMoshtary.RootTable.Columns.Count - 1
                If GridEXMarjoeeAzMoshtary.RootTable.Columns(i).Type.IsValueType Then
                    GridEXMarjoeeAzMoshtary.RootTable.Columns(i).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                    GridEXMarjoeeAzMoshtary.RootTable.Columns(i).FormatMode = Janus.Windows.GridEX.FormatMode.UseIFormattable
                    GridEXMarjoeeAzMoshtary.RootTable.Columns(i).FormatString = "G"
                    GridEXMarjoeeAzMoshtary.RootTable.Columns(i).TotalFormatMode = Janus.Windows.GridEX.FormatMode.UseIFormattable
                    GridEXMarjoeeAzMoshtary.RootTable.Columns(i).TotalFormatString = "G"
                End If
            Next

            Me.CenterToScreen()
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> SetGrid_MarjoeeAzMoshtary ")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> SetGrid_MarjoeeAzMoshtary ")
        End Try
    End Sub
    Private Sub SetGrid_MarjoeeAzMoshtarySatr()
        If dvMarjoeeAzMoshtary.Count = 0 Then
            Exit Sub
        End If

        Try
            With GridEXMarjoeeAzMoshtarySatr
                .DataSource = Nothing
                .DataSource = dsForm.Tables("tblMarjoeeAzMoshtarySatr").DefaultView
                .SetDataBinding(dsForm.Tables("tblMarjoeeAzMoshtarySatr").DefaultView, "")
                .RetrieveStructure()
            End With

            For i As Integer = 0 To GridEXMarjoeeAzMoshtarySatr.CurrentTable.Columns.Count - 1
                GridEXMarjoeeAzMoshtarySatr.CurrentTable.Columns.Item(i).Visible = False
            Next

            GridEXMarjoeeAzMoshtarySatr.CurrentTable.Columns.Item("CodeKala").Caption = "کـد کالا"
            GridEXMarjoeeAzMoshtarySatr.CurrentTable.Columns.Item("CodeKala").Visible = True
            GridEXMarjoeeAzMoshtarySatr.CurrentTable.Columns.Item("CodeKala").Width = 80
            GridEXMarjoeeAzMoshtarySatr.CurrentTable.Columns.Item("CodeKala").EditType = EditType.NoEdit
            GridEXMarjoeeAzMoshtarySatr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXMarjoeeAzMoshtarySatr.CurrentTable.Columns.Item("CodeKala").Position = 0
            GridEXMarjoeeAzMoshtarySatr.CurrentTable.Columns.Item("CodeKala").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXMarjoeeAzMoshtarySatr.CurrentTable.Columns.Item("CodeKala").HeaderAlignment = TextAlignment.Center

            GridEXMarjoeeAzMoshtarySatr.CurrentTable.Columns.Item("NameKala").Caption = "نام کالا"
            GridEXMarjoeeAzMoshtarySatr.CurrentTable.Columns.Item("NameKala").Visible = True
            GridEXMarjoeeAzMoshtarySatr.CurrentTable.Columns.Item("NameKala").Width = 300
            GridEXMarjoeeAzMoshtarySatr.CurrentTable.Columns.Item("NameKala").EditType = EditType.NoEdit
            GridEXMarjoeeAzMoshtarySatr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXMarjoeeAzMoshtarySatr.CurrentTable.Columns.Item("NameKala").Position = 1
            GridEXMarjoeeAzMoshtarySatr.CurrentTable.Columns.Item("NameKala").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXMarjoeeAzMoshtarySatr.CurrentTable.Columns.Item("NameKala").HeaderAlignment = TextAlignment.Center

            GridEXMarjoeeAzMoshtarySatr.CurrentTable.Columns.Item("Tedad").Caption = "تعــداد"
            GridEXMarjoeeAzMoshtarySatr.CurrentTable.Columns.Item("Tedad").Visible = True
            GridEXMarjoeeAzMoshtarySatr.CurrentTable.Columns.Item("Tedad").Width = 80
            GridEXMarjoeeAzMoshtarySatr.CurrentTable.Columns.Item("Tedad").EditType = EditType.NoEdit
            GridEXMarjoeeAzMoshtarySatr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXMarjoeeAzMoshtarySatr.CurrentTable.Columns.Item("Tedad").Position = 2
            GridEXMarjoeeAzMoshtarySatr.CurrentTable.Columns.Item("Tedad").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXMarjoeeAzMoshtarySatr.CurrentTable.Columns.Item("Tedad").HeaderAlignment = TextAlignment.Center

            GridEXMarjoeeAzMoshtarySatr.CurrentTable.Columns.Item("Fee").Caption = "قیمت"
            GridEXMarjoeeAzMoshtarySatr.CurrentTable.Columns.Item("Fee").Visible = True
            GridEXMarjoeeAzMoshtarySatr.CurrentTable.Columns.Item("Fee").Width = 100
            GridEXMarjoeeAzMoshtarySatr.CurrentTable.Columns.Item("Fee").EditType = EditType.NoEdit
            GridEXMarjoeeAzMoshtarySatr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXMarjoeeAzMoshtarySatr.CurrentTable.Columns.Item("Fee").Position = 3
            GridEXMarjoeeAzMoshtarySatr.CurrentTable.Columns.Item("Fee").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXMarjoeeAzMoshtarySatr.CurrentTable.Columns.Item("Fee").HeaderAlignment = TextAlignment.Center

            GridEXMarjoeeAzMoshtarySatr.CurrentTable.Columns.Item("MKOL").Caption = "قیمت کل"
            GridEXMarjoeeAzMoshtarySatr.CurrentTable.Columns.Item("MKOL").Visible = True
            GridEXMarjoeeAzMoshtarySatr.CurrentTable.Columns.Item("MKOL").Width = 120
            GridEXMarjoeeAzMoshtarySatr.CurrentTable.Columns.Item("MKOL").EditType = EditType.NoEdit
            GridEXMarjoeeAzMoshtarySatr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXMarjoeeAzMoshtarySatr.CurrentTable.Columns.Item("MKOL").Position = 4
            GridEXMarjoeeAzMoshtarySatr.CurrentTable.Columns.Item("MKOL").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXMarjoeeAzMoshtarySatr.CurrentTable.Columns.Item("MKOL").HeaderAlignment = TextAlignment.Center

            GridEXMarjoeeAzMoshtarySatr.CurrentTable.Columns.Item("txtNoeMarjoee").Caption = "نـوع مرجوعی"
            GridEXMarjoeeAzMoshtarySatr.CurrentTable.Columns.Item("txtNoeMarjoee").Visible = True
            GridEXMarjoeeAzMoshtarySatr.CurrentTable.Columns.Item("txtNoeMarjoee").Width = 100
            GridEXMarjoeeAzMoshtarySatr.CurrentTable.Columns.Item("txtNoeMarjoee").EditType = EditType.NoEdit
            GridEXMarjoeeAzMoshtarySatr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXMarjoeeAzMoshtarySatr.CurrentTable.Columns.Item("txtNoeMarjoee").Position = 5
            GridEXMarjoeeAzMoshtarySatr.CurrentTable.Columns.Item("txtNoeMarjoee").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXMarjoeeAzMoshtarySatr.CurrentTable.Columns.Item("txtNoeMarjoee").HeaderAlignment = TextAlignment.Center

            GridEXMarjoeeAzMoshtarySatr.CurrentTable.Columns.Item("txtElatMarjoee").Caption = "علت مرجوعی"
            GridEXMarjoeeAzMoshtarySatr.CurrentTable.Columns.Item("txtElatMarjoee").Visible = True
            GridEXMarjoeeAzMoshtarySatr.CurrentTable.Columns.Item("txtElatMarjoee").Width = 300
            GridEXMarjoeeAzMoshtarySatr.CurrentTable.Columns.Item("txtElatMarjoee").EditType = EditType.NoEdit
            GridEXMarjoeeAzMoshtarySatr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOn
            GridEXMarjoeeAzMoshtarySatr.CurrentTable.Columns.Item("txtElatMarjoee").Position = 6
            GridEXMarjoeeAzMoshtarySatr.CurrentTable.Columns.Item("txtElatMarjoee").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXMarjoeeAzMoshtarySatr.CurrentTable.Columns.Item("txtElatMarjoee").HeaderAlignment = TextAlignment.Center

            GridEXMarjoeeAzMoshtarySatr.CurrentTable.Columns.Item("ccMarjoeeSatr").Caption = "ccMarjoeeSatr"
            GridEXMarjoeeAzMoshtarySatr.CurrentTable.Columns.Item("ccMarjoeeSatr").Visible = False
            GridEXMarjoeeAzMoshtarySatr.CurrentTable.Columns.Item("ccMarjoeeSatr").Width = 0
            GridEXMarjoeeAzMoshtarySatr.CurrentTable.Columns.Item("ccMarjoeeSatr").EditType = EditType.NoEdit
            GridEXMarjoeeAzMoshtarySatr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOn
            GridEXMarjoeeAzMoshtarySatr.CurrentTable.Columns.Item("ccMarjoeeSatr").Position = 7
            GridEXMarjoeeAzMoshtarySatr.CurrentTable.Columns.Item("ccMarjoeeSatr").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXMarjoeeAzMoshtarySatr.CurrentTable.Columns.Item("ccMarjoeeSatr").HeaderAlignment = TextAlignment.Center

            For i As Integer = 0 To GridEXMarjoeeAzMoshtarySatr.RootTable.Columns.Count - 1
                If GridEXMarjoeeAzMoshtarySatr.RootTable.Columns(i).Type.IsValueType Then
                    GridEXMarjoeeAzMoshtarySatr.RootTable.Columns(i).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                    GridEXMarjoeeAzMoshtarySatr.RootTable.Columns(i).FormatMode = Janus.Windows.GridEX.FormatMode.UseIFormattable
                    GridEXMarjoeeAzMoshtarySatr.RootTable.Columns(i).FormatString = "G"
                    GridEXMarjoeeAzMoshtarySatr.RootTable.Columns(i).TotalFormatMode = Janus.Windows.GridEX.FormatMode.UseIFormattable
                    GridEXMarjoeeAzMoshtarySatr.RootTable.Columns(i).TotalFormatString = "G"
                End If
            Next

            Me.CenterToScreen()
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> SetGrid_ElamMarjoeeSatr_Kartabl ")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> SetGrid_ElamMarjoeeSatr_Kartabl ")
        End Try
    End Sub
    Private Sub btnAddTitr_MouseEnter(sender As Object, e As EventArgs) Handles btnAddTitr.MouseEnter
        ttSharh.Show("رکورد جدید", btnAddTitr)
    End Sub
    Private Sub btnAddTitr_MouseLeave(sender As Object, e As EventArgs) Handles btnAddTitr.MouseLeave
        ttSharh.Hide(btnAddTitr)
    End Sub
    Private Sub btnRemoveTitr_MouseEnter(sender As Object, e As EventArgs) Handles btnRemoveTitr.MouseEnter
        ttSharh.Show("حذف رکورد", btnRemoveTitr)
    End Sub
    Private Sub btnRemoveTitr_MouseLeave(sender As Object, e As EventArgs) Handles btnRemoveTitr.MouseLeave
        ttSharh.Hide(btnRemoveTitr)
    End Sub
    Private Sub btnSaveTitr_MouseEnter(sender As Object, e As EventArgs) Handles btnSaveTitr.MouseEnter
        ttSharh.Show("ذخیــره", btnSaveTitr)
    End Sub
    Private Sub btnSaveTitr_MouseLeave(sender As Object, e As EventArgs) Handles btnSaveTitr.MouseLeave
        ttSharh.Hide(btnSaveTitr)
    End Sub
    Private Sub btnCancelTitr_MouseEnter(sender As Object, e As EventArgs) Handles btnCancelTitr.MouseEnter
        ttSharh.Show("صرفنظـر", btnCancelTitr)
    End Sub
    Private Sub btnCancelTitr_MouseLeave(sender As Object, e As EventArgs) Handles btnCancelTitr.MouseLeave
        ttSharh.Hide(btnCancelTitr)
    End Sub
    Private Sub btnErsal_MouseEnter(sender As Object, e As EventArgs) Handles btnErsal.MouseEnter
        ttSharh.Show("ارسال به کارتابل", btnErsal)
    End Sub
    Private Sub btnErsal_MouseLeave(sender As Object, e As EventArgs) Handles btnErsal.MouseLeave
        ttSharh.Hide(btnErsal)
    End Sub
    Private Sub btnExit1_MouseEnter(sender As Object, e As EventArgs) Handles btnExit1.MouseEnter
        ttSharh.Show("خــروج", btnExit1)
    End Sub
    Private Sub btnExit1_MouseLeave(sender As Object, e As EventArgs) Handles btnExit1.MouseLeave
        ttSharh.Hide(btnExit1)
    End Sub
    Private Sub btnTaeed_MouseEnter(sender As Object, e As EventArgs) Handles btnTaeed.MouseEnter
        ttSharh.Show("تاییـــد", btnTaeed)
    End Sub
    Private Sub btnTaeed_MouseLeave(sender As Object, e As EventArgs) Handles btnTaeed.MouseLeave
        ttSharh.Hide(btnTaeed)
    End Sub
    Private Sub btnReturnToElamMarjoee_MouseEnter(sender As Object, e As EventArgs) Handles btnReturnToElamMarjoee.MouseEnter
        ttSharh.Show("بازگشت به اعلام مرجوعی", btnReturnToElamMarjoee)
    End Sub
    Private Sub btnReturnToElamMarjoee_MouseLeave(sender As Object, e As EventArgs) Handles btnReturnToElamMarjoee.MouseLeave
        ttSharh.Hide(btnReturnToElamMarjoee)
    End Sub
    Private Sub btnExit2_MouseEnter(sender As Object, e As EventArgs) Handles btnExit2.MouseEnter
        ttSharh.Show("خــروج", btnExit2)
    End Sub
    Private Sub btnExit2_MouseLeave(sender As Object, e As EventArgs) Handles btnExit2.MouseLeave
        ttSharh.Hide(btnExit2)
    End Sub
    Private Sub btnSodorMarjoeeAzMoshtary_MouseEnter(sender As Object, e As EventArgs) Handles btnSodorMarjoeeAzMoshtary.MouseEnter
        ttSharh.Show("صـدور مرجوعی از مشتری", btnSodorMarjoeeAzMoshtary)
    End Sub
    Private Sub btnSodorMarjoeeAzMoshtary_MouseLeave(sender As Object, e As EventArgs) Handles btnSodorMarjoeeAzMoshtary.MouseLeave
        ttSharh.Hide(btnSodorMarjoeeAzMoshtary)
    End Sub
    Private Sub btnReturnToKartabl_MouseEnter(sender As Object, e As EventArgs) Handles btnReturnToKartabl.MouseEnter
        ttSharh.Show("بازگشت به کارتابل", btnReturnToKartabl)
    End Sub
    Private Sub btnReturnToKartabl_MouseLeave(sender As Object, e As EventArgs) Handles btnReturnToKartabl.MouseLeave
        ttSharh.Hide(btnReturnToKartabl)
    End Sub
    Private Sub btnExit3_MouseEnter(sender As Object, e As EventArgs) Handles btnExit3.MouseEnter
        ttSharh.Show("خــروج", btnExit3)
    End Sub
    Private Sub btnExit3_MouseLeave(sender As Object, e As EventArgs) Handles btnExit3.MouseLeave
        ttSharh.Hide(btnExit3)
    End Sub
    Private Sub tbMain_SelectedIndexChanged(sender As Object, e As EventArgs) Handles tbMain.SelectedIndexChanged
        Mode = UD_Dll.Enums.GL_ModeForms.None
        SetButton()
        Search(tbMain.SelectedIndex)
    End Sub
    Private Sub btnAddTitr_Click(sender As Object, e As EventArgs) Handles btnAddTitr.Click
        Mode = UD_Dll.Enums.GL_ModeForms.AddNewRecord
        SetButton()
    End Sub
    Private Sub btnRemoveTitr_Click(sender As Object, e As EventArgs) Handles btnRemoveTitr.Click
        Dim PK As Integer = 0
        Dim Sh_Marjoee As Integer = 0

        If MsgBox("آیا به انتخاب خود اطمینان دارید ؟ ", MsgBoxStyle.Question + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.YesNo, "") = MsgBoxResult.Yes Then
            For i As Integer = 0 To GridEXElamMarjoee.RowCount - 1
                If GridEXElamMarjoee.GetRows(i).Cells("Taeed").Value = True Then
                    PK = Val(GridEXElamMarjoee.GetRows(i).Cells("ccElamMarjoee").Text.Replace(",", ""))
                    objTools.DDelete("tblFO_ElamMarjoee", "ccElamMarjoee = " & PK)
                End If
            Next

            Search(tbMain.SelectedIndex)
        End If
    End Sub
    Private Sub btnSaveTitr_Click(sender As Object, e As EventArgs) Handles btnSaveTitr.Click
        If Mode = UD_Dll.Enums.GL_ModeForms.AddNewRecord Then
            If SaveTitr() = False Then
                Exit Sub
            End If
        ElseIf Mode = UD_Dll.Enums.GL_ModeForms.UpdateRecord Then

        End If

        Mode = UD_Dll.Enums.GL_ModeForms.None
        ClearForm()
        Search(tbMain.SelectedIndex)
    End Sub
    Private Sub btnCancelTitr_Click(sender As Object, e As EventArgs) Handles btnCancelTitr.Click
        Mode = UD_Dll.Enums.GL_ModeForms.None
        ClearForm()
    End Sub
    Private Sub btnErsal_Click(sender As Object, e As EventArgs) Handles btnErsal.Click
        If MsgBox("آیا به انتخاب خود اطمینان دارید ؟", MsgBoxStyle.Question + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.YesNo, "") = MsgBoxResult.No Then
            Exit Sub
        End If

        Dim PK As Integer = 0
        Dim Sh_Marjoee As Integer = 0

        For i As Integer = 0 To GridEXElamMarjoee.RowCount - 1
            If GridEXElamMarjoee.GetRows(i).Cells("Taeed").Value = True Then
                PK = Val(GridEXElamMarjoee.GetRows(i).Cells("ccElamMarjoee").Text.Replace(",", ""))
                If objTools.DCount("ccMarjoee", "tblFO_ElamMarjoeeSatr", "ccMarjoee = " & PK) = 0 Then
                    Sh_Marjoee = Val(GridEXElamMarjoee.GetRows(i).Cells("ShomarehElamMarjoee").Text.Replace(",", ""))
                    MsgBox("اعلام مرجوعی شماره " & Sh_Marjoee & " فاقد کالا می باشد !", MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.OkOnly, "خطا")
                Else
                    objTools.DUpdate("sVazeiat", "tblFO_ElamMarjoee", "100", "ccElamMarjoee = " & PK)
                End If
            End If
        Next

        Search(tbMain.SelectedIndex)
    End Sub
    Private Sub GridEXElamMarjoee_DoubleClick(sender As Object, e As EventArgs) Handles GridEXElamMarjoee.DoubleClick
        Mode = UD_Dll.Enums.GL_ModeForms.UpdateRecord
        SetButton()
        SetFormTitr()
    End Sub
    Private Sub btnAddSatr_Click(sender As Object, e As EventArgs) Handles btnAddSatr.Click
        If Val(GridEXElamMarjoee.CurrentRow.Cells("IsFaktorMarjoee").Text.Replace(",", "")) = True Then
            MsgBox("این رکورد فاکتور مرجوعی بوده و امکان افزودن سطر جدید به آن وجود ندارد !", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "")
            Exit Sub
        End If

        Mode = UD_Dll.Enums.GL_ModeForms.AddNewRow
        flg = True
        SetButton()
    End Sub
    Private Sub btnRemoveSatr_Click(sender As Object, e As EventArgs) Handles btnRemoveSatr.Click
        If Val(GridEXElamMarjoee.CurrentRow.Cells("IsFaktorMarjoee").Text.Replace(",", "")) = True Then
            MsgBox("این رکورد فاکتور مرجوعی بوده و حـذف سطرهای آن وجود ندارد !", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "")
            Exit Sub
        End If

        If Val(GridEXElamMarjoee.CurrentRow.Cells("IsJayezehMarjoee").Text.Replace(",", "")) = True Then
            MsgBox("این ردیف کالای جایزه برگشتی میباشد و امکان ویرایش آن نیست.", MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, "پیام")
            Exit Sub
        End If

        If MsgBox("آیا به انتخاب خود اطمینان دارید ؟", MsgBoxStyle.Question + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.YesNo, "") = MsgBoxResult.No Then
            Exit Sub
        Else
            objTools.DDelete("tblFO_ElamMarjoeeSatr", "ccMarjoeeSatr = " & Val(GridEXElamMarjoeeSatr.CurrentRow.Cells("ccMarjoeeSatr").Text.Replace(",", "")))

            Dim ccMarjoee As Integer = 0
            Dim ccFaktor As Integer = 0
            Dim IsTakhfifDasty As Boolean = False

            ccMarjoee = Val(GridEXElamMarjoee.CurrentRow.Cells("ccElamMarjoee").Text.Replace(",", ""))
            ccFaktor = objTools.ConvertNulls(Val(GridEXElamMarjoee.CurrentRow.Cells("ccFaktorTitr").Text.Replace(",", "")), 0)
            IsTakhfifDasty = objTools.ConvertNulls(objTools.DLookup("IsTakhfifDasty", "tblFO_Faktor", "ccFaktorTitr = " & ccFaktor), False)

            If IsTakhfifDasty = False Then
                If ccFaktor <> 0 Then
                    TJ = New TakhfifOJavaiez.TakhfifJayezeh(ccFaktor, ccMarjoee)
                    TJ.ApplyTakhfifJayezeh()
                    RollBackMalyatAvarez(ccFaktor, ccMarjoee)
                End If
            End If

            SearchSatr(tbMain.SelectedIndex)
        End If
    End Sub
    Private Sub btnSaveSatr_Click(sender As Object, e As EventArgs) Handles btnSaveSatr.Click
        If Mode = UD_Dll.Enums.GL_ModeForms.AddNewRow Then
            If SaveSatr() = False Then
                Exit Sub
            End If

            flg = False
        ElseIf Mode = UD_Dll.Enums.GL_ModeForms.UpdateRow Then

        End If

        Mode = UD_Dll.Enums.GL_ModeForms.None
        ClearForm()
        SearchSatr(tbMain.SelectedIndex)
    End Sub
    Private Function SaveSatr() As Boolean
        SaveSatr = False

        Dim cnSQL As New SqlConnection
        Dim cmSQL As New SqlCommand
        Dim strSQL As String = ""
        Dim ccMarjoee As Integer = 0
        Dim ccFaktor As Integer = 0
        Dim IsTakhfifDasty As Boolean = False

        ccMarjoee = Val(GridEXElamMarjoee.CurrentRow.Cells("ccElamMarjoee").Text.Replace(",", ""))
        ccFaktor = objTools.ConvertNulls(Val(GridEXElamMarjoee.CurrentRow.Cells("ccFaktorTitr").Text.Replace(",", "")), 0)
        IsTakhfifDasty = objTools.ConvertNulls(objTools.DLookup("IsTakhfifDasty", "tblFO_Faktor", "ccFaktorTitr = " & ccFaktor), False)

        If IsValidRow("All") = False Then
            Exit Function
        End If

        Try
            strSQL = "Sales.spPishFaktorGheireGhateei_ElamMarjoee_InsertSatr "

            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            cmSQL.Parameters.AddWithValue("ccMarjoee", ccMarjoee)
            cmSQL.Parameters.AddWithValue("ccKala", txtCodeKala.Tag)
            cmSQL.Parameters.AddWithValue("Tedad", txtTedadKala.Text.Trim)
            cmSQL.Parameters.AddWithValue("Fee", txtFee.Text)
            cmSQL.Parameters.AddWithValue("sElatMarjoee", cmbsElatMarjoee.SelectedValue)
            If rbKharab.Checked Then
                cmSQL.Parameters.AddWithValue("NoeMarjoee", UD_Dll.Enums.FO_NoeKalaElamMarjoee.Karab)
            ElseIf rbSalem.Checked Then
                cmSQL.Parameters.AddWithValue("NoeMarjoee", UD_Dll.Enums.FO_NoeKalaElamMarjoee.Salem)
            End If
            cmSQL.Parameters.AddWithValue("ccFaktorTitr", ccFaktor)

            cmSQL.ExecuteNonQuery()

            cnSQL.Close()

            If IsTakhfifDasty = False Then
                If ccFaktor <> 0 Then
                    TJ = New TakhfifOJavaiez.TakhfifJayezeh(ccFaktor, ccMarjoee)
                    TJ.ApplyTakhfifJayezeh()
                    RollBackMalyatAvarez(ccFaktor, ccMarjoee)
                End If
            End If

            SaveSatr = True
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> SaveSatr ")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> SaveSatr ")
        End Try
    End Function
    Private Sub RollBackMalyatAvarez(ByVal ccFaktor As Integer, ByVal ccMarjoee As Integer)
        Dim strSQL As String = ""
        Dim cn As New SqlConnection(ConnectionString)
        Dim da As SqlDataAdapter = Nothing
        Dim dt As New DataTable
        Dim cm As SqlCommand = Nothing
        Dim M As Double = 0
        Dim AvarezOneKala As Double = 0
        Dim MaliyatOneKala As Double = 0

        Dim TarikhFaktor As String = objTools.DLookup("FaktorTarikh", "tblFO_Faktor", "ccFaktorTitr = " & ccFaktor)

        strSQL = "SELECT ccMarjoeeSatr,ccKala,MKol,Tedad3 FROM tblFO_ElamMarjoeeSatr WHERE ccFaktorTitr = " & ccFaktor & " AND IsJayezehMarjoee = 0 AND ccMarjoee = " & ccMarjoee
        cn.Open()

        da = New SqlDataAdapter(strSQL, cn)
        Try

            da.Fill(dt)

            For Each dr As DataRow In dt.Rows

                AvarezOneKala = objTools.DLookup("(MablaghAvarez) / Tedad3", "tblFO_FaktorSatr", "ccFaktorTitr = " & ccFaktor & " AND ccKala = " & dr("ccKala"))
                MaliyatOneKala = objTools.DLookup("(MablaghMalyat) / Tedad3", "tblFO_FaktorSatr", "ccFaktorTitr = " & ccFaktor & " AND ccKala = " & dr("ccKala"))
                M = (dr("Tedad3") * AvarezOneKala) + (dr("Tedad3") * MaliyatOneKala)

                strSQL = "UPDATE tblFO_ElamMarjoeeSatr SET "
                strSQL &= " MablaghMaliatAvarez = " & Math.Round(M, 0)
                strSQL &= " WHERE ccMarjoeeSatr = " & dr("ccMarjoeeSatr")
                strSQL &= " And ccKala In (Select ccKala From tblAN_Kala where MashmuleMaliyat = 1 OR MashmuleAvarez = 1)"
                strSQL &= " And ccFaktorTitr in ( SELECT ccFaktorTitr FROM tblFO_Faktor WHERE ccFaktorTitr = " & ccFaktor & " AND JamMablaghAvarez <> 0)"

                cm = New SqlCommand(strSQL, cn)
                cm.ExecuteNonQuery()

                If objTools.DLookup("MkolTakhfifMalyatAvarez", "tblFO_Faktor", "ccFaktorTitr = " & ccFaktor) <> 0 Then
                    strSQL = "UPDATE tblFO_ElamMarjoeeSatr SET "
                    strSQL &= " TakhfifMalyatAvarez = " & Math.Round(M, 0)
                    strSQL &= " WHERE ccMarjoeeSatr = " & dr("ccMarjoeeSatr")
                    strSQL &= " And ccKala In (Select ccKala From tblAN_Kala where MashmuleMaliyat = 1 OR MashmuleAvarez = 1)"
                    strSQL &= " And ccFaktorTitr in ( SELECT ccFaktorTitr FROM tblFO_Faktor WHERE ccFaktorTitr = " & ccFaktor & " AND JamMablaghAvarez <> 0)"

                    cm = New SqlCommand(strSQL, cn)
                    cm.ExecuteNonQuery()
                End If

            Next
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> RollBackMalyatAvarez ")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> RollBackMalyatAvarez ")
        End Try
    End Sub
    Private Function IsValidRow(ByVal chkField As String) As Boolean
        Try
            IsValidRow = False

            If chkField = "txtCodeKala" Or chkField = "All" Then
                If txtCodeKala.Tag = 0 Then
                    ErrPro.SetError(txtCodeKala, "کد کالا را انتخاب کنید.")
                    MsgBox("کد کالا را انتخاب کنید.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
                    txtCodeKala.Focus()
                    Exit Function
                End If
                ErrPro.SetError(txtCodeKala, "")
            End If

            If chkField = "txtTedadKala" Or chkField = "All" Then
                If (Me.txtTedadKala.Text = "") Or (CDbl(Me.txtTedadKala.Text) = 0) Then
                    ErrPro.SetError(Me.txtTedadKala, "تعداد کالا را وارد کنید.")
                    MsgBox("تعداد کالا را وارد کنید.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
                    txtTedadKala.Focus()
                    Exit Function
                End If
                ErrPro.SetError(Me.txtTedadKala, "")
            End If
            If Mode = UD_Dll.Enums.GL_ModeForms.AddNewRow Then
                If chkField = "txtCodeKala" Or chkField = "All" Then
                    If objTools.ConvertNulls(objTools.DCount("ccMarjoeeSatr", "tblfo_ElamMarjoeeSatr", "  ccMarjoee = " & Val(GridEXElamMarjoee.CurrentRow.Cells("ccElamMarjoee").Text.Replace(",", "")) & " AND ccKala = " & txtCodeKala.Tag & " AND IsJayezehMarjoee <> 1"), 0) <> 0 Then
                        ErrPro.SetError(txtCodeKala, "  کالای وارد شده تکراری است.")
                        MsgBox(" کالای وارد شده تکراری است.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
                        txtCodeKala.Focus()
                        Exit Function
                    End If
                    ErrPro.SetError(txtCodeKala, "")
                End If

            End If
            If chkField = "txtFee" Or chkField = "All" Then
                If Me.txtFee.Text = "" Then
                    ErrPro.SetError(Me.txtFee, "قیمت را وارد کنید.")
                    MsgBox("قیمت را وارد کنید.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
                    txtFee.Focus()
                    Exit Function
                End If
                ErrPro.SetError(Me.txtFee, "")
            End If

            If chkField = "cmbsElatMarjoee" Or chkField = "All" Then
                If Me.cmbsElatMarjoee.Text = "" Then
                    ErrPro.SetError(Me.cmbsElatMarjoee, "علت اعلام مرجوعی را وارد کنید.")
                    MsgBox("علت اعلام مرجوعی را وارد کنید.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
                    cmbsElatMarjoee.Focus()
                    Exit Function
                End If
                ErrPro.SetError(Me.cmbsElatMarjoee, "")
            End If

            If (chkField = "Marjoee") Or (chkField = "All") Then
                If (rbKharab.Checked = False) And (rbSalem.Checked = False) Then
                    ErrPro.SetError(Me.rbSalem, "نوع مرجوعی را انتخاب کنید.")
                    ErrPro.SetError(Me.rbKharab, "نوع مرجوعی را انتخاب کنید.")
                    MsgBox("نوع مرجوعی را انتخاب کنید.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
                    Exit Function
                End If
                ErrPro.SetError(Me.rbSalem, "")
                ErrPro.SetError(Me.rbKharab, "")
            End If

            If objTools.ConvertNulls(Val(GridEXElamMarjoee.CurrentRow.Cells("ccFaktorTitr").Text.Replace(",", "")), 0) <> 0 AndAlso objTools.ConvertNulls(Val(GridEXElamMarjoee.CurrentRow.Cells("ccFaktorTitr").Text.Replace(",", "")), 0) <> 0 Then
                If objTools.ConvertNulls(objTools.DLookup("ccKala", "qryFO_FaktorTitrSatr", "CodeDoreh = " & Val(GridEXElamMarjoee.CurrentRow.Cells("DorehFaktor").Text.Replace(",", "")) & " AND FaktorShomareh = " & Val(GridEXElamMarjoee.CurrentRow.Cells("ShomarehFaktor").Text.Replace(",", "")) & " AND ccKala =" & txtCodeKala.Tag), 0) = 0 Then
                    MsgBox("کالای انتخاب شده در لیست کالاهای فاکتور وجود ندارد.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
                    Exit Function
                End If

                Dim TedadFaktor As Double = objTools.ConvertNulls(objTools.DLookup("sum(cast(Tedad3 as float))", "qryFO_FaktorTitrSatr", "CodeDoreh = " & Val(GridEXElamMarjoee.CurrentRow.Cells("DorehFaktor").Text.Replace(",", "")) & " AND FaktorShomareh = " & Val(GridEXElamMarjoee.CurrentRow.Cells("ShomarehFaktor").Text.Replace(",", "")) & " AND ccKala =" & txtCodeKala.Tag & " AND IsJayezeh=0"), 0)
                If Mode = UD_Dll.Enums.GL_ModeForms.AddNewRow Then
                    TedadFaktor = TedadFaktor - objTools.ConvertNulls(objTools.DLookup("sum(cast(Tedad3 as float))", "qryFO_ElamMarjoeeTitrSatr", "ccFaktorTitr = " & Val(GridEXElamMarjoee.CurrentRow.Cells("ccFaktorTitr").Text.Replace(",", "")) & " AND ccKala = " & txtCodeKala.Tag & " AND IsJayezehMarjoee = 0 "), 0)
                End If
                If TedadFaktor < Val(txtTedadKala.Text) Then
                    MsgBox("تعداد کالای وارد شده بیشتر از تعداد کالای موجود در فاکتور انتخابی می باشد. تعداد موجود در فاکتور = " & TedadFaktor, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
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
    Private Sub btnCancelSatr_Click(sender As Object, e As EventArgs) Handles btnCancelSatr.Click
        Mode = UD_Dll.Enums.GL_ModeForms.None
        flg = False
        SetButton()
    End Sub
    Private Sub GridEXElamMarjoeeSatr_DoubleClick(sender As Object, e As EventArgs) Handles GridEXElamMarjoeeSatr.DoubleClick
        If Val(GridEXElamMarjoee.CurrentRow.Cells("IsFaktorMarjoee").Text.Replace(",", "")) = True Then
            MsgBox("این رکورد فاکتور مرجوعی بوده و امکان ایجاد تغییر درآن وجود ندارد !", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "")
            Exit Sub
        End If

        If Val(GridEXElamMarjoee.CurrentRow.Cells("IsJayezehMarjoee").Text.Replace(",", "")) = True Then
            MsgBox("این ردیف کالای جایزه برگشتی میباشد و امکان ویرایش آن نیست.", MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, "پیام")
            Exit Sub
        End If

        Mode = UD_Dll.Enums.GL_ModeForms.UpdateRow
        ClearForm()
    End Sub
    Private Sub btnTaeed_Click(sender As Object, e As EventArgs) Handles btnTaeed.Click
        If MsgBox("آیا به انتخاب خود اطمینان دارید ؟", MsgBoxStyle.Question + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.YesNo, "") = MsgBoxResult.No Then
            Exit Sub
        End If

        Dim PK As Integer = 0
        Dim Sh_Marjoee As Integer = 0

        For i As Integer = 0 To GridEXElamMarjoee_Kartabl.RowCount - 1
            If GridEXElamMarjoee_Kartabl.GetRows(i).Cells("Taeed").Value = True Then
                PK = Val(GridEXElamMarjoee_Kartabl.GetRows(i).Cells("ccElamMarjoee").Text.Replace(",", ""))
                objTools.DUpdate("sVazeiat", "tblFO_ElamMarjoee", "101", "ccElamMarjoee = " & PK)
            End If
        Next

        Search(tbMain.SelectedIndex)
    End Sub
    Private Sub btnReturnToElamMarjoee_Click(sender As Object, e As EventArgs) Handles btnReturnToElamMarjoee.Click
        If MsgBox("آیا به انتخاب خود اطمینان دارید ؟", MsgBoxStyle.Question + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.YesNo, "") = MsgBoxResult.No Then
            Exit Sub
        End If

        Dim PK As Integer = 0
        Dim Sh_Marjoee As Integer = 0

        For i As Integer = 0 To GridEXElamMarjoee_Kartabl.RowCount - 1
            If GridEXElamMarjoee_Kartabl.GetRows(i).Cells("Taeed").Value = True Then
                PK = Val(GridEXElamMarjoee_Kartabl.GetRows(i).Cells("ccElamMarjoee").Text.Replace(",", ""))
                objTools.DUpdate("sVazeiat", "tblFO_ElamMarjoee", "99", "ccElamMarjoee = " & PK)
            End If
        Next

        Search(tbMain.SelectedIndex)
    End Sub
    Private Sub SetFormTitr()
        Dim PK As Integer = 0
        PK = Val(GridEXElamMarjoee.CurrentRow.Cells("ccElamMarjoee").Text.Replace(",", ""))

        mskTarikhElamMarjoee.Text = objTools.DLookup("TarikhElamMarjoee", "tblFO_ElamMarjoee", "ccElamMarjoee = " & PK)
        txtCodeMoshtary.Text = GridEXElamMarjoee.CurrentRow.Cells("CodeMoshtary").Text.Replace(",", "").Trim
        If Val(GridEXElamMarjoee.CurrentRow.Cells("DorehFaktor").Text.Replace(",", "")) <> 0 Then
            cmbsCodeDorehFaktor.SelectedValue = Val(GridEXElamMarjoee.CurrentRow.Cells("DorehFaktor").Text.Replace(",", ""))
            txtShomarehFaktor.Text = Val(GridEXElamMarjoee.CurrentRow.Cells("ShomarehFaktor").Text.Replace(",", ""))
        End If
        cmbForoshandeh.SelectedValue = objTools.DLookup("ccForoshandeh", "tblFO_ElamMarjoee", "ccElamMarjoee = " & PK)
        txtMTakhfif.Text = Val(GridEXElamMarjoee.CurrentRow.Cells("MablaghTakhfifDasti").Text.Replace(",", ""))
        txtMJayeze.Text = Val(GridEXElamMarjoee.CurrentRow.Cells("MablaghJayezeDasti").Text.Replace(",", ""))
        chkFaktorMarjoee.Checked = Val(GridEXElamMarjoee.CurrentRow.Cells("IsFaktorMarjoee").Text.Replace(",", ""))
        If chkFaktorMarjoee.Checked = True Then
            cmbElatFaktorMarjoee.SelectedValue = Val(GridEXElamMarjoee.CurrentRow.Cells("sElat").Text.Replace(",", ""))
        Else
            cmbElatFaktorMarjoee.SelectedIndex = -1
            cmbElatFaktorMarjoee.SelectedIndex = -1
        End If

    End Sub
    Private Sub SetFormSatr()
        Dim PK As Integer = 0
        PK = Val(GridEXElamMarjoee.CurrentRow.Cells("ccMarjoeeSatr").Text.Replace(",", ""))
        txtCodeKala.Text = Val(GridEXElamMarjoeeSatr.CurrentRow.Cells("CodeKala").Text.Replace(",", ""))
        txtTedadKala.Text = Val(GridEXElamMarjoeeSatr.CurrentRow.Cells("Tedad").Text.Replace(",", ""))
        cmbsElatMarjoee.SelectedValue = objTools.DLookup("sElatMarjoee", "tblFO_ElamMarjoeeSatr", "ccMarjoeeSatr = " & PK)
        txtFee.Text = Val(GridEXElamMarjoee.CurrentRow.Cells("Fee").Text.Replace(",", ""))
        If Val(GridEXElamMarjoee.CurrentRow.Cells("NoeMarjoee").Text.Replace(",", "")) = 1 Then
            rbKharab.Checked = True
        ElseIf Val(GridEXElamMarjoee.CurrentRow.Cells("Fee").Text.Replace(",", "")) = 2 Then
            rbSalem.Checked = True
        End If
    End Sub
    Private Sub txtCodeMoshtary_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtCodeMoshtary.KeyPress
        Try
            If (Asc(e.KeyChar()) < 48 Or Asc(e.KeyChar()) > 57) And (Asc(e.KeyChar()) <> 8) Then
                e.Handled = True
            End If
            If e.KeyChar = Chr(Keys.Space) Then
                Dim objMoshtary As New Forms_dll.frmFO_MoshtarySearch
                Dim StrSql As String = ""

                StrSql = "Select * from qryFO_Moshtary Where CodeMahal=" & CodeMahalFaal & " AND sVazeiat = " & UD_Dll.Enums.FO_VaziatMoshtary.Faal
                StrSql &= " AND Not exists (Select * From tblGL_SecurityData where namekarbar= '" & UserName & "' and CodeSubSystem = 614 and pk = qryFO_Moshtary.ccMoshtary) "
                StrSql &= " AND Not exists (Select * From tblGL_SecurityData where namekarbar= '" & UserName & "' and CodeSubSystem = 10000 and pk = qryFO_Moshtary.sNoeMoshtary) "
                StrSql &= " order by sMantagheh,sMahaleh,NameMoshtary"


                tCodeMoshtary = ""
                tNameMoshtary = ""
                tccMoshtary = ""

                If txtCodeMoshtary.Text.Length <> 0 Then
                    tCodeMoshtary = txtCodeMoshtary.Text
                End If

                objMoshtary.MultiSelection = False
                SearchItem = "CodeMoshtary"
                objMoshtary.SetForm(StrSql)
                objMoshtary.ShowDialog()
                txtCodeMoshtary.Tag = objMoshtary.tccMoshtary
                txtCodeMoshtary.Text = objMoshtary.tCodeMoshtary
                lblNameMoshtary.Text = objMoshtary.tNameMoshtary
                ccMoshtary = txtCodeMoshtary.Tag

                MultiSelection = False
            End If
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->txtCodeMoshtary_KeyPress")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->txtCodeMoshtary_KeyPress")
        End Try
    End Sub

    Private Sub txtCodeMoshtary_TextChanged(sender As Object, e As EventArgs) Handles txtCodeMoshtary.TextChanged
        Dim Criteria As String = ""

        Criteria = "CodeMahal=" & CodeMahalFaal & " AND sVazeiat = " & UD_Dll.Enums.FO_VaziatMoshtary.Faal
        Criteria &= " AND Not exists (Select * From tblGL_SecurityData where namekarbar= '" & UserName & "' and CodeSubSystem = 614 and pk = qryFO_Moshtary.ccMoshtary) "
        Criteria &= " And CodeMoshtary = '" & IIf(IsNothing(Me.txtCodeMoshtary.Text), 0, Me.txtCodeMoshtary.Text) & "'"

        Me.lblNameMoshtary.Text = objTools.ConvertNulls(objTools.DLookup("NameMoshtary", "qryFO_Moshtary", Criteria), "")
        Me.txtCodeMoshtary.Tag = objTools.ConvertNulls(objTools.DLookup("ccMoshtary", "qryFO_Moshtary", Criteria), 0)
        If IsNumeric(Me.txtCodeMoshtary.Tag) Then
            Me.ccMoshtary = Me.txtCodeMoshtary.Tag
        Else
            Me.ccMoshtary = 0
        End If

        txtShomarehFaktor.Text = ""
        txtShomarehFaktor.Tag = 0
        ccFaktorTitr = 0
    End Sub
    Private Sub txtShomarehFaktor_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtShomarehFaktor.KeyPress
        Try
            If (Asc(e.KeyChar()) < 48 Or Asc(e.KeyChar()) > 57) And (Asc(e.KeyChar()) <> 8) Then
                e.Handled = True
            End If
            If e.KeyChar = Chr(Keys.Space) Then
                Dim objFaktor As New Forms_dll.FaktorSearch
                Dim StrSql As String

                If cmbsCodeDorehFaktor.SelectedIndex = -1 Then
                    MsgBox("دوره فاکتور را انتخاب کنید.", MsgBoxStyle.Information + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, "")
                    Exit Sub
                End If

                'StrSql = "Select * from qryFO_Faktor Where "
                'StrSql &= " sVazeiat = 4261  AND CodeDoreh=" & cmbsCodeDorehFaktor.SelectedValue
                'StrSql &= " AND CodeMahal = " & CodeMahalFaal

                StrSql = "SELECT FaktorShomareh, b.ccForoshandeh, a.ccMoshtary, a.CodeDoreh, ccFaktorTitr,"
                StrSql &= " c.NameMoshtary, d.FName + ' ' + d.LName AS NameMamorPakhsh, g.Sharh AS txtNoePardakht,"
                StrSql &= " f.FName + ' ' + f.LName AS NameForoshandeh, dbo.SetDateSlash(FaktorTarikh) AS FaktorTarikhSlash"
                StrSql &= " FROM tblFO_Faktor AS a WITH(NOLOCK) LEFT OUTER JOIN"
                StrSql &= " tblFO_PishFaktor AS b WITH(NOLOCK) ON a.ccPishFaktor = b.ccPishFaktorTitr LEFT OUTER JOIN"
                StrSql &= " tblFO_Moshtary AS c WITH(NOLOCK) ON a.ccMoshtary = c.ccMoshtary LEFT OUTER JOIN"
                StrSql &= " tblGL_MoshakhasatFardi AS d WITH(NOLOCK) ON a.ccMamorPakhsh = d.CodeFard LEFT OUTER JOIN"
                StrSql &= " tblFO_Foroshandeh AS e WITH(NOLOCK) ON b.ccForoshandeh = e.ccForoshandeh LEFT OUTER JOIN"
                StrSql &= " tblGL_MoshakhasatFardi AS f WITH(NOLOCK) ON e.CodeFard = f.CodeFard LEFT OUTER JOIN"
                StrSql &= " tblGL_ShenasehOmomi AS g WITH(NOLOCK) ON a.sNoePardakht = g.Code"
                StrSql &= " WHERE a.sVazeiat = 4261 And a.CodeDoreh = " & cmbsCodeDorehFaktor.SelectedValue
                StrSql &= " And a.CodeMahal = " & CodeMahalFaal

                If txtCodeMoshtary.Tag <> 0 Then
                    StrSql &= " AND a.ccMoshtary = " & txtCodeMoshtary.Tag
                End If

                tFaktorShomareh = ""
                SearchItem = ""

                If txtShomarehFaktor.Text <> "" Then
                    tFaktorShomareh = txtShomarehFaktor.Text
                    SearchItem = "FaktorShomareh"
                End If

                objFaktor.SetForm(StrSql)
                objFaktor.TopMost = True
                objFaktor.ShowDialog()


                txtShomarehFaktor.Text = objFaktor.tFaktorShomareh
                FaktorShomareh = objFaktor.tFaktorShomareh
                CodeDorehFaktor = cmbsCodeDorehFaktor.SelectedValue
                ccFaktorTitr = objFaktor.tFaktorccFaktor
                ccForoshandeh = objFaktor.tFaktorccForoshandeh
                ccMoshtary = objFaktor.tFaktorccMoshtary

                If objTools.ConvertNulls(objTools.DLookup("IsTakhfifDasty", "tblFO_Faktor", "ccFaktorTitr = " & ccFaktorTitr), False) = False Then
                    txtMJayeze.Text = 0
                    txtMTakhfif.Text = 0
                    txtMJayeze.Enabled = False
                    txtMTakhfif.Enabled = False
                Else
                    txtMJayeze.Text = 0
                    txtMTakhfif.Text = 0
                    txtMJayeze.Enabled = True
                    txtMTakhfif.Enabled = True
                End If
            End If
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->txtCodeMoshtary_KeyPress")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->txtCodeMoshtary_KeyPress")
        End Try
    End Sub
    Private Sub txtShomarehFaktor_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtShomarehFaktor.TextChanged
        If cmbsCodeDorehFaktor.SelectedIndex = -1 OrElse txtShomarehFaktor.Text = "" OrElse txtShomarehFaktor.Text = "0" Then
            txtShomarehFaktor.Text = ""
            cmbsCodeDorehFaktor.SelectedValue = 0
            cmbForoshandeh.SelectedIndex = -1
            cmbForoshandeh.SelectedIndex = -1

            Exit Sub
        End If

        Dim strSQL As String = "Select ccMoshtary,ccForoshandeh,ccFaktorTitr,CodeDoreh,FaktorShomareh from qryFO_Faktor Where "
        strSQL &= "FaktorShomareh = " & Val(txtShomarehFaktor.Text) & " AND CodeDoreh= " & cmbsCodeDorehFaktor.SelectedValue & " AND CodeMahal = " & CodeMahalFaal

        Dim daSQL As SqlDataAdapter
        If dsForm.Tables.Contains("tblFaktor") Then
            dsForm.Tables.Remove("tblFaktor")
        End If
        daSQL = New SqlDataAdapter(strSQL, ConnectionString)
        daSQL.Fill(dsForm, "tblFaktor")

        If dsForm.Tables("tblFaktor").Rows.Count <> 0 Then
            ccMoshtary = dsForm.Tables("tblFaktor").Rows(0)("ccMoshtary")
            ccForoshandeh = dsForm.Tables("tblFaktor").Rows(0)("ccForoshandeh")
            ccFaktorTitr = dsForm.Tables("tblFaktor").Rows(0)("ccFaktorTitr")
            CodeDorehFaktor = dsForm.Tables("tblFaktor").Rows(0)("CodeDoreh")
            FaktorShomareh = dsForm.Tables("tblFaktor").Rows(0)("FaktorShomareh")
            GetccForoshandeh(ccForoshandeh)
        Else
            ccMoshtary = 0
            ccForoshandeh = 0
            ccFaktorTitr = 0
            CodeDorehFaktor = 0
            FaktorShomareh = 0
            GetccForoshandeh(ccForoshandeh)
        End If

        If objTools.ConvertNulls(objTools.DLookup("IsTakhfifDasty", "tblFO_Faktor", "ccFaktorTitr = " & ccFaktorTitr), False) = False Then
            txtMJayeze.Text = 0
            txtMTakhfif.Text = 0
            txtMJayeze.Enabled = False
            txtMTakhfif.Enabled = False
        Else
            txtMJayeze.Text = 0
            txtMTakhfif.Text = 0
            txtMJayeze.Enabled = True
            txtMTakhfif.Enabled = True
        End If
    End Sub
    Private Sub GetccForoshandeh(ByVal ccForoshandeh As Integer)
        If ccForoshandeh = 0 Then
            cmbForoshandeh.SelectedIndex = -1
            cmbForoshandeh.SelectedIndex = -1
            cmbForoshandeh.Enabled = True
        Else
            cmbForoshandeh.SelectedValue = ccForoshandeh
            cmbForoshandeh.Enabled = False
        End If
    End Sub
    Private Sub txtCodeKala_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtCodeKala.KeyPress
        Try
            If (Asc(e.KeyChar()) < 48 Or Asc(e.KeyChar()) > 57) And (Asc(e.KeyChar()) <> 8) Then
                e.Handled = True
            End If
            If e.KeyChar = Chr(Keys.Space) Then
                If Not AllowMarjoeeWithoutFaktor Then
                    If GridEXElamMarjoee.CurrentRow.Cells("ccFaktorTitr").Text.Replace(",", "") Is DBNull.Value Then
                        MsgBox("برای این مرجوعی فاکتور انتخاب نشده است و امکان انتخاب کالا وجود ندارد.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "پیام")
                        Exit Sub
                    End If
                End If

                Dim objKala As New Forms_dll.frmAN_KalaSearch
                Dim StrSql As String

                If AllowMarjoeeWithoutFaktor Then
                    If GridEXElamMarjoee.CurrentRow.Cells("ccFaktorTitr").Text.Replace(",", "") Is DBNull.Value OrElse Val(GridEXElamMarjoee.CurrentRow.Cells("ccFaktorTitr").Text.Replace(",", "")) = 0 Then
                        StrSql = "Select  CodeKala,NameKala,ccKala,txtsVahedeShomaresh,sVahedeShomaresh"
                        StrSql &= ",NameBrand,Radif,0 as IsSabadKala from qryAN_Kala Where Faal = 1 "
                        StrSql &= " union all select CAST(CodeSabad AS NVARCHAR) AS CodeSabad,NameSabadKala ,ccSabadKala,'',0,'','', 1 as IsSabadKala from Sales.SabadKala Where Faal = 1"

                    Else
                        'StrSql = "Select  CodeKala,NameKala,ccKala,txtsVahedeShomaresh,sVahedeShomaresh"
                        'StrSql &= ",NameBrand,Radif,0 as IsSabadKala from qryAN_Kala Where "
                        'StrSql &= " ccKala in (Select ccKala from tblFO_FaktorSatr where ccFaktorTitr = " & dvTitr(cmTitr.Position)("ccFaktorTitr") & ")"
                        'StrSql &= " union all select CodeSabad,NameSabadKala ,ccSabadKala,'',0,'','', 1 as IsSabadKala from Sales.SabadKala Where Faal = 1"

                        StrSql = "SELECT  CodeKala,NameKala,ccKala,txtsVahedeShomaresh,sVahedeShomaresh "
                        StrSql &= " ,NameBrand,Radif,0 AS IsSabadKala FROM qryAN_Kala "
                        StrSql &= " WHERE ccKala IN (SELECT ccKala FROM tblFO_FaktorSatr WHERE ccFaktorTitr = " & Val(GridEXElamMarjoee.CurrentRow.Cells("ccFaktorTitr").Text.Replace(",", "")) & " AND IsSabadKala = 0 AND IsJayezeh = 0)"
                        StrSql &= " UNION ALL "
                        StrSql &= "  SELECT CodeSabad,NameSabadKala ,ccSabadKala,'پـک',0,'','', 1 "
                        StrSql &= " FROM (SELECT CAST(CodeSabad AS NVARCHAR) AS CodeSabad,NameSabadKala ,c.ccSabadKala FROM qryFO_FaktorPrint AS a WITH(NOLOCK) LEFT OUTER JOIN"
                        StrSql &= " Sales.SabadKalaSatr AS b WITH(NOLOCK) ON a.ccSabadKalaSatr = b.ccSabadKalaSatr LEFT OUTER JOIN"
                        StrSql &= " Sales.SabadKala AS c WITH(NOLOCK) ON b.ccSabadKala = c.ccSabadKala"
                        StrSql &= " WHERE ccFaktorTitr = " & Val(GridEXElamMarjoee.CurrentRow.Cells("ccFaktorTitr").Text.Replace(",", "")) & " And IsSabadKala = 1"
                        StrSql &= " GROUP BY CodeSabad,NameSabadKala ,c.ccSabadKala) AS tbl"

                    End If
                Else
                    'StrSql = "Select  CodeKala,NameKala,ccKala,txtsVahedeShomaresh,sVahedeShomaresh"
                    'StrSql &= ",NameBrand,Radif,0 as IsSabadKala FROM qryAN_Kala Where "
                    'StrSql &= " ccKala in (Select ccKala from tblFO_FaktorSatr where ccFaktorTitr = " & dvTitr(cmTitr.Position)("ccFaktorTitr") & ")"
                    'StrSql &= " union all select CodeSabad,NameSabadKala ,ccSabadKala,'',0,'','', 1 as IsSabadKala from Sales.SabadKala Where Faal = 1"

                    StrSql = "SELECT  CodeKala,NameKala,ccKala,txtsVahedeShomaresh,sVahedeShomaresh "
                    StrSql &= " ,NameBrand,Radif,0 AS IsSabadKala FROM qryAN_Kala "
                    StrSql &= " WHERE ccKala IN (SELECT ccKala FROM tblFO_FaktorSatr WHERE ccFaktorTitr = " & Val(GridEXElamMarjoee.CurrentRow.Cells("ccFaktorTitr").Text.Replace(",", "")) & " AND IsSabadKala = 0 AND IsJayezeh = 0)"
                    StrSql &= " UNION ALL "
                    StrSql &= "  SELECT CodeSabad,NameSabadKala ,ccSabadKala,'پـک',0,'','', 1 "
                    StrSql &= " FROM (SELECT CAST(CodeSabad AS NVARCHAR) AS CodeSabad,NameSabadKala ,c.ccSabadKala FROM qryFO_FaktorPrint AS a WITH(NOLOCK) LEFT OUTER JOIN"
                    StrSql &= " Sales.SabadKalaSatr AS b WITH(NOLOCK) ON a.ccSabadKalaSatr = b.ccSabadKalaSatr LEFT OUTER JOIN"
                    StrSql &= " Sales.SabadKala AS c WITH(NOLOCK) ON b.ccSabadKala = c.ccSabadKala"
                    StrSql &= " WHERE ccFaktorTitr = " & Val(GridEXElamMarjoee.CurrentRow.Cells("ccFaktorTitr").Text.Replace(",", "")) & " And IsSabadKala = 1"
                    StrSql &= " GROUP BY CodeSabad,NameSabadKala ,c.ccSabadKala) AS tbl"
                End If


                If txtCodeKala.Text.Length <> 0 Then
                    objKala.tcodeKala = txtCodeKala.Text
                End If

                MultiSelection = False
                SearchItem = "CodeKala"
                objKala.SetForm(StrSql)
                objKala.ShowDialog()
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

    Private Sub txtCodeKala_TextChanged(sender As Object, e As EventArgs) Handles txtCodeKala.TextChanged
        If Mode = UD_Dll.Enums.GL_ModeForms.UpdateRecord Then Exit Sub
        If Trim(Me.txtCodeKala.Text) = "" Then Exit Sub
        If Not flg Then Exit Sub
        Try
            'If dvTitr(cmTitr.Position)("ccFaktorTitr") Is DBNull.Value Then

            'Dim MablaghBedoneFaktor As Double = 0

            'Dim cm As New SqlCommand
            'cm.CommandText = "Select ISNULL(dbo.GetMablaghFrosh (" & _
            '    txtCodeKala.Tag & "," & _
            '    "'" & TarikhEmrooz & "'," & _
            '    CodeMahalFaal & "," & _
            '    dvTitr(cmTitr.Position)("ccMoshtary") & "," & _
            '    dvTitr(cmTitr.Position)("sNoeMoshtary") & "),0)"

            'cm.Connection = New SqlConnection(ConnectionString)
            'cm.Connection.Open()
            'MablaghBedoneFaktor = cm.ExecuteScalar

            'Me.txtFee.Text = MablaghBedoneFaktor

            'Exit Sub
            'End If

            Dim Mablagh As Double = 0
            If GridEXElamMarjoee.CurrentRow.Cells("ccFaktorTitr").Text.Replace(",", "") IsNot DBNull.Value AndAlso Val(GridEXElamMarjoee.CurrentRow.Cells("ccFaktorTitr").Text.Replace(",", "")) <> 0 Then
                Me.lblNameKala.Text = objTools.ConvertNulls(objTools.DLookup("NameKala", "tblAN_Kala", "CodeKala=" & Me.txtCodeKala.Text), "")
                Me.txtCodeKala.Tag = objTools.ConvertNulls(objTools.DLookup("ccKala", "tblAn_Kala", "CodeKala=" & Me.txtCodeKala.Text), 0)

                Mablagh = objTools.ConvertNulls(objTools.DLookup("Fee", "qryFO_FaktorTitrSatr", "ccFaktorTitr = " & Val(GridEXElamMarjoee.CurrentRow.Cells("ccFaktorTitr").Text.Replace(",", "")) & _
                    " AND ccKala =" & txtCodeKala.Tag & " AND IsJayezeh = 0"), 0)

            Else
                'Me.lblNameKala.Text = objTools.ConvertNulls(objTools.DLookup("NameKala", "tblAN_Kala", "CodeKala=" & Me.txtCodeKala.Text & _
                '    " AND ccKala in (Select ccKala from tblFO_FaktorSatr where ccFaktorTitr = " & dvTitr(cmTitr.Position)("ccFaktorTitr") & ")"), "")
                'Me.txtCodeKala.Tag = objTools.ConvertNulls(objTools.DLookup("ccKala", "tblAn_Kala", "CodeKala=" & Me.txtCodeKala.Text), 0)

                Me.lblNameKala.Text = objTools.ConvertNulls(objTools.DLookup("NameKala", "tblAN_Kala", "CodeKala=" & Me.txtCodeKala.Text), "")
                Me.txtCodeKala.Tag = objTools.ConvertNulls(objTools.DLookup("ccKala", "tblAn_Kala", "CodeKala=" & Me.txtCodeKala.Text), 0)

                Dim cm As New SqlCommand
                cm.CommandText = "Select ISNULL(dbo.GetMablaghFrosh (" & _
                    txtCodeKala.Tag & "," & _
                    "'" & TarikhEmrooz & "'," & _
                    CodeMahalFaal & "," & _
                    Val(GridEXElamMarjoee.CurrentRow.Cells("ccMoshtary").Text.Replace(",", "")) & "," & _
                    Val(GridEXElamMarjoee.CurrentRow.Cells("sNoeMoshtary").Text.Replace(",", "")) & "),0)"

                cm.Connection = New SqlConnection(ConnectionString)
                cm.Connection.Open()
                Mablagh = cm.ExecuteScalar

            End If
            Me.txtFee.Text = Mablagh

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->txtCodeKala_TextChanged")
        End Try
    End Sub
    Private Sub GridEXElamMarjoee_Click(sender As Object, e As EventArgs) Handles GridEXElamMarjoee.Click
        BoundCurrencyManagerElamMarjoee()
    End Sub
    Private Sub GridEXElamMarjoee_Kartabl_Click(sender As Object, e As EventArgs) Handles GridEXElamMarjoee_Kartabl.Click
        BoundCurrencyManagerElamMarjoee_Kartabl()
    End Sub
    Private Sub GridEXMarjoeeAzMoshtary_Click(sender As Object, e As EventArgs) Handles GridEXMarjoeeAzMoshtary.Click
        BoundCurrencyManagerMarjoeeAzMoshtary()
    End Sub
    Private Sub BoundCurrencyManagerElamMarjoee()
        cmElamMarjoee = CType(BindingContext(GridEXElamMarjoee.DataSource), CurrencyManager)
        AddHandler cmElamMarjoee.PositionChanged, AddressOf cmElamMarjoee_PositionChanged
        SearchSatr(tbMain.SelectedIndex)
    End Sub
    Private Sub cmElamMarjoee_PositionChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        SearchSatr(tbMain.SelectedIndex)
    End Sub
    Private Sub BoundCurrencyManagerElamMarjoee_Kartabl()
        cmElamMarjoee_Kartabl = CType(BindingContext(GridEXElamMarjoee_Kartabl.DataSource), CurrencyManager)
        AddHandler cmElamMarjoee_Kartabl.PositionChanged, AddressOf cmElamMarjoee_Kartabl_PositionChanged
        SearchSatr(tbMain.SelectedIndex)
    End Sub
    Private Sub cmElamMarjoee_Kartabl_PositionChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        SearchSatr(tbMain.SelectedIndex)
    End Sub
    Private Sub BoundCurrencyManagerMarjoeeAzMoshtary()
        cmMarjoeeAzMoshtary = CType(BindingContext(GridEXMarjoeeAzMoshtary.DataSource), CurrencyManager)
        AddHandler cmMarjoeeAzMoshtary.PositionChanged, AddressOf cmMarjoeeAzMoshtary_PositionChanged
        SearchSatr(tbMain.SelectedIndex)
    End Sub
    Private Sub cmMarjoeeAzMoshtary_PositionChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        SearchSatr(tbMain.SelectedIndex)
    End Sub
    Private Sub btnExit1_Click(sender As Object, e As EventArgs) Handles btnExit1.Click
        Me.Close()
    End Sub
    Private Sub btnExit2_Click(sender As Object, e As EventArgs) Handles btnExit2.Click
        Me.Close()
    End Sub
    Private Sub btnExit3_Click(sender As Object, e As EventArgs) Handles btnExit3.Click
        Me.Close()
    End Sub
    Private Sub chkFaktorMarjoee_CheckedChanged(sender As Object, e As EventArgs) Handles chkFaktorMarjoee.CheckedChanged
        If chkFaktorMarjoee.Checked = False Then
            cmbElatFaktorMarjoee.Visible = False
            lblElatFaktorMarjoee.Visible = False
        Else
            cmbElatFaktorMarjoee.Visible = True
            lblElatFaktorMarjoee.Visible = True
            cmbElatFaktorMarjoee.SelectedIndex = -1
            cmbElatFaktorMarjoee.SelectedIndex = -1
        End If
    End Sub

    Private Sub btnReturnToKartabl_Click(sender As Object, e As EventArgs) Handles btnReturnToKartabl.Click
        If MsgBox("آیا به انتخاب خود اطمینان دارید ؟", MsgBoxStyle.Question + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.YesNo, "") = MsgBoxResult.No Then
            Exit Sub
        End If

        Dim PK As Integer = 0
        Dim Sh_Marjoee As Integer = 0

        For i As Integer = 0 To GridEXMarjoeeAzMoshtary.RowCount - 1
            If GridEXMarjoeeAzMoshtary.GetRows(i).Cells("Taeed").Value = True Then
                PK = Val(GridEXMarjoeeAzMoshtary.GetRows(i).Cells("ccElamMarjoee").Text.Replace(",", ""))
                objTools.DUpdate("sVazeiat", "tblFO_ElamMarjoee", "100", "ccElamMarjoee = " & PK)
            End If
        Next

        Search(tbMain.SelectedIndex)
    End Sub

    Private Sub btnSodorMarjoeeAzMoshtary_Click(sender As Object, e As EventArgs) Handles btnSodorMarjoeeAzMoshtary.Click
        If MsgBox("آیا به انتخاب خود اطمینان دارید ؟", MsgBoxStyle.Question + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.YesNo, "") = MsgBoxResult.No Then
            Exit Sub
        End If

        Dim PK As Integer = 0
        Dim Sh_MarjoeeTaeedShodeh As String = ""


        For i As Integer = 0 To GridEXMarjoeeAzMoshtary.RowCount - 1
            If GridEXMarjoeeAzMoshtary.GetRows(i).Cells("Taeed").Value = True Then
                PK = Val(GridEXMarjoeeAzMoshtary.GetRows(i).Cells("ccElamMarjoee").Text.Replace(",", ""))
                If SodorMarjoeeAzMoshtary(PK) = True Then
                    Sh_MarjoeeTaeedShodeh &= "," & GridEXMarjoeeAzMoshtary.GetRows(i).Cells("ccElamMarjoee").Text.Replace(",", "").Replace(".00", "")
                End If
            End If
        Next

        If Sh_MarjoeeTaeedShodeh <> "" Then
            Sh_MarjoeeTaeedShodeh &= ","
            SodorPishFaktorEzafeBarAzMarjoee(Sh_MarjoeeTaeedShodeh)
            If ShomarehPishFaktor <> 0 Then
                MsgBox("شماره پیش فاکتور " & ShomarehPishFaktor & " به عنوان پیش فاکتور اضافه با صادر گردید و به برگه تفکیک افزوده شد . ", MsgBoxStyle.Information + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.OkOnly, "")
            End If
        End If

        Search(tbMain.SelectedIndex)
    End Sub
    Private Function SodorMarjoeeAzMoshtary(ByVal ccMarjoee As Integer) As Boolean
        SodorMarjoeeAzMoshtary = False

        Dim cnSQL As New SqlConnection
        Dim cmSQL As New SqlCommand
        Dim strSQL As String = ""

        Try
            strSQL = "Sales.spPishFaktorGheireGhateei_ElamMarjoee_SodorMarjoeeAzMoshtary"

            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            cmSQL.Parameters.AddWithValue("ccTafkik_GG", ccTafkik_GG)
            cmSQL.Parameters.AddWithValue("ccMarjoee", ccMarjoee)
            cmSQL.Parameters.AddWithValue("CodeDoreh", CodeDoreh)
            cmSQL.Parameters.AddWithValue("CodeMahal", CodeMahalFaal)
            cmSQL.Parameters.AddWithValue("UserName", UserName)
            cmSQL.Parameters.AddWithValue("Tarikh", TarikhEmrooz)
            cmSQL.Parameters.AddWithValue("Saat", Format(TimeOfDay, "HH:mm:ss"))
            cmSQL.Parameters.AddWithValue("CodeSherkat", CodeSherkat)
            cmSQL.Parameters.AddWithValue("Inserted", SodorMarjoeeAzMoshtary)
            cmSQL.Parameters("Inserted").Direction = ParameterDirection.Output

            cmSQL.ExecuteNonQuery()

            SodorMarjoeeAzMoshtary = cmSQL.Parameters("Inserted").Value

            cmSQL = Nothing
            cnSQL.Close()

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> SodorMarjoeeAzMoshtary ")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> SodorMarjoeeAzMoshtary ")
        End Try
    End Function
    Private Sub SodorPishFaktorEzafeBarAzMarjoee(ByVal strMarjoee As String)
        Dim cnSQL As New SqlConnection
        Dim cmSQL As New SqlCommand
        Dim strSQL As String = ""

        ShomarehPishFaktor = 0

        Try
            strSQL = "Sales.spPishFaktorGheireGhateei_ElamMarjoee_SodorPishFaktorEzafeBarAzMarjoee"

            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            cmSQL.Parameters.AddWithValue("ccTafkik_GG", ccTafkik_GG)
            cmSQL.Parameters.AddWithValue("strMarjoee", strMarjoee)
            cmSQL.Parameters.AddWithValue("CodeMahal", CodeMahalFaal)
            cmSQL.Parameters.AddWithValue("CodeDoreh", CodeDoreh)
            cmSQL.Parameters.AddWithValue("UserName", UserName)
            cmSQL.Parameters.AddWithValue("Tarikh", TarikhEmrooz)
            cmSQL.Parameters.AddWithValue("Saat", Format(TimeOfDay, "HH:mm:ss"))
            cmSQL.Parameters.AddWithValue("ShomarehPishFaktor", ShomarehPishFaktor)
            cmSQL.Parameters("ShomarehPishFaktor").Direction = ParameterDirection.Output

            cmSQL.ExecuteNonQuery()

            ShomarehPishFaktor = cmSQL.Parameters("ShomarehPishFaktor").Value

            cmSQL = Nothing
            cnSQL.Close()

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> SodorPishFaktorEzafeBarAzMarjoee ")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> SodorPishFaktorEzafeBarAzMarjoee ")
        End Try
    End Sub
End Class