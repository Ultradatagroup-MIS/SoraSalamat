<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmFO_PeygiriFaktor
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
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmFO_PeygiriFaktor))
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
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.mskAzTarikh = New System.Windows.Forms.MaskedTextBox
        Me.mskTaTarikh = New System.Windows.Forms.MaskedTextBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.cmbMamorPakhshS = New System.Windows.Forms.ComboBox
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.Label12 = New System.Windows.Forms.Label
        Me.mskShomarehPeygiri = New System.Windows.Forms.MaskedTextBox
        Me.cmbVazeiat = New System.Windows.Forms.ComboBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.btnSearch = New System.Windows.Forms.Button
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.btnDeleteSanad = New System.Windows.Forms.Button
        Me.btnNewSanad = New System.Windows.Forms.Button
        Me.GridEXTitr = New Janus.Windows.GridEX.GridEX
        Me.cmsVazeiat = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.tmsErsal = New System.Windows.Forms.ToolStripMenuItem
        Me.tmsTaeed = New System.Windows.Forms.ToolStripMenuItem
        Me.cmbMamorPakhsh = New System.Windows.Forms.ComboBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.mskTarikh = New System.Windows.Forms.MaskedTextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.btnCancel = New System.Windows.Forms.Button
        Me.btnSaveSanad = New System.Windows.Forms.Button
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        Me.btnNewSatr = New System.Windows.Forms.Button
        Me.btnRemoveFaktor = New System.Windows.Forms.Button
        Me.btnAddFaktor = New System.Windows.Forms.Button
        Me.GridEXSatr = New Janus.Windows.GridEX.GridEX
        Me.cmsMandeh = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.tmsMandeh = New System.Windows.Forms.ToolStripMenuItem
        Me.btnCancelSatr = New System.Windows.Forms.Button
        Me.btnSaveSatr = New System.Windows.Forms.Button
        Me.txtShomarehFaktor = New System.Windows.Forms.TextBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.GroupBox4 = New System.Windows.Forms.GroupBox
        Me.chkPrintWithMandeh = New System.Windows.Forms.CheckBox
        Me.btnReport = New System.Windows.Forms.Button
        Me.btnPrint = New System.Windows.Forms.Button
        Me.btnExit = New System.Windows.Forms.Button
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        CType(Me.GridEXTitr, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.cmsVazeiat.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        CType(Me.GridEXSatr, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.cmsMandeh.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(910, 20)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(45, 13)
        Me.Label4.TabIndex = 98
        Me.Label4.Text = "از تاريخ :"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(781, 20)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(44, 13)
        Me.Label5.TabIndex = 100
        Me.Label5.Text = "تا تاريخ :"
        '
        'mskAzTarikh
        '
        Me.mskAzTarikh.AllowPromptAsInput = False
        Me.mskAzTarikh.Location = New System.Drawing.Point(833, 17)
        Me.mskAzTarikh.Mask = "####/##/##"
        Me.mskAzTarikh.Name = "mskAzTarikh"
        Me.mskAzTarikh.PromptChar = Global.Microsoft.VisualBasic.ChrW(32)
        Me.mskAzTarikh.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.mskAzTarikh.Size = New System.Drawing.Size(76, 21)
        Me.mskAzTarikh.TabIndex = 99
        Me.mskAzTarikh.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals
        '
        'mskTaTarikh
        '
        Me.mskTaTarikh.AllowPromptAsInput = False
        Me.mskTaTarikh.Location = New System.Drawing.Point(704, 17)
        Me.mskTaTarikh.Mask = "####/##/##"
        Me.mskTaTarikh.Name = "mskTaTarikh"
        Me.mskTaTarikh.PromptChar = Global.Microsoft.VisualBasic.ChrW(32)
        Me.mskTaTarikh.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.mskTaTarikh.Size = New System.Drawing.Size(76, 21)
        Me.mskTaTarikh.TabIndex = 101
        Me.mskTaTarikh.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(631, 20)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(65, 13)
        Me.Label7.TabIndex = 103
        Me.Label7.Text = "مامور پخش :"
        '
        'cmbMamorPakhshS
        '
        Me.cmbMamorPakhshS.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbMamorPakhshS.Location = New System.Drawing.Point(414, 17)
        Me.cmbMamorPakhshS.MaxDropDownItems = 20
        Me.cmbMamorPakhshS.Name = "cmbMamorPakhshS"
        Me.cmbMamorPakhshS.Size = New System.Drawing.Size(213, 21)
        Me.cmbMamorPakhshS.TabIndex = 102
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label12)
        Me.GroupBox1.Controls.Add(Me.mskShomarehPeygiri)
        Me.GroupBox1.Controls.Add(Me.cmbVazeiat)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.btnSearch)
        Me.GroupBox1.Controls.Add(Me.cmbMamorPakhshS)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.mskTaTarikh)
        Me.GroupBox1.Controls.Add(Me.mskAzTarikh)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(965, 50)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = " جستجـو "
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(147, 20)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(79, 13)
        Me.Label12.TabIndex = 141
        Me.Label12.Text = "شماره پیگیری :"
        '
        'mskShomarehPeygiri
        '
        Me.mskShomarehPeygiri.AllowPromptAsInput = False
        Me.mskShomarehPeygiri.Location = New System.Drawing.Point(65, 17)
        Me.mskShomarehPeygiri.Mask = "000000000"
        Me.mskShomarehPeygiri.Name = "mskShomarehPeygiri"
        Me.mskShomarehPeygiri.PromptChar = Global.Microsoft.VisualBasic.ChrW(32)
        Me.mskShomarehPeygiri.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.mskShomarehPeygiri.Size = New System.Drawing.Size(76, 21)
        Me.mskShomarehPeygiri.TabIndex = 140
        Me.mskShomarehPeygiri.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.mskShomarehPeygiri.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals
        '
        'cmbVazeiat
        '
        Me.cmbVazeiat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbVazeiat.Location = New System.Drawing.Point(236, 17)
        Me.cmbVazeiat.MaxDropDownItems = 20
        Me.cmbVazeiat.Name = "cmbVazeiat"
        Me.cmbVazeiat.Size = New System.Drawing.Size(118, 21)
        Me.cmbVazeiat.TabIndex = 117
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(358, 20)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(48, 13)
        Me.Label3.TabIndex = 116
        Me.Label3.Text = "وضعیت :"
        '
        'btnSearch
        '
        Me.btnSearch.Image = CType(resources.GetObject("btnSearch.Image"), System.Drawing.Image)
        Me.btnSearch.Location = New System.Drawing.Point(15, 12)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(30, 28)
        Me.btnSearch.TabIndex = 115
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.btnDeleteSanad)
        Me.GroupBox2.Controls.Add(Me.btnNewSanad)
        Me.GroupBox2.Controls.Add(Me.GridEXTitr)
        Me.GroupBox2.Controls.Add(Me.cmbMamorPakhsh)
        Me.GroupBox2.Controls.Add(Me.Label1)
        Me.GroupBox2.Controls.Add(Me.mskTarikh)
        Me.GroupBox2.Controls.Add(Me.Label2)
        Me.GroupBox2.Controls.Add(Me.btnCancel)
        Me.GroupBox2.Controls.Add(Me.btnSaveSanad)
        Me.GroupBox2.Location = New System.Drawing.Point(1, 52)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(962, 252)
        Me.GroupBox2.TabIndex = 2
        Me.GroupBox2.TabStop = False
        '
        'btnDeleteSanad
        '
        Me.btnDeleteSanad.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDeleteSanad.Location = New System.Drawing.Point(389, 219)
        Me.btnDeleteSanad.Name = "btnDeleteSanad"
        Me.btnDeleteSanad.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.btnDeleteSanad.Size = New System.Drawing.Size(92, 26)
        Me.btnDeleteSanad.TabIndex = 102
        Me.btnDeleteSanad.Text = "حـــذف"
        '
        'btnNewSanad
        '
        Me.btnNewSanad.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnNewSanad.Location = New System.Drawing.Point(481, 219)
        Me.btnNewSanad.Name = "btnNewSanad"
        Me.btnNewSanad.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.btnNewSanad.Size = New System.Drawing.Size(92, 26)
        Me.btnNewSanad.TabIndex = 100
        Me.btnNewSanad.Text = "جديد"
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
        Me.GridEXTitr.ContextMenuStrip = Me.cmsVazeiat
        Me.GridEXTitr.Dock = System.Windows.Forms.DockStyle.Top
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
        Me.GridEXTitr.Location = New System.Drawing.Point(3, 17)
        Me.GridEXTitr.Name = "GridEXTitr"
        Me.GridEXTitr.Office2007ColorScheme = Janus.Windows.GridEX.Office2007ColorScheme.Blue
        Me.GridEXTitr.RecordNavigator = True
        Me.GridEXTitr.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.GridEXTitr.SelectionMode = Janus.Windows.GridEX.SelectionMode.MultipleSelection
        Me.GridEXTitr.Size = New System.Drawing.Size(956, 169)
        Me.GridEXTitr.TabIndex = 99
        Me.GridEXTitr.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed
        Me.GridEXTitr.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'cmsVazeiat
        '
        Me.cmsVazeiat.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tmsErsal, Me.tmsTaeed})
        Me.cmsVazeiat.Name = "cmsVazeiat"
        Me.cmsVazeiat.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.cmsVazeiat.Size = New System.Drawing.Size(104, 48)
        '
        'tmsErsal
        '
        Me.tmsErsal.Name = "tmsErsal"
        Me.tmsErsal.Size = New System.Drawing.Size(103, 22)
        Me.tmsErsal.Text = "ارسال"
        '
        'tmsTaeed
        '
        Me.tmsTaeed.Name = "tmsTaeed"
        Me.tmsTaeed.Size = New System.Drawing.Size(103, 22)
        Me.tmsTaeed.Text = "تایید"
        '
        'cmbMamorPakhsh
        '
        Me.cmbMamorPakhsh.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbMamorPakhsh.Location = New System.Drawing.Point(408, 192)
        Me.cmbMamorPakhsh.MaxDropDownItems = 20
        Me.cmbMamorPakhsh.Name = "cmbMamorPakhsh"
        Me.cmbMamorPakhsh.Size = New System.Drawing.Size(213, 21)
        Me.cmbMamorPakhsh.TabIndex = 106
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(629, 195)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(65, 13)
        Me.Label1.TabIndex = 107
        Me.Label1.Text = "مامور پخش :"
        '
        'mskTarikh
        '
        Me.mskTarikh.AllowPromptAsInput = False
        Me.mskTarikh.Location = New System.Drawing.Point(286, 192)
        Me.mskTarikh.Mask = "####/##/##"
        Me.mskTarikh.Name = "mskTarikh"
        Me.mskTarikh.PromptChar = Global.Microsoft.VisualBasic.ChrW(32)
        Me.mskTarikh.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.mskTarikh.Size = New System.Drawing.Size(76, 21)
        Me.mskTarikh.TabIndex = 105
        Me.mskTarikh.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(365, 195)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(35, 13)
        Me.Label2.TabIndex = 104
        Me.Label2.Text = "تاريخ :"
        '
        'btnCancel
        '
        Me.btnCancel.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCancel.Location = New System.Drawing.Point(389, 219)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.btnCancel.Size = New System.Drawing.Size(92, 26)
        Me.btnCancel.TabIndex = 108
        Me.btnCancel.Text = "صرفنظـر"
        '
        'btnSaveSanad
        '
        Me.btnSaveSanad.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSaveSanad.Location = New System.Drawing.Point(481, 219)
        Me.btnSaveSanad.Name = "btnSaveSanad"
        Me.btnSaveSanad.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.btnSaveSanad.Size = New System.Drawing.Size(92, 26)
        Me.btnSaveSanad.TabIndex = 101
        Me.btnSaveSanad.Text = "ذخیـــره"
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.btnNewSatr)
        Me.GroupBox3.Controls.Add(Me.btnRemoveFaktor)
        Me.GroupBox3.Controls.Add(Me.btnAddFaktor)
        Me.GroupBox3.Controls.Add(Me.GridEXSatr)
        Me.GroupBox3.Controls.Add(Me.btnCancelSatr)
        Me.GroupBox3.Controls.Add(Me.btnSaveSatr)
        Me.GroupBox3.Controls.Add(Me.txtShomarehFaktor)
        Me.GroupBox3.Controls.Add(Me.Label6)
        Me.GroupBox3.Location = New System.Drawing.Point(1, 305)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(962, 252)
        Me.GroupBox3.TabIndex = 3
        Me.GroupBox3.TabStop = False
        '
        'btnNewSatr
        '
        Me.btnNewSatr.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnNewSatr.Location = New System.Drawing.Point(435, 220)
        Me.btnNewSatr.Name = "btnNewSatr"
        Me.btnNewSatr.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.btnNewSatr.Size = New System.Drawing.Size(92, 26)
        Me.btnNewSatr.TabIndex = 109
        Me.btnNewSatr.Text = "جديد"
        '
        'btnRemoveFaktor
        '
        Me.btnRemoveFaktor.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnRemoveFaktor.Location = New System.Drawing.Point(343, 220)
        Me.btnRemoveFaktor.Name = "btnRemoveFaktor"
        Me.btnRemoveFaktor.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.btnRemoveFaktor.Size = New System.Drawing.Size(92, 26)
        Me.btnRemoveFaktor.TabIndex = 102
        Me.btnRemoveFaktor.Text = "حـذف فاکتـور"
        '
        'btnAddFaktor
        '
        Me.btnAddFaktor.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAddFaktor.Location = New System.Drawing.Point(527, 220)
        Me.btnAddFaktor.Name = "btnAddFaktor"
        Me.btnAddFaktor.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.btnAddFaktor.Size = New System.Drawing.Size(92, 26)
        Me.btnAddFaktor.TabIndex = 101
        Me.btnAddFaktor.Text = "افـزودن فاکتـور"
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
        Me.GridEXSatr.ContextMenuStrip = Me.cmsMandeh
        Me.GridEXSatr.Dock = System.Windows.Forms.DockStyle.Top
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
        Me.GridEXSatr.Location = New System.Drawing.Point(3, 17)
        Me.GridEXSatr.Name = "GridEXSatr"
        Me.GridEXSatr.Office2007ColorScheme = Janus.Windows.GridEX.Office2007ColorScheme.Blue
        Me.GridEXSatr.RecordNavigator = True
        Me.GridEXSatr.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.GridEXSatr.SelectionMode = Janus.Windows.GridEX.SelectionMode.MultipleSelection
        Me.GridEXSatr.Size = New System.Drawing.Size(956, 169)
        Me.GridEXSatr.TabIndex = 100
        Me.GridEXSatr.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed
        Me.GridEXSatr.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'cmsMandeh
        '
        Me.cmsMandeh.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tmsMandeh})
        Me.cmsMandeh.Name = "cmsMandeh"
        Me.cmsMandeh.Size = New System.Drawing.Size(104, 26)
        '
        'tmsMandeh
        '
        Me.tmsMandeh.Name = "tmsMandeh"
        Me.tmsMandeh.Size = New System.Drawing.Size(103, 22)
        Me.tmsMandeh.Text = "مانــده"
        '
        'btnCancelSatr
        '
        Me.btnCancelSatr.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCancelSatr.Location = New System.Drawing.Point(389, 220)
        Me.btnCancelSatr.Name = "btnCancelSatr"
        Me.btnCancelSatr.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.btnCancelSatr.Size = New System.Drawing.Size(92, 26)
        Me.btnCancelSatr.TabIndex = 111
        Me.btnCancelSatr.Text = "صرفنظـر"
        '
        'btnSaveSatr
        '
        Me.btnSaveSatr.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSaveSatr.Location = New System.Drawing.Point(481, 220)
        Me.btnSaveSatr.Name = "btnSaveSatr"
        Me.btnSaveSatr.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.btnSaveSatr.Size = New System.Drawing.Size(92, 26)
        Me.btnSaveSatr.TabIndex = 110
        Me.btnSaveSatr.Text = "ذخیـــره"
        '
        'txtShomarehFaktor
        '
        Me.txtShomarehFaktor.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtShomarehFaktor.ForeColor = System.Drawing.Color.Black
        Me.txtShomarehFaktor.Location = New System.Drawing.Point(345, 192)
        Me.txtShomarehFaktor.MaxLength = 20
        Me.txtShomarehFaktor.Name = "txtShomarehFaktor"
        Me.txtShomarehFaktor.Size = New System.Drawing.Size(200, 21)
        Me.txtShomarehFaktor.TabIndex = 113
        Me.txtShomarehFaktor.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(548, 195)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(70, 13)
        Me.Label6.TabIndex = 112
        Me.Label6.Text = "شماره فاکتور:"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.chkPrintWithMandeh)
        Me.GroupBox4.Controls.Add(Me.btnReport)
        Me.GroupBox4.Controls.Add(Me.btnPrint)
        Me.GroupBox4.Controls.Add(Me.btnExit)
        Me.GroupBox4.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.GroupBox4.Location = New System.Drawing.Point(0, 554)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(965, 49)
        Me.GroupBox4.TabIndex = 4
        Me.GroupBox4.TabStop = False
        '
        'chkPrintWithMandeh
        '
        Me.chkPrintWithMandeh.AutoSize = True
        Me.chkPrintWithMandeh.Location = New System.Drawing.Point(832, 21)
        Me.chkPrintWithMandeh.Name = "chkPrintWithMandeh"
        Me.chkPrintWithMandeh.Size = New System.Drawing.Size(120, 17)
        Me.chkPrintWithMandeh.TabIndex = 105
        Me.chkPrintWithMandeh.Text = "چاپ با مانده مشتری"
        Me.chkPrintWithMandeh.UseVisualStyleBackColor = True
        '
        'btnReport
        '
        Me.btnReport.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReport.Location = New System.Drawing.Point(528, 15)
        Me.btnReport.Name = "btnReport"
        Me.btnReport.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.btnReport.Size = New System.Drawing.Size(92, 26)
        Me.btnReport.TabIndex = 104
        Me.btnReport.Text = "گـزارش پیگیـری"
        '
        'btnPrint
        '
        Me.btnPrint.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPrint.Location = New System.Drawing.Point(436, 15)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.btnPrint.Size = New System.Drawing.Size(92, 26)
        Me.btnPrint.TabIndex = 103
        Me.btnPrint.Text = "چــــاپ"
        '
        'btnExit
        '
        Me.btnExit.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnExit.Location = New System.Drawing.Point(344, 15)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.btnExit.Size = New System.Drawing.Size(92, 26)
        Me.btnExit.TabIndex = 102
        Me.btnExit.Text = "خـــــروج"
        '
        'frmFO_PeygiriFaktor
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(965, 603)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.GroupBox4)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.Name = "frmFO_PeygiriFaktor"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.RightToLeftLayout = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = " پیگیری فاکتور"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        CType(Me.GridEXTitr, System.ComponentModel.ISupportInitialize).EndInit()
        Me.cmsVazeiat.ResumeLayout(False)
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        CType(Me.GridEXSatr, System.ComponentModel.ISupportInitialize).EndInit()
        Me.cmsMandeh.ResumeLayout(False)
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents mskAzTarikh As System.Windows.Forms.MaskedTextBox
    Friend WithEvents mskTaTarikh As System.Windows.Forms.MaskedTextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents cmbMamorPakhshS As System.Windows.Forms.ComboBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents btnSearch As System.Windows.Forms.Button
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents GridEXTitr As Janus.Windows.GridEX.GridEX
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents btnNewSanad As System.Windows.Forms.Button
    Friend WithEvents btnRemoveFaktor As System.Windows.Forms.Button
    Friend WithEvents btnAddFaktor As System.Windows.Forms.Button
    Friend WithEvents GridEXSatr As Janus.Windows.GridEX.GridEX
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents btnReport As System.Windows.Forms.Button
    Friend WithEvents btnPrint As System.Windows.Forms.Button
    Friend WithEvents btnDeleteSanad As System.Windows.Forms.Button
    Friend WithEvents btnSaveSanad As System.Windows.Forms.Button
    Friend WithEvents cmbMamorPakhsh As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents mskTarikh As System.Windows.Forms.MaskedTextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents cmbVazeiat As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cmsVazeiat As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents tmsErsal As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tmsTaeed As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmsMandeh As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents tmsMandeh As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents chkPrintWithMandeh As System.Windows.Forms.CheckBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents mskShomarehPeygiri As System.Windows.Forms.MaskedTextBox
    Friend WithEvents btnNewSatr As System.Windows.Forms.Button
    Friend WithEvents btnCancelSatr As System.Windows.Forms.Button
    Friend WithEvents btnSaveSatr As System.Windows.Forms.Button
    Friend WithEvents txtShomarehFaktor As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label

End Class
