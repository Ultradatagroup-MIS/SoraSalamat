Public Class Enums ' P_System

#Region " Global "
    Public Enum GL_NoeVorod As Integer
        Dasty = 1
        System = 2
        PocketPC = 3
    End Enum
    Public Enum GL_NoeTaghir As Integer
        None = 0
        AddNewRecord = 1
        UpdateRecord = 2
        DeleteRecord = 3
    End Enum
    Public Enum GL_Fasl As Integer
        Bahar = 2992
        Tabestan = 2993
        Paeiz = 2994
        Zemestan = 2995
    End Enum
    Public Enum GL_Semat As Integer
        Modir = 3971
        Hesabdar = 3972
        Foroshandeh = 3974
        Foroshandeh_Sayar = 4061
        Sarparast_Forosh = 4186
        Modir_Forosh = 4185
        MamorPakhsh = 4062
        MamorVosol = 4063
        SandoghDar = 3973
        Ranandeh = 4068
        Karmand = 4431
    End Enum
    Public Enum GL_ModeForms As Integer
        None = 0
        AddNewRecord = 1
        UpdateRecord = 2
        Search = 3
        AddNewRow = 4
        UpdateRow = 5
        AddNewRow2 = 6
        UpdateRow2 = 7
        Delete = 8
        Filter = 9
    End Enum
    Public Enum GL_DataBaseObject As Integer
        Table = 456
        View = 457
        SP = 458
        Trigger = 459
        UserDefineFunction = 460
    End Enum
    Public Enum GL_UserPermission As Integer
        SelectData = 466
        InsertData = 467
        UpdateData = 468
        DeleteData = 469
        ShowForm = 470
        PrintData = 471
    End Enum
    Public Enum GL_SysConfig As Integer
        VaredShavad = 1
        VaredNashavad = 2
        MohemNist = 3
    End Enum
    Public Enum GL_SysConfigAddKalaMarjoee As Integer
        VaredShavad = 1
        MohemNist = 2
    End Enum
    Public Enum GL_SysConfigEnableFee As Integer
        Faal = 1
        NoFaal = 2
    End Enum
    Public Enum GL_SysConfigShowMablaghInRepAnbar As Integer
        Faal = 1
        NoFaal = 2
    End Enum
    Public Enum GL_SysConfigShowMablaghInAnbar As Integer
        Faal = 1
        NoFaal = 2
    End Enum
    Public Enum GL_SysConfigTasfieElamMarjoee As Integer
        Faal = 1
        NoFaal = 2
    End Enum
    Public Enum GL_SysConfigShowBachInBargeTafkik As Integer
        Faal = 1
        NoFaal = 2
    End Enum
#End Region
#Region " DP "
    Public Enum DP_NoeAmalyatDP As Integer
        Daryaft = 1
        Pardakht = 2
        BestanKary = 3
        Takhfif = 4
    End Enum
    Public Enum DP_AnvaeSanadDP As Integer
        D_NaghdyBeSandogh = 63
        D_CheckBeSandogh = 64
        D_Varizy = 65
        D_DaryaftTazmin = 67
        P_NaghdyAzSandogh = 89
        P_CheckAzHesab = 126
        P_BardashtAzHesab = 127
        P_PardakhtTazmin = 128
        B_Marjoee = 3
        F_TabdilBeTakhfif = 4
    End Enum
    Public Enum DP_Vazeiat As Integer
        DP_BedoneAmalyat = 0
        D_DarjarianVosol = 1
        D_Vosol = 2
        D_Bargasht = 3
        D_OdatCheck = 4
        D_OdatBeSandogh = 14
        D_CheckNaghdShodeh = 15
        D_VagoazaryBeSales = 5
        D_TazminOdat = 6
        D_TazminBeAdy = 7
        P_VosolCheck = 8
        P_BargashtCheck = 9
        P_OdatCheck = 10
        P_TazminOdat = 11
        P_TazminBeAdy = 12
        DP_Ebtal = 13
        P_VagoazaryBeSales = 17
    End Enum
    Public Enum DP_NoeArzHesab As Integer
        Rialy = 57
        Arzy = 58
    End Enum
    Public Enum DP_NoeHesab As Integer
        Jary = 36
    End Enum
    Public Enum DP_NoeVarizy As Integer
        FishNaghdy = 4423
        HavalehBanky = 4424
    End Enum
    Public Enum DP_NoeTazmin As Integer
        Check = 4426
        Safteh = 4427
    End Enum
    Public Enum DP_NoeCheck As Integer
        Ady = 4417
        Banky = 4418
        Ramzdar = 4419
        Teravel = 4420
        Iran = 4421
    End Enum
    Public Enum DP_NoeEnteghal As Integer
        BankBeBankNaghd = 1
        SandoghBeSandogh = 2
        BankBeBankCheck = 3
        SandoghBeBank = 4
        BankBeSandoghNaghd = 5
        BankBeSandoghCheck = 6
    End Enum
