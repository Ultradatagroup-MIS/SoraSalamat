<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmFO_TolidKonandeh
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
        Me.dbgTitr = New System.Windows.Forms.DataGrid
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.txtCodeTolidKonandeh = New System.Windows.Forms.TextBox
        Me.txtNameTolidKonandeh = New System.Windows.Forms.TextBox
        Me.cmbsShahr = New System.Windows.Forms.ComboBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.cmbsOstan = New System.Windows.Forms.ComboBox
        Me.cmbNameShahr = New System.Windows.Forms.Label
        Me.cmbsKeshvar = New System.Windows.Forms.ComboBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.btnRefresh = New System.Windows.Forms.Button
        Me.btnUpdate = New System.Windows.Forms.Button
        Me.btnExit = New System.Windows.Forms.Button
        Me.btnSearch = New System.Windows.Forms.Button
        Me.btnDelete = New System.Windows.Forms.Button
        Me.btnCancel = New System.Windows.Forms.Button
        CType(Me.dbgTitr, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'dbgTitr
        '
        Me.dbgTitr.DataMember = ""
        Me.dbgTitr.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.dbgTitr.Location = New System.Drawing.Point(0, 2)
        Me.dbgTitr.Name = "dbgTitr"
        Me.dbgTitr.Size = New System.Drawing.Size(745, 10)
        Me.dbgTitr.TabIndex = 0
        Me.dbgTitr.Visible = False
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.txtCodeTolidKonandeh)
        Me.GroupBox1.Controls.Add(Me.txtNameTolidKonandeh)
        Me.GroupBox1.Controls.Add(Me.cmbsShahr)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.cmbsOstan)
        Me.GroupBox1.Controls.Add(Me.cmbNameShahr)
        Me.GroupBox1.Controls.Add(Me.cmbsKeshvar)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Location = New System.Drawing.Point(0, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(745, 103)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = False
        '
        'txtCodeTolidKonandeh
        '
        Me.txtCodeTolidKonandeh.Location = New System.Drawing.Point(252, 18)
        Me.txtCodeTolidKonandeh.MaxLength = 4
        Me.txtCodeTolidKonandeh.Name = "txtCodeTolidKonandeh"
        Me.txtCodeTolidKonandeh.Size = New System.Drawing.Size(165, 21)
        Me.txtCodeTolidKonandeh.TabIndex = 3
        '
        'txtNameTolidKonandeh
        '
        Me.txtNameTolidKonandeh.Location = New System.Drawing.Point(494, 18)
        Me.txtNameTolidKonandeh.MaxLength = 50
        Me.txtNameTolidKonandeh.Name = "txtNameTolidKonandeh"
        Me.txtNameTolidKonandeh.Size = New System.Drawing.Size(165, 21)
        Me.txtNameTolidKonandeh.TabIndex = 1
        '
        'cmbsShahr
        '
        Me.cmbsShahr.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbsShahr.FormattingEnabled = True
        Me.cmbsShahr.Location = New System.Drawing.Point(23, 58)
        Me.cmbsShahr.Name = "cmbsShahr"
        Me.cmbsShahr.Size = New System.Drawing.Size(165, 21)
        Me.cmbsShahr.TabIndex = 9
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(661, 62)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(53, 13)
        Me.Label5.TabIndex = 4
        Me.Label5.Text = "نام کشور:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(419, 62)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(55, 13)
        Me.Label4.TabIndex = 6
        Me.Label4.Text = "نام استان:"
        '
        'cmbsOstan
        '
        Me.cmbsOstan.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbsOstan.FormattingEnabled = True
        Me.cmbsOstan.Location = New System.Drawing.Point(252, 58)
        Me.cmbsOstan.Name = "cmbsOstan"
        Me.cmbsOstan.Size = New System.Drawing.Size(165, 21)
        Me.cmbsOstan.TabIndex = 7
        '
        'cmbNameShahr
        '
        Me.cmbNameShahr.AutoSize = True
        Me.cmbNameShahr.Location = New System.Drawing.Point(190, 62)
        Me.cmbNameShahr.Name = "cmbNameShahr"
        Me.cmbNameShahr.Size = New System.Drawing.Size(49, 13)
        Me.cmbNameShahr.TabIndex = 8
        Me.cmbNameShahr.Text = "نام شهر:"
        '
        'cmbsKeshvar
        '
        Me.cmbsKeshvar.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbsKeshvar.FormattingEnabled = True
        Me.cmbsKeshvar.Location = New System.Drawing.Point(494, 58)
        Me.cmbsKeshvar.Name = "cmbsKeshvar"
        Me.cmbsKeshvar.Size = New System.Drawing.Size(165, 21)
        Me.cmbsKeshvar.TabIndex = 5
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.ForeColor = System.Drawing.Color.Maroon
        Me.Label2.Location = New System.Drawing.Point(661, 22)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(75, 13)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "نام تولید کننده:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.ForeColor = System.Drawing.Color.Maroon
        Me.Label1.Location = New System.Drawing.Point(419, 22)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(25, 13)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "کد :"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.btnRefresh)
        Me.GroupBox2.Controls.Add(Me.btnUpdate)
        Me.GroupBox2.Controls.Add(Me.btnExit)
        Me.GroupBox2.Controls.Add(Me.btnSearch)
        Me.GroupBox2.Controls.Add(Me.btnDelete)
        Me.GroupBox2.Controls.Add(Me.btnCancel)
        Me.GroupBox2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.GroupBox2.Location = New System.Drawing.Point(0, 117)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(745, 54)
        Me.GroupBox2.TabIndex = 2
        Me.GroupBox2.TabStop = False
        '
        'btnRefresh
        '
        Me.btnRefresh.Location = New System.Drawing.Point(141, 19)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(91, 23)
        Me.btnRefresh.TabIndex = 4
        Me.btnRefresh.Text = "بازخوانی"
        Me.btnRefresh.UseVisualStyleBackColor = True
        '
        'btnUpdate
        '
        Me.btnUpdate.Location = New System.Drawing.Point(637, 19)
        Me.btnUpdate.Name = "btnUpdate"
        Me.btnUpdate.Size = New System.Drawing.Size(91, 23)
        Me.btnUpdate.TabIndex = 0
        Me.btnUpdate.Text = "ذخیره"
        Me.btnUpdate.UseVisualStyleBackColor = True
        '
        'btnExit
        '
        Me.btnExit.Location = New System.Drawing.Point(17, 19)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(91, 23)
        Me.btnExit.TabIndex = 5
        Me.btnExit.Text = "خروج"
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'btnSearch
        '
        Me.btnSearch.Location = New System.Drawing.Point(513, 19)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(91, 23)
        Me.btnSearch.TabIndex = 1
        Me.btnSearch.Text = "جستجو"
        Me.btnSearch.UseVisualStyleBackColor = True
        '
        'btnDelete
        '
        Me.btnDelete.Location = New System.Drawing.Point(265, 19)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(91, 23)
        Me.btnDelete.TabIndex = 3
        Me.btnDelete.Text = "حذف"
        Me.btnDelete.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        Me.btnCancel.Location = New System.Drawing.Point(389, 19)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(91, 23)
        Me.btnCancel.TabIndex = 2
        Me.btnCancel.Text = "صرفنظر"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'FrmFO_TolidKonandeh
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(745, 171)
        Me.Controls.Add(Me.dbgTitr)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.KeyPreview = True
        Me.Name = "FrmFO_TolidKonandeh"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "تولید کننده"
        CType(Me.dbgTitr, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents dbgTitr As System.Windows.Forms.DataGrid
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents txtCodeTolidKonandeh As System.Windows.Forms.TextBox
    Friend WithEvents txtNameTolidKonandeh As System.Windows.Forms.TextBox
    Friend WithEvents cmbsShahr As System.Windows.Forms.ComboBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents cmbsOstan As System.Windows.Forms.ComboBox
    Friend WithEvents cmbNameShahr As System.Windows.Forms.Label
    Friend WithEvents cmbsKeshvar As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents btnRefresh As System.Windows.Forms.Button
    Friend WithEvents btnUpdate As System.Windows.Forms.Button
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents btnSearch As System.Windows.Forms.Button
    Friend WithEvents btnDelete As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
End Class
