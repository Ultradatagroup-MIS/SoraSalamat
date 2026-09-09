<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmFO_ChangeTafkikInfo
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
        Me.cmbMamorPakhsh = New System.Windows.Forms.ComboBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.btnExit = New System.Windows.Forms.Button()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.mskTarikhPishbiniErsal = New System.Windows.Forms.MaskedTextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.cmbMashinTozie = New System.Windows.Forms.ComboBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.cmbRanandehTozie = New System.Windows.Forms.ComboBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.txtShomarehTafkik = New System.Windows.Forms.TextBox()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.txtShomarehTafkik)
        Me.GroupBox1.Controls.Add(Me.cmbMamorPakhsh)
        Me.GroupBox1.Controls.Add(Me.Label11)
        Me.GroupBox1.Controls.Add(Me.btnExit)
        Me.GroupBox1.Controls.Add(Me.btnSave)
        Me.GroupBox1.Controls.Add(Me.mskTarikhPishbiniErsal)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.cmbMashinTozie)
        Me.GroupBox1.Controls.Add(Me.Label10)
        Me.GroupBox1.Controls.Add(Me.cmbRanandehTozie)
        Me.GroupBox1.Controls.Add(Me.Label9)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(723, 83)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        '
        'cmbMamorPakhsh
        '
        Me.cmbMamorPakhsh.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbMamorPakhsh.Location = New System.Drawing.Point(323, 23)
        Me.cmbMamorPakhsh.MaxDropDownItems = 20
        Me.cmbMamorPakhsh.Name = "cmbMamorPakhsh"
        Me.cmbMamorPakhsh.Size = New System.Drawing.Size(196, 21)
        Me.cmbMamorPakhsh.TabIndex = 35
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(520, 26)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(68, 13)
        Me.Label11.TabIndex = 34
        Me.Label11.Text = " مامور پخش :"
        '
        'btnExit
        '
        Me.btnExit.Location = New System.Drawing.Point(6, 47)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(79, 25)
        Me.btnExit.TabIndex = 33
        Me.btnExit.Text = "خــروج"
        '
        'btnSave
        '
        Me.btnSave.Location = New System.Drawing.Point(6, 20)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(79, 25)
        Me.btnSave.TabIndex = 32
        Me.btnSave.Text = "ذخیــره"
        '
        'mskTarikhPishbiniErsal
        '
        Me.mskTarikhPishbiniErsal.AllowPromptAsInput = False
        Me.mskTarikhPishbiniErsal.Location = New System.Drawing.Point(91, 50)
        Me.mskTarikhPishbiniErsal.Mask = "####/##/##"
        Me.mskTarikhPishbiniErsal.Name = "mskTarikhPishbiniErsal"
        Me.mskTarikhPishbiniErsal.PromptChar = Global.Microsoft.VisualBasic.ChrW(32)
        Me.mskTarikhPishbiniErsal.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.mskTarikhPishbiniErsal.Size = New System.Drawing.Size(68, 21)
        Me.mskTarikhPishbiniErsal.TabIndex = 31
        Me.mskTarikhPishbiniErsal.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(520, 53)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(73, 13)
        Me.Label6.TabIndex = 26
        Me.Label6.Text = " ماشین توزیع :"
        '
        'cmbMashinTozie
        '
        Me.cmbMashinTozie.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbMashinTozie.Location = New System.Drawing.Point(323, 50)
        Me.cmbMashinTozie.MaxDropDownItems = 20
        Me.cmbMashinTozie.Name = "cmbMashinTozie"
        Me.cmbMashinTozie.Size = New System.Drawing.Size(196, 21)
        Me.cmbMashinTozie.TabIndex = 27
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(162, 53)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(66, 13)
        Me.Label10.TabIndex = 30
        Me.Label10.Text = "تاریخ ارسال :"
        '
        'cmbRanandehTozie
        '
        Me.cmbRanandehTozie.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbRanandehTozie.Location = New System.Drawing.Point(91, 23)
        Me.cmbRanandehTozie.MaxDropDownItems = 20
        Me.cmbRanandehTozie.Name = "cmbRanandehTozie"
        Me.cmbRanandehTozie.Size = New System.Drawing.Size(157, 21)
        Me.cmbRanandehTozie.TabIndex = 29
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(251, 26)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(66, 13)
        Me.Label9.TabIndex = 28
        Me.Label9.Text = " راننده توزیع :"
        '
        'txtShomarehTafkik
        '
        Me.txtShomarehTafkik.Dock = System.Windows.Forms.DockStyle.Right
        Me.txtShomarehTafkik.Enabled = False
        Me.txtShomarehTafkik.Font = New System.Drawing.Font("B Titr", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.txtShomarehTafkik.Location = New System.Drawing.Point(599, 17)
        Me.txtShomarehTafkik.Multiline = True
        Me.txtShomarehTafkik.Name = "txtShomarehTafkik"
        Me.txtShomarehTafkik.Size = New System.Drawing.Size(121, 63)
        Me.txtShomarehTafkik.TabIndex = 36
        Me.txtShomarehTafkik.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'frmFO_ChangeTafkikInfo
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(723, 83)
        Me.Controls.Add(Me.GroupBox1)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.MaximizeBox = False
        Me.Name = "frmFO_ChangeTafkikInfo"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.RightToLeftLayout = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "تغییر اطلاعات تفکیک"
        Me.TopMost = True
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents txtShomarehTafkik As System.Windows.Forms.TextBox
    Friend WithEvents cmbMamorPakhsh As System.Windows.Forms.ComboBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents mskTarikhPishbiniErsal As System.Windows.Forms.MaskedTextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents cmbMashinTozie As System.Windows.Forms.ComboBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents cmbRanandehTozie As System.Windows.Forms.ComboBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
End Class
