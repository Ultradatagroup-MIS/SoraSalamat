<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class EtellatResid
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
        Me.cmbAnbar = New System.Windows.Forms.ComboBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.lblTaminKonandeh = New System.Windows.Forms.Label()
        Me.cmbNameTaminKonandeh = New System.Windows.Forms.ComboBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.btnSaveSanad = New System.Windows.Forms.Button()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'cmbAnbar
        '
        Me.cmbAnbar.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbAnbar.Location = New System.Drawing.Point(266, 16)
        Me.cmbAnbar.MaxDropDownItems = 20
        Me.cmbAnbar.Name = "cmbAnbar"
        Me.cmbAnbar.Size = New System.Drawing.Size(159, 21)
        Me.cmbAnbar.TabIndex = 4
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(429, 19)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Label4.Size = New System.Drawing.Size(30, 13)
        Me.Label4.TabIndex = 5
        Me.Label4.Text = "انبار:"
        '
        'lblTaminKonandeh
        '
        Me.lblTaminKonandeh.AutoSize = True
        Me.lblTaminKonandeh.Location = New System.Drawing.Point(172, 19)
        Me.lblTaminKonandeh.Name = "lblTaminKonandeh"
        Me.lblTaminKonandeh.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.lblTaminKonandeh.Size = New System.Drawing.Size(63, 13)
        Me.lblTaminKonandeh.TabIndex = 10
        Me.lblTaminKonandeh.Text = "تامین کننده:"
        '
        'cmbNameTaminKonandeh
        '
        Me.cmbNameTaminKonandeh.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbNameTaminKonandeh.Location = New System.Drawing.Point(10, 16)
        Me.cmbNameTaminKonandeh.MaxDropDownItems = 20
        Me.cmbNameTaminKonandeh.Name = "cmbNameTaminKonandeh"
        Me.cmbNameTaminKonandeh.Size = New System.Drawing.Size(159, 21)
        Me.cmbNameTaminKonandeh.TabIndex = 9
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.btnSaveSanad)
        Me.GroupBox1.Controls.Add(Me.cmbNameTaminKonandeh)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.cmbAnbar)
        Me.GroupBox1.Controls.Add(Me.lblTaminKonandeh)
        Me.GroupBox1.Location = New System.Drawing.Point(8, 7)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(474, 84)
        Me.GroupBox1.TabIndex = 11
        Me.GroupBox1.TabStop = False
        '
        'btnSaveSanad
        '
        Me.btnSaveSanad.AccessibleDescription = ""
        Me.btnSaveSanad.AccessibleName = ""
        Me.btnSaveSanad.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnSaveSanad.Location = New System.Drawing.Point(189, 48)
        Me.btnSaveSanad.Name = "btnSaveSanad"
        Me.btnSaveSanad.Size = New System.Drawing.Size(80, 23)
        Me.btnSaveSanad.TabIndex = 11
        Me.btnSaveSanad.Text = "&ذخيره"
        '
        'EtellatResid
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(492, 96)
        Me.Controls.Add(Me.GroupBox1)
        Me.Name = "EtellatResid"
        Me.Text = "انتخاب انبار و تامین کننده"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents cmbAnbar As System.Windows.Forms.ComboBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents lblTaminKonandeh As System.Windows.Forms.Label
    Friend WithEvents cmbNameTaminKonandeh As System.Windows.Forms.ComboBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents btnSaveSanad As System.Windows.Forms.Button
End Class
