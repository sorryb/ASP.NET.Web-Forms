using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Specialized;
using System.Web.UI.WebControls;
using BugsTrackers.BusinessLogicLayer;

namespace BugsTrackers.DataAccessLayer {


	//*********************************************************************
	//
	// SQLDataAccessLayer Class
	//
	// The SQLDataAccessLayer contains the data access layer for SimSoftware
	// SQL Server. This class implements the abstract methods in the
	// DataAccessLayerBaseClass class.
	//
	//*********************************************************************


    public class SQLDataAccessLayer:DataAccessLayerBaseClass {



	    //*********************************************************************
	    //
	    // Constants
	    //
	    // Each of the constants below represent the name of a SQL Stored
	    // Procedure. If you need to change the name of any stored procedure
	    // used by the Issue Tracker, modify one of the constants.
	    //
	    //*********************************************************************


        private const string SP_CATEGORY_CREATE                     = "ProjectCategories_CreateNewCategory";
        private const string SP_CATEGORY_DELETE                     = "ProjectCategories_DeleteCategory";
        private const string SP_CATEGORY_GETCATEGORIESBYPROJECTID   = "ProjectCategories_GetCategoriesByProjectId";

        private const string SP_ISSUE_CREATE                        = "Issue_CreateNewIssue";
        private const string SP_ISSUE_DELETE                        = "Issue_DeleteIssue";
        private const string SP_ISSUE_GETISSUESBYASSIGNEDUSERNAME   = "Issue_GetIssuesByAssignedUsername";
        private const string SP_ISSUE_GETISSUESBYRELEVANCY          = "Issue_GetIssuesByRelevancy";
        private const string SP_ISSUE_GETISSUESBYCREATORUSERNAME    = "Issue_GetIssuesByCreatorUsername";
        private const string SP_ISSUE_GETISSUEBYID                  = "Issue_GetIssueById";
        private const string SP_ISSUE_GETISSUESBYOWNERUSERNAME      = "Issue_GetIssuesByOwnerUsername";
        private const string SP_ISSUE_GETISSUESBYPROJECTID          = "Issue_GetIssuesByProjectId";
        private const string SP_ISSUE_UPDATE                        = "Issue_UpdateIssue";

        private const string SP_QUERY_GETQUERIESBYUSERNAME          = "Query_GetQueriesByUsername";
        private const string SP_QUERY_SAVEQUERY                     = "Query_SaveQuery";
        private const string SP_QUERY_SAVEQUERYCLAUSE               = "Query_SaveQueryClause";
        private const string SP_QUERY_GETSAVEDQUERY                 = "Query_GetSavedQuery";
        private const string SP_QUERY_DELETEQUERY                   = "Query_DeleteQuery";

        private const string SP_RELATEDISSUE_GETCHILDISSUES         = "RelatedIssue_GetChildIssues";
        private const string SP_RELATEDISSUE_GETPARENTISSUES        = "RelatedIssue_GetParentIssues";
        private const string SP_RELATEDISSUE_GETRELATEDISSUES       = "RelatedIssue_GetRelatedIssues";
        private const string SP_RELATEDISSUE_CREATENEWCHILDISSUE    = "RelatedIssue_CreateNewChildIssue";
        private const string SP_RELATEDISSUE_DELETECHILDISSUE       = "RelatedIssue_DeleteChildIssue";
        private const string SP_RELATEDISSUE_CREATENEWPARENTISSUE   = "RelatedIssue_CreateNewParentIssue";
        private const string SP_RELATEDISSUE_DELETEPARENTISSUE      = "RelatedIssue_DeleteParentIssue";
        private const string SP_RELATEDISSUE_CREATENEWRELATEDISSUE  = "RelatedIssue_CreateNewRelatedIssue";
        private const string SP_RELATEDISSUE_DELETERELATEDISSUE     = "RelatedIssue_DeleteRelatedIssue";

        private const string SP_ISSUECOMMENT_CREATE                     = "IssueComment_CreateNewIssueComment";
        private const string SP_ISSUECOMMENT_GETISSUECOMMENTBYISSUEID   = "IssueComment_GetIssueCommentsByIssueId";

		private const string SP_ISSUEATTACHMENT_CREATE                     = "IssueAttachment_CreateNewIssueAttachment";
		private const string SP_ISSUEATTACHMENT_GETISSUEATTACHMENTSBYISSUEID   = "IssueAttachment_GetIssueAttachmentsByIssueId";
		private const string SP_ISSUEATTACHMENT_GETISSUEATTACHMENTBYID   = "IssueAttachment_GetIssueAttachmentById";
		private const string SP_ISSUEATTACHMENT_DELETE   = "IssueAttachment_Delete";

        private const string SP_ISSUEHISTORY_GETISSUEHISTORYBYISSUEID   = "IssueHistory_GetIssueHistoryByIssueId";

        private const string SP_ISSUENOTIFICATION_DELETE                            = "IssueNotification_DeleteIssueNotification";
        private const string SP_ISSUENOTIFICATION_CREATE                            = "IssueNotification_CreateNewIssueNotification";
        private const string SP_ISSUENOTIFICATION_GETISSUENOTIFICATIONSBYISSUEID    = "IssueNotification_GetIssueNotificationsByIssueId";

        private const string SP_MILESTONE_CREATE                    = "ProjectMilestones_CreateNewMilestone";
        private const string SP_MILESTONE_DELETE                    = "ProjectMilestones_DeleteMilestone";
        private const string SP_MILESTONE_GETMILESTONEBYPROJECTID   = "ProjectMilestones_GetMilestonesByProjectId";

        private const string SP_PRIORITY_CREATE                     = "ProjectPriorities_CreateNewPriority";
        private const string SP_PRIORITY_DELETE                     = "ProjectPriorities_DeletePriority";
        private const string SP_PRIORITY_GETPRIORITIESBYPROJECTID   = "ProjectPriorities_GetPrioritiesByProjectId";

        private const string SP_PROJECT_CREATE                      = "Project_CreateNewProject";
        private const string SP_PROJECT_DELETE                      = "Project_DeleteProject";
        private const string SP_PROJECT_GETALLPROJECTS              = "Project_GetAllProjects";
        private const string SP_PROJECT_GETAPROJECTBYID             = "Project_GetProjectById";
        private const string SP_PROJECT_UPDATE                      = "Project_UpdateProject";
        private const string SP_PROJECT_ADDUSERTOPROJECT            = "Project_AddUserToProject";
        private const string SP_PROJECT_REMOVEUSERFROMPROJECT       = "Project_RemoveUserFromProject";
        private const string SP_PROJECT_GETPROJECTBYMEMBERUSERNAME  = "Project_GetProjectByUsername";
		private const string SP_PROJECT_CLONEPROJECT                = "Project_CloneProject";

        private const string SP_STATUS_CREATE               = "ProjectStatus_CreateNewStatus";
        private const string SP_STATUS_DELETE               = "ProjectStatus_DeleteStatus";
        private const string SP_STATUS_GETSTATUSBYPROJECTID = "ProjectStatus_GetStatusByProjectId";

        private const string SP_CUSTOMFIELD_GETCUSTOMFIELDSBYPROJECTID      = "CustomField_GetCustomFieldsByProjectId";
        private const string SP_CUSTOMFIELD_GETCUSTOMFIELDSBYISSUEID        = "CustomField_GetCustomFieldsByIssueId";
        private const string SP_CUSTOMFIELD_CREATE                          = "CustomField_CreateNewCustomField";
        private const string SP_CUSTOMFIELD_UPDATE                          = "CustomField_UpdateCustomField";
        private const string SP_CUSTOMFIELD_DELETE                          = "CustomField_DeleteCustomField";
        private const string SP_CUSTOMFIELD_SAVECUSTOMFIELDVALUE            = "CustomField_SaveCustomFieldValue";

        private const string SP_USER_AUTHENTICATE           = "User_Authenticate";
        private const string SP_USER_GETUSERBYUSERNAME      = "User_GetUserByUsername";
        private const string SP_USER_GETUSERBYID            = "User_GetUserById";
        private const string SP_USER_GETALLUSERS            = "User_GetAllUsers";
        private const string SP_USER_GETALLUSERSBYROLENAME  = "User_GetAllUsersByRoleName";
        private const string SP_USER_GETUSERSBYPROJECTID    = "User_GetUsersByProjectId";
        private const string SP_USER_UPDATEUSER             = "User_UpdateUser";
        private const string SP_USER_CREATENEWUSER          = "User_CreateNewUser";

        private const string SP_ROLE_GETALLROLES   = "Role_GetAllRoles";



		/*** INSTANCE PROPERTIES ***/

		public override bool SupportsProjectCloning 
		{
			get { return true; }
		}





        /*** INSTANCE METHODS ***/



	    //*********************************************************************
	    //
	    // Category Methods
	    //
	    // The following methods are used for working with project categories.
	    //
	    //*********************************************************************

        public override int CreateNewCategory(Category newCategory) 
		{
            // Validate Parameters
            if ( newCategory == null )
                throw new ArgumentNullException("newCategory");

            // Execute SQL Command
            SqlCommand sqlCmd = new SqlCommand();

            AddParamToSQLCmd(sqlCmd, "@ReturnValue", SqlDbType.Int, 0, ParameterDirection.ReturnValue, null);
            AddParamToSQLCmd(sqlCmd, "@CategoryName", SqlDbType.NVarChar, 256, ParameterDirection.Input, newCategory.Name);
            AddParamToSQLCmd(sqlCmd, "@ProjectId", SqlDbType.Int, 4, ParameterDirection.Input, newCategory.ProjectId);
            AddParamToSQLCmd(sqlCmd, "@ParentCategoryId", SqlDbType.Int, 4, ParameterDirection.Input, newCategory.ParentCategoryId);

            SetCommandType(sqlCmd,CommandType.StoredProcedure, SP_CATEGORY_CREATE);
            ExecuteScalarCmd(sqlCmd);
            return ((int)sqlCmd.Parameters["@ReturnValue"].Value);
        }


        public override bool DeleteCategory(int categoryId) {
            // Validate Parameters
            if (categoryId <= DefaultValues.GetCategoryIdMinValue())
                throw (new ArgumentOutOfRangeException("categoryId"));

            // Execute SQL Command
            SqlCommand sqlCmd = new SqlCommand();

            AddParamToSQLCmd(sqlCmd, "@ReturnValue", SqlDbType.Int, 4, ParameterDirection.ReturnValue, null);
            AddParamToSQLCmd(sqlCmd, "@CategoryIdToDelete", SqlDbType.Int, 4, ParameterDirection.Input, categoryId);

            SetCommandType( sqlCmd, CommandType.StoredProcedure, SP_CATEGORY_DELETE);
            ExecuteScalarCmd(sqlCmd);
            int returnValue = (int)sqlCmd.Parameters["@ReturnValue"].Value;
            return (returnValue == 0 ? true : false);
        }




        public override CategoryCollection GetCategoriesByProjectId(int projectId) {
            // Validate Parameters
            if (projectId <= DefaultValues.GetProjectIdMinValue())
                throw (new ArgumentOutOfRangeException("projectId"));

            // Execute SQL Command
            SqlCommand sqlCmd = new SqlCommand();

            AddParamToSQLCmd(sqlCmd, "@ProjectId", SqlDbType.Int, 0, ParameterDirection.Input, projectId);

            SetCommandType(sqlCmd,CommandType.StoredProcedure, SP_CATEGORY_GETCATEGORIESBYPROJECTID);
            GenerateCollectionFromReader sqlData = new GenerateCollectionFromReader(GenerateCategoryCollectionFromReader);
            CategoryCollection results = (CategoryCollection) ExecuteReaderCmd(sqlCmd, sqlData);
            return results;
        }




