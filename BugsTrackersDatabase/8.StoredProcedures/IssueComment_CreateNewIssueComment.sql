SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE IssueComment_CreateNewIssueComment 
	@IssueId Int,
	@CreatorUsername NVarChar(255),
	@Comment NText
AS
DECLARE @CommentId Int

DECLARE @UserId Int
SELECT @UserId = UserId FROM Users WHERE Username = @CreatorUsername

BEGIN TRAN
	INSERT IssueComments
	(
		IssueId,
		UserId,
		Comment
	)
	VALUES
	(
		@IssueId,
		@UserId,
		@Comment
	)
	
	SET @CommentId = @@IDENTITY

	EXEC IssueHistory_CreateNewHistory @IssueId, @UserId,@UserId,@UserId,5,7

COMMIT TRAN
RETURN @CommentId



GO
