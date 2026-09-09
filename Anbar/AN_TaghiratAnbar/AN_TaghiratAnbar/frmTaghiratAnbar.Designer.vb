<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmTaghiratAnbar
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmTaghiratAnbar))
        Dim gridData_Layout_0 As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim gridData_Layout_1 As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim gridData_Layout_2 As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim gridData_Layout_3 As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim gridData_Layout_4 As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim gridData_Layout_5 As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Me.gpData = New System.Windows.Forms.GroupBox()
        Me.gridData = New Janus.Windows.GridEX.GridEX()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtTarikhMilady = New System.Windows.Forms.TextBox()
        Me.txtSvazeiat = New System.Windows.Forms.TextBox()
        Me.txtTarikhForm = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtShomarehform = New System.Windows.Forms.TextBox()
        Me.gpData.SuspendLayout()
        CType(Me.gridData, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'gpData
        '
        Me.gpData.Controls.Add(Me.gridData)
        Me.gpData.Location = New System.Drawing.Point(5, 1)
        Me.gpData.Name = "gpData"
        Me.gpData.Size = New System.Drawing.Size(775, 526)
        Me.gpData.TabIndex = 0
        Me.gpData.TabStop = False
        Me.gpData.Text = "اطلاعات"
        '
        'gridData
        '
        Me.gridData.AllowChildTableGroups = True
        Me.gridData.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.gridData.AllowDrop = True
        Me.gridData.AllowRemoveColumns = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.gridData.BorderStyle = Janus.Windows.GridEX.BorderStyle.RaisedLight3D
        Me.gridData.BuiltInTextsData = resources.GetString("gridData.BuiltInTextsData")
        Me.gridData.ColumnSetNavigation = Janus.Windows.GridEX.ColumnSetNavigation.ColumnSet
        Me.gridData.DynamicFiltering = True
        Me.gridData.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        Me.gridData.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.gridData.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges
        Me.gridData.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.gridData.GroupByBoxVisible = False
        Me.gridData.GroupRowVisualStyle = Janus.Windows.GridEX.GroupRowVisualStyle.Outlook2003
        Me.gridData.GroupTotals = Janus.Windows.GridEX.GroupTotals.Always
        Me.gridData.KeepRowSettings = True
        gridData_Layout_0.Key = "Layout1"
        gridData_Layout_1.Key = "Layout2"
        gridData_Layout_2.Key = "Layout3"
        gridData_Layout_3.Key = "Layout4"
        gridData_Layout_4.Key = "Layout5"
        gridData_Layout_5.Key = "Layout6"
        Me.gridData.Layouts.AddRange(New Janus.Windows.GridEX.GridEXLayout() {gridData_Layout_0, gridData_Layout_1, gridData_Layout_2, gridData_Layout_3, gridData_Layout_4, gridData_Layout_5})
        Me.gridData.Location = New System.Drawing.Point(6, 17)
        Me.gridData.Name = "gridData"
        Me.gridData.Office2007ColorScheme = Janus.Windows.GridEX.Office2007ColorScheme.Blue
        Me.gridData.RecordNavigator = True
        Me.gridData.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.gridData.SelectionMode = Janus.Windows.GridEX.SelectionMode.MultipleSelection
        Me.gridData.Size = New System.Drawing.Size(763, 503)
        Me.gridData.TabIndex = 100
        Me.gridData.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed
        Me.gridData.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.txtShomarehform)
        Me.GroupBox1.Controls.Add(Me.btnSave)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.txtTarikhMilady)
        Me.GroupBox1.Controls.Add(Me.txtSvazeiat)
        Me.GroupBox1.Controls.Add(Me.txtTarikhForm)
        Me.GroupBox1.Location = New System.Drawing.Point(5, 533)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(775, 163)
        Me.GroupBox1.TabIndex = 101
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "ثبت تغییرات"
        '
        'btnSave
        '
        Me.btnSave.Image = CType(resources.GetObject("btnSave.Image"), System.Drawing.Image)
        Me.btnSave.Location = New System.Drawing.Point(122, 33)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(64, 64)
        Me.btnSave.TabIndex = 6
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(452, 36)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(62, 13)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "کد وضعیت :"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(706, 81)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(71, 13)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "تاریخ میلادی :"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(706, 36)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(55, 13)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "تاریخ فرم :"
        '
        'txtTarikhMilady
        '
        Me.txtTarikhMilady.Location = New System.Drawing.Point(523, 78)
        Me.txtTarikhMilady.Name = "txtTarikhMilady"
        Me.txtTarikhMilady.Size = New System.Drawing.Size(160, 21)
        Me.txtTarikhMilady.TabIndex = 2
        '
        'txtSvazeiat
        '
        Me.txtSvazeiat.Location = New System.Drawing.Point(269, 33)
        Me.txtSvazeiat.Name = "txtSvazeiat"
        Me.txtSvazeiat.Size = New System.Drawing.Size(160, 21)
        Me.txtSvazeiat.TabIndex = 1
        '
        'txtTarikhForm
        '
        Me.txtTarikhForm.Location = New System.Drawing.Point(523, 33)
        Me.txtTarikhForm.Name = "txtTarikhForm"
        Me.txtTarikhForm.Size = New System.Drawing.Size(160, 21)
        Me.txtTarikhForm.TabIndex = 0
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(452, 81)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(64, 13)
        Me.Label4.TabIndex = 8
        Me.Label4.Text = "شماره فرم :"
        '
        'txtShomarehform
        '
        Me.txtShomarehform.Location = New System.Drawing.Point(269, 78)
        Me.txtShomarehform.Name = "txtShomarehform"
        Me.txtShomarehform.Size = New System.Drawing.Size(160, 21)
        Me.txtShomarehform.TabIndex = 7
        '
        'frmTaghiratAnbar
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(784, 698)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.gpData)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.Name = "frmTaghiratAnbar"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.RightToLeftLayout = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "تغییرات انبار"
        Me.gpData.ResumeLayout(False)
        CType(Me.gridData, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents gpData As GroupBox
    Friend WithEvents gridData As Janus.Windows.GridEX.GridEX
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents btnSave As Button
    Friend WithEvents Label3 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents txtTarikhMilady As TextBox
    Friend WithEvents txtSvazeiat As TextBox
    Friend WithEvents txtTarikhForm As TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtShomarehform As System.Windows.Forms.TextBox
End Class
