<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmFO_GozareshFromPeygiri
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
        Me.cmbSharh = New System.Windows.Forms.ComboBox
        Me.mskTarikhPeigiryBady = New System.Windows.Forms.MaskedTextBox
        Me.MaskedTextBox1 = New System.Windows.Forms.MaskedTextBox
        Me.MaskedTextBox2 = New System.Windows.Forms.MaskedTextBox
        Me.MaskedTextBox3 = New System.Windows.Forms.MaskedTextBox
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.btnExit = New System.Windows.Forms.Button
        Me.btnView = New System.Windows.Forms.Button
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.lblNameMoshtary = New System.Windows.Forms.Label
        Me.txtCodeMoshtaryS = New System.Windows.Forms.TextBox
        Me.Label9 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'cmbSharh
        '
        Me.cmbSharh.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbSharh.FormattingEnabled = True
        Me.cmbSharh.Location = New System.Drawing.Point(10, 20)
        Me.cmbSharh.Name = "cmbSharh"
        Me.cmbSharh.Size = New System.Drawing.Size(293, 21)
        Me.cmbSharh.TabIndex = 4
        '
        'mskTarikhPeigiryBady
        '
        Me.mskTarikhPeigiryBady.AllowPromptAsInput = False
        Me.mskTarikhPeigiryBady.Location = New System.Drawing.Point(10, 51)
        Me.mskTarikhPeigiryBady.Mask = "####/##/##"
        Me.mskTarikhPeigiryBady.Name = "mskTarikhPeigiryBady"
        Me.mskTarikhPeigiryBady.PromptChar = Global.Microsoft.VisualBasic.ChrW(32)
        Me.mskTarikhPeigiryBady.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.mskTarikhPeigiryBady.Size = New System.Drawing.Size(83, 21)
        Me.mskTarikhPeigiryBady.TabIndex = 5
        Me.mskTarikhPeigiryBady.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals
        '
        'MaskedTextBox1
        '
        Me.MaskedTextBox1.AllowPromptAsInput = False
        Me.MaskedTextBox1.Location = New System.Drawing.Point(160, 51)
        Me.MaskedTextBox1.Mask = "####/##/##"
        Me.MaskedTextBox1.Name = "MaskedTextBox1"
        Me.MaskedTextBox1.PromptChar = Global.Microsoft.VisualBasic.ChrW(32)
        Me.MaskedTextBox1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.MaskedTextBox1.Size = New System.Drawing.Size(83, 21)
        Me.MaskedTextBox1.TabIndex = 6
        Me.MaskedTextBox1.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals
        '
        'MaskedTextBox2
        '
        Me.MaskedTextBox2.AllowPromptAsInput = False
        Me.MaskedTextBox2.Location = New System.Drawing.Point(160, 81)
        Me.MaskedTextBox2.Mask = "####/##/##"
        Me.MaskedTextBox2.Name = "MaskedTextBox2"
        Me.MaskedTextBox2.PromptChar = Global.Microsoft.VisualBasic.ChrW(32)
        Me.MaskedTextBox2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.MaskedTextBox2.Size = New System.Drawing.Size(83, 21)
        Me.MaskedTextBox2.TabIndex = 7
        Me.MaskedTextBox2.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals
        '
        'MaskedTextBox3
        '
        Me.MaskedTextBox3.AllowPromptAsInput = False
        Me.MaskedTextBox3.Location = New System.Drawing.Point(10, 81)
        Me.MaskedTextBox3.Mask = "####/##/##"
        Me.MaskedTextBox3.Name = "MaskedTextBox3"
        Me.MaskedTextBox3.PromptChar = Global.Microsoft.VisualBasic.ChrW(32)
        Me.MaskedTextBox3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.MaskedTextBox3.Size = New System.Drawing.Size(83, 21)
        Me.MaskedTextBox3.TabIndex = 8
        Me.MaskedTextBox3.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.btnExit)
        Me.GroupBox2.Controls.Add(Me.btnView)
        Me.GroupBox2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.GroupBox2.Location = New System.Drawing.Point(0, 143)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(410, 51)
        Me.GroupBox2.TabIndex = 9
        Me.GroupBox2.TabStop = False
        '
        'btnExit
        '
        Me.btnExit.Location = New System.Drawing.Point(117, 16)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(88, 27)
        Me.btnExit.TabIndex = 5
        Me.btnExit.Text = "خـــــروج"
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'btnView
        '
        Me.btnView.Location = New System.Drawing.Point(205, 16)
        Me.btnView.Name = "btnView"
        Me.btnView.Size = New System.Drawing.Size(88, 27)
        Me.btnView.TabIndex = 4
        Me.btnView.Text = "مشاهــده"
        Me.btnView.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.lblNameMoshtary)
        Me.GroupBox1.Controls.Add(Me.MaskedTextBox2)
        Me.GroupBox1.Controls.Add(Me.MaskedTextBox3)
        Me.GroupBox1.Controls.Add(Me.txtCodeMoshtaryS)
        Me.GroupBox1.Controls.Add(Me.Label9)
        Me.GroupBox1.Controls.Add(Me.MaskedTextBox1)
        Me.GroupBox1.Controls.Add(Me.mskTarikhPeigiryBady)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.cmbSharh)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(410, 147)
        Me.GroupBox1.TabIndex = 10
        Me.GroupBox1.TabStop = False
        '
        'lblNameMoshtary
        '
        Me.lblNameMoshtary.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblNameMoshtary.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNameMoshtary.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblNameMoshtary.Location = New System.Drawing.Point(10, 111)
        Me.lblNameMoshtary.Name = "lblNameMoshtary"
        Me.lblNameMoshtary.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.lblNameMoshtary.Size = New System.Drawing.Size(206, 21)
        Me.lblNameMoshtary.TabIndex = 136
        Me.lblNameMoshtary.Text = "  "
        Me.lblNameMoshtary.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtCodeMoshtaryS
        '
        Me.txtCodeMoshtaryS.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtCodeMoshtaryS.Location = New System.Drawing.Point(219, 111)
        Me.txtCodeMoshtaryS.MaxLength = 15
        Me.txtCodeMoshtaryS.Name = "txtCodeMoshtaryS"
        Me.txtCodeMoshtaryS.Size = New System.Drawing.Size(84, 21)
        Me.txtCodeMoshtaryS.TabIndex = 135
        Me.txtCodeMoshtaryS.Text = " "
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(309, 115)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(88, 13)
        Me.Label9.TabIndex = 134
        Me.Label9.Text = "نـــام مشتــــری :"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(309, 54)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(85, 13)
        Me.Label6.TabIndex = 130
        Me.Label6.Text = "تاریــخ پیگیـــری :"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(99, 54)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(44, 13)
        Me.Label5.TabIndex = 124
        Me.Label5.Text = "تا تاریخ :"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(248, 54)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(45, 13)
        Me.Label4.TabIndex = 122
        Me.Label4.Text = "از تاریخ :"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(309, 23)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(88, 13)
        Me.Label1.TabIndex = 137
        Me.Label1.Text = "علـــت پیگیـــری :"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(99, 84)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(44, 13)
        Me.Label2.TabIndex = 139
        Me.Label2.Text = "تا تاریخ :"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(248, 84)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(45, 13)
        Me.Label3.TabIndex = 138
        Me.Label3.Text = "از تاریخ :"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(309, 84)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(98, 13)
        Me.Label7.TabIndex = 140
        Me.Label7.Text = "تاریخ پیگیری بعدی :"
        '
        'frmFO_GozareshFromPeygiri
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(410, 194)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.GroupBox2)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.Name = "frmFO_GozareshFromPeygiri"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.RightToLeftLayout = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = " گزارش پیگیری"
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents cmbSharh As System.Windows.Forms.ComboBox
    Friend WithEvents mskTarikhPeigiryBady As System.Windows.Forms.MaskedTextBox
    Friend WithEvents MaskedTextBox1 As System.Windows.Forms.MaskedTextBox
    Friend WithEvents MaskedTextBox2 As System.Windows.Forms.MaskedTextBox
    Friend WithEvents MaskedTextBox3 As System.Windows.Forms.MaskedTextBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents btnView As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lblNameMoshtary As System.Windows.Forms.Label
    Friend WithEvents txtCodeMoshtaryS As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
End Class
