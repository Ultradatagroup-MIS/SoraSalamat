Public Class frmGL_ChangeUserPass
    Const W_Form_Org As Integer = 230
    Const W_Form_Extract As Integer = 605
    Dim ErrPro As New ErrorProvider

    Private Sub frmGL_ChangeUserPass_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Width = W_Form_Org
        grbSQL.Enabled = False
        btnSave.Visible = False
    End Sub

    Private Sub btnEnter_Click(sender As Object, e As EventArgs) Handles btnEnter.Click
        If txtNameKarbari.Text.Trim.ToLower <> My.Settings.NameKarbari Then
            MsgBox("نام کاربری صحیح نمی باشد !", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, "خطا")
            ErrPro.SetError(Me.txtNameKarbari, "نام کاربری صحیح نمی باشد !")
            txtNameKarbari.Focus()
            Exit Sub
        End If
        ErrPro.SetError(Me.txtNameKarbari, "")

        If txtPassword.Text.Trim <> My.Settings.PassWord Then
            MsgBox("رمز ورود صحیح نمی باشد !", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, "خطا")
            ErrPro.SetError(Me.txtPassword, "رمز ورود صحیح نمی باشد !")
            txtPassword.Focus()
            Exit Sub
        End If
        ErrPro.SetError(Me.txtPassword, "")

        Me.Width = W_Form_Extract
        btnSave.Visible = True

        grbEnter.Enabled = False
        grbSQL.Enabled = True
        btnEnter.Enabled = False
        txtUserSqlServer1.Focus()


        txtUserSqlServer1.Text = My.Settings.UserSQL1
        txtPassSqlServer1.Text = My.Settings.PassSQL1
        txtUserSqlServer2.Text = My.Settings.UserSQL2
        txtPassSqlServer2.Text = My.Settings.PassSQL2

    End Sub
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If txtUserSqlServer1.Text.Trim = "" Then
            MsgBox("نام کاربری SQL سرور اول را وارد نمایید !", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, "خطا")
            ErrPro.SetError(Me.txtUserSqlServer1, "نام کاربری SQL سرور اول را وارد نمایید !")
            txtUserSqlServer1.Focus()
            Exit Sub
        End If
        ErrPro.SetError(Me.txtUserSqlServer1, "")

        If txtPassSqlServer1.Text.Trim = "" Then
            MsgBox("رمز ورود SQL سرور اول را وارد نمایید !", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, "خطا")
            ErrPro.SetError(Me.txtPassSqlServer1, "SQL سرور اول را وارد نمایید !")
            txtPassSqlServer1.Focus()
            Exit Sub
        End If
        ErrPro.SetError(Me.txtPassSqlServer1, "")

        If txtUserSqlServer2.Text.Trim = "" Then
            MsgBox("نام کاربری SQL سرور دوم را وارد نمایید !", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, "خطا")
            ErrPro.SetError(Me.txtUserSqlServer2, "نام کاربری SQL سرور دوم را وارد نمایید !")
            txtUserSqlServer2.Focus()
            Exit Sub
        End If
        ErrPro.SetError(Me.txtUserSqlServer2, "")

        If txtPassSqlServer2.Text.Trim = "" Then
            MsgBox("رمز ورود SQL سرور دوم را وارد نمایید !", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, "خطا")
            ErrPro.SetError(Me.txtPassSqlServer2, "SQL سرور دوم را وارد نمایید !")
            txtPassSqlServer2.Focus()
            Exit Sub
        End If
        ErrPro.SetError(Me.txtPassSqlServer2, "")


        My.Settings.UserSQL1 = txtUserSqlServer1.Text.Trim
        My.Settings.PassSQL1 = txtPassSqlServer1.Text.Trim
        My.Settings.UserSQL2 = txtUserSqlServer2.Text.Trim
        My.Settings.PassSQL2 = txtPassSqlServer2.Text.Trim

        MsgBox("تغییر اطلاعات با موفقیت صورت پذیرفت .", MsgBoxStyle.Information + MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, "")
        Me.Close()
    End Sub
    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub
End Class