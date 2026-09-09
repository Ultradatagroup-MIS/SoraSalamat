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
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.cmbSherkat = New System.Windows.Forms.ComboBox()
        Me.chkConnection = New System.Windows.Forms.CheckBox()
        Me.btnChange = New System.Windows.Forms.Button()
        Me.btnExit = New System.Windows.Forms.Button()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.cmbSherkat)
        Me.GroupBox1.Controls.Add(Me.chkConnection)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(434, 88)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Settings"
        '
        'cmbSherkat
        '
        Me.cmbSherkat.CausesValidation = False
        Me.cmbSherkat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbSherkat.Location = New System.Drawing.Point(23, 52)
        Me.cmbSherkat.Name = "cmbSherkat"
        Me.cmbSherkat.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.cmbSherkat.Size = New System.Drawing.Size(387, 21)
        Me.cmbSherkat.TabIndex = 2
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
        'btnChange
        '
        Me.btnChange.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnChange.Location = New System.Drawing.Point(222, 106)
        Me.btnChange.Name = "btnChange"
        Me.btnChange.Size = New System.Drawing.Size(130, 23)
        Me.btnChange.TabIndex = 1
        Me.btnChange.Text = "Change Settings"
        Me.btnChange.UseVisualStyleBackColor = True
        '
        'btnExit
        '
        Me.btnExit.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnExit.Location = New System.Drawing.Point(358, 106)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(88, 23)
        Me.btnExit.TabIndex = 2
        Me.btnExit.Text = "Exit"
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'frmMng
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(458, 141)
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
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents btnChange As System.Windows.Forms.Button
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents chkConnection As System.Windows.Forms.CheckBox
    Friend WithEvents cmbSherkat As System.Windows.Forms.ComboBox

End Class
