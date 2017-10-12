ALTER TABLE [dbo].[ProjectCustomFieldValues]  WITH CHECK ADD  CONSTRAINT [FK_IssueTracker_ProjectCustomFieldValues_IssueTracker_ProjectCustomFields] FOREIGN KEY([CustomFieldId])
REFERENCES [ProjectCustomFields] ([CustomFieldId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ProjectCustomFieldValues] CHECK CONSTRAINT [FK_IssueTracker_ProjectCustomFieldValues_IssueTracker_ProjectCustomFields]
GO
