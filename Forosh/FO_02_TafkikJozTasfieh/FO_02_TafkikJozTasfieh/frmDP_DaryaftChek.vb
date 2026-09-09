Public Class frmDP_DaryaftChek

#Region "Variable AND Constant Declration"
    'Const FormTableName = "tblDP_Amalyat"
    'Const FormViewName = "qryDP_Amalyat"
    Const FormAddSize = 92
    Const FormOrgSize = 190
    Const GridAddSize = 188

    Dim ErrPro As New ErrorProvider
    Dim Mode As UD_Dll.Enums.GL_ModeForms = UD_Dll.Enums.GL_ModeForms.AddNewRecord
    Dim dsForm As New DataSet
    Dim cmForm As CurrencyManager
    Dim dvForm As DataView
    Dim txtCaption As String
    Private WithEvents BS As New UD_Dll.PassString
    Dim cntCodeSubSystem As Long = 958
    Private SN As Integer

    Dim Flag As Boolean = False

    Dim ccTafkikJozeTasfiehSatr_Chek As Integer = 0

    Dim flg As Boolean = False ' flag grid

    '=-------------------
    Public ccMoshtary As Long = 0
    Public ccFaktorTitr As Long = 0
    Public ccMamorPakhsh As Long = 0
    Public MablaghFaktor As Long = 0
    Public ccTafkikJozeTasfiehSatr As Long = 0
    Public MandehFaktor As Long = 0

