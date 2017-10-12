using System;
using System.Security.Principal;

namespace BugsTrackers.BusinessLogicLayer {

	//*********************************************************************
	//
	// CustomPrincipal Class
	//
	// The CustomPrincipal class implements the IPrincipal interface so it
	// can be used in place of the GenericPrincipal object.  Requirements for
	// implementing the IPrincipal interface include implementing the
	// IIdentity interface and an implementation for IsInRole.  The custom
	// principal is attached to the current request in Global.asax in the
	// Authenticate_Request event handler.  The user's role is stored in the
	// custom principal object in the Global_AcquireRequestState event handler.
	//
	//*********************************************************************

	public class CustomPrincipal : IPrincipal {

		private int		_userID;
		private string	_userRole = String.Empty;
		private string	_name;

		// Required to implement the IPrincipal interface.
		protected IIdentity _Identity;

		public CustomPrincipal() {}

		public CustomPrincipal(
			IIdentity identity,
			int userID,
			string userRole,
			string name)
		{
			_Identity = identity;
			_userID = userID;
			_userRole = userRole;
		}

		// IIdentity property used to retrieve the Identity object attached to
		// this principal.
		public IIdentity Identity {
			get	{ return _Identity; }
			set	{ _Identity = value; }
		}

		// The user's ID, created when the user was inserted into the database
		public int UserID {
			get { return _userID; }
			set { _userID = value; }
		}

		// The user's role, as defined in ITUser.
		public string UserRole {
			get { return _userRole; }
			set { _userRole = value; }
		}

		// The user's name (either First and Last name, or their network username)
		public string Name {
			get { return _name; }
			set { _name = value; }
		}

		//*********************************************************************
		//
		// Checks to see if the current user is a member of AT LEAST ONE of
		// the roles in the role string.  Returns true if found, otherwise false.
		// role is a comma-delimited list of role IDs.
		//
		//*********************************************************************

		public bool IsInRole(string role) {
			string [] roleArray = role.Split(new char[] {','});

			foreach (string r in roleArray) {
				if (_userRole == r)
					return true;
			}
			return false;
		}

	}
}
