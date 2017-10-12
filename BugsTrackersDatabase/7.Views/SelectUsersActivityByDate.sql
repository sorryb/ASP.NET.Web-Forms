SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW SelectUsersActivityByDate
AS
SELECT     Users.UserName, CONVERT(varchar(10),MIN(Entrylog.EntryDate),103) AS Intrare, CONVERT(varchar(10),MAX(Entrylog.EntryDate),103) AS Iesire
,DATEDIFF(day,MIN(Entrylog.EntryDate) , MAx(Entrylog.EntryDate)) AS NrZile
,DATEDIFF(month,MIN(Entrylog.EntryDate) , MAx(Entrylog.EntryDate)) AS Luni
,DATEDIFF(week,MIN(Entrylog.EntryDate) , MAx(Entrylog.EntryDate)) AS Saptamani
FROM         Projects INNER JOIN
                      Entrylog ON Projects.ProjectId = Entrylog.ProjectID INNER JOIN
                      Users ON Entrylog.UserID = Users.UserId
GROUP BY Users.UserName
GO
