Public Class EtellatResid
#Region "Variable AND Constant Declration"
    Const cntCodeSubSystem As Long = 635

    Const FormTableName = "tblAN_KdxResid"
    Const FormViewName = "qryAN_KdxResid"
    Const FormTableNameSatr = "tblAN_kdxResidSatr"

    Const TitrGridSize As Integer = 170
    Const SatrGridSize As Integer = 150
    Const TitrOrgSize As Integer = 256
    Const SatrOrgSize As Integer = 240
    Private WithEvents BS As New UD_Dll.PassString
    Dim Mode As UD_Dll.Enums.GL_ModeForms = UD_Dll.Enums.GL_ModeForms.AddNewRecord
    Dim cmTitr As CurrencyManager
    Dim cmSatr As CurrencyManager
    Dim dvTitr As DataView
    Dim dvSatr As DataView
    Dim tCodeCounter As Long
    Dim ErrPro As New ErrorProvider
    Dim dsForm As New DataSet
    Dim dvForm As DataView
    Dim txtCaption As String
    Private SN As Integer
    Dim flg As Boolean = False
    Dim FirstInsert As Boolean = False
    Dim flgInsertSatrSefaresh As Boolean = False
    Dim flgInsertSatrMojodiMashin As Boolean = False
    Dim tmpPosition As Integer
    Dim tpos As Integer
    Dim AllowChangeFeePishFaktor As Boolean = False
#End Region
    Private Sub btnSaveSanad_Click(sender As Object, e As EventArgs) Handles btnSaveSanad.Click
        frmAN_kdxSefaresh.ccAnbarResid = cmbAnbar.SelectedValue
        frmAN_kdxSefaresh.ccTaminKonandehResid = cmbNameTaminKonandeh.SelectedValue
    End Sub

    Private Sub EtellatResid_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Dim Strsql As String
            Dim daSQL As SqlDataAdapter

            ' --------------------------------------- Load Combo -----------------
            ' Load Combo tblTaminKonandeh
            Strsql = "Select NameTaminKonandeh,ccTaminKonandeh From tblFO_TaminKonandeh where Faal=1 AND"
            Strsql &= " Not exists (Select * From tblGL_SecurityData where namekarbar= '" & UserName & "' and CodeSubSystem = 654 and pk = tblFO_TaminKonandeh.ccTaminKonandeh) order by ccTaminKonandeh"
            daSQL = New SqlDataAdapter(Strsql, ConnectionString)
            daSQL.Fill(dsForm, "tblTaminKonandeh")
            cmbNameTaminKonandeh.DataSource = Nothing
            cmbNameTaminKonandeh.Items.Clear()
            cmbNameTaminKonandeh.DataSource = dsForm.Tables("tblTaminKonandeh").DefaultView
            cmbNameTaminKonandeh.DisplayMember = "NameTaminKonandeh"
            cmbNameTaminKonandeh.ValueMember = "ccTaminKonandeh"

            ' Load Combo tblAnbar
            Strsql = "Select codeAnbar,NameAnbar From qryAN_Anbar where CodeMahal= 43 AND Faal=1  AND "
            Strsql &= " Not exists (Select * From tblGL_SecurityData where namekarbar= '" & UserName & "' and CodeSubSystem = 633 and pk = qryAN_Anbar.CodeAnbar) order by qryAN_Anbar.Radif "
            daSQL = New SqlDataAdapter(Strsql, ConnectionString)
            daSQL.Fill(dsForm, "tblAnbar")
            cmbAnbar.DataSource = Nothing
            cmbAnbar.Items.Clear()
            cmbAnbar.DataSource = dsForm.Tables("tblAnbar").DefaultView
            cmbAnbar.DisplayMember = "NameAnbar"
            cmbAnbar.ValueMember = "codeAnbar"
            daSQL = Nothing
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->LoadCombo")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->LoadCombo")
        End Try
    End Sub
End Class