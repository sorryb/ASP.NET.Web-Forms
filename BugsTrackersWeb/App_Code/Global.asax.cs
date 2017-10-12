using System;
using System.Collections;
using System.ComponentModel;
using System.Web;
using System.Web.SessionState;
using System.Threading;
using System.Globalization;
using System.Web.Security;
using BugsTrackers.BusinessLogicLayer;


namespace BugsTrackers.Web 
{
	/// <summary>
	/// Summary description for Global.
	/// </summary>
	public class Global : System.Web.HttpApplication
	{
		// Configuation constants used for retrieving application setting values from the
		// web.config file.
		public const string CfgKeyConnString = "ConnectionString";
		public const string CfgKeyUserAcctSource = "UserAccountSource";
		public const string CfgKeyDefaultRole = "DefaultRoleForNewUser";
		public const string CfgKeyFirstDayOfWeek = "FirstDayOfWeek";
		public const string CfgBuildFilePath = "BuildFilePath";
		
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		public Global()
		{
			InitializeComponent();
		}	
		
			
		#region Web Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    
			this.components = new System.ComponentModel.Container();
		}
		#endregion

		
		//*********************************************************************
		//
		// Global.asax
		//
		// The Global.asax file is used to assign the custom roles to each
		// user for each page request. In addition, when Forms Authentication is
		// not enabled, the Global.asax automatically creates a new Issue Tracker
		// user for each new Windows user.
		//
		//*********************************************************************




		//*********************************************************************
		//
		// Application_BeginRequest Event
		//
		// The Application_BeginRequest method is an ASP.NET event that executes
		// on each web request into the application.
		//
		// The thread culture is set for each request using the language
		// settings specified in the browser.
		//
		//*********************************************************************

		protected void Application_BeginRequest(Object sender, EventArgs e) 
		{
			// Default to English if there are no user languages

			if (Request.UserLanguages != null)
				Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(Request.UserLanguages[0]);
			else
				Thread.CurrentThread.CurrentCulture = new CultureInfo("en-us");

			Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;
		}

		//*********************************************************************
		//
		// Application_AuthenticateRequest Event
		//
		// If the client is authenticated with the application, then determine
		// which security roles he/she belongs to and replace the "User" intrinsic
		// with a custom IPrincipal security object that permits "User.IsInRole"
		// role checks within the application
		//
		// Roles are cached in the browser in an in-memory encrypted cookie.  If the
		// cookie doesn't exist yet for this session, create it.
		//
		//*********************************************************************

		protected void Application_AuthenticateRequest(Object sender, EventArgs e) 
		{
			string userInformation = String.Empty;

			if (Request.IsAuthenticated == true) 
			{
				// Create the roles cookie if it doesn't exist yet for this session.
				if ((Request.Cookies[Globals.UserRoles] == null) || (Request.Cookies[Globals.UserRoles].Value == "")) 
				{
					// Retrieve the user's role and ID information and add it to
					// the cookie
					ITUser user = ITUser.GetUserByUsername(Context.User.Identity.Name);
					if (user == null) 
					{
						// The user was not found in the Issue Tracker database so add them using
						// the default role.  Specifying a UserID of 0 will result in the user being
						// inserted into the database.
						ITUser newUser = new ITUser(DefaultValues.GetUserIdMinValue(), Context.User.Identity.Name, Globals.DefaultRoleForNewUser);
						newUser.Save();
						user = newUser;
					}

					// Create a string to persist the role and user id
					userInformation = user.Id + ";" + user.RoleName + ";" + user.Username;

					// Create a cookie authentication ticket.
					FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(
						1,                              // version
						User.Identity.Name,             // user name
						DateTime.Now,                   // issue time
						DateTime.Now.AddHours(1),       // expires every hour
						false,                          // don't persist cookie
						userInformation
						);

					// Encrypt the ticket
					String cookieStr = FormsAuthentication.Encrypt(ticket);

					// Send the cookie to the client
					Response.Cookies[Globals.UserRoles].Value = cookieStr;
					Response.Cookies[Globals.UserRoles].Path = "/";
					Response.Cookies[Globals.UserRoles].Expires = DateTime.Now.AddMinutes(1);

					// Add our own custom principal to the request containing the user's identity, the user id, and
					// the user's role
					Context.User = new CustomPrincipal(User.Identity, user.Id, user.RoleName, user.Username);
				}
				else
				{
					// Get roles from roles cookie
					FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(Context.Request.Cookies[Globals.UserRoles].Value);
					userInformation = ticket.UserData;

					// Add our own custom principal to the request containing the user's identity, the user id, and
					// the user's role from the auth ticket
					string [] info = userInformation.Split( new char[] {';'} );
					Context.User = new CustomPrincipal(
						User.Identity,
						Convert.ToInt32(info[0].ToString()),
						info[1].ToString(),
						info[2].ToString());
				}
			}
		}

	
	}
}

