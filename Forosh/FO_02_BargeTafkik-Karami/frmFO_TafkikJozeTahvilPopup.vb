Public Class frmFO_TafkikJozeTahvilPopup

#Region "Variable AND Constant Declration"
    Public dsForm As New DataSet
    Public NameMamorPakhsh As String
    Public NameMamorTozi As String
    Public ccRanandeh_Tozie As Integer
    Public ccMashin_Tozie As Integer
    Public ccMamorPakhsh As Integer
    Public ShTafkik As Integer
    Public ccTafkikJoze As Integer
    Public TarikhErsal As String
    Public svazeiat As Integer
    Dim dvForm As DataView
    Dim txtCaption As String
    Private WithEvents BS As New UD_Dll.PassString
    Dim ErrPro As New ErrorProvider
#End Region
#Region "Form Event Code"
    Private Sub frmFO_TafkikJozeTahvilPopup_Closed(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Closed
        dsForm = Nothing
        dvForm = Nothing
    End Sub

    Private Sub frmFO_TafkikJozeTahvilPopup_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{tab}")
        End If
    End Sub
    Private Sub frmFO_TafkikJozeTahvilPopup_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load


        Me.Text = "تحویل موزع "
        txtM.Text = NameMamorPakhsh
        mskTarikhTahvil.Text = TarikhEmrooz

 
    End Sub
#End Region
#Region "Global Form Code"
    'Private Sub LoadCombo()

    '    Dim Strsql As String
    '    Dim daSQL As SqlDataAdapter

    '    Strsql = "Select * From qryFO_Mashin Where CodeMahal = " & CodeMahalFaal & " AND sVazeiat = 4073"
    '    daSQL = New SqlDataAdapter(Strsql, ConnectionString)
    '    daSQL.Fill(dsForm, "tblMashin2")
    '    cmbMashinTozie.DataSource = Nothing
    '    cmbMashinTozie.Items.Clear()
    '    cmbMashinTozie.DataSource = dsForm.Tables("tblMashin2").DefaultView
    '    cmbMashinTozie.DisplayMember = "MashinFull"
    '    cmbMashinTozie.ValueMember = "ccMashin"
    '    cmbMashinTozie.SelectedValue = ccMashin_Tozie
    '    cmbMashinTozie.SelectedValue = ccMashin_Tozie

    '    Strsql = "Select * From qryGL_MoshakhasatFardi Where CodeMahal = " & CodeMahalFaal & " AND sVazeiatEstekhdam = 4188 And "
    '    Strsql &= " sSemat in (" & UD_Dll.Enums.GL_Semat.MamorPakhsh & "," & UD_Dll.Enums.GL_Semat.Ranandeh & "," & UD_Dll.Enums.GL_Semat.Foroshandeh_Sayar & ")"
    '    daSQL = New SqlDataAdapter(Strsql, ConnectionString)
    '    daSQL.Fill(dsForm, "tblRanandeh2")
    '    cmbRanandehTozie.DataSource = Nothing
    '    cmbRanandehTozie.Items.Clear()
    '    cmbRanandehTozie.DataSource = dsForm.Tables("tblRanandeh2").DefaultView
    '    cmbRanandehTozie.DisplayMember = "FN"
    '    cmbRanandehTozie.ValueMember = "CodeFard"
    '    cmbRanandehTozie.SelectedValue = ccRanandeh_Tozie
    '    cmbRanandehTozie.SelectedValue = ccRanandeh_Tozie

    '    Strsql = "Select ltrim(rtrim(txtSemat)) + ' --- ' + FN as FN,CodeFard From qryGL_MoshakhasatFardi Where CodeMahal = " & CodeMahalFaal & " AND sVazeiatEstekhdam = 4188 And "
    '    Strsql &= " sSemat in (" & UD_Dll.Enums.GL_Semat.MamorPakhsh & "," & UD_Dll.Enums.GL_Semat.Ranandeh & "," & UD_Dll.Enums.GL_Semat.Foroshandeh_Sayar & ")"
    '    daSQL = New SqlDataAdapter(Strsql, ConnectionString)
    '    daSQL.Fill(dsForm, "tblMamorPakhsh")
    '    cmbMamorPakhsh.DataSource = Nothing
    '    cmbMamorPakhsh.Items.Clear()
    '    cmbMamorPakhsh.DataSource = dsForm.Tables("tblMamorPakhsh").DefaultView
    '    cmbMamorPakhsh.DisplayMember = "FN"
    '    cmbMamorPakhsh.ValueMember = "CodeFard"
    '    cmbMamorPakhsh.SelectedValue = ccMamorPakhsh
    '    cmbMamorPakhsh.SelectedValue = ccMamorPakhsh

    '    mskTarikhErsal.Text = TarikhErsal

    'End Sub
#End Region
#Region "Form Buttons"
    Private Sub btnTaeed_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTaeed.Click
        Dim cnSQL As SqlConnection
        Dim cmSQL As SqlCommand
        Dim strSQL As String
        Try
            If Len(mskTarikhTahvil.Text.ToString) <> 0 Then
                If Not objTarikh.IsShDate(mskTarikhTahvil.Text.ToString) Then
                    mskTarikhTahvil.Focus()
                    Exit Sub
                End If
            Else
                ErrPro.SetError(Me.mskTarikhTahvil, "تاریخ را وارد کنید.")
                MsgBox("تاریخ را وارد کنید.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
                mskTarikhTahvil.Focus()
                Exit Sub
            End If
            ErrPro.SetError(Me.mskTarikhTahvil, "")

            strSQL = "Update  tblFO_TafkikJoze "
            strSQL = strSQL & " Set "
            strSQL = strSQL & " TarikhTahvil = '" & Me.mskTarikhTahvil.Text & "',"
            strSQL = strSQL & " IsTahvil = 1 "
            strSQL = strSQL & " Where ccTafkikJoze = " & ccTafkikJoze & ";"

            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()
            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.ExecuteNonQuery()
            cnSQL.Close()
            cmSQL = Nothing : cnSQL = Nothing
            strSQL = Nothing
            Me.Close()
            MsgBox("برگه تفکیک شمـاره " & ShTafkik & " تحویل داده شد.", MsgBoxStyle.Information, "پیام")


        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->DeleteTitr")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->DeleteTitr")
        End Try
    End Sub
#End Region

End Class