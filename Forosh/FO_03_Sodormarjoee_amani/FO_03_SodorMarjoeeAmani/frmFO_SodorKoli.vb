Public Class frmFO_SodorKoli
#Region "Variable AND Constant Declration"

    Dim AllowPishFaktorTakhfifDasty As Boolean = objTools.ConvertNulls(objTools.DLookup("AllowPishFaktorTakhfifDasty", "tblGL_SysConfig", ""), False)

    Dim cmTitr As CurrencyManager
    Dim cmSatr As CurrencyManager
    Dim cmForm As CurrencyManager
    Dim dvTitr, dvTitr_PishFaktor As DataView
    Dim tCodeCounter As Long
    Const FormTableName = "tblFO_ElamMarjoee"
    Dim ErrPro As New ErrorProvider
    Dim dsForm As New DataSet
    Dim dvForm, dvForm_PishFakotr As DataView
    Dim IsSabadKala As Boolean = False
    Private SN As Integer
    Dim flg_SearchPishFaktor As Boolean = False
    Dim txtCaption As String
    Private WithEvents BS As New UD_Dll.PassString
    Dim FirstInsert As Boolean = False

    Public ccMoshtary As Integer
    Dim sNoeMoshtary As Integer = 0
    Dim IsMalyatAvarezTakhfif As Boolean
    Dim tpos As Integer
    Dim ccKala As Integer

    Dim ccPishFaktorAmani_Vaset As Integer = 0
    Dim ccElamMarjoee_PishFaktor As Integer = 0
    Dim ccElamMarjoeeSatr_PishFaktor As Integer = 0
    Dim ccma As Integer = 0
    Dim Mode As Boolean = False  ' Mode  --> False : Save Titr \\ True : Inser Kala

#End Region
#Region "Form Event Code"
    Private Sub frmFO_SodorKoli_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Me.SkinEngine1.SkinFile = Environment.CurrentDirectory + "\s\" + "a (24)" + ".ssk"
        'Me.SkinEngine1.Active = True

        SetParameter()
        objTools.ChangeKbd(UD_Dll.mdlUtility.KBD.Farsi)
        SetForm()
        SearchTitr()
        LoadComboAnbar()
    End Sub
    Private Sub LoadComboAnbar()

        Dim Strsql As String
        Dim daSQL As SqlDataAdapter
        Try

            Strsql = "Select * From tblan_Anbar Where faal=1 order by NoeAnbar"
            daSQL = New SqlDataAdapter(Strsql, ConnectionString)
            daSQL.Fill(dsForm, "tbl_Anbar")
            Cmbanbar.DataSource = Nothing
            Cmbanbar.Items.Clear()
            Cmbanbar.DataSource = dsForm.Tables("tbl_Anbar").DefaultView
            Cmbanbar.DisplayMember = "NameAnbar"
            Cmbanbar.ValueMember = "CodeAnbar"
            daSQL = Nothing
            Cmbanbar.SelectedIndex = -1
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "Error IN DataBase ---->" & "LoadCombo")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, "Error IN ---->" & "LoadCombo")
        End Try

    End Sub
    Private Sub txtCodeMoshtary_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCodeMoshtary.KeyPress
        Try
            If (Asc(e.KeyChar()) < 48 Or Asc(e.KeyChar()) > 57) And (Asc(e.KeyChar()) <> 8) Then
                e.Handled = True
            End If
            If e.KeyChar = Chr(Keys.Space) Then
                Dim objMoshtary As New Forms_dll.frmFO_MoshtarySearch
                Dim StrSql As String = ""

                StrSql = "Select * from qryFO_Moshtary Where CodeMahal = " & CodeMahalFaal & " AND sVazeiat = " & UD_Dll.Enums.FO_VaziatMoshtary.Faal
                StrSql &= " AND Not exists (Select * From tblGL_SecurityData where namekarbar= '" & UserName & "' and CodeSubSystem = 614 and pk = qryFO_Moshtary.ccMoshtary) "
                StrSql &= " AND Not exists (Select * From tblGL_SecurityData where namekarbar= '" & UserName & "' and CodeSubSystem = 10000 and pk = qryFO_Moshtary.sNoeMoshtary) "
                StrSql &= " AND ccMoshtary IN (SELECT ccMoshtary FROM tblFO_PishFaktor where sVazeiat = 2002) "
                StrSql &= " ORDER BY sMantagheh,sMahaleh,NameMoshtary"

                objMoshtary.tCodeMoshtary = ""
                objMoshtary.tNameMoshtary = ""
                objMoshtary.tccMoshtary = ""

                If txtCodeMoshtary.Text.Length <> 0 Then
                    objMoshtary.tCodeMoshtary = txtCodeMoshtary.Text
                End If

                objMoshtary.MultiSelection = False
                SearchItem = "CodeMoshtary"
                objMoshtary.SetForm(StrSql)
                objMoshtary.ShowDialog()
                txtCodeMoshtary.Tag = objMoshtary.tccMoshtary
                txtCodeMoshtary.Text = objMoshtary.tCodeMoshtary
                lblNameMoshtaryTitr.Text = objMoshtary.tNameMoshtary
                ccMoshtary = IIf(txtCodeMoshtary.Tag.ToString = "", 0, txtCodeMoshtary.Tag)
                lblTedadPishFaktorAmani.Text = objTools.DCount("ccPishFaktorTitr", "tblFO_PishFaktor", "ccMoshtary = " & ccMoshtary & " AND sVazeiat = 2002")

                MultiSelection = False
            End If
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->txtCodeMoshtary_KeyPress")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->txtCodeMoshtary_KeyPress")
        End Try
    End Sub
    Private Sub txtCodeMoshtary_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCodeMoshtary.TextChanged
        Dim Criteria As String = ""

        Criteria = "CodeMahal=" & CodeMahalFaal & " AND sVazeiat = " & UD_Dll.Enums.FO_VaziatMoshtary.Faal
        Criteria &= " AND Not exists (Select * From tblGL_SecurityData where namekarbar= '" & UserName & "' and CodeSubSystem = 614 and pk = qryFO_Moshtary.ccMoshtary) "
        Criteria &= " AND ccMoshtary IN (SELECT ccMoshtary FROM tblFO_PishFaktor where sVazeiat = 2002) "
        Criteria &= " And CodeMoshtary = '" & IIf(IsNothing(Me.txtCodeMoshtary.Text), 0, Me.txtCodeMoshtary.Text) & "'"

        Me.lblNameMoshtaryTitr.Text = objTools.ConvertNulls(objTools.DLookup("NameMoshtary", "qryFO_Moshtary", Criteria), "")
        Me.txtCodeMoshtary.Tag = objTools.ConvertNulls(objTools.DLookup("ccMoshtary", "qryFO_Moshtary", Criteria), 0)
        If IsNumeric(Me.txtCodeMoshtary.Tag) Then
            Me.ccMoshtary = Me.txtCodeMoshtary.Tag
        Else
            Me.ccMoshtary = 0
        End If
        lblTedadPishFaktorAmani.Text = objTools.DCount("ccPishFaktorTitr", "tblFO_PishFaktor", "ccMoshtary = " & Me.txtCodeMoshtary.Tag & " AND sVazeiat = 2002")


    End Sub
    Private Sub txtCodeKala_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCodeKala.KeyPress
        Try
            If (Asc(e.KeyChar()) < 48 Or Asc(e.KeyChar()) > 57) And (Asc(e.KeyChar()) <> 8) Then
                e.Handled = True
            End If
            If e.KeyChar = Chr(Keys.Space) Then
                ccMoshtary = objTools.ConvertNulls(objTools.DLookup("ccmoshtary", "Sales.ElamMarjoee_PishFaktor", "ccElamMarjoee_PishFaktor=" & ccElamMarjoee_PishFaktor), 0)

                Dim objKala As New Forms_dll.frmAN_KalaSearch
                Dim strSQL As String

                strSQL = "SELECT CodeKala,NameKala,ccKala,txtsVahedeShomaresh,sVahedeShomaresh,NameBrand,Radif,0 AS IsSabadKala "
                strSQL &= " FROM qryAN_Kala WHERE Faal = 1 "
                strSQL &= " AND ccKala IN ("
                strSQL &= " SELECT ccKala FROM tblFO_PishFaktorSatr AS a WITH(NOLOCK) LEFT OUTER JOIN"
                strSQL &= " tblFO_PishFaktor AS b WITH(NOLOCK) ON a.ccPishFaktorTitr = b.ccPishFaktorTitr"
                strSQL &= " WHERE ccMoshtary = " & ccMoshtary & " And sVazeiat = 2002)"
                'strSQL &= " UNION ALL "
                'strSQL &= " SELECT CodeSabad,NameSabadKala ,ccSabadKala,'',0,'','', 1 AS IsSabadKala "
                'strSQL &= " FROM Sales.SabadKala WHERE Faal = 1"

                If txtCodeKala.Text.Length <> 0 Then
                    objKala.tcodeKala = txtCodeKala.Text
                End If

                MultiSelection = False
                SearchItem = "CodeKala"
                objKala.SetForm(strSQL)
                objKala.ShowDialog()
                IsSabadKala = objKala.tIsSabadKala
                Me.txtCodeKala.Tag = objKala.tccKala
                Me.txtCodeKala.Text = objKala.tcodeKala
                Me.lblBarCodeKala.Text = objTools.ConvertNulls(objTools.DLookup("BarCode", "tblAN_KalaBarcode", "ccKala = " & objKala.tccKala & " AND sNoeMoshtary = " & sNoeMoshtary & " AND Faal = 1"), "-----")
                Me.lblNameKala_CodeKala.Text = objKala.tNameKala

                MultiSelection = False
            End If
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->txtCodeMoshtaryS_KeyPress")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->txtCodeMoshtaryS_KeyPress")
        End Try
    End Sub
    Private Sub txtBarCodeKala_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtBarCodeKala.KeyPress
        Try
            If (Asc(e.KeyChar()) < 48 Or Asc(e.KeyChar()) > 57) And (Asc(e.KeyChar()) <> 8) Then
                e.Handled = True
            End If
            If e.KeyChar = Chr(Keys.Space) Then
                Dim objBarCodeKala As New Forms_dll.frmAN_KalaBarCodeSearch
                Dim strSQL As String

                strSQL = "SELECT a.ccKala, a.BarCode, NameKala, "
                strSQL &= " b.CodeKala, c.NameBrand, d.Sharh AS txtVahedeShomaresh "
                strSQL &= " From tblan_Kalabarcode AS a WITH(NOLOCK) LEFT OUTER JOIN "
                strSQL &= " tblAN_Kala AS b WITH(NOLOCK) ON a.ccKala = b.ccKala LEFT OUTER JOIN "
                strSQL &= " tblFO_Brand AS c WITH(NOLOCK) ON b.ccBrand = c.ccBrand LEFT OUTER JOIN "
                strSQL &= " tblGL_ShenasehOmomi AS d WITH(NOLOCK) ON b.sVahedeShomaresh = d.Code "
                strSQL &= " WHERE a.Faal = 1 And b.Faal = 1 "
                strSQL &= " AND a.sNoeMoshtary = " & sNoeMoshtary
                strSQL &= " AND a.ccKala IN (SELECT ccKala FROM tblAN_KalaGheymat WHERE ccKala = a.ccKala) "
                strSQL &= " AND NOT EXISTS (SELECT PK FROM tblGL_SecurityData WHERE NameKarbar = 'Administrator' AND CodeSubSystem = 642 AND PK = a.ccKala) "
                strSQL &= " AND a.ccKala IN ("
                strSQL &= " SELECT ccKala FROM tblFO_PishFaktorSatr AS a WITH(NOLOCK) LEFT OUTER JOIN"
                strSQL &= " tblFO_PishFaktor AS b WITH(NOLOCK) ON a.ccPishFaktorTitr = b.ccPishFaktorTitr"
                strSQL &= " WHERE ccMoshtary = " & ccMoshtary & " And sVazeiat = 2002)"

                If txtBarCodeKala.Text.Length <> 0 Then
                    objBarCodeKala.tBarCodeKala = txtBarCodeKala.Text
                End If

                MultiSelection = False
                SearchItem = "BarCode"
                objBarCodeKala.SetForm(strSQL)
                objBarCodeKala.ShowDialog()
                Me.txtCodeKala.Tag = objBarCodeKala.tccKala
                Me.txtCodeKala.Text = objBarCodeKala.tCodeKala
                Me.lblNameKala_BarCodeKala.Text = objBarCodeKala.tNameKala
                Me.txtBarCodeKala.Text = objBarCodeKala.tBarCodeKala
            End If
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->txtCodeMoshtaryS_KeyPress")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->txtCodeMoshtaryS_KeyPress")
        End Try
    End Sub
    Private Sub txtCodeKala_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCodeKala.TextChanged
        If flg_SearchPishFaktor = True Then
            SearchPishFaktor(0)
            flg_SearchPishFaktor = False
        End If
        If Trim(Me.txtCodeKala.Text) = "" Then Exit Sub
        Try
            Me.lblNameKala_CodeKala.Text = objTools.ConvertNulls(objTools.DLookup("NameKala", "tblAN_Kala", "CodeKala = " & Me.txtCodeKala.Text.Trim), "")
            Me.txtCodeKala.Tag = objTools.ConvertNulls(objTools.DLookup("ccKala", "tblAn_Kala", "CodeKala = " & Me.txtCodeKala.Text.Trim), 0)
            Me.lblBarCodeKala.Text = objTools.ConvertNulls(objTools.DLookup("BarCode", "tblAN_KalaBarcode", "ccKala = " & Me.txtCodeKala.Tag & " AND sNoeMoshtary = " & sNoeMoshtary & " AND Faal = 1"), "-----")
            If rbAutomatic.Checked AndAlso txtCodeKala.Tag <> 0 Then
                SearchPishFaktor(txtCodeKala.Tag)
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->txtCodeKala_TextChanged")
        End Try

    End Sub
    Private Sub txtBarCodeKala_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtBarCodeKala.TextChanged
        If flg_SearchPishFaktor = True Then
            SearchPishFaktor(0)
            flg_SearchPishFaktor = False
        End If

        If Trim(Me.txtBarCodeKala.Text) = "" Then Exit Sub

        Try
            Dim Mablagh As Double = 0

            Me.txtBarCodeKala.Tag = objTools.ConvertNulls(objTools.DLookup("ccKala", "tblAN_KalaBarcode", "BarCode = '" & Me.txtBarCodeKala.Text.Trim & "' AND sNoeMoshtary = " & sNoeMoshtary & " AND Faal = 1"), 0)
            Me.lblNameKala_BarCodeKala.Text = objTools.ConvertNulls(objTools.DLookup("NameKala", "tblAN_Kala", "ccKala = " & Me.txtBarCodeKala.Tag), "")

            Dim cm As New SqlCommand
            cm.CommandText = "Select ISNULL(dbo.GetMablaghFrosh(" & _
                txtBarCodeKala.Tag & "," & _
                "'" & TarikhEmrooz & "'," & _
                CodeMahalFaal & "," & _
                ccMoshtary & "," & _
                sNoeMoshtary & "),0)"

            cm.Connection = New SqlConnection(ConnectionString)
            cm.Connection.Open()
            Mablagh = cm.ExecuteScalar

            Me.lblFee.Text = Mablagh
            Me.lblCodeKala.Text = objTools.ConvertNulls(objTools.DLookup("CodeKala", "tblAN_Kala", "ccKala = " & Me.txtBarCodeKala.Tag), 0)


            If Mode = UD_Dll.Enums.GL_ModeForms.UpdateRecord Then Exit Sub
            If Trim(Me.txtBarCodeKala.Text) = "" Then Exit Sub

            Dim ccKala_BarCode As Integer = 0

            ccKala_BarCode = objTools.ConvertNulls(objTools.DLookup("ccKala", "tblAN_KalaBarcode", "BarCode = '" & Me.txtBarCodeKala.Text.Trim & "' AND sNoeMoshtary = " & sNoeMoshtary & " AND Faal = 1"), 0)

            If ccKala_BarCode <> 0 Then
                Me.lblCodeKala.Text = objTools.DLookup("CodeKala", "tblAN_Kala", "ccKala = " & ccKala_BarCode)
            End If

            If rbAutomatic.Checked AndAlso txtBarCodeKala.Tag <> 0 Then
                SearchPishFaktor(txtBarCodeKala.Tag)
            End If

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->txtCodeKala_TextChanged")
        End Try
    End Sub
    'Private Sub GridEXPishFaktor_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GridEXPishFaktor.Click
    '    If dvTitr_PishFaktor.Count > 0 Then
    '        lblTedadDarPishFaktor.Text = GridEXPishFaktor.CurrentRow.Cells("TedadDarPishFaktor").Text
    '        lblTedadFaktorShodehAzGhabl.Text = IIf(GridEXPishFaktor.CurrentRow.Cells("TedadFaktorShodehAzGhabl").Text = "", 0, GridEXPishFaktor.CurrentRow.Cells("TedadFaktorShodehAzGhabl").Text)
    '        lblTedadRezervShodeh.Text = IIf(GridEXPishFaktor.CurrentRow.Cells("TedadRezerv").Text = "", 0, GridEXPishFaktor.CurrentRow.Cells("TedadRezerv").Text)
    '        lblTedadMarjoeeShodeh.Text = IIf(GridEXPishFaktor.CurrentRow.Cells("TedadMarjoeeShodeh").Text = "", 0, GridEXPishFaktor.CurrentRow.Cells("TedadMarjoeeShodeh").Text)
    '  lblTedadGhabelFaktor.Text = GridEXPishFaktor.CurrentRow.Cells("TedadGhabelFaktor").Text
    '        lblFee.Text = GridEXPishFaktor.CurrentRow.Cells("Gheymat").Text
    '        txtTedadKala.Text = 0
    '    End If
    'End Sub
    Private Sub rbCodeKala_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbCodeKala.CheckedChanged
        If rbCodeKala.Checked Then
            grbKala.Visible = True
            grbBarCodeKala.Visible = False
            ClearForm(False, False)
        End If
    End Sub
    Private Sub rbBarCodeKala_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbBarCodeKala.CheckedChanged
        If rbBarCodeKala.Checked Then
            grbKala.Visible = False
            grbBarCodeKala.Visible = True
            ClearForm(True, False)
        End If
    End Sub
    Private Sub rbDasti_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbDasti.CheckedChanged
        If rbDasti.Checked = True Then
            SetForm_NoeVorod(False)
        End If
    End Sub
    Private Sub rbAutomatic_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbAutomatic.CheckedChanged
        If rbAutomatic.Checked = True Then
            SetForm_NoeVorod(True)
        End If
    End Sub
    Private Sub tsmiDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsmiDelete.Click
        If dvTitr.Count = 0 Then
            Exit Sub
        End If
        If MsgBox("آیا مایل به حذف این کالا هستید؟", vbYesNo) = MsgBoxResult.No Then
            Exit Sub
        Else
            Delete_PishFaktorAmaniVaset(False)
            Search()
        End If

    End Sub
