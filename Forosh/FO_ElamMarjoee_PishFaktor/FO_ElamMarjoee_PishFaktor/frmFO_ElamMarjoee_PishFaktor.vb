Public Class frmFO_ElamMarjoee_PishFaktor

#Region "Variable AND Constant Declration"
    Const cntCodeSubSystem As Long = 1000137
    Const TitrOrgSize As Integer = 243
    Const SatrOrgSize As Integer = 250
    Const TitrEditSize As Integer = 211
    Const SatrEditSize As Integer = 221
    Dim cmTitr As CurrencyManager
    Dim cmSatr As CurrencyManager
    Dim dvTitr, dvSatr As DataView
    Dim tCodeCounter As Long
    Const FormTableName = "tblFO_ElamMarjoee"
    Dim ErrPro As New ErrorProvider
    Dim dsForm As New DataSet
    Private SN As Integer
    Dim txtCaption As String
    Private WithEvents BS As New UD_Dll.PassString
    Dim FirstInsert As Boolean = False
    Dim Mode As UD_Dll.Enums.GL_ModeForms = UD_Dll.Enums.GL_ModeForms.None
    Public FlgValidPishFaktor As Boolean = False
    Dim IsMalyatAvarezTakhfif As Boolean
#End Region
    Private Sub frmFO_ElamMarjoee_PishFaktor_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetParameter()
        SN = objSec.GetSecurityNumber(cntCodeSubSystem, UserName)
        LoadCombo()
        cmbVazeiat.SelectedIndex = 0

        objTools.ChangeKbd(UD_Dll.mdlUtility.KBD.Farsi)
        IsMalyatAvarezTakhfif = objTools.ConvertNulls(objTools.DLookup("IsMalyatAvarezTakhfif", "tblGL_SysConfig", "CodeMahal = " & CodeMahalFaal), False)

        SearchTitr()
        SetForm()
    End Sub
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
            txtCaption = "اعلام مرجوعی پیش فاکتور"
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
        Try
            Dim Strsql As String
            Dim daSQL As SqlDataAdapter
            Dim cnSQL As New SqlConnection

            ' --------------------------------------- Load Combo -----------------
            ' Load Combo Doreh
            Strsql = "Select CodeDoreh From tblFO_PishFaktor group by CodeDoreh"
            daSQL = New SqlDataAdapter(Strsql, ConnectionString)
            daSQL.Fill(dsForm, "tblCodeDoreh")
            cmbsCodeDorehPishFaktor.DataSource = Nothing
            cmbsCodeDorehPishFaktor.Items.Clear()
            cmbsCodeDorehPishFaktor.DataSource = dsForm.Tables("tblCodeDoreh").DefaultView
            cmbsCodeDorehPishFaktor.DisplayMember = "CodeDoreh"
            cmbsCodeDorehPishFaktor.ValueMember = "CodeDoreh"

            Strsql = "Select codeAnbar,NameAnbar From tblAN_Anbar where CodeMahal=" & CodeMahalFaal & " AND Faal = 1  AND "
            Strsql &= " Not exists (Select * From tblGL_SecurityData where namekarbar= '" & UserName & "' and CodeSubSystem = 633 and pk = tblAN_Anbar.CodeAnbar) "
            Strsql &= " order by NameAnbar "
            daSQL = New SqlDataAdapter(Strsql, ConnectionString)
            daSQL.Fill(dsForm, "tblAnbarS")
            cmbAnbar.DataSource = Nothing
            cmbAnbar.Items.Clear()
            cmbAnbar.DataSource = dsForm.Tables("tblAnbarS").DefaultView
            cmbAnbar.DisplayMember = "NameAnbar"
            cmbAnbar.ValueMember = "CodeAnbar"
            cmbAnbar.SelectedValue = 0

            ' --------------------------------

            cmbVazeiat.Items.Add("بدون وضعیت")
            cmbVazeiat.Items.Add("تایید شده")

            daSQL = Nothing
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->LoadCombo")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->LoadCombo")
        End Try
    End Sub
    Private Sub SetForm()
        SetButtonTitr()
        SetButtonSatr()

        Select Case Mode
            Case UD_Dll.Enums.GL_ModeForms.None
                GridEXTitr.Height = TitrOrgSize
                GridEXSatr.Height = SatrOrgSize
                GridEXTitr.Enabled = True
                GridEXSatr.Enabled = True
                ClearForm()
            Case UD_Dll.Enums.GL_ModeForms.AddNewRecord
                GridEXTitr.Height = TitrEditSize
                GridEXSatr.Height = SatrOrgSize
                GridEXTitr.Enabled = False
                GridEXSatr.Enabled = False
                txtShomarehMarjoee.Text = objTools.ConvertNulls(objTools.DMax("ShomarehElamMarjoee_PishFaktor", "Sales.ElamMarjoee_PishFaktor", "CodeMahal = " & CodeMahalFaal & " AND CodeDoreh = " & CodeDoreh), 0) + 1
                mskTarikhElamMarjoee.Text = TarikhEmrooz
                txtShomarehPishFaktor.Focus()
            Case UD_Dll.Enums.GL_ModeForms.UpdateRecord
                GridEXTitr.Height = TitrEditSize
                GridEXSatr.Height = SatrOrgSize
                GridEXTitr.Enabled = False
                GridEXSatr.Enabled = False
                txtShomarehPishFaktor.Focus()
                txtShomarehMarjoee.Text = GridEXTitr.CurrentRow.Cells("ShomarehElamMarjoee_PishFaktor").Text.Replace(",", "").ToString
                mskTarikhElamMarjoee.Text = objTools.DLookup("TarikhElamMarjoee_PishFaktor", "Sales.ElamMarjoee_PishFaktor", "ccElamMarjoee_PishFaktor = " & Val(GridEXTitr.CurrentRow.Cells("ccElamMarjoee_PishFaktor").Text.Replace(",", "")))
                Dim ccPishFaktor As Integer = objTools.DLookup("ccPishFaktor", "Sales.ElamMarjoee_PishFaktor", "ccElamMarjoee_PishFaktor = " & Val(GridEXTitr.CurrentRow.Cells("ccElamMarjoee_PishFaktor").Text.Replace(",", "")))
                txtShomarehPishFaktor.Text = objTools.DLookup("PishFaktorShomareh", "tblFO_PishFaktor", "ccPishFaktorTitr = " & ccPishFaktor)
                txtShomarehPishFaktor.Tag = ccPishFaktor
                cmbsCodeDorehPishFaktor.SelectedValue = objTools.DLookup("CodeDoreh", "tblFO_PishFaktor", "ccPishFaktorTitr = " & ccPishFaktor)
                cmbsCodeDorehPishFaktor.SelectedValue = objTools.DLookup("CodeDoreh", "tblFO_PishFaktor", "ccPishFaktorTitr = " & ccPishFaktor)
                FlgValidPishFaktor = True
            Case UD_Dll.Enums.GL_ModeForms.AddNewRow
                GridEXTitr.Height = TitrOrgSize
                GridEXSatr.Height = SatrEditSize
                GridEXTitr.Enabled = False
                GridEXSatr.Enabled = False
                txtCodeKala.Text = ""
                txtCodeKala.Tag = 0
                txtCodeKala.Focus()
                lblNameKala.Text = ""
                txtTedadKala.Text = ""
                txtFee.Text = ""
                mskFeeMiyangin.Text = ""
            Case UD_Dll.Enums.GL_ModeForms.UpdateRow
                GridEXTitr.Height = TitrOrgSize
                GridEXSatr.Height = SatrEditSize
                GridEXTitr.Enabled = False
                GridEXSatr.Enabled = False
                txtCodeKala.Text = Val(GridEXSatr.CurrentRow.Cells("CodeKala").Text.Replace(",", ""))
                txtCodeKala.Tag = Val(GridEXSatr.CurrentRow.Cells("ccKala").Text.Replace(",", ""))
                lblNameKala.Text = GridEXSatr.CurrentRow.Cells("NameKala").Text.Replace(",", "")
                txtTedadKala.Text = Val(GridEXSatr.CurrentRow.Cells("Tedad").Text.Replace(",", ""))
                txtFee.Text = Val(GridEXSatr.CurrentRow.Cells("Fee").Text.Replace(",", ""))
                mskFeeMiyangin.Text = Val(GridEXSatr.CurrentRow.Cells("Btkf").Text.Replace(",", ""))
        End Select
    End Sub
    Private Sub SetButtonTitr()
        Try
            Select Case Mode
                Case UD_Dll.Enums.GL_ModeForms.None
                    btnNewTitr.Enabled = True
                    If Not dvTitr Is Nothing Then
                        If dvTitr.Count > 0 Then
                            btnUpdateTitr.Enabled = True
                            btnRemoveTitr.Enabled = True
                            btnTaeed.Enabled = True
                        Else
                            btnUpdateTitr.Enabled = False
                            btnRemoveTitr.Enabled = False
                            btnTaeed.Enabled = False
                        End If
                    Else
                        btnUpdateTitr.Enabled = False
                        btnRemoveTitr.Enabled = False
                        btnTaeed.Enabled = False
                    End If
                    btnSaveTitr.Enabled = False
                    btnCancelTitr.Enabled = False
                Case UD_Dll.Enums.GL_ModeForms.AddNewRecord
                    btnNewTitr.Enabled = False
                    btnUpdateTitr.Enabled = False
                    btnSaveTitr.Enabled = True
                    btnCancelTitr.Enabled = True
                    btnRemoveTitr.Enabled = False
                    btnTaeed.Enabled = False
                Case UD_Dll.Enums.GL_ModeForms.UpdateRecord
                    btnNewTitr.Enabled = False
                    btnUpdateTitr.Enabled = False
                    btnSaveTitr.Enabled = True
                    btnCancelTitr.Enabled = True
                    btnRemoveTitr.Enabled = False
                    btnTaeed.Enabled = False
                Case UD_Dll.Enums.GL_ModeForms.AddNewRow
                    btnNewTitr.Enabled = False
                    btnUpdateTitr.Enabled = False
                    btnSaveTitr.Enabled = False
                    btnCancelTitr.Enabled = False
                    btnRemoveTitr.Enabled = False
                    btnTaeed.Enabled = False
                Case UD_Dll.Enums.GL_ModeForms.UpdateRow
                    btnNewTitr.Enabled = False
                    btnUpdateTitr.Enabled = False
                    btnSaveTitr.Enabled = False
                    btnCancelTitr.Enabled = False
                    btnRemoveTitr.Enabled = False
                    btnTaeed.Enabled = False
            End Select
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> SetButtonTitr")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> SetButtonTitr")
        End Try
    End Sub
    Private Sub SetButtonSatr()
        Try
            Select Case Mode
                Case UD_Dll.Enums.GL_ModeForms.None
                    If Not dvTitr Is Nothing Then
                        If dvTitr.Count > 0 Then
                            btnNewSatr.Enabled = True
                        Else
                            btnNewSatr.Enabled = False
                        End If
                    Else
                        btnNewSatr.Enabled = False
                    End If
                    If Not dvSatr Is Nothing Then
                        If dvSatr.Count > 0 Then
                            btnUpdateSatr.Enabled = True
                            btnRemoveSatr.Enabled = True
                        Else
                            btnUpdateSatr.Enabled = False
                            btnRemoveSatr.Enabled = False
                        End If
                    Else
                        btnUpdateSatr.Enabled = False
                        btnRemoveSatr.Enabled = False
                    End If
                    btnSaveSatr.Enabled = False
                    btnCancelSatr.Enabled = False
                Case UD_Dll.Enums.GL_ModeForms.AddNewRecord
                    btnNewSatr.Enabled = False
                    btnUpdateSatr.Enabled = False
                    btnSaveSatr.Enabled = False
                    btnCancelSatr.Enabled = False
                    btnRemoveSatr.Enabled = False
                Case UD_Dll.Enums.GL_ModeForms.UpdateRecord
                    btnNewSatr.Enabled = False
                    btnUpdateSatr.Enabled = False
                    btnSaveSatr.Enabled = False
                    btnCancelSatr.Enabled = False
                    btnRemoveSatr.Enabled = False
                Case UD_Dll.Enums.GL_ModeForms.AddNewRow
                    btnNewSatr.Enabled = False
                    btnUpdateSatr.Enabled = False
                    btnSaveSatr.Enabled = True
                    btnCancelSatr.Enabled = True
                    btnRemoveSatr.Enabled = False
                Case UD_Dll.Enums.GL_ModeForms.UpdateRow
                    btnNewSatr.Enabled = False
                    btnUpdateSatr.Enabled = False
                    btnSaveSatr.Enabled = True
                    btnCancelSatr.Enabled = True
                    btnRemoveSatr.Enabled = False
            End Select
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> SetButtonTitr")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> SetButtonTitr")
        End Try
    End Sub
    Private Sub ClearForm()
        Try
            txtShomarehPishFaktor.Text = ""
            mskTarikhElamMarjoee.Text = ""
            cmbsCodeDorehPishFaktor.SelectedValue = CodeDoreh
            txtShomarehPishFaktor.Text = ""
            lblNameMoshtary.Text = ""
            cmbAnbar.SelectedValue = objTools.ConvertNulls(objTools.DLookup("CodeAnbar", "tblAN_Anbar", "Faal = 1 AND AnbarAsly = 1 AND CodeMahal = " & CodeMahalFaal), 0)
            txtCodeKala.Text = ""
            lblTedadMojod.Text = ""
            txtTedadKala.Text = ""
            txtFee.Text = ""
            mskFeeMiyangin.Text = ""
            FlgValidPishFaktor = False
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> ClearForm")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> ClearForm")
        End Try
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
            dvTitr.Sort = "ShomarehElamMarjoee_PishFaktor ASC"
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

            dvTitr = New DataView(dsForm.Tables("tbl_Search"), "", "ShomarehElamMarjoee_PishFaktor ASC", DataViewRowState.CurrentRows)
            dvTitr.AllowNew = False
            dvTitr.AllowDelete = False
            dvTitr.AllowEdit = False


            GridEXTitr.DataSource = Nothing
            GridEXTitr.DataSource = dvTitr

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
            With GridEXTitr
                .DataSource = Nothing
                .DataSource = dsForm.Tables("tbl_Search").DefaultView
                .SetDataBinding(dsForm.Tables("tbl_Search").DefaultView, "")
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

            GridEXTitr.CurrentTable.Columns.Item("ShomarehElamMarjoee_PishFaktor").Caption = "شماره مرجوعی"
            GridEXTitr.CurrentTable.Columns.Item("ShomarehElamMarjoee_PishFaktor").Visible = True
            GridEXTitr.CurrentTable.Columns.Item("ShomarehElamMarjoee_PishFaktor").Width = 120
            GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXTitr.CurrentTable.Columns.Item("ShomarehElamMarjoee_PishFaktor").Position = 1
            GridEXTitr.CurrentTable.Columns.Item("ShomarehElamMarjoee_PishFaktor").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXTitr.CurrentTable.Columns.Item("TarikhElamMarjoee_PishFaktorSlash").Caption = "تاریخ مرجوعی"
            GridEXTitr.CurrentTable.Columns.Item("TarikhElamMarjoee_PishFaktorSlash").Visible = True
            GridEXTitr.CurrentTable.Columns.Item("TarikhElamMarjoee_PishFaktorSlash").Width = 150
            GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXTitr.CurrentTable.Columns.Item("TarikhElamMarjoee_PishFaktorSlash").Position = 2
            GridEXTitr.CurrentTable.Columns.Item("TarikhElamMarjoee_PishFaktorSlash").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXTitr.CurrentTable.Columns.Item("ShomarehPishFaktor").Caption = "شماره پیش فاکتور"
            GridEXTitr.CurrentTable.Columns.Item("ShomarehPishFaktor").Visible = True
            GridEXTitr.CurrentTable.Columns.Item("ShomarehPishFaktor").Width = 120
            GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXTitr.CurrentTable.Columns.Item("ShomarehPishFaktor").Position = 3
            GridEXTitr.CurrentTable.Columns.Item("ShomarehPishFaktor").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXTitr.CurrentTable.Columns.Item("CodeMoshtary").Caption = "کـد مشتری"
            GridEXTitr.CurrentTable.Columns.Item("CodeMoshtary").Visible = True
            GridEXTitr.CurrentTable.Columns.Item("CodeMoshtary").Width = 120
            GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXTitr.CurrentTable.Columns.Item("CodeMoshtary").Position = 4
            GridEXTitr.CurrentTable.Columns.Item("CodeMoshtary").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXTitr.CurrentTable.Columns.Item("NameMoshtary").Caption = "نام مشتـــری"
            GridEXTitr.CurrentTable.Columns.Item("NameMoshtary").Visible = True
            GridEXTitr.CurrentTable.Columns.Item("NameMoshtary").Width = 285
            GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXTitr.CurrentTable.Columns.Item("NameMoshtary").Position = 5
            GridEXTitr.CurrentTable.Columns.Item("NameMoshtary").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXTitr.CurrentTable.Columns.Item("NameAnbar").Caption = "نام انبــار"
            GridEXTitr.CurrentTable.Columns.Item("NameAnbar").Visible = True
            GridEXTitr.CurrentTable.Columns.Item("NameAnbar").Width = 150
            GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXTitr.CurrentTable.Columns.Item("NameAnbar").Position = 6
            GridEXTitr.CurrentTable.Columns.Item("NameAnbar").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXTitr.CurrentTable.Columns.Item("ccElamMarjoee_PishFaktor").Caption = "ccElamMarjoee_PishFaktor"
            GridEXTitr.CurrentTable.Columns.Item("ccElamMarjoee_PishFaktor").Visible = False
            GridEXTitr.CurrentTable.Columns.Item("ccElamMarjoee_PishFaktor").Width = 0
            GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXTitr.CurrentTable.Columns.Item("ccElamMarjoee_PishFaktor").Position = 7
            GridEXTitr.CurrentTable.Columns.Item("ccElamMarjoee_PishFaktor").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXTitr.CurrentTable.Columns.Item("ccPishFaktor").Caption = "ccPishFaktor"
            GridEXTitr.CurrentTable.Columns.Item("ccPishFaktor").Visible = False
            GridEXTitr.CurrentTable.Columns.Item("ccPishFaktor").Width = 0
            GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXTitr.CurrentTable.Columns.Item("ccPishFaktor").Position = 7
            GridEXTitr.CurrentTable.Columns.Item("ccPishFaktor").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXTitr.CurrentTable.Columns.Item("ccAnbar").Caption = "ccAnbar"
            GridEXTitr.CurrentTable.Columns.Item("ccAnbar").Visible = False
            GridEXTitr.CurrentTable.Columns.Item("ccAnbar").Width = 0
            GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXTitr.CurrentTable.Columns.Item("ccAnbar").Position = 8
            GridEXTitr.CurrentTable.Columns.Item("ccAnbar").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

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
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> SetGridStyleTitr ")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> SetGridStyleTitr ")
        End Try

    End Sub
    Private Sub SearchSatr()
        Dim cnSQL As New SqlConnection
        Dim cmSQL As New SqlCommand
        Dim daSQL As New SqlDataAdapter
        Dim strSQL As String = ""

        If dsForm.Tables.Contains("tbl_SearchSatr") Then
            dsForm.Tables.Remove("tbl_SearchSatr")
        End If

        Try
            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()

            strSQL = "Sales.spElamMarjoee_PishFaktor_SearchSatr"

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            cmSQL.Parameters.AddWithValue("ccElamMarjoee_PishFaktor", Val(GridEXTitr.CurrentRow.Cells("ccElamMarjoee_PishFaktor").Text.Replace(",", "")))

            daSQL = New SqlDataAdapter(cmSQL)
            daSQL.Fill(dsForm, "tbl_SearchSatr")

            dvSatr = New DataView
            dvSatr = dsForm.Tables("tbl_SearchSatr").DefaultView
            dvSatr.Sort = "CodeKala ASC"
            dvSatr.AllowDelete = False
            dvSatr.AllowEdit = False
            dvSatr.AllowNew = False

            cmSQL = Nothing : daSQL = Nothing

            dvSatr = New DataView(dsForm.Tables("tbl_SearchSatr"), "", "CodeKala ASC", DataViewRowState.CurrentRows)
            dvSatr.AllowNew = False
            dvSatr.AllowDelete = False
            dvSatr.AllowEdit = False


            GridEXSatr.DataSource = Nothing
            GridEXSatr.DataSource = dvSatr

            SetGridStyleSatr()
            BoundCurrencyManagerSatr()
            SetButtonSatr()

            cnSQL.Close()
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> SearchSatr ")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> SearchSatr ")
        End Try
    End Sub
    Private Sub SetGridStyleSatr()
        Try
            With GridEXSatr
                .DataSource = Nothing
                .DataSource = dsForm.Tables("tbl_SearchSatr").DefaultView
                .SetDataBinding(dsForm.Tables("tbl_SearchSatr").DefaultView, "")
                .RetrieveStructure()
            End With

            For i As Integer = 0 To GridEXSatr.CurrentTable.Columns.Count - 1
                GridEXSatr.CurrentTable.Columns.Item(i).Visible = False
            Next

            GridEXSatr.CurrentTable.Columns.Item("CodeKala").Caption = "کـد کـالا"
            GridEXSatr.CurrentTable.Columns.Item("CodeKala").Visible = True
            GridEXSatr.CurrentTable.Columns.Item("CodeKala").Width = 120
            GridEXSatr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXSatr.CurrentTable.Columns.Item("CodeKala").Position = 0
            GridEXSatr.CurrentTable.Columns.Item("CodeKala").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXSatr.CurrentTable.Columns.Item("NameKala").Caption = "نـام کـالا"
            GridEXSatr.CurrentTable.Columns.Item("NameKala").Visible = True
            GridEXSatr.CurrentTable.Columns.Item("NameKala").Width = 300
            GridEXSatr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXSatr.CurrentTable.Columns.Item("NameKala").Position = 1
            GridEXSatr.CurrentTable.Columns.Item("NameKala").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXSatr.CurrentTable.Columns.Item("Tedad").Caption = "تعــداد"
            GridEXSatr.CurrentTable.Columns.Item("Tedad").Visible = True
            GridEXSatr.CurrentTable.Columns.Item("Tedad").Width = 95
            GridEXSatr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXSatr.CurrentTable.Columns.Item("Tedad").FormatString = "G"
            GridEXSatr.CurrentTable.Columns.Item("Tedad").Position = 2
            GridEXSatr.CurrentTable.Columns.Item("Tedad").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXSatr.CurrentTable.Columns.Item("Fee").Caption = "قیمت"
            GridEXSatr.CurrentTable.Columns.Item("Fee").Visible = True
            GridEXSatr.CurrentTable.Columns.Item("Fee").Width = 120
            GridEXSatr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXSatr.CurrentTable.Columns.Item("Fee").FormatString = "G"
            GridEXSatr.CurrentTable.Columns.Item("Fee").Position = 3
            GridEXSatr.CurrentTable.Columns.Item("Fee").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXSatr.CurrentTable.Columns.Item("Btkf").Caption = "بهای تمام شده"
            GridEXSatr.CurrentTable.Columns.Item("Btkf").Visible = True
            GridEXSatr.CurrentTable.Columns.Item("Btkf").Width = 120
            GridEXSatr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXSatr.CurrentTable.Columns.Item("Btkf").FormatString = "G"
            GridEXSatr.CurrentTable.Columns.Item("Btkf").Position = 4
            GridEXSatr.CurrentTable.Columns.Item("Btkf").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXSatr.CurrentTable.Columns.Item("MablaghMaliatAvarez").Caption = "مالیات و عوارض"
            GridEXSatr.CurrentTable.Columns.Item("MablaghMaliatAvarez").Visible = True
            GridEXSatr.CurrentTable.Columns.Item("MablaghMaliatAvarez").Width = 210
            GridEXSatr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXSatr.CurrentTable.Columns.Item("MablaghMaliatAvarez").FormatString = "G"
            GridEXSatr.CurrentTable.Columns.Item("MablaghMaliatAvarez").Position = 5
            GridEXSatr.CurrentTable.Columns.Item("MablaghMaliatAvarez").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXSatr.CurrentTable.Columns.Item("ccElamMarjoeeSatr_PishFaktor").Caption = "ccElamMarjoeeSatr_PishFaktor"
            GridEXSatr.CurrentTable.Columns.Item("ccElamMarjoeeSatr_PishFaktor").Visible = False
            GridEXSatr.CurrentTable.Columns.Item("ccElamMarjoeeSatr_PishFaktor").Width = 0
            GridEXSatr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXSatr.CurrentTable.Columns.Item("ccElamMarjoeeSatr_PishFaktor").Position = 5
            GridEXSatr.CurrentTable.Columns.Item("ccElamMarjoeeSatr_PishFaktor").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

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
    Private Sub btnNewTitr_Click(sender As Object, e As EventArgs) Handles btnNewTitr.Click
        If Not objSec.HaveUserPermission(SN, UD_Dll.Security.ePermissions.Add) Then Exit Sub
        Mode = UD_Dll.Enums.GL_ModeForms.AddNewRecord
        SetForm()
    End Sub
    Private Sub btnUpdateTitr_Click(sender As Object, e As EventArgs) Handles btnUpdateTitr.Click
        If Not objSec.HaveUserPermission(SN, UD_Dll.Security.ePermissions.Update) Then Exit Sub
        If dvTitr.Count = 0 Then
            Exit Sub
        End If

        If objTools.DLookup("sVazeiat", "Sales.ElamMarjoee_PishFaktor", "ccElamMarjoee_PishFaktor = " & Val(GridEXTitr.CurrentRow.Cells("ccElamMarjoee_PishFaktor").Text.Replace(",", ""))) = 1 Then
            MsgBox("این مرجوعی تایید شده است . امکان ویرایش آن وجود ندارد .", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, "خطا")
            Exit Sub
        End If

        Mode = UD_Dll.Enums.GL_ModeForms.UpdateRecord
        SetForm()
    End Sub

    Private Sub btnSaveTitr_Click(sender As Object, e As EventArgs) Handles btnSaveTitr.Click
        If IsValidTitr() = False Then
            Exit Sub
        End If

        Select Case Mode
            Case UD_Dll.Enums.GL_ModeForms.AddNewRecord
                If Not objSec.HaveUserPermission(SN, UD_Dll.Security.ePermissions.Add) Then Exit Sub
                If SaveTitr() = False Then
                    Exit Sub
                End If
            Case UD_Dll.Enums.GL_ModeForms.UpdateRecord
                If Not objSec.HaveUserPermission(SN, UD_Dll.Security.ePermissions.Update) Then Exit Sub
                If UpdateTitr() = False Then
                    Exit Sub
                End If
        End Select

        Mode = UD_Dll.Enums.GL_ModeForms.None
        SearchTitr()
        SetForm()
    End Sub
    Private Function IsValidTitr() As Boolean
        IsValidTitr = False
        Try
            If cmbsCodeDorehPishFaktor.SelectedValue = 0 Or cmbsCodeDorehPishFaktor.SelectedIndex = -1 Then
                MsgBox("دوره پیش فاکتور را انتخاب نمایید .", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.MsgBoxRight, "خطا")
                ErrPro.SetError(cmbsCodeDorehPishFaktor, "دوره پیش فاکتور را انتخاب نمایید .")
                Exit Function
            End If
            ErrPro.SetError(cmbsCodeDorehPishFaktor, "")

            If txtShomarehPishFaktor.Text.Trim = "" Or FlgValidPishFaktor = False Then
                MsgBox("پیش فاکتور را انتخاب نمایید .", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.MsgBoxRight, "خطا")
                ErrPro.SetError(txtShomarehPishFaktor, "پیش فاکتور را انتخاب نمایید .")
                Exit Function
            End If
            ErrPro.SetError(txtShomarehPishFaktor, "")

            If cmbAnbar.SelectedValue = 0 Or cmbAnbar.SelectedIndex = -1 Then
                MsgBox("انبار مرجوعی را انتخاب نمایید .", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.MsgBoxRight, "خطا")
                ErrPro.SetError(cmbAnbar, "انبار مرجوعی را انتخاب نمایید .")
                Exit Function
            End If
            ErrPro.SetError(cmbAnbar, "")

            If mskTarikhElamMarjoee.Text.Substring(0, 4) <> CodeDoreh Then
                MsgBox("تاريخ با دوره مالی فعال يکی نيست.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "خطا")
                mskTarikhElamMarjoee.Focus()
                Exit Function
            End If

            Return True
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> IsValidTitr")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> IsValidTitr")
        End Try
    End Function
    Private Function IsValidSatr() As Boolean
        IsValidSatr = False
        Try
            If txtCodeKala.Tag = 0 Or txtCodeKala.Text.Trim = "" Then
                MsgBox("کالای مرجوعی را انتخاب نمایید .", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.MsgBoxRight, "خطا")
                ErrPro.SetError(txtCodeKala, "کالای مرجوعی را انتخاب نمایید .")
                Exit Function
            End If
            ErrPro.SetError(txtCodeKala, "")

            If Val(lblTedadMojod.Text) < Val(txtTedadKala.Text.Trim) Then
                MsgBox("تعداد وارد شده بیش از تعداد موجود است .", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.MsgBoxRight, "خطا")
                ErrPro.SetError(txtTedadKala, "تعداد وارد شده بیش از تعداد موجود است .")
                Exit Function
            End If
            ErrPro.SetError(txtTedadKala, "")

            If txtTedadKala.Text.Trim = "" Then
                MsgBox("تعداد را وارد کنید .", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.MsgBoxRight, "خطا")
                ErrPro.SetError(txtCodeKala, "تعداد را وارد کنید .")
                Exit Function
            End If
            ErrPro.SetError(txtTedadKala, "")

            Return True
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> IsValidSatr")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> IsValidSatr")
        End Try
    End Function
    Private Sub btnCancelTitr_Click(sender As Object, e As EventArgs) Handles btnCancelTitr.Click
        Mode = UD_Dll.Enums.GL_ModeForms.None
        SetForm()
    End Sub
    Private Sub btnNewSatr_Click(sender As Object, e As EventArgs) Handles btnNewSatr.Click
        If Not objSec.HaveUserPermission(SN, UD_Dll.Security.ePermissions.AddSatr) Then Exit Sub
        If objTools.DLookup("sVazeiat", "Sales.ElamMarjoee_PishFaktor", "ccElamMarjoee_PishFaktor = " & Val(GridEXTitr.CurrentRow.Cells("ccElamMarjoee_PishFaktor").Text.Replace(",", ""))) = 1 Then
            MsgBox("این مرجوعی تایید شده است . امکان افزودن کالای جدید به آن وجود ندارد .", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, "خطا")
            Exit Sub
        End If

        Mode = UD_Dll.Enums.GL_ModeForms.AddNewRow
        SetForm()
        txtCodeKala.Focus()
    End Sub
    Private Sub btnUpdateSatr_Click(sender As Object, e As EventArgs) Handles btnUpdateSatr.Click
        If Not objSec.HaveUserPermission(SN, UD_Dll.Security.ePermissions.UpdateSatr) Then Exit Sub
        If dvTitr.Count = 0 Then
            Exit Sub
        End If

        If dvSatr.Count = 0 Then
            Exit Sub
        End If

        If objTools.DLookup("sVazeiat", "Sales.ElamMarjoee_PishFaktor", "ccElamMarjoee_PishFaktor = " & Val(GridEXTitr.CurrentRow.Cells("ccElamMarjoee_PishFaktor").Text.Replace(",", ""))) = 1 Then
            MsgBox("این مرجوعی تایید شده است . امکان ویرایش کالاهای آن وجود ندارد .", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, "خطا")
            Exit Sub
        End If

        Mode = UD_Dll.Enums.GL_ModeForms.UpdateRow
        SetForm()
    End Sub
    Private Sub btnSaveSatr_Click(sender As Object, e As EventArgs) Handles btnSaveSatr.Click
        If IsValidSatr() = False Then
            Exit Sub
        End If

        Select Case Mode
            Case UD_Dll.Enums.GL_ModeForms.AddNewRow
                If Not objSec.HaveUserPermission(SN, UD_Dll.Security.ePermissions.AddSatr) Then Exit Sub
                If SaveSatr() = False Then
                    Exit Sub
                End If
            Case UD_Dll.Enums.GL_ModeForms.UpdateRow
                If Not objSec.HaveUserPermission(SN, UD_Dll.Security.ePermissions.UpdateSatr) Then Exit Sub
                If UpdateSatr() = False Then
                    Exit Sub
                End If
        End Select

        Mode = UD_Dll.Enums.GL_ModeForms.None
        SearchSatr()
        SetForm()
    End Sub
    Private Sub btnCancelSatr_Click(sender As Object, e As EventArgs) Handles btnCancelSatr.Click
        Mode = UD_Dll.Enums.GL_ModeForms.None
        SetForm()
    End Sub
    Private Sub GridEXTitr_Click(sender As Object, e As EventArgs) Handles GridEXTitr.Click
        If dvTitr.Count = 0 Then
            Exit Sub
        End If

        If GridEXTitr.CurrentRow.RowType = Janus.Windows.GridEX.RowType.FilterRow Then
            Exit Sub
        End If

        SearchSatr()

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
    Private Function SaveTitr() As Boolean

        SaveTitr = False

        Dim cnSQL As New SqlConnection
        Dim cmSQL As New SqlCommand
        Dim strSQL As String = ""
        Dim PK As Integer = 0

        Try
            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()

            strSQL = "Sales.spElamMarjoee_PishFaktor_SaveTitr"

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            cmSQL.Parameters.AddWithValue("CodeMahal", CodeMahalFaal)
            cmSQL.Parameters.AddWithValue("CodeDoreh", CodeDoreh)
            cmSQL.Parameters.AddWithValue("ShomarehElamMarjoee_PishFaktor", txtShomarehMarjoee.Text)
            cmSQL.Parameters.AddWithValue("TarikhElamMarjoee_PishFaktor", TarikhEmrooz)
            cmSQL.Parameters.AddWithValue("Saat", Format(TimeOfDay, "HH:mm:ss"))
            cmSQL.Parameters.AddWithValue("ccPishFaktor", txtShomarehPishFaktor.Tag)
            cmSQL.Parameters.AddWithValue("ccAnbar", cmbAnbar.SelectedValue)
            cmSQL.Parameters.AddWithValue("ccElamMarjoee_PishFaktor", PK)
            cmSQL.Parameters("ccElamMarjoee_PishFaktor").Direction = ParameterDirection.Output

            cmSQL.ExecuteNonQuery()

            PK = objTools.ConvertNulls(cmSQL.Parameters("ccElamMarjoee_PishFaktor").Value, 0)

            cmSQL = Nothing
            cnSQL.Close()

            If PK <> 0 Then
                Return True
            End If

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> SaveTitr ")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> SaveTitr ")
        End Try
    End Function
    Private Function UpdateTitr() As Boolean

        UpdateTitr = False

        Dim cnSQL As New SqlConnection
        Dim cmSQL As New SqlCommand
        Dim strSQL As String = ""
        Dim PK As Integer = 0
        PK = Val(GridEXTitr.CurrentRow.Cells("ccElamMarjoee_PishFaktor").Text.Replace(",", ""))

        Try
            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()

            strSQL = "Sales.spElamMarjoee_PishFaktor_UpdateTitr"

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            cmSQL.Parameters.AddWithValue("ccPishFaktor", txtShomarehPishFaktor.Tag)
            cmSQL.Parameters.AddWithValue("ccAnbar", cmbAnbar.SelectedValue)
            cmSQL.Parameters.AddWithValue("ccElamMarjoee_PishFaktor", PK)

            cmSQL.ExecuteNonQuery()

            cmSQL = Nothing
            cnSQL.Close()

            Return True

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> UpdateTitr ")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> UpdateTitr ")
        End Try
    End Function
    Private Function SaveSatr() As Boolean

        SaveSatr = False

        Dim cnSQL As New SqlConnection
        Dim cmSQL As New SqlCommand
        Dim strSQL As String = ""
        Dim PK_S As Integer = 0

        Try
            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()

            strSQL = "Sales.spElamMarjoee_PishFaktor_SaveSatr"

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            cmSQL.Parameters.AddWithValue("ccElamMarjoee_PishFaktor", Val(GridEXTitr.CurrentRow.Cells("ccElamMarjoee_PishFaktor").Text.Replace(",", "")))
            cmSQL.Parameters.AddWithValue("ccKala", txtCodeKala.Tag)
            cmSQL.Parameters.AddWithValue("Tedad", txtTedadKala.Text.Trim)
            cmSQL.Parameters.AddWithValue("Fee", txtFee.Text.Trim)
            cmSQL.Parameters.AddWithValue("Btkf", mskFeeMiyangin.Text.Trim)
            cmSQL.Parameters.AddWithValue("ccElamMarjoeeSatr_PishFaktor", PK_S)
            cmSQL.Parameters("ccElamMarjoeeSatr_PishFaktor").Direction = ParameterDirection.Output

            cmSQL.ExecuteNonQuery()

            PK_S = objTools.ConvertNulls(cmSQL.Parameters("ccElamMarjoeeSatr_PishFaktor").Value, 0)

            cmSQL = Nothing
            cnSQL.Close()

            If PK_S <> 0 Then
                CalculateMalyatAvarez(Val(GridEXTitr.CurrentRow.Cells("ccpishfaktor").Text.Replace(",", "")))
                Return True
            End If


        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> SaveSatr ")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> SaveSatr ")
        End Try
    End Function
    Private Function UpdateSatr() As Boolean

        UpdateSatr = False

        Dim cnSQL As New SqlConnection
        Dim cmSQL As New SqlCommand
        Dim strSQL As String = ""
        Dim PK_S As Integer = 0
        PK_S = Val(GridEXTitr.CurrentRow.Cells("ccElamMarjoeeSatr_PishFaktor").Text.Replace(",", ""))

        Try
            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()

            strSQL = "Sales.spElamMarjoee_PishFaktor_UpdateSatr"

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            cmSQL.Parameters.AddWithValue("ccKala", txtCodeKala.Tag)
            cmSQL.Parameters.AddWithValue("Tedad", txtTedadKala.Text.Trim)
            cmSQL.Parameters.AddWithValue("Fee", txtFee.Text.Trim)
            cmSQL.Parameters.AddWithValue("Btkf", mskFeeMiyangin.Text.Trim)
            cmSQL.Parameters.AddWithValue("ccElamMarjoeeSatr_PishFaktor", PK_S)

            cmSQL.ExecuteNonQuery()

            cmSQL = Nothing
            cnSQL.Close()

            CalculateMalyatAvarez(dvTitr(cmTitr.Position)("ccPishFaktorTitr"))

            Return True

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> UpdateSatr ")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> UpdateSatr ")
        End Try
    End Function
    Private Sub txtShomarehPishFaktor_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtShomarehPishFaktor.KeyPress
        Try
            If (Asc(e.KeyChar()) < 48 Or Asc(e.KeyChar()) > 57) And (Asc(e.KeyChar()) <> 8) Then
                e.Handled = True
            End If
            If e.KeyChar = Chr(Keys.Space) Then

                Dim frm As New frmFO_SearchPishFaktor

                Me.Hide()
                frm.ShowDialog()
                Me.Show()
            End If
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> txtShomarehPishFaktor_KeyPress")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> txtShomarehPishFaktor_KeyPress")
        End Try
    End Sub
    Private Sub txtCodeKala_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtCodeKala.KeyPress
        Try
            If (Asc(e.KeyChar()) < 48 Or Asc(e.KeyChar()) > 57) And (Asc(e.KeyChar()) <> 8) Then
                e.Handled = True
            End If
            If e.KeyChar = Chr(Keys.Space) Then

                Dim objKala As New Forms_dll.frmAN_KalaSearch
                Dim StrSql As String
                Dim PK As Integer = objTools.ConvertNulls(objTools.DLookup("ccPishFaktor", "Sales.ElamMarjoee_PishFaktor", "ccElamMarjoee_PishFaktor = " & Val(GridEXTitr.CurrentRow.Cells("ccElamMarjoee_PishFaktor").Text.Replace(",", ""))), 0)

                'StrSql = "SELECT  a.ccKala, f.CodeKala, f.NameKala "
                'StrSql &= " FROM	tblFO_PishFaktorSatr AS a WITH(NOLOCK) LEFT OUTER JOIN"
                'StrSql &= " tblFO_Faktor AS b WITH(NOLock) ON a.ccPishFaktorTitr = b.ccPishFaktor LEFT OUTER JOIN"
                'StrSql &= " tblFO_FaktorSatr AS c WITH(NOLOCK) ON b.ccFaktorTitr = c.ccFaktorTitr and a.cckala = c.cckala LEFT OUTER JOIN"
                'StrSql &= " sales.ElamMarjoee_PishFaktor AS d WITH(NOLOCK) ON a.ccPishFaktorTitr = d.ccPishFaktor LEFT OUTER JOIN"
                'StrSql &= " sales.ElamMarjoeeSatr_PishFaktor AS e WITH(NOLOCK) ON d.ccElamMarjoee_PishFaktor = e.ccElamMarjoee_PishFaktor LEFT OUTER JOIN"
                'StrSql &= " tblAN_Kala AS f WITH(NOLOCK) ON a.ccKala = f.ccKala LEFT OUTER JOIN "
                'StrSql &= " tblFO_PishFaktor AS g WITH(NOLOCK) ON a.ccPishFaktorTitr = g.ccPishFaktorTitr"
                'StrSql &= " WHERE g.sVazeiat = 2002 AND g.ccPishFakTorTitr = " & PK
                'StrSql &= " GROUP BY a.ccKala, f.CodeKala, f.NameKala, a.Tedad3"
                'StrSql &= " HAVING a.Tedad3 - SUM(ISNULL(c.Tedad3, 0)) - SUM(ISNULL(e.Tedad, 0)) > 0"

                StrSql = "select L.ccKala,L.codeKala ,L.NameKala  from ( "
                StrSql &= " select * from ("
                StrSql &= " select b.ccKala,b.Tedad3,w.codeKala ,w.NameKala, "
                StrSql &= " Isnull( (select SUM(Tedad3) from tblFO_Faktor as c left outer join tblFO_FaktorSatr d on c.ccFaktorTitr = d.ccFaktorTitr "
                StrSql &= " where sVazeiat = 4261 and ccPishFaktor = a.ccPishFaktorTitr  and ccKala = b.ccKala),0) as tedadFaktorShodeh,"
                StrSql &= " Isnull((select  sum(ss.tedad) from Sales.ElamMarjoee_PishFaktor as s left outer join Sales.ElamMarjoeeSatr_PishFaktor as ss on s.ccElamMarjoee_PishFaktor = ss.ccElamMarjoee_PishFaktor"
                StrSql &= " where s.ccPishFaktor = a.ccPishFaktorTitr and ccKala = b.ccKala),0) as tedadMarjoee"
                StrSql &= " from   tblFO_PishFaktor as a left outer join "
                StrSql &= " tblFO_PishFaktorSatr as b on a.ccPishFaktorTitr = b.ccPishFaktorTitr  left outer join tblan_Kala as w On b.ccKala = w.ccKala "
                StrSql &= " WHERE a.sVazeiat = 2002 AND a.ccPishFakTorTitr = " & PK & ") K "
                StrSql &= " where K.Tedad3 - K.tedadFaktorShodeh - K.tedadMarjoee > 0 ) L "

                If txtCodeKala.Text.Length <> 0 Then
                    objKala.tcodeKala = txtCodeKala.Text
                End If

                MultiSelection = False
                SearchItem = "CodeKala"
                objKala.SetForm(StrSql)
                objKala.ShowDialog()
                Me.txtCodeKala.Tag = objKala.tccKala
                Me.txtCodeKala.Text = objKala.tcodeKala
                Me.lblNameKala.Text = objKala.tNameKala

                Dim TedadP As Double = objTools.DLookup("Tedad3", "tblFO_PishFaktorSatr", "ccPishFaktorTitr = " & PK & " AND ccKala = " & objKala.tccKala)

                Dim TedadF As Double = objTools.ConvertNulls(objTools.DSum("Tedad3", "tblFO_FaktorSatr", "ccFaktorTitr IN (SELECT ccFaktorTitr FROM tblFO_Faktor WHERE ccPishFaktor = " & PK & ") AND ccKala = " & objKala.tccKala), 0)
                Dim TedadM As Double = objTools.ConvertNulls(objTools.DSum("Tedad", "sales.ElamMarjoeeSatr_PishFaktor AS a LEFT OUTER JOIN sales.ElamMarjoee_PishFaktor AS b ON a.ccElamMarjoee_PishFaktor = b.ccElamMarjoee_PishFaktor", "ccKala = " & objKala.tccKala & " AND b.ccPishFaktor = " & PK), 0)

                Me.txtFee.Text = objTools.DLookup("Fee", "tblFO_PishFaktorSatr", "ccPishFaktorTitr = " & PK & " AND ccKala = " & objKala.tccKala)
                Me.lblTedadMojod.Text = TedadP - TedadF - TedadM
                Me.mskFeeMiyangin.Text = ObjCode.GetMablaghMiangin(objKala.tccKala, Val(GridEXTitr.CurrentRow.Cells("ccAnbar").Text.Replace(",", "")))
                MultiSelection = False
            End If
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->txtCodeMoshtaryS_KeyPress")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->txtCodeMoshtaryS_KeyPress")
        End Try
    End Sub
    Private Sub txtCodeKala_TextChanged(sender As Object, e As EventArgs) Handles txtCodeKala.TextChanged
        If Mode = UD_Dll.Enums.GL_ModeForms.UpdateRecord Then Exit Sub
        If Trim(Me.txtCodeKala.Text) = "" Then Exit Sub
        'If Not flg Then Exit Sub
        Try
            Dim PK As Integer = objTools.DLookup("ccPishFaktor", "Sales.ElamMarjoee_PishFaktor", "ccElamMarjoee_PishFaktor = " & Val(GridEXTitr.CurrentRow.Cells("ccElamMarjoee_PishFaktor").Text.Replace(",", "")))

            Dim ccKala As Integer = objTools.ConvertNulls(objTools.DLookup("ccKala", "tblAN_Kala", "CodeKala = N'" & txtCodeKala.Text.Trim & "'"), 0)
            If ccKala = 0 Then
                Me.lblNameKala.Text = "-----"
                Me.txtCodeKala.Tag = ""
                Me.lblTedadMojod.Text = ""
                Me.txtFee.Text = ""
                Exit Sub
            End If

            Dim TedadP As Double = objTools.ConvertNulls(objTools.DLookup("Tedad3", "tblFO_PishFaktorSatr", "ccPishFaktorTitr = " & PK & " AND ccKala = " & ccKala), 0)
            If TedadP = 0 Then
                Me.lblNameKala.Text = "-----"
                Me.txtCodeKala.Tag = ""
                Me.lblTedadMojod.Text = ""
                Me.txtFee.Text = ""
                Exit Sub
            End If

            Dim TedadF As Double = objTools.ConvertNulls(objTools.DSum("Tedad3", "tblFO_FaktorSatr", "ccFaktorTitr IN (SELECT ccFaktorTitr FROM tblFO_Faktor WHERE ccPishFaktor = " & PK & ") AND ccKala = " & ccKala), 0)
            Dim TedadM As Double = objTools.ConvertNulls(objTools.DSum("Tedad", "sales.ElamMarjoeeSatr_PishFaktor AS a LEFT OUTER JOIN sales.ElamMarjoee_PishFaktor AS b ON a.ccElamMarjoee_PishFaktor = b.ccElamMarjoee_PishFaktor", "ccKala = " & ccKala & " AND b.ccPishFaktor = " & PK), 0)

            If TedadP - TedadF - TedadM = 0 Then
                Exit Sub
            End If

            Me.lblNameKala.Text = objTools.ConvertNulls(objTools.DLookup("NameKala", "tblAN_Kala", "ccKala = " & ccKala), "")
            Me.txtCodeKala.Tag = ccKala
            Me.lblTedadMojod.Text = TedadP - TedadF - TedadM
            Me.txtFee.Text = objTools.DLookup("Fee", "tblFO_PishFaktorSatr", "ccPishFaktorTitr = " & PK & " AND ccKala = " & ccKala)
            Me.mskFeeMiyangin.Text = ObjCode.GetMablaghMiangin(ccKala, Val(GridEXTitr.CurrentRow.Cells("ccAnbar").Text.Replace(",", "")))

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> txtCodeKala_TextChanged")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> txtCodeKala_TextChanged")
        End Try
    End Sub
    Private Sub GridEXTitr_DoubleClick(sender As Object, e As EventArgs) Handles GridEXTitr.DoubleClick
        If Not objSec.HaveUserPermission(SN, UD_Dll.Security.ePermissions.Update) Then Exit Sub
        If dvTitr.Count = 0 Then
            Exit Sub
        End If

        If objTools.DLookup("sVazeiat", "Sales.ElamMarjoee_PishFaktor", "ccElamMarjoee_PishFaktor = " & Val(GridEXTitr.CurrentRow.Cells("ccElamMarjoee_PishFaktor").Text.Replace(",", ""))) = 1 Then
            MsgBox("این مرجوعی تایید شده است . امکان ویرایش آن وجود ندارد .", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, "خطا")
            Exit Sub
        End If

        Mode = UD_Dll.Enums.GL_ModeForms.UpdateRecord
        SetForm()
    End Sub
    Private Sub btnRemoveTitr_Click(sender As Object, e As EventArgs) Handles btnRemoveTitr.Click
        If Not objSec.HaveUserPermission(SN, UD_Dll.Security.ePermissions.Delete) Then Exit Sub
        If dvTitr.Count = 0 Then
            Exit Sub
        End If

        If GridEXTitr.CurrentRow.RowType = Janus.Windows.GridEX.RowType.FilterRow Then
            Exit Sub
        End If

        If objTools.DLookup("sVazeiat", "Sales.ElamMarjoee_PishFaktor", "ccElamMarjoee_PishFaktor = " & Val(GridEXTitr.CurrentRow.Cells("ccElamMarjoee_PishFaktor").Text.Replace(",", ""))) = 1 Then
            MsgBox("این مرجوعی تایید شده است . امکان حـــذف آن وجود ندارد .", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, "خطا")
            Exit Sub
        End If

        If MsgBox("آیا مرجوعی انتخاب شده حـــذف گردد ؟", MsgBoxStyle.Question + MsgBoxStyle.YesNo + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, "حـذف") = MsgBoxResult.Yes Then
            objTools.DDelete("Sales.ElamMarjoee_PishFaktor", "ccElamMarjoee_PishFaktor = " & Val(GridEXTitr.CurrentRow.Cells("ccElamMarjoee_PishFaktor").Text.Replace(",", "")))
            MsgBox("حـــذف مرجوعی مورد نظر با موفقیت انجام شد .", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, "")

            SearchTitr()

        End If
    End Sub
    Private Sub btnRemoveSatr_Click(sender As Object, e As EventArgs) Handles btnRemoveSatr.Click
        If Not objSec.HaveUserPermission(SN, UD_Dll.Security.ePermissions.DeleteSatr) Then Exit Sub
        If dvTitr.Count = 0 Then
            Exit Sub
        End If

        If dvSatr.Count = 0 Then
            Exit Sub
        End If

        If GridEXSatr.CurrentRow.RowType = Janus.Windows.GridEX.RowType.FilterRow Then
            Exit Sub
        End If

        If objTools.DLookup("sVazeiat", "Sales.ElamMarjoee_PishFaktor", "ccElamMarjoee_PishFaktor = " & Val(GridEXTitr.CurrentRow.Cells("ccElamMarjoee_PishFaktor").Text.Replace(",", ""))) = 1 Then
            MsgBox("این مرجوعی تایید شده است . امکان حـــذف کالاهای آن وجود ندارد .", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, "خطا")
            Exit Sub
        End If

        If MsgBox("آیا سطر کالایی انتخاب شده حـــذف گردد ؟", MsgBoxStyle.Question + MsgBoxStyle.YesNo + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, "حـذف") = MsgBoxResult.Yes Then
            objTools.DDelete("Sales.ElamMarjoeeSatr_PishFaktor", "ccElamMarjoeeSatr_PishFaktor = " & Val(GridEXSatr.CurrentRow.Cells("ccElamMarjoeeSatr_PishFaktor").Text.Replace(",", "")))
            MsgBox("حـــذف سطر کالایی مورد نظر با موفقیت انجام شد .", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, "")

            SearchSatr()

        End If
    End Sub
    Private Sub btnTaeed_Click(sender As Object, e As EventArgs) Handles btnTaeed.Click
        Dim CountRow As Integer = 0
        Dim CountTaeed As Integer = 0

        For i As Integer = 0 To GridEXTitr.GetCheckedRows.Length - 1
            If GridEXTitr.GetCheckedRows(i).Cells("Taeed").Value = True Then
                CountRow += 1
            End If
        Next

        If CountRow = 0 Then
            MsgBox("حداقل یک مرجوعی باید انتخاب شود .", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, "")
            Exit Sub
        End If


        If MsgBox("آیا مرجوعی های انتخاب شده تایید گردند ؟", MsgBoxStyle.Question + MsgBoxStyle.YesNo + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, "تایید") = MsgBoxResult.Yes Then
            For i As Integer = 0 To GridEXTitr.GetCheckedRows.Length - 1
                If objTools.DLookup("sVazeiat", "Sales.ElamMarjoee_PishFaktor", "ccElamMarjoee_PishFaktor = " & Val(GridEXTitr.GetCheckedRows(i).Cells("ccElamMarjoee_PishFaktor").Text.Replace(",", ""))) = 1 Then
                    MsgBox("مرجوعی شماره " & Val(GridEXTitr.GetCheckedRows(i).Cells("ShomarehElamMarjoee_PishFaktor").Text.Replace(",", "")) & " تایید شده است ، امکان تایید مجدد آن وجود ندارد .", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, "")
                Else
                    If objTools.DCount("ccKala", "Sales.ElamMarjoeeSatr_PishFaktor", "ccElamMarjoee_PishFaktor = " & Val(GridEXTitr.GetCheckedRows(i).Cells("ccElamMarjoee_PishFaktor").Text.Replace(",", ""))) = 0 Then
                        MsgBox("مرجوعی شماره " & Val(GridEXTitr.GetCheckedRows(i).Cells("ShomarehElamMarjoee_PishFaktor").Text.Replace(",", "")) & " به علت نداشتن سطر کالا تایید نمی شود .", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, "")
                    Else
                        objTools.DUpdate("sVazeiat", "Sales.ElamMarjoee_PishFaktor", "1", "ccElamMarjoee_PishFaktor = " & Val(GridEXTitr.GetCheckedRows(i).Cells("ccElamMarjoee_PishFaktor").Text.Replace(",", "")))
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
    Private Sub GridEXSatr_DoubleClick(sender As Object, e As EventArgs) Handles GridEXSatr.DoubleClick
        If Not objSec.HaveUserPermission(SN, UD_Dll.Security.ePermissions.UpdateSatr) Then Exit Sub
        If dvTitr.Count = 0 Then
            Exit Sub
        End If

        If objTools.DLookup("sVazeiat", "Sales.ElamMarjoee_PishFaktor", "ccElamMarjoee_PishFaktor = " & Val(GridEXTitr.CurrentRow.Cells("ccElamMarjoee_PishFaktor").Text.Replace(",", ""))) = 1 Then
            MsgBox("این مرجوعی تایید شده است . امکان ویرایش آن وجود ندارد .", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, "خطا")
            Exit Sub
        End If

        Mode = UD_Dll.Enums.GL_ModeForms.UpdateRow
        SetForm()
    End Sub
    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        SearchTitr()
    End Sub
    Private Sub cmbVazeiat_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbVazeiat.SelectedIndexChanged
        SearchTitr()
    End Sub

    Private Sub txtShomarehPishFaktor_TextChanged(sender As Object, e As EventArgs) Handles txtShomarehPishFaktor.TextChanged
        Dim ccPishFaktor As Integer = 0
        ccPishFaktor = objTools.ConvertNulls(objTools.DLookup("ccPishFaktorTitr", "tblFO_PishFaktor", "CodeMahal = " & CodeMahalFaal & " AND CodeDoreh = " & cmbsCodeDorehPishFaktor.SelectedValue & " AND PishFaktorShomareh = " & IIf(txtShomarehPishFaktor.Text.Trim = "", 0, txtShomarehPishFaktor.Text.Trim)), 0)

        If ccPishFaktor <> 0 Then
            If ValidPishFaktor(ccPishFaktor) <> 0 Then
                Dim ccMoshtary As Integer = objTools.DLookup("ccMoshtary", "tblFO_PishFaktor", "ccPishFaktorTitr = " & ccPishFaktor)
                txtShomarehPishFaktor.Tag = ccPishFaktor
                lblNameMoshtary.Text = ""
                lblNameMoshtary.Text = objTools.DLookup("LTRIM(RTRIM(STR(CodeMoshtary))) + '-' + NameMoshtary", "tblFO_Moshtary", "ccMoshtary = " & ccMoshtary)
                cmbAnbar.SelectedValue = objTools.DLookup("ccAnbar", "tblFO_PishFaktor", "ccPishFaktorTitr = " & ccPishFaktor)
                FlgValidPishFaktor = True
            Else
                txtShomarehPishFaktor.Tag = 0
                lblNameMoshtary.Text = ""
                cmbAnbar.SelectedIndex = -1
                cmbAnbar.SelectedIndex = -1
                FlgValidPishFaktor = False
            End If
        Else
            txtShomarehPishFaktor.Tag = 0
            lblNameMoshtary.Text = ""
            cmbAnbar.SelectedIndex = -1
            cmbAnbar.SelectedIndex = -1
        End If
    End Sub
    Private Function ValidPishFaktor(ByVal ccPishFaktor As Integer) As Integer
        ValidPishFaktor = 0

        Dim cnSQL As New SqlConnection
        Dim cmSQL As New SqlCommand
        Dim strSQL As String = ""

        Try
            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()

            strSQL = "Sales.spElamMarjoee_PishFaktor_IsValidMandehDar"

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            cmSQL.Parameters.AddWithValue("CodeMahal", CodeMahalFaal)
            'cmSQL.Parameters.AddWithValue("CodeDoreh", CodeDoreh)
            cmSQL.Parameters.AddWithValue("ccPishFaktor", ccPishFaktor)
            cmSQL.Parameters.AddWithValue("CountRecord", ValidPishFaktor)
            cmSQL.Parameters("CountRecord").Direction = ParameterDirection.Output

            cmSQL.ExecuteNonQuery()

            ValidPishFaktor = cmSQL.Parameters("CountRecord").Value

            cmSQL = Nothing
            cnSQL.Close()

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> ValidPishFaktor")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> ValidPishFaktor")
        End Try
    End Function

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        If Not objSec.HaveUserPermission(SN, UD_Dll.Security.ePermissions.Preview) Then Exit Sub
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
                    .Item("Moshtary").Text = "{mydata.NameTablo}"
                    .Item("Ostan").Text = "{mydata.Ostan}"
                    .Item("NameTablo").Text = "{mydata.NameTablo}"
                    .Item("MoshtaryMobile").Text = "{mydata.MoshtaryMobile}"
                    .Item("txtVahedShomaresh").Text = "{mydata.txtVahed}"
                    .Item("mkol").Text = "{mydata.mkol3}"
                End If
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
                .Item("JamKol").Text = "{mydata.JamKol}"
                .Item("MKolMaliatAvarez").Text = "{mydata.MKolMaliatAvarez}"
                '.Item("MablaghAvarez").Text = "{mydata.MablaghAvarez}"
                '.Item("MablaghMalyat").Text = "{mydata.MablaghMalyat}"
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
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->Print PishFaktor")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->Print PishFaktor")
        End Try
    End Sub
    Private Sub CalculateMalyatAvarez(ByVal ccPishFaktor As Integer)
        Dim strSQL As String = ""
        Dim cn As New SqlConnection(ConnectionString)
        Dim da As SqlDataAdapter = Nothing
        Dim dt As New DataTable
        Dim cm As SqlCommand = Nothing
        Dim M As Double = 0
        Dim AvarezOneKala As Double = 0
        Dim MaliyatOneKala As Double = 0

        Dim TarikhFaktor As String = objTools.DLookup("PishFaktorTarikh", "tblFO_PishFaktor", "ccPishFaktortitr = " & ccPishFaktor)

        strSQL = "Select a.ccElamMarjoeeSatr_PishFaktor , ccKala , Fee , Tedad  from Sales.ElamMarjoeeSatr_PishFaktor as a left outer join Sales.ElamMarjoee_PishFaktor as b on a.ccElamMarjoee_PishFaktor=b.ccElamMarjoee_PishFaktor  WHERE b.ccPishFaktor = " & ccPishFaktor & " AND b.ccElamMarjoee_PishFaktor = " & Val(GridEXTitr.CurrentRow.Cells("ccElamMarjoee_PishFaktor").Text.Replace(",", ""))
        cn.Open()

        da = New SqlDataAdapter(strSQL, cn)
        Try

            da.Fill(dt)

            For Each dr As DataRow In dt.Rows

                AvarezOneKala = objTools.DLookup("(MablaghAvarez) / Tedad3", "tblFO_PishFaktorSatr", "ccPishFaktorTitr = " & ccPishFaktor & " AND ccKala = " & dr("ccKala"))
                MaliyatOneKala = objTools.DLookup("(MablaghMalyat) / Tedad3", "tblFO_PishFaktorSatr", "ccPishFaktorTitr = " & ccPishFaktor & " AND ccKala = " & dr("ccKala"))
                M = (dr("Tedad") * AvarezOneKala) + (dr("Tedad") * MaliyatOneKala)

                strSQL = "UPDATE Sales.ElamMarjoeeSatr_PishFaktor SET "
                strSQL &= " MablaghMaliatAvarez = " & Math.Round(M, 0)
                strSQL &= " WHERE ccElamMarjoeesatr_PishFaktor = " & dr("ccElamMarjoeesatr_PishFaktor")
                strSQL &= " And ccKala In (Select ccKala From tblAN_Kala where MashmuleMaliyat = 1 OR MashmuleAvarez = 1)"
                'strSQL &= " And ccPishFaktor in ( SELECT ccPishFaktorTitr FROM tblFO_PishFaktor WHERE ccPishFaktorTitr = " & ccPishFaktor & " AND JamMablaghAvarez <> 0)"

                cm = New SqlCommand(strSQL, cn)
                cm.ExecuteNonQuery()

                
                If objTools.DLookup("MkolTakhfifMalyatAvarez", "tblFO_PishFaktor", "ccPishFaktorTitr = " & ccPishFaktor) <> 0 Then
                    If IsMalyatAvarezTakhfif Then
                        strSQL = "UPDATE Sales.ElamMarjoeeSatr_PishFaktor SET "
                        strSQL &= " TakhfifMalyatAvarez = " & Math.Round(M, 0)
                        strSQL &= " WHERE ccElamMarjoeesatr_PishFaktor = " & dr("ccElamMarjoeesatr_PishFaktor")
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
End Class
