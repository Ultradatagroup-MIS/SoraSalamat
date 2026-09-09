Imports System.Data.SqlClient
Imports System.Data.SqlClient.SqlConnection
Imports System.Data.Common
Imports System.Data
Public Class frmLevel1_Report


#Region "Variable AND Constant Declration"
    Const cntCodeSubSystem As Long = 752
    Const FormTableName = "tblFO_Mashin"
    Const FormViewName = "qryFO_Mashin"

    Dim ErrPro As New ErrorProvider
    Dim Mode As UD_Dll.Enums.GL_ModeForms = UD_Dll.Enums.GL_ModeForms.AddNewRecord
    Dim dsForm As New DataSet
    Dim cmForm As CurrencyManager
    Dim dvForm As DataView
    Private SN As Integer
    Dim txtCaption As String
    Private WithEvents BS As New UD_Dll.PassString
#End Region
#Region "Form Event Code"
    Private Sub frmLevel1_Shahr_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetParameter()
        Search(False)
        SN = objSec.GetSecurityNumber(cntCodeSubSystem, UserName)
        objTools.ChangeKbd(UD_Dll.mdlUtility.KBD.Farsi)
    End Sub
    Private Sub frmLevel1_Report_Closed(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Closed

        dsForm = Nothing
        dvForm = Nothing
    End Sub
    Private Sub frmLevel1_Report_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles MyBase.Paint
        Me.TopMost = True
    End Sub
    Private Sub dbgTitr_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles dbgTitr.DoubleClick
        EditRecord()
    End Sub
    Private Sub frmLevel1_Report_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.F1 Then
            Dim HelpWindow As New Forms_dll.frmGL_HelpWindow
            HelpWindow.CurrentCodeSubSystem = cntCodeSubSystem
            HelpWindow.Show()
            HelpWindow.TopMost = True
        End If
    End Sub
    Private Sub tstxtSearch_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If (Asc(e.KeyChar()) < 47 Or Asc(e.KeyChar()) > 57) And (Asc(e.KeyChar()) <> 8) Then
            e.Handled = True
        End If
    End Sub
    'Private Sub tstxtSearch_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    If tstxtSearch.Text.Length = 0 Then
    '        dvForm.RowFilter = ""
    '        Exit Sub
    '    End If
    '    If IsNothing(dvForm) Then Exit Sub

    '    dvForm.RowFilter = " ShomarehHesab like'%" & tstxtSearch.Text.TrimEnd & "%'"
    'End Sub
#End Region
#Region "Global Form Code"
    Private Sub SetFormData()
        Mode = UD_Dll.Enums.GL_ModeForms.UpdateRecord
        Dim dr As DataRowView
        If cmForm.Count = 0 Then Exit Sub
        dr = dvForm.Item(cmForm.Position)

        If KhazanehForoshHesabdari.cmbNo.SelectedIndex = 1 Then
            Exit Sub
        End If

        If KhazanehForoshHesabdari.cmbNo.SelectedIndex = 0 Then
            sNoeSanad = dr("sNoeSanad")
            sVazeiat = dr("sVazeiat")
        Else
            Noe = dr("Noe")
        End If

        Dim frm As New frmLevel2_Report
        Me.Hide()
        frm.ShowDialog(Me)
        frm = Nothing
        Me.Show()

        dr = Nothing
    End Sub
    Private Sub Search(ByVal WithCriteria As Boolean)
        Dim StrSql As String
        Try
            If KhazanehForoshHesabdari.cmbNo.SelectedIndex = 0 Then
                StrSql = "Report.spGozareshMoghayerat_Khazaneh"
            ElseIf KhazanehForoshHesabdari.cmbNo.SelectedIndex = 1 Then
                StrSql = "Report.spGozareshMoghayerat_Forosh"
            End If

            RefreshFormData(StrSql)
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "Error IN DataBase ---->" & "search")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, "Error IN ---->" & "search")
        End Try
    End Sub
    Private Sub RefreshFormData(ByVal strsql As String)
        Dim cnsql As SqlConnection
        Dim cmsql As SqlCommand
        Dim daSQL As SqlDataAdapter
        Try
            cnsql = New SqlConnection(ConnectionString)
            cnsql.Open()

            cmsql = New SqlCommand(strsql, cnsql)
            cmsql.CommandType = CommandType.StoredProcedure
            cmsql.Parameters.Clear()

            Dim a As String = KhazanehForoshHesabdari.mskAzTarikh.Text
            Dim b As String = KhazanehForoshHesabdari.mskTaTarikh.Text


            cmsql.Parameters.AddWithValue("AzTarikh", a)
            cmsql.Parameters.AddWithValue("TaTarikh", b)
            If KhazanehForoshHesabdari.cmbNo.SelectedIndex = 1 Then
                cmsql.Parameters.AddWithValue("CodeDoreh", CodeDoreh)
            End If


            If dsForm.Tables.Contains(FormTableName) = True Then
                dsForm.Tables.Remove(FormTableName)
            End If
            daSQL = New SqlDataAdapter(cmsql)
            daSQL.Fill(dsForm, FormTableName)
            dvForm = New DataView
            dvForm = dsForm.Tables(FormTableName).DefaultView
            'dvForm.Sort = "Mandeh Desc"
            dvForm.AllowDelete = True
            dvForm.AllowEdit = False
            dvForm.AllowNew = False
            daSQL = Nothing

            If KhazanehForoshHesabdari.cmbNo.SelectedIndex = 1 Then
                Dim dr As DataRow
                dr = dvForm.Table.NewRow
                'If dvForm.Table.Columns.IndexOf("BED") > -1 Then
                '    dr("BED") = dvForm.Table.Compute("Sum(BED)", "")
                'End If

                'If dvForm.Table.Columns.IndexOf("BES") > -1 Then
                '    dr("BES") = dvForm.Table.Compute("Sum(BES)", "")
                'End If

                'dvForm.Table.Rows.Add(dr)

                Dim Mandeh As Long
                For Each dr In dvForm.Table.Rows
                    Mandeh = Mandeh + (dr("BED") - dr("BES"))
                    dr("Mandeh") = Mandeh
                Next
            End If


            SetGridStyle()

            If dvForm.Count = 1 Then
                EditRecord()
            ElseIf dvForm.Count = 0 Then
                'MsgBox("رکوردی پيدا نشد", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "پيام")
            Else
                Mode = UD_Dll.Enums.GL_ModeForms.Search
            End If
        Catch e As SqlException
            MsgBox(e.Message, MsgBoxStyle.Information, "خطا در عمليات بانک")
        Catch e As Exception
            MsgBox(e.Message, MsgBoxStyle.Information, "خطا")
        End Try
    End Sub
    Private Sub SetGridStyle()
        Dim TextCol As DataGridTextBoxColumn
        Dim tableStyle As New DataGridTableStyle
        tableStyle.MappingName = FormTableName
        'dbgTitr.CaptionText = "مغایرت خزانه"
        dbgTitr.BorderStyle = BorderStyle.Fixed3D
        TextCol = New DataGridTextBoxColumn

        tableStyle.GridColumnStyles.Add(TextCol)
        TextCol = New DataGridTextBoxColumn
        With TextCol
            .MappingName = "Sharh"
            .HeaderText = "شرح"
            .Width = 150
            .Alignment = HorizontalAlignment.Left
            .NullText = " "
        End With

        tableStyle.GridColumnStyles.Add(TextCol)
        TextCol = New DataGridTextBoxColumn
        With TextCol
            .MappingName = "Mablagh"
            .HeaderText = "خزانه"
            .Width = 150
            .Format = "###,###.##"
            .Alignment = HorizontalAlignment.Left
            .NullText = " "
        End With
        tableStyle.GridColumnStyles.Add(TextCol)
        TextCol = New DataGridTextBoxColumn
        With TextCol
            .MappingName = "BedH"
            .HeaderText = "حسابداری"
            .Format = "###,###.##"
            .Width = 150
            .Alignment = HorizontalAlignment.Left
            .NullText = " "
        End With

        tableStyle.GridColumnStyles.Add(TextCol)
        TextCol = New DataGridTextBoxColumn
        With TextCol
            .MappingName = "Moghayerat"
            .Format = "###,###.##"
            .HeaderText = "مغایرت"
            .Width = 150
            .Alignment = HorizontalAlignment.Left
            .NullText = " "
        End With
        tableStyle.GridColumnStyles.Add(TextCol)

        TextCol = New DataGridTextBoxColumn
        With TextCol
            .MappingName = "txtNoeTakhfif"
            .Format = "###,###.##"
            .HeaderText = "نوع تخفیف"
            .Width = 200
            .Alignment = HorizontalAlignment.Left
            .NullText = " "
        End With
        tableStyle.GridColumnStyles.Add(TextCol)

        TextCol = New DataGridTextBoxColumn
        With TextCol
            .MappingName = "BED"
            .Format = "###,###.##"
            .HeaderText = "بدهکار"
            .Width = 150
            .Alignment = HorizontalAlignment.Left
        End With
        tableStyle.GridColumnStyles.Add(TextCol)

        TextCol = New DataGridTextBoxColumn
        With TextCol
            .MappingName = "BES"
            .Format = "###,###.##"
            .HeaderText = "بستانکار"
            .Width = 150
            .Alignment = HorizontalAlignment.Left
        End With
        tableStyle.GridColumnStyles.Add(TextCol)

        TextCol = New DataGridTextBoxColumn
        With TextCol
            .MappingName = "Mandeh"
            .Format = "###,###.##"
            .HeaderText = "مانده"
            .Width = 150
            .Alignment = HorizontalAlignment.Left
        End With
        tableStyle.GridColumnStyles.Add(TextCol)


        With dbgTitr
            .TableStyles.Clear()
            .TableStyles.Add(tableStyle)
            .Visible = True
            .DataSource = dvForm
        End With
        BoundCurrencyManager()
        Me.CenterToScreen()
    End Sub
    Private Sub BoundCurrencyManager()
        If dvForm.Count > 1 Then Mode = UD_Dll.Enums.GL_ModeForms.Search
        If dvForm.Count = 1 Then Mode = UD_Dll.Enums.GL_ModeForms.UpdateRecord
        If Mode = UD_Dll.Enums.GL_ModeForms.UpdateRecord Or Mode = UD_Dll.Enums.GL_ModeForms.Search Then
            cmForm = CType(BindingContext(dbgTitr.DataSource), CurrencyManager)
            AddHandler cmForm.ItemChanged, AddressOf cmForm_ItemChanged
            AddHandler cmForm.PositionChanged, AddressOf cmForm_PositionChanged
            'DisplayPosition()
        End If
    End Sub
    Private Sub DisplayPosition()
        '    If Mode = UD_Dll.Enums.GL_ModeForms.UpdateRecord Or Mode = UD_Dll.Enums.GL_ModeForms.Search Then
        '        dbgTitr.CaptionText = txtCaption & "  رکورد " & cmForm.Position + 1 & " از " & cmForm.Count
        '    Else
        '        dbgTitr.CaptionText = txtCaption
        '    End If
        '    dbgTitr.CaptionText = dbgTitr.CaptionText

        '    StatusStrip1.Items.Item(1).Text = tccShomarehHesab
        '    StatusStrip1.Items.Item(3).Text = IIf(tccShomarehHesab.ToString.Length > 3, ObjCode.DigitSeprator(tccShomarehHesab.ToString), tccShomarehHesab.ToString)
        '    StatusStrip1.Items.Item(5).Text = IIf(tccShomarehHesab.ToString.Length > 3, ObjCode.DigitSeprator(tccShomarehHesab.ToString), tccShomarehHesab.ToString)
        '    StatusStrip1.Items.Item(7).Text = IIf(tccShomarehHesab.ToString.Length > 3, ObjCode.DigitSeprator(tccShomarehHesab.ToString), tccShomarehHesab.ToString)
    End Sub
    Private Sub cmForm_ItemChanged(ByVal sender As Object, ByVal e As ItemChangedEventArgs)
        DisplayPosition()
    End Sub
    Private Sub cmForm_PositionChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        DisplayPosition()
    End Sub
    Private Sub EditRecord()
        SetFormData()
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
            txtCaption = "دفتر مشتری در سطح استان"
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
#Region "From Buttons"
    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.Close()
    End Sub
    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Search(True)
    End Sub
    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Search(False)
    End Sub
    Private Sub tsbtnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbtnClose.Click
        Me.Close()
    End Sub
