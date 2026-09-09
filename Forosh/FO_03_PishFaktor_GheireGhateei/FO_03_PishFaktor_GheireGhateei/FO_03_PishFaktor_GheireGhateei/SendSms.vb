Public Class SendSms

#Region "Variable AND Constant Declration"
    Const cntCodeSubSystem As Long = 17
    Dim dsForm As New DataSet
    Dim dvForm As DataView
    Dim txtCaption As String
    Private SN As Integer
    Dim ErrPro As New ErrorProvider
    Dim flg As Boolean = False
    Public dvChecked As DataView

    Dim G_CodeGoroh As String
    Dim G_CodeKol As String
    Dim G_CodeMoeen As String
    Dim G_txtT1 As String
    Dim G_txtT2 As String
    Dim G_txtT3 As String
    Dim G_SharhGKM As String
    Dim Flag As Boolean = False

    Dim tTarikhDP As String
    Dim tSh As Long
    Dim tEbtal As Boolean
    Dim tVazeiat As Long
    Dim tTarikhVazeiat As String
    Dim tSaatVazeiat As String
    Dim tShHEntry As String

    Public Vazeiat As Integer
    Public NoeAmalyat As Integer
    Public strTafkik_GG As String
    Public CountTafkikSelected As Integer
#End Region

    Private Sub btnErsal_Click(sender As Object, e As EventArgs) Handles btnErsal.Click
        If MsgBox(" آیا می خواهید پیامک برای مشتریان فرستاده شود؟", MsgBoxStyle.Question + MsgBoxStyle.YesNo + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, "ارسال پیامک") = MsgBoxResult.Yes Then



            If SendSms_ForFaktorHaye_Tafkik(strTafkik_GG) = True Then
                MsgBox("ارسال پیامک برای مشتری ها با موفقیت انجام شد.", MsgBoxStyle.Information + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.OkOnly, "")
                Me.Close()
            Else
                MsgBox("خطا در ارسال پیامک .", MsgBoxStyle.Information + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.OkOnly, "")
                Me.Close()
            End If
        End If
        Me.Close()
    End Sub
    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.Close()
    End Sub

    '--------------- Ramezani 1395/12/09 
    Private Function SendSms_ForFaktorHaye_Tafkik(ByVal strTafkik_GG As String) As Boolean
        SendSms_ForFaktorHaye_Tafkik = False

        Dim cnSQL As New SqlConnection
        Dim cmSQL As New SqlCommand
        Dim daSQL As New SqlDataAdapter
        Dim dt As New DataTable
        Dim dr As DataRow
        Dim strSQL As String = ""

       

        ProgressBar1.Maximum = CountTafkikSelected
        ProgressBar1.Step = 1
        ProgressBar1.Value = 0

        Try
            If objTools.ConvertNulls(objTools.DLookup("SmsGheirGhateei", "tblGl_Sysconfig", "CodeMahal=" & CodeMahalFaal), 0) = 1 Then
                strSQL = "Sales.spPishFaktorGheireGhateei_SearchFaktorTafkik_ForSendSms "
            ElseIf objTools.ConvertNulls(objTools.DLookup("SmsGheirGhateei", "tblGl_Sysconfig", "CodeMahal=" & CodeMahalFaal), 0) = 2 Then
                strSQL = "Sales.spPishFaktorGheireGhateei_SearchPishFaktorTafkik_ForSendSms "
            End If

            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            cmSQL.Parameters.AddWithValue("strTafkik_GG", strTafkik_GG)

            daSQL = New SqlDataAdapter(cmSQL)
            daSQL.Fill(dt)

            cmSQL = Nothing
            cnSQL.Close()

            Dim MessageStatus = objTools.ConvertNulls(objTools.DLookup("MessageStatus", "tblGl_Sysconfig", "CodeMahal=" & CodeMahalFaal), 0)
            If MessageStatus = 1 Then
                If MsgBox("آیا مایلید پیام برای مشتری ارسال شود؟", MsgBoxStyle.YesNo + MsgBoxStyle.Question + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.DefaultButton2, "SMS") = MsgBoxResult.Yes Then
                    For Each dr In dt.Rows
                        If dr("Mobile") <> "" Then
                            Dim Mobile As String = dr("Mobile")
                            Dim MessageText As String = objTools.ConvertNulls(objTools.DLookup("TextMessageForFaktor", "tblGL_SysConfig", ""), "") + " " + CType(dr("JamKol"), String)
                            If MessageText <> "" Then
                                If Mobile.Length <> 0 Then
                                    If MessageStatus <> 0 Then
                                        Dim messageforMoshtary = objTools.ConvertNulls(objTools.DLookup("MessageForFaktor", "tblGl_Sysconfig", "CodeMahal=" & CodeMahalFaal), 0)
                                        If messageforMoshtary <> 0 Then
                                            Dim phone As String() = {Mobile}
                                            objSms.SentSms(phone, MessageText)
                                        End If
                                    End If
                                End If
                            End If
                        End If
                    Next
                End If
            ElseIf MessageStatus = 2 Then
                For Each dr In dt.Rows
                    If dr("Mobile") <> "" Then
                        Dim Mobile As String = dr("Mobile")
                        Dim MessageText As String = objTools.ConvertNulls(objTools.DLookup("TextMessageForFaktor", "tblGL_SysConfig", ""), "") + " " + CType(dr("JamKol"), String)
                        If Mobile.Length <> 0 Then
                            If MessageText <> "" Then
                                If MessageStatus <> 0 Then
                                    Dim messageforMoshtary = objTools.ConvertNulls(objTools.DLookup("MessageForFaktor", "tblGl_Sysconfig", "CodeMahal=" & CodeMahalFaal), 0)
                                    If messageforMoshtary <> 0 Then
                                        Dim phone As String() = {Mobile}
                                        objSms.SentSms(phone, MessageText)
                                    End If
                                End If
                            End If
                        End If
                    End If
                Next
            End If



            SendSms_ForFaktorHaye_Tafkik = True
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> Calc_Takhfif_Jayezeh_FromTafkik ")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> Calc_Takhfif_Jayezeh_FromTafkik ")
        End Try
    End Function
    '----------------------- Ramezani 1395/12/09




End Class