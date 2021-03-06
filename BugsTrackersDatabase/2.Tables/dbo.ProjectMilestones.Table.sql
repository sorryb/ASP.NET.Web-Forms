/****** Object:  Table [dbo].[ProjectMilestones]    Script Date: 07/09/2007 16:18:01 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ProjectMilestones]') AND type in (N'U'))
DROP TABLE [dbo].[ProjectMilestones]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ProjectMilestones]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ProjectMilestones](
	[MilestoneId] [int] IDENTITY(1,1) NOT NULL,
	[ProjectId] [int] NOT NULL,
	[MilestoneName] [nvarchar](50) NOT NULL,
	[MilestoneImageUrl] [nvarchar](255) NOT NULL,
	[DateCreated] [datetime] NOT NULL CONSTRAINT [DF_IssueTracker_ProjectMilestones_DateCreated]  DEFAULT (getdate())
) ON [PRIMARY]
END
GO
