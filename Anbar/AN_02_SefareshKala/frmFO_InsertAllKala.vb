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

    Public frm_ccSefaresh As Integer
    Public frm_NoeSefaresh As Integer = 0
    Dim strKala As String = ""
    Dim strKala_NotHaveFee As String = ""
    Dim AllowChangeFeePishFaktor As Boolean = False
#End Region
    Private Sub frmFO_InsertAllKala_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            SetParameter()

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
            txtCaption = "سفارش کالا"
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

            strSQL = "WareHouse.spSefareshKala_frmInsertAllKala_Search "

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

            cmSQL.Parameters.AddWithValue("NoeSefaresh", frm_NoeSefaresh)
            cmSQL.Parameters.AddWithValue("UserName", UserName)

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
            GridEXInsertKala.CurrentTable.Columns.Item("CodeKala").Width = 130
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

            GridEXInsertKala.CurrentTable.Columns.Item("ccKala").Caption = "ccKala"
            GridEXInsertKala.CurrentTable.Columns.Item("ccKala").Visible = False
            GridEXInsertKala.CurrentTable.Columns.Item("ccKala").Width = 0
            GridEXInsertKala.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXInsertKala.CurrentTable.Columns.Item("ccKala").Position = 4
            GridEXInsertKala.CurrentTable.Columns.Item("ccKala").FormatString = "G"
            GridEXInsertKala.CurrentTable.Columns.Item("ccKala").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

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
                InsertKala(Val(GridEXInsertKala.GetRow(i).Cells("ccKala").Text.Trim), GridEXInsertKala.GetRow(i).Cells("CodeKala").Text.Trim, GridEXInsertKala.GetRow(i).Cells("Tedad").Text.Trim)
            End If
        Next

        Me.Close()
    End Sub
    Private Function IsValidKala(ByVal chkField As String, ByVal CK As Integer) As Boolean
        Try
            IsValidKala = False

            If chkField = "txtCodeKala" Or chkField = "All" Then
                If objTools.ConvertNulls(objTools.DCount("ccKala", "tblAN_kdxSefareshSatr", "ccKala = " & CK & " AND ccKardexTitr = " & frm_ccSefaresh), 0) >= 1 Then
                    MsgBox("کالای مورد نظر دراین سفارش صادر شده است", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "تایید")
                    Exit Function
                End If
            End If

            Dim KalaFaal As Integer = objTools.ConvertNulls(objTools.DLookup("Faal", "tblAN_Kala", "ccKala = " & CK), 0)
            If Not KalaFaal = 1 Then
                MsgBox("این کالا غیر فعال است", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
                Exit Function
            End If


            If chkField = "All" Then
                If objTools.DLookup("TarikhForm", "tblAN_kdxSefaresh", "ccKardexTitr = " & frm_ccSefaresh).ToString.Substring(0, 4) <> CodeDoreh Then
                    MsgBox("تاريخ پیش فاکتور با دوره مالی فعال يکی نيست.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
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
    Private Sub InsertKala(ByVal ccKala As Integer, ByVal CodeKala As String, ByVal Tedad As Double)
        Try
            Dim cnSQL As SqlConnection
            Dim cmSQL As SqlCommand
            Dim strSQL As String

            strSQL = "WareHouse.spSefareshKala_frmInsertAllKala_InsertKala "

            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            cmSQL.Parameters.AddWithValue("ccKardexTitr", frm_ccSefaresh)
            cmSQL.Parameters.AddWithValue("ccKala", ccKala)
            cmSQL.Parameters.AddWithValue("Tedad", Tedad)

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
    Private Sub chkShowWithJayezeh_CheckedChanged(sender As Object, e As EventArgs)
        Search(False)
    End Sub
End Class