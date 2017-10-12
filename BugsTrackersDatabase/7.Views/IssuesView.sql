SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE VIEW dbo.IssuesView
AS
SELECT     dbo.Issues.*, ISNULL(dbo.ProjectStatus.StatusName, 'none') AS StatusName, 
                      ISNULL(dbo.ProjectStatus.StatusImageUrl, '') AS StatusImageUrl, ISNULL(dbo.ProjectPriorities.PriorityName, 'none') 
                      AS PriorityName, ISNULL(dbo.ProjectPriorities.PriorityImageUrl, '') AS PriorityImageUrl, 
                      ISNULL(dbo.ProjectMilestones.MilestoneName, 'none') AS MilestoneName, 
                      ISNULL(dbo.ProjectMilestones.MilestoneImageUrl, '') AS MilestoneImageUrl, ISNULL(AssignedUsers.DisplayName, 'none') 
                      AS AssignedDisplayName, ISNULL(OwnerUsers.DisplayName, 'none') AS OwnerDisplayName, ISNULL(CreatorUsers.DisplayName, 'none') 
                      AS CreatorDisplayName, ISNULL(CreatorUsers.UserName, 'none') AS CreatorUsername, ISNULL(dbo.ProjectCategories.CategoryName, 
                      'none') AS CategoryName
FROM         dbo.Issues LEFT OUTER JOIN
                      dbo.ProjectStatus ON dbo.Issues.IssueStatusId = dbo.ProjectStatus.StatusId LEFT OUTER JOIN
                      dbo.ProjectPriorities ON dbo.Issues.IssuePriorityId = dbo.ProjectPriorities.PriorityId LEFT OUTER JOIN
                      dbo.ProjectMilestones ON 
                      dbo.Issues.IssueMilestoneId = dbo.ProjectMilestones.MilestoneId LEFT OUTER JOIN
                      Users AssignedUsers ON AssignedUsers.UserId = dbo.Issues.IssueAssignedId LEFT OUTER JOIN
                      Users OwnerUsers ON OwnerUsers.UserId = dbo.Issues.IssueOwnerId LEFT OUTER JOIN
                      Users CreatorUsers ON CreatorUsers.UserId = dbo.Issues.IssueCreatorId LEFT OUTER JOIN
                      dbo.ProjectCategories ON dbo.ProjectCategories.CategoryId = dbo.Issues.IssueCategoryId

GO
