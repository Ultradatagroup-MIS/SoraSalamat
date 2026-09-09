<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAN_InsertSerialNumber
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmAN_InsertSerialNumber))
        Dim GridEXSerial_Layout_0 As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim GridEXSerial_Layout_1 As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim GridEXSerial_Layout_2 As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim GridEXSerial_Layout_3 As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim GridEXSerial_Layout_4 As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim GridEXSerial_Layout_5 As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.txtEtelaatForm = New System.Windows.Forms.TextBox()
        Me.txtEtelaatKala = New System.Windows.Forms.TextBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.btnSearch = New System.Windows.Forms.Button()
        Me.tcInsert = New System.Windows.Forms.TabControl()
        Me.tpManualMode = New System.Windows.Forms.TabPage()
        Me.btnRemoveAllSerial = New System.Windows.Forms.Button()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.lblTedadMandeh = New System.Windows.Forms.Label()
        Me.lblTedadSerial = New System.Windows.Forms.Label()
        Me.lblTedadForm = New System.Windows.Forms.Label()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtSerialNumber = New System.Windows.Forms.TextBox()
        Me.tpEcxelMode = New System.Windows.Forms.TabPage()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.GridEXSerial = New Janus.Windows.GridEX.GridEX()
        Me.txtExcelFileName = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.btnSelectFileExcel = New System.Windows.Forms.Button()
        Me.btnConvertFileExcel = New System.Windows.Forms.Button()
        Me.btnClearFileExcel = New System.Windows.Forms.Button()
        Me.txtTypeFileExcel = New System.Windows.Forms.TextBox()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.tcInsert.SuspendLayout()
        Me.tpManualMode.SuspendLayout()
        Me.tpEcxelMode.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        CType(Me.GridEXSerial, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.txtEtelaatForm)
        Me.GroupBox1.Controls.Add(Me.txtEtelaatKala)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(666, 104)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        '
        'txtEtelaatForm
        '
        Me.txtEtelaatForm.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.txtEtelaatForm.Enabled = False
        Me.txtEtelaatForm.Font = New System.Drawing.Font("B Titr", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.txtEtelaatForm.Location = New System.Drawing.Point(3, 73)
        Me.txtEtelaatForm.Multiline = True
        Me.txtEtelaatForm.Name = "txtEtelaatForm"
        Me.txtEtelaatForm.Size = New System.Drawing.Size(660, 28)
        Me.txtEtelaatForm.TabIndex = 38
        Me.txtEtelaatForm.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtEtelaatKala
        '
        Me.txtEtelaatKala.Dock = System.Windows.Forms.DockStyle.Top
        Me.txtEtelaatKala.Enabled = False
        Me.txtEtelaatKala.Font = New System.Drawing.Font("B Titr", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.txtEtelaatKala.Location = New System.Drawing.Point(3, 17)
        Me.txtEtelaatKala.Multiline = True
        Me.txtEtelaatKala.Name = "txtEtelaatKala"
        Me.txtEtelaatKala.Size = New System.Drawing.Size(660, 54)
        Me.txtEtelaatKala.TabIndex = 37
        Me.txtEtelaatKala.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.btnSearch)
        Me.GroupBox2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.GroupBox2.Location = New System.Drawing.Point(0, 453)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(666, 58)
        Me.GroupBox2.TabIndex = 1
        Me.GroupBox2.TabStop = False
        '
        'btnSearch
        '
        Me.btnSearch.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSearch.Location = New System.Drawing.Point(295, 20)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(77, 29)
        Me.btnSearch.TabIndex = 36
        Me.btnSearch.Text = "خـــروج"
        Me.btnSearch.UseVisualStyleBackColor = True
        '
        'tcInsert
        '
        Me.tcInsert.Alignment = System.Windows.Forms.TabAlignment.Left
        Me.tcInsert.Controls.Add(Me.tpManualMode)
        Me.tcInsert.Controls.Add(Me.tpEcxelMode)
        Me.tcInsert.Dock = System.Windows.Forms.DockStyle.Left
        Me.tcInsert.Location = New System.Drawing.Point(0, 104)
        Me.tcInsert.Multiline = True
        Me.tcInsert.Name = "tcInsert"
        Me.tcInsert.RightToLeftLayout = True
        Me.tcInsert.SelectedIndex = 0
        Me.tcInsert.Size = New System.Drawing.Size(333, 349)
        Me.tcInsert.TabIndex = 2
        '
        'tpManualMode
        '
        Me.tpManualMode.BackColor = System.Drawing.SystemColors.Control
        Me.tpManualMode.Controls.Add(Me.btnRemoveAllSerial)
        Me.tpManualMode.Controls.Add(Me.Label10)
        Me.tpManualMode.Controls.Add(Me.Label9)
        Me.tpManualMode.Controls.Add(Me.Label8)
        Me.tpManualMode.Controls.Add(Me.Label7)
        Me.tpManualMode.Controls.Add(Me.Label6)
        Me.tpManualMode.Controls.Add(Me.Label5)
        Me.tpManualMode.Controls.Add(Me.lblTedadMandeh)
        Me.tpManualMode.Controls.Add(Me.lblTedadSerial)
        Me.tpManualMode.Controls.Add(Me.lblTedadForm)
        Me.tpManualMode.Controls.Add(Me.btnCancel)
        Me.tpManualMode.Controls.Add(Me.Label1)
        Me.tpManualMode.Controls.Add(Me.txtSerialNumber)
        Me.tpManualMode.Location = New System.Drawing.Point(24, 4)
        Me.tpManualMode.Name = "tpManualMode"
        Me.tpManualMode.Padding = New System.Windows.Forms.Padding(3)
        Me.tpManualMode.Size = New System.Drawing.Size(305, 341)
        Me.tpManualMode.TabIndex = 0
        Me.tpManualMode.Text = "ثبت دستی"
        '
        'btnRemoveAllSerial
        '
        Me.btnRemoveAllSerial.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnRemoveAllSerial.Location = New System.Drawing.Point(6, 309)
        Me.btnRemoveAllSerial.Name = "btnRemoveAllSerial"
        Me.btnRemoveAllSerial.Size = New System.Drawing.Size(153, 29)
        Me.btnRemoveAllSerial.TabIndex = 47
        Me.btnRemoveAllSerial.Text = "حـذف سریال های ثبت شده"
        Me.btnRemoveAllSerial.UseVisualStyleBackColor = True
        '
        'Label10
        '
        Me.Label10.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label10.AutoSize = True
        Me.Label10.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label10.Location = New System.Drawing.Point(23, 260)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(34, 13)
        Me.Label10.TabIndex = 46
        Me.Label10.Text = "عـــدد"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label9
        '
        Me.Label9.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label9.AutoSize = True
        Me.Label9.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label9.Location = New System.Drawing.Point(23, 202)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(34, 13)
        Me.Label9.TabIndex = 45
        Me.Label9.Text = "عـــدد"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label8
        '
        Me.Label8.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label8.AutoSize = True
        Me.Label8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label8.Location = New System.Drawing.Point(23, 147)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(34, 13)
        Me.Label8.TabIndex = 44
        Me.Label8.Text = "عـــدد"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label7
        '
        Me.Label7.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label7.AutoSize = True
        Me.Label7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label7.Location = New System.Drawing.Point(161, 260)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(127, 13)
        Me.Label7.TabIndex = 43
        Me.Label7.Text = "تعداد  کالای بـدون سریال :"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label6
        '
        Me.Label6.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label6.AutoSize = True
        Me.Label6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label6.Location = New System.Drawing.Point(161, 202)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(129, 13)
        Me.Label6.TabIndex = 42
        Me.Label6.Text = "تعــداد سـریـال ثبت شـده :"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label5
        '
        Me.Label5.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label5.AutoSize = True
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label5.Location = New System.Drawing.Point(161, 147)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(128, 13)
        Me.Label5.TabIndex = 41
        Me.Label5.Text = "تعداد کالای موجود در فرم :"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblTedadMandeh
        '
        Me.lblTedadMandeh.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblTedadMandeh.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblTedadMandeh.Font = New System.Drawing.Font("B Titr", 16.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.lblTedadMandeh.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblTedadMandeh.Location = New System.Drawing.Point(61, 240)
        Me.lblTedadMandeh.Name = "lblTedadMandeh"
        Me.lblTedadMandeh.Size = New System.Drawing.Size(98, 46)
        Me.lblTedadMandeh.TabIndex = 40
        Me.lblTedadMandeh.Text = "0"
        Me.lblTedadMandeh.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblTedadSerial
        '
        Me.lblTedadSerial.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblTedadSerial.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblTedadSerial.Font = New System.Drawing.Font("B Titr", 16.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.lblTedadSerial.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblTedadSerial.Location = New System.Drawing.Point(61, 182)
        Me.lblTedadSerial.Name = "lblTedadSerial"
        Me.lblTedadSerial.Size = New System.Drawing.Size(98, 46)
        Me.lblTedadSerial.TabIndex = 39
        Me.lblTedadSerial.Text = "0"
        Me.lblTedadSerial.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblTedadForm
        '
        Me.lblTedadForm.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblTedadForm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblTedadForm.Font = New System.Drawing.Font("B Titr", 16.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.lblTedadForm.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblTedadForm.Location = New System.Drawing.Point(61, 127)
        Me.lblTedadForm.Name = "lblTedadForm"
        Me.lblTedadForm.Size = New System.Drawing.Size(98, 46)
        Me.lblTedadForm.TabIndex = 38
        Me.lblTedadForm.Text = "0"
        Me.lblTedadForm.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnCancel
        '
        Me.btnCancel.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnCancel.Location = New System.Drawing.Point(6, 44)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(60, 29)
        Me.btnCancel.TabIndex = 2
        Me.btnCancel.Text = "پاک کردن"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label1.AutoSize = True
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(245, 52)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(52, 13)
        Me.Label1.TabIndex = 29
        Me.Label1.Text = "ســریـال :"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtSerialNumber
        '
        Me.txtSerialNumber.Location = New System.Drawing.Point(72, 49)
        Me.txtSerialNumber.MaxLength = 25
        Me.txtSerialNumber.Name = "txtSerialNumber"
        Me.txtSerialNumber.Size = New System.Drawing.Size(170, 21)
        Me.txtSerialNumber.TabIndex = 1
        '
        'tpEcxelMode
        '
        Me.tpEcxelMode.BackColor = System.Drawing.SystemColors.Control
        Me.tpEcxelMode.Controls.Add(Me.txtTypeFileExcel)
        Me.tpEcxelMode.Controls.Add(Me.btnClearFileExcel)
        Me.tpEcxelMode.Controls.Add(Me.btnConvertFileExcel)
        Me.tpEcxelMode.Controls.Add(Me.btnSelectFileExcel)
        Me.tpEcxelMode.Controls.Add(Me.Label2)
        Me.tpEcxelMode.Controls.Add(Me.txtExcelFileName)
        Me.tpEcxelMode.Location = New System.Drawing.Point(24, 4)
        Me.tpEcxelMode.Name = "tpEcxelMode"
        Me.tpEcxelMode.Padding = New System.Windows.Forms.Padding(3)
        Me.tpEcxelMode.Size = New System.Drawing.Size(305, 341)
        Me.tpEcxelMode.TabIndex = 1
        Me.tpEcxelMode.Text = "انتخاب فایل اکسل"
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.GridEXSerial)
        Me.GroupBox3.Dock = System.Windows.Forms.DockStyle.Right
        Me.GroupBox3.Location = New System.Drawing.Point(335, 104)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(331, 349)
        Me.GroupBox3.TabIndex = 3
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = " بارکدهای ثبت شده "
        '
        'GridEXSerial
        '
        Me.GridEXSerial.AllowChildTableGroups = True
        Me.GridEXSerial.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.GridEXSerial.AllowRemoveColumns = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.GridEXSerial.BorderStyle = Janus.Windows.GridEX.BorderStyle.RaisedLight3D
        Me.GridEXSerial.BuiltInTextsData = resources.GetString("GridEXSerial.BuiltInTextsData")
        Me.GridEXSerial.ColumnSetNavigation = Janus.Windows.GridEX.ColumnSetNavigation.ColumnSet
        Me.GridEXSerial.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GridEXSerial.DynamicFiltering = True
        Me.GridEXSerial.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        Me.GridEXSerial.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.GridEXSerial.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges
        Me.GridEXSerial.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridEXSerial.GroupByBoxVisible = False
        Me.GridEXSerial.GroupRowVisualStyle = Janus.Windows.GridEX.GroupRowVisualStyle.Outlook2003
        Me.GridEXSerial.GroupTotals = Janus.Windows.GridEX.GroupTotals.Always
        Me.GridEXSerial.KeepRowSettings = True
        GridEXSerial_Layout_0.Key = "Layout1"
        GridEXSerial_Layout_1.Key = "Layout2"
        GridEXSerial_Layout_2.Key = "Layout3"
        GridEXSerial_Layout_3.Key = "Layout4"
        GridEXSerial_Layout_4.Key = "Layout5"
        GridEXSerial_Layout_5.Key = "Layout6"
        Me.GridEXSerial.Layouts.AddRange(New Janus.Windows.GridEX.GridEXLayout() {GridEXSerial_Layout_0, GridEXSerial_Layout_1, GridEXSerial_Layout_2, GridEXSerial_Layout_3, GridEXSerial_Layout_4, GridEXSerial_Layout_5})
        Me.GridEXSerial.Location = New System.Drawing.Point(3, 17)
        Me.GridEXSerial.Name = "GridEXSerial"
        Me.GridEXSerial.Office2007ColorScheme = Janus.Windows.GridEX.Office2007ColorScheme.Blue
        Me.GridEXSerial.RecordNavigator = True
        Me.GridEXSerial.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.GridEXSerial.SelectionMode = Janus.Windows.GridEX.SelectionMode.MultipleSelection
        Me.GridEXSerial.Size = New System.Drawing.Size(325, 329)
        Me.GridEXSerial.TabIndex = 3
        Me.GridEXSerial.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed
        Me.GridEXSerial.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'txtExcelFileName
        '
        Me.txtExcelFileName.Enabled = False
        Me.txtExcelFileName.Location = New System.Drawing.Point(6, 47)
        Me.txtExcelFileName.Multiline = True
        Me.txtExcelFileName.Name = "txtExcelFileName"
        Me.txtExcelFileName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtExcelFileName.Size = New System.Drawing.Size(293, 44)
        Me.txtExcelFileName.TabIndex = 0
        '
        'Label2
        '
        Me.Label2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label2.AutoSize = True
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(206, 31)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(96, 13)
        Me.Label2.TabIndex = 30
        Me.Label2.Text = "انتخاب فایل اکسل :"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnSelectFileExcel
        '
        Me.btnSelectFileExcel.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSelectFileExcel.Location = New System.Drawing.Point(192, 99)
        Me.btnSelectFileExcel.Name = "btnSelectFileExcel"
        Me.btnSelectFileExcel.Size = New System.Drawing.Size(107, 29)
        Me.btnSelectFileExcel.TabIndex = 48
        Me.btnSelectFileExcel.Text = "انتخاب فایل Excel"
        Me.btnSelectFileExcel.UseVisualStyleBackColor = True
        '
        'btnConvertFileExcel
        '
        Me.btnConvertFileExcel.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnConvertFileExcel.Location = New System.Drawing.Point(6, 99)
        Me.btnConvertFileExcel.Name = "btnConvertFileExcel"
        Me.btnConvertFileExcel.Size = New System.Drawing.Size(107, 29)
        Me.btnConvertFileExcel.TabIndex = 49
        Me.btnConvertFileExcel.Text = "کانورت فایل انتخابی"
        Me.btnConvertFileExcel.UseVisualStyleBackColor = True
        '
        'btnClearFileExcel
        '
        Me.btnClearFileExcel.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnClearFileExcel.Location = New System.Drawing.Point(114, 99)
        Me.btnClearFileExcel.Name = "btnClearFileExcel"
        Me.btnClearFileExcel.Size = New System.Drawing.Size(77, 29)
        Me.btnClearFileExcel.TabIndex = 50
        Me.btnClearFileExcel.Text = "پاک کردن"
        Me.btnClearFileExcel.UseVisualStyleBackColor = True
        '
        'txtTypeFileExcel
        '
        Me.txtTypeFileExcel.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.txtTypeFileExcel.Enabled = False
        Me.txtTypeFileExcel.Font = New System.Drawing.Font("B Titr", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.txtTypeFileExcel.Location = New System.Drawing.Point(3, 213)
        Me.txtTypeFileExcel.Multiline = True
        Me.txtTypeFileExcel.Name = "txtTypeFileExcel"
        Me.txtTypeFileExcel.Size = New System.Drawing.Size(299, 125)
        Me.txtTypeFileExcel.TabIndex = 51
        Me.txtTypeFileExcel.Text = "توجه :" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "1 - فایل اکسل انتخاب شده باید فقط شامل یک Sheet به نام SerialNumber باشد " & _
    "." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "2 - در اولین سطر از ستونی که شماره سریال ها در آن وارد شده است، عبارت SN ثبت " & _
    "شده باشد ."
        Me.txtTypeFileExcel.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'frmAN_InsertSerialNumber
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(666, 511)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.tcInsert)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.Name = "frmAN_InsertSerialNumber"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.RightToLeftLayout = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "وارد نمودن شماره سریال برای کالا"
        Me.TopMost = True
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.tcInsert.ResumeLayout(False)
        Me.tpManualMode.ResumeLayout(False)
        Me.tpManualMode.PerformLayout()
        Me.tpEcxelMode.ResumeLayout(False)
        Me.tpEcxelMode.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        CType(Me.GridEXSerial, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents txtEtelaatKala As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents btnSearch As System.Windows.Forms.Button
    Friend WithEvents tcInsert As System.Windows.Forms.TabControl
    Friend WithEvents tpManualMode As System.Windows.Forms.TabPage
    Friend WithEvents tpEcxelMode As System.Windows.Forms.TabPage
    Friend WithEvents txtSerialNumber As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents GridEXSerial As Janus.Windows.GridEX.GridEX
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtEtelaatForm As System.Windows.Forms.TextBox
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents lblTedadMandeh As System.Windows.Forms.Label
    Friend WithEvents lblTedadSerial As System.Windows.Forms.Label
    Friend WithEvents lblTedadForm As System.Windows.Forms.Label
    Friend WithEvents btnRemoveAllSerial As System.Windows.Forms.Button
    Friend WithEvents btnClearFileExcel As System.Windows.Forms.Button
    Friend WithEvents btnConvertFileExcel As System.Windows.Forms.Button
    Friend WithEvents btnSelectFileExcel As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtExcelFileName As System.Windows.Forms.TextBox
    Friend WithEvents txtTypeFileExcel As System.Windows.Forms.TextBox
End Class
