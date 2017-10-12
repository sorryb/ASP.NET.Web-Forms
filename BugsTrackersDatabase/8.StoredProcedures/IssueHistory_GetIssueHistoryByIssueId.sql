SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--exec IssueHistory_GetIssueHistoryByIssueId @IssueId=216


CREATE PROCEDURE IssueHistory_GetIssueHistoryByIssueId
	@IssueId Int
AS
SELECT
	HistoryId,
	IssueId,
	'Assignat la:'+U.DisplayName+' Status:'+PS.StatusName + '-' + Users.DisplayName CreatorDisplayName,
	IssueHistory.DateCreated
FROM
	IssueHistory
	INNER JOIN Users ON HistoryUserId = UserId
	INNER JOIN Users U ON IssueAssignedId = U.UserId
	INNER JOIN ProjectStatus PS ON PS.StatusId = IssueHistory.IssueStatusId
WHERE
	IssueId = @IssueId
ORDER BY
	IssueHistory.DateCreated DESC



GO
