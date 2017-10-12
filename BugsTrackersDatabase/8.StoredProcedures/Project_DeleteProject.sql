SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE Project_DeleteProject
	@ProjectIdToDelete	INT
AS
UPDATE Projects SET ProjectDisabled = 1 WHERE ProjectId = @ProjectIdToDelete


GO
