<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class HazfPardakhti
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(HazfPardakhti))
        Dim GridExSandogh_Layout_0 As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout
        Dim GridExSandogh_Layout_1 As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout
        Dim GridExSandogh_Layout_2 As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout
        Dim GridExSandogh_Layout_3 As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout
        Dim GridExSandogh_Layout_4 As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout
        Dim GridExSandogh_Layout_5 As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout
        Me.GridExSandogh = New Janus.Windows.GridEX.GridEX
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.btnExit = New System.Windows.Forms.Button
        Me.btnTaeed = New System.Windows.Forms.Button
        CType(Me.GridExSandogh, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'GridExSandogh
        '
        Me.GridExSandogh.AllowChildTableGroups = True
        Me.GridExSandogh.AllowColumnDrag = False
        Me.GridExSandogh.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.GridExSandogh.AllowDrop = True
        Me.GridExSandogh.AllowRemoveColumns = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.GridExSandogh.BorderStyle = Janus.Windows.GridEX.BorderStyle.RaisedLight3D
        Me.GridExSandogh.BuiltInTextsData = resources.GetString("GridExSandogh.BuiltInTextsData")
        Me.GridExSandogh.ColumnSetNavigation = Janus.Windows.GridEX.ColumnSetNavigation.ColumnSet
        Me.GridExSandogh.DynamicFiltering = True
        Me.GridExSandogh.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        Me.GridExSandogh.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.GridExSandogh.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges
        Me.GridExSandogh.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridExSandogh.GroupByBoxVisible = False
        Me.GridExSandogh.GroupRowVisualStyle = Janus.Windows.GridEX.GroupRowVisualStyle.Outlook2003
        Me.GridExSandogh.GroupTotals = Janus.Windows.GridEX.GroupTotals.Always
        Me.GridExSandogh.KeepRowSettings = True
        GridExSandogh_Layout_0.Key = "Layout1"
        GridExSandogh_Layout_1.Key = "Layout2"
        GridExSandogh_Layout_2.Key = "Layout3"
        GridExSandogh_Layout_3.Key = "Layout4"
        GridExSandogh_Layout_4.Key = "Layout5"
        GridExSandogh_Layout_5.Key = "Layout6"
        Me.GridExSandogh.Layouts.AddRange(New Janus.Windows.GridEX.GridEXLayout() {GridExSandogh_Layout_0, GridExSandogh_Layout_1, GridExSandogh_Layout_2, GridExSandogh_Layout_3, GridExSandogh_Layout_4, GridExSandogh_Layout_5})
        Me.GridExSandogh.Location = New System.Drawing.Point(4, 3)
        Me.GridExSandogh.Name = "GridExSandogh"
        Me.GridExSandogh.Office2007ColorScheme = Janus.Windows.GridEX.Office2007ColorScheme.Blue
        Me.GridExSandogh.RecordNavigator = True
        Me.GridExSandogh.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.GridExSandogh.SelectionMode = Janus.Windows.GridEX.SelectionMode.MultipleSelection
        Me.GridExSandogh.Size = New System.Drawing.Size(941, 389)
        Me.GridExSandogh.TabIndex = 103
        Me.GridExSandogh.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed
        Me.GridExSandogh.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.btnExit)
        Me.GroupBox1.Controls.Add(Me.btnTaeed)
        Me.GroupBox1.Location = New System.Drawing.Point(3, 396)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(942, 54)
        Me.GroupBox1.TabIndex = 104
        Me.GroupBox1.TabStop = False
        '
        'btnExit
        '
        Me.btnExit.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnExit.Location = New System.Drawing.Point(395, 16)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(76, 23)
        Me.btnExit.TabIndex = 7
        Me.btnExit.Text = "خروج"
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'btnTaeed
        '
        Me.btnTaeed.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnTaeed.Location = New System.Drawing.Point(471, 16)
        Me.btnTaeed.Name = "btnTaeed"
        Me.btnTaeed.Size = New System.Drawing.Size(76, 23)
        Me.btnTaeed.TabIndex = 6
        Me.btnTaeed.Text = "حذف"
        Me.btnTaeed.UseVisualStyleBackColor = True
        '
        'HazfPardakhti
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(948, 453)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.GridExSandogh)
        Me.Name = "HazfPardakhti"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Text = "حذف پرداختی ابتدای دوره"
        CType(Me.GridExSandogh, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GridExSandogh As Janus.Windows.GridEX.GridEX
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents btnTaeed As System.Windows.Forms.Button
End Class
