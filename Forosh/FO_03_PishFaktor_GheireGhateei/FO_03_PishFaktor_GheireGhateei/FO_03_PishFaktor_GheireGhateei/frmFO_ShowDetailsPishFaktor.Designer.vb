<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmFO_ShowDetailsPishFaktor
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmFO_ShowDetailsPishFaktor))
        Dim GridEXSatr_Layout_0 As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim GridEXSatr_Layout_1 As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim GridEXSatr_Layout_2 As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim GridEXSatr_Layout_3 As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim GridEXSatr_Layout_4 As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim GridEXSatr_Layout_5 As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.GridEXSatr = New Janus.Windows.GridEX.GridEX()
        Me.btnExit = New System.Windows.Forms.Button()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        CType(Me.GridEXSatr, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.GridEXSatr)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(937, 418)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.btnExit)
        Me.GroupBox2.Location = New System.Drawing.Point(0, 418)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(937, 53)
        Me.GroupBox2.TabIndex = 1
        Me.GroupBox2.TabStop = False
        '
        'GridEXSatr
        '
        Me.GridEXSatr.AllowChildTableGroups = True
        Me.GridEXSatr.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.GridEXSatr.AllowDrop = True
        Me.GridEXSatr.AllowRemoveColumns = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.GridEXSatr.BorderStyle = Janus.Windows.GridEX.BorderStyle.RaisedLight3D
        Me.GridEXSatr.BuiltInTextsData = resources.GetString("GridEXSatr.BuiltInTextsData")
        Me.GridEXSatr.ColumnSetNavigation = Janus.Windows.GridEX.ColumnSetNavigation.ColumnSet
        Me.GridEXSatr.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GridEXSatr.DynamicFiltering = True
        Me.GridEXSatr.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        Me.GridEXSatr.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.GridEXSatr.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges
        Me.GridEXSatr.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridEXSatr.GroupByBoxVisible = False
        Me.GridEXSatr.GroupRowVisualStyle = Janus.Windows.GridEX.GroupRowVisualStyle.Outlook2003
        Me.GridEXSatr.GroupTotals = Janus.Windows.GridEX.GroupTotals.Always
        Me.GridEXSatr.KeepRowSettings = True
        GridEXSatr_Layout_0.Key = "Layout1"
        GridEXSatr_Layout_1.Key = "Layout2"
        GridEXSatr_Layout_2.Key = "Layout3"
        GridEXSatr_Layout_3.Key = "Layout4"
        GridEXSatr_Layout_4.Key = "Layout5"
        GridEXSatr_Layout_5.Key = "Layout6"
        Me.GridEXSatr.Layouts.AddRange(New Janus.Windows.GridEX.GridEXLayout() {GridEXSatr_Layout_0, GridEXSatr_Layout_1, GridEXSatr_Layout_2, GridEXSatr_Layout_3, GridEXSatr_Layout_4, GridEXSatr_Layout_5})
        Me.GridEXSatr.Location = New System.Drawing.Point(3, 16)
        Me.GridEXSatr.Name = "GridEXSatr"
        Me.GridEXSatr.Office2007ColorScheme = Janus.Windows.GridEX.Office2007ColorScheme.Blue
        Me.GridEXSatr.RecordNavigator = True
        Me.GridEXSatr.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.GridEXSatr.SelectionMode = Janus.Windows.GridEX.SelectionMode.MultipleSelection
        Me.GridEXSatr.Size = New System.Drawing.Size(931, 399)
        Me.GridEXSatr.TabIndex = 102
        Me.GridEXSatr.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed
        Me.GridEXSatr.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'btnExit
        '
        Me.btnExit.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnExit.Location = New System.Drawing.Point(431, 16)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(74, 26)
        Me.btnExit.TabIndex = 20
        Me.btnExit.Text = "خــــروج"
        Me.btnExit.Visible = False
        '
        'frmFO_ShowDetailsPishFaktor
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(937, 468)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.MaximizeBox = False
        Me.Name = "frmFO_ShowDetailsPishFaktor"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.RightToLeftLayout = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "جزئیــــــات پیش فاکتــــــور"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        CType(Me.GridEXSatr, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents GridEXSatr As Janus.Windows.GridEX.GridEX
    Friend WithEvents btnExit As System.Windows.Forms.Button
End Class
