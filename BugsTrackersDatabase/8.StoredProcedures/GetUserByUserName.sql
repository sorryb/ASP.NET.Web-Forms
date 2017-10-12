SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO








CREATE  PROCEDURE [dbo].[GetUserByUserName]
(
	@UserName nvarchar(50)
)

AS
	
SELECT 
	UserID, 
	UserName, 
	UserPassword,
-- 	LastName, 
-- 	Email, 
-- 	Telephone, 
	RoleID

FROM 
	Users WHERE UserName = @UserName



GO
