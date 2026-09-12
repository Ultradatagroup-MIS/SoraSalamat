<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmEndDate
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.btnAction = New System.Windows.Forms.Button()
        Me.mskTaTarikh = New System.Windows.Forms.MaskedTextBox()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.lblTaTarikh = New System.Windows.Forms.Label()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.btnAction)
        Me.GroupBox1.Controls.Add(Me.mskTaTarikh)
        Me.GroupBox1.Controls.Add(Me.btnCancel)
        Me.GroupBox1.Controls.Add(Me.lblTaTarikh)
        Me.GroupBox1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.Location = New System.Drawing.Point(5, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.GroupBox1.Size = New System.Drawing.Size(261, 114)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "تاریخ مورد نظر را وارد کنید."
        '
        'btnAction
        '
        Me.btnAction.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnAction.Location = New System.Drawing.Point(133, 74)
        Me.btnAction.Name = "btnAction"
        Me.btnAction.Size = New System.Drawing.Size(75, 23)
        Me.btnAction.TabIndex = 4
        Me.btnAction.Text = "انجام عملیات"
        Me.btnAction.UseVisualStyleBackColor = True
        '
        'mskTaTarikh
        '
        Me.mskTaTarikh.Location = New System.Drawing.Point(88, 32)
        Me.mskTaTarikh.Mask = "0000/00/00"
        Me.mskTaTarikh.Name = "mskTaTarikh"
        Me.mskTaTarikh.PromptChar = Global.Microsoft.VisualBasic.ChrW(32)
        Me.mskTaTarikh.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.mskTaTarikh.Size = New System.Drawing.Size(99, 21)
        Me.mskTaTarikh.TabIndex = 3
        Me.mskTaTarikh.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.mskTaTarikh.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals
        '
        'btnCancel
        '
        Me.btnCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancel.Location = New System.Drawing.Point(52, 74)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(75, 23)
        Me.btnCancel.TabIndex = 3
        Me.btnCancel.Text = "صرفنظر"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'lblTaTarikh
        '
        Me.lblTaTarikh.AutoSize = True
        Me.lblTaTarikh.Location = New System.Drawing.Point(193, 35)
        Me.lblTaTarikh.Name = "lblTaTarikh"
        Me.lblTaTarikh.Size = New System.Drawing.Size(37, 13)
        Me.lblTaTarikh.TabIndex = 2
        Me.lblTaTarikh.Text = "تا تاریخ"
        '
        'frmEndDate
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(278, 138)
        Me.ControlBox = False
        Me.Controls.Add(Me.GroupBox1)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmEndDate"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "بستن بازه تاریخی"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents mskTaTarikh As MaskedTextBox
    Friend WithEvents lblTaTarikh As Label
    Friend WithEvents btnAction As Button
    Friend WithEvents btnCancel As Button
End Class
