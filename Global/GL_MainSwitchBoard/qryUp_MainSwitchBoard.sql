update tblGL_NameSystemSub set 
exepath=''
where CodeSubSystem in (
select CodeSubSystem from dbo.tblGL_NameSystemSub where 
CodeSubSystem not in (
select CodeSubSystem from dbo.tblGL_NameSystemSub where 
CodeSubSystem not in (select CodeLink_Sys from dbo.tblGL_NameSystemSub)))


select * from dbo.tblGL_NameSystemSub where 
CodeSubSystem not in (
select CodeSubSystem from dbo.tblGL_NameSystemSub where 
CodeSubSystem not in (select CodeLink_Sys from dbo.tblGL_NameSystemSub))