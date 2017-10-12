
IF EXISTS (SELECT name FROM sys.objects   WHERE name = 'Issues_Trigger_ForALL' AND type = 'TR')
   DROP TRIGGER dbo.Issues_Trigger_ForALL;
GO

/*-<doc>**************************************************************************************
 * <title>dbo.Issues_Trigger_ForALL</title>
 * <description>Trigger for INSERT ,UPDATE,DELETE on Issues.</description>
 * <author>Sorin</author>
 * <history date="2010-11-16" who="sb">Create</history>  
</doc>****************************************************************************************/


/*********  Procedure creation  *********/

CREATE TRIGGER dbo.Issues_Trigger_ForALL   ON  dbo.Issues 
   AFTER INSERT,DELETE,UPDATE
AS 

SET NOCOUNT ON;

/*********  Variables declaration   *********/
DECLARE
	  @nError		int			-- Error status
	, @sErrMsg		varchar(50)		-- Error message


/*********  Variables initialisation  *********/
SELECT
	  @nError = 0
	, @sErrMsg = ''

/**************  Requests  (Procedure Body)  **************/

IF (@nError = 0)
BEGIN

SET IDENTITY_INSERT dbo.Entrylog OFF;


	IF (Update(DateCreated))-- EXISTS (SELECT * FROM DELETED)
		BEGIN
			INSERT INTO dbo.Entrylog ([Description],[Duration] ,[EntryDate]  ,[ProjectID],[UserID] ,[CategoryID]) 
				SELECT  IssueTitle,1,getdate(),ProjectId,IssueAssignedId,2/* bug*/ FROM INSERTED  
		END
	ELSE
		BEGIN
			INSERT INTO dbo.Entrylog ([Description],[Duration] ,[EntryDate]  ,[ProjectID],[UserID] ,[CategoryID])
				SELECT  IssueTitle,1,getdate(),ProjectId,IssueAssignedId,2/* bug*/ FROM DELETED 
		END
SET IDENTITY_INSERT dbo.Entrylog ON;

	SELECT @nError = @@Error
END

/***************** Error management  *******************/
IF (@nError <> 0)
	BEGIN
			RAISERROR (N'Eroare in  %s %s.', -- Message text.
           10, -- Severity,
           1, -- State,
           N'Issues_Trigger_ForALL', -- First argument.
           ''); -- Second argument.

	END

GO
 