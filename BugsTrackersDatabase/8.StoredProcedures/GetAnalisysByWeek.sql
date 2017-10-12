
CREATE procedure getWeekAnalisys(@year as int)
AS
SELECT 
deschise.An,deschise.Saptam,Initial,Noi,
ISNULL(TotalRedeschise ,0) TotalRedeschise,ISNULL(DEV ,0) RedeschiseDEV,ISNULL((TotalRedeschise -PeDev.DEV),0) as RedeschisePROD,ISNULL(regresii ,0) Regresii,
Minor,Major,Critic,
InchiseInTermen, InchiseInAfaraTermen, InchiseIntern ,
InLucru, Amanate, Invalide,Evolutii
FROM
(
SELECT     
DATEPART(yy, Issues.DateCreated)  An,
DATENAME(ww, Issues.DateCreated) AS Saptam,
 dbo.GetInitialNumberOfBugs( DATENAME(ww, Issues.DateCreated),DATENAME(yy, Issues.DateCreated),5) as Initial,
Count(Issues.DateCreated) as Noi
FROM         ProjectCategories INNER JOIN Issues
                       ON ProjectCategories.CategoryId = Issues.IssueCategoryId 
GROUP BY  DATEPART(yy, Issues.DateCreated) , DATENAME(ww, Issues.DateCreated) ,ProjectCategories.CategoryID ,ProjectCategories.CategoryName ,ProjectCategories.ProjectId,dbo.GetInitialNumberOfBugs( DATENAME(ww, Issues.DateCreated),DATENAME(yy, Issues.DateCreated),5)
HAVING  ProjectCategories.ProjectId = 2 AND  ProjectCategories.CategoryID = 2 AND DATEPART(yy, Issues.DateCreated) =@year
--ORDER BY DATEPART(yy, Issues.DateCreated) , CONVERT(int,DATENAME(ww, Issues.DateCreated))
) Deschise
 LEFT JOIN
