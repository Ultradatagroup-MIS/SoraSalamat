Imports System.Data
Imports System.Data.SqlClient
Imports Microsoft.VisualBasic
Imports System.IO

Public Class frm_BackUp

    Private Sub frm_BackUp_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetParameter()

        ClearForm()
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


    Private Sub ClearForm()
        txtHelp.Text = "جهت ذخیره نسخه پشتیبان از اطلاعات سیستم فراروند ابتدا در درایو C پوشه ای با نام BackUp بسازید"
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            Using cn As New SqlConnection(ConnectionString)
                Using cm As SqlCommand = cn.CreateCommand()
                    cm.CommandType = CommandType.StoredProcedure
                    cm.CommandText = "[dbo].[sp_FullBackUp]"

                    cm.Connection.Open()
                    cm.ExecuteNonQuery()

                    cm.Connection.Close()
                    cm.Parameters.Clear()
                    ClearForm()
                End Using
            End Using
        Catch ex As Exception
            'Throw New Exception("Error In--> Insert Data : " & ex.Message)
            txtHelp.Text = "در درایو C پوشه ای با نام BackUp موجود نیست"
            btnSave.Text = "Ok"
        Finally
        End Try
    End Sub


End Class
