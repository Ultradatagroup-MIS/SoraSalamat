Imports System.Data
Imports System.Data.SqlClient
Public Class DP_rpt_Enteghalat

#Region "Variable AND Constant Declration"
    Private objCode As New UD_Dll.Code
    Dim dvForm As New DataView
    Dim dsForm As New DataSet
    Dim txtCaption As String
    Dim mem As Integer = 0
    Dim ErrPro As New ErrorProvider
    Private SN As Integer
    Dim level As Integer = 0
    Dim Mode As UD_Dll.Enums.GL_ModeForms = UD_Dll.Enums.GL_ModeForms.AddNewRecord
    Const cntCodeSubSystem As Long = 100064
    Dim NoeHesab As Integer
    Public Enum Hesab As Integer
        Sandogh = 1
        Bank = 2
    End Enum
#End Region
#Region " Form Event Code "
    Private Sub DP_rpt_Enteghalat_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        SetParameter()

        SN = objSec.GetSecurityNumber(cntCodeSubSystem, UserName)
        Try
            objTools.ChangeKbd(UD_Dll.mdlUtility.KBD.Farsi)

            'Set Default Tarikh
            If (TarikhEmrooz.Substring(5, 2) >= "01") And (TarikhEmrooz.Substring(5, 2) <= "06") Then
                Dim dt As DateTime = Now.AddDays(-31)
                mskAzTarikh.Text = objTarikh.Mi2Sh(dt)
                mskTaTarikh.Text = TarikhEmrooz
            Else
                Dim dt As DateTime = Now.AddDays(-30)
                mskAzTarikh.Text = objTarikh.Mi2Sh(dt)
                mskTaTarikh.Text = TarikhEmrooz
            End If

            ClearForm()

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->frmTakhsisha_Load")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->frmTakhsisha_Load")
        End Try

    End Sub
    Private Sub rbBank_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbBank.CheckedChanged

        lblHesab.Text = "بانک:"
        NoeHesab = 2
        LoadCombo()

    End Sub
    Private Sub rbSandogh_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbSandogh.CheckedChanged

        lblHesab.Text = "صندوق:"
        NoeHesab = 1
        LoadCombo()

    End Sub
