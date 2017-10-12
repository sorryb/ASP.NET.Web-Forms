SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE ProjectMilestones_CreateNewMilestone
	@ProjectId INT,
	@MilestoneName NVARCHAR(50),
	@MilestoneImageUrl NVARCHAR(255)
AS
IF NOT EXISTS(SELECT MilestoneId  FROM ProjectMilestones WHERE LOWER(MilestoneName)= LOWER(@MilestoneName) AND ProjectId = @ProjectId)
BEGIN
	INSERT ProjectMilestones 
	(
		ProjectId, 
		MilestoneName ,
		MilestoneImageUrl 
	) VALUES (
		@ProjectId, 
		@MilestoneName,
		@MilestoneImageUrl
	)
	RETURN @@IDENTITY
END
RETURN -1


GO
