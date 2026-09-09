Imports System.Data.OleDb
Public Class frmAN_InsertSerialNumber

#Region "Variable AND Constant Declration"

    Dim cmTitr As CurrencyManager
    Dim dvTitr As DataView
    Dim tCodeCounter As Long
    Dim ErrPro As New ErrorProvider
    Dim dsForm As New DataSet
    Private SN As Integer
    Dim txtCaption As String
    Private WithEvents BS As New UD_Dll.PassString
    Public ModeForm As UD_Dll.Enums.GL_ModeForms = UD_Dll.Enums.GL_ModeForms.AddNewRecord
    Public Type As Integer = 0
    Public ccKardexTitr As Integer = 0
    Public ccKardexSatr As Integer = 0
    Public ccKala As Integer = 0
    Public CodeKala As String = ""
    Public NameKala As String = ""
    Public TedadKalayeForm As Integer = 0
    Public EtelaatForm As String = ""
    Dim ccSerialNumber As Integer = 0

    Dim ExcelFileName As String = ""

    Dim ttEnter As New ToolTip
    Dim ttDelete As New ToolTip
#End Region
#Region "Form Event Code"
    Private Sub frmAN_InsertSerialNumber_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetParameter()
        SetFormTedadi()
        txtEtelaatKala.Text = " کد کالای : " + CodeKala + " - " + NameKala
        txtEtelaatForm.Text = EtelaatForm
        SearchSerial()
        objTools.ChangeKbd(UD_Dll.mdlUtility.KBD.Latin)
        txtSerialNumber.Focus()
    End Sub
    Private Sub tcInsert_SelectedIndexChanged(sender As Object, e As EventArgs) Handles tcInsert.SelectedIndexChanged
        If tcInsert.SelectedIndex = 0 Then
            txtSerialNumber.Text = ""
            txtSerialNumber.Focus()
        ElseIf tcInsert.SelectedIndex = 1 Then
            ClearExcelMode()
        End If
    End Sub
    Private Sub GridEXSerial_DoubleClick(sender As Object, e As EventArgs) Handles GridEXSerial.DoubleClick
        If GridEXSerial.RowCount = 0 Then Exit Sub

        ccSerialNumber = Val(GridEXSerial.CurrentRow.Cells("ccSerialNumber").Text.Replace(",", ""))
        txtSerialNumber.Text = GridEXSerial.CurrentRow.Cells("SerialNumber").Text.Replace(",", "")
        txtSerialNumber.Focus()
        ModeForm = UD_Dll.Enums.GL_ModeForms.UpdateRecord
    End Sub
    Private Sub txtSerialNumber_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtSerialNumber.KeyPress
        If e.KeyChar = Chr(Keys.Enter) Then

            If IsValid(tcInsert.SelectedIndex, txtSerialNumber.Text.Trim) = False Then
                Exit Sub
            End If

            If ModeForm = UD_Dll.Enums.GL_ModeForms.AddNewRecord Then
                InsertSerial(txtSerialNumber.Text.Trim)
                SetFormTedadi()
            ElseIf ModeForm = UD_Dll.Enums.GL_ModeForms.UpdateRecord Then
                UpdateSerial()
            End If
        Else
            Exit Sub
        End If

        SearchSerial()
        ClearForm()
    End Sub
    Private Sub GridEXSerial_KeyUp(sender As Object, e As KeyEventArgs) Handles GridEXSerial.KeyUp
        If GridEXSerial.RowCount = 0 Then Exit Sub

        If e.KeyCode = Keys.Delete Then
            If MsgBox("آیا مایلید سریال انتخاب شده حذف گردد ؟", MsgBoxStyle.Question + MsgBoxStyle.YesNo + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, "") = MsgBoxResult.Yes Then
                objTools.DDelete("WareHouse.SerialNumber", "ccSerialNumber = " & Val(GridEXSerial.CurrentRow.Cells("ccSerialNumber").Text.Replace(",", "")))
                SearchSerial()
                SetFormTedadi()
                ClearForm()
            End If
        End If
    End Sub
    Private Sub GridEXSerial_MouseEnter(sender As Object, e As EventArgs) Handles GridEXSerial.MouseEnter
        If GridEXSerial.RowCount = 0 Then Exit Sub
        ttEnter.SetToolTip(GridEXSerial, "جهت حذف سریال مورد نظر، از دکمه Delete بر روی کیبورد استفاده نمایید .")
        ttEnter.Show("جهت حذف سریال مورد نظر، از دکمه Delete بر روی کیبورد استفاده نمایید .", GridEXSerial)
    End Sub
    Private Sub GridEXSerial_MouseLeave(sender As Object, e As EventArgs) Handles GridEXSerial.MouseLeave
        If GridEXSerial.RowCount = 0 Then Exit Sub
        ttDelete.Hide(GridEXSerial)
    End Sub
    Private Sub txtSerialNumber_MouseEnter(sender As Object, e As EventArgs) Handles txtSerialNumber.MouseEnter
        ttDelete.SetToolTip(txtSerialNumber, "جهت ثبت، سریال را وارد نموده و دکمه Enter را فشار دهید .")
        ttDelete.Show("جهت ثبت، سریال را وارد نموده و دکمه Enter را فشار دهید .", txtSerialNumber)
    End Sub
    Private Sub txtSerialNumber_MouseLeave(sender As Object, e As EventArgs) Handles txtSerialNumber.MouseLeave
        ttEnter.Hide(txtSerialNumber)
    End Sub
