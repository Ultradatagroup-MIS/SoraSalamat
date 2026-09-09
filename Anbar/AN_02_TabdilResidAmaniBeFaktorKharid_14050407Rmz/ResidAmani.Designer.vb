<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ResidAmani
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ResidAmani))
        Dim GridEXResid_Layout_0 As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim GridEXResid_Layout_1 As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim GridEXResid_Layout_2 As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim GridEXResid_Layout_3 As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim GridEXResid_Layout_4 As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim GridEXResid_Layout_5 As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.GridEXResid = New Janus.Windows.GridEX.GridEX()
        Me.GroupBox1.SuspendLayout()
        CType(Me.GridEXResid, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.GridEXResid)
        Me.GroupBox1.Location = New System.Drawing.Point(4, -3)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(615, 329)
        Me.GroupBox1.TabIndex = 103
        Me.GroupBox1.TabStop = False
        '
        'GridEXResid
        '
        Me.GridEXResid.AllowChildTableGroups = True
        Me.GridEXResid.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.GridEXResid.AllowDrop = True
        Me.GridEXResid.AllowRemoveColumns = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.GridEXResid.BorderStyle = Janus.Windows.GridEX.BorderStyle.RaisedLight3D
        Me.GridEXResid.BuiltInTextsData = resources.GetString("GridEXResid.BuiltInTextsData")
        Me.GridEXResid.ColumnSetNavigation = Janus.Windows.GridEX.ColumnSetNavigation.ColumnSet
        Me.GridEXResid.DynamicFiltering = True
        Me.GridEXResid.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        Me.GridEXResid.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.GridEXResid.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges
        Me.GridEXResid.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridEXResid.GroupByBoxVisible = False
        Me.GridEXResid.GroupRowVisualStyle = Janus.Windows.GridEX.GroupRowVisualStyle.Outlook2003
        Me.GridEXResid.GroupTotals = Janus.Windows.GridEX.GroupTotals.Always
        Me.GridEXResid.KeepRowSettings = True
        GridEXResid_Layout_0.Key = "Layout1"
        GridEXResid_Layout_1.Key = "Layout2"
        GridEXResid_Layout_2.Key = "Layout3"
        GridEXResid_Layout_3.Key = "Layout4"
        GridEXResid_Layout_4.Key = "Layout5"
        GridEXResid_Layout_5.Key = "Layout6"
        Me.GridEXResid.Layouts.AddRange(New Janus.Windows.GridEX.GridEXLayout() {GridEXResid_Layout_0, GridEXResid_Layout_1, GridEXResid_Layout_2, GridEXResid_Layout_3, GridEXResid_Layout_4, GridEXResid_Layout_5})
        Me.GridEXResid.Location = New System.Drawing.Point(6, 13)
        Me.GridEXResid.Name = "GridEXResid"
        Me.GridEXResid.Office2007ColorScheme = Janus.Windows.GridEX.Office2007ColorScheme.Blue
        Me.GridEXResid.RecordNavigator = True
        Me.GridEXResid.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.GridEXResid.SelectionMode = Janus.Windows.GridEX.SelectionMode.MultipleSelection
        Me.GridEXResid.Size = New System.Drawing.Size(603, 310)
        Me.GridEXResid.TabIndex = 102
        Me.GridEXResid.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed
        Me.GridEXResid.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'ResidAmani
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(622, 330)
        Me.Controls.Add(Me.GroupBox1)
        Me.Name = "ResidAmani"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.RightToLeftLayout = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "رسید امانی"
        Me.GroupBox1.ResumeLayout(False)
        CType(Me.GridEXResid, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents GridEXResid As Janus.Windows.GridEX.GridEX
End Class
