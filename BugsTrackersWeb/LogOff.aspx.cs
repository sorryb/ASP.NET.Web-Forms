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
	/// Summary description for LogOff.
	/// </summary>
	public partial class LogOff : System.Web.UI.Page
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
		// Logoff.aspx
		//
		// This page is used when signing off from the issue tracker.
		//
		//*********************************************************************

		protected void Page_Load(object sender, System.EventArgs e) 
		{
			FormsAuthentication.SignOut();

			// Invalidate roles token
			Response.Cookies[Globals.UserRoles].Value = "";
			Response.Cookies[Globals.UserRoles].Path = "/";
			Response.Cookies[Globals.UserRoles].Expires = new System.DateTime(1999, 10, 12);

			Context.User = null;
			Response.Redirect("~/Issues/IssueList.aspx");
		}


	
	}
}
