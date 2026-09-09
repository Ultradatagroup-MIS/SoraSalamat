<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmDP_DaryaftChek
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmDP_DaryaftChek))
        Dim GridEXChek_Layout_0 As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout
        Dim GridEXChek_Layout_1 As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout
        Dim GridEXChek_Layout_2 As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout
        Dim GridEXChek_Layout_3 As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout
        Dim GridEXChek_Layout_4 As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout
        Dim GridEXChek_Layout_5 As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout
        Me.GridEXChek = New Janus.Windows.GridEX.GridEX
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.btnSave = New System.Windows.Forms.Button
        Me.btnDelete = New System.Windows.Forms.Button
        Me.btnRefresh = New System.Windows.Forms.Button
        Me.btnCancel = New System.Windows.Forms.Button
        Me.btnExit = New System.Windows.Forms.Button
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.Label15 = New System.Windows.Forms.Label
        Me.txtCodeShobehSanad = New System.Windows.Forms.TextBox
        Me.Label18 = New System.Windows.Forms.Label
        Me.txtShobehSanad = New System.Windows.Forms.TextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.txtShomarehHesabSanad = New System.Windows.Forms.TextBox
        Me.Label8 = New System.Windows.Forms.Label
        Me.lblMandehFaktor = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label17 = New System.Windows.Forms.Label
        Me.Label16 = New System.Windows.Forms.Label
        Me.Label14 = New System.Windows.Forms.Label
        Me.txtMablaghPFaktor = New clsMaskNumber.MaskNumber
        Me.txtMablaghKol = New clsMaskNumber.MaskNumber
        Me.Label12 = New System.Windows.Forms.Label
        Me.Label13 = New System.Windows.Forms.Label
        Me.cmbBankSanad = New System.Windows.Forms.ComboBox
        Me.txtShomarehSanad = New System.Windows.Forms.TextBox
        Me.Label10 = New System.Windows.Forms.Label
        Me.lblMablaghFaktor = New System.Windows.Forms.Label
        Me.Label9 = New System.Windows.Forms.Label
        Me.mskTarikhSanad = New System.Windows.Forms.MaskedTextBox
        Me.Label11 = New System.Windows.Forms.Label
        Me.mskTarikhDP = New System.Windows.Forms.MaskedTextBox
        Me.lblTarikhDP = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.cmbShomarehHesab = New System.Windows.Forms.ComboBox
        Me.lblAvarandehVajh = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.lblNameMoshtary = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.lblShomarehFaktor = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        CType(Me.GridEXChek, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'GridEXChek
        '
        Me.GridEXChek.AllowChildTableGroups = True
        Me.GridEXChek.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.GridEXChek.AllowDrop = True
        Me.GridEXChek.AllowRemoveColumns = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.GridEXChek.BorderStyle = Janus.Windows.GridEX.BorderStyle.RaisedLight3D
        Me.GridEXChek.BuiltInTextsData = resources.GetString("GridEXChek.BuiltInTextsData")
        Me.GridEXChek.ColumnSetNavigation = Janus.Windows.GridEX.ColumnSetNavigation.ColumnSet
        Me.GridEXChek.Dock = System.Windows.Forms.DockStyle.Top
        Me.GridEXChek.DynamicFiltering = True
        Me.GridEXChek.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        Me.GridEXChek.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.GridEXChek.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges
        Me.GridEXChek.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridEXChek.GroupByBoxVisible = False
        Me.GridEXChek.GroupRowVisualStyle = Janus.Windows.GridEX.GroupRowVisualStyle.Outlook2003
        Me.GridEXChek.GroupTotals = Janus.Windows.GridEX.GroupTotals.Always
        Me.GridEXChek.KeepRowSettings = True
        GridEXChek_Layout_0.Key = "Layout1"
        GridEXChek_Layout_1.Key = "Layout2"
        GridEXChek_Layout_2.Key = "Layout3"
        GridEXChek_Layout_3.Key = "Layout4"
        GridEXChek_Layout_4.Key = "Layout5"
        GridEXChek_Layout_5.Key = "Layout6"
        Me.GridEXChek.Layouts.AddRange(New Janus.Windows.GridEX.GridEXLayout() {GridEXChek_Layout_0, GridEXChek_Layout_1, GridEXChek_Layout_2, GridEXChek_Layout_3, GridEXChek_Layout_4, GridEXChek_Layout_5})
        Me.GridEXChek.Location = New System.Drawing.Point(0, 0)
        Me.GridEXChek.Name = "GridEXChek"
        Me.GridEXChek.Office2007ColorScheme = Janus.Windows.GridEX.Office2007ColorScheme.Blue
        Me.GridEXChek.RecordNavigator = True
        Me.GridEXChek.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.GridEXChek.Size = New System.Drawing.Size(1083, 11)
        Me.GridEXChek.TabIndex = 100
        Me.GridEXChek.TotalRow = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.GridEXChek.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed
        Me.GridEXChek.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.btnSave)
        Me.GroupBox1.Controls.Add(Me.btnDelete)
        Me.GroupBox1.Controls.Add(Me.btnRefresh)
        Me.GroupBox1.Controls.Add(Me.btnCancel)
        Me.GroupBox1.Controls.Add(Me.btnExit)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.GroupBox1.Location = New System.Drawing.Point(0, 143)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(1083, 46)
        Me.GroupBox1.TabIndex = 101
        Me.GroupBox1.TabStop = False
        '
        'btnSave
        '
        Me.btnSave.Location = New System.Drawing.Point(663, 14)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(81, 27)
        Me.btnSave.TabIndex = 0
        Me.btnSave.Text = "ذخیــــــره"
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'btnDelete
        '
        Me.btnDelete.Location = New System.Drawing.Point(582, 14)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(81, 27)
        Me.btnDelete.TabIndex = 1
        Me.btnDelete.Text = "حـــــــذف"
        Me.btnDelete.UseVisualStyleBackColor = True
        '
        'btnRefresh
        '
        Me.btnRefresh.Location = New System.Drawing.Point(501, 14)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(81, 27)
        Me.btnRefresh.TabIndex = 2
        Me.btnRefresh.Text = "بازخـــوانی"
        Me.btnRefresh.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        Me.btnCancel.Location = New System.Drawing.Point(420, 14)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(81, 27)
        Me.btnCancel.TabIndex = 3
        Me.btnCancel.Text = "صرفــنظــر"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'btnExit
        '
        Me.btnExit.Location = New System.Drawing.Point(339, 14)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(81, 27)
        Me.btnExit.TabIndex = 4
        Me.btnExit.Text = "خــــــروج"
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.Label15)
        Me.GroupBox2.Controls.Add(Me.txtCodeShobehSanad)
        Me.GroupBox2.Controls.Add(Me.Label18)
        Me.GroupBox2.Controls.Add(Me.txtShobehSanad)
        Me.GroupBox2.Controls.Add(Me.Label5)
        Me.GroupBox2.Controls.Add(Me.txtShomarehHesabSanad)
        Me.GroupBox2.Controls.Add(Me.Label8)
        Me.GroupBox2.Controls.Add(Me.lblMandehFaktor)
        Me.GroupBox2.Controls.Add(Me.Label3)
        Me.GroupBox2.Controls.Add(Me.Label2)
        Me.GroupBox2.Controls.Add(Me.Label17)
        Me.GroupBox2.Controls.Add(Me.Label16)
        Me.GroupBox2.Controls.Add(Me.Label14)
        Me.GroupBox2.Controls.Add(Me.txtMablaghPFaktor)
        Me.GroupBox2.Controls.Add(Me.txtMablaghKol)
        Me.GroupBox2.Controls.Add(Me.Label12)
        Me.GroupBox2.Controls.Add(Me.Label13)
        Me.GroupBox2.Controls.Add(Me.cmbBankSanad)
        Me.GroupBox2.Controls.Add(Me.txtShomarehSanad)
        Me.GroupBox2.Controls.Add(Me.Label10)
        Me.GroupBox2.Controls.Add(Me.lblMablaghFaktor)
        Me.GroupBox2.Controls.Add(Me.Label9)
        Me.GroupBox2.Controls.Add(Me.mskTarikhSanad)
        Me.GroupBox2.Controls.Add(Me.Label11)
        Me.GroupBox2.Controls.Add(Me.mskTarikhDP)
        Me.GroupBox2.Controls.Add(Me.lblTarikhDP)
        Me.GroupBox2.Controls.Add(Me.Label7)
        Me.GroupBox2.Controls.Add(Me.cmbShomarehHesab)
        Me.GroupBox2.Controls.Add(Me.lblAvarandehVajh)
        Me.GroupBox2.Controls.Add(Me.Label6)
        Me.GroupBox2.Controls.Add(Me.lblNameMoshtary)
        Me.GroupBox2.Controls.Add(Me.Label4)
        Me.GroupBox2.Controls.Add(Me.lblShomarehFaktor)
        Me.GroupBox2.Controls.Add(Me.Label1)
        Me.GroupBox2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.GroupBox2.Location = New System.Drawing.Point(0, 12)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(1083, 131)
        Me.GroupBox2.TabIndex = 102
        Me.GroupBox2.TabStop = False
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(456, 72)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(70, 13)
        Me.Label15.TabIndex = 24
        Me.Label15.Text = "کــد شعبـــه :"
        Me.Label15.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtCodeShobehSanad
        '
        Me.txtCodeShobehSanad.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCodeShobehSanad.Location = New System.Drawing.Point(324, 67)
        Me.txtCodeShobehSanad.MaxLength = 10
        Me.txtCodeShobehSanad.Name = "txtCodeShobehSanad"
        Me.txtCodeShobehSanad.Size = New System.Drawing.Size(126, 23)
        Me.txtCodeShobehSanad.TabIndex = 25
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Location = New System.Drawing.Point(750, 72)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(50, 13)
        Me.Label18.TabIndex = 22
        Me.Label18.Text = "شعبـــه :"
        Me.Label18.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtShobehSanad
        '
        Me.txtShobehSanad.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtShobehSanad.Location = New System.Drawing.Point(569, 67)
        Me.txtShobehSanad.MaxLength = 30
        Me.txtShobehSanad.Name = "txtShobehSanad"
        Me.txtShobehSanad.Size = New System.Drawing.Size(179, 23)
        Me.txtShobehSanad.TabIndex = 23
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(1001, 72)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(75, 13)
        Me.Label5.TabIndex = 20
        Me.Label5.Text = "شماره حساب:"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtShomarehHesabSanad
        '
        Me.txtShomarehHesabSanad.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtShomarehHesabSanad.Location = New System.Drawing.Point(820, 67)
        Me.txtShomarehHesabSanad.MaxLength = 25
        Me.txtShomarehHesabSanad.Name = "txtShomarehHesabSanad"
        Me.txtShomarehHesabSanad.Size = New System.Drawing.Size(179, 23)
        Me.txtShomarehHesabSanad.TabIndex = 21
        Me.txtShomarehHesabSanad.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(142, 99)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(346, 13)
        Me.Label8.TabIndex = 28
        Me.Label8.Text = "مبلغی که به فاکتور جاری باید اختصاص پیدا کند (مبلغ پرداختی برای فاکتور):"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblMandehFaktor
        '
        Me.lblMandehFaktor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblMandehFaktor.Location = New System.Drawing.Point(31, 14)
        Me.lblMandehFaktor.Name = "lblMandehFaktor"
        Me.lblMandehFaktor.Size = New System.Drawing.Size(109, 19)
        Me.lblMandehFaktor.TabIndex = 34
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(142, 17)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(69, 13)
        Me.Label3.TabIndex = 9
        Me.Label3.Text = "مانده فاکتـور :"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(5, 17)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(22, 13)
        Me.Label2.TabIndex = 32
        Me.Label2.Text = "ريال"
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(5, 99)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(22, 13)
        Me.Label17.TabIndex = 30
        Me.Label17.Text = "ريال"
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(6, 72)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(22, 13)
        Me.Label16.TabIndex = 29
        Me.Label16.Text = "ريال"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(142, 72)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(109, 13)
        Me.Label14.TabIndex = 26
        Me.Label14.Text = "مبلغ کل چک دریافتی :"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtMablaghPFaktor
        '
        Me.txtMablaghPFaktor.FloatLength = CType(0, Byte)
        Me.txtMablaghPFaktor.FloatSign = "/"
        Me.txtMablaghPFaktor.Location = New System.Drawing.Point(31, 96)
        Me.txtMablaghPFaktor.MaxLength = CType(20, Byte)
        Me.txtMablaghPFaktor.Name = "txtMablaghPFaktor"
        Me.txtMablaghPFaktor.SignText = ""
        Me.txtMablaghPFaktor.Size = New System.Drawing.Size(109, 21)
        Me.txtMablaghPFaktor.TabIndex = 29
        '
        'txtMablaghKol
        '
        Me.txtMablaghKol.FloatLength = CType(0, Byte)
        Me.txtMablaghKol.FloatSign = "/"
        Me.txtMablaghKol.Location = New System.Drawing.Point(31, 69)
        Me.txtMablaghKol.MaxLength = CType(20, Byte)
        Me.txtMablaghKol.Name = "txtMablaghKol"
        Me.txtMablaghKol.SignText = ""
        Me.txtMablaghKol.Size = New System.Drawing.Size(109, 21)
        Me.txtMablaghKol.TabIndex = 27
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(142, 43)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(76, 13)
        Me.Label12.TabIndex = 18
        Me.Label12.Text = "شماره سریال :"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(362, 43)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(40, 13)
        Me.Label13.TabIndex = 16
        Me.Label13.Text = "بانــک :"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cmbBankSanad
        '
        Me.cmbBankSanad.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbBankSanad.Location = New System.Drawing.Point(236, 39)
        Me.cmbBankSanad.Name = "cmbBankSanad"
        Me.cmbBankSanad.Size = New System.Drawing.Size(122, 21)
        Me.cmbBankSanad.TabIndex = 17
        '
        'txtShomarehSanad
        '
        Me.txtShomarehSanad.Location = New System.Drawing.Point(31, 40)
        Me.txtShomarehSanad.MaxLength = 12
        Me.txtShomarehSanad.Name = "txtShomarehSanad"
        Me.txtShomarehSanad.Size = New System.Drawing.Size(109, 21)
        Me.txtShomarehSanad.TabIndex = 19
        Me.txtShomarehSanad.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(214, 17)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(22, 13)
        Me.Label10.TabIndex = 8
        Me.Label10.Text = "ريال"
        '
        'lblMablaghFaktor
        '
        Me.lblMablaghFaktor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblMablaghFaktor.Location = New System.Drawing.Point(236, 14)
        Me.lblMablaghFaktor.Name = "lblMablaghFaktor"
        Me.lblMablaghFaktor.Size = New System.Drawing.Size(99, 19)
        Me.lblMablaghFaktor.TabIndex = 7
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(337, 17)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(72, 13)
        Me.Label9.TabIndex = 6
        Me.Label9.Text = "مبلـغ فاکتــور :"
        '
        'mskTarikhSanad
        '
        Me.mskTarikhSanad.Location = New System.Drawing.Point(420, 39)
        Me.mskTarikhSanad.Mask = "####/##/##"
        Me.mskTarikhSanad.Name = "mskTarikhSanad"
        Me.mskTarikhSanad.PromptChar = Global.Microsoft.VisualBasic.ChrW(32)
        Me.mskTarikhSanad.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.mskTarikhSanad.Size = New System.Drawing.Size(81, 21)
        Me.mskTarikhSanad.TabIndex = 15
        Me.mskTarikhSanad.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.mskTarikhSanad.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(504, 43)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(82, 13)
        Me.Label11.TabIndex = 14
        Me.Label11.Text = "تاریخ سر رسید :"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'mskTarikhDP
        '
        Me.mskTarikhDP.Location = New System.Drawing.Point(591, 39)
        Me.mskTarikhDP.Mask = "####/##/##"
        Me.mskTarikhDP.Name = "mskTarikhDP"
        Me.mskTarikhDP.PromptChar = Global.Microsoft.VisualBasic.ChrW(32)
        Me.mskTarikhDP.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.mskTarikhDP.Size = New System.Drawing.Size(81, 21)
        Me.mskTarikhDP.TabIndex = 13
        Me.mskTarikhDP.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.mskTarikhDP.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals
        '
        'lblTarikhDP
        '
        Me.lblTarikhDP.AutoSize = True
        Me.lblTarikhDP.Location = New System.Drawing.Point(675, 43)
        Me.lblTarikhDP.Name = "lblTarikhDP"
        Me.lblTarikhDP.Size = New System.Drawing.Size(68, 13)
        Me.lblTarikhDP.TabIndex = 12
        Me.lblTarikhDP.Text = "تاريخ دریافت :"
        Me.lblTarikhDP.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(1001, 43)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(57, 13)
        Me.Label7.TabIndex = 10
        Me.Label7.Text = "صنـــدوق :"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cmbShomarehHesab
        '
        Me.cmbShomarehHesab.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbShomarehHesab.Location = New System.Drawing.Point(750, 40)
        Me.cmbShomarehHesab.Name = "cmbShomarehHesab"
        Me.cmbShomarehHesab.Size = New System.Drawing.Size(249, 21)
        Me.cmbShomarehHesab.TabIndex = 11
        '
        'lblAvarandehVajh
        '
        Me.lblAvarandehVajh.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblAvarandehVajh.Location = New System.Drawing.Point(411, 14)
        Me.lblAvarandehVajh.Name = "lblAvarandehVajh"
        Me.lblAvarandehVajh.Size = New System.Drawing.Size(178, 19)
        Me.lblAvarandehVajh.TabIndex = 5
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(592, 17)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(61, 13)
        Me.Label6.TabIndex = 4
        Me.Label6.Text = "آورنده وجه :"
        '
        'lblNameMoshtary
        '
        Me.lblNameMoshtary.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblNameMoshtary.Location = New System.Drawing.Point(656, 14)
        Me.lblNameMoshtary.Name = "lblNameMoshtary"
        Me.lblNameMoshtary.Size = New System.Drawing.Size(178, 19)
        Me.lblNameMoshtary.TabIndex = 3
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(834, 17)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(67, 13)
        Me.Label4.TabIndex = 2
        Me.Label4.Text = "نام مشتری :"
        '
        'lblShomarehFaktor
        '
        Me.lblShomarehFaktor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblShomarehFaktor.Location = New System.Drawing.Point(906, 14)
        Me.lblShomarehFaktor.Name = "lblShomarehFaktor"
        Me.lblShomarehFaktor.Size = New System.Drawing.Size(93, 19)
        Me.lblShomarehFaktor.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(1001, 17)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(79, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "شماره فاکتــور :"
        '
        'frmDP_DaryaftChek
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1083, 189)
        Me.Controls.Add(Me.GridEXChek)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "frmDP_DaryaftChek"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.RightToLeftLayout = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = " دریافـــت چــــــک"
        Me.TopMost = True
        CType(Me.GridEXChek, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GridEXChek As Janus.Windows.GridEX.GridEX
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents btnDelete As System.Windows.Forms.Button
    Friend WithEvents btnRefresh As System.Windows.Forms.Button
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents lblAvarandehVajh As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents lblNameMoshtary As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents lblShomarehFaktor As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents cmbShomarehHesab As System.Windows.Forms.ComboBox
    Friend WithEvents mskTarikhSanad As System.Windows.Forms.MaskedTextBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents mskTarikhDP As System.Windows.Forms.MaskedTextBox
    Friend WithEvents lblTarikhDP As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents cmbBankSanad As System.Windows.Forms.ComboBox
    Friend WithEvents txtShomarehSanad As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents lblMablaghFaktor As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents txtMablaghPFaktor As clsMaskNumber.MaskNumber
    Friend WithEvents txtMablaghKol As clsMaskNumber.MaskNumber
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents lblMandehFaktor As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents txtCodeShobehSanad As System.Windows.Forms.TextBox
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents txtShobehSanad As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtShomarehHesabSanad As System.Windows.Forms.TextBox
End Class
