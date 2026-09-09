<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmHo_MohasebeEydi
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmHo_MohasebeEydi))
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Rb_Sanavat = New System.Windows.Forms.RadioButton()
        Me.btnMohasebe = New System.Windows.Forms.Button()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Rb_Eidy = New System.Windows.Forms.RadioButton()
        Me.DGV = New System.Windows.Forms.DataGridView()
        Me.mskTaTarikh = New System.Windows.Forms.MaskedTextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cmbAfrad = New System.Windows.Forms.ComboBox()
        Me.txtRoozKard = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cmbMarkazPakhsh = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cmbDore = New System.Windows.Forms.ComboBox()
        Me.btnPrint = New System.Windows.Forms.Button()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.BtnSearch = New System.Windows.Forms.Button()
        Me.Rb_SanavatS = New System.Windows.Forms.RadioButton()
        Me.Rb_EidyS = New System.Windows.Forms.RadioButton()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.CmbAfradS = New System.Windows.Forms.ComboBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.CmbMarkazS = New System.Windows.Forms.ComboBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.CmbDoreS = New System.Windows.Forms.ComboBox()
        Me.BtnErsal = New System.Windows.Forms.Button()
        Me.BtnDelete = New System.Windows.Forms.Button()
        Me.chkKoli = New System.Windows.Forms.CheckBox()
        Me.btnSaveChange = New System.Windows.Forms.Button()
        Me.GroupBox1.SuspendLayout()
        CType(Me.DGV, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Rb_Sanavat)
        Me.GroupBox1.Controls.Add(Me.btnMohasebe)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.Rb_Eidy)
        Me.GroupBox1.Controls.Add(Me.DGV)
        Me.GroupBox1.Controls.Add(Me.mskTaTarikh)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.cmbAfrad)
        Me.GroupBox1.Controls.Add(Me.txtRoozKard)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.cmbMarkazPakhsh)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.cmbDore)
        Me.GroupBox1.Location = New System.Drawing.Point(2, 96)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(1054, 348)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "محاسبه"
        '
        'Rb_Sanavat
        '
        Me.Rb_Sanavat.AutoSize = True
        Me.Rb_Sanavat.Location = New System.Drawing.Point(117, 23)
        Me.Rb_Sanavat.Name = "Rb_Sanavat"
        Me.Rb_Sanavat.Size = New System.Drawing.Size(57, 17)
        Me.Rb_Sanavat.TabIndex = 17
        Me.Rb_Sanavat.Text = "سنوات"
        Me.Rb_Sanavat.UseVisualStyleBackColor = True
        '
        'btnMohasebe
        '
        Me.btnMohasebe.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold)
        Me.btnMohasebe.ForeColor = System.Drawing.Color.Maroon
        Me.btnMohasebe.Location = New System.Drawing.Point(10, 20)
        Me.btnMohasebe.Name = "btnMohasebe"
        Me.btnMohasebe.Size = New System.Drawing.Size(88, 30)
        Me.btnMohasebe.TabIndex = 6
        Me.btnMohasebe.Tag = "154213"
        Me.btnMohasebe.Text = "محاسبه "
        Me.btnMohasebe.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.ForeColor = System.Drawing.Color.Red
        Me.Label5.Location = New System.Drawing.Point(346, 25)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(54, 13)
        Me.Label5.TabIndex = 17
        Me.Label5.Text = "روز کارکرد:"
        '
        'Rb_Eidy
        '
        Me.Rb_Eidy.AutoSize = True
        Me.Rb_Eidy.Checked = True
        Me.Rb_Eidy.Location = New System.Drawing.Point(183, 23)
        Me.Rb_Eidy.Name = "Rb_Eidy"
        Me.Rb_Eidy.Size = New System.Drawing.Size(52, 17)
        Me.Rb_Eidy.TabIndex = 16
        Me.Rb_Eidy.TabStop = True
        Me.Rb_Eidy.Text = "عیدی"
        Me.Rb_Eidy.UseVisualStyleBackColor = True
        '
        'DGV
        '
        Me.DGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGV.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.DGV.Location = New System.Drawing.Point(3, 64)
        Me.DGV.Name = "DGV"
        Me.DGV.Size = New System.Drawing.Size(1048, 281)
        Me.DGV.TabIndex = 16
        '
        'mskTaTarikh
        '
        Me.mskTaTarikh.Location = New System.Drawing.Point(403, 22)
        Me.mskTaTarikh.Mask = "####/##/##"
        Me.mskTaTarikh.Name = "mskTaTarikh"
        Me.mskTaTarikh.PromptChar = Global.Microsoft.VisualBasic.ChrW(32)
        Me.mskTaTarikh.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.mskTaTarikh.Size = New System.Drawing.Size(76, 21)
        Me.mskTaTarikh.TabIndex = 9
        Me.mskTaTarikh.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.mskTaTarikh.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.ForeColor = System.Drawing.Color.Red
        Me.Label4.Location = New System.Drawing.Point(485, 25)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(44, 13)
        Me.Label4.TabIndex = 15
        Me.Label4.Text = "تا تاریخ :"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.ForeColor = System.Drawing.Color.Red
        Me.Label3.Location = New System.Drawing.Point(718, 25)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(35, 13)
        Me.Label3.TabIndex = 13
        Me.Label3.Text = "افراد :"
        '
        'cmbAfrad
        '
        Me.cmbAfrad.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbAfrad.FormattingEnabled = True
        Me.cmbAfrad.Location = New System.Drawing.Point(535, 22)
        Me.cmbAfrad.Name = "cmbAfrad"
        Me.cmbAfrad.Size = New System.Drawing.Size(177, 21)
        Me.cmbAfrad.TabIndex = 4
        '
        'txtRoozKard
        '
        Me.txtRoozKard.Location = New System.Drawing.Point(240, 22)
        Me.txtRoozKard.Name = "txtRoozKard"
        Me.txtRoozKard.Size = New System.Drawing.Size(100, 21)
        Me.txtRoozKard.TabIndex = 11
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.ForeColor = System.Drawing.Color.Red
        Me.Label2.Location = New System.Drawing.Point(845, 25)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(62, 13)
        Me.Label2.TabIndex = 9
        Me.Label2.Text = "مرکز پخش :"
        '
        'cmbMarkazPakhsh
        '
        Me.cmbMarkazPakhsh.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbMarkazPakhsh.FormattingEnabled = True
        Me.cmbMarkazPakhsh.Location = New System.Drawing.Point(759, 22)
        Me.cmbMarkazPakhsh.Name = "cmbMarkazPakhsh"
        Me.cmbMarkazPakhsh.Size = New System.Drawing.Size(84, 21)
        Me.cmbMarkazPakhsh.TabIndex = 3
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.ForeColor = System.Drawing.Color.Red
        Me.Label1.Location = New System.Drawing.Point(1003, 25)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(33, 13)
        Me.Label1.TabIndex = 7
        Me.Label1.Text = "دوره :"
        '
        'cmbDore
        '
        Me.cmbDore.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbDore.FormattingEnabled = True
        Me.cmbDore.Location = New System.Drawing.Point(913, 22)
        Me.cmbDore.Name = "cmbDore"
        Me.cmbDore.Size = New System.Drawing.Size(84, 21)
        Me.cmbDore.TabIndex = 1
        '
        'btnPrint
        '
        Me.btnPrint.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold)
        Me.btnPrint.ForeColor = System.Drawing.Color.Maroon
        Me.btnPrint.Location = New System.Drawing.Point(962, 460)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(84, 30)
        Me.btnPrint.TabIndex = 8
        Me.btnPrint.Text = "چاپ "
        Me.btnPrint.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.BtnSearch)
        Me.GroupBox2.Controls.Add(Me.Rb_SanavatS)
        Me.GroupBox2.Controls.Add(Me.Rb_EidyS)
        Me.GroupBox2.Controls.Add(Me.Label6)
        Me.GroupBox2.Controls.Add(Me.CmbAfradS)
        Me.GroupBox2.Controls.Add(Me.Label7)
        Me.GroupBox2.Controls.Add(Me.CmbMarkazS)
        Me.GroupBox2.Controls.Add(Me.Label8)
        Me.GroupBox2.Controls.Add(Me.CmbDoreS)
        Me.GroupBox2.Location = New System.Drawing.Point(2, 12)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(1054, 65)
        Me.GroupBox2.TabIndex = 16
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "جستجو"
        '
        'BtnSearch
        '
        Me.BtnSearch.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnSearch.ForeColor = System.Drawing.Color.Green
        Me.BtnSearch.Location = New System.Drawing.Point(10, 19)
        Me.BtnSearch.Name = "BtnSearch"
        Me.BtnSearch.Size = New System.Drawing.Size(88, 30)
        Me.BtnSearch.TabIndex = 16
        Me.BtnSearch.Tag = "154213"
        Me.BtnSearch.Text = "جستجو"
        Me.BtnSearch.UseVisualStyleBackColor = True
        '
        'Rb_SanavatS
        '
        Me.Rb_SanavatS.AutoSize = True
        Me.Rb_SanavatS.Location = New System.Drawing.Point(214, 26)
        Me.Rb_SanavatS.Name = "Rb_SanavatS"
        Me.Rb_SanavatS.Size = New System.Drawing.Size(57, 17)
        Me.Rb_SanavatS.TabIndex = 15
        Me.Rb_SanavatS.Text = "سنوات"
        Me.Rb_SanavatS.UseVisualStyleBackColor = True
        '
        'Rb_EidyS
        '
        Me.Rb_EidyS.AutoSize = True
        Me.Rb_EidyS.Checked = True
        Me.Rb_EidyS.Location = New System.Drawing.Point(277, 26)
        Me.Rb_EidyS.Name = "Rb_EidyS"
        Me.Rb_EidyS.Size = New System.Drawing.Size(52, 17)
        Me.Rb_EidyS.TabIndex = 14
        Me.Rb_EidyS.TabStop = True
        Me.Rb_EidyS.Text = "عیدی"
        Me.Rb_EidyS.UseVisualStyleBackColor = True
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.ForeColor = System.Drawing.Color.Black
        Me.Label6.Location = New System.Drawing.Point(611, 25)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(35, 13)
        Me.Label6.TabIndex = 13
        Me.Label6.Text = "افراد :"
        '
        'CmbAfradS
        '
        Me.CmbAfradS.FormattingEnabled = True
        Me.CmbAfradS.Location = New System.Drawing.Point(365, 22)
        Me.CmbAfradS.Name = "CmbAfradS"
        Me.CmbAfradS.Size = New System.Drawing.Size(240, 21)
        Me.CmbAfradS.TabIndex = 4
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.ForeColor = System.Drawing.Color.Black
        Me.Label7.Location = New System.Drawing.Point(823, 23)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(62, 13)
        Me.Label7.TabIndex = 9
        Me.Label7.Text = "مرکز پخش :"
        '
        'CmbMarkazS
        '
        Me.CmbMarkazS.FormattingEnabled = True
        Me.CmbMarkazS.Location = New System.Drawing.Point(678, 20)
        Me.CmbMarkazS.Name = "CmbMarkazS"
        Me.CmbMarkazS.Size = New System.Drawing.Size(139, 21)
        Me.CmbMarkazS.TabIndex = 3
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.ForeColor = System.Drawing.Color.Black
        Me.Label8.Location = New System.Drawing.Point(1003, 25)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(33, 13)
        Me.Label8.TabIndex = 7
        Me.Label8.Text = "دوره :"
        '
        'CmbDoreS
        '
        Me.CmbDoreS.FormattingEnabled = True
        Me.CmbDoreS.Location = New System.Drawing.Point(913, 22)
        Me.CmbDoreS.Name = "CmbDoreS"
        Me.CmbDoreS.Size = New System.Drawing.Size(84, 21)
        Me.CmbDoreS.TabIndex = 1
        '
        'BtnErsal
        '
        Me.BtnErsal.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold)
        Me.BtnErsal.ForeColor = System.Drawing.Color.Red
        Me.BtnErsal.Location = New System.Drawing.Point(23, 460)
        Me.BtnErsal.Name = "BtnErsal"
        Me.BtnErsal.Size = New System.Drawing.Size(84, 30)
        Me.BtnErsal.TabIndex = 17
        Me.BtnErsal.Text = "ارسال"
        Me.BtnErsal.UseVisualStyleBackColor = True
        '
        'BtnDelete
        '
        Me.BtnDelete.BackColor = System.Drawing.Color.Red
        Me.BtnDelete.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold)
        Me.BtnDelete.ForeColor = System.Drawing.Color.Black
        Me.BtnDelete.Location = New System.Drawing.Point(492, 460)
        Me.BtnDelete.Name = "BtnDelete"
        Me.BtnDelete.Size = New System.Drawing.Size(84, 30)
        Me.BtnDelete.TabIndex = 18
        Me.BtnDelete.Text = "حذف"
        Me.BtnDelete.UseVisualStyleBackColor = False
        '
        'chkKoli
        '
        Me.chkKoli.AutoSize = True
        Me.chkKoli.Location = New System.Drawing.Point(871, 473)
        Me.chkKoli.Name = "chkKoli"
        Me.chkKoli.Size = New System.Drawing.Size(68, 17)
        Me.chkKoli.TabIndex = 19
        Me.chkKoli.Text = "چاپ کلی"
        Me.chkKoli.UseVisualStyleBackColor = True
        '
        'btnSaveChange
        '
        Me.btnSaveChange.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSaveChange.Image = CType(resources.GetObject("btnSaveChange.Image"), System.Drawing.Image)
        Me.btnSaveChange.Location = New System.Drawing.Point(213, 464)
        Me.btnSaveChange.Name = "btnSaveChange"
        Me.btnSaveChange.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.btnSaveChange.Size = New System.Drawing.Size(66, 26)
        Me.btnSaveChange.TabIndex = 20
        '
        'frmHo_MohasebeEydi
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1058, 502)
        Me.Controls.Add(Me.btnSaveChange)
        Me.Controls.Add(Me.chkKoli)
        Me.Controls.Add(Me.BtnDelete)
        Me.Controls.Add(Me.BtnErsal)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.btnPrint)
        Me.Controls.Add(Me.GroupBox1)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "frmHo_MohasebeEydi"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.RightToLeftLayout = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "محاسبه عیدی و سنوات"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.DGV, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cmbAfrad As System.Windows.Forms.ComboBox
    Friend WithEvents txtRoozKard As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cmbDore As System.Windows.Forms.ComboBox
    Friend WithEvents btnPrint As System.Windows.Forms.Button
    Friend WithEvents btnMohasebe As System.Windows.Forms.Button
    Friend WithEvents mskTaTarikh As System.Windows.Forms.MaskedTextBox
    Friend WithEvents cmbMarkazPakhsh As System.Windows.Forms.ComboBox
    Friend WithEvents DGV As DataGridView
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents Label6 As Label
    Friend WithEvents CmbAfradS As ComboBox
    Friend WithEvents Label7 As Label
    Friend WithEvents CmbMarkazS As ComboBox
    Friend WithEvents Label8 As Label
    Friend WithEvents CmbDoreS As ComboBox
    Friend WithEvents Label5 As Label
    Friend WithEvents Rb_Sanavat As RadioButton
    Friend WithEvents Rb_Eidy As RadioButton
    Friend WithEvents Rb_SanavatS As RadioButton
    Friend WithEvents Rb_EidyS As RadioButton
    Friend WithEvents BtnSearch As Button
    Friend WithEvents BtnErsal As Button
    Friend WithEvents BtnDelete As Button
    Friend WithEvents chkKoli As CheckBox
    Friend WithEvents btnSaveChange As Button
End Class
