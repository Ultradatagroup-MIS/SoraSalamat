Public Class frmFO_SaveDarsadMarjoeeMojazForoshandeh
#Region "Variable AND Constant Declration"
    ' Quary And Table Names
    Const FormViewName = "qryFO_PishFaktorTitrSatr"
    ' Security 
    Const cntCodeSubSystem As Long = 1000119
    Private SN As Integer

    Dim ErrPro As New ErrorProvider
    Dim dsForm As New DataSet
    Dim dr As DataRow
    Dim dvForm, dvTitr As DataView
    Dim txtCaption As String

    Private WithEvents BS As New UD_Dll.PassString
#End Region
    Private Sub frmFO_SaveDarsadMarjoeeMojazForoshandeh_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetParameter()
        SN = objSec.GetSecurityNumber(cntCodeSubSystem, UserName)
        objTools.ChangeKbd(UD_Dll.mdlUtility.KBD.Farsi)
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
            CodeDoreh = "1392"

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
    Private Sub Search()
        Try
            GridEXTitr.DataSource = Nothing

            Dim StrSql As String

            StrSql = "Report.spGozareshMarjoeeMojaz_DarsadMarjoeeMojazForoshandegan_Search_Afrashir "

            RefreshFormData(StrSql)

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> Search ")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> Search ")
        End Try
    End Sub
    Private Sub RefreshFormData(ByVal strSql As String)
        Dim cnSQL As New SqlConnection
        Dim cmSQL As New SqlCommand
        Dim daSQL As SqlDataAdapter

        Try

            cnSQL.ConnectionString = ConnectionString
            cnSQL.Open()

            cmSQL = New SqlCommand(strSql, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            cmSQL.Parameters.AddWithValue("CodeMahal", CodeMahalFaal)

            If dsForm.Tables.Contains("tbl_Search") Then
                dsForm.Tables.Remove("tbl_Search")
            End If

            daSQL = New SqlDataAdapter(cmSQL)
            daSQL.Fill(dsForm, "tbl_Search")

            dvForm = New DataView
            dvForm = dsForm.Tables("tbl_Search").DefaultView
            dvForm.Sort = "NameForoshandeh ASC"
            dvForm.AllowDelete = True
            dvForm.AllowEdit = False
            dvForm.AllowNew = False
            daSQL = Nothing

            Dim col As DataColumn
            '------------Adding Columns------------
            col = New DataColumn
            col.ColumnName = "Taeed"
            col.DataType = GetType(Boolean)
            col.DefaultValue = False
            dsForm.Tables("tbl_Search").Columns.Add(col)
            '--------------------------------------

            dvTitr = New DataView(dsForm.Tables("tbl_Search"), "", "NameForoshandeh ASC", DataViewRowState.CurrentRows)
            dvTitr.AllowNew = False
            dvTitr.AllowDelete = False
            dvTitr.AllowEdit = False

            GridEXTitr.DataSource = Nothing
            GridEXTitr.DataSource = dvTitr

            If dvTitr.Count <> 0 Then
                SetGridStyle()
            End If

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> RefreshFormData ")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> RefreshFormData ")
        Finally
            cmSQL = Nothing : daSQL = Nothing
            cnSQL.Close()
        End Try
    End Sub
    Private Sub SetGridStyle()
        Try
            With GridEXTitr
                .DataSource = Nothing
                .DataSource = dsForm.Tables("tbl_Search").DefaultView
                .SetDataBinding(dsForm.Tables("tbl_Search").DefaultView, "")
                .RetrieveStructure()
            End With

            For i As Integer = 0 To GridEXTitr.CurrentTable.Columns.Count - 1
                GridEXTitr.CurrentTable.Columns.Item(i).Visible = False
            Next

            GridEXTitr.CurrentTable.Columns.Item("Taeed").Caption = "Taeed"
            GridEXTitr.CurrentTable.Columns.Item("Taeed").Visible = False
            GridEXTitr.CurrentTable.Columns.Item("Taeed").Width = 0
            GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXTitr.CurrentTable.Columns.Item("Taeed").Position = 0
            GridEXTitr.CurrentTable.Columns.Item("Taeed").Selectable = True
            GridEXTitr.CurrentTable.Columns.Item("Taeed").ActAsSelector = True
            GridEXTitr.CurrentTable.Columns.Item("Taeed").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXTitr.CurrentTable.Columns.Item("CodeForoshandeh").Caption = "کـد فـروشنــده"
            GridEXTitr.CurrentTable.Columns.Item("CodeForoshandeh").Visible = True
            GridEXTitr.CurrentTable.Columns.Item("CodeForoshandeh").Width = 90
            GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXTitr.CurrentTable.Columns.Item("CodeForoshandeh").Position = 1
            GridEXTitr.CurrentTable.Columns.Item("CodeForoshandeh").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXTitr.CurrentTable.Columns.Item("NameForoshandeh").Caption = "نـام فـروشنــده"
            GridEXTitr.CurrentTable.Columns.Item("NameForoshandeh").Visible = True
            GridEXTitr.CurrentTable.Columns.Item("NameForoshandeh").Width = 237
            GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXTitr.CurrentTable.Columns.Item("NameForoshandeh").Position = 2
            GridEXTitr.CurrentTable.Columns.Item("NameForoshandeh").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXTitr.CurrentTable.Columns.Item("DarsadMarjoeeMojaz").Caption = "درصد مرجوعی مجاز"
            GridEXTitr.CurrentTable.Columns.Item("DarsadMarjoeeMojaz").Visible = True
            GridEXTitr.CurrentTable.Columns.Item("DarsadMarjoeeMojaz").Width = 130
            GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXTitr.CurrentTable.Columns.Item("DarsadMarjoeeMojaz").FormatString = "G"
            GridEXTitr.CurrentTable.Columns.Item("DarsadMarjoeeMojaz").Position = 3
            GridEXTitr.CurrentTable.Columns.Item("DarsadMarjoeeMojaz").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridEXTitr.CurrentTable.Columns.Item("ccForoshandeh").Caption = "ccForoshandeh"
            GridEXTitr.CurrentTable.Columns.Item("ccForoshandeh").Visible = False
            GridEXTitr.CurrentTable.Columns.Item("ccForoshandeh").Width = 0
            GridEXTitr.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXTitr.CurrentTable.Columns.Item("ccForoshandeh").Position = 4
            GridEXTitr.CurrentTable.Columns.Item("ccForoshandeh").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

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
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> SetGridStyle")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> SetGridStyle")
        End Try

    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Save()
    End Sub
    Private Sub Save()
        Dim cnSQL As New SqlConnection
        Dim cmSQL As New SqlCommand
        Dim strSQL As String = ""

        Try
            For i As Integer = 0 To GridEXTitr.GetRows().Length - 1
                cnSQL = New SqlConnection(ConnectionString)
                cnSQL.Open()

                strSQL = "Report.spGozareshMarjoeeMojaz_DarsadMarjoeeMojazForoshandegan_Update_Afrashir "

                cmSQL = New SqlCommand(strSQL, cnSQL)
                cmSQL.CommandType = CommandType.StoredProcedure
                cmSQL.Parameters.Clear()

                cmSQL.Parameters.AddWithValue("ccForoshandeh", Val(GridEXTitr.GetRows(i).Cells("ccForoshandeh").Text.Replace(",", "")))
                cmSQL.Parameters.AddWithValue("DarsadMarjoeeMojaz", GridEXTitr.GetRows(i).Cells("DarsadMarjoeeMojaz").Text.Replace(",", ""))

                cmSQL.ExecuteNonQuery()

                cmSQL = Nothing
                cnSQL.Close()
            Next


            Search()

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> Save ")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> Save ")
        End Try
    End Sub
End Class