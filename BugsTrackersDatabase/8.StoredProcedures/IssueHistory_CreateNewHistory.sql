SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE IssueHistory_CreateNewHistory 
	@IssueId Int,
	@UserId Int,
@IssueOwnerId Int, @IssueAssignedId Int,@IssuePriorityId Int,@IssueStatusId Int
AS
INSERT IssueHistory
(
	IssueId,
	HistoryUserId,
	IssueOwnerId  , IssueAssignedId,    IssuePriorityId,  IssueStatusId 
)
VALUES
(
	@IssueId,
	@UserId
,@IssueOwnerId, @IssueAssignedId,@IssuePriorityId,@IssueStatusId
)




GO
