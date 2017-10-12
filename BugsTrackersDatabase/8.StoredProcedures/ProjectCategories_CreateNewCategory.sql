SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE ProjectCategories_CreateNewCategory
	@CategoryName NVarChar(100),
	@ProjectId Int,
	@ParentCategoryId Int
AS
IF NOT EXISTS(SELECT CategoryId FROM ProjectCategories WHERE ProjectId = @ProjectId AND CategoryName = @CategoryName AND ParentCategoryId = @ParentCategoryId)
BEGIN
	INSERT ProjectCategories
	(
		CategoryName,
		ProjectId,
		ParentCategoryId
	)
	VALUES
	(
		@CategoryName,
		@ProjectId,
		@ParentCategoryId
	)
	RETURN @@IDENTITY
END


GO
