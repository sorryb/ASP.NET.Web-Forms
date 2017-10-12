SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO



CREATE PROCEDURE User_GetUserById
	@UserId Int
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
	UserId = @UserId


GO
