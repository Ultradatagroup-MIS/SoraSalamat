Imports System.Runtime.InteropServices
Public Class frmFO_PeigiryFaktor
#Region "Variable AND Constant Declration"
    Const cntCodeSubSystem As Long = 100099
    Const FormTableName = "tblFO_MoshtaryPeigiry"
    Const FormViewName = "tblFO_MoshtaryPeigiry"
    Dim ErrPro As New ErrorProvider
    Dim Mode As UD_Dll.Enums.GL_ModeForms = UD_Dll.Enums.GL_ModeForms.AddNewRecord
    Private SN As Integer
    Dim dsForm As New DataSet
    Dim dvForm As New DataView
    Dim txtCaption As String
    Private WithEvents BS As New UD_Dll.PassString
#End Region
#Region "Form Event Code"
    Private Sub frmFO_PeigiryFaktor_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetParameter()
        SN = objSec.GetSecurityNumber(cntCodeSubSystem, UserName)
        ClearForm()
        objTools.ChangeKbd(UD_Dll.mdlUtility.KBD.Farsi)
        LoadCombo()
        If ccPeigiry <> 0 Then
            Search()
        End If
    End Sub
    Private Sub frmFO_PeigiryFaktor_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles MyBase.Paint
        Me.TopMost = True
    End Sub
    Private Sub frmFO_PeigiryFaktor_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
        ' "Receive" parameter is the caption of destination window
        Dim hwnd As Long = UD_Dll.Code.FindWindow(vbNullString, objCode.GetNameSherkat)
        If hwnd <> 0 Then
            BS.PostString(hwnd, &H400, 0, txtCaption)
        End If

        dsForm = Nothing
        dvForm = Nothing
    End Sub
