SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO



CREATE PROCEDURE User_CreateNewUser
	@Username NVarChar(250),
	@RoleName NVarChar(100),
	@Email NVarChar(250),
	@DisplayName NVarChar(250),
	@UserPassword NVarChar(250) 
AS
IF EXISTS(SELECT UserId FROM Users WHERE Username = @Username)
	RETURN 0

DECLARE @RoleId INT
SELECT @RoleId = RoleId FROM Roles WHERE RoleName = @RoleName
INSERT Users
(
	Username,
	RoleId,
	Email,
	DisplayName,
	UserPassword
) 
VALUES 
(
	@Username,
	@RoleId,
	@Email,
	@DisplayName,
	@UserPassword
)

RETURN @@IDENTITY


GO
