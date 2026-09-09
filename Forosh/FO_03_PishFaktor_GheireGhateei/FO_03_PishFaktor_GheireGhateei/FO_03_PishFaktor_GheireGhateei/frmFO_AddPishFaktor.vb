Public Class frmFO_AddPishFaktor

#Region "Variable AND Constant Declration"
    Const cntCodeSubSystem As Long = 1000121
    Dim Mode As UD_Dll.Enums.GL_ModeForms = UD_Dll.Enums.GL_ModeForms.None
    Dim dvPishFaktor As DataView
    Dim ErrPro As New ErrorProvider
    Dim dsForm As New DataSet
    Private SN As Integer
    Dim tPos As Integer
    Dim txtCaption As String
    Private WithEvents BS As New UD_Dll.PassString
    Public frm_ccTafkik_GG As Integer
#End Region

    Private Sub frmFO_AddPishFaktor_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetParameter()
        LoadCombo()
        SetForm()

        SearchPishFaktor()
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
    Private Sub LoadCombo()

        Dim Strsql As String
        Dim cnSQL As New SqlConnection
        Dim cmSQL As New SqlCommand
        Dim p As New SqlParameter
        Dim daSQL As SqlDataAdapter
        Dim dr As DataRow

        cnSQL = New SqlConnection(ConnectionString)
        cnSQL.Open()

        '----- Load Combo tblAnbar
        Strsql = "Global.spAnbarSalem_LoadCombo "

        cmSQL = New SqlCommand(Strsql, cnSQL)
        cmSQL.CommandType = CommandType.StoredProcedure
        cmSQL.Parameters.Clear()

        cmSQL.Parameters.AddWithValue("CodeMahal", CodeMahalFaal)
        cmSQL.Parameters.AddWithValue("UserName", UserName)

        daSQL = New SqlDataAdapter(cmSQL)
        daSQL.Fill(dsForm, "tblAnbar")
        cmbAnbar.DataSource = Nothing
        cmbAnbar.Items.Clear()
        cmbAnbar.DataSource = dsForm.Tables("tblAnbar").DefaultView
        cmbAnbar.DisplayMember = "NameAnbar"
        cmbAnbar.ValueMember = "codeAnbar"

        daSQL = Nothing

        '----- Load Combo Foroshandeh
        Strsql = "Global.spForoshandeh_LoadCombo "

        cmSQL = New SqlCommand(Strsql, cnSQL)
        cmSQL.CommandType = CommandType.StoredProcedure
        cmSQL.Parameters.Clear()

        cmSQL.Parameters.AddWithValue("CodeMahal", CodeMahalFaal)
        cmSQL.Parameters.AddWithValue("sVazeiat", UD_Dll.Enums.FO_VaziatForoshandeh.NoFaal)
        cmSQL.Parameters.AddWithValue("UserName", UserName)

        daSQL = New SqlDataAdapter(cmSQL)
        daSQL.Fill(dsForm, "tblForoshandeh")

        dr = dsForm.Tables("tblForoshandeh").NewRow()
        dr("ccForoshandeh") = 0
        dr("NameForoshandeh") = "همه"
        dsForm.Tables("tblForoshandeh").Rows.Add(dr)

        cmbForoshandehS.DataSource = Nothing
        cmbForoshandehS.Items.Clear()
        cmbForoshandehS.DataSource = dsForm.Tables("tblForoshandeh").DefaultView
        cmbForoshandehS.DisplayMember = "NameForoshandeh"
        cmbForoshandehS.ValueMember = "ccForoshandeh"

        cmSQL = Nothing : daSQL = Nothing
        cnSQL.Close()

    End Sub
    Private Sub SetForm()
        Dim CodeAnbarAsly As Integer = 0
        CodeAnbarAsly = objTools.DLookupOne("CodeAnbar", "tblAN_Anbar", "AnbarAsly = 1 AND CodeMahal = " & CodeMahalFaal, "CodeAnbar ASC")
        cmbAnbar.SelectedValue = CodeAnbarAsly

        cmbForoshandehS.SelectedValue = 0

        mskAzTarikh.Text = objTarikh.DecDay(TarikhEmrooz, 2)
        mskTaTarikh.Text = TarikhEmrooz
    End Sub
    Private Sub SearchPishFaktor()
        Dim cnSQL As New SqlConnection
        Dim cmSQL As New SqlCommand
        Dim daSQl As New SqlDataAdapter
        Dim strSQL As String = ""

        If IsValidSearchPishFaktor() = False Then
            Exit Sub
        End If

        If dsForm.Tables.Contains("tbl_PishFaktor") Then
            dsForm.Tables.Remove("tbl_PishFaktor")
        End If

        Try
            strSQL = "Sales.spPishFaktorGheireGhateei_SearchPishFaktor "

            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            cmSQL.Parameters.AddWithValue("CodeMahal", CodeMahalFaal)
            cmSQL.Parameters.AddWithValue("CodeDoreh", CodeDoreh)
            cmSQL.Parameters.AddWithValue("ccForoshandeh", cmbForoshandehS.SelectedValue)
            cmSQL.Parameters.AddWithValue("ccAnbar", cmbAnbar.SelectedValue)
            cmSQL.Parameters.AddWithValue("AzTarikh", mskAzTarikh.Text)
            cmSQL.Parameters.AddWithValue("TaTarikh", mskTaTarikh.Text)
            cmSQL.Parameters.AddWithValue("UserName", UserName)

            daSQl = New SqlDataAdapter(cmSQL)
            daSQl.Fill(dsForm, "tbl_PishFaktor")

            '------------Adding Columns------------
            dsForm.Tables("tbl_PishFaktor").Columns.Add("Taeed", GetType(Boolean))
            '--------------------------------------

            Dim dr As DataRow
            For Each dr In dsForm.Tables("tbl_PishFaktor").Rows
                dr("Taeed") = False
            Next

            dvPishFaktor = New DataView(dsForm.Tables("tbl_PishFaktor"))
            dvPishFaktor.Sort = "PishFaktorShomareh DESC"

            dvPishFaktor.AllowNew = False
            dvPishFaktor.AllowDelete = False
            dvPishFaktor.AllowEdit = True

            cmSQL = Nothing : daSQl = Nothing
            cnSQL.Close()

            SetGridPishFaktor()
            With GridEXPishFaktor
                .Visible = True
                .DataSource = Nothing
                .DataSource = dvPishFaktor
            End With

            Me.CenterToScreen()
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> SearchPishFaktor ")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> SearchPishFaktor ")
        End Try
    End Sub
    Private Function IsValidSearchPishFaktor() As Boolean
        IsValidSearchPishFaktor = False

        If Len(mskAzTarikh.Text.ToString) <> 0 Then
            If Not objTarikh.IsShDate(mskAzTarikh.Text.ToString) Then
                mskAzTarikh.Focus()
                Exit Function
            End If
            'If Microsoft.VisualBasic.Left(mskAzTarikh.Text, 4) <> mdlPublic.CodeDoreh Then
            '    MsgBox("از تاريخ وارد شده با دوره انتخاب شده مغايرت دارد.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Critical, "خطا")
            '    ErrPro.SetError(mskAzTarikh, "از تاريخ وارد شده با دوره انتخاب شده مغايرت دارد.")
            '    Exit Function
            'End If
        Else
            ErrPro.SetError(Me.mskAzTarikh, " از تاريخ را وارد نمایید.")
            MsgBox(" از تاریخ را وارد نمایید.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
            mskAzTarikh.Focus()
            Exit Function
        End If
        ErrPro.SetError(Me.mskAzTarikh, "")

        If Len(mskTaTarikh.Text.ToString) <> 0 Then
            If Not objTarikh.IsShDate(mskTaTarikh.Text.ToString) Then
                mskTaTarikh.Focus()
                Exit Function
            End If
            If Microsoft.VisualBasic.Left(mskTaTarikh.Text, 4) <> mdlPublic.CodeDoreh Then
                MsgBox("تا تاريخ وارد شده با دوره انتخاب شده مغايرت دارد.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Critical, "خطا")
                ErrPro.SetError(mskTaTarikh, "تا تاريخ وارد شده با دوره انتخاب شده مغايرت دارد.")
                Exit Function
            End If
        Else
            ErrPro.SetError(Me.mskTaTarikh, " تا تاريخ را وارد نمایید.")
            MsgBox(" تا تاریخ را وارد نمایید.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
            mskTaTarikh.Focus()
            Exit Function
        End If
        ErrPro.SetError(Me.mskTaTarikh, "")

        IsValidSearchPishFaktor = True
    End Function
    Private Sub SetGridPishFaktor()
        If dvPishFaktor.Count = 0 Then
            Exit Sub
        End If

        Try
            With GridEXPishFaktor
                .DataSource = Nothing
                .DataSource = dsForm.Tables("tbl_PishFaktor").DefaultView
                .SetDataBinding(dsForm.Tables("tbl_PishFaktor").DefaultView, "")
                .RetrieveStructure()
            End With

            For i As Integer = 0 To GridEXPishFaktor.CurrentTable.Columns.Count - 1
                GridEXPishFaktor.CurrentTable.Columns.Item(i).Visible = False
            Next

            GridEXPishFaktor.CurrentTable.Columns.Item("Taeed").Caption = "تاييد"
            GridEXPishFaktor.CurrentTable.Columns.Item("Taeed").Visible = True
            GridEXPishFaktor.CurrentTable.Columns.Item("Taeed").Width = 40
            GridEXPishFaktor.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXPishFaktor.CurrentTable.Columns.Item("Taeed").Position = 0
            GridEXPishFaktor.CurrentTable.Columns.Item("Taeed").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXPishFaktor.CurrentTable.Columns.Item("Taeed").HeaderAlignment = TextAlignment.Center

            GridEXPishFaktor.CurrentTable.Columns.Item("PishFaktorShomareh").Caption = "شماره"
            GridEXPishFaktor.CurrentTable.Columns.Item("PishFaktorShomareh").Visible = True
            GridEXPishFaktor.CurrentTable.Columns.Item("PishFaktorShomareh").Width = 70
            GridEXPishFaktor.CurrentTable.Columns.Item("PishFaktorShomareh").EditType = EditType.NoEdit
            GridEXPishFaktor.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXPishFaktor.CurrentTable.Columns.Item("PishFaktorShomareh").Position = 1
            GridEXPishFaktor.CurrentTable.Columns.Item("PishFaktorShomareh").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXPishFaktor.CurrentTable.Columns.Item("PishFaktorShomareh").HeaderAlignment = TextAlignment.Center

            GridEXPishFaktor.CurrentTable.Columns.Item("PishFaktorTarikhSlash").Caption = "تاريخ"
            GridEXPishFaktor.CurrentTable.Columns.Item("PishFaktorTarikhSlash").Visible = True
            GridEXPishFaktor.CurrentTable.Columns.Item("PishFaktorTarikhSlash").Width = 80
            GridEXPishFaktor.CurrentTable.Columns.Item("PishFaktorTarikhSlash").EditType = EditType.NoEdit
            GridEXPishFaktor.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXPishFaktor.CurrentTable.Columns.Item("PishFaktorTarikhSlash").Position = 2
            GridEXPishFaktor.CurrentTable.Columns.Item("PishFaktorTarikhSlash").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXPishFaktor.CurrentTable.Columns.Item("PishFaktorTarikhSlash").HeaderAlignment = TextAlignment.Center

            GridEXPishFaktor.CurrentTable.Columns.Item("NameMoshtary").Caption = "نام مشتری"
            GridEXPishFaktor.CurrentTable.Columns.Item("NameMoshtary").Visible = True
            GridEXPishFaktor.CurrentTable.Columns.Item("NameMoshtary").Width = 210
            GridEXPishFaktor.CurrentTable.Columns.Item("NameMoshtary").EditType = EditType.NoEdit
            GridEXPishFaktor.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXPishFaktor.CurrentTable.Columns.Item("NameMoshtary").Position = 3
            GridEXPishFaktor.CurrentTable.Columns.Item("NameMoshtary").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXPishFaktor.CurrentTable.Columns.Item("NameMoshtary").HeaderAlignment = TextAlignment.Center

            GridEXPishFaktor.CurrentTable.Columns.Item("Address").Caption = "آدرس مشتری"
            GridEXPishFaktor.CurrentTable.Columns.Item("Address").Visible = True
            GridEXPishFaktor.CurrentTable.Columns.Item("Address").Width = 320
            GridEXPishFaktor.CurrentTable.Columns.Item("Address").EditType = EditType.NoEdit
            GridEXPishFaktor.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXPishFaktor.CurrentTable.Columns.Item("Address").Position = 4
            GridEXPishFaktor.CurrentTable.Columns.Item("Address").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXPishFaktor.CurrentTable.Columns.Item("Address").HeaderAlignment = TextAlignment.Center

            GridEXPishFaktor.CurrentTable.Columns.Item("ccPishFaktorTitr").Caption = "ccPishFaktorTitr"
            GridEXPishFaktor.CurrentTable.Columns.Item("ccPishFaktorTitr").Visible = False
            GridEXPishFaktor.CurrentTable.Columns.Item("ccPishFaktorTitr").Width = 0
            GridEXPishFaktor.CurrentTable.Columns.Item("ccPishFaktorTitr").EditType = EditType.NoEdit
            GridEXPishFaktor.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXPishFaktor.CurrentTable.Columns.Item("ccPishFaktorTitr").Position = 5
            GridEXPishFaktor.CurrentTable.Columns.Item("ccPishFaktorTitr").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXPishFaktor.CurrentTable.Columns.Item("ccPishFaktorTitr").HeaderAlignment = TextAlignment.Center

            For i As Integer = 0 To GridEXPishFaktor.RootTable.Columns.Count - 1
                If GridEXPishFaktor.RootTable.Columns(i).Type.IsValueType Then
                    GridEXPishFaktor.RootTable.Columns(i).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                    GridEXPishFaktor.RootTable.Columns(i).FormatMode = Janus.Windows.GridEX.FormatMode.UseIFormattable
                    GridEXPishFaktor.RootTable.Columns(i).FormatString = "G"
                    GridEXPishFaktor.RootTable.Columns(i).TotalFormatMode = Janus.Windows.GridEX.FormatMode.UseIFormattable
                    GridEXPishFaktor.RootTable.Columns(i).TotalFormatString = "G"
                End If
            Next

            Me.CenterToScreen()
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> SetGridPishFaktor ")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> SetGridPishFaktor ")
        End Try
    End Sub
    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        SearchPishFaktor()
    End Sub
    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Dim strPishFaktor As String = ""
        Dim CountPishFaktor As Integer = 0
        Dim ShomarehFaktor As Integer = 0
        ShomarehFaktor = objTools.DLookup("ShomarehTafkik", "Sales.TafkikJoze_PishFaktor", "ccTafkik_GG = " & frm_ccTafkik_GG)

        Try
            For i As Integer = 0 To GridEXPishFaktor.RowCount - 1
                If GridEXPishFaktor.GetRows(i).Cells("Taeed").Value = True Then
                    strPishFaktor &= GridEXPishFaktor.GetRows(i).Cells("ccPishFaktorTitr").Text.Replace(",", "") & ","
                    CountPishFaktor += 1
                End If
            Next

            If strPishFaktor <> "" Then
                strPishFaktor = "," & strPishFaktor
                If AddPishFaktor(strPishFaktor) = True Then
                    MsgBox("تعداد " & CountPishFaktor & " عدد پیش فاکتور با موفقیت به تفکیک افزوده شد .", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "")
                    Me.Close()
                Else
                    Exit Sub
                End If
            Else
                MsgBox("برای صدور برگه تفکیک باید حداقل 1 پیش فاکتور انتخاب نمایید .", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "")
                Exit Sub
            End If

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> btnSodorTafkik_Click ")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> btnSodorTafkik_Click ")
        End Try
    End Sub
    Private Function AddPishFaktor(ByVal strPishFaktor As String) As Boolean
        AddPishFaktor = False

        Dim cnSQL As New SqlConnection
        Dim cmSQL As New SqlCommand
        Dim strSQL As String = ""
        Dim ShomarehTafkik As Integer = 0

        Try
            strSQL = "Sales.spPishFaktorGheireGhateei_AddPishFaktorInTafkik "

            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            cmSQL.Parameters.AddWithValue("strPishFaktor", strPishFaktor)
            cmSQL.Parameters.AddWithValue("ccTafkik_GG", frm_ccTafkik_GG)

            cmSQL.ExecuteNonQuery()

            cmSQL = Nothing
            cnSQL.Close()

            AddPishFaktor = True
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> AddPishFaktor ")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> AddPishFaktor ")
        End Try
    End Function
    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub
End Class