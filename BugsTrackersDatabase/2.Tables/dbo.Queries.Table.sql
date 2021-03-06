/****** Object:  Table [dbo].[Queries]    Script Date: 07/09/2007 16:18:10 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Queries]') AND type in (N'U'))
DROP TABLE [dbo].[Queries]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Queries]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Queries](
	[QueryId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[ProjectId] [int] NOT NULL,
	[QueryName] [nvarchar](50) NOT NULL
) ON [PRIMARY]
END
GO
