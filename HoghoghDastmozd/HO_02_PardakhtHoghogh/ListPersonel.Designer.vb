<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ListPersonel
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
        Me.cmbMahS = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cmbCodeDorehS = New System.Windows.Forms.ComboBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.btnSearch = New System.Windows.Forms.Button()
        Me.btnAll = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.btnOk = New System.Windows.Forms.Button()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.chkLstPersonel = New System.Windows.Forms.CheckedListBox()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'cmbMahS
        '
        Me.cmbMahS.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbMahS.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.cmbMahS.FormattingEnabled = True
        Me.cmbMahS.Location = New System.Drawing.Point(118, 19)
        Me.cmbMahS.Name = "cmbMahS"
        Me.cmbMahS.Size = New System.Drawing.Size(104, 21)
        Me.cmbMahS.TabIndex = 25
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Label3.Location = New System.Drawing.Point(228, 23)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(25, 13)
        Me.Label3.TabIndex = 24
        Me.Label3.Text = "ماه:"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cmbCodeDorehS
        '
        Me.cmbCodeDorehS.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbCodeDorehS.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.cmbCodeDorehS.FormattingEnabled = True
        Me.cmbCodeDorehS.Location = New System.Drawing.Point(257, 19)
        Me.cmbCodeDorehS.Name = "cmbCodeDorehS"
        Me.cmbCodeDorehS.Size = New System.Drawing.Size(104, 21)
        Me.cmbCodeDorehS.TabIndex = 21
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.Label9.Location = New System.Drawing.Point(367, 23)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(30, 13)
        Me.Label9.TabIndex = 20
        Me.Label9.Text = "دوره:"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnSearch
        '
        Me.btnSearch.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.btnSearch.Location = New System.Drawing.Point(12, 17)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(71, 24)
        Me.btnSearch.TabIndex = 19
        Me.btnSearch.Text = "جستجو"
        '
        'btnAll
        '
        Me.btnAll.AccessibleDescription = ""
        Me.btnAll.AccessibleName = ""
        Me.btnAll.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnAll.Location = New System.Drawing.Point(207, 14)
        Me.btnAll.Name = "btnAll"
        Me.btnAll.Size = New System.Drawing.Size(82, 27)
        Me.btnAll.TabIndex = 26
        Me.btnAll.Text = "انتخاب همه"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.btnOk)
        Me.GroupBox1.Controls.Add(Me.btnAll)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.GroupBox1.Location = New System.Drawing.Point(0, 232)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(404, 47)
        Me.GroupBox1.TabIndex = 28
        Me.GroupBox1.TabStop = False
        '
        'btnOk
        '
        Me.btnOk.AccessibleDescription = ""
        Me.btnOk.AccessibleName = ""
        Me.btnOk.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnOk.Location = New System.Drawing.Point(106, 14)
        Me.btnOk.Name = "btnOk"
        Me.btnOk.Size = New System.Drawing.Size(82, 27)
        Me.btnOk.TabIndex = 27
        Me.btnOk.Text = "تایید"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.cmbCodeDorehS)
        Me.GroupBox2.Controls.Add(Me.btnSearch)
        Me.GroupBox2.Controls.Add(Me.cmbMahS)
        Me.GroupBox2.Controls.Add(Me.Label9)
        Me.GroupBox2.Controls.Add(Me.Label3)
        Me.GroupBox2.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupBox2.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(404, 53)
        Me.GroupBox2.TabIndex = 29
        Me.GroupBox2.TabStop = False
        '
        'chkLstPersonel
        '
        Me.chkLstPersonel.FormattingEnabled = True
        Me.chkLstPersonel.Location = New System.Drawing.Point(2, 59)
        Me.chkLstPersonel.Name = "chkLstPersonel"
        Me.chkLstPersonel.Size = New System.Drawing.Size(400, 169)
        Me.chkLstPersonel.TabIndex = 30
        '
        'ListPersonel
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(404, 279)
        Me.Controls.Add(Me.chkLstPersonel)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Name = "ListPersonel"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "لیست پرسنل"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents cmbMahS As ComboBox
    Friend WithEvents Label3 As Label
    Friend WithEvents cmbCodeDorehS As ComboBox
    Friend WithEvents Label9 As Label
    Friend WithEvents btnSearch As Button
    Friend WithEvents btnAll As Button
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents chkLstPersonel As CheckedListBox
    Friend WithEvents btnOk As Button
End Class
