SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO









CREATE PROCEDURE [dbo].[ListCategories]
(
	@ProjectID int
)
AS

SELECT 
	CategoryID, CategoryName, Abbreviation, CAST(EstDuration AS int) EstDuration
	
FROM 
	ProjectCategories
	
WHERE 
	ProjectID = @ProjectID
	AND ISNULL(EndDate,'1/1/2300') > getdate()

--exemple-------------------
--ListCategories 2




GO
