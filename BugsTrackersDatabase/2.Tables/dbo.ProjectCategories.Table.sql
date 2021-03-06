/****** Object:  Table [dbo].[ProjectCategories]    Script Date: 07/09/2007 16:17:54 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ProjectCategories]') AND type in (N'U'))
DROP TABLE [dbo].[ProjectCategories]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ProjectCategories]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ProjectCategories](
	[CategoryId] [int] IDENTITY(1,1) NOT NULL,
	[CategoryName] [nvarchar](100) NOT NULL,
	[ProjectId] [int] NOT NULL,
	[ParentCategoryId] [int] NOT NULL,
	[Abbreviation] [nchar](10) NULL CONSTRAINT [DF_ProjectCategories_Abbreviation]  DEFAULT (N'A'),
	[EstDuration] [int] NULL CONSTRAINT [DF_ProjectCategories_EstDuration]  DEFAULT ((1)),
	[StartDate] [datetime] NULL CONSTRAINT [DF_ProjectCategories_StartDate]  DEFAULT (getdate()),
	[EndDate] [datetime] NULL CONSTRAINT [DF_ProjectCategories_EndDate]  DEFAULT (getdate()+(30))
) ON [PRIMARY]
END
GO
