Public Class frmFO_AddNewTakhfifJayezeh

#Region "Variable AND Constant Declration"
    Public dsForm As New DataSet
    Public dvForm As DataView
    Public dvTitr As DataView

    Dim ErrPro As New ErrorProvider
    Dim cmForm As CurrencyManager
    Private SN As Integer
    Const cntCodeSubSystem As Long = 885
    Dim txtCaption As String
    Private WithEvents BS As New UD_Dll.PassString
    Dim Mode As Integer = 0 '' 0 = Sabte Etelaate Avalieh \ 1 = Dasteh Bandy Moshtarian \ 2 = Dasteh Bandy Kalaei \ 3 = Taeed Nahaei
    Dim Flg As Boolean = False
    Dim FlgSearch As Boolean = False
    Dim ModeDastehBandyMoshtary As Integer = 0 '' 0 = Hameh \ 1 = Moshtary \ 2 = NoeMoshtary \ 3 = NoeSenf \ 4 = Manategh \ 5 = Not Moshtary \ 6 = Not NoeMoshtary \ 7 = Not NoeSenf \ 8 = Not Manategh

    Dim strMoshtary_Valid As String = ","
    Dim strMoshtary_NoeMoshtary_Valid As String = ","
    Dim strMoshtary_NoeSenf_Valid As String = ","
    Dim strMoshtary_Mantagheh_Valid As String = ","
    Dim strMoshtary_NotEffect As String = ","
    Dim strNoeMoshtary_NotEffect As String = ","
    Dim strNoeSenf_NotEffect As String = ","
    Dim strManategh_NotEffect As String = ","

    Dim strKala_Effect As String = ","
    Dim strKala_Brand_Effect As String = ","
    Dim strKala_GorohKala_Effect As String = ","

