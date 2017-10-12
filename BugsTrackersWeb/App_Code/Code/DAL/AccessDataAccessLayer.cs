using System;
using System.Text;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Specialized;
using System.Web.UI.WebControls;
using BugsTrackers.BusinessLogicLayer;

namespace BugsTrackers.DataAccessLayer {


	//*********************************************************************
	//
	// AccessDataAccessLayer Class
	//
	// The AccessDataAccessLayer contains the data access layer for SimSoftware
	// Access. This class implements the abstract methods in the
	// DataAccessLayerBaseClass class.
	//
	//*********************************************************************


    public class AccessDataAccessLayer:DataAccessLayerBaseClass {



	    //*********************************************************************
	    //
	    // Constants
	    //
	    // Each of the constants below represent the name of an Access
	    // Query. If you need to change the name of any query
	    // used by the Issue Tracker, modify one of the constants.
	    //
	    //*********************************************************************


        private const string SP_CATEGORY_CREATE                     = "IssueTracker_ProjectCategories_CreateNewCategory";
        private const string SP_CATEGORY_DELETE                     = "IssueTracker_ProjectCategories_DeleteCategory";
        private const string SP_CATEGORY_GETCATEGORIESBYPROJECTID   = "IssueTracker_ProjectCategories_GetCategoriesByProjectId";

        private const string SP_ISSUE_CREATE                        = "IssueTracker_Issue_CreateNewIssue";
        private const string SP_ISSUE_DELETE                        = "IssueTracker_Issue_DeleteIssue";
        private const string SP_ISSUE_GETISSUESBYASSIGNEDUSERNAME   = "IssueTracker_Issue_GetIssuesByAssignedUsername";
        private const string SP_ISSUE_GETISSUESBYRELEVANCY          = "IssueTracker_Issue_GetIssuesByRelevancy";
        private const string SP_ISSUE_GETISSUESBYCREATORUSERNAME    = "IssueTracker_Issue_GetIssuesByCreatorUsername";
        private const string SP_ISSUE_GETISSUEBYID                  = "IssueTracker_Issue_GetIssueById";
        private const string SP_ISSUE_GETISSUESBYOWNERUSERNAME      = "IssueTracker_Issue_GetIssuesByOwnerUsername";
        private const string SP_ISSUE_GETISSUESBYPROJECTID           = "IssueTracker_Issue_GetIssuesByProjectId";
        private const string SP_ISSUE_UPDATE                        = "IssueTracker_Issue_UpdateIssue";

        private const string SP_QUERY_GETQUERIESBYUSERNAME          = "IssueTracker_Query_GetQueriesByUsername";
        private const string SP_QUERY_SAVEQUERY                     = "IssueTracker_Query_SaveQuery";
        private const string SP_QUERY_SAVEQUERYCLAUSE               = "IssueTracker_Query_SaveQueryClause";
        private const string SP_QUERY_GETSAVEDQUERY                 = "IssueTracker_Query_GetSavedQuery";
        private const string SP_QUERY_DELETEQUERY                   = "IssueTracker_Query_DeleteQuery";

        private const string SP_RELATEDISSUE_GETCHILDISSUES         = "IssueTracker_RelatedIssue_GetChildIssues";
        private const string SP_RELATEDISSUE_GETPARENTISSUES        = "IssueTracker_RelatedIssue_GetParentIssues";
        private const string SP_RELATEDISSUE_GETRELATEDISSUES       = "IssueTracker_RelatedIssue_GetRelatedIssues";
        private const string SP_RELATEDISSUE_CREATENEWCHILDISSUE    = "IssueTracker_RelatedIssue_CreateNewChildIssue";
        private const string SP_RELATEDISSUE_DELETECHILDISSUE       = "IssueTracker_RelatedIssue_DeleteChildIssue";
        private const string SP_RELATEDISSUE_CREATENEWPARENTISSUE   = "IssueTracker_RelatedIssue_CreateNewParentIssue";
        private const string SP_RELATEDISSUE_DELETEPARENTISSUE      = "IssueTracker_RelatedIssue_DeleteParentIssue";
        private const string SP_RELATEDISSUE_CREATENEWRELATEDISSUE  = "IssueTracker_RelatedIssue_CreateNewRelatedIssue";
        private const string SP_RELATEDISSUE_DELETERELATEDISSUE     = "IssueTracker_RelatedIssue_DeleteRelatedIssue";

        private const string SP_ISSUECOMMENT_CREATE                     = "IssueTracker_IssueComment_CreateNewIssueComment";
        private const string SP_ISSUECOMMENT_GETISSUECOMMENTBYISSUEID   = "IssueTracker_IssueComment_GetIssueCommentsByIssueId";

		private const string SP_ISSUEATTACHMENT_CREATE                     = "IssueTracker_IssueAttachment_CreateNewIssueAttachment";
		private const string SP_ISSUEATTACHMENT_GETISSUEATTACHMENTSBYISSUEID   = "IssueTracker_IssueAttachment_GetIssueAttachmentsByIssueId";
		private const string SP_ISSUEATTACHMENT_GETISSUEATTACHMENTBYID   = "IssueTracker_IssueAttachment_GetIssueAttachmentById";
		private const string SP_ISSUEATTACHMENT_DELETE   = "IssueTracker_IssueAttachment_Delete";

        private const string SP_ISSUEHISTORY_GETISSUEHISTORYBYISSUEID   = "IssueTracker_IssueHistory_GetIssueHistoryByIssueId";
        private const string SP_ISSUEHISTORY_CREATENEWHISTORY           = "IssueTracker_IssueHistory_CreateNewHistory";

        private const string SP_ISSUENOTIFICATION_DELETE                            = "IssueTracker_IssueNotification_DeleteIssueNotification";
        private const string SP_ISSUENOTIFICATION_CREATE                            = "IssueTracker_IssueNotification_CreateNewIssueNotification";
        private const string SP_ISSUENOTIFICATION_EXISTS                            = "IssueTracker_IssueNotification_Exists";
        private const string SP_ISSUENOTIFICATION_GETISSUENOTIFICATIONSBYISSUEID    = "IssueTracker_IssueNotification_GetIssueNotificationsByIssueId";

        private const string SP_MILESTONE_CREATE                    = "IssueTracker_ProjectMilestones_CreateNewMilestone";
        private const string SP_MILESTONE_DELETE                    = "IssueTracker_ProjectMilestones_DeleteMilestone";
        private const string SP_MILESTONE_GETMILESTONEBYPROJECTID   = "IssueTracker_ProjectMilestones_GetMilestonesByProjectId";

        private const string SP_PRIORITY_CREATE                     = "IssueTracker_ProjectPriorities_CreateNewPriority";
        private const string SP_PRIORITY_DELETE                     = "IssueTracker_ProjectPriorities_DeletePriority";
        private const string SP_PRIORITY_GETPRIORITIESBYPROJECTID   = "IssueTracker_ProjectPriorities_GetPrioritiesByProjectId";

        private const string SP_PROJECT_CREATE                      = "IssueTracker_Project_CreateNewProject";
        private const string SP_PROJECT_DELETE                      = "IssueTracker_Project_DeleteProject";
        private const string SP_PROJECT_GETALLPROJECTS              = "IssueTracker_Project_GetAllProjects";
        private const string SP_PROJECT_GETAPROJECTBYID             = "IssueTracker_Project_GetProjectById";
        private const string SP_PROJECT_UPDATE                      = "IssueTracker_Project_UpdateProject";
        private const string SP_PROJECT_ADDUSERTOPROJECT            = "IssueTracker_Project_AddUserToProject";
        private const string SP_PROJECT_REMOVEUSERFROMPROJECT       = "IssueTracker_Project_RemoveUserFromProject";
        private const string SP_PROJECT_GETPROJECTBYMEMBERUSERNAME  = "IssueTracker_Project_GetProjectByUsername";

        private const string SP_STATUS_CREATE               = "IssueTracker_ProjectStatus_CreateNewStatus";
        private const string SP_STATUS_DELETE               = "IssueTracker_ProjectStatus_DeleteStatus";
        private const string SP_STATUS_GETSTATUSBYPROJECTID = "IssueTracker_ProjectStatus_GetStatusByProjectId";

