Imports System.Data
Imports System.Data.SqlClient
Public Class Fo_rpt_TarazRozaneMarkaz
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

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        objCode.UserName = UserName
        If UserName.ToLower <> "administrator" Then
            If Not objCode.CheckPermission(100066) Then Exit Sub
        End If

        PrintGozaresh(False)

    End Sub

    Private Sub PrintGozaresh(ByVal WithCriteria As Boolean)
        Try
            Dim cnSQL As SqlConnection
            Dim strSQL As String


            Windows.Forms.Cursor.Current = Cursors.WaitCursor
            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()
            Dim temp As String
            temp = mskTarikh.Text
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

    Private Sub Fo_rpt_TarazRozaneMarkaz_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        objTools.ChangeKbd(UD_Dll.mdlUtility.KBD.Farsi)
        SetParameter()
        mskTarikh.Focus()
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
            CodeDoreh = "1391"
            txtCaption = "تخفیف روی فاکتور"
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

    Private Sub btnExit_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub
End Class