#End Region
    Private Sub frmFO_AddNewTakhfifJayezeh_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetParameter()
        SN = objSec.GetSecurityNumber(cntCodeSubSystem, UserName)
        LoadCombo()
        Flg = True
        ClearForm()
        cmbDastehBandyMoshtary.SelectedIndex = 0
        cmbNoeAeenNameh.SelectedIndex = 0
        cmbNoeFieldJayezeh.SelectedIndex = 0
        SetForm(Mode)
        objTools.ChangeKbd(UD_Dll.mdlUtility.KBD.Farsi)
    End Sub
    Private Sub SetParameter()

        Dim commands As String = Microsoft.VisualBasic.Command()
        If commands.Length = 0 Then

            UserName = "ajak61"
            UserPassWord = "66998833"
            NameMahalFaal = "تهران"
            CodeMahalFaal = "1"
            PersonelCode = "0"
            PersonelName = "Administrator"
            CodeDoreh = "1396"
            txtCaption = "تخفیفات و جوایز ترکیبی"
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
    Private Sub LoadCombo()
        Dim cnSQL As New SqlConnection
        Dim cmSQL As New SqlCommand
        Dim daSQL As New SqlDataAdapter
        Dim strSQL As String = ""
        Dim d As DataRow

        cnSQL = New SqlConnection(ConnectionString)
        cnSQL.Open()

        cmbNoeAeenNameh.Items.Add("------")
        cmbNoeAeenNameh.Items.Add("تخـفیف")
        cmbNoeAeenNameh.Items.Add("جایــزه")

        cmbNoeAzTa.Items.Add("------")
        cmbNoeAzTa.Items.Add("تعـــداد")
        cmbNoeAzTa.Items.Add("ریــــال")

        cmbNoeBastehBandy.Items.Add("------")
        cmbNoeBastehBandy.Items.Add("تعـــداد")
        cmbNoeBastehBandy.Items.Add("بسـتـــه")
        cmbNoeBastehBandy.Items.Add("کـارتــن")
        cmbNoeBastehBandy.Items.Add("سطـر فاکتور")

        LoadCombo_NoeEhdaei(0)

        cmbNoeBastehBandyJayezeh.Items.Add("------")
        cmbNoeBastehBandyJayezeh.Items.Add("تعـــداد")
        cmbNoeBastehBandyJayezeh.Items.Add("بسـتـــه")
        cmbNoeBastehBandyJayezeh.Items.Add("کـارتــن")

        '------------------------------------------------------------------------------

        strSQL = "Select * From tblGL_ShenasehOmomi Where CodeAsli = 151 And CodeFarei <> 0  order by MeghdarAdadi"

        cmSQL = New SqlCommand(strSQL, cnSQL)

        daSQL = New SqlDataAdapter(cmSQL)
        daSQL.Fill(dsForm, "tblNoePardakht")

        chklstNoePardakht.DataSource = Nothing
        chklstNoePardakht.Items.Clear()
        chklstNoePardakht.DataSource = dsForm.Tables("tblNoePardakht")
        chklstNoePardakht.DisplayMember = "Sharh"
        chklstNoePardakht.ValueMember = "Code"
        chklstNoePardakht.SelectedValue = 0

        cmSQL = Nothing

        '------------------------------------------------------------------------------

        cmbDastehBandyMoshtary.Items.Add("------")
        cmbDastehBandyMoshtary.Items.Add("مشتریـان خاص")
        cmbDastehBandyMoshtary.Items.Add("نـوع مشتــری")
        cmbDastehBandyMoshtary.Items.Add("نـوع صنـف")
        cmbDastehBandyMoshtary.Items.Add("منطقـــه ای")
        cmbDastehBandyMoshtary.Items.Add("همـــه")

        cmbNoeFieldNotEffectMoshtary.Items.Add("------")
        cmbNoeFieldNotEffectMoshtary.Items.Add("مشتریـان خاص")
        cmbNoeFieldNotEffectMoshtary.Items.Add("نـوع مشتــری")
        cmbNoeFieldNotEffectMoshtary.Items.Add("نـوع صنـف")
        cmbNoeFieldNotEffectMoshtary.Items.Add("منطقـــه ای")

        cmbNoeFieldJayezeh.Items.Add("------")
        cmbNoeFieldJayezeh.Items.Add("بـه کالا")
        cmbNoeFieldJayezeh.Items.Add("بـه بـرنـــد")
        cmbNoeFieldJayezeh.Items.Add("بـه گـروه کالا")
        cmbNoeFieldJayezeh.Items.Add("بـه ریـال خالص فاکتـور")
        cmbNoeFieldJayezeh.Items.Add("بـه ریـال نا خالص فاکتـور")

        cmbNoeMohasebeh.Items.Add("------")
        cmbNoeMohasebeh.Items.Add("بر روی ترکیب کالاها")

        cmbEffectOn.Items.Add("------")
        cmbEffectOn.Items.Add("آیتم های انتخاب شده")
        'cmbEffectOn.Items.Add("ریـال ناخالص فاکتور")
        'cmbEffectOn.Items.Add("ریـال خالص فاکتور")

        '------------------------------------------------------------------------------

        strSQL = "Select * From tblGL_ShenasehOmomi Where CodeAsli= 152 And CodeFarei<>0 order by Sharh"
        daSQL = New SqlDataAdapter(strSQL, ConnectionString)
        daSQL.Fill(dsForm, "sNoeMoshtary")
        cmbNoeMoshtary.DataSource = Nothing
        cmbNoeMoshtary.Items.Clear()
        cmbNoeMoshtary.DataSource = dsForm.Tables("sNoeMoshtary").DefaultView
        cmbNoeMoshtary.DisplayMember = "Sharh"
        cmbNoeMoshtary.ValueMember = "Code"

        '------------------------------------------------------------------------------

        strSQL = "Select * From tblGL_ShenasehOmomi Where CodeAsli= 150 And CodeFarei <> 0 order by Sharh"
        daSQL = New SqlDataAdapter(strSQL, ConnectionString)
        daSQL.Fill(dsForm, "sNoeSenf")
        With cmbNoeSenf
            .DataSource = Nothing
            .Items.Clear()
            .DataSource = dsForm.Tables("sNoeSenf").DefaultView
            .DisplayMember = "Sharh"
            .ValueMember = "Code"
        End With

        '_________________ Load Ostan Shahr Mantagheh __________________

        strSQL = "SELECT * FROM tblGL_ShenasehOmomi WHERE CodeAsli = 47 AND CodeFarei <> 0 OR Code = 0 ORDER BY Sharh"
        daSQL = New SqlDataAdapter(strSQL, ConnectionString)
        daSQL.Fill(dsForm, "tbl_Ostan")

        strSQL = "SELECT * FROM tblGL_ShenasehOmomi WHERE CodeAsli = 23 AND CodeFarei <> 0 OR Code =0 ORDER BY Sharh"
        daSQL = New SqlDataAdapter(strSQL, ConnectionString)
        daSQL.Fill(dsForm, "tbl_Shahr")

        strSQL = "SELECT * FROM tblGL_ShenasehOmomi WHERE CodeAsli = 21 AND CodeFarei <> 0 OR Code = 0 ORDER BY Sharh"
        daSQL = New SqlDataAdapter(strSQL, ConnectionString)
        daSQL.Fill(dsForm, "tbl_Mantagheh")

        strSQL = "SELECT * FROM tblGL_ShenasehOmomi WHERE CodeAsli = 1 AND CodeFarei <> 0 OR Code = 0 ORDER BY Sharh"
        daSQL = New SqlDataAdapter(strSQL, ConnectionString)
        daSQL.Fill(dsForm, "tbl_Mahaleh")

        cmbOstan.DataSource = Nothing
        cmbOstan.Items.Clear()
        cmbOstan.DataSource = dsForm.Tables("tbl_Ostan").DefaultView
        cmbOstan.DisplayMember = "Sharh"
        cmbOstan.ValueMember = "Code"

        '_________________ Load Goroh1 Goroh2 Goroh3 Goroh4 Goroh5 __________________

        strSQL = "SELECT code,sharh,codelink FROM tblGL_ShenasehOmomi WHERE CodeAsli = 200 AND CodeFarei <> 0 ORDER BY sharh"
        daSQL = New SqlDataAdapter(strSQL, ConnectionString)
        daSQL.Fill(dsForm, "tbl_Goroh1")

        strSQL = "SELECT code,sharh,codelink FROM tblGL_ShenasehOmomi WHERE CodeAsli = 201 AND CodeFarei <> 0 ORDER BY sharh"
        daSQL = New SqlDataAdapter(strSQL, ConnectionString)
        daSQL.Fill(dsForm, "tbl_Goroh2")

        strSQL = "SELECT code,sharh,codelink FROM tblGL_ShenasehOmomi WHERE CodeAsli = 202 AND CodeFarei <> 0 ORDER BY sharh"
        daSQL = New SqlDataAdapter(strSQL, ConnectionString)
        daSQL.Fill(dsForm, "tbl_Goroh3")

        strSQL = "SELECT code,sharh,codelink FROM tblGL_ShenasehOmomi WHERE CodeAsli = 203 AND CodeFarei <> 0 ORDER BY sharh"
        daSQL = New SqlDataAdapter(strSQL, ConnectionString)
        daSQL.Fill(dsForm, "tbl_Goroh4")

        strSQL = "SELECT code,sharh,codelink FROM tblGL_ShenasehOmomi WHERE CodeAsli = 204 AND CodeFarei <> 0 ORDER BY sharh"
        daSQL = New SqlDataAdapter(strSQL, ConnectionString)
        daSQL.Fill(dsForm, "tbl_Goroh5")

        cmbG1.DataSource = Nothing
        cmbG1.Items.Clear()
        cmbG1.DataSource = dsForm.Tables("tbl_Goroh1").DefaultView
        cmbG1.DisplayMember = "Sharh"
        cmbG1.ValueMember = "Code"
        cmbG1.SelectedIndex = -1
        cmbG1.SelectedIndex = -1

        strSQL = "Select * From tblFO_Brand "
        daSQL = New SqlDataAdapter(strSQL, ConnectionString)
        daSQL.Fill(dsForm, "tblBrand")
        cmbBrand.DataSource = Nothing
        cmbBrand.Items.Clear()
        cmbBrand.DataSource = dsForm.Tables("tblBrand").DefaultView
        cmbBrand.DisplayMember = "NameBrand"
        cmbBrand.ValueMember = "ccBrand"

        strSQL = "Select * From tblFO_Brand "
        daSQL = New SqlDataAdapter(strSQL, ConnectionString)
        daSQL.Fill(dsForm, "tblBrand_Kala")

        d = dsForm.Tables("tblBrand_Kala").NewRow
        d("NameBrand") = "همه"
        d("ccBrand") = 0
        dsForm.Tables("tblBrand_Kala").Rows.Add(d)

        cmbBrandKala.DataSource = Nothing
        cmbBrandKala.Items.Clear()
        cmbBrandKala.DataSource = dsForm.Tables("tblBrand_Kala").DefaultView
        cmbBrandKala.DisplayMember = "NameBrand"
        cmbBrandKala.ValueMember = "ccBrand"

    End Sub
    Private Sub LoadCombo_NoeEhdaei(ByVal Type As Integer)
        '' Type ---> 1 : Takhfif / 2 : Jayezeh

        cmbNoeTakhfifEhdaei.Items.Clear()

        cmbNoeTakhfifEhdaei.Items.Add("------")

        If Type = 1 Then
            cmbNoeTakhfifEhdaei.Items.Add("جایـزه کالایی")
            cmbNoeTakhfifEhdaei.Items.Add("درصـــد")
            cmbNoeTakhfifEhdaei.Items.Add("ریـال ثابت")
        ElseIf Type = 2 Then
            cmbNoeTakhfifEhdaei.Items.Add("جایـزه کالایی")
        End If
    End Sub
    Private Sub SetForm(ByVal Mode As Integer)
        SetFormView(Mode)
    End Sub
    Private Sub SetFormView(ByVal Mode As Integer)
        Dim P As Integer
        Select Case Mode
            Case 0
                lblPrimaryInfo.Font = BoldFont(lblPrimaryInfo, True)
                lblTypeMoshtary.Font = BoldFont(lblTypeMoshtary, False)
                lblTypeKala.Font = BoldFont(lblTypeKala, False)
                lblTaeedNahaei.Font = BoldFont(lblTaeedNahaei, False)
                lblTypeMoshtary.Enabled = False
                lblTypeKala.Enabled = False
                lblTaeedNahaei.Enabled = False
                grbPrimaryInfo.Visible = True
                grbTypeMoshtary.Visible = False
                grbTypeKala.Visible = False
                grbTaeedNahaei.Visible = False
                btnPrv.Enabled = False
                btnNext.Enabled = True
                btnSave.Enabled = False
            Case 1
                lblPrimaryInfo.Font = BoldFont(lblPrimaryInfo, False)
                lblTypeMoshtary.Font = BoldFont(lblTypeMoshtary, True)
                lblTypeKala.Font = BoldFont(lblTypeKala, False)
                lblTaeedNahaei.Font = BoldFont(lblTaeedNahaei, False)
                lblTypeMoshtary.Enabled = True
                lblTypeKala.Enabled = False
                lblTaeedNahaei.Enabled = False
                grbPrimaryInfo.Visible = False
                grbTypeMoshtary.Visible = True
                grbTypeKala.Visible = False
                grbTaeedNahaei.Visible = False
                btnPrv.Enabled = True
                btnNext.Enabled = True
                btnSave.Enabled = False
            Case 2
                lblPrimaryInfo.Font = BoldFont(lblPrimaryInfo, False)
                lblTypeMoshtary.Font = BoldFont(lblTypeMoshtary, False)
                lblTypeKala.Font = BoldFont(lblTypeKala, True)
                lblTaeedNahaei.Font = BoldFont(lblTaeedNahaei, False)
                lblTypeMoshtary.Enabled = True
                lblTypeKala.Enabled = True
                lblTaeedNahaei.Enabled = False
                grbPrimaryInfo.Visible = False
                grbTypeMoshtary.Visible = False
                grbTypeKala.Visible = True
                grbTaeedNahaei.Visible = False
                btnPrv.Enabled = True
                btnNext.Enabled = True
                btnSave.Enabled = False

                If cmbNoeAeenNameh.SelectedIndex = 1 AndAlso cmbNoeTakhfifEhdaei.SelectedIndex = 2 Then
                    lblEffectOn.Visible = True
                    cmbEffectOn.SelectedIndex = 0
                    cmbEffectOn.Visible = True
                Else
                    lblEffectOn.Visible = False
                    cmbEffectOn.SelectedIndex = 0
                    cmbEffectOn.Visible = False
                End If
                '-----------------------------
                If (cmbDastehBandyMoshtary.SelectedIndex = 1 Or cmbDastehBandyMoshtary.SelectedIndex = 2 Or cmbDastehBandyMoshtary.SelectedIndex = 3 Or cmbDastehBandyMoshtary.SelectedIndex = 4) Then
                    P = -5

                Else
                    P = cmbNoeFieldNotEffectMoshtary.SelectedIndex
                End If
                SearchInMarkazPakhsh(cmbDastehBandyMoshtary.SelectedIndex, P)
                '---------------------------------------
            Case 3
                lblPrimaryInfo.Font = BoldFont(lblPrimaryInfo, False)
                lblTypeMoshtary.Font = BoldFont(lblTypeMoshtary, False)
                lblTypeKala.Font = BoldFont(lblTypeKala, False)
                lblTaeedNahaei.Font = BoldFont(lblTaeedNahaei, True)
                lblTypeMoshtary.Enabled = True
                lblTypeKala.Enabled = True
                lblTaeedNahaei.Enabled = True
                grbPrimaryInfo.Visible = False
                grbTypeMoshtary.Visible = False
                grbTypeKala.Visible = False
                grbTaeedNahaei.Visible = True
                btnPrv.Enabled = True
                btnNext.Enabled = False
                btnSave.Enabled = True

                SetFormTaeedNahaei()


        End Select
    End Sub
    Private Sub ClearForm()
        cmbOstan.SelectedIndex = -1
        cmbOstan.SelectedIndex = -1

        cmbShahr.SelectedIndex = -1
        cmbShahr.SelectedIndex = -1

        cmbMantagheh.SelectedIndex = -1
        cmbMantagheh.SelectedIndex = -1

        cmbMahaleh.SelectedIndex = -1
        cmbMahaleh.SelectedIndex = -1
    End Sub
    Private Sub SetPrimaryInfo()
        If cmbNoeAeenNameh.SelectedIndex = 0 Then
            mskAzTarikh.Enabled = False
            mskAzTarikh.Text = ""
            mskTaTarikh.Enabled = False
            mskTaTarikh.Text = ""

            cmbNoeAzTa.Enabled = False
            cmbNoeAzTa.SelectedIndex = 0

            txtAz.Enabled = False
            txtAz.Text = ""
            txtTa.Enabled = False
            txtTa.Text = ""
            txtBeEzae.Enabled = False
            txtBeEzae.Text = ""

            cmbNoeBastehBandy.Enabled = False
            cmbNoeBastehBandy.SelectedIndex = 0

            cmbNoeTakhfifEhdaei.Enabled = False
            cmbNoeTakhfifEhdaei.SelectedIndex = 0

            txtCodeKalaJayezeh.Enabled = False
            txtCodeKalaJayezeh.Text = ""

            txtTedadJayezeh.Enabled = False
            txtTedadJayezeh.Text = ""

            cmbNoeBastehBandyJayezeh.Enabled = False
            cmbNoeBastehBandyJayezeh.SelectedIndex = 0

            txtDarsadEhdaei.Enabled = False
            txtDarsadEhdaei.Text = ""

            txtRialEhdaei.Enabled = False
            txtRialEhdaei.Text = ""

            chklstNoePardakht.Enabled = False

            For i As Integer = 0 To chklstNoePardakht.Items.Count - 1
                If chklstNoePardakht.GetItemChecked(i) = True Then
                    chklstNoePardakht.SetItemChecked(i, False)
                End If
            Next
        ElseIf cmbNoeAeenNameh.SelectedIndex = 1 Then
            mskAzTarikh.Enabled = True
            mskAzTarikh.Text = ""
            mskTaTarikh.Enabled = True
            mskTaTarikh.Text = ""

            cmbNoeAzTa.Enabled = True
            cmbNoeAzTa.SelectedIndex = 0

            cmbNoeBastehBandy.Enabled = False
            cmbNoeBastehBandy.SelectedIndex = 0

            txtAz.Enabled = False
            txtAz.Text = ""
            txtTa.Enabled = False
            txtTa.Text = ""
            txtBeEzae.Enabled = False
            txtBeEzae.Text = ""

            cmbNoeTakhfifEhdaei.Enabled = True
            cmbNoeTakhfifEhdaei.SelectedIndex = 0

            txtCodeKalaJayezeh.Enabled = False
            txtCodeKalaJayezeh.Text = ""

            txtTedadJayezeh.Enabled = False
            txtTedadJayezeh.Text = ""

            cmbNoeBastehBandyJayezeh.Enabled = False
            cmbNoeBastehBandyJayezeh.SelectedIndex = 0

            txtDarsadEhdaei.Enabled = False
            txtDarsadEhdaei.Text = ""

            txtRialEhdaei.Enabled = False
            txtRialEhdaei.Text = ""

            chklstNoePardakht.Enabled = True

            mskAzTarikh.Focus()
        ElseIf cmbNoeAeenNameh.SelectedIndex = 2 Then
            mskAzTarikh.Enabled = True
            mskAzTarikh.Text = ""
            mskTaTarikh.Enabled = True
            mskTaTarikh.Text = ""

            cmbNoeAzTa.Enabled = True
            cmbNoeAzTa.SelectedIndex = 0

            cmbNoeBastehBandy.Enabled = False
            cmbNoeBastehBandy.SelectedIndex = 0

            txtAz.Enabled = False
            txtAz.Text = ""
            txtTa.Enabled = False
            txtTa.Text = ""
            txtBeEzae.Enabled = False
            txtBeEzae.Text = ""

            cmbNoeTakhfifEhdaei.Enabled = True
            cmbNoeTakhfifEhdaei.SelectedIndex = 0

            txtCodeKalaJayezeh.Enabled = False
            txtCodeKalaJayezeh.Text = ""

            txtTedadJayezeh.Enabled = False
            txtTedadJayezeh.Text = ""

            cmbNoeBastehBandyJayezeh.Enabled = False
            cmbNoeBastehBandyJayezeh.SelectedIndex = 0

            txtDarsadEhdaei.Enabled = False
            txtDarsadEhdaei.Text = ""

            txtRialEhdaei.Enabled = False
            txtRialEhdaei.Text = ""

            chklstNoePardakht.Enabled = True

            For i As Integer = 0 To chklstNoePardakht.Items.Count - 1
                If chklstNoePardakht.GetItemChecked(i) = True Then
                    chklstNoePardakht.SetItemChecked(i, False)
                End If
            Next

            mskAzTarikh.Focus()
        End If
    End Sub
    Private Sub SetFormDastehBandyMoshtarian(ByVal Effect As Integer, ByVal NotEffect As Integer)
        If Effect = 0 Then
            lblNoeFieldNotEffectMoshtary.Visible = False
            cmbNoeFieldNotEffectMoshtary.Visible = False
            cmbNoeFieldNotEffectMoshtary.SelectedIndex = 0
            grbMoshtary.Visible = False
            grbNoeMoshtary.Visible = False
            grbNoeSenf.Visible = False
            grbMantagheh.Visible = False
            grbNotEffectMoshtary.Visible = False
            grbNotEffectNoeMoshtary.Visible = False
            grbNotEffectNoeSenf.Visible = False
            grbNotEffectMantagheh.Visible = False
        ElseIf Effect = 1 Then
            lblNoeFieldNotEffectMoshtary.Visible = False
            cmbNoeFieldNotEffectMoshtary.Visible = False
            cmbNoeFieldNotEffectMoshtary.SelectedIndex = 0
            grbMoshtary.Visible = True
            grbNoeMoshtary.Visible = False
            grbNoeSenf.Visible = False
            grbMantagheh.Visible = False
            grbNotEffectMoshtary.Visible = False
            grbNotEffectNoeMoshtary.Visible = False
            grbNotEffectNoeSenf.Visible = False
            grbNotEffectMantagheh.Visible = False

            lbMoshtary_Valid.Items.Clear()
            txtSearch_Moshtary.Text = ""
            txtSearch_Moshtary_Valid.Text = ""

            SearchInListBoxMoshtary(lbMoshtary, txtSearch_Moshtary.Text.Trim)
            SearchInMarkazPakhsh(Effect, -5)
        ElseIf Effect = 2 Then
            lblNoeFieldNotEffectMoshtary.Visible = False
            cmbNoeFieldNotEffectMoshtary.Visible = False
            cmbNoeFieldNotEffectMoshtary.SelectedIndex = 0
            grbMoshtary.Visible = False
            grbNoeMoshtary.Visible = True
            grbNoeSenf.Visible = False
            grbMantagheh.Visible = False
            grbNotEffectMoshtary.Visible = False
            grbNotEffectNoeMoshtary.Visible = False
            grbNotEffectNoeSenf.Visible = False
            grbNotEffectMantagheh.Visible = False

            SearchInListBoxMoshtary(lbMoshtary_NoeMoshtary, txtSearch_Moshtary_NoeMoshtary.Text.Trim, sNoeMoshtary:=cmbNoeMoshtary.SelectedValue)
            SearchInMarkazPakhsh(Effect, -5)
        ElseIf Effect = 3 Then
            lblNoeFieldNotEffectMoshtary.Visible = False
            cmbNoeFieldNotEffectMoshtary.Visible = False
            cmbNoeFieldNotEffectMoshtary.SelectedIndex = 0
            grbMoshtary.Visible = False
            grbNoeMoshtary.Visible = False
            grbNoeSenf.Visible = True
            grbMantagheh.Visible = False
            grbNotEffectMoshtary.Visible = False
            grbNotEffectNoeMoshtary.Visible = False
            grbNotEffectNoeSenf.Visible = False
            grbNotEffectMantagheh.Visible = False

            SearchInListBoxMoshtary(lbMoshtary_NoeSenf, txtSearch_Moshtary_NoeSenf.Text.Trim, sNoeSenf:=cmbNoeSenf.SelectedValue)
            SearchInMarkazPakhsh(Effect, -5)
        ElseIf Effect = 4 Then
            lblNoeFieldNotEffectMoshtary.Visible = False
            cmbNoeFieldNotEffectMoshtary.Visible = False
            cmbNoeFieldNotEffectMoshtary.SelectedIndex = 0
            grbMoshtary.Visible = False
            grbNoeMoshtary.Visible = False
            grbNoeSenf.Visible = False
            grbMantagheh.Visible = True
            grbNotEffectMoshtary.Visible = False
            grbNotEffectNoeMoshtary.Visible = False
            grbNotEffectNoeSenf.Visible = False
            grbNotEffectMantagheh.Visible = False
            SearchInMarkazPakhsh(Effect, -5)
        ElseIf Effect = 5 Then
            lblNoeFieldNotEffectMoshtary.Visible = True
            cmbNoeFieldNotEffectMoshtary.Visible = True
            grbMoshtary.Visible = False
            grbNoeMoshtary.Visible = False
            grbNoeSenf.Visible = False
            grbMantagheh.Visible = False
            If NotEffect = 0 Then
                grbNotEffectMoshtary.Visible = False
                grbNotEffectNoeMoshtary.Visible = False
                grbNotEffectNoeSenf.Visible = False
                grbNotEffectMantagheh.Visible = False

                SearchInMarkazPakhsh(Effect, NotEffect)
            ElseIf NotEffect = 1 Then
                grbNotEffectMoshtary.Visible = True
                grbNotEffectNoeMoshtary.Visible = False
                grbNotEffectNoeSenf.Visible = False
                grbNotEffectMantagheh.Visible = False

                SearchInListBoxMoshtary(lbMoshtary_Effect, txtSearch_Moshtary_Effect.Text.Trim)
                SearchInMarkazPakhsh(Effect, NotEffect)
            ElseIf NotEffect = 2 Then
                grbNotEffectMoshtary.Visible = False
                grbNotEffectNoeMoshtary.Visible = True
                grbNotEffectNoeSenf.Visible = False
                grbNotEffectMantagheh.Visible = False

                SearchInListBoxNoeMoshtary(lbNoeMoshtary_Effect)
                SearchInMarkazPakhsh(Effect, NotEffect)
            ElseIf NotEffect = 3 Then
                grbNotEffectMoshtary.Visible = False
                grbNotEffectNoeMoshtary.Visible = False
                grbNotEffectNoeSenf.Visible = True
                grbNotEffectMantagheh.Visible = False

                SearchInListBoxNoeSenf(lbNoeSenf_Effect)
                SearchInMarkazPakhsh(Effect, NotEffect)
            ElseIf NotEffect = 4 Then
                grbNotEffectMoshtary.Visible = False
                grbNotEffectNoeMoshtary.Visible = False
                grbNotEffectNoeSenf.Visible = False
                grbNotEffectMantagheh.Visible = True

                SearchInListBoxOstan()
                SearchInMarkazPakhsh(Effect, NotEffect)
            End If
        End If

        ClearFormDastehBandyMoshtarian(ModeDastehBandyMoshtary)
        ModeDastehBandyMoshtary = Effect + NotEffect
    End Sub
    Private Sub ClearFormDastehBandyMoshtarian(ByVal NoeDastehBandy As Integer)
        '' NoeDastehBandy ------>> 0 = None \ 1 = Moshtary \ 2 = NoeMoshtary \ 3 = NoeSenf \ 4 = Manategh \ 5 : Hameh \ 6 = Not Moshtary \ 7 = Not NoeMoshtary \ 8 = Not NoeSenf \ 9 = Not Manategh

        If NoeDastehBandy = 1 Then
            txtSearch_Moshtary.Text = ""
            txtSearch_Moshtary_Valid.Text = ""
            lbMoshtary_Valid.DataSource = Nothing
            lbMoshtary_Valid.Items.Clear()
            strMoshtary_Valid = ","
        End If

        If NoeDastehBandy = 2 Then
            cmbNoeMoshtary.SelectedIndex = 0
            txtSearch_Moshtary_NoeMoshtary_Valid.Text = ""
            lbMoshtary_NoeMoshtary_Valid.DataSource = Nothing
            lbMoshtary_NoeMoshtary_Valid.Items.Clear()
            strMoshtary_NoeMoshtary_Valid = ","
        End If

        If NoeDastehBandy = 3 Then
            cmbNoeSenf.SelectedIndex = 0
            txtSearch_Moshtary_NoeSenf_Valid.Text = ""
            lbMoshtary_NoeSenf_Valid.DataSource = Nothing
            lbMoshtary_NoeSenf_Valid.Items.Clear()
            strMoshtary_NoeSenf_Valid = ","
        End If

        If NoeDastehBandy = 4 Then
            chkShahr.Checked = False
            txtSearch_Moshtary_Mantagheh.Text = ""
            txtSearch_Moshtary_Mantagheh_Valid.Text = ""
            lbMoshtary_Mantagheh.DataSource = Nothing
            lbMoshtary_Mantagheh.Items.Clear()
            strMoshtary_Mantagheh_Valid = ","
        End If

        If NoeDastehBandy = 6 Then
            txtSearch_Moshtary_Effect.Text = ""
            txtSearch_Moshtary_NotEffect.Text = ""
            lbMoshtary_NotEffect.DataSource = Nothing
            lbMoshtary_NotEffect.Items.Clear()
            strMoshtary_NotEffect = ","
        End If

        If NoeDastehBandy = 7 Then
            txtSearch_NoeMoshtary_Effect.Text = ""
            txtSearch_NoeMoshtary_NotEffect.Text = ""
            lbNoeMoshtary_NotEffect.DataSource = Nothing
            lbNoeMoshtary_NotEffect.Items.Clear()
            strNoeMoshtary_NotEffect = ","
        End If

        If NoeDastehBandy = 8 Then
            txtSearch_NoeSenf_Effect.Text = ""
            txtSearch_NoeSenf_NotEffect.Text = ""
            lbNoeSenf_NotEffect.DataSource = Nothing
            lbNoeSenf_NotEffect.Items.Clear()
            strNoeMoshtary_NotEffect = ","
        End If

        If NoeDastehBandy = 9 Then
            chkMahaleh_Effect.Checked = False
            chkMantagheh_Effect.Checked = False
            chkShahr_Effect.Checked = False
            txtSearch_Ostan_Effect.Text = ""
            txtSearch_Manategh_NotEffect.Text = ""
            lbManategh_NotEffect.DataSource = Nothing
            lbManategh_NotEffect.Items.Clear()
            strManategh_NotEffect = ","
        End If
    End Sub
    Private Function BoldFont(ByVal lbl As Label, ByVal Value As Boolean) As Font
        Dim Font As New Font(lbl.Font, IIf(Value = True, FontStyle.Bold, FontStyle.Regular))
        lbl.Font = Font
        BoldFont = lbl.Font
        Return BoldFont
    End Function
    Private Sub btnPrv_Click(sender As Object, e As EventArgs) Handles btnPrv.Click
        Mode = Mode - 1
        SetForm(Mode)
    End Sub
    Private Sub btnNext_Click(sender As Object, e As EventArgs) Handles btnNext.Click
        If Not IsValid(Mode) Then
            Exit Sub
        End If

        Mode = Mode + 1
        SetForm(Mode)
    End Sub
    Private Sub cmbDastehBandyMoshtary_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbDastehBandyMoshtary.SelectedIndexChanged
        SetFormDastehBandyMoshtarian(cmbDastehBandyMoshtary.SelectedIndex, cmbNoeFieldNotEffectMoshtary.SelectedIndex)

    End Sub
    Private Sub cmbNoeFieldNotEffectMoshtary_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbNoeFieldNotEffectMoshtary.SelectedIndexChanged
        'If cmbNoeFieldNotEffectMoshtary.SelectedIndex <> 0 Then
        '    SetFormDastehBandyMoshtarian(cmbDastehBandyMoshtary.SelectedIndex, cmbNoeFieldNotEffectMoshtary.SelectedIndex)
        'Else
        '    grbMoshtary.Visible = False
        '    grbNoeMoshtary.Visible = False
        '    grbNoeSenf.Visible = False
        '    grbMantagheh.Visible = False
        '    grbNotEffectMoshtary.Visible = False
        '    grbNotEffectNoeMoshtary.Visible = False
        '    grbNotEffectNoeSenf.Visible = False
        '    grbNotEffectMantagheh.Visible = False

        '    ModeDastehBandyMoshtary = cmbDastehBandyMoshtary.SelectedIndex
        'End If

        SetFormDastehBandyMoshtarian(cmbDastehBandyMoshtary.SelectedIndex, cmbNoeFieldNotEffectMoshtary.SelectedIndex)

        If cmbNoeFieldNotEffectMoshtary.SelectedIndex = 0 Then


            grbMoshtary.Visible = False
            grbNoeMoshtary.Visible = False
            grbNoeSenf.Visible = False
            grbMantagheh.Visible = False
            grbNotEffectMoshtary.Visible = False
            grbNotEffectNoeMoshtary.Visible = False
            grbNotEffectNoeSenf.Visible = False
            grbNotEffectMantagheh.Visible = False

            ModeDastehBandyMoshtary = cmbDastehBandyMoshtary.SelectedIndex
        End If
    End Sub
    Private Sub cmbNoeAeenNameh_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbNoeAeenNameh.SelectedIndexChanged
        SetPrimaryInfo()
        LoadCombo_NoeEhdaei(cmbNoeAeenNameh.SelectedIndex)
    End Sub
    Private Sub cmbNoeTakhfifEhdaei_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbNoeTakhfifEhdaei.SelectedIndexChanged
        If cmbNoeAeenNameh.SelectedIndex = 1 Then
            If cmbNoeTakhfifEhdaei.SelectedIndex = 1 Then
                MsgBox("در آیین نامه تخفیفات ، نـوع تخفیف / جایـزه نمی تواند جـایـزه کالایی باشد !", MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.OkOnly, "")
                cmbNoeTakhfifEhdaei.SelectedIndex = 0
                Exit Sub
            End If
        End If

        If cmbNoeTakhfifEhdaei.SelectedIndex = 1 Then
            txtBeEzae.Enabled = True
            txtCodeKalaJayezeh.Enabled = True
            txtCodeKalaJayezeh.Text = ""
            txtCodeKalaJayezeh.Tag = 0
            txtTedadJayezeh.Enabled = True
            txtTedadJayezeh.Text = ""
            cmbNoeBastehBandyJayezeh.Enabled = True
            cmbNoeBastehBandyJayezeh.SelectedIndex = 1
            txtDarsadEhdaei.Enabled = False
            txtDarsadEhdaei.Text = ""
            txtRialEhdaei.Enabled = False
            txtRialEhdaei.Text = ""

            txtCodeKalaJayezeh.Focus()
        ElseIf cmbNoeTakhfifEhdaei.SelectedIndex = 2 Then
            txtBeEzae.Enabled = False
            txtBeEzae.Text = ""
            txtCodeKalaJayezeh.Enabled = False
            txtCodeKalaJayezeh.Text = ""
            txtCodeKalaJayezeh.Tag = 0
            txtTedadJayezeh.Enabled = False
            txtTedadJayezeh.Text = ""
            cmbNoeBastehBandyJayezeh.Enabled = False
            cmbNoeBastehBandyJayezeh.SelectedIndex = 0
            txtDarsadEhdaei.Enabled = True
            txtDarsadEhdaei.Text = ""
            txtRialEhdaei.Enabled = False
            txtRialEhdaei.Text = ""

            txtDarsadEhdaei.Focus()
        ElseIf cmbNoeTakhfifEhdaei.SelectedIndex = 3 Then
            txtBeEzae.Enabled = True
            txtCodeKalaJayezeh.Enabled = False
            txtCodeKalaJayezeh.Text = ""
            txtCodeKalaJayezeh.Tag = 0
            txtTedadJayezeh.Enabled = False
            txtTedadJayezeh.Text = ""
            cmbNoeBastehBandyJayezeh.Enabled = False
            cmbNoeBastehBandyJayezeh.SelectedIndex = 0
            txtDarsadEhdaei.Enabled = False
            txtDarsadEhdaei.Text = ""
            txtRialEhdaei.Enabled = True
            txtRialEhdaei.Text = ""

            txtRialEhdaei.Focus()
        End If
    End Sub
    Private Sub cmbNoeAzTa_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbNoeAzTa.SelectedIndexChanged
        If cmbNoeAzTa.SelectedIndex = 0 Then
            cmbNoeBastehBandy.Enabled = False
            cmbNoeBastehBandy.SelectedIndex = 0
            txtAz.Enabled = False
            txtAz.Text = ""
            txtTa.Enabled = False
            txtTa.Text = ""
            txtBeEzae.Enabled = False
            txtBeEzae.Text = ""
            lblType1.Text = ""
            lblType2.Text = ""
            lblType3.Text = ""
        ElseIf cmbNoeAzTa.SelectedIndex = 1 Then
            cmbNoeBastehBandy.Enabled = True
            cmbNoeBastehBandy.SelectedIndex = 0
            txtAz.Enabled = False
            txtAz.Text = ""
            txtTa.Enabled = False
            txtTa.Text = ""
            txtBeEzae.Enabled = False
            txtBeEzae.Text = ""
            lblType1.Text = ""
            lblType2.Text = ""
            lblType3.Text = ""

        ElseIf cmbNoeAzTa.SelectedIndex = 2 Then
            cmbNoeBastehBandy.Enabled = False
            cmbNoeBastehBandy.SelectedIndex = 0
            txtAz.Enabled = True
            txtAz.Text = ""
            txtTa.Enabled = True
            txtTa.Text = ""
            txtBeEzae.Enabled = False
            txtBeEzae.Text = ""
            lblType1.Text = "ریـال"
            lblType2.Text = "ریـال"
            lblType3.Text = ""

        End If
    End Sub
    Private Sub cmbNoeBastehBandy_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbNoeBastehBandy.SelectedIndexChanged
        If cmbNoeBastehBandy.SelectedIndex = 0 Then
            txtAz.Enabled = False
            txtAz.Text = ""
            txtTa.Enabled = False
            txtTa.Text = ""
            txtBeEzae.Enabled = False
            txtBeEzae.Text = ""
            lblType1.Text = ""
            lblType2.Text = ""
            lblType3.Text = ""
        ElseIf cmbNoeBastehBandy.SelectedIndex = 1 Then
            txtAz.Enabled = True
            txtAz.Text = ""
            txtTa.Enabled = True
            txtTa.Text = ""
            If cmbNoeTakhfifEhdaei.SelectedIndex = 2 Then
                txtBeEzae.Enabled = False
                txtBeEzae.Text = 0
            Else
                txtBeEzae.Enabled = True
                txtBeEzae.Text = ""
            End If
            lblType1.Text = "عـــدد"
            lblType2.Text = "عـــدد"
            lblType3.Text = "عـــدد"
        ElseIf cmbNoeBastehBandy.SelectedIndex = 2 Then
            txtAz.Enabled = True
            txtAz.Text = ""
            txtTa.Enabled = True
            txtTa.Text = ""
            If cmbNoeTakhfifEhdaei.SelectedIndex = 2 Then
                txtBeEzae.Enabled = False
                txtBeEzae.Text = 0
            Else
                txtBeEzae.Enabled = True
                txtBeEzae.Text = ""
            End If
            lblType1.Text = "بستــه"
            lblType2.Text = "بستــه"
            lblType3.Text = "بستــه"
        ElseIf cmbNoeBastehBandy.SelectedIndex = 3 Then
            txtAz.Enabled = True
            txtAz.Text = ""
            txtTa.Enabled = True
            txtTa.Text = ""
            If cmbNoeTakhfifEhdaei.SelectedIndex = 2 Then
                txtBeEzae.Enabled = False
                txtBeEzae.Text = 0
            Else
                txtBeEzae.Enabled = True
                txtBeEzae.Text = ""
            End If
            lblType1.Text = "کارتـن"
            lblType2.Text = "کارتـن"
            lblType3.Text = "کارتـن"
        ElseIf cmbNoeBastehBandy.SelectedIndex = 4 Then
            txtAz.Enabled = True
            txtAz.Text = ""
            txtTa.Enabled = True
            txtTa.Text = ""
            If cmbNoeTakhfifEhdaei.SelectedIndex = 2 Then
                txtBeEzae.Enabled = False
                txtBeEzae.Text = 0
            Else
                txtBeEzae.Enabled = True
                txtBeEzae.Text = ""
            End If
            lblType1.Text = "سطــر"
            lblType2.Text = "سطــر"
            lblType3.Text = "سطــر"
        End If
    End Sub
    Private Sub cmbNoeBastehBandyJayezeh_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbNoeBastehBandyJayezeh.SelectedIndexChanged
        If cmbNoeBastehBandyJayezeh.SelectedIndex = 0 Then
            lblType4.Text = ""
        ElseIf cmbNoeBastehBandyJayezeh.SelectedIndex = 1 Then
            lblType4.Text = "عـــدد"
        ElseIf cmbNoeBastehBandyJayezeh.SelectedIndex = 2 Then
            lblType4.Text = "بستــه"
        ElseIf cmbNoeBastehBandyJayezeh.SelectedIndex = 3 Then
            lblType4.Text = "کارتـن"
        End If
    End Sub
    Private Sub LoadComboShahr()
        If Not Flg Then Exit Sub
        If chkShahr.Checked = False Then
            Exit Sub
        End If
        If dsForm.Tables("tbl_Shahr").Rows.Count = 0 Then Exit Sub

        Try
            FlgSearch = True

            Dim dvShahr As New DataView(dsForm.Tables("tbl_Shahr"), "CodeLink = " & IIf(cmbOstan.SelectedIndex = -1, -1, cmbOstan.SelectedValue), "", DataViewRowState.OriginalRows)

            'dvShahr.AddNew.Row("Code") = 0
            'dvShahr.AddNew.Row("Sharh") = "----"

            cmbShahr.DataSource = Nothing
            cmbShahr.Items.Clear()
            cmbShahr.DataSource = dvShahr
            cmbShahr.DisplayMember = "Sharh"
            cmbShahr.ValueMember = "Code"

            FlgSearch = False
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "Error IN DataBase ---->" & "LoadComboShahr")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, "Error IN ---->" & "LoadComboShahr")
        End Try
    End Sub
    Private Sub LoadComboManategh()
        If Not Flg Then Exit Sub
        If chkMantagheh.Checked = False Then
            Exit Sub
        End If
        If dsForm.Tables("tbl_Mantagheh").Rows.Count = 0 Then Exit Sub
        Try
            FlgSearch = True

            Dim dvMantagheh As New DataView(dsForm.Tables("tbl_Mantagheh"), "CodeLink = " & IIf(cmbShahr.SelectedIndex = -1, -1, cmbShahr.SelectedValue), "", DataViewRowState.OriginalRows)

            'dvMantagheh.AddNew.Row("Code") = 0
            'dvMantagheh.AddNew.Row("Sharh") = "----"

            cmbMantagheh.DataSource = Nothing
            cmbMantagheh.Items.Clear()
            cmbMantagheh.DataSource = dvMantagheh
            cmbMantagheh.DisplayMember = "Sharh"
            cmbMantagheh.ValueMember = "Code"

            FlgSearch = False
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "Error IN DataBase ---->" & "LoadComboManategh")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, "Error IN ---->" & "LoadComboManategh")
        End Try
    End Sub
    Private Sub LoadComboMahaleh()
        If Not Flg Then Exit Sub
        If chkMahaleh.Checked = False Then
            Exit Sub
        End If
        If dsForm.Tables("tbl_Mahaleh").Rows.Count = 0 Then Exit Sub
        Try
            FlgSearch = True

            Dim dvMahaleh As New DataView(dsForm.Tables("tbl_Mahaleh"), "CodeLink = " & IIf(cmbMantagheh.SelectedIndex = -1, -1, cmbMantagheh.SelectedValue), "", DataViewRowState.OriginalRows)

            'dvMahaleh.AddNew.Row("Code") = 0
            'dvMahaleh.AddNew.Row("Sharh") = "----"

            cmbMahaleh.DataSource = Nothing
            cmbMahaleh.Items.Clear()
            cmbMahaleh.DataSource = dvMahaleh
            cmbMahaleh.DisplayMember = "Sharh"
            cmbMahaleh.ValueMember = "Code"

            FlgSearch = False
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "Error IN DataBase ---->" & "LoadComboMahaleh")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, "Error IN ---->" & "LoadComboMahaleh")
        End Try
    End Sub
    Private Sub cmbOstan_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbOstan.SelectedIndexChanged
        LoadComboShahr()
        chkMahaleh.Checked = False
        chkMantagheh.Checked = False
        chkShahr.Checked = False

        chkMahaleh.Enabled = False
        chkMantagheh.Enabled = False

        If dsForm.Tables("tbl_Ostan").Rows.Count = 0 Then Exit Sub
        If Not Flg Then Exit Sub
        SearchInListBoxMoshtary(lbMoshtary_Mantagheh, txtSearch_Moshtary_Mantagheh.Text.Trim, TypeManategh:=1, sManategh:=cmbOstan.SelectedValue)

        txtSearch_Moshtary_Mantagheh_Valid.Text = ""
        lbMoshtary_Mantagheh_Valid.DataSource = Nothing
        lbMoshtary_Mantagheh_Valid.Items.Clear()
        strMoshtary_Mantagheh_Valid = ","
    End Sub
    Private Sub cmbShahr_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbShahr.SelectedIndexChanged
        LoadComboManategh()
        chkMahaleh.Checked = False
        chkMantagheh.Checked = False

        chkMahaleh.Enabled = False

        If dsForm.Tables("tbl_Shahr").Rows.Count = 0 Then Exit Sub
        If Not Flg Then Exit Sub
        If FlgSearch = False Then
            SearchInListBoxMoshtary(lbMoshtary_Mantagheh, txtSearch_Moshtary_Mantagheh.Text.Trim, TypeManategh:=2, sManategh:=cmbShahr.SelectedValue)
        End If

        txtSearch_Moshtary_Mantagheh_Valid.Text = ""
        lbMoshtary_Mantagheh_Valid.DataSource = Nothing
        lbMoshtary_Mantagheh_Valid.Items.Clear()
        strMoshtary_Mantagheh_Valid = ","
    End Sub
    Private Sub cmbMantagheh_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbMantagheh.SelectedIndexChanged
        LoadComboMahaleh()
        chkMahaleh.Checked = False

        If dsForm.Tables("tbl_Mantagheh").Rows.Count = 0 Then Exit Sub
        If Not Flg Then Exit Sub
        If FlgSearch = False Then
            SearchInListBoxMoshtary(lbMoshtary_Mantagheh, txtSearch_Moshtary_Mantagheh.Text.Trim, TypeManategh:=3, sManategh:=cmbMantagheh.SelectedValue)
        End If

        txtSearch_Moshtary_Mantagheh_Valid.Text = ""
        lbMoshtary_Mantagheh_Valid.DataSource = Nothing
        lbMoshtary_Mantagheh_Valid.Items.Clear()
        strMoshtary_Mantagheh_Valid = ","
    End Sub
    Private Sub cmbMahaleh_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbMahaleh.SelectedIndexChanged
        If dsForm.Tables("tbl_Mahaleh").Rows.Count = 0 Then Exit Sub
        If Not Flg Then Exit Sub
        If FlgSearch = False Then
            SearchInListBoxMoshtary(lbMoshtary_Mantagheh, txtSearch_Moshtary_Mantagheh.Text.Trim, TypeManategh:=4, sManategh:=cmbMahaleh.SelectedValue)
        End If

        txtSearch_Moshtary_Mantagheh_Valid.Text = ""
        lbMoshtary_Mantagheh_Valid.DataSource = Nothing
        lbMoshtary_Mantagheh_Valid.Items.Clear()
        strMoshtary_Mantagheh_Valid = ","
    End Sub
    Private Sub SearchInListBoxMoshtary(ByVal NameListBoxSearch As ListBox, ByVal strSearch As String, Optional sNoeMoshtary As Integer = 0, Optional sNoeSenf As Integer = 0, Optional TypeManategh As Integer = 0, Optional sManategh As Integer = 0, Optional strSelectedFields As String = "")
        '' TypeManategh ---> 0 : None // 1 : Ostan // 2 : Shahr // 3 : Mantagheh // 4 : Mahaleh

        Dim cnSQL As New SqlConnection
        Dim cmSQL As New SqlCommand
        Dim daSQL As New SqlDataAdapter
        Dim strSQL As String = ""

        If dsForm.Tables.Contains("tblSearch_ListBoxMoshtary") Then
            dsForm.Tables.Remove("tblSearch_ListBoxMoshtary")
        End If

        Try
            strSQL = "Sales.spTakhfifJayezehTarkibi_ListMoshtary "

            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            cmSQL.Parameters.AddWithValue("CodeMahal", CodeMahalFaal)
            cmSQL.Parameters.AddWithValue("strSearch", strSearch)
            cmSQL.Parameters.AddWithValue("sNoeMoshtary", sNoeMoshtary)
            cmSQL.Parameters.AddWithValue("sNoeSenf", sNoeSenf)
            cmSQL.Parameters.AddWithValue("TypeManategh", TypeManategh)
            cmSQL.Parameters.AddWithValue("sManategh", sManategh)
            cmSQL.Parameters.AddWithValue("strSelectedFields", strSelectedFields)

            daSQL = New SqlDataAdapter(cmSQL)
            daSQL.Fill(dsForm, "tblSearch_ListBoxMoshtary")

            With NameListBoxSearch
                .DataSource = Nothing
                .DataSource = dsForm.Tables("tblSearch_ListBoxMoshtary").DefaultView
                .DisplayMember = "NameMoshtary"
                .ValueMember = "ccMoshtary"
            End With

            cnSQL.Close()

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "Error IN DataBase ---->" & "LoadListBoxMoshtary")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, "Error IN ---->" & "LoadListBoxMoshtary")
        End Try
    End Sub
    Private Sub SearchInMarkazPakhsh(ByVal Effect As Integer, ByVal NotEffect As Integer)
        '' TypeManategh ---> 0 : None // 1 : Ostan // 2 : Shahr // 3 : Mantagheh // 4 : Mahaleh

        Dim cnSQL As New SqlConnection
        Dim cmSQL As New SqlCommand
        Dim daSQL As New SqlDataAdapter
        Dim strSQL As String = ""

        Dim CodeMahalAsly = objTools.ConvertNulls(objTools.DLookup("CodeMahal", "tblGL_MarkazPakhsh", " CodeMahal<>0 and Faal = 1 "), 0)

        If dsForm.Tables.Contains("MarkazPakhsh") Then
            dsForm.Tables.Remove("MarkazPakhsh")
        End If

        Try

            strSQL = "select NameMahal,CodeMahal from dbo.tblGL_MarkazPakhsh where CodeMahal <> 0 "
            If NotEffect = -5 Then

                If Effect = 1 Then
                    strSQL &= " AND CodeMahal = " & CodeMahalFaal
                ElseIf Effect = 2 Then
                    If CodeMahalAsly <> CodeMahalFaal Then
                        strSQL &= " AND CodeMahal = " & CodeMahalFaal
                    Else
                        If strMoshtary_NoeMoshtary_Valid <> "," Then
                            strSQL &= " AND CodeMahal = " & CodeMahalFaal
                        End If
                    End If

                ElseIf Effect = 3 Then
                    If CodeMahalAsly <> CodeMahalFaal Then
                        strSQL &= " AND CodeMahal = " & CodeMahalFaal
                    Else
                        If strMoshtary_NoeSenf_Valid <> "," Then
                            strSQL &= " AND CodeMahal = " & CodeMahalFaal
                        End If
                    End If
                ElseIf Effect = 4 Then
                    strSQL &= " AND CodeMahal = " & CodeMahalFaal
                End If

            Else
              
                If CodeMahalAsly <> CodeMahalFaal Then
                    strSQL &= " AND CodeMahal = " & CodeMahalFaal
                End If
            End If
                daSQL = New SqlDataAdapter(strSQL, ConnectionString)
                daSQL.Fill(dsForm, "MarkazPakhsh")
                chlMarkazPakhsh.DataSource = Nothing
                chlMarkazPakhsh.Items.Clear()
                chlMarkazPakhsh.DataSource = dsForm.Tables("MarkazPakhsh").DefaultView
                chlMarkazPakhsh.DisplayMember = "NameMahal"
                chlMarkazPakhsh.ValueMember = "CodeMahal"

                cnSQL.Close()

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "Error IN DataBase ---->" & "LoadListBoxMoshtary")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, "Error IN ---->" & "LoadListBoxMoshtary")
        End Try
    End Sub
    Private Sub txtSearch_Moshtary_TextChanged(sender As Object, e As EventArgs) Handles txtSearch_Moshtary.TextChanged
        SearchInListBoxMoshtary(lbMoshtary, txtSearch_Moshtary.Text.Trim)
    End Sub
    Private Sub cmbNoeMoshtary_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbNoeMoshtary.SelectedIndexChanged
        If Flg = False Then
            Exit Sub
        End If

        txtSearch_Moshtary_NoeMoshtary.Text = ""
        SearchInListBoxMoshtary(lbMoshtary_NoeMoshtary, txtSearch_Moshtary_NoeMoshtary.Text.Trim, sNoeMoshtary:=cmbNoeMoshtary.SelectedValue)

        txtSearch_Moshtary_NoeMoshtary_Valid.Text = ""
        lbMoshtary_NoeMoshtary_Valid.DataSource = Nothing
        lbMoshtary_NoeMoshtary_Valid.Items.Clear()
        strMoshtary_NoeMoshtary_Valid = ","
    End Sub
    Private Sub txtSearch_Moshtary_NoeMoshtary_TextChanged(sender As Object, e As EventArgs) Handles txtSearch_Moshtary_NoeMoshtary.TextChanged
        SearchInListBoxMoshtary(lbMoshtary_NoeMoshtary, txtSearch_Moshtary_NoeMoshtary.Text.Trim, sNoeMoshtary:=cmbNoeMoshtary.SelectedValue)
    End Sub
    Private Sub cmbNoeSenf_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbNoeSenf.SelectedIndexChanged
        If Flg = False Then
            Exit Sub
        End If

        txtSearch_Moshtary_NoeSenf.Text = ""
        SearchInListBoxMoshtary(lbMoshtary_NoeSenf, txtSearch_Moshtary_NoeSenf.Text.Trim, sNoeSenf:=cmbNoeSenf.SelectedValue)

        txtSearch_Moshtary_NoeSenf_Valid.Text = ""
        lbMoshtary_NoeSenf_Valid.DataSource = Nothing
        lbMoshtary_NoeSenf_Valid.Items.Clear()
        strMoshtary_NoeSenf_Valid = ","
    End Sub
    Private Sub txtSearch_Moshtary_NoeSenf_TextChanged(sender As Object, e As EventArgs) Handles txtSearch_Moshtary_NoeSenf.TextChanged
        SearchInListBoxMoshtary(lbMoshtary_NoeSenf, txtSearch_Moshtary_NoeSenf.Text.Trim, sNoeSenf:=cmbNoeSenf.SelectedValue)
    End Sub
    Private Sub txtSearch_Moshtary_Effect_TextChanged(sender As Object, e As EventArgs) Handles txtSearch_Moshtary_Effect.TextChanged
        SearchInListBoxMoshtary(lbMoshtary_Effect, txtSearch_Moshtary_Effect.Text.Trim)
    End Sub
    Private Sub SearchInListBoxNoeMoshtary(ByVal NameListBoxSearch As ListBox, Optional strSearch As String = "", Optional strSelectedFields As String = "")
        Dim cnSQL As New SqlConnection
        Dim cmSQL As New SqlCommand
        Dim daSQL As New SqlDataAdapter
        Dim strSQL As String = ""

        If dsForm.Tables.Contains("tblSearch_NoeMoshtary") Then
            dsForm.Tables.Remove("tblSearch_NoeMoshtary")
        End If

        strSQL = "SELECT Code, Sharh FROM tblGL_ShenasehOmomi WHERE CodeAsli = 152 AND CodeFarei <> 0 "
        If strSearch <> "" Then
            strSQL &= " AND Sharh LIKE N'%" & strSearch & "%' "
        End If
        If strSelectedFields <> "" Then
            strSQL &= " AND '" & strSelectedFields & "' LIKE N'%,' + LTRIM(RTRIM(STR(Code))) + ',%' "
        End If
        strSQL &= " ORDER BY Sharh"
        daSQL = New SqlDataAdapter(strSQL, ConnectionString)
        daSQL.Fill(dsForm, "tblSearch_NoeMoshtary")

        NameListBoxSearch.DataSource = Nothing
        NameListBoxSearch.Items.Clear()
        NameListBoxSearch.DataSource = dsForm.Tables("tblSearch_NoeMoshtary").DefaultView
        NameListBoxSearch.DisplayMember = "Sharh"
        NameListBoxSearch.ValueMember = "Code"
    End Sub
    Private Sub txtSearch_NoeMoshtary_Effect_TextChanged(sender As Object, e As EventArgs) Handles txtSearch_NoeMoshtary_Effect.TextChanged
        SearchInListBoxNoeMoshtary(lbNoeMoshtary_Effect, txtSearch_NoeMoshtary_Effect.Text.Trim)
    End Sub
    Private Sub SearchInListBoxNoeSenf(ByVal NameListBoxSearch As ListBox, Optional strSearch As String = "", Optional strSelectedFields As String = "")
        Dim cnSQL As New SqlConnection
        Dim cmSQL As New SqlCommand
        Dim daSQL As New SqlDataAdapter
        Dim strSQL As String = ""

        If dsForm.Tables.Contains("tblSearch_NoeSenf") Then
            dsForm.Tables.Remove("tblSearch_NoeSenf")
        End If

        strSQL = "SELECT Code, Sharh FROM tblGL_ShenasehOmomi WHERE CodeAsli = 150 AND CodeFarei <> 0 "
        If strSearch <> "" Then
            strSQL &= " AND Sharh LIKE N'%" & strSearch & "%' "
        End If
        If strSelectedFields <> "" Then
            strSQL &= " AND '" & strSelectedFields & "' LIKE N'%,' + LTRIM(RTRIM(STR(Code))) + ',%' "
        End If
        strSQL &= " order by Sharh"
        daSQL = New SqlDataAdapter(strSQL, ConnectionString)
        daSQL.Fill(dsForm, "tblSearch_NoeSenf")

        NameListBoxSearch.DataSource = Nothing
        NameListBoxSearch.Items.Clear()
        NameListBoxSearch.DataSource = dsForm.Tables("tblSearch_NoeSenf").DefaultView
        NameListBoxSearch.DisplayMember = "Sharh"
        NameListBoxSearch.ValueMember = "Code"
    End Sub
    Private Sub txtSearch_NoeSenf_Effect_TextChanged(sender As Object, e As EventArgs) Handles txtSearch_NoeSenf_Effect.TextChanged
        SearchInListBoxNoeSenf(lbNoeSenf_Effect, txtSearch_NoeSenf_Effect.Text.Trim)
    End Sub
    Private Sub SearchInListBoxOstan(Optional strSearch As String = "")
        FlgSearch = True

        Dim cnSQL As New SqlConnection
        Dim cmSQL As New SqlCommand
        Dim daSQL As New SqlDataAdapter
        Dim strSQL As String = ""

        If dsForm.Tables.Contains("tblSearch_Ostan") Then
            dsForm.Tables.Remove("tblSearch_Ostan")
        End If

        strSQL = "SELECT Code, Sharh FROM tblGL_ShenasehOmomi WHERE CodeAsli = 47 AND CodeFarei <> 0 AND Code <> 0"
        If strSearch <> "" Then
            strSQL &= " AND Sharh LIKE N'%" & strSearch & "%' "
        End If
        strSQL &= " order by Sharh"
        daSQL = New SqlDataAdapter(strSQL, ConnectionString)
        daSQL.Fill(dsForm, "tblSearch_Ostan")

        lbOstan_Effect.DataSource = Nothing
        lbOstan_Effect.Items.Clear()
        lbOstan_Effect.DataSource = dsForm.Tables("tblSearch_Ostan").DefaultView
        lbOstan_Effect.DisplayMember = "Sharh"
        lbOstan_Effect.ValueMember = "Code"

        FlgSearch = False
    End Sub
    Private Sub SearchInListBoxShahr(Optional sOstan As Integer = 0, Optional strSearch As String = "")
        Dim cnSQL As New SqlConnection
        Dim cmSQL As New SqlCommand
        Dim daSQL As New SqlDataAdapter
        Dim strSQL As String = ""

        If dsForm.Tables.Contains("tblSearch_Shahr") Then
            dsForm.Tables.Remove("tblSearch_Shahr")
        End If

        strSQL = "SELECT Code, Sharh FROM tblGL_ShenasehOmomi WHERE CodeAsli = 23 AND CodeFarei <> 0 AND Code <> 0"
        strSQL &= " AND CodeLink = " & sOstan
        If strSearch <> "" Then
            strSQL &= " AND Sharh LIKE N'%" & strSearch & "%' "
        End If
        strSQL &= " order by Sharh"
        daSQL = New SqlDataAdapter(strSQL, ConnectionString)
        daSQL.Fill(dsForm, "tblSearch_Shahr")

        lbShahr_Effect.DataSource = Nothing
        lbShahr_Effect.Items.Clear()
        lbShahr_Effect.DataSource = dsForm.Tables("tblSearch_Shahr").DefaultView
        lbShahr_Effect.DisplayMember = "Sharh"
        lbShahr_Effect.ValueMember = "Code"
    End Sub
    Private Sub SearchInListBoxMantagheh(Optional sShahr As Integer = 0, Optional strSearch As String = "")
        Dim cnSQL As New SqlConnection
        Dim cmSQL As New SqlCommand
        Dim daSQL As New SqlDataAdapter
        Dim strSQL As String = ""

        If dsForm.Tables.Contains("tblSearch_Mantagheh") Then
            dsForm.Tables.Remove("tblSearch_Mantagheh")
        End If

        strSQL = "SELECT Code, Sharh FROM tblGL_ShenasehOmomi WHERE CodeAsli = 21 AND CodeFarei <> 0 AND Code <> 0"
        strSQL &= " AND CodeLink = " & sShahr
        If strSearch <> "" Then
            strSQL &= " AND Sharh LIKE N'%" & strSearch & "%' "
        End If
        strSQL &= " order by Sharh"
        daSQL = New SqlDataAdapter(strSQL, ConnectionString)
        daSQL.Fill(dsForm, "tblSearch_Mantagheh")

        lbMantagheh_Effect.DataSource = Nothing
        lbMantagheh_Effect.Items.Clear()
        lbMantagheh_Effect.DataSource = dsForm.Tables("tblSearch_Mantagheh").DefaultView
        lbMantagheh_Effect.DisplayMember = "Sharh"
        lbMantagheh_Effect.ValueMember = "Code"
    End Sub
    Private Sub SearchInListBoxMahaleh(Optional sMantagheh As Integer = 0, Optional strSearch As String = "")
        Dim cnSQL As New SqlConnection
        Dim cmSQL As New SqlCommand
        Dim daSQL As New SqlDataAdapter
        Dim strSQL As String = ""

        If dsForm.Tables.Contains("tblSearch_Mahaleh") Then
            dsForm.Tables.Remove("tblSearch_Mahaleh")
        End If

        strSQL = "SELECT Code, Sharh FROM tblGL_ShenasehOmomi WHERE CodeAsli = 1 AND CodeFarei <> 0 AND Code <> 0"
        strSQL &= " AND CodeLink = " & sMantagheh
        If strSearch <> "" Then
            strSQL &= " AND Sharh LIKE N'%" & strSearch & "%' "
        End If
        strSQL &= " order by Sharh"
        daSQL = New SqlDataAdapter(strSQL, ConnectionString)
        daSQL.Fill(dsForm, "tblSearch_Mahaleh")

        lbMahaleh_Effect.DataSource = Nothing
        lbMahaleh_Effect.Items.Clear()
        lbMahaleh_Effect.DataSource = dsForm.Tables("tblSearch_Mahaleh").DefaultView
        lbMahaleh_Effect.DisplayMember = "Sharh"
        lbMahaleh_Effect.ValueMember = "Code"
    End Sub
    Private Sub SearchInListBoxManategh_NotEffect(ByVal strSelectedFields As String, Optional strSearch As String = "")
        Dim cnSQL As New SqlConnection
        Dim cmSQL As New SqlCommand
        Dim daSQL As New SqlDataAdapter
        Dim strSQL As String = ""

        If dsForm.Tables.Contains("tblSearch_Manategh_NotEffect") Then
            dsForm.Tables.Remove("tblSearch_Manategh_NotEffect")
        End If

        strSQL = "SELECT Code, Sharh FROM tblGL_ShenasehOmomi WHERE 1 = 1"
        If strSearch <> "" Then
            strSQL &= " AND Sharh LIKE N'%" & strSearch & "%' "
        End If
        If strSelectedFields <> "," Then
            strSQL &= " AND '" & strSelectedFields & "' LIKE N'%,' + LTRIM(RTRIM(STR(Code))) + ',%' "
        Else
            strSQL &= " AND 1 = -1 "
        End If
        strSQL &= " order by Sharh"
        daSQL = New SqlDataAdapter(strSQL, ConnectionString)
        daSQL.Fill(dsForm, "tblSearch_Manategh_NotEffect")

        lbManategh_NotEffect.DataSource = Nothing
        lbManategh_NotEffect.Items.Clear()
        lbManategh_NotEffect.DataSource = dsForm.Tables("tblSearch_Manategh_NotEffect").DefaultView
        lbManategh_NotEffect.DisplayMember = "Sharh"
        lbManategh_NotEffect.ValueMember = "Code"
    End Sub
    Private Sub chkShahr_CheckedChanged(sender As Object, e As EventArgs) Handles chkShahr.CheckedChanged
        If cmbOstan.SelectedIndex = -1 Or cmbOstan.SelectedValue = 0 Then Exit Sub
        If chkShahr.Checked = False Then
            cmbShahr.DataSource = Nothing
            cmbShahr.Items.Clear()
            chkMantagheh.Enabled = False
        Else
            LoadComboShahr()
            txtSearch_Moshtary_Mantagheh.Text = ""
            SearchInListBoxMoshtary(lbMoshtary_Mantagheh, txtSearch_Moshtary_Mantagheh.Text.Trim, TypeManategh:=2, sManategh:=cmbShahr.SelectedValue)
            chkMantagheh.Enabled = True
        End If

        txtSearch_Moshtary_Mantagheh_Valid.Text = ""
        lbMoshtary_Mantagheh_Valid.DataSource = Nothing
        lbMoshtary_Mantagheh_Valid.Items.Clear()
        strMoshtary_Mantagheh_Valid = ","
    End Sub
    Private Sub chkMantagheh_CheckedChanged(sender As Object, e As EventArgs) Handles chkMantagheh.CheckedChanged
        If cmbShahr.SelectedIndex = -1 Or cmbShahr.SelectedValue = 0 Then Exit Sub
        If chkMantagheh.Checked = False Then
            cmbMantagheh.DataSource = Nothing
            cmbMantagheh.Items.Clear()
            chkMahaleh.Enabled = False
        Else
            LoadComboManategh()
            txtSearch_Moshtary_Mantagheh.Text = ""
            SearchInListBoxMoshtary(lbMoshtary_Mantagheh, txtSearch_Moshtary_Mantagheh.Text.Trim, TypeManategh:=3, sManategh:=cmbMantagheh.SelectedValue)
            chkMahaleh.Enabled = True
        End If

        txtSearch_Moshtary_Mantagheh_Valid.Text = ""
        lbMoshtary_Mantagheh_Valid.DataSource = Nothing
        lbMoshtary_Mantagheh_Valid.Items.Clear()
        strMoshtary_Mantagheh_Valid = ","
    End Sub
    Private Sub chkMahaleh_CheckedChanged(sender As Object, e As EventArgs) Handles chkMahaleh.CheckedChanged
        If cmbMantagheh.SelectedIndex = -1 Or cmbMantagheh.SelectedValue = 0 Then Exit Sub
        If chkMahaleh.Checked = False Then
            cmbMahaleh.DataSource = Nothing
            cmbMahaleh.Items.Clear()
        Else
            LoadComboMahaleh()
            txtSearch_Moshtary_Mantagheh.Text = ""
            SearchInListBoxMoshtary(lbMoshtary_Mantagheh, txtSearch_Moshtary_Mantagheh.Text.Trim, TypeManategh:=4, sManategh:=cmbMahaleh.SelectedValue)
        End If

        txtSearch_Moshtary_Mantagheh_Valid.Text = ""
        lbMoshtary_Mantagheh_Valid.DataSource = Nothing
        lbMoshtary_Mantagheh_Valid.Items.Clear()
        strMoshtary_Mantagheh_Valid = ","
    End Sub
    Private Sub txtSearch_Moshtary_Mantagheh_TextChanged(sender As Object, e As EventArgs) Handles txtSearch_Moshtary_Mantagheh.TextChanged
        If chkMahaleh.Checked = True Then
            If dsForm.Tables("tbl_Mahaleh").Rows.Count = 0 Then
                SearchInListBoxMoshtary(lbMoshtary_Mantagheh, txtSearch_Moshtary_Mantagheh.Text.Trim, TypeManategh:=3, sManategh:=cmbMantagheh.SelectedValue)
            Else
                SearchInListBoxMoshtary(lbMoshtary_Mantagheh, txtSearch_Moshtary_Mantagheh.Text.Trim, TypeManategh:=4, sManategh:=cmbMahaleh.SelectedValue)
            End If
        ElseIf chkMantagheh.Checked = True Then
            If dsForm.Tables("tbl_Mantagheh").Rows.Count = 0 Then
                SearchInListBoxMoshtary(lbMoshtary_Mantagheh, txtSearch_Moshtary_Mantagheh.Text.Trim, TypeManategh:=2, sManategh:=cmbShahr.SelectedValue)
            Else
                SearchInListBoxMoshtary(lbMoshtary_Mantagheh, txtSearch_Moshtary_Mantagheh.Text.Trim, TypeManategh:=3, sManategh:=cmbMantagheh.SelectedValue)
            End If
        ElseIf chkShahr.Checked = True Then
            If dsForm.Tables("tbl_Shahr").Rows.Count = 0 Then
                SearchInListBoxMoshtary(lbMoshtary_Mantagheh, txtSearch_Moshtary_Mantagheh.Text.Trim, TypeManategh:=1, sManategh:=cmbOstan.SelectedValue)
            Else
                SearchInListBoxMoshtary(lbMoshtary_Mantagheh, txtSearch_Moshtary_Mantagheh.Text.Trim, TypeManategh:=2, sManategh:=cmbShahr.SelectedValue)
            End If
        Else
            SearchInListBoxMoshtary(lbMoshtary_Mantagheh, txtSearch_Moshtary_Mantagheh.Text.Trim, TypeManategh:=1, sManategh:=cmbOstan.SelectedValue)
        End If
    End Sub
    Private Sub lbOstan_Effect_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lbOstan_Effect.SelectedIndexChanged
        If FlgSearch = True Then
            Exit Sub
        End If

        If chkShahr_Effect.Checked = False Then
            Exit Sub
        End If

        strManategh_NotEffect = ","
        txtSearch_Manategh_NotEffect.Text = ""
        lbManategh_NotEffect.DataSource = Nothing
        lbManategh_NotEffect.Items.Clear()

        txtSearch_Shahr_Effect.Text = ""
        SearchInListBoxShahr(sOstan:=lbOstan_Effect.SelectedValue)
    End Sub
    Private Sub chkShahr_Effect_CheckedChanged(sender As Object, e As EventArgs) Handles chkShahr_Effect.CheckedChanged
        If chkShahr_Effect.Checked = False Then
            txtSearch_Shahr_Effect.Text = ""
            chkMantagheh_Effect.CheckState = False
            chkMahaleh_Effect.CheckState = False
            lbShahr_Effect.DataSource = Nothing
            lbShahr_Effect.Items.Clear()
        Else
            txtSearch_Shahr_Effect.Text = ""
            SearchInListBoxShahr(sOstan:=lbOstan_Effect.SelectedValue)
        End If

        strManategh_NotEffect = ","
        txtSearch_Manategh_NotEffect.Text = ""
        lbManategh_NotEffect.DataSource = Nothing
        lbManategh_NotEffect.Items.Clear()
    End Sub
    Private Sub lbShahr_Effect_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lbShahr_Effect.SelectedIndexChanged
        If FlgSearch = True Then
            Exit Sub
        End If

        If chkMantagheh_Effect.Checked = False Then
            Exit Sub
        End If

        strManategh_NotEffect = ","
        txtSearch_Manategh_NotEffect.Text = ""
        lbManategh_NotEffect.DataSource = Nothing
        lbManategh_NotEffect.Items.Clear()

        txtSearch_Mantagheh_Effect.Text = ""
        SearchInListBoxMantagheh(sShahr:=lbShahr_Effect.SelectedValue)
    End Sub
    Private Sub chkMantagheh_Effect_CheckedChanged(sender As Object, e As EventArgs) Handles chkMantagheh_Effect.CheckedChanged
        If chkMantagheh_Effect.Checked = False Then
            txtSearch_Mantagheh_Effect.Text = ""
            chkMahaleh_Effect.Checked = False
            lbMantagheh_Effect.DataSource = Nothing
            lbMantagheh_Effect.Items.Clear()
        Else
            txtSearch_Mantagheh_Effect.Text = ""
            SearchInListBoxMantagheh(sShahr:=lbShahr_Effect.SelectedValue)
        End If

        strManategh_NotEffect = ","
        txtSearch_Manategh_NotEffect.Text = ""
        lbManategh_NotEffect.DataSource = Nothing
        lbManategh_NotEffect.Items.Clear()
    End Sub
    Private Sub lbMantagheh_Effect_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lbMantagheh_Effect.SelectedIndexChanged
        If FlgSearch = True Then
            Exit Sub
        End If

        If chkMahaleh_Effect.Checked = False Then
            Exit Sub
        End If

        strManategh_NotEffect = ","
        txtSearch_Manategh_NotEffect.Text = ""
        lbManategh_NotEffect.DataSource = Nothing
        lbManategh_NotEffect.Items.Clear()

        txtSearch_Mahaleh_Effect.Text = ""
        SearchInListBoxMahaleh(sMantagheh:=lbMantagheh_Effect.SelectedValue)
    End Sub
    Private Sub chkMahaleh_Effect_CheckedChanged(sender As Object, e As EventArgs) Handles chkMahaleh_Effect.CheckedChanged
        If chkMahaleh_Effect.Checked = False Then
            txtSearch_Mahaleh_Effect.Text = ""
            lbMahaleh_Effect.DataSource = Nothing
            lbMahaleh_Effect.Items.Clear()
        Else
            txtSearch_Mahaleh_Effect.Text = ""
            SearchInListBoxMahaleh(sMantagheh:=lbMantagheh_Effect.SelectedValue)
        End If

        strManategh_NotEffect = ","
        txtSearch_Manategh_NotEffect.Text = ""
        lbManategh_NotEffect.DataSource = Nothing
        lbManategh_NotEffect.Items.Clear()
    End Sub
    Private Sub txtSearch_Ostan_Effect_TextChanged(sender As Object, e As EventArgs) Handles txtSearch_Ostan_Effect.TextChanged
        SearchInListBoxOstan(txtSearch_Ostan_Effect.Text.Trim)
    End Sub
    Private Sub txtSearch_Shahr_Effect_TextChanged(sender As Object, e As EventArgs) Handles txtSearch_Shahr_Effect.TextChanged
        If chkShahr_Effect.Checked Then
            SearchInListBoxShahr(lbOstan_Effect.SelectedValue, txtSearch_Shahr_Effect.Text.Trim)
        End If
    End Sub
    Private Sub txtSearch_Mantagheh_Effect_TextChanged(sender As Object, e As EventArgs) Handles txtSearch_Mantagheh_Effect.TextChanged
        If chkMantagheh_Effect.Checked Then
            SearchInListBoxMantagheh(lbShahr_Effect.SelectedValue, txtSearch_Mantagheh_Effect.Text.Trim)
        End If
    End Sub
    Private Sub txtSearch_Mahaleh_Effect_TextChanged(sender As Object, e As EventArgs) Handles txtSearch_Mahaleh_Effect.TextChanged
        If chkMahaleh_Effect.Checked Then
            SearchInListBoxMahaleh(lbMantagheh_Effect.SelectedValue, txtSearch_Mahaleh_Effect.Text.Trim)
        End If
    End Sub
    Private Sub SetFormTypeKala(ByVal Type As Integer)
        'If Type = 0 Then
        lblNoeMohasebeh.Visible = False
        cmbNoeMohasebeh.Visible = False
        cmbNoeMohasebeh.SelectedIndex = 0
        lblEffectOn.Visible = False
        cmbEffectOn.Visible = False
        cmbEffectOn.SelectedIndex = 0
        grbKala.Visible = False
        grbBrand.Visible = False
        grbGorohKala.Visible = False

        strKala_Effect = ","
        strKala_Brand_Effect = ","
        strKala_GorohKala_Effect = ","

        If Type = 1 Then
            lblNoeMohasebeh.Visible = True
            cmbNoeMohasebeh.Visible = True
            cmbNoeMohasebeh.SelectedIndex = 0
            If cmbNoeAeenNameh.SelectedIndex = 1 AndAlso cmbNoeTakhfifEhdaei.SelectedIndex = 2 Then
                lblEffectOn.Visible = True
                cmbEffectOn.Visible = True
                cmbEffectOn.SelectedIndex = 0
            End If
            grbKala.Visible = True
            grbBrand.Visible = False
            grbGorohKala.Visible = False

            SearchInListBoxKala(lbKala, txtSearch_Kala.Text.Trim, ccBrand:=cmbBrandKala.SelectedValue)
        ElseIf Type = 2 Then
            lblNoeMohasebeh.Visible = False
            cmbNoeMohasebeh.Visible = False
            cmbNoeMohasebeh.SelectedIndex = 0
            If cmbNoeAeenNameh.SelectedIndex = 1 AndAlso cmbNoeTakhfifEhdaei.SelectedIndex = 2 Then
                lblEffectOn.Visible = True
                cmbEffectOn.Visible = True
                cmbEffectOn.SelectedIndex = 0
            End If
            grbKala.Visible = False
            grbBrand.Visible = True
            grbGorohKala.Visible = False

            SearchInListBoxKala(lbKala_Brand, txtSearch_Kala_brand.Text.Trim, ccBrand:=cmbBrand.SelectedValue)
        ElseIf Type = 3 Then
            lblNoeMohasebeh.Visible = False
            cmbNoeMohasebeh.Visible = False
            cmbNoeMohasebeh.SelectedIndex = 0
            If cmbNoeAeenNameh.SelectedIndex = 1 AndAlso cmbNoeTakhfifEhdaei.SelectedIndex = 2 Then
                lblEffectOn.Visible = True
                cmbEffectOn.Visible = True
                cmbEffectOn.SelectedIndex = 0
            End If
            grbKala.Visible = False
            grbBrand.Visible = False
            grbGorohKala.Visible = True

            SearchInListBoxKala(lbKala_Brand, txtSearch_Kala_GorohKala.Text.Trim, LevelGorohKala:=1, CodeGorohKala:=cmbG1.SelectedValue)
        ElseIf Type = 4 Then
            lblNoeMohasebeh.Visible = False
            cmbNoeMohasebeh.Visible = False
            cmbNoeMohasebeh.SelectedIndex = 0
            If cmbNoeAeenNameh.SelectedIndex = 1 AndAlso cmbNoeTakhfifEhdaei.SelectedIndex = 2 Then
                lblEffectOn.Visible = False
                cmbEffectOn.Visible = False
                cmbEffectOn.SelectedIndex = 0
            End If
            grbKala.Visible = False
            grbBrand.Visible = False
            grbGorohKala.Visible = False

        ElseIf Type = 5 Then
            lblNoeMohasebeh.Visible = False
            cmbNoeMohasebeh.Visible = False
            cmbNoeMohasebeh.SelectedIndex = 0
            If cmbNoeAeenNameh.SelectedIndex = 1 AndAlso cmbNoeTakhfifEhdaei.SelectedIndex = 2 Then
                lblEffectOn.Visible = False
                cmbEffectOn.Visible = False
                cmbEffectOn.SelectedIndex = 0
            End If
            grbKala.Visible = False
            grbBrand.Visible = False
            grbGorohKala.Visible = False
        End If
    End Sub
    Private Sub cmbNoeFieldJayezeh_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbNoeFieldJayezeh.SelectedIndexChanged
        If cmbNoeBastehBandy.SelectedIndex = 4 AndAlso cmbNoeFieldJayezeh.SelectedIndex = 1 Then
            MsgBox("برای نوع بسته بندی « سطر فاکتور » آیین نامه برای « کالای خاص » نمی توان تعریف نمود .", MsgBoxStyle.Information + MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, "")
            cmbNoeFieldJayezeh.SelectedIndex = 0
        End If

        SetFormTypeKala(cmbNoeFieldJayezeh.SelectedIndex)
    End Sub
    Private Sub SearchInListBoxKala(ByVal NameListBoxSearch As ListBox, ByVal strSearch As String, Optional ccBrand As Integer = 0, Optional LevelGorohKala As Integer = 0, Optional CodeGorohKala As Integer = 0, Optional strSelectedFields As String = "")
        '' TypeManategh ---> 0 : None // 1 : Goroh1Kala // 2 : Goroh2Kala // 3 : Goroh3Kala // 4 : Goroh4Kala // 5 : Goroh5Kala

        Dim cnSQL As New SqlConnection
        Dim cmSQL As New SqlCommand
        Dim daSQL As New SqlDataAdapter
        Dim strSQL As String = ""

        If dsForm.Tables.Contains("tblSearch_ListBoxKala") Then
            dsForm.Tables.Remove("tblSearch_ListBoxKala")
        End If

        If LevelGorohKala = 2 AndAlso CodeGorohKala = 0 Then
            LevelGorohKala = 1
            CodeGorohKala = cmbG1.SelectedValue
        ElseIf LevelGorohKala = 3 AndAlso CodeGorohKala = 0 Then
            LevelGorohKala = 2
            CodeGorohKala = cmbG2.SelectedValue
        ElseIf LevelGorohKala = 4 AndAlso CodeGorohKala = 0 Then
            LevelGorohKala = 3
            CodeGorohKala = cmbG3.SelectedValue
        ElseIf LevelGorohKala = 5 AndAlso CodeGorohKala = 0 Then
            LevelGorohKala = 4
            CodeGorohKala = cmbG4.SelectedValue
        End If

        Try
            strSQL = "Sales.spTakhfifJayezehTarkibi_ListKala "

            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            cmSQL.Parameters.AddWithValue("strSearch", strSearch)
            cmSQL.Parameters.AddWithValue("ccBrand", ccBrand)
            cmSQL.Parameters.AddWithValue("LevelGorohKala", LevelGorohKala)
            cmSQL.Parameters.AddWithValue("CodeGorohKala", CodeGorohKala)
            cmSQL.Parameters.AddWithValue("strSelectedFields", strSelectedFields)

            daSQL = New SqlDataAdapter(cmSQL)
            daSQL.Fill(dsForm, "tblSearch_ListBoxKala")

            With NameListBoxSearch
                .DataSource = Nothing
                .DataSource = dsForm.Tables("tblSearch_ListBoxKala").DefaultView
                .DisplayMember = "NameKala"
                .ValueMember = "ccKala"
            End With

            cnSQL.Close()

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "Error IN DataBase ----> SearchInListBoxKala")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, "Error IN ----> SearchInListBoxKala")
        End Try
    End Sub

    Private Sub txtSearch_Kala_TextChanged(sender As Object, e As EventArgs) Handles txtSearch_Kala.TextChanged
        SearchInListBoxKala(lbKala, txtSearch_Kala.Text.Trim)
    End Sub
    Private Sub txtSearch_Kala_GorohKala_TextChanged(sender As Object, e As EventArgs) Handles txtSearch_Kala_GorohKala.TextChanged
        If chkG5.Checked = True Then
            If dsForm.Tables("tbl_Goroh5").Rows.Count = 0 Then
                SearchInListBoxKala(lbKala_GorohKala, txtSearch_Kala_GorohKala.Text.Trim, LevelGorohKala:=4, CodeGorohKala:=cmbG4.SelectedValue)
            Else
                SearchInListBoxKala(lbKala_GorohKala, txtSearch_Kala_GorohKala.Text.Trim, LevelGorohKala:=5, CodeGorohKala:=cmbG5.SelectedValue)
            End If
        ElseIf chkG4.Checked = True Then
            If dsForm.Tables("tbl_Goroh4").Rows.Count = 0 Then
                SearchInListBoxKala(lbKala_GorohKala, txtSearch_Kala_GorohKala.Text.Trim, LevelGorohKala:=3, CodeGorohKala:=cmbG3.SelectedValue)
            Else
                SearchInListBoxKala(lbKala_GorohKala, txtSearch_Kala_GorohKala.Text.Trim, LevelGorohKala:=4, CodeGorohKala:=cmbG4.SelectedValue)
            End If
        ElseIf chkG3.Checked = True Then
            If dsForm.Tables("tbl_Goroh3").Rows.Count = 0 Then
                SearchInListBoxKala(lbKala_GorohKala, txtSearch_Kala_GorohKala.Text.Trim, LevelGorohKala:=2, CodeGorohKala:=cmbG2.SelectedValue)
            Else
                SearchInListBoxKala(lbKala_GorohKala, txtSearch_Kala_GorohKala.Text.Trim, LevelGorohKala:=3, CodeGorohKala:=cmbG3.SelectedValue)
            End If
        ElseIf chkG2.Checked = True Then
            If dsForm.Tables("tbl_Goroh2").Rows.Count = 0 Then
                SearchInListBoxKala(lbKala_GorohKala, txtSearch_Kala_GorohKala.Text.Trim, LevelGorohKala:=1, CodeGorohKala:=cmbG1.SelectedValue)
            Else
                SearchInListBoxKala(lbKala_GorohKala, txtSearch_Kala_GorohKala.Text.Trim, LevelGorohKala:=2, CodeGorohKala:=cmbG2.SelectedValue)
            End If
        Else
            SearchInListBoxKala(lbKala_GorohKala, txtSearch_Kala_GorohKala.Text.Trim, LevelGorohKala:=1, CodeGorohKala:=cmbG1.SelectedValue)
        End If

    End Sub
    Private Sub txtSearch_Kala_brand_TextChanged(sender As Object, e As EventArgs) Handles txtSearch_Kala_brand.TextChanged
        SearchInListBoxKala(lbKala_Brand, txtSearch_Kala_brand.Text.Trim, ccBrand:=cmbBrand.SelectedValue)
    End Sub
    Private Sub cmbBrand_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbBrand.SelectedIndexChanged
        If Flg = False Then
            Exit Sub
        End If

        txtSearch_Kala_brand.Text = ""
        SearchInListBoxKala(lbKala_Brand, txtSearch_Kala_brand.Text.Trim, ccBrand:=cmbBrand.SelectedValue)

        txtSearch_Kala_Brand_Effect.Text = ""
        lbKala_Brand_Effect.DataSource = Nothing
        lbKala_Brand_Effect.Items.Clear()
    End Sub
    Private Sub LoadComboGoroh2()
        If dsForm.Tables("tbl_Goroh2").Rows.Count = 0 Then Exit Sub
        If chkG2.Checked = False Then
            Exit Sub
        End If
        Try
            FlgSearch = True

            Dim dvGoroh2 As New DataView(dsForm.Tables("tbl_Goroh2"), "CodeLink = " & IIf(cmbG1.SelectedIndex = -1, -1, cmbG1.SelectedValue), "", DataViewRowState.OriginalRows)
            With cmbG3
                .DataSource = Nothing
                .Items.Clear()
            End With
            With cmbG2
                .DataSource = Nothing
                .Items.Clear()
                .DataSource = dvGoroh2
                .DisplayMember = "Sharh"
                .ValueMember = "Code"
                .SelectedIndex = -1
            End With

            FlgSearch = False
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "Error IN DataBase ---->" & "LoadComboGoroh2")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, "Error IN ---->" & "LoadComboGoroh2")
        End Try

    End Sub
    Private Sub LoadComboGoroh3()
        If dsForm.Tables("tbl_Goroh3").Rows.Count = 0 Then Exit Sub
        If chkG3.Checked = False Then
            Exit Sub
        End If
        Try
            FlgSearch = True

            Dim dvGoroh3 As New DataView(dsForm.Tables("tbl_Goroh3"), "CodeLink = " & IIf(cmbG2.SelectedIndex = -1, -1, cmbG2.SelectedValue), "", DataViewRowState.OriginalRows)
            With cmbG4
                .DataSource = Nothing
                .Items.Clear()
            End With
            With cmbG3
                .DataSource = Nothing
                .Items.Clear()
                .DataSource = dvGoroh3
                .DisplayMember = "Sharh"
                .ValueMember = "Code"
                .SelectedIndex = -1
            End With

            FlgSearch = False
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "Error IN DataBase ---->" & "LoadComboGoroh3")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, "Error IN ---->" & "LoadComboGoroh3")
        End Try
    End Sub
    Private Sub LoadComboGoroh4()
        If dsForm.Tables("tbl_Goroh4").Rows.Count = 0 Then Exit Sub
        If chkG4.Checked = False Then
            Exit Sub
        End If
        Try
            FlgSearch = True

            Dim dvGoroh4 As New DataView(dsForm.Tables("tbl_Goroh4"), "CodeLink = " & IIf(cmbG3.SelectedIndex = -1, -1, cmbG3.SelectedValue), "", DataViewRowState.OriginalRows)
            With cmbG5
                .DataSource = Nothing
                .Items.Clear()
            End With
            With cmbG4
                .DataSource = Nothing
                .Items.Clear()
                .DataSource = dvGoroh4
                .DisplayMember = "Sharh"
                .ValueMember = "Code"
                .SelectedIndex = -1
            End With

            FlgSearch = False
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "Error IN DataBase ---->" & "LoadComboGoroh4")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, "Error IN ---->" & "LoadComboGoroh4")
        End Try
    End Sub
    Private Sub LoadComboGoroh5()
        If dsForm.Tables("tbl_Goroh5").Rows.Count = 0 Then Exit Sub
        If chkG5.Checked = False Then
            Exit Sub
        End If
        Try
            FlgSearch = True

            Dim dvGoroh5 As New DataView(dsForm.Tables("tbl_Goroh5"), "CodeLink = " & IIf(cmbG4.SelectedIndex = -1, -1, cmbG4.SelectedValue), "", DataViewRowState.OriginalRows)
            With cmbG5
                .DataSource = Nothing
                .Items.Clear()
                .DataSource = dvGoroh5
                .DisplayMember = "Sharh"
                .ValueMember = "Code"
                .SelectedIndex = -1
            End With

            FlgSearch = False
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "Error IN DataBase ---->" & "LoadComboGoroh5")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, "Error IN ---->" & "LoadComboGoroh5")
        End Try
    End Sub
    Private Sub cmbG1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbG1.SelectedIndexChanged
        LoadComboGoroh2()
        chkG5.Checked = False
        chkG4.Checked = False
        chkG3.Checked = False
        chkG2.Checked = False

        chkG5.Enabled = False
        chkG4.Enabled = False
        chkG3.Enabled = False

        If dsForm.Tables("tbl_Goroh1").Rows.Count = 0 Then Exit Sub
        If Not Flg Then Exit Sub
        SearchInListBoxKala(lbKala_GorohKala, txtSearch_Kala_GorohKala.Text.Trim, LevelGorohKala:=1, CodeGorohKala:=cmbG1.SelectedValue)

        strKala_GorohKala_Effect = ","
        txtSearch_Kala_GorohKala_Effect.Text = ""
        lbKala_GorohKala_Effect.DataSource = Nothing
        lbKala_GorohKala_Effect.Items.Clear()
    End Sub
    Private Sub chkG2_CheckedChanged(sender As Object, e As EventArgs) Handles chkG2.CheckedChanged
        If dsForm.Tables("tbl_Goroh1").Rows.Count = 0 Then Exit Sub
        If cmbG1.SelectedIndex = -1 Or cmbG1.SelectedValue = 0 Then Exit Sub
        If chkG2.Checked = False Then
            cmbG2.DataSource = Nothing
            cmbG2.Items.Clear()
            chkG3.Enabled = False
        Else
            LoadComboGoroh2()
            txtSearch_Kala_GorohKala.Text = ""
            SearchInListBoxKala(lbKala_GorohKala, txtSearch_Kala_GorohKala.Text.Trim, LevelGorohKala:=2, CodeGorohKala:=cmbG2.SelectedValue)
            chkG3.Enabled = True
        End If

        strKala_GorohKala_Effect = ","
        txtSearch_Kala_GorohKala_Effect.Text = ""
        lbKala_GorohKala_Effect.DataSource = Nothing
        lbKala_GorohKala_Effect.Items.Clear()
    End Sub
    Private Sub cmbG2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbG2.SelectedIndexChanged
        LoadComboGoroh3()
        chkG5.Checked = False
        chkG4.Checked = False
        chkG3.Checked = False

        chkG5.Enabled = False
        chkG4.Enabled = False

        If dsForm.Tables("tbl_Goroh2").Rows.Count = 0 Then Exit Sub
        If Not Flg Then Exit Sub
        If FlgSearch = False Then
            SearchInListBoxKala(lbKala_GorohKala, txtSearch_Kala_GorohKala.Text.Trim, LevelGorohKala:=2, CodeGorohKala:=cmbG2.SelectedValue)
        End If

        strKala_GorohKala_Effect = ","
        txtSearch_Kala_GorohKala_Effect.Text = ""
        lbKala_GorohKala_Effect.DataSource = Nothing
        lbKala_GorohKala_Effect.Items.Clear()
    End Sub
    Private Sub chkG3_CheckedChanged(sender As Object, e As EventArgs) Handles chkG3.CheckedChanged
        If dsForm.Tables("tbl_Goroh2").Rows.Count = 0 Then Exit Sub
        If cmbG2.SelectedIndex = -1 Or cmbG2.SelectedValue = 0 Then Exit Sub
        If chkG3.Checked = False Then
            cmbG3.DataSource = Nothing
            cmbG3.Items.Clear()
            chkG4.Enabled = False
        Else
            LoadComboGoroh3()
            txtSearch_Kala_GorohKala.Text = ""
            SearchInListBoxKala(lbKala_GorohKala, txtSearch_Kala_GorohKala.Text.Trim, LevelGorohKala:=3, CodeGorohKala:=cmbG3.SelectedValue)
            chkG4.Enabled = True
        End If

        strKala_GorohKala_Effect = ","
        txtSearch_Kala_GorohKala_Effect.Text = ""
        lbKala_GorohKala_Effect.DataSource = Nothing
        lbKala_GorohKala_Effect.Items.Clear()
    End Sub
    Private Sub cmbG3_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbG3.SelectedIndexChanged
        LoadComboGoroh4()
        chkG5.Checked = False
        chkG4.Checked = False

        chkG5.Enabled = False

        If dsForm.Tables("tbl_Goroh3").Rows.Count = 0 Then Exit Sub
        If Not Flg Then Exit Sub
        If FlgSearch = False Then
            SearchInListBoxKala(lbKala_GorohKala, txtSearch_Kala_GorohKala.Text.Trim, LevelGorohKala:=3, CodeGorohKala:=cmbG3.SelectedValue)
        End If

        strKala_GorohKala_Effect = ","
        txtSearch_Kala_GorohKala_Effect.Text = ""
        lbKala_GorohKala_Effect.DataSource = Nothing
        lbKala_GorohKala_Effect.Items.Clear()
    End Sub
    Private Sub chkG4_CheckedChanged(sender As Object, e As EventArgs) Handles chkG4.CheckedChanged
        If dsForm.Tables("tbl_Goroh3").Rows.Count = 0 Then Exit Sub
        If cmbG3.SelectedIndex = -1 Or cmbG3.SelectedValue = 0 Then Exit Sub
        If chkG4.Checked = False Then
            cmbG4.DataSource = Nothing
            cmbG4.Items.Clear()
            chkG5.Enabled = False
        Else
            LoadComboGoroh4()
            txtSearch_Kala_GorohKala.Text = ""
            SearchInListBoxKala(lbKala_GorohKala, txtSearch_Kala_GorohKala.Text.Trim, LevelGorohKala:=4, CodeGorohKala:=cmbG4.SelectedValue)
            chkG5.Enabled = True
        End If

        strKala_GorohKala_Effect = ","
        txtSearch_Kala_GorohKala_Effect.Text = ""
        lbKala_GorohKala_Effect.DataSource = Nothing
        lbKala_GorohKala_Effect.Items.Clear()
    End Sub
    Private Sub cmbG4_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbG4.SelectedIndexChanged
        LoadComboGoroh5()
        chkG5.Checked = False

        If dsForm.Tables("tbl_Goroh4").Rows.Count = 0 Then Exit Sub
        If Not Flg Then Exit Sub
        If FlgSearch = False Then
            SearchInListBoxKala(lbKala_GorohKala, txtSearch_Kala_GorohKala.Text.Trim, LevelGorohKala:=4, CodeGorohKala:=cmbG4.SelectedValue)
        End If

        txtSearch_Kala_GorohKala_Effect.Text = ""
        lbKala_GorohKala_Effect.DataSource = Nothing
        lbKala_GorohKala_Effect.Items.Clear()
    End Sub
    Private Sub chkG5_CheckedChanged(sender As Object, e As EventArgs) Handles chkG5.CheckedChanged
        If dsForm.Tables("tbl_Goroh4").Rows.Count = 0 Then Exit Sub
        If cmbG4.SelectedIndex = -1 Or cmbG4.SelectedValue = 0 Then Exit Sub
        If chkG5.Checked = False Then
            cmbG5.DataSource = Nothing
            cmbG5.Items.Clear()
        Else
            LoadComboGoroh5()
            txtSearch_Kala_GorohKala.Text = ""
            SearchInListBoxKala(lbKala_GorohKala, txtSearch_Kala_GorohKala.Text.Trim, LevelGorohKala:=5, CodeGorohKala:=cmbG5.SelectedValue)
        End If

        strKala_GorohKala_Effect = ","
        txtSearch_Kala_GorohKala_Effect.Text = ""
        lbKala_GorohKala_Effect.DataSource = Nothing
        lbKala_GorohKala_Effect.Items.Clear()
    End Sub
    Private Sub cmbG5_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbG5.SelectedIndexChanged
        If FlgSearch = False Then
            SearchInListBoxKala(lbKala_GorohKala, txtSearch_Kala_GorohKala.Text.Trim, LevelGorohKala:=5, CodeGorohKala:=cmbG5.SelectedValue)
        End If

        strKala_GorohKala_Effect = ","
        txtSearch_Kala_GorohKala_Effect.Text = ""
        lbKala_GorohKala_Effect.DataSource = Nothing
        lbKala_GorohKala_Effect.Items.Clear()
    End Sub
    Private Sub brnAddMoshtary_Valid_Click(sender As Object, e As EventArgs) Handles brnAddMoshtary_Valid.Click
        If lbMoshtary.Items.Count = 0 Then
            Exit Sub
        End If

        strMoshtary_Valid = strMoshtary_Valid.Replace(Str(lbMoshtary.SelectedValue).Trim & ",", "")
        strMoshtary_Valid &= Str(lbMoshtary.SelectedValue).Trim & ","

        SearchInListBoxMoshtary(lbMoshtary_Valid, txtSearch_Moshtary_Valid.Text.Trim, strSelectedFields:=strMoshtary_Valid)
    End Sub
    Private Sub lbMoshtary_DoubleClick(sender As Object, e As EventArgs) Handles lbMoshtary.DoubleClick
        If lbMoshtary.Items.Count = 0 Then
            Exit Sub
        End If

        strMoshtary_Valid = strMoshtary_Valid.Replace(Str(lbMoshtary.SelectedValue).Trim & ",", "")
        strMoshtary_Valid &= Str(lbMoshtary.SelectedValue).Trim & ","

        SearchInListBoxMoshtary(lbMoshtary_Valid, txtSearch_Moshtary_Valid.Text.Trim, strSelectedFields:=strMoshtary_Valid)
    End Sub
    Private Sub btnRemoveMoshtary_Valid_Click(sender As Object, e As EventArgs) Handles btnRemoveMoshtary_Valid.Click
        If lbMoshtary_Valid.Items.Count = 0 Then
            Exit Sub
        End If

        strMoshtary_Valid = strMoshtary_Valid.Replace(Str(lbMoshtary_Valid.SelectedValue).Trim & ",", "")

        SearchInListBoxMoshtary(lbMoshtary_Valid, txtSearch_Moshtary_Valid.Text.Trim, strSelectedFields:=strMoshtary_Valid)
    End Sub
    Private Sub lbMoshtary_Valid_DoubleClick(sender As Object, e As EventArgs) Handles lbMoshtary_Valid.DoubleClick
        If lbMoshtary_Valid.Items.Count = 0 Then
            Exit Sub
        End If

        strMoshtary_Valid = strMoshtary_Valid.Replace(Str(lbMoshtary_Valid.SelectedValue).Trim & ",", "")

        SearchInListBoxMoshtary(lbMoshtary_Valid, txtSearch_Moshtary_Valid.Text.Trim, strSelectedFields:=strMoshtary_Valid)
    End Sub
    Private Sub btnAddMoshtary_NoeMoshtary_Valid_Click(sender As Object, e As EventArgs) Handles btnAddMoshtary_NoeMoshtary_Valid.Click
        If lbMoshtary_NoeMoshtary.Items.Count = 0 Then
            Exit Sub
        End If

        strMoshtary_NoeMoshtary_Valid = strMoshtary_NoeMoshtary_Valid.Replace(Str(lbMoshtary_NoeMoshtary.SelectedValue).Trim & ",", "")
        strMoshtary_NoeMoshtary_Valid &= Str(lbMoshtary_NoeMoshtary.SelectedValue).Trim & ","

        SearchInListBoxMoshtary(lbMoshtary_NoeMoshtary_Valid, txtSearch_Moshtary_NoeMoshtary_Valid.Text.Trim, strSelectedFields:=strMoshtary_NoeMoshtary_Valid)
    End Sub
    Private Sub lbMoshtary_NoeMoshtary_DoubleClick(sender As Object, e As EventArgs) Handles lbMoshtary_NoeMoshtary.DoubleClick
        If lbMoshtary_NoeMoshtary.Items.Count = 0 Then
            Exit Sub
        End If

        strMoshtary_NoeMoshtary_Valid = strMoshtary_NoeMoshtary_Valid.Replace(Str(lbMoshtary_NoeMoshtary.SelectedValue).Trim & ",", "")
        strMoshtary_NoeMoshtary_Valid &= Str(lbMoshtary_NoeMoshtary.SelectedValue).Trim & ","

        SearchInListBoxMoshtary(lbMoshtary_NoeMoshtary_Valid, txtSearch_Moshtary_NoeMoshtary_Valid.Text.Trim, strSelectedFields:=strMoshtary_NoeMoshtary_Valid)
    End Sub
    Private Sub btnRemoveMoshtary_NoeMoshtary_Valid_Click(sender As Object, e As EventArgs) Handles btnRemoveMoshtary_NoeMoshtary_Valid.Click
        If lbMoshtary_NoeMoshtary_Valid.Items.Count = 0 Then
            Exit Sub
        End If

        strMoshtary_NoeMoshtary_Valid = strMoshtary_NoeMoshtary_Valid.Replace(Str(lbMoshtary_NoeMoshtary_Valid.SelectedValue).Trim & ",", "")

        SearchInListBoxMoshtary(lbMoshtary_NoeMoshtary_Valid, txtSearch_Moshtary_NoeMoshtary_Valid.Text.Trim, strSelectedFields:=strMoshtary_NoeMoshtary_Valid)
    End Sub
    Private Sub lbMoshtary_NoeMoshtary_Valid_DoubleClick(sender As Object, e As EventArgs) Handles lbMoshtary_NoeMoshtary_Valid.DoubleClick
        If lbMoshtary_NoeMoshtary_Valid.Items.Count = 0 Then
            Exit Sub
        End If

        strMoshtary_NoeMoshtary_Valid = strMoshtary_NoeMoshtary_Valid.Replace(Str(lbMoshtary_NoeMoshtary_Valid.SelectedValue).Trim & ",", "")

        SearchInListBoxMoshtary(lbMoshtary_NoeMoshtary_Valid, txtSearch_Moshtary_NoeMoshtary_Valid.Text.Trim, strSelectedFields:=strMoshtary_NoeMoshtary_Valid)
    End Sub
    Private Sub btnAddMoshtary_NoeSenf_Valid_Click(sender As Object, e As EventArgs) Handles btnAddMoshtary_NoeSenf_Valid.Click
        If lbMoshtary_NoeSenf.Items.Count = 0 Then
            Exit Sub
        End If

        strMoshtary_NoeSenf_Valid = strMoshtary_NoeSenf_Valid.Replace(Str(lbMoshtary_NoeSenf.SelectedValue).Trim & ",", "")
        strMoshtary_NoeSenf_Valid &= Str(lbMoshtary_NoeSenf.SelectedValue).Trim & ","

        SearchInListBoxMoshtary(lbMoshtary_NoeSenf_Valid, txtSearch_Moshtary_NoeSenf_Valid.Text.Trim, strSelectedFields:=strMoshtary_NoeSenf_Valid)
    End Sub
    Private Sub lbMoshtary_NoeSenf_DoubleClick(sender As Object, e As EventArgs) Handles lbMoshtary_NoeSenf.DoubleClick
        If lbMoshtary_NoeSenf.Items.Count = 0 Then
            Exit Sub
        End If

        strMoshtary_NoeSenf_Valid = strMoshtary_NoeSenf_Valid.Replace(Str(lbMoshtary_NoeSenf.SelectedValue).Trim & ",", "")
        strMoshtary_NoeSenf_Valid &= Str(lbMoshtary_NoeSenf.SelectedValue).Trim & ","

        SearchInListBoxMoshtary(lbMoshtary_NoeSenf_Valid, txtSearch_Moshtary_NoeSenf_Valid.Text.Trim, strSelectedFields:=strMoshtary_NoeSenf_Valid)
    End Sub

    Private Sub btnRemoveMoshtary_NoeSenf_Valid_Click(sender As Object, e As EventArgs) Handles btnRemoveMoshtary_NoeSenf_Valid.Click
        If lbMoshtary_NoeSenf_Valid.Items.Count = 0 Then
            Exit Sub
        End If

        strMoshtary_NoeSenf_Valid = strMoshtary_NoeSenf_Valid.Replace(Str(lbMoshtary_NoeSenf_Valid.SelectedValue).Trim & ",", "")

        SearchInListBoxMoshtary(lbMoshtary_NoeSenf_Valid, txtSearch_Moshtary_NoeSenf_Valid.Text.Trim, strSelectedFields:=strMoshtary_NoeSenf_Valid)
    End Sub
    Private Sub lbMoshtary_NoeSenf_Valid_DoubleClick(sender As Object, e As EventArgs) Handles lbMoshtary_NoeSenf_Valid.DoubleClick
        If lbMoshtary_NoeSenf_Valid.Items.Count = 0 Then
            Exit Sub
        End If

        strMoshtary_NoeSenf_Valid = strMoshtary_NoeSenf_Valid.Replace(Str(lbMoshtary_NoeSenf_Valid.SelectedValue).Trim & ",", "")

        SearchInListBoxMoshtary(lbMoshtary_NoeSenf_Valid, txtSearch_Moshtary_NoeSenf_Valid.Text.Trim, strSelectedFields:=strMoshtary_NoeSenf_Valid)
    End Sub

    Private Sub btnAddMoshtary_Mantagheh_Valid_Click(sender As Object, e As EventArgs) Handles btnAddMoshtary_Mantagheh_Valid.Click
        If lbMoshtary_Mantagheh.Items.Count = 0 Then
            Exit Sub
        End If

        strMoshtary_Mantagheh_Valid = strMoshtary_Mantagheh_Valid.Replace(Str(lbMoshtary_Mantagheh.SelectedValue).Trim & ",", "")
        strMoshtary_Mantagheh_Valid &= Str(lbMoshtary_Mantagheh.SelectedValue).Trim & ","

        SearchInListBoxMoshtary(lbMoshtary_Mantagheh_Valid, txtSearch_Moshtary_Mantagheh_Valid.Text.Trim, strSelectedFields:=strMoshtary_Mantagheh_Valid)
    End Sub

    Private Sub lbMoshtary_Mantagheh_DoubleClick(sender As Object, e As EventArgs) Handles lbMoshtary_Mantagheh.DoubleClick
        If lbMoshtary_Mantagheh.Items.Count = 0 Then
            Exit Sub
        End If

        strMoshtary_Mantagheh_Valid = strMoshtary_Mantagheh_Valid.Replace(Str(lbMoshtary_Mantagheh.SelectedValue).Trim & ",", "")
        strMoshtary_Mantagheh_Valid &= Str(lbMoshtary_Mantagheh.SelectedValue).Trim & ","

        SearchInListBoxMoshtary(lbMoshtary_Mantagheh_Valid, txtSearch_Moshtary_Mantagheh_Valid.Text.Trim, strSelectedFields:=strMoshtary_Mantagheh_Valid)
    End Sub
    Private Sub btnRemoveMoshtary_Mantagheh_Valid_Click(sender As Object, e As EventArgs) Handles btnRemoveMoshtary_Mantagheh_Valid.Click
        If lbMoshtary_Mantagheh_Valid.Items.Count = 0 Then
            Exit Sub
        End If

        strMoshtary_Mantagheh_Valid = strMoshtary_Mantagheh_Valid.Replace(Str(lbMoshtary_Mantagheh_Valid.SelectedValue).Trim & ",", "")

        SearchInListBoxMoshtary(lbMoshtary_Mantagheh_Valid, txtSearch_Moshtary_Mantagheh_Valid.Text.Trim, strSelectedFields:=strMoshtary_Mantagheh_Valid)
    End Sub

    Private Sub lbMoshtary_Mantagheh_Valid_DoubleClick(sender As Object, e As EventArgs) Handles lbMoshtary_Mantagheh_Valid.DoubleClick
        If lbMoshtary_Mantagheh_Valid.Items.Count = 0 Then
            Exit Sub
        End If

        strMoshtary_Mantagheh_Valid = strMoshtary_Mantagheh_Valid.Replace(Str(lbMoshtary_Mantagheh_Valid.SelectedValue).Trim & ",", "")

        SearchInListBoxMoshtary(lbMoshtary_Mantagheh_Valid, txtSearch_Moshtary_Mantagheh_Valid.Text.Trim, strSelectedFields:=strMoshtary_Mantagheh_Valid)
    End Sub
    Private Sub btnAddMoshtary_NotEffect_Click(sender As Object, e As EventArgs) Handles btnAddMoshtary_NotEffect.Click
        If lbMoshtary_Effect.Items.Count = 0 Then
            Exit Sub
        End If

        strMoshtary_NotEffect = strMoshtary_NotEffect.Replace(Str(lbMoshtary_Effect.SelectedValue).Trim & ",", "")
        strMoshtary_NotEffect &= Str(lbMoshtary_Effect.SelectedValue).Trim & ","

        SearchInListBoxMoshtary(lbMoshtary_NotEffect, txtSearch_Moshtary_NotEffect.Text.Trim, strSelectedFields:=strMoshtary_NotEffect)
    End Sub
    Private Sub lbMoshtary_Effect_DoubleClick(sender As Object, e As EventArgs) Handles lbMoshtary_Effect.DoubleClick
        If lbMoshtary_Effect.Items.Count = 0 Then
            Exit Sub
        End If

        strMoshtary_NotEffect = strMoshtary_NotEffect.Replace(Str(lbMoshtary_Effect.SelectedValue).Trim & ",", "")
        strMoshtary_NotEffect &= Str(lbMoshtary_Effect.SelectedValue).Trim & ","

        SearchInListBoxMoshtary(lbMoshtary_NotEffect, txtSearch_Moshtary_NotEffect.Text.Trim, strSelectedFields:=strMoshtary_NotEffect)
    End Sub

    Private Sub btnRemoveMoshtary_NotEffect_Click(sender As Object, e As EventArgs) Handles btnRemoveMoshtary_NotEffect.Click
        If lbMoshtary_NotEffect.Items.Count = 0 Then
            Exit Sub
        End If

        strMoshtary_NotEffect = strMoshtary_NotEffect.Replace(Str(lbMoshtary_NotEffect.SelectedValue).Trim & ",", "")

        SearchInListBoxMoshtary(lbMoshtary_NotEffect, txtSearch_Moshtary_NotEffect.Text.Trim, strSelectedFields:=strMoshtary_NotEffect)
    End Sub

    Private Sub lbMoshtary_NotEffect_DoubleClick(sender As Object, e As EventArgs) Handles lbMoshtary_NotEffect.DoubleClick
        If lbMoshtary_NotEffect.Items.Count = 0 Then
            Exit Sub
        End If

        strMoshtary_NotEffect = strMoshtary_NotEffect.Replace(Str(lbMoshtary_NotEffect.SelectedValue).Trim & ",", "")

        SearchInListBoxMoshtary(lbMoshtary_NotEffect, txtSearch_Moshtary_NotEffect.Text.Trim, strSelectedFields:=strMoshtary_NotEffect)
    End Sub
    Private Sub btnAddNoeMoshtary_NotEffect_Click(sender As Object, e As EventArgs) Handles btnAddNoeMoshtary_NotEffect.Click
        If lbNoeMoshtary_Effect.Items.Count = 0 Then
            Exit Sub
        End If

        strNoeMoshtary_NotEffect = strNoeMoshtary_NotEffect.Replace(Str(lbNoeMoshtary_Effect.SelectedValue).Trim & ",", "")
        strNoeMoshtary_NotEffect &= Str(lbNoeMoshtary_Effect.SelectedValue).Trim & ","

        SearchInListBoxNoeMoshtary(lbNoeMoshtary_NotEffect, strSearch:=txtSearch_NoeMoshtary_Effect.Text.Trim, strSelectedFields:=strNoeMoshtary_NotEffect)
    End Sub
    Private Sub lbNoeMoshtary_Effect_DoubleClick(sender As Object, e As EventArgs) Handles lbNoeMoshtary_Effect.DoubleClick
        If lbNoeMoshtary_Effect.Items.Count = 0 Then
            Exit Sub
        End If

        strNoeMoshtary_NotEffect = strNoeMoshtary_NotEffect.Replace(Str(lbNoeMoshtary_Effect.SelectedValue).Trim & ",", "")
        strNoeMoshtary_NotEffect &= Str(lbNoeMoshtary_Effect.SelectedValue).Trim & ","

        SearchInListBoxNoeMoshtary(lbNoeMoshtary_NotEffect, strSearch:=txtSearch_NoeMoshtary_Effect.Text.Trim, strSelectedFields:=strNoeMoshtary_NotEffect)
    End Sub
    Private Sub btnRemoveNoeMoshtary_NotEffect_Click(sender As Object, e As EventArgs) Handles btnRemoveNoeMoshtary_NotEffect.Click
        If lbNoeMoshtary_NotEffect.Items.Count = 0 Then
            Exit Sub
        End If

        strNoeMoshtary_NotEffect = strNoeMoshtary_NotEffect.Replace(Str(lbNoeMoshtary_NotEffect.SelectedValue).Trim & ",", "")

        SearchInListBoxNoeMoshtary(lbNoeMoshtary_NotEffect, strSearch:=txtSearch_NoeMoshtary_NotEffect.Text.Trim, strSelectedFields:=strNoeMoshtary_NotEffect)
    End Sub
    Private Sub lbNoeMoshtary_NotEffect_DoubleClick(sender As Object, e As EventArgs) Handles lbNoeMoshtary_NotEffect.DoubleClick
        If lbNoeMoshtary_NotEffect.Items.Count = 0 Then
            Exit Sub
        End If

        strNoeMoshtary_NotEffect = strNoeMoshtary_NotEffect.Replace(Str(lbNoeMoshtary_NotEffect.SelectedValue).Trim & ",", "")

        SearchInListBoxNoeMoshtary(lbNoeMoshtary_NotEffect, strSearch:=txtSearch_NoeMoshtary_NotEffect.Text.Trim, strSelectedFields:=strNoeMoshtary_NotEffect)
    End Sub
    Private Sub btnAddNoeSenf_NotEffect_Click(sender As Object, e As EventArgs) Handles btnAddNoeSenf_NotEffect.Click
        If lbNoeSenf_Effect.Items.Count = 0 Then
            Exit Sub
        End If

        strNoeSenf_NotEffect = strNoeSenf_NotEffect.Replace(Str(lbNoeSenf_Effect.SelectedValue).Trim & ",", "")
        strNoeSenf_NotEffect &= Str(lbNoeSenf_Effect.SelectedValue).Trim & ","

        SearchInListBoxNoeSenf(lbNoeSenf_NotEffect, strSearch:=txtSearch_NoeSenf_Effect.Text.Trim, strSelectedFields:=strNoeSenf_NotEffect)
    End Sub
    Private Sub lbNoeSenf_Effect_DoubleClick(sender As Object, e As EventArgs) Handles lbNoeSenf_Effect.DoubleClick
        If lbNoeSenf_Effect.Items.Count = 0 Then
            Exit Sub
        End If

        strNoeSenf_NotEffect = strNoeSenf_NotEffect.Replace(Str(lbNoeSenf_Effect.SelectedValue).Trim & ",", "")
        strNoeSenf_NotEffect &= Str(lbNoeSenf_Effect.SelectedValue).Trim & ","

        SearchInListBoxNoeSenf(lbNoeSenf_NotEffect, strSearch:=txtSearch_NoeSenf_Effect.Text.Trim, strSelectedFields:=strNoeSenf_NotEffect)
    End Sub
    Private Sub btnRemoveNoeSenf_NotEffect_Click(sender As Object, e As EventArgs) Handles btnRemoveNoeSenf_NotEffect.Click
        If lbNoeSenf_NotEffect.Items.Count = 0 Then
            Exit Sub
        End If

        strNoeSenf_NotEffect = strNoeSenf_NotEffect.Replace(Str(lbNoeSenf_NotEffect.SelectedValue).Trim & ",", "")

        SearchInListBoxNoeSenf(lbNoeSenf_NotEffect, strSearch:=txtSearch_NoeSenf_NotEffect.Text.Trim, strSelectedFields:=strNoeSenf_NotEffect)
    End Sub
    Private Sub lbNoeSenf_NotEffect_DoubleClick(sender As Object, e As EventArgs) Handles lbNoeSenf_NotEffect.DoubleClick
        If lbNoeSenf_NotEffect.Items.Count = 0 Then
            Exit Sub
        End If

        strNoeSenf_NotEffect = strNoeSenf_NotEffect.Replace(Str(lbNoeSenf_NotEffect.SelectedValue).Trim & ",", "")

        SearchInListBoxNoeSenf(lbNoeSenf_NotEffect, strSearch:=txtSearch_NoeSenf_NotEffect.Text.Trim, strSelectedFields:=strNoeSenf_NotEffect)
    End Sub
    Private Sub btnAddManategh_NotEffect_Click(sender As Object, e As EventArgs) Handles btnAddManategh_NotEffect.Click
        If chkMahaleh_Effect.Checked = False Then
            If chkMantagheh_Effect.Checked = False Then
                If chkShahr_Effect.Checked = False Then
                    If lbOstan_Effect.Items.Count = 0 Then
                        Exit Sub
                    End If

                    strManategh_NotEffect = strManategh_NotEffect.Replace(Str(lbOstan_Effect.SelectedValue).Trim & ",", "")
                    strManategh_NotEffect &= Str(lbOstan_Effect.SelectedValue).Trim & ","
                Else
                    If lbShahr_Effect.Items.Count = 0 Then
                        Exit Sub
                    End If

                    strManategh_NotEffect = strManategh_NotEffect.Replace(Str(lbShahr_Effect.SelectedValue).Trim & ",", "")
                    strManategh_NotEffect &= Str(lbShahr_Effect.SelectedValue).Trim & ","
                End If
            Else
                If lbMantagheh_Effect.Items.Count = 0 Then
                    Exit Sub
                End If

                strManategh_NotEffect = strManategh_NotEffect.Replace(Str(lbMantagheh_Effect.SelectedValue).Trim & ",", "")
                strManategh_NotEffect &= Str(lbMantagheh_Effect.SelectedValue).Trim & ","
            End If
        Else
            If lbMahaleh_Effect.Items.Count = 0 Then
                Exit Sub
            End If

            strManategh_NotEffect = strManategh_NotEffect.Replace(Str(lbMahaleh_Effect.SelectedValue).Trim & ",", "")
            strManategh_NotEffect &= Str(lbMahaleh_Effect.SelectedValue).Trim & ","
        End If

        SearchInListBoxManategh_NotEffect(strManategh_NotEffect, txtSearch_Manategh_NotEffect.Text.Trim)
    End Sub
    Private Sub lbOstan_Effect_DoubleClick(sender As Object, e As EventArgs) Handles lbOstan_Effect.DoubleClick
        If chkMahaleh_Effect.Checked = False Then
            If chkMantagheh_Effect.Checked = False Then
                If chkShahr_Effect.Checked = False Then
                    If lbOstan_Effect.Items.Count = 0 Then
                        Exit Sub
                    End If

                    strManategh_NotEffect = strManategh_NotEffect.Replace(Str(lbOstan_Effect.SelectedValue).Trim & ",", "")
                    strManategh_NotEffect &= Str(lbOstan_Effect.SelectedValue).Trim & ","

                    SearchInListBoxManategh_NotEffect(strManategh_NotEffect, txtSearch_Manategh_NotEffect.Text.Trim)
                End If
            End If
        End If
    End Sub
    Private Sub lbShahr_Effect_DoubleClick(sender As Object, e As EventArgs) Handles lbShahr_Effect.DoubleClick
        If chkMahaleh_Effect.Checked = False Then
            If chkMantagheh_Effect.Checked = False Then
                If chkShahr_Effect.Checked = True Then
                    If lbShahr_Effect.Items.Count = 0 Then
                        Exit Sub
                    End If

                    strManategh_NotEffect = strManategh_NotEffect.Replace(Str(lbShahr_Effect.SelectedValue).Trim & ",", "")
                    strManategh_NotEffect &= Str(lbShahr_Effect.SelectedValue).Trim & ","

                    SearchInListBoxManategh_NotEffect(strManategh_NotEffect, txtSearch_Manategh_NotEffect.Text.Trim)
                End If
            End If
        End If
    End Sub
    Private Sub lbMantagheh_Effect_DoubleClick(sender As Object, e As EventArgs) Handles lbMantagheh_Effect.DoubleClick
        If chkMahaleh_Effect.Checked = False Then
            If chkMantagheh_Effect.Checked = True Then
                If lbMantagheh_Effect.Items.Count = 0 Then
                    Exit Sub
                End If

                strManategh_NotEffect = strManategh_NotEffect.Replace(Str(lbMantagheh_Effect.SelectedValue).Trim & ",", "")
                strManategh_NotEffect &= Str(lbMantagheh_Effect.SelectedValue).Trim & ","

                SearchInListBoxManategh_NotEffect(strManategh_NotEffect, txtSearch_Manategh_NotEffect.Text.Trim)
            End If
        End If
    End Sub
    Private Sub lbMahaleh_Effect_DoubleClick(sender As Object, e As EventArgs) Handles lbMahaleh_Effect.DoubleClick
        If chkMahaleh_Effect.Checked = True Then
            If lbMahaleh_Effect.Items.Count = 0 Then
                Exit Sub
            End If

            strManategh_NotEffect = strManategh_NotEffect.Replace(Str(lbMahaleh_Effect.SelectedValue).Trim & ",", "")
            strManategh_NotEffect &= Str(lbMahaleh_Effect.SelectedValue).Trim & ","

            SearchInListBoxManategh_NotEffect(strManategh_NotEffect, txtSearch_Manategh_NotEffect.Text.Trim)
        End If
    End Sub
    Private Sub btnRemoveManategh_NotEffect_Click(sender As Object, e As EventArgs) Handles btnRemoveManategh_NotEffect.Click
        If lbManategh_NotEffect.Items.Count = 0 Then
            Exit Sub
        End If

        strManategh_NotEffect = strManategh_NotEffect.Replace(Str(lbManategh_NotEffect.SelectedValue).Trim & ",", "")

        SearchInListBoxManategh_NotEffect(strManategh_NotEffect, txtSearch_Manategh_NotEffect.Text.Trim)
    End Sub
    Private Sub lbManategh_NotEffect_DoubleClick(sender As Object, e As EventArgs) Handles lbManategh_NotEffect.DoubleClick
        If lbManategh_NotEffect.Items.Count = 0 Then
            Exit Sub
        End If

        strManategh_NotEffect = strManategh_NotEffect.Replace(Str(lbManategh_NotEffect.SelectedValue).Trim & ",", "")

        SearchInListBoxManategh_NotEffect(strManategh_NotEffect, txtSearch_Manategh_NotEffect.Text.Trim)
    End Sub
    Private Sub txtSearch_Moshtary_Valid_TextChanged(sender As Object, e As EventArgs) Handles txtSearch_Moshtary_Valid.TextChanged
        SearchInListBoxMoshtary(lbMoshtary_Valid, txtSearch_Moshtary_Valid.Text.Trim, strSelectedFields:=strMoshtary_Valid)
    End Sub
    Private Sub txtSearch_Moshtary_NoeMoshtary_Valid_TextChanged(sender As Object, e As EventArgs) Handles txtSearch_Moshtary_NoeMoshtary_Valid.TextChanged
        SearchInListBoxMoshtary(lbMoshtary_NoeMoshtary_Valid, txtSearch_Moshtary_NoeMoshtary_Valid.Text.Trim, strSelectedFields:=strMoshtary_NoeMoshtary_Valid)
    End Sub
    Private Sub txtSearch_Moshtary_NoeSenf_Valid_TextChanged(sender As Object, e As EventArgs) Handles txtSearch_Moshtary_NoeSenf_Valid.TextChanged
        SearchInListBoxMoshtary(lbMoshtary_NoeSenf_Valid, txtSearch_Moshtary_NoeSenf_Valid.Text.Trim, strSelectedFields:=strMoshtary_NoeSenf_Valid)
    End Sub
    Private Sub txtSearch_Moshtary_Mantagheh_Valid_TextChanged(sender As Object, e As EventArgs) Handles txtSearch_Moshtary_Mantagheh_Valid.TextChanged
        SearchInListBoxMoshtary(lbMoshtary_Mantagheh_Valid, txtSearch_Moshtary_Mantagheh_Valid.Text.Trim, strSelectedFields:=strMoshtary_Mantagheh_Valid)
    End Sub
    Private Sub txtSearch_Moshtary_NotEffect_TextChanged(sender As Object, e As EventArgs) Handles txtSearch_Moshtary_NotEffect.TextChanged
        SearchInListBoxMoshtary(lbMoshtary_NotEffect, txtSearch_Moshtary_NotEffect.Text.Trim, strSelectedFields:=strMoshtary_NotEffect)
    End Sub
    Private Sub txtSearch_NoeMoshtary_NotEffect_TextChanged(sender As Object, e As EventArgs) Handles txtSearch_NoeMoshtary_NotEffect.TextChanged
        SearchInListBoxNoeMoshtary(lbNoeMoshtary_NotEffect, strSearch:=txtSearch_NoeMoshtary_NotEffect.Text.Trim, strSelectedFields:=strNoeMoshtary_NotEffect)
    End Sub
    Private Sub txtSearch_NoeSenf_NotEffect_TextChanged(sender As Object, e As EventArgs) Handles txtSearch_NoeSenf_NotEffect.TextChanged
        SearchInListBoxNoeSenf(lbNoeSenf_NotEffect, strSearch:=txtSearch_NoeSenf_NotEffect.Text.Trim, strSelectedFields:=strNoeSenf_NotEffect)
    End Sub
    Private Sub txtSearch_Manategh_NotEffect_TextChanged(sender As Object, e As EventArgs) Handles txtSearch_Manategh_NotEffect.TextChanged
        SearchInListBoxManategh_NotEffect(strManategh_NotEffect, txtSearch_Manategh_NotEffect.Text.Trim)
    End Sub
    Private Sub btnAddKala_Click(sender As Object, e As EventArgs) Handles btnAddKala.Click
        If lbKala.Items.Count = 0 Then
            Exit Sub
        End If

        strKala_Effect = strKala_Effect.Replace(Str(lbKala.SelectedValue).Trim & ",", "")
        strKala_Effect &= Str(lbKala.SelectedValue).Trim & ","

        SearchInListBoxKala(lbKala_Effect, txtSearch_Kala_Effect.Text.Trim, strSelectedFields:=strKala_Effect)
    End Sub
    Private Sub lbKala_DoubleClick(sender As Object, e As EventArgs) Handles lbKala.DoubleClick
        If lbKala.Items.Count = 0 Then
            Exit Sub
        End If

        strKala_Effect = strKala_Effect.Replace(Str(lbKala.SelectedValue).Trim & ",", "")
        strKala_Effect &= Str(lbKala.SelectedValue).Trim & ","

        SearchInListBoxKala(lbKala_Effect, txtSearch_Kala_Effect.Text.Trim, strSelectedFields:=strKala_Effect)
    End Sub
    Private Sub btnRemoveKala_Click(sender As Object, e As EventArgs) Handles btnRemoveKala.Click
        If lbKala_Effect.Items.Count = 0 Then
            Exit Sub
        End If

        strKala_Effect = strKala_Effect.Replace(Str(lbKala_Effect.SelectedValue).Trim & ",", "")

        SearchInListBoxKala(lbKala_Effect, txtSearch_Kala_Effect.Text.Trim, strSelectedFields:=strKala_Effect)
    End Sub
    Private Sub lbKala_Effect_DoubleClick(sender As Object, e As EventArgs) Handles lbKala_Effect.DoubleClick
        If lbKala_Effect.Items.Count = 0 Then
            Exit Sub
        End If

        strKala_Effect = strKala_Effect.Replace(Str(lbKala_Effect.SelectedValue).Trim & ",", "")

        SearchInListBoxKala(lbKala_Effect, txtSearch_Kala_Effect.Text.Trim, strSelectedFields:=strKala_Effect)
    End Sub
    Private Sub txtSearch_Kala_Effect_TextChanged(sender As Object, e As EventArgs) Handles txtSearch_Kala_Effect.TextChanged
        SearchInListBoxKala(lbKala_Effect, txtSearch_Kala_Effect.Text.Trim, strSelectedFields:=strKala_Effect)
    End Sub
    Private Sub btnAddBrand_Click(sender As Object, e As EventArgs) Handles btnAddBrand.Click
        If lbKala_Brand.Items.Count = 0 Then
            Exit Sub
        End If

        strKala_Brand_Effect = strKala_Brand_Effect.Replace(Str(lbKala_Brand.SelectedValue).Trim & ",", "")
        strKala_Brand_Effect &= Str(lbKala_Brand.SelectedValue).Trim & ","

        SearchInListBoxKala(lbKala_Brand_Effect, txtSearch_Kala_Brand_Effect.Text.Trim, strSelectedFields:=strKala_Brand_Effect)
    End Sub
    Private Sub lbKala_Brand_DoubleClick(sender As Object, e As EventArgs) Handles lbKala_Brand.DoubleClick
        If lbKala_Brand.Items.Count = 0 Then
            Exit Sub
        End If

        strKala_Brand_Effect = strKala_Brand_Effect.Replace(Str(lbKala_Brand.SelectedValue).Trim & ",", "")
        strKala_Brand_Effect &= Str(lbKala_Brand.SelectedValue).Trim & ","

        SearchInListBoxKala(lbKala_Brand_Effect, txtSearch_Kala_Brand_Effect.Text.Trim, strSelectedFields:=strKala_Brand_Effect)
    End Sub
    Private Sub btnRemoveBrand_Click(sender As Object, e As EventArgs) Handles btnRemoveBrand.Click
        If lbKala_Brand_Effect.Items.Count = 0 Then
            Exit Sub
        End If

        strKala_Brand_Effect = strKala_Brand_Effect.Replace(Str(lbKala_Brand_Effect.SelectedValue).Trim & ",", "")

        SearchInListBoxKala(lbKala_Brand_Effect, txtSearch_Kala_Brand_Effect.Text.Trim, strSelectedFields:=strKala_Brand_Effect)
    End Sub
    Private Sub lbKala_Brand_Effect_DoubleClick(sender As Object, e As EventArgs) Handles lbKala_Brand_Effect.DoubleClick
        If lbKala_Brand_Effect.Items.Count = 0 Then
            Exit Sub
        End If

        strKala_Brand_Effect = strKala_Brand_Effect.Replace(Str(lbKala_Brand_Effect.SelectedValue).Trim & ",", "")

        SearchInListBoxKala(lbKala_Brand_Effect, txtSearch_Kala_Brand_Effect.Text.Trim, strSelectedFields:=strKala_Brand_Effect)
    End Sub
    Private Sub txtSearch_Kala_Brand_Effect_TextChanged(sender As Object, e As EventArgs) Handles txtSearch_Kala_Brand_Effect.TextChanged
        SearchInListBoxKala(lbKala_Brand_Effect, txtSearch_Kala_Brand_Effect.Text.Trim, strSelectedFields:=strKala_Brand_Effect)
    End Sub
    Private Sub btnAddGorohKala_Click(sender As Object, e As EventArgs) Handles btnAddGorohKala.Click
        If lbKala_GorohKala.Items.Count = 0 Then
            Exit Sub
        End If

        strKala_GorohKala_Effect = strKala_GorohKala_Effect.Replace(Str(lbKala_GorohKala.SelectedValue).Trim & ",", "")
        strKala_GorohKala_Effect &= Str(lbKala_GorohKala.SelectedValue).Trim & ","

        SearchInListBoxKala(lbKala_GorohKala_Effect, txtSearch_Kala_GorohKala_Effect.Text.Trim, strSelectedFields:=strKala_GorohKala_Effect)
    End Sub
    Private Sub lbKala_GorohKala_DoubleClick(sender As Object, e As EventArgs) Handles lbKala_GorohKala.DoubleClick
        If lbKala_GorohKala.Items.Count = 0 Then
            Exit Sub
        End If

        strKala_GorohKala_Effect = strKala_GorohKala_Effect.Replace(Str(lbKala_GorohKala.SelectedValue).Trim & ",", "")
        strKala_GorohKala_Effect &= Str(lbKala_GorohKala.SelectedValue).Trim & ","

        SearchInListBoxKala(lbKala_GorohKala_Effect, txtSearch_Kala_GorohKala_Effect.Text.Trim, strSelectedFields:=strKala_GorohKala_Effect)
    End Sub
    Private Sub btnRemoveGorohKala_Click(sender As Object, e As EventArgs) Handles btnRemoveGorohKala.Click
        If lbKala_GorohKala_Effect.Items.Count = 0 Then
            Exit Sub
        End If

        strKala_GorohKala_Effect = strKala_GorohKala_Effect.Replace(Str(lbKala_GorohKala_Effect.SelectedValue).Trim & ",", "")

        SearchInListBoxKala(lbKala_GorohKala_Effect, txtSearch_Kala_GorohKala_Effect.Text.Trim, strSelectedFields:=strKala_GorohKala_Effect)
    End Sub
    Private Sub lbKala_GorohKala_Effect_DoubleClick(sender As Object, e As EventArgs) Handles lbKala_GorohKala_Effect.DoubleClick
        If lbKala_GorohKala_Effect.Items.Count = 0 Then
            Exit Sub
        End If

        strKala_GorohKala_Effect = strKala_GorohKala_Effect.Replace(Str(lbKala_GorohKala_Effect.SelectedValue).Trim & ",", "")

        SearchInListBoxKala(lbKala_GorohKala_Effect, txtSearch_Kala_GorohKala_Effect.Text.Trim, strSelectedFields:=strKala_GorohKala_Effect)
    End Sub
    Private Sub txtSearch_Kala_GorohKala_Effect_TextChanged(sender As Object, e As EventArgs) Handles txtSearch_Kala_GorohKala_Effect.TextChanged
        SearchInListBoxKala(lbKala_GorohKala_Effect, txtSearch_Kala_GorohKala_Effect.Text.Trim, strSelectedFields:=strKala_GorohKala_Effect)
    End Sub
    Private Function IsValid(ByVal ModeForm As Integer)
        '' ModeForm :: 0 = Sabte Etelaate Avalieh \ 1 = Dasteh Bandy Moshtarian \ 2 = Dasteh Bandy Kalaei \ 3 = Taeed Nahaei
        IsValid = False
        Try
            If ModeForm = 0 Then
                If cmbNoeAeenNameh.SelectedIndex = 0 Then
                    MsgBox("نوع آیین نامه را مشخص نمایید", MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.OkOnly, "خطا")
                    ErrPro.SetError(cmbNoeAeenNameh, "نوع آیین نامه را مشخص نمایید .")
                    cmbNoeAeenNameh.Focus()
                    Exit Function
                End If
                ErrPro.SetError(cmbNoeAeenNameh, "")

                ''----------------------------------------------------------
                If Me.mskAzTarikh.Text = "" Then
                    ErrPro.SetError(Me.mskAzTarikh, "از تاریخ را وارد کنيد.")
                    MsgBox("از تاریخ را وارد کنيد.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
                    mskAzTarikh.Focus()
                    Exit Function
                ElseIf Not objTarikh.IsShDate(mskAzTarikh.Text.ToString) Then
                    ErrPro.SetError(Me.mskTaTarikh, ".از تاریخ را صحیح وارد کنيد")
                    mskAzTarikh.Focus()
                    Exit Function
                ElseIf Me.mskAzTarikh.Text < TarikhEmrooz Then
                    ErrPro.SetError(Me.mskAzTarikh, "از تاریخ نمی تواند پیش از تاریخ امروز باشد.")
                    MsgBox("از تاریخ نمی تواند پیش از تاریخ امروز باشد.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
                    mskAzTarikh.Focus()
                    Exit Function
                End If
                ErrPro.SetError(Me.mskAzTarikh, "")

                ''----------------------------------------------------------
                If Me.mskTaTarikh.Text = "" Then
                    ErrPro.SetError(Me.mskTaTarikh, "تا تاریخ را وارد کنيد.")
                    MsgBox("تا تاریخ را وارد کنيد.", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
                    mskAzTarikh.Focus()
                    Exit Function
                ElseIf Not objTarikh.IsShDate(mskTaTarikh.Text.ToString) Then
                    ErrPro.SetError(Me.mskTaTarikh, ".تا تاریخ را صحیح وارد کنيد")
                    mskTaTarikh.Focus()
                    Exit Function
                ElseIf mskAzTarikh.Text > mskTaTarikh.Text Then
                    ErrPro.SetError(Me.mskTaTarikh, ".تا تاریخ نباید پیش از شروع آن باشد")
                    mskTaTarikh.Focus()
                    Exit Function
                End If
                ErrPro.SetError(Me.mskTaTarikh, "")

                ''----------------------------------------------------------
                If cmbNoeAzTa.SelectedIndex = 0 Then
                    MsgBox("نوع محاسبـه آیین نامه را مشخص نمایید", MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.OkOnly, "خطا")
                    ErrPro.SetError(cmbNoeAzTa, "نوع محاسبـه آیین نامه را مشخص نمایید .")
                    cmbNoeAzTa.Focus()
                    Exit Function
                End If
                ErrPro.SetError(cmbNoeAzTa, "")

                ''----------------------------------------------------------
                If cmbNoeAzTa.SelectedIndex = 1 Then
                    If cmbNoeBastehBandy.SelectedIndex = 0 Then
                        MsgBox("نوع بسته بندی آیین نامه را مشخص نمایید", MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.OkOnly, "خطا")
                        ErrPro.SetError(cmbNoeBastehBandy, "نوع بسته بندی آیین نامه را مشخص نمایید .")
                        cmbNoeBastehBandy.Focus()
                        Exit Function
                    End If
                    ErrPro.SetError(cmbNoeBastehBandy, "")
                End If

                ''----------------------------------------------------------
                If cmbNoeAzTa.SelectedIndex = 1 Then
                    If txtAz.Text = "" Then
                        MsgBox("از تعداد کالا را وارد کنید.", MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.OkOnly, "خطا")
                        ErrPro.SetError(txtAz, "از تعداد کالا را وارد کنید.")
                        Exit Function
                    End If
                    ErrPro.SetError(txtAz, "")

                    ''----------------------------------------------------------
                    If txtTa.Text = "" Then
                        MsgBox("تا تعداد کالا را وارد کنید .", MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.OkOnly, "خطا")
                        ErrPro.SetError(txtTa, "تا تعداد کالا را وارد کنید .")
                        Exit Function
                    End If
                    ErrPro.SetError(txtTa, "")

                    ''----------------------------------------------------------
                    If cmbNoeTakhfifEhdaei.SelectedIndex <> 2 AndAlso txtBeEzae.Text = "" Then
                        MsgBox("به ازاء تعداد را وارد کنید .", MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.OkOnly, "خطا")
                        ErrPro.SetError(txtBeEzae, "به ازاء تعداد را وارد کنید .")
                        Exit Function
                    End If
                    ErrPro.SetError(txtBeEzae, "")

                ElseIf cmbNoeAzTa.SelectedIndex = 2 Then
                    If txtAz.Text = "" Then
                        MsgBox("از ریــــال را وارد کنید.", MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.OkOnly, "خطا")
                        ErrPro.SetError(txtAz, "از ریــــال را وارد کنید .")
                        Exit Function
                    End If
                    ErrPro.SetError(txtAz, "")

                    ''----------------------------------------------------------
                    If txtTa.Text = "" Then
                        MsgBox("تا ریــــال را وارد کنید .", MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.OkOnly, "خطا")
                        ErrPro.SetError(txtTa, "تا ریــــال را وارد کنید .")
                        Exit Function
                    End If
                    ErrPro.SetError(txtTa, "")
                End If

                If CInt(txtAz.Text) > CInt(txtTa.Text) Then
                    MsgBox("« از » باید کوچکتر از « تا » باشد .", MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "ذخيره")
                    ErrPro.SetError(txtAz, "« از » باید کوچکتر از « تا » باشد .")
                    Exit Function
                End If
                ErrPro.SetError(txtAz, "")

                If cmbNoeTakhfifEhdaei.SelectedIndex = 0 Then
                    MsgBox("نوع تخفیف / جایزه اهدایی را مشخص نمایید .", MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.OkOnly, "خطا")
                    ErrPro.SetError(cmbNoeTakhfifEhdaei, "نوع تخفیف / جایزه اهدایی را مشخص نمایید .")
                    cmbNoeTakhfifEhdaei.Focus()
                    Exit Function
                End If
                ErrPro.SetError(cmbNoeTakhfifEhdaei, "")

                If txtSharh.Text.Trim = "" Then
                    MsgBox("شـــرح را وارد نماییــد .", MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.OkOnly, "خطا")
                    ErrPro.SetError(txtSharh, "شـــرح را وارد نماییــد .")
                    txtSharh.Focus()
                    Exit Function
                End If
                ErrPro.SetError(txtSharh, "")

                If cmbNoeTakhfifEhdaei.SelectedIndex = 1 Then
                    If txtCodeKalaJayezeh.Tag = 0 Then
                        MsgBox("کالای جایزه اهدایی را مشخص نمایید .", MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.OkOnly, "خطا")
                        ErrPro.SetError(txtCodeKalaJayezeh, "کالای جایزه اهدایی را مشخص نمایید .")
                        txtCodeKalaJayezeh.Focus()
                        Exit Function
                    End If
                    ErrPro.SetError(txtCodeKalaJayezeh, "")

                    If cmbNoeBastehBandyJayezeh.SelectedIndex = 0 Then
                        MsgBox("نوع بسته بندی جایزه را مشخص نمایید .", MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.OkOnly, "خطا")
                        ErrPro.SetError(cmbNoeBastehBandyJayezeh, "نوع بسته بندی جایزه را مشخص نمایید .")
                        cmbNoeBastehBandyJayezeh.Focus()
                        Exit Function
                    End If
                    ErrPro.SetError(cmbNoeBastehBandyJayezeh, "")

                    If txtTedadJayezeh.Text = "" Or Val(txtTedadJayezeh.Text) = 0 Then
                        MsgBox("تعداد جایزه را وارد کنید .", MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.OkOnly, "خطا")
                        ErrPro.SetError(txtTedadJayezeh, "تعداد جایزه را وارد کنید .")
                        Exit Function
                    End If
                    ErrPro.SetError(txtTedadJayezeh, "")
                ElseIf cmbNoeTakhfifEhdaei.SelectedIndex = 2 Then
                    If txtDarsadEhdaei.Text = "" Or Val(txtDarsadEhdaei.Text) = 0 Then
                        MsgBox("درصد اهدایی را وارد کنید .", MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.OkOnly, "خطا")
                        ErrPro.SetError(txtDarsadEhdaei, "درصد اهدایی را وارد کنید .")
                        Exit Function
                    End If
                    ErrPro.SetError(txtDarsadEhdaei, "")
                ElseIf cmbNoeTakhfifEhdaei.SelectedIndex = 3 Then
                    If txtRialEhdaei.Text = "" Or Val(txtRialEhdaei.Text) = 0 Then
                        MsgBox("ریـال اهدایی را وارد کنید .", MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.OkOnly, "خطا")
                        ErrPro.SetError(txtRialEhdaei, "ریـال اهدایی را وارد کنید .")
                        Exit Function
                    End If
                    ErrPro.SetError(txtRialEhdaei, "")
                End If

                Dim CountNoePardakht As Integer = 0
                For i As Integer = 0 To chklstNoePardakht.Items.Count - 1
                    If chklstNoePardakht.GetItemChecked(i) = True Then
                        CountNoePardakht += 1
                    End If
                Next

                If CountNoePardakht = 0 Then
                    If MsgBox("حداقل باید یک نوع پرداخت را انتخاب نمایید ، در غیر این صورت آیین نامه برای تمام نوع پرداختهای موجود تعریف خواهد شد ." & vbCrLf & " آیا برای تمام نوع پرداخت ها آیین نامه تعریف گردد ؟", MsgBoxStyle.Question + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.YesNo, "") = MsgBoxResult.Yes Then
                        For i As Integer = 0 To chklstNoePardakht.Items.Count - 1
                            chklstNoePardakht.SetItemChecked(i, True)
                        Next
                    Else
                        ErrPro.SetError(chklstNoePardakht, "حداقل باید یک نوع پرداخت را انتخاب نمایید .")
                        Exit Function
                    End If
                End If
                ErrPro.SetError(chklstNoePardakht, "")

            ElseIf ModeForm = 1 Then
                If cmbDastehBandyMoshtary.SelectedIndex = -1 Or cmbDastehBandyMoshtary.SelectedIndex = 0 Then
                    MsgBox("نوع دسته بندی مشتریان را مشخص نمایید .", MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.OkOnly, "خطا")
                    ErrPro.SetError(cmbDastehBandyMoshtary, "نوع دسته بندی مشتریان را مشخص نمایید .")
                    cmbDastehBandyMoshtary.Focus()
                    Exit Function
                End If
                ErrPro.SetError(cmbDastehBandyMoshtary, "")

                If cmbDastehBandyMoshtary.SelectedIndex = 1 Then
                    If strMoshtary_Valid = "," Then
                        MsgBox("حداقل باید یک مشتری انتخاب نمایید .", MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.OkOnly, "خطا")
                        ErrPro.SetError(lbMoshtary_Valid, "حداقل باید یک مشتری انتخاب نمایید .")
                        lbMoshtary_Valid.Focus()
                        Exit Function
                    End If
                End If
                ErrPro.SetError(lbMoshtary_Valid, "")

                If cmbDastehBandyMoshtary.SelectedIndex = 2 Then
                    If cmbNoeMoshtary.SelectedIndex = -1 Then
                        MsgBox("نوع مشتری را انتخاب نمایید .", MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.OkOnly, "خطا")
                        ErrPro.SetError(cmbNoeMoshtary, "نوع مشتری را انتخاب نمایید .")
                        cmbNoeMoshtary.Focus()
                        Exit Function
                    End If
                    ErrPro.SetError(cmbNoeMoshtary, "")

                    If strMoshtary_NoeMoshtary_Valid = "," Then
                        If MsgBox("مشتری انتخاب نشده است . آیین نامه تعریف شده برای نوع مشتری « " & cmbNoeMoshtary.Text & " » ثبت خواهد شد . آیا ثبت انجام شود ؟", MsgBoxStyle.Question + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.YesNo, "") = MsgBoxResult.No Then
                            ErrPro.SetError(lbMoshtary_NoeMoshtary, "مشتری انتخاب نمایید .")
                            lbMoshtary_NoeMoshtary.Focus()
                            Exit Function
                        End If
                    End If
                End If
                ErrPro.SetError(lbMoshtary_NoeMoshtary, "")

                If cmbDastehBandyMoshtary.SelectedIndex = 3 Then
                    If cmbNoeSenf.SelectedIndex = -1 Then
                        MsgBox("نوع صنف را انتخاب نمایید .", MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.OkOnly, "خطا")
                        ErrPro.SetError(cmbNoeSenf, "نوع صنف را انتخاب نمایید .")
                        cmbNoeSenf.Focus()
                        Exit Function
                    End If
                    ErrPro.SetError(cmbNoeSenf, "")

                    If strMoshtary_NoeSenf_Valid = "," Then
                        If MsgBox("مشتری انتخاب نشده است . آیین نامه تعریف شده برای نوع صنف « " & cmbNoeSenf.Text & " » ثبت خواهد شد . آیا ثبت انجام شود ؟", MsgBoxStyle.Question + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.YesNo, "") = MsgBoxResult.No Then
                            ErrPro.SetError(lbMoshtary_NoeSenf, "مشتری انتخاب نمایید .")
                            lbMoshtary_NoeSenf.Focus()
                            Exit Function
                        End If
                    End If
                End If
                ErrPro.SetError(lbMoshtary_NoeSenf, "")

                If cmbDastehBandyMoshtary.SelectedIndex = 4 Then
                    If cmbOstan.SelectedIndex = -1 Then
                        MsgBox("استان را انتخاب نمایید .", MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.OkOnly, "خطا")
                        ErrPro.SetError(cmbOstan, "استان را انتخاب نمایید .")
                        cmbOstan.Focus()
                        Exit Function
                    End If
                    ErrPro.SetError(cmbOstan, "")

                    If chkMahaleh.Checked = True Then
                        If cmbMahaleh.SelectedIndex = -1 Then
                            MsgBox("محلـه را انتخاب نمایید .", MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.OkOnly, "خطا")
                            ErrPro.SetError(cmbMahaleh, "محلـه را انتخاب نمایید .")
                            cmbMahaleh.Focus()
                            Exit Function
                        End If
                        ErrPro.SetError(cmbMahaleh, "")

                        If strMoshtary_Mantagheh_Valid = "," Then
                            If MsgBox("مشتری انتخاب نشده است . آیین نامه تعریف شده برای محله « " & cmbMahaleh.Text & " » ثبت خواهد شد . آیا ثبت انجام شود ؟", MsgBoxStyle.Question + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.YesNo, "") = MsgBoxResult.No Then
                                ErrPro.SetError(lbMoshtary_Mantagheh, "مشتری انتخاب نمایید .")
                                lbMoshtary_Mantagheh.Focus()
                                Exit Function
                            End If
                        End If
                        ErrPro.SetError(lbMoshtary_Mantagheh, "")

                    ElseIf chkMahaleh.Checked = False AndAlso chkMantagheh.Checked = True Then
                        If cmbMantagheh.SelectedIndex = -1 Then
                            MsgBox("منطقـه را انتخاب نمایید .", MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.OkOnly, "خطا")
                            ErrPro.SetError(cmbMantagheh, "منطقـه را انتخاب نمایید .")
                            cmbMantagheh.Focus()
                            Exit Function
                        End If
                        ErrPro.SetError(cmbMantagheh, "")

                        If strMoshtary_Mantagheh_Valid = "," Then
                            If MsgBox("مشتری انتخاب نشده است . آیین نامه تعریف شده برای منطقـه « " & cmbMantagheh.Text & " » ثبت خواهد شد . آیا ثبت انجام شود ؟", MsgBoxStyle.Question + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.YesNo, "") = MsgBoxResult.No Then
                                ErrPro.SetError(lbMoshtary_Mantagheh, "مشتری انتخاب نمایید .")
                                lbMoshtary_Mantagheh.Focus()
                                Exit Function
                            End If
                        End If
                        ErrPro.SetError(lbMoshtary_Mantagheh, "")

                    ElseIf chkMantagheh.Checked = False AndAlso chkShahr.Checked = True Then
                        If cmbShahr.SelectedIndex = -1 Then
                            MsgBox("شهـر را انتخاب نمایید .", MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.OkOnly, "خطا")
                            ErrPro.SetError(cmbShahr, "شهـر را انتخاب نمایید .")
                            cmbShahr.Focus()
                            Exit Function
                        End If
                        ErrPro.SetError(cmbShahr, "")

                        If strMoshtary_Mantagheh_Valid = "," Then
                            If MsgBox("مشتری انتخاب نشده است . آیین نامه تعریف شده برای شهـر « " & cmbShahr.Text & " » ثبت خواهد شد . آیا ثبت انجام شود ؟", MsgBoxStyle.Question + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.YesNo, "") = MsgBoxResult.No Then
                                ErrPro.SetError(lbMoshtary_Mantagheh, "مشتری انتخاب نمایید .")
                                lbMoshtary_Mantagheh.Focus()
                                Exit Function
                            End If
                        End If
                        ErrPro.SetError(lbMoshtary_Mantagheh, "")

                    Else
                        If strMoshtary_Mantagheh_Valid = "," Then
                            If MsgBox("مشتری انتخاب نشده است . آیین نامه تعریف شده برای استـان « " & cmbOstan.Text & " » ثبت خواهد شد . آیا ثبت انجام شود ؟", MsgBoxStyle.Question + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.YesNo, "") = MsgBoxResult.No Then
                                ErrPro.SetError(lbMoshtary_Mantagheh, "مشتری انتخاب نمایید .")
                                lbMoshtary_Mantagheh.Focus()
                                Exit Function
                            End If
                        End If
                        ErrPro.SetError(lbMoshtary_Mantagheh, "")
                    End If
                End If

                If cmbDastehBandyMoshtary.SelectedIndex = 5 Then
                    If cmbNoeFieldNotEffectMoshtary.SelectedIndex = 0 Then
                        If MsgBox("مشتری انتخاب نشده است . آیین نامه تعریف شده برای تمام مشتریان ثبت خواهد شد . آیا ثبت انجام شود ؟", MsgBoxStyle.Question + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.YesNo, "") = MsgBoxResult.No Then
                            ErrPro.SetError(cmbNoeFieldNotEffectMoshtary, "نوع را انتخاب نمایید .")
                            cmbNoeFieldNotEffectMoshtary.Focus()
                            Exit Function
                        End If
                    End If
                    ErrPro.SetError(cmbNoeFieldNotEffectMoshtary, "")

                    If cmbNoeFieldNotEffectMoshtary.SelectedIndex = 1 Then
                        If strMoshtary_NotEffect = "," Then
                            MsgBox("حداقل باید یک مشتری انتخاب نمایید .", MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.OkOnly, "خطا")
                            ErrPro.SetError(lbMoshtary_NotEffect, "حداقل باید یک مشتری انتخاب نمایید .")
                            lbMoshtary_NotEffect.Focus()
                            Exit Function
                        End If
                    End If
                    ErrPro.SetError(lbMoshtary_NotEffect, "")

                    If cmbNoeFieldNotEffectMoshtary.SelectedIndex = 2 Then
                        If strNoeMoshtary_NotEffect = "," Then
                            MsgBox("حداقل باید یک نوع مشتری انتخاب نمایید .", MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.OkOnly, "خطا")
                            ErrPro.SetError(lbNoeMoshtary_NotEffect, "حداقل باید یک نوع مشتری انتخاب نمایید .")
                            lbNoeMoshtary_NotEffect.Focus()
                            Exit Function
                        End If
                    End If
                    ErrPro.SetError(lbNoeMoshtary_NotEffect, "")

                    If cmbNoeFieldNotEffectMoshtary.SelectedIndex = 3 Then
                        If strNoeSenf_NotEffect = "," Then
                            MsgBox("حداقل باید یک نوع صنف انتخاب نمایید .", MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.OkOnly, "خطا")
                            ErrPro.SetError(lbNoeSenf_NotEffect, "حداقل باید یک نوع صنف انتخاب نمایید .")
                            lbNoeSenf_NotEffect.Focus()
                            Exit Function
                        End If
                    End If
                    ErrPro.SetError(lbNoeSenf_NotEffect, "")

                    If cmbNoeFieldNotEffectMoshtary.SelectedIndex = 4 Then
                        If strManategh_NotEffect = "," Then
                            MsgBox("حداقل باید یک استان، شهر، منطقه و یا محله انتخاب نمایید .", MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.OkOnly, "خطا")
                            ErrPro.SetError(lbManategh_NotEffect, "حداقل باید یک استان، شهر، منطقه و یا محله انتخاب نمایید .")
                            lbManategh_NotEffect.Focus()
                            Exit Function
                        End If
                    End If
                    ErrPro.SetError(lbManategh_NotEffect, "")
                End If
            ElseIf ModeForm = 2 Then

                If cmbNoeFieldJayezeh.SelectedIndex = -1 Or cmbNoeFieldJayezeh.SelectedIndex = 0 Then
                    MsgBox("نوع دسته بندی کالایی جهت محاسبه آیین نامه را مشخص نمایید .", MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.OkOnly, "خطا")
                    ErrPro.SetError(cmbNoeFieldJayezeh, "نوع دسته بندی کالایی جهت محاسبه آیین نامه را مشخص نمایید .")
                    cmbNoeFieldJayezeh.Focus()
                    Exit Function
                End If
                ErrPro.SetError(cmbNoeFieldJayezeh, "")

                If ((cmbNoeFieldJayezeh.SelectedIndex = 1 Or cmbNoeFieldJayezeh.SelectedIndex = 4 Or cmbNoeFieldJayezeh.SelectedIndex = 5) And cmbNoeBastehBandy.SelectedIndex = 4) AndAlso (cmbNoeMohasebeh.SelectedIndex <> 1) Then
                    If cmbNoeFieldJayezeh.SelectedIndex = 1 Then
                        MsgBox("نوع بسته بندی « سطر کالا » برای یک کالای خاص نمی تواند تعریف شود، یا نوع محاسبه و یا نوع بسته بندی را تغییر دهید  .", MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.OkOnly, "خطا")
                        ErrPro.SetError(cmbNoeFieldJayezeh, "نوع بسته بندی « سطر کالا » برای یک کالای خاص نمی تواند تعریف شود، یا نوع محاسبه و یا نوع بسته بندی را تغییر دهید  .")
                    ElseIf cmbNoeFieldJayezeh.SelectedIndex = 4 Then
                        MsgBox("نوع بسته بندی « سطر کالا » برای مبلغ خالص فاکتور نمی تواند تعریف شود، یا نوع محاسبه و یا نوع بسته بندی را تغییر دهید  .", MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.OkOnly, "خطا")
                        ErrPro.SetError(cmbNoeFieldJayezeh, "نوع بسته بندی « سطر کالا » برای مبلغ خالص فاکتور نمی تواند تعریف شود، یا نوع محاسبه و یا نوع بسته بندی را تغییر دهید  .")
                    ElseIf cmbNoeFieldJayezeh.SelectedIndex = 5 Then
                        MsgBox("نوع بسته بندی « سطر کالا » برای مبلغ نا خالص فاکتور نمی تواند تعریف شود، یا نوع محاسبه و یا نوع بسته بندی را تغییر دهید  .", MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.OkOnly, "خطا")
                        ErrPro.SetError(cmbNoeFieldJayezeh, "نوع بسته بندی « سطر کالا » برای مبلغ نا خالص فاکتور نمی تواند تعریف شود، یا نوع محاسبه و یا نوع بسته بندی را تغییر دهید  .")
                    End If
                    cmbNoeFieldJayezeh.Focus()
                    Exit Function
                End If
                ErrPro.SetError(cmbNoeFieldJayezeh, "")

                If ((cmbNoeFieldJayezeh.SelectedIndex = 2 Or cmbNoeFieldJayezeh.SelectedIndex = 3) And cmbNoeBastehBandy.SelectedIndex = 4) Then
                    If cmbNoeFieldJayezeh.SelectedIndex = 2 Then
                        If strKala_Brand_Effect <> "," Then
                            MsgBox("در نوع بسته بندی « سطر کالا » باید تنها یک برند انتخاب نمایید و نمی توانید کالای خاصی از یک برند را انتخاب نمایید  .", MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.OkOnly, "خطا")
                            ErrPro.SetError(cmbNoeFieldJayezeh, "در نوع بسته بندی « سطر کالا » باید تنها یک برند انتخاب نمایید و نمی توانید کالای خاصی از یک برند را انتخاب نمایید  .")
                            cmbNoeFieldJayezeh.Focus()
                            Exit Function
                        End If
                    ElseIf cmbNoeFieldJayezeh.SelectedIndex = 3 Then
                        If strKala_GorohKala_Effect <> "," Then
                            MsgBox("در نوع بسته بندی « سطر کالا » باید تنها یک گروه کالا انتخاب نمایید و نمی توانید کالای خاصی از یک برند را انتخاب نمایید  .", MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.OkOnly, "خطا")
                            ErrPro.SetError(cmbNoeFieldJayezeh, "در نوع بسته بندی « سطر کالا » باید تنها یک گروه کالا انتخاب نمایید و نمی توانید کالای خاصی از یک برند را انتخاب نمایید  .")
                            cmbNoeFieldJayezeh.Focus()
                            Exit Function
                        End If
                    End If
                End If
                ErrPro.SetError(cmbNoeFieldJayezeh, "")

                If cmbNoeFieldJayezeh.SelectedIndex = 1 Then
                    If strKala_Effect = "," Then
                        MsgBox("حداقل باید یک کالا انتخاب نمایید .", MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.OkOnly, "خطا")
                        ErrPro.SetError(lbKala_Effect, "حداقل باید یک کالا انتخاب نمایید .")
                        lbKala_Effect.Focus()
                        Exit Function
                    End If
                End If
                ErrPro.SetError(lbKala_Effect, "")

                If cmbNoeFieldJayezeh.SelectedIndex = 2 Then
                    If cmbBrand.SelectedIndex = -1 Then
                        MsgBox("برند را انتخاب نمایید .", MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.OkOnly, "خطا")
                        ErrPro.SetError(cmbBrand, "برند را انتخاب نمایید .")
                        cmbBrand.Focus()
                        Exit Function
                    End If
                    ErrPro.SetError(cmbBrand, "")

                    If strKala_Brand_Effect = "," Then
                        If MsgBox("کالا انتخاب نشده است . آیین نامه تعریف شده برای برند « " & cmbBrand.Text & " » ثبت خواهد شد . آیا ثبت انجام شود ؟", MsgBoxStyle.Question + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.YesNo, "") = MsgBoxResult.No Then
                            ErrPro.SetError(lbKala_Brand, "کالا انتخاب نمایید .")
                            lbKala_Brand.Focus()
                            Exit Function
                        End If
                    End If
                    ErrPro.SetError(lbKala_Brand, "")

                End If

                If cmbNoeFieldJayezeh.SelectedIndex = 3 Then
                    If cmbG1.SelectedIndex = -1 Then
                        MsgBox("گروه 1 کالا را انتخاب نمایید .", MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.OkOnly, "خطا")
                        ErrPro.SetError(cmbG1, "گروه 1 کالا را انتخاب نمایید .")
                        cmbG1.Focus()
                        Exit Function
                    End If
                    ErrPro.SetError(cmbG1, "")

                    If chkG5.Checked = True Then
                        If cmbG5.SelectedIndex = -1 Then
                            MsgBox("گروه 5 کالا را انتخاب نمایید .", MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.OkOnly, "خطا")
                            ErrPro.SetError(cmbG5, "گروه 5 کالا را انتخاب نمایید .")
                            cmbG5.Focus()
                            Exit Function
                        End If
                        ErrPro.SetError(cmbG5, "")

                        If strKala_GorohKala_Effect = "," Then
                            If MsgBox("کالا انتخاب نشده است . آیین نامه تعریف شده برای گروه 5 کالا « " & cmbG5.Text & " » ثبت خواهد شد . آیا ثبت انجام شود ؟", MsgBoxStyle.Question + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.YesNo, "") = MsgBoxResult.No Then
                                ErrPro.SetError(lbKala_GorohKala, "کالا انتخاب نمایید .")
                                lbKala_GorohKala.Focus()
                                Exit Function
                            End If
                        End If
                        ErrPro.SetError(lbKala_GorohKala, "")

                    ElseIf chkG4.Checked = True Then
                        If cmbG4.SelectedIndex = -1 Then
                            MsgBox("گروه 4 کالا را انتخاب نمایید .", MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.OkOnly, "خطا")
                            ErrPro.SetError(cmbG4, "گروه 4 کالا را انتخاب نمایید .")
                            cmbG4.Focus()
                            Exit Function
                        End If
                        ErrPro.SetError(cmbG4, "")

                        If strKala_GorohKala_Effect = "," Then
                            If MsgBox("کالا انتخاب نشده است . آیین نامه تعریف شده برای گروه 4 کالا « " & cmbG4.Text & " » ثبت خواهد شد . آیا ثبت انجام شود ؟", MsgBoxStyle.Question + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.YesNo, "") = MsgBoxResult.No Then
                                ErrPro.SetError(lbKala_GorohKala, "کالا انتخاب نمایید .")
                                lbKala_GorohKala.Focus()
                                Exit Function
                            End If
                        End If
                        ErrPro.SetError(lbKala_GorohKala, "")

                    ElseIf chkG3.Checked = True Then
                        If cmbG3.SelectedIndex = -1 Then
                            MsgBox("گروه 3 کالا را انتخاب نمایید .", MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.OkOnly, "خطا")
                            ErrPro.SetError(cmbG3, "گروه 3 کالا را انتخاب نمایید .")
                            cmbG3.Focus()
                            Exit Function
                        End If
                        ErrPro.SetError(cmbG3, "")

                        If strKala_GorohKala_Effect = "," Then
                            If MsgBox("کالا انتخاب نشده است . آیین نامه تعریف شده برای گروه 3 کالا « " & cmbG3.Text & " » ثبت خواهد شد . آیا ثبت انجام شود ؟", MsgBoxStyle.Question + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.YesNo, "") = MsgBoxResult.No Then
                                ErrPro.SetError(lbKala_GorohKala, "کالا انتخاب نمایید .")
                                lbKala_GorohKala.Focus()
                                Exit Function
                            End If
                        End If
                        ErrPro.SetError(lbKala_GorohKala, "")

                    ElseIf chkG2.Checked = True Then
                        If cmbG2.SelectedIndex = -1 Then
                            MsgBox("گروه 2 کالا را انتخاب نمایید .", MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.OkOnly, "خطا")
                            ErrPro.SetError(cmbG2, "گروه 2 کالا را انتخاب نمایید .")
                            cmbG2.Focus()
                            Exit Function
                        End If
                        ErrPro.SetError(cmbG2, "")

                        If strKala_GorohKala_Effect = "," Then
                            If MsgBox("کالا انتخاب نشده است . آیین نامه تعریف شده برای گروه 2 کالا « " & cmbG2.Text & " » ثبت خواهد شد . آیا ثبت انجام شود ؟", MsgBoxStyle.Question + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.YesNo, "") = MsgBoxResult.No Then
                                ErrPro.SetError(lbKala_GorohKala, "کالا انتخاب نمایید .")
                                lbKala_GorohKala.Focus()
                                Exit Function
                            End If
                        End If
                        ErrPro.SetError(lbKala_GorohKala, "")
                    Else
                        If strKala_GorohKala_Effect = "," Then
                            If MsgBox("کالا انتخاب نشده است . آیین نامه تعریف شده برای گروه 1 کالا « " & cmbG1.Text & " » ثبت خواهد شد . آیا ثبت انجام شود ؟", MsgBoxStyle.Question + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.YesNo, "") = MsgBoxResult.No Then
                                ErrPro.SetError(lbKala_GorohKala, "کالا انتخاب نمایید .")
                                lbKala_GorohKala.Focus()
                                Exit Function
                            End If
                        End If
                        ErrPro.SetError(lbKala_GorohKala, "")
                    End If
                End If
                ' MakazPakhsh
            ElseIf ModeForm = 10 Then


                Dim CountMahal As Integer = 0
                For i As Integer = 0 To chlMarkazPakhsh.Items.Count - 1
                    If chlMarkazPakhsh.GetItemChecked(i) = True Then
                        CountMahal += 1
                    End If
                Next


                If CountMahal = 0 Then
                    If MsgBox("حداقل باید یک مرکزپخش را انتخاب نمایید ، در غیر این صورت آیین نامه برای تمام مرکزپخش‌های موجود تعریف خواهد شد ." & vbCrLf & " آیا برای تمام مراکزپخش آیین نامه تعریف گردد ؟", MsgBoxStyle.Question + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.YesNo, "") = MsgBoxResult.Yes Then
                        For i As Integer = 0 To chlMarkazPakhsh.Items.Count - 1
                            chlMarkazPakhsh.SetItemChecked(i, True)
                        Next
                    Else
                        ErrPro.SetError(chlMarkazPakhsh, "حداقل باید یک مرکزپخش را انتخاب نمایید .")
                        Exit Function
                    End If
                End If
                ErrPro.SetError(chlMarkazPakhsh, "")


            End If
               

            Return True
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "Error IN DataBase ----> IsValid")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, "Error IN ----> IsValid")
        End Try
    End Function
    Private Sub SetFormTaeedNahaei()
        ShowDetails_PrimaryInfo()
        ShowDetails_TypeMoshtary()
        ShowDetails_TypeKala()
    End Sub
    Private Sub ShowDetails_PrimaryInfo()
        txtShowDetails_PrimaryInfo.Text = cmbNoeAeenNameh.Text.Trim + " - از تاریخ " + objTarikh.SetDateSlash(mskAzTarikh.Text.Trim) + " تا تاریخ  " + objTarikh.SetDateSlash(mskTaTarikh.Text.Trim)
        txtShowDetails_PrimaryInfo.Text &= " -  از " + objCode.DigitSeprator(txtAz.Text.Trim) + " " + lblType1.Text.Trim + " تا " + objCode.DigitSeprator(txtTa.Text.Trim) + " " + lblType2.Text.Trim

        If cmbNoeAzTa.SelectedIndex = 1 Then
            txtShowDetails_PrimaryInfo.Text &= " به ازای هر " + objCode.DigitSeprator(txtBeEzae.Text.Trim) + " " + lblType3.Text.Trim
        End If

        If cmbNoeAeenNameh.SelectedIndex = 1 Then
            txtShowDetails_PrimaryInfo.Text &= " - برای نوع پرداخت های : " + ShowNoePardakht()
        End If

        If cmbNoeTakhfifEhdaei.SelectedIndex = 1 Then
            txtShowDetails_PrimaryInfo.Text &= " - " + txtTedadJayezeh.Text.Trim + " " + lblType4.Text.Trim + " جایزه"
            txtShowDetails_PrimaryInfo.Text &= " از کد کالای " + txtCodeKalaJayezeh.Text.Trim + " با نام " + lblNameKalaJayezeh.Text.Trim + " تعلق می گیرد ."
        ElseIf cmbNoeTakhfifEhdaei.SelectedIndex = 2 Then
            txtShowDetails_PrimaryInfo.Text &= " - " + txtDarsadEhdaei.Text.Trim + " درصد " + IIf(cmbNoeAeenNameh.SelectedIndex = 1, "تخفیف", "جایـزه") + " تعلق می گیرد ."
        ElseIf cmbNoeTakhfifEhdaei.SelectedIndex = 3 Then
            txtShowDetails_PrimaryInfo.Text &= " - " + objCode.DigitSeprator(txtRialEhdaei.Text.Trim) + " ریـال " + IIf(cmbNoeAeenNameh.SelectedIndex = 1, "تخفیف", "جایـزه") + " تعلق می گیرد ."
        End If
    End Sub
    Private Function ShowNoePardakht() As String
        ShowNoePardakht = ""
        Try
            Dim strNoePardakht As String = ""
            For i As Integer = 0 To chklstNoePardakht.Items.Count - 1
                If chklstNoePardakht.GetItemChecked(i) = True Then
                    chklstNoePardakht.SelectedIndex = i
                    strNoePardakht &= chklstNoePardakht.Text + " ، "
                End If
            Next

            strNoePardakht = strNoePardakht.Substring(0, strNoePardakht.Length - 2)

            Return strNoePardakht
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, "Error IN DataBase ----> IsValid")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, "Error IN ----> IsValid")
        End Try

    End Function
    Private Sub txtCodeKalaJayezeh_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtCodeKalaJayezeh.KeyPress
        Try
            If (Asc(e.KeyChar()) < 48 Or Asc(e.KeyChar()) > 57) And (Asc(e.KeyChar()) <> 8) Then
                e.Handled = True
            End If

            If e.KeyChar = Chr(Keys.Space) Then
                Dim objKala As New Forms_dll.frmAN_KalaSearch
                Dim StrSql As String

                StrSql = "Select  CodeKala,NameKala,ccKala,txtsVahedeShomaresh,sVahedeShomaresh,NameBrand,RadifBrand from qryAN_Kala Where Faal=1 "
                StrSql &= "order by RadifBrand,txtsG1,CodeKala"

                If txtCodeKalaJayezeh.Text.Length <> 0 Then
                    objKala.tcodeKala = sender.Text
                End If

                MultiSelection = False
                SearchItem = "CodeKala"
                objKala.SetForm(StrSql)
                objKala.ShowDialog()

                sender.Tag = objKala.tccKala
                sender.Text = objKala.tcodeKala
                If sender.name = txtCodeKalaJayezeh.Name Then
                    lblNameKalaJayezeh.Text = objKala.tNameKala
                Else
                    lblNameKalaJayezeh.Text = objKala.tNameKala
                End If

                MultiSelection = False
            End If
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ---->txtCodeMoshtaryS_KeyPress")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ---->txtCodeMoshtaryS_KeyPress")
        End Try
    End Sub
    Private Sub txtCodeKalaJayezeh_TextChanged(sender As Object, e As EventArgs) Handles txtCodeKalaJayezeh.TextChanged
        If Trim(sender.Text) = "" Then
            If sender.name = txtCodeKalaJayezeh.Name Then
                Me.lblNameKalaJayezeh.Text = ""
            Else
                Me.lblNameKalaJayezeh.Text = ""
            End If

            sender.Tag = 0
            Exit Sub
        End If

        If sender.name = txtCodeKalaJayezeh.Name Then
            Me.lblNameKalaJayezeh.Text = objTools.ConvertNulls(objTools.DLookup("NameKala", "tblAN_Kala", "CodeKala = '" & sender.Text & "'"), "")
        Else
            Me.lblNameKalaJayezeh.Text = objTools.ConvertNulls(objTools.DLookup("NameKala", "tblAN_Kala", "CodeKala = '" & sender.Text & "'"), "")
        End If

        sender.Tag = objTools.ConvertNulls(objTools.DLookup("ccKala", "tblAn_Kala", "CodeKala = '" & sender.Text & "'"), 0)
    End Sub
    Private Sub ShowDetails_TypeMoshtary()
        If cmbDastehBandyMoshtary.SelectedIndex = 1 Then
            txtShowDetails_TypeMoshtary.Text = " به مشتریان : " + ReadDetails(strMoshtary_Valid, 1)
        ElseIf cmbDastehBandyMoshtary.SelectedIndex = 2 Then
            txtShowDetails_TypeMoshtary.Text = "به نوع مشتری : " + cmbNoeMoshtary.Text

            If strMoshtary_NoeMoshtary_Valid <> "," Then
                txtShowDetails_TypeMoshtary.Text &= " به جز مشتریان : " + ReadDetails(strMoshtary_NoeMoshtary_Valid, 1)
            End If
        ElseIf cmbDastehBandyMoshtary.SelectedIndex = 3 Then
            txtShowDetails_TypeMoshtary.Text = "به نوع صنف : " + cmbNoeSenf.Text

            If strMoshtary_NoeSenf_Valid <> "," Then
                txtShowDetails_TypeMoshtary.Text &= " به جز مشتریان : " + ReadDetails(strMoshtary_NoeSenf_Valid, 1)
            End If
        ElseIf cmbDastehBandyMoshtary.SelectedIndex = 4 Then
            txtShowDetails_TypeMoshtary.Text = "به استـان : " + cmbOstan.Text

            If chkShahr.Checked = True Then
                If cmbShahr.SelectedIndex <> -1 Then
                    txtShowDetails_TypeMoshtary.Text &= " به شهـر : " + cmbShahr.Text
                End If
            End If

            If chkMantagheh.Checked = True Then
                If cmbMantagheh.SelectedIndex <> -1 Then
                    txtShowDetails_TypeMoshtary.Text &= " به منطقـه : " + cmbMantagheh.Text
                End If
            End If

            If chkMahaleh.Checked = True Then
                If cmbMahaleh.SelectedIndex <> -1 Then
                    txtShowDetails_TypeMoshtary.Text &= " به محلـه : " + cmbMahaleh.Text
                End If
            End If

            If strMoshtary_Mantagheh_Valid <> "," Then
                txtShowDetails_TypeMoshtary.Text &= " به جز مشتریان : " + ReadDetails(strMoshtary_Mantagheh_Valid, 1)
            End If
        ElseIf cmbDastehBandyMoshtary.SelectedIndex = 5 Then
            txtShowDetails_TypeMoshtary.Text = "به همـه مشتـریان "

            If cmbNoeFieldNotEffectMoshtary.SelectedIndex = 1 Then
                txtShowDetails_TypeMoshtary.Text &= " به جز مشتریان : " + ReadDetails(strMoshtary_NotEffect, 1)
            End If

            If cmbNoeFieldNotEffectMoshtary.SelectedIndex = 2 Then
                txtShowDetails_TypeMoshtary.Text &= " به جز نوع مشتریان : " + ReadDetails(strNoeMoshtary_NotEffect, 2)
            End If

            If cmbNoeFieldNotEffectMoshtary.SelectedIndex = 3 Then
                txtShowDetails_TypeMoshtary.Text &= " به جز نوع صنف های : " + ReadDetails(strNoeSenf_NotEffect, 3)
            End If

            If cmbNoeFieldNotEffectMoshtary.SelectedIndex = 4 Then
                If chkShahr_Effect.Checked = False Then
                    txtShowDetails_TypeMoshtary.Text &= " به جز استان های : " + ReadDetails(strManategh_NotEffect, 4)
                ElseIf chkMantagheh_Effect.Checked = False Then
                    txtShowDetails_TypeMoshtary.Text &= " به جز شهـر های : " + ReadDetails(strManategh_NotEffect, 4)
                ElseIf chkMahaleh_Effect.Checked = False Then
                    txtShowDetails_TypeMoshtary.Text &= " به جز مناطق : " + ReadDetails(strManategh_NotEffect, 4)
                Else
                    txtShowDetails_TypeMoshtary.Text &= " به جز محلـه های : " + ReadDetails(strManategh_NotEffect, 4)
                End If
            End If
        End If
    End Sub
    Private Sub ShowDetails_TypeKala()
        If cmbNoeFieldJayezeh.SelectedIndex = 1 Then
            If cmbNoeMohasebeh.SelectedIndex <> 1 Then
                txtShowDetails_TypeKala.Text = " به کالا های : " + ReadDetails(strKala_Effect, 5)
            Else
                txtShowDetails_TypeKala.Text = " به ترکیبی از کالا های : " + ReadDetails(strKala_Effect, 5)
            End If
        End If

        If cmbNoeFieldJayezeh.SelectedIndex = 2 Then
            If strKala_Brand_Effect = "," Then
                txtShowDetails_TypeKala.Text = " به برنـد : " + cmbBrand.Text
            Else
                If chkNotEffectKala_Brand.Checked = False Then
                    txtShowDetails_TypeKala.Text = " به ترکیب کالاهای : " + ReadDetails(strKala_Brand_Effect, 5)
                    txtShowDetails_TypeKala.Text &= vbNewLine + " از برنـد " + cmbBrand.Text
                Else
                    txtShowDetails_TypeKala.Text = " به برنـد " + cmbBrand.Text + vbNewLine
                    txtShowDetails_TypeKala.Text &= " به جـز کالاهای : " + ReadDetails(strKala_Brand_Effect, 5)
                End If
            End If
        End If

        If cmbNoeFieldJayezeh.SelectedIndex = 3 Then
            txtShowDetails_TypeKala.Text = ""

            If strKala_GorohKala_Effect <> "," Then
                If chkNotEffectKala_GorohKala.Checked = False Then
                    txtShowDetails_TypeKala.Text = " به ترکیب کالاهای : " + ReadDetails(strKala_GorohKala_Effect, 5)
                    txtShowDetails_TypeKala.Text &= vbNewLine + " از"
                Else
                    txtShowDetails_TypeKala.Text = " به "
                End If
            End If

            txtShowDetails_TypeKala.Text &= " گروه 1 کالا : " + cmbG1.Text
            If chkG2.Checked = True Then
                If cmbG2.SelectedIndex <> -1 Then
                    txtShowDetails_TypeKala.Text &= " - گروه 2 کالا : " + cmbG2.Text
                    If chkG3.Checked = True Then
                        If cmbG3.SelectedIndex <> -1 Then
                            txtShowDetails_TypeKala.Text &= " - گروه 3 کالا : " + cmbG3.Text
                            If chkG4.Checked = True Then
                                If cmbG4.SelectedIndex <> -1 Then
                                    txtShowDetails_TypeKala.Text &= " - گروه 4 کالا : " + cmbG4.Text
                                    If chkG5.Checked = True Then
                                        If cmbG5.SelectedIndex <> -1 Then
                                            txtShowDetails_TypeKala.Text &= " - گروه 5 کالا : " + cmbG5.Text
                                        End If
                                    End If
                                End If
                            End If
                        End If
                    End If
                End If
            End If

            txtShowDetails_TypeKala.Text &= vbNewLine

            If strKala_GorohKala_Effect <> "," Then
                If chkNotEffectKala_GorohKala.Checked <> False Then
                    txtShowDetails_TypeKala.Text &= " به جـز کالاهای : " + ReadDetails(strKala_GorohKala_Effect, 5)
                End If
            End If
        End If

        If cmbNoeFieldJayezeh.SelectedIndex = 4 Then
            txtShowDetails_TypeKala.Text = " به ریـال خالص فاکتور "
        End If

        If cmbNoeFieldJayezeh.SelectedIndex = 5 Then
            txtShowDetails_TypeKala.Text = " به ریـال ناخالص فاکتور "
        End If
    End Sub
    Private Function ReadDetails(ByVal strDetails As String, ByVal Type As Integer) As String
        '' Type ---> 1 : Moshtary / 2 : NoeMoshtary / 3 : NoeSenf / 4 : Mantagheh / 5 : Kala
        ReadDetails = ""

        Dim cnSQL As New SqlConnection
        Dim cmSQL As New SqlCommand
        Dim daSQL As New SqlDataAdapter
        Dim strSQL As String = ""

        If dsForm.Tables.Contains("tblDetails") Then
            dsForm.Tables.Remove("tblDetails")
        End If

        Try
            strSQL = "Sales.spTakhfifJayezehTarkibi_ShowDetails "

            cnSQL = New SqlConnection(ConnectionString)
            cnSQL.Open()

            cmSQL = New SqlCommand(strSQL, cnSQL)
            cmSQL.CommandType = CommandType.StoredProcedure
            cmSQL.Parameters.Clear()

            cmSQL.Parameters.AddWithValue("strDetails", strDetails)
            cmSQL.Parameters.AddWithValue("Type", Type)

            daSQL = New SqlDataAdapter(cmSQL)
            daSQL.Fill(dsForm, "tblDetails")

            For Each dr In dsForm.Tables("tblDetails").DefaultView
                ReadDetails &= dr("Result")
            Next

            cmSQL = Nothing
            cnSQL.Close()

            Return ReadDetails
        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> ReadDetails ")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> ReadDetails ")
        End Try
    End Function
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If IsValid(10) = False Then
            Exit Sub
        End If
        If Save() = False Then
            Exit Sub
        Else
            MsgBox("عملیات ثبت با موفقیت صورت پذیرفت .", MsgBoxStyle.Information + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.OkOnly, "")
            Me.Close()
        End If
    End Sub
    Private Function Save() As Boolean
        Save = False

        Dim cnSQL As New SqlConnection
        Dim cmSQL As New SqlCommand
        Dim strSQL As String = ""

        Dim strNoePardakht As String = ""
        For i As Integer = 0 To chklstNoePardakht.Items.Count - 1
            If chklstNoePardakht.GetItemChecked(i) = True Then
                chklstNoePardakht.SelectedIndex = i
                strNoePardakht &= chklstNoePardakht.SelectedValue.ToString + ","
            End If
        Next
        strNoePardakht = strNoePardakht.Substring(0, strNoePardakht.Length - 1)

        Dim strMoshtary As String = ""
        Dim strNotEffectMoshtary As String = ""
        If cmbDastehBandyMoshtary.SelectedIndex = 2 Then
            If chkNotEffectMoshtary_NoeMoshtaey.Checked = False Then
                strMoshtary = IIf(strMoshtary_NoeMoshtary_Valid = ",", "", strMoshtary_NoeMoshtary_Valid)
            Else
                strNotEffectMoshtary = IIf(strMoshtary_NoeMoshtary_Valid = ",", "", strMoshtary_NoeMoshtary_Valid)
            End If
        ElseIf cmbDastehBandyMoshtary.SelectedIndex = 3 Then
            If chkNotEffectMoshtary_Senf.Checked = False Then
                strMoshtary = IIf(strMoshtary_NoeSenf_Valid = ",", "", strMoshtary_NoeSenf_Valid)
            Else
                strNotEffectMoshtary = IIf(strMoshtary_NoeSenf_Valid = ",", "", strMoshtary_NoeSenf_Valid)
            End If
        ElseIf cmbDastehBandyMoshtary.SelectedIndex = 4 Then
            If chkNotEffectMoshtary_Manategh.Checked = False Then
                strMoshtary = IIf(strMoshtary_Mantagheh_Valid = ",", "", strMoshtary_Mantagheh_Valid)
            Else
                strNotEffectMoshtary = IIf(strMoshtary_Mantagheh_Valid = ",", "", strMoshtary_Mantagheh_Valid)
            End If
        ElseIf cmbDastehBandyMoshtary.SelectedIndex = 5 Then
            If cmbNoeFieldNotEffectMoshtary.SelectedIndex = 1 Then
                strNotEffectMoshtary = IIf(strMoshtary_NotEffect = ",", "", strMoshtary_NotEffect)
            End If
        End If

        Dim strKala As String = ""
        If cmbNoeFieldJayezeh.SelectedIndex = 1 Then
            If chkNotEffectKala.Checked = False Then
                strKala = IIf(strKala_Effect = ",", "", strKala_Effect)
            End If
        ElseIf cmbNoeFieldJayezeh.SelectedIndex = 2 Then
            If chkNotEffectKala_Brand.Checked = False Then
                strKala = IIf(strKala_Brand_Effect = ",", "", strKala_Brand_Effect)
            End If
        ElseIf cmbNoeFieldJayezeh.SelectedIndex = 3 Then
            If chkNotEffectKala_GorohKala.Checked = False Then
                strKala = IIf(strKala_GorohKala_Effect = ",", "", strKala_GorohKala_Effect)
            End If
        End If

        Dim strNotEffectKala As String = ""
        If cmbNoeFieldJayezeh.SelectedIndex = 1 Then
            If chkNotEffectKala.Checked = True Then
                strNotEffectKala = IIf(strKala_Effect = ",", "", strKala_Effect)
            End If
        ElseIf cmbNoeFieldJayezeh.SelectedIndex = 2 Then
            If chkNotEffectKala_Brand.Checked = True Then
                strNotEffectKala = IIf(strKala_Brand_Effect = ",", "", strKala_Brand_Effect)
            End If
        ElseIf cmbNoeFieldJayezeh.SelectedIndex = 3 Then
            If chkNotEffectKala_GorohKala.Checked = True Then
                strNotEffectKala = IIf(strKala_GorohKala_Effect = ",", "", strKala_GorohKala_Effect)
            End If
        End If

        Try
            If rbGheirRasmi.Checked Or rbRasmi.Checked Then
                For l As Integer = 0 To chlMarkazPakhsh.Items.Count - 1
                    If chlMarkazPakhsh.GetItemChecked(l) Then
                        chlMarkazPakhsh.SelectedIndex = l

                        strSQL = "Sales.spTakhfifJayezehTarkibi_Save "

                        cnSQL = New SqlConnection(ConnectionString)
                        cnSQL.Open()

                        cmSQL = New SqlCommand(strSQL, cnSQL)
                        cmSQL.CommandType = CommandType.StoredProcedure
                        cmSQL.Parameters.Clear()

                        cmSQL.Parameters.AddWithValue("CodeMahal", chlMarkazPakhsh.SelectedValue)
                        cmSQL.Parameters.AddWithValue("AzTarikh", mskAzTarikh.Text)
                        cmSQL.Parameters.AddWithValue("TaTarikh", mskTaTarikh.Text)
                        cmSQL.Parameters.AddWithValue("NoeAeenNameh", cmbNoeAeenNameh.SelectedIndex)
                        cmSQL.Parameters.AddWithValue("NoeAzTa", cmbNoeAzTa.SelectedIndex)
                        cmSQL.Parameters.AddWithValue("Az", txtAz.Text.Trim)
                        cmSQL.Parameters.AddWithValue("Ta", txtTa.Text.Trim)
                        cmSQL.Parameters.AddWithValue("BeEzae", IIf(txtBeEzae.Text.Trim = "", 0, txtBeEzae.Text.Trim))
                        cmSQL.Parameters.AddWithValue("NoeBastehBandy", IIf(cmbNoeAzTa.SelectedIndex = 2, 0, cmbNoeBastehBandy.SelectedIndex))
                        cmSQL.Parameters.AddWithValue("strNoePardakht", strNoePardakht)
                        cmSQL.Parameters.AddWithValue("NoeTakhfifEhdaei", cmbNoeTakhfifEhdaei.SelectedIndex)
                        cmSQL.Parameters.AddWithValue("Sharh", txtSharh.Text.Trim)
                        cmSQL.Parameters.AddWithValue("NoeFieldMoshtary", cmbDastehBandyMoshtary.SelectedIndex)
                        cmSQL.Parameters.AddWithValue("strMoshtary", IIf(strMoshtary_Valid <> ",", strMoshtary_Valid, ""))
                        cmSQL.Parameters.AddWithValue("sNoeMoshtary", IIf(cmbDastehBandyMoshtary.SelectedIndex = 2, cmbNoeMoshtary.SelectedValue, 0))
                        cmSQL.Parameters.AddWithValue("sNoeSenf", IIf(cmbDastehBandyMoshtary.SelectedIndex = 3, cmbNoeSenf.SelectedValue, 0))
                        cmSQL.Parameters.AddWithValue("sOstan", IIf(cmbDastehBandyMoshtary.SelectedIndex = 4, cmbOstan.SelectedValue, 0))
                        cmSQL.Parameters.AddWithValue("sShahr", IIf(cmbDastehBandyMoshtary.SelectedIndex = 4, IIf(chkShahr.Checked = True, cmbShahr.SelectedValue, 0), 0))
                        cmSQL.Parameters.AddWithValue("sMantagheh", IIf(cmbDastehBandyMoshtary.SelectedIndex = 4, IIf(chkMantagheh.Checked = True, cmbMantagheh.SelectedValue, 0), 0))
                        cmSQL.Parameters.AddWithValue("sMahaleh", IIf(cmbDastehBandyMoshtary.SelectedIndex = 4, IIf(chkMahaleh.Checked = True, cmbMahaleh.SelectedValue, 0), 0))
                        cmSQL.Parameters.AddWithValue("strEffectMoshtary", strMoshtary)
                        cmSQL.Parameters.AddWithValue("NoeFeildNotEffectMoshtary", IIf(cmbNoeFieldNotEffectMoshtary.SelectedIndex > 0, cmbNoeFieldNotEffectMoshtary.SelectedIndex, 0))
                        cmSQL.Parameters.AddWithValue("strNotEffectMoshtary", strNotEffectMoshtary)
                        cmSQL.Parameters.AddWithValue("strNotEffectNoeMoshtary", IIf(cmbNoeFieldNotEffectMoshtary.SelectedIndex = 2, IIf(strNoeMoshtary_NotEffect = ",", "", strNoeMoshtary_NotEffect), ""))
                        cmSQL.Parameters.AddWithValue("strNotEffectNoeSenf", IIf(cmbNoeFieldNotEffectMoshtary.SelectedIndex = 3, IIf(strNoeSenf_NotEffect = ",", "", strNoeSenf_NotEffect), ""))
                        cmSQL.Parameters.AddWithValue("strNotEffectOstan", IIf(cmbNoeFieldNotEffectMoshtary.SelectedIndex = 4, IIf(chkShahr_Effect.Checked = False, IIf(strManategh_NotEffect = ",", "", strManategh_NotEffect), ""), ""))
                        cmSQL.Parameters.AddWithValue("strNotEffectShahr", IIf(cmbNoeFieldNotEffectMoshtary.SelectedIndex = 4, IIf(chkShahr_Effect.Checked = True And chkMantagheh_Effect.Checked = False, IIf(strManategh_NotEffect = ",", "", strManategh_NotEffect), ""), ""))
                        cmSQL.Parameters.AddWithValue("strNotEffectMantagheh", IIf(cmbNoeFieldNotEffectMoshtary.SelectedIndex = 4, IIf(chkMantagheh_Effect.Checked = True And chkMahaleh_Effect.Checked = False, IIf(strManategh_NotEffect = ",", "", strManategh_NotEffect), ""), ""))
                        cmSQL.Parameters.AddWithValue("strNotEffectMahaleh", IIf(cmbNoeFieldNotEffectMoshtary.SelectedIndex = 4, IIf(chkMahaleh_Effect.Checked = True, IIf(strManategh_NotEffect = ",", "", strManategh_NotEffect), ""), ""))
                        cmSQL.Parameters.AddWithValue("NoeFieldJayezeh", cmbNoeFieldJayezeh.SelectedIndex)
                        cmSQL.Parameters.AddWithValue("strKala", strKala)
                        cmSQL.Parameters.AddWithValue("ccBrand", IIf(cmbNoeFieldJayezeh.SelectedIndex = 2, cmbBrand.SelectedValue, 0))
                        cmSQL.Parameters.AddWithValue("sG1", IIf(cmbNoeFieldJayezeh.SelectedIndex = 3, cmbG1.SelectedValue, 0))
                        cmSQL.Parameters.AddWithValue("sG2", IIf(cmbNoeFieldJayezeh.SelectedIndex = 3, IIf(chkG2.Checked = True, cmbG2.SelectedValue, 0), 0))
                        cmSQL.Parameters.AddWithValue("sG3", IIf(cmbNoeFieldJayezeh.SelectedIndex = 3, IIf(chkG3.Checked = True, cmbG3.SelectedValue, 0), 0))
                        cmSQL.Parameters.AddWithValue("sG4", IIf(cmbNoeFieldJayezeh.SelectedIndex = 3, IIf(chkG4.Checked = True, cmbG4.SelectedValue, 0), 0))
                        cmSQL.Parameters.AddWithValue("sG5", IIf(cmbNoeFieldJayezeh.SelectedIndex = 3, IIf(chkG5.Checked = True, cmbG5.SelectedValue, 0), 0))
                        cmSQL.Parameters.AddWithValue("strNotEffectKala", strNotEffectKala)
                        cmSQL.Parameters.AddWithValue("NoeMohasebeh", IIf(cmbNoeMohasebeh.SelectedIndex > 0, 1, 0))
                        cmSQL.Parameters.AddWithValue("ccKalaJayezeh", IIf(cmbNoeTakhfifEhdaei.SelectedIndex = 1, txtCodeKalaJayezeh.Tag, 0))
                        cmSQL.Parameters.AddWithValue("TedadKalaJayezeh", IIf(cmbNoeTakhfifEhdaei.SelectedIndex = 1, txtTedadJayezeh.Text.Trim, 0))
                        cmSQL.Parameters.AddWithValue("NoeBastehBandyJayezeh", IIf(cmbNoeTakhfifEhdaei.SelectedIndex = 1, cmbNoeBastehBandyJayezeh.SelectedIndex, 0))
                        cmSQL.Parameters.AddWithValue("DarsadTJ", IIf(cmbNoeTakhfifEhdaei.SelectedIndex = 2, txtDarsadEhdaei.Text.Trim, 0))
                        cmSQL.Parameters.AddWithValue("RialTJ", IIf(cmbNoeTakhfifEhdaei.SelectedIndex = 3, txtRialEhdaei.Text.Trim, 0))
                        cmSQL.Parameters.AddWithValue("EffectOn", IIf((cmbNoeAeenNameh.SelectedIndex = 1 AndAlso cmbNoeTakhfifEhdaei.SelectedIndex = 2), cmbEffectOn.SelectedIndex, 1))
                        cmSQL.Parameters.AddWithValue("IsRasmi", IIf(rbRasmi.Checked, 1, 0))
                        cmSQL.ExecuteNonQuery()
                    End If

                Next
            ElseIf RbBoth.Checked Then
                For l As Integer = 0 To chlMarkazPakhsh.Items.Count - 1
                    If chlMarkazPakhsh.GetItemChecked(l) Then
                        chlMarkazPakhsh.SelectedIndex = l
                        For i = 0 To 1
                            strSQL = "Sales.spTakhfifJayezehTarkibi_Save "

                            cnSQL = New SqlConnection(ConnectionString)
                            cnSQL.Open()

                            cmSQL = New SqlCommand(strSQL, cnSQL)
                            cmSQL.CommandType = CommandType.StoredProcedure
                            cmSQL.Parameters.Clear()

                            cmSQL.Parameters.AddWithValue("CodeMahal", chlMarkazPakhsh.SelectedValue)
                            cmSQL.Parameters.AddWithValue("AzTarikh", mskAzTarikh.Text)
                            cmSQL.Parameters.AddWithValue("TaTarikh", mskTaTarikh.Text)
                            cmSQL.Parameters.AddWithValue("NoeAeenNameh", cmbNoeAeenNameh.SelectedIndex)
                            cmSQL.Parameters.AddWithValue("NoeAzTa", cmbNoeAzTa.SelectedIndex)
                            cmSQL.Parameters.AddWithValue("Az", txtAz.Text.Trim)
                            cmSQL.Parameters.AddWithValue("Ta", txtTa.Text.Trim)
                            cmSQL.Parameters.AddWithValue("BeEzae", IIf(txtBeEzae.Text.Trim = "", 0, txtBeEzae.Text.Trim))
                            cmSQL.Parameters.AddWithValue("NoeBastehBandy", IIf(cmbNoeAzTa.SelectedIndex = 2, 0, cmbNoeBastehBandy.SelectedIndex))
                            cmSQL.Parameters.AddWithValue("strNoePardakht", strNoePardakht)
                            cmSQL.Parameters.AddWithValue("NoeTakhfifEhdaei", cmbNoeTakhfifEhdaei.SelectedIndex)
                            cmSQL.Parameters.AddWithValue("Sharh", txtSharh.Text.Trim)
                            cmSQL.Parameters.AddWithValue("NoeFieldMoshtary", cmbDastehBandyMoshtary.SelectedIndex)
                            cmSQL.Parameters.AddWithValue("strMoshtary", IIf(strMoshtary_Valid <> ",", strMoshtary_Valid, ""))
                            cmSQL.Parameters.AddWithValue("sNoeMoshtary", IIf(cmbDastehBandyMoshtary.SelectedIndex = 2, cmbNoeMoshtary.SelectedValue, 0))
                            cmSQL.Parameters.AddWithValue("sNoeSenf", IIf(cmbDastehBandyMoshtary.SelectedIndex = 3, cmbNoeSenf.SelectedValue, 0))
                            cmSQL.Parameters.AddWithValue("sOstan", IIf(cmbDastehBandyMoshtary.SelectedIndex = 4, cmbOstan.SelectedValue, 0))
                            cmSQL.Parameters.AddWithValue("sShahr", IIf(cmbDastehBandyMoshtary.SelectedIndex = 4, IIf(chkShahr.Checked = True, cmbShahr.SelectedValue, 0), 0))
                            cmSQL.Parameters.AddWithValue("sMantagheh", IIf(cmbDastehBandyMoshtary.SelectedIndex = 4, IIf(chkMantagheh.Checked = True, cmbMantagheh.SelectedValue, 0), 0))
                            cmSQL.Parameters.AddWithValue("sMahaleh", IIf(cmbDastehBandyMoshtary.SelectedIndex = 4, IIf(chkMahaleh.Checked = True, cmbMahaleh.SelectedValue, 0), 0))
                            cmSQL.Parameters.AddWithValue("strEffectMoshtary", strMoshtary)
                            cmSQL.Parameters.AddWithValue("NoeFeildNotEffectMoshtary", IIf(cmbNoeFieldNotEffectMoshtary.SelectedIndex > 0, cmbNoeFieldNotEffectMoshtary.SelectedIndex, 0))
                            cmSQL.Parameters.AddWithValue("strNotEffectMoshtary", strNotEffectMoshtary)
                            cmSQL.Parameters.AddWithValue("strNotEffectNoeMoshtary", IIf(cmbNoeFieldNotEffectMoshtary.SelectedIndex = 2, IIf(strNoeMoshtary_NotEffect = ",", "", strNoeMoshtary_NotEffect), ""))
                            cmSQL.Parameters.AddWithValue("strNotEffectNoeSenf", IIf(cmbNoeFieldNotEffectMoshtary.SelectedIndex = 3, IIf(strNoeSenf_NotEffect = ",", "", strNoeSenf_NotEffect), ""))
                            cmSQL.Parameters.AddWithValue("strNotEffectOstan", IIf(cmbNoeFieldNotEffectMoshtary.SelectedIndex = 4, IIf(chkShahr_Effect.Checked = False, IIf(strManategh_NotEffect = ",", "", strManategh_NotEffect), ""), ""))
                            cmSQL.Parameters.AddWithValue("strNotEffectShahr", IIf(cmbNoeFieldNotEffectMoshtary.SelectedIndex = 4, IIf(chkShahr_Effect.Checked = True And chkMantagheh_Effect.Checked = False, IIf(strManategh_NotEffect = ",", "", strManategh_NotEffect), ""), ""))
                            cmSQL.Parameters.AddWithValue("strNotEffectMantagheh", IIf(cmbNoeFieldNotEffectMoshtary.SelectedIndex = 4, IIf(chkMantagheh_Effect.Checked = True And chkMahaleh_Effect.Checked = False, IIf(strManategh_NotEffect = ",", "", strManategh_NotEffect), ""), ""))
                            cmSQL.Parameters.AddWithValue("strNotEffectMahaleh", IIf(cmbNoeFieldNotEffectMoshtary.SelectedIndex = 4, IIf(chkMahaleh_Effect.Checked = True, IIf(strManategh_NotEffect = ",", "", strManategh_NotEffect), ""), ""))
                            cmSQL.Parameters.AddWithValue("NoeFieldJayezeh", cmbNoeFieldJayezeh.SelectedIndex)
                            cmSQL.Parameters.AddWithValue("strKala", strKala)
                            cmSQL.Parameters.AddWithValue("ccBrand", IIf(cmbNoeFieldJayezeh.SelectedIndex = 2, cmbBrand.SelectedValue, 0))
                            cmSQL.Parameters.AddWithValue("sG1", IIf(cmbNoeFieldJayezeh.SelectedIndex = 3, cmbG1.SelectedValue, 0))
                            cmSQL.Parameters.AddWithValue("sG2", IIf(cmbNoeFieldJayezeh.SelectedIndex = 3, IIf(chkG2.Checked = True, cmbG2.SelectedValue, 0), 0))
                            cmSQL.Parameters.AddWithValue("sG3", IIf(cmbNoeFieldJayezeh.SelectedIndex = 3, IIf(chkG3.Checked = True, cmbG3.SelectedValue, 0), 0))
                            cmSQL.Parameters.AddWithValue("sG4", IIf(cmbNoeFieldJayezeh.SelectedIndex = 3, IIf(chkG4.Checked = True, cmbG4.SelectedValue, 0), 0))
                            cmSQL.Parameters.AddWithValue("sG5", IIf(cmbNoeFieldJayezeh.SelectedIndex = 3, IIf(chkG5.Checked = True, cmbG5.SelectedValue, 0), 0))
                            cmSQL.Parameters.AddWithValue("strNotEffectKala", strNotEffectKala)
                            cmSQL.Parameters.AddWithValue("NoeMohasebeh", IIf(cmbNoeMohasebeh.SelectedIndex > 0, 1, 0))
                            cmSQL.Parameters.AddWithValue("ccKalaJayezeh", IIf(cmbNoeTakhfifEhdaei.SelectedIndex = 1, txtCodeKalaJayezeh.Tag, 0))
                            cmSQL.Parameters.AddWithValue("TedadKalaJayezeh", IIf(cmbNoeTakhfifEhdaei.SelectedIndex = 1, txtTedadJayezeh.Text.Trim, 0))
                            cmSQL.Parameters.AddWithValue("NoeBastehBandyJayezeh", IIf(cmbNoeTakhfifEhdaei.SelectedIndex = 1, cmbNoeBastehBandyJayezeh.SelectedIndex, 0))
                            cmSQL.Parameters.AddWithValue("DarsadTJ", IIf(cmbNoeTakhfifEhdaei.SelectedIndex = 2, txtDarsadEhdaei.Text.Trim, 0))
                            cmSQL.Parameters.AddWithValue("RialTJ", IIf(cmbNoeTakhfifEhdaei.SelectedIndex = 3, txtRialEhdaei.Text.Trim, 0))
                            cmSQL.Parameters.AddWithValue("EffectOn", IIf((cmbNoeAeenNameh.SelectedIndex = 1 AndAlso cmbNoeTakhfifEhdaei.SelectedIndex = 2), cmbEffectOn.SelectedIndex, 1))
                            cmSQL.Parameters.AddWithValue("IsRasmi", i)
                            cmSQL.ExecuteNonQuery()
                        Next
                    End If

                Next
            End If


                    cmSQL = Nothing
                    cnSQL.Close()

                    Return True

        Catch sqlExc As SqlException
            MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading + MsgBoxStyle.Information, " Error IN DataBase ----> Save ")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.MsgBoxRight + MsgBoxStyle.MsgBoxRtlReading, " Error IN ----> Save ")
        End Try
    End Function
    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub
    Private Sub chkNotEffectKala_CheckedChanged(sender As Object, e As EventArgs) Handles chkNotEffectKala.CheckedChanged
        If chkNotEffectKala.Checked = True Then
            cmbNoeMohasebeh.SelectedIndex = 0
            cmbNoeMohasebeh.Enabled = False

            If cmbEffectOn.Visible = True Then
                cmbEffectOn.Enabled = False
                cmbEffectOn.SelectedIndex = 0
            End If
        Else
            If cmbEffectOn.Visible = True Then
                cmbNoeMohasebeh.Enabled = True
                cmbEffectOn.Enabled = True
            End If
        End If
    End Sub
    Private Sub cmbNoeMohasebeh_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbNoeMohasebeh.SelectedIndexChanged
        If cmbNoeFieldJayezeh.SelectedIndex = 1 Then
            If cmbNoeMohasebeh.SelectedIndex = 1 Then
                chkNotEffectKala.Checked = False
            End If
        End If
    End Sub
    Private Sub chkNotEffectKala_GorohKala_CheckedChanged(sender As Object, e As EventArgs) Handles chkNotEffectKala_GorohKala.CheckedChanged
        If cmbEffectOn.Visible = True Then
            If chkNotEffectKala_GorohKala.Checked = True Then
                cmbEffectOn.Enabled = False
                cmbEffectOn.SelectedIndex = 0
            Else
                cmbEffectOn.Enabled = True
            End If
        End If
    End Sub
    Private Sub chkNotEffectKala_Brand_CheckedChanged(sender As Object, e As EventArgs) Handles chkNotEffectKala_Brand.CheckedChanged
        If cmbEffectOn.Visible = True Then
            If chkNotEffectKala_Brand.Checked = True Then
                cmbEffectOn.Enabled = False
                cmbEffectOn.SelectedIndex = 0
            Else
                cmbEffectOn.Enabled = True
            End If
        End If
    End Sub

    Private Sub cmbBrandKala_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbBrandKala.SelectedIndexChanged
        If Flg = False Then
            Exit Sub
        End If

        txtSearch_Kala_brand.Text = ""
        SearchInListBoxKala(lbKala, txtSearch_Kala.Text.Trim, ccBrand:=cmbBrandKala.SelectedValue)

        'txtSearch_Kala_Effect.Text = ""
        'lbKala_Effect.DataSource = Nothing
        'lbKala_Effect.Items.Clear()
    End Sub

    Private Sub btnAddAllKala_Click(sender As Object, e As EventArgs) Handles btnAddAllKala.Click
        If lbKala.Items.Count = 0 Then
            Exit Sub
        End If

        For index As Integer = 0 To lbKala.Items.Count - 1 Step 1
            'strKala_Effect = strKala_Effect.Replace(Str(lbKala.SelectedValue).Trim & ",", "")
            lbKala.SetSelected(index, True)
            strKala_Effect &= Str(lbKala.SelectedValue).Trim & ","
        Next index

        'strKala_Effect = strKala_Effect.Replace(Str(lbKala.SelectedValue).Trim & ",", "")
        'strKala_Effect &= Str(lbKala.SelectedValue).Trim & ","

        SearchInListBoxKala(lbKala_Effect, txtSearch_Kala_Effect.Text.Trim, strSelectedFields:=strKala_Effect)
    End Sub

    Private Sub btnRemoveَAllKala_Click(sender As Object, e As EventArgs) Handles btnRemoveَAllKala.Click
        If lbKala_Effect.Items.Count = 0 Then
            Exit Sub
        End If

        'For index As Integer = 0 To lbKala_Effect.Items.Count - 1 Step 1
        '    'strKala_Effect = strKala_Effect.Replace(Str(lbKala.SelectedValue).Trim & ",", "")
        '    lbKala_Effect.SetSelected(index, True)
        '    strKala_Effect &= Str(lbKala_Effect.SelectedValue).Trim & ","
        'Next index

        strKala_Effect = strKala_Effect.Replace(Str(lbKala_Effect.SelectedValue).Trim & ",", "")


        SearchInListBoxKala(lbKala_Effect, txtSearch_Kala_Effect.Text.Trim, strSelectedFields:=strKala_Effect)
    End Sub
End Class