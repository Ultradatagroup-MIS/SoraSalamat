<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmGL_ShahrCode
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmGL_ShahrCode))
        Me.dbgTitr = New System.Windows.Forms.DataGrid
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.btnSearch = New System.Windows.Forms.Button
        Me.txtNameShahr = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.btnSaveChange = New System.Windows.Forms.Button
        Me.btnExit = New System.Windows.Forms.Button
        CType(Me.dbgTitr, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'dbgTitr
        '
        Me.dbgTitr.DataMember = ""
        Me.dbgTitr.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.dbgTitr.Location = New System.Drawing.Point(7, 15)
        Me.dbgTitr.Name = "dbgTitr"
        Me.dbgTitr.Size = New System.Drawing.Size(477, 348)
        Me.dbgTitr.TabIndex = 0
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.btnSearch)
        Me.GroupBox1.Controls.Add(Me.txtNameShahr)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.ImeMode = System.Windows.Forms.ImeMode.HangulFull
        Me.GroupBox1.Location = New System.Drawing.Point(8, 3)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(491, 49)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Ã” ÃÊ"
        '
        'btnSearch
        '
        Me.btnSearch.Image = CType(resources.GetObject("btnSearch.Image"), System.Drawing.Image)
        Me.btnSearch.Location = New System.Drawing.Point(18, 14)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(36, 29)
        Me.btnSearch.TabIndex = 2
        '
        'txtNameShahr
        '
        Me.txtNameShahr.Location = New System.Drawing.Point(152, 17)
        Me.txtNameShahr.Name = "txtNameShahr"
        Me.txtNameShahr.Size = New System.Drawing.Size(256, 21)
        Me.txtNameShahr.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(411, 21)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(52, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "‰«„ ‘Â— :"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.dbgTitr)
        Me.GroupBox2.Location = New System.Drawing.Point(8, 50)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(491, 373)
        Me.GroupBox2.TabIndex = 1
        Me.GroupBox2.TabStop = False
        '
        'btnSaveChange
        '
        Me.btnSaveChange.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSaveChange.Image = CType(resources.GetObject("btnSaveChange.Image"), System.Drawing.Image)
        Me.btnSaveChange.Location = New System.Drawing.Point(21, 429)
        Me.btnSaveChange.Name = "btnSaveChange"
        Me.btnSaveChange.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.btnSaveChange.Size = New System.Drawing.Size(26, 27)
        Me.btnSaveChange.TabIndex = 2
        Me.btnSaveChange.TabStop = False
        '
        'btnExit
        '
        Me.btnExit.Location = New System.Drawing.Point(189, 430)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(128, 27)
        Me.btnExit.TabIndex = 3
        Me.btnExit.Text = "Œ‹‹‹‹‹‹‹—ÊÃ"
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'frmGL_ShahrCode
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(507, 463)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.btnSaveChange)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.GroupBox2)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.ImeMode = System.Windows.Forms.ImeMode.Close
        Me.MaximizeBox = False
        Me.Name = "frmGL_ShahrCode"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "  òœ ‘‹Â—”‹ «‰ "
        CType(Me.dbgTitr, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents dbgTitr As System.Windows.Forms.DataGrid
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents txtNameShahr As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnSaveChange As System.Windows.Forms.Button
    Friend WithEvents btnSearch As System.Windows.Forms.Button
    Friend WithEvents btnExit As System.Windows.Forms.Button

End Class
