using System;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Collections;
using System.Collections.Specialized;
using BugsTrackers.BusinessLogicLayer;
using System.Configuration;

namespace BugsTrackers.DataAccessLayer {



	//*********************************************************************
	//
	// DataAccessLayerBaseClass Class
	//
	// The DataAccessLayerBaseClass lists all the abstract methods
	// that each data access layer provider (SQL Server, Access, etc.)
	// must implement.
	//
	//*********************************************************************


    public  abstract class DataAccessLayerBaseClass {

        /*** PRIVATE FIELDS ***/

        private string _connectionString ;

        /*** PROPERTIES ***/

        public string ConnectionString {
            get {
                string str = ConfigurationSettings.AppSettings["ConnectionString"];
                if (str==null || str.Length <=0)
                    throw (new ApplicationException("ConnectionString configuration is missing from you web.config. It should contain  <appSettings><add key=\"ConnectionString\" value=\"database=IssueTrackerStarterKit;server=localhost;Trusted_Connection=true\" /></appSettings> "));
                else
                    return (str);
            }
            set {_connectionString = value;}
        }


		/*** Abstract Properties ***/
		public abstract bool SupportsProjectCloning{get;}


        /*** ABSTRACT METHODS ***/

        // Category
        public abstract int    CreateNewCategory( Category newCategory );
        public abstract bool DeleteCategory( int categoryId );
        public abstract CategoryCollection GetCategoriesByProjectId( int projectId );

        // Issue
        public abstract int CreateNewIssue (Issue newIssue);
        public abstract bool   DeleteIssue (int issueId);
        public abstract IssueCollection GetIssuesByRelevancy(int projectId, string username);
        public abstract IssueCollection GetIssuesByAssignedUsername(int projectId, string AssignedUsername);
        public abstract IssueCollection GetIssuesByCreatorUsername(int projectId, string creatorUsername);
        public abstract IssueCollection GetIssuesByOwnerUsername(int projectId, string creatorUsername);
        public abstract Issue GetIssueById(int issueId);
        public abstract IssueCollection GetIssuesByProjectId (int projectId);
        public abstract bool UpdateIssue(Issue issueToUpdate);
        public abstract IssueCollection PerformQuery(int projectId, QueryClauseCollection queryClauses);
        public abstract IssueCollection PerformSavedQuery(int projectId, int queryId);

        // Related Issues
        public abstract RelatedIssueCollection GetChildIssues(int issueId);
        public abstract RelatedIssueCollection GetParentIssues(int issueId);
        public abstract RelatedIssueCollection GetRelatedIssues(int issueId);
        public abstract int CreateNewChildIssue(int primaryIssueId, int secondaryIssueId);
        public abstract bool DeleteChildIssue(int primaryIssueId, int secondaryIssueId);
        public abstract int CreateNewParentIssue(int primaryIssueId, int secondaryIssueId);
        public abstract bool DeleteParentIssue(int primaryIssueId, int secondaryIssueId);
        public abstract int CreateNewRelatedIssue(int primaryIssueId, int secondaryIssueId);
        public abstract bool DeleteRelatedIssue(int primaryIssueId, int secondaryIssueId);

        // Queries
        public abstract QueryCollection GetQueriesByUsername(string username, int projectId);
        public abstract bool SaveQuery(string username, int projectId, string queryName, QueryClauseCollection queryClauses);
        public abstract bool DeleteQuery(int queryId);

        // IssueComments
        public abstract int CreateNewIssueComment (IssueComment newComment);
        public abstract IssueCommentCollection GetIssueCommentsByIssueId  (int issueId);

		// IssueAttachments
		public abstract int CreateNewIssueAttachment (IssueAttachment newAttachment);
		public abstract IssueAttachmentCollection GetIssueAttachmentsByIssueId  (int issueId);
		public abstract IssueAttachment GetIssueAttachmentById  (int attachmentId);
		public abstract void DeleteIssueAttachment  (int attachmentId);

        // IssueHistory
        public abstract IssueHistoryCollection GetIssueHistoryByIssueId  (int issueId);

        // IssueNotifications
        public abstract int CreateNewIssueNotification (IssueNotification newNotification);
        public abstract IssueNotificationCollection GetIssueNotificationsByIssueId (int issueId);
        public abstract bool DeleteIssueNotification (int issueId, string username);

        // Milestone
        public abstract  int CreateNewMilestone(Milestone newMileStone);
        public abstract  bool DeleteMilestone(int milestoneId);
        public abstract  MilestoneCollection GetMilestoneByProjectId (int milestoneId);

