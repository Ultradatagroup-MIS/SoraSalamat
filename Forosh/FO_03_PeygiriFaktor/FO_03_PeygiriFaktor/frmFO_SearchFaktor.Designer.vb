<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmFO_SearchFaktor
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmFO_SearchFaktor))
        Dim GridEXFaktor_Layout_0 As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout
        Dim GridEXFaktor_Layout_1 As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout
        Dim GridEXFaktor_Layout_2 As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout
        Dim GridEXFaktor_Layout_3 As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout
        Dim GridEXFaktor_Layout_4 As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout
        Dim GridEXFaktor_Layout_5 As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.GridEXFaktor = New Janus.Windows.GridEX.GridEX
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.cmbDorehS = New System.Windows.Forms.ComboBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.btnExit = New System.Windows.Forms.Button
        Me.btnTaeed = New System.Windows.Forms.Button
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
        Me.GroupBox1.Size = New System.Drawing.Size(942, 321)
        Me.GroupBox1.TabIndex = 2
        Me.GroupBox1.TabStop = False
        '
        'GridEXFaktor
        '
        Me.GridEXFaktor.AllowChildTableGroups = True
        Me.GridEXFaktor.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.GridEXFaktor.AllowRemoveColumns = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.GridEXFaktor.BorderStyle = Janus.Windows.GridEX.BorderStyle.RaisedLight3D
        Me.GridEXFaktor.BuiltInTextsData = resources.GetString("GridEXFaktor.BuiltInTextsData")
        Me.GridEXFaktor.ColumnSetNavigation = Janus.Windows.GridEX.ColumnSetNavigation.ColumnSet
        Me.GridEXFaktor.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GridEXFaktor.DynamicFiltering = True
        Me.GridEXFaktor.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
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
        Me.GridEXFaktor.Size = New System.Drawing.Size(936, 301)
        Me.GridEXFaktor.TabIndex = 100
        Me.GridEXFaktor.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed
        Me.GridEXFaktor.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.Label3)
        Me.GroupBox2.Controls.Add(Me.cmbDorehS)
        Me.GroupBox2.Controls.Add(Me.btnExit)
        Me.GroupBox2.Controls.Add(Me.btnTaeed)
        Me.GroupBox2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.GroupBox2.Location = New System.Drawing.Point(0, 318)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(942, 53)
        Me.GroupBox2.TabIndex = 3
        Me.GroupBox2.TabStop = False
        '
        'cmbDorehS
        '
        Me.cmbDorehS.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbDorehS.Location = New System.Drawing.Point(770, 21)
        Me.cmbDorehS.MaxDropDownItems = 20
        Me.cmbDorehS.Name = "cmbDorehS"
        Me.cmbDorehS.Size = New System.Drawing.Size(83, 21)
        Me.cmbDorehS.TabIndex = 121
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(859, 24)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(65, 13)
        Me.Label3.TabIndex = 122
        Me.Label3.Text = "دوره فاکتـور :"
        '
        'btnExit
        '
        Me.btnExit.Location = New System.Drawing.Point(363, 14)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(107, 33)
        Me.btnExit.TabIndex = 3
        Me.btnExit.Text = "خـــــروج"
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'btnTaeed
        '
        Me.btnTaeed.Location = New System.Drawing.Point(472, 14)
        Me.btnTaeed.Name = "btnTaeed"
        Me.btnTaeed.Size = New System.Drawing.Size(107, 33)
        Me.btnTaeed.TabIndex = 2
        Me.btnTaeed.Text = "تاییــــد"
        Me.btnTaeed.UseVisualStyleBackColor = True
        '
        'frmFO_SearchFaktor
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(942, 371)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.GroupBox2)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.Name = "frmFO_SearchFaktor"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.RightToLeftLayout = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "جستجـو فاکتــــور"
        Me.GroupBox1.ResumeLayout(False)
        CType(Me.GridEXFaktor, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents GridEXFaktor As Janus.Windows.GridEX.GridEX
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents btnTaeed As System.Windows.Forms.Button
    Friend WithEvents cmbDorehS As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
End Class
