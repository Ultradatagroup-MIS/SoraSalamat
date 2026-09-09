Public Class frmFO_MoshtaryAnalysis
#Region "Variable AND Constant Declration"
    Const cntCodeSubSystem As Long = 100099
    Private SN As Integer
    Public dsForm As New DataSet
    Dim dvForm As DataView
    Dim txtCaption As String
    Dim Fset As Boolean = True
    Private WithEvents BS As New UD_Dll.PassString
    Const FormTableName = "qryFO_Moshtary"
    Public dvTitr As DataView
    Public dvSatr As DataView
    Public cmAdamTaeed, cmTitr, cmTaeed, cmElat As CurrencyManager
    Dim PK As Integer
    Dim strMantaghehS As String = ""
    Dim strMahalehS As String = ""
    Dim ErrPro As New ErrorProvider
#End Region
#Region "Form Event Code"
    Private Sub frmFO_MoshtaryAnalysis_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetParameter()
        SN = objSec.GetSecurityNumber(cntCodeSubSystem, UserName)
        'mskAzTarikh.Text = CodeDoreh & "0101"
        mskAzTarikh.Text = objTarikh.DecDay(TarikhEmrooz, 7)
        mskTaTarikh.Text = TarikhEmrooz
        LoadCombo()
        ClearForm()
    End Sub
    Private Sub SetParameter()

        Dim commands As String = Microsoft.VisualBasic.Command()
        If commands.Length = 0 Then

            UserName = "administrator"
            UserPassWord = "66998833"
            NameMahalFaal = "تهران"
            CodeMahalFaal = "1"
            PersonelCode = "0"
            PersonelName = "Administrator"
            CodeDoreh = "1392"
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
        Dim strSQL As String
        Dim cnSQL As New SqlConnection
        Dim cmSQL As New SqlCommand
        Dim daSQL As SqlDataAdapter
        Dim dr As DataRow

        Try

            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()

            strSQL = "Global.spGorohForosh_LoadCombo "

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            cmSQL.Parameters.AddWithValue("CodeMahal", CodeMahalFaal)

            daSQL = New SqlDataAdapter(cmSQL)
            daSQL.Fill(dsForm, "tbl_GorohForosh")
            dr = dsForm.Tables("tbl_GorohForosh").NewRow
            dr("sharhGorohForosh") = "همه"
            dr("ccGorohForosh") = 0
            dsForm.Tables("tbl_GorohForosh").Rows.Add(dr)
            cmbGForosh.DataSource = Nothing
            cmbGForosh.Items.Clear()
            cmbGForosh.DataSource = dsForm.Tables("tbl_GorohForosh").DefaultView
            cmbGForosh.DisplayMember = "sharhGorohForosh"
            cmbGForosh.ValueMember = "ccGorohForosh"

            ' -------------------------------------------------------------------

            strSQL = "Global.spElatMarjoee_LoadCombo "

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            daSQL = New SqlDataAdapter(cmSQL)
            daSQL.Fill(dsForm, "tblElat")

            ' -------------------------------------------------------------------

            strSQL = "Global.spMantagheh_LoadCheckList "

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            daSQL = New SqlDataAdapter(cmSQL)
            daSQL.Fill(dsForm, "tblMantagheh")
            chkMantagheh.DataSource = Nothing
            chkMantagheh.Items.Clear()
            chkMantagheh.DataSource = dsForm.Tables("tblMantagheh").DefaultView
            chkMantagheh.DisplayMember = "txtMantagheh"
            chkMantagheh.ValueMember = "sMantagheh"

            ' -------------------------------------------------------------------

            strSQL = "Global.spNoeMoshtary_LoadCombo "

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            daSQL = New SqlDataAdapter(cmSQL)
            daSQL.Fill(dsForm, "tblNoeMoshtary")
            dr = dsForm.Tables("tblNoeMoshtary").NewRow
            dr("Code") = 0
            dr("Sharh") = "همه"
            dsForm.Tables("tblNoeMoshtary").Rows.Add(dr)
            cmbNoeMoshtary.DataSource = Nothing
            cmbNoeMoshtary.Items.Clear()
            cmbNoeMoshtary.DataSource = dsForm.Tables("tblNoeMoshtary").DefaultView
            cmbNoeMoshtary.DisplayMember = "Sharh"
            cmbNoeMoshtary.ValueMember = "Code"
            cmbNoeMoshtary.SelectedValue = 0

            cmSQL = Nothing : daSQL = Nothing
            cnSQL.Close()

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> LoadCombo")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> LoadCombo")
        End Try
    End Sub
    Private Sub ClearForm()
        cmbGForosh.SelectedValue = 0
        If cmbGForosh.SelectedValue = 0 Then
            LoadComboForoshandeh()
        End If

        cmbForoshandeh.SelectedValue = 0
        cmbGForosh.SelectedValue = 0
    End Sub
    Private Sub LoadComboForoshandeh()
        Dim strSQL As String
        Dim cnSQL As New SqlConnection
        Dim cmSQL As New SqlCommand
        Dim daSQL As SqlDataAdapter
        Dim dr As DataRow

        Try

            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()

            strSQL = "Sales.spMoshtaryAnalysis_LoadComboForoshandeh "

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            cmSQL.Parameters.AddWithValue("CodeMahal", CodeMahalFaal)
            cmSQL.Parameters.AddWithValue("sVazeiat", UD_Dll.Enums.FO_VaziatForoshandeh.NoFaal)
            cmSQL.Parameters.AddWithValue("UserName", UserName)
            cmSQL.Parameters.AddWithValue("ccGorohForosh", cmbGForosh.SelectedValue)

            daSQL = New SqlDataAdapter(cmSQL)
            If dsForm.Tables.Contains("tblForoshandehS") Then
                dsForm.Tables.Remove("tblForoshandehS")
            End If
            daSQL.Fill(dsForm, "tblForoshandehS")
            dr = dsForm.Tables("tblForoshandehS").NewRow
            dr("NameForoshandeh") = "همه"
            dr("ccForoshandeh") = 0
            dsForm.Tables("tblForoshandehS").Rows.Add(dr)
            cmbForoshandeh.DataSource = Nothing
            cmbForoshandeh.Items.Clear()

            cmbForoshandeh.DataSource = dsForm.Tables("tblForoshandehS").DefaultView
            cmbForoshandeh.DisplayMember = "NameForoshandeh"
            cmbForoshandeh.ValueMember = "ccForoshandeh"
            cmbForoshandeh.SelectedValue = 0

            cmSQL = Nothing : daSQL = Nothing
            cnSQL.Close()

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> LoadComboForoshandeh")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> LoadComboForoshandeh")
        End Try
    End Sub
    Private Sub cmbGForosh_SelectionChangeCommitted(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbGForosh.SelectionChangeCommitted
        LoadComboForoshandeh()
    End Sub
#End Region
#Region "Global Form Code"
    Private Sub Search(ByVal Mantagheh As String, ByVal Mahaleh As String)
        Dim strSQL As String
        Dim cnSQL As New SqlConnection
        Dim cmSQL As New SqlCommand
        Dim daSQL As New SqlDataAdapter

        If dsForm.Tables.Contains("tbl_MoshtaryAnalysis") Then
            dsForm.Tables.Remove("tbl_MoshtaryAnalysis")
        End If

        Try

            strSQL = "Sales.spMoshtaryAnalysis_Search "

            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            cmSQL.Parameters.AddWithValue("CodeMahal", CodeMahalFaal)
            cmSQL.Parameters.AddWithValue("CodeDoreh", CodeDoreh)
            cmSQL.Parameters.AddWithValue("AzTarikh", mskAzTarikh.Text)
            cmSQL.Parameters.AddWithValue("TaTarikh", mskTaTarikh.Text)
            cmSQL.Parameters.AddWithValue("ccGorohForosh", cmbGForosh.SelectedValue)
            cmSQL.Parameters.AddWithValue("ccForoshandeh", cmbForoshandeh.SelectedValue)
            cmSQL.Parameters.AddWithValue("strMantagheh", "," & Mantagheh & ",")
            cmSQL.Parameters.AddWithValue("strMahaleh", "," & Mahaleh & ",")
            cmSQL.Parameters.AddWithValue("ShowMandehMoshtary", chkShowMandehMoshtary.Checked)

            daSQL = New SqlDataAdapter(cmSQL)
            daSQL.Fill(dsForm, "tbl_MoshtaryAnalysis")

            SetTitrGrid()

            For I As Integer = 0 To chkMantagheh.Items.Count - 1
                If chkMantagheh.GetItemChecked(I) Then
                    chkMantagheh.SetItemChecked(I, False)
                End If
            Next

            For I As Integer = 0 To chkMasir.Items.Count - 1
                If chkMasir.GetItemChecked(I) Then
                    chkMasir.SetItemChecked(I, False)
                End If
            Next

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> Search ")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> Search ")
        End Try
    End Sub
    Private Sub SetTitrGrid()
        With GridEXTitr
            .DataSource = Nothing
            .DataSource = dsForm.Tables("tbl_MoshtaryAnalysis").DefaultView
            .SetDataBinding(dsForm.Tables("tbl_MoshtaryAnalysis").DefaultView, "")
            .RetrieveStructure()
        End With

        For i As Integer = 0 To GridEXTitr.CurrentTable.Columns.Count - 1
            GridEXTitr.CurrentTable.Columns.Item(i).Visible = False
        Next

        GridEXTitr.CurrentTable.Columns.Item("Radif").Caption = "ردیف"
        GridEXTitr.CurrentTable.Columns.Item("Radif").Visible = True
        GridEXTitr.CurrentTable.Columns.Item("Radif").Width = 50
        GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
        GridEXTitr.CurrentTable.Columns.Item("Radif").Position = 0
        GridEXTitr.CurrentTable.Columns.Item("Radif").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

        GridEXTitr.CurrentTable.Columns.Item("CodeMoshtary").Caption = "کد مشتری"
        GridEXTitr.CurrentTable.Columns.Item("CodeMoshtary").Visible = True
        GridEXTitr.CurrentTable.Columns.Item("CodeMoshtary").Width = 80
        GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
        GridEXTitr.CurrentTable.Columns.Item("CodeMoshtary").Position = 1

        GridEXTitr.CurrentTable.Columns.Item("NameMoshtary").Caption = "نام مشتری"
        GridEXTitr.CurrentTable.Columns.Item("NameMoshtary").Visible = True
        GridEXTitr.CurrentTable.Columns.Item("NameMoshtary").Width = 240
        GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
        GridEXTitr.CurrentTable.Columns.Item("NameMoshtary").Position = 2
        GridEXTitr.CurrentTable.Columns.GridEX.TableHeaderFormatStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

        GridEXTitr.CurrentTable.Columns.Item("FaktorShomareh").Caption = "شماره فاکتور"
        GridEXTitr.CurrentTable.Columns.Item("FaktorShomareh").Visible = True
        GridEXTitr.CurrentTable.Columns.Item("FaktorShomareh").Width = 90
        GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
        GridEXTitr.CurrentTable.Columns.Item("FaktorShomareh").Position = 3
        GridEXTitr.CurrentTable.Columns.Item("FaktorShomareh").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

        GridEXTitr.CurrentTable.Columns.Item("FaktorTarikhSlash").Caption = "تاریخ فاکتور"
        GridEXTitr.CurrentTable.Columns.Item("FaktorTarikhSlash").Visible = True
        GridEXTitr.CurrentTable.Columns.Item("FaktorTarikhSlash").Width = 80
        GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
        GridEXTitr.CurrentTable.Columns.Item("FaktorTarikhSlash").Position = 4

        GridEXTitr.CurrentTable.Columns.Item("Modat").Caption = "مــدت"
        GridEXTitr.CurrentTable.Columns.Item("Modat").Visible = True
        GridEXTitr.CurrentTable.Columns.Item("Modat").Width = 60
        GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
        GridEXTitr.CurrentTable.Columns.Item("Modat").Position = 5
        GridEXTitr.CurrentTable.Columns.Item("Modat").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

        GridEXTitr.CurrentTable.Columns.Item("MablaghFaktor").Caption = "مبلـغ فاکتـور"
        GridEXTitr.CurrentTable.Columns.Item("MablaghFaktor").Visible = True
        GridEXTitr.CurrentTable.Columns.Item("MablaghFaktor").Width = 100
        GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
        GridEXTitr.CurrentTable.Columns.Item("MablaghFaktor").Position = 6
        GridEXTitr.CurrentTable.Columns.Item("MablaghFaktor").FormatString = "###,###"

        GridEXTitr.CurrentTable.Columns.Item("PardakhtiForFaktor").Caption = "پرداختی فاکتـور"
        GridEXTitr.CurrentTable.Columns.Item("PardakhtiForFaktor").Visible = True
        GridEXTitr.CurrentTable.Columns.Item("PardakhtiForFaktor").Width = 100
        GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
        GridEXTitr.CurrentTable.Columns.Item("PardakhtiForFaktor").Position = 7
        GridEXTitr.CurrentTable.Columns.Item("PardakhtiForFaktor").FormatString = "###,###"

        GridEXTitr.CurrentTable.Columns.Item("MandehFaktor").Caption = "مانده فاکتـور"
        GridEXTitr.CurrentTable.Columns.Item("MandehFaktor").Visible = True
        GridEXTitr.CurrentTable.Columns.Item("MandehFaktor").Width = 100
        GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
        GridEXTitr.CurrentTable.Columns.Item("MandehFaktor").Position = 8
        GridEXTitr.CurrentTable.Columns.Item("MandehFaktor").FormatString = "###,###"

        If chkShowMandehMoshtary.Checked Then
            GridEXTitr.CurrentTable.Columns.Item("MandehMoshtary").Caption = "مانده مشتـری"
            GridEXTitr.CurrentTable.Columns.Item("MandehMoshtary").Visible = True
            GridEXTitr.CurrentTable.Columns.Item("MandehMoshtary").Width = 120
            GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXTitr.CurrentTable.Columns.Item("MandehMoshtary").Position = 9
            GridEXTitr.CurrentTable.Columns.Item("MandehMoshtary").FormatString = "###,###"
        End If

        GridEXTitr.CurrentTable.Columns.Item("txtMantagheh").Caption = "منطقه"
        GridEXTitr.CurrentTable.Columns.Item("txtMantagheh").Visible = True
        GridEXTitr.CurrentTable.Columns.Item("txtMantagheh").Width = 130
        GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
        GridEXTitr.CurrentTable.Columns.Item("txtMantagheh").Position = 10

        GridEXTitr.CurrentTable.Columns.Item("txtMahaleh").Caption = "محله"
        GridEXTitr.CurrentTable.Columns.Item("txtMahaleh").Visible = True
        GridEXTitr.CurrentTable.Columns.Item("txtMahaleh").Width = 130
        GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
        GridEXTitr.CurrentTable.Columns.Item("txtMahaleh").Position = 11

        GridEXTitr.CurrentTable.Columns.Item("TarikhPeigiryNext").Caption = "تاریخ پیگیری بعدی"
        GridEXTitr.CurrentTable.Columns.Item("TarikhPeigiryNext").Visible = True
        GridEXTitr.CurrentTable.Columns.Item("TarikhPeigiryNext").Width = 120
        GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
        GridEXTitr.CurrentTable.Columns.Item("TarikhPeigiryNext").Position = 12

        GridEXTitr.CurrentTable.Columns.Item("ccFaktorTitr").Caption = "ccFaktorTitr"
        GridEXTitr.CurrentTable.Columns.Item("ccFaktorTitr").Visible = False
        GridEXTitr.CurrentTable.Columns.Item("ccFaktorTitr").Width = 0
        GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
        GridEXTitr.CurrentTable.Columns.Item("ccFaktorTitr").Position = 13

        GridEXTitr.CurrentTable.Columns.Item("ccMoshtary").Caption = "ccMoshtary"
        GridEXTitr.CurrentTable.Columns.Item("ccMoshtary").Visible = False
        GridEXTitr.CurrentTable.Columns.Item("ccMoshtary").Width = 0
        GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
        GridEXTitr.CurrentTable.Columns.Item("ccMoshtary").Position = 14

        'For i As Integer = 0 To GridEXTitr.RowCount - 1
        '    Dim rowcol As New Janus.Windows.GridEX.GridEXFormatStyle()
        '    Dim a As String = GridEXTitr.GetRow(i).Cells("NameMoshtary").Text

        '    If GridEXTitr.GetRow(i).Cells("txtVazeiatPishFaktorRozeMoshtary").Text = "فاکتور شده" Then
        '        rowcol.BackColor = Color.LightGreen
        '        GridEXTitr.GetRow(i).RowStyle = rowcol
        '    ElseIf GridEXTitr.GetRow(i).Cells("txtVazeiatPishFaktorRozeMoshtary").Text = "ويزيت نشده" Then
        '        rowcol.BackColor = Color.Yellow
        '        GridEXTitr.GetRow(i).RowStyle = rowcol
        '    ElseIf GridEXTitr.GetRow(i).Cells("txtVazeiatPishFaktorRozeMoshtary").Text = "بدون وضعيت" Then
        '        rowcol.BackColor = Color.White
        '        GridEXTitr.GetRow(i).RowStyle = rowcol
        '    End If
        'Next

        For i As Integer = 0 To GridEXTitr.RootTable.Columns.Count - 1
            If GridEXTitr.RootTable.Columns(i).Type.IsValueType Then
                GridEXTitr.RootTable.Columns(i).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                GridEXTitr.RootTable.Columns(i).FormatMode = Janus.Windows.GridEX.FormatMode.UseIFormattable
                GridEXTitr.RootTable.Columns(i).FormatString = "###,###"
                GridEXTitr.RootTable.Columns(i).TotalFormatMode = Janus.Windows.GridEX.FormatMode.UseIFormattable
                GridEXTitr.RootTable.Columns(i).TotalFormatString = "###,###"
            End If
        Next
    End Sub
    Private Sub SetPeigiryGrid()

        With GridEXPeygiry
            .DataSource = Nothing
            .DataSource = dsForm.Tables("tblPeigiryFaktor").DefaultView
            .SetDataBinding(dsForm.Tables("tblPeigiryFaktor").DefaultView, "")
            .RetrieveStructure()
        End With

        For i As Integer = 0 To GridEXPeygiry.CurrentTable.Columns.Count - 1
            GridEXPeygiry.CurrentTable.Columns.Item(i).Visible = False
        Next

        GridEXPeygiry.CurrentTable.Columns.Item("TarikhPeigiry").Caption = "تاریخ پیگیــری"
        GridEXPeygiry.CurrentTable.Columns.Item("TarikhPeigiry").Visible = True
        GridEXPeygiry.CurrentTable.Columns.Item("TarikhPeigiry").Width = 120
        GridEXPeygiry.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
        GridEXPeygiry.CurrentTable.Columns.Item("TarikhPeigiry").Position = 0
        GridEXPeygiry.CurrentTable.Columns.Item("TarikhPeigiry").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

        GridEXPeygiry.CurrentTable.Columns.Item("SaatPeigiry").Caption = "ساعـت پیگیــری"
        GridEXPeygiry.CurrentTable.Columns.Item("SaatPeigiry").Visible = True
        GridEXPeygiry.CurrentTable.Columns.Item("SaatPeigiry").Width = 120
        GridEXPeygiry.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
        GridEXPeygiry.CurrentTable.Columns.Item("SaatPeigiry").Position = 1

        GridEXPeygiry.CurrentTable.Columns.Item("TarikhPeigiryNext").Caption = "تاریخ پیگیــری بعـدی"
        GridEXPeygiry.CurrentTable.Columns.Item("TarikhPeigiryNext").Visible = True
        GridEXPeygiry.CurrentTable.Columns.Item("TarikhPeigiryNext").Width = 120
        GridEXPeygiry.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
        GridEXPeygiry.CurrentTable.Columns.Item("TarikhPeigiryNext").Position = 2
        GridEXPeygiry.CurrentTable.Columns.GridEX.TableHeaderFormatStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

        GridEXPeygiry.CurrentTable.Columns.Item("UserName").Caption = "نام کاربـــر"
        GridEXPeygiry.CurrentTable.Columns.Item("UserName").Visible = True
        GridEXPeygiry.CurrentTable.Columns.Item("UserName").Width = 150
        GridEXPeygiry.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
        GridEXPeygiry.CurrentTable.Columns.Item("UserName").Position = 3
        GridEXPeygiry.CurrentTable.Columns.Item("UserName").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

        GridEXPeygiry.CurrentTable.Columns.Item("Sharh").Caption = "شــــــرح"
        GridEXPeygiry.CurrentTable.Columns.Item("Sharh").Visible = True
        GridEXPeygiry.CurrentTable.Columns.Item("Sharh").Width = 180
        GridEXPeygiry.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
        GridEXPeygiry.CurrentTable.Columns.Item("Sharh").Position = 4
        GridEXPeygiry.CurrentTable.Columns.Item("Sharh").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

        GridEXPeygiry.CurrentTable.Columns.Item("Tozihat").Caption = "توضیحـــــات"
        GridEXPeygiry.CurrentTable.Columns.Item("Tozihat").Visible = True
        GridEXPeygiry.CurrentTable.Columns.Item("Tozihat").Width = 435
        GridEXPeygiry.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
        GridEXPeygiry.CurrentTable.Columns.Item("Tozihat").Position = 5
        GridEXPeygiry.CurrentTable.Columns.Item("Tozihat").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

        GridEXPeygiry.CurrentTable.Columns.Item("ccPeigiryFaktor").Caption = "ccPeigiryFaktor"
        GridEXPeygiry.CurrentTable.Columns.Item("ccPeigiryFaktor").Visible = False
        GridEXPeygiry.CurrentTable.Columns.Item("ccPeigiryFaktor").Width = 0
        GridEXPeygiry.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
        GridEXPeygiry.CurrentTable.Columns.Item("ccPeigiryFaktor").Position = 6
        GridEXPeygiry.CurrentTable.Columns.Item("ccPeigiryFaktor").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

        For i As Integer = 0 To GridEXPeygiry.RootTable.Columns.Count - 1
            If GridEXPeygiry.RootTable.Columns(i).Type.IsValueType Then
                GridEXPeygiry.RootTable.Columns(i).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                GridEXPeygiry.RootTable.Columns(i).FormatMode = Janus.Windows.GridEX.FormatMode.UseIFormattable
                GridEXPeygiry.RootTable.Columns(i).FormatString = "###,###"
                GridEXPeygiry.RootTable.Columns(i).TotalFormatMode = Janus.Windows.GridEX.FormatMode.UseIFormattable
                GridEXPeygiry.RootTable.Columns(i).TotalFormatString = "###,###"
            End If
        Next
    End Sub
    Private Sub RefreshGridPeigiry()
        Dim strSQL As String
        Dim cnSQL As New SqlConnection
        Dim cmSQL As New SqlCommand
        Dim daSQL As New SqlDataAdapter

        If dsForm.Tables.Contains("tblPeigiryFaktor") Then
            dsForm.Tables.Remove("tblPeigiryFaktor")
        End If

        Try
            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()

            strSQL = "Sales.spMoshtaryAnalysis_PeigiryFaktor_Search "

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            cmSQL.Parameters.AddWithValue("ccFaktorTitr", Val(GridEXTitr.CurrentRow.Cells("ccFaktorTitr").Text.Replace(",", "")))

            daSQL = New SqlDataAdapter(cmSQL)
            daSQL.Fill(dsForm, "tblPeigiryFaktor")

            cmSQL = Nothing : daSQL = Nothing
            cnSQL.Close()

            SetPeigiryGrid()

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "Error IN DataBase ---->" & "RefreshGridPeigiry")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, "Error IN ---->" & "RefreshGridPeigiry")
        End Try
    End Sub
    Private Sub DeleteRecord()
        Dim cnSQL As SqlConnection
        Dim cmSQL As SqlCommand
        Dim strSQL As String
        Dim intRowsAffected As Integer
        Try
            If IsNothing(GridEXPeygiry.CurrentRow) Then Exit Sub

            strSQL = "Sales.spMoshtaryAnalysis_ElatPeigiryFaktor_Delete "

            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            cmSQL.Parameters.AddWithValue("ccPeigiryFaktor", Val(GridEXPeygiry.CurrentRow.Cells("ccPeigiryFaktor").Text.Replace(",", "")))

            intRowsAffected = cmSQL.ExecuteNonQuery()
            If intRowsAffected < 1 Then
                MsgBox("عمليات حذف رکورد " & GridEXPeygiry.CurrentRow.Cells("ccPeigiryFaktor").Text.Replace(",", "") & " با موفقيت انجام نشد.رکورد پيدا نشد", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "حذف رکورد")
            End If
            ' Close and Clean up objects
            cnSQL.Close()
            cmSQL = Nothing : cnSQL = Nothing

            RefreshGridPeigiry()

        Catch sqlExc As SqlException
            Select Case sqlExc.Number
                Case 229
                    If Microsoft.VisualBasic.Left(sqlExc.Message, 1) = "ِD" Then
                        MsgBox("خطا در حذف رکورد,حذف انجام نشد.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "خطاي بانک")
                    End If
                Case 547
                    MsgBox("براي اين رکورد اطلاعات ديگري وجود دارد.ابتدا آنها را حذف کنيد.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "خطاي بانک")
                Case Else
                    MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "خطاي بانک")
            End Select
        Catch e As Exception
            MsgBox(e.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "خطا")
        End Try
    End Sub
    Private Function IsValid(ByVal CheckField As String) As Boolean
        IsValid = False

        If CheckField = "mskAzTarikh" Or CheckField = "All" Then
            If Me.mskAzTarikh.Text = "" Then
                ErrPro.SetError(Me.mskAzTarikh, "از تاریخ را وارد کنید.")
                MsgBox("از تاریخ را وارد کنید.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "جستجو")
                mskAzTarikh.Focus()
                Exit Function
            End If
            ErrPro.SetError(Me.mskAzTarikh, "")
        End If
        If mskAzTarikh.Text.Length <> 0 Then
            If Not objTarikh.IsShDate(mskAzTarikh.Text) Then
                ErrPro.SetError(Me.mskAzTarikh, "از تاریخ صحیح وارد نشده است.")
                Exit Function
            End If
        End If

        If CheckField = "mskTaTarikh" Or CheckField = "All" Then
            If Me.mskTaTarikh.Text = "" Then
                ErrPro.SetError(Me.mskTaTarikh, "تا تاریخ را وارد کنید .")
                MsgBox("تا تاریخ را وارد کنید .", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "جستجو")
                mskTaTarikh.Focus()
                Exit Function
            End If
            ErrPro.SetError(Me.mskTaTarikh, "")
        End If
        If mskTaTarikh.Text.Length <> 0 Then
            If Not objTarikh.IsShDate(mskTaTarikh.Text) Then
                ErrPro.SetError(Me.mskTaTarikh, "تا تاریخ صحیح وارد نشده است .")
                Exit Function
            End If
        End If

        Return True
    End Function
    Private Function SetMandeh(ByVal ccMoshtary As Integer)
        Dim cn As New SqlConnection
        Dim cm As New SqlCommand
        Dim strSQL As String = ""

        Dim Mandeh As Double = 0

        strSQL = "Sales.spPishFaktor_CalcMandehMoshtary "

        cn = New SqlConnection(ConnectionString)
        cn.Open()

        cm = New SqlCommand(strSQL, cn)
        cm.CommandType = CommandType.StoredProcedure
        cm.Parameters.Clear()

        cm.Parameters.AddWithValue("ccMoshtary", ccMoshtary)
        cm.Parameters.AddWithValue("TarikhEmrooz", TarikhEmrooz)
        cm.Parameters.AddWithValue("Mandeh", Mandeh)
        cm.Parameters("Mandeh").Direction = ParameterDirection.Output

        cm.ExecuteNonQuery()


        Mandeh = cm.Parameters("Mandeh").Value

        cm = Nothing
        cn.Close()

        Return Mandeh
    End Function
#End Region
#Region "From Buttons"
    Private Sub btnNamayeshMasir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNamayeshMasir.Click
        Dim strSQL As String
        Dim cnSQL As New SqlConnection
        Dim cmSQL As New SqlCommand
        Dim strMantagheh As String = ""
        Dim daSQL As SqlDataAdapter

        Try

            If dsForm.Tables.Contains("tblMasir") Then
                dsForm.Tables.Remove("tblMasir")
            End If

            If chkMantagheh.CheckedItems.Count = 0 Then
                If chkMantagheh.CheckedIndices.Count = 0 Then
                    For I As Integer = 0 To chkMantagheh.Items.Count - 1
                        chkMantagheh.SetItemChecked(I, True)
                    Next
                End If
            End If

            If chkMantagheh.CheckedItems.Count <> 0 Then
                For I As Integer = 0 To chkMantagheh.Items.Count - 1
                    If chkMantagheh.GetItemChecked(I) Then
                        chkMantagheh.SelectedIndex = I
                        strMantagheh &= chkMantagheh.SelectedValue.ToString & ","
                    End If
                Next
                strMantagheh = strMantagheh.Remove(strMantagheh.Length - 1, 1)
            End If

            If strMantagheh = "" Then
                strMantagheh = "0"
            End If

            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()

            strSQL = "Sales.spMoshtaryAnalysis_LoadChkListMasir "

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            cmSQL.Parameters.AddWithValue("strMantagheh", "," & strMantagheh & ",")
            daSQL = New SqlDataAdapter(cmSQL)
            daSQL.Fill(dsForm, "tblMasir")
            chkMasir.DataSource = Nothing
            chkMasir.Items.Clear()
            chkMasir.DataSource = dsForm.Tables("tblMasir").DefaultView
            chkMasir.DisplayMember = "NameMasir"
            chkMasir.ValueMember = "sMahaleh"

            strMantagheh = ""

            cmSQL = Nothing : daSQL = Nothing
            cnSQL.Close()

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> btnNamayeshMasir ")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> btnNamayeshMasir ")
        End Try
    End Sub
    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub
    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Dim strMantagheh As String = ""
        Dim strMahaleh As String = ""

        If IsValid("All") = False Then
            Exit Sub
        End If

        If chkMantagheh.CheckedItems.Count <> 0 Then
            For I As Integer = 0 To chkMantagheh.Items.Count - 1
                If chkMantagheh.GetItemChecked(I) Then
                    chkMantagheh.SelectedIndex = I
                    strMantagheh &= chkMantagheh.SelectedValue.ToString & ","
                End If
            Next
            strMantagheh = strMantagheh.Remove(strMantagheh.Length - 1, 1)
        End If

        If chkMasir.CheckedItems.Count = 0 Then
            If chkMasir.CheckedIndices.Count = 0 Then
                For I As Integer = 0 To chkMasir.Items.Count - 1
                    chkMasir.SetItemChecked(I, True)
                Next
            End If
        End If

        If chkMasir.CheckedItems.Count <> 0 Then
            For I As Integer = 0 To chkMasir.Items.Count - 1
                If chkMasir.GetItemChecked(I) Then
                    chkMasir.SelectedIndex = I
                    strMahaleh &= chkMasir.SelectedValue.ToString & ","
                End If
            Next
            strMahaleh = strMahaleh.Remove(strMahaleh.Length - 1, 1)
        End If

        strMantaghehS = strMantagheh
        strMahalehS = strMahaleh

        Search(strMantaghehS, strMahalehS)
    End Sub
    Private Sub btnVosolFaktor_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnVosolFaktor.Click
        Try
            Me.Hide()

            Dim ExePath As String = Application.StartupPath & "\DP_VosoolFaktor.exe"
            Shell(ExePath & " " & UserName & ";" & UserCode & ";" & UserPassWord & ";" & NameMahalFaal & ";" & CodeMahalFaal & ";" & PersonelCode & ";" & PersonelName & ";" & CodeDoreh & ";" & " وصــول فاکتـــور " & ";", AppWinStyle.NormalFocus, True)

            Me.Show()
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "Error IN DataBase ----> btnVosolFaktor_Click")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, "Error IN ----> btnVosolFaktor_Click")
        End Try
    End Sub
    Private Sub btnMoshtary_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMoshtary.Click
        Try
            Me.Hide()

            Dim dr As DataRowView
            If GridEXTitr.DataSource Is Nothing Or GridEXTitr.RowCount = 0 Then
                Exit Sub
            End If

            dr = GridEXTitr.GetRow(GridEXTitr.Row).DataRow

            Dim ExePath As String = Application.StartupPath & "\FO_Moshtary.exe"
            Shell(ExePath & " " & UserName & ";" & UserCode & ";" & UserPassWord & ";" & NameMahalFaal & ";" & CodeMahalFaal & ";" & PersonelCode & ";" & PersonelName & ";" & CodeDoreh & ";" & " " & ";" & dr("ccMoshtary"), AppWinStyle.NormalFocus, True)

            Me.Show()
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "Error IN DataBase ---->" & "btnMoshtary_Click")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, "Error IN ---->" & "btnMoshtary_Click")
        End Try
    End Sub
    Private Sub btnDaftarMoshtary_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDaftarMoshtary.Click
        Dim frm As New Forms_dll.frmFO_MoshtaryMoreInfo
        Dim dr As DataRowView
        If GridEXTitr.DataSource Is Nothing Or GridEXTitr.RowCount = 0 Then
            Exit Sub
        End If
        dr = GridEXTitr.GetRow(GridEXTitr.Row).DataRow

        frm.ccMoshtary = dr("ccMoshtary")
        frm.ShowDialog(Me)
        frm = Nothing
        Me.Show()
    End Sub
    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        If IsNothing(GridEXTitr.CurrentRow) Then Exit Sub

        drowFaktor = GridEXTitr.GetRow(GridEXTitr.Row).DataRow

        ccPeigiry = 0

        Dim frm As New frmFO_PeigiryFaktor
        frm.ShowDialog()

        GridEXTitr.CurrentRow.Cells("TarikhPeigiryNext").Text = objTools.ConvertNulls(objTools.DLookupOne("dbo.SetDateSlash(TarikhPeigiryNext)", "tblfo_PeigiryFaktor", "ccFaktorTitr = " & Val(GridEXTitr.CurrentRow.Cells("ccFaktorTitr").Text.Replace(",", "")), "TarikhPeigiryNext DESC"), "----")
        RefreshGridPeigiry()
    End Sub
    Private Sub btnEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEdit.Click
        If IsNothing(GridEXTitr.CurrentRow) Then Exit Sub
        If IsNothing(GridEXPeygiry.CurrentRow) Then Exit Sub

        drowFaktor = GridEXTitr.GetRow(GridEXTitr.Row).DataRow
        ccPeigiry = Val(GridEXPeygiry.CurrentRow.Cells("ccPeigiryFaktor").Text)

        Dim frm As New frmFO_PeigiryFaktor
        frm.ShowDialog()

        GridEXTitr.CurrentRow.Cells("TarikhPeigiryNext").Text = objTools.ConvertNulls(objTools.DLookupOne("dbo.SetDateSlash(TarikhPeigiryNext)", "tblfo_PeigiryFaktor", "ccFaktorTitr = " & Val(GridEXTitr.CurrentRow.Cells("ccFaktorTitr").Text.Replace(",", "")), "TarikhPeigiryNext DESC"), "----")
        RefreshGridPeigiry()
    End Sub
    Private Sub GridEXTitr_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GridEXTitr.Click
        RefreshGridPeigiry()
        tMandeh.Text = "مانده این مشتری = " & Format(SetMandeh(Val(GridEXTitr.CurrentRow.Cells("ccMoshtary").Text.Replace(",", ""))), "###,###") & " ريال"
    End Sub
    Private Sub btnReport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReport.Click
        If Not objSec.HaveUserPermission(SN, UD_Dll.Security.ePermissions.Preview) Then Exit Sub
        Dim frmRPT As New frmRPT_Peigiry

        Me.Hide()
        frmRPT.ShowDialog(Me)
        frmRPT = Nothing

        Me.Show()
    End Sub
    Private Sub btnDel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDel.Click
        If Not objSec.HaveUserPermission(SN, UD_Dll.Security.ePermissions.Delete) Then Exit Sub
        If IsNothing(GridEXTitr.CurrentRow) Then Exit Sub
        If MsgBox("آيا رکورد حذف شود؟", MsgBoxStyle.YesNo + MsgBoxStyle.Question + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.DefaultButton2, "حذف رکورد") = MsgBoxResult.Yes Then
            DeleteRecord()
        End If

        GridEXTitr.CurrentRow.Cells("TarikhPeigiryNext").Text = objTools.ConvertNulls(objTools.DLookupOne("dbo.SetDateSlash(TarikhPeigiryNext)", "tblfo_PeigiryFaktor", "ccFaktorTitr = " & Val(GridEXTitr.CurrentRow.Cells("ccFaktorTitr").Text.Replace(",", "")), "TarikhPeigiryNext DESC"), "----")
    End Sub
