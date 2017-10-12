ALTER TABLE [dbo].[QueryClauses]  WITH CHECK ADD  CONSTRAINT [FK_IssueTracker_QueryClauses_IssueTracker_Queries] FOREIGN KEY([QueryId])
REFERENCES [Queries] ([QueryId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[QueryClauses] CHECK CONSTRAINT [FK_IssueTracker_QueryClauses_IssueTracker_Queries]
GO
