Public Class frmFO_PeygiriFaktor

#Region "Variable AND Constant Declration"
    Const cntCodeSubSystem As Long = 1000104
    Dim Mode As UD_Dll.Enums.GL_ModeForms = UD_Dll.Enums.GL_ModeForms.None
    Dim ModeSatr As UD_Dll.Enums.GL_ModeForms = UD_Dll.Enums.GL_ModeForms.None
    Const GridOrgSize As Long = 199
    Const GridSize As Long = 169
    Private SN As Integer
    Public dsForm As New DataSet
    Dim dvForm As DataView
    Dim txtCaption As String
    Dim Fset As Boolean = True
    Private WithEvents BS As New UD_Dll.PassString
    Public dvTitr As DataView
    Public dvSatr As DataView
    Public cmTitr, cmSatr As CurrencyManager
    Dim PK As Integer
    Dim strMantaghehS As String = ""
    Dim strMahalehS As String = ""
    Public str_ccFaktor As String = ""
    Dim ErrPro As New ErrorProvider
#End Region
#Region "Form Event Code"
    Private Sub frmFO_PeygiriFaktor_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetParameter()
        SN = objSec.GetSecurityNumber(cntCodeSubSystem, UserName)
        mskAzTarikh.Text = TarikhEmrooz
        mskTaTarikh.Text = TarikhEmrooz
        mskTarikh.Text = TarikhEmrooz
        LoadCombo()
        cmbMamorPakhshS.SelectedValue = 0
        cmbVazeiat.SelectedIndex = 1
        cmbVazeiat.SelectedIndex = 1
        SetForm()
        SetFormSatr()

        Search()
        objTools.ChangeKbd(UD_Dll.mdlUtility.KBD.Farsi)

    End Sub
    Private Sub GridEXSatr_CellEdited(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles GridEXSatr.CellEdited
        If GridEXSatr.CurrentRow.RowType = Janus.Windows.GridEX.RowType.Record Then
            UpdateTozihat_Peygiri(Val(GridEXSatr.CurrentRow.Cells("ccPeygiriFaktorSatr").Text.Replace(",", "")))
        End If
    End Sub
    Private Sub cmbVazeiat_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbVazeiat.SelectedIndexChanged
        Search()
    End Sub
    Private Sub GridEXSatr_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles GridEXSatr.KeyPress
        If e.KeyChar = Chr(Keys.Escape) Then
            If GridEXSatr.CurrentRow.RowType = Janus.Windows.GridEX.RowType.Record Then
                RefreshSatrData()
            End If
        End If
    End Sub
    Private Sub txtShomarehFaktor_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtShomarehFaktor.KeyPress
        Try
            If (Asc(e.KeyChar()) < 48 Or Asc(e.KeyChar()) > 57) And (Asc(e.KeyChar()) <> 8) Then
                e.Handled = True
            End If
            If e.KeyChar = Chr(Keys.Space) Then
                If cmTitr.Position = -1 Then Exit Sub

                ' 2000 FO_PishFaktorTaeedModir
                'If UserName.ToUpper <> "ADMINISTRATOR" Then
                '    If (Not ObjCode.CheckPermission(2000)) Then Exit Sub
                'End If

                If objTools.DLookup("Vazeiat", "Sales.PeygiriFaktor", "ccPeygiriFaktor = " & Val(GridEXTitr.CurrentRow.Cells("ccPeygiriFaktor").Text.Replace(",", ""))) <> 1 Then
                    MsgBox("شماره پیگیری انتخاب شده از حالت بدون وضعیت خارج شده است . نمی توانید فاکتور جدید به آن اضافه نمایید . .", MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " خـطا")
                    Exit Sub
                End If

                Dim frm As New frmFO_SearchFaktor
                str_ccFaktor = ""

                Me.Hide()
                frm.ShowDialog()
                Me.Show()
            End If
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->txtCodeMoshtary_KeyPress")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->txtCodeMoshtary_KeyPress")
        End Try
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

            strSQL = "Global.spMamorPakhsh_LoadCombo "

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            cmSQL.Parameters.AddWithValue("CodeMahal", CodeMahalFaal)
            cmSQL.Parameters.AddWithValue("sSemat", "," & UD_Dll.Enums.GL_Semat.MamorPakhsh & "," & UD_Dll.Enums.GL_Semat.Ranandeh & "," & UD_Dll.Enums.GL_Semat.Foroshandeh_Sayar & "," & UD_Dll.Enums.GL_Semat.Foroshandeh & "," & 4843 & ",") '-- 4843 تحصیل دار

            daSQL = New SqlDataAdapter(cmSQL)
            daSQL.Fill(dsForm, "tbl_MamorPakhshS")
            dr = dsForm.Tables("tbl_MamorPakhshS").NewRow
            dr("FN") = "همه"
            dr("CodeFard") = 0
            dsForm.Tables("tbl_MamorPakhshS").Rows.Add(dr)
            cmbMamorPakhshS.DataSource = Nothing
            cmbMamorPakhshS.Items.Clear()
            cmbMamorPakhshS.DataSource = dsForm.Tables("tbl_MamorPakhshS").DefaultView
            cmbMamorPakhshS.DisplayMember = "FN"
            cmbMamorPakhshS.ValueMember = "CodeFard"

            ' --------------------------------

            daSQL.Fill(dsForm, "tbl_MamorPakhsh")
            dr = dsForm.Tables("tbl_MamorPakhsh").NewRow

            cmbMamorPakhsh.DataSource = Nothing
            cmbMamorPakhsh.Items.Clear()
            cmbMamorPakhsh.DataSource = dsForm.Tables("tbl_MamorPakhsh").DefaultView
            cmbMamorPakhsh.DisplayMember = "FN"
            cmbMamorPakhsh.ValueMember = "CodeFard"

            cmSQL = Nothing : daSQL = Nothing
            cnSQL.Close()

            ' --------------------------------

            cmbVazeiat.Items.Add("همـــه")
            cmbVazeiat.Items.Add("بدون وضعیت")
            cmbVazeiat.Items.Add("ارسال شده")
            cmbVazeiat.Items.Add("تایید شده")

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> LoadCombo")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> LoadCombo")
        End Try
    End Sub
    Private Sub SetForm()
        If Mode = UD_Dll.Enums.GL_ModeForms.None Then
            GridEXTitr.Height = GridOrgSize
            btnNewSanad.Visible = True
            btnDeleteSanad.Visible = True
            btnSaveSanad.Visible = False
            btnCancel.Visible = False

            btnNewSanad.Enabled = True
            btnDeleteSanad.Enabled = True
            btnNewSatr.Enabled = True
            btnRemoveFaktor.Enabled = True
            btnAddFaktor.Enabled = True
            btnReport.Enabled = True
            btnPrint.Enabled = True
            btnSearch.Enabled = True
            cmbMamorPakhshS.Enabled = True
            cmbVazeiat.Enabled = True
        ElseIf Mode = UD_Dll.Enums.GL_ModeForms.AddNewRecord Then
            GridEXTitr.Height = GridSize
            btnNewSanad.Visible = False
            btnDeleteSanad.Visible = False
            btnSaveSanad.Visible = True
            btnCancel.Visible = True

            btnNewSatr.Enabled = False
            btnRemoveFaktor.Enabled = False
            btnAddFaktor.Enabled = False
            btnReport.Enabled = False
            btnPrint.Enabled = False
            btnSearch.Enabled = False
            cmbMamorPakhshS.Enabled = False
            cmbVazeiat.Enabled = False
        ElseIf UD_Dll.Enums.GL_ModeForms.UpdateRecord Then
            GridEXTitr.Height = GridSize
            btnNewSanad.Visible = False
            btnDeleteSanad.Visible = False
            btnSaveSanad.Visible = True
            btnCancel.Visible = True
            cmbMamorPakhsh.SelectedValue = Val(GridEXTitr.CurrentRow.Cells("CodeFard_MamorPakhsh").Text.Replace(",", ""))
            mskTarikh.Text = GridEXTitr.CurrentRow.Cells("TarikhPeygiri").Text.Replace("/", "")

            btnNewSatr.Enabled = False
            btnRemoveFaktor.Enabled = False
            btnAddFaktor.Enabled = False
            btnReport.Enabled = False
            btnPrint.Enabled = False
            btnSearch.Enabled = False
            cmbMamorPakhshS.Enabled = False
            cmbVazeiat.Enabled = False
        End If
    End Sub
    Private Sub SetFormSatr()
        If ModeSatr = UD_Dll.Enums.GL_ModeForms.None Then
            GridEXSatr.Height = GridOrgSize
            btnNewSatr.Visible = True
            btnRemoveFaktor.Visible = True
            btnSaveSatr.Visible = False
            btnCancelSatr.Visible = False
            btnAddFaktor.Visible = True
            txtShomarehFaktor.Text = ""
            str_ccFaktor = ""

            btnNewSanad.Enabled = True
            btnDeleteSanad.Enabled = True
            btnNewSatr.Enabled = True
            btnRemoveFaktor.Enabled = True
            btnAddFaktor.Enabled = True
            btnReport.Enabled = True
            btnPrint.Enabled = True
            btnSearch.Enabled = True
            cmbMamorPakhshS.Enabled = True
            cmbVazeiat.Enabled = True
        ElseIf ModeSatr = UD_Dll.Enums.GL_ModeForms.AddNewRow Then
            GridEXSatr.Height = GridSize
            btnNewSatr.Visible = False
            btnRemoveFaktor.Visible = False
            btnSaveSatr.Visible = True
            btnCancelSatr.Visible = True
            btnAddFaktor.Visible = False

            btnNewSanad.Enabled = False
            btnDeleteSanad.Enabled = False
            btnAddFaktor.Enabled = True
            btnReport.Enabled = True
            btnPrint.Enabled = True
            btnSearch.Enabled = True
            cmbMamorPakhshS.Enabled = True
            cmbVazeiat.Enabled = True
        End If
    End Sub
    Public Sub Search()
        Try
            Dim strSQL As String

            If Not IsValid("All") Then
                Exit Sub
            End If

            strSQL = "Sales.spPeygiriFaktor_SearchTitr "

            RefreshTitrdata(strSQL)

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> Search ")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> Search ")
        End Try
    End Sub
    Private Sub RefreshTitrData(ByVal strSQL As String)
        Dim daSQL As SqlDataAdapter
        Dim cnSQL As New SqlConnection
        Dim cmSQL As New SqlCommand

        Try

            If dsForm.Tables.Contains("HETitr") Then
                dsForm.Tables.Remove("HETitr")
            End If

            cnSQL.ConnectionString = ConnectionString
            cnSQL.Open()

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            cmSQL.Parameters.AddWithValue("CodeMahal", CodeMahalFaal)
            cmSQL.Parameters.AddWithValue("CodeDoreh", CodeDoreh)
            cmSQL.Parameters.AddWithValue("CodeFard_MamorPakhsh", cmbMamorPakhshS.SelectedValue)
            cmSQL.Parameters.AddWithValue("AzTarikh", mskAzTarikh.Text)
            cmSQL.Parameters.AddWithValue("Tatarikh", mskTaTarikh.Text)
            cmSQL.Parameters.AddWithValue("Vazeiat", cmbVazeiat.SelectedIndex)
            cmSQL.Parameters.AddWithValue("ShomarehPeygiri", IIf(mskShomarehPeygiri.Text.Trim = "", 0, Val(mskShomarehPeygiri.Text.Trim)))

            daSQL = New SqlDataAdapter(cmSQL)
            daSQL.Fill(dsForm, "HETitr")

            dvForm = New DataView
            dvForm = dsForm.Tables("HETitr").DefaultView
            dvForm.Sort = "TarikhPeygiri Desc"
            dvForm.AllowDelete = True
            dvForm.AllowEdit = False
            dvForm.AllowNew = False
            daSQL = Nothing


            Dim col As DataColumn
            '------------Adding Columns------------
            col = New DataColumn
            col.ColumnName = "Taeed"
            col.DataType = GetType(Boolean)
            col.DefaultValue = False
            dsForm.Tables("HETitr").Columns.Add(col)
            '-----------------------------------------

            dvTitr = New DataView(dsForm.Tables("HETitr"), "", "TarikhPeygiri DESC", DataViewRowState.CurrentRows)
            dvTitr.AllowNew = False
            dvTitr.AllowDelete = False
            dvTitr.AllowEdit = True

            cnSQL.Close()
            daSQL = Nothing

            GridEXTitr.DataSource = Nothing
            GridEXTitr.DataSource = dvTitr

            SetGridStyle()
            BoundCurrencyManagerTitr(cmSQL)

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->RefreshTitrdata")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->RefreshTitrdata")
        Finally
            cmSQL.Dispose()
            cnSQL.Dispose()
        End Try
    End Sub
    Private Sub SetGridStyle()
        Try
            With GridEXTitr
                .DataSource = Nothing
                .DataSource = dsForm.Tables("HETitr").DefaultView
                .SetDataBinding(dsForm.Tables("HETitr").DefaultView, "")
                .RetrieveStructure()
            End With

            For i As Integer = 0 To GridEXTitr.CurrentTable.Columns.Count - 1
                GridEXTitr.CurrentTable.Columns.Item(i).Visible = False
            Next

            GridEXTitr.CurrentTable.Columns.Item("Taeed").Caption = "انتخاب"
            GridEXTitr.CurrentTable.Columns.Item("Taeed").Visible = True
            GridEXTitr.CurrentTable.Columns.Item("Taeed").Width = 20
            GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXTitr.CurrentTable.Columns.Item("Taeed").Position = 0
            GridEXTitr.CurrentTable.Columns.Item("Taeed").Selectable = True
            GridEXTitr.CurrentTable.Columns.Item("Taeed").ActAsSelector = True
            GridEXTitr.CurrentTable.Columns.Item("Taeed").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXTitr.CurrentTable.Columns.Item("ShomarehPeygiri").Caption = "شماره پیگیری"
            GridEXTitr.CurrentTable.Columns.Item("ShomarehPeygiri").Visible = True
            GridEXTitr.CurrentTable.Columns.Item("ShomarehPeygiri").Width = 100
            GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXTitr.CurrentTable.Columns.Item("ShomarehPeygiri").Position = 1
            GridEXTitr.CurrentTable.Columns.Item("ShomarehPeygiri").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXTitr.CurrentTable.Columns.Item("TarikhPeygiri").Caption = "تاريخ"
            GridEXTitr.CurrentTable.Columns.Item("TarikhPeygiri").Visible = True
            GridEXTitr.CurrentTable.Columns.Item("TarikhPeygiri").Width = 80
            GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXTitr.CurrentTable.Columns.Item("TarikhPeygiri").Position = 2
            GridEXTitr.CurrentTable.Columns.Item("TarikhPeygiri").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXTitr.CurrentTable.Columns.Item("NameMamorPakhsh").Caption = "نام مامور پخش"
            GridEXTitr.CurrentTable.Columns.Item("NameMamorPakhsh").Visible = True
            GridEXTitr.CurrentTable.Columns.Item("NameMamorPakhsh").Width = 160
            GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXTitr.CurrentTable.Columns.Item("NameMamorPakhsh").Position = 3
            GridEXTitr.CurrentTable.Columns.Item("NameMamorPakhsh").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXTitr.CurrentTable.Columns.Item("TedadFaktor").Caption = "تعداد فاکتور"
            GridEXTitr.CurrentTable.Columns.Item("TedadFaktor").Visible = True
            GridEXTitr.CurrentTable.Columns.Item("TedadFaktor").Width = 80
            GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXTitr.CurrentTable.Columns.Item("TedadFaktor").Position = 4
            GridEXTitr.CurrentTable.Columns.Item("TedadFaktor").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXTitr.CurrentTable.Columns.Item("TedadMoshtary").Caption = "تعداد مشتری"
            GridEXTitr.CurrentTable.Columns.Item("TedadMoshtary").Visible = True
            GridEXTitr.CurrentTable.Columns.Item("TedadMoshtary").Width = 85
            GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXTitr.CurrentTable.Columns.Item("TedadMoshtary").Position = 5
            GridEXTitr.CurrentTable.Columns.Item("TedadMoshtary").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXTitr.CurrentTable.Columns.Item("JamMablaghFaktor").Caption = "جمع فاکتورها"
            GridEXTitr.CurrentTable.Columns.Item("JamMablaghFaktor").Visible = True
            GridEXTitr.CurrentTable.Columns.Item("JamMablaghFaktor").Width = 100
            GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXTitr.CurrentTable.Columns.Item("JamMablaghFaktor").Position = 6
            GridEXTitr.CurrentTable.Columns.Item("JamMablaghFaktor").FormatMode = Janus.Windows.GridEX.FormatMode.UseIFormattable
            GridEXTitr.CurrentTable.Columns.Item("JamMablaghFaktor").FormatString = "###,###.##"
            GridEXTitr.CurrentTable.Columns.Item("JamMablaghFaktor").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXTitr.CurrentTable.Columns.Item("JamMablaghDaryafti").Caption = "جمع پرداختی ها"
            GridEXTitr.CurrentTable.Columns.Item("JamMablaghDaryafti").Visible = True
            GridEXTitr.CurrentTable.Columns.Item("JamMablaghDaryafti").Width = 110
            GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXTitr.CurrentTable.Columns.Item("JamMablaghDaryafti").Position = 7
            GridEXTitr.CurrentTable.Columns.Item("JamMablaghDaryafti").FormatString = "N"
            GridEXTitr.CurrentTable.Columns.Item("JamMablaghDaryafti").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXTitr.CurrentTable.Columns.Item("JamMablaghMandeh").Caption = "مانـــده"
            GridEXTitr.CurrentTable.Columns.Item("JamMablaghMandeh").Visible = True
            GridEXTitr.CurrentTable.Columns.Item("JamMablaghMandeh").Width = 100
            GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXTitr.CurrentTable.Columns.Item("JamMablaghMandeh").Position = 8
            GridEXTitr.CurrentTable.Columns.Item("JamMablaghMandeh").FormatString = "N"
            GridEXTitr.CurrentTable.Columns.Item("JamMablaghMandeh").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXTitr.CurrentTable.Columns.Item("txtVazeiat").Caption = "وضعیت"
            GridEXTitr.CurrentTable.Columns.Item("txtVazeiat").Visible = True
            GridEXTitr.CurrentTable.Columns.Item("txtVazeiat").Width = 100
            GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXTitr.CurrentTable.Columns.Item("txtVazeiat").Position = 9
            GridEXTitr.CurrentTable.Columns.Item("txtVazeiat").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXTitr.CurrentTable.Columns.Item("CodeFard_MamorPakhsh").Caption = "CodeFard_MamorPakhsh"
            GridEXTitr.CurrentTable.Columns.Item("CodeFard_MamorPakhsh").Visible = False
            GridEXTitr.CurrentTable.Columns.Item("CodeFard_MamorPakhsh").Width = 0
            GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXTitr.CurrentTable.Columns.Item("CodeFard_MamorPakhsh").Position = 10
            GridEXTitr.CurrentTable.Columns.Item("CodeFard_MamorPakhsh").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXTitr.CurrentTable.Columns.Item("ccPeygiriFaktor").Caption = "ccPeygiriFaktor"
            GridEXTitr.CurrentTable.Columns.Item("ccPeygiriFaktor").Visible = False
            GridEXTitr.CurrentTable.Columns.Item("ccPeygiriFaktor").Width = 0
            GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXTitr.CurrentTable.Columns.Item("ccPeygiriFaktor").Position = 11
            GridEXTitr.CurrentTable.Columns.Item("ccPeygiriFaktor").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXTitr.CurrentTable.Columns.Item("Vazeiat").Caption = "Vazeiat"
            GridEXTitr.CurrentTable.Columns.Item("Vazeiat").Visible = False
            GridEXTitr.CurrentTable.Columns.Item("Vazeiat").Width = 0
            GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXTitr.CurrentTable.Columns.Item("Vazeiat").Position = 12
            GridEXTitr.CurrentTable.Columns.Item("Vazeiat").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

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
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->SetGridStyle")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->SetGridStyle")
        End Try

    End Sub
    Private Sub BoundCurrencyManagerTitr(ByVal cm As SqlCommand)
        Try
            cmTitr = CType(BindingContext(GridEXTitr.DataSource), CurrencyManager)
            'AddHandler cmTitr.ItemChanged, AddressOf cmTitr_ItemChanged
            AddHandler cmTitr.PositionChanged, AddressOf cmTitr_PositionChanged

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->BoundCurrencyManagerTitr")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->BoundCurrencyManagerTitr")
        End Try
    End Sub
    Private Sub cmTitr_PositionChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim cm As New SqlCommand
        Try

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->cmTitr_PositionChanged")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->cmTitr_PositionChanged")
        End Try

    End Sub
    Private Function IsValid(ByVal chkField As String) As Boolean
        IsValid = False

        Try
            If Mode = UD_Dll.Enums.GL_ModeForms.AddNewRecord Or Mode = UD_Dll.Enums.GL_ModeForms.UpdateRecord Then
                If chkField = "mskTarikh" Or chkField = "All" Then
                    If Len(mskTarikh.Text.ToString) <> 0 Then
                        If Not objTarikh.IsShDate(mskTarikh.Text.ToString) Then
                            mskTarikh.Focus()
                            Exit Function
                        End If
                    Else
                        ErrPro.SetError(Me.mskTarikh, "تاریخ را وارد کنید.")
                        MsgBox("تاریخ را وارد کنید.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
                        mskTarikh.Focus()
                        Exit Function
                    End If
                    ErrPro.SetError(Me.mskTarikh, "")
                End If
            End If

            If Mode = UD_Dll.Enums.GL_ModeForms.None Then

                If chkField = "mskAzTarikh" Or chkField = "All" Then
                    If Len(mskAzTarikh.Text.ToString) <> 0 Then
                        If Not objTarikh.IsShDate(mskAzTarikh.Text.ToString) Then
                            mskAzTarikh.Focus()
                            Exit Function
                        End If
                    Else
                        ErrPro.SetError(Me.mskAzTarikh, "از تاریخ را وارد کنید.")
                        MsgBox("از تاریخ را وارد کنید.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
                        mskAzTarikh.Focus()
                        Exit Function
                    End If
                    ErrPro.SetError(Me.mskAzTarikh, "")
                End If

                If chkField = "mskTaTarikh" Or chkField = "All" Then
                    If Len(mskTaTarikh.Text.ToString) <> 0 Then
                        If Not objTarikh.IsShDate(mskTaTarikh.Text.ToString) Then
                            mskTaTarikh.Focus()
                            Exit Function
                        End If
                    Else
                        ErrPro.SetError(Me.mskTaTarikh, "تا تاریخ را وارد کنید.")
                        MsgBox("تا تاریخ را وارد کنید.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
                        mskTaTarikh.Focus()
                        Exit Function
                    End If
                    ErrPro.SetError(Me.mskTaTarikh, "")
                End If
            End If

            Return True
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> IsValid ")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> IsValid ")
        End Try
    End Function
    Private Function IsValidSatr(ByVal chkField As String) As Boolean
        IsValidSatr = False

        Try
            If chkField = "txtShomarehFaktor" Or chkField = "All" Then
                If txtShomarehFaktor.Text.Trim = "" Then
                    ErrPro.SetError(Me.txtShomarehFaktor, "لطفاَ شماره فاکتور وارد نمایید .")
                    MsgBox("لطفاَ شماره فاکتور وارد نمایید .", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
                    txtShomarehFaktor.Focus()
                    Exit Function
                End If
                ErrPro.SetError(Me.txtShomarehFaktor, "")
            End If

            Return True
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> IsValidSatr ")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> IsValidSatr ")
        End Try
    End Function
    Private Function AddNewSanad() As Boolean
        Try
            AddNewSanad = False

            Dim cnSQL As New SqlConnection
            Dim cmSQL As New SqlCommand
            Dim strSQL As String = ""

            If Not IsValid("All") Then
                Exit Function
            End If

            strSQL = "Sales.spPeygiriFaktor_InsertTitr "

            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            cmSQL.Parameters.AddWithValue("CodeFard_MamorPakhsh", cmbMamorPakhsh.SelectedValue)
            cmSQL.Parameters.AddWithValue("CodeMahal", CodeMahalFaal)
            cmSQL.Parameters.AddWithValue("CodeDoreh", CodeDoreh)
            cmSQL.Parameters.AddWithValue("TarikhPeygiri", mskTarikh.Text)

            cmSQL.ExecuteNonQuery()

            cmSQL = Nothing
            cnSQL.Close()

            AddNewSanad = True

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> AddNewSanad ")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> AddNewSanad ")
        End Try
    End Function
    Private Function AddNewRow() As Boolean
        Try
            AddNewRow = False

            Dim cnSQL As New SqlConnection
            Dim cmSQL As New SqlCommand
            Dim strSQL As String = ""

            If Not IsValidSatr("All") Then
                Exit Function
            End If

            strSQL = "Sales.spPeygiriFaktor_InsertSatr "

            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            cmSQL.Parameters.AddWithValue("ccPeygiriFaktor", Val(GridEXTitr.CurrentRow.Cells("ccPeygiriFaktor").Text.Replace(",", "")))
            cmSQL.Parameters.AddWithValue("str_ccFaktor", str_ccFaktor)

            cmSQL.ExecuteNonQuery()

            cmSQL = Nothing
            cnSQL.Close()

            AddNewRow = True

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> AddNewRow ")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> AddNewRow ")
        End Try
    End Function
    Private Function UpdateSanad() As Boolean
        Try
            UpdateSanad = False

            Dim cnSQL As New SqlConnection
            Dim cmSQL As New SqlCommand
            Dim strSQL As String = ""

            If Not IsValid("All") Then
                Exit Function
            End If

            strSQL = "Sales.spPeygiriFaktor_UpdateTitr "

            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            cmSQL.Parameters.AddWithValue("CodeFard_MamorPakhsh", cmbMamorPakhsh.SelectedValue)
            cmSQL.Parameters.AddWithValue("TarikhPeygiri", mskTarikh.Text)
            cmSQL.Parameters.AddWithValue("ccPeygiriFaktor", Val(GridEXTitr.CurrentRow.Cells("ccPeygiriFaktor").Text.Replace(",", "")))

            cmSQL.ExecuteNonQuery()

            cmSQL = Nothing
            cnSQL.Close()

            UpdateSanad = True

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> UpdateSanad ")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> UpdateSanad ")
        End Try
    End Function
    Private Sub Delete(ByVal ccPeygiriFaktor As Double, ByVal ShomarehPeygiri As Double)
        Try

            If objTools.DLookup("Vazeiat", "Sales.PeygiriFaktor", "ccPeygiriFaktor = " & ccPeygiriFaktor) <> 1 Then
                MsgBox("شماره پیگیری " & ShomarehPeygiri & " از حالت بدون وضعیت خارج شده است . امکان حذف آن وجود ندارد .", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " پیام")
                Exit Sub
            End If

            If objTools.DCount("ccPeygiriFaktor", "Sales.PeygiriFaktorSatr", "ccPeygiriFaktor = " & ccPeygiriFaktor) <> 0 Then
                If MsgBox("شمــاره پیگیری " & ShomarehPeygiri & " دارای فاکتور است . آیا عملیات حذف این پیگیری صورت پذیرد ؟ .", MsgBoxStyle.Question + MsgBoxStyle.YesNo + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " پیام") = MsgBoxResult.Yes Then
                    DeleteSanad(ccPeygiriFaktor, True)
                Else
                    Exit Sub
                End If
            Else
                DeleteSanad(ccPeygiriFaktor, False)
            End If

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> Delete ")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> Delete ")
        End Try
    End Sub
    Private Sub DeleteSanad(ByVal ccPeygiriFaktor As Double, ByVal HaveFaktor As Boolean)
        Try
            Dim cnSQL As New SqlConnection
            Dim cmSQL As New SqlCommand
            Dim strSQL As String = ""

            strSQL = "Sales.spPeygiriFaktor_DeleteTitr "

            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            cmSQL.Parameters.AddWithValue("ccPeygiriFaktor", ccPeygiriFaktor)
            cmSQL.Parameters.AddWithValue("HaveFaktor", HaveFaktor)

            cmSQL.ExecuteNonQuery()

            cmSQL = Nothing
            cnSQL.Close()

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> DeleteSanad ")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> DeleteSanad ")
        End Try
    End Sub
    Private Sub RefreshSatrData()
        Try
            If dvTitr.Count = 0 Then
                Exit Sub
            End If

            Dim cnSQL As New SqlConnection
            Dim cmSQL As New SqlCommand
            Dim daSQL As SqlDataAdapter
            Dim strSQL As String

            strSQL = "Sales.spPeygiriFaktor_SearchSatr "

            If dsForm.Tables.Contains("HESatr") Then
                dsForm.Tables.Remove("HESatr")
            End If

            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            cmSQL.Parameters.AddWithValue("ccPeygiriFaktor", Val(GridEXTitr.CurrentRow.Cells("ccPeygiriFaktor").Text.Replace(",", "")))

            daSQL = New SqlDataAdapter(cmSQL)
            daSQL.Fill(dsForm, "HESatr")
            dvForm = New DataView
            dvForm = dsForm.Tables("HESatr").DefaultView
            dvForm.Sort = "CodeMoshtary ASC"
            dvForm.AllowDelete = True
            dvForm.AllowEdit = False
            dvForm.AllowNew = False
            daSQL = Nothing

            Dim col As DataColumn
            '------------Adding Columns------------
            col = New DataColumn
            col.ColumnName = "Taeed"
            col.DataType = GetType(Boolean)
            col.DefaultValue = False
            dsForm.Tables("HESatr").Columns.Add(col)
            '-----------------------------------------

            dvSatr = New DataView(dsForm.Tables("HESatr")) ', "", "CodeMoshtary ASC", DataViewRowState.OriginalRows)
            dvSatr.RowFilter = " "
            dvSatr.AllowNew = False
            dvSatr.AllowDelete = True
            dvSatr.AllowEdit = True

            cnSQL.Close()
            daSQL = Nothing

            GridEXSatr.DataSource = Nothing
            GridEXSatr.DataSource = dvSatr

            SetGridStyleSatr()
            BoundCurrencyManagerSatr()

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->RefreshSatrData")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->RefreshSatrData")
        End Try

    End Sub
    Private Sub SetGridStyleSatr()
        Try
            With GridEXSatr
                .DataSource = Nothing
                .DataSource = dsForm.Tables("HESatr").DefaultView
                .SetDataBinding(dsForm.Tables("HESatr").DefaultView, "")
                .RetrieveStructure()
            End With

            For i As Integer = 0 To GridEXSatr.CurrentTable.Columns.Count - 1
                GridEXSatr.CurrentTable.Columns.Item(i).Visible = False
            Next

            GridEXSatr.CurrentTable.Columns.Item("Taeed").Caption = "انتخاب"
            GridEXSatr.CurrentTable.Columns.Item("Taeed").Visible = True
            GridEXSatr.CurrentTable.Columns.Item("Taeed").Width = 20
            GridEXSatr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXSatr.CurrentTable.Columns.Item("Taeed").Position = 0
            GridEXSatr.CurrentTable.Columns.Item("Taeed").Selectable = True
            GridEXSatr.CurrentTable.Columns.Item("Taeed").ActAsSelector = True
            GridEXSatr.CurrentTable.Columns.Item("Taeed").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXSatr.CurrentTable.Columns.Item("ShomarehFaktor").Caption = "شماره فاکتور"
            GridEXSatr.CurrentTable.Columns.Item("ShomarehFaktor").Visible = True
            GridEXSatr.CurrentTable.Columns.Item("ShomarehFaktor").Width = 90
            GridEXSatr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXSatr.CurrentTable.Columns.Item("ShomarehFaktor").EditType = Janus.Windows.GridEX.EditType.NoEdit
            GridEXSatr.CurrentTable.Columns.Item("ShomarehFaktor").Position = 1
            GridEXSatr.CurrentTable.Columns.Item("ShomarehFaktor").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXSatr.CurrentTable.Columns.Item("TarikhFaktor").Caption = "تاريخ فاکتور"
            GridEXSatr.CurrentTable.Columns.Item("TarikhFaktor").Visible = True
            GridEXSatr.CurrentTable.Columns.Item("TarikhFaktor").Width = 80
            GridEXSatr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXSatr.CurrentTable.Columns.Item("TarikhFaktor").EditType = Janus.Windows.GridEX.EditType.NoEdit
            GridEXSatr.CurrentTable.Columns.Item("TarikhFaktor").Position = 2
            GridEXSatr.CurrentTable.Columns.Item("TarikhFaktor").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXSatr.CurrentTable.Columns.Item("CodeMoshtary").Caption = "کد مشتری"
            GridEXSatr.CurrentTable.Columns.Item("CodeMoshtary").Visible = True
            GridEXSatr.CurrentTable.Columns.Item("CodeMoshtary").Width = 80
            GridEXSatr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXSatr.CurrentTable.Columns.Item("CodeMoshtary").EditType = Janus.Windows.GridEX.EditType.NoEdit
            GridEXSatr.CurrentTable.Columns.Item("CodeMoshtary").Position = 3
            GridEXSatr.CurrentTable.Columns.Item("CodeMoshtary").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXSatr.CurrentTable.Columns.Item("NameMoshtary").Caption = "نام مشتـری"
            GridEXSatr.CurrentTable.Columns.Item("NameMoshtary").Visible = True
            GridEXSatr.CurrentTable.Columns.Item("NameMoshtary").Width = 150
            GridEXSatr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXSatr.CurrentTable.Columns.Item("NameMoshtary").EditType = Janus.Windows.GridEX.EditType.NoEdit
            GridEXSatr.CurrentTable.Columns.Item("NameMoshtary").Position = 4
            GridEXSatr.CurrentTable.Columns.Item("NameMoshtary").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXSatr.CurrentTable.Columns.Item("Mablaghfaktor").Caption = "مبلغ فاکتـور"
            GridEXSatr.CurrentTable.Columns.Item("Mablaghfaktor").Visible = True
            GridEXSatr.CurrentTable.Columns.Item("Mablaghfaktor").Width = 100
            GridEXSatr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXSatr.CurrentTable.Columns.Item("Mablaghfaktor").EditType = Janus.Windows.GridEX.EditType.NoEdit
            GridEXSatr.CurrentTable.Columns.Item("Mablaghfaktor").Position = 5
            GridEXSatr.CurrentTable.Columns.Item("Mablaghfaktor").FormatString = "N"
            GridEXSatr.CurrentTable.Columns.Item("Mablaghfaktor").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXSatr.CurrentTable.Columns.Item("MablaghDaryafti").Caption = "مبلغ دریافتـی"
            GridEXSatr.CurrentTable.Columns.Item("MablaghDaryafti").Visible = True
            GridEXSatr.CurrentTable.Columns.Item("MablaghDaryafti").Width = 100
            GridEXSatr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXSatr.CurrentTable.Columns.Item("MablaghDaryafti").EditType = Janus.Windows.GridEX.EditType.NoEdit
            GridEXSatr.CurrentTable.Columns.Item("MablaghDaryafti").Position = 6
            GridEXSatr.CurrentTable.Columns.Item("MablaghDaryafti").FormatString = "N"
            GridEXSatr.CurrentTable.Columns.Item("MablaghDaryafti").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXSatr.CurrentTable.Columns.Item("MandehFaktor").Caption = "مانده فاکتـور"
            GridEXSatr.CurrentTable.Columns.Item("MandehFaktor").Visible = True
            GridEXSatr.CurrentTable.Columns.Item("MandehFaktor").Width = 100
            GridEXSatr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXSatr.CurrentTable.Columns.Item("MandehFaktor").EditType = Janus.Windows.GridEX.EditType.NoEdit
            GridEXSatr.CurrentTable.Columns.Item("MandehFaktor").Position = 7
            GridEXSatr.CurrentTable.Columns.Item("MandehFaktor").FormatString = "N"
            GridEXSatr.CurrentTable.Columns.Item("MandehFaktor").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXSatr.CurrentTable.Columns.Item("Tozihat").Caption = "توضیحـــات"
            GridEXSatr.CurrentTable.Columns.Item("Tozihat").Visible = True
            GridEXSatr.CurrentTable.Columns.Item("Tozihat").Width = 300
            GridEXSatr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXSatr.CurrentTable.Columns.Item("Tozihat").Position = 8
            GridEXSatr.CurrentTable.Columns.Item("Tozihat").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXSatr.CurrentTable.Columns.Item("NatijehPeygiri").Caption = "نتیجه پیگیـری"
            GridEXSatr.CurrentTable.Columns.Item("NatijehPeygiri").Visible = True
            GridEXSatr.CurrentTable.Columns.Item("NatijehPeygiri").Width = 300
            GridEXSatr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXSatr.CurrentTable.Columns.Item("NatijehPeygiri").Position = 9
            GridEXSatr.CurrentTable.Columns.Item("NatijehPeygiri").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXSatr.CurrentTable.Columns.Item("ccPeygiriFaktorSatr").Caption = "ccPeygiriFaktorSatr"
            GridEXSatr.CurrentTable.Columns.Item("ccPeygiriFaktorSatr").Visible = False
            GridEXSatr.CurrentTable.Columns.Item("ccPeygiriFaktorSatr").Width = 0
            GridEXSatr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXSatr.CurrentTable.Columns.Item("ccPeygiriFaktorSatr").Position = 10
            GridEXSatr.CurrentTable.Columns.Item("ccPeygiriFaktorSatr").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXSatr.CurrentTable.Columns.Item("ccMoshtary").Caption = "ccMoshtary"
            GridEXSatr.CurrentTable.Columns.Item("ccMoshtary").Visible = False
            GridEXSatr.CurrentTable.Columns.Item("ccMoshtary").Width = 0
            GridEXSatr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXSatr.CurrentTable.Columns.Item("ccMoshtary").Position = 11
            GridEXSatr.CurrentTable.Columns.Item("ccMoshtary").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            For i As Integer = 0 To GridEXSatr.RootTable.Columns.Count - 1
                If GridEXSatr.RootTable.Columns(i).Type.IsValueType Then
                    GridEXSatr.RootTable.Columns(i).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                    GridEXSatr.RootTable.Columns(i).FormatMode = Janus.Windows.GridEX.FormatMode.UseIFormattable
                    GridEXSatr.RootTable.Columns(i).FormatString = "###,###.##"
                    GridEXSatr.RootTable.Columns(i).TotalFormatMode = Janus.Windows.GridEX.FormatMode.UseIFormattable
                    GridEXSatr.RootTable.Columns(i).TotalFormatString = "###,###.##"
                End If
            Next

            GridEXSatr.Visible = True
            GridEXSatr.HeaderFormatStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> SetGridStyleSatr ")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> SetGridStyleSatr ")
        End Try

    End Sub
    Private Sub DeleteSatr(ByVal ccPeygiriFaktor As Double, ByVal ccPeygiriFaktorSatr As Double, ByVal ShomarehFaktor As Double)
        Try
            Dim ShomarehPeygiri As Integer = 0
            ShomarehPeygiri = objTools.DLookup("ShomarehPeygiri", "Sales.PeygiriFaktor", "ccPeygiriFaktor = " & ccPeygiriFaktor)

            If objTools.DLookup("Vazeiat", "Sales.PeygiriFaktor", "ccPeygiriFaktor = " & ccPeygiriFaktor) <> 1 Then
                MsgBox("شماره پیگیری " & ShomarehPeygiri & " از حالت بدون وضعیت خارج شده است . امکان حذف فاکتورهای آن وجود ندارد .", MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " پیام")
                Exit Sub
            End If

            DeleteFaktor(ccPeygiriFaktorSatr)

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> DeleteSatr ")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> DeleteSatr ")
        End Try
    End Sub
    Private Sub DeleteFaktor(ByVal ccPeygiriFaktorSatr As Double)
        Try
            Dim cnSQL As New SqlConnection
            Dim cmSQL As New SqlCommand
            Dim strSQL As String = ""

            strSQL = "Sales.spPeygiriFaktor_DeleteFaktor "

            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            cmSQL.Parameters.AddWithValue("ccPeygiriFaktorSatr", ccPeygiriFaktorSatr)

            cmSQL.ExecuteNonQuery()

            cmSQL = Nothing
            cnSQL.Close()

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> DeleteFaktor ")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> DeleteFaktor ")
        End Try
    End Sub
    Private Sub BoundCurrencyManagerSatr()
        Try
            cmSatr = CType(BindingContext(GridEXSatr.DataSource), CurrencyManager)
            AddHandler cmSatr.ItemChanged, AddressOf cmSatr_ItemChanged
            AddHandler cmSatr.PositionChanged, AddressOf cmSatr_PositionChanged
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->BoundCurrencyManagerSatr")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->BoundCurrencyManagerSatr")
        End Try

    End Sub
    Private Sub cmSatr_ItemChanged(ByVal sender As Object, ByVal e As ItemChangedEventArgs)
        Try

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->cmSatr_ItemChanged")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->cmSatr_ItemChanged")
        End Try

    End Sub
    Private Sub cmSatr_PositionChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->cmSatr_PositionChanged")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->cmSatr_PositionChanged")
        End Try

    End Sub
    Private Sub TaeenVazeiat(ByVal Type As Integer)
        '' Type : 2 --> Ersal // 3 --> Taeed

        If dvTitr.Count = 0 Then Exit Sub

        Try
            If MsgBox("آيا به انتخاب های خود اطمینان دارید؟", MsgBoxStyle.YesNo + MsgBoxStyle.Question + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.DefaultButton2, "تعیین وضعیت رکورد") = MsgBoxResult.Yes Then
                For i As Integer = 0 To GridEXTitr.GetCheckedRows().Length - 1
                    UpdateVazeiat(Val(GridEXTitr.GetCheckedRows(i).Cells("ccPeygiriFaktor").Text.Replace(",", "")), Type)
                Next

                Search()

            End If

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> TaeenVazeiat ")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> TaeenVazeiat ")
        End Try
    End Sub
    Private Sub UpdateVazeiat(ByVal ccPeygiriFaktor As Double, ByVal Type As Integer)
        Try
            Dim cnSQL As New SqlConnection
            Dim cmSQL As New SqlCommand
            Dim strSQL As String = ""

            Dim ShomarehPeygiri As Integer
            ShomarehPeygiri = objTools.DLookup("ShomarehPeygiri", "Sales.PeygiriFaktor", "ccPeygiriFaktor = " & ccPeygiriFaktor)

            If IsValidUpdate(ccPeygiriFaktor, Type) = 0 Then
                If Type = 2 Then
                    MsgBox("وضعیت شماره پیگیری " & ShomarehPeygiri & " « بدون وضعیت » نمی باشد، نمی توانید آن را ارســال نمایید .", MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " خـطا")
                ElseIf Type = 3 Then
                    MsgBox("وضعیت شماره پیگیری " & ShomarehPeygiri & " « ارسال شده » نمی باشد، نمی توانید آن را تاییــد نمایید .", MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " خـطا")
                End If
                Exit Sub
            ElseIf IsValidUpdate(ccPeygiriFaktor, Type) = 2 Then
                MsgBox("شماره پیگیری " & ShomarehPeygiri & " فاقد فاکتور می باشد . امکان ارسال آن وجود ندارد .", MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " خـطا")
                Exit Sub
            End If

            strSQL = "Sales.spPeygiriFaktor_UpdateVazeiat "

            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            cmSQL.Parameters.AddWithValue("ccPeygiriFaktor", ccPeygiriFaktor)
            cmSQL.Parameters.AddWithValue("Type", Type)

            cmSQL.ExecuteNonQuery()

            cmSQL = Nothing
            cnSQL.Close()

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> UpdateVazeiat ")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> UpdateVazeiat ")
        End Try
    End Sub
    Private Function IsValidUpdate(ByVal ccPeygiriFaktor As Double, ByVal Type As Integer) As Integer
        IsValidUpdate = 0

        Dim Vazeiat As Integer = 0
        Dim CountFaktor As Integer = 0

        Vazeiat = objTools.DLookup("Vazeiat", "Sales.PeygiriFaktor", "ccPeygiriFaktor = " & ccPeygiriFaktor)
        CountFaktor = objTools.DCount("ccFaktorTitr", "Sales.PeygiriFaktorSatr", "ccPeygiriFaktor = " & ccPeygiriFaktor)

        If Type = 2 Then
            If Vazeiat <> 1 Then
                Exit Function
            End If

            If CountFaktor = 0 Then
                IsValidUpdate = 2
                Exit Function
            End If
        End If

        If Type = 3 Then
            If Vazeiat <> 2 Then
                Exit Function
            End If
        End If

        Return 1
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
    Private Sub UpdateTozihat_Peygiri(ByVal ccPeygiriFaktorSatr As Double)
        Dim cnSQL As New SqlConnection
        Dim cmSQL As New SqlCommand
        Dim strSQL As String

        Dim Vazeiat As Integer
        Dim Tozihat As String
        Dim NatijehPeygiri As String

        Vazeiat = objTools.DLookup("Vazeiat", "Sales.PeygiriFaktor", "ccPeygiriFaktor = " & Val(GridEXTitr.CurrentRow.Cells("ccPeygiriFaktor").Text.Replace(",", "")))

        If Vazeiat = 1 Then
            Tozihat = GridEXSatr.CurrentRow.Cells("Tozihat").Text
        Else
            Tozihat = objTools.DLookup("Tozihat", "Sales.PeygiriFaktorSatr", "ccPeygiriFaktorSatr = " & Val(GridEXSatr.CurrentRow.Cells("ccPeygiriFaktorSatr").Text.Replace(",", "")))
        End If

        If Vazeiat = 2 Then
            NatijehPeygiri = GridEXSatr.CurrentRow.Cells("NatijehPeygiri").Text
        Else
            NatijehPeygiri = objTools.DLookup("NatijehPeygiri", "Sales.PeygiriFaktorSatr", "ccPeygiriFaktorSatr = " & Val(GridEXSatr.CurrentRow.Cells("ccPeygiriFaktorSatr").Text.Replace(",", "")))
        End If

        Try
            strSQL = "Sales.spPeygiriFaktor_UpdateTozihat_Peygiri "

            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            cmSQL.Parameters.AddWithValue("ccPeygiriFaktorSatr", ccPeygiriFaktorSatr)
            cmSQL.Parameters.AddWithValue("Tozihat", Tozihat)
            cmSQL.Parameters.AddWithValue("NatijehPeygiri", NatijehPeygiri)

            cmSQL.ExecuteNonQuery()

            cmSQL = Nothing
            cnSQL.Close()

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> UpdateTozihat_Peygiri ")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> UpdateTozihat_Peygiri ")
        End Try
    End Sub
    Private Sub PrintPeygiri()
        Try
            Dim cnSQL As SqlConnection
            Dim cmSQL As New SqlCommand
            Dim strSQL As String
            Dim strPeygiri As String = ","

            If dsForm.Tables.Contains("tbl_PeygiriFaktor") Then
                dsForm.Tables.Remove("tbl_PeygiriFaktor")
            End If

            For i As Integer = 0 To GridEXTitr.GetCheckedRows().Length - 1
                strPeygiri &= GridEXTitr.GetCheckedRows(i).Cells("ccPeygiriFaktor").Text.Trim.Replace(",", "") & ","
            Next

            strSQL = "Sales.spPeygiriFaktor_PrintPeygiri "

            Windows.Forms.Cursor.Current = Cursors.WaitCursor
            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()
            cmSQL.CommandTimeout = "999999"

            cmSQL.Parameters.AddWithValue("ccPeygiriFaktor_Str", strPeygiri)
            cmSQL.Parameters.AddWithValue("PrintWithMandeh", chkPrintWithMandeh.Checked)

            Dim daSQL As SqlDataAdapter
            daSQL = New SqlDataAdapter(cmSQL)
            daSQL.Fill(dsForm, "tbl_PeygiriFaktor")

            Dim rpt As New CrystalDecisions.CrystalReports.Engine.ReportDocument
            Dim rpttables As CrystalDecisions.CrystalReports.Engine.Tables
            Dim rptformula As CrystalDecisions.CrystalReports.Engine.FormulaFieldDefinitions
            Dim frm As New Forms_dll.frmGL_Gozaresh

            If chkPrintWithMandeh.Checked = False Then
                rpt.Load(rptPath & "\rptFO_GozareshPeigiryFaktorHa.rpt")
            Else
                rpt.Load(rptPath & "\rptFO_GozareshPeigiryFaktorHa_WithMandehMoshtary.rpt")
            End If

            rpttables = rpt.Database.Tables
            rpttables.Item(0).SetDataSource(dsForm.Tables("tbl_PeygiriFaktor"))

            rptformula = rpt.DataDefinition.FormulaFields
            With rptformula

                .Item("Group_Sanad").Text = "{mydata.ccPeygiriFaktor}"

                .Item("NameMamorPakhsh").Text = "{mydata.NameMamorPakhsh}"
                .Item("TarikhPeygiri").Text = "{mydata.TarikhPeygiri}"
                .Item("ShomarehPeygiri").Text = "{mydata.ShomarehPeygiri}"
                .Item("TedadMoshtary").Text = "{mydata.TedadMoshtary}"
                .Item("TedadFaktor").Text = "{mydata.TedadFaktor}"
                .Item("FaktorShomareh").Text = "{mydata.FaktorShomareh}"
                .Item("TarikhFaktor").Text = "{mydata.TarikhFaktor}"
                .Item("CodeMoshtary").Text = "{mydata.CodeMoshtary}"
                .Item("NameMoshtary").Text = "{mydata.NameMoshtary}"
                .Item("MablaghFaktor").Text = "{mydata.MablaghFaktor}"
                .Item("MablaghDaryafti").Text = "{mydata.MablaghDaryafti}"
                .Item("MandehFaktor").Text = "{mydata.MandehFaktor}"
                .Item("Tozihat").Text = "{mydata.Tozihat}"
                .Item("NatijehPeygiri").Text = "{mydata.NatijehPeygiri}"
                If chkPrintWithMandeh.Checked Then
                    .Item("MandehMoshtary").Text = "{mydata.MandehMoshtary}"
                End If
                .Item("Title").Text = "'" & "پیگیــری فاکتــور" & "'"
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
                .Zoom(100)
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
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> PrintPeygiri ")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> PrintPeygiri ")
        End Try
    End Sub
