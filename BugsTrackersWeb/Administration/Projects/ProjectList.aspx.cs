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
	/// Summary description for ProjectList.
	/// </summary>
	public partial class ProjectList : System.Web.UI.Page
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
			this.ProjectsGrid.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.ProjectsGrid_Page);
			this.ProjectsGrid.SortCommand += new System.Web.UI.WebControls.DataGridSortCommandEventHandler(this.ProjectsGrid_Sort);
			this.PreRender += new System.EventHandler(this.ProjectList_PreRender);

		}
		#endregion

	
		//*********************************************************************
		//
		// ProjectList.aspx
		//
		// This page displays all of the projects.
		//
		//*********************************************************************


		protected void Page_Load(object sender, System.EventArgs e) 
		{

			if (!Page.IsPostBack)
				BindProjects();
		}


		protected void ProjectList_PreRender(object sender, System.EventArgs e)
		{
			if (Project.SupportsProjectCloning)
				btnCloneProject.Visible = true;
		}


		//*********************************************************************
		//
		// The BindProjects method retrieves the list of projects the current user
		// can view based on their role and then databinds them to the ProjectsGrid
		//
		//*********************************************************************

		private void BindProjects()
		{
			ProjectCollection projectList = Project.GetAllProjects();

			// Call method to sort the data before databinding
			SortGridData(projectList, SortField, SortAscending);

			ProjectsGrid.DataSource = projectList;
			ProjectsGrid.DataBind();
		}

		//*********************************************************************
		//
		// The SortGridData method is a helper method called when databinding the Projects grid
		// to sort the columns of the grid.
		//
		//*********************************************************************


		private void SortGridData(ProjectCollection list, string sortField, bool asc) 
		{
			ProjectCollection.ProjectFields sortCol = ProjectCollection.ProjectFields.InitValue;

			switch(sortField) 
			{
				case "Name":
					sortCol = ProjectCollection.ProjectFields.Name;
					break;
				case "Manager":
					sortCol = ProjectCollection.ProjectFields.Manager;
					break;
				case "Creator":
					sortCol = ProjectCollection.ProjectFields.Creator;
					break;
				case "DateCreated":
					sortCol = ProjectCollection.ProjectFields.DateCreated;
					break;
			}

			list.Sort(sortCol, asc);
		}


		//*********************************************************************
		//
		// The ProjectsGrid_Page event handler sets the index of the currently displayed
		// page for the Projects grid and re-binds it.
		//
		//*********************************************************************

		protected void ProjectsGrid_Page(Object sender, DataGridPageChangedEventArgs e) 
		{
			ProjectsGrid.CurrentPageIndex = e.NewPageIndex;

			BindProjects();
		}

		//*********************************************************************
		//
		// The ProjectGrid_Sort event handler changes the sortfield for the Projects grid
		// and re-binds it.
		//
		//*********************************************************************

		private void ProjectsGrid_Sort(object source, System.Web.UI.WebControls.DataGridSortCommandEventArgs e) 
		{
			SortField = e.SortExpression;

			BindProjects();
		}

		string SortField 
		{
			get 
			{
				object o = ViewState["SortField"];
				if (o == null) 
				{
					return String.Empty;
				}
				return (string)o;
			}

			set 
			{
				if (value == SortField) 
				{
					// same as current sort file, toggle sort direction
					SortAscending = !SortAscending;
				}
				ViewState["SortField"] = value;
			}
		}

		//*********************************************************************
		//
		// SortAscending property is tracked in ViewState
		//
		//*********************************************************************

		bool SortAscending
		{
			get 
			{
				object o = ViewState["SortAscending"];
				if (o == null) 
				{
					return true;
				}
				return (bool)o;
			}

			set 
			{
				ViewState["SortAscending"] = value;
			}
		}



		protected void AddProject(Object s, EventArgs e) 
		{
			Response.Redirect("AddProject.aspx");

		}


		protected void btnCloneProject_Click(object sender, System.EventArgs e)
		{
			Response.Redirect("CloneProject.aspx");
		}

	
	
	}
}
