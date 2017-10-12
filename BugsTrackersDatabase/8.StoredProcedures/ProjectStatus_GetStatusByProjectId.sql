SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE ProjectStatus_GetStatusByProjectId
 @ProjectId	    INT
AS
SELECT * FROM ProjectStatus WHERE ProjectId=@ProjectId ORDER BY StatusName


GO