#End Region
#Region "Global Form Code"
    Private Sub Search()
        Dim strSQL As String
        Dim cnSQL As New SqlConnection
        Dim cmSQL As New SqlCommand
        Dim daSQL As New SqlDataAdapter

        If dsForm.Tables.Contains("tblElatPeigiryFaktor") = True Then
            dsForm.Tables.Remove("tblElatPeigiryFaktor")
        End If

        Try
            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()

            strSQL = "Sales.spMoshtaryAnalysis_ElatPeigiryFaktor_Search "

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            cmSQL.Parameters.AddWithValue("ccPeigiryFaktor", ccPeigiry)

            daSQL = New SqlDataAdapter(cmSQL)
            daSQL.Fill(dsForm, "tblElatPeigiryFaktor")
            dvForm = New DataView
            dvForm = dsForm.Tables("tblElatPeigiryFaktor").DefaultView
            dvForm.Sort = "TarikhPeigiryNext"
            dvForm.AllowDelete = False
            dvForm.AllowEdit = False
            dvForm.AllowNew = False
            cmSQL = Nothing : daSQL = Nothing
            cnSQL.Close()

            SetFormData()
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "Error IN DataBase ---->" & "search")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, "Error IN ---->" & "search")
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
            CodeDoreh = "1392"
            txtCaption = "پیگیری فاکتور"
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
    Private Sub ClearForm()
        Mode = UD_Dll.Enums.GL_ModeForms.AddNewRecord

        mskTarikhPeigiryBady.Text = TarikhEmrooz
        txtSharh.Text = ""
        cmbSharh.SelectedIndex = -1

        ErrPro.Dispose()
        SetButton()
        mskTarikhPeigiryBady.Focus()
    End Sub
    Private Sub LoadCombo()
        Dim strSQL As String
        Dim cnSQL As New SqlConnection
        Dim cmSQL As New SqlCommand
        Dim daSQL As SqlDataAdapter

        If dsForm.Tables.Contains("tblElatPeigiryFaktor") Then
            dsForm.Tables.Remove("tblElatPeigiryFaktor")
        End If

        Try
            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()

            strSQL = "Global.spElatPeigiryFaktor_LoadCombo "

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            daSQL = New SqlDataAdapter(cmSQL)
            daSQL.Fill(dsForm, "tblElatPeigiryFaktor")
            cmbSharh.DataSource = Nothing
            cmbSharh.Items.Clear()
            cmbSharh.DataSource = dsForm.Tables("tblElatPeigiryFaktor").DefaultView
            cmbSharh.ValueMember = "Code"
            cmbSharh.DisplayMember = "Sharh"
         
            cmSQL = Nothing : daSQL = Nothing
            cnSQL.Close()

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "Error IN DataBase ---->" & "LoadCombo")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, "Error IN ---->" & "LoadCombo")
        End Try
    End Sub
    Private Sub SetButton()
        Me.btnUpdate.Enabled = False
        Me.btnExit.Enabled = True
        If Mode = UD_Dll.Enums.GL_ModeForms.AddNewRecord Then
            Me.btnUpdate.Enabled = True
        ElseIf Mode = UD_Dll.Enums.GL_ModeForms.UpdateRecord Then
            Me.btnUpdate.Enabled = True
        End If
    End Sub
    Private Sub AddNewRecord()
        If Not IsValidForm("All") Then
            Exit Sub
        End If

        Dim cnSQL As SqlConnection
        Dim cmSQL As SqlCommand
        Dim strSQL As String
        Try

            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()

            strSQL = "Sales.spMoshtaryAnalysis_ElatPeigiryFaktor_Insert "

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            cmSQL.Parameters.AddWithValue("ccFaktorTitr", drowFaktor("ccFaktorTitr"))
            cmSQL.Parameters.AddWithValue("TarikhPeigiry", TarikhEmrooz)
            cmSQL.Parameters.AddWithValue("SaatPeigiry", Format(TimeOfDay, "HH:mm:ss"))
            cmSQL.Parameters.AddWithValue("TarikhPeigiryNext", mskTarikhPeigiryBady.Text)
            cmSQL.Parameters.AddWithValue("Sharh", txtSharh.Text)
            cmSQL.Parameters.AddWithValue("ccElat", cmbSharh.SelectedValue)
            cmSQL.Parameters.AddWithValue("UserName", UserName)
            cmSQL.Parameters.AddWithValue("Tarikh", TarikhEmrooz)
            cmSQL.Parameters.AddWithValue("Saat", Format(TimeOfDay, "HH:mm:ss"))

            cmSQL.ExecuteNonQuery()

            cmSQL = Nothing
            cnSQL.Close()

            ClearForm()
            Me.Close()

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
    Private Sub UpdateRecord()
        If Not IsValidForm("All") Then
            Exit Sub
        End If
        Dim cnSQL As SqlConnection
        Dim cmSQL As SqlCommand
        Dim strSQL As String
        Try
            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()

            strSQL = "Sales.spMoshtaryAnalysis_ElatPeigiryFaktor_Update "

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            cmSQL.Parameters.AddWithValue("CodeMahal", CodeMahalFaal)
            cmSQL.Parameters.AddWithValue("TarikhPeigiryNext", mskTarikhPeigiryBady.Text)
            cmSQL.Parameters.AddWithValue("ccElat", cmbSharh.SelectedValue)
            cmSQL.Parameters.AddWithValue("Sharh", txtSharh.Text)
            cmSQL.Parameters.AddWithValue("UserName", UserName)
            cmSQL.Parameters.AddWithValue("Tarikh", TarikhEmrooz)
            cmSQL.Parameters.AddWithValue("Saat", Format(TimeOfDay, "HH:mm:ss"))
            cmSQL.Parameters.AddWithValue("ccPeigiryFaktor", ccPeigiry)

            cmSQL.ExecuteNonQuery()

            cmSQL = Nothing
            cnSQL.Close()

            ClearForm()
            Me.Close()

        Catch sqlExc As SqlException
            If (sqlExc.Number = 229) Then
                If Microsoft.VisualBasic.Left(sqlExc.Message, 1) = "U" Then
                    MsgBox("خطا در اصلاح رکورد,اصلاح انجام نشد", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "خطاي بانک")
                End If
            Else
                MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "خطاي بانک")
            End If
        Catch e As Exception
            MsgBox(e.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "خطا")
        End Try
    End Sub
    Private Function IsValidForm(ByVal CheckField As String) As Boolean
        IsValidForm = False

        If CheckField = "mskTarikhPeigiryBady" Or CheckField = "All" Then
            If Me.mskTarikhPeigiryBady.Text = "" Then
                ErrPro.SetError(Me.mskTarikhPeigiryBady, "تاریخ پی گیری بعدی را وارد کنید.")
                MsgBox("تاریخ پی گیری بعدی را وارد کنید.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
                mskTarikhPeigiryBady.Focus()
                Exit Function
            End If
            ErrPro.SetError(Me.mskTarikhPeigiryBady, "")
        End If
        If mskTarikhPeigiryBady.Text.Length <> 0 Then
            If Not objTarikh.IsShDate(mskTarikhPeigiryBady.Text) Then
                ErrPro.SetError(Me.mskTarikhPeigiryBady, "تاریخ پی گیری بعدی را وارد کنید.")
                Exit Function
            End If
        End If

        Return True
    End Function
    Private Sub SetFormData()
        Mode = UD_Dll.Enums.GL_ModeForms.UpdateRecord

        mskTarikhPeigiryBady.Text = dvForm.Item(0)("TarikhPeigiryNext")
        txtSharh.Text = dvForm.Item(0)("Sharh")
        cmbSharh.SelectedValue = dvForm.Item(0)("ccElat")

        SetButton()
    End Sub
#End Region
#Region "From Buttons"
    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub
    Private Sub btnUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdate.Click
        If Mode = UD_Dll.Enums.GL_ModeForms.AddNewRecord Then
            If Not objSec.HaveUserPermission(SN, UD_Dll.Security.ePermissions.Add) Then Exit Sub
            AddNewRecord()
        ElseIf Mode = UD_Dll.Enums.GL_ModeForms.UpdateRecord Then
            If Not objSec.HaveUserPermission(SN, UD_Dll.Security.ePermissions.Update) Then Exit Sub
            UpdateRecord()
        End If
    End Sub
#End Region
End Class