(
SELECT -- *   
DATEPART(yy, IssueHistory.DateCreated)  An,
CONVERT(int,DATENAME(ww, IssueHistory.DateCreated)) AS Saptam,
Count(ProjectStatus.StatusId) as TotalRedeschise
FROM       IssueHistory 
INNER JOIN dbo.ProjectStatus ON dbo.ProjectStatus.statusId = IssueHistory.IssuestatusId 
WHERE  dbo.ProjectStatus.StatusName = 'redeschis' AND ProjectStatus.ProjectId = 2 and DATEPART(yy, IssueHistory.DateCreated) = @year
GROUP BY  DATEPART(yy, IssueHistory.DateCreated) , DATENAME(ww, IssueHistory.DateCreated) 
--ORDER BY Saptam
) ReDeschise ON Deschise.An = Redeschise.An AND Deschise.Saptam = Redeschise.Saptam
LEFt JOIN --- redeschise DEV
(

SELECT
    DATEPART(yy, Issues.DateCreated) AS An, CONVERT(int, DATENAME(ww, Issues.DateCreated)) AS Saptam, COUNT(Issues.issueId) 
                      AS DEV
FROM
(
SELECT   IssueHistory.IssueID,MAX(Issues.DateCreated)DateCreated
FROM        Issues INNER JOIN  
                      IssueHistory on Issues.IssueID = IssueHistory.IssueId  INNER JOIN
                      ProjectStatus ON ProjectStatus.StatusId = IssueHistory.IssueStatusId INNER JOIN
                      ProjectCustomFieldValues ON IssueHistory.IssueId = ProjectCustomFieldValues.IssueId INNER JOIN
                      ProjectCustomFields ON ProjectCustomFieldValues.CustomFieldId = ProjectCustomFields.CustomFieldId
WHERE   
  (ProjectStatus.StatusName = 'redeschis') AND 
(ProjectStatus.ProjectId = 2) AND 
(DATEPART(yy, IssueHistory.DateCreated) = @year) AND 
 (ProjectCustomFields.CustomFieldName = N'Mediu Dezvoltare(DEV sau PROD)') AND 
ProjectCustomFieldValues.CustomFieldValue Like 'DEV' 
GROUP BY IssueHistory.IssueId
)Issues
GROUP BY DATEPART(yy, Issues.DateCreated), DATENAME(ww, Issues.DateCreated)
)PeDEV   ON Deschise.An =PeDEV.An AND Deschise.Saptam = PeDEV.Saptam
LEFT JOIN --regresii
(
SELECT
    DATEPART(yy, Issues.DateCreated) AS An, CONVERT(int, DATENAME(ww, Issues.DateCreated)) AS Saptam, COUNT(Issues.issueId) 
                      AS REGRESIi
FROM
(
SELECT   IssueHistory.IssueID,MAX(Issues.DateCreated)DateCreated
FROM        Issues INNER JOIN  
                      IssueHistory on Issues.IssueID = IssueHistory.IssueId  INNER JOIN
                      ProjectStatus ON ProjectStatus.StatusId = IssueHistory.IssueStatusId INNER JOIN
                      ProjectCustomFieldValues ON IssueHistory.IssueId = ProjectCustomFieldValues.IssueId INNER JOIN
                      ProjectCustomFields ON ProjectCustomFieldValues.CustomFieldId = ProjectCustomFields.CustomFieldId
WHERE   
  --(ProjectStatus.StatusName = 'redeschis') AND 
(ProjectStatus.ProjectId = 2) AND 
(DATEPART(yy, IssueHistory.DateCreated) =@year) AND 
 (ProjectCustomFields.CustomFieldName = N'Regresie (0 sau 1)') AND 
ProjectCustomFieldValues.CustomFieldValue = '1'
GROUP BY IssueHistory.IssueId
)Issues
GROUP BY DATEPART(yy, Issues.DateCreated), DATENAME(ww, Issues.DateCreated)
)Regr   ON Deschise.An =Regr.An AND Deschise.Saptam = Regr.Saptam
-------
INNER JOIN ----------Prioritati-------------------------------------------------------------------
(
SELECT Issues.An,Issues.Saptam,ISNULL(Minor,0)Minor,ISNULL(Major,0)Major,ISNULL(Critic,0)Critic
FROM 
(
SELECT 
DATEPART(yy, Issues.DateCreated)An,
DATENAME(ww, Issues.DateCreated) AS Saptam
FROM         Issues 
WHERE (DATEPART(yy, Issues.DateCreated) = @year)
GROUP BY  DATEPART(yy, Issues.DateCreated) , DATENAME(ww, Issues.DateCreated)
)Issues LEFT JOIN
(
SELECT 
DATEPART(yy, Issues.DateCreated)An,
DATENAME(ww, Issues.DateCreated) AS Saptam,
Count(Issues.issueId) as Major
FROM         Issues INNER JOIN
                      ProjectPriorities ON Issues.IssuePriorityId = ProjectPriorities.PriorityId 
WHERE     (Issues.ProjectId = 2) AND ProjectPriorities.PriorityID = 5
GROUP BY   DATEPART(yy, Issues.DateCreated) , DATENAME(ww, Issues.DateCreated)
) Major ON Major.an = Issues.An AND Major.Saptam = Issues.Saptam 
LEFT JOIN
(
SELECT 
DATEPART(yy, Issues.DateCreated)An,
DATENAME(ww, Issues.DateCreated) AS Saptam,
Count(Issues.issueId) as Minor
FROM         Issues INNER JOIN
                         ProjectPriorities ON Issues.IssuePriorityId = ProjectPriorities.PriorityId 
WHERE     (Issues.ProjectId = 2) AND ProjectPriorities.PriorityID = 6
GROUP BY  DATEPART(yy, Issues.DateCreated) , DATENAME(ww, Issues.DateCreated)
) MInor   ON Issues.an = Minor.An AND Issues.Saptam = Minor.Saptam
LEFT JOIN
(
SELECT 
DATEPART(yy, Issues.DateCreated)An,
DATENAME(ww, Issues.DateCreated) AS Saptam,
Count(Issues.issueId) as Critic
FROM         Issues INNER JOIN
                         ProjectPriorities ON Issues.IssuePriorityId = ProjectPriorities.PriorityId 
WHERE     (Issues.ProjectId = 2) AND ProjectPriorities.PriorityID = 4
GROUP BY  DATEPART(yy, Issues.DateCreated) , DATENAME(ww, Issues.DateCreated)
) Critic ON Issues.an = Critic.An AND Issues.Saptam = Critic.Saptam 
)Prioritati   ON Deschise.An =Prioritati.An AND Deschise.Saptam = Prioritati.Saptam

 -------------------------------------------------------------inchise--------------------------------------------
 INNER JOIN   
