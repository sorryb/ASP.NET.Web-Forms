SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO



CREATE PROCEDURE Project_GetProjectByUsername
	@Username NVarChar(255) 
AS
DECLARE @UserId Int
SELECT @UserId = UserId FROM Users WHERE Username = @Username

SELECT 
	Projects.*,
	Managers.DisplayName ProjectManagerDisplayName,
	Creators.DisplayName ProjectCreatorDisplayName 
FROM 
	Projects 
INNER JOIN ProjectMembers ON Projects.ProjectId = ProjectMembers.ProjectId
INNER JOIN Users Managers ON Managers.UserId = ProjectManagerId
INNER JOIN Users Creators ON Creators.UserId = ProjectCreatorId
WHERE 
	ProjectMembers.UserID = @UserId
	AND ProjectDisabled = 0
ORDER BY 
	ProjectName ASC


GO
