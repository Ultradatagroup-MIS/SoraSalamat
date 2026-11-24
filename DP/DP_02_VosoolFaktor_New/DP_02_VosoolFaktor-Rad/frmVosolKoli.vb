Public Class frmVosolKoli
    Private Sub txtCodeMoshtaryS_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCodeMoshtaryF.KeyPress
        Try
            If (Asc(e.KeyChar()) < 48 Or Asc(e.KeyChar()) > 57) And (Asc(e.KeyChar()) <> 8) Then
                e.Handled = True
            End If
            If e.KeyChar = Chr(Keys.Space) Then
                Dim objMoshtary As New Forms_dll.frmFO_MoshtarySearch
                Dim StrSql As String

                StrSql = "Select * from qryFO_Moshtary Where CodeMahal=" & CodeMahalFaal & " AND sVazeiat = 3980 "
                StrSql &= " AND Not exists (Select * From tblGL_SecurityData where namekarbar= '" & UserName & "' and CodeSubSystem = 614 and pk = qryFO_Moshtary.ccMoshtary) order by NameMoshtary"

                tCodeMoshtary = ""
                tNameMoshtary = ""
                tccMoshtary = ""
                SearchItem = "CodeMoshtary"
                If txtCodeMoshtaryF.Text.Length <> 0 Then
                    tCodeMoshtary = txtCodeMoshtaryF.Text
                End If
                objMoshtary.MultiSelection = False
                objMoshtary.SetForm(StrSql)
                objMoshtary.ShowDialog()
                txtCodeMoshtaryF.Tag = IIf(IsNothing(objMoshtary.tccMoshtary), 0, objMoshtary.tccMoshtary)
                txtCodeMoshtaryF.Text = IIf(IsNothing(objMoshtary.tCodeMoshtary), "", objMoshtary.tCodeMoshtary)
                lblNameMoshtary.Text = IIf(IsNothing(objMoshtary.tNameMoshtary), "", objMoshtary.tNameMoshtary)

            End If
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->txtCodeMoshtaryS_KeyPress")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->txtCodeMoshtaryS_KeyPress")
        End Try

    End Sub
End Class