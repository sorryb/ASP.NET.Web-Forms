SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE ProjectStatus_CreateNewStatus
	@ProjectID Int,
	@StatusName NVarChar(50),
	@StatusImageUrl NVarChar(255)
AS
IF NOT EXISTS(SELECT StatusId  FROM ProjectStatus WHERE LOWER(StatusName)= LOWER(@StatusName) AND ProjectId = @ProjectId )
BEGIN
	INSERT ProjectStatus 
	(
		ProjectId,
		StatusName ,
		StatusImageUrl  
	) VALUES (
		@ProjectId,
		@StatusName,
		@StatusImageUrl
	)
   RETURN (@@IDENTITY)
END
RETURN -1


GO