        // Priority
        public abstract int  CreateNewPriority   (Priority newPriority);
        public abstract bool    DeletePriority      (int PriorityId);
        public abstract PriorityCollection GetPrioritiesByProjectId(int projectId);

        // Project
        public abstract int  CreateNewProject(Project newProject);
        public abstract bool    DeleteProject   (int projectID);
        public abstract ProjectCollection GetAllProjects();
        public abstract Project GetProjectById(int  projectId);
        public abstract ProjectCollection GetProjectByMemberUsername(string username);
        public abstract bool    UpdateProject   (Project projectToUpdate);
        public abstract bool AddUserToProject(int userId, int projectId);
        public abstract bool RemoveUserFromProject(int userId, int projectId);
		public abstract bool CloneProject(int projectId, string projectName);

        // Status
        public abstract int  CreateNewStatus   (Status newStatus);
        public abstract bool    DeleteStatus      (int statusId);
        public abstract StatusCollection GetStatusByProjectId(int projectId);

        // User
        public abstract bool Authenticate(string username, string password);
        public abstract ITUser GetUserById(int userId);
        public abstract ITUser GetUserByUsername(string username);
        public abstract ITUserCollection GetAllUsers();
        public abstract ITUserCollection GetAllUsersByRoleName(string roleName);
        public abstract ITUserCollection GetUsersByProjectId(int projectId);
        public abstract int CreateNewUser(ITUser user);
        public abstract bool UpdateUser(ITUser user);

        // Role
        public abstract RoleCollection GetAllRoles();

        // Custom Fields
        public abstract CustomFieldCollection GetCustomFieldsByProjectId(int projectId);
        public abstract CustomFieldCollection GetCustomFieldsByIssueId(int issueId);
        public abstract int  CreateNewCustomField(CustomField newCustomField);
        public abstract bool    UpdateCustomField(CustomField customFieldToUpdate);
        public abstract bool    DeleteCustomField(int customFieldId);
        public abstract bool    SaveCustomFieldValues(int issueId, CustomFieldCollection fields);
 
	

		//*********************************************************************
		//
		// GenerateCollectionFromReader Delegate
		//
		// The GenerateCollectionFromReader delegate represents any method
		// which returns a collection from a Data Reader.
		//
		//*********************************************************************

		protected delegate CollectionBase GenerateCollectionFromReader(IDataReader returnData);

	

		//*********************************************************************
		//
		// Generate Collection Helper Methods
		//
		// The following methods are used to generate collections of objects.
		//
		//*********************************************************************


		protected CollectionBase  GenerateCategoryCollectionFromReader(IDataReader returnData) 
		{
			CategoryCollection mlsCollection = new CategoryCollection();
			while (returnData.Read()) 
			{
				Category newCategory=new Category((int)returnData["CategoryId"], (int)returnData["ProjectId"],(int)returnData["ParentCategoryId"],(string)returnData["CategoryName"]  );
				mlsCollection.Add(newCategory);
			}
			return (mlsCollection);
		}


		protected CollectionBase  GenerateIssueCollectionFromReader(IDataReader returnData) 
		{
			IssueCollection issCollection = new IssueCollection();
			while (returnData.Read()) 
			{
                    int projectId = (int)returnData["ProjectId"];
                    string issueTitle = (string)returnData["IssueTitle"];
                    int issueCategoryId = (int)returnData["IssueCategoryId"];
                    string categoryName = (string)returnData["CategoryName"];
                    int issueMilestoneId = (int)returnData["IssueMilestoneId"];
                    string milestoneName = (string)returnData["MilestoneName"];
                    string milestoneImageUrl = (string)returnData["MilestoneImageUrl"];
                    int issuePriorityId = (int)returnData["IssuePriorityId"];
                    string priorityName  =(string)returnData["PriorityName"];
                    string priorityImageUrl = (string)returnData["PriorityImageUrl"];
                    int issueStatusId = (int)returnData["IssueStatusId"];
                    string statusName = (string)returnData["StatusName"];
                    string statusImageUrl = (string)returnData["StatusImageUrl"];
                    string assignedDisplayName = (string)returnData["AssignedDisplayName"];
                    int issueAssignedId = (int)returnData["IssueAssignedId"];
                    string ownerDisplayName = (string)returnData["OwnerDisplayName"];
                    int issueOwnerId = (int)returnData["IssueOwnerId"];
                    string creatorDisplayName = (string)returnData["CreatorDisplayName"];
                    int issueCreatorId = (int)returnData["IssueCreatorId"];
                    string creatorUsername = (string)returnData["CreatorUsername"];
                    DateTime dateCreated = (DateTime)returnData["DateCreated"];


                Issue newIssue = new Issue((int)returnData["IssueId"],
                    projectId ,
                    issueTitle ,
                    issueCategoryId ,
                    categoryName ,
                    issueMilestoneId ,
                    milestoneName ,
                    milestoneImageUrl ,
                    issuePriorityId ,
                    priorityName ,
                    priorityImageUrl ,
                    issueStatusId ,
                    statusName ,
                    statusImageUrl ,
                    assignedDisplayName ,
                    issueAssignedId ,
                    ownerDisplayName ,
                    issueOwnerId ,
                    creatorDisplayName ,
                    issueCreatorId ,
                    creatorUsername ,
                    dateCreated );

				issCollection.Add(newIssue);

			}
			return (issCollection);
		}



