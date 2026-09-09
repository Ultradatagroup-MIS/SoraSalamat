ALTER TABLE dbo.tblGL_SysConfig
ADD   CheckSahmiehBandy bit 
GO
update  dbo.tblGL_SysConfig set 
CheckSahmiehBandy=0
GO