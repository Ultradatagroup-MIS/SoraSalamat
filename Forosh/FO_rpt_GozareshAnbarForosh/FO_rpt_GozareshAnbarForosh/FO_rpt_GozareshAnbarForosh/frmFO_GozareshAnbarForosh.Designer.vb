<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmFO_GozareshAnbarForosh
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.cmbAnbar = New System.Windows.Forms.ComboBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.mskAzTarikh = New System.Windows.Forms.MaskedTextBox
        Me.lblAzTarikh = New System.Windows.Forms.Label
        Me.mskTaTarikh = New System.Windows.Forms.MaskedTextBox
        Me.lblTaTarikh = New System.Windows.Forms.Label
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.btnCancel = New System.Windows.Forms.Button
        Me.btnReport = New System.Windows.Forms.Button
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.cmbAnbar)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.mskAzTarikh)
        Me.GroupBox1.Controls.Add(Me.lblAzTarikh)
        Me.GroupBox1.Controls.Add(Me.mskTaTarikh)
        Me.GroupBox1.Controls.Add(Me.lblTaTarikh)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(317, 81)
        Me.GroupBox1.TabIndex = 2
        Me.GroupBox1.TabStop = False
        '
        'cmbAnbar
        '
        Me.cmbAnbar.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbAnbar.FormattingEnabled = True
        Me.cmbAnbar.Location = New System.Drawing.Point(12, 20)
        Me.cmbAnbar.Name = "cmbAnbar"
        Me.cmbAnbar.Size = New System.Drawing.Size(244, 21)
        Me.cmbAnbar.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(262, 23)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(43, 13)
        Me.Label1.TabIndex = 12
        Me.Label1.Text = "انبــــار :"
        '
        'mskAzTarikh
        '
        Me.mskAzTarikh.AllowPromptAsInput = False
        Me.mskAzTarikh.CutCopyMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals
        Me.mskAzTarikh.Location = New System.Drawing.Point(166, 51)
        Me.mskAzTarikh.Mask = "####/##/##"
        Me.mskAzTarikh.Name = "mskAzTarikh"
        Me.mskAzTarikh.PromptChar = Global.Microsoft.VisualBasic.ChrW(32)
        Me.mskAzTarikh.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.mskAzTarikh.Size = New System.Drawing.Size(90, 21)
        Me.mskAzTarikh.TabIndex = 1
        Me.mskAzTarikh.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals
        '
        'lblAzTarikh
        '
        Me.lblAzTarikh.AutoSize = True
        Me.lblAzTarikh.Location = New System.Drawing.Point(262, 54)
        Me.lblAzTarikh.Name = "lblAzTarikh"
        Me.lblAzTarikh.Size = New System.Drawing.Size(45, 13)
        Me.lblAzTarikh.TabIndex = 10
        Me.lblAzTarikh.Text = "از تاریخ :"
        '
        'mskTaTarikh
        '
        Me.mskTaTarikh.AllowPromptAsInput = False
        Me.mskTaTarikh.CutCopyMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals
        Me.mskTaTarikh.Location = New System.Drawing.Point(12, 51)
        Me.mskTaTarikh.Mask = "####/##/##"
        Me.mskTaTarikh.Name = "mskTaTarikh"
        Me.mskTaTarikh.PromptChar = Global.Microsoft.VisualBasic.ChrW(32)
        Me.mskTaTarikh.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.mskTaTarikh.Size = New System.Drawing.Size(90, 21)
        Me.mskTaTarikh.TabIndex = 2
        Me.mskTaTarikh.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals
        '
        'lblTaTarikh
        '
        Me.lblTaTarikh.AutoSize = True
        Me.lblTaTarikh.Location = New System.Drawing.Point(108, 55)
        Me.lblTaTarikh.Name = "lblTaTarikh"
        Me.lblTaTarikh.Size = New System.Drawing.Size(44, 13)
        Me.lblTaTarikh.TabIndex = 8
        Me.lblTaTarikh.Text = "تا تاریخ :"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.btnCancel)
        Me.GroupBox2.Controls.Add(Me.btnReport)
        Me.GroupBox2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.GroupBox2.Location = New System.Drawing.Point(0, 79)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(317, 47)
        Me.GroupBox2.TabIndex = 3
        Me.GroupBox2.TabStop = False
        '
        'btnCancel
        '
        Me.btnCancel.Location = New System.Drawing.Point(78, 14)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(80, 26)
        Me.btnCancel.TabIndex = 1
        Me.btnCancel.Text = "خروج"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'btnReport
        '
        Me.btnReport.Location = New System.Drawing.Point(158, 14)
        Me.btnReport.Name = "btnReport"
        Me.btnReport.Size = New System.Drawing.Size(80, 26)
        Me.btnReport.TabIndex = 0
        Me.btnReport.Text = "تایید"
        Me.btnReport.UseVisualStyleBackColor = True
        '
        'frmFO_GozareshAnbarForosh
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(317, 126)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.GroupBox2)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.Name = "frmFO_GozareshAnbarForosh"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.RightToLeftLayout = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "گــزارش انبــــار - فـــروش"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents cmbAnbar As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents mskAzTarikh As System.Windows.Forms.MaskedTextBox
    Friend WithEvents lblAzTarikh As System.Windows.Forms.Label
    Friend WithEvents mskTaTarikh As System.Windows.Forms.MaskedTextBox
    Friend WithEvents lblTaTarikh As System.Windows.Forms.Label
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnReport As System.Windows.Forms.Button
End Class