	    //*********************************************************************
	    //
	    // Issue Comment Methods
	    //
	    // The following methods are used for working with user comments added
	    // to issues.
	    //
	    //*********************************************************************

        public override int CreateNewIssueComment (IssueComment newComment) {
            // validate Parameters
            if (newComment == null)
                throw (new ArgumentNullException("newComment"));

            // Execute SQL Command
            SqlCommand sqlCmd = new SqlCommand();

            AddParamToSQLCmd(sqlCmd, "@ReturnValue", SqlDbType.Int, 0, ParameterDirection.ReturnValue, null);
            AddParamToSQLCmd(sqlCmd, "@IssueId", SqlDbType.Int, 0, ParameterDirection.Input, newComment.IssueId);
            AddParamToSQLCmd(sqlCmd, "@CreatorUserName", SqlDbType.NText, 256, ParameterDirection.Input, newComment.CreatorUsername);
            sqlCmd.Parameters.Add("@Comment", SqlDbType.NText);
            sqlCmd.Parameters["@Comment"].Value = newComment.Comment;

            SetCommandType( sqlCmd, CommandType.StoredProcedure, SP_ISSUECOMMENT_CREATE);
            ExecuteScalarCmd(sqlCmd);
            return ((int)sqlCmd.Parameters["@ReturnValue"].Value);
        }


        public override IssueCommentCollection GetIssueCommentsByIssueId  (int issueId) {
            // Validate Parameters
            if (issueId <= DefaultValues.GetIssueCommentIdMinValue() )
                throw (new ArgumentOutOfRangeException("issueId"));

            // Execute SQL Command
            SqlCommand sqlCmd = new SqlCommand();

            AddParamToSQLCmd(sqlCmd, "@IssueId", SqlDbType.Int, 0, ParameterDirection.Input, issueId);

            SetCommandType(sqlCmd,CommandType.StoredProcedure, SP_ISSUECOMMENT_GETISSUECOMMENTBYISSUEID);
            GenerateCollectionFromReader sqlData = new GenerateCollectionFromReader (GenerateIssueCommentCollectionFromReader);
            IssueCommentCollection stsCollection = (IssueCommentCollection) ExecuteReaderCmd(sqlCmd, sqlData);
            return (stsCollection);
        }



		//*********************************************************************
		//
		// Issue Attachment Methods
		//
		// The following methods are used for working with file attachments added
		// to issues.
		//
		//*********************************************************************

		public override int CreateNewIssueAttachment (IssueAttachment newAttachment) 
		{
			// validate Parameters
			if (newAttachment == null)
				throw (new ArgumentNullException("newAttachment"));

			// Execute SQL Command
			SqlCommand sqlCmd = new SqlCommand();

			AddParamToSQLCmd(sqlCmd, "@ReturnValue", SqlDbType.Int, 0, ParameterDirection.ReturnValue, null);
			AddParamToSQLCmd(sqlCmd, "@IssueId", SqlDbType.Int, 0, ParameterDirection.Input, newAttachment.IssueId);
			AddParamToSQLCmd(sqlCmd, "@CreatorUserName", SqlDbType.NText, 256, ParameterDirection.Input, newAttachment.CreatorUsername);
			AddParamToSQLCmd(sqlCmd, "@FileName", SqlDbType.NText, 250, ParameterDirection.Input, newAttachment.FileName);
			AddParamToSQLCmd(sqlCmd, "@ContentType", SqlDbType.NText, 50, ParameterDirection.Input, newAttachment.ContentType);
			sqlCmd.Parameters.Add("@Attachment", SqlDbType.Image, newAttachment.Attachment.Length);
			sqlCmd.Parameters["@Attachment"].Value = newAttachment.Attachment;

			SetCommandType( sqlCmd, CommandType.StoredProcedure, SP_ISSUEATTACHMENT_CREATE);
			ExecuteScalarCmd(sqlCmd);
			return ((int)sqlCmd.Parameters["@ReturnValue"].Value);
		}



		public override IssueAttachmentCollection GetIssueAttachmentsByIssueId  (int issueId) 
		{
			// Validate Parameters
			if (issueId <= DefaultValues.GetIssueCommentIdMinValue() )
				throw (new ArgumentOutOfRangeException("issueId"));

			// Execute SQL Command
			SqlCommand sqlCmd = new SqlCommand();

			AddParamToSQLCmd(sqlCmd, "@IssueId", SqlDbType.Int, 0, ParameterDirection.Input, issueId);

			SetCommandType(sqlCmd,CommandType.StoredProcedure, SP_ISSUEATTACHMENT_GETISSUEATTACHMENTSBYISSUEID);
			GenerateCollectionFromReader sqlData = new GenerateCollectionFromReader (GenerateIssueAttachmentCollectionFromReader);
			IssueAttachmentCollection stsCollection = (IssueAttachmentCollection) ExecuteReaderCmd(sqlCmd, sqlData);
			return (stsCollection);
		}




		public override IssueAttachment GetIssueAttachmentById  (int attachmentId) 
		{
			IssueAttachment attachment = null;

			// validate Parameters
			if (attachmentId <= DefaultValues.GetIssueAttachmentIdMinValue() )
				throw (new ArgumentOutOfRangeException("attachmentId"));

			// Setup SQL Command
			SqlCommand sqlCmd = new SqlCommand();
			AddParamToSQLCmd(sqlCmd, "@AttachmentId", SqlDbType.Int, 0, ParameterDirection.Input, attachmentId);
			SetCommandType( sqlCmd, CommandType.StoredProcedure, SP_ISSUEATTACHMENT_GETISSUEATTACHMENTBYID);

			// Execute Reader
			if (ConnectionString == string.Empty)
				throw (new ArgumentOutOfRangeException("ConnectionString"));


			using (SqlConnection cn = new SqlConnection(this.ConnectionString)) 
			{
				sqlCmd.Connection = cn;
				cn.Open();
				SqlDataReader dtr = sqlCmd.ExecuteReader();
				if (dtr.Read()) 
				{
					attachment = new IssueAttachment((string)dtr["FileName"],(string)dtr["ContentType"],(byte[])dtr["Attachment"]);
				}

			}

			return attachment;
		}


		public override void DeleteIssueAttachment(int attachmentId) 
		{
			// Validate Parameters
			if (attachmentId <= DefaultValues.GetIssueIdMinValue() )
				throw (new ArgumentOutOfRangeException("attachmentId"));


			// Execute SQL Command
			SqlCommand sqlCmd = new SqlCommand();

			AddParamToSQLCmd(sqlCmd, "@ReturnValue", SqlDbType.Int, 4, ParameterDirection.ReturnValue, null);
			AddParamToSQLCmd(sqlCmd, "@AttachmentId", SqlDbType.Int, 0, ParameterDirection.Input, attachmentId);

			SetCommandType( sqlCmd, CommandType.StoredProcedure, SP_ISSUEATTACHMENT_DELETE);
			ExecuteScalarCmd(sqlCmd);
		}




	    //*********************************************************************
	    //
	    // Issue History Methods
	    //
	    // The following methods are used for tracking changes to issues.
	    //
	    //*********************************************************************



        public override IssueHistoryCollection GetIssueHistoryByIssueId  (int issueId) 
		{
            // Validate Parameters
            if (issueId <= DefaultValues.GetIssueIdMinValue() )
                throw (new ArgumentOutOfRangeException("issueId"));

            // Execute SQL Command
            SqlCommand sqlCmd = new SqlCommand();

            AddParamToSQLCmd(sqlCmd, "@IssueId", SqlDbType.Int, 0, ParameterDirection.Input, issueId);

            SetCommandType(sqlCmd,CommandType.StoredProcedure, SP_ISSUEHISTORY_GETISSUEHISTORYBYISSUEID);
            GenerateCollectionFromReader sqlData = new GenerateCollectionFromReader (GenerateIssueHistoryCollectionFromReader);
            IssueHistoryCollection stsCollection = (IssueHistoryCollection) ExecuteReaderCmd(sqlCmd, sqlData);
            return (stsCollection);
        }


	    //*********************************************************************
	    //
	    // Issue Notification Methods
	    //
	    // The following methods are used for notifying users about changes
	    // to issues.
	    //
	    //*********************************************************************



        public override int CreateNewIssueNotification (IssueNotification newNotification) {
            // Validate Parameters
            if (newNotification == null)
                throw (new ArgumentNullException("newNotification"));

            // Execute SQL Command
            SqlCommand sqlCmd = new SqlCommand();

            AddParamToSQLCmd(sqlCmd, "@ReturnValue", SqlDbType.Int, 0, ParameterDirection.ReturnValue, null);
            AddParamToSQLCmd(sqlCmd, "@IssueId", SqlDbType.Int, 0, ParameterDirection.Input, newNotification.IssueId);
            AddParamToSQLCmd(sqlCmd, "@NotificationUserName", SqlDbType.NText, 255, ParameterDirection.Input, newNotification.NotificationUsername);

            SetCommandType( sqlCmd, CommandType.StoredProcedure, SP_ISSUENOTIFICATION_CREATE);
            ExecuteScalarCmd(sqlCmd);
            return ((int)sqlCmd.Parameters["@ReturnValue"].Value);
        }


        public override bool DeleteIssueNotification(int issueId, string username) {
            // Validate Parameters
            if (issueId <= DefaultValues.GetIssueIdMinValue() )
                throw (new ArgumentOutOfRangeException("issueId"));


            if (username == String.Empty)
                throw (new ArgumentOutOfRangeException("username"));

            // Execute SQL Command
            SqlCommand sqlCmd = new SqlCommand();

            AddParamToSQLCmd(sqlCmd, "@ReturnValue", SqlDbType.Int, 4, ParameterDirection.ReturnValue, null);
            AddParamToSQLCmd(sqlCmd, "@IssueId", SqlDbType.Int, 0, ParameterDirection.Input, issueId);
            AddParamToSQLCmd(sqlCmd, "@Username", SqlDbType.NVarChar, 255, ParameterDirection.Input, username);

            SetCommandType( sqlCmd, CommandType.StoredProcedure, SP_ISSUENOTIFICATION_DELETE);
            ExecuteScalarCmd(sqlCmd);
            int returnValue = (int)sqlCmd.Parameters["@ReturnValue"].Value;
            return (returnValue == 0 ? true : false);
        }



        public override IssueNotificationCollection GetIssueNotificationsByIssueId  (int issueId) {
            // Validate Parameters
            if (issueId <= DefaultValues.GetIssueIdMinValue() )
                throw (new ArgumentOutOfRangeException("issueId"));

            // Execute SQL Command
            SqlCommand sqlCmd = new SqlCommand();

            AddParamToSQLCmd(sqlCmd, "@IssueId", SqlDbType.Int, 0, ParameterDirection.Input, issueId);

            SetCommandType(sqlCmd,CommandType.StoredProcedure, SP_ISSUENOTIFICATION_GETISSUENOTIFICATIONSBYISSUEID);
            GenerateCollectionFromReader sqlData = new GenerateCollectionFromReader (GenerateIssueNotificationCollectionFromReader);
            IssueNotificationCollection notCollection = (IssueNotificationCollection) ExecuteReaderCmd(sqlCmd, sqlData);
            return (notCollection);
        }




