SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO



CREATE PROCEDURE User_GetUsersByProjectId
	@ProjectId Int
AS
SELECT Users.UserId, Username, UserPassword, DisplayName, Email, RoleName 
FROM 
	Users
LEFT OUTER JOIN
	Roles
ON
	Users.RoleId = Roles.RoleId
LEFT OUTER JOIN
	ProjectMembers
ON
	Users.UserId = ProjectMembers.UserId
WHERE
	ProjectMembers.ProjectId = @ProjectId


GO
