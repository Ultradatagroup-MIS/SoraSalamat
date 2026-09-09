<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmFO_NoePardakht
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
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.cmbNoePardakht = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.btnSabt = New System.Windows.Forms.Button()
        Me.btnExit = New System.Windows.Forms.Button()
        Me.lblModatCheck = New System.Windows.Forms.Label()
        Me.txtModatCheck = New System.Windows.Forms.TextBox()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.lblModatCheck)
        Me.GroupBox1.Controls.Add(Me.txtModatCheck)
        Me.GroupBox1.Controls.Add(Me.cmbNoePardakht)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(391, 58)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        '
        'cmbNoePardakht
        '
        Me.cmbNoePardakht.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbNoePardakht.Location = New System.Drawing.Point(162, 22)
        Me.cmbNoePardakht.MaxDropDownItems = 20
        Me.cmbNoePardakht.Name = "cmbNoePardakht"
        Me.cmbNoePardakht.Size = New System.Drawing.Size(134, 21)
        Me.cmbNoePardakht.TabIndex = 25
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(302, 25)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(83, 13)
        Me.Label1.TabIndex = 24
        Me.Label1.Text = "نـــوع پرداخـــت :"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.btnSabt)
        Me.GroupBox2.Controls.Add(Me.btnExit)
        Me.GroupBox2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.GroupBox2.Location = New System.Drawing.Point(0, 54)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(391, 49)
        Me.GroupBox2.TabIndex = 1
        Me.GroupBox2.TabStop = False
        '
        'btnSabt
        '
        Me.btnSabt.Location = New System.Drawing.Point(196, 18)
        Me.btnSabt.Name = "btnSabt"
        Me.btnSabt.Size = New System.Drawing.Size(78, 25)
        Me.btnSabt.TabIndex = 25
        Me.btnSabt.Text = "ثبــت"
        '
        'btnExit
        '
        Me.btnExit.Location = New System.Drawing.Point(116, 18)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(78, 25)
        Me.btnExit.TabIndex = 24
        Me.btnExit.Text = "خروج"
        '
        'lblModatCheck
        '
        Me.lblModatCheck.AutoSize = True
        Me.lblModatCheck.Location = New System.Drawing.Point(84, 25)
        Me.lblModatCheck.Name = "lblModatCheck"
        Me.lblModatCheck.Size = New System.Drawing.Size(56, 13)
        Me.lblModatCheck.TabIndex = 26
        Me.lblModatCheck.Text = "مدت چک :"
        Me.lblModatCheck.Visible = False
        '
        'txtModatCheck
        '
        Me.txtModatCheck.Location = New System.Drawing.Point(30, 21)
        Me.txtModatCheck.MaxLength = 3
        Me.txtModatCheck.Name = "txtModatCheck"
        Me.txtModatCheck.Size = New System.Drawing.Size(54, 21)
        Me.txtModatCheck.TabIndex = 27
        '
        'frmFO_NoePardakht
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(391, 103)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.GroupBox2)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.MinimizeBox = False
        Me.Name = "frmFO_NoePardakht"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.RightToLeftLayout = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "تغییر نوع پرداخت"
        Me.TopMost = True
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents cmbNoePardakht As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnSabt As System.Windows.Forms.Button
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents lblModatCheck As System.Windows.Forms.Label
    Friend WithEvents txtModatCheck As System.Windows.Forms.TextBox
End Class
