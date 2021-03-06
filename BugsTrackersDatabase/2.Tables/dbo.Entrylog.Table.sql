USE [BugTracker]
GO
/****** Object:  Table [dbo].[Entrylog]    Script Date: 07/09/2007 16:17:37 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Entrylog]') AND type in (N'U'))
DROP TABLE [dbo].[Entrylog]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Entrylog]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Entrylog](
	[EntryLogID] [int] IDENTITY(1,1) NOT NULL,
	[Description] [nvarchar](255) NOT NULL,
	[Duration] [decimal](10, 2) NOT NULL,
	[EntryDate] [smalldatetime] NOT NULL,
	[ProjectID] [int] NOT NULL,
	[UserID] [int] NOT NULL,
	[CategoryID] [int] NOT NULL
) ON [PRIMARY]
END
GO
