SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE ProjectPriorities_GetPrioritiesByProjectId
 @ProjectId	    INT
AS
SELECT 
	PriorityId,
	ProjectId,
	PriorityName,
	PriorityImageUrl
FROM 
	ProjectPriorities 
WHERE 
	ProjectId=@ProjectId
ORDER BY
	PriorityName


GO
