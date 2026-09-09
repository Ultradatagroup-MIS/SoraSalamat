<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmFO_InsertFaktor
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmFO_InsertFaktor))
        Dim GridEXFaktor_Layout_0 As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout
        Dim GridEXFaktor_Layout_1 As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout
        Dim GridEXFaktor_Layout_2 As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout
        Dim GridEXFaktor_Layout_3 As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout
        Dim GridEXFaktor_Layout_4 As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout
        Dim GridEXFaktor_Layout_5 As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.GridEXFaktor = New Janus.Windows.GridEX.GridEX
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.btnExit = New System.Windows.Forms.Button
        Me.btnEnter = New System.Windows.Forms.Button
        Me.GroupBox1.SuspendLayout()
        CType(Me.GridEXFaktor, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.GridEXFaktor)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(1070, 321)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        '
        'GridEXFaktor
        '
        Me.GridEXFaktor.AllowChildTableGroups = True
        Me.GridEXFaktor.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.GridEXFaktor.AllowDrop = True
        Me.GridEXFaktor.AllowRemoveColumns = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.GridEXFaktor.BorderStyle = Janus.Windows.GridEX.BorderStyle.RaisedLight3D
        Me.GridEXFaktor.BuiltInTextsData = resources.GetString("GridEXFaktor.BuiltInTextsData")
        Me.GridEXFaktor.ColumnSetNavigation = Janus.Windows.GridEX.ColumnSetNavigation.ColumnSet
        Me.GridEXFaktor.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GridEXFaktor.DynamicFiltering = True
        Me.GridEXFaktor.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.GridEXFaktor.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges
        Me.GridEXFaktor.Font = New System.Drawing.Font("Tahoma", 9.75!)
        Me.GridEXFaktor.GroupByBoxVisible = False
        Me.GridEXFaktor.GroupRowVisualStyle = Janus.Windows.GridEX.GroupRowVisualStyle.Outlook2003
        Me.GridEXFaktor.GroupTotals = Janus.Windows.GridEX.GroupTotals.Always
        Me.GridEXFaktor.KeepRowSettings = True
        GridEXFaktor_Layout_0.Key = "Layout1"
        GridEXFaktor_Layout_1.Key = "Layout2"
        GridEXFaktor_Layout_2.Key = "Layout3"
        GridEXFaktor_Layout_3.Key = "Layout4"
        GridEXFaktor_Layout_4.Key = "Layout5"
        GridEXFaktor_Layout_5.Key = "Layout6"
        Me.GridEXFaktor.Layouts.AddRange(New Janus.Windows.GridEX.GridEXLayout() {GridEXFaktor_Layout_0, GridEXFaktor_Layout_1, GridEXFaktor_Layout_2, GridEXFaktor_Layout_3, GridEXFaktor_Layout_4, GridEXFaktor_Layout_5})
        Me.GridEXFaktor.Location = New System.Drawing.Point(3, 17)
        Me.GridEXFaktor.Name = "GridEXFaktor"
        Me.GridEXFaktor.Office2007ColorScheme = Janus.Windows.GridEX.Office2007ColorScheme.Blue
        Me.GridEXFaktor.RecordNavigator = True
        Me.GridEXFaktor.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.GridEXFaktor.SelectionMode = Janus.Windows.GridEX.SelectionMode.MultipleSelection
        Me.GridEXFaktor.Size = New System.Drawing.Size(1064, 301)
        Me.GridEXFaktor.TabIndex = 100
        Me.GridEXFaktor.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed
        Me.GridEXFaktor.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.btnExit)
        Me.GroupBox2.Controls.Add(Me.btnEnter)
        Me.GroupBox2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.GroupBox2.Location = New System.Drawing.Point(0, 318)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(1070, 53)
        Me.GroupBox2.TabIndex = 1
        Me.GroupBox2.TabStop = False
        '
        'btnExit
        '
        Me.btnExit.Location = New System.Drawing.Point(427, 14)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(107, 33)
        Me.btnExit.TabIndex = 3
        Me.btnExit.Text = "Œ‹‹‹‹‹—ÊÃ"
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'btnEnter
        '
        Me.btnEnter.Location = New System.Drawing.Point(536, 14)
        Me.btnEnter.Name = "btnEnter"
        Me.btnEnter.Size = New System.Drawing.Size(107, 33)
        Me.btnEnter.TabIndex = 2
        Me.btnEnter.Text = "À»  ›«ò ‹‹Ê—Â«"
        Me.btnEnter.UseVisualStyleBackColor = True
        '
        'frmFO_InsertFaktor
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1070, 371)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.GroupBox2)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.Name = "frmFO_InsertFaktor"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.RightToLeftLayout = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Ê«—œ ‰„Êœ‰ ›«ò Ê—"
        Me.GroupBox1.ResumeLayout(False)
        CType(Me.GridEXFaktor, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents GridEXFaktor As Janus.Windows.GridEX.GridEX
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents btnEnter As System.Windows.Forms.Button
End Class
