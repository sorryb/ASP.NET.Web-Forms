if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[GenerateTextFilesTables]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[GenerateTextFilesTables]
GO

create proc GenerateTextFilesTables

AS
declare @tmpTables TABLE(ID int identity ,tableName varchar(100))
declare @tmpTypesTables TABLE(ID int identity ,tableName varchar(100))
INSERT INTO @tmpTables(tableName)
 SELECT   'ZileNelucratoare'   [TableName] 
 UNION ALL
 SELECT   'Users'   [TableName] 
  UNION ALL  
 SELECT   'Roles'   [TableName]                                                                                                                 
                                                                                                                            
 
/*---not script all tables
INSERT INTO @tmpTables(tableName)
SELECT 
    [TableName] = so.name 
  -- , [RowCount] = MAX(si.rows) 
FROM 
    sysobjects so, 
    sysindexes si 
WHERE 
    so.xtype = 'U' 
    AND 
    si.id = OBJECT_ID(so.name) 
GROUP BY 
    so.name 
ORDER BY 
    1 DESC

DELETE @tmpTables WHERE tableName in
 (

'sysdiagrams'
)
----------*/

INSERT INTO @tmpTypesTables(tableName) select tableName from @tmpTables

---------------------GENERATE REFERENCE LIST---------------------------
--select *,1 as Ordine into TabeleReferinta from @tmpTypesTables
--select * from TabeleReferinta
-----------------------------------------------------------------------

declare @i int,@count int,@TableNameNomenc varchar(100)
select @count= count(*) from @tmpTypesTables
select @i=1
while @i <= @count
BEGIN
	select @TableNameNomenc = tableName from @tmpTypesTables where ID = @i

	exec ScriptToFile 
		@SourceUID = 'sa' , 
		@SourcePWD = '' , 
		@OutFilePath = 'D:\Test\Create INSERTS\' ,
		@OutFileName = null ,
		@SourceSVR = '(local)' ,
		@Database = 'BugTracker' ,
		@TableName = @TableNameNomenc--'Caracteristici_Credit'

	SET @i= @i + 1
END

/*------------exemple ---------------------------
GenerateTextFilesTables
-------------------------------------------------*/