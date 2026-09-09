<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MinMaxSefaresh
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
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.lblNameKala = New System.Windows.Forms.Label()
        Me.txtCodeKala = New System.Windows.Forms.TextBox()
        Me.lblKala = New System.Windows.Forms.Label()
        Me.cmbNameAnbar = New System.Windows.Forms.ComboBox()
        Me.lblNameAnbar = New System.Windows.Forms.Label()
        Me.txtSefaresh = New System.Windows.Forms.TextBox()
        Me.h = New System.Windows.Forms.Label()
        Me.txtMax = New System.Windows.Forms.TextBox()
        Me.lblMax = New System.Windows.Forms.Label()
        Me.txtMin = New System.Windows.Forms.TextBox()
        Me.lblMin = New System.Windows.Forms.Label()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.btnExit = New System.Windows.Forms.Button()
        Me.btnDelete = New System.Windows.Forms.Button()
        Me.btnSearch = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.btnEdit = New System.Windows.Forms.Button()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.GridEX1 = New Janus.Windows.GridEX.GridEX()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        CType(Me.GridEX1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.lblNameKala)
        Me.GroupBox1.Controls.Add(Me.txtCodeKala)
        Me.GroupBox1.Controls.Add(Me.lblKala)
        Me.GroupBox1.Controls.Add(Me.cmbNameAnbar)
        Me.GroupBox1.Controls.Add(Me.lblNameAnbar)
        Me.GroupBox1.Controls.Add(Me.txtSefaresh)
        Me.GroupBox1.Controls.Add(Me.h)
        Me.GroupBox1.Controls.Add(Me.txtMax)
        Me.GroupBox1.Controls.Add(Me.lblMax)
        Me.GroupBox1.Controls.Add(Me.txtMin)
        Me.GroupBox1.Controls.Add(Me.lblMin)
        Me.GroupBox1.Location = New System.Drawing.Point(1, 15)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(474, 129)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        '
        'lblNameKala
        '
        Me.lblNameKala.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblNameKala.Location = New System.Drawing.Point(30, 53)
        Me.lblNameKala.Name = "lblNameKala"
        Me.lblNameKala.Size = New System.Drawing.Size(247, 22)
        Me.lblNameKala.TabIndex = 19
        '
        'txtCodeKala
        '
        Me.txtCodeKala.Location = New System.Drawing.Point(279, 55)
        Me.txtCodeKala.Name = "txtCodeKala"
        Me.txtCodeKala.Size = New System.Drawing.Size(99, 21)
        Me.txtCodeKala.TabIndex = 18
        '
        'lblKala
        '
        Me.lblKala.AutoSize = True
        Me.lblKala.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.lblKala.ForeColor = System.Drawing.Color.Maroon
        Me.lblKala.Location = New System.Drawing.Point(388, 57)
        Me.lblKala.Name = "lblKala"
        Me.lblKala.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.lblKala.Size = New System.Drawing.Size(42, 13)
        Me.lblKala.TabIndex = 17
        Me.lblKala.Text = "نام کالا:"
        Me.lblKala.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'cmbNameAnbar
        '
        Me.cmbNameAnbar.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbNameAnbar.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbNameAnbar.FormattingEnabled = True
        Me.cmbNameAnbar.Location = New System.Drawing.Point(28, 17)
        Me.cmbNameAnbar.Name = "cmbNameAnbar"
        Me.cmbNameAnbar.Size = New System.Drawing.Size(349, 21)
        Me.cmbNameAnbar.TabIndex = 1
        '
        'lblNameAnbar
        '
        Me.lblNameAnbar.AutoSize = True
        Me.lblNameAnbar.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNameAnbar.ForeColor = System.Drawing.Color.Maroon
        Me.lblNameAnbar.Location = New System.Drawing.Point(386, 22)
        Me.lblNameAnbar.Name = "lblNameAnbar"
        Me.lblNameAnbar.Size = New System.Drawing.Size(44, 13)
        Me.lblNameAnbar.TabIndex = 10
        Me.lblNameAnbar.Text = "نام انبار:"
        '
        'txtSefaresh
        '
        Me.txtSefaresh.Location = New System.Drawing.Point(14, 93)
        Me.txtSefaresh.Name = "txtSefaresh"
        Me.txtSefaresh.Size = New System.Drawing.Size(100, 21)
        Me.txtSefaresh.TabIndex = 5
        '
        'h
        '
        Me.h.AutoSize = True
        Me.h.ForeColor = System.Drawing.Color.Maroon
        Me.h.Location = New System.Drawing.Point(117, 98)
        Me.h.Name = "h"
        Me.h.Size = New System.Drawing.Size(44, 13)
        Me.h.TabIndex = 8
        Me.h.Text = "سفارش"
        '
        'txtMax
        '
        Me.txtMax.Location = New System.Drawing.Point(166, 95)
        Me.txtMax.Name = "txtMax"
        Me.txtMax.Size = New System.Drawing.Size(100, 21)
        Me.txtMax.TabIndex = 4
        '
        'lblMax
        '
        Me.lblMax.AutoSize = True
        Me.lblMax.ForeColor = System.Drawing.Color.Maroon
        Me.lblMax.Location = New System.Drawing.Point(270, 100)
        Me.lblMax.Name = "lblMax"
        Me.lblMax.Size = New System.Drawing.Size(44, 13)
        Me.lblMax.TabIndex = 6
        Me.lblMax.Text = "ماکزیمم"
        '
        'txtMin
        '
        Me.txtMin.Location = New System.Drawing.Point(319, 97)
        Me.txtMin.Name = "txtMin"
        Me.txtMin.Size = New System.Drawing.Size(100, 21)
        Me.txtMin.TabIndex = 3
        '
        'lblMin
        '
        Me.lblMin.AutoSize = True
        Me.lblMin.ForeColor = System.Drawing.Color.Maroon
        Me.lblMin.Location = New System.Drawing.Point(423, 102)
        Me.lblMin.Name = "lblMin"
        Me.lblMin.Size = New System.Drawing.Size(40, 13)
        Me.lblMin.TabIndex = 4
        Me.lblMin.Text = "مینیمم"
        '
        'GroupBox2
        '
        Me.GroupBox2.Location = New System.Drawing.Point(92, 148)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(8, 8)
        Me.GroupBox2.TabIndex = 1
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "GroupBox2"
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.btnExit)
        Me.GroupBox3.Controls.Add(Me.btnDelete)
        Me.GroupBox3.Controls.Add(Me.btnSearch)
        Me.GroupBox3.Controls.Add(Me.btnCancel)
        Me.GroupBox3.Controls.Add(Me.btnEdit)
        Me.GroupBox3.Controls.Add(Me.btnSave)
        Me.GroupBox3.Location = New System.Drawing.Point(0, 134)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(475, 43)
        Me.GroupBox3.TabIndex = 1
        Me.GroupBox3.TabStop = False
        '
        'btnExit
        '
        Me.btnExit.Location = New System.Drawing.Point(11, 17)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(58, 23)
        Me.btnExit.TabIndex = 5
        Me.btnExit.Text = "&خروج"
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'btnDelete
        '
        Me.btnDelete.Location = New System.Drawing.Point(155, 17)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(58, 23)
        Me.btnDelete.TabIndex = 4
        Me.btnDelete.Text = "&حذف"
        Me.btnDelete.UseVisualStyleBackColor = True
        '
        'btnSearch
        '
        Me.btnSearch.Location = New System.Drawing.Point(217, 17)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(58, 23)
        Me.btnSearch.TabIndex = 3
        Me.btnSearch.Text = "&جستجو"
        Me.btnSearch.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        Me.btnCancel.Location = New System.Drawing.Point(279, 17)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(58, 23)
        Me.btnCancel.TabIndex = 2
        Me.btnCancel.Text = "&صرفنظر"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'btnEdit
        '
        Me.btnEdit.Location = New System.Drawing.Point(342, 17)
        Me.btnEdit.Name = "btnEdit"
        Me.btnEdit.Size = New System.Drawing.Size(58, 23)
        Me.btnEdit.TabIndex = 1
        Me.btnEdit.Text = "&ویرایش"
        Me.btnEdit.UseVisualStyleBackColor = True
        '
        'btnSave
        '
        Me.btnSave.Location = New System.Drawing.Point(406, 17)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(58, 23)
        Me.btnSave.TabIndex = 0
        Me.btnSave.Text = "&ذخیره"
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'GridEX1
        '
        Me.GridEX1.AllowChildTableGroups = True
        Me.GridEX1.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.GridEX1.AllowDrop = True
        Me.GridEX1.AllowRemoveColumns = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.GridEX1.BorderStyle = Janus.Windows.GridEX.BorderStyle.RaisedLight3D
        Me.GridEX1.ColumnSetNavigation = Janus.Windows.GridEX.ColumnSetNavigation.ColumnSet
        Me.GridEX1.DynamicFiltering = True
        Me.GridEX1.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        Me.GridEX1.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.GridEX1.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges
        Me.GridEX1.Font = New System.Drawing.Font("Tahoma", 9.75!)
        Me.GridEX1.GroupByBoxVisible = False
        Me.GridEX1.GroupRowVisualStyle = Janus.Windows.GridEX.GroupRowVisualStyle.Outlook2003
        Me.GridEX1.GroupTotals = Janus.Windows.GridEX.GroupTotals.Always
        Me.GridEX1.Location = New System.Drawing.Point(2, 1)
        Me.GridEX1.Name = "GridEX1"
        Me.GridEX1.Office2007ColorScheme = Janus.Windows.GridEX.Office2007ColorScheme.Blue
        Me.GridEX1.RecordNavigator = True
        Me.GridEX1.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.GridEX1.Size = New System.Drawing.Size(474, 19)
        Me.GridEX1.TabIndex = 8
        Me.GridEX1.TabStop = False
        Me.GridEX1.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed
        Me.GridEX1.Visible = False
        Me.GridEX1.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'MinMaxSefaresh
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(478, 180)
        Me.Controls.Add(Me.GridEX1)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MaximizeBox = False
        Me.Name = "MinMaxSefaresh"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "مینیمم ماکزیمم سفارش "
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        CType(Me.GridEX1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents txtSefaresh As System.Windows.Forms.TextBox
    Friend WithEvents h As System.Windows.Forms.Label
    Friend WithEvents txtMax As System.Windows.Forms.TextBox
    Friend WithEvents lblMax As System.Windows.Forms.Label
    Friend WithEvents txtMin As System.Windows.Forms.TextBox
    Friend WithEvents lblMin As System.Windows.Forms.Label
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents btnDelete As System.Windows.Forms.Button
    Friend WithEvents btnSearch As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnEdit As System.Windows.Forms.Button
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents GridEX1 As Janus.Windows.GridEX.GridEX
    Friend WithEvents cmbNameAnbar As System.Windows.Forms.ComboBox
    Friend WithEvents lblNameAnbar As System.Windows.Forms.Label
    Friend WithEvents lblNameKala As System.Windows.Forms.Label
    Friend WithEvents txtCodeKala As System.Windows.Forms.TextBox
    Friend WithEvents lblKala As System.Windows.Forms.Label

End Class
