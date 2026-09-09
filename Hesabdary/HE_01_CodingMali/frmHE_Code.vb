Public Class frmHE_Code
    Private SN As Integer
    Dim dsForm As New DataSet
    Dim dvForm As DataView
    Dim txtCaption As String
    Private WithEvents BS As New UD_Dll.PassString
    Const cntCodeSubSystem As Byte = 1
    Private Sub frmHE_Code_Closed(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Closed
        ' "Receive" parameter is the caption of destination window
        Dim hwnd As Long = UD_Dll.Code.FindWindow(vbNullString, ObjCode.GetNameSherkat)
        If hwnd <> 0 Then
            BS.PostString(hwnd, &H400, 0, txtCaption)
        End If

        dsForm = Nothing
        dvForm = Nothing
    End Sub
    Private Sub btnCodingMaly_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Me.Hide()
            Dim frmCodG As New frmHE_CodeGoroh
            frmCodG.ShowDialog(Me)
            Me.Show()
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->btnCodingMaly_Click")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->btnCodingMaly_Click")
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
    Private Sub btnAnvaMHazine_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAnvaMHazine.Click
        Try
            NoeTafsily = UD_Dll.Enums.HE_Tafsily.Tafsily3
            Me.Hide()
            Dim frmAnvaeT As New frmHE_CodeAnvaeTafsily
            frmAnvaeT.ShowDialog(Me)
            Me.Show()
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->btnAnvaMHazine_Click")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->btnAnvaMHazine_Click")
        End Try

    End Sub
    Private Sub btnAnvaT1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAnvaT1.Click
        Try
            NoeTafsily = UD_Dll.Enums.HE_Tafsily.Tafsily1
            Me.Hide()
            Dim frmAnvaeT As New frmHE_CodeAnvaeTafsily
            frmAnvaeT.ShowDialog(Me)
            Me.Show()
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->btnAnvaT1_Click")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->btnAnvaT1_Click")
        End Try

    End Sub
    Private Sub btnAnvaT2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAnvaT2.Click
        Try
            NoeTafsily = UD_Dll.Enums.HE_Tafsily.Tafsily2
            Me.Hide()
            Dim frmAnvaeT As New frmHE_CodeAnvaeTafsily
            frmAnvaeT.ShowDialog(Me)
            Me.Show()
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->btnAnvaT2_Click")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->btnAnvaT2_Click")
        End Try

    End Sub
    Private Sub btnMarkazH_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMarkazH.Click
        Try
            NoeTafsily = UD_Dll.Enums.HE_Tafsily.Tafsily3
            Me.Hide()
            Dim frmCodeT As New frmHE_CodeTafsily
            frmCodeT.ShowDialog(Me)
            Me.Show()
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->btnMarkazH_Click")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->btnMarkazH_Click")
        End Try

    End Sub
    Private Sub btnTafsily2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTafsily2.Click
        Try
            NoeTafsily = UD_Dll.Enums.HE_Tafsily.Tafsily2
            Me.Hide()
            Dim frmCodeT As New frmHE_CodeTafsily
            frmCodeT.ShowDialog(Me)
            Me.Show()
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->btnTafsily2_Click")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->btnTafsily2_Click")
        End Try

    End Sub
    Private Sub btnTafsily1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTafsily1.Click
        Try
            NoeTafsily = UD_Dll.Enums.HE_Tafsily.Tafsily1
            Me.Hide()
            Dim frmCodeT As New frmHE_CodeTafsily
            frmCodeT.ShowDialog(Me)
            Me.Show()
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->btnTafsily1_Click")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->btnTafsily1_Click")
        End Try

    End Sub
    Private Sub frmHE_Code_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown

        'If e.KeyCode = Keys.F1 Then
        '    Dim HelpWindow As New UD_Dll.frmGL_HelpWindow
        '    HelpWindow.CurrentCodeSubSystem = cntCodeSubSystem
        '    HelpWindow.Show()
        '    HelpWindow.TopMost = True
        'End If

    End Sub
    Private Sub frmMainSBoard_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles MyBase.Paint
        Try
            Me.TopMost = True
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->frmMainSBoard_Paint")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->frmMainSBoard_Paint")
        End Try

    End Sub
    Private Sub frmMainSBoard_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try

            SEtParameter()
            Me.TopMost = True
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->frmMainSBoard_Load")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->frmMainSBoard_Load")
        End Try

    End Sub
    Private Sub SetParameter()

        Dim commands As String = Microsoft.VisualBasic.Command()
        If commands.Length = 0 Then

            UserName = "Administrator"
            UserPassWord = "66998833"
            NameMahalFaal = "ÊåÑÇä"
            CodeMahalFaal = "1"
            PersonelCode = "0"
            PersonelName = "Administrator"
            CodeDoreh = "1389"

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
    Private Sub btnSoodZian_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSoodZian.Click
        Dim frm As New frmMain
        frm.CodeNoe = 1
        frm.ShowDialog(Me)
    End Sub

    Private Sub btnTaraz_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTaraz.Click
        Dim frm As New frmMain
        frm.CodeNoe = 2
        frm.ShowDialog(Me)
    End Sub

    Private Sub btnCodingMaly_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCodingMaly.Click
        Try
            Me.Hide()
            Dim frmCodG As New frmHE_CodeGoroh
            frmCodG.ShowDialog(Me)
            Me.Show()
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->btnCodingMaly_Click")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->btnCodingMaly_Click")
        End Try
    End Sub
End Class