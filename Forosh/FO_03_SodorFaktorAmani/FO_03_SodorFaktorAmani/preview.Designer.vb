<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class preview
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(preview))
        Dim GridEX_Layout_0 As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim GridEX_Layout_1 As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim GridEX_Layout_2 As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim GridEX_Layout_3 As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim GridEX_Layout_4 As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim GridEX_Layout_5 As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.btnPrint = New System.Windows.Forms.Button()
        Me.btnExit = New System.Windows.Forms.Button()
        Me.GridEX = New Janus.Windows.GridEX.GridEX()
        Me.GroupBox1.SuspendLayout()
        CType(Me.GridEX, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.btnPrint)
        Me.GroupBox1.Controls.Add(Me.btnExit)
        Me.GroupBox1.Location = New System.Drawing.Point(0, 370)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(804, 44)
        Me.GroupBox1.TabIndex = 102
        Me.GroupBox1.TabStop = False
        '
        'btnPrint
        '
        Me.btnPrint.AccessibleDescription = "Refresh Button"
        Me.btnPrint.AccessibleName = "Refresh Button"
        Me.btnPrint.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnPrint.Location = New System.Drawing.Point(563, 9)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(84, 27)
        Me.btnPrint.TabIndex = 7
        Me.btnPrint.Text = "چـــاپ"
        '
        'btnExit
        '
        Me.btnExit.AccessibleDescription = "Search Button"
        Me.btnExit.AccessibleName = "Search Button"
        Me.btnExit.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnExit.Location = New System.Drawing.Point(157, 9)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(84, 27)
        Me.btnExit.TabIndex = 8
        Me.btnExit.Text = "&خروج"
        '
        'GridEX
        '
        Me.GridEX.AllowChildTableGroups = True
        Me.GridEX.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.GridEX.AllowDrop = True
        Me.GridEX.AllowRemoveColumns = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.GridEX.BorderStyle = Janus.Windows.GridEX.BorderStyle.RaisedLight3D
        Me.GridEX.BuiltInTextsData = resources.GetString("GridEX.BuiltInTextsData")
        Me.GridEX.ColumnSetNavigation = Janus.Windows.GridEX.ColumnSetNavigation.ColumnSet
        Me.GridEX.DynamicFiltering = True
        Me.GridEX.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        Me.GridEX.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.GridEX.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges
        Me.GridEX.Font = New System.Drawing.Font("Tahoma", 9.75!)
        Me.GridEX.GroupByBoxVisible = False
        Me.GridEX.GroupRowVisualStyle = Janus.Windows.GridEX.GroupRowVisualStyle.Outlook2003
        Me.GridEX.GroupTotals = Janus.Windows.GridEX.GroupTotals.Always
        Me.GridEX.KeepRowSettings = True
        GridEX_Layout_0.Key = "Layout1"
        GridEX_Layout_1.Key = "Layout2"
        GridEX_Layout_2.Key = "Layout3"
        GridEX_Layout_3.Key = "Layout4"
        GridEX_Layout_4.Key = "Layout5"
        GridEX_Layout_5.Key = "Layout6"
        Me.GridEX.Layouts.AddRange(New Janus.Windows.GridEX.GridEXLayout() {GridEX_Layout_0, GridEX_Layout_1, GridEX_Layout_2, GridEX_Layout_3, GridEX_Layout_4, GridEX_Layout_5})
        Me.GridEX.Location = New System.Drawing.Point(0, 3)
        Me.GridEX.Name = "GridEX"
        Me.GridEX.Office2007ColorScheme = Janus.Windows.GridEX.Office2007ColorScheme.Blue
        Me.GridEX.RecordNavigator = True
        Me.GridEX.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.GridEX.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.GridEX.SelectionMode = Janus.Windows.GridEX.SelectionMode.MultipleSelection
        Me.GridEX.Size = New System.Drawing.Size(804, 361)
        Me.GridEX.TabIndex = 101
        Me.GridEX.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed
        Me.GridEX.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'preview
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(806, 426)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.GridEX)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!)
        Me.Name = "preview"
        Me.Text = "پیش نمایش"
        Me.GroupBox1.ResumeLayout(False)
        CType(Me.GridEX, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents btnPrint As System.Windows.Forms.Button
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents GridEX As Janus.Windows.GridEX.GridEX
End Class
