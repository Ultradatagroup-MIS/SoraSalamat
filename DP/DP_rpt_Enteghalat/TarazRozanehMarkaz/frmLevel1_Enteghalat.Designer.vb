<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmLevel1_Enteghalat
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmLevel1_Enteghalat))
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.tsbtnClose = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsbtnPrint = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripLabel2 = New System.Windows.Forms.ToolStripLabel()
        Me.tstxtMoshtary = New System.Windows.Forms.ToolStripTextBox()
        Me.ToolStripLabel1 = New System.Windows.Forms.ToolStripLabel()
        Me.txtMablagh = New System.Windows.Forms.ToolStripTextBox()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.dbgTitr = New System.Windows.Forms.DataGrid()
        Me.ToolStrip1.SuspendLayout()
        CType(Me.dbgTitr, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsbtnClose, Me.ToolStripSeparator2, Me.tsbtnPrint, Me.ToolStripSeparator1, Me.ToolStripLabel2, Me.tstxtMoshtary, Me.ToolStripLabel1, Me.txtMablagh})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(984, 35)
        Me.ToolStrip1.TabIndex = 1
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'tsbtnClose
        '
        Me.tsbtnClose.AutoSize = False
        Me.tsbtnClose.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tsbtnClose.Image = CType(resources.GetObject("tsbtnClose.Image"), System.Drawing.Image)
        Me.tsbtnClose.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.tsbtnClose.Name = "tsbtnClose"
        Me.tsbtnClose.Size = New System.Drawing.Size(32, 32)
        Me.tsbtnClose.Text = "ÎÑæÌ"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(6, 35)
        '
        'tsbtnPrint
        '
        Me.tsbtnPrint.AutoSize = False
        Me.tsbtnPrint.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tsbtnPrint.Image = CType(resources.GetObject("tsbtnPrint.Image"), System.Drawing.Image)
        Me.tsbtnPrint.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.tsbtnPrint.Name = "tsbtnPrint"
        Me.tsbtnPrint.Size = New System.Drawing.Size(32, 32)
        Me.tsbtnPrint.Text = "Ç"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 35)
        '
        'ToolStripLabel2
        '
        Me.ToolStripLabel2.Name = "ToolStripLabel2"
        Me.ToolStripLabel2.Size = New System.Drawing.Size(47, 32)
        Me.ToolStripLabel2.Text = "مشتری :"
        '
        'tstxtMoshtary
        '
        Me.tstxtMoshtary.AutoSize = False
        Me.tstxtMoshtary.Name = "tstxtMoshtary"
        Me.tstxtMoshtary.Size = New System.Drawing.Size(250, 35)
        '
        'ToolStripLabel1
        '
        Me.ToolStripLabel1.Name = "ToolStripLabel1"
        Me.ToolStripLabel1.Size = New System.Drawing.Size(32, 32)
        Me.ToolStripLabel1.Text = "مبلغ :"
        '
        'txtMablagh
        '
        Me.txtMablagh.Name = "txtMablagh"
        Me.txtMablagh.Size = New System.Drawing.Size(200, 35)
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Dock = System.Windows.Forms.DockStyle.Top
        Me.StatusStrip1.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 35)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.StatusStrip1.Size = New System.Drawing.Size(984, 22)
        Me.StatusStrip1.TabIndex = 2
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'dbgTitr
        '
        Me.dbgTitr.AllowSorting = False
        Me.dbgTitr.AlternatingBackColor = System.Drawing.Color.LemonChiffon
        Me.dbgTitr.BackColor = System.Drawing.Color.Maroon
        Me.dbgTitr.BackgroundColor = System.Drawing.Color.Gray
        Me.dbgTitr.CaptionBackColor = System.Drawing.Color.Maroon
        Me.dbgTitr.DataMember = ""
        Me.dbgTitr.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.dbgTitr.HeaderBackColor = System.Drawing.Color.Gray
        Me.dbgTitr.HeaderFont = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.dbgTitr.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.dbgTitr.LinkColor = System.Drawing.Color.Gray
        Me.dbgTitr.Location = New System.Drawing.Point(-3, 60)
        Me.dbgTitr.Name = "dbgTitr"
        Me.dbgTitr.ReadOnly = True
        Me.dbgTitr.SelectionBackColor = System.Drawing.Color.Maroon
        Me.dbgTitr.Size = New System.Drawing.Size(990, 692)
        Me.dbgTitr.TabIndex = 3
        '
        'frmLevel1_Enteghalat
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(984, 750)
        Me.Controls.Add(Me.dbgTitr)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Name = "frmLevel1_Enteghalat"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Text = "دفتر انتقالات"
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        CType(Me.dbgTitr, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents tsbtnClose As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents tsbtnPrint As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripLabel2 As System.Windows.Forms.ToolStripLabel
    Friend WithEvents tstxtMoshtary As System.Windows.Forms.ToolStripTextBox
    Friend WithEvents ToolStripLabel1 As System.Windows.Forms.ToolStripLabel
    Friend WithEvents txtMablagh As System.Windows.Forms.ToolStripTextBox
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents dbgTitr As System.Windows.Forms.DataGrid
End Class
