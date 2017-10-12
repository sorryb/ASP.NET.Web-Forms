SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--Text
-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--
CREATE VIEW SelectTotalsByUsersAndStatus as 
SELECT   Y.* FROM (
SELECT   TOP 100 PERCENT  PS.StatusName, selectByUser.Number, selectByUser.UserName
FROM         ProjectStatus AS PS LEFT OUTER JOIN
                          (SELECT     COUNT(*) AS Number, Users.UserName, Issues.IssueStatusId
                            FROM          Issues INNER JOIN
                                                   Users ON Issues.IssueAssignedId = Users.UserId
                            GROUP BY Issues.IssueStatusId, Issues.ProjectId, Issues.IssueAssignedId, Users.UserName
                            HAVING      (Issues.ProjectId = 2)) AS selectByUser ON PS.StatusId = selectByUser.IssueStatusId
WHERE     (selectByUser.UserName IS NOT NULL)
ORDER BY selectByUser.UserName)Y

UNION ALL
SELECT TotalOnStatus.* FROM (
SELECT     ProjectStatus.StatusName, COUNT(*) AS Number,' TotalOnStatus' as UserName
FROM         Issues INNER JOIN
                      ProjectStatus ON Issues.IssueStatusId = ProjectStatus.StatusId
GROUP BY Issues.IssueStatusId, ProjectStatus.StatusName, Issues.ProjectId
HAVING      (Issues.ProjectId = 2))TotalOnStatus

UNION ALL
SELECT Dificulty.* FROM (
SELECT      ProjectMilestones.MilestoneName as StatusName,COUNT(*) AS Number, '  Dificulty' AS UserName
FROM         Issues INNER JOIN
                      ProjectMilestones ON Issues.IssueMilestoneId = ProjectMilestones.MilestoneId
GROUP BY ProjectMilestones.MilestoneName, Issues.ProjectId
HAVING      (Issues.ProjectId = 2))Dificulty

UNION ALL
SELECT Category.* FROM (
SELECT    ProjectCategories.CategoryName as Statusname,COUNT(*) AS Number, '   Category' AS UserName
FROM         Issues INNER JOIN
                      ProjectCategories ON Issues.IssueCategoryId = ProjectCategories.CategoryId
GROUP BY ProjectCategories.CategoryName, Issues.ProjectId
HAVING      (Issues.ProjectId = 2))Category
--UNION ALL --on ownered 
--SELECT    '' as Statusname, COUNT(*) AS Number, Users.UserName
--                            FROM          Issues INNER JOIN
--                                                   Users ON Issues.IssueOwnerId = Users.UserId
--                            GROUP BY  Issues.ProjectId, Issues.IssueOwnerId, Users.UserName
--                            HAVING      (Issues.ProjectId = 2)
UNION ALL--total by company
 SELECT * FROM
(
 SELECT    'BROM' as Statusname,  COUNT(*) AS Number, '    Total by Company' as UserName
                            FROM          Issues INNER JOIN 
                                                   Users ON Issues.IssueOwnerId = Users.UserId  AND Issues.IssueOwnerId in (8,17,18,13)
                            GROUP BY  Issues.ProjectId
                            HAVING      (Issues.ProjectId = 2)
UNION ALL --all ownered by Kepler
 SELECT    'KEPLER' as Statusname,  COUNT(*) AS Number, '    Total by Company' as UserName
                            FROM          Issues INNER JOIN 
                                                   Users ON Issues.IssueOwnerId = Users.UserId  AND Issues.IssueOwnerId not in (8,17,18,13)
                            GROUP BY  Issues.ProjectId
                            HAVING      (Issues.ProjectId = 2)   
)allByCo

GO
