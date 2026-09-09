Public Class frmFO_GozareshRoozanehPakhsh
#Region "Variable AND Constant Declration"
    Dim cntCodeSubSystem As Long = 100102
    Dim dvTitr_ForoshKala As DataView
    Dim dvTitr_Tafkik_Ranandeh As DataView
    Dim dvTitr_Marjoee As DataView
    Dim dvTitr_FaktorResidi As DataView
    Dim ErrPro As New ErrorProvider
    Dim dsForm As New DataSet
    Dim dvForm As DataView
    Private SN As Integer
    Dim txtCaption As String
    Private WithEvents BS As New UD_Dll.PassString
    Dim flag As Boolean = False
#End Region
#Region "Form Event Code"
    Private Sub frmFO_GozareshRoozanehPakhsh_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            SetParameter()
            SN = objSec.GetSecurityNumber(cntCodeSubSystem, UserName)
            mskTarikh.Text = TarikhEmrooz
            LoadCombo()
            flag = True
            cmbMarkazPakhsh.SelectedValue = CodeMahalFaal
            LoadForm()
            cmbNoeGozaresh.SelectedIndex = 0
            cmbNoeGozaresh.SelectedIndex = 0
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->frm_Load")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->frm_Load")
        End Try
    End Sub
    Private Sub cmbMarkazPakhsh_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbMarkazPakhsh.SelectedIndexChanged
        If flag = True Then
            LoadForm()
        End If
    End Sub
