<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmFO_SabadGorohKala
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmFO_SabadGorohKala))
        Dim GridEXTitr_Layout_0 As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout
        Dim GridEXTitr_Layout_1 As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout
        Dim GridEXTitr_Layout_2 As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout
        Dim GridEXTitr_Layout_3 As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout
        Dim GridEXTitr_Layout_4 As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout
        Dim GridEXTitr_Layout_5 As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout
        Dim GridEXSatr_Layout_0 As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout
        Dim GridEXSatr_Layout_1 As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout
        Dim GridEXSatr_Layout_2 As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout
        Dim GridEXSatr_Layout_3 As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout
        Dim GridEXSatr_Layout_4 As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout
        Dim GridEXSatr_Layout_5 As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.GridEXTitr = New Janus.Windows.GridEX.GridEX
        Me.chkFaal = New System.Windows.Forms.CheckBox
        Me.txtNameSabadGorohKala = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.GridEXSatr = New Janus.Windows.GridEX.GridEX
        Me.cmbGoroh5 = New System.Windows.Forms.ComboBox
        Me.cmbGoroh4 = New System.Windows.Forms.ComboBox
        Me.cmbGoroh3 = New System.Windows.Forms.ComboBox
        Me.cmbGoroh2 = New System.Windows.Forms.ComboBox
        Me.cmbGoroh1 = New System.Windows.Forms.ComboBox
        Me.Label8 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.mskHadeAghalKaridDarFaktor = New System.Windows.Forms.TextBox
        Me.mskTedadKala = New System.Windows.Forms.TextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.btnDeleteTitr = New System.Windows.Forms.Button
        Me.btnNewTitr = New System.Windows.Forms.Button
        Me.btnEditTitr = New System.Windows.Forms.Button
        Me.btnCancelTitr = New System.Windows.Forms.Button
        Me.btnSaveTitr = New System.Windows.Forms.Button
        Me.btnFullDelete = New System.Windows.Forms.Button
        Me.btnNewRow = New System.Windows.Forms.Button
        Me.btnDeleteRow = New System.Windows.Forms.Button
        Me.btnEditRow = New System.Windows.Forms.Button
        Me.btnCancelRow = New System.Windows.Forms.Button
        Me.btnSaveRow = New System.Windows.Forms.Button
        Me.btnExit = New System.Windows.Forms.Button
        Me.btnPrint = New System.Windows.Forms.Button
        Me.GroupBox1.SuspendLayout()
        CType(Me.GridEXTitr, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        CType(Me.GridEXSatr, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.GridEXTitr)
        Me.GroupBox1.Controls.Add(Me.chkFaal)
        Me.GroupBox1.Controls.Add(Me.txtNameSabadGorohKala)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Location = New System.Drawing.Point(3, 2)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(707, 239)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        '
        'GridEXTitr
        '
        Me.GridEXTitr.AllowChildTableGroups = True
        Me.GridEXTitr.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.GridEXTitr.AllowDrop = True
        Me.GridEXTitr.AllowRemoveColumns = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.GridEXTitr.BorderStyle = Janus.Windows.GridEX.BorderStyle.RaisedLight3D
        Me.GridEXTitr.BuiltInTextsData = resources.GetString("GridEXTitr.BuiltInTextsData")
        Me.GridEXTitr.ColumnSetNavigation = Janus.Windows.GridEX.ColumnSetNavigation.ColumnSet
        Me.GridEXTitr.DynamicFiltering = True
        Me.GridEXTitr.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        Me.GridEXTitr.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.GridEXTitr.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges
        Me.GridEXTitr.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridEXTitr.GroupByBoxVisible = False
        Me.GridEXTitr.GroupRowVisualStyle = Janus.Windows.GridEX.GroupRowVisualStyle.Outlook2003
        Me.GridEXTitr.GroupTotals = Janus.Windows.GridEX.GroupTotals.Always
        Me.GridEXTitr.KeepRowSettings = True
        GridEXTitr_Layout_0.Key = "Layout1"
        GridEXTitr_Layout_1.Key = "Layout2"
        GridEXTitr_Layout_2.Key = "Layout3"
        GridEXTitr_Layout_3.Key = "Layout4"
        GridEXTitr_Layout_4.Key = "Layout5"
        GridEXTitr_Layout_5.Key = "Layout6"
        Me.GridEXTitr.Layouts.AddRange(New Janus.Windows.GridEX.GridEXLayout() {GridEXTitr_Layout_0, GridEXTitr_Layout_1, GridEXTitr_Layout_2, GridEXTitr_Layout_3, GridEXTitr_Layout_4, GridEXTitr_Layout_5})
        Me.GridEXTitr.Location = New System.Drawing.Point(6, 11)
        Me.GridEXTitr.Name = "GridEXTitr"
        Me.GridEXTitr.Office2007ColorScheme = Janus.Windows.GridEX.Office2007ColorScheme.Blue
        Me.GridEXTitr.RecordNavigator = True
        Me.GridEXTitr.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.GridEXTitr.Size = New System.Drawing.Size(695, 190)
        Me.GridEXTitr.TabIndex = 98
        Me.GridEXTitr.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed
        Me.GridEXTitr.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'chkFaal
        '
        Me.chkFaal.AutoSize = True
        Me.chkFaal.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.chkFaal.Location = New System.Drawing.Point(167, 212)
        Me.chkFaal.Name = "chkFaal"
        Me.chkFaal.Size = New System.Drawing.Size(62, 17)
        Me.chkFaal.TabIndex = 101
        Me.chkFaal.Text = "فعـــــال"
        Me.chkFaal.UseVisualStyleBackColor = True
        '
        'txtNameSabadGorohKala
        '
        Me.txtNameSabadGorohKala.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.txtNameSabadGorohKala.Location = New System.Drawing.Point(282, 210)
        Me.txtNameSabadGorohKala.Name = "txtNameSabadGorohKala"
        Me.txtNameSabadGorohKala.Size = New System.Drawing.Size(197, 21)
        Me.txtNameSabadGorohKala.TabIndex = 100
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Label1.Location = New System.Drawing.Point(485, 213)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(107, 13)
        Me.Label1.TabIndex = 99
        Me.Label1.Text = "نام سبـــــد گروه کالا :"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.GridEXSatr)
        Me.GroupBox2.Controls.Add(Me.cmbGoroh5)
        Me.GroupBox2.Controls.Add(Me.cmbGoroh4)
        Me.GroupBox2.Controls.Add(Me.cmbGoroh3)
        Me.GroupBox2.Controls.Add(Me.cmbGoroh2)
        Me.GroupBox2.Controls.Add(Me.cmbGoroh1)
        Me.GroupBox2.Controls.Add(Me.Label8)
        Me.GroupBox2.Controls.Add(Me.Label7)
        Me.GroupBox2.Controls.Add(Me.Label6)
        Me.GroupBox2.Controls.Add(Me.Label3)
        Me.GroupBox2.Controls.Add(Me.Label2)
        Me.GroupBox2.Controls.Add(Me.mskHadeAghalKaridDarFaktor)
        Me.GroupBox2.Controls.Add(Me.mskTedadKala)
        Me.GroupBox2.Controls.Add(Me.Label5)
        Me.GroupBox2.Controls.Add(Me.Label4)
        Me.GroupBox2.Location = New System.Drawing.Point(3, 276)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(707, 260)
        Me.GroupBox2.TabIndex = 1
        Me.GroupBox2.TabStop = False
        '
        'GridEXSatr
        '
        Me.GridEXSatr.AllowChildTableGroups = True
        Me.GridEXSatr.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.GridEXSatr.AllowDrop = True
        Me.GridEXSatr.AllowRemoveColumns = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.GridEXSatr.BorderStyle = Janus.Windows.GridEX.BorderStyle.RaisedLight3D
        Me.GridEXSatr.BuiltInTextsData = resources.GetString("GridEXSatr.BuiltInTextsData")
        Me.GridEXSatr.ColumnSetNavigation = Janus.Windows.GridEX.ColumnSetNavigation.ColumnSet
        Me.GridEXSatr.DynamicFiltering = True
        Me.GridEXSatr.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        Me.GridEXSatr.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.GridEXSatr.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges
        Me.GridEXSatr.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
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
        Me.GridEXSatr.Location = New System.Drawing.Point(6, 13)
        Me.GridEXSatr.Name = "GridEXSatr"
        Me.GridEXSatr.Office2007ColorScheme = Janus.Windows.GridEX.Office2007ColorScheme.Blue
        Me.GridEXSatr.RecordNavigator = True
        Me.GridEXSatr.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.GridEXSatr.Size = New System.Drawing.Size(695, 150)
        Me.GridEXSatr.TabIndex = 99
        Me.GridEXSatr.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed
        Me.GridEXSatr.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'cmbGoroh5
        '
        Me.cmbGoroh5.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbGoroh5.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.cmbGoroh5.FormattingEnabled = True
        Me.cmbGoroh5.Location = New System.Drawing.Point(364, 231)
        Me.cmbGoroh5.Name = "cmbGoroh5"
        Me.cmbGoroh5.Size = New System.Drawing.Size(265, 21)
        Me.cmbGoroh5.TabIndex = 120
        '
        'cmbGoroh4
        '
        Me.cmbGoroh4.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbGoroh4.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.cmbGoroh4.FormattingEnabled = True
        Me.cmbGoroh4.Location = New System.Drawing.Point(23, 204)
        Me.cmbGoroh4.Name = "cmbGoroh4"
        Me.cmbGoroh4.Size = New System.Drawing.Size(265, 21)
        Me.cmbGoroh4.TabIndex = 119
        '
        'cmbGoroh3
        '
        Me.cmbGoroh3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbGoroh3.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.cmbGoroh3.FormattingEnabled = True
        Me.cmbGoroh3.Location = New System.Drawing.Point(364, 204)
        Me.cmbGoroh3.Name = "cmbGoroh3"
        Me.cmbGoroh3.Size = New System.Drawing.Size(265, 21)
        Me.cmbGoroh3.TabIndex = 118
        '
        'cmbGoroh2
        '
        Me.cmbGoroh2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbGoroh2.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.cmbGoroh2.FormattingEnabled = True
        Me.cmbGoroh2.Location = New System.Drawing.Point(23, 177)
        Me.cmbGoroh2.Name = "cmbGoroh2"
        Me.cmbGoroh2.Size = New System.Drawing.Size(265, 21)
        Me.cmbGoroh2.TabIndex = 117
        '
        'cmbGoroh1
        '
        Me.cmbGoroh1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbGoroh1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.cmbGoroh1.FormattingEnabled = True
        Me.cmbGoroh1.Location = New System.Drawing.Point(364, 177)
        Me.cmbGoroh1.Name = "cmbGoroh1"
        Me.cmbGoroh1.Size = New System.Drawing.Size(265, 21)
        Me.cmbGoroh1.TabIndex = 116
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Label8.Location = New System.Drawing.Point(634, 234)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(64, 13)
        Me.Label8.TabIndex = 115
        Me.Label8.Text = "گــــــروه 5  :"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Label7.Location = New System.Drawing.Point(294, 207)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(64, 13)
        Me.Label7.TabIndex = 114
        Me.Label7.Text = "گــــــروه 4  :"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Label6.Location = New System.Drawing.Point(634, 207)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(64, 13)
        Me.Label6.TabIndex = 113
        Me.Label6.Text = "گــــــروه 3  :"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Label3.Location = New System.Drawing.Point(294, 180)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(64, 13)
        Me.Label3.TabIndex = 113
        Me.Label3.Text = "گــــــروه 2  :"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Label2.Location = New System.Drawing.Point(634, 180)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(64, 13)
        Me.Label2.TabIndex = 112
        Me.Label2.Text = "گــــــروه 1  :"
        '
        'mskHadeAghalKaridDarFaktor
        '
        Me.mskHadeAghalKaridDarFaktor.BackColor = System.Drawing.SystemColors.Window
        Me.mskHadeAghalKaridDarFaktor.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.mskHadeAghalKaridDarFaktor.Location = New System.Drawing.Point(23, 231)
        Me.mskHadeAghalKaridDarFaktor.MaxLength = 8
        Me.mskHadeAghalKaridDarFaktor.Name = "mskHadeAghalKaridDarFaktor"
        Me.mskHadeAghalKaridDarFaktor.Size = New System.Drawing.Size(60, 21)
        Me.mskHadeAghalKaridDarFaktor.TabIndex = 111
        '
        'mskTedadKala
        '
        Me.mskTedadKala.BackColor = System.Drawing.SystemColors.Window
        Me.mskTedadKala.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.mskTedadKala.Location = New System.Drawing.Point(244, 231)
        Me.mskTedadKala.MaxLength = 8
        Me.mskTedadKala.Name = "mskTedadKala"
        Me.mskTedadKala.Size = New System.Drawing.Size(60, 21)
        Me.mskTedadKala.TabIndex = 110
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Label5.Location = New System.Drawing.Point(89, 234)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(149, 13)
        Me.Label5.TabIndex = 107
        Me.Label5.Text = "حداقل تعداد خــرید در فاکتـــور :"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Label4.Location = New System.Drawing.Point(309, 234)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(49, 13)
        Me.Label4.TabIndex = 105
        Me.Label4.Text = "تعـــداد  :"
        '
        'btnDeleteTitr
        '
        Me.btnDeleteTitr.AccessibleDescription = ""
        Me.btnDeleteTitr.AccessibleName = ""
        Me.btnDeleteTitr.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.btnDeleteTitr.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnDeleteTitr.Location = New System.Drawing.Point(418, 250)
        Me.btnDeleteTitr.Name = "btnDeleteTitr"
        Me.btnDeleteTitr.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.btnDeleteTitr.Size = New System.Drawing.Size(112, 23)
        Me.btnDeleteTitr.TabIndex = 11
        Me.btnDeleteTitr.Text = "حذف"
        '
        'btnNewTitr
        '
        Me.btnNewTitr.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.btnNewTitr.Location = New System.Drawing.Point(306, 250)
        Me.btnNewTitr.Name = "btnNewTitr"
        Me.btnNewTitr.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.btnNewTitr.Size = New System.Drawing.Size(112, 23)
        Me.btnNewTitr.TabIndex = 9
        Me.btnNewTitr.Text = "جديد"
        '
        'btnEditTitr
        '
        Me.btnEditTitr.AccessibleDescription = ""
        Me.btnEditTitr.AccessibleName = ""
        Me.btnEditTitr.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.btnEditTitr.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnEditTitr.Location = New System.Drawing.Point(194, 250)
        Me.btnEditTitr.Name = "btnEditTitr"
        Me.btnEditTitr.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.btnEditTitr.Size = New System.Drawing.Size(112, 23)
        Me.btnEditTitr.TabIndex = 10
        Me.btnEditTitr.Text = "ويرايش"
        '
        'btnCancelTitr
        '
        Me.btnCancelTitr.AccessibleDescription = ""
        Me.btnCancelTitr.AccessibleName = ""
        Me.btnCancelTitr.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.btnCancelTitr.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnCancelTitr.Location = New System.Drawing.Point(354, 250)
        Me.btnCancelTitr.Name = "btnCancelTitr"
        Me.btnCancelTitr.Size = New System.Drawing.Size(104, 23)
        Me.btnCancelTitr.TabIndex = 8
        Me.btnCancelTitr.Text = "صرفنظر"
        '
        'btnSaveTitr
        '
        Me.btnSaveTitr.AccessibleDescription = ""
        Me.btnSaveTitr.AccessibleName = ""
        Me.btnSaveTitr.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.btnSaveTitr.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnSaveTitr.Location = New System.Drawing.Point(250, 250)
        Me.btnSaveTitr.Name = "btnSaveTitr"
        Me.btnSaveTitr.Size = New System.Drawing.Size(104, 23)
        Me.btnSaveTitr.TabIndex = 7
        Me.btnSaveTitr.Text = "ذخيره"
        '
        'btnFullDelete
        '
        Me.btnFullDelete.AccessibleDescription = ""
        Me.btnFullDelete.AccessibleName = ""
        Me.btnFullDelete.BackColor = System.Drawing.Color.Maroon
        Me.btnFullDelete.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnFullDelete.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.btnFullDelete.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnFullDelete.Location = New System.Drawing.Point(9, 250)
        Me.btnFullDelete.Name = "btnFullDelete"
        Me.btnFullDelete.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.btnFullDelete.Size = New System.Drawing.Size(107, 23)
        Me.btnFullDelete.TabIndex = 17
        Me.btnFullDelete.Text = "حذف سبد گروه کالا"
        Me.btnFullDelete.UseVisualStyleBackColor = False
        Me.btnFullDelete.Visible = False
        '
        'btnNewRow
        '
        Me.btnNewRow.AccessibleDescription = ""
        Me.btnNewRow.AccessibleName = ""
        Me.btnNewRow.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.btnNewRow.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnNewRow.Location = New System.Drawing.Point(306, 541)
        Me.btnNewRow.Name = "btnNewRow"
        Me.btnNewRow.Size = New System.Drawing.Size(112, 25)
        Me.btnNewRow.TabIndex = 20
        Me.btnNewRow.Text = "رديف جديد"
        '
        'btnDeleteRow
        '
        Me.btnDeleteRow.AccessibleDescription = ""
        Me.btnDeleteRow.AccessibleName = ""
        Me.btnDeleteRow.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.btnDeleteRow.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnDeleteRow.Location = New System.Drawing.Point(418, 541)
        Me.btnDeleteRow.Name = "btnDeleteRow"
        Me.btnDeleteRow.Size = New System.Drawing.Size(112, 25)
        Me.btnDeleteRow.TabIndex = 22
        Me.btnDeleteRow.Text = "حذف"
        '
        'btnEditRow
        '
        Me.btnEditRow.AccessibleDescription = ""
        Me.btnEditRow.AccessibleName = ""
        Me.btnEditRow.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.btnEditRow.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnEditRow.Location = New System.Drawing.Point(194, 541)
        Me.btnEditRow.Name = "btnEditRow"
        Me.btnEditRow.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.btnEditRow.Size = New System.Drawing.Size(112, 25)
        Me.btnEditRow.TabIndex = 18
        Me.btnEditRow.Text = "ويرايش"
        '
        'btnCancelRow
        '
        Me.btnCancelRow.AccessibleDescription = ""
        Me.btnCancelRow.AccessibleName = ""
        Me.btnCancelRow.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.btnCancelRow.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnCancelRow.Location = New System.Drawing.Point(354, 542)
        Me.btnCancelRow.Name = "btnCancelRow"
        Me.btnCancelRow.Size = New System.Drawing.Size(112, 24)
        Me.btnCancelRow.TabIndex = 21
        Me.btnCancelRow.Text = "صرفنظر"
        '
        'btnSaveRow
        '
        Me.btnSaveRow.AccessibleDescription = ""
        Me.btnSaveRow.AccessibleName = ""
        Me.btnSaveRow.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.btnSaveRow.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnSaveRow.Location = New System.Drawing.Point(242, 542)
        Me.btnSaveRow.Name = "btnSaveRow"
        Me.btnSaveRow.Size = New System.Drawing.Size(112, 24)
        Me.btnSaveRow.TabIndex = 19
        Me.btnSaveRow.Text = "ذخيره"
        '
        'btnExit
        '
        Me.btnExit.AccessibleDescription = ""
        Me.btnExit.AccessibleName = ""
        Me.btnExit.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.btnExit.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnExit.Location = New System.Drawing.Point(625, 541)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(85, 25)
        Me.btnExit.TabIndex = 23
        Me.btnExit.Text = "خــروج"
        '
        'btnPrint
        '
        Me.btnPrint.AccessibleDescription = ""
        Me.btnPrint.AccessibleName = ""
        Me.btnPrint.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.btnPrint.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnPrint.Location = New System.Drawing.Point(625, 250)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(85, 23)
        Me.btnPrint.TabIndex = 24
        Me.btnPrint.Text = "چـــاپ"
        '
        'frmFO_SabadGorohKala
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(714, 572)
        Me.Controls.Add(Me.btnPrint)
        Me.Controls.Add(Me.btnEditTitr)
        Me.Controls.Add(Me.btnNewTitr)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.btnNewRow)
        Me.Controls.Add(Me.btnDeleteRow)
        Me.Controls.Add(Me.btnEditRow)
        Me.Controls.Add(Me.btnDeleteTitr)
        Me.Controls.Add(Me.btnCancelRow)
        Me.Controls.Add(Me.btnSaveTitr)
        Me.Controls.Add(Me.btnSaveRow)
        Me.Controls.Add(Me.btnFullDelete)
        Me.Controls.Add(Me.btnCancelTitr)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.MaximizeBox = False
        Me.Name = "frmFO_SabadGorohKala"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.RightToLeftLayout = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = " سبـــد گروه کالا"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.GridEXTitr, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        CType(Me.GridEXSatr, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents btnDeleteTitr As System.Windows.Forms.Button
    Friend WithEvents btnNewTitr As System.Windows.Forms.Button
    Friend WithEvents btnEditTitr As System.Windows.Forms.Button
    Friend WithEvents btnCancelTitr As System.Windows.Forms.Button
    Friend WithEvents btnSaveTitr As System.Windows.Forms.Button
    Friend WithEvents GridEXTitr As Janus.Windows.GridEX.GridEX
    Friend WithEvents GridEXSatr As Janus.Windows.GridEX.GridEX
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtNameSabadGorohKala As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents chkFaal As System.Windows.Forms.CheckBox
    Friend WithEvents btnFullDelete As System.Windows.Forms.Button
    Friend WithEvents btnNewRow As System.Windows.Forms.Button
    Friend WithEvents btnDeleteRow As System.Windows.Forms.Button
    Friend WithEvents btnEditRow As System.Windows.Forms.Button
    Friend WithEvents btnCancelRow As System.Windows.Forms.Button
    Friend WithEvents btnSaveRow As System.Windows.Forms.Button
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents mskHadeAghalKaridDarFaktor As System.Windows.Forms.TextBox
    Friend WithEvents mskTedadKala As System.Windows.Forms.TextBox
    Friend WithEvents cmbGoroh3 As System.Windows.Forms.ComboBox
    Friend WithEvents cmbGoroh2 As System.Windows.Forms.ComboBox
    Friend WithEvents cmbGoroh1 As System.Windows.Forms.ComboBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cmbGoroh5 As System.Windows.Forms.ComboBox
    Friend WithEvents cmbGoroh4 As System.Windows.Forms.ComboBox
    Friend WithEvents btnPrint As System.Windows.Forms.Button

End Class
