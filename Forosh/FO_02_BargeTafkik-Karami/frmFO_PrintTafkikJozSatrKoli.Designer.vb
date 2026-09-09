<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmFO_PrintTafkikJozSatrKoli
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
        Me.chksG1 = New System.Windows.Forms.CheckedListBox
        Me.btnReport = New System.Windows.Forms.Button
        Me.btnExit = New System.Windows.Forms.Button
        Me.lblShomarehTafkik = New System.Windows.Forms.Label
        Me.lblRanandeh = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'chksG1
        '
        Me.chksG1.Dock = System.Windows.Forms.DockStyle.Top
        Me.chksG1.FormattingEnabled = True
        Me.chksG1.Location = New System.Drawing.Point(0, 0)
        Me.chksG1.Name = "chksG1"
        Me.chksG1.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.chksG1.Size = New System.Drawing.Size(241, 109)
        Me.chksG1.TabIndex = 14
        '
        'btnReport
        '
        Me.btnReport.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.btnReport.Location = New System.Drawing.Point(124, 170)
        Me.btnReport.Name = "btnReport"
        Me.btnReport.Size = New System.Drawing.Size(117, 43)
        Me.btnReport.TabIndex = 15
        Me.btnReport.Text = "چــــــــاپ"
        Me.btnReport.UseVisualStyleBackColor = True
        '
        'btnExit
        '
        Me.btnExit.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.btnExit.Location = New System.Drawing.Point(2, 170)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(118, 43)
        Me.btnExit.TabIndex = 16
        Me.btnExit.Text = "خـــــــروج"
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'lblShomarehTafkik
        '
        Me.lblShomarehTafkik.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblShomarehTafkik.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.lblShomarehTafkik.Location = New System.Drawing.Point(5, 116)
        Me.lblShomarehTafkik.Name = "lblShomarehTafkik"
        Me.lblShomarehTafkik.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblShomarehTafkik.Size = New System.Drawing.Size(133, 20)
        Me.lblShomarehTafkik.TabIndex = 17
        Me.lblShomarehTafkik.Text = "Label1"
        Me.lblShomarehTafkik.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblRanandeh
        '
        Me.lblRanandeh.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblRanandeh.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.lblRanandeh.Location = New System.Drawing.Point(5, 141)
        Me.lblRanandeh.Name = "lblRanandeh"
        Me.lblRanandeh.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.lblRanandeh.Size = New System.Drawing.Size(133, 23)
        Me.lblRanandeh.TabIndex = 18
        Me.lblRanandeh.Text = "Label1"
        Me.lblRanandeh.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(141, 120)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(96, 13)
        Me.Label1.TabIndex = 19
        Me.Label1.Text = "شماره برگه تفکیک :"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(142, 147)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(89, 13)
        Me.Label2.TabIndex = 20
        Me.Label2.Text = "نام راننـــــــــــده :"
        '
        'frmFO_PrintTafkikJozSatrKoli
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(241, 213)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.lblRanandeh)
        Me.Controls.Add(Me.lblShomarehTafkik)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.btnReport)
        Me.Controls.Add(Me.chksG1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmFO_PrintTafkikJozSatrKoli"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = " چـــــاپ کلی"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents chksG1 As System.Windows.Forms.CheckedListBox
    Friend WithEvents btnReport As System.Windows.Forms.Button
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents lblShomarehTafkik As System.Windows.Forms.Label
    Friend WithEvents lblRanandeh As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
End Class