#End Region
#Region " Hesabdary "
    Public Enum HE_AnvaeTafsily As Integer
        HesabBank = 1
        Sandogh = 2
        Personely = 3
        Sahamdaran = 4
        Moshtarian = 5
        TaminKonandeh = 6
        AshkhasHaghighi = 7
        Peymankaran = 8
        MarakezHazineh = 9
        Mahsolat = 13
    End Enum
    Public Enum HE_Tafsily As Integer
        Tafsily1 = 1
        Tafsily2 = 2
        Tafsily3 = 3
    End Enum
    Public Enum HE_AnvaeSanadMaly As Integer
        SanadHesabdary = 152
        Daryaft = 153
        Pardakht = 154
        SanadBastanHesabMovaghat = 155
        SanadBastanHesabDaem = 156
        SanadEftetahieh = 157
        SanadForosh = 158
        SanadEkhtetamieh = 159
        SanadHoghoghDastMozd = 160
        SanadAnbar = 161
        SanadEsteghrar = 162
        SanadEnteghal = 163
    End Enum
    Public Enum HE_NoeSanadVaset As Integer
        SanadDaryaftTahvilBeShakhseSalesOdatBeSandogh = 43
        SanadDaryaftOdatCheck = 30
        SanadDaryaftTahvilBeShakhseSales = 31
        SanadDaryaftTazmin = 27
        SanadDaryaftCheckNazdeSandogh = 6
        SanadDaryaftCheckDarJaryanVosol = 7
        SanadDaryaftCheckVosolShod = 8
        SanadDaryaftNaghdBeSandogh = 12
        SanadDaryaftFisheNaghdi = 13
        SanadMarjoeeBeTaminKonande = 5
        SanadAnbarKharidKala = 14
        SanadAnbarEshantion = 15
        SanadAnbarMarjoeeAzMashin = 16
        SanadAnbarMarjoeeAzMoshtary = 17
        SanadAnbarResidAzMarkazPakhsh = 18
        SanadAnbarHavalehBeMashin = 19
        SanadAnbarHavalehBeMarkazPakhsh = 20
        SanadAnbarHavalehBeFaktor = 21
        SanadAnbarEzafehBeAnbar = 22
        SanadAnbarKasrAzAnbar = 23
        SanadAnbarResidAvalDoreh = 24
        SanadAnbarFaktorKharid = 35
        SanadPardakhtCheck = 9
        SanadPardakhtCheckVosol = 32
        SanadPardakhtNaghdAzSandogh = 10
        SanadPardakhtBardashtAzHesab = 11
        SanadPardakhtTazmin = 28
        SanadForoshFaktor = 26
        SanadDaryaftCheckBargasht = 36
        SanadEsteghrarMandehMoshtary = 38
        SanadEsteghrarMandehShHesab = 39
        SanadEsteghrarMandehSandogh = 40
        SanadEsteghrarMandehKala = 41
        SanadForoshElamMarjoee = 42
        SanadPardakhtTankhah = 44

        SanadEnteghalCheckAzBankBeBank = 46
        SanadEnteghalNaghdAzBankBeBank = 47
        SanadEnteghalCheckAzBankBeSandogh = 48
        SanadEnteghalNaghdAzBankBesandogh = 49
        SanadEnteghalSandoghBeSandogh = 50
        SanadEnteghalSandoghBeBank = 51
        SanadDaryaftCheckNaghdShodeh = 52

        SanadPardakhtCheckPardakhtShode_Odat = 53
        SanadPardakhtCheckPardakhtShode_Odat_NaghdShode = 54
        SanadPardakhtCheckPardakhtShode_Odat_Ebtal = 55
    End Enum
    Public Enum HE_VaziatSanadMaly As Integer
        BedonAmalyat = 0
        ErsalBarayeTaeed = 3942
        Odat = 3943
        Taeed = 3961
        ErsalMojadadBarayeTaeed = 3962
    End Enum
    Public Enum HE_MahiatGoroh As Integer
        SodVaZiany = 7
        TarazNameey = 8
        Entezamy = 9
    End Enum
    Public Enum HE_MahiatKol As Integer
        Bedehkar = 11
        Bestankar = 12
        MohemNist = 13
    End Enum
    Public Enum HE_MahiatMoeen As Integer
        Bedehkar = 15
        Bestankar = 16
        MohemNist = 21
    End Enum
    Public Enum HE_BedBes As Integer
        Bedehkar = 44
        Bestankar = 45
    End Enum
    Public Enum HE_SotohGozaresh
        Goroh = 2469
        Kol = 2470
        Moeen = 2471
        Tafsily1 = 2472
        Tafsily2 = 2473
        Tafsily3 = 2474
    End Enum
    Public Enum HE_NoeSanadHesabdary_Vorody
        ResidAzTaminKonandeh = 14
        MarjoeeAzMashin = 16
        MarjoeeAzMoshtary = 17
        ResidAzMarkazPakhsh = 18
        EzafehBeAnbar = 22
        ResidAvalDoreh = 24
    End Enum
    Public Enum HE_NoeSanadHesabdary_Khorojy
        MarjoeeBeTaminKonandeh = 5
        Eshantion = 15
        HavalehBeMashin = 19
        HavalehBeMarkazPakhsh = 20
        HavalehBeFaktor = 21
        KasrAzAnbar = 23
        HavalehBeForoshandehSayar = 29
    End Enum