( SELECT Issues.An,Issues.Saptam,ISNULL(InchiseInTermen,0)InchiseInTermen,0 as InchiseInAfaraTermen,ISNULL(InchiseIntern,0)InchiseIntern
FROM 
(
SELECT 
DATEPART(yy, IssueHistory.DateCreated)An,
DATENAME(ww, IssueHistory.DateCreated) AS Saptam
FROM         IssueHistory 
WHERE (DATEPART(yy, IssueHistory.DateCreated) = @year)
GROUP BY  DATEPART(yy, IssueHistory.DateCreated) , DATENAME(ww, IssueHistory.DateCreated)
)Issues LEFT JOIN
(
SELECT 
DATEPART(yy, IssueHistory.DateCreated)An,
DATENAME(ww, IssueHistory.DateCreated) AS Saptam,
Count(IssueHistory.issueId) as InchiseInTermen
FROM         IssueHistory INNER JOIN
                      dbo.ProjectStatus ON dbo.ProjectStatus.statusId = IssueHistory.IssuestatusId 
                      INNER JOIN
                      dbo.Issues ON dbo.Issues.issueId = IssueHistory.issueId 
WHERE     (Issues.ProjectId = 2) AND dbo.ProjectStatus.statusId = 19--InchiseInTermen
GROUP BY   DATEPART(yy, IssueHistory.DateCreated) , DATENAME(ww, IssueHistory.DateCreated)
) InchiseInTermen ON InchiseInTermen.an = Issues.An AND InchiseInTermen.Saptam = Issues.Saptam 
--
LEFT JOIN
(
SELECT 
DATEPART(yy, IssueHistory.DateCreated)An,
DATENAME(ww, IssueHistory.DateCreated) AS Saptam,
Count(IssueHistory.DateCreated) as InchiseIntern
FROM         Issues  INNER JOIN
                      dbo.ProjectStatus ON dbo.ProjectStatus.statusId = Issues.IssuestatusId 
                     INNER JOIN  Users ON Issues.IssueOwnerId = Users.UserId  AND Issues.IssueOwnerId not in (8,17,18,13) --from BROM
                      INNER JOIN
                      (SELECT issueId,MAX(DateCreated )DateCreated FROM dbo.IssueHistory where   IssuestatusId = 19 GROUP BY issueId) IssueHistory ON dbo.Issues.issueId = IssueHistory.issueId 
WHERE     (Issues.ProjectId = 2) --InchiseIntern 
GROUP BY  DATEPART(yy, IssueHistory.DateCreated) , DATENAME(ww, IssueHistory.DateCreated)
) InchiseIntern ON Issues.an = InchiseIntern.An AND Issues.Saptam = InchiseIntern.Saptam )
Inchise   ON Deschise.An =Inchise.An AND Deschise.Saptam = Inchise.Saptam
---------------------------------------------------------------------------------  
 INNER JOIN 
