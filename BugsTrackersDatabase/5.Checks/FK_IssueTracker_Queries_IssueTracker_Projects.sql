ALTER TABLE [dbo].[Queries]  WITH CHECK ADD  CONSTRAINT [FK_IssueTracker_Queries_IssueTracker_Projects] FOREIGN KEY([ProjectId])
REFERENCES [Projects] ([ProjectId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Queries] CHECK CONSTRAINT [FK_IssueTracker_Queries_IssueTracker_Projects]
GO
