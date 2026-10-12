Imports System.Data.SqlClient


Public Class frmMarjoeeSearch
        Inherits System.Windows.Forms.Form

        Private ReadOnly _cs As String
        Private ReadOnly _codeMahal As Integer
        Private ReadOnly _codeDoreh As Integer
        Private ReadOnly _vazeiat As Integer

        Private WithEvents grd As DataGridView
        Private WithEvents txtFilter As TextBox
        Private dt As DataTable

        Public SelectedId As Integer = 0

        Public Sub New(ByVal cs As String, ByVal codeMahal As Integer, ByVal codeDoreh As Integer, ByVal vazeiat As Integer)
            _cs = cs
            _codeMahal = codeMahal
            _codeDoreh = codeDoreh
            _vazeiat = vazeiat

            Me.Text = "جستجوی مرجوعی"
            Me.StartPosition = FormStartPosition.CenterParent
            Me.Size = New Size(900, 500)
            Me.RightToLeft = RightToLeft.Yes
            Me.RightToLeftLayout = True
            Me.KeyPreview = True

            txtFilter = New TextBox()
            txtFilter.Dock = DockStyle.Top
            txtFilter.Height = 24
            Me.Controls.Add(txtFilter)

            grd = New DataGridView()
            grd.Dock = DockStyle.Fill
            grd.ReadOnly = True
            grd.AllowUserToAddRows = False
            grd.AllowUserToDeleteRows = False
            grd.SelectionMode = DataGridViewSelectionMode.FullRowSelect
            grd.MultiSelect = False
            grd.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            grd.RowHeadersVisible = False
            Me.Controls.Add(grd)
            grd.BringToFront()
        End Sub

        Private Sub frmMarjoeeSearch_Load(sender As Object, e As EventArgs) Handles MyBase.Load
            LoadData()
            txtFilter.Focus()
        End Sub

        Private Sub LoadData()
            Try
                Dim sql As String =
                "SELECT ccKardexTitr, ShomarehForm AS [شماره فرم], TarikhFormSlash AS [تاریخ], " &
                "NameTaminKonandeh AS [تامین کننده], NameAnbar AS [انبار], ShomarehResid AS [شماره رسید], Tozihat AS [توضیحات] " &
                "FROM qryAN_KdxMarjoeeBeTaminKonandeh " &
                "WHERE CodeMahal = @m AND CodeDoreh = @d AND sVazeiat = @v " &
                "ORDER BY ShomarehForm DESC"

                dt = New DataTable
                Using cn As New SqlConnection(_cs)
                    Using da As New SqlDataAdapter(sql, cn)
                        da.SelectCommand.Parameters.AddWithValue("@m", _codeMahal)
                        da.SelectCommand.Parameters.AddWithValue("@d", _codeDoreh.ToString())
                        da.SelectCommand.Parameters.AddWithValue("@v", _vazeiat)
                        da.Fill(dt)
                    End Using
                End Using

                grd.DataSource = dt.DefaultView
                If grd.Columns.Contains("ccKardexTitr") Then grd.Columns("ccKardexTitr").Visible = False
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        End Sub

        Private Sub txtFilter_TextChanged(sender As Object, e As EventArgs) Handles txtFilter.TextChanged
            If dt Is Nothing Then Exit Sub
            Dim t As String = txtFilter.Text.Trim().Replace("'", "''")
            If t = "" Then
                dt.DefaultView.RowFilter = ""
            Else
                Dim f As String = "Convert([شماره فرم], 'System.String') LIKE '%" & t & "%'" &
                              " OR [تامین کننده] LIKE '%" & t & "%'" &
                              " OR [توضیحات] LIKE '%" & t & "%'"
                Try
                    dt.DefaultView.RowFilter = f
                Catch
                    dt.DefaultView.RowFilter = ""
                End Try
            End If
        End Sub

        Private Sub PickCurrent()
            If grd.CurrentRow Is Nothing Then Exit Sub
            Dim v As Object = grd.CurrentRow.Cells("ccKardexTitr").Value
            If v Is Nothing OrElse IsDBNull(v) Then Exit Sub
            SelectedId = Convert.ToInt32(v)
            Me.DialogResult = DialogResult.OK
            Me.Close()
        End Sub

        Private Sub grd_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles grd.CellDoubleClick
            If e.RowIndex >= 0 Then PickCurrent()
        End Sub

        Private Sub frmMarjoeeSearch_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
            If e.KeyCode = Keys.Enter Then
                e.Handled = True
                e.SuppressKeyPress = True
                PickCurrent()
            ElseIf e.KeyCode = Keys.Escape Then
                Me.DialogResult = DialogResult.Cancel
                Me.Close()
            ElseIf e.KeyCode = Keys.Down AndAlso txtFilter.Focused Then
                grd.Focus()
            End If
        End Sub
    End Class
