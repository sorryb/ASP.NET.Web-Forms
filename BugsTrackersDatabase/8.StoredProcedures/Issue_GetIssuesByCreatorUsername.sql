SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE Issue_GetIssuesByCreatorUsername 
  @ProjectId Int,
  @CreatorUsername NVarChar(255)
AS
DECLARE @CreatorId Int
SELECT @CreatorId = UserId FROM Users WHERE Username = @CreatorUsername
	
SELECT 
	*
FROM 
	IssuesView
WHERE
	ProjectId = @ProjectId
	AND Disabled = 0
	AND IssueCreatorId = @CreatorId
ORDER BY
	IssueID Desc


GO
