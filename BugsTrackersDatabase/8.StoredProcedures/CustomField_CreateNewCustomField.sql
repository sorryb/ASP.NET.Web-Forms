SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO



CREATE PROCEDURE CustomField_CreateNewCustomField
	@ProjectId Int,
	@CustomFieldName NVarChar(50),
	@CustomFieldDataType Int,
	@CustomFieldRequired Bit	
AS
IF NOT EXISTS(SELECT CustomFieldId FROM ProjectCustomFields WHERE ProjectID = @ProjectID AND LOWER(CustomFieldName) = LOWER(@CustomFieldName) )
BEGIN
	INSERT ProjectCustomFields
	(
		ProjectId,
		CustomFieldName,
		CustomFieldDataType,
		CustomFieldRequired
	)
	VALUES
	(
		@ProjectId,
		@CustomFieldName,
		@CustomFieldDataType,
		@CustomFieldRequired
	)

	RETURN @@IDENTITY
END
RETURN 0


GO
