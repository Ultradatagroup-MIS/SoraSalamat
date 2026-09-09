Imports System.Data.SqlClient
Imports System.Data
Imports System.IO.Directory
Imports Microsoft.Office.Interop.Excel 
Imports Microsoft.Office.Interop
Imports CrystalDecisions.CrystalReports.Engine
Public Class frmRptMain
    Dim dsForm As New DataSet
    Public objTools As New UD_Dll.mdlUtility
    Public ConnectionString As String = objTools.GetConnectionString()
    Public objTarikh As New UD_Dll.Tarikh
    Public objSec As New UD_Dll.Security
    Public UserName As String
    Public CodeMahalFaal As Long
    Dim flg As Boolean
    Public Smantagheh As String = "-1"
    Public UserPassWord As String
    Public NameMahalFaal As String = ""
    Public PersonelCode As Long
    Public PersonelName As String
    Public CodeDoreh As Long = 0
    Public UserCode As Long
    Public CodeSherkat As Long = 1
    Dim txtCaption As String
    Private SN As Integer

    Public rptPath As String = objTools.ConvertNulls(DLookup("ReportPath", "tblGL_Sherkat", "CodeSherkat = " & CodeSherkat), "")

    Private Sub frmRptMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetParameter()
        LoadCombo()
        mskAzTarikh1.Focus()

    End Sub
    Function DLookup(ByVal fldName As String, ByVal tblName As String, ByVal criteria As String) As Object
        Dim db As SqlConnection
        Dim StrSql As String
        Dim cmSQL As SqlCommand
        Dim ConStr As String

        DLookup = Convert.DBNull
        ConStr = ConnectionString
        db = New SqlConnection(ConStr)
        db.Open()
        Try
            If Len(criteria) = 0 Then
                StrSql = "Select " & fldName & " From " & tblName
            Else
                StrSql = "Select " & fldName & " From " & tblName & " Where " & criteria
            End If
            cmSQL = New SqlCommand(StrSql, db)
            DLookup = cmSQL.ExecuteScalar

        Catch ex As SqlException
            MsgBox(ex.Message, MsgBoxStyle.Information, "Message")
        Finally
            db.Close()
            cmSQL = Nothing
            db = Nothing
        End Try

    End Function
    Private Sub SetParameter()

        Dim commands As String = Microsoft.VisualBasic.Command()
        If commands.Length = 0 Then

            UserName = "Administrator"
            UserPassWord = "66998833"
            NameMahalFaal = "ÊåÑÇä"
            CodeMahalFaal = "1"
            PersonelCode = "0"
            PersonelName = "Administrator"
            CodeDoreh = "1395"

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
    Private Sub LoadCombo()

        Dim strSQL As String
        Dim daSQL As SqlDataAdapter
        Dim d As DataRow
        strSQL = "Select * From qryFO_Foroshandeh where ccForoshandeh <> 0 And CodeMahal= " & CodeMahalFaal & "AND sVazeiat <> " & UD_Dll.Enums.FO_VaziatForoshandeh.NoFaal & " AND"
        strSQL &= " Not exists (Select * From tblGL_SecurityData where namekarbar= '" & UserName & "' and CodeSubSystem = 628 and pk = qryFO_Foroshandeh.ccForoshandeh) order by NameForoshandeh"
        daSQL = New SqlDataAdapter(strSQL, ConnectionString)

        If dsForm.Tables.Contains("tblForoshandehS") Then
            dsForm.Tables.Remove("tblForoshandehS")
        End If
        'daSQL.Fill(dsForm, "tblForoshandehS")
        'cmbForoshandeh.DataSource = Nothing
        'cmbForoshandeh.Items.Clear()
        'cmbForoshandeh.DataSource = dsForm.Tables("tblForoshandehS").DefaultView
        'cmbForoshandeh.DisplayMember = "NameForoshandeh"
        'cmbForoshandeh.ValueMember = "ccForoshandeh"

        daSQL.Fill(dsForm, "tblForoshandehS")
        d = dsForm.Tables("tblForoshandehS").NewRow
        d("NameForoshandeh") = "همه"
        d("ccForoshandeh") = 0
        dsForm.Tables("tblForoshandehS").Rows.Add(d)
        cmbForoshandeh.DataSource = Nothing
        cmbForoshandeh.Items.Clear()
        cmbForoshandeh.DataSource = dsForm.Tables("tblForoshandehS").DefaultView
        cmbForoshandeh.DisplayMember = "NameForoshandeh"
        cmbForoshandeh.ValueMember = "ccForoshandeh"
        cmbForoshandeh.SelectedValue = 0



        strSQL = " Select * From tblGL_ShenasehOmomi Where CodeAsli = 21 And CodeFarei<>0 "
        strSQL &= "AND code in (select sMantagheh from tblFO_moshtary) Order By Code "

        daSQL = New SqlDataAdapter(strSQL, ConnectionString)

        If dsForm.Tables.Contains("chkListMantagheh") Then
            dsForm.Tables.Remove("chkListMantagheh")
        End If

        daSQL.Fill(dsForm, "chkListMantagheh")
        flg = False
        chkListMantagheh.DataSource = Nothing
        chkListMantagheh.Items.Clear()
        chkListMantagheh.DataSource = dsForm.Tables("chkListMantagheh").DefaultView
        chkListMantagheh.DisplayMember = "Sharh"
        chkListMantagheh.ValueMember = "Code"
        chkListMantagheh.SelectedValue = 0
        flg = True



    End Sub



    Private Sub btnReport_Click(sender As Object, e As EventArgs) Handles btnReport.Click
        Dim cnSQL As SqlConnection
        Dim strSQL As String
        Dim daSQL As SqlDataAdapter
        Smantagheh = "-1"
        Dim rpt As New ReportDocument
        Dim rpttables As Tables
        Dim rptformula As FormulaFieldDefinitions
        Dim frm As New Forms_dll.frmGL_Gozaresh
        Dim rptName As String = ""


        For i As Integer = 0 To chkListMantagheh.Items.Count - 1
            If chkListMantagheh.GetItemChecked(i) Then
                chkListMantagheh.SelectedIndex = i
                Smantagheh += "," + chkListMantagheh.SelectedValue.ToString()
            End If
        Next
        cnSQL = New SqlConnection(ConnectionString)
        cnSQL.Open()

        strSQL = " select	"
        If cmbForoshandeh.SelectedValue <> 0 Then
            strSQL &= " x.Codemoshtary, x.NameMoshtary, "
        Else
            strSQL &= " y.Sharh as txtMantagheh, "
        End If
        strSQL &= "	(select Count(ccfaktortitr) from	 tblfo_faktor a left outer join "
        strSQL &= "	 tblFO_PishFaktor b on a.ccpishfaktor = b.ccpishfaktortitr "
        If cmbForoshandeh.SelectedValue = 0 Then
            strSQL &= "left outer join tblfo_moshtary c on a.ccmoshtary = c.ccmoshtary "
        End If
        strSQL &= " Where "
        If cmbForoshandeh.SelectedValue <> 0 Then
            strSQL &= " a.ccmoshtary = x.ccmoshtary and  b.ccforoshandeh =  '" & cmbForoshandeh.SelectedValue & "' And "
        Else
            strSQL &= " c.Smantagheh = x.smantagheh AND "

        End If
        strSQL &= "	a.faktortarikh Between '" & mskAzTarikh1.Text & "' and '" & mskTaTarikh1.Text & "' ) as tedadfaktor1, "
        strSQL &= "	(select isnull(sum(JamKol),0) from tblfo_faktor a left outer join  "
        strSQL &= "	tblFO_PishFaktor b on a.ccpishfaktor = b.ccpishfaktortitr "
        If cmbForoshandeh.SelectedValue = 0 Then
            strSQL &= " left outer join tblfo_moshtary c on a.ccmoshtary = c.ccmoshtary "
        End If
        strSQL &= " Where "
        If cmbForoshandeh.SelectedValue <> 0 Then
            strSQL &= " a.ccmoshtary = x.ccmoshtary and  b.ccforoshandeh =  '" & cmbForoshandeh.SelectedValue & "' And "
        Else
            strSQL &= " c.Smantagheh = x.smantagheh AND "
        End If
        strSQL &= "	a.faktortarikh Between '" & mskAzTarikh1.Text & "' and '" & mskTaTarikh1.Text & "'  ) as jamkol1 , "
        strSQL &= "	(select Count(ccfaktortitr) from	 tblfo_faktor a left outer join "
        strSQL &= "	tblFO_PishFaktor b on a.ccpishfaktor = b.ccpishfaktortitr "
        If cmbForoshandeh.SelectedValue = 0 Then
            strSQL &= "left outer join tblfo_moshtary c on a.ccmoshtary = c.ccmoshtary "
        End If
        strSQL &= " Where "
        If cmbForoshandeh.SelectedValue <> 0 Then
            strSQL &= " a.ccmoshtary = x.ccmoshtary and  b.ccforoshandeh =  '" & cmbForoshandeh.SelectedValue & "' And "
        Else
            strSQL &= " c.Smantagheh = x.smantagheh AND "
        End If
        strSQL &= "	 a.faktortarikh Between '" & mskAzTarikh2.Text & "' and '" & mskTaTarikh2.Text & "' ) as tedadfaktor2, "
        strSQL &= "	(select isnull(sum(JamKol),0) from tblfo_faktor a left outer join "
        strSQL &= "	tblFO_PishFaktor b on a.ccpishfaktor = b.ccpishfaktortitr "
        If cmbForoshandeh.SelectedValue = 0 Then
            strSQL &= "left outer join tblfo_moshtary c on a.ccmoshtary = c.ccmoshtary "
        End If
        strSQL &= " Where "
        If cmbForoshandeh.SelectedValue <> 0 Then
             strSQL &= " a.ccmoshtary = x.ccmoshtary and  b.ccforoshandeh =  '" & cmbForoshandeh.SelectedValue & "' And "
        Else
            strSQL &= " c.Smantagheh = x.smantagheh AND "
        End If
        strSQL &= " a.faktortarikh Between '" & mskAzTarikh2.Text & "' and '" & mskTaTarikh2.Text & "' ) as jamkol2 "
        strSQL &= " From tblfo_moshtary x left outer join tblgl_shenasehOmomi y on x.sMantagheh  = y.code   where  x.svazeiat = 3980 and x.Smantagheh in ( " & Smantagheh & ") "
        strSQL &= " group by "
        If cmbForoshandeh.SelectedValue <> 0 Then
            strSQL &= " ccMoshtary,CodeMoshtary,NameMoshtary "
        Else
            strSQL &= " sMantagheh , y.Sharh "
        End If
        strSQL &= " having (( (SELECT ISNULL(SUM(a.JamKol), 0) FROM tblFO_Faktor AS a LEFT OUTER JOIN tblFO_PishFaktor AS b ON a.ccPishFaktor = b.ccPishFaktorTitr "
        If cmbForoshandeh.SelectedValue = 0 Then
            strSQL &= "left outer join tblfo_moshtary c on a.ccmoshtary = c.ccmoshtary "
        End If
        strSQL &= " Where "
        If cmbForoshandeh.SelectedValue <> 0 Then
            strSQL &= " a.ccmoshtary = x.ccmoshtary and  b.ccforoshandeh =  '" & cmbForoshandeh.SelectedValue & "' And "
        Else
            strSQL &= " c.Smantagheh = x.smantagheh AND "
        End If
        strSQL &= " a.faktortarikh Between '" & mskAzTarikh1.Text & "' and '" & mskTaTarikh1.Text & "'  ) + (SELECT     ISNULL(SUM(a.JamKol), 0) FROM          tblFO_Faktor AS a LEFT OUTER JOIN tblFO_PishFaktor AS b ON a.ccPishFaktor = b.ccPishFaktorTitr "
        If cmbForoshandeh.SelectedValue = 0 Then
            strSQL &= "left outer join tblfo_moshtary c on a.ccmoshtary = c.ccmoshtary "
        End If
        strSQL &= " Where "
        If cmbForoshandeh.SelectedValue <> 0 Then
            strSQL &= " a.ccmoshtary = x.ccmoshtary and  b.ccforoshandeh =  '" & cmbForoshandeh.SelectedValue & "' And "
        Else
            strSQL &= " c.Smantagheh = x.smantagheh AND "
        End If
        strSQL &= " a.faktortarikh Between '" & mskAzTarikh2.Text & "' and '" & mskTaTarikh2.Text & "'  ) <> 0 ))  "

        If dsForm.Tables.Contains("tblGozaresh") Then
            dsForm.Tables.Remove("tblGozaresh")
        End If
        daSQL = New SqlDataAdapter(strSQL, cnSQL)
        daSQL.SelectCommand.CommandTimeout = 99999
        daSQL.Fill(dsForm, "tblGozaresh")
        If dsForm.Tables("tblGozaresh").Rows.Count = 0 Then
            MsgBox("هیـــــچ رکوردی برای گزارش پیدا نشد.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "پیام")
            Exit Sub
        End If

        If cmbForoshandeh.SelectedValue <> 0 Then
            rpt.Load(rptPath & "\rptFO_GozareshMoghayeseForosh.rpt")
        Else
            rpt.Load(rptPath & "\rptFO_GozareshMoghayeseForosh_WithoutForoshandeh.rpt")
        End If

        rpttables = rpt.Database.Tables
        rpttables.Item(0).SetDataSource(dsForm.Tables("tblGozaresh"))

        rptformula = rpt.DataDefinition.FormulaFields
        With rptformula
            If cmbForoshandeh.SelectedValue = 0 Then
                .Item("txtMantagheh").Text = "{mydata.txtMantagheh}"
            End If
            .Item("Namemoshtary").Text = "{mydata.Namemoshtary}"
            .Item("CodeMoshtary").Text = "{mydata.Codemoshtary}"
            .Item("aztarikh1").Text = "'" & objTarikh.SetDateSlash(mskAzTarikh1.Text) & "'"
            .Item("aztarikh2").Text = "'" & objTarikh.SetDateSlash(mskAzTarikh2.Text) & "'"
            .Item("tatarikh1").Text = "'" & objTarikh.SetDateSlash(mskTaTarikh1.Text) & "'"
            .Item("tatarikh2").Text = "'" & objTarikh.SetDateSlash(mskTaTarikh2.Text) & "'"
            .Item("jamkol1").Text = "{mydata.jamkol1}"
            .Item("jamkol2").Text = "{mydata.jamkol2}"
            .Item("tedadfaktor1").Text = "{mydata.tedadfaktor1}"
            .Item("tedadfaktor2").Text = "{mydata.tedadfaktor2}"

        End With
        rpt.Refresh()

        frm.Text = txtCaption

        With frm.CRV
            .ReportSource = rpt
            .DisplayGroupTree = False
            .ShowGroupTreeButton = False
        End With


        Me.Hide()
        frm.ShowDialog(Me)
        frm = Nothing
        daSQL = Nothing
        rpt = Nothing
        Me.Show()
        ClearForm()
    End Sub
    Private Sub ClearForm()
        cmbForoshandeh.SelectedIndex = -1
        cmbForoshandeh.SelectedIndex = -1
        mskAzTarikh1.Text = ""
        mskTaTarikh1.Text = ""
        mskAzTarikh2.Text = ""
        mskTaTarikh2.Text = ""
        For i As Integer = 0 To chkListMantagheh.Items.Count - 1
            chkListMantagheh.SetItemChecked(i, False)
        Next
    End Sub

    Private Sub btnExcel_Click(sender As Object, e As EventArgs) Handles btnExcel.Click
        Dim cnSQL As SqlConnection
        Dim strSQL As String
        Dim daSQL As SqlDataAdapter
        Dim datatableMain As New System.Data.DataTable()
        Smantagheh = "-1"

        For i As Integer = 0 To chkListMantagheh.Items.Count - 1
            If chkListMantagheh.GetItemChecked(i) Then
                chkListMantagheh.SelectedIndex = i
                Smantagheh += "," + chkListMantagheh.SelectedValue.ToString()
            End If
        Next
        cnSQL = New SqlConnection(ConnectionString)
        cnSQL.Open()

        strSQL = " select	"
        If cmbForoshandeh.SelectedValue <> 0 Then
            strSQL &= " x.Codemoshtary, x.NameMoshtary, "
        Else
            strSQL &= " y.Sharh as txtMantagheh, "
        End If
        strSQL &= "	(select Count(ccfaktortitr) from	 tblfo_faktor a left outer join "
        strSQL &= "	 tblFO_PishFaktor b on a.ccpishfaktor = b.ccpishfaktortitr "
        If cmbForoshandeh.SelectedValue = 0 Then
            strSQL &= "left outer join tblfo_moshtary c on a.ccmoshtary = c.ccmoshtary "
        End If
        strSQL &= " Where "
        If cmbForoshandeh.SelectedValue <> 0 Then
            strSQL &= " a.ccmoshtary = x.ccmoshtary and  b.ccforoshandeh =  '" & cmbForoshandeh.SelectedValue & "' And "
        Else
            strSQL &= " c.Smantagheh in ( " & Smantagheh & ") AND "

        End If
        strSQL &= "	a.faktortarikh Between '" & mskAzTarikh1.Text & "' and '" & mskTaTarikh1.Text & "' ) as tedadfaktor1, "
        strSQL &= "	(select isnull(sum(JamKol),0) from tblfo_faktor a left outer join  "
        strSQL &= "	tblFO_PishFaktor b on a.ccpishfaktor = b.ccpishfaktortitr "
        If cmbForoshandeh.SelectedValue = 0 Then
            strSQL &= " left outer join tblfo_moshtary c on a.ccmoshtary = c.ccmoshtary "
        End If
        strSQL &= " Where "
        If cmbForoshandeh.SelectedValue <> 0 Then
            strSQL &= " a.ccmoshtary = x.ccmoshtary and  b.ccforoshandeh =  '" & cmbForoshandeh.SelectedValue & "' And "
        Else
            strSQL &= " c.Smantagheh in ( " & Smantagheh & ") AND "
        End If
        strSQL &= "	a.faktortarikh Between '" & mskAzTarikh1.Text & "' and '" & mskTaTarikh1.Text & "'  ) as jamkol1 , "
        strSQL &= "	(select Count(ccfaktortitr) from	 tblfo_faktor a left outer join "
        strSQL &= "	tblFO_PishFaktor b on a.ccpishfaktor = b.ccpishfaktortitr "
        If cmbForoshandeh.SelectedValue = 0 Then
            strSQL &= "left outer join tblfo_moshtary c on a.ccmoshtary = c.ccmoshtary "
        End If
        strSQL &= " Where "
        If cmbForoshandeh.SelectedValue <> 0 Then
            strSQL &= " a.ccmoshtary = x.ccmoshtary and  b.ccforoshandeh =  '" & cmbForoshandeh.SelectedValue & "' And "
        Else
            strSQL &= " c.Smantagheh in ( " & Smantagheh & ") AND "
        End If
        strSQL &= "	 a.faktortarikh Between '" & mskAzTarikh2.Text & "' and '" & mskTaTarikh2.Text & "' ) as tedadfaktor2, "
        strSQL &= "	(select isnull(sum(JamKol),0) from tblfo_faktor a left outer join "
        strSQL &= "	tblFO_PishFaktor b on a.ccpishfaktor = b.ccpishfaktortitr "
        If cmbForoshandeh.SelectedValue = 0 Then
            strSQL &= "left outer join tblfo_moshtary c on a.ccmoshtary = c.ccmoshtary "
        End If
        strSQL &= " Where "
        If cmbForoshandeh.SelectedValue <> 0 Then
            strSQL &= " a.ccmoshtary = x.ccmoshtary and  b.ccforoshandeh =  '" & cmbForoshandeh.SelectedValue & "' And "
        Else
            strSQL &= " c.Smantagheh in ( " & Smantagheh & ") AND "
        End If
        strSQL &= " a.faktortarikh Between '" & mskAzTarikh2.Text & "' and '" & mskTaTarikh2.Text & "' ) as jamkol2 "
        strSQL &= " From tblfo_moshtary x left outer join tblgl_shenasehOmomi y on x.sMantagheh  = y.code   where  x.svazeiat = 3980 and x.Smantagheh in ( " & Smantagheh & ") "
        strSQL &= " group by "
        If cmbForoshandeh.SelectedValue <> 0 Then
            strSQL &= " ccMoshtary,CodeMoshtary,NameMoshtary "
        Else
            strSQL &= " sMantagheh , y.Sharh "
        End If
        strSQL &= " having (( (SELECT ISNULL(SUM(a.JamKol), 0) FROM tblFO_Faktor AS a LEFT OUTER JOIN tblFO_PishFaktor AS b ON a.ccPishFaktor = b.ccPishFaktorTitr "
        If cmbForoshandeh.SelectedValue = 0 Then
            strSQL &= "left outer join tblfo_moshtary c on a.ccmoshtary = c.ccmoshtary "
        End If
        strSQL &= " Where "
        If cmbForoshandeh.SelectedValue <> 0 Then
            strSQL &= " a.ccmoshtary = x.ccmoshtary and  b.ccforoshandeh =  '" & cmbForoshandeh.SelectedValue & "' And "
        Else
            strSQL &= " c.Smantagheh in ( " & Smantagheh & ") AND "
        End If
        strSQL &= " a.faktortarikh Between '" & mskAzTarikh1.Text & "' and '" & mskTaTarikh1.Text & "'  ) + (SELECT     ISNULL(SUM(a.JamKol), 0) FROM          tblFO_Faktor AS a LEFT OUTER JOIN tblFO_PishFaktor AS b ON a.ccPishFaktor = b.ccPishFaktorTitr "
        If cmbForoshandeh.SelectedValue = 0 Then
            strSQL &= "left outer join tblfo_moshtary c on a.ccmoshtary = c.ccmoshtary "
        End If
        strSQL &= " Where "
        If cmbForoshandeh.SelectedValue <> 0 Then
            strSQL &= " a.ccmoshtary = x.ccmoshtary and  b.ccforoshandeh =  '" & cmbForoshandeh.SelectedValue & "' And "
        Else
            strSQL &= " c.Smantagheh in ( " & Smantagheh & ") AND "
        End If
        strSQL &= " a.faktortarikh Between '" & mskAzTarikh2.Text & "' and '" & mskTaTarikh2.Text & "'  ) <> 0 ))  "

        If dsForm.Tables.Contains("tblGozaresh") Then
            dsForm.Tables.Remove("tblGozaresh")
        End If
        daSQL = New SqlDataAdapter(strSQL, cnSQL)
        daSQL.SelectCommand.CommandTimeout = 99999
        daSQL.Fill(dsForm, "tblGozaresh")

        Dim f As FolderBrowserDialog = New FolderBrowserDialog
        Try
            If f.ShowDialog() = DialogResult.OK Then
                'This section help you if your language is not English.
                System.Threading.Thread.CurrentThread.CurrentCulture = _
                System.Globalization.CultureInfo.CreateSpecificCulture("en-US")
                Dim oExcel As Excel.Application
                Dim oBook As Excel.Workbook
                Dim oSheet As Excel.Worksheet
                oExcel = CreateObject("Excel.Application")
                oBook = oExcel.Workbooks.Add(Type.Missing)
                oSheet = oBook.Worksheets(1)

                Dim dc As System.Data.DataColumn
                Dim dr As System.Data.DataRow
                Dim colIndex As Integer = 0
                Dim rowIndex As Integer = 0


                ' daSQL.Fill(datatableMain)
                daSQL.Fill(datatableMain)
                cnSQL.Close()


                'Export the Columns to excel file
                For Each dc In datatableMain.Columns
                    colIndex = colIndex + 1
                    oSheet.Cells(1, colIndex) = dc.ColumnName
                Next

                'Export the rows to excel file
                For Each dr In datatableMain.Rows
                    rowIndex = rowIndex + 1
                    colIndex = 0
                    For Each dc In datatableMain.Columns
                        colIndex = colIndex + 1
                        oSheet.Cells(rowIndex + 1, colIndex) = dr(dc.ColumnName)
                    Next
                Next

                'Set final path
                Dim fileName As String = "\ExportToExcel" + ".xls"
                Dim finalPath = f.SelectedPath + fileName
                ' txtPath.Text = finalPath
                oSheet.Columns.AutoFit()
                'Save file in final path
                oBook.SaveAs(finalPath, XlFileFormat.xlWorkbookNormal, Type.Missing, _
                Type.Missing, Type.Missing, Type.Missing, XlSaveAsAccessMode.xlExclusive, _
                Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing)

                'Release the objects
                ReleaseObject(oSheet)
                oBook.Close(False, Type.Missing, Type.Missing)
                ReleaseObject(oBook)
                oExcel.Quit()
                ReleaseObject(oExcel)
                'Some time Office application does not quit after automation: 
                'so i am calling GC.Collect method.
                GC.Collect()

                MessageBox.Show("Export done successfully!")


            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Warning", MessageBoxButtons.OK)
        End Try
    End Sub

    Private Sub ReleaseObject(ByVal o As Object)
        Try
            While (System.Runtime.InteropServices.Marshal.ReleaseComObject(o) > 0)
            End While
        Catch
        Finally
            o = Nothing
        End Try
    End Sub

    Private Sub btnSelect_Click(sender As Object, e As EventArgs) Handles btnSelect.Click
        Dim x As Integer = 0
        For i As Integer = 0 To chkListMantagheh.Items.Count - 1
            chkListMantagheh.SetSelected(i, True)
            If chkListMantagheh.GetItemChecked(i) = True Then
                x = x + 1
            End If
        Next

        If x = 0 Then
            For i As Integer = 0 To chkListMantagheh.Items.Count - 1
                chkListMantagheh.SetItemChecked(i, True)
            Next
        Else
            For i As Integer = 0 To chkListMantagheh.Items.Count - 1
                chkListMantagheh.SetItemChecked(i, False)
            Next
        End If
    End Sub
End Class
