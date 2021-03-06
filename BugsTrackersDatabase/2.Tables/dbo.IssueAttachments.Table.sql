/****** Object:  Table [dbo].[IssueAttachments]    Script Date: 07/09/2007 16:17:40 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[IssueAttachments]') AND type in (N'U'))
DROP TABLE [dbo].[IssueAttachments]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[IssueAttachments]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[IssueAttachments](
	[AttachmentId] [int] IDENTITY(1,1) NOT NULL,
	[IssueId] [int] NOT NULL,
	[UserId] [int] NOT NULL,
	[DateCreated] [datetime] NOT NULL CONSTRAINT [DF_IssueTracker_IssueAttachments_DateCreated]  DEFAULT (getdate()),
	[FileName] [nvarchar](250) NOT NULL,
	[ContentType] [nvarchar](50) NOT NULL,
	[Attachment] [image] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
