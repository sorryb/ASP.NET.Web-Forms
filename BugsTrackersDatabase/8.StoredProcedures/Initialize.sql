SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO



CREATE PROCEDURE Initialize 
	@AdminUsername NVarChar(255),
	@AdminPassword NVarChar(255)
AS
DECLARE @AdminRoleId Int

INSERT Roles
(
	RoleName,
	RoleLevel
)
VALUES
(
	'Administrator',
	1
)

SET @AdminRoleId = @@IDENTITY

INSERT Roles
(
	RoleName,
	RoleLevel
)
VALUES
(
	'Project Manager',
	2
)

INSERT Roles
(
	RoleName,
	RoleLevel
)
VALUES
(
	'Consultant',
	3
)

INSERT Roles
(
	RoleName,
	RoleLevel
)
VALUES
(
	'Guest',
	4
)


INSERT Users
(
	Username,
	UserPassword,
	DisplayName,
	RoleId
)
VALUES
(
	@AdminUsername,
	@AdminPassword,
	@AdminUsername,
	@AdminRoleId
)


GO
