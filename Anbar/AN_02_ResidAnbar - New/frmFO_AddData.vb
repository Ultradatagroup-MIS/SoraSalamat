Public Class Form1

    Const cntCodeSubSystem As Long = 635
    Const FormTableName = "tblAN_KdxResid"
    Const FormViewName = "qryAN_KdxResid"
    Const FormTableNameSatr = "tblAN_kdxResidSatr"

    Dim dsForm As New DataSet
    Dim Mode As UD_Dll.Enums.GL_ModeForms = UD_Dll.Enums.GL_ModeForms.AddNewRecord
    Dim cmTitr As CurrencyManager
    Dim cmSatr As CurrencyManager
    Dim dvTitr As DataView
    Dim dvSatr As DataView
    Dim tCodeCounter As Long
    Dim ErrPro As New ErrorProvider
    Dim dvForm As DataView
    Dim txtCaption As String
    Private SN As Integer
    Dim flg As Boolean = False
    Dim FirstInsert As Boolean = False
    Dim flgInsertSatrSefaresh As Boolean = False
    Dim flgInsertSatrMojodiMashin As Boolean = False
    Dim tmpPosition As Integer
    Dim ccShomarehBach As Integer
    Dim tpos As Integer
    Dim AllowChangeFeePishFaktor As Boolean = False
    Dim AllowChangeFeeResid As Boolean = False
    Private _isCalculating As Boolean = False
    Private ccKardexTitr As Integer = 0
    Private dt_SearchSatr As DataTable
    Private _gridStructureReady As Boolean = False
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        istarikhmiladi = objTools.ConvertNulls(objTools.DLookup("IsTarikhMiladi", "tblGL_SysConfig", "CodeMahal = " & CodeMahalFaal), False)
        SN = objSec.GetSecurityNumber(cntCodeSubSystem, UserName)
        AllowChangeFeePishFaktor = objTools.ConvertNulls(objTools.DLookup("AllowChangeFeePishFaktor", "tblGL_SysConfig", "CodeMahal = " & CodeMahalFaal), False)
        AllowChangeFeeResid = objTools.ConvertNulls(objTools.DLookup("MablaghKharidDasti", "tblGL_SysConfig", "CodeMahal = " & CodeMahalFaal), False)
        Mode = UD_Dll.Enums.GL_ModeForms.None

        If objTools.ConvertNulls(objTools.DLookup("ShowTedadBastehKarton", "tblGl_Sysconfig", "CodeMahal = " & CodeMahalFaal & ""), False) = True Then
            'txtTedadBasteh.Enabled = False
            'txtTedadKarton.Enabled = False
        End If

        LoadCombo()
        flg = False
        ClearForm()
        flg = True

        With GridEXSatr
            .UpdateMode = Janus.Windows.GridEX.UpdateMode.RowUpdate
            .AllowEdit = Janus.Windows.GridEX.InheritableBoolean.True
            .AllowAddNew = Janus.Windows.GridEX.InheritableBoolean.True
            .TabKeyBehavior = Janus.Windows.GridEX.TabKeyBehavior.ControlNavigation
            .Enabled = False   ' تا قبل از ذخیره‌ی هدر
        End With

    End Sub
    Private Sub LoadCombo()
        Try
            Dim Strsql As String
            Dim daSQL As SqlDataAdapter

            ' --------------------------------------- Load Combo -----------------
            ' Load Combo tblTaminKonandeh
            Strsql = "Select NameTaminKonandeh,ccTaminKonandeh From tblFO_TaminKonandeh where Faal=1 AND CodeTafsily1 in (select codetafsily from tblHE_CodeTafsily where codemahal = " & CodeMahalFaal & ")"
            Strsql &= " AND Not exists (Select * From tblGL_SecurityData where namekarbar= '" & UserName & "' and CodeSubSystem = 654 and pk = tblFO_TaminKonandeh.ccTaminKonandeh) order by ccTaminKonandeh"
            daSQL = New SqlDataAdapter(Strsql, ConnectionString)
            daSQL.Fill(dsForm, "tblTaminKonandeh")
            cmbNameTaminKonandeh.DataSource = Nothing
            cmbNameTaminKonandeh.Items.Clear()
            cmbNameTaminKonandeh.DataSource = dsForm.Tables("tblTaminKonandeh").DefaultView
            cmbNameTaminKonandeh.DisplayMember = "NameTaminKonandeh"
            cmbNameTaminKonandeh.ValueMember = "ccTaminKonandeh"

            ' Load Combo tblAnbar
            Strsql = "Select codeAnbar,NameAnbar From qryAN_Anbar where CodeMahal=" & CodeMahalFaal & " AND Faal=1  AND "
            Strsql &= " Not exists (Select * From tblGL_SecurityData where namekarbar= '" & UserName & "' and CodeSubSystem = 633 and pk = qryAN_Anbar.CodeAnbar) order by qryAN_Anbar.Radif "
            daSQL = New SqlDataAdapter(Strsql, ConnectionString)
            daSQL.Fill(dsForm, "tblAnbar")
            cmbAnbar.DataSource = Nothing
            cmbAnbar.Items.Clear()
            cmbAnbar.DataSource = dsForm.Tables("tblAnbar").DefaultView
            cmbAnbar.DisplayMember = "NameAnbar"
            cmbAnbar.ValueMember = "codeAnbar"

            ' Load Search Combo CodeDorehSefaresh
            Strsql = "Select CodeDoreh From tblGl_Doreh group by CodeDoreh "
            daSQL = New SqlDataAdapter(Strsql, ConnectionString)
            daSQL.Fill(dsForm, "tblDoreh")
            cmbsCodeDorehSefaresh.DataSource = Nothing
            cmbsCodeDorehSefaresh.Items.Clear()
            cmbsCodeDorehSefaresh.DataSource = dsForm.Tables("tblDoreh").DefaultView
            cmbsCodeDorehSefaresh.DisplayMember = "CodeDoreh"
            cmbsCodeDorehSefaresh.ValueMember = "CodeDoreh"


            ' Load Combo TahvilGirandeh
            If objCode.CheckCompany = 5 Then
                Strsql = "Select FN From dbo.qryGL_MoshakhasatFardi"
                Strsql &= " Where sSemat in (4068,4061,4062,4846,4848,4849,5553) And sVazeiatEstekhdam = 4188 "
            Else
                Strsql = "Select FN From dbo.qryGL_MoshakhasatFardi"
                Strsql &= " Where sSemat in (4068,4061,4062,4846,4848,4849,5553) And sVazeiatEstekhdam = 4188 "
            End If
            daSQL = New SqlDataAdapter(Strsql, ConnectionString)
            daSQL.Fill(dsForm, "TahvilGirandeh")
            cmbTahvilGirandeh.DataSource = Nothing
            cmbTahvilGirandeh.Items.Clear()
            cmbTahvilGirandeh.DataSource = dsForm.Tables("TahvilGirandeh").DefaultView
            cmbTahvilGirandeh.DisplayMember = "FN"
            cmbTahvilGirandeh.ValueMember = "FN"
            cmbTahvilGirandeh.SelectedValue = 0

            ' Load Combo TahvilDahandeh
            If objCode.CheckCompany = 5 Then
                Strsql = "Select FN From dbo.qryGL_MoshakhasatFardi"
                Strsql &= " Where sSemat in (4068,4061,4062,4846,4848,4849,5553) And sVazeiatEstekhdam = 4188 "
            Else
                Strsql = "Select FN From dbo.qryGL_MoshakhasatFardi"
                Strsql &= " Where sSemat in (4068,4061,4062,4846,4848,4849,5553) And sVazeiatEstekhdam = 4188 "
            End If
            daSQL = New SqlDataAdapter(Strsql, ConnectionString)
            daSQL.Fill(dsForm, "TahvilDahandeh")
            cmbTahvilDahandeh.DataSource = Nothing
            cmbTahvilDahandeh.Items.Clear()
            cmbTahvilDahandeh.DataSource = dsForm.Tables("TahvilDahandeh").DefaultView
            cmbTahvilDahandeh.DisplayMember = "FN"
            cmbTahvilDahandeh.ValueMember = "FN"
            cmbTahvilDahandeh.SelectedValue = 0



            daSQL = Nothing
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->LoadCombo")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->LoadCombo")
        End Try
    End Sub
    Private Sub ClearForm()
        Try
            cmbAnbar.SelectedIndex = -1
            cmbAnbar.SelectedIndex = -1

            'mskTarikhTolid.Enabled = True
            'mskTarikhEngheza.Enabled = True

            'cmbMarkazPakhsh.SelectedIndex = -1
            'cmbMarkazPakhsh.SelectedIndex = -1

            'Me.txtCodeKala.Text = ""
            'Me.txtCodeKala.Tag = ""
            'Me.lblNameKala.Text = ""

            cmbAnbar.SelectedIndex = -1
            cmbAnbar.SelectedIndex = -1

            cmbNameTaminKonandeh.SelectedIndex = -1
            cmbNameTaminKonandeh.SelectedIndex = -1
            If Mode <> UD_Dll.Enums.GL_ModeForms.UpdateRecord Then
                'cmbsCodeDorehSefaresh.SelectedIndex = -1
                'cmbsCodeDorehSefaresh.SelectedIndex = -1
                'cmbsCodeDorehSefaresh.Enabled = True
                'Else
                cmbsCodeDorehSefaresh.SelectedIndex = cmbsCodeDorehSefaresh.Items.Count - 1
                cmbsCodeDorehSefaresh.Enabled = True
            End If
            cmbTahvilGirandeh.SelectedIndex = -1
            cmbTahvilGirandeh.SelectedIndex = -1

            cmbTahvilDahandeh.SelectedIndex = -1
            cmbTahvilDahandeh.SelectedIndex = -1

            cmbNameTaminKonandeh.Enabled = True

            txtShomarehSefaresh.Text = ""
            txtShomarehSefaresh.Enabled = True

            'mskAzTarikh.Text = ""
            'mskTaTarikh.Text = ""

            'txtShomarehBatch.Enabled = True

            ccShomarehBach = 0


            'txtIRC.Text = ""
            'txtGTIN.Text = ""


            'mskTarikhEngheza.Text = ""
            'mskTarikhTolid.Text = ""
            mskTarikhForm.Text = FnTarikhEmrooz()

            If objCode.CheckCompany = 24 Or objCode.CheckCompany = 1 Then
                mskTarikhForm.Enabled = False
            End If

            'txtMablaghKharid.Text = ""
            'txtShomarehBatch.Text = ""
            'txtTedadKarton.Text = ""
            'txtTedadBasteh.Text = ""
            'txtTedad.Text = ""
            txtTozihat.Text = ""

            cmbAnbar.SelectedValue = objTools.ConvertNulls(objTools.DLookup("CodeAnbar", "tblAN_Anbar", "AnbarAsly=1"), 0)

            ErrPro.Dispose()
            'Ali.na 1405/07/05
            'If AllowChangeFeePishFaktor = False Then
            '    Me.txtMablaghKharid.Enabled = False
            'End If

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->ClearForm")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->ClearForm")
        End Try
    End Sub
    Private Sub txtSearchT_TextChanged(sender As Object, e As EventArgs) Handles txtSearchT.TextChanged
        If dsForm.Tables.Contains("tblTaminKonandeh") Then

            If cmbNameTaminKonandeh.DataSource Is dsForm.Tables("tblTaminKonandeh").DefaultView Then
                Me.dsForm.Tables("tblTaminKonandeh").DefaultView.Sort = "ccTaminKonandeh"
                Me.dsForm.Tables("tblTaminKonandeh").DefaultView.RowFilter = "NameTaminKonandeh Like  '%" & txtSearchT.Text.TrimEnd & "%'"
            End If

        End If
        If txtSearchT.Text = "" Then
            cmbNameTaminKonandeh.SelectedIndex = -1
        End If
    End Sub
    Private Sub CheckIsNumeric(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles _
     txtShomarehSefaresh.KeyPress


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
    Private Sub txtShomarehSefaresh_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtShomarehSefaresh.KeyPress
        Try
            If cmbsCodeDorehSefaresh.SelectedValue = 0 Then
                MsgBox("کد دوره سفارش را وارد نمایید", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "خطا")
                cmbsCodeDorehSefaresh.Focus()
                Exit Sub
            End If

            If (Asc(e.KeyChar()) < 48 Or Asc(e.KeyChar()) > 57) And (Asc(e.KeyChar()) <> 8) Then
                e.Handled = True
            End If
            If e.KeyChar = Chr(Keys.Space) Then
                Dim objPopUp As New PopUp_Sefaresh
                Dim StrSql As String

                'StrSql = "SELECT  * "
                'StrSql &= ", isnull([dbo].[GetStrShomarehResids](qryAN_KdxSefaresh.ccKardexTitr),'') as ShomarehResids"
                'StrSql &= " FROM qryAN_KdxSefaresh Where NoeSefaresh=2 AND ccTaminKonandeh<>0 AND sVazeiat =1 AND"
                'StrSql &= " CodeMahal = " & CodeMahalFaal & " And CodeDoreh = '" & CodeDoreh & "'"

                StrSql = "    select ShomarehForm, TarikhForm, ccKardexTitr,NameTaminKonandeh,Tozihat ,ShomarehResids "
                StrSql &= " from ("
                StrSql &= " SELECT    a.ShomarehForm, b.ccKala, b.Tedad3 - sum(d.Tedad3) AS TedadMandeh,"
                StrSql &= "   f.NameTaminKonandeh, a.TarikhForm ,a.Tozihat,a.ccKardexTitr,"
                StrSql &= "    isnull([dbo].[GetStrShomarehResids](b.ccKardexTitr),'') as ShomarehResids  "
                StrSql &= "  From      tblAN_KdxSefaresh as a left outer join  "
                StrSql &= "     tblAN_KdxSefareshSatr as b on a.ccKardexTitr = b. ccKardexTitr left outer join "
                StrSql &= "    tblAN_kdxResid as c on a.ccKardexTitr = c.ccSefareshTitr left outer join "
                StrSql &= "    tblAN_kdxResidSatr as d on c.ccKardexTitr = d.ccKardexTitr and b.ccKala = d.ccKala left outer join "
                StrSql &= "  tblFO_TaminKonandeh as f on a.ccTaminKonandeh =f.ccTaminKonandeh  "
                StrSql &= " where      b.ccKala  is not null and  a.NoeSefaresh=2 AND a.ccTaminKonandeh<>0 AND a.sVazeiat =1 AND "
                StrSql &= "    a.CodeMahal =  " & CodeMahalFaal & " And a.CodeDoreh = '" & CodeDoreh & "'"
                StrSql &= "  group by    a.ShomarehForm, b.ccKala, b.Tedad3,f.NameTaminKonandeh,a.TarikhForm,a.Tozihat,a.ccKardexTitr,"
                StrSql &= "  isnull([dbo].[GetStrShomarehResids](b.ccKardexTitr),'') "
                StrSql &= "  having(b.Tedad3 - sum(d.Tedad3) > 0)"

                StrSql &= "  union all"

                StrSql &= "  SELECT  a.ShomarehForm, b.ccKala, 0 AS TedadMandeh ,f.NameTaminKonandeh,a.TarikhForm ,a.Tozihat,"
                StrSql &= "   a.ccKardexTitr,isnull([dbo].[GetStrShomarehResids](b.ccKardexTitr),'') as ShomarehResids  "
                StrSql &= "From        tblAN_KdxSefaresh as a left outer join    "
                StrSql &= " tblAN_KdxSefareshSatr as b on a.ccKardexTitr = b. ccKardexTitr left outer join "
                StrSql &= " tblAN_kdxResid as c on a.ccKardexTitr = c.ccSefareshTitr left outer join "
                StrSql &= "   tblAN_kdxResidSatr as d on c.ccKardexTitr = d.ccKardexTitr and b.ccKala = d.ccKala left outer join "
                StrSql &= "  tblFO_TaminKonandeh as f on a.ccTaminKonandeh = f.ccTaminKonandeh "

                StrSql &= " where  b.ccKala  is not null and  a.NoeSefaresh=2 AND a.ccTaminKonandeh<>0 AND a.sVazeiat =1 AND "
                StrSql &= "    a.CodeMahal =  " & CodeMahalFaal & " And a.CodeDoreh = '" & CodeDoreh & "'"
                StrSql &= " and  a.ccKardexTitr not in (select ISNULL(ccSefareshTitr,0) from tblAN_kdxResid )"
                StrSql &= " ) as Tbl"
                StrSql &= " group by ShomarehForm, TarikhForm, ccKardexTitr,NameTaminKonandeh,Tozihat,ShomarehResids "

                objPopUp.strSQL = StrSql
                objPopUp.ShowDialog()
                txtShomarehSefaresh.Text = objPopUp.ShomarehSefaresh
            End If
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->txtCodeMoshtary_KeyPress")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->txtCodeMoshtary_KeyPress")
        End Try
    End Sub
    Private Sub txtShomarehSefaresh_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtShomarehSefaresh.TextChanged
        If txtShomarehSefaresh.Text <> "0" And txtShomarehSefaresh.Text <> "" Then
            cmbNameTaminKonandeh.SelectedValue = objTools.ConvertNulls(objTools.DLookup("ccTaminKonandeh", "tblAN_KdxSefaresh", "CodeDoreh=" & cmbsCodeDorehSefaresh.SelectedValue & " AND ShomarehForm=" & txtShomarehSefaresh.Text & " AND CodeMahal = " & CodeMahalFaal & " And sVazeiat= " & 1), 0)
            If cmbNameTaminKonandeh.SelectedValue <> 0 Then
                cmbNameTaminKonandeh.Enabled = False
            Else : cmbNameTaminKonandeh.Enabled = True
            End If
        End If
        If Mode <> UD_Dll.Enums.GL_ModeForms.UpdateRecord Then
            If txtShomarehSefaresh.Text = "" Or txtShomarehSefaresh.Text = "0" Then
                cmbNameTaminKonandeh.SelectedIndex = -1
                cmbNameTaminKonandeh.SelectedIndex = -1
                cmbNameTaminKonandeh.Enabled = True
            End If
        End If
    End Sub
    Private Sub txtShomarehSefaresh_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtShomarehSefaresh.Validated
        If txtShomarehSefaresh.Text <> "0" And txtShomarehSefaresh.Text <> "" Then
            cmbNameTaminKonandeh.SelectedValue = objTools.ConvertNulls(objTools.DLookup("ccTaminKonandeh", "tblAN_KdxSefaresh", "CodeDoreh=" & cmbsCodeDorehSefaresh.SelectedValue & " AND ShomarehForm=" & txtShomarehSefaresh.Text & " AND CodeMahal = " & CodeMahalFaal), 0)
            If cmbNameTaminKonandeh.SelectedValue <> 0 Then
                cmbNameTaminKonandeh.Enabled = False
            Else : cmbNameTaminKonandeh.Enabled = True
            End If
        End If
        If Mode <> UD_Dll.Enums.GL_ModeForms.UpdateRecord Then
            If txtShomarehSefaresh.Text = "" Or txtShomarehSefaresh.Text = "0" Then
                cmbNameTaminKonandeh.SelectedIndex = -1
                cmbNameTaminKonandeh.SelectedIndex = -1
                cmbNameTaminKonandeh.Enabled = True
            End If
        End If
    End Sub
    Private Sub cmbsCodeDorehSefaresh_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbsCodeDorehSefaresh.SelectedIndexChanged
        If cmbsCodeDorehSefaresh.SelectedIndex = -1 Then
            txtShomarehSefaresh.Enabled = False
        ElseIf Mode <> UD_Dll.Enums.GL_ModeForms.UpdateRecord Then
            txtShomarehSefaresh.Enabled = True
        End If
    End Sub

    Private Sub btnSaveSanad_Click(sender As Object, e As EventArgs) Handles btnSaveSanad.Click
        If ccKardexTitr <> 0 Then Exit Sub       ' جلوگیری از ذخیره‌ی دوباره
        If Not AddNewRecord() Then Exit Sub

        If tCodeCounter <= 0 Then
            MessageBox.Show("ذخیره‌ی هدر انجام نشد")
            Exit Sub
        End If

        ccKardexTitr = CInt(tCodeCounter)

        ' بعد از ثبت هدر، این‌ها نباید عوض شوند (قیمت و بچ بر اساس تامین‌کننده می‌آید)
        cmbNameTaminKonandeh.Enabled = False
        txtSearchT.Enabled = False
        cmbAnbar.Enabled = False
        cmbsCodeDorehSefaresh.Enabled = False
        txtShomarehSefaresh.Enabled = False
        mskTarikhForm.Enabled = False

        SearchSatr()
        btnSaveSanad.Enabled = False

        GridEXSatr.Enabled = True
        GridEXSatr.Focus()
        GridEXSatr.MoveToNewRecord()
        GridEXSatr.Col = 0
    End Sub

    Private Function AddNewRecord() As Boolean
        Try
            AddNewRecord = False

            If Not IsValidBeforSaveTitr("All") Then
                Exit Function
            End If

            Dim ccKardexTitrSefaresh As Integer = 0

            Using cn As New SqlConnection(ConnectionString)
                Using cm As SqlCommand = cn.CreateCommand()

                    cn.Open()

                    cm.Parameters.Clear()
                    cm.CommandType = CommandType.StoredProcedure
                    cm.CommandText = "[dbo].[spAN_KdxResid_Insert]"
                    cm.Parameters.AddWithValue("CodeMahal", CodeMahalFaal)
                    cm.Parameters.AddWithValue("CodeDoreh", CodeDoreh)
                    cm.Parameters.AddWithValue("ccAnbar", If(IsNothing(cmbAnbar.SelectedValue), 0, cmbAnbar.SelectedValue))
                    cm.Parameters.AddWithValue("TarikhForm", mskTarikhForm.Text)
                    cm.Parameters.AddWithValue("Tozihat", txtTozihat.Text)
                    cm.Parameters.AddWithValue("sVazeiat", UD_Dll.Enums.AN_VazeiatKDXResid.BedoneAmalyat)
                    cm.Parameters.AddWithValue("CodeDorehSefaresh", If(IsNothing(cmbsCodeDorehSefaresh.SelectedValue), CodeDoreh, cmbsCodeDorehSefaresh.SelectedValue))
                    cm.Parameters.AddWithValue("ShomarehSefaresh", If(txtShomarehSefaresh.Text.Trim = "", 0, txtShomarehSefaresh.Text.Trim))
                    cm.Parameters.AddWithValue("ccTaminKonandeh", If(IsNothing(cmbNameTaminKonandeh.SelectedValue), 0, cmbNameTaminKonandeh.SelectedValue))
                    cm.Parameters.AddWithValue("NoeResid", 3)
                    cm.Parameters.AddWithValue("ccMashin", DBNull.Value)
                    cm.Parameters.AddWithValue("ccMarkazPakhsh", CodeMahalFaal)
                    cm.Parameters.AddWithValue("NameFardTahvilGirandeh", cmbTahvilGirandeh.Text)
                    cm.Parameters.AddWithValue("NameFardTahvilDahandeh", cmbTahvilDahandeh.Text)
                    cm.Parameters.AddWithValue("Ranandeh", If(txtRanandeh.Text = "", DBNull.Value, txtRanandeh.Text))
                    cm.Parameters.AddWithValue("Khodro", If(txtKhodro.Text = "", DBNull.Value, txtKhodro.Text))
                    cm.Parameters.AddWithValue("Barnameh", If(txtBarnameh.Text = "", DBNull.Value, txtBarnameh.Text))
                    cm.Parameters.AddWithValue("Mobile", If(txtShomarehMobile.Text = "", DBNull.Value, txtShomarehMobile.Text))
                    cm.Parameters.AddWithValue("UserName", UserName)
                    cm.Parameters.AddWithValue("Tarikh", FnTarikhEmrooz())
                    cm.Parameters.AddWithValue("Saat", Format(TimeOfDay, "HH:mm:ss"))
                    Dim dt As New DataTable

                    Using da As New SqlDataAdapter(cm)
                        da.Fill(dt)
                    End Using

                    If dt.Rows.Count > 0 Then
                        tCodeCounter = Convert.ToInt32(dt.Rows(0)("tCodeCounter"))

                        If IsDBNull(dt.Rows(0)("ccKardexTitrSefaresh")) Then
                            ccKardexTitrSefaresh = 0
                        Else
                            ccKardexTitrSefaresh = Convert.ToInt32(dt.Rows(0)("ccKardexTitrSefaresh"))
                        End If
                    End If
                End Using
            End Using





            ' Insert Satr Kala From Sefaresh
            If ccKardexTitrSefaresh > 0 AndAlso
           Not IsNothing(cmbsCodeDorehSefaresh.SelectedValue) AndAlso
           txtShomarehSefaresh.Text <> "" Then

                flgInsertSatrSefaresh = True

                If MsgBox("آیا مایلید کالاهای موجود در سفارش به رسید اضافه شود؟", MsgBoxStyle.YesNo + MsgBoxStyle.Question + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.DefaultButton2, "تایید") = MsgBoxResult.Yes Then

                    CreateSatrFromSefaresh(tCodeCounter, ccKardexTitrSefaresh, cmbNameTaminKonandeh.SelectedValue)

                End If
            End If

            objCode.SabteTaghirat(CodeMahalFaal, UD_Dll.Enums.GL_NoeTaghir.AddNewRecord, "tblAN_KdxResid", tCodeCounter, tCodeCounter, "ذخیره")
            Mode = UD_Dll.Enums.GL_ModeForms.None

            Return True

        Catch sqlEx As SqlException

            If sqlEx.ErrorCode = -2146232060 Then

                MsgBox("این شماره سفارش در سیستم موجود نمی باشد.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "خطا در عملیات ذخیره سازی")
            Else

                MsgBox(sqlEx.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "Error IN ---->AddNewRecord")

            End If

        Catch ex As Exception

            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, "Error IN ---->AddNewRecord")
        End Try
    End Function

    Private Sub CreateSatrFromSefaresh(ByVal ccKardexTitr As Integer, ByVal ccKardexTitrSefaresh As Integer, ByVal ccTaminkonandeh As Integer)
        Try
            Dim cnSQL As SqlConnection
            Dim cmSQL As SqlCommand
            Dim strSQL As String

            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()

            strSQL = "Insert Into tblAN_kdxResidSatr "
            strSQL &= " (ccKardexTitr,Radif,ccKala,Tedad1,Tedad2,Tedad3,MablaghKharid,Tedad0,TedadKarton,TedadBasteh) "
            'RANK()
            strSQL &= " Select " & ccKardexTitr & " as ccKardexTitr, ROW_NUMBER() OVER (PARTITION BY 1 order by ccKala) as Radif,ccKala ,"
            strSQL &= " Tedad3-isnull((select sum(tedad3) from dbo.qryAN_kdxResidTitrSatr where cckala =qryAN_KdxSefareshSatr.ccKala"
            strSQL &= " and ccSefareshTitr=" & ccKardexTitrSefaresh & " ),0) as Tedad1,"
            strSQL &= " Tedad3-isnull((select sum(tedad3) from dbo.qryAN_kdxResidTitrSatr where cckala =qryAN_KdxSefareshSatr.ccKala"
            strSQL &= " and ccSefareshTitr=" & ccKardexTitrSefaresh & " ),0) as Tedad2,"
            strSQL &= " Tedad3-isnull((select sum(tedad3) from dbo.qryAN_kdxResidTitrSatr where cckala =qryAN_KdxSefareshSatr.ccKala"
            strSQL &= " and ccSefareshTitr=" & ccKardexTitrSefaresh & " ),0) as Tedad3,"
            strSQL &= " isnull((select dbo.[GetMablaghKharid](qryAN_KdxSefareshSatr.ccKala,'" & FnTarikhEmrooz() & "'," & CodeMahalFaal & "," & ccTaminkonandeh & ")),0) as MablaghKharid "
            strSQL &= " ,TedadKhord,TedadKarton,TedadBasteh"
            strSQL &= " From qryAN_KdxSefareshSatr Where ccKardexTitr = " & ccKardexTitrSefaresh
            strSQL &= " and "
            strSQL &= " (Tedad3-isnull((select sum(tedad3) from dbo.qryAN_kdxResidTitrSatr where cckala =qryAN_KdxSefareshSatr.ccKala"
            strSQL &= " and ccSefareshTitr=" & ccKardexTitrSefaresh
            strSQL &= " ),0)) > 0"

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.ExecuteNonQuery()

            cnSQL.Close()
            cmSQL = Nothing : cnSQL = Nothing

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->CreateSatrHavalehTafkikAnbar")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->CreateSatrHavalehTafkikAnbar")
        End Try
    End Sub

    Private Function IsValidBeforSaveTitr(ByVal CheckField As String) As Boolean
        Try
            IsValidBeforSaveTitr = False
            If CheckField = "mskTarikhForm" Or CheckField = "All" Then
                If istarikhmiladi Then
                    If Len(mskTarikhForm.Text.ToString) <> 0 Then
                        If Not objTarikh.IsMiDate(mskTarikhForm.Text.ToString) Then
                            mskTarikhForm.Focus()
                            Exit Function
                        End If
                        If mskTarikhForm.Text.Substring(0, 4) <> CodeDoreh Then
                            MsgBox("تاريخ با دوره مالی فعال يکی نيست.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
                            mskTarikhForm.Focus()
                            Exit Function
                        End If
                    Else
                        ErrPro.SetError(Me.mskTarikhForm, "تاريخ را وارد کنيد.")
                        MsgBox("تاريخ را وارد کنيد.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
                        mskTarikhForm.Focus()
                        Exit Function
                    End If
                    ErrPro.SetError(Me.mskTarikhForm, "")

                Else

                    If Len(mskTarikhForm.Text.ToString) <> 0 Then
                        If Not objTarikh.IsShDate(mskTarikhForm.Text.ToString) Then
                            mskTarikhForm.Focus()
                            Exit Function
                        End If
                        If mskTarikhForm.Text.Substring(0, 4) <> CodeDoreh Then
                            MsgBox("تاريخ با دوره مالی فعال يکی نيست.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
                            mskTarikhForm.Focus()
                            Exit Function
                        End If
                    Else
                        ErrPro.SetError(Me.mskTarikhForm, "تاريخ را وارد کنيد.")
                        MsgBox("تاريخ را وارد کنيد.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
                        mskTarikhForm.Focus()
                        Exit Function
                    End If
                    ErrPro.SetError(Me.mskTarikhForm, "")

                End If





            End If


            If CheckField = "cmbNameTaminKonandeh" Or CheckField = "All" Then
                If Me.cmbNameTaminKonandeh.SelectedIndex = -1 Then
                    ErrPro.SetError(Me.cmbNameTaminKonandeh, "تامین کننده را انتخاب کنید.")
                    MsgBox("تامین کننده را انتخاب کنید.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
                    cmbNameTaminKonandeh.Focus()
                    Exit Function
                End If
                ErrPro.SetError(Me.cmbNameTaminKonandeh, "")
            End If

            If CheckField = "cmbAnbar" Or CheckField = "All" Then
                If Me.cmbAnbar.Text = "" Then
                    ErrPro.SetError(Me.cmbAnbar, "انبار را انتخاب کنید.")
                    MsgBox("انبار را انتخاب کنید.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
                    cmbAnbar.Focus()
                    Exit Function
                End If
                ErrPro.SetError(Me.cmbAnbar, "")
            End If

            Return True
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->IsValidBeforSaveTitr")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->IsValidBeforSaveTitr")
        End Try
    End Function

    Private Sub GridEXSatr_AddingRecord(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles GridEXSatr.AddingRecord


        If Not SaveRow(GridEXSatr.CurrentRow, True) Then e.Cancel = True
        GridEXSatr.Col = GridEXSatr.RootTable.Columns("CodeKala").Position
    End Sub
    Private Function SaveRow(row As Janus.Windows.GridEX.GridEXRow, isNew As Boolean) As Boolean
        If row Is Nothing Then Return False
        If isNew AndAlso IsRowEmpty(row) Then Return False
        If Not ValidateRow(row) Then Return False


        'Dim ccKala As Integer = Convert.ToInt32(row.Cells("ccKala").Value)
        '    Dim t0 As Double, price As Double
        '    TryGetNumber(row, "Tedad0", t0)
        '    TryGetNumber(row, "MablaghKharid", price)
        '    Dim tedad3 As Double = objCode.ReturnTedad(ccKala, t0, 0, 0)
        '    'Dim ccKala As Integer = Convert.ToInt32(row.Cells("ccKala").Value)
        '    'Dim t0 As Double = Val(row.Cells("Tedad3").Value)
        '    Dim tBasteh As Double = 0
        '    Dim tKarton As Double = 0

        'Dim tedad3 As Double = objCode.ReturnTedad(ccKala, t0, tBasteh, tKarton)
        'Dim tBasteh As Double = Val(row.Cells("TedadBasteh").Value)
        'Dim tKarton As Double = Val(row.Cells("TedadKarton").Value)
        'Dim tedad3 As Double = objCode.ReturnTedad(ccKala, t0, tBasteh, tKarton)




        Try
                Dim ccKala As Integer = Convert.ToInt32(row.Cells("ccKala").Value)
                Dim tedad3, price As Double
                TryGetNumber(row, "Tedad3", tedad3)
            TryGetNumber(row, "MablaghKharid", price)
            Dim jayezeh As Double = 0
            TryGetNumber(row, "TedadJayezeh", jayezeh)

            Using con As New SqlConnection(ConnectionString)
                    Using cmd As SqlCommand = con.CreateCommand()
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.CommandText = "[dbo].[spAN_KdxResid_InsertSatr]"
                    cmd.Parameters.AddWithValue("@ccKardexTitr", ccKardexTitr)
                    cmd.Parameters.AddWithValue("@ccKala", ccKala)
                    cmd.Parameters.AddWithValue("@Tedad3", tedad3)
                    cmd.Parameters.AddWithValue("@TedadJayezeh", jayezeh)
                    cmd.Parameters.AddWithValue("@MablaghKharid", price)
                    cmd.Parameters.AddWithValue("@ShomarehBatch", ToDbStr(row.Cells("ShomarehBatch").Value))
                    cmd.Parameters.AddWithValue("@TarikhTolid", ToMiladi(row.Cells("TarikhTolid").Value))
                    cmd.Parameters.AddWithValue("@TarikhEngheza", ToMiladi(row.Cells("TarikhEngheza").Value))
                    cmd.Parameters.AddWithValue("@IRC", Convert.ToString(row.Cells("IRC").Value).Trim())
                    cmd.Parameters.AddWithValue("@GTIN", Convert.ToString(row.Cells("GTIN").Value).Trim())
                    cmd.Parameters.AddWithValue("@UserName", UserName)
                    Dim outId As New SqlParameter("@ccKardexSatr", SqlDbType.Int) With {.Direction = ParameterDirection.Output}
                    cmd.Parameters.Add(outId)
                    con.Open()
                    cmd.ExecuteNonQuery()
                End Using
                End Using
                Return True
            Catch ex As Exception
                MessageBox.Show(ex.Message)
                Return False
            End Try

    End Function

    Private Function ToDbStr(v As Object) As Object
        If v Is Nothing OrElse IsDBNull(v) OrElse v.ToString().Trim() = "" Then Return DBNull.Value
        Return v.ToString().Trim()
    End Function



    Private Function IsRowEmpty(row As Janus.Windows.GridEX.GridEXRow) As Boolean
        For Each key As String In New String() {"CodeKala", "ccKala", "Tedad3"}
            Dim v As Object = row.Cells(key).Value
            If v IsNot Nothing AndAlso Not IsDBNull(v) Then
                If Convert.ToString(v).Trim().Length > 0 Then Return False
            End If
        Next
        Return True
    End Function
    Private Function ValidateRow(ByVal row As Janus.Windows.GridEX.GridEXRow) As Boolean
        Dim num As Double

        Dim ccKala As Object = row.Cells("ccKala").Value
        If ccKala Is Nothing OrElse IsDBNull(ccKala) OrElse ccKala.ToString().Trim() = "" Then
            MessageBox.Show("کالا را انتخاب کنید")
            FocusCol("CodeKala")
            Return False
        End If

        If Not TryGetNumber(row, "Tedad3", num) OrElse num <= 0 Then
            MessageBox.Show("تعداد را وارد کنید")
            FocusCol("Tedad3")
            Return False
        End If

        If Not TryGetNumber(row, "MablaghKharid", num) OrElse num <= 0 Then
            MessageBox.Show("مبلغ خرید را وارد کنید")
            FocusCol("MablaghKharid")
            Return False
        End If

        Dim sb As Object = row.Cells("ShomarehBatch").Value
        If sb Is Nothing OrElse IsDBNull(sb) OrElse sb.ToString().Trim() = "" Then
            MessageBox.Show("شماره بچ را وارد کنید")
            FocusCol("ShomarehBatch")
            Return False
        End If

        Dim tolid As String = "", engheza As String = ""

        If Not TryToMiladi(row.Cells("TarikhTolid").Value, tolid) Then
            MessageBox.Show("تاریخ تولید خالی یا نامعتبر است")
            FocusCol("TarikhTolid")
            Return False
        End If

        If Not TryToMiladi(row.Cells("TarikhEngheza").Value, engheza) Then
            MessageBox.Show("تاریخ انقضا خالی یا نامعتبر است")
            FocusCol("TarikhEngheza")
            Return False
        End If

        If String.CompareOrdinal(engheza, tolid) <= 0 Then
            MessageBox.Show("تاریخ انقضا باید بعد از تاریخ تولید باشد")
            FocusCol("TarikhEngheza")
            Return False
        End If

        ' تعداد جایزه (اختیاری): نباید منفی باشد یا از تعداد کل بیشتر
        Dim jayezeh As Double = 0
        Dim jv As Object = row.Cells("TedadJayezeh").Value
        If jv IsNot Nothing AndAlso Not IsDBNull(jv) AndAlso jv.ToString().Trim() <> "" Then
            If Not Double.TryParse(jv.ToString().Trim(), jayezeh) OrElse jayezeh < 0 Then
                MessageBox.Show("تعداد جایزه نامعتبر است")
                FocusCol("TedadJayezeh")
                Return False
            End If
        End If

        Dim totalTedad As Double = 0
        TryGetNumber(row, "Tedad3", totalTedad)
        If jayezeh > totalTedad Then
            MessageBox.Show("تعداد جایزه از تعداد کالا نباید بیشتر باشد.")
            FocusCol("TedadJayezeh")
            Return False
        End If


        Return True
    End Function

    Private Function TryGetNumber(ByVal row As Janus.Windows.GridEX.GridEXRow,
                                  ByVal colKey As String, ByRef result As Double) As Boolean
        result = 0
        Dim v As Object = row.Cells(colKey).Value
        If v Is Nothing OrElse IsDBNull(v) Then Return False
        If v.ToString().Trim().Length = 0 Then Return False
        Return Double.TryParse(v.ToString().Trim(), result)
    End Function

    Private Sub FocusCol(ByVal colKey As String)
        GridEXSatr.Col = GridEXSatr.RootTable.Columns(colKey).Position
    End Sub
    Private Sub GridEXSatr_CellUpdated(ByVal sender As Object,
    ByVal e As Janus.Windows.GridEX.ColumnActionEventArgs) _
  Handles GridEXSatr.CellUpdated

        If _isCalculating Then Exit Sub
        If e.Column Is Nothing OrElse GridEXSatr.CurrentRow Is Nothing Then Exit Sub
        If GridEXSatr.CurrentRow.RowType <> Janus.Windows.GridEX.RowType.NewRecord Then Exit Sub

        Select Case e.Column.Key
            Case "CodeKala"
                FillKalaFromCode(GridEXSatr.CurrentRow)
            Case "Tedad3", "TedadJayezeh", "MablaghKharid"
                CalcRow(GridEXSatr.CurrentRow)
            Case "ShomarehBatch"
                ApplyBachPrice(GridEXSatr.CurrentRow)
        End Select
    End Sub

    Private Sub CalcRow(ByVal row As Janus.Windows.GridEX.GridEXRow)
        If row Is Nothing Then Exit Sub

        Dim tedad, jayezeh, fee As Double
        TryGetNumber(row, "Tedad3", tedad)
        TryGetNumber(row, "TedadJayezeh", jayezeh)
        TryGetNumber(row, "MablaghKharid", fee)

        _isCalculating = True
        Try
            ' جایزه جزو مبلغ فاکتور نیست
            row.Cells("MablaghKol").Value = Math.Max(tedad - jayezeh, 0) * fee
        Finally
            _isCalculating = False
        End Try
    End Sub

    Private Sub GridEXSatr_EditingCell(sender As Object, e As Janus.Windows.GridEX.EditingCellEventArgs) _
Handles GridEXSatr.EditingCell
        ' فقط ردیف جدید قابل ویرایشه
        If GridEXSatr.CurrentRow IsNot Nothing AndAlso
       GridEXSatr.CurrentRow.RowType <> Janus.Windows.GridEX.RowType.NewRecord Then
            e.Cancel = True
        End If
    End Sub

    Private Sub GridEXSatr_UpdatingRecord(sender As Object, e As System.ComponentModel.CancelEventArgs) _
    Handles GridEXSatr.UpdatingRecord
        'If Not SaveRow(GridEXSatr.CurrentRow, False) Then e.Cancel = True
        e.Cancel = True   ' ویرایش نداریم
    End Sub
    Private Sub SearchSatr()
        Using cn As New SqlConnection(ConnectionString)
            Using cm As SqlCommand = cn.CreateCommand()
                cm.CommandType = CommandType.StoredProcedure
                cm.CommandText = "[dbo].[spAN_KdxResid_SearchSatr]"
                cm.Parameters.AddWithValue("@ccKardexTitr", ccKardexTitr)
                Dim da As New SqlDataAdapter(cm)
                dt_SearchSatr = New DataTable
                da.Fill(dt_SearchSatr)
            End Using
        End Using
        SetGridSatr()
    End Sub

    Private Sub GridEXSatr_RecordAdded(sender As Object, e As EventArgs) Handles GridEXSatr.RecordAdded
        BeginInvoke(New MethodInvoker(AddressOf ReloadSatr))
    End Sub

    Private Sub ReloadSatr()
        SearchSatr()
        GridEXSatr.MoveToNewRecord()
        GridEXSatr.Col = 0
        GridEXSatr.Focus()
    End Sub
    Private Sub SetCol(ByVal key As String, ByVal caption As String, ByVal width As Integer,
                   ByVal pos As Integer, ByVal editable As Boolean,
                   Optional ByVal fmt As String = "")
        Dim c As Janus.Windows.GridEX.GridEXColumn = GridEXSatr.RootTable.Columns(key)
        c.Caption = caption
        c.Visible = True
        c.Width = width
        c.Position = pos
        c.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
        c.EditType = If(editable, Janus.Windows.GridEX.EditType.TextBox, Janus.Windows.GridEX.EditType.NoEdit)
        If fmt <> "" Then c.FormatString = fmt
    End Sub

    Private Sub SetGridSatr()
        Try
            With GridEXSatr
                .SetDataBinding(dt_SearchSatr.DefaultView, "")
                If Not _gridStructureReady Then
                    .RetrieveStructure()
                    _gridStructureReady = True
                    .UpdateMode = Janus.Windows.GridEX.UpdateMode.RowUpdate
                    .AllowEdit = Janus.Windows.GridEX.InheritableBoolean.True
                    .AllowAddNew = Janus.Windows.GridEX.InheritableBoolean.True
                    .AllowDelete = Janus.Windows.GridEX.InheritableBoolean.True
                    .TabKeyBehavior = Janus.Windows.GridEX.TabKeyBehavior.ColumnNavigation
                End If
            End With

            For Each c As Janus.Windows.GridEX.GridEXColumn In GridEXSatr.RootTable.Columns
                c.Visible = False
            Next

            'SetCol("Radif", "ردیف", 40, 1, False)
            'SetCol("CodeKala", "کد کالا", 70, 2, True)
            'SetCol("NameKala", "نام کالا", 230, 3, False)
            'SetCol("ShomarehBatch", "شماره بچ", 100, 4, True)
            'SetCol("Tedad3", "تعداد", 70, 5, True, "###,###.##")
            'SetCol("MablaghKharid", "مبلغ خرید", 90, 6, True, "###,###")
            'SetCol("MablaghKol", "مبلغ کل", 100, 7, False, "###,###")
            'SetCol("TarikhTolid", "تاریخ تولید", 90, 8, True)
            'SetCol("TarikhEngheza", "تاریخ انقضا", 90, 9, True)
            'SetCol("IRC", "IRC", 100, 10, True)
            'SetCol("GTIN", "GTIN", 100, 11, True)

            SetCol("Radif", "ردیف", 40, 1, False)
            SetCol("CodeKala", "کد کالا", 70, 2, True)
            SetCol("NameKala", "نام کالا", 230, 3, False)
            SetCol("Tedad3", "تعداد ", 70, 4, True, "###,###.##")
            SetCol("TedadJayezeh", "تعداد جایزه", 70, 5, True, "###,###.##")
            SetCol("MablaghKharid", "مبلغ خرید", 90, 6, True, "###,###")
            SetCol("MablaghKol", "مبلغ کل", 100, 7, False, "###,###")
            SetCol("IsJayezeh", "جایزه", 45, 8, False)
            SetCol("ShomarehBatch", "شماره بچ", 100, 9, True)
            SetCol("TarikhTolid", "تاریخ تولید", 90, 10, True)
            SetCol("TarikhEngheza", "تاریخ انقضا", 90, 11, True)
            SetCol("IRC", "IRC", 100, 12, True)
            SetCol("GTIN", "GTIN", 100, 13, True)


        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error in SetGridSatr")
        End Try
    End Sub
    Private Sub GridEXSatr_KeyDown(sender As Object, e As KeyEventArgs) Handles GridEXSatr.KeyDown
        If e.KeyCode <> Keys.F2 AndAlso e.KeyCode <> Keys.F5 Then Exit Sub
        If GridEXSatr.CurrentRow Is Nothing OrElse GridEXSatr.CurrentColumn Is Nothing Then Exit Sub
        If GridEXSatr.CurrentRow.RowType <> Janus.Windows.GridEX.RowType.NewRecord Then Exit Sub

        ' ---------- F2 : کالا ----------
        If e.KeyCode = Keys.F2 AndAlso GridEXSatr.CurrentColumn.Key = "CodeKala" Then
            Dim objKala As New Forms_dll.frmAN_KalaSearch
            Dim StrSqlKala As String = "Select CodeKala,NameKala,ccKala,txtsVahedeShomaresh,sVahedeShomaresh,NameBrand,RadifBrand,0 as IsSabadKala " &
                                       " from qryAN_Kala Where Faal = 1"
            MultiSelection = False
            SearchItem = "CodeKala"
            objKala.SetForm(StrSqlKala)
            objKala.ShowDialog()
            If Val(objKala.tccKala) = 0 Then Exit Sub

            With GridEXSatr.CurrentRow
                '.Cells("CodeKala").Value = objKala.tcodeKala
                '.Cells("ccKala").Value = objKala.tccKala
                '.Cells("NameKala").Value = objKala.tNameKala
                '.Cells("MablaghKharid").Value = GetMablaghKharid(CInt(objKala.tccKala))
                '' با عوض شدن کالا، بچ قبلی معتبر نیست
                '.Cells("ShomarehBatch").Value = DBNull.Value
                '.Cells("TarikhTolid").Value = DBNull.Value
                '.Cells("TarikhEngheza").Value = DBNull.Value
                '.Cells("IRC").Value = DBNull.Value
                '.Cells("GTIN").Value = DBNull.Value
                FillKalaRow(GridEXSatr.CurrentRow, CInt(objKala.tccKala), objKala.tcodeKala, objKala.tNameKala)
            End With
            MultiSelection = False
            e.Handled = True
            Exit Sub
        End If

        ' ---------- F5 : شماره بچ ----------
        If e.KeyCode = Keys.F5 AndAlso GridEXSatr.CurrentColumn.Key = "ShomarehBatch" Then
            Dim ccKalaObj As Object = GridEXSatr.CurrentRow.Cells("ccKala").Value
            If ccKalaObj Is Nothing OrElse IsDBNull(ccKalaObj) OrElse Val(ccKalaObj) = 0 Then
                MessageBox.Show("ابتدا کالا را انتخاب کنید")
                FocusCol("CodeKala")
                Exit Sub
            End If

            Dim objShomarehBach As New Forms_dll.ShomarehBachSearch
            Dim StrSql As String
            StrSql = "Select ShomarehBach,TarikhTolidSlash,TarikhEnghezaSlash,ccShomarehBach, "
            StrSql &= " NameTaminKonandeh,IsNull(NameTolidKonandeh,'---'),NameKala"
            StrSql &= " ,(SELECT [dbo].[fnAN_GetMojodiDarHalForosh_bachTarikh] (" & cmbAnbar.SelectedValue & ", Q.ccKala, Q.TarikhTolid, Q.TarikhEngheza, " & CodeMahalFaal & ", " & CodeDoreh & ", " & TarikhEmrooz & ", replace(Q.ShomarehBach,'',''))) As MojodiDarHalForoshBach, IRC, GTIN "
            StrSql &= " From QryGL_ShomarehBach as Q "
            StrSql &= " Where ccKala = " & Val(ccKalaObj)
            If Not IsNothing(cmbNameTaminKonandeh.SelectedValue) Then
                StrSql &= " And ccTaminKonandeh = " & cmbNameTaminKonandeh.SelectedValue
            End If

            objShomarehBach.SearchItem = "ShomarehBach"
            objShomarehBach.SetForm(StrSql)
            objShomarehBach.ShowDialog()

            If String.IsNullOrEmpty(objShomarehBach.tShomarehBach) Then Exit Sub

            With GridEXSatr.CurrentRow
                .Cells("ShomarehBatch").Value = objShomarehBach.tShomarehBach
                .Cells("TarikhTolid").Value = objShomarehBach.tTarikhTolid
                .Cells("TarikhEngheza").Value = objShomarehBach.tTarikhEngheza
                .Cells("IRC").Value = objShomarehBach.tiRC
                .Cells("GTIN").Value = objShomarehBach.tGTIN
            End With
            e.Handled = True
            ApplyBachPrice(GridEXSatr.CurrentRow)
        End If
    End Sub
    ' قیمت بر اساس شماره بچ. اگر نبود 0 برمی‌گرداند
    Private Function GetMablaghKharidBach(ByVal ccKala As Integer, ByVal shomarehBach As String) As Double
        Using cn As New SqlConnection(ConnectionString)
            Using cm As New SqlCommand("SELECT dbo.GetMablaghKharid_Bach(@ccKala,@Tarikh,@CodeMahal,@ccTamin,@Bach)", cn)
                cm.Parameters.AddWithValue("@ccKala", ccKala)
                cm.Parameters.AddWithValue("@Tarikh", FnTarikhEmrooz().Replace("/", ""))
                cm.Parameters.AddWithValue("@CodeMahal", CodeMahalFaal)
                cm.Parameters.AddWithValue("@ccTamin", If(IsNothing(cmbNameTaminKonandeh.SelectedValue), 0, cmbNameTaminKonandeh.SelectedValue))
                cm.Parameters.AddWithValue("@Bach", shomarehBach)
                cn.Open()
                Dim r As Object = cm.ExecuteScalar()
                If r Is Nothing OrElse IsDBNull(r) Then Return 0
                Return Convert.ToDouble(r)
            End Using
        End Using
    End Function

    ' اگر برای این بچ قیمت داشتیم، در سطر بگذار و مبلغ کل را حساب کن
    Private Sub ApplyBachPrice(ByVal row As Janus.Windows.GridEX.GridEXRow)
        If row Is Nothing Then Exit Sub

        Dim ccKalaObj As Object = row.Cells("ccKala").Value
        Dim bach As String = Convert.ToString(row.Cells("ShomarehBatch").Value).Trim()
        If ccKalaObj Is Nothing OrElse IsDBNull(ccKalaObj) OrElse bach = "" Then Exit Sub

        Dim price As Double = GetMablaghKharidBach(CInt(ccKalaObj), bach)

        If price > 0 Then
            _isCalculating = True
            Try
                row.Cells("MablaghKharid").Value = price
            Finally
                _isCalculating = False
            End Try
        End If

        CalcRow(row)
    End Sub

    Private Sub FillKalaRow(ByVal row As Janus.Windows.GridEX.GridEXRow, ByVal ccKala As Integer,
                        ByVal codeKala As Object, ByVal nameKala As Object)
        _isCalculating = True
        Try
            row.Cells("CodeKala").Value = codeKala
            row.Cells("ccKala").Value = If(ccKala > 0, CObj(ccKala), DBNull.Value)
            row.Cells("NameKala").Value = nameKala
            row.Cells("MablaghKharid").Value = 0     ' قیمت فقط از روی شماره بچ می‌آید
            'row.Cells("MablaghKharid").Value = If(ccKala > 0, GetMablaghKharid(ccKala), 0)
            ' با عوض شدن کالا، بچ قبلی معتبر نیست
            row.Cells("ShomarehBatch").Value = DBNull.Value
            row.Cells("TarikhTolid").Value = DBNull.Value
            row.Cells("TarikhEngheza").Value = DBNull.Value
            row.Cells("IRC").Value = DBNull.Value
            row.Cells("GTIN").Value = DBNull.Value
        Finally
            _isCalculating = False
        End Try
        CalcRow(row)
    End Sub

    ' قیمت بر اساس کالا (بدون بچ)
    Private Function GetMablaghKharid(ByVal ccKala As Integer) As Double
        Using cn As New SqlConnection(ConnectionString)
            Using cm As New SqlCommand("SELECT dbo.GetMablaghKharid(@ccKala,@Tarikh,@CodeMahal,@ccTamin)", cn)
                cm.Parameters.AddWithValue("@ccKala", ccKala)
                cm.Parameters.AddWithValue("@Tarikh", FnTarikhEmrooz())
                cm.Parameters.AddWithValue("@CodeMahal", CodeMahalFaal)
                cm.Parameters.AddWithValue("@ccTamin", If(IsNothing(cmbNameTaminKonandeh.SelectedValue), 0, cmbNameTaminKonandeh.SelectedValue))
                cn.Open()
                Dim r As Object = cm.ExecuteScalar()
                If r Is Nothing OrElse IsDBNull(r) Then Return 0
                Return Convert.ToDouble(r)
            End Using
        End Using
    End Function
    Private Sub FillKalaFromCode(ByVal row As Janus.Windows.GridEX.GridEXRow)
        Dim code As String = Convert.ToString(row.Cells("CodeKala").Value).Trim()
        Dim codeNum As Long

        If code = "" OrElse Not Long.TryParse(code, codeNum) Then
            FillKalaRow(row, 0, code, DBNull.Value)
            Exit Sub
        End If

        Dim ccKala As Integer = 0
        Dim nameKala As Object = DBNull.Value

        Using cn As New SqlConnection(ConnectionString)
            Using cm As New SqlCommand("SELECT TOP 1 ccKala, NameKala FROM qryAN_Kala WHERE Faal = 1 AND CodeKala = @c", cn)
                cm.Parameters.AddWithValue("@c", codeNum)
                cn.Open()
                Using rd As SqlDataReader = cm.ExecuteReader()
                    If rd.Read() Then
                        ccKala = Convert.ToInt32(rd("ccKala"))
                        nameKala = rd("NameKala")
                    End If
                End Using
            End Using
        End Using

        FillKalaRow(row, ccKala, code, nameKala)
    End Sub
    ' تاریخ را به میلادی و بدون / تبدیل می‌کند. اگر نامعتبر بود False برمی‌گرداند


    Private Sub GridEXSatr_DeletingRecord(sender As Object, e As Janus.Windows.GridEX.RowActionCancelEventArgs) _
Handles GridEXSatr.DeletingRecord

        ' همیشه Cancel می‌کنیم؛ حذف و رفرش را خودمان انجام می‌دهیم
        e.Cancel = True

        Dim row As Janus.Windows.GridEX.GridEXRow = GridEXSatr.CurrentRow
        If row Is Nothing OrElse row.RowType <> Janus.Windows.GridEX.RowType.Record Then Exit Sub

        Dim id As Object = row.Cells("ccKardexSatr").Value
        If id Is Nothing OrElse IsDBNull(id) Then Exit Sub

        If MessageBox.Show("آیا از حذف این ردیف مطمئن هستید؟",
                           "حذف", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                           MessageBoxDefaultButton.Button2,
                           MessageBoxOptions.RightAlign Or MessageBoxOptions.RtlReading) <> DialogResult.Yes Then
            Exit Sub
        End If

        Try
            Using con As New SqlConnection(ConnectionString)
                Using cmd As SqlCommand = con.CreateCommand()
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.CommandText = "[dbo].[spAN_KdxResid_DeleteSatr]"
                    cmd.Parameters.AddWithValue("@ccKardexSatr", id)
                    con.Open()
                    cmd.ExecuteNonQuery()
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        ' ردیف‌ها دوباره شماره‌گذاری می‌شوند، پس لیست را دوباره لود کن
        BeginInvoke(New MethodInvoker(AddressOf ReloadSatr))
    End Sub

    '' تاریخ را به ۸ رقم تمیز می‌کند (بدون تبدیل). تبدیل شمسی به میلادی در SP است.
    'Private Function TryDate8(ByVal v As Object, ByRef result As String) As Boolean
    '    result = ""
    '    If v Is Nothing OrElse IsDBNull(v) Then Return False

    '    Dim d As String = v.ToString().Trim().Replace("/", "").Replace("-", "").Replace(" ", "")
    '    If Not System.Text.RegularExpressions.Regex.IsMatch(d, "^\d{8}$") Then Return False

    '    Dim y As Integer = CInt(d.Substring(0, 4))
    '    Dim m As Integer = CInt(d.Substring(4, 2))
    '    Dim dd As Integer = CInt(d.Substring(6, 2))

    '    If y >= 2000 Then
    '        ' میلادی
    '        Dim t As DateTime
    '        If Not DateTime.TryParseExact(d, "yyyyMMdd",
    '                System.Globalization.CultureInfo.InvariantCulture,
    '                System.Globalization.DateTimeStyles.None, t) Then Return False
    '    Else
    '        ' شمسی
    '        If y < 1300 OrElse y > 1600 Then Return False
    '        If m < 1 OrElse m > 12 OrElse dd < 1 Then Return False
    '        If m <= 6 AndAlso dd > 31 Then Return False
    '        If m > 6 AndAlso dd > 30 Then Return False
    '    End If

    '    result = d
    '    Return True
    'End Function

    'Private Function ToDate8(v As Object) As Object
    '    Dim r As String = ""
    '    If TryDate8(v, r) Then Return r
    '    Return DBNull.Value
    'End Function


    ' مثل کد قدیمی: سال < 2000 یعنی شمسی و با Sh2Mi تبدیل می‌شود، وگرنه همان است
    ' خروجی: میلادی ۸ رقمی مثل 20260101. اگر نامعتبر بود False
    'Private Function TryToMiladi(ByVal v As Object, ByRef result As String) As Boolean
    '    result = ""
    '    If v Is Nothing OrElse IsDBNull(v) Then Return False

    '    Dim digits As String = v.ToString().Trim().Replace("/", "").Replace("-", "").Replace(" ", "")
    '    If Not System.Text.RegularExpressions.Regex.IsMatch(digits, "^\d{8}$") Then Return False

    '    Dim withSlash As String = digits.Substring(0, 4) & "/" & digits.Substring(4, 2) & "/" & digits.Substring(6, 2)
    '    Dim res As String

    '    Try
    '        If CInt(digits.Substring(0, 4)) < 2000 Then
    '            Dim r As Object = objTarikh.Sh2Mi(withSlash)
    '            If TypeOf r Is DateTime Then
    '                res = CDate(r).ToString("yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture)
    '            Else
    '                res = Convert.ToString(r)
    '            End If
    '        Else
    '            res = withSlash
    '        End If
    '    Catch
    '        Return False
    '    End Try

    '    res = res.Replace("/", "").Replace("-", "").Trim()

    '    ' خروجی باید یک تاریخ میلادی واقعی باشد
    '    Dim dt As DateTime
    '    If Not System.Text.RegularExpressions.Regex.IsMatch(res, "^\d{8}$") Then Return False
    '    If Not DateTime.TryParseExact(res, "yyyyMMdd",
    '        System.Globalization.CultureInfo.InvariantCulture,
    '        System.Globalization.DateTimeStyles.None, dt) Then Return False
    '    If dt.Year < 2000 Then Return False

    '    result = res
    '    Return True
    'End Function

    Private Function ToMiladi(v As Object) As Object
        Dim r As String = ""
        If TryToMiladi(v, r) Then Return r
        Return DBNull.Value
    End Function


    ' Sh2Mi را صدا می‌زند و خروجی را به ۸ رقم تمیز می‌کند. اگر خطا داد "" برمی‌گرداند
    Private Function CallSh2Mi(ByVal s As String) As String
        Try
            Dim r As Object = objTarikh.Sh2Mi(s)
            If r Is Nothing Then Return ""
            If TypeOf r Is DateTime Then
                Return CDate(r).ToString("yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture)
            End If
            Return Convert.ToString(r).Replace("/", "").Replace("-", "").Trim()
        Catch
            Return ""
        End Try
    End Function

    Private Function IsValidMiladi8(ByVal s As String) As Boolean
        If s Is Nothing Then Return False
        If Not System.Text.RegularExpressions.Regex.IsMatch(s, "^\d{8}$") Then Return False
        Dim dt As DateTime
        If Not DateTime.TryParseExact(s, "yyyyMMdd",
                System.Globalization.CultureInfo.InvariantCulture,
                System.Globalization.DateTimeStyles.None, dt) Then Return False
        Return dt.Year >= 2000
    End Function

    ' سال < 2000 یعنی شمسی و با Sh2Mi تبدیل می‌شود، وگرنه همان است
    ' خروجی: میلادی ۸ رقمی مثل 20260101. اگر نامعتبر بود False
    Private Function TryToMiladi(ByVal v As Object, ByRef result As String) As Boolean
        result = ""
        If v Is Nothing OrElse IsDBNull(v) Then Return False

        Dim digits As String = v.ToString().Trim().Replace("/", "").Replace("-", "").Replace(" ", "")
        If Not System.Text.RegularExpressions.Regex.IsMatch(digits, "^\d{8}$") Then Return False

        Dim res As String

        If CInt(digits.Substring(0, 4)) < 2000 Then
            ' شمسی: Sh2Mi ورودی ۸ رقمی بدون / می‌خواهد
            res = CallSh2Mi(digits)

            ' اگر نشد، یک بار با / امتحان کن
            If Not IsValidMiladi8(res) Then
                res = CallSh2Mi(digits.Substring(0, 4) & "/" & digits.Substring(4, 2) & "/" & digits.Substring(6, 2))
            End If
        Else
            res = digits
        End If

        If Not IsValidMiladi8(res) Then Return False

        result = res
        Return True
    End Function
    ' ================== دکمه‌ی جدید ==================
    Private Sub btnNew_Click(ByVal sender As Object, ByVal e As EventArgs)
        NewResid()
    End Sub

    ' کنترل‌های هدر را فعال یا غیرفعال می‌کند
    Private Sub SetHeaderEnabled(ByVal en As Boolean)
        cmbNameTaminKonandeh.Enabled = en
        txtSearchT.Enabled = en
        cmbAnbar.Enabled = en
        cmbsCodeDorehSefaresh.Enabled = en
        txtShomarehSefaresh.Enabled = en
        mskTarikhForm.Enabled = en
        txtTozihat.Enabled = en
        cmbTahvilGirandeh.Enabled = en
        cmbTahvilDahandeh.Enabled = en
        txtRanandeh.Enabled = en
        txtKhodro.Enabled = en
        txtBarnameh.Enabled = en
        txtShomarehMobile.Enabled = en
    End Sub

    Private Sub NewResid()
        ' ۱) ریست وضعیت
        ccKardexTitr = 0
        tCodeCounter = 0
        ccShomarehBach = 0
        _gridStructureReady = False      ' چون DataSource خالی می‌شود، ساختار باید دوباره ساخته شود

        ' ۲) هدر: فعال و خالی (ClearForm قاعده‌ی غیرفعال بودن تاریخ در بعضی شرکت‌ها را خودش اعمال می‌کند)
        SetHeaderEnabled(True)
        ClearForm()

        txtSearchT.Text = ""
        txtSearchNumber.Text = ""
        txtRanandeh.Text = ""
        txtKhodro.Text = ""
        txtBarnameh.Text = ""
        txtShomarehMobile.Text = ""
        btnSaveSanad.Enabled = True

        ' ۳) گرید: خالی و غیرفعال
        dt_SearchSatr = New DataTable
        GridEXSatr.SetDataBinding(Nothing, "")
        GridEXSatr.Enabled = False

        ' ۴) فوکوس روی اولین فیلد
        cmbNameTaminKonandeh.Focus()
    End Sub

    ' ================== جستجوی رسید ==================
    Private Sub txtSearchNumber_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles txtSearchNumber.KeyDown
        If e.KeyCode = Keys.Space Then
            e.Handled = True
            e.SuppressKeyPress = True

            ' فقط رسیدهایی که sVazeiat = 0 دارند
            Dim frm As New frmResidSearch(ConnectionString, CodeMahalFaal, CInt(CodeDoreh), 0)
            If frm.ShowDialog() = DialogResult.OK AndAlso frm.SelectedId > 0 Then
                ShowResid(frm.SelectedId)
            End If

        ElseIf e.KeyCode <> Keys.Tab AndAlso e.KeyCode <> Keys.ShiftKey Then
            ' هر کلید دیگری بلاک می‌شود (به‌جز Tab)
            e.Handled = True
            e.SuppressKeyPress = True
        End If
    End Sub

    Private Sub txtSearchNumber_KeyPress(ByVal sender As Object, ByVal e As KeyPressEventArgs) Handles txtSearchNumber.KeyPress
        e.Handled = True
    End Sub

    Private Shared Function RGetStr(ByVal r As DataRow, ByVal col As String) As String
        If Not r.Table.Columns.Contains(col) Then Return ""
        Dim v As Object = r(col)
        If v Is Nothing OrElse IsDBNull(v) Then Return ""
        Return v.ToString()
    End Function

    Private Shared Function RGetInt(ByVal r As DataRow, ByVal col As String) As Integer
        If Not r.Table.Columns.Contains(col) Then Return 0
        Dim v As Object = r(col)
        If v Is Nothing OrElse IsDBNull(v) Then Return 0
        Return Convert.ToInt32(v)
    End Function

    ' رسید انتخاب‌شده را در فرم نشان می‌دهد و گرید را برای ادامه‌ی کار فعال می‌کند
    Private Sub ShowResid(ByVal id As Integer)
        Dim dt As New DataTable
        Try
            Using cn As New SqlConnection(ConnectionString)
                Using cm As SqlCommand = cn.CreateCommand()
                    cm.CommandType = CommandType.StoredProcedure
                    cm.CommandText = "[dbo].[spAN_KdxResid_LoadTitr]"
                    cm.Parameters.AddWithValue("@ccKardexTitr", id)
                    Using da As New SqlDataAdapter(cm)
                        da.Fill(dt)
                    End Using
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        If dt.Rows.Count = 0 Then
            MessageBox.Show("اطلاعات رسید پیدا نشد")
            Exit Sub
        End If

        Dim r As DataRow = dt.Rows(0)

        NewResid()                      ' اول همه‌چیز را تمیز کن

        ccKardexTitr = id
        txtSearchNumber.Text = id.ToString()

        mskTarikhForm.Text = RGetStr(r, "TarikhForm")
        cmbNameTaminKonandeh.SelectedValue = RGetInt(r, "ccTaminKonandeh")
        cmbAnbar.SelectedValue = RGetInt(r, "ccAnbar")
        txtTozihat.Text = RGetStr(r, "Tozihat")

        cmbTahvilGirandeh.SelectedValue = RGetStr(r, "NameFardTahvilGirandeh")
        If cmbTahvilGirandeh.SelectedIndex = -1 Then cmbTahvilGirandeh.Text = RGetStr(r, "NameFardTahvilGirandeh")
        cmbTahvilDahandeh.SelectedValue = RGetStr(r, "NameFardTahvilDahandeh")
        If cmbTahvilDahandeh.SelectedIndex = -1 Then cmbTahvilDahandeh.Text = RGetStr(r, "NameFardTahvilDahandeh")

        txtRanandeh.Text = RGetStr(r, "Ranandeh")
        txtKhodro.Text = RGetStr(r, "Khodro")
        txtBarnameh.Text = RGetStr(r, "Barnameh")
        txtShomarehMobile.Text = RGetStr(r, "Mobile")

        ' هدر ثبت‌شده است: قفل و ذخیره‌ی هدر غیرفعال
        SetHeaderEnabled(False)
        btnSaveSanad.Enabled = False

        SearchSatr()
        GridEXSatr.Enabled = True
        GridEXSatr.Focus()
        GridEXSatr.MoveToNewRecord()
        GridEXSatr.Col = 0
    End Sub

    Private Sub btnNew_Click_1(sender As Object, e As EventArgs) Handles btnNew.Click
        NewResid()
    End Sub
End Class