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
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.btnPrint)
        Me.GroupBox2.Controls.Add(Me.btnExit)
        Me.GroupBox2.Controls.Add(Me.lblAzTarikh)
        Me.GroupBox2.Controls.Add(Me.mskAzTarikhRpt)
        Me.GroupBox2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox2.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(358, 56)
        Me.GroupBox2.TabIndex = 1
        Me.GroupBox2.TabStop = False
        '
        'btnPrint
        '
        Me.btnPrint.Location = New System.Drawing.Point(94, 18)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(75, 25)
        Me.btnPrint.TabIndex = 21
        Me.btnPrint.Text = "چـــــــاپ"
        '
        'btnExit
        '
        Me.btnExit.Location = New System.Drawing.Point(13, 18)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(75, 25)
        Me.btnExit.TabIndex = 20
        Me.btnExit.Text = "خــــروج"
        '
        'lblAzTarikh
        '
        Me.lblAzTarikh.AutoSize = True
        Me.lblAzTarikh.Location = New System.Drawing.Point(305, 23)
        Me.lblAzTarikh.Name = "lblAzTarikh"
        Me.lblAzTarikh.Size = New System.Drawing.Size(44, 13)
        Me.lblAzTarikh.TabIndex = 5
        Me.lblAzTarikh.Text = " تاریــخ :"
        '
        'mskAzTarikhRpt
        '
        Me.mskAzTarikhRpt.AllowPromptAsInput = False
        Me.mskAzTarikhRpt.Location = New System.Drawing.Point(218, 20)
        Me.mskAzTarikhRpt.Mask = "####/##/##"
        Me.mskAzTarikhRpt.Name = "mskAzTarikhRpt"
        Me.mskAzTarikhRpt.PromptChar = Global.Microsoft.VisualBasic.ChrW(32)
        Me.mskAzTarikhRpt.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.mskAzTarikhRpt.Size = New System.Drawing.Size(83, 21)
        Me.mskAzTarikhRpt.TabIndex = 2
        Me.mskAzTarikhRpt.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals
        '
        'frmRPT_Peigiry
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(358, 56)
        Me.Controls.Add(Me.GroupBox2)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "frmRPT_Peigiry"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.RightToLeftLayout = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "گزارش پیگیری"
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents mskAzTarikhRpt As System.Windows.Forms.MaskedTextBox
    Friend WithEvents lblAzTarikh As System.Windows.Forms.Label
    Friend WithEvents btnPrint As System.Windows.Forms.Button
    Friend WithEvents btnExit As System.Windows.Forms.Button
End Class
