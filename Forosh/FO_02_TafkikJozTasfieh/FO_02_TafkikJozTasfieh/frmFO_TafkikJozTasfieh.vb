Public Class frmFO_TafkikJozTasfieh

#Region "Variable AND Constant Declration"
    ' Security 
    Const cntCodeSubSystem As Long = 100086
    Private SN As Integer

    Const GridOrgSize As Integer = 236
    Const GridSize As Integer = 170
    Dim dvTitr As DataView
    Dim dvSatr As DataView
    Dim dvBestankary As DataView
    Dim tCodeCounter As Long
    Const FormAddSize = 0
    Const GridAddSize = 0
    Dim cmTitr As CurrencyManager
    Dim cmSatr As CurrencyManager
    Dim cmBestankary As CurrencyManager

    Dim ErrPro As New ErrorProvider
    Dim Mode As UD_Dll.Enums.GL_ModeForms = UD_Dll.Enums.GL_ModeForms.AddNewRecord
    Dim dsForm As New DataSet
    Dim cmForm As CurrencyManager
    Dim dvForm As DataView
    Dim flg As Boolean = False
    Dim FlgSetForm As Boolean = False
    Dim txtCaption As String
    Dim tSaatVazeiat As String
    Dim tTarikhDP As String
    Dim tSh As Long
    Dim tEbtal As Boolean
    Dim tVazeiat As Long
    Dim tTarikhVazeiat As String
    Dim tShHEntry As String
    Private WithEvents BS As New UD_Dll.PassString
#End Region
#Region "Form Event Code"
    Private Sub frmFO_TafkikJozTasfieh_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Mode = UD_Dll.Enums.GL_ModeForms.None
        SetParameter()
        SN = objSec.GetSecurityNumber(cntCodeSubSystem, UserName)
        LoadCombo()
        mskAzTarikh.Text = TarikhEmrooz
        mskTaTarikh.Text = TarikhEmrooz
        cmbVazeiat.SelectedIndex = 0
        Search()
        SetTitrButton()
        SetSatrButton()
        GroupBox4.Visible = False
        btnTaeed.Enabled = False
        btnPrintTitr.Enabled = False
        flg = True
        mskShomarehTafkik.Text = ""
        mskAzTarikh.Focus()

        objTools.ChangeKbd(UD_Dll.mdlUtility.KBD.Farsi)
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
            CodeDoreh = "1393"
            txtCaption = "تسویه برگه تفکیک"
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
    Private Sub GridEXTitr_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GridEXTitr.Click
        BoundCurrencyManagerTitr()
        RefreshSatrData()
    End Sub
    Private Sub GridEXSatr_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GridEXSatr.Click
        If dvSatr Is Nothing Then
            Exit Sub
        End If
        BoundCurrencyManagerSatr()
    End Sub
    Private Sub GridEXTitr_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GridEXTitr.DoubleClick
        If dvTitr.Count = 0 Then
            Exit Sub
        End If
        If Mode = UD_Dll.Enums.GL_ModeForms.UpdateRow Then
            MsgBox("ابتدا باید از حالت ویرایش تسویه فاکتور خارج شوید !", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "خطــا")
            Exit Sub
        End If

        If GridEXTitr.CurrentRow.Cells("CodeVazeiat").Value = 10 Then
            MsgBox("این رکورد تایید شده است، اجازه ویرایش آن را ندارید !", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "خطــا")
            Exit Sub
        End If
        Mode = UD_Dll.Enums.GL_ModeForms.UpdateRecord
        SetFormTitrData()
        mskNaghdTitr.Focus()
    End Sub
    Private Sub GridEXSatr_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GridEXSatr.DoubleClick
        If dvTitr.Count <> 0 Then
            If dvSatr.Count = 0 Then
                Exit Sub
            End If
        Else
            Exit Sub
        End If

        If GridEXTitr.CurrentRow.Cells("CodeVazeiat").Value = 10 Then
            MsgBox("این رکورد تایید شده است، اجازه ویرایش آن را ندارید !", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "خطــا")
            Exit Sub
        End If

        If Mode = UD_Dll.Enums.GL_ModeForms.UpdateRecord Then
            MsgBox("ابتدا باید از حالت ویرایش تسویه برگه تفکیک خارج شوید !", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "خطــا")
            Exit Sub
        End If

        Mode = UD_Dll.Enums.GL_ModeForms.UpdateRow
        SetFormSatrData(cmSatr.Position)

        If objTools.DLookup("ChekNoePardakht", "tblGL_SysConfig", "CodeMahal = " & CodeMahalFaal) = True Then
            CheckNoePardakht()
        End If
        mskNaghdSatr.Focus()
        FlgSetForm = True
    End Sub
    Private Sub mskCheckSatr_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles mskCheckSatr.KeyPress
        Try
            If (Asc(e.KeyChar()) < 48 Or Asc(e.KeyChar()) > 57) And (Asc(e.KeyChar()) <> 8) Then
                e.Handled = True
            End If

            If e.KeyChar = Chr(Keys.Space) Then

                Dim frm As New frmDP_DaryaftChek
                Me.Hide()

                frm.ccMoshtary = GridEXSatr.CurrentRow.Cells("ccMoshtary").Value
                frm.ccFaktorTitr = GridEXSatr.CurrentRow.Cells("ccFaktorTitr").Value
                frm.ccMamorPakhsh = GridEXSatr.CurrentRow.Cells("ccMamorPakhsh").Value
                frm.MablaghFaktor = GridEXSatr.CurrentRow.Cells("JamKol").Value
                frm.ccTafkikJozeTasfiehSatr = GridEXSatr.CurrentRow.Cells("ccTafkikJozeTasfiehSatr").Value
                frm.MandehFaktor = GridEXSatr.CurrentRow.Cells("JamKol").Value - (CType(mskNaghdSatr.Text, Integer) + CType(mskResidSatr.Text, Integer) + CType(mskKartKhanSatr.Text, Integer) + CType(mskMarjoeeSatr.Text, Integer) + CType(mskTakhfifSatr.Text, Integer))

                frm.ShowDialog(Me)
                Me.Show()

            End If
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->txtCodeMoshtaryS_KeyPress")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->txtCodeMoshtaryS_KeyPress")
        End Try
    End Sub
    Private Sub GridEXTitr_RowCheckStateChanged(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.RowCheckStateChangeEventArgs) Handles GridEXTitr.RowCheckStateChanged
        If GridEXTitr.GetCheckedRows().Length = 0 Then
            btnTaeed.Enabled = False
            btnPrintTitr.Enabled = False
        Else
            btnTaeed.Enabled = True
            btnPrintTitr.Enabled = True
        End If
    End Sub
    Private Sub mskNaghdTitr_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mskNaghdTitr.TextChanged
        Dim Str As String = ""
        Str = mskNaghdTitr.Text.Trim.Replace(",", "")
        mskNaghdTitr.Text = ObjCode.DigitSeprator(Str).Replace(".", ",")
        SendKeys.Send("{End}")
    End Sub
    Private Sub mskCheckTitr_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mskCheckTitr.TextChanged
        Dim Str As String = ""
        Str = mskCheckTitr.Text.Trim.Replace(",", "")
        mskCheckTitr.Text = ObjCode.DigitSeprator(Str).Replace(".", ",")
        SendKeys.Send("{End}")
    End Sub
    Private Sub mskResidTitr_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mskResidTitr.TextChanged
        Dim Str As String = ""
        Str = mskResidTitr.Text.Trim.Replace(",", "")
        mskResidTitr.Text = ObjCode.DigitSeprator(Str).Replace(".", ",")
        SendKeys.Send("{End}")
    End Sub
    Private Sub mskKartKhanTitr_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mskKartKhanTitr.TextChanged
        Dim Str As String = ""
        Str = mskKartKhanTitr.Text.Trim.Replace(",", "")
        mskKartKhanTitr.Text = ObjCode.DigitSeprator(Str).Replace(".", ",")
        SendKeys.Send("{End}")
    End Sub
    Private Sub mskMarjoeeTitr_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mskMarjoeeTitr.TextChanged
        Dim Str As String = ""
        Str = mskMarjoeeTitr.Text.Trim.Replace(",", "")
        mskMarjoeeTitr.Text = ObjCode.DigitSeprator(Str).Replace(".", ",")
        SendKeys.Send("{End}")
    End Sub
    Private Sub mskTakhfifTitr_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mskTakhfifTitr.TextChanged
        Dim Str As String = ""
        Str = mskTakhfifTitr.Text.Trim.Replace(",", "")
        mskTakhfifTitr.Text = ObjCode.DigitSeprator(Str).Replace(".", ",")
        SendKeys.Send("{End}")
    End Sub
    Private Sub mskNaghdSatr_MaskInputRejected(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MaskInputRejectedEventArgs) Handles mskNaghdSatr.MaskInputRejected
        Dim Str As String = ""
        Str = mskNaghdSatr.Text.Trim.Replace(",", "")
        mskNaghdSatr.Text = ObjCode.DigitSeprator(Str).Replace(".", ",")
        SendKeys.Send("{End}")
    End Sub
    Private Sub mskCheckSatr_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mskCheckSatr.TextChanged
        'If FlgSetForm = True Then
        '    mskCheckSatr.Text = objTools.ConvertNulls(objTools.DSum("PardakhtyInFaktor", "Sales.TafkikJozeTasfiehSatrChek", "ccFaktor = " & Val(GridEXSatr.CurrentRow.Cells("ccFaktorTitr").Text.Replace(",", ""))), 0)
        'End If

        Dim Str As String = ""
        Str = mskCheckSatr.Text.Trim.Replace(",", "")
        mskCheckSatr.Text = ObjCode.DigitSeprator(Str).Replace(".", ",")
        SendKeys.Send("{End}")
    End Sub
    Private Sub mskResidSatr_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mskResidSatr.TextChanged
        Dim Str As String = ""
        Str = mskResidSatr.Text.Trim.Replace(",", "")
        mskResidSatr.Text = ObjCode.DigitSeprator(Str).Replace(".", ",")
        SendKeys.Send("{End}")
    End Sub
    Private Sub mskKartKhanSatr_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mskKartKhanSatr.TextChanged
        Dim Str As String = ""
        Str = mskKartKhanSatr.Text.Trim.Replace(",", "")
        mskKartKhanSatr.Text = ObjCode.DigitSeprator(Str).Replace(".", ",")
        SendKeys.Send("{End}")
    End Sub
    Private Sub mskMarjoeeSatr_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mskMarjoeeSatr.TextChanged
        Dim Str As String = ""
        Str = mskMarjoeeSatr.Text.Trim.Replace(",", "")
        mskMarjoeeSatr.Text = ObjCode.DigitSeprator(Str).Replace(".", ",")
        SendKeys.Send("{End}")
    End Sub
    Private Sub mskTakhfifSatr_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mskTakhfifSatr.TextChanged
        Dim Str As String = ""
        Str = mskTakhfifSatr.Text.Trim.Replace(",", "")
        mskTakhfifSatr.Text = ObjCode.DigitSeprator(Str).Replace(".", ",")
        SendKeys.Send("{End}")
    End Sub
    Private Sub mskBestankarySatr_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mskBestankarySatr.TextChanged
        Dim Str As String = ""
        Str = mskBestankarySatr.Text.Trim.Replace(",", "")
        mskBestankarySatr.Text = ObjCode.DigitSeprator(Str).Replace(".", ",")
        SendKeys.Send("{End}")
    End Sub
    Private Sub cmbVazeiat_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbVazeiat.SelectedIndexChanged
        Search()
    End Sub
