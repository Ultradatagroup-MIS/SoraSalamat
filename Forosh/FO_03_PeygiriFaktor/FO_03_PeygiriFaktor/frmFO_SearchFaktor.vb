Public Class frmFO_SearchFaktor
#Region "Variable AND Constant Declration"
    'Const cntCodeSubSystem As Long = 625

    Dim cmTitr As CurrencyManager
    Dim ErrPro As New ErrorProvider
    Dim dsForm As New DataSet
    Dim dvForm As DataView
    Private SN As Integer
    Dim txtCaption As String
    Private WithEvents BS As New UD_Dll.PassString
    Dim str_ccFaktor As String = ""
    Dim str_ShomarehFaktor As String = ""
    Dim Flag As Boolean = False
#End Region
#Region "Form Event Code"
    Private Sub frmFO_SearchFaktor_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            SetParameter()
            LoadCombo()
            cmbDorehS.SelectedValue = CodeDoreh
            Search(False)
            objTools.ChangeKbd(UD_Dll.mdlUtility.KBD.Farsi)
            Flag = True

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> frmFO_SearchFaktor_Load ")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> frmFO_SearchFaktor_Load ")
        End Try
    End Sub
    Private Sub cmbDorehS_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbDorehS.SelectedIndexChanged
        If Flag = True Then
            Search(False)
        End If
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
            CodeDoreh = "1392"
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

            cmSQL = Nothing
            daSQL = Nothing
            cnSQL.Close()

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> LoadCombo")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> LoadCombo")
        End Try
    End Sub
    Public Sub Search(ByVal Type As Boolean)
        Try
            Dim strSQL As String

            strSQL = "Sales.spPeygiriFaktor_SearchFaktorMandehDar "

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

            cmSQL.Parameters.AddWithValue("CodeMahal", CodeMahalFaal)
            cmSQL.Parameters.AddWithValue("CodeDoreh", cmbDorehS.SelectedValue)

            daSQL = New SqlDataAdapter(cmSQL)
            daSQL.Fill(dsForm, "HETitr")
            dvForm = New DataView
            dvForm = dsForm.Tables("HETitr").DefaultView
            dvForm.Sort = "ShomarehFaktor ASC"
            dvForm.AllowDelete = True
            dvForm.AllowEdit = True
            dvForm.AllowNew = True
            daSQL = Nothing

            Dim col As DataColumn
            '------------Adding Columns------------
            col = New DataColumn
            col.ColumnName = "Taeed"
            col.DataType = GetType(Boolean)
            col.DefaultValue = False
            dsForm.Tables("HETitr").Columns.Add(col)
            '-----------------------------------------

            dvForm = New DataView(dsForm.Tables("HETitr"), "", "ShomarehFaktor ASC", DataViewRowState.CurrentRows)
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

            GridEXFaktor.CurrentTable.Columns.Item("Taeed").Caption = "انتخاب"
            GridEXFaktor.CurrentTable.Columns.Item("Taeed").Visible = True
            GridEXFaktor.CurrentTable.Columns.Item("Taeed").Width = 20
            GridEXFaktor.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXFaktor.CurrentTable.Columns.Item("Taeed").Position = 0
            GridEXFaktor.CurrentTable.Columns.Item("Taeed").Selectable = True
            GridEXFaktor.CurrentTable.Columns.Item("Taeed").ActAsSelector = True
            GridEXFaktor.CurrentTable.Columns.Item("Taeed").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXFaktor.CurrentTable.Columns.Item("CodeMoshtary").Caption = "کــد مشتـــری"
            GridEXFaktor.CurrentTable.Columns.Item("CodeMoshtary").Visible = True
            GridEXFaktor.CurrentTable.Columns.Item("CodeMoshtary").Width = 90
            GridEXFaktor.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOn
            GridEXFaktor.CurrentTable.Columns.Item("CodeMoshtary").EditType = Janus.Windows.GridEX.EditType.NoEdit
            GridEXFaktor.CurrentTable.Columns.Item("CodeMoshtary").Position = 1
            GridEXFaktor.CurrentTable.Columns.Item("CodeMoshtary").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXFaktor.CurrentTable.Columns.Item("NameMoshtary").Caption = "نام مشتـــری"
            GridEXFaktor.CurrentTable.Columns.Item("NameMoshtary").Visible = True
            GridEXFaktor.CurrentTable.Columns.Item("NameMoshtary").Width = 268
            GridEXFaktor.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOn
            GridEXFaktor.CurrentTable.Columns.Item("NameMoshtary").EditType = Janus.Windows.GridEX.EditType.NoEdit
            GridEXFaktor.CurrentTable.Columns.Item("NameMoshtary").Position = 2
            GridEXFaktor.CurrentTable.Columns.Item("NameMoshtary").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXFaktor.CurrentTable.Columns.Item("ShomarehFaktor").Caption = "شماره فاکتور"
            GridEXFaktor.CurrentTable.Columns.Item("ShomarehFaktor").Visible = True
            GridEXFaktor.CurrentTable.Columns.Item("ShomarehFaktor").Width = 100
            GridEXFaktor.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXFaktor.CurrentTable.Columns.Item("ShomarehFaktor").Position = 3
            GridEXFaktor.CurrentTable.Columns.Item("ShomarehFaktor").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXFaktor.CurrentTable.Columns.Item("TarikhFaktor").Caption = "تاریخ فاکتور"
            GridEXFaktor.CurrentTable.Columns.Item("TarikhFaktor").Visible = True
            GridEXFaktor.CurrentTable.Columns.Item("TarikhFaktor").Width = 90
            GridEXFaktor.CurrentTable.Columns.Item("TarikhFaktor").EditType = Janus.Windows.GridEX.EditType.NoEdit
            GridEXFaktor.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXFaktor.CurrentTable.Columns.Item("TarikhFaktor").Position = 4
            GridEXFaktor.CurrentTable.Columns.Item("TarikhFaktor").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXFaktor.CurrentTable.Columns.Item("MablaghFaktor").Caption = "مبلغ فاکتور"
            GridEXFaktor.CurrentTable.Columns.Item("MablaghFaktor").Visible = True
            GridEXFaktor.CurrentTable.Columns.Item("MablaghFaktor").Width = 110
            GridEXFaktor.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXFaktor.CurrentTable.Columns.Item("MablaghFaktor").EditType = Janus.Windows.GridEX.EditType.NoEdit
            GridEXFaktor.CurrentTable.Columns.Item("MablaghFaktor").Position = 5
            GridEXFaktor.CurrentTable.Columns.Item("MablaghFaktor").FormatString = "N"
            GridEXFaktor.CurrentTable.Columns.Item("MablaghFaktor").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXFaktor.CurrentTable.Columns.Item("JamPardakhti").Caption = "پرداختی فاکتور"
            GridEXFaktor.CurrentTable.Columns.Item("JamPardakhti").Visible = True
            GridEXFaktor.CurrentTable.Columns.Item("JamPardakhti").Width = 110
            GridEXFaktor.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXFaktor.CurrentTable.Columns.Item("JamPardakhti").EditType = Janus.Windows.GridEX.EditType.NoEdit
            GridEXFaktor.CurrentTable.Columns.Item("JamPardakhti").Position = 6
            GridEXFaktor.CurrentTable.Columns.Item("JamPardakhti").FormatString = "N"
            GridEXFaktor.CurrentTable.Columns.Item("JamPardakhti").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXFaktor.CurrentTable.Columns.Item("Mandeh").Caption = "مانده فاکتور"
            GridEXFaktor.CurrentTable.Columns.Item("Mandeh").Visible = True
            GridEXFaktor.CurrentTable.Columns.Item("Mandeh").Width = 110
            GridEXFaktor.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXFaktor.CurrentTable.Columns.Item("Mandeh").EditType = Janus.Windows.GridEX.EditType.NoEdit
            GridEXFaktor.CurrentTable.Columns.Item("Mandeh").Position = 7
            GridEXFaktor.CurrentTable.Columns.Item("Mandeh").FormatString = "N"
            GridEXFaktor.CurrentTable.Columns.Item("Mandeh").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXFaktor.CurrentTable.Columns.Item("ccFaktorTitr").Caption = "ccFaktorTitr"
            GridEXFaktor.CurrentTable.Columns.Item("ccFaktorTitr").Visible = False
            GridEXFaktor.CurrentTable.Columns.Item("ccFaktorTitr").Width = 0
            GridEXFaktor.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXFaktor.CurrentTable.Columns.Item("ccFaktorTitr").Position = 8
            GridEXFaktor.CurrentTable.Columns.Item("ccFaktorTitr").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

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
#End Region
#Region "From Buttons "
    Private Sub btnTaeed_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTaeed.Click
        For i As Integer = 0 To GridEXFaktor.GetCheckedRows().Length - 1
            str_ccFaktor &= Val(GridEXFaktor.GetCheckedRows(i).Cells("ccFaktorTitr").Text.Replace(",", "")) & ","
            str_ShomarehFaktor &= Val(GridEXFaktor.GetCheckedRows(i).Cells("ShomarehFaktor").Text.Replace(",", "")) & ","
        Next

        If str_ccFaktor <> "" Then
            str_ccFaktor = "," & str_ccFaktor
            str_ShomarehFaktor = str_ShomarehFaktor.Remove(str_ShomarehFaktor.Length - 1, 1)
        End If

        frmFO_PeygiriFaktor.str_ccFaktor = str_ccFaktor
        frmFO_PeygiriFaktor.txtShomarehFaktor.Text = str_ShomarehFaktor

        Me.Close()

    End Sub
    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub
#End Region
End Class