ALTER TABLE [dbo].[ProjectStatus]  WITH CHECK ADD  CONSTRAINT [FK_IssueTracker_ProjectStatus_IssueTracker_Projects] FOREIGN KEY([ProjectId])
REFERENCES [Projects] ([ProjectId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ProjectStatus] CHECK CONSTRAINT [FK_IssueTracker_ProjectStatus_IssueTracker_Projects]
GO
