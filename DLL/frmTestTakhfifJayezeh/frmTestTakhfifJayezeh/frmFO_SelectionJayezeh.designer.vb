<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmFO_SelectionJayezeh
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmFO_SelectionJayezeh))
        Dim GridEXKala_Layout_0 As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim GridEXKala_Layout_1 As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim GridEXKala_Layout_2 As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim GridEXKala_Layout_3 As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim GridEXKala_Layout_4 As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim GridEXKala_Layout_5 As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.txtTedaJayezeh = New System.Windows.Forms.TextBox()
        Me.lblTedadJayezeh = New System.Windows.Forms.Label()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.GridEXKala = New Janus.Windows.GridEX.GridEX()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.btnTaeed = New System.Windows.Forms.Button()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        CType(Me.GridEXKala, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox3.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.txtTedaJayezeh)
        Me.GroupBox1.Controls.Add(Me.lblTedadJayezeh)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(772, 48)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        '
        'txtTedaJayezeh
        '
        Me.txtTedaJayezeh.Enabled = False
        Me.txtTedaJayezeh.Location = New System.Drawing.Point(509, 18)
        Me.txtTedaJayezeh.Name = "txtTedaJayezeh"
        Me.txtTedaJayezeh.Size = New System.Drawing.Size(100, 21)
        Me.txtTedaJayezeh.TabIndex = 1
        Me.txtTedaJayezeh.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'lblTedadJayezeh
        '
        Me.lblTedadJayezeh.AutoSize = True
        Me.lblTedadJayezeh.Location = New System.Drawing.Point(615, 21)
        Me.lblTedadJayezeh.Name = "lblTedadJayezeh"
        Me.lblTedadJayezeh.Size = New System.Drawing.Size(145, 13)
        Me.lblTedadJayezeh.TabIndex = 0
        Me.lblTedadJayezeh.Text = "تعـــداد جایـــزه قـابل تخصیص :"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.GridEXKala)
        Me.GroupBox2.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupBox2.Location = New System.Drawing.Point(0, 48)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(772, 401)
        Me.GroupBox2.TabIndex = 1
        Me.GroupBox2.TabStop = False
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
        Me.GridEXKala.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GridEXKala.DynamicFiltering = True
        Me.GridEXKala.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        Me.GridEXKala.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.GridEXKala.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges
        Me.GridEXKala.Font = New System.Drawing.Font("Tahoma", 9.75!)
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
        Me.GridEXKala.Location = New System.Drawing.Point(3, 17)
        Me.GridEXKala.Name = "GridEXKala"
        Me.GridEXKala.Office2007ColorScheme = Janus.Windows.GridEX.Office2007ColorScheme.Blue
        Me.GridEXKala.RecordNavigator = True
        Me.GridEXKala.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.GridEXKala.SelectionMode = Janus.Windows.GridEX.SelectionMode.MultipleSelection
        Me.GridEXKala.Size = New System.Drawing.Size(766, 381)
        Me.GridEXKala.TabIndex = 100
        Me.GridEXKala.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed
        Me.GridEXKala.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.btnTaeed)
        Me.GroupBox3.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.GroupBox3.Location = New System.Drawing.Point(0, 445)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(772, 46)
        Me.GroupBox3.TabIndex = 2
        Me.GroupBox3.TabStop = False
        '
        'btnTaeed
        '
        Me.btnTaeed.Location = New System.Drawing.Point(349, 14)
        Me.btnTaeed.Name = "btnTaeed"
        Me.btnTaeed.Size = New System.Drawing.Size(75, 26)
        Me.btnTaeed.TabIndex = 0
        Me.btnTaeed.Text = "تاییـــد"
        Me.btnTaeed.UseVisualStyleBackColor = True
        '
        'frmFO_SelectionJayezeh
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(772, 491)
        Me.ControlBox = False
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.GroupBox1)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.Name = "frmFO_SelectionJayezeh"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.RightToLeftLayout = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "انتخــاب جایــزه"
        Me.TopMost = True
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        CType(Me.GridEXKala, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox3.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents txtTedaJayezeh As System.Windows.Forms.TextBox
    Friend WithEvents lblTedadJayezeh As System.Windows.Forms.Label
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents btnTaeed As System.Windows.Forms.Button
    Friend WithEvents GridEXKala As Janus.Windows.GridEX.GridEX
End Class
