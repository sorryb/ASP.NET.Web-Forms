ALTER TABLE [dbo].[Issues]  WITH CHECK ADD  CONSTRAINT [FK_IssueTracker_Issues_IssueTracker_Projects] FOREIGN KEY([ProjectId])
REFERENCES [Projects] ([ProjectId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Issues] CHECK CONSTRAINT [FK_IssueTracker_Issues_IssueTracker_Projects]
GO
