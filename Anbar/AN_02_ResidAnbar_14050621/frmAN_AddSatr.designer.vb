<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAN_AddSatr
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
        Me.TextGTIN = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.TextIRC = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.mskTaTarikh = New System.Windows.Forms.MaskedTextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.mskAzTarikh = New System.Windows.Forms.MaskedTextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtMasrafkonandeh = New clsMaskNumber.MaskNumber()
        Me.txtForosh = New clsMaskNumber.MaskNumber()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.lblNameKala = New System.Windows.Forms.Label()
        Me.txtCodeKala = New System.Windows.Forms.TextBox()
        Me.txtShomarehBatch = New System.Windows.Forms.TextBox()
        Me.lblRadif = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.mskTarikhTolid = New System.Windows.Forms.MaskedTextBox()
        Me.mskTarikhEngheza = New System.Windows.Forms.MaskedTextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.btnCancelSanad = New System.Windows.Forms.Button()
        Me.btnSaveSanad = New System.Windows.Forms.Button()
        Me.txtMablaghKharid = New clsMaskNumber.MaskNumber()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.txtMablaghKharid)
        Me.GroupBox1.Controls.Add(Me.TextGTIN)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.TextIRC)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.mskTaTarikh)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.mskAzTarikh)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.txtMasrafkonandeh)
        Me.GroupBox1.Controls.Add(Me.txtForosh)
        Me.GroupBox1.Controls.Add(Me.Label15)
        Me.GroupBox1.Controls.Add(Me.Label13)
        Me.GroupBox1.Controls.Add(Me.lblNameKala)
        Me.GroupBox1.Controls.Add(Me.txtCodeKala)
        Me.GroupBox1.Controls.Add(Me.txtShomarehBatch)
        Me.GroupBox1.Controls.Add(Me.lblRadif)
        Me.GroupBox1.Controls.Add(Me.Label9)
        Me.GroupBox1.Controls.Add(Me.mskTarikhTolid)
        Me.GroupBox1.Controls.Add(Me.mskTarikhEngheza)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.GroupBox1.Size = New System.Drawing.Size(546, 189)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "ورود اطلاعات کالا"
        '
        'TextGTIN
        '
        Me.TextGTIN.Enabled = False
        Me.TextGTIN.Location = New System.Drawing.Point(314, 162)
        Me.TextGTIN.Name = "TextGTIN"
        Me.TextGTIN.Size = New System.Drawing.Size(134, 21)
        Me.TextGTIN.TabIndex = 78
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.ForeColor = System.Drawing.Color.Black
        Me.Label3.Location = New System.Drawing.Point(454, 165)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(35, 13)
        Me.Label3.TabIndex = 77
        Me.Label3.Text = "GTIN:"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'TextIRC
        '
        Me.TextIRC.Enabled = False
        Me.TextIRC.Location = New System.Drawing.Point(314, 136)
        Me.TextIRC.Name = "TextIRC"
        Me.TextIRC.Size = New System.Drawing.Size(134, 21)
        Me.TextIRC.TabIndex = 74
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Label4.Location = New System.Drawing.Point(452, 140)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Label4.Size = New System.Drawing.Size(29, 13)
        Me.Label4.TabIndex = 75
        Me.Label4.Text = "IRC:"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'mskTaTarikh
        '
        Me.mskTaTarikh.AllowPromptAsInput = False
        Me.mskTaTarikh.Location = New System.Drawing.Point(40, 154)
        Me.mskTaTarikh.Mask = "####/##/##"
        Me.mskTaTarikh.Name = "mskTaTarikh"
        Me.mskTaTarikh.PromptChar = Global.Microsoft.VisualBasic.ChrW(32)
        Me.mskTaTarikh.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.mskTaTarikh.Size = New System.Drawing.Size(135, 21)
        Me.mskTaTarikh.TabIndex = 73
        Me.mskTaTarikh.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.mskTaTarikh.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.ForeColor = System.Drawing.Color.Maroon
        Me.Label1.Location = New System.Drawing.Point(177, 158)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(44, 13)
        Me.Label1.TabIndex = 71
        Me.Label1.Text = "تا تاریخ :"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'mskAzTarikh
        '
        Me.mskAzTarikh.AllowPromptAsInput = False
        Me.mskAzTarikh.Location = New System.Drawing.Point(40, 128)
        Me.mskAzTarikh.Mask = "####/##/##"
        Me.mskAzTarikh.Name = "mskAzTarikh"
        Me.mskAzTarikh.PromptChar = Global.Microsoft.VisualBasic.ChrW(32)
        Me.mskAzTarikh.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.mskAzTarikh.Size = New System.Drawing.Size(135, 21)
        Me.mskAzTarikh.TabIndex = 72
        Me.mskAzTarikh.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.mskAzTarikh.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.ForeColor = System.Drawing.Color.Maroon
        Me.Label2.Location = New System.Drawing.Point(177, 132)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(45, 13)
        Me.Label2.TabIndex = 70
        Me.Label2.Text = "از تاریخ :"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtMasrafkonandeh
        '
        Me.txtMasrafkonandeh.FloatLength = CType(0, Byte)
        Me.txtMasrafkonandeh.FloatSign = "/"
        Me.txtMasrafkonandeh.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.txtMasrafkonandeh.Location = New System.Drawing.Point(40, 102)
        Me.txtMasrafkonandeh.MaxLength = CType(19, Byte)
        Me.txtMasrafkonandeh.Name = "txtMasrafkonandeh"
        Me.txtMasrafkonandeh.SignText = ""
        Me.txtMasrafkonandeh.Size = New System.Drawing.Size(135, 21)
        Me.txtMasrafkonandeh.TabIndex = 67
        '
        'txtForosh
        '
        Me.txtForosh.FloatLength = CType(0, Byte)
        Me.txtForosh.FloatSign = "/"
        Me.txtForosh.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.txtForosh.Location = New System.Drawing.Point(40, 77)
        Me.txtForosh.MaxLength = CType(19, Byte)
        Me.txtForosh.Name = "txtForosh"
        Me.txtForosh.SignText = ""
        Me.txtForosh.Size = New System.Drawing.Size(135, 21)
        Me.txtForosh.TabIndex = 66
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.ForeColor = System.Drawing.Color.Maroon
        Me.Label15.Location = New System.Drawing.Point(182, 105)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(68, 13)
        Me.Label15.TabIndex = 69
        Me.Label15.Text = "مصرف کننده:"
        Me.Label15.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.ForeColor = System.Drawing.Color.Maroon
        Me.Label13.Location = New System.Drawing.Point(181, 81)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(39, 13)
        Me.Label13.TabIndex = 68
        Me.Label13.Text = "فروش:"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblNameKala
        '
        Me.lblNameKala.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblNameKala.Location = New System.Drawing.Point(40, 27)
        Me.lblNameKala.Name = "lblNameKala"
        Me.lblNameKala.Size = New System.Drawing.Size(251, 21)
        Me.lblNameKala.TabIndex = 22
        '
        'txtCodeKala
        '
        Me.txtCodeKala.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtCodeKala.Location = New System.Drawing.Point(314, 27)
        Me.txtCodeKala.MaxLength = 12
        Me.txtCodeKala.Name = "txtCodeKala"
        Me.txtCodeKala.Size = New System.Drawing.Size(132, 21)
        Me.txtCodeKala.TabIndex = 21
        '
        'txtShomarehBatch
        '
        Me.txtShomarehBatch.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtShomarehBatch.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.txtShomarehBatch.Location = New System.Drawing.Point(314, 56)
        Me.txtShomarehBatch.MaxLength = 10
        Me.txtShomarehBatch.Name = "txtShomarehBatch"
        Me.txtShomarehBatch.Size = New System.Drawing.Size(133, 21)
        Me.txtShomarehBatch.TabIndex = 29
        '
        'lblRadif
        '
        Me.lblRadif.AutoSize = True
        Me.lblRadif.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.lblRadif.Location = New System.Drawing.Point(450, 31)
        Me.lblRadif.Name = "lblRadif"
        Me.lblRadif.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.lblRadif.Size = New System.Drawing.Size(42, 13)
        Me.lblRadif.TabIndex = 23
        Me.lblRadif.Text = "نام کالا:"
        Me.lblRadif.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Label9.Location = New System.Drawing.Point(452, 60)
        Me.Label9.Name = "Label9"
        Me.Label9.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Label9.Size = New System.Drawing.Size(55, 13)
        Me.Label9.TabIndex = 30
        Me.Label9.Text = "شماره بچ:"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'mskTarikhTolid
        '
        Me.mskTarikhTolid.Enabled = False
        Me.mskTarikhTolid.Location = New System.Drawing.Point(314, 82)
        Me.mskTarikhTolid.Mask = "####/##/##"
        Me.mskTarikhTolid.Name = "mskTarikhTolid"
        Me.mskTarikhTolid.PromptChar = Global.Microsoft.VisualBasic.ChrW(32)
        Me.mskTarikhTolid.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.mskTarikhTolid.Size = New System.Drawing.Size(134, 21)
        Me.mskTarikhTolid.TabIndex = 32
        Me.mskTarikhTolid.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.mskTarikhTolid.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals
        '
        'mskTarikhEngheza
        '
        Me.mskTarikhEngheza.Enabled = False
        Me.mskTarikhEngheza.Location = New System.Drawing.Point(314, 109)
        Me.mskTarikhEngheza.Mask = "####/##/##"
        Me.mskTarikhEngheza.Name = "mskTarikhEngheza"
        Me.mskTarikhEngheza.PromptChar = Global.Microsoft.VisualBasic.ChrW(32)
        Me.mskTarikhEngheza.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.mskTarikhEngheza.Size = New System.Drawing.Size(134, 21)
        Me.mskTarikhEngheza.TabIndex = 31
        Me.mskTarikhEngheza.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.mskTarikhEngheza.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Label5.Location = New System.Drawing.Point(179, 56)
        Me.Label5.Name = "Label5"
        Me.Label5.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Label5.Size = New System.Drawing.Size(55, 13)
        Me.Label5.TabIndex = 33
        Me.Label5.Text = "مبلغ خرید:"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(451, 113)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(59, 13)
        Me.Label6.TabIndex = 37
        Me.Label6.Text = "تاریخ انقضا:"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(451, 86)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(56, 13)
        Me.Label7.TabIndex = 35
        Me.Label7.Text = "تاریخ تولید:"
        '
        'btnCancelSanad
        '
        Me.btnCancelSanad.AccessibleDescription = ""
        Me.btnCancelSanad.AccessibleName = ""
        Me.btnCancelSanad.Font = New System.Drawing.Font("Tahoma", 8.25!)
        Me.btnCancelSanad.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnCancelSanad.Location = New System.Drawing.Point(119, 222)
        Me.btnCancelSanad.Name = "btnCancelSanad"
        Me.btnCancelSanad.Size = New System.Drawing.Size(132, 27)
        Me.btnCancelSanad.TabIndex = 11
        Me.btnCancelSanad.Text = "خروج"
        '
        'btnSaveSanad
        '
        Me.btnSaveSanad.AccessibleDescription = ""
        Me.btnSaveSanad.AccessibleName = ""
        Me.btnSaveSanad.Font = New System.Drawing.Font("Tahoma", 8.25!)
        Me.btnSaveSanad.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnSaveSanad.Location = New System.Drawing.Point(276, 222)
        Me.btnSaveSanad.Name = "btnSaveSanad"
        Me.btnSaveSanad.Size = New System.Drawing.Size(140, 27)
        Me.btnSaveSanad.TabIndex = 10
        Me.btnSaveSanad.Text = "&ذخيره"
        '
        'txtMablaghKharid
        '
        Me.txtMablaghKharid.FloatLength = CType(0, Byte)
        Me.txtMablaghKharid.FloatSign = "/"
        Me.txtMablaghKharid.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.txtMablaghKharid.Location = New System.Drawing.Point(40, 52)
        Me.txtMablaghKharid.MaxLength = CType(19, Byte)
        Me.txtMablaghKharid.Name = "txtMablaghKharid"
        Me.txtMablaghKharid.SignText = ""
        Me.txtMablaghKharid.Size = New System.Drawing.Size(135, 21)
        Me.txtMablaghKharid.TabIndex = 79
        '
        'frmAN_AddSatr
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(570, 261)
        Me.Controls.Add(Me.btnCancelSanad)
        Me.Controls.Add(Me.btnSaveSanad)
        Me.Controls.Add(Me.GroupBox1)
        Me.Name = "frmAN_AddSatr"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "افزودن قیمت های سه گانه"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents btnCancelSanad As Button
    Friend WithEvents btnSaveSanad As Button
    Friend WithEvents Label7 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents mskTarikhEngheza As MaskedTextBox
    Friend WithEvents mskTarikhTolid As MaskedTextBox
    Friend WithEvents Label9 As Label
    Friend WithEvents lblRadif As Label
    Friend WithEvents txtShomarehBatch As TextBox
    Friend WithEvents txtCodeKala As TextBox
    Friend WithEvents lblNameKala As Label
    Friend WithEvents txtMasrafkonandeh As clsMaskNumber.MaskNumber
    Friend WithEvents txtForosh As clsMaskNumber.MaskNumber
    Friend WithEvents Label15 As Label
    Friend WithEvents Label13 As Label
    Friend WithEvents mskTaTarikh As MaskedTextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents mskAzTarikh As MaskedTextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents TextIRC As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents TextGTIN As TextBox
    Friend WithEvents txtMablaghKharid As clsMaskNumber.MaskNumber
End Class