		protected CollectionBase  GenerateRelatedIssueCollectionFromReader(IDataReader returnData) 
		{
			RelatedIssueCollection issCollection = new RelatedIssueCollection();
			while (returnData.Read()) 
			{
				RelatedIssue  newIssue = new RelatedIssue( (int)returnData["IssueId"], (string)returnData["IssueTitle"], (DateTime)returnData["DateCreated"]);
				issCollection.Add(newIssue);
			}
			return (issCollection);
		}


		protected CollectionBase  GenerateIssueCommentCollectionFromReader(IDataReader returnData) 
		{
			IssueCommentCollection cmtCollection = new IssueCommentCollection();
			while (returnData.Read()) 
			{
				IssueComment newComment=new IssueComment((int)returnData["CommentId"], (int)returnData["IssueId"], (string)returnData["Comment"], (string)returnData["CreatorUsername"], (string)returnData["CreatorDisplayName"], (DateTime)returnData["DateCreated"] );
				cmtCollection.Add(newComment);
			}
			return (cmtCollection);
		}


		protected CollectionBase  GenerateIssueAttachmentCollectionFromReader(IDataReader returnData) 
		{
			IssueAttachmentCollection attCollection = new IssueAttachmentCollection();
			while (returnData.Read()) 
			{
				IssueAttachment newAttachment=new IssueAttachment((int)returnData["AttachmentId"], (int)returnData["IssueId"], (string)returnData["CreatorUsername"], (string)returnData["CreatorDisplayName"], (DateTime)returnData["DateCreated"], (string)returnData["FileName"] );
				attCollection.Add(newAttachment);
			}
			return (attCollection);
		}


		protected CollectionBase  GenerateIssueHistoryCollectionFromReader(IDataReader returnData) 
		{
			IssueHistoryCollection hstCollection = new IssueHistoryCollection();
			while (returnData.Read()) 
			{
				IssueHistory newHistory=new IssueHistory((int)returnData["HistoryId"], (int)returnData["IssueId"], (string)returnData["CreatorDisplayName"], (DateTime)returnData["DateCreated"] );
				hstCollection.Add(newHistory);
			}
			return (hstCollection);
		}



		protected CollectionBase  GenerateIssueNotificationCollectionFromReader(IDataReader returnData) 
		{
			IssueNotificationCollection notCollection = new IssueNotificationCollection();
			while (returnData.Read()) 
			{
				IssueNotification newNotification =new IssueNotification((int)returnData["NotificationId"], (int)returnData["IssueId"], (string)returnData["NotificationUsername"], (string)returnData["NotificationDisplayName"], (string)returnData["NotificationEmail"]);
				notCollection.Add(newNotification);
			}
			return (notCollection);
		}


		protected CollectionBase  GenerateITUserCollectionFromReader(IDataReader returnData) 
		{
			ITUserCollection userCollection = new ITUserCollection();
			while (returnData.Read()) 
			{
				ITUser newUser=new ITUser((int)returnData["UserId"], (string)returnData["Username"], (string)returnData["RoleName"], (string)returnData["Email"], (string)returnData["DisplayName"], (string)returnData["UserPassword"]);
				userCollection.Add(newUser);
			}
			return (userCollection);
		}



		protected CollectionBase  GenerateMilestoneCollectionFromReader(IDataReader returnData) 
		{
			MilestoneCollection mlsCollection = new MilestoneCollection();
			while (returnData.Read()) 
			{
				Milestone newMilestone=new Milestone((int)returnData["MilestoneId"], (int)returnData["ProjectId"], (string)returnData["MilestoneName"], (string)returnData["MilestoneImageUrl"]);
				mlsCollection.Add(newMilestone);
			}
			return (mlsCollection);
		}


