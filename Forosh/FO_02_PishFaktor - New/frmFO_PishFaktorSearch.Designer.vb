<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmFO_PishFaktorSearch
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmFO_PishFaktorSearch))
        Dim GridEXList_Layout_0 As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim GridEXList_Layout_1 As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim GridEXList_Layout_2 As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim GridEXList_Layout_3 As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim GridEXList_Layout_4 As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim GridEXList_Layout_5 As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Me.GridEXList = New Janus.Windows.GridEX.GridEX()
        CType(Me.GridEXList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GridEXList
        '
        Me.GridEXList.AllowAddNew = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.GridEXList.AllowChildTableGroups = True
        Me.GridEXList.AllowColumnDrag = False
        Me.GridEXList.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.GridEXList.AllowDrop = True
        Me.GridEXList.AllowRemoveColumns = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.GridEXList.AlternatingRowFormatStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
        Me.GridEXList.BorderStyle = Janus.Windows.GridEX.BorderStyle.RaisedLight3D
        Me.GridEXList.BuiltInTextsData = resources.GetString("GridEXList.BuiltInTextsData")
        Me.GridEXList.CardCaptionFormatStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
        Me.GridEXList.CardColumnHeaderFormatStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
        Me.GridEXList.ColumnSetNavigation = Janus.Windows.GridEX.ColumnSetNavigation.ColumnSet
        Me.GridEXList.DefaultFilterRowComparison = Janus.Windows.GridEX.FilterConditionOperator.Contains
        Me.GridEXList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GridEXList.DynamicFiltering = True
        Me.GridEXList.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        Me.GridEXList.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.GridEXList.FilterRowFormatStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
        Me.GridEXList.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges
        Me.GridEXList.Font = New System.Drawing.Font("Tahoma", 9.75!)
        Me.GridEXList.GroupByBoxVisible = False
        Me.GridEXList.GroupRowVisualStyle = Janus.Windows.GridEX.GroupRowVisualStyle.Outlook2003
        Me.GridEXList.GroupTotals = Janus.Windows.GridEX.GroupTotals.Always
        Me.GridEXList.KeepRowSettings = True
        GridEXList_Layout_0.Key = "Layout1"
        GridEXList_Layout_1.Key = "Layout2"
        GridEXList_Layout_2.Key = "Layout3"
        GridEXList_Layout_3.Key = "Layout4"
        GridEXList_Layout_4.Key = "Layout5"
        GridEXList_Layout_5.Key = "Layout6"
        Me.GridEXList.Layouts.AddRange(New Janus.Windows.GridEX.GridEXLayout() {GridEXList_Layout_0, GridEXList_Layout_1, GridEXList_Layout_2, GridEXList_Layout_3, GridEXList_Layout_4, GridEXList_Layout_5})
        Me.GridEXList.Location = New System.Drawing.Point(0, 0)
        Me.GridEXList.Margin = New System.Windows.Forms.Padding(2)
        Me.GridEXList.Name = "GridEXList"
        Me.GridEXList.Office2007ColorScheme = Janus.Windows.GridEX.Office2007ColorScheme.Blue
        Me.GridEXList.RecordNavigator = True
        Me.GridEXList.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.GridEXList.Size = New System.Drawing.Size(800, 450)
        Me.GridEXList.TabIndex = 101
        Me.GridEXList.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed
        Me.GridEXList.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'frmFO_PishFaktorSearch
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.GridEXList)
        Me.Name = "frmFO_PishFaktorSearch"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Text = "انتخاب پیش فاکتور"
        CType(Me.GridEXList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents GridEXList As Janus.Windows.GridEX.GridEX
End Class
