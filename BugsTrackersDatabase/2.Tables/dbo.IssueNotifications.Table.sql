/****** Object:  Table [dbo].[IssueNotifications]    Script Date: 07/09/2007 16:17:46 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[IssueNotifications]') AND type in (N'U'))
DROP TABLE [dbo].[IssueNotifications]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[IssueNotifications]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[IssueNotifications](
	[NotificationId] [int] IDENTITY(1,1) NOT NULL,
	[IssueId] [int] NOT NULL,
	[UserId] [int] NOT NULL
) ON [PRIMARY]
END
GO
