<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class An_KhorojMavadAvalieh
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(An_KhorojMavadAvalieh))
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.mskTaTarikh = New System.Windows.Forms.MaskedTextBox()
        Me.mskAzTarikh = New System.Windows.Forms.MaskedTextBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.txtShomarehS = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnSearch = New System.Windows.Forms.Button()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.dbgTitr = New System.Windows.Forms.DataGrid()
        Me.txtShomarehForm = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtSharh = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.cmbTolidkonandeh = New System.Windows.Forms.ComboBox()
        Me.cmbAnbar = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.mskTarikh = New System.Windows.Forms.MaskedTextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.btnLTitr = New System.Windows.Forms.Button()
        Me.btnFTitr = New System.Windows.Forms.Button()
        Me.btnErsal = New System.Windows.Forms.Button()
        Me.btnDeleteTitr = New System.Windows.Forms.Button()
        Me.btnNewSanad = New System.Windows.Forms.Button()
        Me.btnEditSanad = New System.Windows.Forms.Button()
        Me.btnCancelSanad = New System.Windows.Forms.Button()
        Me.btnSaveSanad = New System.Windows.Forms.Button()
        Me.btnFullDelete = New System.Windows.Forms.Button()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.dbgSatr = New System.Windows.Forms.DataGrid()
        Me.btnLSatr = New System.Windows.Forms.Button()
        Me.btnFSatr = New System.Windows.Forms.Button()
        Me.txtFee = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.txtRjKol = New System.Windows.Forms.TextBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.txtTedadKarton = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txtTedadBasteh = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.txtTedad = New System.Windows.Forms.TextBox()
        Me.lblBed = New System.Windows.Forms.Label()
        Me.cmbVahedKala = New System.Windows.Forms.ComboBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.lblNameKala = New System.Windows.Forms.Label()
        Me.txtCodeKala = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.txtRadif = New System.Windows.Forms.TextBox()
        Me.lblRadif = New System.Windows.Forms.Label()
        Me.gbButton = New System.Windows.Forms.GroupBox()
        Me.btnSaveChange = New System.Windows.Forms.Button()
        Me.btnEditRow = New System.Windows.Forms.Button()
        Me.btnSaveRow = New System.Windows.Forms.Button()
        Me.btnExit = New System.Windows.Forms.Button()
        Me.btnCancelRow = New System.Windows.Forms.Button()
        Me.btnDeleteRow = New System.Windows.Forms.Button()
        Me.btnNewRow = New System.Windows.Forms.Button()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        CType(Me.dbgTitr, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox3.SuspendLayout()
        CType(Me.dbgSatr, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbButton.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.mskTaTarikh)
        Me.GroupBox1.Controls.Add(Me.mskAzTarikh)
        Me.GroupBox1.Controls.Add(Me.Label16)
        Me.GroupBox1.Controls.Add(Me.Label17)
        Me.GroupBox1.Controls.Add(Me.txtShomarehS)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.btnSearch)
        Me.GroupBox1.Location = New System.Drawing.Point(0, -2)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(994, 49)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "جستجو"
        '
        'mskTaTarikh
        '
        Me.mskTaTarikh.AllowPromptAsInput = False
        Me.mskTaTarikh.Location = New System.Drawing.Point(695, 16)
        Me.mskTaTarikh.Mask = "####/##/##"
        Me.mskTaTarikh.Name = "mskTaTarikh"
        Me.mskTaTarikh.PromptChar = Global.Microsoft.VisualBasic.ChrW(32)
        Me.mskTaTarikh.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.mskTaTarikh.Size = New System.Drawing.Size(96, 21)
        Me.mskTaTarikh.TabIndex = 3
        Me.mskTaTarikh.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals
        '
        'mskAzTarikh
        '
        Me.mskAzTarikh.AllowPromptAsInput = False
        Me.mskAzTarikh.Location = New System.Drawing.Point(850, 16)
        Me.mskAzTarikh.Mask = "####/##/##"
        Me.mskAzTarikh.Name = "mskAzTarikh"
        Me.mskAzTarikh.PromptChar = Global.Microsoft.VisualBasic.ChrW(32)
        Me.mskAzTarikh.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.mskAzTarikh.Size = New System.Drawing.Size(96, 21)
        Me.mskAzTarikh.TabIndex = 1
        Me.mskAzTarikh.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(790, 20)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(44, 13)
        Me.Label16.TabIndex = 2
        Me.Label16.Text = "تا تاريخ :"
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(949, 20)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(45, 13)
        Me.Label17.TabIndex = 0
        Me.Label17.Text = "از تاريخ :"
        '
        'txtShomarehS
        '
        Me.txtShomarehS.Location = New System.Drawing.Point(552, 16)
        Me.txtShomarehS.MaxLength = 5
        Me.txtShomarehS.Name = "txtShomarehS"
        Me.txtShomarehS.Size = New System.Drawing.Size(84, 21)
        Me.txtShomarehS.TabIndex = 5
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(637, 20)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(41, 13)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "شماره:"
        '
        'btnSearch
        '
        Me.btnSearch.Image = CType(resources.GetObject("btnSearch.Image"), System.Drawing.Image)
        Me.btnSearch.Location = New System.Drawing.Point(41, 15)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(30, 28)
        Me.btnSearch.TabIndex = 8
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.dbgTitr)
        Me.GroupBox2.Controls.Add(Me.txtShomarehForm)
        Me.GroupBox2.Controls.Add(Me.Label7)
        Me.GroupBox2.Controls.Add(Me.txtSharh)
        Me.GroupBox2.Controls.Add(Me.Label5)
        Me.GroupBox2.Controls.Add(Me.Label4)
        Me.GroupBox2.Controls.Add(Me.cmbTolidkonandeh)
        Me.GroupBox2.Controls.Add(Me.cmbAnbar)
        Me.GroupBox2.Controls.Add(Me.Label2)
        Me.GroupBox2.Controls.Add(Me.mskTarikh)
        Me.GroupBox2.Controls.Add(Me.Label3)
        Me.GroupBox2.Controls.Add(Me.btnLTitr)
        Me.GroupBox2.Controls.Add(Me.btnFTitr)
        Me.GroupBox2.Location = New System.Drawing.Point(-3, 44)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(995, 261)
        Me.GroupBox2.TabIndex = 2
        Me.GroupBox2.TabStop = False
        '
        'dbgTitr
        '
        Me.dbgTitr.CaptionVisible = False
        Me.dbgTitr.DataMember = ""
        Me.dbgTitr.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.dbgTitr.Location = New System.Drawing.Point(32, 13)
        Me.dbgTitr.Name = "dbgTitr"
        Me.dbgTitr.Size = New System.Drawing.Size(965, 200)
        Me.dbgTitr.TabIndex = 11
        Me.dbgTitr.TabStop = False
        '
        'txtShomarehForm
        '
        Me.txtShomarehForm.Location = New System.Drawing.Point(880, 230)
        Me.txtShomarehForm.MaxLength = 5
        Me.txtShomarehForm.Name = "txtShomarehForm"
        Me.txtShomarehForm.Size = New System.Drawing.Size(69, 21)
        Me.txtShomarehForm.TabIndex = 1
        '
        'Label7
        '
        Me.Label7.Location = New System.Drawing.Point(948, 234)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(46, 13)
        Me.Label7.TabIndex = 0
        Me.Label7.Text = "شماره :"
        '
        'txtSharh
        '
        Me.txtSharh.Location = New System.Drawing.Point(32, 230)
        Me.txtSharh.Name = "txtSharh"
        Me.txtSharh.Size = New System.Drawing.Size(211, 21)
        Me.txtSharh.TabIndex = 9
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(243, 234)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(34, 13)
        Me.Label5.TabIndex = 8
        Me.Label5.Text = "شرح:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(654, 234)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(75, 13)
        Me.Label4.TabIndex = 4
        Me.Label4.Text = "نام تولید کننده:"
        '
        'cmbTolidkonandeh
        '
        Me.cmbTolidkonandeh.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbTolidkonandeh.FormattingEnabled = True
        Me.cmbTolidkonandeh.Location = New System.Drawing.Point(503, 230)
        Me.cmbTolidkonandeh.Name = "cmbTolidkonandeh"
        Me.cmbTolidkonandeh.Size = New System.Drawing.Size(150, 21)
        Me.cmbTolidkonandeh.TabIndex = 5
        '
        'cmbAnbar
        '
        Me.cmbAnbar.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbAnbar.Location = New System.Drawing.Point(279, 230)
        Me.cmbAnbar.MaxDropDownItems = 20
        Me.cmbAnbar.Name = "cmbAnbar"
        Me.cmbAnbar.Size = New System.Drawing.Size(170, 21)
        Me.cmbAnbar.TabIndex = 7
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(449, 234)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(44, 13)
        Me.Label2.TabIndex = 6
        Me.Label2.Text = "نام انبار:"
        '
        'mskTarikh
        '
        Me.mskTarikh.AllowPromptAsInput = False
        Me.mskTarikh.Location = New System.Drawing.Point(737, 230)
        Me.mskTarikh.Mask = "####/##/##"
        Me.mskTarikh.Name = "mskTarikh"
        Me.mskTarikh.PromptChar = Global.Microsoft.VisualBasic.ChrW(32)
        Me.mskTarikh.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.mskTarikh.Size = New System.Drawing.Size(91, 21)
        Me.mskTarikh.TabIndex = 3
        Me.mskTarikh.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(827, 234)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(38, 13)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "تاريخ  :"
        '
        'btnLTitr
        '
        Me.btnLTitr.Location = New System.Drawing.Point(3, 190)
        Me.btnLTitr.Name = "btnLTitr"
        Me.btnLTitr.Size = New System.Drawing.Size(23, 23)
        Me.btnLTitr.TabIndex = 11
        Me.btnLTitr.TabStop = False
        '
        'btnFTitr
        '
        Me.btnFTitr.Location = New System.Drawing.Point(3, 13)
        Me.btnFTitr.Name = "btnFTitr"
        Me.btnFTitr.Size = New System.Drawing.Size(23, 23)
        Me.btnFTitr.TabIndex = 10
        Me.btnFTitr.TabStop = False
        '
        'btnErsal
        '
        Me.btnErsal.AccessibleDescription = ""
        Me.btnErsal.AccessibleName = ""
        Me.btnErsal.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnErsal.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnErsal.Location = New System.Drawing.Point(751, 311)
        Me.btnErsal.Name = "btnErsal"
        Me.btnErsal.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.btnErsal.Size = New System.Drawing.Size(104, 23)
        Me.btnErsal.TabIndex = 16
        Me.btnErsal.Text = "&ارسال"
        '
        'btnDeleteTitr
        '
        Me.btnDeleteTitr.AccessibleDescription = ""
        Me.btnDeleteTitr.AccessibleName = ""
        Me.btnDeleteTitr.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDeleteTitr.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnDeleteTitr.Location = New System.Drawing.Point(319, 310)
        Me.btnDeleteTitr.Name = "btnDeleteTitr"
        Me.btnDeleteTitr.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.btnDeleteTitr.Size = New System.Drawing.Size(104, 23)
        Me.btnDeleteTitr.TabIndex = 15
        Me.btnDeleteTitr.Text = "&حذف"
        '
        'btnNewSanad
        '
        Me.btnNewSanad.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnNewSanad.Location = New System.Drawing.Point(441, 310)
        Me.btnNewSanad.Name = "btnNewSanad"
        Me.btnNewSanad.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.btnNewSanad.Size = New System.Drawing.Size(104, 23)
        Me.btnNewSanad.TabIndex = 13
        Me.btnNewSanad.Text = "&جديد"
        '
        'btnEditSanad
        '
        Me.btnEditSanad.AccessibleDescription = ""
        Me.btnEditSanad.AccessibleName = ""
        Me.btnEditSanad.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnEditSanad.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnEditSanad.Location = New System.Drawing.Point(566, 310)
        Me.btnEditSanad.Name = "btnEditSanad"
        Me.btnEditSanad.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.btnEditSanad.Size = New System.Drawing.Size(104, 23)
        Me.btnEditSanad.TabIndex = 11
        Me.btnEditSanad.Text = "&ويرايش"
        '
        'btnCancelSanad
        '
        Me.btnCancelSanad.AccessibleDescription = ""
        Me.btnCancelSanad.AccessibleName = ""
        Me.btnCancelSanad.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnCancelSanad.Location = New System.Drawing.Point(389, 310)
        Me.btnCancelSanad.Name = "btnCancelSanad"
        Me.btnCancelSanad.Size = New System.Drawing.Size(104, 23)
        Me.btnCancelSanad.TabIndex = 14
        Me.btnCancelSanad.Text = "&صرفنظر"
        '
        'btnSaveSanad
        '
        Me.btnSaveSanad.AccessibleDescription = ""
        Me.btnSaveSanad.AccessibleName = ""
        Me.btnSaveSanad.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnSaveSanad.Location = New System.Drawing.Point(493, 310)
        Me.btnSaveSanad.Name = "btnSaveSanad"
        Me.btnSaveSanad.Size = New System.Drawing.Size(104, 23)
        Me.btnSaveSanad.TabIndex = 12
        Me.btnSaveSanad.Text = "&ذخيره"
        '
        'btnFullDelete
        '
        Me.btnFullDelete.AccessibleDescription = ""
        Me.btnFullDelete.AccessibleName = ""
        Me.btnFullDelete.BackColor = System.Drawing.Color.Transparent
        Me.btnFullDelete.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnFullDelete.ForeColor = System.Drawing.Color.Maroon
        Me.btnFullDelete.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnFullDelete.Location = New System.Drawing.Point(861, 310)
        Me.btnFullDelete.Name = "btnFullDelete"
        Me.btnFullDelete.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.btnFullDelete.Size = New System.Drawing.Size(128, 23)
        Me.btnFullDelete.TabIndex = 10
        Me.btnFullDelete.Text = "حذف فرم"
        Me.btnFullDelete.UseVisualStyleBackColor = False
        Me.btnFullDelete.Visible = False
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.dbgSatr)
        Me.GroupBox3.Controls.Add(Me.btnLSatr)
        Me.GroupBox3.Controls.Add(Me.btnFSatr)
        Me.GroupBox3.Controls.Add(Me.txtFee)
        Me.GroupBox3.Controls.Add(Me.Label12)
        Me.GroupBox3.Controls.Add(Me.txtRjKol)
        Me.GroupBox3.Controls.Add(Me.Label14)
        Me.GroupBox3.Controls.Add(Me.txtTedadKarton)
        Me.GroupBox3.Controls.Add(Me.Label8)
        Me.GroupBox3.Controls.Add(Me.txtTedadBasteh)
        Me.GroupBox3.Controls.Add(Me.Label9)
        Me.GroupBox3.Controls.Add(Me.txtTedad)
        Me.GroupBox3.Controls.Add(Me.lblBed)
        Me.GroupBox3.Controls.Add(Me.cmbVahedKala)
        Me.GroupBox3.Controls.Add(Me.Label10)
        Me.GroupBox3.Controls.Add(Me.lblNameKala)
        Me.GroupBox3.Controls.Add(Me.txtCodeKala)
        Me.GroupBox3.Controls.Add(Me.Label11)
        Me.GroupBox3.Controls.Add(Me.txtRadif)
        Me.GroupBox3.Controls.Add(Me.lblRadif)
        Me.GroupBox3.Location = New System.Drawing.Point(0, 340)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(994, 298)
        Me.GroupBox3.TabIndex = 17
        Me.GroupBox3.TabStop = False
        '
        'dbgSatr
        '
        Me.dbgSatr.CaptionVisible = False
        Me.dbgSatr.DataMember = ""
        Me.dbgSatr.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.dbgSatr.Location = New System.Drawing.Point(29, 20)
        Me.dbgSatr.Name = "dbgSatr"
        Me.dbgSatr.Size = New System.Drawing.Size(965, 211)
        Me.dbgSatr.TabIndex = 11
        Me.dbgSatr.TabStop = False
        '
        'btnLSatr
        '
        Me.btnLSatr.Location = New System.Drawing.Point(2, 209)
        Me.btnLSatr.Name = "btnLSatr"
        Me.btnLSatr.Size = New System.Drawing.Size(23, 23)
        Me.btnLSatr.TabIndex = 11
        Me.btnLSatr.TabStop = False
        '
        'btnFSatr
        '
        Me.btnFSatr.Location = New System.Drawing.Point(3, 13)
        Me.btnFSatr.Name = "btnFSatr"
        Me.btnFSatr.Size = New System.Drawing.Size(23, 23)
        Me.btnFSatr.TabIndex = 10
        Me.btnFSatr.TabStop = False
        '
        'txtFee
        '
        Me.txtFee.Location = New System.Drawing.Point(194, 238)
        Me.txtFee.Name = "txtFee"
        Me.txtFee.Size = New System.Drawing.Size(69, 21)
        Me.txtFee.TabIndex = 28
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Label12.Location = New System.Drawing.Point(269, 245)
        Me.Label12.Name = "Label12"
        Me.Label12.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Label12.Size = New System.Drawing.Size(27, 13)
        Me.Label12.TabIndex = 27
        Me.Label12.Text = "فی:"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtRjKol
        '
        Me.txtRjKol.Location = New System.Drawing.Point(42, 239)
        Me.txtRjKol.Name = "txtRjKol"
        Me.txtRjKol.Size = New System.Drawing.Size(69, 21)
        Me.txtRjKol.TabIndex = 26
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Label14.Location = New System.Drawing.Point(117, 242)
        Me.Label14.Name = "Label14"
        Me.Label14.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Label14.Size = New System.Drawing.Size(53, 13)
        Me.Label14.TabIndex = 25
        Me.Label14.Text = "قیمت کل:"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtTedadKarton
        '
        Me.txtTedadKarton.Location = New System.Drawing.Point(194, 265)
        Me.txtTedadKarton.Name = "txtTedadKarton"
        Me.txtTedadKarton.Size = New System.Drawing.Size(69, 21)
        Me.txtTedadKarton.TabIndex = 24
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Label8.Location = New System.Drawing.Point(267, 270)
        Me.Label8.Name = "Label8"
        Me.Label8.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Label8.Size = New System.Drawing.Size(60, 13)
        Me.Label8.TabIndex = 23
        Me.Label8.Text = "تعداد کارتن:"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtTedadBasteh
        '
        Me.txtTedadBasteh.Location = New System.Drawing.Point(359, 264)
        Me.txtTedadBasteh.Name = "txtTedadBasteh"
        Me.txtTedadBasteh.Size = New System.Drawing.Size(69, 21)
        Me.txtTedadBasteh.TabIndex = 22
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Label9.Location = New System.Drawing.Point(434, 268)
        Me.Label9.Name = "Label9"
        Me.Label9.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Label9.Size = New System.Drawing.Size(62, 13)
        Me.Label9.TabIndex = 21
        Me.Label9.Text = "تعداد بسته:"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtTedad
        '
        Me.txtTedad.Location = New System.Drawing.Point(359, 240)
        Me.txtTedad.Name = "txtTedad"
        Me.txtTedad.Size = New System.Drawing.Size(69, 21)
        Me.txtTedad.TabIndex = 20
        '
        'lblBed
        '
        Me.lblBed.AutoSize = True
        Me.lblBed.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.lblBed.Location = New System.Drawing.Point(434, 244)
        Me.lblBed.Name = "lblBed"
        Me.lblBed.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.lblBed.Size = New System.Drawing.Size(34, 13)
        Me.lblBed.TabIndex = 19
        Me.lblBed.Text = "تعداد:"
        Me.lblBed.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'cmbVahedKala
        '
        Me.cmbVahedKala.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbVahedKala.Enabled = False
        Me.cmbVahedKala.Location = New System.Drawing.Point(785, 265)
        Me.cmbVahedKala.MaxDropDownItems = 20
        Me.cmbVahedKala.Name = "cmbVahedKala"
        Me.cmbVahedKala.Size = New System.Drawing.Size(99, 21)
        Me.cmbVahedKala.TabIndex = 18
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(889, 269)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(50, 13)
        Me.Label10.TabIndex = 17
        Me.Label10.Text = "واحد کالا:"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblNameKala
        '
        Me.lblNameKala.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblNameKala.Location = New System.Drawing.Point(491, 240)
        Me.lblNameKala.Name = "lblNameKala"
        Me.lblNameKala.Size = New System.Drawing.Size(247, 22)
        Me.lblNameKala.TabIndex = 16
        '
        'txtCodeKala
        '
        Me.txtCodeKala.Location = New System.Drawing.Point(742, 240)
        Me.txtCodeKala.Name = "txtCodeKala"
        Me.txtCodeKala.Size = New System.Drawing.Size(99, 21)
        Me.txtCodeKala.TabIndex = 15
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Label11.Location = New System.Drawing.Point(845, 244)
        Me.Label11.Name = "Label11"
        Me.Label11.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Label11.Size = New System.Drawing.Size(42, 13)
        Me.Label11.TabIndex = 14
        Me.Label11.Text = "نام کالا:"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtRadif
        '
        Me.txtRadif.BackColor = System.Drawing.Color.White
        Me.txtRadif.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.txtRadif.ForeColor = System.Drawing.Color.Black
        Me.txtRadif.Location = New System.Drawing.Point(906, 239)
        Me.txtRadif.MaxLength = 5
        Me.txtRadif.Name = "txtRadif"
        Me.txtRadif.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txtRadif.Size = New System.Drawing.Size(48, 21)
        Me.txtRadif.TabIndex = 13
        '
        'lblRadif
        '
        Me.lblRadif.AutoSize = True
        Me.lblRadif.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.lblRadif.Location = New System.Drawing.Point(953, 243)
        Me.lblRadif.Name = "lblRadif"
        Me.lblRadif.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.lblRadif.Size = New System.Drawing.Size(33, 13)
        Me.lblRadif.TabIndex = 12
        Me.lblRadif.Text = "رديف:"
        Me.lblRadif.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'gbButton
        '
        Me.gbButton.Controls.Add(Me.btnSaveChange)
        Me.gbButton.Controls.Add(Me.btnEditRow)
        Me.gbButton.Controls.Add(Me.btnSaveRow)
        Me.gbButton.Controls.Add(Me.btnExit)
        Me.gbButton.Controls.Add(Me.btnCancelRow)
        Me.gbButton.Controls.Add(Me.btnDeleteRow)
        Me.gbButton.Controls.Add(Me.btnNewRow)
        Me.gbButton.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.gbButton.Location = New System.Drawing.Point(0, 638)
        Me.gbButton.Name = "gbButton"
        Me.gbButton.Size = New System.Drawing.Size(994, 47)
        Me.gbButton.TabIndex = 18
        Me.gbButton.TabStop = False
        '
        'btnSaveChange
        '
        Me.btnSaveChange.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSaveChange.Image = CType(resources.GetObject("btnSaveChange.Image"), System.Drawing.Image)
        Me.btnSaveChange.Location = New System.Drawing.Point(3, 15)
        Me.btnSaveChange.Name = "btnSaveChange"
        Me.btnSaveChange.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.btnSaveChange.Size = New System.Drawing.Size(26, 24)
        Me.btnSaveChange.TabIndex = 6
        '
        'btnEditRow
        '
        Me.btnEditRow.AccessibleDescription = ""
        Me.btnEditRow.AccessibleName = ""
        Me.btnEditRow.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnEditRow.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnEditRow.Location = New System.Drawing.Point(695, 15)
        Me.btnEditRow.Name = "btnEditRow"
        Me.btnEditRow.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.btnEditRow.Size = New System.Drawing.Size(86, 25)
        Me.btnEditRow.TabIndex = 2
        Me.btnEditRow.Text = "ويرا&يش"
        '
        'btnSaveRow
        '
        Me.btnSaveRow.AccessibleDescription = ""
        Me.btnSaveRow.AccessibleName = ""
        Me.btnSaveRow.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnSaveRow.Location = New System.Drawing.Point(593, 15)
        Me.btnSaveRow.Name = "btnSaveRow"
        Me.btnSaveRow.Size = New System.Drawing.Size(86, 25)
        Me.btnSaveRow.TabIndex = 3
        Me.btnSaveRow.Text = "ذ&خيره"
        '
        'btnExit
        '
        Me.btnExit.AccessibleDescription = ""
        Me.btnExit.AccessibleName = ""
        Me.btnExit.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnExit.Location = New System.Drawing.Point(389, 15)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(86, 25)
        Me.btnExit.TabIndex = 5
        Me.btnExit.Text = "خرو&ج"
        '
        'btnCancelRow
        '
        Me.btnCancelRow.AccessibleDescription = ""
        Me.btnCancelRow.AccessibleName = ""
        Me.btnCancelRow.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnCancelRow.Location = New System.Drawing.Point(491, 15)
        Me.btnCancelRow.Name = "btnCancelRow"
        Me.btnCancelRow.Size = New System.Drawing.Size(86, 25)
        Me.btnCancelRow.TabIndex = 4
        Me.btnCancelRow.Text = "ص&رفنظر"
        '
        'btnDeleteRow
        '
        Me.btnDeleteRow.AccessibleDescription = ""
        Me.btnDeleteRow.AccessibleName = ""
        Me.btnDeleteRow.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnDeleteRow.Location = New System.Drawing.Point(797, 15)
        Me.btnDeleteRow.Name = "btnDeleteRow"
        Me.btnDeleteRow.Size = New System.Drawing.Size(86, 25)
        Me.btnDeleteRow.TabIndex = 1
        Me.btnDeleteRow.Text = "حذ&ف"
        '
        'btnNewRow
        '
        Me.btnNewRow.AccessibleDescription = ""
        Me.btnNewRow.AccessibleName = ""
        Me.btnNewRow.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnNewRow.Location = New System.Drawing.Point(899, 15)
        Me.btnNewRow.Name = "btnNewRow"
        Me.btnNewRow.Size = New System.Drawing.Size(86, 25)
        Me.btnNewRow.TabIndex = 0
        Me.btnNewRow.Text = "ر&ديف جديد"
        '
        'An_KhorojMavadAvalieh
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(994, 685)
        Me.Controls.Add(Me.gbButton)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.btnErsal)
        Me.Controls.Add(Me.btnDeleteTitr)
        Me.Controls.Add(Me.btnNewSanad)
        Me.Controls.Add(Me.btnEditSanad)
        Me.Controls.Add(Me.btnCancelSanad)
        Me.Controls.Add(Me.btnSaveSanad)
        Me.Controls.Add(Me.btnFullDelete)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.Name = "An_KhorojMavadAvalieh"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "خروج مواد اولیه"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        CType(Me.dbgTitr, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        CType(Me.dbgSatr, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbButton.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents mskTaTarikh As System.Windows.Forms.MaskedTextBox
    Friend WithEvents mskAzTarikh As System.Windows.Forms.MaskedTextBox
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents txtShomarehS As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnSearch As System.Windows.Forms.Button
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents dbgTitr As System.Windows.Forms.DataGrid
    Friend WithEvents txtShomarehForm As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtSharh As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents cmbTolidkonandeh As System.Windows.Forms.ComboBox
    Friend WithEvents cmbAnbar As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents mskTarikh As System.Windows.Forms.MaskedTextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents btnLTitr As System.Windows.Forms.Button
    Friend WithEvents btnFTitr As System.Windows.Forms.Button
    Friend WithEvents btnErsal As System.Windows.Forms.Button
    Friend WithEvents btnDeleteTitr As System.Windows.Forms.Button
    Friend WithEvents btnNewSanad As System.Windows.Forms.Button
    Friend WithEvents btnEditSanad As System.Windows.Forms.Button
    Friend WithEvents btnCancelSanad As System.Windows.Forms.Button
    Friend WithEvents btnSaveSanad As System.Windows.Forms.Button
    Friend WithEvents btnFullDelete As System.Windows.Forms.Button
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents dbgSatr As System.Windows.Forms.DataGrid
    Friend WithEvents btnLSatr As System.Windows.Forms.Button
    Friend WithEvents btnFSatr As System.Windows.Forms.Button
    Friend WithEvents lblNameKala As System.Windows.Forms.Label
    Friend WithEvents txtCodeKala As System.Windows.Forms.TextBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents txtRadif As System.Windows.Forms.TextBox
    Friend WithEvents lblRadif As System.Windows.Forms.Label
    Friend WithEvents txtTedadBasteh As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents txtTedad As System.Windows.Forms.TextBox
    Friend WithEvents lblBed As System.Windows.Forms.Label
    Friend WithEvents cmbVahedKala As System.Windows.Forms.ComboBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents txtTedadKarton As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents gbButton As System.Windows.Forms.GroupBox
    Friend WithEvents btnSaveChange As System.Windows.Forms.Button
    Friend WithEvents btnEditRow As System.Windows.Forms.Button
    Friend WithEvents btnSaveRow As System.Windows.Forms.Button
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents btnCancelRow As System.Windows.Forms.Button
    Friend WithEvents btnDeleteRow As System.Windows.Forms.Button
    Friend WithEvents btnNewRow As System.Windows.Forms.Button
    Friend WithEvents txtFee As System.Windows.Forms.TextBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents txtRjKol As System.Windows.Forms.TextBox
    Friend WithEvents Label14 As System.Windows.Forms.Label

End Class
