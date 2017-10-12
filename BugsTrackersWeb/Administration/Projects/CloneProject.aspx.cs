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
	/// Summary description for CloneProject.
	/// </summary>
	public partial class CloneProject : System.Web.UI.Page
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!IsPostBack) 
			{
				// Bind projects to dropdownlist
				dropProjects.DataSource = Project.GetAllProjects();
				dropProjects.DataBind();
			}
		}



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

		protected void btnClone_Click(object sender, System.EventArgs e)
		{
			if (IsValid) 
			{
				bool success = Project.CloneProject(dropProjects.SelectedValue, txtNewProjectName.Text);
				if (success)
					Response.Redirect( "ProjectList.aspx" );
				else
					lblError.Text = "Could not clone project";
			}
		
		}

		protected void btnCancel_Click(object sender, System.EventArgs e)
		{
			Response.Redirect( "ProjectList.aspx" );
		
		}
	}
}
