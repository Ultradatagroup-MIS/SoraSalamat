<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_TaghiratForm
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm_TaghiratForm))
        Dim GridTaghiratForm_Layout_0 As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim GridTaghiratForm_Layout_1 As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim GridTaghiratForm_Layout_2 As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim GridTaghiratForm_Layout_3 As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim GridTaghiratForm_Layout_4 As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim GridTaghiratForm_Layout_5 As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Me.GBData = New System.Windows.Forms.GroupBox()
        Me.txtTaghiratDatabase = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtTaghiratForm = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtNameForm = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cmbPersonel = New System.Windows.Forms.ComboBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.lblTitle = New System.Windows.Forms.Label()
        Me.GridTaghiratForm = New Janus.Windows.GridEX.GridEX()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.btnDelete = New System.Windows.Forms.Button()
        Me.btnAddPersonel = New System.Windows.Forms.Button()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.btnRefresh = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.btnTaeed = New System.Windows.Forms.Button()
        Me.cmbLoadNameForm = New System.Windows.Forms.ComboBox()
        Me.GBData.SuspendLayout()
        CType(Me.GridTaghiratForm, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'GBData
        '
        Me.GBData.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GBData.Controls.Add(Me.cmbLoadNameForm)
        Me.GBData.Controls.Add(Me.txtTaghiratDatabase)
        Me.GBData.Controls.Add(Me.Label3)
        Me.GBData.Controls.Add(Me.txtTaghiratForm)
        Me.GBData.Controls.Add(Me.Label2)
        Me.GBData.Controls.Add(Me.txtNameForm)
        Me.GBData.Controls.Add(Me.Label1)
        Me.GBData.Controls.Add(Me.cmbPersonel)
        Me.GBData.Controls.Add(Me.Label9)
        Me.GBData.Controls.Add(Me.lblTitle)
        Me.GBData.Controls.Add(Me.GridTaghiratForm)
        Me.GBData.Location = New System.Drawing.Point(5, 1)
        Me.GBData.Name = "GBData"
        Me.GBData.Size = New System.Drawing.Size(681, 356)
        Me.GBData.TabIndex = 0
        Me.GBData.TabStop = False
        '
        'txtTaghiratDatabase
        '
        Me.txtTaghiratDatabase.Location = New System.Drawing.Point(35, 238)
        Me.txtTaghiratDatabase.Multiline = True
        Me.txtTaghiratDatabase.Name = "txtTaghiratDatabase"
        Me.txtTaghiratDatabase.Size = New System.Drawing.Size(549, 92)
        Me.txtTaghiratDatabase.TabIndex = 5
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.Maroon
        Me.Label3.Location = New System.Drawing.Point(590, 276)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(59, 14)
        Me.Label3.TabIndex = 54
        Me.Label3.Text = " دیتابیس :"
        '
        'txtTaghiratForm
        '
        Me.txtTaghiratForm.Location = New System.Drawing.Point(35, 128)
        Me.txtTaghiratForm.Multiline = True
        Me.txtTaghiratForm.Name = "txtTaghiratForm"
        Me.txtTaghiratForm.Size = New System.Drawing.Size(549, 92)
        Me.txtTaghiratForm.TabIndex = 4
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.Maroon
        Me.Label2.Location = New System.Drawing.Point(590, 163)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(71, 14)
        Me.Label2.TabIndex = 52
        Me.Label2.Text = "تغییرات فرم :"
        '
        'txtNameForm
        '
        Me.txtNameForm.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtNameForm.Location = New System.Drawing.Point(35, 92)
        Me.txtNameForm.Name = "txtNameForm"
        Me.txtNameForm.Size = New System.Drawing.Size(223, 21)
        Me.txtNameForm.TabIndex = 3
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Maroon
        Me.Label1.Location = New System.Drawing.Point(590, 94)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(50, 14)
        Me.Label1.TabIndex = 50
        Me.Label1.Text = "نام فرم :"
        '
        'cmbPersonel
        '
        Me.cmbPersonel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbPersonel.FormattingEnabled = True
        Me.cmbPersonel.Location = New System.Drawing.Point(409, 56)
        Me.cmbPersonel.Name = "cmbPersonel"
        Me.cmbPersonel.Size = New System.Drawing.Size(175, 21)
        Me.cmbPersonel.TabIndex = 1
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.Color.Maroon
        Me.Label9.Location = New System.Drawing.Point(590, 58)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(66, 14)
        Me.Label9.TabIndex = 48
        Me.Label9.Text = "نام پرسنل :"
        '
        'lblTitle
        '
        Me.lblTitle.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblTitle.AutoSize = True
        Me.lblTitle.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTitle.ForeColor = System.Drawing.Color.Maroon
        Me.lblTitle.Location = New System.Drawing.Point(190, 13)
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Size = New System.Drawing.Size(32, 13)
        Me.lblTitle.TabIndex = 47
        Me.lblTitle.Text = "Title"
        Me.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'GridTaghiratForm
        '
        Me.GridTaghiratForm.AllowChildTableGroups = True
        Me.GridTaghiratForm.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.GridTaghiratForm.AllowDrop = True
        Me.GridTaghiratForm.AllowRemoveColumns = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.GridTaghiratForm.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GridTaghiratForm.BorderStyle = Janus.Windows.GridEX.BorderStyle.Flat
        Me.GridTaghiratForm.BuiltInTextsData = resources.GetString("GridTaghiratForm.BuiltInTextsData")
        Me.GridTaghiratForm.ColumnSetNavigation = Janus.Windows.GridEX.ColumnSetNavigation.ColumnSet
        Me.GridTaghiratForm.DynamicFiltering = True
        Me.GridTaghiratForm.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        Me.GridTaghiratForm.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.GridTaghiratForm.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges
        Me.GridTaghiratForm.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridTaghiratForm.GroupByBoxVisible = False
        Me.GridTaghiratForm.GroupRowVisualStyle = Janus.Windows.GridEX.GroupRowVisualStyle.Outlook2003
        Me.GridTaghiratForm.GroupTotals = Janus.Windows.GridEX.GroupTotals.Always
        Me.GridTaghiratForm.KeepRowSettings = True
        GridTaghiratForm_Layout_0.Key = "Layout1"
        GridTaghiratForm_Layout_1.Key = "Layout2"
        GridTaghiratForm_Layout_2.Key = "Layout3"
        GridTaghiratForm_Layout_3.Key = "Layout4"
        GridTaghiratForm_Layout_4.Key = "Layout5"
        GridTaghiratForm_Layout_5.Key = "Layout6"
        Me.GridTaghiratForm.Layouts.AddRange(New Janus.Windows.GridEX.GridEXLayout() {GridTaghiratForm_Layout_0, GridTaghiratForm_Layout_1, GridTaghiratForm_Layout_2, GridTaghiratForm_Layout_3, GridTaghiratForm_Layout_4, GridTaghiratForm_Layout_5})
        Me.GridTaghiratForm.Location = New System.Drawing.Point(6, 13)
        Me.GridTaghiratForm.Name = "GridTaghiratForm"
        Me.GridTaghiratForm.Office2007ColorScheme = Janus.Windows.GridEX.Office2007ColorScheme.Blue
        Me.GridTaghiratForm.RecordNavigator = True
        Me.GridTaghiratForm.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.GridTaghiratForm.SelectionMode = Janus.Windows.GridEX.SelectionMode.MultipleSelection
        Me.GridTaghiratForm.Size = New System.Drawing.Size(669, 337)
        Me.GridTaghiratForm.TabIndex = 102
        Me.GridTaghiratForm.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed
        Me.GridTaghiratForm.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.btnDelete)
        Me.GroupBox1.Controls.Add(Me.btnAddPersonel)
        Me.GroupBox1.Controls.Add(Me.btnSave)
        Me.GroupBox1.Controls.Add(Me.btnRefresh)
        Me.GroupBox1.Controls.Add(Me.btnCancel)
        Me.GroupBox1.Controls.Add(Me.btnTaeed)
        Me.GroupBox1.Location = New System.Drawing.Point(5, 363)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(681, 59)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = False
        '
        'btnDelete
        '
        Me.btnDelete.Location = New System.Drawing.Point(172, 20)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(86, 28)
        Me.btnDelete.TabIndex = 9
        Me.btnDelete.Text = "حذف"
        Me.btnDelete.UseVisualStyleBackColor = True
        '
        'btnAddPersonel
        '
        Me.btnAddPersonel.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAddPersonel.ForeColor = System.Drawing.Color.Maroon
        Me.btnAddPersonel.Location = New System.Drawing.Point(47, 20)
        Me.btnAddPersonel.Name = "btnAddPersonel"
        Me.btnAddPersonel.Size = New System.Drawing.Size(86, 28)
        Me.btnAddPersonel.TabIndex = 10
        Me.btnAddPersonel.Text = "تنظیمات"
        Me.btnAddPersonel.UseVisualStyleBackColor = True
        '
        'btnSave
        '
        Me.btnSave.Location = New System.Drawing.Point(547, 20)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(86, 28)
        Me.btnSave.TabIndex = 6
        Me.btnSave.Text = "ذخیره"
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'btnRefresh
        '
        Me.btnRefresh.Location = New System.Drawing.Point(297, 20)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(86, 28)
        Me.btnRefresh.TabIndex = 8
        Me.btnRefresh.Text = "بازخـوانی"
        Me.btnRefresh.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        Me.btnCancel.Location = New System.Drawing.Point(422, 20)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(86, 28)
        Me.btnCancel.TabIndex = 7
        Me.btnCancel.Text = "صرف نظر"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'btnTaeed
        '
        Me.btnTaeed.ForeColor = System.Drawing.Color.Maroon
        Me.btnTaeed.Location = New System.Drawing.Point(547, 20)
        Me.btnTaeed.Name = "btnTaeed"
        Me.btnTaeed.Size = New System.Drawing.Size(86, 28)
        Me.btnTaeed.TabIndex = 18
        Me.btnTaeed.Text = "تایید سرپرست"
        Me.btnTaeed.UseVisualStyleBackColor = True
        '
        'cmbLoadNameForm
        '
        Me.cmbLoadNameForm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbLoadNameForm.FormattingEnabled = True
        Me.cmbLoadNameForm.Location = New System.Drawing.Point(264, 92)
        Me.cmbLoadNameForm.Name = "cmbLoadNameForm"
        Me.cmbLoadNameForm.Size = New System.Drawing.Size(320, 21)
        Me.cmbLoadNameForm.TabIndex = 2
        '
        'frm_TaghiratForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(690, 424)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.GBData)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "frm_TaghiratForm"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.RightToLeftLayout = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "ثبت تغییرات فرم ها و دیتابیس"
        Me.GBData.ResumeLayout(False)
        Me.GBData.PerformLayout()
        CType(Me.GridTaghiratForm, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GBData As System.Windows.Forms.GroupBox
    Friend WithEvents lblTitle As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents btnRefresh As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnAddPersonel As System.Windows.Forms.Button
    Friend WithEvents txtTaghiratDatabase As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtTaghiratForm As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtNameForm As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cmbPersonel As System.Windows.Forms.ComboBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents GridTaghiratForm As Janus.Windows.GridEX.GridEX
    Friend WithEvents btnDelete As System.Windows.Forms.Button
    Friend WithEvents btnTaeed As System.Windows.Forms.Button
    Friend WithEvents cmbLoadNameForm As System.Windows.Forms.ComboBox

End Class
