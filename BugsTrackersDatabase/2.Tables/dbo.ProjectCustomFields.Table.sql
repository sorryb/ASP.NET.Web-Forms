/****** Object:  Table [dbo].[ProjectCustomFields]    Script Date: 07/09/2007 16:17:56 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ProjectCustomFields]') AND type in (N'U'))
DROP TABLE [dbo].[ProjectCustomFields]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ProjectCustomFields]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ProjectCustomFields](
	[CustomFieldId] [int] IDENTITY(1,1) NOT NULL,
	[ProjectId] [int] NOT NULL,
	[CustomFieldName] [nvarchar](50) NOT NULL,
	[CustomFieldRequired] [bit] NOT NULL,
	[CustomFieldDataType] [int] NOT NULL
) ON [PRIMARY]
END
GO
