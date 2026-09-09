USE [PAKHSH]
GO

/****** Object:  Table [Sales].[TafkikJozeTasfiehSatr]    Script Date: 03/30/2013 10:08:42 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [Sales].[TafkikJozeTasfiehSatr](
	[ccTafkikJoze] [bigint] NOT NULL,
	[ccDarkhastFaktor] [bigint] NOT NULL,
	[MablaghKolTafkik] [float] NOT NULL,
	[MablaghNaghd] [bigint] NOT NULL,
	[MablaghChek] [bigint] NOT NULL,
	[MablaghKartKhan] [bigint] NOT NULL,
	[MablaghTakhfif] [bigint] NOT NULL,
	[MablaghMarjoee] [bigint] NOT NULL,
	[MablaghResid] [bigint] NOT NULL,
	[TarikhEntry] [datetime] NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_TafkikJozeTasfiehSatr] PRIMARY KEY CLUSTERED 
(
	[ccTafkikJoze] ASC,
	[ccDarkhastFaktor] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [Sales].[TafkikJozeTasfiehSatr]  WITH CHECK ADD  CONSTRAINT [FK_TafkikJozeTasfiehSatr_TafkikJozeTasfieh] FOREIGN KEY([ccTafkikJoze])
REFERENCES [Sales].[TafkikJozeTasfieh] ([ccTafkikJoze])
ON DELETE CASCADE
GO

ALTER TABLE [Sales].[TafkikJozeTasfiehSatr] CHECK CONSTRAINT [FK_TafkikJozeTasfiehSatr_TafkikJozeTasfieh]
GO

ALTER TABLE [Sales].[TafkikJozeTasfiehSatr] ADD  CONSTRAINT [DF_TafkikJozeTasfiehSatr_ccTafkikJoze]  DEFAULT ((0)) FOR [ccTafkikJoze]
GO

ALTER TABLE [Sales].[TafkikJozeTasfiehSatr] ADD  CONSTRAINT [DF_TafkikJozeTasfiehSatr_ccDarkhastFaktor]  DEFAULT ((0)) FOR [ccDarkhastFaktor]
GO

ALTER TABLE [Sales].[TafkikJozeTasfiehSatr] ADD  CONSTRAINT [DF_TafkikJozeTasfiehSatr_MablaghKolTafkik]  DEFAULT ((0)) FOR [MablaghKolTafkik]
GO

ALTER TABLE [Sales].[TafkikJozeTasfiehSatr] ADD  CONSTRAINT [DF_TafkikJozeTasfiehSatr_MablaghNaghd]  DEFAULT ((0)) FOR [MablaghNaghd]
GO

ALTER TABLE [Sales].[TafkikJozeTasfiehSatr] ADD  CONSTRAINT [DF_TafkikJozeTasfiehSatr_MablaghChek]  DEFAULT ((0)) FOR [MablaghChek]
GO

ALTER TABLE [Sales].[TafkikJozeTasfiehSatr] ADD  CONSTRAINT [DF_TafkikJozeTasfiehSatr_MablaghKartKhan]  DEFAULT ((0)) FOR [MablaghKartKhan]
GO

ALTER TABLE [Sales].[TafkikJozeTasfiehSatr] ADD  CONSTRAINT [DF_TafkikJozeTasfiehSatr_MablaghTakhfif]  DEFAULT ((0)) FOR [MablaghTakhfif]
GO

ALTER TABLE [Sales].[TafkikJozeTasfiehSatr] ADD  CONSTRAINT [DF_TafkikJozeTasfiehSatr_MablaghMarjoee]  DEFAULT ((0)) FOR [MablaghMarjoee]
GO

ALTER TABLE [Sales].[TafkikJozeTasfiehSatr] ADD  CONSTRAINT [DF_TafkikJozeTasfiehSatr_MablaghResid]  DEFAULT ((0)) FOR [MablaghResid]
GO

ALTER TABLE [Sales].[TafkikJozeTasfiehSatr] ADD  CONSTRAINT [DF_TafkikJozeTasfiehSatr_TarikhEntry]  DEFAULT (getdate()) FOR [TarikhEntry]
GO

ALTER TABLE [Sales].[TafkikJozeTasfiehSatr] ADD  CONSTRAINT [DF_TafkikJozeTasfiehSatr_ModifiedDate]  DEFAULT (getdate()) FOR [ModifiedDate]
GO