#End Region
#Region " Forosh "
    Public Enum FO_VaziatForoshandeh As Integer
        Faal = 4055
        NoFaal = 4056
    End Enum
    Public Enum FO_VaziatAdamSabtPishFaktor As Integer
        BedoneAmalyat = 0
        TaeedShodeh = 1
    End Enum
    Public Enum FO_VaziatPishFaktor As Integer
        BedoneAmalyatTaeedModir = -1
        BedoneAmalyat = -2
        TaeedNashodeh = 4009
        TaeedShodeh = 4338
        ForoshNarafteh = 4191
        FaktorSaderShodeh = 4338
    End Enum
    Public Enum FO_VaziatDarkhastForoshandehSayar As Integer
        BedoneAmalyat = 0
        TaeedShodeh = 1
        TaeedNashodeh = 2
    End Enum
    Public Enum FO_VaziatFaktor As Integer
        BedoneAmalyat = 0
        TaeedNashodeh = 4260
        TaeedShodeh = 4261
    End Enum
    Public Enum FO_VaziatMoshtary As Integer
        Faal = 3980
        NoFaal = 3981
    End Enum
    Public Enum FO_VaziatElamMarjoee As Integer
        BedoneAmalyat = 0
        TaeedNashodeh = 4354
        TaeedShodeh = 4355
        Ersalshodeh = -1
    End Enum
    Public Enum FO_RoozShoroMasir As Integer
        Shanbeh = 2984
        YekShanbeh = 2985
        DoShanbeh = 2986
        SeShanbeh = 2987
        CheharShanbeh = 2988
        PanjShanbeh = 2989
        Jomeh = 2990
    End Enum
    Public Enum FO_ToorVizitMasir As Integer
        HarRooz = 4084
        FaselehYekRooz = 4085
        FaselehDoRooz = 4086
        HarHafteh = 4087
        HarDoHafteh = 4088
        HarSeHafeh = 4089
        HarCheharHafteh = 4090
        HarNohRooz = 4723
        HarHashtRooz = 4722
    End Enum
    Public Enum FO_VazieatMasir As Integer
        Faal = 4368
        NoFaal = 4369
    End Enum
    Public Enum FO_Hafteh As Integer
        HaftehAval = 4093
        HaftehDovom = 4094
        HaftehSevom = 4253
        HaftehChaharom = 4254
    End Enum
    Public Enum FO_NoeTatily As Integer
        Jomeh = 4258
        Rasmi = 4257
        Sherkat = 4256
    End Enum
    Public Enum FO_VazieatBargeTafkik
        ErsalNashodeh = 4193
        ErsalBeMasir = 4207
        FaktorSaderShodeh = 4339
        ErsalBeAnbar = 4194
        TaeedNahaie = 4839
    End Enum
    Public Enum FO_NoePardakht
        Resid = 3951
        Check = 3952
        CheckeNaghd = 4937
        Naghd = 3953
        Naghd2Star = 4734 'Naghd o check
    End Enum
    Public Enum FO_NoeTasviehMarjoee
        BestanKaryMoshtary = 4779
        TasviehBaFaktor = 4778
    End Enum
    Public Enum FO_VazeiatKalaJabejaieMarjoee
        TaeedShodeh = 4781
        TaeedNashodeh = 4782
        BedoneAmalyat = 0
    End Enum
    Public Enum FO_NoeKalaElamMarjoee As Integer
        Salem = 2
        Karab = 1
    End Enum
    Public Enum FO_NoeTafkik As Integer
        kharid = 1
        Anbar = 2
        HarDo = 3
    End Enum
    Public Enum FO_NoeTakhfifKala As Integer
        RialFaktor = 0
        Kala = 1
        GorohKala = 2
        Brand = 3
        KalaSelected = 4
        SabadKala = 5
    End Enum
