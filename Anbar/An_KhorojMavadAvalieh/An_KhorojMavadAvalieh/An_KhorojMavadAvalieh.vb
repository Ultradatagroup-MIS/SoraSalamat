
Imports System.Data.SqlClient
Imports System.Data.SqlClient.SqlConnection
Imports System.Data.Common
Imports System.Data


Public Class An_KhorojMavadAvalieh


#Region "Variable AND Constant Declration"
    Const cntCodeSubSystem As Long = 1000124
    Const TitrGridSize As Integer = 211
    Const SatrGridSize As Integer = 209
    Const TitrOrgSize As Integer = 244
    Const SatrOrgSize As Integer = 280

    Const FormTableName = "tblAN_KasrMavadAvalieh"

    Dim Mode As UD_Dll.Enums.GL_ModeForms = UD_Dll.Enums.GL_ModeForms.AddNewRecord
    Dim cmTitr As CurrencyManager
    Dim cmSatr As CurrencyManager
    Dim dvTitr As DataView
    Dim dvSatr As DataView
    Dim tCodeCounter As Long
    Dim ErrPro As New ErrorProvider
    Dim dsForm As New DataSet
    Dim dvForm As DataView
    Dim txtCaption As String
    Private WithEvents BS As New UD_Dll.PassString
    Private SN As Integer
    Dim flg As Boolean = False
    Dim FirstInsert As Boolean = False
    Dim tpos As Integer
