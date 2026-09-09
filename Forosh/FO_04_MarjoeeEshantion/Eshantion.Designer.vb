<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Eshantion
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Eshantion))
        Dim GridEXEshantion_Layout_0 As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim GridEXEshantion_Layout_1 As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim GridEXEshantion_Layout_2 As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim GridEXEshantion_Layout_3 As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim GridEXEshantion_Layout_4 As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim GridEXEshantion_Layout_5 As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Me.GridEXEshantion = New Janus.Windows.GridEX.GridEX()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.BackgroundWorker1 = New System.ComponentModel.BackgroundWorker()
        CType(Me.GridEXEshantion, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'GridEXEshantion
        '
        Me.GridEXEshantion.AllowChildTableGroups = True
        Me.GridEXEshantion.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.GridEXEshantion.AllowDrop = True
        Me.GridEXEshantion.AllowRemoveColumns = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.GridEXEshantion.BorderStyle = Janus.Windows.GridEX.BorderStyle.RaisedLight3D
        Me.GridEXEshantion.BuiltInTextsData = resources.GetString("GridEXEshantion.BuiltInTextsData")
        Me.GridEXEshantion.ColumnSetNavigation = Janus.Windows.GridEX.ColumnSetNavigation.ColumnSet
        Me.GridEXEshantion.DynamicFiltering = True
        Me.GridEXEshantion.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        Me.GridEXEshantion.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.GridEXEshantion.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges
        Me.GridEXEshantion.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridEXEshantion.GroupByBoxVisible = False
        Me.GridEXEshantion.GroupRowVisualStyle = Janus.Windows.GridEX.GroupRowVisualStyle.Outlook2003
        Me.GridEXEshantion.GroupTotals = Janus.Windows.GridEX.GroupTotals.Always
        Me.GridEXEshantion.KeepRowSettings = True
        GridEXEshantion_Layout_0.Key = "Layout1"
        GridEXEshantion_Layout_1.Key = "Layout2"
        GridEXEshantion_Layout_2.Key = "Layout3"
        GridEXEshantion_Layout_3.Key = "Layout4"
        GridEXEshantion_Layout_4.Key = "Layout5"
        GridEXEshantion_Layout_5.Key = "Layout6"
        Me.GridEXEshantion.Layouts.AddRange(New Janus.Windows.GridEX.GridEXLayout() {GridEXEshantion_Layout_0, GridEXEshantion_Layout_1, GridEXEshantion_Layout_2, GridEXEshantion_Layout_3, GridEXEshantion_Layout_4, GridEXEshantion_Layout_5})
        Me.GridEXEshantion.Location = New System.Drawing.Point(0, 0)
        Me.GridEXEshantion.Name = "GridEXEshantion"
        Me.GridEXEshantion.Office2007ColorScheme = Janus.Windows.GridEX.Office2007ColorScheme.Blue
        Me.GridEXEshantion.RecordNavigator = True
        Me.GridEXEshantion.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.GridEXEshantion.SelectionMode = Janus.Windows.GridEX.SelectionMode.MultipleSelection
        Me.GridEXEshantion.Size = New System.Drawing.Size(805, 373)
        Me.GridEXEshantion.TabIndex = 101
        Me.GridEXEshantion.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed
        Me.GridEXEshantion.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.GroupBox1.Controls.Add(Me.GridEXEshantion)
        Me.GroupBox1.Location = New System.Drawing.Point(0, -1)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(805, 373)
        Me.GroupBox1.TabIndex = 102
        Me.GroupBox1.TabStop = False
        '
        'Eshantion
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(805, 371)
        Me.Controls.Add(Me.GroupBox1)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Eshantion"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.RightToLeftLayout = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "لیست اشانتیون ها"
        CType(Me.GridEXEshantion, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents GridEXEshantion As Janus.Windows.GridEX.GridEX
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents BackgroundWorker1 As System.ComponentModel.BackgroundWorker
End Class
