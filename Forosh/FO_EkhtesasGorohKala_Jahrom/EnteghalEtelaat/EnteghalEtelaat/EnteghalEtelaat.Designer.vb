<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmEnteghalEtelaat
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmEnteghalEtelaat))
        Dim Grid_Layout_0 As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim Grid_Layout_1 As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim Grid_Layout_2 As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim Grid_Layout_3 As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim Grid_Layout_4 As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim Grid_Layout_5 As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Me.Grid = New Janus.Windows.GridEX.GridEX()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.mskTaTarikh = New System.Windows.Forms.MaskedTextBox()
        Me.mskAzTarikh = New System.Windows.Forms.MaskedTextBox()
        Me.btnEnteghal = New System.Windows.Forms.Button()
        Me.chlMarkazPakhsh = New System.Windows.Forms.CheckedListBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cmbNoeSearch = New System.Windows.Forms.ComboBox()
        CType(Me.Grid, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Grid
        '
        Me.Grid.AllowChildTableGroups = True
        Me.Grid.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.Grid.AllowDrop = True
        Me.Grid.AllowRemoveColumns = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.Grid.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Grid.BorderStyle = Janus.Windows.GridEX.BorderStyle.Flat
        Me.Grid.BuiltInTextsData = resources.GetString("Grid.BuiltInTextsData")
        Me.Grid.ColumnSetNavigation = Janus.Windows.GridEX.ColumnSetNavigation.ColumnSet
        Me.Grid.DynamicFiltering = True
        Me.Grid.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        Me.Grid.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.Grid.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges
        Me.Grid.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Grid.GroupByBoxVisible = False
        Me.Grid.GroupRowVisualStyle = Janus.Windows.GridEX.GroupRowVisualStyle.Outlook2003
        Me.Grid.GroupTotals = Janus.Windows.GridEX.GroupTotals.Always
        Me.Grid.KeepRowSettings = True
        Grid_Layout_0.Key = "Layout1"
        Grid_Layout_1.Key = "Layout2"
        Grid_Layout_2.Key = "Layout3"
        Grid_Layout_3.Key = "Layout4"
        Grid_Layout_4.Key = "Layout5"
        Grid_Layout_5.Key = "Layout6"
        Me.Grid.Layouts.AddRange(New Janus.Windows.GridEX.GridEXLayout() {Grid_Layout_0, Grid_Layout_1, Grid_Layout_2, Grid_Layout_3, Grid_Layout_4, Grid_Layout_5})
        Me.Grid.Location = New System.Drawing.Point(12, 54)
        Me.Grid.Name = "Grid"
        Me.Grid.Office2007ColorScheme = Janus.Windows.GridEX.Office2007ColorScheme.Blue
        Me.Grid.RecordNavigator = True
        Me.Grid.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.Grid.SelectionMode = Janus.Windows.GridEX.SelectionMode.MultipleSelection
        Me.Grid.Size = New System.Drawing.Size(596, 288)
        Me.Grid.TabIndex = 103
        Me.Grid.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed
        Me.Grid.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.mskTaTarikh)
        Me.GroupBox1.Controls.Add(Me.mskAzTarikh)
        Me.GroupBox1.Controls.Add(Me.btnEnteghal)
        Me.GroupBox1.Controls.Add(Me.chlMarkazPakhsh)
        Me.GroupBox1.Controls.Add(Me.Grid)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.cmbNoeSearch)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(620, 511)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = False
        '
        'mskTaTarikh
        '
        Me.mskTaTarikh.Location = New System.Drawing.Point(12, 20)
        Me.mskTaTarikh.Mask = "0000/00/00"
        Me.mskTaTarikh.Name = "mskTaTarikh"
        Me.mskTaTarikh.PromptChar = Global.Microsoft.VisualBasic.ChrW(32)
        Me.mskTaTarikh.Size = New System.Drawing.Size(100, 21)
        Me.mskTaTarikh.TabIndex = 107
        '
        'mskAzTarikh
        '
        Me.mskAzTarikh.Location = New System.Drawing.Point(169, 20)
        Me.mskAzTarikh.Mask = "0000/00/00"
        Me.mskAzTarikh.Name = "mskAzTarikh"
        Me.mskAzTarikh.PromptChar = Global.Microsoft.VisualBasic.ChrW(32)
        Me.mskAzTarikh.Size = New System.Drawing.Size(100, 21)
        Me.mskAzTarikh.TabIndex = 106
        '
        'btnEnteghal
        '
        Me.btnEnteghal.Font = New System.Drawing.Font("Tahoma", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.btnEnteghal.Location = New System.Drawing.Point(12, 373)
        Me.btnEnteghal.Name = "btnEnteghal"
        Me.btnEnteghal.Size = New System.Drawing.Size(100, 92)
        Me.btnEnteghal.TabIndex = 105
        Me.btnEnteghal.Text = "انتقال"
        Me.btnEnteghal.UseVisualStyleBackColor = True
        '
        'chlMarkazPakhsh
        '
        Me.chlMarkazPakhsh.FormattingEnabled = True
        Me.chlMarkazPakhsh.Location = New System.Drawing.Point(391, 356)
        Me.chlMarkazPakhsh.Name = "chlMarkazPakhsh"
        Me.chlMarkazPakhsh.Size = New System.Drawing.Size(217, 132)
        Me.chlMarkazPakhsh.TabIndex = 104
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(118, 23)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(44, 13)
        Me.Label2.TabIndex = 5
        Me.Label2.Text = "تا تاریخ :"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(275, 23)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(45, 13)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "از تاریخ :"
        '
        'cmbNoeSearch
        '
        Me.cmbNoeSearch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbNoeSearch.FormattingEnabled = True
        Me.cmbNoeSearch.Location = New System.Drawing.Point(391, 20)
        Me.cmbNoeSearch.Name = "cmbNoeSearch"
        Me.cmbNoeSearch.Size = New System.Drawing.Size(217, 21)
        Me.cmbNoeSearch.TabIndex = 1
        '
        'frmEnteghalEtelaat
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(620, 511)
        Me.Controls.Add(Me.GroupBox1)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.MaximizeBox = False
        Me.Name = "frmEnteghalEtelaat"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.RightToLeftLayout = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "انتقال اطلاعات"
        CType(Me.Grid, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Grid As Janus.Windows.GridEX.GridEX
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents mskTaTarikh As System.Windows.Forms.MaskedTextBox
    Friend WithEvents mskAzTarikh As System.Windows.Forms.MaskedTextBox
    Friend WithEvents btnEnteghal As System.Windows.Forms.Button
    Friend WithEvents chlMarkazPakhsh As System.Windows.Forms.CheckedListBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cmbNoeSearch As System.Windows.Forms.ComboBox

End Class
