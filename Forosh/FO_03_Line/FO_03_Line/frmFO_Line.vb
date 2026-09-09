Public Class frmFO_Line
#Region "Variable AND Constant Declration"
    Const cntCodeSubSystem As Long = 1000128
    Private SN As Integer
    Public dsForm As New DataSet
    Dim dvForm As DataView
    Dim txtCaption As String
    Private WithEvents BS As New UD_Dll.PassString
    Public dvTitr As DataView
    Public dvSatr As DataView
    Public cmTitr, cmSatr As CurrencyManager
    Dim ErrPro As New ErrorProvider
    Public AddNewLine As Boolean = False
    Dim flg As Boolean = False
#End Region
#Region "Form Event Code"
    Private Sub frmFO_Line_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetParameter()
        LoadCombo()
        SetLabelDelete(0, False)
        SearchTitr()
        cmbNoeSatr.SelectedIndex = 0
        objTools.ChangeKbd(UD_Dll.mdlUtility.KBD.Farsi)
        flg = True
    End Sub
    Private Sub lbLine_SelectedValueChanged(sender As Object, e As EventArgs) Handles lbLine.SelectedValueChanged
        cmbNoeSatr.SelectedIndex = 0
        txtSearch.Text = ""

        RefreshSatr()
    End Sub
    Private Sub cmbNoeSatr_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbNoeSatr.SelectedIndexChanged
        If cmbNoeSatr.SelectedIndex = 0 Then
            RefreshSatr()
        Else
            SearchSatr(cmbNoeSatr.SelectedIndex)
        End If
    End Sub
    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        If cmbNoeSatr.SelectedIndex = 0 Then
            RefreshSatr()
        Else
            SearchSatr(cmbNoeSatr.SelectedIndex)
        End If
    End Sub
    Private Sub lbKala_MouseEnter(sender As Object, e As EventArgs) Handles lbKala.MouseEnter
        SetLabelDelete(1, True)
    End Sub
    Private Sub lbKala_MouseLeave(sender As Object, e As EventArgs) Handles lbKala.MouseLeave
        SetLabelDelete(1, False)
    End Sub
    Private Sub lbMoshtary_MouseEnter(sender As Object, e As EventArgs) Handles lbMoshtary.MouseEnter
        SetLabelDelete(2, True)
    End Sub
    Private Sub lbMoshtary_MouseLeave(sender As Object, e As EventArgs) Handles lbMoshtary.MouseLeave
        SetLabelDelete(2, False)
    End Sub
    Private Sub lbForoshandeh_MouseEnter(sender As Object, e As EventArgs) Handles lbForoshandeh.MouseEnter
        SetLabelDelete(3, True)
    End Sub
    Private Sub lbForoshandeh_MouseLeave(sender As Object, e As EventArgs) Handles lbForoshandeh.MouseLeave
        SetLabelDelete(3, False)
    End Sub
    Private Sub lbKala_DoubleClick(sender As Object, e As EventArgs) Handles lbKala.DoubleClick
        DeleteSatr(1)
        SearchSatr(1)
    End Sub
    Private Sub lbMoshtary_DoubleClick(sender As Object, e As EventArgs) Handles lbMoshtary.DoubleClick
        DeleteSatr(2)
        SearchSatr(2)
    End Sub
    Private Sub lbForoshandeh_DoubleClick(sender As Object, e As EventArgs) Handles lbForoshandeh.DoubleClick
        DeleteSatr(3)
        SearchSatr(3)
    End Sub
