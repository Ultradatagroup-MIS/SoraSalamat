Public Class frmHE_SharhAsnadIntellisense
    Inherits System.Windows.Forms.Form

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents lstSharhAsnad As System.Windows.Forms.ListBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.lstSharhAsnad = New System.Windows.Forms.ListBox
        Me.SuspendLayout()
        '
        'lstSharhAsnad
        '
        Me.lstSharhAsnad.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lstSharhAsnad.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lstSharhAsnad.IntegralHeight = False
        Me.lstSharhAsnad.ItemHeight = 16
        Me.lstSharhAsnad.Location = New System.Drawing.Point(4, 4)
        Me.lstSharhAsnad.Name = "lstSharhAsnad"
        Me.lstSharhAsnad.Size = New System.Drawing.Size(170, 34)
        Me.lstSharhAsnad.TabIndex = 0
        '
        'SharhAsnadIntellisense
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 16)
        Me.ClientSize = New System.Drawing.Size(178, 42)
        Me.ControlBox = False
        Me.Controls.Add(Me.lstSharhAsnad)
        Me.DockPadding.All = 4
        Me.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "SharhAsnadIntellisense"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.ResumeLayout(False)

    End Sub

#End Region
#Region " Enums "
    Public Enum HE_AnvaeSanadMaly As Integer
        SanadHesabdary = 152
        Daryaft = 153
        Pardakht = 154
        SanadBastanHesabMovaghat = 155
        SanadBastanHesabDaem = 156
        SanadEftetahieh = 157
        SanadForosh = 158
        SanadEkhtetamieh = 159
        SanadHoghoghDastMozd = 160
        SanadAnbar = 161
        SanadAmval_Estehlak = 162
        SanadAmval_NaghloEnteghal = 163
        SanadAmval_Tadilat = 164
        SanadAmval_Forosh = 165
        SanadAmval_Amany = 166
        SanadAmval_Hazineh = 167
        'SanadAmval_ForoshH = 168
    End Enum
#End Region
#Region " Variables & Properties "
    Dim dsMenu As New DataSet
    Dim dvMenu As DataView
    Dim dvForm As DataView
    Dim dsForm As New DataSet
    Dim txtCaption As String
    Private WithEvents BS As New UD_Dll.PassString
    Dim pNoeSanad As UD_Dll.Enums.HE_AnvaeSanadMaly
    Dim pSharhSanad As String
    '------------------------------'
    Public Property NoeSanad() As HE_AnvaeSanadMaly
        Get
            Return pNoeSanad
        End Get
        Set(ByVal Value As HE_AnvaeSanadMaly)
            pNoeSanad = Value
        End Set
    End Property
    '------------------------------'
    Public Property SharhSanad() As String
        Get
            Return pSharhSanad
        End Get
        Set(ByVal Value As String)
            pSharhSanad = Value
        End Set
    End Property
#End Region
#Region " Events "
    Private Sub frmHE_SharhAsnadIntellisense_Closed(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Closed
        dsForm = Nothing
        dvForm = Nothing
    End Sub
    Private Sub SharhAsnadIntellisense_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SharhSanad = ""
        If NoeSanad = 0 Then NoeSanad = UD_Dll.Enums.HE_AnvaeSanadMaly.SanadHesabdary
    End Sub
    Private Sub lstSharhAsnad_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles lstSharhAsnad.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            If lstSharhAsnad.SelectedIndex <> -1 Then
                HideMenu()
            End If
        ElseIf e.KeyCode = System.Windows.Forms.Keys.Escape Then
            SharhSanad = ""
            Me.Close()
        End If
    End Sub
    Private Sub SharhAsnadIntellisense_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles MyBase.Paint
        Dim wLine = New Drawing.Point() {New Drawing.Point(0, Me.Height - 1), New Drawing.Point(0, 0), New Drawing.Point(Me.Width - 1, 0)}
        Dim bLine = New Drawing.Point() {New Drawing.Point(0, Me.Height - 1), New Drawing.Point(Me.Width - 1, Me.Height - 1), New Drawing.Point(Me.Width - 1, 0)}
        Dim Gra As Drawing.Graphics
        Gra = Me.CreateGraphics

        Gra.Clear(Drawing.SystemColors.Control)
        Gra.DrawLines(New Drawing.Pen(Drawing.Color.Gray), bLine)
        Gra.DrawLines(New Drawing.Pen(Drawing.Color.White), wLine)
    End Sub
    Private Sub lstSharhAsnad_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstSharhAsnad.DoubleClick
        HideMenu()
    End Sub
#End Region
#Region " Global Code "
    Public Sub InitMenu()
        Dim daSQL As SqlDataAdapter
        Dim strSQL As String

        strSQL = "Select * From tblHE_SharhAsnad "
        If dsMenu.Tables.Contains("tblMenu") Then
            dsMenu.Tables.Remove("tblMenu")
        End If

        daSQL = New SqlDataAdapter(strSQL, ConnectionString)
        daSQL.Fill(dsMenu, "tblMenu")

        dvMenu = New DataView(dsMenu.Tables("tblMenu"), "sNoeSanad = " & NoeSanad, "SharhSanad", DataViewRowState.CurrentRows)
        With lstSharhAsnad
            .DataSource = Nothing
            .Items.Clear()
            .DataSource = dvMenu
            .DisplayMember = "SharhSanad"
            .ValueMember = "SharhSanad"
        End With
        daSQL = Nothing
    End Sub
    Public Sub HideMenu()
        SharhSanad = lstSharhAsnad.SelectedValue
        Me.Close()
    End Sub
#End Region

End Class
