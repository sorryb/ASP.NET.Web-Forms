-- ================================================
-- Template generated from Template Explorer using:
-- Create Scalar Function (New Menu).SQL
--
-- Use the Specify Values for Template Parameters 
-- command (Ctrl-Shift-M) to fill in the parameter 
-- values below.
--
-- This block of comments will not be included in
-- the definition of the function.
-- ================================================
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[GetInitialNumberOfBugs]') and xtype in (N'FN', N'IF', N'TF'))
drop function [dbo].[GetInitialNumberOfBugs]
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
CREATE FUNCTION GetInitialNumberOfBugs
(
	 @weekPart int, @yearPart int,@status int 
)
RETURNS int
AS
BEGIN
    declare @returnVal int 
		SELECT   
		@returnVal = Count(Issues.DateCreated) 
		FROM       Issues 
		LEFT JOIN 
		(
		SELECT IssueID,MAX(DateCreated) MaxDate FROM IssueHistory GROUP BY IssueID  		 
		) OnlyMaxRecords	ON 	OnlyMaxRecords.IssueID = Issues.IssueID	 		   
		WHERE  Issues.ProjectId = 2 AND  Issues.IssueCategoryID = 2 AND Issues.IssueStatusId = @status	
		AND DATEPART(yy, Issues.DateCreated) = @yearPart  AND  CONVERT(int,DATENAME(ww, Issues.DateCreated))<=  @weekPart
/*
    	SELECT   
		@returnVal = Count(Issues.DateCreated) 
		FROM         ProjectCategories INNER JOIN Issues
							   ON ProjectCategories.CategoryId = Issues.IssueCategoryId 
		WHERE  ProjectCategories.ProjectId = 2 AND  ProjectCategories.CategoryID = 2 --AND IssueStatusId = @status 
		AND DATEPART(yy, Issues.DateCreated) = @yearPart  AND  CONVERT(int,DATENAME(ww, Issues.DateCreated))<  @weekPart
*/
    return @returnVal

END


--test : print dbo.GetInitialNumberOfBugs( 28,2007,5)
GO

