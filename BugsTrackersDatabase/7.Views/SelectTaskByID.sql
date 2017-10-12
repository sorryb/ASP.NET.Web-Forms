SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW SelectTaskByID
AS
SELECT     TOP (100) PERCENT Users.UserName, CONVERT(varchar(10), History.DateCreated, 104) + ' ' + CONVERT(varchar(10), History.DateCreated, 108) 
                      AS DateCreated, ProjectStatus.StatusId, ProjectStatus.StatusName, Issues.IssueId, Issues.IssueTitle, ProjectCategories.CategoryId, 
                      ProjectCategories.CategoryName, History.DateCreated AS CreationDate
FROM         Issues INNER JOIN
                          (SELECT     IssueId,MIN(DateCreated) AS DateCreated,  IssueOwnerId,IssueStatusId
                            FROM          IssueHistory
                            GROUP BY IssueId, IssueOwnerId, IssueStatusId) AS History ON 
                      Issues.IssueId = History.IssueId INNER JOIN
                      Users ON History.IssueOwnerId = Users.UserId INNER JOIN
                      ProjectStatus ON History.IssueStatusId = ProjectStatus.StatusId INNER JOIN
                      ProjectCategories ON Issues.IssueCategoryId = ProjectCategories.CategoryId
WHERE     (ProjectStatus.ProjectId = 2) AND History.IssueStatusId is not null 
ORDER BY CreationDate asc
GO
