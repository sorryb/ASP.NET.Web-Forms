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

namespace BugsTrackers.Issues
{
	/// <summary>
	/// Summary description for IssueList.
	/// </summary>
	public partial class IssueList : System.Web.UI.Page
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
			this.PreRender += new System.EventHandler(this.IssueList_PreRender);
		}
		#endregion

	
		//*********************************************************************
		//
		// IssueList.aspx
		//
		// This page enables a user to pick a project and display a list of
		// associated issues.
		//
		//*********************************************************************


		public void Page_Load(object sender, System.EventArgs e) 
		{
			if (!Page.IsPostBack) 
			{
				// Set Project ID from Query String
				if (Request.QueryString["pid"] != null) 
				{
					try 
					{
						dropProjects.SelectedValue = Int32.Parse(Request.QueryString["pid"]);
					} 
					catch {}
				}

				// Bind projects to dropdownlist
				dropProjects.DataSource = Project.GetProjectByMemberUsername(User.Identity.Name);
				dropProjects.DataBind();

				// If no projects, redirect to no project page
				if (dropProjects.SelectedValue == 0)
					Response.Redirect( "~/NoProjects.aspx" );

				lblProjectName.Text = dropProjects.SelectedItem.Text;

				BindIssues();
			}
		}

		

		protected void IssueList_PreRender(object sender, System.EventArgs e)
		{
			if (Page.User.IsInRole("Guest"))
				btnAdd.Enabled = false;
		}


		protected void ViewSelectedIndexChanged(Object s, EventArgs e) 
		{
			ctlDisplayIssues.CurrentPageIndex = 0;
			BindIssues();
		}


		protected void IssuesRebind(Object s, EventArgs e) 
		{
			BindIssues();
		}



		public void BindIssues() 
		{
			IssueCollection colIssues = null;

			int projectId = dropProjects.SelectedValue;

			switch (dropView.SelectedValue) 
			{
				case "Relevant":
					colIssues = Issue.GetIssuesByRelevancy(projectId, User.Identity.Name);
					break;
				case "Assigned":
					colIssues = Issue.GetIssuesByAssignedUsername(projectId, User.Identity.Name);
					break;
				case "Owned":
					colIssues = Issue.GetIssuesByOwnerUsername(projectId, User.Identity.Name);
					break;
				case "Created":
					colIssues = Issue.GetIssuesByCreatorUsername(projectId, User.Identity.Name);
					break;
				case "All":
					colIssues = Issue.GetIssuesByProjectId(projectId);
					break;
			}

			ctlDisplayIssues.DataSource = colIssues;
			ctlDisplayIssues.DataBind();

			// Display Current Project Name
			lblProjectName.Text = dropProjects.SelectedItem.Text;
		}




		protected void AddIssue(Object s, EventArgs e) 
		{
			Response.Redirect("~/Issues/IssueDetail.aspx?pid=" + dropProjects.SelectedValue);
		}


	
	}
}