----------------------------------------------Status-------------------------
(
SELECT Issues.An,Issues.Saptam,ISNULL(InLucru,0)InLucru,ISNULL(Amanate,0)Amanate,ISNULL(Invalide,0)Invalide
FROM 
(
SELECT 
DATEPART(yy, Issues.DateCreated)An,
DATENAME(ww, Issues.DateCreated) AS Saptam
FROM         Issues 
WHERE (DATEPART(yy, Issues.DateCreated) = @year)
GROUP BY  DATEPART(yy, Issues.DateCreated) , DATENAME(ww, Issues.DateCreated)
)Issues LEFT JOIN
(
SELECT 
DATEPART(yy, Issues.DateCreated)An,
DATENAME(ww, Issues.DateCreated) AS Saptam,
Count(Issues.issueId) as InLucru
FROM         Issues INNER JOIN
                      dbo.ProjectStatus ON dbo.ProjectStatus.statusId = Issues.IssuestatusId 
WHERE     (Issues.ProjectId = 2) AND dbo.ProjectStatus.statusId = 6--InLucru
GROUP BY   DATEPART(yy, Issues.DateCreated) , DATENAME(ww, Issues.DateCreated)
) InLucru ON InLucru.an = Issues.An AND InLucru.Saptam = Issues.Saptam 
LEFT JOIN
(
SELECT 
DATEPART(yy, Issues.DateCreated)An,
DATENAME(ww, Issues.DateCreated) AS Saptam,
Count(Issues.issueId) as Invalide
FROM         Issues INNER JOIN
                      dbo.ProjectStatus ON dbo.ProjectStatus.statusId = Issues.IssuestatusId 
WHERE     (Issues.ProjectId = 2) AND dbo.ProjectStatus.statusId = 27--Invalide
GROUP BY  DATEPART(yy, Issues.DateCreated) , DATENAME(ww, Issues.DateCreated)
) Invalide   ON Issues.an = Invalide.An AND Issues.Saptam = Invalide.Saptam
LEFT JOIN
(
SELECT 
DATEPART(yy, Issues.DateCreated)An,
DATENAME(ww, Issues.DateCreated) AS Saptam,
Count(Issues.issueId) as Amanate
FROM         Issues INNER JOIN
                      dbo.ProjectStatus ON dbo.ProjectStatus.statusId = Issues.IssuestatusId 
WHERE     (Issues.ProjectId = 2) AND dbo.ProjectStatus.statusId = 9--Amanate
GROUP BY  DATEPART(yy, Issues.DateCreated) , DATENAME(ww, Issues.DateCreated)
) Amanate ON Issues.an = Amanate.An AND Issues.Saptam = Amanate.Saptam  
 ) InStatus   ON Deschise.An =InStatus.An AND Deschise.Saptam = InStatus.Saptam
 -----------------------evoluri--------------------
  INNER JOIN   
( SELECT Issues.An,Issues.Saptam,ISNULL(Evolutii,0)Evolutii
FROM 
(
SELECT 
DATEPART(yy, IssueHistory.DateCreated)An,
DATENAME(ww, IssueHistory.DateCreated) AS Saptam
FROM         IssueHistory 
WHERE (DATEPART(yy, IssueHistory.DateCreated) = @year)
GROUP BY  DATEPART(yy, IssueHistory.DateCreated) , DATENAME(ww, IssueHistory.DateCreated)
)Issues
 LEFT JOIN
 (
SELECT     
DATEPART(yy, Issues.DateCreated)  An,
DATENAME(ww, Issues.DateCreated) AS Saptam,
Count(Issues.DateCreated) as Evolutii
FROM         ProjectCategories INNER JOIN Issues
                       ON ProjectCategories.CategoryId = Issues.IssueCategoryId 
GROUP BY  DATEPART(yy, Issues.DateCreated) , DATENAME(ww, Issues.DateCreated) ,ProjectCategories.CategoryID ,ProjectCategories.CategoryName ,ProjectCategories.ProjectId--,dbo.GetInitialNumberOfBugs( DATENAME(ww, Issues.DateCreated),DATENAME(yy, Issues.DateCreated),5)
HAVING  ProjectCategories.CategoryName like 'Evol%' AND  ProjectCategories.ProjectId = 2 AND DATEPART(yy, Issues.DateCreated) =@year
) Evoluri ON Issues.An = Evoluri.An AND Issues.Saptam = Evoluri.Saptam
) Evols ON Deschise.An =Evols.An AND Deschise.Saptam = Evols.Saptam

--------------------final order by-----------------------
 ORDER BY CONVERT(int,Deschise.Saptam) desc
 
 --getWeekAnalisys 2007
GO
