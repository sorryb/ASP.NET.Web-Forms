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

namespace BugsTrackers.Administration.Projects
{
	/// <summary>
	/// Summary description for EditProject.
	/// </summary>
	public partial class EditProject : System.Web.UI.Page
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
		// EditProject.aspx
		//
		// This page enables administrators to update a projects properties.
		// This page loads all of the user controls for editing a project (the
		// same controls used by the New Project Wizard).
		//
		//*********************************************************************


		int ProjectId 
		{
			get 
			{
				if (ViewState["ProjectId"] == null)
					return -1;
				else
					return (int)ViewState["ProjectId"];
			}

			set { ViewState["ProjectId"] = value; }
		}


		protected void Page_Load(object sender, System.EventArgs e) 
		{
			if (!Page.IsPostBack)
				ProjectId = Int32.Parse(Request.QueryString["id"]);

			ctlProjectDescription.ProjectId = ProjectId;
			ctlProjectMembers.ProjectId = ProjectId;
			ctlCategories.ProjectId = ProjectId;
			ctlProjectStatus.ProjectId = ProjectId;
			ctlPriority.ProjectId = ProjectId;
			ctlMilestone.ProjectId = ProjectId;
			ctlCustomFields.ProjectId = ProjectId;

			if (!Page.IsPostBack) 
			{
				ctlProjectDescription.Initialize();
				ctlProjectMembers.Initialize();
				ctlCategories.Initialize();
				ctlProjectStatus.Initialize();
				ctlPriority.Initialize();
				ctlMilestone.Initialize();
				ctlCustomFields.Initialize();
			}

			DeleteButton.Attributes.Add("onclick", "return confirm('Are you sure you want to delete this project?');");
		}


		protected void SaveButton_Click(Object s, EventArgs e) 
		{
			if (Page.IsValid) 
			{
				ctlProjectDescription.Update();
				Response.Redirect( "~/Administration/Projects/ProjectList.aspx" );
			}
		}


		protected void CancelButton_Click(Object s, EventArgs e) 
		{
			Response.Redirect( "~/Administration/Projects/ProjectList.aspx" );
		}



		protected void DeleteButton_Click(Object s, EventArgs e) 
		{
			Project.DeleteProject(ProjectId);
			Response.Redirect("~/Administration/Projects/ProjectList.aspx");
		}

	
	
	}
}
