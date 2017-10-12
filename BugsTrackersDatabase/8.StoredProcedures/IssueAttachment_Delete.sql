SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO



CREATE PROCEDURE IssueAttachment_Delete 
(
  @attachmentId Int
)
AS
DELETE IssueAttachments
WHERE AttachmentId = @attachmentId


GO
