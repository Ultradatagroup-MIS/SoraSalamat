Public Class frmFO_ControlAeenNameh
    Dim cmTitr As CurrencyManager
    Dim dsForm As New DataSet
    Dim dvForm, dvSearchKoli As New DataView
    Public strAeenNameh As String = ""
    Dim Counter As Integer = 0

    Dim ErrPro As New ErrorProvider
    Private Const OrgWidthSize As Integer = 898
    Private Const AddWidthSize As Integer = 150

    Dim FormMode As Integer = 0 '' 0 = None / 1 = Close Date / 2 = New Date

    Private Sub frmFO_ControlAeenNameh_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Width = OrgWidthSize

        reLoadForm()
    End Sub
    Private Sub reLoadForm()
        SplitCounterAeenNameh()
        cmTitr.Position = 0

        SetButton()

        ReadAeenNameh(dvForm(cmTitr.Position)("ccAeenNameh"), tbShowAeenNameh.SelectedIndex)
        SearchKoli()
    End Sub
    Private Sub SplitCounterAeenNameh()
        Dim cnSQL As New SqlConnection
        Dim cmSQL As New SqlCommand
        Dim daSQL As New SqlDataAdapter
        Dim strSQL As String = ""

        If dsForm.Tables.Contains("tblAeenNameh") Then
            dsForm.Tables.Remove("tblAeenNameh")
        End If

        Try
            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()

            strSQL = "Sales.spTakhfifJayezehTarkibi_ControlAeenNameh_SplitAeenNameh "

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            cmSQL.Parameters.AddWithValue("strAeenNameh", strAeenNameh)
            cmSQL.Parameters.AddWithValue("SplitChar", ",")

            daSQL = New SqlDataAdapter(cmSQL)
            daSQL.Fill(dsForm, "tblAeenNameh")

            dvForm = dsForm.Tables("tblAeenNameh").DefaultView

            cmSQL = Nothing : daSQL = Nothing
            cnSQL.Close()

            BoundCurrencyManagerTitr()

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> SplitCounterAeenNameh ")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> SplitCounterAeenNameh ")
        End Try
    End Sub
    Private Sub ReadAeenNameh(ByVal ccAeenNameh As Integer, ByVal Type As Integer)
        '' Type : 0 = Vahed // 1 = Koli

        Dim cnSQL As New SqlConnection
        Dim cmSQL As New SqlCommand
        Dim strSQL As String = ""
        Try
            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()

            strSQL = "Sales.spTakhfifJayezehTarkibi_ShowDetailsAeenNameh "

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            cmSQL.Parameters.AddWithValue("ccTakhfifJayezehTarkibi", ccAeenNameh)

            Using Reader = cmSQL.ExecuteReader
                While Reader.Read
                    If Type = 0 Then
                        lblNoeAeenNameh.Text = Reader.GetString(Reader.GetOrdinal("txtNoeAeenNameh")).ToString()
                        lblAzTarikh.Text = Reader.GetString(Reader.GetOrdinal("AzTarikh")).ToString()
                        lblTaTarikh.Text = Reader.GetString(Reader.GetOrdinal("TaTarikh")).ToString()
                        lblNoePardakht.Text = Reader.GetString(Reader.GetOrdinal("txtNoePardakht")).ToString()
                        lblCodeSystem.Text = Reader.GetInt64(Reader.GetOrdinal("ccTakhfifJayezehTarkibi")).ToString()
                        lblBaze.Text = Reader.GetString(Reader.GetOrdinal("txtBazeh")).ToString()
                        lblNoeEhdaei.Text = Reader.GetString(Reader.GetOrdinal("txtNoeTakhfifEhdaei")).ToString()
                        lblSharh.Text = Reader.GetString(Reader.GetOrdinal("txtSharhAeenNameh")).ToString()
                        lblNoeFieldMoshtaryan.Text = Reader.GetString(Reader.GetOrdinal("txtNoeFieldMoshtary")).ToString()
                        lblNoeFieldKala.Text = Reader.GetString(Reader.GetOrdinal("txtNoeFieldJayezeh")).ToString()
                    ElseIf Type = 1 Then
                        lblBaze_Koli.Text = Reader.GetString(Reader.GetOrdinal("txtBazeh")).ToString()
                        lblNoeEhdaei_Koli.Text = Reader.GetString(Reader.GetOrdinal("txtNoeTakhfifEhdaei")).ToString()
                        lblNoeFieldMoshtaryan_Koli.Text = Reader.GetString(Reader.GetOrdinal("txtNoeFieldMoshtary")).ToString()
                        lblNoeFieldKala_Koli.Text = Reader.GetString(Reader.GetOrdinal("txtNoeFieldJayezeh")).ToString()
                    End If

                End While
            End Using

            cmSQL = Nothing
            cnSQL.Close()

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> ReadAeenNameh ")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> ReadAeenNameh ")
        End Try
    End Sub
    Private Sub SetButton()
        If dvForm.Count = 1 Then
            btnPrv.Visible = False
            btnNext.Visible = False
        Else
            btnPrv.Visible = True
            btnNext.Visible = True
            btnPrv.Enabled = False
        End If
    End Sub
    Private Sub BoundCurrencyManagerTitr()
        Try
            cmTitr = CType(BindingContext(dsForm.Tables("tblAeenNameh")), CurrencyManager)
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
            'Dim dv As DataRowView = dvForm(cmTitr.Position)
            ReadAeenNameh(dvForm(cmTitr.Position)("ccAeenNameh"), tbShowAeenNameh.SelectedIndex)
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->cmTitr_PositionChanged")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->cmTitr_PositionChanged")
        End Try

    End Sub
    Private Sub btnNext_Click(sender As Object, e As EventArgs) Handles btnNext.Click
        cmTitr.Position += 1
        Counter += 1

        btnPrv.Enabled = True
        If Counter = dvForm.Count - 1 Then
            btnNext.Enabled = False
        End If
    End Sub
    Private Sub btnPrv_Click(sender As Object, e As EventArgs) Handles btnPrv.Click
        cmTitr.Position -= 1
        Counter -= 1

        btnNext.Enabled = True
        If Counter = 0 Then
            btnPrv.Enabled = False
        End If
    End Sub
    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub btnCloseDate_Click(sender As Object, e As EventArgs) Handles btnCloseDate.Click
        If tbShowAeenNameh.SelectedIndex = 1 Then
            Dim CountField As Integer = 0

            For i As Integer = 0 To GridEXTitr.GetCheckedRows().Length - 1
                If GridEXTitr.GetCheckedRows(i).Cells("Taeed").Value Then
                    CountField += 1
                End If
            Next

            If CountField = 0 Then
                MsgBox("ابتدا باید یک آیین نامه انتخاب نمایید !", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, "")
                Exit Sub
            End If
        End If

        FormMode = 1
        SetForm(FormMode)
    End Sub
    Private Sub btnNewDate_Click(sender As Object, e As EventArgs) Handles btnNewDate.Click
        If tbShowAeenNameh.SelectedIndex = 1 Then
            Dim CountField As Integer = 0

            For i As Integer = 0 To GridEXTitr.GetCheckedRows().Length - 1
                If GridEXTitr.GetCheckedRows(i).Cells("Taeed").Value Then
                    CountField += 1
                End If
            Next

            If CountField = 0 Then
                MsgBox("ابتدا باید یک آیین نامه انتخاب نمایید !", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, "")
                Exit Sub
            End If
        End If

        FormMode = 2
        SetForm(FormMode)
    End Sub
    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        FormMode = 0
        SetForm(FormMode)
    End Sub
    Private Sub SetForm(ByVal Type As Integer)
        ''Type ---> 0 : None \\ 1 : Close Date \\ 2 : New Date \\ 
        If Type = 0 Then
            Me.Width = OrgWidthSize
            mskAzTarikh.Text = ""
            mskTaTarikh.Text = ""

            If Counter = dvForm.Count - 1 Then
                btnNext.Enabled = False
            Else
                btnNext.Enabled = True
            End If

            If Counter = 0 Then
                btnPrv.Enabled = False
            Else
                btnPrv.Enabled = True
            End If

            btnCloseDate.Enabled = True
            btnDelete.Enabled = True
            btnNewDate.Enabled = True

            If tbShowAeenNameh.SelectedIndex = 1 Then
                GridEXTitr.Enabled = True
            End If

        ElseIf Type = 1 Then
            Me.Width += AddWidthSize
            lblTaTarikhEdit.Text = "تاریخ :"
            mskTaTarikh.Text = ""
            mskAzTarikh.Visible = False
            lblAzTarikhEdit.Visible = False

            btnNext.Enabled = False
            btnPrv.Enabled = False
            btnCloseDate.Enabled = False
            btnDelete.Enabled = False
            btnNewDate.Enabled = False

            lblTozih.Text = " جهت بستـن بازه تاریخی آییـن نامه انتخاب شده، تاریخ مورد نظر را وارد نموده و دکمه ذخیــره را فشـار دهیـد ."

            If tbShowAeenNameh.SelectedIndex = 1 Then
                GridEXTitr.Enabled = False
            End If

            mskTaTarikh.Focus()
        ElseIf Type = 2 Then
            Me.Width += AddWidthSize
            lblTaTarikhEdit.Text = "تا تاریخ :"
            mskTaTarikh.Text = ""
            mskAzTarikh.Visible = True
            lblAzTarikhEdit.Visible = True

            btnNext.Enabled = False
            btnPrv.Enabled = False
            btnCloseDate.Enabled = False
            btnDelete.Enabled = False
            btnNewDate.Enabled = False

            lblTozih.Text = " جهت تعریف مجدد آییـن نامه انتخاب شده برای بازه زمانی جدید، تاریخ های مورد نظر خود را وارد نموده و دکمه ذخیره را فشـار دهیـد ."

            If tbShowAeenNameh.SelectedIndex = 1 Then
                GridEXTitr.Enabled = False
            End If

            mskAzTarikh.Focus()
        End If
    End Sub
    Private Sub SearchKoli()
        Dim cnSQL As New SqlConnection
        Dim cmSQL As New SqlCommand
        Dim daSQL As New SqlDataAdapter
        Dim strSQL As String = ""

        If dsForm.Tables.Contains("tbl_Search") Then
            dsForm.Tables.Remove("tbl_Search")
        End If

        Try
            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()

            strSQL = "Sales.spTakhfifJayezehTarkibi_ControlAeenNameh_SearchKoli "

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            cmSQL.Parameters.AddWithValue("strAeenNameh", strAeenNameh)

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

            dvSearchKoli = New DataView
            dvSearchKoli = New DataView(dsForm.Tables("tbl_Search"), "", "AzTarikh ASC", DataViewRowState.CurrentRows)
            dvSearchKoli.Sort = "AzTarikh ASC"
            dvSearchKoli.AllowDelete = False
            dvSearchKoli.AllowEdit = False
            dvSearchKoli.AllowNew = False

            cmSQL = Nothing : daSQL = Nothing

            GridEXTitr.DataSource = Nothing
            GridEXTitr.DataSource = dvSearchKoli

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
            If dvSearchKoli.Count = 0 Then
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

            GridEXTitr.CurrentTable.Columns.Item("ccTakhfifJayezehTarkibi").Caption = "کد سیستمی"
            GridEXTitr.CurrentTable.Columns.Item("ccTakhfifJayezehTarkibi").Visible = True
            GridEXTitr.CurrentTable.Columns.Item("ccTakhfifJayezehTarkibi").Width = 95
            GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXTitr.CurrentTable.Columns.Item("ccTakhfifJayezehTarkibi").Position = 1
            GridEXTitr.CurrentTable.Columns.Item("ccTakhfifJayezehTarkibi").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXTitr.CurrentTable.Columns.Item("AzTarikh").Caption = "از تاریخ"
            GridEXTitr.CurrentTable.Columns.Item("AzTarikh").Visible = True
            GridEXTitr.CurrentTable.Columns.Item("AzTarikh").Width = 80
            GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXTitr.CurrentTable.Columns.Item("AzTarikh").Position = 2
            GridEXTitr.CurrentTable.Columns.Item("AzTarikh").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXTitr.CurrentTable.Columns.Item("TaTarikh").Caption = "تا تاریخ"
            GridEXTitr.CurrentTable.Columns.Item("TaTarikh").Visible = True
            GridEXTitr.CurrentTable.Columns.Item("TaTarikh").Width = 80
            GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXTitr.CurrentTable.Columns.Item("TaTarikh").Position = 3
            GridEXTitr.CurrentTable.Columns.Item("TaTarikh").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXTitr.CurrentTable.Columns.Item("txtNoeAeenNameh").Caption = "نوع آییـن نامه"
            GridEXTitr.CurrentTable.Columns.Item("txtNoeAeenNameh").Visible = True
            GridEXTitr.CurrentTable.Columns.Item("txtNoeAeenNameh").Width = 100
            GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXTitr.CurrentTable.Columns.Item("txtNoeAeenNameh").Position = 4
            GridEXTitr.CurrentTable.Columns.Item("txtNoeAeenNameh").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXTitr.CurrentTable.Columns.Item("txtNoePardakht").Caption = "نوع پرداخت"
            GridEXTitr.CurrentTable.Columns.Item("txtNoePardakht").Visible = True
            GridEXTitr.CurrentTable.Columns.Item("txtNoePardakht").Width = 100
            GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXTitr.CurrentTable.Columns.Item("txtNoePardakht").Position = 5
            GridEXTitr.CurrentTable.Columns.Item("txtNoePardakht").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXTitr.CurrentTable.Columns.Item("txtNoeFieldMoshtary").Caption = "نوع فیلـد مشتـری"
            GridEXTitr.CurrentTable.Columns.Item("txtNoeFieldMoshtary").Visible = True
            GridEXTitr.CurrentTable.Columns.Item("txtNoeFieldMoshtary").Width = 150
            GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXTitr.CurrentTable.Columns.Item("txtNoeFieldMoshtary").Position = 6
            GridEXTitr.CurrentTable.Columns.Item("txtNoeFieldMoshtary").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXTitr.CurrentTable.Columns.Item("txtNoeFieldJayezeh").Caption = "تعلق می گیرد بـه"
            GridEXTitr.CurrentTable.Columns.Item("txtNoeFieldJayezeh").Visible = True
            GridEXTitr.CurrentTable.Columns.Item("txtNoeFieldJayezeh").Width = 150
            GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXTitr.CurrentTable.Columns.Item("txtNoeFieldJayezeh").Position = 7
            GridEXTitr.CurrentTable.Columns.Item("txtNoeFieldJayezeh").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXTitr.CurrentTable.Columns.Item("Sharh").Caption = "شــــرح"
            GridEXTitr.CurrentTable.Columns.Item("Sharh").Visible = True
            GridEXTitr.CurrentTable.Columns.Item("Sharh").Width = 300
            GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXTitr.CurrentTable.Columns.Item("Sharh").Position = 8
            GridEXTitr.CurrentTable.Columns.Item("Sharh").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center


            GridEXTitr.CurrentTable.Columns.Item("txtRasmi").Caption = "رسمی/ غیر رسمی"
            GridEXTitr.CurrentTable.Columns.Item("txtRasmi").Visible = True
            GridEXTitr.CurrentTable.Columns.Item("txtRasmi").Width = 100
            GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXTitr.CurrentTable.Columns.Item("txtRasmi").Position = 9
            GridEXTitr.CurrentTable.Columns.Item("txtRasmi").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center


            GridEXTitr.CurrentTable.Columns.Item("NameMahal").Caption = "نام محل"
            GridEXTitr.CurrentTable.Columns.Item("NameMahal").Visible = True
            GridEXTitr.CurrentTable.Columns.Item("NameMahal").Width = 100
            GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXTitr.CurrentTable.Columns.Item("NameMahal").Position = 10
            GridEXTitr.CurrentTable.Columns.Item("NameMahal").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

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

    Private Sub tbShowAeenNameh_SelectedIndexChanged(sender As Object, e As EventArgs) Handles tbShowAeenNameh.SelectedIndexChanged
        If tbShowAeenNameh.SelectedIndex = 0 Then
            btnPrv.Visible = True
            btnNext.Visible = True
        ElseIf tbShowAeenNameh.SelectedIndex = 1 Then
            btnPrv.Visible = False
            btnNext.Visible = False

            If tbShowAeenNameh.SelectedIndex = 1 Then
                GridEXTitr.Enabled = True
            End If

            SearchKoli()
        End If

        FormMode = 0
        SetForm(FormMode)
    End Sub
    Private Sub GridEXTitr_Click(sender As Object, e As EventArgs) Handles GridEXTitr.Click
        ReadAeenNameh(Val(GridEXTitr.CurrentRow.Cells("ccTakhfifJayezehTarkibi").Text.Replace(",", "")), tbShowAeenNameh.SelectedIndex)
    End Sub
    Private Function ControlAeenNameh(ByVal Type As Integer, ByVal ccTakhfifJayezehTarkibi As Integer) As Boolean
        ControlAeenNameh = False
        '' Type : 0 = Delete \ 1 = Close Date \ 2 = New Date

        Dim cnSQL As New SqlConnection
        Dim cmSQL As New SqlCommand
        Dim strSQL As String = ""

        Try
            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()

            strSQL = "Sales.spTakhfifJayezehTarkibi_Control"

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            cmSQL.Parameters.AddWithValue("ccTakhfifJayezehTarkibi", ccTakhfifJayezehTarkibi)
            cmSQL.Parameters.AddWithValue("Type", Type)
            cmSQL.Parameters.AddWithValue("AzTarikh", mskAzTarikh.Text)
            cmSQL.Parameters.AddWithValue("TaTarikh", mskTaTarikh.Text)
            cmSQL.Parameters.AddWithValue("PkResult", 0)
            cmSQL.Parameters("PkResult").Direction = ParameterDirection.Output

            cmSQL.ExecuteNonQuery()

            If Type = 2 Then
                strAeenNameh &= cmSQL.Parameters("PkResult").Value.ToString.Trim + ","
            End If

            cmSQL = Nothing
            cnSQL.Close()

            Return True
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> ControlAeenNameh ")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> ControlAeenNameh ")
        End Try
    End Function
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click

        If Me.mskTaTarikh.Text < TarikhEmrooz Then
            ErrPro.SetError(Me.mskTaTarikh, "از تاریخ نمی تواند پیش از تاریخ امروز باشد.")
            MsgBox("از تاریخ نمی تواند پیش از تاریخ امروز باشد.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
            mskAzTarikh.Focus()
            Exit Sub
        End If
        If tbShowAeenNameh.SelectedIndex = 0 Then
            ControlAeenNameh(FormMode, dvForm(cmTitr.Position)("ccAeenNameh"))
            ReadAeenNameh(dvForm(cmTitr.Position)("ccAeenNameh"), tbShowAeenNameh.SelectedIndex)

        ElseIf tbShowAeenNameh.SelectedIndex = 1 Then
            For i As Integer = 0 To GridEXTitr.GetCheckedRows().Length - 1
                If GridEXTitr.GetCheckedRows(i).Cells("Taeed").Value Then
                    ControlAeenNameh(FormMode, Val(GridEXTitr.GetCheckedRows(i).Cells("ccTakhfifJayezehTarkibi").Text.Replace(",", "")))
                End If
            Next
        End If

        If FormMode = 2 Then
            reLoadForm()
        Else
            SearchKoli()
        End If

        FormMode = 0
        SetForm(FormMode)
    End Sub
    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        If tbShowAeenNameh.SelectedIndex = 0 Then

            ControlAeenNameh(0, Val(dvForm(cmTitr.Position)("ccAeenNameh").ToString.Trim))

            strAeenNameh = strAeenNameh.Replace(dvForm(cmTitr.Position)("ccAeenNameh").ToString.Trim & ",", "")

            If strAeenNameh = "," Then
                Me.Close()
                Exit Sub
            End If
        ElseIf tbShowAeenNameh.SelectedIndex = 1 Then
            Dim CountField As Integer = 0

            For i As Integer = 0 To GridEXTitr.GetCheckedRows().Length - 1
                If GridEXTitr.GetCheckedRows(i).Cells("Taeed").Value Then
                    CountField += 1
                End If
            Next

            If CountField = 0 Then
                MsgBox("ابتدا باید یک آیین نامه انتخاب نمایید !", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, "")
                Exit Sub
            End If

            For i As Integer = 0 To GridEXTitr.GetCheckedRows().Length - 1
                If GridEXTitr.GetCheckedRows(i).Cells("Taeed").Value Then
                    ControlAeenNameh(0, Val(GridEXTitr.GetCheckedRows(i).Cells("ccTakhfifJayezehTarkibi").Text.Replace(",", "")))
                    strAeenNameh = strAeenNameh.Replace(Val(GridEXTitr.GetCheckedRows(i).Cells("ccTakhfifJayezehTarkibi").Text.Replace(",", "")) & ",", "")
                End If
            Next
        End If

        If strAeenNameh = "," Then
            Me.Close()
            Exit Sub
        End If

        tbShowAeenNameh.SelectedIndex = 0

        reLoadForm()

    End Sub
    Private Function IsValid(ByVal Type As Integer, ByVal AzTarikh As String, ByVal TaTarikh As String) As Boolean
        IsValid = False
        '' Type : 0 = Delete \ 1 = Close Date \ 2 = New Date
        Try
            If Type = 0 Then

            ElseIf Type = 1 Then
                If objTarikh.IsShDate(mskTaTarikh.Text) = False Then
                    Exit Function
                End If

                If mskTaTarikh.Text > TaTarikh Then
                    If objTools.DCount("ccFaktorTitr", "tblFO_Faktor", "FaktorTarikh => " & mskTaTarikh.Text & " AND CodeMahal = " & CodeMahalFaal) > 0 Then
                        '' Faktor Mojod Ast !
                        Exit Function
                    End If
                End If

                If mskTaTarikh.Text < TarikhEmrooz Then
                    If objTools.DCount("ccFaktorTitr", "tblFO_Faktor", "FaktorTarikh => " & mskTaTarikh.Text & " AND CodeMahal = " & CodeMahalFaal) > 0 Then
                        '' Faktor Mojod Ast !
                        Exit Function
                    End If
                End If
            ElseIf Type = 2 Then
                If objTarikh.IsShDate(mskTaTarikh.Text) = False Then
                    Exit Function
                End If
            End If

            Return True
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> ControlAeenNameh ")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> ControlAeenNameh ")
        End Try
    End Function
End Class