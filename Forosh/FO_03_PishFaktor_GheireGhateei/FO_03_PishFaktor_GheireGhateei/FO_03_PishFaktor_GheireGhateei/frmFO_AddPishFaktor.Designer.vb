<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmFO_AddPishFaktor
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmFO_AddPishFaktor))
        Dim GridEXPishFaktor_Layout_0 As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim GridEXPishFaktor_Layout_1 As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim GridEXPishFaktor_Layout_2 As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim GridEXPishFaktor_Layout_3 As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim GridEXPishFaktor_Layout_4 As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim GridEXPishFaktor_Layout_5 As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.btnSearch = New System.Windows.Forms.Button()
        Me.mskTaTarikh = New System.Windows.Forms.MaskedTextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.cmbAnbar = New System.Windows.Forms.ComboBox()
        Me.lblAnbar = New System.Windows.Forms.Label()
        Me.mskAzTarikh = New System.Windows.Forms.MaskedTextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.cmbForoshandehS = New System.Windows.Forms.ComboBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.btnExit = New System.Windows.Forms.Button()
        Me.btnAdd = New System.Windows.Forms.Button()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.GridEXPishFaktor = New Janus.Windows.GridEX.GridEX()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        CType(Me.GridEXPishFaktor, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.btnSearch)
        Me.GroupBox1.Controls.Add(Me.mskTaTarikh)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.cmbAnbar)
        Me.GroupBox1.Controls.Add(Me.lblAnbar)
        Me.GroupBox1.Controls.Add(Me.mskAzTarikh)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.cmbForoshandehS)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(741, 50)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = " جستجو "
        '
        'btnSearch
        '
        Me.btnSearch.Image = CType(resources.GetObject("btnSearch.Image"), System.Drawing.Image)
        Me.btnSearch.Location = New System.Drawing.Point(6, 14)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(41, 30)
        Me.btnSearch.TabIndex = 22
        '
        'mskTaTarikh
        '
        Me.mskTaTarikh.AllowPromptAsInput = False
        Me.mskTaTarikh.Location = New System.Drawing.Point(54, 18)
        Me.mskTaTarikh.Mask = "####/##/##"
        Me.mskTaTarikh.Name = "mskTaTarikh"
        Me.mskTaTarikh.PromptChar = Global.Microsoft.VisualBasic.ChrW(32)
        Me.mskTaTarikh.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.mskTaTarikh.Size = New System.Drawing.Size(68, 21)
        Me.mskTaTarikh.TabIndex = 21
        Me.mskTaTarikh.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(124, 21)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(44, 13)
        Me.Label5.TabIndex = 20
        Me.Label5.Text = "تا تاريخ :"
        '
        'cmbAnbar
        '
        Me.cmbAnbar.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbAnbar.Location = New System.Drawing.Point(294, 18)
        Me.cmbAnbar.MaxDropDownItems = 20
        Me.cmbAnbar.Name = "cmbAnbar"
        Me.cmbAnbar.Size = New System.Drawing.Size(134, 21)
        Me.cmbAnbar.TabIndex = 19
        '
        'lblAnbar
        '
        Me.lblAnbar.AutoSize = True
        Me.lblAnbar.Location = New System.Drawing.Point(429, 21)
        Me.lblAnbar.Name = "lblAnbar"
        Me.lblAnbar.Size = New System.Drawing.Size(52, 13)
        Me.lblAnbar.TabIndex = 18
        Me.lblAnbar.Text = "انبـــــــار :"
        '
        'mskAzTarikh
        '
        Me.mskAzTarikh.AllowPromptAsInput = False
        Me.mskAzTarikh.Location = New System.Drawing.Point(174, 18)
        Me.mskAzTarikh.Mask = "####/##/##"
        Me.mskAzTarikh.Name = "mskAzTarikh"
        Me.mskAzTarikh.PromptChar = Global.Microsoft.VisualBasic.ChrW(32)
        Me.mskAzTarikh.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.mskAzTarikh.Size = New System.Drawing.Size(68, 21)
        Me.mskAzTarikh.TabIndex = 15
        Me.mskAzTarikh.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(244, 21)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(45, 13)
        Me.Label4.TabIndex = 14
        Me.Label4.Text = "از تاريخ :"
        '
        'cmbForoshandehS
        '
        Me.cmbForoshandehS.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbForoshandehS.Location = New System.Drawing.Point(487, 18)
        Me.cmbForoshandehS.MaxDropDownItems = 20
        Me.cmbForoshandehS.Name = "cmbForoshandehS"
        Me.cmbForoshandehS.Size = New System.Drawing.Size(195, 21)
        Me.cmbForoshandehS.TabIndex = 5
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(683, 21)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(55, 13)
        Me.Label7.TabIndex = 4
        Me.Label7.Text = "فروشنده :"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.btnExit)
        Me.GroupBox2.Controls.Add(Me.btnAdd)
        Me.GroupBox2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.GroupBox2.Location = New System.Drawing.Point(0, 539)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(741, 43)
        Me.GroupBox2.TabIndex = 1
        Me.GroupBox2.TabStop = False
        '
        'btnExit
        '
        Me.btnExit.Location = New System.Drawing.Point(292, 13)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(78, 25)
        Me.btnExit.TabIndex = 25
        Me.btnExit.Text = "خروج"
        '
        'btnAdd
        '
        Me.btnAdd.Location = New System.Drawing.Point(371, 13)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(78, 25)
        Me.btnAdd.TabIndex = 24
        Me.btnAdd.Text = "افــزودن"
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.GridEXPishFaktor)
        Me.GroupBox3.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupBox3.Location = New System.Drawing.Point(0, 50)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(741, 492)
        Me.GroupBox3.TabIndex = 2
        Me.GroupBox3.TabStop = False
        '
        'GridEXPishFaktor
        '
        Me.GridEXPishFaktor.AllowChildTableGroups = True
        Me.GridEXPishFaktor.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.GridEXPishFaktor.AllowDrop = True
        Me.GridEXPishFaktor.AllowRemoveColumns = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.GridEXPishFaktor.BorderStyle = Janus.Windows.GridEX.BorderStyle.RaisedLight3D
        Me.GridEXPishFaktor.BuiltInTextsData = resources.GetString("GridEXPishFaktor.BuiltInTextsData")
        Me.GridEXPishFaktor.ColumnSetNavigation = Janus.Windows.GridEX.ColumnSetNavigation.ColumnSet
        Me.GridEXPishFaktor.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GridEXPishFaktor.DynamicFiltering = True
        Me.GridEXPishFaktor.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        Me.GridEXPishFaktor.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.GridEXPishFaktor.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges
        Me.GridEXPishFaktor.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridEXPishFaktor.GroupByBoxVisible = False
        Me.GridEXPishFaktor.GroupRowVisualStyle = Janus.Windows.GridEX.GroupRowVisualStyle.Outlook2003
        Me.GridEXPishFaktor.GroupTotals = Janus.Windows.GridEX.GroupTotals.Always
        Me.GridEXPishFaktor.KeepRowSettings = True
        GridEXPishFaktor_Layout_0.Key = "Layout1"
        GridEXPishFaktor_Layout_1.Key = "Layout2"
        GridEXPishFaktor_Layout_2.Key = "Layout3"
        GridEXPishFaktor_Layout_3.Key = "Layout4"
        GridEXPishFaktor_Layout_4.Key = "Layout5"
        GridEXPishFaktor_Layout_5.Key = "Layout6"
        Me.GridEXPishFaktor.Layouts.AddRange(New Janus.Windows.GridEX.GridEXLayout() {GridEXPishFaktor_Layout_0, GridEXPishFaktor_Layout_1, GridEXPishFaktor_Layout_2, GridEXPishFaktor_Layout_3, GridEXPishFaktor_Layout_4, GridEXPishFaktor_Layout_5})
        Me.GridEXPishFaktor.Location = New System.Drawing.Point(3, 17)
        Me.GridEXPishFaktor.Name = "GridEXPishFaktor"
        Me.GridEXPishFaktor.Office2007ColorScheme = Janus.Windows.GridEX.Office2007ColorScheme.Blue
        Me.GridEXPishFaktor.RecordNavigator = True
        Me.GridEXPishFaktor.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.GridEXPishFaktor.SelectionMode = Janus.Windows.GridEX.SelectionMode.MultipleSelection
        Me.GridEXPishFaktor.Size = New System.Drawing.Size(735, 472)
        Me.GridEXPishFaktor.TabIndex = 102
        Me.GridEXPishFaktor.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed
        Me.GridEXPishFaktor.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'frmFO_AddPishFaktor
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(741, 582)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.Name = "frmFO_AddPishFaktor"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.RightToLeftLayout = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "افزودن پیش فاکتور"
        Me.TopMost = True
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox3.ResumeLayout(False)
        CType(Me.GridEXPishFaktor, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents btnAdd As System.Windows.Forms.Button
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents cmbForoshandehS As System.Windows.Forms.ComboBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents mskAzTarikh As System.Windows.Forms.MaskedTextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents cmbAnbar As System.Windows.Forms.ComboBox
    Friend WithEvents lblAnbar As System.Windows.Forms.Label
    Friend WithEvents mskTaTarikh As System.Windows.Forms.MaskedTextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents GridEXPishFaktor As Janus.Windows.GridEX.GridEX
    Friend WithEvents btnSearch As System.Windows.Forms.Button
End Class
