SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO



CREATE PROCEDURE IssueNotification_DeleteIssueNotification
	@IssueId Int,
	@Username NVarChar(255)
AS
DECLARE @UserId Int
SELECT @UserId = UserId FROM Users WHERE Username = @Username

DELETE 
	IssueNotifications
WHERE
	IssueId = @IssueId
	AND UserId = @UserId 


GO
