/****** Object:  Table [dbo].[RelatedIssues]    Script Date: 07/09/2007 16:18:13 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RelatedIssues]') AND type in (N'U'))
DROP TABLE [dbo].[RelatedIssues]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RelatedIssues]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[RelatedIssues](
	[PrimaryIssueId] [int] NOT NULL,
	[SecondaryIssueId] [int] NOT NULL,
	[RelationType] [int] NOT NULL
) ON [PRIMARY]
END
GO