        private const string SP_CUSTOMFIELD_GETCUSTOMFIELDSBYPROJECTID      = "IssueTracker_CustomField_GetCustomFieldsByProjectId";
        private const string SP_CUSTOMFIELD_GETCUSTOMFIELDSBYISSUEID        = "IssueTracker_CustomField_GetCustomFieldsByIssueId";
        private const string SP_CUSTOMFIELD_CREATE                          = "IssueTracker_CustomField_CreateNewCustomField";
        private const string SP_CUSTOMFIELD_EXISTS                          = "IssueTracker_CustomField_Exists";
        private const string SP_CUSTOMFIELD_UPDATE                          = "IssueTracker_CustomField_UpdateCustomField";
        private const string SP_CUSTOMFIELD_DELETE                          = "IssueTracker_CustomField_DeleteCustomField";
        private const string SP_CUSTOMFIELD_UPDATECUSTOMFIELDVALUE            = "IssueTracker_CustomField_UpdateCustomFieldValue";
        private const string SP_CUSTOMFIELD_INSERTCUSTOMFIELDVALUE            = "IssueTracker_CustomField_InsertCustomFieldValue";

        private const string SP_USER_AUTHENTICATE           = "IssueTracker_User_Authenticate";
        private const string SP_USER_GETUSERBYUSERNAME      = "IssueTracker_User_GetUserByUsername";
        private const string SP_USER_GETUSERBYID            = "IssueTracker_User_GetUserById";
        private const string SP_USER_GETALLUSERS            = "IssueTracker_User_GetAllUsers";
        private const string SP_USER_GETALLUSERSBYROLENAME  = "IssueTracker_User_GetAllUsersByRoleName";
        private const string SP_USER_GETUSERSBYPROJECTID    = "IssueTracker_User_GetUsersByProjectId";
        private const string SP_USER_UPDATEUSER             = "IssueTracker_User_UpdateUser";
        private const string SP_USER_CREATENEWUSER          = "IssueTracker_User_CreateNewUser";
        private const string SP_USER_GETUSERID              = "IssueTracker_User_GetUserId";
        private const string SP_USER_EXISTS                 = "IssueTracker_User_Exists";

        private const string SP_ROLE_GETALLROLES   = "IssueTracker_Role_GetAllRoles";


		/*** INSTANCE PROPERTIES ***/

		public override bool SupportsProjectCloning 
		{
			get { return false; }
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
            OleDbCommand sqlCmd = new OleDbCommand();

            AddParamToSQLCmd(sqlCmd, "@CategoryName", OleDbType.VarWChar, 256, ParameterDirection.Input, newCategory.Name);
            AddParamToSQLCmd(sqlCmd, "@ProjectId", OleDbType.Integer, 4, ParameterDirection.Input, newCategory.ProjectId);
            AddParamToSQLCmd(sqlCmd, "@ParentCategoryId", OleDbType.Integer, 4, ParameterDirection.Input, newCategory.ParentCategoryId);

            SetCommandType(sqlCmd,CommandType.StoredProcedure, SP_CATEGORY_CREATE);
            return ExecuteScalarCmdWithIdentity(sqlCmd);
        }


        public override bool DeleteCategory(int categoryId) {
            // Validate Parameters
            if (categoryId <= DefaultValues.GetCategoryIdMinValue())
                throw (new ArgumentOutOfRangeException("categoryId"));

            // Execute SQL Command
            OleDbCommand sqlCmd = new OleDbCommand();

            AddParamToSQLCmd(sqlCmd, "@CategoryIdToDelete", OleDbType.Integer, 4, ParameterDirection.Input, categoryId);

            SetCommandType( sqlCmd, CommandType.StoredProcedure, SP_CATEGORY_DELETE);
            ExecuteScalarCmd(sqlCmd);
            return true;
        }




        public override CategoryCollection GetCategoriesByProjectId(int projectId) {
            // Validate Parameters
            if (projectId <= DefaultValues.GetProjectIdMinValue())
                throw (new ArgumentOutOfRangeException("projectId"));

            // Execute SQL Command
            OleDbCommand sqlCmd = new OleDbCommand();

            AddParamToSQLCmd(sqlCmd, "@ProjectId", OleDbType.Integer, 0, ParameterDirection.Input, projectId);

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
            OleDbCommand sqlCmd = new OleDbCommand();

            AddParamToSQLCmd(sqlCmd, "@IssueId", OleDbType.Integer, 0, ParameterDirection.Input, newComment.IssueId);
            AddParamToSQLCmd(sqlCmd, "@Comment", OleDbType.LongVarWChar, newComment.Comment.Length, ParameterDirection.Input, newComment.Comment);
            AddParamToSQLCmd(sqlCmd, "@CreatorUserName", OleDbType.VarWChar, 256, ParameterDirection.Input, newComment.CreatorUsername);

            SetCommandType( sqlCmd, CommandType.StoredProcedure, SP_ISSUECOMMENT_CREATE);
            int newCommentId = ExecuteScalarCmdWithIdentity(sqlCmd);


            // Update Issue History
            CreateNewHistory(newComment.IssueId, newComment.CreatorUsername);

            return newCommentId;
        }


        public override IssueCommentCollection GetIssueCommentsByIssueId  (int issueId) {
            // Validate Parameters
            if (issueId <= DefaultValues.GetIssueCommentIdMinValue() )
                throw (new ArgumentOutOfRangeException("issueId"));

            // Execute SQL Command
            OleDbCommand sqlCmd = new OleDbCommand();

            AddParamToSQLCmd(sqlCmd, "@IssueId", OleDbType.Integer, 0, ParameterDirection.Input, issueId);

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
			OleDbCommand sqlCmd = new OleDbCommand();

			AddParamToSQLCmd(sqlCmd, "@IssueId", OleDbType.Integer, 0, ParameterDirection.Input, newAttachment.IssueId);
			AddParamToSQLCmd(sqlCmd, "@FileName", OleDbType.VarWChar, 250, ParameterDirection.Input, newAttachment.FileName);
			AddParamToSQLCmd(sqlCmd, "@ContentType", OleDbType.VarWChar, 50, ParameterDirection.Input, newAttachment.ContentType);
			sqlCmd.Parameters.Add("@Attachment", OleDbType.LongVarBinary, newAttachment.Attachment.Length);
			sqlCmd.Parameters["@Attachment"].Value = newAttachment.Attachment;
			AddParamToSQLCmd(sqlCmd, "@CreatorUserName", OleDbType.VarWChar, 256, ParameterDirection.Input, newAttachment.CreatorUsername);

			SetCommandType( sqlCmd, CommandType.StoredProcedure, SP_ISSUEATTACHMENT_CREATE);
			int newAttachmentId = ExecuteScalarCmdWithIdentity(sqlCmd);

			// Update Issue History
			CreateNewHistory(newAttachment.IssueId, newAttachment.CreatorUsername);

			return newAttachmentId;
		}