#End Region

    Private Sub frmFO_DaryaftChek_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Mode = UD_Dll.Enums.GL_ModeForms.AddNewRecord
        LoadForm()
        CancelForm()
        objTools.ChangeKbd(UD_Dll.mdlUtility.KBD.Farsi)
    End Sub
    Private Sub LoadForm()
        Dim Strsql As String
        Dim cnSQL As New SqlConnection
        Dim cmSQL As New SqlCommand
        Dim daSQL As SqlDataAdapter
        Dim d As DataRow

        lblShomarehFaktor.Text = objTools.DLookup("FaktorShomareh", "tblFO_Faktor", "ccFaktorTitr = " & ccFaktorTitr)
        lblNameMoshtary.Text = objTools.DLookup("NameMoshtary", "tblFO_Moshtary", "ccMoshtary = " & ccMoshtary)
        lblAvarandehVajh.Text = objTools.DLookup("FName + ' ' + LName", "tblGL_MoshakhasatFardi", "CodeFard = " & ccMamorPakhsh)
        lblMablaghFaktor.Text = objTools.DLookup("JamKol", "tblFO_Faktor", "ccFaktorTitr = " & ccFaktorTitr)
        lblMandehFaktor.Text = MandehFaktor
        lblShomarehFaktor.TextAlign = ContentAlignment.MiddleCenter
        lblNameMoshtary.TextAlign = ContentAlignment.MiddleCenter
        lblAvarandehVajh.TextAlign = ContentAlignment.MiddleCenter
        lblMablaghFaktor.TextAlign = ContentAlignment.MiddleCenter
        mskTarikhDP.Text = TarikhEmrooz

        '-------------------------------------------
        Strsql = "Global.spShomarehHesab_LoadCombo "

        cnSQL = New SqlConnection(ConnectionString)
        cnSQL.Open()

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

        '-------------------------------------------

        Strsql = "Global.spBankSanad_LoadCombo "

        cmSQL = New SqlCommand(Strsql, cnSQL)
        cmSQL.CommandType = CommandType.StoredProcedure
        cmSQL.Parameters.Clear()

        daSQL = New SqlDataAdapter(cmSQL)
        daSQL.Fill(dsForm, "ComboBank")

        cmbBankSanad.DataSource = Nothing
        cmbBankSanad.Items.Clear()
        cmbBankSanad.DataSource = dsForm.Tables("ComboBank").DefaultView
        cmbBankSanad.DisplayMember = "Sharh"
        cmbBankSanad.ValueMember = "Code"
        d = dsForm.Tables("ComboBank").NewRow
        'd("Code") = ""
        d("Sharh") = ""
        dsForm.Tables("ComboBank").Rows.Add(d)
        cmbBankSanad.SelectedIndex = cmbBankSanad.Items.Count - 1
    End Sub
    Public Sub SetButton()
        Me.btnSave.Enabled = False
        Me.btnDelete.Enabled = False
        Me.btnCancel.Enabled = False
        Me.btnExit.Enabled = True
        Me.btnRefresh.Enabled = False
        If Mode = UD_Dll.Enums.GL_ModeForms.AddNewRecord Then
            Me.btnSave.Enabled = True
            Me.btnCancel.Enabled = True
            Me.btnRefresh.Enabled = True
        ElseIf Mode = UD_Dll.Enums.GL_ModeForms.UpdateRecord Then
            Me.btnSave.Enabled = True
            Me.btnCancel.Enabled = True
            Me.btnDelete.Enabled = True
        ElseIf Mode = UD_Dll.Enums.GL_ModeForms.Search Then
            Me.btnCancel.Enabled = True
            Me.btnDelete.Enabled = True
        End If
    End Sub
    Private Sub search()
        Try
            Dim StrSql As String

            StrSql = "Sales.spTafkikJozTasfiehSatrChek_Search "

            RefreshTitrdata(StrSql)
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->Search")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->Search")
        End Try
    End Sub
    Private Sub RefreshTitrdata(ByVal StrSql As String)
        Try
            Dim cn As SqlConnection
            Dim cm As SqlCommand
            Dim da As SqlDataAdapter

            If dsForm.Tables.Contains("tblTafkikJozTasfiehSatrChek") Then
                dsForm.Tables.Remove("tblTafkikJozTasfiehSatrChek")
            End If

            cn = New SqlConnection(ConnectionString)
            cn.Open()

            cm = New SqlCommand(StrSql, cn)
            cm.CommandType = CommandType.StoredProcedure
            cm.Parameters.Clear()

            cm.Parameters.AddWithValue("ccFaktor", ccFaktorTitr)

            da = New SqlDataAdapter(cm)
            da.Fill(dsForm, "tblTafkikJozTasfiehSatrChek")

            dvForm = New DataView(dsForm.Tables("tblTafkikJozTasfiehSatrChek"), "", "FaktorShomareh ASC", DataViewRowState.CurrentRows)
            dvForm.AllowNew = False
            dvForm.AllowDelete = False
            dvForm.AllowEdit = False

            cm.Connection.Close()
            cn.Close()
            cm = Nothing
            da = Nothing

            GridEXChek.DataSource = Nothing
            GridEXChek.DataSource = dvForm

            SetGridStyle()

            'If dvForm.Count = 0 Then
            '    CancelForm()
            'Else
            '    Mode = UD_Dll.Enums.GL_ModeForms.Search
            'End If

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->Search")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->Search")
        End Try
    End Sub
    Public Sub CancelForm()
        Mode = UD_Dll.Enums.GL_ModeForms.AddNewRecord
        ClearForm()
        DisableGrid()
        SetButton()
    End Sub
    Private Sub DisableGrid()
        GridEXChek.Visible = False
        Me.Height = FormOrgSize
        If GridEXChek.Height >= GridAddSize Then
            GridEXChek.Height = GridEXChek.Height - GridAddSize
        End If
        Me.CenterToScreen()
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If Mode = UD_Dll.Enums.GL_ModeForms.AddNewRecord Then
            AddNewRecord()
        ElseIf Mode = UD_Dll.Enums.GL_ModeForms.UpdateRecord Then
            UpdateRecord()
        End If

    End Sub
    Private Sub AddNewRecord()
        Try
            If Not IsValidForm("All") Then Exit Sub

            Dim cn As SqlConnection
            Dim cm As SqlCommand
            Dim strSQL As String = ""

            strSQL = "Sales.spTafkikJozTasfiehSatrChek_Insert "

            cn = New SqlConnection(ConnectionString)
            cn.Open()

            cm = New SqlCommand(strSQL, cn)
            cm.CommandType = CommandType.StoredProcedure
            cm.Parameters.Clear()

            cm.Parameters.AddWithValue("ccTafkikJozeTasfiehSatr", ccTafkikJozeTasfiehSatr)
            cm.Parameters.AddWithValue("ccFaktor", ccFaktorTitr)
            cm.Parameters.AddWithValue("ccMoshtary", ccMoshtary)
            cm.Parameters.AddWithValue("ccShomarehHesab", cmbShomarehHesab.SelectedValue)
            cm.Parameters.AddWithValue("ccMamorPakhsh", ccMamorPakhsh)
            cm.Parameters.AddWithValue("MablaghKolChek", txtMablaghKol.Text)
            cm.Parameters.AddWithValue("PardakhtyInFaktor", txtMablaghPFaktor.Text)
            cm.Parameters.AddWithValue("TarikhDP", mskTarikhDP.Text)
            cm.Parameters.AddWithValue("TarikhSanad", mskTarikhSanad.Text)
            cm.Parameters.AddWithValue("ShomarehSanad", txtShomarehSanad.Text)
            cm.Parameters.AddWithValue("sBank", cmbBankSanad.SelectedValue)
            cm.Parameters.AddWithValue("SharhShomarehHesab", cmbShomarehHesab.SelectedText)
            cm.Parameters.AddWithValue("ShomarehHesabSanad", IIf(Len(txtShomarehHesabSanad.Text) = 0, "", txtShomarehHesabSanad.Text.Trim))
            cm.Parameters.AddWithValue("ShobehSanad", IIf(Len(txtShobehSanad.Text) = 0, "", txtShobehSanad.Text.Trim))
            cm.Parameters.AddWithValue("CodeShobehSanad", IIf(Len(txtCodeShobehSanad.Text) = 0, "", txtCodeShobehSanad.Text.Trim))

            cm.ExecuteNonQuery()

            cm = Nothing
            cn.Close()

            MsgBox("عملیات ثبت با موفقیت انجام شد .", MsgBoxStyle.Information + MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, "")

            ClearForm()

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->Search")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->Search")
        End Try
    End Sub
    Private Sub UpdateRecord()
        Try
            If Not IsValidForm("All") Then Exit Sub

            Dim cn As SqlConnection
            Dim cm As SqlCommand
            Dim strSQL As String = ""

            strSQL = "Sales.spTafkikJozTasfiehSatrChek_Update "

            cn = New SqlConnection(ConnectionString)
            cn.Open()

            cm = New SqlCommand(strSQL, cn)
            cm.CommandType = CommandType.StoredProcedure
            cm.Parameters.Clear()

            cm.Parameters.AddWithValue("ccShomarehHesab", cmbShomarehHesab.SelectedValue)
            cm.Parameters.AddWithValue("MablaghKolChek", txtMablaghKol.Text)
            cm.Parameters.AddWithValue("PardakhtyInFaktor", txtMablaghPFaktor.Text)
            cm.Parameters.AddWithValue("TarikhDP", mskTarikhDP.Text)
            cm.Parameters.AddWithValue("TarikhSanad", mskTarikhSanad.Text)
            cm.Parameters.AddWithValue("ShomarehSanad", txtShomarehSanad.Text)
            cm.Parameters.AddWithValue("sBank", cmbBankSanad.SelectedValue)
            cm.Parameters.AddWithValue("ShomarehHesabSanad", IIf(Len(txtShomarehHesabSanad.Text) = 0, "", txtShomarehHesabSanad.Text.Trim))
            cm.Parameters.AddWithValue("ShobehSanad", IIf(Len(txtShobehSanad.Text) = 0, "", txtShobehSanad.Text.Trim))
            cm.Parameters.AddWithValue("CodeShobehSanad", IIf(Len(txtCodeShobehSanad.Text) = 0, "", txtCodeShobehSanad.Text.Trim))
            cm.Parameters.AddWithValue("ccTafkikJozeTasfiehSatr_Chek", ccTafkikJozeTasfiehSatr_Chek)

            cm.ExecuteNonQuery()

            cm.Connection.Close()
            cn.Close()
            cm = Nothing

            MsgBox("عملیات به روز رسانی با موفقیت انجام شد .", MsgBoxStyle.Information + MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, "")

            ClearForm()

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->Search")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->Search")
        End Try
    End Sub
    Private Sub SetGridStyle()
        Try
            'Titr
            '=========================================================================================='

            If dvForm.Count = 0 And Mode <> UD_Dll.Enums.GL_ModeForms.Search Then
                MsgBox("هیچ رکوردی یافت نشد .", MsgBoxStyle.Information + MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, "")
                Mode = UD_Dll.Enums.GL_ModeForms.AddNewRecord
                Exit Sub
            End If
            With GridEXChek
                .DataSource = Nothing
                .DataSource = dsForm.Tables("tblTafkikJozTasfiehSatrChek").DefaultView
                .SetDataBinding(dsForm.Tables("tblTafkikJozTasfiehSatrChek").DefaultView, "")
                .RetrieveStructure()
            End With

            For i As Integer = 0 To GridEXChek.CurrentTable.Columns.Count - 1
                GridEXChek.CurrentTable.Columns.Item(i).Visible = False
            Next

            GridEXChek.CurrentTable.Columns.Item("FaktorShomareh").Caption = "شماره فاکتور"
            GridEXChek.CurrentTable.Columns.Item("FaktorShomareh").Visible = True
            GridEXChek.CurrentTable.Columns.Item("FaktorShomareh").Width = 90
            GridEXChek.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXChek.CurrentTable.Columns.Item("FaktorShomareh").Position = 0
            GridEXChek.CurrentTable.Columns.Item("FaktorShomareh").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXChek.CurrentTable.Columns.Item("NameMoshtary").Caption = "نام مشتری"
            GridEXChek.CurrentTable.Columns.Item("NameMoshtary").Visible = True
            GridEXChek.CurrentTable.Columns.Item("NameMoshtary").Width = 220
            GridEXChek.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXChek.CurrentTable.Columns.Item("NameMoshtary").Position = 1
            GridEXChek.CurrentTable.Columns.Item("NameMoshtary").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXChek.CurrentTable.Columns.Item("AvarandehVajh").Caption = "آورنده وجــه"
            GridEXChek.CurrentTable.Columns.Item("AvarandehVajh").Visible = True
            GridEXChek.CurrentTable.Columns.Item("AvarandehVajh").Width = 100
            GridEXChek.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXChek.CurrentTable.Columns.Item("AvarandehVajh").Position = 2
            GridEXChek.CurrentTable.Columns.Item("AvarandehVajh").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXChek.CurrentTable.Columns.Item("SharhShomarehHesab").Caption = "نام صنــدوق"
            GridEXChek.CurrentTable.Columns.Item("SharhShomarehHesab").Visible = True
            GridEXChek.CurrentTable.Columns.Item("SharhShomarehHesab").Width = 150
            GridEXChek.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXChek.CurrentTable.Columns.Item("SharhShomarehHesab").Position = 3
            GridEXChek.CurrentTable.Columns.Item("SharhShomarehHesab").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXChek.CurrentTable.Columns.Item("ShomarehSanad").Caption = "شماره چــک"
            GridEXChek.CurrentTable.Columns.Item("ShomarehSanad").Visible = True
            GridEXChek.CurrentTable.Columns.Item("ShomarehSanad").Width = 90
            GridEXChek.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXChek.CurrentTable.Columns.Item("ShomarehSanad").Position = 4
            GridEXChek.CurrentTable.Columns.Item("ShomarehSanad").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXChek.CurrentTable.Columns.Item("TarikhDPSlash").Caption = "تاریخ دریافت"
            GridEXChek.CurrentTable.Columns.Item("TarikhDPSlash").Visible = True
            GridEXChek.CurrentTable.Columns.Item("TarikhDPSlash").Width = 110
            GridEXChek.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXChek.CurrentTable.Columns.Item("TarikhDPSlash").Position = 5
            GridEXChek.CurrentTable.Columns.Item("TarikhDPSlash").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXChek.CurrentTable.Columns.Item("TarikhSanadSlash").Caption = "تاریخ سررسیــد"
            GridEXChek.CurrentTable.Columns.Item("TarikhSanadSlash").Visible = True
            GridEXChek.CurrentTable.Columns.Item("TarikhSanadSlash").Width = 100
            GridEXChek.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXChek.CurrentTable.Columns.Item("TarikhSanadSlash").Position = 6
            GridEXChek.CurrentTable.Columns.Item("TarikhSanadSlash").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXChek.CurrentTable.Columns.Item("MablaghKolChek").Caption = "مبلغ چـک"
            GridEXChek.CurrentTable.Columns.Item("MablaghKolChek").Visible = True
            GridEXChek.CurrentTable.Columns.Item("MablaghKolChek").Width = 100
            GridEXChek.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXChek.CurrentTable.Columns.Item("MablaghKolChek").Position = 7
            GridEXChek.CurrentTable.Columns.Item("MablaghKolChek").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXChek.CurrentTable.Columns.Item("PardakhtyInFaktor").Caption = "پرداختی برای این فاکتور"
            GridEXChek.CurrentTable.Columns.Item("PardakhtyInFaktor").Visible = True
            GridEXChek.CurrentTable.Columns.Item("PardakhtyInFaktor").Width = 130
            GridEXChek.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXChek.CurrentTable.Columns.Item("PardakhtyInFaktor").Position = 8
            GridEXChek.CurrentTable.Columns.Item("PardakhtyInFaktor").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXChek.CurrentTable.Columns.Item("NameBank").Caption = "نام بانک"
            GridEXChek.CurrentTable.Columns.Item("NameBank").Visible = True
            GridEXChek.CurrentTable.Columns.Item("NameBank").Width = 100
            GridEXChek.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXChek.CurrentTable.Columns.Item("NameBank").Position = 9
            GridEXChek.CurrentTable.Columns.Item("NameBank").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXChek.CurrentTable.Columns.Item("ccTafkikJozeTasfiehSatr_Chek").Caption = "ccTafkikJozeTasfiehSatr_Chek"
            GridEXChek.CurrentTable.Columns.Item("ccTafkikJozeTasfiehSatr_Chek").Visible = False
            GridEXChek.CurrentTable.Columns.Item("ccTafkikJozeTasfiehSatr_Chek").Width = 0
            GridEXChek.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXChek.CurrentTable.Columns.Item("ccTafkikJozeTasfiehSatr_Chek").Position = 10
            GridEXChek.CurrentTable.Columns.Item("ccTafkikJozeTasfiehSatr_Chek").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXChek.CurrentTable.Columns.Item("ccShomarehHesab").Caption = "ccShomarehHesab"
            GridEXChek.CurrentTable.Columns.Item("ccShomarehHesab").Visible = False
            GridEXChek.CurrentTable.Columns.Item("ccShomarehHesab").Width = 0
            GridEXChek.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXChek.CurrentTable.Columns.Item("ccShomarehHesab").Position = 11
            GridEXChek.CurrentTable.Columns.Item("ccShomarehHesab").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXChek.CurrentTable.Columns.Item("TarikhDP").Caption = "TarikhDP"
            GridEXChek.CurrentTable.Columns.Item("TarikhDP").Visible = False
            GridEXChek.CurrentTable.Columns.Item("TarikhDP").Width = 0
            GridEXChek.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXChek.CurrentTable.Columns.Item("TarikhDP").Position = 12
            GridEXChek.CurrentTable.Columns.Item("TarikhDP").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXChek.CurrentTable.Columns.Item("TarikhSanad").Caption = "TarikhSanad"
            GridEXChek.CurrentTable.Columns.Item("TarikhSanad").Visible = False
            GridEXChek.CurrentTable.Columns.Item("TarikhSanad").Width = 0
            GridEXChek.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXChek.CurrentTable.Columns.Item("TarikhSanad").Position = 13
            GridEXChek.CurrentTable.Columns.Item("TarikhSanad").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXChek.CurrentTable.Columns.Item("sBank").Caption = "sBank"
            GridEXChek.CurrentTable.Columns.Item("sBank").Visible = False
            GridEXChek.CurrentTable.Columns.Item("sBank").Width = 0
            GridEXChek.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXChek.CurrentTable.Columns.Item("sBank").Position = 14
            GridEXChek.CurrentTable.Columns.Item("sBank").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXChek.CurrentTable.Columns.Item("ShomarehHesabSanad").Caption = "ShomarehHesabSanad"
            GridEXChek.CurrentTable.Columns.Item("ShomarehHesabSanad").Visible = False
            GridEXChek.CurrentTable.Columns.Item("ShomarehHesabSanad").Width = 0
            GridEXChek.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXChek.CurrentTable.Columns.Item("ShomarehHesabSanad").Position = 15
            GridEXChek.CurrentTable.Columns.Item("ShomarehHesabSanad").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXChek.CurrentTable.Columns.Item("ShobehSanad").Caption = "ShobehSanad"
            GridEXChek.CurrentTable.Columns.Item("ShobehSanad").Visible = False
            GridEXChek.CurrentTable.Columns.Item("ShobehSanad").Width = 0
            GridEXChek.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXChek.CurrentTable.Columns.Item("ShobehSanad").Position = 16
            GridEXChek.CurrentTable.Columns.Item("ShobehSanad").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXChek.CurrentTable.Columns.Item("CodeShobehSanad").Caption = "CodeShobehSanad"
            GridEXChek.CurrentTable.Columns.Item("CodeShobehSanad").Visible = False
            GridEXChek.CurrentTable.Columns.Item("CodeShobehSanad").Width = 0
            GridEXChek.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXChek.CurrentTable.Columns.Item("CodeShobehSanad").Position = 17
            GridEXChek.CurrentTable.Columns.Item("CodeShobehSanad").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            For i As Integer = 0 To GridEXChek.RootTable.Columns.Count - 1
                If GridEXChek.RootTable.Columns(i).Type.IsValueType Then
                    GridEXChek.RootTable.Columns(i).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                    GridEXChek.RootTable.Columns(i).FormatMode = Janus.Windows.GridEX.FormatMode.UseIFormattable
                    GridEXChek.RootTable.Columns(i).FormatString = "###,###.##"
                    GridEXChek.RootTable.Columns(i).TotalFormatMode = Janus.Windows.GridEX.FormatMode.UseIFormattable
                    GridEXChek.RootTable.Columns(i).TotalFormatString = "###,###.##"
                End If
            Next

            GridEXChek.Visible = True
            If GridEXChek.Height <= GridAddSize Then
                Me.Height = Me.Height + FormAddSize
                GridEXChek.Height = GridEXChek.Height + GridAddSize
            End If
            
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->SetGridStyle")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->SetGridStyle")
        End Try

    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        Mode = UD_Dll.Enums.GL_ModeForms.Search
        search()
        SetButton()
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        CancelForm()
    End Sub
    Private Sub ClearForm()
        cmbShomarehHesab.SelectedIndex = -1
        cmbBankSanad.SelectedIndex = -1
        cmbBankSanad.SelectedIndex = -1
        'mskTarikhDP.Text = ""
        mskTarikhSanad.Text = ""
        txtShomarehSanad.Text = ""
        txtMablaghKol.Text = ""
        txtMablaghPFaktor.Text = ""
        ccTafkikJozeTasfiehSatr_Chek = 0
        txtShomarehHesabSanad.Text = ""
        txtShobehSanad.Text = ""
        txtCodeShobehSanad.Text = ""
        ccTafkikJozeTasfiehSatr_Chek = 0
    End Sub
    Private Function IsValidForm(ByVal CheckField As String) As Boolean
        IsValidForm = False

        If CheckField = "cmbShomarehHesab" Or CheckField = "All" Then
            If (IsNothing(cmbShomarehHesab.SelectedValue)) Then
                ErrPro.SetError(Me.cmbShomarehHesab, "شماره حساب را وارد کنيد")
                MsgBox("شماره حساب را وارد کنيد", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
                cmbShomarehHesab.Focus()
                Exit Function
            End If
            ErrPro.SetError(Me.cmbShomarehHesab, "")
        End If

        If CheckField = "mskTarikhDP" Or CheckField = "All" Then
            If Len(mskTarikhDP.Text.Trim) <> 0 Then
                If Not objTarikh.IsShDate(mskTarikhDP.Text) Then
                    mskTarikhDP.Focus()
                    Exit Function
                End If
            Else
                ErrPro.SetError(Me.mskTarikhDP, "تاريخ " & " راوارد کنيد")
                MsgBox("تاريخ " & " راوارد کنيد", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
                mskTarikhDP.Focus()
                Exit Function
            End If
            If CodeDoreh <> objTarikh.ShamsiYear(mskTarikhDP.Text) Then
                ErrPro.SetError(Me.mskTarikhDP, "تاريخ با دوره يکی نيست.")
                MsgBox("تاريخ با دوره يکی نيست.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
                mskTarikhDP.Focus()
                Exit Function
            End If
            'If objTarikh.ShamsiYear(mskTarikhDP.Text) <> objTarikh.ShamsiYear(TarikhEmrooz) Then
            '    ErrPro.SetError(Me.mskTarikhDP, "تاريخ بايد در سال ورود اطلاعات باشد.")
            '    MsgBox("تاريخ بايد در سال ورود اطلاعات باشد.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
            '    mskTarikhDP.Focus()
            '    Exit Function
            'End If
            If mskTarikhDP.Text > TarikhEmrooz Then
                ErrPro.SetError(Me.mskTarikhDP, "تاريخ نمی تواند جلوتر از تاریخ روز باشد.")
                MsgBox("تاريخ نمی تواند جلوتر از تاریخ روز باشد.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
                mskTarikhDP.Focus()
                Exit Function
            End If
            ErrPro.SetError(Me.mskTarikhDP, "")
        End If
        If CheckField = "mskTarikhSanad" Or CheckField = "All" Then
            If Len(mskTarikhSanad.Text.Trim) <> 0 Then
                If Not objTarikh.IsShDate(mskTarikhSanad.Text) Then
                    mskTarikhSanad.Focus()
                    Exit Function
                End If
            Else
                ErrPro.SetError(Me.mskTarikhSanad, "تاريخ سر رسید " & " راوارد کنيد")
                MsgBox("تاريخ سر رسید  " & " راوارد کنيد", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
                mskTarikhSanad.Focus()
                Exit Function
            End If
            ErrPro.SetError(Me.mskTarikhSanad, "")
        End If

        '??
        'If CheckField = "mskTarikhSanad" Or CheckField = "All" Then
        '    If mskTarikhSanad.Text.Length <> 0 And mskTarikhSanad.Text < mskTarikhDP.Text Then
        '        ErrPro.SetError(mskTarikhSanad, "تاریخ سررسید نمیتواند پیش از تاریخ دریافت باشد.")
        '        MsgBox("تاریخ سررسید نمیتواند پیش از تاریخ دریافت باشد.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
        '        mskTarikhSanad.Focus()
        '        Exit Function
        '    End If
        '    ErrPro.SetError(Me.mskTarikhSanad, "")
        'End If

        If CheckField = "txtShomarehSanad" Or CheckField = "All" Then
            If txtShomarehSanad.Text.Length = 0 Then
                ErrPro.SetError(txtShomarehSanad, "شماره را وارد کنيد")
                MsgBox("شماره را وارد کنيد", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
                txtShomarehSanad.Focus()
                Exit Function
            End If
            ErrPro.SetError(Me.txtShomarehSanad, "")
        End If

        If objTools.ConvertNulls(objTools.DCount("ShomarehSanad", "tblDP_Amalyat", "ShomarehSanad = '" & txtShomarehSanad.Text & "'"), 0) >= 1 Then
            If Mode = UD_Dll.Enums.GL_ModeForms.AddNewRecord Then
                If MsgBox("شماره چک تکراری وارد شده است، رکورد ذخیره شود ؟", MsgBoxStyle.YesNo + MsgBoxStyle.Question + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.DefaultButton2, "تایید") = MsgBoxResult.No Then
                    txtShomarehSanad.Focus()
                    ErrPro.SetError(Me.txtShomarehSanad, "شماره چک تکراری وارد شده است، رکورد ذخیره شود ؟")
                    Exit Function
                End If
            End If
        Else
            ErrPro.SetError(Me.txtShomarehSanad, "")
        End If

        If CheckField = "txtMablaghKol" Or CheckField = "All" Then
            If txtMablaghKol.Text.Length = 0 Then
                ErrPro.SetError(txtMablaghKol, "مبلغ کل را وارد کنيد")
                MsgBox("مبلغ را وارد کنيد", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
                txtMablaghKol.Focus()
                Exit Function
            End If
            ErrPro.SetError(Me.txtMablaghKol, "")
        End If

        If CheckField = "txtMablaghPFaktor" Or CheckField = "All" Then
            If txtMablaghPFaktor.Text.Length = 0 Then
                ErrPro.SetError(txtMablaghPFaktor, "مبلغ پرداختی برای این فاکتور را وارد کنید.")
                MsgBox("مبلغ پرداختی برای این فاکتور را وارد کنید.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
                txtMablaghPFaktor.Focus()
                Exit Function
            End If
            ErrPro.SetError(Me.txtMablaghPFaktor, "")
        End If

        If Val(txtMablaghKol.Text) < Val(txtMablaghPFaktor.Text) Then
            ErrPro.SetError(txtMablaghPFaktor, "مبلغ پرداختی برای این فاکتور نمی تواند از کل وجه دریافتی بیشتر باشد.")
            MsgBox("مبلغ پرداختی برای این فاکتور نمی تواند از کل وجه دریافتی بیشتر باشد.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
            txtMablaghPFaktor.Focus()
            Exit Function
        End If
        ErrPro.SetError(Me.txtMablaghPFaktor, "")

        'Dim MandehFak As Double = (Val(objTools.DLookup("JamKol", "tblFO_Faktor", "ccFaktorTitr=" & ccFaktorTitr)) - Val(objTools.ConvertNulls(objTools.DSum("Mablagh", "qryDP_AmalyatFaktor", "ccFaktorTitr=" & ccFaktorTitr & " AND Ebtal=0"), 0)))
        If Val(txtMablaghPFaktor.Text) > MandehFaktor Then
            ErrPro.SetError(txtMablaghPFaktor, "مبلغ پرداختی برای این فاکتور نباید از " & MandehFaktor & " بیشتر باشد.")
            MsgBox("مبلغ پرداختی برای این فاکتور نباید از " & MandehFaktor & " بیشتر باشد.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
            txtMablaghPFaktor.Focus()
            Exit Function
        End If
        ErrPro.SetError(Me.txtMablaghPFaktor, "")

        Dim RassGiriBrand As Boolean = objTools.DLookup("RassGiriBrand", "tblGL_SysConfig", "CodeMahal = " & CodeMahalFaal)
        Dim CheckNoePardakht As Boolean = objTools.DLookup("ChekNoePardakht", "tblGL_SysConfig", "CodeMahal = " & CodeMahalFaal)
        Dim ModatPardakht As Integer
        If CheckNoePardakht = True Then
            If RassGiriBrand = True Then
                ModatPardakht = objTools.DLookup("SenCheckBrand", "QryFo_Faktor", "ccFaktorTitr = " & ccFaktorTitr)
            Else
                ModatPardakht = objTools.DLookup("ModatCheck", "TblFo_Faktor", "ccFaktorTitr = " & ccFaktorTitr)
            End If

            Dim ccTafkik As Integer = objTools.ConvertNulls((objTools.DLookup("ccTafkik", "TblFo_Faktor", "ccFaktorTitr = " & ccFaktorTitr)), 0)
            Dim DiffGheymat As Integer
            If ccTafkik <> 0 Then
                Dim TarikhErsal As Integer = objTools.DLookup("TarikhErsal", "tblFO_TafkikJoze", "ccTafkik = " & ccTafkik)
                DiffGheymat = objTarikh.DiffDaySh(TarikhErsal, mskTarikhSanad.Text)
            Else
                Dim TarikhFaktor As Integer = objTools.ConvertNulls((objTools.DLookup("FaktorTarikh", "TblFo_Faktor", "ccFaktorTitr = " & ccFaktorTitr)), 0)
                DiffGheymat = objTarikh.DiffDaySh(TarikhFaktor, mskTarikhSanad.Text)
            End If
            If ModatPardakht < DiffGheymat Then
                ErrPro.SetError(txtMablaghPFaktor, "شما مجاز به ثبت چک با این تاریخ نمی باشید. چک شما حداکثر می تواند " & ModatPardakht & " روزه باشد.")
                MsgBox("شما مجاز به ثبت چک با این تاریخ نمی باشید. چک شما حداکثر می تواند " & ModatPardakht & " روزه باشد.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
            End If

            Exit Function

            ErrPro.SetError(Me.mskTarikhSanad, "")
        End If
        If cmbBankSanad.SelectedIndex = cmbBankSanad.Items.Count - 1 Then
            ErrPro.SetError(cmbBankSanad, "بانک مورد نظر را انتخاب کنید.")
            MsgBox("بانک مورد نظر را انتخاب کنید.")
            cmbBankSanad.Focus()
            Exit Function
        End If
        ErrPro.SetError(Me.cmbBankSanad, "")

        Return True
    End Function

    Private Sub GridEXChek_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GridEXChek.DoubleClick
        Mode = UD_Dll.Enums.GL_ModeForms.UpdateRecord
        DisableGrid()
        SetButton()
        SetFormData()
    End Sub
    Private Sub SetFormData()
        Dim drvTemp As DataRowView
        drvTemp = dvForm(cmForm.Position)

        cmbShomarehHesab.SelectedValue = Val(GridEXChek.CurrentRow.Cells("ccShomarehHesab").Text.Replace(",", ""))
        mskTarikhDP.Text = GridEXChek.CurrentRow.Cells("TarikhDP").Text
        mskTarikhSanad.Text = GridEXChek.CurrentRow.Cells("TarikhSanad").Text
        cmbBankSanad.SelectedValue = Val(GridEXChek.CurrentRow.Cells("sBank").Text.Replace(",", ""))
        txtShomarehSanad.Text = GridEXChek.CurrentRow.Cells("ShomarehSanad").Text
        txtMablaghKol.Text = Val(GridEXChek.CurrentRow.Cells("MablaghKolChek").Text.Replace(",", ""))
        txtMablaghPFaktor.Text = Val(GridEXChek.CurrentRow.Cells("PardakhtyInFaktor").Text.Replace(",", ""))
        txtShomarehHesabSanad.Text = GridEXChek.CurrentRow.Cells("ShomarehHesabSanad").Text
        txtShobehSanad.Text = GridEXChek.CurrentRow.Cells("ShobehSanad").Text
        txtCodeShobehSanad.Text = GridEXChek.CurrentRow.Cells("CodeShobehSanad").Text
        ccTafkikJozeTasfiehSatr_Chek = Val(GridEXChek.CurrentRow.Cells("ccTafkikJozeTasfiehSatr_Chek").Text.Replace(",", ""))

    End Sub
    Private Sub BoundCurrencyManagerTitr()
        Try
            cmForm = CType(BindingContext(GridEXChek.DataSource), CurrencyManager)
            AddHandler cmForm.ItemChanged, AddressOf cmForm_ItemChanged
            AddHandler cmForm.PositionChanged, AddressOf cmForm_PositionChanged
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->BoundCurrencyManagerTitr")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->BoundCurrencyManagerTitr")
        End Try

    End Sub
    Private Sub cmForm_ItemChanged(ByVal sender As Object, ByVal e As ItemChangedEventArgs)
        Try

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->cmTitr_ItemChanged")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->cmTitr_ItemChanged")
        End Try

    End Sub
    Private Sub cmForm_PositionChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->cmTitr_PositionChanged")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->cmTitr_PositionChanged")
        End Try

    End Sub

    Private Sub GridEXChek_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GridEXChek.Click
        BoundCurrencyManagerTitr()
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        If MsgBox("عملیات حـــذف انجام شود ؟", MsgBoxStyle.Question + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.YesNo, "") = MsgBoxResult.Yes Then
            DeleteRecord()
            search()
        End If
    End Sub
    Private Sub DeleteRecord()
        Dim cn As SqlConnection
        Dim cm As SqlCommand
        Dim strSQL As String = "Sales.spTafkikJozTasfiehSatrChek_Delete "

        Dim ccTJTS_C As Integer = 0
        If Mode = UD_Dll.Enums.GL_ModeForms.Search Then
            ccTJTS_C = Val(GridEXChek.CurrentRow.Cells("ccTafkikJozeTasfiehSatr_Chek").Text.Replace(",", ""))
        ElseIf Mode = UD_Dll.Enums.GL_ModeForms.UpdateRecord Then
            ccTJTS_C = ccTafkikJozeTasfiehSatr_Chek
        End If

        cn = New SqlConnection(ConnectionString)
        cn.Open()

        cm = New SqlCommand(strSQL, cn)
        cm.CommandType = CommandType.StoredProcedure
        cm.Parameters.Clear()

        cm.Parameters.AddWithValue("ccTafkikJozeTasfiehSatr_Chek", ccTJTS_C)

        cm.ExecuteNonQuery()

        cm.Connection.Close()
        cn.Close()
        cm = Nothing

        MsgBox("عملیات حـــذف با موفقیت انجام شد .", MsgBoxStyle.Information + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.OkOnly, "")

    End Sub

    Private Sub frmDP_DaryaftChek_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        frmFO_TafkikJozTasfieh.mskCheckSatr.Text = objTools.ConvertNulls(objTools.DSum("PardakhtyInFaktor", "Sales.TafkikJozeTasfiehSatrChek", "ccFaktor = " & ccFaktorTitr), 0)
    End Sub
    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        frmFO_TafkikJozTasfieh.mskCheckSatr.Text = objTools.ConvertNulls(objTools.DSum("PardakhtyInFaktor", "Sales.TafkikJozeTasfiehSatrChek", "ccFaktor = " & ccFaktorTitr), 0)
        Me.Close()
    End Sub
End Class