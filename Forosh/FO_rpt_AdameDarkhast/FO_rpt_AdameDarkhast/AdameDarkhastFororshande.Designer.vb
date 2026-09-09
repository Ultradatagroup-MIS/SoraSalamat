<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DP_rpt_Enteghalat
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(DP_rpt_Enteghalat))
        Dim GridEX1_Layout_0 As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim GridEX1_Layout_1 As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim GridEX1_Layout_2 As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim GridEX1_Layout_3 As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim GridEX1_Layout_4 As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim GridEX1_Layout_5 As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.mskAzTarikh = New System.Windows.Forms.MaskedTextBox()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.btnReport = New System.Windows.Forms.Button()
        Me.cmbElat = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.mskTaTarikh = New System.Windows.Forms.MaskedTextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.txtCodeMoshtary = New System.Windows.Forms.TextBox()
        Me.lblNameMoshtary = New System.Windows.Forms.Label()
        Me.cmbNameForoshandeh = New System.Windows.Forms.ComboBox()
        Me.GridEX1 = New Janus.Windows.GridEX.GridEX()
        Me.btnGozaresh = New System.Windows.Forms.Button()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.GroupBox1.SuspendLayout()
        CType(Me.GridEX1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(708, 22)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(45, 13)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "از تاریخ :"
        '
        'mskAzTarikh
        '
        Me.mskAzTarikh.AllowPromptAsInput = False
        Me.mskAzTarikh.CutCopyMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals
        Me.mskAzTarikh.Location = New System.Drawing.Point(624, 19)
        Me.mskAzTarikh.Mask = "####/##/##"
        Me.mskAzTarikh.Name = "mskAzTarikh"
        Me.mskAzTarikh.PromptChar = Global.Microsoft.VisualBasic.ChrW(32)
        Me.mskAzTarikh.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.mskAzTarikh.Size = New System.Drawing.Size(78, 21)
        Me.mskAzTarikh.TabIndex = 2
        Me.mskAzTarikh.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals
        '
        'btnCancel
        '
        Me.btnCancel.Location = New System.Drawing.Point(305, 13)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(79, 27)
        Me.btnCancel.TabIndex = 10
        Me.btnCancel.Text = "خروج"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'btnReport
        '
        Me.btnReport.Location = New System.Drawing.Point(9, 46)
        Me.btnReport.Name = "btnReport"
        Me.btnReport.Size = New System.Drawing.Size(79, 26)
        Me.btnReport.TabIndex = 9
        Me.btnReport.Text = "گزارش"
        Me.btnReport.UseVisualStyleBackColor = True
        '
        'cmbElat
        '
        Me.cmbElat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbElat.FormattingEnabled = True
        Me.cmbElat.Location = New System.Drawing.Point(108, 49)
        Me.cmbElat.Name = "cmbElat"
        Me.cmbElat.Size = New System.Drawing.Size(283, 21)
        Me.cmbElat.TabIndex = 7
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(682, 52)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(71, 13)
        Me.Label3.TabIndex = 8
        Me.Label3.Text = "نام فروشنده :"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(395, 51)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(35, 13)
        Me.Label4.TabIndex = 9
        Me.Label4.Text = "علت :"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.mskTaTarikh)
        Me.GroupBox1.Controls.Add(Me.Label9)
        Me.GroupBox1.Controls.Add(Me.btnReport)
        Me.GroupBox1.Controls.Add(Me.txtCodeMoshtary)
        Me.GroupBox1.Controls.Add(Me.lblNameMoshtary)
        Me.GroupBox1.Controls.Add(Me.cmbNameForoshandeh)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.mskAzTarikh)
        Me.GroupBox1.Controls.Add(Me.cmbElat)
        Me.GroupBox1.Location = New System.Drawing.Point(3, -5)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(762, 82)
        Me.GroupBox1.TabIndex = 10
        Me.GroupBox1.TabStop = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(564, 22)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(44, 13)
        Me.Label1.TabIndex = 15
        Me.Label1.Text = "تا تاریخ :"
        '
        'mskTaTarikh
        '
        Me.mskTaTarikh.AllowPromptAsInput = False
        Me.mskTaTarikh.CutCopyMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals
        Me.mskTaTarikh.Location = New System.Drawing.Point(483, 19)
        Me.mskTaTarikh.Mask = "####/##/##"
        Me.mskTaTarikh.Name = "mskTaTarikh"
        Me.mskTaTarikh.PromptChar = Global.Microsoft.VisualBasic.ChrW(32)
        Me.mskTaTarikh.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.mskTaTarikh.Size = New System.Drawing.Size(77, 21)
        Me.mskTaTarikh.TabIndex = 16
        Me.mskTaTarikh.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(395, 23)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(51, 13)
        Me.Label9.TabIndex = 12
        Me.Label9.Text = "مشتری :"
        '
        'txtCodeMoshtary
        '
        Me.txtCodeMoshtary.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtCodeMoshtary.Location = New System.Drawing.Point(310, 19)
        Me.txtCodeMoshtary.MaxLength = 15
        Me.txtCodeMoshtary.Name = "txtCodeMoshtary"
        Me.txtCodeMoshtary.Size = New System.Drawing.Size(81, 21)
        Me.txtCodeMoshtary.TabIndex = 13
        Me.txtCodeMoshtary.Text = " "
        '
        'lblNameMoshtary
        '
        Me.lblNameMoshtary.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblNameMoshtary.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNameMoshtary.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblNameMoshtary.Location = New System.Drawing.Point(9, 19)
        Me.lblNameMoshtary.Name = "lblNameMoshtary"
        Me.lblNameMoshtary.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.lblNameMoshtary.Size = New System.Drawing.Size(298, 21)
        Me.lblNameMoshtary.TabIndex = 14
        Me.lblNameMoshtary.Text = "  "
        Me.lblNameMoshtary.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cmbNameForoshandeh
        '
        Me.cmbNameForoshandeh.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbNameForoshandeh.FormattingEnabled = True
        Me.cmbNameForoshandeh.Location = New System.Drawing.Point(435, 48)
        Me.cmbNameForoshandeh.Name = "cmbNameForoshandeh"
        Me.cmbNameForoshandeh.Size = New System.Drawing.Size(246, 21)
        Me.cmbNameForoshandeh.TabIndex = 10
        '
        'GridEX1
        '
        Me.GridEX1.AllowChildTableGroups = True
        Me.GridEX1.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.GridEX1.AllowDrop = True
        Me.GridEX1.AllowRemoveColumns = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.GridEX1.BorderStyle = Janus.Windows.GridEX.BorderStyle.RaisedLight3D
        Me.GridEX1.BuiltInTextsData = resources.GetString("GridEX1.BuiltInTextsData")
        Me.GridEX1.ColumnSetNavigation = Janus.Windows.GridEX.ColumnSetNavigation.ColumnSet
        Me.GridEX1.DynamicFiltering = True
        Me.GridEX1.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        Me.GridEX1.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.GridEX1.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges
        Me.GridEX1.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridEX1.GroupByBoxVisible = False
        Me.GridEX1.GroupRowVisualStyle = Janus.Windows.GridEX.GroupRowVisualStyle.Outlook2003
        Me.GridEX1.GroupTotals = Janus.Windows.GridEX.GroupTotals.Always
        Me.GridEX1.KeepRowSettings = True
        GridEX1_Layout_0.Key = "Layout1"
        GridEX1_Layout_1.Key = "Layout2"
        GridEX1_Layout_2.Key = "Layout3"
        GridEX1_Layout_3.Key = "Layout4"
        GridEX1_Layout_4.Key = "Layout5"
        GridEX1_Layout_5.Key = "Layout6"
        Me.GridEX1.Layouts.AddRange(New Janus.Windows.GridEX.GridEXLayout() {GridEX1_Layout_0, GridEX1_Layout_1, GridEX1_Layout_2, GridEX1_Layout_3, GridEX1_Layout_4, GridEX1_Layout_5})
        Me.GridEX1.Location = New System.Drawing.Point(3, 83)
        Me.GridEX1.Name = "GridEX1"
        Me.GridEX1.Office2007ColorScheme = Janus.Windows.GridEX.Office2007ColorScheme.Blue
        Me.GridEX1.RecordNavigator = True
        Me.GridEX1.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.GridEX1.SelectionMode = Janus.Windows.GridEX.SelectionMode.MultipleSelection
        Me.GridEX1.Size = New System.Drawing.Size(762, 282)
        Me.GridEX1.TabIndex = 99
        Me.GridEX1.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed
        Me.GridEX1.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'btnGozaresh
        '
        Me.btnGozaresh.Location = New System.Drawing.Point(385, 13)
        Me.btnGozaresh.Name = "btnGozaresh"
        Me.btnGozaresh.Size = New System.Drawing.Size(79, 27)
        Me.btnGozaresh.TabIndex = 100
        Me.btnGozaresh.Text = "چاپ"
        Me.btnGozaresh.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.btnGozaresh)
        Me.GroupBox2.Controls.Add(Me.btnCancel)
        Me.GroupBox2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.GroupBox2.Location = New System.Drawing.Point(0, 366)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(768, 46)
        Me.GroupBox2.TabIndex = 101
        Me.GroupBox2.TabStop = False
        '
        'DP_rpt_Enteghalat
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(768, 412)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GridEX1)
        Me.Controls.Add(Me.GroupBox1)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "DP_rpt_Enteghalat"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "گزارش عدم درخواست فروشنده"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.GridEX1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents mskAzTarikh As System.Windows.Forms.MaskedTextBox
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnReport As System.Windows.Forms.Button
    Friend WithEvents cmbElat As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents cmbNameForoshandeh As System.Windows.Forms.ComboBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents txtCodeMoshtary As System.Windows.Forms.TextBox
    Friend WithEvents lblNameMoshtary As System.Windows.Forms.Label
    Friend WithEvents GridEX1 As Janus.Windows.GridEX.GridEX
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents mskTaTarikh As System.Windows.Forms.MaskedTextBox
    Friend WithEvents btnGozaresh As System.Windows.Forms.Button
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox

End Class
