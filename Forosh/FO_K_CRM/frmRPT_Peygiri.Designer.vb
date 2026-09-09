<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmRPT_Peigiry
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
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.btnPrint = New System.Windows.Forms.Button
        Me.btnExit = New System.Windows.Forms.Button
        Me.lblAzTarikh = New System.Windows.Forms.Label
        Me.mskAzTarikhRpt = New System.Windows.Forms.MaskedTextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.mskTaTarikhRpt = New System.Windows.Forms.MaskedTextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.cmbSharh = New System.Windows.Forms.ComboBox
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.Label2)
        Me.GroupBox2.Controls.Add(Me.cmbSharh)
        Me.GroupBox2.Controls.Add(Me.Label1)
        Me.GroupBox2.Controls.Add(Me.mskTaTarikhRpt)
        Me.GroupBox2.Controls.Add(Me.lblAzTarikh)
        Me.GroupBox2.Controls.Add(Me.mskAzTarikhRpt)
        Me.GroupBox2.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupBox2.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(404, 88)
        Me.GroupBox2.TabIndex = 2
        Me.GroupBox2.TabStop = False
        '
        'btnPrint
        '
        Me.btnPrint.Location = New System.Drawing.Point(198, 16)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(75, 25)
        Me.btnPrint.TabIndex = 21
        Me.btnPrint.Text = "چـــــــاپ"
        '
        'btnExit
        '
        Me.btnExit.Location = New System.Drawing.Point(117, 16)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(75, 25)
        Me.btnExit.TabIndex = 20
        Me.btnExit.Text = "خــــروج"
        '
        'lblAzTarikh
        '
        Me.lblAzTarikh.AutoSize = True
        Me.lblAzTarikh.Location = New System.Drawing.Point(296, 55)
        Me.lblAzTarikh.Name = "lblAzTarikh"
        Me.lblAzTarikh.Size = New System.Drawing.Size(54, 13)
        Me.lblAzTarikh.TabIndex = 5
        Me.lblAzTarikh.Text = "از  تاریــخ :"
        '
        'mskAzTarikhRpt
        '
        Me.mskAzTarikhRpt.AllowPromptAsInput = False
        Me.mskAzTarikhRpt.Location = New System.Drawing.Point(209, 52)
        Me.mskAzTarikhRpt.Mask = "####/##/##"
        Me.mskAzTarikhRpt.Name = "mskAzTarikhRpt"
        Me.mskAzTarikhRpt.PromptChar = Global.Microsoft.VisualBasic.ChrW(32)
        Me.mskAzTarikhRpt.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.mskAzTarikhRpt.Size = New System.Drawing.Size(83, 21)
        Me.mskAzTarikhRpt.TabIndex = 2
        Me.mskAzTarikhRpt.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(142, 55)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(53, 13)
        Me.Label1.TabIndex = 23
        Me.Label1.Text = "تا  تاریــخ :"
        '
        'mskTaTarikhRpt
        '
        Me.mskTaTarikhRpt.AllowPromptAsInput = False
        Me.mskTaTarikhRpt.Location = New System.Drawing.Point(55, 52)
        Me.mskTaTarikhRpt.Mask = "####/##/##"
        Me.mskTaTarikhRpt.Name = "mskTaTarikhRpt"
        Me.mskTaTarikhRpt.PromptChar = Global.Microsoft.VisualBasic.ChrW(32)
        Me.mskTaTarikhRpt.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.mskTaTarikhRpt.Size = New System.Drawing.Size(83, 21)
        Me.mskTaTarikhRpt.TabIndex = 22
        Me.mskTaTarikhRpt.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(316, 23)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(70, 13)
        Me.Label2.TabIndex = 24
        Me.Label2.Text = "علت پیگیری :"
        '
        'cmbSharh
        '
        Me.cmbSharh.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbSharh.FormattingEnabled = True
        Me.cmbSharh.Location = New System.Drawing.Point(18, 20)
        Me.cmbSharh.Name = "cmbSharh"
        Me.cmbSharh.Size = New System.Drawing.Size(292, 21)
        Me.cmbSharh.TabIndex = 25
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.btnExit)
        Me.GroupBox1.Controls.Add(Me.btnPrint)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.GroupBox1.Location = New System.Drawing.Point(0, 85)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(404, 47)
        Me.GroupBox1.TabIndex = 3
        Me.GroupBox1.TabStop = False
        '
        'frmRPT_Peigiry
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(404, 132)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.Name = "frmRPT_Peigiry"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.RightToLeftLayout = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "frmRPT_Peygiri"
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents btnPrint As System.Windows.Forms.Button
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents lblAzTarikh As System.Windows.Forms.Label
    Friend WithEvents mskAzTarikhRpt As System.Windows.Forms.MaskedTextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents mskTaTarikhRpt As System.Windows.Forms.MaskedTextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cmbSharh As System.Windows.Forms.ComboBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
End Class
