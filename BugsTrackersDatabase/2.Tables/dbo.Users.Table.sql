/****** Object:  Table [dbo].[Users]    Script Date: 07/09/2007 16:18:17 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Users]') AND type in (N'U'))
DROP TABLE [dbo].[Users]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Users]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Users](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](255) NOT NULL,
	[UserPassword] [nvarchar](20) NULL,
	[RoleId] [int] NOT NULL,
	[Email] [nvarchar](255) NOT NULL CONSTRAINT [DF_IssueTracker_Users_Email]  DEFAULT ('none'),
	[DisplayName] [nvarchar](50) NOT NULL
) ON [PRIMARY]
END
GO
