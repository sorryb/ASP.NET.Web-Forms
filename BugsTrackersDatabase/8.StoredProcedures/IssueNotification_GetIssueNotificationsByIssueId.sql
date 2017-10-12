SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE IssueNotification_GetIssueNotificationsByIssueId 
	@IssueId Int
AS
SELECT 
	NotificationId,
	IssueId,
	Username NotificationUsername,
	DisplayName NotificationDisplayName,
	Email NotificationEmail
FROM
	IssueNotifications
	INNER JOIN Users ON IssueNotifications.UserId = Users.UserId
WHERE
	IssueId = @IssueID
ORDER BY
	DisplayName


GO