#End Region
#Region "Global Form Code"
    Private Sub SetParameter()
        Dim commands As String = Microsoft.VisualBasic.Command()
        If commands.Length = 0 Then
            UserName = "administrator"
            UserPassWord = "66998833"
            NameMahalFaal = "تهران"
            CodeMahalFaal = "1"
            PersonelCode = "0"
            PersonelName = "Administrator"
            CodeDoreh = "1393"
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
            'ObjCode.UserName = UserName
        End If
    End Sub
    Private Sub LoadCombo()
        cmbNoeSatr.Items.Add("همـــه")
        cmbNoeSatr.Items.Add("کـــــالا")
        cmbNoeSatr.Items.Add("مشتـــری")
        cmbNoeSatr.Items.Add("فروشنـده")
    End Sub
    Private Sub SearchTitr()
        Dim cnSQL As New SqlConnection
        Dim cmSQL As New SqlCommand
        Dim daSQL As New SqlDataAdapter
        Dim strSQL As String = ""

        If dsForm.Tables.Contains("tbl_SearchTitr") Then
            dsForm.Tables.Remove("tbl_SearchTitr")
        End If

        Try
            strSQL = "Sales.spLine_SearchTitr "

            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            cmSQL.Parameters.AddWithValue("CodeMahal", CodeMahalFaal)

            daSQL = New SqlDataAdapter(cmSQL)
            daSQL.Fill(dsForm, "tbl_SearchTitr")

            With lbLine
                .DataSource = Nothing
                .DataSource = dsForm.Tables("tbl_SearchTitr").DefaultView
                .ValueMember = "ccLine"
                .DisplayMember = "SharhLine"
                .SelectedIndex = -1
            End With

            cmSQL = Nothing
            daSQL = Nothing
            cnSQL.Close()

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> SearchTitr")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> SearchTitr")
        End Try
    End Sub
    Private Sub SearchSatr(ByVal Type As Integer)
        '' 0 : All / 1 : Kala / 2 : Moshtary / 3 : Foroshandeh
        Dim ClearSearch As Integer = 1

        If flg = False Then
            Exit Sub
        End If

        If dsForm.Tables("tbl_SearchTitr").Rows.Count = 0 Then
            ClearSearch = 0
        End If

        Dim cnSQL As New SqlConnection
        Dim cmSQL As New SqlCommand
        Dim daSQL As New SqlDataAdapter
        Dim strSQL As String = ""

        If Type = 1 Then
            If dsForm.Tables.Contains("tbl_SearchKala") Then
                dsForm.Tables.Remove("tbl_SearchKala")
            End If
        ElseIf Type = 2 Then
            If dsForm.Tables.Contains("tbl_SearchMoshtary") Then
                dsForm.Tables.Remove("tbl_SearchMoshtary")
            End If
        ElseIf Type = 3 Then
            If dsForm.Tables.Contains("tbl_SearchForoshandeh") Then
                dsForm.Tables.Remove("tbl_SearchForoshandeh")
            End If
        End If

        Try
            strSQL = "Sales.spLine_SearchSatr "

            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            cmSQL.Parameters.AddWithValue("ccLine", IIf(ClearSearch = 0, 0, IIf(lbLine.SelectedValue = Nothing, 0, lbLine.SelectedValue)))
            cmSQL.Parameters.AddWithValue("Type", Type)
            cmSQL.Parameters.AddWithValue("txtSearch", txtSearch.Text.Trim)

            daSQL = New SqlDataAdapter(cmSQL)
            If Type = 1 Then
                daSQL.Fill(dsForm, "tbl_SearchKala")
            ElseIf Type = 2 Then
                daSQL.Fill(dsForm, "tbl_SearchMoshtary")
            ElseIf Type = 3 Then
                daSQL.Fill(dsForm, "tbl_SearchForoshandeh")
            End If

            If Type = 1 Then
                With lbKala
                    .DataSource = Nothing
                    .DataSource = dsForm.Tables("tbl_SearchKala").DefaultView
                    .ValueMember = "ccLineSatr"
                    .DisplayMember = "SharhLineSatr"
                    .SelectedIndex = -1
                End With
            ElseIf Type = 2 Then
                With lbMoshtary
                    .DataSource = Nothing
                    .DataSource = dsForm.Tables("tbl_SearchMoshtary").DefaultView
                    .ValueMember = "ccLineSatr"
                    .DisplayMember = "SharhLineSatr"
                    .SelectedIndex = -1
                End With
            ElseIf Type = 3 Then
                With lbForoshandeh
                    .DataSource = Nothing
                    .DataSource = dsForm.Tables("tbl_SearchForoshandeh").DefaultView
                    .ValueMember = "ccLineSatr"
                    .DisplayMember = "SharhLineSatr"
                    .SelectedIndex = -1
                End With
            End If

            cmSQL = Nothing
            daSQL = Nothing
            cnSQL.Close()

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> SearchSatr")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> SearchSatr")
        End Try
    End Sub
    Private Sub DeleteTitr()
        If objTools.DCount("ccPishFaktorTitr", "tblFO_PishFaktor", "ccLine = " & lbLine.SelectedValue) > 0 Then
            MsgBox("از این لاین استفاده شده است . امکان حذف آن وجود ندارد !", MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.OkOnly, "")
        Else
            objTools.DDelete("Sales.Line", "ccLine = " & lbLine.SelectedValue)
            SearchTitr()
        End If
    End Sub
    Private Sub DeleteSatr(ByVal Type As Integer)
        '' Type ---> 1 : Kala \ 2 : Moshtary \ 3 : Foroshandeh
        If Type = 1 Then
            If lbKala.SelectedValue Is Nothing Then
                Exit Sub
            End If

            objTools.DDelete("Sales.LineSatr", "ccLineSatr = " & lbKala.SelectedValue)
        ElseIf Type = 2 Then
            If lbMoshtary.SelectedValue Is Nothing Then
                Exit Sub
            End If

            objTools.DDelete("Sales.LineSatr", "ccLineSatr = " & lbMoshtary.SelectedValue)
        ElseIf Type = 3 Then
            If lbForoshandeh.SelectedValue Is Nothing Then
                Exit Sub
            End If

            objTools.DDelete("Sales.LineSatr", "ccLineSatr = " & lbForoshandeh.SelectedValue)
        End If
    End Sub
    Private Sub SetLabelDelete(ByVal Type As Integer, ByVal EL As Boolean)
        '' type ---> 0 : None / 1 : Kala / 2 : Moshtary / 3 : Foroshandeh
        '' EL ---> False : Leave / True : Enter
        lblKala.Visible = False
        lblMoshtary.Visible = False
        lblForoshandeh.Visible = False

        If Type = 1 Then
            If EL = False Then
                lblKala.Visible = False
            Else
                lblKala.Visible = True
            End If
        ElseIf Type = 2 Then
            If EL = False Then
                lblMoshtary.Visible = False
            Else
                lblMoshtary.Visible = True
            End If
        ElseIf Type = 3 Then
            If EL = False Then
                lblForoshandeh.Visible = False
            Else
                lblForoshandeh.Visible = True
            End If
        End If
    End Sub
    Private Sub RefreshSatr()
        SearchSatr(1)
        SearchSatr(2)
        SearchSatr(3)
    End Sub
