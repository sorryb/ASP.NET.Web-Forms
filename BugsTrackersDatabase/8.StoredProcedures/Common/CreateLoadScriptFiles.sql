
if exists (select * from sysobjects where id = object_id(N'[dbo].[CreateLoadScriptFiles]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[CreateLoadScriptFiles]
GO


CREATE PROCEDURE dbo.CreateLoadScriptFiles
@TblName varchar(128)
as
/* ------help------------------------------
	select COLUMNPROPERTY(OBJECT_ID('Users'),'UserID','IsIdentity')--- check if identity	
*/-----------------------------------------
SET NOCOUNT ON

	
	DECLARE @a table(id int identity (1,1), ColType int, ColName varchar(128))

	insert 	@a (ColType, ColName)
	select case when DATA_TYPE like '%char%' then 1 /*when DATA_TYPE like '%date%' then 1*/ else 0 end A,
		COLUMN_NAME
	from 	information_schema.columns
	where 	TABLE_NAME = @TblName AND COLUMNPROPERTY(OBJECT_ID(@TblName),COLUMN_NAME,'IsIdentity')<> 1
	order by ORDINAL_POSITION
	
	if not exists (select * from @a)
	begin
		raiserror('No columns found for table %s', 16,-1, @TblName)
		return
	end

declare	@id int ,
	@maxid int ,
	@cmd1 varchar(7000) ,
	@cmd2 varchar(7000)

	select 	@id = 0 ,
		@maxid = max(id)
	from 	@a

	SET	@cmd1 = 'select '' insert ' + @TblName + ' ( '
	SET	@cmd2 = ' + '' select '' + '
	while @id < @maxid
	begin
		select @id = min(id) from @a where id > @id

		select 	@cmd1 = @cmd1 +'[' + ColName + '],'
		from	@a
		where	id = @id

		select @cmd2 = 	@cmd2
				+ ' case when [' + ColName + '] is null '
				+	' then ''null'' '
				+	' else '
				+	  case when ColType = 1 then  ''''''''' + [' + ColName + '] + ''''''''' else ''''''''' + convert(varchar(20),[' + ColName + '] )+'''''''' ' end
				+ ' end + '','' + '
		from	@a
		where	id = @id
	end


	select @cmd1 = left(@cmd1,len(@cmd1)-1) + ' ) '' '
	select @cmd2 = left(@cmd2,len(@cmd2)-8) + ' insertText from ' + @tblName

	--select '/*' + @cmd1 + @cmd2 + '*/'

	exec (' Create view ' + '__View__' + @tblName+' as '+ @cmd1 + @cmd2)


/*
exec CreateLoadScriptFiles 'Caracteristici_Credit'
*/
go
SET nocount off
GO