#End Region
#Region "Global Form Code"
    Private Sub SetParameter()
        Dim commands As String = Microsoft.VisualBasic.Command()
        If commands.Length = 0 Then
            UserName = "administrator"
            UserPassWord = "66998833"
            NameMahalFaal = "تهران"
            CodeMahalFaal = "1"
            PersonelCode = "0"
            PersonelName = "administrator"
            CodeDoreh = "1396"
            txtCaption = "اعلام مرجوعی امانی"
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
    Private Sub SetForm()
        If Mode = False Then
            ccPishFaktorAmani_Vaset = 0
            ccMoshtary = 0
            sNoeMoshtary = 0
            rbCodeKala.Checked = True
            txtCodeMoshtary.Text = ""
            lblTedadPishFaktorAmani.Text = ""
            txtCodeMoshtary.Focus()
            mskAzTarikh.Text = TarikhEmrooz
            mskTaTarikh.Text = TarikhEmrooz
            cmbVazeiat.Items.Clear()
            cmbVazeiat.Items.Add("بدون وضعیت")
            cmbVazeiat.Items.Add("تایید شده")
            cmbVazeiat.SelectedIndex = 0
            Search()
            SearchPishFaktor(0)


            grbSatr.Enabled = False
            btnRemoveTitr.Enabled = False
            'btnClearForm.Enabled = False
            btnInsertKala.Enabled = True
            btnCancel.Enabled = False
            btnUpdateTitr.Enabled = True
            txtCodeMoshtary.Focus()

        ElseIf Mode = True Then
            rbCodeKala.Checked = True
            sNoeMoshtary = objTools.DLookup("sNoeMoshtary", "tblFO_Moshtary", "ccMoshtary = " & ccMoshtary)
            lblTedadPishFaktorAmani.Text = objTools.DCount("ccPishFaktorTitr", "tblFO_PishFaktor", "ccMoshtary = " & ccMoshtary & " AND sVazeiat = 2002")

            btnUpdateTitr.Enabled = True
            btnRemoveTitr.Enabled = True
            'btnClearForm.Enabled = True
            btnInsertKala.Enabled = False
            btnCancel.Enabled = True

            rbCodeKala.Checked = True
            rbBarCodeKala.Checked = False
            rbDasti.Checked = True
            rbAutomatic.Checked = False
            rbSoodi.Checked = False
            rbNozoli.Checked = False

        End If

        If rbCodeKala.Checked Then
            ClearForm(False, False)
        ElseIf rbBarCodeKala.Checked Then
            ClearForm(True, False)
        End If

        mskTarikhElamMarjoee.Text = TarikhEmrooz
     

    End Sub
    Private Sub ClearForm(ByVal Type As Boolean, ByVal TmpCode As Boolean)
        '' Type ---> False : CodeKala // True : BarCodeKala
        If TmpCode = False Then
            txtCodeKala.Text = ""

            txtCodeKala.Tag = 0
            lblBarCodeKala.Text = ""
            lblNameKala_CodeKala.Text = ""
            txtBarCodeKala.Text = ""
            txtBarCodeKala.Tag = 0
            lblCodeKala.Text = ""
            lblNameKala_BarCodeKala.Text = ""
            txtTedadKala.Text = 0
        End If
        If Type = False Then
            grbKala.Visible = True
            grbBarCodeKala.Visible = False
            txtCodeKala.Focus()
        Else
            grbKala.Visible = False
            grbBarCodeKala.Visible = True
            txtBarCodeKala.Focus()
        End If
    End Sub
    Private Sub Delete_PishFaktorAmaniVaset(ByVal Type As Boolean)
        Try
            '''' Type : False --> Delete Satr // True : Delete Titr
            Dim cnSQL As New SqlConnection
            Dim cmSQL As New SqlCommand
            Dim strSQL As String = ""
            Dim CodeCounter As Integer = 0

            If Type = False Then
                CodeCounter = Val(GridEXsatr.CurrentRow.Cells("ccElamMarjoeeSatr_PishFaktor").Text.Replace(",", ""))
                cnSQL = New SqlConnection(ConnectionString)
                cnSQL.Open()
                strSQL = "Sales.spElamMarjoeeAmani_InsertKoli_DeleteSatrKala "
                cmSQL = New SqlCommand(strSQL, cnSQL)
                cmSQL.CommandType = CommandType.StoredProcedure
                cmSQL.Parameters.Clear()
                cmSQL.Parameters.AddWithValue("Type", Type)
                cmSQL.Parameters.AddWithValue("CodeCounter", CodeCounter)
                cmSQL.ExecuteNonQuery()
                cmSQL = Nothing
                cnSQL.Close()
            ElseIf Type = True Then
                For i As Integer = 0 To GridEXtitr.GetCheckedRows.Length - 1
                    CodeCounter = Val(GridEXtitr.GetCheckedRows(i).Cells("ccElamMarjoee_PishFaktor").Value)
                    cnSQL = New SqlConnection(ConnectionString)
                    cnSQL.Open()
                    strSQL = "Sales.spElamMarjoeeAmani_InsertKoli_DeleteSatrKala "
                    cmSQL = New SqlCommand(strSQL, cnSQL)
                    cmSQL.CommandType = CommandType.StoredProcedure
                    cmSQL.Parameters.Clear()
                    cmSQL.Parameters.AddWithValue("Type", Type)
                    cmSQL.Parameters.AddWithValue("CodeCounter", CodeCounter)
                    cmSQL.ExecuteNonQuery()
                    cmSQL = Nothing
                    cnSQL.Close()
                Next
            End If
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> Delete_PishFaktorAmaniVaset ")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> Delete_PishFaktorAmaniVaset ")
        End Try

    End Sub
    Private Sub SearchPishFaktor(ByVal ccKala As Integer)

        Dim cnSQL As New SqlConnection
        Dim cmSQL As New SqlCommand
        Dim daSQL As New SqlDataAdapter
        Dim strSQL As String = ""
        If rbCodeKala.Checked Then
            ccKala = txtCodeKala.Tag
        ElseIf rbBarCodeKala.Checked Then
            ccKala = txtBarCodeKala.Tag
        End If
        ccMoshtary = objTools.ConvertNulls(objTools.DLookup("ccmoshtary", "sales.ElamMarjoee_PishFaktor", "ccElamMarjoee_PishFaktor=" & ccElamMarjoee_PishFaktor), 0)
        If dsForm.Tables.Contains("tbl_SearchPishFaktor") Then
            dsForm.Tables.Remove("tbl_SearchPishFaktor")
        End If
        If ccMoshtary = 0 Then
            Exit Sub
        End If

        Try
            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()

            strSQL = "Sales.spSodormarjoeeAmani_SearchPishFaktor"

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()
            cmSQL.Parameters.AddWithValue("CodeMahal", CodeMahalFaal)
            cmSQL.Parameters.AddWithValue("ccKala", ccKala)
            cmSQL.Parameters.AddWithValue("ccMoshtary", ccMoshtary)
            cmSQL.Parameters.AddWithValue("TarikhEmrooz", TarikhEmrooz)
            ''   cmSQL.Parameters.AddWithValue("ccElamMarjoee_PishFaktor", ccElamMarjoee_PishFaktor)

            daSQL = New SqlDataAdapter(cmSQL)
            daSQL.Fill(dsForm, "tbl_SearchPishFaktor")

            dvForm_PishFakotr = New DataView
            dvForm_PishFakotr = dsForm.Tables("tbl_SearchPishFaktor").DefaultView
            dvForm_PishFakotr.Sort = "ccPishFaktorTitr DESC"
            dvForm_PishFakotr.AllowDelete = False
            dvForm_PishFakotr.AllowEdit = False
            dvForm_PishFakotr.AllowNew = False

            cmSQL = Nothing : daSQL = Nothing

            dvTitr_PishFaktor = New DataView(dsForm.Tables("tbl_SearchPishFaktor"), "", "ccPishFaktorTitr DESC", DataViewRowState.CurrentRows)
            dvTitr_PishFaktor.AllowNew = False
            dvTitr_PishFaktor.AllowDelete = False
            dvTitr_PishFaktor.AllowEdit = False


            GridEXPishFaktor.DataSource = Nothing
            GridEXPishFaktor.DataSource = dvTitr_PishFaktor

            SetGridStyle_PishFaktor()

            cnSQL.Close()
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> SearchPishFaktor ")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> SearchPishFaktor ")
        End Try
    End Sub
    Private Sub SetGridStyle_PishFaktor()
        Try
            If dvTitr_PishFaktor.Count < 0 Then
                Exit Sub
            End If
            With GridEXPishFaktor
                .DataSource = Nothing
                .DataSource = dsForm.Tables("tbl_SearchPishFaktor").DefaultView
                .SetDataBinding(dsForm.Tables("tbl_SearchPishFaktor").DefaultView, "")
                .RetrieveStructure()
            End With

            For i As Integer = 0 To GridEXPishFaktor.CurrentTable.Columns.Count - 1
                GridEXPishFaktor.CurrentTable.Columns.Item(i).Visible = False
            Next

            GridEXPishFaktor.CurrentTable.Columns.Item("PishFaktorShomareh").Caption = "شماره پیش فاکتور"
            GridEXPishFaktor.CurrentTable.Columns.Item("PishFaktorShomareh").Visible = True
            GridEXPishFaktor.CurrentTable.Columns.Item("PishFaktorShomareh").Width = 100
            GridEXPishFaktor.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXPishFaktor.CurrentTable.Columns.Item("PishFaktorShomareh").Position = 0
            GridEXPishFaktor.CurrentTable.Columns.Item("PishFaktorShomareh").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXPishFaktor.CurrentTable.Columns.Item("PishFaktorTarikh").Caption = "تاریخ پیش فاکتـور"
            GridEXPishFaktor.CurrentTable.Columns.Item("PishFaktorTarikh").Visible = True
            GridEXPishFaktor.CurrentTable.Columns.Item("PishFaktorTarikh").Width = 110
            GridEXPishFaktor.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXPishFaktor.CurrentTable.Columns.Item("PishFaktorTarikh").Position = 1
            GridEXPishFaktor.CurrentTable.Columns.Item("PishFaktorTarikh").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center


            GridEXPishFaktor.CurrentTable.Columns.Item("CodeDoreh").Caption = "CodeDoreh"
            GridEXPishFaktor.CurrentTable.Columns.Item("CodeDoreh").Visible = False
            GridEXPishFaktor.CurrentTable.Columns.Item("CodeDoreh").Width = 0
            GridEXPishFaktor.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXPishFaktor.CurrentTable.Columns.Item("CodeDoreh").Position = 3
            GridEXPishFaktor.CurrentTable.Columns.Item("CodeDoreh").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXPishFaktor.CurrentTable.Columns.Item("ccPishFaktorTitr").Caption = "ccPishFaktorTitr"
            GridEXPishFaktor.CurrentTable.Columns.Item("ccPishFaktorTitr").Visible = False
            GridEXPishFaktor.CurrentTable.Columns.Item("ccPishFaktorTitr").Width = 0
            GridEXPishFaktor.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXPishFaktor.CurrentTable.Columns.Item("ccPishFaktorTitr").FormatString = "G"
            GridEXPishFaktor.CurrentTable.Columns.Item("ccPishFaktorTitr").Position = 4
            GridEXPishFaktor.CurrentTable.Columns.Item("ccPishFaktorTitr").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXPishFaktor.CurrentTable.Columns.Item("TedadDarPishFaktor").Caption = "تعداد در پیش فاکتور"
            GridEXPishFaktor.CurrentTable.Columns.Item("TedadDarPishFaktor").Visible = True
            GridEXPishFaktor.CurrentTable.Columns.Item("TedadDarPishFaktor").Width = 120
            GridEXPishFaktor.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXPishFaktor.CurrentTable.Columns.Item("TedadDarPishFaktor").Position = 5
            GridEXPishFaktor.CurrentTable.Columns.Item("TedadDarPishFaktor").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXPishFaktor.CurrentTable.Columns.Item("TedadFaktorShodehAzGhabl").Caption = "تعداد فاکتور شده"
            GridEXPishFaktor.CurrentTable.Columns.Item("TedadFaktorShodehAzGhabl").Visible = True
            GridEXPishFaktor.CurrentTable.Columns.Item("TedadFaktorShodehAzGhabl").Width = 100
            GridEXPishFaktor.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXPishFaktor.CurrentTable.Columns.Item("TedadFaktorShodehAzGhabl").Position = 6
            GridEXPishFaktor.CurrentTable.Columns.Item("TedadFaktorShodehAzGhabl").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXPishFaktor.CurrentTable.Columns.Item("TedadRezerv").Caption = "تعداد رزرو"
            GridEXPishFaktor.CurrentTable.Columns.Item("TedadRezerv").Visible = True
            GridEXPishFaktor.CurrentTable.Columns.Item("TedadRezerv").Width = 100
            GridEXPishFaktor.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXPishFaktor.CurrentTable.Columns.Item("TedadRezerv").Position = 7
            GridEXPishFaktor.CurrentTable.Columns.Item("TedadRezerv").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXPishFaktor.CurrentTable.Columns.Item("TedadMarjoeeShodeh").Caption = "تعداد مرجوع شده"
            GridEXPishFaktor.CurrentTable.Columns.Item("TedadMarjoeeShodeh").Visible = True
            GridEXPishFaktor.CurrentTable.Columns.Item("TedadMarjoeeShodeh").Width = 130
            GridEXPishFaktor.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXPishFaktor.CurrentTable.Columns.Item("TedadMarjoeeShodeh").Position = 8
            GridEXPishFaktor.CurrentTable.Columns.Item("TedadMarjoeeShodeh").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXPishFaktor.CurrentTable.Columns.Item("TedadGhabelMarjoee").Caption = "تعداد قابل مرجوع"
            GridEXPishFaktor.CurrentTable.Columns.Item("TedadGhabelMarjoee").Visible = True
            GridEXPishFaktor.CurrentTable.Columns.Item("TedadGhabelMarjoee").Width = 130
            GridEXPishFaktor.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXPishFaktor.CurrentTable.Columns.Item("TedadGhabelMarjoee").Position = 9
            GridEXPishFaktor.CurrentTable.Columns.Item("TedadGhabelMarjoee").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXPishFaktor.CurrentTable.Columns.Item("Gheymat").Caption = "قیمت"
            GridEXPishFaktor.CurrentTable.Columns.Item("Gheymat").Visible = True
            GridEXPishFaktor.CurrentTable.Columns.Item("Gheymat").Width = 100
            GridEXPishFaktor.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXPishFaktor.CurrentTable.Columns.Item("Gheymat").Position = 2
            GridEXPishFaktor.CurrentTable.Columns.Item("Gheymat").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            'GridEXPishFaktor.CurrentTable.Columns.Item("btkf").Caption = "قیمت میانگین"
            'GridEXPishFaktor.CurrentTable.Columns.Item("btkf").Visible = True
            'GridEXPishFaktor.CurrentTable.Columns.Item("btkf").Width = 100
            'GridEXPishFaktor.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            'GridEXPishFaktor.CurrentTable.Columns.Item("btkf").Position = 10
            'GridEXPishFaktor.CurrentTable.Columns.Item("btkf").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            For i As Integer = 0 To GridEXPishFaktor.RootTable.Columns.Count - 1
                If GridEXPishFaktor.RootTable.Columns(i).Type.IsValueType Then
                    GridEXPishFaktor.RootTable.Columns(i).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                    GridEXPishFaktor.RootTable.Columns(i).FormatMode = Janus.Windows.GridEX.FormatMode.UseIFormattable
                    GridEXPishFaktor.RootTable.Columns(i).FormatString = "###,###.##"
                    GridEXPishFaktor.RootTable.Columns(i).TotalFormatMode = Janus.Windows.GridEX.FormatMode.UseIFormattable
                    GridEXPishFaktor.RootTable.Columns(i).TotalFormatString = "###,###.##"
                End If
            Next

            GridEXPishFaktor.Visible = True
            GridEXPishFaktor.HeaderFormatStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center


        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> SetGridStyle_PishFaktor ")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> SetGridStyle_PishFaktor ")
        End Try

    End Sub
    Private Sub Search()
        Dim cnSQL As New SqlConnection
        Dim cmSQL As New SqlCommand
        Dim daSQL As New SqlDataAdapter
        Dim strSQL As String = ""

        If dsForm.Tables.Contains("tbl_Search") Then
            dsForm.Tables.Remove("tbl_Search")
        End If

        Try
            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()

            strSQL = "Sales.spSodorMarjoeeAmani_InsertKoli_Search "

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            cmSQL.Parameters.AddWithValue("ccElamMarjoee_PishFaktor", ccElamMarjoee_PishFaktor)

            daSQL = New SqlDataAdapter(cmSQL)
            daSQL.Fill(dsForm, "tbl_Search")

            dvForm = New DataView
            dvForm = dsForm.Tables("tbl_Search").DefaultView
            dvForm.Sort = "CodeKala ASC"
            dvForm.AllowDelete = False
            dvForm.AllowEdit = False
            dvForm.AllowNew = False

            cmSQL = Nothing : daSQL = Nothing

            dvTitr = New DataView(dsForm.Tables("tbl_Search"), "", "CodeKala ASC", DataViewRowState.CurrentRows)
            dvTitr.AllowNew = False
            dvTitr.AllowDelete = False
            dvTitr.AllowEdit = False


            GridEXsatr.DataSource = Nothing
            GridEXsatr.DataSource = dvTitr

            SetGridStyle()

            cnSQL.Close()
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> Search ")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> Search ")
        End Try
    End Sub
    Private Sub SetGridStyle()
        Try
            If dvTitr.Count <= 0 Then
                Exit Sub
            End If

            With GridEXsatr
                .DataSource = Nothing
                .DataSource = dsForm.Tables("tbl_Search").DefaultView
                .SetDataBinding(dsForm.Tables("tbl_Search").DefaultView, "")
                .RetrieveStructure()
            End With

            For i As Integer = 0 To GridEXsatr.CurrentTable.Columns.Count - 1
                GridEXsatr.CurrentTable.Columns.Item(i).Visible = False
            Next

            GridEXsatr.CurrentTable.Columns.Item("CodeKala").Caption = "کـد کـالا"
            GridEXsatr.CurrentTable.Columns.Item("CodeKala").Visible = True
            GridEXsatr.CurrentTable.Columns.Item("CodeKala").Width = 80
            GridEXsatr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXsatr.CurrentTable.Columns.Item("CodeKala").Position = 0
            GridEXsatr.CurrentTable.Columns.Item("CodeKala").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXsatr.CurrentTable.Columns.Item("NameKala").Caption = "نـام کـالا"
            GridEXsatr.CurrentTable.Columns.Item("NameKala").Visible = True
            GridEXsatr.CurrentTable.Columns.Item("NameKala").Width = 200
            GridEXsatr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXsatr.CurrentTable.Columns.Item("NameKala").Position = 1
            GridEXsatr.CurrentTable.Columns.Item("NameKala").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXsatr.CurrentTable.Columns.Item("Tedad").Caption = "تعــداد"
            GridEXsatr.CurrentTable.Columns.Item("Tedad").Visible = True
            GridEXsatr.CurrentTable.Columns.Item("Tedad").Width = 70
            GridEXsatr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOn
            GridEXsatr.CurrentTable.Columns.Item("Tedad").FormatString = "G"
            GridEXsatr.CurrentTable.Columns.Item("Tedad").Position = 2
            GridEXsatr.CurrentTable.Columns.Item("Tedad").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXsatr.CurrentTable.Columns.Item("Fee").Caption = "قیمـت"
            GridEXsatr.CurrentTable.Columns.Item("Fee").Visible = True
            GridEXsatr.CurrentTable.Columns.Item("Fee").Width = 70
            GridEXsatr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXsatr.CurrentTable.Columns.Item("Fee").FormatString = "G"
            GridEXsatr.CurrentTable.Columns.Item("Fee").Position = 3
            GridEXsatr.CurrentTable.Columns.Item("Fee").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXsatr.CurrentTable.Columns.Item("Mablagh").Caption = "مبلـــغ"
            GridEXsatr.CurrentTable.Columns.Item("Mablagh").Visible = True
            GridEXsatr.CurrentTable.Columns.Item("Mablagh").Width = 100
            GridEXsatr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXsatr.CurrentTable.Columns.Item("Mablagh").FormatString = "G"
            GridEXsatr.CurrentTable.Columns.Item("Mablagh").Position = 4
            GridEXsatr.CurrentTable.Columns.Item("Mablagh").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXsatr.CurrentTable.Columns.Item("PishFaktorShomareh").Caption = "ش پیش فاکتور"
            GridEXsatr.CurrentTable.Columns.Item("PishFaktorShomareh").Visible = True
            GridEXsatr.CurrentTable.Columns.Item("PishFaktorShomareh").Width = 110
            GridEXsatr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXsatr.CurrentTable.Columns.Item("PishFaktorShomareh").Position = 5
            GridEXsatr.CurrentTable.Columns.Item("PishFaktorShomareh").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXsatr.CurrentTable.Columns.Item("PishFaktorTarikh").Caption = "ت پیش فاکتور"
            GridEXsatr.CurrentTable.Columns.Item("PishFaktorTarikh").Visible = True
            GridEXsatr.CurrentTable.Columns.Item("PishFaktorTarikh").Width = 100
            GridEXsatr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXsatr.CurrentTable.Columns.Item("PishFaktorTarikh").Position = 6
            GridEXsatr.CurrentTable.Columns.Item("PishFaktorTarikh").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXsatr.CurrentTable.Columns.Item("NameForoshandeh").Caption = "نـام فـروشنـده"
            GridEXsatr.CurrentTable.Columns.Item("NameForoshandeh").Visible = True
            GridEXsatr.CurrentTable.Columns.Item("NameForoshandeh").Width = 200
            GridEXsatr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXsatr.CurrentTable.Columns.Item("NameForoshandeh").Position = 7
            GridEXsatr.CurrentTable.Columns.Item("NameForoshandeh").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXsatr.CurrentTable.Columns.Item("NameMoshtary").Caption = "نـام مشتری "
            GridEXsatr.CurrentTable.Columns.Item("NameMoshtary").Visible = True
            GridEXsatr.CurrentTable.Columns.Item("NameMoshtary").Width = 200
            GridEXsatr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXsatr.CurrentTable.Columns.Item("NameMoshtary").Position = 8
            GridEXsatr.CurrentTable.Columns.Item("NameMoshtary").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center


            GridEXsatr.CurrentTable.Columns.Item("ccElamMarjoee_PishFaktor").Caption = "ccElamMarjoee_PishFaktor"
            GridEXsatr.CurrentTable.Columns.Item("ccElamMarjoee_PishFaktor").Visible = False
            GridEXsatr.CurrentTable.Columns.Item("ccElamMarjoee_PishFaktor").Width = 0
            GridEXsatr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXsatr.CurrentTable.Columns.Item("ccElamMarjoee_PishFaktor").Position = 9
            GridEXsatr.CurrentTable.Columns.Item("ccElamMarjoee_PishFaktor").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXsatr.CurrentTable.Columns.Item("ccElamMarjoeeSatr_PishFaktor").Caption = "ccElamMarjoeeSatr_PishFaktor"
            GridEXsatr.CurrentTable.Columns.Item("ccElamMarjoeeSatr_PishFaktor").Visible = False
            GridEXsatr.CurrentTable.Columns.Item("ccElamMarjoeeSatr_PishFaktor").Width = 0
            GridEXsatr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXsatr.CurrentTable.Columns.Item("ccElamMarjoeeSatr_PishFaktor").Position = 10
            GridEXsatr.CurrentTable.Columns.Item("ccElamMarjoeeSatr_PishFaktor").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center


            For i As Integer = 0 To GridEXsatr.RootTable.Columns.Count - 1
                If GridEXsatr.RootTable.Columns(i).Type.IsValueType Then
                    GridEXsatr.RootTable.Columns(i).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                    GridEXsatr.RootTable.Columns(i).FormatMode = Janus.Windows.GridEX.FormatMode.UseIFormattable
                    GridEXsatr.RootTable.Columns(i).FormatString = "###,###.##"
                    GridEXsatr.RootTable.Columns(i).TotalFormatMode = Janus.Windows.GridEX.FormatMode.UseIFormattable
                    GridEXsatr.RootTable.Columns(i).TotalFormatString = "###,###.##"
                End If
            Next

            GridEXsatr.Visible = True
            GridEXsatr.HeaderFormatStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center


        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> SetGridStyle ")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> SetGridStyle ")
        End Try

    End Sub
    Private Function IsValidRow(ByVal chkField As String) As Boolean
        Try
            IsValidRow = False

            If rbCodeKala.Checked = True Then
                If chkField = "txtCodeKala" Or chkField = "All" Then
                    If txtCodeKala.Tag = 0 Then
                        ErrPro.SetError(txtCodeKala, "کد کالا را انتخاب کنید.")
                        MsgBox("کد کالا را انتخاب کنید.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
                        txtCodeKala.Focus()
                        Exit Function
                    End If
                    ErrPro.SetError(txtCodeKala, "")
                End If
            ElseIf rbBarCodeKala.Checked = True Then
                If chkField = "txtBarCodeKala" Or chkField = "All" Then
                    If txtBarCodeKala.Tag = 0 Then
                        ErrPro.SetError(txtBarCodeKala, "بار کد کالا را انتخاب کنید.")
                        MsgBox("بار کد کالا را انتخاب کنید.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
                        txtBarCodeKala.Focus()
                        Exit Function
                    End If
                    ErrPro.SetError(txtBarCodeKala, "")
                End If
            End If

            If rbDasti.Checked = True Then
                If chkField = "ccPishFaktorTitr" Or chkField = "All" Then
                    If (CDbl(GridEXPishFaktor.CurrentRow.Cells("TedadDarPishFaktor").Text.Trim) = 0) Or (GridEXPishFaktor.CurrentRow.Cells("TedadDarPishFaktor").Text).Trim = "" Then
                        MsgBox(" لطفاً یک پیش فاکتور انتخــاب نمایید .", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
                        Exit Function
                    End If
                End If

                If rbCodeKala.Checked Then
                    If chkField = "txtCodeKala" Or chkField = "All" Then
                        If objTools.ConvertNulls(objTools.DCount("cckala", "sales.ElamMarjoeesatr_PishFaktor", "ccelamMarjoee_pishfaktor = " & Val(GridEXtitr.CurrentRow.Cells("ccelamMarjoee_pishfaktor").Text.Replace(",", "")) & " AND ccKala = " & txtCodeKala.Tag & "and ccpishfaktor=" & Val(GridEXPishFaktor.CurrentRow.Cells("ccpishfaktortitr").Text.Replace(",", ""))), 0) <> 0 Then
                            ErrPro.SetError(txtCodeKala, "کالای وارد شده از پیش فاکتور انتخابی تکراری است .")
                            MsgBox(" کالای وارد شده از پیش فاکتور انتخابی تکراری است .", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
                            txtCodeKala.Focus()
                            Exit Function
                        End If
                        ErrPro.SetError(txtCodeKala, "")
                    End If
                ElseIf rbBarCodeKala.Checked Then
                    If chkField = "txtBarCodeKala" Or chkField = "All" Then
                        If objTools.ConvertNulls(objTools.DCount("cckala", "sales.ElamMarjoeesatr_PishFaktor", "ccelamMarjoee_pishfaktor = " & Val(GridEXtitr.CurrentRow.Cells("ccelamMarjoee_pishfaktor").Text.Replace(",", "")) & "AND  ccKala = " & txtCodeKala.Tag & "and ccpishfaktor=" & Val(GridEXPishFaktor.CurrentRow.Cells("ccpishfaktortitr").Text.Replace(",", ""))), 0) <> 0 Then
                            ErrPro.SetError(txtBarCodeKala, "کالای وارد شده از پیش فاکتور انتخابی تکراری است .")
                            MsgBox(" کالای وارد شده از پیش فاکتور انتخابی تکراری است .", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
                            txtBarCodeKala.Focus()
                            Exit Function
                        End If
                        ErrPro.SetError(txtBarCodeKala, "")
                    End If
                End If

            End If

            Dim TedadDarPishFaktor As Double = 0
            Dim PK_F As Double = 0
            Dim TedadDarFaktorSaderShodeh As Double = 0
            Dim TedadRezervShodeh As Double = 0
            Dim TedadMarjoee As Double = 0

            If rbDasti.Checked Then
                If rbCodeKala.Checked Then
                    TedadDarPishFaktor = objTools.DLookup("Tedad3", "tblFO_PishFaktorSatr", "ccPishFaktorTitr = " & Val(GridEXPishFaktor.CurrentRow.Cells("ccPishFaktorTitr").Text.Replace(",", "")) & " AND ccKala = " & txtCodeKala.Tag)
                    TedadDarFaktorSaderShodeh = TedadDarFaktor(Val(GridEXPishFaktor.CurrentRow.Cells("ccPishFaktorTitr").Text.Replace(",", "")), txtCodeKala.Tag)
                    TedadRezervShodeh = objTools.DLookup("Tedad", "tblFO_PishFaktorAmaniSatr_Vaset", "ccPishFaktorTitr = " & Val(GridEXPishFaktor.CurrentRow.Cells("ccPishFaktorTitr").Text.Replace(",", "")) & " AND ccKala = " & txtCodeKala.Tag)
                    TedadRezervShodeh += objTools.DLookup("Tedad", "Sales.ElamMarjoeeSatr_PishFaktor", "ccPishFaktor = " & Val(GridEXPishFaktor.CurrentRow.Cells("ccPishFaktorTitr").Text.Replace(",", "")) & " AND ccKala = " & txtCodeKala.Tag)
                    TedadMarjoee = TedadMarjoeeShodeh(Val(GridEXPishFaktor.CurrentRow.Cells("ccPishFaktorTitr").Text.Replace(",", "")), txtCodeKala.Tag)
                ElseIf rbBarCodeKala.Checked Then
                    TedadDarPishFaktor = objTools.DLookup("Tedad3", "tblFO_PishFaktorSatr", "ccPishFaktorTitr = " & Val(GridEXPishFaktor.CurrentRow.Cells("ccPishFaktorTitr").Text.Replace(",", "")) & " AND ccKala = " & txtBarCodeKala.Tag)
                    TedadDarFaktorSaderShodeh = TedadDarFaktor(Val(GridEXPishFaktor.CurrentRow.Cells("ccPishFaktorTitr").Text.Replace(",", "")), txtBarCodeKala.Tag)
                    TedadRezervShodeh = objTools.DLookup("Tedad", "tblFO_PishFaktorAmaniSatr_Vaset", "ccPishFaktorTitr = " & Val(GridEXPishFaktor.CurrentRow.Cells("ccPishFaktorTitr").Text.Replace(",", "")) & " AND ccKala = " & txtBarCodeKala.Tag)
                    TedadMarjoee = TedadMarjoeeShodeh(Val(GridEXPishFaktor.CurrentRow.Cells("ccPishFaktorTitr").Text.Replace(",", "")), txtBarCodeKala.Tag)
                End If
            End If

            If chkField = "txtTedadKala" Or chkField = "All" Then
                If (Me.txtTedadKala.Text = "") Then
                    ErrPro.SetError(Me.txtTedadKala, "تعداد کالا را وارد کنید.")
                    MsgBox("تعداد کالا را وارد کنید.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
                    txtTedadKala.Focus()
                    Exit Function
                ElseIf (CDbl(Me.txtTedadKala.Text) = 0) Then
                    ErrPro.SetError(Me.txtTedadKala, "تعداد کالا نمی تواند صفـر باشد !")
                    MsgBox("تعداد کالا نمی تواند صفـر باشد !", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
                    txtTedadKala.Focus()
                    Exit Function
                ElseIf rbDasti.Checked = True AndAlso (CDbl(Me.txtTedadKala.Text)) > (CDbl(GridEXPishFaktor.CurrentRow.Cells("TedadGhabelmarjoee").Text.Trim)) Then
                    ErrPro.SetError(txtTedadKala, "تعــداد نمی تواند بزرگتر از تعداد قابل فاکتـور باشد .")
                    MsgBox(" تعــداد نمی تواند بزرگتر از تعداد قابل فاکتـور باشد .", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
                    txtTedadKala.Focus()
                    Exit Function
                    'ElseIf rbDasti.Checked = True AndAlso (CDbl(Me.lblTedadGhabelFaktor.Text.Trim)) < (TedadDarPishFaktor - TedadDarFaktorSaderShodeh - TedadRezervShodeh - TedadMarjoee) Then
                    '    ErrPro.SetError(txtTedadKala, "تعــداد قابل فاکتـور تغییر کرده، لطفا مجددا بر روی دکمه « پیش فاکتور های شامل کالا » کلیک نمایید .")
                    '    MsgBox(" تعــداد قابل فاکتـور تغییر کرده، لطفا مجددا بر روی دکمه « پیش فاکتور های شامل کالا » کلیک نمایید .", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
                    '    txtTedadKala.Focus()
                    '    Exit Function
                End If
                ErrPro.SetError(Me.txtTedadKala, "")
            End If

            Return True
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->IsValidRow ")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->IsValidRow ")
        End Try
    End Function
    Private Function TedadDarFaktor(ByVal ccPishFaktorTitr As Double, ByVal ccKala As Integer) As Integer
        TedadDarFaktor = 0

        Dim cnSQL As New SqlConnection
        Dim cmSQL As New SqlCommand
        Dim strSQL As String = ""

        Try
            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()

            strSQL = "Sales.spSodorFaktorAmani_InsertKoli_CalcTedadDarFaktor "

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            cmSQL.Parameters.AddWithValue("ccPishFaktorTitr", ccPishFaktorTitr)
            cmSQL.Parameters.AddWithValue("ccKala", ccKala)
            cmSQL.Parameters.AddWithValue("TedadDarFaktor", TedadDarFaktor)
            cmSQL.Parameters("TedadDarFaktor").Direction = ParameterDirection.Output

            cmSQL.ExecuteNonQuery()

            TedadDarFaktor = cmSQL.Parameters("TedadDarFaktor").Value

            cnSQL.Close()
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> TedadDarFaktor ")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> TedadDarFaktor ")
        End Try
    End Function
    Private Function TedadMarjoeeShodeh(ByVal ccPishFaktorTitr As Double, ByVal ccKala As Integer) As Integer
        TedadMarjoeeShodeh = 0

        Dim cnSQL As New SqlConnection
        Dim cmSQL As New SqlCommand
        Dim strSQL As String = ""

        Try
            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()

            strSQL = "Sales.spSodorFaktorAmani_InsertKoli_CalcTedadMarjoeeShodeh "

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            cmSQL.Parameters.AddWithValue("ccPishFaktorTitr", ccPishFaktorTitr)
            cmSQL.Parameters.AddWithValue("ccKala", ccKala)
            cmSQL.Parameters.AddWithValue("TedadMarjoeeShodeh", TedadMarjoeeShodeh)
            cmSQL.Parameters("TedadMarjoeeShodeh").Direction = ParameterDirection.Output

            cmSQL.ExecuteNonQuery()

            TedadMarjoeeShodeh = cmSQL.Parameters("TedadMarjoeeShodeh").Value

            cnSQL.Close()
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> TedadMarjoeeShodeh ")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> TedadMarjoeeShodeh ")
        End Try
    End Function
    Private Function InsertTitr() As Boolean
        Dim cnSQL As New SqlConnection
        Dim cmSQL As New SqlCommand
        Dim strSQL As String = ""
        Try
            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()

            strSQL = "Sales.spSodorMarjoeeAmani_insertTitr"
            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()
            cmSQL.Parameters.AddWithValue("codemahal", CodeMahalFaal)
            cmSQL.Parameters.AddWithValue("codedoreh", CodeDoreh)
            cmSQL.Parameters.AddWithValue("ShomarehElamMarjoee_PishFaktor", txtShomarehMarjoee.Text)
            cmSQL.Parameters.AddWithValue("UserName", UserName)
            cmSQL.Parameters.AddWithValue("ccmoshtary", ccMoshtary)
            cmSQL.Parameters.AddWithValue("Tarikh", TarikhEmrooz)
            cmSQL.Parameters.AddWithValue("Saat", Format(TimeOfDay, "HH:mm:ss"))
            cmSQL.Parameters.AddWithValue("ccElamMarjoee_PishFaktor", ccElamMarjoee_PishFaktor)
            cmSQL.Parameters("ccElamMarjoee_PishFaktor").Direction = ParameterDirection.Output
            cmSQL.ExecuteNonQuery()

            ccElamMarjoee_PishFaktor = objTools.ConvertNulls(cmSQL.Parameters("ccElamMarjoee_PishFaktor").Value, 0)

            cmSQL = Nothing
            cnSQL.Close()
         
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> InsertTitr ")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> InsertTitr ")
        End Try
    End Function

    Private Function UpdateTitr() As Boolean
        Dim cnSQL As New SqlConnection
        Dim cmSQL As New SqlCommand
        Dim strSQL As String = ""
        Dim p As New SqlParameter


        Try
            strSQL = "Sales.spSodorMarjoeeAmani_UpdateTitr"
            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()
            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            p = New SqlParameter("ccmoshtary", SqlDbType.Int)
            p.Value = ccMoshtary
            cmSQL.Parameters.Add(p)

            p = New SqlParameter("ccElamMarjoee_PishFaktor", SqlDbType.Int)
            p.Value = ccElamMarjoee_PishFaktor
            cmSQL.Parameters.Add(p)

            cmSQL.ExecuteNonQuery()

            ccElamMarjoee_PishFaktor = objTools.ConvertNulls(cmSQL.Parameters("ccElamMarjoee_PishFaktor").Value, 0)

            cmSQL = Nothing
            cnSQL.Close()

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> InsertTitr ")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> InsertTitr ")
        End Try
    End Function

    Private Sub InsertSatr(ByVal ccPishFaktorTitr As Integer, ByVal Tedad As Integer, ByVal Gheymat As Integer)
        Dim cnSQL As New SqlConnection
        Dim cmSQL As New SqlCommand
        Dim strSQL As String = ""
        Dim ccKala As Integer = 0
        Dim pk_s As Integer = 0
        If rbCodeKala.Checked Then
            ccKala = txtCodeKala.Tag
        ElseIf rbBarCodeKala.Checked Then
            ccKala = txtBarCodeKala.Tag
        End If

        Try
            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()

            strSQL = "Sales.spSodormarjoeeAmani_InsertSatr"
            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            cmSQL.Parameters.AddWithValue("ccElamMarjoee_PishFaktor", Val(GridEXtitr.CurrentRow.Cells("ccElamMarjoee_PishFaktor").Text.Replace(",", "")))
            cmSQL.Parameters.AddWithValue("ccmoshtary", ccMoshtary)
            cmSQL.Parameters.AddWithValue("ccpishfaktor", ccPishFaktorTitr)
            cmSQL.Parameters.AddWithValue("Tarikh", TarikhEmrooz)
            cmSQL.Parameters.AddWithValue("ccKala", ccKala)
            cmSQL.Parameters.AddWithValue("Tedad", Tedad)
            cmSQL.Parameters.AddWithValue("Fee", Gheymat)
            cmSQL.Parameters.AddWithValue("Btkf", Val(mskFeeMiyangin.Text))
            cmSQL.Parameters.AddWithValue("ccElamMarjoee1_PishFaktor", pk_s)
            cmSQL.Parameters("ccElamMarjoee1_PishFaktor").Direction = ParameterDirection.Output

            cmSQL.ExecuteNonQuery()

            ccElamMarjoee_PishFaktor = objTools.ConvertNulls(cmSQL.Parameters("ccElamMarjoee_PishFaktor").Value, 0)
            pk_s = objTools.ConvertNulls(cmSQL.Parameters("ccElamMarjoee1_PishFaktor").Value, 0)
            cmSQL = Nothing
            cnSQL.Close()

            CalculateMalyatAvarez(pk_s)

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> InsertSatr_Dasti ")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> InsertSatr_Dasti ")
        End Try
    End Sub
    Private Sub CalculateMalyatAvarez(ByVal pk As Integer)
        Dim strSQL As String = ""
        Dim cn As New SqlConnection(ConnectionString)
        Dim da As SqlDataAdapter = Nothing
        Dim dt As New DataTable
        Dim cm As SqlCommand = Nothing
        Dim M As Double = 0
        Dim AvarezOneKala As Double = 0
        Dim MaliyatOneKala As Double = 0
        Dim ccpishfaktor As Integer = 0
        ''  Dim TarikhFaktor As String = objTools.DLookup("PishFaktorTarikh", "tblFO_PishFaktor", "ccPishFaktortitr = " & ccPishFaktor)

        strSQL = "Select ccElamMarjoeeSatr_PishFaktor , ccKala , Fee , Tedad  from Sales.ElamMarjoeeSatr_PishFaktor WHERE ccElamMarjoeeSatr_PishFaktor = " & pk
        ccpishfaktor = objTools.DLookup("ccpishfaktor", "Sales.ElamMarjoeeSatr_PishFaktor", "ccElamMarjoeeSatr_PishFaktor = " & pk)
        cn.Open()

        da = New SqlDataAdapter(strSQL, cn)
        Try

            da.Fill(dt)

            For Each dr As DataRow In dt.Rows

                AvarezOneKala = objTools.DLookup("(MablaghAvarez) / Tedad3", "tblFO_PishFaktorSatr", "ccPishFaktorTitr = " & ccpishfaktor & " AND ccKala = " & dr("ccKala"))
                MaliyatOneKala = objTools.DLookup("(MablaghMalyat) / Tedad3", "tblFO_PishFaktorSatr", "ccPishFaktorTitr = " & ccpishfaktor & " AND ccKala = " & dr("ccKala"))
                M = (dr("Tedad") * AvarezOneKala) + (dr("Tedad") * MaliyatOneKala)

                strSQL = "UPDATE Sales.ElamMarjoeeSatr_PishFaktor SET "
                strSQL &= " MablaghMaliatAvarez = " & Math.Round(M, 0)
                strSQL &= " WHERE ccElamMarjoeesatr_PishFaktor = " & pk
                strSQL &= " And ccKala In (Select ccKala From tblAN_Kala where MashmuleMaliyat = 1 OR MashmuleAvarez = 1)"
                cm = New SqlCommand(strSQL, cn)
                cm.ExecuteNonQuery()

                If objTools.DLookup("MkolTakhfifMalyatAvarez", "tblFO_PishFaktor", "ccPishFaktorTitr = " & ccPishFaktor) <> 0 Then
                    If IsMalyatAvarezTakhfif Then
                        strSQL = "UPDATE Sales.ElamMarjoeeSatr_PishFaktor SET "
                        strSQL &= " TakhfifMalyatAvarez = " & Math.Round(M, 0)
                        strSQL &= " WHERE ccElamMarjoeesatr_PishFaktor = " & pk
                        strSQL &= " And ccKala In (Select ccKala From tblAN_Kala where MashmuleMaliyat = 1 OR MashmuleAvarez = 1)"

                        cm = New SqlCommand(strSQL, cn)
                        cm.ExecuteNonQuery()
                    End If

                End If

            Next
        Catch ex As Exception
            MsgBox(ex.Message)

        End Try
    End Sub
    Private Sub InsertSatr_Automatic()
        Dim cnSQL As New SqlConnection
        Dim cmSQL As New SqlCommand
        Dim daSQL As New SqlDataAdapter
        Dim dv As New DataView
        Dim dr As DataRowView
        Dim strSQL As String = ""
        Dim Flag_End As Boolean = False
        Dim TedadAvalieh As Double = 0
        Dim TedadMandeh As Double = 0
        Dim TedadGhabelSabt As Double = 0
        Dim ccKala As Integer = 0

        If rbCodeKala.Checked Then
            ccKala = txtCodeKala.Tag
        ElseIf rbBarCodeKala.Checked Then
            ccKala = txtBarCodeKala.Tag
        End If

        If dsForm.Tables.Contains("tbl_SearchPishFaktorForInsertAutomatic") Then
            dsForm.Tables.Remove("tbl_SearchPishFaktorForInsertAutomatic")
        End If

        Try
            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()

            strSQL = "Sales.spSodorFaktorAmani_InsertKoli_SearchPishFaktor "

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            cmSQL.Parameters.AddWithValue("ccPishFaktorAmani_Vaset", ccPishFaktorAmani_Vaset)
            cmSQL.Parameters.AddWithValue("CodeMahal", CodeMahalFaal)
            cmSQL.Parameters.AddWithValue("ccKala", ccKala)
            cmSQL.Parameters.AddWithValue("ccMoshtary", ccMoshtary)
            cmSQL.Parameters.AddWithValue("TarikhEmrooz", TarikhEmrooz)

            daSQL = New SqlDataAdapter(cmSQL)
            daSQL.Fill(dsForm, "tbl_SearchPishFaktorForInsertAutomatic")

            dv = New DataView(dsForm.Tables("tbl_SearchPishFaktorForInsertAutomatic"))
            If rbNozoli.Checked = True Then
                dv.Sort = "PishFaktorShomareh DESC"
            ElseIf rbSoodi.Checked = True Then
                dv.Sort = "PishFaktorShomareh ASC"
            End If

            TedadAvalieh = txtTedadKala.Text
            TedadMandeh = txtTedadKala.Text

            If dv.Count = 0 Then
                MsgBox("هیچ پیش فاکتور مانده دار از این کالا یافت نشد .", MsgBoxStyle.Information + MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, "")
                Exit Sub
            End If

            For Each dr In dv
                If TedadMandeh <> 0 Then
                    If TedadMandeh < dr("TedadGhabelFaktor") Then
                        TedadGhabelSabt = TedadMandeh
                        TedadMandeh = 0
                    Else
                        TedadGhabelSabt = dr("TedadGhabelFaktor")
                        TedadMandeh = TedadMandeh - dr("TedadGhabelFaktor")
                    End If

                    InsertSatr(dr("ccPishFaktorTitr"), TedadGhabelSabt, dr("Gheymat"))
                End If
            Next

            If TedadMandeh > 0 Then
                MsgBox("تعداد " & TedadAvalieh - TedadMandeh & " عدد با موفقیت فاکتور گردید .", MsgBoxStyle.Information + MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, "")
                MsgBox("تعداد " & TedadMandeh & " عدد به علت اتمام پیش فاکتور مانده دار از این کالا ثبت نگردید .", MsgBoxStyle.Information + MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, "")
                Exit Sub
            End If

            cmSQL = Nothing : daSQL = Nothing
            cnSQL.Close()

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> InsertSatr_Automatic ")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> InsertSatr_Automatic ")
        End Try
    End Sub
    Private Sub SetForm_NoeVorod(ByVal Type As Boolean)
        ' -- Type --> False : Dasti // True : Automatic
        If Type = False Then
            rbNozoli.Checked = False
            rbSoodi.Checked = False

            btnShowPishFaktor.Enabled = True
            grbPishFaktor.Enabled = True
            grbSabtAzPishFaktor.Enabled = False
        ElseIf Type = True Then
            rbNozoli.Checked = True
            rbSoodi.Checked = False

            btnShowPishFaktor.Enabled = False
            grbPishFaktor.Enabled = False
            grbSabtAzPishFaktor.Enabled = True

            If rbCodeKala.Checked Then
                If txtCodeKala.Tag <> 0 Then
                    SearchPishFaktor(txtCodeKala.Tag)
                End If
            Else
                If txtBarCodeKala.Tag <> 0 Then
                    SearchPishFaktor(txtBarCodeKala.Tag)
                End If
            End If

            'lblTedadDarPishFaktor.Text = ""
            'lblTedadFaktorShodehAzGhabl.Text = ""
            'lblTedadRezervShodeh.Text = ""
            'lblTedadMarjoeeShodeh.Text = ""
            'lblTedadGhabelFaktor.Text = ""
            txtTedadKala.Text = 0

        End If

    End Sub
    

#End Region
#Region "From Buttons "
    Private Sub btnInsertKala_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnInsertKala.Click
        btnNewTitr.Enabled = True
        If lblNameMoshtaryTitr.Text = "" Then
            ErrPro.SetError(Me.txtCodeMoshtary, "مشتری را وارد کنید.")
            MsgBox("مشتری را وارد کنید.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
            Exit Sub
        End If
        ErrPro.SetError(Me.txtCodeMoshtary, "")

        If lblTedadPishFaktorAmani.Text.Trim = 0 Then
            ErrPro.SetError(Me.txtCodeMoshtary, "برای این مشتری پیش فاکتوری وجود ندارد .")
            MsgBox("برای این مشتری پیش فاکتوری وجود ندارد .", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
            Exit Sub
        End If
        ErrPro.SetError(Me.txtCodeMoshtary, "")

        ccMoshtary = ccMoshtary
        sNoeMoshtary = objTools.DLookup("sNoeMoshtary", "tblFO_Moshtary", "ccMoshtary = " & ccMoshtary)

        If ccPishFaktorAmani_Vaset = 0 Then

            Select Mode
                Case 0
                    InsertTitr()
                Case 1
                    UpdateTitr()
            End Select
        End If
        SearchTitr()
        SetForm()
        Paneltitr.Visible = False

    End Sub
    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        If ccPishFaktorAmani_Vaset <> 0 Then
            If dvTitr.Count <> 0 Then
                If MsgBox("در صورت خــروج و تاییــد نکردن مــوارد ذخیـره شده، هیچ فاکتوری ثبت نخواهد شده" & vbCrLf _
                         & " و تمامی مــوارد ذخیـره شده حــذف خواهنـد شد . آیا خــروج انجــام شــود ؟" & vbCrLf _
                         , MsgBoxStyle.Question + MsgBoxStyle.YesNo + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, "خـــروج") = MsgBoxResult.Yes Then
                    Delete_PishFaktorAmaniVaset(True)
                Else
                    Exit Sub
                End If
            End If
        End If

        Mode = False
        SetForm()
        Paneltitr.Visible = False
        btnNewTitr.Enabled = True
    End Sub
    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If rbDasti.Checked AndAlso GridEXPishFaktor.CurrentRow.Cells("tedaddarpishfaktor").Text = "" Then
            MsgBox("ابتدا باید یک پیش فاکتور انتخاب نمایید .", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information + MsgBoxStyle.Critical, "خطا")
            Exit Sub
        End If

        If Not IsValidRow("All") Then
            Exit Sub
        End If

        If ccElamMarjoee_PishFaktor = 0 Then
            InsertTitr()
        End If
        Dim PK As Integer = Val(GridEXPishFaktor.CurrentRow.Cells("ccPishFaktorTitr").Text.Replace(",", ""))
        If rbDasti.Checked = True Then
            If lblFee.Text.Replace(",", "") = "" Then
                MsgBox("برای این کالا در سیستم قیمت تعریف نشده است .", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information + MsgBoxStyle.Critical, "خطا")
                Exit Sub
            End If

            Dim Gheymat As Integer = lblFee.Text.Replace(",", "")
            InsertSatr(PK, Val(txtTedadKala.Text), Gheymat)

        ElseIf rbAutomatic.Checked = True Then
            InsertSatr_Automatic()
        End If

        ClearForm(False, True)
        Search()
    End Sub
    Private Sub btnShowPishFaktor_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If rbCodeKala.Checked Then
            SearchPishFaktor(txtCodeKala.Tag)
        Else
            SearchPishFaktor(txtBarCodeKala.Tag)
        End If

        flg_SearchPishFaktor = True
    End Sub
    Private Sub btnClearForm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        ClearForm(False, False)
    End Sub
    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        If ccPishFaktorAmani_Vaset <> 0 Then
            If dvTitr.Count <> 0 Then
                If MsgBox("در صورت خــروج و تاییــد نکردن مــوارد ذخیـره شده، هیچ فاکتـوری ثبت نخواهد شده" & vbCrLf _
                         & " و تمامی مــوارد ذخیـره شده حــذف خواهنـد گردید . آیا خــروج انجــام شــود ؟" & vbCrLf _
                         , MsgBoxStyle.Question + MsgBoxStyle.YesNo + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, "خـــروج") = MsgBoxResult.Yes Then
                    Delete_PishFaktorAmaniVaset(True)
                Else
                    Exit Sub
                End If
            End If
        End If

        Me.Close()
    End Sub
    Private Sub frmFO_SodorKoli_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If ccPishFaktorAmani_Vaset <> 0 Then
            If dvTitr.Count <> 0 Then
                Delete_PishFaktorAmaniVaset(True)
                MsgBox("به علت تاییـــد نشدن موارد ثبت شده، هیچ فاکتـوری ثبت نخواهد شده" & vbCrLf _
                         & " و تمامی مــوارد ذخیـره شده حــذف خواهنـد گردید !!!" & vbCrLf _
                         , MsgBoxStyle.Information + MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, "خـــروج")
            End If
        End If
    End Sub
#End Region
#Region "Stored Procedure"
    '' Sales.spSodorFaktorAmani_InsertKoli_DeletePishFakotrVasetTitrSatr
    '' Sales.spSodorFaktorAmani_InsertKoli_SearchPishFaktor 
    '' Sales.spSodorFaktorAmani_InsertKoli_Search
    '' Sales.spSodorFaktorAmani_InsertKoli_CalcTedadDarFaktor
    '' Sales.spSodorFaktorAmani_InsertKoli_InsertPishFaktorAmaniVasetTitr
    '' Sales.spSodorFaktorAmani_InsertKoli_InsertPishFaktorAmaniVasetSatr
    '' Sales.spSodorFaktorAmani_InsertKoli_SearchPishFaktor
    '' Sales.spSodorFaktorAmani_InsertKoli_Taeed_SearchPishFaktor
    '' Sales.spSodorFaktorAmani_InsertKoli_Taeed_CreateTitrFaktor
    '' Sales.spSodorFaktorAmani_InsertKoli_Taeed_SearchKalaInPishFaktor 
    '' Sales.spSodorFaktorAmani_InsertKoli_Taeed_InsertKalaInSatrFaktor
    '' Sales.spSodorFaktorAmani_InsertKoli_Taeed_UpdateTaeedFaktor
    '' Sales.spSodorFaktorAmani_InsertKoli_Taeed_UpdateVazeiatSanad
#End Region

    Private Sub btnShowPishFaktor_Click_1(sender As Object, e As EventArgs) Handles btnShowPishFaktor.Click
        If rbCodeKala.Checked Then
            SearchPishFaktor(txtCodeKala.Tag)
        Else
            SearchPishFaktor(txtBarCodeKala.Tag)
        End If
        lblFee.Text = ""
        flg_SearchPishFaktor = True
    End Sub

    Private Sub btnNewTitr_Click(sender As Object, e As EventArgs) Handles btnNewTitr.Click
        txtShomarehMarjoee.Text = objTools.ConvertNulls(objTools.DMax("ShomarehElamMarjoee_PishFaktor", "Sales.ElamMarjoee_PishFaktor", "CodeMahal = " & CodeMahalFaal & " AND CodeDoreh = " & CodeDoreh), 0) + 1
        'txtShomarehMarjoee.Text = objTools.ConvertNulls("ShomarehElamMarjoee_PishFaktor", "Sales.ElamMarjoee_PishFaktor where  CodeMahal = " & CodeMahalFaal & " AND CodeDoreh = " & CodeDoreh)
        txtCodeMoshtary.Enabled = True
        Paneltitr.Visible = True
        btnUpdateTitr.Enabled = False
        btnNewTitr.Enabled = False
        btnInsertKala.Enabled = True
        btnCancel.Enabled = True
        Mode = 0
        txtCodeMoshtary.Focus()
    End Sub
    Private Sub SearchTitr()
        Dim cnSQL As New SqlConnection
        Dim cmSQL As New SqlCommand
        Dim daSQL As New SqlDataAdapter
        Dim strSQL As String = ""
   
        If dsForm.Tables.Contains("tbl_Search") Then
            dsForm.Tables.Remove("tbl_Search")
        End If

        Try
            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()

            strSQL = "Sales.spElamMarjoee_PishFaktor_SearchTitr"

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            cmSQL.Parameters.AddWithValue("AzTarikh", IIf(mskAzTarikh.Text <> "", mskAzTarikh.Text, CodeDoreh & "0101"))
            cmSQL.Parameters.AddWithValue("TaTarikh", IIf(mskTaTarikh.Text <> "", mskTaTarikh.Text, TarikhEmrooz))
            cmSQL.Parameters.AddWithValue("CodeDoreh", CodeDoreh)
            cmSQL.Parameters.AddWithValue("CodeMahal", CodeMahalFaal)
            cmSQL.Parameters.AddWithValue("sVazeiat", cmbVazeiat.SelectedIndex)
            cmSQL.Parameters.AddWithValue("ShomarehElamMarjoee_PishFaktor", IIf(txtShomarehS.Text.Trim <> "", txtShomarehS.Text.Trim, 0))
            daSQL = New SqlDataAdapter(cmSQL)
            daSQL.Fill(dsForm, "tbl_Search")

            dvTitr = New DataView
            dvTitr = dsForm.Tables("tbl_Search").DefaultView
            dvTitr.Sort = "ShomarehElamMarjoee_PishFaktor desc"
            dvTitr.AllowDelete = False
            dvTitr.AllowEdit = False
            dvTitr.AllowNew = False

    Dim col As DataColumn
    '------------Adding Columns------------
            col = New DataColumn
            col.ColumnName = "Taeed"
            col.DataType = GetType(Boolean)
            col.DefaultValue = False
            dsForm.Tables("tbl_Search").Columns.Add(col)
    '-----------------------------------------

            cmSQL = Nothing : daSQL = Nothing

            dvTitr = New DataView(dsForm.Tables("tbl_Search"), "", "ShomarehElamMarjoee_PishFaktor desc", DataViewRowState.CurrentRows)
            dvTitr.AllowNew = False
            dvTitr.AllowDelete = False
            dvTitr.AllowEdit = False


            GridEXsatr.DataSource = Nothing
            GridEXsatr.DataSource = dvTitr

            SetGridStyleTitr()

            cnSQL.Close()
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> SearchTitr ")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> SearchTitr ")
        End Try
    End Sub

    Private Sub SetGridStyleTitr()
        Try
            With GridEXtitr
                .DataSource = Nothing
                .DataSource = dsForm.Tables("tbl_Search").DefaultView
                .SetDataBinding(dsForm.Tables("tbl_Search").DefaultView, "")
                .RetrieveStructure()
            End With

            For i As Integer = 0 To GridEXtitr.CurrentTable.Columns.Count - 1
                GridEXtitr.CurrentTable.Columns.Item(i).Visible = False
            Next

            GridEXtitr.CurrentTable.Columns.Item("Taeed").Caption = "انتخاب"
            GridEXtitr.CurrentTable.Columns.Item("Taeed").Visible = True
            GridEXtitr.CurrentTable.Columns.Item("Taeed").Width = 120
            GridEXtitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXtitr.CurrentTable.Columns.Item("Taeed").Position = 0
            GridEXtitr.CurrentTable.Columns.Item("Taeed").Selectable = True
            GridEXtitr.CurrentTable.Columns.Item("Taeed").ActAsSelector = True
            GridEXtitr.CurrentTable.Columns.Item("Taeed").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXtitr.CurrentTable.Columns.Item("CodeMoshtary").Caption = "کد مشتری"
            GridEXtitr.CurrentTable.Columns.Item("CodeMoshtary").Visible = True
            GridEXtitr.CurrentTable.Columns.Item("CodeMoshtary").Width = 100
            GridEXtitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXtitr.CurrentTable.Columns.Item("CodeMoshtary").Position = 2
            GridEXtitr.CurrentTable.Columns.Item("CodeMoshtary").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXtitr.CurrentTable.Columns.Item("NameMoshtary").Caption = "نام مشتری"
            GridEXtitr.CurrentTable.Columns.Item("NameMoshtary").Visible = True
            GridEXtitr.CurrentTable.Columns.Item("NameMoshtary").Width = 200
            GridEXtitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXtitr.CurrentTable.Columns.Item("NameMoshtary").Position = 2
            GridEXtitr.CurrentTable.Columns.Item("NameMoshtary").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXtitr.CurrentTable.Columns.Item("ShomarehElamMarjoee_PishFaktor").Caption = "شماره مرجوعی"
            GridEXtitr.CurrentTable.Columns.Item("ShomarehElamMarjoee_PishFaktor").Visible = True
            GridEXtitr.CurrentTable.Columns.Item("ShomarehElamMarjoee_PishFaktor").Width = 100
            GridEXtitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXtitr.CurrentTable.Columns.Item("ShomarehElamMarjoee_PishFaktor").Position = 1
            GridEXtitr.CurrentTable.Columns.Item("ShomarehElamMarjoee_PishFaktor").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXtitr.CurrentTable.Columns.Item("TarikhElamMarjoee_PishFaktorSlash").Caption = "تاریخ مرجوعی"
            GridEXtitr.CurrentTable.Columns.Item("TarikhElamMarjoee_PishFaktorSlash").Visible = True
            GridEXtitr.CurrentTable.Columns.Item("TarikhElamMarjoee_PishFaktorSlash").Width = 100
            GridEXtitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXtitr.CurrentTable.Columns.Item("TarikhElamMarjoee_PishFaktorSlash").Position = 2
            GridEXtitr.CurrentTable.Columns.Item("TarikhElamMarjoee_PishFaktorSlash").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXtitr.CurrentTable.Columns.Item("svazeiat").Caption = "وضعیت"
            GridEXtitr.CurrentTable.Columns.Item("svazeiat").Visible = True
            GridEXtitr.CurrentTable.Columns.Item("svazeiat").Width = 100
            GridEXtitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXtitr.CurrentTable.Columns.Item("svazeiat").Position = 3
            GridEXtitr.CurrentTable.Columns.Item("svazeiat").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            'GridEXtitr.CurrentTable.Columns.Item("NameMoshtary").Caption = "نام مشتـــری"
            'GridEXtitr.CurrentTable.Columns.Item("NameMoshtary").Visible = True
            'GridEXtitr.CurrentTable.Columns.Item("NameMoshtary").Width = 250
            'GridEXtitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            'GridEXtitr.CurrentTable.Columns.Item("NameMoshtary").Position = 5
            'GridEXtitr.CurrentTable.Columns.Item("NameMoshtary").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXtitr.CurrentTable.Columns.Item("ccElamMarjoee_PishFaktor").Caption = "ccElamMarjoee_PishFaktor"
            GridEXtitr.CurrentTable.Columns.Item("ccElamMarjoee_PishFaktor").Visible = False
            GridEXtitr.CurrentTable.Columns.Item("ccElamMarjoee_PishFaktor").Width = 0
            GridEXtitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXtitr.CurrentTable.Columns.Item("ccElamMarjoee_PishFaktor").Position = 7
            '     GridEXtitr.CurrentTable.Columns.Item("ccElamMarjoee_PishFaktor").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            'GridEXtitr.CurrentTable.Columns.Item("ccAnbar").Caption = "ccAnbar"
            'GridEXtitr.CurrentTable.Columns.Item("ccAnbar").Visible = False
            'GridEXtitr.CurrentTable.Columns.Item("ccAnbar").Width = 0
            'GridEXtitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            'GridEXtitr.CurrentTable.Columns.Item("ccAnbar").Position = 8
            'GridEXtitr.CurrentTable.Columns.Item("ccAnbar").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            For i As Integer = 0 To GridEXtitr.RootTable.Columns.Count - 1
                If GridEXtitr.RootTable.Columns(i).Type.IsValueType Then
                    GridEXtitr.RootTable.Columns(i).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                    GridEXtitr.RootTable.Columns(i).FormatMode = Janus.Windows.GridEX.FormatMode.UseIFormattable
                    GridEXtitr.RootTable.Columns(i).FormatString = "###,###.##"
                    GridEXtitr.RootTable.Columns(i).TotalFormatMode = Janus.Windows.GridEX.FormatMode.UseIFormattable
                    GridEXtitr.RootTable.Columns(i).TotalFormatString = "###,###.##"
                End If
            Next

            GridEXtitr.Visible = True
            GridEXtitr.HeaderFormatStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center


        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> SetGridStyleTitr ")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> SetGridStyleTitr ")
        End Try

    End Sub

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        SearchTitr()
        grbSatr.Enabled = False
        If dvTitr.Count <> 0 Then
            ccElamMarjoee_PishFaktor = Val(GridEXtitr.CurrentRow.Cells("ccElamMarjoee_PishFaktor").Text)
        End If
        ClearForm(True, False)
        SetForm()
        Search()
    End Sub

    Private Sub GridEXtitr_Click(sender As Object, e As EventArgs) Handles GridEXtitr.Click
        If GridEXtitr.SelectedItems.Count <> 0 Then
            Mode = True
            ccElamMarjoee_PishFaktor = GridEXtitr.CurrentRow.Cells("ccElamMarjoee_PishFaktor").Text
            If objTools.ConvertNulls(objTools.DLookup("svazeiat", "Sales.ElamMarjoee_PishFaktor", "ccElamMarjoee_PishFaktor=" & ccElamMarjoee_PishFaktor), 0) = 0 Then

                grbSatr.Enabled = True

            End If
            ClearForm(True, False)
            SetForm()
            Search()
        End If
    End Sub



    Private Sub btnRemoveTitr_Click(sender As Object, e As EventArgs) Handles btnRemoveTitr.Click
        If MsgBox("آیا مایل به حذف این مرجوعی هستید؟", vbYesNo, "حذف مرجوعی") = MsgBoxResult.No Then
            Exit Sub
        Else
            Dim CountRow As Byte
            For i As Integer = 0 To GridEXtitr.GetCheckedRows.Length - 1
                If GridEXtitr.GetCheckedRows(i).Cells("Taeed").Value = True Then
                    CountRow += 1
                End If
            Next

            If CountRow = 0 Then
                MsgBox("حداقل یک مرجوعی باید انتخاب شود .", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, "")
                Exit Sub
            End If

            If objTools.ConvertNulls(objTools.DLookup("svazeiat", "Sales.ElamMarjoee_PishFaktor", "ccElamMarjoee_PishFaktor=" & ccElamMarjoee_PishFaktor), 0) = 0 Then
                Delete_PishFaktorAmaniVaset(True)
                SearchTitr()
                Search()
            Else
                MsgBox("این رکورد تایید شده است امکان حذف وجود ندارد.", vbOKOnly, "حذف")
                Exit Sub
            End If
        End If
        GridEXPishFaktor.DataSource = Nothing
    End Sub

    Private Sub btnUpdateTitr_Click(sender As Object, e As EventArgs) Handles btnUpdateTitr.Click
        Dim svazeiat As Integer = objTools.ConvertNulls(objTools.DLookup("svazeiat", "Sales.ElamMarjoee_PishFaktor", "ccElamMarjoee_PishFaktor=" & ccElamMarjoee_PishFaktor), 0)
        If svazeiat = 1 Then
            MsgBox("این رکورد تایید شده است اجازه ویرایش آن را ندارید.", vbOKOnly)
            Exit Sub
        End If
        ccElamMarjoee_PishFaktor = GridEXtitr.CurrentRow.Cells("ccElamMarjoee_PishFaktor").Text
        txtCodeMoshtary.Enabled = True
        Paneltitr.Visible = True
        btnNewTitr.Enabled = False
        btnInsertKala.Enabled = True
        btnCancel.Enabled = True
        Mode = 1
    End Sub

    Private Sub BtnTaeed_Click(sender As Object, e As EventArgs) Handles BtnTaeed.Click
        Dim CountRow As Integer = 0
        Dim CountTaeed As Integer = 0

        For i As Integer = 0 To GridEXtitr.GetCheckedRows.Length - 1
            If GridEXtitr.GetCheckedRows(i).Cells("Taeed").Value = True Then
                CountRow += 1
            End If
        Next

        If CountRow = 0 Then
            MsgBox("حداقل یک مرجوعی باید انتخاب شود .", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, "")
            Exit Sub
        End If

        If Cmbanbar.SelectedIndex < 0 Then
            MsgBox("انبار  باید انتخاب شود .", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, "")
            Exit Sub
        End If

        If MsgBox("آیا مرجوعی های انتخاب شده تایید گردند ؟", MsgBoxStyle.Question + MsgBoxStyle.YesNo + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, "تایید") = MsgBoxResult.Yes Then
            For i As Integer = 0 To GridEXtitr.GetCheckedRows.Length - 1
                If objTools.DLookup("sVazeiat", "Sales.ElamMarjoee_PishFaktor", "ccElamMarjoee_PishFaktor = " & Val(GridEXtitr.GetCheckedRows(i).Cells("ccElamMarjoee_PishFaktor").Text.Replace(",", ""))) = 1 Then
                    MsgBox("مرجوعی شماره " & Val(GridEXtitr.GetCheckedRows(i).Cells("ShomarehElamMarjoee_PishFaktor").Text.Replace(",", "")) & " تایید شده است ، امکان تایید مجدد آن وجود ندارد .", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, "")
                Else
                    If objTools.DCount("ccKala", "Sales.ElamMarjoeeSatr_PishFaktor", "ccElamMarjoee_PishFaktor = " & Val(GridEXtitr.GetCheckedRows(i).Cells("ccElamMarjoee_PishFaktor").Text.Replace(",", ""))) = 0 Then
                        MsgBox("مرجوعی شماره " & Val(GridEXtitr.GetCheckedRows(i).Cells("ShomarehElamMarjoee_PishFaktor").Text.Replace(",", "")) & " به علت نداشتن سطر کالا تایید نمی شود .", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, "")
                    Else
                        objTools.DUpdate("sVazeiat", "Sales.ElamMarjoee_PishFaktor", "1", "ccElamMarjoee_PishFaktor = " & Val(GridEXtitr.GetCheckedRows(i).Cells("ccElamMarjoee_PishFaktor").Text.Replace(",", "")))
                        objTools.DUpdate("ccAnbar", "Sales.ElamMarjoee_PishFaktor", Cmbanbar.SelectedValue, "ccElamMarjoee_PishFaktor = " & Val(GridEXtitr.GetCheckedRows(i).Cells("ccElamMarjoee_PishFaktor").Text.Replace(",", "")))
                        CountTaeed += 1
                    End If
                End If
            Next

            If CountTaeed > 0 Then
                MsgBox("تعداد " & CountTaeed & "عدد از مرجوعی های انتخاب شده تایید گردید .", MsgBoxStyle.Information + MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, "")
                SearchTitr()
            End If
        End If
    End Sub

    Private Sub BtnPrint_Click(sender As Object, e As EventArgs) Handles BtnPrint.Click
        'If Not objSec.HaveUserPermission(SN, UD_Dll.Security.ePermissions.Preview) Then Exit Sub
        Try
            If dvTitr.Count > 0 Then
                Me.TopMost = False
                PrintMarjoee_PishFaktor()
                Me.TopMost = True
            End If
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->btnPrintM_Click")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->btnPrintM_Click")
        End Try
    End Sub


    Private Sub PrintMarjoee_PishFaktor()
        Try
            Dim cnSQL As SqlConnection
            Dim strSQL As String
            Dim cmSQL As New SqlCommand
            Dim p As New SqlParameter

            strSQL = "[Sales].[spElamMarjoee_PishFaktor_Print]"

            Windows.Forms.Cursor.Current = Cursors.WaitCursor
            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()
            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            p = New SqlParameter("ccElamMarjoee_PishFaktor", SqlDbType.Int)
            p.Value = Val(GridEXTitr.CurrentRow.Cells("ccElamMarjoee_PishFaktor").Text.Replace(",", ""))
            cmSQL.Parameters.Add(p)

            If dsForm.Tables.Contains("qryFO_ElamMarjoee_PishFaktor") Then
                dsForm.Tables.Remove("qryFO_ElamMarjoee_PishFaktor")
            End If

            Dim daSQL As SqlDataAdapter
            daSQL = New SqlDataAdapter(cmSQL)
            daSQL.Fill(dsForm, "qryFO_ElamMarjoee_PishFaktor")

            Dim rpt As New CrystalDecisions.CrystalReports.Engine.ReportDocument
            Dim rpttables As CrystalDecisions.CrystalReports.Engine.Tables
            Dim rptformula As CrystalDecisions.CrystalReports.Engine.FormulaFieldDefinitions
            Dim frm As New Forms_dll.frmGL_Gozaresh

            Dim OptionalPrintInPishFaktor As Boolean = False
            OptionalPrintInPishFaktor = objTools.ConvertNulls(objTools.DLookup("OptionalPrintInPishFaktor", "tblGL_SysConfig", "CodeMahal = " & CodeMahalFaal), False)


            rpt.Load(rptPath & "\rptFO_GozareshElamMarjoee_PishFaktor.rpt")

            'If OptionalPrintInPishFaktor Then
            '    Dim ReportName As String = ""
            '    ReportName = objTools.ConvertNulls(objTools.DLookup("LTRIM(RTRIM(rptPrintOptionalInPishFaktor))", "tblGL_Sherkat", "CodeSherkat = " & CodeSherkat), "")
            '    rpt.Load(rptPath & "\" & ReportName)
            'ElseIf ObjCode.CheckCompany = 4 Then
            '    rpt.Load(rptPath & "\rptFO_GozareshPishFaktor_Lux.rpt")

            'ElseIf ObjCode.CheckCompany = 7 Then
            '    rpt.Load(rptPath & "\rptFO_GozareshPishFaktor_KamardGostar.rpt")
            'Else
            '    rpt.Load(rptPath & "\rptFO_GozareshPishFaktor.rpt")
            'End If


            rpttables = rpt.Database.Tables
            rpttables.Item(0).SetDataSource(dsForm.Tables("qryFO_ElamMarjoee_PishFaktor"))

            rptformula = rpt.DataDefinition.FormulaFields
            With rptformula

                If ObjCode.CheckCompany <> 7 Then
                    .Item("DarsadTakhfif").Text = "{mydata.DarsadTakhfif}"
                    '.Item("jamtakhfif").Text = "{mydata.jamtakhfif}"
                    .Item("Tozihat").Text = "{mydata.Tozihat}"
                End If
                If OptionalPrintInPishFaktor Then
                    .Item("Moshtary").Text = "{mydata.NameMoshtary}"
                    .Item("Ostan").Text = "{mydata.Ostan}"
                    .Item("NameTablo").Text = "{mydata.NameTablo}"
                    .Item("MoshtaryMobile").Text = "{mydata.MoshtaryMobile}"
                    .Item("txtVahedShomaresh").Text = "{mydata.txtVahed}"
                    .Item("mkol").Text = "{mydata.mkol3}"
                End If
                .Item("Moshtary").Text = "{mydata.NameMoshtary}"
                .Item("Group_Sanad").Text = "{mydata.ccElamMarjoee_PishFaktor}"
                .Item("Sh").Text = "{mydata.ShomarehElamMarjoee_PishFaktor}"
                .Item("Tarikh").Text = "{mydata.TarikhElamMarjoee_PishFaktorSlash}"
                .Item("Bazaryab").Text = "{mydata.NameForoshandeh}"
                .Item("CodeForoshandeh").Text = "{mydata.CodeForoshandeh}"
                .Item("Moshtary").Text = "{mydata.NameMoshtary}"
                .Item("CodeMoshtary").Text = "{mydata.CodeMoshtary}"
                .Item("Telephone").Text = "{mydata.Telephone}"
                .Item("AddressMoshtary").Text = "trim({mydata.Address})"
                .Item("NameKala").Text = "trim({mydata.NameKala})"
                .Item("Tedad").Text = "{mydata.Tedad}"
                .Item("Tedad3").Text = "{mydata.Tedad3}"
                .Item("TedadBasteh").Text = "{mydata.TedadBasteh}"
                .Item("TedadKarton").Text = "{mydata.TedadKarton}"
                .Item("Fee").Text = "{mydata.fee}"
                .Item("MKOL3").Text = "{mydata.MKOL3}"
                '  .Item("mkol").Text = "{mydata.mkol}"
                .Item("Takhfif").Text = "{mydata.Takhfif}"
                .Item("Codekala").Text = "{mydata.Codekala}"
                .Item("txtNoePardakht").Text = "{mydata.txtNoePardakht}"
                '  .Item("JamTakhfif").Text = "{mydata.JamTakhfifKala}"
                .Item("MalyatAvarez").Text = "{mydata.JamMalyatAvarez}"
                .Item("MablaghAvarez").Text = "{mydata.MablaghAvarez}"
                .Item("MablaghMalyat").Text = "{mydata.MablaghMalyat}"
                .Item("MablaghMaliatAvarez").Text = "{mydata.MablaghMaliatAvarez}"
                .Item("UserName").Text = "{mydata.UserName}"
                .Item("Tozihat").Text = "{mydata.Tozihat}"
                .Item("Title").Text = "'" & "اعلام مرجوعی پیش فاکتور امانی" & "'"
                .Item("Title2").Text = "'" & NameSherkat & "'"
                .Item("Title3").Text = "'" & NameMahalFaal & "'"
                .Item("KarbarGozaresh").Text = "'" & PersonelName & "'"
                .Item("TarikhGozaresh").Text = "'" & objTarikh.SetDateSlash(TarikhEmrooz) & "'"
                .Item("SaatGozaresh").Text = "'" & Format(TimeOfDay, "HH:mm:ss") & "'"
                'If ObjCode.CheckCompany = 4 And Not OptionalPrintInPishFaktor Then
                '    .Item("VAhed").Text = "{mydata.txtVahed}"
                'End If
                'If ObjCode.CheckCompany = 4 Then
                '    .Item("MablaghMasrafKonandeh").Text = "{mydata.MablaghMasrafKonandeh}"
                'End If
            End With
            rpt.Refresh()

            frm.Text = txtCaption

            With frm.CRV
                .ReportSource = rpt
                .DisplayGroupTree = False
                .ShowGroupTreeButton = False
                .Zoom(75)
                '    If Not objSec.HaveUserPermission(SN, UD_Dll.Security.ePermissions.Print) Then .ShowPrintButton = False
                '   If Not objSec.HaveUserPermission(SN, UD_Dll.Security.ePermissions.Export) Then .ShowExportButton = False
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

    Private Sub GridEXPishFaktor_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GridEXPishFaktor.Click
        Try
            Dim PishFaktorTarikh As Integer
            Dim ccPishfaktor As Integer = (GridEXPishFaktor.CurrentRow.Cells("ccpishfaktorTitr").Text)
            PishFaktorTarikh = objTools.ConvertNulls(objTools.DLookup("pishfaktortarikh", "tblfo_pishfaktor", "ccpishfaktortitr=" & ccPishfaktor), 0)
            txtTedadKala.Focus()
            Dim mablagh As Integer = 0
            mablagh = objTools.ConvertNulls(objTools.DLookup("fee", "tblFO_PishFaktorSatr", "ccPishFaktorTitr = " & ccPishfaktor & " and cckala=" & txtCodeKala.Tag), 0)
            Me.lblFee.Text = mablagh
            ' lblFee.Text = GridEXPishFaktor.CurrentRow.Cells("Gheymat").Text

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->Print PishFaktor")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->Print PishFaktor")
        End Try
    End Sub

    Private Sub txtTedadKala_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtTedadKala.TextChanged


    End Sub
End Class