#End Region
#Region "Global Form Code"
    Private Sub SetParameter()
        Dim commands As String = Microsoft.VisualBasic.Command()
        If commands.Length = 0 Then

            UserName = "Administrator"
            UserPassWord = "66998833"
            NameMahalFaal = "تهران"
            CodeMahalFaal = "1"
            PersonelCode = "0"
            PersonelName = "Administrator"
            CodeDoreh = "1391"
            txtCaption = "پیش فاکتور"


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
            ObjCode.UserName = UserName
        End If
    End Sub
    Private Sub LoadCombo()
        Try
            Dim cn As New SqlConnection
            Dim cm As New SqlCommand
            Dim da As New SqlDataAdapter
            Dim dr As DataRow
            Dim strSQL As String = ""

            cn = New SqlConnection(ConnectionString)
            cn.Open()

            strSQL = "Global.spMarkazPakhsh_LoadCombo "

            cm = New SqlCommand(strSQL, cn)
            cm.CommandType = CommandType.StoredProcedure
            cm.Parameters.Clear()

            cm.Parameters.AddWithValue("CodeMahal", 0)

            da = New SqlDataAdapter(cm)
            da.Fill(dsForm, "tbl_MarkazPakhsh")

            dr = dsForm.Tables("tbl_MarkazPakhsh").NewRow
            dr("CodeMahal") = 0
            dr("NameMahal") = "همــه"
            dsForm.Tables("tbl_MarkazPakhsh").Rows.Add(dr)

            cmbMarkazPakhsh.DataSource = Nothing
            cmbMarkazPakhsh.Items.Clear()
            cmbMarkazPakhsh.DataSource = dsForm.Tables("tbl_MarkazPakhsh").DefaultView
            cmbMarkazPakhsh.DisplayMember = "NameMahal"
            cmbMarkazPakhsh.ValueMember = "CodeMahal"

            cm = Nothing : da = Nothing
            cn.Close()

            ' -------------------------------------------------

            cmbNoeGozaresh.Items.Add("فـــروش کـــالا")
            cmbNoeGozaresh.Items.Add("برگـه تفکیک به تفکیک راننــدگان")
            cmbNoeGozaresh.Items.Add("برگشت از فـــروش")
            cmbNoeGozaresh.Items.Add("فاکتـــورهای رسیـــدی")

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> LoadCombo")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> LoadCombo")
        End Try
    End Sub
    Private Sub LoadForm()
        If flag = True Then
            If Not IsValidForm() Then
                Exit Sub
            End If

            LoadGozareshForoshKala()
            LoadGozareshTafkik_Ranandeh()
            LoadGozareshMarjoee()
            LoadGozareshFaktorResidi()
        End If
    End Sub
    Private Function IsValidForm() As Boolean
        IsValidForm = False

        Try
            If Len(mskTarikh.Text.ToString) <> 0 Then
                If Not objTarikh.IsShDate(mskTarikh.Text.ToString) Then
                    mskTarikh.Focus()
                    Exit Function
                End If
                If mskTarikh.Text.Substring(0, 4) <> CodeDoreh Then
                    MsgBox("تاريخ با دوره مالی فعال يکی نيست.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "")
                    mskTarikh.Focus()
                    Exit Function
                End If
                If ObjCode.CheckCompany <> 11 Then
                    If mskTarikh.Text > TarikhEmrooz Then
                        MsgBox("تاریخ وارد شده از تاریخ امروز جلوتر است.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "")
                        mskTarikh.Focus()
                        Exit Function
                    End If
                End If
            Else
                ErrPro.SetError(Me.mskTarikh, "تاريخ را وارد کنيد.")
                MsgBox("تاريخ را وارد کنيد.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "")
                mskTarikh.Focus()
                Exit Function
            End If
            ErrPro.SetError(Me.mskTarikh, "")

            If cmbMarkazPakhsh.SelectedIndex = -1 Then
                ErrPro.SetError(Me.cmbMarkazPakhsh, "لطفاً جهت نمایش گزارش ، مرکز پخش را مشخص نمایید .")
                MsgBox("لطفاً جهت نمایش گزارش ، مرکز پخش را مشخص نمایید .", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "")
                cmbMarkazPakhsh.Focus()
                Exit Function
            End If
            ErrPro.SetError(Me.cmbMarkazPakhsh, "")

            Return True

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->IsValidBeforSaveTitr")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->IsValidBeforSaveTitr")
        End Try
    End Function

    Private Sub LoadGozareshForoshKala()
        Try
            GridExForoshKala.DataSource = Nothing

            Dim StrSql As String

            StrSql = "Report.spGozareshRoozanehPakhsh_ForoshKala "

            RefreshFormData(StrSql, 1)

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->Search")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->Search")
        End Try
    End Sub
    Private Sub LoadGozareshTafkik_Ranandeh()
        Try
            GridExTafkik_Ranandeh.DataSource = Nothing

            Dim StrSql As String

            StrSql = "Report.spGozareshRoozanehPakhsh_Tafkik_Ranandeh "

            RefreshFormData(StrSql, 2)

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->Search")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->Search")
        End Try
    End Sub
    Private Sub LoadGozareshMarjoee()
        Try
            GridExMarjoee.DataSource = Nothing

            Dim StrSql As String

            StrSql = "Report.spGozareshRoozanehPakhsh_BargashtAzForosh "

            RefreshFormData(StrSql, 3)

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->Search")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->Search")
        End Try
    End Sub
    Private Sub LoadGozareshFaktorResidi()
        Try
            GridExFaktorResidi.DataSource = Nothing

            Dim StrSql As String

            StrSql = "Report.spGozareshRoozanehPakhsh_FaktorResidi "

            RefreshFormData(StrSql, 4)

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->Search")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->Search")
        End Try
    End Sub

    Private Sub RefreshFormData(ByVal strSql As String, ByVal Noe As Integer)
        Dim daSQL As SqlDataAdapter
        Dim cn As New SqlConnection
        Dim cm As New SqlCommand
        Try

            cn.ConnectionString = ConnectionString
            cn.Open()

            cm = New SqlCommand(strSql, cn)
            cm.CommandType = CommandType.StoredProcedure
            cm.Parameters.Clear()

            cm.Parameters.AddWithValue("CodeMahal", cmbMarkazPakhsh.SelectedValue)
            cm.Parameters.AddWithValue("CodeDoreh", CodeDoreh)
            cm.Parameters.AddWithValue("Tarikh", mskTarikh.Text)

            If Noe = 1 Then

                If dsForm.Tables.Contains("tbl_ForoshKala") Then
                    dsForm.Tables.Remove("tbl_ForoshKala")
                End If

                daSQL = New SqlDataAdapter(cm)
                daSQL.Fill(dsForm, "tbl_ForoshKala")

                dvForm = New DataView
                dvForm = dsForm.Tables("tbl_ForoshKala").DefaultView
                dvForm.Sort = "CodeKala ASC"
                dvForm.AllowDelete = True
                dvForm.AllowEdit = False
                dvForm.AllowNew = False
                daSQL = Nothing

                dvTitr_ForoshKala = New DataView(dsForm.Tables("tbl_ForoshKala"), "", "CodeKala ASC", DataViewRowState.CurrentRows)
                dvTitr_ForoshKala.AllowNew = False
                dvTitr_ForoshKala.AllowDelete = False
                dvTitr_ForoshKala.AllowEdit = False

                GridExForoshKala.DataSource = Nothing
                GridExForoshKala.DataSource = dvTitr_ForoshKala

                If dvTitr_ForoshKala.Count <> 0 Then
                    SetGridStyle_ForoshKala()
                End If

            ElseIf Noe = 2 Then

                If dsForm.Tables.Contains("tbl_Tafkik_Ranandeh") Then
                    dsForm.Tables.Remove("tbl_Tafkik_Ranandeh")
                End If

                daSQL = New SqlDataAdapter(cm)
                daSQL.Fill(dsForm, "tbl_Tafkik_Ranandeh")

                dvForm = New DataView
                dvForm = dsForm.Tables("tbl_Tafkik_Ranandeh").DefaultView
                dvForm.Sort = "ShomarehTafkik ASC"
                dvForm.AllowDelete = True
                dvForm.AllowEdit = False
                dvForm.AllowNew = False
                daSQL = Nothing

                dvTitr_Tafkik_Ranandeh = New DataView(dsForm.Tables("tbl_Tafkik_Ranandeh"), "", "ShomarehTafkik ASC", DataViewRowState.CurrentRows)
                dvTitr_Tafkik_Ranandeh.AllowNew = False
                dvTitr_Tafkik_Ranandeh.AllowDelete = False
                dvTitr_Tafkik_Ranandeh.AllowEdit = False

                GridExTafkik_Ranandeh.DataSource = Nothing
                GridExTafkik_Ranandeh.DataSource = dvTitr_Tafkik_Ranandeh

                If dvTitr_Tafkik_Ranandeh.Count <> 0 Then
                    SetGridStyle_Tafkik_Ranandeh()
                End If

            ElseIf Noe = 3 Then

                If dsForm.Tables.Contains("tbl_Marjoee") Then
                    dsForm.Tables.Remove("tbl_Marjoee")
                End If

                daSQL = New SqlDataAdapter(cm)
                daSQL.Fill(dsForm, "tbl_Marjoee")

                dvForm = New DataView
                dvForm = dsForm.Tables("tbl_Marjoee").DefaultView
                dvForm.Sort = "NameForoshandeh ASC"
                dvForm.AllowDelete = True
                dvForm.AllowEdit = False
                dvForm.AllowNew = False
                daSQL = Nothing

                dvTitr_Marjoee = New DataView(dsForm.Tables("tbl_Marjoee"), "", "NameForoshandeh ASC", DataViewRowState.CurrentRows)
                dvTitr_Marjoee.AllowNew = False
                dvTitr_Marjoee.AllowDelete = False
                dvTitr_Marjoee.AllowEdit = False

                GridExMarjoee.DataSource = Nothing
                GridExMarjoee.DataSource = dvTitr_Marjoee

                If dvTitr_Marjoee.Count <> 0 Then
                    SetGridStyle_Marjoee()
                End If

            ElseIf Noe = 4 Then

                If dsForm.Tables.Contains("tbl_FaktorResidi") Then
                    dsForm.Tables.Remove("tbl_FaktorResidi")
                End If

                daSQL = New SqlDataAdapter(cm)
                daSQL.Fill(dsForm, "tbl_FaktorResidi")

                dvForm = New DataView
                dvForm = dsForm.Tables("tbl_FaktorResidi").DefaultView
                dvForm.Sort = "FaktorShomareh ASC"
                dvForm.AllowDelete = True
                dvForm.AllowEdit = False
                dvForm.AllowNew = False
                daSQL = Nothing

                dvTitr_FaktorResidi = New DataView(dsForm.Tables("tbl_FaktorResidi"), "", "FaktorShomareh ASC", DataViewRowState.CurrentRows)
                dvTitr_FaktorResidi.AllowNew = False
                dvTitr_FaktorResidi.AllowDelete = False
                dvTitr_FaktorResidi.AllowEdit = False

                GridExFaktorResidi.DataSource = Nothing
                GridExFaktorResidi.DataSource = dvTitr_FaktorResidi

                If dvTitr_FaktorResidi.Count <> 0 Then
                    SetGridStyle_FaktorResidi()
                End If

            End If

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->RefreshTitrdata")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->RefreshTitrdata")
        Finally
            cm = Nothing : daSQL = Nothing
            cn.Close()
        End Try
    End Sub
    Private Sub SetGridStyle_ForoshKala()
        Try
            With GridExForoshKala
                .DataSource = Nothing
                .DataSource = dsForm.Tables("tbl_ForoshKala").DefaultView
                .SetDataBinding(dsForm.Tables("tbl_ForoshKala").DefaultView, "")
                .RetrieveStructure()
            End With

            For i As Integer = 0 To GridExForoshKala.CurrentTable.Columns.Count - 1
                GridExForoshKala.CurrentTable.Columns.Item(i).Visible = False
            Next

            GridExForoshKala.CurrentTable.Columns.Item("rNum").Caption = "ردیف"
            GridExForoshKala.CurrentTable.Columns.Item("rNum").Visible = True
            GridExForoshKala.CurrentTable.Columns.Item("rNum").Width = 50
            GridExForoshKala.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridExForoshKala.CurrentTable.Columns.Item("rNum").Position = 0
            GridExForoshKala.CurrentTable.Columns.Item("rNum").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridExForoshKala.CurrentTable.Columns.Item("CodeKala").Caption = "کد کالا"
            GridExForoshKala.CurrentTable.Columns.Item("CodeKala").Visible = True
            GridExForoshKala.CurrentTable.Columns.Item("CodeKala").Width = 100
            GridExForoshKala.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridExForoshKala.CurrentTable.Columns.Item("CodeKala").Position = 1
            GridExForoshKala.CurrentTable.Columns.Item("CodeKala").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridExForoshKala.CurrentTable.Columns.Item("NameKala").Caption = "نــام کــــالا"
            GridExForoshKala.CurrentTable.Columns.Item("NameKala").Visible = True
            GridExForoshKala.CurrentTable.Columns.Item("NameKala").Width = 318
            GridExForoshKala.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridExForoshKala.CurrentTable.Columns.Item("NameKala").Position = 2
            GridExForoshKala.CurrentTable.Columns.Item("NameKala").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridExForoshKala.CurrentTable.Columns.Item("TedadForosh").Caption = "تعـــداد فــــروش"
            GridExForoshKala.CurrentTable.Columns.Item("TedadForosh").Visible = True
            GridExForoshKala.CurrentTable.Columns.Item("TedadForosh").Width = 120
            GridExForoshKala.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridExForoshKala.CurrentTable.Columns.Item("TedadForosh").Position = 3
            GridExForoshKala.CurrentTable.Columns.Item("TedadForosh").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridExForoshKala.CurrentTable.Columns.Item("Fee").Caption = "فــــی فــــروش"
            GridExForoshKala.CurrentTable.Columns.Item("Fee").Visible = True
            GridExForoshKala.CurrentTable.Columns.Item("Fee").Width = 150
            GridExForoshKala.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridExForoshKala.CurrentTable.Columns.Item("Fee").FormatString = "G"
            GridExForoshKala.CurrentTable.Columns.Item("Fee").Position = 4
            GridExForoshKala.CurrentTable.Columns.Item("Fee").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridExForoshKala.CurrentTable.Columns.Item("MablaghForosh").Caption = "مبلـــغ فــــروش"
            GridExForoshKala.CurrentTable.Columns.Item("MablaghForosh").Visible = True
            GridExForoshKala.CurrentTable.Columns.Item("MablaghForosh").Width = 150
            GridExForoshKala.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridExForoshKala.CurrentTable.Columns.Item("MablaghForosh").FormatString = "G"
            GridExForoshKala.CurrentTable.Columns.Item("MablaghForosh").Position = 5
            GridExForoshKala.CurrentTable.Columns.Item("MablaghForosh").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridExForoshKala.CurrentTable.Columns.Item("MablaghMarjoee").Caption = "مبلـــغ برگشت از فــــروش"
            GridExForoshKala.CurrentTable.Columns.Item("MablaghMarjoee").Visible = True
            GridExForoshKala.CurrentTable.Columns.Item("MablaghMarjoee").Width = 160
            GridExForoshKala.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridExForoshKala.CurrentTable.Columns.Item("MablaghMarjoee").Position = 6
            GridExForoshKala.CurrentTable.Columns.Item("MablaghMarjoee").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridExForoshKala.CurrentTable.Columns.Item("ForoshKhales").Caption = "فــــروش خالــص"
            GridExForoshKala.CurrentTable.Columns.Item("ForoshKhales").Visible = True
            GridExForoshKala.CurrentTable.Columns.Item("ForoshKhales").Width = 150
            GridExForoshKala.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridExForoshKala.CurrentTable.Columns.Item("ForoshKhales").FormatString = "G"
            GridExForoshKala.CurrentTable.Columns.Item("ForoshKhales").Position = 7
            GridExForoshKala.CurrentTable.Columns.Item("ForoshKhales").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            For i As Integer = 0 To GridExForoshKala.RootTable.Columns.Count - 1
                If GridExForoshKala.RootTable.Columns(i).Type.IsValueType Then
                    GridExForoshKala.RootTable.Columns(i).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                    GridExForoshKala.RootTable.Columns(i).FormatMode = Janus.Windows.GridEX.FormatMode.UseIFormattable
                    GridExForoshKala.RootTable.Columns(i).FormatString = "###,###.##"
                    GridExForoshKala.RootTable.Columns(i).TotalFormatMode = Janus.Windows.GridEX.FormatMode.UseIFormattable
                    GridExForoshKala.RootTable.Columns(i).TotalFormatString = "###,###.##"
                End If
            Next

            GridExForoshKala.Visible = True
            GridExForoshKala.HeaderFormatStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center


        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> SetGridExForoshKala")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> SetGridExForoshKala")
        End Try

    End Sub
    Private Sub SetGridStyle_Tafkik_Ranandeh()
        Try
            With GridExTafkik_Ranandeh
                .DataSource = Nothing
                .DataSource = dsForm.Tables("tbl_Tafkik_Ranandeh").DefaultView
                .SetDataBinding(dsForm.Tables("tbl_Tafkik_Ranandeh").DefaultView, "")
                .RetrieveStructure()
            End With

            For i As Integer = 0 To GridExTafkik_Ranandeh.CurrentTable.Columns.Count - 1
                GridExTafkik_Ranandeh.CurrentTable.Columns.Item(i).Visible = False
            Next

            GridExTafkik_Ranandeh.CurrentTable.Columns.Item("rNum").Caption = "ردیف"
            GridExTafkik_Ranandeh.CurrentTable.Columns.Item("rNum").Visible = True
            GridExTafkik_Ranandeh.CurrentTable.Columns.Item("rNum").Width = 40
            GridExTafkik_Ranandeh.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridExTafkik_Ranandeh.CurrentTable.Columns.Item("rNum").Position = 0
            GridExTafkik_Ranandeh.CurrentTable.Columns.Item("rNum").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridExTafkik_Ranandeh.CurrentTable.Columns.Item("ShomarehTafkik").Caption = "ش تفکیک"
            GridExTafkik_Ranandeh.CurrentTable.Columns.Item("ShomarehTafkik").Visible = True
            GridExTafkik_Ranandeh.CurrentTable.Columns.Item("ShomarehTafkik").Width = 70
            GridExTafkik_Ranandeh.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridExTafkik_Ranandeh.CurrentTable.Columns.Item("ShomarehTafkik").Position = 1
            GridExTafkik_Ranandeh.CurrentTable.Columns.Item("ShomarehTafkik").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridExTafkik_Ranandeh.CurrentTable.Columns.Item("NameRanandehTozie").Caption = "نـــام راننــــده"
            GridExTafkik_Ranandeh.CurrentTable.Columns.Item("NameRanandehTozie").Visible = True
            GridExTafkik_Ranandeh.CurrentTable.Columns.Item("NameRanandehTozie").Width = 210
            GridExTafkik_Ranandeh.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridExTafkik_Ranandeh.CurrentTable.Columns.Item("NameRanandehTozie").Position = 2
            GridExTafkik_Ranandeh.CurrentTable.Columns.Item("NameRanandehTozie").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridExTafkik_Ranandeh.CurrentTable.Columns.Item("TedadFaktor").Caption = "تعداد فاکتور"
            GridExTafkik_Ranandeh.CurrentTable.Columns.Item("TedadFaktor").Visible = True
            GridExTafkik_Ranandeh.CurrentTable.Columns.Item("TedadFaktor").Width = 80
            GridExTafkik_Ranandeh.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridExTafkik_Ranandeh.CurrentTable.Columns.Item("TedadFaktor").Position = 3
            GridExTafkik_Ranandeh.CurrentTable.Columns.Item("TedadFaktor").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridExTafkik_Ranandeh.CurrentTable.Columns.Item("JamRialiTafkik").Caption = "جمع ریالی برگه تفکیک"
            GridExTafkik_Ranandeh.CurrentTable.Columns.Item("JamRialiTafkik").Visible = True
            GridExTafkik_Ranandeh.CurrentTable.Columns.Item("JamRialiTafkik").Width = 150
            GridExTafkik_Ranandeh.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridExTafkik_Ranandeh.CurrentTable.Columns.Item("JamRialiTafkik").FormatString = "G"
            GridExTafkik_Ranandeh.CurrentTable.Columns.Item("JamRialiTafkik").Position = 4
            GridExTafkik_Ranandeh.CurrentTable.Columns.Item("JamRialiTafkik").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridExTafkik_Ranandeh.CurrentTable.Columns.Item("TedadDaryafti").Caption = "تعداد دریافتی"
            GridExTafkik_Ranandeh.CurrentTable.Columns.Item("TedadDaryafti").Visible = True
            GridExTafkik_Ranandeh.CurrentTable.Columns.Item("TedadDaryafti").Width = 90
            GridExTafkik_Ranandeh.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridExTafkik_Ranandeh.CurrentTable.Columns.Item("TedadDaryafti").Position = 5
            GridExTafkik_Ranandeh.CurrentTable.Columns.Item("TedadDaryafti").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridExTafkik_Ranandeh.CurrentTable.Columns.Item("TedadResidi").Caption = "تعداد نیمه تسویه"
            GridExTafkik_Ranandeh.CurrentTable.Columns.Item("TedadResidi").Visible = True
            GridExTafkik_Ranandeh.CurrentTable.Columns.Item("TedadResidi").Width = 110
            GridExTafkik_Ranandeh.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridExTafkik_Ranandeh.CurrentTable.Columns.Item("TedadResidi").Position = 6
            GridExTafkik_Ranandeh.CurrentTable.Columns.Item("TedadResidi").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridExTafkik_Ranandeh.CurrentTable.Columns.Item("MablaghDaryafti").Caption = "مبلغ دریافتی"
            GridExTafkik_Ranandeh.CurrentTable.Columns.Item("MablaghDaryafti").Visible = True
            GridExTafkik_Ranandeh.CurrentTable.Columns.Item("MablaghDaryafti").Width = 90
            GridExTafkik_Ranandeh.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridExTafkik_Ranandeh.CurrentTable.Columns.Item("MablaghDaryafti").FormatString = "G"
            GridExTafkik_Ranandeh.CurrentTable.Columns.Item("MablaghDaryafti").Position = 7
            GridExTafkik_Ranandeh.CurrentTable.Columns.Item("MablaghDaryafti").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            'GridExTafkik_Ranandeh.CurrentTable.Columns.Item("MablaghResidi").Caption = "مبلغ رسیدی"
            'GridExTafkik_Ranandeh.CurrentTable.Columns.Item("MablaghResidi").Visible = True
            'GridExTafkik_Ranandeh.CurrentTable.Columns.Item("MablaghResidi").Width = 90
            'GridExTafkik_Ranandeh.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            'GridExTafkik_Ranandeh.CurrentTable.Columns.Item("MablaghResidi").FormatString = "G"
            'GridExTafkik_Ranandeh.CurrentTable.Columns.Item("MablaghResidi").Position = 8
            'GridExTafkik_Ranandeh.CurrentTable.Columns.Item("MablaghResidi").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridExTafkik_Ranandeh.CurrentTable.Columns.Item("TedadBargashti").Caption = "تعداد برگشتی"
            GridExTafkik_Ranandeh.CurrentTable.Columns.Item("TedadBargashti").Visible = True
            GridExTafkik_Ranandeh.CurrentTable.Columns.Item("TedadBargashti").Width = 90
            GridExTafkik_Ranandeh.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridExTafkik_Ranandeh.CurrentTable.Columns.Item("TedadBargashti").Position = 8
            GridExTafkik_Ranandeh.CurrentTable.Columns.Item("TedadBargashti").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridExTafkik_Ranandeh.CurrentTable.Columns.Item("MablaghBargashti").Caption = "مبلغ برگشتی"
            GridExTafkik_Ranandeh.CurrentTable.Columns.Item("MablaghBargashti").Visible = True
            GridExTafkik_Ranandeh.CurrentTable.Columns.Item("MablaghBargashti").Width = 100
            GridExTafkik_Ranandeh.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridExTafkik_Ranandeh.CurrentTable.Columns.Item("MablaghBargashti").FormatString = "G"
            GridExTafkik_Ranandeh.CurrentTable.Columns.Item("MablaghBargashti").Position = 9
            GridExTafkik_Ranandeh.CurrentTable.Columns.Item("MablaghBargashti").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridExTafkik_Ranandeh.CurrentTable.Columns.Item("MablaghTabdilBeTakhfif").Caption = "تبدیل به تخفیف"
            GridExTafkik_Ranandeh.CurrentTable.Columns.Item("MablaghTabdilBeTakhfif").Visible = True
            GridExTafkik_Ranandeh.CurrentTable.Columns.Item("MablaghTabdilBeTakhfif").Width = 120
            GridExTafkik_Ranandeh.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridExTafkik_Ranandeh.CurrentTable.Columns.Item("MablaghTabdilBeTakhfif").FormatString = "G"
            GridExTafkik_Ranandeh.CurrentTable.Columns.Item("MablaghTabdilBeTakhfif").Position = 10
            GridExTafkik_Ranandeh.CurrentTable.Columns.Item("MablaghTabdilBeTakhfif").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridExTafkik_Ranandeh.CurrentTable.Columns.Item("Mandeh").Caption = "مانـــده"
            GridExTafkik_Ranandeh.CurrentTable.Columns.Item("Mandeh").Visible = True
            GridExTafkik_Ranandeh.CurrentTable.Columns.Item("Mandeh").Width = 100
            GridExTafkik_Ranandeh.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridExTafkik_Ranandeh.CurrentTable.Columns.Item("Mandeh").FormatString = "G"
            GridExTafkik_Ranandeh.CurrentTable.Columns.Item("Mandeh").Position = 11
            GridExTafkik_Ranandeh.CurrentTable.Columns.Item("Mandeh").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridExTafkik_Ranandeh.CurrentTable.Columns.Item("DarsadDaryafti").Caption = "درصد دریافتی"
            GridExTafkik_Ranandeh.CurrentTable.Columns.Item("DarsadDaryafti").Visible = True
            GridExTafkik_Ranandeh.CurrentTable.Columns.Item("DarsadDaryafti").Width = 90
            GridExTafkik_Ranandeh.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridExTafkik_Ranandeh.CurrentTable.Columns.Item("DarsadDaryafti").Position = 12
            GridExTafkik_Ranandeh.CurrentTable.Columns.Item("DarsadDaryafti").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridExTafkik_Ranandeh.CurrentTable.Columns.Item("DarsadBargashti").Caption = "درصد برگشتی"
            GridExTafkik_Ranandeh.CurrentTable.Columns.Item("DarsadBargashti").Visible = True
            GridExTafkik_Ranandeh.CurrentTable.Columns.Item("DarsadBargashti").Width = 100
            GridExTafkik_Ranandeh.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridExTafkik_Ranandeh.CurrentTable.Columns.Item("DarsadBargashti").Position = 13
            GridExTafkik_Ranandeh.CurrentTable.Columns.Item("DarsadBargashti").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridExTafkik_Ranandeh.CurrentTable.Columns.Item("DarsadTabdilBeTakhfif").Caption = "درصد تبدیل به ت"
            GridExTafkik_Ranandeh.CurrentTable.Columns.Item("DarsadTabdilBeTakhfif").Visible = True
            GridExTafkik_Ranandeh.CurrentTable.Columns.Item("DarsadTabdilBeTakhfif").Width = 100
            GridExTafkik_Ranandeh.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridExTafkik_Ranandeh.CurrentTable.Columns.Item("DarsadTabdilBeTakhfif").Position = 14
            GridExTafkik_Ranandeh.CurrentTable.Columns.Item("DarsadTabdilBeTakhfif").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridExTafkik_Ranandeh.CurrentTable.Columns.Item("DarsadMandeh").Caption = "درصد مانده"
            GridExTafkik_Ranandeh.CurrentTable.Columns.Item("DarsadMandeh").Visible = True
            GridExTafkik_Ranandeh.CurrentTable.Columns.Item("DarsadMandeh").Width = 85
            GridExTafkik_Ranandeh.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridExTafkik_Ranandeh.CurrentTable.Columns.Item("DarsadMandeh").Position = 15
            GridExTafkik_Ranandeh.CurrentTable.Columns.Item("DarsadMandeh").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            'GridExTafkik_Ranandeh.CurrentTable.Columns.Item("DarsadResidi").Caption = "درصد رسیدی"
            'GridExTafkik_Ranandeh.CurrentTable.Columns.Item("DarsadResidi").Visible = True
            'GridExTafkik_Ranandeh.CurrentTable.Columns.Item("DarsadResidi").Width = 85
            'GridExTafkik_Ranandeh.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            'GridExTafkik_Ranandeh.CurrentTable.Columns.Item("DarsadResidi").Position = 12
            'GridExTafkik_Ranandeh.CurrentTable.Columns.Item("DarsadResidi").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            For i As Integer = 0 To GridExTafkik_Ranandeh.RootTable.Columns.Count - 1
                If GridExTafkik_Ranandeh.RootTable.Columns(i).Type.IsValueType Then
                    GridExTafkik_Ranandeh.RootTable.Columns(i).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                    GridExTafkik_Ranandeh.RootTable.Columns(i).FormatMode = Janus.Windows.GridEX.FormatMode.UseIFormattable
                    GridExTafkik_Ranandeh.RootTable.Columns(i).FormatString = "###,###.##"
                    GridExTafkik_Ranandeh.RootTable.Columns(i).TotalFormatMode = Janus.Windows.GridEX.FormatMode.UseIFormattable
                    GridExTafkik_Ranandeh.RootTable.Columns(i).TotalFormatString = "###,###.##"
                End If
            Next

            GridExTafkik_Ranandeh.Visible = True
            GridExTafkik_Ranandeh.HeaderFormatStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> SetGridExTafkik_Ranandeh")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> SetGridExTafkik_Ranandeh")
        End Try

    End Sub
    Private Sub SetGridStyle_Marjoee()
        Try
            With GridExMarjoee
                .DataSource = Nothing
                .DataSource = dsForm.Tables("tbl_Marjoee").DefaultView
                .SetDataBinding(dsForm.Tables("tbl_Marjoee").DefaultView, "")
                .RetrieveStructure()
            End With

            For i As Integer = 0 To GridExMarjoee.CurrentTable.Columns.Count - 1
                GridExMarjoee.CurrentTable.Columns.Item(i).Visible = False
            Next

            GridExMarjoee.CurrentTable.Columns.Item("rNum").Caption = "ردیف"
            GridExMarjoee.CurrentTable.Columns.Item("rNum").Visible = True
            GridExMarjoee.CurrentTable.Columns.Item("rNum").Width = 50
            GridExMarjoee.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridExMarjoee.CurrentTable.Columns.Item("rNum").Position = 0
            GridExMarjoee.CurrentTable.Columns.Item("rNum").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridExMarjoee.CurrentTable.Columns.Item("NameForoshandeh").Caption = "نـــام فروشنـــده"
            GridExMarjoee.CurrentTable.Columns.Item("NameForoshandeh").Visible = True
            GridExMarjoee.CurrentTable.Columns.Item("NameForoshandeh").Width = 230
            GridExMarjoee.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridExMarjoee.CurrentTable.Columns.Item("NameForoshandeh").Position = 1
            GridExMarjoee.CurrentTable.Columns.Item("NameForoshandeh").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridExMarjoee.CurrentTable.Columns.Item("NameMamorPakhsh").Caption = "نـــام مامور پخــش"
            GridExMarjoee.CurrentTable.Columns.Item("NameMamorPakhsh").Visible = True
            GridExMarjoee.CurrentTable.Columns.Item("NameMamorPakhsh").Width = 230
            GridExMarjoee.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridExMarjoee.CurrentTable.Columns.Item("NameMamorPakhsh").Position = 2
            GridExMarjoee.CurrentTable.Columns.Item("NameMamorPakhsh").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridExMarjoee.CurrentTable.Columns.Item("CodeMoshtary").Caption = "کـــد مشتــری"
            GridExMarjoee.CurrentTable.Columns.Item("CodeMoshtary").Visible = True
            GridExMarjoee.CurrentTable.Columns.Item("CodeMoshtary").Width = 110
            GridExMarjoee.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridExMarjoee.CurrentTable.Columns.Item("CodeMoshtary").Position = 3
            GridExMarjoee.CurrentTable.Columns.Item("CodeMoshtary").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridExMarjoee.CurrentTable.Columns.Item("NameMoshtary").Caption = "نـــام مشتــری"
            GridExMarjoee.CurrentTable.Columns.Item("NameMoshtary").Visible = True
            GridExMarjoee.CurrentTable.Columns.Item("NameMoshtary").Width = 230
            GridExMarjoee.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridExMarjoee.CurrentTable.Columns.Item("NameMoshtary").Position = 4
            GridExMarjoee.CurrentTable.Columns.Item("NameMoshtary").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridExMarjoee.CurrentTable.Columns.Item("ShomarehFaktor").Caption = "شمـاره فاکتــور"
            GridExMarjoee.CurrentTable.Columns.Item("ShomarehFaktor").Visible = True
            GridExMarjoee.CurrentTable.Columns.Item("ShomarehFaktor").NullText = "نــدارد"
            GridExMarjoee.CurrentTable.Columns.Item("ShomarehFaktor").Width = 110
            GridExMarjoee.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridExMarjoee.CurrentTable.Columns.Item("ShomarehFaktor").Position = 5
            GridExMarjoee.CurrentTable.Columns.Item("ShomarehFaktor").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridExMarjoee.CurrentTable.Columns.Item("NameKala").Caption = "نـــام کــالا"
            GridExMarjoee.CurrentTable.Columns.Item("NameKala").Visible = True
            GridExMarjoee.CurrentTable.Columns.Item("NameKala").Width = 230
            GridExMarjoee.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridExMarjoee.CurrentTable.Columns.Item("NameKala").Position = 6
            GridExMarjoee.CurrentTable.Columns.Item("NameKala").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridExMarjoee.CurrentTable.Columns.Item("TedadKala").Caption = "تعـداد کــالا"
            GridExMarjoee.CurrentTable.Columns.Item("TedadKala").Visible = True
            GridExMarjoee.CurrentTable.Columns.Item("TedadKala").Width = 100
            GridExMarjoee.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridExMarjoee.CurrentTable.Columns.Item("TedadKala").Position = 7
            GridExMarjoee.CurrentTable.Columns.Item("TedadKala").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridExMarjoee.CurrentTable.Columns.Item("MablaghKala").Caption = "مبلــغ کــالا"
            GridExMarjoee.CurrentTable.Columns.Item("MablaghKala").Visible = True
            GridExMarjoee.CurrentTable.Columns.Item("MablaghKala").Width = 100
            GridExMarjoee.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridExMarjoee.CurrentTable.Columns.Item("MablaghKala").FormatString = "G"
            GridExMarjoee.CurrentTable.Columns.Item("MablaghKala").Position = 8
            GridExMarjoee.CurrentTable.Columns.Item("MablaghKala").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridExMarjoee.CurrentTable.Columns.Item("txtElatMarjoee").Caption = "علـت مرجــوعی"
            GridExMarjoee.CurrentTable.Columns.Item("txtElatMarjoee").Visible = True
            GridExMarjoee.CurrentTable.Columns.Item("txtElatMarjoee").Width = 230
            GridExMarjoee.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridExMarjoee.CurrentTable.Columns.Item("txtElatMarjoee").Position = 9
            GridExMarjoee.CurrentTable.Columns.Item("txtElatMarjoee").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            For i As Integer = 0 To GridExMarjoee.RootTable.Columns.Count - 1
                If GridExMarjoee.RootTable.Columns(i).Type.IsValueType Then
                    GridExMarjoee.RootTable.Columns(i).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                    GridExMarjoee.RootTable.Columns(i).FormatMode = Janus.Windows.GridEX.FormatMode.UseIFormattable
                    GridExMarjoee.RootTable.Columns(i).FormatString = "###,###.##"
                    GridExMarjoee.RootTable.Columns(i).TotalFormatMode = Janus.Windows.GridEX.FormatMode.UseIFormattable
                    GridExMarjoee.RootTable.Columns(i).TotalFormatString = "###,###.##"
                End If
            Next

            GridExMarjoee.Visible = True
            GridExMarjoee.HeaderFormatStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center


        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> SetGridExMarjoee")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> SetGridExMarjoee")
        End Try

    End Sub
    Private Sub SetGridStyle_FaktorResidi()
        Try
            With GridExFaktorResidi
                .DataSource = Nothing
                .DataSource = dsForm.Tables("tbl_FaktorResidi").DefaultView
                .SetDataBinding(dsForm.Tables("tbl_FaktorResidi").DefaultView, "")
                .RetrieveStructure()
            End With

            For i As Integer = 0 To GridExFaktorResidi.CurrentTable.Columns.Count - 1
                GridExFaktorResidi.CurrentTable.Columns.Item(i).Visible = False
            Next

            GridExFaktorResidi.CurrentTable.Columns.Item("rNum").Caption = "ردیف"
            GridExFaktorResidi.CurrentTable.Columns.Item("rNum").Visible = True
            GridExFaktorResidi.CurrentTable.Columns.Item("rNum").Width = 40
            GridExFaktorResidi.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridExFaktorResidi.CurrentTable.Columns.Item("rNum").Position = 0
            GridExFaktorResidi.CurrentTable.Columns.Item("rNum").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridExFaktorResidi.CurrentTable.Columns.Item("FaktorShomareh").Caption = "شمــاره فاکتــور"
            GridExFaktorResidi.CurrentTable.Columns.Item("FaktorShomareh").Visible = True
            GridExFaktorResidi.CurrentTable.Columns.Item("FaktorShomareh").Width = 120
            GridExFaktorResidi.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridExFaktorResidi.CurrentTable.Columns.Item("FaktorShomareh").Position = 1
            GridExFaktorResidi.CurrentTable.Columns.Item("FaktorShomareh").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridExFaktorResidi.CurrentTable.Columns.Item("CodeMoshtary").Caption = "کــد مشتـــری"
            GridExFaktorResidi.CurrentTable.Columns.Item("CodeMoshtary").Visible = True
            GridExFaktorResidi.CurrentTable.Columns.Item("CodeMoshtary").Width = 120
            GridExFaktorResidi.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridExFaktorResidi.CurrentTable.Columns.Item("CodeMoshtary").Position = 2
            GridExFaktorResidi.CurrentTable.Columns.Item("CodeMoshtary").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridExFaktorResidi.CurrentTable.Columns.Item("NameMoshtary").Caption = "نـام مشتـــری"
            GridExFaktorResidi.CurrentTable.Columns.Item("NameMoshtary").Visible = True
            GridExFaktorResidi.CurrentTable.Columns.Item("NameMoshtary").Width = 240
            GridExFaktorResidi.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridExFaktorResidi.CurrentTable.Columns.Item("NameMoshtary").Position = 3
            GridExFaktorResidi.CurrentTable.Columns.Item("NameMoshtary").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridExFaktorResidi.CurrentTable.Columns.Item("MablaghFaktor").Caption = "مبلغ فاکتور"
            GridExFaktorResidi.CurrentTable.Columns.Item("MablaghFaktor").Visible = True
            GridExFaktorResidi.CurrentTable.Columns.Item("MablaghFaktor").Width = 120
            GridExFaktorResidi.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridExFaktorResidi.CurrentTable.Columns.Item("MablaghFaktor").FormatString = "G"
            GridExFaktorResidi.CurrentTable.Columns.Item("MablaghFaktor").Position = 4
            GridExFaktorResidi.CurrentTable.Columns.Item("MablaghFaktor").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridExFaktorResidi.CurrentTable.Columns.Item("MablaghDaryafti").Caption = "مبلغ دریافتــی"
            GridExFaktorResidi.CurrentTable.Columns.Item("MablaghDaryafti").Visible = True
            GridExFaktorResidi.CurrentTable.Columns.Item("MablaghDaryafti").Width = 120
            GridExFaktorResidi.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridExFaktorResidi.CurrentTable.Columns.Item("MablaghDaryafti").FormatString = "G"
            GridExFaktorResidi.CurrentTable.Columns.Item("MablaghDaryafti").Position = 5
            GridExFaktorResidi.CurrentTable.Columns.Item("MablaghDaryafti").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridExFaktorResidi.CurrentTable.Columns.Item("NameRanandeh").Caption = "نـام تحصیلدار - راننده"
            GridExFaktorResidi.CurrentTable.Columns.Item("NameRanandeh").Visible = True
            GridExFaktorResidi.CurrentTable.Columns.Item("NameRanandeh").Width = 220
            GridExFaktorResidi.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridExFaktorResidi.CurrentTable.Columns.Item("NameRanandeh").Position = 6
            GridExFaktorResidi.CurrentTable.Columns.Item("NameRanandeh").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            GridExFaktorResidi.CurrentTable.Columns.Item("NameForoshandeh").Caption = "نـام فروشنــــده"
            GridExFaktorResidi.CurrentTable.Columns.Item("NameForoshandeh").Visible = True
            GridExFaktorResidi.CurrentTable.Columns.Item("NameForoshandeh").Width = 220
            GridExFaktorResidi.CurrentTable.Columns.GridEX.EditMode = Janus.Windows.GridEX.EditMode.EditOff
            GridExFaktorResidi.CurrentTable.Columns.Item("NameForoshandeh").Position = 7
            GridExFaktorResidi.CurrentTable.Columns.Item("NameForoshandeh").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center

            For i As Integer = 0 To GridExFaktorResidi.RootTable.Columns.Count - 1
                If GridExFaktorResidi.RootTable.Columns(i).Type.IsValueType Then
                    GridExFaktorResidi.RootTable.Columns(i).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                    GridExFaktorResidi.RootTable.Columns(i).FormatMode = Janus.Windows.GridEX.FormatMode.UseIFormattable
                    GridExFaktorResidi.RootTable.Columns(i).FormatString = "###,###.##"
                    GridExFaktorResidi.RootTable.Columns(i).TotalFormatMode = Janus.Windows.GridEX.FormatMode.UseIFormattable
                    GridExFaktorResidi.RootTable.Columns(i).TotalFormatString = "###,###.##"
                End If
            Next

            GridExFaktorResidi.Visible = True
            GridExFaktorResidi.HeaderFormatStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center


        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> SetGridExFaktorResidi")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> SetGridExFaktorResidi")
        End Try

    End Sub

    Private Sub SetReport(ByVal Noe As Integer)
        Try
            Dim cnSQL As New SqlConnection
            Dim cmSQL As New SqlCommand
            Dim strSQL As String = ""
            Dim daSQL As SqlDataAdapter

            If Not IsValidForm() Then
                Exit Sub
            End If

            Windows.Forms.Cursor.Current = Cursors.WaitCursor

            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()

            If dsForm.Tables.Contains("tblGozaresh") Then
                dsForm.Tables.Remove("tblGozaresh")
            End If

            If Noe = 0 Then
                strSQL = "Report.spGozareshRoozanehPakhsh_ForoshKala "
            ElseIf Noe = 1 Then
                strSQL = "Report.spGozareshRoozanehPakhsh_Tafkik_Ranandeh "
            ElseIf Noe = 2 Then
                strSQL = "Report.spGozareshRoozanehPakhsh_BargashtAzForosh "
            ElseIf Noe = 3 Then
                strSQL = "Report.spGozareshRoozanehPakhsh_FaktorResidi "
            End If

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            cmSQL.Parameters.AddWithValue("CodeMahal", cmbMarkazPakhsh.SelectedValue)
            cmSQL.Parameters.AddWithValue("CodeDoreh", CodeDoreh)
            cmSQL.Parameters.AddWithValue("Tarikh", mskTarikh.Text)

            daSQL = New SqlDataAdapter(cmSQL)
            daSQL.Fill(dsForm, "tblGozaresh")

            If dsForm.Tables("tblGozaresh").Rows.Count = 0 Then
                MsgBox("هیـــــچ رکوردی برای گزارش پیدا نشد.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "پیام")
                Exit Sub
            End If

            Dim rpt As New ReportDocument
            Dim rpttables As Tables
            Dim rptformula As FormulaFieldDefinitions
            Dim frm As New Forms_dll.frmGL_Gozaresh

            If Noe = 0 Then
                rpt.Load(rptPath & "\rptFO_GozareshRoozanehPakhsh_ForoshKala.rpt")
            ElseIf Noe = 1 Then
                rpt.Load(rptPath & "\rptFO_GozareshRoozanehPakhsh_Tafkik_Ranandeh.rpt")
            ElseIf Noe = 2 Then
                rpt.Load(rptPath & "\rptFO_GozareshRoozanehPakhsh_BargashtAzForosh.rpt")
            ElseIf Noe = 3 Then
                rpt.Load(rptPath & "\rptFO_GozareshRoozanehPakhsh_FaktorResidi.rpt")
            End If

            rpttables = rpt.Database.Tables
            rpttables.Item(0).SetDataSource(dsForm.Tables("tblGozaresh"))

            rptformula = rpt.DataDefinition.FormulaFields

            With rptformula
                If Noe = 0 Then
                    .Item("CodeKala").Text = "{mydata.CodeKala}"
                    .Item("NameKala").Text = "{mydata.NameKala}"
                    .Item("Tedad").Text = "{mydata.TedadForosh}"
                    .Item("Fee").Text = "{mydata.Fee}"
                    .Item("MablaghForosh").Text = "{mydata.MablaghForosh}"
                    .Item("Marjoee").Text = "{mydata.MablaghMarjoee}"
                    .Item("ForoshKhales").Text = "{mydata.ForoshKhales}"
                    .Item("Tarikh").Text = "{mydata.TarikhSlash}"
                    .Item("Title").Text = "'" & " گزارش فـروش کـالا " & "'"
                ElseIf Noe = 1 Then
                    .Item("ShomarehTafkik").Text = "{mydata.ShomarehTafkik}"
                    .Item("NameRanandeh").Text = "{mydata.NameRanandehTozie}"
                    .Item("TedadFaktor").Text = "{mydata.TedadFaktor}"
                    .Item("JamRialiTafkik").Text = "{mydata.JamRialiTafkik}"
                    .Item("TedadDaryafti").Text = "{mydata.TedadDaryafti}"
                    .Item("TedadResidi").Text = "{mydata.TedadResidi}"
                    .Item("MablaghDaryafti").Text = "{mydata.MablaghDaryafti}"
                    .Item("TedadBargashti").Text = "{mydata.TedadBargashti}"
                    .Item("MablaghBargashti").Text = "{mydata.MablaghBargashti}"
                    .Item("MablaghTabdilBeTakhfif").Text = "{mydata.MablaghTabdilBeTakhfif}"
                    .Item("Mandeh").Text = "{mydata.Mandeh}"
                    .Item("DarsadDaryafti").Text = "{mydata.DarsadDaryafti}"
                    .Item("DarsadTabdilBeTakhfif").Text = "{mydata.DarsadTabdilBeTakhfif}"
                    .Item("DarsadBargashti").Text = "{mydata.DarsadBargashti}"
                    .Item("Tarikh").Text = "{mydata.TarikhSlash}"
                    .Item("JamMabaleghDaryaftiRooz").Text = "{mydata.JamMabaleghDaryaftiRooz}"
                    .Item("PardakhtiAzDaryaftiRooz").Text = "{mydata.PardakhtiAzDaryaftiRooz}"
                    .Item("Title").Text = "'" & " گزارش برگه تفکیک به تفکیک راننده " & "'"
                ElseIf Noe = 2 Then
                    .Item("NameForoshandeh").Text = "{mydata.NameForoshandeh}"
                    .Item("NameMamorPakhsh").Text = "{mydata.NameMamorPakhsh}"
                    .Item("CodeMoshtary").Text = "{mydata.CodeMoshtary}"
                    .Item("NameMoshtary").Text = "{mydata.NameMoshtary}"
                    .Item("ShomarehFaktor").Text = "{mydata.ShomarehFaktor}"
                    .Item("txtElatMarjoee").Text = "{mydata.txtElatMarjoee}"
                    .Item("NameKala").Text = "{mydata.NameKala}"
                    .Item("TedadKala").Text = "{mydata.TedadKala}"
                    .Item("MablaghKol").Text = "{mydata.MablaghKala}"
                    .Item("Tarikh").Text = "{mydata.TarikhSlash}"
                    .Item("Title").Text = "'" & " گزارش برگشت از فروش " & "'"
                ElseIf Noe = 3 Then
                    .Item("ShomarehFaktor").Text = "{mydata.FaktorShomareh}"
                    .Item("CodeMoshtary").Text = "{mydata.CodeMoshtary}"
                    .Item("NameMoshtary").Text = "{mydata.NameMoshtary}"
                    .Item("MablaghFaktor").Text = "{mydata.MablaghFaktor}"
                    .Item("MablaghDaryafti").Text = "{mydata.MablaghDaryafti}"
                    .Item("NameForoshandeh").Text = "{mydata.NameForoshandeh}"
                    .Item("NameRanandeh").Text = "{mydata.NameRanandeh}"
                    .Item("Tarikh").Text = "{mydata.TarikhSlash}"
                    .Item("Title").Text = "'" & " گزارش فاکتورهای رسیدی " & "'"
                End If

                .Item("Group_Sanad").Text = CodeMahalFaal
                .Item("Title2").Text = "'" & NameSherkat & "'"
                .Item("Title3").Text = "'" & objTools.ConvertNulls(objTools.DLookup("NameMahal", "tblGL_MarkazPakhsh", "CodeMahal = " & cmbMarkazPakhsh.SelectedValue), "") & "'"
                '.Item("Title3").Text = "'" & NameMahalFaal & "'"
                .Item("KarbarGozaresh").Text = "'" & PersonelName & "'"
                .Item("TarikhGozaresh").Text = "'" & objTarikh.SetDateSlash(TarikhEmrooz) & "'"
                .Item("SaatGozaresh").Text = "'" & Format(TimeOfDay, "HH:mm:ss") & "'"
            End With
            rpt.Refresh()

            frm.Text = txtCaption
            With frm.CRV
                .ReportSource = rpt
                .DisplayGroupTree = False
                .ShowGroupTreeButton = False
                If Not objSec.HaveUserPermission(SN, UD_Dll.Security.ePermissions.Print) Then .ShowPrintButton = False
                If Not objSec.HaveUserPermission(SN, UD_Dll.Security.ePermissions.Export) Then .ShowExportButton = False
            End With

            Me.Hide()
            frm.ShowDialog(Me)
            frm = Nothing
            daSQL = Nothing
            rpt = Nothing
            Me.Show()

            Windows.Forms.Cursor.Current = Cursors.Default
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->Print PishFaktor")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->Print BargehTafkik")
        End Try
    End Sub