#End Region
#Region "Global Form Code"
    Private Sub SetParameter()
        Dim commands As String = Microsoft.VisualBasic.Command()
        If commands.Length = 0 Then
            UserName = "Administrator"
            UserPassWord = "66998833"
            NameMahalFaal = "تهران"
            CodeMahalFaal = "1"
            PersonelCode = "0"
            PersonelName = "Administrator"
            CodeDoreh = "1394"
            txtCaption = "شماره سریال"
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
    Private Sub SearchSerial()
        Dim cnSQL As New SqlConnection
        Dim cmSQL As New SqlCommand
        Dim daSQL As New SqlDataAdapter
        Dim strSQL As String = ""

        If dsForm.Tables.Contains("tbl_Serial") Then
            dsForm.Tables.Remove("tbl_Serial")
        End If

        Try
            strSQL = "WareHouse.spSerialNumber_AddSerial_SearchSerial "

            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            cmSQL.Parameters.AddWithValue("Type", Type)
            cmSQL.Parameters.AddWithValue("ccKardexTitr", ccKardexTitr)
            cmSQL.Parameters.AddWithValue("ccKardexSatr", ccKardexSatr)
            cmSQL.Parameters.AddWithValue("ccKala", ccKala)

            daSQL = New SqlDataAdapter(cmSQL)
            daSQL.Fill(dsForm, "tbl_Serial")

            dvTitr = New DataView(dsForm.Tables("tbl_Serial"))
            dvTitr.Sort = "SerialNumber ASC"

            dvTitr.AllowNew = False
            dvTitr.AllowDelete = False
            dvTitr.AllowEdit = True

            cmSQL = Nothing : daSQL = Nothing
            cnSQL.Close()

            SetGridSerial()
            With GridEXSerial
                .Visible = True
                .DataSource = Nothing
                .DataSource = dvTitr
            End With

            Me.CenterToScreen()
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "Error IN DataBase ----> SearchSerial")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, "Error IN ----> SearchSerial")
        End Try
    End Sub
    Private Sub SetGridSerial()
        If dvTitr.Count = 0 Then
            Exit Sub
        End If

        Try
            With GridEXSerial
                .DataSource = Nothing
                .DataSource = dsForm.Tables("tbl_Serial").DefaultView
                .SetDataBinding(dsForm.Tables("tbl_Serial").DefaultView, "")
                .RetrieveStructure()
            End With

            For i As Integer = 0 To GridEXSerial.CurrentTable.Columns.Count - 1
                GridEXSerial.CurrentTable.Columns.Item(i).Visible = False
            Next

            GridEXSerial.CurrentTable.Columns.Item("SerialNumber").Caption = "شماره سریـال"
            GridEXSerial.CurrentTable.Columns.Item("SerialNumber").Visible = True
            GridEXSerial.CurrentTable.Columns.Item("SerialNumber").Width = 300
            GridEXSerial.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXSerial.CurrentTable.Columns.Item("SerialNumber").Position = 0
            GridEXSerial.CurrentTable.Columns.Item("SerialNumber").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXSerial.CurrentTable.Columns.Item("SerialNumber").HeaderAlignment = TextAlignment.Center

            GridEXSerial.CurrentTable.Columns.Item("ccSerialNumber").Caption = "ccSerialNumber"
            GridEXSerial.CurrentTable.Columns.Item("ccSerialNumber").Visible = False
            GridEXSerial.CurrentTable.Columns.Item("ccSerialNumber").Width = 0
            GridEXSerial.CurrentTable.Columns.Item("ccSerialNumber").EditType = EditType.NoEdit
            GridEXSerial.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXSerial.CurrentTable.Columns.Item("ccSerialNumber").Position = 1
            GridEXSerial.CurrentTable.Columns.Item("ccSerialNumber").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXSerial.CurrentTable.Columns.Item("ccSerialNumber").HeaderAlignment = TextAlignment.Center

            For i As Integer = 0 To GridEXSerial.RootTable.Columns.Count - 1
                If GridEXSerial.RootTable.Columns(i).Type.IsValueType Then
                    GridEXSerial.RootTable.Columns(i).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                    GridEXSerial.RootTable.Columns(i).FormatMode = Janus.Windows.GridEX.FormatMode.UseIFormattable
                    GridEXSerial.RootTable.Columns(i).FormatString = "G"
                    GridEXSerial.RootTable.Columns(i).TotalFormatMode = Janus.Windows.GridEX.FormatMode.UseIFormattable
                    GridEXSerial.RootTable.Columns(i).TotalFormatString = "G"
                End If
            Next

            Me.CenterToScreen()
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> SetGridSerial ")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> SetGridSerial ")
        End Try
    End Sub
    Private Sub InsertSerial(ByVal SerialNumber As String)
        Dim cnSQL As New SqlConnection
        Dim cmSQL As New SqlCommand
        Dim strSQL As String = ""

        Try
            strSQL = "WareHouse.spSerialNumber_AddSerial_InsertSerial "

            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            cmSQL.Parameters.AddWithValue("Type", Type)
            cmSQL.Parameters.AddWithValue("ccKardexTitr", ccKardexTitr)
            cmSQL.Parameters.AddWithValue("ccKardexSatr", ccKardexSatr)
            cmSQL.Parameters.AddWithValue("ccKala", ccKala)
            cmSQL.Parameters.AddWithValue("SerialNumber", SerialNumber)

            cmSQL.ExecuteNonQuery()

            cmSQL = Nothing
            cnSQL.Close()

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "Error IN DataBase ----> InsertSerial")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, "Error IN ----> InsertSerial")
        End Try
    End Sub
    Private Sub UpdateSerial()
        Dim cnSQL As New SqlConnection
        Dim cmSQL As New SqlCommand
        Dim strSQL As String = ""

        Try
            strSQL = "WareHouse.spSerialNumber_AddSerial_UpdateSerial "

            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            cmSQL.Parameters.AddWithValue("ccSerialNumber", ccSerialNumber)
            cmSQL.Parameters.AddWithValue("SerialNumber", txtSerialNumber.Text.Trim)

            cmSQL.ExecuteNonQuery()

            cmSQL = Nothing
            cnSQL.Close()

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "Error IN DataBase ----> UpdateSerial")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, "Error IN ----> UpdateSerial")
        End Try
    End Sub
    Private Function IsValid(ByVal Mode As Integer, ByVal Serial As String) As Boolean
        '' Mode ----> 0 = Dasti / 1 = Excel
        IsValid = False

        Try
            If Mode = 0 Then
                If objTools.DCount("ccSerialNumber", "WareHouse.SerialNumber", "ccKala = " & ccKala & " AND TypeKardex = " & Type & " AND ccKardexTitr = " & ccKardexTitr & " AND ccKardexSatr = " & ccKardexSatr) = TedadKalayeForm Then
                    MsgBox("به تعداد موجود از این کالا، شماره سریال ثبت شده است ! ", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, "خطا")
                    ClearForm()
                    Exit Function
                End If

                If txtSerialNumber.Text.Trim = "" Then
                    MsgBox("سریال را وارد نمایید !", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, "خطا")
                    ErrPro.SetError(txtSerialNumber, "سریال را وارد نمایید !")
                    Exit Function
                End If
                ErrPro.SetError(txtSerialNumber, "")

                If ModeForm = UD_Dll.Enums.GL_ModeForms.AddNewRecord Then
                    If objTools.DCount("SerialNumber", "WareHouse.SerialNumber", "SerialNumber = '" & Serial & "' AND TypeKardex = " & Type) > 0 Then
                        MsgBox("این شماره سریال قبلا استفاده شده است !", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, "خطا")
                        ErrPro.SetError(txtSerialNumber, "این شماره سریال قبلا استفاده شده است !")
                        Exit Function
                    End If
                    ErrPro.SetError(txtSerialNumber, "")
                End If
            ElseIf Mode = 1 Then
                If objTools.DCount("SerialNumber", "WareHouse.SerialNumber", "SerialNumber = '" & Serial & "' AND TypeKardex = " & Type) > 0 Then
                    Exit Function
                End If
            End If

            Return True
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "Error IN DataBase ----> IsValid")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, "Error IN ----> IsValid")
        End Try
    End Function
    Private Sub SetFormTedadi()
        lblTedadForm.Text = TedadKalayeForm
        lblTedadSerial.Text = objTools.DCount("ccSerialNumber", "WareHouse.SerialNumber", "ccKala = " & ccKala & " AND TypeKardex = " & Type & " AND ccKardexTitr = " & ccKardexTitr & " AND ccKardexSatr = " & ccKardexSatr)
        lblTedadMandeh.Text = TedadKalayeForm - objTools.DCount("ccSerialNumber", "WareHouse.SerialNumber", "ccKala = " & ccKala & " AND TypeKardex = " & Type & " AND ccKardexTitr = " & ccKardexTitr & " AND ccKardexSatr = " & ccKardexSatr)
    End Sub
    Private Sub ClearForm()
        ModeForm = UD_Dll.Enums.GL_ModeForms.AddNewRecord
        ccSerialNumber = 0
        txtSerialNumber.Text = ""
        txtSerialNumber.Focus()
    End Sub
