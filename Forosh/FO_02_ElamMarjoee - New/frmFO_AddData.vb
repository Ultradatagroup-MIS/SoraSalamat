


Imports System.Data
Imports System.Data.SqlClient
Imports System.Reflection.Emit
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.Tab

Public Class frmFO_AddData

#Region "Variable AND Constant Declration"
    'Const cntCodeSubSystem As Long = 625

    Dim cmTitr As CurrencyManager
    Dim ErrPro As New ErrorProvider
    Dim dsForm As New DataSet
    Dim dtP As New DataTable
    Dim dr As DataRow
    Dim dvForm As DataTable
    Private SN As Integer
    Dim txtCaption As String
    Private WithEvents BS As New UD_Dll.PassString

    Private LastRowIndex As Integer = -1
    Dim ccElamMarjoee As Integer = 0
    Dim ccMoshtaryAddress As Integer = 0
    Dim CodeFard As Integer = 0
    Dim AllowChangeFeePishFaktor As Boolean
    Dim MeghdarAdadi As Integer
    Dim flg As Boolean = False
    Dim ccFaktorTitr As Integer

    Dim ccMoshtary As Integer
    Dim AllowMarjoeeWithoutFaktor As Boolean = objTools.ConvertNulls(objTools.DLookup("AllowMarjoeeWithoutFaktor", "tblGL_SysConfig", "CodeMahal = " & CodeMahalFaal), False)
    Dim AllowChangeFeeMarjoee As Boolean = objTools.ConvertNulls(objTools.DLookup("AllowChangeFeeMarjoee", "dbo.tblGL_SysConfig", "CodeMahal = " & CodeMahalFaal), False)
    Dim AllowPishFaktorTakhfifDasty As Boolean = objTools.ConvertNulls(objTools.DLookup("AllowPishFaktorTakhfifDasty", "tblGL_SysConfig", ""), False)

#End Region


    Public Class ElamMarjoeeTitrInfo

#Region "Fields"
        Private _ccElamMarjoee As Integer
        Private _TarikhElamMarjoee As String = ""
        Private _ccMoshtary As Integer
        Private _ShomarehElamMarjoee As Integer
        Private _CodeMoshtary As String = ""
        Private _NameMoshtary As String = ""
        Private _CodeFard_MamorPakhsh As Integer
        Private _ccForoshandeh As Integer
        Private _NoeMarjoeeFT As Integer
        Private _ccFaktorTitr As Integer
        Private _Tozihat As String = ""
        'Private _ccAnbar As Integer
        'Private _PishFaktorShomareh As Integer = 0
        Private _IsFaktorMarjoee As Boolean
        Private _dtSearchTitr As DataTable
#End Region

#Region "Properties"
        Public ReadOnly Property ccElamMarjoee() As Integer
            Get
                Return _ccElamMarjoee
            End Get
        End Property

        Public ReadOnly Property TarikhElamMarjoee() As String
            Get
                Return _TarikhElamMarjoee
            End Get
        End Property

        Public ReadOnly Property ccMoshtary() As Integer
            Get
                Return _ccMoshtary
            End Get
        End Property
        Public ReadOnly Property ShomarehElamMarjoee() As Integer
            Get
                Return _ShomarehElamMarjoee
            End Get
        End Property
        Public ReadOnly Property CodeMoshtary() As String
            Get
                Return _CodeMoshtary
            End Get
        End Property

        Public ReadOnly Property NameMoshtary() As String
            Get
                Return _NameMoshtary
            End Get
        End Property

        Public ReadOnly Property CodeFard_MamorPakhsh() As Integer
            Get
                Return _CodeFard_MamorPakhsh
            End Get
        End Property

        Public ReadOnly Property ccForoshandeh() As Integer
            Get
                Return _ccForoshandeh
            End Get
        End Property

        Public ReadOnly Property NoeMarjoeeFT() As Integer
            Get
                Return _NoeMarjoeeFT
            End Get
        End Property

        Public ReadOnly Property ccFaktorTitr() As Integer
            Get
                Return _ccFaktorTitr
            End Get
        End Property

        Public ReadOnly Property Tozihat() As String
            Get
                Return _Tozihat
            End Get
        End Property



        Public ReadOnly Property IsFaktorMarjoee() As Boolean
            Get
                Return _IsFaktorMarjoee
            End Get
        End Property
        Public ReadOnly Property dt_SearchTitr() As DataTable
            Get
                Return _dtSearchTitr
            End Get
        End Property