	    //*********************************************************************
	    //
	    // Query Methods
	    //
	    // The following methods are used for performing queries against
	    // the issues database.
	    //
	    // This method was written in the hospital while waiting for my wife
	    // to go into labor.
	    //
	    //*********************************************************************


        public override bool SaveQuery(string username, int projectId, string queryName, QueryClauseCollection queryClauses) {
            // Validate Parameters
            if (username == null || username == String.Empty)
                throw (new ArgumentOutOfRangeException("username"));

            if (queryName == null || queryName == String.Empty)
                throw (new ArgumentOutOfRangeException("queryName"));

            if (queryClauses.Count == 0)
                throw (new ArgumentOutOfRangeException("queryClauses"));


            // Create Save Query Command
            SqlCommand cmdSaveQuery = new SqlCommand();

            AddParamToSQLCmd(cmdSaveQuery, "@ReturnValue", SqlDbType.Int, 0, ParameterDirection.ReturnValue, null);
            AddParamToSQLCmd(cmdSaveQuery, "@Username", SqlDbType.NVarChar, 255, ParameterDirection.Input, username);
            AddParamToSQLCmd(cmdSaveQuery, "@projectId", SqlDbType.Int, 0, ParameterDirection.Input, projectId);
            AddParamToSQLCmd(cmdSaveQuery, "@QueryName", SqlDbType.NText, 50, ParameterDirection.Input, queryName);

            SetCommandType(cmdSaveQuery, CommandType.StoredProcedure, SP_QUERY_SAVEQUERY);



            // Create Save Query Clause Command
            SqlCommand cmdClause = new SqlCommand();

            cmdClause.Parameters.Add( "@QueryId", SqlDbType.Int);
            cmdClause.Parameters.Add( "@BooleanOperator", SqlDbType.NVarChar, 50);
            cmdClause.Parameters.Add( "@FieldName", SqlDbType.NVarChar, 50);
            cmdClause.Parameters.Add( "@ComparisonOperator", SqlDbType.NVarChar, 50);
            cmdClause.Parameters.Add( "@FieldValue", SqlDbType.NVarChar, 50);
            cmdClause.Parameters.Add( "@DataType", SqlDbType.Int);

            SetCommandType(cmdClause, CommandType.StoredProcedure, SP_QUERY_SAVEQUERYCLAUSE);



            // Prepare connection
            using (SqlConnection cn = new SqlConnection(this.ConnectionString)) {

                cn.Open();

                // Execute Query Command
                cmdSaveQuery.Connection = cn;
                cmdSaveQuery.ExecuteNonQuery();
                int queryId = (int)cmdSaveQuery.Parameters["@ReturnValue"].Value;
                if (queryId == 0)
                    return false;



                // Save Query Clauses
                cmdClause.Connection = cn;
                foreach (QueryClause clause in queryClauses) {
                    cmdClause.Parameters["@QueryId"].Value = queryId;
                    cmdClause.Parameters["@BooleanOperator"].Value = clause.BooleanOperator;
                    cmdClause.Parameters["@FieldName"].Value = clause.FieldName;
                    cmdClause.Parameters["@ComparisonOperator"].Value = clause.ComparisonOperator;
                    cmdClause.Parameters["@FieldValue"].Value = clause.FieldValue;
                    cmdClause.Parameters["@DataType"].Value = clause.DataType;

                    cmdClause.ExecuteNonQuery();

                }

            }

            return true;
        }



        public override bool DeleteQuery(int queryId) {
            // Validate Parameters
            if (queryId <= DefaultValues.GetQueryIdMinValue() )
                throw (new ArgumentOutOfRangeException("queryId"));

            // Execute SQL Command
            SqlCommand sqlCmd = new SqlCommand();

            AddParamToSQLCmd(sqlCmd, "@queryId", SqlDbType.Int, 0, ParameterDirection.Input, queryId);

            SetCommandType(sqlCmd,CommandType.StoredProcedure, SP_QUERY_DELETEQUERY);
            ExecuteScalarCmd(sqlCmd);
            return true;
        }



        public override QueryCollection GetQueriesByUsername(string username, int projectId) {
            if (username == null || username == String.Empty)
                throw new ArgumentOutOfRangeException("username");

            // Execute SQL Command
            SqlCommand sqlCmd = new SqlCommand();

            AddParamToSQLCmd(sqlCmd, "@Username", SqlDbType.NVarChar, 255, ParameterDirection.Input, username);
            AddParamToSQLCmd(sqlCmd, "@projectId", SqlDbType.Int, 0, ParameterDirection.Input, projectId);

            SetCommandType(sqlCmd,CommandType.StoredProcedure, SP_QUERY_GETQUERIESBYUSERNAME);
            GenerateCollectionFromReader sqlData = new GenerateCollectionFromReader (GenerateQueryCollectionFromReader);
            QueryCollection qCollection = (QueryCollection) ExecuteReaderCmd(sqlCmd, sqlData);
            return (qCollection);
        }



	    //*********************************************************************
	    //
	    // Issue Methods
	    //
	    // The following methods are used for working with issues.
	    //
	    //*********************************************************************


        public override int CreateNewIssue (Issue newIssue) {
            // Validate Parameters
            if (newIssue == null)
                throw (new ArgumentNullException("newIssue"));

            // Execute SQL Command
            SqlCommand sqlCmd = new SqlCommand();

            AddParamToSQLCmd(sqlCmd, "@ReturnValue", SqlDbType.Int, 0, ParameterDirection.ReturnValue, null);
            AddParamToSQLCmd(sqlCmd, "@ProjectId", SqlDbType.Int, 0, ParameterDirection.Input, newIssue.ProjectId);
            AddParamToSQLCmd(sqlCmd, "@IssueTitle", SqlDbType.NText, 255, ParameterDirection.Input, newIssue.Title);
            AddParamToSQLCmd(sqlCmd, "@IssueCategoryId", SqlDbType.Int, 0, ParameterDirection.Input, newIssue.CategoryId);
            AddParamToSQLCmd(sqlCmd, "@IssueStatusId", SqlDbType.Int, 0, ParameterDirection.Input, newIssue.StatusId);
            AddParamToSQLCmd(sqlCmd, "@IssuePriorityId", SqlDbType.Int, 0, ParameterDirection.Input, newIssue.PriorityId);
            AddParamToSQLCmd(sqlCmd, "@IssueMilestoneId", SqlDbType.Int, 0, ParameterDirection.Input, newIssue.MilestoneId);
            AddParamToSQLCmd(sqlCmd, "@IssueAssignedId", SqlDbType.Int, 0, ParameterDirection.Input, newIssue.AssignedId);
            AddParamToSQLCmd(sqlCmd, "@IssueOwnerId", SqlDbType.Int, 0, ParameterDirection.Input, newIssue.OwnerId);
            AddParamToSQLCmd(sqlCmd, "@IssueCreatorUsername", SqlDbType.NText, 255, ParameterDirection.Input, newIssue.CreatorUsername);


            SetCommandType(sqlCmd,CommandType.StoredProcedure, SP_ISSUE_CREATE);
            ExecuteScalarCmd(sqlCmd);
            return ((int)sqlCmd.Parameters["@ReturnValue"].Value);
        }


        public override bool UpdateIssue(Issue issueToUpdate) {
            // Validate Parameters
            if (issueToUpdate == null)
                throw (new ArgumentNullException("issueToUpdate"));

            // Execute SQL Command
            SqlCommand sqlCmd = new SqlCommand();

            AddParamToSQLCmd(sqlCmd, "@ReturnValue", SqlDbType.Int, 0, ParameterDirection.ReturnValue, null);
            AddParamToSQLCmd(sqlCmd, "@IssueId", SqlDbType.Int, 0, ParameterDirection.Input, issueToUpdate.Id);
            AddParamToSQLCmd(sqlCmd, "@ProjectId", SqlDbType.Int, 0, ParameterDirection.Input, issueToUpdate.ProjectId);
            AddParamToSQLCmd(sqlCmd, "@IssueTitle", SqlDbType.NText, 255, ParameterDirection.Input, issueToUpdate.Title);
            AddParamToSQLCmd(sqlCmd, "@IssueCategoryId", SqlDbType.Int, 0, ParameterDirection.Input, issueToUpdate.CategoryId);
            AddParamToSQLCmd(sqlCmd, "@IssueStatusId", SqlDbType.Int, 0, ParameterDirection.Input, issueToUpdate.StatusId);
            AddParamToSQLCmd(sqlCmd, "@IssuePriorityId", SqlDbType.Int, 0, ParameterDirection.Input, issueToUpdate.PriorityId);
            AddParamToSQLCmd(sqlCmd, "@IssueMilestoneId", SqlDbType.Int, 0, ParameterDirection.Input, issueToUpdate.MilestoneId);
            AddParamToSQLCmd(sqlCmd, "@IssueAssignedId", SqlDbType.Int, 0, ParameterDirection.Input, issueToUpdate.AssignedId);
            AddParamToSQLCmd(sqlCmd, "@IssueOwnerId", SqlDbType.Int, 0, ParameterDirection.Input, issueToUpdate.OwnerId);
            AddParamToSQLCmd(sqlCmd, "@IssueCreatorUsername", SqlDbType.NText, 255, ParameterDirection.Input, issueToUpdate.CreatorUsername);


            SetCommandType(sqlCmd,CommandType.StoredProcedure, SP_ISSUE_UPDATE);
            ExecuteScalarCmd(sqlCmd);
            int returnValue = (int)sqlCmd.Parameters["@ReturnValue"].Value;
            return (returnValue == 0 ? true : false);
        }



        public override bool DeleteIssue(int issueID) {
            // Validate Parameters
            if (issueID <= DefaultValues.GetIssueIdMinValue())
                throw (new ArgumentOutOfRangeException("issueID"));

            // Execute SSQL Command
            SqlCommand sqlCmd = new SqlCommand();

            AddParamToSQLCmd(sqlCmd, "@ReturnValue", SqlDbType.Int, 0, ParameterDirection.ReturnValue, null);
            AddParamToSQLCmd(sqlCmd, "@IssueIdToDelete", SqlDbType.Int, 0, ParameterDirection.Input, issueID);

            SetCommandType( sqlCmd, CommandType.StoredProcedure, SP_ISSUE_DELETE);
            ExecuteScalarCmd(sqlCmd);
            int returnValue = (int)sqlCmd.Parameters["@ReturnValue"].Value;
            return (returnValue == 0 ? true : false);
        }





        public override IssueCollection GetIssuesByAssignedUsername(int projectId, string assignedUsername) {
            // Validate Parameters
            if (projectId <= DefaultValues.GetProjectIdMinValue() )
                throw (new ArgumentOutOfRangeException("projectId"));

            if (assignedUsername == null || assignedUsername == String.Empty)
                throw (new ArgumentOutOfRangeException("assignedUsername"));

            // Execute SQL Command
            SqlCommand sqlCmd = new SqlCommand();

            AddParamToSQLCmd(sqlCmd, "@ProjectId", SqlDbType.Int, 0, ParameterDirection.Input, projectId);
            AddParamToSQLCmd(sqlCmd, "@Username", SqlDbType.NVarChar, 255, ParameterDirection.Input, assignedUsername);

            SetCommandType(sqlCmd,CommandType.StoredProcedure, SP_ISSUE_GETISSUESBYASSIGNEDUSERNAME);
            GenerateCollectionFromReader sqlData = new GenerateCollectionFromReader (GenerateIssueCollectionFromReader);
            IssueCollection issCollection = (IssueCollection) ExecuteReaderCmd(sqlCmd, sqlData);
            return (issCollection);
        }



