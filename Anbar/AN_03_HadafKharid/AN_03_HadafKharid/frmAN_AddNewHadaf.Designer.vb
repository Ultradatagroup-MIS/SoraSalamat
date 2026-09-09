<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAN_AddNewHadaf
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
        Me.txtCodeKala = New System.Windows.Forms.TextBox()
        Me.lblNameKala = New System.Windows.Forms.Label()
        Me.cmbGoroh3 = New System.Windows.Forms.ComboBox()
        Me.lblGoroh3 = New System.Windows.Forms.Label()
        Me.cmbBrand = New System.Windows.Forms.ComboBox()
        Me.lblBrand = New System.Windows.Forms.Label()
        Me.cmbGoroh1 = New System.Windows.Forms.ComboBox()
        Me.lblGoroh = New System.Windows.Forms.Label()
        Me.lblKala = New System.Windows.Forms.Label()
        Me.cmbGoroh2 = New System.Windows.Forms.ComboBox()
        Me.lblGoroh2 = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.cmbNoeBasehBandy = New System.Windows.Forms.ComboBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txtTedadHadaf = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtRialHadaf = New System.Windows.Forms.TextBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.cmbNoeMohasebeHadaf = New System.Windows.Forms.ComboBox()
        Me.grbNoeKalaei = New System.Windows.Forms.GroupBox()
        Me.cmbGoroh5 = New System.Windows.Forms.ComboBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.cmbGoroh4 = New System.Windows.Forms.ComboBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.cmbTaminKonandeh = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.mskTaTarikh = New System.Windows.Forms.MaskedTextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.mskAzTarikh = New System.Windows.Forms.MaskedTextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.cmbNoeKalaeiHadaf = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtSharhHadaf = New System.Windows.Forms.TextBox()
        Me.lblSharhHadaf = New System.Windows.Forms.Label()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.btnExit = New System.Windows.Forms.Button()
        Me.btnClear = New System.Windows.Forms.Button()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.txtNoeForm = New System.Windows.Forms.TextBox()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.grbNoeKalaei.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.SuspendLayout()
        '
        'txtCodeKala
        '
        Me.txtCodeKala.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtCodeKala.Location = New System.Drawing.Point(689, 14)
        Me.txtCodeKala.MaxLength = 8
        Me.txtCodeKala.Name = "txtCodeKala"
        Me.txtCodeKala.Size = New System.Drawing.Size(90, 21)
        Me.txtCodeKala.TabIndex = 10
        '
        'lblNameKala
        '
        Me.lblNameKala.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblNameKala.Location = New System.Drawing.Point(293, 14)
        Me.lblNameKala.Name = "lblNameKala"
        Me.lblNameKala.Size = New System.Drawing.Size(392, 21)
        Me.lblNameKala.TabIndex = 11
        '
        'cmbGoroh3
        '
        Me.cmbGoroh3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbGoroh3.FormattingEnabled = True
        Me.cmbGoroh3.Location = New System.Drawing.Point(10, 47)
        Me.cmbGoroh3.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.cmbGoroh3.Name = "cmbGoroh3"
        Me.cmbGoroh3.Size = New System.Drawing.Size(211, 21)
        Me.cmbGoroh3.TabIndex = 19
        '
        'lblGoroh3
        '
        Me.lblGoroh3.AutoSize = True
        Me.lblGoroh3.Location = New System.Drawing.Point(227, 50)
        Me.lblGoroh3.Name = "lblGoroh3"
        Me.lblGoroh3.Size = New System.Drawing.Size(43, 13)
        Me.lblGoroh3.TabIndex = 18
        Me.lblGoroh3.Text = "گروه 3 :"
        Me.lblGoroh3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cmbBrand
        '
        Me.cmbBrand.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbBrand.FormattingEnabled = True
        Me.cmbBrand.Location = New System.Drawing.Point(10, 14)
        Me.cmbBrand.Name = "cmbBrand"
        Me.cmbBrand.Size = New System.Drawing.Size(211, 21)
        Me.cmbBrand.TabIndex = 13
        '
        'lblBrand
        '
        Me.lblBrand.AutoSize = True
        Me.lblBrand.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblBrand.Location = New System.Drawing.Point(227, 17)
        Me.lblBrand.Name = "lblBrand"
        Me.lblBrand.Size = New System.Drawing.Size(31, 13)
        Me.lblBrand.TabIndex = 12
        Me.lblBrand.Text = "برند :" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        Me.lblBrand.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cmbGoroh1
        '
        Me.cmbGoroh1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbGoroh1.FormattingEnabled = True
        Me.cmbGoroh1.Location = New System.Drawing.Point(568, 47)
        Me.cmbGoroh1.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.cmbGoroh1.Name = "cmbGoroh1"
        Me.cmbGoroh1.Size = New System.Drawing.Size(211, 21)
        Me.cmbGoroh1.TabIndex = 15
        '
        'lblGoroh
        '
        Me.lblGoroh.AutoSize = True
        Me.lblGoroh.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblGoroh.Location = New System.Drawing.Point(782, 50)
        Me.lblGoroh.Name = "lblGoroh"
        Me.lblGoroh.Size = New System.Drawing.Size(43, 13)
        Me.lblGoroh.TabIndex = 14
        Me.lblGoroh.Text = "گروه 1 :"
        Me.lblGoroh.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblKala
        '
        Me.lblKala.AutoSize = True
        Me.lblKala.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblKala.Location = New System.Drawing.Point(782, 17)
        Me.lblKala.Name = "lblKala"
        Me.lblKala.Size = New System.Drawing.Size(29, 13)
        Me.lblKala.TabIndex = 10
        Me.lblKala.Text = "کالا :" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        Me.lblKala.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cmbGoroh2
        '
        Me.cmbGoroh2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbGoroh2.FormattingEnabled = True
        Me.cmbGoroh2.Location = New System.Drawing.Point(293, 47)
        Me.cmbGoroh2.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.cmbGoroh2.Name = "cmbGoroh2"
        Me.cmbGoroh2.Size = New System.Drawing.Size(211, 21)
        Me.cmbGoroh2.TabIndex = 17
        '
        'lblGoroh2
        '
        Me.lblGoroh2.AutoSize = True
        Me.lblGoroh2.Location = New System.Drawing.Point(510, 50)
        Me.lblGoroh2.Name = "lblGoroh2"
        Me.lblGoroh2.Size = New System.Drawing.Size(43, 13)
        Me.lblGoroh2.TabIndex = 16
        Me.lblGoroh2.Text = "گروه 2 :"
        Me.lblGoroh2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.txtNoeForm)
        Me.GroupBox1.Controls.Add(Me.GroupBox2)
        Me.GroupBox1.Controls.Add(Me.grbNoeKalaei)
        Me.GroupBox1.Controls.Add(Me.cmbTaminKonandeh)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.mskTaTarikh)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.mskAzTarikh)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.cmbNoeKalaeiHadaf)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.txtSharhHadaf)
        Me.GroupBox1.Controls.Add(Me.lblSharhHadaf)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(843, 321)
        Me.GroupBox1.TabIndex = 21
        Me.GroupBox1.TabStop = False
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.Label9)
        Me.GroupBox2.Controls.Add(Me.cmbNoeBasehBandy)
        Me.GroupBox2.Controls.Add(Me.Label8)
        Me.GroupBox2.Controls.Add(Me.txtTedadHadaf)
        Me.GroupBox2.Controls.Add(Me.Label7)
        Me.GroupBox2.Controls.Add(Me.txtRialHadaf)
        Me.GroupBox2.Controls.Add(Me.Label13)
        Me.GroupBox2.Controls.Add(Me.cmbNoeMohasebeHadaf)
        Me.GroupBox2.Location = New System.Drawing.Point(6, 271)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(831, 44)
        Me.GroupBox2.TabIndex = 25
        Me.GroupBox2.TabStop = False
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label9.Location = New System.Drawing.Point(316, 17)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(27, 13)
        Me.Label9.TabIndex = 28
        Me.Label9.Text = "ريـال"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cmbNoeBasehBandy
        '
        Me.cmbNoeBasehBandy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbNoeBasehBandy.FormattingEnabled = True
        Me.cmbNoeBasehBandy.Location = New System.Drawing.Point(10, 14)
        Me.cmbNoeBasehBandy.Name = "cmbNoeBasehBandy"
        Me.cmbNoeBasehBandy.Size = New System.Drawing.Size(77, 21)
        Me.cmbNoeBasehBandy.TabIndex = 31
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label8.Location = New System.Drawing.Point(227, 17)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(70, 13)
        Me.Label8.TabIndex = 29
        Me.Label8.Text = "تعـداد هـدف :"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtTedadHadaf
        '
        Me.txtTedadHadaf.Location = New System.Drawing.Point(93, 14)
        Me.txtTedadHadaf.MaxLength = 20
        Me.txtTedadHadaf.Name = "txtTedadHadaf"
        Me.txtTedadHadaf.Size = New System.Drawing.Size(128, 21)
        Me.txtTedadHadaf.TabIndex = 30
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label7.Location = New System.Drawing.Point(492, 17)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(64, 13)
        Me.Label7.TabIndex = 26
        Me.Label7.Text = "ريـال هـدف :"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtRialHadaf
        '
        Me.txtRialHadaf.Location = New System.Drawing.Point(349, 14)
        Me.txtRialHadaf.MaxLength = 20
        Me.txtRialHadaf.Name = "txtRialHadaf"
        Me.txtRialHadaf.Size = New System.Drawing.Size(137, 21)
        Me.txtRialHadaf.TabIndex = 27
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label13.Location = New System.Drawing.Point(720, 17)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(105, 13)
        Me.Label13.TabIndex = 24
        Me.Label13.Text = "نـوع محاسبـه هـدف :"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cmbNoeMohasebeHadaf
        '
        Me.cmbNoeMohasebeHadaf.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbNoeMohasebeHadaf.FormattingEnabled = True
        Me.cmbNoeMohasebeHadaf.Location = New System.Drawing.Point(568, 14)
        Me.cmbNoeMohasebeHadaf.Name = "cmbNoeMohasebeHadaf"
        Me.cmbNoeMohasebeHadaf.Size = New System.Drawing.Size(146, 21)
        Me.cmbNoeMohasebeHadaf.TabIndex = 25
        '
        'grbNoeKalaei
        '
        Me.grbNoeKalaei.Controls.Add(Me.cmbGoroh5)
        Me.grbNoeKalaei.Controls.Add(Me.Label4)
        Me.grbNoeKalaei.Controls.Add(Me.cmbGoroh4)
        Me.grbNoeKalaei.Controls.Add(Me.Label6)
        Me.grbNoeKalaei.Controls.Add(Me.lblKala)
        Me.grbNoeKalaei.Controls.Add(Me.cmbGoroh3)
        Me.grbNoeKalaei.Controls.Add(Me.cmbGoroh2)
        Me.grbNoeKalaei.Controls.Add(Me.lblGoroh3)
        Me.grbNoeKalaei.Controls.Add(Me.txtCodeKala)
        Me.grbNoeKalaei.Controls.Add(Me.lblGoroh2)
        Me.grbNoeKalaei.Controls.Add(Me.lblNameKala)
        Me.grbNoeKalaei.Controls.Add(Me.lblBrand)
        Me.grbNoeKalaei.Controls.Add(Me.cmbBrand)
        Me.grbNoeKalaei.Controls.Add(Me.cmbGoroh1)
        Me.grbNoeKalaei.Controls.Add(Me.lblGoroh)
        Me.grbNoeKalaei.Location = New System.Drawing.Point(6, 163)
        Me.grbNoeKalaei.Name = "grbNoeKalaei"
        Me.grbNoeKalaei.Size = New System.Drawing.Size(831, 106)
        Me.grbNoeKalaei.TabIndex = 24
        Me.grbNoeKalaei.TabStop = False
        '
        'cmbGoroh5
        '
        Me.cmbGoroh5.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbGoroh5.FormattingEnabled = True
        Me.cmbGoroh5.Location = New System.Drawing.Point(293, 76)
        Me.cmbGoroh5.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.cmbGoroh5.Name = "cmbGoroh5"
        Me.cmbGoroh5.Size = New System.Drawing.Size(211, 21)
        Me.cmbGoroh5.TabIndex = 23
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(510, 79)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(43, 13)
        Me.Label4.TabIndex = 22
        Me.Label4.Text = "گروه 5 :"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cmbGoroh4
        '
        Me.cmbGoroh4.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbGoroh4.FormattingEnabled = True
        Me.cmbGoroh4.Location = New System.Drawing.Point(568, 76)
        Me.cmbGoroh4.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.cmbGoroh4.Name = "cmbGoroh4"
        Me.cmbGoroh4.Size = New System.Drawing.Size(211, 21)
        Me.cmbGoroh4.TabIndex = 21
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label6.Location = New System.Drawing.Point(782, 79)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(43, 13)
        Me.Label6.TabIndex = 20
        Me.Label6.Text = "گروه 4 :"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cmbTaminKonandeh
        '
        Me.cmbTaminKonandeh.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbTaminKonandeh.FormattingEnabled = True
        Me.cmbTaminKonandeh.Location = New System.Drawing.Point(574, 135)
        Me.cmbTaminKonandeh.Name = "cmbTaminKonandeh"
        Me.cmbTaminKonandeh.Size = New System.Drawing.Size(177, 21)
        Me.cmbTaminKonandeh.TabIndex = 9
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(757, 138)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(65, 13)
        Me.Label3.TabIndex = 8
        Me.Label3.Text = "تامین کننده :"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'mskTaTarikh
        '
        Me.mskTaTarikh.AllowPromptAsInput = False
        Me.mskTaTarikh.Location = New System.Drawing.Point(574, 107)
        Me.mskTaTarikh.Mask = "####/##/##"
        Me.mskTaTarikh.Name = "mskTaTarikh"
        Me.mskTaTarikh.PromptChar = Global.Microsoft.VisualBasic.ChrW(32)
        Me.mskTaTarikh.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.mskTaTarikh.Size = New System.Drawing.Size(62, 21)
        Me.mskTaTarikh.TabIndex = 7
        Me.mskTaTarikh.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(641, 110)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(44, 13)
        Me.Label2.TabIndex = 6
        Me.Label2.Text = "تا تاریخ :"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'mskAzTarikh
        '
        Me.mskAzTarikh.AllowPromptAsInput = False
        Me.mskAzTarikh.Location = New System.Drawing.Point(689, 107)
        Me.mskAzTarikh.Mask = "####/##/##"
        Me.mskAzTarikh.Name = "mskAzTarikh"
        Me.mskAzTarikh.PromptChar = Global.Microsoft.VisualBasic.ChrW(32)
        Me.mskAzTarikh.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.mskAzTarikh.Size = New System.Drawing.Size(62, 21)
        Me.mskAzTarikh.TabIndex = 5
        Me.mskAzTarikh.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label5.Location = New System.Drawing.Point(757, 110)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(45, 13)
        Me.Label5.TabIndex = 4
        Me.Label5.Text = "از تاریخ :"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cmbNoeKalaeiHadaf
        '
        Me.cmbNoeKalaeiHadaf.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbNoeKalaeiHadaf.FormattingEnabled = True
        Me.cmbNoeKalaeiHadaf.Location = New System.Drawing.Point(574, 79)
        Me.cmbNoeKalaeiHadaf.Name = "cmbNoeKalaeiHadaf"
        Me.cmbNoeKalaeiHadaf.Size = New System.Drawing.Size(177, 21)
        Me.cmbNoeKalaeiHadaf.TabIndex = 2
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(757, 82)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(74, 13)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "هـدف بر روی :"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtSharhHadaf
        '
        Me.txtSharhHadaf.Location = New System.Drawing.Point(6, 79)
        Me.txtSharhHadaf.Multiline = True
        Me.txtSharhHadaf.Name = "txtSharhHadaf"
        Me.txtSharhHadaf.Size = New System.Drawing.Size(486, 77)
        Me.txtSharhHadaf.TabIndex = 0
        '
        'lblSharhHadaf
        '
        Me.lblSharhHadaf.AutoSize = True
        Me.lblSharhHadaf.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblSharhHadaf.Location = New System.Drawing.Point(498, 82)
        Me.lblSharhHadaf.Name = "lblSharhHadaf"
        Me.lblSharhHadaf.Size = New System.Drawing.Size(70, 13)
        Me.lblSharhHadaf.TabIndex = 1
        Me.lblSharhHadaf.Text = "شـرح هـدف :"
        Me.lblSharhHadaf.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.btnExit)
        Me.GroupBox3.Controls.Add(Me.btnClear)
        Me.GroupBox3.Controls.Add(Me.btnSave)
        Me.GroupBox3.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.GroupBox3.Location = New System.Drawing.Point(0, 318)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(843, 51)
        Me.GroupBox3.TabIndex = 22
        Me.GroupBox3.TabStop = False
        '
        'btnExit
        '
        Me.btnExit.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnExit.Location = New System.Drawing.Point(6, 16)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(92, 29)
        Me.btnExit.TabIndex = 34
        Me.btnExit.Text = "خــــــــــروج"
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'btnClear
        '
        Me.btnClear.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClear.Location = New System.Drawing.Point(650, 16)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(92, 29)
        Me.btnClear.TabIndex = 33
        Me.btnClear.Text = "خالی کردن فرم"
        Me.btnClear.UseVisualStyleBackColor = True
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Location = New System.Drawing.Point(745, 16)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(92, 29)
        Me.btnSave.TabIndex = 32
        Me.btnSave.Text = "ذخیــــره"
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'txtNoeForm
        '
        Me.txtNoeForm.Dock = System.Windows.Forms.DockStyle.Top
        Me.txtNoeForm.Enabled = False
        Me.txtNoeForm.Font = New System.Drawing.Font("B Titr", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.txtNoeForm.Location = New System.Drawing.Point(3, 17)
        Me.txtNoeForm.Multiline = True
        Me.txtNoeForm.Name = "txtNoeForm"
        Me.txtNoeForm.Size = New System.Drawing.Size(837, 56)
        Me.txtNoeForm.TabIndex = 26
        Me.txtNoeForm.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'frmAN_AddNewHadaf
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(843, 369)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.GroupBox3)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.Name = "frmAN_AddNewHadaf"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.RightToLeftLayout = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = " تعریف و ویرایش هدف خرید"
        Me.TopMost = True
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.grbNoeKalaei.ResumeLayout(False)
        Me.grbNoeKalaei.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents txtCodeKala As System.Windows.Forms.TextBox
    Friend WithEvents lblNameKala As System.Windows.Forms.Label
    Friend WithEvents cmbGoroh3 As System.Windows.Forms.ComboBox
    Friend WithEvents lblGoroh3 As System.Windows.Forms.Label
    Friend WithEvents cmbBrand As System.Windows.Forms.ComboBox
    Friend WithEvents lblBrand As System.Windows.Forms.Label
    Friend WithEvents cmbGoroh1 As System.Windows.Forms.ComboBox
    Friend WithEvents lblGoroh As System.Windows.Forms.Label
    Friend WithEvents lblKala As System.Windows.Forms.Label
    Friend WithEvents cmbGoroh2 As System.Windows.Forms.ComboBox
    Friend WithEvents lblGoroh2 As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents lblSharhHadaf As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtSharhHadaf As System.Windows.Forms.TextBox
    Friend WithEvents cmbTaminKonandeh As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents mskTaTarikh As System.Windows.Forms.MaskedTextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents mskAzTarikh As System.Windows.Forms.MaskedTextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents cmbNoeKalaeiHadaf As System.Windows.Forms.ComboBox
    Friend WithEvents grbNoeKalaei As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents cmbNoeBasehBandy As System.Windows.Forms.ComboBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtTedadHadaf As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtRialHadaf As System.Windows.Forms.TextBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents cmbNoeMohasebeHadaf As System.Windows.Forms.ComboBox
    Friend WithEvents cmbGoroh5 As System.Windows.Forms.ComboBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents cmbGoroh4 As System.Windows.Forms.ComboBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents btnClear As System.Windows.Forms.Button
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents txtNoeForm As System.Windows.Forms.TextBox
End Class
