<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmFO_PeigiryFaktor
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
        Me.cmbSharh = New System.Windows.Forms.ComboBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.txtSharh = New System.Windows.Forms.TextBox
        Me.mskTarikhPeigiryBady = New System.Windows.Forms.MaskedTextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.btnExit = New System.Windows.Forms.Button
        Me.btnUpdate = New System.Windows.Forms.Button
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.cmbSharh)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.txtSharh)
        Me.GroupBox1.Controls.Add(Me.mskTarikhPeigiryBady)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(524, 147)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = False
        '
        'cmbSharh
        '
        Me.cmbSharh.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbSharh.FormattingEnabled = True
        Me.cmbSharh.Location = New System.Drawing.Point(12, 15)
        Me.cmbSharh.Name = "cmbSharh"
        Me.cmbSharh.Size = New System.Drawing.Size(252, 21)
        Me.cmbSharh.TabIndex = 6
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(270, 19)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(37, 13)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "شرح :"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(463, 43)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(54, 13)
        Me.Label6.TabIndex = 4
        Me.Label6.Text = "توضیحات :"
        '
        'txtSharh
        '
        Me.txtSharh.Location = New System.Drawing.Point(11, 43)
        Me.txtSharh.Multiline = True
        Me.txtSharh.Name = "txtSharh"
        Me.txtSharh.Size = New System.Drawing.Size(450, 94)
        Me.txtSharh.TabIndex = 5
        '
        'mskTarikhPeigiryBady
        '
        Me.mskTarikhPeigiryBady.AllowPromptAsInput = False
        Me.mskTarikhPeigiryBady.Location = New System.Drawing.Point(321, 15)
        Me.mskTarikhPeigiryBady.Mask = "####/##/##"
        Me.mskTarikhPeigiryBady.Name = "mskTarikhPeigiryBady"
        Me.mskTarikhPeigiryBady.PromptChar = Global.Microsoft.VisualBasic.ChrW(32)
        Me.mskTarikhPeigiryBady.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.mskTarikhPeigiryBady.Size = New System.Drawing.Size(83, 21)
        Me.mskTarikhPeigiryBady.TabIndex = 1
        Me.mskTarikhPeigiryBady.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(410, 19)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(107, 13)
        Me.Label3.TabIndex = 0
        Me.Label3.Text = "تاریخ پی گیری بعدی :"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.btnExit)
        Me.GroupBox2.Controls.Add(Me.btnUpdate)
        Me.GroupBox2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.GroupBox2.Location = New System.Drawing.Point(0, 153)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(524, 52)
        Me.GroupBox2.TabIndex = 2
        Me.GroupBox2.TabStop = False
        '
        'btnExit
        '
        Me.btnExit.AccessibleDescription = "Search Button"
        Me.btnExit.AccessibleName = "Search Button"
        Me.btnExit.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnExit.Location = New System.Drawing.Point(184, 17)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(75, 27)
        Me.btnExit.TabIndex = 2
        Me.btnExit.Text = "&خروج"
        '
        'btnUpdate
        '
        Me.btnUpdate.AccessibleDescription = "Update Button"
        Me.btnUpdate.AccessibleName = "Update Button"
        Me.btnUpdate.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnUpdate.Location = New System.Drawing.Point(265, 17)
        Me.btnUpdate.Name = "btnUpdate"
        Me.btnUpdate.Size = New System.Drawing.Size(75, 27)
        Me.btnUpdate.TabIndex = 3
        Me.btnUpdate.Text = "&ذخيره"
        '
        'frmFO_PeigiryFaktor
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(524, 205)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Name = "frmFO_PeigiryFaktor"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.RightToLeftLayout = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "پیگیری فاکتــور"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtSharh As System.Windows.Forms.TextBox
    Friend WithEvents mskTarikhPeigiryBady As System.Windows.Forms.MaskedTextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents btnUpdate As System.Windows.Forms.Button
    Friend WithEvents cmbSharh As System.Windows.Forms.ComboBox
End Class
