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

    Dim Mode As Boolean = False  ' Mode  --> False : Save Titr \\ True : Inser Kala

#End Region
#Region "Form Event Code"
    Private Sub frmFO_SodorKoli_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetParameter()
        objTools.ChangeKbd(UD_Dll.mdlUtility.KBD.Farsi)
        If UserName.ToUpper <> "ADMINISTRATOR" Then
            Dim HaveUserPermission As Integer = objTools.ConvertNulls(objTools.DLookup("SecurityNumber", "tblGL_Security", "CodeSubSystem = 6000020 AND CodeMahal = " & CodeMahalFaal & "  AND NameKarbar = '" & UserName.ToLower & "'"), 0)
            If HaveUserPermission = 0 Then
                mskTarikhFaktor.Enabled = False
                mskTarikhFaktor.Enabled = False
            Else
                mskTarikhFaktor.Enabled = True
                mskTarikhFaktor.Enabled = True
            End If
        Else
            mskTarikhFaktor.Enabled = True
            mskTarikhFaktor.Enabled = True
        End If
        SetForm()
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
        '  lblTedadPishFaktorAmani.Text = objTools.DCount("ccPishFaktorTitr", "tblFO_PishFaktor", "ccMoshtary = " & Me.txtCodeMoshtary.Tag & " AND sVazeiat = 2002")
        lblTedadPishFaktorAmani.Text = objTools.DCount("ccPishFaktorTitr", "tblFO_PishFaktor", "ccMoshtary = " & Me.txtCodeMoshtary.Tag & " AND Codedoreh > = " & CodeDoreh - 1 & " AND CodeMahal = " & CodeMahalFaal & " AND sVazeiat = 2002 and PishFaktorAmani = 1 AND ccPishFaktorTitr NOT IN (SELECT ccPishFaktor FROM tblFO_Faktor)")
    End Sub
    Private Sub txtCodeKala_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCodeKala.KeyPress
        Try
            If (Asc(e.KeyChar()) < 48 Or Asc(e.KeyChar()) > 57) And (Asc(e.KeyChar()) <> 8) Then
                e.Handled = True
            End If
            If e.KeyChar = Chr(Keys.Space) Then

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
            Dim Mablagh As Double = 0

            Me.lblNameKala_CodeKala.Text = objTools.ConvertNulls(objTools.DLookup("NameKala", "tblAN_Kala", "CodeKala = " & Me.txtCodeKala.Text.Trim), "")
            Me.txtCodeKala.Tag = objTools.ConvertNulls(objTools.DLookup("ccKala", "tblAn_Kala", "CodeKala = " & Me.txtCodeKala.Text.Trim), 0)

            Dim cm As New SqlCommand
            cm.CommandText = "Select ISNULL(dbo.GetMablaghFrosh(" & _
                txtCodeKala.Tag & "," & _
                "'" & TarikhEmrooz & "'," & _
                CodeMahalFaal & "," & _
                ccMoshtary & "," & _
                sNoeMoshtary & "),0)"

            cm.Connection = New SqlConnection(ConnectionString)
            cm.Connection.Open()
            Mablagh = cm.ExecuteScalar

            Me.lblFee.Text = Mablagh
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
    Private Sub GridEXPishFaktor_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GridEXPishFaktor.Click
        If dvTitr_PishFaktor.Count > 0 Then
            lblTedadDarPishFaktor.Text = GridEXPishFaktor.CurrentRow.Cells("TedadDarPishFaktor").Text
            lblTedadFaktorShodehAzGhabl.Text = IIf(GridEXPishFaktor.CurrentRow.Cells("TedadFaktorShodehAzGhabl").Text = "", 0, GridEXPishFaktor.CurrentRow.Cells("TedadFaktorShodehAzGhabl").Text)
            lblTedadRezervShodeh.Text = IIf(GridEXPishFaktor.CurrentRow.Cells("TedadRezerv").Text = "", 0, GridEXPishFaktor.CurrentRow.Cells("TedadRezerv").Text)
            lblTedadMarjoeeShodeh.Text = IIf(GridEXPishFaktor.CurrentRow.Cells("TedadMarjoeeShodeh").Text = "", 0, GridEXPishFaktor.CurrentRow.Cells("TedadMarjoeeShodeh").Text)
            lblTedadGhabelFaktor.Text = GridEXPishFaktor.CurrentRow.Cells("TedadGhabelFaktor").Text
            lblFee.Text = GridEXPishFaktor.CurrentRow.Cells("Gheymat").Text
            txtTedadKala.Text = 0
        End If
    End Sub
    Private Sub rbCodeKala_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbCodeKala.CheckedChanged
        If rbCodeKala.Checked Then
            grbKala.Visible = True
            grbBarCodeKala.Visible = False
            ClearForm(False)
        End If
    End Sub
    Private Sub rbBarCodeKala_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbBarCodeKala.CheckedChanged
        If rbBarCodeKala.Checked Then
            grbKala.Visible = False
            grbBarCodeKala.Visible = True
            ClearForm(True)
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

        Delete_PishFaktorAmaniVaset(False)
        Search()
    End Sub
