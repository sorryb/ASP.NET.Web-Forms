using System;
using System.DirectoryServices;
using BugsTrackers.DataAccessLayer;
using System.Configuration;
using System.Data;


namespace BugsTrackers.BusinessLogicLayer {



	//*********************************************************************
	//
	// ITUser Class
	//
	// The ITUser class represents an Issue Tracker user.
	//
	//*********************************************************************


    public class ITUser {

        /** PRIVATE FIELDS **/

        private int _Id;
        private string _Username = String.Empty;
        private string _RoleName;
        private string _Email = String.Empty;
        private string _DisplayName = String.Empty;
        private string _Password = String.Empty;


		private const string _activeDirectoryPath = @"GC://";
		private const string _filter = "(&(ObjectClass=Person)(SAMAccountName={0}))";
		private const string _windowsSAMPath = @"WinNT://";
		private static string _path = String.Empty;


		//------

		public string Role;
		public string UserName;
		public string FirstName ;
		public string LastName ;
		//-----------------------
        /** CONSTRUCTORS **/

        public ITUser() {}


        public ITUser(string username) {
            _Username = username;
        }


        public ITUser(int userId, string username, string roleName) {
            _Id = userId;
            _Username = username;
            _RoleName = roleName;
        }




        public ITUser(int userId, string username, string roleName, string email, string displayName, string password) {
            _Id = userId;
            _Username = username;
            _RoleName = roleName;
            _Email = email;
            _DisplayName = displayName;
            _Password = password;
        }



        /** PROPERTIES **/


        public int Id {
            get { return _Id; }
        }
		/// <summary>
		/// UserID
		/// </summary>
		public int UserID 
		{
			get { return _Id; }
			 set { _Id = value; }
		}
	
        public string Username {
            get { return _Username; }
            set { _Username = value; }
        }

		public string Name 
		{
			get { return _Username; }
			set { _Username = value; }
		}

        public string DisplayName {
            get { return _DisplayName; }
            set { _DisplayName = value; }
        }


        public string RoleName {
            get { return _RoleName; }
            set { _RoleName = value; }
        }


        public string Email {
            get { return _Email; }
            set { _Email = value; }
        }


        public string Password {
            get { return _Password; }
            set { _Password = value; }
        }


        /** INSTANCE METHODS **/

        public bool Save () {
            // Get the Display Name
            if (DisplayName.Trim() == String.Empty)
                this.DisplayName = GetDisplayName(this.Username);

            DataAccessLayerBaseClass DBLayer = DataAccessLayerBaseClassHelper.GetDataAccessLayer();
            if (Id <= DefaultValues.GetUserIdMinValue()) {
                int TempId = DBLayer.CreateNewUser(this);
                if (TempId > 0) {
                    _Id = TempId;
                    return true;
                } else
                    return false;
            } else
                return (DBLayer.UpdateUser(this));
        }


        /** STATIC METHODS **/

        public static bool Authenticate(string username, string password) {
            if (username == null || username.Length==0)
                throw (new ArgumentOutOfRangeException("username"));

            if (password == null || password.Length==0)
                throw (new ArgumentOutOfRangeException("password"));

            DataAccessLayerBaseClass DBLayer = DataAccessLayerBaseClassHelper.GetDataAccessLayer();
            return (DBLayer.Authenticate(username, password));
        }





        public static ITUser GetUserByUsername(string username) {
            if (username == null ||username.Length==0)
                throw (new ArgumentOutOfRangeException("username"));

            DataAccessLayerBaseClass DBLayer = DataAccessLayerBaseClassHelper.GetDataAccessLayer();
            return (DBLayer.GetUserByUsername(username));
        }



        public static ITUser GetUserById(int userId) {
            DataAccessLayerBaseClass DBLayer = DataAccessLayerBaseClassHelper.GetDataAccessLayer();
            return (DBLayer.GetUserById(userId));
        }



        public static ITUserCollection GetUsersByProjectId(int projectId) {
            DataAccessLayerBaseClass DBLayer = DataAccessLayerBaseClassHelper.GetDataAccessLayer();
            return (DBLayer.GetUsersByProjectId(projectId));
        }



        public static ITUserCollection GetAllUsers() {
            DataAccessLayerBaseClass DBLayer = DataAccessLayerBaseClassHelper.GetDataAccessLayer();
            return (DBLayer.GetAllUsers());
        }


        public static ITUserCollection GetAllUsersByRoleName(string roleName) {
            DataAccessLayerBaseClass DBLayer = DataAccessLayerBaseClassHelper.GetDataAccessLayer();
            return (DBLayer.GetAllUsersByRoleName(roleName));
        }


