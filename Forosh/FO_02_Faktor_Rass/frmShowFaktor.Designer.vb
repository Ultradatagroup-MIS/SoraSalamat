<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmShowFaktor
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmShowFaktor))
        Dim GridEXFaktor_Layout_0 As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim GridEXFaktor_Layout_1 As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim GridEXFaktor_Layout_2 As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim GridEXFaktor_Layout_3 As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim GridEXFaktor_Layout_4 As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim GridEXFaktor_Layout_5 As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Me.GridEXFaktor = New Janus.Windows.GridEX.GridEX()
        CType(Me.GridEXFaktor, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
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
        Me.GridEXFaktor.DynamicFiltering = True
        Me.GridEXFaktor.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        Me.GridEXFaktor.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.GridEXFaktor.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges
        Me.GridEXFaktor.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
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
        Me.GridEXFaktor.Location = New System.Drawing.Point(13, 13)
        Me.GridEXFaktor.Margin = New System.Windows.Forms.Padding(4)
        Me.GridEXFaktor.Name = "GridEXFaktor"
        Me.GridEXFaktor.Office2007ColorScheme = Janus.Windows.GridEX.Office2007ColorScheme.Blue
        Me.GridEXFaktor.RecordNavigator = True
        Me.GridEXFaktor.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.GridEXFaktor.SelectionMode = Janus.Windows.GridEX.SelectionMode.MultipleSelection
        Me.GridEXFaktor.Size = New System.Drawing.Size(774, 424)
        Me.GridEXFaktor.TabIndex = 103
        Me.GridEXFaktor.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed
        Me.GridEXFaktor.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'frmShowFaktor
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.GridEXFaktor)
        Me.Name = "frmShowFaktor"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Text = "نمایش فاکتور"
        CType(Me.GridEXFaktor, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents GridEXFaktor As Janus.Windows.GridEX.GridEX
End Class
