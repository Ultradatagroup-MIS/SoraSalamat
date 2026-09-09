Public Class frmFO_AddKalaInPishFaktor

#Region "Variable AND Constant Declration"
    Const cntCodeSubSystem As Long = 1000121
    Dim Mode As UD_Dll.Enums.GL_ModeForms = UD_Dll.Enums.GL_ModeForms.None
    Dim dvKala As DataView
    Dim ErrPro As New ErrorProvider
    Dim dsForm As New DataSet
    Private SN As Integer
    Dim tPos As Integer
    Dim txtCaption As String
    Private WithEvents BS As New UD_Dll.PassString
    Public frm_ccTafkik_GG As Integer
    Public frm_ccPishFaktor As Integer
#End Region
    Private Sub frmFO_AddKalaInPishFaktor_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetParameter()
        SearchKala()
    End Sub
    Private Sub SetParameter()
        Dim commands As String = Microsoft.VisualBasic.Command()
        If commands.Length = 0 Then

            UserName = "Administrator"
            UserPassWord = "66998833"
            NameMahalFaal = "تهران"
            CodeMahalFaal = "2049"
            PersonelCode = "0"
            PersonelName = "Administrator"
            CodeDoreh = "1396"
            txtCaption = "پیش فاکتور غیر قطعی"
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
            objCode.UserName = UserName
        End If
    End Sub
    Private Sub SearchKala()
        Dim cnSQL As New SqlConnection
        Dim cmSQL As New SqlCommand
        Dim daSQl As New SqlDataAdapter
        Dim strSQL As String = ""

        If dsForm.Tables.Contains("tbl_Kala") Then
            dsForm.Tables.Remove("tbl_Kala")
        End If

        Try
            strSQL = "Sales.spPishFaktorGheireGhateei_AddKala_Search "

            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            cmSQL.Parameters.AddWithValue("ccTafkik_GG", frm_ccTafkik_GG)
            cmSQL.Parameters.AddWithValue("ccPishFaktorTitr", frm_ccPishFaktor)

            daSQl = New SqlDataAdapter(cmSQL)
            daSQl.Fill(dsForm, "tbl_Kala")

            '------------Adding Columns------------
            dsForm.Tables("tbl_Kala").Columns.Add("Taeed", GetType(Boolean))
            '--------------------------------------

            Dim dr As DataRow
            For Each dr In dsForm.Tables("tbl_Kala").Rows
                dr("Taeed") = False
            Next

            dvKala = New DataView(dsForm.Tables("tbl_Kala"))
            dvKala.Sort = "CodeKala ASC"

            dvKala.AllowNew = False
            dvKala.AllowDelete = False
            dvKala.AllowEdit = True

            cmSQL = Nothing : daSQl = Nothing
            cnSQL.Close()

            SetGridKala()
            With GridEXKala
                .Visible = True
                .DataSource = Nothing
                .DataSource = dvKala
            End With

            Me.CenterToScreen()
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> SearchKala ")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> SearchKala ")
        End Try
    End Sub
    Private Sub SetGridKala()
        If dvKala.Count = 0 Then
            Exit Sub
        End If

        Try
            With GridEXKala
                .DataSource = Nothing
                .DataSource = dsForm.Tables("tbl_Kala").DefaultView
                .SetDataBinding(dsForm.Tables("tbl_Kala").DefaultView, "")
                .RetrieveStructure()
            End With

            For i As Integer = 0 To GridEXKala.CurrentTable.Columns.Count - 1
                GridEXKala.CurrentTable.Columns.Item(i).Visible = False
            Next

            GridEXKala.CurrentTable.Columns.Item("Taeed").Caption = "انتخاب"
            GridEXKala.CurrentTable.Columns.Item("Taeed").Visible = True
            GridEXKala.CurrentTable.Columns.Item("Taeed").Width = 55
            GridEXKala.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXKala.CurrentTable.Columns.Item("Taeed").Position = 0
            GridEXKala.CurrentTable.Columns.Item("Taeed").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXKala.CurrentTable.Columns.Item("Taeed").HeaderAlignment = TextAlignment.Center

            GridEXKala.CurrentTable.Columns.Item("CodeKala").Caption = "کـد کالا"
            GridEXKala.CurrentTable.Columns.Item("CodeKala").Visible = True
            GridEXKala.CurrentTable.Columns.Item("CodeKala").Width = 110
            GridEXKala.CurrentTable.Columns.Item("CodeKala").EditType = EditType.NoEdit
            GridEXKala.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXKala.CurrentTable.Columns.Item("CodeKala").Position = 1
            GridEXKala.CurrentTable.Columns.Item("CodeKala").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXKala.CurrentTable.Columns.Item("CodeKala").HeaderAlignment = TextAlignment.Center

            GridEXKala.CurrentTable.Columns.Item("NameKala").Caption = "نــام کالا"
            GridEXKala.CurrentTable.Columns.Item("NameKala").Visible = True
            GridEXKala.CurrentTable.Columns.Item("NameKala").Width = 285
            GridEXKala.CurrentTable.Columns.Item("NameKala").EditType = EditType.NoEdit
            GridEXKala.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXKala.CurrentTable.Columns.Item("NameKala").Position = 2
            GridEXKala.CurrentTable.Columns.Item("NameKala").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXKala.CurrentTable.Columns.Item("NameKala").HeaderAlignment = TextAlignment.Center

            GridEXKala.CurrentTable.Columns.Item("ZaribForosh").Caption = "ضریب فروش"
            GridEXKala.CurrentTable.Columns.Item("ZaribForosh").Visible = True
            GridEXKala.CurrentTable.Columns.Item("ZaribForosh").Width = 80
            GridEXKala.CurrentTable.Columns.Item("ZaribForosh").EditType = EditType.NoEdit
            GridEXKala.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXKala.CurrentTable.Columns.Item("ZaribForosh").Position = 3
            GridEXKala.CurrentTable.Columns.Item("ZaribForosh").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXKala.CurrentTable.Columns.Item("ZaribForosh").HeaderAlignment = TextAlignment.Center

            GridEXKala.CurrentTable.Columns.Item("TedadGhabeleSabt").Caption = "تعـداد قابل ثبت"
            GridEXKala.CurrentTable.Columns.Item("TedadGhabeleSabt").Visible = True
            GridEXKala.CurrentTable.Columns.Item("TedadGhabeleSabt").Width = 95
            GridEXKala.CurrentTable.Columns.Item("TedadGhabeleSabt").EditType = EditType.NoEdit
            GridEXKala.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXKala.CurrentTable.Columns.Item("TedadGhabeleSabt").Position = 4
            GridEXKala.CurrentTable.Columns.Item("TedadGhabeleSabt").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXKala.CurrentTable.Columns.Item("TedadGhabeleSabt").HeaderAlignment = TextAlignment.Center

            GridEXKala.CurrentTable.Columns.Item("Tedad").Caption = "تعـداد"
            GridEXKala.CurrentTable.Columns.Item("Tedad").Visible = True
            GridEXKala.CurrentTable.Columns.Item("Tedad").Width = 70
            GridEXKala.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXKala.CurrentTable.Columns.Item("Tedad").Position = 5
            GridEXKala.CurrentTable.Columns.Item("Tedad").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXKala.CurrentTable.Columns.Item("Tedad").HeaderAlignment = TextAlignment.Center

            GridEXKala.CurrentTable.Columns.Item("ccKala").Caption = "ccKala"
            GridEXKala.CurrentTable.Columns.Item("ccKala").Visible = False
            GridEXKala.CurrentTable.Columns.Item("ccKala").Width = 0
            GridEXKala.CurrentTable.Columns.Item("ccKala").EditType = EditType.NoEdit
            GridEXKala.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXKala.CurrentTable.Columns.Item("ccKala").Position = 6
            GridEXKala.CurrentTable.Columns.Item("ccKala").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXKala.CurrentTable.Columns.Item("ccKala").HeaderAlignment = TextAlignment.Center

            For i As Integer = 0 To GridEXKala.RootTable.Columns.Count - 1
                If GridEXKala.RootTable.Columns(i).Type.IsValueType Then
                    GridEXKala.RootTable.Columns(i).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                    GridEXKala.RootTable.Columns(i).FormatMode = Janus.Windows.GridEX.FormatMode.UseIFormattable
                    GridEXKala.RootTable.Columns(i).FormatString = "G"
                    GridEXKala.RootTable.Columns(i).TotalFormatMode = Janus.Windows.GridEX.FormatMode.UseIFormattable
                    GridEXKala.RootTable.Columns(i).TotalFormatString = "G"
                End If
            Next

            Me.CenterToScreen()
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> SetGridKala ")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> SetGridKala ")
        End Try
    End Sub
    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Dim ccKala As Integer = 0
        Dim ZaribForosh As Integer = 0
        Dim TedadGhabeleSabt As Double = 0
        Dim Tedad As Double = 0
        Dim CountAddRecord As Integer = 0

        If MsgBox("آیا به انتخاب خود اطمینان دارید ؟ ", MsgBoxStyle.Question + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.YesNo, "") = MsgBoxResult.No Then
            Exit Sub
        End If

        For i As Integer = 0 To GridEXKala.RowCount - 1
            If GridEXKala.GetRow(i).Cells("Taeed").Value = True Then
                ccKala = Val(GridEXKala.GetRow(i).Cells("ccKala").Text.Replace(",", ""))
                ZaribForosh = Val(GridEXKala.GetRow(i).Cells("ZaribForosh").Text.Replace(",", ""))
                TedadGhabeleSabt = Val(GridEXKala.GetRow(i).Cells("TedadGhabeleSabt").Text.Replace(",", ""))
                Tedad = Val(GridEXKala.GetRow(i).Cells("Tedad").Text.Replace(",", ""))

                If TedadGhabeleSabt >= Tedad Then
                    If Tedad Mod ZaribForosh = 0 Then
                        If InsertSatr(ccKala, Tedad) = True Then
                            CountAddRecord += 1
                        End If
                    End If
                End If
            End If
        Next

        If CountAddRecord = 0 Then
            MsgBox("هیچ کالایی به پیش فاکتور افزوده نشد !", MsgBoxStyle.Information + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.OkOnly, "")
        Else
            MsgBox("تعداد " & CountAddRecord & " عــدد کالا با موفقیت به پیش فاکتور انتخاب شده افزوده شد .", MsgBoxStyle.Information + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.OkOnly, "")
        End If

        Me.Close()

    End Sub
    Private Function InsertSatr(ByVal ccKala As Integer, ByVal Tedad As Double) As Boolean
        InsertSatr = False

        Dim cnSQL As New SqlConnection
        Dim cmSQL As New SqlCommand
        Dim strSQL As String = ""

        Try
            strSQL = "Sales.spPishFaktorGheireGhateei_AddKala_InsertKala "

            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            cmSQL.Parameters.AddWithValue("ccTafkik_GG", frm_ccTafkik_GG)
            cmSQL.Parameters.AddWithValue("ccPishFaktorTitr", frm_ccPishFaktor)
            cmSQL.Parameters.AddWithValue("ccKala", ccKala)
            cmSQL.Parameters.AddWithValue("Tedad", Tedad)

            cmSQL.ExecuteNonQuery()

            cmSQL = Nothing
            cnSQL.Close()

            InsertSatr = True
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> SetGridKala ")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> SetGridKala ")
        End Try
    End Function
    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub
End Class