#End Region
#Region "From Buttons "
    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        Me.Close()
    End Sub
    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        ClearForm()
    End Sub
    Private Sub btnRemoveAllSerial_Click(sender As Object, e As EventArgs) Handles btnRemoveAllSerial.Click
        If MsgBox("آیا مایلید تمامی سریال های ثبت شده حذف گردد ؟", MsgBoxStyle.Question + MsgBoxStyle.YesNo + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, "") = MsgBoxResult.Yes Then
            objTools.DDelete("WareHouse.SerialNumber", "TypeKardex = " & Type & " AND ccKardexTitr = " & ccKardexTitr & " AND ccKardexSatr = " & ccKardexSatr & " AND ccKala = " & ccKala)
            SearchSerial()
            ClearForm()
            SetFormTedadi()
        End If
    End Sub
#End Region
#Region "Stored Procedures"

#End Region

    Private Sub btnSelectFileExcel_Click(sender As Object, e As EventArgs) Handles btnSelectFileExcel.Click
        Dim OFD As OpenFileDialog = New OpenFileDialog
        Dim Result As DialogResult

        OFD.Filter = "*.xls|*.xlsx"
        OFD.FilterIndex = 1
        OFD.Multiselect = False

        Result = OFD.ShowDialog()

        If Result = Windows.Forms.DialogResult.OK Then
            ExcelFileName = OFD.FileName
        End If

        txtExcelFileName.Text = ExcelFileName
    End Sub

    Private Sub btnClearFileExcel_Click(sender As Object, e As EventArgs) Handles btnClearFileExcel.Click
        ClearExcelMode()
    End Sub
    Private Sub ClearExcelMode()
        txtExcelFileName.Text = ""
        ExcelFileName = ""
    End Sub

    Private Sub btnConvertFileExcel_Click(sender As Object, e As EventArgs) Handles btnConvertFileExcel.Click
        Try
            If ExcelFileName = "" Then
                MsgBox("جهت کانورت باید ابتدا یک فایل Excel مطابق با توضیحاتی که در قسمت پایین شرح داده شده است، انتخاب نمایید !", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, "خطا")
                Exit Sub
            End If

            Dim MyConnection As System.Data.OleDb.OleDbConnection
            Dim DtSet As System.Data.DataSet
            Dim MyCommand As System.Data.OleDb.OleDbDataAdapter
            Dim dt As New Data.DataTable

            MyConnection = New System.Data.OleDb.OleDbConnection("provider=Microsoft.Jet.OLEDB.4.0;Data Source = '" & ExcelFileName & "';Extended Properties = Excel 8.0;")
            MyCommand = New System.Data.OleDb.OleDbDataAdapter("select * from [SerialNumber$]", MyConnection)
            MyCommand.TableMappings.Add("Table", "SerialNumber")
            DtSet = New System.Data.DataSet
            MyCommand.Fill(DtSet, "SerialNumber")

            dt = DtSet.Tables("SerialNumber")

            MyConnection.Close()

            Dim ConutRecordTekrari As Integer = 0
            Dim strRecordTekrari As String = ""

            If dt.Rows.Count > Val(lblTedadMandeh.Text.Trim) Then
                MsgBox("تعداد کالای موجود در فایل Excel بیشتر از تعداد کالای بدون سریال است . امکان ثبت وجود ندارد !", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, "خطا")
                Exit Sub
            End If

            For i As Integer = 0 To dt.Rows.Count - 1
                If IsValid(tcInsert.SelectedIndex, dt.Rows(i)("SN")) = False Then
                    ConutRecordTekrari += 1
                    strRecordTekrari &= dt.Rows(i)("SN") & ","
                Else
                    InsertSerial(dt.Rows(i)("SN"))
                End If
            Next

            If ConutRecordTekrari > 0 Then
                strRecordTekrari = strRecordTekrari.Substring(0, strRecordTekrari.Length - 1)
                MsgBox("از ثبت " & ConutRecordTekrari.ToString & " عدد از سریال ها به علت تکراری بودن جلوگیری شد ." & vbCrLf _
                       & "شماره سریال های : " & strRecordTekrari, MsgBoxStyle.Information + MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, "")
            End If

            SearchSerial()
            ClearExcelMode()
            SetFormTedadi()
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "Error IN DataBase ----> btnConvertFileExcel_Click")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, "Error IN ----> btnConvertFileExcel_Click")
        End Try
    End Sub
End Class