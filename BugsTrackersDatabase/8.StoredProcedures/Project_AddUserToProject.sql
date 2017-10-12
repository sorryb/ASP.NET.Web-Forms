SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE Project_AddUserToProject
	@UserId Int,
	@ProjectId Int 
AS
IF NOT EXISTS (SELECT UserId FROM ProjectMembers WHERE UserId = @UserId AND ProjectId = @ProjectId)
BEGIN
	INSERT ProjectMembers
	(
		UserId,
		ProjectId
	)
	VALUES
	(
		@UserId,
		@ProjectId
	)
END


GO
