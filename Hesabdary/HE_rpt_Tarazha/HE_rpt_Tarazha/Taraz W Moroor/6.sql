USE [DB_Pakhsh]
GO

/****** Object:  StoredProcedure [dbo].[rpt_MoroorTafsily2_moeen]    Script Date: 3/14/2017 3:22:12 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO






CREATE pROCEDURE [dbo].[rpt_MoroorTafsily2_moeen]
         @AzTarikh			nvarchar(100)	,
         @TaTarikh			nvarchar(100)	,
         @CodeTafsily1		int				,
         @CodeTafsily2		int				,
         @CodeTafsily3		int				

       	   
AS
BEGIN
     select CodeGoroh ,FSharhGoroh ,CodeKol ,FSharhKol ,CodeMoeen ,FSharhMoeen ,FSharhGoroh+' - '+FSharhKol+' - '+FSharhMoeen as SharhSanad
	 ,FSharhMoeen as sharhCodeing ,CodeGoroh+''+CodeKol+''+CodeMoeen as codehesab,NumTafsily2 as CodeTafsilyOld,Tafsily2 as CodeTafsily
	 ,sum(cast(Bed as float))as bed , sum(cast(bes as float)) as bes , cast (0.0 as float) as Mandeh from qryHE_SanadHesabdaryTitrSatr where  Tafsily2 = @CodeTafsily2 
and TarikhSanad between @AzTarikh And @TaTarikh
group by CodeGoroh ,FSharhGoroh ,CodeKol ,FSharhKol ,CodeMoeen ,FSharhMoeen,NumTafsily2 ,Tafsily2

END




















GO

