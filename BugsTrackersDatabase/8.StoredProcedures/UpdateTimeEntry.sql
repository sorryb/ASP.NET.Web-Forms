SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE  PROCEDURE UpdateTimeEntry
(
	@EntryLogID int,
	@UserID int,
	@ProjectID int,
	@CategoryID int,
	@EntryDate datetime,
	@Description nvarchar(255),
	@Duration decimal(10,2)
)
AS

UPDATE
	 EntryLog
		SET 	
			UserID=@UserID,
		     	ProjectID = @ProjectID,
			CategoryID = @CategoryID,
			EntryDate = @EntryDate,
			Description = @Description,
			Duration = @Duration
	       
WHERE 
	EntryLogID = @EntryLogID



GO
