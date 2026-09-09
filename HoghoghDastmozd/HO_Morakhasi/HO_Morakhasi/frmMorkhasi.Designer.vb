<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMorkhasi
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
        Dim GridEXTitr_Layout_0 As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim GridEXTitr_Layout_1 As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim GridEXTitr_Layout_2 As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim GridEXTitr_Layout_3 As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim GridEXTitr_Layout_4 As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim GridEXTitr_Layout_5 As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Me.BottunBox = New System.Windows.Forms.GroupBox()
        Me.btnPrintRooz = New System.Windows.Forms.Button()
        Me.btnPrintSaaty = New System.Windows.Forms.Button()
        Me.btnExit = New System.Windows.Forms.Button()
        Me.txtCancel = New System.Windows.Forms.Button()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.txtSaat = New System.Windows.Forms.TextBox()
        Me.lblPersonel = New System.Windows.Forms.Label()
        Me.cmbPersonel = New System.Windows.Forms.ComboBox()
        Me.lblSaat = New System.Windows.Forms.Label()
        Me.txtRooz = New System.Windows.Forms.TextBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.txtTozihat = New System.Windows.Forms.TextBox()
        Me.lblTozihat = New System.Windows.Forms.Label()
        Me.mskTaTarikh = New System.Windows.Forms.MaskedTextBox()
        Me.mskAzTarikh = New System.Windows.Forms.MaskedTextBox()
        Me.lblTaTArikh = New System.Windows.Forms.Label()
        Me.lblAzTarikh = New System.Windows.Forms.Label()
        Me.cmbMah = New System.Windows.Forms.ComboBox()
        Me.lblMah = New System.Windows.Forms.Label()
        Me.lblRooz = New System.Windows.Forms.Label()
        Me.GridEXTitr = New Janus.Windows.GridEX.GridEX()
        Me.BtnDelete = New System.Windows.Forms.Button()
        Me.BottunBox.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.GridEXTitr, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'BottunBox
        '
        Me.BottunBox.Controls.Add(Me.BtnDelete)
        Me.BottunBox.Controls.Add(Me.btnPrintRooz)
        Me.BottunBox.Controls.Add(Me.btnPrintSaaty)
        Me.BottunBox.Controls.Add(Me.btnExit)
        Me.BottunBox.Controls.Add(Me.txtCancel)
        Me.BottunBox.Controls.Add(Me.btnSave)
        Me.BottunBox.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.BottunBox.Location = New System.Drawing.Point(0, 346)
        Me.BottunBox.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.BottunBox.Name = "BottunBox"
        Me.BottunBox.Padding = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.BottunBox.Size = New System.Drawing.Size(806, 50)
        Me.BottunBox.TabIndex = 1
        Me.BottunBox.TabStop = False
        '
        'btnPrintRooz
        '
        Me.btnPrintRooz.Font = New System.Drawing.Font("B Koodak", 6.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.btnPrintRooz.Location = New System.Drawing.Point(303, 18)
        Me.btnPrintRooz.Name = "btnPrintRooz"
        Me.btnPrintRooz.Size = New System.Drawing.Size(75, 26)
        Me.btnPrintRooz.TabIndex = 4
        Me.btnPrintRooz.Text = "چاپ روزانه"
        Me.btnPrintRooz.UseVisualStyleBackColor = True
        '
        'btnPrintSaaty
        '
        Me.btnPrintSaaty.Font = New System.Drawing.Font("B Koodak", 6.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.btnPrintSaaty.Location = New System.Drawing.Point(439, 18)
        Me.btnPrintSaaty.Name = "btnPrintSaaty"
        Me.btnPrintSaaty.Size = New System.Drawing.Size(75, 26)
        Me.btnPrintSaaty.TabIndex = 3
        Me.btnPrintSaaty.Text = "چاپ ساعتی"
        Me.btnPrintSaaty.UseVisualStyleBackColor = True
        '
        'btnExit
        '
        Me.btnExit.Font = New System.Drawing.Font("B Koodak", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.btnExit.Location = New System.Drawing.Point(12, 18)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(75, 26)
        Me.btnExit.TabIndex = 2
        Me.btnExit.Text = "خروج"
        Me.btnExit.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'txtCancel
        '
        Me.txtCancel.Font = New System.Drawing.Font("B Koodak", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.txtCancel.Location = New System.Drawing.Point(576, 18)
        Me.txtCancel.Name = "txtCancel"
        Me.txtCancel.Size = New System.Drawing.Size(75, 26)
        Me.txtCancel.TabIndex = 1
        Me.txtCancel.Text = "صرفنظر"
        Me.txtCancel.UseVisualStyleBackColor = True
        '
        'btnSave
        '
        Me.btnSave.Font = New System.Drawing.Font("B Koodak", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.btnSave.Location = New System.Drawing.Point(713, 17)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(75, 26)
        Me.btnSave.TabIndex = 0
        Me.btnSave.Text = "ذخیره"
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'txtSaat
        '
        Me.txtSaat.Location = New System.Drawing.Point(510, 34)
        Me.txtSaat.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.txtSaat.Name = "txtSaat"
        Me.txtSaat.Size = New System.Drawing.Size(39, 21)
        Me.txtSaat.TabIndex = 0
        '
        'lblPersonel
        '
        Me.lblPersonel.AutoSize = True
        Me.lblPersonel.Font = New System.Drawing.Font("B Koodak", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.lblPersonel.Location = New System.Drawing.Point(755, 34)
        Me.lblPersonel.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblPersonel.Name = "lblPersonel"
        Me.lblPersonel.Size = New System.Drawing.Size(38, 19)
        Me.lblPersonel.TabIndex = 1
        Me.lblPersonel.Text = "پرسنل :"
        '
        'cmbPersonel
        '
        Me.cmbPersonel.FormattingEnabled = True
        Me.cmbPersonel.Location = New System.Drawing.Point(607, 34)
        Me.cmbPersonel.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.cmbPersonel.Name = "cmbPersonel"
        Me.cmbPersonel.Size = New System.Drawing.Size(140, 21)
        Me.cmbPersonel.TabIndex = 2
        '
        'lblSaat
        '
        Me.lblSaat.AutoSize = True
        Me.lblSaat.Font = New System.Drawing.Font("B Koodak", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.lblSaat.Location = New System.Drawing.Point(556, 36)
        Me.lblSaat.Name = "lblSaat"
        Me.lblSaat.Size = New System.Drawing.Size(36, 19)
        Me.lblSaat.TabIndex = 3
        Me.lblSaat.Text = "ساعت :"
        '
        'txtRooz
        '
        Me.txtRooz.Location = New System.Drawing.Point(437, 34)
        Me.txtRooz.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.txtRooz.Name = "txtRooz"
        Me.txtRooz.Size = New System.Drawing.Size(38, 21)
        Me.txtRooz.TabIndex = 5
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.txtTozihat)
        Me.GroupBox1.Controls.Add(Me.lblTozihat)
        Me.GroupBox1.Controls.Add(Me.mskTaTarikh)
        Me.GroupBox1.Controls.Add(Me.mskAzTarikh)
        Me.GroupBox1.Controls.Add(Me.lblTaTArikh)
        Me.GroupBox1.Controls.Add(Me.lblAzTarikh)
        Me.GroupBox1.Controls.Add(Me.cmbMah)
        Me.GroupBox1.Controls.Add(Me.lblMah)
        Me.GroupBox1.Controls.Add(Me.txtRooz)
        Me.GroupBox1.Controls.Add(Me.lblRooz)
        Me.GroupBox1.Controls.Add(Me.lblSaat)
        Me.GroupBox1.Controls.Add(Me.cmbPersonel)
        Me.GroupBox1.Controls.Add(Me.lblPersonel)
        Me.GroupBox1.Controls.Add(Me.txtSaat)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.GroupBox1.Size = New System.Drawing.Size(806, 114)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        '
        'txtTozihat
        '
        Me.txtTozihat.Location = New System.Drawing.Point(12, 75)
        Me.txtTozihat.Name = "txtTozihat"
        Me.txtTozihat.Size = New System.Drawing.Size(735, 21)
        Me.txtTozihat.TabIndex = 13
        '
        'lblTozihat
        '
        Me.lblTozihat.AutoSize = True
        Me.lblTozihat.Font = New System.Drawing.Font("B Koodak", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.lblTozihat.Location = New System.Drawing.Point(748, 77)
        Me.lblTozihat.Name = "lblTozihat"
        Me.lblTozihat.Size = New System.Drawing.Size(49, 19)
        Me.lblTozihat.TabIndex = 12
        Me.lblTozihat.Text = "توضیحات :"
        '
        'mskTaTarikh
        '
        Me.mskTaTarikh.Location = New System.Drawing.Point(12, 34)
        Me.mskTaTarikh.Mask = "0000/00/00"
        Me.mskTaTarikh.Name = "mskTaTarikh"
        Me.mskTaTarikh.PromptChar = Global.Microsoft.VisualBasic.ChrW(32)
        Me.mskTaTarikh.Size = New System.Drawing.Size(74, 21)
        Me.mskTaTarikh.TabIndex = 11
        '
        'mskAzTarikh
        '
        Me.mskAzTarikh.Location = New System.Drawing.Point(147, 34)
        Me.mskAzTarikh.Mask = "0000/00/00"
        Me.mskAzTarikh.Name = "mskAzTarikh"
        Me.mskAzTarikh.PromptChar = Global.Microsoft.VisualBasic.ChrW(32)
        Me.mskAzTarikh.Size = New System.Drawing.Size(74, 21)
        Me.mskAzTarikh.TabIndex = 10
        '
        'lblTaTArikh
        '
        Me.lblTaTArikh.AutoSize = True
        Me.lblTaTArikh.Font = New System.Drawing.Font("B Koodak", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.lblTaTArikh.Location = New System.Drawing.Point(92, 36)
        Me.lblTaTArikh.Name = "lblTaTArikh"
        Me.lblTaTArikh.Size = New System.Drawing.Size(41, 19)
        Me.lblTaTArikh.TabIndex = 9
        Me.lblTaTArikh.Text = "تا تاریخ :"
        '
        'lblAzTarikh
        '
        Me.lblAzTarikh.AutoSize = True
        Me.lblAzTarikh.Font = New System.Drawing.Font("B Koodak", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.lblAzTarikh.Location = New System.Drawing.Point(227, 36)
        Me.lblAzTarikh.Name = "lblAzTarikh"
        Me.lblAzTarikh.Size = New System.Drawing.Size(41, 19)
        Me.lblAzTarikh.TabIndex = 8
        Me.lblAzTarikh.Text = "از تاریخ :"
        '
        'cmbMah
        '
        Me.cmbMah.FormattingEnabled = True
        Me.cmbMah.Location = New System.Drawing.Point(282, 34)
        Me.cmbMah.Name = "cmbMah"
        Me.cmbMah.Size = New System.Drawing.Size(121, 21)
        Me.cmbMah.TabIndex = 7
        '
        'lblMah
        '
        Me.lblMah.AutoSize = True
        Me.lblMah.Font = New System.Drawing.Font("B Koodak", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.lblMah.Location = New System.Drawing.Point(406, 36)
        Me.lblMah.Name = "lblMah"
        Me.lblMah.Size = New System.Drawing.Size(24, 19)
        Me.lblMah.TabIndex = 6
        Me.lblMah.Text = "ماه :"
        '
        'lblRooz
        '
        Me.lblRooz.AutoSize = True
        Me.lblRooz.Font = New System.Drawing.Font("B Koodak", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.lblRooz.Location = New System.Drawing.Point(478, 34)
        Me.lblRooz.Name = "lblRooz"
        Me.lblRooz.Size = New System.Drawing.Size(25, 19)
        Me.lblRooz.TabIndex = 4
        Me.lblRooz.Text = "روز :"
        '
        'GridEXTitr
        '
        Me.GridEXTitr.AllowChildTableGroups = True
        Me.GridEXTitr.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.GridEXTitr.AllowDrop = True
        Me.GridEXTitr.AllowRemoveColumns = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.GridEXTitr.BorderStyle = Janus.Windows.GridEX.BorderStyle.RaisedLight3D
        Me.GridEXTitr.ColumnSetNavigation = Janus.Windows.GridEX.ColumnSetNavigation.ColumnSet
        Me.GridEXTitr.DynamicFiltering = True
        Me.GridEXTitr.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        Me.GridEXTitr.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.GridEXTitr.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges
        Me.GridEXTitr.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridEXTitr.GroupByBoxVisible = False
        Me.GridEXTitr.GroupRowVisualStyle = Janus.Windows.GridEX.GroupRowVisualStyle.Outlook2003
        Me.GridEXTitr.GroupTotals = Janus.Windows.GridEX.GroupTotals.Always
        Me.GridEXTitr.KeepRowSettings = True
        GridEXTitr_Layout_0.Key = "Layout1"
        GridEXTitr_Layout_1.Key = "Layout2"
        GridEXTitr_Layout_2.Key = "Layout3"
        GridEXTitr_Layout_3.Key = "Layout4"
        GridEXTitr_Layout_4.Key = "Layout5"
        GridEXTitr_Layout_5.Key = "Layout6"
        Me.GridEXTitr.Layouts.AddRange(New Janus.Windows.GridEX.GridEXLayout() {GridEXTitr_Layout_0, GridEXTitr_Layout_1, GridEXTitr_Layout_2, GridEXTitr_Layout_3, GridEXTitr_Layout_4, GridEXTitr_Layout_5})
        Me.GridEXTitr.Location = New System.Drawing.Point(12, 120)
        Me.GridEXTitr.Name = "GridEXTitr"
        Me.GridEXTitr.Office2007ColorScheme = Janus.Windows.GridEX.Office2007ColorScheme.Blue
        Me.GridEXTitr.RecordNavigator = True
        Me.GridEXTitr.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.GridEXTitr.SelectionMode = Janus.Windows.GridEX.SelectionMode.MultipleSelection
        Me.GridEXTitr.Size = New System.Drawing.Size(782, 220)
        Me.GridEXTitr.TabIndex = 99
        Me.GridEXTitr.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed
        Me.GridEXTitr.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'BtnDelete
        '
        Me.BtnDelete.Font = New System.Drawing.Font("B Koodak", 6.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.BtnDelete.Location = New System.Drawing.Point(175, 18)
        Me.BtnDelete.Name = "BtnDelete"
        Me.BtnDelete.Size = New System.Drawing.Size(75, 26)
        Me.BtnDelete.TabIndex = 5
        Me.BtnDelete.Text = "حذف"
        Me.BtnDelete.UseVisualStyleBackColor = True
        '
        'frmMorkhasi
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.WhiteSmoke
        Me.ClientSize = New System.Drawing.Size(806, 396)
        Me.Controls.Add(Me.GridEXTitr)
        Me.Controls.Add(Me.BottunBox)
        Me.Controls.Add(Me.GroupBox1)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.MaximizeBox = False
        Me.Name = "frmMorkhasi"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.RightToLeftLayout = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "برگه مرخصی"
        Me.BottunBox.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.GridEXTitr, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents BottunBox As System.Windows.Forms.GroupBox
    Friend WithEvents txtSaat As System.Windows.Forms.TextBox
    Friend WithEvents lblPersonel As System.Windows.Forms.Label
    Friend WithEvents cmbPersonel As System.Windows.Forms.ComboBox
    Friend WithEvents lblSaat As System.Windows.Forms.Label
    Friend WithEvents txtRooz As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents lblMah As System.Windows.Forms.Label
    Friend WithEvents lblRooz As System.Windows.Forms.Label
    Friend WithEvents lblAzTarikh As System.Windows.Forms.Label
    Friend WithEvents cmbMah As System.Windows.Forms.ComboBox
    Friend WithEvents lblTaTArikh As System.Windows.Forms.Label
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents txtCancel As System.Windows.Forms.Button
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents mskAzTarikh As System.Windows.Forms.MaskedTextBox
    Friend WithEvents mskTaTarikh As System.Windows.Forms.MaskedTextBox
    Friend WithEvents GridEXTitr As Janus.Windows.GridEX.GridEX
    Friend WithEvents txtTozihat As System.Windows.Forms.TextBox
    Friend WithEvents lblTozihat As System.Windows.Forms.Label
    Friend WithEvents btnPrintRooz As System.Windows.Forms.Button
    Friend WithEvents btnPrintSaaty As System.Windows.Forms.Button
    Friend WithEvents BtnDelete As System.Windows.Forms.Button

End Class