#End Region
#Region "Menu"
    Private Sub MenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        ' 784 Ud_Dll.frmFO_MoshtaryMoreInfo
        'If UserName.ToUpper <> "ADMINISTRATOR" Then
        '    If Not ObjCode.CheckPermission(784) Then Exit Sub
        'End If

        'Dim objVazeiat As New Forms_dll.frmFO_MoshtaryMoreInfo
        'objVazeiat.ccMoshtary = dvTitr(cmTitr.Position)("ccMoshtary")
        'objVazeiat.ShowDialog()
    End Sub
#End Region
End Class

' Stored Procedure'haye Estefadeh Shode Dar in Form :

'---* frmFO_MoshtaryAnalysis *---
'Global.spGorohForosh_LoadCombo
'Global.spElatMarjoee_LoadCombo
'Global.spMantagheh_LoadCheckList
'Global.spNoeMoshtary_LoadCombo
'Sales.spMoshtaryAnalysis_LoadComboForoshandeh
'Sales.spMoshtaryAnalysis_Search
'Sales.spMoshtaryAnalysis_PeigiryFaktor_Search
'Sales.spMoshtaryAnalysis_ElatPeigiryFaktor_Delete
'Sales.spMoshtaryAnalysis_LoadChkListMasir
'Sales.spPishFaktor_CalcMandehMoshtary

'---* frmFO_PeigiryFaktor *---
'Global.spElatPeigiryFaktor_LoadCombo
'Sales.spMoshtaryAnalysis_ElatPeigiryFaktor_Insert
'Sales.spMoshtaryAnalysis_ElatPeigiryFaktor_Update
'Sales.spMoshtaryAnalysis_ElatPeigiryFaktor_Search

'---* frmRPT_Peigiry *---
'Sales.spMoshtaryAnalysis_PeigiryFaktor_Print
'Report : rptFO_GozareshPeigiryFaktor.rpt