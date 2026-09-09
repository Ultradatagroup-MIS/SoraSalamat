Imports Janus.Windows.GridEX

Public Class frmFO_VazeiatPishFaktor

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
    Public flg As Boolean = False
#End Region

    Private Sub frmFO_VazeiatPishFaktor_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetParameter()
        LoadCombo()

        cmbVazeiat.SelectedValue = 5000
        cmbVazeiat.SelectedValue = 5000

        mskAzTarikh.Text = TarikhEmrooz
        mskTaTarikh.Text = TarikhEmrooz

        Search()

        flg = True
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
            CodeDoreh = "1394"
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
            ObjCode.UserName = UserName
        End If
    End Sub
    Private Sub LoadCombo()

        Dim Strsql As String
        Dim cnSQL As New SqlConnection
        Dim cmSQL As New SqlCommand
        Dim p As New SqlParameter
        Dim daSQL As SqlDataAdapter

        cnSQL = New SqlConnection(ConnectionString)
        cnSQL.Open()

        '----- Load Combo Vazeiat PishFaktor
        Strsql = "Global.spFormDll_VazeiatPishFaktor_LoadCombo "

        cmSQL = New SqlCommand(Strsql, cnSQL)
        cmSQL.CommandType = CommandType.StoredProcedure
        cmSQL.Parameters.Clear()

        daSQL = New SqlDataAdapter(cmSQL)
        daSQL.Fill(dsForm, "tblVazeiat")
        cmbVazeiat.DataSource = Nothing
        cmbVazeiat.Items.Clear()
        cmbVazeiat.DataSource = dsForm.Tables("tblVazeiat").DefaultView
        cmbVazeiat.DisplayMember = "txtVazeiat"
        cmbVazeiat.ValueMember = "sVazeiat"

        cmSQL = Nothing : daSQL = Nothing
        cnSQL.Close()

    End Sub
    Private Sub Search()
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
            strSQL = "Global.spFormDll_VazeiatPishFaktor_SearchPishFaktor "

            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            cmSQL.Parameters.AddWithValue("sVazeiat", cmbVazeiat.SelectedValue)
            cmSQL.Parameters.AddWithValue("CodeMahal", CodeMahalFaal)
            cmSQL.Parameters.AddWithValue("CodeDoreh", CodeDoreh)
            cmSQL.Parameters.AddWithValue("AzTarikh", mskAzTarikh.Text)
            cmSQL.Parameters.AddWithValue("TaTarikh", mskTaTarikh.Text)
            cmSQL.Parameters.AddWithValue("Shomareh", IIf(txtShomarehPishFaktor.Text.Trim = "", 0, txtShomarehPishFaktor.Text.Trim))
            cmSQL.Parameters.AddWithValue("ccKala", txtCodeKala.Tag)

            daSQl = New SqlDataAdapter(cmSQL)
            daSQl.Fill(dsForm, "tbl_PishFaktor")

            dvPishFaktor = New DataView(dsForm.Tables("tbl_PishFaktor"))
            dvPishFaktor.Sort = "ShomarehPishFaktor ASC"

            dvPishFaktor.AllowNew = False
            dvPishFaktor.AllowDelete = False
            dvPishFaktor.AllowEdit = True

            cmSQL = Nothing : daSQl = Nothing
            cnSQL.Close()

            SetGrid()
            With GridEXPishFaktor
                .Visible = True
                .DataSource = Nothing
                .DataSource = dvPishFaktor
            End With

            Me.CenterToScreen()
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> Search ")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> Search ")
        End Try
    End Sub
    Private Function IsValidSearchPishFaktor() As Boolean
        IsValidSearchPishFaktor = False

        If Len(mskAzTarikh.Text.ToString) <> 0 Then
            If Not objTarikh.IsShDate(mskAzTarikh.Text.ToString) Then
                mskAzTarikh.Focus()
                Exit Function
            End If
            If Microsoft.VisualBasic.Left(mskAzTarikh.Text, 4) <> mdlPublic.CodeDoreh Then
                MsgBox("از تاريخ وارد شده با دوره انتخاب شده مغايرت دارد.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Critical, "خطا")
                ErrPro.SetError(mskAzTarikh, "از تاريخ وارد شده با دوره انتخاب شده مغايرت دارد.")
                Exit Function
            End If
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
    Private Sub SetGrid()
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

            GridEXPishFaktor.CurrentTable.Columns.Item("ShomarehPishFaktor").Caption = "شماره"
            GridEXPishFaktor.CurrentTable.Columns.Item("ShomarehPishFaktor").Visible = True
            GridEXPishFaktor.CurrentTable.Columns.Item("ShomarehPishFaktor").Width = 100
            GridEXPishFaktor.CurrentTable.Columns.Item("ShomarehPishFaktor").EditType = EditType.NoEdit
            GridEXPishFaktor.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXPishFaktor.CurrentTable.Columns.Item("ShomarehPishFaktor").Position = 0
            GridEXPishFaktor.CurrentTable.Columns.Item("ShomarehPishFaktor").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXPishFaktor.CurrentTable.Columns.Item("ShomarehPishFaktor").HeaderAlignment = TextAlignment.Center

            GridEXPishFaktor.CurrentTable.Columns.Item("TarikhPishFaktor").Caption = "تاريخ"
            GridEXPishFaktor.CurrentTable.Columns.Item("TarikhPishFaktor").Visible = True
            GridEXPishFaktor.CurrentTable.Columns.Item("TarikhPishFaktor").Width = 100
            GridEXPishFaktor.CurrentTable.Columns.Item("TarikhPishFaktor").EditType = EditType.NoEdit
            GridEXPishFaktor.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXPishFaktor.CurrentTable.Columns.Item("TarikhPishFaktor").Position = 1
            GridEXPishFaktor.CurrentTable.Columns.Item("TarikhPishFaktor").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXPishFaktor.CurrentTable.Columns.Item("TarikhPishFaktor").HeaderAlignment = TextAlignment.Center

            GridEXPishFaktor.CurrentTable.Columns.Item("txtVazeiat").Caption = "وضعیت"
            GridEXPishFaktor.CurrentTable.Columns.Item("txtVazeiat").Visible = True
            GridEXPishFaktor.CurrentTable.Columns.Item("txtVazeiat").Width = 550
            GridEXPishFaktor.CurrentTable.Columns.Item("txtVazeiat").EditType = EditType.NoEdit
            GridEXPishFaktor.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridEXPishFaktor.CurrentTable.Columns.Item("txtVazeiat").Position = 2
            GridEXPishFaktor.CurrentTable.Columns.Item("txtVazeiat").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GridEXPishFaktor.CurrentTable.Columns.Item("txtVazeiat").HeaderAlignment = TextAlignment.Center

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
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> SetGrid ")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> SetGrid ")
        End Try
    End Sub

    Private Sub cmbVazeiat_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbVazeiat.SelectedIndexChanged
        If flg = True Then
            Search()
        End If
    End Sub

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        Search()
    End Sub
    Private Sub txtCodeKala_TextChanged(sender As Object, e As EventArgs) Handles txtCodeKala.TextChanged
        Me.lblNameKala.Text = objTools.ConvertNulls(objTools.DLookup("NameKala", "tblAN_Kala", "Faal = 1 AND CodeKala = '" & Me.txtCodeKala.Text.Trim & "'"), "")
        Me.txtCodeKala.Tag = objTools.ConvertNulls(objTools.DLookup("ccKala", "tblAn_Kala", "Faal = 1 AND CodeKala = '" & Me.txtCodeKala.Text.Trim & "'"), 0)
    End Sub
End Class