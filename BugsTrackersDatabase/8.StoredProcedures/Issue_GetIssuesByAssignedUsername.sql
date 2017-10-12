SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE Issue_GetIssuesByAssignedUsername 
	@ProjectId Int,
	@Username NVarChar(255)
AS
DECLARE @UserId Int
SELECT @UserId = UserId FROM Users WHERE Username = @Username
	
SELECT 
	*
FROM 
	IssuesView
WHERE
	ProjectId = @ProjectId
	AND Disabled = 0
	AND IssueAssignedId = @UserId
ORDER BY
	IssueID Desc


GO
