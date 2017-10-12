SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE proc [dbo].[GetUserDisplayName]
	@Username nvarchar(50)
AS
	SELECT DisplayName FROM Users
	WHERE UserName = @Username



GO
