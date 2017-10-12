ALTER TABLE [dbo].[IssueComments]  WITH CHECK ADD  CONSTRAINT [FK_IssueTracker_IssueComments_IssueTracker_Issues] FOREIGN KEY([IssueId])
REFERENCES [Issues] ([IssueId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[IssueComments] CHECK CONSTRAINT [FK_IssueTracker_IssueComments_IssueTracker_Issues]
GO
