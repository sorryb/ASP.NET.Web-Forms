/****** Object:  Table [dbo].[ZileNelucratoare]    Script Date: 07/09/2007 16:18:18 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ZileNelucratoare]') AND type in (N'U'))
DROP TABLE [dbo].[ZileNelucratoare]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ZileNelucratoare]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ZileNelucratoare](
	[Tara] [nvarchar](255) NULL,
	[Data] [smalldatetime] NULL
) ON [PRIMARY]
END
GO
