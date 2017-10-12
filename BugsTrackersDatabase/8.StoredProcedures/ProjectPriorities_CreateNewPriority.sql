SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE ProjectPriorities_CreateNewPriority
 @ProjectId	    INT,
 @PriorityName        NVARCHAR(255),
 @PriorityImageUrl NVarChar(255)
AS
IF NOT EXISTS(SELECT PriorityId  FROM ProjectPriorities WHERE LOWER(PriorityName)= LOWER(@PriorityName) AND ProjectId = @ProjectId)
BEGIN
	INSERT ProjectPriorities 
   	( 
		ProjectId, 
		PriorityName,
		PriorityImageUrl 
   	) VALUES (
		@ProjectId, 
		@PriorityName,
		@PriorityImageUrl
  	)
   	RETURN @@IDENTITY
END
RETURN 0


GO
