SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO



CREATE PROCEDURE User_GetAllUsersByRoleName
	@RoleName NVarChar(50) 
AS
DECLARE @RoleLevel Int

SELECT @RoleLevel = RoleLevel FROM Roles WHERE RoleName = @RoleName

SELECT 
	Users.* 
FROM 
	Users
	INNER JOIN Roles ON Users.RoleId = Roles.RoleId
WHERE
	RoleLevel <= @RoleLevel


GO
