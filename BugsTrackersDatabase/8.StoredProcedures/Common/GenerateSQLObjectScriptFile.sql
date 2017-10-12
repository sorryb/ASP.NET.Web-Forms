/********************************************************/
/****** Object:  Stored Procedure dbo.GenerateSQLObjectScriptFile******/
/******Script Date: 5/8/2003 11:06:52 AM ******/
/****** Created By:	Shailesh Khanal	******/
/***************************************************
ObjectType:
Database
Procedure
View
Table
Index
Trigger
Key
Check
Job
*****/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[GenerateSQLObjectScriptFile]') 
  and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[GenerateSQLObjectScriptFile]
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO

/****** Object:  Stored Procedure dbo.proc_genscript    Script Date: 5/8/2003 11:06:53 AM ******/

CREATE PROCEDURE GenerateSQLObjectScriptFile 
	@ServerName varchar(30), 
	@DBName varchar(30), 
	@ObjectName varchar(50), 
	@ObjectType varchar(10), 
	@TableName varchar(50),
	@ScriptFile varchar(255)
AS

DECLARE @CmdStr varchar(255)
DECLARE @object int
DECLARE @hr int

SET NOCOUNT ON
SET @CmdStr = 'Connect('+@ServerName+')'
EXEC @hr = sp_OACreate 'SQLDMO.SQLServer', @object OUT

--Comment out for standard login
EXEC @hr = sp_OASetProperty @object, 'LoginSecure', TRUE

/* Uncomment for Standard Login
EXEC @hr = sp_OASetProperty @object, 'Login', 'sa'
EXEC @hr = sp_OASetProperty @object, 'password', 'sapassword'
*/

EXEC @hr = sp_OAMethod @object,@CmdStr
SET @CmdStr = 
  CASE @ObjectType
    WHEN 'Database' 	THEN 'Databases("' 
    WHEN 'Procedure'	THEN 'Databases("' + @DBName + '").StoredProcedures("'
    WHEN 'View' 	THEN 'Databases("' + @DBName + '").Views("'
    WHEN 'Table'	THEN 'Databases("' + @DBName + '").Tables("'
    WHEN 'Index'	THEN 'Databases("' + @DBName + '").Tables("' + @TableName + '").Indexes("'
    WHEN 'Trigger'	THEN 'Databases("' + @DBName + '").Tables("' + @TableName + '").Triggers("'
    WHEN 'Key'	THEN 'Databases("' + @DBName + '").Tables("' + @TableName + '").Keys("'
    WHEN 'Check'	THEN 'Databases("' + @DBName + '").Tables("' + @TableName + '").Checks("'
    WHEN 'Job'	THEN 'Jobserver.Jobs("'
  END

SET @CmdStr = @CmdStr + @ObjectName + '").Script(5,"' + @ScriptFile + '")'
EXEC @hr = sp_OAMethod @object, @CmdStr
EXEC @hr = sp_OADestroy @object
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO
 /*
 ///////////////////////exemple/////////////////////////////////////
 //Usage Example: 
 exec GenerateSQLObjectScriptFile 
	@ServerName = 'laptopsql', 
	@DBName = 'BugTracker', 
	@ObjectName = 'PK_IssueTracker_IssueNotifications', 
	@ObjectType = 'Index', 
	@TableName = 'IssueNotifications',
	@ScriptFile = 'D:\test\PK_IssueTracker_IssueNotifications.sql'
/////////////////////////////////////////////////////////////////
*/

/*
////////////Primary Keys //////////////////////////////////////
select c.name col,pk.name
from sysindexes i
join sysobjects o ON i.id = o.id
join sysobjects pk ON i.name = pk.name
AND pk.parent_obj = i.id
AND pk.xtype = 'PK'
join sysindexkeys ik on i.id = ik.id
and i.indid = ik.indid
join syscolumns c ON ik.id = c.id
AND ik.colid = c.colid
where o.name = 'ProjectStatus'
order by ik.keyno


//////////////////////Foreign Keys////////////////////////////////

SELECT * FROM sys.foreign_keys WHERE  parent_object_id = OBJECT_ID(N'[dbo].[ProjectStatus]'))

/////////////////////////////////////////////////////////////////
*/