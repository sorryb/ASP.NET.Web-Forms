SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE IssueNotification_CreateNewIssueNotification
	@IssueId Int,
	@NotificationUsername NVarChar(255) 
AS
DECLARE @UserId Int
SELECT @UserId = UserId FROM Users WHERE Username = @NotificationUsername

IF NOT EXISTS( SELECT NotificationId FROM IssueNotifications WHERE UserId = @UserId AND IssueId = @IssueId)
BEGIN
	INSERT IssueNotifications
	(
		IssueId,
		UserId
	)
	VALUES
	(
		@IssueId,
		@UserId
	)
END


GO
