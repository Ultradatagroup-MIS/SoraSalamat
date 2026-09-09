<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmFO_Line
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
        Me.lbKala = New System.Windows.Forms.ListBox()
        Me.grbKala = New System.Windows.Forms.GroupBox()
        Me.lblKala = New System.Windows.Forms.Label()
        Me.lblMoshtary = New System.Windows.Forms.Label()
        Me.lbMoshtary = New System.Windows.Forms.ListBox()
        Me.txtSearch = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cmbNoeSatr = New System.Windows.Forms.ComboBox()
        Me.grbMoshtary = New System.Windows.Forms.GroupBox()
        Me.lblForoshandeh = New System.Windows.Forms.Label()
        Me.lbForoshandeh = New System.Windows.Forms.ListBox()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.grbLineSatr = New System.Windows.Forms.GroupBox()
        Me.grbForoshandeh = New System.Windows.Forms.GroupBox()
        Me.btnExit = New System.Windows.Forms.Button()
        Me.btnAddSatr = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.btnRemoveTitr = New System.Windows.Forms.Button()
        Me.btnAddTitr = New System.Windows.Forms.Button()
        Me.lbLine = New System.Windows.Forms.ListBox()
        Me.grbLine = New System.Windows.Forms.GroupBox()
        Me.grbKala.SuspendLayout()
        Me.grbMoshtary.SuspendLayout()
        Me.grbLineSatr.SuspendLayout()
        Me.grbForoshandeh.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.grbLine.SuspendLayout()
        Me.SuspendLayout()
        '
        'lbKala
        '
        Me.lbKala.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbKala.FormattingEnabled = True
        Me.lbKala.Location = New System.Drawing.Point(3, 17)
        Me.lbKala.Name = "lbKala"
        Me.lbKala.Size = New System.Drawing.Size(268, 264)
        Me.lbKala.TabIndex = 1
        '
        'grbKala
        '
        Me.grbKala.Controls.Add(Me.lblKala)
        Me.grbKala.Controls.Add(Me.lbKala)
        Me.grbKala.Location = New System.Drawing.Point(562, 47)
        Me.grbKala.Name = "grbKala"
        Me.grbKala.Size = New System.Drawing.Size(274, 310)
        Me.grbKala.TabIndex = 8
        Me.grbKala.TabStop = False
        Me.grbKala.Text = " کالاها ( نام کالا - کد کالا) "
        '
        'lblKala
        '
        Me.lblKala.AutoSize = True
        Me.lblKala.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblKala.Location = New System.Drawing.Point(17, 288)
        Me.lblKala.Name = "lblKala"
        Me.lblKala.Size = New System.Drawing.Size(244, 13)
        Me.lblKala.TabIndex = 5
        Me.lblKala.Text = "جهت حـذف بر روی کالای مورد نظر دبل کلیک نمایید ."
        '
        'lblMoshtary
        '
        Me.lblMoshtary.AutoSize = True
        Me.lblMoshtary.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblMoshtary.Location = New System.Drawing.Point(9, 288)
        Me.lblMoshtary.Name = "lblMoshtary"
        Me.lblMoshtary.Size = New System.Drawing.Size(256, 13)
        Me.lblMoshtary.TabIndex = 5
        Me.lblMoshtary.Text = "جهت حـذف بر روی مشتری مورد نظر دبل کلیک نمایید ."
        '
        'lbMoshtary
        '
        Me.lbMoshtary.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbMoshtary.FormattingEnabled = True
        Me.lbMoshtary.Location = New System.Drawing.Point(3, 17)
        Me.lbMoshtary.Name = "lbMoshtary"
        Me.lbMoshtary.Size = New System.Drawing.Size(268, 264)
        Me.lbMoshtary.TabIndex = 1
        '
        'txtSearch
        '
        Me.txtSearch.Location = New System.Drawing.Point(249, 18)
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.Size = New System.Drawing.Size(271, 21)
        Me.txtSearch.TabIndex = 7
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(526, 23)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(81, 13)
        Me.Label2.TabIndex = 5
        Me.Label2.Text = "کلمه مورد نظـر :"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(747, 23)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(88, 13)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "نـــــــوع جستجو :"
        '
        'cmbNoeSatr
        '
        Me.cmbNoeSatr.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbNoeSatr.FormattingEnabled = True
        Me.cmbNoeSatr.Location = New System.Drawing.Point(611, 20)
        Me.cmbNoeSatr.Name = "cmbNoeSatr"
        Me.cmbNoeSatr.Size = New System.Drawing.Size(130, 21)
        Me.cmbNoeSatr.TabIndex = 3
        '
        'grbMoshtary
        '
        Me.grbMoshtary.Controls.Add(Me.lblMoshtary)
        Me.grbMoshtary.Controls.Add(Me.lbMoshtary)
        Me.grbMoshtary.Location = New System.Drawing.Point(283, 47)
        Me.grbMoshtary.Name = "grbMoshtary"
        Me.grbMoshtary.Size = New System.Drawing.Size(274, 310)
        Me.grbMoshtary.TabIndex = 9
        Me.grbMoshtary.TabStop = False
        Me.grbMoshtary.Text = " مشتـریان ( نام مشتری - کد مشتری ) "
        '
        'lblForoshandeh
        '
        Me.lblForoshandeh.AutoSize = True
        Me.lblForoshandeh.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblForoshandeh.Location = New System.Drawing.Point(7, 288)
        Me.lblForoshandeh.Name = "lblForoshandeh"
        Me.lblForoshandeh.Size = New System.Drawing.Size(260, 13)
        Me.lblForoshandeh.TabIndex = 5
        Me.lblForoshandeh.Text = "جهت حـذف بر روی فروشنده مورد نظر دبل کلیک نمایید ."
        '
        'lbForoshandeh
        '
        Me.lbForoshandeh.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbForoshandeh.FormattingEnabled = True
        Me.lbForoshandeh.Location = New System.Drawing.Point(3, 17)
        Me.lbForoshandeh.Name = "lbForoshandeh"
        Me.lbForoshandeh.Size = New System.Drawing.Size(268, 264)
        Me.lbForoshandeh.TabIndex = 1
        '
        'btnCancel
        '
        Me.btnCancel.Location = New System.Drawing.Point(151, 15)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(82, 28)
        Me.btnCancel.TabIndex = 11
        Me.btnCancel.Text = "بازخوانی"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'grbLineSatr
        '
        Me.grbLineSatr.Controls.Add(Me.btnCancel)
        Me.grbLineSatr.Controls.Add(Me.grbForoshandeh)
        Me.grbLineSatr.Controls.Add(Me.txtSearch)
        Me.grbLineSatr.Controls.Add(Me.Label2)
        Me.grbLineSatr.Controls.Add(Me.Label1)
        Me.grbLineSatr.Controls.Add(Me.cmbNoeSatr)
        Me.grbLineSatr.Controls.Add(Me.grbMoshtary)
        Me.grbLineSatr.Controls.Add(Me.grbKala)
        Me.grbLineSatr.Location = New System.Drawing.Point(236, 1)
        Me.grbLineSatr.Name = "grbLineSatr"
        Me.grbLineSatr.Size = New System.Drawing.Size(842, 364)
        Me.grbLineSatr.TabIndex = 8
        Me.grbLineSatr.TabStop = False
        Me.grbLineSatr.Text = "جزئیات لاین "
        '
        'grbForoshandeh
        '
        Me.grbForoshandeh.Controls.Add(Me.lblForoshandeh)
        Me.grbForoshandeh.Controls.Add(Me.lbForoshandeh)
        Me.grbForoshandeh.Location = New System.Drawing.Point(5, 47)
        Me.grbForoshandeh.Name = "grbForoshandeh"
        Me.grbForoshandeh.Size = New System.Drawing.Size(274, 310)
        Me.grbForoshandeh.TabIndex = 10
        Me.grbForoshandeh.TabStop = False
        Me.grbForoshandeh.Text = " فروشنـدگان ( نام فروشنده - کد فروشنده ) "
        '
        'btnExit
        '
        Me.btnExit.Location = New System.Drawing.Point(11, 16)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(101, 28)
        Me.btnExit.TabIndex = 0
        Me.btnExit.Text = "خــــروج"
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'btnAddSatr
        '
        Me.btnAddSatr.Location = New System.Drawing.Point(118, 16)
        Me.btnAddSatr.Name = "btnAddSatr"
        Me.btnAddSatr.Size = New System.Drawing.Size(171, 28)
        Me.btnAddSatr.TabIndex = 2
        Me.btnAddSatr.Text = "افــزودن کالا ، مشتری ، فروشنده"
        Me.btnAddSatr.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.btnExit)
        Me.GroupBox1.Controls.Add(Me.btnAddSatr)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.GroupBox1.Location = New System.Drawing.Point(0, 366)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(1081, 51)
        Me.GroupBox1.TabIndex = 7
        Me.GroupBox1.TabStop = False
        '
        'btnRemoveTitr
        '
        Me.btnRemoveTitr.Location = New System.Drawing.Point(13, 327)
        Me.btnRemoveTitr.Name = "btnRemoveTitr"
        Me.btnRemoveTitr.Size = New System.Drawing.Size(101, 28)
        Me.btnRemoveTitr.TabIndex = 3
        Me.btnRemoveTitr.Text = "حـــــذف"
        Me.btnRemoveTitr.UseVisualStyleBackColor = True
        '
        'btnAddTitr
        '
        Me.btnAddTitr.Location = New System.Drawing.Point(116, 327)
        Me.btnAddTitr.Name = "btnAddTitr"
        Me.btnAddTitr.Size = New System.Drawing.Size(101, 28)
        Me.btnAddTitr.TabIndex = 2
        Me.btnAddTitr.Text = "افــزودن"
        Me.btnAddTitr.UseVisualStyleBackColor = True
        '
        'lbLine
        '
        Me.lbLine.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbLine.FormattingEnabled = True
        Me.lbLine.Location = New System.Drawing.Point(3, 17)
        Me.lbLine.Name = "lbLine"
        Me.lbLine.Size = New System.Drawing.Size(224, 303)
        Me.lbLine.TabIndex = 1
        '
        'grbLine
        '
        Me.grbLine.Controls.Add(Me.btnRemoveTitr)
        Me.grbLine.Controls.Add(Me.btnAddTitr)
        Me.grbLine.Controls.Add(Me.lbLine)
        Me.grbLine.Location = New System.Drawing.Point(4, 1)
        Me.grbLine.Name = "grbLine"
        Me.grbLine.Size = New System.Drawing.Size(230, 364)
        Me.grbLine.TabIndex = 6
        Me.grbLine.TabStop = False
        Me.grbLine.Text = " لایـن "
        '
        'frmFO_Line
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1081, 417)
        Me.Controls.Add(Me.grbLineSatr)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.grbLine)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.Name = "frmFO_Line"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.RightToLeftLayout = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "لایـن فـروش"
        Me.grbKala.ResumeLayout(False)
        Me.grbKala.PerformLayout()
        Me.grbMoshtary.ResumeLayout(False)
        Me.grbMoshtary.PerformLayout()
        Me.grbLineSatr.ResumeLayout(False)
        Me.grbLineSatr.PerformLayout()
        Me.grbForoshandeh.ResumeLayout(False)
        Me.grbForoshandeh.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.grbLine.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lbKala As System.Windows.Forms.ListBox
    Friend WithEvents grbKala As System.Windows.Forms.GroupBox
    Friend WithEvents lblKala As System.Windows.Forms.Label
    Friend WithEvents lblMoshtary As System.Windows.Forms.Label
    Friend WithEvents lbMoshtary As System.Windows.Forms.ListBox
    Friend WithEvents txtSearch As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cmbNoeSatr As System.Windows.Forms.ComboBox
    Friend WithEvents grbMoshtary As System.Windows.Forms.GroupBox
    Friend WithEvents lblForoshandeh As System.Windows.Forms.Label
    Friend WithEvents lbForoshandeh As System.Windows.Forms.ListBox
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents grbLineSatr As System.Windows.Forms.GroupBox
    Friend WithEvents grbForoshandeh As System.Windows.Forms.GroupBox
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents btnAddSatr As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents btnRemoveTitr As System.Windows.Forms.Button
    Friend WithEvents btnAddTitr As System.Windows.Forms.Button
    Friend WithEvents lbLine As System.Windows.Forms.ListBox
    Friend WithEvents grbLine As System.Windows.Forms.GroupBox

End Class
