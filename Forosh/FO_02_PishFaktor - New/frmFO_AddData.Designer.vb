<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmFO_AddData
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmFO_AddData))
        Dim GridEXSatr_Layout_0 As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim GridEXSatr_Layout_1 As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim GridEXSatr_Layout_2 As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim GridEXSatr_Layout_3 As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim GridEXSatr_Layout_4 As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim GridEXSatr_Layout_5 As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.GridEXSatr = New Janus.Windows.GridEX.GridEX()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.txtSearchNumber = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.btnNew = New System.Windows.Forms.Button()
        Me.chkMalyat = New System.Windows.Forms.CheckBox()
        Me.lblAnbar = New System.Windows.Forms.Label()
        Me.btnSaveSanad = New System.Windows.Forms.Button()
        Me.cmbAnbar = New System.Windows.Forms.ComboBox()
        Me.cmbAddress = New System.Windows.Forms.ComboBox()
        Me.lblNameMoshtary = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.mskTarikh = New System.Windows.Forms.MaskedTextBox()
        Me.cmbBazaryab = New System.Windows.Forms.ComboBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.txtCodeMoshtary = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.cmbNoePardakht = New System.Windows.Forms.ComboBox()
        Me.txtTozihat = New System.Windows.Forms.TextBox()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.lblModatCheck = New System.Windows.Forms.Label()
        Me.txtModatCheck = New System.Windows.Forms.TextBox()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        CType(Me.GridEXSatr, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.GroupBox4)
        Me.GroupBox1.Controls.Add(Me.GroupBox3)
        Me.GroupBox1.Controls.Add(Me.GroupBox2)
        Me.GroupBox1.Location = New System.Drawing.Point(9, 10)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(2)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(2)
        Me.GroupBox1.Size = New System.Drawing.Size(1340, 672)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.GridEXSatr)
        Me.GroupBox4.Font = New System.Drawing.Font("Tahoma", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox4.Location = New System.Drawing.Point(4, 182)
        Me.GroupBox4.Margin = New System.Windows.Forms.Padding(2)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Padding = New System.Windows.Forms.Padding(2)
        Me.GroupBox4.Size = New System.Drawing.Size(1332, 485)
        Me.GroupBox4.TabIndex = 2
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "ورود اطلاعات کالا"
        '
        'GridEXSatr
        '
        Me.GridEXSatr.AllowAddNew = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.GridEXSatr.AllowChildTableGroups = True
        Me.GridEXSatr.AllowColumnDrag = False
        Me.GridEXSatr.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.GridEXSatr.AllowDrop = True
        Me.GridEXSatr.AllowRemoveColumns = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.GridEXSatr.AlternatingRowFormatStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
        Me.GridEXSatr.BorderStyle = Janus.Windows.GridEX.BorderStyle.RaisedLight3D
        Me.GridEXSatr.BuiltInTextsData = resources.GetString("GridEXSatr.BuiltInTextsData")
        Me.GridEXSatr.CardCaptionFormatStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
        Me.GridEXSatr.CardColumnHeaderFormatStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
        Me.GridEXSatr.ColumnSetNavigation = Janus.Windows.GridEX.ColumnSetNavigation.ColumnSet
        Me.GridEXSatr.DefaultFilterRowComparison = Janus.Windows.GridEX.FilterConditionOperator.Contains
        Me.GridEXSatr.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GridEXSatr.DynamicFiltering = True
        Me.GridEXSatr.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        Me.GridEXSatr.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.GridEXSatr.FilterRowFormatStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
        Me.GridEXSatr.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges
        Me.GridEXSatr.Font = New System.Drawing.Font("Tahoma", 9.75!)
        Me.GridEXSatr.GroupByBoxVisible = False
        Me.GridEXSatr.GroupRowVisualStyle = Janus.Windows.GridEX.GroupRowVisualStyle.Outlook2003
        Me.GridEXSatr.GroupTotals = Janus.Windows.GridEX.GroupTotals.Always
        Me.GridEXSatr.KeepRowSettings = True
        GridEXSatr_Layout_0.Key = "Layout1"
        GridEXSatr_Layout_1.Key = "Layout2"
        GridEXSatr_Layout_2.Key = "Layout3"
        GridEXSatr_Layout_3.Key = "Layout4"
        GridEXSatr_Layout_4.Key = "Layout5"
        GridEXSatr_Layout_5.Key = "Layout6"
        Me.GridEXSatr.Layouts.AddRange(New Janus.Windows.GridEX.GridEXLayout() {GridEXSatr_Layout_0, GridEXSatr_Layout_1, GridEXSatr_Layout_2, GridEXSatr_Layout_3, GridEXSatr_Layout_4, GridEXSatr_Layout_5})
        Me.GridEXSatr.Location = New System.Drawing.Point(2, 15)
        Me.GridEXSatr.Margin = New System.Windows.Forms.Padding(2)
        Me.GridEXSatr.Name = "GridEXSatr"
        Me.GridEXSatr.Office2007ColorScheme = Janus.Windows.GridEX.Office2007ColorScheme.Blue
        Me.GridEXSatr.RecordNavigator = True
        Me.GridEXSatr.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.GridEXSatr.Size = New System.Drawing.Size(1328, 468)
        Me.GridEXSatr.TabIndex = 100
        Me.GridEXSatr.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed
        Me.GridEXSatr.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'GroupBox3
        '
        Me.GroupBox3.BackColor = System.Drawing.Color.LightSteelBlue
        Me.GroupBox3.Controls.Add(Me.txtSearchNumber)
        Me.GroupBox3.Controls.Add(Me.Label1)
        Me.GroupBox3.Font = New System.Drawing.Font("Tahoma", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox3.Location = New System.Drawing.Point(4, 9)
        Me.GroupBox3.Margin = New System.Windows.Forms.Padding(2)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Padding = New System.Windows.Forms.Padding(2)
        Me.GroupBox3.Size = New System.Drawing.Size(1332, 46)
        Me.GroupBox3.TabIndex = 1
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "جستجو"
        '
        'txtSearchNumber
        '
        Me.txtSearchNumber.Location = New System.Drawing.Point(1094, 17)
        Me.txtSearchNumber.Margin = New System.Windows.Forms.Padding(2)
        Me.txtSearchNumber.MaxLength = 5
        Me.txtSearchNumber.Name = "txtSearchNumber"
        Me.txtSearchNumber.ReadOnly = True
        Me.txtSearchNumber.Size = New System.Drawing.Size(84, 20)
        Me.txtSearchNumber.TabIndex = 20
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(1182, 21)
        Me.Label1.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(41, 13)
        Me.Label1.TabIndex = 19
        Me.Label1.Text = "شماره:"
        '
        'GroupBox2
        '
        Me.GroupBox2.BackColor = System.Drawing.Color.PaleTurquoise
        Me.GroupBox2.Controls.Add(Me.btnNew)
        Me.GroupBox2.Controls.Add(Me.chkMalyat)
        Me.GroupBox2.Controls.Add(Me.lblAnbar)
        Me.GroupBox2.Controls.Add(Me.btnSaveSanad)
        Me.GroupBox2.Controls.Add(Me.cmbAnbar)
        Me.GroupBox2.Controls.Add(Me.cmbAddress)
        Me.GroupBox2.Controls.Add(Me.lblNameMoshtary)
        Me.GroupBox2.Controls.Add(Me.Label18)
        Me.GroupBox2.Controls.Add(Me.Label3)
        Me.GroupBox2.Controls.Add(Me.Label4)
        Me.GroupBox2.Controls.Add(Me.mskTarikh)
        Me.GroupBox2.Controls.Add(Me.cmbBazaryab)
        Me.GroupBox2.Controls.Add(Me.Label9)
        Me.GroupBox2.Controls.Add(Me.txtCodeMoshtary)
        Me.GroupBox2.Controls.Add(Me.Label6)
        Me.GroupBox2.Controls.Add(Me.cmbNoePardakht)
        Me.GroupBox2.Controls.Add(Me.txtTozihat)
        Me.GroupBox2.Controls.Add(Me.Label26)
        Me.GroupBox2.Controls.Add(Me.lblModatCheck)
        Me.GroupBox2.Controls.Add(Me.txtModatCheck)
        Me.GroupBox2.Font = New System.Drawing.Font("Tahoma", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox2.Location = New System.Drawing.Point(4, 60)
        Me.GroupBox2.Margin = New System.Windows.Forms.Padding(2)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Padding = New System.Windows.Forms.Padding(2)
        Me.GroupBox2.Size = New System.Drawing.Size(1332, 118)
        Me.GroupBox2.TabIndex = 0
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "ورود اطلاعات اصلی"
        '
        'btnNew
        '
        Me.btnNew.AccessibleDescription = ""
        Me.btnNew.AccessibleName = ""
        Me.btnNew.BackColor = System.Drawing.Color.MediumOrchid
        Me.btnNew.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnNew.Location = New System.Drawing.Point(4, 65)
        Me.btnNew.Margin = New System.Windows.Forms.Padding(2)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(260, 44)
        Me.btnNew.TabIndex = 102
        Me.btnNew.Text = "اتمام کار / خالی کردن صفحه"
        Me.btnNew.UseVisualStyleBackColor = False
        '
        'chkMalyat
        '
        Me.chkMalyat.AutoSize = True
        Me.chkMalyat.Location = New System.Drawing.Point(425, 16)
        Me.chkMalyat.Name = "chkMalyat"
        Me.chkMalyat.Size = New System.Drawing.Size(128, 17)
        Me.chkMalyat.TabIndex = 101
        Me.chkMalyat.Text = "شامل مالیات و عوارض"
        Me.chkMalyat.UseVisualStyleBackColor = True
        '
        'lblAnbar
        '
        Me.lblAnbar.AutoSize = True
        Me.lblAnbar.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAnbar.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblAnbar.Location = New System.Drawing.Point(968, 18)
        Me.lblAnbar.Name = "lblAnbar"
        Me.lblAnbar.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.lblAnbar.Size = New System.Drawing.Size(31, 13)
        Me.lblAnbar.TabIndex = 24
        Me.lblAnbar.Text = "انبار :"
        Me.lblAnbar.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnSaveSanad
        '
        Me.btnSaveSanad.AccessibleDescription = ""
        Me.btnSaveSanad.AccessibleName = ""
        Me.btnSaveSanad.BackColor = System.Drawing.Color.MediumOrchid
        Me.btnSaveSanad.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnSaveSanad.Location = New System.Drawing.Point(4, 12)
        Me.btnSaveSanad.Margin = New System.Windows.Forms.Padding(2)
        Me.btnSaveSanad.Name = "btnSaveSanad"
        Me.btnSaveSanad.Size = New System.Drawing.Size(260, 45)
        Me.btnSaveSanad.TabIndex = 101
        Me.btnSaveSanad.Text = "&ذخيره"
        Me.btnSaveSanad.UseVisualStyleBackColor = False
        '
        'cmbAnbar
        '
        Me.cmbAnbar.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbAnbar.Location = New System.Drawing.Point(771, 15)
        Me.cmbAnbar.MaxDropDownItems = 20
        Me.cmbAnbar.Name = "cmbAnbar"
        Me.cmbAnbar.Size = New System.Drawing.Size(194, 20)
        Me.cmbAnbar.TabIndex = 23
        '
        'cmbAddress
        '
        Me.cmbAddress.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbAddress.Location = New System.Drawing.Point(771, 65)
        Me.cmbAddress.Margin = New System.Windows.Forms.Padding(2)
        Me.cmbAddress.MaxDropDownItems = 20
        Me.cmbAddress.Name = "cmbAddress"
        Me.cmbAddress.Size = New System.Drawing.Size(407, 20)
        Me.cmbAddress.TabIndex = 34
        '
        'lblNameMoshtary
        '
        Me.lblNameMoshtary.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblNameMoshtary.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNameMoshtary.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblNameMoshtary.Location = New System.Drawing.Point(771, 44)
        Me.lblNameMoshtary.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblNameMoshtary.Name = "lblNameMoshtary"
        Me.lblNameMoshtary.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.lblNameMoshtary.Size = New System.Drawing.Size(311, 17)
        Me.lblNameMoshtary.TabIndex = 33
        Me.lblNameMoshtary.Text = "  "
        Me.lblNameMoshtary.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Font = New System.Drawing.Font("Tahoma", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.Location = New System.Drawing.Point(1180, 65)
        Me.Label18.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(79, 13)
        Me.Label18.TabIndex = 32
        Me.Label18.Text = "آدرس مشتری :"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(1178, 22)
        Me.Label3.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(38, 13)
        Me.Label3.TabIndex = 20
        Me.Label3.Text = "تاريخ  :"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(1177, 92)
        Me.Label4.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(55, 13)
        Me.Label4.TabIndex = 22
        Me.Label4.Text = "فروشنده :"
        '
        'mskTarikh
        '
        Me.mskTarikh.AllowPromptAsInput = False
        Me.mskTarikh.Enabled = False
        Me.mskTarikh.Font = New System.Drawing.Font("Tahoma", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.mskTarikh.Location = New System.Drawing.Point(1094, 19)
        Me.mskTarikh.Margin = New System.Windows.Forms.Padding(2)
        Me.mskTarikh.Mask = "####/##/##"
        Me.mskTarikh.Name = "mskTarikh"
        Me.mskTarikh.PromptChar = Global.Microsoft.VisualBasic.ChrW(32)
        Me.mskTarikh.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.mskTarikh.Size = New System.Drawing.Size(84, 20)
        Me.mskTarikh.TabIndex = 21
        Me.mskTarikh.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals
        '
        'cmbBazaryab
        '
        Me.cmbBazaryab.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbBazaryab.Font = New System.Drawing.Font("Tahoma", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbBazaryab.Location = New System.Drawing.Point(771, 89)
        Me.cmbBazaryab.Margin = New System.Windows.Forms.Padding(2)
        Me.cmbBazaryab.MaxDropDownItems = 20
        Me.cmbBazaryab.Name = "cmbBazaryab"
        Me.cmbBazaryab.Size = New System.Drawing.Size(407, 20)
        Me.cmbBazaryab.TabIndex = 23
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Tahoma", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(1179, 44)
        Me.Label9.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(51, 13)
        Me.Label9.TabIndex = 24
        Me.Label9.Text = "مشتری :"
        '
        'txtCodeMoshtary
        '
        Me.txtCodeMoshtary.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtCodeMoshtary.Font = New System.Drawing.Font("Tahoma", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCodeMoshtary.Location = New System.Drawing.Point(1093, 42)
        Me.txtCodeMoshtary.Margin = New System.Windows.Forms.Padding(2)
        Me.txtCodeMoshtary.MaxLength = 15
        Me.txtCodeMoshtary.Name = "txtCodeMoshtary"
        Me.txtCodeMoshtary.Size = New System.Drawing.Size(84, 20)
        Me.txtCodeMoshtary.TabIndex = 25
        Me.txtCodeMoshtary.Text = " "
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label6.Location = New System.Drawing.Point(670, 16)
        Me.Label6.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Label6.Size = New System.Drawing.Size(70, 13)
        Me.Label6.TabIndex = 26
        Me.Label6.Text = "نحوه پرداخت :"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cmbNoePardakht
        '
        Me.cmbNoePardakht.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbNoePardakht.Font = New System.Drawing.Font("Tahoma", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbNoePardakht.Location = New System.Drawing.Point(585, 12)
        Me.cmbNoePardakht.Margin = New System.Windows.Forms.Padding(2)
        Me.cmbNoePardakht.MaxDropDownItems = 20
        Me.cmbNoePardakht.Name = "cmbNoePardakht"
        Me.cmbNoePardakht.Size = New System.Drawing.Size(84, 20)
        Me.cmbNoePardakht.TabIndex = 27
        '
        'txtTozihat
        '
        Me.txtTozihat.Font = New System.Drawing.Font("Tahoma", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTozihat.Location = New System.Drawing.Point(285, 65)
        Me.txtTozihat.Margin = New System.Windows.Forms.Padding(2)
        Me.txtTozihat.MaxLength = 200
        Me.txtTozihat.Multiline = True
        Me.txtTozihat.Name = "txtTozihat"
        Me.txtTozihat.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtTozihat.Size = New System.Drawing.Size(384, 22)
        Me.txtTozihat.TabIndex = 31
        Me.txtTozihat.Text = " "
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.Font = New System.Drawing.Font("Tahoma", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label26.Location = New System.Drawing.Point(668, 73)
        Me.Label26.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(57, 13)
        Me.Label26.TabIndex = 30
        Me.Label26.Text = "توضیحـات :"
        '
        'lblModatCheck
        '
        Me.lblModatCheck.AutoSize = True
        Me.lblModatCheck.Font = New System.Drawing.Font("Tahoma", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblModatCheck.Location = New System.Drawing.Point(669, 40)
        Me.lblModatCheck.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblModatCheck.Name = "lblModatCheck"
        Me.lblModatCheck.Size = New System.Drawing.Size(56, 13)
        Me.lblModatCheck.TabIndex = 28
        Me.lblModatCheck.Text = "مدت چک :"
        Me.lblModatCheck.Visible = False
        '
        'txtModatCheck
        '
        Me.txtModatCheck.Font = New System.Drawing.Font("Tahoma", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtModatCheck.Location = New System.Drawing.Point(585, 37)
        Me.txtModatCheck.Margin = New System.Windows.Forms.Padding(2)
        Me.txtModatCheck.MaxLength = 3
        Me.txtModatCheck.Name = "txtModatCheck"
        Me.txtModatCheck.Size = New System.Drawing.Size(84, 20)
        Me.txtModatCheck.TabIndex = 29
        Me.txtModatCheck.Visible = False
        '
        'frmFO_AddData
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1360, 691)
        Me.Controls.Add(Me.GroupBox1)
        Me.Margin = New System.Windows.Forms.Padding(2)
        Me.MaximizeBox = False
        Me.Name = "frmFO_AddData"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "ورود اطلاعات پیش فاکتور"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox4.ResumeLayout(False)
        CType(Me.GridEXSatr, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents mskTarikh As MaskedTextBox
    Friend WithEvents cmbBazaryab As ComboBox
    Friend WithEvents Label9 As Label
    Friend WithEvents txtCodeMoshtary As TextBox
    Friend WithEvents Label6 As Label
    Friend WithEvents cmbNoePardakht As ComboBox
    Friend WithEvents txtTozihat As TextBox
    Friend WithEvents Label26 As Label
    Friend WithEvents lblModatCheck As Label
    Friend WithEvents txtModatCheck As TextBox
    Friend WithEvents Label18 As Label
    Friend WithEvents lblNameMoshtary As Label
    Friend WithEvents cmbAddress As ComboBox
    Friend WithEvents GroupBox3 As GroupBox
    Friend WithEvents txtSearchNumber As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents GroupBox4 As GroupBox
    Friend WithEvents GridEXSatr As Janus.Windows.GridEX.GridEX
    Friend WithEvents btnSaveSanad As Button
    Friend WithEvents lblAnbar As Label
    Friend WithEvents cmbAnbar As ComboBox
    Friend WithEvents chkMalyat As CheckBox
    Friend WithEvents btnNew As Button
End Class
