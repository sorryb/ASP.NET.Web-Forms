SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE Issue_DeleteIssue
	@IssueIdToDelete Int 
AS
UPDATE Issues SET
	Disabled = 1
WHERE
	IssueId = @IssueIdToDelete


GO
