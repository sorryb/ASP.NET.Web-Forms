SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE IssueAttachment_GetIssueAttachmentsByIssueId
	@IssueId Int 
AS

SELECT 
	AttachmentId,
	IssueId,
	Username CreatorUsername,
	DisplayName CreatorDisplayName,
	FileName,
	DateCreated
FROM
	IssueAttachments
	INNER JOIN Users ON IssueAttachments.UserId = Users.UserId
WHERE
	IssueId = @IssueId
ORDER BY 
	DateCreated DESC


GO
