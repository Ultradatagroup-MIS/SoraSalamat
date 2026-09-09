USE [PAKHSH]
GO

/****** Object:  Table [Sales].[TafkikJozeTasfieh]    Script Date: 03/30/2013 10:08:08 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [Sales].[TafkikJozeTasfieh](
	[ccMarkazPakhsh] [int] NOT NULL,
	[ccTafkikJoze] [bigint] NOT NULL,
	[TarikhTasfieh] [datetime] NOT NULL,
	[TedadKalaTafkik] [float] NOT NULL,
	[MablaghKolTafkik] [float] NOT NULL,
	[MablaghNaghd] [bigint] NOT NULL,
	[MablaghChek] [bigint] NOT NULL,
	[MablaghKartKhan] [bigint] NOT NULL,
	[MablaghTakhfif] [bigint] NOT NULL,
	[MablaghMarjoee] [bigint] NOT NULL,
	[MablaghResid] [bigint] NOT NULL,
	[CodeVazeiat] [tinyint] NOT NULL,
	[TarikhEntry] [datetime] NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_TafkikJozeTasfieh] PRIMARY KEY CLUSTERED 
(
	[ccTafkikJoze] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0 Sabt Avalieh,10 Taeed Shodeh' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'TafkikJozeTasfieh', @level2type=N'COLUMN',@level2name=N'CodeVazeiat'
GO

ALTER TABLE [Sales].[TafkikJozeTasfieh]  WITH CHECK ADD  CONSTRAINT [FK_TafkikJozeTasfieh_TafkikJoze] FOREIGN KEY([ccTafkikJoze])
REFERENCES [Sales].[TafkikJoze] ([ccTafkikJoze])
GO

ALTER TABLE [Sales].[TafkikJozeTasfieh] CHECK CONSTRAINT [FK_TafkikJozeTasfieh_TafkikJoze]
GO

ALTER TABLE [Sales].[TafkikJozeTasfieh] ADD  CONSTRAINT [DF_TafkikJozeTasfieh_ccMarkazPakhsh]  DEFAULT ((0)) FOR [ccMarkazPakhsh]
GO

ALTER TABLE [Sales].[TafkikJozeTasfieh] ADD  CONSTRAINT [DF_TafkikJozeTasfieh_ccTafkikJoze]  DEFAULT ((0)) FOR [ccTafkikJoze]
GO

ALTER TABLE [Sales].[TafkikJozeTasfieh] ADD  CONSTRAINT [DF_TafkikJozeTasfieh_TarikhTasfieh]  DEFAULT (getdate()) FOR [TarikhTasfieh]
GO

ALTER TABLE [Sales].[TafkikJozeTasfieh] ADD  CONSTRAINT [DF_Table_1_TedadKala]  DEFAULT ((0)) FOR [TedadKalaTafkik]
GO

ALTER TABLE [Sales].[TafkikJozeTasfieh] ADD  CONSTRAINT [DF_TafkikJozeTasfieh_MablaghKolTafkik]  DEFAULT ((0)) FOR [MablaghKolTafkik]
GO

ALTER TABLE [Sales].[TafkikJozeTasfieh] ADD  CONSTRAINT [DF_TafkikJozeTasfieh_MablaghNaghd]  DEFAULT ((0)) FOR [MablaghNaghd]
GO

ALTER TABLE [Sales].[TafkikJozeTasfieh] ADD  CONSTRAINT [DF_TafkikJozeTasfieh_MablaghChek]  DEFAULT ((0)) FOR [MablaghChek]
GO

ALTER TABLE [Sales].[TafkikJozeTasfieh] ADD  CONSTRAINT [DF_TafkikJozeTasfieh_MablaghKartKhan]  DEFAULT ((0)) FOR [MablaghKartKhan]
GO

ALTER TABLE [Sales].[TafkikJozeTasfieh] ADD  CONSTRAINT [DF_TafkikJozeTasfieh_MablaghTakhfif]  DEFAULT ((0)) FOR [MablaghTakhfif]
GO

ALTER TABLE [Sales].[TafkikJozeTasfieh] ADD  CONSTRAINT [DF_TafkikJozeTasfieh_MablaghMarjoee]  DEFAULT ((0)) FOR [MablaghMarjoee]
GO

ALTER TABLE [Sales].[TafkikJozeTasfieh] ADD  CONSTRAINT [DF_TafkikJozeTasfieh_MablaghResid]  DEFAULT ((0)) FOR [MablaghResid]
GO

ALTER TABLE [Sales].[TafkikJozeTasfieh] ADD  CONSTRAINT [DF_TafkikJozeTasfieh_CodeVazeiat]  DEFAULT ((0)) FOR [CodeVazeiat]
GO

ALTER TABLE [Sales].[TafkikJozeTasfieh] ADD  CONSTRAINT [DF_TafkikJozeTasfieh_TarikhEntry]  DEFAULT (getdate()) FOR [TarikhEntry]
GO

ALTER TABLE [Sales].[TafkikJozeTasfieh] ADD  CONSTRAINT [DF_TafkikJozeTasfieh_ModifiedDate]  DEFAULT (getdate()) FOR [ModifiedDate]
GO

