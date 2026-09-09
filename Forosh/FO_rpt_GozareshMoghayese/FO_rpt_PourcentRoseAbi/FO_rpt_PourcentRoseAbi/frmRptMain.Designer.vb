<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmRptMain
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmRptMain))
        Me.btnReport = New System.Windows.Forms.Button()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.chkListMantagheh = New System.Windows.Forms.CheckedListBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.cmbForoshandeh = New System.Windows.Forms.ComboBox()
        Me.gb1 = New System.Windows.Forms.GroupBox()
        Me.mskTaTarikh1 = New System.Windows.Forms.MaskedTextBox()
        Me.mskAzTarikh1 = New System.Windows.Forms.MaskedTextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.gb2 = New System.Windows.Forms.GroupBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.mskAzTarikh2 = New System.Windows.Forms.MaskedTextBox()
        Me.mskTaTarikh2 = New System.Windows.Forms.MaskedTextBox()
        Me.btnExcel = New System.Windows.Forms.Button()
        Me.gb3 = New System.Windows.Forms.GroupBox()
        Me.btnSelect = New System.Windows.Forms.Button()
        Me.gb1.SuspendLayout()
        Me.gb2.SuspendLayout()
        Me.gb3.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnReport
        '
        Me.btnReport.BackgroundImage = CType(resources.GetObject("btnReport.BackgroundImage"), System.Drawing.Image)
        Me.btnReport.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnReport.Location = New System.Drawing.Point(399, 231)
        Me.btnReport.Name = "btnReport"
        Me.btnReport.Size = New System.Drawing.Size(47, 51)
        Me.btnReport.TabIndex = 11
        Me.btnReport.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(378, 122)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(41, 13)
        Me.Label5.TabIndex = 10
        Me.Label5.Text = "منطقه:"
        '
        'chkListMantagheh
        '
        Me.chkListMantagheh.FormattingEnabled = True
        Me.chkListMantagheh.Location = New System.Drawing.Point(6, 62)
        Me.chkListMantagheh.Name = "chkListMantagheh"
        Me.chkListMantagheh.Size = New System.Drawing.Size(366, 116)
        Me.chkListMantagheh.TabIndex = 9
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(378, 29)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(55, 13)
        Me.Label6.TabIndex = 7
        Me.Label6.Text = "فروشنده :"
        '
        'cmbForoshandeh
        '
        Me.cmbForoshandeh.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbForoshandeh.FormattingEnabled = True
        Me.cmbForoshandeh.Location = New System.Drawing.Point(6, 26)
        Me.cmbForoshandeh.Name = "cmbForoshandeh"
        Me.cmbForoshandeh.Size = New System.Drawing.Size(366, 21)
        Me.cmbForoshandeh.TabIndex = 6
        '
        'gb1
        '
        Me.gb1.Controls.Add(Me.btnSelect)
        Me.gb1.Controls.Add(Me.Label5)
        Me.gb1.Controls.Add(Me.chkListMantagheh)
        Me.gb1.Controls.Add(Me.Label6)
        Me.gb1.Controls.Add(Me.cmbForoshandeh)
        Me.gb1.Location = New System.Drawing.Point(12, 12)
        Me.gb1.Name = "gb1"
        Me.gb1.Size = New System.Drawing.Size(437, 208)
        Me.gb1.TabIndex = 13
        Me.gb1.TabStop = False
        '
        'mskTaTarikh1
        '
        Me.mskTaTarikh1.AllowPromptAsInput = False
        Me.mskTaTarikh1.CutCopyMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals
        Me.mskTaTarikh1.Location = New System.Drawing.Point(18, 82)
        Me.mskTaTarikh1.Mask = "####/##/##"
        Me.mskTaTarikh1.Name = "mskTaTarikh1"
        Me.mskTaTarikh1.PromptChar = Global.Microsoft.VisualBasic.ChrW(32)
        Me.mskTaTarikh1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.mskTaTarikh1.Size = New System.Drawing.Size(82, 21)
        Me.mskTaTarikh1.TabIndex = 2
        Me.mskTaTarikh1.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals
        '
        'mskAzTarikh1
        '
        Me.mskAzTarikh1.AllowPromptAsInput = False
        Me.mskAzTarikh1.CutCopyMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals
        Me.mskAzTarikh1.Location = New System.Drawing.Point(18, 29)
        Me.mskAzTarikh1.Mask = "####/##/##"
        Me.mskAzTarikh1.Name = "mskAzTarikh1"
        Me.mskAzTarikh1.PromptChar = Global.Microsoft.VisualBasic.ChrW(32)
        Me.mskAzTarikh1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.mskAzTarikh1.Size = New System.Drawing.Size(82, 21)
        Me.mskAzTarikh1.TabIndex = 1
        Me.mskAzTarikh1.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(116, 32)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(45, 13)
        Me.Label1.TabIndex = 7
        Me.Label1.Text = "از تاریخ :"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(116, 85)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(44, 13)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "تا تاریخ :"
        '
        'gb2
        '
        Me.gb2.Controls.Add(Me.mskTaTarikh1)
        Me.gb2.Controls.Add(Me.mskAzTarikh1)
        Me.gb2.Controls.Add(Me.Label1)
        Me.gb2.Controls.Add(Me.Label3)
        Me.gb2.Location = New System.Drawing.Point(9, 226)
        Me.gb2.Name = "gb2"
        Me.gb2.Size = New System.Drawing.Size(180, 123)
        Me.gb2.TabIndex = 9
        Me.gb2.TabStop = False
        Me.gb2.Text = "تاریخ اول"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(118, 32)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(45, 13)
        Me.Label2.TabIndex = 9
        Me.Label2.Text = "از تاریخ :"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(118, 85)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(44, 13)
        Me.Label4.TabIndex = 8
        Me.Label4.Text = "تا تاریخ :"
        '
        'mskAzTarikh2
        '
        Me.mskAzTarikh2.AllowPromptAsInput = False
        Me.mskAzTarikh2.CutCopyMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals
        Me.mskAzTarikh2.Location = New System.Drawing.Point(14, 29)
        Me.mskAzTarikh2.Mask = "####/##/##"
        Me.mskAzTarikh2.Name = "mskAzTarikh2"
        Me.mskAzTarikh2.PromptChar = Global.Microsoft.VisualBasic.ChrW(32)
        Me.mskAzTarikh2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.mskAzTarikh2.Size = New System.Drawing.Size(82, 21)
        Me.mskAzTarikh2.TabIndex = 4
        Me.mskAzTarikh2.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals
        '
        'mskTaTarikh2
        '
        Me.mskTaTarikh2.AllowPromptAsInput = False
        Me.mskTaTarikh2.CutCopyMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals
        Me.mskTaTarikh2.Location = New System.Drawing.Point(14, 82)
        Me.mskTaTarikh2.Mask = "####/##/##"
        Me.mskTaTarikh2.Name = "mskTaTarikh2"
        Me.mskTaTarikh2.PromptChar = Global.Microsoft.VisualBasic.ChrW(32)
        Me.mskTaTarikh2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.mskTaTarikh2.Size = New System.Drawing.Size(82, 21)
        Me.mskTaTarikh2.TabIndex = 5
        Me.mskTaTarikh2.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals
        '
        'btnExcel
        '
        Me.btnExcel.BackgroundImage = CType(resources.GetObject("btnExcel.BackgroundImage"), System.Drawing.Image)
        Me.btnExcel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnExcel.Location = New System.Drawing.Point(399, 298)
        Me.btnExcel.Name = "btnExcel"
        Me.btnExcel.Size = New System.Drawing.Size(47, 51)
        Me.btnExcel.TabIndex = 12
        Me.btnExcel.UseVisualStyleBackColor = True
        '
        'gb3
        '
        Me.gb3.Controls.Add(Me.Label2)
        Me.gb3.Controls.Add(Me.Label4)
        Me.gb3.Controls.Add(Me.mskAzTarikh2)
        Me.gb3.Controls.Add(Me.mskTaTarikh2)
        Me.gb3.Location = New System.Drawing.Point(213, 226)
        Me.gb3.Name = "gb3"
        Me.gb3.Size = New System.Drawing.Size(180, 123)
        Me.gb3.TabIndex = 10
        Me.gb3.TabStop = False
        Me.gb3.Text = "تاریخ دوم"
        '
        'btnSelect
        '
        Me.btnSelect.Location = New System.Drawing.Point(6, 181)
        Me.btnSelect.Name = "btnSelect"
        Me.btnSelect.Size = New System.Drawing.Size(96, 23)
        Me.btnSelect.TabIndex = 14
        Me.btnSelect.Text = "انتخاب همه"
        Me.btnSelect.UseVisualStyleBackColor = True
        '
        'frmRptMain
        '
        Me.AllowDrop = True
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(459, 360)
        Me.Controls.Add(Me.btnReport)
        Me.Controls.Add(Me.gb1)
        Me.Controls.Add(Me.gb2)
        Me.Controls.Add(Me.btnExcel)
        Me.Controls.Add(Me.gb3)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "frmRptMain"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.RightToLeftLayout = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "گزارش فروش مقایسه ای"
        Me.gb1.ResumeLayout(False)
        Me.gb1.PerformLayout()
        Me.gb2.ResumeLayout(False)
        Me.gb2.PerformLayout()
        Me.gb3.ResumeLayout(False)
        Me.gb3.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnReport As System.Windows.Forms.Button
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents chkListMantagheh As System.Windows.Forms.CheckedListBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents cmbForoshandeh As System.Windows.Forms.ComboBox
    Friend WithEvents gb1 As System.Windows.Forms.GroupBox
    Friend WithEvents mskTaTarikh1 As System.Windows.Forms.MaskedTextBox
    Friend WithEvents mskAzTarikh1 As System.Windows.Forms.MaskedTextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents gb2 As System.Windows.Forms.GroupBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents mskAzTarikh2 As System.Windows.Forms.MaskedTextBox
    Friend WithEvents mskTaTarikh2 As System.Windows.Forms.MaskedTextBox
    Friend WithEvents btnExcel As System.Windows.Forms.Button
    Friend WithEvents gb3 As System.Windows.Forms.GroupBox
    Friend WithEvents btnSelect As System.Windows.Forms.Button

End Class
