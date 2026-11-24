Imports System.IO
Imports System.Data.SqlClient
Imports System.Data.SqlClient.SqlConnection
Imports System.Data.Common
Imports System.Data
Public Class HazfPardakhti
#Region "Variable AND Constant Declration"
    Const cntCodeSubSystem As Long = 752
    Const FormTableName = "tblFO_EbtedayeDoreh"

    Dim ErrPro As New ErrorProvider
    Dim Mode As UD_Dll.Enums.GL_ModeForms = UD_Dll.Enums.GL_ModeForms.AddNewRecord
    Dim dsForm As New DataSet
    Dim cmForm As CurrencyManager
    Dim dvForm As DataView
    Private SN As Integer
    Dim txtCaption As String
    Public ccMoshtaryEtebar As Integer
    Public ccMoshtaryElamie As Integer
    Public ccForoshandeh As Integer
    Public Noe As Integer
    Private WithEvents BS As New UD_Dll.PassString
#End Region

    Private Sub HazfPardakhti_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetParameter()
        Search(False)
        SN = objSec.GetSecurityNumber(cntCodeSubSystem, UserName)
        objTools.ChangeKbd(UD_Dll.mdlUtility.KBD.Farsi)
    End Sub
    Private Sub SetParameter()

        Dim commands As String = Microsoft.VisualBasic.Command()
        If commands.Length = 0 Then

            UserName = "Administrator"
            UserPassWord = "66998833"
            NameMahalFaal = "تهران"
            CodeMahalFaal = "1"
            PersonelCode = "0"
            PersonelName = "Administrator"
            CodeDoreh = "1392"
            txtCaption = "دفتر مشتری در سطح استان"
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
    Private Sub Search(ByVal WithCriteria As Boolean)
        Dim StrSql As String
        Try
            If Noe = 1 Then
                StrSql = "Treasury.spVosolFaktor_SearchBedehiEbtedayeDorehTolo"
                RefreshFormData(StrSql)
            ElseIf Noe = 2 Then
                StrSql = "Treasury.spVosolFaktor_SearchElamieMoshtaryTolo"
                RefreshFormDataElamie(StrSql)
            End If
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "Error IN DataBase ---->" & "search")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, "Error IN ---->" & "search")
        End Try
    End Sub
    Private Sub RefreshFormData(ByVal strsql As String)
        Dim cnsql As SqlConnection
        Dim cmsql As SqlCommand
        Dim daSQL As SqlDataAdapter
        Try
            cnsql = New SqlConnection(ConnectionString)
            cnsql.Open()

            cmsql = New SqlCommand(strsql, cnsql)
            cmsql.CommandType = CommandType.StoredProcedure
            cmsql.Parameters.Clear()

            cmsql.Parameters.AddWithValue("ccMoshtary", ccMoshtaryEtebar)

            If dsForm.Tables.Contains(FormTableName) = True Then
                dsForm.Tables.Remove(FormTableName)
            End If
            daSQL = New SqlDataAdapter(cmsql)
            daSQL.Fill(dsForm, FormTableName)
            dvForm = New DataView
            dvForm = dsForm.Tables(FormTableName).DefaultView
            'dvForm.Sort = "Mandeh Desc"
            dvForm.AllowDelete = True
            dvForm.AllowEdit = False
            dvForm.AllowNew = False
            daSQL = Nothing

            SetGridStyle()
        Catch e As SqlException
            MsgBox(e.Message, MsgBoxStyle.Information, "خطا در عمليات بانک")
        Catch e As Exception
            MsgBox(e.Message, MsgBoxStyle.Information, "خطا")
        End Try
    End Sub
    Private Sub RefreshFormDataElamie(ByVal strsql As String)
        Dim cnsql As SqlConnection
        Dim cmsql As SqlCommand
        Dim daSQL As SqlDataAdapter
        Try
            cnsql = New SqlConnection(ConnectionString)
            cnsql.Open()

            cmsql = New SqlCommand(strsql, cnsql)
            cmsql.CommandType = CommandType.StoredProcedure
            cmsql.Parameters.Clear()

            cmsql.Parameters.AddWithValue("ccMoshtary", ccMoshtaryElamie)

            If dsForm.Tables.Contains(FormTableName) = True Then
                dsForm.Tables.Remove(FormTableName)
            End If
            daSQL = New SqlDataAdapter(cmsql)
            daSQL.Fill(dsForm, FormTableName)
            dvForm = New DataView
            dvForm = dsForm.Tables(FormTableName).DefaultView
            'dvForm.Sort = "Mandeh Desc"
            dvForm.AllowDelete = True
            dvForm.AllowEdit = False
            dvForm.AllowNew = False
            daSQL = Nothing

            SetGridStyleElamie()
        Catch e As SqlException
            MsgBox(e.Message, MsgBoxStyle.Information, "خطا در عمليات بانک")
        Catch e As Exception
            MsgBox(e.Message, MsgBoxStyle.Information, "خطا")
        End Try
    End Sub
    Private Sub SetGridStyle()
        Try
            With GridExSandogh
                .DataSource = Nothing
                .DataSource = dsForm.Tables(FormTableName).DefaultView
                .SetDataBinding(dsForm.Tables(FormTableName).DefaultView, "")
                .RetrieveStructure()
            End With

            For i As Integer = 0 To GridExSandogh.CurrentTable.Columns.Count - 1
                GridExSandogh.CurrentTable.Columns.Item(i).Visible = False
            Next

            GridExSandogh.CurrentTable.Columns.Item("NoeAmalyat").Caption = "نوع"
            GridExSandogh.CurrentTable.Columns.Item("NoeAmalyat").Visible = True
            GridExSandogh.CurrentTable.Columns.Item("NoeAmalyat").Width = 150
            GridExSandogh.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridExSandogh.CurrentTable.Columns.Item("NoeAmalyat").Position = 0
            GridExSandogh.CurrentTable.Columns.Item("NoeAmalyat").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridExSandogh.CurrentTable.Columns.Item("NameMoshtary").Caption = "نام مشتری"
            GridExSandogh.CurrentTable.Columns.Item("NameMoshtary").Visible = True
            GridExSandogh.CurrentTable.Columns.Item("NameMoshtary").Width = 150
            GridExSandogh.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridExSandogh.CurrentTable.Columns.Item("NameMoshtary").Position = 1
            GridExSandogh.CurrentTable.Columns.Item("NameMoshtary").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridExSandogh.CurrentTable.Columns.Item("MablaghAmalyat").Caption = "مبلغ عملیات"
            GridExSandogh.CurrentTable.Columns.Item("MablaghAmalyat").Visible = True
            GridExSandogh.CurrentTable.Columns.Item("MablaghAmalyat").Width = 100
            GridExSandogh.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridExSandogh.CurrentTable.Columns.Item("MablaghAmalyat").Position = 2
            GridExSandogh.CurrentTable.Columns.Item("MablaghAmalyat").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridExSandogh.CurrentTable.Columns.Item("TarikhAmalyat").Caption = "تاریخ عملیات"
            GridExSandogh.CurrentTable.Columns.Item("TarikhAmalyat").Visible = True
            GridExSandogh.CurrentTable.Columns.Item("TarikhAmalyat").Width = 100
            GridExSandogh.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridExSandogh.CurrentTable.Columns.Item("TarikhAmalyat").Position = 3
            GridExSandogh.CurrentTable.Columns.Item("TarikhAmalyat").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridExSandogh.CurrentTable.Columns.Item("MablaghPardakhti").Caption = "مبلغ پرداختی ابتدای دوره"
            GridExSandogh.CurrentTable.Columns.Item("MablaghPardakhti").Visible = True
            GridExSandogh.CurrentTable.Columns.Item("MablaghPardakhti").Width = 150
            GridExSandogh.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridExSandogh.CurrentTable.Columns.Item("MablaghPardakhti").Position = 4
            GridExSandogh.CurrentTable.Columns.Item("MablaghPardakhti").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridExSandogh.CurrentTable.Columns.Item("EbtedayeDoreh").Caption = "ابتدای دوره مشتری"
            GridExSandogh.CurrentTable.Columns.Item("EbtedayeDoreh").Visible = True
            GridExSandogh.CurrentTable.Columns.Item("EbtedayeDoreh").Width = 120
            GridExSandogh.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridExSandogh.CurrentTable.Columns.Item("EbtedayeDoreh").Position = 5
            GridExSandogh.CurrentTable.Columns.Item("EbtedayeDoreh").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridExSandogh.CurrentTable.Columns.Item("TarikhSabtEbtedayeDoreh").Caption = "تاریخ ثبت ابتدای دوره"
            GridExSandogh.CurrentTable.Columns.Item("TarikhSabtEbtedayeDoreh").Visible = True
            GridExSandogh.CurrentTable.Columns.Item("TarikhSabtEbtedayeDoreh").Width = 150
            GridExSandogh.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridExSandogh.CurrentTable.Columns.Item("TarikhSabtEbtedayeDoreh").Position = 6
            GridExSandogh.CurrentTable.Columns.Item("TarikhSabtEbtedayeDoreh").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            For i As Integer = 0 To GridExSandogh.RootTable.Columns.Count - 1
                If GridExSandogh.RootTable.Columns(i).Type.IsValueType Then
                    GridExSandogh.RootTable.Columns(i).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                    GridExSandogh.RootTable.Columns(i).FormatMode = Janus.Windows.GridEX.FormatMode.UseIFormattable
                    GridExSandogh.RootTable.Columns(i).FormatString = "###,###.##"
                    GridExSandogh.RootTable.Columns(i).TotalFormatMode = Janus.Windows.GridEX.FormatMode.UseIFormattable
                    GridExSandogh.RootTable.Columns(i).TotalFormatString = "###,###.##"
                End If
            Next

            GridExSandogh.Visible = True
            GridExSandogh.HeaderFormatStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center


        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> SetGridExSandogh")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> SetGridExSandogh")
        End Try
    End Sub
    Private Sub SetGridStyleElamie()
        Try
            With GridExSandogh
                .DataSource = Nothing
                .DataSource = dsForm.Tables(FormTableName).DefaultView
                .SetDataBinding(dsForm.Tables(FormTableName).DefaultView, "")
                .RetrieveStructure()
            End With

            For i As Integer = 0 To GridExSandogh.CurrentTable.Columns.Count - 1
                GridExSandogh.CurrentTable.Columns.Item(i).Visible = False
            Next

            GridExSandogh.CurrentTable.Columns.Item("NoeAmalyat").Caption = "نوع"
            GridExSandogh.CurrentTable.Columns.Item("NoeAmalyat").Visible = True
            GridExSandogh.CurrentTable.Columns.Item("NoeAmalyat").Width = 170
            GridExSandogh.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridExSandogh.CurrentTable.Columns.Item("NoeAmalyat").Position = 0
            GridExSandogh.CurrentTable.Columns.Item("NoeAmalyat").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridExSandogh.CurrentTable.Columns.Item("NameMoshtary").Caption = "نام مشتری"
            GridExSandogh.CurrentTable.Columns.Item("NameMoshtary").Visible = True
            GridExSandogh.CurrentTable.Columns.Item("NameMoshtary").Width = 170
            GridExSandogh.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridExSandogh.CurrentTable.Columns.Item("NameMoshtary").Position = 1
            GridExSandogh.CurrentTable.Columns.Item("NameMoshtary").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridExSandogh.CurrentTable.Columns.Item("MablaghAmalyat").Caption = "مبلغ عملیات"
            GridExSandogh.CurrentTable.Columns.Item("MablaghAmalyat").Visible = True
            GridExSandogh.CurrentTable.Columns.Item("MablaghAmalyat").Width = 120
            GridExSandogh.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridExSandogh.CurrentTable.Columns.Item("MablaghAmalyat").Position = 2
            GridExSandogh.CurrentTable.Columns.Item("MablaghAmalyat").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridExSandogh.CurrentTable.Columns.Item("TarikhAmalyat").Caption = "تاریخ عملیات"
            GridExSandogh.CurrentTable.Columns.Item("TarikhAmalyat").Visible = True
            GridExSandogh.CurrentTable.Columns.Item("TarikhAmalyat").Width = 120
            GridExSandogh.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridExSandogh.CurrentTable.Columns.Item("TarikhAmalyat").Position = 3
            GridExSandogh.CurrentTable.Columns.Item("TarikhAmalyat").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridExSandogh.CurrentTable.Columns.Item("MablaghPardakhti").Caption = "مبلغ پرداختی اعلامیه"
            GridExSandogh.CurrentTable.Columns.Item("MablaghPardakhti").Visible = True
            GridExSandogh.CurrentTable.Columns.Item("MablaghPardakhti").Width = 170
            GridExSandogh.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridExSandogh.CurrentTable.Columns.Item("MablaghPardakhti").Position = 4
            GridExSandogh.CurrentTable.Columns.Item("MablaghPardakhti").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridExSandogh.CurrentTable.Columns.Item("TarikhSabtEbtedayeDoreh").Caption = "تاریخ ثبت اعلامیه"
            GridExSandogh.CurrentTable.Columns.Item("TarikhSabtEbtedayeDoreh").Visible = True
            GridExSandogh.CurrentTable.Columns.Item("TarikhSabtEbtedayeDoreh").Width = 170
            GridExSandogh.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridExSandogh.CurrentTable.Columns.Item("TarikhSabtEbtedayeDoreh").Position = 6
            GridExSandogh.CurrentTable.Columns.Item("TarikhSabtEbtedayeDoreh").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            For i As Integer = 0 To GridExSandogh.RootTable.Columns.Count - 1
                If GridExSandogh.RootTable.Columns(i).Type.IsValueType Then
                    GridExSandogh.RootTable.Columns(i).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                    GridExSandogh.RootTable.Columns(i).FormatMode = Janus.Windows.GridEX.FormatMode.UseIFormattable
                    GridExSandogh.RootTable.Columns(i).FormatString = "###,###.##"
                    GridExSandogh.RootTable.Columns(i).TotalFormatMode = Janus.Windows.GridEX.FormatMode.UseIFormattable
                    GridExSandogh.RootTable.Columns(i).TotalFormatString = "###,###.##"
                End If
            Next

            GridExSandogh.Visible = True
            GridExSandogh.HeaderFormatStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center


        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> SetGridExSandogh")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> SetGridExSandogh")
        End Try
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub btnTaeed_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTaeed.Click
        Dim StrSql As String
        Try
            If Noe = 1 Then
                StrSql = "Treasury.spVosolFaktor_HazfBedehiEbtedayeDorehTolo"
                RefreshFormDataHazf(StrSql)
            ElseIf Noe = 2 Then
                StrSql = "Treasury.spVosolFaktor_HazfElamieMoshtaryTolo"
                RefreshFormDataHazfElamie(StrSql)
            End If
            Search(False)
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "Error IN DataBase ---->" & "search")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, "Error IN ---->" & "search")
        End Try
    End Sub
    Private Sub RefreshFormDataHazf(ByVal strsql As String)
        Dim cnsql As SqlConnection
        Dim cmsql As SqlCommand
        Try
            cnsql = New SqlConnection(ConnectionString)
            cnsql.Open()

            cmsql = New SqlCommand(strsql, cnsql)
            cmsql.CommandType = CommandType.StoredProcedure
            cmsql.Parameters.Clear()

            cmsql.Parameters.AddWithValue("ccMoshtary", ccMoshtaryEtebar)
            cmsql.Parameters.AddWithValue("CodeAmalyat", GridExSandogh.CurrentRow.Cells("CodeAmalyat").Value)
            cmsql.Parameters.AddWithValue("Mablagh", GridExSandogh.CurrentRow.Cells("MablaghPardakhti").Value)

            If dsForm.Tables.Contains("tbl") = True Then
                dsForm.Tables.Remove("tbl")
            End If

            cmsql.ExecuteNonQuery()

        Catch e As SqlException
            MsgBox(e.Message, MsgBoxStyle.Information, "خطا در عمليات بانک")
        Catch e As Exception
            MsgBox(e.Message, MsgBoxStyle.Information, "خطا")
        End Try
    End Sub
    Private Sub RefreshFormDataHazfElamie(ByVal strsql As String)
        Dim cnsql As SqlConnection
        Dim cmsql As SqlCommand
        Try
            cnsql = New SqlConnection(ConnectionString)
            cnsql.Open()

            cmsql = New SqlCommand(strsql, cnsql)
            cmsql.CommandType = CommandType.StoredProcedure
            cmsql.Parameters.Clear()

            cmsql.Parameters.AddWithValue("ccMoshtary", ccMoshtaryElamie)
            cmsql.Parameters.AddWithValue("CodeAmalyat", GridExSandogh.CurrentRow.Cells("CodeAmalyat").Value)

            If dsForm.Tables.Contains("tbl") = True Then
                dsForm.Tables.Remove("tbl")
            End If

            cmsql.ExecuteNonQuery()

        Catch e As SqlException
            MsgBox(e.Message, MsgBoxStyle.Information, "خطا در عمليات بانک")
        Catch e As Exception
            MsgBox(e.Message, MsgBoxStyle.Information, "خطا")
        End Try
    End Sub
End Class