#End Region
#Region "Global Form Code"
    Private Sub SetParameter()
        Dim commands As String = Microsoft.VisualBasic.Command()
        If commands.Length = 0 Then
            UserName = "Administrator"
            UserPassWord = "66998833"
            NameMahalFaal = "تهران"
            CodeMahalFaal = "2060"
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
    Private Sub SetForm()
        If Mode = False Then
            ccPishFaktorAmani_Vaset = 0
            ccMoshtary = 0
            sNoeMoshtary = 0
            rbCodeKala.Checked = True
            txtCodeMoshtary.Text = ""
            lblTedadPishFaktorAmani.Text = ""
            txtCodeMoshtary.Focus()

            Search()
            SearchPishFaktor(0)

            grbSatr.Enabled = False
            btnCreateFaktor.Enabled = False
            btnClearForm.Enabled = False
            btnInsertKala.Enabled = True
            btnCancel.Enabled = False

            txtCodeMoshtary.Focus()

        ElseIf Mode = True Then
            rbCodeKala.Checked = True
            sNoeMoshtary = objTools.DLookup("sNoeMoshtary", "tblFO_Moshtary", "ccMoshtary = " & ccMoshtary)
            lblTedadPishFaktorAmani.Text = objTools.DCount("ccPishFaktorTitr", "tblFO_PishFaktor", "ccMoshtary = " & ccMoshtary & " AND sVazeiat = 2002")

            grbSatr.Enabled = True
            btnCreateFaktor.Enabled = True
            btnClearForm.Enabled = True
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
            ClearForm(False)
        ElseIf rbBarCodeKala.Checked Then
            ClearForm(True)
        End If
    End Sub
    Private Sub ClearForm(ByVal Type As Boolean)
        '' Type ---> False : CodeKala // True : BarCodeKala
        txtCodeKala.Text = ""
        txtCodeKala.Tag = 0
        lblBarCodeKala.Text = ""
        lblNameKala_CodeKala.Text = ""
        txtBarCodeKala.Text = ""
        txtBarCodeKala.Tag = 0
        lblCodeKala.Text = ""
        lblNameKala_BarCodeKala.Text = ""
        lblTedadDarPishFaktor.Text = ""
        lblTedadFaktorShodehAzGhabl.Text = ""
        lblTedadRezervShodeh.Text = ""
        lblTedadMarjoeeShodeh.Text = ""
        lblTedadGhabelFaktor.Text = ""
        lblFee.Text = ""
        txtTedadKala.Text = 0


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
        '''' Type : False --> Delete Satr // True : Delete Titr
        Dim cnSQL As New SqlConnection
        Dim cmSQL As New SqlCommand
        Dim strSQL As String = ""
        Dim CodeCounter As Integer = 0

        If Type = False Then
            CodeCounter = Val(GridEXTitr.CurrentRow.Cells("ccPishFaktorAmaniSatr_Vaset").Text.Replace(",", ""))
        ElseIf Type = True Then
            CodeCounter = ccPishFaktorAmani_Vaset
        End If

        Try
            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()

            strSQL = "Sales.spSodorFaktorAmani_InsertKoli_DeletePishFakotrVasetTitrSatr "

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            cmSQL.Parameters.AddWithValue("Type", Type)
            cmSQL.Parameters.AddWithValue("CodeCounter", CodeCounter)

            cmSQL.ExecuteNonQuery()

            cmSQL = Nothing
            cnSQL.Close()
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

        If dsForm.Tables.Contains("tbl_SearchPishFaktor") Then
            dsForm.Tables.Remove("tbl_SearchPishFaktor")
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
            With GridEXPishFaktor
                .DataSource = Nothing
                .DataSource = dsForm.Tables("tbl_SearchPishFaktor").DefaultView
                .SetDataBinding(dsForm.Tables("tbl_SearchPishFaktor").DefaultView, "")
                .RetrieveStructure()
            End With

            For i As Integer = 0 To GridEXPishFaktor.CurrentTable.Columns.Count - 1
                GridEXPishFaktor.CurrentTable.Columns.Item(i).Visible = False
            Next

            GridEXPishFaktor.CurrentTable.Columns.Item("PishFaktorShomareh").Caption = "ش پیش فاکتور"
            GridEXPishFaktor.CurrentTable.Columns.Item("PishFaktorShomareh").Visible = True
            GridEXPishFaktor.CurrentTable.Columns.Item("PishFaktorShomareh").Width = 85
            GridEXPishFaktor.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXPishFaktor.CurrentTable.Columns.Item("PishFaktorShomareh").Position = 0
            GridEXPishFaktor.CurrentTable.Columns.Item("PishFaktorShomareh").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXPishFaktor.CurrentTable.Columns.Item("PishFaktorTarikh").Caption = "ت پیش فاکتـور"
            GridEXPishFaktor.CurrentTable.Columns.Item("PishFaktorTarikh").Visible = True
            GridEXPishFaktor.CurrentTable.Columns.Item("PishFaktorTarikh").Width = 80
            GridEXPishFaktor.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXPishFaktor.CurrentTable.Columns.Item("PishFaktorTarikh").Position = 1
            GridEXPishFaktor.CurrentTable.Columns.Item("PishFaktorTarikh").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXPishFaktor.CurrentTable.Columns.Item("TedadEstefadeh").Caption = "استفاده"
            GridEXPishFaktor.CurrentTable.Columns.Item("TedadEstefadeh").Visible = True
            GridEXPishFaktor.CurrentTable.Columns.Item("TedadEstefadeh").Width = 70
            GridEXPishFaktor.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXPishFaktor.CurrentTable.Columns.Item("TedadEstefadeh").Position = 2
            GridEXPishFaktor.CurrentTable.Columns.Item("TedadEstefadeh").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

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

            GridEXPishFaktor.CurrentTable.Columns.Item("TedadDarPishFaktor").Caption = "TedadDarPishFaktor"
            GridEXPishFaktor.CurrentTable.Columns.Item("TedadDarPishFaktor").Visible = False
            GridEXPishFaktor.CurrentTable.Columns.Item("TedadDarPishFaktor").Width = 0
            GridEXPishFaktor.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXPishFaktor.CurrentTable.Columns.Item("TedadDarPishFaktor").Position = 5
            GridEXPishFaktor.CurrentTable.Columns.Item("TedadDarPishFaktor").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXPishFaktor.CurrentTable.Columns.Item("TedadFaktorShodehAzGhabl").Caption = "TedadFaktorShodehAzGhabl"
            GridEXPishFaktor.CurrentTable.Columns.Item("TedadFaktorShodehAzGhabl").Visible = False
            GridEXPishFaktor.CurrentTable.Columns.Item("TedadFaktorShodehAzGhabl").Width = 0
            GridEXPishFaktor.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXPishFaktor.CurrentTable.Columns.Item("TedadFaktorShodehAzGhabl").Position = 6
            GridEXPishFaktor.CurrentTable.Columns.Item("TedadFaktorShodehAzGhabl").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXPishFaktor.CurrentTable.Columns.Item("TedadRezerv").Caption = "TedadRezerv"
            GridEXPishFaktor.CurrentTable.Columns.Item("TedadRezerv").Visible = False
            GridEXPishFaktor.CurrentTable.Columns.Item("TedadRezerv").Width = 0
            GridEXPishFaktor.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXPishFaktor.CurrentTable.Columns.Item("TedadRezerv").Position = 7
            GridEXPishFaktor.CurrentTable.Columns.Item("TedadRezerv").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXPishFaktor.CurrentTable.Columns.Item("TedadMarjoeeShodeh").Caption = "TedadMarjoeeShodeh"
            GridEXPishFaktor.CurrentTable.Columns.Item("TedadMarjoeeShodeh").Visible = False
            GridEXPishFaktor.CurrentTable.Columns.Item("TedadMarjoeeShodeh").Width = 0
            GridEXPishFaktor.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXPishFaktor.CurrentTable.Columns.Item("TedadMarjoeeShodeh").Position = 8
            GridEXPishFaktor.CurrentTable.Columns.Item("TedadMarjoeeShodeh").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXPishFaktor.CurrentTable.Columns.Item("TedadGhabelFaktor").Caption = "TedadGhabelFaktor"
            GridEXPishFaktor.CurrentTable.Columns.Item("TedadGhabelFaktor").Visible = False
            GridEXPishFaktor.CurrentTable.Columns.Item("TedadGhabelFaktor").Width = 0
            GridEXPishFaktor.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXPishFaktor.CurrentTable.Columns.Item("TedadGhabelFaktor").Position = 9
            GridEXPishFaktor.CurrentTable.Columns.Item("TedadGhabelFaktor").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXPishFaktor.CurrentTable.Columns.Item("Gheymat").Caption = "Gheymat"
            GridEXPishFaktor.CurrentTable.Columns.Item("Gheymat").Visible = False
            GridEXPishFaktor.CurrentTable.Columns.Item("Gheymat").Width = 0
            GridEXPishFaktor.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXPishFaktor.CurrentTable.Columns.Item("Gheymat").Position = 10
            GridEXPishFaktor.CurrentTable.Columns.Item("Gheymat").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

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

            strSQL = "Sales.spSodorFaktorAmani_InsertKoli_Search "

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            cmSQL.Parameters.AddWithValue("ccPishFaktorAmani_Vaset", ccPishFaktorAmani_Vaset)

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


            GridEXTitr.DataSource = Nothing
            GridEXTitr.DataSource = dvTitr

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
            If dvTitr.Count = 0 Then
                Exit Sub
            End If

            With GridEXTitr
                .DataSource = Nothing
                .DataSource = dsForm.Tables("tbl_Search").DefaultView
                .SetDataBinding(dsForm.Tables("tbl_Search").DefaultView, "")
                .RetrieveStructure()
            End With

            For i As Integer = 0 To GridEXTitr.CurrentTable.Columns.Count - 1
                GridEXTitr.CurrentTable.Columns.Item(i).Visible = False
            Next

            GridEXTitr.CurrentTable.Columns.Item("CodeKala").Caption = "کـد کـالا"
            GridEXTitr.CurrentTable.Columns.Item("CodeKala").Visible = True
            GridEXTitr.CurrentTable.Columns.Item("CodeKala").Width = 80
            GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXTitr.CurrentTable.Columns.Item("CodeKala").EditType = Janus.Windows.GridEX.EditType.NoEdit
            GridEXTitr.CurrentTable.Columns.Item("CodeKala").Position = 0
            GridEXTitr.CurrentTable.Columns.Item("CodeKala").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXTitr.CurrentTable.Columns.Item("NameKala").Caption = "نـام کـالا"
            GridEXTitr.CurrentTable.Columns.Item("NameKala").Visible = True
            GridEXTitr.CurrentTable.Columns.Item("NameKala").Width = 200
            GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXTitr.CurrentTable.Columns.Item("NameKala").EditType = Janus.Windows.GridEX.EditType.NoEdit
            GridEXTitr.CurrentTable.Columns.Item("NameKala").Position = 1
            GridEXTitr.CurrentTable.Columns.Item("NameKala").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXTitr.CurrentTable.Columns.Item("Tedad").Caption = "تعــداد"
            GridEXTitr.CurrentTable.Columns.Item("Tedad").Visible = True
            GridEXTitr.CurrentTable.Columns.Item("Tedad").Width = 70
            GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXTitr.CurrentTable.Columns.Item("Tedad").EditType = Janus.Windows.GridEX.EditType.NoEdit
            GridEXTitr.CurrentTable.Columns.Item("Tedad").FormatString = "G"
            GridEXTitr.CurrentTable.Columns.Item("Tedad").Position = 2
            GridEXTitr.CurrentTable.Columns.Item("Tedad").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXTitr.CurrentTable.Columns.Item("Fee").Caption = "قیمـت"
            GridEXTitr.CurrentTable.Columns.Item("Fee").Visible = True
            GridEXTitr.CurrentTable.Columns.Item("Fee").Width = 70
            GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXTitr.CurrentTable.Columns.Item("Fee").EditType = Janus.Windows.GridEX.EditType.NoEdit
            GridEXTitr.CurrentTable.Columns.Item("Fee").FormatString = "G"
            GridEXTitr.CurrentTable.Columns.Item("Fee").Position = 3
            GridEXTitr.CurrentTable.Columns.Item("Fee").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXTitr.CurrentTable.Columns.Item("Mablagh").Caption = "مبلـــغ"
            GridEXTitr.CurrentTable.Columns.Item("Mablagh").Visible = True
            GridEXTitr.CurrentTable.Columns.Item("Mablagh").Width = 100
            GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXTitr.CurrentTable.Columns.Item("Mablagh").EditType = Janus.Windows.GridEX.EditType.NoEdit
            GridEXTitr.CurrentTable.Columns.Item("Mablagh").FormatString = "G"
            GridEXTitr.CurrentTable.Columns.Item("Mablagh").Position = 4
            GridEXTitr.CurrentTable.Columns.Item("Mablagh").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            '---------------
            GridEXTitr.CurrentTable.Columns.Item("TakhfifKala").Caption = "تخفیف کالا"
            GridEXTitr.CurrentTable.Columns.Item("TakhfifKala").Visible = True
            GridEXTitr.CurrentTable.Columns.Item("TakhfifKala").Width = 100
            GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOn
            GridEXTitr.CurrentTable.Columns.Item("TakhfifKala").Position = 5
            GridEXTitr.CurrentTable.Columns.Item("TakhfifKala").FormatString = "N"
            GridEXTitr.CurrentTable.Columns.Item("TakhfifKala").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            '---------------

            GridEXTitr.CurrentTable.Columns.Item("PishFaktorShomareh").Caption = "ش پیش فاکتور"
            GridEXTitr.CurrentTable.Columns.Item("PishFaktorShomareh").Visible = True
            GridEXTitr.CurrentTable.Columns.Item("PishFaktorShomareh").Width = 110
            GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXTitr.CurrentTable.Columns.Item("PishFaktorShomareh").EditType = Janus.Windows.GridEX.EditType.NoEdit
            GridEXTitr.CurrentTable.Columns.Item("PishFaktorShomareh").Position = 6
            GridEXTitr.CurrentTable.Columns.Item("PishFaktorShomareh").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXTitr.CurrentTable.Columns.Item("PishFaktorTarikh").Caption = "ت پیش فاکتور"
            GridEXTitr.CurrentTable.Columns.Item("PishFaktorTarikh").Visible = True
            GridEXTitr.CurrentTable.Columns.Item("PishFaktorTarikh").Width = 100
            GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXTitr.CurrentTable.Columns.Item("PishFaktorTarikh").EditType = Janus.Windows.GridEX.EditType.NoEdit
            GridEXTitr.CurrentTable.Columns.Item("PishFaktorTarikh").Position = 7
            GridEXTitr.CurrentTable.Columns.Item("PishFaktorTarikh").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXTitr.CurrentTable.Columns.Item("NameForoshandeh").Caption = "نـام فـروشنـده"
            GridEXTitr.CurrentTable.Columns.Item("NameForoshandeh").Visible = True
            GridEXTitr.CurrentTable.Columns.Item("NameForoshandeh").Width = 200
            GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXTitr.CurrentTable.Columns.Item("NameForoshandeh").EditType = Janus.Windows.GridEX.EditType.NoEdit
            GridEXTitr.CurrentTable.Columns.Item("NameForoshandeh").Position = 8
            GridEXTitr.CurrentTable.Columns.Item("NameForoshandeh").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXTitr.CurrentTable.Columns.Item("ccPishFaktorAmaniSatr_Vaset").Caption = "ccPishFaktorAmaniSatr_Vaset"
            GridEXTitr.CurrentTable.Columns.Item("ccPishFaktorAmaniSatr_Vaset").Visible = False
            GridEXTitr.CurrentTable.Columns.Item("ccPishFaktorAmaniSatr_Vaset").Width = 0
            GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXTitr.CurrentTable.Columns.Item("ccPishFaktorAmaniSatr_Vaset").Position = 9
            GridEXTitr.CurrentTable.Columns.Item("ccPishFaktorAmaniSatr_Vaset").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            For i As Integer = 0 To GridEXTitr.RootTable.Columns.Count - 1
                If GridEXTitr.RootTable.Columns(i).Type.IsValueType Then
                    GridEXTitr.RootTable.Columns(i).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                    GridEXTitr.RootTable.Columns(i).FormatMode = Janus.Windows.GridEX.FormatMode.UseIFormattable
                    GridEXTitr.RootTable.Columns(i).FormatString = "###,###.##"
                    GridEXTitr.RootTable.Columns(i).TotalFormatMode = Janus.Windows.GridEX.FormatMode.UseIFormattable
                    GridEXTitr.RootTable.Columns(i).TotalFormatString = "###,###.##"
                End If
            Next

            GridEXTitr.Visible = True
            GridEXTitr.HeaderFormatStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center


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
            '---------------ho3ein----------

            If mskTarikhFaktor.Text = "" Then
                ErrPro.SetError(Me.mskTarikhFaktor, "تاریخ صدور فاکتور را وارد کنید.")
                MsgBox("تاریخ صدور فاکتور را وارد کنید.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
                mskTarikhFaktor.Focus()
                Exit Function
            End If


            If Len(mskTarikhFaktor.Text.ToString) <> 0 Then
                If Not objTarikh.IsShDate(mskTarikhFaktor.Text.ToString) Then
                    mskTarikhFaktor.Focus()
                    Exit Function
                End If

                If TarikhEmrooz < mskTarikhFaktor.Text Then
                    MsgBox("تاریخ صدور فاکتور از تاریخ روز جلوتر است.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Critical, "خطا")
                    ErrPro.SetError(mskTarikhFaktor, "تاریخ صدور فاکتور از تاریخ روز جلوتر است.")
                    Exit Function
                End If
            Else
                ErrPro.SetError(Me.mskTarikhFaktor, " تاریخ را وارد کنید.")
                MsgBox(" تاریخ را وارد کنید.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
                mskTarikhFaktor.Focus()
                Exit Function
            End If
            '---------------ho3ein----------


            If rbDasti.Checked = True Then
                If chkField = "ccPishFaktorTitr" Or chkField = "All" Then
                    If (CDbl(Me.lblTedadDarPishFaktor.Text.Trim) = 0) Or lblTedadDarPishFaktor.Text.Trim = "" Then
                        MsgBox(" لطفاً یک پیش فاکتور انتخــاب نمایید .", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
                        Exit Function
                    End If
                End If

                If rbCodeKala.Checked Then
                    If chkField = "txtCodeKala" Or chkField = "All" Then
                        If objTools.ConvertNulls(objTools.DCount("ccPishFaktorAmaniSatr_Vaset", "tblFO_PishFaktorAmaniSatr_Vaset", "ccPishFaktorTitr = " & Val(GridEXPishFaktor.CurrentRow.Cells("ccPishFaktorTitr").Text.Replace(",", "")) & " AND ccPishFaktorAmani_Vaset = " & ccPishFaktorAmani_Vaset & " AND ccKala = " & txtCodeKala.Tag), 0) <> 0 Then
                            ErrPro.SetError(txtCodeKala, "کالای وارد شده از پیش فاکتور انتخابی تکراری است .")
                            MsgBox(" کالای وارد شده از پیش فاکتور انتخابی تکراری است .", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
                            txtCodeKala.Focus()
                            Exit Function
                        End If
                        ErrPro.SetError(txtCodeKala, "")
                    End If
                ElseIf rbBarCodeKala.Checked Then
                    If chkField = "txtBarCodeKala" Or chkField = "All" Then
                        If objTools.ConvertNulls(objTools.DCount("ccPishFaktorAmaniSatr_Vaset", "tblFO_PishFaktorAmaniSatr_Vaset", "ccPishFaktorTitr = " & Val(GridEXPishFaktor.CurrentRow.Cells("ccPishFaktorTitr").Text.Replace(",", "")) & " AND ccPishFaktorAmani_Vaset = " & ccPishFaktorAmani_Vaset & " AND ccKala = " & txtBarCodeKala.Tag), 0) <> 0 Then
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
                ElseIf rbDasti.Checked = True AndAlso (CDbl(Me.txtTedadKala.Text)) > (CDbl(Me.lblTedadGhabelFaktor.Text.Trim)) Then
                    ErrPro.SetError(txtTedadKala, "تعــداد نمی تواند بزرگتر از تعداد قابل فاکتـور باشد .")
                    MsgBox(" تعــداد نمی تواند بزرگتر از تعداد قابل فاکتـور باشد .", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
                    txtTedadKala.Focus()
                    Exit Function
                ElseIf rbDasti.Checked = True AndAlso (CDbl(Me.lblTedadGhabelFaktor.Text.Trim)) < (TedadDarPishFaktor - TedadDarFaktorSaderShodeh - TedadRezervShodeh - TedadMarjoee) Then
                    ErrPro.SetError(txtTedadKala, "تعــداد قابل فاکتـور تغییر کرده، لطفا مجددا بر روی دکمه « پیش فاکتور های شامل کالا » کلیک نمایید .")
                    MsgBox(" تعــداد قابل فاکتـور تغییر کرده، لطفا مجددا بر روی دکمه « پیش فاکتور های شامل کالا » کلیک نمایید .", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
                    txtTedadKala.Focus()
                    Exit Function
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
    Private Sub InsertTitr()
        Dim cnSQL As New SqlConnection
        Dim cmSQL As New SqlCommand
        Dim strSQL As String = ""

        Try
            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()

            strSQL = "Sales.spSodorFaktorAmani_InsertKoli_InsertPishFaktorAmaniVasetTitr "

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            cmSQL.Parameters.AddWithValue("UserName", UserName)
            cmSQL.Parameters.AddWithValue("Tarikh", TarikhEmrooz)
            cmSQL.Parameters.AddWithValue("Saat", Format(TimeOfDay, "HH:mm:ss"))
            cmSQL.Parameters.AddWithValue("ccPishFaktorAmani_Vaset", ccPishFaktorAmani_Vaset)
            cmSQL.Parameters("ccPishFaktorAmani_Vaset").Direction = ParameterDirection.Output

            cmSQL.ExecuteNonQuery()

            ccPishFaktorAmani_Vaset = cmSQL.Parameters("ccPishFaktorAmani_Vaset").Value

            cmSQL = Nothing
            cnSQL.Close()

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> InsertTitr ")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> InsertTitr ")
        End Try
    End Sub
    Private Sub InsertSatr(ByVal ccPishFaktorTitr As Integer, ByVal Tedad As Double, ByVal Gheymat As Integer)
        Dim cnSQL As New SqlConnection
        Dim cmSQL As New SqlCommand
        Dim strSQL As String = ""
        Dim ccKala As Integer = 0

        If rbCodeKala.Checked Then
            ccKala = txtCodeKala.Tag
        ElseIf rbBarCodeKala.Checked Then
            ccKala = txtBarCodeKala.Tag
        End If

        Try
            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()

            strSQL = "Sales.spSodorFaktorAmani_InsertKoli_InsertPishFaktorAmaniVasetSatr "

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            cmSQL.Parameters.AddWithValue("ccPishFaktorAmani_Vaset", ccPishFaktorAmani_Vaset)
            cmSQL.Parameters.AddWithValue("CodeDoreh", CodeDoreh)
            cmSQL.Parameters.AddWithValue("CodeMahal", CodeMahalFaal)
            cmSQL.Parameters.AddWithValue("ccMoshtary", ccMoshtary)
            cmSQL.Parameters.AddWithValue("ccPishFaktorTitr", ccPishFaktorTitr)
            cmSQL.Parameters.AddWithValue("Tarikh", TarikhEmrooz)
            cmSQL.Parameters.AddWithValue("ccKala", ccKala)
            cmSQL.Parameters.AddWithValue("Tedad", Tedad)
            cmSQL.Parameters.AddWithValue("Fee", Gheymat)

            cmSQL.ExecuteNonQuery()

            cmSQL = Nothing
            cnSQL.Close()

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> InsertSatr_Dasti ")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> InsertSatr_Dasti ")
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
            'Dim Tedad3 As Integer = 0
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
                    'Tedad3 = Tedad3 + TedadGhabelSabt
                    InsertSatr(dr("ccPishFaktorTitr"), TedadGhabelSabt, dr("Gheymat"))
                End If
            Next
            'InsertSatr(dr("ccPishFaktorTitr"), Tedad3, dr("Gheymat"), objTools.ConvertNulls(txtTakhfifKala.Text, 0))

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

            grbBtnShowPishFaktor.Enabled = True
            grbPishFaktor.Enabled = True
            grbSabtAzPishFaktor.Enabled = False

        ElseIf Type = True Then
            rbNozoli.Checked = True
            rbSoodi.Checked = False

            grbBtnShowPishFaktor.Enabled = False
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

            lblTedadDarPishFaktor.Text = ""
            lblTedadFaktorShodehAzGhabl.Text = ""
            lblTedadRezervShodeh.Text = ""
            lblTedadMarjoeeShodeh.Text = ""
            lblTedadGhabelFaktor.Text = ""
            txtTedadKala.Text = 0


        End If

    End Sub
    Private Sub CreateFaktor()
        Dim cnSQL As New SqlConnection
        Dim cmSQL As New SqlCommand
        Dim daSQL As New SqlDataAdapter
        Dim dv As New DataView
        Dim dr As DataRowView
        Dim strSQL As String = ""

        If dsForm.Tables.Contains("tblTaeed_SearchPishFaktor") Then
            dsForm.Tables.Remove("tblTaeed_SearchPishFaktor")
        End If

        Try
            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()

            strSQL = "Sales.spSodorFaktorAmani_InsertKoli_Taeed_SearchPishFaktor "

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            cmSQL.Parameters.AddWithValue("ccPishFaktorAmani_Vaset", ccPishFaktorAmani_Vaset)

            daSQL = New SqlDataAdapter(cmSQL)
            daSQL.Fill(dsForm, "tblTaeed_SearchPishFaktor")

            dv = New DataView(dsForm.Tables("tblTaeed_SearchPishFaktor"))

            For Each dr In dv
                CreateTitrFaktor(dr("ccPishFaktorTitr"), ccPishFaktorAmani_Vaset)
            Next

            cmSQL = Nothing : daSQL = Nothing
            cnSQL.Close()

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> CreateFaktor ")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> CreateFaktor ")
        End Try
    End Sub
    Private Sub CreateTitrFaktor(ByVal ccPishFaktor As Integer, ByVal ccPishFaktorAmani_Vaset As Integer)
        Dim cnSQL As New SqlConnection
        Dim cmSQL As New SqlCommand
        Dim daSQL As New SqlDataAdapter
        Dim dv As New DataView
        Dim dr As DataRowView
        Dim strSQL As String = ""
        Dim ccFaktorTitr As Integer = 0
        Dim FaktorShomareh As Integer = 0

        FaktorShomareh = objTools.ConvertNulls(objTools.DLookupOne("FaktorShomareh", "tblFO_Faktor", "CodeMahal = " & CodeMahalFaal & " AND CodeDoreh = " & CodeDoreh, "FaktorShomareh DESC"), 0) + 1

        If dsForm.Tables.Contains("tblTaeed_SearchKalaInPishFaktor") Then
            dsForm.Tables.Remove("tblTaeed_SearchKalaInPishFaktor")
        End If

        Try
            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()

            strSQL = "Sales.spSodorFaktorAmani_InsertKoli_Taeed_CreateTitrFaktor "

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            cmSQL.Parameters.AddWithValue("CodeDoreh", CodeDoreh)
            cmSQL.Parameters.AddWithValue("FaktorShomareh", FaktorShomareh)
            cmSQL.Parameters.AddWithValue("FaktorTarikh", mskTarikhFaktor.Text)
            cmSQL.Parameters.AddWithValue("ccPishFaktor", ccPishFaktor)
            cmSQL.Parameters.AddWithValue("UserName", UserName)
            cmSQL.Parameters.AddWithValue("Tarikh", TarikhEmrooz)
            cmSQL.Parameters.AddWithValue("Saat", Format(TimeOfDay, "HH:mm:ss"))
            cmSQL.Parameters.AddWithValue("ccFaktorTitr", ccFaktorTitr)
            cmSQL.Parameters.AddWithValue("ccPishFaktorAmani_Vaset", ccPishFaktorAmani_Vaset)

            cmSQL.Parameters("ccFaktorTitr").Direction = ParameterDirection.Output

            cmSQL.ExecuteNonQuery()

            ccFaktorTitr = cmSQL.Parameters("ccFaktorTitr").Value

            cmSQL = Nothing

            '--------------------------------------------

            strSQL = "Sales.spSodorFaktorAmani_InsertKoli_Taeed_SearchKalaInPishFaktor "

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            cmSQL.Parameters.AddWithValue("ccPishFaktorAmani_Vaset", ccPishFaktorAmani_Vaset)
            cmSQL.Parameters.AddWithValue("ccPishFaktorTitr", ccPishFaktor)
            cmSQL.Parameters.AddWithValue("TarikhEmrooz", TarikhEmrooz)

            daSQL = New SqlDataAdapter(cmSQL)
            daSQL.Fill(dsForm, "tblTaeed_SearchKalaInPishFaktor")

            dv = New DataView(dsForm.Tables("tblTaeed_SearchKalaInPishFaktor"))

            For Each dr In dv
                InsertKalaInFaktor(ccFaktorTitr, ccPishFaktor, dr("ccKala"), dr("Tedad"), dr("Fee"), dr("TakhfifKala"))
            Next

            'If objTools.ConvertNulls(objTools.DLookup("Malyat", "tblFO_PishFaktor", "ccPishFaktorTitr = " & ccPishFaktor), False) = True Then
            '    ApplyMalyatAvarez(ccFaktorTitr)
            'End If

            UpdateTaeedFaktor(ccFaktorTitr)

            cmSQL = Nothing : daSQL = Nothing
            cnSQL.Close()

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> CreateTitrFaktor ")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> CreateTitrFaktor ")
        End Try
    End Sub
    Private Sub InsertKalaInFaktor(ByVal ccFaktor As Integer, ByVal ccPishFaktorTitr As Integer, ByVal ccKala As Integer, ByVal Tedad As Double, ByVal Fee As Integer, ByVal TakhfifKala As Double)
        Dim cnSQL As New SqlConnection
        Dim cmSQL As New SqlCommand
        Dim strSQL As String = ""

        Try
            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()

            strSQL = "Sales.spSodorFaktorAmani_InsertKoli_Taeed_InsertKalaInSatrFaktor "

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            cmSQL.Parameters.AddWithValue("ccFaktor", ccFaktor)
            cmSQL.Parameters.AddWithValue("ccPishFaktor", ccPishFaktorTitr)
            cmSQL.Parameters.AddWithValue("ccKala", ccKala)
            cmSQL.Parameters.AddWithValue("Tedad", Tedad)
            cmSQL.Parameters.AddWithValue("Fee", Fee)
            cmSQL.Parameters.AddWithValue("TakhfifKala", TakhfifKala)
            cmSQL.ExecuteNonQuery()

            cmSQL = Nothing
            cnSQL.Close()

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> InsertKalaInFaktor ")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> InsertKalaInFaktor ")
        End Try
    End Sub
    Private Sub ApplyMalyatAvarez(ByVal ccFaktorTitr As Double)
        Dim strSQL As String = ""
        Dim cn As New SqlConnection(ConnectionString)
        Dim da As SqlDataAdapter = Nothing
        Dim dt As New DataTable
        Dim cm As SqlCommand = Nothing
        Dim p As New SqlParameter

        IsMalyatAvarezTakhfif = objTools.ConvertNulls(objTools.DLookup("IsMalyatAvarezTakhfif", "tblGL_SysConfig", "CodeMahal = " & CodeMahalFaal), False)
        Dim Malyat As Double = 0
        Dim Avarez As Double = 0

        Try
            strSQL = "Sales.spSodorFaktorAmani_ApplyMalyatAvarez "

            cn.Open()
            cm = New SqlCommand(strSQL, cn)
            cm.CommandType = CommandType.StoredProcedure
            cm.Parameters.Clear()

            cm.Parameters.AddWithValue("ccFaktorTitr", ccFaktorTitr)
            cm.Parameters.AddWithValue("IsMalyatAvarezTakhfif", IsMalyatAvarezTakhfif)

            cm.ExecuteNonQuery()

            cm = Nothing
            cn.Close()

            'strSQL = "Sales.spSodorFaktorAmani_InsertKoli_ApplyMalyatAvarez_Search "

            'cn.Open()
            'cm = New SqlCommand(strSQL, cn)
            'cm.CommandType = CommandType.StoredProcedure
            'cm.Parameters.Clear()

            'p = New SqlParameter("ccFaktorTitr", SqlDbType.Int)
            'p.Value = ccFaktorTitr
            'cm.Parameters.Add(p)

            'da = New SqlDataAdapter(cm)

            'Try
            '    da.Fill(dt)
            '    For Each dr As DataRow In dt.Rows

            '        Dim a As Double = objTools.ConvertNulls(objTools.DLookup("TakhfifKala", "tblFO_Faktorsatr", "ccFaktorsatr = " & dr("ccFaktorSatr")), 0)

            '        Malyat = Math.Round(objCode.GetMablaghMalyat(dr("MKOL3") - a), 0)
            '        Avarez = Math.Round(objCode.GetMablaghAvarez(dr("MKOL3") - a), 0)

            '        Dim TakhfifMalyatAvarez As Double = Malyat + Avarez

            '        strSQL = "Sales.spSodorFaktorAmani_InsertKoli_ApplyMalyatAvarez_UpdateMablaghMalyatAndAvarez "

            '        cm = New SqlCommand(strSQL, cn)
            '        cm.CommandType = CommandType.StoredProcedure
            '        cm.Parameters.Clear()

            '        p = New SqlParameter("ccFaktorSatr", SqlDbType.Int)
            '        p.Value = dr("ccFaktorSatr")
            '        cm.Parameters.Add(p)

            '        p = New SqlParameter("MablaghMalyat", SqlDbType.Float)
            '        p.Value = Malyat
            '        cm.Parameters.Add(p)

            '        p = New SqlParameter("MablaghAvarez", SqlDbType.Float)
            '        p.Value = Avarez
            '        cm.Parameters.Add(p)

            '        cm.ExecuteNonQuery()

            '        '----------------------------------------------------------------------------------------


            '        strSQL = "Sales.spSodorFaktorAmani_InsertKoli_ApplyMalyatAvarez_UpdateTakhfifMalyatAvarez "

            '        cm = New SqlCommand(strSQL, cn)
            '        cm.CommandType = CommandType.StoredProcedure
            '        cm.Parameters.Clear()

            '        p = New SqlParameter("ccFaktorSatr", SqlDbType.Int)
            '        p.Value = dr("ccFaktorSatr")
            '        cm.Parameters.Add(p)

            '        p = New SqlParameter("TakhfifMalyatAvarez", SqlDbType.Float)
            '        p.Value = 0
            '        cm.Parameters.Add(p)

            '        cm.ExecuteNonQuery()

            '        '----------------------------------------------------------------------------------------

            '        If IsMalyatAvarezTakhfif = True Then
            '            strSQL = "Sales.spSodorFaktorAmani_InsertKoli_ApplyMalyatAvarez_UpdateTakhfifMalyatAvarez "

            '            cm = New SqlCommand(strSQL, cn)
            '            cm.CommandType = CommandType.StoredProcedure
            '            cm.Parameters.Clear()

            '            p = New SqlParameter("ccFaktorSatr", SqlDbType.Int)
            '            p.Value = dr("ccFaktorSatr")
            '            cm.Parameters.Add(p)

            '            p = New SqlParameter("TakhfifMalyatAvarez", SqlDbType.Float)
            '            p.Value = TakhfifMalyatAvarez
            '            cm.Parameters.Add(p)

            '            cm.ExecuteNonQuery()

            '        End If

            '    Next

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub UpdateTaeedFaktor(ByVal ccFaktorTitr As Long)
        Try
            Dim cnSQL As SqlConnection
            Dim cmSQL As SqlCommand
            Dim p As New SqlParameter
            Dim strSQL

            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()

            strSQL = "Sales.spSodorFaktorAmani_InsertKoli_Taeed_UpdateTaeedFaktor "

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            p = New SqlParameter("ccFaktorTitr", SqlDbType.BigInt)
            p.Value = ccFaktorTitr
            cmSQL.Parameters.Add(p)

            cmSQL.ExecuteNonQuery()

            cnSQL.Close()
            cmSQL = Nothing : cnSQL = Nothing

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->UpdateTaeedFaktor")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->UpdateTaeedFaktor")
        End Try
    End Sub
    Private Sub UpdateVazeiatSanad()
        Dim cnSQL As New SqlConnection
        Dim cmSQL As New SqlCommand
        Dim strSQL As String = ""

        Try
            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()

            strSQL = "Sales.spSodorFaktorAmani_InsertKoli_Taeed_UpdateVazeiatSanad "

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            cmSQL.Parameters.AddWithValue("ccPishFaktorAmani_Vaset", ccPishFaktorAmani_Vaset)

            cmSQL.ExecuteNonQuery()

            cmSQL = Nothing
            cnSQL.Close()

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> UpdateVazeiatSanad ")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> UpdateVazeiatSanad ")
        End Try
    End Sub
#End Region
#Region "From Buttons "
    Private Sub btnInsertKala_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnInsertKala.Click
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


        '---------------ho3ein----------

        If mskTarikhFaktor.Text = "" Then
            ErrPro.SetError(Me.mskTarikhFaktor, "تاریخ صدور فاکتور را وارد کنید.")
            MsgBox("تاریخ صدور فاکتور را وارد کنید.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
            mskTarikhFaktor.Focus()
            Exit Sub
        End If


        If Len(mskTarikhFaktor.Text.ToString) <> 0 Then
            If Not objTarikh.IsShDate(mskTarikhFaktor.Text.ToString) Then
                mskTarikhFaktor.Focus()
                Exit Sub
            End If

            If TarikhEmrooz < mskTarikhFaktor.Text Then
                MsgBox("تاریخ صدور فاکتور از تاریخ روز جلوتر است.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Critical, "خطا")
                ErrPro.SetError(mskTarikhFaktor, "تاریخ صدور فاکتور از تاریخ روز جلوتر است.")
                Exit Sub
            End If
        Else
            ErrPro.SetError(Me.mskTarikhFaktor, " تاریخ را وارد کنید.")
            MsgBox(" تاریخ را وارد کنید.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
            mskTarikhFaktor.Focus()
            Exit Sub
        End If
        '---------------ho3ein----------

        ccMoshtary = ccMoshtary
        sNoeMoshtary = objTools.DLookup("sNoeMoshtary", "tblFO_Moshtary", "ccMoshtary = " & ccMoshtary)

        Mode = True
        SetForm()

        If ccPishFaktorAmani_Vaset = 0 Then
            InsertTitr()
        End If
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
    End Sub
    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If rbDasti.Checked AndAlso lblTedadDarPishFaktor.Text = "" Then
            MsgBox("ابتدا باید یک پیش فاکتور انتخاب نمایید .", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information + MsgBoxStyle.Critical, "خطا")
            Exit Sub
        End If

        If Not IsValidRow("All") Then
            Exit Sub
        End If

        If ccPishFaktorAmani_Vaset = 0 Then
            InsertTitr()
        End If

        If rbDasti.Checked = True Then
            Dim PK As Integer = Val(GridEXPishFaktor.CurrentRow.Cells("ccPishFaktorTitr").Text.Replace(",", ""))

            If lblFee.Text.Replace(",", "") = "" Then
                MsgBox("برای این کالا در سیستم قیمت تعریف نشده است .", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information + MsgBoxStyle.Critical, "خطا")
                Exit Sub
            End If

            Dim Gheymat As Integer = lblFee.Text.Replace(",", "")

            InsertSatr(PK, txtTedadKala.Text, Gheymat)
        ElseIf rbAutomatic.Checked = True Then
            InsertSatr_Automatic()
        End If

        ClearForm(False)
        Search()
    End Sub
    Private Sub btnShowPishFaktor_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnShowPishFaktor.Click
        If rbCodeKala.Checked Then
            SearchPishFaktor(txtCodeKala.Tag)
        Else
            SearchPishFaktor(txtBarCodeKala.Tag)
        End If

        flg_SearchPishFaktor = True
    End Sub
    Private Sub btnCreateFaktor_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCreateFaktor.Click
        If dvTitr.Count = 0 Then
            MsgBox("رکوردی ثبت نگردیده است .", MsgBoxStyle.Information + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.OkOnly, "")
            Exit Sub
        End If

        CreateFaktor()

        UpdateVazeiatSanad()

        Mode = False
        SetForm()
    End Sub
    Private Sub btnClearForm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearForm.Click
        ClearForm(False)
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

    Private Sub btnSabteTaghirat_Click(sender As Object, e As EventArgs) Handles btnSabteTaghirat.Click
        Dim cnSQL As New SqlConnection
        Dim cmSQL As New SqlCommand
        Dim daSQL As New SqlDataAdapter
        Dim dv As New DataView
        Dim strSQL As String = ""


        Try



            For i As Integer = 0 To GridEXTitr.RowCount - 1
                If Val(GridEXTitr.GetRows(i).Cells("TakhfifKala").Text.Replace(",", "")) <> 0 Then

                    If Val(GridEXTitr.GetRows(i).Cells("TakhfifKala").Text.Replace(",", "")) < Val(GridEXTitr.GetRows(i).Cells("Mablagh").Text.Replace(",", "")) Then
                        SabteTaghirat(Val(GridEXTitr.GetRows(i).Cells("ccPishFaktorAmaniSatr_Vaset").Text.Replace(",", "")), Val(GridEXTitr.GetRows(i).Cells("TakhfifKala").Text.Replace(",", "")))
                    Else
                        MsgBox(" تخفیف وارد شده نمی تواند بیشتر از « " & Val(GridEXTitr.GetRows(i).Cells("Mablagh").Text.Replace(",", "")) & " » باشد .", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "خطا")
                        ClearForm(False)
                        Search()
                        Exit Sub

                    End If
                End If
            Next
            MsgBox("تغییرات با موفقیت ذخیره شد.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, "")
            ClearForm(False)
            Search()
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> خطا در ثبت تغییرات ")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> Search ")
        End Try
    End Sub
    Private Sub SabteTaghirat(ByVal ccPishFaktorAmaniSatr_Vaset As Long, ByVal TakhfifKala As Long)
        Try

            Dim cnSQL As SqlConnection
            Dim cmSQL As SqlCommand
            Dim p As New SqlParameter
            Dim strSQL As String

            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()

            strSQL = "Sales.spSodorFaktorAmani_Update_PishFaktorAmaniVasetSatr "

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            cmSQL.Parameters.AddWithValue("ccPishFaktorAmaniSatr_Vaset", ccPishFaktorAmaniSatr_Vaset)
            cmSQL.Parameters.AddWithValue("TakhfifKala", TakhfifKala)
            cmSQL.ExecuteNonQuery()



            cnSQL.Close()
            cmSQL = Nothing : cnSQL = Nothing


        Catch ex As Exception
            MsgBox(ex.Message)

        End Try
    End Sub

    Private Sub btnPreview_Click(sender As Object, e As EventArgs) Handles btnPreview.Click
        If dvTitr.Count = 0 Then
            MsgBox("رکوردی ثبت نگردیده است .", MsgBoxStyle.Information + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.OkOnly, "")
            Exit Sub
        End If

        Dim frmpreview As New preview
        frmpreview.ccPishFaktorAmani_Vaset = ccPishFaktorAmani_Vaset
        Me.Hide()
        frmpreview.ShowDialog(Me)
        Me.Show()





    End Sub
End Class