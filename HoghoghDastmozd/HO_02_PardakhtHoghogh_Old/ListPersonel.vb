Public Class ListPersonel
#Region "Variable AND Constant Declration"
    Const cntCodeSubSystem As Long = 4090
    Const FormTableName = "PayRoll.MohasebehHoghogh"
    Const FormViewName = "PayRoll.vMohasebehHoghogh"

    Dim ErrPro As New ErrorProvider
    Dim Mode As UD_Dll.Enums.GL_ModeForms = UD_Dll.Enums.GL_ModeForms.AddNewRecord
    Dim dsForm As New DataSet
    Dim dvForm As DataView
    Dim dvSatr As DataView
    Private SN As Integer
    Dim txtCaption As String
    Public MablaghVajh As Integer = 0

    Private WithEvents BS As New UD_Dll.PassString
#End Region
    Private Sub ListPersonel_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetParameter()
        SN = objSec.GetSecurityNumber(cntCodeSubSystem, UserName)
        ClearForm()
        objTools.ChangeKbd(UD_Dll.mdlUtility.KBD.Farsi)


        cmbCodeDorehS.SelectedIndex = -1
        cmbMahS.SelectedIndex = -1
    End Sub
    Private Sub ClearForm()
        LoadComboSearch()

        Mode = UD_Dll.Enums.GL_ModeForms.AddNewRecord

        cmbCodeDorehS.SelectedValue = CodeDoreh
        cmbMahS.SelectedValue = cmbMahS.SelectedValue


        chkLstPersonel.DataSource = Nothing
        chkLstPersonel.Items.Clear()

        ErrPro.Dispose()

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
            CodeDoreh = "1396"
            txtCaption = "لیست پرسنل"
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
    Private Sub LoadComboSearch()

        Dim Strsql As String
        Dim daSQL As SqlDataAdapter


        Strsql = "Select * From tblGL_Mah  order by ccMah"
        daSQL = New SqlDataAdapter(Strsql, ConnectionString)
        If dsForm.Tables.Contains("tblMahS") = True Then
            dsForm.Tables.Remove("tblMahS")
        End If
        daSQL.Fill(dsForm, "tblMahS")
        cmbMahS.DataSource = Nothing
        cmbMahS.Items.Clear()
        cmbMahS.DataSource = dsForm.Tables("tblMahS").DefaultView
        cmbMahS.DisplayMember = "txtMah"
        cmbMahS.ValueMember = "ccMah"

        Strsql = "Select Distinct CodeDoreh From tblGL_Doreh order by CodeDoreh"
        daSQL = New SqlDataAdapter(Strsql, ConnectionString)
        If dsForm.Tables.Contains("tblGL_DorehS") = True Then
            dsForm.Tables.Remove("tblGL_DorehS")
        End If
        daSQL.Fill(dsForm, "tblGL_DorehS")
        cmbCodeDorehS.DataSource = Nothing
        cmbCodeDorehS.Items.Clear()
        cmbCodeDorehS.DataSource = dsForm.Tables("tblGL_DorehS").DefaultView
        cmbCodeDorehS.DisplayMember = "CodeDoreh"
        cmbCodeDorehS.ValueMember = "CodeDoreh"

        daSQL = Nothing
    End Sub

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click

        Dim Strsql As String
        Dim daSQL As SqlDataAdapter



        'Strsql = "Select * From qryGL_MoshakhasatFardi Where CodeMahal = " & CodeMahalFaal & " AND sVazeiatEstekhdam = 4188  "
        Strsql = "Select * From qryGL_MoshakhasatFardi Where  "
        Strsql &= "  CodeFard not in (select CodeFard From PayRoll.PersonelBlockShodeh where ccMah = " & cmbMahS.SelectedValue & " AND Codedoreh = " & CodeDoreh & ") "
        Strsql &= " AND CodeFard in (select ccAfrad From PayRoll.KarkardDataEntry where CodeMah = " & cmbMahS.SelectedValue & " AND Sal = " & CodeDoreh & ") "
        Strsql &= " AND CodeFard in (select ccAfrad From PayRoll.MohasebehHoghogh where ccDariaftPardakht is  null and Mah = " & cmbMahS.SelectedValue & " AND Sal = " & CodeDoreh & ") "
        Strsql &= " AND CodeFard in (select ccAfrad From PayRoll.PersonelHokmKargoziny ) order by FN"

        daSQL = New SqlDataAdapter(Strsql, ConnectionString)
        If dsForm.Tables.Contains("tblFard") = True Then
            dsForm.Tables.Remove("tblFard")
        End If


        Dim dr As DataRow

        daSQL.Fill(dsForm, "tblFard")
        chkLstPersonel.DataSource = Nothing
        chkLstPersonel.Items.Clear()
        chkLstPersonel.DataSource = dsForm.Tables("tblFard").DefaultView
        chkLstPersonel.DisplayMember = "FN"
        chkLstPersonel.ValueMember = "CodeFard"

        'For Each dr In dsForm.Tables("tblFard").Rows
        '    dr("Taeed") = False
        'Next

        daSQL = Nothing

        cmbMahS.SelectedValue = cmbMahS.SelectedValue
    End Sub

    Private Sub btnAll_Click(sender As Object, e As EventArgs) Handles btnAll.Click
        If chkLstPersonel.CheckedItems.Count = 0 Then
            If chkLstPersonel.CheckedIndices.Count = 0 Then
                For I As Integer = 0 To chkLstPersonel.Items.Count - 1
                    chkLstPersonel.SetItemChecked(I, True)
                Next
            End If
        End If
    End Sub

    Private Sub btnOk_Click(sender As Object, e As EventArgs) Handles btnOk.Click
        Try
            Dim strPersonel As String = ""
            If chkLstPersonel.CheckedItems.Count = 0 Then
                If chkLstPersonel.CheckedIndices.Count = 0 Then
                    For I As Integer = 0 To chkLstPersonel.Items.Count - 1
                        chkLstPersonel.SetItemChecked(I, True)
                    Next
                End If
            End If
            If chkLstPersonel.CheckedItems.Count <> 0 Then
                For I As Integer = 0 To chkLstPersonel.Items.Count - 1
                    If chkLstPersonel.GetItemChecked(I) Then
                        chkLstPersonel.SelectedIndex = I
                        strPersonel &= chkLstPersonel.SelectedValue.ToString & ","
                    End If
                Next

                strPersonel = strPersonel.Remove(strPersonel.Length - 1, 1)
            End If

            Dim frm As New frmDP_PardakhtMenuCheckAzHesab

            Me.Hide()

            frm.MablaghHoghogh = objTools.ConvertNulls(objTools.DSum("MablaghKhalesDariafty", "payroll.MohasebehHoghogh", "sal = " & cmbCodeDorehS.SelectedValue & " and mah = " & cmbMahS.SelectedValue & " and ccAfrad in (" & strPersonel & ")"), 0)
            frm.PersonelHoghogh = strPersonel
            frm.MahHoghogh = cmbMahS.SelectedValue
            frm.SalHoghogh = cmbCodeDorehS.SelectedValue


            frm.ShowDialog()

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "خطاي بانک")
        End Try
    End Sub
    Private Function IsValidForm(ByVal CheckField As String) As Boolean
        IsValidForm = False

        Dim strPersonel As String = ""
        If chkLstPersonel.CheckedItems.Count = 0 Then
            If chkLstPersonel.CheckedIndices.Count = 0 Then
                For I As Integer = 0 To chkLstPersonel.Items.Count - 1
                    chkLstPersonel.SetItemChecked(I, True)
                Next
            End If
        End If
        If chkLstPersonel.CheckedItems.Count <> 0 Then
            For I As Integer = 0 To chkLstPersonel.Items.Count - 1
                If chkLstPersonel.GetItemChecked(I) Then
                    chkLstPersonel.SelectedIndex = I
                    strPersonel &= chkLstPersonel.SelectedValue.ToString & ","
                End If
            Next

            strPersonel = strPersonel.Remove(strPersonel.Length - 1, 1)
        End If


        If objTools.ConvertNulls(objTools.DSum("MablaghKhalesDariafty", "PayRoll.MohasebehHoghogh", "Mah = " & cmbMahS.SelectedValue & " And Sal = " & CodeDoreh & " and ccAfrad in (" & strPersonel & ")"), 0) <> MablaghVajh Then
            MsgBox("مبلغ وارد شده با جمع حقوق پرسنل مغایرت دارد.", MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.OkOnly, "خطا")
            Exit Function
        End If

        Return True
    End Function

End Class