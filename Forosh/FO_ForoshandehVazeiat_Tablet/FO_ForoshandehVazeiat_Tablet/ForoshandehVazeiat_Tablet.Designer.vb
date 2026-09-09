<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_ForoshandehVazeiat_Tablet
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
        Me.lstForoshandeh = New System.Windows.Forms.ListBox()
        Me.gbVazeiatTablet = New System.Windows.Forms.GroupBox()
        Me.rbFaal = New System.Windows.Forms.RadioButton()
        Me.rbGheyrFaal = New System.Windows.Forms.RadioButton()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.gbVazeiatTablet.SuspendLayout()
        Me.SuspendLayout()
        '
        'lstForoshandeh
        '
        Me.lstForoshandeh.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.lstForoshandeh.FormattingEnabled = True
        Me.lstForoshandeh.Location = New System.Drawing.Point(12, 12)
        Me.lstForoshandeh.Name = "lstForoshandeh"
        Me.lstForoshandeh.Size = New System.Drawing.Size(190, 264)
        Me.lstForoshandeh.TabIndex = 0
        '
        'gbVazeiatTablet
        '
        Me.gbVazeiatTablet.Controls.Add(Me.btnSave)
        Me.gbVazeiatTablet.Controls.Add(Me.rbGheyrFaal)
        Me.gbVazeiatTablet.Controls.Add(Me.rbFaal)
        Me.gbVazeiatTablet.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.gbVazeiatTablet.Location = New System.Drawing.Point(208, 5)
        Me.gbVazeiatTablet.Name = "gbVazeiatTablet"
        Me.gbVazeiatTablet.Size = New System.Drawing.Size(200, 271)
        Me.gbVazeiatTablet.TabIndex = 1
        Me.gbVazeiatTablet.TabStop = False
        Me.gbVazeiatTablet.Text = "وضعیت فروشنده تبلت"
        '
        'rbFaal
        '
        Me.rbFaal.AutoSize = True
        Me.rbFaal.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbFaal.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.rbFaal.Location = New System.Drawing.Point(80, 62)
        Me.rbFaal.Name = "rbFaal"
        Me.rbFaal.Size = New System.Drawing.Size(55, 20)
        Me.rbFaal.TabIndex = 2
        Me.rbFaal.TabStop = True
        Me.rbFaal.Text = "فعال"
        Me.rbFaal.UseVisualStyleBackColor = True
        '
        'rbGheyrFaal
        '
        Me.rbGheyrFaal.AutoSize = True
        Me.rbGheyrFaal.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbGheyrFaal.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.rbGheyrFaal.Location = New System.Drawing.Point(55, 105)
        Me.rbGheyrFaal.Name = "rbGheyrFaal"
        Me.rbGheyrFaal.Size = New System.Drawing.Size(80, 20)
        Me.rbGheyrFaal.TabIndex = 3
        Me.rbGheyrFaal.TabStop = True
        Me.rbGheyrFaal.Text = "غیر فعال"
        Me.rbGheyrFaal.UseVisualStyleBackColor = True
        '
        'btnSave
        '
        Me.btnSave.Font = New System.Drawing.Font("B Mitra", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.btnSave.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.btnSave.Location = New System.Drawing.Point(57, 173)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(78, 36)
        Me.btnSave.TabIndex = 4
        Me.btnSave.Text = "ذخیره"
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'frm_ForoshandehVazeiat_Tablet
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(416, 290)
        Me.Controls.Add(Me.gbVazeiatTablet)
        Me.Controls.Add(Me.lstForoshandeh)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.Name = "frm_ForoshandehVazeiat_Tablet"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.RightToLeftLayout = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "تغییر وضعیت فروشنده تبلت"
        Me.gbVazeiatTablet.ResumeLayout(False)
        Me.gbVazeiatTablet.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lstForoshandeh As System.Windows.Forms.ListBox
    Friend WithEvents gbVazeiatTablet As System.Windows.Forms.GroupBox
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents rbGheyrFaal As System.Windows.Forms.RadioButton
    Friend WithEvents rbFaal As System.Windows.Forms.RadioButton

End Class