        public override IssueCollection PerformQuery(int projectId, QueryClauseCollection queryClauses) {
            // Validate Parameters

            if (projectId <= DefaultValues.GetProjectIdMinValue())
                throw (new ArgumentOutOfRangeException("projectId"));

            if (queryClauses.Count == 0)
                throw (new ArgumentOutOfRangeException("queryClauses"));


            // Build Command Text
            StringBuilder commandBuilder = new StringBuilder();
            commandBuilder.Append("SELECT * FROM IssuesView WHERE ProjectId=@projectId AND Disabled=0 AND IssueId IN (SELECT IssueId FROM Issues WHERE 1=1 ");

            int i = 0;
            foreach (QueryClause qc in queryClauses) {
                commandBuilder.AppendFormat( " {0} {1} {2} @p{3}", qc.BooleanOperator, qc.FieldName, qc.ComparisonOperator, i);
                i++;
            }

            commandBuilder.Append( ") ORDER BY IssueId DESC" );


            // Create Command object
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandText = commandBuilder.ToString();


            // Build Parameter List
            sqlCmd.Parameters.Add("@projectId", SqlDbType.Int).Value = projectId;
            i = 0;
            foreach (QueryClause qc in queryClauses) {
                sqlCmd.Parameters.Add( "@p" + i.ToString(), qc.DataType ).Value = qc.FieldValue;
                i ++;
            }

            GenerateCollectionFromReader sqlData = new GenerateCollectionFromReader (GenerateIssueCollectionFromReader);
            IssueCollection issCollection = (IssueCollection) ExecuteReaderCmd(sqlCmd, sqlData);
            return (issCollection);
        }



        public override IssueCollection PerformSavedQuery(int projectId,int queryId) {
            // Validate Parameters
            if (queryId <= DefaultValues.GetQueryIdMinValue() )
                throw (new ArgumentOutOfRangeException("queryId"));

            if (projectId <= DefaultValues.GetProjectIdMinValue())
                throw (new ArgumentOutOfRangeException("projectId"));

            // Get Query Clauses
            SqlCommand sqlCmd = new SqlCommand();

            AddParamToSQLCmd(sqlCmd, "@QueryId", SqlDbType.Int, 0, ParameterDirection.Input, queryId);

            SetCommandType(sqlCmd,CommandType.StoredProcedure, SP_QUERY_GETSAVEDQUERY);
            GenerateCollectionFromReader sqlData = new GenerateCollectionFromReader (GenerateQueryClauseCollectionFromReader);
            QueryClauseCollection queryClauses = (QueryClauseCollection) ExecuteReaderCmd(sqlCmd, sqlData);

            return PerformQuery(projectId, queryClauses);
        }



        public override IssueCollection GetIssuesByRelevancy(int projectId, string username) {
            // Validate Parameters
            if (projectId <= DefaultValues.GetProjectIdMinValue() )
                throw (new ArgumentOutOfRangeException("projectId"));

            if (username == String.Empty)
                throw (new ArgumentOutOfRangeException("username"));

            // Execute SQL Command
            SqlCommand sqlCmd = new SqlCommand();

            AddParamToSQLCmd(sqlCmd, "@projectId", SqlDbType.Int, 0, ParameterDirection.Input, projectId);
            AddParamToSQLCmd(sqlCmd, "@username", SqlDbType.NText, 255, ParameterDirection.Input, username);

            SetCommandType(sqlCmd,CommandType.StoredProcedure, SP_ISSUE_GETISSUESBYRELEVANCY);
            GenerateCollectionFromReader sqlData = new GenerateCollectionFromReader (GenerateIssueCollectionFromReader);
            IssueCollection issCollection = (IssueCollection) ExecuteReaderCmd(sqlCmd, sqlData);
            return (issCollection);
        }




        public override IssueCollection GetIssuesByCreatorUsername(int projectId, string creatorUsername) {
            // Validate Parameters
            if (projectId <= DefaultValues.GetProjectIdMinValue() )
                throw (new ArgumentOutOfRangeException("projectId"));

            if (creatorUsername == String.Empty)
                throw (new ArgumentOutOfRangeException("creatorUsername"));

            // Execute SQL Command
            SqlCommand sqlCmd = new SqlCommand();

            AddParamToSQLCmd(sqlCmd, "@projectId", SqlDbType.Int, 0, ParameterDirection.Input, projectId);
            AddParamToSQLCmd(sqlCmd, "@creatorUsername", SqlDbType.NText, 255, ParameterDirection.Input, creatorUsername);

            SetCommandType(sqlCmd,CommandType.StoredProcedure, SP_ISSUE_GETISSUESBYCREATORUSERNAME);
            GenerateCollectionFromReader sqlData = new GenerateCollectionFromReader (GenerateIssueCollectionFromReader);
            IssueCollection issCollection = (IssueCollection) ExecuteReaderCmd(sqlCmd, sqlData);
            return (issCollection);
        }




        public override IssueCollection GetIssuesByOwnerUsername(int projectId, string ownerUsername) {
            // Validate Parameters
            if (projectId <= DefaultValues.GetProjectIdMinValue() )
                throw (new ArgumentOutOfRangeException("projectId"));

            if (ownerUsername == String.Empty)
                throw (new ArgumentOutOfRangeException("ownerUsername"));

            SqlCommand sqlCmd = new SqlCommand();

            AddParamToSQLCmd(sqlCmd, "@ProjectId", SqlDbType.Int, 0, ParameterDirection.Input, projectId);
            AddParamToSQLCmd(sqlCmd, "@Username", SqlDbType.NText, 255, ParameterDirection.Input, ownerUsername);

            SetCommandType(sqlCmd,CommandType.StoredProcedure, SP_ISSUE_GETISSUESBYOWNERUSERNAME);
            GenerateCollectionFromReader sqlData = new GenerateCollectionFromReader (GenerateIssueCollectionFromReader);
            IssueCollection issCollection = (IssueCollection) ExecuteReaderCmd(sqlCmd, sqlData);
            return (issCollection);
        }





	    //*********************************************************************
	    //
	    // Related Issue Methods
	    //
	    // The following methods are used for retrieving issues related
	    // to the current issue.
	    //
	    //*********************************************************************


        public override RelatedIssueCollection GetChildIssues(int issueId) {
            // Validate Parameters
            if (issueId <= DefaultValues.GetIssueIdMinValue())
                throw (new ArgumentOutOfRangeException("issueId"));

            // Execute SQL Command
            SqlCommand sqlCmd = new SqlCommand();

            AddParamToSQLCmd(sqlCmd, "@IssueId", SqlDbType.Int, 0, ParameterDirection.Input, issueId);
            AddParamToSQLCmd(sqlCmd, "@RelationType", SqlDbType.Int, 0, ParameterDirection.Input, IssueRelationType.ParentChild);

            SetCommandType(sqlCmd,CommandType.StoredProcedure, SP_RELATEDISSUE_GETCHILDISSUES);
            GenerateCollectionFromReader sqlData = new GenerateCollectionFromReader (GenerateRelatedIssueCollectionFromReader);
            RelatedIssueCollection issCollection = (RelatedIssueCollection) ExecuteReaderCmd(sqlCmd, sqlData);
            return (issCollection);
        }



        public override RelatedIssueCollection GetParentIssues(int issueId) {
            // Validate Parameters
            if (issueId <= DefaultValues.GetIssueIdMinValue())
                throw (new ArgumentOutOfRangeException("issueId"));

            // Execute SQL Command
            SqlCommand sqlCmd = new SqlCommand();

            AddParamToSQLCmd(sqlCmd, "@IssueId", SqlDbType.Int, 0, ParameterDirection.Input, issueId);
            AddParamToSQLCmd(sqlCmd, "@RelationType", SqlDbType.Int, 0, ParameterDirection.Input, IssueRelationType.ParentChild);

            SetCommandType(sqlCmd,CommandType.StoredProcedure, SP_RELATEDISSUE_GETPARENTISSUES);
            GenerateCollectionFromReader sqlData = new GenerateCollectionFromReader (GenerateRelatedIssueCollectionFromReader);
            RelatedIssueCollection issCollection = (RelatedIssueCollection) ExecuteReaderCmd(sqlCmd, sqlData);
            return (issCollection);
        }



        public override int CreateNewChildIssue (int primaryIssueId, int secondaryIssueId) {
            // Validate Parameters
            if (primaryIssueId <= DefaultValues.GetIssueIdMinValue())
                throw (new ArgumentOutOfRangeException("primaryIssueId"));

            if (secondaryIssueId <= DefaultValues.GetIssueIdMinValue())
                throw (new ArgumentOutOfRangeException("secondaryIssueId"));

            // Execute SQL Command
            SqlCommand sqlCmd = new SqlCommand();

            AddParamToSQLCmd(sqlCmd, "@ReturnValue", SqlDbType.Int, 0, ParameterDirection.ReturnValue, null);
            AddParamToSQLCmd(sqlCmd, "@primaryIssueId", SqlDbType.Int, 0, ParameterDirection.Input, primaryIssueId);
            AddParamToSQLCmd(sqlCmd, "@secondaryIssueId", SqlDbType.Int, 0, ParameterDirection.Input, secondaryIssueId);
            AddParamToSQLCmd(sqlCmd, "@relationType", SqlDbType.Int, 0, ParameterDirection.Input, IssueRelationType.ParentChild);

            SetCommandType(sqlCmd,CommandType.StoredProcedure, SP_RELATEDISSUE_CREATENEWCHILDISSUE);
            ExecuteScalarCmd(sqlCmd);
            return ((int)sqlCmd.Parameters["@ReturnValue"].Value);
        }



        public override bool DeleteChildIssue (int primaryIssueId, int secondaryIssueId) {
            // Validate Parameters
            if (primaryIssueId <= DefaultValues.GetIssueIdMinValue())
                throw (new ArgumentOutOfRangeException("primaryIssueId"));

            if (secondaryIssueId <= DefaultValues.GetIssueIdMinValue())
                throw (new ArgumentOutOfRangeException("secondaryIssueId"));

            // Execute SQL Command
            SqlCommand sqlCmd = new SqlCommand();

            AddParamToSQLCmd(sqlCmd, "@ReturnValue", SqlDbType.Int, 0, ParameterDirection.ReturnValue, null);
            AddParamToSQLCmd(sqlCmd, "@primaryIssueId", SqlDbType.Int, 0, ParameterDirection.Input, primaryIssueId);
            AddParamToSQLCmd(sqlCmd, "@secondaryIssueId", SqlDbType.Int, 0, ParameterDirection.Input, secondaryIssueId);
            AddParamToSQLCmd(sqlCmd, "@relationType", SqlDbType.Int, 0, ParameterDirection.Input, IssueRelationType.ParentChild);

            SetCommandType(sqlCmd,CommandType.StoredProcedure, SP_RELATEDISSUE_DELETECHILDISSUE);
            ExecuteScalarCmd(sqlCmd);
            int returnValue = (int)sqlCmd.Parameters["@ReturnValue"].Value;
            return (returnValue == 0 ? true : false);
        }



