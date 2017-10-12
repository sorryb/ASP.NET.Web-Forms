ALTER TABLE [dbo].[RelatedIssues]  WITH CHECK ADD  CONSTRAINT [FK_IssueTracker_RelatedIssues_IssueTracker_Issues] FOREIGN KEY([PrimaryIssueId])
REFERENCES [Issues] ([IssueId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[RelatedIssues] CHECK CONSTRAINT [FK_IssueTracker_RelatedIssues_IssueTracker_Issues]
GO
