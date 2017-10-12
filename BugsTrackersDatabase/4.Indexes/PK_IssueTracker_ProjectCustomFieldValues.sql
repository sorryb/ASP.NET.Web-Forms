ALTER TABLE [dbo].[ProjectCustomFieldValues] ADD  CONSTRAINT [PK_IssueTracker_ProjectCustomFieldValues] PRIMARY KEY CLUSTERED 
(
	[IssueId] ASC,
	[CustomFieldId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO