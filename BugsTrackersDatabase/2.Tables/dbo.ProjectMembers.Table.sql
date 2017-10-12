/****** Object:  Table [dbo].[ProjectMembers]    Script Date: 07/09/2007 16:17:58 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ProjectMembers]') AND type in (N'U'))
DROP TABLE [dbo].[ProjectMembers]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ProjectMembers]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ProjectMembers](
	[UserId] [int] NOT NULL,
	[ProjectId] [int] NOT NULL
) ON [PRIMARY]
END
GO
