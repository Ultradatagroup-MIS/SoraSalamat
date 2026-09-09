Imports System.Data
Imports System.Data.SqlClient
Public Class DP_rpt_Enteghalat
#Region "Variable AND Constant Declration"
    Private objCode As New UD_Dll.Code
    Dim dvForm As New DataView
    Dim dsForm As New DataSet
    Dim txtCaption As String
    Dim mem As Integer = 0
    Const FormTableName = "Sales.AdamDarkhast"
    Dim ErrPro As New ErrorProvider
    Private SN As Integer
    Dim level As Integer = 0
    Dim Mode As UD_Dll.Enums.GL_ModeForms = UD_Dll.Enums.GL_ModeForms.AddNewRecord
    Const cntCodeSubSystem As Long = 100064
    Dim NoeHesab As Integer
    Dim flg As Boolean = False
    Public Enum Hesab As Integer
        Sandogh = 1
        Bank = 2
    End Enum
#End Region
#Region " Form Event Code "
    Private Sub DP_rpt_Enteghalat_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetParameter()

        SN = objSec.GetSecurityNumber(cntCodeSubSystem, UserName)
        Try
            objTools.ChangeKbd(UD_Dll.mdlUtility.KBD.Farsi)

            'Set Default Tarikh
            'If (TarikhEmrooz.Substring(5, 2) >= "01") And (TarikhEmrooz.Substring(5, 2) <= "06") Then
            '    Dim dt As DateTime = Now.AddDays(-31)
            '    mskAzTarikh.Text = TarikhEmrooz
            'Else
            '    Dim dt As DateTime = Now.AddDays(-30)
            '    mskAzTarikh.Text = TarikhEmrooz
            'End If

            If (TarikhEmrooz.Substring(5, 2) >= "01") And (TarikhEmrooz.Substring(5, 2) <= "06") Then
                Dim dt As DateTime = Now.AddDays(-31)
                mskAzTarikh.Text = objTarikh.Mi2Sh(dt)
                mskTaTarikh.Text = TarikhEmrooz
            Else
                Dim dt As DateTime = Now.AddDays(-30)
                mskAzTarikh.Text = objTarikh.Mi2Sh(dt)
                mskTaTarikh.Text = TarikhEmrooz
            End If

            LoadCombo()
            ClearForm()

            flg = True
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->frmTakhsisha_Load")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->frmTakhsisha_Load")
        End Try

    End Sub
    Private Sub SetReport()

        Dim strsql As String
        Dim cnsql As New SqlConnection
        Dim cmsql As New SqlCommand
        Dim dasql As SqlDataAdapter

        Try
            If dsForm.Tables.Contains(FormTableName) = True Then
                dsForm.Tables.Remove(FormTableName)
            End If
            cnsql = New SqlConnection(ConnectionString)
            cnsql.Open()

            strsql = "TabletPC.ReportAdameDarkhast"

            cmsql = New SqlCommand(strsql, cnsql)
            cmsql.CommandType = CommandType.StoredProcedure
            cmsql.Parameters.Clear()

            cmsql.Parameters.AddWithValue("ccMoshtary", IIf(txtCodeMoshtary.Text.Length = 0, 0, txtCodeMoshtary.Tag))
            cmsql.Parameters.AddWithValue("ccForoshandeh", IIf(IsNothing(cmbNameForoshandeh.SelectedValue), 0, cmbNameForoshandeh.SelectedValue))
            cmsql.Parameters.AddWithValue("AzTarikh", IIf(IsNothing(mskAzTarikh.Text), "", mskAzTarikh.Text))
            cmsql.Parameters.AddWithValue("TaTarikh", IIf(IsNothing(mskTaTarikh.Text), "", mskTaTarikh.Text))
            cmsql.Parameters.AddWithValue("ElatAdamDarkhast", IIf(IsNothing(cmbElat.SelectedValue), "", cmbElat.SelectedValue))

            dasql = New SqlDataAdapter(cmsql)
            dasql.Fill(dsForm, FormTableName)
            dvForm = New DataView
            dvForm = dsForm.Tables(FormTableName).DefaultView
            dvForm.Sort = "ccAdamDarkhast"
            dvForm.AllowDelete = True
            dvForm.AllowEdit = False
            dvForm.AllowNew = False
            dasql = Nothing

            cnsql.Close()
            cmsql = Nothing
            cnsql = Nothing

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->RefreshTitrdata")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->RefreshTitrdata")
        End Try

    End Sub
    Private Sub SetGridStyle()
        GridEX1.Visible = True
        With GridEX1
            .DataSource = Nothing
            .DataSource = dsForm.Tables(FormTableName).DefaultView
            .SetDataBinding(dsForm.Tables(FormTableName).DefaultView, "")
            .RetrieveStructure()
        End With

        For i As Integer = 0 To GridEX1.CurrentTable.Columns.Count - 1
            GridEX1.CurrentTable.Columns.Item(i).Visible = False
        Next


        GridEX1.CurrentTable.Columns.Item("NameMoshtary").Caption = "نام مشتری"
        GridEX1.CurrentTable.Columns.Item("NameMoshtary").Visible = True
        GridEX1.CurrentTable.Columns.Item("NameMoshtary").Width = 200
        GridEX1.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
        GridEX1.CurrentTable.Columns.Item("NameMoshtary").Position = 0

        'GridEX1.DataSource = dsForm.Tables("tblElat")
        GridEX1.CurrentTable.Columns.Item("NameForoshandeh").Caption = "نام فروشنده"
        GridEX1.CurrentTable.Columns.Item("NameForoshandeh").Visible = True
        GridEX1.CurrentTable.Columns.Item("NameForoshandeh").Width = 150
        GridEX1.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
        GridEX1.CurrentTable.Columns.Item("NameForoshandeh").Position = 1


        GridEX1.CurrentTable.Columns.Item("Tarikh").Caption = "تاریخ"
        GridEX1.CurrentTable.Columns.Item("Tarikh").Visible = True
        GridEX1.CurrentTable.Columns.Item("Tarikh").Width = 100
        GridEX1.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
        GridEX1.CurrentTable.Columns.Item("Tarikh").Position = 2

        GridEX1.CurrentTable.Columns.Item("Saat").Caption = "ساعت"
        GridEX1.CurrentTable.Columns.Item("Saat").Visible = True
        GridEX1.CurrentTable.Columns.Item("Saat").Width = 100
        GridEX1.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
        GridEX1.CurrentTable.Columns.Item("Saat").Position = 3

        GridEX1.CurrentTable.Columns.Item("ElatAdamDarkhast").Caption = "علت عدم درخواست"
        GridEX1.CurrentTable.Columns.Item("ElatAdamDarkhast").Visible = True
        GridEX1.CurrentTable.Columns.Item("ElatAdamDarkhast").Width = 150
        GridEX1.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
        GridEX1.CurrentTable.Columns.Item("ElatAdamDarkhast").Position = 4

        For i As Integer = 0 To GridEX1.RootTable.Columns.Count - 1
            If GridEX1.RootTable.Columns(i).Type.IsValueType Then
                GridEX1.RootTable.Columns(i).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                GridEX1.RootTable.Columns(i).FormatMode = Janus.Windows.GridEX.FormatMode.UseIFormattable
                GridEX1.RootTable.Columns(i).FormatString = "N"
                GridEX1.RootTable.Columns(i).TotalFormatMode = Janus.Windows.GridEX.FormatMode.UseIFormattable
                GridEX1.RootTable.Columns(i).TotalFormatString = "N"
            End If
        Next
        Me.CenterToScreen()
    End Sub
