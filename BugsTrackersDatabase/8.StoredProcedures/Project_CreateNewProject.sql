SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO



CREATE PROCEDURE Project_CreateNewProject
 @ProjectName		 NVARCHAR(256),
 @ProjectDescription 	 NVARCHAR(1000),
 @ProjectManagerId 	 Int,
 @ProjectCreatorUsername	NVARCHAR(256)
AS
DECLARE @ProjectCreatorId Int
SELECT @ProjectCreatorId = UserId FROM Users WHERE Username = @ProjectCreatorUsername

IF NOT EXISTS( SELECT ProjectId  FROM Projects WHERE LOWER(ProjectName) = LOWER(@ProjectName))
BEGIN
	INSERT Projects 
	(
		ProjectName,
		ProjectDescription,
		ProjectManagerId,
		ProjectCreatorId
	) 
	VALUES
	(
		@ProjectName,
		@ProjectDescription,
		@ProjectManagerId,
		@ProjectCreatorId
	)

 	RETURN @@IDENTITY
END
ELSE
  RETURN 1


GO
