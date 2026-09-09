Imports System.Data
Imports System.Data.SqlClient
Public Class EditTafsilyVahed
    Dim dsForm As New DataSet
    Dim dvForm As New DataView
    Private Sub EditTafsilyVahed_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txtNameTafsilyVahed.Text = NameTafsilyVahed & " _ " & ShomarehTafsilyVahed
        Dim sNoeTafsily As Integer = PubsNoeTafsily
        Search(pubsNoeTafsily)
    End Sub
    Private Sub Search(ByVal pubsNoeTafsily As Integer)
        Dim strSQL As String = ""
        Dim strShart As String = ""

        strSQL = " select "
        strSQL &= " cast (0 as bit) as chk ,"
        strSQL &= " a.FSharhTafsily, b.NumTafsily, c.FSharhNoeTafsily, a.CodeTafsily, NameMahal "
        strSQL &= " from qryHE_GKMAT as a "
        strSQL &= " left outer join "
        strSQL &= " tblHE_CodeTafsily as b on a.CodeTafsily = b.CodeTafsily "
        strSQL &= " left outer join "
        strSQL &= " tblHE_AnvaeTafsily as c on b.CodeNoeTafsily = c.CodeNoeTafsily "
        strSQL &= " left outer join "
        strSQL &= " tblGl_MarkazPakhsh as d on d.CodeMahal = b.CodeMahal "
        strSQL &= " where a.CodeTafsily not in (Select CodeTafsily from tblHE_TafsilyVahed) AND a.sNoeTafsily = " & pubsNoeTafsily
        'strSQL &= " and a.CodeMahal = " & CodeMahalFaal
        ' جستجو بر اساس نام تفصیلی


        strSQL &= " group by NameMahal,a.FSharhTafsily, b.NumTafsily, c.FSharhNoeTafsily, a.CodeTafsily"


        RefreshTitrdata(strSQL)

    End Sub
    Private Sub RefreshTitrdata(ByVal strSql As String)
        Dim daSQL As SqlDataAdapter

        If dsForm.Tables.Contains("tbl1") = True Then
            dsForm.Tables.Remove("tbl1")
        End If
        daSQL = New SqlDataAdapter(strSql, ConnectionString & ";timeout=0")
        daSQL.SelectCommand.CommandTimeout = 99999
        daSQL.Fill(dsForm, "tbl1")
        dvForm = New DataView
        dvForm = dsForm.Tables("tbl1").DefaultView
        dvForm.Sort = ""
        dvForm.AllowDelete = True
        dvForm.AllowEdit = True
        dvForm.AllowNew = False
        daSQL = Nothing

        SetGridStyle(dvForm)

    End Sub
    Private Sub SetGridStyle(ByVal dv As DataView)

            DataGridView1.DataSource = dv

            For i As Integer = 0 To DataGridView1.ColumnCount - 1
                DataGridView1.Columns(i).Visible = False
            Next
            If CType(DataGridView1.DataSource, DataView).Table.Columns.IndexOf("chk") <> -1 Then
                DataGridView1.Columns("chk").Width = 50
                DataGridView1.Columns("chk").HeaderText = "انتخاب"
                DataGridView1.Columns("chk").Visible = True
                DataGridView1.Columns("chk").ReadOnly = False
            End If

            If CType(DataGridView1.DataSource, DataView).Table.Columns.IndexOf("FSharhTafsily") <> -1 Then
                DataGridView1.Columns("FSharhTafsily").Width = 200
                DataGridView1.Columns("FSharhTafsily").HeaderText = "شرح تفصیلی"
                DataGridView1.Columns("FSharhTafsily").Visible = True
                DataGridView1.Columns("FSharhTafsily").ReadOnly = True
            End If

            If CType(DataGridView1.DataSource, DataView).Table.Columns.IndexOf("NumTafsily") <> -1 Then
                DataGridView1.Columns("NumTafsily").Width = 150
                DataGridView1.Columns("NumTafsily").HeaderText = "شماره تفصیلی"
                DataGridView1.Columns("NumTafsily").Visible = True
                DataGridView1.Columns("NumTafsily").ReadOnly = True
            End If

            If CType(DataGridView1.DataSource, DataView).Table.Columns.IndexOf("FSharhNoeTafsily") <> -1 Then
                DataGridView1.Columns("FSharhNoeTafsily").Width = 200
                DataGridView1.Columns("FSharhNoeTafsily").HeaderText = "نوع تفصیلی"
                DataGridView1.Columns("FSharhNoeTafsily").Visible = True
                DataGridView1.Columns("FSharhNoeTafsily").ReadOnly = True
            End If

            If CType(DataGridView1.DataSource, DataView).Table.Columns.IndexOf("NameMahal") <> -1 Then
                DataGridView1.Columns("NameMahal").Width = 80
                DataGridView1.Columns("NameMahal").HeaderText = "مرکز پخش"
                DataGridView1.Columns("NameMahal").Visible = True
                DataGridView1.Columns("NameMahal").ReadOnly = True
            End If

    End Sub

    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Me.Close()
    End Sub

    Private Sub AddNewRecord()

        Dim cnSQL As SqlConnection
        Dim cmSQL As SqlCommand
        Dim strSQL As String

        cnSQL = New SqlConnection(ConnectionString)
        cnSQL.Open()


        Try
            For Each dr As DataRowView In dvForm
                If dr("chk") Then

                    strSQL = "INSERT into tblHE_TafsilyVahed "
                    strSQL &= " (NameTafsilyVahed,ShomarehTafsilyVahed,NumTafsily,CodeTafsily,sNoeTafsily,CodeMahal)"
                    strSQL &= " VALUES ("
                    strSQL &= "'" & NameTafsilyVahed & "'"
                    strSQL &= "," & ShomarehTafsilyVahed
                    strSQL &= "," & dr("NumTafsily")
                    strSQL &= "," & dr("CodeTafsily")
                    strSQL &= "," & pubsNoeTafsily
                    strSQL &= "," & CodeMahalFaal & ")"

                    cmSQL = New SqlCommand(strSQL, cnSQL)
                    cmSQL.ExecuteNonQuery()
                End If
            Next
            cnSQL.Close()
            cmSQL = Nothing : cnSQL = Nothing

            MsgBox("عملیات با موفقیت انجام شد .", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "")


        Catch sqlExc As SqlException
        End Try
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        AddNewRecord()
        Search(pubsNoeTafsily)
    End Sub
End Class