/****** Object:  Table [dbo].[ProjectPriorities]    Script Date: 07/09/2007 16:18:03 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ProjectPriorities]') AND type in (N'U'))
DROP TABLE [dbo].[ProjectPriorities]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ProjectPriorities]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ProjectPriorities](
	[PriorityId] [int] IDENTITY(1,1) NOT NULL,
	[ProjectId] [int] NOT NULL,
	[PriorityName] [nvarchar](50) NOT NULL,
	[PriorityImageUrl] [nvarchar](255) NOT NULL,
	[DateCreated] [datetime] NOT NULL CONSTRAINT [DF_IssueTracker_ProjectPriorities_DateCreated]  DEFAULT (getdate())
) ON [PRIMARY]
END
GO