        public override int CreateNewParentIssue (int primaryIssueId, int secondaryIssueId) {
            // Validate Parameters
            if (primaryIssueId <= DefaultValues.GetIssueIdMinValue())
                throw (new ArgumentOutOfRangeException("primaryIssueId"));

            if (secondaryIssueId <= DefaultValues.GetIssueIdMinValue())
                throw (new ArgumentOutOfRangeException("secondaryIssueId"));

            // Execute SQL Command
            SqlCommand sqlCmd = new SqlCommand();

            AddParamToSQLCmd(sqlCmd, "@ReturnValue", SqlDbType.Int, 0, ParameterDirection.ReturnValue, null);
            AddParamToSQLCmd(sqlCmd, "@primaryIssueId", SqlDbType.Int, 0, ParameterDirection.Input, primaryIssueId);
            AddParamToSQLCmd(sqlCmd, "@secondaryIssueId", SqlDbType.Int, 0, ParameterDirection.Input, secondaryIssueId);
            AddParamToSQLCmd(sqlCmd, "@relationType", SqlDbType.Int, 0, ParameterDirection.Input, IssueRelationType.ParentChild);

            SetCommandType(sqlCmd,CommandType.StoredProcedure, SP_RELATEDISSUE_CREATENEWPARENTISSUE);
            ExecuteScalarCmd(sqlCmd);
            return ((int)sqlCmd.Parameters["@ReturnValue"].Value);
        }


        public override bool DeleteParentIssue (int primaryIssueId, int secondaryIssueId) {
            // Validate Parameters
            if (primaryIssueId <= DefaultValues.GetIssueIdMinValue())
                throw (new ArgumentOutOfRangeException("primaryIssueId"));

            if (secondaryIssueId <= DefaultValues.GetIssueIdMinValue())
                throw (new ArgumentOutOfRangeException("secondaryIssueId"));

            // Execute SQL Command
            SqlCommand sqlCmd = new SqlCommand();

            AddParamToSQLCmd(sqlCmd, "@ReturnValue", SqlDbType.Int, 0, ParameterDirection.ReturnValue, null);
            AddParamToSQLCmd(sqlCmd, "@primaryIssueId", SqlDbType.Int, 0, ParameterDirection.Input, primaryIssueId);
            AddParamToSQLCmd(sqlCmd, "@secondaryIssueId", SqlDbType.Int, 0, ParameterDirection.Input, secondaryIssueId);
            AddParamToSQLCmd(sqlCmd, "@relationType", SqlDbType.Int, 0, ParameterDirection.Input, IssueRelationType.ParentChild);

            SetCommandType(sqlCmd,CommandType.StoredProcedure, SP_RELATEDISSUE_DELETEPARENTISSUE);
            ExecuteScalarCmd(sqlCmd);
            int returnValue = (int)sqlCmd.Parameters["@ReturnValue"].Value;
            return (returnValue == 0 ? true : false);
        }



        public override RelatedIssueCollection GetRelatedIssues(int issueId) {
            // Validate Parameters
            if (issueId <= DefaultValues.GetIssueIdMinValue())
                throw (new ArgumentOutOfRangeException("issueId"));

            // Execute SQL Command
            SqlCommand sqlCmd = new SqlCommand();

            AddParamToSQLCmd(sqlCmd, "@IssueId", SqlDbType.Int, 0, ParameterDirection.Input, issueId);
            AddParamToSQLCmd(sqlCmd, "@RelationType", SqlDbType.Int, 0, ParameterDirection.Input, IssueRelationType.Related);

            SetCommandType(sqlCmd,CommandType.StoredProcedure, SP_RELATEDISSUE_GETRELATEDISSUES);
            GenerateCollectionFromReader sqlData = new GenerateCollectionFromReader (GenerateRelatedIssueCollectionFromReader);
            RelatedIssueCollection issCollection = (RelatedIssueCollection) ExecuteReaderCmd(sqlCmd, sqlData);
            return (issCollection);
        }


        public override int CreateNewRelatedIssue (int primaryIssueId, int secondaryIssueId) {
            // Validate Parameters
            if (primaryIssueId <= DefaultValues.GetIssueIdMinValue())
                throw (new ArgumentOutOfRangeException("primaryIssueId"));

            if (secondaryIssueId <= DefaultValues.GetIssueIdMinValue())
                throw (new ArgumentOutOfRangeException("secondaryIssueId"));

            // Execute SQL Command
            SqlCommand sqlCmd = new SqlCommand();

            AddParamToSQLCmd(sqlCmd, "@ReturnValue", SqlDbType.Int, 0, ParameterDirection.ReturnValue, null);
            AddParamToSQLCmd(sqlCmd, "@primaryIssueId", SqlDbType.Int, 0, ParameterDirection.Input, primaryIssueId);
            AddParamToSQLCmd(sqlCmd, "@secondaryIssueId", SqlDbType.Int, 0, ParameterDirection.Input, secondaryIssueId);
            AddParamToSQLCmd(sqlCmd, "@relationType", SqlDbType.Int, 0, ParameterDirection.Input, IssueRelationType.Related);

            SetCommandType(sqlCmd,CommandType.StoredProcedure, SP_RELATEDISSUE_CREATENEWRELATEDISSUE);
            ExecuteScalarCmd(sqlCmd);
            return ((int)sqlCmd.Parameters["@ReturnValue"].Value);
        }


        public override bool DeleteRelatedIssue (int primaryIssueId, int secondaryIssueId) {
            // Validate Parameters
            if (primaryIssueId <= DefaultValues.GetIssueIdMinValue())
                throw (new ArgumentOutOfRangeException("primaryIssueId"));

            if (secondaryIssueId <= DefaultValues.GetIssueIdMinValue())
                throw (new ArgumentOutOfRangeException("secondaryIssueId"));

            // Execute SQL Command
            SqlCommand sqlCmd = new SqlCommand();

            AddParamToSQLCmd(sqlCmd, "@ReturnValue", SqlDbType.Int, 0, ParameterDirection.ReturnValue, null);
            AddParamToSQLCmd(sqlCmd, "@PrimaryIssueId", SqlDbType.Int, 0, ParameterDirection.Input, primaryIssueId);
            AddParamToSQLCmd(sqlCmd, "@SecondaryIssueId", SqlDbType.Int, 0, ParameterDirection.Input, secondaryIssueId);
            AddParamToSQLCmd(sqlCmd, "@relationType", SqlDbType.Int, 0, ParameterDirection.Input, IssueRelationType.Related);

            SetCommandType(sqlCmd,CommandType.StoredProcedure, SP_RELATEDISSUE_DELETERELATEDISSUE);
            ExecuteScalarCmd(sqlCmd);
            int returnValue = (int)sqlCmd.Parameters["@ReturnValue"].Value;
            return (returnValue == 0 ? true : false);
        }



        public override Issue GetIssueById(int issueId) {
            // validate Parameters
            if (issueId <= DefaultValues.GetIssueIdMinValue() )
                throw (new ArgumentOutOfRangeException("issueId"));

            // Execute SQL Command
            SqlCommand sqlCmd = new SqlCommand();

            AddParamToSQLCmd(sqlCmd, "@IssueId", SqlDbType.Int, 0, ParameterDirection.Input, issueId);

            SetCommandType( sqlCmd, CommandType.StoredProcedure, SP_ISSUE_GETISSUEBYID);
            GenerateCollectionFromReader test = new GenerateCollectionFromReader (GenerateIssueCollectionFromReader);
            IssueCollection issCollection =(IssueCollection) ExecuteReaderCmd(sqlCmd, test);
            if (issCollection.Count > 0)
                return issCollection[0];
            else
                return null;
        }


        public override IssueCollection GetIssuesByProjectId (int projectId) {
            // Validate Parameters
            if (projectId <= DefaultValues.GetProjectIdMinValue() )
                throw (new ArgumentOutOfRangeException("projectId"));

            // Execute SQL Command
            SqlCommand sqlCmd = new SqlCommand();

            AddParamToSQLCmd(sqlCmd, "@ProjectId", SqlDbType.Int, 0, ParameterDirection.Input, projectId);

            SetCommandType(sqlCmd,CommandType.StoredProcedure, SP_ISSUE_GETISSUESBYPROJECTID);
            GenerateCollectionFromReader sqlData = new GenerateCollectionFromReader (GenerateIssueCollectionFromReader);
            IssueCollection issCollection = (IssueCollection) ExecuteReaderCmd(sqlCmd, sqlData);
            return (issCollection);
        }





	    //*********************************************************************
	    //
	    // Issue Milestone Methods
	    //
	    // The following methods are used for retrieving milestones
	    // associated with an issue.
	    //
	    //*********************************************************************


        public override  int CreateNewMilestone(Milestone newMileStone) {
            // Validate Parameters
            if (newMileStone== null)
                throw (new ArgumentNullException("newMileStone"));

            // Execute SQL Command
            SqlCommand sqlCmd = new SqlCommand();

            AddParamToSQLCmd(sqlCmd, "@ReturnValue", SqlDbType.Int, 0, ParameterDirection.ReturnValue, null);
            AddParamToSQLCmd(sqlCmd, "@ProjectId", SqlDbType.Int, 0, ParameterDirection.Input, newMileStone.ProjectId);
            AddParamToSQLCmd(sqlCmd, "@MilestoneName", SqlDbType.NText, 50, ParameterDirection.Input, newMileStone.Name);
            AddParamToSQLCmd(sqlCmd, "@MilestoneImageUrl", SqlDbType.NText, 255, ParameterDirection.Input, newMileStone.ImageUrl);

            SetCommandType(sqlCmd,CommandType.StoredProcedure, SP_MILESTONE_CREATE);
            ExecuteScalarCmd(sqlCmd);
            return ((int)sqlCmd.Parameters["@ReturnValue"].Value);
        }


        public override  bool DeleteMilestone(int milestoneId) {
            // Validate Parameters
            if (milestoneId <= DefaultValues.GetMilestoneIdMinValue() )
                throw (new ArgumentOutOfRangeException("milestoneId"));

            // Execute SQL Command
            SqlCommand sqlCmd = new SqlCommand();

            AddParamToSQLCmd(sqlCmd, "@MilestoneIdToDelete", SqlDbType.Int, 4, ParameterDirection.Input, milestoneId);
            AddParamToSQLCmd(sqlCmd, "@ResultValue", SqlDbType.Int, 0, ParameterDirection.ReturnValue, null);

            SetCommandType( sqlCmd, CommandType.StoredProcedure, SP_MILESTONE_DELETE);
            ExecuteScalarCmd(sqlCmd);
            int resultValue = (int)sqlCmd.Parameters["@ResultValue"].Value;
            return (resultValue == 0 ? true : false);
        }



        public override  MilestoneCollection GetMilestoneByProjectId (int projectId) {
            // Validate Parameters
            if (projectId <= DefaultValues.GetMilestoneIdMinValue() )
                throw (new ArgumentOutOfRangeException("projectId"));

            // Execute SQL Command
            SqlCommand sqlCmd = new SqlCommand();

            AddParamToSQLCmd(sqlCmd, "@ProjectId", SqlDbType.Int, 0, ParameterDirection.Input, projectId);

            SetCommandType(sqlCmd,CommandType.StoredProcedure, SP_MILESTONE_GETMILESTONEBYPROJECTID);
            GenerateCollectionFromReader sqlData = new GenerateCollectionFromReader (GenerateMilestoneCollectionFromReader);
            MilestoneCollection stsCollection = (MilestoneCollection) ExecuteReaderCmd(sqlCmd, sqlData);
            return (stsCollection);
        }





	    //*********************************************************************
	    //
	    // Issue Priority Methods
	    //
	    // The following methods are used for retrieving priorities
	    // associated with an issue.
	    //
	    //*********************************************************************



