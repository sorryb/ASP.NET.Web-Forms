SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO









CREATE   PROCEDURE [dbo].[AddTimeEntry]
(
	@UserID int,
	@ProjectID int,
	@CategoryID int,
	@EntryDate datetime,
	@Description nvarchar(255),
	@Duration decimal(10,2)
)
AS

INSERT INTO EntryLog
(
	Description, 
	Duration, 
	EntryDate,
	ProjectID,
	UserID,	
	CategoryID
)
VALUES
(   
	
	@Description,
	@Duration,
	@EntryDate,
	@ProjectID,
	@UserID,
	@CategoryID	
)

SELECT
    @@Identity AS EntryLogID

GO
