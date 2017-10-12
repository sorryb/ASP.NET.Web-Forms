SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

/*-<doc>**************************************************************************************
 * <title>dbo.[GetWorkingDays]</title>
 * <description>description here.</description>
 * <in name="@fileNo" type="KEY_ID" mandatory="yes" default="(none)">description here</in>
 * <out>out parametre name and description</out>
 * <author>Sorin</author>
 * <history date="" who="XXX">Creation</history>
</doc>****************************************************************************************/
CREATE FUNCTION [dbo].[GetWorkingDays] 
(
	@startDate datetime,
	@endDate datetime
)
RETURNS varchar(10)
AS
BEGIN

return Convert(varchar(10),DATEDIFF(d,@startDate,@endDate)-(select count(data) from ZileNelucratoare where data between @startDate and @endDate))

END
/**************** Exemple of use *************************/
-- print dbo.GetWorkingDays('2007-02-20',getdate())
/**************************************************************/


GO
