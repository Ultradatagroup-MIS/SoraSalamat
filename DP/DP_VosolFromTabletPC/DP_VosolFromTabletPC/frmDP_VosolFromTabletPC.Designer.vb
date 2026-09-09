<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmDP_VosolFromTabletPC
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmDP_VosolFromTabletPC))
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
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.GridEXSatr = New Janus.Windows.GridEX.GridEX
        Me.btnExit = New System.Windows.Forms.Button
        Me.btnSave = New System.Windows.Forms.Button
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.mskTaTarikh = New System.Windows.Forms.MaskedTextBox
        Me.mskAzTarikh = New System.Windows.Forms.MaskedTextBox
        Me.btnSearch = New System.Windows.Forms.Button
        Me.GroupBox4 = New System.Windows.Forms.GroupBox
        Me.GroupBox1.SuspendLayout()
        CType(Me.GridEXTitr, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        CType(Me.GridEXSatr, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.GridEXTitr)
        Me.GroupBox1.Location = New System.Drawing.Point(4, 52)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(1240, 282)
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
        Me.GridEXTitr.Dock = System.Windows.Forms.DockStyle.Fill
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
        Me.GridEXTitr.Size = New System.Drawing.Size(1234, 262)
        Me.GridEXTitr.TabIndex = 99
        Me.GridEXTitr.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed
        Me.GridEXTitr.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.GridEXSatr)
        Me.GroupBox2.Location = New System.Drawing.Point(4, 336)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(1240, 171)
        Me.GroupBox2.TabIndex = 1
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = " فاکتــورهای این پرداختی "
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
        Me.GridEXSatr.Dock = System.Windows.Forms.DockStyle.Fill
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
        Me.GridEXSatr.Size = New System.Drawing.Size(1234, 151)
        Me.GridEXSatr.TabIndex = 100
        Me.GridEXSatr.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed
        Me.GridEXSatr.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'btnExit
        '
        Me.btnExit.Location = New System.Drawing.Point(9, 12)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(99, 30)
        Me.btnExit.TabIndex = 2
        Me.btnExit.Text = "خــــــروج"
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'btnSave
        '
        Me.btnSave.Location = New System.Drawing.Point(1133, 12)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(99, 30)
        Me.btnSave.TabIndex = 3
        Me.btnSave.Text = "ثبــــت"
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.Label3)
        Me.GroupBox3.Controls.Add(Me.Label2)
        Me.GroupBox3.Controls.Add(Me.mskTaTarikh)
        Me.GroupBox3.Controls.Add(Me.mskAzTarikh)
        Me.GroupBox3.Controls.Add(Me.btnSearch)
        Me.GroupBox3.Location = New System.Drawing.Point(4, 0)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(1240, 58)
        Me.GroupBox3.TabIndex = 4
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "جستجــو "
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(1023, 27)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(44, 13)
        Me.Label3.TabIndex = 11
        Me.Label3.Text = "تا تاریخ :"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(1168, 27)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(45, 13)
        Me.Label2.TabIndex = 10
        Me.Label2.Text = "از تاریخ :"
        '
        'mskTaTarikh
        '
        Me.mskTaTarikh.Location = New System.Drawing.Point(934, 24)
        Me.mskTaTarikh.Mask = "####/##/##"
        Me.mskTaTarikh.Name = "mskTaTarikh"
        Me.mskTaTarikh.PromptChar = Global.Microsoft.VisualBasic.ChrW(32)
        Me.mskTaTarikh.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.mskTaTarikh.Size = New System.Drawing.Size(83, 21)
        Me.mskTaTarikh.TabIndex = 9
        Me.mskTaTarikh.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals
        '
        'mskAzTarikh
        '
        Me.mskAzTarikh.Location = New System.Drawing.Point(1079, 24)
        Me.mskAzTarikh.Mask = "####/##/##"
        Me.mskAzTarikh.Name = "mskAzTarikh"
        Me.mskAzTarikh.PromptChar = Global.Microsoft.VisualBasic.ChrW(32)
        Me.mskAzTarikh.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.mskAzTarikh.Size = New System.Drawing.Size(83, 21)
        Me.mskAzTarikh.TabIndex = 8
        Me.mskAzTarikh.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals
        '
        'btnSearch
        '
        Me.btnSearch.Location = New System.Drawing.Point(9, 18)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(99, 30)
        Me.btnSearch.TabIndex = 4
        Me.btnSearch.Text = "جستجــو"
        Me.btnSearch.UseVisualStyleBackColor = True
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.btnSave)
        Me.GroupBox4.Controls.Add(Me.btnExit)
        Me.GroupBox4.Location = New System.Drawing.Point(4, 510)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(1240, 46)
        Me.GroupBox4.TabIndex = 5
        Me.GroupBox4.TabStop = False
        '
        'frmDP_VosolFromTabletPC
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1248, 558)
        Me.Controls.Add(Me.GroupBox4)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "frmDP_VosolFromTabletPC"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.RightToLeftLayout = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "وصولی های تبلت"
        Me.GroupBox1.ResumeLayout(False)
        CType(Me.GridEXTitr, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        CType(Me.GridEXSatr, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.GroupBox4.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents GridEXTitr As Janus.Windows.GridEX.GridEX
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents GridEXSatr As Janus.Windows.GridEX.GridEX
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents mskTaTarikh As System.Windows.Forms.MaskedTextBox
    Friend WithEvents mskAzTarikh As System.Windows.Forms.MaskedTextBox
    Friend WithEvents btnSearch As System.Windows.Forms.Button
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox

End Class
