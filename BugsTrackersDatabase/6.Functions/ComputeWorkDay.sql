SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Sorin>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
CREATE FUNCTION [dbo].[ComputeWorkDay]
(
	@startDate datetime,
	@endDate datetime
)
RETURNS float
AS
BEGIN
	-- Declare the return variable here
	DECLARE @noOfDayes float
--		,@startDate datetime,
--	@endDate datetime
--set 	@startDate ='2006-09-01 10:38:16.933'
--set 	@endDate ='2006-09-06 16:52:03.070'
--print DATEDIFF(d,@startDate,@endDate) 
--print CONVERT(float,19 - DATEPART(hh,@startDate)) / 8
--print CONVERT(float,DATEPART(hh,@endDate) - 9) / 8
	IF DATEDIFF(d,@startDate,@endDate)> 2
	BEGIN
-------print 'mai mult de 1 zii'

			SET @noOfDayes =CONVERT(float,dbo.GetWorkingDays(@startDate,@endDate) )
						   + (CONVERT(float,19 - DATEPART(hh,@startDate)) / 8)
						   + (CONVERT(float,DATEPART(hh,@endDate) - 9) / 8)
	END
	ELSE
	IF DATEDIFF(d,@startDate,@endDate)= 1
		BEGIN
--print 'a doua zii'
				SET @noOfDayes = (CONVERT(float,19 - DATEPART(hh,@startDate)) / 8)
							   + (CONVERT(float,DATEPART(hh,@endDate) - 9) / 8)
		END
	ELSE
		BEGIN
--print 'in aceeasi zii'
				SET @noOfDayes = CONVERT(float,DATEDIFF(hh,@startDate,@endDate)) / 8
				
		END
	RETURN  @noOfDayes


---exemple :  print dbo.ComputeWorkDay ('2006-09-06 10:38:16.933','2006-09-07 16:52:03.070')
END

GO
