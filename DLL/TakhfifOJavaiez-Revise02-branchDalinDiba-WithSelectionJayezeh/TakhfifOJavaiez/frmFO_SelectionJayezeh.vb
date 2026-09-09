Public Class frmFO_SelectionJayezeh
#Region "Variable AND Constant Declration"
    'Const cntCodeSubSystem As Long = 625

    Dim cmTitr As CurrencyManager
    Dim ErrPro As New ErrorProvider
    Dim dsForm As New DataSet
    Dim dvForm As DataView
    Private SN As Integer
    Dim txtCaption As String
    Private WithEvents BS As New UD_Dll.PassString

    Public ccJayezeh As Integer = 0
    Public TedadJayezeh As Double = 0
    Public frm_ccPishFaktor As Integer = 0
    Public frm_ccElamMarjoee As Integer = 0
    Public frm_Effect As Boolean = False
    Public frm_CodeDoreh As Integer = objTools.DLookup("CodeDoreh", "tblFO_PishFaktor", "ccPishFaktorTitr = " & frm_ccPishFaktor)
    Public frm_CodeMahal As Integer = objTools.DLookup("CodeMahal", "tblFO_PishFaktor", "ccPishFaktorTitr = " & frm_ccPishFaktor)
    Dim strKala As String = ""

