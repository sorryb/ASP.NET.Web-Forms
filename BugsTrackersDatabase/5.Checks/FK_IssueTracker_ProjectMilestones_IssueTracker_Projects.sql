ALTER TABLE [dbo].[ProjectMilestones]  WITH CHECK ADD  CONSTRAINT [FK_IssueTracker_ProjectMilestones_IssueTracker_Projects] FOREIGN KEY([ProjectId])
REFERENCES [Projects] ([ProjectId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ProjectMilestones] CHECK CONSTRAINT [FK_IssueTracker_ProjectMilestones_IssueTracker_Projects]
GO