#End Region
#Region "Global Form Code"
    Private Sub LoadCombo()
        Try
            Dim Strsql As String
            Dim cnSQL As SqlConnection
            Dim daSQL As SqlDataAdapter
            Dim cmSQL As SqlCommand
            Dim p As SqlParameter
            Dim dr As DataRow

            Strsql = "Global.spMamorPakhsh_LoadCombo "

            cnSQL = New SqlConnection(ConnectionString)

            cmSQL = New SqlCommand(Strsql, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            p = New SqlParameter("CodeMahal", SqlDbType.Int)
            p.Value = CType(CodeMahalFaal, Integer)
            cmSQL.Parameters.Add(p)

            p = New SqlParameter("sSemat", SqlDbType.NVarChar, 20)
            p.Value = "," & UD_Dll.Enums.GL_Semat.MamorPakhsh & "," & UD_Dll.Enums.GL_Semat.Ranandeh & "," & UD_Dll.Enums.GL_Semat.Foroshandeh_Sayar & ","
            cmSQL.Parameters.Add(p)

            daSQL = New SqlDataAdapter(cmSQL)
            daSQL.Fill(dsForm, "tblMamorPakhsh")
            dr = dsForm.Tables("tblMamorPakhsh").NewRow
            dr("FN") = "همه"
            dr("CodeFard") = 0
            dsForm.Tables("tblMamorPakhsh").Rows.Add(dr)
            cmbMamorPakhsh.DataSource = Nothing
            cmbMamorPakhsh.Items.Clear()
            cmbMamorPakhsh.DataSource = dsForm.Tables("tblMamorPakhsh").DefaultView
            cmbMamorPakhsh.DisplayMember = "FN"
            cmbMamorPakhsh.ValueMember = "CodeFard"
            cmbMamorPakhsh.SelectedIndex = -1
            cmbMamorPakhsh.SelectedIndex = -1

            '-------------------------------------------

            cmbVazeiat.Items.Add("بدون وضعیت")
            cmbVazeiat.Items.Add("تائید شده")
            cmbVazeiat.Items.Add("همه")

            '-------------------------------------------
            Strsql = "Global.spShomarehHesab_LoadCombo "

            cmSQL = New SqlCommand(Strsql, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            cmSQL.Parameters.AddWithValue("CodeMahal", CodeMahalFaal)

            daSQL = New SqlDataAdapter(cmSQL)
            daSQL.Fill(dsForm, "ComboShomarehHesab")
            cmbShomarehHesab.DataSource = Nothing
            cmbShomarehHesab.Items.Clear()
            cmbShomarehHesab.DataSource = dsForm.Tables("ComboShomarehHesab").DefaultView
            cmbShomarehHesab.DisplayMember = "ShomarehHesab"
            cmbShomarehHesab.ValueMember = "ccShomarehHesab"
            If dsForm.Tables("ComboShomarehHesab").DefaultView.Count = 0 Then
                MsgBox("برای استفاده از این فرم ابتدا باید شماره حساب های شرکت را تعریف کنید.", MsgBoxStyle.Information Or MsgBoxStyle.MsgBoxRtlReading, "پیام")
                Me.Close()
            Else
                cmbShomarehHesab.SelectedIndex = 0
                'tShHEntry = cmbShomarehHesab.SelectedValue
            End If
            cmbShomarehHesab.SelectedIndex = -1

            cmSQL.Connection.Close()
            cnSQL.Close()
            daSQL = Nothing

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->LoadCombo")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->LoadCombo")
        End Try
    End Sub
    Private Sub Search()
        Try
            Dim StrSql As String

            StrSql = "Sales.spTafkikJozTasfieh_Search "

            RefreshTitrdata(StrSql)
            'RefreshSatrData()
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->Search")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->Search")
        End Try
    End Sub
    Private Sub RefreshTitrdata(ByVal strSql As String)
        Try
            Dim cnSQL As New SqlConnection
            Dim cmSQL As New SqlCommand
            Dim daSQL As SqlDataAdapter

            Dim CodeVazeiat As Integer = 0
            If (cmbVazeiat.SelectedIndex = 0 Or cmbVazeiat.SelectedIndex = -1) Then
                CodeVazeiat = 0
            ElseIf cmbVazeiat.SelectedIndex = 1 Then
                CodeVazeiat = 10
            Else
                CodeVazeiat = 1
            End If

            If dsForm.Tables.Contains("Sales_TafkikJozTasfieh") Then
                dsForm.Tables.Remove("Sales_TafkikJozTasfieh")
            End If

            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()

            cmSQL = New SqlCommand(strSql, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            cmSQL.Parameters.AddWithValue("ccMamorPakhsh", IIf(cmbMamorPakhsh.SelectedIndex = -1, 0, cmbMamorPakhsh.SelectedValue))
            cmSQL.Parameters.AddWithValue("ShomarehTafkik", IIf(mskShomarehTafkik.Text = "", 0, mskShomarehTafkik.Text))
            cmSQL.Parameters.AddWithValue("AzTarikh", mskAzTarikh.Text)
            cmSQL.Parameters.AddWithValue("TaTarikh", mskTaTarikh.Text)
            cmSQL.Parameters.AddWithValue("CodeMahal", CodeMahalFaal)
            cmSQL.Parameters.AddWithValue("CodeVazeiat", CodeVazeiat)

            daSQL = New SqlDataAdapter(cmSQL)
            daSQL.Fill(dsForm, "Sales_TafkikJozTasfieh")

            dvTitr = New DataView(dsForm.Tables("Sales_TafkikJozTasfieh"), "", "ShomarehTafkik ASC", DataViewRowState.CurrentRows)
            dvTitr.AllowNew = False
            dvTitr.AllowDelete = False
            dvTitr.AllowEdit = False

            cmSQL.Connection.Close()
            cnSQL.Close()
            cmSQL = Nothing
            daSQL = Nothing

            GridEXTitr.DataSource = Nothing
            GridEXTitr.DataSource = dvTitr
            BoundCurrencyManagerTitr()
            'SetTitrButton()

            If dvTitr.Count = 0 Then
                GridEXSatr.DataSource = Nothing
            End If

            SetGridStyle()

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->RefreshTitrdata")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->RefreshTitrdata")
        End Try

    End Sub
    Private Sub RefreshSatrData()
        Try
            If dvTitr.Count = 0 Then
                Exit Sub
            End If

            If flg = False Then
                Exit Sub
            End If

            Dim Strsql As String
            Dim cnSQL As New SqlConnection
            Dim cmSQL As New SqlCommand
            Dim daSQL As SqlDataAdapter

            If dsForm.Tables.Contains("Sales_TafkikJozTasfiehSatr") Then
                dsForm.Tables.Remove("Sales_TafkikJozTasfiehSatr")
            End If

            Strsql = "Sales.spTafkikJozTasfiehSatr_Search "

            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()

            cmSQL = New SqlCommand(Strsql, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            cmSQL.Parameters.AddWithValue("ccTafkikJozeTasfieh", IIf(Val(GridEXTitr.CurrentRow.Cells("ccTafkikJozeTasfieh").Text.Replace(",", "")) = Nothing, 0, Val(GridEXTitr.CurrentRow.Cells("ccTafkikJozeTasfieh").Text.Replace(",", ""))))
            'cmSQL.Parameters.AddWithValue("ccTafkikJozeTasfieh", dvTitr(cmTitr.Position)("ccTafkikJozeTasfieh"))

            daSQL = New SqlDataAdapter(cmSQL)
            daSQL.Fill(dsForm, "Sales_TafkikJozTasfiehSatr")

            dvSatr = New DataView(dsForm.Tables("Sales_TafkikJozTasfiehSatr"))

            dvSatr.AllowNew = False
            dvSatr.AllowDelete = False
            dvSatr.AllowEdit = True

            daSQL = Nothing
            GridEXSatr.DataSource = Nothing
            GridEXSatr.DataSource = dvSatr
            cnSQL.Close()
            BoundCurrencyManagerSatr()
            SetGridStyleSatr()
            'SetSatrButton()

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->RefreshSatrData")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->RefreshSatrData")
        End Try

    End Sub
    Private Sub AddNewRecord()
        Dim cn As New SqlConnection
        Dim cm As New SqlCommand
        Dim strSQL As String = ""

        strSQL = "Sales.spTafkikJozTasfieh_Update "

        cn = New SqlConnection(ConnectionString)
        cn.Open()

        cm = New SqlCommand(strSQL, cn)
        cm.CommandType = CommandType.StoredProcedure
        cm.Parameters.Clear()

        cm.Parameters.AddWithValue("MablaghNaghd", mskNaghdTitr.Text.Replace(",", ""))
        cm.Parameters.AddWithValue("MablaghChek", mskCheckTitr.Text.Replace(",", ""))
        cm.Parameters.AddWithValue("MablaghResid", mskResidTitr.Text.Replace(",", ""))
        cm.Parameters.AddWithValue("MablaghKartKhan", mskKartKhanTitr.Text.Replace(",", ""))
        cm.Parameters.AddWithValue("MablaghMarjoee", mskMarjoeeTitr.Text.Replace(",", ""))
        cm.Parameters.AddWithValue("MablaghTakhfif", mskTakhfifTitr.Text.Replace(",", ""))
        cm.Parameters.AddWithValue("ccTafkikJozeTasfieh", Val(GridEXTitr.CurrentRow.Cells("ccTafkikJozeTasfieh").Text.Replace(",", "")))

        cm.ExecuteNonQuery()

        cm.Connection.Close()
        cn.Close()
        cm = Nothing

    End Sub
    Private Sub AddNewRow()
        Dim cn As New SqlConnection
        Dim cm As New SqlCommand
        Dim strSQL As String = ""

        strSQL = "Sales.spTafkikJozTasfiehSatr_Update "

        cn = New SqlConnection(ConnectionString)
        cn.Open()

        cm = New SqlCommand(strSQL, cn)
        cm.CommandType = CommandType.StoredProcedure
        cm.Parameters.Clear()

        cm.Parameters.AddWithValue("MablaghNaghd", mskNaghdSatr.Text.Replace(",", ""))
        cm.Parameters.AddWithValue("MablaghChek", mskCheckSatr.Text.Replace(",", ""))
        cm.Parameters.AddWithValue("MablaghResid", mskResidSatr.Text.Replace(",", ""))
        cm.Parameters.AddWithValue("MablaghKartKhan", mskKartKhanSatr.Text.Replace(",", ""))
        cm.Parameters.AddWithValue("MablaghMarjoee", mskMarjoeeSatr.Text.Replace(",", ""))
        cm.Parameters.AddWithValue("MablaghTakhfif", mskTakhfifSatr.Text.Replace(",", ""))
        cm.Parameters.AddWithValue("ccDarkhastFaktor", Val(GridEXSatr.CurrentRow.Cells("ccFaktorTitr").Text.Replace(",", "")))

        cm.ExecuteNonQuery()

        cm.Connection.Close()
        cn.Close()
        cm = Nothing

    End Sub
    Private Sub BoundCurrencyManagerTitr()
        Try
            cmTitr = CType(BindingContext(GridEXTitr.DataSource), CurrencyManager)
            AddHandler cmTitr.ItemChanged, AddressOf cmTitr_ItemChanged
            AddHandler cmTitr.PositionChanged, AddressOf cmTitr_PositionChanged
            RefreshSatrData()
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->BoundCurrencyManagerTitr")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->BoundCurrencyManagerTitr")
        End Try

    End Sub
    Private Sub BoundCurrencyManagerSatr()
        Try
            cmSatr = CType(BindingContext(GridEXSatr.DataSource), CurrencyManager)
            AddHandler cmSatr.ItemChanged, AddressOf cmSatr_ItemChanged
            AddHandler cmSatr.PositionChanged, AddressOf cmSatr_PositionChanged
            'RefreshBestankary()
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->BoundCurrencyManagerSatr")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->BoundCurrencyManagerSatr")
        End Try

    End Sub
    Private Sub cmTitr_ItemChanged(ByVal sender As Object, ByVal e As ItemChangedEventArgs)
        Try
            Search()
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->cmTitr_ItemChanged")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->cmTitr_ItemChanged")
        End Try

    End Sub
    Private Sub cmTitr_PositionChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            'SetFormTitrData()
            'RefreshSatrData()
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->cmTitr_PositionChanged")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->cmTitr_PositionChanged")
        End Try

    End Sub
    Private Sub cmSatr_ItemChanged(ByVal sender As Object, ByVal e As ItemChangedEventArgs)
        Try
            'SetSatrButton()
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->cmSatr_ItemChanged")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->cmSatr_ItemChanged")
        End Try

    End Sub
    Private Sub cmSatr_PositionChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            'SetSatrButton()
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->cmSatr_PositionChanged")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->cmSatr_PositionChanged")
        End Try

    End Sub
    Private Sub cmBestankary_ItemChanged(ByVal sender As Object, ByVal e As ItemChangedEventArgs)
        Try
            'SetSatrButton()
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->cmBestankary_ItemChanged")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->cmBestankary_ItemChanged")
        End Try

    End Sub
    Private Sub cmBestankary_PositionChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            'SetSatrButton()
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->cmBestankary_PositionChanged")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->cmBestankary_PositionChanged")
        End Try

    End Sub
    Private Sub SetFormTitrData()
        Try
            'If dvTitr.Count = 0 Or cmTitr.Position = -1 Then
            '    Mode = UD_Dll.Enums.GL_ModeForms.None
            '    Exit Sub
            'End If

            Mode = UD_Dll.Enums.GL_ModeForms.UpdateRecord

            'Dim drvTemp As DataRowView
            'drvTemp = dvTitr(cmTitr.Position)
            'mskNaghdTitr.Text = drvTemp("MablaghNaghd")
            'mskCheckTitr.Text = drvTemp("MablaghChek")
            'mskResidTitr.Text = drvTemp("MablaghResid")
            'mskKartKhanTitr.Text = drvTemp("MablaghKartKhan")
            'mskMarjoeeTitr.Text = drvTemp("MablaghMarjoee")
            'mskTakhfifTitr.Text = drvTemp("MablaghTakhfif")

            mskNaghdTitr.Text = GridEXTitr.CurrentRow.Cells("MablaghNaghd").Value
            mskCheckTitr.Text = GridEXTitr.CurrentRow.Cells("MablaghChek").Value
            mskResidTitr.Text = GridEXTitr.CurrentRow.Cells("MablaghResid").Value
            mskKartKhanTitr.Text = GridEXTitr.CurrentRow.Cells("MablaghKartKhan").Value
            mskMarjoeeTitr.Text = GridEXTitr.CurrentRow.Cells("MablaghMarjoee").Value
            mskTakhfifTitr.Text = GridEXTitr.CurrentRow.Cells("MablaghTakhfif").Value

            SetTitrButton()

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->SetFormTitrData")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->SetFormTitrData")
        End Try

    End Sub
    Private Sub SetFormSatrData(ByVal Pos As Integer)
        Try
            'If dvSatr.Count = 0 Or cmTitr.Position = -1 Then
            '    Mode = UD_Dll.Enums.GL_ModeForms.None
            '    Exit Sub
            'End If

            'Dim drvTemp As DataRowView
            'drvTemp = dvSatr(Pos)
            'drvTemp = dvSatr(cmSatr.Position)
            'mskNaghdSatr.Text = drvTemp("MablaghNaghd")
            'mskCheckSatr.Text = drvTemp("MablaghChek")
            'mskResidSatr.Text = drvTemp("MablaghResid")
            'mskKartKhanSatr.Text = drvTemp("MablaghKartKhan")
            'mskMarjoeeSatr.Text = drvTemp("MablaghMarjoee")
            'mskTakhfifSatr.Text = drvTemp("MablaghTakhfif")

            mskNaghdSatr.Text = GridEXSatr.CurrentRow.Cells("MablaghNaghd").Value
            mskCheckSatr.Text = GridEXSatr.CurrentRow.Cells("MablaghChek").Value
            mskResidSatr.Text = GridEXSatr.CurrentRow.Cells("MablaghResid").Value
            mskKartKhanSatr.Text = GridEXSatr.CurrentRow.Cells("MablaghKartKhan").Value
            mskMarjoeeSatr.Text = GridEXSatr.CurrentRow.Cells("MablaghMarjoee").Value
            mskTakhfifSatr.Text = GridEXSatr.CurrentRow.Cells("MablaghTakhfif").Value
            mskBestankarySatr.Text = GridEXSatr.CurrentRow.Cells("MablaghBestankary").Value

            SetSatrButton()

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->SetFormSatrData")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->SetFormSatrData")
        End Try

    End Sub
    Private Sub CheckNoePardakht()
        Dim NoePardakht As Integer = objTools.DLookup("sNoePardakht", "tblFO_Faktor", "ccFaktorTitr = " & dvSatr(cmSatr.Position)("ccFaktorTitr"))
        Select Case NoePardakht
            Case 3952
                mskResidSatr.Enabled = False
            Case 5488
                mskResidSatr.Enabled = False
            Case 3953
                mskResidSatr.Enabled = False
                mskCheckSatr.Enabled = False
            Case 4734
                mskResidSatr.Enabled = False
                mskCheckSatr.Enabled = False
            Case 5688
                mskResidSatr.Enabled = False
                mskCheckSatr.Enabled = False
                mskNaghdSatr.Enabled = False
        End Select
    End Sub
    Private Sub SetGridStyle()
        Try
            'Titr
            '=========================================================================================='
            With GridEXTitr
                .DataSource = Nothing
                .DataSource = dsForm.Tables("Sales_TafkikJozTasfieh").DefaultView
                .SetDataBinding(dsForm.Tables("Sales_TafkikJozTasfieh").DefaultView, "")
                .RetrieveStructure()
            End With

            For i As Integer = 0 To GridEXTitr.CurrentTable.Columns.Count - 1
                GridEXTitr.CurrentTable.Columns.Item(i).Visible = False
            Next

            GridEXTitr.CurrentTable.Columns.Item("Taeed").Caption = "انتخاب"
            GridEXTitr.CurrentTable.Columns.Item("Taeed").HeaderStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXTitr.CurrentTable.Columns.Item("Taeed").Visible = True
            GridEXTitr.CurrentTable.Columns.Item("Taeed").Width = 50
            GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXTitr.CurrentTable.Columns.Item("Taeed").Position = 0
            GridEXTitr.CurrentTable.Columns.Item("Taeed").Selectable = True
            GridEXTitr.CurrentTable.Columns.Item("Taeed").ActAsSelector = True
            GridEXTitr.CurrentTable.Columns.Item("Taeed").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXTitr.CurrentTable.Columns.Item("ShomarehTafkik").Caption = "شماره تفکیـک"
            GridEXTitr.CurrentTable.Columns.Item("ShomarehTafkik").HeaderStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXTitr.CurrentTable.Columns.Item("ShomarehTafkik").Visible = True
            GridEXTitr.CurrentTable.Columns.Item("ShomarehTafkik").Width = 100
            GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXTitr.CurrentTable.Columns.Item("ShomarehTafkik").Position = 1
            GridEXTitr.CurrentTable.Columns.Item("ShomarehTafkik").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXTitr.CurrentTable.Columns.Item("TarikhTafkik").Caption = "تاریـخ تفکیـک"
            GridEXTitr.CurrentTable.Columns.Item("TarikhTafkik").HeaderStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXTitr.CurrentTable.Columns.Item("TarikhTafkik").Visible = True
            GridEXTitr.CurrentTable.Columns.Item("TarikhTafkik").Width = 100
            GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXTitr.CurrentTable.Columns.Item("TarikhTafkik").Position = 2
            GridEXTitr.CurrentTable.Columns.Item("TarikhTafkik").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXTitr.CurrentTable.Columns.Item("NameMamorPakhsh").Caption = "نام مامور پخش"
            GridEXTitr.CurrentTable.Columns.Item("NameMamorPakhsh").HeaderStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXTitr.CurrentTable.Columns.Item("NameMamorPakhsh").Visible = True
            GridEXTitr.CurrentTable.Columns.Item("NameMamorPakhsh").Width = 150
            GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXTitr.CurrentTable.Columns.Item("NameMamorPakhsh").Position = 3
            GridEXTitr.CurrentTable.Columns.Item("NameMamorPakhsh").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXTitr.CurrentTable.Columns.Item("txtCodeVazeiat").Caption = "وضعـیت"
            GridEXTitr.CurrentTable.Columns.Item("txtCodeVazeiat").HeaderStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXTitr.CurrentTable.Columns.Item("txtCodeVazeiat").Visible = True
            GridEXTitr.CurrentTable.Columns.Item("txtCodeVazeiat").Width = 90
            GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXTitr.CurrentTable.Columns.Item("txtCodeVazeiat").Position = 4
            GridEXTitr.CurrentTable.Columns.Item("txtCodeVazeiat").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXTitr.CurrentTable.Columns.Item("TedadKalaTafkik").Caption = "تعداد کالا"
            GridEXTitr.CurrentTable.Columns.Item("TedadKalaTafkik").HeaderStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXTitr.CurrentTable.Columns.Item("TedadKalaTafkik").Visible = True
            GridEXTitr.CurrentTable.Columns.Item("TedadKalaTafkik").Width = 60
            GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXTitr.CurrentTable.Columns.Item("TedadKalaTafkik").Position = 5
            GridEXTitr.CurrentTable.Columns.Item("TedadKalaTafkik").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXTitr.CurrentTable.Columns.Item("MablaghKolTafkik").Caption = "مبلغ کل تفکیـک"
            GridEXTitr.CurrentTable.Columns.Item("MablaghKolTafkik").HeaderStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXTitr.CurrentTable.Columns.Item("MablaghKolTafkik").Visible = True
            GridEXTitr.CurrentTable.Columns.Item("MablaghKolTafkik").Width = 110
            GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXTitr.CurrentTable.Columns.Item("MablaghKolTafkik").Position = 6
            GridEXTitr.CurrentTable.Columns.Item("MablaghKolTafkik").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXTitr.CurrentTable.Columns.Item("JamPardakhti").Caption = "مجموع پرداختی"
            GridEXTitr.CurrentTable.Columns.Item("JamPardakhti").HeaderStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXTitr.CurrentTable.Columns.Item("JamPardakhti").Visible = True
            GridEXTitr.CurrentTable.Columns.Item("JamPardakhti").Width = 100
            GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXTitr.CurrentTable.Columns.Item("JamPardakhti").Position = 7
            GridEXTitr.CurrentTable.Columns.Item("JamPardakhti").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXTitr.CurrentTable.Columns.Item("MandehTafkik").Caption = "مانده"
            GridEXTitr.CurrentTable.Columns.Item("MandehTafkik").HeaderStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXTitr.CurrentTable.Columns.Item("MandehTafkik").Visible = True
            GridEXTitr.CurrentTable.Columns.Item("MandehTafkik").Width = 100
            GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXTitr.CurrentTable.Columns.Item("MandehTafkik").Position = 8
            GridEXTitr.CurrentTable.Columns.Item("MandehTafkik").CellStyle.BackColor = Color.Yellow
            GridEXTitr.CurrentTable.Columns.Item("MandehTafkik").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXTitr.CurrentTable.Columns.Item("MablaghNaghd").Caption = "مبلغ نقــد"
            GridEXTitr.CurrentTable.Columns.Item("MablaghNaghd").HeaderStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXTitr.CurrentTable.Columns.Item("MablaghNaghd").Visible = True
            GridEXTitr.CurrentTable.Columns.Item("MablaghNaghd").Width = 100
            GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXTitr.CurrentTable.Columns.Item("MablaghNaghd").Position = 9
            GridEXTitr.CurrentTable.Columns.Item("MablaghNaghd").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXTitr.CurrentTable.Columns.Item("MNaghdP").Caption = "نقد پرداخت شده"
            GridEXTitr.CurrentTable.Columns.Item("MNaghdP").HeaderStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXTitr.CurrentTable.Columns.Item("MNaghdP").Visible = True
            GridEXTitr.CurrentTable.Columns.Item("MNaghdP").Width = 110
            GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXTitr.CurrentTable.Columns.Item("MNaghdP").Position = 10
            GridEXTitr.CurrentTable.Columns.Item("MNaghdP").EditType = Janus.Windows.GridEX.EditType.NoEdit
            GridEXTitr.CurrentTable.Columns.Item("MNaghdP").CellStyle.BackColor = Color.Moccasin
            GridEXTitr.CurrentTable.Columns.Item("MNaghdP").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXTitr.CurrentTable.Columns.Item("MablaghChek").Caption = "مبلغ چـک"
            GridEXTitr.CurrentTable.Columns.Item("MablaghChek").HeaderStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXTitr.CurrentTable.Columns.Item("MablaghChek").Visible = True
            GridEXTitr.CurrentTable.Columns.Item("MablaghChek").Width = 100
            GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXTitr.CurrentTable.Columns.Item("MablaghChek").Position = 11
            GridEXTitr.CurrentTable.Columns.Item("MablaghChek").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXTitr.CurrentTable.Columns.Item("MChekP").Caption = "چک پرداخت شده"
            GridEXTitr.CurrentTable.Columns.Item("MChekP").HeaderStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXTitr.CurrentTable.Columns.Item("MChekP").Visible = True
            GridEXTitr.CurrentTable.Columns.Item("MChekP").Width = 110
            GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXTitr.CurrentTable.Columns.Item("MChekP").Position = 12
            GridEXTitr.CurrentTable.Columns.Item("MChekP").EditType = Janus.Windows.GridEX.EditType.NoEdit
            GridEXTitr.CurrentTable.Columns.Item("MChekP").CellStyle.BackColor = Color.Moccasin
            GridEXTitr.CurrentTable.Columns.Item("MChekP").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXTitr.CurrentTable.Columns.Item("MablaghResid").Caption = "مبلغ رسیـد"
            GridEXTitr.CurrentTable.Columns.Item("MablaghResid").HeaderStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXTitr.CurrentTable.Columns.Item("MablaghResid").Visible = True
            GridEXTitr.CurrentTable.Columns.Item("MablaghResid").Width = 100
            GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXTitr.CurrentTable.Columns.Item("MablaghResid").Position = 13
            GridEXTitr.CurrentTable.Columns.Item("MablaghResid").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXTitr.CurrentTable.Columns.Item("MablaghKartKhan").Caption = "مبلغ کارت خوان"
            GridEXTitr.CurrentTable.Columns.Item("MablaghKartKhan").HeaderStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXTitr.CurrentTable.Columns.Item("MablaghKartKhan").Visible = True
            GridEXTitr.CurrentTable.Columns.Item("MablaghKartKhan").Width = 100
            GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXTitr.CurrentTable.Columns.Item("MablaghKartKhan").Position = 14
            GridEXTitr.CurrentTable.Columns.Item("MablaghKartKhan").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXTitr.CurrentTable.Columns.Item("MablaghMarjoee").Caption = "مبلغ مرجوعی"
            GridEXTitr.CurrentTable.Columns.Item("MablaghMarjoee").HeaderStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXTitr.CurrentTable.Columns.Item("MablaghMarjoee").Visible = True
            GridEXTitr.CurrentTable.Columns.Item("MablaghMarjoee").Width = 100
            GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXTitr.CurrentTable.Columns.Item("MablaghMarjoee").Position = 15
            GridEXTitr.CurrentTable.Columns.Item("MablaghMarjoee").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXTitr.CurrentTable.Columns.Item("MMarjoeeP").Caption = "مرجوعی ثبت شده"
            GridEXTitr.CurrentTable.Columns.Item("MMarjoeeP").HeaderStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXTitr.CurrentTable.Columns.Item("MMarjoeeP").Visible = True
            GridEXTitr.CurrentTable.Columns.Item("MMarjoeeP").Width = 120
            GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXTitr.CurrentTable.Columns.Item("MMarjoeeP").Position = 16
            GridEXTitr.CurrentTable.Columns.Item("MMarjoeeP").EditType = Janus.Windows.GridEX.EditType.NoEdit
            GridEXTitr.CurrentTable.Columns.Item("MMarjoeeP").CellStyle.BackColor = Color.Moccasin
            GridEXTitr.CurrentTable.Columns.Item("MMarjoeeP").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXTitr.CurrentTable.Columns.Item("MablaghTakhfif").Caption = "مبلغ تخفیف"
            GridEXTitr.CurrentTable.Columns.Item("MablaghTakhfif").HeaderStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXTitr.CurrentTable.Columns.Item("MablaghTakhfif").Visible = True
            GridEXTitr.CurrentTable.Columns.Item("MablaghTakhfif").Width = 100
            GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXTitr.CurrentTable.Columns.Item("MablaghTakhfif").Position = 17
            GridEXTitr.CurrentTable.Columns.Item("MablaghTakhfif").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXTitr.CurrentTable.Columns.Item("MTabdilBeTakhfif").Caption = "تبدیل به تخفیف"
            GridEXTitr.CurrentTable.Columns.Item("MTabdilBeTakhfif").HeaderStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXTitr.CurrentTable.Columns.Item("MTabdilBeTakhfif").Visible = True
            GridEXTitr.CurrentTable.Columns.Item("MTabdilBeTakhfif").Width = 110
            GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXTitr.CurrentTable.Columns.Item("MTabdilBeTakhfif").Position = 18
            GridEXTitr.CurrentTable.Columns.Item("MTabdilBeTakhfif").EditType = Janus.Windows.GridEX.EditType.NoEdit
            GridEXTitr.CurrentTable.Columns.Item("MTabdilBeTakhfif").CellStyle.BackColor = Color.Moccasin
            GridEXTitr.CurrentTable.Columns.Item("MTabdilBeTakhfif").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXTitr.CurrentTable.Columns.Item("MablaghBestankary").Caption = "پرداختی از بستانکاری"
            GridEXTitr.CurrentTable.Columns.Item("MablaghBestankary").HeaderStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXTitr.CurrentTable.Columns.Item("MablaghBestankary").Visible = True
            GridEXTitr.CurrentTable.Columns.Item("MablaghBestankary").Width = 140
            GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXTitr.CurrentTable.Columns.Item("MablaghBestankary").Position = 19
            GridEXTitr.CurrentTable.Columns.Item("MablaghBestankary").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXTitr.CurrentTable.Columns.Item("CodeVazeiat").Caption = "کد وضعـیت"
            GridEXTitr.CurrentTable.Columns.Item("CodeVazeiat").HeaderStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXTitr.CurrentTable.Columns.Item("CodeVazeiat").Visible = False
            GridEXTitr.CurrentTable.Columns.Item("CodeVazeiat").Width = 0
            GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXTitr.CurrentTable.Columns.Item("CodeVazeiat").Position = 20
            GridEXTitr.CurrentTable.Columns.Item("CodeVazeiat").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXTitr.CurrentTable.Columns.Item("ccTafkikJozeTasfieh").Caption = "ccTafkikJozeTasfieh"
            GridEXTitr.CurrentTable.Columns.Item("ccTafkikJozeTasfieh").HeaderStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXTitr.CurrentTable.Columns.Item("ccTafkikJozeTasfieh").Visible = False
            GridEXTitr.CurrentTable.Columns.Item("ccTafkikJozeTasfieh").Width = 0
            GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXTitr.CurrentTable.Columns.Item("ccTafkikJozeTasfieh").Position = 21
            GridEXTitr.CurrentTable.Columns.Item("ccTafkikJozeTasfieh").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            For i As Integer = 0 To GridEXTitr.RootTable.Columns.Count - 1
                If GridEXTitr.RootTable.Columns(i).Type.IsValueType Then
                    GridEXTitr.RootTable.Columns(i).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                    GridEXTitr.RootTable.Columns(i).FormatMode = Janus.Windows.GridEX.FormatMode.UseIFormattable
                    GridEXTitr.RootTable.Columns(i).FormatString = "N"
                    GridEXTitr.RootTable.Columns(i).TotalFormatMode = Janus.Windows.GridEX.FormatMode.UseIFormattable
                    GridEXTitr.RootTable.Columns(i).TotalFormatString = "N"
                End If
            Next

            GridEXTitr.Visible = True

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->SetGridStyle")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->SetGridStyle")
        End Try

    End Sub
    Private Sub SetGridStyleSatr()
        Try

            If dvTitr.Count = 0 Then
                Exit Try
            End If

            If dvSatr.Count = 0 Then
                Exit Try
            End If

            With GridEXSatr
                .DataSource = Nothing
                .DataSource = dsForm.Tables("Sales_TafkikJozTasfiehSatr").DefaultView
                .SetDataBinding(dsForm.Tables("Sales_TafkikJozTasfiehSatr").DefaultView, "")
                .RetrieveStructure()
            End With

            For i As Integer = 0 To GridEXSatr.CurrentTable.Columns.Count - 1
                GridEXSatr.CurrentTable.Columns.Item(i).Visible = False
            Next

            GridEXSatr.CurrentTable.Columns.Item("FaktorShomareh").Caption = "شماره فاکتور"
            GridEXSatr.CurrentTable.Columns.Item("FaktorShomareh").HeaderStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXSatr.CurrentTable.Columns.Item("FaktorShomareh").Visible = True
            GridEXSatr.CurrentTable.Columns.Item("FaktorShomareh").Width = 90
            GridEXSatr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXSatr.CurrentTable.Columns.Item("FaktorShomareh").Position = 0
            GridEXSatr.CurrentTable.Columns.Item("FaktorShomareh").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXSatr.CurrentTable.Columns.Item("FaktorTarikh").Caption = "تاریـخ فاکتور"
            GridEXSatr.CurrentTable.Columns.Item("FaktorTarikh").HeaderStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXSatr.CurrentTable.Columns.Item("FaktorTarikh").Visible = True
            GridEXSatr.CurrentTable.Columns.Item("FaktorTarikh").Width = 90
            GridEXSatr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXSatr.CurrentTable.Columns.Item("FaktorTarikh").Position = 1
            GridEXSatr.CurrentTable.Columns.Item("FaktorTarikh").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXSatr.CurrentTable.Columns.Item("NameMoshtary").Caption = "نام مشتــری"
            GridEXSatr.CurrentTable.Columns.Item("NameMoshtary").HeaderStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXSatr.CurrentTable.Columns.Item("NameMoshtary").Visible = True
            GridEXSatr.CurrentTable.Columns.Item("NameMoshtary").Width = 225
            GridEXSatr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXSatr.CurrentTable.Columns.Item("NameMoshtary").Position = 2
            GridEXSatr.CurrentTable.Columns.Item("NameMoshtary").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXSatr.CurrentTable.Columns.Item("JamKol").Caption = "مبلغ کل فاکتور"
            GridEXSatr.CurrentTable.Columns.Item("JamKol").HeaderStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXSatr.CurrentTable.Columns.Item("JamKol").Visible = True
            GridEXSatr.CurrentTable.Columns.Item("JamKol").Width = 100
            GridEXSatr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXSatr.CurrentTable.Columns.Item("JamKol").Position = 3
            GridEXSatr.CurrentTable.Columns.Item("JamKol").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXSatr.CurrentTable.Columns.Item("JamPardakhti").Caption = "مجموع پرداختی"
            GridEXSatr.CurrentTable.Columns.Item("JamPardakhti").HeaderStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXSatr.CurrentTable.Columns.Item("JamPardakhti").Visible = True
            GridEXSatr.CurrentTable.Columns.Item("JamPardakhti").Width = 100
            GridEXSatr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXSatr.CurrentTable.Columns.Item("JamPardakhti").Position = 4
            GridEXSatr.CurrentTable.Columns.Item("JamPardakhti").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXSatr.CurrentTable.Columns.Item("MandehFaktor").Caption = "مانده"
            GridEXSatr.CurrentTable.Columns.Item("MandehFaktor").HeaderStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXSatr.CurrentTable.Columns.Item("MandehFaktor").Visible = True
            GridEXSatr.CurrentTable.Columns.Item("MandehFaktor").Width = 100
            GridEXSatr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXSatr.CurrentTable.Columns.Item("MandehFaktor").Position = 5
            GridEXSatr.CurrentTable.Columns.Item("MandehFaktor").CellStyle.BackColor = Color.Yellow
            GridEXSatr.CurrentTable.Columns.Item("MandehFaktor").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXSatr.CurrentTable.Columns.Item("MablaghNaghd").Caption = "مبلغ نقــد"
            GridEXSatr.CurrentTable.Columns.Item("MablaghNaghd").HeaderStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXSatr.CurrentTable.Columns.Item("MablaghNaghd").Visible = True
            GridEXSatr.CurrentTable.Columns.Item("MablaghNaghd").Width = 90
            GridEXSatr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXSatr.CurrentTable.Columns.Item("MablaghNaghd").Position = 6
            GridEXSatr.CurrentTable.Columns.Item("MablaghNaghd").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXSatr.CurrentTable.Columns.Item("MNaghdP").Caption = "نقد پرداخت شده"
            GridEXSatr.CurrentTable.Columns.Item("MNaghdP").HeaderStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXSatr.CurrentTable.Columns.Item("MNaghdP").Visible = True
            GridEXSatr.CurrentTable.Columns.Item("MNaghdP").Width = 110
            GridEXSatr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXSatr.CurrentTable.Columns.Item("MNaghdP").Position = 7
            GridEXSatr.CurrentTable.Columns.Item("MNaghdP").EditType = Janus.Windows.GridEX.EditType.NoEdit
            GridEXSatr.CurrentTable.Columns.Item("MNaghdP").CellStyle.BackColor = Color.Moccasin
            GridEXSatr.CurrentTable.Columns.Item("MNaghdP").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXSatr.CurrentTable.Columns.Item("MablaghChek").Caption = "مبلغ چـک"
            GridEXSatr.CurrentTable.Columns.Item("MablaghChek").HeaderStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXSatr.CurrentTable.Columns.Item("MablaghChek").Visible = True
            GridEXSatr.CurrentTable.Columns.Item("MablaghChek").Width = 90
            GridEXSatr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXSatr.CurrentTable.Columns.Item("MablaghChek").Position = 8
            GridEXSatr.CurrentTable.Columns.Item("MablaghChek").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXSatr.CurrentTable.Columns.Item("MChekP").Caption = "چک پرداخت شده"
            GridEXSatr.CurrentTable.Columns.Item("MChekP").HeaderStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXSatr.CurrentTable.Columns.Item("MChekP").Visible = True
            GridEXSatr.CurrentTable.Columns.Item("MChekP").Width = 110
            GridEXSatr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXSatr.CurrentTable.Columns.Item("MChekP").Position = 9
            GridEXSatr.CurrentTable.Columns.Item("MChekP").EditType = Janus.Windows.GridEX.EditType.NoEdit
            GridEXSatr.CurrentTable.Columns.Item("MChekP").CellStyle.BackColor = Color.Moccasin
            GridEXSatr.CurrentTable.Columns.Item("MChekP").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXSatr.CurrentTable.Columns.Item("MablaghResid").Caption = "مبلغ رسیـد"
            GridEXSatr.CurrentTable.Columns.Item("MablaghResid").HeaderStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXSatr.CurrentTable.Columns.Item("MablaghResid").Visible = True
            GridEXSatr.CurrentTable.Columns.Item("MablaghResid").Width = 90
            GridEXSatr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXSatr.CurrentTable.Columns.Item("MablaghResid").Position = 10
            GridEXSatr.CurrentTable.Columns.Item("MablaghResid").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXSatr.CurrentTable.Columns.Item("MablaghKartKhan").Caption = "مبلغ کارت خوان"
            GridEXSatr.CurrentTable.Columns.Item("MablaghKartKhan").HeaderStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXSatr.CurrentTable.Columns.Item("MablaghKartKhan").Visible = True
            GridEXSatr.CurrentTable.Columns.Item("MablaghKartKhan").Width = 100
            GridEXSatr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXSatr.CurrentTable.Columns.Item("MablaghKartKhan").Position = 11
            GridEXSatr.CurrentTable.Columns.Item("MablaghKartKhan").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXSatr.CurrentTable.Columns.Item("MablaghMarjoee").Caption = "مبلغ مرجوعی"
            GridEXSatr.CurrentTable.Columns.Item("MablaghMarjoee").HeaderStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXSatr.CurrentTable.Columns.Item("MablaghMarjoee").Visible = True
            GridEXSatr.CurrentTable.Columns.Item("MablaghMarjoee").Width = 90
            GridEXSatr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXSatr.CurrentTable.Columns.Item("MablaghMarjoee").Position = 12
            GridEXSatr.CurrentTable.Columns.Item("MablaghMarjoee").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXSatr.CurrentTable.Columns.Item("MMarjoeeP").Caption = "مرجوعی ثبت شده"
            GridEXSatr.CurrentTable.Columns.Item("MMarjoeeP").HeaderStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXSatr.CurrentTable.Columns.Item("MMarjoeeP").Visible = True
            GridEXSatr.CurrentTable.Columns.Item("MMarjoeeP").Width = 120
            GridEXSatr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXSatr.CurrentTable.Columns.Item("MMarjoeeP").Position = 13
            GridEXSatr.CurrentTable.Columns.Item("MMarjoeeP").EditType = Janus.Windows.GridEX.EditType.NoEdit
            GridEXSatr.CurrentTable.Columns.Item("MMarjoeeP").CellStyle.BackColor = Color.Moccasin
            GridEXSatr.CurrentTable.Columns.Item("MMarjoeeP").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXSatr.CurrentTable.Columns.Item("MablaghTakhfif").Caption = "مبلغ تخفیف"
            GridEXSatr.CurrentTable.Columns.Item("MablaghTakhfif").HeaderStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXSatr.CurrentTable.Columns.Item("MablaghTakhfif").Visible = True
            GridEXSatr.CurrentTable.Columns.Item("MablaghTakhfif").Width = 90
            GridEXSatr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXSatr.CurrentTable.Columns.Item("MablaghTakhfif").Position = 14
            GridEXSatr.CurrentTable.Columns.Item("MablaghTakhfif").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXSatr.CurrentTable.Columns.Item("MTabdilBeTakhfif").Caption = "تبدیل به تخفیف"
            GridEXSatr.CurrentTable.Columns.Item("MTabdilBeTakhfif").HeaderStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXSatr.CurrentTable.Columns.Item("MTabdilBeTakhfif").Visible = True
            GridEXSatr.CurrentTable.Columns.Item("MTabdilBeTakhfif").Width = 110
            GridEXSatr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXSatr.CurrentTable.Columns.Item("MTabdilBeTakhfif").Position = 15
            GridEXSatr.CurrentTable.Columns.Item("MTabdilBeTakhfif").EditType = Janus.Windows.GridEX.EditType.NoEdit
            GridEXSatr.CurrentTable.Columns.Item("MTabdilBeTakhfif").CellStyle.BackColor = Color.Moccasin
            GridEXSatr.CurrentTable.Columns.Item("MTabdilBeTakhfif").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXSatr.CurrentTable.Columns.Item("MablaghBestankary").Caption = "پرداختی از بستانکاری"
            GridEXSatr.CurrentTable.Columns.Item("MablaghBestankary").HeaderStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXSatr.CurrentTable.Columns.Item("MablaghBestankary").Visible = True
            GridEXSatr.CurrentTable.Columns.Item("MablaghBestankary").Width = 140
            GridEXSatr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXSatr.CurrentTable.Columns.Item("MablaghBestankary").Position = 16
            GridEXSatr.CurrentTable.Columns.Item("MablaghBestankary").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXSatr.CurrentTable.Columns.Item("ccTafkikJozeTasfiehSatr").Caption = "ccTafkikJozeTasfiehSatr"
            GridEXSatr.CurrentTable.Columns.Item("ccTafkikJozeTasfiehSatr").Visible = False
            GridEXSatr.CurrentTable.Columns.Item("ccTafkikJozeTasfiehSatr").Width = 0
            GridEXSatr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXSatr.CurrentTable.Columns.Item("ccTafkikJozeTasfiehSatr").Position = 17

            GridEXSatr.CurrentTable.Columns.Item("ccMoshtary").Caption = "ccMoshtary"
            GridEXSatr.CurrentTable.Columns.Item("ccMoshtary").Visible = False
            GridEXSatr.CurrentTable.Columns.Item("ccMoshtary").Width = 0
            GridEXSatr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXSatr.CurrentTable.Columns.Item("ccMoshtary").Position = 18

            GridEXSatr.CurrentTable.Columns.Item("ccFaktorTitr").Caption = "ccFaktorTitr"
            GridEXSatr.CurrentTable.Columns.Item("ccFaktorTitr").Visible = False
            GridEXSatr.CurrentTable.Columns.Item("ccFaktorTitr").Width = 0
            GridEXSatr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXSatr.CurrentTable.Columns.Item("ccFaktorTitr").Position = 19

            GridEXSatr.CurrentTable.Columns.Item("ccMamorPakhsh").Caption = "ccMamorPakhsh"
            GridEXSatr.CurrentTable.Columns.Item("ccMamorPakhsh").Visible = False
            GridEXSatr.CurrentTable.Columns.Item("ccMamorPakhsh").Width = 0
            GridEXSatr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXSatr.CurrentTable.Columns.Item("ccMamorPakhsh").Position = 20

            For i As Integer = 0 To GridEXSatr.RootTable.Columns.Count - 1
                If GridEXSatr.RootTable.Columns(i).Type.IsValueType Then
                    GridEXSatr.RootTable.Columns(i).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                    GridEXSatr.RootTable.Columns(i).FormatMode = Janus.Windows.GridEX.FormatMode.UseIFormattable
                    GridEXSatr.RootTable.Columns(i).FormatString = "N"
                    GridEXSatr.RootTable.Columns(i).TotalFormatMode = Janus.Windows.GridEX.FormatMode.UseIFormattable
                    GridEXSatr.RootTable.Columns(i).TotalFormatString = "N"
                End If
            Next

            GridEXSatr.Visible = True

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> SetGridStyleSatr")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> SetGridStyleSatr")
        End Try
    End Sub
    Private Sub SetTitrButton()
        Try
            Select Case Mode
                Case UD_Dll.Enums.GL_ModeForms.UpdateRecord
                    GridEXTitr.Height = GridSize
                    GridEXTitr.Enabled = False
                    GridEXSatr.Enabled = False
                    btnExit.Enabled = False
                    gbSearch.Enabled = False
                    lblNaghdTitr.Visible = True
                    mskNaghdTitr.Visible = True
                    lblCheckTitr.Visible = True
                    mskCheckTitr.Visible = True
                    lblResidTitr.Visible = True
                    mskResidTitr.Visible = True
                    lblKartKhanTitr.Visible = True
                    mskKartKhanTitr.Visible = True
                    lblMarjoeeTitr.Visible = True
                    mskMarjoeeTitr.Visible = True
                    lblTakhfifTitr.Visible = True
                    mskTakhfifTitr.Visible = True
                    btnSaveTitr.Visible = True
                    btnCancelTitr.Visible = True
                    lblR1.Visible = True
                    lblR2.Visible = True
                    lblR3.Visible = True
                    lblR4.Visible = True
                    lblR5.Visible = True
                    lblR6.Visible = True
                Case UD_Dll.Enums.GL_ModeForms.None
                    GridEXTitr.Height = GridOrgSize
                    GridEXTitr.Enabled = True
                    GridEXSatr.Enabled = True
                    btnExit.Enabled = True
                    gbSearch.Enabled = True
                    lblNaghdTitr.Visible = False
                    mskNaghdTitr.Visible = False
                    lblCheckTitr.Visible = False
                    mskCheckTitr.Visible = False
                    lblResidTitr.Visible = False
                    mskResidTitr.Visible = False
                    lblKartKhanTitr.Visible = False
                    mskKartKhanTitr.Visible = False
                    lblMarjoeeTitr.Visible = False
                    mskMarjoeeTitr.Visible = False
                    lblTakhfifTitr.Visible = False
                    mskTakhfifTitr.Visible = False
                    btnSaveTitr.Visible = False
                    btnCancelTitr.Visible = False
                    lblR1.Visible = False
                    lblR2.Visible = False
                    lblR3.Visible = False
                    lblR4.Visible = False
                    lblR5.Visible = False
                    lblR6.Visible = False
            End Select
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->SetTitrButton")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->SetTitrButton")
        End Try

    End Sub
    Private Sub SetSatrButton()
        Try
            Select Case Mode
                Case UD_Dll.Enums.GL_ModeForms.UpdateRow
                    GridEXSatr.Height = GridSize
                    GridEXTitr.Enabled = False
                    GridEXSatr.Enabled = False
                    btnExit.Enabled = False
                    gbSearch.Enabled = False
                    lblNaghdSatr.Visible = True
                    mskNaghdSatr.Visible = True
                    lblCheckSatr.Visible = True
                    mskCheckSatr.Visible = True
                    lblResidSatr.Visible = True
                    mskResidSatr.Visible = True
                    lblKartKhanSatr.Visible = True
                    mskKartKhanSatr.Visible = True
                    lblMarjoeeSatr.Visible = True
                    mskMarjoeeSatr.Visible = True
                    lblTakhfifSatr.Visible = True
                    mskTakhfifSatr.Visible = True
                    btnBestankarySatr.Visible = True
                    mskBestankarySatr.Visible = True
                    btnSaveSatr.Visible = True
                    btnCancelSatr.Visible = True
                    lblR7.Visible = True
                    lblR8.Visible = True
                    lblR9.Visible = True
                    lblR10.Visible = True
                    lblR11.Visible = True
                    lblR12.Visible = True
                    lblR13.Visible = True
                Case UD_Dll.Enums.GL_ModeForms.None
                    GridEXSatr.Height = GridOrgSize
                    GridEXTitr.Enabled = True
                    GridEXSatr.Enabled = True
                    btnExit.Enabled = True
                    gbSearch.Enabled = True
                    lblNaghdSatr.Visible = False
                    mskNaghdSatr.Visible = False
                    lblCheckSatr.Visible = False
                    mskCheckSatr.Visible = False
                    lblResidSatr.Visible = False
                    mskResidSatr.Visible = False
                    lblKartKhanSatr.Visible = False
                    mskKartKhanSatr.Visible = False
                    lblMarjoeeSatr.Visible = False
                    mskMarjoeeSatr.Visible = False
                    lblTakhfifSatr.Visible = False
                    mskTakhfifSatr.Visible = False
                    btnBestankarySatr.Visible = False
                    mskBestankarySatr.Visible = False
                    btnSaveSatr.Visible = False
                    btnCancelSatr.Visible = False
                    lblR7.Visible = False
                    lblR8.Visible = False
                    lblR9.Visible = False
                    lblR10.Visible = False
                    lblR11.Visible = False
                    lblR12.Visible = False
                    lblR13.Visible = False
                    mskResidSatr.Enabled = True
                    mskCheckSatr.Enabled = True
                    mskNaghdSatr.Enabled = True
                    FlgSetForm = False
            End Select
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->SetSatrButton")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->SetSatrButton")
        End Try
    End Sub
    Private Function IsValidTaeedNahaei(ByVal CheckField As String, ByVal i As Integer) As Boolean
        IsValidTaeedNahaei = False

        Dim MandehTafkik As Integer = GridEXTitr.GetCheckedRows(i).Cells("MandehTafkik").Value
        Dim MJamSatr As Double = objTools.ConvertNulls(objTools.DSum("(MablaghNaghd + MablaghChek + MablaghResid + MablaghKartKhan + MablaghMarjoee + MablaghTakhfif + MablaghBestankary)", "Sales.TafkikJozeTasfiehSatr", "ccTafkikJozeTasfieh = " & GridEXTitr.GetCheckedRows(i).Cells("ccTafkikJozeTasfieh").Value), 0)
        Dim MandehTafkikSatr As Double = GridEXTitr.GetCheckedRows(i).Cells("MablaghKolTafkik").Value - (MJamSatr + GridEXTitr.GetCheckedRows(i).Cells("MNaghdP").Value + GridEXTitr.GetCheckedRows(i).Cells("MChekP").Value + GridEXTitr.GetCheckedRows(i).Cells("MMarjoeeP").Value + GridEXTitr.GetCheckedRows(i).Cells("MTabdilBeTakhfif").Value)

        If CheckField = "All" Then
            If MandehTafkik <> 0 Then
                MsgBox("برگه تفکیک شماره " & Trim(GridEXTitr.GetCheckedRows(i).Cells("ShomarehTafkik").Value) & " به طور کامل تسویه نشده است !", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
                Exit Function
            End If
            If MandehTafkikSatr <> 0 Then
                MsgBox("فاکتورهای برگه تفکیک شماره " & Trim(GridEXTitr.GetCheckedRows(i).Cells("ShomarehTafkik").Value) & " به طور کامل تسویه نشده است !", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
                Exit Function
            End If
        End If

        'Dim MKolTafkik As Integer = GridEXTitr.GetCheckedRows(i).Cells("MablaghKolTafkik").Value

        'Dim MNaghdTitr As Integer = GridEXTitr.GetCheckedRows(i).Cells("MablaghNaghd").Value
        'Dim MChekTitr As Integer = GridEXTitr.GetCheckedRows(i).Cells("MablaghChek").Value
        'Dim MResidTitr As Integer = GridEXTitr.GetCheckedRows(i).Cells("MablaghResid").Value
        'Dim MKartKhanTitr As Integer = GridEXTitr.GetCheckedRows(i).Cells("MablaghKartKhan").Value
        'Dim MMarjoeeTitr As Integer = GridEXTitr.GetCheckedRows(i).Cells("MablaghMarjoee").Value
        'Dim MTakhfifTitr As Integer = GridEXTitr.GetCheckedRows(i).Cells("MablaghTakhfif").Value

        'If CheckField = "All" Then
        '    If MKolTafkik > MNaghdTitr + MChekTitr + MResidTitr + MKartKhanTitr + MMarjoeeTitr + MTakhfifTitr Then
        '        MsgBox("برگه تفکیک شماره " & Trim(GridEXTitr.GetCheckedRows(i).Cells("ShomarehTafkik").Value) & " به طور کامل تسویه نشده است !", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
        '        Exit Function
        '    End If
        'End If

        Return True
    End Function
    Private Function IsValidMablaghTitr(ByVal CheckField As String) As Boolean
        IsValidMablaghTitr = False

        Dim MkolTafkik As Integer = dvTitr(cmTitr.Position)("MablaghKolTafkik")

        Dim SumNaghdSatr As Integer = objTools.ConvertNulls(objTools.DSum("MablaghNaghd", "Sales.TafkikJozeTasfiehSatr", "ccTafkikJozeTasfieh = " & GridEXTitr.CurrentRow.Cells("ccTafkikJozeTasfieh").Value), 0)
        Dim SumChekSatr As Integer = objTools.ConvertNulls(objTools.DSum("MablaghChek", "Sales.TafkikJozeTasfiehSatr", "ccTafkikJozeTasfieh = " & GridEXTitr.CurrentRow.Cells("ccTafkikJozeTasfieh").Value), 0)
        Dim SumResidSatr As Integer = objTools.ConvertNulls(objTools.DSum("MablaghResid", "Sales.TafkikJozeTasfiehSatr", "ccTafkikJozeTasfieh = " & GridEXTitr.CurrentRow.Cells("ccTafkikJozeTasfieh").Value), 0)
        Dim SumKartKhanSatr As Integer = objTools.ConvertNulls(objTools.DSum("MablaghKartKhan", "Sales.TafkikJozeTasfiehSatr", "ccTafkikJozeTasfieh = " & GridEXTitr.CurrentRow.Cells("ccTafkikJozeTasfieh").Value), 0)
        Dim SumMarjoeeSatr As Integer = objTools.ConvertNulls(objTools.DSum("MablaghMarjoee", "Sales.TafkikJozeTasfiehSatr", "ccTafkikJozeTasfieh = " & GridEXTitr.CurrentRow.Cells("ccTafkikJozeTasfieh").Value), 0)
        Dim SumTakhfifSatr As Integer = objTools.ConvertNulls(objTools.DSum("MablaghTakhfif", "Sales.TafkikJozeTasfiehSatr", "ccTafkikJozeTasfieh = " & GridEXTitr.CurrentRow.Cells("ccTafkikJozeTasfieh").Value), 0)

        Dim SumTafkik As Integer = Val(mskNaghdTitr.Text) + Val(mskCheckTitr.Text) + Val(mskResidTitr.Text) + Val(mskKartKhanTitr.Text) + Val(mskMarjoeeTitr.Text) + Val(mskTakhfifTitr.Text) + GridEXTitr.CurrentRow.Cells("MNaghdP").Value + GridEXTitr.CurrentRow.Cells("MChekP").Value + GridEXTitr.CurrentRow.Cells("MMarjoeeP").Value + GridEXTitr.CurrentRow.Cells("MTabdilBeTakhfif").Value

        If CheckField = "All" Then
            If MkolTafkik < SumTafkik Then
                ErrPro.SetError(Me.mskNaghdTitr, "مبالغ ورودی را اصلاح نمایید .")
                MsgBox("مجموع مبالغ وارد شده از مبلغ کل برگه تفکیک شماره " & GridEXTitr.CurrentRow.Cells("ShomarehTafkik").Value & " بیشتر است !", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "خطـــا")
                mskNaghdTitr.Focus()
                Exit Function
            End If
            ErrPro.SetError(Me.mskNaghdTitr, "")
        End If

        If CheckField = "MablaghNaghd" Or CheckField = "All" Then
            If Val(mskNaghdTitr.Text) < SumNaghdSatr Then
                ErrPro.SetError(Me.mskNaghdTitr, "مبلغ ورودی را اصلاح نمایید .")
                MsgBox("مبلغ نقدی وارد شده برای برگه تفکیک نمی تواند کوچکتر از مجموع مبالغ نقدی وارد شده برای فاکتورهای آن برگه تفکیک باشد !", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
                mskNaghdTitr.Focus()
                Exit Function
            End If
            ErrPro.SetError(Me.mskNaghdTitr, "")
        End If

        If CheckField = "MablaghChek" Or CheckField = "All" Then
            If Val(mskCheckTitr.Text) < SumChekSatr Then
                ErrPro.SetError(Me.mskCheckTitr, "مبلغ ورودی را اصلاح نمایید .")
                MsgBox("مبلغ چک وارد شده برای برگه تفکیک نمی تواند کوچکتر از مجموع مبالغ چکهای وارد شده برای فاکتورهای آن برگه تفکیک باشد !", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
                mskCheckTitr.Focus()
                Exit Function
            End If
            ErrPro.SetError(Me.mskCheckTitr, "")
        End If

        If CheckField = "MablaghResid" Or CheckField = "All" Then
            If Val(mskResidTitr.Text) < SumResidSatr Then
                ErrPro.SetError(Me.mskResidTitr, "مبلغ ورودی را اصلاح نمایید .")
                MsgBox("مبلغ رسید وارد شده برای برگه تفکیک نمی تواند کوچکتر از مجموع مبالغ رسیدهای وارد شده برای فاکتورهای آن برگه تفکیک باشد !", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
                mskResidTitr.Focus()
                Exit Function
            End If
            ErrPro.SetError(Me.mskResidTitr, "")
        End If

        If CheckField = "MablaghKartKhan" Or CheckField = "All" Then
            If Val(mskKartKhanTitr.Text) < SumKartKhanSatr Then
                ErrPro.SetError(Me.mskKartKhanTitr, "مبلغ ورودی را اصلاح نمایید .")
                MsgBox("مبلغ کارتخوان وارد شده برای برگه تفکیک نمی تواند کوچکتر از مجموع مبالغ کارتخوان وارد شده برای فاکتورهای آن برگه تفکیک باشد !", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
                mskKartKhanTitr.Focus()
                Exit Function
            End If
            ErrPro.SetError(Me.mskKartKhanTitr, "")
        End If

        If CheckField = "MablaghMarjoee" Or CheckField = "All" Then
            If Val(mskMarjoeeTitr.Text) < SumMarjoeeSatr Then
                ErrPro.SetError(Me.mskMarjoeeTitr, "مبلغ ورودی را اصلاح نمایید .")
                MsgBox("مبلغ مرجوعی وارد شده برای برگه تفکیک نمی تواند کوچکتر از مجموع مبالغ مرجوعی های وارد شده برای فاکتورهای آن برگه تفکیک باشد !", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
                mskMarjoeeTitr.Focus()
                Exit Function
            End If
            ErrPro.SetError(Me.mskMarjoeeTitr, "")
        End If

        If CheckField = "MablaghTakhfif" Or CheckField = "All" Then
            If Val(mskTakhfifTitr.Text) < SumTakhfifSatr Then
                ErrPro.SetError(Me.mskTakhfifTitr, "مبلغ ورودی را اصلاح نمایید .")
                MsgBox("مبلغ تخفیف وارد شده برای برگه تفکیک نمی تواند کوچکتر از مجموع مبالغ تخفیفات وارد شده برای فاکتورهای آن برگه تفکیک باشد !", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
                mskTakhfifTitr.Focus()
                Exit Function
            End If
            ErrPro.SetError(Me.mskTakhfifTitr, "")
        End If
        Return True
    End Function
    Private Function IsValidMablaghSatr(ByVal CheckField As String) As Boolean
        IsValidMablaghSatr = False

        Dim MNaghdTitr As Integer = GridEXTitr.CurrentRow.Cells("MablaghNaghd").Value
        Dim MChekTitr As Integer = GridEXTitr.CurrentRow.Cells("MablaghChek").Value
        Dim MResidTitr As Integer = GridEXTitr.CurrentRow.Cells("MablaghResid").Value
        Dim MKartKhanTitr As Integer = GridEXTitr.CurrentRow.Cells("MablaghKartKhan").Value
        Dim MMarjoeeTitr As Integer = GridEXTitr.CurrentRow.Cells("MablaghMarjoee").Value
        Dim MTakhfifTitr As Integer = GridEXTitr.CurrentRow.Cells("MablaghTakhfif").Value
        Dim MBestankaryTitr As Integer = GridEXTitr.CurrentRow.Cells("MablaghBestankary").Value

        Dim JamFaktor As Integer = Val(GridEXSatr.CurrentRow.Cells("JamKol").Text.Replace(",", ""))
        Dim SumFaktor As Double = CType(mskNaghdSatr.Text, Integer) + CType(mskCheckSatr.Text, Integer) + CType(mskResidSatr.Text, Integer) + CType(mskKartKhanSatr.Text, Integer) + CType(mskMarjoeeSatr.Text, Integer) + CType(mskTakhfifSatr.Text, Integer) + CType(mskBestankarySatr.Text, Integer) + GridEXSatr.CurrentRow.Cells("MNaghdP").Value + GridEXSatr.CurrentRow.Cells("MChekP").Value + GridEXSatr.CurrentRow.Cells("MMarjoeeP").Value + GridEXSatr.CurrentRow.Cells("MTabdilBeTakhfif").Value

        Dim SumNaghd As Integer = objTools.ConvertNulls(objTools.DSum("MablaghNaghd", "Sales.TafkikJozeTasfiehSatr", "ccTafkikJozeTasfieh = " & GridEXTitr.CurrentRow.Cells("ccTafkikJozeTasfieh").Value & " AND ccTafkikJozeTasfiehSatr <> " & GridEXSatr.CurrentRow.Cells("ccTafkikJozeTasfiehSatr").Value), 0) + Val(mskNaghdSatr.Text)
        Dim SumChek As Integer = objTools.ConvertNulls(objTools.DSum("MablaghChek", "Sales.TafkikJozeTasfiehSatr", "ccTafkikJozeTasfieh = " & GridEXTitr.CurrentRow.Cells("ccTafkikJozeTasfieh").Value & " AND ccTafkikJozeTasfiehSatr <> " & GridEXSatr.CurrentRow.Cells("ccTafkikJozeTasfiehSatr").Value), 0) + Val(mskCheckSatr.Text)
        Dim SumResid As Integer = objTools.ConvertNulls(objTools.DSum("MablaghResid", "Sales.TafkikJozeTasfiehSatr", "ccTafkikJozeTasfieh = " & GridEXTitr.CurrentRow.Cells("ccTafkikJozeTasfieh").Value & " AND ccTafkikJozeTasfiehSatr <> " & GridEXSatr.CurrentRow.Cells("ccTafkikJozeTasfiehSatr").Value), 0) + Val(mskResidSatr.Text)
        Dim SumKartKhan As Integer = objTools.ConvertNulls(objTools.DSum("MablaghKartKhan", "Sales.TafkikJozeTasfiehSatr", "ccTafkikJozeTasfieh = " & GridEXTitr.CurrentRow.Cells("ccTafkikJozeTasfieh").Value & " AND ccTafkikJozeTasfiehSatr <> " & GridEXSatr.CurrentRow.Cells("ccTafkikJozeTasfiehSatr").Value), 0) + Val(mskKartKhanSatr.Text)
        Dim SumMarjoee As Integer = objTools.ConvertNulls(objTools.DSum("MablaghMarjoee", "Sales.TafkikJozeTasfiehSatr", "ccTafkikJozeTasfieh = " & GridEXTitr.CurrentRow.Cells("ccTafkikJozeTasfieh").Value & " AND ccTafkikJozeTasfiehSatr <> " & GridEXSatr.CurrentRow.Cells("ccTafkikJozeTasfiehSatr").Value), 0) + Val(mskMarjoeeSatr.Text)
        Dim SumTakhfif As Integer = objTools.ConvertNulls(objTools.DSum("MablaghTakhfif", "Sales.TafkikJozeTasfiehSatr", "ccTafkikJozeTasfieh = " & GridEXTitr.CurrentRow.Cells("ccTafkikJozeTasfieh").Value & " AND ccTafkikJozeTasfiehSatr <> " & GridEXSatr.CurrentRow.Cells("ccTafkikJozeTasfiehSatr").Value), 0) + Val(mskTakhfifSatr.Text)
        Dim SumBestankary As Integer = objTools.ConvertNulls(objTools.DSum("MablaghBestankary", "Sales.TafkikJozeTasfiehSatr", "ccTafkikJozeTasfieh = " & GridEXTitr.CurrentRow.Cells("ccTafkikJozeTasfieh").Value & " AND ccTafkikJozeTasfiehSatr <> " & GridEXSatr.CurrentRow.Cells("ccTafkikJozeTasfiehSatr").Value), 0) + Val(mskTakhfifSatr.Text)

        If CheckField = "All" Then
            If JamFaktor < SumFaktor Then
                ErrPro.SetError(Me.mskNaghdSatr, "مبالغ ورودی را اصلاح نمایید .")
                MsgBox("مجموع مبالغ وارد شده از مبلغ کل فاکتور شماره " & Val(GridEXSatr.CurrentRow.Cells("FaktorShomareh").Text.Replace(",", "")) & " بیشتر است !", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "خطـــا")
                mskNaghdSatr.Focus()
                Exit Function
            End If
            ErrPro.SetError(Me.mskNaghdSatr, "")
        End If

        If CheckField = "MablaghNaghd" Or CheckField = "All" Then
            If MNaghdTitr < SumNaghd Then
                ErrPro.SetError(Me.mskNaghdSatr, "مبلغ ورودی را اصلاح نمایید .")
                MsgBox("مجموع مبالغ نقد وارد شده برای فاکتور ها از مبلغ نقدی ثبت شده برای برگه تفکیک بیشتر است !", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
                mskNaghdSatr.Focus()
                Exit Function
            End If
            ErrPro.SetError(Me.mskNaghdSatr, "")
        End If

        If CheckField = "MablaghChek" Or CheckField = "All" Then
            If MChekTitr < SumChek Then
                ErrPro.SetError(Me.mskCheckSatr, "مبلغ ورودی را اصلاح نمایید .")
                MsgBox("مجموع مبالغ چکهای وارد شده برای فاکتور ها از مبلغ چک ثبت شده برای برگه تفکیک بیشتر است !", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
                mskCheckSatr.Focus()
                Exit Function
            End If
            ErrPro.SetError(Me.mskCheckSatr, "")
        End If

        If CheckField = "MablaghResid" Or CheckField = "All" Then
            If MResidTitr < SumResid Then
                ErrPro.SetError(Me.mskResidSatr, "مبلغ ورودی را اصلاح نمایید .")
                MsgBox("مجموع مبالغ رسیدهای وارد شده برای فاکتور ها از مبلغ رسید ثبت شده برای برگه تفکیک بیشتر است !", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
                mskResidSatr.Focus()
                Exit Function
            End If
            ErrPro.SetError(Me.mskResidSatr, "")
        End If

        If CheckField = "MablaghKartKhan" Or CheckField = "All" Then
            If MKartKhanTitr < SumKartKhan Then
                ErrPro.SetError(Me.mskKartKhanSatr, "مبلغ ورودی را اصلاح نمایید .")
                MsgBox("مجموع مبالغ کارتخوان وارد شده برای فاکتور ها از مبلغ کارتخوان ثبت شده برای برگه تفکیک بیشتر است !", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
                mskKartKhanSatr.Focus()
                Exit Function
            End If
            ErrPro.SetError(Me.mskKartKhanSatr, "")
        End If

        If CheckField = "MablaghMarjoee" Or CheckField = "All" Then
            If MMarjoeeTitr < SumMarjoee Then
                ErrPro.SetError(Me.mskMarjoeeSatr, "مبلغ ورودی را اصلاح نمایید .")
                MsgBox("مجموع مبالغ مرجوعی های وارد شده برای فاکتور ها از مبلغ مرجوعی ثبت شده برای برگه تفکیک بیشتر است !", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
                mskMarjoeeSatr.Focus()
                Exit Function
            End If
            ErrPro.SetError(Me.mskMarjoeeSatr, "")
        End If

        If CheckField = "MablaghTakhfif" Or CheckField = "All" Then
            If MTakhfifTitr < SumTakhfif Then
                ErrPro.SetError(Me.mskTakhfifSatr, "مبلغ ورودی را اصلاح نمایید .")
                MsgBox("مجموع تخفیفات وارد شده برای فاکتور ها از مبلغ تخفیف ثبت شده برای برگه تفکیک بیشتر است !", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
                mskTakhfifSatr.Focus()
                Exit Function
            End If
            ErrPro.SetError(Me.mskTakhfifSatr, "")
        End If
        Return True
    End Function
    Private Sub UpdateVazeiatTafkik(ByVal ccTafkikJozeTasfieh As Integer)
        Dim cn As New SqlConnection
        Dim cm As New SqlCommand
        Dim strSQL As String = ""

        strSQL = "Sales.spTafkikJozTasfieh_UpdateCodeVazeiat "

        cn = New SqlConnection(ConnectionString)
        cn.Open()

        cm = New SqlCommand(strSQL, cn)
        cm.CommandType = CommandType.StoredProcedure
        cm.Parameters.Clear()

        cm.Parameters.AddWithValue("ccTafkikJozeTasfieh", ccTafkikJozeTasfieh)

        cm.ExecuteNonQuery()

        cm.Connection.Close()
        cn.Close()

    End Sub
    Private Sub AddNewRecordNaghd(ByVal ccFaktorTitr As Integer)

        Windows.Forms.Cursor.Current = Cursors.WaitCursor
        Dim cnSQL As SqlConnection : Dim cmSQL As SqlCommand
        Dim daSQL As SqlDataAdapter
        Dim dt1 As New DataTable
        Dim strSQL As String

        Dim ccMamorPakhsh As Integer = objTools.DLookup("ccMamorPakhsh", "tblFO_Faktor", "ccFaktorTitr = " & ccFaktorTitr)
        Dim ShomarehFaktor As Integer = objTools.DLookup("FaktorShomareh", "tblFO_Faktor", "ccFaktorTitr = " & ccFaktorTitr)
        Dim ccMoshtary As Integer = objTools.DLookup("ccMoshtary", "tblFO_Faktor", "ccFaktorTitr = " & ccFaktorTitr)
        Dim NameMoshtary As String = objTools.DLookup("NameMoshtary", "tblFO_Moshtary", "ccMoshtary = " & ccMoshtary)
        Dim CodeAmalyat As Integer = 0
        Dim ccAmalyat As Integer = 0

        cnSQL = New SqlConnection(ConnectionString)
        cnSQL.Open()

        strSQL = "Sales.spTasviehTafkik_SearchNaghdForFaktor "

        cmSQL = New SqlCommand(strSQL, cnSQL)
        cmSQL.CommandType = CommandType.StoredProcedure
        cmSQL.Parameters.Clear()

        cmSQL.Parameters.AddWithValue("ccFaktor", ccFaktorTitr)

        daSQL = New SqlDataAdapter(cmSQL)

        daSQL.Fill(dt1)

        cmSQL.Connection.Close()
        cnSQL.Close()

        If dt1.Rows.Count = 0 Then
            Exit Sub
        End If

        For Each dr As DataRow In dt1.Rows
            Try
                cnSQL = New SqlConnection(ConnectionString)
                cnSQL.Open()

                Dim CodeTafsily As Long = objTools.DLookup("CodeTafsily1", "tblFO_Moshtary", "ccMoshtary =" & ccMoshtary)
                ' Get Goroh Kol Moeen Vaset Bank 
                Dim GKM As String = ObjCode.GetCodeHesabFaktorMoshtary(ccFaktorTitr)
                If GKM = "" Then
                    Throw New Exception("به مشتری مورد نظر کد تفصیلی تخصیص داده نشده است.")
                End If
                Dim G_CodeGoroh As String = GKM.Substring(0, 1)
                Dim G_CodeKol As String = GKM.Substring(1, 2)
                Dim G_CodeMoeen As String = GKM.Substring(3, 2)

                strSQL = "Treasury.spVosolFaktor_DaryaftNaghd_Amalyat_Insert "

                cmSQL = New SqlCommand(strSQL, cnSQL)
                cmSQL.CommandType = CommandType.StoredProcedure
                cmSQL.Parameters.Clear()

                tSaatVazeiat = Format(TimeOfDay, "HH:mm:ss")

                cmSQL.Parameters.AddWithValue("CodeMahal", CodeMahalFaal)
                cmSQL.Parameters.AddWithValue("CodeDoreh", CodeDoreh)
                cmSQL.Parameters.AddWithValue("NoeAmalyat", UD_Dll.Enums.DP_NoeAmalyatDP.Daryaft)
                cmSQL.Parameters.AddWithValue("sNoeSanad", UD_Dll.Enums.DP_AnvaeSanadDP.D_NaghdyBeSandogh)
                cmSQL.Parameters.AddWithValue("ccShomarehHesab", dr("ccShomarehHesab"))
                cmSQL.Parameters.AddWithValue("CodeFard", dr("ccMamorPakhsh"))
                cmSQL.Parameters.AddWithValue("ShomarehResidMovaghat", dr("ShomarehResidMovaghat"))
                cmSQL.Parameters.AddWithValue("TarikhDP", dr("TarikhDP"))
                cmSQL.Parameters.AddWithValue("TarikhSanad", dr("TarikhSanad"))
                cmSQL.Parameters.AddWithValue("Mablagh", dr("MablaghKolNaghd"))
                cmSQL.Parameters.AddWithValue("CodeGoroh", G_CodeGoroh)
                cmSQL.Parameters.AddWithValue("CodeKol", G_CodeKol)
                cmSQL.Parameters.AddWithValue("CodeMoeen", G_CodeMoeen)
                cmSQL.Parameters.AddWithValue("Tafsily1", CodeTafsily)
                cmSQL.Parameters.AddWithValue("Tafsily2", 0)
                cmSQL.Parameters.AddWithValue("Tafsily3", 0)
                cmSQL.Parameters.AddWithValue("sVazeiat", UD_Dll.Enums.DP_Vazeiat.DP_BedoneAmalyat)
                cmSQL.Parameters.AddWithValue("TarikhVazeiat", TarikhEmrooz)
                cmSQL.Parameters.AddWithValue("Sharh", " بابت فاکتور شماره " & ShomarehFaktor)
                cmSQL.Parameters.AddWithValue("UserName", UserName)
                cmSQL.Parameters.AddWithValue("Tarikh", TarikhEmrooz)
                cmSQL.Parameters.AddWithValue("Saat", tSaatVazeiat)
                cmSQL.Parameters.AddWithValue("TasviehWithTafkik", 1)
                cmSQL.Parameters.AddWithValue("CodeAmalyat", CodeAmalyat)
                cmSQL.Parameters("CodeAmalyat").Direction = ParameterDirection.Output

                cmSQL.ExecuteNonQuery()

                ccAmalyat = cmSQL.Parameters("CodeAmalyat").Value

                Dim MablaghAmalyat As Integer = dr("MablaghKolNaghd")

                tTarikhDP = dr("TarikhDP")
                tSh = dr("ccShomarehHesab")
                tShHEntry = tSh
                tEbtal = False
                tVazeiat = UD_Dll.Enums.DP_Vazeiat.DP_BedoneAmalyat
                tTarikhVazeiat = TarikhEmrooz

                '--------------------

                strSQL = "Treasury.spVosolFaktor_DaryaftNaghd_AmalyatVazeiat_Insert "

                cmSQL = New SqlCommand(strSQL, cnSQL)
                cmSQL.CommandType = CommandType.StoredProcedure
                cmSQL.Parameters.Clear()

                cmSQL.Parameters.AddWithValue("CodeAmalyat", ccAmalyat)
                cmSQL.Parameters.AddWithValue("ccShomarehHesab", dr("ccShomarehHesab"))
                cmSQL.Parameters.AddWithValue("CodeFard", dr("ccMamorPakhsh"))
                cmSQL.Parameters.AddWithValue("CodeGoroh", G_CodeGoroh)
                cmSQL.Parameters.AddWithValue("CodeKol", G_CodeKol)
                cmSQL.Parameters.AddWithValue("CodeMoeen", G_CodeMoeen)
                cmSQL.Parameters.AddWithValue("Tafsily1", CodeTafsily)
                cmSQL.Parameters.AddWithValue("Tafsily2", 0)
                cmSQL.Parameters.AddWithValue("Tafsily3", 0)
                cmSQL.Parameters.AddWithValue("sNoeSanad", UD_Dll.Enums.DP_AnvaeSanadDP.D_NaghdyBeSandogh)
                cmSQL.Parameters.AddWithValue("sVazeiat", UD_Dll.Enums.DP_Vazeiat.DP_BedoneAmalyat)
                cmSQL.Parameters.AddWithValue("TarikhVazeiat", dr("TarikhDP"))
                cmSQL.Parameters.AddWithValue("SaatVazeiat", tSaatVazeiat)

                cmSQL.ExecuteNonQuery()

                '----------------------

                strSQL = "Treasury.spVosolFaktor_DaryaftNaghd_AmalyatFaktor_Insert "

                cmSQL = New SqlCommand(strSQL, cnSQL)
                cmSQL.CommandType = CommandType.StoredProcedure
                cmSQL.Parameters.Clear()

                cmSQL.Parameters.AddWithValue("CodeAmalyat", ccAmalyat)
                cmSQL.Parameters.AddWithValue("ccFaktorTitr", ccFaktorTitr)
                cmSQL.Parameters.AddWithValue("Mablagh", dr("PardakhtyInFaktor"))
                cmSQL.Parameters.AddWithValue("TasviehMoavagheh", 0)

                cmSQL.ExecuteNonQuery()

                cnSQL.Close()
                cmSQL = Nothing
                cnSQL = Nothing

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
            Finally
                Windows.Forms.Cursor.Current = Cursors.Default
            End Try
        Next
    End Sub
    Private Sub AddNewRecordChek(ByVal ccFaktorTitr As Integer)

        Windows.Forms.Cursor.Current = Cursors.WaitCursor
        Dim cnSQL As SqlConnection : Dim cmSQL As SqlCommand
        Dim daSQL As SqlDataAdapter
        Dim dt1 As New DataTable
        Dim strSQL As String

        Dim ccMamorPakhsh As Integer = objTools.DLookup("ccMamorPakhsh", "tblFO_Faktor", "ccFaktorTitr = " & ccFaktorTitr)
        Dim ShomarehFaktor As Integer = objTools.DLookup("FaktorShomareh", "tblFO_Faktor", "ccFaktorTitr = " & ccFaktorTitr)
        Dim ccMoshtary As Integer = objTools.DLookup("ccMoshtary", "tblFO_Faktor", "ccFaktorTitr = " & ccFaktorTitr)
        Dim NameMoshtary As String = objTools.DLookup("NameMoshtary", "tblFO_Moshtary", "ccMoshtary = " & ccMoshtary)
        Dim CodeAmalyat As Integer = 0
        Dim ccAmalyat As Integer = 0

        cnSQL = New SqlConnection(ConnectionString)
        cnSQL.Open()

        strSQL = "Sales.spTasviehTafkik_SearchChekForFaktor "

        cmSQL = New SqlCommand(strSQL, cnSQL)
        cmSQL.CommandType = CommandType.StoredProcedure
        cmSQL.Parameters.Clear()

        cmSQL.Parameters.AddWithValue("ccFaktor", ccFaktorTitr)

        daSQL = New SqlDataAdapter(cmSQL)

        daSQL.Fill(dt1)

        cmSQL.Connection.Close()
        cnSQL.Close()

        If dt1.Rows.Count = 0 Then
            Exit Sub
        End If

        For Each dr As DataRow In dt1.Rows

            Try
                cnSQL = New SqlConnection(ConnectionString)
                cnSQL.Open()

                Dim CodeTafsily As Long = objTools.DLookup("CodeTafsily1", "tblFO_Moshtary", "ccMoshtary =" & ccMoshtary)
                ' Get Goroh Kol Moeen Vaset Bank 
                Dim GKM As String = ObjCode.GetCodeHesabFaktorMoshtary(ccFaktorTitr)

                If GKM = "" Then
                    Throw New Exception("به مشتری مورد نظر کد تفصیلی تخصیص داده نشده است.")
                End If

                Dim G_CodeGoroh As String = GKM.Substring(0, 1)
                Dim G_CodeKol As String = GKM.Substring(1, 2)
                Dim G_CodeMoeen As String = GKM.Substring(3, 2)

                strSQL = "Treasury.spVosolFaktor_DaryaftCheck_Amalyat_Insert "

                cmSQL = New SqlCommand(strSQL, cnSQL)
                cmSQL.CommandType = CommandType.StoredProcedure
                cmSQL.Parameters.Clear()

                tSaatVazeiat = Format(TimeOfDay, "HH:mm:ss")

                cmSQL.Parameters.AddWithValue("CodeMahal", CodeMahalFaal)
                cmSQL.Parameters.AddWithValue("CodeDoreh", CodeDoreh)
                cmSQL.Parameters.AddWithValue("NoeAmalyat", UD_Dll.Enums.DP_NoeAmalyatDP.Daryaft)
                cmSQL.Parameters.AddWithValue("sNoeSanad", UD_Dll.Enums.DP_AnvaeSanadDP.D_CheckBeSandogh)
                cmSQL.Parameters.AddWithValue("ccShomarehHesab", dr("ccShomarehHesab"))
                cmSQL.Parameters.AddWithValue("CodeFard", dr("ccMamorPakhsh"))
                cmSQL.Parameters.AddWithValue("ShomarehResidMovaghat", 0)
                cmSQL.Parameters.AddWithValue("Noe2", 4417) ' -- check ady --
                cmSQL.Parameters.AddWithValue("TarikhDP", dr("TarikhDP"))
                cmSQL.Parameters.AddWithValue("TarikhSanad", dr("TarikhSanad"))
                cmSQL.Parameters.AddWithValue("ShomarehSanad", dr("ShomarehSanad"))
                cmSQL.Parameters.AddWithValue("NameTarafSanad", NameMoshtary)
                cmSQL.Parameters.AddWithValue("Mablagh", dr("MablaghKolChek"))
                cmSQL.Parameters.AddWithValue("sBankSanad", dr("sBank"))
                cmSQL.Parameters.AddWithValue("ShomarehHesabSanad", dr("ShomarehHesabSanad"))
                cmSQL.Parameters.AddWithValue("ShobehSanad", dr("ShobehSanad"))
                cmSQL.Parameters.AddWithValue("CodeShobehSanad", dr("CodeShobehSanad"))
                cmSQL.Parameters.AddWithValue("CodeGoroh", G_CodeGoroh)
                cmSQL.Parameters.AddWithValue("CodeKol", G_CodeKol)
                cmSQL.Parameters.AddWithValue("CodeMoeen", G_CodeMoeen)
                cmSQL.Parameters.AddWithValue("Tafsily1", CodeTafsily)
                cmSQL.Parameters.AddWithValue("Tafsily2", 0)
                cmSQL.Parameters.AddWithValue("Tafsily3", 0)
                cmSQL.Parameters.AddWithValue("sVazeiat", UD_Dll.Enums.DP_Vazeiat.DP_BedoneAmalyat)
                cmSQL.Parameters.AddWithValue("TarikhVazeiat", TarikhEmrooz)
                cmSQL.Parameters.AddWithValue("Sharh", " بابت فاکتور شماره " & ShomarehFaktor)
                cmSQL.Parameters.AddWithValue("UserName", UserName)
                cmSQL.Parameters.AddWithValue("Tarikh", TarikhEmrooz)
                cmSQL.Parameters.AddWithValue("Saat", tSaatVazeiat)
                cmSQL.Parameters.AddWithValue("TasviehWithTafkik", 1)
                cmSQL.Parameters.AddWithValue("CodeAmalyat", CodeAmalyat)
                cmSQL.Parameters("CodeAmalyat").Direction = ParameterDirection.Output

                cmSQL.ExecuteNonQuery()

                ccAmalyat = cmSQL.Parameters("CodeAmalyat").Value

                Dim MablaghAmalyat As Integer = dr("MablaghKolChek")
                tTarikhDP = dr("TarikhDP")
                tSh = dr("ccShomarehHesab")
                tShHEntry = tSh
                tEbtal = False
                tVazeiat = UD_Dll.Enums.DP_Vazeiat.DP_BedoneAmalyat
                tTarikhVazeiat = TarikhEmrooz

                '--------------------

                strSQL = "Treasury.spVosolFaktor_DaryaftNaghd_AmalyatVazeiat_Insert "

                cmSQL = New SqlCommand(strSQL, cnSQL)
                cmSQL.CommandType = CommandType.StoredProcedure
                cmSQL.Parameters.Clear()

                cmSQL.Parameters.AddWithValue("CodeAmalyat", ccAmalyat)
                cmSQL.Parameters.AddWithValue("ccShomarehHesab", dr("ccShomarehHesab"))
                cmSQL.Parameters.AddWithValue("CodeFard", dr("ccMamorPakhsh"))
                cmSQL.Parameters.AddWithValue("CodeGoroh", G_CodeGoroh)
                cmSQL.Parameters.AddWithValue("CodeKol", G_CodeKol)
                cmSQL.Parameters.AddWithValue("CodeMoeen", G_CodeMoeen)
                cmSQL.Parameters.AddWithValue("Tafsily1", CodeTafsily)
                cmSQL.Parameters.AddWithValue("Tafsily2", 0)
                cmSQL.Parameters.AddWithValue("Tafsily3", 0)
                cmSQL.Parameters.AddWithValue("sNoeSanad", UD_Dll.Enums.DP_AnvaeSanadDP.D_CheckBeSandogh)
                cmSQL.Parameters.AddWithValue("sVazeiat", UD_Dll.Enums.DP_Vazeiat.DP_BedoneAmalyat)
                cmSQL.Parameters.AddWithValue("TarikhVazeiat", dr("TarikhDP"))
                cmSQL.Parameters.AddWithValue("SaatVazeiat", tSaatVazeiat)

                cmSQL.ExecuteNonQuery()

                '-----------------------------------

                strSQL = "Treasury.spVosolFaktor_DaryaftNaghd_AmalyatFaktor_Insert "

                cmSQL = New SqlCommand(strSQL, cnSQL)
                cmSQL.CommandType = CommandType.StoredProcedure
                cmSQL.Parameters.Clear()

                cmSQL.Parameters.AddWithValue("CodeAmalyat", ccAmalyat)
                cmSQL.Parameters.AddWithValue("ccFaktorTitr", ccFaktorTitr)
                cmSQL.Parameters.AddWithValue("Mablagh", dr("PardakhtyInFaktor"))
                cmSQL.Parameters.AddWithValue("TasviehMoavagheh", 0)

                cmSQL.ExecuteNonQuery()

                cnSQL.Close()
                cmSQL = Nothing
                cnSQL = Nothing

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
            Finally
                Windows.Forms.Cursor.Current = Cursors.Default
            End Try
        Next
    End Sub
    Private Sub AddNewRecordTabdilBeTakhfif(ByVal ccFaktorTitr As Integer)

        Windows.Forms.Cursor.Current = Cursors.WaitCursor
        Dim cnSQL As SqlConnection : Dim cmSQL As SqlCommand
        Dim strSQL As String

        Dim ccMamorPakhsh As Integer = objTools.DLookup("ccMamorPakhsh", "tblFO_Faktor", "ccFaktorTitr = " & ccFaktorTitr)
        Dim MablaghTakhfif As Integer = objTools.DLookup("MablaghTakhfif", "Sales.TafkikJozeTasfiehSatr ", "ccDarkhastFaktor = " & ccFaktorTitr)
        Dim ShomarehFaktor As Integer = objTools.DLookup("FaktorShomareh", "tblFO_Faktor", "ccFaktorTitr = " & ccFaktorTitr)
        Dim ccMoshtary As Integer = objTools.DLookup("ccMoshtary", "tblFO_Faktor", "ccFaktorTitr = " & ccFaktorTitr)
        Dim CodeAmalyat As Integer = 0
        Dim ccAmalyat As Integer = 0

        If MablaghTakhfif = 0 Then
            Exit Sub
        End If

        Try
            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()

            Dim CodeTafsily As Long = objTools.DLookup("CodeTafsily1", "tblFO_Moshtary", "ccMoshtary =" & ccMoshtary)
            ' Get Goroh Kol Moeen Vaset Bank 
            Dim GKM As String = objTools.DLookup("Moeen15", "tblGL_Sherkat", "CodeSherkat = " & CodeSherkat)
            If GKM = "" Then
                Throw New Exception("به مشتری مورد نظر کد تفصیلی تخصیص داده نشده است.")
            End If
            Dim G_CodeGoroh As String = GKM.Substring(0, 1)
            Dim G_CodeKol As String = GKM.Substring(1, 2)
            Dim G_CodeMoeen As String = GKM.Substring(3, 2)

            strSQL = "Treasury.spVosolFaktor_TabdilBeTakhfif_Amalyat_Insert "

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            tSaatVazeiat = Format(TimeOfDay, "HH:mm:ss")

            cmSQL.Parameters.AddWithValue("CodeMahal", CodeMahalFaal)
            cmSQL.Parameters.AddWithValue("CodeDoreh", CodeDoreh)
            cmSQL.Parameters.AddWithValue("NoeAmalyat", UD_Dll.Enums.DP_NoeAmalyatDP.Takhfif)
            cmSQL.Parameters.AddWithValue("sNoeSanad", UD_Dll.Enums.DP_AnvaeSanadDP.F_TabdilBeTakhfif)
            cmSQL.Parameters.AddWithValue("TarikhDP", TarikhEmrooz)
            cmSQL.Parameters.AddWithValue("TarikhSanad", TarikhEmrooz)
            cmSQL.Parameters.AddWithValue("Mablagh", MablaghTakhfif)
            cmSQL.Parameters.AddWithValue("CodeGoroh", G_CodeGoroh)
            cmSQL.Parameters.AddWithValue("CodeKol", G_CodeKol)
            cmSQL.Parameters.AddWithValue("CodeMoeen", G_CodeMoeen)
            cmSQL.Parameters.AddWithValue("Tafsily1", CodeTafsily)
            cmSQL.Parameters.AddWithValue("Tafsily2", 0)
            cmSQL.Parameters.AddWithValue("Tafsily3", 0)
            cmSQL.Parameters.AddWithValue("sVazeiat", UD_Dll.Enums.DP_Vazeiat.DP_BedoneAmalyat)
            cmSQL.Parameters.AddWithValue("TarikhVazeiat", TarikhEmrooz)
            cmSQL.Parameters.AddWithValue("Tozihat", " بابت تبدیل به تخفیف فاکتور " & ShomarehFaktor & " ، ثبت شده در تسویه تفکیک ")
            cmSQL.Parameters.AddWithValue("UserName", UserName)
            cmSQL.Parameters.AddWithValue("Tarikh", TarikhEmrooz)
            cmSQL.Parameters.AddWithValue("Saat", tSaatVazeiat)
            cmSQL.Parameters.AddWithValue("TasviehWithTafkik", 1)
            cmSQL.Parameters.AddWithValue("CodeAmalyat", CodeAmalyat)
            cmSQL.Parameters("CodeAmalyat").Direction = ParameterDirection.Output

            cmSQL.ExecuteNonQuery()

            ccAmalyat = cmSQL.Parameters("CodeAmalyat").Value

            Dim MablaghAmalyat As Integer = MablaghTakhfif

            tTarikhDP = TarikhEmrooz
            'tSh = IIf(IsNothing(cmbShomarehHesab.SelectedValue), "", cmbShomarehHesab.SelectedValue)
            'tShHEntry = tSh
            tEbtal = False
            tVazeiat = UD_Dll.Enums.DP_Vazeiat.DP_BedoneAmalyat
            tTarikhVazeiat = TarikhEmrooz

            '--------------------

            strSQL = "Treasury.spVosolFaktor_TabdilBeTakhfif_AmalyatVazeiat_Insert "

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            cmSQL.Parameters.AddWithValue("CodeAmalyat", ccAmalyat)
            cmSQL.Parameters.AddWithValue("CodeGoroh", G_CodeGoroh)
            cmSQL.Parameters.AddWithValue("CodeKol", G_CodeKol)
            cmSQL.Parameters.AddWithValue("CodeMoeen", G_CodeMoeen)
            cmSQL.Parameters.AddWithValue("Tafsily1", CodeTafsily)
            cmSQL.Parameters.AddWithValue("Tafsily2", 0)
            cmSQL.Parameters.AddWithValue("Tafsily3", 0)
            cmSQL.Parameters.AddWithValue("sNoeSanad", UD_Dll.Enums.DP_AnvaeSanadDP.F_TabdilBeTakhfif)
            cmSQL.Parameters.AddWithValue("sVazeiat", UD_Dll.Enums.DP_Vazeiat.DP_BedoneAmalyat)
            cmSQL.Parameters.AddWithValue("TarikhVazeiat", TarikhEmrooz)
            cmSQL.Parameters.AddWithValue("SaatVazeiat", tSaatVazeiat)

            cmSQL.ExecuteNonQuery()

            '----------------------

            strSQL = "Treasury.spVosolFaktor_TabdilBeTakhfif_AmalyatFaktor_Insert "

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            cmSQL.Parameters.AddWithValue("CodeAmalyat", ccAmalyat)
            cmSQL.Parameters.AddWithValue("ccFaktorTitr", ccFaktorTitr)
            cmSQL.Parameters.AddWithValue("Mablagh", MablaghTakhfif)
            cmSQL.Parameters.AddWithValue("TasviehMoavagheh", 0)

            cmSQL.ExecuteNonQuery()

            cnSQL.Close()
            cmSQL = Nothing
            cnSQL = Nothing

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
        Finally
            Windows.Forms.Cursor.Current = Cursors.Default
        End Try
    End Sub
    Private Sub PrintTasviehTafkik(ByVal ccTafkikJozeTasfieh As String)
        Try
            Dim cnSQL As SqlConnection
            Dim strSQL As String
            Dim cmSQL As New SqlCommand

            strSQL = "Sales.spTafkikJozTasfieh_Print "

            Windows.Forms.Cursor.Current = Cursors.WaitCursor

            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            cmSQL.Parameters.AddWithValue("ccTafkikJozeTasfieh", ccTafkikJozeTasfieh)

            If dsForm.Tables.Contains("tblTafkikJozeTasfieh") Then
                dsForm.Tables.Remove("tblTafkikJozeTasfieh")
            End If

            Dim daSQL As SqlDataAdapter
            daSQL = New SqlDataAdapter(cmSQL)
            daSQL.Fill(dsForm, "tblTafkikJozeTasfieh")

            Dim rpt As New CrystalDecisions.CrystalReports.Engine.ReportDocument
            Dim rpttables As CrystalDecisions.CrystalReports.Engine.Tables
            Dim rptformula As CrystalDecisions.CrystalReports.Engine.FormulaFieldDefinitions
            Dim frm As New Forms_dll.frmGL_Gozaresh

            rpt.Load(rptPath & "\rptFO_GozareshTasviehTafkik.rpt")

            rpttables = rpt.Database.Tables
            rpttables.Item(0).SetDataSource(dsForm.Tables("tblTafkikJozeTasfieh"))

            rptformula = rpt.DataDefinition.FormulaFields
            With rptformula

                .Item("Group_Sanad").Text = "{mydata.ShomarehTafkik}"

                .Item("Sh").Text = "{mydata.ShomarehTafkik}"
                .Item("TarikhTafkik").Text = "{mydata.TarikhTafkik}"
                .Item("ccMamorPakhsh").Text = "{mydata.ccMamorPakhsh}"
                .Item("NameMamorPakhsh").Text = "{mydata.NameMamorPakhsh}"
                .Item("TedadKalaTafkik").Text = "{mydata.TedadKalaTafkik}"
                .Item("MablaghKolTafkik").Text = "{mydata.MablaghKolTafkik}"
                .Item("txtCodeVazeiat").Text = "{mydata.txtCodeVazeiat}"
                .Item("MablaghNaghdTitr").Text = "{mydata.MablaghNaghdTitr}"
                .Item("MablaghChekTitr").Text = "{mydata.MablaghChekTitr}"
                .Item("MablaghResidTitr").Text = "{mydata.MablaghResidTitr}"
                .Item("MablaghKartKhanTitr").Text = "{mydata.MablaghKartKhanTitr}"
                .Item("MablaghMarjoeeTitr").Text = "{mydata.MablaghMarjoeeTitr}"
                .Item("MablaghTakhfifTitr").Text = "{mydata.MablaghTakhfifTitr}"

                .Item("FaktorShomareh").Text = "{mydata.FaktorShomareh}"
                .Item("FaktorTarikh").Text = "{mydata.FaktorTarikh}"
                .Item("CodeMoshtary").Text = "{mydata.CodeMoshtary}"
                .Item("NameMoshtary").Text = "{mydata.NameMoshtary}"
                .Item("JamKol").Text = "{mydata.JamKol}"
                .Item("MablaghNaghdSatr").Text = "{mydata.MablaghNaghdSatr}"
                .Item("MablaghChekSatr").Text = "{mydata.MablaghChekSatr}"
                .Item("MablaghResidSatr").Text = "{mydata.MablaghResidSatr}"
                .Item("MablaghKartKhanSatr").Text = "{mydata.MablaghKartKhanSatr}"
                .Item("MablaghMarjoeeSatr").Text = "{mydata.MablaghMarjoeeSatr}"
                .Item("MablaghTakhfifSatr").Text = "{mydata.MablaghTakhfifSatr}"

                .Item("Title").Text = "'" & "تسویه برگه تفکیک" & "'"
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
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> Print Tasvieh Tafkik")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> Print Tasvieh Tafkik")
        End Try
    End Sub
#End Region
#Region "From Buttons"
    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Search()
    End Sub
    Private Sub btnSaveTitr_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveTitr.Click
        If Not objSec.HaveUserPermission(SN, UD_Dll.Security.ePermissions.Update) Then Exit Sub

        If IsValidMablaghTitr("All") = False Then
            Exit Sub
        End If

        If MsgBox("آیا مایلید اطلاعات ذخیره گردد ؟", MsgBoxStyle.OkCancel + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Question, "") = MsgBoxResult.Ok Then
            AddNewRecord()
            MsgBox("عملیات ثبت با موفقیت انجام شد .", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "")
            Mode = UD_Dll.Enums.GL_ModeForms.None
            SetTitrButton()
            Search()
        Else
            Exit Sub
        End If
    End Sub
    Private Sub btnSaveSatr_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveSatr.Click
        If Not objSec.HaveUserPermission(SN, UD_Dll.Security.ePermissions.UpdateSatr) Then Exit Sub

        If IsValidMablaghSatr("All") = False Then
            Exit Sub
        End If

        If MsgBox("آیا مایلید اطلاعات ذخیره گردد ؟", MsgBoxStyle.OkCancel + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Question, "") = MsgBoxResult.Ok Then
            AddNewRow()
            MsgBox("عملیات ثبت با موفقیت انجام شد .", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "")
            Mode = UD_Dll.Enums.GL_ModeForms.None
            SetSatrButton()

            Dim t As Integer = 0
            t = cmTitr.Position
            Search()
            cmTitr.Position = t
        Else
            Exit Sub
        End If
    End Sub
    Private Sub btnCancelTitr_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelTitr.Click
        Mode = UD_Dll.Enums.GL_ModeForms.None
        SetTitrButton()
    End Sub
    Private Sub btnCancelSatr_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelSatr.Click
        If Val(mskBestankarySatr.Text) <> Val(GridEXSatr.CurrentRow.Cells("MablaghBestankary").Text.Replace(",", "")) Then
            MsgBox("مبلغ بستانکاری تغییر نموده . در صورت صرفنطر، باید بستانکاری های ثبت شده و یا حذف شده به حالت ابتدایی بازگردد .", MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxSetForeground + MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "خطا")
            Exit Sub
        End If

        Mode = UD_Dll.Enums.GL_ModeForms.None
        SetSatrButton()
        Search()
    End Sub
    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub
    Private Sub btnTaeed_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTaeed.Click
        If Not objSec.HaveUserPermission(SN, UD_Dll.Security.ePermissions.Add) Then Exit Sub

        If dvTitr.Count = 0 Then
            Exit Sub
        End If

        For i As Integer = 0 To GridEXTitr.GetCheckedRows().Length - 1
            If IsValidTaeedNahaei("All", i) = False Then
                Exit Sub
            End If
        Next

        If GridEXTitr.GetCheckedRows().Length = 0 Then
            MsgBox("هیچ سطری جهت تایید انتخاب نشده است !", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "خطــا")
            Exit Sub
        End If
        GroupBox4.Visible = True
        btnTaeed.Enabled = False
    End Sub
    Private Sub btnExitTaeed_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExitTaeed.Click
        GroupBox4.Visible = False
        btnTaeed.Enabled = True
    End Sub
    Private Sub btnTaeedNahaei_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTaeedNahaei.Click

        'If (IsNothing(cmbShomarehHesab.SelectedValue)) Then
        '    ErrPro.SetError(Me.cmbShomarehHesab, "شماره حساب را وارد کنيد")
        '    MsgBox("شماره حساب را وارد کنيد", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
        '    cmbShomarehHesab.Focus()
        '    ErrPro.SetError(Me.cmbShomarehHesab, "")
        '    Exit Sub
        'End If

        If MsgBox("آیا به انتخاب / انتخاب های خود اطمینان دارید ؟", MsgBoxStyle.YesNo + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Question, "") = MsgBoxResult.No Then
            Exit Sub
        End If

        For i As Integer = 0 To GridEXTitr.GetCheckedRows().Length - 1

            If GridEXTitr.GetCheckedRows(i).Cells("CodeVazeiat").Value = 0 Then

                Dim cn As New SqlConnection
                Dim cm As New SqlCommand
                Dim da As New SqlDataAdapter
                Dim dt As New DataTable
                Dim p As New SqlParameter
                Dim strSQL As String = ""

                strSQL = "Global.spFaktorInTafkik "

                cn = New SqlConnection(ConnectionString)
                cn.Open()

                cm = New SqlCommand(strSQL, cn)
                cm.CommandType = CommandType.StoredProcedure
                cm.Parameters.Clear()

                cm.Parameters.AddWithValue("ccTafkik", GridEXTitr.GetCheckedRows(i).Cells("ccTafkikJoze").Value)

                da = New SqlDataAdapter(cm)

                da.Fill(dt)

                cm.Connection.Close()
                cn.Close()
                cm = Nothing
                da = Nothing


                For Each dr As DataRow In dt.Rows
                    AddNewRecordNaghd(dr("ccFaktorTitr"))
                    AddNewRecordChek(dr("ccFaktorTitr"))
                    AddNewRecordTabdilBeTakhfif(dr("ccFaktorTitr"))
                Next

                UpdateVazeiatTafkik(GridEXTitr.GetCheckedRows(i).Cells("ccTafkikJozeTasfieh").Value)

            ElseIf GridEXTitr.GetCheckedRows(i).Cells("CodeVazeiat").Value = 10 Then
                MsgBox("برگه تفکیک شماره " & Trim(GridEXTitr.GetCheckedRows(i).Cells("ShomarehTafkik").Value) & " تایید شده است .", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "")
                If i = GridEXTitr.GetCheckedRows.Length() - 1 Then
                    GroupBox4.Visible = False
                    btnTaeed.Enabled = True
                End If
            End If

        Next

        Search()

    End Sub
    Private Sub btnPrintTitr_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrintTitr.Click
        If Not objSec.HaveUserPermission(SN, UD_Dll.Security.ePermissions.Preview) Then Exit Sub
        Dim strTafkik As String = ""

        Try
            If dvTitr.Count > 0 Then
                For i As Integer = 0 To GridEXTitr.GetCheckedRows().Length - 1
                    strTafkik &= "," & GridEXTitr.GetCheckedRows(i).Cells("ccTafkikJozeTasfieh").Value
                Next
                If strTafkik <> "" Then
                    strTafkik &= ","
                End If

                Me.TopMost = False
                PrintTasviehTafkik(strTafkik)
                Me.TopMost = True
            End If
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->btnPrintM_Click")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->btnPrintM_Click")
        End Try
    End Sub
    Private Sub btnBestankarySatr_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBestankarySatr.Click
        Dim frm As New frmDP_Bestankary

        frm.B_ccTafkikJozeTasfiehSatr = Val(GridEXSatr.CurrentRow.Cells("ccTafkikJozeTasfiehSatr").Text.Replace(",", ""))
        frm.B_ccFaktor = Val(GridEXSatr.CurrentRow.Cells("ccFaktorTitr").Text.Replace(",", ""))
        frm.B_FaktorShomareh = GridEXSatr.CurrentRow.Cells("FaktorShomareh").Text
        frm.B_FaktorTarikh = GridEXSatr.CurrentRow.Cells("FaktorTarikh").Text
        frm.B_MablaghFaktor = Val(GridEXSatr.CurrentRow.Cells("JamKol").Text.Replace(",", ""))
        frm.B_PardakhtiFaktor = GridEXSatr.CurrentRow.Cells("JamPardakhti").Text
        frm.B_MandehFaktor = GridEXSatr.CurrentRow.Cells("MandehFaktor").Text
        frm.B_ccMoshtary = Val(GridEXSatr.CurrentRow.Cells("ccMoshtary").Text.Replace(",", ""))
        frm.B_NameMoshtary = GridEXSatr.CurrentRow.Cells("NameMoshtary").Text

        Me.Hide()
        frm.ShowDialog()
        Me.Show()

        mskBestankarySatr.Text = objTools.DSum("MablaghBestankary", "Sales.TafkikJozeTasfiehSatr", "ccTafkikJozeTasfiehSatr = " & Val(GridEXSatr.CurrentRow.Cells("ccTafkikJozeTasfiehSatr").Text.Replace(",", "")))
    End Sub
#End Region

    
    
    Private Sub mskNaghdSatr_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles mskNaghdSatr.KeyPress
        Try
            If (Asc(e.KeyChar()) < 48 Or Asc(e.KeyChar()) > 57) And (Asc(e.KeyChar()) <> 8) Then
                e.Handled = True
            End If

            If e.KeyChar = Chr(Keys.Space) Then

                Dim frm As New frmDP_DaryaftNaghd
                Me.Hide()

                frm.ccMoshtary = GridEXSatr.CurrentRow.Cells("ccMoshtary").Value
                frm.ccFaktorTitr = Val(GridEXSatr.CurrentRow.Cells("ccFaktorTitr").Text.Replace(",", ""))
                frm.ccMamorPakhsh = GridEXSatr.CurrentRow.Cells("ccMamorPakhsh").Value
                frm.MablaghFaktor = GridEXSatr.CurrentRow.Cells("JamKol").Value
                frm.ccTafkikJozeTasfiehSatr = GridEXSatr.CurrentRow.Cells("ccTafkikJozeTasfiehSatr").Value
                frm.MandehFaktor = GridEXSatr.CurrentRow.Cells("JamKol").Value - (CType(mskNaghdSatr.Text, Integer) + CType(mskResidSatr.Text, Integer) + CType(mskKartKhanSatr.Text, Integer) + CType(mskMarjoeeSatr.Text, Integer) + CType(mskTakhfifSatr.Text, Integer))

                frm.ShowDialog(Me)
                Me.Show()

            End If
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> mskNaghdSatr_KeyPress ")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> mskNaghdSatr_KeyPress ")
        End Try
    End Sub

    Private Sub mskNaghdSatr_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mskNaghdSatr.TextChanged
        If FlgSetForm = True Then
            mskNaghdSatr.Text = objTools.ConvertNulls(objTools.DSum("PardakhtyInFaktor", "Sales.TafkikJozeTasfiehSatrNaghd", "ccFaktor = " & Val(GridEXSatr.CurrentRow.Cells("ccFaktorTitr").Text.Replace(",", ""))), 0)
        End If
    End Sub
End Class


'' SP
'' Sales.spTafkikJozTasfieh_Search
'' Sales.spTafkikJozTasfiehSatr_Search
'' Sales.spTafkikJozTasfieh_Update
'' Sales.spTafkikJozTasfiehSatr_Update
'' Sales.spTafkikJozTasfieh_UpdateCodeVazeiat
'' Sales.spTasviehTafkik_SearchChekForFaktor
'' Sales.spTafkikJozTasfieh_Print
'' Treasury.spVosolFaktor_DaryaftNaghd_Amalyat_Insert
'' Treasury.spVosolFaktor_DaryaftNaghd_AmalyatVazeiat_Insert
'' Treasury.spVosolFaktor_DaryaftNaghd_AmalyatFaktor_Insert
'' Treasury.spVosolFaktor_DaryaftChek_Amalyat_Insert
'' Treasury.spVosolFaktor_DaryaftNaghd_AmalyatVazeiat_Insert
'' Treasury.spVosolFaktor_DaryaftNaghd_AmalyatFaktor_Insert
'' Global.spFaktorInTafkik

'' SP Daryaft Check
'' Global.spShomarehHesab_LoadCombo
'' Global.spBankSanad_LoadCombo
'' Sales.spTafkikJozTasfiehSatrChek_Search
'' Sales.spTafkikJozTasfiehSatrChek_Insert
'' Sales.spTafkikJozTasfiehSatrChek_Update
'' Sales.spTafkikJozTasfiehSatrChek_Delete

'' SP Bestankary
'' Sales.spTafkikJozTasfieh_SearchPardakhtyFromBestankary
'' Sales.spTafkikJozTasfieh_SearchBestankary
'' Sales.spTafkikJozTasfieh_InsertFromBestankary
'' Sales.spTafkikJozTasfieh_DeletePardakhtyFromBestankary

'' Report
'' rptFO_GozareshTasviehTafkik.rpt
