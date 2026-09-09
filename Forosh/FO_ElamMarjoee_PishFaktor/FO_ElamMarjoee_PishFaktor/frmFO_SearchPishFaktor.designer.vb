<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmFO_SearchPishFaktor
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmFO_SearchPishFaktor))
        Dim GridEXPishFaktor_Layout_0 As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim GridEXPishFaktor_Layout_1 As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim GridEXPishFaktor_Layout_2 As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim GridEXPishFaktor_Layout_3 As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim GridEXPishFaktor_Layout_4 As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim GridEXPishFaktor_Layout_5 As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.GridEXPishFaktor = New Janus.Windows.GridEX.GridEX()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.btnExit = New System.Windows.Forms.Button()
        Me.btnTaeed = New System.Windows.Forms.Button()
        Me.GroupBox1.SuspendLayout()
        CType(Me.GridEXPishFaktor, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.GridEXPishFaktor)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(1013, 321)
        Me.GroupBox1.TabIndex = 2
        Me.GroupBox1.TabStop = False
        '
        'GridEXPishFaktor
        '
        Me.GridEXPishFaktor.AllowChildTableGroups = True
        Me.GridEXPishFaktor.AllowRemoveColumns = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.GridEXPishFaktor.BorderStyle = Janus.Windows.GridEX.BorderStyle.RaisedLight3D
        Me.GridEXPishFaktor.BuiltInTextsData = resources.GetString("GridEXPishFaktor.BuiltInTextsData")
        Me.GridEXPishFaktor.ColumnSetNavigation = Janus.Windows.GridEX.ColumnSetNavigation.ColumnSet
        Me.GridEXPishFaktor.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GridEXPishFaktor.DynamicFiltering = True
        Me.GridEXPishFaktor.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        Me.GridEXPishFaktor.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.GridEXPishFaktor.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges
        Me.GridEXPishFaktor.Font = New System.Drawing.Font("Tahoma", 9.75!)
        Me.GridEXPishFaktor.GroupByBoxVisible = False
        Me.GridEXPishFaktor.GroupRowVisualStyle = Janus.Windows.GridEX.GroupRowVisualStyle.Outlook2003
        Me.GridEXPishFaktor.GroupTotals = Janus.Windows.GridEX.GroupTotals.Always
        Me.GridEXPishFaktor.KeepRowSettings = True
        GridEXPishFaktor_Layout_0.Key = "Layout1"
        GridEXPishFaktor_Layout_1.Key = "Layout2"
        GridEXPishFaktor_Layout_2.Key = "Layout3"
        GridEXPishFaktor_Layout_3.Key = "Layout4"
        GridEXPishFaktor_Layout_4.Key = "Layout5"
        GridEXPishFaktor_Layout_5.Key = "Layout6"
        Me.GridEXPishFaktor.Layouts.AddRange(New Janus.Windows.GridEX.GridEXLayout() {GridEXPishFaktor_Layout_0, GridEXPishFaktor_Layout_1, GridEXPishFaktor_Layout_2, GridEXPishFaktor_Layout_3, GridEXPishFaktor_Layout_4, GridEXPishFaktor_Layout_5})
        Me.GridEXPishFaktor.Location = New System.Drawing.Point(3, 17)
        Me.GridEXPishFaktor.Name = "GridEXPishFaktor"
        Me.GridEXPishFaktor.Office2007ColorScheme = Janus.Windows.GridEX.Office2007ColorScheme.Blue
        Me.GridEXPishFaktor.RecordNavigator = True
        Me.GridEXPishFaktor.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.GridEXPishFaktor.SelectionMode = Janus.Windows.GridEX.SelectionMode.MultipleSelection
        Me.GridEXPishFaktor.Size = New System.Drawing.Size(1007, 301)
        Me.GridEXPishFaktor.TabIndex = 100
        Me.GridEXPishFaktor.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed
        Me.GridEXPishFaktor.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.btnExit)
        Me.GroupBox2.Controls.Add(Me.btnTaeed)
        Me.GroupBox2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.GroupBox2.Location = New System.Drawing.Point(0, 318)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(1013, 53)
        Me.GroupBox2.TabIndex = 3
        Me.GroupBox2.TabStop = False
        '
        'btnExit
        '
        Me.btnExit.Location = New System.Drawing.Point(398, 14)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(107, 33)
        Me.btnExit.TabIndex = 3
        Me.btnExit.Text = "خـــــروج"
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'btnTaeed
        '
        Me.btnTaeed.Location = New System.Drawing.Point(507, 14)
        Me.btnTaeed.Name = "btnTaeed"
        Me.btnTaeed.Size = New System.Drawing.Size(107, 33)
        Me.btnTaeed.TabIndex = 2
        Me.btnTaeed.Text = "تاییــــد"
        Me.btnTaeed.UseVisualStyleBackColor = True
        '
        'frmFO_SearchPishFaktor
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1013, 371)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.GroupBox2)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.Name = "frmFO_SearchPishFaktor"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.RightToLeftLayout = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "جستجـو پیش فاکتــــور"
        Me.GroupBox1.ResumeLayout(False)
        CType(Me.GridEXPishFaktor, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents GridEXPishFaktor As Janus.Windows.GridEX.GridEX
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents btnTaeed As System.Windows.Forms.Button
End Class