        public override int  CreateNewPriority   (Priority newPriority) {
            // Validate Parameters
            if (newPriority== null)
                throw (new ArgumentNullException("newPriority"));

            // Execute SQL Command
            SqlCommand sqlCmd = new SqlCommand();

            AddParamToSQLCmd(sqlCmd, "@ReturnValue", SqlDbType.Int, 0, ParameterDirection.ReturnValue, null);
            AddParamToSQLCmd(sqlCmd, "@ProjectId", SqlDbType.Int, 0, ParameterDirection.Input, newPriority.ProjectId);
            AddParamToSQLCmd(sqlCmd, "@PriorityName", SqlDbType.NText, 50, ParameterDirection.Input, newPriority.Name);
            AddParamToSQLCmd(sqlCmd, "@PriorityImageUrl", SqlDbType.NText, 255, ParameterDirection.Input, newPriority.ImageUrl);

            SetCommandType(sqlCmd,CommandType.StoredProcedure, SP_PRIORITY_CREATE);
            ExecuteScalarCmd(sqlCmd);
            return ((int)sqlCmd.Parameters["@ReturnValue"].Value);
        }


        public override bool    DeletePriority (int PriorityId) {
            // Validate Parameters
            if (PriorityId <= DefaultValues.GetPriorityIdMinValue() )
                throw (new ArgumentOutOfRangeException("PriorityId"));

            // Execute SQL Command
            SqlCommand sqlCmd = new SqlCommand();

            AddParamToSQLCmd(sqlCmd, "@ReturnValue", SqlDbType.Int, 0, ParameterDirection.ReturnValue, null);
            AddParamToSQLCmd(sqlCmd, "@PriorityIdToDelete", SqlDbType.Int, 0, ParameterDirection.Input, PriorityId);

            SetCommandType( sqlCmd, CommandType.StoredProcedure, SP_PRIORITY_DELETE);
            ExecuteScalarCmd(sqlCmd);
            int returnValue = (int)sqlCmd.Parameters["@ReturnValue"].Value;
            return (returnValue == 0 ? true : false);
        }


        public override PriorityCollection GetPrioritiesByProjectId(int projectId) {
            // Validate Parameters
            if (projectId <= DefaultValues.GetProjectIdMinValue() )
                throw (new ArgumentOutOfRangeException("projectId"));

            // Execute SQL Command
            SqlCommand sqlCmd = new SqlCommand();

            AddParamToSQLCmd(sqlCmd, "@ProjectId", SqlDbType.Int, 0, ParameterDirection.Input, projectId);

            SetCommandType(sqlCmd,CommandType.StoredProcedure, SP_PRIORITY_GETPRIORITIESBYPROJECTID);
            GenerateCollectionFromReader sqlData = new GenerateCollectionFromReader (GeneratePriorityCollectionFromReader);
            PriorityCollection stsCollection = (PriorityCollection) ExecuteReaderCmd(sqlCmd, sqlData);
            return (stsCollection);
        }



	    //*********************************************************************
	    //
	    // Project Methods
	    //
	    // The following methods are used for working with projects.
	    //
	    //*********************************************************************

        public override int CreateNewProject(Project newProject) {
            // Validate Parameters
            if (newProject == null)
                throw (new ArgumentNullException("newProject"));

            // Execute SQL Command
            SqlCommand sqlCmd = new SqlCommand();

            AddParamToSQLCmd(sqlCmd, "@ReturnValue"         ,SqlDbType.Int, 0, ParameterDirection.ReturnValue, null);
            AddParamToSQLCmd(sqlCmd, "@ProjectName"         ,SqlDbType.NText, 255, ParameterDirection.Input, newProject.Name);
            AddParamToSQLCmd(sqlCmd, "@ProjectDescription"  ,SqlDbType.NText, 1000, ParameterDirection.Input, newProject.Description);
            AddParamToSQLCmd(sqlCmd, "@ProjectManagerId"           ,SqlDbType.Int, 0, ParameterDirection.Input, newProject.ManagerId);
            AddParamToSQLCmd(sqlCmd, "@ProjectCreatorUserName"     ,SqlDbType.NText, 255, ParameterDirection.Input, newProject.CreatorUserName);

            SetCommandType(sqlCmd,CommandType.StoredProcedure, SP_PROJECT_CREATE);
            ExecuteScalarCmd(sqlCmd);
            return ((int)sqlCmd.Parameters["@ReturnValue"].Value);
        }


        public override bool DeleteProject(int projectID) {
            // Validate Parameters
            if (projectID <= DefaultValues.GetProjectIdMinValue() )
                throw (new ArgumentOutOfRangeException("projectID"));

            // Execute SQL Command
            SqlCommand sqlCmd = new SqlCommand();

            AddParamToSQLCmd(sqlCmd, "@ReturnValue", SqlDbType.Int, 0, ParameterDirection.ReturnValue, null);
            AddParamToSQLCmd(sqlCmd, "@ProjectIdToDelete", SqlDbType.Int, 0, ParameterDirection.Input, projectID);

            SetCommandType( sqlCmd, CommandType.StoredProcedure, SP_PROJECT_DELETE);
            ExecuteScalarCmd(sqlCmd);
            int returnValue = (int)sqlCmd.Parameters["@ReturnValue"].Value;
            return (returnValue == 0 ? true : false);
        }


        public override ProjectCollection  GetAllProjects() {
            SqlCommand sqlCmd = new SqlCommand();
            SetCommandType( sqlCmd, CommandType.StoredProcedure, SP_PROJECT_GETALLPROJECTS);
            GenerateCollectionFromReader test = new GenerateCollectionFromReader (GenerateProjectCollectionFromReader);
            ProjectCollection prjCollection =(ProjectCollection) ExecuteReaderCmd(sqlCmd, test);
            return prjCollection;
        }




        public override  ProjectCollection GetProjectByMemberUsername (string username) {
            // validate Parameters
            if (username == null || username == String.Empty )
                throw (new ArgumentOutOfRangeException("username"));

            // Execute SQL Command
            SqlCommand sqlCmd = new SqlCommand();

            AddParamToSQLCmd(sqlCmd, "@Username", SqlDbType.NText, 255, ParameterDirection.Input, username);

            SetCommandType( sqlCmd, CommandType.StoredProcedure, SP_PROJECT_GETPROJECTBYMEMBERUSERNAME);
            GenerateCollectionFromReader test = new GenerateCollectionFromReader (GenerateProjectCollectionFromReader);
            ProjectCollection prjCollection =(ProjectCollection) ExecuteReaderCmd(sqlCmd, test);
            return prjCollection;
        }




        public override Project GetProjectById(int  projectId) {
            // validate Parameters
            if (projectId <= DefaultValues.GetProjectIdMinValue() )
                throw (new ArgumentOutOfRangeException("projectId"));

            // Execute SQL Command
            SqlCommand sqlCmd = new SqlCommand();

            AddParamToSQLCmd(sqlCmd, "@ProjectId", SqlDbType.Int, 0, ParameterDirection.Input, projectId);

            SetCommandType( sqlCmd, CommandType.StoredProcedure, SP_PROJECT_GETAPROJECTBYID);
            GenerateCollectionFromReader test = new GenerateCollectionFromReader (GenerateProjectCollectionFromReader);
            ProjectCollection prjCollection =(ProjectCollection) ExecuteReaderCmd(sqlCmd, test);
            if (prjCollection.Count > 0)
                return prjCollection[0];
            else
                return null;
        }




        public override bool UpdateProject (Project projectToUpdate) {
            // validate input
            if (projectToUpdate == null)
                throw (new ArgumentNullException("projectToUpdate"));

            if (projectToUpdate.Id <= 0)
                throw (new ArgumentOutOfRangeException("projectToUpdate"));

            // Execute SQL Command
            SqlCommand sqlCmd = new SqlCommand();

            AddParamToSQLCmd(sqlCmd, "@ReturnValue", SqlDbType.Int, 0, ParameterDirection.ReturnValue, null);
            AddParamToSQLCmd(sqlCmd, "@ProjectId", SqlDbType.Int, 0, ParameterDirection.Input, projectToUpdate.Id);
            AddParamToSQLCmd(sqlCmd, "@ProjectName", SqlDbType.NText, 256, ParameterDirection.Input, projectToUpdate.Name);
            AddParamToSQLCmd(sqlCmd, "@ProjectDescription", SqlDbType.NText, 1000, ParameterDirection.Input, projectToUpdate.Description);
            AddParamToSQLCmd(sqlCmd, "@ProjectManagerId", SqlDbType.Int, 0, ParameterDirection.Input, projectToUpdate.ManagerId);

            SetCommandType(sqlCmd,CommandType.StoredProcedure, SP_PROJECT_UPDATE);
            ExecuteScalarCmd(sqlCmd);
            int returnValue = (int)sqlCmd.Parameters["@ReturnValue"].Value;
            return (returnValue == 0 ? true : false);
        }



        public override bool AddUserToProject(int userId, int projectId) {
            // validate Parameters
            if (userId <= DefaultValues.GetUserIdMinValue() )
                throw (new ArgumentOutOfRangeException("userId"));

            if (projectId <= DefaultValues.GetProjectIdMinValue() )
                throw (new ArgumentOutOfRangeException("projectId"));

            // Execute SQL Command
            SqlCommand sqlCmd = new SqlCommand();

            AddParamToSQLCmd(sqlCmd, "@ReturnValue", SqlDbType.Int, 0, ParameterDirection.ReturnValue, null);
            AddParamToSQLCmd(sqlCmd, "@UserId", SqlDbType.Int, 0, ParameterDirection.Input, userId);
            AddParamToSQLCmd(sqlCmd, "@ProjectId", SqlDbType.Int, 0, ParameterDirection.Input, projectId);

            SetCommandType(sqlCmd,CommandType.StoredProcedure, SP_PROJECT_ADDUSERTOPROJECT);
            ExecuteScalarCmd(sqlCmd);
            int returnValue = (int)sqlCmd.Parameters["@ReturnValue"].Value;
            return (returnValue == 0 ? true : false);
        }



        public override bool RemoveUserFromProject(int userId, int projectId) {
            // validate Parameters
            if (userId <= DefaultValues.GetUserIdMinValue() )
                throw (new ArgumentOutOfRangeException("userId"));

            if (projectId <= DefaultValues.GetProjectIdMinValue() )
                throw (new ArgumentOutOfRangeException("projectId"));

            // Execute SQL Command
            SqlCommand sqlCmd = new SqlCommand();

            AddParamToSQLCmd(sqlCmd, "@ReturnValue", SqlDbType.Int, 0, ParameterDirection.ReturnValue, null);
            AddParamToSQLCmd(sqlCmd, "@UserId", SqlDbType.Int, 0, ParameterDirection.Input, userId);
            AddParamToSQLCmd(sqlCmd, "@ProjectId", SqlDbType.Int, 0, ParameterDirection.Input, projectId);

            SetCommandType(sqlCmd,CommandType.StoredProcedure, SP_PROJECT_REMOVEUSERFROMPROJECT);
            ExecuteScalarCmd(sqlCmd);
            int returnValue = (int)sqlCmd.Parameters["@ReturnValue"].Value;
            return (returnValue == 0 ? true : false);
        }


