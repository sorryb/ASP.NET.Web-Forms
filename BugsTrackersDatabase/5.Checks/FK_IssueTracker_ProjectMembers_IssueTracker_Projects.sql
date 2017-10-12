ALTER TABLE [dbo].[ProjectMembers]  WITH CHECK ADD  CONSTRAINT [FK_IssueTracker_ProjectMembers_IssueTracker_Projects] FOREIGN KEY([ProjectId])
REFERENCES [Projects] ([ProjectId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ProjectMembers] CHECK CONSTRAINT [FK_IssueTracker_ProjectMembers_IssueTracker_Projects]
GO
