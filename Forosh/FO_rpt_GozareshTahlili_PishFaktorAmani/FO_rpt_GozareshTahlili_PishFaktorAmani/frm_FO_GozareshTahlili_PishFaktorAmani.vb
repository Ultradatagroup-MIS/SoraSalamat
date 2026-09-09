Imports CrystalDecisions.CrystalReports.Engine

Public Class frm_FO_rpt_GozareshTahlili_PishFaktorAmani

#Region "Variable AND Constant Declration"
    ' Quary And Table Names
    ' Security 
    Const cntCodeSubSystem As Long = 5000
    Private SN As Integer

    Dim ErrPro As New ErrorProvider
    Dim dsForm As New DataSet
    Dim dvForm As DataView
    Dim txtCaption As String
    Private WithEvents BS As New UD_Dll.PassString
    Dim flg As Boolean

#End Region
#Region " Form Event Code "
    Private Sub frmFO_ReportMoshtary_Closed(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Closed
        ' "Receive" parameter is the caption of destination window
        Dim hwnd As Long = UD_Dll.Code.FindWindow(vbNullString, ObjCode.GetNameSherkat)
        If hwnd <> 0 Then
            BS.PostString(hwnd, &H400, 0, txtCaption)
        End If

        dsForm = Nothing
        dvForm = Nothing
    End Sub
    Private Sub frmFO_ReportMoshtary_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{tab}")
        End If
    End Sub
    Private Sub frmFO_ReportMoshtary_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        Me.TopMost = True
    End Sub
    Private Sub frmFO_ReportMoshtary_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetParameter()
        LoadCombo()
        SN = objSec.GetSecurityNumber(cntCodeSubSystem, UserName)
        Try
            flg = True
            mskAzTarikh.Text = TarikhEmrooz
            mskTaTarikh.Text = TarikhEmrooz
            objTools.ChangeKbd(UD_Dll.mdlUtility.KBD.Farsi)
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->frmTakhsisha_Load")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->frmTakhsisha_Load")
        End Try
    End Sub
