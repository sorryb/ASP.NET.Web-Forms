ALTER TABLE [dbo].[ProjectCustomFields]  WITH CHECK ADD  CONSTRAINT [FK_IssueTracker_ProjectCustomFields_IssueTracker_Projects] FOREIGN KEY([ProjectId])
REFERENCES [Projects] ([ProjectId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ProjectCustomFields] CHECK CONSTRAINT [FK_IssueTracker_ProjectCustomFields_IssueTracker_Projects]
GO
