Public Class frmGL_TaeenMahal
#Region "Variable AND Constant Declration"
    Const FormTableName As String = "tblGL_MahalFaal"
    Dim ErrPro As New ErrorProvider
    Dim dsForm As New DataSet
#End Region
#Region "Form Event Code"
    Private Sub frmTaeenDoreh_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        LoadCombo()
    End Sub
#End Region
#Region "Global Form Code"
    Private Sub LoadCombo()
        Dim daSQL As SqlDataAdapter

        daSQL = New SqlDataAdapter("Select * From tblGL_MarkazPakhsh", ConnectionString)
        If dsForm.Tables.Contains("tblMarkaz") = True Then
            dsForm.Tables.Remove("tblMarkaz")
        End If
        daSQL.Fill(dsForm, "tblMarkaz")
        cmbDoreh.DataSource = Nothing
        cmbDoreh.Items.Clear()
        cmbDoreh.DataSource = dsForm.Tables("tblMarkaz").DefaultView
        cmbDoreh.DisplayMember = "NameMahal"
        cmbDoreh.ValueMember = "CodeMahal"
        cmbDoreh.SelectedValue = CodeDoreh

        daSQL = Nothing
    End Sub
    Private Function IsValidForm(ByVal CheckField As String) As Boolean
        IsValidForm = False
        If CheckField = "cmbDoreh" Or CheckField = "All" Then
            If cmbDoreh.SelectedIndex = -1 Or IsNothing(cmbDoreh.SelectedValue) Then
                ErrPro.SetError(Me.cmbDoreh, "œÊ—Â —« «‰ Œ«» ò‰Ìœ")
                MsgBox("œÊ—Â —« «‰ Œ«» ò‰Ìœ", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "–ŒÌ—Â")
                cmbDoreh.Focus()
                Exit Function
            End If
            ErrPro.SetError(Me.cmbDoreh, "")
        End If
        Return True
    End Function
    Private Sub UpdateRecord()
        If Not IsValidForm("All") Then
            Exit Sub
        End If

        Dim cnSQL As SqlConnection
        Dim cmSQL As SqlCommand
        Dim strSQL As String
        Try
            If objTools.ConvertNulls(objTools.DLookup("CodeMahal", FormTableName, "UserName = '" & UserName & "'"), 0) = 0 Then
                strSQL = " Insert Into  " & FormTableName
                strSQL &= " (CodeMahal,UserName) Values ( "
                strSQL &= cmbDoreh.SelectedValue & ","
                strSQL &= "'" & UserName & "')"

                cnSQL = New SqlConnection(ConnectionString)
                cnSQL.Open()
                cmSQL = New SqlCommand(strSQL, cnSQL)
                cmSQL.ExecuteNonQuery()

                cnSQL.Close() : cnSQL = Nothing : cmSQL = Nothing
                CodeDoreh = cmbDoreh.SelectedValue
                Me.Close()
            Else
                strSQL = "Update " & FormTableName
                strSQL = strSQL & " Set "
                strSQL = strSQL & "CodeMahal = " & cmbDoreh.SelectedValue
                strSQL = strSQL & " Where UserName = '" & UserName & "'"

                cnSQL = New SqlConnection(ConnectionString)
                cnSQL.Open()
                cmSQL = New SqlCommand(strSQL, cnSQL)
                cmSQL.ExecuteNonQuery()

                cnSQL.Close() : cnSQL = Nothing : cmSQL = Nothing
                CodeMahalFaal = cmbDoreh.SelectedValue
                NameMahalFaal = objTools.ConvertNulls(objTools.DLookup("NameMahal", "tblGL_MarkazPakhsh", "CodeMahal = " & CodeMahalFaal), "")
                Me.Close()
            End If


        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Critical, "Œÿ«Ì »«‰ò")
        Catch e As Exception
            MsgBox(e.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Critical, "Œÿ«")
        End Try
    End Sub
#End Region
#Region "From Buttons"
    Private Sub btnTaeed_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTaeed.Click
        If cmbDoreh.Tag = cmbDoreh.SelectedValue Then
            Exit Sub
        End If
        UpdateRecord()

        Dim f As New MDIParent
        f.KillingAllProcess()
        f.LoadItem()
        f = Nothing


    End Sub
    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub
#End Region
End Class