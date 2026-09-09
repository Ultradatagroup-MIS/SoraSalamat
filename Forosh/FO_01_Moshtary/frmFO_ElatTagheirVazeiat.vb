Public Class frmFO_ElatTagheirVazeiat
#Region "Variable AND Constant Declration"
    ' Security 
    Private SN As Integer
    Dim ErrPro As New ErrorProvider
#End Region
#Region " Form Event Code "
    Private Sub frmFO_ElatTagheirVazeiat_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        objTools.ChangeKbd(UD_Dll.mdlUtility.KBD.Farsi)
        txtElat.Focus()
    End Sub
    Private Sub frmFO_ElatTagheirVazeiat_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{tab}")
        End If
    End Sub
#End Region
#Region "Global Form Code"
    Private Function IsValidField(ByVal chkField As String) As Boolean
        Try
            IsValidField = False
            If chkField = "txtElat" Or chkField = "All" Then
                If Len(txtElat.Text.ToString) = 0 Then
                    ErrPro.SetError(Me.txtElat, "علت غیر فعال کردن مشتری را وارد کنیـــد.")
                    MsgBox("علت غیر فعال کردن مشتری را وارد کنیـــد.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
                    txtElat.Focus()
                    Exit Function
                End If
                ErrPro.SetError(Me.txtElat, "")
            End If
            Return True
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->IsValidRow")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->IsValidRow")
        End Try
    End Function
#End Region
#Region " Button Event "
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If Not IsValidField("All") Then
            Exit Sub
        End If
        frmFO_Moshtary.ElatVazeiatMoshtary = txtElat.Text
        Me.Close()
    End Sub
#End Region




End Class