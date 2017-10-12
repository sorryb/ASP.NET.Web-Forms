SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE Project_GetProjectById
 @ProjectId INT
AS
SELECT
	ProjectId,
	ProjectName,
	ProjectDescription,
	ProjectManagerId,
	ProjectCreatorId,
	ProjectCreationDate,
	Managers.DisplayName ProjectManagerDisplayName,
	Creators.DisplayName ProjectCreatorDisplayName
FROM 
	Projects 
	INNER JOIN Users Managers ON Managers.UserId = ProjectManagerId	
	INNER JOIN Users Creators ON Creators.UserId = ProjectCreatorId	
WHERE
	ProjectId = @ProjectId


GO
