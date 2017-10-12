SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE ProjectCategories_GetCategoriesByProjectId
	@ProjectId Int 
AS
SELECT
	CategoryId,
	CategoryName,
	ProjectId,
	ParentCategoryId
FROM
	ProjectCategories
WHERE
	ProjectId = @ProjectId
ORDER BY
	CategoryName


GO
