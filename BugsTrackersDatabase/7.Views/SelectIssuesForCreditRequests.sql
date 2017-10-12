SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE  view SelectIssuesForCreditRequests 
as 
SELECT     Issues.IssueId,issues.IssueAssignedId,issues.IssuestatusId, Issues.IssueTitle, Issues.DateCreated, Issues.Disabled, ProjectCategories.CategoryName, ProjectMilestones.MilestoneName, 
                      ProjectPriorities.PriorityName, ProjectStatus.StatusName, Users.DisplayName as Asignat, Users.UserName as Creator
FROM         ProjectStatus INNER JOIN
                      Users INNER JOIN
                      ProjectMembers ON Users.UserId = ProjectMembers.UserId INNER JOIN
                      Issues ON ProjectMembers.ProjectId = Issues.ProjectId AND Users.UserId = Issues.IssueCreatorId AND 
                      Users.UserId = Issues.IssueOwnerId INNER JOIN
                      ProjectCategories ON Issues.IssueCategoryId = ProjectCategories.CategoryId AND ProjectMembers.ProjectId = ProjectCategories.ProjectId INNER JOIN
                      ProjectMilestones ON Issues.IssueMilestoneId = ProjectMilestones.MilestoneId AND 
                      ProjectMembers.ProjectId = ProjectMilestones.ProjectId INNER JOIN
                      ProjectPriorities ON Issues.IssuePriorityId = ProjectPriorities.PriorityId AND ProjectMembers.ProjectId = ProjectPriorities.ProjectId ON 
                      ProjectStatus.StatusId = Issues.IssueStatusId AND ProjectStatus.ProjectId = ProjectMembers.ProjectId
WHERE     (ProjectMembers.ProjectId = 2)
GO
