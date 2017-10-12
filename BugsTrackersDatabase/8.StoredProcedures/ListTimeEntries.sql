SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO





CREATE PROCEDURE [dbo].[ListTimeEntries]
(
	@QueryUserID int,
	@UserID int,
	@StartDate datetime,
	@EndDate datetime
)
AS
DECLARE @@QueryUserRoleID int

SELECT @@QueryUserRoleID = Users.RoleID FROM Users WHERE Users.UserID = @QueryUserID

IF @@QueryUserRoleID = 1 or @QueryUserID = @UserID
	BEGIN
		SELECT 
			EntryLogID, EntryLog.Description, Duration, EntryDate, EntryLog.ProjectID AS ProjectID, 
			EntryLog.CategoryID AS CategoryID, Categories.Abbreviation AS CategoryName, Projects.ProjectName AS ProjectName,
			ProjectManagerId ManagerUserID, Categories.Abbreviation AS CatShortName
		FROM 
			EntryLog 
				INNER JOIN 
				  projectCategories Categories
				ON 
				  EntryLog.CategoryID = Categories.CategoryID 
				INNER JOIN 
				  Projects 
				ON 
				  EntryLog.ProjectID = Projects.ProjectID	
		WHERE 
			UserID = @UserID 
			AND 
			--Convert(nvarchar, EntryDate, 1) >= Convert(nvarchar, @StartDate, 1)
			EntryDate  >=  @StartDate
			AND 
			--Convert(nvarchar, EntryDate, 1) <= Convert(nvarchar, @EndDate, 1)
			EntryDate  <=  @EndDate
		ORDER BY EntryDate,EntryLogID
	END
ELSE IF @@QueryUserRoleID = 2
	BEGIN
		SELECT 
			EntryLogID, EntryLog.Description, Duration, EntryDate, EntryLog.ProjectID AS ProjectID, 
			EntryLog.CategoryID AS CategoryID, Categories.Abbreviation AS CategoryName, Projects.ProjectName AS ProjectName,
			ProjectManagerId as ManagerUserID, Categories.Abbreviation AS CatShortName
		FROM 
			EntryLog 
				INNER JOIN 
				 projectCategories Categories 
				ON 
				  EntryLog.CategoryID = Categories.CategoryID 
				INNER JOIN 
				  Projects 
				ON 
				  EntryLog.ProjectID = Projects.ProjectID	
		WHERE 
			UserID = @UserID 
			AND 
			Convert(nvarchar, EntryDate, 1) >= Convert(nvarchar, @StartDate, 1)
			AND 
			Convert(nvarchar, EntryDate, 1) <= Convert(nvarchar, @EndDate, 1)
			AND
			ProjectManagerId = @QueryUserID
		ORDER BY EntryDate,EntryLogID
	END


--exec ListTimeEntries @QueryUserID=1,@UserID=14,@StartDate='2006-08-28',@EndDate='2006-09-03'


GO
