using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Collections;
//using BugsTrackers.DataAccessLayer;
using System.Configuration;

namespace BugsTrackers.BusinessLogicLayer {


	//*********************************************************************
	//
	// Project Class
	//
	// Represents a project (a group of related issues).
	//
	//*********************************************************************


    public class Project {

        /*** PRIVATE FIELDS ***/

        private string       _Description;
        private int          _Id;
        private string       _Name;
        private int          _ManagerId;
        private string       _ManagerDisplayName;
        private DateTime     _DateCreated;
        private string       _CreatorUserName;
        private string       _CreatorDisplayName;


        /*** CONSTRUCTORS ***/
		public Project()
		{}

        public Project(int id, string name, string description, int managerId, string creatorUsername)
                :this (id, name, description, managerId, String.Empty, creatorUsername, String.Empty, DefaultValues.GetDateTimeMinValue() )
        { }


        public Project(int id, string name, string description, string managerDisplayName, string creatorDisplayName, DateTime dateCreated)
                :this (id, name, description, DefaultValues.GetUserIdMinValue(), managerDisplayName, String.Empty, creatorDisplayName, dateCreated)
        { }



        public Project(int id, string name, string description, int managerId, string managerDisplayName, string creatorUsername, string creatorDisplayName, DateTime dateCreated ) {
            // Validate Mandatory Fields//
            if (name == null ||name.Length==0 )
                throw (new ArgumentOutOfRangeException("name"));

            _Id                       = id;
            _Description              = description;
            _Name                     = name;
            _ManagerId                = managerId;
            _ManagerDisplayName       = managerDisplayName;
            _CreatorUserName          = creatorUsername;
            _CreatorDisplayName       = creatorDisplayName;
            _DateCreated              = dateCreated;
        }


        /*** PROPERTIES ***/

        public string CreatorUserName {
            get {return _CreatorUserName;}
        }

        public int Id {
            get {return _Id;}
        }


        public string Name {
            get
            {
                if (_Name == null ||_Name.Length==0)
                    return string.Empty;
                else
                    return _Name;
            }
            set {_Name = value;}
        }


        public string Description {
            get {
                if (_Description == null ||_Description.Length==0)
                    return string.Empty;
                else
                    return _Description;
            }
            set
            { _Description = value;}
        }

        public DateTime DateCreated {
            get {return  _DateCreated;}
        }

        public int ManagerId {
            get {return  _ManagerId;}
        }


        public string ManagerDisplayName {
            get {
                if (_ManagerDisplayName == null || _ManagerDisplayName.Length==0)
                    return string.Empty;
                else
                    return _ManagerDisplayName;
            }
            set {_ManagerDisplayName = value;}
        }


/// //////////////////////////////////////////////////////////////////////
		/// 				private CategoriesCollection _categories;
		private DateTime			_estCompletionDate;
		private decimal				_estDuration;
		private int					_managerUserID;
		private string				_managerUserName;

		public DateTime EstCompletionDate
		{
			get{ return _estCompletionDate; }
			set{ _estCompletionDate = value; }
		}

		public decimal EstDuration
		{
			get{ return _estDuration; }
			set{ _estDuration = value; }
		}

		public int ManagerUserID
		{
			get{ return _managerUserID; }
			set{ _managerUserID = value; }
		}

		public string ManagerUserName
		{
			get{ return _managerUserName; }
			set{ _managerUserName = value; }
		}
		private int _projectID;
		public int ProjectID
		{
			get{ return _projectID; }
			set{ _projectID = value; }
		}


		//*********************************************************************
		//
		// The GetCategories method retrieves the list of categories for the project associated with
		// the entered project id.  This method returns a collection of Categories objects.
		//
		//*********************************************************************

		public static CategoriesCollection GetCategories(int projectID)
		{
			DataSet ds = SqlHelper.ExecuteDataset(
				ConfigurationSettings.AppSettings[Web.Global.CfgKeyConnString], 
				"ListCategories", projectID); 

			CategoriesCollection categories = new CategoriesCollection();
			foreach(DataRow r in ds.Tables[0].Rows)
			{
				Category cat = new Category();
				cat.CategoryID = Convert.ToInt32(r["CategoryID"]);
				cat.ProjectID = projectID;
				cat.Name = r["CategoryName"].ToString();
				cat.Abbreviation = r["Abbreviation"].ToString();
				cat.EstDuration = Convert.ToDecimal(r["EstDuration"]);
				categories.Add(cat);
			}
			return categories;	
		}
		//----------------------------------------------------------------


        public string CreatorDisplayName {
            get {
                if (_CreatorDisplayName == null || _CreatorDisplayName.Length==0)
                    return string.Empty;
                else
                    return _CreatorDisplayName;
            }
            set {_CreatorDisplayName = value;}
        }



        /*** INSTANCE METHOD  ***/

        public bool Delete () {
            DataAccessLayerBaseClass DBLayer = DataAccessLayerBaseClassHelper.GetDataAccessLayer();
            return DBLayer.DeleteProject (this.Id);
        }


        public bool Save () {
            DataAccessLayerBaseClass DBLayer = DataAccessLayerBaseClassHelper.GetDataAccessLayer();
            if (Id <= DefaultValues.GetProjectIdMinValue()) {
                int TempId = DBLayer.CreateNewProject(this);
                if (TempId>0) {
                    _Id = TempId;
                    return true;
                }  else
                    return false;
            }
            else
                return (DBLayer.UpdateProject(this));
        }


		/*** STATIC PROPERTIES ***/

		public static bool SupportsProjectCloning 
		{
			get 
			{
				DataAccessLayerBaseClass DBLayer = DataAccessLayerBaseClassHelper.GetDataAccessLayer();
				return (DBLayer.SupportsProjectCloning);
			}
		}




