ALTER TABLE [dbo].[IssueNotifications]  WITH CHECK ADD  CONSTRAINT [FK_IssueTracker_IssueNotifications_IssueTracker_Issues] FOREIGN KEY([IssueId])
REFERENCES [Issues] ([IssueId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[IssueNotifications] CHECK CONSTRAINT [FK_IssueTracker_IssueNotifications_IssueTracker_Issues]
GO
