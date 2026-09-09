insert into tblFO_Moshtary (CodeMahal ,CodeMoshtary ,CodeMoshtary_Old ,NameMoshtary ,
				NameTablo ,[Address],tel,
				sKeshvar,sOstan,sShahr,sVazeiat,sNoeSenf,sNoeMoshtary)
select 5,CodeMoshtary,isnull(CodeMoshtary,''),isnull(NameMoshtary,''),
isnull(NameTablo,''),[Address]
	,isnull(tel,'')
	,453,506,1150,3980,4745,449662
from dbo.shiraz
where codemoshtary is not null

--select * from tblGL_ShenasehOmomi where CodeLink = 506

insert into tblFO_MoshtaryAddress (ccMoshtary ,sNoeAddress ,Faal ,Address ,
						Telephone )
select ccMoshtary ,3976,1,[Address],tel from tblFO_Moshtary 
where CodeMahal = 5 and ccMoshtary not in (select ccMoshtary from tblFO_MoshtaryAddress )

--select * from tblHE_CodeTafsily where CodeNoeTafsily =5 order by numtafsily desc

Insert into tblHE_CodeTafsily (CodeMahal,NumTafsily,CodeNoeTafsily,
	FSharhTafsily,Faal,UserName  )
select 1,ROW_NUMBER() OVER(ORDER BY ccMoshtary DESC)+33628 AS 'NumTafsily'
,5,namemoshtary + ' ' + NameTablo  ,1,ccMoshtary from tblFO_Moshtary 
where CodeMahal = 5 and CodeTafsily1 is null

update tblFO_Moshtary set
CodeTafsily1 = 
(select codetafsily from tblHE_CodeTafsily 
	where UserName = tblFO_Moshtary.ccMoshtary and CodeNoeTafsily =5)
	
	
--update moshtary1 set
--code= (select * from tblGL_ShenasehOmomi where CodeAsli =36
--and Code not in (5277,5279,4156) and
--right(Sharh,2)=moshtary1.mantagheh )

