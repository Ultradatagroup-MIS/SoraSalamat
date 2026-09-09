
Imports System.Data.SqlClient
Imports System.Data.SqlClient.SqlConnection
Imports System.Data.Common
Imports System.Data

Public Class frmAN_ResidMavadAvalieh

#Region "Variable AND Constant Declration"
    Const cntCodeSubSystem As Long = 635

    Const FormTableName = "tblAN_ResidMavadAvalieh"
    Const FormViewName = "qryAN_ResidMavadAvalieh"
    Const FormTableNameSatr = "tblAN_ResidMavadAvaliehSatr"

    Const TitrGridSize As Integer = 170
    Const SatrGridSize As Integer = 150
    Const TitrOrgSize As Integer = 256
    Const SatrOrgSize As Integer = 240
    Private WithEvents BS As New UD_Dll.PassString
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
    Private SN As Integer
    Dim flg As Boolean = False
    Dim FirstInsert As Boolean = False
    Dim flgInsertSatrSefaresh As Boolean = False
    Dim flgInsertSatrMojodiMashin As Boolean = False
    Dim tmpPosition As Integer
    Dim tpos As Integer
    Dim AllowChangeFeePishFaktor As Boolean = objTools.ConvertNulls(objTools.DLookup("AllowChangeFeePishFaktor", "tblGL_SysConfig", "CodeMahal = " & CodeMahalFaal), False)
