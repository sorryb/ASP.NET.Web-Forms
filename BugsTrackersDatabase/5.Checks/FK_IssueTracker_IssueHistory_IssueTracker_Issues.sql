ALTER TABLE [dbo].[IssueHistory]  WITH CHECK ADD  CONSTRAINT [FK_IssueTracker_IssueHistory_IssueTracker_Issues] FOREIGN KEY([IssueId])
REFERENCES [Issues] ([IssueId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[IssueHistory] CHECK CONSTRAINT [FK_IssueTracker_IssueHistory_IssueTracker_Issues]
GO
