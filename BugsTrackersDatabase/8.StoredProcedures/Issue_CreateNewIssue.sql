SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE Issue_CreateNewIssue
  @IssueTitle NVARCHAR(256),
  @ProjectId Int,
  @IssueCategoryId Int,
  @IssueStatusId Int,
  @IssuePriorityId Int,
  @IssueMilestoneId Int,
  @IssueAssignedId Int,
  @IssueOwnerId Int,
  @IssueCreatorUsername NVarChar(200)
AS
DECLARE @newIssueId Int

-- Get Creator UserID
DECLARE @IssueCreatorId Int
SELECT @IssueCreatorId = UserId FROM Users WHERE Username = @IssueCreatorUsername

BEGIN TRAN
	INSERT Issues
	(
		ProjectId,
		IssueTitle,
		IssueCategoryId,
		IssueStatusId,
		IssuePriorityId,
		IssueMilestoneId,
		IssueAssignedId,
		IssueOwnerId,
		IssueCreatorId
	)
	VALUES
	(
		@ProjectId,
		@IssueTitle,
		@IssueCategoryId,
		@IssueStatusId,
		@IssuePriorityId,
		@IssueMilestoneId,
		@IssueAssignedId,
		@IssueOwnerId,
		@IssueCreatorId
	)
	
	SET @newIssueId =  @@IDENTITY
	
	
	INSERT IssueHistory
	(
		IssueId,
		HistoryUserId
	)
	VALUES
	(
		@newIssueId,
		@IssueCreatorId
	)

COMMIT TRAN

RETURN @newIssueId


GO
