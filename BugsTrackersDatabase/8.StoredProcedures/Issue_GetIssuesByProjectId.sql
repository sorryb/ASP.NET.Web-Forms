SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE Issue_GetIssuesByProjectId 
	@ProjectId Int
AS
SELECT 
	*
FROM 
	IssuesView
WHERE
	ProjectId = @ProjectId
	AND Disabled = 0
ORDER BY
	IssueID Desc


GO
