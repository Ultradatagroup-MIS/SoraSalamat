Public Class frmFO_VorodKala

#Region "Variable AND Constant Declration"

    Dim AllowPishFaktorTakhfifDasty As Boolean = objTools.ConvertNulls(objTools.DLookup("AllowPishFaktorTakhfifDasty", "tblGL_SysConfig", ""), False)

    Dim cmTitr As CurrencyManager
    Dim cmSatr As CurrencyManager
    Dim cmForm As CurrencyManager
    Dim dvTitr, dvTitr_Faktor As DataView
    Dim tCodeCounter As Long
    Const FormTableName = "tblFO_ElamMarjoee"
    Dim ErrPro As New ErrorProvider
    Dim dsForm As New DataSet
    Dim dvForm, dvForm_Fakotr As DataView
    Dim IsSabadKala As Boolean = False
    Private SN As Integer
    Dim flg_SearchFaktor As Boolean = False
    Dim txtCaption As String
    Private WithEvents BS As New UD_Dll.PassString
    Dim FirstInsert As Boolean = False

    Public ccMoshtary As Integer
    Dim sNoeMoshtary As Integer = 0
    Public CodeFardMamorPakhsh As Integer
    Dim IsMalyatAvarezTakhfif As Boolean
    Dim tpos As Integer

    Dim ccMarjoee_Vaset As Integer = 0

    Dim Mode As Boolean = False  ' Mode  --> False : Save Titr \\ True : Inser Kala

    Dim WithEvents TJ As TakhfifOJavaiez.TakhfifJayezeh

