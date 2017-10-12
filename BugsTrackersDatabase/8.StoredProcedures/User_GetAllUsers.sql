SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO

CREATE PROCEDURE [dbo].[User_GetAllUsers] 
AS
	SELECT UserId, Username, UserPassword, DisplayName, Email, RoleName ,Roles.RoleID
	FROM 
		Users
	LEFT OUTER JOIN
		Roles
	ON
		Users.RoleId = Roles.RoleId

GO