		public override bool CloneProject(int projectId, string projectName) 
		{
			// validate Parameters
			if (projectId <= DefaultValues.GetProjectIdMinValue() )
				throw (new ArgumentOutOfRangeException("projectId"));

			// Execute SQL Command
			SqlCommand sqlCmd = new SqlCommand();

			AddParamToSQLCmd(sqlCmd, "@ReturnValue", SqlDbType.Int, 0, ParameterDirection.ReturnValue, null);
			AddParamToSQLCmd(sqlCmd, "@ProjectId", SqlDbType.Int, 0, ParameterDirection.Input, projectId);
			AddParamToSQLCmd(sqlCmd, "@ProjectName", SqlDbType.NVarChar, 255, ParameterDirection.Input, projectName);

			SetCommandType(sqlCmd,CommandType.StoredProcedure, SP_PROJECT_CLONEPROJECT);
			ExecuteScalarCmd(sqlCmd);
			int returnValue = (int)sqlCmd.Parameters["@ReturnValue"].Value;
			return (returnValue == 0 ? true : false);
		}






	    //*********************************************************************
	    //
	    // Issue Status Methods
	    //
	    // The following methods are used for working with issue status value.
	    //
	    //*********************************************************************


        public override int CreateNewStatus (Status newStatus) 
		{
            // Validate Parameters
            if (newStatus== null)
                throw (new ArgumentNullException("newStatus"));

            // Execute SQL Command
            SqlCommand sqlCmd = new SqlCommand();

            AddParamToSQLCmd(sqlCmd, "@ReturnValue", SqlDbType.Int, 0, ParameterDirection.ReturnValue, null);
            AddParamToSQLCmd(sqlCmd, "@ProjectId", SqlDbType.Int, 0, ParameterDirection.Input, newStatus.ProjectId);
            AddParamToSQLCmd(sqlCmd, "@StatusName", SqlDbType.NText, 50, ParameterDirection.Input, newStatus.Name);
            AddParamToSQLCmd(sqlCmd, "@StatusImageUrl", SqlDbType.NText, 255, ParameterDirection.Input, newStatus.ImageUrl);

            SetCommandType(sqlCmd,CommandType.StoredProcedure, SP_STATUS_CREATE);
            ExecuteScalarCmd(sqlCmd);
            return ((int)sqlCmd.Parameters["@ReturnValue"].Value);
        }


        public override bool DeleteStatus  (int statusId) {
            // Validate Parameters
            if (statusId <= DefaultValues.GetStatusIdMinValue() )
                throw (new ArgumentOutOfRangeException("statusId"));

            // Execute SQL Command
            SqlCommand sqlCmd = new SqlCommand();

            AddParamToSQLCmd(sqlCmd, "@ReturnValue", SqlDbType.Int, 0, ParameterDirection.ReturnValue, null);
            AddParamToSQLCmd(sqlCmd, "@StatusIdToDelete", SqlDbType.Int, 0, ParameterDirection.Input, statusId);

            SetCommandType( sqlCmd, CommandType.StoredProcedure, SP_STATUS_DELETE);
            ExecuteScalarCmd(sqlCmd);
            int returnValue = (int)sqlCmd.Parameters["@ReturnValue"].Value;
            return (returnValue == 0 ? true : false);
        }



        public override StatusCollection GetStatusByProjectId(int projectId) {
            // validate Parameters
            if (projectId <= DefaultValues.GetProjectIdMinValue() )
                throw (new ArgumentOutOfRangeException("projectId"));

            // Execute SQL Command
            SqlCommand sqlCmd = new SqlCommand();

            AddParamToSQLCmd(sqlCmd, "@ProjectId", SqlDbType.Int, 0, ParameterDirection.Input, projectId);

            SetCommandType(sqlCmd,CommandType.StoredProcedure, SP_STATUS_GETSTATUSBYPROJECTID);
            GenerateCollectionFromReader sqlData = new GenerateCollectionFromReader (GenerateStatusCollectionFromReader);
            StatusCollection stsCollection = (StatusCollection) ExecuteReaderCmd(sqlCmd, sqlData);
            return (stsCollection);
        }




	    //*********************************************************************
	    //
	    // User Methods
	    //
	    // The following methods are used for working with users.
	    //
	    //*********************************************************************



        public override bool Authenticate(string username, string password) {
            // validate Parameters
            if (username == null || username.Length==0 )
                throw (new ArgumentOutOfRangeException("username"));
            if (password == null || password.Length==0 )
                throw (new ArgumentOutOfRangeException("password"));


            // Execute SQL Command
            SqlCommand sqlCmd = new SqlCommand();

            AddParamToSQLCmd(sqlCmd, "@ReturnValue", SqlDbType.Int, 0, ParameterDirection.ReturnValue, null);
            AddParamToSQLCmd(sqlCmd, "@Username", SqlDbType.NText, 255, ParameterDirection.Input, username);
            AddParamToSQLCmd(sqlCmd, "@Password", SqlDbType.NText, 255, ParameterDirection.Input, password);

            SetCommandType(sqlCmd,CommandType.StoredProcedure, SP_USER_AUTHENTICATE);
            ExecuteScalarCmd(sqlCmd);
            int returnValue = (int)sqlCmd.Parameters["@ReturnValue"].Value;
            return (returnValue == 0 ? true : false);
        }



        public override ITUser GetUserByUsername(string username) {
            // validate input
            if (username == null || username.Length==0 )
                throw (new ArgumentOutOfRangeException("username"));

            // Execute SQL Command
            SqlCommand sqlCmd = new SqlCommand();

            AddParamToSQLCmd(sqlCmd, "@ReturnValue", SqlDbType.Int, 0, ParameterDirection.ReturnValue, null);
            AddParamToSQLCmd(sqlCmd, "@Username", SqlDbType.NText, 255, ParameterDirection.Input, username);

            SetCommandType(sqlCmd,CommandType.StoredProcedure, SP_USER_GETUSERBYUSERNAME);
            GenerateCollectionFromReader test = new GenerateCollectionFromReader (GenerateITUserCollectionFromReader);
            ITUserCollection userCollection =(ITUserCollection) ExecuteReaderCmd(sqlCmd, test);
            if (userCollection.Count > 0)
                return userCollection[0];
            else
                return null;
        }



        public override ITUser GetUserById(int userId) {
            // validate Parameters
            if (userId <= DefaultValues.GetUserIdMinValue() )
                throw (new ArgumentOutOfRangeException("userId"));

            // Execute SQL Command
            SqlCommand sqlCmd = new SqlCommand();

            AddParamToSQLCmd(sqlCmd, "@ReturnValue", SqlDbType.Int, 0, ParameterDirection.ReturnValue, null);
            AddParamToSQLCmd(sqlCmd, "@UserId", SqlDbType.Int, 0, ParameterDirection.Input, userId);

            SetCommandType(sqlCmd,CommandType.StoredProcedure, SP_USER_GETUSERBYID);
            GenerateCollectionFromReader test = new GenerateCollectionFromReader (GenerateITUserCollectionFromReader);
            ITUserCollection userCollection =(ITUserCollection) ExecuteReaderCmd(sqlCmd, test);
            if (userCollection.Count > 0)
                return userCollection[0];
            else
                return null;
        }




        public override ITUserCollection GetAllUsers() {
            // Execute SQL Command
            SqlCommand sqlCmd = new SqlCommand();
            SetCommandType(sqlCmd,CommandType.StoredProcedure, SP_USER_GETALLUSERS);
            GenerateCollectionFromReader test = new GenerateCollectionFromReader (GenerateITUserCollectionFromReader);
            ITUserCollection userCollection =(ITUserCollection) ExecuteReaderCmd(sqlCmd, test);
            return userCollection;
        }




        public override ITUserCollection GetAllUsersByRoleName(string roleName) {
            // validate Parameters
            if (roleName == null || roleName.Length==0 )
                throw (new ArgumentOutOfRangeException("password"));

            SqlCommand sqlCmd = new SqlCommand();

            AddParamToSQLCmd(sqlCmd, "@UserId", SqlDbType.NText, 0, ParameterDirection.Input, roleName);

            SetCommandType(sqlCmd,CommandType.StoredProcedure, SP_USER_GETALLUSERSBYROLENAME);
            GenerateCollectionFromReader test = new GenerateCollectionFromReader (GenerateITUserCollectionFromReader);
            ITUserCollection userCollection =(ITUserCollection) ExecuteReaderCmd(sqlCmd, test);
            return userCollection;
        }



        public override ITUserCollection GetUsersByProjectId(int projectId) {
            // Validate Parameters
            if (projectId <= DefaultValues.GetProjectIdMinValue() )
                throw (new ArgumentOutOfRangeException("projectId"));

            // Execute SQL Command
            SqlCommand sqlCmd = new SqlCommand();
            SetCommandType(sqlCmd,CommandType.StoredProcedure, SP_USER_GETUSERSBYPROJECTID);

            AddParamToSQLCmd(sqlCmd, "@ProjectId", SqlDbType.Int, 0, ParameterDirection.Input, projectId);

            GenerateCollectionFromReader test = new GenerateCollectionFromReader (GenerateITUserCollectionFromReader);
            ITUserCollection userCollection =(ITUserCollection) ExecuteReaderCmd(sqlCmd, test);
            return userCollection;
        }





        public override bool UpdateUser (ITUser userToUpdate) {
            // validate input
            if (userToUpdate == null)
                throw (new ArgumentNullException("userToUpdate"));
            if (userToUpdate.Id <= 0)
                throw (new ArgumentOutOfRangeException("userToUpdate"));

            // Execute SQL Command
            SqlCommand sqlCmd = new SqlCommand();

            AddParamToSQLCmd(sqlCmd, "@ReturnValue", SqlDbType.Int, 0, ParameterDirection.ReturnValue, null);
            AddParamToSQLCmd(sqlCmd, "@UserId", SqlDbType.Int, 0, ParameterDirection.Input, userToUpdate.Id);
            AddParamToSQLCmd(sqlCmd, "@Username", SqlDbType.NText, 256, ParameterDirection.Input, userToUpdate.Username);
            AddParamToSQLCmd(sqlCmd, "@RoleName", SqlDbType.NText, 256, ParameterDirection.Input, userToUpdate.RoleName);
            AddParamToSQLCmd(sqlCmd, "@Email"  , SqlDbType.NText, 256, ParameterDirection.Input, userToUpdate.Email);
            AddParamToSQLCmd(sqlCmd, "@DisplayName"  , SqlDbType.NText, 256, ParameterDirection.Input, userToUpdate.DisplayName);
            AddParamToSQLCmd(sqlCmd, "@UserPassword"  , SqlDbType.NText, 256, ParameterDirection.Input, userToUpdate.Password);

            SetCommandType(sqlCmd,CommandType.StoredProcedure, SP_USER_UPDATEUSER);
            ExecuteScalarCmd(sqlCmd);
            int returnValue = (int)sqlCmd.Parameters["@ReturnValue"].Value;
            return (returnValue == 0 ? true : false);
        }


