<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmRassCheck
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
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.btnExit = New System.Windows.Forms.Button
        Me.btnTaed = New System.Windows.Forms.Button
        Me.cmbBankSanad = New System.Windows.Forms.ComboBox
        Me.Label9 = New System.Windows.Forms.Label
        Me.rbBaForm = New System.Windows.Forms.RadioButton
        Me.rbBedoneForm = New System.Windows.Forms.RadioButton
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.rbBedoneForm)
        Me.GroupBox1.Controls.Add(Me.rbBaForm)
        Me.GroupBox1.Controls.Add(Me.btnExit)
        Me.GroupBox1.Controls.Add(Me.btnTaed)
        Me.GroupBox1.Controls.Add(Me.cmbBankSanad)
        Me.GroupBox1.Controls.Add(Me.Label9)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 3)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(371, 129)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        '
        'btnExit
        '
        Me.btnExit.Location = New System.Drawing.Point(102, 87)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(80, 24)
        Me.btnExit.TabIndex = 3
        Me.btnExit.Text = "خروچ"
        '
        'btnTaed
        '
        Me.btnTaed.Location = New System.Drawing.Point(188, 87)
        Me.btnTaed.Name = "btnTaed"
        Me.btnTaed.Size = New System.Drawing.Size(80, 24)
        Me.btnTaed.TabIndex = 2
        Me.btnTaed.Text = "تأیید"
        '
        'cmbBankSanad
        '
        Me.cmbBankSanad.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbBankSanad.Location = New System.Drawing.Point(6, 19)
        Me.cmbBankSanad.Name = "cmbBankSanad"
        Me.cmbBankSanad.Size = New System.Drawing.Size(325, 21)
        Me.cmbBankSanad.TabIndex = 1
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(334, 22)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(31, 13)
        Me.Label9.TabIndex = 0
        Me.Label9.Text = "بانک:"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'rbBaForm
        '
        Me.rbBaForm.AutoSize = True
        Me.rbBaForm.Location = New System.Drawing.Point(280, 46)
        Me.rbBaForm.Name = "rbBaForm"
        Me.rbBaForm.Size = New System.Drawing.Size(51, 17)
        Me.rbBaForm.TabIndex = 4
        Me.rbBaForm.TabStop = True
        Me.rbBaForm.Text = "با فرم"
        Me.rbBaForm.UseVisualStyleBackColor = True
        '
        'rbBedoneForm
        '
        Me.rbBedoneForm.AutoSize = True
        Me.rbBedoneForm.Location = New System.Drawing.Point(265, 64)
        Me.rbBedoneForm.Name = "rbBedoneForm"
        Me.rbBedoneForm.Size = New System.Drawing.Size(66, 17)
        Me.rbBedoneForm.TabIndex = 4
        Me.rbBedoneForm.TabStop = True
        Me.rbBedoneForm.Text = "بدون فرم"
        Me.rbBedoneForm.UseVisualStyleBackColor = True
        '
        'frmRassCheck
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(396, 141)
        Me.Controls.Add(Me.GroupBox1)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "frmRassCheck"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "بانک"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents cmbBankSanad As System.Windows.Forms.ComboBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents btnTaed As System.Windows.Forms.Button
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents rbBedoneForm As System.Windows.Forms.RadioButton
    Friend WithEvents rbBaForm As System.Windows.Forms.RadioButton
End Class
