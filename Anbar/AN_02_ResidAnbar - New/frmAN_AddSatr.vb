Public Class frmAN_AddSatr




    Private Sub frmAN_AddSatr_Load(sender As Object, e As EventArgs) Handles MyBase.Load




        ccTaminKonandeh = objTools.ConvertNulls(objTools.DLookup("ccTaminKonandeh", "tblAN_KdxResid", "ccKardexTitr = " & ccTitr), 0)
        TarikhForm = objTools.ConvertNulls(objTools.DLookup("TarikhForm", "tblAN_KdxResid", "ccKardexTitr = " & ccTitr), 0)
        'IsMahsol = objTools.ConvertNulls(objTools.DLookup("IsMahsol", "tblAN_KdxResid", "ccKardexTitr = " & ccTitr), 0)
        'ccAnbar = objTools.ConvertNulls(objTools.DLookup("ccAnbar", "tblAN_KdxResid", "ccKardexTitr = " & ccTitr), 0)
        'ccSefareshTitr = objTools.ConvertNulls(objTools.DLookup("ccSefareshTitr", "tblAN_KdxResid", "ccKardexTitr = " & ccTitr), 0)
        'ResidMavadAvalieh = objTools.ConvertNulls(objTools.DLookup("ResidMavadAvalieh", "tblAN_KdxResid", "ccKardexTitr = " & ccTitr), 0)
        'NoeResid = objTools.ConvertNulls(objTools.DLookup("NoeResid", "tblAN_KdxResid", "ccKardexTitr = " & ccTitr), 0)



        Dim da As SqlDataAdapter = New SqlDataAdapter
            Dim dt_SearchSatr As Data.DataTable = Nothing
            Using cn As New SqlConnection(ConnectionString)
                Using cm As SqlCommand = cn.CreateCommand()
                    cn.Open()
                    cm.Parameters.Clear()
                    cm.CommandType = CommandType.StoredProcedure
                    cm.CommandText = "[WareHouse].[sp_ResidAnbar_SearchSatr]"
                    cm.Parameters.AddWithValue("ccKardexSatr", ccSatr)
                    da.SelectCommand = cm
                    cm.CommandTimeout = 999999
                    dt_SearchSatr = New DataTable
                    da.Fill(dt_SearchSatr)
                End Using
            End Using
            If dt_SearchSatr.Rows.Count <> 0 Then
            txtCodeKala.Enabled = False
            txtShomarehBatch.Enabled = False
            mskAzTarikh.Enabled = False
            txtCodeKala.Tag = dt_SearchSatr.Rows(0)("ccKala")
            txtCodeKala.Text = dt_SearchSatr.Rows(0)("CodeKala")
            lblNameKala.Text = dt_SearchSatr.Rows(0)("NameKala")
            txtMablaghKharid.Text = dt_SearchSatr.Rows(0)("MablaghKharid").ToString.Replace(",", ".")
            mskTarikhTolid.Text = dt_SearchSatr.Rows(0)("TarikhTolid")
            mskTarikhEngheza.Text = dt_SearchSatr.Rows(0)("TarikhEngheza")
            txtShomarehBatch.Text = dt_SearchSatr.Rows(0)("ShomarehBach")
            mskAzTarikh.Text = TarikhForm
            mskTaTarikh.Text = CodeDoreh.ToString + "1229"
            TextIRC.Text = dt_SearchSatr.Rows(0)("IRC")
            TextGTIN.Text = dt_SearchSatr.Rows(0)("GTIN")



        End If



    End Sub





    Private Sub btnSaveSanad_Click(sender As Object, e As EventArgs) Handles btnSaveSanad.Click



        UpdateRow()
        ClearForm()
        MsgBox("قیمت های سه گانه با موفقیت ثبت شدند.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
        Me.Close()


    End Sub
    Private Sub UpdateRow()
        Try
            If Not IsValidRow("All") Then
                Exit Sub
            End If


            Using cn As New SqlConnection(ConnectionString)
                Using cm As SqlCommand = cn.CreateCommand()
                    cn.Open()
                    cm.Parameters.Clear()
                    cm.CommandType = CommandType.StoredProcedure
                    cm.CommandText = "[WareHouse].[sp_ResidAnbar_InsertKalaGheymat]"
                    cm.Parameters.AddWithValue("ccKardexSatr", ccSatr)
                    cm.Parameters.AddWithValue("ccKala", txtCodeKala.Tag)
                    cm.Parameters.AddWithValue("MablaghKharid", txtMablaghKharid.Text)
                    cm.Parameters.AddWithValue("MablaghForosh", txtForosh.Text)
                    cm.Parameters.AddWithValue("MablaghMasraf", txtMasrafkonandeh.Text)
                    cm.Parameters.AddWithValue("TarikhTolid", mskTarikhTolid.Text)
                    cm.Parameters.AddWithValue("TarikhEngheza", mskTarikhEngheza.Text)
                    cm.Parameters.AddWithValue("ShomarehBach", txtShomarehBatch.Text)
                    cm.Parameters.AddWithValue("IRC", TextIRC.Text)
                    cm.Parameters.AddWithValue("GTIN", TextGTIN.Text)
                    cm.Parameters.AddWithValue("AzTarikh", mskAzTarikh.Text)
                    cm.Parameters.AddWithValue("TaTarikh", mskTaTarikh.Text)
                    cm.Parameters.AddWithValue("ccTaminKonandeh", ccTaminKonandeh)
                    cm.Parameters.AddWithValue("CodeMahal", CodeMahalFaal)
                    cm.Parameters.AddWithValue("UserName", UserName)
                    cm.CommandTimeout = 999999
                    cm.ExecuteNonQuery()
                End Using
            End Using





        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->UpdateRow")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->UpdateRow")
        End Try
    End Sub
    Private Sub RefreshSatrData()
        Try



            '    Dim Strsql As String
            '    Dim daSQL As SqlDataAdapter

            '    If dsForm.Tables.Contains("tblAN_KdxResidSatr") Then
            '        dsForm.Tables.Remove("tblAN_KdxResidSatr")
            '    End If

            '    Strsql = "Select *  ,isnull(case when ccSefareshTitr<>0 then  "
            '    Strsql &= " (select SUM(Tedad3) as Tedad FROM qryAN_kdxSefareshSatr WHERE   qryAN_kdxSefareshSatr.ccKardexTitr="
            '    Strsql &= " a.ccSefareshTitr AND   qryAN_kdxSefareshSatr.ccKala = a.ccKala ) "
            '    Strsql &= " - "
            '    Strsql &= " (select sum(tedad3) from qryAN_kdxResidSatr where ccKardexTitr in "
            '    Strsql &= " (select ccKardexTitr from qryAN_kdxResid where ccSefareshTitr =a.ccSefareshTitr )"
            '    Strsql &= " AND qryAN_kdxResidSatr.ccKala = a.ccKala "
            '    Strsql &= " AND qryAN_kdxResidSatr.ccKardexSatr <=a.ccKardexSatr "
            '    Strsql &= " ) End ,0) as TedadMandehDarResid "
            '    Strsql &= " ,isnull((select sum(tedad3) from dbo.qryAN_kdxResidTitrSatr where cckala =a.ccKala"
            '    Strsql &= " and ccSefareshTitr=a.ccSefareshTitr and ccSefareshTitr<>0 ),0) TedadKolResidShodeh "
            '    Strsql &= " From qryAN_KdxResidSatr a Where ccKardexTitr = " & ccTitr

            '    daSQL = New SqlDataAdapter(Strsql, ConnectionString)
            '    daSQL.Fill(dsForm, "tblAN_KdxResidSatr")

            '    dvSatr = New DataView(dsForm.Tables("tblAN_KdxResidSatr"))

            '    dvSatr.Sort = "Radif asc"

            '    dvSatr.AllowNew = False
            '    dvSatr.AllowDelete = True
            '    dvSatr.AllowEdit = False

            '    daSQL = Nothing
            '    frmAN_ResidTolid.GridEXSatr.DataSource = Nothing
            '    frmAN_ResidTolid.GridEXSatr.DataSource = dvSatr


        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->RefreshSatrData")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->RefreshSatrData")
        End Try
    End Sub

    Private Sub ClearForm()
        Try




            Me.txtCodeKala.Text = ""
            Me.txtCodeKala.Tag = ""
            Me.lblNameKala.Text = ""





            mskTarikhEngheza.Text = ""
            mskTarikhTolid.Text = ""


            txtMablaghKharid.Text = ""
            txtShomarehBatch.Text = ""
















        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->ClearForm")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->ClearForm")
        End Try
    End Sub
    Private Function IsValidRow(ByVal chkField As String) As Boolean
        Try
            IsValidRow = False



            'If Mode = UD_Dll.Enums.GL_ModeForms.AddNewRow Then
            '    If objTools.ConvertNulls(objTools.DLookup("ccKala", "qryAN_Kala", "CodeKala=" & Me.txtCodeKala.Text & " AND ccTaminKonandeh =" & dvTitr(cmTitr.Position)("ccTaminKonandeh")), 0) = 0 Then
            '        ErrPro.SetError(Me.txtCodeKala, "کالای وارد شده در لیست کالاهای تامین کننده مورد نظر وجود ندارد .")
            '        MsgBox("کالای وارد شده در لیست کالاهای تامین کننده مورد نظر وجود ندارد .", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
            '        Exit Function
            '    End If
            '    ErrPro.SetError(txtCodeKala, "")
            'End If




            ' Check Kardan Sys Config  baraye vared kardan ya nakardane tarikh masraf
            'Select Case objCode.CheckTarikhMasraf
            '    Case UD_Dll.Enums.GL_SysConfig.VaredShavad
            '        If chkField = "mskTarikhTolid" Or chkField = "All" Then
            '            If mskTarikhTolid.Text.Length <> 0 Then
            '                If Not objTarikh.IsShDate(mskTarikhTolid.Text.ToString) Then
            '                    mskTarikhEngheza.Focus()
            '                    Exit Function
            '                End If
            '            Else
            '                ErrPro.SetError(Me.mskTarikhTolid, "تاريخ تولید را وارد کنيد.")
            '                MsgBox("تاريخ تولید را وارد کنيد.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
            '                mskTarikhTolid.Focus()
            '                Exit Function
            '            End If
            '            ErrPro.SetError(Me.mskTarikhTolid, "")
            '        End If

            If chkField = "txtMablaghKharid" Or chkField = "All" Then
                If (txtMablaghKharid.Text.Length = 0 Or txtMablaghKharid.Text = "0") Then

                    ErrPro.SetError(Me.txtMablaghKharid, "قیمت خرید را وارد کنيد.")
                    MsgBox("قیمت خرید را وارد کنيد.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
                    txtMablaghKharid.Focus()
                    Exit Function
                End If
                ErrPro.SetError(Me.txtMablaghKharid, "")
            End If

            If chkField = "txtForosh" Or chkField = "All" Then
                If (txtForosh.Text.Length = 0 Or txtForosh.Text = "0") Then

                    ErrPro.SetError(Me.txtForosh, "قیمت فروش را وارد کنيد.")
                    MsgBox("قیمت فروش را وارد کنيد.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
                    txtForosh.Focus()
                    Exit Function
                End If
                ErrPro.SetError(Me.txtForosh, "")
            End If



            If chkField = "txtMasrafkonandeh" Or chkField = "All" Then
                If (txtMasrafkonandeh.Text.Length = 0 Or txtMasrafkonandeh.Text = "0") Then

                    ErrPro.SetError(Me.txtMasrafkonandeh, "قیمت مصرف کننده را وارد کنيد.")
                    MsgBox("قیمت مصرف کننده را وارد کنيد.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
                    txtMasrafkonandeh.Focus()
                    Exit Function
                End If
                ErrPro.SetError(Me.txtMasrafkonandeh, "")
            End If
            'End Select


            If chkField = "mskTaTarikh" Or chkField = "All" Then
                If Len(mskTaTarikh.Text.ToString) <> 0 Then
                    If Not objTarikh.IsShDate(mskTaTarikh.Text.ToString) Then
                        mskTaTarikh.Focus()
                        Exit Function
                    End If
                    If mskTaTarikh.Text.Substring(0, 4) <> CodeDoreh Then
                        MsgBox("تا تاريخ با دوره مالی فعال يکی نيست.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
                        mskTaTarikh.Focus()
                        Exit Function
                    End If
                Else
                    ErrPro.SetError(Me.mskTaTarikh, "تا تاريخ را وارد کنيد.")
                    MsgBox("تا تاريخ را وارد کنيد.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
                    mskTaTarikh.Focus()
                    Exit Function
                End If
                ErrPro.SetError(Me.mskTaTarikh, "")
            End If


            Return True


        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->IsValidRow")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->IsValidRow")
        End Try

    End Function

    'Private Function IsValidMojodiMax(ByVal Tedad1 As Double, Optional ByVal ccKala As Integer = -1) As Boolean
    '    IsValidMojodiMax = False

    '    Dim dtError As New DataTable
    '    Dim dr As DataRow

    '    Dim TedadPishFaktorFaktorNashodeh As Double = 0
    '    Dim MojodiFely As Double = 0
    '    Dim MojodiGhabelForosh As Double = 0

    '    'Create DataColumn For Data Table
    '    dtError.Columns.Add("NameKala", Type.GetType("System.String"))
    '    dtError.Columns.Add("Tedad", Type.GetType("System.Double"))
    '    dtError.Columns.Add("Mojod", Type.GetType("System.Double"))
    '    dtError.Columns.Add("TedadKast", Type.GetType("System.Double"))

    '    Dim ccAnbarAsly As Integer = ccAnbar
    '    'GetMojodyDarHalForosh(ccKala, ccAnbarAsly, TedadPishFaktorFaktorNashodeh, MojodiFely, MojodiGhabelForosh)
    '    Dim MojodiKala As Double = ObjCode.MojodiAnbar(ccAnbar, Str(CodeDoreh).Trim + "0101", TarikhEmrooz, ccKala)
    '    Dim Max1 As Double = objTools.ConvertNulls(objTools.DLookup("Max", "tblAN_MinMaxSefaresh", " ccanbar =" & ccAnbarAsly & "And cckala= " & ccKala & ""), 0)


    '    'If Val(MojodiKala) > Max1 Then
    '    '    MsgBox(" موجودی بیش از نقطه ماکزیمم می باشد", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "پیغام")
    '    'End If


    '    Return True
    'End Function

    Private Sub btnCancelSanad_Click(sender As Object, e As EventArgs) Handles btnCancelSanad.Click
        ClearForm()
        Close()
    End Sub
End Class