#End Region
#Region "Form Event Code"
    Private Sub frmAN_KdxResid_Closed(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Closed
        ' "Receive" parameter is the caption of destination window
        Dim hwnd As Long = UD_Dll.Code.FindWindow(vbNullString, objCode.GetNameSherkat)
        If hwnd <> 0 Then
            BS.PostString(hwnd, &H400, 0, txtCaption)
        End If

        dsForm = Nothing
        dvForm = Nothing
    End Sub
    Private Sub frmAN_KdxResid_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            SetParameter()
            SN = objSec.GetSecurityNumber(cntCodeSubSystem, UserName)

            Mode = UD_Dll.Enums.GL_ModeForms.None
            LoadCombo()
            ClearForm()
            Search(False, False)
            flg = True
            SetFormObject()
            objTools.ChangeKbd(UD_Dll.mdlUtility.KBD.Farsi)
            If AllowChangeFeePishFaktor Then
                Me.txtMablaghKharid.Enabled = True
            Else
                Me.txtMablaghKharid.Enabled = False
            End If



        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->frmResid_Load")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->frmResid_Load")
        End Try
    End Sub
    Private Sub frmAN_KdxResid_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        Try
            'Me.TopMost = True
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->frmSanadHesabdary_Paint")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->frmSanadHesabdary_Paint")
        End Try
        'btnNewSanad.Focus()
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
    Private Sub cmbsCodeDorehSefaresh_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbsCodeDorehSefaresh.SelectedIndexChanged
        If cmbsCodeDorehSefaresh.SelectedIndex = -1 Then
            txtShomarehSefaresh.Enabled = False
        ElseIf Mode <> UD_Dll.Enums.GL_ModeForms.UpdateRecord Then
            txtShomarehSefaresh.Enabled = True
        End If
    End Sub
    Private Sub txtCodeKala_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCodeKala.TextChanged
        If Mode = UD_Dll.Enums.GL_ModeForms.UpdateRecord Then Exit Sub
        If Trim(Me.txtCodeKala.Text) = "" Then
            lblNameKala.Text = ""
            txtMablaghKharid.Text = 0
            Exit Sub
        End If
        If Not flg Then Exit Sub

        Dim str As String = ""
        str = "CodeKala=" & Me.txtCodeKala.Text & " AND ccTaminKonandeh = " & dvTitr(cmTitr.Position)("ccTaminKonandeh") & " AND ccKala In (Select ccKala From tblAN_KdxSefareshSatr Where ccKardexTitr = " & dvTitr(cmTitr.Position)("ccSefareshTitr") & ")"

        Me.lblNameKala.Text = objTools.ConvertNulls(objTools.DLookup("NameKala", "tblAN_Kala", "CodeKala='" & Me.txtCodeKala.Text & "'"), "")
        Me.txtCodeKala.Tag = objTools.ConvertNulls(objTools.DLookup("ccKala", "tblAn_Kala", "CodeKala = '" & Me.txtCodeKala.Text & "'" & ""), 0)
        'Me.txtCodeKala.Tag = objTools.ConvertNulls(objTools.DLookup("ccKala", "tblAn_Kala", "CodeKala='" & Me.txtCodeKala.Text & "'" & " AND ccTaminKonandeh = " & dvTitr(cmTitr.Position)("ccTaminKonandeh")), 0)
        Dim a As String

        a = objCode.GetMablaghKharid(Me.txtCodeKala.Tag, dvTitr(cmTitr.Position)("TarikhForm"), CodeMahalFaal, dvTitr(cmTitr.Position)("ccTaminKonandeh"))
        If a = "0" Then
            Me.txtMablaghKharid.Text = "1"
        Else
            Me.txtMablaghKharid.Text = a
        End If

    End Sub
    Private Sub txtCodeKala_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCodeKala.KeyPress
        Try
            If (Asc(e.KeyChar()) < 48 Or Asc(e.KeyChar()) > 57) And (Asc(e.KeyChar()) <> 8) Then
                e.Handled = True
            End If
            If e.KeyChar = Chr(Keys.Space) Then
                Dim objKala As New Forms_dll.frmAN_KalaSearch
                Dim StrSql As String = String.Empty

                If txtCodeKala.Text.Length <> 0 Then
                    objKala.tcodeKala = txtCodeKala.Text
                End If

                'Load Search Combo Kala
                'If objTools.ConvertNulls(dvTitr(cmTitr.Position)("ccTaminKonandeh"), 0) = 0 Then
                '    StrSql = "Select CodeKala,NameKala,ccKala,sVahedeShomaresh,txtsVahedeShomaresh,NameBrand,Radif From qryAN_Kala where Faal=1 and "
                '    StrSql &= " Not exists (Select * From tblGL_SecurityData where namekarbar= '" & UserName
                '    StrSql &= "' and CodeSubSystem = 642 and pk = qryAN_Kala.ccKala) AND "
                '    StrSql &= " ccKala In (Select ccKala From tblAN_AnbarKala Where ccAnbar = " & dvTitr(cmTitr.Position)("ccAnbar") & ")"
                'Else
                '    StrSql = "Select CodeKala,NameKala,ccKala,sVahedeShomaresh,txtsVahedeShomaresh,NameBrand,Radif From qryAN_Kala where Faal=1 and "
                '    StrSql &= " Not exists (Select * From tblGL_SecurityData where namekarbar= '" & UserName
                '    StrSql &= "' and CodeSubSystem = 642 and pk = qryAN_Kala.ccKala) AND "
                '    StrSql &= " ccTaminKonandeh=" & dvTitr(cmTitr.Position)("ccTaminKonandeh") & " AND "
                '    StrSql &= " ccKala In (Select ccKala From tblAN_AnbarKala Where ccAnbar = " & dvTitr(cmTitr.Position)("ccAnbar") & ")"
                'End If
                'If objTools.ConvertNulls(dvTitr(cmTitr.Position)("ccSefareshTitr"), 0) <> 0 Then
                '    StrSql &= " AND     ccKala In (Select ccKala From tblAN_KdxSefareshSatr Where ccKardexTitr = " & dvTitr(cmTitr.Position)("ccSefareshTitr") & ")"
                'End If


                StrSql = "Select CodeKala,NameKala,ccKala,sVahedeShomaresh,txtsVahedeShomaresh,NameBrand,Radif From qryAN_Kala where Faal=1 and "
                StrSql &= " Not exists (Select * From tblGL_SecurityData where namekarbar= '" & UserName
                StrSql &= "' and CodeSubSystem = 642 and pk = qryAN_Kala.ccKala) AND "
                StrSql &= " ccKala In (Select ccKala From tblAN_AnbarKala Where ccAnbar = " & dvTitr(cmTitr.Position)("ccAnbar") & ")"

                If objTools.ConvertNulls(dvTitr(cmTitr.Position)("ccSefareshTitr"), 0) <> 0 Then
                    StrSql &= " AND     ccKala In (Select ccKala From tblAN_KdxSefareshSatr Where ccKardexTitr = " & dvTitr(cmTitr.Position)("ccSefareshTitr") & ")"
                End If



                MultiSelection = False
                SearchItem = "CodeKala"
                objKala.SetForm(StrSql)
                objKala.ShowDialog()

                Me.txtCodeKala.Tag = objKala.tccKala
                Me.txtCodeKala.Text = objKala.tcodeKala
                Me.lblNameKala.Text = objKala.tNameKala

                txtShomarehBatch.Text = ""
                mskTarikhEngheza.Text = ""
                mskTarikhTolid.Text = ""

                MultiSelection = False
            End If
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->txtCodeMoshtaryS_KeyPress")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->txtCodeMoshtaryS_KeyPress")
        End Try
    End Sub
    Private Sub txtShomarehBach_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtShomarehBatch.KeyPress
        Try
            If (Asc(e.KeyChar()) < 48 Or Asc(e.KeyChar()) > 57) And (Asc(e.KeyChar()) <> 8) Then
                e.Handled = True
            End If
            If e.KeyChar = Chr(Keys.Space) Then
                If txtCodeKala.Text.Length = 0 And lblNameKala.Text = "" Then
                    MsgBox("ابتدا کد و نام کالا را وارد کنید", MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " خطای وارد نکردن اطلاعات ضروری")
                    Exit Sub
                End If
                Dim objShomarehBach As New Forms_dll.ShomarehBachSearch
                Dim StrSql As String
                If dvTitr(cmTitr.Position)("NoeResid") = UD_Dll.Enums.AN_NoeResid.ResidAzTaminKonandeh Then
                    StrSql = "Select  ShomarehBach,TarikhTolidSlash,TarikhEnghezaSlash,ccShomarehBach, "
                    StrSql &= "  NameTaminKonandeh,IsNull(NameTolidKonandeh,'---'),NameKala "
                    StrSql &= "   From QryGL_ShomarehBach "
                    StrSql &= " Where ccKala = " & Me.txtCodeKala.Tag
                    StrSql &= " And ccTaminKonandeh = " & dvTitr(cmTitr.Position)("ccTaminKonandeh")

                Else
                    StrSql = "Select  ShomarehBach,TarikhTolidSlash,TarikhEnghezaSlash,ccShomarehBach, "
                    StrSql &= "  NameTaminKonandeh,IsNull(NameTolidKonandeh , '---'),NameKala "
                    StrSql &= "   From QryGL_ShomarehBach "
                    StrSql &= " Where ccKala = " & Me.txtCodeKala.Tag

                End If

                objShomarehBach.SearchItem = "ShomarehBach"
                objShomarehBach.SetForm(StrSql)
                objShomarehBach.ShowDialog()

                Me.txtShomarehBatch.Text = objShomarehBach.tShomarehBach
                ' 1 Vared Shavad 3 mohem nist
                If (objCode.CheckTarikhMasraf() = 1) Or (objCode.CheckTarikhMasraf() = 3) Then
                    mskTarikhTolid.Text = objShomarehBach.tTarikhTolid
                    mskTarikhEngheza.Text = objShomarehBach.tTarikhEngheza
                End If
            End If
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->txtCodeMoshtaryS_KeyPress")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->txtCodeMoshtaryS_KeyPress")
        End Try
    End Sub
    Private Sub frmAN_KdxResid_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.F1 Then
            Dim HelpWindow As New Forms_dll.frmGL_HelpWindow
            HelpWindow.CurrentCodeSubSystem = cntCodeSubSystem
            HelpWindow.Show()
            HelpWindow.TopMost = True
        ElseIf e.KeyCode = Keys.Enter Then
            SendKeys.Send("{tab}")
        End If
    End Sub
    Private Sub MaskSelect(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mskAzTarikh.Enter, mskAzTarikh.Click, mskTaTarikh.Enter, mskTaTarikh.Click
        Try
            SendKeys.Send("{HOME}+{END}")
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->MaskSelect")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->MaskSelect")
        End Try
    End Sub
    Private Sub CheckIsNumeric(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles _
     txtMablaghKharid.KeyPress, txtShomarehBatch.KeyPress, txtshomarehFormS.KeyPress, _
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
#End Region
#Region "Global Form Code"
    Private Sub LoadCombo()
        Try
            Dim Strsql As String
            Dim daSQL As SqlDataAdapter

            ' --------------------------------------- Load Combo -----------------
            ' Load Combo tblTaminKonandeh
            Strsql = "Select NameTaminKonandeh,ccTaminKonandeh From tblFO_TaminKonandeh where Faal=1 AND"
            Strsql &= " Not exists (Select * From tblGL_SecurityData where namekarbar= '" & UserName & "' and CodeSubSystem = 654 and pk = tblFO_TaminKonandeh.ccTaminKonandeh) order by ccTaminKonandeh"
            daSQL = New SqlDataAdapter(Strsql, ConnectionString)
            daSQL.Fill(dsForm, "tblTaminKonandeh")
            cmbNameTaminKonandeh.DataSource = Nothing
            cmbNameTaminKonandeh.Items.Clear()
            cmbNameTaminKonandeh.DataSource = dsForm.Tables("tblTaminKonandeh").DefaultView
            cmbNameTaminKonandeh.DisplayMember = "NameTaminKonandeh"
            cmbNameTaminKonandeh.ValueMember = "ccTaminKonandeh"

            ' Load Combo tblAnbar
            Strsql = "Select codeAnbar,NameAnbar From qryAN_Anbar where CodeMahal=" & CodeMahalFaal & " AND Faal=1  AND  NoeAnbar=4 AND "
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

            ' Load Search Combo NoeResid
            Strsql = "Select CodeMahal,NameMahal From tblGL_MarkazPakhsh Where CodeMahal<>0 "
            daSQL = New SqlDataAdapter(Strsql, ConnectionString)
            daSQL.Fill(dsForm, "tblMarkazPakhsh")
            cmbMarkazPakhsh.DataSource = Nothing
            cmbMarkazPakhsh.Items.Clear()
            cmbMarkazPakhsh.DataSource = dsForm.Tables("tblMarkazPakhsh").DefaultView
            cmbMarkazPakhsh.DisplayMember = "NameMahal"
            cmbMarkazPakhsh.ValueMember = "CodeMahal"
            cmbMarkazPakhsh.SelectedValue = 0

            ' Load Combo TahvilGirandeh
            If objCode.CheckCompany = 5 Then
                Strsql = "Select FN From dbo.qryGL_MoshakhasatFardi"
                Strsql &= " Where sSemat in (4068,4061,4062,4846,4848,4849,5553) And sVazeiatEstekhdam = 4188 "
            Else
                Strsql = "Select FN From dbo.qryGL_MoshakhasatFardi"
                Strsql &= " Where sSemat in (4068,4061,4062) And sVazeiatEstekhdam = 4188 "
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
                Strsql &= " Where sSemat in (4068,4061,4062) And sVazeiatEstekhdam = 4188 "
            End If
            daSQL = New SqlDataAdapter(Strsql, ConnectionString)
            daSQL.Fill(dsForm, "TahvilDahandeh")
            cmbTahvilDahandeh.DataSource = Nothing
            cmbTahvilDahandeh.Items.Clear()
            cmbTahvilDahandeh.DataSource = dsForm.Tables("TahvilDahandeh").DefaultView
            cmbTahvilDahandeh.DisplayMember = "FN"
            cmbTahvilDahandeh.ValueMember = "FN"
            cmbTahvilDahandeh.SelectedValue = 0

            '------------------------------------------

           

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

            cmbMarkazPakhsh.SelectedIndex = -1
            cmbMarkazPakhsh.SelectedIndex = -1

            Me.txtCodeKala.Text = ""
            Me.txtCodeKala.Tag = ""
            Me.lblNameKala.Text = ""

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

            mskAzTarikh.Text = ""
            mskTaTarikh.Text = ""



            mskTarikhEngheza.Text = ""
            mskTarikhTolid.Text = ""
            mskTarikhForm.Text = TarikhEmrooz

            If objCode.CheckCompany = 24 Or objCode.CheckCompany = 1 Then
                mskTarikhForm.Enabled = False
            End If

            txtMablaghKharid.Text = ""
            txtShomarehBatch.Text = ""
            txtTedadKarton.Text = ""
            txtTedadBasteh.Text = ""
            txtTedad.Text = ""
            txtTozihat.Text = ""

            cmbAnbar.SelectedValue = objTools.ConvertNulls(objTools.DLookup("CodeAnbar", "tblAN_Anbar", "AnbarAsly=1"), 0)

            ErrPro.Dispose()

            Me.txtMablaghKharid.Enabled = False
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->ClearForm")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->ClearForm")
        End Try
    End Sub
    Private Sub SetFormTitrData()
        Try
            If dvTitr.Count = 0 Or cmTitr.Position = -1 Then
                Mode = UD_Dll.Enums.GL_ModeForms.None
                Exit Sub
            End If

            Dim drvTemp As DataRowView
            drvTemp = dvTitr(cmTitr.Position)

            Me.cmbTahvilGirandeh.SelectedValue = drvTemp("NameFardTahvilGirandeh")
            Me.cmbTahvilDahandeh.SelectedValue = drvTemp("NameFardTahvilDahandeh")
            mskTarikhForm.Text = drvTemp("TarikhForm")
            cmbAnbar.SelectedValue = drvTemp("ccAnbar")
            cmbNameTaminKonandeh.SelectedValue = drvTemp("ccTaminKonandeh")
            cmbNameTaminKonandeh.Enabled = False
            cmbMarkazPakhsh.SelectedValue = drvTemp("ccMarkazPakhsh")
            txtShomarehSefaresh.Text = drvTemp("ShomarehSefaresh")
            txtShomarehSefaresh.Enabled = False
            cmbsCodeDorehSefaresh.SelectedValue = drvTemp("CodeDorehSefaresh")
            If cmbsCodeDorehSefaresh.SelectedIndex = -1 Then
                cmbsCodeDorehSefaresh.SelectedIndex = cmbsCodeDorehSefaresh.Items.Count - 1
            End If
            cmbsCodeDorehSefaresh.Enabled = False
            txtTozihat.Text = drvTemp("Tozihat")

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->SetFormTitrData")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->SetFormTitrData")
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
                txtCodeKala.Enabled = False
                txtCodeKala.Tag = drvTemp("ccKala")
                txtCodeKala.Text = drvTemp("CodeKala")
                lblNameKala.Text = drvTemp("NameKala")

                Dim IsFastFood As Boolean = objTools.ConvertNulls(objTools.DLookup("IsFastFood", "dbo.tblGL_SysConfig", "CodeMahal  = " & CodeMahalFaal), False)
                If IsFastFood = True Then
                    txtTedad.Text = drvTemp("Tedad0")
                Else
                    txtTedad.Text = drvTemp("Tedad0")
                    'txtTedadBasteh.Text = drvTemp("TedadBasteh")
                    'txtTedadKarton.Text = drvTemp("TedadKarton")

                End If


                txtMablaghKharid.Text = drvTemp("MablaghKharid").ToString.Replace(",", ".")
                mskTarikhTolid.Text = drvTemp("TarikhTolid")
                mskTarikhEngheza.Text = drvTemp("TarikhEngheza")
                txtShomarehBatch.Text = drvTemp("ShomarehBach")

            End If

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->SetFormSatrData")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->SetFormSatrData")
        End Try

    End Sub
    Private Sub Search(ByVal WithCriteria As Boolean, Optional ByVal withMSG As Boolean = True)
        Try
            Dim StrSql As String
            Dim WhereStr As String

            StrSql = "SELECT * FROM " & FormViewName & " Where CodeMahal = " & CodeMahalFaal & " AND "
            StrSql &= " CodeDoreh = " & CodeDoreh & " AND "
            WhereStr = ""
            If WithCriteria Then
                If txtshomarehFormS.Text.Length > 0 Then
                    WhereStr = "ShomarehForm = " & txtshomarehFormS.Text & " AND "
                End If
                If mskAzTarikh.Text <> "" Then
                    WhereStr &= "TarikhForm >= '" & mskAzTarikh.Text & "' AND "
                End If
                If mskTaTarikh.Text <> "" Then
                    WhereStr &= "TarikhForm <= '" & mskTaTarikh.Text & "' AND "
                End If
            End If

            StrSql = StrSql & WhereStr
            StrSql = StrSql & " sVazeiat = " & UD_Dll.Enums.AN_VazeiatKDXRSAvalDoreh.BedoneAmalyat

            StrSql &= " AND Not exists (Select * From tblGL_SecurityData where namekarbar= '" & UserName & "' and CodeSubSystem = 633 and pk = " & FormViewName & ".ccAnbar) "
            StrSql &= "order by TarikhForm "

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

            If dsForm.Tables.Contains("tblAN_ResidMavadAvalieh") Then
                dsForm.Tables.Remove("tblAN_ResidMavadAvalieh")
            End If
            daSQL = New SqlDataAdapter(strSql, ConnectionString)
            daSQL.Fill(dsForm, "tblAN_ResidMavadAvalieh")


            dvTitr = New DataView(dsForm.Tables("tblAN_ResidMavadAvalieh"), "", "ccKardexTitr DESC", DataViewRowState.CurrentRows)
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
                If WithMSG Then
                    'MsgBox("سندي پيدا نشد.", MsgBoxStyle.MsgBoxRtlReading Or MsgBoxStyle.OkOnly Or MsgBoxStyle.Information, "جستجو")
                End If
            End If
            SetGridStyle()
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->RefreshTitrdata")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->RefreshTitrdata")
        End Try
    End Sub
    Private Sub RefreshSatrData()
        Try
            If dvTitr.Count = 0 Then
                Exit Sub
            End If

            Dim Strsql As String
            Dim daSQL As SqlDataAdapter

            If dsForm.Tables.Contains("tblAN_ResidMavadAvaliehSatr") Then
                dsForm.Tables.Remove("tblAN_ResidMavadAvaliehSatr")
            End If

            Strsql = "Select *  ,isnull(case when ccSefareshTitr<>0 then  "
            Strsql &= " (select SUM(Tedad3) as Tedad FROM qryAN_kdxSefareshSatr WHERE   qryAN_kdxSefareshSatr.ccKardexTitr="
            Strsql &= " a.ccSefareshTitr AND   qryAN_kdxSefareshSatr.ccKala = a.ccKala ) "
            Strsql &= " - "
            Strsql &= " (select sum(tedad3) from qryAN_ResidMavadAvaliehSatr where ccKardexTitr in "
            Strsql &= " (select ccKardexTitr from qryAN_ResidMavadAvalieh where ccSefareshTitr =a.ccSefareshTitr )"
            Strsql &= " AND qryAN_ResidMavadAvaliehSatr.ccKala = a.ccKala "
            Strsql &= " AND qryAN_ResidMavadAvaliehSatr.ccKardexSatr <=a.ccKardexSatr "
            Strsql &= " ) End ,0) as TedadMandehDarResid "
            Strsql &= " ,isnull((select sum(tedad3) from dbo.qryAN_ResidMavadAvaliehTitrSatr where cckala =a.ccKala"
            Strsql &= " and ccSefareshTitr=a.ccSefareshTitr and ccSefareshTitr<>0 ),0) TedadKolResidShodeh "
            Strsql &= " From qryAN_ResidMavadAvaliehSatr a Where ccKardexTitr = " & dvTitr(cmTitr.Position)("ccKardexTitr")

            daSQL = New SqlDataAdapter(Strsql, ConnectionString)
            daSQL.Fill(dsForm, "tblAN_ResidMavadAvaliehSatr")

            dvSatr = New DataView(dsForm.Tables("tblAN_ResidMavadAvaliehSatr"))

            dvSatr.Sort = "Radif asc"

            dvSatr.AllowNew = False
            dvSatr.AllowDelete = True
            dvSatr.AllowEdit = False

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
    Private Sub SetGridStyle()
        Try
            Dim tableStyleTitr As New DataGridTableStyle
            tableStyleTitr.MappingName = "tblAN_ResidMavadAvalieh"
            tableStyleTitr.RowHeaderWidth = 20
            tableStyleTitr.AllowSorting = True
            dbgTitr.BorderStyle = BorderStyle.Fixed3D



            Dim TextCol99 As New DataGridTextBoxColumn
            With TextCol99
                .MappingName = "txtVazeiat"
                .HeaderText = "وضعیت"
                .Width = 100
                .Alignment = HorizontalAlignment.Center
            End With
            tableStyleTitr.GridColumnStyles.Add(TextCol99)

            Dim TextCol02 As New DataGridTextBoxColumn
            With TextCol02
                .MappingName = "TarikhFormSlash"
                .HeaderText = "تاریخ فرم"
                .Width = 150
                .Alignment = HorizontalAlignment.Left
            End With
            tableStyleTitr.GridColumnStyles.Add(TextCol02)

            Dim TextCol01 As New DataGridTextBoxColumn
            With TextCol01
                .MappingName = "NameAnbar"
                .HeaderText = "نام انبار"
                .Width = 150
                .Alignment = HorizontalAlignment.Left
            End With
            tableStyleTitr.GridColumnStyles.Add(TextCol01)

            Dim TextCol00 As New DataGridTextBoxColumn
            With TextCol00
                .MappingName = "NameTaminKonandeh"
                .HeaderText = "نام تامین کننده"
                .Width = 150
                .Alignment = HorizontalAlignment.Left
            End With
            tableStyleTitr.GridColumnStyles.Add(TextCol00)

            Dim TextCol000 As New DataGridTextBoxColumn
            With TextCol000
                .MappingName = "Tozihat"
                .HeaderText = "توضیحات"
                .Width = 200
                .Alignment = HorizontalAlignment.Left
            End With
            tableStyleTitr.GridColumnStyles.Add(TextCol000)

            'Dim TextCol1 As New DataGridTextBoxColumn
            'With TextCol1
            '    .MappingName = "NameMahal"
            '    .HeaderText = "نام مرکز پخش"
            '    .Width = 150
            '    .Alignment = HorizontalAlignment.Left
            'End With
            'tableStyleTitr.GridColumnStyles.Add(TextCol1)

            Dim TextCol1 As New DataGridTextBoxColumn
            With TextCol1
                .MappingName = "JamMablagh"
                .HeaderText = "جمع مبلغ"
                .Width = 150
                .Format = "###,###"
                .Alignment = HorizontalAlignment.Left
            End With
            tableStyleTitr.GridColumnStyles.Add(TextCol1)







            Dim TextCol06 As New DataGridTextBoxColumn
            With TextCol06
                .MappingName = "ShSefaresh"
                .HeaderText = "شماره سفارش"
                .Width = 100
                .Alignment = HorizontalAlignment.Center
            End With
            tableStyleTitr.GridColumnStyles.Add(TextCol06)

            Dim TextCol13 As New DataGridTextBoxColumn
            With TextCol13
                .MappingName = "Username"
                .HeaderText = "نام کاربر"
                .Width = 100
                .Alignment = HorizontalAlignment.Left
            End With
            tableStyleTitr.GridColumnStyles.Add(TextCol13)

            Dim TextCol12 As New DataGridTextBoxColumn
            With TextCol12
                .MappingName = "Date"
                .HeaderText = "تاریخ"
                .Width = 100
                .Alignment = HorizontalAlignment.Left
            End With
            tableStyleTitr.GridColumnStyles.Add(TextCol12)

            dbgTitr.Visible = True
            dbgTitr.RowHeaderWidth = 20
            dbgTitr.AllowSorting = False
            dbgTitr.TableStyles.Clear()
            dbgTitr.TableStyles.Add(tableStyleTitr)
            '=========================================================================================='
            Dim tableStyleSatr As New DataGridTableStyle
            tableStyleSatr.MappingName = "tblAN_ResidMavadAvaliehSatr"
            tableStyleSatr.RowHeaderWidth = 20
            tableStyleSatr.AllowSorting = True
            dbgSatr.BorderStyle = BorderStyle.Fixed3D

            Dim TextCol25 As New DataGridTextBoxColumn
            With TextCol25
                .MappingName = "Radif"
                .HeaderText = "رديف"
                .Width = 70
                .ReadOnly = True
                .Alignment = HorizontalAlignment.Center
            End With
            tableStyleSatr.GridColumnStyles.Add(TextCol25)




            Dim TextCol35 As New DataGridTextBoxColumn
            With TextCol35
                .MappingName = "CodeKala"
                .HeaderText = "کد کالا"
                .Width = 50
                .ReadOnly = True
                .Alignment = HorizontalAlignment.Left
            End With
            tableStyleSatr.GridColumnStyles.Add(TextCol35)

            Dim TextCol36 As New DataGridTextBoxColumn
            With TextCol36
                .MappingName = "NameKala"
                .HeaderText = "نام کالا"
                .Width = 50
                .ReadOnly = True
                .Alignment = HorizontalAlignment.Left
            End With
            tableStyleSatr.GridColumnStyles.Add(TextCol36)

            Dim TextCol40 As New DataGridTextBoxColumn
            With TextCol40
                .MappingName = "txtVahed"
                .HeaderText = "واحد کالا"
                .Width = 150
                .ReadOnly = True
                .Alignment = HorizontalAlignment.Left
            End With
            tableStyleSatr.GridColumnStyles.Add(TextCol40)
            Dim IsFastFood As Boolean = objTools.ConvertNulls(objTools.DLookup("IsFastFood", "dbo.tblGL_SysConfig", "CodeMahal  = " & CodeMahalFaal), False)
            If IsFastFood = True Then
                Dim TextCol50 As New DataGridTextBoxColumn
                With TextCol50
                    .MappingName = "Tedad1"
                    .HeaderText = "وزن کالا"
                    .Width = 100
                    .ReadOnly = False
                    .Alignment = HorizontalAlignment.Center
                    .Format = "###,###.##"
                End With
                tableStyleSatr.GridColumnStyles.Add(TextCol50)
            Else
                Dim TextCol50 As New DataGridTextBoxColumn
                With TextCol50
                    .MappingName = "Tedad1"
                    .HeaderText = "تعداد کالا"
                    .Width = 100
                    .ReadOnly = False
                    .Alignment = HorizontalAlignment.Center
                    .Format = "###,###.##"
                End With
                tableStyleSatr.GridColumnStyles.Add(TextCol50)
            End If


            If objTools.DLookup("ShowGheymatInAnbar", "tblGL_SysConfig", "CodeMahal =" & CodeMahalFaal) <> 2 Then
                Dim TextCol51 As New DataGridTextBoxColumn
                With TextCol51
                    .MappingName = "MablaghKharid"
                    .HeaderText = "قیمت خرید"
                    .Width = 100
                    .ReadOnly = False
                    .Alignment = HorizontalAlignment.Center
                    .Format = "###,###"
                End With
                tableStyleSatr.GridColumnStyles.Add(TextCol51)
            End If


            Dim TextCol47 As New DataGridTextBoxColumn
            With TextCol47
                .MappingName = "MKOL"
                .HeaderText = "جمع قیمت کالا "
                .Width = 110
                .ReadOnly = False
                .Alignment = HorizontalAlignment.Left
                .Format = "###,###"
            End With
            tableStyleSatr.GridColumnStyles.Add(TextCol47)


            'Dim TextCol46 As New DataGridTextBoxColumn
            'With TextCol46
            '    .MappingName = "TedadMandehDarResid"
            '    .HeaderText = "تعداد مانده در سفارش"
            '    .Width = 110
            '    .ReadOnly = False
            '    .Alignment = HorizontalAlignment.Left
            '    .Format = "###.##"
            'End With
            'tableStyleSatr.GridColumnStyles.Add(TextCol46)

            'Dim TextCol460 As New DataGridTextBoxColumn
            'With TextCol460
            '    .MappingName = "TedadKolResidShodeh"
            '    .HeaderText = "تعداد کل رسید شده"
            '    .Width = 100
            '    .ReadOnly = False
            '    .Alignment = HorizontalAlignment.Left
            '    .Format = "###.##"
            'End With
            'tableStyleSatr.GridColumnStyles.Add(TextCol460)

            Dim TextCol45 As New DataGridTextBoxColumn
            With TextCol45
                .MappingName = "ShomarehBach"
                .HeaderText = "شماره بچ"
                .Width = 100
                .ReadOnly = False
                .Alignment = HorizontalAlignment.Left
            End With
            tableStyleSatr.GridColumnStyles.Add(TextCol45)

            Dim TextCol41 As New DataGridTextBoxColumn
            With TextCol41
                .MappingName = "TarikhTolidSlash"
                .HeaderText = "تاریخ تولید"
                .Width = 100
                .ReadOnly = False
                .Alignment = HorizontalAlignment.Center
            End With
            tableStyleSatr.GridColumnStyles.Add(TextCol41)

            Dim TextCol42 As New DataGridTextBoxColumn
            With TextCol42
                .MappingName = "TarikhEnghezaSlash"
                .HeaderText = "تاریخ انقضا"
                .Width = 100
                .ReadOnly = False
                .Alignment = HorizontalAlignment.Center
            End With
            tableStyleSatr.GridColumnStyles.Add(TextCol42)

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
            'Search(False)
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->cmTitr_ItemChanged")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->cmTitr_ItemChanged")
        End Try
    End Sub
    Private Sub cmTitr_PositionChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            SetFormTitrData()
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
    Private Function IsValidBeforSaveTitr(ByVal CheckField As String) As Boolean
        Try
            IsValidBeforSaveTitr = False
            If CheckField = "mskTarikhForm" Or CheckField = "All" Then
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
            'If txtShomarehSefaresh.Text <> "" Then
            '    If objTools.ConvertNulls(objTools.DLookup("ccTaminKonandeh", "tblAN_kdxSefaresh", " CodeMahal = " & CodeMahalFaal & " AND CodeDoreh=" & IIf(IsNothing(cmbsCodeDorehSefaresh.SelectedValue), 0, cmbsCodeDorehSefaresh.SelectedValue) & " AND ShomarehForm= " & IIf(txtShomarehSefaresh.Text = "", 0, txtShomarehSefaresh.Text)), 0) = 0 Then
            '        ErrPro.SetError(Me.txtShomarehSefaresh, "شماره سفارشی که وارد کرده اید وجود ندارد.")
            '        MsgBox("شماره سفارشی که وارد کرده اید وجود ندارد.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
            '        Exit Function
            '    End If

            '    If objTools.ConvertNulls(objTools.DLookup("sVazeiat", "tblAN_kdxSefaresh", "CodeMahal = " & CodeMahalFaal & " AND CodeDoreh=" & IIf(IsNothing(cmbsCodeDorehSefaresh.SelectedValue), 0, cmbsCodeDorehSefaresh.SelectedValue) & " AND ShomarehForm= " & IIf(txtShomarehSefaresh.Text = "", 0, txtShomarehSefaresh.Text)), 0) <> 3 Then
            '        ErrPro.SetError(Me.txtShomarehSefaresh, "شماره سفارشی که وارد کرده اید تایید نشده است .")
            '        MsgBox("شماره سفارشی که وارد کرده اید تایید نشده است .", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
            '        Exit Function
            '    End If


            'End If

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
    Private Function AddNewRecord() As Boolean
        Try
            AddNewRecord = False

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

            Dim ccKardexTitrSefaresh As Integer = 0
            Dim ShomarehForm As Integer = objTools.DLookup("Top(1) ShomarehForm", "tblAN_ResidMavadAvalieh", "CodeDoreh = " & CodeDoreh & " AND CodeMahal = " & CodeMahalFaal & " ORDER By ccKardexTitr DESC ") + 1

            strSQL = "Insert Into " & FormTableName
            strSQL &= "(CodeMahal,CodeDoreh,ccAnbar,ShomarehForm,TarikhForm,Tozihat,sVazeiat,"
            strSQL &= "CodeDorehSefaresh,ShomarehSefaresh,ccTaminKonandeh,NoeResid,ccMashin,ccMarkazPakhsh,"
            strSQL &= "NameFardTahvilGirandeh,NameFardTahvilDahandeh,UserName,Tarikh,Saat,ccSefareshTitr,Date) Values "
            strSQL &= "(" & CodeMahalFaal & ","
            strSQL &= CodeDoreh & ","
            strSQL &= IIf(IsNothing(cmbAnbar.SelectedValue), 0, cmbAnbar.SelectedValue) & ","
            strSQL &= ShomarehForm & ","
            strSQL &= "'" & mskTarikhForm.Text & "',"
            strSQL &= "'" & txtTozihat.Text & "',"
            strSQL &= UD_Dll.Enums.AN_VazeiatKDXResid.BedoneAmalyat & ","
            strSQL &= IIf(IsNothing(cmbsCodeDorehSefaresh.SelectedValue), CodeDoreh, cmbsCodeDorehSefaresh.SelectedValue) & ","
            strSQL &= IIf(txtShomarehSefaresh.Text = "", 0, txtShomarehSefaresh.Text) & ","
            strSQL &= IIf(IsNothing(cmbNameTaminKonandeh.SelectedValue), 0, cmbNameTaminKonandeh.SelectedValue) & ","
            strSQL &= 3 & ","
            strSQL &= "NULL" & ","
            strSQL &= IIf(IsNothing(cmbMarkazPakhsh.SelectedValue), "NULL", cmbMarkazPakhsh.SelectedValue) & ","
            strSQL &= "'" & cmbTahvilGirandeh.Text & "', "
            strSQL &= "'" & cmbTahvilDahandeh.Text & "',"

            strSQL &= "'" & UserName & "',"
            strSQL &= "'" & TarikhEmrooz & "',"
            strSQL &= "'" & Format(TimeOfDay, "HH:mm:ss") & "',"

            If (Me.cmbsCodeDorehSefaresh.SelectedValue <> 0 And Me.txtShomarehSefaresh.Text <> "") Then
                flgInsertSatrSefaresh = True
                ccKardexTitrSefaresh = objTools.ConvertNulls(objTools.DLookup("ccKardexTitr", "tblAN_KdxSefaresh", "NoeSefaresh=2 AND CodeMahal =" & CodeMahalFaal & " and CodeDoreh =" & IIf(IsNothing(cmbsCodeDorehSefaresh.SelectedValue), 0, cmbsCodeDorehSefaresh.SelectedValue) & " and ShomarehForm =" & IIf(txtShomarehSefaresh.Text = "", 0, txtShomarehSefaresh.Text)), 0)
                strSQL &= ccKardexTitrSefaresh & ",GetDate())"
            ElseIf (Me.cmbsCodeDorehSefaresh.SelectedValue <> 0 And Me.txtShomarehSefaresh.Text <> "") Then
                flgInsertSatrSefaresh = True
                ccKardexTitrSefaresh = objTools.ConvertNulls(objTools.DLookup("ccKardexTitr", "tblAN_KdxSefaresh", "NoeSefaresh=1 AND CodeMahal =" & CodeMahalFaal & " and CodeDoreh =" & IIf(IsNothing(cmbsCodeDorehSefaresh.SelectedValue), 0, cmbsCodeDorehSefaresh.SelectedValue) & " and ShomarehForm =" & IIf(txtShomarehSefaresh.Text = "", 0, txtShomarehSefaresh.Text)), 0)
                strSQL &= ccKardexTitrSefaresh & ",GetDate())"
            Else
                strSQL &= "NULL,GetDate())"
            End If

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.ExecuteNonQuery()
            tCodeCounter = objTools.GetScopeIdentity(cnSQL)

            ' Insert Satr Kala From Sefaresh
            If (flgInsertSatrSefaresh) And (IsNothing(cmbsCodeDorehSefaresh.SelectedValue) = False) And txtShomarehSefaresh.Text <> "" Then
                If MsgBox("آیا مایلید کالاهای موجود در سفارش به رسید اضافه شود؟", MsgBoxStyle.YesNo + MsgBoxStyle.Question + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.DefaultButton2, "تایید") = MsgBoxResult.Yes Then
                    CreateSatrFromSefaresh(tCodeCounter, ccKardexTitrSefaresh, cmbNameTaminKonandeh.SelectedValue)
                End If
            End If

            cnSQL.Close()
            cmSQL = Nothing : cnSQL = Nothing

            Mode = UD_Dll.Enums.GL_ModeForms.None
            Return True
        Catch sqlEx As SqlException
            If sqlEx.ErrorCode = -2146232060 Then
                MsgBox("این شماره سفارش در سیستم موجود نمی باشد.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "خطا در عملیات ذخیره سازی")
            Else
                MsgBox(sqlEx.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "Error IN ---->AddNewRecord")
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->AddNewRecord")
        End Try
    End Function
    Private Function UpdateRecord() As Boolean
        Try
            UpdateRecord = False
            If Not IsValidBeforSaveTitr("All") Then
                Exit Function
                Return False
            End If
            '
            Dim cnSQL As SqlConnection
            Dim cmSQL As SqlCommand
            Dim strSqlTitr

            objCode.SabteTaghirat(CodeMahalFaal, UD_Dll.Enums.GL_NoeTaghir.UpdateRecord, "tblAN_KdxResid", dvTitr(cmTitr.Position)("ccKardexTitr"), dvTitr(cmTitr.Position)("ShomarehForm"), "بروز رساني ")

            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()
            strSqlTitr = "Update " & FormTableName
            strSqlTitr = strSqlTitr & " Set "
            strSqlTitr &= " ccAnbar = " & IIf(IsNothing(cmbAnbar.SelectedValue), 0, cmbAnbar.SelectedValue) & ","
            strSqlTitr &= " TarikhForm = '" & mskTarikhForm.Text & "',"
            strSqlTitr &= " NoeResid = " & 3 & ","
            strSqlTitr &= " ccTaminKonandeh = " & IIf(IsNothing(cmbNameTaminKonandeh.SelectedValue), 0, cmbNameTaminKonandeh.SelectedValue) & ","
            strSqlTitr &= " CodeDorehSefaresh = " & IIf(IsNothing(cmbsCodeDorehSefaresh.SelectedValue), "NULL", cmbsCodeDorehSefaresh.SelectedValue) & ","
            strSqlTitr &= " ShomarehSefaresh = " & IIf(txtShomarehSefaresh.Text = "", 0, txtShomarehSefaresh.Text) & ","
            strSqlTitr &= " ccMarkazPakhsh = " & IIf(IsNothing(cmbMarkazPakhsh.SelectedValue), "NULL", cmbMarkazPakhsh.SelectedValue) & ","
            strSqlTitr &= " NameFardTahvilGirandeh = '" & cmbTahvilDahandeh.Text & "',"
            strSqlTitr &= " NameFardTahvilDahandeh = '" & cmbTahvilGirandeh.Text & "',"
            strSqlTitr &= " Tozihat = '" & txtTozihat.Text & "',"
            strSqlTitr &= " ccSefareshTitr = " & objTools.ConvertNulls(objTools.DLookup("ccKardexTitr", "tblAN_KdxSefaresh", "CodeMahal = " & CodeMahalFaal & " and CodeDoreh = " & IIf(IsNothing(cmbsCodeDorehSefaresh.SelectedValue), 0, cmbsCodeDorehSefaresh.SelectedValue) & " and ShomarehForm = " & IIf(txtShomarehSefaresh.Text = "", 0, txtShomarehSefaresh.Text)), "NULL")
            strSqlTitr &= " Where ccKardexTitr = " & dvTitr(cmTitr.Position)("ccKardexTitr")

            cmSQL = New SqlCommand(strSqlTitr, cnSQL)
            cmSQL.ExecuteNonQuery()

            cnSQL.Close()
            cmSQL = Nothing : cnSQL = Nothing

            Mode = UD_Dll.Enums.GL_ModeForms.None
            Return True

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->UpdateRecord")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->UpdateRecord")
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


            strSQLRow = "ccKardexTitr = " & dvTitr(cmTitr.Position)("ccKardexTitr")

            If objTools.ConvertNulls(objTools.DLookup("ccKardexTitr", "tblAN_kdxResidSatr", strSQLRow), "-1") = -1 Then
                strSQLTitr = "Delete From tblAN_kdxResid Where ccKardexTitr =" & dvTitr(cmTitr.Position)("ccKardexTitr")
                cmSQL = New SqlCommand(strSQLTitr, cnSQL)
                cmSQL.ExecuteNonQuery()
                cnSQL.Close()
                cmSQL = Nothing : cnSQL = Nothing

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
            Dim cnSQL As SqlConnection
            Dim cmSQL As SqlCommand
            Dim strSQLRow As String
            Dim strSQLTitr As String


            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()

            strSQLRow = "Delete From tblAN_ResidMavadAvaliehSatr Where " & _
                " ccKardexTitr = " & dvTitr(cmTitr.Position)("ccKardexTitr")

            strSQLTitr = "Delete From tblAN_ResidMavadAvalieh Where ccKardexTitr = " & dvTitr(cmTitr.Position)("ccKardexTitr")

            cmSQL = New SqlCommand(strSQLRow, cnSQL)
            cmSQL.ExecuteNonQuery()

            cmSQL = New SqlCommand(strSQLTitr, cnSQL)
            cmSQL.ExecuteNonQuery()

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
    Private Function IsValidRow(ByVal chkField As String) As Boolean
        Try
            IsValidRow = False

            If Mode = UD_Dll.Enums.GL_ModeForms.AddNewRow Then
                If objTools.ConvertNulls(objTools.DLookup("ccKala", "qryAN_KalaAnbar", "CodeKala=" & Me.txtCodeKala.Text & " AND ccAnbar =" & dvTitr(cmTitr.Position)("ccAnbar")), 0) = 0 Then
                    ErrPro.SetError(Me.txtCodeKala, "کالای وارد شده در لیست کالاهای انبار مورد نظر وجود ندارد .")
                    MsgBox("کالای وارد شده در لیست کالاهای انبار مورد نظر وجود ندارد .", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
                    Exit Function
                End If
                ErrPro.SetError(txtCodeKala, "")
            End If

            'If Mode = UD_Dll.Enums.GL_ModeForms.AddNewRow Then
            '    If objTools.ConvertNulls(objTools.DLookup("ccKala", "qryAN_Kala", "CodeKala=" & Me.txtCodeKala.Text & " AND ccTaminKonandeh =" & dvTitr(cmTitr.Position)("ccTaminKonandeh")), 0) = 0 Then
            '        ErrPro.SetError(Me.txtCodeKala, "کالای وارد شده در لیست کالاهای تامین کننده مورد نظر وجود ندارد .")
            '        MsgBox("کالای وارد شده در لیست کالاهای تامین کننده مورد نظر وجود ندارد .", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
            '        Exit Function
            '    End If
            '    ErrPro.SetError(txtCodeKala, "")
            'End If
            If chkField = "txtCodeKala" Or chkField = "All" Then
                If Mode = UD_Dll.Enums.GL_ModeForms.AddNewRow Then
                    If objTools.ConvertNulls(objTools.DCount("ccKala", "tblAN_KdxResidSatr", "ccKala = " & txtCodeKala.Tag & " AND ccKardexTitr = " & dvTitr(cmTitr.Position)("ccKardexTitr")), 0) >= 1 Then
                        ErrPro.SetError(txtCodeKala, " کالا تکراری است.")
                        'If MsgBox("کالای مورد نظر دراین رسید وارد شده است !", MsgBoxStyle.OkOnly + MsgBoxStyle.Information + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.DefaultButton2, "توجه") = MsgBoxResult.Ok Then
                        MsgBox("کالای مورد نظر دراین سند صادر شده است", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "تایید")
                        txtCodeKala.Focus()
                        Exit Function
                        'End If
                    End If
                End If
                ErrPro.SetError(Me.txtCodeKala, "")

            End If

            If chkField = "txtCodeKala" Or chkField = "All" Then
                If Val(txtCodeKala.Tag) = 0 Then
                    ErrPro.SetError(txtCodeKala, "کد کالا را وارد کنيد.")
                    MsgBox("کد کالا را وارد کنيد.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
                    txtCodeKala.Focus()
                    Exit Function
                End If
                ErrPro.SetError(Me.txtCodeKala, "")
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

            If (objTools.ConvertNulls(objTools.DLookup("ccKala", "tblAn_Kala", "faal=0 and CodeKala='" & Me.txtCodeKala.Text & "'" & " AND ccTaminKonandeh = " & dvTitr(cmTitr.Position)("ccTaminKonandeh")), 0)) Then
                ErrPro.SetError(txtCodeKala, "کد کالا غیر فعال است .")
                MsgBox("کد کالای فعال را وارد کنيد.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
                txtCodeKala.Focus()
                Exit Function
            End If
            ErrPro.SetError(Me.txtCodeKala, "")


            If chkField = "txtMablaghKharid" Or chkField = "All" Then
                If (txtMablaghKharid.Text.Length = 0) Or Val(txtMablaghKharid.Text) = 0 Then
                    ErrPro.SetError(Me.txtMablaghKharid, "مبلغ خرید را وارد کنید.")
                    MsgBox("مبلغ خرید را وارد کنید.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
                    txtMablaghKharid.Focus()
                    Exit Function
                    ErrPro.SetError(Me.txtMablaghKharid, "")
                End If
            End If

            ' Check Kardan Sys Config  baraye vared kardan ya nakardane Shomareh Bach
            Select Case objCode.CheckShomarehBach
                Case UD_Dll.Enums.GL_SysConfig.VaredShavad
                    If chkField = "txtShomarehBatch" Or chkField = "All" Then
                        If Me.txtShomarehBatch.Text = "" Then
                            ErrPro.SetError(Me.txtShomarehBatch, "شماره بچ را وارد کنید.")
                            MsgBox("شماره بچ را وارد کنید.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
                            txtShomarehBatch.Focus()
                            Exit Function
                        End If
                        ErrPro.SetError(Me.txtShomarehBatch, "")
                    End If

                    Dim ccKalaShomarehBach As Integer = objTools.ConvertNulls(objTools.DLookup("ccKala", "tblGL_ShomarehBach", "ShomarehBach = '" & txtShomarehBatch.Text & "'"), 0)
                    If ccKalaShomarehBach = 0 Then
                        MsgBox("شماره بچی که شما وارد کرده اید به سیستم معرفی نشده است.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
                        txtShomarehBatch.Focus()
                        Exit Function
                    Else
                        If txtCodeKala.Tag <> ccKalaShomarehBach Then
                            MsgBox("این شماره بچ برای این کالا نمی باشد.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
                            txtShomarehBatch.Focus()
                            Exit Function
                        End If
                    End If
                Case UD_Dll.Enums.GL_SysConfig.VaredNashavad
                    If chkField = "txtShomarehBatch" Or chkField = "All" Then
                        If Me.txtShomarehBatch.Text <> "" Then
                            ErrPro.SetError(Me.txtShomarehBatch, "شماره بچ را نباید وارد کنید.")
                            MsgBox("شماره بچ را نباید وارد کنید.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
                            txtShomarehBatch.Focus()
                            Exit Function
                        End If
                        ErrPro.SetError(Me.txtShomarehBatch, "")
                    End If
                Case UD_Dll.Enums.GL_SysConfig.MohemNist

                    If Me.txtShomarehBatch.Text = "" Then
                        ErrPro.SetError(Me.txtShomarehBatch, "شماره بچ را وارد کنید.")
                        MsgBox("شماره بچ را وارد کنید.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
                        txtShomarehBatch.Focus()
                        Exit Function
                    End If
                    ErrPro.SetError(Me.txtShomarehBatch, "")
            End Select

            ' Check Kardan Sys Config  baraye vared kardan ya nakardane tarikh masraf
            Select Case objCode.CheckTarikhMasraf
                Case UD_Dll.Enums.GL_SysConfig.VaredShavad
                    If chkField = "mskTarikhTolid" Or chkField = "All" Then
                        If mskTarikhTolid.Text.Length <> 0 Then
                            If Not objTarikh.IsShDate(mskTarikhTolid.Text.ToString) Then
                                mskTarikhEngheza.Focus()
                                Exit Function
                            End If
                        Else
                            ErrPro.SetError(Me.mskTarikhTolid, "تاريخ تولید را وارد کنيد.")
                            MsgBox("تاريخ تولید را وارد کنيد.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
                            mskTarikhTolid.Focus()
                            Exit Function
                        End If
                        ErrPro.SetError(Me.mskTarikhTolid, "")
                    End If

                    If chkField = "mskTarikhEngheza" Or chkField = "All" Then
                        If mskTarikhTolid.Text.Length <> 0 Then
                            If Not objTarikh.IsShDate(mskTarikhEngheza.Text.ToString) Then
                                mskTarikhEngheza.Focus()
                                Exit Function
                            End If
                        Else
                            ErrPro.SetError(Me.mskTarikhEngheza, "تاريخ انقضاء را وارد کنيد.")
                            MsgBox("تاريخ انقضاء را وارد کنيد.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
                            mskTarikhEngheza.Focus()
                            Exit Function
                        End If
                        ErrPro.SetError(Me.mskTarikhEngheza, "")
                    End If

                Case UD_Dll.Enums.GL_SysConfig.VaredNashavad

                    If chkField = "mskTarikhTolid" Or chkField = "All" Then
                        If mskTarikhTolid.Text.Length <> 0 Then
                            ErrPro.SetError(Me.mskTarikhTolid, "تاريخ تولید را نباید وارد کنيد.")
                            MsgBox("تاريخ تولید را نباید وارد کنيد.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
                            mskTarikhTolid.Focus()
                            Exit Function
                        End If
                        ErrPro.SetError(Me.mskTarikhTolid, "")
                    End If

                    If chkField = "mskTarikhEngheza" Or chkField = "All" Then
                        If mskTarikhEngheza.Text.Length <> 0 Then
                            ErrPro.SetError(Me.mskTarikhEngheza, "تاريخ انقضاء را نباید وارد کنيد.")
                            MsgBox("تاريخ انقضاء را نباید وارد کنيد.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
                            mskTarikhEngheza.Focus()
                            Exit Function
                        End If
                        ErrPro.SetError(Me.mskTarikhEngheza, "")
                    End If

            End Select


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

            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()

            Dim Tedad As Double = objCode.ReturnTedad(txtCodeKala.Tag, Val(txtTedad.Text), Val(txtTedadBasteh.Text), Val(txtTedadKarton.Text))

            strSQL = "INSERT into " & FormTableNameSatr
            strSQL &= "( ccKardexTitr,Radif,ccKala,Tedad1,Tedad2,Tedad3,Tedad0,"
            strSQL &= "TedadBasteh,TedadKarton,sVahed,MablaghKharid,MablaghFaktor,TarikhTolid,TarikhEngheza,ShomarehBach)"
            strSQL &= " VALUES ("
            strSQL &= dvTitr(cmTitr.Position)("ccKardexTitr") & ","
            strSQL &= objTools.ConvertNulls(objTools.DMax("Radif", "tblAN_residmavadavaliehSatr", "ccKardexTitr=" & dvTitr(cmTitr.Position)("ccKardexTitr")), 0) + 1 & ","
            strSQL &= IIf(IsNothing(txtCodeKala.Tag), 0, txtCodeKala.Tag) & ","

            strSQL &= Tedad.ToString.Replace(",", ".") & ","
            strSQL &= Tedad.ToString.Replace(",", ".") & ","
            strSQL &= Tedad.ToString.Replace(",", ".") & ","


            strSQL &= Val(txtTedad.Text) & ","
            strSQL &= Val(txtTedadBasteh.Text) & ","
            strSQL &= Val(txtTedadKarton.Text) & ","

            strSQL &= 0 & ","
            strSQL &= IIf(Me.txtMablaghKharid.Text.Length = 0, 0, Me.txtMablaghKharid.Text.Replace(",", ".")) & ","
            strSQL &= IIf(Me.txtMablaghKharid.Text.Length = 0, 0, Me.txtMablaghKharid.Text.Replace(",", ".")) & ","
            strSQL &= "'" & mskTarikhTolid.Text & "',"
            strSQL &= "'" & mskTarikhEngheza.Text & "',"
            strSQL &= "'" & IIf(Me.txtShomarehBatch.Text.Length = 0, "", Me.txtShomarehBatch.Text) & "')"

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.ExecuteNonQuery()
            cnSQL.Close()
            cmSQL = Nothing : cnSQL = Nothing
            ClearForm()
            RefreshSatrData()
            Mode = UD_Dll.Enums.GL_ModeForms.None
            SetFormObject()

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->AddNewRow")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->AddNewRow")
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

            Dim Tedad As Double = objCode.ReturnTedad(txtCodeKala.Tag, Val(txtTedad.Text), Val(txtTedadBasteh.Text), Val(txtTedadKarton.Text))

            strSQL = "Update " & FormTableNameSatr
            strSQL = strSQL & " Set " & _
                    " ccKala = " & IIf(IsNothing(txtCodeKala.Tag), 0, txtCodeKala.Tag) & "," & _
                    " Tedad1 = " & Tedad.ToString.Replace(",", ".") & "," & _
                    " Tedad2 = " & Tedad.ToString.Replace(",", ".") & "," & _
                    " Tedad3 = " & Tedad.ToString.Replace(",", ".") & "," & _
                    " Tedad0 = " & Val(txtTedad.Text.ToString.Replace(",", ".")) & "," & _
                    " TedadBasteh = " & Val(txtTedadBasteh.Text.ToString.Replace(",", ".")) & "," & _
                    " TedadKarton = " & Val(txtTedadKarton.Text.ToString.Replace(",", ".")) & "," & _
                    " sVahed = " & 0 & "," & _
                    " MablaghKharid = " & IIf(Me.txtMablaghKharid.Text.Length = 0, 0, Me.txtMablaghKharid.Text.Replace(",", ".")) & "," & _
                    " MablaghFaktor = " & IIf(Me.txtMablaghKharid.Text.Length = 0, 0, Me.txtMablaghKharid.Text.Replace(",", ".")) & "," & _
                    " TarikhTolid = '" & mskTarikhTolid.Text & "'," & _
                    " TarikhEngheza = '" & mskTarikhEngheza.Text & "'," & _
                    " ShomarehBach = '" & IIf(Me.txtShomarehBatch.Text.Length = 0, "", Me.txtShomarehBatch.Text) & "'" & _
                    "  WHERE ccKardexSatr = " & dvSatr(cmSatr.Position)("ccKardexSatr")

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
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->UpdateRow")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->UpdateRow")
        End Try
    End Sub
    Private Sub DeleteRow()
        Try
            Dim cnSQL As SqlConnection
            Dim cmSQL As SqlCommand
            Dim strSQL As String

            If cmSatr.Position < 0 Then
                MsgBox("هیچ سطری برای حذف وجود ندارد.", MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, "پیام")
                Exit Sub
            End If
            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()
            strSQL = "Delete From tblAN_ResidMavadAvaliehSatr Where ccKardexSatr=" & dvSatr(cmSatr.Position)("ccKardexSatr")
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

                    btnNewRow.Visible = False
                    btnDeleteRow.Visible = False
                    btnEditRow.Visible = False
                    btnSaveRow.Visible = True
                    btnCancelRow.Visible = True
                Case UD_Dll.Enums.GL_ModeForms.UpdateRow
                    btnSearch.Enabled = False
                    btnFTitr.Enabled = False
                    btnLTitr.Enabled = False
                    btnFSatr.Enabled = False
                    btnLSatr.Enabled = False

                    btnNewRow.Visible = False
                    btnDeleteRow.Visible = False
                    btnEditRow.Visible = False
                    btnSaveRow.Visible = True
                    btnCancelRow.Visible = True
                Case UD_Dll.Enums.GL_ModeForms.None
                    btnSearch.Enabled = True
                    btnFTitr.Enabled = True
                    btnLTitr.Enabled = True
                    btnFSatr.Enabled = True
                    btnLSatr.Enabled = True

                    If dvTitr.Count > 0 Then
                        btnNewRow.Visible = True
                    Else
                        btnNewRow.Visible = False
                    End If
                    btnSaveRow.Visible = False
                    btnCancelRow.Visible = False
                    If Not dvSatr Is Nothing Then
                        If dvSatr.Count > 0 Then
                            btnDeleteRow.Visible = True
                            btnEditRow.Visible = True
                        End If
                    Else
                        btnDeleteRow.Visible = False
                        btnEditRow.Visible = False
                    End If
                Case Else
                    btnNewRow.Visible = False
                    btnEditRow.Visible = False
                    btnDeleteRow.Visible = False
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
                    Me.cmbsCodeDorehSefaresh.Focus()
                    mskTarikhForm.Text = TarikhEmrooz
                Case UD_Dll.Enums.GL_ModeForms.AddNewRow
                    dbgSatr.Height = SatrGridSize
                    dbgTitr.Enabled = False
                    dbgSatr.Enabled = False
                Case UD_Dll.Enums.GL_ModeForms.UpdateRow
                    dbgSatr.Height = SatrGridSize
                    dbgTitr.Enabled = False
                    dbgSatr.Enabled = False
                Case UD_Dll.Enums.GL_ModeForms.UpdateRecord
                    dbgTitr.Height = TitrGridSize
                    dbgTitr.Enabled = False
                    dbgSatr.Enabled = False
                    Me.cmbAnbar.Focus()
            End Select
            btnLSatr.Top = dbgSatr.Height + dbgSatr.Top - btnLSatr.Height
            btnLTitr.Top = dbgTitr.Height + dbgTitr.Top - btnLTitr.Height
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->SetFormObject")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->SetFormObject")
        End Try
    End Sub
    Private Sub PrintKdxResid()
        Try
            Dim cnSQL As SqlConnection
            Dim strSQL As String

            Windows.Forms.Cursor.Current = Cursors.WaitCursor
            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()

            strSQL = "select * from qryAN_KdxResidTitrSatr Where "
            strSQL &= " ccKardexTitr = " & dvTitr(cmTitr.Position)("ccKardexTitr") & " AND Tedad3 <> 0"
            strSQL &= " Order by ccKardexSatr"

            If dsForm.Tables.Contains("qryAN_KdxResidTitrSatr") Then
                dsForm.Tables.Remove("qryAN_KdxResidTitrSatr")
            End If

            Dim daSQL As SqlDataAdapter
            daSQL = New SqlDataAdapter(strSQL, cnSQL)
            daSQL.Fill(dsForm, "qryAN_KdxResidTitrSatr")

            Dim rpt As New CrystalDecisions.CrystalReports.Engine.ReportDocument
            Dim rpttables As CrystalDecisions.CrystalReports.Engine.Tables
            Dim rptformula As CrystalDecisions.CrystalReports.Engine.FormulaFieldDefinitions
            Dim frm As New Forms_dll.frmGL_Gozaresh

            rpt.Load(rptPath & "\rptAN_GozareshResid.rpt")

            rpttables = rpt.Database.Tables
            rpttables.Item(0).SetDataSource(dsForm.Tables("qryAN_KdxResidTitrSatr"))

            rptformula = rpt.DataDefinition.FormulaFields
            With rptformula

                .Item("Group_Sanad").Text = "{mydata.ccKardexTitr}"
                .Item("Sh").Text = "{mydata.ShomarehForm}"
                .Item("Tarikh").Text = "{mydata.TarikhFormSlash}"
                .Item("ShomarehSefaresh").Text = "{mydata.ShSefaresh}"
                .Item("Anbar").Text = "{mydata.NameAnbar}"
                .Item("txtNoeResidSharh").Text = "{mydata.txtNoeResidSharh}"
                .Item("txtVazeiat").Text = "{mydata.txtVazeiat}"
                .Item("NameKala").Text = "trim({mydata.NameKala})"
                .Item("Tedad").Text = "{mydata.Tedad3}"
                .Item("CodeKala").Text = "{mydata.CodeKala}"

                Select Case objCode.CheckMoshahedehGheymatInRepAnbar()
                    Case UD_Dll.Enums.GL_SysConfigShowMablaghInRepAnbar.Faal
                        .Item("MablaghKharid").Text = "{mydata.MablaghKharid}"
                    Case UD_Dll.Enums.GL_SysConfigShowMablaghInRepAnbar.NoFaal
                        .Item("MablaghKharid").Text = "0"
                End Select

                .Item("TarikhTolidSlash").Text = "{mydata.TarikhTolidSlash}"
                .Item("TarikhEnghezaSlash").Text = "{mydata.TarikhEnghezaSlash}"
                .Item("ShomarehBach").Text = "{mydata.ShomarehBach}"

                .Item("Title").Text = "'" & "گزارش رسید انبار" & "'"
                .Item("Title2").Text = "'" & NameSherkat & "'"
                .Item("Title3").Text = "'" & NameMahalFaal & "'"
                .Item("KarbarGozaresh").Text = "'" & PersonelName & "'"
                .Item("TarikhGozaresh").Text = "'" & objTarikh.SetDateSlash(TarikhEmrooz) & "'"
                .Item("SaatGozaresh").Text = "'" & Format(TimeOfDay, "HH:mm:ss") & "'"
                .Item("Tozihat").Text = "{mydata.Tozihat}"


            End With
            rpt.Refresh()

            frm.Text = "رسید انبار"

            frm.WindowState = FormWindowState.Maximized
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
    Private Sub CreateSatrFromSefaresh(ByVal ccKardexTitr As Integer, ByVal ccKardexTitrSefaresh As Integer, ByVal ccTaminkonandeh As Integer)
        Try
            Dim cnSQL As SqlConnection
            Dim cmSQL As SqlCommand
            Dim strSQL As String

            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()

            strSQL = "Insert Into tblAN_ResidMavadAvaliehSatr "
            strSQL &= " (ccKardexTitr,Radif,ccKala,Tedad1,Tedad2,Tedad3,MablaghKharid,Tedad0,TedadKarton,TedadBasteh) "

            strSQL &= " Select " & ccKardexTitr & " as ccKardexTitr, RANK() OVER (PARTITION BY 1 order by ccKala) as Radif,ccKala ,"
            strSQL &= " Tedad3-isnull((select sum(tedad3) from dbo.qryAN_ResidMavadAvaliehTitrSatr where cckala =qryAN_KdxSefareshSatr.ccKala"
            strSQL &= " and ccSefareshTitr=" & ccKardexTitrSefaresh & " ),0) as Tedad1,"
            strSQL &= " Tedad3-isnull((select sum(tedad3) from dbo.qryAN_ResidMavadAvaliehTitrSatr where cckala =qryAN_KdxSefareshSatr.ccKala"
            strSQL &= " and ccSefareshTitr=" & ccKardexTitrSefaresh & " ),0) as Tedad2,"
            strSQL &= " Tedad3-isnull((select sum(tedad3) from dbo.qryAN_ResidMavadAvaliehTitrSatr where cckala =qryAN_KdxSefareshSatr.ccKala"
            strSQL &= " and ccSefareshTitr=" & ccKardexTitrSefaresh & " ),0) as Tedad3,"
            strSQL &= " isnull((select dbo.[GetMablaghKharid](qryAN_KdxSefareshSatr.ccKala,'" & TarikhEmrooz & "'," & CodeMahalFaal & "," & ccTaminkonandeh & ")),0) as MablaghKharid "
            strSQL &= " ,TedadKhord,TedadKarton,TedadBasteh"
            strSQL &= " From qryAN_KdxSefareshSatr Where ccKardexTitr = " & ccKardexTitrSefaresh
            strSQL &= " and "
            strSQL &= " (Tedad3-isnull((select sum(tedad3) from dbo.qryAN_ResidMavadAvaliehTitrSatr where cckala =qryAN_KdxSefareshSatr.ccKala"
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
    Private Sub SetParameter()

        Dim commands As String = Microsoft.VisualBasic.Command()
        If commands.Length = 0 Then

            UserName = "Administrator"
            UserPassWord = "66998833"
            NameMahalFaal = "تهران"
            CodeMahalFaal = "1"
            PersonelCode = "0"
            PersonelName = "Administrator"
            CodeDoreh = "1393"
            txtCaption = "رســـید از تــامین کننـــده"
            objCode.UserName = UserName

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
        Try
            Mode = UD_Dll.Enums.GL_ModeForms.AddNewRow
            If Mode = UD_Dll.Enums.GL_ModeForms.AddNewRow Then
                txtCodeKala.Enabled = True
            End If
            SetFormObject()
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
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->btnCancel_Click")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->btnCancel_Click")
        End Try
    End Sub
    Private Sub btnCancelSanad_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelSanad.Click
        Try
            Mode = UD_Dll.Enums.GL_ModeForms.None

            SetFormObject()
            Search(False, False)
            SetFormTitrData()
            ClearForm()

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
        Try
            Mode = UD_Dll.Enums.GL_ModeForms.AddNewRecord
            ClearForm()
            Search(False, False)
            dbgSatr.DataSource = Nothing

            SetFormObject()
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->btnNewSanad_Click")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->btnNewSanad_Click")
        End Try
    End Sub
    Private Sub btnEditSanad_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEditSanad.Click
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
        Try
            Select Case Mode
                Case UD_Dll.Enums.GL_ModeForms.AddNewRecord
                    If Not objSec.HaveUserPermission(SN, UD_Dll.Security.ePermissions.Add) Then Exit Sub
                    If AddNewRecord() = False Then
                        Exit Sub
                    Else
                        If Not flgInsertSatrSefaresh Then
                            'InsertAllKala()
                            FirstInsert = True
                        End If
                    End If
                Case UD_Dll.Enums.GL_ModeForms.UpdateRecord
                    If Not objSec.HaveUserPermission(SN, UD_Dll.Security.ePermissions.Update) Then Exit Sub
                    If UpdateRecord() = False Then
                        Exit Sub
                    End If
            End Select
            Search(False)
            ClearForm()
            SetFormObject()
            SetFormTitrData()

            flg = False
            'LoadComboKala()
            flg = True
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
        If Not objSec.HaveUserPermission(SN, UD_Dll.Security.ePermissions.DeleteSatr) Then Exit Sub
        If Not objSec.HaveUserPermission(SN, UD_Dll.Security.ePermissions.Delete) Then Exit Sub

        Try
            If MsgBox("آيا حاضريد کل رسید حذف شود؟", MsgBoxStyle.YesNo + MsgBoxStyle.Question + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.DefaultButton2, "حذف رکورد") = MsgBoxResult.No Then
                Exit Sub
            End If
            If MsgBox("آيا حاضريد کل رسید حذف شود؟", MsgBoxStyle.YesNo + MsgBoxStyle.Question + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.DefaultButton2, "حذف رکورد") = MsgBoxResult.Yes Then
                DeleteFull()
                Search(True)
            End If
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->btnDeleteTitr_Click")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->btnDeleteTitr_Click")
        End Try
    End Sub
    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        If Not objSec.HaveUserPermission(SN, UD_Dll.Security.ePermissions.Preview) Then Exit Sub
        Try
            If dvTitr.Count > 0 Then
                Me.TopMost = False
                PrintKdxResid()
                Me.TopMost = True
            End If
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->btnPrintM_Click")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->btnPrintM_Click")
        End Try
    End Sub
#End Region

    Private Sub btnErsal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnErsal.Click
        Dim cnSQL As SqlConnection
        Dim cmSQL As SqlCommand
        Dim strSqlTitr

        Dim ccSatr As Integer = objTools.DCount("ccKardexSatr", "tblAN_ResidMavadAvaliehSatr", "ccKardexTitr= " & dvTitr(cmTitr.Position)("ccKardexTitr") & " ")
        If ccSatr = 0 Then
            MsgBox("رسید مورد نظر کالا ندارد.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "خطا")
            Exit Sub
        End If

        objCode.SabteTaghirat(CodeMahalFaal, UD_Dll.Enums.GL_NoeTaghir.UpdateRecord, "tblAN_ResidMavadAvalieh ", dvTitr(cmTitr.Position)("ccKardexTitr"), dvTitr(cmTitr.Position)("ShomarehForm"), "بروز رساني ")

        cnSQL = New SqlConnection(ConnectionString)
        cnSQL.Open()

        strSqlTitr = " Update " & FormTableName
        strSqlTitr &= " Set sVazeiat = 2 "
        strSqlTitr &= " Where ccKardexTitr = " & dvTitr(cmTitr.Position)("ccKardexTitr")

        cmSQL = New SqlCommand(strSqlTitr, cnSQL)
        cmSQL.ExecuteNonQuery()
        MsgBox("عمليــات با موفقیت انجــام شـــد .", MsgBoxStyle.MsgBoxRtlReading Or MsgBoxStyle.MsgBoxRight Or MsgBoxStyle.Information, "ذخيره")
        cnSQL.Close()
        cmSQL = Nothing : cnSQL = Nothing

        tpos = cmTitr.Position
        Search(False)
        cmTitr.Position = tpos
    End Sub

    Private Sub OpenForm(ByVal CodeSubSystem As Integer)

        If CodeSubSystem = -1 Then
            Me.Close()
            Exit Sub
        End If
        Dim ExePath As String = Application.StartupPath + "\"
        '"e:\Projects\Pakhsh_M\Final_Exe_Dll\Exe\"

        Dim ExeName As String = objTools.DLookup("ExePath", "tblGL_NameSystemSub", " CodeSubSystem = " & CodeSubSystem)
        Dim FormText As String = ""
        Dim ExeNameWithOutExtension As String = ""
        If ExeName <> "" AndAlso ExeName.Trim.Substring(ExeName.Length - 4, 4).ToUpper = ".Exe".ToUpper Then
            ExeNameWithOutExtension = ExeName.Remove(ExeName.Length - 4, 4)
        End If
        ExePath &= ExeName

        Select Case CodeSubSystem
            Case 748
                'If CheckMemory(ExeNameWithOutExtension) Then Exit Sub
                Shell(ExePath & " " & UserName & ";" & UserCode & ";" & UserPassWord & ";" & NameMahalFaal & ";" & CodeMahalFaal & ";" & PersonelCode & ";" & PersonelName & ";" & CodeDoreh & ";" & FormText, AppWinStyle.NormalFocus)
            Case 931
                'If CheckMemory(ExeNameWithOutExtension) Then Exit Sub
                Shell(ExePath & " " & UserName & ";" & UserCode & ";" & UserPassWord & ";" & NameMahalFaal & ";" & CodeMahalFaal & ";" & PersonelCode & ";" & PersonelName & ";" & CodeDoreh & ";" & FormText, AppWinStyle.NormalFocus)

            Case 889
                'If CheckMemory(ExeNameWithOutExtension) Then Exit Sub
                Shell(ExePath & " " & UserName & ";" & UserCode & ";" & UserPassWord & ";" & NameMahalFaal & ";" & CodeMahalFaal & ";" & PersonelCode & ";" & PersonelName & ";" & CodeDoreh & ";" & FormText, AppWinStyle.NormalFocus)
        End Select


    End Sub
    
End Class
