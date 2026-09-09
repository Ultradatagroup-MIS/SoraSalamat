Imports System.Data
Imports System.Data.SqlClient
Imports Microsoft.VisualBasic
Imports System.IO
Public Class frmFO_EkhtesaseGorohKala
    Private Sub frmFO_EkhtesaseGorohKala_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetParameter()
        LoadCombo()
        ClearForm()
    End Sub
    Private Sub LoadCombo()
        FlagSearch = False
        Dim dt As Data.DataTable = Nothing
        Dim da As SqlDataAdapter = New SqlDataAdapter
        Try
            '-------------- Load Combo GorohKala1
            Using cn As New SqlConnection(ConnectionString)
                Using cm As SqlCommand = cn.CreateCommand()
                    cn.Open()
                    cm.Parameters.Clear()
                    cm.CommandType = CommandType.StoredProcedure
                    cm.CommandText = "[Global].[spGorohKala_1_LoadCombo]"
                    da.SelectCommand = cm
                    dt = New DataTable
                    da.Fill(dt)
                End Using
            End Using
            cmbGoroh1.DataSource = dt
            cmbGoroh1.DisplayMember = "Sharh"
            cmbGoroh1.ValueMember = "Code"

            '-------------- Load Liste Kala
            dt = Nothing
            Using cn As New SqlConnection(ConnectionString)
                Using cm As SqlCommand = cn.CreateCommand()
                    cn.Open()
                    cm.Parameters.Clear()
                    cm.CommandType = CommandType.StoredProcedure
                    cm.CommandText = "[Global].[spKala_LoadCombo]"
                    da.SelectCommand = cm
                    dt = New DataTable
                    da.Fill(dt)
                End Using
            End Using
            chlKala_All.DataSource = dt
            chlKala_All.DisplayMember = "NameKala"
            chlKala_All.ValueMember = "ccKala"

            lblTedadKala_All.Text = dt.Rows.Count
        Catch ex As Exception
            Throw New Exception("Error In--> Load Combo : " & ex.Message)
        Finally
        End Try
    End Sub
    Private Sub ClearForm()
        cmbGoroh1.SelectedIndex = -1
        cmbGoroh2.SelectedIndex = -1
        cmbGoroh3.SelectedIndex = -1
        cmbGoroh4.SelectedIndex = -1
        cmbGoroh5.SelectedIndex = -1
        FlagSearch = True
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
            CodeDoreh = "1395"

        Else
            UserName = commands.Split(";")(0)
            UserCode = commands.Split(";")(1)
            UserPassWord = commands.Split(";")(2)
            NameMahalFaal = commands.Split(";")(3)
            CodeMahalFaal = commands.Split(";")(4)
            PersonelCode = commands.Split(";")(5)
            PersonelName = commands.Split(";")(6)
            CodeDoreh = commands.Split(";")(7)

        End If
    End Sub
    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        If FlagSearch = True Then
            Dim dt As Data.DataTable = Nothing
            Dim da As SqlDataAdapter = New SqlDataAdapter

            '-------------- Search Kala
                Using cn As New SqlConnection(ConnectionString)
                    Using cm As SqlCommand = cn.CreateCommand()
                        cn.Open()
                        cm.Parameters.Clear()
                        cm.CommandType = CommandType.StoredProcedure
                    cm.CommandText = "[Global].[spKala_LoadCombo_Search]"
                    cm.Parameters.AddWithValue("Str", txtSearch.Text.Trim)
                        da.SelectCommand = cm
                        dt = New DataTable
                        da.Fill(dt)
                    End Using
                End Using
            chlKala_All.DataSource = dt
            chlKala_All.DisplayMember = "NameKala"
            chlKala_All.ValueMember = "ccKala"

            lblTedadKala_All.Text = dt.Rows.Count
        End If
    End Sub
    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        chlKala_All.DataSource = Nothing
        chlKala_Ekhtesas.DataSource = Nothing
        lblTedadKala_All.Text = "0"
        lblTedadKala_Ekhtesas.Text = "0"

        LoadCombo()
        ClearForm()
    End Sub
    Private Sub LoadGorohKala(Noe As Integer, CodeLink As Double)
        Select Case (Noe)
            Case 2
                Search_Goroh("[Global].[spGorohKala_2_LoadCombo]", CodeLink, cmbGoroh2)
            Case 3
                Search_Goroh("[Global].[spGorohKala_3_LoadCombo]", CodeLink, cmbGoroh3)
            Case 4
                Search_Goroh("[Global].[spGorohKala_4_LoadCombo]", CodeLink, cmbGoroh4)
            Case 5
                Search_Goroh("[Global].[spGorohKala_5_LoadCombo]", CodeLink, cmbGoroh5)
        End Select
    End Sub
    Private Sub LoadListKala(Noe As Integer)
        Select Case (Noe)
            Case 1
                Search_Kala("[Global].[spKala_SearchGoroh_ALL]", 1, chlKala_All, 0, 0, 0, 0, 0)
                Search_Kala("[Global].[spKala_SearchGoroh_Ekhtesas]", 1, chlKala_Ekhtesas, cmbGoroh1.SelectedValue, 0, 0, 0, 0)
            Case 2
                Search_Kala("[Global].[spKala_SearchGoroh_ALL]", 2, chlKala_All, cmbGoroh1.SelectedValue, 0, 0, 0, 0)
                Search_Kala("[Global].[spKala_SearchGoroh_Ekhtesas]", 2, chlKala_Ekhtesas, cmbGoroh1.SelectedValue, cmbGoroh2.SelectedValue, 0, 0, 0)
            Case 3
                Search_Kala("[Global].[spKala_SearchGoroh_ALL]", 3, chlKala_All, cmbGoroh1.SelectedValue, cmbGoroh2.SelectedValue, 0, 0, 0)
                Search_Kala("[Global].[spKala_SearchGoroh_Ekhtesas]", 3, chlKala_Ekhtesas, cmbGoroh1.SelectedValue, cmbGoroh2.SelectedValue, cmbGoroh3.SelectedValue, 0, 0)
            Case 4
                Search_Kala("[Global].[spKala_SearchGoroh_ALL]", 4, chlKala_All, cmbGoroh1.SelectedValue, cmbGoroh2.SelectedValue, cmbGoroh3.SelectedValue, 0, 0)
                Search_Kala("[Global].[spKala_SearchGoroh_Ekhtesas]", 4, chlKala_Ekhtesas, cmbGoroh1.SelectedValue, cmbGoroh2.SelectedValue, cmbGoroh3.SelectedValue, cmbGoroh4.SelectedValue, 0)
            Case 5
                Search_Kala("[Global].[spKala_SearchGoroh_ALL]", 5, chlKala_All, cmbGoroh1.SelectedValue, cmbGoroh2.SelectedValue, cmbGoroh3.SelectedValue, cmbGoroh4.SelectedValue, 0)
                Search_Kala("[Global].[spKala_SearchGoroh_Ekhtesas]", 5, chlKala_Ekhtesas, cmbGoroh1.SelectedValue, cmbGoroh2.SelectedValue, cmbGoroh3.SelectedValue, cmbGoroh4.SelectedValue, cmbGoroh5.SelectedValue)
        End Select
    End Sub
    Private Sub Search_Goroh(spName As String, CodeLink As Double, cmb As ComboBox)
        Dim dt As Data.DataTable = Nothing
        Dim da As SqlDataAdapter = New SqlDataAdapter
        cmb.DataSource = Nothing

        Using cn As New SqlConnection(ConnectionString)
            Using cm As SqlCommand = cn.CreateCommand()
                cn.Open()
                cm.Parameters.Clear()
                cm.CommandType = CommandType.StoredProcedure
                cm.CommandText = spName
                cm.Parameters.AddWithValue("CodeLink", CodeLink)
                da.SelectCommand = cm
                dt = New DataTable
                da.Fill(dt)
            End Using
        End Using
        cmb.DataSource = dt
        cmb.DisplayMember = "Sharh"
        cmb.ValueMember = "Code"
        cmb.SelectedIndex = -1
    End Sub
    Private Sub cmbGoroh1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbGoroh1.SelectedIndexChanged
        If FlagSearch = True And flg = True Then
            flg = False
            LoadGorohKala(2, cmbGoroh1.SelectedValue)
            LoadListKala(1)
            NoeSearch = 1
            cmbGoroh2.SelectedIndex = -1
            cmbGoroh3.SelectedIndex = -1
            cmbGoroh4.SelectedIndex = -1
            cmbGoroh5.SelectedIndex = -1
            flg = True
        End If
    End Sub
    Private Sub cmbGoroh2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbGoroh2.SelectedIndexChanged
        If FlagSearch = True And flg = True Then
            flg = False
            LoadGorohKala(3, cmbGoroh2.SelectedValue)
            LoadListKala(2)
            NoeSearch = 2
            cmbGoroh3.SelectedIndex = -1
            cmbGoroh4.SelectedIndex = -1
            cmbGoroh5.SelectedIndex = -1
            flg = True
        End If
    End Sub
    Private Sub cmbGoroh3_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbGoroh3.SelectedIndexChanged
        If FlagSearch = True And flg = True Then
            flg = False
            LoadGorohKala(4, cmbGoroh2.SelectedValue)
            LoadListKala(3)
            NoeSearch = 3
            cmbGoroh4.SelectedIndex = -1
            cmbGoroh5.SelectedIndex = -1
            flg = True
        End If
    End Sub
    Private Sub cmbGoroh4_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbGoroh4.SelectedIndexChanged
        If FlagSearch = True And flg = True Then
            flg = False
            LoadGorohKala(5, cmbGoroh3.SelectedValue)
            LoadListKala(4)
            NoeSearch = 4
            cmbGoroh5.SelectedIndex = -1
            flg = True
        End If
    End Sub
    Private Sub Search_Kala(spName As String, Noe As Integer, chl As CheckedListBox, sg1 As Double, sg2 As Double, sg3 As Double, sg4 As Double, sg5 As Double)
        Dim dt As Data.DataTable = Nothing
        Dim da As SqlDataAdapter = New SqlDataAdapter
        chl.DataSource = Nothing

        Using cn As New SqlConnection(ConnectionString)
            Using cm As SqlCommand = cn.CreateCommand()
                cn.Open()
                cm.Parameters.Clear()
                cm.CommandType = CommandType.StoredProcedure
                cm.CommandText = spName
                cm.Parameters.AddWithValue("Noe", Noe)
                cm.Parameters.AddWithValue("SG1", sg1)
                cm.Parameters.AddWithValue("SG2", sg2)
                cm.Parameters.AddWithValue("SG3", sg3)
                cm.Parameters.AddWithValue("SG4", sg4)
                cm.Parameters.AddWithValue("SG5", sg5)
                da.SelectCommand = cm
                dt = New DataTable
                da.Fill(dt)
            End Using
        End Using
        chl.DataSource = dt
        If dt.Rows.Count > 0 Then
            chl.DisplayMember = "NameKala"
            chl.ValueMember = "ccKala"
            If spName = "[Global].[spKala_SearchGoroh_ALL]" Then
                lblTedadKala_All.Text = dt.Rows.Count
            Else
                lblTedadKala_Ekhtesas.Text = dt.Rows.Count
            End If
        Else
            If spName = "[Global].[spKala_SearchGoroh_ALL]" Then
                lblTedadKala_All.Text = 0
            Else
                lblTedadKala_Ekhtesas.Text = 0
            End If
        End If
    End Sub

    Private Sub cmbGoroh5_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbGoroh5.SelectedIndexChanged
        If FlagSearch = True And flg = True Then
            flg = False
            LoadListKala(5)
            NoeSearch = 5
            flg = True
        End If
    End Sub

    Private Sub btnSelectAll_Click(sender As Object, e As EventArgs) Handles btnSelectAll.Click
        If chlKala_All.Items.Count > 0 Then
            If flgSelect = True Then
                For i As Integer = 0 To chlKala_All.Items.Count - 1
                    chlKala_All.SetSelected(i, True)
                    chlKala_All.SetItemChecked(i, True)
                Next
                flgSelect = False
            ElseIf flgSelect = False Then
                    For i As Integer = 0 To chlKala_All.Items.Count - 1
                        chlKala_All.SetSelected(i, True)
                        chlKala_All.SetItemChecked(i, False)
                    Next
                    flgSelect = True
            End If
        End If
    End Sub

    Private Sub btnTransfer_Click(sender As Object, e As EventArgs) Handles btnTransfer.Click
        If cmbGoroh1.SelectedIndex = -1 Then
            MsgBox("ابتدا گروع کالا را انتخاب کنید", MsgBoxStyle.Information, "خطا")
            Exit Sub
        End If
        Dim SG_Check As Double = 0
        Dim ccKala As Integer = 0
        Dim Str As String = " , "
        Dim NKala As String = ""
        For i As Integer = 0 To chlKala_All.Items.Count - 1
            chlKala_All.SetSelected(i, True)
            If chlKala_All.GetItemChecked(i) = True Then
                ccKala = chlKala_All.SelectedValue
                Select Case NoeSearch
                    Case 1
                        SG_Check = objTools.ConvertNulls(objTools.DLookup("Sg1", "tblan_Kala", "ccKala = " & ccKala), 0)
                        If SG_Check <> 0 Then
                            NKala = objTools.DLookup("NameKala", "tblan_Kala", "ccKala= " & ccKala)
                            Str &= " (" & NKala & ") "
                        Else
                            objTools.DUpdate("sg1", "tblan_Kala", cmbGoroh1.SelectedValue, "ccKala = " & ccKala)
                        End If
                    Case 2
                        SG_Check = objTools.ConvertNulls(objTools.DLookup("Sg2", "tblan_Kala", "ccKala = " & ccKala), 0)
                        If SG_Check <> 0 Then
                            NKala = objTools.DLookup("NameKala", "tblan_Kala", "ccKala= " & ccKala)
                            Str &= " (" & NKala & ") "
                        Else
                            objTools.DUpdate("sg2", "tblan_Kala", cmbGoroh2.SelectedValue, "ccKala = " & ccKala)
                        End If
                    Case 3
                        SG_Check = objTools.ConvertNulls(objTools.DLookup("Sg3", "tblan_Kala", "ccKala = " & ccKala), 0)
                        If SG_Check <> 0 Then
                            NKala = objTools.DLookup("NameKala", "tblan_Kala", "ccKala= " & ccKala)
                            Str &= " (" & NKala & ") "
                        Else
                            objTools.DUpdate("sg3", "tblan_Kala", cmbGoroh3.SelectedValue, "ccKala = " & ccKala)
                        End If
                    Case 4
                        SG_Check = objTools.ConvertNulls(objTools.DLookup("Sg4", "tblan_Kala", "ccKala = " & ccKala), 0)
                        If SG_Check <> 0 Then
                            NKala = objTools.DLookup("NameKala", "tblan_Kala", "ccKala= " & ccKala)
                            Str &= " (" & NKala & ") "
                        Else
                            objTools.DUpdate("sg4", "tblan_Kala", cmbGoroh4.SelectedValue, "ccKala = " & ccKala)
                        End If
                    Case 5
                        SG_Check = objTools.ConvertNulls(objTools.DLookup("Sg5", "tblan_Kala", "ccKala = " & ccKala), 0)
                        If SG_Check <> 0 Then
                            NKala = objTools.DLookup("NameKala", "tblan_Kala", "ccKala= " & ccKala)
                            Str &= " (" & NKala & ") "
                        Else
                            objTools.DUpdate("sg5", "tblan_Kala", cmbGoroh5.SelectedValue, "ccKala = " & ccKala)
                        End If
                End Select

            End If
        Next
        If Str <> " , " Then
            MsgBox("به این کالاها قبلا گروه کالای " & NoeSearch & " تعلق گرفته است و امکان تغییر آن را ندارید: " & Str, MsgBoxStyle.OkOnly + MsgBoxStyle.Critical _
                    , "پیام")
        End If
        LoadListKala(NoeSearch)
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If chlKala_Ekhtesas.SelectedItems.Count = 0 Then Exit Sub
        MsgBox("آیا گروه کالا های انتخابی حذف شود؟", MsgBoxStyle.YesNo + MsgBoxStyle.Critical, "پیام")
        If MsgBoxResult.Yes Then
            For i As Integer = 0 To chlKala_Ekhtesas.Items.Count - 1
                chlKala_Ekhtesas.SetSelected(i, True)
                If chlKala_Ekhtesas.GetItemChecked(i) Then
                    Select Case NoeSearch
                        Case 1
                            objTools.DUpdate("sG1", "tblan_Kala", "0", "ccKala = " & chlKala_Ekhtesas.SelectedValue)
                        Case 2
                            objTools.DUpdate("sG2", "tblan_Kala", "0", "ccKala = " & chlKala_Ekhtesas.SelectedValue)
                        Case 3
                            objTools.DUpdate("sG3", "tblan_Kala", "0", "ccKala = " & chlKala_Ekhtesas.SelectedValue)
                        Case 4
                            objTools.DUpdate("sG4", "tblan_Kala", "0", "ccKala = " & chlKala_Ekhtesas.SelectedValue)
                        Case 5
                            objTools.DUpdate("sG5", "tblan_Kala", "0", "ccKala = " & chlKala_Ekhtesas.SelectedValue)
                    End Select
                End If
            Next

            Select Case NoeSearch
                Case 1
                    Search_Kala("[Global].[spKala_SearchGoroh_ALL]", 1, chlKala_All, cmbGoroh1.SelectedValue, 0, 0, 0, 0)
                    Search_Kala("[Global].[spKala_SearchGoroh_Ekhtesas]", 1, chlKala_Ekhtesas, cmbGoroh1.SelectedValue, 0, 0, 0, 0)
                Case 2
                    Search_Kala("[Global].[spKala_SearchGoroh_ALL]", 2, chlKala_All, cmbGoroh1.SelectedValue, cmbGoroh2.SelectedValue, 0, 0, 0)
                    Search_Kala("[Global].[spKala_SearchGoroh_Ekhtesas]", 2, chlKala_Ekhtesas, cmbGoroh1.SelectedValue, cmbGoroh2.SelectedValue, 0, 0, 0)
                Case 3
                    Search_Kala("[Global].[spKala_SearchGoroh_ALL]", 3, chlKala_All, cmbGoroh1.SelectedValue, cmbGoroh2.SelectedValue, cmbGoroh3.SelectedValue, 0, 0)
                    Search_Kala("[Global].[spKala_SearchGoroh_Ekhtesas]", 3, chlKala_Ekhtesas, cmbGoroh1.SelectedValue, cmbGoroh2.SelectedValue, cmbGoroh3.SelectedValue, 0, 0)
                Case 4
                    Search_Kala("[Global].[spKala_SearchGoroh_ALL]", 4, chlKala_All, cmbGoroh1.SelectedValue, cmbGoroh2.SelectedValue, cmbGoroh3.SelectedValue, cmbGoroh4.SelectedValue, 0)
                    Search_Kala("[Global].[spKala_SearchGoroh_Ekhtesas]", 4, chlKala_Ekhtesas, cmbGoroh1.SelectedValue, cmbGoroh2.SelectedValue, cmbGoroh3.SelectedValue, cmbGoroh4.SelectedValue, 0)
                Case 5
                    Search_Kala("[Global].[spKala_SearchGoroh_ALL]", 5, chlKala_All, cmbGoroh1.SelectedValue, cmbGoroh2.SelectedValue, cmbGoroh3.SelectedValue, cmbGoroh4.SelectedValue, cmbGoroh5.SelectedValue)
                    Search_Kala("[Global].[spKala_SearchGoroh_Ekhtesas]", 5, chlKala_Ekhtesas, cmbGoroh1.SelectedValue, cmbGoroh2.SelectedValue, cmbGoroh3.SelectedValue, cmbGoroh4.SelectedValue, cmbGoroh5.SelectedValue)
            End Select

        End If

    End Sub
End Class
