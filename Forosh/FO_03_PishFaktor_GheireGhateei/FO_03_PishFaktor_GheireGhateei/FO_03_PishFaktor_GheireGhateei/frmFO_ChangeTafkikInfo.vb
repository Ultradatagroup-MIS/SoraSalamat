Public Class frmFO_ChangeTafkikInfo
#Region "Variable AND Constant Declration"
    Const cntCodeSubSystem As Long = 1000126
    Dim Mode As UD_Dll.Enums.GL_ModeForms = UD_Dll.Enums.GL_ModeForms.None
    Dim ErrPro As New ErrorProvider
    Dim dsForm As New DataSet
    Private SN As Integer
    Dim tPos As Integer
    Dim txtCaption As String
    Private WithEvents BS As New UD_Dll.PassString
    Public ccTafkik_GG_ChangeInfo As Integer = 0
#End Region
    Private Sub frmFO_ChangeTafkikInfo_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadCombo()
        LoadForm()
    End Sub
    Private Sub LoadCombo()

        Dim Strsql As String
        Dim cnSQL As New SqlConnection
        Dim cmSQL As New SqlCommand
        Dim p As New SqlParameter
        Dim daSQL As SqlDataAdapter
        Dim dr As DataRow

        cnSQL = New SqlConnection(ConnectionString)
        cnSQL.Open()

        '-----  Load Combo Mamor Pakhsh
        Strsql = "Global.spMamorPakhsh_LoadCombo "

        cmSQL = New SqlCommand(Strsql, cnSQL)
        cmSQL.CommandType = CommandType.StoredProcedure
        cmSQL.Parameters.Clear()

        cmSQL.Parameters.AddWithValue("CodeMahal", CodeMahalFaal)
        cmSQL.Parameters.AddWithValue("sSemat", "," & UD_Dll.Enums.GL_Semat.MamorPakhsh & "," & UD_Dll.Enums.GL_Semat.Ranandeh & "," & UD_Dll.Enums.GL_Semat.Foroshandeh_Sayar & ",")

        daSQL = New SqlDataAdapter(cmSQL)
        daSQL.Fill(dsForm, "tblMamorPakhsh")
        daSQL.Fill(dsForm, "tblMamorPakhshS")

        cmbMamorPakhsh.DataSource = Nothing
        cmbMamorPakhsh.Items.Clear()
        cmbMamorPakhsh.DataSource = dsForm.Tables("tblMamorPakhsh").DefaultView
        cmbMamorPakhsh.DisplayMember = "FN"
        cmbMamorPakhsh.ValueMember = "CodeFard"

        dr = dsForm.Tables("tblMamorPakhshS").NewRow()
        dr("CodeFard") = 0
        dr("FN") = "همه"
        dsForm.Tables("tblMamorPakhshS").Rows.Add(dr)

        daSQL = Nothing

        '-----  Load Combo Mashin Tozie
        Strsql = "Global.spMashin_Tozie_LoadCombo"

        cmSQL = New SqlCommand(Strsql, cnSQL)
        cmSQL.CommandType = CommandType.StoredProcedure
        cmSQL.Parameters.Clear()

        cmSQL.Parameters.AddWithValue("CodeMahal", CodeMahalFaal)

        daSQL = New SqlDataAdapter(cmSQL)
        daSQL.Fill(dsForm, "tblMashinTozie")

        cmbMashinTozie.DataSource = Nothing
        cmbMashinTozie.Items.Clear()
        cmbMashinTozie.DataSource = dsForm.Tables("tblMashinTozie").DefaultView
        cmbMashinTozie.DisplayMember = "MashinFull"
        cmbMashinTozie.ValueMember = "ccMashin"

        daSQL = Nothing

        '-----  Load Combo Ranandeh Tozie
        Strsql = "Global.spRanandeh_Haml_Tozie_LoadCombo"

        cmSQL = New SqlCommand(Strsql, cnSQL)
        cmSQL.CommandType = CommandType.StoredProcedure
        cmSQL.Parameters.Clear()

        cmSQL.Parameters.AddWithValue("CodeMahal", CodeMahalFaal)
        cmSQL.Parameters.AddWithValue("sSemat", "," & UD_Dll.Enums.GL_Semat.MamorPakhsh & "," & UD_Dll.Enums.GL_Semat.Ranandeh & "," & UD_Dll.Enums.GL_Semat.Foroshandeh_Sayar & ",")

        daSQL = New SqlDataAdapter(cmSQL)
        daSQL.Fill(dsForm, "tblRanandehTozie")

        cmbRanandehTozie.DataSource = Nothing
        cmbRanandehTozie.Items.Clear()
        cmbRanandehTozie.DataSource = dsForm.Tables("tblRanandehTozie").DefaultView
        cmbRanandehTozie.DisplayMember = "FN"
        cmbRanandehTozie.ValueMember = "CodeFard_Ranandeh"

        cmSQL = Nothing : daSQL = Nothing
        cnSQL.Close()

    End Sub
    Private Sub LoadForm()
        txtShomarehTafkik.Text = objTools.DLookup("ShomarehTafkik", "Sales.TafkikJoze_PishFaktor", "ccTafkik_GG = " & ccTafkik_GG_ChangeInfo)
        cmbMamorPakhsh.SelectedValue = objTools.DLookup("ccMamorPakhsh", "Sales.TafkikJoze_PishFaktor", "ccTafkik_GG = " & ccTafkik_GG_ChangeInfo)
        cmbRanandehTozie.SelectedValue = objTools.DLookup("ccRananadehTozie", "Sales.TafkikJoze_PishFaktor", "ccTafkik_GG = " & ccTafkik_GG_ChangeInfo)
        cmbMashinTozie.SelectedValue = objTools.DLookup("ccMashinTozie", "Sales.TafkikJoze_PishFaktor", "ccTafkik_GG = " & ccTafkik_GG_ChangeInfo)
        mskTarikhPishbiniErsal.Text = objTools.DLookup("TarikhErsal", "Sales.TafkikJoze_PishFaktor", "ccTafkik_GG = " & ccTafkik_GG_ChangeInfo)
    End Sub
    Private Function UpdateInfoTafkik() As Boolean
        UpdateInfoTafkik = False

        If Len(mskTarikhPishbiniErsal.Text.ToString) <> 0 Then
            If Not objTarikh.IsShDate(mskTarikhPishbiniErsal.Text.ToString) Then
                mskTarikhPishbiniErsal.Focus()
                Exit Function
            End If
            If Microsoft.VisualBasic.Left(mskTarikhPishbiniErsal.Text, 4) <> mdlPublic.CodeDoreh Then
                MsgBox("از تاريخ وارد شده با دوره انتخاب شده مغايرت دارد.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Critical, "خطا")
                ErrPro.SetError(mskTarikhPishbiniErsal, "از تاريخ وارد شده با دوره انتخاب شده مغايرت دارد.")
                Exit Function
            End If
        Else
            ErrPro.SetError(Me.mskTarikhPishbiniErsal, " از تاريخ را وارد نمایید.")
            MsgBox(" از تاریخ را وارد نمایید.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
            mskTarikhPishbiniErsal.Focus()
            Exit Function
        End If
        ErrPro.SetError(Me.mskTarikhPishbiniErsal, "")

        objTools.DUpdate("ccMamorPakhsh", "Sales.TafkikJoze_PishFaktor", cmbMamorPakhsh.SelectedValue, "ccTafkik_GG = " & ccTafkik_GG_ChangeInfo)
        objTools.DUpdate("ccRananadehTozie", "Sales.TafkikJoze_PishFaktor", cmbRanandehTozie.SelectedValue, "ccTafkik_GG = " & ccTafkik_GG_ChangeInfo)
        objTools.DUpdate("ccMashinTozie", "Sales.TafkikJoze_PishFaktor", cmbMashinTozie.SelectedValue, "ccTafkik_GG = " & ccTafkik_GG_ChangeInfo)
        objTools.DUpdate("TarikhErsal", "Sales.TafkikJoze_PishFaktor", mskTarikhPishbiniErsal.Text, "ccTafkik_GG = " & ccTafkik_GG_ChangeInfo)

        UpdateInfoTafkik = True
    End Function
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If CheckVaznAndHajm() = False Then Exit Sub

        If UpdateInfoTafkik() = True Then
            MsgBox("اطلاعات با موفقیت ثبت گردید .", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "")
            Me.Close()
        End If
    End Sub
    Private Function CheckVaznAndHajm() As Boolean
        CheckVaznAndHajm = False

        Try
            Dim strSQL As String
            Dim drv As DataRowView
            Dim Count As Integer = 0

            Dim strPishFaktor As String = ""

            strPishFaktor = objTools.DLookupOne("[dbo].[fnFo_GetStrPishFaktorFromTafkikPishFaktor] (" & ccTafkik_GG_ChangeInfo & ")", "tblAN_Anbar", "1 = 1", "CodeAnbar DESC")

            If strPishFaktor.Length = 0 Then
                Return False
                Exit Function
            End If

            strSQL = "SELECT ccKala,SUM(Tedad3) AS Tedad "
            strSQL &= " FROM tblFO_PishFaktorSatr AS a WITH(NOLOCK) "
            strSQL &= " WHERE  a.ccPishFaktorTitr IN (" & strPishFaktor & ")"
            strSQL &= " GROUP BY ccKala"

            Dim daSQL As SqlDataAdapter
            If dsForm.Tables.Contains("tblKalaSum") Then
                dsForm.Tables.Remove("tblKalaSum")
            End If
            daSQL = New SqlDataAdapter(strSQL, ConnectionString)
            daSQL.Fill(dsForm, "tblKalaSum")

            Dim VaznTafkik As Double = 0
            Dim HajmTafkik As Double = 0
            Dim TedadKarton As Double = 0
            For Each drv In dsForm.Tables("tblKalaSum").DefaultView
                HajmTafkik += objTools.ConvertNulls(objTools.DLookup("Tol", "tblAN_Kala", "ccKala =" & drv("ccKala")), 1) * _
                        objTools.ConvertNulls(objTools.DLookup("Arz", "tblAN_Kala", "ccKala =" & drv("ccKala")), 1) * _
                        objTools.ConvertNulls(objTools.DLookup("Ertefa", "tblAN_Kala", "ccKala =" & drv("ccKala")), 1) * _
                        drv("Tedad")

                VaznTafkik += objTools.DLookup("VaznKhales", "tblAN_Kala", "ccKala =" & drv("ccKala")) * drv("Tedad")
            Next

            ''-------------------------------

            Dim HajmMashin As Double = 0
            Dim VaznMashin As Double = 0
            Dim checkHajmKala As Boolean = False
            Dim checkVaznKala As Boolean = False

            checkHajmKala = objTools.ConvertNulls(objTools.DLookup("CheckHajmMashin", "tblGL_SysConfig", "CodeMahal =" & CodeMahalFaal), 0)
            If checkHajmKala Then
                HajmMashin = objTools.ConvertNulls(objTools.DLookup("Tol", "qryFO_Mashin", "ccMashin = " & cmbMashinTozie.SelectedValue), 1) * _
                             objTools.ConvertNulls(objTools.DLookup("Arz", "qryFO_Mashin", "ccMashin = " & cmbMashinTozie.SelectedValue), 1) * _
                             objTools.ConvertNulls(objTools.DLookup("Ertefa", "qryFO_Mashin", "ccMashin = " & cmbMashinTozie.SelectedValue), 1)

                If HajmTafkik > HajmMashin Then
                    MsgBox("حجم اجناس انتخاب شده از حجم ماشین بیشتر است." & vbCrLf & " حجم ماشین :" & HajmMashin, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
                    Exit Function
                End If
            End If

            checkVaznKala = objTools.ConvertNulls(objTools.DLookup("CheckVaznMashin", "tblGL_SysConfig", "CodeMahal =" & CodeMahalFaal), 0)
            If checkVaznKala Then
                VaznMashin = objTools.ConvertNulls(objTools.DLookup("Vazn", "qryFO_Mashin", "ccMashin =" & cmbMashinTozie.SelectedValue), 1)

                If VaznTafkik > VaznMashin Then
                    MsgBox("وزن اجناس انتخاب شده از وزن قابل حمل ماشین بیشتر است." & vbCrLf & " وزن  قابل حمل ماشین : " & VaznMashin & " کیلوگرم " & vbCrLf & " جمع وزن کالاها  : " & VaznTafkik & " کیلوگرم ", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
                    Exit Function
                End If
            End If

            ''-------------------------------

            Return True
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> CheckVaznAndHajm ")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> CheckVaznAndHajm ")
        End Try
    End Function
    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub
End Class