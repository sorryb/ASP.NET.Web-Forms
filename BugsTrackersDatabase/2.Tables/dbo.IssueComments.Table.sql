/****** Object:  Table [dbo].[IssueComments]    Script Date: 07/09/2007 16:17:42 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[IssueComments]') AND type in (N'U'))
DROP TABLE [dbo].[IssueComments]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[IssueComments]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[IssueComments](
	[CommentId] [int] IDENTITY(1,1) NOT NULL,
	[IssueId] [int] NOT NULL,
	[Comment] [ntext] NOT NULL,
	[UserId] [int] NOT NULL,
	[DateCreated] [datetime] NOT NULL CONSTRAINT [DF_IssueTracker_IssueComments_DateCreated]  DEFAULT (getdate())
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
