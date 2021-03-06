/****** Object:  Table [dbo].[Projects]    Script Date: 07/09/2007 16:18:07 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Projects]') AND type in (N'U'))
DROP TABLE [dbo].[Projects]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Projects]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Projects](
	[ProjectId] [int] IDENTITY(1,1) NOT NULL,
	[ProjectName] [nvarchar](255) NOT NULL,
	[ProjectDescription] [nvarchar](255) NOT NULL,
	[ProjectCreatorId] [int] NOT NULL,
	[ProjectManagerId] [int] NOT NULL,
	[ProjectCreationDate] [datetime] NOT NULL CONSTRAINT [DF_IssueTracker_Projects_ProjectCreationDate]  DEFAULT (getdate()),
	[ProjectDisabled] [bit] NOT NULL CONSTRAINT [DF_IssueTracker_Projects_ProjectDisabled]  DEFAULT (0),
	[EstDuration] [int] NULL CONSTRAINT [DF_Projects_EstDuration]  DEFAULT ((1)),
	[EstCompletionDate] [datetime] NULL CONSTRAINT [DF_Projects_EstCompletionDate]  DEFAULT (getdate()+(1))
) ON [PRIMARY]
END
GO