#End Region
    'Private Sub tstxtSearch_TextChanged1(ByVal sender As Object, ByVal e As System.EventArgs)
    '    If tstxtSearch.Text.Length = 0 Then
    '        dvForm.RowFilter = ""
    '        Exit Sub
    '    End If
    '    If IsNothing(dvForm) Then Exit Sub

    '    dvForm.RowFilter = " ShomarehHesab like'%" & tstxtSearch.Text.TrimEnd & "%'"
    'End Sub

    Private Sub tsbtnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbtnPrint.Click
        Try
            Dim rpt As New CrystalDecisions.CrystalReports.Engine.ReportDocument
            Dim rpttables As CrystalDecisions.CrystalReports.Engine.Tables
            Dim rptformula As CrystalDecisions.CrystalReports.Engine.FormulaFieldDefinitions
            Dim frm As New Forms_dll.frmGL_Gozaresh

            If KhazanehForoshHesabdari.cmbNo.SelectedIndex = 0 Then
                rpt.Load(rptPath & "\MoghayeratKhazaneh.rpt")
            ElseIf KhazanehForoshHesabdari.cmbNo.SelectedIndex = 1 Then
                rpt.Load(rptPath & "\MoghayeratTakhfifatForosh.rpt")
            End If


            rpttables = rpt.Database.Tables
            rpttables.Item(0).SetDataSource(dbgTitr.DataSource)

            rptformula = rpt.DataDefinition.FormulaFields
            With rptformula
                If KhazanehForoshHesabdari.cmbNo.SelectedIndex = 0 Then
                    .Item("Group_Sanad").Text = "{mydata.CodeMahal}"
                    .Item("Sharh").Text = "{mydata.Sharh}"
                    .Item("Mablagh").Text = "{mydata.Mablagh}"
                    .Item("BedH").Text = "{mydata.BedH}"
                    .Item("Moghayerat").Text = "{mydata.Moghayerat}"

                    .Item("AzTarikh").Text = "'" & objTarikh.SetDateSlash(KhazanehForoshHesabdari.mskAzTarikh.Text) & "'"
                    .Item("TaTarikh").Text = "'" & objTarikh.SetDateSlash(KhazanehForoshHesabdari.mskTaTarikh.Text) & "'"

                    .Item("Title").Text = "'" & "گزارش خزانه" & "'"
                ElseIf KhazanehForoshHesabdari.cmbNo.SelectedIndex = 1 Then
                    .Item("Group_Sanad").Text = "{mydata.CodeDoreh}"
                    .Item("txtNoeTakhfif").Text = "{mydata.txtNoeTakhfif}"
                    .Item("BED").Text = "{mydata.BED}"
                    .Item("BES").Text = "{mydata.BES}"
                    .Item("Mandeh").Text = "{mydata.Mandeh}"

                    .Item("AzTarikh").Text = "'" & objTarikh.SetDateSlash(KhazanehForoshHesabdari.mskAzTarikh.Text) & "'"
                    .Item("TaTarikh").Text = "'" & objTarikh.SetDateSlash(KhazanehForoshHesabdari.mskTaTarikh.Text) & "'"

                    .Item("Title").Text = "'" & "گزارش تخفیفات فروش" & "'"
                End If

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
                If Not objSec.HaveUserPermission(SN, UD_Dll.Security.ePermissions.Print) Then .ShowPrintButton = False
                If Not objSec.HaveUserPermission(SN, UD_Dll.Security.ePermissions.Export) Then .ShowExportButton = False
            End With


            Me.Hide()
            frm.ShowDialog(Me)
            frm = Nothing
            rpt = Nothing
            Me.Show()
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->Print PishFaktor")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->Print PishFaktor")
        End Try
    End Sub
End Class
