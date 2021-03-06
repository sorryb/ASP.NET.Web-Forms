/****** Object:  Table [dbo].[QueryClauses]    Script Date: 07/09/2007 16:18:12 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[QueryClauses]') AND type in (N'U'))
DROP TABLE [dbo].[QueryClauses]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[QueryClauses]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[QueryClauses](
	[QueryId] [int] NOT NULL,
	[BooleanOperator] [nvarchar](50) NOT NULL,
	[FieldName] [nvarchar](50) NOT NULL,
	[ComparisonOperator] [nvarchar](50) NOT NULL,
	[FieldValue] [nvarchar](50) NOT NULL,
	[DataType] [int] NOT NULL
) ON [PRIMARY]
END
GO