        public static string GetDisplayName(string identification) {

			// Determine which method to use to retrieve user information

			// WindowsSAM
			if (Globals.UserAccountSource == "WindowsSAM") {
				// Extract the machine or domain name and the user name from the
				// identification string
				string [] samPath = identification.Split(new char[] {'\\'});
				_path = _windowsSAMPath + samPath[0];

				try	{
					// Find the user
					DirectoryEntry entryRoot = new DirectoryEntry(_path);
					DirectoryEntry userEntry = entryRoot.Children.Find(samPath[1], "user");
					return userEntry.Properties["FullName"].Value.ToString();
				} catch	{
					return identification;
				}
			}

			// Active Directory
			else if (Globals.UserAccountSource == "ActiveDirectory") {
				_path = _activeDirectoryPath;

				// Setup the filter
				identification = identification.Substring(identification.LastIndexOf(@"\") + 1,
					identification.Length - identification.LastIndexOf(@"\")-1);
				string userNameFilter = string.Format(_filter, identification);

				// Get a Directory Searcher to the LDAPPath
				DirectorySearcher searcher = new DirectorySearcher(_path);
				if (searcher == null) {
					return identification;
				}

				// Add the propierties that need to be retrieved
				searcher.PropertiesToLoad.Add("givenName");
				searcher.PropertiesToLoad.Add("sn");

				// Set the filter for the search
				searcher.Filter = userNameFilter;

				try {
					// Execute the search
					SearchResult search = searcher.FindOne();

					if (search != null) {
						string firstName = SearchResultProperty(search, "givenName");
						string lastName = SearchResultProperty(search, "sn");
						return firstName + " " + lastName;
					} else
						return identification;
				} catch	{
					return identification;
				}
			} else {
				// The user has not choosen an UserAccountSource or UserAccountSource as None
				// Usernames will be displayed as "Domain/Username"
				return identification;
			}


        }



		//*********************************************************************
		//
		// Retrieves the specified property from the SearchResult collection.
		//
		//*********************************************************************

		private static String SearchResultProperty(SearchResult sr,	string field) {
			if (sr.Properties[field] != null) {
				return (String)sr.Properties[field][0];
			}

			return null;
		}

		//*********************************************************************
		//
		// GetUsers Static Method
		// Retrieves a list of users based on the specified userID and role.
		// The list returned is restricted by role.  For instance, users with
		// the role of Administrator can see all users, while users with the
		// role of Consultant can only see themselves.
		//
		//*********************************************************************
		
		public static UsersCollection GetUsers(int userID, string role)
		{
			string firstName = string.Empty;
			string lastName = string.Empty;

			DataSet ds = SqlHelper.ExecuteDataset(ConfigurationSettings.AppSettings[Web.Global.CfgKeyConnString],
				"User_GetAllUsers"/*, userID, Convert.ToInt32(role)*/); 
			UsersCollection users = new UsersCollection();

			// Separate Data into a collection of Users.
			foreach(DataRow r in ds.Tables[0].Rows)
			{
				ITUser usr = new ITUser();
				usr.UserName = r["UserName"].ToString();
				usr.Role = r["RoleID"].ToString();
				usr.RoleName = r["RoleName"].ToString();
				usr.UserID = Convert.ToInt32(r["UserID"]);
				usr.Name = GetDisplayName(usr.UserName, ref firstName, ref lastName);
				usr.FirstName = firstName;
				usr.LastName = lastName;
				users.Add(usr);
			}
			return users;
		}

		//*********************************************************************
		//
		// GetDisplayName static method
		// Gets the user's first and last name from the specified TTUser account source, which is
		// set in Web.confg.
		//
		//*********************************************************************

		public static string GetDisplayName(string userName, ref string firstName, ref string lastName)
		{
			string displayName = string.Empty;
			string dbName = string.Empty;

			// The DirectoryHelper class will attempt to get the user's first 
			// and last name from the specified account source.
			DirectoryHelper.FindUser(userName, ref firstName, ref lastName);

			// If the first and last name could not be retrieved, return the TTUserName.
			if (firstName.Length > 0 || lastName.Length > 0)
			{
				displayName = firstName + " " + lastName;
			}
			else
			{
				dbName = GetDisplayNameFromDB(userName);
				if (dbName != string.Empty)
					displayName = dbName;
				else
					displayName = userName;
			}
			return displayName;
		}
		/// <summary>
		/// GetDisplayNameFromDB
		/// </summary>
		/// <param name="userName"></param>
		/// <returns></returns>
		public static string GetDisplayNameFromDB(string userName)
		{
			string displayName = string.Empty;
			displayName = Convert.ToString(SqlHelper.ExecuteScalar(ConfigurationSettings.AppSettings[Web.Global.CfgKeyConnString], 
				"GetUserDisplayName", userName));
			return displayName;
		}

    }
}
