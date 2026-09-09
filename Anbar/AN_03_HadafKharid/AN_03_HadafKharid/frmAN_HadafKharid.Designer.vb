<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAN_HadafKharid
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmAN_HadafKharid))
        Dim GridEXTitr_Layout_0 As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim GridEXTitr_Layout_1 As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim GridEXTitr_Layout_2 As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim GridEXTitr_Layout_3 As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim GridEXTitr_Layout_4 As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim GridEXTitr_Layout_5 As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Me.grbGrid = New System.Windows.Forms.GroupBox()
        Me.GridEXTitr = New Janus.Windows.GridEX.GridEX()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.btnExit = New System.Windows.Forms.Button()
        Me.btnRemove = New System.Windows.Forms.Button()
        Me.btnAddNew = New System.Windows.Forms.Button()
        Me.lblEditHadaf = New System.Windows.Forms.Label()
        Me.grbGrid.SuspendLayout()
        CType(Me.GridEXTitr, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'grbGrid
        '
        Me.grbGrid.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grbGrid.Controls.Add(Me.GridEXTitr)
        Me.grbGrid.Location = New System.Drawing.Point(0, 0)
        Me.grbGrid.Name = "grbGrid"
        Me.grbGrid.Size = New System.Drawing.Size(969, 434)
        Me.grbGrid.TabIndex = 0
        Me.grbGrid.TabStop = False
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
        Me.GridEXTitr.Size = New System.Drawing.Size(963, 414)
        Me.GridEXTitr.TabIndex = 1
        Me.GridEXTitr.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed
        Me.GridEXTitr.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.lblEditHadaf)
        Me.GroupBox1.Controls.Add(Me.btnExit)
        Me.GroupBox1.Controls.Add(Me.btnRemove)
        Me.GroupBox1.Controls.Add(Me.btnAddNew)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.GroupBox1.Location = New System.Drawing.Point(0, 429)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(969, 59)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = False
        '
        'btnExit
        '
        Me.btnExit.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnExit.Location = New System.Drawing.Point(12, 20)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(92, 29)
        Me.btnExit.TabIndex = 2
        Me.btnExit.Text = "خــــــــــروج"
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'btnRemove
        '
        Me.btnRemove.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnRemove.Location = New System.Drawing.Point(776, 20)
        Me.btnRemove.Name = "btnRemove"
        Me.btnRemove.Size = New System.Drawing.Size(92, 29)
        Me.btnRemove.TabIndex = 1
        Me.btnRemove.Text = "حــذف هـدف"
        Me.btnRemove.UseVisualStyleBackColor = True
        '
        'btnAddNew
        '
        Me.btnAddNew.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnAddNew.Location = New System.Drawing.Point(871, 20)
        Me.btnAddNew.Name = "btnAddNew"
        Me.btnAddNew.Size = New System.Drawing.Size(92, 29)
        Me.btnAddNew.TabIndex = 0
        Me.btnAddNew.Text = "هـدف جـدید"
        Me.btnAddNew.UseVisualStyleBackColor = True
        '
        'lblEditHadaf
        '
        Me.lblEditHadaf.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblEditHadaf.AutoSize = True
        Me.lblEditHadaf.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblEditHadaf.Location = New System.Drawing.Point(353, 28)
        Me.lblEditHadaf.Name = "lblEditHadaf"
        Me.lblEditHadaf.Size = New System.Drawing.Size(257, 13)
        Me.lblEditHadaf.TabIndex = 3
        Me.lblEditHadaf.Text = "جهت ویرایش ، بر روی هدف مورد نظر دبل کلیک نمایید ."
        Me.lblEditHadaf.Visible = False
        '
        'frmAN_HadafKharid
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(969, 488)
        Me.Controls.Add(Me.grbGrid)
        Me.Controls.Add(Me.GroupBox1)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Name = "frmAN_HadafKharid"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.RightToLeftLayout = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = " هــدف خریــد"
        Me.TopMost = True
        Me.grbGrid.ResumeLayout(False)
        CType(Me.GridEXTitr, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents grbGrid As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents GridEXTitr As Janus.Windows.GridEX.GridEX
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents btnRemove As System.Windows.Forms.Button
    Friend WithEvents btnAddNew As System.Windows.Forms.Button
    Friend WithEvents lblEditHadaf As System.Windows.Forms.Label

End Class