#End Region
#Region "Global Form Code"
    Private Sub ClearForm()

        rbSandogh.Checked = True
        lblHesab.Text = "صندوق:"
        NoeHesab = 1
        LoadCombo()

    End Sub
    Private Sub LoadCombo()

        Dim Strsql As String
        Dim daSQL As SqlDataAdapter

        If NoeHesab = Hesab.Sandogh Then
            Strsql = "Select * From qryDP_ShomarehHesab Where CodeMahal =  " & CodeMahalFaal
            Strsql &= " AND Faal = 1 And Sandogh = 1 order by ShomarehHesabKamel"
            daSQL = New SqlDataAdapter(Strsql, ConnectionString)
            If dsForm.Tables.Contains("tblHesab") Then
                dsForm.Tables.Remove("tblHesab")
            End If
            daSQL.SelectCommand.CommandTimeout = 99999
            daSQL.Fill(dsForm, "tblHesab")
            cmbHesab.DataSource = Nothing
            cmbHesab.Items.Clear()
            cmbHesab.DataSource = dsForm.Tables("tblHesab").DefaultView
            cmbHesab.DisplayMember = "ShomarehHesabKamel"
            cmbHesab.ValueMember = "ccShomarehHesab"

        ElseIf NoeHesab = Hesab.Bank Then
            Strsql = "Select * From qryDP_ShomarehHesab Where CodeMahal =  " & CodeMahalFaal
            Strsql &= " AND Faal = 1 And Sandogh = 0 order by ShomarehHesabKamel"
            daSQL = New SqlDataAdapter(Strsql, ConnectionString)
            If dsForm.Tables.Contains("tblHesab") Then
                dsForm.Tables.Remove("tblHesab")
            End If
            daSQL.SelectCommand.CommandTimeout = 99999
            daSQL.Fill(dsForm, "tblHesab")
            cmbHesab.DataSource = Nothing
            cmbHesab.Items.Clear()
            cmbHesab.DataSource = dsForm.Tables("tblHesab").DefaultView
            cmbHesab.DisplayMember = "ShomarehHesabKamel"
            cmbHesab.ValueMember = "ccShomarehHesab"
        End If

    End Sub
    Private Function IsValidField(ByVal chkField As String) As Boolean

        Try
            IsValidField = False

            If chkField = "mskAzTarikh" Or chkField = "All" Then
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
                    If mskAzTarikh.Text > TarikhEmrooz Then
                        MsgBox("تاریخ وارد شده از تاریخ امروز جلوتر است.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
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
                    If mskTaTarikh.Text.Substring(0, 4) <> CodeDoreh Then
                        MsgBox("تاريخ با دوره مالی فعال يکی نيست.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
                        mskTaTarikh.Focus()
                        Exit Function
                    End If
                    If mskTaTarikh.Text > TarikhEmrooz Then
                        MsgBox("تاریخ وارد شده از تاریخ امروز جلوتر است.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
                        mskTaTarikh.Focus()
                        Exit Function
                    End If
                Else
                    ErrPro.SetError(Me.mskTaTarikh, "تا تاریخ را وارد کنید.")
                    MsgBox("تا تاریخ را وارد کنید.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
                    mskTaTarikh.Focus()
                    Exit Function
                End If
                ErrPro.SetError(Me.mskAzTarikh, "")
            End If
            Return True

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->IsValidRow")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->IsValidRow")
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
            CodeDoreh = "1391"
            txtCaption = "انتقالات"
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
    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        objCode.UserName = UserName
        If UserName.ToLower <> "administrator" Then
            If Not objCode.CheckPermission(100066) Then Exit Sub
        End If

        PrintGozaresh(False)

    End Sub
    Private Sub btnExit_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.Close()
    End Sub
    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub
    Private Sub btnDaftarEnteghalat_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDaftarEnteghalat.Click
        IsValidField(True)
        DaftarEnteghalat()
    End Sub
#End Region
    Private Sub PrintGozaresh(ByVal WithCriteria As Boolean)
        Try
            Dim cnSQL As SqlConnection
            Dim strSQL As String


            Windows.Forms.Cursor.Current = Cursors.WaitCursor
            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()
            Dim temp As String
            temp = mskAzTarikh.Text
            temp = Replace(temp, "/", "")


            strSQL = " select * from [dbo].[fn_TarazMarkaz] ('" & temp & "' , '" & temp & "','" & CodeMahalFaal & "' )"


            If dsForm.Tables.Contains("fn_TarazMarkaz") Then
                dsForm.Tables.Remove("fn_TarazMarkaz")
            End If

            Dim daSQL As SqlDataAdapter
            daSQL = New SqlDataAdapter(strSQL, cnSQL)
            daSQL.Fill(dsForm, "fn_TarazMarkaz")


            Dim rpt As New ReportDocument
            Dim rpttables As Tables
            Dim rptformula As FormulaFieldDefinitions
            Dim frm As New Forms_dll.frmGL_Gozaresh

            rpt.Load(rptPath & "\rptFO_GozareshTarazRozaneMarkaz.rpt")

            rpttables = rpt.Database.Tables
            rpttables.Item(0).SetDataSource(dsForm.Tables("fn_TarazMarkaz"))

            rptformula = rpt.DataDefinition.FormulaFields
            With rptformula



                .Item("Sharh").Text = "{mydata.Sharh}"
                .Item("bed").Text = "{mydata.bed}"
                .Item("bes").Text = "{mydata.bes}"



                .Item("Title").Text = "'" & "گزارش تراز روزانه مرکز" & "'"
                .Item("Title2").Text = "'" & NameSherkat & "'"
                .Item("Title3").Text = "'" & NameMahalFaal & "'"
                .Item("KarbarGozaresh").Text = "'" & PersonelName & "'"
                .Item("TarikhGozaresh").Text = "'" & objTarikh.SetDateSlash(TarikhEmrooz) & "'"
                .Item("SaatGozaresh").Text = "'" & Format(TimeOfDay, "HH:mm:ss") & "'"



            End With
            rpt.Refresh()

            frm.Text = "گزارش تراز روزانه مرکز"

            frm.WindowState = FormWindowState.Maximized
            With frm.CRV
                .ReportSource = rpt
                .DisplayGroupTree = False
                .ShowGroupTreeButton = False
                .Zoom(100)
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
    Private Sub DaftarEnteghalat()
        Try
            Me.TopMost = False
            azTarikh = mskAzTarikh.Text
            taTarikh = mskTaTarikh.Text
            Dim dt As DateTime = objTarikh.SetDateSlash(objTarikh.Sh2Mi(mskAzTarikh.Text))
            TarikhRoozGhabl = objTarikh.Mi2Sh(dt.AddDays(-1))


            CodeShomarehHesab = cmbHesab.SelectedValue
            'SharhShomarehHesab = cmbHesab.Text
            Dim frm As New frmLevel1_Enteghalat

            Me.Hide()
            frm.ShowDialog(Me)
            frm = Nothing
            Me.Show()

            ClearForm()

            Me.TopMost = True
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->btnPrintM_Click")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->btnPrintM_Click")
        End Try
    End Sub

End Class
