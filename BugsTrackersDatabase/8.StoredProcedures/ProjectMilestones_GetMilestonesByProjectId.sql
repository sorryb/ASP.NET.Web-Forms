SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE ProjectMilestones_GetMilestonesByProjectId
	@ProjectId INT
AS
SELECT * FROM ProjectMilestones WHERE ProjectId=@ProjectId ORDER BY MilestoneName


GO
