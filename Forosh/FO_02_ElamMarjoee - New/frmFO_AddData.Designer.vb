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
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtShomarehFaktor = New System.Windows.Forms.TextBox()
        Me.cmbMamorPakhsh = New System.Windows.Forms.ComboBox()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.TxtSharh = New System.Windows.Forms.TextBox()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.GroupBox5 = New System.Windows.Forms.GroupBox()
        Me.rbBargashtAzTozi = New System.Windows.Forms.RadioButton()
        Me.rbBargashtAzForosh = New System.Windows.Forms.RadioButton()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.btnNew = New System.Windows.Forms.Button()
        Me.chkMalyat = New System.Windows.Forms.CheckBox()
        Me.btnSaveSanad = New System.Windows.Forms.Button()
        Me.lblNameMoshtary = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.mskTarikh = New System.Windows.Forms.MaskedTextBox()
        Me.cmbBazaryab = New System.Windows.Forms.ComboBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.txtCodeMoshtary = New System.Windows.Forms.TextBox()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        CType(Me.GridEXSatr, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox5.SuspendLayout()
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
        Me.GroupBox1.Size = New System.Drawing.Size(1340, 827)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.GridEXSatr)
        Me.GroupBox4.Font = New System.Drawing.Font("Tahoma", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox4.Location = New System.Drawing.Point(4, 221)
        Me.GroupBox4.Margin = New System.Windows.Forms.Padding(2)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Padding = New System.Windows.Forms.Padding(2)
        Me.GroupBox4.Size = New System.Drawing.Size(1332, 606)
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
        Me.GridEXSatr.NewRowPosition = Janus.Windows.GridEX.NewRowPosition.BottomRow
        Me.GridEXSatr.Office2007ColorScheme = Janus.Windows.GridEX.Office2007ColorScheme.Blue
        Me.GridEXSatr.RecordNavigator = True
        Me.GridEXSatr.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.GridEXSatr.Size = New System.Drawing.Size(1328, 589)
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
        Me.GroupBox2.Controls.Add(Me.Label2)
        Me.GroupBox2.Controls.Add(Me.txtShomarehFaktor)
        Me.GroupBox2.Controls.Add(Me.cmbMamorPakhsh)
        Me.GroupBox2.Controls.Add(Me.Label23)
        Me.GroupBox2.Controls.Add(Me.TxtSharh)
        Me.GroupBox2.Controls.Add(Me.Label29)
        Me.GroupBox2.Controls.Add(Me.GroupBox5)
        Me.GroupBox2.Controls.Add(Me.Label3)
        Me.GroupBox2.Controls.Add(Me.btnNew)
        Me.GroupBox2.Controls.Add(Me.chkMalyat)
        Me.GroupBox2.Controls.Add(Me.btnSaveSanad)
        Me.GroupBox2.Controls.Add(Me.lblNameMoshtary)
        Me.GroupBox2.Controls.Add(Me.Label4)
        Me.GroupBox2.Controls.Add(Me.mskTarikh)
        Me.GroupBox2.Controls.Add(Me.cmbBazaryab)
        Me.GroupBox2.Controls.Add(Me.Label9)
        Me.GroupBox2.Controls.Add(Me.txtCodeMoshtary)
        Me.GroupBox2.Font = New System.Drawing.Font("Tahoma", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox2.Location = New System.Drawing.Point(4, 60)
        Me.GroupBox2.Margin = New System.Windows.Forms.Padding(2)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Padding = New System.Windows.Forms.Padding(2)
        Me.GroupBox2.Size = New System.Drawing.Size(1332, 157)
        Me.GroupBox2.TabIndex = 0
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "ورود اطلاعات اصلی"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(1184, 46)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Label2.Size = New System.Drawing.Size(70, 13)
        Me.Label2.TabIndex = 111
        Me.Label2.Text = "شماره فاکتور:"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtShomarehFaktor
        '
        Me.txtShomarehFaktor.Location = New System.Drawing.Point(1096, 42)
        Me.txtShomarehFaktor.MaxLength = 10
        Me.txtShomarehFaktor.Name = "txtShomarehFaktor"
        Me.txtShomarehFaktor.Size = New System.Drawing.Size(82, 20)
        Me.txtShomarehFaktor.TabIndex = 112
        '
        'cmbMamorPakhsh
        '
        Me.cmbMamorPakhsh.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbMamorPakhsh.Location = New System.Drawing.Point(859, 119)
        Me.cmbMamorPakhsh.MaxDropDownItems = 20
        Me.cmbMamorPakhsh.Name = "cmbMamorPakhsh"
        Me.cmbMamorPakhsh.Size = New System.Drawing.Size(319, 20)
        Me.cmbMamorPakhsh.TabIndex = 110
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.Location = New System.Drawing.Point(1178, 123)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(65, 13)
        Me.Label23.TabIndex = 109
        Me.Label23.Text = "مامور پخش :"
        '
        'TxtSharh
        '
        Me.TxtSharh.Location = New System.Drawing.Point(486, 22)
        Me.TxtSharh.Name = "TxtSharh"
        Me.TxtSharh.Size = New System.Drawing.Size(317, 20)
        Me.TxtSharh.TabIndex = 109
        '
        'Label29
        '
        Me.Label29.AutoSize = True
        Me.Label29.Location = New System.Drawing.Point(806, 26)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(57, 13)
        Me.Label29.TabIndex = 108
        Me.Label29.Text = "توضیحـات :"
        '
        'GroupBox5
        '
        Me.GroupBox5.BackColor = System.Drawing.Color.Moccasin
        Me.GroupBox5.Controls.Add(Me.rbBargashtAzTozi)
        Me.GroupBox5.Controls.Add(Me.rbBargashtAzForosh)
        Me.GroupBox5.Location = New System.Drawing.Point(707, 67)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(131, 75)
        Me.GroupBox5.TabIndex = 107
        Me.GroupBox5.TabStop = False
        Me.GroupBox5.Text = "نوع مرجوعی"
        '
        'rbBargashtAzTozi
        '
        Me.rbBargashtAzTozi.AutoSize = True
        Me.rbBargashtAzTozi.Location = New System.Drawing.Point(21, 46)
        Me.rbBargashtAzTozi.Name = "rbBargashtAzTozi"
        Me.rbBargashtAzTozi.Size = New System.Drawing.Size(94, 17)
        Me.rbBargashtAzTozi.TabIndex = 108
        Me.rbBargashtAzTozi.TabStop = True
        Me.rbBargashtAzTozi.Text = "برگشت از توزیع"
        Me.rbBargashtAzTozi.UseVisualStyleBackColor = True
        '
        'rbBargashtAzForosh
        '
        Me.rbBargashtAzForosh.AutoSize = True
        Me.rbBargashtAzForosh.Location = New System.Drawing.Point(15, 23)
        Me.rbBargashtAzForosh.Name = "rbBargashtAzForosh"
        Me.rbBargashtAzForosh.Size = New System.Drawing.Size(101, 17)
        Me.rbBargashtAzForosh.TabIndex = 106
        Me.rbBargashtAzForosh.TabStop = True
        Me.rbBargashtAzForosh.Text = "برگشت از فروش"
        Me.rbBargashtAzForosh.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(1180, 22)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(104, 13)
        Me.Label3.TabIndex = 103
        Me.Label3.Text = "تاریخ اعلام مرجوعی:"
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
        Me.chkMalyat.Location = New System.Drawing.Point(880, 22)
        Me.chkMalyat.Name = "chkMalyat"
        Me.chkMalyat.Size = New System.Drawing.Size(128, 17)
        Me.chkMalyat.TabIndex = 101
        Me.chkMalyat.Text = "شامل مالیات و عوارض"
        Me.chkMalyat.UseVisualStyleBackColor = True
        Me.chkMalyat.Visible = False
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
        'lblNameMoshtary
        '
        Me.lblNameMoshtary.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblNameMoshtary.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNameMoshtary.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblNameMoshtary.Location = New System.Drawing.Point(859, 69)
        Me.lblNameMoshtary.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblNameMoshtary.Name = "lblNameMoshtary"
        Me.lblNameMoshtary.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.lblNameMoshtary.Size = New System.Drawing.Size(220, 18)
        Me.lblNameMoshtary.TabIndex = 33
        Me.lblNameMoshtary.Text = "  "
        Me.lblNameMoshtary.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(1177, 97)
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
        Me.cmbBazaryab.Location = New System.Drawing.Point(859, 94)
        Me.cmbBazaryab.Margin = New System.Windows.Forms.Padding(2)
        Me.cmbBazaryab.MaxDropDownItems = 20
        Me.cmbBazaryab.Name = "cmbBazaryab"
        Me.cmbBazaryab.Size = New System.Drawing.Size(319, 20)
        Me.cmbBazaryab.TabIndex = 23
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Tahoma", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(1180, 69)
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
        Me.txtCodeMoshtary.Location = New System.Drawing.Point(1094, 67)
        Me.txtCodeMoshtary.Margin = New System.Windows.Forms.Padding(2)
        Me.txtCodeMoshtary.MaxLength = 15
        Me.txtCodeMoshtary.Name = "txtCodeMoshtary"
        Me.txtCodeMoshtary.Size = New System.Drawing.Size(84, 20)
        Me.txtCodeMoshtary.TabIndex = 25
        Me.txtCodeMoshtary.Text = " "
        '
        'frmFO_AddData
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1355, 749)
        Me.Controls.Add(Me.GroupBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Margin = New System.Windows.Forms.Padding(2)
        Me.MaximizeBox = False
        Me.Name = "frmFO_AddData"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "ورود اطلاعات اعلام مرجوعی"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox4.ResumeLayout(False)
        CType(Me.GridEXSatr, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox5.ResumeLayout(False)
        Me.GroupBox5.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents Label4 As Label
    Friend WithEvents mskTarikh As MaskedTextBox
    Friend WithEvents cmbBazaryab As ComboBox
    Friend WithEvents lblNameMoshtary As Label
    Friend WithEvents GroupBox3 As GroupBox
    Friend WithEvents txtSearchNumber As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents GroupBox4 As GroupBox
    Friend WithEvents GridEXSatr As Janus.Windows.GridEX.GridEX
    Friend WithEvents btnSaveSanad As Button
    Friend WithEvents chkMalyat As CheckBox
    Friend WithEvents btnNew As Button
    Friend WithEvents Label3 As Label
    Friend WithEvents GroupBox5 As GroupBox
    Friend WithEvents rbBargashtAzTozi As RadioButton
    Friend WithEvents rbBargashtAzForosh As RadioButton
    Friend WithEvents TxtSharh As TextBox
    Friend WithEvents Label29 As Label
    Friend WithEvents cmbMamorPakhsh As ComboBox
    Friend WithEvents Label23 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents txtShomarehFaktor As TextBox
    Friend WithEvents Label9 As Label
    Friend WithEvents txtCodeMoshtary As TextBox
End Class
