-- convert olfatmehr


insert into tblFO_Moshtary (CodeMahal ,CodeMoshtary ,CodeMoshtary_Old ,NameMoshtary ,
				NameTablo ,[Address],tel,
				sKeshvar,sOstan,sShahr,sVazeiat,sNoeSenf,sNoeMoshtary)
select 32,ccMoshtary,isnull(ccMoshtary,''),isnull(NameMoshtary,''),
isnull(NameTablo,''),[Address]
	,isnull(tel,'')
	-- iran,zanjan,zanjan,faal,khordeforosh,supermarket
	,453,525,1311,3980,5558,5562
from dbo.Sheet1$
where address is not null and shahr ='“‰Ã«‰ '

--select * from tblGL_ShenasehOmomi where CodeLink = 506

insert into tblFO_MoshtaryAddress (ccMoshtary ,sNoeAddress ,Faal ,Address ,
						Telephone )
select ccMoshtary ,3976,1,[Address],tel from tblFO_Moshtary 
where CodeMahal = 5 and ccMoshtary not in (select ccMoshtary from tblFO_MoshtaryAddress )

--select * from tblHE_CodeTafsily where CodeNoeTafsily =5  and codemahal = 5  order by numtafsily desc

Insert into tblHE_CodeTafsily (CodeMahal,NumTafsily,CodeNoeTafsily,
	FSharhTafsily,Faal,UserName  )
select 5,ROW_NUMBER() OVER(ORDER BY ccMoshtary DESC)+33662 AS 'NumTafsily'
,5,namemoshtary + ' ' + NameTablo  ,1,ccMoshtary from tblFO_Moshtary 
where CodeMahal = 5 and CodeTafsily1 =0

update tblFO_Moshtary set
CodeTafsily1 = 
(select codetafsily from tblHE_CodeTafsily 
	where UserName = tblFO_Moshtary.ccMoshtary and CodeNoeTafsily =5 and CodeMahal = 5 
	and UserName is not null)where ccMoshtary in (select UserName  from tblHE_CodeTafsily where CodeMahal = 5)
	
	
--update moshtary1 set
--code= (select * from tblGL_ShenasehOmomi where CodeAsli =36
--and Code not in (5277,5279,4156) and
--right(Sharh,2)=moshtary1.mantagheh )



select * from tblGL_ShenasehOmomi
where Sharh like N'%Œ—œÂ%'

select * from tblGL_ShenasehOmomi
where CodeAsli = 23

select * from tblGL_ShenasehOmomi
where CodeAsli = 47

select * from tblGL_ShenasehOmomi
where CodeAsli = 150


