SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE Project_UpdateProject
 @ProjectId		 Int,
 @ProjectName		 NVARCHAR(255),
 @ProjectDescription 	 NVARCHAR(1000),
 @ProjectManagerId 	 Int
AS
DECLARE @ProjectIdFound INT
SELECT @ProjectIdFound = ProjectId  FROM Projects WHERE ProjectId = @ProjectId
IF (@ProjectIdFound IS NOT NULL)
BEGIN
	UPDATE Projects SET
		ProjectName = @ProjectName,
		ProjectDescription = @ProjectDescription,
		ProjectManagerId = @ProjectManagerId
	WHERE
		ProjectId = @ProjectId
	RETURN 0
END
ELSE
	RETURN 1


GO
