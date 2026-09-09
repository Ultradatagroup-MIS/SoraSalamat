<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmFO_InsertKala
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmFO_InsertKala))
        Dim GridEXInsertKala_Layout_0 As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim GridEXInsertKala_Layout_1 As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim GridEXInsertKala_Layout_2 As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim GridEXInsertKala_Layout_3 As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim GridEXInsertKala_Layout_4 As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim GridEXInsertKala_Layout_5 As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.GridEXInsertKala = New Janus.Windows.GridEX.GridEX()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.btnExit = New System.Windows.Forms.Button()
        Me.btnEnter = New System.Windows.Forms.Button()
        Me.GroupBox1.SuspendLayout()
        CType(Me.GridEXInsertKala, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.GridEXInsertKala)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(1046, 328)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "ورود اطلاعات"
        '
        'GridEXInsertKala
        '
        Me.GridEXInsertKala.AllowChildTableGroups = True
        Me.GridEXInsertKala.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.GridEXInsertKala.AllowDrop = True
        Me.GridEXInsertKala.AllowRemoveColumns = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.GridEXInsertKala.BorderStyle = Janus.Windows.GridEX.BorderStyle.RaisedLight3D
        Me.GridEXInsertKala.BuiltInTextsData = resources.GetString("GridEXInsertKala.BuiltInTextsData")
        Me.GridEXInsertKala.ColumnSetNavigation = Janus.Windows.GridEX.ColumnSetNavigation.ColumnSet
        Me.GridEXInsertKala.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GridEXInsertKala.DynamicFiltering = True
        Me.GridEXInsertKala.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.GridEXInsertKala.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges
        Me.GridEXInsertKala.Font = New System.Drawing.Font("Tahoma", 9.75!)
        Me.GridEXInsertKala.GroupByBoxVisible = False
        Me.GridEXInsertKala.GroupRowVisualStyle = Janus.Windows.GridEX.GroupRowVisualStyle.Outlook2003
        Me.GridEXInsertKala.GroupTotals = Janus.Windows.GridEX.GroupTotals.Always
        Me.GridEXInsertKala.KeepRowSettings = True
        GridEXInsertKala_Layout_0.Key = "Layout1"
        GridEXInsertKala_Layout_1.Key = "Layout2"
        GridEXInsertKala_Layout_2.Key = "Layout3"
        GridEXInsertKala_Layout_3.Key = "Layout4"
        GridEXInsertKala_Layout_4.Key = "Layout5"
        GridEXInsertKala_Layout_5.Key = "Layout6"
        Me.GridEXInsertKala.Layouts.AddRange(New Janus.Windows.GridEX.GridEXLayout() {GridEXInsertKala_Layout_0, GridEXInsertKala_Layout_1, GridEXInsertKala_Layout_2, GridEXInsertKala_Layout_3, GridEXInsertKala_Layout_4, GridEXInsertKala_Layout_5})
        Me.GridEXInsertKala.Location = New System.Drawing.Point(3, 17)
        Me.GridEXInsertKala.Name = "GridEXInsertKala"
        Me.GridEXInsertKala.Office2007ColorScheme = Janus.Windows.GridEX.Office2007ColorScheme.Blue
        Me.GridEXInsertKala.RecordNavigator = True
        Me.GridEXInsertKala.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.GridEXInsertKala.SelectionMode = Janus.Windows.GridEX.SelectionMode.MultipleSelection
        Me.GridEXInsertKala.Size = New System.Drawing.Size(1040, 308)
        Me.GridEXInsertKala.TabIndex = 99
        Me.GridEXInsertKala.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed
        Me.GridEXInsertKala.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.btnExit)
        Me.GroupBox2.Controls.Add(Me.btnEnter)
        Me.GroupBox2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.GroupBox2.Location = New System.Drawing.Point(0, 328)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(1046, 55)
        Me.GroupBox2.TabIndex = 1
        Me.GroupBox2.TabStop = False
        '
        'btnExit
        '
        Me.btnExit.Location = New System.Drawing.Point(413, 16)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(107, 33)
        Me.btnExit.TabIndex = 1
        Me.btnExit.Text = "خروج"
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'btnEnter
        '
        Me.btnEnter.Location = New System.Drawing.Point(526, 16)
        Me.btnEnter.Name = "btnEnter"
        Me.btnEnter.Size = New System.Drawing.Size(107, 33)
        Me.btnEnter.TabIndex = 0
        Me.btnEnter.Text = "ثبت کالا"
        Me.btnEnter.UseVisualStyleBackColor = True
        '
        'frmFO_InsertKala
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1046, 383)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.Name = "frmFO_InsertKala"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.RightToLeftLayout = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "ورود کالا"
        Me.TopMost = True
        Me.GroupBox1.ResumeLayout(False)
        CType(Me.GridEXInsertKala, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents btnEnter As System.Windows.Forms.Button
    Friend WithEvents GridEXInsertKala As Janus.Windows.GridEX.GridEX
End Class