#End Region
#Region "Global Form Code"
    Private Sub ClearForm()

        cmbNameForoshandeh.SelectedIndex = -1
        cmbNameForoshandeh.SelectedIndex = -1

        cmbElat.SelectedIndex = -1
        cmbElat.SelectedIndex = -1
    End Sub
    Private Sub LoadCombo()

        Dim Strsql As String
        Dim daSQL As SqlDataAdapter
        Dim cnsql As New SqlConnection
        Dim cmsql As SqlCommand
        Dim dr As DataRow

        If dsForm.Tables.Contains("NameForoshandeh") = True Then
            dsForm.Tables.Remove("NameForoshandeh")
        End If

        cnsql = New SqlConnection(ConnectionString)
        cnsql.Open()

        Strsql = "Global.spForoshandeh_LoadCombo"

        cmsql = New SqlCommand(Strsql, cnsql)
        cmsql.CommandType = CommandType.StoredProcedure
        cmsql.Parameters.Clear()

        cmsql.Parameters.AddWithValue("CodeMahal", CodeMahalFaal)
        cmsql.Parameters.AddWithValue("sVazeiat", 0)
        cmsql.Parameters.AddWithValue("UserName", "")

        daSQL = New SqlDataAdapter(cmsql)

        daSQL.Fill(dsForm, "NameForoshandeh")

        dr = dsForm.Tables("NameForoshandeh").NewRow
        dr("ccForoshandeh") = 0
        dr("NameForoshandeh") = "همــه"
        dsForm.Tables("NameForoshandeh").Rows.Add(dr)

        cmbNameForoshandeh.DataSource = Nothing
        cmbNameForoshandeh.Items.Clear()
        cmbNameForoshandeh.DataSource = dsForm.Tables("NameForoshandeh").DefaultView
        cmbNameForoshandeh.DisplayMember = "NameForoshandeh"
        cmbNameForoshandeh.ValueMember = "ccForoshandeh"
        daSQL = Nothing

        Strsql = "Global.spElatAdameDarkhastForoshandeh_LoadCombo"

        cmsql = New SqlCommand(Strsql, cnsql)
        cmsql.CommandType = CommandType.StoredProcedure
        cmsql.Parameters.Clear()

        daSQL = New SqlDataAdapter(cmsql)
        daSQL.Fill(dsForm, "ElatAdamDarkhast")
        cmbElat.DataSource = Nothing
        cmbElat.Items.Clear()
        cmbElat.DataSource = dsForm.Tables("ElatAdamDarkhast").DefaultView
        cmbElat.DisplayMember = "Sharh"
        cmbElat.ValueMember = "Code"
        daSQL = Nothing

    End Sub
    Private Function IsValidField(ByVal chkField As String) As Boolean

        Try
            IsValidField = False

            If chkField = "mskTarikh" Or chkField = "All" Then
                If Len(mskAzTarikh.Text.ToString) <> 0 Then
                    If Not objTarikh.IsShDate(mskAzTarikh.Text.ToString) Then
                        mskAzTarikh.Focus()
                        Exit Function
                    End If
                    If mskAzTarikh.Text.Substring(0, 4) <> CodeDoreh Then
                        MsgBox("تاريخ با دوره مالی فعال يکی نيست.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
                        mskAzTarikh.Focus()
                        Exit Function
                    End If
                    If mskAzTarikh.Text > TarikhEmrooz Then
                        MsgBox("تاریخ وارد شده از تاریخ امروز جلوتر است.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
                        mskAzTarikh.Focus()
                        Exit Function
                    End If
                Else
                    ErrPro.SetError(Me.mskAzTarikh, "تا تاریخ را وارد کنید.")
                    MsgBox("تا تاریخ را وارد کنید.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
                    mskAzTarikh.Focus()
                    Exit Function
                End If
                ErrPro.SetError(Me.mskAzTarikh, "")
            End If
            Return True

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->IsValidRow")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->IsValidRow")
        End Try

    End Function
    Private Sub SetParameter()

        Dim commands As String = Microsoft.VisualBasic.Command()
        If commands.Length = 0 Then

            UserName = "Administrator"
            UserPassWord = "66998833"
            NameMahalFaal = "تهران"
            CodeMahalFaal = "1"
            PersonelCode = "0"
            PersonelName = "Administrator"
            CodeDoreh = "1392"
            txtCaption = "گزارش عدم درخواست فروشنده"
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
#End Region
#Region " Button Event "
    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        objCode.UserName = UserName
        If UserName.ToLower <> "administrator" Then
            If Not objCode.CheckPermission(100066) Then Exit Sub
        End If

        PrintGozaresh(False)

    End Sub
    Private Sub btnExit_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.Close()
    End Sub
    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub
    Private Sub btnReport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReport.Click
        IsValidField(True)
        SetReport()
        SetGridStyle()
    End Sub
#End Region
    Private Sub PrintGozaresh(ByVal WithCriteria As Boolean)
        Try
            Dim cnSQL As SqlConnection
            Dim strSQL As String


            Windows.Forms.Cursor.Current = Cursors.WaitCursor
            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()



            If dsForm.Tables.Contains("fn_TarazMarkaz") Then
                dsForm.Tables.Remove("fn_TarazMarkaz")
            End If

            Dim daSQL As SqlDataAdapter
            daSQL = New SqlDataAdapter(strSQL, cnSQL)
            daSQL.Fill(dsForm, "fn_TarazMarkaz")


            Dim rpt As New ReportDocument
            Dim rpttables As Tables
            Dim rptformula As FormulaFieldDefinitions
            Dim frm As New Forms_dll.frmGL_Gozaresh

            rpt.Load(rptPath & "\rptFO_GozareshTarazRozaneMarkaz.rpt")

            rpttables = rpt.Database.Tables
            rpttables.Item(0).SetDataSource(dsForm.Tables("fn_TarazMarkaz"))

            rptformula = rpt.DataDefinition.FormulaFields
            With rptformula



                .Item("Sharh").Text = "{mydata.Sharh}"
                .Item("bed").Text = "{mydata.bed}"
                .Item("bes").Text = "{mydata.bes}"



                .Item("Title").Text = "'" & "گزارش تراز روزانه مرکز" & "'"
                .Item("Title2").Text = "'" & NameSherkat & "'"
                .Item("Title3").Text = "'" & NameMahalFaal & "'"
                .Item("KarbarGozaresh").Text = "'" & PersonelName & "'"
                .Item("TarikhGozaresh").Text = "'" & objTarikh.SetDateSlash(TarikhEmrooz) & "'"
                .Item("SaatGozaresh").Text = "'" & Format(TimeOfDay, "HH:mm:ss") & "'"



            End With
            rpt.Refresh()

            frm.Text = "گزارش تراز روزانه مرکز"

            frm.WindowState = FormWindowState.Maximized
            With frm.CRV
                .ReportSource = rpt
                .DisplayGroupTree = False
                .ShowGroupTreeButton = False
                .Zoom(100)
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
    'Private Sub txtCodeMoshtary_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCodeMoshtary.TextChanged
    '    'If Mode = UD_Dll.Enums.GL_ModeForms.UpdateRecord Then Exit Sub

    '    Dim Criteria As String = ""
    '    If rbMoshtaryRooz.Checked Then
    '        Dim tTarikh As String
    '        If mskTarikh.Text = "" Then
    '            tTarikh = TarikhEmrooz
    '        Else
    '            tTarikh = mskTarikh.Text
    '        End If
    '        Criteria = " ccForoshandeh = " & IIf(IsNothing(cmbBazaryab.SelectedValue), 0, cmbBazaryab.SelectedValue)
    '        Criteria &= " And CodeMoshtary = '" & IIf(IsNothing(Me.txtCodeMoshtary.Text), 0, Me.txtCodeMoshtary.Text) & "'"
    '        Criteria &= " Order By NameMasir"
    '        Me.lblNameMoshtary.Text = objTools.ConvertNulls(objTools.DLookup("NameMoshtary", "fnFO_GetNobatVisitForoshandeh('" & tTarikh & "') as Main ", Criteria), "")
    '        Me.txtCodeMoshtary.Tag = objTools.ConvertNulls(objTools.DLookup("ccMoshtary", "fnFO_GetNobatVisitForoshandeh('" & tTarikh & "')", Criteria), "")
    '    ElseIf rbMoshtaryForoshandeh.Checked Then
    '        Criteria = "CodeMahal=" & CodeMahalFaal & " AND ccForoshandeh = " & IIf(IsNothing(cmbBazaryab.SelectedValue), 0, cmbBazaryab.SelectedValue)
    '        Criteria &= " AND Not exists (Select * From tblGL_SecurityData where namekarbar= '" & UserName & "' and CodeSubSystem = 614 and pk = qryFO_Moshtary.ccMoshtary) "
    '        Criteria &= " And CodeMoshtary = '" & IIf(IsNothing(Me.txtCodeMoshtary.Text), 0, Me.txtCodeMoshtary.Text) & "'"
    '        Criteria &= " AND sVazeiat = " & UD_Dll.Enums.FO_VaziatMoshtary.Faal
    '        Me.lblNameMoshtary.Text = objTools.ConvertNulls(objTools.DLookup("NameMoshtary", "qryFO_Moshtary", Criteria), "")
    '        Me.txtCodeMoshtary.Tag = objTools.ConvertNulls(objTools.DLookup("ccMoshtary", "qryFO_Moshtary", Criteria), "")
    '    ElseIf rbMoshtaryKol.Checked Then
    '        Criteria = "CodeMahal=" & CodeMahalFaal
    '        Criteria &= " AND Not exists (Select * From tblGL_SecurityData where namekarbar= '" & UserName & "' and CodeSubSystem = 614 and pk = qryFO_Moshtary.ccMoshtary) "
    '        Criteria &= " And CodeMoshtary = '" & IIf(IsNothing(Me.txtCodeMoshtary.Text), 0, Me.txtCodeMoshtary.Text) & "'"
    '        Criteria &= " AND sVazeiat = " & UD_Dll.Enums.FO_VaziatMoshtary.Faal
    '        Me.lblNameMoshtary.Text = objTools.ConvertNulls(objTools.DLookup("NameMoshtary", "qryFO_Moshtary", Criteria), "")
    '        Me.txtCodeMoshtary.Tag = objTools.ConvertNulls(objTools.DLookup("ccMoshtary", "qryFO_Moshtary", Criteria), "")
    '    End If


    '    If lblNameMoshtary.Text <> "" Then
    '        LoadMoshtaryAddress()
    '        LoadMoshtaryAfrad()
    '        cmbNoePardakht.SelectedValue = objTools.DLookup("sNoePardakht", "tblFO_Moshtary", "ccMoshtary = " & Me.txtCodeMoshtary.Tag)
    '    Else
    '        If dsForm.Tables.Contains("tblAddress") Then
    '            dsForm.Tables.Remove("tblAddress")
    '        End If
    '        cmbAddress.DataSource = Nothing
    '        cmbAddress.Items.Clear()

    '        If dsForm.Tables.Contains("tblMoshtaryAfrad") Then
    '            dsForm.Tables.Remove("tblMoshtaryAfrad")
    '        End If
    '        cmbMoshtaryAfrad.DataSource = Nothing
    '        cmbMoshtaryAfrad.Items.Clear()
    '    End If
    '    If lblNameMoshtary.Text <> "" Then
    '        lblTabloMoshtary.Text = objTools.DLookup("NameTablo", "tblFO_Moshtary", "ccMoshtary = " & Me.txtCodeMoshtary.Tag)
    '        lblTellMoshtary.Text = objTools.DLookup("Telephone", "tblFO_MoshtaryAddress", "ccMoshtary = " & Me.txtCodeMoshtary.Tag)
    '    Else
    '        lblTabloMoshtary.Text = ""
    '        lblTellMoshtary.Text = ""
    '    End If

    '    'If lblNameMoshtary.Text <> "" Then
    '    '    If objTools.DCount("ccFaktorTitr", "tblFO_Faktor", "ccMoshtary = " & Me.txtCodeMoshtary.Tag) < 4 Then
    '    '        cmbNoePardakht.SelectedIndex = 4
    '    '        cmbNoePardakht.Enabled = False
    '    '    End If
    '    'End If
    'End Sub
    Private Sub txtCodeMoshtary_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCodeMoshtary.KeyPress
        Try
            If (Asc(e.KeyChar()) < 48 Or Asc(e.KeyChar()) > 57) And (Asc(e.KeyChar()) <> 8) Then
                e.Handled = True
            End If
            If e.KeyChar = Chr(Keys.Space) Then
                Dim objMoshtary As New Forms_dll.frmFO_MoshtarySearch
                Dim StrSql As String = ""

                StrSql = "Select * from qryFO_Moshtary Where CodeMahal=" & CodeMahalFaal & " AND sVazeiat = " & UD_Dll.Enums.FO_VaziatMoshtary.Faal
                StrSql &= "AND Not exists (Select * From tblGL_SecurityData where namekarbar= '" & UserName & "' and CodeSubSystem = 614 and pk = qryFO_Moshtary.ccMoshtary) "
                StrSql &= " AND Not exists (Select * From tblGL_SecurityData where namekarbar= '" & UserName & "' and CodeSubSystem = 10000 and pk = qryFO_Moshtary.sNoeMoshtary) "
                StrSql &= " order by sMantagheh,sMahaleh,NameMoshtary"

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
    Private Sub txtCodeMoshtary_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCodeMoshtary.TextChanged

        Dim Criteria As String = ""

        Criteria = "CodeMahal=" & CodeMahalFaal
        Criteria &= " AND Not exists (Select * From tblGL_SecurityData where namekarbar= '" & UserName & "' and CodeSubSystem = 614 and pk = qryFO_Moshtary.ccMoshtary) "
        Criteria &= " And CodeMoshtary = '" & IIf(IsNothing(Me.txtCodeMoshtary.Text), 0, Me.txtCodeMoshtary.Text) & "'"
        Criteria &= " AND sVazeiat = " & UD_Dll.Enums.FO_VaziatMoshtary.Faal
        Me.lblNameMoshtary.Text = objTools.ConvertNulls(objTools.DLookup("NameMoshtary", "qryFO_Moshtary", Criteria), "")
        Me.txtCodeMoshtary.Tag = objTools.ConvertNulls(objTools.DLookup("ccMoshtary", "qryFO_Moshtary", Criteria), "")
    End Sub
    Private Sub btnGozaresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGozaresh.Click
        Try
            Dim cnsql As New SqlConnection
            Dim cmsql As New SqlCommand
            Dim dasql As SqlDataAdapter
            Dim strSQL As String

            Windows.Forms.Cursor.Current = Cursors.WaitCursor
            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()

            strSQL = "TabletPC.ReportAdameDarkhast"
            cmsql = New SqlCommand(strSQL, cnSQL)
            cmsql.CommandType = CommandType.StoredProcedure
            cmsql.Parameters.Clear()

            cmsql.Parameters.AddWithValue("ccMoshtary", IIf(txtCodeMoshtary.Text.Length = 0, 0, txtCodeMoshtary.Tag))
            cmsql.Parameters.AddWithValue("ccForoshandeh", IIf(IsNothing(cmbNameForoshandeh.SelectedValue), 0, cmbNameForoshandeh.SelectedValue))
            cmsql.Parameters.AddWithValue("AzTarikh", IIf(IsNothing(mskAzTarikh.Text), "", mskAzTarikh.Text))
            cmsql.Parameters.AddWithValue("TaTarikh", IIf(IsNothing(mskTaTarikh.Text), "", mskTaTarikh.Text))
            cmsql.Parameters.AddWithValue("ElatAdamDarkhast", IIf(IsNothing(cmbElat.SelectedValue), "", cmbElat.SelectedValue))

            If dsForm.Tables.Contains("Sales.AdamDarkhast") Then
                dsForm.Tables.Remove("Sales.AdamDarkhast")
            End If

            dasql = New SqlDataAdapter(cmsql)
            daSQL.Fill(dsForm, "Sales.AdamDarkhast")


            Dim rpt As New ReportDocument
            Dim rpttables As Tables
            Dim rptformula As FormulaFieldDefinitions
            Dim frm As New Forms_dll.frmGL_Gozaresh

            rpt.Load(rptPath & "\rptFO_AdamDarkhastForoshandeh.rpt")

            rpttables = rpt.Database.Tables
            rpttables.Item(0).SetDataSource(dsForm.Tables("Sales.AdamDarkhast"))

            rptformula = rpt.DataDefinition.FormulaFields
            With rptformula
                .Item("NameMoshtary").Text = "{mydata.NameMoshtary}"
                .Item("NameForoshandeh").Text = "{mydata.NameForoshandeh}"
                .Item("Tarikh").Text = "{mydata.Tarikh}"
                .Item("Saat").Text = "{mydata.Saat}"
                .Item("ElatAdamDarkhast").Text = "{mydata.ElatAdamDarkhast}"

                .Item("Title").Text = "'" & "گزارش عدم درخواست فروشنده" & "'"
                .Item("Title2").Text = "'" & NameSherkat & "'"
                .Item("Title3").Text = "'" & NameMahalFaal & "'"
                .Item("KarbarGozaresh").Text = "'" & PersonelName & "'"
                .Item("TarikhGozaresh").Text = "'" & objTarikh.SetDateSlash(TarikhEmrooz) & "'"
                .Item("SaatGozaresh").Text = "'" & Format(TimeOfDay, "HH:mm:ss") & "'"
            End With
            rpt.Refresh()

            frm.Text = "گزارش عــدم درخواست"

            frm.WindowState = FormWindowState.Maximized
            With frm.CRV
                .ReportSource = rpt
                .DisplayGroupTree = False
                .ShowGroupTreeButton = False
                .Zoom(75)
                If Not objSec.HaveUserPermission(SN, UD_Dll.Security.ePermissions.Print) Then .ShowPrintButton = False
                'If Not objSec.HaveUserPermission(SN, UD_Dll.Security.ePermissions.Export) Then .ShowExportButton = False
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
    Private Sub cmbNameForoshandeh_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbNameForoshandeh.SelectedIndexChanged
        If flg = True Then
            IsValidField(True)
            SetReport()
            SetGridStyle()
        End If
    End Sub
End Class
