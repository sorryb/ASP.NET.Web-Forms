SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE User_GetUserByUsername
	@Username NVarchar(255)
AS
SELECT 
	UserId, Username, UserPassword, Email, DisplayName, RoleName 
FROM 
	Users
JOIN
	Roles
ON 
	Users.RoleId = Roles.RoleId
WHERE 
	Username = @Username


GO
