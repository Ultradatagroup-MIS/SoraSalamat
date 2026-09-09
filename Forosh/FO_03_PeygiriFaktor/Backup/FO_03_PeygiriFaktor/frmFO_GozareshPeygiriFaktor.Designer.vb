<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmFO_GozareshPeygiriFaktor
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
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.Label10 = New System.Windows.Forms.Label
        Me.Label11 = New System.Windows.Forms.Label
        Me.Label12 = New System.Windows.Forms.Label
        Me.mskAzShomarehPeygiri = New System.Windows.Forms.MaskedTextBox
        Me.mskTaShomarehPeygiri = New System.Windows.Forms.MaskedTextBox
        Me.lblNameMoshtaryS = New System.Windows.Forms.Label
        Me.txtCodeMoshtaryS = New System.Windows.Forms.TextBox
        Me.Label9 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.mskFaktorShomarehTa = New System.Windows.Forms.MaskedTextBox
        Me.mskFaktorShomarehAz = New System.Windows.Forms.MaskedTextBox
        Me.mskTaTarikh = New System.Windows.Forms.MaskedTextBox
        Me.mskAzTarikh = New System.Windows.Forms.MaskedTextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.cmbVazeiatS = New System.Windows.Forms.ComboBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.cmbMamorPakhshS = New System.Windows.Forms.ComboBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.btnExit = New System.Windows.Forms.Button
        Me.btnView = New System.Windows.Forms.Button
        Me.cmbDoreh = New System.Windows.Forms.ComboBox
        Me.Label13 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.cmbDoreh)
        Me.GroupBox1.Controls.Add(Me.Label13)
        Me.GroupBox1.Controls.Add(Me.Label10)
        Me.GroupBox1.Controls.Add(Me.Label11)
        Me.GroupBox1.Controls.Add(Me.Label12)
        Me.GroupBox1.Controls.Add(Me.mskAzShomarehPeygiri)
        Me.GroupBox1.Controls.Add(Me.mskTaShomarehPeygiri)
        Me.GroupBox1.Controls.Add(Me.lblNameMoshtaryS)
        Me.GroupBox1.Controls.Add(Me.txtCodeMoshtaryS)
        Me.GroupBox1.Controls.Add(Me.Label9)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.mskFaktorShomarehTa)
        Me.GroupBox1.Controls.Add(Me.mskFaktorShomarehAz)
        Me.GroupBox1.Controls.Add(Me.mskTaTarikh)
        Me.GroupBox1.Controls.Add(Me.mskAzTarikh)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.cmbVazeiatS)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.cmbMamorPakhshS)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(497, 162)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(156, 75)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(53, 13)
        Me.Label10.TabIndex = 141
        Me.Label10.Text = "تا شماره :"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(320, 75)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(54, 13)
        Me.Label11.TabIndex = 140
        Me.Label11.Text = "از شماره :"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(410, 75)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(79, 13)
        Me.Label12.TabIndex = 139
        Me.Label12.Text = "شماره پیگیری :"
        '
        'mskAzShomarehPeygiri
        '
        Me.mskAzShomarehPeygiri.AllowPromptAsInput = False
        Me.mskAzShomarehPeygiri.Location = New System.Drawing.Point(238, 72)
        Me.mskAzShomarehPeygiri.Mask = "000000000"
        Me.mskAzShomarehPeygiri.Name = "mskAzShomarehPeygiri"
        Me.mskAzShomarehPeygiri.PromptChar = Global.Microsoft.VisualBasic.ChrW(32)
        Me.mskAzShomarehPeygiri.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.mskAzShomarehPeygiri.Size = New System.Drawing.Size(76, 21)
        Me.mskAzShomarehPeygiri.TabIndex = 4
        Me.mskAzShomarehPeygiri.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals
        '
        'mskTaShomarehPeygiri
        '
        Me.mskTaShomarehPeygiri.AllowPromptAsInput = False
        Me.mskTaShomarehPeygiri.Location = New System.Drawing.Point(74, 72)
        Me.mskTaShomarehPeygiri.Mask = "000000000"
        Me.mskTaShomarehPeygiri.Name = "mskTaShomarehPeygiri"
        Me.mskTaShomarehPeygiri.PromptChar = Global.Microsoft.VisualBasic.ChrW(32)
        Me.mskTaShomarehPeygiri.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.mskTaShomarehPeygiri.Size = New System.Drawing.Size(76, 21)
        Me.mskTaShomarehPeygiri.TabIndex = 5
        Me.mskTaShomarehPeygiri.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals
        '
        'lblNameMoshtaryS
        '
        Me.lblNameMoshtaryS.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblNameMoshtaryS.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNameMoshtaryS.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblNameMoshtaryS.Location = New System.Drawing.Point(10, 127)
        Me.lblNameMoshtaryS.Name = "lblNameMoshtaryS"
        Me.lblNameMoshtaryS.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.lblNameMoshtaryS.Size = New System.Drawing.Size(304, 21)
        Me.lblNameMoshtaryS.TabIndex = 136
        Me.lblNameMoshtaryS.Text = "  "
        Me.lblNameMoshtaryS.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtCodeMoshtaryS
        '
        Me.txtCodeMoshtaryS.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtCodeMoshtaryS.Location = New System.Drawing.Point(320, 128)
        Me.txtCodeMoshtaryS.MaxLength = 15
        Me.txtCodeMoshtaryS.Name = "txtCodeMoshtaryS"
        Me.txtCodeMoshtaryS.Size = New System.Drawing.Size(84, 21)
        Me.txtCodeMoshtaryS.TabIndex = 8
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(410, 131)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(51, 13)
        Me.Label9.TabIndex = 134
        Me.Label9.Text = "مشتری :"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(88, 102)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(53, 13)
        Me.Label1.TabIndex = 133
        Me.Label1.Text = "تا شماره :"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(224, 102)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(54, 13)
        Me.Label2.TabIndex = 132
        Me.Label2.Text = "از شماره :"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(410, 48)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(70, 13)
        Me.Label6.TabIndex = 130
        Me.Label6.Text = "تاریخ پیگیری :"
        '
        'mskFaktorShomarehTa
        '
        Me.mskFaktorShomarehTa.AllowPromptAsInput = False
        Me.mskFaktorShomarehTa.Location = New System.Drawing.Point(10, 99)
        Me.mskFaktorShomarehTa.Mask = "000000000"
        Me.mskFaktorShomarehTa.Name = "mskFaktorShomarehTa"
        Me.mskFaktorShomarehTa.PromptChar = Global.Microsoft.VisualBasic.ChrW(32)
        Me.mskFaktorShomarehTa.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.mskFaktorShomarehTa.Size = New System.Drawing.Size(76, 21)
        Me.mskFaktorShomarehTa.TabIndex = 7
        Me.mskFaktorShomarehTa.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals
        '
        'mskFaktorShomarehAz
        '
        Me.mskFaktorShomarehAz.AllowPromptAsInput = False
        Me.mskFaktorShomarehAz.Location = New System.Drawing.Point(146, 99)
        Me.mskFaktorShomarehAz.Mask = "000000000"
        Me.mskFaktorShomarehAz.Name = "mskFaktorShomarehAz"
        Me.mskFaktorShomarehAz.PromptChar = Global.Microsoft.VisualBasic.ChrW(32)
        Me.mskFaktorShomarehAz.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.mskFaktorShomarehAz.Size = New System.Drawing.Size(76, 21)
        Me.mskFaktorShomarehAz.TabIndex = 6
        Me.mskFaktorShomarehAz.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals
        '
        'mskTaTarikh
        '
        Me.mskTaTarikh.AllowPromptAsInput = False
        Me.mskTaTarikh.Location = New System.Drawing.Point(74, 45)
        Me.mskTaTarikh.Mask = "####/##/##"
        Me.mskTaTarikh.Name = "mskTaTarikh"
        Me.mskTaTarikh.PromptChar = Global.Microsoft.VisualBasic.ChrW(32)
        Me.mskTaTarikh.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.mskTaTarikh.Size = New System.Drawing.Size(76, 21)
        Me.mskTaTarikh.TabIndex = 3
        Me.mskTaTarikh.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals
        '
        'mskAzTarikh
        '
        Me.mskAzTarikh.AllowPromptAsInput = False
        Me.mskAzTarikh.Location = New System.Drawing.Point(238, 45)
        Me.mskAzTarikh.Mask = "####/##/##"
        Me.mskAzTarikh.Name = "mskAzTarikh"
        Me.mskAzTarikh.PromptChar = Global.Microsoft.VisualBasic.ChrW(32)
        Me.mskAzTarikh.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.mskAzTarikh.Size = New System.Drawing.Size(76, 21)
        Me.mskAzTarikh.TabIndex = 2
        Me.mskAzTarikh.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(156, 48)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(44, 13)
        Me.Label5.TabIndex = 124
        Me.Label5.Text = "تا تاریخ :"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(320, 48)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(45, 13)
        Me.Label4.TabIndex = 122
        Me.Label4.Text = "از تاریخ :"
        '
        'cmbVazeiatS
        '
        Me.cmbVazeiatS.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbVazeiatS.Location = New System.Drawing.Point(10, 16)
        Me.cmbVazeiatS.MaxDropDownItems = 20
        Me.cmbVazeiatS.Name = "cmbVazeiatS"
        Me.cmbVazeiatS.Size = New System.Drawing.Size(112, 21)
        Me.cmbVazeiatS.TabIndex = 1
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(128, 19)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(48, 13)
        Me.Label3.TabIndex = 120
        Me.Label3.Text = "وضعیت :"
        '
        'cmbMamorPakhshS
        '
        Me.cmbMamorPakhshS.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbMamorPakhshS.Location = New System.Drawing.Point(193, 16)
        Me.cmbMamorPakhshS.MaxDropDownItems = 20
        Me.cmbMamorPakhshS.Name = "cmbMamorPakhshS"
        Me.cmbMamorPakhshS.Size = New System.Drawing.Size(211, 21)
        Me.cmbMamorPakhshS.TabIndex = 0
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(410, 19)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(81, 13)
        Me.Label7.TabIndex = 119
        Me.Label7.Text = "نام مامور پخش :"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.btnExit)
        Me.GroupBox2.Controls.Add(Me.btnView)
        Me.GroupBox2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.GroupBox2.Location = New System.Drawing.Point(0, 159)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(497, 51)
        Me.GroupBox2.TabIndex = 1
        Me.GroupBox2.TabStop = False
        '
        'btnExit
        '
        Me.btnExit.Location = New System.Drawing.Point(160, 16)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(88, 27)
        Me.btnExit.TabIndex = 1
        Me.btnExit.Text = "خـــــروج"
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'btnView
        '
        Me.btnView.Location = New System.Drawing.Point(248, 16)
        Me.btnView.Name = "btnView"
        Me.btnView.Size = New System.Drawing.Size(88, 27)
        Me.btnView.TabIndex = 0
        Me.btnView.Text = "مشاهــده"
        Me.btnView.UseVisualStyleBackColor = True
        '
        'cmbDoreh
        '
        Me.cmbDoreh.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbDoreh.Location = New System.Drawing.Point(284, 99)
        Me.cmbDoreh.MaxDropDownItems = 20
        Me.cmbDoreh.Name = "cmbDoreh"
        Me.cmbDoreh.Size = New System.Drawing.Size(80, 21)
        Me.cmbDoreh.TabIndex = 142
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(410, 102)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(65, 13)
        Me.Label13.TabIndex = 143
        Me.Label13.Text = "دوره فاکتـور :"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(370, 102)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(33, 13)
        Me.Label8.TabIndex = 144
        Me.Label8.Text = "دوره :"
        '
        'frmFO_GozareshPeygiriFaktor
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(497, 210)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.GroupBox2)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.Name = "frmFO_GozareshPeygiriFaktor"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.RightToLeftLayout = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "گزارش پیگیری فاکتور"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents btnView As System.Windows.Forms.Button
    Friend WithEvents cmbVazeiatS As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cmbMamorPakhshS As System.Windows.Forms.ComboBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents mskTaTarikh As System.Windows.Forms.MaskedTextBox
    Friend WithEvents mskAzTarikh As System.Windows.Forms.MaskedTextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents mskFaktorShomarehAz As System.Windows.Forms.MaskedTextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents mskFaktorShomarehTa As System.Windows.Forms.MaskedTextBox
    Friend WithEvents txtCodeMoshtaryS As System.Windows.Forms.TextBox
    Friend WithEvents lblNameMoshtaryS As System.Windows.Forms.Label
    Friend WithEvents mskTaShomarehPeygiri As System.Windows.Forms.MaskedTextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents mskAzShomarehPeygiri As System.Windows.Forms.MaskedTextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents cmbDoreh As System.Windows.Forms.ComboBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
End Class