#End Region
#Region "From Buttons "
    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Search()
    End Sub
    Private Sub GridEXTitr_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GridEXTitr.Click
        RefreshSatrData()
    End Sub
    Private Sub GridEXTitr_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GridEXTitr.DoubleClick
        Mode = UD_Dll.Enums.GL_ModeForms.UpdateRecord
        SetForm()
    End Sub
    Private Sub btnNewSanad_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNewSanad.Click
        Mode = UD_Dll.Enums.GL_ModeForms.AddNewRecord
        SetForm()
    End Sub
    Private Sub btnSaveSanad_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveSanad.Click
        Try
            Select Case Mode
                Case UD_Dll.Enums.GL_ModeForms.AddNewRecord
                    If Not objSec.HaveUserPermission(SN, UD_Dll.Security.ePermissions.Add) Then Exit Sub
                    If AddNewSanad() = False Then
                        Exit Sub
                    End If
                Case UD_Dll.Enums.GL_ModeForms.UpdateRecord
                    If Not objSec.HaveUserPermission(SN, UD_Dll.Security.ePermissions.Update) Then Exit Sub
                    If UpdateSanad() = False Then
                        Exit Sub
                    End If
            End Select

            Mode = UD_Dll.Enums.GL_ModeForms.None
            SetForm()

            Search()

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> btnSaveSanad_Click ")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> btnSaveSanad_Click ")
        End Try
    End Sub
    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Mode = UD_Dll.Enums.GL_ModeForms.None
        SetForm()
    End Sub
    Private Sub btnCancelSatr_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelSatr.Click
        ModeSatr = UD_Dll.Enums.GL_ModeForms.None
        SetFormSatr()
    End Sub
    Private Sub btnDeleteSanad_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeleteSanad.Click
        If dvTitr.Count = 0 Then Exit Sub

        If Not objSec.HaveUserPermission(SN, UD_Dll.Security.ePermissions.DeleteSatr) Then Exit Sub
        If Not objSec.HaveUserPermission(SN, UD_Dll.Security.ePermissions.Delete) Then Exit Sub

        Try
            If MsgBox("آيا مایلید پیگیری های انتخاب شده حذف گردند؟", MsgBoxStyle.YesNo + MsgBoxStyle.Question + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.DefaultButton2, "حذف رکورد") = MsgBoxResult.Yes Then
                For i As Integer = 0 To GridEXTitr.GetCheckedRows().Length - 1
                    Delete(Val(GridEXTitr.GetCheckedRows(i).Cells("ccPeygiriFaktor").Text.Replace(",", "")), Val(GridEXTitr.GetCheckedRows(i).Cells("ShomarehPeygiri").Text.Replace(",", "")))
                Next

                Search()
            End If

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> btnDeleteSanad_Click ")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> btnDeleteSanad_Click ")
        End Try
    End Sub
    Private Sub GridEXSatr_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GridEXSatr.Click
        tmsMandeh.Text = "مانده این مشتری = " & Format(SetMandeh(Val(GridEXSatr.CurrentRow.Cells("ccMoshtary").Text.Replace(",", ""))), "###,###") & " ريال"
    End Sub
    Private Sub btnAddFaktor_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddFaktor.Click
        If cmTitr.Position = -1 Then Exit Sub

        ' 2000 FO_PishFaktorTaeedModir
        'If UserName.ToUpper <> "ADMINISTRATOR" Then
        '    If (Not ObjCode.CheckPermission(2000)) Then Exit Sub
        'End If

        If objTools.DLookup("Vazeiat", "Sales.PeygiriFaktor", "ccPeygiriFaktor = " & Val(GridEXTitr.CurrentRow.Cells("ccPeygiriFaktor").Text.Replace(",", ""))) <> 1 Then
            MsgBox("شماره پیگیری انتخاب شده از حالت بدون وضعیت خارج شده است . نمی توانید فاکتور جدید به آن اضافه نمایید . .", MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " خـطا")
            Exit Sub
        End If

        Dim frm As New frmFO_InsertFaktor
        frm.frm_ccPeygiriFaktor = Val(GridEXTitr.CurrentRow.Cells("ccPeygiriFaktor").Text.Replace(",", ""))

        Me.Hide()
        frm.ShowDialog()
        Me.Show()

        RefreshSatrData()
    End Sub
    Private Sub btnNewSatr_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNewSatr.Click
        If cmTitr.Position = -1 Then Exit Sub
        ModeSatr = UD_Dll.Enums.GL_ModeForms.AddNewRow
        SetFormSatr()
    End Sub
    Private Sub btnSaveSatr_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveSatr.Click
        Try
            Select Case ModeSatr
                Case UD_Dll.Enums.GL_ModeForms.AddNewRow
                    If Not objSec.HaveUserPermission(SN, UD_Dll.Security.ePermissions.Add) Then Exit Sub
                    If AddNewRow() = False Then
                        Exit Sub
                    End If
            End Select

            ModeSatr = UD_Dll.Enums.GL_ModeForms.None
            SetFormSatr()

            RefreshSatrData()

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> btnSaveSatr_Click ")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> btnSaveSatr_Click ")
        End Try
    End Sub
    Private Sub btnRemoveFaktor_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemoveFaktor.Click
        If cmTitr.Position = -1 Then Exit Sub
        If dvSatr.Count = 0 Then Exit Sub

        If Not objSec.HaveUserPermission(SN, UD_Dll.Security.ePermissions.DeleteSatr) Then Exit Sub
        If Not objSec.HaveUserPermission(SN, UD_Dll.Security.ePermissions.Delete) Then Exit Sub

        Dim ccPeygiriFaktor As Double
        ccPeygiriFaktor = Val(GridEXTitr.CurrentRow.Cells("ccPeygiriFaktor").Text.Replace(",", ""))

        Try
            If MsgBox("آيا مایلید فاکتـورهای انتخاب شده حذف گردند؟", MsgBoxStyle.YesNo + MsgBoxStyle.Question + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.DefaultButton2, "حذف رکورد") = MsgBoxResult.Yes Then
                For i As Integer = 0 To GridEXSatr.GetCheckedRows().Length - 1
                    DeleteSatr(ccPeygiriFaktor, Val(GridEXSatr.GetCheckedRows(i).Cells("ccPeygiriFaktorSatr").Text.Replace(",", "")), Val(GridEXSatr.GetCheckedRows(i).Cells("ShomarehFaktor").Text.Replace(",", "")))
                Next

                RefreshSatrData()

            End If

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> btnRemoveFaktor_Click ")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> btnRemoveFaktor_Click ")
        End Try
    End Sub
    Private Sub tmsErsal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmsErsal.Click
        TaeenVazeiat(2)
    End Sub
    Private Sub tmsTaeed_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmsTaeed.Click
        TaeenVazeiat(3)
    End Sub
    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        If Not objSec.HaveUserPermission(SN, UD_Dll.Security.ePermissions.Preview) Then Exit Sub
        Try
            If dvTitr.Count > 0 Then
                Me.TopMost = False
                PrintPeygiri()
                Me.TopMost = True
            End If
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> btnPrint_Click ")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> btnPrint_Click ")
        End Try
    End Sub
    Private Sub btnReport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReport.Click
        ' 2000 FO_PishFaktorTaeedModir
        If UserName.ToUpper <> "ADMINISTRATOR" Then
            If (Not ObjCode.CheckPermission(1000105)) Then Exit Sub
        End If

        Dim frm As New frmFO_GozareshPeygiriFaktor

        Me.Hide()
        frm.ShowDialog()
        Me.Show()
    End Sub
