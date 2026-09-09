Public Class frmFO_TaghireVaziatMarjoee

#Region "Variable AND Constant Declration"
    Const cntCodeSubSystem As Long = 658

    Const TitrGridSize As Integer = 180
    Const SatrGridSize As Integer = 222
    Const TitrOrgSize As Integer = 242
    Const SatrOrgSize As Integer = 284
    Const tblElamMarjoee = "qryFO_ElamMarjoee"

    Dim AllowMarjoeeWithoutFaktor As Boolean = objTools.ConvertNulls(objTools.DLookup("AllowMarjoeeWithoutFaktor", "tblGL_SysConfig", ""), False)
    Dim AllowChangeFeeMarjoee As Boolean = objTools.ConvertNulls(objTools.DLookup("AllowChangeFeeMarjoee", "dbo.tblGL_SysConfig", "CodeMahal = " & CodeMahalFaal), False)
    Dim AllowPishFaktorTakhfifDasty As Boolean = objTools.ConvertNulls(objTools.DLookup("AllowPishFaktorTakhfifDasty", "tblGL_SysConfig", ""), False)

    Dim Mode As UD_Dll.Enums.GL_ModeForms = UD_Dll.Enums.GL_ModeForms.AddNewRecord
    Dim cmTitr As CurrencyManager
    Dim cmSatr As CurrencyManager
    Dim dvTitr As DataView
    Dim dvSatr As DataView
    Dim tCodeCounter As Long
    Const FormTableName = "tblFO_ElamMarjoee"
    Dim ErrPro As New ErrorProvider
    Dim dsForm As New DataSet
    Dim dvForm As DataView

    Private SN As Integer
    Dim flg As Boolean = False
    Dim txtCaption As String
    Private WithEvents BS As New UD_Dll.PassString
    Dim FirstInsert As Boolean = False

    Dim ccMoshtary As Integer
    Dim ccForoshandeh As Integer = 0
    Dim ccFaktorTitr As Integer
    Dim FaktorShomareh As Integer
    Dim CodeDorehFaktor As Integer
    Dim IsMalyatAvarezTakhfif As Boolean
#End Region
#Region "Form Event Code"
    Private Sub frmFO_TaghireVaziatMarjoee_Closed(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Closed
        ' "Receive" parameter is the caption of destination window
        Dim hwnd As Long = UD_Dll.Code.FindWindow(vbNullString, ObjCode.GetNameSherkat)
        If hwnd <> 0 Then
            BS.PostString(hwnd, &H400, 0, txtCaption)
        End If

        dsForm = Nothing
        dvForm = Nothing
    End Sub
    Private Sub frmFO_TaghireVaziatMarjoee_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            SetParameter()
            SN = objSec.GetSecurityNumber(cntCodeSubSystem, UserName)

            Mode = UD_Dll.Enums.GL_ModeForms.None
            ClearForm()
            Search(True, False)
            flg = True
            SetFormObject()
            objTools.ChangeKbd(UD_Dll.mdlUtility.KBD.Farsi)
            IsMalyatAvarezTakhfif = objTools.ConvertNulls(objTools.DLookup("IsMalyatAvarezTakhfif", "tblGL_SysConfig", "CodeMahal = " & CodeMahalFaal), False)


        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->frmFO_TaghireVaziatMarjoee_Load")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->frmFO_TaghireVaziatMarjoee_Load")
        End Try
    End Sub