#End Region
#Region "From Buttons "
    Private Sub btnRemoveTitr_Click(sender As Object, e As EventArgs) Handles btnRemoveTitr.Click
        DeleteTitr()
        RefreshSatr()
    End Sub
    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        cmbNoeSatr.SelectedIndex = 0
        txtSearch.Text = ""

        RefreshSatr()
    End Sub
    Private Sub btnAddTitr_Click(sender As Object, e As EventArgs) Handles btnAddTitr.Click
        Dim frm As New frmAddLineTitr

        AddNewLine = False

        Me.Hide()
        frm.ShowDialog()
        Me.Show()

        If AddNewLine = True Then
            SearchTitr()
        End If

    End Sub
    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub
    Private Sub btnAddSatr_Click(sender As Object, e As EventArgs) Handles btnAddSatr.Click
        Dim frm As New frmAddLineSatr

        If lbLine.SelectedValue = Nothing Then
            MsgBox("جهت افزودن جزئیات باید یک لاین را انتخاب نمایید  !", MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.OkOnly, "")
            Exit Sub
        End If

        frm.ccLine = lbLine.SelectedValue

        Me.Hide()
        frm.ShowDialog()
        Me.Show()

        RefreshSatr()

    End Sub
#End Region
#Region "From Stored Procedures and Reports "
    '' Sales.spLine_SearchTitr
    '' Sales.spLine_SearchSatr
#End Region
End Class
