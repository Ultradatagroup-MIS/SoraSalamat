Imports System.Data.SqlClient

Public Class frmMorkhasi
    Dim ErrPro As New ErrorProvider
    Dim Mode As UD_Dll.Enums.GL_ModeForms = UD_Dll.Enums.GL_ModeForms.AddNewRecord

    Private Sub frmMorkhasi_Load(sender As Object, e As EventArgs) Handles MyBase.Load


        SetParameter()
        LoadCombo()
        ClearForm()
        Search()
        objTools.ChangeKbd(UD_Dll.mdlUtility.KBD.Farsi)
        cmbPersonel.Focus()
    End Sub
    Private Sub Search()
        Dim Strsql As String
        Dim daSQL As SqlClient.SqlDataAdapter
        Dim dsForm As New DataTable


        Strsql = "select FName + ' ' + LName as Name,dbo.SetDateSlash(AzTarikh) as AzTarikh,dbo.SetDateSlash(TaTarikh) as TaTarikh ,* from payroll.Morkhasi a left outer join tblGL_MoshakhasatFardi b ON a.CodeFard=b.CodeFard where codedoreh= " & CodeDoreh
        daSQL = New SqlDataAdapter(Strsql, ConnectionString)
        'If dsForm.Tables.Contains("tblMorkhasi") = True Then
        '    dsForm.Tables.Remove("tblMorkhasi")
        'End If
        daSQL.Fill(dsForm)
        SetGridStyle(dsForm)
    End Sub
    Private Sub SetGridStyle(ByVal dv As DataTable)

        With GridEXTitr
            .DataSource = Nothing
            .DataSource = dv
            .SetDataBinding(dv, "")
            .RetrieveStructure()

        End With

        For i As Integer = 0 To GridEXTitr.CurrentTable.Columns.Count - 1
            GridEXTitr.CurrentTable.Columns.Item(i).Visible = False
        Next

        GridEXTitr.CurrentTable.Columns.Item("Name").Caption = "نام"
        GridEXTitr.CurrentTable.Columns.Item("Name").Visible = True
        GridEXTitr.CurrentTable.Columns.Item("Name").Width = 120
        GridEXTitr.CurrentTable.Columns.Item("Name").Position = 0
        GridEXTitr.CurrentTable.Columns.Item("Name").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center


        GridEXTitr.CurrentTable.Columns.Item("MorkhasiSaat").Caption = "مرخصی ساعتی"
        GridEXTitr.CurrentTable.Columns.Item("MorkhasiSaat").Visible = True
        GridEXTitr.CurrentTable.Columns.Item("MorkhasiSaat").Width = 80
        GridEXTitr.CurrentTable.Columns.Item("MorkhasiSaat").Position = 1
        GridEXTitr.CurrentTable.Columns.Item("MorkhasiSaat").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

        GridEXTitr.CurrentTable.Columns.Item("MorkhasiRooz").Caption = "مرخصی روزانه"
        GridEXTitr.CurrentTable.Columns.Item("MorkhasiRooz").Visible = True
        GridEXTitr.CurrentTable.Columns.Item("MorkhasiRooz").Width = 80
        GridEXTitr.CurrentTable.Columns.Item("MorkhasiRooz").Position = 2
        GridEXTitr.CurrentTable.Columns.Item("MorkhasiRooz").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

        GridEXTitr.CurrentTable.Columns.Item("AzTarikh").Caption = "از تاریخ "
        GridEXTitr.CurrentTable.Columns.Item("AzTarikh").Visible = True
        GridEXTitr.CurrentTable.Columns.Item("AzTarikh").Width = 80
        GridEXTitr.CurrentTable.Columns.Item("AzTarikh").Position = 4
        GridEXTitr.CurrentTable.Columns.Item("AzTarikh").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

        GridEXTitr.CurrentTable.Columns.Item("TaTarikh").Caption = "تا تاریخ "
        GridEXTitr.CurrentTable.Columns.Item("TaTarikh").Visible = True
        GridEXTitr.CurrentTable.Columns.Item("TaTarikh").Width = 80
        GridEXTitr.CurrentTable.Columns.Item("TaTarikh").Position = 4
        GridEXTitr.CurrentTable.Columns.Item("TaTarikh").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

        GridEXTitr.CurrentTable.Columns.Item("Tozihat").Caption = "توضیحات "
        GridEXTitr.CurrentTable.Columns.Item("Tozihat").Visible = True
        GridEXTitr.CurrentTable.Columns.Item("Tozihat").Width = 250
        GridEXTitr.CurrentTable.Columns.Item("Tozihat").Position = 5
        GridEXTitr.CurrentTable.Columns.Item("Tozihat").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center



  


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

        End If
    End Sub
    Private Sub LoadCombo()
        Dim Strsql As String
        Dim daSQL As SqlClient.SqlDataAdapter
        Dim dsForm As New DataSet


        Strsql = "Select * From tblGL_Mah  order by ccMah"
        daSQL = New SqlDataAdapter(Strsql, ConnectionString)
        If dsForm.Tables.Contains("tblMah") = True Then
            dsForm.Tables.Remove("tblMah")
        End If
        daSQL.Fill(dsForm, "tblMah")
        cmbMah.DataSource = Nothing
        cmbMah.Items.Clear()
        cmbMah.DataSource = dsForm.Tables("tblMah").DefaultView
        cmbMah.DisplayMember = "txtMah"
        cmbMah.ValueMember = "ccMah"


        Strsql = "Select * From qryGL_MoshakhasatFardi Where   sVazeiatEstekhdam = 4188  "

        daSQL = New SqlDataAdapter(Strsql, ConnectionString)
        If dsForm.Tables.Contains("tblPersonel") = True Then
            dsForm.Tables.Remove("tblPersonel")
        End If
        daSQL.Fill(dsForm, "tblPersonel")
        cmbPersonel.DataSource = Nothing
        cmbPersonel.Items.Clear()
        cmbPersonel.DataSource = dsForm.Tables("tblPersonel").DefaultView
        cmbPersonel.DisplayMember = "FN"
        cmbPersonel.ValueMember = "CodeFard"
    End Sub
    Private Sub ClearForm()
        Mode = UD_Dll.Enums.GL_ModeForms.AddNewRow

        txtRooz.Text = ""
        txtSaat.Text = ""
        cmbMah.Text = ""
        cmbPersonel.SelectedIndex = -1
        mskAzTarikh.Text = ""
        mskTaTarikh.Text = ""
        txtTozihat.Text = ""
        BtnDelete.Enabled = False

    End Sub
    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles lblMah.Click

    End Sub

    Private Sub Label1_Click_1(sender As Object, e As EventArgs) Handles lblAzTarikh.Click

    End Sub

    Private Sub Label1_Click_2(sender As Object, e As EventArgs) Handles lblTaTArikh.Click

    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs)

    End Sub


    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub
    Private Function IsValidForm(ByVal CheckField As String) As Boolean
        IsValidForm = False

        If CheckField = "ccAfrad" Or CheckField = "All" Then
            If Me.cmbPersonel.Text = "" Then
                ErrPro.SetError(Me.cmbPersonel, "پرسنل را انتخاب کنید.")
                MsgBox("پرسنل را انتخاب کنید.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
                cmbPersonel.Focus()
                Exit Function
            End If
            ErrPro.SetError(Me.cmbPersonel, "")
        End If

        'If CheckField = "txtRadif" Or CheckField = "All" Then
        '    If Me.txtRadif.Text = "" Then
        '        ErrPro.SetError(Me.txtRadif, "ردیف را وارد کنید. ")
        '        MsgBox("ردیف را وارد کنید.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
        '        txtRadif.Focus()
        '        Exit Function
        '    End If
        '    ErrPro.SetError(Me.txtRadif, "")
        'End If

        Return True
    End Function
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If Mode = UD_Dll.Enums.GL_ModeForms.AddNewRow Then
            AddNewRecord()
        ElseIf Mode = UD_Dll.Enums.GL_ModeForms.UpdateRecord Then
            UpdateRecord()

        End If


    End Sub
    Private Sub AddNewRecord()
        If Not IsValidForm("All") Then
            Exit Sub
        End If

        Dim cnSQL As SqlClient.SqlConnection
        Dim cmSQL As SqlClient.SqlCommand
        Dim strSQL As String

        Try

            strSQL = "INSERT into Payroll.Morkhasi"
            strSQL = strSQL & "( Codefard, MorkhasiSaat, MorkhasiRooz, Mah,AzTarikh,TaTarikh , Tozihat , CodeDoreh)"

            strSQL = strSQL & " VALUES("
            strSQL = strSQL & cmbPersonel.SelectedValue & ","
            strSQL = strSQL & IIf(txtSaat.Text = "", 0, txtSaat.Text) & ","
            strSQL = strSQL & IIf(txtRooz.Text = "", 0, txtRooz.Text) & ","
            strSQL = strSQL & cmbMah.SelectedValue & ","
            strSQL = strSQL & "'" & Replace(mskAzTarikh.Text, "/", "") & "'" & ","
            strSQL = strSQL & "'" & Replace(mskTaTarikh.Text, "/", "") & "'" & ","
            strSQL = strSQL & IIf(txtTozihat.Text = " ", "", "'" & txtTozihat.Text & "'") & ","
            strSQL = strSQL & "'" & CodeDoreh & "'" & ")"


            cnSQL = New SqlClient.SqlConnection(ConnectionString)
            cnSQL.Open()
            cmSQL = New SqlClient.SqlCommand(strSQL, cnSQL)
            cmSQL.ExecuteNonQuery()

            cnSQL.Close()
            cmSQL = Nothing : cnSQL = Nothing

            '----------- update satr
            Dim ccKarkard As Integer = objTools.ConvertNulls(objTools.DLookup("ccKarkard", "Payroll.KarkardDataEntry", "ccAfrad = " & cmbPersonel.SelectedValue & " And CodeMah=" & cmbMah.SelectedValue & " And Sal= " & CodeDoreh), 0)
            Dim MorkhasiSaat As Integer = objTools.ConvertNulls(objTools.DLookup("MorkhasiSaaty", "Payroll.KarkardDataEntry", "ccKarkard = " & ccKarkard), 0)
            Dim MorkhasiRooz As Integer = objTools.ConvertNulls(objTools.DLookup("MorkhasiEstehghaghy", "Payroll.KarkardDataEntry", "ccKarkard = " & ccKarkard), 0)

            ' if karkard is emty insert to karkard
            If ccKarkard = 0 Then
                strSQL = "INSERT into payroll.KarkardDataEntry"
                strSQL = strSQL & "(ccMarkazPakhsh, ccAfrad, Sal, CodeMah, RoozKarkard, HEzafehKar, MEzafehKar,HEzafehKarTatily, MEzafehKarTatily, RGheibat, HKasr, MKasr, ShabKary, TaakhirVorod, GheibatBeinVaght, "
                strSQL = strSQL & "TaajilKhoroj, TaakhirService, MorkhasiSaaty,MorkhasiSaatyMin, MorkhasiEstehghaghy, MorkhasiEstelajy, MorkhasiBedonehHoghogh, MamoriatSaaty, MamoriatRoozaneh, "
                strSQL = strSQL & "MamoriatAmozeshy, MamoriatModirAmel, EsterahatPezeshki, NobatKari, ZaribNobatKari, MandehMorkhasiSaat, "
                strSQL = strSQL & "MandehMorkhasiDaghigheh,RoozNahar,MamoriatTatily)"

                strSQL = strSQL & " VALUES("
                strSQL = strSQL & " 1 ,"
                strSQL = strSQL & cmbPersonel.SelectedValue & ","
                strSQL = strSQL & CodeDoreh & ","
                strSQL = strSQL & cmbMah.SelectedValue & ","
                strSQL = strSQL & "0,"
                strSQL = strSQL & "0,"
                strSQL = strSQL & "0,"
                strSQL = strSQL & "0,"
                strSQL = strSQL & "0,"
                strSQL = strSQL & "0,"
                strSQL = strSQL & "0,"
                strSQL = strSQL & "0,"
                strSQL = strSQL & "0,"
                strSQL = strSQL & " 0,"
                strSQL = strSQL & "0,"
                strSQL = strSQL & "0,"
                strSQL = strSQL & "0,"
                strSQL = strSQL & Val(txtSaat.Text) & ","
                strSQL = strSQL & "0,"
                strSQL = strSQL & Val(txtRooz.Text) & ","
                strSQL = strSQL & "0,"
                strSQL = strSQL & "0,"
                strSQL = strSQL & "0,"
                strSQL = strSQL & "0,"
                strSQL = strSQL & "0,"
                strSQL = strSQL & "0,"
                strSQL = strSQL & "0,"
                strSQL = strSQL & "0,"
                strSQL = strSQL & "0,"
                strSQL = strSQL & "0,"
                strSQL = strSQL & "0,"
                strSQL = strSQL & "0,"
                strSQL = strSQL & "0)"

                cnSQL = New SqlClient.SqlConnection(ConnectionString)
                cnSQL.Open()
                cmSQL = New SqlClient.SqlCommand(strSQL, cnSQL)
                cmSQL.ExecuteNonQuery()

                cnSQL.Close()
                cmSQL = Nothing : cnSQL = Nothing

                ClearForm()
            End If


            '  strSQL = "update Payroll.KarkardDataEntry set MorkhasiSaaty = (select Sum(MorkhasiSaat) from Payroll.Morkhasi   where CodeFard = " & cmbPersonel.SelectedValue ) where cckarkard = " & ccKarkard  )  "
            objTools.DUpdate("MorkhasiSaaty", "Payroll.KarkardDataEntry", MorkhasiSaat + Val(txtSaat.Text), "ccKarkard = " & ccKarkard)
            objTools.DUpdate("MorkhasiEstehghaghy", "Payroll.KarkardDataEntry", MorkhasiRooz + Val(txtRooz.Text), "ccKarkard = " & ccKarkard)

            ClearForm()
            Search()
        Catch sqlExc As SqlClient.SqlException

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
    Private Sub GridEXTitr_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GridEXTitr.DoubleClick
        Mode = UD_Dll.Enums.GL_ModeForms.UpdateRecord

  
        Dim daSQL As SqlClient.SqlDataAdapter
        Dim dsForm As New DataTable
        Dim strSQL As String

        strSQL = "select * from payroll.Morkhasi a left outer join tblGL_MoshakhasatFardi b ON a.CodeFard=b.CodeFard where ccmorkhasi = " & ccMorkhasi

        daSQL = New SqlDataAdapter(strSQL, ConnectionString)
      
        daSQL.Fill(dsForm)

        cmbPersonel.SelectedValue = dsForm.Rows(0)("CodeFard")
        txtSaat.Text = dsForm.Rows(0)("MorkhasiSaat")
        txtRooz.Text = dsForm.Rows(0)("MorkhasiRooz")
        cmbMah.SelectedValue = dsForm.Rows(0)("Mah")
        mskAzTarikh.Text = dsForm.Rows(0)("AzTarikh")
        mskTaTarikh.Text = dsForm.Rows(0)("TaTarikh")
        txtTozihat.Text = objTools.ConvertNulls(dsForm.Rows(0)("Tozihat"), "")
        BtnDelete.Enabled = True




    End Sub
    Private Sub UpdateRecord()
      
        Dim strSQL As String
        Dim cnSQL As SqlClient.SqlConnection
        Dim cmSQL As SqlClient.SqlCommand

        strSQL = "update payroll.Morkhasi set MorkhasiSaat = " & txtSaat.Text & " , MorkhasiRooz = " & txtRooz.Text & ", Mah = " & cmbMah.SelectedValue
        strSQL &= " , AzTarikh = " & mskAzTarikh.Text.Replace("/", "") & " , TaTarikh = " & mskTaTarikh.Text.Replace("/", "") & ",CodeDoreh= " & CodeDoreh & " , Tozihat = " & IIf(txtTozihat.Text = " ", "", "'" & txtTozihat.Text & "'") & " where ccMorkhasi = " & ccMorkhasi

        cnSQL = New SqlClient.SqlConnection(ConnectionString)
        cnSQL.Open()
        cmSQL = New SqlClient.SqlCommand(strSQL, cnSQL)
        cmSQL.ExecuteNonQuery()

        cnSQL.Close()
        cmSQL = Nothing : cnSQL = Nothing

        '------------ UPDATE KARKARD SATR

        Dim ccKarkard As Integer = objTools.ConvertNulls(objTools.DLookup("ccKarkard", "Payroll.KarkardDataEntry", "ccAfrad = " & cmbPersonel.SelectedValue & " And CodeMah=" & cmbMah.SelectedValue & " And Sal= " & CodeDoreh), 0)
        Dim MorkhasiSaat As Integer = objTools.ConvertNulls(objTools.DLookup("MorkhasiSaaty", "Payroll.KarkardDataEntry", "ccKarkard = " & ccKarkard), 0)
        Dim MorkhasiRooz As Integer = objTools.ConvertNulls(objTools.DLookup("MorkhasiEstehghaghy", "Payroll.KarkardDataEntry", "ccKarkard = " & ccKarkard), 0)

        objTools.DUpdate("MorkhasiSaaty", "Payroll.KarkardDataEntry", MorkhasiSaat + Val(txtSaat.Text), "ccKarkard = " & ccKarkard)
        objTools.DUpdate("MorkhasiEstehghaghy", "Payroll.KarkardDataEntry", MorkhasiRooz + Val(txtRooz.Text), "ccKarkard = " & ccKarkard)

        ClearForm()
        Search()


    End Sub
    Private Sub cmbPersonel_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbPersonel.SelectedIndexChanged

    End Sub

    Private Sub GridEXTitr_FormattingRow(sender As Object, e As Janus.Windows.GridEX.RowLoadEventArgs) Handles GridEXTitr.FormattingRow

    End Sub

    Private Sub GridEXTitr_MouseClick(sender As Object, e As MouseEventArgs) Handles GridEXTitr.MouseClick
        If GridEXTitr.SelectedItems.Count = 0 Then
            Exit Sub
        Else
            ccMorkhasi = Val(Me.GridEXTitr.CurrentRow.Cells("ccMorkhasi").Text.Trim)
        End If

    End Sub

    Private Sub cmbMah_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbMah.SelectedIndexChanged

    End Sub

    Private Sub txtCancel_Click(sender As Object, e As EventArgs) Handles txtCancel.Click
        ClearForm()
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub btnPrintSaaty_Click(sender As Object, e As EventArgs) Handles btnPrintSaaty.Click
        printMorkhasiSaaty()

    End Sub

    Private Sub printMorkhasiSaaty()

        Dim daSQL As SqlClient.SqlDataAdapter
        Dim dsForm As New DataTable
        Dim strSQL As String

        strSQL = "select FName + ' ' + LName as Name,dbo.SetDateSlash(AzTarikh) as AzTarikh,dbo.SetDateSlash(TaTarikh) as TaTarikh ,* from payroll.Morkhasi a left outer join tblGL_MoshakhasatFardi b ON a.CodeFard=b.CodeFard where ccmorkhasi = " & ccMorkhasi

        daSQL = New SqlDataAdapter(strSQL, ConnectionString)

        daSQL.Fill(dsForm)

        Dim rpt As New CrystalDecisions.CrystalReports.Engine.ReportDocument
        Dim rpttables As CrystalDecisions.CrystalReports.Engine.Tables
        Dim rptformula As CrystalDecisions.CrystalReports.Engine.FormulaFieldDefinitions
        Dim frm As New Forms_dll.frmGL_Gozaresh

        rpt.Load(rptPath & "\rptHO_BargMorkhasiSaaty.rpt")

        rpttables = rpt.Database.Tables
        rpttables.Item(0).SetDataSource(dsForm)

        rptformula = rpt.DataDefinition.FormulaFields
        With rptformula

            .Item("Name").Text = "{mydata.Name}"
            .Item("CodeFard").Text = "{mydata.CodeFard}"
            .Item("AzTarikh").Text = "{mydata.AzTarikh}"
            .Item("TaTarikh").Text = "{mydata.TaTarikh}"
            .Item("MorkhasiSaat").Text = "{mydata.MorkhasiSaat}"
            .Item("MorkhasiRooz").Text = "{mydata.MorkhasiRooz}"
            .Item("Tozihat").Text = "{mydata.Tozihat}"
            .Item("Group_Sanad").Text = "{mydata.ccMorkhasi}"
            .Item("Shomareh").Text = ""

        End With
        rpt.Refresh()



        With frm.CRV
            .ReportSource = rpt
            .DisplayGroupTree = False
            .ShowGroupTreeButton = False
            .Zoom(75)
          
        End With


        Me.Hide()
        frm.ShowDialog(Me)
        frm = Nothing
        daSQL = Nothing
        rpt = Nothing
        Me.Show()
        Windows.Forms.Cursor.Current = Cursors.Default
    End Sub

    Private Sub btnPrintRooz_Click(sender As Object, e As EventArgs) Handles btnPrintRooz.Click
        PrintMorkhasiRoozaneh()
    End Sub
    Private Sub PrintMorkhasiRoozaneh()

        Dim daSQL As SqlClient.SqlDataAdapter
        Dim dsForm As New DataTable
        Dim strSQL As String

        strSQL = "select FName + ' ' + LName as Name,dbo.SetDateSlash(AzTarikh) as AzTarikh,dbo.SetDateSlash(TaTarikh) as TaTarikh ,* from payroll.Morkhasi a left outer join tblGL_MoshakhasatFardi b ON a.CodeFard=b.CodeFard where ccmorkhasi = " & ccMorkhasi

        daSQL = New SqlDataAdapter(strSQL, ConnectionString)

        daSQL.Fill(dsForm)

        Dim rpt As New CrystalDecisions.CrystalReports.Engine.ReportDocument
        Dim rpttables As CrystalDecisions.CrystalReports.Engine.Tables
        Dim rptformula As CrystalDecisions.CrystalReports.Engine.FormulaFieldDefinitions
        Dim frm As New Forms_dll.frmGL_Gozaresh

        rpt.Load(rptPath & "\rptHO_BargMorkhasiRoozaneh.rpt")

        rpttables = rpt.Database.Tables
        rpttables.Item(0).SetDataSource(dsForm)

        rptformula = rpt.DataDefinition.FormulaFields
        With rptformula

            .Item("Name").Text = "{mydata.Name}"
            .Item("CodeFard").Text = "{mydata.CodeFard}"
            .Item("AzTarikh").Text = "{mydata.AzTarikh}"
            .Item("TaTarikh").Text = "{mydata.TaTarikh}"
            .Item("MorkhasiSaat").Text = "{mydata.MorkhasiSaat}"
            .Item("MorkhasiRooz").Text = "{mydata.MorkhasiRooz}"
            .Item("Tozihat").Text = "{mydata.Tozihat}"
            .Item("Group_Sanad").Text = "{mydata.ccMorkhasi}"
            .Item("Shomareh").Text = ""

        End With
        rpt.Refresh()



        With frm.CRV
            .ReportSource = rpt
            .DisplayGroupTree = False
            .ShowGroupTreeButton = False
            .Zoom(75)

        End With


        Me.Hide()
        frm.ShowDialog(Me)
        frm = Nothing
        daSQL = Nothing
        rpt = Nothing
        Me.Show()
        Windows.Forms.Cursor.Current = Cursors.Default
    End Sub

    Private Sub BtnDelete_Click(sender As Object, e As EventArgs) Handles BtnDelete.Click
        Try
            If MsgBox("آيا رکورد حذف شود؟", MsgBoxStyle.YesNo + MsgBoxStyle.Question + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.DefaultButton2, "حذف رکورد") = MsgBoxResult.Yes Then
                Dim cm As New SqlCommand
                DeleteRow2()

            End If

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->btnDelete_Click")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->btnDelete_Click")
        End Try
    End Sub
    Private Sub DeleteRow2()
        Try

            Mode = UD_Dll.Enums.GL_ModeForms.Delete

            Dim cnSQL As SqlConnection
            Dim cmSQL As SqlCommand
            Dim strSQL As String
            Dim p As New SqlParameter

            strSQL = "Sales.spMorakhasi_Delete"

            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            cmSQL.Parameters.AddWithValue("ccmorkhasi", Val(GridEXTitr.CurrentRow.Cells("ccmorkhasi").Text.Replace(",", "")))

            cmSQL.ExecuteNonQuery()

            cnSQL.Close()
            cmSQL = Nothing : cnSQL = Nothing


            '----------Delete from karkard
            Dim ccKarkard As Integer = objTools.ConvertNulls(objTools.DLookup("ccKarkard", "Payroll.KarkardDataEntry", "ccAfrad = " & cmbPersonel.SelectedValue & " And CodeMah=" & cmbMah.SelectedValue & " And Sal= " & CodeDoreh), 0)
            Dim MorkhasiSaat As Integer = objTools.ConvertNulls(objTools.DLookup("MorkhasiSaaty", "Payroll.KarkardDataEntry", "ccKarkard = " & ccKarkard), 0)
            Dim MorkhasiRooz As Integer = objTools.ConvertNulls(objTools.DLookup("MorkhasiEstehghaghy", "Payroll.KarkardDataEntry", "ccKarkard = " & ccKarkard), 0)

            objTools.DUpdate("MorkhasiSaaty", "Payroll.KarkardDataEntry", MorkhasiSaat - Val(txtSaat.Text), "ccKarkard = " & ccKarkard)
            objTools.DUpdate("MorkhasiEstehghaghy", "Payroll.KarkardDataEntry", MorkhasiRooz - Val(txtRooz.Text), "ccKarkard = " & ccKarkard)

            Mode = UD_Dll.Enums.GL_ModeForms.None


        
            ClearForm()
            Search()
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->DeleteRow")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->DeleteRow")
        End Try

    End Sub
End Class

