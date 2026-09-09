Imports System.IO
Imports System.Data
Imports System.Text
Imports System.Data.SqlClient
Imports Microsoft.VisualBasic
Public Class frmTaghiratAnbar

    Dim AzTarikh As String
    Dim Tatarikh As String

    Public TableName As String
    Public tbl As String
    Dim flg As Boolean
    Public Smantagheh As String = "-1"
    Public Count As Integer
    Public cc As Integer

    Dim txtCaption As String
    Private SN As Integer
    Private WithEvents BS As New UD_Dll.PassString
    Const cntCodeSubSystem As Long = 1000219
    Private Sub frmTaghiratAnbar_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetParameter()
        AzTarikh = CodeDoreh & "0101"
        Tatarikh = CodeDoreh & "1230"
        LoadData()
        SN = objSec.GetSecurityNumber(cntCodeSubSystem, UserName)
        objTools.ChangeKbd(UD_Dll.mdlUtility.KBD.Farsi)
    End Sub
    Private Sub SetParameter()

        Dim commands As String = Microsoft.VisualBasic.Command()
        If commands.Length = 0 Then

            UserName = "Administrator"
            UserPassWord = "66998833"
            NameMahalFaal = "تهران"
            CodeMahalFaal = "45"
            PersonelCode = "0"
            PersonelName = "Administrator"
            CodeDoreh = "1395"
            txtCaption = "مانیتورینگ انبار"
        Else

            UserName = commands.Split(";")(0)
            UserCode = commands.Split(";")(1)
            UserPassWord = commands.Split(";")(2)
            NameMahalFaal = commands.Split(";")(3)
            CodeMahalFaal = commands.Split(";")(4)
            PersonelCode = commands.Split(";")(5)
            PersonelName = commands.Split(";")(6)
            CodeDoreh = commands.Split(";")(7)
            txtCaption = commands.Split(";")(8)
            Me.Text = txtCaption
        End If

    End Sub
    Private Sub LoadData()

        Dim dt As Data.DataTable = Nothing
        Dim da As SqlDataAdapter = New SqlDataAdapter
        Try
            Using cn As New SqlConnection(ConnectionString)
                Using cm As SqlCommand = cn.CreateCommand()
                    cn.Open()
                    cm.Parameters.Clear()
                    cm.CommandType = CommandType.StoredProcedure
                    cm.CommandText = "[dbo].[sp_GetTaghiratAnbar]"
                    cm.Parameters.AddWithValue("AzTarikh", AzTarikh)
                    cm.Parameters.AddWithValue("TaTarikh", Tatarikh)
                    da.SelectCommand = cm
                    dt = New DataTable
                    da.Fill(dt)
                End Using
            End Using

        Catch ex As Exception
            Throw New Exception("Error In--> GetData : " & ex.Message)
        Finally
        End Try
        '-----------
        SetGridStyle(dt)
    End Sub
    Private Sub SetGridStyle(dtForm As DataTable)

        With gridData
            .DataSource = Nothing
            .DataSource = dtForm
            .SetDataBinding(dtForm, "")
            .RetrieveStructure()
        End With

        For i As Integer = 0 To gridData.CurrentTable.Columns.Count - 1
            gridData.CurrentTable.Columns.Item(i).Visible = False
        Next

        gridData.CurrentTable.Columns.Item("tarikhform").Caption = "تاریخ فرم"
        gridData.CurrentTable.Columns.Item("tarikhform").Visible = True
        gridData.CurrentTable.Columns.Item("tarikhform").Width = 150
        gridData.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
        gridData.CurrentTable.Columns.Item("tarikhform").Position = 0
        gridData.CurrentTable.Columns.Item("tarikhform").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

        gridData.CurrentTable.Columns.Item("TarikhMilady").Caption = "تاریخ میلادی"
        gridData.CurrentTable.Columns.Item("TarikhMilady").Visible = True
        gridData.CurrentTable.Columns.Item("TarikhMilady").Width = 70
        gridData.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
        gridData.CurrentTable.Columns.Item("TarikhMilady").Position = 1
        gridData.CurrentTable.Columns.Item("TarikhMilady").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center


        gridData.CurrentTable.Columns.Item("txtNoeForm").Caption = "نوع فرم"
        gridData.CurrentTable.Columns.Item("txtNoeForm").Visible = True
        gridData.CurrentTable.Columns.Item("txtNoeForm").Width = 65
        gridData.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
        gridData.CurrentTable.Columns.Item("txtNoeForm").Position = 2
        gridData.CurrentTable.Columns.Item("txtNoeForm").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center


        gridData.CurrentTable.Columns.Item("codekala").Caption = "کد کالا "
        gridData.CurrentTable.Columns.Item("codekala").Visible = True
        gridData.CurrentTable.Columns.Item("codekala").Width = 70
        gridData.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
        gridData.CurrentTable.Columns.Item("codekala").Position = 3
        gridData.CurrentTable.Columns.Item("codekala").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center


        gridData.CurrentTable.Columns.Item("namekala").Caption = "نام کالا "
        gridData.CurrentTable.Columns.Item("namekala").Visible = True
        gridData.CurrentTable.Columns.Item("namekala").Width = 105
        gridData.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
        gridData.CurrentTable.Columns.Item("namekala").Position = 4
        gridData.CurrentTable.Columns.Item("namekala").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center


        gridData.CurrentTable.Columns.Item("NameAnbar").Caption = "نام انبار"
        gridData.CurrentTable.Columns.Item("NameAnbar").Visible = True
        gridData.CurrentTable.Columns.Item("NameAnbar").Width = 105
        gridData.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
        gridData.CurrentTable.Columns.Item("NameAnbar").Position = 5
        gridData.CurrentTable.Columns.Item("NameAnbar").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

        gridData.CurrentTable.Columns.Item("ShomarehForm").Caption = "شماره فرم"
        gridData.CurrentTable.Columns.Item("ShomarehForm").Visible = True
        gridData.CurrentTable.Columns.Item("ShomarehForm").Width = 105
        gridData.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
        gridData.CurrentTable.Columns.Item("ShomarehForm").Position = 6
        gridData.CurrentTable.Columns.Item("ShomarehForm").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

        gridData.CurrentTable.Columns.Item("sVazeiat").Caption = "کد وضعیت"
        gridData.CurrentTable.Columns.Item("sVazeiat").Visible = True
        gridData.CurrentTable.Columns.Item("sVazeiat").Width = 200
        gridData.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
        gridData.CurrentTable.Columns.Item("sVazeiat").Position = 7
        gridData.CurrentTable.Columns.Item("sVazeiat").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

        gridData.CurrentTable.Columns.Item("CodeDoreh").Caption = "دوره"
        gridData.CurrentTable.Columns.Item("CodeDoreh").Visible = True
        gridData.CurrentTable.Columns.Item("CodeDoreh").Width = 200
        gridData.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
        gridData.CurrentTable.Columns.Item("CodeDoreh").Position = 8
        gridData.CurrentTable.Columns.Item("CodeDoreh").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

    End Sub
    Private Sub gridData_DoubleClick(sender As Object, e As EventArgs) Handles gridData.DoubleClick
        Dim dt As Data.DataTable = Nothing
        Dim da As SqlDataAdapter = New SqlDataAdapter
        Try
            Using cn As New SqlConnection(ConnectionString)
                Using cm As SqlCommand = cn.CreateCommand()
                    cn.Open()
                    cm.Parameters.Clear()
                    cm.CommandType = CommandType.StoredProcedure
                    cm.CommandText = "[dbo].[sp_GetTaghiratAnbar_PK]"
                    cm.Parameters.AddWithValue("ccSatr", Val(Me.gridData.CurrentRow.Cells("ccSatr").Text.Replace(",", "")))
                    da.SelectCommand = cm
                    dt = New DataTable
                    da.Fill(dt)
                End Using
            End Using

        Catch ex As Exception
            Throw New Exception("Error In--> GetData_Pk : " & ex.Message)
        Finally
        End Try
        '-----------

        If dt.Rows.Count > 0 Then
            txtTarikhForm.Text = dt.Rows(0)("TarikhForm")
            txtTarikhMilady.Text = dt.Rows(0)("TarikhMilady")
            txtSvazeiat.Text = dt.Rows(0)("Svazeiat")
            TableName = dt.Rows(0)("TableName")
            cc = dt.Rows(0)("ccTitr")
            txtShomarehform.Text = dt.Rows(0)("Shomarehform")

        End If
    End Sub
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Select Case TableName
            Case "dbo.tblAN_kdxHavalehSatr"
                Count = 1
            Case "dbo.tblAN_kdxResidSatr"
                Count = 2
            Case "dbo.tblAN_ResidMavadAvaliehSatr"
                Count = 3
            Case "dbo.tblAN_KasrMavadAvaliehSatr"
                Count = 4
            Case "dbo.tblAN_kdxMarjoeeBeTaminKonandehSatr"
                Count = 5
            Case "dbo.tblAN_KdxRSAvalDorehSatr"
                Count = 6
            Case "dbo.tblAN_MarjoeeAzMoshtarySatr"
                Count = 7
            Case "Sales.ElamMarjoeeSatr_PishFaktor"
                Count = 8
            Case "dbo.tblAN_EshantionSatr"
                Count = 9
            Case "dbo.tblAN_KasrEzafehSatr"
                Count = 10
            Case "dbo.tblAN_KdxAnbarBeAnbarSatr"
                Count = 11
            Case "dbo.tblFO_PishFaktorSatr"
                Count = 12
            Case "dbo.tblFO_FaktorSatr"
                Count = 13

        End Select


        Using cn As New SqlConnection(ConnectionString)
            Using cm As SqlCommand = cn.CreateCommand
                cm.CommandType = CommandType.StoredProcedure
                cm.CommandText = "[dbo].[sp_GetTaghiratAnbar_Update]"
                cm.Parameters.AddWithValue("tblNum", Count)
                cm.Parameters.AddWithValue("TarikhForm", txtTarikhForm.Text.Trim)
                cm.Parameters.AddWithValue("TarikhMilady", txtTarikhMilady.Text.Trim)
                cm.Parameters.AddWithValue("svazeiat", Val(txtSvazeiat.Text))
                cm.Parameters.AddWithValue("ccTitr", cc)
                cm.Parameters.AddWithValue("Shomarehform", txtShomarehform.Text.Trim)
                cm.Connection.Open()
                cm.ExecuteNonQuery()
                cm.Connection.Close()
            End Using
        End Using

        MsgBox("با موفقیت ویرایش شد.", MsgBoxStyle.Information + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.OkOnly, "")

        txtTarikhForm.Text = ""
        txtTarikhMilady.Text = ""
        txtSvazeiat.Text = ""
        TableName = ""
        txtShomarehform.Text = ""
        cc = 0
        Count = 0

        LoadData()

    End Sub

    Private Sub txtTarikhForm_TextChanged(sender As Object, e As EventArgs) Handles txtTarikhForm.TextChanged

    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles txtShomarehform.TextChanged

    End Sub

    Private Sub Label4_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Label4_shomarehform(sender As Object, e As EventArgs) Handles Label4.Click

    End Sub
End Class
