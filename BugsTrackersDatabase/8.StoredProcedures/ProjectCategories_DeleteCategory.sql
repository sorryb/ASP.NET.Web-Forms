SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO



CREATE PROCEDURE ProjectCategories_DeleteCategory
	@CategoryIdToDelete Int
AS
DELETE 
	ProjectCategories
WHERE
	CategoryId = @CategoryIdToDelete


GO