        public override int CreateNewUser(ITUser newUser){
            // Validate Parameters
            if (newUser == null)
                throw (new ArgumentNullException("newUser"));

            // Execute SQL Command
            SqlCommand sqlCmd = new SqlCommand();

            AddParamToSQLCmd(sqlCmd, "@ReturnValue", SqlDbType.Int, 0, ParameterDirection.ReturnValue, null);
            AddParamToSQLCmd(sqlCmd, "@Username"          , SqlDbType.NText, 256, ParameterDirection.Input, newUser.Username);
            AddParamToSQLCmd(sqlCmd, "@RoleName"  , SqlDbType.NText, 256, ParameterDirection.Input, newUser.RoleName);
            AddParamToSQLCmd(sqlCmd, "@Email"  , SqlDbType.NText, 256, ParameterDirection.Input, newUser.Email);
            AddParamToSQLCmd(sqlCmd, "@DisplayName"  , SqlDbType.NText, 256, ParameterDirection.Input, newUser.DisplayName);
            AddParamToSQLCmd(sqlCmd, "@UserPassword"  , SqlDbType.NText, 256, ParameterDirection.Input, newUser.Password);

            SetCommandType(sqlCmd,CommandType.StoredProcedure, SP_USER_CREATENEWUSER);
            ExecuteScalarCmd(sqlCmd);
            return ((int)sqlCmd.Parameters["@ReturnValue"].Value);
        }




	    //*********************************************************************
	    //
	    // Role Methods
	    //
	    // The following methods are used for working with roles.
	    //
	    //*********************************************************************

        public override RoleCollection GetAllRoles() {
            SqlCommand sqlCmd = new SqlCommand();
            SetCommandType(sqlCmd,CommandType.StoredProcedure, SP_ROLE_GETALLROLES);
            GenerateCollectionFromReader test = new GenerateCollectionFromReader (GenerateRoleCollectionFromReader);
            RoleCollection roleCollection =(RoleCollection) ExecuteReaderCmd(sqlCmd, test);
            return roleCollection;
        }



	    //*********************************************************************
	    //
	    // Custom Field Methods
	    //
	    // The following methods are used for working with custom fields.
	    //
	    //*********************************************************************

        public override CustomFieldCollection GetCustomFieldsByProjectId(int projectId) {
            // Validate Parameters
            if (projectId <= DefaultValues.GetProjectIdMinValue() )
                throw (new ArgumentOutOfRangeException("projectId"));

            // Execute SQL Commands
            SqlCommand sqlCmd = new SqlCommand();

            AddParamToSQLCmd(sqlCmd, "@ProjectId", SqlDbType.Int, 0, ParameterDirection.Input, projectId);

            SetCommandType(sqlCmd,CommandType.StoredProcedure, SP_CUSTOMFIELD_GETCUSTOMFIELDSBYPROJECTID);
            GenerateCollectionFromReader sqlData = new GenerateCollectionFromReader (GenerateCustomFieldCollectionFromReader);
            CustomFieldCollection fieldCollection = (CustomFieldCollection) ExecuteReaderCmd(sqlCmd, sqlData);
            return fieldCollection;
        }



        public override CustomFieldCollection GetCustomFieldsByIssueId(int issueId) {
            // Validate Parameters
            if (issueId <= DefaultValues.GetIssueIdMinValue() )
                throw (new ArgumentOutOfRangeException("issueId"));

            // Execute SQL Command
            SqlCommand sqlCmd = new SqlCommand();

            AddParamToSQLCmd(sqlCmd, "@issueId", SqlDbType.Int, 0, ParameterDirection.Input, issueId);

            SetCommandType(sqlCmd,CommandType.StoredProcedure, SP_CUSTOMFIELD_GETCUSTOMFIELDSBYISSUEID);
            GenerateCollectionFromReader sqlData = new GenerateCollectionFromReader (GenerateCustomFieldCollectionFromReader);
            CustomFieldCollection fieldCollection = (CustomFieldCollection) ExecuteReaderCmd(sqlCmd, sqlData);
            return fieldCollection;
        }




        public override int CreateNewCustomField(CustomField newCustomField) {
            // Validate Parameters
            if (newCustomField== null)
                throw (new ArgumentNullException("newCustomField"));

            // Execute SQL Command
            SqlCommand sqlCmd = new SqlCommand();

            AddParamToSQLCmd(sqlCmd, "@ReturnValue", SqlDbType.Int, 0, ParameterDirection.ReturnValue, null);
            AddParamToSQLCmd(sqlCmd, "@ProjectId", SqlDbType.Int, 0, ParameterDirection.Input, newCustomField.ProjectId);
            AddParamToSQLCmd(sqlCmd, "@CustomFieldName", SqlDbType.NText, 50, ParameterDirection.Input, newCustomField.Name);
            AddParamToSQLCmd(sqlCmd, "@CustomFieldDataType", SqlDbType.Int, 0, ParameterDirection.Input, newCustomField.DataType);
            AddParamToSQLCmd(sqlCmd, "@CustomFieldRequired", SqlDbType.Bit, 0, ParameterDirection.Input, newCustomField.Required);

            SetCommandType(sqlCmd,CommandType.StoredProcedure, SP_CUSTOMFIELD_CREATE);
            ExecuteScalarCmd(sqlCmd);
            return ((int)sqlCmd.Parameters["@ReturnValue"].Value);
        }



        public override bool UpdateCustomField(CustomField customFieldToUpdate) {
            // Validate Parameters
            if (customFieldToUpdate == null)
                throw (new ArgumentNullException("customFieldToUpdate"));

            if (customFieldToUpdate.Id <= 0)
                throw (new ArgumentOutOfRangeException("customFieldToUpdate.Id"));

            if (customFieldToUpdate.Name == null || customFieldToUpdate.Name.Length==0 )
                throw (new ArgumentOutOfRangeException("customFieldToUpdate.Name"));

            // Execute SQL Command
            SqlCommand sqlCmd = new SqlCommand();

            AddParamToSQLCmd(sqlCmd, "@ResultValue", SqlDbType.Int, 0, ParameterDirection.ReturnValue, null);
            AddParamToSQLCmd(sqlCmd, "@CustomFieldId", SqlDbType.Int, 0, ParameterDirection.Input, customFieldToUpdate.Id);
            AddParamToSQLCmd(sqlCmd, "@CustomFieldName", SqlDbType.NText, 50, ParameterDirection.Input, customFieldToUpdate.Name);
            AddParamToSQLCmd(sqlCmd, "@CustomFieldDataType", SqlDbType.Int, 0, ParameterDirection.Input, customFieldToUpdate.DataType);
            AddParamToSQLCmd(sqlCmd, "@CustomFieldRequired", SqlDbType.Bit, 0, ParameterDirection.Input, customFieldToUpdate.Required);

            SetCommandType(sqlCmd,CommandType.StoredProcedure, SP_CUSTOMFIELD_UPDATE);
            ExecuteScalarCmd(sqlCmd);
            int returnValue = (int)sqlCmd.Parameters["@ReturnValue"].Value;
            return (returnValue == 0 ? true : false);
        }



        public override bool DeleteCustomField(int customFieldId) {
            // Validate Parameters
            if (customFieldId <= DefaultValues.GetCustomFieldIdMinValue() )
                throw (new ArgumentOutOfRangeException("customFieldId"));

            // Execute SQL Command
            SqlCommand sqlCmd = new SqlCommand();

            AddParamToSQLCmd(sqlCmd, "@ReturnValue", SqlDbType.Int, 0, ParameterDirection.ReturnValue, null);
            AddParamToSQLCmd(sqlCmd, "@CustomIdToDelete", SqlDbType.Int, 0, ParameterDirection.Input, customFieldId);

            SetCommandType( sqlCmd, CommandType.StoredProcedure, SP_CUSTOMFIELD_DELETE);
            ExecuteScalarCmd(sqlCmd);
            int returnValue = (int)sqlCmd.Parameters["@ReturnValue"].Value;
            return (returnValue == 0 ? true : false);
        }



        public override bool SaveCustomFieldValues(int issueId, CustomFieldCollection fields) {
            // Validate Parameters
            if (fields == null)
                throw (new ArgumentNullException("fields"));

            if (issueId <= DefaultValues.GetIssueIdMinValue() )
                throw (new ArgumentOutOfRangeException("issueId"));

            if (ConnectionString == string.Empty)
                throw (new ArgumentOutOfRangeException("ConnectionString"));

            // Execute SQL Commands
            SqlCommand sqlCmd = new SqlCommand();

            sqlCmd.Parameters.Add( "@ReturnValue", SqlDbType.Int, 0).Direction = ParameterDirection.ReturnValue;
            sqlCmd.Parameters.Add( "@IssueId", SqlDbType.Int, 0).Direction = ParameterDirection.Input;
            sqlCmd.Parameters.Add( "@CustomFieldId", SqlDbType.Int, 0).Direction = ParameterDirection.Input;
            sqlCmd.Parameters.Add( "@CustomFieldValue", SqlDbType.NVarChar, 255).Direction = ParameterDirection.Input;

            SetCommandType( sqlCmd, CommandType.StoredProcedure, SP_CUSTOMFIELD_SAVECUSTOMFIELDVALUE);

            bool errors = false;

            using (SqlConnection cn = new SqlConnection(this.ConnectionString)) {
				        sqlCmd.Connection = cn;
				        cn.Open();

				        foreach (CustomField field in fields) {
				            sqlCmd.Parameters["@IssueId"].Value = issueId;
				            sqlCmd.Parameters["@CustomFieldId"].Value = field.Id;
				            sqlCmd.Parameters["@CustomFieldValue"].Value = field.Value;
				            sqlCmd.ExecuteScalar();
				            if ((int)sqlCmd.Parameters["@ReturnValue"].Value == 1)
				                errors = true;
				        }
            }

            return !errors;
        }





	    //*********************************************************************
	    //
	    // SQL Helper Methods
	    //
	    // The following utility methods are used to interact with SQL Server.
	    //
	    //*********************************************************************


        private void AddParamToSQLCmd(SqlCommand sqlCmd, string paramId, SqlDbType sqlType, int paramSize, ParameterDirection paramDirection, object paramvalue) {
            // Validate Parameter Properties
            if (sqlCmd== null)
                throw (new ArgumentNullException("sqlCmd"));
            if (paramId == string.Empty)
                throw (new ArgumentOutOfRangeException("paramId"));

            // Add Parameter
            SqlParameter newSqlParam = new SqlParameter();
            newSqlParam.ParameterName= paramId;
            newSqlParam.SqlDbType = sqlType;
            newSqlParam.Direction = paramDirection;

            if (paramSize > 0)
                newSqlParam.Size=paramSize;

            if (paramvalue != null)
                newSqlParam.Value = paramvalue;

            sqlCmd.Parameters.Add (newSqlParam);
        }


        private Object ExecuteScalarCmd(SqlCommand sqlCmd) {
            // Validate Command Properties
            if (ConnectionString == string.Empty)
                throw (new ArgumentOutOfRangeException("ConnectionString"));

            if (sqlCmd== null)
                throw (new ArgumentNullException("sqlCmd"));

            Object result = null;

            using (SqlConnection cn = new SqlConnection(this.ConnectionString)) {
				        sqlCmd.Connection = cn;
				        cn.Open();
				        result = sqlCmd.ExecuteScalar();
            }

            return result;
        }


		private CollectionBase ExecuteReaderCmd(SqlCommand sqlCmd, GenerateCollectionFromReader gcfr) {
			if (ConnectionString == string.Empty)
				throw (new ArgumentOutOfRangeException("ConnectionString"));

			if (sqlCmd== null)
				throw (new ArgumentNullException("sqlCmd"));

			using (SqlConnection cn = new SqlConnection(this.ConnectionString)) {
				sqlCmd.Connection = cn;
                cn.Open();
				CollectionBase temp =gcfr(sqlCmd.ExecuteReader());
                return (temp);
			}
		}



        private void SetCommandType(SqlCommand sqlCmd, CommandType cmdType, string cmdText) {
            sqlCmd.CommandType = cmdType;
            sqlCmd.CommandText = cmdText;
        }



  }
}
