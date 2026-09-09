Public Class frmFO_PishFaktor_GheireGhateei
#Region "Variable AND Constant Declration"
    Const cntCodeSubSystem As Long = 1000126
    Dim Mode As UD_Dll.Enums.GL_ModeForms = UD_Dll.Enums.GL_ModeForms.None
    Dim cmTafkik_Eslah, cmTafkik_Sodor, cmPishFaktor_Kala_Eslah, cmPishFaktor_Kala_Sodor As CurrencyManager
    Dim dvPishFaktor, dvTafkik_Eslah, dvTafkik_Sodor, dvPishFaktor_Eslah, dvPishFaktor_Sodor, dvPishFaktor_Kala_Eslah, dvPishFaktor_Kala_Sodor As DataView
    Dim ErrPro As New ErrorProvider
    Dim dsForm As New DataSet
    Dim dvChecked As DataView
    Private SN As Integer
    Dim tPos As Integer
    Dim txtCaption As String
    Private WithEvents BS As New UD_Dll.PassString
    Dim FlgSearchKala As Boolean = False
    Dim FlgSearchPishFaktor_Sodor As Boolean = False
    Dim flgShowVaznMashin As Boolean = False
    Dim AllowChangeFeePishFaktor As Boolean = 0
#End Region

    Private Sub frmFO_PishFaktor_GheireGhateei_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetParameter()
        SN = objSec.GetSecurityNumber(cntCodeSubSystem, UserName)
        LoadCombo()
        SetForm()

        cmbVazeiat_Eslah.SelectedIndex = 0
        cmbAnbar.SelectedValue = objTools.ConvertNulls(objTools.DLookup("CodeAnbar", "tblAN_Anbar", "AnbarAsly = 1 AND Faal = 1 AND CodeMahal = " & CodeMahalFaal), 0)

        SearchPishFaktor()
        SearchTafkik_Eslah(cmbVazeiat_Eslah.SelectedIndex)
        SearchTafkik_Sodor()

        FlgSearchKala = True
        FlgSearchPishFaktor_Sodor = True
        flgShowVaznMashin = True

        AllowChangeFeePishFaktor = objTools.DLookup("AllowChangeFeePishFaktor", "tblGL_SysConfig", "CodeMahal = " & CodeMahalFaal)
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
            txtCaption = "پیش فاکتور غیر قطعی"
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

        Dim Strsql As String
        Dim cnSQL As New SqlConnection
        Dim cmSQL As New SqlCommand
        Dim p As New SqlParameter
        Dim daSQL As SqlDataAdapter
        Dim dr As DataRow

        cnSQL = New SqlConnection(ConnectionString)
        cnSQL.Open()

        '----- Load Combo tblAnbar
        Strsql = "Global.spAnbarSalem_LoadCombo"

        cmSQL = New SqlCommand(Strsql, cnSQL)
        cmSQL.CommandType = CommandType.StoredProcedure
        cmSQL.Parameters.Clear()

        cmSQL.Parameters.AddWithValue("CodeMahal", CodeMahalFaal)
        cmSQL.Parameters.AddWithValue("UserName", UserName)

        daSQL = New SqlDataAdapter(cmSQL)
        daSQL.Fill(dsForm, "tblAnbar")
        Dim d As DataRow
        d = dsForm.Tables("tblAnbar").NewRow
        d("NameAnbar") = "همه"
        d("codeAnbar") = 0
        dsForm.Tables("tblAnbar").Rows.Add(d)

        cmbAnbar.DataSource = Nothing
        cmbAnbar.Items.Clear()
        cmbAnbar.DataSource = dsForm.Tables("tblAnbar").DefaultView
        cmbAnbar.DisplayMember = "NameAnbar"
        cmbAnbar.ValueMember = "codeAnbar"

        daSQL = Nothing

        '----- Load Combo Foroshandeh
        Strsql = "Global.spForoshandeh_LoadCombo"

        cmSQL = New SqlCommand(Strsql, cnSQL)
        cmSQL.CommandType = CommandType.StoredProcedure
        cmSQL.Parameters.Clear()

        cmSQL.Parameters.AddWithValue("CodeMahal", CodeMahalFaal)
        cmSQL.Parameters.AddWithValue("sVazeiat", UD_Dll.Enums.FO_VaziatForoshandeh.NoFaal)
        cmSQL.Parameters.AddWithValue("UserName", UserName)

        daSQL = New SqlDataAdapter(cmSQL)
        daSQL.Fill(dsForm, "tblForoshandeh")

        dr = dsForm.Tables("tblForoshandeh").NewRow()
        dr("ccForoshandeh") = 0
        dr("NameForoshandeh") = "همه"
        dsForm.Tables("tblForoshandeh").Rows.Add(dr)

        cmbForoshandehS.DataSource = Nothing
        cmbForoshandehS.Items.Clear()
        cmbForoshandehS.DataSource = dsForm.Tables("tblForoshandeh").DefaultView
        cmbForoshandehS.DisplayMember = "NameForoshandeh"
        cmbForoshandehS.ValueMember = "ccForoshandeh"

        daSQL = Nothing

        '-----  Load Combo Mamor Pakhsh
        Strsql = "Global.spMamorPakhsh_LoadCombo"

        cmSQL = New SqlCommand(Strsql, cnSQL)
        cmSQL.CommandType = CommandType.StoredProcedure
        cmSQL.Parameters.Clear()

        cmSQL.Parameters.AddWithValue("CodeMahal", CodeMahalFaal)
        cmSQL.Parameters.AddWithValue("sSemat", "," & UD_Dll.Enums.GL_Semat.MamorPakhsh & "," & UD_Dll.Enums.GL_Semat.Ranandeh & "," & UD_Dll.Enums.GL_Semat.Foroshandeh_Sayar & ",")

        daSQL = New SqlDataAdapter(cmSQL)
        daSQL.Fill(dsForm, "tblMamorPakhsh")
        daSQL.Fill(dsForm, "tblMamorPakhshS")

        cmbMamorPakhsh.DataSource = Nothing
        cmbMamorPakhsh.Items.Clear()
        cmbMamorPakhsh.DataSource = dsForm.Tables("tblMamorPakhsh").DefaultView
        cmbMamorPakhsh.DisplayMember = "FN"
        cmbMamorPakhsh.ValueMember = "CodeFard"

        dr = dsForm.Tables("tblMamorPakhshS").NewRow()
        dr("CodeFard") = 0
        dr("FN") = "همه"
        dsForm.Tables("tblMamorPakhshS").Rows.Add(dr)

        cmbMamorPakhshS_Eslsh.DataSource = Nothing
        cmbMamorPakhshS_Eslsh.Items.Clear()
        cmbMamorPakhshS_Eslsh.DataSource = dsForm.Tables("tblMamorPakhshS").DefaultView
        cmbMamorPakhshS_Eslsh.DisplayMember = "FN"
        cmbMamorPakhshS_Eslsh.ValueMember = "CodeFard"

        cmbMamorPakhshS_Sodor.DataSource = Nothing
        cmbMamorPakhshS_Sodor.Items.Clear()
        cmbMamorPakhshS_Sodor.DataSource = dsForm.Tables("tblMamorPakhshS").DefaultView
        cmbMamorPakhshS_Sodor.DisplayMember = "FN"
        cmbMamorPakhshS_Sodor.ValueMember = "CodeFard"

        daSQL = Nothing

        '-----  Load Combo Mashin Tozie
        Strsql = "Global.spMashin_Tozie_LoadCombo"

        cmSQL = New SqlCommand(Strsql, cnSQL)
        cmSQL.CommandType = CommandType.StoredProcedure
        cmSQL.Parameters.Clear()

        cmSQL.Parameters.AddWithValue("CodeMahal", CodeMahalFaal)

        daSQL = New SqlDataAdapter(cmSQL)
        daSQL.Fill(dsForm, "tblMashinTozie")

        cmbMashinTozie.DataSource = Nothing
        cmbMashinTozie.Items.Clear()
        cmbMashinTozie.DataSource = dsForm.Tables("tblMashinTozie").DefaultView
        cmbMashinTozie.DisplayMember = "MashinFull"
        cmbMashinTozie.ValueMember = "ccMashin"

        daSQL = Nothing

        '-----  Load Combo Ranandeh Tozie
        Strsql = "Global.spRanandeh_Haml_Tozie_LoadCombo"

        cmSQL = New SqlCommand(Strsql, cnSQL)
        cmSQL.CommandType = CommandType.StoredProcedure
        cmSQL.Parameters.Clear()

        cmSQL.Parameters.AddWithValue("CodeMahal", CodeMahalFaal)
        cmSQL.Parameters.AddWithValue("sSemat", "," & UD_Dll.Enums.GL_Semat.MamorPakhsh & "," & UD_Dll.Enums.GL_Semat.Ranandeh & "," & UD_Dll.Enums.GL_Semat.Foroshandeh_Sayar & ",")

        daSQL = New SqlDataAdapter(cmSQL)
        daSQL.Fill(dsForm, "tblRanandehTozie")

        cmbRanandehTozie.DataSource = Nothing
        cmbRanandehTozie.Items.Clear()
        cmbRanandehTozie.DataSource = dsForm.Tables("tblRanandehTozie").DefaultView
        cmbRanandehTozie.DisplayMember = "FN"
        cmbRanandehTozie.ValueMember = "CodeFard_Ranandeh"

        '-----  Load Combo Vazeiat Sodor
        cmbVazeiat_Eslah.Items.Add("بدون وضعیت")
        cmbVazeiat_Eslah.Items.Add("ارسال شـده")

        cmSQL = Nothing : daSQL = Nothing
        cnSQL.Close()

    End Sub
    Private Sub SetForm()
        Dim CodeAnbarAsly As Integer = 0
        CodeAnbarAsly = objTools.DLookupOne("CodeAnbar", "tblAN_Anbar", "Faal = 1 AND AnbarAsly = 1 AND CodeMahal = " & CodeMahalFaal, "CodeAnbar ASC")
        cmbAnbar.SelectedValue = CodeAnbarAsly
        cmbAnbar.SelectedValue = CodeAnbarAsly

        cmbForoshandehS.SelectedValue = 0
        cmbMamorPakhshS_Eslsh.SelectedValue = 0
        cmbVazeiat_Eslah.SelectedValue = 0
        cmbMamorPakhshS_Sodor.SelectedIndex = 0
        cmbMamorPakhshS_Sodor.SelectedIndex = 0

        mskTarikhPishbiniErsal.Text = TarikhEmrooz
        mskAzTarikh.Text = objTarikh.Mi2Sh((Now.AddDays(-31)))
        mskTaTarikh.Text = TarikhEmrooz
        mskAzTarikh_Eslah.Text = objTarikh.Mi2Sh((Now.AddDays(-31)))
        mskTaTarikh_Eslah.Text = TarikhEmrooz
        mskAzTarikh_Sodor.Text = objTarikh.Mi2Sh((Now.AddDays(-31)))
        mskTaTarikh_Sodor.Text = TarikhEmrooz

        FlgSearchKala = False
        FlgSearchPishFaktor_Sodor = False
    End Sub
    Private Sub SearchPishFaktor()
        Dim cnSQL As New SqlConnection
        Dim cmSQL As New SqlCommand
        Dim daSQl As New SqlDataAdapter
        Dim strSQL As String = ""

        If IsValidSearchPishFaktor() = False Then
            Exit Sub
        End If

        If dsForm.Tables.Contains("tbl_PishFaktor") Then
            dsForm.Tables.Remove("tbl_PishFaktor")
        End If

        Try
            strSQL = "Sales.spPishFaktorGheireGhateei_SearchPishFaktor "

            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            cmSQL.Parameters.AddWithValue("CodeMahal", CodeMahalFaal)
            cmSQL.Parameters.AddWithValue("CodeDoreh", CodeDoreh)
            cmSQL.Parameters.AddWithValue("ccForoshandeh", cmbForoshandehS.SelectedValue)
            cmSQL.Parameters.AddWithValue("ccAnbar", cmbAnbar.SelectedValue)
            cmSQL.Parameters.AddWithValue("AzTarikh", mskAzTarikh.Text)
            cmSQL.Parameters.AddWithValue("TaTarikh", mskTaTarikh.Text)
            cmSQL.Parameters.AddWithValue("UserName", UserName)

            daSQl = New SqlDataAdapter(cmSQL)
            daSQl.Fill(dsForm, "tbl_PishFaktor")

            '------------Adding Columns------------
            dsForm.Tables("tbl_PishFaktor").Columns.Add("Taeed", GetType(Boolean))
            '--------------------------------------

            Dim dr As DataRow
            For Each dr In dsForm.Tables("tbl_PishFaktor").Rows
                dr("Taeed") = False
            Next

            dvPishFaktor = New DataView(dsForm.Tables("tbl_PishFaktor"))
            dvPishFaktor.Sort = "PishFaktorShomareh DESC"

            dvPishFaktor.AllowNew = False
            dvPishFaktor.AllowDelete = False
            dvPishFaktor.AllowEdit = True

            cmSQL = Nothing : daSQl = Nothing
            cnSQL.Close()

            SetGridPishFaktor()
            With GridEXPishFaktor
                .Visible = True
                .DataSource = Nothing
                .DataSource = dvPishFaktor
            End With

            Me.CenterToScreen()
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> SearchPishFaktor ")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> SearchPishFaktor ")
        End Try
    End Sub
    Private Function IsValidSearchPishFaktor() As Boolean
        IsValidSearchPishFaktor = False

        If Len(mskAzTarikh.Text.ToString) <> 0 Then
            If Not objTarikh.IsShDate(mskAzTarikh.Text.ToString) Then
                mskAzTarikh.Focus()
                Exit Function
            End If
            'If Microsoft.VisualBasic.Left(mskAzTarikh.Text, 4) <> mdlPublic.CodeDoreh Then
            '    MsgBox("از تاريخ وارد شده با دوره انتخاب شده مغايرت دارد.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Critical, "خطا")
            '    ErrPro.SetError(mskAzTarikh, "از تاريخ وارد شده با دوره انتخاب شده مغايرت دارد.")
            '    Exit Function
            'End If
        Else
            ErrPro.SetError(Me.mskAzTarikh, " از تاريخ را وارد نمایید.")
            MsgBox(" از تاریخ را وارد نمایید.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
            mskAzTarikh.Focus()
            Exit Function
        End If
        ErrPro.SetError(Me.mskAzTarikh, "")

        If Len(mskTaTarikh.Text.ToString) <> 0 Then
            If Not objTarikh.IsShDate(mskTaTarikh.Text.ToString) Then
                mskTaTarikh.Focus()
                Exit Function
            End If
            If Microsoft.VisualBasic.Left(mskTaTarikh.Text, 4) <> mdlPublic.CodeDoreh Then
                MsgBox("تا تاريخ وارد شده با دوره انتخاب شده مغايرت دارد.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Critical, "خطا")
                ErrPro.SetError(mskTaTarikh, "تا تاريخ وارد شده با دوره انتخاب شده مغايرت دارد.")
                Exit Function
            End If
        Else
            ErrPro.SetError(Me.mskTaTarikh, " تا تاريخ را وارد نمایید.")
            MsgBox(" تا تاریخ را وارد نمایید.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
            mskTaTarikh.Focus()
            Exit Function
        End If
        ErrPro.SetError(Me.mskTaTarikh, "")

        IsValidSearchPishFaktor = True
    End Function
    Private Sub SearchTafkik_Eslah(ByVal sVazeiat As Integer)
        '' sVazeiat ----> 0 = Bedone Vazeiat , 1 = Ersal Shodeh 
        Dim cnSQL As New SqlConnection
        Dim cmSQL As New SqlCommand
        Dim daSQl As New SqlDataAdapter
        Dim strSQL As String = ""

        If IsValidSearchTafkik_Eslah() = False Then
            Exit Sub
        End If

        If dsForm.Tables.Contains("tbl_Tafkik_Eslah") Then
            dsForm.Tables.Remove("tbl_Tafkik_Eslah")
        End If

        Try
            strSQL = "Sales.spPishFaktorGheireGhateei_SearchTafkik_Eslah "

            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            cmSQL.Parameters.AddWithValue("CodeMahal", CodeMahalFaal)
            cmSQL.Parameters.AddWithValue("CodeDoreh", CodeDoreh)
            cmSQL.Parameters.AddWithValue("ccMamorPakhsh", cmbMamorPakhshS_Eslsh.SelectedValue)
            cmSQL.Parameters.AddWithValue("AzTarikh", mskAzTarikh_Eslah.Text)
            cmSQL.Parameters.AddWithValue("TaTarikh", mskTaTarikh_Eslah.Text)
            cmSQL.Parameters.AddWithValue("sVazeiat", cmbVazeiat_Eslah.SelectedIndex)
            cmSQL.Parameters.AddWithValue("UserName", UserName)

            daSQl = New SqlDataAdapter(cmSQL)
            daSQl.Fill(dsForm, "tbl_Tafkik_Eslah")

            '------------Adding Columns------------
            dsForm.Tables("tbl_Tafkik_Eslah").Columns.Add("Taeed", GetType(Boolean))
            '--------------------------------------

            Dim dr As DataRow
            For Each dr In dsForm.Tables("tbl_Tafkik_Eslah").Rows
                dr("Taeed") = False
            Next

            dvTafkik_Eslah = New DataView(dsForm.Tables("tbl_Tafkik_Eslah"))
            dvTafkik_Eslah.Sort = "ShomarehTafkik DESC"

            dvTafkik_Eslah.AllowNew = False
            dvTafkik_Eslah.AllowDelete = False
            dvTafkik_Eslah.AllowEdit = True

            cmSQL = Nothing : daSQl = Nothing
            cnSQL.Close()

            SetGridTafkik_Eslah()
            With GridEXTafkik_Eslah
                .Visible = True
                .DataSource = Nothing
                .DataSource = dvTafkik_Eslah
            End With

            BoundCurrencyManagerTafkik_Eslah()
            Me.CenterToScreen()
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> SearchTafkik_Eslah ")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> SearchTafkik_Eslah ")
        End Try
    End Sub
    Private Function IsValidSearchTafkik_Eslah() As Boolean
        IsValidSearchTafkik_Eslah = False

        If Len(mskAzTarikh_Eslah.Text.ToString) <> 0 Then
            If Not objTarikh.IsShDate(mskAzTarikh_Eslah.Text.ToString) Then
                mskAzTarikh_Eslah.Focus()
                Exit Function
            End If
            'If Microsoft.VisualBasic.Left(mskAzTarikh_Eslah.Text, 4) <> mdlPublic.CodeDoreh Then
            '    MsgBox("از تاريخ وارد شده با دوره انتخاب شده مغايرت دارد.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Critical, "خطا")
            '    ErrPro.SetError(mskAzTarikh_Eslah, "از تاريخ وارد شده با دوره انتخاب شده مغايرت دارد.")
            '    Exit Function
            'End If
        Else
            ErrPro.SetError(Me.mskAzTarikh_Eslah, " از تاريخ را وارد نمایید.")
            MsgBox(" از تاریخ را وارد نمایید.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
            mskAzTarikh_Eslah.Focus()
            Exit Function
        End If
        ErrPro.SetError(Me.mskAzTarikh_Eslah, "")

        If Len(mskTaTarikh_Eslah.Text.ToString) <> 0 Then
            If Not objTarikh.IsShDate(mskTaTarikh_Eslah.Text.ToString) Then
                mskTaTarikh_Eslah.Focus()
                Exit Function
            End If
            If Microsoft.VisualBasic.Left(mskTaTarikh_Eslah.Text, 4) <> mdlPublic.CodeDoreh Then
                MsgBox("تا تاريخ وارد شده با دوره انتخاب شده مغايرت دارد.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Critical, "خطا")
                ErrPro.SetError(mskTaTarikh_Eslah, "تا تاريخ وارد شده با دوره انتخاب شده مغايرت دارد.")
                Exit Function
            End If
        Else
            ErrPro.SetError(Me.mskTaTarikh_Eslah, " تا تاريخ را وارد نمایید.")
            MsgBox(" تا تاریخ را وارد نمایید.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
            mskTaTarikh_Eslah.Focus()
            Exit Function
        End If
        ErrPro.SetError(Me.mskTaTarikh_Eslah, "")

        IsValidSearchTafkik_Eslah = True
    End Function
    Private Sub SearchPishFaktorTafkik_Eslah()
        Dim PK As Integer = 0
        If dvTafkik_Eslah.Count = 0 Then
            PK = 0
        Else
            PK = Val(GridEXTafkik_Eslah.CurrentRow.Cells("ccTafkik_GG").Text.Replace(",", ""))
        End If

        Dim cnSQL As New SqlConnection
        Dim cmSQL As New SqlCommand
        Dim daSQl As New SqlDataAdapter
        Dim strSQL As String = ""

        If dsForm.Tables.Contains("tbl_PishFaktorTafkik_Eslah") Then
            dsForm.Tables.Remove("tbl_PishFaktorTafkik_Eslah")
        End If

        Try
            strSQL = "Sales.spPishFaktorGheireGhateei_SearchPishFaktorTafkik_Eslah "

            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            cmSQL.Parameters.AddWithValue("ccTafkik_GG", PK)

            daSQl = New SqlDataAdapter(cmSQL)
            daSQl.Fill(dsForm, "tbl_PishFaktorTafkik_Eslah")

            dvPishFaktor_Eslah = New DataView(dsForm.Tables("tbl_PishFaktorTafkik_Eslah"))
            dvPishFaktor_Eslah.Sort = "PishFaktorShomareh DESC"

            dvPishFaktor_Eslah.AllowNew = False
            dvPishFaktor_Eslah.AllowDelete = False
            dvPishFaktor_Eslah.AllowEdit = True

            cmSQL = Nothing : daSQl = Nothing
            cnSQL.Close()

            SetGridPishFaktorTafkik_Eslah()
            With GridEXPishFaktor_Eslah
                .Visible = True
                .DataSource = Nothing
                .DataSource = dvPishFaktor_Eslah
            End With

            'BoundCurrencyManagerPishFaktorTafkik_Eslah()

            Me.CenterToScreen()

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> SearchPishFaktorTafkik_Eslah ")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> SearchPishFaktorTafkik_Eslah ")
        End Try
    End Sub
    Private Sub SearchKalaPishFaktor_Eslah()
        Dim PK As Integer = 0

        If dvPishFaktor_Eslah.Count = 0 Then
            PK = 0
        Else
            PK = Val(GridEXPishFaktor_Eslah.CurrentRow.Cells("ccPishFaktorTitr").Text.Replace(",", ""))
        End If

        If FlgSearchKala = False Then
            Exit Sub
        End If

        Dim cnSQL As New SqlConnection
        Dim cmSQL As New SqlCommand
        Dim daSQl As New SqlDataAdapter
        Dim strSQL As String = ""

        If dsForm.Tables.Contains("tbl_KalaPishFaktor_Eslah") Then
            dsForm.Tables.Remove("tbl_KalaPishFaktor_Eslah")
        End If

        Try
            strSQL = "Sales.spPishFaktorGheireGhateei_SearchKalaPishFaktor_Eslah "

            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            cmSQL.Parameters.AddWithValue("ccPishFaktorTitr", PK)

            daSQl = New SqlDataAdapter(cmSQL)
            daSQl.Fill(dsForm, "tbl_KalaPishFaktor_Eslah")

            dvPishFaktor_Kala_Eslah = New DataView(dsForm.Tables("tbl_KalaPishFaktor_Eslah"))
            dvPishFaktor_Kala_Eslah.Sort = "CodeKala ASC"

            dvPishFaktor_Kala_Eslah.AllowNew = False
            dvPishFaktor_Kala_Eslah.AllowDelete = False
            dvPishFaktor_Kala_Eslah.AllowEdit = True

            cmSQL = Nothing : daSQl = Nothing
            cnSQL.Close()

            SetGridKalaPishFaktor_Eslah()
            With GridEXPishFaktor_Kala
                .Visible = True
                .DataSource = Nothing
                .DataSource = dvPishFaktor_Kala_Eslah
            End With

            Me.CenterToScreen()

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> SearchKalaPishFaktor_Eslah ")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> SearchKalaPishFaktor_Eslah ")
        End Try
    End Sub
    Private Sub SearchTafkik_Sodor()
        Dim cnSQL As New SqlConnection
        Dim cmSQL As New SqlCommand
        Dim daSQl As New SqlDataAdapter
        Dim strSQL As String = ""

        If IsValidSearchTafkik_Sodor() = False Then
            Exit Sub
        End If

        If dsForm.Tables.Contains("tbl_Tafkik_Sodor") Then
            dsForm.Tables.Remove("tbl_Tafkik_Sodor")
        End If

        Try
            strSQL = "Sales.spPishFaktorGheireGhateei_SearchTafkik_Sodor "

            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            cmSQL.Parameters.AddWithValue("CodeMahal", CodeMahalFaal)
            cmSQL.Parameters.AddWithValue("CodeDoreh", CodeDoreh)
            cmSQL.Parameters.AddWithValue("ccMamorPakhsh", cmbMamorPakhshS_Sodor.SelectedValue)
            cmSQL.Parameters.AddWithValue("AzTarikh", mskAzTarikh_Sodor.Text)
            cmSQL.Parameters.AddWithValue("TaTarikh", mskTaTarikh_Sodor.Text)
            cmSQL.Parameters.AddWithValue("UserName", UserName)

            daSQl = New SqlDataAdapter(cmSQL)
            daSQl.Fill(dsForm, "tbl_Tafkik_Sodor")

            '------------Adding Columns------------
            dsForm.Tables("tbl_Tafkik_Sodor").Columns.Add("Taeed", GetType(Boolean))
            '--------------------------------------

            Dim dr As DataRow
            For Each dr In dsForm.Tables("tbl_Tafkik_Sodor").Rows
                dr("Taeed") = False
            Next

            dvTafkik_Sodor = New DataView(dsForm.Tables("tbl_Tafkik_Sodor"))
            dvTafkik_Sodor.Sort = "ShomarehTafkik DESC"

            dvTafkik_Sodor.AllowNew = False
            dvTafkik_Sodor.AllowDelete = False
            dvTafkik_Sodor.AllowEdit = True

            cmSQL = Nothing : daSQl = Nothing
            cnSQL.Close()

            SetGridTafkik_Sodor()
            With GridEXTafkik_Sodor
                .Visible = True
                .DataSource = Nothing
                .DataSource = dvTafkik_Sodor
            End With

            BoundCurrencyManagerTafkik_Sodor()
            Me.CenterToScreen()

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> SearchTafkik_Sodor ")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> SearchTafkik_Sodor ")
        End Try
    End Sub
    Private Function IsValidSearchTafkik_Sodor() As Boolean
        IsValidSearchTafkik_Sodor = False

        If Len(mskAzTarikh_Sodor.Text.ToString) <> 0 Then
            If Not objTarikh.IsShDate(mskAzTarikh_Sodor.Text.ToString) Then
                mskAzTarikh_Sodor.Focus()
                Exit Function
            End If
            'If Microsoft.VisualBasic.Left(mskAzTarikh_Sodor.Text, 4) <> mdlPublic.CodeDoreh Then
            '    MsgBox("از تاريخ وارد شده با دوره انتخاب شده مغايرت دارد.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Critical, "خطا")
            '    ErrPro.SetError(mskAzTarikh_Sodor, "از تاريخ وارد شده با دوره انتخاب شده مغايرت دارد.")
            '    Exit Function
            'End If
        Else
            ErrPro.SetError(Me.mskAzTarikh_Sodor, " از تاريخ را وارد نمایید.")
            MsgBox(" از تاریخ را وارد نمایید.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
            mskAzTarikh_Sodor.Focus()
            Exit Function
        End If
        ErrPro.SetError(Me.mskAzTarikh_Sodor, "")

        If Len(mskTaTarikh_Sodor.Text.ToString) <> 0 Then
            If Not objTarikh.IsShDate(mskTaTarikh_Sodor.Text.ToString) Then
                mskTaTarikh_Sodor.Focus()
                Exit Function
            End If
            If Microsoft.VisualBasic.Left(mskTaTarikh_Sodor.Text, 4) <> mdlPublic.CodeDoreh Then
                MsgBox("تا تاريخ وارد شده با دوره انتخاب شده مغايرت دارد.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Critical, "خطا")
                ErrPro.SetError(mskTaTarikh_Sodor, "تا تاريخ وارد شده با دوره انتخاب شده مغايرت دارد.")
                Exit Function
            End If
        Else
            ErrPro.SetError(Me.mskTaTarikh_Sodor, " تا تاريخ را وارد نمایید.")
            MsgBox(" تا تاریخ را وارد نمایید.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
            mskTaTarikh_Sodor.Focus()
            Exit Function
        End If
        ErrPro.SetError(Me.mskTaTarikh_Sodor, "")

        IsValidSearchTafkik_Sodor = True
    End Function
    Private Sub SearchPishFaktorTafkik_Sodor()
        Dim PK As Integer = 0
        If dvTafkik_Sodor.Count = 0 Then
            PK = 0
        Else
            PK = Val(GridEXTafkik_Sodor.CurrentRow.Cells("ccTafkik_GG").Text.Replace(",", ""))
        End If

        If FlgSearchPishFaktor_Sodor = False Then
            Exit Sub
        End If

        Dim cnSQL As New SqlConnection
        Dim cmSQL As New SqlCommand
        Dim daSQl As New SqlDataAdapter
        Dim strSQL As String = ""

        If dsForm.Tables.Contains("tbl_PishFaktorTafkik_Sodor") Then
            dsForm.Tables.Remove("tbl_PishFaktorTafkik_Sodor")
        End If

        Try
            strSQL = "Sales.spPishFaktorGheireGhateei_SearchPishFaktorTafkik_Sodor "

            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            cmSQL.Parameters.AddWithValue("ccTafkik_GG", PK)

            daSQl = New SqlDataAdapter(cmSQL)
            daSQl.Fill(dsForm, "tbl_PishFaktorTafkik_Sodor")

            '------------Adding Columns------------
            dsForm.Tables("tbl_PishFaktorTafkik_Sodor").Columns.Add("Taeed", GetType(Boolean))
            '--------------------------------------

            Dim dr As DataRow
            For Each dr In dsForm.Tables("tbl_PishFaktorTafkik_Sodor").Rows
                dr("Taeed") = False
            Next

            dvPishFaktor_Sodor = New DataView(dsForm.Tables("tbl_PishFaktorTafkik_Sodor"))
            dvPishFaktor_Sodor.Sort = "PishFaktorShomareh DESC"

            dvPishFaktor_Sodor.AllowNew = False
            dvPishFaktor_Sodor.AllowDelete = False
            dvPishFaktor_Sodor.AllowEdit = True

            cmSQL = Nothing : daSQl = Nothing
            cnSQL.Close()

            SetGridPishFaktorTafkik_Sodor()
            With GridEXPishFaktor_Sodor
                .Visible = True
                .DataSource = Nothing
                .DataSource = dvPishFaktor_Sodor
            End With

            Me.CenterToScreen()

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> SearchPishFaktorTafkik_Sodor ")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> SearchPishFaktorTafkik_Sodor ")
        End Try
    End Sub
    Private Sub SearchKalaPishFaktor_Sodor()
        Dim PK As Integer = 0

        If dvPishFaktor_Sodor.Count = 0 Then
            PK = 0
        Else
            PK = Val(GridEXPishFaktor_Sodor.CurrentRow.Cells("ccPishFaktorTitr").Text.Replace(",", ""))
        End If

        'If FlgSearchKala = False Then
        'Exit Sub
        'End If

        Dim cnSQL As New SqlConnection
        Dim cmSQL As New SqlCommand
        Dim daSQl As New SqlDataAdapter
        Dim strSQL As String = ""

        If dsForm.Tables.Contains("tbl_KalaPishFaktor_Sodor") Then
            dsForm.Tables.Remove("tbl_KalaPishFaktor_Sodor")
        End If

        Try
            strSQL = "Sales.spPishFaktorGheireGhateei_SearchKalaPishFaktor_Sodor "

            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            cmSQL.Parameters.AddWithValue("ccPishFaktorTitr", PK)

            daSQl = New SqlDataAdapter(cmSQL)
            daSQl.Fill(dsForm, "tbl_KalaPishFaktor_Sodor")

            dvPishFaktor_Kala_Sodor = New DataView(dsForm.Tables("tbl_KalaPishFaktor_Sodor"))
            dvPishFaktor_Kala_Sodor.Sort = "CodeKala ASC"

            dvPishFaktor_Kala_Sodor.AllowNew = False
            dvPishFaktor_Kala_Sodor.AllowDelete = False
            dvPishFaktor_Kala_Sodor.AllowEdit = True

            cmSQL = Nothing : daSQl = Nothing
            cnSQL.Close()

            SetGridKalaPishFaktor_Sodor()
            With GridEXPishFaktor_Kala_Sodor
                .Visible = True
                .DataSource = Nothing
                .DataSource = dvPishFaktor_Kala_Sodor
            End With

            Me.CenterToScreen()

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> SearchKalaPishFaktor_Sodor ")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> SearchKalaPishFaktor_Sodor ")
        End Try
    End Sub
    Private Sub SetGridPishFaktor()
        If dvPishFaktor.Count = 0 Then
            Exit Sub
        End If

        Try
            With GridEXPishFaktor
                .DataSource = Nothing
                .DataSource = dsForm.Tables("tbl_PishFaktor").DefaultView
                .SetDataBinding(dsForm.Tables("tbl_PishFaktor").DefaultView, "")
                .RetrieveStructure()
            End With

            For i As Integer = 0 To GridEXPishFaktor.CurrentTable.Columns.Count - 1
                GridEXPishFaktor.CurrentTable.Columns.Item(i).Visible = False
            Next
            GridEXPishFaktor.CurrentTable.AllowEdit = InheritableBoolean.False

            GridEXPishFaktor.CurrentTable.Columns.Item("Taeed").Caption = "تاييد"
            GridEXPishFaktor.CurrentTable.Columns.Item("Taeed").Visible = True
            GridEXPishFaktor.CurrentTable.Columns.Item("Taeed").Width = 40
            GridEXPishFaktor.CurrentTable.Columns.Item("Taeed").Selectable = True
            GridEXPishFaktor.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXPishFaktor.CurrentTable.Columns.Item("Taeed").Position = 0
            GridEXPishFaktor.CurrentTable.Columns.Item("Taeed").Selectable = True
            GridEXPishFaktor.CurrentTable.Columns.Item("Taeed").ActAsSelector = True
            GridEXPishFaktor.CurrentTable.Columns.Item("Taeed").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXPishFaktor.CurrentTable.Columns.Item("Taeed").HeaderAlignment = TextAlignment.Center

            GridEXPishFaktor.CurrentTable.Columns.Item("PishFaktorShomareh").Caption = "شماره"
            GridEXPishFaktor.CurrentTable.Columns.Item("PishFaktorShomareh").Visible = True
            GridEXPishFaktor.CurrentTable.Columns.Item("PishFaktorShomareh").Width = 80
            'GridEXPishFaktor.CurrentTable.Columns.Item("PishFaktorShomareh").EditType = EditType.NoEdit
            GridEXPishFaktor.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXPishFaktor.CurrentTable.Columns.Item("PishFaktorShomareh").Position = 1
            GridEXPishFaktor.CurrentTable.Columns.Item("PishFaktorShomareh").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXPishFaktor.CurrentTable.Columns.Item("PishFaktorShomareh").HeaderAlignment = TextAlignment.Center

            GridEXPishFaktor.CurrentTable.Columns.Item("PishFaktorTarikhSlash").Caption = "تاريخ"
            GridEXPishFaktor.CurrentTable.Columns.Item("PishFaktorTarikhSlash").Visible = True
            GridEXPishFaktor.CurrentTable.Columns.Item("PishFaktorTarikhSlash").Width = 80
            'GridEXPishFaktor.CurrentTable.Columns.Item("PishFaktorTarikhSlash").EditType = EditType.NoEdit
            GridEXPishFaktor.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXPishFaktor.CurrentTable.Columns.Item("PishFaktorTarikhSlash").Position = 2
            GridEXPishFaktor.CurrentTable.Columns.Item("PishFaktorTarikhSlash").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXPishFaktor.CurrentTable.Columns.Item("PishFaktorTarikhSlash").HeaderAlignment = TextAlignment.Center

            GridEXPishFaktor.CurrentTable.Columns.Item("NameMoshtary").Caption = "نام مشتری"
            GridEXPishFaktor.CurrentTable.Columns.Item("NameMoshtary").Visible = True
            GridEXPishFaktor.CurrentTable.Columns.Item("NameMoshtary").Width = 210
            'GridEXPishFaktor.CurrentTable.Columns.Item("NameMoshtary").EditType = EditType.NoEdit
            GridEXPishFaktor.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXPishFaktor.CurrentTable.Columns.Item("NameMoshtary").Position = 3
            GridEXPishFaktor.CurrentTable.Columns.Item("NameMoshtary").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXPishFaktor.CurrentTable.Columns.Item("NameMoshtary").HeaderAlignment = TextAlignment.Center

            GridEXPishFaktor.CurrentTable.Columns.Item("txtNoeSenf").Caption = "نوع صنف"
            GridEXPishFaktor.CurrentTable.Columns.Item("txtNoeSenf").Visible = True
            GridEXPishFaktor.CurrentTable.Columns.Item("txtNoeSenf").Width = 110
            'GridEXPishFaktor.CurrentTable.Columns.Item("txtNoeSenf").EditType = EditType.NoEdit
            GridEXPishFaktor.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXPishFaktor.CurrentTable.Columns.Item("txtNoeSenf").Position = 4
            GridEXPishFaktor.CurrentTable.Columns.Item("txtNoeSenf").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXPishFaktor.CurrentTable.Columns.Item("txtNoeSenf").HeaderAlignment = TextAlignment.Center

            GridEXPishFaktor.CurrentTable.Columns.Item("NameForoshandeh").Caption = "نام فروشنده"
            GridEXPishFaktor.CurrentTable.Columns.Item("NameForoshandeh").Visible = True
            GridEXPishFaktor.CurrentTable.Columns.Item("NameForoshandeh").Width = 180
            'GridEXPishFaktor.CurrentTable.Columns.Item("NameForoshandeh").EditType = EditType.NoEdit
            GridEXPishFaktor.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXPishFaktor.CurrentTable.Columns.Item("NameForoshandeh").Position = 5
            GridEXPishFaktor.CurrentTable.Columns.Item("NameForoshandeh").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXPishFaktor.CurrentTable.Columns.Item("NameForoshandeh").HeaderAlignment = TextAlignment.Center

            GridEXPishFaktor.CurrentTable.Columns.Item("Address").Caption = "آدرس"
            GridEXPishFaktor.CurrentTable.Columns.Item("Address").Visible = True
            GridEXPishFaktor.CurrentTable.Columns.Item("Address").Width = 250
            'GridEXPishFaktor.CurrentTable.Columns.Item("Address").EditType = EditType.NoEdit
            GridEXPishFaktor.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXPishFaktor.CurrentTable.Columns.Item("Address").Position = 6
            GridEXPishFaktor.CurrentTable.Columns.Item("Address").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXPishFaktor.CurrentTable.Columns.Item("Address").HeaderAlignment = TextAlignment.Center

            GridEXPishFaktor.CurrentTable.Columns.Item("Makan").Caption = "مکان"
            GridEXPishFaktor.CurrentTable.Columns.Item("Makan").Visible = True
            GridEXPishFaktor.CurrentTable.Columns.Item("Makan").Width = 200
            'GridEXPishFaktor.CurrentTable.Columns.Item("Makan").EditType = EditType.NoEdit
            GridEXPishFaktor.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXPishFaktor.CurrentTable.Columns.Item("Makan").Position = 7
            GridEXPishFaktor.CurrentTable.Columns.Item("Makan").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXPishFaktor.CurrentTable.Columns.Item("Makan").HeaderAlignment = TextAlignment.Center

            GridEXPishFaktor.CurrentTable.Columns.Item("txtNoePardakht").Caption = "تسویه"
            GridEXPishFaktor.CurrentTable.Columns.Item("txtNoePardakht").Visible = True
            GridEXPishFaktor.CurrentTable.Columns.Item("txtNoePardakht").Width = 50
            'GridEXPishFaktor.CurrentTable.Columns.Item("txtNoePardakht").EditType = EditType.NoEdit
            GridEXPishFaktor.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXPishFaktor.CurrentTable.Columns.Item("txtNoePardakht").Position = 8
            GridEXPishFaktor.CurrentTable.Columns.Item("txtNoePardakht").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXPishFaktor.CurrentTable.Columns.Item("txtNoePardakht").HeaderAlignment = TextAlignment.Center

            GridEXPishFaktor.CurrentTable.Columns.Item("JamPishFaktor").Caption = "جمع مبلغ"
            GridEXPishFaktor.CurrentTable.Columns.Item("JamPishFaktor").Visible = True
            GridEXPishFaktor.CurrentTable.Columns.Item("JamPishFaktor").Width = 120
            'GridEXPishFaktor.CurrentTable.Columns.Item("JamPishFaktor").EditType = EditType.NoEdit
            GridEXPishFaktor.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXPishFaktor.CurrentTable.Columns.Item("JamPishFaktor").Position = 9
            GridEXPishFaktor.CurrentTable.Columns.Item("JamPishFaktor").FormatString = "###,###.##"
            GridEXPishFaktor.CurrentTable.Columns.Item("JamPishFaktor").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXPishFaktor.CurrentTable.Columns.Item("JamPishFaktor").HeaderAlignment = TextAlignment.Center

            GridEXPishFaktor.CurrentTable.Columns.Item("Takhfif").Caption = "تخفیف روی پ ف"
            GridEXPishFaktor.CurrentTable.Columns.Item("Takhfif").Visible = False
            GridEXPishFaktor.CurrentTable.Columns.Item("Takhfif").Width = 110
            'GridEXPishFaktor.CurrentTable.Columns.Item("Takhfif").EditType = EditType.NoEdit
            GridEXPishFaktor.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXPishFaktor.CurrentTable.Columns.Item("Takhfif").Position = 10
            GridEXPishFaktor.CurrentTable.Columns.Item("Takhfif").FormatString = "###,###.##"
            GridEXPishFaktor.CurrentTable.Columns.Item("Takhfif").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXPishFaktor.CurrentTable.Columns.Item("Takhfif").HeaderAlignment = TextAlignment.Center

            GridEXPishFaktor.CurrentTable.Columns.Item("JamTakhfifKala").Caption = "جمع تخفیف کالا"
            GridEXPishFaktor.CurrentTable.Columns.Item("JamTakhfifKala").Visible = False
            GridEXPishFaktor.CurrentTable.Columns.Item("JamTakhfifKala").Width = 100
            'GridEXPishFaktor.CurrentTable.Columns.Item("JamTakhfifKala").EditType = EditType.NoEdit
            GridEXPishFaktor.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXPishFaktor.CurrentTable.Columns.Item("JamTakhfifKala").Position = 11
            GridEXPishFaktor.CurrentTable.Columns.Item("JamTakhfifKala").FormatString = "###,###.##"
            GridEXPishFaktor.CurrentTable.Columns.Item("JamTakhfifKala").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXPishFaktor.CurrentTable.Columns.Item("JamTakhfifKala").HeaderAlignment = TextAlignment.Center

            GridEXPishFaktor.CurrentTable.Columns.Item("JamJayezeh").Caption = "جمع جوائز"
            GridEXPishFaktor.CurrentTable.Columns.Item("JamJayezeh").Visible = False
            GridEXPishFaktor.CurrentTable.Columns.Item("JamJayezeh").Width = 100
            'GridEXPishFaktor.CurrentTable.Columns.Item("JamJayezeh").EditType = EditType.NoEdit
            GridEXPishFaktor.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXPishFaktor.CurrentTable.Columns.Item("JamJayezeh").Position = 12
            GridEXPishFaktor.CurrentTable.Columns.Item("JamJayezeh").FormatString = "###,###.##"
            GridEXPishFaktor.CurrentTable.Columns.Item("JamJayezeh").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXPishFaktor.CurrentTable.Columns.Item("JamJayezeh").HeaderAlignment = TextAlignment.Center

            GridEXPishFaktor.CurrentTable.Columns.Item("JamMalyatAvarez").Caption = "مالیات و عوارض"
            GridEXPishFaktor.CurrentTable.Columns.Item("JamMalyatAvarez").Visible = False
            GridEXPishFaktor.CurrentTable.Columns.Item("JamMalyatAvarez").Width = 110
            'GridEXPishFaktor.CurrentTable.Columns.Item("JamMalyatAvarez").EditType = EditType.NoEdit
            GridEXPishFaktor.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXPishFaktor.CurrentTable.Columns.Item("JamMalyatAvarez").Position = 13
            GridEXPishFaktor.CurrentTable.Columns.Item("JamMalyatAvarez").FormatString = "###,###.##"
            GridEXPishFaktor.CurrentTable.Columns.Item("JamMalyatAvarez").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXPishFaktor.CurrentTable.Columns.Item("JamMalyatAvarez").HeaderAlignment = TextAlignment.Center

            GridEXPishFaktor.CurrentTable.Columns.Item("MablaghKol").Caption = "مبلغ کل"
            GridEXPishFaktor.CurrentTable.Columns.Item("MablaghKol").Visible = True
            GridEXPishFaktor.CurrentTable.Columns.Item("MablaghKol").Width = 120
            'GridEXPishFaktor.CurrentTable.Columns.Item("MablaghKol").EditType = EditType.NoEdit
            GridEXPishFaktor.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXPishFaktor.CurrentTable.Columns.Item("MablaghKol").Position = 14
            GridEXPishFaktor.CurrentTable.Columns.Item("MablaghKol").FormatString = "###,###.##"
            GridEXPishFaktor.CurrentTable.Columns.Item("MablaghKol").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXPishFaktor.CurrentTable.Columns.Item("MablaghKol").HeaderAlignment = TextAlignment.Center

            GridEXPishFaktor.CurrentTable.Columns.Item("Telephone").Caption = "تلفن"
            GridEXPishFaktor.CurrentTable.Columns.Item("Telephone").Visible = True
            GridEXPishFaktor.CurrentTable.Columns.Item("Telephone").Width = 70
            'GridEXPishFaktor.CurrentTable.Columns.Item("Telephone").EditType = EditType.NoEdit
            GridEXPishFaktor.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXPishFaktor.CurrentTable.Columns.Item("Telephone").Position = 15
            GridEXPishFaktor.CurrentTable.Columns.Item("Telephone").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXPishFaktor.CurrentTable.Columns.Item("Telephone").HeaderAlignment = TextAlignment.Center

            GridEXPishFaktor.CurrentTable.Columns.Item("Tozihat").Caption = "توضـیحات"
            GridEXPishFaktor.CurrentTable.Columns.Item("Tozihat").Visible = True
            GridEXPishFaktor.CurrentTable.Columns.Item("Tozihat").Width = 250
            'GridEXPishFaktor.CurrentTable.Columns.Item("Tozihat").EditType = EditType.NoEdit
            GridEXPishFaktor.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXPishFaktor.CurrentTable.Columns.Item("Tozihat").Position = 16
            GridEXPishFaktor.CurrentTable.Columns.Item("Tozihat").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXPishFaktor.CurrentTable.Columns.Item("Tozihat").HeaderAlignment = TextAlignment.Center

            GridEXPishFaktor.CurrentTable.Columns.Item("UserName").Caption = "نام کاربر"
            GridEXPishFaktor.CurrentTable.Columns.Item("UserName").Visible = True
            GridEXPishFaktor.CurrentTable.Columns.Item("UserName").Width = 100
            'GridEXPishFaktor.CurrentTable.Columns.Item("UserName").EditType = EditType.NoEdit
            GridEXPishFaktor.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXPishFaktor.CurrentTable.Columns.Item("UserName").Position = 17
            GridEXPishFaktor.CurrentTable.Columns.Item("UserName").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXPishFaktor.CurrentTable.Columns.Item("UserName").HeaderAlignment = TextAlignment.Center

            GridEXPishFaktor.CurrentTable.Columns.Item("ccPishFaktorTitr").Caption = "ccPishFaktorTitr"
            GridEXPishFaktor.CurrentTable.Columns.Item("ccPishFaktorTitr").Visible = False
            GridEXPishFaktor.CurrentTable.Columns.Item("ccPishFaktorTitr").Width = 0
            GridEXPishFaktor.CurrentTable.Columns.Item("ccPishFaktorTitr").EditType = EditType.NoEdit
            GridEXPishFaktor.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXPishFaktor.CurrentTable.Columns.Item("ccPishFaktorTitr").Position = 18
            GridEXPishFaktor.CurrentTable.Columns.Item("ccPishFaktorTitr").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXPishFaktor.CurrentTable.Columns.Item("ccPishFaktorTitr").HeaderAlignment = TextAlignment.Center

            For i As Integer = 0 To GridEXPishFaktor.RootTable.Columns.Count - 1
                If GridEXPishFaktor.RootTable.Columns(i).Type.IsValueType Then
                    GridEXPishFaktor.RootTable.Columns(i).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                    GridEXPishFaktor.RootTable.Columns(i).FormatMode = Janus.Windows.GridEX.FormatMode.UseIFormattable
                    GridEXPishFaktor.RootTable.Columns(i).FormatString = "G"
                    GridEXPishFaktor.RootTable.Columns(i).TotalFormatMode = Janus.Windows.GridEX.FormatMode.UseIFormattable
                    GridEXPishFaktor.RootTable.Columns(i).TotalFormatString = "G"
                End If
            Next

            Me.CenterToScreen()
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> SetGridPishFaktor ")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> SetGridPishFaktor ")
        End Try
    End Sub
    Private Sub SetGridTafkik_Eslah()
        If dvTafkik_Eslah.Count = 0 Then
            Exit Sub
        End If

        Try
            With GridEXTafkik_Eslah
                .DataSource = Nothing
                .DataSource = dsForm.Tables("tbl_Tafkik_Eslah").DefaultView
                .SetDataBinding(dsForm.Tables("tbl_Tafkik_Eslah").DefaultView, "")
                .RetrieveStructure()
            End With

            For i As Integer = 0 To GridEXTafkik_Eslah.CurrentTable.Columns.Count - 1
                GridEXTafkik_Eslah.CurrentTable.Columns.Item(i).Visible = False
            Next
            GridEXTafkik_Eslah.CurrentTable.AllowEdit = InheritableBoolean.False

            GridEXTafkik_Eslah.CurrentTable.Columns.Item("Taeed").Caption = "تاييد"
            GridEXTafkik_Eslah.CurrentTable.Columns.Item("Taeed").Visible = True
            GridEXTafkik_Eslah.CurrentTable.Columns.Item("Taeed").Width = 50
            GridEXTafkik_Eslah.CurrentTable.Columns.Item("Taeed").EditType = EditType.CheckBox
            GridEXTafkik_Eslah.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXTafkik_Eslah.CurrentTable.Columns.Item("Taeed").Position = 0
            GridEXTafkik_Eslah.CurrentTable.Columns.Item("Taeed").ActAsSelector = True
            GridEXTafkik_Eslah.CurrentTable.Columns.Item("Taeed").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXTafkik_Eslah.CurrentTable.Columns.Item("Taeed").HeaderAlignment = TextAlignment.Center

            GridEXTafkik_Eslah.CurrentTable.Columns.Item("ShomarehTafkik").Caption = "شماره تفکیک"
            GridEXTafkik_Eslah.CurrentTable.Columns.Item("ShomarehTafkik").Visible = True
            GridEXTafkik_Eslah.CurrentTable.Columns.Item("ShomarehTafkik").Width = 100
            'GridEXTafkik_Eslah.CurrentTable.Columns.Item("ShomarehTafkik").EditType = EditType.NoEdit
            GridEXTafkik_Eslah.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXTafkik_Eslah.CurrentTable.Columns.Item("ShomarehTafkik").Position = 1
            GridEXTafkik_Eslah.CurrentTable.Columns.Item("ShomarehTafkik").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXTafkik_Eslah.CurrentTable.Columns.Item("ShomarehTafkik").HeaderAlignment = TextAlignment.Center

            GridEXTafkik_Eslah.CurrentTable.Columns.Item("TarikhTafkik").Caption = "تاريخ تفکیک"
            GridEXTafkik_Eslah.CurrentTable.Columns.Item("TarikhTafkik").Visible = True
            GridEXTafkik_Eslah.CurrentTable.Columns.Item("TarikhTafkik").Width = 80
            'GridEXTafkik_Eslah.CurrentTable.Columns.Item("TarikhTafkik").EditType = EditType.NoEdit
            GridEXTafkik_Eslah.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXTafkik_Eslah.CurrentTable.Columns.Item("TarikhTafkik").Position = 2
            GridEXTafkik_Eslah.CurrentTable.Columns.Item("TarikhTafkik").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXTafkik_Eslah.CurrentTable.Columns.Item("TarikhTafkik").HeaderAlignment = TextAlignment.Center

            GridEXTafkik_Eslah.CurrentTable.Columns.Item("TarikhErsal").Caption = "تاریخ ارسال"
            GridEXTafkik_Eslah.CurrentTable.Columns.Item("TarikhErsal").Visible = True
            GridEXTafkik_Eslah.CurrentTable.Columns.Item("TarikhErsal").Width = 80
            'GridEXTafkik_Eslah.CurrentTable.Columns.Item("TarikhErsal").EditType = EditType.NoEdit
            GridEXTafkik_Eslah.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXTafkik_Eslah.CurrentTable.Columns.Item("TarikhErsal").Position = 3
            GridEXTafkik_Eslah.CurrentTable.Columns.Item("TarikhErsal").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXTafkik_Eslah.CurrentTable.Columns.Item("TarikhErsal").HeaderAlignment = TextAlignment.Center

            GridEXTafkik_Eslah.CurrentTable.Columns.Item("NameMamorPakhsh").Caption = "نام مامورپخش"
            GridEXTafkik_Eslah.CurrentTable.Columns.Item("NameMamorPakhsh").Visible = True
            GridEXTafkik_Eslah.CurrentTable.Columns.Item("NameMamorPakhsh").Width = 250
            'GridEXTafkik_Eslah.CurrentTable.Columns.Item("NameMamorPakhsh").EditType = EditType.NoEdit
            GridEXTafkik_Eslah.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXTafkik_Eslah.CurrentTable.Columns.Item("NameMamorPakhsh").Position = 4
            GridEXTafkik_Eslah.CurrentTable.Columns.Item("NameMamorPakhsh").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXTafkik_Eslah.CurrentTable.Columns.Item("NameMamorPakhsh").HeaderAlignment = TextAlignment.Center

            GridEXTafkik_Eslah.CurrentTable.Columns.Item("NameRanandehTozie").Caption = "راننده توزیع"
            GridEXTafkik_Eslah.CurrentTable.Columns.Item("NameRanandehTozie").Visible = True
            GridEXTafkik_Eslah.CurrentTable.Columns.Item("NameRanandehTozie").Width = 250
            'GridEXTafkik_Eslah.CurrentTable.Columns.Item("NameRanandehTozie").EditType = EditType.NoEdit
            GridEXTafkik_Eslah.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXTafkik_Eslah.CurrentTable.Columns.Item("NameRanandehTozie").Position = 5
            GridEXTafkik_Eslah.CurrentTable.Columns.Item("NameRanandehTozie").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXTafkik_Eslah.CurrentTable.Columns.Item("NameRanandehTozie").HeaderAlignment = TextAlignment.Center

            GridEXTafkik_Eslah.CurrentTable.Columns.Item("MashinTozie").Caption = "ماشین توزیع"
            GridEXTafkik_Eslah.CurrentTable.Columns.Item("MashinTozie").Visible = True
            GridEXTafkik_Eslah.CurrentTable.Columns.Item("MashinTozie").Width = 250
            'GridEXTafkik_Eslah.CurrentTable.Columns.Item("MashinTozie").EditType = EditType.NoEdit
            GridEXTafkik_Eslah.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXTafkik_Eslah.CurrentTable.Columns.Item("MashinTozie").Position = 6
            GridEXTafkik_Eslah.CurrentTable.Columns.Item("MashinTozie").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXTafkik_Eslah.CurrentTable.Columns.Item("MashinTozie").HeaderAlignment = TextAlignment.Center

            GridEXTafkik_Eslah.CurrentTable.Columns.Item("ccTafkik_GG").Caption = "ccTafkik_GG"
            GridEXTafkik_Eslah.CurrentTable.Columns.Item("ccTafkik_GG").Visible = False
            GridEXTafkik_Eslah.CurrentTable.Columns.Item("ccTafkik_GG").Width = 0
            GridEXTafkik_Eslah.CurrentTable.Columns.Item("ccTafkik_GG").EditType = EditType.NoEdit
            GridEXTafkik_Eslah.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXTafkik_Eslah.CurrentTable.Columns.Item("ccTafkik_GG").Position = 7
            GridEXTafkik_Eslah.CurrentTable.Columns.Item("ccTafkik_GG").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXTafkik_Eslah.CurrentTable.Columns.Item("ccTafkik_GG").HeaderAlignment = TextAlignment.Center

            For i As Integer = 0 To GridEXTafkik_Eslah.RootTable.Columns.Count - 1
                If GridEXTafkik_Eslah.RootTable.Columns(i).Type.IsValueType Then
                    GridEXTafkik_Eslah.RootTable.Columns(i).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                    GridEXTafkik_Eslah.RootTable.Columns(i).FormatMode = Janus.Windows.GridEX.FormatMode.UseIFormattable
                    GridEXTafkik_Eslah.RootTable.Columns(i).FormatString = "G"
                    GridEXTafkik_Eslah.RootTable.Columns(i).TotalFormatMode = Janus.Windows.GridEX.FormatMode.UseIFormattable
                    GridEXTafkik_Eslah.RootTable.Columns(i).TotalFormatString = "G"
                End If
            Next

            Me.CenterToScreen()
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> SetGridTafkik_Eslah ")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> SetGridTafkik_Eslah ")
        End Try
    End Sub
    Private Sub SetGridPishFaktorTafkik_Eslah()
        If dvPishFaktor_Eslah.Count = 0 Then
            Exit Sub
        End If

        Try
            With GridEXPishFaktor_Eslah
                .DataSource = Nothing
                .DataSource = dsForm.Tables("tbl_PishFaktorTafkik_Eslah").DefaultView
                .SetDataBinding(dsForm.Tables("tbl_PishFaktorTafkik_Eslah").DefaultView, "")
                .RetrieveStructure()
            End With

            For i As Integer = 0 To GridEXPishFaktor_Eslah.CurrentTable.Columns.Count - 1
                GridEXPishFaktor_Eslah.CurrentTable.Columns.Item(i).Visible = False
            Next

            GridEXPishFaktor_Eslah.CurrentTable.AllowEdit = InheritableBoolean.False

            GridEXPishFaktor_Eslah.CurrentTable.Columns.Item("PishFaktorShomareh").Caption = "شماره پیش فاکتور"
            GridEXPishFaktor_Eslah.CurrentTable.Columns.Item("PishFaktorShomareh").Visible = True
            GridEXPishFaktor_Eslah.CurrentTable.Columns.Item("PishFaktorShomareh").Width = 115
            'GridEXPishFaktor_Eslah.CurrentTable.Columns.Item("PishFaktorShomareh").EditType = EditType.NoEdit
            GridEXPishFaktor_Eslah.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXPishFaktor_Eslah.CurrentTable.Columns.Item("PishFaktorShomareh").Position = 0
            GridEXPishFaktor_Eslah.CurrentTable.Columns.Item("PishFaktorShomareh").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXPishFaktor_Eslah.CurrentTable.Columns.Item("PishFaktorShomareh").HeaderAlignment = TextAlignment.Center

            GridEXPishFaktor_Eslah.CurrentTable.Columns.Item("PishFaktorTarikh").Caption = "تاريخ پیش فاکتور"
            GridEXPishFaktor_Eslah.CurrentTable.Columns.Item("PishFaktorTarikh").Visible = True
            GridEXPishFaktor_Eslah.CurrentTable.Columns.Item("PishFaktorTarikh").Width = 110
            'GridEXPishFaktor_Eslah.CurrentTable.Columns.Item("PishFaktorTarikh").EditType = EditType.NoEdit
            GridEXPishFaktor_Eslah.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXPishFaktor_Eslah.CurrentTable.Columns.Item("PishFaktorTarikh").Position = 1
            GridEXPishFaktor_Eslah.CurrentTable.Columns.Item("PishFaktorTarikh").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXPishFaktor_Eslah.CurrentTable.Columns.Item("PishFaktorTarikh").HeaderAlignment = TextAlignment.Center




            GridEXPishFaktor_Eslah.CurrentTable.Columns.Item("NameMoshtary").Caption = "نام مشتـری"
            GridEXPishFaktor_Eslah.CurrentTable.Columns.Item("NameMoshtary").Visible = True
            GridEXPishFaktor_Eslah.CurrentTable.Columns.Item("NameMoshtary").Width = 320
            'GridEXPishFaktor_Eslah.CurrentTable.Columns.Item("NameMoshtary").EditType = EditType.NoEdit
            GridEXPishFaktor_Eslah.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXPishFaktor_Eslah.CurrentTable.Columns.Item("NameMoshtary").Position = 2
            GridEXPishFaktor_Eslah.CurrentTable.Columns.Item("NameMoshtary").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXPishFaktor_Eslah.CurrentTable.Columns.Item("NameMoshtary").HeaderAlignment = TextAlignment.Center

            GridEXPishFaktor_Eslah.CurrentTable.Columns.Item("txtNoePardakht").Caption = "نوع پرداخت"
            GridEXPishFaktor_Eslah.CurrentTable.Columns.Item("txtNoePardakht").Visible = True
            GridEXPishFaktor_Eslah.CurrentTable.Columns.Item("txtNoePardakht").Width = 110
            'GridEXPishFaktor_Eslah.CurrentTable.Columns.Item("txtNoePardakht").EditType = EditType.NoEdit
            GridEXPishFaktor_Eslah.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXPishFaktor_Eslah.CurrentTable.Columns.Item("txtNoePardakht").Position = 3
            GridEXPishFaktor_Eslah.CurrentTable.Columns.Item("txtNoePardakht").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXPishFaktor_Eslah.CurrentTable.Columns.Item("txtNoePardakht").HeaderAlignment = TextAlignment.Center

            GridEXPishFaktor_Eslah.CurrentTable.Columns.Item("MablaghKol").Caption = "مبلغ کل"
            GridEXPishFaktor_Eslah.CurrentTable.Columns.Item("MablaghKol").Visible = True
            GridEXPishFaktor_Eslah.CurrentTable.Columns.Item("MablaghKol").Width = 110
            'GridEXPishFaktor_Eslah.CurrentTable.Columns.Item("MablaghKol").EditType = EditType.NoEdit
            GridEXPishFaktor_Eslah.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXPishFaktor_Eslah.CurrentTable.Columns.Item("MablaghKol").Position = 4
            GridEXPishFaktor_Eslah.CurrentTable.Columns.Item("MablaghKol").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXPishFaktor_Eslah.CurrentTable.Columns.Item("MablaghKol").HeaderAlignment = TextAlignment.Center

            GridEXPishFaktor_Eslah.CurrentTable.Columns.Item("txtTahvil").Caption = "وضعیت تحویل"
            If cmbVazeiat_Eslah.SelectedIndex = 0 Then
                GridEXPishFaktor_Eslah.CurrentTable.Columns.Item("txtTahvil").Visible = False
                GridEXPishFaktor_Eslah.CurrentTable.Columns.Item("txtTahvil").Width = 0
            Else
                GridEXPishFaktor_Eslah.CurrentTable.Columns.Item("txtTahvil").Visible = True
                GridEXPishFaktor_Eslah.CurrentTable.Columns.Item("txtTahvil").Width = 110
            End If
            GridEXPishFaktor_Eslah.CurrentTable.Columns.Item("txtTahvil").EditType = EditType.NoEdit
            'GridEXPishFaktor_Eslah.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXPishFaktor_Eslah.CurrentTable.Columns.Item("txtTahvil").Position = 5
            GridEXPishFaktor_Eslah.CurrentTable.Columns.Item("txtTahvil").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXPishFaktor_Eslah.CurrentTable.Columns.Item("txtTahvil").HeaderAlignment = TextAlignment.Center

            GridEXPishFaktor_Eslah.CurrentTable.Columns.Item("ccPishFaktorTitr").Caption = "ccPishFaktorTitr"
            GridEXPishFaktor_Eslah.CurrentTable.Columns.Item("ccPishFaktorTitr").Visible = False
            GridEXPishFaktor_Eslah.CurrentTable.Columns.Item("ccPishFaktorTitr").Width = 0
            GridEXPishFaktor_Eslah.CurrentTable.Columns.Item("ccPishFaktorTitr").EditType = EditType.NoEdit
            GridEXPishFaktor_Eslah.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXPishFaktor_Eslah.CurrentTable.Columns.Item("ccPishFaktorTitr").Position = 6
            GridEXPishFaktor_Eslah.CurrentTable.Columns.Item("ccPishFaktorTitr").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXPishFaktor_Eslah.CurrentTable.Columns.Item("ccPishFaktorTitr").HeaderAlignment = TextAlignment.Center

            GridEXPishFaktor_Eslah.CurrentTable.Columns.Item("ccTafkikSatr_GG").Caption = "ccTafkikSatr_GG"
            GridEXPishFaktor_Eslah.CurrentTable.Columns.Item("ccTafkikSatr_GG").Visible = False
            GridEXPishFaktor_Eslah.CurrentTable.Columns.Item("ccTafkikSatr_GG").Width = 0
            GridEXPishFaktor_Eslah.CurrentTable.Columns.Item("ccTafkikSatr_GG").EditType = EditType.NoEdit
            GridEXPishFaktor_Eslah.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXPishFaktor_Eslah.CurrentTable.Columns.Item("ccTafkikSatr_GG").Position = 7
            GridEXPishFaktor_Eslah.CurrentTable.Columns.Item("ccTafkikSatr_GG").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXPishFaktor_Eslah.CurrentTable.Columns.Item("ccTafkikSatr_GG").HeaderAlignment = TextAlignment.Center

            GridEXPishFaktor_Eslah.CurrentTable.Columns.Item("IsTahvil").Caption = "IsTahvil"
            GridEXPishFaktor_Eslah.CurrentTable.Columns.Item("IsTahvil").Visible = False
            GridEXPishFaktor_Eslah.CurrentTable.Columns.Item("IsTahvil").Width = 0
            GridEXPishFaktor_Eslah.CurrentTable.Columns.Item("IsTahvil").EditType = EditType.NoEdit
            GridEXPishFaktor_Eslah.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXPishFaktor_Eslah.CurrentTable.Columns.Item("IsTahvil").Position = 8
            GridEXPishFaktor_Eslah.CurrentTable.Columns.Item("IsTahvil").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXPishFaktor_Eslah.CurrentTable.Columns.Item("IsTahvil").HeaderAlignment = TextAlignment.Center

            GridEXPishFaktor_Eslah.CurrentTable.Columns.Item("sNoePardakht").Caption = "sNoePardakht"
            GridEXPishFaktor_Eslah.CurrentTable.Columns.Item("sNoePardakht").Visible = False
            GridEXPishFaktor_Eslah.CurrentTable.Columns.Item("sNoePardakht").Width = 0
            GridEXPishFaktor_Eslah.CurrentTable.Columns.Item("sNoePardakht").EditType = EditType.NoEdit
            GridEXPishFaktor_Eslah.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXPishFaktor_Eslah.CurrentTable.Columns.Item("sNoePardakht").Position = 9
            GridEXPishFaktor_Eslah.CurrentTable.Columns.Item("sNoePardakht").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXPishFaktor_Eslah.CurrentTable.Columns.Item("sNoePardakht").HeaderAlignment = TextAlignment.Center

            GridEXPishFaktor_Eslah.CurrentTable.Columns.Item("IsEzafeBar").Caption = "IsEzafeBar"
            GridEXPishFaktor_Eslah.CurrentTable.Columns.Item("IsEzafeBar").Visible = False
            GridEXPishFaktor_Eslah.CurrentTable.Columns.Item("IsEzafeBar").Width = 0
            GridEXPishFaktor_Eslah.CurrentTable.Columns.Item("IsEzafeBar").EditType = EditType.NoEdit
            GridEXPishFaktor_Eslah.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXPishFaktor_Eslah.CurrentTable.Columns.Item("IsEzafeBar").Position = 10
            GridEXPishFaktor_Eslah.CurrentTable.Columns.Item("IsEzafeBar").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXPishFaktor_Eslah.CurrentTable.Columns.Item("IsEzafeBar").HeaderAlignment = TextAlignment.Center

            For i As Integer = 0 To GridEXPishFaktor_Eslah.RootTable.Columns.Count - 1
                If GridEXPishFaktor_Eslah.RootTable.Columns(i).Type.IsValueType Then
                    GridEXPishFaktor_Eslah.RootTable.Columns(i).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                    GridEXPishFaktor_Eslah.RootTable.Columns(i).FormatMode = Janus.Windows.GridEX.FormatMode.UseIFormattable
                    GridEXPishFaktor_Eslah.RootTable.Columns(i).FormatString = "G"
                    GridEXPishFaktor_Eslah.RootTable.Columns(i).TotalFormatMode = Janus.Windows.GridEX.FormatMode.UseIFormattable
                    GridEXPishFaktor_Eslah.RootTable.Columns(i).TotalFormatString = "G"
                End If
            Next

            Me.CenterToScreen()
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> SetGridPishFaktorTafkik_Eslah ")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> SetGridPishFaktorTafkik_Eslah ")
        End Try
    End Sub
    Private Sub SetGridKalaPishFaktor_Eslah()
        If dvPishFaktor_Kala_Eslah.Count = 0 Then
            Exit Sub
        End If

        Try
            With GridEXPishFaktor_Kala
                .DataSource = Nothing
                .DataSource = dsForm.Tables("tbl_KalaPishFaktor_Eslah").DefaultView
                .SetDataBinding(dsForm.Tables("tbl_KalaPishFaktor_Eslah").DefaultView, "")
                .RetrieveStructure()
            End With

            For i As Integer = 0 To GridEXPishFaktor_Kala.CurrentTable.Columns.Count - 1
                GridEXPishFaktor_Kala.CurrentTable.Columns.Item(i).Visible = False
            Next
            GridEXPishFaktor_Kala.CurrentTable.AllowEdit = InheritableBoolean.True

            GridEXPishFaktor_Kala.CurrentTable.Columns.Item("ccPishFaktorSatr").Caption = "ccPishFaktorSatr"
            GridEXPishFaktor_Kala.CurrentTable.Columns.Item("ccPishFaktorSatr").Visible = False
            GridEXPishFaktor_Kala.CurrentTable.Columns.Item("ccPishFaktorSatr").Width = 0
            'GridEXPishFaktor_Kala.CurrentTable.Columns.Item("ccPishFaktorSatr").EditType = EditType.NoEdit
            GridEXPishFaktor_Kala.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXPishFaktor_Kala.CurrentTable.Columns.Item("ccPishFaktorSatr").Position = 0
            GridEXPishFaktor_Kala.CurrentTable.Columns.Item("ccPishFaktorSatr").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXPishFaktor_Kala.CurrentTable.Columns.Item("ccPishFaktorSatr").HeaderAlignment = TextAlignment.Center

            GridEXPishFaktor_Kala.CurrentTable.Columns.Item("CodeKala").Caption = "کـد کالا"
            GridEXPishFaktor_Kala.CurrentTable.Columns.Item("CodeKala").Visible = True
            GridEXPishFaktor_Kala.CurrentTable.Columns.Item("CodeKala").Width = 80
            'GridEXPishFaktor_Kala.CurrentTable.Columns.Item("CodeKala").EditType = EditType.NoEdit
            GridEXPishFaktor_Kala.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXPishFaktor_Kala.CurrentTable.Columns.Item("CodeKala").Position = 0
            GridEXPishFaktor_Kala.CurrentTable.Columns.Item("CodeKala").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXPishFaktor_Kala.CurrentTable.Columns.Item("CodeKala").HeaderAlignment = TextAlignment.Center

            GridEXPishFaktor_Kala.CurrentTable.Columns.Item("NameKala").Caption = "نام کالا"
            GridEXPishFaktor_Kala.CurrentTable.Columns.Item("NameKala").Visible = True
            GridEXPishFaktor_Kala.CurrentTable.Columns.Item("NameKala").Width = 300
            'GridEXPishFaktor_Kala.CurrentTable.Columns.Item("NameKala").EditType = EditType.NoEdit
            GridEXPishFaktor_Kala.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXPishFaktor_Kala.CurrentTable.Columns.Item("NameKala").Position = 1
            GridEXPishFaktor_Kala.CurrentTable.Columns.Item("NameKala").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXPishFaktor_Kala.CurrentTable.Columns.Item("NameKala").HeaderAlignment = TextAlignment.Center

            GridEXPishFaktor_Kala.CurrentTable.Columns.Item("TedadAvalieh").Caption = "تعـداد اولیه"
            GridEXPishFaktor_Kala.CurrentTable.Columns.Item("TedadAvalieh").Visible = True
            GridEXPishFaktor_Kala.CurrentTable.Columns.Item("TedadAvalieh").Width = 70
            'GridEXPishFaktor_Kala.CurrentTable.Columns.Item("TedadAvalieh").EditType = EditType.NoEdit
            GridEXPishFaktor_Kala.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXPishFaktor_Kala.CurrentTable.Columns.Item("TedadAvalieh").Position = 2
            GridEXPishFaktor_Kala.CurrentTable.Columns.Item("TedadAvalieh").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXPishFaktor_Kala.CurrentTable.Columns.Item("TedadAvalieh").HeaderAlignment = TextAlignment.Center

            GridEXPishFaktor_Kala.CurrentTable.Columns.Item("TedadKartonAvalieh").Caption = "تعـداد کارتن اولیه"
            GridEXPishFaktor_Kala.CurrentTable.Columns.Item("TedadKartonAvalieh").Visible = True
            GridEXPishFaktor_Kala.CurrentTable.Columns.Item("TedadKartonAvalieh").Width = 110
            'GridEXPishFaktor_Kala.CurrentTable.Columns.Item("TedadKartonAvalieh").EditType = EditType.NoEdit
            GridEXPishFaktor_Kala.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXPishFaktor_Kala.CurrentTable.Columns.Item("TedadKartonAvalieh").Position = 3
            GridEXPishFaktor_Kala.CurrentTable.Columns.Item("TedadKartonAvalieh").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXPishFaktor_Kala.CurrentTable.Columns.Item("TedadKartonAvalieh").HeaderAlignment = TextAlignment.Center

            GridEXPishFaktor_Kala.CurrentTable.Columns.Item("TedadGhabelSabt").Caption = "تعـداد قابل ثبت"
            GridEXPishFaktor_Kala.CurrentTable.Columns.Item("TedadGhabelSabt").Visible = True
            GridEXPishFaktor_Kala.CurrentTable.Columns.Item("TedadGhabelSabt").Width = 95
            If cmbVazeiat_Eslah.SelectedIndex = 0 Then
                GridEXPishFaktor_Kala.CurrentTable.Columns.Item("TedadGhabelSabt").EditType = EditType.NoEdit
            End If
            GridEXPishFaktor_Kala.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXPishFaktor_Kala.CurrentTable.Columns.Item("TedadGhabelSabt").Position = 4
            GridEXPishFaktor_Kala.CurrentTable.Columns.Item("TedadGhabelSabt").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXPishFaktor_Kala.CurrentTable.Columns.Item("TedadGhabelSabt").HeaderAlignment = TextAlignment.Center

            GridEXPishFaktor_Kala.CurrentTable.Columns.Item("txtElatTaghirTedad").Caption = "علت تغییر تعداد"
            GridEXPishFaktor_Kala.CurrentTable.Columns.Item("txtElatTaghirTedad").Visible = True
            GridEXPishFaktor_Kala.CurrentTable.Columns.Item("txtElatTaghirTedad").Width = 200
            GridEXPishFaktor_Kala.CurrentTable.Columns.Item("txtElatTaghirTedad").EditType = EditType.NoEdit
            GridEXPishFaktor_Kala.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXPishFaktor_Kala.CurrentTable.Columns.Item("txtElatTaghirTedad").Position = 5
            GridEXPishFaktor_Kala.CurrentTable.Columns.Item("txtElatTaghirTedad").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXPishFaktor_Kala.CurrentTable.Columns.Item("txtElatTaghirTedad").HeaderAlignment = TextAlignment.Center

            GridEXPishFaktor_Kala.CurrentTable.Columns.Item("FeeGhabelEslah").Caption = "قیمت"
            GridEXPishFaktor_Kala.CurrentTable.Columns.Item("FeeGhabelEslah").Visible = True
            GridEXPishFaktor_Kala.CurrentTable.Columns.Item("FeeGhabelEslah").Width = 105
            If cmbVazeiat_Eslah.SelectedIndex = 0 Then
                If AllowChangeFeePishFaktor = False Then
                    GridEXPishFaktor_Kala.CurrentTable.Columns.Item("FeeGhabelEslah").EditType = EditType.NoEdit
                End If
            End If
            GridEXPishFaktor_Kala.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXPishFaktor_Kala.CurrentTable.Columns.Item("FeeGhabelEslah").Position = 6
            GridEXPishFaktor_Kala.CurrentTable.Columns.Item("FeeGhabelEslah").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXPishFaktor_Kala.CurrentTable.Columns.Item("FeeGhabelEslah").HeaderAlignment = TextAlignment.Center

            GridEXPishFaktor_Kala.CurrentTable.Columns.Item("ccPishFaktorSatr").Caption = "ccPishFaktorSatr"
            GridEXPishFaktor_Kala.CurrentTable.Columns.Item("ccPishFaktorSatr").Visible = False
            GridEXPishFaktor_Kala.CurrentTable.Columns.Item("ccPishFaktorSatr").Width = 0
            GridEXPishFaktor_Kala.CurrentTable.Columns.Item("ccPishFaktorSatr").EditType = EditType.NoEdit
            GridEXPishFaktor_Kala.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOn
            GridEXPishFaktor_Kala.CurrentTable.Columns.Item("ccPishFaktorSatr").Position = 7
            GridEXPishFaktor_Kala.CurrentTable.Columns.Item("ccPishFaktorSatr").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXPishFaktor_Kala.CurrentTable.Columns.Item("ccPishFaktorSatr").HeaderAlignment = TextAlignment.Center

            GridEXPishFaktor_Kala.CurrentTable.Columns.Item("ccKala").Caption = "ccKala"
            GridEXPishFaktor_Kala.CurrentTable.Columns.Item("ccKala").Visible = False
            GridEXPishFaktor_Kala.CurrentTable.Columns.Item("ccKala").Width = 0
            GridEXPishFaktor_Kala.CurrentTable.Columns.Item("ccKala").EditType = EditType.NoEdit
            GridEXPishFaktor_Kala.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXPishFaktor_Kala.CurrentTable.Columns.Item("ccKala").Position = 8
            GridEXPishFaktor_Kala.CurrentTable.Columns.Item("ccKala").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXPishFaktor_Kala.CurrentTable.Columns.Item("ccKala").HeaderAlignment = TextAlignment.Center

            GridEXPishFaktor_Kala.CurrentTable.Columns.Item("sElatTaghirTedad").Caption = "sElatTaghirTedad"
            GridEXPishFaktor_Kala.CurrentTable.Columns.Item("sElatTaghirTedad").Visible = False
            GridEXPishFaktor_Kala.CurrentTable.Columns.Item("sElatTaghirTedad").Width = 0
            GridEXPishFaktor_Kala.CurrentTable.Columns.Item("sElatTaghirTedad").EditType = EditType.NoEdit
            GridEXPishFaktor_Kala.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXPishFaktor_Kala.CurrentTable.Columns.Item("sElatTaghirTedad").Position = 9
            GridEXPishFaktor_Kala.CurrentTable.Columns.Item("sElatTaghirTedad").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXPishFaktor_Kala.CurrentTable.Columns.Item("sElatTaghirTedad").HeaderAlignment = TextAlignment.Center

            GridEXPishFaktor_Kala.CurrentTable.Columns.Item("IsJayezeh").Caption = "IsJayezeh"
            GridEXPishFaktor_Kala.CurrentTable.Columns.Item("IsJayezeh").Visible = False
            GridEXPishFaktor_Kala.CurrentTable.Columns.Item("IsJayezeh").Width = 0
            GridEXPishFaktor_Kala.CurrentTable.Columns.Item("IsJayezeh").EditType = EditType.NoEdit
            GridEXPishFaktor_Kala.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXPishFaktor_Kala.CurrentTable.Columns.Item("IsJayezeh").Position = 10
            GridEXPishFaktor_Kala.CurrentTable.Columns.Item("IsJayezeh").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXPishFaktor_Kala.CurrentTable.Columns.Item("IsJayezeh").HeaderAlignment = TextAlignment.Center

            GridEXPishFaktor_Kala.CurrentTable.Columns.Item("FeeKala").Caption = "FeeKala"
            GridEXPishFaktor_Kala.CurrentTable.Columns.Item("FeeKala").Visible = False
            GridEXPishFaktor_Kala.CurrentTable.Columns.Item("FeeKala").Width = 0
            GridEXPishFaktor_Kala.CurrentTable.Columns.Item("FeeKala").EditType = EditType.NoEdit
            GridEXPishFaktor_Kala.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXPishFaktor_Kala.CurrentTable.Columns.Item("FeeKala").Position = 11
            GridEXPishFaktor_Kala.CurrentTable.Columns.Item("FeeKala").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXPishFaktor_Kala.CurrentTable.Columns.Item("FeeKala").HeaderAlignment = TextAlignment.Center

            For i As Integer = 0 To GridEXPishFaktor_Kala.RootTable.Columns.Count - 1
                If GridEXPishFaktor_Kala.RootTable.Columns(i).Type.IsValueType Then
                    GridEXPishFaktor_Kala.RootTable.Columns(i).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                    GridEXPishFaktor_Kala.RootTable.Columns(i).FormatMode = Janus.Windows.GridEX.FormatMode.UseIFormattable
                    GridEXPishFaktor_Kala.RootTable.Columns(i).FormatString = "G"
                    GridEXPishFaktor_Kala.RootTable.Columns(i).TotalFormatMode = Janus.Windows.GridEX.FormatMode.UseIFormattable
                    GridEXPishFaktor_Kala.RootTable.Columns(i).TotalFormatString = "G"
                End If
            Next

            Me.CenterToScreen()
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> SetGridKalaPishFaktor_Eslah ")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> SetGridKalaPishFaktor_Eslah ")
        End Try
    End Sub
    Private Sub SetGridTafkik_Sodor()
        If dvTafkik_Sodor.Count = 0 Then
            Exit Sub
        End If

        Try
            With GridEXTafkik_Sodor
                .DataSource = Nothing
                .DataSource = dsForm.Tables("tbl_Tafkik_Sodor").DefaultView
                .SetDataBinding(dsForm.Tables("tbl_Tafkik_Sodor").DefaultView, "")
                .RetrieveStructure()
            End With

            For i As Integer = 0 To GridEXTafkik_Sodor.CurrentTable.Columns.Count - 1
                GridEXTafkik_Sodor.CurrentTable.Columns.Item(i).Visible = False
            Next

            GridEXTafkik_Sodor.CurrentTable.Columns.Item("Taeed").Caption = "تاييد"
            If chkSodorAzPishFaktor.Checked = False Then
                GridEXTafkik_Sodor.CurrentTable.Columns.Item("Taeed").Visible = True
            Else
                GridEXTafkik_Sodor.CurrentTable.Columns.Item("Taeed").Visible = False
            End If
            GridEXTafkik_Sodor.CurrentTable.AllowEdit = InheritableBoolean.False


            GridEXTafkik_Sodor.CurrentTable.Columns.Item("Taeed").Width = 50
            GridEXTafkik_Sodor.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXTafkik_Sodor.CurrentTable.Columns.Item("Taeed").Position = 0
            GridEXTafkik_Sodor.CurrentTable.Columns.Item("Taeed").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXTafkik_Sodor.CurrentTable.Columns.Item("Taeed").HeaderAlignment = TextAlignment.Center
            GridEXTafkik_Sodor.CurrentTable.Columns.Item("Taeed").ActAsSelector = True

            GridEXTafkik_Sodor.CurrentTable.Columns.Item("ShomarehTafkik").Caption = "شماره تفکیک"
            GridEXTafkik_Sodor.CurrentTable.Columns.Item("ShomarehTafkik").Visible = True
            GridEXTafkik_Sodor.CurrentTable.Columns.Item("ShomarehTafkik").Width = 100
            'GridEXTafkik_Sodor.CurrentTable.Columns.Item("ShomarehTafkik").EditType = EditType.NoEdit
            GridEXTafkik_Sodor.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXTafkik_Sodor.CurrentTable.Columns.Item("ShomarehTafkik").Position = 1
            GridEXTafkik_Sodor.CurrentTable.Columns.Item("ShomarehTafkik").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXTafkik_Sodor.CurrentTable.Columns.Item("ShomarehTafkik").HeaderAlignment = TextAlignment.Center

            GridEXTafkik_Sodor.CurrentTable.Columns.Item("TarikhTafkik").Caption = "تاريخ تفکیک"
            GridEXTafkik_Sodor.CurrentTable.Columns.Item("TarikhTafkik").Visible = True
            GridEXTafkik_Sodor.CurrentTable.Columns.Item("TarikhTafkik").Width = 90
            'GridEXTafkik_Sodor.CurrentTable.Columns.Item("TarikhTafkik").EditType = EditType.NoEdit
            GridEXTafkik_Sodor.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXTafkik_Sodor.CurrentTable.Columns.Item("TarikhTafkik").Position = 2
            GridEXTafkik_Sodor.CurrentTable.Columns.Item("TarikhTafkik").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXTafkik_Sodor.CurrentTable.Columns.Item("TarikhTafkik").HeaderAlignment = TextAlignment.Center

            GridEXTafkik_Sodor.CurrentTable.Columns.Item("TarikhErsal").Caption = "تاریخ ارسال"
            GridEXTafkik_Sodor.CurrentTable.Columns.Item("TarikhErsal").Visible = True
            GridEXTafkik_Sodor.CurrentTable.Columns.Item("TarikhErsal").Width = 90
            'GridEXTafkik_Sodor.CurrentTable.Columns.Item("TarikhErsal").EditType = EditType.NoEdit
            GridEXTafkik_Sodor.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXTafkik_Sodor.CurrentTable.Columns.Item("TarikhErsal").Position = 3
            GridEXTafkik_Sodor.CurrentTable.Columns.Item("TarikhErsal").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXTafkik_Sodor.CurrentTable.Columns.Item("TarikhErsal").HeaderAlignment = TextAlignment.Center

            GridEXTafkik_Sodor.CurrentTable.Columns.Item("NameMamorPakhsh").Caption = "نام مامورپخش"
            GridEXTafkik_Sodor.CurrentTable.Columns.Item("NameMamorPakhsh").Visible = True
            GridEXTafkik_Sodor.CurrentTable.Columns.Item("NameMamorPakhsh").Width = 245
            'GridEXTafkik_Sodor.CurrentTable.Columns.Item("NameMamorPakhsh").EditType = EditType.NoEdit
            GridEXTafkik_Sodor.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXTafkik_Sodor.CurrentTable.Columns.Item("NameMamorPakhsh").Position = 4
            GridEXTafkik_Sodor.CurrentTable.Columns.Item("NameMamorPakhsh").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXTafkik_Sodor.CurrentTable.Columns.Item("NameMamorPakhsh").HeaderAlignment = TextAlignment.Center

            GridEXTafkik_Sodor.CurrentTable.Columns.Item("NameRanandehTozie").Caption = "راننده توزیع"
            GridEXTafkik_Sodor.CurrentTable.Columns.Item("NameRanandehTozie").Visible = True
            GridEXTafkik_Sodor.CurrentTable.Columns.Item("NameRanandehTozie").Width = 245
            'GridEXTafkik_Sodor.CurrentTable.Columns.Item("NameRanandehTozie").EditType = EditType.NoEdit
            GridEXTafkik_Sodor.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXTafkik_Sodor.CurrentTable.Columns.Item("NameRanandehTozie").Position = 5
            GridEXTafkik_Sodor.CurrentTable.Columns.Item("NameRanandehTozie").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXTafkik_Sodor.CurrentTable.Columns.Item("NameRanandehTozie").HeaderAlignment = TextAlignment.Center

            GridEXTafkik_Sodor.CurrentTable.Columns.Item("MashinTozie").Caption = "ماشین توزیع"
            GridEXTafkik_Sodor.CurrentTable.Columns.Item("MashinTozie").Visible = True
            GridEXTafkik_Sodor.CurrentTable.Columns.Item("MashinTozie").Width = 245
            'GridEXTafkik_Sodor.CurrentTable.Columns.Item("MashinTozie").EditType = EditType.NoEdit
            GridEXTafkik_Sodor.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXTafkik_Sodor.CurrentTable.Columns.Item("MashinTozie").Position = 6
            GridEXTafkik_Sodor.CurrentTable.Columns.Item("MashinTozie").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXTafkik_Sodor.CurrentTable.Columns.Item("MashinTozie").HeaderAlignment = TextAlignment.Center

            GridEXTafkik_Sodor.CurrentTable.Columns.Item("ccTafkik_GG").Caption = "ccTafkik_GG"
            GridEXTafkik_Sodor.CurrentTable.Columns.Item("ccTafkik_GG").Visible = False
            GridEXTafkik_Sodor.CurrentTable.Columns.Item("ccTafkik_GG").Width = 0
            'GridEXTafkik_Sodor.CurrentTable.Columns.Item("ccTafkik_GG").EditType = EditType.NoEdit
            GridEXTafkik_Sodor.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXTafkik_Sodor.CurrentTable.Columns.Item("ccTafkik_GG").Position = 6
            GridEXTafkik_Sodor.CurrentTable.Columns.Item("ccTafkik_GG").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXTafkik_Sodor.CurrentTable.Columns.Item("ccTafkik_GG").HeaderAlignment = TextAlignment.Center

            For i As Integer = 0 To GridEXTafkik_Sodor.RootTable.Columns.Count - 1
                If GridEXTafkik_Sodor.RootTable.Columns(i).Type.IsValueType Then
                    GridEXTafkik_Sodor.RootTable.Columns(i).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                    GridEXTafkik_Sodor.RootTable.Columns(i).FormatMode = Janus.Windows.GridEX.FormatMode.UseIFormattable
                    GridEXTafkik_Sodor.RootTable.Columns(i).FormatString = "G"
                    GridEXTafkik_Sodor.RootTable.Columns(i).TotalFormatMode = Janus.Windows.GridEX.FormatMode.UseIFormattable
                    GridEXTafkik_Sodor.RootTable.Columns(i).TotalFormatString = "G"
                End If
            Next

            Me.CenterToScreen()
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> SetGridTafkik_Sodor ")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> SetGridTafkik_Sodor ")
        End Try
    End Sub
    Private Sub SetGridPishFaktorTafkik_Sodor()
        If dvPishFaktor_Sodor.Count = 0 Then
            Exit Sub
        End If
        Try
            With GridEXPishFaktor_Sodor
                .DataSource = Nothing
                .DataSource = dsForm.Tables("tbl_PishFaktorTafkik_Sodor").DefaultView
                .SetDataBinding(dsForm.Tables("tbl_PishFaktorTafkik_Sodor").DefaultView, "")
                .RetrieveStructure()
            End With

            For i As Integer = 0 To GridEXPishFaktor_Sodor.CurrentTable.Columns.Count - 1
                GridEXPishFaktor_Sodor.CurrentTable.Columns.Item(i).Visible = False
            Next
            GridEXPishFaktor_Sodor.CurrentTable.AllowEdit = InheritableBoolean.False

            GridEXPishFaktor_Sodor.CurrentTable.Columns.Item("Taeed").Caption = "تاييد"
            If chkSodorAzPishFaktor.Checked = False Then
                GridEXPishFaktor_Sodor.CurrentTable.Columns.Item("Taeed").Visible = False
            Else
                GridEXPishFaktor_Sodor.CurrentTable.Columns.Item("Taeed").Visible = True
            End If
            GridEXPishFaktor_Sodor.CurrentTable.Columns.Item("Taeed").Width = 50
            GridEXPishFaktor_Sodor.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXPishFaktor_Sodor.CurrentTable.Columns.Item("Taeed").Position = 0
            GridEXPishFaktor_Sodor.CurrentTable.Columns.Item("Taeed").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXPishFaktor_Sodor.CurrentTable.Columns.Item("Taeed").HeaderAlignment = TextAlignment.Center
            GridEXPishFaktor_Sodor.CurrentTable.Columns.Item("Taeed").ActAsSelector = True

            GridEXPishFaktor_Sodor.CurrentTable.Columns.Item("PishFaktorShomareh").Caption = "شماره پیش فاکتور"
            GridEXPishFaktor_Sodor.CurrentTable.Columns.Item("PishFaktorShomareh").Visible = True
            GridEXPishFaktor_Sodor.CurrentTable.Columns.Item("PishFaktorShomareh").Width = 115
            'GridEXPishFaktor_Sodor.CurrentTable.Columns.Item("PishFaktorShomareh").EditType = EditType.NoEdit
            GridEXPishFaktor_Sodor.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXPishFaktor_Sodor.CurrentTable.Columns.Item("PishFaktorShomareh").Position = 1
            GridEXPishFaktor_Sodor.CurrentTable.Columns.Item("PishFaktorShomareh").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXPishFaktor_Sodor.CurrentTable.Columns.Item("PishFaktorShomareh").HeaderAlignment = TextAlignment.Center

            GridEXPishFaktor_Sodor.CurrentTable.Columns.Item("PishFaktorTarikh").Caption = "تاريخ پیش فاکتور"
            GridEXPishFaktor_Sodor.CurrentTable.Columns.Item("PishFaktorTarikh").Visible = True
            GridEXPishFaktor_Sodor.CurrentTable.Columns.Item("PishFaktorTarikh").Width = 110
            'GridEXPishFaktor_Sodor.CurrentTable.Columns.Item("PishFaktorTarikh").EditType = EditType.NoEdit
            GridEXPishFaktor_Sodor.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXPishFaktor_Sodor.CurrentTable.Columns.Item("PishFaktorTarikh").Position = 2
            GridEXPishFaktor_Sodor.CurrentTable.Columns.Item("PishFaktorTarikh").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXPishFaktor_Sodor.CurrentTable.Columns.Item("PishFaktorTarikh").HeaderAlignment = TextAlignment.Center

            GridEXPishFaktor_Sodor.CurrentTable.Columns.Item("NameMoshtary").Caption = "نام مشتـری"
            GridEXPishFaktor_Sodor.CurrentTable.Columns.Item("NameMoshtary").Visible = True
            GridEXPishFaktor_Sodor.CurrentTable.Columns.Item("NameMoshtary").Width = 220
            'GridEXPishFaktor_Sodor.CurrentTable.Columns.Item("NameMoshtary").EditType = EditType.NoEdit
            GridEXPishFaktor_Sodor.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXPishFaktor_Sodor.CurrentTable.Columns.Item("NameMoshtary").Position = 3
            GridEXPishFaktor_Sodor.CurrentTable.Columns.Item("NameMoshtary").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXPishFaktor_Sodor.CurrentTable.Columns.Item("NameMoshtary").HeaderAlignment = TextAlignment.Center

            GridEXPishFaktor_Sodor.CurrentTable.Columns.Item("NameForoshandeh").Caption = "نام فروشنده"
            GridEXPishFaktor_Sodor.CurrentTable.Columns.Item("NameForoshandeh").Visible = True
            GridEXPishFaktor_Sodor.CurrentTable.Columns.Item("NameForoshandeh").Width = 220
            'GridEXPishFaktor_Sodor.CurrentTable.Columns.Item("NameForoshandeh").EditType = EditType.NoEdit
            GridEXPishFaktor_Sodor.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXPishFaktor_Sodor.CurrentTable.Columns.Item("NameForoshandeh").Position = 4
            GridEXPishFaktor_Sodor.CurrentTable.Columns.Item("NameForoshandeh").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXPishFaktor_Sodor.CurrentTable.Columns.Item("NameForoshandeh").HeaderAlignment = TextAlignment.Center

            GridEXPishFaktor_Sodor.CurrentTable.Columns.Item("MablaghKol").Caption = "مبلغ کل"
            GridEXPishFaktor_Sodor.CurrentTable.Columns.Item("MablaghKol").Visible = True
            GridEXPishFaktor_Sodor.CurrentTable.Columns.Item("MablaghKol").Width = 150
            'GridEXPishFaktor_Sodor.CurrentTable.Columns.Item("MablaghKol").EditType = EditType.NoEdit
            GridEXPishFaktor_Sodor.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXPishFaktor_Sodor.CurrentTable.Columns.Item("MablaghKol").Position = 5
            GridEXPishFaktor_Sodor.CurrentTable.Columns.Item("MablaghKol").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXPishFaktor_Sodor.CurrentTable.Columns.Item("MablaghKol").HeaderAlignment = TextAlignment.Center

            GridEXPishFaktor_Sodor.CurrentTable.Columns.Item("ccPishFaktorTitr").Caption = "ccPishFaktorTitr"
            GridEXPishFaktor_Sodor.CurrentTable.Columns.Item("ccPishFaktorTitr").Visible = False
            GridEXPishFaktor_Sodor.CurrentTable.Columns.Item("ccPishFaktorTitr").Width = 0
            GridEXPishFaktor_Sodor.CurrentTable.Columns.Item("ccPishFaktorTitr").EditType = EditType.NoEdit
            GridEXPishFaktor_Sodor.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXPishFaktor_Sodor.CurrentTable.Columns.Item("ccPishFaktorTitr").Position = 6
            GridEXPishFaktor_Sodor.CurrentTable.Columns.Item("ccPishFaktorTitr").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXPishFaktor_Sodor.CurrentTable.Columns.Item("ccPishFaktorTitr").HeaderAlignment = TextAlignment.Center

            For i As Integer = 0 To GridEXPishFaktor_Sodor.RootTable.Columns.Count - 1
                If GridEXPishFaktor_Sodor.RootTable.Columns(i).Type.IsValueType Then
                    GridEXPishFaktor_Sodor.RootTable.Columns(i).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                    GridEXPishFaktor_Sodor.RootTable.Columns(i).FormatMode = Janus.Windows.GridEX.FormatMode.UseIFormattable
                    GridEXPishFaktor_Sodor.RootTable.Columns(i).FormatString = "G"
                    GridEXPishFaktor_Sodor.RootTable.Columns(i).TotalFormatMode = Janus.Windows.GridEX.FormatMode.UseIFormattable
                    GridEXPishFaktor_Sodor.RootTable.Columns(i).TotalFormatString = "G"
                End If
            Next

            Me.CenterToScreen()
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> SetGridPishFaktorTafkik_Sodor ")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> SetGridPishFaktorTafkik_Sodor ")
        End Try
    End Sub
    Private Sub SetGridKalaPishFaktor_Sodor()
        If dvPishFaktor_Kala_Sodor.Count = 0 Then
            Exit Sub
        End If

        Try
            With GridEXPishFaktor_Kala_Sodor
                .DataSource = Nothing
                .DataSource = dsForm.Tables("tbl_KalaPishFaktor_Sodor").DefaultView
                .SetDataBinding(dsForm.Tables("tbl_KalaPishFaktor_Sodor").DefaultView, "")
                .RetrieveStructure()
            End With

            For i As Integer = 0 To GridEXPishFaktor_Kala_Sodor.CurrentTable.Columns.Count - 1
                GridEXPishFaktor_Kala_Sodor.CurrentTable.Columns.Item(i).Visible = False
            Next
            GridEXPishFaktor_Kala_Sodor.CurrentTable.AllowEdit = InheritableBoolean.False

            GridEXPishFaktor_Kala_Sodor.CurrentTable.Columns.Item("CodeKala").Caption = "کـد کالا"
            GridEXPishFaktor_Kala_Sodor.CurrentTable.Columns.Item("CodeKala").Visible = True
            GridEXPishFaktor_Kala_Sodor.CurrentTable.Columns.Item("CodeKala").Width = 80
            'GridEXPishFaktor_Kala_Sodor.CurrentTable.Columns.Item("CodeKala").EditType = EditType.NoEdit
            GridEXPishFaktor_Kala_Sodor.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXPishFaktor_Kala_Sodor.CurrentTable.Columns.Item("CodeKala").Position = 0
            GridEXPishFaktor_Kala_Sodor.CurrentTable.Columns.Item("CodeKala").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXPishFaktor_Kala_Sodor.CurrentTable.Columns.Item("CodeKala").HeaderAlignment = TextAlignment.Center

            GridEXPishFaktor_Kala_Sodor.CurrentTable.Columns.Item("NameKala").Caption = "نام کالا"
            GridEXPishFaktor_Kala_Sodor.CurrentTable.Columns.Item("NameKala").Visible = True
            GridEXPishFaktor_Kala_Sodor.CurrentTable.Columns.Item("NameKala").Width = 400
            'GridEXPishFaktor_Kala_Sodor.CurrentTable.Columns.Item("NameKala").EditType = EditType.NoEdit
            GridEXPishFaktor_Kala_Sodor.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXPishFaktor_Kala_Sodor.CurrentTable.Columns.Item("NameKala").Position = 1
            GridEXPishFaktor_Kala_Sodor.CurrentTable.Columns.Item("NameKala").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXPishFaktor_Kala_Sodor.CurrentTable.Columns.Item("NameKala").HeaderAlignment = TextAlignment.Center

            GridEXPishFaktor_Kala_Sodor.CurrentTable.Columns.Item("TedadSabtShodeh").Caption = "تعـداد ثبت شده"
            GridEXPishFaktor_Kala_Sodor.CurrentTable.Columns.Item("TedadSabtShodeh").Visible = True
            GridEXPishFaktor_Kala_Sodor.CurrentTable.Columns.Item("TedadSabtShodeh").Width = 110
            'GridEXPishFaktor_Kala_Sodor.CurrentTable.Columns.Item("TedadSabtShodeh").EditType = EditType.NoEdit
            GridEXPishFaktor_Kala_Sodor.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXPishFaktor_Kala_Sodor.CurrentTable.Columns.Item("TedadSabtShodeh").Position = 2
            GridEXPishFaktor_Kala_Sodor.CurrentTable.Columns.Item("TedadSabtShodeh").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXPishFaktor_Kala_Sodor.CurrentTable.Columns.Item("TedadSabtShodeh").HeaderAlignment = TextAlignment.Center

            GridEXPishFaktor_Kala_Sodor.CurrentTable.Columns.Item("TedadSabtShodehBeKarton").Caption = "تعـداد کارتن ثبت شده"
            GridEXPishFaktor_Kala_Sodor.CurrentTable.Columns.Item("TedadSabtShodehBeKarton").Visible = True
            GridEXPishFaktor_Kala_Sodor.CurrentTable.Columns.Item("TedadSabtShodehBeKarton").Width = 140
            'GridEXPishFaktor_Kala_Sodor.CurrentTable.Columns.Item("TedadSabtShodehBeKarton").EditType = EditType.NoEdit
            GridEXPishFaktor_Kala_Sodor.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXPishFaktor_Kala_Sodor.CurrentTable.Columns.Item("TedadSabtShodehBeKarton").Position = 3
            GridEXPishFaktor_Kala_Sodor.CurrentTable.Columns.Item("TedadSabtShodehBeKarton").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXPishFaktor_Kala_Sodor.CurrentTable.Columns.Item("TedadSabtShodehBeKarton").HeaderAlignment = TextAlignment.Center

            GridEXPishFaktor_Kala_Sodor.CurrentTable.Columns.Item("ccPishFaktorSatr").Caption = "ccPishFaktorSatr"
            GridEXPishFaktor_Kala_Sodor.CurrentTable.Columns.Item("ccPishFaktorSatr").Visible = False
            GridEXPishFaktor_Kala_Sodor.CurrentTable.Columns.Item("ccPishFaktorSatr").Width = 0
            GridEXPishFaktor_Kala_Sodor.CurrentTable.Columns.Item("ccPishFaktorSatr").EditType = EditType.NoEdit
            GridEXPishFaktor_Kala_Sodor.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOn
            GridEXPishFaktor_Kala_Sodor.CurrentTable.Columns.Item("ccPishFaktorSatr").Position = 4
            GridEXPishFaktor_Kala_Sodor.CurrentTable.Columns.Item("ccPishFaktorSatr").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXPishFaktor_Kala_Sodor.CurrentTable.Columns.Item("ccPishFaktorSatr").HeaderAlignment = TextAlignment.Center

            GridEXPishFaktor_Kala_Sodor.CurrentTable.Columns.Item("ccKala").Caption = "ccKala"
            GridEXPishFaktor_Kala_Sodor.CurrentTable.Columns.Item("ccKala").Visible = False
            GridEXPishFaktor_Kala_Sodor.CurrentTable.Columns.Item("ccKala").Width = 0
            GridEXPishFaktor_Kala_Sodor.CurrentTable.Columns.Item("ccKala").EditType = EditType.NoEdit
            GridEXPishFaktor_Kala_Sodor.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXPishFaktor_Kala_Sodor.CurrentTable.Columns.Item("ccKala").Position = 5
            GridEXPishFaktor_Kala_Sodor.CurrentTable.Columns.Item("ccKala").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXPishFaktor_Kala_Sodor.CurrentTable.Columns.Item("ccKala").HeaderAlignment = TextAlignment.Center

            For i As Integer = 0 To GridEXPishFaktor_Kala_Sodor.RootTable.Columns.Count - 1
                If GridEXPishFaktor_Kala_Sodor.RootTable.Columns(i).Type.IsValueType Then
                    GridEXPishFaktor_Kala_Sodor.RootTable.Columns(i).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                    GridEXPishFaktor_Kala_Sodor.RootTable.Columns(i).FormatMode = Janus.Windows.GridEX.FormatMode.UseIFormattable
                    GridEXPishFaktor_Kala_Sodor.RootTable.Columns(i).FormatString = "G"
                    GridEXPishFaktor_Kala_Sodor.RootTable.Columns(i).TotalFormatMode = Janus.Windows.GridEX.FormatMode.UseIFormattable
                    GridEXPishFaktor_Kala_Sodor.RootTable.Columns(i).TotalFormatString = "G"
                End If
            Next

            Me.CenterToScreen()
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> SetGridKalaPishFaktor_Sodor ")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> SetGridKalaPishFaktor_Sodor ")
        End Try
    End Sub
    Private Sub BoundCurrencyManagerTafkik_Eslah()
        cmTafkik_Eslah = CType(BindingContext(GridEXTafkik_Eslah.DataSource), CurrencyManager)
        AddHandler cmTafkik_Eslah.PositionChanged, AddressOf cmTafkik_Eslah_PositionChanged
        SearchPishFaktorTafkik_Eslah()
        SearchKalaPishFaktor_Eslah()
    End Sub
    Private Sub cmTafkik_Eslah_PositionChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        SearchPishFaktorTafkik_Eslah()
    End Sub
    Private Sub BoundCurrencyManagerPishFaktorTafkik_Eslah()
        cmPishFaktor_Kala_Eslah = CType(BindingContext(GridEXPishFaktor_Eslah.DataSource), CurrencyManager)
        AddHandler cmPishFaktor_Kala_Eslah.PositionChanged, AddressOf cmPishFaktor_Kala_Eslah_PositionChanged
        SearchKalaPishFaktor_Eslah()
    End Sub
    Private Sub cmPishFaktor_Kala_Eslah_PositionChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        SearchKalaPishFaktor_Eslah()
    End Sub
    Private Sub BoundCurrencyManagerTafkik_Sodor()
        cmTafkik_Sodor = CType(BindingContext(GridEXTafkik_Sodor.DataSource), CurrencyManager)
        AddHandler cmTafkik_Sodor.PositionChanged, AddressOf cmTafkik_Sodor_PositionChanged
        SearchPishFaktorTafkik_Sodor()
    End Sub
    Private Sub cmTafkik_Sodor_PositionChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        SearchPishFaktorTafkik_Sodor()
    End Sub
    Private Sub BoundCurrencyManagerPishFaktorTafkik_Sodor()
        cmPishFaktor_Kala_Sodor = CType(BindingContext(GridEXPishFaktor_Sodor.DataSource), CurrencyManager)
        AddHandler cmPishFaktor_Kala_Sodor.PositionChanged, AddressOf cmPishFaktor_Kala_Sodor_PositionChanged
        SearchKalaPishFaktor_Sodor()
    End Sub
    Private Sub cmPishFaktor_Kala_Sodor_PositionChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        SearchKalaPishFaktor_Sodor()
    End Sub
    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        SearchPishFaktor()
    End Sub
    Private Sub btnSearchTafkik_Eslah_Click(sender As Object, e As EventArgs) Handles btnSearchTafkik_Eslah.Click
        SearchTafkik_Eslah(cmbVazeiat_Eslah.SelectedIndex)
    End Sub
    Private Sub btnSearchTafkik_Sodor_Click(sender As Object, e As EventArgs) Handles btnSearchTafkik_Sodor.Click
        SearchTafkik_Sodor()
    End Sub

    Private Sub tbMain_SelectedIndexChanged(sender As Object, e As EventArgs) Handles tbMain.SelectedIndexChanged
        If UserName.ToUpper <> "ADMINISTRATOR" Then
            If tbMain.SelectedIndex = 2 Then
                ObjCode.UserName = UserName
                If Not ObjCode.CheckPermission(1000207) Then
                    tbMain.SelectedIndex = 1
                    Exit Sub
                End If
            End If
        End If
        SetForm()
        If tbMain.SelectedIndex = 0 Then
            SearchPishFaktor()
        ElseIf tbMain.SelectedIndex = 1 Then
            cmbVazeiat_Eslah.SelectedIndex = 0
            cmbMamorPakhshS_Eslsh.SelectedValue = 0
            SearchTafkik_Eslah(cmbVazeiat_Eslah.SelectedIndex)
            FlgSearchKala = True
        ElseIf tbMain.SelectedIndex = 2 Then
            cmbMamorPakhshS_Sodor.SelectedValue = 0
            SearchTafkik_Sodor()
            FlgSearchPishFaktor_Sodor = True
        End If
    End Sub
    Private Sub btnShowCalcVaznAndHajm_Click(sender As Object, e As EventArgs) Handles btnShowCalcVaznAndHajm.Click
        If CheckVaznAndHajm() = False Then
            MsgBox("برای محاسبه باید حداقل 1 پیش فاکتور انتخاب نمایید .", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "")
            Exit Sub
        End If
    End Sub
    Private Function CheckVaznAndHajm() As Boolean
        CheckVaznAndHajm = False

        Try
            lblJamTedad.Text = ""
            lblJamRial.Text = ""
            lblJamVazn.Text = ""
            lblHajm.Text = ""
            lblKarton.Text = ""
            lblTedadFaktor.Text = ""

            Dim strSQL As String
            Dim drv As DataRowView
            Dim Count As Integer = 0

            Dim strPishFaktor As String = ""

            For i As Integer = 0 To GridEXPishFaktor.RowCount - 1
                If GridEXPishFaktor.GetRows(i).Cells("Taeed").Value = True Then
                    strPishFaktor &= GridEXPishFaktor.GetRows(i).Cells("ccPishFaktorTitr").Text.Replace(",", "") & ","
                    Count += 1
                End If
            Next

            If strPishFaktor.Length > 0 Then
                If strPishFaktor.Substring(strPishFaktor.Length - 1, 1) = "," Then
                    strPishFaktor = strPishFaktor.Remove(strPishFaktor.Length - 1, 1)
                End If
            Else
                Return False
                Exit Function
            End If

            strSQL = "SELECT a.ccKala,SUM(Tedad3) AS Tedad, CAST(SUM(Tedad3) / b.TedadDarKarton AS INT) AS TedadKarton "
            strSQL &= " FROM tblFO_PishFaktorSatr AS a WITH(NOLOCK) LEFT OUTER JOIN "
            strSQL &= " tblAN_Kala AS b WITH(NOLOCK) ON a.ccKala = b.ccKala "
            strSQL &= " WHERE  a.ccPishFaktorTitr IN (" & strPishFaktor & ")"
            strSQL &= " GROUP BY a.ccKala, b.TedadDarKarton"

            Dim daSQL As SqlDataAdapter
            If dsForm.Tables.Contains("tblKalaSum") Then
                dsForm.Tables.Remove("tblKalaSum")
            End If
            daSQL = New SqlDataAdapter(strSQL, ConnectionString)
            daSQL.Fill(dsForm, "tblKalaSum")

            Dim Vazn As Double = 0
            Dim Hajm As Double = 0
            Dim TedadKarton As Double = 0
            For Each drv In dsForm.Tables("tblKalaSum").DefaultView
                If objTools.ConvertNulls(objTools.DLookup("CheckHajmTafkikInGheireGhatee_Kartoni_ZarReq", "tblGL_SysConfig", "CodeMahal = " & CodeMahalFaal), False) = False Then
                    Hajm += objTools.ConvertNulls(objTools.DLookup("Tol", "tblAN_Kala", "ccKala =" & drv("ccKala")), 1) * _
                        objTools.ConvertNulls(objTools.DLookup("Arz", "tblAN_Kala", "ccKala =" & drv("ccKala")), 1) * _
                        objTools.ConvertNulls(objTools.DLookup("Ertefa", "tblAN_Kala", "ccKala =" & drv("ccKala")), 1) * _
                        drv("Tedad")
                Else
                    Hajm += objTools.ConvertNulls(objTools.DLookup("Tol", "tblAN_Kala", "ccKala =" & drv("ccKala")), 1) * _
                        objTools.ConvertNulls(objTools.DLookup("Arz", "tblAN_Kala", "ccKala =" & drv("ccKala")), 1) * _
                        objTools.ConvertNulls(objTools.DLookup("Ertefa", "tblAN_Kala", "ccKala =" & drv("ccKala")), 1) * _
                        drv("TedadKarton")
                End If
                

                TedadKarton += drv("Tedad") / objTools.ConvertNulls(objTools.DLookup("TedadDarKarton", "tblAN_Kala", "ccKala =" & drv("ccKala")), 1)

                Vazn += objTools.DLookup("VaznKhales", "tblAN_Kala", "ccKala =" & drv("ccKala")) * drv("Tedad")
            Next

            lblHajm.Text = Hajm
            lblJamVazn.Text = Vazn / 1000
            lblJamTedad.Text = dsForm.Tables("tblKalaSum").Compute("sum(Tedad)", "")
            lblJamRial.Text = ObjCode.DigitSeprator(objTools.DSum("MablaghKol", "tblFO_PishFaktor", "ccPishFaktorTitr IN (" & strPishFaktor & ")"))
            lblKarton.Text = TedadKarton
            lblTedadFaktor.Text = Count

            Return True
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> CheckVaznAndHajm ")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> CheckVaznAndHajm ")
        End Try
    End Function
    Private Sub btnSodorTafkik_Click(sender As Object, e As EventArgs) Handles btnSodorTafkik.Click
        Dim strPishFaktor As String = ""

        Try
            If CheckVaznAndHajm() = False Then
                MsgBox("برای صدور برگه تفکیک باید حداقل 1 پیش فاکتور انتخاب نمایید .", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "")
                Exit Sub
            End If

            For i As Integer = 0 To GridEXPishFaktor.RowCount - 1
                If GridEXPishFaktor.GetRows(i).Cells("Taeed").Value = True Then
                    strPishFaktor &= GridEXPishFaktor.GetRows(i).Cells("ccPishFaktorTitr").Text.Replace(",", "") & ","
                End If
            Next

            If strPishFaktor <> "" Then
                If IsValidSodorTafkik() = False Then
                    Exit Sub
                End If

                strPishFaktor = "," & strPishFaktor
                SodorTafkik(strPishFaktor)
                SearchPishFaktor()
            Else
                MsgBox("برای صدور برگه تفکیک باید حداقل 1 پیش فاکتور انتخاب نمایید .", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "")
                Exit Sub
            End If

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> btnSodorTafkik_Click ")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> btnSodorTafkik_Click ")
        End Try
    End Sub
    Private Function IsValidSodorTafkik() As Boolean
        IsValidSodorTafkik = False

        Try
            If cmbMamorPakhsh.DataSource Is Nothing Then
                MsgBox("برای صدور برگه تفکیک باید یک مامور پخش انتخاب نمایید .", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "")
                ErrPro.SetError(cmbMamorPakhsh, "برای صدور برگه تفکیک باید یک مامور پخش انتخاب نمایید .")
                Exit Function
            End If
            ErrPro.SetError(cmbMamorPakhsh, "")

            If cmbMashinTozie.DataSource Is Nothing Then
                MsgBox("برای صدور برگه تفکیک باید یک ماشین توزیع انتخاب نمایید .", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "")
                ErrPro.SetError(cmbMashinTozie, "برای صدور برگه تفکیک باید یک ماشین توزیع انتخاب نمایید .")
                Exit Function
            End If
            ErrPro.SetError(cmbMashinTozie, "")

            If cmbRanandehTozie.DataSource Is Nothing Then
                MsgBox("برای صدور برگه تفکیک باید یک راننده توزیع انتخاب نمایید .", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "")
                ErrPro.SetError(cmbRanandehTozie, "برای صدور برگه تفکیک باید یک راننده توزیع انتخاب نمایید .")
                Exit Function
            End If
            ErrPro.SetError(cmbRanandehTozie, "")

            If Len(mskTarikhPishbiniErsal.Text.ToString) <> 0 Then
                If Not objTarikh.IsShDate(mskTarikhPishbiniErsal.Text.ToString) Then
                    mskTarikhPishbiniErsal.Focus()
                    Exit Function
                End If
                If Microsoft.VisualBasic.Left(mskTarikhPishbiniErsal.Text, 4) <> mdlPublic.CodeDoreh Then
                    MsgBox("تاريخ پیش بینی وارد شده با دوره انتخاب شده مغايرت دارد.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Critical, "خطا")
                    ErrPro.SetError(mskTarikhPishbiniErsal, "تاريخ پیش بینی وارد شده با دوره انتخاب شده مغايرت دارد.")
                    Exit Function
                End If
            Else
                ErrPro.SetError(Me.mskTarikhPishbiniErsal, " تاريخ پیش بینی را وارد نمایید.")
                MsgBox(" تاریخ پیش بینی را وارد نمایید.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
                mskTarikhPishbiniErsal.Focus()
                Exit Function
            End If
            ErrPro.SetError(Me.mskTarikhPishbiniErsal, "")

            ''-------------------------------

            Dim HajmMashin As Double = 0
            Dim VaznMashin As Double = 0
            Dim checkHajmKala As Boolean = False
            Dim checkVaznKala As Boolean = False

            checkHajmKala = objTools.ConvertNulls(objTools.DLookup("CheckHajmMashin", "tblGL_SysConfig", "CodeMahal =" & CodeMahalFaal), 0)
            If checkHajmKala Then
                HajmMashin = objTools.ConvertNulls(objTools.DLookup("Tol", "qryFO_Mashin", "ccMashin = " & cmbMashinTozie.SelectedValue), 1) * _
                             objTools.ConvertNulls(objTools.DLookup("Arz", "qryFO_Mashin", "ccMashin = " & cmbMashinTozie.SelectedValue), 1) * _
                             objTools.ConvertNulls(objTools.DLookup("Ertefa", "qryFO_Mashin", "ccMashin = " & cmbMashinTozie.SelectedValue), 1)

                If Val(lblHajm.Text) > HajmMashin Then
                    MsgBox("حجم اجناس انتخاب شده از حجم ماشین بیشتر است." & vbCrLf & " حجم ماشین :" & HajmMashin, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
                    Exit Function
                End If
            End If

            checkVaznKala = objTools.ConvertNulls(objTools.DLookup("CheckVaznMashin", "tblGL_SysConfig", "CodeMahal =" & CodeMahalFaal), 0)
            If checkVaznKala Then
                VaznMashin = objTools.ConvertNulls(objTools.DLookup("Vazn", "qryFO_Mashin", "ccMashin =" & cmbMashinTozie.SelectedValue), 1)

                If Val(lblJamVazn.Text) > VaznMashin Then
                    VaznMashin = VaznMashin
                    Dim JamVaznkala As Double = lblJamVazn.Text
                    MsgBox("وزن اجناس انتخاب شده از وزن قابل حمل ماشین بیشتر است." & vbCrLf & " وزن  قابل حمل ماشین : " & VaznMashin & " کیلوگرم " & vbCrLf & " جمع وزن کالاها  : " & JamVaznkala & " کیلوگرم ", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
                    Exit Function
                End If
            End If

            ''-------------------------------

            IsValidSodorTafkik = True

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> IsValidSodorTafkik ")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> IsValidSodorTafkik ")
        End Try
    End Function
    Private Sub SodorTafkik(ByVal strPishFaktor As String)
        Dim cnSQL As New SqlConnection
        Dim cmSQL As New SqlCommand
        Dim strSQL As String = ""
        Dim ShomarehTafkik As Integer = 0

        Try
            strSQL = "Sales.spPishFaktorGheireGhateei_CreateTafkik "

            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            cmSQL.Parameters.AddWithValue("CodeMahal", CodeMahalFaal)
            cmSQL.Parameters.AddWithValue("CodeDoreh", CodeDoreh)
            cmSQL.Parameters.AddWithValue("TarikhEmrooz", TarikhEmrooz)
            cmSQL.Parameters.AddWithValue("ccMamorPakhsh", cmbMamorPakhsh.SelectedValue)
            cmSQL.Parameters.AddWithValue("ccRananadehTozie", cmbRanandehTozie.SelectedValue)
            cmSQL.Parameters.AddWithValue("ccMashinTozie", cmbMashinTozie.SelectedValue)
            cmSQL.Parameters.AddWithValue("TarikhErsal", mskTarikhPishbiniErsal.Text)
            cmSQL.Parameters.AddWithValue("strPishFaktor", strPishFaktor)
            cmSQL.Parameters.AddWithValue("UserName", UserName)
            cmSQL.Parameters.AddWithValue("Saat", Format(TimeOfDay, "HH:mm:ss"))
            cmSQL.Parameters.AddWithValue("ID", ShomarehTafkik)
            cmSQL.Parameters("ID").Direction = ParameterDirection.Output

            cmSQL.ExecuteNonQuery()

            ShomarehTafkik = cmSQL.Parameters("ID").Value

            MsgBox("شماره تفکیک " & ShomarehTafkik & " با موفقیت صادر گردید .", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "")

            cmSQL = Nothing
            cnSQL.Close()

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> SodorTafkik ")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> SodorTafkik ")
        End Try
    End Sub

    Private Sub GridEXTafkik_Eslah_Click(sender As Object, e As EventArgs) Handles GridEXTafkik_Eslah.Click
        BoundCurrencyManagerTafkik_Eslah()
    End Sub
    Private Sub GridEXPishFaktor_Eslah_Click(sender As Object, e As EventArgs) Handles GridEXPishFaktor_Eslah.Click
        BoundCurrencyManagerPishFaktorTafkik_Eslah()

        If cmbVazeiat_Eslah.SelectedIndex = 1 Then
            If GridEXPishFaktor_Eslah.CurrentRow.Cells("IsTahvil").Value = True Then
                tsmiAdameTahvil.Visible = True
                tsmiTahvil.Visible = False
            Else
                tsmiAdameTahvil.Visible = False
                tsmiTahvil.Visible = True
            End If
        End If

    End Sub
    Private Sub GridEXTafkik_Sodor_Click(sender As Object, e As EventArgs) Handles GridEXTafkik_Sodor.Click
        BoundCurrencyManagerTafkik_Sodor()
    End Sub
    Private Sub btnSabteTaghirat_Click(sender As Object, e As EventArgs) Handles btnSabteTaghirat.Click
        Dim CountTaghirat As Integer = 0
        Try
            For i As Integer = 0 To GridEXPishFaktor_Kala.RowCount - 1
                If Not IsValidElat(cmsKala.Text.Trim) Then Exit Sub
            Next
            If GridEXPishFaktor_Eslah.CurrentRow.Cells("IsTahvil").Value = False Then
                MsgBox("پیش فاکتور انتخاب شده به حالت « عـدم تحویل » تغییر وضعیت داده شده است . نمیتوانید تغییری در تعداد کالاهای آن ایجاد نمایید .", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "")
                Exit Sub
            End If

            For i As Integer = 0 To GridEXPishFaktor_Kala.RowCount - 1
                If (Val(GridEXPishFaktor_Kala.GetRow(i).Cells("TedadAvalieh").Text.Replace(",", "")) <> Val(GridEXPishFaktor_Kala.GetRow(i).Cells("TedadGhabelSabt").Text.Replace(",", ""))) Or (Val(GridEXPishFaktor_Kala.GetRow(i).Cells("TedadGhabelSabt").Text.Replace(",", "")) <> objTools.DLookup("Tedad3", "tblFO_PishFaktorSatr", "ccPishFaktorSatr = " & Val(GridEXPishFaktor_Kala.GetRow(i).Cells("ccPishFaktorSatr").Text.Replace(",", "")))) Then
                    CountTaghirat += 1
                End If
            Next

            If CountTaghirat = 0 Then
                MsgBox("تغییری صورت نپذیرفته است .", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "")
                Exit Sub
            Else
                If MsgBox("آیا تغییرات اعمال شده ثبت گردد ؟ ", MsgBoxStyle.Question + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.YesNo, "") = MsgBoxResult.No Then
                    Exit Sub
                End If

                CountTaghirat = 0

                For i As Integer = 0 To GridEXPishFaktor_Kala.RowCount - 1
                    If (Val(GridEXPishFaktor_Kala.GetRow(i).Cells("TedadAvalieh").Text.Replace(",", "")) <> Val(GridEXPishFaktor_Kala.GetRow(i).Cells("TedadGhabelSabt").Text.Replace(",", ""))) Or (Val(GridEXPishFaktor_Kala.GetRow(i).Cells("TedadGhabelSabt").Text.Replace(",", "")) <> objTools.DLookup("Tedad3", "tblFO_PishFaktorSatr", "ccPishFaktorSatr = " & Val(GridEXPishFaktor_Kala.GetRow(i).Cells("ccPishFaktorSatr").Text.Replace(",", "")))) Then
                        If SabteTaghirat(Val(GridEXTafkik_Eslah.CurrentRow.Cells("ccTafkik_GG").Text.Replace(",", "")), Val(GridEXPishFaktor_Kala.GetRow(i).Cells("ccPishFaktorSatr").Text.Replace(",", "")), Val(GridEXPishFaktor_Kala.GetRow(i).Cells("TedadGhabelSabt").Text.Replace(",", ""))) = False Then
                            MsgBox("با ثبت تعداد وارد شده برای کالای " & GridEXPishFaktor_Kala.GetRow(i).Cells("NameKala").Text & " ، تعداد ثبت شده برای این کالا بیشتر از تعداد موجود در تفکیک می شود که امکانپذیر نمی باشد .", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "")
                        Else
                            CountTaghirat += 1
                        End If
                    End If
                Next
            End If

            If CountTaghirat > 0 Then
                MsgBox("تعـداد " & CountTaghirat & " سطـر از ایـن پیش فاکتـور با موفقیت به روز رسانی گردیـد .", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "")
            End If

            SearchKalaPishFaktor_Eslah()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, "خطا")
        Finally
        End Try
    End Sub
    Function IsValidElat(ByVal Elat As String) As Boolean
        Elat = SElatTaghirTeadadPishGheireGhateei
        IsValidElat = False
        Dim strError As String = String.Empty
        If Elat = 0 Then
            strError &= ". علت تغییر تعداد را وارد کنید"
        End If
        Return True
    End Function

    Private Sub btnSabtTaghirGheymat_Click(sender As Object, e As EventArgs) Handles btnSabtTaghirGheymat.Click
        Dim CountTaghirat As Integer = 0
        Dim ResultTaghir As Integer = 0

        If GridEXPishFaktor_Eslah.CurrentRow.Cells("IsTahvil").Value = False Then
            MsgBox("پیش فاکتور انتخاب شده به حالت « عـدم تحویل » تغییر وضعیت داده شده است . نمیتوانید تغییری در قیمت کالاهای آن ایجاد نمایید .", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "")
            Exit Sub
        End If

        For i As Integer = 0 To GridEXPishFaktor_Kala.RowCount - 1
            If (Val(GridEXPishFaktor_Kala.GetRow(i).Cells("FeeGhabelEslah").Text.Replace(",", "")) <> Val(GridEXPishFaktor_Kala.GetRow(i).Cells("FeeKala").Text.Replace(",", ""))) Or (Val(GridEXPishFaktor_Kala.GetRow(i).Cells("FeeGhabelEslah").Text.Replace(",", "")) <> objTools.DLookup("Fee", "tblFO_PishFaktorSatr", "ccPishFaktorSatr = " & Val(GridEXPishFaktor_Kala.GetRow(i).Cells("ccPishFaktorSatr").Text.Replace(",", "")))) Then
                CountTaghirat += 1
            End If
        Next

        If CountTaghirat = 0 Then
            MsgBox("تغییری صورت نپذیرفته است .", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "")
            Exit Sub
        Else
            If MsgBox("آیا تغییرات اعمال شده ثبت گردد ؟ ", MsgBoxStyle.Question + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.YesNo, "") = MsgBoxResult.No Then
                Exit Sub
            End If

            CountTaghirat = 0

            For i As Integer = 0 To GridEXPishFaktor_Kala.RowCount - 1
                ResultTaghir = SabteTaghirat_Fee(Val(GridEXTafkik_Eslah.CurrentRow.Cells("ccTafkik_GG").Text.Replace(",", "")), Val(GridEXPishFaktor_Kala.GetRow(i).Cells("ccPishFaktorSatr").Text.Replace(",", "")), Val(GridEXPishFaktor_Kala.GetRow(i).Cells("FeeGhabelEslah").Text.Replace(",", "")))
                If (Val(GridEXPishFaktor_Kala.GetRow(i).Cells("FeeGhabelEslah").Text.Replace(",", "")) <> Val(GridEXPishFaktor_Kala.GetRow(i).Cells("FeeKala").Text.Replace(",", ""))) Or (Val(GridEXPishFaktor_Kala.GetRow(i).Cells("FeeGhabelEslah").Text.Replace(",", "")) <> objTools.DLookup("Fee", "tblFO_PishFaktorSatr", "ccPishFaktorSatr = " & Val(GridEXPishFaktor_Kala.GetRow(i).Cells("ccPishFaktorSatr").Text.Replace(",", "")))) Then
                    If ResultTaghir = 0 Then
                        MsgBox("قیمت وارد شده برای کالای " & GridEXPishFaktor_Kala.GetRow(i).Cells("NameKala").Text & " کوچکتر یا مساوی صفر است که امکان ثبت آن وجود ندارد !", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Critical, "")
                    ElseIf ResultTaghir = 1 Then
                        MsgBox("کالای " & GridEXPishFaktor_Kala.GetRow(i).Cells("NameKala").Text & " جایزه است و امکان ثبت قیمت برای آن وجود ندارد !", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Critical, "")
                    ElseIf ResultTaghir = 2 Then
                        CountTaghirat += 1
                    End If
                End If
            Next
        End If

        If CountTaghirat > 0 Then
            MsgBox("تعـداد " & CountTaghirat & " سطـر از ایـن پیش فاکتـور با موفقیت به روز رسانی گردیـد .", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "")
        End If

        SearchKalaPishFaktor_Eslah()
    End Sub
    Private Function SabteTaghirat(ByVal ccTafkik_GG As Integer, ByVal ccPishFaktorSatr As Integer, ByVal TedadGhabelSabt As Double) As Boolean
        SabteTaghirat = False

        Dim cnSQL As New SqlConnection
        Dim cmSQL As New SqlCommand
        Dim strSQL As String = ""

        If objTools.DLookup("IsJayezeh", "tblFO_PishFaktorSatr", "ccPishFaktorSatr = " & ccPishFaktorSatr) Then
            SabteTaghirat = True
            Exit Function
        End If

        Try
            strSQL = "Sales.spPishFaktorGheireGhateei_SabteTaghirat "

            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            cmSQL.Parameters.AddWithValue("ccTafkik_GG", ccTafkik_GG)
            cmSQL.Parameters.AddWithValue("ccPishFaktorSatr", ccPishFaktorSatr)
            cmSQL.Parameters.AddWithValue("TedadGhabelSabt", TedadGhabelSabt)
            cmSQL.Parameters.AddWithValue("SabteTaghirat", SabteTaghirat)
            cmSQL.Parameters("SabteTaghirat").Direction = ParameterDirection.Output

            cmSQL.ExecuteNonQuery()

            SabteTaghirat = cmSQL.Parameters("SabteTaghirat").Value

            cmSQL = Nothing
            cnSQL.Close()

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> SabteTaghirat ")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> SabteTaghirat ")
        End Try
    End Function
    Private Function SabteTaghirat_Fee(ByVal ccTafkik_GG As Integer, ByVal ccPishFaktorSatr As Integer, ByVal Fee As Double) As Integer
        '' Return  When 0 Then قیمت صفر است When 1 Then جایزه است When 2 Then ثبت End
        SabteTaghirat_Fee = 0

        Dim cnSQL As New SqlConnection
        Dim cmSQL As New SqlCommand
        Dim strSQL As String = ""

        If Fee <= 0 Then
            SabteTaghirat_Fee = 0
            Exit Function
        End If

        If objTools.DLookup("IsJayezeh", "tblFO_PishFaktorSatr", "ccPishFaktorSatr = " & ccPishFaktorSatr) Then
            SabteTaghirat_Fee = 1
            Exit Function
        End If

        Try
            objTools.DUpdate("Fee", "tblFO_PishFaktorSatr", Fee, "ccPishFaktorSatr = " & ccPishFaktorSatr)
            ''''
            Dim DarsadTakhfifDasti As String
            DarsadTakhfifDasti = objTools.ConvertNulls(objTools.DLookup("DarsadTakhfifDasti", "tblFO_PishFaktorSatr", "ccPishFaktorSatr = " & ccPishFaktorSatr), 0)
            Dim mkol3 As String = objTools.ConvertNulls(objTools.DLookup("mkol3", "tblFO_PishFaktorSatr", "ccPishFaktorSatr = " & ccPishFaktorSatr), 0)
            objTools.DUpdate("TakhfifKalaDasti", "tblFO_PishFaktorSatr", ((DarsadTakhfifDasti * (mkol3)) / 100), "ccPishFaktorSatr = " & ccPishFaktorSatr)

            SabteTaghirat_Fee = 2

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> SabteTaghirat_Fee ")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> SabteTaghirat_Fee ")
        End Try
    End Function
    Private Sub btnTaeed_Click(sender As Object, e As EventArgs) Handles btnTaeed.Click
        Dim strTafkik_GG As String = ""

        If MsgBox("آیا به انتخاب خود اطمینان دارید ؟ ", MsgBoxStyle.Question + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.YesNo, "") = MsgBoxResult.No Then
            Exit Sub
        End If

        For i As Integer = 0 To GridEXTafkik_Eslah.RowCount - 1
            If GridEXTafkik_Eslah.GetRow(i).Cells("Taeed").Value = True Then
                strTafkik_GG += "," & Val(GridEXTafkik_Eslah.GetRow(i).Cells("ccTafkik_GG").Text.Replace(",", ""))
            End If
        Next

        If strTafkik_GG = "" Then
            If cmbVazeiat_Eslah.SelectedIndex = 0 Then
                MsgBox("هیچ تفکیکی جهت ارسـال انتخاب نشده است .", MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.OkOnly, "")

            ElseIf cmbVazeiat_Eslah.SelectedIndex = 1 Then
                MsgBox("هیچ تفکیکی جهت تایید انتخاب نشده است .", MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.OkOnly, "")
            End If

            Exit Sub
        Else
            strTafkik_GG += ","

            If cmbVazeiat_Eslah.SelectedIndex = 0 Then
                If ErsalOrTaeedTafkik(strTafkik_GG, 1) = True Then '' 1 = Ersal Tafkik
                    MsgBox("وضعیت تفکیک های انتخاب شده به ارسال شده تغییر داده شد .", MsgBoxStyle.Information + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.OkOnly, "")
                End If


                If objTools.ConvertNulls(objTools.DLookup("SmsGheirGhateei", "tblGl_Sysconfig", "CodeMahal=" & CodeMahalFaal), 0) = 2 Then
                    Dim frmSendSms As New SendSms
                    frmSendSms.strTafkik_GG = strTafkik_GG
                    'frmSendSms.CountTafkikSelected = CountTafkikSelected
                    Me.Hide()
                    frmSendSms.ShowDialog(Me)
                    Me.Show()
                End If



            ElseIf cmbVazeiat_Eslah.SelectedIndex = 1 Then
                If ErsalOrTaeedTafkik(strTafkik_GG, 2) = True Then '' 2 = Taeed Tafkik
                    MsgBox("تاییـد با موفقیت انجـام شد .", MsgBoxStyle.Information + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.OkOnly, "")
                End If
            End If
            End If

        SearchTafkik_Eslah(cmbVazeiat_Eslah.SelectedIndex)
    End Sub
    Private Function ErsalOrTaeedTafkik(ByVal strTafkik_GG As String, ByVal sVazeiat As Integer) As Boolean
        '' sVazeiat ---> . = Bedone Vazeiat, 1 = Ersal Shodeh, 2 = Taeed Shodeh
        ErsalOrTaeedTafkik = False

        Dim cnSQL As New SqlConnection
        Dim cmSQL As New SqlCommand
        Dim strSQL As String = ""

        Try
            strSQL = "Sales.spPishFaktorGheireGhateei_UpdateVazeiatTafkik "

            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            cmSQL.Parameters.AddWithValue("strTafkik_GG", strTafkik_GG)
            cmSQL.Parameters.AddWithValue("sVazeiat", sVazeiat)

            cmSQL.ExecuteNonQuery()

            cmSQL = Nothing
            cnSQL.Close()

            ErsalOrTaeedTafkik = True
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> ErsalOrTaeedTafkik ")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> ErsalOrTaeedTafkik ")
        End Try
    End Function
    Private Sub btnSodorFaktor_Click(sender As Object, e As EventArgs) Handles btnSodorFaktor.Click
        If chkSodorAzPishFaktor.Checked = False Then
            SodorFromTafkik()
        Else
            SodorFromPishFaktor()
        End If
    End Sub
    Private Function Calc_Takhfif_Jayezeh_FromTafkik(ByVal strTafkik_GG As String) As Boolean
        Calc_Takhfif_Jayezeh_FromTafkik = False

        Dim cnSQL As New SqlConnection
        Dim cmSQL As New SqlCommand
        Dim daSQL As New SqlDataAdapter
        Dim dt As New DataTable
        Dim dr As DataRow
        Dim strSQL As String = ""

        Try
            strSQL = "Sales.spPishFaktorGheireGhateei_SearchPishFaktorTafkik_ForTakhfifJayezeh "

            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            cmSQL.Parameters.AddWithValue("strTafkik_GG", strTafkik_GG)

            daSQL = New SqlDataAdapter(cmSQL)
            daSQL.Fill(dt)

            cmSQL = Nothing
            cnSQL.Close()

            For Each dr In dt.Rows
                Dim TJ As New TakhfifOJavaiez.TakhfifJayezeh(CInt(dr("ccPishFaktorTitr")), TakhfifOJavaiez.TakhfifJayezeh.ApplyOnTypes.PishFaktor)
                TJ.ApplyTakhfifJayezeh()

                If dr("Malyat") Then
                    ApplyMalyatAvarez(dr("ccPishFaktorTitr"))
                End If
            Next

            Calc_Takhfif_Jayezeh_FromTafkik = True
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> Calc_Takhfif_Jayezeh_FromTafkik ")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> Calc_Takhfif_Jayezeh_FromTafkik ")
        End Try
    End Function
    '--------------- Ramezani 1395/12/09 
    'Private Function SendSms_ForFaktorHaye_Tafkik(ByVal strTafkik_GG As String) As Boolean
    '    SendSms_ForFaktorHaye_Tafkik = False

    '    Dim cnSQL As New SqlConnection
    '    Dim cmSQL As New SqlCommand
    '    Dim daSQL As New SqlDataAdapter
    '    Dim dt As New DataTable
    '    Dim dr As DataRow
    '    Dim strSQL As String = ""

    '    Try
    '        strSQL = "Sales.spPishFaktorGheireGhateei_SearchFaktorTafkik_ForSendSms "

    '        cnSQL = New SqlConnection(ConnectionString)
    '        cnSQL.Open()

    '        cmSQL = New SqlCommand(strSQL, cnSQL)
    '        cmSQL.CommandType = CommandType.StoredProcedure
    '        cmSQL.Parameters.Clear()

    '        cmSQL.Parameters.AddWithValue("strTafkik_GG", strTafkik_GG)

    '        daSQL = New SqlDataAdapter(cmSQL)
    '        daSQL.Fill(dt)

    '        cmSQL = Nothing
    '        cnSQL.Close()

    '        For Each dr In dt.Rows
    '            If dr("Mobile") <> "" Then
    '                Dim Mobile As String = dr("Mobile")
    '                If Mobile.Length <> 0 Then
    '                    Dim MessageStatus = objTools.ConvertNulls(objTools.DLookup("MessageStatus", "tblGl_Sysconfig", "CodeMahal=" & CodeMahalFaal), 0)
    '                    If MessageStatus <> 0 Then
    '                        Dim messageforMoshtary = objTools.ConvertNulls(objTools.DLookup("MessageForFaktor", "tblGl_Sysconfig", "CodeMahal=" & CodeMahalFaal), 0)
    '                        If messageforMoshtary <> 0 Then
    '                            Dim phone As String() = {Mobile}

    '                            Dim MessageText As String = objTools.ConvertNulls(objTools.DLookup("TextMessageForFaktor", "tblGL_SysConfig", ""), "") + " " + CType(dr("JamKol"), String)

    '                            If MessageStatus = 1 Then
    '                                If MessageText <> "" Then
    '                                    If MsgBox("آیا مایلید پیام برای مشتری ارسال شود؟", MsgBoxStyle.YesNo + MsgBoxStyle.Question + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.DefaultButton2, "SMS") = MsgBoxResult.Yes Then
    '                                        objSms.SentSms(phone, MessageText)
    '                                    End If
    '                                End If
    '                            ElseIf MessageStatus = 2 Then
    '                                If MessageText <> "" Then
    '                                    objSms.SentSms(phone, MessageText)
    '                                End If

    '                            End If
    '                        End If
    '                    End If
    '                End If
    '            End If
    '        Next

    '        SendSms_ForFaktorHaye_Tafkik = True
    '    Catch sqlExc As SqlException
    '        MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> Calc_Takhfif_Jayezeh_FromTafkik ")
    '    Catch ex As Exception
    '        MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> Calc_Takhfif_Jayezeh_FromTafkik ")
    '    End Try
    'End Function
    '----------------------- Ramezani 1395/12/09

    Private Sub ApplyMalyatAvarez(ByVal ccPishFaktor As Double)
        Dim strSQL As String = ""
        Dim cnSQL As New SqlConnection(ConnectionString)
        Dim cmSQL As New SqlCommand
        Dim daSQL As New SqlDataAdapter
        Dim dt As New DataTable
        Dim IsMalyatAvarezTakhfif As Boolean

        IsMalyatAvarezTakhfif = objTools.ConvertNulls(objTools.DLookup("IsMalyatAvarezTakhfif", "tblGL_SysConfig", "CodeMahal = " & CodeMahalFaal), False)
        Dim Malyat As Double = 0
        Dim Avarez As Double = 0

        strSQL = "Sales.spPishFaktor_ApplyMalyatAvarez_Search "

        cnSQL.Open()

        cmSQL = New SqlCommand(strSQL, cnSQL)
        cmSQL.CommandType = CommandType.StoredProcedure
        cmSQL.Parameters.Clear()

        cmSQL.Parameters.AddWithValue("ccPishFaktorTitr", ccPishFaktor)

        daSQL = New SqlDataAdapter(cmSQL)

        Try
            daSQL.Fill(dt)
            For Each dr As DataRow In dt.Rows

                Dim a As Double = objTools.ConvertNulls(objTools.DLookup("TakhfifKala", "tblFO_PishFaktorsatr", "ccPishFaktorsatr = " & dr("ccPishFaktorSatr")), 0)

                If objTools.DLookup("EffectTakhfifFaktorInCalcMalyatAvarez_BranchZar", "tblGL_SysConfig", "CodeMahal = " & CodeMahalFaal) = True Then
                    Dim Takhfif As Double = 0
                    Dim JamPishFaktor As Double = 0
                    Dim JamTakhfifKala As Double = 0
                    Dim DarsadTakhfifFaktor As Double = 0
                    Dim MablaghForCalcMalyatAvarez As Double = 0


                    Takhfif = objTools.DLookup("CAST(Takhfif AS FLOAT)", "tblFO_PishFaktor", "ccPishFaktorTitr = " & ccPishFaktor)
                    JamPishFaktor = objTools.DLookup("CAST(JamPishFaktor AS FLOAT)", "tblFO_PishFaktor", "ccPishFaktorTitr = " & ccPishFaktor)
                    JamTakhfifKala = objTools.DLookup("CAST(JamTakhfifKala AS FLOAT)", "tblFO_PishFaktor", "ccPishFaktorTitr = " & ccPishFaktor)

                    DarsadTakhfifFaktor = (Takhfif * 100) / (JamPishFaktor - JamTakhfifKala)

                    MablaghForCalcMalyatAvarez = dr("MKOL3") - a - ((dr("MKOL3") - a) * DarsadTakhfifFaktor / 100)

                    Malyat = Math.Round(ObjCode.GetMablaghMalyat(MablaghForCalcMalyatAvarez), 0)
                    Avarez = Math.Round(ObjCode.GetMablaghAvarez(MablaghForCalcMalyatAvarez), 0)
                Else
                    Malyat = Math.Round(ObjCode.GetMablaghMalyat(dr("MKOL3") - a), 0)
                    Avarez = Math.Round(ObjCode.GetMablaghAvarez(dr("MKOL3") - a), 0)
                End If

                Dim TakhfifMalyatAvarez As Double = Malyat + Avarez

                strSQL = "Sales.spPishFaktor_ApplyMalyatAvarez_UpdateMablaghMalyat "

                cmSQL = New SqlCommand(strSQL, cnSQL)
                cmSQL.CommandType = CommandType.StoredProcedure
                cmSQL.Parameters.Clear()

                cmSQL.Parameters.AddWithValue("ccPishFaktorSatr", dr("ccPishFaktorSatr"))
                cmSQL.Parameters.AddWithValue("MablaghMalyat", Malyat)

                cmSQL.ExecuteNonQuery()

                '----------------------------------------------------------------------------------------

                strSQL = "Sales.spPishFaktor_ApplyMalyatAvarez_UpdateMablaghAvarez "

                cmSQL = New SqlCommand(strSQL, cnSQL)
                cmSQL.CommandType = CommandType.StoredProcedure
                cmSQL.Parameters.Clear()

                cmSQL.Parameters.AddWithValue("ccPishFaktorSatr", dr("ccPishFaktorSatr"))
                cmSQL.Parameters.AddWithValue("MablaghAvarez", Avarez)

                cmSQL.ExecuteNonQuery()

                '----------------------------------------------------------------------------------------

                strSQL = "Sales.spPishFaktor_ApplyMalyatAvarez_UpdateTakhfifMalyatAvarez "

                cmSQL = New SqlCommand(strSQL, cnSQL)
                cmSQL.CommandType = CommandType.StoredProcedure
                cmSQL.Parameters.Clear()

                cmSQL.Parameters.AddWithValue("ccPishFaktorSatr", dr("ccPishFaktorSatr"))
                cmSQL.Parameters.AddWithValue("TakhfifMalyatAvarez", 0)

                cmSQL.ExecuteNonQuery()

                '----------------------------------------------------------------------------------------

                If IsMalyatAvarezTakhfif = True Then
                    strSQL = "Sales.spPishFaktor_ApplyMalyatAvarez_UpdateTakhfifMalyatAvarez "

                    cmSQL = New SqlCommand(strSQL, cnSQL)
                    cmSQL.CommandType = CommandType.StoredProcedure
                    cmSQL.Parameters.Clear()

                    cmSQL.Parameters.AddWithValue("ccPishFaktorSatr", dr("ccPishFaktorSatr"))
                    cmSQL.Parameters.AddWithValue("TakhfifMalyatAvarez", TakhfifMalyatAvarez)

                    cmSQL.ExecuteNonQuery()

                End If
            Next
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> ApplyMalyatAvarez ")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> ApplyMalyatAvarez ")
        End Try
    End Sub
    Private Function SodorFaktor_FromTafkik(ByVal strTafkik_GG As String) As Boolean
        SodorFaktor_FromTafkik = False

        Dim cnSQL As New SqlConnection
        Dim cmSQL As New SqlCommand
        Dim strSQL As String = ""

        Try
            strSQL = "Sales.spPishFaktorGheireGhateei_SodorFaktor_FromTafkik "

            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            cmSQL.Parameters.AddWithValue("strTafkik_GG", strTafkik_GG)
            cmSQL.Parameters.AddWithValue("CodeDoreh", CodeDoreh)
            cmSQL.Parameters.AddWithValue("CodeMahal", CodeMahalFaal)
            cmSQL.Parameters.AddWithValue("UserName", UserName)
            cmSQL.Parameters.AddWithValue("Tarikh", TarikhEmrooz)
            cmSQL.Parameters.AddWithValue("Saat", Format(TimeOfDay, "HH:mm:ss"))

            cmSQL.ExecuteNonQuery()
          

            cmSQL = Nothing
            cnSQL.Close()

            SodorFaktor_FromTafkik = True
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> SodorFaktor_FromTafkik ")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> SodorFaktor_FromTafkik ")
        End Try
    End Function

    Private Sub cmbVazeiat_Eslah_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbVazeiat_Eslah.SelectedIndexChanged
        SearchTafkik_Eslah(cmbVazeiat_Eslah.SelectedIndex)

        If cmbVazeiat_Eslah.SelectedIndex = 0 Then
            btnSabteTaghirat.Enabled = False
            btnSabtTaghirGheymat.Enabled = False
            btnPrint.Visible = False
            btnTaeed.Text = "ارســال برگـه تفکیک"
            tsmiUpdateToBedoneVazeiat.Visible = False
            tsmiRemoveTafkik.Visible = True
            tsmiDeletePishFaktor.Visible = True
            tsmiAddPishFaktor.Visible = True
            tsmiTahvil.Visible = False
            tsmiAdameTahvil.Visible = False
            tsmiAddKala.Visible = False
            tsmiChangeNoePardakht.Visible = False
            tsmiElatTaghireTedad_Jozei.Visible = False
            tsmiElamMarjoee.Visible = False
            tsmiChangeInfoTafkik.Visible = True
            tsmiPrintPishFaktorTafkik.Visible = True
            btnPrint.Visible = True
        ElseIf cmbVazeiat_Eslah.SelectedIndex = 1 Then
            btnSabteTaghirat.Enabled = True
            If AllowChangeFeePishFaktor = True Then
                btnSabtTaghirGheymat.Enabled = True
            End If
            btnPrint.Visible = True
            btnTaeed.Text = "تاییـــد برگـه تفکیک"
            tsmiUpdateToBedoneVazeiat.Visible = True
            tsmiRemoveTafkik.Visible = False
            tsmiDeletePishFaktor.Visible = False
            tsmiAddPishFaktor.Visible = False
            tsmiAddKala.Visible = False
            tsmiChangeNoePardakht.Visible = True
            tsmiElatTaghireTedad_Jozei.Visible = True
            tsmiElamMarjoee.Visible = True
            tsmiChangeInfoTafkik.Visible = False
            tsmiPrintPishFaktorTafkik.Visible = False
            btnPrint.Visible = False
        End If

        SearchTafkik_Eslah(cmbVazeiat_Eslah.SelectedIndex)
    End Sub
    Private Sub tsmiAddPishFaktor_Click(sender As Object, e As EventArgs) Handles tsmiAddPishFaktor.Click
        Dim frmAddPishFaktor As New frmFO_AddPishFaktor
        If cmTafkik_Eslah.Position = -1 Then Exit Sub

        If objTools.DLookup("sVazeiat", "Sales.TafkikJoze_PishFaktor", "ccTafkik_GG = " & Val(GridEXTafkik_Eslah.CurrentRow.Cells("ccTafkik_GG").Text.Replace(",", ""))) <> 0 Then
            MsgBox("این تفکیک ارسال شده است . امکان افزودن پیش فاکتور جدید به آن وجود ندارد .", MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " خـطا")
            Exit Sub
        Else
            Dim frm As New frmFO_AddPishFaktor
            frm.frm_ccTafkik_GG = Val(GridEXTafkik_Eslah.CurrentRow.Cells("ccTafkik_GG").Text.Replace(",", ""))

            Me.Hide()
            frm.ShowDialog()
            Me.Show()

            SearchPishFaktorTafkik_Eslah()
        End If
    End Sub

    Private Sub tsmiDeletePishFaktor_Click(sender As Object, e As EventArgs) Handles tsmiDeletePishFaktor.Click
        If objTools.DLookup("sVazeiat", "Sales.TafkikJoze_PishFaktor", "ccTafkik_GG = " & Val(GridEXTafkik_Eslah.CurrentRow.Cells("ccTafkik_GG").Text.Replace(",", ""))) <> 0 Then
            MsgBox("این تفکیک ارسال شده است . امکان حذف پیش فاکتورهای آن وجود ندارد .", MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " خـطا")
            Exit Sub
        Else
            If objTools.DCount("ccTafkik_GG", "Sales.TafkikJozeSatr_PishFaktor", "ccTafkik_GG = " & Val(GridEXTafkik_Eslah.CurrentRow.Cells("ccTafkik_GG").Text.Replace(",", ""))) = 1 Then
                If MsgBox("در صورت حذف این پیش فاکتور ، تعداد پیش فاکتورهای تکیک صفر خواهد شد و برگه تفکیک حذف میگردد ." & _
                          " آیا پیش فاکتور حذف گردد ؟ ", MsgBoxStyle.Question + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.YesNo, "") = MsgBoxResult.Yes Then
                    If DeletePishFaktor(Val(GridEXPishFaktor_Eslah.CurrentRow.Cells("ccPishFaktorTitr").Text.Replace(",", ""))) Then
                        objTools.DDelete("Sales.TafkikJoze_PishFaktor", "ccTafkik_GG = " & Val(GridEXTafkik_Eslah.CurrentRow.Cells("ccTafkik_GG").Text.Replace(",", "")))
                        SearchTafkik_Eslah(cmbVazeiat_Eslah.SelectedIndex)
                        SearchPishFaktorTafkik_Eslah()
                    End If
                End If
            Else
                If DeletePishFaktor(Val(GridEXPishFaktor_Eslah.CurrentRow.Cells("ccPishFaktorTitr").Text.Replace(",", ""))) Then
                    SearchPishFaktorTafkik_Eslah()
                End If
            End If

        End If
    End Sub
    Private Function DeletePishFaktor(ByVal ccPishFaktorTitr As Integer) As Boolean
        DeletePishFaktor = False

        Dim cnSQL As New SqlConnection
        Dim cmSQL As New SqlCommand
        Dim strSQL As String = ""

        Try
            strSQL = "Sales.spPishFaktorGheireGhateei_DeletePishFaktor "

            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            cmSQL.Parameters.AddWithValue("ccPishFaktorTitr", ccPishFaktorTitr)

            cmSQL.ExecuteNonQuery()

            cmSQL = Nothing
            cnSQL.Close()

            DeletePishFaktor = True
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> SodorFaktor ")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> SodorFaktor ")
        End Try
    End Function
    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Dim strTafkik_GG As String = ""
        Dim CountTafkikSelected As Integer = 0

        For i As Integer = 0 To GridEXTafkik_Eslah.RowCount - 1
            If GridEXTafkik_Eslah.GetRow(i).Cells("Taeed").Value = True Then
                strTafkik_GG &= "," & Val(GridEXTafkik_Eslah.GetRow(i).Cells("ccTafkik_GG").Text.Replace(",", ""))
                CountTafkikSelected += 1
            End If
        Next

        If CountTafkikSelected = 0 Then
            MsgBox("هیچ تفکیکی جهت چـــاپ انتخاب نشده است .", MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.OkOnly, "")
            Exit Sub
        Else
            strTafkik_GG &= ","
            PrintTafkik_Kala(strTafkik_GG)
        End If
    End Sub
    Private Sub PrintTafkik_Kala(ByVal strTafkik_GG As String)
        Dim cnSQL As New SqlConnection
        Dim cmSQL As New SqlCommand
        Dim daSQL As New SqlDataAdapter
        Dim strSQL As String = ""

        If dsForm.Tables.Contains("tblPrint") Then
            dsForm.Tables.Remove("tblPrint")
        End If

        Try
            strSQL = "Sales.spPishFaktorGheireGhateei_PrintTafkik_Kala"

            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            cmSQL.Parameters.AddWithValue("strTafkik_GG", strTafkik_GG)

            daSQL = New SqlDataAdapter(cmSQL)
            daSQL.Fill(dsForm, "tblPrint")

            Dim rpt As New CrystalDecisions.CrystalReports.Engine.ReportDocument
            Dim rpttables As CrystalDecisions.CrystalReports.Engine.Tables
            Dim rptformula As CrystalDecisions.CrystalReports.Engine.FormulaFieldDefinitions
            Dim frm As New Forms_dll.frmGL_Gozaresh

            rpt.Load(rptPath & "\rptFO_GozareshPishFaktorGheireGhateei_Tafkik_Kala.rpt")

            rpttables = rpt.Database.Tables
            rpttables.Item(0).SetDataSource(dsForm.Tables("tblPrint"))

            rptformula = rpt.DataDefinition.FormulaFields
            With rptformula
                .Item("ccTafkik_GG").Text = "{mydata.ccTafkik_GG}"

                .Item("ShomarehTafkik").Text = "{mydata.ShomarehTafkik}"

                .Item("NameMamorPakhsh").Text = "{mydata.NameMamorPakhsh}"
                .Item("NameRanandeh").Text = "{mydata.NameRanandeh}"
                .Item("CodeKala").Text = "{mydata.CodeKala}"
                .Item("NameKala").Text = "{mydata.NameKala}"
                .Item("TedadTafkikShodeh").Text = "{mydata.TedadTafkikShodeh}"
                .Item("TedadKartonTafkikShodeh").Text = "{mydata.TedadKartonTafkikShodeh}"
                '.Item("TedadBastehTafkikShodeh").Text = "{mydata.TedadBastehTafkikShodeh}"
                '.Item("TedadKhordTafkikShodeh").Text = "{mydata.TedadKhordTafkikShodeh}"
                .Item("TedadSabtShodeh").Text = "{mydata.TedadSabtShodeh}"
                .Item("TedadKartonSabtShodeh").Text = "{mydata.TedadKartonSabtShodeh}"
                '.Item("TedadBastehSabtShodeh").Text = "{mydata.TedadBastehSabtShodeh}"
                '.Item("TedadKhordSabtShodeh").Text = "{mydata.TedadKhordSabtShodeh}"
                .Item("TedadEkhtelaf").Text = "{mydata.TedadEkhtelaf}"
                .Item("TedadKartonEkhtelaf").Text = "{mydata.TedadKartonEkhtelaf}"
                '.Item("TedadBastehEkhtelaf").Text = "{mydata.TedadBastehEkhtelaf}"
                '.Item("TedadKhordEkhtelaf").Text = "{mydata.TedadKhordEkhtelaf}"
                .Item("strPishFaktorInTafkik").Text = "{mydata.strPishFaktorTafkik}"
                .Item("VaznKarton").Text = "{mydata.VaznKarton}"

                .Item("Title").Text = "'" & "تفکیک پیش فاکتور" & "'"
                .Item("Title2").Text = "'" & NameSherkat & "'"
                .Item("Title3").Text = "'" & NameMahalFaal & "'"
                .Item("KarbarGozaresh").Text = "'" & PersonelName & "'"
                .Item("TarikhGozaresh").Text = "'" & objTarikh.SetDateSlash(TarikhEmrooz) & "'"
                .Item("SaatGozaresh").Text = "'" & Format(TimeOfDay, "HH:mm:ss") & "'"
            End With
            rpt.Refresh()

            frm.Text = txtCaption

            With frm.CRV
                .ReportSource = rpt
                .DisplayGroupTree = False
                .ShowGroupTreeButton = False
                .Zoom(75)
                If Not objSec.HaveUserPermission(SN, UD_Dll.Security.ePermissions.Print) Then .ShowPrintButton = False
                If Not objSec.HaveUserPermission(SN, UD_Dll.Security.ePermissions.Export) Then .ShowExportButton = False
            End With


            Me.Hide()
            frm.ShowDialog(Me)
            frm = Nothing
            daSQL = Nothing
            rpt = Nothing
            Me.Show()
            Windows.Forms.Cursor.Current = Cursors.Default

            cnSQL.Close()

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> PrintTafkik ")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> PrintTafkik ")
        End Try
    End Sub
    Private Sub PrintPishFaktor(ByVal strTafkik_GG As String, ByVal Type As Integer)
        Dim cnSQL As New SqlConnection
        Dim cmSQL As New SqlCommand
        Dim daSQL As New SqlDataAdapter
        Dim strSQL As String = ""

        If dsForm.Tables.Contains("tblPrint") Then
            dsForm.Tables.Remove("tblPrint")
        End If

        Try
            strSQL = "Sales.spPishFaktorGheireGhateei_PrintTafkik_Kala"

            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            cmSQL.Parameters.AddWithValue("strTafkik_GG", strTafkik_GG)

            daSQL = New SqlDataAdapter(cmSQL)
            daSQL.Fill(dsForm, "tblPrint")

            Dim rpt As New CrystalDecisions.CrystalReports.Engine.ReportDocument
            Dim rpttables As CrystalDecisions.CrystalReports.Engine.Tables
            Dim rptformula As CrystalDecisions.CrystalReports.Engine.FormulaFieldDefinitions
            Dim frm As New Forms_dll.frmGL_Gozaresh

            rpt.Load(rptPath & "\rptFO_GozareshPishFaktorGheireGhateei_Tafkik_Kala.rpt")

            rpttables = rpt.Database.Tables
            rpttables.Item(0).SetDataSource(dsForm.Tables("tblPrint"))

            rptformula = rpt.DataDefinition.FormulaFields
            With rptformula
                .Item("ccTafkik_GG").Text = "{mydata.ccTafkik_GG}"

                .Item("ShomarehTafkik").Text = "{mydata.ShomarehTafkik}"
                .Item("NameMamorPakhsh").Text = "{mydata.NameMamorPakhsh}"
                .Item("NameRanandeh").Text = "{mydata.NameRanandeh}"
                .Item("CodeKala").Text = "{mydata.CodeKala}"
                .Item("NameKala").Text = "{mydata.NameKala}"
                .Item("TedadTafkikShodeh").Text = "{mydata.TedadTafkikShodeh}"
                .Item("TedadKartonTafkikShodeh").Text = "{mydata.TedadKartonTafkikShodeh}"
                .Item("TedadSabtShodeh").Text = "{mydata.TedadSabtShodeh}"
                .Item("TedadKartonSabtShodeh").Text = "{mydata.TedadKartonSabtShodeh}"
                .Item("TedadEkhtelaf").Text = "{mydata.TedadEkhtelaf}"
                .Item("TedadKartonEkhtelaf").Text = "{mydata.TedadKartonEkhtelaf}"

                .Item("Title").Text = "'" & "تفکیک پیش فاکتور" & "'"
                .Item("Title2").Text = "'" & NameSherkat & "'"
                .Item("Title3").Text = "'" & NameMahalFaal & "'"
                .Item("KarbarGozaresh").Text = "'" & PersonelName & "'"
                .Item("TarikhGozaresh").Text = "'" & objTarikh.SetDateSlash(TarikhEmrooz) & "'"
                .Item("SaatGozaresh").Text = "'" & Format(TimeOfDay, "HH:mm:ss") & "'"
            End With
            rpt.Refresh()

            frm.Text = txtCaption

            With frm.CRV
                .ReportSource = rpt
                .DisplayGroupTree = False
                .ShowGroupTreeButton = False
                .Zoom(75)
                If Not objSec.HaveUserPermission(SN, UD_Dll.Security.ePermissions.Print) Then .ShowPrintButton = False
                If Not objSec.HaveUserPermission(SN, UD_Dll.Security.ePermissions.Export) Then .ShowExportButton = False
            End With


            Me.Hide()
            frm.ShowDialog(Me)
            frm = Nothing
            daSQL = Nothing
            rpt = Nothing
            Me.Show()
            Windows.Forms.Cursor.Current = Cursors.Default

            cnSQL.Close()

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> PrintPishFaktor ")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> PrintPishFaktor ")
        End Try
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

    Private Sub tsmiReturnToPishFaktor_Click(sender As Object, e As EventArgs) Handles tsmiReturnToPishFaktor.Click
        If MsgBox("آیا به انتخاب خود اطمینان دارید ؟", MsgBoxStyle.Question + MsgBoxStyle.YesNo + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " خـطا") = MsgBoxResult.Yes Then
            If ReturnPishFaktor(Val(GridEXPishFaktor.CurrentRow.Cells("ccPishFaktorTitr").Text.Replace(",", ""))) Then
                SearchPishFaktor()
            End If
        End If
    End Sub
    Private Function ReturnPishFaktor(ByVal ccPishFaktorTitr As Integer) As Boolean
        ReturnPishFaktor = False

        Dim cnSQL As New SqlConnection
        Dim cmSQL As New SqlCommand
        Dim strSQL As String = ""

        Try
            strSQL = "Sales.spPishFaktorGheireGhateei_ReturnPishFaktor "

            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            cmSQL.Parameters.AddWithValue("ccPishFaktorTitr", ccPishFaktorTitr)

            cmSQL.ExecuteNonQuery()

            cmSQL = Nothing
            cnSQL.Close()

            ReturnPishFaktor = True
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> SodorFaktor ")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> SodorFaktor ")
        End Try
    End Function
    Private Sub tsmiReturnToKartabl_Click(sender As Object, e As EventArgs) Handles tsmiReturnToKartabl.Click
        If UserName.ToUpper <> "ADMINISTRATOR" Then
            ObjCode.UserName = UserName
            If Not ObjCode.CheckPermission(1000217) Then
                '    MsgBox("شما دسترسی این کار را ندارید", MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " خـطا")
                Exit Sub
            End If
        End If
        If MsgBox("آیا به انتخاب خود اطمینان دارید ؟", MsgBoxStyle.Question + MsgBoxStyle.YesNo + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " خـطا") = MsgBoxResult.Yes Then
            objTools.DUpdate("sVazeiat", "tblFO_PishFaktor", "-1", "ccPishFaktorTitr = " & GridEXPishFaktor.CurrentRow.Cells("ccPishFaktorTitr").Text.Replace(",", ""))
            SearchPishFaktor()
        End If
    End Sub
    Private Sub tsmiUpdateToBedoneVazeiat_Click(sender As Object, e As EventArgs) Handles tsmiUpdateToBedoneVazeiat.Click
        If objTools.DCount("ccElamMarjoee", "tblFO_ElamMarjoee", "ccTafkik_GG = " & GridEXTafkik_Eslah.CurrentRow.Cells("ccTafkik_GG").Text.Replace(",", "")) > 0 Then
            MsgBox("برای این برگه تفکیک مرجوعی ثبت گردیده است . امکان تغییر وضعیت آن وجود ندارد .", MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.OkOnly, "")
            Exit Sub
        End If

        Dim strTafkik_GG As String = ""
        If MsgBox("آیا به انتخاب خود اطمینان دارید ؟", MsgBoxStyle.Question + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.YesNo, "") = MsgBoxResult.Yes Then


            strTafkik_GG = "," & GridEXTafkik_Eslah.CurrentRow.Cells("ccTafkik_GG").Text.Replace(",", "") & ","

            If ErsalOrTaeedTafkik(strTafkik_GG, 0) Then
                SearchTafkik_Eslah(cmbVazeiat_Eslah.SelectedIndex)
            End If
        End If
    End Sub
    Private Sub tsmiRemoveTafkik_Click(sender As Object, e As EventArgs) Handles tsmiRemoveTafkik.Click
        If cmbVazeiat_Eslah.SelectedIndex = 1 Then
            MsgBox("این تفکیک ارسال شده است . امکان حذف آن وجود ندارد .", MsgBoxStyle.Question + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.OkOnly, " خـطا")
        Else
            If MsgBox("آیا تفکیک انتخاب شده حذف گردد ؟", MsgBoxStyle.Information + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.YesNo, " ") = MsgBoxResult.Yes Then
                objTools.DDelete("Sales.TafkikJoze_PishFaktor", "ccTafkik_GG = " & Val(GridEXTafkik_Eslah.CurrentRow.Cells("ccTafkik_GG").Text.Replace(",", "")))
                SearchTafkik_Eslah(cmbVazeiat_Eslah.SelectedIndex)
            End If
        End If

    End Sub
    Private Sub tsmiTahvil_Click(sender As Object, e As EventArgs) Handles tsmiTahvil.Click
        If cmPishFaktor_Kala_Eslah.Position = -1 Then Exit Sub
        If GridEXPishFaktor_Eslah.CurrentRow.Cells("IsTahvil").Value = True Then
            MsgBox("وضعیت پیش فاکتور انتخاب شده « تحویل داده شده » می باشد !!!", MsgBoxStyle.Information + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.OkOnly, "خطا")
            Exit Sub
        End If

        Dim ccTafkik_GG As Integer = 0
        Dim ccPishFaktorTitr As Integer = 0

        ccTafkik_GG = Val(GridEXTafkik_Eslah.CurrentRow.Cells("ccTafkik_GG").Text.Replace(",", ""))
        ccPishFaktorTitr = Val(GridEXPishFaktor_Eslah.CurrentRow.Cells("ccPishFaktorTitr").Text.Replace(",", ""))

        If IsValidMojodiForUpdateVazeiatPishFaktorToTahvilDadehShodeh(ccTafkik_GG, ccPishFaktorTitr) = False Then
            objTools.DUpdate("IsTahvil", "Sales.TafkikJozeSatr_PishFaktor", "1", "ccPishFaktorTitr = " & ccPishFaktorTitr)
            objTools.DUpdate("Tedad3", "tblFO_PishFaktorSatr", "Tedad1", "ccPishFaktorTitr = " & Val(GridEXPishFaktor_Eslah.CurrentRow.Cells("ccPishFaktorTitr").Text.Replace(",", "")))
            objTools.DUpdate("sElatTaghirTedad", "Sales.TafkikJozeSatr_PishFaktor_Kala", 0, "ccTafkikSatr_GG = " & Val(GridEXPishFaktor_Eslah.CurrentRow.Cells("ccTafkikSatr_GG").Text.Replace(",", "")))
            SearchPishFaktorTafkik_Eslah()
            SearchKalaPishFaktor_Eslah()
        Else
            MsgBox("در برگه تفکیک موجودی کافی برای بازگرداندن پیش فاکتور به حالت « تحویل داده شده » وجود ندارد !", MsgBoxStyle.Question + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.OkOnly, " خـطا")
        End If
    End Sub
    Private Function IsValidMojodiForUpdateVazeiatPishFaktorToTahvilDadehShodeh(ByVal ccTafkik_GG As Integer, ByVal ccPishFaktorTitr As Integer) As Boolean
        IsValidMojodiForUpdateVazeiatPishFaktorToTahvilDadehShodeh = False

        Dim cnSQL As New SqlConnection
        Dim cmSQL As New SqlCommand
        Dim strSQL As String = ""

        Try
            strSQL = "Sales.spPishFaktorGheireGhateei_IsValidUpdateVazeiatPishFaktorToTahvil "

            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            cmSQL.Parameters.AddWithValue("ccTafkik_GG", ccTafkik_GG)
            cmSQL.Parameters.AddWithValue("ccPishFaktorTitr", ccPishFaktorTitr)
            cmSQL.Parameters.AddWithValue("IsValid", IsValidMojodiForUpdateVazeiatPishFaktorToTahvilDadehShodeh)

            cmSQL.ExecuteNonQuery()

            IsValidMojodiForUpdateVazeiatPishFaktorToTahvilDadehShodeh = cmSQL.Parameters("IsValid").Value

            cnSQL.Close()

            Return IsValidMojodiForUpdateVazeiatPishFaktorToTahvilDadehShodeh
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> IsValidMojodiForUpdateVazeiatPishFaktorToTahvilDadehShodeh ")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> IsValidMojodiForUpdateVazeiatPishFaktorToTahvilDadehShodeh ")
        End Try
    End Function
    Private Sub tsmiAdameTahvil_Click(sender As Object, e As EventArgs) Handles tsmiAdameTahvil.Click
        If cmPishFaktor_Kala_Eslah.Position = -1 Then Exit Sub

        If GridEXPishFaktor_Eslah.CurrentRow.Cells("IsEzafeBar").Value = True Then
            MsgBox("پیش فاکتور انتخاب شده «اضافه بار » می باشد و امکان تغییر وضعیت آن وجود ندارد .", MsgBoxStyle.Information + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.OkOnly, "خطا")
            Exit Sub
        End If

        If objTools.DCount("ccPishFaktorTitr", "tblFO_PishFaktorSatr", "Tedad1 <> Tedad3 AND ccPishFaktorTitr = " & Val(GridEXPishFaktor_Eslah.CurrentRow.Cells("ccPishFaktorTitr").Text.Replace(",", ""))) = 0 Then
            If GridEXPishFaktor_Eslah.CurrentRow.Cells("IsTahvil").Value = False Then
                MsgBox("وضعیت پیش فاکتور انتخاب شده « عـدم تحویل » می باشد !!!", MsgBoxStyle.Information + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.OkOnly, "خطا")
                Exit Sub
            End If

            Dim frm As New frmFO_ElatTaghireTedadKala
            frm.IsKoli = True
            frm.frm_ccTafkikSatr_GG = Val(GridEXPishFaktor_Eslah.CurrentRow.Cells("ccTafkikSatr_GG").Text.Replace(",", ""))

            Me.Hide()
            frm.ShowDialog()
            Me.Show()

            If frm.Flg = True Then
                objTools.DUpdate("IsTahvil", "Sales.TafkikJozeSatr_PishFaktor", "0", "ccPishFaktorTitr = " & Val(GridEXPishFaktor_Eslah.CurrentRow.Cells("ccPishFaktorTitr").Text.Replace(",", "")))
                objTools.DUpdate("Tedad3", "tblFO_PishFaktorSatr", "0", "ccPishFaktorTitr = " & Val(GridEXPishFaktor_Eslah.CurrentRow.Cells("ccPishFaktorTitr").Text.Replace(",", "")))
                SearchPishFaktorTafkik_Eslah()
                SearchKalaPishFaktor_Eslah()
            Else
                MsgBox("به سبب عدم انتخاب « علت » تغییرات ثبت نگردید !!!", MsgBoxStyle.Information + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.OkOnly, "خطا")
                Exit Sub
            End If
        Else
            MsgBox("کالاهای این پیش فاکتور تغییر کرده است . نمی توانید وضعیت آنرا به « عـدم تحویل » تغییر دهید !", MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.OkOnly, "")
        End If
    End Sub
    Private Sub GridEXPishFaktor_Sodor_Click(sender As Object, e As EventArgs) Handles GridEXPishFaktor_Sodor.Click
        BoundCurrencyManagerPishFaktorTafkik_Sodor()
    End Sub
    Private Sub chkSodorAzPishFaktor_CheckedChanged(sender As Object, e As EventArgs) Handles chkSodorAzPishFaktor.CheckedChanged
        SearchTafkik_Sodor()
    End Sub
    Private Sub btnCalcTakhfif_Jayezeh_Click(sender As Object, e As EventArgs) Handles btnCalcTakhfif_Jayezeh.Click
        If objTools.DLookup("AllowPishFaktorTakhfifDasty", "tblGL_SysConfig", "CodeMahal = " & CodeMahalFaal) = True Then
            MsgBox("تنظیمات محاسبه تخفیف و جایزه به صورت دستی می باشد !", MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.OkOnly, "")
            Exit Sub
        End If

        If chkSodorAzPishFaktor.Checked = False Then
            CalcTakhfif_Jayezeh_OnTafkik()
        Else
            CalcTakhfif_Jayezeh_OnPishFaktor()
        End If
    End Sub
    Private Function SodorFaktor_FromPishFaktor(ByVal ccTafkik_GG As Integer, ByVal strPishFaktor As String) As Integer
        SodorFaktor_FromPishFaktor = 0

        Dim cnSQL As New SqlConnection
        Dim cmSQL As New SqlCommand
        Dim strSQL As String = ""
        Dim FlgEndPishFaktorFaktorNoFaktor As Integer = 0

        Try
            strSQL = "Sales.spPishFaktorGheireGhateei_SodorFaktor_FromPishFaktor "

            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            cmSQL.Parameters.AddWithValue("ccTafkik_GG", ccTafkik_GG)
            cmSQL.Parameters.AddWithValue("strPishFaktor", strPishFaktor)
            cmSQL.Parameters.AddWithValue("CodeDoreh", CodeDoreh)
            cmSQL.Parameters.AddWithValue("CodeMahal", CodeMahalFaal)
            cmSQL.Parameters.AddWithValue("UserName", UserName)
            cmSQL.Parameters.AddWithValue("Tarikh", TarikhEmrooz)
            cmSQL.Parameters.AddWithValue("Saat", Format(TimeOfDay, "HH:mm:ss"))
            cmSQL.Parameters.AddWithValue("FlgEndPishFaktorFaktorNoFaktor", FlgEndPishFaktorFaktorNoFaktor)
            cmSQL.Parameters("FlgEndPishFaktorFaktorNoFaktor").Direction = ParameterDirection.Output

            cmSQL.ExecuteNonQuery()

            FlgEndPishFaktorFaktorNoFaktor = cmSQL.Parameters("FlgEndPishFaktorFaktorNoFaktor").Value

            cmSQL = Nothing
            cnSQL.Close()

            SodorFaktor_FromPishFaktor = 1

            Return SodorFaktor_FromPishFaktor + FlgEndPishFaktorFaktorNoFaktor
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> SodorFaktor_FromPishFaktor ")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> SodorFaktor_FromPishFaktor ")
        End Try
    End Function
    Private Sub SodorFromTafkik()
        Dim strTafkik_GG As String = ""
        Dim CountTafkikSelected As Integer = 0

        Try
            For i As Integer = 0 To GridEXTafkik_Sodor.RowCount - 1
                If GridEXTafkik_Sodor.GetRow(i).Cells("Taeed").Value = True Then
                    strTafkik_GG &= "," & Val(GridEXTafkik_Sodor.GetRow(i).Cells("ccTafkik_GG").Text.Replace(",", ""))
                    CountTafkikSelected += 1
                End If
            Next

            If CountTafkikSelected = 0 Then
                MsgBox("هیچ تفکیکی جهت صدور فاکتور انتخاب نشده است .", MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.OkOnly, "")
                Exit Sub
            Else
                If MsgBox("تعداد " & CountTafkikSelected & " برگه تفکیک جهت صدور فاکتورهایشان انتخاب شده اند ." & vbCrLf _
                          & " آیا صـدور فاکتورها انجام شود ؟", MsgBoxStyle.Question + MsgBoxStyle.YesNo + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, "صدور فاکتور") = MsgBoxResult.Yes Then
                    strTafkik_GG &= ","

                    If objTools.DLookup("AllowPishFaktorTakhfifDasty", "tblGL_SysConfig", "CodeMahal = " & CodeMahalFaal) = False Then
                        If Calc_Takhfif_Jayezeh_FromTafkik(strTafkik_GG) = False Then
                            Exit Sub
                        End If
                    End If

                    If SodorFaktor_FromTafkik(strTafkik_GG) = True Then
                        MsgBox("فاکتورها با موفقیت صادر گردید .", MsgBoxStyle.Information + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.OkOnly, "")


                        If objTools.ConvertNulls(objTools.DLookup("SmsGheirGhateei", "tblGl_Sysconfig", "CodeMahal=" & CodeMahalFaal), 0) = 1 Then
                            Dim frmSendSms As New SendSms
                            frmSendSms.strTafkik_GG = strTafkik_GG
                            frmSendSms.CountTafkikSelected = CountTafkikSelected
                            Me.Hide()
                            frmSendSms.ShowDialog(Me)
                            Me.Show()
                        End If

                        SearchTafkik_Sodor()

                        'If SendSms_ForFaktorHaye_Tafkik(strTafkik_GG) = True Then
                        '    MsgBox("ارسال پیامک برای مشتری ها با موفقیت انجام شد.", MsgBoxStyle.Information + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.OkOnly, "")
                        'Else
                        '    MsgBox("خطا در ارسال پیامک .", MsgBoxStyle.Information + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.OkOnly, "")
                        'End If

                    End If
                End If
            End If

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> SodorFromTafkik ")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> SodorFromTafkik ")
        End Try
    End Sub
    Private Sub SodorFromPishFaktor()
        Dim strPishFaktor As String = ""
        Dim CountPishFaktorSelected As Integer = 0
        Dim Malyat As Boolean = False
        Dim ccTafkik_GG As Integer = Val(GridEXTafkik_Sodor.CurrentRow.Cells("ccTafkik_GG").Text.Replace(",", ""))
        Dim ccPishFaktor As Integer = 0
        Dim VazeiatSodorFaktor As Integer = 0

        Try
            For i As Integer = 0 To GridEXPishFaktor_Sodor.RowCount - 1
                If GridEXPishFaktor_Sodor.GetRow(i).Cells("Taeed").Value = True Then
                    ccPishFaktor = Val(GridEXPishFaktor_Sodor.GetRow(i).Cells("ccPishFaktorTitr").Text.Replace(",", ""))

                    Dim TJ As New TakhfifOJavaiez.TakhfifJayezeh(ccPishFaktor, TakhfifOJavaiez.TakhfifJayezeh.ApplyOnTypes.PishFaktor)
                    TJ.ApplyTakhfifJayezeh()

                    If objTools.DLookup("Malyat", "tblFO_PishFaktor", "ccPishFaktorTitr = " & Val(GridEXPishFaktor_Sodor.GetRow(i).Cells("ccPishFaktorTitr").Text.Replace(",", ""))) = True Then
                        ApplyMalyatAvarez(Val(GridEXPishFaktor_Sodor.GetRow(i).Cells("ccPishFaktorTitr").Text.Replace(",", "")))
                    End If

                    strPishFaktor &= "," & ccPishFaktor

                    CountPishFaktorSelected += 1
                End If
            Next

            If CountPishFaktorSelected = 0 Then
                MsgBox("هیچ پیش فاکتوری جهت صدور فاکتور انتخاب نشده است .", MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.OkOnly, "")
                Exit Sub
            Else
                If MsgBox("تعداد " & CountPishFaktorSelected & " پیش فاکتور جهت صدور فاکتورانتخاب شده اند ." & vbCrLf _
                          & " آیا صـدور فاکتور انجام شود ؟", MsgBoxStyle.Question + MsgBoxStyle.YesNo + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, "صدور فاکتور") = MsgBoxResult.Yes Then
                    strPishFaktor &= ","

                    '' Baresi Inke Aya Tamame PishFaktorha Faktor Shodan Ya na :)

                    VazeiatSodorFaktor = SodorFaktor_FromPishFaktor(ccTafkik_GG, strPishFaktor)
                    If VazeiatSodorFaktor = 1 Then '' PishFaktor Faktor Nashodeh Vojod Darad .
                        MsgBox("فاکتورها با موفقیت صادر گردید .", MsgBoxStyle.Information + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.OkOnly, "")
                        SearchPishFaktorTafkik_Sodor()
                    ElseIf VazeiatSodorFaktor = 2 Then '' Tamame PishFaktorha Faktor shodan .
                        MsgBox("فاکتورها با موفقیت صادر گردید .", MsgBoxStyle.Information + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.OkOnly, "")
                        SearchTafkik_Sodor()
                    End If
                End If
            End If
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> SodorFromPishFaktor ")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> SodorFromPishFaktor ")
        End Try
    End Sub
    Private Sub CalcTakhfif_Jayezeh_OnTafkik()
        Dim strTafkik_GG As String = ""

        Try
            For i As Integer = 0 To GridEXTafkik_Sodor.RowCount - 1
                If GridEXTafkik_Sodor.GetRow(i).Cells("Taeed").Value = True Then
                    strTafkik_GG &= "," & Val(GridEXTafkik_Sodor.GetRow(i).Cells("ccTafkik_GG").Text.Replace(",", ""))
                End If
            Next

            If strTafkik_GG = "" Then
                MsgBox("هیچ تفکیکی جهت محاسبه تخفیف و جایزه انتخاب نشده است !", MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.OkOnly, "")
                Exit Sub
            Else
                strTafkik_GG += ","
                If Calc_Takhfif_Jayezeh_FromTafkik(strTafkik_GG) = False Then
                    Exit Sub
                Else
                    SearchPishFaktorTafkik_Sodor()
                End If
            End If
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> CalcTakhfif_Jayezeh_OnTafkik ")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> CalcTakhfif_Jayezeh_OnTafkik ")
        End Try
    End Sub
    Private Sub CalcTakhfif_Jayezeh_OnPishFaktor()
        Dim CountPishFaktorSelected As Integer = 0

        Try
            For i As Integer = 0 To GridEXPishFaktor_Sodor.RowCount - 1
                If GridEXPishFaktor_Sodor.GetRow(i).Cells("Taeed").Value = True Then
                    CountPishFaktorSelected += 1
                End If
            Next

            If CountPishFaktorSelected = 0 Then
                MsgBox("هیچ پیش فاکتوری جهت محاسبـه تخفیف و جایزه انتخاب نشده است !", MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.OkOnly, "")
                Exit Sub
            Else
                For i As Integer = 0 To GridEXPishFaktor_Sodor.RowCount - 1
                    If GridEXPishFaktor_Sodor.GetRow(i).Cells("Taeed").Value = True Then
                        Dim ccPishFaktor As Integer = 0
                        ccPishFaktor = Val(GridEXPishFaktor_Sodor.GetRow(i).Cells("ccPishFaktorTitr").Text.Replace(",", ""))

                        Dim TJ As New TakhfifOJavaiez.TakhfifJayezeh(ccPishFaktor, TakhfifOJavaiez.TakhfifJayezeh.ApplyOnTypes.PishFaktor)
                        TJ.ApplyTakhfifJayezeh()

                        If objTools.DLookup("Malyat", "tblFO_PishFaktor", "ccPishFaktorTitr = " & Val(GridEXPishFaktor_Sodor.GetRow(i).Cells("ccPishFaktorTitr").Text.Replace(",", ""))) = True Then
                            ApplyMalyatAvarez(Val(GridEXPishFaktor_Sodor.GetRow(i).Cells("ccPishFaktorTitr").Text.Replace(",", "")))
                        End If
                    End If
                Next

                SearchPishFaktorTafkik_Sodor()
            End If
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> CalcTakhfif_Jayezeh_OnPishFaktor ")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> CalcTakhfif_Jayezeh_OnPishFaktor ")
        End Try
    End Sub

    Private Sub tsmiAddKala_Click(sender As Object, e As EventArgs) Handles tsmiAddKala.Click
        Dim frmAddKala As New frmFO_AddKalaInPishFaktor
        If cmPishFaktor_Kala_Eslah.Position = -1 Then Exit Sub

        If GridEXPishFaktor_Eslah.CurrentRow.Cells("IsEzafeBar").Value = True Then
            MsgBox("پیش فاکتور انتخاب شده «اضافه بار » می باشد و امکان افزودن کالای جدید به آن وجود ندارد .", MsgBoxStyle.Information + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.OkOnly, "خطا")
            Exit Sub
        End If

        Dim frm As New frmFO_AddKalaInPishFaktor
        frm.frm_ccTafkik_GG = Val(GridEXTafkik_Eslah.CurrentRow.Cells("ccTafkik_GG").Text.Replace(",", ""))
        frm.frm_ccPishFaktor = Val(GridEXPishFaktor_Eslah.CurrentRow.Cells("ccPishFaktorTitr").Text.Replace(",", ""))

        Me.Hide()
        frm.ShowDialog()
        Me.Show()

        SearchKalaPishFaktor_Eslah()

    End Sub
    Private Sub tsmiUpdateToErsalShodeh_Click(sender As Object, e As EventArgs) Handles tsmiUpdateToErsalShodeh.Click
        Dim strTafkik_GG As String = ""

        If MsgBox("آیا به انتخاب خود اطمینان دارید ؟", MsgBoxStyle.Question + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.YesNo, "") = MsgBoxResult.Yes Then
            If objTools.DCount("ccTafkik_GG", "Sales.TafkikJozeSatr_PishFaktor", "sVazeiat = 1 AND ccTafkik_GG = " & Val(GridEXTafkik_Sodor.CurrentRow.Cells("ccTafkik_GG").Text.Replace(",", ""))) = 0 Then '' Tedad Pish Faktor haye Faktor Shodeh Tafkik
                strTafkik_GG = "," & GridEXTafkik_Sodor.CurrentRow.Cells("ccTafkik_GG").Text.Replace(",", "") & ","
                If ErsalOrTaeedTafkik(strTafkik_GG, 1) Then
                    SearchTafkik_Sodor()
                End If
            Else
                MsgBox("برخی از پیش فاکتور های این تفکیک فاکتور شده است . امکان تغییر وضعیت آن وجود ندارد .", MsgBoxStyle.Information + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.OkOnly, "خطا")
            End If

        End If
    End Sub

    Private Sub tsmiChangeNoePardakht_Click(sender As Object, e As EventArgs) Handles tsmiChangeNoePardakht.Click
        If cmPishFaktor_Kala_Eslah.Position = -1 Then Exit Sub

        If GridEXPishFaktor_Eslah.CurrentRow.Cells("IsEzafeBar").Value = True Then
            MsgBox("پیش فاکتور انتخاب شده «اضافه بار » می باشد و امکان تغییر نحوه پرداخت آن وجود ندارد .", MsgBoxStyle.Information + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.OkOnly, "خطا")
            Exit Sub
        End If

        Dim frm As New frmFO_NoePardakht
        frm.frm_ccPishFaktor = Val(GridEXPishFaktor_Eslah.CurrentRow.Cells("ccPishFaktorTitr").Text.Replace(",", ""))

        Me.Hide()
        frm.ShowDialog()
        Me.Show()

        SearchPishFaktorTafkik_Eslah()
    End Sub

    Private Sub tsmiElatTaghireTedad_Jozei_Click(sender As Object, e As EventArgs) Handles tsmiElatTaghireTedad_Jozei.Click
        'If cmPishFaktor_Kala_Eslah.Position = -1 Then Exit Sub
        If GridEXPishFaktor_Eslah.CurrentRow.Cells("IsTahvil").Value = False Then
            MsgBox("وضعیت پیش فاکتور انتخاب شده « عـدم تحویل » می باشد !!!", MsgBoxStyle.Information + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.OkOnly, "خطا")
            Exit Sub
        End If

        If GridEXPishFaktor_Kala.CurrentRow.Cells("IsJayezeh").Value = True Then
            MsgBox("کالای انتخاب شده جایزه است !!!", MsgBoxStyle.Information + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.OkOnly, "خطا")
            Exit Sub
        End If

        If GridEXPishFaktor_Kala.CurrentRow.Cells("TedadAvalieh").Value = GridEXPishFaktor_Kala.CurrentRow.Cells("TedadGhabelSabt").Value Then
            MsgBox("تعداد این کالا تغییر نکرده است !!!", MsgBoxStyle.Information + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.OkOnly, "خطا")
            Exit Sub
        End If

        Dim frm As New frmFO_ElatTaghireTedadKala
        frm.IsKoli = False
        frm.frm_ccTafkikSatr_GG = Val(GridEXPishFaktor_Eslah.CurrentRow.Cells("ccTafkikSatr_GG").Text.Replace(",", ""))
        frm.frm_ccKala = Val(GridEXPishFaktor_Kala.CurrentRow.Cells("ccKala").Text.Replace(",", ""))

        Me.Hide()
        frm.ShowDialog()
        Me.Show()

        SearchKalaPishFaktor_Eslah()
    End Sub

    Private Sub tsmiElamMarjoee_Click(sender As Object, e As EventArgs) Handles tsmiElamMarjoee.Click
        Dim frmAddPishFaktor As New frmFO_AddPishFaktor
        If cmTafkik_Eslah.Position = -1 Then Exit Sub

        Dim frm As New frmFO_ElamMarjoee
        frm.ccTafkik_GG = Val(GridEXTafkik_Eslah.CurrentRow.Cells("ccTafkik_GG").Text.Replace(",", ""))

        Me.Hide()
        frm.ShowDialog()
        Me.Show()

        SearchPishFaktorTafkik_Eslah()
    End Sub

    'Private Sub btnChap_Click(sender As Object, e As EventArgs) Handles btnChap.Click
    '    Me.Hide()

    '    Dim ExePath As String = Application.StartupPath & "\FO_rpt_PishFaktor.exe"
    '    Shell(ExePath & " " & UserName & ";" & UserCode & ";" & UserPassWord & ";" & NameMahalFaal & ";" & CodeMahalFaal & ";" & PersonelCode & ";" & PersonelName & ";" & CodeDoreh & ";" & " گزارش پیش فاکتور ", AppWinStyle.NormalFocus)

    '    Me.Show()
    'End Sub

    Private Sub BtnChap2_Click(sender As Object, e As EventArgs) Handles BtnChap2.Click
        Me.Hide()

        Dim ExePath As String = Application.StartupPath & "\FO_rpt_PishFaktor.exe"
        Shell(ExePath & " " & UserName & ";" & UserCode & ";" & UserPassWord & ";" & NameMahalFaal & ";" & CodeMahalFaal & ";" & PersonelCode & ";" & PersonelName & ";" & CodeDoreh & ";" & " گزارش پیش فاکتور ", AppWinStyle.NormalFocus)

        Me.Show()
    End Sub

    Private Sub BtnChap1_Click(sender As Object, e As EventArgs) Handles BtnChap1.Click
        Me.Hide()

        Dim ExePath As String = Application.StartupPath & "\FO_rpt_PishFaktor.exe"
        Shell(ExePath & " " & UserName & ";" & UserCode & ";" & UserPassWord & ";" & NameMahalFaal & ";" & CodeMahalFaal & ";" & PersonelCode & ";" & PersonelName & ";" & CodeDoreh & ";" & " گزارش پیش فاکتور ", AppWinStyle.NormalFocus)

        Me.Show()
    End Sub

    Private Sub tsmiPrintPishFaktorTafkik_Click(sender As Object, e As EventArgs) Handles tsmiPrintPishFaktorTafkik.Click
        Try
            If dvTafkik_Eslah.Count > 0 Then
                Me.TopMost = False
                PrintPishFaktor()
                Me.TopMost = True
            End If
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->btnPrintM_Click")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->btnPrintM_Click")
        End Try
    End Sub




    Private Sub PrintPishFaktor()
        Try
            Dim cnSQL As SqlConnection
            Dim strSQL As String
            Dim cmSQL As New SqlCommand
            Dim p As New SqlParameter

            strSQL = "Sales.spPishFaktorGheireGhateei_PrintTafkik_PishFaktor "

            Windows.Forms.Cursor.Current = Cursors.WaitCursor
            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()
            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.CommandTimeout = 999999
            cmSQL.Parameters.Clear()

            cmSQL.Parameters.AddWithValue("ccTafkik_GG", Val(GridEXTafkik_Eslah.CurrentRow.Cells("ccTafkik_GG").Text.Replace(",", "")))

            If dsForm.Tables.Contains("qryFO_PishFaktorTitrSatr") Then
                dsForm.Tables.Remove("qryFO_PishFaktorTitrSatr")
            End If

            Dim daSQL As SqlDataAdapter
            daSQL = New SqlDataAdapter(cmSQL)
            daSQL.Fill(dsForm, "qryFO_PishFaktorTitrSatr")

            Dim rpt As New CrystalDecisions.CrystalReports.Engine.ReportDocument
            Dim rpttables As CrystalDecisions.CrystalReports.Engine.Tables
            Dim rptformula As CrystalDecisions.CrystalReports.Engine.FormulaFieldDefinitions
            Dim frm As New Forms_dll.frmGL_Gozaresh

            Dim OptionalPrintInPishFaktor As Boolean = False
            OptionalPrintInPishFaktor = objTools.ConvertNulls(objTools.DLookup("OptionalPrintInPishFaktor", "tblGL_SysConfig", "CodeMahal = " & CodeMahalFaal), False)

            If OptionalPrintInPishFaktor Then
                Dim rptName As String = ""
                rptName = objTools.DLookup("rptNamePishFaktorOptional", "tblGL_Sherkat", "CodeSherkat = 1")
                rpt.Load(rptPath & "\" & rptName)
            Else

                rpt.Load(rptPath & "\rptFO_GozareshFaktorA4_fararavand_gheireghati.rpt")

            End If

            rpttables = rpt.Database.Tables
            rpttables.Item(0).SetDataSource(dsForm.Tables("qryFO_PishFaktorTitrSatr"))

            rptformula = rpt.DataDefinition.FormulaFields
            With rptformula

                If ObjCode.CheckCompany <> 7 Then
                    .Item("DarsadTakhfif").Text = "{mydata.DarsadTakhfif}"
                    .Item("TakhfifKala").Text = "{mydata.TakhfifKala}"
                    .Item("Tozihat").Text = "{mydata.Tozihat}"
                End If
                'If OptionalPrintInPishFaktor Then
                .Item("Moshtary").Text = "{mydata.NameTablo}"
                .Item("Ostan").Text = "{mydata.Ostan}"
                .Item("NameTablo").Text = "{mydata.NameTablo}"
                .Item("MoshtaryMobile").Text = "{mydata.MoshtaryMobile}"
                .Item("txtVahedShomaresh").Text = "{mydata.txtVahed}"
                .Item("txtVahed").Text = "{mydata.txtVahed}"
                'End If
                .Item("Group_Sanad").Text = "{mydata.ccPishFaktorTitr}"
                .Item("Sh").Text = "{mydata.PishFaktorShomareh}"
                .Item("Tarikh").Text = "{mydata.PishFaktorTarikhSlash}"
                .Item("Bazaryab").Text = "{mydata.NameForoshandeh}"
                .Item("LNameForoshandeh").Text = "{mydata.LNameForoshandeh}"
                .Item("Shahr").Text = "{mydata.Shahr}"
                .Item("CodeForoshandeh").Text = "{mydata.CodeForoshandeh}"
                .Item("Moshtary").Text = "{mydata.NameMoshtary}"
                .Item("NameTablo").Text = "{mydata.NameTablo}"
                .Item("CodeMoshtary").Text = "{mydata.CodeMoshtary}"
                .Item("TelMoshtary").Text = "{mydata.telephone}"
                .Item("masir").Text = "{mydata.masir}"
                .Item("AddressMoshtary").Text = "trim({mydata.Address})"
                .Item("NameKala").Text = "trim({mydata.NameKala})"
                .Item("Tedad").Text = "{mydata.Tedad1}"
                .Item("TedadBasteh").Text = "{mydata.TedadBasteh}"
                .Item("TedadKarton").Text = "{mydata.TedadKarton}"
                .Item("TedadDarKarton").Text = "{mydata.TedadDarKarton}"
                .Item("Fee").Text = "{mydata.fee}"
                .Item("MKOL3").Text = "{mydata.MKOL3}"
                .Item("MKOL").Text = "{mydata.MKOL3}"
                .Item("Takhfif").Text = "{mydata.Takhfif}"
                .Item("Codekala").Text = "{mydata.Codekala}"

                .Item("BarCodeKala").Text = "{mydata.BarCodeKala}"

                .Item("MablaghMalyat").Text = "{mydata.MablaghMalyat}"
                .Item("MablaghAvarez").Text = "{mydata.MablaghAvarez}"
                .Item("TedadDarBasteh").Text = "{mydata.TedadDarBasteh}"
                .Item("TedadDarKarton").Text = "{mydata.TedadDarKarton}"
                '.Item("DarsadTakhfifNaghdi").Text = "{mydata.DarsadTakhfifNaghdi}"

                .Item("txtNoePardakht").Text = "{mydata.txtNoePardakht}"
                '.Item("JamTakhfif").Text = "{mydata.JamTakhfifKala}"
                .Item("JamTakhfifKala").Text = "{mydata.JamTakhfifKala}"
                '-----------------------------------------------------------
                ' Ramezani _ 13950913 _ takhfifDasti_Nikkhah
                .Item("TakhfifKalaDasti").Text = "{mydata.TakhfifKalaDasti}"
                '.Item("JamTakhfifKalaDasti").Text = "{mydata.JamTakhfifKalaDasti}"
                .Item("DarsadTakhfifDasti").Text = "{mydata.DarsadTakhfifDasti}"
                '------------------------------------------------
                .Item("MalyatAvarez").Text = "{mydata.JamMalyatAvarez}"
                .Item("Tozihat").Text = "{mydata.Tozihat}"
                .Item("ShomarehTafkik").Text = "{mydata.ShomarehTafkik}"
                .Item("RanandehTozie").Text = "{mydata.RanandehTozie}"
                '.Item("DarsadTakhfifNaghdi").Text = "{mydata.DarsadTakhfifNaghdi}"
                .Item("RanandehTozie").Text = "{mydata.RanandehTozie}"
                .Item("MoshtaryMobile").Text = "{mydata.MoshtaryMobile}"
                .Item("VisitorMobile").Text = "{mydata.VisitorMobile}"

                '.Item("TarikhRass").Text = "{mydata.TarikhRass}"
                'If OptionalPrintInPishFaktor Then
                .Item("Bazaryab").Text = "{mydata.Bazaryab}"
                .Item("Moshtary").Text = "{mydata.Moshtary}"
                .Item("Khordeh").Text = "{mydata.tedadkhordeh}"
                .Item("Tedad").Text = "{mydata.Tedad}"
                .Item("Mkol").Text = "{mydata.Mkol}"
                .Item("Sh").Text = "{mydata.Sh}"
                .Item("AddressMoshtary").Text = "{mydata.AddressMoshtary}"
                .Item("TelMoshtary").Text = "{mydata.TelMoshtary}"
                .Item("ShomarehTafkik").Text = "{mydata.ShomarehTafkik}"
                .Item("TedadKarton").Text = "{mydata.TedadKarton}"
                '.Item("Khordeh").Text = "{mydata.Khordeh}"
                .Item("MablaghMalyat").Text = "{mydata.MablaghMalyat}"
                .Item("MablaghAvarez").Text = "{mydata.MablaghAvarez}"
                .Item("MobileForoshandeh").Text = "{mydata.VisitorMobile}"
                .Item("CodeMeli").Text = "{mydata.CodeMeli}"
                .Item("NameRanandeh").Text = "{mydata.NameRanandeh}"
                .Item("Takhfifdasty").Text = "{mydata.Takhfifdasty}"
                .Item("MobileRanandeh").Text = "{mydata.MobileRanandeh}"
                .Item("Mandeh").Text = "{mydata.Mandeh}"
                'End If

                .Item("ShomarehEghtesady").Text = "'" & objTools.ConvertNulls(objTools.DLookup("ShomarehEghtesady", "tblGL_Sherkat", "CodeSherkat=1"), "") & "'"
                .Item("Title").Text = "'" & "پیـــش فاکـــتور" & "'"
                .Item("Title2").Text = "'" & NameSherkat & "'"
                .Item("Title3").Text = "'" & NameMahalFaal & "'"
                .Item("KarbarGozaresh").Text = "'" & PersonelName & "'"
                .Item("TarikhGozaresh").Text = "'" & objTarikh.SetDateSlash(TarikhEmrooz) & "'"
                .Item("SaatGozaresh").Text = "'" & Format(TimeOfDay, "HH:mm:ss") & "'"
                'If ObjCode.CheckCompany = 4 And Not OptionalPrintInPishFaktor Then
                ''       .Item("VAhed").Text = "{mydata.txtVahed}"
                'End If
                'If ObjCode.CheckCompany = 4 Then
                '    .Item("MablaghMasrafKonandeh").Text = "{mydata.MablaghMasrafKonandeh}"
                'End If
                Dim Address As String = objTools.ConvertNulls(objTools.DLookup("Address", "tblGL_MarkazPakhsh", "CodeMahal = " & CodeMahalFaal & ""), "")
                Dim Tel As String = objTools.ConvertNulls(objTools.DLookup("Tel", "tblGL_MarkazPakhsh", "CodeMahal = " & CodeMahalFaal & ""), "")
                .Item("CodePosty").Text = "'" & objTools.ConvertNulls(objTools.DLookup("CodePosty", "tblGL_MarkazPakhsh", "CodeMahal = " & CodeMahalFaal), "") & "'"
                .Item("NameSherkat").Text = "'" & objTools.ConvertNulls(objTools.DLookup("NameSherkat", "tblGL_Sherkat", "CodeSherkat = " & CodeSherkat), "") & "'"
                .Item("Address").Text = "'" & Address & "'"
                .Item("Tel").Text = "'" & Tel & "'"
            End With
            rpt.Refresh()

            frm.Text = txtCaption

            With frm.CRV
                .ReportSource = rpt
                .DisplayGroupTree = False
                .ShowGroupTreeButton = False
                .Zoom(75)
                If Not objSec.HaveUserPermission(SN, UD_Dll.Security.ePermissions.Print) Then .ShowPrintButton = False
                If Not objSec.HaveUserPermission(SN, UD_Dll.Security.ePermissions.Export) Then .ShowExportButton = False
            End With

            Me.Hide()
            frm.ShowDialog(Me)
            frm = Nothing
            daSQL = Nothing
            rpt = Nothing
            Me.Show()
            Windows.Forms.Cursor.Current = Cursors.Default
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->Print PishFaktor")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->Print PishFaktor")
        End Try
    End Sub





    Private Sub btnChap_Click(sender As Object, e As EventArgs) Handles btnChap.Click
        PrintPishFaktor()
    End Sub

    Private Sub GridEXPishFaktor_DoubleClick(sender As Object, e As EventArgs) Handles GridEXPishFaktor.DoubleClick
        If dvPishFaktor.Count = 0 Then Exit Sub

        Dim frmSatr As New frmFO_ShowDetailsPishFaktor
        frmSatr.ccTitr = Val(GridEXPishFaktor.CurrentRow.Cells("ccPishFaktorTitr").Text.Replace(",", ""))

        Me.Hide()
        frmSatr.ShowDialog()
        Me.Show()

    End Sub
    Private Sub cmbMashinTozie_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbMashinTozie.SelectedIndexChanged
        If flgShowVaznMashin = False Then
            Exit Sub
        End If

        Dim VaznMashin As Double = 0
        VaznMashin = objTools.ConvertNulls(objTools.DLookup("Vazn", "qryFO_Mashin", "ccMashin =" & cmbMashinTozie.SelectedValue), 1)
        lblVaznMashin.Text = VaznMashin
    End Sub

    Private Sub tsmiChangeTafkik_Click(sender As Object, e As EventArgs) Handles tsmiChangeInfoTafkik.Click
        Dim frm As New frmFO_ChangeTafkikInfo

        frm.ccTafkik_GG_ChangeInfo = Val(GridEXTafkik_Eslah.CurrentRow.Cells("ccTafkik_GG").Text.Replace(",", ""))

        Me.Hide()
        frm.ShowDialog()
        Me.Show()

        SearchTafkik_Eslah(cmbVazeiat_Eslah.SelectedIndex)
    End Sub



    Private Sub GridEXPishFaktor_Kala_MouseClick(sender As Object, e As MouseEventArgs) Handles GridEXPishFaktor_Kala.MouseClick
        Dim ccPishFaktorSatr As Integer = Val(Me.GridEXPishFaktor_Kala.CurrentRow.Cells("ccPishFaktorSatr").Text.Replace(",", ""))
        Dim ccpishFaktorTitr As Integer = objTools.DLookup("ccPishFaktorTitr", "tblfo_PishFaktorSatr", "ccPishfaktorSatr = " & ccPishFaktorSatr & "")
        Dim dt As New DataTable
        Using cn As New SqlConnection(ConnectionString)
            Using cm As SqlCommand = cn.CreateCommand
                cm.CommandType = CommandType.StoredProcedure
                cm.CommandText = "[Sales].[spPishFaktorGheireGhateei_SearchKalaPishFaktor_Eslah]"
                cm.Parameters.AddWithValue("ccPishFaktorTitr", ccpishFaktorTitr)
                Dim da As SqlDataAdapter = New SqlDataAdapter(cm)
                da.Fill(dt)
                cm.Connection.Open()
            End Using
        End Using
        If (ccPishFaktorSatr <> 0) Then
            SElatTaghirTeadadPishGheireGhateei = dt.Rows(0)("sElatTaghirTedad")
        End If
    End Sub





End Class
