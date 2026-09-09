Public Class frmFO_SearchPishFaktor
#Region "Variable AND Constant Declration"
    'Const cntCodeSubSystem As Long = 625

    Dim cmTitr As CurrencyManager
    Dim ErrPro As New ErrorProvider
    Dim dsForm As New DataSet
    Dim dvForm As DataView
    Private SN As Integer
    Dim txtCaption As String
    Private WithEvents BS As New UD_Dll.PassString
    Dim Flag As Boolean = False
#End Region
#Region "Form Event Code"
    Private Sub frmFO_SearchFaktor_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            SetParameter()
            Search(False)
            objTools.ChangeKbd(UD_Dll.mdlUtility.KBD.Farsi)
            Flag = True

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> frmFO_SearchFaktor_Load ")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> frmFO_SearchFaktor_Load ")
        End Try
    End Sub
    Private Sub cmbDorehS_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
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
            CodeMahalFaal = "2060"
            PersonelCode = "0"
            PersonelName = "Administrator"
            CodeDoreh = "1396"
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
    Public Sub Search(ByVal Type As Boolean)
        Try
            Dim strSQL As String

            strSQL = "Sales.spElamMarjoee_PishFaktor_SearchMandehDar "

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
            cmSQL.Parameters.AddWithValue("CodeDoreh", frmFO_ElamMarjoee_PishFaktor.cmbsCodeDorehPishFaktor.SelectedValue)

            daSQL = New SqlDataAdapter(cmSQL)
            daSQL.Fill(dsForm, "HETitr")
            dvForm = New DataView
            dvForm = dsForm.Tables("HETitr").DefaultView
            dvForm.Sort = "PishFaktorShomareh ASC"
            dvForm.AllowDelete = True
            dvForm.AllowEdit = True
            dvForm.AllowNew = True
            daSQL = Nothing

            dvForm = New DataView(dsForm.Tables("HETitr"), "", "PishFaktorShomareh ASC", DataViewRowState.CurrentRows)
            dvForm.AllowNew = True
            dvForm.AllowDelete = True
            dvForm.AllowEdit = True

            cmSQL.Connection.Close()
            cnSQL.Close()
            daSQL = Nothing

            GridEXPishFaktor.DataSource = Nothing
            GridEXPishFaktor.DataSource = dvForm

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
            With GridEXPishFaktor
                .DataSource = Nothing
                .DataSource = dsForm.Tables("HETitr").DefaultView
                .SetDataBinding(dsForm.Tables("HETitr").DefaultView, "")
                .RetrieveStructure()
            End With

            For i As Integer = 0 To GridEXPishFaktor.CurrentTable.Columns.Count - 1
                GridEXPishFaktor.CurrentTable.Columns.Item(i).Visible = False
            Next

            GridEXPishFaktor.CurrentTable.Columns.Item("CodeMoshtary").Caption = "کــد مشتـــری"
            GridEXPishFaktor.CurrentTable.Columns.Item("CodeMoshtary").Visible = True
            GridEXPishFaktor.CurrentTable.Columns.Item("CodeMoshtary").Width = 90
            GridEXPishFaktor.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOn
            'GridEXPishFaktor.CurrentTable.Columns.Item("CodeMoshtary").EditType = Janus.Windows.GridEX.EditType.NoEdit
            GridEXPishFaktor.CurrentTable.Columns.Item("CodeMoshtary").Position = 0
            GridEXPishFaktor.CurrentTable.Columns.Item("CodeMoshtary").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXPishFaktor.CurrentTable.Columns.Item("NameMoshtary").Caption = "نام مشتـــری"
            GridEXPishFaktor.CurrentTable.Columns.Item("NameMoshtary").Visible = True
            GridEXPishFaktor.CurrentTable.Columns.Item("NameMoshtary").Width = 268
            GridEXPishFaktor.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOn
            'GridEXPishFaktor.CurrentTable.Columns.Item("NameMoshtary").EditType = Janus.Windows.GridEX.EditType.NoEdit
            GridEXPishFaktor.CurrentTable.Columns.Item("NameMoshtary").Position = 1
            GridEXPishFaktor.CurrentTable.Columns.Item("NameMoshtary").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXPishFaktor.CurrentTable.Columns.Item("PishFaktorShomareh").Caption = "شماره پیش فاکتور"
            GridEXPishFaktor.CurrentTable.Columns.Item("PishFaktorShomareh").Visible = True
            GridEXPishFaktor.CurrentTable.Columns.Item("PishFaktorShomareh").Width = 115
            'GridEXPishFaktor.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXPishFaktor.CurrentTable.Columns.Item("PishFaktorShomareh").Position = 2
            GridEXPishFaktor.CurrentTable.Columns.Item("PishFaktorShomareh").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXPishFaktor.CurrentTable.Columns.Item("PishFaktorTarikh").Caption = "تاریخ پیش فاکتور"
            GridEXPishFaktor.CurrentTable.Columns.Item("PishFaktorTarikh").Visible = True
            GridEXPishFaktor.CurrentTable.Columns.Item("PishFaktorTarikh").Width = 105
            'GridEXPishFaktor.CurrentTable.Columns.Item("PishFaktorTarikh").EditType = Janus.Windows.GridEX.EditType.NoEdit
            GridEXPishFaktor.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXPishFaktor.CurrentTable.Columns.Item("PishFaktorTarikh").Position = 3
            GridEXPishFaktor.CurrentTable.Columns.Item("PishFaktorTarikh").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXPishFaktor.CurrentTable.Columns.Item("NameAnbar").Caption = "صادره از انبار"
            GridEXPishFaktor.CurrentTable.Columns.Item("NameAnbar").Visible = True
            GridEXPishFaktor.CurrentTable.Columns.Item("NameAnbar").Width = 110
            GridEXPishFaktor.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            'GridEXPishFaktor.CurrentTable.Columns.Item("NameAnbar").EditType = Janus.Windows.GridEX.EditType.NoEdit
            GridEXPishFaktor.CurrentTable.Columns.Item("NameAnbar").Position = 4
            GridEXPishFaktor.CurrentTable.Columns.Item("NameAnbar").FormatString = "N"
            GridEXPishFaktor.CurrentTable.Columns.Item("NameAnbar").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXPishFaktor.CurrentTable.Columns.Item("TedadMandeh").Caption = "تعداد مانده"
            GridEXPishFaktor.CurrentTable.Columns.Item("TedadMandeh").Visible = True
            GridEXPishFaktor.CurrentTable.Columns.Item("TedadMandeh").Width = 70
            GridEXPishFaktor.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXPishFaktor.CurrentTable.Columns.Item("TedadMandeh").EditType = Janus.Windows.GridEX.EditType.NoEdit
            GridEXPishFaktor.CurrentTable.Columns.Item("TedadMandeh").Position = 5
            GridEXPishFaktor.CurrentTable.Columns.Item("TedadMandeh").FormatString = "N"
            GridEXPishFaktor.CurrentTable.Columns.Item("TedadMandeh").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXPishFaktor.CurrentTable.Columns.Item("TedadPishFaktorShodeh").Caption = "تعداد کالا"
            GridEXPishFaktor.CurrentTable.Columns.Item("TedadPishFaktorShodeh").Visible = True
            GridEXPishFaktor.CurrentTable.Columns.Item("TedadPishFaktorShodeh").Width = 70
            GridEXPishFaktor.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXPishFaktor.CurrentTable.Columns.Item("TedadPishFaktorShodeh").EditType = Janus.Windows.GridEX.EditType.NoEdit
            GridEXPishFaktor.CurrentTable.Columns.Item("TedadPishFaktorShodeh").Position = 6
            GridEXPishFaktor.CurrentTable.Columns.Item("TedadPishFaktorShodeh").FormatString = "G"
            GridEXPishFaktor.CurrentTable.Columns.Item("TedadPishFaktorShodeh").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXPishFaktor.CurrentTable.Columns.Item("TedadFaktorShodeh").Caption = "فاکتور شده"
            GridEXPishFaktor.CurrentTable.Columns.Item("TedadFaktorShodeh").Visible = True
            GridEXPishFaktor.CurrentTable.Columns.Item("TedadFaktorShodeh").Width = 80
            GridEXPishFaktor.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXPishFaktor.CurrentTable.Columns.Item("TedadFaktorShodeh").EditType = Janus.Windows.GridEX.EditType.NoEdit
            GridEXPishFaktor.CurrentTable.Columns.Item("TedadFaktorShodeh").Position = 7
            GridEXPishFaktor.CurrentTable.Columns.Item("TedadFaktorShodeh").FormatString = "G"
            GridEXPishFaktor.CurrentTable.Columns.Item("TedadFaktorShodeh").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXPishFaktor.CurrentTable.Columns.Item("TedadMarjoeeShodeh").Caption = "مرجوع شده"
            GridEXPishFaktor.CurrentTable.Columns.Item("TedadMarjoeeShodeh").Visible = True
            GridEXPishFaktor.CurrentTable.Columns.Item("TedadMarjoeeShodeh").Width = 80
            GridEXPishFaktor.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXPishFaktor.CurrentTable.Columns.Item("TedadMarjoeeShodeh").EditType = Janus.Windows.GridEX.EditType.NoEdit
            GridEXPishFaktor.CurrentTable.Columns.Item("TedadMarjoeeShodeh").Position = 8
            GridEXPishFaktor.CurrentTable.Columns.Item("TedadMarjoeeShodeh").FormatString = "G"
            GridEXPishFaktor.CurrentTable.Columns.Item("TedadMarjoeeShodeh").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXPishFaktor.CurrentTable.Columns.Item("ccPishFaktorTitr").Caption = "ccPishFaktorTitr"
            GridEXPishFaktor.CurrentTable.Columns.Item("ccPishFaktorTitr").Visible = False
            GridEXPishFaktor.CurrentTable.Columns.Item("ccPishFaktorTitr").Width = 0
            GridEXPishFaktor.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXPishFaktor.CurrentTable.Columns.Item("ccPishFaktorTitr").Position = 9
            GridEXPishFaktor.CurrentTable.Columns.Item("ccPishFaktorTitr").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXPishFaktor.CurrentTable.Columns.Item("ccMoshtary").Caption = "ccMoshtary"
            GridEXPishFaktor.CurrentTable.Columns.Item("ccMoshtary").Visible = False
            GridEXPishFaktor.CurrentTable.Columns.Item("ccMoshtary").Width = 0
            GridEXPishFaktor.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXPishFaktor.CurrentTable.Columns.Item("ccMoshtary").Position = 10
            GridEXPishFaktor.CurrentTable.Columns.Item("ccMoshtary").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXPishFaktor.CurrentTable.Columns.Item("ccAnbar").Caption = "ccAnbar"
            GridEXPishFaktor.CurrentTable.Columns.Item("ccAnbar").Visible = False
            GridEXPishFaktor.CurrentTable.Columns.Item("ccAnbar").Width = 0
            GridEXPishFaktor.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXPishFaktor.CurrentTable.Columns.Item("ccAnbar").Position = 11
            GridEXPishFaktor.CurrentTable.Columns.Item("ccAnbar").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            For i As Integer = 0 To GridEXPishFaktor.RootTable.Columns.Count - 1
                If GridEXPishFaktor.RootTable.Columns(i).Type.IsValueType Then
                    GridEXPishFaktor.RootTable.Columns(i).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                    GridEXPishFaktor.RootTable.Columns(i).FormatMode = Janus.Windows.GridEX.FormatMode.UseIFormattable
                    GridEXPishFaktor.RootTable.Columns(i).FormatString = "###,###.##"
                    GridEXPishFaktor.RootTable.Columns(i).TotalFormatMode = Janus.Windows.GridEX.FormatMode.UseIFormattable
                    GridEXPishFaktor.RootTable.Columns(i).TotalFormatString = "###,###.##"
                End If
            Next

            GridEXPishFaktor.Visible = True
            GridEXPishFaktor.HeaderFormatStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> SetGridStyle ")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> SetGridStyle ")
        End Try

    End Sub
    Private Sub BoundCurrencyManager(ByVal cm As SqlCommand)
        Try
            cmTitr = CType(BindingContext(GridEXPishFaktor.DataSource), CurrencyManager)
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
        frmFO_ElamMarjoee_PishFaktor.txtShomarehPishFaktor.Tag = Val(GridEXPishFaktor.CurrentRow.Cells("ccPishFaktorTitr").Text.Replace(",", ""))
        frmFO_ElamMarjoee_PishFaktor.txtShomarehPishFaktor.Text = Val(GridEXPishFaktor.CurrentRow.Cells("PishFaktorShomareh").Text.Replace(",", ""))
        frmFO_ElamMarjoee_PishFaktor.lblNameMoshtary.Text = GridEXPishFaktor.CurrentRow.Cells("CodeMoshtary").Text.Replace(",", "").ToString & " - " & GridEXPishFaktor.CurrentRow.Cells("NameMoshtary").Text.ToString
        frmFO_ElamMarjoee_PishFaktor.cmbAnbar.SelectedValue = Val(GridEXPishFaktor.CurrentRow.Cells("ccAnbar").Text.Replace(",", ""))
        frmFO_ElamMarjoee_PishFaktor.FlgValidPishFaktor = True
        Me.Close()
    End Sub
    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub
    Private Sub GridEXFaktor_DoubleClick(sender As Object, e As EventArgs) Handles GridEXPishFaktor.DoubleClick
        frmFO_ElamMarjoee_PishFaktor.txtShomarehPishFaktor.Tag = Val(GridEXPishFaktor.CurrentRow.Cells("ccPishFaktorTitr").Text.Replace(",", ""))
        frmFO_ElamMarjoee_PishFaktor.txtShomarehPishFaktor.Text = Val(GridEXPishFaktor.CurrentRow.Cells("PishFaktorShomareh").Text.Replace(",", ""))
        frmFO_ElamMarjoee_PishFaktor.lblNameMoshtary.Text = GridEXPishFaktor.CurrentRow.Cells("CodeMoshtary").Text.Replace(",", "").ToString & " - " & GridEXPishFaktor.CurrentRow.Cells("NameMoshtary").Text.ToString
        frmFO_ElamMarjoee_PishFaktor.cmbAnbar.SelectedValue = Val(GridEXPishFaktor.CurrentRow.Cells("ccAnbar").Text.Replace(",", ""))
        frmFO_ElamMarjoee_PishFaktor.FlgValidPishFaktor = True

        Me.Close()
    End Sub
#End Region
End Class