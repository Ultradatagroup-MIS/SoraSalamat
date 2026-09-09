<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMng
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
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.lblCurrentAdmin = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.plConnection = New System.Windows.Forms.Panel
        Me.lblCurrentConnection = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.txtUserId = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.txtDatabase = New System.Windows.Forms.TextBox
        Me.txtServerName = New System.Windows.Forms.TextBox
        Me.chkConnection = New System.Windows.Forms.CheckBox
        Me.txtAdminPass = New System.Windows.Forms.TextBox
        Me.chkAdminPass = New System.Windows.Forms.CheckBox
        Me.btnChange = New System.Windows.Forms.Button
        Me.btnExit = New System.Windows.Forms.Button
        Me.Label5 = New System.Windows.Forms.Label
        Me.txtPass = New System.Windows.Forms.TextBox
        Me.btnTest = New System.Windows.Forms.Button
        Me.GroupBox1.SuspendLayout()
        Me.plConnection.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.lblCurrentAdmin)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.plConnection)
        Me.GroupBox1.Controls.Add(Me.chkConnection)
        Me.GroupBox1.Controls.Add(Me.txtAdminPass)
        Me.GroupBox1.Controls.Add(Me.chkAdminPass)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(434, 370)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Settings"
        '
        'lblCurrentAdmin
        '
        Me.lblCurrentAdmin.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblCurrentAdmin.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblCurrentAdmin.Location = New System.Drawing.Point(119, 334)
        Me.lblCurrentAdmin.Name = "lblCurrentAdmin"
        Me.lblCurrentAdmin.Size = New System.Drawing.Size(306, 20)
        Me.lblCurrentAdmin.TabIndex = 5
        '
        'Label6
        '
        Me.Label6.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(44, 338)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(71, 13)
        Me.Label6.TabIndex = 4
        Me.Label6.Text = "Current Value"
        '
        'plConnection
        '
        Me.plConnection.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.plConnection.Controls.Add(Me.btnTest)
        Me.plConnection.Controls.Add(Me.txtPass)
        Me.plConnection.Controls.Add(Me.Label5)
        Me.plConnection.Controls.Add(Me.lblCurrentConnection)
        Me.plConnection.Controls.Add(Me.Label4)
        Me.plConnection.Controls.Add(Me.Label1)
        Me.plConnection.Controls.Add(Me.Label2)
        Me.plConnection.Controls.Add(Me.txtUserId)
        Me.plConnection.Controls.Add(Me.Label3)
        Me.plConnection.Controls.Add(Me.txtDatabase)
        Me.plConnection.Controls.Add(Me.txtServerName)
        Me.plConnection.Location = New System.Drawing.Point(38, 42)
        Me.plConnection.Name = "plConnection"
        Me.plConnection.Size = New System.Drawing.Size(390, 213)
        Me.plConnection.TabIndex = 1
        '
        'lblCurrentConnection
        '
        Me.lblCurrentConnection.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblCurrentConnection.Location = New System.Drawing.Point(81, 114)
        Me.lblCurrentConnection.Name = "lblCurrentConnection"
        Me.lblCurrentConnection.Size = New System.Drawing.Size(306, 51)
        Me.lblCurrentConnection.TabIndex = 9
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(6, 115)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(71, 13)
        Me.Label4.TabIndex = 8
        Me.Label4.Text = "Current Value"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(6, 7)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(69, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Server Name"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(6, 59)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(43, 13)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "User ID"
        '
        'txtUserId
        '
        Me.txtUserId.Location = New System.Drawing.Point(81, 55)
        Me.txtUserId.Name = "txtUserId"
        Me.txtUserId.Size = New System.Drawing.Size(306, 20)
        Me.txtUserId.TabIndex = 5
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(6, 33)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(53, 13)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Database"
        '
        'txtDatabase
        '
        Me.txtDatabase.Location = New System.Drawing.Point(81, 29)
        Me.txtDatabase.Name = "txtDatabase"
        Me.txtDatabase.Size = New System.Drawing.Size(306, 20)
        Me.txtDatabase.TabIndex = 3
        '
        'txtServerName
        '
        Me.txtServerName.Location = New System.Drawing.Point(81, 3)
        Me.txtServerName.Name = "txtServerName"
        Me.txtServerName.Size = New System.Drawing.Size(306, 20)
        Me.txtServerName.TabIndex = 1
        '
        'chkConnection
        '
        Me.chkConnection.AutoSize = True
        Me.chkConnection.Checked = True
        Me.chkConnection.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkConnection.Location = New System.Drawing.Point(23, 19)
        Me.chkConnection.Name = "chkConnection"
        Me.chkConnection.Size = New System.Drawing.Size(150, 17)
        Me.chkConnection.TabIndex = 0
        Me.chkConnection.Text = "Change Connection String"
        Me.chkConnection.UseVisualStyleBackColor = True
        '
        'txtAdminPass
        '
        Me.txtAdminPass.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtAdminPass.Location = New System.Drawing.Point(119, 311)
        Me.txtAdminPass.MaxLength = 25
        Me.txtAdminPass.Name = "txtAdminPass"
        Me.txtAdminPass.Size = New System.Drawing.Size(306, 20)
        Me.txtAdminPass.TabIndex = 3
        '
        'chkAdminPass
        '
        Me.chkAdminPass.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.chkAdminPass.AutoSize = True
        Me.chkAdminPass.Checked = True
        Me.chkAdminPass.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkAdminPass.Location = New System.Drawing.Point(23, 288)
        Me.chkAdminPass.Name = "chkAdminPass"
        Me.chkAdminPass.Size = New System.Drawing.Size(144, 17)
        Me.chkAdminPass.TabIndex = 2
        Me.chkAdminPass.Text = "Change Admin Password"
        Me.chkAdminPass.UseVisualStyleBackColor = True
        '
        'btnChange
        '
        Me.btnChange.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnChange.Location = New System.Drawing.Point(222, 388)
        Me.btnChange.Name = "btnChange"
        Me.btnChange.Size = New System.Drawing.Size(130, 23)
        Me.btnChange.TabIndex = 1
        Me.btnChange.Text = "Change Settings"
        Me.btnChange.UseVisualStyleBackColor = True
        '
        'btnExit
        '
        Me.btnExit.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnExit.Location = New System.Drawing.Point(358, 388)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(88, 23)
        Me.btnExit.TabIndex = 2
        Me.btnExit.Text = "Exit"
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(6, 85)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(53, 13)
        Me.Label5.TabIndex = 6
        Me.Label5.Text = "Password"
        '
        'txtPass
        '
        Me.txtPass.Location = New System.Drawing.Point(81, 81)
        Me.txtPass.Name = "txtPass"
        Me.txtPass.Size = New System.Drawing.Size(306, 20)
        Me.txtPass.TabIndex = 7
        '
        'btnTest
        '
        Me.btnTest.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnTest.Location = New System.Drawing.Point(257, 174)
        Me.btnTest.Name = "btnTest"
        Me.btnTest.Size = New System.Drawing.Size(130, 23)
        Me.btnTest.TabIndex = 10
        Me.btnTest.TabStop = False
        Me.btnTest.Text = "Test Connection"
        Me.btnTest.UseVisualStyleBackColor = True
        '
        'frmMng
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(458, 423)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.btnChange)
        Me.Controls.Add(Me.GroupBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.Name = "frmMng"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Reg Settings"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.plConnection.ResumeLayout(False)
        Me.plConnection.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents btnChange As System.Windows.Forms.Button
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents txtAdminPass As System.Windows.Forms.TextBox
    Friend WithEvents chkAdminPass As System.Windows.Forms.CheckBox
    Friend WithEvents txtUserId As System.Windows.Forms.TextBox
    Friend WithEvents txtDatabase As System.Windows.Forms.TextBox
    Friend WithEvents txtServerName As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents chkConnection As System.Windows.Forms.CheckBox
    Friend WithEvents plConnection As System.Windows.Forms.Panel
    Friend WithEvents lblCurrentConnection As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents lblCurrentAdmin As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtPass As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents btnTest As System.Windows.Forms.Button

End Class
