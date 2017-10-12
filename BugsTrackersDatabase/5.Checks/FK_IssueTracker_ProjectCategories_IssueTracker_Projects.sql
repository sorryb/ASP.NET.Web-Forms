ALTER TABLE [dbo].[ProjectCategories]  WITH CHECK ADD  CONSTRAINT [FK_IssueTracker_ProjectCategories_IssueTracker_Projects] FOREIGN KEY([ProjectId])
REFERENCES [Projects] ([ProjectId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ProjectCategories] CHECK CONSTRAINT [FK_IssueTracker_ProjectCategories_IssueTracker_Projects]
GO