#End Region

        ''' <summary>
        ''' اطلاعات تیتر را از دیتابیس می‌خواند. اگر رکوردی نبود False برمی‌گرداند.
        ''' </summary>
        Public Function Load(ByVal id As Integer, ByVal connectionString As String) As Boolean
            _dtSearchTitr = New DataTable()

            Using cn As New SqlConnection(connectionString)
                Using cm As SqlCommand = cn.CreateCommand()
                    cm.CommandType = CommandType.StoredProcedure
                    cm.CommandText = "[Sales].[spMarjoeeVorodKoli_SearchTitr]"
                    cm.CommandTimeout = 999999
                    cm.Parameters.AddWithValue("@ccElamMarjoee", id)

                    Dim da As New SqlDataAdapter(cm)
                    da.Fill(_dtSearchTitr)
                End Using
            End Using

            If _dtSearchTitr.Rows.Count = 0 Then Return False

            Dim r As DataRow = _dtSearchTitr.Rows(0)
            _ccElamMarjoee = id
            _TarikhElamMarjoee = GetStr(r, "TarikhElamMarjoee")
            _ccMoshtary = GetInt(r, "ccMoshtary")
            _ccFaktorTitr = GetInt(r, "ccFaktorTitr")
            _CodeMoshtary = GetStr(r, "CodeMoshtary")
            _NameMoshtary = GetStr(r, "NameMoshtary")
            _CodeFard_MamorPakhsh = GetInt(r, "CodeFard_MamorPakhsh")
            _ccForoshandeh = GetInt(r, "ccForoshandeh")
            '_sNoePardakht = GetInt(r, "sNoePardakht")
            '_ModatCheck = GetInt(r, "ModatCheck")
            _Tozihat = GetStr(r, "Tozihat")
            '_ccAnbar = GetInt(r, "ccAnbar")
            _ShomarehElamMarjoee = GetInt(r, "ShomarehElamMarjoee")
            _IsFaktorMarjoee = GetBool(r, "IsFaktorMarjoee")
            Return True
        End Function

#Region "Helpers"
        ' اگر ستون در خروجی SP نبود، خطا نمی‌دهد و مقدار پیش‌فرض برمی‌گرداند
        Private Shared Function GetInt(ByVal r As DataRow, ByVal col As String) As Integer
            If Not r.Table.Columns.Contains(col) Then Return 0
            Dim v As Object = r(col)
            If v Is Nothing OrElse IsDBNull(v) Then Return 0
            Return Convert.ToInt32(v)
        End Function

        Private Shared Function GetStr(ByVal r As DataRow, ByVal col As String) As String
            If Not r.Table.Columns.Contains(col) Then Return ""
            Dim v As Object = r(col)
            If v Is Nothing OrElse IsDBNull(v) Then Return ""
            Return v.ToString()
        End Function
        Private Shared Function GetBool(ByVal r As DataRow, ByVal col As String) As Boolean
            If Not r.Table.Columns.Contains(col) Then Return False
            Dim v As Object = r(col)
            If v Is Nothing OrElse IsDBNull(v) Then Return False
            Return Convert.ToBoolean(v)
        End Function
#End Region

    End Class
    Private Sub frmFO_AddData_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        mskTarikh.Text = TarikhEmrooz
        txtSearchNumber.Focus()
        'chkMalyat.Checked = True
        'chkMalyat.Enabled = False


        flg = False
        LoadCombo()
        ClearForm()
        txtSearchNumber.Focus()
        Mode = UD_Dll.Enums.GL_ModeForms.AddNewRecord
        flg = True
        Dim AllowChangeFeeMarjoee As Boolean = objTools.ConvertNulls(objTools.DLookup("AllowChangeFeeMarjoee", "dbo.tblGL_SysConfig", "CodeMahal = " & CodeMahalFaal), False)
        Dim AllowPishFaktorTakhfifDasty As Boolean = objTools.ConvertNulls(objTools.DLookup("AllowPishFaktorTakhfifDasty", "tblGL_SysConfig", ""), False)
        With GridEXSatr
            .TabKeyBehavior = Janus.Windows.GridEX.TabKeyBehavior.ColumnNavigation
            .UpdateMode = Janus.Windows.GridEX.UpdateMode.RowUpdate
            .AllowEdit = Janus.Windows.GridEX.InheritableBoolean.True
            .AllowAddNew = Janus.Windows.GridEX.InheritableBoolean.True
        End With
        With GridEXSatr
            .AllowEdit = Janus.Windows.GridEX.InheritableBoolean.True
            .AllowAddNew = Janus.Windows.GridEX.InheritableBoolean.True
            .UpdateMode = Janus.Windows.GridEX.UpdateMode.RowUpdate
            .TabKeyBehavior = Janus.Windows.GridEX.TabKeyBehavior.ControlNavigation
            .Enabled = False          ' تا قبل از ذخیره‌ی هدر غیرفعال
        End With
    End Sub
    Private Sub ClearForm()
        Try

            mskTarikh.Enabled = False

            ccElamMarjoee = 0

            ccMoshtary = 0
            cmbBazaryab.SelectedIndex = -1
            cmbBazaryab.SelectedIndex = -1
            txtCodeMoshtary.Text = ""
            txtCodeMoshtary.Tag = ""
            lblNameMoshtary.Text = ""
            cmbMamorPakhsh.SelectedIndex = -1
            cmbMamorPakhsh.SelectedIndex = -1

            TxtSharh.Text = ""
            txtCodeMoshtary.Tag = 0
            txtCodeMoshtary.Text = ""
            lblNameMoshtary.Text = ""


            ErrPro.Dispose()

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->ClearForm")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->ClearForm")
        End Try
    End Sub
    Private Sub LoadCombo()
        Try
            Dim Strsql As String
            Dim daSQL As SqlDataAdapter
            Dim cnSQL As New SqlConnection
            Dim cmSQL As New SqlCommand
            Dim dr As DataRow


            ' --------------------------------------- Search Combo -----------------

            '' Load Search Combo Vaziat
            'Strsql = "Select * From tblGL_ShenasehOmomi Where Code = 0 or CodeAsli=48 AND CodeFarei <> 0 "
            'daSQL = New SqlDataAdapter(Strsql, ConnectionString)
            'daSQL.Fill(dsForm, "tblVazeiat")
            'cmbsVaziat.DataSource = Nothing
            'cmbsVaziat.Items.Clear()
            'cmbsVaziat.DataSource = dsForm.Tables("tblVazeiat").DefaultView
            'cmbsVaziat.DisplayMember = "Sharh"
            'cmbsVaziat.ValueMember = "Code"
            'For Each dr In dsForm.Tables("tblVazeiat").Rows
            '    If dr("Code") = 0 Then
            '        dr("Sharh") = "همه"
            '    End If
            'Next
            'cmbsVaziat.SelectedValue = UD_Dll.Enums.FO_VaziatElamMarjoee.BedoneAmalyat






            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()


            Strsql = "Global.spMamorPakhsh_LoadCombo "

            cmSQL = New SqlCommand(Strsql, cnSQL)
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



            '' Load Combo sElatMarjoee
            'Strsql = "Select * From tblGL_ShenasehOmomi Where CodeAsli= 63 And CodeFarei<>0 order by Sharh"
            'daSQL = New SqlDataAdapter(Strsql, ConnectionString)
            'daSQL.Fill(dsForm, "tblElatMarjoee")
            'cmbsElatMarjoee.DataSource = Nothing
            'cmbsElatMarjoee.Items.Clear()
            'cmbsElatMarjoee.DataSource = dsForm.Tables("tblElatMarjoee").DefaultView
            'cmbsElatMarjoee.DisplayMember = "Sharh"
            'cmbsElatMarjoee.ValueMember = "Code"

            ' Load Combo sVahed
            'Strsql = "Select * From tblGL_ShenasehOmomi Where CodeAsli= 50 And CodeFarei<>0 order by Sharh"
            'daSQL = New SqlDataAdapter(Strsql, ConnectionString)
            'daSQL.Fill(dsForm, "tbl_VahedKala")



            'cmbNoe.Items.Add("همه")
            'cmbNoe.Items.Add("بدون وضعیت")
            'cmbNoe.Items.Add("تائید شده")
            'cmbNoe.Items.Add("تائید نشده")
            'cmbNoe.Items.Add("ارسال شده")

            Strsql = "Select ccForoshandeh,LN From qryFO_Foroshandeh where ccForoshandeh <> 0 AND CodeMahal = " & CodeMahalFaal '& "AND sVazeiat <> " & UD_Dll.Enums.FO_VaziatForoshandeh.NoFaal & " "
            Strsql &= " AND Not exists (Select * From tblGL_SecurityData where namekarbar= '" & UserName & "' and CodeSubSystem = 628 and pk = qryFO_Foroshandeh.ccForoshandeh) order by LName"
            daSQL = New SqlDataAdapter(Strsql, ConnectionString)
            daSQL.Fill(dsForm, "tblForoshandeh")

            cmbBazaryab.DataSource = Nothing
            cmbBazaryab.Items.Clear()
            cmbBazaryab.DataSource = dsForm.Tables("tblForoshandeh").DefaultView
            cmbBazaryab.ValueMember = "ccForoshandeh"
            cmbBazaryab.DisplayMember = "LN"
            cmbBazaryab.SelectedIndex = -1

            daSQL = Nothing
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->LoadCombo")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->LoadCombo")
        End Try
    End Sub
    Private Sub txtCodeMoshtary_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCodeMoshtary.TextChanged
        Dim Criteria As String = ""
        If flg = True Then Exit Sub
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
    Private Sub txtCodeMoshtary_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCodeMoshtary.KeyPress
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

    'Private Sub SetGridSatr()


    '    Try
    '        With GridEXSatr
    '            .DataSource = Nothing
    '            .DataSource = dt_SearchSatr.DefaultView
    '            .SetDataBinding(dt_SearchSatr.DefaultView, "")
    '            .RetrieveStructure()

    '        End With
    '        With GridEXSatr
    '            .TabKeyBehavior = Janus.Windows.GridEX.TabKeyBehavior.ColumnNavigation
    '            .UpdateMode = Janus.Windows.GridEX.UpdateMode.RowUpdate
    '            .AllowEdit = Janus.Windows.GridEX.InheritableBoolean.True
    '            .AllowAddNew = Janus.Windows.GridEX.InheritableBoolean.True
    '        End With
    '        For i As Integer = 0 To GridEXSatr.CurrentTable.Columns.Count - 1
    '            GridEXSatr.CurrentTable.Columns.Item(i).Width = 150
    '            GridEXSatr.CurrentTable.Columns.Item("ccKala").Visible = False
    '            GridEXSatr.CurrentTable.Columns.Item("ccPishFaktorSatr").Visible = False
    '        Next
    '        With GridEXSatr.CurrentTable
    '            .Columns("CodeKala").EditType = Janus.Windows.GridEX.EditType.TextBox
    '            .Columns("Tedad3").EditType = Janus.Windows.GridEX.EditType.TextBox
    '            .Columns("Fee").EditType = Janus.Windows.GridEX.EditType.TextBox
    '            .Columns("DarsadTakhfif").EditType = Janus.Windows.GridEX.EditType.TextBox
    '            .Columns("ccKala").Visible = False
    '            .Columns("ccPishFaktorSatr").Visible = False
    '        End With


    '    Catch sqlExc As SqlException
    '        MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->SetGridStyle")
    '    Catch ex As Exception
    '        MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->SetGridStyle")
    '    End Try

    'End Sub
    Private Function IsJayezehRow(ByVal row As Janus.Windows.GridEX.GridEXRow) As Boolean
        Dim v As Object = row.Cells("IsJayezeh").Value
        If v Is Nothing OrElse IsDBNull(v) Then Return False
        Return Convert.ToBoolean(v)
    End Function
    Private _gridStructureReady As Boolean = False

    Private Sub SetGridSatr()
        Try
            With GridEXSatr
                .SetDataBinding(dt_SearchSatr.DefaultView, "")
                If Not _gridStructureReady Then
                    .RetrieveStructure()
                    _gridStructureReady = True

                    'For i As Integer = 0 To .CurrentTable.Columns.Count - 1
                    '    .CurrentTable.Columns(i).Width = 150
                    'Next
                    '.CurrentTable.Columns("ccKala").Visible = False
                    '.CurrentTable.Columns("ccPishFaktorSatr").Visible = False

                    .TabKeyBehavior = Janus.Windows.GridEX.TabKeyBehavior.ColumnNavigation
                    .UpdateMode = Janus.Windows.GridEX.UpdateMode.RowUpdate
                    .AllowEdit = Janus.Windows.GridEX.InheritableBoolean.True
                    .AllowAddNew = Janus.Windows.GridEX.InheritableBoolean.True


                    .AlternatingColors = True
                    .AlternatingRowFormatStyle.BackColor = Color.LightGray
                    .RowFormatStyle.BackColor = Color.White

                End If
            End With

            For i As Integer = 0 To GridEXSatr.CurrentTable.Columns.Count - 1
                GridEXSatr.CurrentTable.Columns.Item(i).Visible = False
            Next


            GridEXSatr.CurrentTable.Columns.Item("Radif").Caption = "ردیف"
            GridEXSatr.CurrentTable.Columns.Item("Radif").Visible = True
            GridEXSatr.CurrentTable.Columns.Item("Radif").Width = 40
            'GridEXSatr.CurrentTable.Columns.Item("Radif").EditType = Janus.Windows.GridEX.EditType.NoEdit
            GridEXSatr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXSatr.CurrentTable.Columns.Item("Radif").Position = 1
            GridEXSatr.CurrentTable.Columns.Item("Radif").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXSatr.CurrentTable.Columns.Item("CodeKala").Caption = "کد کالا"
            GridEXSatr.CurrentTable.Columns.Item("CodeKala").Visible = True
            GridEXSatr.CurrentTable.Columns.Item("CodeKala").Width = 100
            GridEXSatr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXSatr.CurrentTable.Columns.Item("CodeKala").Position = 2
            GridEXSatr.CurrentTable.Columns.Item("CodeKala").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXSatr.CurrentTable.Columns.Item("NameKala").Caption = "نام کالا"
            GridEXSatr.CurrentTable.Columns.Item("NameKala").Visible = True
            GridEXSatr.CurrentTable.Columns.Item("NameKala").Width = 260
            GridEXSatr.CurrentTable.Columns.Item("NameKala").EditType = Janus.Windows.GridEX.EditType.NoEdit
            GridEXSatr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXSatr.CurrentTable.Columns.Item("NameKala").Position = 3
            GridEXSatr.CurrentTable.Columns.Item("NameKala").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center


            GridEXSatr.CurrentTable.Columns.Item("Tedad3").Caption = "تعداد کالا"
            GridEXSatr.CurrentTable.Columns.Item("Tedad3").Visible = True
            GridEXSatr.CurrentTable.Columns.Item("Tedad3").Width = 70
            GridEXSatr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOn
            GridEXSatr.CurrentTable.Columns.Item("Tedad3").Position = 4
            GridEXSatr.CurrentTable.Columns.Item("Tedad3").FormatString = "###,###.##"
            GridEXSatr.CurrentTable.Columns.Item("Tedad3").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center


            'جایزه


            With GridEXSatr.CurrentTable.Columns.Item("IsJayezeh")
                .Caption = "جایزه"
                .Visible = True
                .Width = 70
                .Position = 5
                .TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

                ' این دو خط باعث می‌شن ستون به‌صورت تیک نمایش داده بشه و قابل کلیک باشه
                .ColumnType = Janus.Windows.GridEX.ColumnType.CheckBox
                .EditType = Janus.Windows.GridEX.EditType.CheckBox
            End With

            GridEXSatr.EditMode = Janus.Windows.GridEX.EditMode.EditOn
            'جایزه
            If AllowChangeFeePishFaktor Then
                GridEXSatr.CurrentTable.Columns.Item("Fee").Caption = "فی"
                GridEXSatr.CurrentTable.Columns.Item("Fee").Visible = True
                GridEXSatr.CurrentTable.Columns.Item("Fee").Width = 100
                GridEXSatr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
                'GridEXSatr.CurrentTable.Columns.Item("Fee").EditType = Janus.Windows.GridEX.EditType.NoEdit
                GridEXSatr.CurrentTable.Columns.Item("Fee").Position = 6
                GridEXSatr.CurrentTable.Columns.Item("Fee").FormatMode = Janus.Windows.GridEX.FormatMode.UseIFormattable
                GridEXSatr.CurrentTable.Columns.Item("Fee").FormatString = "###,###"
                GridEXSatr.CurrentTable.Columns.Item("Fee").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            Else
                GridEXSatr.CurrentTable.Columns.Item("Fee").Caption = "فی"
                GridEXSatr.CurrentTable.Columns.Item("Fee").Visible = True
                GridEXSatr.CurrentTable.Columns.Item("Fee").Width = 100
                GridEXSatr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
                GridEXSatr.CurrentTable.Columns.Item("Fee").EditType = Janus.Windows.GridEX.EditType.NoEdit
                GridEXSatr.CurrentTable.Columns.Item("Fee").Position = 6
                GridEXSatr.CurrentTable.Columns.Item("Fee").FormatMode = Janus.Windows.GridEX.FormatMode.UseIFormattable
                GridEXSatr.CurrentTable.Columns.Item("Fee").FormatString = "###,###"
                GridEXSatr.CurrentTable.Columns.Item("Fee").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            End If



            GridEXSatr.CurrentTable.Columns.Item("MojodiDarHaleForosh").Caption = "موجودی"
            GridEXSatr.CurrentTable.Columns.Item("MojodiDarHaleForosh").Visible = True
            GridEXSatr.CurrentTable.Columns.Item("MojodiDarHaleForosh").Width = 100
            GridEXSatr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXSatr.CurrentTable.Columns.Item("MojodiDarHaleForosh").EditType = Janus.Windows.GridEX.EditType.NoEdit
            GridEXSatr.CurrentTable.Columns.Item("MojodiDarHaleForosh").Position = 7
            GridEXSatr.CurrentTable.Columns.Item("MojodiDarHaleForosh").FormatString = "N"
            GridEXSatr.CurrentTable.Columns.Item("MojodiDarHaleForosh").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center






            GridEXSatr.CurrentTable.Columns.Item("MKol3").Caption = "جمع مبلغ"
            GridEXSatr.CurrentTable.Columns.Item("MKol3").Visible = True
            GridEXSatr.CurrentTable.Columns.Item("MKol3").Width = 100
            GridEXSatr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXSatr.CurrentTable.Columns.Item("MKol3").EditType = Janus.Windows.GridEX.EditType.NoEdit
            GridEXSatr.CurrentTable.Columns.Item("MKol3").Position = 8
            GridEXSatr.CurrentTable.Columns.Item("MKol3").FormatString = "###,###"
            GridEXSatr.CurrentTable.Columns.Item("MKol3").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXSatr.CurrentTable.Columns.Item("DarsadTakhfif").Caption = "درصد تخفیف"
            GridEXSatr.CurrentTable.Columns.Item("DarsadTakhfif").Visible = True
            GridEXSatr.CurrentTable.Columns.Item("DarsadTakhfif").Width = 100
            GridEXSatr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            'GridEXSatr.CurrentTable.Columns.Item("DarsadTakhfif").EditType = Janus.Windows.GridEX.EditType.NoEdit
            GridEXSatr.CurrentTable.Columns.Item("DarsadTakhfif").Position = 9
            GridEXSatr.CurrentTable.Columns.Item("DarsadTakhfif").FormatString = "N"

            GridEXSatr.CurrentTable.Columns.Item("DarsadTakhfif").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXSatr.CurrentTable.Columns.Item("TakhfifKala").Caption = "تخفیف کالا"
            GridEXSatr.CurrentTable.Columns.Item("TakhfifKala").Visible = True
            GridEXSatr.CurrentTable.Columns.Item("TakhfifKala").Width = 120
            GridEXSatr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXSatr.CurrentTable.Columns.Item("TakhfifKala").EditType = Janus.Windows.GridEX.EditType.NoEdit
            GridEXSatr.CurrentTable.Columns.Item("TakhfifKala").Position = 10
            GridEXSatr.CurrentTable.Columns.Item("TakhfifKala").FormatString = "###,###"
            GridEXSatr.CurrentTable.Columns.Item("TakhfifKala").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center


            GridEXSatr.CurrentTable.Columns.Item("MablaghMalyat").Caption = "مالیات"
            GridEXSatr.CurrentTable.Columns.Item("MablaghMalyat").Visible = True
            GridEXSatr.CurrentTable.Columns.Item("MablaghMalyat").Width = 80
            GridEXSatr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXSatr.CurrentTable.Columns.Item("MablaghMalyat").EditType = Janus.Windows.GridEX.EditType.NoEdit
            GridEXSatr.CurrentTable.Columns.Item("MablaghMalyat").Position = 11
            GridEXSatr.CurrentTable.Columns.Item("MablaghMalyat").FormatString = "###,###"
            GridEXSatr.CurrentTable.Columns.Item("MablaghMalyat").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center


            GridEXSatr.CurrentTable.Columns.Item("FeeKol").Caption = "مبلغ نهایی"
            GridEXSatr.CurrentTable.Columns.Item("FeeKol").Visible = True
            GridEXSatr.CurrentTable.Columns.Item("FeeKol").Width = 130
            GridEXSatr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXSatr.CurrentTable.Columns.Item("FeeKol").EditType = Janus.Windows.GridEX.EditType.NoEdit
            GridEXSatr.CurrentTable.Columns.Item("FeeKol").Position = 12
            GridEXSatr.CurrentTable.Columns.Item("FeeKol").FormatString = "###,###"
            GridEXSatr.CurrentTable.Columns.Item("FeeKol").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            ' tblPishFaktorSatr.ShomarehBach , 
            'tblPishFaktorSatr.TarikhTolid , tblPishFaktorSatr.TarikhENgheza
            GridEXSatr.CurrentTable.Columns.Item("ShomarehBach").Caption = "شماره بچ"
            GridEXSatr.CurrentTable.Columns.Item("ShomarehBach").Visible = True
            GridEXSatr.CurrentTable.Columns.Item("ShomarehBach").Width = 120
            GridEXSatr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXSatr.CurrentTable.Columns.Item("ShomarehBach").EditType = Janus.Windows.GridEX.EditType.NoEdit
            GridEXSatr.CurrentTable.Columns.Item("ShomarehBach").Position = 13
            'GridEXSatr.CurrentTable.Columns.Item("ShomarehBach").FormatString = "###,###"
            GridEXSatr.CurrentTable.Columns.Item("ShomarehBach").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXSatr.CurrentTable.Columns.Item("TarikhTolid").Caption = "تاریخ تولید"
            GridEXSatr.CurrentTable.Columns.Item("TarikhTolid").Visible = True
            GridEXSatr.CurrentTable.Columns.Item("TarikhTolid").Width = 80
            GridEXSatr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXSatr.CurrentTable.Columns.Item("TarikhTolid").EditType = Janus.Windows.GridEX.EditType.NoEdit
            GridEXSatr.CurrentTable.Columns.Item("TarikhTolid").Position = 14
            GridEXSatr.CurrentTable.Columns.Item("TarikhTolid").FormatString = "###,###"
            GridEXSatr.CurrentTable.Columns.Item("TarikhTolid").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXSatr.CurrentTable.Columns.Item("TarikhENgheza").Caption = "تاریخ انقضا"
            GridEXSatr.CurrentTable.Columns.Item("TarikhENgheza").Visible = True
            GridEXSatr.CurrentTable.Columns.Item("TarikhENgheza").Width = 80
            GridEXSatr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXSatr.CurrentTable.Columns.Item("TarikhENgheza").EditType = Janus.Windows.GridEX.EditType.NoEdit
            GridEXSatr.CurrentTable.Columns.Item("TarikhENgheza").Position = 15
            GridEXSatr.CurrentTable.Columns.Item("TarikhENgheza").FormatString = "###,###"
            GridEXSatr.CurrentTable.Columns.Item("TarikhENgheza").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXSatr.CurrentTable.Columns.Item("ccKala").Caption = "ccKala"
            GridEXSatr.CurrentTable.Columns.Item("ccKala").Visible = False
            GridEXSatr.CurrentTable.Columns.Item("ccKala").Width = 0
            GridEXSatr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOn
            GridEXSatr.CurrentTable.Columns.Item("ccKala").Position = 16
            GridEXSatr.CurrentTable.Columns.Item("ccKala").FormatString = "N"
            GridEXSatr.CurrentTable.Columns.Item("ccKala").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXSatr.CurrentTable.Columns.Item("ccPishFaktorSatr").Caption = "ccPishFaktorSatr"
            GridEXSatr.CurrentTable.Columns.Item("ccPishFaktorSatr").Visible = False
            GridEXSatr.CurrentTable.Columns.Item("ccPishFaktorSatr").Width = 0
            GridEXSatr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOn
            'GridEXSatr.CurrentTable.Columns.Item("ccPishFaktorSatr").Position = 13
            GridEXSatr.CurrentTable.Columns.Item("ccPishFaktorSatr").FormatString = "N"
            GridEXSatr.CurrentTable.Columns.Item("ccPishFaktorSatr").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly, "Error in SetGridSatr")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error in SetGridSatr")
        End Try
    End Sub

    Private Sub btnSaveSanad_Click(sender As Object, e As EventArgs) Handles btnSaveSanad.Click
        If ccElamMarjoee <> 0 Then Exit Sub        ' جلوگیری از Insert دوباره‌ی هدر
        If Not ValidateTitr() Then Exit Sub      ' قبل از ذخیره‌ی هدر
        AddNewRecord()
        If ccElamMarjoee = 0 Then Exit Sub


        Dim info As New ElamMarjoeeTitrInfo()
        If Not info.Load(ccElamMarjoee, ConnectionString) Then
            MessageBox.Show("اطلاعات پیش‌فاکتور ذخیره‌شده پیدا نشد")

        End If
        '' لود هدر ذخیره‌شده از دیتابیس و نمایش در فرم
        'If Not LoadTitr(ccPishfaktorTitr) Then Exit Sub

        SearchSatr()
        btnSaveSanad.Enabled = False

        GridEXSatr.Enabled = True
        GridEXSatr.Focus()
        GridEXSatr.MoveToNewRecord()
        GridEXSatr.Col = 0

    End Sub
    Private Function LoadTitr(ByVal id As Integer) As Boolean
        Dim info As New ElamMarjoeeTitrInfo()

        Try
            If Not info.Load(id, ConnectionString) Then
                MessageBox.Show("اطلاعات پیش‌فاکتور ذخیره‌شده پیدا نشد")
                Return False
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Return False
        End Try


        txtSearchNumber.Text = info.ShomarehElamMarjoee
        mskTarikh.Text = info.TarikhElamMarjoee
        txtCodeMoshtary.Text = info.CodeMoshtary
        txtCodeMoshtary.Tag = info.ccMoshtary
        cmbBazaryab.SelectedValue = info.ccForoshandeh
        cmbMamorPakhsh.SelectedValue = info.CodeFard_MamorPakhsh
        If info.NoeMarjoeeFT = 1 Then
            rbBargashtAzForosh.Checked = True
        ElseIf info.NoeMarjoeeFT = 2 Then
            rbBargashtAzTozi.Checked = True
        End If
        TxtSharh.Text = info.Tozihat

        Return True
    End Function
    Private Function IsComboEmpty(ByVal cmb As ComboBox) As Boolean
        If cmb.SelectedIndex < 0 Then Return True
        Dim v As Object = cmb.SelectedValue
        If v Is Nothing OrElse IsDBNull(v) Then Return True
        Return False
    End Function

    Private Function ValidateTitr() As Boolean



        If Len(mskTarikh.Text.ToString) <> 0 Then
            If Not objTarikh.IsShDate(mskTarikh.Text.ToString) Then
                mskTarikh.Focus()
                Return False
            End If
            If mskTarikh.Text.Substring(0, 4) <> CodeDoreh Then
                MsgBox("تاريخ با دوره مالی فعال يکی نيست.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
                mskTarikh.Focus()
                Return False
            End If
            If mskTarikh.Text > TarikhEmrooz Then
                ErrPro.SetError(Me.mskTarikh, "تاریخ اعلام مرجوعی نباید جلوتر از تاریخ امروز  باشد.")
                MsgBox("تاریخ اعلام مرجوعی نباید جلوتر از تاریخ امروز  باشد.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
                mskTarikh.Focus()
                Return False
            End If
            If ObjCode.CheckCompany <> 5 Then
                If mskTarikh.Text < TarikhEmrooz Then
                    ErrPro.SetError(Me.mskTarikh, "تاریخ اعلام مرجوعی نباید پیش از تاریخ امروز  باشد.")
                    MsgBox("تاریخ اعلام مرجوعی نباید پیش از تاریخ امروز  باشد.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
                    mskTarikh.Focus()
                    Return False
                End If
            End If

        Else
            ErrPro.SetError(Me.mskTarikh, "تاريخ را وارد کنيد.")
            MsgBox("تاريخ را وارد کنيد.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
            mskTarikh.Focus()
            Return False
        End If
        ErrPro.SetError(Me.mskTarikh, "")


        If rbBargashtAzForosh.Checked = False And rbBargashtAzTozi.Checked = False Then
            ErrPro.SetError(Me.GroupBox5, "نوع مرجوعی را مشخص کنید.")
            MsgBox("نوع مرجوعی را مشخص کنید.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
            GroupBox5.Focus()
            Return False
        End If
        ErrPro.SetError(Me.GroupBox5, "")


        If objTools.DCount("ccFaktorTitr", "tblFO_ElamMarjoee", "ccFaktorTitr = " & ccFaktorTitr & " AND IsFaktorMarjoee = 1  and sVazeiat=4355") <> 0 Then
            MsgBox("برای این شماره فاکتور قبلاً فاکتور مرجوعی صادر شده است !.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, )
            Exit Function
        End If


        If mskTarikh.Text.Length <> 0 Then
            If mskTarikh.Text < TarikhEmrooz Then
                ErrPro.SetError(Me.mskTarikh, "تاريخ نمی تواند از تاریخ روز عقب تر باشد.")
                MsgBox("تاريخ نمی تواند از تاریخ روز عقب تر باشد.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
                Exit Function
            End If
        End If



        ' فروشنده
        If IsComboEmpty(cmbBazaryab) Then
            MessageBox.Show("فروشنده را انتخاب کنید")
            cmbBazaryab.Focus()
            Return False
        End If



        ' انبار
        If IsComboEmpty(cmbMamorPakhsh) Then
            MessageBox.Show("مامور پخش را انتخاب کنید")
            cmbMamorPakhsh.Focus()
            Return False
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

        Return True
    End Function
    Private Sub AddNewRecord()


        Dim da As SqlDataAdapter = New SqlDataAdapter

        Using cn As New SqlConnection(ConnectionString)
            Using cm As SqlCommand = cn.CreateCommand()
                cn.Open()
                cm.Parameters.Clear()
                cm.CommandType = CommandType.StoredProcedure
                cm.CommandText = "[Sales].[spMarjoeeVorodKoli_InsertTitr]"
                cm.Parameters.AddWithValue("CodeMahal", CodeMahalFaal)
                cm.Parameters.AddWithValue("CodeDoreh", CodeDoreh)
                cm.Parameters.AddWithValue("TarikhElmMarjoee", mskTarikh.Text)
                cm.Parameters.AddWithValue("ccMoshtary", txtCodeMoshtary.Tag)
                cm.Parameters.AddWithValue("ccForoshandeh", cmbBazaryab.SelectedValue)
                cm.Parameters.AddWithValue("CodeFard_MamorPakhsh", cmbMamorPakhsh.SelectedValue)
                If rbBargashtAzForosh.Checked Then
                    cm.Parameters.AddWithValue("NoeMarjoeeFT", 1)
                ElseIf rbBargashtAzTozi.Checked Then
                    cm.Parameters.AddWithValue("NoeMarjoeeFT", 2)
                End If
                cm.Parameters.AddWithValue("Malyat", chkMalyat.Checked)
                cm.Parameters.AddWithValue("PishFaktorAmani", 0)
                cm.Parameters.AddWithValue("PishFaktorGheireGhateei", 0)
                cm.Parameters.AddWithValue("Tozihat", TxtSharh.Text.TrimEnd)
                cm.Parameters.AddWithValue("NoeVorod", 1)
                cm.Parameters.AddWithValue("UserName", UserName)

                Dim pOut As SqlParameter = cm.Parameters.Add("@ccElamMarjoee", SqlDbType.Int)
                pOut.Direction = ParameterDirection.Output
                cm.ExecuteNonQuery()

                If pOut.Value IsNot DBNull.Value Then
                    ccElamMarjoee = Convert.ToInt32(pOut.Value)
                End If
            End Using
        End Using

    End Sub
    Private Sub SearchSatr()



        Dim da As SqlDataAdapter = New SqlDataAdapter

        Using cn As New SqlConnection(ConnectionString)
            Using cm As SqlCommand = cn.CreateCommand()
                cn.Open()
                cm.Parameters.Clear()
                cm.CommandType = CommandType.StoredProcedure
                cm.CommandText = "[Sales].[spMarjoeeVorodKoli_SearchSatr]"
                cm.Parameters.AddWithValue("ccElamMarjoee", ccElamMarjoee)
                da.SelectCommand = cm
                cm.CommandTimeout = 999999
                dt_SearchSatr = New DataTable
                da.Fill(dt_SearchSatr)
            End Using
        End Using

        SetGridSatr()
    End Sub

    Private Sub GridEXSatr_KeyDown(sender As Object, e As KeyEventArgs) Handles GridEXSatr.KeyDown
        If e.KeyCode = Keys.F2 Then


            Dim info As New ElamMarjoeeTitrInfo()
            info.Load(ccElamMarjoee, ConnectionString)


            If GridEXSatr.CurrentColumn Is Nothing Then Exit Sub
            If GridEXSatr.CurrentRow.RowType <> Janus.Windows.GridEX.RowType.NewRecord Then Exit Sub
            If GridEXSatr.CurrentColumn Is Nothing Then Exit Sub
            If GridEXSatr.CurrentColumn.Key = "CodeKala" Then

                Dim objKala As New Forms_dll.frmAN_KalaSearch
                Dim StrSqlKala As String



                StrSqlKala = "Select  CodeKala,NameKala,ccKala,txtsVahedeShomaresh,sVahedeShomaresh,NameBrand,RadifBrand,0 as IsSabadKala "
                'StrSqlKala &= " ,(SELECT dbo.fnAN_GetMojodiDarHalForosh (" & info.ccAnbar & ", ccKala, " & CodeMahalFaal & "," & CodeDoreh & "," & TarikhEmrooz & ")) AS MojodiDarHalForosh, "
                'StrSqlKala &= " (SELECT dbo.[GetMablaghFrosh_NoePardakht] (ccKala, " & info.PishFaktorTarikh & ", " & CodeMahalFaal & ", " & info.ccMoshtary & " , " & info.sNoeMoshtary & ", 3952)) as FeeForoshCheck ,"
                'StrSqlKala &= " (SELECT dbo.[GetMablaghFrosh_NoePardakht] (ccKala," & info.PishFaktorTarikh & ", " & CodeMahalFaal & ", " & info.ccMoshtary & " , " & info.sNoeMoshtary & ", 3953)) as FeeForoshNaghd"
                StrSqlKala &= " from qryAN_Kala"
                StrSqlKala &= " Where Faal = 1  AND ccKala in (Select ccKala from tblAN_KalaGheymat where ccKala =qryAN_Kala.ccKala )  "




                MultiSelection = False
                SearchItem = "CodeKala"
                objKala.SetForm(StrSqlKala)
                objKala.ShowDialog()

                FillRowWithKala(GridEXSatr.CurrentRow, objKala.tcodeKala, objKala.tccKala, objKala.tNameKala)
                'GridEXSatr.CurrentRow.Cells("CodeKala").Value = objKala.tcodeKala
                'GridEXSatr.CurrentRow.Cells("ccKala").Value = objKala.tccKala
                'GridEXSatr.CurrentRow.Cells("NameKala").Value = objKala.tNameKala
                'GridEXSatr.CurrentRow.Cells("Fee").Value = ObjCode.GetMablaghForosh_NoePardakht(objKala.tccKala, info.PishFaktorTarikh, CodeMahalFaal, info.ccMoshtary, info.sNoePardakht)
                'GetMojodyGhabelForosh(objKala.tccKala, info.ccAnbar, MojodiGhabelForoshKOL)
                'GridEXSatr.CurrentRow.Cells("MojodiDarHaleForosh").Value = MojodiGhabelForoshKOL



                MultiSelection = False


            End If

        End If


    End Sub





    Private Sub GridEXSatr_AddingRecord(sender As Object, e As System.ComponentModel.CancelEventArgs) _
    Handles GridEXSatr.AddingRecord
        If Not SaveRow(GridEXSatr.CurrentRow, True) Then e.Cancel = True
        'GridEXSatr.Col = GridEXSatr.RootTable.Columns("CodeKala").Position
    End Sub

    'Private Sub GridEXSatr_UpdatingRecord(sender As Object, e As System.ComponentModel.CancelEventArgs) _
    'Handles GridEXSatr.UpdatingRecord
    '    'If Not SaveRow(GridEXSatr.CurrentRow, False) Then e.Cancel = True
    '    e.Cancel = True   ' ویرایش نداریم
    'End Sub
    Private Sub GridEXSatr_UpdatingRecord(ByVal sender As Object,
        ByVal e As System.ComponentModel.CancelEventArgs) Handles GridEXSatr.UpdatingRecord
        If Not UpdateRow(GridEXSatr.CurrentRow) Then e.Cancel = True
    End Sub

    Private Sub GridEXSatr_RecordAdded(sender As Object, e As EventArgs) Handles GridEXSatr.RecordAdded
        'GridEXSatr.MoveToNewRecord()
        'GridEXSatr.Col = 0
        BeginInvoke(New MethodInvoker(AddressOf ReloadSatr))
    End Sub
    Private Sub ReloadSatr()
        SearchSatr()                       ' لود دوباره از SP
        GridEXSatr.MoveToNewRecord()
        GridEXSatr.Col = 0
        GridEXSatr.Focus()
    End Sub
    'jayezeh
    'Private Sub GridEXSatr_EditingCell(sender As Object, e As Janus.Windows.GridEX.EditingCellEventArgs) _
    'Handles GridEXSatr.EditingCell
    '    ' فقط ردیف جدید قابل ویرایشه
    '    If GridEXSatr.CurrentRow IsNot Nothing AndAlso
    '   GridEXSatr.CurrentRow.RowType <> Janus.Windows.GridEX.RowType.NewRecord Then
    '        e.Cancel = True
    '    End If
    'End Sub
    ' جلوگیری از ویرایش DarsadTakhfif وقتی جایزه است
    'jayezeh
    'Private Sub GridEXSatr_EditingCell(ByVal sender As Object,
    '    ByVal e As Janus.Windows.GridEX.EditingCellEventArgs) Handles GridEXSatr.EditingCell

    '    If GridEXSatr.CurrentRow Is Nothing Then Exit Sub

    '    ' ردیف‌های ذخیره‌شده قابل ویرایش نیستند
    '    If GridEXSatr.CurrentRow.RowType <> Janus.Windows.GridEX.RowType.NewRecord Then
    '        e.Cancel = True
    '        Exit Sub
    '    End If


    '    Dim colKey As String = ""
    '    If GridEXSatr.CurrentColumn IsNot Nothing Then colKey = GridEXSatr.CurrentColumn.Key

    '    ' تیک جایزه وقتی تنظیمات اجازه نمی‌ده
    '    If Not AllowPishFaktorTakhfifDasty AndAlso colKey = "IsJayezeh" Then
    '        e.Cancel = True
    '        Exit Sub
    '    End If


    '    ' درصد تخفیف وقتی تنظیمات اجازه نمی‌ده، یا ردیف جایزه است
    '    If colKey = "DarsadTakhfif" Then
    '        If Not AllowPishFaktorTakhfifDasty OrElse IsJayezehRow(GridEXSatr.CurrentRow) Then
    '            e.Cancel = True
    '        End If
    '    End If
    'End Sub
    'jayezeh
    Private _oldTedad As Double = 0
    Private Sub GridEXSatr_EditingCell(ByVal sender As Object,
      ByVal e As Janus.Windows.GridEX.EditingCellEventArgs) Handles GridEXSatr.EditingCell

        If GridEXSatr.CurrentRow Is Nothing OrElse GridEXSatr.CurrentColumn Is Nothing Then Exit Sub

        Dim colKey As String = GridEXSatr.CurrentColumn.Key
        Dim isNewRow As Boolean = (GridEXSatr.CurrentRow.RowType = Janus.Windows.GridEX.RowType.NewRecord)

        ' ردیف ذخیره‌شده: فقط تعداد، فی و درصد تخفیف قابل ویرایش است
        If Not isNewRow Then
            If colKey <> "Tedad3" AndAlso colKey <> "Fee" AndAlso colKey <> "DarsadTakhfif" Then
                e.Cancel = True
                Exit Sub
            End If
        End If

        ' تیک جایزه وقتی تنظیمات اجازه نمی‌ده
        If Not AllowPishFaktorTakhfifDasty AndAlso colKey = "IsJayezeh" Then
            e.Cancel = True
            Exit Sub
        End If

        ' درصد تخفیف وقتی تنظیمات اجازه نمی‌ده، یا ردیف جایزه است
        If colKey = "DarsadTakhfif" Then
            If Not AllowPishFaktorTakhfifDasty OrElse IsJayezehRow(GridEXSatr.CurrentRow) Then
                e.Cancel = True
            End If
        End If
    End Sub
    Private Function IsRowEmpty(row As Janus.Windows.GridEX.GridEXRow) As Boolean
        For Each key As String In New String() {"CodeKala", "ccKala", "Tedad3", "DarsadTakhfif"}
            Dim v As Object = row.Cells(key).Value
            If v IsNot Nothing AndAlso Not IsDBNull(v) Then
                If Convert.ToString(v).Trim().Length > 0 Then Return False
            End If
        Next
        Return True
    End Function
    Private Function SaveRow(row As Janus.Windows.GridEX.GridEXRow, isNew As Boolean) As Boolean
        If row Is Nothing Then Return False

        ' ردیف خالی: ذخیره نشه، پیام نده، ولی Cancel بشه تا به ردیف بعدی نره
        If isNew AndAlso IsRowEmpty(row) Then Return False
        If Not ValidateRow(row) Then Return False


        Try
            Using con As New SqlConnection(ConnectionString)
                Using cmd As SqlCommand = con.CreateCommand()
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.CommandText = "[Sales].[spMarjoeeVorodKoli_InsertSatr]"
                    cmd.Parameters.AddWithValue("@ccElamMarjoee", ccElamMarjoee)
                    cmd.Parameters.AddWithValue("@ccKala", row.Cells("ccKala").Value)
                    cmd.Parameters.AddWithValue("@Tedad3", If(row.Cells("Tedad3").Value, 0))
                    cmd.Parameters.AddWithValue("@Fee", If(row.Cells("Fee").Value, 0))
                    cmd.Parameters.AddWithValue("@IsJayezeh", IsJayezehRow(row))
                    ' درصد تخفیف برای جایزه همیشه صفر
                    Dim darsad As Object = If(IsJayezehRow(row), CObj(0), IIf(row.Cells("DarsadTakhfif").Value Is DBNull.Value, 0, row.Cells("DarsadTakhfif").Value))
                    cmd.Parameters.AddWithValue("@DarsadTakhfif", darsad)
                    'cmd.Parameters.AddWithValue("@DarsadTakhfif", IIf(row.Cells("DarsadTakhfif").Value Is DBNull.Value, 0, row.Cells("DarsadTakhfif").Value))

                    Dim outId As New SqlParameter("@ccPishFaktorSatr", SqlDbType.Int) With {
                     .Direction = ParameterDirection.Output}
                    cmd.Parameters.Add(outId)

                    con.Open()
                    cmd.ExecuteNonQuery()

                    'If isNew AndAlso Not IsDBNull(outId.Value) Then
                    '    row.BeginEdit()
                    '    row.Cells("ccPishFaktorSatr").Value = outId.Value
                    '    row.EndEdit()
                    'End If
                End Using
            End Using
            Return True
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Return False
        End Try

    End Function

    Private Function UpdateRow(ByVal row As Janus.Windows.GridEX.GridEXRow) As Boolean
        If row Is Nothing Then Return False

        If Not ValidateUpdate(row) Then Return False     ' ولیدیشن مخصوص ویرایش

        Try
            Using con As New SqlConnection(ConnectionString)
                Using cmd As SqlCommand = con.CreateCommand()
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.CommandText = "[Sales].[spPishFaktorVorodKoli_UpdateSatr]"
                    cmd.Parameters.AddWithValue("@ccPishFaktorSatr", row.Cells("ccPishFaktorSatr").Value)
                    cmd.Parameters.AddWithValue("@Tedad3", row.Cells("Tedad3").Value)
                    cmd.Parameters.AddWithValue("@Fee", row.Cells("Fee").Value)

                    Dim darsad As Object = 0
                    If AllowPishFaktorTakhfifDasty AndAlso Not IsJayezehRow(row) Then
                        darsad = If(row.Cells("DarsadTakhfif").Value, DBNull.Value)
                    End If
                    cmd.Parameters.AddWithValue("@DarsadTakhfif", darsad)

                    con.Open()
                    cmd.ExecuteNonQuery()
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Return False
        End Try

        ' چون ممکنه ردیف‌های لینک‌شده (بچ‌ها) هم عوض شده باشن، دوباره لود کن
        BeginInvoke(New MethodInvoker(AddressOf ReloadSatr))
        Return True
    End Function


    ' مقدار سلول را به عدد تبدیل می‌کند؛ اگر خالی یا غیرعددی بود False برمی‌گرداند
    Private Function TryGetNumber(ByVal row As Janus.Windows.GridEX.GridEXRow,
                                  ByVal colKey As String, ByRef result As Double) As Boolean
        result = 0
        Dim v As Object = row.Cells(colKey).Value
        If v Is Nothing OrElse IsDBNull(v) Then Return False
        If v.ToString().Trim().Length = 0 Then Return False
        Return Double.TryParse(v.ToString().Trim(), result)
    End Function
    Private Function ValidateUpdate(ByVal row As Janus.Windows.GridEX.GridEXRow) As Boolean

        Dim num As Double


        Dim info As New ElamMarjoeeTitrInfo()
        info.Load(ccElamMarjoee, ConnectionString)

        Dim OldTedad As Double = 0
        OldTedad = objTools.ConvertNulls(objTools.DLookup("Tedad3", "tblFO_PishFaktorSatr", "ccPishFaktorSatr =" & row.Cells("ccPishFaktorSatr").Value), 0)
        '
        Dim ShomarehBach As Object = row.Cells("ShomarehBach").Value
        Dim TarikhTolid As Object = row.Cells("TarikhTolid").Value
        Dim TarikhEngheza As Object = row.Cells("TarikhEngheza").Value

        Dim ccKala As Object = row.Cells("ccKala").Value
        ' کالا: حتماً انتخاب شده باشد

        If ccKala Is Nothing OrElse IsDBNull(ccKala) OrElse ccKala.ToString().Trim() = "" Then
            MessageBox.Show("کالا را انتخاب کنید")
            FocusCol("CodeKala")
            Return False
        End If


        ' تعداد: حتماً پر و بزرگ‌تر از صفر
        If Not TryGetNumber(row, "Tedad3", num) OrElse num <= 0 Then
            MessageBox.Show("تعداد را وارد کنید")
            FocusCol("Tedad3")
            Return False
        End If

        ' فی: حتماً پر (صفر هم قبول نیست؛ اگر صفر مجاز است، شرط num <= 0 را حذف کنید)
        If Not TryGetNumber(row, "Fee", num) OrElse num <= 0 Then
            MessageBox.Show("فی را وارد کنید")
            FocusCol("Fee")
            Return False
        End If
        'GetMojodyDarHalForoshBach(ccKala, info.ccAnbar, ShomarehBach, TarikhTolid, TarikhEngheza)

        Dim tedad As Double
        TryGetNumber(row, "Tedad3", tedad)

        'Dim maxMojaz As Double = MojodiGhabelForoshKOLBach + OldTedad
        'If tedad > maxMojaz Then
        '    MessageBox.Show("تعداد از موجودی قابل فروش بیشتر است." & vbCrLf &
        '            "شماره بچ: " & Convert.ToString(ShomarehBach) & vbCrLf &
        '            "حداکثر مجاز: " & maxMojaz.ToString(),
        '            "موجودی ناکافی", MessageBoxButtons.OK, MessageBoxIcon.Warning,
        '            MessageBoxDefaultButton.Button1,
        '            MessageBoxOptions.RightAlign Or MessageBoxOptions.RtlReading)
        '    FocusCol("Tedad3")
        '    Return False
        'End If



        'If tedad > MojodiGhabelForoshKOLBach + OldTedad Then
        '    MessageBox.Show("تعداد از موجودی قابل فروش بیشتر است")
        '    FocusCol("Tedad3")
        '    Return False
        'End If



        Return True
    End Function
    Private Function ValidateRow(ByVal row As Janus.Windows.GridEX.GridEXRow) As Boolean
        Dim num As Double


        Dim info As New ElamMarjoeeTitrInfo()
        info.Load(ccElamMarjoee, ConnectionString)


        ' کالا: حتماً انتخاب شده باشد
        Dim ccKala As Object = row.Cells("ccKala").Value
        If ccKala Is Nothing OrElse IsDBNull(ccKala) OrElse ccKala.ToString().Trim() = "" Then
            MessageBox.Show("کالا را انتخاب کنید")
            FocusCol("CodeKala")
            Return False
        End If


        ' تعداد: حتماً پر و بزرگ‌تر از صفر
        If Not TryGetNumber(row, "Tedad3", num) OrElse num <= 0 Then
            MessageBox.Show("تعداد را وارد کنید")
            FocusCol("Tedad3")
            Return False
        End If

        ' فی: حتماً پر (صفر هم قبول نیست؛ اگر صفر مجاز است، شرط num <= 0 را حذف کنید)
        If Not TryGetNumber(row, "Fee", num) OrElse num <= 0 Then
            MessageBox.Show("فی را وارد کنید")
            FocusCol("Fee")
            Return False
        End If

        'GetMojodyGhabelForosh(ccKala, info.ccAnbar, MojodiGhabelForoshKOL)
        ' موجودی قابل فروش: باید بزرگ‌تر از صفر باشد
        'If Not TryGetNumber(row, "MojodiDarHaleForosh", num) OrElse num <= 0 Then
        'If MojodiGhabelForoshKOL <= 0 Then

        '    MessageBox.Show("موجودی قابل فروش این کالا صفر است")
        '    FocusCol("Tedad3")
        '    Return False
        'End If
        'Dim tedad As Double
        'TryGetNumber(row, "Tedad3", tedad)
        ''TryGetNumber(row, "MojodiDarHaleForosh", mojodi)
        'If tedad > MojodiGhabelForoshKOL Then
        '    MessageBox.Show("تعداد از موجودی قابل فروش بیشتر است")
        '    FocusCol("Tedad3")
        '    Return False
        'End If


        Return True
    End Function

    Private Sub FocusCol(ByVal colKey As String)
        GridEXSatr.Col = GridEXSatr.RootTable.Columns(colKey).Position
    End Sub

    Private _isCalculating As Boolean = False

    'Private Sub GridEXSatr_CellValueChanged(ByVal sender As Object,
    '        ByVal e As Janus.Windows.GridEX.ColumnActionEventArgs) _
    '        Handles GridEXSatr.CellValueChanged

    '    If _isCalculating Then Exit Sub
    '    If e.Column Is Nothing Then Exit Sub

    '    Dim key As String = e.Column.Key
    '    If key = "Tedad3" OrElse key = "Fee" OrElse key = "DarsadTakhfif" Then
    '        CalcRow(GridEXSatr.CurrentRow)
    '    End If
    'End Sub
    'Private Sub GridEXSatr_CellUpdated(ByVal sender As Object,
    '    ByVal e As Janus.Windows.GridEX.ColumnActionEventArgs) _
    '    Handles GridEXSatr.CellUpdated

    '    'If _isCalculating Then Exit Sub
    '    'If e.Column Is Nothing Then Exit Sub

    '    'Dim key As String = e.Column.Key
    '    'If key = "Tedad3" OrElse key = "Fee" OrElse key = "DarsadTakhfif" Then
    '    '    CalcRow(GridEXSatr.CurrentRow)
    '    'End If
    '    Dim key As String = e.Column.Key
    '    If key = "IsJayezeh" AndAlso IsJayezehRow(GridEXSatr.CurrentRow) Then
    '        _isCalculating = True
    '        Try
    '            GridEXSatr.CurrentRow.Cells("DarsadTakhfif").Value = 0
    '        Finally
    '            _isCalculating = False
    '        End Try
    '    End If

    '    If key = "Tedad3" OrElse key = "Fee" OrElse key = "DarsadTakhfif" OrElse key = "IsJayezeh" Then
    '        CalcRow(GridEXSatr.CurrentRow)
    '    End If


    'End Sub
    Private Sub GridEXSatr_CellUpdated(ByVal sender As Object,
        ByVal e As Janus.Windows.GridEX.ColumnActionEventArgs) _
        Handles GridEXSatr.CellUpdated

        If _isCalculating Then Exit Sub
        If e.Column Is Nothing Then Exit Sub

        Dim key As String = e.Column.Key
        Dim row As Janus.Windows.GridEX.GridEXRow = GridEXSatr.CurrentRow
        If row Is Nothing Then Exit Sub

        ' ۱) تیک جایزه: درصد تخفیف صفر بشه
        If key = "IsJayezeh" AndAlso IsJayezehRow(row) Then
            _isCalculating = True
            Try
                row.Cells("DarsadTakhfif").Value = 0
            Finally
                _isCalculating = False
            End Try
        End If

        ' ۲) تایپ کد کالا (فقط ردیف جدید)
        If key = "CodeKala" AndAlso row.RowType = Janus.Windows.GridEX.RowType.NewRecord Then
            Dim typed As String = Convert.ToString(row.Cells("CodeKala").Value)

            If typed.Trim().Length > 0 Then
                Dim cc As Integer = FindCcKala(typed)

                If cc = 0 Then
                    MessageBox.Show("کالایی با این کد پیدا نشد. برای جستجو F2 بزنید.")
                    _isCalculating = True
                    Try
                        row.Cells("CodeKala").Value = DBNull.Value
                        row.Cells("ccKala").Value = DBNull.Value
                    Finally
                        _isCalculating = False
                    End Try
                Else
                    Dim nm As Object = objTools.DLookup("NameKala", "tblAn_Kala", "ccKala = " & cc)
                    FillRowWithKala(row, typed.Trim(), cc, nm)
                    CalcRow(row)
                End If
            End If
        End If

        ' ۳) محاسبه‌ی مبلغ و تخفیف
        If key = "Tedad3" OrElse key = "Fee" OrElse key = "DarsadTakhfif" OrElse key = "IsJayezeh" Then
            CalcRow(row)
        End If

        ' ۴) نمایش موجودی وقتی تعداد ردیف ذخیره‌شده عوض شد
        If key = "Tedad3" AndAlso row.RowType = Janus.Windows.GridEX.RowType.Record Then
            ShowMojodi(row)
        End If
    End Sub
    Private Sub ShowMojodi(ByVal row As Janus.Windows.GridEX.GridEXRow)
        Dim idSatr As Object = row.Cells("ccPishFaktorSatr").Value
        If idSatr Is Nothing OrElse IsDBNull(idSatr) Then Exit Sub

        Dim info As New ElamMarjoeeTitrInfo()
        If Not info.Load(ccElamMarjoee, ConnectionString) Then Exit Sub

        ' تعداد قبلی از دیتابیس (نه از گرید، چون مقدار جدید الان توش هست)
        Dim oldTedad As Double = Convert.ToDouble(
        objTools.ConvertNulls(objTools.DLookup("Tedad3", "tblFO_PishFaktorSatr",
            "ccPishFaktorSatr = " & idSatr.ToString()), 0))

        'GetMojodyDarHalForoshBach(row.Cells("ccKala").Value, info.ccAnbar,
        'row.Cells("ShomarehBach").Value, row.Cells("TarikhTolid").Value,
        'row.Cells("TarikhEngheza").Value)

        '_isCalculating = True
        'Try
        '    row.Cells("MojodiDarHaleForosh").Value = MojodiGhabelForoshKOLBach + oldTedad
        'Finally
        '    _isCalculating = False
        'End Try
    End Sub

    Private Sub CalcRow(ByVal row As Janus.Windows.GridEX.GridEXRow)
        If row Is Nothing Then Exit Sub

        Dim tedad, fee, darsad As Double
        TryGetNumber(row, "Tedad3", tedad)
        TryGetNumber(row, "Fee", fee)
        TryGetNumber(row, "DarsadTakhfif", darsad)

        Dim mKol As Double = tedad * fee
        Dim takhfif As Double = mKol * darsad / 100

        _isCalculating = True
        Try
            row.Cells("MKol3").Value = mKol
            row.Cells("TakhfifKala").Value = takhfif

        Finally
            _isCalculating = False
        End Try
    End Sub


    Private Sub GridEXSatr_DeletingRecord(sender As Object, e As Janus.Windows.GridEX.RowActionCancelEventArgs) _
    Handles GridEXSatr.DeletingRecord

        ' همیشه Cancel می‌کنیم؛ حذف و رفرش را خودمان انجام می‌دهیم
        e.Cancel = True

        Dim row As Janus.Windows.GridEX.GridEXRow = GridEXSatr.CurrentRow
        If row Is Nothing OrElse row.RowType <> Janus.Windows.GridEX.RowType.Record Then Exit Sub

        Dim id As Object = row.Cells("ccPishFaktorSatr").Value
        If id Is Nothing OrElse IsDBNull(id) Then Exit Sub

        If MessageBox.Show("این ردیف و ردیف‌های وابسته به آن حذف می‌شوند. ادامه می‌دهید؟",
                       "حذف", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                       MessageBoxDefaultButton.Button2,
                       MessageBoxOptions.RightAlign Or MessageBoxOptions.RtlReading) <> DialogResult.Yes Then
            Exit Sub
        End If

        Try
            Using con As New SqlConnection(ConnectionString)
                Using cmd As SqlCommand = con.CreateCommand()
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.CommandText = "[Sales].[spPishFaktor_DeleteSatr]"
                    cmd.Parameters.AddWithValue("@ccPishFaktorSatr", id)
                    con.Open()
                    cmd.ExecuteNonQuery()
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        ' چون ممکنه چند ردیف حذف شده باشه، لیست را دوباره لود کن
        BeginInvoke(New MethodInvoker(AddressOf ReloadSatr))
    End Sub

    Private Sub txtSearchNumber_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles txtSearchNumber.KeyDown
        If e.KeyCode = Keys.Space Then
            e.Handled = True
            e.SuppressKeyPress = True

            Dim frm As New frmFO_MarjoeeSearch()
            If frm.ShowDialog() = DialogResult.OK AndAlso frm.SelectedId > 0 Then
                ShowMarjoee(frm.SelectedId)
                Mode = UD_Dll.Enums.GL_ModeForms.UpdateRecord
            Else
                Mode = UD_Dll.Enums.GL_ModeForms.AddNewRecord
            End If

        ElseIf e.KeyCode <> Keys.Tab AndAlso e.KeyCode <> Keys.ShiftKey Then
            ' هر کلید دیگه‌ای (به‌جز Tab برای رفتن به کنترل بعدی) بلاک بشه
            e.Handled = True
            e.SuppressKeyPress = True
        End If
    End Sub
    Private Sub txtSearchNumber_KeyPress(ByVal sender As Object, ByVal e As KeyPressEventArgs) Handles txtSearchNumber.KeyPress
        e.Handled = True
    End Sub
    Private Sub ShowMarjoee(ByVal id As Integer)
        ccElamMarjoee = id

        If Not LoadTitr(id) Then
            ccElamMarjoee = 0
            Exit Sub
        End If

        'txtSearchNumber.Text = id.ToString()

        ' هدر در حالت نمایش قفل شود
        mskTarikh.Enabled = False
        txtCodeMoshtary.Enabled = False
        cmbMamorPakhsh.Enabled = False
        cmbBazaryab.Enabled = False
        txtShomarehFaktor.Enabled = False
        txtCodeMoshtary.Enabled = False
        cmbBazaryab.Enabled = False
        TxtSharh.Enabled = False
        btnSaveSanad.Enabled = False

        SearchSatr()

        ' برای نمایش، گرید فعال باشد تا حذف/مشاهده ممکن باشد
        GridEXSatr.Enabled = True
        ' بعد از اتمام رویداد KeyDown و بسته شدن فرم جستجو، فوکوس بره روی ردیف جدید
        BeginInvoke(New MethodInvoker(AddressOf FocusNewRow))
    End Sub
    Private Sub FocusNewRow()
        GridEXSatr.Focus()
        GridEXSatr.MoveToNewRecord()
        GridEXSatr.Col = GridEXSatr.RootTable.Columns("CodeKala").Position
    End Sub

    Private Sub NewPishFaktor()
        ' ۱) ریست وضعیت
        ccElamMarjoee = 0
        ccMoshtaryAddress = 0
        _gridStructureReady = False        ' ساختار گرید قبلاً ساخته شده؛ دست نمی‌زنیم

        ' ۲) هدر: فعال کردن کنترل‌ها
        mskTarikh.Enabled = False
        txtCodeMoshtary.Enabled = True
        'cmbAddress.Enabled = True
        cmbBazaryab.Enabled = True

        TxtSharh.Enabled = True
        btnSaveSanad.Enabled = True

        ' ۳) هدر: خالی کردن مقدارها
        mskTarikh.Text = TarikhEmrooz
        txtCodeMoshtary.Text = ""
        txtCodeMoshtary.Tag = Nothing
        lblNameMoshtary.Text = ""          ' کنترل نام مشتری؛ اسمش را جایگزین کنید

        txtSearchNumber.Text = ""


        cmbBazaryab.SelectedIndex = -1
        cmbBazaryab.SelectedIndex = -1
        flg = False

        flg = True


        ' ۴) گرید: خالی و غیرفعال
        dt_SearchSatr = New DataTable
        GridEXSatr.SetDataBinding(Nothing, "")
        GridEXSatr.Enabled = False
        chkMalyat.Checked = True
        chkMalyat.Enabled = False
        ' ۵) فوکوس روی اولین فیلد
        txtCodeMoshtary.Focus()
    End Sub

    Private Sub btnNew_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnNew.Click
        NewPishFaktor()
    End Sub
    Protected Overrides Function ProcessCmdKey(ByRef msg As Message, keyData As Keys) As Boolean
        If keyData = Keys.Enter AndAlso GridEXSatr.Enabled AndAlso GridEXSatr.ContainsFocus Then
            GoNextCol()
            Return True
        End If
        Return MyBase.ProcessCmdKey(msg, keyData)
    End Function
    Private Sub GridEXSatr_EnterKey(sender As Object, e As KeyEventArgs) Handles GridEXSatr.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.Handled = True
            e.SuppressKeyPress = True
            GoNextCol()
        End If
    End Sub
    Private Sub GoNextCol()
        Dim g As Janus.Windows.GridEX.GridEX = GridEXSatr
        If g.CurrentRow Is Nothing Then Exit Sub

        Dim isNewRow As Boolean = (g.CurrentRow.RowType = Janus.Windows.GridEX.RowType.NewRecord)

        Dim cols As New List(Of Janus.Windows.GridEX.GridEXColumn)
        For Each c As Janus.Windows.GridEX.GridEXColumn In g.RootTable.Columns
            If c.Visible AndAlso c.EditType <> Janus.Windows.GridEX.EditType.NoEdit Then
                If isNewRow OrElse c.Key <> "CodeKala" Then cols.Add(c)
            End If
        Next
        If cols.Count = 0 Then Exit Sub
        cols.Sort(Function(a, b) a.Position.CompareTo(b.Position))

        Dim idx As Integer = -1
        If g.CurrentColumn IsNot Nothing Then idx = cols.IndexOf(g.CurrentColumn)

        If idx = -1 Then
            g.CurrentColumn = cols(0)
        ElseIf idx < cols.Count - 1 Then
            g.CurrentColumn = cols(idx + 1)
        Else
            g.UpdateData()          ' ستون آخر: ذخیره‌ی سطر
        End If
    End Sub
    ' ردیف را با کالای انتخاب‌شده پر می‌کند؛ هم F2 و هم تایپ از این استفاده می‌کنند
    Private Sub FillRowWithKala(ByVal row As Janus.Windows.GridEX.GridEXRow,
        ByVal codeKala As Object, ByVal ccKala As Object, ByVal nameKala As Object)

        Dim info As New ElamMarjoeeTitrInfo()
        If Not info.Load(ccElamMarjoee, ConnectionString) Then Exit Sub

        _isCalculating = True
        Try
            row.Cells("CodeKala").Value = codeKala
            row.Cells("ccKala").Value = ccKala
            row.Cells("NameKala").Value = nameKala
            'row.Cells("Fee").Value = ObjCode.GetMablaghForosh_NoePardakht(ccKala, info.TarikhElamMarjoee, CodeMahalFaal, info.ccMoshtary, info.sNoePardakht)
            'GetMojodyGhabelForosh(ccKala, info.ccAnbar, MojodiGhabelForoshKOL)
            'row.Cells("MojodiDarHaleForosh").Value = MojodiGhabelForoshKOL
        Finally
            _isCalculating = False
        End Try
    End Sub
    ' ccKala را با کد تایپ‌شده برمی‌گرداند؛ اگر نبود 0
    Private Function FindCcKala(ByVal code As String) As Integer
        Dim c As String = code.Trim()
        If Not IsNumeric(c) Then Return 0      ' جلوی خطای SQL و تزریق را می‌گیرد

        Dim v As Object = objTools.ConvertNulls(
        objTools.DLookup("ccKala", "tblAn_Kala", "Faal = 1 AND CodeKala = " & c), 0)
        Return Convert.ToInt32(v)
    End Function
    Private Sub txtShomarehFaktor_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtShomarehFaktor.KeyPress
        Try
            If (Asc(e.KeyChar()) < 48 Or Asc(e.KeyChar()) > 57) And (Asc(e.KeyChar()) <> 8) Then
                e.Handled = True
            End If
            If e.KeyChar = Chr(Keys.Space) Then
                Dim objFaktor As New Forms_dll.FaktorSearch
                Dim StrSql As String


                StrSql = "SELECT FaktorShomareh, b.ccForoshandeh, a.ccMoshtary, a.CodeDoreh, ccFaktorTitr, b.PishFaktorShomareh AS ShomarehPishFaktor,"
                StrSql &= " c.NameMoshtary, d.FName + ' ' + d.LName AS NameMamorPakhsh, g.Sharh AS txtNoePardakht,"
                StrSql &= " f.FName + ' ' + f.LName AS NameForoshandeh, dbo.SetDateSlash(FaktorTarikh) AS FaktorTarikhSlash"
                StrSql &= " FROM tblFO_Faktor AS a WITH(NOLOCK) LEFT OUTER JOIN"
                StrSql &= " tblFO_PishFaktor AS b WITH(NOLOCK) ON a.ccPishFaktor = b.ccPishFaktorTitr LEFT OUTER JOIN"
                StrSql &= " tblFO_Moshtary AS c WITH(NOLOCK) ON a.ccMoshtary = c.ccMoshtary LEFT OUTER JOIN"
                StrSql &= " tblGL_MoshakhasatFardi AS d WITH(NOLOCK) ON a.ccMamorPakhsh = d.CodeFard LEFT OUTER JOIN"
                StrSql &= " tblFO_Foroshandeh AS e WITH(NOLOCK) ON b.ccForoshandeh = e.ccForoshandeh LEFT OUTER JOIN"
                StrSql &= " tblGL_MoshakhasatFardi AS f WITH(NOLOCK) ON e.CodeFard = f.CodeFard LEFT OUTER JOIN"
                StrSql &= " tblGL_ShenasehOmomi AS g WITH(NOLOCK) ON a.sNoePardakht = g.Code"
                StrSql &= " WHERE a.sVazeiat = 4261 "
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

                If objFaktor.tFaktorccFaktor <> 0 Then
                    txtShomarehFaktor.Text = objFaktor.tFaktorShomareh
                    ccFaktorTitr = objFaktor.tFaktorccFaktor
                    cmbBazaryab.SelectedValue = objFaktor.tFaktorccForoshandeh
                    ccMoshtary = objFaktor.tFaktorccMoshtary
                    flg = True
                    txtCodeMoshtary.Tag = objFaktor.tFaktorccMoshtary
                    Me.lblNameMoshtary.Text = objTools.ConvertNulls(objTools.DLookup("NameMoshtary", "TblFO_Moshtary", "ccMoshtary = " & objFaktor.tFaktorccMoshtary), "")
                    txtCodeMoshtary.Text = objTools.ConvertNulls(objTools.DLookup("CodeMoshtary", "TblFO_Moshtary", "ccMoshtary = " & objFaktor.tFaktorccMoshtary), "")
                    flg = False

                    txtCodeMoshtary.Enabled = False
                    cmbBazaryab.Enabled = False

                Else

                    txtCodeMoshtary.Enabled = True
                    cmbBazaryab.Enabled = True
                End If


            End If
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->txtCodeMoshtary_KeyPress")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->txtCodeMoshtary_KeyPress")
        End Try
    End Sub
End Class

