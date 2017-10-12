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
using System.Web.Security;
using BugsTrackers.BusinessLogicLayer;

namespace BugsTrackers
{
	/// <summary>
	/// Summary description for Register.
	/// </summary>
	public partial class Register : System.Web.UI.Page
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
		// Register.aspx
		//
		// This page enables users to register when Forms authentication is
		// enabled.
		//
		//*********************************************************************




		protected void SaveUser(Object s, EventArgs e) 
		{
			if (Page.IsValid) 
			{

				ITUser objUser = new ITUser();

				objUser.Username = txtUsername.Text;
				objUser.RoleName = Globals.DefaultRoleForNewUser;
				objUser.Email = txtEmail.Text;
				objUser.Password = txtPassword.Text;

				if (!objUser.Save())
					lblError.Text = "Could not save user";
				else 
				{
					FormsAuthentication.SetAuthCookie(txtUsername.Text,false);
					Response.Redirect( "~/Issues/IssueList.aspx" );
				}
			}
		}

	
	}
}