#End Region
    Private Sub frmFO_SelectionJayezeh_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            txtTedaJayezeh.Text = TedadJayezeh
            frm_CodeDoreh = objTools.DLookup("CodeDoreh", "tblFO_PishFaktor", "ccPishFaktorTitr = " & frm_ccPishFaktor)
            frm_CodeMahal = objTools.DLookup("CodeMahal", "tblFO_PishFaktor", "ccPishFaktorTitr = " & frm_ccPishFaktor)

            Search()

            objTools.ChangeKbd(UD_Dll.mdlUtility.KBD.Farsi)

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> frmFO_InsertAllKala_Load ")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> frmFO_InsertAllKala_Load ")
        End Try
    End Sub
    Public Sub Search()
        Try
            Dim strSQL As String

            strSQL = "dbo.sp_CalcJayezeh_SearchKala "

            RefreshTitrdata(strSQL)

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> Search ")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> Search ")
        End Try
    End Sub
    Private Sub RefreshTitrdata(ByVal strSQL As String) '' True --> Search Koli \\ False --> Load Kardan Safhe Be Sorat Khali
        Dim daSQL As SqlDataAdapter
        Dim cnSQL As New SqlConnection
        Dim cmSQL As New SqlCommand

        Dim ccAnbar As Integer = 0
        ccAnbar = objTools.ConvertNulls(objTools.DLookup("ccAnbar", "tblFO_PishFaktor", "ccPishFaktorTitr = " & frm_ccPishFaktor), 0)

        If ccAnbar = 0 Then
            ccAnbar = objTools.DLookup("CodeAnbar", "tblAN_Anbar", "AnbarAsly = 1 ANd Faal = 1 AND CodeMahal = " & frm_CodeMahal)
        End If

        Try

            If dsForm.Tables.Contains("HETitr") Then
                dsForm.Tables.Remove("HETitr")
            End If

            cnSQL.ConnectionString = ConnectionString
            cnSQL.Open()

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            cmSQL.Parameters.AddWithValue("ccJayezeh", ccJayezeh)
            cmSQL.Parameters.AddWithValue("ccPishFaktor", frm_ccPishFaktor)
            cmSQL.Parameters.AddWithValue("ccMarjoee", frm_ccElamMarjoee)
            cmSQL.Parameters.AddWithValue("Effect", frm_Effect)
            cmSQL.Parameters.AddWithValue("ccAnbar", ccAnbar)

            daSQL = New SqlDataAdapter(cmSQL)
            daSQL.Fill(dsForm, "HETitr")
            dvForm = New DataView
            dvForm = dsForm.Tables("HETitr").DefaultView
            dvForm.AllowDelete = True
            dvForm.AllowEdit = True
            dvForm.AllowNew = True
            daSQL = Nothing

            dvForm = New DataView(dsForm.Tables("HETitr"), "", "CodeKala ASC", DataViewRowState.CurrentRows)
            dvForm.AllowNew = True
            dvForm.AllowDelete = True
            dvForm.AllowEdit = True

            cmSQL.Connection.Close()
            cnSQL.Close()
            daSQL = Nothing

            GridEXKala.DataSource = Nothing
            GridEXKala.DataSource = dvForm

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
            With GridEXKala
                .DataSource = Nothing
                .DataSource = dsForm.Tables("HETitr").DefaultView
                .SetDataBinding(dsForm.Tables("HETitr").DefaultView, "")
                .RetrieveStructure()
            End With

            For i As Integer = 0 To GridEXKala.CurrentTable.Columns.Count - 1
                GridEXKala.CurrentTable.Columns.Item(i).Visible = False
            Next

            GridEXKala.CurrentTable.Columns.Item("CodeKala").Caption = "کد کالا"
            GridEXKala.CurrentTable.Columns.Item("CodeKala").Visible = True
            GridEXKala.CurrentTable.Columns.Item("CodeKala").Width = 100
            'GridEXKala.CurrentTable.Columns.Item("CodeKala").EditType = Janus.Windows.GridEX.EditType.NoEdit
            GridEXKala.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXKala.CurrentTable.Columns.Item("CodeKala").Position = 0
            GridEXKala.CurrentTable.Columns.Item("CodeKala").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXKala.CurrentTable.Columns.Item("NameKala").Caption = "نام کالا"
            GridEXKala.CurrentTable.Columns.Item("NameKala").Visible = True
            GridEXKala.CurrentTable.Columns.Item("NameKala").Width = 450
            'GridEXKala.CurrentTable.Columns.Item("NameKala").EditType = Janus.Windows.GridEX.EditType.NoEdit
            GridEXKala.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXKala.CurrentTable.Columns.Item("NameKala").Position = 1
            GridEXKala.CurrentTable.Columns.Item("NameKala").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXKala.CurrentTable.Columns.Item("Tedad").Caption = "تعداد جایزه"
            GridEXKala.CurrentTable.Columns.Item("Tedad").Visible = True
            GridEXKala.CurrentTable.Columns.Item("Tedad").Width = 100
            GridEXKala.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOn
            GridEXKala.CurrentTable.Columns.Item("Tedad").Position = 2
            GridEXKala.CurrentTable.Columns.Item("Tedad").FormatString = "G"
            GridEXKala.CurrentTable.Columns.Item("Tedad").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXKala.CurrentTable.Columns.Item("TedadMojod").Caption = "تعداد موجود"
            GridEXKala.CurrentTable.Columns.Item("TedadMojod").Visible = True
            GridEXKala.CurrentTable.Columns.Item("TedadMojod").Width = 100
            GridEXKala.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXKala.CurrentTable.Columns.Item("TedadMojod").Position = 3
            GridEXKala.CurrentTable.Columns.Item("TedadMojod").FormatString = "G"
            GridEXKala.CurrentTable.Columns.Item("TedadMojod").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXKala.CurrentTable.Columns.Item("ccKala").Caption = "ccKala"
            GridEXKala.CurrentTable.Columns.Item("ccKala").Visible = False
            GridEXKala.CurrentTable.Columns.Item("ccKala").Width = 0
            GridEXKala.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXKala.CurrentTable.Columns.Item("ccKala").Position = 4
            GridEXKala.CurrentTable.Columns.Item("ccKala").FormatString = "G"
            GridEXKala.CurrentTable.Columns.Item("ccKala").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            For i As Integer = 0 To GridEXKala.RootTable.Columns.Count - 1
                If GridEXKala.RootTable.Columns(i).Type.IsValueType Then
                    GridEXKala.RootTable.Columns(i).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                    GridEXKala.RootTable.Columns(i).FormatMode = Janus.Windows.GridEX.FormatMode.UseIFormattable
                    GridEXKala.RootTable.Columns(i).FormatString = "G"
                    GridEXKala.RootTable.Columns(i).TotalFormatMode = Janus.Windows.GridEX.FormatMode.UseIFormattable
                    GridEXKala.RootTable.Columns(i).TotalFormatString = "G"
                End If
            Next

            GridEXKala.Visible = True
            GridEXKala.HeaderFormatStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> SetGridStyle ")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> SetGridStyle ")
        End Try

    End Sub
    Private Sub BoundCurrencyManager(ByVal cm As SqlCommand)
        Try
            cmTitr = CType(BindingContext(GridEXKala.DataSource), CurrencyManager)
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
    Private Sub btnTaeed_Click(sender As Object, e As EventArgs) Handles btnTaeed.Click
        Dim dicKala As New Dictionary(Of Integer, Double)

        For i As Integer = 0 To GridEXKala.RowCount - 1
            If Val(GridEXKala.GetRow(i).Cells("Tedad").Text.Trim) > 0 Then
                If frm_ccElamMarjoee <> 0 Then
                    If Val(GridEXKala.GetRow(i).Cells("Tedad").Text.Trim) > Val(GridEXKala.GetRow(i).Cells("TedadMojod").Text.Trim) Then
                        MsgBox("تعداد وارد شده برای کالای " & Val(GridEXKala.GetRow(i).Cells("Tedad").Text.Trim) & " بیشتر از تعداد موجود است !!", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, "خطا")
                        Exit Sub
                    End If
                End If

                dicKala.Add(Val(GridEXKala.GetRow(i).Cells("ccKala").Text.Trim.Replace(",", "")), Val(GridEXKala.GetRow(i).Cells("Tedad").Text.Trim))
            End If
        Next

        If IsValid(dicKala) = False Then
            Exit Sub
        End If

        '' ------------- Hazfe Mohasebat Ghablie Takhfif & Jayezeh ------------- 
        objTools.DDelete("TakhfifJaizeh.CalcJayezeh_Selection", "ccJayezeh = " & ccJayezeh & " AND ccPishFaktor = " & frm_ccPishFaktor & " AND ccMarjoee = " & frm_ccElamMarjoee)
        '' ------------- Hazfe Mohasebat Ghablie Takhfif & Jayezeh -------------

        For Each j As Integer In dicKala.Keys
            If frm_ccElamMarjoee <> 0 Then
                InsertJayezeh_Marjoee(j, dicKala(j))
            Else
                InsertJayezeh(j, dicKala(j))
            End If
        Next

        Me.Close()
    End Sub
    Private Function IsValid(ByVal dicKala As Dictionary(Of Integer, Double)) As Boolean
        IsValid = False
        Dim SumKala As Double = 0

        For Each i As String In dicKala.Keys
            SumKala += dicKala(i)
        Next

        If SumKala > TedadJayezeh Then
            MsgBox("تعداد وارد شده بیش از تعداد جایزه قابل تخصیص می باشد، لطفا اصلاح نمایید !", MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.OkOnly, "خطا")
            Exit Function
        ElseIf SumKala < TedadJayezeh Then
            MsgBox("تعداد وارد شده کمتر از تعداد جایزه قابل تخصیص می باشد، لطفا اصلاح نمایید !", MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.OkOnly, "خطا")
            Exit Function
        Else
            'For Each j As String In dicKala.Keys
            '    'کنترل موجودی هر کالا 
            '    If IsValidMojodi(j, dicKala(j)) = False Then
            '        Dim CodeKala As String = 0
            '        CodeKala = objTools.DLookup("CodeKala", "tblAN_Kala", "ccKala = " & j)

            '        MsgBox("موجودی کـد کالای " & CodeKala & " کمتر از مقدار ثبت شده است، لطفا اصلاح نمایید !", MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.OkOnly, "خطا")
            '        Exit Function
            '    End If
            'Next
        End If

        IsValid = True
    End Function
    Private Function IsValidMojodi(ByVal ccKala As Integer, ByVal Tedad As Double) As Boolean
        IsValidMojodi = False

        Dim cnSQL As New SqlConnection
        Dim cmSQL As New SqlCommand
        Dim strSQL As String = ""
        Dim Mojodi As Double = 0

        Try
            strSQL = "Global.spCalcMojodiGhabelForosh "

            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            cmSQL.Parameters.AddWithValue("ccPishFaktor", frm_ccPishFaktor)
            cmSQL.Parameters.AddWithValue("AzTarikh", frm_CodeDoreh + "0101")
            cmSQL.Parameters.AddWithValue("TaTarikh", TarikhEmrooz)
            cmSQL.Parameters.AddWithValue("ccKala", ccKala)
            cmSQL.Parameters.AddWithValue("CodeMahal", frm_CodeMahal)
            cmSQL.Parameters.AddWithValue("CodeDoreh", frm_CodeDoreh)
            cmSQL.Parameters.AddWithValue("Mojodi", Mojodi)
            cmSQL.Parameters("Mojodi").Direction = ParameterDirection.Output

            cmSQL.ExecuteNonQuery()

            Mojodi = cmSQL.Parameters("Mojodi").Value

            cmSQL = Nothing
            cnSQL.Close()

            If Mojodi >= Tedad Then
                IsValidMojodi = True
            End If

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> IsValidMojodi ")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> IsValidMojodi ")
        End Try
    End Function
    Private Sub InsertJayezeh(ByVal ccKala As Integer, ByVal Tedad As Double)
        Dim cnSQL As New SqlConnection
        Dim cmSQL As New SqlCommand
        Dim strSQL As String = ""
        Try
            strSQL = "dbo.sp_CalcJayezeh_InsertJayezehSelection "

            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            cmSQL.Parameters.AddWithValue("ccPishFaktorTitr", frm_ccPishFaktor)
            cmSQL.Parameters.AddWithValue("ccKala", ccKala)
            cmSQL.Parameters.AddWithValue("Tedad", Tedad)
            cmSQL.Parameters.AddWithValue("Tarikh", TarikhEmrooz)
            cmSQL.Parameters.AddWithValue("ccJayezeh", ccJayezeh)

            cmSQL.ExecuteNonQuery()

            cmSQL = Nothing
            cnSQL.Close()

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> InsertJayezeh ")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> InsertJayezeh ")
        End Try
    End Sub
    Private Sub InsertJayezeh_Marjoee(ByVal ccKala As Integer, ByVal Tedad As Double)
        Dim cnSQL As New SqlConnection
        Dim cmSQL As New SqlCommand
        Dim strSQL As String = ""
        Try
            strSQL = "dbo.sp_CalcJayezeh_InsertJayezehSelection_Marjoee "

            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            cmSQL.Parameters.AddWithValue("ccPishFaktorTitr", frm_ccPishFaktor)
            cmSQL.Parameters.AddWithValue("ccMarjoee", frm_ccElamMarjoee)
            cmSQL.Parameters.AddWithValue("ccKala", ccKala)
            cmSQL.Parameters.AddWithValue("Tedad", Tedad)
            cmSQL.Parameters.AddWithValue("Tarikh", TarikhEmrooz)
            cmSQL.Parameters.AddWithValue("ccJayezeh", ccJayezeh)
            cmSQL.Parameters.AddWithValue("Effect", frm_Effect)

            cmSQL.ExecuteNonQuery()

            cmSQL = Nothing
            cnSQL.Close()

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> InsertJayezeh ")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> InsertJayezeh ")
        End Try
    End Sub

    Private Sub GridEXKala_FormattingRow(sender As Object, e As Janus.Windows.GridEX.RowLoadEventArgs) Handles GridEXKala.FormattingRow

    End Sub
End Class