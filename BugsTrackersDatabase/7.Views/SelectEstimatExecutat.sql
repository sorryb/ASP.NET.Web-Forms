SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create view SelectEstimatExecutat
AS 
SELECT TOP 100 PERCENT issues.IssueTitle,InLucru.IssueId,PC.CategoryName,Users.UserName,DataInLucru,DataTerminat,DATEDIFF(hh,DataInLucru,DataTerminat) as Executat,IssueEstimated.CustomFieldValue Estimat
FROM 
(SELECT  TOP 100 PERCENT   IssueId, HistoryUserId,MIN(DateCreated) AS DataInLucru
FROM         IssueHistory
GROUP BY IssueId, HistoryUserId,IssueStatusId
HAVING      (IssueStatusId =6)
ORDER BY IssueId, MIN(DateCreated)) InLucru INNER JOIN 
(
SELECT   TOP 100 PERCENT    IssueId, HistoryUserId,MIN(DateCreated) AS DataTerminat
FROM         IssueHistory
GROUP BY IssueId, HistoryUserId,IssueStatusId
HAVING      (IssueStatusId =7)
ORDER BY IssueId, MIN(DateCreated)
)Terminate ON InLucru.IssueId = Terminate.IssueId AND InLucru.HistoryUserId = Terminate.HistoryUserId
INNER JOIN Users on Users.UserID = Terminate.HistoryUserId
INNER JOIN (SELECT * FROM  ISSUES WHERE issues.ProjectId = 2 ) issues ON issues.issueID = Terminate.IssueId 
INNER JOIN ProjectCategories PC on PC.CategoryId = issues.IssueCategoryId
LEFT JOIN 
(
SELECT     ProjectCustomFieldValues.IssueId, ProjectCustomFields.CustomFieldName, ProjectCustomFieldValues.CustomFieldValue, 
                      ProjectCustomFieldValues.CustomFieldId
FROM         ProjectCustomFields INNER JOIN
                      ProjectCustomFieldValues ON ProjectCustomFields.CustomFieldId = ProjectCustomFieldValues.CustomFieldId
) IssueEstimated on IssueEstimated.IssueId = issues.IssueId
ORDER BY issues.issueID ASC,InLucru.DataInLucru ASC



--select * from issueHIstory where issueID = 255


GO