#End Region
#Region "Global Form Code"
    Private Sub SetParameter()
        Dim commands As String = Microsoft.VisualBasic.Command()
        If commands.Length = 0 Then
            UserName = "Administrator"
            UserPassWord = "66998833"
            NameMahalFaal = "تهران"
            CodeMahalFaal = "5"
            PersonelCode = "0"
            PersonelName = "Administrator"
            CodeDoreh = "1390"
            txtCaption = "تغییر وضعیت اعلام مرجوعی"
            ObjCode.UserName = UserName
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
    Private Sub ClearForm()
        Try
            mskTaTarikh.Text = ""
            mskAzTarikh.Text = ""
            txtShomareElamMarjoee.Text = ""

            ErrPro.Dispose()

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->ClearForm")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->ClearForm")
        End Try
    End Sub
    Private Sub Search(ByVal WithCriteria As Boolean, Optional ByVal withMSG As Boolean = True)
        Try

            Dim StrSql As String
            Dim WhereStr As String

            StrSql = "Select * from " & tblElamMarjoee
            StrSql &= " where sVazeiat = 4355 "
            If txtShomareElamMarjoee.Text <> "" Then
                StrSql &= " AND ShomarehElamMarjoee= " & txtShomareElamMarjoee.Text
            End If
            StrSql &= " And CodeMahal = " & CodeMahalFaal & " AND CodeDoreh = " & CodeDoreh & " AND "
            WhereStr = ""
            If WithCriteria Then
                If mskAzTarikh.Text <> "" Then
                    WhereStr &= "TarikhElamMarjoee >= '" & mskAzTarikh.Text & "' AND "
                End If
                If mskTaTarikh.Text <> "" Then
                    WhereStr &= "TarikhElamMarjoee <= '" & mskTaTarikh.Text & "' AND "
                End If
            End If
            WhereStr &= " 1=1"
            StrSql = StrSql & WhereStr

            RefreshTitrdata(StrSql, withMSG)
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->Search")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->Search")
        End Try
    End Sub
    Private Sub RefreshTitrdata(ByVal strSql As String, Optional ByVal WithMSG As Boolean = True)
        Try
            Dim daSQL As SqlDataAdapter
            
            If dsForm.Tables.Contains("tblFO_ReturnElamMarjoee") Then
                dsForm.Tables.Remove("tblFO_ReturnElamMarjoee")
            End If
            daSQL = New SqlDataAdapter(strSql, ConnectionString)
            daSQL.Fill(dsForm, "tblFO_ReturnElamMarjoee")

            '------------Adding Columns------------
            'dsForm.Tables("tblFO_ReturnElamMarjoee").Columns.Add("Taeed", GetType(Boolean))
            Dim col As DataColumn
            col = New DataColumn
            col.ColumnName = "Taeed"
            col.DataType = GetType(Boolean)
            col.DefaultValue = False
            dsForm.Tables("tblFO_ReturnElamMarjoee").Columns.Add(col)
            '-----------------------------------------
            Dim dr As DataRow
            For Each dr In dsForm.Tables("tblFO_ReturnElamMarjoee").Rows
                dr("Taeed") = False
            Next

            dvTitr = New DataView(dsForm.Tables("tblFO_ReturnElamMarjoee"), "", "ccElamMarjoee DESC", DataViewRowState.CurrentRows)
            dvTitr.AllowNew = False
            dvTitr.AllowDelete = False
            dvTitr.AllowEdit = True

            lblM.DataSource = Nothing
            lblM.DataSource = dvTitr
            BoundCurrencyManagerTitr()
            SetGridStyle()
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->RefreshTitrdata")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->RefreshTitrdata")
        End Try
    End Sub
    Private Sub SetGridStyle()
        Try
            Dim tableStyleTitr As New DataGridTableStyle
            tableStyleTitr.MappingName = "tblFO_ReturnElamMarjoee"
            tableStyleTitr.RowHeaderWidth = 20
            tableStyleTitr.AllowSorting = True
            lblM.BorderStyle = BorderStyle.Fixed3D

            Dim TextCol041 As New DataGridBoolColumn
            With TextCol041
                .MappingName = "Taeed"
                .HeaderText = " انتخاب "
                .Width = 60
                .ReadOnly = False                
                .Alignment = HorizontalAlignment.Center
            End With
            tableStyleTitr.GridColumnStyles.Add(TextCol041)

            Dim TextCol04 As New DataGridTextBoxColumn
            With TextCol04
                .MappingName = "ShomarehElamMarjoee"
                .HeaderText = " شماره "
                .Width = 60
                .ReadOnly = True
                .Alignment = HorizontalAlignment.Center
            End With
            tableStyleTitr.GridColumnStyles.Add(TextCol04)

            Dim TextCol03 As New DataGridTextBoxColumn
            With TextCol03
                .MappingName = "TarikhElamMarjoeeSlash"
                .HeaderText = "تاریخ"
                .Width = 80
                .ReadOnly = True
                .Alignment = HorizontalAlignment.Left
            End With
            tableStyleTitr.GridColumnStyles.Add(TextCol03)

            Dim TextCol01 As New DataGridTextBoxColumn
            With TextCol01
                .MappingName = "NameForoshandeh"
                .HeaderText = "نام فروشنده"
                .NullText = "(بدون فاکتور)"
                .Width = 150
                .ReadOnly = True
                .Alignment = HorizontalAlignment.Left
            End With
            tableStyleTitr.GridColumnStyles.Add(TextCol01)

            Dim TextCol020 As New DataGridTextBoxColumn
            With TextCol020
                .MappingName = "CodeMoshtary"
                .HeaderText = "کد مشتری"
                .Width = 60
                .ReadOnly = True
                .NullText = "(بدون فاکتور)"
                .Alignment = HorizontalAlignment.Left
            End With
            tableStyleTitr.GridColumnStyles.Add(TextCol020)

            Dim TextCol02 As New DataGridTextBoxColumn
            With TextCol02
                .NullText = "(بدون فاکتور)"
                .MappingName = "NameMoshtary"
                .HeaderText = "نام مشتری"
                .Width = 150
                .ReadOnly = True
                .Alignment = HorizontalAlignment.Left
            End With
            tableStyleTitr.GridColumnStyles.Add(TextCol02)

            Dim TextCol005 As New DataGridTextBoxColumn
            With TextCol005
                .MappingName = "JamMablaghKol"
                .HeaderText = "جمع کل مرجوعی"
                .Width = 100
                .Format = "N"
                .ReadOnly = True
                .Alignment = HorizontalAlignment.Left
            End With
            tableStyleTitr.GridColumnStyles.Add(TextCol005)


            Dim TextCol0053 As New DataGridTextBoxColumn
            With TextCol0053
                .MappingName = "JamMablagh"
                .HeaderText = "مبلغ مرجوعی"
                .Width = 100
                .Format = "N"
                .ReadOnly = True
                .Alignment = HorizontalAlignment.Left
            End With
            tableStyleTitr.GridColumnStyles.Add(TextCol0053)

            Dim TextCol0051 As New DataGridTextBoxColumn
            With TextCol0051
                .MappingName = "MablaghJayezeDasti"
                .HeaderText = "مبلغ برگشتی جایزه"
                .Width = 100
                .Format = "N"
                .ReadOnly = True
                .Alignment = HorizontalAlignment.Left
            End With
            tableStyleTitr.GridColumnStyles.Add(TextCol0051)

            Dim TextCol0052 As New DataGridTextBoxColumn
            With TextCol0052
                .MappingName = "MablaghTakhfifDasti"
                .HeaderText = "مبلغ برگشتی تخفیف"
                .Width = 100
                .Format = "N"
                .ReadOnly = True
                .Alignment = HorizontalAlignment.Left
            End With
            tableStyleTitr.GridColumnStyles.Add(TextCol0052)

            Dim TextCol061 As New DataGridTextBoxColumn
            With TextCol061
                .MappingName = "ShomarehFaktor"
                .HeaderText = "شماره فاکتور"
                .Width = 80
                .ReadOnly = True
                .Alignment = HorizontalAlignment.Left
            End With
            tableStyleTitr.GridColumnStyles.Add(TextCol061)

            Dim TextCol06 As New DataGridTextBoxColumn
            With TextCol06
                .MappingName = "CodeDorehFaktor"
                .HeaderText = "دوره فاکتور"
                .Width = 70
                .ReadOnly = True
                .Alignment = HorizontalAlignment.Left
            End With
            tableStyleTitr.GridColumnStyles.Add(TextCol06)

            Dim TextCol021 As New DataGridTextBoxColumn
            With TextCol021
                .MappingName = "Telephone"
                .HeaderText = "تلفن"
                .Width = 150
                .ReadOnly = True
                .NullText = "(بدون فاکتور)"
                .Alignment = HorizontalAlignment.Left
            End With
            tableStyleTitr.GridColumnStyles.Add(TextCol021)

            Dim TextCol022 As New DataGridTextBoxColumn
            With TextCol022
                .NullText = "(بدون فاکتور)"
                .MappingName = "NameTablo"
                .HeaderText = "نام تابلو"
                .Width = 150
                .ReadOnly = True
                .Alignment = HorizontalAlignment.Left
            End With
            tableStyleTitr.GridColumnStyles.Add(TextCol022)

            Dim TextCol09 As New DataGridTextBoxColumn
            With TextCol09
                .MappingName = "Address"
                .HeaderText = "آدرس مشتری"
                .NullText = "(بدون فاکتور)"
                .Width = 250
                .ReadOnly = True
                .Alignment = HorizontalAlignment.Left
            End With
            tableStyleTitr.GridColumnStyles.Add(TextCol09)

            lblM.Visible = True
            lblM.RowHeaderWidth = 20

            lblM.TableStyles.Clear()
            lblM.TableStyles.Add(tableStyleTitr)
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->SetGridStyle")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->SetGridStyle")
        End Try
    End Sub
    Private Sub SetFormObject()
        Try
            SetTitrButton()
            Select Case Mode
                Case UD_Dll.Enums.GL_ModeForms.None

                Case UD_Dll.Enums.GL_ModeForms.AddNewRecord

                Case UD_Dll.Enums.GL_ModeForms.AddNewRow

                Case UD_Dll.Enums.GL_ModeForms.UpdateRow

                Case UD_Dll.Enums.GL_ModeForms.UpdateRecord

            End Select


        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->SetFormObject")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->SetFormObject")
        End Try
    End Sub
    Private Sub SetTitrButton()
        Try
            Select Case Mode
                Case UD_Dll.Enums.GL_ModeForms.AddNewRecord

                Case UD_Dll.Enums.GL_ModeForms.UpdateRecord

                Case UD_Dll.Enums.GL_ModeForms.None

                Case Else

            End Select
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->SetTitrButton")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->SetTitrButton")
        End Try
    End Sub
    Private Function ReturnToElamMarjoee() As Boolean
        Try
            IsValidBeforRetrunToElamMarjoee()

            Dim CodeSanadMarjoeeAzMoshtary As Integer

            Dim ccMarjoeeAzMoshtary As Integer = objTools.ConvertNulls(objTools.DLookup("ccMarjoeeAzMoshtary", "tblAN_MarjoeeAzMoshtary", "ccElamMarjoeeTitr = " & dvTitr(cmTitr.Position)("ccElamMarjoee")), 0)
            If ccMarjoeeAzMoshtary <> 0 Then
                CodeSanadMarjoeeAzMoshtary = objTools.ConvertNulls(objTools.DLookup("CodeSanad", "tblHE_TableVasetSanad", "ccNoeSanad = " & ccMarjoeeAzMoshtary & "AND NoeSanad = 17"), 0)
                If Not CodeSanadMarjoeeAzMoshtary = 0 Then
                    Dim ShomarehMarjoeeAzMoshtary As Integer = objTools.ConvertNulls(objTools.DLookup("ShomarehMarjoeeAzMoshtary", "tblAN_MarjoeeAzMoshtary", "ccElamMarjoeeTitr = " & dvTitr(cmTitr.Position)("ccElamMarjoee")), 0)
                    MsgBox("برای این مرجوعی از مشتری-" & ShomarehMarjoeeAzMoshtary & "-سند صادر شده است، ابتدا از کارتابل سند حسابداری سند آن را حذف کنید.", )
                    Exit Try
                Else
                    If MsgBox("این اعلام مرجوعی به مرجوعی از مشنری تبدیل شده است. آیا مایلید آن را حذف کنید؟", MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.YesNo, "") = MsgBoxResult.Yes Then
                        DeleteMarjoeeAzMoshtary()
                    Else
                        Exit Try
                    End If
                End If
            End If
            Dim CodeSanadElamMarjoee As Integer = objTools.ConvertNulls(objTools.DLookup("CodeSanad", "tblHE_TableVasetSanad", "noeSanad = " & UD_Dll.Enums.HE_NoeSanadVaset.SanadForoshElamMarjoee & " AND ccNoeSanad = " & dvTitr(cmTitr.Position)("ccElamMarjoee")), 0)
            If CodeSanadElamMarjoee <> 0 Then
                MsgBox("برای این مرجوعی -" & dvTitr(cmTitr.Position)("ShomarehElamMarjoee") & "-سند صادر شده است، ابتدا از کارتابل سند حسابداری سند آن را حذف کنید.")
                Exit Try
            Else
                ReturnElamMarjoee()
                ObjCode.SabteTaghirat(CodeMahalFaal, UD_Dll.Enums.GL_NoeTaghir.UpdateRecord, "tblFO_ElamMarjoee", dvTitr(cmTitr.Position)("ccElamMarjoee"), dvTitr(cmTitr.Position)("ShomarehElamMarjoee"), "بروز رساني ")
            End If

            'Dim ccElamMarjoeeTitr As Integer = objTools.ConvertNulls(objTools.DLookup("ccElamMarjoeeTitr", "tblAN_MarjoeeAzMoshtary", " ccElamMarjoeeTitr = " & dvTitr(cmTitr.Position)("ccElamMarjoeeTitr")), 0)
            'If ccElamMarjoeeTitr = 0 Then
            '    ReturnElamMarjoee()
            '    ObjCode.SabteTaghirat(CodeMahalFaal, UD_Dll.Enums.GL_NoeTaghir.UpdateRecord, "tblFO_ElamMarjoee", dvTitr(cmTitr.Position)("ccElamMarjoee"), dvTitr(cmTitr.Position)("ShomarehElamMarjoee"), "بروز رساني ")
            'Else
            '    Dim CodeSanadElamMarjoee As Integer = objTools.ConvertNulls(objTools.DLookup("CodeSanad", "tblHE_TableVasetSanad", "noeSanad = " & UD_Dll.Enums.HE_NoeSanadVaset.SanadForoshElamMarjoee & " AND ccNoeSanad = " & ccElamMarjoeeTitr), 0)
            '    If CodeSanadElamMarjoee <> 0 Then
            '        MsgBox("", MsgBoxStyle.OkOnly, "برای این مرجوعی -" & dvTitr(cmTitr.Position)("ShomarehElamMarjoee") & "-سند صادر شده است، ابتدا از کارتابل سند حسابداری سند آن را حذف کنید.")
            '    Else
            '        DeleteMarjoeeAzMoshtary()
            '        ReturnElamMarjoee()
            '        ObjCode.SabteTaghirat(CodeMahalFaal, UD_Dll.Enums.GL_NoeTaghir.UpdateRecord, "tblFO_ElamMarjoee", dvTitr(cmTitr.Position)("ccElamMarjoee"), dvTitr(cmTitr.Position)("ShomarehElamMarjoee"), "بروز رساني ")
            '    End If
            'End If

            Mode = UD_Dll.Enums.GL_ModeForms.None
            Return True
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->ReturnToElamMarjoee")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->ReturnToElamMarjoee")
        End Try
    End Function
    Private Function IsValidBeforRetrunToElamMarjoee() As Boolean
        Try
            IsValidBeforRetrunToElamMarjoee = False

            
            Return True
        Catch sqlExc As SqlException
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->IsValidBeforRetrunToElamMarjoee")
        End Try
    End Function
    Private Function DeleteMarjoeeAzMoshtary() As Boolean
        Try
            Dim cmSQL As SqlCommand
            Dim strSql As String
            Dim cnSQL = New SqlConnection(ConnectionString)

            strSql = "DeleteMarjoeeAzMoshtary"
            cmSQL = New SqlCommand(strSql, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Add("@ShomarehElamMarjoee", SqlDbType.Int).Value = dvTitr(cmTitr.Position)("ShomarehElamMarjoee")
            cmSQL.Parameters.Add("@CodeMahal", SqlDbType.Int).Value = CodeMahalFaal
            cmSQL.Parameters.Add("@CodeDoreh", SqlDbType.Int).Value = CodeDoreh
            cmSQL.Connection.Open()
            cmSQL.ExecuteNonQuery()
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->DeleteMarjoeeAzMoshtary")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->DeleteMarjoeeAzMoshtary")
        End Try
    End Function
    Private Function ReturnElamMarjoee() As Boolean
        Try
            Dim cmSQL As SqlCommand
            Dim strSql As String
            Dim cnSQL = New SqlConnection(ConnectionString)

            strSql = "ElamMarjoee_Return"
            cmSQL = New SqlCommand(strSql, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Add("@ShomarehElamMarjoee", SqlDbType.Int).Value = dvTitr(cmTitr.Position)("ShomarehElamMarjoee")
            cmSQL.Parameters.Add("@CodeMahal", SqlDbType.Int).Value = CodeMahalFaal
            cmSQL.Parameters.Add("@CodeDoreh", SqlDbType.Int).Value = CodeDoreh
            cmSQL.Connection.Open()
            cmSQL.ExecuteNonQuery()
            cmSQL.CommandTimeout = 99999
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->IsValidBeforSaveTitr")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->IsValidBeforSaveTitr")
        End Try
    End Function
    Private Sub BoundCurrencyManagerTitr()
        Try
            cmTitr = CType(BindingContext(lblM.DataSource), CurrencyManager)

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->BoundCurrencyManagerTitr")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->BoundCurrencyManagerTitr")
        End Try
    End Sub
#End Region
#Region "From Buttons"
    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            Search(True)
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->btnSearch_Click")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->btnSearch_Click")
        End Try
    End Sub
    Private Sub btnReturn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReturn.Click
        Try
            IsValidBeforRetrunToElamMarjoee()

            For Each dr As DataRowView In dsForm.Tables("tblFO_ReturnElamMarjoee").DefaultView
                If dr("Taeed") = True Then
                    If ReturnToElamMarjoee() = False Then
                        Exit Sub
                    End If
                End If
            Next

            Search(True)
            SetFormObject()
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->btnReturn_Click")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->btnReturn_Click")
        End Try
    End Sub
    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Try
            Me.Close()
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->btnExit_Click")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->btnExit_Click")
        End Try
    End Sub
#End Region
End Class