		protected CollectionBase GeneratePriorityCollectionFromReader(IDataReader returnData) 
		{
			PriorityCollection mlsCollection = new PriorityCollection();
			while (returnData.Read()) 
			{
				Priority newPriority=new Priority((int)returnData["PriorityId"], (int)returnData["ProjectId"], (string)returnData["PriorityName"], (string)returnData["PriorityImageUrl"]);
				mlsCollection.Add(newPriority);
			}
			return (mlsCollection);
		}


		protected CollectionBase  GenerateProjectCollectionFromReader(IDataReader returnData) 
		{
			ProjectCollection prjCollection = new ProjectCollection();
			while (returnData.Read()) 
			{
				Project prj=new Project((int)returnData["ProjectId"], (string)returnData["ProjectName"], (string)returnData["ProjectDescription"],
					(string)returnData["ProjectManagerDisplayName"], (string)returnData["ProjectCreatorDisplayName"], (DateTime)returnData["ProjectCreationDate"] );

				prjCollection.Add(prj);
			}
			return (prjCollection);
		}


		protected CollectionBase  GenerateStatusCollectionFromReader(IDataReader returnData) 
		{
			StatusCollection stsCollection = new StatusCollection();
			while (returnData.Read()) 
			{
				Status newStatus=new Status((int)returnData["StatusId"], (int)returnData["ProjectId"], (string)returnData["StatusName"], (string)returnData["StatusImageUrl"]);
				stsCollection.Add(newStatus);
			}
			return (stsCollection );
		}



		protected CollectionBase  GenerateQueryCollectionFromReader(IDataReader returnData) 
		{
			QueryCollection qCollection = new QueryCollection();
			while (returnData.Read()) 
			{
				Query newQuery = new Query((int)returnData["QueryId"], (string)returnData["QueryName"]);
				qCollection.Add(newQuery);
			}
			return qCollection;
		}



		protected CollectionBase  GenerateRoleCollectionFromReader(IDataReader returnData) 
		{
			RoleCollection colRoles = new RoleCollection();
			while (returnData.Read()) 
			{
				Role newRole = new Role((int)returnData["RoleId"], (string)returnData["RoleName"]);
				colRoles.Add(newRole);
			}
			return (colRoles );
		}


		protected CollectionBase  GenerateCustomFieldCollectionFromReader(IDataReader returnData) 
		{
			CustomFieldCollection cfCollection = new CustomFieldCollection();
			while (returnData.Read()) 
			{
				CustomField cf = new CustomField ((int)returnData["CustomFieldId"], (int)returnData["ProjectId"], (string)returnData["CustomFieldName"], (ValidationDataType)returnData["CustomFieldDataType"], (bool)returnData["CustomFieldRequired"], (string)returnData["CustomFieldValue"]);
				cfCollection.Add(cf);
			}
			return (cfCollection);
		}


		protected CollectionBase  GenerateQueryClauseCollectionFromReader(IDataReader returnData) 
		{
			QueryClauseCollection queryCollection = new QueryClauseCollection();
			while (returnData.Read()) 
			{
				QueryClause newQueryClause = new QueryClause((string)returnData["BooleanOperator"], (string)returnData["FieldName"],(string)returnData["ComparisonOperator"],(string)returnData["FieldValue"], (SqlDbType)returnData["DataType"]  );
				queryCollection.Add(newQueryClause);
			}
			return (queryCollection);
		}

	
	}



	//*********************************************************************
	//
	// DataAccessLyerBaseClassHelper Class
	//
	// Loads different data access layers depending on the configuration
	// setting in the Web.Config file.
	//
	//*********************************************************************


    public  class DataAccessLayerBaseClassHelper {
		public static DataAccessLayerBaseClass GetDataAccessLayer() 
		{
//Following line has been commented out and new code added replacing 'Type.GetType' with 'BuildManager.GetType' 
//			Type trp = Type.GetType(Globals.DataAccessType, true);
			Type trp = System.Web.Compilation.BuildManager.GetType(Globals.DataAccessType, true);
			// Throw an error if wrong base type
			if (trp.BaseType != Type.GetType("BugsTrackers.DataAccessLayer.DataAccessLayerBaseClass"))
				throw new Exception( "Data Access Layer does not inherit DataAccessLayerBaseClass!");
			DataAccessLayerBaseClass dc=  (DataAccessLayerBaseClass)Activator.CreateInstance (trp);
			return (dc);
		}



    }


  }

