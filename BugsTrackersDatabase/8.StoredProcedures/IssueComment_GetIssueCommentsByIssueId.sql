SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE IssueComment_GetIssueCommentsByIssueId
	@IssueId Int 
AS

SELECT 
	CommentId,
	IssueId,
	Comment,
	Username CreatorUsername,
	DisplayName CreatorDisplayName,
	DateCreated
FROM
	IssueComments
	INNER JOIN Users ON IssueComments.UserId = Users.UserId
WHERE
	IssueId = @IssueId
ORDER BY 
	DateCreated DESC


GO
