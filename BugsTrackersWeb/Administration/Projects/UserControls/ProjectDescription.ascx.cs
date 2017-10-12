namespace BugsTrackers.Administration.Projects
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using BugsTrackers.BusinessLogicLayer;

	/// <summary>
	///		Summary description for ProjectDescription.
	/// </summary>
	public partial class ProjectDescription : System.Web.UI.UserControl
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
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{

		}
		#endregion

	
		//*********************************************************************
		//
		// ProjectDescription.ascx
		//
		// This user control is used by both the new project wizard and update
		// project page.
		//
		//*********************************************************************


		private int _ProjectId = -1;


		public int ProjectId 
		{
			get { return _ProjectId; }
			set { _ProjectId = value; }
		}


		public bool Update() 
		{
			if (Page.IsValid) 
			{
				Project newProject = new Project(ProjectId, txtName.Text.Trim(), txtDescription.Text.Trim(), dropProjectManager.SelectedValue, Page.User.Identity.Name );
				if (newProject.Save()) 
				{
					_ProjectId = newProject.Id;
					return true;
				} 
				else
					lblError.Text = "Could not save project";
			}
			return false;
		}


		public void Initialize() 
		{
			dropProjectManager.DataSource = ITUser.GetAllUsers();
			dropProjectManager.DataBind();

			if (ProjectId != -1) 
			{
				Project projectToUpdate = Project.GetProjectById(ProjectId);

				if (projectToUpdate != null) 
				{
					txtName.Text = projectToUpdate.Name;
					txtDescription.Text = projectToUpdate.Description;
					dropProjectManager.SelectedValue = projectToUpdate.ManagerId;
					dropProjectManager.RemoveDefault();
				}
			}

		}

	
	
	}
}
