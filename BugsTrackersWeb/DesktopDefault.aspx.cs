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

namespace BugsTrackers
{
	/// <summary>
	/// Summary description for DesktopDefault.
	/// </summary>
	public partial class DesktopDefault : System.Web.UI.Page
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
		// DesktopDefault.aspx
		//
		// This page is the entry point into the Desktop version of the
		// Issue Tracker. If a user is authenticated, then the user is redirected
		// to the IssueList.aspx page. Otherwise, a Login form is displayed.
		//
		//*********************************************************************



		protected void Page_Load(object sender, System.EventArgs e) 
		{
			if (Request.IsAuthenticated)
				Response.Redirect("~/Issues/IssueList.aspx");
		}


		protected void Login(Object s, EventArgs e) 
		{
			if (Page.IsValid) 
			{
				if (ITUser.Authenticate(txtUsername.Text, txtPassword.Text))
					FormsAuthentication.RedirectFromLoginPage( txtUsername.Text, chkRemember.Checked );
				else
					lblError.Text = "Invalid username or password";
			}
		}


		protected void btnRegister_Click(object sender, System.EventArgs e)
		{
			Response.Redirect( "Register.aspx" );
		}

	
	}
}