#End Region
#Region "From Buttons "
    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        LoadForm()
    End Sub
    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub
    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        If Not objSec.HaveUserPermission(SN, UD_Dll.Security.ePermissions.Preview) Then Exit Sub
        Try
            Me.TopMost = False
            SetReport(cmbNoeGozaresh.SelectedIndex)
            Me.TopMost = True
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->btnPrintM_Click")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->btnPrintM_Click")
        End Try
    End Sub
#End Region

    Private Sub TabControl1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TabControl1.SelectedIndexChanged
        If TabControl1.SelectedIndex = 0 Then
            cmbNoeGozaresh.SelectedIndex = 0
            cmbNoeGozaresh.SelectedIndex = 0
        ElseIf TabControl1.SelectedIndex = 1 Then
            cmbNoeGozaresh.SelectedIndex = 1
            cmbNoeGozaresh.SelectedIndex = 1
        ElseIf TabControl1.SelectedIndex = 2 Then
            cmbNoeGozaresh.SelectedIndex = 2
            cmbNoeGozaresh.SelectedIndex = 2
        ElseIf TabControl1.SelectedIndex = 3 Then
            cmbNoeGozaresh.SelectedIndex = 3
            cmbNoeGozaresh.SelectedIndex = 3
        End If
    End Sub
End Class

'------ Stored Procedure ------
'' Global.spMarkazPakhsh_LoadCombo
'' Report.spGozareshRoozanehPakhsh_ForoshKala
'' Report.spGozareshRoozanehPakhsh_Tafkik_Ranandeh
'' Report.spGozareshRoozanehPakhsh_BargashtAzForosh
'' Report.spGozareshRoozanehPakhsh_FaktorResidi

'------ Report ------
'' rptFO_GozareshRoozanehPakhsh_ForoshKala.rpt
'' rptFO_GozareshRoozanehPakhsh_Tafkik_Ranandeh.rpt
'' rptFO_GozareshRoozanehPakhsh_BargashtAzForosh.rpt
'' rptFO_GozareshRoozanehPakhsh_FaktorResidi.rpt