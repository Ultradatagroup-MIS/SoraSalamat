<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class KalaEshantion
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(KalaEshantion))
        Dim GridEXKalaEshantion_Layout_0 As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim GridEXKalaEshantion_Layout_1 As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim GridEXKalaEshantion_Layout_2 As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim GridEXKalaEshantion_Layout_3 As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim GridEXKalaEshantion_Layout_4 As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim GridEXKalaEshantion_Layout_5 As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Me.GridEXKalaEshantion = New Janus.Windows.GridEX.GridEX()
        CType(Me.GridEXKalaEshantion, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GridEXKalaEshantion
        '
        Me.GridEXKalaEshantion.AllowChildTableGroups = True
        Me.GridEXKalaEshantion.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.GridEXKalaEshantion.AllowDrop = True
        Me.GridEXKalaEshantion.AllowRemoveColumns = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.GridEXKalaEshantion.BorderStyle = Janus.Windows.GridEX.BorderStyle.RaisedLight3D
        Me.GridEXKalaEshantion.BuiltInTextsData = resources.GetString("GridEXKalaEshantion.BuiltInTextsData")
        Me.GridEXKalaEshantion.ColumnSetNavigation = Janus.Windows.GridEX.ColumnSetNavigation.ColumnSet
        Me.GridEXKalaEshantion.DynamicFiltering = True
        Me.GridEXKalaEshantion.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        Me.GridEXKalaEshantion.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.GridEXKalaEshantion.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges
        Me.GridEXKalaEshantion.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridEXKalaEshantion.GroupByBoxVisible = False
        Me.GridEXKalaEshantion.GroupRowVisualStyle = Janus.Windows.GridEX.GroupRowVisualStyle.Outlook2003
        Me.GridEXKalaEshantion.GroupTotals = Janus.Windows.GridEX.GroupTotals.Always
        Me.GridEXKalaEshantion.KeepRowSettings = True
        GridEXKalaEshantion_Layout_0.Key = "Layout1"
        GridEXKalaEshantion_Layout_1.Key = "Layout2"
        GridEXKalaEshantion_Layout_2.Key = "Layout3"
        GridEXKalaEshantion_Layout_3.Key = "Layout4"
        GridEXKalaEshantion_Layout_4.Key = "Layout5"
        GridEXKalaEshantion_Layout_5.Key = "Layout6"
        Me.GridEXKalaEshantion.Layouts.AddRange(New Janus.Windows.GridEX.GridEXLayout() {GridEXKalaEshantion_Layout_0, GridEXKalaEshantion_Layout_1, GridEXKalaEshantion_Layout_2, GridEXKalaEshantion_Layout_3, GridEXKalaEshantion_Layout_4, GridEXKalaEshantion_Layout_5})
        Me.GridEXKalaEshantion.Location = New System.Drawing.Point(-1, 0)
        Me.GridEXKalaEshantion.Name = "GridEXKalaEshantion"
        Me.GridEXKalaEshantion.Office2007ColorScheme = Janus.Windows.GridEX.Office2007ColorScheme.Blue
        Me.GridEXKalaEshantion.RecordNavigator = True
        Me.GridEXKalaEshantion.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.GridEXKalaEshantion.SelectionMode = Janus.Windows.GridEX.SelectionMode.MultipleSelection
        Me.GridEXKalaEshantion.Size = New System.Drawing.Size(687, 372)
        Me.GridEXKalaEshantion.TabIndex = 102
        Me.GridEXKalaEshantion.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed
        Me.GridEXKalaEshantion.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'KalaEshantion
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(686, 372)
        Me.Controls.Add(Me.GridEXKalaEshantion)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "KalaEshantion"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.RightToLeftLayout = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "کالاهای اشانتیون"
        CType(Me.GridEXKalaEshantion, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents GridEXKalaEshantion As Janus.Windows.GridEX.GridEX
End Class
