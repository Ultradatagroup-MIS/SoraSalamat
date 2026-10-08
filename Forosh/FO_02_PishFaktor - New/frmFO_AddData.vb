


Imports System.Data
Imports System.Data.SqlClient
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
    Dim ccPishfaktorTitr As Integer = 0
    Dim ccMoshtaryAddress As Integer = 0
    Dim CodeFard As Integer = 0
    Dim AllowChangeFeePishFaktor As Boolean
    Dim MeghdarAdadi As Integer
    Dim flg As Boolean = False
#End Region


    Public Class PishFaktorTitrInfo

#Region "Fields"
        Private _ccPishFaktorTitr As Integer
        Private _PishFaktorTarikh As String = ""
        Private _ccMoshtary As Integer
        Private _CodeMoshtary As String = ""
        Private _NameMoshtary As String = ""
        Private _ccMoshtaryAddress As Integer
        Private _ccForoshandeh As Integer
        Private _sNoePardakht As Integer
        Private _ModatCheck As Integer
        Private _Tozihat As String = ""
        Private _ccAnbar As Integer
        Private _dtSearchTitr As DataTable
#End Region

#Region "Properties"
        Public ReadOnly Property ccPishFaktorTitr() As Integer
            Get
                Return _ccPishFaktorTitr
            End Get
        End Property

        Public ReadOnly Property PishFaktorTarikh() As String
            Get
                Return _PishFaktorTarikh
            End Get
        End Property

        Public ReadOnly Property ccMoshtary() As Integer
            Get
                Return _ccMoshtary
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

        Public ReadOnly Property ccMoshtaryAddress() As Integer
            Get
                Return _ccMoshtaryAddress
            End Get
        End Property

        Public ReadOnly Property ccForoshandeh() As Integer
            Get
                Return _ccForoshandeh
            End Get
        End Property

        Public ReadOnly Property sNoePardakht() As Integer
            Get
                Return _sNoePardakht
            End Get
        End Property

        Public ReadOnly Property ModatCheck() As Integer
            Get
                Return _ModatCheck
            End Get
        End Property

        Public ReadOnly Property Tozihat() As String
            Get
                Return _Tozihat
            End Get
        End Property
        Public ReadOnly Property ccAnbar As Integer
            Get
                Return _ccAnbar
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
                    cm.CommandText = "[Sales].[spPishFaktorVorodKoli_SearchTitr]"
                    cm.CommandTimeout = 999999
                    cm.Parameters.AddWithValue("@ccPishFaktorTitr", id)

                    Dim da As New SqlDataAdapter(cm)
                    da.Fill(_dtSearchTitr)
                End Using
            End Using

            If _dtSearchTitr.Rows.Count = 0 Then Return False

            Dim r As DataRow = _dtSearchTitr.Rows(0)
            _ccPishFaktorTitr = id
            _PishFaktorTarikh = GetStr(r, "PishFaktorTarikh")
            _ccMoshtary = GetInt(r, "ccMoshtary")
            _CodeMoshtary = GetStr(r, "CodeMoshtary")
            _NameMoshtary = GetStr(r, "NameMoshtary")
            _ccMoshtaryAddress = GetInt(r, "ccMoshtaryAddress")
            _ccForoshandeh = GetInt(r, "ccForoshandeh")
            _sNoePardakht = GetInt(r, "sNoePardakht")
            _ModatCheck = GetInt(r, "ModatCheck")
            _Tozihat = GetStr(r, "Tozihat")
            _ccAnbar = GetInt(r, "ccAnbar")

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
#End Region

    End Class
    Private Sub frmFO_AddData_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        mskTarikh.Text = TarikhEmrooz
        flg = False
        LoadCombo()
        ClearForm()
        flg = True
        AllowChangeFeePishFaktor = objTools.ConvertNulls(objTools.DLookup("AllowChangeFeePishFaktor", "tblGL_SysConfig", "CodeMahal = " & CodeMahalFaal), False)
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
            txtCodeMoshtaryS.Text = ""
            txtCodeMoshtaryS.Tag = ""
            txtShomarehS.Text = ""

            txtModatCheck.Text = ""

            cmbBazaryab.SelectedIndex = -1
            cmbBazaryab.SelectedIndex = -1
            txtCodeMoshtary.Text = ""
            txtCodeMoshtary.Tag = ""
            lblNameMoshtary.Text = ""
            txtTozihat.Text = ""





            cmbAddress.SelectedIndex = -1
            cmbAddress.SelectedIndex = -1
            cmbAddress.DataSource = Nothing
            cmbAddress.Items.Clear()


            ObjCode.UserName = UserName
            cmbNoePardakht.SelectedIndex = -1


            'lblGorohForosh.Visible = False
            'cmbGorohForosh.Visible = False
            'lblAnbar.Visible = False
            'cmbAnbar.Visible = False

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
            Dim cn As New SqlConnection
            Dim cm As New SqlCommand
            Dim p As New SqlParameter
            Dim d As DataRow





            Strsql = "Global.spNoePardakht_LoadCombo"
            cn.ConnectionString = ConnectionString
            cn.Open()
            cm = New SqlCommand(Strsql, cn)
            cm.CommandType = CommandType.StoredProcedure
            cm.Parameters.Clear()

            daSQL = New SqlDataAdapter(cm)
            daSQL.Fill(dsForm, "tblNoePardakht")


            cmbNoePardakht.DataSource = Nothing
            cmbNoePardakht.Items.Clear()
            cmbNoePardakht.DataSource = dsForm.Tables("tblNoePardakht").DefaultView
            cmbNoePardakht.DisplayMember = "Sharh"
            cmbNoePardakht.ValueMember = "Code"

            cm = Nothing

            Strsql = "Global.spForoshandeh_LoadCombo "

            cm = New SqlCommand(Strsql, cn)
            cm.CommandType = CommandType.StoredProcedure
            cm.Parameters.Clear()

            p = New SqlParameter("CodeMahal", SqlDbType.Int)
            p.Value = CodeMahalFaal
            cm.Parameters.Add(p)

            p = New SqlParameter("sVazeiat", SqlDbType.Int)
            p.Value = UD_Dll.Enums.FO_VaziatForoshandeh.NoFaal
            cm.Parameters.Add(p)

            p = New SqlParameter("UserName", SqlDbType.NVarChar, 20)
            p.Value = UserName
            cm.Parameters.Add(p)

            daSQL = New SqlDataAdapter(cm)
            daSQL.Fill(dsForm, "tblForoshandeh")
            cmbBazaryab.DataSource = Nothing
            cmbBazaryab.Items.Clear()
            cmbBazaryab.DataSource = dsForm.Tables("tblForoshandeh").DefaultView
            cmbBazaryab.DisplayMember = "LN"
            cmbBazaryab.ValueMember = "ccForoshandeh"

            cm = Nothing


            daSQL.Fill(dsForm, "tblForoshandehS")
            d = dsForm.Tables("tblForoshandehS").NewRow
            d("NameForoshandeh") = "همه"
            d("ccForoshandeh") = 0
            dsForm.Tables("tblForoshandehS").Rows.Add(d)
            cmbBazaryabS.DataSource = Nothing
            cmbBazaryabS.Items.Clear()
            cmbBazaryabS.DataSource = dsForm.Tables("tblForoshandehS").DefaultView
            cmbBazaryabS.DisplayMember = "NameForoshandeh"
            cmbBazaryabS.ValueMember = "ccForoshandeh"
            cmbBazaryabS.SelectedValue = 0

            cm = Nothing


            If dsForm.Tables.Contains("tblAnbar") Then
                dsForm.Tables.Remove("tblAnbar")
            End If

            Strsql = "Global.spAnbar_LoadCombo"

            cn = New SqlConnection(ConnectionString)
            cn.Open()

            cm = New SqlCommand(Strsql, cn)
            cm.CommandType = CommandType.StoredProcedure
            cm.Parameters.Clear()

            cm.Parameters.AddWithValue("CodeMahal", CodeMahalFaal)

            daSQL = New SqlDataAdapter(cm)
            daSQL.Fill(dsForm, "tblAnbar")

            cmbAnbar.DataSource = Nothing
            cmbAnbar.Items.Clear()
            cmbAnbar.DataSource = dsForm.Tables("tblAnbar").DefaultView
            cmbAnbar.DisplayMember = "NameAnbar"
            cmbAnbar.ValueMember = "CodeAnbar"

            cm = Nothing
            daSQL = Nothing
            cn.Close()

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->LoadCombo")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->LoadCombo")
        End Try
    End Sub
    Private Sub txtCodeMoshtary_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCodeMoshtary.TextChanged
        'If Mode = UD_Dll.Enums.GL_ModeForms.UpdateRecord Then Exit Sub


        Dim Criteria As String = ""

        Dim Bazaryab As Integer = 0
        Bazaryab = IIf(IsNothing(cmbBazaryab.SelectedValue), 0, cmbBazaryab.SelectedValue)

        If objTools.ConvertNulls(objTools.DLookup("ccMoshtary", "tblFO_Moshtary", "sVazeiat = 3980 AND CodeMahal = " & CodeMahalFaal & " AND CodeMoshtary = " & IIf(txtCodeMoshtary.Text.Trim = "", 0, Val(txtCodeMoshtary.Text.Trim))), 0) <> 0 Then

            Criteria = "CodeMahal=" & CodeMahalFaal
            Criteria &= " And IsMoshtaryBadHesab = 0 AND Not exists (Select * From tblGL_SecurityData where namekarbar= '" & UserName & "' and CodeSubSystem = 614 and pk = qryFO_Moshtary.ccMoshtary) "
            Criteria &= " And CodeMoshtary = '" & IIf(IsNothing(Me.txtCodeMoshtary.Text), 0, Me.txtCodeMoshtary.Text) & "'"
            Criteria &= " AND sVazeiat = " & UD_Dll.Enums.FO_VaziatMoshtary.Faal

            If Bazaryab <> 0 Then
                Criteria &= " AND ccMoshtary IN (SELECT ccMoshtary FROM tblFO_ForoshandehMoshtary WHERE  ccForoshandeh = " & Bazaryab & ") "
            End If
            Me.lblNameMoshtary.Text = objTools.ConvertNulls(objTools.DLookup("NameMoshtary", "qryFO_Moshtary", Criteria), "")
            Me.txtCodeMoshtary.Tag = objTools.ConvertNulls(objTools.DLookup("ccMoshtary", "qryFO_Moshtary", Criteria), "")





            If lblNameMoshtary.Text.Trim <> "" Then
                LoadMoshtaryAddress()

                If objTools.DLookup("sNoePardakht", "tblFO_Moshtary", "ccMoshtary = " & Me.txtCodeMoshtary.Tag) = 0 Then

                    cmbNoePardakht.SelectedValue = 3953
                Else
                    cmbNoePardakht.SelectedValue = objTools.DLookup("sNoePardakht", "tblFO_Moshtary", "ccMoshtary = " & Me.txtCodeMoshtary.Tag)

                End If
                If Bazaryab = 0 Then
                    cmbBazaryab.SelectedValue = objTools.DLookupOne("ccForoshandeh", "tblFO_ForoshandehMoshtary", "ccMoshtary = " & Me.txtCodeMoshtary.Tag, " ccForoshandehMoshtary desc")
                End If

            Else
                If dsForm.Tables.Contains("tblAddress") Then
                    dsForm.Tables.Remove("tblAddress")
                End If
                cmbAddress.DataSource = Nothing
                cmbAddress.Items.Clear()

                If dsForm.Tables.Contains("tblMoshtaryAfrad") Then
                    dsForm.Tables.Remove("tblMoshtaryAfrad")
                End If

            End If
            'If lblNameMoshtary.Text.Trim <> "" Then
            '    lblTabloMoshtary.Text = objTools.DLookup("NameTablo", "tblFO_Moshtary", "ccMoshtary = " & Me.txtCodeMoshtary.Tag)
            '    lblTellMoshtary.Text = objTools.DLookup("Telephone", "tblFO_MoshtaryAddress", "ccMoshtary = " & Me.txtCodeMoshtary.Tag)
            '    lblNoeMoshtary.Text = objTools.DLookup("txtNoeMoshtary", "qryFO_Moshtary", "ccMoshtary = " & Me.txtCodeMoshtary.Tag)
            'Else
            '    lblTabloMoshtary.Text = ""
            '    lblTellMoshtary.Text = ""
            '    lblNoeMoshtary.Text = ""
            'End If




        End If



    End Sub
    Private Sub txtCodeMoshtary_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCodeMoshtary.KeyPress
        Try
            If (Asc(e.KeyChar()) < 48 Or Asc(e.KeyChar()) > 57) And (Asc(e.KeyChar()) <> 8) Then
                e.Handled = True
            End If
            Dim Bazaryab As Integer = 0
            Bazaryab = IIf(IsNothing(cmbBazaryab.SelectedValue), 0, cmbBazaryab.SelectedValue)


            If e.KeyChar = Chr(Keys.Space) Then
                Dim objMoshtary As New Forms_dll.frmFO_MoshtarySearch
                Dim StrSql As String = ""

                StrSql = "Select * from qryFO_Moshtary Where CodeMahal=" & CodeMahalFaal & " And IsMoshtaryBadHesab = 0 AND sVazeiat = " & UD_Dll.Enums.FO_VaziatMoshtary.Faal
                StrSql &= "AND Not exists (Select * From tblGL_SecurityData where namekarbar= '" & UserName & "' and CodeSubSystem = 614 and pk = qryFO_Moshtary.ccMoshtary) "
                StrSql &= " AND Not exists (Select * From tblGL_SecurityData where namekarbar= '" & UserName & "' and CodeSubSystem = 10000 and pk = qryFO_Moshtary.sNoeMoshtary) "
                If Bazaryab <> 0 Then
                    StrSql &= " AND ccMoshtary IN (SELECT ccMoshtary FROM tblFO_ForoshandehMoshtary WHERE  ccForoshandeh = " & Bazaryab & ") "
                End If
                StrSql &= " order by sMantagheh,sMahaleh,NameMoshtary"




                tCodeMoshtary = ""
                tNameMoshtary = ""
                tccMoshtary = ""
                cmbAddress.SelectedIndex = -1
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

                If txtCodeMoshtary.Tag <> 0 Then
                    LoadMoshtaryAddress()
                Else
                    cmbAddress.SelectedIndex = -1
                End If








                MultiSelection = False
            End If

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->txtCodeMoshtary_KeyPress")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->txtCodeMoshtary_KeyPress")
        End Try
    End Sub
    Private Sub LoadMoshtaryAddress()
        Dim Strsql As String
        Dim daSQL As SqlDataAdapter
        Dim cn As New SqlConnection
        Dim cm As New SqlCommand
        Dim p As New SqlParameter

        Strsql = "Sales.spPishFaktor_LoadMoshtaryAddress"

        cn.ConnectionString = ConnectionString
        cn.Open()
        cm = New SqlCommand(Strsql, cn)
        cm.CommandType = CommandType.StoredProcedure
        cm.Parameters.Clear()

        p = New SqlParameter("ccMoshtary", SqlDbType.Int)
        p.Value = IIf(txtCodeMoshtary.Tag.ToString.Length = 0, 0, txtCodeMoshtary.Tag)
        cm.Parameters.Add(p)

        daSQL = New SqlDataAdapter(cm)
        If dsForm.Tables.Contains("tblAddress") Then
            dsForm.Tables.Remove("tblAddress")
        End If
        daSQL.Fill(dsForm, "tblAddress")
        cmbAddress.DataSource = Nothing
        cmbAddress.Items.Clear()
        cmbAddress.DataSource = dsForm.Tables("tblAddress").DefaultView
        cmbAddress.DisplayMember = "AddressKamel"
        cmbAddress.ValueMember = "ccMoshtaryAddress"

        cm.Connection.Close()
        cn.Close()

        daSQL = Nothing
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
            GridEXSatr.CurrentTable.Columns.Item("CodeKala").Width = 60
            GridEXSatr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXSatr.CurrentTable.Columns.Item("CodeKala").Position = 2
            GridEXSatr.CurrentTable.Columns.Item("CodeKala").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXSatr.CurrentTable.Columns.Item("NameKala").Caption = "نام کالا"
            GridEXSatr.CurrentTable.Columns.Item("NameKala").Visible = True
            GridEXSatr.CurrentTable.Columns.Item("NameKala").Width = 230
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



            If AllowChangeFeePishFaktor Then
                GridEXSatr.CurrentTable.Columns.Item("Fee").Caption = "فی"
                GridEXSatr.CurrentTable.Columns.Item("Fee").Visible = True
                GridEXSatr.CurrentTable.Columns.Item("Fee").Width = 80
                GridEXSatr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
                'GridEXSatr.CurrentTable.Columns.Item("Fee").EditType = Janus.Windows.GridEX.EditType.NoEdit
                GridEXSatr.CurrentTable.Columns.Item("Fee").Position = 5
                GridEXSatr.CurrentTable.Columns.Item("Fee").FormatMode = Janus.Windows.GridEX.FormatMode.UseIFormattable
                GridEXSatr.CurrentTable.Columns.Item("Fee").FormatString = "###,###"
                GridEXSatr.CurrentTable.Columns.Item("Fee").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            Else
                GridEXSatr.CurrentTable.Columns.Item("Fee").Caption = "فی"
                GridEXSatr.CurrentTable.Columns.Item("Fee").Visible = True
                GridEXSatr.CurrentTable.Columns.Item("Fee").Width = 80
                GridEXSatr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
                GridEXSatr.CurrentTable.Columns.Item("Fee").EditType = Janus.Windows.GridEX.EditType.NoEdit
                GridEXSatr.CurrentTable.Columns.Item("Fee").Position = 5
                GridEXSatr.CurrentTable.Columns.Item("Fee").FormatMode = Janus.Windows.GridEX.FormatMode.UseIFormattable
                GridEXSatr.CurrentTable.Columns.Item("Fee").FormatString = "###,###"
                GridEXSatr.CurrentTable.Columns.Item("Fee").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            End If



            GridEXSatr.CurrentTable.Columns.Item("MojodiDarHaleForosh").Caption = "موجودی"
            GridEXSatr.CurrentTable.Columns.Item("MojodiDarHaleForosh").Visible = True
            GridEXSatr.CurrentTable.Columns.Item("MojodiDarHaleForosh").Width = 60
            GridEXSatr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXSatr.CurrentTable.Columns.Item("MojodiDarHaleForosh").EditType = Janus.Windows.GridEX.EditType.NoEdit
            GridEXSatr.CurrentTable.Columns.Item("MojodiDarHaleForosh").Position = 6
            GridEXSatr.CurrentTable.Columns.Item("MojodiDarHaleForosh").FormatString = "N"
            GridEXSatr.CurrentTable.Columns.Item("MojodiDarHaleForosh").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center






            GridEXSatr.CurrentTable.Columns.Item("MKol3").Caption = "جمع مبلغ"
            GridEXSatr.CurrentTable.Columns.Item("MKol3").Visible = True
            GridEXSatr.CurrentTable.Columns.Item("MKol3").Width = 100
            GridEXSatr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXSatr.CurrentTable.Columns.Item("MKol3").EditType = Janus.Windows.GridEX.EditType.NoEdit
            GridEXSatr.CurrentTable.Columns.Item("MKol3").Position = 7
            GridEXSatr.CurrentTable.Columns.Item("MKol3").FormatString = "###,###"
            GridEXSatr.CurrentTable.Columns.Item("MKol3").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXSatr.CurrentTable.Columns.Item("DarsadTakhfif").Caption = "درصد تخفیف"
            GridEXSatr.CurrentTable.Columns.Item("DarsadTakhfif").Visible = True
            GridEXSatr.CurrentTable.Columns.Item("DarsadTakhfif").Width = 100
            GridEXSatr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            'GridEXSatr.CurrentTable.Columns.Item("DarsadTakhfif").EditType = Janus.Windows.GridEX.EditType.NoEdit
            GridEXSatr.CurrentTable.Columns.Item("DarsadTakhfif").Position = 8
            GridEXSatr.CurrentTable.Columns.Item("DarsadTakhfif").FormatString = "N"

            GridEXSatr.CurrentTable.Columns.Item("DarsadTakhfif").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXSatr.CurrentTable.Columns.Item("TakhfifKala").Caption = "تخفیف کالا"
            GridEXSatr.CurrentTable.Columns.Item("TakhfifKala").Visible = True
            GridEXSatr.CurrentTable.Columns.Item("TakhfifKala").Width = 120
            GridEXSatr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXSatr.CurrentTable.Columns.Item("TakhfifKala").EditType = Janus.Windows.GridEX.EditType.NoEdit
            GridEXSatr.CurrentTable.Columns.Item("TakhfifKala").Position = 9
            GridEXSatr.CurrentTable.Columns.Item("TakhfifKala").FormatString = "###,###"
            GridEXSatr.CurrentTable.Columns.Item("TakhfifKala").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center


            GridEXSatr.CurrentTable.Columns.Item("MablaghMalyat").Caption = "مالیات"
            GridEXSatr.CurrentTable.Columns.Item("MablaghMalyat").Visible = True
            GridEXSatr.CurrentTable.Columns.Item("MablaghMalyat").Width = 80
            GridEXSatr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXSatr.CurrentTable.Columns.Item("MablaghMalyat").EditType = Janus.Windows.GridEX.EditType.NoEdit
            GridEXSatr.CurrentTable.Columns.Item("MablaghMalyat").Position = 10
            GridEXSatr.CurrentTable.Columns.Item("MablaghMalyat").FormatString = "###,###"
            GridEXSatr.CurrentTable.Columns.Item("MablaghMalyat").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center


            GridEXSatr.CurrentTable.Columns.Item("FeeKol").Caption = "مبلغ نهایی"
            GridEXSatr.CurrentTable.Columns.Item("FeeKol").Visible = True
            GridEXSatr.CurrentTable.Columns.Item("FeeKol").Width = 100
            GridEXSatr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXSatr.CurrentTable.Columns.Item("FeeKol").EditType = Janus.Windows.GridEX.EditType.NoEdit
            GridEXSatr.CurrentTable.Columns.Item("FeeKol").Position = 11
            GridEXSatr.CurrentTable.Columns.Item("FeeKol").FormatString = "###,###"
            GridEXSatr.CurrentTable.Columns.Item("FeeKol").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            ' tblPishFaktorSatr.ShomarehBach , 
            'tblPishFaktorSatr.TarikhTolid , tblPishFaktorSatr.TarikhENgheza
            GridEXSatr.CurrentTable.Columns.Item("ShomarehBach").Caption = "شماره بچ"
            GridEXSatr.CurrentTable.Columns.Item("ShomarehBach").Visible = True
            GridEXSatr.CurrentTable.Columns.Item("ShomarehBach").Width = 80
            GridEXSatr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXSatr.CurrentTable.Columns.Item("ShomarehBach").EditType = Janus.Windows.GridEX.EditType.NoEdit
            GridEXSatr.CurrentTable.Columns.Item("ShomarehBach").Position = 12
            'GridEXSatr.CurrentTable.Columns.Item("ShomarehBach").FormatString = "###,###"
            GridEXSatr.CurrentTable.Columns.Item("ShomarehBach").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXSatr.CurrentTable.Columns.Item("TarikhTolid").Caption = "تاریخ تولید"
            GridEXSatr.CurrentTable.Columns.Item("TarikhTolid").Visible = True
            GridEXSatr.CurrentTable.Columns.Item("TarikhTolid").Width = 80
            GridEXSatr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXSatr.CurrentTable.Columns.Item("TarikhTolid").EditType = Janus.Windows.GridEX.EditType.NoEdit
            GridEXSatr.CurrentTable.Columns.Item("TarikhTolid").Position = 13
            GridEXSatr.CurrentTable.Columns.Item("TarikhTolid").FormatString = "###,###"
            GridEXSatr.CurrentTable.Columns.Item("TarikhTolid").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXSatr.CurrentTable.Columns.Item("TarikhENgheza").Caption = "تاریخ انقضا"
            GridEXSatr.CurrentTable.Columns.Item("TarikhENgheza").Visible = True
            GridEXSatr.CurrentTable.Columns.Item("TarikhENgheza").Width = 80
            GridEXSatr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXSatr.CurrentTable.Columns.Item("TarikhENgheza").EditType = Janus.Windows.GridEX.EditType.NoEdit
            GridEXSatr.CurrentTable.Columns.Item("TarikhENgheza").Position = 14
            GridEXSatr.CurrentTable.Columns.Item("TarikhENgheza").FormatString = "###,###"
            GridEXSatr.CurrentTable.Columns.Item("TarikhENgheza").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXSatr.CurrentTable.Columns.Item("ccKala").Caption = "ccKala"
            GridEXSatr.CurrentTable.Columns.Item("ccKala").Visible = False
            GridEXSatr.CurrentTable.Columns.Item("ccKala").Width = 0
            GridEXSatr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOn
            GridEXSatr.CurrentTable.Columns.Item("ccKala").Position = 15
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
        If ccPishfaktorTitr <> 0 Then Exit Sub        ' جلوگیری از Insert دوباره‌ی هدر
        If Not ValidateTitr() Then Exit Sub      ' قبل از ذخیره‌ی هدر
        AddNewRecord()
        If ccPishfaktorTitr = 0 Then Exit Sub


        Dim info As New PishFaktorTitrInfo()
        If Not info.Load(ccPishfaktorTitr, ConnectionString) Then
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
        Dim info As New PishFaktorTitrInfo()

        Try
            If Not info.Load(id, ConnectionString) Then
                MessageBox.Show("اطلاعات پیش‌فاکتور ذخیره‌شده پیدا نشد")
                Return False
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Return False
        End Try

        ccMoshtaryAddress = info.ccMoshtaryAddress

        mskTarikh.Text = info.PishFaktorTarikh
        txtCodeMoshtary.Text = info.CodeMoshtary
        txtCodeMoshtary.Tag = info.ccMoshtary

        cmbBazaryab.SelectedValue = info.ccForoshandeh
        cmbNoePardakht.SelectedValue = info.sNoePardakht
        txtModatCheck.Text = info.ModatCheck.ToString()
        txtTozihat.Text = info.Tozihat

        Return True
    End Function
    Private Function IsComboEmpty(ByVal cmb As ComboBox) As Boolean
        If cmb.SelectedIndex < 0 Then Return True
        Dim v As Object = cmb.SelectedValue
        If v Is Nothing OrElse IsDBNull(v) Then Return True
        Return False
    End Function

    Private Function ValidateTitr() As Boolean

        ' تاریخ
        Dim tarikh As String = mskTarikh.Text.Replace("/", "").Replace(" ", "").Trim()
        If tarikh.Length = 0 Then
            MessageBox.Show("تاریخ را وارد کنید")
            mskTarikh.Focus()
            Return False
        End If

        ' مشتری
        If txtCodeMoshtary.Tag Is Nothing OrElse IsDBNull(txtCodeMoshtary.Tag) _
       OrElse Val(txtCodeMoshtary.Tag.ToString()) <= 0 Then
            MessageBox.Show("مشتری را انتخاب کنید")
            txtCodeMoshtary.Focus()
            Return False
        End If

        ' آدرس مشتری
        If IsComboEmpty(cmbAddress) Then
            MessageBox.Show("آدرس مشتری را انتخاب کنید")
            cmbAddress.Focus()
            Return False
        End If

        ' فروشنده
        If IsComboEmpty(cmbBazaryab) Then
            MessageBox.Show("فروشنده را انتخاب کنید")
            cmbBazaryab.Focus()
            Return False
        End If

        ' نحوه پرداخت
        If IsComboEmpty(cmbNoePardakht) Then
            MessageBox.Show("نحوه پرداخت را انتخاب کنید")
            cmbNoePardakht.Focus()
            Return False
        End If

        If txtModatCheck.Visible Then
            If Val(txtModatCheck.Text) <= 0 Then
                MessageBox.Show("مدت چک را وارد کنید")
                txtModatCheck.Focus()
                Return False
            End If
        End If

        ' انبار
        If IsComboEmpty(cmbAnbar) Then
            MessageBox.Show("انبار را انتخاب کنید")
            cmbAnbar.Focus()
            Return False
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
                cm.CommandText = "[Sales].[spPishFaktorVorodKoli_InsertTitr]"
                cm.Parameters.AddWithValue("CodeMahal", CodeMahalFaal)
                cm.Parameters.AddWithValue("CodeDoreh", CodeDoreh)
                cm.Parameters.AddWithValue("PishFaktorTarikh", mskTarikh.Text)
                cm.Parameters.AddWithValue("ccMoshtary", txtCodeMoshtary.Tag)
                cm.Parameters.AddWithValue("ccMoshtaryAddress", cmbAddress.SelectedValue)
                cm.Parameters.AddWithValue("ccForoshandeh", cmbBazaryab.SelectedValue)
                cm.Parameters.AddWithValue("CodeFard", objTools.ConvertNulls(objTools.DLookup("CodeFard", "tblFO_Foroshandeh", "ccForoshandeh = " & cmbBazaryab.SelectedValue), 0))
                cm.Parameters.AddWithValue("sNoePardakht", cmbNoePardakht.SelectedValue)
                cm.Parameters.AddWithValue("ModatCheck", Val(txtModatCheck.Text))
                cm.Parameters.AddWithValue("Malyat", chkMalyat.Checked)
                cm.Parameters.AddWithValue("PishFaktorAmani", 0)
                cm.Parameters.AddWithValue("PishFaktorGheireGhateei", 0)
                cm.Parameters.AddWithValue("Tozihat", txtTozihat.Text.TrimEnd)
                cm.Parameters.AddWithValue("NoeVorod", 1)
                cm.Parameters.AddWithValue("ccAnbar", cmbAnbar.SelectedValue)
                cm.Parameters.AddWithValue("UserName", UserName)

                Dim pOut As SqlParameter = cm.Parameters.Add("@ccPishFaktorTitr", SqlDbType.Int)
                pOut.Direction = ParameterDirection.Output
                cm.ExecuteNonQuery()

                If pOut.Value IsNot DBNull.Value Then
                    ccPishfaktorTitr = Convert.ToInt32(pOut.Value)
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
                cm.CommandText = "[Sales].[spPishFaktorVorodKoli_SearchSatr]"
                cm.Parameters.AddWithValue("ccPishFaktorTitr", ccPishfaktorTitr)
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


            Dim info As New PishFaktorTitrInfo()
            info.Load(ccPishfaktorTitr, ConnectionString)


            If GridEXSatr.CurrentColumn Is Nothing Then Exit Sub

            If GridEXSatr.CurrentColumn.Key = "CodeKala" Then

                Dim objKala As New Forms_dll.frmAN_KalaSearch
                Dim StrSqlKala As String



                StrSqlKala = "Select  CodeKala,NameKala,ccKala,txtsVahedeShomaresh,sVahedeShomaresh,NameBrand,RadifBrand,0 as IsSabadKala "

                StrSqlKala &= " from qryAN_Kala"
                StrSqlKala &= " Where Faal = 1  AND ccKala in (Select ccKala from tblAN_KalaGheymat where ccKala =qryAN_Kala.ccKala )  "




                MultiSelection = False
                SearchItem = "CodeKala"
                objKala.SetForm(StrSqlKala)
                objKala.ShowDialog()

                GridEXSatr.CurrentRow.Cells("CodeKala").Value = objKala.tcodeKala
                GridEXSatr.CurrentRow.Cells("ccKala").Value = objKala.tccKala
                GridEXSatr.CurrentRow.Cells("NameKala").Value = objKala.tNameKala

                GridEXSatr.CurrentRow.Cells("Fee").Value = ObjCode.GetMablaghForosh_NoePardakht(objKala.tccKala, info.PishFaktorTarikh, CodeMahalFaal, info.ccMoshtary, info.sNoePardakht)



                GetMojodyGhabelForosh(objKala.tccKala, info.ccAnbar, MojodiGhabelForoshKOL)

                GridEXSatr.CurrentRow.Cells("MojodiDarHaleForosh").Value = MojodiGhabelForoshKOL
                MultiSelection = False


            End If

        End If


    End Sub





    Private Sub GridEXSatr_AddingRecord(sender As Object, e As System.ComponentModel.CancelEventArgs) _
    Handles GridEXSatr.AddingRecord
        If Not SaveRow(GridEXSatr.CurrentRow, True) Then e.Cancel = True
        'GridEXSatr.Col = GridEXSatr.RootTable.Columns("CodeKala").Position
    End Sub

    Private Sub GridEXSatr_UpdatingRecord(sender As Object, e As System.ComponentModel.CancelEventArgs) _
    Handles GridEXSatr.UpdatingRecord
        'If Not SaveRow(GridEXSatr.CurrentRow, False) Then e.Cancel = True
        e.Cancel = True   ' ویرایش نداریم
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
    Private Sub GridEXSatr_EditingCell(sender As Object, e As Janus.Windows.GridEX.EditingCellEventArgs) _
    Handles GridEXSatr.EditingCell
        ' فقط ردیف جدید قابل ویرایشه
        If GridEXSatr.CurrentRow IsNot Nothing AndAlso
       GridEXSatr.CurrentRow.RowType <> Janus.Windows.GridEX.RowType.NewRecord Then
            e.Cancel = True
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
                    cmd.CommandText = "[Sales].[spPishFaktorVorodKoli_InsertSatr]"
                    cmd.Parameters.AddWithValue("@ccPishFaktorTitr", ccPishfaktorTitr)
                    cmd.Parameters.AddWithValue("@ccKala", row.Cells("ccKala").Value)
                    cmd.Parameters.AddWithValue("@Tedad3", If(row.Cells("Tedad3").Value, 0))
                    cmd.Parameters.AddWithValue("@Fee", If(row.Cells("Fee").Value, 0))
                    cmd.Parameters.AddWithValue("@DarsadTakhfif", IIf(row.Cells("DarsadTakhfif").Value Is DBNull.Value, 0, row.Cells("DarsadTakhfif").Value))

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




    ' مقدار سلول را به عدد تبدیل می‌کند؛ اگر خالی یا غیرعددی بود False برمی‌گرداند
    Private Function TryGetNumber(ByVal row As Janus.Windows.GridEX.GridEXRow,
                                  ByVal colKey As String, ByRef result As Double) As Boolean
        result = 0
        Dim v As Object = row.Cells(colKey).Value
        If v Is Nothing OrElse IsDBNull(v) Then Return False
        If v.ToString().Trim().Length = 0 Then Return False
        Return Double.TryParse(v.ToString().Trim(), result)
    End Function

    Private Function ValidateRow(ByVal row As Janus.Windows.GridEX.GridEXRow) As Boolean
        Dim num As Double


        Dim info As New PishFaktorTitrInfo()
        info.Load(ccPishfaktorTitr, ConnectionString)


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

        GetMojodyGhabelForosh(ccKala, info.ccAnbar, MojodiGhabelForoshKOL)
        ' موجودی قابل فروش: باید بزرگ‌تر از صفر باشد
        'If Not TryGetNumber(row, "MojodiDarHaleForosh", num) OrElse num <= 0 Then
        If MojodiGhabelForoshKOL <= 0 Then

            MessageBox.Show("موجودی قابل فروش این کالا صفر است")
            FocusCol("Tedad3")
            Return False
        End If
        Dim tedad As Double
        TryGetNumber(row, "Tedad3", tedad)
        'TryGetNumber(row, "MojodiDarHaleForosh", mojodi)
        If tedad > MojodiGhabelForoshKOL Then
            MessageBox.Show("تعداد از موجودی قابل فروش بیشتر است")
            FocusCol("Tedad3")
            Return False
        End If


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
    Private Sub GridEXSatr_CellUpdated(ByVal sender As Object,
        ByVal e As Janus.Windows.GridEX.ColumnActionEventArgs) _
        Handles GridEXSatr.CellUpdated

        If _isCalculating Then Exit Sub
        If e.Column Is Nothing Then Exit Sub

        Dim key As String = e.Column.Key
        If key = "Tedad3" OrElse key = "Fee" OrElse key = "DarsadTakhfif" Then
            CalcRow(GridEXSatr.CurrentRow)
        End If
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

    Private Sub cmbNoePardakht_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbNoePardakht.SelectedIndexChanged
        If flg Then
            MeghdarAdadi = ObjCode.GetMeghdarAdadi(cmbNoePardakht.SelectedValue)

            If MeghdarAdadi < 4 Then

                txtModatCheck.Visible = True
                lblModatCheck.Visible = True


                txtModatCheck.Text = objTools.ConvertNulls(objTools.DLookup("ModateChek", "tblFO_Moshtary", "ccMoshtary = " & txtCodeMoshtary.Tag), 0)

            Else
                txtModatCheck.Visible = False
                txtModatCheck.Text = ""
                lblModatCheck.Visible = False


            End If
        End If
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
End Class