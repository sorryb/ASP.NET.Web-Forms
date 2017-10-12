SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




CREATE PROCEDURE [dbo].[IssueAttachment_CreateNewIssueAttachment] 
	@IssueId Int,
	@CreatorUsername NVarChar(255),
	@FileName NVarChar(250),
	@ContentType NVarChar(50),
	@Attachment Image
AS
DECLARE @AttachmentId Int

DECLARE @UserId Int
SELECT @UserId = UserId FROM Users WHERE Username = @CreatorUsername

BEGIN TRAN
	INSERT IssueAttachments
	(
		IssueId,
		UserId,
		FileName,
		ContentType,
		Attachment
	)
	VALUES
	(
		@IssueId,
		@UserId,
		@FileName,
		@ContentType,
		@Attachment
	)
	
	SET @AttachmentId = @@IDENTITY

	EXEC IssueHistory_CreateNewHistory @IssueId, @UserId,@UserId,@UserId,7,5

COMMIT TRAN
RETURN @AttachmentId



GO
