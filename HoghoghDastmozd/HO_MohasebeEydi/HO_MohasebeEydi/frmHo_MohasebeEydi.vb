Imports System.Data
Imports System.Data.SqlClient
Imports Microsoft.VisualBasic
Imports System.IO
Imports CrystalDecisions.CrystalReports.Engine

Public Class frmHo_MohasebeEydi
    Dim Flag As Boolean = False
    Dim txtCaption As String
    Dim dsForm As New DataSet
    Dim dvForm As DataView
    Dim dvSatr As DataView
    Dim TedadRK As Int16 = 0
    Public cntCodeSubSystem = 60000216




    Private Sub frmHo_MohasebeEydi_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetParameter()
        LoadCombo()
        ClearForm()
        Search_Eidy(False)
        Flag = True
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
            CodeDoreh = "1402"

        Else
            UserName = commands.Split(";")(0)
            UserCode = commands.Split(";")(1)
            UserPassWord = commands.Split(";")(2)
            NameMahalFaal = commands.Split(";")(3)
            CodeMahalFaal = commands.Split(";")(4)
            PersonelCode = commands.Split(";")(5)
            PersonelName = commands.Split(";")(6)
            CodeDoreh = commands.Split(";")(7)

        End If
    End Sub
    Private Sub LoadCombo()

        Dim dt As Data.DataTable = Nothing
        Dim da As SqlDataAdapter = New SqlDataAdapter
        Dim Strsql As String
        Dim daSQL As SqlDataAdapter
        Dim dsForm As New DataSet

        Try
            '-------------- Load Combo Mah
            dt = Nothing
            Using cn As New SqlConnection(ConnectionString)
                Using cm As SqlCommand = cn.CreateCommand()
                    cn.Open()
                    cm.Parameters.Clear()
                    cm.CommandType = CommandType.StoredProcedure
                    cm.CommandText = "[Global].[sp_GetDoreh]"
                    da.SelectCommand = cm
                    dt = New DataTable
                    da.Fill(dt)
                End Using
            End Using
            CmbDoreS.DataSource = dt
            CmbDoreS.DisplayMember = "CodeDoreh"
            CmbDoreS.ValueMember = "CodeDoreh"


            cmbDore.DataSource = dt
            cmbDore.DisplayMember = "CodeDoreh"
            cmbDore.ValueMember = "CodeDoreh"

            dt = Nothing
            Using cn As New SqlConnection(ConnectionString)
                Using cm As SqlCommand = cn.CreateCommand()
                    cn.Open()
                    cm.Parameters.Clear()
                    cm.CommandType = CommandType.StoredProcedure
                    cm.CommandText = "[Payroll].[spMohasebehEydi_LoadComboPersonel]"
                    cm.Parameters.AddWithValue("CodeMahal", CodeMahalFaal)
                    cm.Parameters.AddWithValue("CodeDoreh", CodeDoreh)
                    da.SelectCommand = cm
                    dt = New DataTable
                    da.Fill(dt)
                End Using
            End Using
            CmbAfradS.DataSource = dt
            CmbAfradS.DisplayMember = "FN"
            CmbAfradS.ValueMember = "CodeFard"
            CmbAfradS.SelectedValue = -1

            cmbAfrad.DataSource = dt
            cmbAfrad.DisplayMember = "FN"
            cmbAfrad.ValueMember = "CodeFard"

            '-------------- Load Combo MarkazPakhsh

            Strsql = "Select NameMahal,CodeMahal from dbo.tblGL_MarkazPakhsh where CodeMahal<>0 "
            daSQL = New SqlDataAdapter(Strsql, ConnectionString)
            daSQL.Fill(dsForm, "tblMarkazPakhsh")
            cmbMarkazPakhsh.DataSource = Nothing
            cmbMarkazPakhsh.Items.Clear()
            cmbMarkazPakhsh.DataSource = dsForm.Tables("tblMarkazPakhsh").DefaultView
            cmbMarkazPakhsh.DisplayMember = "NameMahal"
            cmbMarkazPakhsh.ValueMember = "CodeMahal"
            'cmbMarkazPakhsh.SelectedValue = -1


            CmbMarkazS.DataSource = Nothing
            CmbMarkazS.Items.Clear()
            CmbMarkazS.DataSource = dsForm.Tables("tblMarkazPakhsh").DefaultView
            CmbMarkazS.DisplayMember = "NameMahal"
            CmbMarkazS.ValueMember = "CodeMahal"



        Catch ex As Exception
            Throw New Exception("Error In--> Load Combo : " & ex.Message)
        Finally
        End Try
    End Sub
    Private Sub ClearForm()

        cmbMarkazPakhsh.SelectedIndex = -1
        cmbAfrad.SelectedIndex = -1
        txtRoozKard.Text = ""
        mskTaTarikh.Text = TarikhEmrooz
        cmbDore.SelectedValue = TarikhEmrooz.Substring(0, 4)

    End Sub
    Private Sub Search_Eidy(ByVal WithCriteria As Boolean)
        Dim StrSql As String

        Try
            StrSql = "SELECT tblgl_MoshakhasatFardi.fname+' '+tblgl_MoshakhasatFardi.lname as Fn, * FROM Payroll.Eidy left outer join tblgl_MoshakhasatFardi on Payroll.Eidy.ccafrad=tblgl_MoshakhasatFardi.codefard Where Svazeiat=1  "

            If CmbDoreS.SelectedIndex <> -1 Then
                    StrSql &= " and Sal = " & CmbDoreS.SelectedValue
                End If

                If CmbMarkazS.SelectedIndex <> -1 Then
                    StrSql &= " and ccMarkazPakhsh = " & CmbMarkazS.SelectedValue
                End If
                If CmbAfradS.SelectedIndex <> -1 Then
                    StrSql &= " and CcAfrad = " & CmbAfradS.SelectedValue
                End If



            RefreshFormData(StrSql)
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "Error IN DataBase ---->" & "search")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, "Error IN ---->" & "search")
        End Try
    End Sub
    Private Sub Search_Sanavat(ByVal WithCriteria As Boolean)
        Dim StrSql As String

        Try
            StrSql = "SELECT tblgl_MoshakhasatFardi.fname +' '+ tblgl_MoshakhasatFardi.lname as Fn, * FROM Payroll.Sanavat left outer join tblgl_MoshakhasatFardi on Payroll.Sanavat.ccafrad=tblgl_MoshakhasatFardi.codefard Where Svazeiat=1  "

            If WithCriteria Then
                If CmbDoreS.SelectedIndex <> -1 Then
                    StrSql &= " and Sal = " & CmbDoreS.SelectedValue
                End If

                If CmbMarkazS.SelectedIndex <> -1 Then
                    StrSql &= " and ccMarkazPakhsh = " & CmbMarkazS.SelectedValue
                End If
                If CmbAfradS.SelectedIndex <> -1 Then
                    StrSql &= " and CcAfrad = " & CmbAfradS.SelectedValue
                End If
            End If


            RefreshFormData(StrSql)
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "Error IN DataBase ---->" & "search")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, "Error IN ---->" & "search")
        End Try
    End Sub



    Function MohasebehEidy() As Boolean
        Try
            If cmbDore.SelectedIndex = -1 Then
                MsgBox("دوره مالی را وارد کنید.")
                Return False
                Exit Function
            End If
            If cmbMarkazPakhsh.SelectedIndex = -1 Then
                MsgBox("مرکز پخش را وارد کنید.")
                Return False
                Exit Function
            End If
            If cmbAfrad.SelectedIndex = -1 Then
                MsgBox("پرسنل را وارد کنید.")
                Return False
                Exit Function
            End If
            If Val(txtRoozKard.Text) <= 0 Then
                MsgBox(" روز کارکرد  وارد  نشده عددی بین 1 تا 365 وارد نمایید.")
                Return False
                Exit Function
            End If
            If CodeDoreh = 1399 Then
                If Val(txtRoozKard.Text) > 366 Then
                    MsgBox("روز کارکرد نمیتواند بیشتر از 366 باشد.")
                    Return False
                    Exit Function
                End If
            End If
            If CodeDoreh <> 1399 Then
                If Val(txtRoozKard.Text) > 365 Then
                    MsgBox("روز کارکرد نمیتواند بیشتر از 365 باشد.")
                    Return False
                    Exit Function
                End If
            End If


            Dim TedadRoozPardakhtShodeh As Int16 = 0
            TedadRoozPardakhtShodeh = objTools.ConvertNulls(objTools.DSum("TedadRoozKarKard", " payroll.Eidy", "ccAfrad = " & cmbAfrad.SelectedValue & " And Sal = " & cmbDore.SelectedValue), 0)

            If (365 - TedadRoozPardakhtShodeh) < 0 Then
                MsgBox("روز کارکرد نمیتواند بیشتر از " & 365 - TedadRoozPardakhtShodeh & " باشد.")
                Return False
                Exit Function
            End If


            Dim cmErrorLog As Data.SqlClient.SqlCommand = GetSQLCommandForTransaction()
            Dim TarikhAvalSal As String
            Dim TarikhMohasebehM As String
            Dim TaTarikh As String = mskTaTarikh.Text

            Dim tSal As Integer = CInt(TaTarikh.Substring(0, 4))
            Dim tMah As Byte = CInt(TaTarikh.Substring(4, 2))
            Dim oDorehMaly As Integer = cmbDore.SelectedValue
            TarikhAvalSal = GetTarikhMilady(CType(oDorehMaly, String) + "0101")
            TarikhAvalSal += " 23:59:59"
            oDorehMaly = Nothing

            TarikhMohasebehM = GetTarikhMilady(mskTaTarikh.Text)
            TarikhMohasebehM += " 23:59:59"

            Dim TedadRoozAzAvalSal As Integer = DateDiff(DateInterval.Day, CDate(TarikhAvalSal), CDate(TarikhMohasebehM)) + 1

            Dim i As Integer = 0
            Dim Sal As Integer
            Dim Darsad As Double
            Dim MablaghSabet As Double
            Dim Saghf As Double = 0
            Dim TedadMah As Byte = 0
            Dim strsql As String = ""
            Dim cmsql As New SqlCommand
            Dim da As SqlDataAdapter = New SqlDataAdapter
            Dim dtr As New DataTable
            Dim cn As SqlConnection
            Dim MablaghMohasebeh As Double = 0
            cn = New SqlConnection(ConnectionString)
            cn.Open()


            strsql = "payroll.spMohasebehEydi_AyeenNamehSalParameter"
            cmsql = New SqlCommand(strsql, cn)
            cmsql.CommandType = CommandType.StoredProcedure
            cmsql.Parameters.Clear()
            cmsql.Parameters.AddWithValue("AzTarikh", TarikhMohasebehM)
            da = New SqlDataAdapter(cmsql)

            da.Fill(dtr)
            If dtr.Rows.Count = 0 Then
                InsertErroLog(ccMarkazPakhsh, 0, Sal, 0, 110, "اطلاعات جدول آیین نامه پارامترهای سالانه وجود ندارد", cmErrorLog)
                cmsql.Transaction.Rollback()
                Return False
                Exit Function
            Else
                Saghf = CDbl(dtr.Rows(0)("SaghfEidyPadash"))
                MablaghMohasebeh = CDbl(dtr.Rows(0)("HadeAghalDastmozd")) * 30
                TedadMah = 2
            End If

            Sal = CInt(cmbDore.SelectedValue)
            Darsad = 100

            MablaghSabet = 0
            MohasebehEidy(MablaghMohasebeh, Me.cmbMarkazPakhsh.SelectedValue, Sal, cmbAfrad.SelectedValue,
                                  mskTaTarikh.Text, Darsad, MablaghSabet, Saghf, TedadMah,
                                  tSal, tMah, TedadRoozAzAvalSal, TarikhMohasebehM, TarikhAvalSal, cmsql)

            Search_Eidy(False)
            Return True
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->MohasebehEidy")
            Return False
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->MohasebehEidy")
            Return False
        End Try


    End Function

    Function MohasebehSanavat() As Boolean
        Try
            If cmbDore.SelectedIndex = -1 Then
                MsgBox("دوره مالی را وارد کنید.")
                Return False
                Exit Function
            End If

            If CodeDoreh = 1399 Then
                If Val(txtRoozKard.Text) > 366 Then
                    MsgBox("روز کارکرد نمیتواند بیشتر از 366 باشد.")
                    Return False
                    Exit Function
                End If
            End If
            If CodeDoreh <> 1399 Then
                If Val(txtRoozKard.Text) > 365 Then
                    MsgBox("روز کارکرد نمیتواند بیشتر از 365 باشد.")
                    Return False
                    Exit Function
                End If
            End If
            Dim TedadRoozPardakhtShodeh As Int16 = 0
            TedadRoozPardakhtShodeh = objTools.ConvertNulls(objTools.DSum("TedadRoozKarKard", " payroll.Sanavat", "ccAfrad = " & cmbAfrad.SelectedValue & " And Sal = " & cmbDore.SelectedValue), 0)



            Dim cmErrorLog As Data.SqlClient.SqlCommand = GetSQLCommandForTransaction()
            Dim TarikhAvalSal As String
            Dim TarikhMohasebehM As String
            Dim TaTarikh As String = mskTaTarikh.Text

            Dim tSal As Integer = CInt(TaTarikh.Substring(0, 4))
            Dim tMah As Byte = CInt(TaTarikh.Substring(4, 2))
            Dim oDorehMaly As Integer = cmbDore.SelectedValue
            TarikhAvalSal = GetTarikhMilady(CType(oDorehMaly, String) + "0101")
            TarikhAvalSal += " 23:59:59"
            oDorehMaly = Nothing

            TarikhMohasebehM = GetTarikhMilady(mskTaTarikh.Text)
            TarikhMohasebehM += " 23:59:59"

            Dim TedadRoozAzAvalSal As Integer = DateDiff(DateInterval.Day, CDate(TarikhAvalSal), CDate(TarikhMohasebehM)) + 1

            Dim i As Integer = 0
            Dim Sal As Integer
            Dim Darsad As Double
            Dim MablaghSabet As Double
            Dim Saghf As Double = 0
            Dim MablaghMohasebeh As Double = 0
            Dim TedadMah As Byte = 0
            Dim strsql As String = ""
            Dim cmsql As New SqlCommand
            Dim da As SqlDataAdapter = New SqlDataAdapter
            Dim dtr As New DataTable
            Dim cn As SqlConnection

            cn = New SqlConnection(ConnectionString)
            cn.Open()


            strsql = "payroll.spMohasebehSanavat_Hokm"
            cmsql = New SqlCommand(strsql, cn)
            cmsql.CommandType = CommandType.StoredProcedure
            cmsql.Parameters.Clear()
            cmsql.Parameters.AddWithValue("AzTarikh", TarikhMohasebehM)
            da = New SqlDataAdapter(cmsql)

            da.Fill(dtr)
            If dtr.Rows.Count = 0 Then
                InsertErroLog(ccMarkazPakhsh, 0, Sal, 0, 110, "اطلاعات احکام پرسنل وجود ندارد", cmErrorLog)
                cmsql.Transaction.Rollback()
                Return False
                Exit Function
            Else
                Saghf = CDbl(dtr.Rows(0)("MozdeSabet"))
                'MablaghMohasebeh = CDbl(dtr.Rows(0)("HadeAghalDastmozd")) * 30
                TedadMah = 1
            End If

            Sal = CInt(cmbDore.SelectedValue)


            MablaghSabet = 0
            MohasebehSanavat(Me.cmbMarkazPakhsh.SelectedValue, Sal, cmbAfrad.SelectedValue, mskTaTarikh.Text, MablaghSabet, Saghf, TedadMah, tSal, tMah, TedadRoozAzAvalSal, TarikhMohasebehM, TarikhAvalSal, cmsql)

            Search_Sanavat(False)

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->MohasebehSanavat")
            Return False
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->MohasebehSanavat")
            Return False
        End Try

        Return True
    End Function

    Private Sub RefreshFormData(ByVal strsql As String)
        Dim daSQL As SqlDataAdapter
        Try
            If Rb_EidyS.Checked Then

                If dsForm.Tables.Contains("Eidy") = True Then
                    dsForm.Tables.Remove("Eidy")
                End If
                If dsForm.Tables.Contains("Sanavat") = True Then
                    dsForm.Tables.Remove("Sanavat")
                End If
                daSQL = New SqlDataAdapter(strsql, ConnectionString)
                daSQL.Fill(dsForm, "Eidy")
                dvForm = New DataView
                dvForm = dsForm.Tables("Eidy").DefaultView
                '------------Adding Columns------------
                dsForm.Tables("Eidy").Columns.Add("Taeed", GetType(Boolean))
                Dim dr As DataRow
                For Each dr In dsForm.Tables("Eidy").Rows
                    dr("Taeed") = False
                Next

            Else
                If dsForm.Tables.Contains("Eidy") = True Then
                    dsForm.Tables.Remove("Eidy")
                End If
                If dsForm.Tables.Contains("Sanavat") = True Then
                    dsForm.Tables.Remove("Sanavat")
                End If
                daSQL = New SqlDataAdapter(strsql, ConnectionString)
                daSQL.Fill(dsForm, "Sanavat")
                dvForm = New DataView
                dvForm = dsForm.Tables("Sanavat").DefaultView
                '------------Adding Columns------------
                dsForm.Tables("Sanavat").Columns.Add("Taeed", GetType(Boolean))
                For Each dr In dsForm.Tables("Sanavat").Rows
                    dr("Taeed") = False
                Next
            End If

            dvForm.AllowDelete = True
            dvForm.AllowNew = False
            daSQL = Nothing

            DGV.DataSource = Nothing
            SetGridStyle(dvForm)



        Catch e As SqlException
            MsgBox(e.Message, MsgBoxStyle.Information, "خطا در عمليات بانک")
        Catch e As Exception
            MsgBox(e.Message, MsgBoxStyle.Information, "خطا")
        End Try
    End Sub
    Private Sub SetGridStyle(ByVal dv As DataView)
        DGV.DataSource = dv
        For i As Integer = 0 To DGV.ColumnCount - 1
            DGV.Columns(i).Visible = False
        Next

        If CType(DGV.DataSource, DataView).Table.Columns.IndexOf("Taeed") <> -1 Then
            DGV.Columns("Taeed").Width = 50
            DGV.Columns("Taeed").HeaderText = "انتخاب"
            DGV.Columns("Taeed").Visible = True
            DGV.Columns("Taeed").DisplayIndex = 0
        End If

        If CType(DGV.DataSource, DataView).Table.Columns.IndexOf("Sal") <> -1 Then
            DGV.Columns("Sal").Width = 50
            DGV.Columns("Sal").HeaderText = "سال"
            DGV.Columns("Sal").Visible = True
            DGV.Columns("Sal").ReadOnly = True
            DGV.Columns("Sal").DisplayIndex = 1
        End If
        If CType(DGV.DataSource, DataView).Table.Columns.IndexOf("FN") <> -1 Then
            DGV.Columns("FN").Width = 150
            DGV.Columns("FN").HeaderText = "نام فرد"
            DGV.Columns("FN").Visible = True
            DGV.Columns("FN").ReadOnly = True
            DGV.Columns("FN").DisplayIndex = 2
        End If
        If CType(DGV.DataSource, DataView).Table.Columns.IndexOf("TedadRoozKarKard") <> -1 Then
            DGV.Columns("TedadRoozKarKard").Width = 80
            DGV.Columns("TedadRoozKarKard").HeaderText = "تعداد روز کارکرد"
            DGV.Columns("TedadRoozKarKard").Visible = True
            DGV.Columns("TedadRoozKarKard").ReadOnly = True
            DGV.Columns("TedadRoozKarKard").DisplayIndex = 3
        End If


        If CType(DGV.DataSource, DataView).Table.Columns.IndexOf("MablaghKol") <> -1 Then
            DGV.Columns("MablaghKol").Width = 150
            DGV.Columns("MablaghKol").HeaderText = "مبلغ اصلی"
            DGV.Columns("MablaghKol").Visible = True
            DGV.Columns("MablaghKol").ReadOnly = True
            DGV.Columns("MablaghKol").DisplayIndex = 4
            DGV.Columns("MablaghKol").DefaultCellStyle.Format = "N"
        End If

        If CType(DGV.DataSource, DataView).Table.Columns.IndexOf("MablaghMohasebeh") <> -1 Then
            DGV.Columns("MablaghMohasebeh").Width = 150
            If Rb_EidyS.Checked Then
                DGV.Columns("MablaghMohasebeh").HeaderText = "مبلغ عیدی"
            Else
                DGV.Columns("MablaghMohasebeh").HeaderText = "مبلغ سنوات"
            End If
            DGV.Columns("MablaghMohasebeh").Visible = True
            DGV.Columns("MablaghMohasebeh").ReadOnly = False
            DGV.Columns("MablaghMohasebeh").DisplayIndex = 5
            DGV.Columns("MablaghMohasebeh").DefaultCellStyle.Format = "N"
        End If
        If CType(DGV.DataSource, DataView).Table.Columns.IndexOf("MablaghMaliat") <> -1 Then
            DGV.Columns("MablaghMaliat").Width = 150
            DGV.Columns("MablaghMaliat").HeaderText = "مبلغ مالیات"
            DGV.Columns("MablaghMaliat").Visible = True
            DGV.Columns("MablaghMaliat").ReadOnly = False
            DGV.Columns("MablaghMaliat").DisplayIndex = 6
            DGV.Columns("MablaghMaliat").DefaultCellStyle.Format = "N"
        End If

        If CType(DGV.DataSource, DataView).Table.Columns.IndexOf("TarikhEntry") <> -1 Then
            DGV.Columns("TarikhEntry").Width = 150
            DGV.Columns("TarikhEntry").HeaderText = "تاریخ درج"
            DGV.Columns("TarikhEntry").Visible = True
            DGV.Columns("TarikhEntry").ReadOnly = True
            DGV.Columns("TarikhEntry").DisplayIndex = 7
            DGV.Columns("TarikhEntry").DefaultCellStyle.Format = "N"
        End If

        Me.CenterToScreen()
    End Sub
    Private Sub MohasebehEidy(ByVal MablaghMohasebeh As Double, ByVal ccMarkazPakhsh As Integer, ByVal Sal As Integer, ByVal ShomarehPersonely As String,
                        ByVal TaTarikh As String, ByVal Darsad As Double, ByVal MablaghSabet As Double,
                        ByVal Saghf As Double, ByVal TedadMah As Byte, ByVal tSal As Integer, ByVal tMah As Byte,
                        ByVal TedadRoozAzAvalSal As Integer, ByVal TarikhMohasebehM As String, ByVal TarikhAvalSal As String,
                        ByVal cm As Data.SqlClient.SqlCommand)
        Dim strSql As String = String.Empty
        Dim da As SqlDataAdapter = New SqlDataAdapter
        Dim p As SqlParameter = Nothing
        Dim dtHokm As New DataTable("Hokm")
        Dim ccTafsily As Integer = 0
        Dim oTafsily As Integer
        Dim cn As SqlConnection
        Try


            cn = New SqlConnection(ConnectionString)
            cn.Open()


            strSql = "payroll.spMohasebehEydi_Mohasebeh"

            cm = New SqlCommand(strSql, cn)
            cm.CommandType = CommandType.StoredProcedure
            cm.Parameters.Clear()

            cm.Parameters.AddWithValue("Sal", tSal)
            cm.Parameters.AddWithValue("Mah", tMah)
            cm.Parameters.AddWithValue("AzTarikh", TarikhMohasebehM)
            cm.Parameters.AddWithValue("CodeMahal", CodeMahalFaal)
            cm.Parameters.AddWithValue("ShomarehPersonely", ShomarehPersonely)
            'p = New SqlParameter("TarikhAvaleSal", SqlDbType.NVarChar, 50)
            'p.Value = TarikhAvalSal
            'cm.Parameters.Add(p)


            cm.CommandText = strSql
            da.SelectCommand = cm
            da.Fill(dtHokm)
            Dim MablaghMozdShoghl As Double
            Dim MablaghKol As Double
            'Dim MablaghMohasebeh As Double
            Dim MablaghPardakhtShodehAzGhabl As Double
            Dim TedadRoozKarKard As Integer
            Dim MM As Double
            'Dim oEidy As New Eidy
            Dim i As Integer

            For i = 0 To dtHokm.Rows.Count - 1
                MablaghMozdShoghl = CDbl(dtHokm.Rows(i)("MablaghMohasebeh"))
                'MablaghMozdShoghl = MablaghMohasebeh
                'TedadRoozKarKard = CInt(dtHokm.Rows(i)("TedadRoozKarKard")) + 29
                TedadRoozKarKard = txtRoozKard.Text

                MablaghKol = MablaghMozdShoghl * TedadMah
                If MablaghKol > Saghf Then MablaghKol = Saghf
                If MablaghSabet > 0 Then
                    MM = MablaghSabet
                ElseIf Darsad > 0 Then
                    MM = Math.Round((MablaghKol * Darsad) / 100, 0)
                End If
                If CodeDoreh = 1399 Then
                    MablaghMohasebeh = Math.Round((TedadRoozKarKard * MM) / 366, 0)
                End If
                If CodeDoreh <> 1399 Then
                    MablaghMohasebeh = Math.Round((TedadRoozKarKard * MM) / 365, 0)
                End If

                MablaghPardakhtShodehAzGhabl = CInt(dtHokm.Rows(i)("MablaghPardakhtShodehAzGhabl"))

                ccTafsily = objTools.ConvertNulls(objTools.DLookup("codetafsily", "tblGL_MoshakhasatFardi", "CodeFard = " & dtHokm.Rows(i)("ccAfrad") & ""), 0)

                'Dim strSQL As String = String.Empty
                'Dim da As SqlDataAdapter = New SqlDataAdapter
                Dim dtMaliat As New DataTable("Maliat")
                Dim j As Integer
                Dim Mablagh As Double = 0
                Dim MablaghGhabl As Double = 0
                'Dim drv As DataRowView
                Dim ccMohasebehHoghoghSatr As Long = 0
                Dim MashmolMaliat As Double = 0
                Dim MablaghMaliat As Double = 0
                Dim tMashmolM As Double = 0


                Dim MaxTarikh As Date
                strSql = " "
                strSql &= "Select @MaxTarikh=Isnull(Max(FromDate),'1990-03-21') From Payroll.Maliat Where FromDate <= @AzTarikh"
                cm.Parameters.Clear()
                cm.CommandType = CommandType.Text
                p = New SqlParameter("AzTarikh", SqlDbType.NVarChar, 50)
                p.Value = TarikhMohasebehM
                cm.Parameters.Add(p)
                p = New SqlParameter("MaxTarikh", SqlDbType.DateTime)
                p.Direction = ParameterDirection.Output
                cm.Parameters.Add(p)
                cm.CommandText = strSql
                cm.ExecuteScalar()
                MaxTarikh = CDate(IIf(String.IsNullOrEmpty(cm.Parameters("MaxTarikh").Value.ToString), CDate("1990-03-21"), cm.Parameters("MaxTarikh").Value))




                tMashmolM = MablaghMohasebeh
                strSql = ""
                strSql &= "Select * "
                strSql &= " From Payroll.Maliat a"
                strSql &= " Where a.FromDate <= @AzTarikh And Year(a.FromDate) = " & MaxTarikh.Year & " And "
                strSql &= " Month(a.FromDate) = " & MaxTarikh.Month & " And "
                strSql &= " Day(a.FromDate) = " & MaxTarikh.Day & " And "
                strSql &= " AzMablagh <= " & tMashmolM & " Order by AzMablagh DESC"
                cm.Parameters.Clear()
                cm.CommandType = CommandType.Text
                p = New SqlParameter("AzTarikh", SqlDbType.NVarChar, 50)
                p.Value = TarikhMohasebehM
                cm.Parameters.Add(p)
                cm.CommandText = strSql
                da.SelectCommand = cm
                da.Fill(dtMaliat)
                For j = 0 To dtMaliat.Rows.Count - 1
                    MablaghMaliat += Math.Round(((tMashmolM - dtMaliat.Rows(i)("AzMablagh")) * dtMaliat.Rows(j)("NerkhMaliat")) / 100, 0)
                    If MablaghMaliat < 0 Then
                        MablaghMaliat = 0
                    Else
                        tMashmolM -= (tMashmolM - dtMaliat.Rows(i)("AzMablagh"))
                    End If
                Next j
                Mablagh = Mablagh

                If MablaghMohasebeh <> 0 Then
                    InsertDataWithCm(ccMarkazPakhsh, dtHokm.Rows(i)("ccAfrad"), Sal, MablaghMozdShoghl, TaTarikh, Darsad, MM,
                    TedadRoozKarKard, MablaghKol, MablaghMohasebeh, MablaghPardakhtShodehAzGhabl, 2, tSal.ToString.PadLeft(4, "0") + tMah.ToString.PadLeft(2, "0"), MablaghMaliat, MashmolMaliat, ccTafsily, cm)
                End If
            Next
            'oEidy = Nothing


            If cm.Transaction IsNot Nothing Then
                cm.Transaction.Commit()
                MsgBox(" خطا در محاسبه عیدی  ", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
            End If



        Catch ex As SqlException
            cm.Transaction.Rollback()
            Throw New Exception("Error In MohasebehEidy --> " & ex.Message)
        Catch ex As Exception
            cm.Transaction.Rollback()
            Throw New Exception("Error In MohasebehEidy --> " & ex.Message)
        Finally

        End Try

    End Sub



    Private Sub MohasebehSanavat(ByVal ccMarkazPakhsh As Integer, ByVal Sal As Integer, ByVal ShomarehPersonely As String,
                        ByVal TaTarikh As String, ByVal MablaghSabet As Double,
                        ByVal Saghf As Double, ByVal TedadMah As Byte, ByVal tSal As Integer, ByVal tMah As Byte,
                        ByVal TedadRoozAzAvalSal As Integer, ByVal TarikhMohasebehM As String, ByVal TarikhAvalSal As String,
                        ByVal cm As Data.SqlClient.SqlCommand)
        Dim strSql As String = String.Empty
        Dim da As SqlDataAdapter = New SqlDataAdapter
        Dim p As SqlParameter = Nothing
        Dim dtHokm As New DataTable("Hokm")
        Dim ccTafsily As Integer = 0
        Dim cn As SqlConnection
        Try

            cn = New SqlConnection(ConnectionString)
            cn.Open()
            strSql = "payroll.spMohasebehSanavat_Mohasebeh"
            cm = New SqlCommand(strSql, cn)
            cm.CommandType = CommandType.StoredProcedure
            cm.Parameters.Clear()
            cm.Parameters.AddWithValue("Sal", tSal)
            cm.Parameters.AddWithValue("Mah", tMah)
            cm.Parameters.AddWithValue("AzTarikh", TarikhMohasebehM)
            cm.Parameters.AddWithValue("CodeMahal", CodeMahalFaal)
            cm.Parameters.AddWithValue("ShomarehPersonely", ShomarehPersonely)

            cm.CommandText = strSql
            da.SelectCommand = cm
            da.Fill(dtHokm)
            Dim MablaghMohasebeh As Double
            Dim MablaghMozdShoghl As Double
            Dim MablaghKol As Double
            'Dim MablaghMohasebeh As Double
            Dim TedadRoozKarKard As Integer
            Dim MM As Double
            Dim i As Integer

            For i = 0 To dtHokm.Rows.Count - 1
                MablaghMozdShoghl = CDbl(dtHokm.Rows(i)("MablaghMohasebeh"))
                'MablaghMozdShoghl = MablaghMohasebeh
                TedadRoozKarKard = txtRoozKard.Text
                MablaghKol = MablaghMozdShoghl * TedadMah
                If MablaghKol > Saghf Then MablaghKol = Saghf
                If MablaghSabet > 0 Then
                    MM = MablaghSabet
                Else
                    MM = MablaghKol
                End If
                If CodeDoreh = 1399 Then
                    MablaghMohasebeh = Math.Round((TedadRoozKarKard * MM) / 366, 0)
                End If
                If CodeDoreh <> 1399 Then
                    MablaghMohasebeh = Math.Round((TedadRoozKarKard * MM) / 365, 0)
                End If
                'MablaghMohasebeh = Math.Round((TedadRoozKarKard * MM) / 365, 0)

                ccTafsily = objTools.ConvertNulls(objTools.DLookup("codetafsily", "tblGL_MoshakhasatFardi", "CodeFard = " & dtHokm.Rows(i)("ccAfrad") & ""), 0)

                Dim Mablagh As Double = 0
                Dim MablaghGhabl As Double = 0
                Dim ccMohasebehHoghoghSatr As Long = 0

                If MablaghMohasebeh <> 0 Then
                    InsertsanavatWithCM(ccMarkazPakhsh, dtHokm.Rows(i)("ccAfrad"), Sal, MablaghMozdShoghl, TaTarikh, MM,
                    TedadRoozKarKard, MablaghKol, MablaghMohasebeh, 2, tSal.ToString.PadLeft(4, "0") + tMah.ToString.PadLeft(2, "0"), ccTafsily, cm)
                End If
            Next


            ''

            If cm.Transaction IsNot Nothing Then
                cm.Transaction.Commit()
                MsgBox(" خطا در محاسبه سنوات  ", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
            End If

        Catch ex As SqlException
            cm.Transaction.Rollback()
            Throw New Exception("Error In MohasebehEidy --> " & ex.Message)

            MsgBox(" خطا در محاسبه سنوات  ", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
        Catch ex As Exception
            cm.Transaction.Rollback()
            Throw New Exception("Error In MohasebehEidy --> " & ex.Message)

            MsgBox(" خطا در محاسبه سنوات  ", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
        Finally

        End Try

    End Sub
    Sub InsertDataWithCm(ByVal ccMarkazPakhsh As Integer, ByVal ccAfrad As Integer, ByVal Sal As Integer, ByVal MablaghHoghogh As Double,
                  ByVal TaTarikh As String, ByVal Darsad As Double, ByVal MablaghSabet As Double, ByVal TedadRoozKarKard As Integer, ByVal MablaghKol As Double,
                  ByVal MablaghMohasebeh As Double, ByVal MablaghPardakhtShodehAzGhabl As Double,
                  ByVal CodeNoeVorod As Byte, ByVal SalMahPardakht As String, ByVal MablaghMaliat As Double,
                  ByVal MashmolMaliat As Double, ByVal ccTafsily As Integer, ByVal cm As Data.SqlClient.SqlCommand)

        InsertDataWithCM(ccMarkazPakhsh, ccAfrad, Sal, MablaghHoghogh, mskTaTarikh.Text, Darsad, MablaghSabet, TedadRoozKarKard,
                                     MablaghKol, MablaghMohasebeh, MablaghPardakhtShodehAzGhabl, 2, SalMahPardakht, MablaghMaliat, MashmolMaliat, ccTafsily, 1, cm)

    End Sub 'InsertData
    Sub InsertSanavatWithCm(ByVal ccMarkazPakhsh As Integer, ByVal ccAfrad As Integer, ByVal Sal As Integer, ByVal MablaghHoghogh As Double,
                  ByVal TaTarikh As String, ByVal Darsad As Double, ByVal MablaghSabet As Double, ByVal TedadRoozKarKard As Integer, ByVal MablaghKol As Double,
                  ByVal MablaghMohasebeh As Double, ByVal MablaghPardakhtShodehAzGhabl As Double,
                  ByVal CodeNoeVorod As Byte, ByVal SalMahPardakht As String, ByVal MablaghMaliat As Double,
                  ByVal MashmolMaliat As Double, ByVal ccTafsily As Integer, ByVal cm As Data.SqlClient.SqlCommand)


        InsertsanavatWithCM(ccMarkazPakhsh, ccAfrad, Sal, MablaghHoghogh, mskTaTarikh.Text, MablaghSabet, TedadRoozKarKard, MablaghKol, MablaghMohasebeh, MablaghPardakhtShodehAzGhabl, ccTafsily, 1, cm)

    End Sub 'InsertData

    Function InsertsanavatWithCM(ByVal ccMarkazPakhsh As Integer, ByVal ccAfrad As Integer, ByVal Sal As Integer, ByVal MablaghHoghogh As Double,
                           ByVal TaTarikh As String, ByVal MablaghSabet As Double, ByVal TedadRoozKarKard As Integer, ByVal MablaghKol As Double,
                           ByVal MablaghMohasebeh As Double, ByVal MablaghPardakhtShodehAzGhabl As Double,
                              ByVal ccTafsily As Integer, ByVal ccUser As Integer, ByVal cm As Data.SqlClient.SqlCommand) As Long


        Dim p As SqlParameter = Nothing
        Dim strSQL As String = String.Empty
        Dim Description As String = String.Empty
        Dim cn As SqlConnection

        cm.Parameters.Clear()
        Try
            Dim ccSanavat As Integer = 0
            ccSanavat = objTools.ConvertNulls(objTools.DLookup("ccSanavat", "Payroll.Sanavat", "ccAfrad = " & ccAfrad & " AND sal = " & Sal & " AND ccMarkazPakhsh = " & ccMarkazPakhsh & " "), 0)
            If ccSanavat <> 0 Then
                objTools.DDelete("Payroll.Sanavat", "ccSanavat = " & ccSanavat & " and svazeiat <> 3 ")
            End If




            cn = New SqlConnection(ConnectionString)
            cn.Open()

            strSQL &= "payroll.spMohasebehMablaghSanavat_Insert"

            cm = New SqlCommand(strSQL, cn)
            cm.CommandType = CommandType.StoredProcedure
            cm.Parameters.Clear()

            cm.Parameters.AddWithValue("ccMarkazPakhsh", ccMarkazPakhsh)
            cm.Parameters.AddWithValue("ccAfrad", ccAfrad)
            cm.Parameters.AddWithValue("Sal", Sal)
            cm.Parameters.AddWithValue("MablaghHoghogh", MablaghHoghogh)
            cm.Parameters.AddWithValue("TaTarikh", TaTarikh)

            cm.Parameters.AddWithValue("TedadRoozKarKard", TedadRoozKarKard)
            cm.Parameters.AddWithValue("MablaghKol", MablaghKol)
            cm.Parameters.AddWithValue("MablaghMohasebeh", MablaghMohasebeh)
            cm.Parameters.AddWithValue("MablaghGhabelPardakht", MablaghMohasebeh)

            cm.Parameters.AddWithValue("TarikhEntry", TarikhEmrooz)
            cm.Parameters.AddWithValue("ModifiedDate", TarikhEmrooz)
            cm.Parameters.AddWithValue("ccTafsily", ccTafsily)
            cm.Parameters.AddWithValue("Ident", ParameterDirection.Output)


            cm.CommandText = strSQL
            cm.ExecuteNonQuery()
            InsertsanavatWithCM = cm.Parameters("Ident").Value
            Description = "ccAfrad=" & ccAfrad & "; Sal=" & Sal & "; MablaghKol=" & MablaghKol & "; MablaghMohasebeh=" & MablaghMohasebeh &
            "; MablaghPardakhtShodehAzGhabl=" & MablaghPardakhtShodehAzGhabl

        Catch ex As SqlException
            cm.Transaction.Rollback()
            Throw New Exception("Error In MohasebehEidy --> " & ex.Message)
        Catch ex As Exception
            cm.Transaction.Rollback()
            Throw New Exception("Error In MohasebehEidy --> " & ex.Message)
        Finally

        End Try
    End Function
    Function InsertDataWithCM(ByVal ccMarkazPakhsh As Integer, ByVal ccAfrad As Integer, ByVal Sal As Integer, ByVal MablaghHoghogh As Double,
                           ByVal TaTarikh As String, ByVal Darsad As Double, ByVal MablaghSabet As Double, ByVal TedadRoozKarKard As Integer, ByVal MablaghKol As Double,
                           ByVal MablaghMohasebeh As Double, ByVal MablaghPardakhtShodehAzGhabl As Double,
                           ByVal CodeNoeVorod As Byte, ByVal SalMahPardakht As String, ByVal MablaghMaliat As Double,
                           ByVal MashmolMaliat As Double, ByVal ccTafsily As Integer, ByVal ccUser As Integer,
                           ByVal cm As Data.SqlClient.SqlCommand) As Long


        Dim p As SqlParameter = Nothing
        Dim strSQL As String = String.Empty
        Dim Description As String = String.Empty
        Dim cn As SqlConnection

        cm.Parameters.Clear()
        Try
            Dim ccEidy As Integer = 0
            ccEidy = objTools.ConvertNulls(objTools.DLookup("ccEidy", "Payroll.Eidy", "ccAfrad = " & ccAfrad & " AND sal = " & Sal & " AND ccMarkazPakhsh = " & ccMarkazPakhsh & " "), 0)
            If ccEidy <> 0 Then
                objTools.DDelete("Payroll.Eidy", "ccEidy = " & ccEidy & "and svazeiat <> 3 ")
            End If

            cn = New SqlConnection(ConnectionString)
            cn.Open()

            strSQL &= "payroll.spMohasebehMablaghEydi_Insert"

            cm = New SqlCommand(strSQL, cn)
            cm.CommandType = CommandType.StoredProcedure
            cm.Parameters.Clear()

            cm.Parameters.AddWithValue("ccMarkazPakhsh", ccMarkazPakhsh)
            cm.Parameters.AddWithValue("ccAfrad", ccAfrad)
            cm.Parameters.AddWithValue("Sal", Sal)
            cm.Parameters.AddWithValue("MablaghHoghogh", MablaghHoghogh)
            cm.Parameters.AddWithValue("TaTarikh", TaTarikh)
            cm.Parameters.AddWithValue("Darsad", Darsad)
            cm.Parameters.AddWithValue("MablaghSabet", MablaghSabet)
            cm.Parameters.AddWithValue("TedadRoozKarKard", TedadRoozKarKard)
            cm.Parameters.AddWithValue("MablaghKol", MablaghKol)
            cm.Parameters.AddWithValue("MablaghMohasebeh", MablaghMohasebeh)
            cm.Parameters.AddWithValue("MablaghPardakhtShodehAzGhabl", MablaghPardakhtShodehAzGhabl)
            cm.Parameters.AddWithValue("MablaghGhabelPardakht", MablaghMohasebeh - MablaghMaliat)
            cm.Parameters.AddWithValue("CodeNoeVorod", CodeNoeVorod)
            cm.Parameters.AddWithValue("SalMahPardakht", SalMahPardakht)
            cm.Parameters.AddWithValue("TarikhEntry", TarikhEmrooz)
            cm.Parameters.AddWithValue("ModifiedDate", TarikhEmrooz)
            cm.Parameters.AddWithValue("MablaghMaliat", MablaghMaliat)
            cm.Parameters.AddWithValue("MashmolMaliat", MashmolMaliat)
            cm.Parameters.AddWithValue("ccTafsily", ccTafsily)
            cm.Parameters.AddWithValue("Ident", ParameterDirection.Output)


            cm.CommandText = strSQL
            cm.ExecuteNonQuery()
            InsertDataWithCM = cm.Parameters("Ident").Value
            Description = "ccAfrad=" & ccAfrad & "; Sal=" & Sal & "; MablaghKol=" & MablaghKol & "; MablaghMohasebeh=" & MablaghMohasebeh &
            "; MablaghPardakhtShodehAzGhabl=" & MablaghPardakhtShodehAzGhabl & "; MablaghMaliat=" & MablaghMaliat

        Catch ex As SqlException
            cm.Transaction.Rollback()
            Throw New Exception("Error In MohasebehEidy --> " & ex.Message)
        Catch ex As Exception
            cm.Transaction.Rollback()
            Throw New Exception("Error In MohasebehEidy --> " & ex.Message)
        Finally

        End Try
    End Function 'InsertDataWithCM
    Private Sub btnMohasebe_Click(sender As Object, e As EventArgs) Handles btnMohasebe.Click
        If Rb_Eidy.Checked Then
            If MohasebehEidy() = True Then
                MessageBox.Show("محاسبه عیدی با موفقیت انجام شد")
            Else
                MessageBox.Show("محاسبه انجام نشد")
            End If
            Rb_EidyS.Checked = True
        ElseIf Rb_Sanavat.Checked Then
            If MohasebehSanavat() = True Then
                MessageBox.Show("محاسبه سنوات با موفقیت انجام شد")
            Else
                MessageBox.Show("محاسبه انجام نشد")
            End If
            Rb_SanavatS.Checked = True
        End If
    End Sub
    Sub InsertErroLog(ByVal ccMarkazPakhsh As Integer, ByVal ccAfrad As Long, ByVal Sal As Integer, ByVal Mah As Byte, ByVal ErrorCode As Integer,
         ByVal MsgError As String, ByVal cm As SqlCommand)

        Dim strSQL As String = String.Empty
        Try
            strSQL = ""
            strSQL &= "Insert into Payroll.MohasebehHoghoghErrorLog "
            strSQL &= "(ccMarkazPakhsh,ccAfrad,Sal,Mah,ErrorCode,MsgError)"
            strSQL &= "Values (" & ccMarkazPakhsh & "," & ccAfrad & "," & Sal & "," & Mah & "," & ErrorCode & ",'" & MsgError & "')"
            cm.CommandText = strSQL
            cm.ExecuteNonQuery()
        Catch ex As SqlException
            Throw New Exception("Error In InsertErroLog --> " & ex.Message)
        Catch ex As Exception
            Throw New Exception("Error In  InsertErroLog --> " & ex.Message)
        Finally
        End Try
    End Sub
    Public Function GetSQLCommandForTransaction() As SqlCommand
        Dim cm As SqlCommand = New SqlCommand

        cm.Connection = New SqlConnection
        cm.Connection.ConnectionString = ConnectionString
        cm.Connection.Open()
        cm.Transaction = cm.Connection.BeginTransaction

        Return cm
    End Function
    Function GetTarikhMilady(ByVal DateShamsi As String) As String
        Dim cm As SqlCommand = New SqlCommand
        Dim cn As SqlConnection = New SqlConnection
        Dim p As SqlParameter = Nothing
        Dim strSQL As String = String.Empty
        Try

            strSQL = "Select [dbo].[fnGL_ConvertToMiladi](@DateShamsi)"

            p = New SqlParameter("DateShamsi", SqlDbType.NVarChar, 50)
            p.Value = DateShamsi
            cm.Parameters.Add(p)

            cn.ConnectionString = ConnectionString
            cn.Open()
            cm.CommandText = strSQL
            cm.Connection = cn


        Catch ex As SqlException
            MsgBox(ex.Message)
        Finally
            GetTarikhMilady = cm.ExecuteScalar

            cn.Close()
            cn.Dispose()
            cm.Dispose()
        End Try
    End Function
    Private Sub cmbAfrad_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbAfrad.SelectedIndexChanged

        Dim dt As Data.DataTable = Nothing
        Dim da As SqlDataAdapter = New SqlDataAdapter
        Dim dsForm As New DataSet

        Try
            If Flag = False Then Exit Sub
            If cmbAfrad.SelectedIndex <> -1 Then
                If cmbDore.SelectedValue = -1 Then
                    MessageBox.Show("دوره را انتخاب نمایید")
                    Exit Sub
                End If
                If cmbMarkazPakhsh.SelectedValue = -1 Then
                    MessageBox.Show("مرکز پخش را انتخاب نمایید")
                    Exit Sub
                End If
                '-------------- Load Combo Mah
                dt = Nothing
                Using cn As New SqlConnection(ConnectionString)
                    Using cm As SqlCommand = cn.CreateCommand()
                        cn.Open()
                        cm.Parameters.Clear()
                        cm.CommandType = CommandType.StoredProcedure
                        cm.CommandText = "payroll.spMohasebehEydi_RoozKarkard"
                        cm.Parameters.AddWithValue("ccAfrad", cmbAfrad.SelectedValue)
                        cm.Parameters.AddWithValue("CodeDoreh", cmbDore.SelectedValue)
                        txtRoozKard.Text = objTools.ConvertNulls(cm.ExecuteScalar(), 0)
                        TedadRK = txtRoozKard.Text
                    End Using
                End Using

            End If

        Catch ex As Exception
            Throw New Exception("Error In--> Load Combo : " & ex.Message)
        Finally
        End Try
    End Sub

    Private Sub cmbMarkazPakhsh_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbMarkazPakhsh.SelectedIndexChanged

        Dim dt As Data.DataTable = Nothing
        Dim da As SqlDataAdapter = New SqlDataAdapter
        Dim dsForm As New DataSet

        Try

            dt = Nothing
            If Flag = True Then
                ''If cmbMarkazPakhsh.SelectedIndex <> 0 And cmbMarkazPakhsh.SelectedIndex <> -1 Then

                Using cn As New SqlConnection(ConnectionString)
                    Using cm As SqlCommand = cn.CreateCommand()
                        cn.Open()
                        cm.Parameters.Clear()
                        cm.CommandType = CommandType.StoredProcedure
                        cm.CommandText = "[Payroll].[spMohasebehEydi_LoadComboPersonel]"
                        cm.Parameters.AddWithValue("CodeMahal", cmbMarkazPakhsh.SelectedValue)
                        cm.Parameters.AddWithValue("CodeDoreh", CodeDoreh)
                        da.SelectCommand = cm
                        dt = New DataTable
                        da.Fill(dt)
                    End Using
                End Using
                cmbAfrad.DataSource = dt
                cmbAfrad.DisplayMember = "FN"
                cmbAfrad.ValueMember = "CodeFard"

            End If
        Catch ex As Exception
            Throw New Exception("Error In--> Load Combo : " & ex.Message)
        Finally
        End Try
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click

        Dim cn As SqlConnection
        Dim cm As SqlCommand
        Dim da As SqlDataAdapter
        Dim strsql As String = ""
        Dim dsForm As New DataSet

        cn = New SqlConnection(ConnectionString)
        cn.Open()
        If Rb_EidyS.Checked Then
            strsql = "payroll.spMohasebehEydi_PrintEydi"
            cm = New SqlCommand(strsql, cn)
            cm.CommandType = CommandType.StoredProcedure
            cm.Parameters.Clear()
            cm.Parameters.AddWithValue("Sal", IIf(CmbDoreS.SelectedIndex = -1, CodeDoreh, CmbDoreS.SelectedValue))
            cm.Parameters.AddWithValue("CodeMahal", IIf(CmbMarkazS.SelectedIndex = -1, CodeMahalFaal, CmbMarkazS.SelectedValue))
            da = New SqlDataAdapter(cm)
            da.Fill(dsForm, "Eidy")
            If dsForm.Tables("Eidy").Rows.Count = 0 Then
                MsgBox("هیـــــچ رکوردی برای گزارش پیدا نشد.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "پیام")
                Exit Sub
            End If
        Else
            strsql = "payroll.spMohasebehSanavat_PrintSanavat"
            cm = New SqlCommand(strsql, cn)
            cm.CommandType = CommandType.StoredProcedure
            cm.Parameters.Clear()
            cm.Parameters.AddWithValue("Sal", IIf(CmbDoreS.SelectedIndex = -1, CodeDoreh, CmbDoreS.SelectedValue))
            cm.Parameters.AddWithValue("CodeMahal", IIf(CmbMarkazS.SelectedIndex = -1, CodeMahalFaal, CmbMarkazS.SelectedValue))
            da = New SqlDataAdapter(cm)
            da.Fill(dsForm, "Sanavat")
            If dsForm.Tables("Sanavat").Rows.Count = 0 Then
                MsgBox("هیـــــچ رکوردی برای گزارش پیدا نشد.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "پیام")
                Exit Sub
            End If
        End If


        Dim rpt As New ReportDocument
        Dim rpttables As Tables
        Dim rptformula As FormulaFieldDefinitions
        Dim frm As New Forms_dll.frmGL_Gozaresh
        Dim rptName As String = ""

        ' Dim rptPath As String = objTools.ConvertNulls(objTools.DLookup("ReportPath", "tblGL_Sherkat", "CodeSherkat = " & CodeSherkat), "")

        If chkKoli.Checked Then
            If Rb_EidyS.Checked Then
                rpt.Load(rptPath & "\rptHO_EidyKoli.rpt")
                rpttables = rpt.Database.Tables
                rpttables.Item(0).SetDataSource(dsForm.Tables("Eidy"))
            Else
                rpt.Load(rptPath & "\rptHO_SanavatKoli.rpt")
                rpttables = rpt.Database.Tables
                rpttables.Item(0).SetDataSource(dsForm.Tables("Sanavat"))
            End If
        Else

            If Rb_EidyS.Checked Then
                rpt.Load(rptPath & "\rptHO_Eidy.rpt")
                rpttables = rpt.Database.Tables
                rpttables.Item(0).SetDataSource(dsForm.Tables("Eidy"))
            Else
                rpt.Load(rptPath & "\rptHO_Eidy.rpt")
                rpttables = rpt.Database.Tables
                rpttables.Item(0).SetDataSource(dsForm.Tables("Sanavat"))
            End If
        End If


        rptformula = rpt.DataDefinition.FormulaFields
        With rptformula
            If chkKoli.Checked Then
                .Item("Group_Sanad").Text = "{mydata.CodeFard}"

            Else

                .Item("Group_Sanad").Text = "{mydata.sal}"
            End If

            .Item("namemahal").Text = "{mydata.namemahal}"
            .Item("Sal").Text = "{mydata.Sal}"
            .Item("NamePersonel").Text = "{mydata.NamePersonel}"
            .Item("ShomarehPersonely").Text = "{mydata.ShomarehPersonely}"
            .Item("mablaghmohasebeh").Text = "{mydata.mablaghmohasebeh}"
            .Item("MablaghMaliat").Text = "{mydata.MablaghMaliat}"
            .Item("MablaghGhabelPardakht").Text = "{mydata.MablaghGhabelPardakht}"
            .Item("TedadRoozKarKard").Text = "{mydata.TedadRoozKarKard}"
            .Item("shomarehhesab").Text = "{mydata.shomarehhesab}"

            If Rb_EidyS.Checked Then
                .Item("NameGozaresh").Text = "'عیدی'"
            Else
                .Item("NameGozaresh").Text = "'سنوات'"
            End If

            .Item("KarbarGozaresh").Text = "'" & PersonelName & "'"
            .Item("TarikhGozaresh").Text = "'" & objTarikh.SetDateSlash(TarikhEmrooz) & "'"
            .Item("SaatGozaresh").Text = "'" & Format(TimeOfDay, "HH:mm:ss") & "'"
        End With
        rpt.Refresh()

        frm.Text = txtCaption

        With frm.CRV
            .ReportSource = rpt
            .DisplayGroupTree = False
            .Zoom(100)
            .ShowGroupTreeButton = False
        End With


        Me.Hide()
        frm.ShowDialog(Me)
        frm = Nothing
        rpt = Nothing
        Me.Show()
        Windows.Forms.Cursor.Current = Cursors.Default
    End Sub

    Private Sub BtnSearch_Click(sender As Object, e As EventArgs) Handles BtnSearch.Click
        If Rb_EidyS.Checked Then
            Search_Eidy(True)
        ElseIf Rb_SanavatS.Checked Then

            Search_Sanavat(True)
        End If

    End Sub

    Private Sub BtnErsal_Click(sender As Object, e As EventArgs) Handles BtnErsal.Click
        If MsgBox("آيا از انتخاب خود مطمئن هستيد ؟", MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2 + MsgBoxStyle.Question + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.MsgBoxRight, "تایید") = MsgBoxResult.Yes Then
            If TaeedSanad() = False Then Exit Sub
            If Rb_EidyS.Checked Then
                Search_Eidy(True)
            ElseIf Rb_SanavatS.Checked Then

                Search_Sanavat(True)
            End If
        End If
    End Sub

    Private Sub Rb_EidyS_CheckedChanged(sender As Object, e As EventArgs) Handles Rb_EidyS.CheckedChanged
        If Rb_EidyS.Checked And Flag = True Then
            Search_Eidy(True)
        End If
    End Sub

    Private Sub Rb_SanavatS_CheckedChanged(sender As Object, e As EventArgs) Handles Rb_SanavatS.CheckedChanged
        If Rb_SanavatS.Checked And Flag = True Then
            Search_Sanavat(True)
        End If
    End Sub
    Private Function TaeedSanad() As Boolean
        Dim cnSQL As SqlConnection
        cnSQL = New SqlConnection(ConnectionString)
        cnSQL.Open()
        Taeed(cnSQL)
        Return True
    End Function
    Private Function DeleteSanad() As Boolean
        Dim cnSQL As SqlConnection
        cnSQL = New SqlConnection(ConnectionString)
        cnSQL.Open()
        DeleteRecord(cnSQL)
        Return True
    End Function

    Private Function Taeed(ByVal cnSql As SqlConnection) As Boolean
        Dim cmSQL As SqlCommand
        Dim strSql As String = ""
        Dim Radif As Integer = 0
        Dim strCC As String = ""
        Dim Noe As Integer = 0
        Try

            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            cnSql = New SqlConnection(ConnectionString)
            cnSql.Open()
            For Each drv In dvForm
                If drv("Taeed") = True Then
                    If dsForm.Tables.Contains("Eidy") = True Then
                        Try
                            strSql = "Update [Payroll].[Eidy] Set "
                            strSql &= " TarikhEntry = " & TarikhEmrooz & ","
                            strSql &= " sVazeiat = " & 2 & " "
                            strSql &= " Where ccEidy = " & drv("ccEidy")
                            cmSQL = New SqlCommand(strSql, cnSql)
                            cmSQL.CommandTimeout = 99999
                            cmSQL.ExecuteNonQuery()
                            ' Insert To Log Table
                            ObjCode.SabteTaghirat(CodeMahalFaal, UD_Dll.Enums.GL_NoeTaghir.UpdateRecord, "Payroll.Eidy", drv("ccEidy"), drv("ccAfrad"), "تایید عیدی")
                        Catch sqlExc As SqlException
                            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase")
                        Catch Exc As Exception
                            MsgBox(Exc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN Form")
                        End Try
                    ElseIf dsForm.Tables.Contains("Sanavat") = True Then
                        Try
                            strSql = "Update [Payroll].[Sanavat] Set "
                            strSql &= " TarikhEntry = " & TarikhEmrooz & ","
                            strSql &= " sVazeiat = " & 2 & " "
                            strSql &= " Where ccSanavat = " & drv("ccSanavat")
                            cmSQL = New SqlCommand(strSql, cnSql)
                            cmSQL.CommandTimeout = 99999
                            cmSQL.ExecuteNonQuery()
                            ' Insert To Log Table
                            ObjCode.SabteTaghirat(CodeMahalFaal, UD_Dll.Enums.GL_NoeTaghir.UpdateRecord, "Payroll.Sanavat", drv("ccSanavat"), drv("ccAfrad"), "تایید عیدی")
                        Catch sqlExc As SqlException
                            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase")
                        Catch Exc As Exception
                            MsgBox(Exc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN Form")
                        End Try
                    End If
                    cmSQL = Nothing
                End If
            Next
        Catch ex As Exception
            MsgBox(ex.Message)
            Return False
        Finally
            cnSql.Close()
            cnSql = Nothing
        End Try
        Return True
    End Function

    Private Function DeleteRecord(ByVal cnSql As SqlConnection) As Boolean
        Dim cmSQL As SqlCommand
        Dim strSql As String = ""
        Dim Radif As Integer = 0
        Dim strCC As String = ""
        Dim Noe As Integer = 0
        Try

            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            cnSql = New SqlConnection(ConnectionString)
            cnSql.Open()
            For Each drv In dvForm
                If drv("Taeed") = True Then
                    If dsForm.Tables.Contains("Eidy") Then
                        Try
                            strSql = "delete from  [Payroll].[Eidy]  "
                            strSql &= " Where ccEidy = " & drv("ccEidy")
                            cmSQL = New SqlCommand(strSql, cnSql)
                            cmSQL.CommandTimeout = 99999
                            cmSQL.ExecuteNonQuery()
                            ' Insert To Log Table
                            ObjCode.SabteTaghirat(CodeMahalFaal, UD_Dll.Enums.GL_NoeTaghir.UpdateRecord, "Payroll.Eidy", drv("ccEidy"), drv("ccAfrad"), "حذف عیدی")
                        Catch sqlExc As SqlException
                            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase")
                        Catch Exc As Exception
                            MsgBox(Exc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN Form")
                        End Try
                    ElseIf dsForm.Tables.Contains("Sanavat") = True Then
                        dsForm.Tables.Remove("Sanavat")

                        Try
                            strSql = " delete from  [Payroll].[Sanavat]   "
                            strSql &= " Where ccSanavat = " & drv("ccSanavat")
                            cmSQL = New SqlCommand(strSql, cnSql)
                            cmSQL.CommandTimeout = 99999
                            cmSQL.ExecuteNonQuery()
                            ' Insert To Log Table
                            ObjCode.SabteTaghirat(CodeMahalFaal, UD_Dll.Enums.GL_NoeTaghir.UpdateRecord, "Payroll.Sanavat", drv("ccSanavat"), drv("ccAfrad"), "حذف سنوات")
                        Catch sqlExc As SqlException
                            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase")
                        Catch Exc As Exception
                            MsgBox(Exc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN Form")
                        End Try
                    End If
                    cmSQL = Nothing
                End If
            Next
        Catch ex As Exception
            MsgBox(ex.Message)
            Return False
        Finally
            cnSql.Close()
            cnSql = Nothing
        End Try
        Return True
    End Function

    Private Sub BtnDelete_Click(sender As Object, e As EventArgs) Handles BtnDelete.Click
        If MsgBox("آيا از انتخاب خود برای حذف مطمئن هستيد ؟", MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2 + MsgBoxStyle.Question + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.MsgBoxRight, "تایید") = MsgBoxResult.Yes Then
            If DeleteSanad() = False Then Exit Sub
            If Rb_EidyS.Checked Then
                Search_Eidy(True)
            ElseIf Rb_SanavatS.Checked Then

                Search_Sanavat(True)
            End If
        End If
    End Sub



    Private Sub btnSaveChange_Click(sender As Object, e As EventArgs) Handles btnSaveChange.Click

        If dsForm.Tables.Contains("Eidy") = True Then
            If dsForm.Tables("Eidy").GetChanges(DataRowState.Modified).Rows.Count <> 0 Then
                For Each dr As DataRow In dsForm.Tables("Eidy").GetChanges(DataRowState.Modified).Rows
                    objTools.DUpdate("MablaghMohasebeh", " Payroll.Eidy", " " & dr("MablaghMohasebeh") & " ", "ccEidy = " & dr("ccEidy"))
                    objTools.DUpdate("MablaghMaliat", " Payroll.Eidy", " " & dr("MablaghMaliat") & " ", "ccEidy = " & dr("ccEidy"))
                    objTools.DUpdate("MablaghGhabelPardakht", " Payroll.Eidy", "MablaghMohasebeh - MablaghMaliat ", "ccEidy = " & dr("ccEidy"))
                Next
            End If
        End If
        If dsForm.Tables.Contains("Sanavat") = True Then
            If dsForm.Tables("Sanavat").GetChanges(DataRowState.Modified).Rows.Count <> 0 Then
                For Each dr As DataRow In dsForm.Tables("Sanavat").GetChanges(DataRowState.Modified).Rows
                    objTools.DUpdate("MablaghMohasebeh", " Payroll.Sanavat", " " & dr("MablaghMohasebeh") & " ", "ccSanavat = " & dr("ccSanavat"))
                    objTools.DUpdate("MablaghGhabelPardakht", " Payroll.Sanavat", " " & dr("MablaghMohasebeh") & " ", "ccSanavat = " & dr("ccSanavat"))
                Next
            End If
        End If
        MsgBox("عمليــات ذخيــــره سـازی انجــام شـــد .", MsgBoxStyle.MsgBoxRtlReading Or MsgBoxStyle.MsgBoxRight Or MsgBoxStyle.Information, "ذخيره")
        ClearForm()
        If Rb_EidyS.Checked Then
            Search_Eidy(False)
        ElseIf Rb_SanavatS.Checked Then

            Search_Sanavat(False)
        End If
    End Sub
End Class