#End Region
#Region "Global Form Code"
    Private Sub SetReport()
        Try

            Dim cnSQL As SqlConnection
            Dim strSQL As String
            Dim daSQL As SqlDataAdapter
            Dim cmSQL As SqlCommand

            Dim frm As New Forms_dll.frmGL_Gozaresh

            Windows.Forms.Cursor.Current = Cursors.WaitCursor
            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()

            strSQL = "Report.spPishFaktorAmani_Tahlili"

            If dsForm.Tables.Contains("tblGozaresh") Then
                dsForm.Tables.Remove("tblGozaresh")
            End If

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()
  
            cmSQL.Parameters.AddWithValue("AzTarikh", mskAzTarikh.Text.Trim)
            cmSQL.Parameters.AddWithValue("TaTarikh", mskTaTarikh.Text.Trim)
            cmSQL.Parameters.AddWithValue("CodeDoreh", CodeDoreh)
            cmSQL.Parameters.AddWithValue("CodeMahal", CodeMahalFaal)
            cmSQL.Parameters.AddWithValue("ccForoshandeh", cmbBazaryabS.SelectedValue)
            cmSQL.Parameters.AddWithValue("ccMoshtary", txtCodeMoshtaryS.Tag)

            daSQL = New SqlDataAdapter(cmSQL)
            daSQL.Fill(dsForm, "tblGozaresh")


            If dsForm.Tables("tblGozaresh").Rows.Count = 0 Then
                MsgBox("هیـــــچ رکوردی برای گزارش پیدا نشد.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "پیام")
                Exit Sub
            End If

            Dim rpt As New ReportDocument
            Dim rpttables As Tables
            Dim rptformula As FormulaFieldDefinitions

            rpt.Load(rptPath & "\rptFO_GozareshTaminkonandehDaftar.rpt")


            rpttables = rpt.Database.Tables
            rpttables.Item(0).SetDataSource(dsForm.Tables("tblGozaresh"))

            rptformula = rpt.DataDefinition.FormulaFields
            With rptformula
                .Item("ccPishFaktorTitr").Text = "{mydata.ccPishFaktorTitr}"
                .Item("PishFaktorShomareh").Text = "{mydata.PishFaktorShomareh}"
                .Item("PishFaktorTarikh").Text = "{mydata.PishFaktorTarikh}"
                .Item("ccMoshtary").Text = "{mydata.ccMoshtary}"
                .Item("CodeMoshtary").Text = "{mydata.CodeMoshtary}"
                .Item("NameMoshtary").Text = "{mydata.NameMoshtary}"
                .Item("NameTablo").Text = "{mydata.NameTablo}"
                .Item("NoeSenf").Text = "{mydata.NoeSenf}"
                .Item("MantagheShahrdary").Text = "{mydata.MantagheShahrdary}"
                .Item("TaTarikh").Text = objTarikh.GetDateSlash(mskTaTarikh.Text)
                .Item("AZTarikh").Text = objTarikh.GetDateSlash(mskAzTarikh.Text)
                .Item("Address").Text = "{mydata.Address}"
                .Item("ccAnbar").Text = "{mydata.ccAnbar}"
                .Item("NameAnbar").Text = "{mydata.NameAnbar}"
                .Item("CodeKala").Text = "{mydata.CodeKala}"
                .Item("NameKala").Text = "{mydata.NameKala}"
                .Item("TedadPishFaktorShodeh").Text = "{mydata.TedadPishFaktorShodeh}"
                .Item("NameForoshandeh").Text = "{mydata.NameForoshandeh}"
                .Item("TedadFaktorShodeh").Text = "{mydata.TedadFaktorShodeh}"
                .Item("TedadMarjoeeShodeh").Text = "{mydata.TedadMarjoeeShodeh}"
                .Item("TedadMandeh").Text = "{mydata.TedadMandeh}"
               


            End With
            rpt.Refresh()

            frm.Text = "گزارش دفتر تامین کننده"
            With frm.CRV
                .ReportSource = rpt
                .DisplayGroupTree = False
                .ShowGroupTreeButton = False
                If Not objSec.HaveUserPermission(SN, UD_Dll.Security.ePermissions.Print) Then .ShowPrintButton = False
                If Not objSec.HaveUserPermission(SN, UD_Dll.Security.ePermissions.Export) Then .ShowExportButton = False
            End With

            Me.Hide()
            frm.ShowDialog(Me)
            frm = Nothing
            daSQL = Nothing
            rpt = Nothing
            rpttables = Nothing
            rptformula = Nothing


            Me.Show()

            Windows.Forms.Cursor.Current = Cursors.Default


        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->Print PishFaktor")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->Print PishFaktor")
        End Try
    End Sub
    Private Sub SetParameter()

        Dim commands As String = Microsoft.VisualBasic.Command()
        If commands.Length = 0 Then

            UserName = "Administrator"
            UserPassWord = "66998833"
            NameMahalFaal = "ÊåÑÇä"
            CodeMahalFaal = "1"
            PersonelCode = "0"
            PersonelName = "Administrator"
            CodeDoreh = "1394"

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
    Private Sub btnReport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReport.Click
        If Not objSec.HaveUserPermission(SN, UD_Dll.Security.ePermissions.Preview) Then Exit Sub

        If IsValid() = False Then
            Exit Sub
        End If

        Try
            Me.TopMost = False
            SetReport()
            Me.TopMost = True
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->btnPrintM_Click")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->btnPrintM_Click")
        End Try
    End Sub
    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub
    Private Function IsValid() As Boolean
        IsValid = False
        Try

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
            Else
                ErrPro.SetError(Me.mskAzTarikh, "تاريخ را وارد کنيد.")
                MsgBox("تاريخ را وارد کنيد.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
                mskAzTarikh.Focus()
                Exit Function
            End If
            ErrPro.SetError(Me.mskAzTarikh, "")

            If Len(mskTaTarikh.Text.ToString) <> 0 Then
                If Not objTarikh.IsShDate(mskTaTarikh.Text.ToString) Then
                    mskTaTarikh.Focus()
                    Exit Function
                End If
                If mskTaTarikh.Text.Substring(0, 4) <> CodeDoreh Then
                    MsgBox("تاريخ با دوره مالی فعال يکی نيست.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
                    mskTaTarikh.Focus()
                    Exit Function
                End If
            Else
                ErrPro.SetError(Me.mskTaTarikh, "تاريخ را وارد کنيد.")
                MsgBox("تاريخ را وارد کنيد.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
                mskTaTarikh.Focus()
                Exit Function
            End If
            ErrPro.SetError(Me.mskTaTarikh, "")

            Return True
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> IsValid")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> IsValid")
        End Try
    End Function

    Private Sub LoadCombo()

        Dim Strsql As String
        Dim daSQL As New SqlDataAdapter
        Dim cn As New SqlConnection
        Dim cm As New SqlCommand
        Dim p As New SqlParameter
        Dim d As DataRow

        cn = New SqlConnection(ConnectionString)
        cn.Open()

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

        cm = Nothing : daSQL = Nothing
        cn.Close()
    End Sub
#End Region


    Private Sub txtCodeMoshtaryS_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtCodeMoshtaryS.KeyPress

        Try
            If (Asc(e.KeyChar()) < 48 Or Asc(e.KeyChar()) > 57) And (Asc(e.KeyChar()) <> 8) Then
                e.Handled = True
                'Else
                '    MsgBox("کلید Space را برای انتخاب مشتریان بزنید.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "پیام")
                '    Exit Sub
            End If
            If e.KeyChar = Chr(Keys.Space) Then
                Dim objMoshtary As New Forms_dll.frmFO_MoshtarySearch
                Dim StrSql As String

                If cmbBazaryabS.SelectedValue = 0 Then
                    StrSql = "Select * from qryFO_Moshtary Where CodeMahal=" & CodeMahalFaal & " AND sVazeiat = 3980 "
                    StrSql &= " AND Not exists (Select * From tblGL_SecurityData where namekarbar= '" & UserName & "' and CodeSubSystem = 614 and pk =qryFO_Moshtary.ccMoshtary) "
                    StrSql &= " AND Not exists (Select * From tblGL_SecurityData where namekarbar= '" & UserName & "' and CodeSubSystem = 10000 and pk = qryFO_Moshtary.sNoeMoshtary) "
                    StrSql &= " AND sVazeiat = " & UD_Dll.Enums.FO_VaziatMoshtary.Faal & " order by sMantagheh,sMahaleh,NameMoshtary"
                Else
                    StrSql = "Select * from qryFO_Moshtary Where CodeMahal=" & CodeMahalFaal & " AND ccForoshandeh = " & IIf(IsNothing(cmbBazaryabS.SelectedValue), 0, cmbBazaryabS.SelectedValue)
                    StrSql &= " AND Not exists (Select * From tblGL_SecurityData where namekarbar= '" & UserName & "' and CodeSubSystem = 614 and pk = qryFO_Moshtary.ccMoshtary) "
                    StrSql &= " AND Not exists (Select * From tblGL_SecurityData where namekarbar= '" & UserName & "' and CodeSubSystem = 10000 and pk = qryFO_Moshtary.sNoeMoshtary) "
                    StrSql &= " AND sVazeiat = " & UD_Dll.Enums.FO_VaziatMoshtary.Faal
                    StrSql &= " order by sMantagheh,sMahaleh,NameMoshtary"
                End If

                tCodeMoshtary = ""
                tNameMoshtary = ""
                tccMoshtary = ""
                SearchItem = "CodeMoshtary"
                If txtCodeMoshtaryS.Text.Length <> 0 Then
                    tCodeMoshtary = txtCodeMoshtaryS.Text
                End If
                MultiSelection = True
                objMoshtary.SetForm(StrSql)
                objMoshtary.ShowDialog()
                txtCodeMoshtaryS.Text = objMoshtary.tCodeMoshtary
                txtCodeMoshtaryS.Tag = objMoshtary.tccMoshtary
                MultiSelection = False
            End If
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->txtCodeMoshtaryS_KeyPress")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->txtCodeMoshtaryS_KeyPress")
        End Try

    End Sub
End Class
