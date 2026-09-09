Public Class frmFO_TakhfifJayezehTarkibi

#Region "Variable AND Constant Declration"
    Public dsForm As New DataSet
    Public dvForm As DataView
    Public dvTitr As DataView

    Dim ErrPro As New ErrorProvider
    Dim cmForm As CurrencyManager
    Private SN As Integer
    Const cntCodeSubSystem As Long = 1000129
    Dim txtCaption As String
    Private WithEvents BS As New UD_Dll.PassString

#End Region

    Private Sub frmFO_TakhfifJayezehTarkibi_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetParameter()
        SN = objSec.GetSecurityNumber(cntCodeSubSystem, UserName)
        If Not objSec.HaveUserPermission(SN, UD_Dll.Security.ePermissions.Open) Then
            Me.Close()
            Exit Sub
        End If

        SetParameter()
        LoadCombo()
        mskAzTarikh.Text = TarikhEmrooz
        mskTaTarikh.Text = TarikhEmrooz

        cmbNoe.SelectedIndex = 0
        Search()
    End Sub
    Private Sub SetParameter()

        Dim commands As String = Microsoft.VisualBasic.Command()
        If commands.Length = 0 Then

            UserName = "administrator"
            UserPassWord = "66998833"
            NameMahalFaal = "تهران"
            CodeMahalFaal = "1"
            PersonelCode = "0"
            PersonelName = "Administrator"
            CodeDoreh = "1396"
            txtCaption = "تخفیفات و جوایز ترکیبی"
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
        cmbNoe.Items.Add("همـــه")
        cmbNoe.Items.Add("تخفیـف")
        cmbNoe.Items.Add("جاــزه")
    End Sub
    Private Sub Search()
        Dim cnSQL As New SqlConnection
        Dim cmSQL As New SqlCommand
        Dim daSQL As New SqlDataAdapter
        Dim strSQL As String = ""

        If dsForm.Tables.Contains("tbl_Search") Then
            dsForm.Tables.Remove("tbl_Search")
        End If

        Try
            Dim CodeMahalAsly = objTools.ConvertNulls(objTools.DLookup("CodeMahal", "tblGL_MarkazPakhsh", " CodeMahal<>0 and Faal = 1 "), 0)
          
            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()

            strSQL = "Sales.spTakhfifJayezehTarkibi_Search "

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            If CodeMahalAsly <> CodeMahalFaal Then
                cmSQL.Parameters.AddWithValue("CodeMahal", CodeMahalFaal)
            Else
                cmSQL.Parameters.AddWithValue("CodeMahal", -5)
            End If


            cmSQL.Parameters.AddWithValue("AzTarikh", mskAzTarikh.Text.Trim)
            cmSQL.Parameters.AddWithValue("TaTarikh", mskTaTarikh.Text.Trim)
            cmSQL.Parameters.AddWithValue("NoeAeenNameh", cmbNoe.SelectedIndex)
            cmSQL.Parameters.AddWithValue("TarikhEmrooz", TarikhEmrooz)

            daSQL = New SqlDataAdapter(cmSQL)
            daSQL.Fill(dsForm, "tbl_Search")

            Dim col As DataColumn
            '------------Adding Columns------------
            col = New DataColumn
            col.ColumnName = "Taeed"
            col.DataType = GetType(Boolean)
            col.DefaultValue = False
            dsForm.Tables("tbl_Search").Columns.Add(col)
            '-----------------------------------------

            dvForm = New DataView
            dvForm = dsForm.Tables("tbl_Search").DefaultView
            dvForm.Sort = "AzTarikh ASC"
            dvForm.AllowDelete = False
            dvForm.AllowEdit = False
            dvForm.AllowNew = False

            cmSQL = Nothing : daSQL = Nothing

            dvTitr = New DataView(dsForm.Tables("tbl_Search"), "", "AzTarikh ASC", DataViewRowState.CurrentRows)
            dvTitr.AllowNew = False
            dvTitr.AllowDelete = False
            dvTitr.AllowEdit = False

            GridEXTitr.DataSource = Nothing
            GridEXTitr.DataSource = dvTitr

            SetGridStyle()

            cnSQL.Close()
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "Error IN DataBase ----> Search")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, "Error IN ----> Search")
        End Try
    End Sub
    Private Sub SetGridStyle()
        Try
            If dvTitr.Count = 0 Then
                Exit Sub
            End If

            With GridEXTitr
                .DataSource = Nothing
                .DataSource = dsForm.Tables("tbl_Search").DefaultView
                .SetDataBinding(dsForm.Tables("tbl_Search").DefaultView, "")
                .RetrieveStructure()
            End With

            For i As Integer = 0 To GridEXTitr.CurrentTable.Columns.Count - 1
                GridEXTitr.CurrentTable.Columns.Item(i).Visible = False
            Next

            GridEXTitr.CurrentTable.Columns.Item("Taeed").Caption = "انتخاب"
            GridEXTitr.CurrentTable.Columns.Item("Taeed").Visible = True
            GridEXTitr.CurrentTable.Columns.Item("Taeed").Width = 20
            GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXTitr.CurrentTable.Columns.Item("Taeed").Position = 0
            GridEXTitr.CurrentTable.Columns.Item("Taeed").Selectable = True
            GridEXTitr.CurrentTable.Columns.Item("Taeed").ActAsSelector = True
            GridEXTitr.CurrentTable.Columns.Item("Taeed").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXTitr.CurrentTable.Columns.Item("AzTarikh").Caption = "از تاریخ"
            GridEXTitr.CurrentTable.Columns.Item("AzTarikh").Visible = True
            GridEXTitr.CurrentTable.Columns.Item("AzTarikh").Width = 80
            GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXTitr.CurrentTable.Columns.Item("AzTarikh").Position = 1
            GridEXTitr.CurrentTable.Columns.Item("AzTarikh").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXTitr.CurrentTable.Columns.Item("TaTarikh").Caption = "تا تاریخ"
            GridEXTitr.CurrentTable.Columns.Item("TaTarikh").Visible = True
            GridEXTitr.CurrentTable.Columns.Item("TaTarikh").Width = 80
            GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXTitr.CurrentTable.Columns.Item("TaTarikh").Position = 2
            GridEXTitr.CurrentTable.Columns.Item("TaTarikh").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXTitr.CurrentTable.Columns.Item("txtNoeAeenNameh").Caption = "نوع آییـن نامه"
            GridEXTitr.CurrentTable.Columns.Item("txtNoeAeenNameh").Visible = True
            GridEXTitr.CurrentTable.Columns.Item("txtNoeAeenNameh").Width = 100
            GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXTitr.CurrentTable.Columns.Item("txtNoeAeenNameh").Position = 3
            GridEXTitr.CurrentTable.Columns.Item("txtNoeAeenNameh").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXTitr.CurrentTable.Columns.Item("txtNoePardakht").Caption = "نوع پرداخت"
            GridEXTitr.CurrentTable.Columns.Item("txtNoePardakht").Visible = True
            GridEXTitr.CurrentTable.Columns.Item("txtNoePardakht").Width = 100
            GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXTitr.CurrentTable.Columns.Item("txtNoePardakht").Position = 4
            GridEXTitr.CurrentTable.Columns.Item("txtNoePardakht").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXTitr.CurrentTable.Columns.Item("txtNoeFieldMoshtary").Caption = "نوع فیلـد مشتـری"
            GridEXTitr.CurrentTable.Columns.Item("txtNoeFieldMoshtary").Visible = True
            GridEXTitr.CurrentTable.Columns.Item("txtNoeFieldMoshtary").Width = 150
            GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXTitr.CurrentTable.Columns.Item("txtNoeFieldMoshtary").Position = 5
            GridEXTitr.CurrentTable.Columns.Item("txtNoeFieldMoshtary").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXTitr.CurrentTable.Columns.Item("txtNoeFieldJayezeh").Caption = "تعلق می گیرد بـه"
            GridEXTitr.CurrentTable.Columns.Item("txtNoeFieldJayezeh").Visible = True
            GridEXTitr.CurrentTable.Columns.Item("txtNoeFieldJayezeh").Width = 150
            GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXTitr.CurrentTable.Columns.Item("txtNoeFieldJayezeh").Position = 6
            GridEXTitr.CurrentTable.Columns.Item("txtNoeFieldJayezeh").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXTitr.CurrentTable.Columns.Item("Sharh").Caption = "شــــرح"
            GridEXTitr.CurrentTable.Columns.Item("Sharh").Visible = True
            GridEXTitr.CurrentTable.Columns.Item("Sharh").Width = 300
            GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXTitr.CurrentTable.Columns.Item("Sharh").Position = 7
            GridEXTitr.CurrentTable.Columns.Item("Sharh").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXTitr.CurrentTable.Columns.Item("ccTakhfifJayezehTarkibi").Caption = "ccTakhfifJayezehTarkibi"
            GridEXTitr.CurrentTable.Columns.Item("ccTakhfifJayezehTarkibi").Visible = False
            GridEXTitr.CurrentTable.Columns.Item("ccTakhfifJayezehTarkibi").Width = 0
            GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXTitr.CurrentTable.Columns.Item("ccTakhfifJayezehTarkibi").Position = 8
            GridEXTitr.CurrentTable.Columns.Item("ccTakhfifJayezehTarkibi").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXTitr.CurrentTable.Columns.Item("VazeiatTarikh").Caption = "VazeiatTarikh"
            GridEXTitr.CurrentTable.Columns.Item("VazeiatTarikh").Visible = False
            GridEXTitr.CurrentTable.Columns.Item("VazeiatTarikh").Width = 0
            GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXTitr.CurrentTable.Columns.Item("VazeiatTarikh").Position = 9
            GridEXTitr.CurrentTable.Columns.Item("VazeiatTarikh").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center


            GridEXTitr.CurrentTable.Columns.Item("txtRasmi").Caption = "رسمی/ غیر رسمی"
            GridEXTitr.CurrentTable.Columns.Item("txtRasmi").Visible = True
            GridEXTitr.CurrentTable.Columns.Item("txtRasmi").Width = 150
            GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXTitr.CurrentTable.Columns.Item("txtRasmi").Position = 10
            GridEXTitr.CurrentTable.Columns.Item("txtRasmi").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center


            GridEXTitr.CurrentTable.Columns.Item("NameMahal").Caption = "نام محل"
            GridEXTitr.CurrentTable.Columns.Item("NameMahal").Visible = True
            GridEXTitr.CurrentTable.Columns.Item("NameMahal").Width = 100
            GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXTitr.CurrentTable.Columns.Item("NameMahal").Position = 11
            GridEXTitr.CurrentTable.Columns.Item("NameMahal").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center


            For i As Integer = 0 To GridEXTitr.RowCount - 1
                Dim rowcol As New Janus.Windows.GridEX.GridEXFormatStyle()
                Dim a As String = GridEXTitr.GetRow(i).Cells("VazeiatTarikh").Text

                If Val(GridEXTitr.GetRow(i).Cells("VazeiatTarikh").Text.Replace(",", "")) = 0 Then
                    rowcol.BackColor = Color.MediumAquamarine
                    GridEXTitr.GetRow(i).RowStyle = rowcol
                ElseIf Val(GridEXTitr.GetRow(i).Cells("VazeiatTarikh").Text.Replace(",", "")) = 1 Then
                    rowcol.BackColor = Color.Yellow
                    GridEXTitr.GetRow(i).RowStyle = rowcol
                ElseIf Val(GridEXTitr.GetRow(i).Cells("VazeiatTarikh").Text.Replace(",", "")) = 2 Then
                    rowcol.BackColor = Color.White
                    GridEXTitr.GetRow(i).RowStyle = rowcol
                End If
            Next

            For i As Integer = 0 To GridEXTitr.RootTable.Columns.Count - 1
                If GridEXTitr.RootTable.Columns(i).Type.IsValueType Then
                    GridEXTitr.RootTable.Columns(i).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                    GridEXTitr.RootTable.Columns(i).FormatMode = Janus.Windows.GridEX.FormatMode.UseIFormattable
                    GridEXTitr.RootTable.Columns(i).FormatString = "###,###.##"
                    GridEXTitr.RootTable.Columns(i).TotalFormatMode = Janus.Windows.GridEX.FormatMode.UseIFormattable
                    GridEXTitr.RootTable.Columns(i).TotalFormatString = "###,###.##"
                End If
            Next

            GridEXTitr.Visible = True
            GridEXTitr.HeaderFormatStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center


        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> SetGridStyle ")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> SetGridStyle ")
        End Try

    End Sub
    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        Search()
    End Sub
    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        Dim frm As New frmFO_AddNewTakhfifJayezeh

        Me.Hide()
        frm.ShowDialog()
        Me.Show()

        Search()

    End Sub
    Private Sub btnControlAeenNameh_Click(sender As Object, e As EventArgs) Handles btnControlAeenNameh.Click
        Dim Counter As Integer = 0
        Dim str As String = ""

        For i As Integer = 0 To GridEXTitr.GetCheckedRows().Length - 1
            If GridEXTitr.GetCheckedRows(i).Cells("Taeed").Value Then
                str &= GridEXTitr.GetCheckedRows(i).Cells("ccTakhfifJayezehTarkibi").Text.Replace(",", "") & ","
                Counter += 1
            End If
        Next

        If Counter = 0 Then
            MsgBox("ابتدا باید یک آیین نامه انتخاب نمایید !", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, "")
            Exit Sub
        Else
            Dim frm As New frmFO_ControlAeenNameh

            Me.Hide()

            frm.strAeenNameh = "," & str
            frm.ShowDialog()

            Me.Show()

            Search()
        End If
    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub btnToday_Click(sender As Object, e As EventArgs) Handles btnToday.Click
        Dim cnSQL As New SqlConnection
        Dim cmSQL As New SqlCommand
        Dim daSQL As New SqlDataAdapter
        Dim strSQL As String = ""

        If dsForm.Tables.Contains("tbl_Search") Then
            dsForm.Tables.Remove("tbl_Search")
        End If

        Try


            Dim CodeMahalAsly = objTools.ConvertNulls(objTools.DLookup("CodeMahal", "tblGL_MarkazPakhsh", " CodeMahal<>0 and Faal = 1 "), 0)

            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()

            strSQL = "Sales.spTakhfifJayezehTarkibi_Search_btnToday"

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            If CodeMahalAsly <> CodeMahalFaal Then
                cmSQL.Parameters.AddWithValue("CodeMahal", CodeMahalFaal)
            Else
                cmSQL.Parameters.AddWithValue("CodeMahal", -5)
            End If

            cmSQL.Parameters.AddWithValue("AzTarikh", mskAzTarikh.Text.Trim)
            cmSQL.Parameters.AddWithValue("TaTarikh", mskTaTarikh.Text.Trim)
            cmSQL.Parameters.AddWithValue("NoeAeenNameh", cmbNoe.SelectedIndex)
            cmSQL.Parameters.AddWithValue("TarikhEmrooz", TarikhEmrooz)

            daSQL = New SqlDataAdapter(cmSQL)
            daSQL.Fill(dsForm, "tbl_Search")

            Dim col As DataColumn
            '------------Adding Columns------------
            col = New DataColumn
            col.ColumnName = "Taeed"
            col.DataType = GetType(Boolean)
            col.DefaultValue = False
            dsForm.Tables("tbl_Search").Columns.Add(col)
            '-----------------------------------------

            dvForm = New DataView
            dvForm = dsForm.Tables("tbl_Search").DefaultView
            dvForm.Sort = "AzTarikh ASC"
            dvForm.AllowDelete = False
            dvForm.AllowEdit = False
            dvForm.AllowNew = False

            cmSQL = Nothing : daSQL = Nothing

            dvTitr = New DataView(dsForm.Tables("tbl_Search"), "", "AzTarikh ASC", DataViewRowState.CurrentRows)
            dvTitr.AllowNew = False
            dvTitr.AllowDelete = False
            dvTitr.AllowEdit = False

            GridEXTitr.DataSource = Nothing
            GridEXTitr.DataSource = dvTitr

            SetGridStyle()

            cnSQL.Close()
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "Error IN DataBase ----> Search")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, "Error IN ----> Search")
        End Try
    End Sub
End Class