#End Region
#Region "From Stored Procedures and Reports "
    '' ------------------------------------------------
    '' *******    frmFO_PeygiriFaktor    *******

    '' sp :
    '' Sales.spPeygiriFaktor_SearchTitr
    '' Sales.spPeygiriFaktor_InsertTitr
    '' Sales.spPeygiriFaktor_UpdateTitr
    '' Sales.spPeygiriFaktor_DeleteTitr
    '' Sales.spPeygiriFaktor_SearchSatr
    '' Sales.spPeygiriFaktor_InsertSatr
    '' Sales.spPeygiriFaktor_DeleteFaktor
    '' Sales.spPeygiriFaktor_UpdateVazeiat
    '' Sales.spPishFaktor_CalcMandehMoshtary
    '' Sales.spPeygiriFaktor_UpdateTozihat_Peygiri
    '' Sales.spPeygiriFaktor_PrintPeygiri
    '' Global.spMamorPakhsh_LoadCombo

    '' rpt :
    '' rptFO_GozareshPeigiryFaktorHa.rpt

    '' ------------------------------------------------
    '' *******    frmFO_InsertFaktor    *******

    '' sp :
    '' Sales.spPeygiriFaktor_frmInsertFaktor_Search
    '' Sales.spPeygiriFaktor_ValidFaktorMandehDar
    '' Sales.spPeygiriFaktor_InsertSatr_InsertFaktor

    '' ------------------------------------------------
    '' *******    frmFO_GozareshPeygiriFaktor    *******

    '' sp :
    '' Global.spMamorPakhsh_LoadCombo
    '' Report.spPeygiriFaktor_Print

    '' rpt :
    '' rptFO_GozareshPeigiryFaktor_Print.rpt

    '' ------------------------------------------------
    '' *******    frmFO_SerchFaktor    *******

    '' Sales.spPeygiriFaktor_SearchFaktorMandehDar

    '' ------------------------------------------------

#End Region
    
End Class