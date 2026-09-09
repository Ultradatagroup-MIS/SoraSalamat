Public Class frmFO_InsertAllKala
#Region "Variable AND Constant Declration"
    'Const cntCodeSubSystem As Long = 625

    Dim cmTitr As CurrencyManager
    Dim ErrPro As New ErrorProvider
    Dim dsForm As New DataSet
    Dim dvForm As DataView
    Private SN As Integer
    Dim txtCaption As String
    Private WithEvents BS As New UD_Dll.PassString

    Public frm_ccPishFaktor As Integer
    Public frm_PishFaktorTarikh As String = ""
    Public frm_ccMoshtary As Integer = 0
    Dim strKala As String = ""
    Dim strKala_NotHaveFee As String = ""
    Dim AllowChangeFeePishFaktor As Boolean = False
#End Region
    Private Sub frmFO_InsertAllKala_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            SetParameter()
            If objTools.ConvertNulls(objTools.DLookup("FormPishFaktor_InsertAllKala_ShowJayezeh", "tblGL_SysConfig", "CodeMahal = " & CodeMahalFaal), False) = True Then
                chkShowWithJayezeh.Checked = True
            Else
                chkShowWithJayezeh.Checked = False
            End If

            AllowChangeFeePishFaktor = objTools.ConvertNulls(objTools.DLookup("AllowChangeFeePishFaktor", "tblGL_SysConfig", "CodeMahal = " & CodeMahalFaal), 0)

            Search(False)
            objTools.ChangeKbd(UD_Dll.mdlUtility.KBD.Farsi)

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> frmFO_InsertAllKala_Load ")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> frmFO_InsertAllKala_Load ")
        End Try
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

            strSQL = "Sales.spPishFaktor_frmInsertAllKala_Search "

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

            cmSQL.Parameters.AddWithValue("Tarikh", frm_PishFaktorTarikh)
            cmSQL.Parameters.AddWithValue("ccMarkazPakhsh", CodeMahalFaal)
            cmSQL.Parameters.AddWithValue("ccMoshtary", frm_ccMoshtary)
            cmSQL.Parameters.AddWithValue("ShowType", chkShowWithJayezeh.CheckState)
            cmSQL.Parameters.AddWithValue("ccPishFaktor", frm_ccPishFaktor)
            cmSQL.Parameters.AddWithValue("CodeDoreh", CodeDoreh)
            cmSQL.Parameters.AddWithValue("TarikhEmrooz", TarikhEmrooz)

            daSQL = New SqlDataAdapter(cmSQL)
            daSQL.Fill(dsForm, "HETitr")
            dvForm = New DataView
            dvForm = dsForm.Tables("HETitr").DefaultView
            dvForm.AllowDelete = True
            dvForm.AllowEdit = True
            dvForm.AllowNew = True
            daSQL = Nothing

            dvForm = New DataView(dsForm.Tables("HETitr"), "", "CodeKala ASC, DarsadTakhfif ASC", DataViewRowState.CurrentRows)
            dvForm.AllowNew = True
            dvForm.AllowDelete = True
            dvForm.AllowEdit = True

            cmSQL.Connection.Close()
            cnSQL.Close()
            daSQL = Nothing

            GridEXInsertKala.DataSource = Nothing
            GridEXInsertKala.DataSource = dvForm

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
            With GridEXInsertKala
                .DataSource = Nothing
                .DataSource = dsForm.Tables("HETitr").DefaultView
                .SetDataBinding(dsForm.Tables("HETitr").DefaultView, "")
                .RetrieveStructure()
            End With

            For i As Integer = 0 To GridEXInsertKala.CurrentTable.Columns.Count - 1
                GridEXInsertKala.CurrentTable.Columns.Item(i).Visible = False
            Next

            GridEXInsertKala.CurrentTable.Columns.Item("Radif").Caption = "ردیف"
            GridEXInsertKala.CurrentTable.Columns.Item("Radif").Visible = True
            GridEXInsertKala.CurrentTable.Columns.Item("Radif").Width = 60
            GridEXInsertKala.CurrentTable.Columns.Item("Radif").EditType = Janus.Windows.GridEX.EditType.NoEdit
            GridEXInsertKala.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXInsertKala.CurrentTable.Columns.Item("Radif").Position = 0
            GridEXInsertKala.CurrentTable.Columns.Item("Radif").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXInsertKala.CurrentTable.Columns.Item("CodeKala").Caption = "کد کالا"
            GridEXInsertKala.CurrentTable.Columns.Item("CodeKala").Visible = True
            GridEXInsertKala.CurrentTable.Columns.Item("CodeKala").Width = 100
            'GridEXInsertKala.CurrentTable.Columns.Item("CodeKala").EditType = Janus.Windows.GridEX.EditType.NoEdit
            GridEXInsertKala.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXInsertKala.CurrentTable.Columns.Item("CodeKala").Position = 1
            GridEXInsertKala.CurrentTable.Columns.Item("CodeKala").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXInsertKala.CurrentTable.Columns.Item("NameKala").Caption = "نام کالا"
            GridEXInsertKala.CurrentTable.Columns.Item("NameKala").Visible = True
            GridEXInsertKala.CurrentTable.Columns.Item("NameKala").Width = 480
            'GridEXInsertKala.CurrentTable.Columns.Item("NameKala").EditType = Janus.Windows.GridEX.EditType.NoEdit
            GridEXInsertKala.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXInsertKala.CurrentTable.Columns.Item("NameKala").Position = 2
            GridEXInsertKala.CurrentTable.Columns.Item("NameKala").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXInsertKala.CurrentTable.Columns.Item("Tedad").Caption = "تعداد درخواستی"
            GridEXInsertKala.CurrentTable.Columns.Item("Tedad").Visible = True
            GridEXInsertKala.CurrentTable.Columns.Item("Tedad").Width = 110
            GridEXInsertKala.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOn
            GridEXInsertKala.CurrentTable.Columns.Item("Tedad").Position = 3
            GridEXInsertKala.CurrentTable.Columns.Item("Tedad").FormatString = "G"
            GridEXInsertKala.CurrentTable.Columns.Item("Tedad").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXInsertKala.CurrentTable.Columns.Item("Fee").Caption = "فی"
            GridEXInsertKala.CurrentTable.Columns.Item("Fee").Visible = True
            GridEXInsertKala.CurrentTable.Columns.Item("Fee").Width = 80
            GridEXInsertKala.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            If AllowChangeFeePishFaktor = False Then
                GridEXInsertKala.CurrentTable.Columns.Item("Fee").EditType = Janus.Windows.GridEX.EditType.NoEdit
            End If
            GridEXInsertKala.CurrentTable.Columns.Item("Fee").Position = 4
            GridEXInsertKala.CurrentTable.Columns.Item("Fee").FormatString = "N"
            GridEXInsertKala.CurrentTable.Columns.Item("Fee").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXInsertKala.CurrentTable.Columns.Item("DarsadTakhfif").Caption = "درصد تخفیف"
            GridEXInsertKala.CurrentTable.Columns.Item("DarsadTakhfif").Visible = True
            GridEXInsertKala.CurrentTable.Columns.Item("DarsadTakhfif").Width = 100
            GridEXInsertKala.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOn
            GridEXInsertKala.CurrentTable.Columns.Item("DarsadTakhfif").Position = 5
            GridEXInsertKala.CurrentTable.Columns.Item("DarsadTakhfif").FormatString = "N"
            GridEXInsertKala.CurrentTable.Columns.Item("DarsadTakhfif").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXInsertKala.CurrentTable.Columns.Item("JamKol").Caption = "جمع کل"
            GridEXInsertKala.CurrentTable.Columns.Item("JamKol").Visible = True
            GridEXInsertKala.CurrentTable.Columns.Item("JamKol").Width = 100
            GridEXInsertKala.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXInsertKala.CurrentTable.Columns.Item("JamKol").EditType = Janus.Windows.GridEX.EditType.NoEdit
            GridEXInsertKala.CurrentTable.Columns.Item("JamKol").Position = 6
            GridEXInsertKala.CurrentTable.Columns.Item("JamKol").FormatString = "N"
            GridEXInsertKala.CurrentTable.Columns.Item("JamKol").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXInsertKala.CurrentTable.Columns.Item("MojodiDarHalForosh").Caption = "موجودی"
            GridEXInsertKala.CurrentTable.Columns.Item("MojodiDarHalForosh").Visible = True
            GridEXInsertKala.CurrentTable.Columns.Item("MojodiDarHalForosh").Width = 70
            GridEXInsertKala.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXInsertKala.CurrentTable.Columns.Item("MojodiDarHalForosh").EditType = Janus.Windows.GridEX.EditType.NoEdit
            GridEXInsertKala.CurrentTable.Columns.Item("MojodiDarHalForosh").Position = 7
            GridEXInsertKala.CurrentTable.Columns.Item("MojodiDarHalForosh").FormatString = "N"
            GridEXInsertKala.CurrentTable.Columns.Item("MojodiDarHalForosh").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXInsertKala.CurrentTable.Columns.Item("txtJayezeh").Caption = "ماهیت"
            GridEXInsertKala.CurrentTable.Columns.Item("txtJayezeh").Visible = True
            GridEXInsertKala.CurrentTable.Columns.Item("txtJayezeh").Width = 70
            GridEXInsertKala.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXInsertKala.CurrentTable.Columns.Item("txtJayezeh").EditType = Janus.Windows.GridEX.EditType.NoEdit
            GridEXInsertKala.CurrentTable.Columns.Item("txtJayezeh").Position = 8
            GridEXInsertKala.CurrentTable.Columns.Item("txtJayezeh").FormatString = "N"
            GridEXInsertKala.CurrentTable.Columns.Item("txtJayezeh").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXInsertKala.CurrentTable.Columns.Item("ccKala").Caption = "ccKala"
            GridEXInsertKala.CurrentTable.Columns.Item("ccKala").Visible = False
            GridEXInsertKala.CurrentTable.Columns.Item("ccKala").Width = 0
            GridEXInsertKala.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXInsertKala.CurrentTable.Columns.Item("ccKala").Position = 9
            GridEXInsertKala.CurrentTable.Columns.Item("ccKala").FormatString = "G"
            GridEXInsertKala.CurrentTable.Columns.Item("ccKala").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXInsertKala.CurrentTable.Columns.Item("IsJayezeh").Caption = "IsJayezeh"
            GridEXInsertKala.CurrentTable.Columns.Item("IsJayezeh").Visible = False
            GridEXInsertKala.CurrentTable.Columns.Item("IsJayezeh").Width = 0
            GridEXInsertKala.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXInsertKala.CurrentTable.Columns.Item("IsJayezeh").EditType = Janus.Windows.GridEX.EditType.NoEdit
            GridEXInsertKala.CurrentTable.Columns.Item("IsJayezeh").Position = 10
            GridEXInsertKala.CurrentTable.Columns.Item("IsJayezeh").FormatString = "G"
            GridEXInsertKala.CurrentTable.Columns.Item("IsJayezeh").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            For i As Integer = 0 To GridEXInsertKala.RowCount - 1
                Dim rowcol As New Janus.Windows.GridEX.GridEXFormatStyle()

                If Val(GridEXInsertKala.GetRow(i).Cells("IsJayezeh").Text) = 0 Then
                    rowcol.BackColor = Color.White
                    GridEXInsertKala.GetRow(i).RowStyle = rowcol
                ElseIf Val(GridEXInsertKala.GetRow(i).Cells("IsJayezeh").Text) = 1 Then
                    rowcol.BackColor = Color.LightGreen
                    GridEXInsertKala.GetRow(i).RowStyle = rowcol
                End If
            Next

            For i As Integer = 0 To GridEXInsertKala.RootTable.Columns.Count - 1
                If GridEXInsertKala.RootTable.Columns(i).Type.IsValueType Then
                    GridEXInsertKala.RootTable.Columns(i).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                    GridEXInsertKala.RootTable.Columns(i).FormatMode = Janus.Windows.GridEX.FormatMode.UseIFormattable
                    GridEXInsertKala.RootTable.Columns(i).FormatString = "G"
                    GridEXInsertKala.RootTable.Columns(i).TotalFormatMode = Janus.Windows.GridEX.FormatMode.UseIFormattable
                    GridEXInsertKala.RootTable.Columns(i).TotalFormatString = "G"
                End If
            Next

            GridEXInsertKala.Visible = True
            GridEXInsertKala.HeaderFormatStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> SetGridStyle ")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> SetGridStyle ")
        End Try

    End Sub
    Private Sub BoundCurrencyManager(ByVal cm As SqlCommand)
        Try
            cmTitr = CType(BindingContext(GridEXInsertKala.DataSource), CurrencyManager)
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

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub btnEnter_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEnter.Click

        For i As Integer = 0 To GridEXInsertKala.RowCount - 1
            If GridEXInsertKala.GetRow(i).Cells("Tedad").Text.Trim <> 0 Then
                InsertKala(Val(GridEXInsertKala.GetRow(i).Cells("ccKala").Text.Trim), GridEXInsertKala.GetRow(i).Cells("CodeKala").Text.Trim, GridEXInsertKala.GetRow(i).Cells("Tedad").Text.Trim, Val(GridEXInsertKala.GetRow(i).Cells("DarsadTakhfif").Text.Trim), Val(GridEXInsertKala.GetRow(i).Cells("IsJayezeh").Text.Trim), Val(GridEXInsertKala.GetRow(i).Cells("Fee").Text.Trim.Replace(",", "")))
            End If
        Next

        If strKala_NotHaveFee <> "" Then
            strKala_NotHaveFee = strKala_NotHaveFee.Substring(0, strKala_NotHaveFee.Length - 1)

            MsgBox("کد کالاهای " & strKala_NotHaveFee & " به دلیل صفر بودن قیمت ثبت نگردیدند .", MsgBoxStyle.Information + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.OkOnly, "")
        End If

        If strKala <> "" Then
            strKala = strKala.Substring(0, strKala.Length - 1)

            MsgBox("کد کالاهای " & strKala & " به دلیل عدم موجودی ثبت نگردیدند .", MsgBoxStyle.Information + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.OkOnly, "")
        End If
        ' Mohasebeh Sen Check Kala Or Brand

        Dim NPardakht As Integer = objTools.ConvertNulls(objTools.DLookup("sNoePardakht", "tblFO_PishFaktor", "ccPishFaktorTitr = " & frm_ccPishFaktor), 0)
        If NPardakht = 3952 Or NPardakht = 3951 Or NPardakht = 5488 Then
            RassBrand(frm_ccPishFaktor)
        End If

        If ObjCode.GetMeghdarAdadi(NPardakht) < 4 Then
            EffectTedadSatrOnModatCheck(frm_ccPishFaktor)
        End If

        Me.Close()
    End Sub
    Private Sub EffectTedadSatrOnModatCheck(ByVal CK As Integer)
        Try
            Dim EffectSatrFaktorOnModatCheck As Boolean = objTools.ConvertNulls(objTools.DLookup("EffectSatrFaktorOnModatCheck", "tblGL_SysConfig", "CodeMahal = " & CodeMahalFaal), False)

            If EffectSatrFaktorOnModatCheck = True Then
                Dim cnSQL As New SqlConnection
                Dim cmSQL As New SqlCommand
                Dim strSQL As String = ""

                cnSQL = New SqlConnection(ConnectionString)
                cnSQL.Open()

                strSQL = "Sales.spPishFaktor_EffectTedadSatrOnModatCheck "

                cmSQL = New SqlCommand(strSQL, cnSQL)
                cmSQL.CommandType = CommandType.StoredProcedure
                cmSQL.Parameters.Clear()

                cmSQL.Parameters.AddWithValue("ccPishFaktorTitr", CK)
                cmSQL.Parameters.AddWithValue("CodeMahal", CodeMahalFaal)

                cmSQL.ExecuteNonQuery()

                cmSQL = Nothing
                cnSQL.Close()
            End If

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> EffectTedadSatrOnModatCheck ")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> EffectTedadSatrOnModatCheck ")
        End Try

    End Sub
    Private Sub GridEXInsertKala_CurrentCellChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GridEXInsertKala.CurrentCellChanged
        'If GridEXInsertKala.CurrentRow.Selected = False Then
        '    Exit Sub
        'End If

        'Dim ccKala_G As Integer = 0
        'Dim Fee_G As String = ""
        'Dim ZaribForosh_G As String = ""
        'Dim NameKala_G As String = ""

        'If GridEXInsertKala.CurrentRow.RowType = Janus.Windows.GridEX.RowType.Record Then
        '    If GridEXInsertKala.CurrentColumn.Index = 4 And GridEXInsertKala.CurrentRow.Cells(3).Text <> "" Then
        '        NameKala_G = objTools.ConvertNulls(objTools.DLookup("NameKala", "tblAN_kala", "CodeKala = " & GridEXInsertKala.CurrentRow.Cells(3).Text), "---")
        '        ccKala_G = objTools.ConvertNulls(objTools.DLookup("ccKala", "tblAN_kala", "CodeKala = " & GridEXInsertKala.CurrentRow.Cells(3).Text), 0)
        '        Fee_G = ObjCode.GetMablaghForosh(ccKala_G, frm_PishFaktorTarikh, CodeMahalFaal, frm_ccMoshtary)
        '        ZaribForosh_G = objTools.ConvertNulls(objTools.DLookup("ZaribForosh", "tblAN_Kala", "ccKala =" & ccKala_G), 0)

        '        If Not IsValidKala("All", ccKala_G) Then
        '            SendKeys.Send("{Esc}")
        '            Exit Sub
        '        End If

        '        '------------------
        '        Dim TedadPishFaktorFaktorNashodeh As Double = 0
        '        Dim MojodiFely As Double = 0
        '        Dim MojodiGhabelForosh As Double = 0
        '        Dim ccAnbarForosh As Integer = objTools.ConvertNulls(objTools.DLookup("CodeAnbar", "tblAN_Anbar", "AnbarAsly = 1 And CodeMahal = " & CodeMahalFaal), 0)
        '        GetMojodyDarHalForosh(ccKala_G, ccAnbarForosh, TedadPishFaktorFaktorNashodeh, MojodiFely, MojodiGhabelForosh)
        '        '------------------
        '        GridEXInsertKala.CurrentRow.Cells(4).Text = NameKala_G
        '        GridEXInsertKala.CurrentRow.Cells(5).Text = ZaribForosh_G
        '        GridEXInsertKala.CurrentRow.Cells(6).Text = Fee_G
        '        GridEXInsertKala.CurrentRow.Cells(7).Text = MojodiGhabelForosh

        '        GridEXInsertKala.CurrentRow.Cells(11).Text = 0
        '        GridEXInsertKala.CurrentRow.Cells(12).Text = 0
        '        GridEXInsertKala.CurrentRow.Cells(1).Text = 1

        '        For i As Integer = 0 To 3
        '            SendKeys.Send("{Tab}")
        '        Next
        '    ElseIf GridEXInsertKala.CurrentColumn.Index = 9 Then
        '        ccKala_G = objTools.ConvertNulls(objTools.DLookup("ccKala", "tblAN_kala", "CodeKala = " & GridEXInsertKala.CurrentRow.Cells(3).Text), 0)

        '        GridEXInsertKala.CurrentRow.Cells(11).Text = ObjCode.ReturnTedad(ccKala_G, IIf(GridEXInsertKala.CurrentRow.Cells(10).Text <> "", GridEXInsertKala.CurrentRow.Cells(10).Text, 0), IIf(GridEXInsertKala.CurrentRow.Cells(9).Text <> "", GridEXInsertKala.CurrentRow.Cells(9).Text, 0), IIf(GridEXInsertKala.CurrentRow.Cells(8).Text <> "", GridEXInsertKala.CurrentRow.Cells(8).Text, 0))
        '        GridEXInsertKala.CurrentRow.Cells(12).Text = (GridEXInsertKala.CurrentRow.Cells(11).Text * GridEXInsertKala.CurrentRow.Cells(6).Text)

        '        If GridEXInsertKala.CurrentRow.Cells(8).Text = "" Then
        '            GridEXInsertKala.CurrentRow.Cells(8).Text = 0
        '        End If
        '    ElseIf GridEXInsertKala.CurrentColumn.Index = 10 Then
        '        ccKala_G = objTools.ConvertNulls(objTools.DLookup("ccKala", "tblAN_kala", "CodeKala = " & GridEXInsertKala.CurrentRow.Cells(3).Text), 0)

        '        GridEXInsertKala.CurrentRow.Cells(11).Text = ObjCode.ReturnTedad(ccKala_G, IIf(GridEXInsertKala.CurrentRow.Cells(10).Text <> "", GridEXInsertKala.CurrentRow.Cells(10).Text, 0), IIf(GridEXInsertKala.CurrentRow.Cells(9).Text <> "", GridEXInsertKala.CurrentRow.Cells(9).Text, 0), IIf(GridEXInsertKala.CurrentRow.Cells(8).Text <> "", GridEXInsertKala.CurrentRow.Cells(8).Text, 0))
        '        GridEXInsertKala.CurrentRow.Cells(12).Text = (GridEXInsertKala.CurrentRow.Cells(11).Text * GridEXInsertKala.CurrentRow.Cells(6).Text)

        '        If GridEXInsertKala.CurrentRow.Cells(9).Text = "" Then
        '            GridEXInsertKala.CurrentRow.Cells(9).Text = 0
        '        End If
        '    ElseIf GridEXInsertKala.CurrentColumn.Index = 11 Then
        '        ccKala_G = objTools.ConvertNulls(objTools.DLookup("ccKala", "tblAN_kala", "CodeKala = " & GridEXInsertKala.CurrentRow.Cells(3).Text), 0)

        '        GridEXInsertKala.CurrentRow.Cells(11).Text = ObjCode.ReturnTedad(ccKala_G, IIf(GridEXInsertKala.CurrentRow.Cells(10).Text <> "", GridEXInsertKala.CurrentRow.Cells(10).Text, 0), IIf(GridEXInsertKala.CurrentRow.Cells(9).Text <> "", GridEXInsertKala.CurrentRow.Cells(9).Text, 0), IIf(GridEXInsertKala.CurrentRow.Cells(8).Text <> "", GridEXInsertKala.CurrentRow.Cells(8).Text, 0))
        '        GridEXInsertKala.CurrentRow.Cells(12).Text = (GridEXInsertKala.CurrentRow.Cells(11).Text * GridEXInsertKala.CurrentRow.Cells(6).Text)

        '        If GridEXInsertKala.CurrentRow.Cells(10).Text = "" Then
        '            GridEXInsertKala.CurrentRow.Cells(10).Text = 0
        '        End If

        '        If Not IsValidTedad(ccKala_G, Val(GridEXInsertKala.CurrentRow.Cells(11).Text.Replace(",", ""))) Then
        '            GridEXInsertKala.CurrentRow.Delete()
        '            Exit Sub
        '        End If

        '        If Not IsValidEtebar(Val(GridEXInsertKala.CurrentRow.Cells(12).Text.Replace(",", ""))) Then
        '            GridEXInsertKala.CurrentRow.Delete()
        '            Exit Sub
        '        End If

        '        AddRow()

        '        For i As Integer = 0 To 1
        '            SendKeys.Send("{Tab}")
        '        Next

        '    End If
        'End If
    End Sub
    Private Sub AddRow()
        'Dim d As DataRow

        'd = dsForm.Tables("HETitr").NewRow
        'd("ccKala") = GridEXInsertKala.CurrentRow.Cells("ccKala").Text.Replace(",", "")
        'd("NameKala") = GridEXInsertKala.CurrentRow.Cells("NameKala").Text
        'd("Fee") = GridEXInsertKala.CurrentRow.Cells("Fee").Text.Replace(",", "")
        'd("TedadKarton") = GridEXInsertKala.CurrentRow.Cells("TedadKarton").Text.Replace(",", "")
        'd("TedadBasteh") = GridEXInsertKala.CurrentRow.Cells("TedadBasteh").Text.Replace(",", "")
        'd("TedadKhordeh") = GridEXInsertKala.CurrentRow.Cells("TedadKhordeh").Text.Replace(",", "")
        'd("Tedad1") = GridEXInsertKala.CurrentRow.Cells("Tedad1").Text.Replace(",", "")
        'd("Taeed") = GridEXInsertKala.CurrentRow.Cells("Taeed").Text.Replace(",", "")

        'dsForm.Tables("HETitr").Rows.Add(d)

        'dvForm = dsForm.Tables("HETitr").DefaultView
    End Sub
    Private Function IsValidKala(ByVal chkField As String, ByVal CK As Integer) As Boolean
        Try
            IsValidKala = False

            Dim ValidKala As Boolean = False
            If objTools.DCount("pk", "tblGL_SecurityData", "namekarbar= '" & UserName & "' and CodeSubSystem = 642 and pk = " & CK) Then
                ValidKala = True
            End If

            If chkField = "ccKala" Or chkField = "All" Then
                If UserName.ToLower <> "administrator" And ValidKala = True Then
                    MsgBox("شما دسترسی استفاده از این کالا را ندارید .", MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " پیام")
                    Exit Function
                End If
            End If

            If chkField = "txtCodeKala" Or chkField = "All" Then
                If objTools.ConvertNulls(objTools.DCount("ccKala", "tblFO_PishFaktorSatr", "ccKala = " & CK & " AND ccPishFaktorTitr = " & frm_ccPishFaktor), 0) >= 1 Then
                    MsgBox("کالای مورد نظر دراین پیش فاکتور صادر شده است", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "تایید")
                    Exit Function
                End If
            End If

            Dim KalaFaal As Integer = objTools.ConvertNulls(objTools.DLookup("Faal", "tblAN_Kala", "ccKala = " & CK), 0)
            If Not KalaFaal = 1 Then
                MsgBox("این کالا غیر فعال است", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
                Exit Function
            End If


            If chkField = "All" Then
                If frm_PishFaktorTarikh.ToString.Substring(0, 4) <> CodeDoreh Then
                    MsgBox("تاريخ پیش فاکتور با دوره مالی فعال يکی نيست.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
                    Exit Function
                End If
            End If

            Dim Fee As Integer = ObjCode.GetMablaghForosh(CK, frm_PishFaktorTarikh, CodeMahalFaal, frm_ccMoshtary)
            If chkField = "txtfee" Or chkField = "All" Then
                If Fee <= 0 Then
                    MsgBox("قیمت کالا را وارد کنيد.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
                    Exit Function
                End If
            End If

            If Fee < objTools.ConvertNulls(objTools.DLookup("top 1 Gheymat", "tblAN_KalaGheymat", "ccKala =  " & CK & "  and NoeFieldGheymat = 1 and ccMarkazPakhsh = " & CodeMahalFaal & " Order By Tarikh Desc"), 0) Then
                If MsgBox("مبلغ خرید بیش از مبلغ فروش است، مایل به ذخیره کالا هستید؟", MsgBoxStyle.YesNo + MsgBoxStyle.Question + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.DefaultButton1, "تایید") = MsgBoxResult.No Then
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
    Private Function IsValidTedad(ByVal CK As Integer, ByVal TedadKala As Double) As Boolean
        Try
            IsValidTedad = False

            If objTools.ConvertNulls(objTools.DLookup("CheckZaribForosh", "tblGL_SysConfig", "CodeMahal = " & CodeMahalFaal), 0) Then
                Dim ZaribTedadForosh As Double = objTools.ConvertNulls(objTools.DLookup("ZaribForosh", "tblAN_Kala", "ccKala = " & CK), 0)
                If TedadKala Mod ZaribTedadForosh <> 0 Then
                    MsgBox("ضریب فروش برای این کالا " & ZaribTedadForosh & " می باشد، لطفاً تعداد را طبق ضریب فروش وارد کنید.", MsgBoxStyle.MsgBoxRtlReading Or MsgBoxStyle.MsgBoxRight Or MsgBoxStyle.Information, "ذخيره")
                    Exit Function
                End If
            End If

            If objTools.ConvertNulls(objTools.DLookup("CheckSahmiehBandy", "tblGL_SysConfig", "CodeMahal=" & CodeMahalFaal), 0) Then

                Dim cnSQL As SqlConnection
                Dim drSQL As SqlDataReader
                Dim cmSQL As SqlCommand
                Dim strSQL As String = ""
                Dim p As New SqlParameter

                Dim TedadSahmieh As Double = 0
                Dim AzTarikhSahmieh As String = ""
                Dim TaTarikhSahmieh As String = ""
                Dim ccForoshandeh_S As Integer = objTools.DLookup("ccForoshandeh", "tblFO_PishFaktor", "ccPishFaktorTitr = " & frm_ccPishFaktor)
                Dim CodeFard As Integer = objTools.DLookup("CodeFard", "qryFO_Foroshandeh", "ccForoshandeh = " & ccForoshandeh_S)

                strSQL = "Sales.spPishFaktor_SahmiehBandi "

                cnSQL = New SqlConnection(ConnectionString)
                cnSQL.Open()

                cmSQL = New SqlCommand(strSQL, cnSQL)
                cmSQL.CommandType = CommandType.StoredProcedure
                cmSQL.Parameters.Clear()

                p = New SqlParameter("CodeMahal", SqlDbType.Int)
                p.Value = CodeMahalFaal
                cmSQL.Parameters.Add(p)

                p = New SqlParameter("CodeFard_Foroshandeh", SqlDbType.Int)
                p.Value = CodeFard
                cmSQL.Parameters.Add(p)

                p = New SqlParameter("ccKala", SqlDbType.Int)
                p.Value = CK
                cmSQL.Parameters.Add(p)

                p = New SqlParameter("Tarikh", SqlDbType.NVarChar, 8)
                p.Value = frm_PishFaktorTarikh
                cmSQL.Parameters.Add(p)

                drSQL = cmSQL.ExecuteReader()
                drSQL.Read()

                If drSQL.HasRows Then
                    TedadSahmieh = objTools.ConvertNulls(drSQL("tedadSahmieh"), 0)
                    AzTarikhSahmieh = objTools.ConvertNulls(drSQL("AzTarikhSlash"), 0)
                    TaTarikhSahmieh = objTools.ConvertNulls(drSQL("TaTarikhSlash"), 0)

                    Dim NameKala_S As String = objTools.ConvertNulls(objTools.DLookup("NameKala", "tblAN_kala", "ccKala = " & CK), "---")

                    Dim TedadSahmiehEstefadehShodeh As Double = objTools.ConvertNulls(objTools.DSum("tedad3", "qryFO_PishFaktorTitrSatr", "codefard= " & CodeFard & " AND ccKala =" & _
                                               CK & " AND pishfaktortarikh between '" & AzTarikhSahmieh.Replace("/", "") & " ' and '" & TaTarikhSahmieh.Replace("/", "") & "'"), 0)
                    If TedadSahmiehEstefadehShodeh + TedadKala > TedadSahmieh Then
                        MsgBox("سهمیه کالای " & NameKala_S & " برای فروشنده این پیش فاکتور " & TedadSahmieh & "  می باشد. ", _
                                        MsgBoxStyle.MsgBoxRtlReading Or MsgBoxStyle.MsgBoxRight Or MsgBoxStyle.Information, "")
                        Exit Function
                    End If
                End If

                drSQL.Close()
                cnSQL.Close()

            End If

            Return True
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> IsValidTedad ")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> IsValidTedad ")
        End Try
    End Function
    Private Function IsValidEtebar(ByVal MablaghSatr As Double) As Boolean
        Try
            IsValidEtebar = False

            'Dim MablaghSatr As Double = (Tedad * Fee)

            Dim CheckSaghfFaktor As Boolean = objTools.ConvertNulls(objTools.DLookup("CheckSaghfFaktor", "tblGL_SysConfig", "CodeMahal = " & CodeMahalFaal), 0)
            If CheckSaghfFaktor = True Then
                Dim JamPishFaktor As Double = objTools.ConvertNulls(objTools.DSum("MKol3", "tblFO_PishFaktorSatr", "ccPishFaktorTitr = " & frm_ccPishFaktor), 0)
                Dim JPF As Double = (JamPishFaktor + MablaghSatr)
                Dim SaghfFaktor As Double = objTools.ConvertNulls(objTools.DLookup("SaghfFaktor", "tblfo_moshtary", "ccMoshtary = " & frm_ccMoshtary), 0)

                If SaghfFaktor < JPF Then
                    MsgBox("جمع مبلغ این پیش فاکتور از سقف اعتباری آن بیشتر است.سقف اعتباری(" & SaghfFaktor & ")", MsgBoxStyle.MsgBoxRtlReading Or MsgBoxStyle.MsgBoxRight Or MsgBoxStyle.Information, "ذخيره")
                    Exit Function
                End If
            End If

            Dim CheckKafeFaktor As Boolean = objTools.ConvertNulls(objTools.DLookup("KafePishFaktor", "tblGL_SysConfig", "CodeMahal = " & CodeMahalFaal), 0)
            If CheckKafeFaktor = True Then
                Dim JamPishFaktor As Double = objTools.ConvertNulls(objTools.DSum("MKol3", "tblFO_PishFaktorSatr", "ccPishFaktorTitr = " & frm_ccPishFaktor), 0)
                Dim JPF As Double = (JamPishFaktor + MablaghSatr)
                Dim KafeFaktor As Double = objTools.ConvertNulls(objTools.DLookup("KafeFaktor", "tblfo_moshtary", "ccMoshtary = " & frm_ccMoshtary), 0)

                If KafeFaktor > JPF Then
                    MsgBox("جمع مبلغ این پیش فاکتور از کف اعتباری آن کمتر است.کف اعتباری(" & KafeFaktor & ")", MsgBoxStyle.MsgBoxRtlReading Or MsgBoxStyle.MsgBoxRight Or MsgBoxStyle.Information, "ذخيره")
                    Exit Function
                End If
            End If

            Return True
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> IsValidInsert ")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> IsValidInsert ")
        End Try
    End Function
    Private Sub InsertKala(ByVal ccKala As Integer, ByVal CodeKala As String, ByVal Tedad As Double, ByVal DarsadTakhfif As Double, ByVal IsJayezeh As Int16, ByVal Fee As Double)
        Try

            '' -------------- Control Mojodi --------------
            Dim TedadPishFaktorFaktorNashodeh As Double = 0
            Dim MojodiFely As Double = 0
            Dim MojodiGhabelForosh As Double = 0
            Dim ccAnbarForosh As Integer = 0
            Dim Mojodi As Double = 0
            Dim MojodiDarInPishFaktor = 0

            If AllowChangeFeePishFaktor = True Then
                If Fee = 0 Then
                    strKala_NotHaveFee &= CodeKala & ","
                    Exit Sub
                End If
            End If

            If objTools.DLookup("PishFaktorAmani", "tblFO_PishFaktor", "ccPishFaktorTitr = " & frm_ccPishFaktor) = True Then
                ccAnbarForosh = objTools.DLookup("ccAnbar", "tblFO_PishFaktor", "ccPishFaktorTitr = " & frm_ccPishFaktor)
            Else
                ccAnbarForosh = objTools.ConvertNulls(objTools.DLookup("CodeAnbar", "tblAN_Anbar", "AnbarAsly = 1 And CodeMahal = " & CodeMahalFaal), 0)
            End If

            If ObjCode.CheckMojodiAlert = 2 Then
                GetMojodyDarHalForosh(ccKala, ccAnbarForosh, TedadPishFaktorFaktorNashodeh, MojodiFely, MojodiGhabelForosh)
                'MojodiDarInPishFaktor = objTools.ConvertNulls(objTools.DSum("Tedad3", "tblFO_PishFaktorSatr", "ccPishFaktorTitr = " & frm_ccPishFaktor & " AND ccKala = " & ccKala), 0)
                Mojodi = MojodiGhabelForoshKOL - MojodiDarInPishFaktor

                If Tedad > Mojodi Then
                    If IsJayezeh = 0 Then
                        strKala &= CodeKala & ","
                    Else
                        strKala &= CodeKala & " (جایزه),"
                    End If
                    Exit Sub
                End If
                '' -------------- Control Mojodi --------------
            End If

            Dim cnSQL As SqlConnection
            Dim cmSQL As SqlCommand
            Dim strSQL As String

            Dim Radif As Integer = objTools.ConvertNulls(objTools.DMax("Radif", "tblFO_PishFaktorSatr", "ccPishFaktorTitr = " & frm_ccPishFaktor), 0) + 1

            Dim sVahedeShomaresh = objTools.DLookup("sVahedeShomaresh", "tblAN_Kala", "ccKala = " & ccKala)
            Dim NoeMoshtary = objTools.DLookup("sNoeMoshtary", "tblFO_Moshtary", "ccMoshtary = " & frm_ccMoshtary)

            strSQL = "Sales.spPishFaktor_frmInsertAllKala_InsertKala "

            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            cmSQL.Parameters.AddWithValue("ccPishFaktorTitr", frm_ccPishFaktor)
            cmSQL.Parameters.AddWithValue("Radif", Radif)
            cmSQL.Parameters.AddWithValue("ccKala", ccKala)
            cmSQL.Parameters.AddWithValue("Tedad", Tedad)
            cmSQL.Parameters.AddWithValue("DarsadTakhfif", IIf(IsJayezeh = 0, DarsadTakhfif, 100))
            cmSQL.Parameters.AddWithValue("AllowChangeFeePishFaktor", AllowChangeFeePishFaktor)
            cmSQL.Parameters.AddWithValue("Fee", Fee)

            cmSQL.ExecuteNonQuery()

            cmSQL.Connection.Close()
            cnSQL.Close()

            cmSQL = Nothing
            cnSQL = Nothing

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
    Private Sub RassBrand(ByVal ccPishFaktor As Double)
        If Not objTools.ConvertNulls(objTools.DLookup("RassGiriBrand", "tblGL_SysConfig", "CodeMahal = " & CodeMahalFaal), 0) Then
            Exit Sub
        End If

        Dim cnSQL As SqlConnection
        Dim strSQL As String
        Dim daSQL As SqlDataAdapter

        Dim drSenCheckTitrSatr As DataRow
        Try
            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()
            '-----------------------------------------------------------------------------------------
            If dsForm.Tables.Contains("tblAN_KalaSenChecksatr") Then
                dsForm.Tables.Remove("tblAN_KalaSenChecksatr")
            End If
            strSQL = "Select * From qryAN_KalaSenChecksatr where Faal = 1"
            daSQL = New SqlDataAdapter(strSQL, ConnectionString)
            daSQL.Fill(dsForm, "tblAN_KalaSenChecksatr")
            '-----------------------------------------------------------------------------------------
            Dim MablaghKol As Long = 0
            Dim SumModatDarMablagh As Long = 0
            Dim Modat As Double = 0
            For Each drSenCheckTitrSatr In dsForm.Tables("tblAN_KalaSenChecksatr").Rows
                MablaghKol = objTools.ConvertNulls(objTools.DSum("Mkol3", "qryFO_PishFaktorSatr", " ccPishFaktorTitr = " & ccPishFaktor _
                                                       & " AND IsJayezeh = 0 AND ccBrand = " & drSenCheckTitrSatr("ccBrand") & " AND sG2 =" _
                                                       & drSenCheckTitrSatr("sG2")), 0)
                If MablaghKol >= drSenCheckTitrSatr("AzMablagh") And MablaghKol <= drSenCheckTitrSatr("TaMablagh") Then
                    SumModatDarMablagh += drSenCheckTitrSatr("Modat") * MablaghKol
                End If
            Next

            If SumModatDarMablagh = 0 Then
                Modat = 0
            Else
                Modat = SumModatDarMablagh / objTools.DLookup("JamPishFaktor", "tblFO_PishFaktor", "ccPishFaktorTitr=" & ccPishFaktor)
            End If

            objTools.DUpdate("SenCheckBrand", "tblFO_PishFaktor", Modat, " ccPishFaktorTitr = " & ccPishFaktor)
            objTools.DUpdate("ModatCheck", "tblFO_PishFaktor", Modat, " ccPishFaktorTitr = " & ccPishFaktor)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub
    Private Sub chkShowWithJayezeh_CheckedChanged(sender As Object, e As EventArgs) Handles chkShowWithJayezeh.CheckedChanged
        Search(False)
    End Sub
End Class