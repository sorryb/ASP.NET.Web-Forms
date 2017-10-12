SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE Query_GetQueriesByUsername 
	@Username NVarChar(255),
	@ProjectId Int
AS
DECLARE @UserId Int
SELECT @UserId = UserId FROM Users WHERE Username = @Username

SELECT
	QueryId,
	QueryName
FROM
	Queries
WHERE
	UserId = @UserId
	AND ProjectID = @ProjectId
ORDER BY
	QueryName


GO
