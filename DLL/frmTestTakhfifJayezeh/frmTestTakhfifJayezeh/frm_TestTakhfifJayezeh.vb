Public Class frm_TestTakhfifJayezeh
    Const cntCodeSubSystem As Long = 1000143
    Dim ccPishFaktorTitr = 0
    Dim ccFaktorTitr As Integer = 0
    Dim ccElamMarjoee As Integer = 0
    Private SN As Integer
    Dim txtCaption As String

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetParameter()
        SN = objSec.GetSecurityNumber(cntCodeSubSystem, UserName)
        LoadCombo()

        cmbNoe.SelectedIndex = 0
        cmbNoe.SelectedIndex = 0

        objTools.ChangeKbd(UD_Dll.mdlUtility.KBD.Farsi)
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
            CodeDoreh = "1394"
            txtCaption = " تعریف و ویرایش هدف خرید"
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
    Private Sub LoadCombo()
        cmbNoe.Items.Add("اعمال بر پیش فاکتور")
        cmbNoe.Items.Add("اعمال بر مرجوعی")

        Dim cn As New SqlConnection
        Dim da As New SqlDataAdapter
        Dim dsForm As New DataSet
        Dim strSQL As String = ""

        cn = New SqlConnection(ConnectionString)
        cn.Open()

        strSQL = "SELECT LTRIM(RTRIM(STR(CodeDoreh))) AS txtCodeDoreh, CodeDoreh FROM tblFO_PishFaktor GROUP BY CodeDoreh "

        da = New SqlDataAdapter(strSQL, cn)
        da.Fill(dsForm, "tblDoreh")

        cmbDoreh.DataSource = Nothing
        cmbDoreh.Items.Clear()
        cmbDoreh.DataSource = dsForm.Tables("tblDoreh").DefaultView
        cmbDoreh.DisplayMember = "txtCodeDoreh"
        cmbDoreh.ValueMember = "CodeDoreh"

        da = Nothing
        cn.Close()

    End Sub
    Private Sub btnCalc_Click(sender As Object, e As EventArgs) Handles btnCalc.Click
        If txtShomareh.Text.Trim = "" Then
            MsgBox("شماره وارد نشده است . جهت محاسبه ، شماره را وارد نمایید . ", MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.OkOnly, "")
            Exit Sub
        End If

        If cmbNoe.SelectedIndex = 0 Then
            CalcOnPishFaktor()
        ElseIf cmbNoe.SelectedIndex = 1 Then
            CalcOnMarjoee()
        End If
    End Sub
    Private Sub CalcOnPishFaktor()
        ccPishFaktorTitr = objTools.ConvertNulls(objTools.DLookup("ccPishFaktorTitr", "tblFO_PishFaktor", "sVazeiat < 4009 AND CodeMahal = " & CodeMahalFaal & " AND CodeDoreh = " & cmbDoreh.SelectedValue & " AND PishFaktorShomareh = " & txtShomareh.Text.Trim & " AND ccPishFaktorTitr NOT IN (SELECT ccPishFaktor FROM tblFO_Faktor)"), 0)

        If ccPishFaktorTitr = 0 Then
            MsgBox("پیش فاکتوری با این مشخصات وجود ندارد و یا تایید شده است . نمیتوانید محاسبه کنید . ", MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.OkOnly, "")
        Else
            If objTools.DLookup("PishFaktorAmani", "tblFO_PishFaktor", "ccPishFaktorTitr = " & ccPishFaktorTitr) = True Then
                MsgBox("پیش فاکتور امانی است . نمیتوانید محاسبه کنید . ", MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.OkOnly, "")
            Else
                Dim TJ As New TakhfifJayezeh(ccPishFaktorTitr, 1)
                TJ.ApplyTakhfifJayezeh()
            End If
        End If
    End Sub
    Private Sub CalcOnMarjoee()
        ccElamMarjoee = objTools.ConvertNulls(objTools.DLookup("ccElamMarjoee", "tblFO_ElamMarjoee", "CodeMahal = " & CodeMahalFaal & " AND CodeDoreh = " & cmbDoreh.SelectedValue & " AND ShomarehElamMarjoee = " & txtShomareh.Text.Trim & " AND sVazeiat IN (0,1)"), 0)
        ccFaktorTitr = objTools.ConvertNulls(objTools.DLookup("ccFaktorTitr", "tblFO_ElamMarjoee", "ccElamMarjoee = " & ccElamMarjoee), 0)

        If ccElamMarjoee = 0 Then
            MsgBox("مرجوعی با این مشخصات وجود ندارد و یا تایید شده است . نمیتوانید محاسبه کنید . ", MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.OkOnly, "")
        ElseIf ccFaktorTitr = 0 Then
            MsgBox("مرجوعی بدون فاکتور است . نمیتوانید محاسبه کنید . ", MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.OkOnly, "")
        Else
            Dim TJ As New TakhfifJayezeh(ccFaktorTitr, ccElamMarjoee)
            TJ.ApplyTakhfifJayezeh()
        End If
    End Sub
    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub
End Class
