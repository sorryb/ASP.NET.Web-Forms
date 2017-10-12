using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using BugsTrackers.BusinessLogicLayer;
using System.Web.Security;

namespace BugsTrackers.Administration.Users
{
	/// <summary>
	/// Summary description for UserDetail.
	/// </summary>
	public partial class UserDetail : System.Web.UI.Page
	{
	

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    

		}
		#endregion

	
		//*********************************************************************
		//
		// UserDetail.aspx
		//
		// This page is used by Administrators when adding or editing issue tracker
		// users.
		//
		//*********************************************************************


		public int UserId 
		{
			get 
			{
				if (ViewState["UserId"] == null)
					return 0;
				else
					return (int)ViewState["UserId"];
			}

			set { ViewState["UserId"] = value; }
		}



		public bool IsCurrentUser 
		{
			get 
			{
				if (ViewState["IsCurrentUser"] == null)
					return false;
				else
					return (bool)ViewState["IsCurrentUser"];
			}

			set { ViewState["IsCurrentUser"] = value; }
		}



		protected void Page_Load(object sender, System.EventArgs e) 
		{
			if (!Page.IsPostBack) 
			{
				dropRoles.DataSource = Role.GetAllRoles();
				dropRoles.DataTextField = "Name";
				dropRoles.DataBind();

				// Get User Id from query string
				if (Request.QueryString["id"] != null)
					UserId = Int32.Parse(Request.QueryString["id"]);

				// Display current user data when user ID is not 0
				if (UserId != 0) 
				{
					ITUser objUser = ITUser.GetUserById(UserId);
					txtUsername.Text = objUser.Username;
					dropRoles.SelectedValue = objUser.RoleName;
					txtEmail.Text = objUser.Email;
					if (String.Compare(User.Identity.Name, objUser.Username, true) == 0)
						IsCurrentUser = true;
				}

				// Hide password textboxes when not forms authentication
				if (User.Identity.AuthenticationType != "Forms")
					formsPassword.Visible = false;
			}

		}


		protected void SaveUser(Object s, EventArgs e) 
		{
			bool isCurrentUser = false;

			if (Page.IsValid) 
			{

				ITUser objUser = new ITUser();

				if (UserId != 0)
					objUser = ITUser.GetUserById(UserId);

				objUser.Username = txtUsername.Text;
				objUser.RoleName = dropRoles.SelectedItem.Text;
				objUser.Email = txtEmail.Text;

				if (txtPassword.Text != String.Empty)
					objUser.Password = txtPassword.Text;

				if (!objUser.Save()) 
				{
					lblError.Text = "Could not save user";
				} 
				else 
				{
					if (IsCurrentUser)
						FormsAuthentication.SignOut();

					Response.Redirect( "UserList.aspx" );
				}
			}
		}


		protected void CancelEdit(Object s, EventArgs e) 
		{
			Response.Redirect("UserList.aspx");
		}

	
	
	}
}
