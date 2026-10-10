<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
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
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.txtBargashtTakhfif = New System.Windows.Forms.TextBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.txtBargashtJayezeh = New System.Windows.Forms.TextBox()
        Me.mskTarikh = New System.Windows.Forms.MaskedTextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.txtTozihat = New System.Windows.Forms.TextBox()
        Me.cmbMashinHaml = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.txtshomarehForm = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cmbAnbar = New System.Windows.Forms.ComboBox()
        Me.cmbTaminKonandeh = New System.Windows.Forms.ComboBox()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.txtShomarehResid = New System.Windows.Forms.TextBox()
        Me.cmbsCodeDorehResid = New System.Windows.Forms.ComboBox()
        Me.Label39 = New System.Windows.Forms.Label()
        Me.txtmalyat = New System.Windows.Forms.TextBox()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.txtAvarez = New System.Windows.Forms.TextBox()
        Me.ChkApplyMalyat = New System.Windows.Forms.CheckBox()
        Me.txtSearchT = New System.Windows.Forms.TextBox()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.GroupBox1.Controls.Add(Me.txtSearchT)
        Me.GroupBox1.Controls.Add(Me.Label19)
        Me.GroupBox1.Controls.Add(Me.txtBargashtTakhfif)
        Me.GroupBox1.Controls.Add(Me.Label15)
        Me.GroupBox1.Controls.Add(Me.txtBargashtJayezeh)
        Me.GroupBox1.Controls.Add(Me.mskTarikh)
        Me.GroupBox1.Controls.Add(Me.Label12)
        Me.GroupBox1.Controls.Add(Me.txtTozihat)
        Me.GroupBox1.Controls.Add(Me.cmbMashinHaml)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label14)
        Me.GroupBox1.Controls.Add(Me.Label13)
        Me.GroupBox1.Controls.Add(Me.txtshomarehForm)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.cmbAnbar)
        Me.GroupBox1.Controls.Add(Me.cmbTaminKonandeh)
        Me.GroupBox1.Controls.Add(Me.Label20)
        Me.GroupBox1.Controls.Add(Me.txtShomarehResid)
        Me.GroupBox1.Controls.Add(Me.cmbsCodeDorehResid)
        Me.GroupBox1.Controls.Add(Me.Label39)
        Me.GroupBox1.Controls.Add(Me.txtmalyat)
        Me.GroupBox1.Controls.Add(Me.Label22)
        Me.GroupBox1.Controls.Add(Me.txtAvarez)
        Me.GroupBox1.Controls.Add(Me.ChkApplyMalyat)
        Me.GroupBox1.Location = New System.Drawing.Point(1, 1)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.GroupBox1.Size = New System.Drawing.Size(1003, 280)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "ورودی اطلاعات اصلی"
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Font = New System.Drawing.Font("Tahoma", 8.25!)
        Me.Label19.Location = New System.Drawing.Point(117, 133)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(107, 13)
        Me.Label19.TabIndex = 47
        Me.Label19.Text = "مبلغ برگشتی تخفیف:"
        '
        'txtBargashtTakhfif
        '
        Me.txtBargashtTakhfif.Location = New System.Drawing.Point(24, 129)
        Me.txtBargashtTakhfif.Name = "txtBargashtTakhfif"
        Me.txtBargashtTakhfif.Size = New System.Drawing.Size(83, 20)
        Me.txtBargashtTakhfif.TabIndex = 42
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Font = New System.Drawing.Font("Tahoma", 8.25!)
        Me.Label15.Location = New System.Drawing.Point(325, 135)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(100, 13)
        Me.Label15.TabIndex = 45
        Me.Label15.Text = "مبلغ برگشتی جایزه:"
        '
        'txtBargashtJayezeh
        '
        Me.txtBargashtJayezeh.Location = New System.Drawing.Point(229, 131)
        Me.txtBargashtJayezeh.Name = "txtBargashtJayezeh"
        Me.txtBargashtJayezeh.Size = New System.Drawing.Size(83, 20)
        Me.txtBargashtJayezeh.TabIndex = 41
        '
        'mskTarikh
        '
        Me.mskTarikh.Location = New System.Drawing.Point(716, 25)
        Me.mskTarikh.Mask = "####/##/##"
        Me.mskTarikh.Name = "mskTarikh"
        Me.mskTarikh.PromptChar = Global.Microsoft.VisualBasic.ChrW(32)
        Me.mskTarikh.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.mskTarikh.Size = New System.Drawing.Size(88, 20)
        Me.mskTarikh.TabIndex = 35
        Me.mskTarikh.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Tahoma", 8.25!)
        Me.Label12.Location = New System.Drawing.Point(931, 164)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(51, 13)
        Me.Label12.TabIndex = 48
        Me.Label12.Text = "توضیحات:"
        '
        'txtTozihat
        '
        Me.txtTozihat.Location = New System.Drawing.Point(629, 162)
        Me.txtTozihat.Name = "txtTozihat"
        Me.txtTozihat.Size = New System.Drawing.Size(297, 20)
        Me.txtTozihat.TabIndex = 44
        '
        'cmbMashinHaml
        '
        Me.cmbMashinHaml.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbMashinHaml.Location = New System.Drawing.Point(24, 69)
        Me.cmbMashinHaml.MaxDropDownItems = 20
        Me.cmbMashinHaml.Name = "cmbMashinHaml"
        Me.cmbMashinHaml.Size = New System.Drawing.Size(218, 21)
        Me.cmbMashinHaml.TabIndex = 38
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 8.25!)
        Me.Label2.Location = New System.Drawing.Point(246, 73)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(71, 13)
        Me.Label2.TabIndex = 43
        Me.Label2.Text = " ماشین حمل:"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Font = New System.Drawing.Font("Tahoma", 8.25!)
        Me.Label14.Location = New System.Drawing.Point(913, 68)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(78, 13)
        Me.Label14.TabIndex = 46
        Me.Label14.Text = "نام تامین کننده:"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Tahoma", 8.25!)
        Me.Label13.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label13.Location = New System.Drawing.Point(931, 29)
        Me.Label13.Name = "Label13"
        Me.Label13.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Label13.Size = New System.Drawing.Size(61, 13)
        Me.Label13.TabIndex = 34
        Me.Label13.Text = "شماره فرم:"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtshomarehForm
        '
        Me.txtshomarehForm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtshomarehForm.Enabled = False
        Me.txtshomarehForm.ForeColor = System.Drawing.Color.Blue
        Me.txtshomarehForm.Location = New System.Drawing.Point(861, 25)
        Me.txtshomarehForm.Name = "txtshomarehForm"
        Me.txtshomarehForm.Size = New System.Drawing.Size(65, 20)
        Me.txtshomarehForm.TabIndex = 33
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 8.25!)
        Me.Label4.Location = New System.Drawing.Point(593, 69)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(44, 13)
        Me.Label4.TabIndex = 40
        Me.Label4.Text = "نام انبار:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 8.25!)
        Me.Label3.Location = New System.Drawing.Point(805, 29)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(52, 13)
        Me.Label3.TabIndex = 37
        Me.Label3.Text = "تاریخ فرم:"
        '
        'cmbAnbar
        '
        Me.cmbAnbar.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbAnbar.Location = New System.Drawing.Point(343, 64)
        Me.cmbAnbar.MaxDropDownItems = 20
        Me.cmbAnbar.Name = "cmbAnbar"
        Me.cmbAnbar.Size = New System.Drawing.Size(247, 21)
        Me.cmbAnbar.TabIndex = 36
        '
        'cmbTaminKonandeh
        '
        Me.cmbTaminKonandeh.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbTaminKonandeh.Location = New System.Drawing.Point(701, 102)
        Me.cmbTaminKonandeh.MaxDropDownItems = 20
        Me.cmbTaminKonandeh.Name = "cmbTaminKonandeh"
        Me.cmbTaminKonandeh.Size = New System.Drawing.Size(210, 21)
        Me.cmbTaminKonandeh.TabIndex = 39
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Font = New System.Drawing.Font("Tahoma", 8.25!)
        Me.Label20.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label20.Location = New System.Drawing.Point(590, 105)
        Me.Label20.Name = "Label20"
        Me.Label20.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Label20.Size = New System.Drawing.Size(69, 13)
        Me.Label20.TabIndex = 49
        Me.Label20.Text = "شماره رسید:"
        Me.Label20.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtShomarehResid
        '
        Me.txtShomarehResid.Location = New System.Drawing.Point(467, 102)
        Me.txtShomarehResid.MaxLength = 10
        Me.txtShomarehResid.Name = "txtShomarehResid"
        Me.txtShomarehResid.Size = New System.Drawing.Size(66, 20)
        Me.txtShomarehResid.TabIndex = 51
        '
        'cmbsCodeDorehResid
        '
        Me.cmbsCodeDorehResid.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbsCodeDorehResid.Location = New System.Drawing.Point(536, 102)
        Me.cmbsCodeDorehResid.MaxDropDownItems = 20
        Me.cmbsCodeDorehResid.Name = "cmbsCodeDorehResid"
        Me.cmbsCodeDorehResid.Size = New System.Drawing.Size(54, 21)
        Me.cmbsCodeDorehResid.TabIndex = 50
        '
        'Label39
        '
        Me.Label39.AutoSize = True
        Me.Label39.Font = New System.Drawing.Font("Tahoma", 8.25!)
        Me.Label39.Location = New System.Drawing.Point(326, 162)
        Me.Label39.Name = "Label39"
        Me.Label39.Size = New System.Drawing.Size(108, 13)
        Me.Label39.TabIndex = 53
        Me.Label39.Text = "مبلغ برگشتی مالیات :"
        '
        'txtmalyat
        '
        Me.txtmalyat.Location = New System.Drawing.Point(229, 158)
        Me.txtmalyat.Name = "txtmalyat"
        Me.txtmalyat.Size = New System.Drawing.Size(83, 20)
        Me.txtmalyat.TabIndex = 52
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Font = New System.Drawing.Font("Tahoma", 8.25!)
        Me.Label22.Location = New System.Drawing.Point(118, 162)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(104, 13)
        Me.Label22.TabIndex = 55
        Me.Label22.Text = "مبلغ برگشت عوارض:"
        '
        'txtAvarez
        '
        Me.txtAvarez.Location = New System.Drawing.Point(27, 157)
        Me.txtAvarez.Name = "txtAvarez"
        Me.txtAvarez.Size = New System.Drawing.Size(80, 20)
        Me.txtAvarez.TabIndex = 54
        '
        'ChkApplyMalyat
        '
        Me.ChkApplyMalyat.AutoSize = True
        Me.ChkApplyMalyat.Checked = True
        Me.ChkApplyMalyat.CheckState = System.Windows.Forms.CheckState.Checked
        Me.ChkApplyMalyat.Font = New System.Drawing.Font("Tahoma", 8.25!)
        Me.ChkApplyMalyat.Location = New System.Drawing.Point(457, 157)
        Me.ChkApplyMalyat.Name = "ChkApplyMalyat"
        Me.ChkApplyMalyat.Size = New System.Drawing.Size(137, 17)
        Me.ChkApplyMalyat.TabIndex = 56
        Me.ChkApplyMalyat.Text = "مشمول مالیات و عوارض"
        Me.ChkApplyMalyat.UseVisualStyleBackColor = True
        '
        'txtSearchT
        '
        Me.txtSearchT.Location = New System.Drawing.Point(706, 65)
        Me.txtSearchT.Name = "txtSearchT"
        Me.txtSearchT.Size = New System.Drawing.Size(205, 20)
        Me.txtSearchT.TabIndex = 136
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1005, 450)
        Me.Controls.Add(Me.GroupBox1)
        Me.Name = "Form1"
        Me.Text = "Form1"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents Label19 As Label
    Friend WithEvents txtBargashtTakhfif As TextBox
    Friend WithEvents Label15 As Label
    Friend WithEvents txtBargashtJayezeh As TextBox
    Friend WithEvents mskTarikh As MaskedTextBox
    Friend WithEvents Label12 As Label
    Friend WithEvents txtTozihat As TextBox
    Friend WithEvents cmbMashinHaml As ComboBox
    Friend WithEvents Label2 As Label
    Friend WithEvents Label14 As Label
    Friend WithEvents Label13 As Label
    Friend WithEvents txtshomarehForm As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents cmbAnbar As ComboBox
    Friend WithEvents cmbTaminKonandeh As ComboBox
    Friend WithEvents Label20 As Label
    Friend WithEvents txtShomarehResid As TextBox
    Friend WithEvents cmbsCodeDorehResid As ComboBox
    Friend WithEvents Label39 As Label
    Friend WithEvents txtmalyat As TextBox
    Friend WithEvents Label22 As Label
    Friend WithEvents txtAvarez As TextBox
    Friend WithEvents ChkApplyMalyat As CheckBox
    Friend WithEvents txtSearchT As TextBox
End Class
