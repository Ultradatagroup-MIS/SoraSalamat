USE [DB_Pakhsh]
GO

/****** Object:  StoredProcedure [Report].[spGozareshRoozaneh_FaktorMarjoeeRoozaneh]    Script Date: 08/31/2013 12:10:09 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- Exec Report.spGozareshRoozaneh_FaktorMarjoeeRoozaneh 26,'13920321',0
CREATE PROCEDURE [Report].[spGozareshRoozaneh_FaktorMarjoeeRoozaneh]
		@CodeMahal AS INT,
		@TarikhElamMarjoee AS NVARCHAR(8),
		@ccGorohForosh AS INT,
		@strForoshandeh AS NVARCHAR(1000)
AS
BEGIN 
			SELECT	ISNULL(h.FName + ' ' + h.LName, '-----') AS NameSarparast, ISNULL(f.ccGorohForosh, 0) AS ccGorohForosh,
					ISNULL(g.SharhGorohForosh, 0) AS SharhGorohForosh, ISNULL(c.FName + ' ' + c.LName, '-----') AS NameForoshandeh,
					d.NameMoshtary, ISNULL(SUM(f.JamFaktor), 0) AS MablaghFaktor, ISNULL(SUM(f.JamKol), 0) AS JamFaktor,
					e.Sharh AS txtElatMarjoee, dbo.SetDateSlash(a.TarikhElamMarjoee) AS TarikhElamMarjoee
			FROM	
					tblFO_ElamMarjoee AS a WITH(NOLOCK) LEFT OUTER JOIN
					tblFO_Foroshandeh AS b WITH(NOLOCK) ON a.ccForoshandeh = b.ccForoshandeh LEFT OUTER JOIN
					tblGL_MoshakhasatFardi AS c WITH(NOLOCK) ON b.CodeFard = c.CodeFard LEFT OUTER JOIN
					tblFO_Moshtary AS d WITH(NOLOCK) ON a.ccMoshtary = d.ccMoshtary LEFT OUTER JOIN
					tblGL_ShenasehOmomi as e WITH(NOLOCK) ON a.sElat = e.Code LEFT OUTER JOIN
					tblFO_Faktor AS f WITH(NOLOCK) ON a.ccFaktorTitr = f.ccFaktorTitr LEFT OUTER JOIN
					tblFO_GorohForosh AS g WITH(NOLOCK) ON f.ccGorohForosh = g.ccGorohForosh LEFT OUTER JOIN
					tblGL_MoshakhasatFardi AS h WITH(NOLOCK) ON g.CodeFard_Sarparast = h.CodeFard
			WHERE	
					(a.TarikhElamMarjoee = @TarikhElamMarjoee) AND (a.CodeMahal = @CodeMahal)
					AND (f.ccGorohForosh = @ccGorohForosh OR @ccGorohForosh = 0) 
					AND (@strForoshandeh LIKE N'%,' +  LTRIM(RTRIM(STR(a.ccForoshandeh))) + ',%' OR @strForoshandeh = '')
					--AND FaktorMarjoee = 1
			GROUP BY
					h.FName, h.LName, c.FName, c.LName, a.TarikhElamMarjoee,
					g.SharhGorohForosh, f.ccGorohForosh, d.NameMoshtary, e.Sharh
END
GO

---------------------------------------------------------------

USE [DB_Pakhsh]
GO

/****** Object:  StoredProcedure [Report].[spGozareshRoozaneh_Foroshandeh_Brand_Forosh]    Script Date: 08/31/2013 12:12:53 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- EXEC Report.spGozareshRoozaneh_Foroshandeh_Brand_Forosh '13910101','13911230', 0

CREATE PROCEDURE [Report].[spGozareshRoozaneh_Foroshandeh_Brand_Forosh]
		 @CodeMahal AS INT,
         @AzTarikh AS NVARCHAR(8),
         @TaTarikh AS NVARCHAR(8),
         @ccGorohForosh AS INT,
         @strForoshandeh AS NVARCHAR(1000),
         @strBrand AS NVARCHAR(1000)
AS
BEGIN
			SELECT	CodeDoreh,NameBrand,SUM(Mkol3) AS mablagh,
					NameForoshandehShow2 AS NameForoshandeh
			FROM	qryFO_ForoshTahlil 
			WHERE	CodeMahal = @CodeMahal 
					AND IsJayezeh <> 1
					AND FaktorTarikh BETWEEN @AzTarikh AND @TaTarikh
					AND (ccGorohForosh = @ccGorohForosh OR @ccGorohForosh = 0)
					AND (@strForoshandeh LIKE N'%,' +  LTRIM(RTRIM(STR(ccForoshandeh))) + ',%' OR @strForoshandeh = '')
					AND (@strBrand LIKE N'%,' +  LTRIM(RTRIM(STR(ccBrand))) + ',%' OR @strBrand = '')
			GROUP BY  CodeDoreh,NameBrand,NameForoshandehShow2
END
GO

---------------------------------------------------------------


USE [DB_Pakhsh]
GO

/****** Object:  StoredProcedure [Report].[spGozareshRoozaneh_Foroshandeh_Kala_Forosh]    Script Date: 08/31/2013 12:14:10 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- EXEC Report.spGozareshRoozaneh_Foroshandeh_Kala_Forosh 1,'13910101','13911230',0,',102,'

CREATE PROCEDURE [Report].[spGozareshRoozaneh_Foroshandeh_Kala_Forosh]
         @CodeMahal AS INT,
         @AzTarikh AS NVARCHAR(8),
         @TaTarikh AS NVARCHAR(8),
         @ccGorohForosh AS INT,
         @strForoshandeh AS NVARCHAR(1000),
         @strKala AS NVARCHAR(1000)
AS
BEGIN
			
			SELECT	CodeDoreh,NameKala,SUM(Mkol3) AS mablagh, 
					NameForoshandehShow2 AS NameForoshandeh
			FROM	qryFO_ForoshTahlil 
			WHERE	CodeMahal = @CodeMahal 
					AND IsJayezeh <> 1
					AND FaktorTarikh BETWEEN @AzTarikh AND @TaTarikh
					AND (ccGorohForosh = @ccGorohForosh OR @ccGorohForosh = 0)
					AND (@strForoshandeh LIKE N'%,' +  LTRIM(RTRIM(STR(ccForoshandeh))) + ',%' OR @strForoshandeh = '')
					AND (@strKala LIKE N'%,' +  LTRIM(RTRIM(STR(ccKala))) + ',%' OR @strKala = '')
			GROUP BY  CodeDoreh,NameKala,NameForoshandehShow2
END
GO

------------------------------------------------------------------

USE [DB_Pakhsh]
GO

/****** Object:  StoredProcedure [Report].[spGozareshRoozaneh_LoadForoshandeh]    Script Date: 08/31/2013 12:15:09 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- Exec Report.spGozareshRoozaneh_LoadForoshandeh 1,4056,0,'','',Administrator
CREATE PROCEDURE [Report].[spGozareshRoozaneh_LoadForoshandeh]
			@CodeMahal AS INT,
			@sVazeiat AS INT,
			@ccGorohForosh AS INT,
			@strNameForoshandeh AS NVARCHAR(500),
			@strSearch AS NVARCHAR(500),
			@UserName AS NVARCHAR(50)
AS
BEGIN 
			SELECT	(tblMoshakhasatFardi.LName + N' ' + tblMoshakhasatFardi.FName) AS LN,
					(tblMoshakhasatFardi.FName + N' ' + tblMoshakhasatFardi.LName) AS NameForoshandeh,
					tblForoshandeh.ccForoshandeh
			FROM	
					tblFO_Foroshandeh AS tblForoshandeh WITH (NOLOCK) LEFT OUTER JOIN
					tblGL_MoshakhasatFardi AS tblMoshakhasatFardi WITH (NOLOCK) ON tblForoshandeh.CodeFard = tblMoshakhasatFardi.CodeFard
			WHERE     
					(tblForoshandeh.ccForoshandeh <> 0) AND
					(tblForoshandeh.CodeMahal = @CodeMahal OR @CodeMahal = 0) AND
					(tblForoshandeh.sVazeiat <> @sVazeiat OR @sVazeiat = 0) AND 
					(tblForoshandeh.ccGorohForosh = @ccGorohForosh OR @ccGorohForosh = 0) AND
					(tblMoshakhasatFardi.LName + N' ' + tblMoshakhasatFardi.FName LIKE N'%' + LTRIM(RTRIM(@strNameForoshandeh)) + '%' OR @strNameForoshandeh = '') AND
					(@StrSearch LIKE N'%,' + LTRIM(RTRIM(tblForoshandeh.ccForoshandeh)) + ',%' OR @StrSearch = '') AND
					(NOT EXISTS
								  (SELECT     CodeSubSystem, NameKarbar, PK, UserName, Tarikh, Saat
									 FROM         tblGL_SecurityData
									 WHERE     (NameKarbar = @UserName OR @UserName = '') AND
											   (CodeSubSystem = 628) AND 
											   (PK = tblForoshandeh.ccForoshandeh)
									)
					) 
			ORDER BY
					tblMoshakhasatFardi.LName
END
GO

------------------------------------------------------------------


USE [DB_Pakhsh]
GO

/****** Object:  StoredProcedure [Report].[spGozareshRoozaneh_SearchBrand]    Script Date: 08/31/2013 12:16:33 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- Exec Report.spGozareshRoozaneh_SearchBrand ''
CREATE PROCEDURE [Report].[spGozareshRoozaneh_SearchBrand]
		@StrNameBrand AS NVARCHAR(500)
AS
BEGIN 
			SELECT	ccBrand, NameBrand
			FROM	tblFO_Brand
			WHERE	(NameBrand LIKE N'%' + LTRIM(RTRIM(@StrNameBrand)) + '%' OR @StrNameBrand = '')
END
GO

------------------------------------------------------------------

USE [DB_Pakhsh]
GO

/****** Object:  StoredProcedure [Report].[spGozareshRoozaneh_SearchForoshRoozaneh]    Script Date: 08/31/2013 12:17:37 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- Exec Report.spGozareshRoozaneh_SearchForoshRoozaneh 1,'13920217',0
CREATE PROCEDURE [Report].[spGozareshRoozaneh_SearchForoshRoozaneh]
		@CodeMahal AS INT,
		@FaktorTarikh AS NVARCHAR(8),
		@ccGorohForosh AS INT,
		@strForoshandeh AS NVARCHAR(1000)
AS
BEGIN 
			SELECT		g.FName + ' ' + g.LName AS NameSarparast,
						ISNULL(e.FName + ' ' + e.LName, '-----') AS NameForoshandeh,
						SUM(CAST(a.JamFaktor AS BIGINT)) AS MablaghKol, SUM(CAST(a.JamKol AS BIGINT)) AS JamKol,
						COUNT(DISTINCT a.ccFaktorTitr) AS TedadFaktor, COUNT(DISTINCT a.ccMoshtary) AS TedadMoshtary, 
						COUNT(b.ccFaktorSatr) AS TedadSatr, dbo.SetDateSlash(a.FaktorTarikh) AS FaktorTarikh,
						f.SharhGorohForosh, f.ccGorohForosh
			FROM          tblFO_Faktor AS a WITH (NOLOCK) LEFT OUTER JOIN
						  tblFO_FaktorSatr AS b WITH (NOLOCK) ON a.ccFaktorTitr = b.ccFaktorTitr LEFT OUTER JOIN
						  tblFO_PishFaktor AS c WITH (NOLOCK) ON a.ccPishFaktor = c.ccPishFaktorTitr LEFT OUTER JOIN
						  tblFO_Foroshandeh AS d WITH (NOLOCK) ON c.ccForoshandeh = d.ccForoshandeh LEFT OUTER JOIN
						  tblGL_MoshakhasatFardi AS e WITH (NOLOCK) ON d.CodeFard = e.CodeFard LEFT OUTER JOIN
						  tblFO_GorohForosh AS f WITH (NOLOCK) ON d.ccGorohForosh = f.ccGorohForosh LEFT OUTER JOIN
						  tblGL_MoshakhasatFardi AS g WITH (NOLOCK) ON f.CodeFard_Sarparast = g.CodeFard LEFT OUTER JOIN
						  tblGL_MarkazPakhsh AS h WITH (NOLOCK) ON a.CodeMahal = h.CodeMahal
			WHERE		(a.FaktorTarikh = @FaktorTarikh) AND (a.CodeMahal = @CodeMahal)
						AND (a.ccGorohForosh = @ccGorohForosh OR @ccGorohForosh = 0)
						AND (@strForoshandeh LIKE N'%,' +  LTRIM(RTRIM(STR(c.ccForoshandeh))) + ',%' OR @strForoshandeh = '')
			GROUP BY	h.NameMahal, g.FName, g.LName, e.FName, e.LName,
						a.FaktorTarikh, f.SharhGorohForosh, f.ccGorohForosh
END
GO

------------------------------------------------------------------

USE [DB_Pakhsh]
GO

/****** Object:  StoredProcedure [Report].[spGozareshRoozaneh_SearchItem]    Script Date: 08/31/2013 12:18:37 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- Exec Report.spGozareshRoozaneh_SearchItem ',155,158,', 1
CREATE PROCEDURE [Report].[spGozareshRoozaneh_SearchItem]
		@StrSearch AS NVARCHAR(500),
		@Noe AS BIT
AS
BEGIN
		IF @Noe = 0
		BEGIN
			SELECT	ccKala, CodeKala, NameKala
			FROM	tblAN_Kala
			WHERE	Faal = 1
					AND (@StrSearch LIKE N'%,' + LTRIM(RTRIM(ccKala)) + ',%' OR @StrSearch = '')
		END
		ELSE IF @Noe = 1
		BEGIN
			SELECT	ccBrand, NameBrand
			FROM	tblFO_Brand
			WHERE	(@StrSearch LIKE N'%,' + LTRIM(RTRIM(ccBrand)) + ',%' OR @StrSearch = '')
		END 
END
GO

------------------------------------------------------------------

USE [DB_Pakhsh]
GO

/****** Object:  StoredProcedure [Report].[spGozareshRoozaneh_SearchKala]    Script Date: 08/31/2013 12:19:36 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- Exec Report.spGozareshRoozaneh_SearchKala '”Ê”'
CREATE PROCEDURE [Report].[spGozareshRoozaneh_SearchKala]
		@StrNameKala AS NVARCHAR(500)
AS
BEGIN 
			SELECT	ccKala, CodeKala, NameKala
			FROM	tblAN_Kala
			WHERE	Faal = 1
					AND (NameKala LIKE N'%' + LTRIM(RTRIM(@StrNameKala)) + '%' OR @StrNameKala = '')
END
GO