Public Class frmFO_InsertFaktor
#Region "Variable AND Constant Declration"
    'Const cntCodeSubSystem As Long = 625

    Dim cmTitr As CurrencyManager
    Dim ErrPro As New ErrorProvider
    Dim dsForm As New DataSet
    Dim dvForm As DataView
    Private SN As Integer
    Dim txtCaption As String
    Private WithEvents BS As New UD_Dll.PassString

    Public frm_ccPeygiriFaktor As Integer
#End Region
#Region "Form Event Code"
    Private Sub frmFO_InsertKala_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            SetParameter()
            LoadCombo()
            Search(False)
            objTools.ChangeKbd(UD_Dll.mdlUtility.KBD.Farsi)

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->frm_Load")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->frm_Load")
        End Try
    End Sub
    Private Sub LoadCombo()
        Dim strSQL As String
        Dim cnSQL As New SqlConnection
        Dim cmSQL As New SqlCommand
        Dim daSQL As SqlDataAdapter
        Dim dr As DataRow

        Try

            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()

            strSQL = "Global.spCodeDoreh_LoadCombo "

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            daSQL = New SqlDataAdapter(cmSQL)
            daSQL.Fill(dsForm, "tbl_DorehS")

            cmbDorehS.DataSource = Nothing
            cmbDorehS.Items.Clear()
            cmbDorehS.DataSource = dsForm.Tables("tbl_DorehS").DefaultView
            cmbDorehS.DisplayMember = "txtDoreh"
            cmbDorehS.ValueMember = "CodeDoreh"

            cmbDorehS.SelectedValue = CodeDoreh

            cmSQL = Nothing
            daSQL = Nothing
            cnSQL.Close()

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> LoadCombo")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> LoadCombo")
        End Try
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
            CodeDoreh = "1395"
            txtCaption = "پیش فاکتور"
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
    Public Sub Search(ByVal Type As Boolean) '' True --> Search Koli \\ False --> Load Kardan Safhe Be Sorat Khali
        Try
            Dim strSQL As String

            strSQL = "Sales.spPeygiriFaktor_frmInsertFaktor_Search "

            RefreshTitrdata(strSQL, Type)

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->Search")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->Search")
        End Try
    End Sub
    Private Sub RefreshTitrdata(ByVal strSql As String, ByVal Type As Boolean) '' True --> Search Koli \\ False --> Load Kardan Safhe Be Sorat Khali
        Dim daSQL As SqlDataAdapter
        Dim cnSQL As New SqlConnection
        Dim cmSQL As New SqlCommand
        Try

            If dsForm.Tables.Contains("HETitr") Then
                dsForm.Tables.Remove("HETitr")
            End If

            cnSQL.ConnectionString = ConnectionString
            cnSQL.Open()

            cmSQL = New SqlCommand(strSql, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            daSQL = New SqlDataAdapter(cmSQL)
            daSQL.Fill(dsForm, "HETitr")
            dvForm = New DataView
            dvForm = dsForm.Tables("HETitr").DefaultView
            dvForm.Sort = "ShomarehFaktor DESC"
            dvForm.AllowDelete = True
            dvForm.AllowEdit = True
            dvForm.AllowNew = True
            daSQL = Nothing

            dvForm = New DataView(dsForm.Tables("HETitr"), "", "ShomarehFaktor DESC", DataViewRowState.CurrentRows)
            dvForm.AllowNew = True
            dvForm.AllowDelete = True
            dvForm.AllowEdit = True

            cmSQL.Connection.Close()
            cnSQL.Close()
            daSQL = Nothing

            GridEXFaktor.DataSource = Nothing
            GridEXFaktor.DataSource = dvForm

            SetGridStyle()
            BoundCurrencyManager(cmSQL)

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->RefreshTitrdata")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->RefreshTitrdata")
        Finally
            cmSQL.Dispose()
            cnSQL.Dispose()
        End Try
    End Sub
    Private Sub SetGridStyle()
        Try
            With GridEXFaktor
                .DataSource = Nothing
                .DataSource = dsForm.Tables("HETitr").DefaultView
                .SetDataBinding(dsForm.Tables("HETitr").DefaultView, "")
                .RetrieveStructure()
            End With

            For i As Integer = 0 To GridEXFaktor.CurrentTable.Columns.Count - 1
                GridEXFaktor.CurrentTable.Columns.Item(i).Visible = False
            Next

            GridEXFaktor.CurrentTable.Columns.Item("ShomarehFaktor").Caption = "شماره فاکتور"
            GridEXFaktor.CurrentTable.Columns.Item("ShomarehFaktor").Visible = True
            GridEXFaktor.CurrentTable.Columns.Item("ShomarehFaktor").Width = 100
            GridEXFaktor.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXFaktor.CurrentTable.Columns.Item("ShomarehFaktor").Position = 0
            GridEXFaktor.CurrentTable.Columns.Item("ShomarehFaktor").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXFaktor.CurrentTable.Columns.Item("TarikhFaktor").Caption = "تاریخ فاکتور"
            GridEXFaktor.CurrentTable.Columns.Item("TarikhFaktor").Visible = True
            GridEXFaktor.CurrentTable.Columns.Item("TarikhFaktor").Width = 90
            GridEXFaktor.CurrentTable.Columns.Item("TarikhFaktor").EditType = Janus.Windows.GridEX.EditType.NoEdit
            GridEXFaktor.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXFaktor.CurrentTable.Columns.Item("TarikhFaktor").Position = 1
            GridEXFaktor.CurrentTable.Columns.Item("TarikhFaktor").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXFaktor.CurrentTable.Columns.Item("MablaghFaktor").Caption = "مبلغ فاکتور"
            GridEXFaktor.CurrentTable.Columns.Item("MablaghFaktor").Visible = True
            GridEXFaktor.CurrentTable.Columns.Item("MablaghFaktor").Width = 100
            GridEXFaktor.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXFaktor.CurrentTable.Columns.Item("MablaghFaktor").EditType = Janus.Windows.GridEX.EditType.NoEdit
            GridEXFaktor.CurrentTable.Columns.Item("MablaghFaktor").Position = 2
            GridEXFaktor.CurrentTable.Columns.Item("MablaghFaktor").FormatString = "N"
            GridEXFaktor.CurrentTable.Columns.Item("MablaghFaktor").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXFaktor.CurrentTable.Columns.Item("NameMoshtary").Caption = "نام مشتـــری"
            GridEXFaktor.CurrentTable.Columns.Item("NameMoshtary").Visible = True
            GridEXFaktor.CurrentTable.Columns.Item("NameMoshtary").Width = 200
            GridEXFaktor.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOn
            GridEXFaktor.CurrentTable.Columns.Item("NameMoshtary").EditType = Janus.Windows.GridEX.EditType.NoEdit
            GridEXFaktor.CurrentTable.Columns.Item("NameMoshtary").Position = 3
            GridEXFaktor.CurrentTable.Columns.Item("NameMoshtary").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXFaktor.CurrentTable.Columns.Item("Tozihat").Caption = "توضیحــــات"
            GridEXFaktor.CurrentTable.Columns.Item("Tozihat").Visible = True
            GridEXFaktor.CurrentTable.Columns.Item("Tozihat").Width = 535
            GridEXFaktor.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXFaktor.CurrentTable.Columns.Item("Tozihat").Position = 4
            GridEXFaktor.CurrentTable.Columns.Item("Tozihat").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            For i As Integer = 0 To GridEXFaktor.RootTable.Columns.Count - 1
                If GridEXFaktor.RootTable.Columns(i).Type.IsValueType Then
                    GridEXFaktor.RootTable.Columns(i).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                    GridEXFaktor.RootTable.Columns(i).FormatMode = Janus.Windows.GridEX.FormatMode.UseIFormattable
                    GridEXFaktor.RootTable.Columns(i).FormatString = "###,###.##"
                    GridEXFaktor.RootTable.Columns(i).TotalFormatMode = Janus.Windows.GridEX.FormatMode.UseIFormattable
                    GridEXFaktor.RootTable.Columns(i).TotalFormatString = "###,###.##"
                End If
            Next

            GridEXFaktor.Visible = True
            GridEXFaktor.HeaderFormatStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> SetGridStyle ")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> SetGridStyle ")
        End Try

    End Sub
    Private Sub BoundCurrencyManager(ByVal cm As SqlCommand)
        Try
            cmTitr = CType(BindingContext(GridEXFaktor.DataSource), CurrencyManager)
            AddHandler cmTitr.PositionChanged, AddressOf cmTitr_PositionChanged

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->BoundCurrencyManagerTitr")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->BoundCurrencyManagerTitr")
        End Try
    End Sub
    Private Sub cmTitr_PositionChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim cm As New SqlCommand
        Try

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->cmTitr_PositionChanged")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->cmTitr_PositionChanged")
        End Try

    End Sub
    Private Sub GridEXInsertKala_CurrentCellChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GridEXFaktor.CurrentCellChanged
        If GridEXFaktor.CurrentRow.Selected = False Then
            Exit Sub
        End If

        Dim ccFaktorTitr As Integer = 0
        Dim ShomarehFaktor As Integer = 0
        Dim TarikhFaktor As String = ""
        Dim MablaghFaktor As Integer = 0
        Dim ccMoshtary As Integer = 0
        Dim NameMoshtary As String = ""

        If GridEXFaktor.CurrentRow.RowType = Janus.Windows.GridEX.RowType.Record Then
            If GridEXFaktor.CurrentColumn.Index = 2 And GridEXFaktor.CurrentRow.Cells(1).Text <> "" Then

                ccFaktorTitr = objTools.ConvertNulls(objTools.DLookup("ccFaktorTitr", "tblFO_Faktor", "CodeMahal = " & CodeMahalFaal & " AND CodeDoreh = " & cmbDorehS.SelectedValue & " AND FaktorShomareh = " & GridEXFaktor.CurrentRow.Cells(1).Text.Replace(",", "")), 0)
                ShomarehFaktor = Val(GridEXFaktor.CurrentRow.Cells(1).Text.Replace(",", ""))
                TarikhFaktor = objTools.ConvertNulls(objTools.DLookup("dbo.SetDateSlash(FaktorTarikh)", "tblFO_Faktor", "ccFaktorTitr = " & ccFaktorTitr), "---")
                MablaghFaktor = objTools.ConvertNulls(objTools.DLookup("JamKol", "tblFO_Faktor", "ccFaktorTitr = " & ccFaktorTitr), 0)
                ccMoshtary = objTools.ConvertNulls(objTools.DLookup("ccMoshtary", "tblFO_Faktor", "ccFaktorTitr = " & ccFaktorTitr), 0)
                NameMoshtary = objTools.ConvertNulls(objTools.DLookup("NameMoshtary", "tblFO_Moshtary", "ccMoshtary = " & ccMoshtary), 0)

                If Not IsValidFaktor("All", ccFaktorTitr, ShomarehFaktor) Then
                    SendKeys.Send("{Esc}")
                    GridEXFaktor.CurrentRow.Delete()
                    Exit Sub
                End If

                If Not IsValidFaktorMandehDar(ccFaktorTitr) Then
                    MsgBox("شماره فاکتور وارد شده تسویه شده است .", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "تایید")
                    SendKeys.Send("{Esc}")
                    GridEXFaktor.CurrentRow.Delete()
                    Exit Sub
                End If

                GridEXFaktor.CurrentRow.Cells(2).Text = TarikhFaktor
                GridEXFaktor.CurrentRow.Cells(3).Text = MablaghFaktor
                GridEXFaktor.CurrentRow.Cells(4).Text = NameMoshtary

                For i As Integer = 0 To 2
                    SendKeys.Send("{Tab}")
                Next
            ElseIf GridEXFaktor.CurrentColumn.Index = 6 Then
                AddRow()
            End If
        End If
    End Sub
    Private Sub AddRow()
        Dim d As DataRow

        d = dsForm.Tables("HETitr").NewRow
        d("ShomarehFaktor") = GridEXFaktor.CurrentRow.Cells("ShomarehFaktor").Text.Replace(",", "")
        d("TarikhFaktor") = GridEXFaktor.CurrentRow.Cells("TarikhFaktor").Text
        d("MablaghFaktor") = GridEXFaktor.CurrentRow.Cells("MablaghFaktor").Text.Replace(",", "")
        d("NameMoshtary") = GridEXFaktor.CurrentRow.Cells("NameMoshtary").Text
        d("Tozihat") = GridEXFaktor.CurrentRow.Cells("Tozihat").Text

        dsForm.Tables("HETitr").Rows.Add(d)

        dvForm = dsForm.Tables("HETitr").DefaultView
    End Sub
    Private Function IsValidFaktor(ByVal chkField As String, ByVal ccFaktorTitr As Integer, ByVal ShomarehFaktor As Integer) As Boolean
        Try
            IsValidFaktor = False

            If chkField = "ShomarehFaktor" Or chkField = "All" Then
                If objTools.ConvertNulls(objTools.DCount("FaktorShomareh", "tblFO_Faktor", "FaktorShomareh = " & ShomarehFaktor & " AND CodeMahal = " & CodeMahalFaal & " AND CodeDoreh = " & cmbDorehS.SelectedValue), 0) = 0 Then
                    MsgBox("شماره فاکتور وارد شده در دوره مالی و مرکز پخش انتخاب شده وجود ندارد .", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "تایید")
                    Exit Function
                End If
            End If

            If chkField = "ccFaktorTitr" Or chkField = "All" Then
                If objTools.ConvertNulls(objTools.DCount("ccFaktorTitr", "Sales.PeygiriFaktorSatr", "ccFaktorTitr = " & ccFaktorTitr & " AND ccPeygiriFaktor = " & frm_ccPeygiriFaktor), 0) >= 1 Then
                    MsgBox("فاکتور مورد نظر دراین پیگیری وجود دارد .", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "تایید")
                    Exit Function
                End If
            End If

            Return True
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> IsValidKala ")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> IsValidKala ")
        End Try

    End Function
    Private Function IsValidFaktorMandehDar(ByVal ccFaktor As Integer) As Boolean
        Try
            IsValidFaktorMandehDar = False

            Dim cnSQL As New SqlConnection
            Dim cmSQL As New SqlCommand
            Dim strSQL As String = ""

            strSQL = "Sales.spPeygiriFaktor_ValidFaktorMandehDar "

            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            cmSQL.Parameters.AddWithValue("ccFaktorTitr", ccFaktor)
            cmSQL.Parameters.AddWithValue("IsValid", IsValidFaktorMandehDar)
            cmSQL.Parameters("IsValid").Direction = ParameterDirection.Output

            cmSQL.ExecuteNonQuery()

            IsValidFaktorMandehDar = cmSQL.Parameters("IsValid").Value

            cmSQL = Nothing
            cnSQL.Close()

            Return IsValidFaktorMandehDar
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> IsValidKala ")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> IsValidKala ")
        End Try

    End Function
    Private Sub InsertFaktor()
        Try
            Dim dr As DataRowView

            For Each dr In dvForm
                If objTools.ConvertNulls(dr("ShomarehFaktor"), "") <> "" Then

                    Dim cnSQL As SqlConnection
                    Dim cmSQL As SqlCommand
                    Dim strSQL As String


                    strSQL = "Sales.spPeygiriFaktor_InsertSatr_InsertFaktor "

                    cnSQL = New SqlConnection(ConnectionString)
                    cnSQL.Open()

                    cmSQL = New SqlCommand(strSQL, cnSQL)
                    cmSQL.CommandType = CommandType.StoredProcedure
                    cmSQL.Parameters.Clear()

                    cmSQL.Parameters.AddWithValue("ccPeygiriFaktor", frm_ccPeygiriFaktor)
                    cmSQL.Parameters.AddWithValue("FaktorShomareh", dr("ShomarehFaktor"))
                    cmSQL.Parameters.AddWithValue("Tozihat", dr("Tozihat"))
                    cmSQL.Parameters.AddWithValue("CodeDoreh", cmbDorehS.SelectedValue)

                    cmSQL.ExecuteNonQuery()

                    cmSQL.Connection.Close()
                    cnSQL.Close()

                    cmSQL = Nothing
                    cnSQL = Nothing
                End If
            Next

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
#End Region
#Region "From Buttons "
    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub
    Private Sub btnEnter_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEnter.Click
        InsertFaktor()
        Me.Close()
    End Sub
#End Region
End Class