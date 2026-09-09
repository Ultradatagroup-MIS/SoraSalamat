 
/****** Object:  Table [dbo].[tblFO_KalaSahmiehBandy]    Script Date: 08/23/2011 12:54:46 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[tblFO_KalaSahmiehBandy](
	[ccKalaSahmiehBandy] [int] IDENTITY(1,1) NOT NULL,
	[AzTarikh] [datetime] NOT NULL,
	[TaTarikh] [datetime] NOT NULL,
	[CodeFard_Foroshandeh] [int] NOT NULL,
	[ccForoshandeh] [int] NOT NULL,
	[ccKala] [int] NOT NULL,
	[CodeMahal] [int] NOT NULL,
	[TedadMojody] [int] NOT NULL,
	[DarsadSahmiehBandy] [float] NOT NULL,
	[CodeVazeiat] [int] NOT NULL,
	[UserName] [nvarchar](20) NOT NULL,
	[Tarikh] [nvarchar](8) NOT NULL,
	[Saat] [nvarchar](8) NOT NULL,
 CONSTRAINT [PK_tblFO_KalaSahmiehBandy] PRIMARY KEY CLUSTERED 
(
	[ccKalaSahmiehBandy] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[tblFO_KalaSahmiehBandy] ADD  CONSTRAINT [DF_tblFO_KalaSahmiehBandy_TaTarikh1]  DEFAULT ('') FOR [AzTarikh]
GO

ALTER TABLE [dbo].[tblFO_KalaSahmiehBandy] ADD  CONSTRAINT [DF_tblFO_KalaSahmiehBandy_TaTarikh]  DEFAULT ('') FOR [TaTarikh]
GO

ALTER TABLE [dbo].[tblFO_KalaSahmiehBandy] ADD  CONSTRAINT [DF_tblFO_KalaSahmiehBandy_ccForoshandeh]  DEFAULT ((0)) FOR [CodeFard_Foroshandeh]
GO

ALTER TABLE [dbo].[tblFO_KalaSahmiehBandy] ADD  CONSTRAINT [DF_tblFO_KalaSahmiehBandy_ccForoshandeh_1]  DEFAULT ((0)) FOR [ccForoshandeh]
GO

ALTER TABLE [dbo].[tblFO_KalaSahmiehBandy] ADD  CONSTRAINT [DF_tblFO_KalaSahmiehBandy_ccKala]  DEFAULT ((0)) FOR [ccKala]
GO

ALTER TABLE [dbo].[tblFO_KalaSahmiehBandy] ADD  CONSTRAINT [DF_tblFO_KalaSahmiehBandy_CodeMahal]  DEFAULT ((0)) FOR [CodeMahal]
GO

ALTER TABLE [dbo].[tblFO_KalaSahmiehBandy] ADD  CONSTRAINT [DF_tblFO_KalaSahmiehBandy_TedadMojody]  DEFAULT ((0)) FOR [TedadMojody]
GO

ALTER TABLE [dbo].[tblFO_KalaSahmiehBandy] ADD  CONSTRAINT [DF_tblFO_KalaSahmiehBandy_DarsadSahmiehBandy]  DEFAULT ((0)) FOR [DarsadSahmiehBandy]
GO

ALTER TABLE [dbo].[tblFO_KalaSahmiehBandy] ADD  CONSTRAINT [DF_tblFO_KalaSahmiehBandy_CodeVazeiat]  DEFAULT ((0)) FOR [CodeVazeiat]
GO

ALTER TABLE [dbo].[tblFO_KalaSahmiehBandy] ADD  CONSTRAINT [DF_tblFO_KalaSahmiehBandy_UserName]  DEFAULT ('') FOR [UserName]
GO

ALTER TABLE [dbo].[tblFO_KalaSahmiehBandy] ADD  CONSTRAINT [DF_tblFO_KalaSahmiehBandy_Tarikh]  DEFAULT ('') FOR [Tarikh]
GO

ALTER TABLE [dbo].[tblFO_KalaSahmiehBandy] ADD  CONSTRAINT [DF_tblFO_KalaSahmiehBandy_Saat]  DEFAULT ('') FOR [Saat]
GO



/****** Object:  View [dbo].[qryFO_KalaSahmiehBandy]    Script Date: 08/23/2011 13:38:30 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[qryFO_KalaSahmiehBandy]
AS
SELECT     dbo.tblAN_Kala.CodeKala, dbo.tblAN_Kala.NameKala, dbo.tblFO_KalaSahmiehBandy.CodeFard_Foroshandeh, dbo.tblFO_KalaSahmiehBandy.ccKalaSahmiehBandy, 
                      dbo.tblFO_KalaSahmiehBandy.ccKala, dbo.tblFO_KalaSahmiehBandy.CodeMahal, dbo.tblFO_KalaSahmiehBandy.TedadMojody, 
                      dbo.tblFO_KalaSahmiehBandy.DarsadSahmiehBandy, dbo.tblFO_KalaSahmiehBandy.CodeVazeiat, dbo.tblFO_KalaSahmiehBandy.ccForoshandeh, 
                      dbo.tblGL_MoshakhasatFardi.FName + N' ' + dbo.tblGL_MoshakhasatFardi.LName AS FN, dbo.tblFO_KalaSahmiehBandy.TaTarikh, 
                      dbo.fnGL_ConvertToShamsiWithSlash(dbo.tblFO_KalaSahmiehBandy.TaTarikh) AS TaTarikhSlash, dbo.tblFO_KalaSahmiehBandy.AzTarikh, 
                      dbo.fnGL_ConvertToShamsiWithSlash(dbo.tblFO_KalaSahmiehBandy.AzTarikh) AS AzTarikhSlash
FROM         dbo.tblFO_KalaSahmiehBandy LEFT OUTER JOIN
                      dbo.tblGL_MoshakhasatFardi ON dbo.tblFO_KalaSahmiehBandy.CodeFard_Foroshandeh = dbo.tblGL_MoshakhasatFardi.CodeFard LEFT OUTER JOIN
                      dbo.tblAN_Kala ON dbo.tblFO_KalaSahmiehBandy.ccKala = dbo.tblAN_Kala.ccKala

GO
