<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmFO_VazeiatPishFaktor
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmFO_VazeiatPishFaktor))
        Dim GridEXPishFaktor_Layout_0 As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim GridEXPishFaktor_Layout_1 As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim GridEXPishFaktor_Layout_2 As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim GridEXPishFaktor_Layout_3 As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim GridEXPishFaktor_Layout_4 As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim GridEXPishFaktor_Layout_5 As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.GridEXPishFaktor = New Janus.Windows.GridEX.GridEX()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.txtShomarehPishFaktor = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnSearch = New System.Windows.Forms.Button()
        Me.lblAnbar = New System.Windows.Forms.Label()
        Me.cmbVazeiat = New System.Windows.Forms.ComboBox()
        Me.mskTaTarikh = New System.Windows.Forms.MaskedTextBox()
        Me.mskAzTarikh = New System.Windows.Forms.MaskedTextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtCodeKala = New System.Windows.Forms.TextBox()
        Me.lblRadif = New System.Windows.Forms.Label()
        Me.lblNameKala = New System.Windows.Forms.Label()
        Me.GroupBox1.SuspendLayout()
        CType(Me.GridEXPishFaktor, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.GridEXPishFaktor)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(762, 429)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
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
        Me.GridEXPishFaktor.Size = New System.Drawing.Size(756, 409)
        Me.GridEXPishFaktor.TabIndex = 102
        Me.GridEXPishFaktor.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed
        Me.GridEXPishFaktor.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.txtCodeKala)
        Me.GroupBox2.Controls.Add(Me.lblRadif)
        Me.GroupBox2.Controls.Add(Me.lblNameKala)
        Me.GroupBox2.Controls.Add(Me.txtShomarehPishFaktor)
        Me.GroupBox2.Controls.Add(Me.Label1)
        Me.GroupBox2.Controls.Add(Me.btnSearch)
        Me.GroupBox2.Controls.Add(Me.lblAnbar)
        Me.GroupBox2.Controls.Add(Me.cmbVazeiat)
        Me.GroupBox2.Controls.Add(Me.mskTaTarikh)
        Me.GroupBox2.Controls.Add(Me.mskAzTarikh)
        Me.GroupBox2.Controls.Add(Me.Label5)
        Me.GroupBox2.Controls.Add(Me.Label4)
        Me.GroupBox2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.GroupBox2.Location = New System.Drawing.Point(0, 425)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(762, 86)
        Me.GroupBox2.TabIndex = 1
        Me.GroupBox2.TabStop = False
        '
        'txtShomarehPishFaktor
        '
        Me.txtShomarehPishFaktor.Location = New System.Drawing.Point(64, 19)
        Me.txtShomarehPishFaktor.MaxLength = 5
        Me.txtShomarehPishFaktor.Name = "txtShomarehPishFaktor"
        Me.txtShomarehPishFaktor.Size = New System.Drawing.Size(71, 21)
        Me.txtShomarehPishFaktor.TabIndex = 26
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(136, 23)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(41, 13)
        Me.Label1.TabIndex = 25
        Me.Label1.Text = "شماره:"
        '
        'btnSearch
        '
        Me.btnSearch.Image = CType(resources.GetObject("btnSearch.Image"), System.Drawing.Image)
        Me.btnSearch.Location = New System.Drawing.Point(12, 15)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(46, 59)
        Me.btnSearch.TabIndex = 24
        '
        'lblAnbar
        '
        Me.lblAnbar.AutoSize = True
        Me.lblAnbar.Location = New System.Drawing.Point(706, 23)
        Me.lblAnbar.Name = "lblAnbar"
        Me.lblAnbar.Size = New System.Drawing.Size(48, 13)
        Me.lblAnbar.TabIndex = 23
        Me.lblAnbar.Text = "وضعیت :"
        '
        'cmbVazeiat
        '
        Me.cmbVazeiat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbVazeiat.Location = New System.Drawing.Point(410, 20)
        Me.cmbVazeiat.MaxDropDownItems = 20
        Me.cmbVazeiat.Name = "cmbVazeiat"
        Me.cmbVazeiat.Size = New System.Drawing.Size(295, 21)
        Me.cmbVazeiat.TabIndex = 22
        '
        'mskTaTarikh
        '
        Me.mskTaTarikh.AllowPromptAsInput = False
        Me.mskTaTarikh.Location = New System.Drawing.Point(185, 20)
        Me.mskTaTarikh.Mask = "####/##/##"
        Me.mskTaTarikh.Name = "mskTaTarikh"
        Me.mskTaTarikh.PromptChar = Global.Microsoft.VisualBasic.ChrW(32)
        Me.mskTaTarikh.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.mskTaTarikh.Size = New System.Drawing.Size(64, 21)
        Me.mskTaTarikh.TabIndex = 21
        Me.mskTaTarikh.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals
        '
        'mskAzTarikh
        '
        Me.mskAzTarikh.AllowPromptAsInput = False
        Me.mskAzTarikh.Location = New System.Drawing.Point(298, 20)
        Me.mskAzTarikh.Mask = "####/##/##"
        Me.mskAzTarikh.Name = "mskAzTarikh"
        Me.mskAzTarikh.PromptChar = Global.Microsoft.VisualBasic.ChrW(32)
        Me.mskAzTarikh.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.mskAzTarikh.Size = New System.Drawing.Size(64, 21)
        Me.mskAzTarikh.TabIndex = 19
        Me.mskAzTarikh.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(250, 23)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(44, 13)
        Me.Label5.TabIndex = 20
        Me.Label5.Text = "تا تاريخ :"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(363, 23)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(45, 13)
        Me.Label4.TabIndex = 18
        Me.Label4.Text = "از تاريخ :"
        '
        'txtCodeKala
        '
        Me.txtCodeKala.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtCodeKala.Location = New System.Drawing.Point(526, 54)
        Me.txtCodeKala.MaxLength = 15
        Me.txtCodeKala.Name = "txtCodeKala"
        Me.txtCodeKala.Size = New System.Drawing.Size(136, 21)
        Me.txtCodeKala.TabIndex = 28
        Me.txtCodeKala.Text = " "
        '
        'lblRadif
        '
        Me.lblRadif.AutoSize = True
        Me.lblRadif.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.lblRadif.Location = New System.Drawing.Point(664, 57)
        Me.lblRadif.Name = "lblRadif"
        Me.lblRadif.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.lblRadif.Size = New System.Drawing.Size(90, 13)
        Me.lblRadif.TabIndex = 27
        Me.lblRadif.Text = "نــــــــام کـــــــالا :"
        Me.lblRadif.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblNameKala
        '
        Me.lblNameKala.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblNameKala.Location = New System.Drawing.Point(64, 54)
        Me.lblNameKala.Name = "lblNameKala"
        Me.lblNameKala.Size = New System.Drawing.Size(456, 21)
        Me.lblNameKala.TabIndex = 29
        '
        'frmFO_VazeiatPishFaktor
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(762, 511)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.GroupBox2)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.Name = "frmFO_VazeiatPishFaktor"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.RightToLeftLayout = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "نمایش وضعیت پیش فاکتورها"
        Me.TopMost = True
        Me.GroupBox1.ResumeLayout(False)
        CType(Me.GridEXPishFaktor, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents GridEXPishFaktor As Janus.Windows.GridEX.GridEX
    Friend WithEvents txtShomarehPishFaktor As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnSearch As System.Windows.Forms.Button
    Friend WithEvents lblAnbar As System.Windows.Forms.Label
    Friend WithEvents cmbVazeiat As System.Windows.Forms.ComboBox
    Friend WithEvents mskTaTarikh As System.Windows.Forms.MaskedTextBox
    Friend WithEvents mskAzTarikh As System.Windows.Forms.MaskedTextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtCodeKala As System.Windows.Forms.TextBox
    Friend WithEvents lblRadif As System.Windows.Forms.Label
    Friend WithEvents lblNameKala As System.Windows.Forms.Label
End Class
