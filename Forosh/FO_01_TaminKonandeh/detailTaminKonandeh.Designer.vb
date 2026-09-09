<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class detailTaminKonandeh
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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtTozihat = New System.Windows.Forms.TextBox()
        Me.btnUpdate = New System.Windows.Forms.Button()
        Me.lblNameTaminKonandeh = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(387, 14)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(82, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = " نام تامین کننده "
        '
        'txtTozihat
        '
        Me.txtTozihat.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.txtTozihat.Location = New System.Drawing.Point(12, 42)
        Me.txtTozihat.Multiline = True
        Me.txtTozihat.Name = "txtTozihat"
        Me.txtTozihat.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txtTozihat.Size = New System.Drawing.Size(457, 169)
        Me.txtTozihat.TabIndex = 2
        '
        'btnUpdate
        '
        Me.btnUpdate.AccessibleDescription = "Update Button"
        Me.btnUpdate.AccessibleName = "Update Button"
        Me.btnUpdate.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnUpdate.Location = New System.Drawing.Point(185, 227)
        Me.btnUpdate.Name = "btnUpdate"
        Me.btnUpdate.Size = New System.Drawing.Size(86, 22)
        Me.btnUpdate.TabIndex = 3
        Me.btnUpdate.Text = "&ذخيره"
        '
        'lblNameTaminKonandeh
        '
        Me.lblNameTaminKonandeh.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblNameTaminKonandeh.Location = New System.Drawing.Point(70, 9)
        Me.lblNameTaminKonandeh.Name = "lblNameTaminKonandeh"
        Me.lblNameTaminKonandeh.Size = New System.Drawing.Size(291, 21)
        Me.lblNameTaminKonandeh.TabIndex = 4
        Me.lblNameTaminKonandeh.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'detailTaminKonandeh
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(484, 261)
        Me.Controls.Add(Me.lblNameTaminKonandeh)
        Me.Controls.Add(Me.btnUpdate)
        Me.Controls.Add(Me.txtTozihat)
        Me.Controls.Add(Me.Label1)
        Me.Name = "detailTaminKonandeh"
        Me.Text = "اطلاعات تامین کننده"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtTozihat As System.Windows.Forms.TextBox
    Friend WithEvents btnUpdate As System.Windows.Forms.Button
    Friend WithEvents lblNameTaminKonandeh As System.Windows.Forms.Label
End Class
