SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE  PROCEDURE GetTimeEntry
(
    @EntryLogID int
)
AS

SELECT 
    EL.Description, 
    EL.Duration, 
    EL.EntryDate, 
    EL.ProjectID, 
    EL.UserID, 
    EL.CategoryID, 
    P.Name AS ProjectName

FROM 
    EntryLog EL
        INNER JOIN TT_Projects P
            ON EL.ProjectID = P.ProjectID
    
WHERE 
    EL.EntryLogID = @EntryLogID



GO
