SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE User_Authenticate 
  @Username NVarChar(255),
  @Password NVarChar(255)
AS
IF EXISTS( SELECT UserId FROM Users WHERE Username = @Username AND UserPassword = @Password)
  RETURN 0
ELSE
  RETURN -1


GO
