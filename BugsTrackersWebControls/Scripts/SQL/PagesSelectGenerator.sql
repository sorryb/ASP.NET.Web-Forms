
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[PagesSelectGenerator]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[PagesSelectGenerator]
GO

/*-<doc>**************************************************************************************
 * <title>dbo.PagesSelectGenerator</title>
 * <description>Generate pages for a select webgrid</description>
 * <in name="@TotalRows" type="varchar" default="(none)">Total rows of table or select source</in>
 * <in name="@IntPageRows" type="varchar" default="(none)">NUmber of rows on pages(currently 10)</in>
 * <in name="@sTableName" type="varchar" default="(none)">Table or select name.</in>
 * <in name="@sOrderBy" type="varchar" default="(none)">Order by clause</in> 
 * <in name="@sWhereClause" type="varchar" default="(none)">Where clause.</in>  
 * <author>Sorin</author>
 * <history date="2009-01-13">Creation</history>
 * <history date="2008-08-13">add delivery options</history> 
</doc>****************************************************************************************/
/*********  Procedure creation  *********/


CREATE  PROCEDURE dbo.PagesSelectGenerator
(
	@TotalRows nvarchar(10),
	@IntPageRows int,
	@sTableName  nvarchar(100),
	@sOrderBy nvarchar(100),
	@sWhereClause nvarchar(250)= ''
)
AS
/*********  Variables declaration   *********/
DECLARE
			  @nError		int	-- Error status
			, @sErrMsg		varchar(50)		-- Error message
			, @SQLString    nvarchar(500)
			, @ParmDefinition nvarchar(500)
			, @sWhereComposedTable nvarchar(500)

/*********  Variables initialisation  *********/
SELECT
	  @nError = 0
	, @sErrMsg = ''

/**********  Arguments validation  ************/
IF (@TotalRows IS NULL)
BEGIN
	SELECT @sErrMsg = ' @TotalRows, parameter can not be null'
	SELECT @nError = 50001
END

IF (@IntPageRows IS NULL)
BEGIN
	SELECT @sErrMsg = ' @IntPageRows, parameter can not be null'
	SELECT @nError = 50002
END

IF (@sTableName IS NULL)
BEGIN
	SELECT @sErrMsg = ' @sTableName,that means name of the table or select, parameter can not be null'
	SELECT @nError = 50003
END

IF (@sOrderBy = '' OR @sOrderBy IS NULL  )
BEGIN

	SELECT TOP 1 @sOrderBy = COLUMN_NAME FROM INFORMATION_SCHEMA.Columns where TABLE_NAME = @sTableName
	SET @sOrderBy = '[' + @sOrderBy + ']'
END
/**************  Requests  (Procedure Body)  **************/
IF (@nError = 0)
BEGIN

	IF (@sWhereClause = '')
		SET @sWhereComposedTable = @sTableName
	ELSE
		SET @sWhereComposedTable = '(Select * From ' + @sTableName + ' where ' + @sWhereClause + ')' + @sTableName

	SET @SQLString =
		 N'SELECT * 
			FROM 
			(
				select TOP '+ @TotalRows + ' ROW_NUMBER() OVER (ORDER BY '+ @sOrderBy +') as ROW_NUM, *  FROM '+ @sWhereComposedTable +' ORDER BY ROW_NUM
			) innerSelect 
			WHERE ROW_NUM > @PageRows';
	SET @ParmDefinition = N'@PageRows int'--,@OrderBy nvarchar(50)';

	--print @SQLString

	EXECUTE sp_executesql @SQLString, @ParmDefinition,@PageRows = @IntPageRows--,@OrderBy = @sOrderBy


	SELECT @nError = @@Error
END	

/***************** Error management  *******************/
IF (@nError =50001)
BEGIN
	SELECT @sErrMsg = convert(varchar(50),ISNULL( @nError , -1))+'|Error in PagesSelectGenerator - ' + @sErrMsg
		+ ' ; @sTableName  = ' + ISNULL(convert(varchar(8),@sTableName), 'NULL') + ' - '
	RAISERROR @nError @sErrMsg
END

/**************** Send error status ******************/
RETURN @nError
GO
/**************** Exemple *****************************************/

-- /* PagesSelectGenerator 11,0 ,'Istoric_Cereri','IDIstoric','NrDosar like ''0855925%'''  */ 
/*******************************************************************/

GO




