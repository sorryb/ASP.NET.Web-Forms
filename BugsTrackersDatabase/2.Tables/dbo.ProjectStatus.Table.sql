/****** Object:  Table [dbo].[ProjectStatus]    Script Date: 07/09/2007 16:18:09 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ProjectStatus]') AND type in (N'U'))
DROP TABLE [dbo].[ProjectStatus]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ProjectStatus]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ProjectStatus](
	[StatusId] [int] IDENTITY(1,1) NOT NULL,
	[ProjectId] [int] NOT NULL,
	[StatusName] [nvarchar](50) NOT NULL,
	[StatusImageUrl] [nvarchar](255) NOT NULL,
	[DateCreated] [datetime] NOT NULL CONSTRAINT [DF_IssueTracker_IssueStatus_DateCreated]  DEFAULT (getdate())
) ON [PRIMARY]
END
GO
