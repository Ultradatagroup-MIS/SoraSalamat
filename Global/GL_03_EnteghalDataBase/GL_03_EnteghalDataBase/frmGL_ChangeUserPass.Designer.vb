<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmGL_ChangeUserPass
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.grb = New System.Windows.Forms.GroupBox()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.btnExit = New System.Windows.Forms.Button()
        Me.btnEnter = New System.Windows.Forms.Button()
        Me.grbEnter = New System.Windows.Forms.GroupBox()
        Me.txtPassword = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtNameKarbari = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.grbSQL = New System.Windows.Forms.GroupBox()
        Me.txtPassSqlServer2 = New System.Windows.Forms.TextBox()
        Me.txtUserSqlServer2 = New System.Windows.Forms.TextBox()
        Me.txtPassSqlServer1 = New System.Windows.Forms.TextBox()
        Me.txtUserSqlServer1 = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lblServer2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lblServer1 = New System.Windows.Forms.Label()
        Me.grb.SuspendLayout()
        Me.grbEnter.SuspendLayout()
        Me.grbSQL.SuspendLayout()
        Me.SuspendLayout()
        '
        'grb
        '
        Me.grb.Controls.Add(Me.btnSave)
        Me.grb.Controls.Add(Me.btnExit)
        Me.grb.Controls.Add(Me.btnEnter)
        Me.grb.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.grb.Location = New System.Drawing.Point(0, 144)
        Me.grb.Name = "grb"
        Me.grb.Size = New System.Drawing.Size(589, 57)
        Me.grb.TabIndex = 0
        Me.grb.TabStop = False
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Location = New System.Drawing.Point(257, 20)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(75, 29)
        Me.btnSave.TabIndex = 12
        Me.btnSave.Text = "دخیــره"
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'btnExit
        '
        Me.btnExit.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnExit.Location = New System.Drawing.Point(12, 20)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(75, 29)
        Me.btnExit.TabIndex = 13
        Me.btnExit.Text = "خــــروج"
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'btnEnter
        '
        Me.btnEnter.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnEnter.Location = New System.Drawing.Point(499, 20)
        Me.btnEnter.Name = "btnEnter"
        Me.btnEnter.Size = New System.Drawing.Size(75, 29)
        Me.btnEnter.TabIndex = 6
        Me.btnEnter.Text = "ورود >>>"
        Me.btnEnter.UseVisualStyleBackColor = True
        '
        'grbEnter
        '
        Me.grbEnter.Controls.Add(Me.txtPassword)
        Me.grbEnter.Controls.Add(Me.Label4)
        Me.grbEnter.Controls.Add(Me.txtNameKarbari)
        Me.grbEnter.Controls.Add(Me.Label3)
        Me.grbEnter.Dock = System.Windows.Forms.DockStyle.Left
        Me.grbEnter.Location = New System.Drawing.Point(0, 0)
        Me.grbEnter.Name = "grbEnter"
        Me.grbEnter.Size = New System.Drawing.Size(213, 144)
        Me.grbEnter.TabIndex = 1
        Me.grbEnter.TabStop = False
        '
        'txtPassword
        '
        Me.txtPassword.Location = New System.Drawing.Point(21, 105)
        Me.txtPassword.Name = "txtPassword"
        Me.txtPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtPassword.Size = New System.Drawing.Size(177, 21)
        Me.txtPassword.TabIndex = 5
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(141, 81)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(51, 13)
        Me.Label4.TabIndex = 4
        Me.Label4.Text = "رمز ورود :"
        '
        'txtNameKarbari
        '
        Me.txtNameKarbari.Location = New System.Drawing.Point(21, 40)
        Me.txtNameKarbari.Name = "txtNameKarbari"
        Me.txtNameKarbari.Size = New System.Drawing.Size(177, 21)
        Me.txtNameKarbari.TabIndex = 3
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(141, 17)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(60, 13)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "نام کاربری :"
        '
        'grbSQL
        '
        Me.grbSQL.Controls.Add(Me.txtPassSqlServer2)
        Me.grbSQL.Controls.Add(Me.txtUserSqlServer2)
        Me.grbSQL.Controls.Add(Me.txtPassSqlServer1)
        Me.grbSQL.Controls.Add(Me.txtUserSqlServer1)
        Me.grbSQL.Controls.Add(Me.Label2)
        Me.grbSQL.Controls.Add(Me.lblServer2)
        Me.grbSQL.Controls.Add(Me.Label1)
        Me.grbSQL.Controls.Add(Me.lblServer1)
        Me.grbSQL.Dock = System.Windows.Forms.DockStyle.Right
        Me.grbSQL.Location = New System.Drawing.Point(218, 0)
        Me.grbSQL.Name = "grbSQL"
        Me.grbSQL.Size = New System.Drawing.Size(371, 144)
        Me.grbSQL.TabIndex = 7
        Me.grbSQL.TabStop = False
        '
        'txtPassSqlServer2
        '
        Me.txtPassSqlServer2.Location = New System.Drawing.Point(14, 113)
        Me.txtPassSqlServer2.Name = "txtPassSqlServer2"
        Me.txtPassSqlServer2.Size = New System.Drawing.Size(226, 21)
        Me.txtPassSqlServer2.TabIndex = 11
        '
        'txtUserSqlServer2
        '
        Me.txtUserSqlServer2.Location = New System.Drawing.Point(14, 81)
        Me.txtUserSqlServer2.Name = "txtUserSqlServer2"
        Me.txtUserSqlServer2.Size = New System.Drawing.Size(226, 21)
        Me.txtUserSqlServer2.TabIndex = 10
        '
        'txtPassSqlServer1
        '
        Me.txtPassSqlServer1.Location = New System.Drawing.Point(14, 50)
        Me.txtPassSqlServer1.Name = "txtPassSqlServer1"
        Me.txtPassSqlServer1.Size = New System.Drawing.Size(226, 21)
        Me.txtPassSqlServer1.TabIndex = 9
        '
        'txtUserSqlServer1
        '
        Me.txtUserSqlServer1.Location = New System.Drawing.Point(14, 20)
        Me.txtUserSqlServer1.Name = "txtUserSqlServer1"
        Me.txtUserSqlServer1.Size = New System.Drawing.Size(226, 21)
        Me.txtUserSqlServer1.TabIndex = 8
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(246, 116)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(110, 13)
        Me.Label2.TabIndex = 26
        Me.Label2.Text = "رمز ورود SQL سرور 2 :"
        '
        'lblServer2
        '
        Me.lblServer2.AutoSize = True
        Me.lblServer2.Location = New System.Drawing.Point(246, 87)
        Me.lblServer2.Name = "lblServer2"
        Me.lblServer2.Size = New System.Drawing.Size(119, 13)
        Me.lblServer2.TabIndex = 26
        Me.lblServer2.Text = "نام کاربری SQL سرور 2 :"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(246, 53)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(110, 13)
        Me.Label1.TabIndex = 25
        Me.Label1.Text = "رمز ورود SQL سرور 1 :"
        '
        'lblServer1
        '
        Me.lblServer1.AutoSize = True
        Me.lblServer1.Location = New System.Drawing.Point(246, 23)
        Me.lblServer1.Name = "lblServer1"
        Me.lblServer1.Size = New System.Drawing.Size(119, 13)
        Me.lblServer1.TabIndex = 25
        Me.lblServer1.Text = "نام کاربری SQL سرور 1 :"
        '
        'frmGL_ChangeUserPass
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(589, 201)
        Me.Controls.Add(Me.grbEnter)
        Me.Controls.Add(Me.grbSQL)
        Me.Controls.Add(Me.grb)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.Name = "frmGL_ChangeUserPass"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.RightToLeftLayout = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "تغییر نام کاربری و رمز عبور سرورها"
        Me.grb.ResumeLayout(False)
        Me.grbEnter.ResumeLayout(False)
        Me.grbEnter.PerformLayout()
        Me.grbSQL.ResumeLayout(False)
        Me.grbSQL.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents grb As System.Windows.Forms.GroupBox
    Friend WithEvents grbEnter As System.Windows.Forms.GroupBox
    Friend WithEvents grbSQL As System.Windows.Forms.GroupBox
    Friend WithEvents txtPassword As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtNameKarbari As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtPassSqlServer2 As System.Windows.Forms.TextBox
    Friend WithEvents txtUserSqlServer2 As System.Windows.Forms.TextBox
    Friend WithEvents txtPassSqlServer1 As System.Windows.Forms.TextBox
    Friend WithEvents txtUserSqlServer1 As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents lblServer2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lblServer1 As System.Windows.Forms.Label
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents btnEnter As System.Windows.Forms.Button
    Friend WithEvents btnSave As System.Windows.Forms.Button
End Class