#End Region
#Region " Anbar "
    Public Enum AN_VazeiatFaktorKharid As Integer
        BedoneAmalyat = 0
        TaeedShodeh = 1
    End Enum
    Public Enum AN_VazeiatKDXSefaresh As Integer
        BedoneAmalyat = 0
        TaeedShodeh = 1
    End Enum
    Public Enum AN_VazeiatKDXHavaleh As Integer
        BedoneAmalyat = 0
        TaeedShodeh = 1
    End Enum
    Public Enum AN_VazeiatHavalehBeForoshandehSayar As Integer
        BedoneAmalyat = 0
        TaeedShodeh = 1
    End Enum
    Public Enum AN_VazeiatKDXMarjoeeBeTaminKonandeh As Integer
        BedoneAmalyat = 0
        TaeedShodeh = 1
    End Enum
    Public Enum AN_VazeiatKDXMarjoeeAzMoshtary As Integer
        BedoneAmalyat = 0
        TaeedShodeh = 1
    End Enum
    Public Enum AN_VazeiatKDXResid As Integer
        BedoneAmalyat = 0
        TaeedShodeh = 1
    End Enum
    Public Enum AN_VazeiatKDXRSAvalDoreh As Integer
        BedoneAmalyat = 0
        TaeedShodeh = 1
    End Enum
    Public Enum AN_VazeiatKDXEshantion As Integer
        BedoneAmalyat = 0
        TaeedShodeh = 1
    End Enum
    Public Enum AN_VazeiatKDXKasrEzafeh As Integer
        BedoneAmalyat = 0
        TaeedShodeh = 1
    End Enum
    Public Enum AN_VazeiatBarnameh
        BedoneAmalyat = 0
        TaeedShodeh = 1
    End Enum
    Public Enum AN_VazeiatKDXKdxAnbarBeAnbar As Integer
        BedoneAmalyat = 0
        TaeedShodeh = 1
    End Enum
    Public Enum AN_VazeiatResidMavadAvalieh As Integer
        BedoneAmalyat = 0
        TaeedShodeh = 1
    End Enum
    Public Enum AN_VazeiatKasrMavadAvalieh As Integer
        BedoneAmalyat = 0
        TaeedShodeh = 1
    End Enum
    Public Enum AN_NoeHavaleh As Integer
        HavalehBeMashin = 1
        HavalehBeFaktor = 2
        HavalehBeMarkazPakhsh = 3
        HavalehBeForoshandehSayar = 4
    End Enum
    Public Enum AN_VazieatAnbar As Integer
        Faal = 1
        NoFaal = 0
    End Enum
    Public Enum AN_NoeResid As Integer
        ResidAzTaminKonandeh = 3
        ResidAzMarkazPakhsh = 2
        MarjoeeAzMashin = 1
    End Enum
    Public Enum AN_NoeSefaresh As Integer
        SefareshBeMarkazPakhsh = 1
        SefareshBeTaminKonandeh = 2
    End Enum
    Public Enum AN_NoeAnbar As Integer
        AnbarSalem = 1
        AnbarZayeat = 2
        AnbarGharantineh = 3
    End Enum
    Public Enum AN_NoeFormKasrEzafeh As Integer
        Kasr = 1
        Ezafeh = 2
    End Enum
    Public Enum AN_NoeGheimat As Short
        Sefr = 0
        Miangin = 4021
    End Enum
#End Region
#Region " Hoghogh "
    Public Enum HO_VazeiatHokm As Integer
        BedoneAmalyat = 4
        TaeedShodeh = 3
    End Enum

#End Region
#Region "Enum Reports"
    Public Enum Rpt_NoeSearch
        AnyPartOfField = 222
        StartOfField = 223
        EndOfField = 224
        WholeField = 225
    End Enum
    Public Enum Rpt_NoeControl As Integer
        TextBox = 392
        ComboBox = 393
        TreeView = 394
        CheckBox = 395
        MaskEditBox = 396
        Number = 397
    End Enum
#End Region

End Class