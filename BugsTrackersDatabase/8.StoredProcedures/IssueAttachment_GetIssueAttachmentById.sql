SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO



CREATE PROCEDURE IssueAttachment_GetIssueAttachmentById 
(
  @attachmentId INT
)
AS
SELECT
	FileName,
	ContentType,
	Attachment
FROM
	IssueAttachments
WHERE
	AttachmentId = @AttachmentId


GO
