SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE Query_SaveQuery 
	@Username NVarChar(255),
	@ProjectId Int,
	@QueryName NVarChar(50)
AS
-- Get UserID
DECLARE @UserId Int
SELECT @UserId = UserId FROM Users WHERE Username = @Username

IF EXISTS(SELECT QueryName FROM Queries WHERE QueryName = @QueryName AND UserId = @UserId)
BEGIN
	RETURN 0
END

INSERT Queries
(
	UserId,
	ProjectId,
	QueryName
)
VALUES
(
	@UserId,
	@ProjectId,
	@QueryName
)
RETURN @@IDENTITY


GO
