<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmFO_AdamTaeed
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
        Me.cmbElatAdamTaeed = New System.Windows.Forms.ComboBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.btnExit = New System.Windows.Forms.Button
        Me.btnAdamTaeed = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'cmbElatAdamTaeed
        '
        Me.cmbElatAdamTaeed.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbElatAdamTaeed.Location = New System.Drawing.Point(12, 12)
        Me.cmbElatAdamTaeed.MaxDropDownItems = 20
        Me.cmbElatAdamTaeed.Name = "cmbElatAdamTaeed"
        Me.cmbElatAdamTaeed.Size = New System.Drawing.Size(249, 21)
        Me.cmbElatAdamTaeed.TabIndex = 7
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(263, 15)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(35, 13)
        Me.Label5.TabIndex = 6
        Me.Label5.Text = "علت :"
        '
        'btnExit
        '
        Me.btnExit.AccessibleDescription = ""
        Me.btnExit.AccessibleName = ""
        Me.btnExit.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnExit.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnExit.Location = New System.Drawing.Point(62, 39)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(90, 25)
        Me.btnExit.TabIndex = 8
        Me.btnExit.Text = "خــــــروج"
        '
        'btnAdamTaeed
        '
        Me.btnAdamTaeed.AccessibleDescription = ""
        Me.btnAdamTaeed.AccessibleName = ""
        Me.btnAdamTaeed.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAdamTaeed.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnAdamTaeed.Location = New System.Drawing.Point(152, 39)
        Me.btnAdamTaeed.Name = "btnAdamTaeed"
        Me.btnAdamTaeed.Size = New System.Drawing.Size(90, 25)
        Me.btnAdamTaeed.TabIndex = 9
        Me.btnAdamTaeed.Text = "عــدم تاییــــد"
        '
        'frmFO_AdamTaeed
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(304, 70)
        Me.Controls.Add(Me.btnAdamTaeed)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.cmbElatAdamTaeed)
        Me.Controls.Add(Me.Label5)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmFO_AdamTaeed"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "علت عــم تاییـــد"
        Me.TopMost = True
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents cmbElatAdamTaeed As System.Windows.Forms.ComboBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents btnAdamTaeed As System.Windows.Forms.Button
End Class
