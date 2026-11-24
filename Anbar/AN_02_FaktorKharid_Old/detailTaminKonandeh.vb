Public Class detailTaminKonandeh
#Region " Variable AND Constant Declration "
    Const FormTableName = "tblFO_TaminKonandeh"
    Dim ErrPro As New ErrorProvider
    Dim Mode As UD_Dll.Enums.GL_ModeForms = UD_Dll.Enums.GL_ModeForms.AddNewRecord
    Dim dsForm As New DataSet
    Dim dvForm As DataView

    Private SN As Integer
    Dim cmForm As CurrencyManager
    Dim txtCaption As String
    Dim A As Integer

#End Region
#Region " Form Event Code "
    Private Sub detailTaminKonandeh_Load(sender As Object, e As EventArgs) Handles Me.Load

        A = ccTaminKonandeh
        lblNameTaminKonandeh.Text = NameTaminKonandeh
        Search(True)
    End Sub
    Private Sub btnUpdate_Click(sender As Object, e As EventArgs)

        If IsValidform("All") Then
            AddNewRecord()
        Else
            Exit Sub
        End If
        Me.Close()
    End Sub

#End Region
#Region " Global Form Code "
    Private Function IsValidform(ByVal CheckField As String) As Boolean
        IsValidform = False
        If txtTozihat.Text = "" Then
            ErrPro.SetError(txtTozihat, ".توضیحات را وارد کنید ")
            MsgBox(".توضیحات را وارد کنید ", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
            txtTozihat.Focus()
            Exit Function
        End If
        ErrPro.SetError(Me.txtTozihat, "")

        Return True
    End Function

    Private Sub AddNewRecord()
        Try
            Dim cnSQL As SqlConnection
            Dim cmSQL As SqlCommand
            Dim strSQL As String

            strSQL = "dbo.spTaminkonandeh_InsertTozihat"

            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()


            cmSQL.Parameters.AddWithValue("ccTaminKonandeh", ccTaminKonandeh)
            cmSQL.Parameters.AddWithValue("Tozihat", txtTozihat.Text)


            cmSQL.ExecuteNonQuery()

            cmSQL = Nothing
            cnSQL.Close()

            MsgBox("ذخیــره با موفقیت انجام شد .", MsgBoxStyle.Information + MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, "ذخیــره")


        Catch sqlExc As SqlException
            If (sqlExc.Number = 2627) Or (sqlExc.Number = 229) Then
                If Microsoft.VisualBasic.Left(sqlExc.Message, 1) = "I" Then
                    MsgBox("خطا در اضافه کردن رکورد جديد ,ثبت انجام نشد", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "خطاي بانک")
                ElseIf Microsoft.VisualBasic.Left(sqlExc.Message, 1) = "V" Then
                    MsgBox("خطا در اضافه کردن رکورد جديد ,رکورد در بانک موجود است ,ثبت انجام نشد", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "خطاي بانک")
                End If
            Else
                MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "خطاي بانک")
            End If
        End Try

    End Sub
    Private Sub Search(ByVal WithCriteria As Boolean)
        Dim StrSql As String
        Dim Cmsql As SqlCommand
        Dim cnsql As New SqlConnection(ConnectionString)
        cnsql.Open()
        Try
            StrSql = "SELECT ISNULL(tozihat , 0) FROM " & FormTableName & " Where "

            StrSql &= " ccTaminKonandeh = " & A

           


            Cmsql = New SqlCommand(StrSql, cnsql)
          
                txtTozihat.Text = Cmsql.ExecuteScalar
                cnsql.Close()


        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "Error IN DataBase ---->" & "search")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, "Error IN ---->" & "search")
        End Try
    End Sub
#End Region
End Class