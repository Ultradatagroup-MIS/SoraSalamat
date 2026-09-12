Public Class frmEndDate

    Enum Modes
        BulkInsert = 1
        CloseEnd = 2
    End Enum
    Dim objTarikh As New UD_Dll.Tarikh
    Dim ErrPro As New ErrorProvider

    Private _Mode As Modes
    Public Property Mode() As Modes
        Get
            Return _Mode
        End Get
        Set(ByVal value As Modes)
            _Mode = value
        End Set
    End Property


    Private _AzTarikh As String
    Public ReadOnly Property AzTarikh() As String
        Get
            Return _AzTarikh
        End Get
    End Property


    Private _TaTarikh As String
    Public ReadOnly Property TaTarikh() As String
        Get
            Return _TaTarikh
        End Get
    End Property

    Private Sub frmInputDate_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{tab}")
        End If
    End Sub
    Private Sub btnAction_Click(sender As Object, e As EventArgs) Handles btnAction.Click
        If Not IsValidForm() Then
            Exit Sub
        End If

        Me._TaTarikh = mskTaTarikh.Text
        Me.DialogResult = Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub
    Private Function IsValidForm() As Boolean
        IsValidForm = False




        If Len(mskTaTarikh.Text.ToString) <> 0 Then
            If Not objTarikh.IsShDate(mskTaTarikh.Text.ToString) Then
                mskTaTarikh.Focus()
                Exit Function
            End If
        Else
            ErrPro.SetError(Me.mskTaTarikh, "تا تاريخ را وارد کنيد.")
            MsgBox("تا تاريخ را وارد کنيد.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
            mskTaTarikh.Focus()
            Exit Function
        End If
        ErrPro.SetError(Me.mskTaTarikh, "")



        If Me.mskTaTarikh.Text < objTarikh.DecDay(TarikhEmrooz, 1).Replace("/", "") Then
            ErrPro.SetError(Me.mskTaTarikh, "تاریخ پایانی نباید پیش از یک روز قبل باشد.")
            MsgBox("تاریخ پایانی نباید پیش از یک روز قبل باشد.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
            mskTaTarikh.Focus()
            Exit Function
        End If

        Return True
    End Function

    Private Sub frmEndDate_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetForm()
        mskTaTarikh.Focus()
    End Sub
    Private Sub SetForm()

        lblTaTarikh.Visible = True

        mskTaTarikh.Visible = True
        Me.Width = 333

        If Me.Mode = Modes.CloseEnd Then
            Me.Width = 180

        End If
    End Sub
End Class