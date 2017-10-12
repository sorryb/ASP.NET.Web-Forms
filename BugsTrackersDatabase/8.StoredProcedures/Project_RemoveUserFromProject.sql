SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO



CREATE PROCEDURE Project_RemoveUserFromProject
	@UserId Int,
	@ProjectId Int 
AS
DELETE 
	ProjectMembers
WHERE
	UserId = @UserId
	AND ProjectId = @ProjectId


GO
