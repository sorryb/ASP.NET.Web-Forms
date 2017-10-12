SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO



CREATE PROCEDURE CustomField_DeleteCustomField
 @CustomIdToDelete INT
AS
DELETE FROM ProjectCustomFields WHERE CustomFieldId = @CustomIdToDelete


GO
