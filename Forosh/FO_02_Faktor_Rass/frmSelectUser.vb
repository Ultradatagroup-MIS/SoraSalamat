Public Class frmSelectUser
    Public CodeFard As Integer = 0
    Dim ErrPro As New ErrorProvider
    Dim dsForm As New DataSet
    Private Sub frmSelectUser_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        TextBox1.Text = ""
        LoadCombo()
        cmbFard_Other.SelectedIndex = -1
        cmbFard_Other.SelectedIndex = -1
    End Sub
    Private Sub LoadCombo()
        Dim Strsql As String : Dim daSQL As SqlDataAdapter
        Dim cnsql As SqlConnection
        Dim cmsql As SqlCommand


        If dsForm.Tables.Contains("tblFard") Then
            dsForm.Tables.Remove("tblFard")
        End If
        cnsql = New SqlConnection(ConnectionString)
        cnsql.Open()


        Strsql = "Select * From qryGL_MoshakhasatFardi Where CodeMahal = " & CodeMahalFaal & " and  CodeFard in (7194,7195,7212,7254,7269) AND CodeFard <> " & CodeFard & " AND sVazeiatEstekhdam = 4188  order by FN"
        daSQL = New SqlDataAdapter(Strsql, ConnectionString)
        daSQL.Fill(dsForm, "tblFard")
        cmbFard_Other.DataSource = Nothing
        cmbFard_Other.Items.Clear()
        cmbFard_Other.DataSource = dsForm.Tables("tblFard").DefaultView
        cmbFard_Other.DisplayMember = "FN"
        cmbFard_Other.ValueMember = "CodeFard"



        cmsql = Nothing : daSQL = Nothing
        cnsql.Close()

        daSQL = Nothing
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        cmbFard_Other.SelectedIndex = -1
        TextBox1.Text = ""

    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click


        If (IsNothing(cmbFard_Other.SelectedValue) Or cmbFard_Other.SelectedValue = 0) Then
            ErrPro.SetError(Me.cmbFard_Other, "پرسنل را انتخاب کنید")
            MsgBox("پرسنل را انتخاب کنید", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
            cmbFard_Other.Focus()
            Exit Sub
        End If
        ErrPro.SetError(Me.cmbFard_Other, "")


        Dim da As SqlDataAdapter = New SqlDataAdapter
        Using cn As New SqlConnection(ConnectionString)
            Using cm As SqlCommand = cn.CreateCommand()
                cn.Open()
                cm.Parameters.Clear()
                cm.CommandType = CommandType.StoredProcedure
                cm.CommandText = "[Treasury].[RaasGiri_Enteghal_OtherUser]"
                cm.Parameters.AddWithValue("ccTitr", ccTitr)
                cm.Parameters.AddWithValue("CodeFard", cmbFard_Other.SelectedValue)
                cm.ExecuteNonQuery()
            End Using
        End Using
        Me.close()
    End Sub


    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        If dsForm.Tables.Contains("tblFard") Then

            If cmbFard_Other.DataSource Is dsForm.Tables("tblFard").DefaultView Then
                'Me.dsForm.Tables("ComboShomarehHesab").DefaultView.Sort = "ShomarehHesabKamel"
                Me.dsForm.Tables("tblFard").DefaultView.RowFilter = "FN Like  '%" & TextBox1.Text.TrimEnd & "%'"
            End If
        End If
        If TextBox1.Text = "" Then
            cmbFard_Other.SelectedIndex = -1
        End If
    End Sub
End Class