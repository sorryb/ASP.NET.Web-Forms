/****** Object:  Table [dbo].[Issues]    Script Date: 07/09/2007 16:17:51 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Issues]') AND type in (N'U'))
DROP TABLE [dbo].[Issues]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Issues]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Issues](
	[IssueId] [int] IDENTITY(1,1) NOT NULL,
	[ProjectId] [int] NOT NULL,
	[IssueTitle] [nvarchar](255) NOT NULL,
	[IssueCategoryId] [int] NOT NULL,
	[IssueMilestoneId] [int] NOT NULL,
	[IssuePriorityId] [int] NOT NULL,
	[IssueStatusId] [int] NOT NULL,
	[IssueCreatorId] [int] NOT NULL,
	[IssueOwnerId] [int] NOT NULL,
	[IssueAssignedId] [int] NOT NULL,
	[DateCreated] [datetime] NOT NULL CONSTRAINT [DF_IssueTracker_Issues_DateCreated]  DEFAULT (getdate()),
	[Disabled] [bit] NOT NULL CONSTRAINT [DF_IssueTracker_Issues_Disabled]  DEFAULT ((0))
) ON [PRIMARY]
END
GO
