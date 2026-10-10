Imports System.Data.SqlClient

Public Class frmResidSearch

        Public SelectedId As Integer = 0

        Private ReadOnly _cs As String
        Private ReadOnly _codeMahal As Integer
        Private ReadOnly _codeDoreh As Integer
        Private ReadOnly _sVazeiat As Integer
        Private dgv As New DataGridView()

        Public Sub New(ByVal connectionString As String, ByVal codeMahal As Integer,
                   ByVal codeDoreh As Integer, ByVal sVazeiat As Integer)
            InitializeComponent()

            _cs = connectionString
            _codeMahal = codeMahal
            _codeDoreh = codeDoreh
            _sVazeiat = sVazeiat

            Me.Text = "جستجوی رسید"
            Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
            Me.RightToLeftLayout = True
            Me.StartPosition = FormStartPosition.CenterParent
            Me.Size = New System.Drawing.Size(780, 480)

            dgv.Dock = DockStyle.Fill
            dgv.ReadOnly = True
            dgv.AllowUserToAddRows = False
            dgv.AllowUserToDeleteRows = False
            dgv.MultiSelect = False
            dgv.RowHeadersVisible = False
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            Me.Controls.Add(dgv)

            AddHandler dgv.CellDoubleClick, AddressOf Dgv_CellDoubleClick
            AddHandler dgv.KeyDown, AddressOf Dgv_KeyDown
        End Sub

        Protected Overrides Sub OnLoad(ByVal e As EventArgs)
            MyBase.OnLoad(e)
            Try
                Dim dt As New DataTable()
                Using cn As New SqlConnection(_cs)
                    Using cm As SqlCommand = cn.CreateCommand()
                        cm.CommandType = CommandType.StoredProcedure
                        cm.CommandText = "[dbo].[spAN_KdxResid_SearchTitrList]"
                        cm.Parameters.AddWithValue("@CodeMahal", _codeMahal)
                        cm.Parameters.AddWithValue("@CodeDoreh", _codeDoreh)
                        cm.Parameters.AddWithValue("@sVazeiat", _sVazeiat)
                        Using da As New SqlDataAdapter(cm)
                            da.Fill(dt)
                        End Using
                    End Using
                End Using

                dgv.DataSource = dt
            SetHeader("ccKardexTitr", "شماره سیستمی")
            SetHeader("TarikhForm", "تاریخ")
            SetHeader("NameTaminKonandeh", "تامین‌کننده")
            SetHeader("Tozihat", "توضیحات")
            SetHeader("JamMablagh", "جمع مبلغ")
        Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        End Sub

        Private Sub SetHeader(ByVal key As String, ByVal text As String)
            If dgv.Columns.Contains(key) Then dgv.Columns(key).HeaderText = text
        End Sub

        Private Sub PickCurrent()
            If dgv.CurrentRow Is Nothing Then Exit Sub
            SelectedId = Convert.ToInt32(dgv.CurrentRow.Cells("ccKardexTitr").Value)
            Me.DialogResult = DialogResult.OK
            Me.Close()
        End Sub

        Private Sub Dgv_CellDoubleClick(ByVal sender As Object, ByVal e As DataGridViewCellEventArgs)
            If e.RowIndex >= 0 Then PickCurrent()
        End Sub

        Private Sub Dgv_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs)
            If e.KeyCode = Keys.Enter Then
                e.Handled = True
                PickCurrent()
            ElseIf e.KeyCode = Keys.Escape Then
                Me.DialogResult = DialogResult.Cancel
                Me.Close()
            End If
        End Sub

    Private Sub frmResidSearch_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class