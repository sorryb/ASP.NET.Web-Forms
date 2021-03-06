/****** Object:  Table [dbo].[IssueHistory]    Script Date: 07/09/2007 16:17:45 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[IssueHistory]') AND type in (N'U'))
DROP TABLE [dbo].[IssueHistory]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[IssueHistory]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[IssueHistory](
	[HistoryId] [int] IDENTITY(1,1) NOT NULL,
	[IssueId] [int] NOT NULL,
	[HistoryUserId] [int] NOT NULL,
	[DateCreated] [datetime] NOT NULL CONSTRAINT [DF_IssueTracker_IssueHistory_DateCreated]  DEFAULT (getdate()),
	[IssueOwnerId] [int] NULL,
	[IssueAssignedId] [int] NULL,
	[IssuePriorityId] [int] NULL,
	[IssueStatusId] [int] NULL
) ON [PRIMARY]
END
GO
