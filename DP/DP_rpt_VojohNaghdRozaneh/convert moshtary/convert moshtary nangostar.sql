insert into tblFO_Moshtary (CodeMahal ,CodeMoshtary ,CodeMoshtary_Old ,NameMoshtary ,
				NameTablo ,Address,tel,mobile,
				sKeshvar,sOstan,sShahr,sVazeiat,sNoeMoshtary)
select 1,noemoshtary,isnull(noemoshtary,''),isnull(name,''),
isnull(tablo,''),address
	,isnull(tel,''),isnull(mobile,'')
	,453,403170,403194,3980,5558
from  Sheet1$


insert into tblFO_MoshtaryAddress (ccMoshtary ,sNoeAddress ,Faal ,Address ,
						Telephone,Mobile )
select ccMoshtary ,3976,1,address,tel,mobile from tblFO_Moshtary 


Insert into tblHE_CodeTafsily (CodeMahal,NumTafsily,CodeNoeTafsily,
	FSharhTafsily,Faal,UserName  )
select 1,ROW_NUMBER() OVER(ORDER BY ccMoshtary DESC)+10001 AS 'NumTafsily'
,5,namemoshtary + ' ' + NameTablo  ,1,ccMoshtary from tblFO_Moshtary 


update tblFO_Moshtary set
CodeTafsily1 = 
(select codetafsily from tblHE_CodeTafsily 
	where UserName = tblFO_Moshtary.ccMoshtary and CodeNoeTafsily =5)