		public override IssueAttachmentCollection GetIssueAttachmentsByIssueId  (int issueId) 
		{
			// Validate Parameters
			if (issueId <= DefaultValues.GetIssueCommentIdMinValue() )
				throw (new ArgumentOutOfRangeException("issueId"));

			// Execute SQL Command
			OleDbCommand sqlCmd = new OleDbCommand();

			AddParamToSQLCmd(sqlCmd, "@IssueId", OleDbType.Integer, 0, ParameterDirection.Input, issueId);

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
			OleDbCommand sqlCmd = new OleDbCommand();
			AddParamToSQLCmd(sqlCmd, "@AttachmentId", OleDbType.Integer, 0, ParameterDirection.Input, attachmentId);
			SetCommandType( sqlCmd, CommandType.StoredProcedure, SP_ISSUEATTACHMENT_GETISSUEATTACHMENTBYID);

			// Execute Reader
			if (ConnectionString == string.Empty)
				throw (new ArgumentOutOfRangeException("ConnectionString"));


			using (OleDbConnection cn = new OleDbConnection(this.ConnectionString)) 
			{
				sqlCmd.Connection = cn;
				cn.Open();
				OleDbDataReader dtr = sqlCmd.ExecuteReader();
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
			OleDbCommand sqlCmd = new OleDbCommand();

			AddParamToSQLCmd(sqlCmd, "@AttachmentId", OleDbType.Integer, 0, ParameterDirection.Input, attachmentId);

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

        private int CreateNewHistory (int issueId, string creatorUsername) 
		{
            // validate Parameters
            if (issueId <= DefaultValues.GetIssueCommentIdMinValue() )
                throw (new ArgumentOutOfRangeException("issueId"));

            // Execute SQL Command
            OleDbCommand sqlCmd = new OleDbCommand();

            AddParamToSQLCmd(sqlCmd, "@IssueId", OleDbType.Integer, 0, ParameterDirection.Input, issueId);
            AddParamToSQLCmd(sqlCmd, "@CreatorUserName", OleDbType.VarWChar, 256, ParameterDirection.Input, creatorUsername);
			//add following params
			//@IssueOwnerId Int, @IssueAssignedId Int,@IssuePriorityId Int,@IssueStatusId Int
			AddParamToSQLCmd(sqlCmd, "@IssueOwnerId", OleDbType.Integer, 0, ParameterDirection.Input, 2);
			AddParamToSQLCmd(sqlCmd, "@IssueAssignedId", OleDbType.Integer, 0, ParameterDirection.Input, 2);
			AddParamToSQLCmd(sqlCmd, "@IssuePriorityId", OleDbType.Integer, 0, ParameterDirection.Input, 5);
			AddParamToSQLCmd(sqlCmd, "@IssueStatusId", OleDbType.Integer, 0, ParameterDirection.Input, 7);

            SetCommandType( sqlCmd, CommandType.StoredProcedure, SP_ISSUEHISTORY_CREATENEWHISTORY);
            return ExecuteScalarCmdWithIdentity(sqlCmd);
        }


        public override IssueHistoryCollection GetIssueHistoryByIssueId  (int issueId) {
            // Validate Parameters
            if (issueId <= DefaultValues.GetIssueIdMinValue() )
                throw (new ArgumentOutOfRangeException("issueId"));

            // Execute SQL Command
            OleDbCommand sqlCmd = new OleDbCommand();

            AddParamToSQLCmd(sqlCmd, "@IssueId", OleDbType.Integer, 0, ParameterDirection.Input, issueId);

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


            // Check for duplicate notification
            OleDbCommand sqlCmd = new OleDbCommand();

            AddParamToSQLCmd(sqlCmd, "@IssueId", OleDbType.Integer, 0, ParameterDirection.Input, newNotification.IssueId);
            AddParamToSQLCmd(sqlCmd, "@Username", OleDbType.VarWChar, 255, ParameterDirection.Input, newNotification.NotificationUsername);

            SetCommandType( sqlCmd, CommandType.StoredProcedure, SP_ISSUENOTIFICATION_EXISTS);
            if (ExecuteScalarCmd(sqlCmd) != null)
                return 0;


            // Execute SQL Command
            sqlCmd = new OleDbCommand();

            AddParamToSQLCmd(sqlCmd, "@IssueId", OleDbType.Integer, 0, ParameterDirection.Input, newNotification.IssueId);
            AddParamToSQLCmd(sqlCmd, "@Username", OleDbType.VarWChar, 255, ParameterDirection.Input, newNotification.NotificationUsername);

            SetCommandType( sqlCmd, CommandType.StoredProcedure, SP_ISSUENOTIFICATION_CREATE);
            return ExecuteScalarCmdWithIdentity(sqlCmd);
        }


        public override bool DeleteIssueNotification(int issueId, string username) {
            // Validate Parameters
            if (issueId <= DefaultValues.GetIssueIdMinValue() )
                throw (new ArgumentOutOfRangeException("issueId"));


            if (username == String.Empty)
                throw (new ArgumentOutOfRangeException("username"));

            // Execute SQL Command
            OleDbCommand sqlCmd = new OleDbCommand();

            AddParamToSQLCmd(sqlCmd, "@Username", OleDbType.VarWChar, 255, ParameterDirection.Input, username);
            AddParamToSQLCmd(sqlCmd, "@IssueId", OleDbType.Integer, 0, ParameterDirection.Input, issueId);

            SetCommandType( sqlCmd, CommandType.StoredProcedure, SP_ISSUENOTIFICATION_DELETE);
            ExecuteScalarCmd(sqlCmd);
            return true;
        }



        public override IssueNotificationCollection GetIssueNotificationsByIssueId  (int issueId) {
            // Validate Parameters
            if (issueId <= DefaultValues.GetIssueIdMinValue() )
                throw (new ArgumentOutOfRangeException("issueId"));

            // Execute SQL Command
            OleDbCommand sqlCmd = new OleDbCommand();

            AddParamToSQLCmd(sqlCmd, "@IssueId", OleDbType.Integer, 0, ParameterDirection.Input, issueId);

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
	    // the issue database.
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
            OleDbCommand cmdSaveQuery = new OleDbCommand();

            AddParamToSQLCmd(cmdSaveQuery, "@QueryName", OleDbType.VarWChar, 50, ParameterDirection.Input, queryName);
            AddParamToSQLCmd(cmdSaveQuery, "@projectId", OleDbType.Integer, 0, ParameterDirection.Input, projectId);
            AddParamToSQLCmd(cmdSaveQuery, "@Username", OleDbType.VarWChar, 255, ParameterDirection.Input, username);

            SetCommandType(cmdSaveQuery, CommandType.StoredProcedure, SP_QUERY_SAVEQUERY);


            // Create Save Query Clause Command
            OleDbCommand cmdClause = new OleDbCommand();

            cmdClause.Parameters.Add( "@QueryId", OleDbType.Integer);
            cmdClause.Parameters.Add( "@BooleanOperator", OleDbType.VarWChar, 50);
            cmdClause.Parameters.Add( "@FieldName", OleDbType.VarWChar, 50);
            cmdClause.Parameters.Add( "@ComparisonOperator", OleDbType.VarWChar, 50);
            cmdClause.Parameters.Add( "@FieldValue", OleDbType.VarWChar, 50);
            cmdClause.Parameters.Add( "@DataType", OleDbType.Integer);

            SetCommandType(cmdClause, CommandType.StoredProcedure, SP_QUERY_SAVEQUERYCLAUSE);


            // Execute Save Query Command
            int queryId = ExecuteScalarCmdWithIdentity(cmdSaveQuery);
            if (queryId == 0)
                return false;


            // Prepare connection
            using (OleDbConnection cn = new OleDbConnection(this.ConnectionString)) {

                cn.Open();


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
            OleDbCommand sqlCmd = new OleDbCommand();

            AddParamToSQLCmd(sqlCmd, "@queryId", OleDbType.Integer, 0, ParameterDirection.Input, queryId);

            SetCommandType(sqlCmd,CommandType.StoredProcedure, SP_QUERY_DELETEQUERY);
            ExecuteScalarCmd(sqlCmd);
            return true;
        }


        public override QueryCollection GetQueriesByUsername(string username, int projectId) {
            if (username == null || username == String.Empty)
                throw new ArgumentOutOfRangeException("username");

            if (projectId <= DefaultValues.GetProjectIdMinValue() )
                throw (new ArgumentOutOfRangeException("projectId"));

            // Execute SQL Command
            OleDbCommand sqlCmd = new OleDbCommand();

            AddParamToSQLCmd(sqlCmd, "@Username", OleDbType.VarWChar, 255, ParameterDirection.Input, username);
            AddParamToSQLCmd(sqlCmd, "@ProjectId", OleDbType.Integer, 0, ParameterDirection.Input, projectId);

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
            OleDbCommand sqlCmd = new OleDbCommand();

            AddParamToSQLCmd(sqlCmd, "@ProjectId", OleDbType.Integer, 0, ParameterDirection.Input, newIssue.ProjectId);
            AddParamToSQLCmd(sqlCmd, "@IssueTitle", OleDbType.VarWChar, 255, ParameterDirection.Input, newIssue.Title);
            AddParamToSQLCmd(sqlCmd, "@IssueCategoryId", OleDbType.Integer, 0, ParameterDirection.Input, newIssue.CategoryId);
            AddParamToSQLCmd(sqlCmd, "@IssueStatusId", OleDbType.Integer, 0, ParameterDirection.Input, newIssue.StatusId);
            AddParamToSQLCmd(sqlCmd, "@IssuePriorityId", OleDbType.Integer, 0, ParameterDirection.Input, newIssue.PriorityId);
            AddParamToSQLCmd(sqlCmd, "@IssueMilestoneId", OleDbType.Integer, 0, ParameterDirection.Input, newIssue.MilestoneId);
            AddParamToSQLCmd(sqlCmd, "@IssueAssignedId", OleDbType.Integer, 0, ParameterDirection.Input, newIssue.AssignedId);
            AddParamToSQLCmd(sqlCmd, "@IssueOwnerId", OleDbType.Integer, 0, ParameterDirection.Input, newIssue.OwnerId);
            AddParamToSQLCmd(sqlCmd, "@IssueCreatorUsername", OleDbType.VarWChar, 255, ParameterDirection.Input, newIssue.CreatorUsername);


            SetCommandType(sqlCmd,CommandType.StoredProcedure, SP_ISSUE_CREATE);
            int newIssueId = ExecuteScalarCmdWithIdentity(sqlCmd);


            // Update Issue History
            CreateNewHistory(newIssueId, newIssue.CreatorUsername);

            return newIssueId;
        }


        public override bool UpdateIssue(Issue issueToUpdate) {
            // Validate Parameters
            if (issueToUpdate == null)
                throw (new ArgumentNullException("issueToUpdate"));

            // Execute SQL Command
            OleDbCommand sqlCmd = new OleDbCommand();

            AddParamToSQLCmd(sqlCmd, "@ProjectId", OleDbType.Integer, 0, ParameterDirection.Input, issueToUpdate.ProjectId);
            AddParamToSQLCmd(sqlCmd, "@IssueTitle", OleDbType.VarWChar, 255, ParameterDirection.Input, issueToUpdate.Title);
            AddParamToSQLCmd(sqlCmd, "@IssueCategoryId", OleDbType.Integer, 0, ParameterDirection.Input, issueToUpdate.CategoryId);
            AddParamToSQLCmd(sqlCmd, "@IssueStatusId", OleDbType.Integer, 0, ParameterDirection.Input, issueToUpdate.StatusId);
            AddParamToSQLCmd(sqlCmd, "@IssuePriorityId", OleDbType.Integer, 0, ParameterDirection.Input, issueToUpdate.PriorityId);
            AddParamToSQLCmd(sqlCmd, "@IssueMilestoneId", OleDbType.Integer, 0, ParameterDirection.Input, issueToUpdate.MilestoneId);
            AddParamToSQLCmd(sqlCmd, "@IssueAssignedId", OleDbType.Integer, 0, ParameterDirection.Input, issueToUpdate.AssignedId);
            AddParamToSQLCmd(sqlCmd, "@IssueOwnerId", OleDbType.Integer, 0, ParameterDirection.Input, issueToUpdate.OwnerId);
            AddParamToSQLCmd(sqlCmd, "@IssueId", OleDbType.Integer, 0, ParameterDirection.Input, issueToUpdate.Id);

            SetCommandType(sqlCmd,CommandType.StoredProcedure, SP_ISSUE_UPDATE);
            ExecuteScalarCmd(sqlCmd);

            // Update Issue History
            CreateNewHistory(issueToUpdate.Id, issueToUpdate.CreatorUsername);

            return true;
        }



        public override bool DeleteIssue(int issueID) {
            // Validate Parameters
            if (issueID <= DefaultValues.GetIssueIdMinValue())
                throw (new ArgumentOutOfRangeException("issueID"));

            // Execute SQL Command
            OleDbCommand sqlCmd = new OleDbCommand();

            AddParamToSQLCmd(sqlCmd, "@IssueIdToDelete", OleDbType.Integer, 0, ParameterDirection.Input, issueID);

            SetCommandType( sqlCmd, CommandType.StoredProcedure, SP_ISSUE_DELETE);
            ExecuteScalarCmd(sqlCmd);
            return true;
        }







        public override IssueCollection PerformQuery(int projectId, QueryClauseCollection queryClauses) {
            // Validate Parameters

            if (projectId <= DefaultValues.GetProjectIdMinValue())
                throw (new ArgumentOutOfRangeException("projectId"));

            if (queryClauses.Count == 0)
                throw (new ArgumentOutOfRangeException("queryClauses"));



            // Build Command Text
            StringBuilder commandBuilder = new StringBuilder();
            commandBuilder.Append("SELECT * FROM IssueTracker_IssuesView WHERE Disabled=0 AND IssueId IN (SELECT IssueId FROM IssueTracker_Issues WHERE 1=1 ");

            foreach (QueryClause qc in queryClauses)
                commandBuilder.AppendFormat( " {0} {1} {2} ?", qc.BooleanOperator, qc.FieldName, qc.ComparisonOperator);

            commandBuilder.Append( ") AND ProjectId=? ORDER BY IssueId DESC" );


            // Create Command object
            OleDbCommand sqlCmd = new OleDbCommand();
            sqlCmd.CommandText = commandBuilder.ToString();


            // Build Parameter List
            int i = 0;
            foreach (QueryClause qc in queryClauses) {
                sqlCmd.Parameters.Add( "@p" + i.ToString(), ConvertSqlDbType(qc.DataType) ).Value = qc.FieldValue;
                i ++;
            }
            sqlCmd.Parameters.Add("@projectId", OleDbType.Integer).Value = projectId;


            GenerateCollectionFromReader sqlData = new GenerateCollectionFromReader (GenerateIssueCollectionFromReader);
            IssueCollection issCollection = (IssueCollection) ExecuteReaderCmd(sqlCmd, sqlData);
            return (issCollection);
        }


        private OleDbType ConvertSqlDbType(SqlDbType type) {
            switch (type) {
                case SqlDbType.Int:
                    return OleDbType.Integer;
                case SqlDbType.DateTime:
                    return OleDbType.Date;
                default:
                    return OleDbType.VarWChar;
            }

        }



        public override IssueCollection PerformSavedQuery(int projectId, int queryId) {
            // Validate Parameters
            if (projectId <= DefaultValues.GetProjectIdMinValue())
                throw (new ArgumentOutOfRangeException("projectId"));

            if (queryId <= DefaultValues.GetQueryIdMinValue() )
                throw (new ArgumentOutOfRangeException("queryId"));


            // Execute SQL Command
            OleDbCommand sqlCmd = new OleDbCommand();

            AddParamToSQLCmd(sqlCmd, "@QueryId", OleDbType.Integer, 0, ParameterDirection.Input, queryId);


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
            OleDbCommand sqlCmd = new OleDbCommand();

            AddParamToSQLCmd(sqlCmd, "@Username", OleDbType.VarWChar, 255, ParameterDirection.Input, username);

            SetCommandType( sqlCmd, CommandType.StoredProcedure, SP_USER_GETUSERID);
            int userId = (int)ExecuteScalarCmd(sqlCmd);

            // Execute SQL Command
            sqlCmd = new OleDbCommand();

            AddParamToSQLCmd(sqlCmd, "@userId", OleDbType.Integer, 0, ParameterDirection.Input, userId);
            AddParamToSQLCmd(sqlCmd, "@projectId", OleDbType.Integer, 0, ParameterDirection.Input, projectId);

            SetCommandType(sqlCmd,CommandType.StoredProcedure, SP_ISSUE_GETISSUESBYRELEVANCY);
            GenerateCollectionFromReader sqlData = new GenerateCollectionFromReader (GenerateIssueCollectionFromReader);
            IssueCollection issCollection = (IssueCollection) ExecuteReaderCmd(sqlCmd, sqlData);
            return (issCollection);
        }


        public override IssueCollection GetIssuesByAssignedUsername(int projectId, string assignedUsername) {
            // Validate Parameters
            if (projectId <= DefaultValues.GetProjectIdMinValue() )
                throw (new ArgumentOutOfRangeException("projectId"));

            if (assignedUsername == null || assignedUsername == String.Empty)
                throw (new ArgumentOutOfRangeException("assignedUsername"));


            // Execute SQL Command
            OleDbCommand sqlCmd = new OleDbCommand();

            AddParamToSQLCmd(sqlCmd, "@Username", OleDbType.VarWChar, 255, ParameterDirection.Input, assignedUsername);

            SetCommandType( sqlCmd, CommandType.StoredProcedure, SP_USER_GETUSERID);
            int userId = (int)ExecuteScalarCmd(sqlCmd);


            // Execute SQL Command
            sqlCmd = new OleDbCommand();

            AddParamToSQLCmd(sqlCmd, "@ProjectId", OleDbType.Integer, 0, ParameterDirection.Input, projectId);
            AddParamToSQLCmd(sqlCmd, "@userId", OleDbType.Integer, 0, ParameterDirection.Input, userId);

            SetCommandType(sqlCmd,CommandType.StoredProcedure, SP_ISSUE_GETISSUESBYASSIGNEDUSERNAME);
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
            OleDbCommand sqlCmd = new OleDbCommand();

            AddParamToSQLCmd(sqlCmd, "@Username", OleDbType.VarWChar, 255, ParameterDirection.Input, creatorUsername);

            SetCommandType( sqlCmd, CommandType.StoredProcedure, SP_USER_GETUSERID);
            int userId = (int)ExecuteScalarCmd(sqlCmd);


            // Execute SQL Command
            sqlCmd = new OleDbCommand();

            AddParamToSQLCmd(sqlCmd, "@projectId", OleDbType.Integer, 0, ParameterDirection.Input, projectId);
            AddParamToSQLCmd(sqlCmd, "@userId", OleDbType.Integer, 0, ParameterDirection.Input, userId);

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


            // Execute SQL Command
            OleDbCommand sqlCmd = new OleDbCommand();

            AddParamToSQLCmd(sqlCmd, "@Username", OleDbType.VarWChar, 255, ParameterDirection.Input, ownerUsername);

            SetCommandType( sqlCmd, CommandType.StoredProcedure, SP_USER_GETUSERID);
            int userId = (int)ExecuteScalarCmd(sqlCmd);

            // Execute SQL Command
            sqlCmd = new OleDbCommand();

            AddParamToSQLCmd(sqlCmd, "@ProjectId", OleDbType.Integer, 0, ParameterDirection.Input, projectId);
            AddParamToSQLCmd(sqlCmd, "@UserId", OleDbType.Integer, 0, ParameterDirection.Input, userId);

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
            OleDbCommand sqlCmd = new OleDbCommand();

            AddParamToSQLCmd(sqlCmd, "@IssueId", OleDbType.Integer, 0, ParameterDirection.Input, issueId);
            AddParamToSQLCmd(sqlCmd, "@RelationType", OleDbType.Integer, 0, ParameterDirection.Input, IssueRelationType.ParentChild);

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
            OleDbCommand sqlCmd = new OleDbCommand();

            AddParamToSQLCmd(sqlCmd, "@IssueId", OleDbType.Integer, 0, ParameterDirection.Input, issueId);
            AddParamToSQLCmd(sqlCmd, "@RelationType", OleDbType.Integer, 0, ParameterDirection.Input, IssueRelationType.ParentChild);

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
            OleDbCommand sqlCmd = new OleDbCommand();

            AddParamToSQLCmd(sqlCmd, "@primaryIssueId", OleDbType.Integer, 0, ParameterDirection.Input, primaryIssueId);
            AddParamToSQLCmd(sqlCmd, "@secondaryIssueId", OleDbType.Integer, 0, ParameterDirection.Input, secondaryIssueId);
            AddParamToSQLCmd(sqlCmd, "@relationType", OleDbType.Integer, 0, ParameterDirection.Input, IssueRelationType.ParentChild);

            SetCommandType(sqlCmd,CommandType.StoredProcedure, SP_RELATEDISSUE_CREATENEWCHILDISSUE);
            return ExecuteScalarCmdWithIdentity(sqlCmd);
        }



        public override bool DeleteChildIssue (int primaryIssueId, int secondaryIssueId) {
            // Validate Parameters
            if (primaryIssueId <= DefaultValues.GetIssueIdMinValue())
                throw (new ArgumentOutOfRangeException("primaryIssueId"));

            if (secondaryIssueId <= DefaultValues.GetIssueIdMinValue())
                throw (new ArgumentOutOfRangeException("secondaryIssueId"));

            // Execute SQL Command
            OleDbCommand sqlCmd = new OleDbCommand();

            AddParamToSQLCmd(sqlCmd, "@primaryIssueId", OleDbType.Integer, 0, ParameterDirection.Input, primaryIssueId);
            AddParamToSQLCmd(sqlCmd, "@secondaryIssueId", OleDbType.Integer, 0, ParameterDirection.Input, secondaryIssueId);
            AddParamToSQLCmd(sqlCmd, "@relationType", OleDbType.Integer, 0, ParameterDirection.Input, IssueRelationType.ParentChild);

            SetCommandType(sqlCmd,CommandType.StoredProcedure, SP_RELATEDISSUE_DELETECHILDISSUE);
            ExecuteScalarCmd(sqlCmd);
            return true;
        }



        public override int CreateNewParentIssue (int primaryIssueId, int secondaryIssueId) {
            // Validate Parameters
            if (primaryIssueId <= DefaultValues.GetIssueIdMinValue())
                throw (new ArgumentOutOfRangeException("primaryIssueId"));

            if (secondaryIssueId <= DefaultValues.GetIssueIdMinValue())
                throw (new ArgumentOutOfRangeException("secondaryIssueId"));

            // Execute SQL Command
            OleDbCommand sqlCmd = new OleDbCommand();

            AddParamToSQLCmd(sqlCmd, "@secondaryIssueId", OleDbType.Integer, 0, ParameterDirection.Input, secondaryIssueId);
            AddParamToSQLCmd(sqlCmd, "@primaryIssueId", OleDbType.Integer, 0, ParameterDirection.Input, primaryIssueId);
            AddParamToSQLCmd(sqlCmd, "@relationType", OleDbType.Integer, 0, ParameterDirection.Input, IssueRelationType.ParentChild);

            SetCommandType(sqlCmd,CommandType.StoredProcedure, SP_RELATEDISSUE_CREATENEWPARENTISSUE);
            return ExecuteScalarCmdWithIdentity(sqlCmd);
        }


        public override bool DeleteParentIssue (int primaryIssueId, int secondaryIssueId) {
            // Validate Parameters
            if (primaryIssueId <= DefaultValues.GetIssueIdMinValue())
                throw (new ArgumentOutOfRangeException("primaryIssueId"));

            if (secondaryIssueId <= DefaultValues.GetIssueIdMinValue())
                throw (new ArgumentOutOfRangeException("secondaryIssueId"));

            // Execute SQL Command
            OleDbCommand sqlCmd = new OleDbCommand();

            AddParamToSQLCmd(sqlCmd, "@secondaryIssueId", OleDbType.Integer, 0, ParameterDirection.Input, secondaryIssueId);
            AddParamToSQLCmd(sqlCmd, "@primaryIssueId", OleDbType.Integer, 0, ParameterDirection.Input, primaryIssueId);
            AddParamToSQLCmd(sqlCmd, "@relationType", OleDbType.Integer, 0, ParameterDirection.Input, IssueRelationType.ParentChild);

            SetCommandType(sqlCmd,CommandType.StoredProcedure, SP_RELATEDISSUE_DELETEPARENTISSUE);
            ExecuteScalarCmd(sqlCmd);
            return true;
        }



        public override RelatedIssueCollection GetRelatedIssues(int issueId) {
            // Validate Parameters
            if (issueId <= DefaultValues.GetIssueIdMinValue())
                throw (new ArgumentOutOfRangeException("issueId"));

            // Execute SQL Command
            OleDbCommand sqlCmd = new OleDbCommand();

            AddParamToSQLCmd(sqlCmd, "@IssueId", OleDbType.Integer, 0, ParameterDirection.Input, issueId);
            AddParamToSQLCmd(sqlCmd, "@RelationType", OleDbType.Integer, 0, ParameterDirection.Input, IssueRelationType.Related);

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
            OleDbCommand sqlCmd = new OleDbCommand();

            AddParamToSQLCmd(sqlCmd, "@primaryIssueId", OleDbType.Integer, 0, ParameterDirection.Input, primaryIssueId);
            AddParamToSQLCmd(sqlCmd, "@secondaryIssueId", OleDbType.Integer, 0, ParameterDirection.Input, secondaryIssueId);
            AddParamToSQLCmd(sqlCmd, "@relationType", OleDbType.Integer, 0, ParameterDirection.Input, IssueRelationType.Related);

            SetCommandType(sqlCmd,CommandType.StoredProcedure, SP_RELATEDISSUE_CREATENEWRELATEDISSUE);
            return ExecuteScalarCmdWithIdentity(sqlCmd);
        }


        public override bool DeleteRelatedIssue (int primaryIssueId, int secondaryIssueId) {
            // Validate Parameters
            if (primaryIssueId <= DefaultValues.GetIssueIdMinValue())
                throw (new ArgumentOutOfRangeException("primaryIssueId"));

            if (secondaryIssueId <= DefaultValues.GetIssueIdMinValue())
                throw (new ArgumentOutOfRangeException("secondaryIssueId"));

            // Execute SQL Command
            OleDbCommand sqlCmd = new OleDbCommand();

            AddParamToSQLCmd(sqlCmd, "@PrimaryIssueId", OleDbType.Integer, 0, ParameterDirection.Input, primaryIssueId);
            AddParamToSQLCmd(sqlCmd, "@SecondaryIssueId", OleDbType.Integer, 0, ParameterDirection.Input, secondaryIssueId);
            AddParamToSQLCmd(sqlCmd, "@relationType", OleDbType.Integer, 0, ParameterDirection.Input, IssueRelationType.Related);

            SetCommandType(sqlCmd,CommandType.StoredProcedure, SP_RELATEDISSUE_DELETERELATEDISSUE);
            ExecuteScalarCmd(sqlCmd);
            return true;
        }



        public override Issue GetIssueById(int issueId) {
            // validate Parameters
            if (issueId <= DefaultValues.GetIssueIdMinValue() )
                throw (new ArgumentOutOfRangeException("issueId"));

            // Execute SQL Command
            OleDbCommand sqlCmd = new OleDbCommand();

            AddParamToSQLCmd(sqlCmd, "@IssueId", OleDbType.Integer, 0, ParameterDirection.Input, issueId);

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
            OleDbCommand sqlCmd = new OleDbCommand();

            AddParamToSQLCmd(sqlCmd, "@ProjectId", OleDbType.Integer, 0, ParameterDirection.Input, projectId);

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
            OleDbCommand sqlCmd = new OleDbCommand();

            AddParamToSQLCmd(sqlCmd, "@ProjectId", OleDbType.Integer, 0, ParameterDirection.Input, newMileStone.ProjectId);
            AddParamToSQLCmd(sqlCmd, "@MilestoneName", OleDbType.VarWChar, 50, ParameterDirection.Input, newMileStone.Name);
            AddParamToSQLCmd(sqlCmd, "@MilestoneImageUrl", OleDbType.VarWChar, 255, ParameterDirection.Input, newMileStone.ImageUrl);

            SetCommandType(sqlCmd,CommandType.StoredProcedure, SP_MILESTONE_CREATE);
            return ExecuteScalarCmdWithIdentity(sqlCmd);
        }


        public override  bool DeleteMilestone(int milestoneId) {
            // Validate Parameters
            if (milestoneId <= DefaultValues.GetMilestoneIdMinValue() )
                throw (new ArgumentOutOfRangeException("milestoneId"));

            // Execute SQL Command
            OleDbCommand sqlCmd = new OleDbCommand();

            AddParamToSQLCmd(sqlCmd, "@MilestoneIdToDelete", OleDbType.Integer, 4, ParameterDirection.Input, milestoneId);

            SetCommandType( sqlCmd, CommandType.StoredProcedure, SP_MILESTONE_DELETE);
            ExecuteScalarCmd(sqlCmd);
            return true;
        }



        public override  MilestoneCollection GetMilestoneByProjectId (int projectId) {
            // Validate Parameters
            if (projectId <= DefaultValues.GetMilestoneIdMinValue() )
                throw (new ArgumentOutOfRangeException("projectId"));

            // Execute SQL Command
            OleDbCommand sqlCmd = new OleDbCommand();

            AddParamToSQLCmd(sqlCmd, "@ProjectId", OleDbType.Integer, 0, ParameterDirection.Input, projectId);

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
            OleDbCommand sqlCmd = new OleDbCommand();

            AddParamToSQLCmd(sqlCmd, "@ProjectId", OleDbType.Integer, 0, ParameterDirection.Input, newPriority.ProjectId);
            AddParamToSQLCmd(sqlCmd, "@PriorityName", OleDbType.VarWChar, 50, ParameterDirection.Input, newPriority.Name);
            AddParamToSQLCmd(sqlCmd, "@PriorityImageUrl", OleDbType.VarWChar, 255, ParameterDirection.Input, newPriority.ImageUrl);

            SetCommandType(sqlCmd,CommandType.StoredProcedure, SP_PRIORITY_CREATE);
            return ExecuteScalarCmdWithIdentity(sqlCmd);
        }


        public override bool    DeletePriority (int PriorityId) {
            // Validate Parameters
            if (PriorityId <= DefaultValues.GetPriorityIdMinValue() )
                throw (new ArgumentOutOfRangeException("PriorityId"));

            // Execute SQL Command
            OleDbCommand sqlCmd = new OleDbCommand();

            AddParamToSQLCmd(sqlCmd, "@PriorityIdToDelete", OleDbType.Integer, 0, ParameterDirection.Input, PriorityId);

            SetCommandType( sqlCmd, CommandType.StoredProcedure, SP_PRIORITY_DELETE);
            ExecuteScalarCmd(sqlCmd);
            return true;
        }


        public override PriorityCollection GetPrioritiesByProjectId(int projectId) {
            // Validate Parameters
            if (projectId <= DefaultValues.GetProjectIdMinValue() )
                throw (new ArgumentOutOfRangeException("projectId"));

            // Execute SQL Command
            OleDbCommand sqlCmd = new OleDbCommand();

            AddParamToSQLCmd(sqlCmd, "@ProjectId", OleDbType.Integer, 0, ParameterDirection.Input, projectId);

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
            OleDbCommand sqlCmd = new OleDbCommand();

            AddParamToSQLCmd(sqlCmd, "@ProjectName"         ,OleDbType.VarWChar, 255, ParameterDirection.Input, newProject.Name);
            AddParamToSQLCmd(sqlCmd, "@ProjectDescription"  ,OleDbType.VarWChar, 1000, ParameterDirection.Input, newProject.Description);
            AddParamToSQLCmd(sqlCmd, "@ProjectManagerId"           ,OleDbType.Integer, 0, ParameterDirection.Input, newProject.ManagerId);
            AddParamToSQLCmd(sqlCmd, "@ProjectCreatorUserName"     ,OleDbType.VarWChar, 255, ParameterDirection.Input, newProject.CreatorUserName);

            SetCommandType(sqlCmd,CommandType.StoredProcedure, SP_PROJECT_CREATE);
            return ExecuteScalarCmdWithIdentity(sqlCmd);
        }


        public override bool DeleteProject(int projectID) {
            // Validate Parameters
            if (projectID <= DefaultValues.GetProjectIdMinValue() )
                throw (new ArgumentOutOfRangeException("projectID"));

            // Execute SQL Command
            OleDbCommand sqlCmd = new OleDbCommand();

            AddParamToSQLCmd(sqlCmd, "@ProjectIdToDelete", OleDbType.Integer, 0, ParameterDirection.Input, projectID);

            SetCommandType( sqlCmd, CommandType.StoredProcedure, SP_PROJECT_DELETE);
            ExecuteScalarCmd(sqlCmd);
            return true;
        }


        public override ProjectCollection  GetAllProjects() {
            OleDbCommand sqlCmd = new OleDbCommand();
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
            OleDbCommand sqlCmd = new OleDbCommand();

            AddParamToSQLCmd(sqlCmd, "@Username", OleDbType.VarWChar, 255, ParameterDirection.Input, username);

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
            OleDbCommand sqlCmd = new OleDbCommand();

            AddParamToSQLCmd(sqlCmd, "@ProjectId", OleDbType.Integer, 0, ParameterDirection.Input, projectId);

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
            OleDbCommand sqlCmd = new OleDbCommand();
            AddParamToSQLCmd(sqlCmd, "@ProjectName", OleDbType.VarWChar, 256, ParameterDirection.Input, projectToUpdate.Name);
            AddParamToSQLCmd(sqlCmd, "@ProjectDescription", OleDbType.VarWChar, 255, ParameterDirection.Input, projectToUpdate.Description);
            AddParamToSQLCmd(sqlCmd, "@ProjectManagerId", OleDbType.Integer, 0, ParameterDirection.Input, projectToUpdate.ManagerId);
            AddParamToSQLCmd(sqlCmd, "@ProjectId", OleDbType.Integer, 0, ParameterDirection.Input, projectToUpdate.Id);

            SetCommandType(sqlCmd,CommandType.StoredProcedure, SP_PROJECT_UPDATE);
            ExecuteScalarCmd(sqlCmd);
            return true;
        }



        public override bool AddUserToProject(int userId, int projectId) {
            // validate Parameters
            if (userId <= DefaultValues.GetUserIdMinValue() )
                throw (new ArgumentOutOfRangeException("userId"));

            if (projectId <= DefaultValues.GetProjectIdMinValue() )
                throw (new ArgumentOutOfRangeException("projectId"));

            // Execute SQL Command
            OleDbCommand sqlCmd = new OleDbCommand();

            AddParamToSQLCmd(sqlCmd, "@UserId", OleDbType.Integer, 0, ParameterDirection.Input, userId);
            AddParamToSQLCmd(sqlCmd, "@ProjectId", OleDbType.Integer, 0, ParameterDirection.Input, projectId);

            SetCommandType(sqlCmd,CommandType.StoredProcedure, SP_PROJECT_ADDUSERTOPROJECT);
            ExecuteScalarCmd(sqlCmd);
            return true;
        }



        public override bool RemoveUserFromProject(int userId, int projectId) {
            // validate Parameters
            if (userId <= DefaultValues.GetUserIdMinValue() )
                throw (new ArgumentOutOfRangeException("userId"));

            if (projectId <= DefaultValues.GetProjectIdMinValue() )
                throw (new ArgumentOutOfRangeException("projectId"));

            // Execute SQL Command
            OleDbCommand sqlCmd = new OleDbCommand();

            AddParamToSQLCmd(sqlCmd, "@UserId", OleDbType.Integer, 0, ParameterDirection.Input, userId);
            AddParamToSQLCmd(sqlCmd, "@ProjectId", OleDbType.Integer, 0, ParameterDirection.Input, projectId);

            SetCommandType(sqlCmd,CommandType.StoredProcedure, SP_PROJECT_REMOVEUSERFROMPROJECT);
            ExecuteScalarCmd(sqlCmd);
            return true;
        }



		public override bool CloneProject(int projectId, string projectName) 
		{
			return false;
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
            OleDbCommand sqlCmd = new OleDbCommand();

            AddParamToSQLCmd(sqlCmd, "@ProjectId", OleDbType.Integer, 0, ParameterDirection.Input, newStatus.ProjectId);
            AddParamToSQLCmd(sqlCmd, "@StatusName", OleDbType.VarWChar, 50, ParameterDirection.Input, newStatus.Name);
            AddParamToSQLCmd(sqlCmd, "@StatusImageUrl", OleDbType.VarWChar, 255, ParameterDirection.Input, newStatus.ImageUrl);

            SetCommandType(sqlCmd,CommandType.StoredProcedure, SP_STATUS_CREATE);
            return ExecuteScalarCmdWithIdentity(sqlCmd);
        }


        public override bool DeleteStatus  (int statusId) {
            // Validate Parameters
            if (statusId <= DefaultValues.GetStatusIdMinValue() )
                throw (new ArgumentOutOfRangeException("statusId"));

            // Execute SQL Command
            OleDbCommand sqlCmd = new OleDbCommand();

            AddParamToSQLCmd(sqlCmd, "@StatusIdToDelete", OleDbType.Integer, 0, ParameterDirection.Input, statusId);

            SetCommandType( sqlCmd, CommandType.StoredProcedure, SP_STATUS_DELETE);
            ExecuteScalarCmd(sqlCmd);
            return true;
        }



        public override StatusCollection GetStatusByProjectId(int projectId) {
            // validate Parameters
            if (projectId <= DefaultValues.GetProjectIdMinValue() )
                throw (new ArgumentOutOfRangeException("projectId"));

            // Execute SQL Command
            OleDbCommand sqlCmd = new OleDbCommand();

            AddParamToSQLCmd(sqlCmd, "@ProjectId", OleDbType.Integer, 0, ParameterDirection.Input, projectId);

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
            OleDbCommand sqlCmd = new OleDbCommand();
            SetCommandType(sqlCmd,CommandType.StoredProcedure, SP_USER_AUTHENTICATE);

            AddParamToSQLCmd(sqlCmd, "@Username", OleDbType.VarWChar, 255, ParameterDirection.Input, username);
            AddParamToSQLCmd(sqlCmd, "@Password", OleDbType.VarWChar, 255, ParameterDirection.Input, password);

            object returnValue = ExecuteScalarCmd(sqlCmd);
            return (returnValue == null ? false : true);
        }



        public override ITUser GetUserByUsername(string username) {
            // validate input
            if (username == null || username.Length==0 )
                throw (new ArgumentOutOfRangeException("username"));

            // Execute SQL Command
            OleDbCommand sqlCmd = new OleDbCommand();
            SetCommandType(sqlCmd,CommandType.StoredProcedure, SP_USER_GETUSERBYUSERNAME);

            AddParamToSQLCmd(sqlCmd, "@Username", OleDbType.VarWChar, 255, ParameterDirection.Input, username);

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
            OleDbCommand sqlCmd = new OleDbCommand();

            AddParamToSQLCmd(sqlCmd, "@UserId", OleDbType.Integer, 0, ParameterDirection.Input, userId);

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
            OleDbCommand sqlCmd = new OleDbCommand();
            SetCommandType(sqlCmd,CommandType.StoredProcedure, SP_USER_GETALLUSERS);
            GenerateCollectionFromReader test = new GenerateCollectionFromReader (GenerateITUserCollectionFromReader);
            ITUserCollection userCollection =(ITUserCollection) ExecuteReaderCmd(sqlCmd, test);
            return userCollection;
        }




        public override ITUserCollection GetAllUsersByRoleName(string roleName) {
            // validate Parameters
            if (roleName == null || roleName.Length==0 )
                throw (new ArgumentOutOfRangeException("password"));

            OleDbCommand sqlCmd = new OleDbCommand();

            AddParamToSQLCmd(sqlCmd, "@UserId", OleDbType.VarWChar, 0, ParameterDirection.Input, roleName);

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
            OleDbCommand sqlCmd = new OleDbCommand();
            SetCommandType(sqlCmd,CommandType.StoredProcedure, SP_USER_GETUSERSBYPROJECTID);

            AddParamToSQLCmd(sqlCmd, "@ProjectId", OleDbType.Integer, 0, ParameterDirection.Input, projectId);

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
            OleDbCommand sqlCmd = new OleDbCommand();

            AddParamToSQLCmd(sqlCmd, "@Username", OleDbType.VarWChar, 256, ParameterDirection.Input, userToUpdate.Username);
            AddParamToSQLCmd(sqlCmd, "@Email"  , OleDbType.VarWChar, 256, ParameterDirection.Input, userToUpdate.Email);
            AddParamToSQLCmd(sqlCmd, "@DisplayName"  , OleDbType.VarWChar, 256, ParameterDirection.Input, userToUpdate.DisplayName);
            AddParamToSQLCmd(sqlCmd, "@UserPassword"  , OleDbType.VarWChar, 256, ParameterDirection.Input, userToUpdate.Password);
            AddParamToSQLCmd(sqlCmd, "@UserId", OleDbType.Integer, 0, ParameterDirection.Input, userToUpdate.Id);
            AddParamToSQLCmd(sqlCmd, "@RoleName", OleDbType.VarWChar, 256, ParameterDirection.Input, userToUpdate.RoleName);

            SetCommandType(sqlCmd,CommandType.StoredProcedure, SP_USER_UPDATEUSER);
            ExecuteScalarCmd(sqlCmd);
            return true;
        }


        public override int CreateNewUser(ITUser newUser){
            // Validate Parameters
            if (newUser == null)
                throw (new ArgumentNullException("newUser"));

            // Check for duplicate user
            OleDbCommand sqlCmd = new OleDbCommand();

            AddParamToSQLCmd(sqlCmd, "@Username", OleDbType.VarWChar, 255, ParameterDirection.Input, newUser.Username);

            SetCommandType( sqlCmd, CommandType.StoredProcedure, SP_USER_EXISTS);
            if (ExecuteScalarCmd(sqlCmd) != null)
                return 0;

            // Execute SQL Command
            sqlCmd = new OleDbCommand();

            AddParamToSQLCmd(sqlCmd, "@Username"          , OleDbType.VarWChar, 256, ParameterDirection.Input, newUser.Username);
            AddParamToSQLCmd(sqlCmd, "@Email"  , OleDbType.VarWChar, 256, ParameterDirection.Input, newUser.Email);
            AddParamToSQLCmd(sqlCmd, "@DisplayName"  , OleDbType.VarWChar, 256, ParameterDirection.Input, newUser.DisplayName);
            AddParamToSQLCmd(sqlCmd, "@UserPassword"  , OleDbType.VarWChar, 256, ParameterDirection.Input, newUser.Password);
            AddParamToSQLCmd(sqlCmd, "@RoleName"  , OleDbType.VarWChar, 256, ParameterDirection.Input, newUser.RoleName);

            SetCommandType(sqlCmd,CommandType.StoredProcedure, SP_USER_CREATENEWUSER);
            return ExecuteScalarCmdWithIdentity(sqlCmd);
        }




	    //*********************************************************************
	    //
	    // Role Methods
	    //
	    // The following methods are used for working with roles.
	    //
	    //*********************************************************************

        public override RoleCollection GetAllRoles() {
            OleDbCommand sqlCmd = new OleDbCommand();
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
            OleDbCommand sqlCmd = new OleDbCommand();

            AddParamToSQLCmd(sqlCmd, "@ProjectId", OleDbType.Integer, 0, ParameterDirection.Input, projectId);

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
            OleDbCommand sqlCmd = new OleDbCommand();

            AddParamToSQLCmd(sqlCmd, "@issueId", OleDbType.Integer, 0, ParameterDirection.Input, issueId);

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
            OleDbCommand sqlCmd = new OleDbCommand();

            AddParamToSQLCmd(sqlCmd, "@ProjectId", OleDbType.Integer, 0, ParameterDirection.Input, newCustomField.ProjectId);
            AddParamToSQLCmd(sqlCmd, "@CustomFieldName", OleDbType.VarWChar, 50, ParameterDirection.Input, newCustomField.Name);
            AddParamToSQLCmd(sqlCmd, "@CustomFieldDataType", OleDbType.Integer, 0, ParameterDirection.Input, newCustomField.DataType);
            AddParamToSQLCmd(sqlCmd, "@CustomFieldRequired", OleDbType.Boolean, 0, ParameterDirection.Input, newCustomField.Required);

            SetCommandType(sqlCmd,CommandType.StoredProcedure, SP_CUSTOMFIELD_CREATE);
            return ExecuteScalarCmdWithIdentity(sqlCmd);
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
            OleDbCommand sqlCmd = new OleDbCommand();

            AddParamToSQLCmd(sqlCmd, "@ResultValue", OleDbType.Integer, 0, ParameterDirection.ReturnValue, null);
            AddParamToSQLCmd(sqlCmd, "@CustomFieldId", OleDbType.Integer, 0, ParameterDirection.Input, customFieldToUpdate.Id);
            AddParamToSQLCmd(sqlCmd, "@CustomFieldName", OleDbType.VarWChar, 50, ParameterDirection.Input, customFieldToUpdate.Name);
            AddParamToSQLCmd(sqlCmd, "@CustomFieldDataType", OleDbType.Integer, 0, ParameterDirection.Input, customFieldToUpdate.DataType);
            AddParamToSQLCmd(sqlCmd, "@CustomFieldRequired", OleDbType.Boolean, 0, ParameterDirection.Input, customFieldToUpdate.Required);

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
            OleDbCommand sqlCmd = new OleDbCommand();

            AddParamToSQLCmd(sqlCmd, "@CustomIdToDelete", OleDbType.Integer, 0, ParameterDirection.Input, customFieldId);

            SetCommandType( sqlCmd, CommandType.StoredProcedure, SP_CUSTOMFIELD_DELETE);
            ExecuteScalarCmd(sqlCmd);
            return true;
        }



        public override bool SaveCustomFieldValues(int issueId, CustomFieldCollection fields) {
            // Validate Parameters
            if (fields == null)
                throw (new ArgumentNullException("fields"));

            if (issueId <= DefaultValues.GetIssueIdMinValue() )
                throw (new ArgumentOutOfRangeException("issueId"));

            if (ConnectionString == string.Empty)
                throw (new ArgumentOutOfRangeException("ConnectionString"));


            using (OleDbConnection cn = new OleDbConnection(this.ConnectionString)) {
				        cn.Open();

				        foreach (CustomField field in fields) {

                            OleDbCommand sqlCmd = new OleDbCommand();
				            sqlCmd.Connection = cn;

                            if (CustomFieldExists(cn, issueId, field.Id))
                                sqlCmd.CommandText = SP_CUSTOMFIELD_UPDATECUSTOMFIELDVALUE;
                            else
				                sqlCmd.CommandText = SP_CUSTOMFIELD_INSERTCUSTOMFIELDVALUE;

                            sqlCmd.CommandType = CommandType.StoredProcedure;
                            sqlCmd.Parameters.Add( "@CustomFieldValue", OleDbType.VarWChar, 255).Direction = ParameterDirection.Input;
                            sqlCmd.Parameters.Add( "@IssueId", OleDbType.Integer, 0).Direction = ParameterDirection.Input;
                            sqlCmd.Parameters.Add( "@CustomFieldId", OleDbType.Integer, 0).Direction = ParameterDirection.Input;

				            sqlCmd.Parameters["@IssueId"].Value = issueId;
				            sqlCmd.Parameters["@CustomFieldId"].Value = field.Id;
				            sqlCmd.Parameters["@CustomFieldValue"].Value = field.Value;
				            sqlCmd.ExecuteNonQuery();
				        }
            }

            return true;
        }


        bool CustomFieldExists(OleDbConnection cn, int issueId, int customFieldId) {
            OleDbCommand existCmd = new OleDbCommand();
            existCmd.Connection = cn;
            SetCommandType(existCmd, CommandType.StoredProcedure, SP_CUSTOMFIELD_EXISTS);
            existCmd.Parameters.Add( "@IssueId", OleDbType.Integer, 0).Direction = ParameterDirection.Input;
            existCmd.Parameters["@IssueId"].Value = issueId;
            existCmd.Parameters.Add( "@CustomFieldId", OleDbType.Integer, 0).Direction = ParameterDirection.Input;
            existCmd.Parameters["@CustomFieldId"].Value = customFieldId;
            if (existCmd.ExecuteScalar() == null)
                return false;
            else
                return true;
        }






	    //*********************************************************************
	    //
	    // SQL Helper Methods
	    //
	    // The following utility methods are used to interact with SQL Server.
	    //
	    //*********************************************************************


        private void AddParamToSQLCmd(OleDbCommand sqlCmd, string paramId, OleDbType sqlType, int paramSize, ParameterDirection paramDirection, object paramvalue) {
            // Validate Parameter Properties
            if (sqlCmd== null)
                throw (new ArgumentNullException("sqlCmd"));
            if (paramId == string.Empty)
                throw (new ArgumentOutOfRangeException("paramId"));

            // Add Parameter
            OleDbParameter newSqlParam = new OleDbParameter();
            newSqlParam.ParameterName= paramId;
            newSqlParam.OleDbType = sqlType;
            newSqlParam.Direction = paramDirection;

            if (paramSize > 0)
                newSqlParam.Size=paramSize;

            if (paramvalue != null)
                newSqlParam.Value = paramvalue;

            sqlCmd.Parameters.Add (newSqlParam);
        }


        private object ExecuteScalarCmd(OleDbCommand sqlCmd) {
            // Validate Command Properties
            if (ConnectionString == string.Empty)
                throw (new ArgumentOutOfRangeException("ConnectionString"));

            if (sqlCmd== null)
                throw (new ArgumentNullException("sqlCmd"));

            object result = null;
            using (OleDbConnection cn = new OleDbConnection(this.ConnectionString)) {
				        sqlCmd.Connection = cn;
				        cn.Open();
				        result = sqlCmd.ExecuteScalar();
            }

            return result;
        }




        private int ExecuteScalarCmdWithIdentity(OleDbCommand sqlCmd) {
            // Validate Command Properties
            if (ConnectionString == string.Empty)
                throw (new ArgumentOutOfRangeException("ConnectionString"));

            if (sqlCmd== null)
                throw (new ArgumentNullException("sqlCmd"));

            OleDbCommand idCmd = new OleDbCommand();
            idCmd.CommandText = "SELECT @@IDENTITY";

            int result = 0;
            using (OleDbConnection cn = new OleDbConnection(this.ConnectionString)) {
				        sqlCmd.Connection = cn;
				        idCmd.Connection = cn;
				        cn.Open();
				        sqlCmd.ExecuteNonQuery();
				        result = (int)idCmd.ExecuteScalar();
            }

            return result;
        }



		private CollectionBase ExecuteReaderCmd(OleDbCommand sqlCmd, GenerateCollectionFromReader gcfr) {
			if (ConnectionString == string.Empty)
				throw (new ArgumentOutOfRangeException("ConnectionString"));

			if (sqlCmd== null)
				throw (new ArgumentNullException("sqlCmd"));

			using (OleDbConnection cn = new OleDbConnection(this.ConnectionString)) {
				sqlCmd.Connection = cn;
                cn.Open();
				CollectionBase temp = gcfr(sqlCmd.ExecuteReader());
                return (temp);
			}
		}



        private void SetCommandType(OleDbCommand sqlCmd, CommandType cmdType, string cmdText) {
            sqlCmd.CommandType = cmdType;
            sqlCmd.CommandText = cmdText;
        }





    }
}
