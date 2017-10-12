SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE Issue_UpdateIssue
  @IssueId Int,
  @IssueTitle NVARCHAR(256),
  @IssueCategoryId Int,
  @ProjectId Int,
  @IssueStatusId Int,
  @IssuePriorityId Int,
  @IssueMilestoneId Int,
  @IssueAssignedId Int,
  @IssueOwnerId Int,
  @IssueCreatorUsername NVarChar(255)
AS
DECLARE @IssueCreatorId Int
SELECT @IssueCreatorId = UserId FROM Users WHERE Username = @IssueCreatorUsername


BEGIN TRAN
	UPDATE Issues SET
		IssueTitle = @IssueTitle,
		IssueCategoryId = @IssueCategoryId,
		ProjectId = @ProjectId,
		IssueStatusId = @IssueStatusId,
		IssuePriorityId = @IssuePriorityId,
		IssueMilestoneId = @IssueMilestoneId,
		IssueAssignedId = @IssueAssignedId,
		IssueOwnerId = @IssueOwnerId
	WHERE 
		IssueId = @IssueId

	EXEC IssueHistory_CreateNewHistory @IssueId, @IssueCreatorId,@IssueOwnerId, @IssueAssignedId,@IssuePriorityId,@IssueStatusId

COMMIT TRAN



GO