        /*** STATIC METHODS ***/

        public static bool DeleteProject (int projectId) 
		{
            // validate input
            if (projectId <= DefaultValues.GetProjectIdMinValue() )
                throw (new ArgumentOutOfRangeException("projectId"));

            DataAccessLayerBaseClass DBLayer = DataAccessLayerBaseClassHelper.GetDataAccessLayer();
            return (DBLayer.DeleteProject(projectId));
        }



        public static ProjectCollection GetAllProjects() {
            DataAccessLayerBaseClass DBLayer = DataAccessLayerBaseClassHelper.GetDataAccessLayer();
            return (DBLayer.GetAllProjects());
        }



        public static Project GetProjectById(int  projectId) {
            // validate input
            if (projectId <= 0)
                throw (new ArgumentOutOfRangeException("projectId"));

            DataAccessLayerBaseClass DBLayer = DataAccessLayerBaseClassHelper.GetDataAccessLayer();
            return (DBLayer.GetProjectById(projectId));
        }



        public static ProjectCollection GetProjectByMemberUsername(string username) {
            // validate input
            if (username == string.Empty)
                throw (new ArgumentOutOfRangeException("username"));

            DataAccessLayerBaseClass DBLayer = DataAccessLayerBaseClassHelper.GetDataAccessLayer();
            return (DBLayer.GetProjectByMemberUsername(username));
        }



        public static bool AddUserToProject(int userId, int projectId) {
            DataAccessLayerBaseClass DBLayer = DataAccessLayerBaseClassHelper.GetDataAccessLayer();
            return (DBLayer.AddUserToProject(userId, projectId));
        }



        public static bool RemoveUserFromProject(int userId, int projectId) {
            DataAccessLayerBaseClass DBLayer = DataAccessLayerBaseClassHelper.GetDataAccessLayer();
            return (DBLayer.RemoveUserFromProject(userId, projectId));
        }

		
		public static bool CloneProject(int projectId, string projectName) 
		{
			DataAccessLayerBaseClass DBLayer = DataAccessLayerBaseClassHelper.GetDataAccessLayer();
			return (DBLayer.CloneProject(projectId, projectName));
		}

		//*********************************************************************
		//
		// Retrieves the list of all the projects
		//
		//*********************************************************************

		public static ProjectsCollection GetProjects()
		{
			DataSet ds = SqlHelper.ExecuteDataset(
				ConfigurationSettings.AppSettings[Web.Global.CfgKeyConnString], 
				CommandType.StoredProcedure, "TT_ListAllProjects");  

			// Separate Data into a Collection of projects
			ProjectsCollection projects = new ProjectsCollection();
			foreach(DataRow r in ds.Tables[0].Rows)
			{
				Project prj = new Project();
				prj.ProjectID = Convert.ToInt32(r["ProjectID"]);
				prj.Name = r["ProjectName"].ToString();
				prj.Description = r["Description"].ToString();
				prj.ManagerUserID = Convert.ToInt32(r["ManagerUserID"]);
				prj.EstCompletionDate = Convert.ToDateTime(r["EstCompletionDate"]);
				prj.EstDuration = Convert.ToDecimal(r["EstDuration"]);
				projects.Add(prj);
			}
			return projects;
		}

		//*********************************************************************
		//
		// Retrieves a list of projects based on the user's role
		//
		//*********************************************************************

		public static ProjectsCollection GetProjects(int userID, string role)
		{
			string firstName = string.Empty;
			string lastName = string.Empty;

			DataSet ds = SqlHelper.ExecuteDataset(
				ConfigurationSettings.AppSettings[Web.Global.CfgKeyConnString],
				"TT_ListProjects", userID, Convert.ToInt32(role));  

			ProjectsCollection projects = new ProjectsCollection();
			foreach(DataRow r in ds.Tables[0].Rows)
			{
				Project prj = new Project();
				prj.ProjectID = Convert.ToInt32(r["ProjectID"]);
				prj.Name = r["ProjectName"].ToString();
				prj.Description = r["Description"].ToString();
				prj.ManagerUserID = Convert.ToInt32(r["ManagerUserID"]);
				prj.ManagerUserName = 
					ITUser.GetDisplayName(Convert.ToString(r["UserName"]), ref firstName, ref lastName);
				prj.EstCompletionDate = Convert.ToDateTime(r["EstCompletionDate"]);
				prj.EstDuration = Convert.ToDecimal(r["EstDuration"]);
				projects.Add(prj);
			}
			return projects;
		}

		//*********************************************************************
		//
		// Retrieves a list of projects based on project membership
		//
		//*********************************************************************

		public static ProjectsCollection GetProjects(int queryUserID, int userID)
		{
			ProjectsCollection projects = new ProjectsCollection();
			DataSet ds = SqlHelper.ExecuteDataset(
				ConfigurationSettings.AppSettings[Web.Global.CfgKeyConnString], 
				"ListProjectsWithMembership", queryUserID, userID);

			foreach(DataRow r in ds.Tables[0].Rows)
			{
				Project prj = new Project();
				prj.ProjectID = Convert.ToInt32(r["ProjectID"]);
				prj.Name = r["ProjectName"].ToString();
				prj.Description = r["ProjectDescription"].ToString();
				prj.ManagerUserID = Convert.ToInt32(r["ProjectManagerId"]);
				prj.EstCompletionDate = Convert.ToDateTime(r["EstCompletionDate"]);
				prj.EstDuration = Convert.ToDecimal(r["EstDuration"]);
				projects.Add(prj);
			}
			return projects;
		}

  }
}
