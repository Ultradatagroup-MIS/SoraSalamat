<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmFO_TafkikJozeBaresiPopup
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
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.btnTaeed = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.mskTarikhErsal = New System.Windows.Forms.MaskedTextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cmbMashinTozie = New System.Windows.Forms.ComboBox()
        Me.cmbRanandehTozie = New System.Windows.Forms.ComboBox()
        Me.cmbMamorPakhsh = New System.Windows.Forms.ComboBox()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(423, 24)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(68, 13)
        Me.Label11.TabIndex = 0
        Me.Label11.Text = " مامور پخش :"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(423, 78)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(70, 13)
        Me.Label6.TabIndex = 4
        Me.Label6.Text = " ماشین توزیع:"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(423, 50)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(66, 13)
        Me.Label9.TabIndex = 2
        Me.Label9.Text = " راننده توزیع :"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnTaeed
        '
        Me.btnTaeed.Location = New System.Drawing.Point(14, 17)
        Me.btnTaeed.Name = "btnTaeed"
        Me.btnTaeed.Size = New System.Drawing.Size(76, 23)
        Me.btnTaeed.TabIndex = 0
        Me.btnTaeed.Text = "تایید "
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.mskTarikhErsal)
        Me.GroupBox1.Controls.Add(Me.Label9)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.cmbMashinTozie)
        Me.GroupBox1.Controls.Add(Me.Label11)
        Me.GroupBox1.Controls.Add(Me.cmbRanandehTozie)
        Me.GroupBox1.Controls.Add(Me.cmbMamorPakhsh)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(507, 136)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.btnTaeed)
        Me.GroupBox2.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupBox2.Location = New System.Drawing.Point(0, 136)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(507, 46)
        Me.GroupBox2.TabIndex = 1
        Me.GroupBox2.TabStop = False
        '
        'mskTarikhErsal
        '
        Me.mskTarikhErsal.AllowPromptAsInput = False
        Me.mskTarikhErsal.CutCopyMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals
        Me.mskTarikhErsal.Location = New System.Drawing.Point(199, 105)
        Me.mskTarikhErsal.Mask = "####/##/##"
        Me.mskTarikhErsal.Name = "mskTarikhErsal"
        Me.mskTarikhErsal.PromptChar = Global.Microsoft.VisualBasic.ChrW(32)
        Me.mskTarikhErsal.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.mskTarikhErsal.Size = New System.Drawing.Size(72, 21)
        Me.mskTarikhErsal.TabIndex = 12
        Me.mskTarikhErsal.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(275, 108)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(32, 13)
        Me.Label1.TabIndex = 11
        Me.Label1.Text = "تاریخ:"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cmbMashinTozie
        '
        Me.cmbMashinTozie.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbMashinTozie.Location = New System.Drawing.Point(14, 75)
        Me.cmbMashinTozie.MaxDropDownItems = 20
        Me.cmbMashinTozie.Name = "cmbMashinTozie"
        Me.cmbMashinTozie.Size = New System.Drawing.Size(405, 21)
        Me.cmbMashinTozie.TabIndex = 10
        '
        'cmbRanandehTozie
        '
        Me.cmbRanandehTozie.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbRanandehTozie.Location = New System.Drawing.Point(14, 47)
        Me.cmbRanandehTozie.MaxDropDownItems = 20
        Me.cmbRanandehTozie.Name = "cmbRanandehTozie"
        Me.cmbRanandehTozie.Size = New System.Drawing.Size(405, 21)
        Me.cmbRanandehTozie.TabIndex = 9
        '
        'cmbMamorPakhsh
        '
        Me.cmbMamorPakhsh.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbMamorPakhsh.Location = New System.Drawing.Point(14, 21)
        Me.cmbMamorPakhsh.MaxDropDownItems = 20
        Me.cmbMamorPakhsh.Name = "cmbMamorPakhsh"
        Me.cmbMamorPakhsh.Size = New System.Drawing.Size(405, 21)
        Me.cmbMamorPakhsh.TabIndex = 8
        '
        'frmFO_TafkikJozeBaresiPopup
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(507, 193)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.Name = "frmFO_TafkikJozeBaresiPopup"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "تغییر اطلاعات برگه تفکیک"
        Me.TopMost = True
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents btnTaeed As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents mskTarikhErsal As System.Windows.Forms.MaskedTextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cmbMashinTozie As System.Windows.Forms.ComboBox
    Friend WithEvents cmbRanandehTozie As System.Windows.Forms.ComboBox
    Friend WithEvents cmbMamorPakhsh As System.Windows.Forms.ComboBox
End Class
