SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO



CREATE PROCEDURE Query_DeleteQuery 
  @QueryId Int
AS
DELETE Queries WHERE QueryId = @QueryId
DELETE QueryClauses WHERE QueryId = @QueryId


GO
