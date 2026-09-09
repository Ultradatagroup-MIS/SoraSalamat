<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class KalaResid
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(KalaResid))
        Dim GridEXKala_Layout_0 As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim GridEXKala_Layout_1 As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim GridEXKala_Layout_2 As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim GridEXKala_Layout_3 As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim GridEXKala_Layout_4 As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim GridEXKala_Layout_5 As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.GridEXKala = New Janus.Windows.GridEX.GridEX()
        Me.GroupBox1.SuspendLayout()
        CType(Me.GridEXKala, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.GridEXKala)
        Me.GroupBox1.Location = New System.Drawing.Point(3, -1)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(583, 273)
        Me.GroupBox1.TabIndex = 102
        Me.GroupBox1.TabStop = False
        '
        'GridEXKala
        '
        Me.GridEXKala.AllowChildTableGroups = True
        Me.GridEXKala.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.GridEXKala.AllowDrop = True
        Me.GridEXKala.AllowRemoveColumns = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.GridEXKala.BorderStyle = Janus.Windows.GridEX.BorderStyle.RaisedLight3D
        Me.GridEXKala.BuiltInTextsData = resources.GetString("GridEXKala.BuiltInTextsData")
        Me.GridEXKala.ColumnSetNavigation = Janus.Windows.GridEX.ColumnSetNavigation.ColumnSet
        Me.GridEXKala.DynamicFiltering = True
        Me.GridEXKala.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        Me.GridEXKala.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.GridEXKala.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges
        Me.GridEXKala.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridEXKala.GroupByBoxVisible = False
        Me.GridEXKala.GroupRowVisualStyle = Janus.Windows.GridEX.GroupRowVisualStyle.Outlook2003
        Me.GridEXKala.GroupTotals = Janus.Windows.GridEX.GroupTotals.Always
        Me.GridEXKala.KeepRowSettings = True
        GridEXKala_Layout_0.Key = "Layout1"
        GridEXKala_Layout_1.Key = "Layout2"
        GridEXKala_Layout_2.Key = "Layout3"
        GridEXKala_Layout_3.Key = "Layout4"
        GridEXKala_Layout_4.Key = "Layout5"
        GridEXKala_Layout_5.Key = "Layout6"
        Me.GridEXKala.Layouts.AddRange(New Janus.Windows.GridEX.GridEXLayout() {GridEXKala_Layout_0, GridEXKala_Layout_1, GridEXKala_Layout_2, GridEXKala_Layout_3, GridEXKala_Layout_4, GridEXKala_Layout_5})
        Me.GridEXKala.Location = New System.Drawing.Point(9, 13)
        Me.GridEXKala.Name = "GridEXKala"
        Me.GridEXKala.Office2007ColorScheme = Janus.Windows.GridEX.Office2007ColorScheme.Blue
        Me.GridEXKala.RecordNavigator = True
        Me.GridEXKala.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.GridEXKala.SelectionMode = Janus.Windows.GridEX.SelectionMode.MultipleSelection
        Me.GridEXKala.Size = New System.Drawing.Size(568, 254)
        Me.GridEXKala.TabIndex = 102
        Me.GridEXKala.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed
        Me.GridEXKala.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'KalaResid
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(589, 276)
        Me.Controls.Add(Me.GroupBox1)
        Me.Name = "KalaResid"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.RightToLeftLayout = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "کالاهای رسید امانی"
        Me.GroupBox1.ResumeLayout(False)
        CType(Me.GridEXKala, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents GridEXKala As Janus.Windows.GridEX.GridEX
End Class
