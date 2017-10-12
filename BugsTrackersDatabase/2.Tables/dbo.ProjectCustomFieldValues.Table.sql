/****** Object:  Table [dbo].[ProjectCustomFieldValues]    Script Date: 07/09/2007 16:17:58 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ProjectCustomFieldValues]') AND type in (N'U'))
DROP TABLE [dbo].[ProjectCustomFieldValues]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ProjectCustomFieldValues]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ProjectCustomFieldValues](
	[IssueId] [int] NOT NULL,
	[CustomFieldId] [int] NOT NULL,
	[CustomFieldValue] [nvarchar](255) NOT NULL
) ON [PRIMARY]
END
GO
