SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO







CREATE   PROCEDURE [dbo].[ListProjectsWithMembership]
(
	@QueryUserID int,
	@UserID int
)
AS

DECLARE @@QueryUserRoleID int
SELECT @@QueryUserRoleID = Users.RoleID FROM Users WHERE Users.UserID = @QueryUserID

IF @@QueryUserRoleID = 1 OR @QueryUserID = @UserID
  BEGIN
	SELECT 	Projects.ProjectID,
		ProjectName, 
		ProjectDescription, 
		ProjectManagerId, 
		EstCompletionDate, 
		EstDuration
	FROM Projects 
	INNER JOIN ProjectMembers ON ProjectMembers.ProjectID = Projects.ProjectID
	WHERE UserID = @UserID
  END

IF @@QueryUserRoleID = 2
  BEGIN
	SELECT 	Projects.ProjectID,
		ProjectName, 
		ProjectDescription, 
		ProjectManagerId, 
		EstCompletionDate, 
		EstDuration
	FROM Projects 
	INNER JOIN ProjectMembers ON ProjectMembers.ProjectID = Projects.ProjectID
	WHERE UserID = @UserID AND ProjectManagerId = @QueryUserID
  END



GO
