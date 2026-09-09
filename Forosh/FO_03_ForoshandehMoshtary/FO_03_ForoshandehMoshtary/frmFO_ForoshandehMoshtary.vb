Imports System.Data.SqlClient
Imports System.Data.SqlClient.SqlConnection
Imports System.Data.Common
Imports System.Data
Public Class frmTakhsisMoshtaryBeForoshandeh

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub
#Region "Variable AND Constant Declration"
    Const cntCodeSubSystem As Long = 1000140
    Dim dsForm As New DataSet
    Dim dvForm As DataView
    Private SN As Integer
    Dim txtCaption As String
    Private WithEvents BS As New UD_Dll.PassString
    Dim FlgLoad As Boolean = False
#End Region
    Private Sub frmTakhsisMoshtaryBeForoshandeh_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetParameter()
        LoadComboForoshandeh()
        FlgLoad = True
        LoadLbMoshtary()
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
            CodeDoreh = "1394"
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

    Private Sub LoadComboForoshandeh()
        Dim Strsql As String
        Dim daSQL As New SqlDataAdapter
        Dim cn As New SqlConnection
        Dim cm As New SqlCommand

        If dsForm.Tables.Contains("tblForoshandehS") Then
            dsForm.Tables.Remove("tblForoshandehS")
        End If
        Try
            cn.ConnectionString = ConnectionString
            cn.Open()

            Strsql = "[Sales].[spForoshandehMoshtary_LoadComboForoshandeh]"

            cm = New SqlCommand(Strsql, cn)
            cm.CommandType = CommandType.StoredProcedure
            cm.Parameters.Clear()
            cm.Parameters.AddWithValue("CodeMahal", CodeMahalFaal)
            cm.Parameters.AddWithValue("sVazeiat", UD_Dll.Enums.FO_VaziatForoshandeh.NoFaal)
            cm.Parameters.AddWithValue("UserName", UserName)
            cm.Parameters.AddWithValue("txtNameForoshandeh", txtSearchForoshandeh.Text.Trim)
            daSQL = New SqlDataAdapter(cm)
            daSQL.Fill(dsForm, "tblForoshandehS")
            cmbBazaryabS.DataSource = Nothing
            cmbBazaryabS.Items.Clear()
            cmbBazaryabS.DataSource = dsForm.Tables("tblForoshandehS").DefaultView
            cmbBazaryabS.DisplayMember = "NameForoshandeh"
            cmbBazaryabS.ValueMember = "ccForoshandeh"

            cm = Nothing : daSQL = Nothing
            cn.Close()


        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> LoadCombo")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> LoadCombo")
        End Try
    End Sub
    Private Sub LoadLbForoshandehMoshtary()
        If FlgLoad = False Then
            Exit Sub
        End If

        Dim Strsql As String
        Dim daSQL As New SqlDataAdapter
        Dim cn As New SqlConnection
        Dim cmSql As New SqlCommand

        If dsForm.Tables.Contains("TableSearchMoshtary") Then
            dsForm.Tables.Remove("TableSearchMoshtary")
        End If

        Try
            cn.ConnectionString = ConnectionString
            cn.Open()

            Strsql = "[sales].[spForoshandehMoshtary_LoadMoshtary]"

            cmSql = New SqlCommand(Strsql, cn)
            cmSql.CommandType = CommandType.StoredProcedure
            cmSql.Parameters.Clear()
            cmSql.Parameters.AddWithValue("CodeMahal", CodeMahalFaal)
            cmSql.Parameters.AddWithValue("Foroshandeh", cmbBazaryabS.SelectedValue)
            cmSql.Parameters.AddWithValue("txtNameMoshtary", txtSearchMoshtaryan.Text.Trim)
            daSQL = New SqlDataAdapter(cmSql)
            daSQL.Fill(dsForm, "TableSearchMoshtary")
            With lbMoshtaryan
                .DataSource = Nothing
                .DataSource = dsForm.Tables("TableSearchMoshtary").DefaultView
                .ValueMember = "ccMoshtary"
                .DisplayMember = "NameMoshtary"
                .SelectedIndex = -1
            End With

            cmSql = Nothing : daSQL = Nothing
            cn.Close()

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> LoadLbMoshtary")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> LoadLbMoshtary")
        End Try
    End Sub
    Private Sub LoadLbMoshtary()
        If FlgLoad = False Then
            Exit Sub
        End If

        Dim Strsql As String
        Dim daSQL As New SqlDataAdapter
        Dim cn As New SqlConnection
        Dim cmSql As New SqlCommand

        If dsForm.Tables.Contains("TableSearchMoshtary") Then
            dsForm.Tables.Remove("TableSearchMoshtary")
        End If

        Try
            cn.ConnectionString = ConnectionString
            cn.Open()

            Strsql = "sales.spForoshandehMoshtary_LoadMoshtary"

            cmSql = New SqlCommand(Strsql, cn)
            cmSql.CommandType = CommandType.StoredProcedure
            cmSql.Parameters.Clear()
            cmSql.Parameters.AddWithValue("CodeMahal", CodeMahalFaal)
            cmSql.Parameters.AddWithValue("Foroshandeh", IIf(cmbBazaryabS.SelectedValue = Nothing, 0, cmbBazaryabS.SelectedValue))
            cmSql.Parameters.AddWithValue("txtNameMoshtary", txtSearchMoshtaryan.Text.Trim)
            daSQL = New SqlDataAdapter(cmSql)
            daSQL.Fill(dsForm, "TableSearchMoshtary")
            With lbMoshtaryan
                .DataSource = Nothing
                .DataSource = dsForm.Tables("TableSearchMoshtary").DefaultView
                .ValueMember = "ccMoshtary"
                .DisplayMember = "NameMoshtary"
                .SelectedIndex = -1
            End With

            cmSql = Nothing : daSQL = Nothing
            cn.Close()

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> LoadLbMoshtary")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> LoadLbMoshtary")
        End Try
    End Sub
    Private Sub InsertLbMoshtaryForoshandeh()
        Dim Strsql As String
        Dim daSQL As New SqlDataAdapter
        Dim cn As New SqlConnection
        Dim cmSql As New SqlCommand

        If dsForm.Tables.Contains("TableInsertMoshtaryForoshandeh") Then
            dsForm.Tables.Remove("TableInsertMoshtaryForoshandeh")
        End If

        Try
            cn.ConnectionString = ConnectionString
            cn.Open()

            Strsql = "Sales.spForoshandehMoshtary_InsertMoshtary"

            cmSql = New SqlCommand(Strsql, cn)
            cmSql.CommandType = CommandType.StoredProcedure
            cmSql.Parameters.Clear()
            cmSql.Parameters.AddWithValue("ccMoshtary", lbMoshtaryan.SelectedValue)
            cmSql.Parameters.AddWithValue("Foroshandeh", cmbBazaryabS.SelectedValue)
            cmSql.ExecuteNonQuery()
            cmSql = Nothing : daSQL = Nothing
            cn.Close()
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> InsertLbMoshtaryForoshandeh")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> InsertLbMoshtaryForoshandeh")
        End Try
    End Sub
    Private Sub LoadLbMoshtaryForoshandeh()
        If FlgLoad = False Then
            Exit Sub
        End If

        Dim Strsql As String
        Dim daSQL As New SqlDataAdapter
        Dim cn As New SqlConnection
        Dim cmSql As New SqlCommand

        If dsForm.Tables.Contains("TableSearchMoshtaryForoshandeh") Then
            dsForm.Tables.Remove("TableSearchMoshtaryForoshandeh")
        End If

        Try
            cn.ConnectionString = ConnectionString
            cn.Open()

            Strsql = "Sales.spForoshandehMoshtary_LoadMoshtaryForoshandeh"

            cmSql = New SqlCommand(Strsql, cn)
            cmSql.CommandType = CommandType.StoredProcedure
            cmSql.Parameters.Clear()
            cmSql.Parameters.AddWithValue("CodeMahal", CodeMahalFaal)
            cmSql.Parameters.AddWithValue("Foroshandeh", IIf(cmbBazaryabS.SelectedValue = Nothing, 0, cmbBazaryabS.SelectedValue))
            cmSql.Parameters.AddWithValue("txtNameMoshtaryFO", txtSearchMoshtaryanForoshandeh.Text.Trim)
            daSQL = New SqlDataAdapter(cmSql)
            daSQL.Fill(dsForm, "TableSearchMoshtaryForoshandeh")
            With lbMoshtaryanForoshandeh
                .DataSource = Nothing
                .DataSource = dsForm.Tables("TableSearchMoshtaryForoshandeh").DefaultView
                .ValueMember = "ccMoshtary"
                .DisplayMember = "NameMoshtary"
                .SelectedIndex = -1
            End With
            cmSql = Nothing : daSQL = Nothing
            cn.Close()

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> LoadLbMoshtaryForoshandeh")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> LoadLbMoshtaryForoshandeh")
        End Try
    End Sub

    Private Sub cmbBazaryabS_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbBazaryabS.SelectedIndexChanged
        LoadLbMoshtary()
        LoadLbMoshtaryForoshandeh()
    End Sub

    Private Sub txtSearchForoshandeh_KeyPress(sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtSearchForoshandeh.KeyPress
        If Asc(e.KeyChar) = 13 Then
            LoadComboForoshandeh()
        End If
    End Sub

    Private Sub txtSearchMoshtaryan_TextChanged(sender As Object, e As EventArgs) Handles txtSearchMoshtaryan.TextChanged
        LoadLbMoshtary()
    End Sub

    Private Sub txtSearchMoshtaryanForoshandeh_TextChanged(sender As Object, e As EventArgs) Handles txtSearchMoshtaryanForoshandeh.TextChanged
        LoadLbMoshtaryForoshandeh()
    End Sub

    Private Sub btnNext1_Click(sender As Object, e As EventArgs) Handles btnNext1.Click
        If lbMoshtaryan.Items.Count <> 0 Then
            InsertLbMoshtaryForoshandeh()
            LoadLbMoshtary()
            LoadLbMoshtaryForoshandeh()
        End If
    End Sub

    Private Sub lbMoshtaryan_DoubleClick(sender As Object, e As EventArgs) Handles lbMoshtaryan.DoubleClick
        If lbMoshtaryan.Items.Count <> 0 Then
            InsertLbMoshtaryForoshandeh()
            LoadLbMoshtary()
            LoadLbMoshtaryForoshandeh()
        End If
    End Sub

    Private Sub btnBack1_Click(sender As Object, e As EventArgs) Handles btnBack1.Click
        If lbMoshtaryanForoshandeh.Items.Count <> 0 Then
            objTools.DDelete("tblFO_ForoshandehMoshtary", "ccForoshandeh = " & cmbBazaryabS.SelectedValue & " AND ccMoshtary = " & lbMoshtaryanForoshandeh.SelectedValue)
            LoadLbMoshtary()
            LoadLbMoshtaryForoshandeh()
        End If
    End Sub

    Private Sub lbMoshtaryanForoshandeh_DoubleClick(sender As Object, e As EventArgs) Handles lbMoshtaryanForoshandeh.DoubleClick
        If lbMoshtaryanForoshandeh.Items.Count <> 0 Then
            objTools.DDelete("tblFO_ForoshandehMoshtary", "ccForoshandeh = " & cmbBazaryabS.SelectedValue & " AND ccMoshtary = " & lbMoshtaryanForoshandeh.SelectedValue)
            LoadLbMoshtary()
            LoadLbMoshtaryForoshandeh()
        End If
    End Sub
End Class
