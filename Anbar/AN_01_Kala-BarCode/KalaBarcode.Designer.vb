<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class KalaBarcode
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(KalaBarcode))
        Dim GridEX_Layout_0 As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim GridEX_Layout_1 As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim GridEX_Layout_2 As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim GridEX_Layout_3 As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim GridEX_Layout_4 As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim GridEX_Layout_5 As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Me.txtBarCode = New System.Windows.Forms.TextBox()
        Me.LblBarcode = New System.Windows.Forms.Label()
        Me.txtNameKala = New System.Windows.Forms.TextBox()
        Me.LblNameKala = New System.Windows.Forms.Label()
        Me.LblCodeKala = New System.Windows.Forms.Label()
        Me.txtCodeKala = New System.Windows.Forms.TextBox()
        Me.LblNoeMoshtary = New System.Windows.Forms.Label()
        Me.GroupBox5 = New System.Windows.Forms.GroupBox()
        Me.btnRefresh = New System.Windows.Forms.Button()
        Me.btnExit = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.btnSearch = New System.Windows.Forms.Button()
        Me.btnDelete = New System.Windows.Forms.Button()
        Me.btnUpdate = New System.Windows.Forms.Button()
        Me.cmbNoeMoshtary = New System.Windows.Forms.ComboBox()
        Me.GridEX = New Janus.Windows.GridEX.GridEX()
        Me.grbMain = New System.Windows.Forms.GroupBox()
        Me.GroupBox5.SuspendLayout()
        CType(Me.GridEX, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grbMain.SuspendLayout()
        Me.SuspendLayout()
        '
        'txtBarCode
        '
        Me.txtBarCode.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.txtBarCode.Location = New System.Drawing.Point(458, 82)
        Me.txtBarCode.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.txtBarCode.MaxLength = 50
        Me.txtBarCode.Name = "txtBarCode"
        Me.txtBarCode.Size = New System.Drawing.Size(275, 21)
        Me.txtBarCode.TabIndex = 19
        '
        'LblBarcode
        '
        Me.LblBarcode.AutoSize = True
        Me.LblBarcode.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.LblBarcode.Location = New System.Drawing.Point(760, 82)
        Me.LblBarcode.Name = "LblBarcode"
        Me.LblBarcode.Size = New System.Drawing.Size(47, 13)
        Me.LblBarcode.TabIndex = 20
        Me.LblBarcode.Text = "بارکــــد :"
        '
        'txtNameKala
        '
        Me.txtNameKala.Enabled = False
        Me.txtNameKala.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.txtNameKala.Location = New System.Drawing.Point(458, 45)
        Me.txtNameKala.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.txtNameKala.MaxLength = 50
        Me.txtNameKala.Name = "txtNameKala"
        Me.txtNameKala.Size = New System.Drawing.Size(275, 21)
        Me.txtNameKala.TabIndex = 21
        '
        'LblNameKala
        '
        Me.LblNameKala.AutoSize = True
        Me.LblNameKala.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.LblNameKala.Location = New System.Drawing.Point(760, 49)
        Me.LblNameKala.Name = "LblNameKala"
        Me.LblNameKala.Size = New System.Drawing.Size(45, 13)
        Me.LblNameKala.TabIndex = 22
        Me.LblNameKala.Text = "نام کالا :"
        '
        'LblCodeKala
        '
        Me.LblCodeKala.AutoSize = True
        Me.LblCodeKala.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.LblCodeKala.Location = New System.Drawing.Point(348, 47)
        Me.LblCodeKala.Name = "LblCodeKala"
        Me.LblCodeKala.Size = New System.Drawing.Size(43, 13)
        Me.LblCodeKala.TabIndex = 24
        Me.LblCodeKala.Text = "کد کالا :"
        '
        'txtCodeKala
        '
        Me.txtCodeKala.Enabled = False
        Me.txtCodeKala.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.txtCodeKala.Location = New System.Drawing.Point(24, 45)
        Me.txtCodeKala.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.txtCodeKala.MaxLength = 50
        Me.txtCodeKala.Name = "txtCodeKala"
        Me.txtCodeKala.Size = New System.Drawing.Size(300, 21)
        Me.txtCodeKala.TabIndex = 23
        '
        'LblNoeMoshtary
        '
        Me.LblNoeMoshtary.AccessibleRole = System.Windows.Forms.AccessibleRole.Link
        Me.LblNoeMoshtary.AutoSize = True
        Me.LblNoeMoshtary.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.LblNoeMoshtary.Location = New System.Drawing.Point(346, 82)
        Me.LblNoeMoshtary.Name = "LblNoeMoshtary"
        Me.LblNoeMoshtary.Size = New System.Drawing.Size(69, 13)
        Me.LblNoeMoshtary.TabIndex = 26
        Me.LblNoeMoshtary.Text = "نوع مشتری :"
        '
        'GroupBox5
        '
        Me.GroupBox5.Controls.Add(Me.btnRefresh)
        Me.GroupBox5.Controls.Add(Me.btnExit)
        Me.GroupBox5.Controls.Add(Me.btnCancel)
        Me.GroupBox5.Controls.Add(Me.btnSearch)
        Me.GroupBox5.Controls.Add(Me.btnDelete)
        Me.GroupBox5.Controls.Add(Me.btnUpdate)
        Me.GroupBox5.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.GroupBox5.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.GroupBox5.Location = New System.Drawing.Point(0, 159)
        Me.GroupBox5.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Padding = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.GroupBox5.Size = New System.Drawing.Size(839, 64)
        Me.GroupBox5.TabIndex = 27
        Me.GroupBox5.TabStop = False
        '
        'btnRefresh
        '
        Me.btnRefresh.AccessibleDescription = "Refresh Button"
        Me.btnRefresh.AccessibleName = "Refresh Button"
        Me.btnRefresh.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.btnRefresh.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnRefresh.Location = New System.Drawing.Point(298, 21)
        Me.btnRefresh.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(117, 28)
        Me.btnRefresh.TabIndex = 4
        Me.btnRefresh.Text = "&بازخوانی"
        '
        'btnExit
        '
        Me.btnExit.AccessibleDescription = "Search Button"
        Me.btnExit.AccessibleName = "Search Button"
        Me.btnExit.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.btnExit.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnExit.Location = New System.Drawing.Point(48, 21)
        Me.btnExit.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(117, 28)
        Me.btnExit.TabIndex = 8
        Me.btnExit.Text = "&خروج"
        '
        'btnCancel
        '
        Me.btnCancel.AccessibleDescription = "Cancel Button"
        Me.btnCancel.AccessibleName = "Cancel Button"
        Me.btnCancel.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.btnCancel.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnCancel.Location = New System.Drawing.Point(173, 21)
        Me.btnCancel.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(117, 28)
        Me.btnCancel.TabIndex = 2
        Me.btnCancel.Text = "&صرفنظر"
        '
        'btnSearch
        '
        Me.btnSearch.AccessibleDescription = "Search Button"
        Me.btnSearch.AccessibleName = "Search Button"
        Me.btnSearch.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.btnSearch.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnSearch.Location = New System.Drawing.Point(423, 21)
        Me.btnSearch.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(117, 28)
        Me.btnSearch.TabIndex = 1
        Me.btnSearch.Text = "ج&ستجو"
        '
        'btnDelete
        '
        Me.btnDelete.AccessibleDescription = "Delete Button"
        Me.btnDelete.AccessibleName = "Delete Button"
        Me.btnDelete.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.btnDelete.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnDelete.Location = New System.Drawing.Point(548, 21)
        Me.btnDelete.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(117, 28)
        Me.btnDelete.TabIndex = 3
        Me.btnDelete.Text = "&حذف"
        '
        'btnUpdate
        '
        Me.btnUpdate.AccessibleDescription = "Update Button"
        Me.btnUpdate.AccessibleName = "Update Button"
        Me.btnUpdate.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.btnUpdate.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnUpdate.Location = New System.Drawing.Point(673, 21)
        Me.btnUpdate.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.btnUpdate.Name = "btnUpdate"
        Me.btnUpdate.Size = New System.Drawing.Size(117, 28)
        Me.btnUpdate.TabIndex = 0
        Me.btnUpdate.Text = "&ذخيره"
        '
        'cmbNoeMoshtary
        '
        Me.cmbNoeMoshtary.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbNoeMoshtary.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.cmbNoeMoshtary.FormattingEnabled = True
        Me.cmbNoeMoshtary.Location = New System.Drawing.Point(24, 82)
        Me.cmbNoeMoshtary.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.cmbNoeMoshtary.Name = "cmbNoeMoshtary"
        Me.cmbNoeMoshtary.Size = New System.Drawing.Size(300, 21)
        Me.cmbNoeMoshtary.TabIndex = 25
        '
        'GridEX
        '
        Me.GridEX.AllowChildTableGroups = True
        Me.GridEX.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.GridEX.AllowDrop = True
        Me.GridEX.AllowRemoveColumns = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.GridEX.BorderStyle = Janus.Windows.GridEX.BorderStyle.RaisedLight3D
        Me.GridEX.BuiltInTextsData = resources.GetString("GridEX.BuiltInTextsData")
        Me.GridEX.ColumnSetNavigation = Janus.Windows.GridEX.ColumnSetNavigation.ColumnSet
        Me.GridEX.DynamicFiltering = True
        Me.GridEX.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        Me.GridEX.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.GridEX.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges
        Me.GridEX.Font = New System.Drawing.Font("Tahoma", 9.75!)
        Me.GridEX.GroupByBoxVisible = False
        Me.GridEX.GroupRowVisualStyle = Janus.Windows.GridEX.GroupRowVisualStyle.Outlook2003
        Me.GridEX.GroupTotals = Janus.Windows.GridEX.GroupTotals.Always
        Me.GridEX.KeepRowSettings = True
        GridEX_Layout_0.Key = "Layout1"
        GridEX_Layout_1.Key = "Layout2"
        GridEX_Layout_2.Key = "Layout3"
        GridEX_Layout_3.Key = "Layout4"
        GridEX_Layout_4.Key = "Layout5"
        GridEX_Layout_5.Key = "Layout6"
        Me.GridEX.Layouts.AddRange(New Janus.Windows.GridEX.GridEXLayout() {GridEX_Layout_0, GridEX_Layout_1, GridEX_Layout_2, GridEX_Layout_3, GridEX_Layout_4, GridEX_Layout_5})
        Me.GridEX.Location = New System.Drawing.Point(0, 5)
        Me.GridEX.Name = "GridEX"
        Me.GridEX.Office2007ColorScheme = Janus.Windows.GridEX.Office2007ColorScheme.Blue
        Me.GridEX.RecordNavigator = True
        Me.GridEX.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.GridEX.SelectionMode = Janus.Windows.GridEX.SelectionMode.MultipleSelection
        Me.GridEX.Size = New System.Drawing.Size(839, 18)
        Me.GridEX.TabIndex = 99
        Me.GridEX.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed
        Me.GridEX.Visible = False
        Me.GridEX.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'grbMain
        '
        Me.grbMain.Controls.Add(Me.txtCodeKala)
        Me.grbMain.Controls.Add(Me.LblBarcode)
        Me.grbMain.Controls.Add(Me.txtBarCode)
        Me.grbMain.Controls.Add(Me.cmbNoeMoshtary)
        Me.grbMain.Controls.Add(Me.txtNameKala)
        Me.grbMain.Controls.Add(Me.LblNoeMoshtary)
        Me.grbMain.Controls.Add(Me.LblNameKala)
        Me.grbMain.Controls.Add(Me.LblCodeKala)
        Me.grbMain.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.grbMain.Location = New System.Drawing.Point(0, 29)
        Me.grbMain.Name = "grbMain"
        Me.grbMain.Size = New System.Drawing.Size(839, 130)
        Me.grbMain.TabIndex = 100
        Me.grbMain.TabStop = False
        '
        'KalaBarcode
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(839, 223)
        Me.Controls.Add(Me.grbMain)
        Me.Controls.Add(Me.GridEX)
        Me.Controls.Add(Me.GroupBox5)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.Name = "KalaBarcode"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "کالا بارکد"
        Me.GroupBox5.ResumeLayout(False)
        CType(Me.GridEX, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grbMain.ResumeLayout(False)
        Me.grbMain.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents txtBarCode As System.Windows.Forms.TextBox
    Friend WithEvents LblBarcode As System.Windows.Forms.Label
    Friend WithEvents txtNameKala As System.Windows.Forms.TextBox
    Friend WithEvents LblNameKala As System.Windows.Forms.Label
    Friend WithEvents LblCodeKala As System.Windows.Forms.Label
    Friend WithEvents txtCodeKala As System.Windows.Forms.TextBox
    Friend WithEvents LblNoeMoshtary As System.Windows.Forms.Label
    Friend WithEvents GroupBox5 As System.Windows.Forms.GroupBox
    Friend WithEvents btnRefresh As System.Windows.Forms.Button
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnSearch As System.Windows.Forms.Button
    Friend WithEvents btnDelete As System.Windows.Forms.Button
    Friend WithEvents btnUpdate As System.Windows.Forms.Button
    Friend WithEvents cmbNoeMoshtary As System.Windows.Forms.ComboBox
    Friend WithEvents GridEX As Janus.Windows.GridEX.GridEX
    Friend WithEvents grbMain As System.Windows.Forms.GroupBox
End Class
