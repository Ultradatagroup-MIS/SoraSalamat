<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DP_rpt_Enteghalat
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
        Me.Label1 = New System.Windows.Forms.Label
        Me.mskAzTarikh = New System.Windows.Forms.MaskedTextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.mskTaTarikh = New System.Windows.Forms.MaskedTextBox
        Me.rbSandogh = New System.Windows.Forms.RadioButton
        Me.rbBank = New System.Windows.Forms.RadioButton
        Me.lblHesab = New System.Windows.Forms.Label
        Me.cmbHesab = New System.Windows.Forms.ComboBox
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        Me.btnCancel = New System.Windows.Forms.Button
        Me.btnDaftarEnteghalat = New System.Windows.Forms.Button
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(522, 18)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(42, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "از تاریخ:"
        '
        'mskAzTarikh
        '
        Me.mskAzTarikh.AllowPromptAsInput = False
        Me.mskAzTarikh.CutCopyMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals
        Me.mskAzTarikh.Location = New System.Drawing.Point(404, 14)
        Me.mskAzTarikh.Mask = "####/##/##"
        Me.mskAzTarikh.Name = "mskAzTarikh"
        Me.mskAzTarikh.PromptChar = Global.Microsoft.VisualBasic.ChrW(32)
        Me.mskAzTarikh.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.mskAzTarikh.Size = New System.Drawing.Size(100, 21)
        Me.mskAzTarikh.TabIndex = 2
        Me.mskAzTarikh.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(172, 18)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(41, 13)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "تا تاریخ:"
        '
        'mskTaTarikh
        '
        Me.mskTaTarikh.AllowPromptAsInput = False
        Me.mskTaTarikh.CutCopyMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals
        Me.mskTaTarikh.Location = New System.Drawing.Point(58, 14)
        Me.mskTaTarikh.Mask = "####/##/##"
        Me.mskTaTarikh.Name = "mskTaTarikh"
        Me.mskTaTarikh.PromptChar = Global.Microsoft.VisualBasic.ChrW(32)
        Me.mskTaTarikh.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.mskTaTarikh.Size = New System.Drawing.Size(100, 21)
        Me.mskTaTarikh.TabIndex = 2
        Me.mskTaTarikh.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals
        '
        'rbSandogh
        '
        Me.rbSandogh.AutoSize = True
        Me.rbSandogh.Location = New System.Drawing.Point(457, 22)
        Me.rbSandogh.Name = "rbSandogh"
        Me.rbSandogh.Size = New System.Drawing.Size(59, 17)
        Me.rbSandogh.TabIndex = 5
        Me.rbSandogh.Text = "صندوق"
        Me.rbSandogh.UseVisualStyleBackColor = True
        '
        'rbBank
        '
        Me.rbBank.AutoSize = True
        Me.rbBank.Location = New System.Drawing.Point(563, 22)
        Me.rbBank.Name = "rbBank"
        Me.rbBank.Size = New System.Drawing.Size(45, 17)
        Me.rbBank.TabIndex = 4
        Me.rbBank.Text = "بانک"
        Me.rbBank.UseVisualStyleBackColor = True
        '
        'lblHesab
        '
        Me.lblHesab.AutoSize = True
        Me.lblHesab.Location = New System.Drawing.Point(331, 24)
        Me.lblHesab.Name = "lblHesab"
        Me.lblHesab.Size = New System.Drawing.Size(31, 13)
        Me.lblHesab.TabIndex = 6
        Me.lblHesab.Text = "بانک:"
        '
        'cmbHesab
        '
        Me.cmbHesab.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbHesab.FormattingEnabled = True
        Me.cmbHesab.Location = New System.Drawing.Point(9, 20)
        Me.cmbHesab.Name = "cmbHesab"
        Me.cmbHesab.Size = New System.Drawing.Size(318, 21)
        Me.cmbHesab.TabIndex = 7
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.cmbHesab)
        Me.GroupBox1.Controls.Add(Me.rbBank)
        Me.GroupBox1.Controls.Add(Me.rbSandogh)
        Me.GroupBox1.Controls.Add(Me.lblHesab)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 62)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(630, 54)
        Me.GroupBox1.TabIndex = 10
        Me.GroupBox1.TabStop = False
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.mskAzTarikh)
        Me.GroupBox2.Controls.Add(Me.Label1)
        Me.GroupBox2.Controls.Add(Me.Label2)
        Me.GroupBox2.Controls.Add(Me.mskTaTarikh)
        Me.GroupBox2.Location = New System.Drawing.Point(12, 7)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(630, 44)
        Me.GroupBox2.TabIndex = 11
        Me.GroupBox2.TabStop = False
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.btnCancel)
        Me.GroupBox3.Controls.Add(Me.btnDaftarEnteghalat)
        Me.GroupBox3.Location = New System.Drawing.Point(12, 123)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(630, 56)
        Me.GroupBox3.TabIndex = 12
        Me.GroupBox3.TabStop = False
        '
        'btnCancel
        '
        Me.btnCancel.Location = New System.Drawing.Point(21, 20)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(62, 23)
        Me.btnCancel.TabIndex = 10
        Me.btnCancel.Text = "خروج"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'btnDaftarEnteghalat
        '
        Me.btnDaftarEnteghalat.Location = New System.Drawing.Point(235, 20)
        Me.btnDaftarEnteghalat.Name = "btnDaftarEnteghalat"
        Me.btnDaftarEnteghalat.Size = New System.Drawing.Size(160, 23)
        Me.btnDaftarEnteghalat.TabIndex = 9
        Me.btnDaftarEnteghalat.Text = "دفتر انتقالات"
        Me.btnDaftarEnteghalat.UseVisualStyleBackColor = True
        '
        'DP_rpt_Enteghalat
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(654, 196)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "DP_rpt_Enteghalat"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "گزارش انتقالات"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents mskAzTarikh As System.Windows.Forms.MaskedTextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents mskTaTarikh As System.Windows.Forms.MaskedTextBox
    Friend WithEvents rbSandogh As System.Windows.Forms.RadioButton
    Friend WithEvents rbBank As System.Windows.Forms.RadioButton
    Friend WithEvents lblHesab As System.Windows.Forms.Label
    Friend WithEvents cmbHesab As System.Windows.Forms.ComboBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnDaftarEnteghalat As System.Windows.Forms.Button

End Class
