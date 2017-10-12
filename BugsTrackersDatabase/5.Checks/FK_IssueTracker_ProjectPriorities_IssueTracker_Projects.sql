ALTER TABLE [dbo].[ProjectPriorities]  WITH CHECK ADD  CONSTRAINT [FK_IssueTracker_ProjectPriorities_IssueTracker_Projects] FOREIGN KEY([ProjectId])
REFERENCES [Projects] ([ProjectId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ProjectPriorities] CHECK CONSTRAINT [FK_IssueTracker_ProjectPriorities_IssueTracker_Projects]
GO
