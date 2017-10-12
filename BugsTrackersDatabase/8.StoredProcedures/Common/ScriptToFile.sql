--This isn't DMO but it compliments the other scripts.
--Given a table it will script all the data as insert statements.
--This can be used to populate a table or to record changes e.g. in lookup tables.
--It uses sp_CreateDataLoadScript (the remote version) which can be found under tsql scripts on this site)

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ScriptToFile]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[ScriptToFile]
GO

create proc ScriptToFile
@SourceUID	varchar(128) ,
@SourcePWD	varchar(128) ,
@OutFilePath	varchar(256) ,
@OutFileName	varchar(128) = null ,
@SourceSVR	varchar(128) ,
@Database	varchar(128) ,
@TableName	varchar(128)
as
/*
exec ScriptToFile 
	@SourceUID = 'sa' , 
	@SourcePWD = 'sorin' , 
	@OutFilePath = 'D:\Work\SQL\Generate INSERTS\Create INSERTS\' ,
	@OutFileName = null ,
	@SourceSVR = '(local)' ,
	@Database = 'BugTracker' ,
	@TableName = 'Caracteristici_Credit'
*/
declare @sql varchar(4000)

	if @OutFileName	is null
		Select @OutFileName =  @TableName + '.sql'
	
	if @SourceSVR = '(local)'
		select @SourceSVR = '[' + @@servername + ']'
	
	-- create output directory - will fail if already exists but ...
	select @OutFilePath = @OutFilePath + replace(replace(@SourceSVR,'[',''),']','')
	select	@sql = 'mkdir "' + @OutFilePath + '"'
	exec master..xp_cmdshell @sql, no_output
	select @OutFilePath = @OutFilePath + '\' + replace(replace(@Database,'[',''),']','')
	select	@sql = 'mkdir "' + @OutFilePath + '"'
	exec master..xp_cmdshell @sql, no_output
	select @OutFilePath = @OutFilePath + '\Data'
	select	@sql = 'mkdir "' + @OutFilePath + '"'
	exec master..xp_cmdshell @sql, no_output
	select @OutFilePath = @OutFilePath + '\'	
	
	--select @sql = 'bcp "/*set fmtonly off */exec BugTracker.dbo.sp_CreateDataLoadScript '''+  @TableName + '''" queryout "' + @OutFilePath + @OutFileName + '" -c -S ' + @@servername

	exec BugTracker.dbo.CreateLoadScriptFiles @TableName 

	select @sql = 'bcp "select * from BugTracker.dbo.'+ '__View__' + @TableName+' " queryout "' + @OutFilePath + @OutFileName + '" -c -S ' + @@servername
	
	

	if @SourceUID is not null
	begin
		select @sql = @sql + ' -U ' + @SourceUID + ' -P ' + @SourcePWD
	end
	
--print @sql
	exec master..xp_cmdshell @sql
exec('drop view ' + '__View__' + @TableName)


--sqlcmd -q "exec BugTracker.dbo.sp_CreateDataLoadScript 'Roluri'"  -o Tabel.txt  -d BugTracker

GO