#End Region
#Region "Form Event Code"
    Private Sub frmAN_kdxKasrEzafehAnbar_Closed(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Closed
        ' "Receive" parameter is the caption of destination window
        'Dim hwnd As Long = UD_Dll.Code.FindWindow(vbNullString, objCode.GetNameSherkat)
        'If hwnd <> 0 Then
        '    BS.PostString(hwnd, &H400, 0, txtCaption)
        'End If

        dsForm = Nothing
        dvForm = Nothing
    End Sub
    Private Sub frmAN_KasrEzafehAnbar_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles MyBase.Paint
        Try
            Me.TopMost = True
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->frmSanadHesabdary_Paint")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->frmSanadHesabdary_Paint")
        End Try

    End Sub
    Private Sub frmAN_KasrEzafehAnbar_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            SetParameter()
            SN = objSec.GetSecurityNumber(cntCodeSubSystem, UserName)

            Mode = UD_Dll.Enums.GL_ModeForms.None
            flg = False
            LoadCombo()
            flg = True
            ClearForm()
            Search(True)
            SetFormObject()
            objTools.ChangeKbd(UD_Dll.mdlUtility.KBD.Farsi)
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->frm_Load")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->frm_Load")
        End Try
    End Sub
    Private Sub btnFTitr_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles btnFTitr.Paint
        Try
            Dim Lines As Point() = {New Point(4, 15), New Point(18, 15), New Point(11, 7)}
            e.Graphics.FillPolygon(Brushes.Black, Lines)
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->btnFTitr_Paint")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->btnFTitr_Paint")
        End Try

    End Sub
    Private Sub btnFSatr_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles btnFSatr.Paint
        Try
            Dim Lines As Point() = {New Point(4, 15), New Point(18, 15), New Point(11, 7)}
            e.Graphics.FillPolygon(Brushes.Black, Lines)
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->btnFSatr_Paint")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->btnFSatr_Paint")
        End Try

    End Sub
    Private Sub btnLTitr_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles btnLTitr.Paint
        Try
            Dim Lines As Point() = {New Point(5, 7), New Point(17, 7), New Point(11, 14)}
            e.Graphics.FillPolygon(Brushes.Black, Lines)
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->btnLTitr_Paint")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->btnLTitr_Paint")
        End Try

    End Sub
    Private Sub btnLSatr_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles btnLSatr.Paint
        Try
            Dim Lines As Point() = {New Point(5, 7), New Point(17, 7), New Point(11, 14)}
            e.Graphics.FillPolygon(Brushes.Black, Lines)
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->btnLSatr_Paint")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->btnLSatr_Paint")
        End Try

    End Sub
    Private Sub dbgTitr_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles dbgTitr.DoubleClick
        Try
            Mode = UD_Dll.Enums.GL_ModeForms.UpdateRecord
            SetFormTitrData()
            SetFormObject()
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->dbgTitr_DoubleClick")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->dbgTitr_DoubleClick")
        End Try

    End Sub
    Private Sub dbgSatr_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles dbgSatr.DoubleClick
        Try
            If Mode = UD_Dll.Enums.GL_ModeForms.None Then
                Mode = UD_Dll.Enums.GL_ModeForms.UpdateRow
                SetFormSatrData(cmSatr.Position)
                SetFormObject()
            End If
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->dbgSatr_DoubleClick")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->dbgSatr_DoubleClick")
        End Try
    End Sub
    'Private Sub txtFee_TextChange(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtFee.TextChange
    '    If Me.txtTedad.Text.Length <> 0 Then
    '        Me.txtRjKol.Text = (CDbl(txtTedad.Text) * CDbl(txtFee.Text).ToString)
    '    End If
    'End Sub
    'Private Sub txtTedad_TextChange(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtTedad.Click
    '    If Me.txtTedad.Text.Length <> 0 Then
    '        Me.txtRjKol.Text = (CDbl(txtTedad.Text) * CDbl(txtFee.Text).ToString)
    '    End If
    'End Sub
    Private Sub txtRadif_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtRadif.KeyPress
        Try
            If (Asc(e.KeyChar()) < 48 Or Asc(e.KeyChar()) > 57) And (Asc(e.KeyChar()) <> 8) Then
                e.Handled = True
            End If
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->txtRadif_KeyPress")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->txtRadif_KeyPress")
        End Try
    End Sub
    Private Sub txtShomarehS_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtShomarehForm.KeyPress, txtShomarehS.KeyPress
        If (Asc(e.KeyChar()) < 48 Or Asc(e.KeyChar()) > 57) And (Asc(e.KeyChar()) <> 8) Then
            e.Handled = True
        End If
    End Sub
    Private Sub txtCodeKala_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCodeKala.TextChanged
        If Mode = UD_Dll.Enums.GL_ModeForms.UpdateRecord Then Exit Sub
        If Trim(Me.txtCodeKala.Text) = "" Then Exit Sub
        If Not flg Then Exit Sub


        Me.lblNameKala.Text = objTools.ConvertNulls(objTools.DLookup("NameKala", "tblAN_Kala", "CodeKala='" & Me.txtCodeKala.Text & "'"), "")
        Me.txtCodeKala.Tag = objTools.ConvertNulls(objTools.DLookup("ccKala", "tblAn_Kala", "CodeKala='" & Me.txtCodeKala.Text & "'"), 0)

        LoadVahedKala()
        Me.cmbVahedKala.SelectedValue = objTools.ConvertNulls(objTools.DLookup("sVahedeShomaresh", "tblAN_Kala", "ccKala = " & IIf(txtCodeKala.Text.Length = 0, 0, txtCodeKala.Tag)), 0)


        txtFee.Enabled = True

        'txtFee.Text = CType(objCode.GetMablaghMiangin(txtCodeKala.Tag, dvTitr(cmTitr.Position)("ccAnbar")), String)
        'If txtFee.Text = 0 Then
        '    txtFee.Text = CType(objCode.GetMablaghKharid_BeduneTaminKonandeh(txtCodeKala.Tag, TarikhEmrooz), String)
        'End If

    End Sub
    Private Sub txtCodeKala_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCodeKala.KeyPress
        Try
            If (Asc(e.KeyChar()) < 48 Or Asc(e.KeyChar()) > 57) And (Asc(e.KeyChar()) <> 8) Then
                e.Handled = True
            End If
            If e.KeyChar = Chr(Keys.Space) Then
                Dim objKala As New Forms_dll.frmAN_KalaSearch
                Dim StrSql As String

                StrSql = "Select  b.CodeKala,b.NameKala,b.ccKala,a.txtsVahedeShomaresh,a.sVahedeShomaresh,NameBrand,Radif from qryAN_Kala a left outer join qryan_kalaanbar b "
                StrSql = StrSql & " on a.cckala=b.cckala  Where   b.ccanbar = " & dvTitr(cmTitr.Position)("ccanbar")

                MultiSelection = False
                SearchItem = "CodeKala"
                objKala.SetForm(StrSql)
                objKala.ShowDialog()

                'If txtCodeKala.Text.Length <> 0 Then
                '    objKala.tcodeKala = txtCodeKala.Text
                'End If

                Me.txtCodeKala.Tag = objKala.tccKala
                Me.txtCodeKala.Text = objKala.tcodeKala
                Me.lblNameKala.Text = objKala.tNameKala
                Me.cmbVahedKala.Text = objKala.tVahedShomaresh
                MultiSelection = False
            End If
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->txtCodeMoshtaryS_KeyPress")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->txtCodeMoshtaryS_KeyPress")
        End Try

    End Sub
    Private Sub frmAN_kdxKasrEzafehAnbar_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.F1 Then
            Dim HelpWindow As New Forms_dll.frmGL_HelpWindow
            HelpWindow.CurrentCodeSubSystem = cntCodeSubSystem
            HelpWindow.Show()
            HelpWindow.TopMost = True
        End If
    End Sub
    Private Sub MaskSelect(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles _
    mskTarikh.Enter, mskTarikh.Click, mskAzTarikh.Enter, mskAzTarikh.Click, mskTaTarikh.Enter, mskTaTarikh.Click
        Try
            SendKeys.Send("{HOME}+{END}")
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->MaskSelect")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->MaskSelect")
        End Try
    End Sub
    Private Sub CheckIsNumeric(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles _
     txtCodeKala.KeyPress, txtFee.KeyPress, txtRadif.KeyPress, txtRjKol.KeyPress, txtShomarehForm.KeyPress, txtShomarehS.KeyPress, txtTedad.KeyPress
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
#End Region
#Region "Global Form Code"
    Private Sub LoadCombo()
        Try
            Dim Strsql As String
            Dim daSQL As SqlDataAdapter

            '===================== Anbar =============================================================='
            ' Load Combo tblAnbar
            Strsql = "Select codeAnbar,NameAnbar From qryAN_Anbar where CodeMahal=" & CodeMahalFaal & " AND NoeAnbar=4  AND "
            Strsql &= " Not exists (Select * From tblGL_SecurityData where namekarbar= '" & UserName & "' and CodeSubSystem = 633 and pk = qryAN_Anbar.CodeAnbar) "
            Strsql &= " order by NameAnbar "
            daSQL = New SqlDataAdapter(Strsql, ConnectionString)
            daSQL.Fill(dsForm, "tblAnbar")
            cmbAnbar.DataSource = Nothing
            cmbAnbar.Items.Clear()
            cmbAnbar.DataSource = dsForm.Tables("tblAnbar").DefaultView
            cmbAnbar.DisplayMember = "NameAnbar"
            cmbAnbar.ValueMember = "codeAnbar"


            ' Load Combo tblTolidkonnandeh
            Strsql = "Select NameTolidKonandeh,ccTolidKonandeh From tblFO_tolidKonandeh "
            Strsql &= " where Not exists (Select * From tblGL_SecurityData where namekarbar= '" & UserName & "' and CodeSubSystem = 654 and pk = tblFO_TolidKonandeh.ccTolidKonandeh) order by ccTolidKonandeh"
            daSQL = New SqlDataAdapter(Strsql, ConnectionString)
            daSQL.Fill(dsForm, "tblTolidKonandeh")
            cmbTolidkonandeh.DataSource = Nothing
            cmbTolidkonandeh.Items.Clear()
            cmbTolidkonandeh.DataSource = dsForm.Tables("tblTolidKonandeh").DefaultView
            cmbTolidkonandeh.DisplayMember = "NameTolidKonandeh"
            cmbTolidkonandeh.ValueMember = "ccTolidKonandeh"


            daSQL = Nothing
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->LoadCombo")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->LoadCombo")
        End Try
    End Sub
    Private Sub LoadVahedKala()
        Dim Strsql As String
        Dim daSQL As SqlDataAdapter
        Strsql = "Select * From qryAN_KalaVahedShomareshAll Where ccKala = " & txtCodeKala.Tag
        daSQL = New SqlDataAdapter(Strsql, ConnectionString)
        If dsForm.Tables.Contains("tblVahedKala") Then
            dsForm.Tables.Remove("tblVahedKala")
        End If
        daSQL.Fill(dsForm, "tblVahedKala")
        cmbVahedKala.DataSource = dsForm.Tables("tblVahedKala").DefaultView
        cmbVahedKala.DisplayMember = "Sharh"
        cmbVahedKala.ValueMember = "sVahedeShomaresh"
        daSQL = Nothing
    End Sub
    Private Sub SetFormTitrData()
        Try
            If dvTitr.Count = 0 Or cmTitr.Position = -1 Then
                Mode = UD_Dll.Enums.GL_ModeForms.None
                Exit Sub
            End If

            Dim drvTemp As DataRowView
            drvTemp = dvTitr(cmTitr.Position)

            txtShomarehForm.Text = drvTemp("ShomarehForm")
            mskTarikh.Text = drvTemp("TarikhForm")
            cmbAnbar.SelectedValue = drvTemp("ccAnbar")
            cmbTolidkonandeh.SelectedIndex = drvTemp("NoeForm")
            txtSharh.Text = drvTemp("Sharh")

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->SetFormTitrData")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->SetFormTitrData")
        End Try

    End Sub
    Private Sub ClearForm()
        Try
            mskAzTarikh.Text = ""
            mskTaTarikh.Text = ""

            txtShomarehForm.Text = ""
            mskTarikh.Text = TarikhEmrooz
            If objCode.CheckCompany = 24 Or objCode.CheckCompany = 1 Then
                mskTarikh.Enabled = True
            End If
            cmbAnbar.SelectedIndex = -1
            cmbAnbar.SelectedIndex = -1
            cmbTolidkonandeh.SelectedIndex = -1
            cmbTolidkonandeh.SelectedIndex = -1
            txtSharh.Text = ""

            txtRadif.Text = ""

            txtCodeKala.Tag = ""
            txtCodeKala.Text = ""
            lblNameKala.Text = ""

            txtTedad.Text = ""
            cmbVahedKala.SelectedIndex = -1
            cmbVahedKala.SelectedIndex = -1

            txtFee.Text = 0
            txtRjKol.Text = 0

            ErrPro.Dispose()

            objCode.UserName = UserName
            'Select Case objCode.CheckEnableFee()
            '    Case UD_Dll.Enums.GL_SysConfigEnableFee.Faal
            '        Me.txtFee.Enabled = True
            '    Case UD_Dll.Enums.GL_SysConfigEnableFee.NoFaal
            '        Me.txtFee.Enabled = False
            'End Select
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->ClearForm")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->ClearForm")
        End Try
    End Sub
    Private Sub Search(ByVal WithCriteria As Boolean, Optional ByVal withMSG As Boolean = True)
        Try

            Dim StrSql As String
            Dim WhereStr As String

            StrSql = "SELECT * "
            StrSql = StrSql & " FROM qryAN_KasrMavadAvalieh " & " Where CodeMahal = " & CodeMahalFaal & " AND "
            StrSql = StrSql & " CodeDoreh = '" & CodeDoreh & "' AND "
            WhereStr = ""
            If WithCriteria Then
                If txtShomarehS.Text.Length > 0 Then
                    WhereStr = "ShomarehForm = " & txtShomarehS.Text & " AND "
                End If
                If mskAzTarikh.Text <> "" Then
                    WhereStr &= "TarikhForm >= '" & mskAzTarikh.Text & "' AND "
                End If
                If mskTaTarikh.Text <> "" Then
                    WhereStr &= "TarikhForm <= '" & mskTaTarikh.Text & "' AND "
                End If
            End If
            'WhereStr &= "1 = 1 AND sVazeiat <>" & UD_Dll.Enums.AN_VazeiatKasrMavadAvalieh.BedoneAmalyat
            WhereStr &= "sVazeiat = " & UD_Dll.Enums.AN_VazeiatKasrMavadAvalieh.BedoneAmalyat
            StrSql = StrSql & WhereStr
            StrSql &= " AND Not exists (Select * From tblGL_SecurityData where namekarbar= '" & UserName & "' and CodeSubSystem = 633 and pk = qryAN_KasrMavadAvalieh.ccAnbar) "

            RefreshTitrdata(StrSql, withMSG)

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->Search")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->Search")
        End Try

    End Sub
    Private Sub RefreshTitrdata(ByVal strSql As String, Optional ByVal WithMSG As Boolean = True)
        Try
            Dim daSQL As SqlDataAdapter

            If dsForm.Tables.Contains("HETitr") Then
                dsForm.Tables.Remove("HETitr")
            End If
            daSQL = New SqlDataAdapter(strSql, ConnectionString)
            daSQL.Fill(dsForm, "HETitr")

            dvTitr = New DataView(dsForm.Tables("HETitr"), "", "ccKasr DESC", DataViewRowState.CurrentRows)
            dvTitr.AllowNew = False
            dvTitr.AllowDelete = False
            dvTitr.AllowEdit = False

            daSQL = Nothing

            dbgTitr.DataSource = Nothing
            dbgTitr.DataSource = dvTitr
            BoundCurrencyManagerTitr()
            SetTitrButton()
            If dvTitr.Count = 0 Then
                dbgSatr.DataSource = Nothing
            End If
            SetGridStyle()
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->RefreshTitrdata")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->RefreshTitrdata")
        End Try
    End Sub
    Private Sub SetGridStyle()
        Try
            Dim tableStyleTitr As New DataGridTableStyle
            tableStyleTitr.MappingName = "HETitr"
            tableStyleTitr.RowHeaderWidth = 20
            tableStyleTitr.AllowSorting = False
            dbgTitr.BorderStyle = BorderStyle.Fixed3D


            Dim TextCol99 As New DataGridTextBoxColumn
            With TextCol99
                .MappingName = "txtVazeiat"
                .HeaderText = "وضعیت"
                .Width = 100
                .Alignment = HorizontalAlignment.Center
            End With
            tableStyleTitr.GridColumnStyles.Add(TextCol99)

            Dim TextCol01 As New DataGridTextBoxColumn
            With TextCol01
                .MappingName = "ShomarehForm"
                .HeaderText = "شماره فرم"
                .Width = 110
                .Alignment = HorizontalAlignment.Center
            End With
            tableStyleTitr.GridColumnStyles.Add(TextCol01)

            Dim TextCol02 As New DataGridTextBoxColumn
            With TextCol02
                .MappingName = "TarikhFormSlash"
                .HeaderText = "تاريخ"
                .Width = 90
                .Alignment = HorizontalAlignment.Center
            End With
            tableStyleTitr.GridColumnStyles.Add(TextCol02)

            Dim TextCol00 As New DataGridTextBoxColumn
            With TextCol00
                .MappingName = "txtNoeForm"
                .HeaderText = "نوع فرم"
                .Width = 170
                .Alignment = HorizontalAlignment.Left
            End With
            tableStyleTitr.GridColumnStyles.Add(TextCol00)

            Dim TextCol1 As New DataGridTextBoxColumn
            With TextCol1
                .MappingName = "NameAnbar"
                .HeaderText = "نام انبار"
                .Width = 170
                .Alignment = HorizontalAlignment.Left
            End With
            tableStyleTitr.GridColumnStyles.Add(TextCol1)

            If objTools.DLookup("ShowGheymatInAnbar", "tblGL_SysConfig", "CodeMahal =" & CodeMahalFaal) <> 2 Then
                Dim TextCol0 As New DataGridTextBoxColumn
                With TextCol0
                    .MappingName = "JamMablagh"
                    .HeaderText = "قیمت کل"
                    .Width = 100
                    .Format = "###,###"
                    .Alignment = HorizontalAlignment.Left
                End With
                tableStyleTitr.GridColumnStyles.Add(TextCol0)
            End If

            Dim TextCol100 As New DataGridTextBoxColumn
            With TextCol100
                .MappingName = "Sharh"
                .HeaderText = "شرح"
                .Width = 170
                .Alignment = HorizontalAlignment.Left
            End With
            tableStyleTitr.GridColumnStyles.Add(TextCol100)

            dbgTitr.Visible = True
            dbgTitr.RowHeaderWidth = 20
            dbgTitr.AllowSorting = False
            dbgTitr.TableStyles.Clear()
            dbgTitr.TableStyles.Add(tableStyleTitr)
            '=========================================================================================='
            Dim tableStyleSatr As New DataGridTableStyle
            tableStyleSatr.MappingName = "HESatr"
            tableStyleSatr.RowHeaderWidth = 20
            tableStyleSatr.AllowSorting = True
            dbgSatr.BorderStyle = BorderStyle.Fixed3D

            Dim TextCol25 As New DataGridTextBoxColumn
            With TextCol25
                .MappingName = "Radif"
                .HeaderText = "رديف"
                .Width = 50
                .ReadOnly = True
                .Alignment = HorizontalAlignment.Center
            End With
            tableStyleSatr.GridColumnStyles.Add(TextCol25)

            Dim TextCol35w As New DataGridTextBoxColumn
            With TextCol35w
                .MappingName = "CodeKala"
                .HeaderText = "کد کالا"
                .Width = 100
                .ReadOnly = True
                .Alignment = HorizontalAlignment.Left
            End With
            tableStyleSatr.GridColumnStyles.Add(TextCol35w)

            Dim TextCol35 As New DataGridTextBoxColumn
            With TextCol35
                .MappingName = "NameKala"
                .HeaderText = "نام کالا"
                .Width = 250
                .ReadOnly = True
                .Alignment = HorizontalAlignment.Left
            End With
            tableStyleSatr.GridColumnStyles.Add(TextCol35)

            Dim TextCol40 As New DataGridTextBoxColumn
            With TextCol40
                .MappingName = "txtVahed"
                .HeaderText = "واحد کالا"
                .Width = 100
                .ReadOnly = True
                .Alignment = HorizontalAlignment.Left
            End With
            tableStyleSatr.GridColumnStyles.Add(TextCol40)

            Dim TextCol45 As New DataGridTextBoxColumn
            With TextCol45
                .MappingName = "Tedad3"
                .HeaderText = "تعداد"
                .Width = 100
                .Format = "###,###.##"
                .ReadOnly = False
                .Alignment = HorizontalAlignment.Left
            End With
            tableStyleSatr.GridColumnStyles.Add(TextCol45)

            If objTools.DLookup("ShowGheymatInAnbar", "tblGL_SysConfig", "CodeMahal =" & CodeMahalFaal) <> 2 Then
                Dim TextCol50 As New DataGridTextBoxColumn
                With TextCol50
                    .MappingName = "MablaghKharid"
                    .HeaderText = "فی"
                    .Width = 100
                    .ReadOnly = False
                    .Format = "###,###"
                    .Alignment = HorizontalAlignment.Left
                End With
                tableStyleSatr.GridColumnStyles.Add(TextCol50)

                Dim TextCol65 As New DataGridTextBoxColumn
                With TextCol65
                    .MappingName = "MKol"
                    .HeaderText = "مبلغ کل"
                    .Format = "###,###"
                    .Width = 100
                    .ReadOnly = True
                    .Alignment = HorizontalAlignment.Left
                End With
                tableStyleSatr.GridColumnStyles.Add(TextCol65)
            End If

            dbgSatr.Visible = True
            dbgSatr.RowHeaderWidth = 20
            dbgSatr.AllowSorting = False
            dbgSatr.ReadOnly = False
            dbgSatr.TableStyles.Clear()
            dbgSatr.TableStyles.Add(tableStyleSatr)
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->SetGridStyle")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->SetGridStyle")
        End Try
    End Sub
    Private Function IsValidBeforSaveTitr(ByVal CheckField As String) As Boolean
        Try
            IsValidBeforSaveTitr = False
            If CheckField = "mskTarikh" Or CheckField = "All" Then
                If Len(mskTarikh.Text.ToString) <> 0 Then
                    If Not objTarikh.IsShDate(mskTarikh.Text.ToString) Then
                        mskTarikh.Focus()
                        Exit Function
                    End If
                    If mskTarikh.Text.Substring(0, 4) <> CodeDoreh Then
                        MsgBox("تاريخ با دوره مالی فعال يکی نيست.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
                        mskTarikh.Focus()
                        Exit Function
                    End If
                    If mskTarikh.Text > TarikhEmrooz Then
                        MsgBox("تاریخ وارد شده از تاریخ امروز جلوتر است.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
                        mskTarikh.Focus()
                        Exit Function
                    End If
                Else

                    ErrPro.SetError(Me.mskTarikh, "تاريخ را وارد کنيد.")
                    MsgBox("تاريخ را وارد کنيد.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
                    mskTarikh.Focus()
                    Exit Function
                End If
                ErrPro.SetError(Me.mskTarikh, "")
            End If

            If CheckField = "cmbAnbar" Or CheckField = "All" Then
                If cmbAnbar.SelectedIndex = -1 And cmbAnbar.SelectedValue = 0 Then
                    ErrPro.SetError(Me.cmbAnbar, "نام انبار را وارد کنید.")
                    MsgBox("نام انبار را وارد کنید.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
                    cmbAnbar.Focus()
                    Exit Function
                End If
                ErrPro.SetError(Me.cmbAnbar, "")
            End If

            'If CheckField = "cmbNoeForm" Or CheckField = "All" Then
            '    If cmbTolidkonandeh.SelectedIndex <= 0 Then
            '        ErrPro.SetError(Me.cmbTolidkonandeh, "نوع فرم را وارد کنید.")
            '        MsgBox("نوع فرم را وارد کنید.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
            '        cmbTolidkonandeh.Focus()
            '        Exit Function
            '    End If
            '    ErrPro.SetError(Me.cmbTolidkonandeh, "")
            'End If

            Return True
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->IsValidBeforSaveTitr")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->IsValidBeforSaveTitr")
        End Try
    End Function
    Private Function AddNewSanad() As Boolean
        Try
            AddNewSanad = False

            If Not IsValidBeforSaveTitr("All") Then
                Exit Function
                Return False
            End If

            cmTitr.Position = 0

            Dim cnSQL As SqlConnection
            Dim cmSQL As SqlCommand
            Dim strSQL As String

            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()

            strSQL = "Insert Into tblAN_KasrMavadAvalieh "
            strSQL &= "(CodeMahal,CodeDoreh,ShomarehForm,TarikhForm,ccTolidKonandeh , "
            strSQL &= "ccAnbar,Sharh,UserName,Tarikh,Saat,Date) Values "
            strSQL &= "(" & CodeMahalFaal & ","
            strSQL &= CodeDoreh & ","
            strSQL &= objTools.ConvertNulls(objTools.DMax("ShomarehForm", "tblAN_KasrMavadAvalieh", "CodeMahal = " & CodeMahalFaal & " AND CodeDoreh = " & CodeDoreh), 0) + 1 & ","
            strSQL &= "'" & mskTarikh.Text & "',"
            strSQL &= IIf(IsNothing(cmbTolidkonandeh.SelectedValue), 0, cmbTolidkonandeh.SelectedValue) & ","
            strSQL &= IIf(IsNothing(cmbAnbar.SelectedValue), 0, cmbAnbar.SelectedValue) & ","
            strSQL &= "'" & txtSharh.Text & "',"
            strSQL &= "'" & UserName & "',"
            strSQL &= "'" & TarikhEmrooz & "',"
            strSQL &= "'" & Format(TimeOfDay, "HH:mm:ss") & "',"
            strSQL &= " dbo.fnGL_ConvertToMiladi('" & mskTarikh.Text & "'))"

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.ExecuteNonQuery()
            tCodeCounter = objTools.GetScopeIdentity(cnSQL)

            objCode.SabteTaghirat(CodeMahalFaal, UD_Dll.Enums.GL_NoeTaghir.AddNewRecord, "tblAN_KasrMavadAvalieh", tCodeCounter, objTools.ConvertNulls(objTools.DMax("ShomarehForm", "tblAN_KasrMavadAvalieh", "CodeMahal = " & CodeMahalFaal & " AND CodeDoreh = " & CodeDoreh), 0), "ذخیره")


            cnSQL.Close()
            cmSQL = Nothing : cnSQL = Nothing

            Mode = UD_Dll.Enums.GL_ModeForms.None
            Return True
        Catch sqlExc As SqlException
            If (sqlExc.Number = 2627) Or (sqlExc.Number = 229) Then
                If Microsoft.VisualBasic.Left(sqlExc.Message, 1) = "I" Then
                    MsgBox("خطا در اضافه کردن رکورد جديد ,ثبت انجام نشد", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "خطاي بانک")
                ElseIf Microsoft.VisualBasic.Left(sqlExc.Message, 1) = "V" Then
                    MsgBox("خطا در اضافه کردن رکورد جديد ,رکورد در بانک موجود است ,ثبت انجام نشد", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "خطاي بانک")
                End If
            Else
                MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "خطاي بانک")
            End If
        Catch e As Exception
            MsgBox(e.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "خطا")
        End Try
    End Function
    Private Function UpdateSanad() As Boolean
        Try
            UpdateSanad = False
            If Not IsValidBeforSaveTitr("All") Then
                Exit Function
                Return False
            End If
            '
            Dim cnSQL As SqlConnection
            Dim cmSQL As SqlCommand
            Dim strSqlTitr
            objCode.SabteTaghirat(CodeMahalFaal, UD_Dll.Enums.GL_NoeTaghir.UpdateRecord, "tblAN_KasrMavadAvalieh", dvTitr(cmTitr.Position)("ccKasr"), dvTitr(cmTitr.Position)("ShomarehForm"), "بروز رساني ")

            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()
            strSqlTitr = "UPDATE tblAN_KasrMavadAvalieh SET " & _
                    " TarikhForm = '" & mskTarikh.Text & "'," & _
                    " ccAnbar = " & IIf(IsNothing(cmbAnbar.SelectedValue), 0, cmbAnbar.SelectedValue) & "," & _
                    " ShomarehForm = '" & txtShomarehForm.Text & "'," & _
                    " Sharh = '" & txtSharh.Text & "'," & _
                    " UserName = '" & UserName & "'," & _
                    " Tarikh = '" & TarikhEmrooz & "'," & _
                    " Date = dbo.fnGL_ConvertToMiladi('" & mskTarikh.Text & "')," & _
                    " Saat = '" & Format(TimeOfDay, "HH:mm:ss") & "'" & _
                    " Where ccKasr = " & dvTitr(cmTitr.Position)("ccKasr")

            cmSQL = New SqlCommand(strSqlTitr, cnSQL)
            cmSQL.ExecuteNonQuery()

            cnSQL.Close()
            cmSQL = Nothing : cnSQL = Nothing

            Mode = UD_Dll.Enums.GL_ModeForms.None
            Return True
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->UpdateSanad")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->UpdateSanad")
        End Try

    End Function
    Private Sub DeleteTitr()
        Try
            Dim cnSQL As SqlConnection
            Dim cmSQL As SqlCommand
            Dim strSQLRow As String
            Dim strSQLTitr As String

            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()


            strSQLRow = "ccKasr = " & dvTitr(cmTitr.Position)("ccKasr")

            If objTools.ConvertNulls(objTools.DLookup("ccKasr", "tblAN_KasrMavadAvaliehSatr", strSQLRow), "-1") = -1 Then
                strSQLTitr = "Delete From tblAN_KasrMavadAvalieh Where ccKasr =" & dvTitr(cmTitr.Position)("ccKasr")
                cmSQL = New SqlCommand(strSQLTitr, cnSQL)
                cmSQL.ExecuteNonQuery()
                cnSQL.Close()
                cmSQL = Nothing : cnSQL = Nothing

                objCode.SabteTaghirat(CodeMahalFaal, UD_Dll.Enums.GL_NoeTaghir.DeleteRecord, "tblAN_KasrMavadAvaliehSatr", dvTitr(cmTitr.Position)("ccKasr"), dvTitr(cmTitr.Position)("ShomarehForm"), "حذف ")



                Mode = UD_Dll.Enums.GL_ModeForms.None
            Else
                MsgBox("اين رکورد دارای کالا ميباشد.ابتدا آنها را حذف کنيد", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "خطاي بانک")
            End If
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->DeleteTitr")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->DeleteTitr")
        End Try
    End Sub
    Private Sub DeleteFull()
        Try
            If cmTitr.Position = -1 Then Exit Sub

            Dim cnSQL As SqlConnection
            Dim cmSQL As SqlCommand
            Dim strSQLRow As String
            Dim strSQLTitr As String

            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()

            strSQLRow = "Delete From tblAN_KasrMavadAvaliehSatr Where " & _
                " ccKasr = " & dvTitr(cmTitr.Position)("ccKasr")

            strSQLTitr = "Delete From tblAN_KasrMavadAvalieh Where ccKasr = " & dvTitr(cmTitr.Position)("ccKasr")

            cmSQL = New SqlCommand(strSQLRow, cnSQL)
            cmSQL.ExecuteNonQuery()

            cmSQL = New SqlCommand(strSQLTitr, cnSQL)
            cmSQL.ExecuteNonQuery()


            objCode.SabteTaghirat(CodeMahalFaal, UD_Dll.Enums.GL_NoeTaghir.DeleteRecord, "tblAN_KasrMavadAvalieh", dvTitr(cmTitr.Position)("ccKasr"), dvTitr(cmTitr.Position)("ShomarehForm"), "حذف ")


            cnSQL.Close()
            cmSQL = Nothing
            cnSQL = Nothing

            Mode = UD_Dll.Enums.GL_ModeForms.None
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->DeleteTitr")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->DeleteTitr")
        End Try
    End Sub
    Private Sub SetFormSatrData(ByVal Pos As Integer, Optional ByVal strField As String = "")
        Try
            If dvSatr.Count = 0 Or cmTitr.Position = -1 Then
                Mode = UD_Dll.Enums.GL_ModeForms.None
                Exit Sub
            End If

            Dim drvTemp As DataRowView
            drvTemp = dvSatr(Pos)
            If Mode = UD_Dll.Enums.GL_ModeForms.UpdateRow Then
                drvTemp = dvSatr(cmSatr.Position)
                txtRadif.Text = drvTemp("Radif")

                txtCodeKala.Tag = drvTemp("ccKala")
                txtCodeKala.Text = drvTemp("CodeKala")
                lblNameKala.Text = drvTemp("NameKala")

                txtTedad.Text = drvTemp("Tedad1")
                'txtFee.Text = drvTemp("MablaghKharid")
                cmbVahedKala.SelectedValue = drvTemp("sVahed")
            End If

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->SetFormSatrData")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->SetFormSatrData")
        End Try

    End Sub
    Private Function IsValidRow(ByVal chkField As String) As Boolean
        Try
            IsValidRow = False

            '''''''''''''''''''''' Radif ''''''''''''''''''''''''''''''''
            If (chkField = "txtRadif" Or chkField = "All") And Not (Mode = UD_Dll.Enums.GL_ModeForms.UpdateRow) Then
                If txtRadif.Text.Length = 0 Then
                    MsgBox("رديف را وارد کنيد.", MsgBoxStyle.OkOnly Or MsgBoxStyle.Information Or MsgBoxStyle.MsgBoxRtlReading, "خطا")
                    ErrPro.SetError(lblRadif, "رديف را وارد کنيد.")
                    txtRadif.Focus()
                    Exit Function
                Else
                    If objTools.DCount("Radif", "tblAN_KasrMavadAvaliehSatr", "ccKasr = " & dvTitr(cmTitr.Position)("ccKasr") & " AND Radif=" & txtRadif.Text) = 1 Then
                        MsgBox("شماره رديف  تکراری است.", MsgBoxStyle.OkOnly Or MsgBoxStyle.Information Or MsgBoxStyle.MsgBoxRtlReading, "خطا")
                        ErrPro.SetError(lblRadif, "شماره رديف تکراری است.")
                        txtRadif.Focus()
                        Exit Function
                    End If
                End If
                ErrPro.SetError(lblRadif, "")
            End If


            Dim Tedad As Double = objCode.ReturnTedad(txtCodeKala.Tag, Val(txtTedad.Text), Val(txtTedadBasteh.Text), Val(txtTedadKarton.Text))
            If chkField = "txtTedad" Or chkField = "All" Then
                If Val(Tedad) = 0 Then
                    ErrPro.SetError(Me.txtTedad, "تعداد کالا را وارد کنید.")
                    MsgBox("تعداد کالا را وارد کنید.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
                    txtTedad.Focus()
                    Exit Function
                End If
                ErrPro.SetError(Me.txtTedad, "")
            End If

            'If chkField = "txtTedad" Or chkField = "All" Then
            '    If txtTedad.Text.Length = 0 Or txtTedad.Text = "0" Then
            '        ErrPro.SetError(Me.txtTedad, "تعداد را وارد کنید.")
            '        MsgBox("تعداد را وارد کنید.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
            '        txtTedad.Focus()
            '        Exit Function
            '    End If
            '    ErrPro.SetError(Me.txtTedad, "")
            'End If


            Dim TedadKala As Double = objCode.ReturnTedad(txtCodeKala.Tag, Val(txtTedad.Text), Val(txtTedadBasteh.Text), Val(txtTedadKarton.Text))

            If Not IsValidMojodi(TedadKala, txtCodeKala.Tag) Then
                MsgBox("موجودی این کالا کافی نیست ", MsgBoxStyle.MsgBoxRtlReading Or MsgBoxStyle.MsgBoxRight Or MsgBoxStyle.Critical, "خطا")
                txtTedad.Focus()
                Exit Function
            End If




            If chkField = "txtCodeKala" Or chkField = "All" Then
                If Val(txtCodeKala.Tag) = 0 Then
                    ErrPro.SetError(txtCodeKala, "کد کالا را وارد کنيد.")
                    MsgBox("کد کالا را وارد کنيد.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
                    txtCodeKala.Focus()
                    Exit Function
                End If
                ErrPro.SetError(txtCodeKala, "")
            End If

            If chkField = "txtCodeKala" Or chkField = "All" Then
                If Mode = UD_Dll.Enums.GL_ModeForms.AddNewRow Then
                    If objTools.ConvertNulls(objTools.DCount("ccKala", "tblAN_KasrMavadAvaliehSatr", "ccKala = " & txtCodeKala.Tag & " AND ccKasr = " & dvTitr(cmTitr.Position)("ccKasr")), 0) >= 1 Then
                        ErrPro.SetError(txtCodeKala, " کالا تکراری است.")
                        MsgBox("کالای مورد نظر دراین سند صادر شده است", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "تایید")
                        txtCodeKala.Focus()
                        Exit Function
                    End If
                End If
                ErrPro.SetError(Me.txtCodeKala, "")
            End If

            'If chkField = "txtFee" Or chkField = "All" Then
            '    If Val(txtFee.Text) = 0 Then
            '        ErrPro.SetError(txtFee, "فی کالا را وارد کنيد.")
            '        MsgBox("فی کالا را وارد کنيد.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
            '        txtFee.Focus()
            '        Exit Function
            '    End If
            '    ErrPro.SetError(txtFee, "")
            'End If
           
            Return True

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->IsValidRow")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->IsValidRow")
        End Try
    End Function
    Private Sub AddNewRow()
        Try
            If Not IsValidRow("All") Then
                Exit Sub
            End If

            Dim cnSQL As SqlConnection
            Dim cmSQL As SqlCommand
            Dim strSQL As String
            Dim Tedad As Double = objCode.ReturnTedad(txtCodeKala.Tag, Val(txtTedad.Text), Val(txtTedadBasteh.Text), Val(txtTedadKarton.Text))

            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()
            strSQL = "INSERT INTO tblAN_KasrMavadAvaliehSatr ("
            strSQL += "ccKasr,"
            strSQL += "Radif,"
            strSQL += "ccKala,"
            strSQL += "Tedad1,"
            strSQL += "Tedad2,"
            strSQL += "Tedad3,"
            strSQL += "Tedad0,"
            strSQL += "TedadBasteh,"
            strSQL += "TedadKarton,"
            strSQL += "MablaghKharid"
            strSQL += ")"
            strSQL += " VALUES ("

            strSQL += dvTitr(cmTitr.Position)("ccKasr") & ","
            strSQL += txtRadif.Text & ","
            strSQL += txtCodeKala.Tag & ","
            'strSQL += IIf(Me.txtTedad.Text.Length = 0, 0, Me.txtTedad.Text) & ","
            'strSQL += IIf(Me.txtTedad.Text.Length = 0, 0, Me.txtTedad.Text) & ","
            'strSQL += IIf(Me.txtTedad.Text.Length = 0, 0, Me.txtTedad.Text) & ","
         

            strSQL += Tedad.ToString.Replace(",", ".") & ","
            strSQL += Tedad.ToString.Replace(",", ".") & ","
            strSQL += Tedad.ToString.Replace(",", ".") & ","


            strSQL += Val(txtTedad.Text) & ","
            strSQL += Val(txtTedadBasteh.Text) & ","
            strSQL += Val(txtTedadKarton.Text) & ","
            strSQL += IIf(Me.txtFee.Text.Length = 0, 0, Me.txtFee.Text.Replace(",", "."))


            strSQL += ")"
            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.ExecuteNonQuery()
            cnSQL.Close()
            cmSQL = Nothing : cnSQL = Nothing

            ClearForm()
            RefreshSatrData()
            Mode = UD_Dll.Enums.GL_ModeForms.None
            SetFormObject()

        Catch sqlExc As SqlException
            If (sqlExc.Number = 2627) Or (sqlExc.Number = 229) Then
                If Microsoft.VisualBasic.Left(sqlExc.Message, 1) = "I" Then
                    MsgBox("خطا در اضافه کردن رکورد جديد ,ثبت انجام نشد", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "خطاي بانک")
                ElseIf Microsoft.VisualBasic.Left(sqlExc.Message, 1) = "V" Then
                    MsgBox("خطا در اضافه کردن رکورد جديد ,رکورد در بانک موجود است ,ثبت انجام نشد", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "خطاي بانک")
                End If
            Else
                MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "خطاي بانک")
            End If
        Catch e As Exception
            MsgBox(e.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "خطا")
        End Try
    End Sub
    Private Sub UpdateRow()
        Try
            If Not IsValidRow("All") Then
                Exit Sub
            End If


            Dim cnSQL As SqlConnection
            Dim cmSQL As SqlCommand
            Dim strSQL As String

            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()
            strSQL = "UPDATE tblAN_KasrMavadAvaliehSatr SET " & _
                    " Radif = " & txtRadif.Text & "," & _
                    " ccKala = " & txtCodeKala.Tag & "," & _
                    " Tedad1 = " & IIf(Me.txtTedad.Text.Length = 0, 0, Me.txtTedad.Text) & "," & _
                    " Tedad2 = " & IIf(Me.txtTedad.Text.Length = 0, 0, Me.txtTedad.Text) & "," & _
                    " Tedad3 = " & IIf(Me.txtTedad.Text.Length = 0, 0, Me.txtTedad.Text) & "," & _
                    " Tedad0 = " & Val(txtTedad.Text.ToString.Replace(",", ".")) & "," & _
                    " TedadBasteh = " & Val(txtTedadBasteh.Text.ToString.Replace(",", ".")) & "," & _
                    " TedadKarton = " & Val(txtTedadKarton.Text.ToString.Replace(",", ".")) & "," & _
                    " MablaghKharid = " & IIf(Me.txtFee.Text.Length = 0, 0, Me.txtFee.Text.Replace(",", ".")) & _
                    "  WHERE ccKasrSatr = " & dvSatr(cmSatr.Position)("ccKasrSatr")
            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.ExecuteNonQuery()

            cnSQL.Close()
            cmSQL = Nothing
            cnSQL = Nothing

            Dim LastR As Integer = dvSatr(cmSatr.Position)("Radif") - 1

            ClearForm()

            RefreshSatrData()

            For i As Integer = 0 To cmSatr.Count - 1
                If LastR = dvSatr(i)("Radif") Then
                    cmSatr.Position = i
                    Exit For
                End If
            Next

            Mode = UD_Dll.Enums.GL_ModeForms.None
            SetFormObject()

        Catch sqlExc As SqlException
            If (sqlExc.Number = 2627) Or (sqlExc.Number = 229) Then
                If Microsoft.VisualBasic.Left(sqlExc.Message, 1) = "I" Then
                    MsgBox("خطا در اضافه کردن رکورد جديد ,ثبت انجام نشد", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "خطاي بانک")
                ElseIf Microsoft.VisualBasic.Left(sqlExc.Message, 1) = "V" Then
                    MsgBox("خطا در اضافه کردن رکورد جديد ,رکورد در بانک موجود است ,ثبت انجام نشد", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "خطاي بانک")
                End If
            Else
                MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "خطاي بانک")
            End If
        Catch e As Exception
            MsgBox(e.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "خطا")
        End Try
    End Sub
    Private Sub DeleteRow()
        Try
            Dim cnSQL As SqlConnection
            Dim cmSQL As SqlCommand
            Dim strSQL As String

            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()
            strSQL = "Delete From tblAN_KasrMavadAvaliehSatr Where ccKasrSatr=" & dvSatr(cmSatr.Position)("ccKasrSatr")
            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.ExecuteNonQuery()

            cnSQL.Close()
            cmSQL = Nothing : cnSQL = Nothing

            Mode = UD_Dll.Enums.GL_ModeForms.None
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->DeleteRow")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->DeleteRow")
        End Try
    End Sub
    Private Sub RefreshSatrData()
        Try
            If dvTitr.Count = 0 Then
                Exit Sub
            End If

            Dim Strsql As String
            Dim daSQL As SqlDataAdapter

            If dsForm.Tables.Contains("HESatr") Then
                dsForm.Tables.Remove("HESatr")
            End If

            Strsql = "Select * From qryAN_KasrMavadAvaliehSatr Where ccKasr = " & dvTitr(cmTitr.Position)("ccKasr")

            daSQL = New SqlDataAdapter(Strsql, ConnectionString)
            daSQL.Fill(dsForm, "HESatr")

            dvSatr = New DataView(dsForm.Tables("HESatr"))

            dvSatr.Sort = "Radif asc"

            dvSatr.AllowNew = False
            dvSatr.AllowDelete = True
            dvSatr.AllowEdit = True

            daSQL = Nothing
            dbgSatr.DataSource = Nothing
            dbgSatr.DataSource = dvSatr
            BoundCurrencyManagerSatr()
            SetSatrButton()
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->RefreshSatrData")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->RefreshSatrData")
        End Try
    End Sub
    Private Sub SetTitrButton()
        Try
            Select Case Mode
                Case UD_Dll.Enums.GL_ModeForms.AddNewRecord
                    btnSearch.Enabled = False
                    btnFTitr.Enabled = False
                    btnLTitr.Enabled = False
                    btnFSatr.Enabled = False
                    btnLSatr.Enabled = False

                    btnNewSanad.Visible = False
                    btnEditSanad.Visible = False
                    btnDeleteTitr.Visible = False
                    btnSaveSanad.Visible = True
                    btnCancelSanad.Visible = True
                Case UD_Dll.Enums.GL_ModeForms.UpdateRecord
                    btnSearch.Enabled = False
                    btnFTitr.Enabled = False
                    btnLTitr.Enabled = False
                    btnFSatr.Enabled = False
                    btnLSatr.Enabled = False

                    btnNewSanad.Visible = False
                    btnEditSanad.Visible = False
                    btnDeleteTitr.Visible = False
                    btnSaveSanad.Visible = True
                    btnCancelSanad.Visible = True
                Case UD_Dll.Enums.GL_ModeForms.None
                    btnSearch.Enabled = True
                    btnFTitr.Enabled = True
                    btnLTitr.Enabled = True
                    btnFSatr.Enabled = True
                    btnLSatr.Enabled = True

                    btnNewSanad.Visible = True
                    btnSaveSanad.Visible = False
                    btnCancelSanad.Visible = False
                    If dvTitr.Count > 0 Then
                        btnEditSanad.Visible = True
                        btnDeleteTitr.Visible = True
                    Else
                        btnEditSanad.Visible = False
                        btnDeleteTitr.Visible = False
                    End If
                Case Else
                    btnNewSanad.Visible = False
                    btnEditSanad.Visible = False
                    btnDeleteTitr.Visible = False
            End Select
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->SetTitrButton")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->SetTitrButton")
        End Try

    End Sub
    Private Sub SetSatrButton()
        Try
            Select Case Mode
                Case UD_Dll.Enums.GL_ModeForms.AddNewRow
                    btnSearch.Enabled = False
                    btnFTitr.Enabled = False
                    btnLTitr.Enabled = False
                    btnFSatr.Enabled = False
                    btnLSatr.Enabled = False

                    btnNewRow.Enabled = False
                    btnDeleteRow.Enabled = False
                    btnEditRow.Enabled = False
                    btnSaveRow.Enabled = True
                    btnCancelRow.Enabled = True
                Case UD_Dll.Enums.GL_ModeForms.UpdateRow
                    btnSearch.Enabled = False
                    btnFTitr.Enabled = False
                    btnLTitr.Enabled = False
                    btnFSatr.Enabled = False
                    btnLSatr.Enabled = False

                    btnNewRow.Enabled = False
                    btnDeleteRow.Enabled = False
                    btnEditRow.Enabled = False
                    btnSaveRow.Enabled = True
                    btnCancelRow.Enabled = True
                Case UD_Dll.Enums.GL_ModeForms.None
                    btnSearch.Enabled = True
                    btnFTitr.Enabled = True
                    btnLTitr.Enabled = True
                    btnFSatr.Enabled = True
                    btnLSatr.Enabled = True

                    If dvTitr.Count > 0 Then
                        btnNewRow.Enabled = True
                    Else
                        btnNewRow.Enabled = False
                    End If
                    btnSaveRow.Enabled = False
                    btnCancelRow.Enabled = False
                    If Not dvSatr Is Nothing Then
                        If dvSatr.Count > 0 Then
                            btnDeleteRow.Enabled = True
                            btnEditRow.Enabled = True
                        End If
                    Else
                        btnDeleteRow.Enabled = False
                        btnEditRow.Enabled = False
                    End If
                Case Else
                    btnNewRow.Enabled = False
                    btnEditRow.Enabled = False
                    btnDeleteRow.Enabled = False
            End Select
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->SetSatrButton")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->SetSatrButton")
        End Try

    End Sub
    Private Sub SetFormObject()
        Try
            SetTitrButton()
            SetSatrButton()
            'If UserName.ToUpper = "ADMINISTRATOR" Then
            btnFullDelete.Visible = True
            'End If
            Select Case Mode
                Case UD_Dll.Enums.GL_ModeForms.None
                    dbgTitr.Height = TitrOrgSize
                    dbgSatr.Height = SatrOrgSize
                    dbgTitr.Enabled = True
                    dbgSatr.Enabled = True
                Case UD_Dll.Enums.GL_ModeForms.AddNewRecord
                    dbgTitr.Height = TitrGridSize
                    dbgTitr.Enabled = False
                    dbgSatr.Enabled = False
                    mskTarikh.Focus()
                    txtShomarehForm.Text = objTools.ConvertNulls(objTools.DMax("ShomarehForm", "tblAN_KasrMavadAvalieh", "CodeMahal=" & CodeMahalFaal & " AND CodeDoreh = " & CodeDoreh), 0) + 1
                Case UD_Dll.Enums.GL_ModeForms.AddNewRow
                    dbgSatr.Height = SatrGridSize
                    dbgTitr.Enabled = False
                    dbgSatr.Enabled = False
                    txtRadif.Text = objTools.ConvertNulls(objTools.DMax("Radif", "tblAN_KasrMavadAvaliehSatr", "ccKasr=" & dvTitr(cmTitr.Position)("ccKasr")), 0) + 1
                Case UD_Dll.Enums.GL_ModeForms.UpdateRow
                    dbgSatr.Height = SatrGridSize
                    dbgTitr.Enabled = False
                    dbgSatr.Enabled = False
                Case UD_Dll.Enums.GL_ModeForms.UpdateRecord
                    dbgTitr.Height = TitrGridSize
                    dbgTitr.Enabled = False
                    dbgSatr.Enabled = False
                    Me.mskTarikh.Focus()
            End Select
            btnLSatr.Top = dbgSatr.Height + dbgSatr.Top - btnLSatr.Height
            btnLTitr.Top = dbgTitr.Height + dbgTitr.Top - btnLTitr.Height
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->SetFormObject")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->SetFormObject")
        End Try
    End Sub
    Private Function IsValidMojodi(ByVal Tedad1 As Double, ByVal ccKala As Integer) As Boolean
        IsValidMojodi = False

        Dim dtError As New DataTable
        Dim dr As DataRow

        Dim TedadPishFaktorFaktorNashodeh As Double = 0
        Dim MojodiFely As Double = 0
        Dim MojodiGhabelForosh As Double = 0

        'Create DataColumn For Data Table
        dtError.Columns.Add("NameKala", Type.GetType("System.String"))
        dtError.Columns.Add("Tedad", Type.GetType("System.Int32"))
        dtError.Columns.Add("Mojod", Type.GetType("System.Int32"))
        dtError.Columns.Add("TedadKast", Type.GetType("System.Int32"))

        Dim ccAnbarAsly As Integer = dvTitr(cmTitr.Position)("ccAnbar")
        'GetMojodyDarHalForosh(ccKala, ccAnbarAsly, TedadPishFaktorFaktorNashodeh, MojodiFely, MojodiGhabelForosh)
        Dim MojodiKala As Double = objCode.MojodiAnbar(dvTitr(cmTitr.Position)("ccAnbar"), Str(CodeDoreh).Trim + "0101", dvTitr(cmTitr.Position)("TarikhForm"), ccKala)


        'If Val(MojodiKala) < Val(txtTedad.Text.ToString.Replace(",", ".")) Then
        If MojodiKala < Tedad1 Then
            dr = dtError.NewRow()
            dr("NameKala") = lblNameKala.Text
            dr("Tedad") = Tedad1
            dr("Mojod") = MojodiKala
            dr("TedadKast") = Tedad1 - MojodiKala
            dtError.Rows.Add(dr)
        End If

        If dtError.Rows.Count > 0 Then
            Dim frmPishFaktorMojodiKalaErrorList As New frmAN_MojodiKalaErrorList
            frmPishFaktorMojodiKalaErrorList.dgvTitr.DataSource = dtError.DefaultView
            frmPishFaktorMojodiKalaErrorList.ShowDialog()
            If frmPishFaktorMojodiKalaErrorList.flg = False Then
                Exit Function
            End If
        End If

        Return True
    End Function
    'Private Function IsValidMojodi(ByVal ccKala As Integer, ByVal Tedad1 As Double) As Boolean
    '    IsValidMojodi = False
    '    Dim TaTarikh As String = TarikhEmrooz
    '    Dim AzTarikh As String = CodeDoreh & "0101"
    '    Dim Mojodi As Double = objCode.MojodiAnbar(dvTitr(cmTitr.Position)("ccAnbar"), AzTarikh, TaTarikh, ccKala)
    '    Mojodi = CInt((Mojodi * 100) + 0.5) / 100
    '    Mojodi = Mojodi - Tedad1
    '    If Mojodi < 0 Then
    '        Dim NameKala As String = objTools.ConvertNulls(objTools.DLookup("NameKala", "tblAN_Kala", "ccKala= " & ccKala), "")
    '        MsgBox("تعداد کالای " & NameKala & "  در انبار کمتر از تعداد درخواست شده می باشد.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "پیام")
    '        Exit Function
    '        Return False
    '    End If
    '    Return True
    'End Function
    Private Sub BoundCurrencyManagerTitr()
        Try
            cmTitr = CType(BindingContext(dbgTitr.DataSource), CurrencyManager)
            AddHandler cmTitr.ItemChanged, AddressOf cmTitr_ItemChanged
            AddHandler cmTitr.PositionChanged, AddressOf cmTitr_PositionChanged
            RefreshSatrData()
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->BoundCurrencyManagerTitr")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->BoundCurrencyManagerTitr")
        End Try

    End Sub
    Private Sub BoundCurrencyManagerSatr()
        Try
            cmSatr = CType(BindingContext(dbgSatr.DataSource), CurrencyManager)
            AddHandler cmSatr.ItemChanged, AddressOf cmSatr_ItemChanged
            AddHandler cmSatr.PositionChanged, AddressOf cmSatr_PositionChanged
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->BoundCurrencyManagerSatr")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->BoundCurrencyManagerSatr")
        End Try
    End Sub
    Private Sub cmTitr_ItemChanged(ByVal sender As Object, ByVal e As ItemChangedEventArgs)
        Try
            Search(True)
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->cmTitr_ItemChanged")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->cmTitr_ItemChanged")
        End Try
    End Sub
    Private Sub cmTitr_PositionChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            RefreshSatrData()
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->cmTitr_PositionChanged")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->cmTitr_PositionChanged")
        End Try
    End Sub
    Private Sub cmSatr_ItemChanged(ByVal sender As Object, ByVal e As ItemChangedEventArgs)
        Try
            SetSatrButton()
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->cmSatr_ItemChanged")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->cmSatr_ItemChanged")
        End Try

    End Sub
    Private Sub cmSatr_PositionChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            SetSatrButton()
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->cmSatr_PositionChanged")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->cmSatr_PositionChanged")
        End Try

    End Sub
    Private Function IsValidTarikh() As Boolean
        Try
            IsValidTarikh = False
            For Each dr As DataRow In dsForm.Tables("HESatr").Rows
                If Not (objTarikh.IsShDate(dr("TarikhTolidSlash").ToString.Trim, True) _
                 And objTarikh.IsShDate(dr("TarikhEnghezaSlash").ToString.Trim, True)) Then
                    Exit Function
                    Return False
                End If
            Next
            Return True
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->btnSaveChange_Click")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->btnSaveChange_Click")
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
            CodeDoreh = "1396"

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
#End Region
#Region "From Buttons"
    Private Sub btnNewRow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNewRow.Click
        If Not objSec.HaveUserPermission(SN, UD_Dll.Security.ePermissions.AddSatr) Then Exit Sub
        Try
            Mode = UD_Dll.Enums.GL_ModeForms.AddNewRow
            SetFormObject()
            txtTedad.Text = "0"
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->btnNewRow_Click")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->btnNewRow_Click")
        End Try

    End Sub
    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Try
            Me.Close()
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->btnExit_Click")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->btnExit_Click")
        End Try

    End Sub
    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            Search(True)
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->btnSearch_Click")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->btnSearch_Click")
        End Try

    End Sub
    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelRow.Click
        Try
            Mode = UD_Dll.Enums.GL_ModeForms.None
            ClearForm()
            SetFormObject()
            'If cmTitr.Position >= 0 Then
            '    FillFooterLable(CLng(objTools.ConvertNulls(dvTitr(cmTitr.Position)("sNoePardakht"), 0)))
            'End If
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->btnCancel_Click")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->btnCancel_Click")
        End Try

    End Sub
    Private Sub btnCancelSanad_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelSanad.Click
        Try
            Mode = UD_Dll.Enums.GL_ModeForms.None
            ClearForm()
            SetFormObject()
            Search(True, False)
            'If cmTitr.Position >= 0 Then
            '    FillFooterLable(CLng(objTools.ConvertNulls(dvTitr(cmTitr.Position)("sNoePardakht"), 0)))
            'End If
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->btnCancelSanad_Click")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->btnCancelSanad_Click")
        End Try

    End Sub
    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeleteRow.Click
        If Not objSec.HaveUserPermission(SN, UD_Dll.Security.ePermissions.Delete) Then Exit Sub
        Try
            If MsgBox("آيا رکورد حذف شود؟", MsgBoxStyle.YesNo + MsgBoxStyle.Question + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.DefaultButton2, "حذف رکورد") = MsgBoxResult.Yes Then

                DeleteRow()
                RefreshSatrData()
                SetFormObject()
            End If
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->btnDelete_Click")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->btnDelete_Click")
        End Try

    End Sub
    Private Sub btnDeleteTitr_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeleteTitr.Click
        If Not objSec.HaveUserPermission(SN, UD_Dll.Security.ePermissions.Delete) Then Exit Sub
        Try
            If MsgBox("آيا تيتر جاري حذف شود؟", MsgBoxStyle.YesNo + MsgBoxStyle.Question + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.DefaultButton2, "حذف رکورد") = MsgBoxResult.Yes Then
                DeleteTitr()
                Search(True)
            End If
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->btnDeleteTitr_Click")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->btnDeleteTitr_Click")
        End Try

    End Sub
    Private Sub btnNewSanad_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNewSanad.Click
        If Not objSec.HaveUserPermission(SN, UD_Dll.Security.ePermissions.Add) Then Exit Sub
        Try
            txtShomarehS.Text = ""
            Mode = UD_Dll.Enums.GL_ModeForms.AddNewRecord
            Search(True, False)
            ClearForm()
            dbgSatr.DataSource = Nothing

            mskTarikh.Text = TarikhEmrooz
            SetFormObject()
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->btnNewSanad_Click")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->btnNewSanad_Click")
        End Try

    End Sub
    Private Sub btnEditSanad_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEditSanad.Click
        If Not objSec.HaveUserPermission(SN, UD_Dll.Security.ePermissions.Update) Then Exit Sub
        Try
            Mode = UD_Dll.Enums.GL_ModeForms.UpdateRecord
            SetFormTitrData()
            SetFormObject()
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->btnEditSanad_Click")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->btnEditSanad_Click")
        End Try

    End Sub
    Private Sub btnEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEditRow.Click
        If Not objSec.HaveUserPermission(SN, UD_Dll.Security.ePermissions.UpdateSatr) Then Exit Sub
        Try
            Mode = UD_Dll.Enums.GL_ModeForms.UpdateRow
            SetFormSatrData(cmSatr.Position)
            SetFormObject()
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->btnEdit_Click")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->btnEdit_Click")
        End Try

    End Sub
    Private Sub btnSaveSanad_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveSanad.Click
        Dim tPos As Long
        Dim chkUpd As Boolean = False
        Try
            Select Case Mode
                Case UD_Dll.Enums.GL_ModeForms.AddNewRecord
                    If Not objSec.HaveUserPermission(SN, UD_Dll.Security.ePermissions.Add) Then Exit Sub
                    If AddNewSanad() = False Then
                        Exit Sub
                    End If
                Case UD_Dll.Enums.GL_ModeForms.UpdateRecord
                    If Not objSec.HaveUserPermission(SN, UD_Dll.Security.ePermissions.Update) Then Exit Sub
                    tPos = cmTitr.Position
                    If UpdateSanad() = False Then
                        Exit Sub
                    End If
            End Select
            Search(True)
            SetFormObject()
            If chkUpd Then
                cmTitr.Position = tPos
            End If

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->btnSaveSanad_Click")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->btnSaveSanad_Click")
        End Try

    End Sub
    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveRow.Click
        Try
            Select Case Mode
                Case UD_Dll.Enums.GL_ModeForms.AddNewRow
                    If Not objSec.HaveUserPermission(SN, UD_Dll.Security.ePermissions.AddSatr) Then Exit Sub
                    AddNewRow()
                Case UD_Dll.Enums.GL_ModeForms.UpdateRow
                    If Not objSec.HaveUserPermission(SN, UD_Dll.Security.ePermissions.UpdateSatr) Then Exit Sub
                    UpdateRow()
            End Select
            FirstInsert = True
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->btnSave_Click")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->btnSave_Click")
        End Try
    End Sub
    Private Sub btnFTitr_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFTitr.Click
        Try
            cmTitr.Position = 0
            dbgTitr.CurrentRowIndex = cmTitr.Position
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->btnFTitr_Click")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->btnFTitr_Click")
        End Try

    End Sub
    Private Sub btnLTitr_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLTitr.Click
        Try
            cmTitr.Position = cmTitr.Count - 1
            dbgTitr.CurrentRowIndex = cmTitr.Position
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->btnLTitr_Click")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->btnLTitr_Click")
        End Try

    End Sub
    Private Sub btnFSatr_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFSatr.Click
        Try
            cmSatr.Position = 0
            dbgSatr.CurrentRowIndex = cmSatr.Position
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->btnFSatr_Click")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->btnFSatr_Click")
        End Try

    End Sub
    Private Sub btnLSatr_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLSatr.Click
        Try
            cmSatr.Position = cmSatr.Count - 1
            dbgSatr.CurrentRowIndex = cmSatr.Position
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->btnLSatr_Click")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->btnLSatr_Click")
        End Try

    End Sub
    Private Sub btnFullDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFullDelete.Click
        If dvTitr.Count = 0 Then Exit Sub
        If Not objSec.HaveUserPermission(SN, UD_Dll.Security.ePermissions.DeleteSatr) Then Exit Sub
        If Not objSec.HaveUserPermission(SN, UD_Dll.Security.ePermissions.Delete) Then Exit Sub

        Try
            If MsgBox("آيا حاضريد کل پیش فاکتور حذف شود؟", MsgBoxStyle.YesNo + MsgBoxStyle.Question + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.DefaultButton2, "حذف رکورد") = MsgBoxResult.No Then
                Exit Sub
            End If
            If MsgBox("آيا حاضريد کل پیش فاکتور حذف شود؟", MsgBoxStyle.YesNo + MsgBoxStyle.Question + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.DefaultButton2, "حذف رکورد") = MsgBoxResult.Yes Then
                DeleteFull()
                Search(True)
            End If
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->btnDeleteTitr_Click")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->btnDeleteTitr_Click")
        End Try
    End Sub
    Private Sub btnSaveChange_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveChange.Click
        If Not objSec.HaveUserPermission(SN, UD_Dll.Security.ePermissions.UpdateSatr) Then Exit Sub
        If Not IsValidTarikh() Then
            MsgBox("عمليــات ذخيــــره سـازی انجــام نشـــد، تاریخ را صحیح وارد کنید.", MsgBoxStyle.MsgBoxRtlReading Or MsgBoxStyle.MsgBoxRight Or MsgBoxStyle.Information, "ذخيره")
            Exit Sub
        End If

        Dim cmSQL As SqlCommand : Dim cnSQL As SqlConnection
        Dim strSQL As String = ""

        Dim TarikhTolid As String = String.Empty
        Dim TarikhEngheza As String = String.Empty

        Try
            If dsForm.HasChanges(DataRowState.Modified) Then
                If dsForm.Tables("HESatr").GetChanges(DataRowState.Modified).Rows.Count <> 0 Then

                    cnSQL = New SqlConnection(ConnectionString)
                    cnSQL.Open()

                    For Each dr As DataRow In dsForm.Tables("HESatr").GetChanges(DataRowState.Modified).Rows
                        TarikhTolid = objTools.ConvertNulls(objTools.DLookup("TarikhTolid", "tblGL_ShomarehBach", "ShomarehBach= '" & dr("ShomarehBach") & "'"), "")
                        TarikhEngheza = objTools.ConvertNulls(objTools.DLookup("TarikhEngheza", "tblGL_ShomarehBach", "ShomarehBach= '" & dr("ShomarehBach") & "'"), "")

                        strSQL = "UPDATE tblAN_KasrEzafehSatr SET "
                        strSQL += " Tedad1 = " & objTools.ConvertNulls(dr("Tedad1"), 0) & ","
                        strSQL += " Tedad2 = " & objTools.ConvertNulls(dr("Tedad1"), 0) & ","
                        strSQL += " Tedad3 = " & objTools.ConvertNulls(dr("Tedad1"), 0) & ","
                        strSQL += " MablaghKharid = " & objTools.ConvertNulls(dr("MablaghKharid"), 0) & ","
                        If TarikhTolid = String.Empty Then
                            strSQL &= "TarikhTolid = '" & dr("TarikhTolidSlash") & "',"
                        Else
                            strSQL &= "TarikhTolid = '" & TarikhTolid & "',"
                        End If
                        If TarikhEngheza = String.Empty Then
                            strSQL &= "TarikhEngheza = '" & dr("TarikhEnghezaSlash") & "',"
                        Else
                            strSQL &= "TarikhEngheza = '" & TarikhEngheza & "',"
                        End If
                        If dr("ShomarehBach").ToString.Trim.Length > 10 Then
                            strSQL &= "ShomarehBach = '" & dr("ShomarehBach").ToString.Trim.Substring(1, 10) & "'"
                        Else
                            strSQL &= "ShomarehBach = '" & dr("ShomarehBach") & "'"
                        End If
                        strSQL += " WHERE ccKasrEzafehSatr = " & dr("ccKasrEzafehSatr")
                        cmSQL = New SqlCommand(strSQL, cnSQL)
                        cmSQL.ExecuteNonQuery()
                        FirstInsert = True
                    Next

                    dsForm.Tables("HESatr").AcceptChanges()

                    RefreshSatrData()

                    MsgBox("عمليــات ذخيــــره سـازی انجــام شـــد .", MsgBoxStyle.MsgBoxRtlReading Or MsgBoxStyle.MsgBoxRight Or MsgBoxStyle.Information, "ذخيره")
                End If
            End If
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->btnSaveChange_Click")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->btnSaveChange_Click")
        Finally
            cnSQL = Nothing : cmSQL = Nothing
        End Try
    End Sub
#End Region


    Private Sub btnErsal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnErsal.Click
        Dim cnSQL As SqlConnection
        Dim cmSQL As SqlCommand
        Dim strSqlTitr

        objCode.SabteTaghirat(CodeMahalFaal, UD_Dll.Enums.GL_NoeTaghir.UpdateRecord, "tblAN_KasrMavadAvalieh", dvTitr(cmTitr.Position)("ccKasr"), dvTitr(cmTitr.Position)("ShomarehForm"), "بروز رساني ")

        cnSQL = New SqlConnection(ConnectionString)
        cnSQL.Open()

        strSqlTitr = " Update " & FormTableName
        strSqlTitr &= " Set sVazeiat = 2 "
        strSqlTitr &= " Where ccKasr = " & dvTitr(cmTitr.Position)("ccKasr")

        cmSQL = New SqlCommand(strSqlTitr, cnSQL)
        cmSQL.ExecuteNonQuery()
        MsgBox("عمليــات با موفقیت انجــام شـــد .", MsgBoxStyle.MsgBoxRtlReading Or MsgBoxStyle.MsgBoxRight Or MsgBoxStyle.Information, "ذخيره")
        cnSQL.Close()
        cmSQL = Nothing : cnSQL = Nothing

        tpos = cmTitr.Position
        Search(False)
        cmTitr.Position = tpos
    End Sub

    Private Sub btnErsal_MarginChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnErsal.MarginChanged

    End Sub
End Class