#End Region
    Private Sub frmFO_VorodKala_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetParameter()
        objTools.ChangeKbd(UD_Dll.mdlUtility.KBD.Farsi)

        LoadCombo()
        SetForm()
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
            CodeDoreh = "1394"
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
        Dim cnSQL As New SqlConnection
        Dim cmSQL As New SqlCommand
        Dim daSQL As New SqlDataAdapter
        Dim dr As DataRow
        Dim strSQL As String = ""

        cnSQL = New SqlConnection(ConnectionString)
        cnSQL.Open()

        ' --------------------- Load Search Combo MamorPakhsh

        'strSQL = "Select * From qryGL_MoshakhasatFardi Where CodeMahal = " & CodeMahalFaal & " AND sVazeiatEstekhdam = 4188 And "
        'strSQL &= " sSemat in (" & UD_Dll.Enums.GL_Semat.MamorPakhsh & "," & UD_Dll.Enums.GL_Semat.Foroshandeh_Sayar & ")"

        'cmSQL = New SqlCommand(strSQL, cnSQL)

        'daSQL = New SqlDataAdapter(cmSQL)
        'daSQL.Fill(dsForm, "tblMamorPakhsh")

        'dr = dsForm.Tables("tblMamorPakhsh").NewRow()
        'dr("CodeFard") = 0
        'dr("FN") = "-----"
        'dsForm.Tables("tblMamorPakhsh").Rows.Add(dr)

        'cmbMamorPakhsh.DataSource = Nothing
        'cmbMamorPakhsh.Items.Clear()
        'cmbMamorPakhsh.DataSource = dsForm.Tables("tblMamorPakhsh").DefaultView
        'cmbMamorPakhsh.DisplayMember = "FN"
        'cmbMamorPakhsh.ValueMember = "CodeFard"
        'cmbMamorPakhsh.SelectedValue = 0


        strSQL = "Global.spMamorPakhsh_LoadCombo "

        cmSQL = New SqlCommand(strSQL, cnSQL)
        cmSQL.CommandType = CommandType.StoredProcedure
        cmSQL.Parameters.Clear()

        cmSQL.Parameters.AddWithValue("CodeMahal", CodeMahalFaal)
        cmSQL.Parameters.AddWithValue("sSemat", "," & UD_Dll.Enums.GL_Semat.MamorPakhsh & "," & UD_Dll.Enums.GL_Semat.Ranandeh & "," & UD_Dll.Enums.GL_Semat.Foroshandeh_Sayar & ",")

        daSQL = New SqlDataAdapter(cmSQL)
        daSQL.Fill(dsForm, "tblMamorPakhsh")

        cmbMamorPakhsh.DataSource = Nothing
        cmbMamorPakhsh.Items.Clear()
        cmbMamorPakhsh.DataSource = dsForm.Tables("tblMamorPakhsh").DefaultView
        cmbMamorPakhsh.DisplayMember = "FN"
        cmbMamorPakhsh.ValueMember = "CodeFard"
        cmbMamorPakhsh.SelectedValue = 0


        cmSQL = Nothing

        ' --------------------- Load Combo sElatMarjoee
        strSQL = "SELECT * FROM tblGL_ShenasehOmomi WHERE CodeAsli = 63 AND CodeFarei <> 0 ORDER BY Sharh"

        cmSQL = New SqlCommand(strSQL, cnSQL)

        daSQL = New SqlDataAdapter(cmSQL)
        daSQL.Fill(dsForm, "tblElatMarjoee")

        dr = dsForm.Tables("tblElatMarjoee").NewRow()
        dr("Code") = 0
        dr("Sharh") = "-----"
        dsForm.Tables("tblElatMarjoee").Rows.Add(dr)

        cmbsElatMarjoee.DataSource = Nothing
        cmbsElatMarjoee.Items.Clear()
        cmbsElatMarjoee.DataSource = dsForm.Tables("tblElatMarjoee").DefaultView
        cmbsElatMarjoee.DisplayMember = "Sharh"
        cmbsElatMarjoee.ValueMember = "Code"

        cmSQL = Nothing

        ' --------------------- Load Combo Doreh

        strSQL = "SELECT CodeDoreh FROM tblFO_Faktor GROUP BY CodeDoreh ORDER BY CodeDoreh DESC"

        cmSQL = New SqlCommand(strSQL, cnSQL)

        daSQL = New SqlDataAdapter(cmSQL)
        daSQL.Fill(dsForm, "tblCodeDoreh")

        cmbCodeDorehFaktor.DataSource = Nothing
        cmbCodeDorehFaktor.Items.Clear()
        cmbCodeDorehFaktor.DataSource = dsForm.Tables("tblCodeDoreh").DefaultView
        cmbCodeDorehFaktor.DisplayMember = "CodeDoreh"
        cmbCodeDorehFaktor.ValueMember = "CodeDoreh"

        cmSQL = Nothing : daSQL = Nothing
        cnSQL.Close()

    End Sub
    Private Sub SetForm()
        If Mode = False Then
            cmbMamorPakhsh.SelectedValue = 0
            ccMarjoee_Vaset = 0
            ccMoshtary = 0
            sNoeMoshtary = 0
            CodeFardMamorPakhsh = 0

            txtCodeMoshtary.Text = ""
            cmbMamorPakhsh.SelectedValue = 0

            lblCodeMoshtary.Text = ""
            lblNameMoshtary.Text = ""
            lblCodeMamorPakhsh.Text = ""
            lblNameMamorPakhsh.Text = ""
            lblTarikhMarjoee.Text = ""

            cmbCodeDorehFaktor.SelectedValue = CodeDoreh
            cmbsElatMarjoee.SelectedValue = 0

            txtCodeMoshtary.Focus()

            Search()
            SearchFaktor(0, False)

            grbTitr.Enabled = True
            grbTitrShow.Enabled = False
            grbSatr.Enabled = False
            btnCreateMarjoee.Enabled = False
            btnClearForm.Enabled = False

            txtCodeMoshtary.Focus()

        ElseIf Mode = True Then
            lblCodeMoshtary.Text = objTools.DLookup("CodeMoshtary", "tblFO_Moshtary", "ccMoshtary = " & ccMoshtary)
            lblNameMoshtary.Text = objTools.DLookup("NameMoshtary", "tblFO_Moshtary", "ccMoshtary = " & ccMoshtary)
            sNoeMoshtary = objTools.DLookup("sNoeMoshtary", "tblFO_Moshtary", "ccMoshtary = " & ccMoshtary)
            lblCodeMamorPakhsh.Text = IIf(CodeFardMamorPakhsh = 0, "----", CodeFardMamorPakhsh)
            lblNameMamorPakhsh.Text = IIf(CodeFardMamorPakhsh = 0, "انتخـــاب نشـــده است !", objTools.DLookup("FN", "qryGL_MoshakhasatFardi", "CodeFard = " & CodeFardMamorPakhsh))
            lblTarikhMarjoee.Text = objTarikh.SetDateSlash(TarikhEmrooz)

            grbTitr.Enabled = False
            grbTitrShow.Enabled = True
            grbSatr.Enabled = True
            btnCreateMarjoee.Enabled = True
            btnClearForm.Enabled = True

            rbSalem.Checked = False
            rbKharab.Checked = False
            rbDasti.Checked = True
            rbAutomatic.Checked = False
            rbSoodi.Checked = False
            rbNozoli.Checked = False
        End If

        ClearForm(False)
    End Sub
    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        If ccMarjoee_Vaset <> 0 Then
            If dvTitr.Count <> 0 Then
                If MsgBox("در صورت خــروج و تاییــد نکردن مــوارد ذخیـره شده، هیچ اعلام مرجوعی ثبت نخواهد شده" & vbCrLf _
                         & " و تمامی مــوارد ذخیـره شده حــذف خواهنـد شد . آیا خــروج انجــام شــود ؟" & vbCrLf _
                         , MsgBoxStyle.Question + MsgBoxStyle.YesNo + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, "خـــروج") = MsgBoxResult.Yes Then
                    Delete_MarjoeeVaset(True)
                Else
                    Exit Sub
                End If
            End If
        End If

        Mode = False
        SetForm()
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
                Me.lblNameKala.Text = objKala.tNameKala

                MultiSelection = False
            End If
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->txtCodeMoshtaryS_KeyPress")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->txtCodeMoshtaryS_KeyPress")
        End Try
    End Sub
    Private Sub txtCodeKala_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCodeKala.TextChanged
        If flg_SearchFaktor = True Then
            SearchFaktor(0, False)
            flg_SearchFaktor = False
        End If

        If Trim(Me.txtCodeKala.Text) = "" Then Exit Sub

        Try
            Dim Mablagh As Double = 0

            Me.lblNameKala.Text = objTools.ConvertNulls(objTools.DLookup("NameKala", "tblAN_Kala", "CodeKala = " & Me.txtCodeKala.Text), "")
            Me.txtCodeKala.Tag = objTools.ConvertNulls(objTools.DLookup("ccKala", "tblAn_Kala", "CodeKala = " & Me.txtCodeKala.Text), 0)

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

            Me.lblFeeInFaktor.Text = Mablagh

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->txtCodeKala_TextChanged")
        End Try
    End Sub
    Private Sub btnShowFaktor_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnShowFaktor.Click
        SearchFaktor(txtCodeKala.Tag, True)
        flg_SearchFaktor = True
    End Sub
    Private Sub SearchFaktor(ByVal ccKala As Integer, ByVal ShowBedoneFaktor As Boolean)
        Dim cnSQL As New SqlConnection
        Dim cmSQL As New SqlCommand
        Dim daSQL As New SqlDataAdapter
        Dim strSQL As String = ""

        If dsForm.Tables.Contains("tbl_SearchFaktor") Then
            dsForm.Tables.Remove("tbl_SearchFaktor")
        End If

        Try
            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()

            strSQL = "Sales.spElamMarjoee_InsertOneKala_SearchFaktor "

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            cmSQL.Parameters.AddWithValue("ccMarjoee_Vaset", ccMarjoee_Vaset)
            cmSQL.Parameters.AddWithValue("CodeDoreh", cmbCodeDorehFaktor.SelectedValue)
            cmSQL.Parameters.AddWithValue("CodeMahal", CodeMahalFaal)
            cmSQL.Parameters.AddWithValue("ccKala", ccKala)
            cmSQL.Parameters.AddWithValue("ccMoshtary", ccMoshtary)
            cmSQL.Parameters.AddWithValue("sNoeMoshtary", sNoeMoshtary)
            cmSQL.Parameters.AddWithValue("Tarikh", TarikhEmrooz)
            cmSQL.Parameters.AddWithValue("ShowBedoneFaktor", ShowBedoneFaktor)

            daSQL = New SqlDataAdapter(cmSQL)
            daSQL.Fill(dsForm, "tbl_SearchFaktor")

            dvForm_Fakotr = New DataView
            dvForm_Fakotr = dsForm.Tables("tbl_SearchFaktor").DefaultView
            dvForm_Fakotr.Sort = "ccFaktorTitr DESC"
            dvForm_Fakotr.AllowDelete = False
            dvForm_Fakotr.AllowEdit = False
            dvForm_Fakotr.AllowNew = False

            cmSQL = Nothing : daSQL = Nothing

            dvTitr_Faktor = New DataView(dsForm.Tables("tbl_SearchFaktor"), "", "ccFaktorTitr DESC", DataViewRowState.CurrentRows)
            dvTitr_Faktor.AllowNew = False
            dvTitr_Faktor.AllowDelete = False
            dvTitr_Faktor.AllowEdit = False


            GridEXFaktor.DataSource = Nothing
            GridEXFaktor.DataSource = dvTitr_Faktor

            SetGridStyle_Faktor()

            cnSQL.Close()
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> SearchFaktor ")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> SearchFaktor ")
        End Try
    End Sub
    Private Sub SetGridStyle_Faktor()
        Try
            With GridEXFaktor
                .DataSource = Nothing
                .DataSource = dsForm.Tables("tbl_SearchFaktor").DefaultView
                .SetDataBinding(dsForm.Tables("tbl_SearchFaktor").DefaultView, "")
                .RetrieveStructure()
            End With

            For i As Integer = 0 To GridEXFaktor.CurrentTable.Columns.Count - 1
                GridEXFaktor.CurrentTable.Columns.Item(i).Visible = False
            Next

            GridEXFaktor.CurrentTable.Columns.Item("FaktorShomareh").Caption = "شماره فاکتور"
            GridEXFaktor.CurrentTable.Columns.Item("FaktorShomareh").Visible = True
            GridEXFaktor.CurrentTable.Columns.Item("FaktorShomareh").Width = 85
            GridEXFaktor.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXFaktor.CurrentTable.Columns.Item("FaktorShomareh").Position = 0
            GridEXFaktor.CurrentTable.Columns.Item("FaktorShomareh").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXFaktor.CurrentTable.Columns.Item("FaktorTarikh").Caption = "تاریـخ فاکتـور"
            GridEXFaktor.CurrentTable.Columns.Item("FaktorTarikh").Visible = True
            GridEXFaktor.CurrentTable.Columns.Item("FaktorTarikh").Width = 80
            GridEXFaktor.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXFaktor.CurrentTable.Columns.Item("FaktorTarikh").Position = 1
            GridEXFaktor.CurrentTable.Columns.Item("FaktorTarikh").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXFaktor.CurrentTable.Columns.Item("TedadEstefadeh").Caption = "استفاده"
            GridEXFaktor.CurrentTable.Columns.Item("TedadEstefadeh").Visible = True
            GridEXFaktor.CurrentTable.Columns.Item("TedadEstefadeh").Width = 70
            GridEXFaktor.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXFaktor.CurrentTable.Columns.Item("TedadEstefadeh").Position = 2
            GridEXFaktor.CurrentTable.Columns.Item("TedadEstefadeh").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXFaktor.CurrentTable.Columns.Item("CodeDoreh").Caption = "CodeDoreh"
            GridEXFaktor.CurrentTable.Columns.Item("CodeDoreh").Visible = False
            GridEXFaktor.CurrentTable.Columns.Item("CodeDoreh").Width = 0
            GridEXFaktor.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXFaktor.CurrentTable.Columns.Item("CodeDoreh").Position = 3
            GridEXFaktor.CurrentTable.Columns.Item("CodeDoreh").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXFaktor.CurrentTable.Columns.Item("ccFaktorTitr").Caption = "ccFaktorTitr"
            GridEXFaktor.CurrentTable.Columns.Item("ccFaktorTitr").Visible = False
            GridEXFaktor.CurrentTable.Columns.Item("ccFaktorTitr").Width = 0
            GridEXFaktor.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXFaktor.CurrentTable.Columns.Item("ccFaktorTitr").FormatString = "G"
            GridEXFaktor.CurrentTable.Columns.Item("ccFaktorTitr").Position = 4
            GridEXFaktor.CurrentTable.Columns.Item("ccFaktorTitr").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXFaktor.CurrentTable.Columns.Item("TedadDarFaktor").Caption = "TedadDarFaktor"
            GridEXFaktor.CurrentTable.Columns.Item("TedadDarFaktor").Visible = False
            GridEXFaktor.CurrentTable.Columns.Item("TedadDarFaktor").Width = 0
            GridEXFaktor.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXFaktor.CurrentTable.Columns.Item("TedadDarFaktor").Position = 5
            GridEXFaktor.CurrentTable.Columns.Item("TedadDarFaktor").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXFaktor.CurrentTable.Columns.Item("TedadMarjoeeAzGhabl").Caption = "TedadMarjoeeAzGhabl"
            GridEXFaktor.CurrentTable.Columns.Item("TedadMarjoeeAzGhabl").Visible = False
            GridEXFaktor.CurrentTable.Columns.Item("TedadMarjoeeAzGhabl").Width = 0
            GridEXFaktor.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXFaktor.CurrentTable.Columns.Item("TedadMarjoeeAzGhabl").Position = 6
            GridEXFaktor.CurrentTable.Columns.Item("TedadMarjoeeAzGhabl").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXFaktor.CurrentTable.Columns.Item("TedadRezerv").Caption = "TedadRezerv"
            GridEXFaktor.CurrentTable.Columns.Item("TedadRezerv").Visible = False
            GridEXFaktor.CurrentTable.Columns.Item("TedadRezerv").Width = 0
            GridEXFaktor.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXFaktor.CurrentTable.Columns.Item("TedadRezerv").Position = 7
            GridEXFaktor.CurrentTable.Columns.Item("TedadRezerv").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXFaktor.CurrentTable.Columns.Item("TedadGhabelMarjoee").Caption = "TedadGhabelMarjoee"
            GridEXFaktor.CurrentTable.Columns.Item("TedadGhabelMarjoee").Visible = False
            GridEXFaktor.CurrentTable.Columns.Item("TedadGhabelMarjoee").Width = 0
            GridEXFaktor.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXFaktor.CurrentTable.Columns.Item("TedadGhabelMarjoee").Position = 8
            GridEXFaktor.CurrentTable.Columns.Item("TedadGhabelMarjoee").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXFaktor.CurrentTable.Columns.Item("Gheymat").Caption = "Gheymat"
            GridEXFaktor.CurrentTable.Columns.Item("Gheymat").Visible = False
            GridEXFaktor.CurrentTable.Columns.Item("Gheymat").Width = 0
            GridEXFaktor.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXFaktor.CurrentTable.Columns.Item("Gheymat").Position = 9
            GridEXFaktor.CurrentTable.Columns.Item("Gheymat").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            For i As Integer = 0 To GridEXFaktor.RootTable.Columns.Count - 1
                If GridEXFaktor.RootTable.Columns(i).Type.IsValueType Then
                    GridEXFaktor.RootTable.Columns(i).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                    GridEXFaktor.RootTable.Columns(i).FormatMode = Janus.Windows.GridEX.FormatMode.UseIFormattable
                    GridEXFaktor.RootTable.Columns(i).FormatString = "###,###.##"
                    GridEXFaktor.RootTable.Columns(i).TotalFormatMode = Janus.Windows.GridEX.FormatMode.UseIFormattable
                    GridEXFaktor.RootTable.Columns(i).TotalFormatString = "###,###.##"
                End If
            Next

            GridEXFaktor.Visible = True
            GridEXFaktor.HeaderFormatStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center


        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> SetGridStyle_Faktor ")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> SetGridStyle_Faktor ")
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

            strSQL = "Sales.spElamMarjoee_InsertOneKala_Search "

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            cmSQL.Parameters.AddWithValue("ccMarjoee_Vaset", ccMarjoee_Vaset)

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
            GridEXTitr.CurrentTable.Columns.Item("CodeKala").Position = 0
            GridEXTitr.CurrentTable.Columns.Item("CodeKala").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXTitr.CurrentTable.Columns.Item("NameKala").Caption = "نـام کـالا"
            GridEXTitr.CurrentTable.Columns.Item("NameKala").Visible = True
            GridEXTitr.CurrentTable.Columns.Item("NameKala").Width = 200
            GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXTitr.CurrentTable.Columns.Item("NameKala").Position = 1
            GridEXTitr.CurrentTable.Columns.Item("NameKala").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXTitr.CurrentTable.Columns.Item("Tedad").Caption = "تعــداد"
            GridEXTitr.CurrentTable.Columns.Item("Tedad").Visible = True
            GridEXTitr.CurrentTable.Columns.Item("Tedad").Width = 70
            GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXTitr.CurrentTable.Columns.Item("Tedad").FormatString = "G"
            GridEXTitr.CurrentTable.Columns.Item("Tedad").Position = 2
            GridEXTitr.CurrentTable.Columns.Item("Tedad").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXTitr.CurrentTable.Columns.Item("Fee").Caption = "قیمـت"
            GridEXTitr.CurrentTable.Columns.Item("Fee").Visible = True
            GridEXTitr.CurrentTable.Columns.Item("Fee").Width = 70
            GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXTitr.CurrentTable.Columns.Item("Fee").FormatString = "G"
            GridEXTitr.CurrentTable.Columns.Item("Fee").Position = 3
            GridEXTitr.CurrentTable.Columns.Item("Fee").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXTitr.CurrentTable.Columns.Item("Mablagh").Caption = "مبلـــغ"
            GridEXTitr.CurrentTable.Columns.Item("Mablagh").Visible = True
            GridEXTitr.CurrentTable.Columns.Item("Mablagh").Width = 100
            GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXTitr.CurrentTable.Columns.Item("Mablagh").FormatString = "G"
            GridEXTitr.CurrentTable.Columns.Item("Mablagh").Position = 4
            GridEXTitr.CurrentTable.Columns.Item("Mablagh").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXTitr.CurrentTable.Columns.Item("FaktorShomareh").Caption = "شماره فاکتور"
            GridEXTitr.CurrentTable.Columns.Item("FaktorShomareh").Visible = True
            GridEXTitr.CurrentTable.Columns.Item("FaktorShomareh").Width = 90
            GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXTitr.CurrentTable.Columns.Item("FaktorShomareh").NullText = "بدون فاکتور"
            GridEXTitr.CurrentTable.Columns.Item("FaktorShomareh").Position = 5
            GridEXTitr.CurrentTable.Columns.Item("FaktorShomareh").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXTitr.CurrentTable.Columns.Item("FaktorTarikh").Caption = "فاکتور تاریخ"
            GridEXTitr.CurrentTable.Columns.Item("FaktorTarikh").Visible = True
            GridEXTitr.CurrentTable.Columns.Item("FaktorTarikh").Width = 80
            GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXTitr.CurrentTable.Columns.Item("FaktorTarikh").Position = 6
            GridEXTitr.CurrentTable.Columns.Item("FaktorTarikh").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXTitr.CurrentTable.Columns.Item("NameForoshandeh").Caption = "نـام فـروشنـده"
            GridEXTitr.CurrentTable.Columns.Item("NameForoshandeh").Visible = True
            GridEXTitr.CurrentTable.Columns.Item("NameForoshandeh").Width = 200
            GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXTitr.CurrentTable.Columns.Item("NameForoshandeh").Position = 7
            GridEXTitr.CurrentTable.Columns.Item("NameForoshandeh").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXTitr.CurrentTable.Columns.Item("txtElatMarjoee").Caption = "علـت مرجوعی"
            GridEXTitr.CurrentTable.Columns.Item("txtElatMarjoee").Visible = True
            GridEXTitr.CurrentTable.Columns.Item("txtElatMarjoee").Width = 150
            GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXTitr.CurrentTable.Columns.Item("txtElatMarjoee").Position = 8
            GridEXTitr.CurrentTable.Columns.Item("txtElatMarjoee").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXTitr.CurrentTable.Columns.Item("txtNoeMarjoee").Caption = "نوع مرجوعی"
            GridEXTitr.CurrentTable.Columns.Item("txtNoeMarjoee").Visible = True
            GridEXTitr.CurrentTable.Columns.Item("txtNoeMarjoee").Width = 100
            GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXTitr.CurrentTable.Columns.Item("txtNoeMarjoee").Position = 9
            GridEXTitr.CurrentTable.Columns.Item("txtNoeMarjoee").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXTitr.CurrentTable.Columns.Item("ccMarjoeeSatr_Vaset").Caption = "ccMarjoeeSatr_Vaset"
            GridEXTitr.CurrentTable.Columns.Item("ccMarjoeeSatr_Vaset").Visible = False
            GridEXTitr.CurrentTable.Columns.Item("ccMarjoeeSatr_Vaset").Width = 0
            GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXTitr.CurrentTable.Columns.Item("ccMarjoeeSatr_Vaset").Position = 10
            GridEXTitr.CurrentTable.Columns.Item("ccMarjoeeSatr_Vaset").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

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
    Private Sub GridEXFaktor_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GridEXFaktor.Click
        lblTedadDarFaktor.Text = GridEXFaktor.CurrentRow.Cells("TedadDarFaktor").Text
        lblTedadMarjoeeAzGhabl.Text = IIf(GridEXFaktor.CurrentRow.Cells("TedadMarjoeeAzGhabl").Text = "", 0, GridEXFaktor.CurrentRow.Cells("TedadMarjoeeAzGhabl").Text)
        lblTedadRezervShodeh.Text = IIf(GridEXFaktor.CurrentRow.Cells("TedadRezerv").Text = "", 0, GridEXFaktor.CurrentRow.Cells("TedadRezerv").Text)
        lblTedadGhabelMarjoee.Text = GridEXFaktor.CurrentRow.Cells("TedadGhabelMarjoee").Text
        lblFeeInFaktor.Text = GridEXFaktor.CurrentRow.Cells("Gheymat").Text
        txtTedadKala.Text = 0
    End Sub
    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If Not IsValidRow("All") Then
            Exit Sub
        End If

        If ccMarjoee_Vaset = 0 Then
            InsertTitr()
        End If

        If rbDasti.Checked = True Then
            Dim PK As Integer = Val(GridEXFaktor.CurrentRow.Cells("ccFaktorTitr").Text.Replace(",", ""))
            Dim CodeDorehFaktor As Integer = GridEXFaktor.CurrentRow.Cells("CodeDoreh").Text.Replace(",", "")

            If lblFeeInFaktor.Text.Replace(",", "") = "" Then
                MsgBox("برای این کالا در سیستم قیمت تعریف نشده است .", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information + MsgBoxStyle.Critical, "خطا")
                Exit Sub
            End If

            Dim Gheymat As Integer = lblFeeInFaktor.Text.Replace(",", "")

            InsertSatr(PK, CodeDorehFaktor, txtTedadKala.Text, Gheymat)
        ElseIf rbAutomatic.Checked = True Then
            InsertSatr_Automatic()
        End If

        ClearForm(False)
        Search()

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

            If chkField = "cmbsElatMarjoee" Or chkField = "All" Then
                If Me.cmbsElatMarjoee.Text = "" Or Me.cmbsElatMarjoee.SelectedValue = 0 Then
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

            If rbDasti.Checked = True Then
                If chkField = "ccFaktorTitr" Or chkField = "All" Then
                    If (CDbl(Me.lblTedadDarFaktor.Text.Trim) = 0) Or lblTedadDarFaktor.Text.Trim = "" Then
                        MsgBox(" لطفاً یک فاکتور انتخــاب نمایید .", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
                        Exit Function
                    End If
                End If

                If chkField = "txtCodeKala" Or chkField = "All" Then
                    If objTools.ConvertNulls(objTools.DCount("ccMarjoeeSatr_Vaset", "tblFO_ElamMarjoeeSatr_Vaset", "ccFaktorTitr = " & Val(GridEXFaktor.CurrentRow.Cells("ccFaktorTitr").Text.Replace(",", "")) & " AND ccMarjoee_Vaset = " & ccMarjoee_Vaset & " AND ccKala = " & txtCodeKala.Tag), 0) <> 0 Then
                        ErrPro.SetError(txtCodeKala, "کالای وارد شده از فاکتور انتخابی تکراری است .")
                        MsgBox(" کالای وارد شده از فاکتور انتخابی تکراری است .", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
                        txtCodeKala.Focus()
                        Exit Function
                    End If
                    ErrPro.SetError(txtCodeKala, "")
                End If
            End If

            Dim TedadDarFaktor As Double = 0
            Dim TedadDarMarjoee As Double = 0
            Dim TedadRezervShodeh As Double = 0

            If rbDasti.Checked = True Then
                TedadDarFaktor = objTools.DCount("Tedad3", "tblFO_FaktorSatr", "ccFaktorTitr = " & Val(GridEXFaktor.CurrentRow.Cells("ccFaktorTitr").Text.Replace(",", "")) & " AND ccKala = " & txtCodeKala.Tag)
                TedadDarMarjoee = objTools.DCount("Tedad3", "tblFO_ElamMarjoeeSatr", "ccFaktorTitr = " & Val(GridEXFaktor.CurrentRow.Cells("ccFaktorTitr").Text.Replace(",", "")) & " AND ccKala = " & txtCodeKala.Tag)
                TedadRezervShodeh = objTools.DCount("Tedad", "tblFO_ElamMarjoeeSatr_Vaset", "ccFaktorTitr = " & Val(GridEXFaktor.CurrentRow.Cells("ccFaktorTitr").Text.Replace(",", "")) & " AND ccKala = " & txtCodeKala.Tag)
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
                ElseIf rbDasti.Checked = True AndAlso (CDbl(Me.txtTedadKala.Text)) > (CDbl(Me.lblTedadGhabelMarjoee.Text.Trim)) Then
                    ErrPro.SetError(txtTedadKala, "تعــداد نمی تواند بزرگتر از تعداد قابل مرجوعی باشد .")
                    MsgBox(" تعــداد نمی تواند بزرگتر از تعداد قابل مرجوعی باشد .", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
                    txtTedadKala.Focus()
                    Exit Function
                ElseIf rbDasti.Checked = True AndAlso (CDbl(Me.lblTedadGhabelMarjoee.Text.Trim)) < (TedadDarFaktor - TedadDarMarjoee - TedadRezervShodeh) Then
                    ErrPro.SetError(txtTedadKala, "تعــداد قابل مرجوعی تغییر کرده، لطفا مجددا بر روی دکمه « فاکتور های شامل کالا » کلیک نمایید .")
                    MsgBox(" تعــداد قابل مرجوعی تغییر کرده، لطفا مجددا بر روی دکمه « فاکتور های شامل کالا » کلیک نمایید .", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
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
    Private Sub InsertTitr()
        Dim cnSQL As New SqlConnection
        Dim cmSQL As New SqlCommand
        Dim strSQL As String = ""

        Try
            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()

            strSQL = "Sales.spElamMarjoee_InsertOneKala_InsertMarjoeeVasetTitr "

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            cmSQL.Parameters.AddWithValue("UserName", UserName)
            cmSQL.Parameters.AddWithValue("Tarikh", TarikhEmrooz)
            cmSQL.Parameters.AddWithValue("Saat", Format(TimeOfDay, "HH:mm:ss"))
            cmSQL.Parameters.AddWithValue("ccMarjoee_Vaset", ccMarjoee_Vaset)
            cmSQL.Parameters("ccMarjoee_Vaset").Direction = ParameterDirection.Output

            cmSQL.ExecuteNonQuery()

            ccMarjoee_Vaset = cmSQL.Parameters("ccMarjoee_Vaset").Value

            cmSQL = Nothing
            cnSQL.Close()

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> InsertTitr ")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> InsertTitr ")
        End Try
    End Sub
    Private Sub InsertSatr(ByVal ccFaktorTitr As Integer, ByVal CodeDorehFaktor As Integer, ByVal Tedad As Double, ByVal Gheymat As Integer)
        Dim cnSQL As New SqlConnection
        Dim cmSQL As New SqlCommand
        Dim strSQL As String = ""

        Try
            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()

            strSQL = "Sales.spElamMarjoee_InsertOneKala_InsertMarjoeeVasetSatr "

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            cmSQL.Parameters.AddWithValue("ccMarjoee_Vaset", ccMarjoee_Vaset)
            cmSQL.Parameters.AddWithValue("CodeDoreh", CodeDoreh)
            cmSQL.Parameters.AddWithValue("CodeMahal", CodeMahalFaal)
            cmSQL.Parameters.AddWithValue("ccMoshtary", ccMoshtary)
            cmSQL.Parameters.AddWithValue("ccFaktorTitr", ccFaktorTitr)
            cmSQL.Parameters.AddWithValue("CodeDorehFaktor", CodeDorehFaktor)
            cmSQL.Parameters.AddWithValue("Tarikh", TarikhEmrooz)
            cmSQL.Parameters.AddWithValue("CodeFard_MamorPakhsh", CodeFardMamorPakhsh)
            cmSQL.Parameters.AddWithValue("ccKala", txtCodeKala.Tag)
            cmSQL.Parameters.AddWithValue("Tedad", Tedad)
            cmSQL.Parameters.AddWithValue("Fee", Gheymat)
            cmSQL.Parameters.AddWithValue("sElatMarjoee", cmbsElatMarjoee.SelectedValue)
            If rbKharab.Checked Then
                cmSQL.Parameters.AddWithValue("NoeMarjoee", UD_Dll.Enums.FO_NoeKalaElamMarjoee.Karab)
            ElseIf rbSalem.Checked Then
                cmSQL.Parameters.AddWithValue("NoeMarjoee", UD_Dll.Enums.FO_NoeKalaElamMarjoee.Salem)
            End If

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
        Dim TedadMandeh As Double = 0
        Dim TedadGhabelSabt As Double = 0

        If dsForm.Tables.Contains("tbl_SearchFaktorForInsertAutomatic") Then
            dsForm.Tables.Remove("tbl_SearchFaktorForInsertAutomatic")
        End If

        Try
            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()

            strSQL = "Sales.spElamMarjoee_InsertOneKala_SearchFaktor "

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            cmSQL.Parameters.AddWithValue("ccMarjoee_Vaset", ccMarjoee_Vaset)
            cmSQL.Parameters.AddWithValue("CodeDoreh", CodeDoreh)
            cmSQL.Parameters.AddWithValue("CodeMahal", CodeMahalFaal)
            cmSQL.Parameters.AddWithValue("ccKala", txtCodeKala.Tag)
            cmSQL.Parameters.AddWithValue("ccMoshtary", ccMoshtary)
            cmSQL.Parameters.AddWithValue("sNoeMoshtary", sNoeMoshtary)
            cmSQL.Parameters.AddWithValue("Tarikh", TarikhEmrooz)
            cmSQL.Parameters.AddWithValue("ShowBedoneFaktor", False)

            daSQL = New SqlDataAdapter(cmSQL)
            daSQL.Fill(dsForm, "tbl_SearchFaktorForInsertAutomatic")

            dv = New DataView(dsForm.Tables("tbl_SearchFaktorForInsertAutomatic"))
            If rbNozoli.Checked = True Then
                dv.Sort = "FaktorShomareh DESC"
            ElseIf rbSoodi.Checked = True Then
                dv.Sort = "FaktorShomareh ASC"
            End If

            TedadMandeh = txtTedadKala.Text

            If dv.Count = 0 Then
                If MsgBox("هیچ فاکتور مانده دار از این کالا در این دوره یافت نشد . آیا مایلید برای این کالا، مرجوعی بدون فاکتور ثبت شود ؟", MsgBoxStyle.Question + MsgBoxStyle.YesNo + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, "") = MsgBoxResult.Yes Then
                    Dim dr_AddBedoneFaktor As DataRow

                    dr_AddBedoneFaktor = dsForm.Tables("tbl_SearchFaktorForInsertAutomatic").NewRow()
                    dr_AddBedoneFaktor("ccFaktorTitr") = 0
                    dr_AddBedoneFaktor("CodeDoreh") = CodeDoreh
                    dr_AddBedoneFaktor("TedadGhabelMarjoee") = TedadMandeh
                    dr_AddBedoneFaktor("Gheymat") = objTools.ConvertNulls(objTools.DLookupOne("dbo.GetMablaghFrosh(" & txtCodeKala.Tag & "," & TarikhEmrooz & "," & CodeMahalFaal & "," & ccMoshtary & "," & sNoeMoshtary & ")", "tblFO_Faktor", "", "ccFaktorTitr Asc"), 0)
                    dsForm.Tables("tbl_SearchFaktorForInsertAutomatic").Rows.Add(dr_AddBedoneFaktor)
                Else
                    Exit Sub
                End If
            End If

            For Each dr In dv
                If TedadMandeh <> 0 Then
                    If TedadMandeh < dr("TedadGhabelMarjoee") Then
                        TedadGhabelSabt = TedadMandeh
                        TedadMandeh = 0
                    Else
                        TedadGhabelSabt = dr("TedadGhabelMarjoee")
                        TedadMandeh = TedadMandeh - dr("TedadGhabelMarjoee")
                    End If

                    InsertSatr(dr("ccFaktorTitr"), dr("CodeDoreh"), TedadGhabelSabt, dr("Gheymat"))
                End If
            Next

            If TedadMandeh > 0 Then
                If MsgBox("تعداد " & TedadMandeh & " عددبه علت اتمام فاکتور مانده دار از این کالا در این دوره ثبت نگردید . آیا مایلید برای این تعداد کالا، مرجوعی بدون فاکتور ثبت شود ؟", MsgBoxStyle.Question + MsgBoxStyle.YesNo + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, "") = MsgBoxResult.Yes Then
                    Dim Gheymat As Integer = 0
                    Gheymat = objTools.ConvertNulls(objTools.DLookupOne("dbo.GetMablaghFrosh(" & txtCodeKala.Tag & "," & TarikhEmrooz & "," & CodeMahalFaal & "," & ccMoshtary & "," & sNoeMoshtary & ")", "tblFO_Faktor", "", "ccFaktorTitr Asc"), 0)

                    InsertSatr(0, CodeDoreh, TedadMandeh, Gheymat)
                Else
                    Exit Sub
                End If
            End If

            cmSQL = Nothing : daSQL = Nothing
            cnSQL.Close()

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> InsertSatr_Automatic ")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> InsertSatr_Automatic ")
        End Try
    End Sub
    Private Sub Delete_MarjoeeVaset(ByVal Type As Boolean)
        '''' Type : False --> Delete Satr // True : Delete Titr
        Dim cnSQL As New SqlConnection
        Dim cmSQL As New SqlCommand
        Dim strSQL As String = ""
        Dim CodeCounter As Integer = 0

        If Type = False Then
            CodeCounter = Val(GridEXTitr.CurrentRow.Cells("ccMarjoeeSatr_Vaset").Text.Replace(",", ""))
        ElseIf Type = True Then
            CodeCounter = ccMarjoee_Vaset
        End If

        Try
            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()

            strSQL = "Sales.spElamMarjoee_InsertOneKala_DeleteMarjoeeVasetTitrSatr "

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            cmSQL.Parameters.AddWithValue("Type", Type)
            cmSQL.Parameters.AddWithValue("CodeCounter", CodeCounter)

            cmSQL.ExecuteNonQuery()

            cmSQL = Nothing
            cnSQL.Close()
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> Delete_MarjoeeVaset ")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> Delete_MarjoeeVaset ")
        End Try
    End Sub
    Private Sub ClearForm(ByVal Type As Boolean)
        ' Type --> False : Koli // True : Bedone txtCodeKala
        If Type = False Then
            txtCodeKala.Text = ""
        End If

        cmbCodeDorehFaktor.SelectedValue = CodeDoreh
        cmbsElatMarjoee.SelectedValue = 0
        rbSalem.Checked = False
        rbKharab.Checked = False
        lblTedadDarFaktor.Text = ""
        lblTedadMarjoeeAzGhabl.Text = ""
        lblTedadRezervShodeh.Text = ""
        lblTedadGhabelMarjoee.Text = ""
        lblFeeInFaktor.Text = ""
        txtTedadKala.Text = 0
        txtCodeKala.Focus()
    End Sub
    Private Sub tsmiDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsmiDelete.Click
        If dvTitr.Count = 0 Then
            Exit Sub
        End If

        Delete_MarjoeeVaset(False)
        Search()
    End Sub
    Private Sub frmFO_VorodKala_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        Try
            If ccMarjoee_Vaset <> 0 Then
                If objTools.DCount("ccMarjoeeSatr_Vaset", "tblFO_ElamMarjoeeSatr_Vaset", "ccMarjoee_Vaset = " & ccMarjoee_Vaset) <> 0 Then
                    MsgBox("تمامی مـوارد ثبت شــده حـــذف خواهـند شـد .", MsgBoxStyle.Information + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.OkOnly, "خـــروج")
                    Delete_MarjoeeVaset(True)
                End If
            End If

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> frmFO_AddNewPourSant_FormClosing ")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> frmFO_AddNewPourSant_FormClosing ")
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
                lblNameMoshtaryTitr.Text = objMoshtary.tNameMoshtary
                ccMoshtary = txtCodeMoshtary.Tag

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
        Criteria &= " And CodeMoshtary = '" & IIf(IsNothing(Me.txtCodeMoshtary.Text), 0, Me.txtCodeMoshtary.Text) & "'"

        Me.lblNameMoshtaryTitr.Text = objTools.ConvertNulls(objTools.DLookup("NameMoshtary", "qryFO_Moshtary", Criteria), "")
        Me.txtCodeMoshtary.Tag = objTools.ConvertNulls(objTools.DLookup("ccMoshtary", "qryFO_Moshtary", Criteria), 0)
        If IsNumeric(Me.txtCodeMoshtary.Tag) Then
            Me.ccMoshtary = Me.txtCodeMoshtary.Tag
        Else
            Me.ccMoshtary = 0
        End If
    End Sub
    Private Sub btnInsertKala_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnInsertKala.Click
        If lblNameMoshtaryTitr.Text = "" Then
            ErrPro.SetError(Me.txtCodeMoshtary, "مشتری را وارد کنید.")
            MsgBox("مشتری را وارد کنید.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
            Exit Sub
        End If
        ErrPro.SetError(Me.txtCodeMoshtary, "")

        ccMoshtary = ccMoshtary
        CodeFardMamorPakhsh = cmbMamorPakhsh.SelectedValue

        Mode = True
        SetForm()

        If ccMarjoee_Vaset = 0 Then
            InsertTitr()
        End If

    End Sub
    Private Sub btnExit_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        If ccMarjoee_Vaset <> 0 Then
            If dvTitr.Count <> 0 Then
                If MsgBox("در صورت خــروج و تاییــد نکردن مــوارد ذخیـره شده، هیچ اعلام مرجوعی ثبت نخواهد شده" & vbCrLf _
                         & " و تمامی مــوارد ذخیـره شده حــذف خواهنـد شد . آیا خــروج انجــام شــود ؟" & vbCrLf _
                         , MsgBoxStyle.Question + MsgBoxStyle.YesNo + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, "خـــروج") = MsgBoxResult.Yes Then
                    Delete_MarjoeeVaset(True)
                Else
                    Exit Sub
                End If
            End If
        End If

        Me.Close()
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
    Private Sub SetForm_NoeVorod(ByVal Type As Boolean)
        ' -- Type --> False : Dasti // True : Automatic
        If Type = False Then
            rbSalem.Checked = False
            rbKharab.Checked = False
            rbNozoli.Checked = False
            rbSoodi.Checked = False

            grbBtnShowFaktor.Enabled = True
            grbFaktor.Enabled = True
            grbSabtAzFaktor.Enabled = False
        ElseIf Type = True Then
            rbSalem.Checked = False
            rbKharab.Checked = False
            rbNozoli.Checked = True
            rbSoodi.Checked = False

            grbBtnShowFaktor.Enabled = False
            grbFaktor.Enabled = False
            grbSabtAzFaktor.Enabled = True

            SearchFaktor(0, False)
        End If

        ClearForm(True)
    End Sub
    Private Sub txtTedadKala_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtTedadKala.TextChanged
        If rbAutomatic.Checked = True Then
            lblTedadDarFaktor.Text = "عـطـف بـه فـاکتـور"
            lblTedadMarjoeeAzGhabl.Text = "عـطـف بـه فـاکتـور"
            lblTedadRezervShodeh.Text = "عـطـف بـه فـاکتـور"
            lblTedadGhabelMarjoee.Text = txtTedadKala.Text
            lblFeeInFaktor.Text = "عـطـف بـه فـاکتـور"
        End If
    End Sub
    Private Sub CreateMarjoee()
        Dim cnSQL As New SqlConnection
        Dim cmSQL As New SqlCommand
        Dim daSQL As New SqlDataAdapter
        Dim dv As New DataView
        Dim dr As DataRowView
        Dim strSQL As String = ""

        If dsForm.Tables.Contains("tblTaeed_SearchFaktor") Then
            dsForm.Tables.Remove("tblTaeed_SearchFaktor")
        End If

        Try
            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()

            strSQL = "Sales.spElamMarjoee_InsertOneKala_Taeed_SearchFaktor "

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            cmSQL.Parameters.AddWithValue("ccMarjoee_Vaset", ccMarjoee_Vaset)

            daSQL = New SqlDataAdapter(cmSQL)
            daSQL.Fill(dsForm, "tblTaeed_SearchFaktor")

            dv = New DataView(dsForm.Tables("tblTaeed_SearchFaktor"))

            For Each dr In dv
                CreateTitrMarjoee(dr("ccFaktorTitr"))
            Next

            cmSQL = Nothing : daSQL = Nothing
            cnSQL.Close()

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> CreateMarjoee ")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> CreateMarjoee ")
        End Try
    End Sub
    Private Sub CreateTitrMarjoee(ByVal ccFaktor As Integer)
        Dim cnSQL As New SqlConnection
        Dim cmSQL As New SqlCommand
        Dim daSQL As New SqlDataAdapter
        Dim dv As New DataView
        Dim dr As DataRowView
        Dim strSQL As String = ""
        Dim ccElamMarjoee As Integer = 0

        If dsForm.Tables.Contains("tblTaeed_SearchKalaInFaktor") Then
            dsForm.Tables.Remove("tblTaeed_SearchKalaInFaktor")
        End If

        Try
            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()

            strSQL = "Sales.spElamMarjoee_InsertOneKala_Taeed_CreateTitrMarjoee "

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            cmSQL.Parameters.AddWithValue("ccMarjoee_Vaset", ccMarjoee_Vaset)
            cmSQL.Parameters.AddWithValue("ccFaktorTitr", ccFaktor)
            cmSQL.Parameters.AddWithValue("ccElamMarjoee", ccElamMarjoee)
            cmSQL.Parameters("ccElamMarjoee").Direction = ParameterDirection.Output

            cmSQL.ExecuteNonQuery()

            ccElamMarjoee = cmSQL.Parameters("ccElamMarjoee").Value

            cmSQL = Nothing

            '--------------------------------------------

            strSQL = "Sales.spElamMarjoee_InsertOneKala_Taeed_SearchKalaInFaktor "

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            cmSQL.Parameters.AddWithValue("ccMarjoee_Vaset", ccMarjoee_Vaset)
            cmSQL.Parameters.AddWithValue("ccFaktorTitr", ccFaktor)

            daSQL = New SqlDataAdapter(cmSQL)
            daSQL.Fill(dsForm, "tblTaeed_SearchKalaInFaktor")

            dv = New DataView(dsForm.Tables("tblTaeed_SearchKalaInFaktor"))

            For Each dr In dv
                InsertKalaInMarjoee(ccElamMarjoee, ccFaktor, dr("ccKala"), dr("Tedad"), dr("Fee"), dr("sElatMarjoee"), dr("NoeMarjoee"), dr("CodeDorehFaktor"))
            Next

            If AllowPishFaktorTakhfifDasty = False Then
                If ccFaktor <> 0 Then
                    'TJ = New TakhfifOJavaiez.TakhfifJayezeh(ccFaktor, ccElamMarjoee)
                    '    TJ.ApplyTakhfifJayezeh()
                    RollBackMalyatAvarez(ccFaktor, ccElamMarjoee)
                End If
            End If

            cmSQL = Nothing : daSQL = Nothing
            cnSQL.Close()

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> CreateTitrMarjoee ")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> CreateTitrMarjoee ")
        End Try
    End Sub
    Private Sub InsertKalaInMarjoee(ByVal ccMarjoee As Integer, ByVal ccFaktorTitr As Integer, ByVal ccKala As Integer, ByVal Tedad As Double, ByVal Fee As Integer, ByVal sElatMarjoee As Integer, ByVal NoeMarjoee As Integer, ByVal CodeDorehFaktor As Integer)
        Dim cnSQL As New SqlConnection
        Dim cmSQL As New SqlCommand
        Dim strSQL As String = ""

        Try
            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()

            strSQL = "Sales.spElamMarjoee_InsertOneKala_Taeed_InsertKalaInSatrMarjoee "

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            cmSQL.Parameters.AddWithValue("ccMarjoee", ccMarjoee)
            cmSQL.Parameters.AddWithValue("ccFaktorTitr", ccFaktorTitr)
            cmSQL.Parameters.AddWithValue("ccKala", ccKala)
            cmSQL.Parameters.AddWithValue("Tedad", Tedad)
            cmSQL.Parameters.AddWithValue("Fee", Fee)
            cmSQL.Parameters.AddWithValue("sElatMarjoee", sElatMarjoee)
            cmSQL.Parameters.AddWithValue("NoeMarjoee", NoeMarjoee)
            cmSQL.Parameters.AddWithValue("CodeDorehFaktor", CodeDorehFaktor)

            cmSQL.ExecuteNonQuery()

            cmSQL = Nothing
            cnSQL.Close()

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> InsertKalaInMarjoee ")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> InsertKalaInMarjoee ")
        End Try
    End Sub
    Private Sub UpdateVazeiatSanad()
        Dim cnSQL As New SqlConnection
        Dim cmSQL As New SqlCommand
        Dim strSQL As String = ""

        Try
            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()

            strSQL = "Sales.spElamMarjoee_InsertOneKala_Taeed_UpdateVazeiatSanad "

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            cmSQL.Parameters.AddWithValue("ccMarjoee_Vaset", ccMarjoee_Vaset)

            cmSQL.ExecuteNonQuery()

            cmSQL = Nothing
            cnSQL.Close()

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> InsertKalaInMarjoee ")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> InsertKalaInMarjoee ")
        End Try
    End Sub
    Private Sub btnClearForm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearForm.Click
        ClearForm(False)
    End Sub
    Private Sub btnCreateMarjoee_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCreateMarjoee.Click
        If dvTitr.Count = 0 Then
            MsgBox("رکوردی ثبت نگردیده است .", MsgBoxStyle.Information + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.OkOnly, "")
            Exit Sub
        End If

        CreateMarjoee()

        Mode = False
        SetForm()
    End Sub
    Private Sub RollBackMalyatAvarez(ByVal ccFaktor As Integer, ByVal ccElamMarjoee As Integer)
        Dim strSQL As String = ""
        Dim cn As New SqlConnection(ConnectionString)
        Dim da As SqlDataAdapter = Nothing
        Dim dt As New DataTable
        Dim cm As SqlCommand = Nothing
        Dim M As Double = 0
        Dim AvarezOneKala As Double = 0
        Dim MaliyatOneKala As Double = 0

        Dim TarikhFaktor As String = objTools.DLookup("FaktorTarikh", "tblFO_Faktor", "ccFaktorTitr = " & ccFaktor)

        'strSQL = "SELECT ccMarjoeeSatr,ccKala,MKol,Tedad3 FROM tblFO_ElamMarjoeeSatr WHERE ccFaktorTitr = " & ccFaktor & " AND IsJayezehMarjoee = 0"
        strSQL = "SELECT ccMarjoeeSatr,ccKala,MKol,Tedad3 FROM tblFO_ElamMarjoeeSatr WHERE ccFaktorTitr = " & ccFaktor & " AND IsJayezehMarjoee = 0 AND ccMarjoee = " & ccElamMarjoee
        cn.Open()

        da = New SqlDataAdapter(strSQL, cn)
        Try

            da.Fill(dt)

            For Each dr As DataRow In dt.Rows

                AvarezOneKala = objTools.DLookup("(MablaghAvarez) / Tedad3", "tblFO_FaktorSatr", "ccFaktorTitr = " & ccFaktor & " AND ccKala = " & dr("ccKala") & " AND IsJayezeh = 0 ")
                MaliyatOneKala = objTools.DLookup("(MablaghMalyat) / Tedad3", "tblFO_FaktorSatr", "ccFaktorTitr = " & ccFaktor & " AND ccKala = " & dr("ccKala") & " AND IsJayezeh = 0 ")
                M = (dr("Tedad3") * AvarezOneKala) + (dr("Tedad3") * MaliyatOneKala)

                'Dim TJ As New TakhfifOJavaiez.TakhfifJayezeh(ccFaktor, dr("ccKala"), TakhfifOJavaiez.TakhfifJayezeh.ApplyOnTypes.Faktor)
                'Dim DarsadAvarez = objTools.DLookupOne("DarsadAvarez", "tblFO_MalyatAvarez ", "AzTarikh < " & TarikhFaktor, "  AzTarikh desc")
                'Dim DarsadMalyat = objTools.DLookupOne("DarsadMalyat", "tblFO_MalyatAvarez ", "AzTarikh < " & TarikhFaktor, "  AzTarikh desc")
                'Dim MA = DarsadAvarez + DarsadMalyat

                'M = (dr("MKol") - objTools.ConvertNulls(TJ.GetTakhfifKala, 0)) * MA / 100

                strSQL = "UPDATE tblFO_ElamMarjoeeSatr SET "
                strSQL &= " MablaghMaliatAvarez = " & Math.Round(M, 0)
                strSQL &= " WHERE ccMarjoeeSatr = " & dr("ccMarjoeeSatr")
                strSQL &= " And ccKala In (Select ccKala From tblAN_Kala where MashmuleMaliyat = 1 OR MashmuleAvarez = 1)"
                strSQL &= " And ccFaktorTitr in ( SELECT ccFaktorTitr FROM tblFO_Faktor WHERE ccFaktorTitr = " & ccFaktor & " AND JamMablaghAvarez <> 0)"

                cm = New SqlCommand(strSQL, cn)
                cm.ExecuteNonQuery()

                'Dim IsMalyatAvarezTakhfif As Boolean = objTools.DLookup("IsMalyatAvarezTakhfif", "tblGL_SysConfig", "CodeMahal = " & CodeMahalFaal)

                If objTools.DLookup("MkolTakhfifMalyatAvarez", "tblFO_Faktor", "ccFaktorTitr = " & ccFaktor) <> 0 Then
                    If IsMalyatAvarezTakhfif Then
                        strSQL = "UPDATE tblFO_ElamMarjoeeSatr SET "
                        strSQL &= " TakhfifMalyatAvarez = " & Math.Round(M, 0)
                        strSQL &= " WHERE ccMarjoeeSatr = " & dr("ccMarjoeeSatr")
                        strSQL &= " And ccKala In (Select ccKala From tblAN_Kala where MashmuleMaliyat = 1 OR MashmuleAvarez = 1)"
                        strSQL &= " And ccFaktorTitr in ( SELECT ccFaktorTitr FROM tblFO_Faktor WHERE ccFaktorTitr = " & ccFaktor & " AND JamMablaghAvarez <> 0)"

                        cm = New SqlCommand(strSQL, cn)
                        cm.ExecuteNonQuery()
                    End If

                End If

            Next
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
End Class

'' Sales.spElamMarjoee_InsertOneKala_SearchFaktor
'' Sales.spElamMarjoee_InsertOneKala_Search
'' Sales.spElamMarjoee_InsertOneKala_InsertMarjoeeVasetTitr
'' Sales.spElamMarjoee_InsertOneKala_InsertMarjoeeVasetSatr
'' Sales.spElamMarjoee_InsertOneKala_SearchFaktor
'' Sales.spElamMarjoee_InsertOneKala_DeleteMarjoeeVasetTitrSatr
'' Sales.spElamMarjoee_InsertOneKala_Taeed_SearchFaktor
'' Sales.spElamMarjoee_InsertOneKala_Taeed_CreateTitrMarjoee
'' Sales.spElamMarjoee_InsertOneKala_Taeed_SearchKalaInFaktor
'' Sales.spElamMarjoee_InsertOneKala_Taeed_InsertKalaInSatrMarjoee
'' Sales.spElamMarjoee_InsertOneKala_Taeed_UpdateVazeiatSanad