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

namespace BugsTrackers.Queries
{
	/// <summary>
	/// Summary description for QueryList.
	/// </summary>
	public partial class QueryList : System.Web.UI.Page
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
		// QueryList.aspx
		//
		// This page displays a list of existing queries.
		//
		//*********************************************************************


		protected void Page_Load(object sender, System.EventArgs e) 
		{
			if (!Page.IsPostBack) 
			{
				dropProjects.DataSource = Project.GetProjectByMemberUsername(User.Identity.Name);
				dropProjects.DataBind();

				if (dropProjects.SelectedValue == 0)
					Response.Redirect( "~/NoProjects.aspx" );

				BindQueries();
			}

			Button2.Attributes.Add("onclick", "return confirm('Sunteti sigur ca doriti sa stergeti clauza? ');");
		}

		void ProjectSelectedIndexChanged(Object s, EventArgs e) 
		{
			BindQueries();
		}



		protected void IssuesRebind(Object s, EventArgs e) 
		{
			ExecuteQuery();
		}



		void BindQueries() 
		{
			dropQueries.DataSource = Query.GetQueriesByUsername(User.Identity.Name,dropProjects.SelectedValue);
			dropQueries.DataBind();
		}

        public void BindQuerie(Object s, EventArgs e)
        {
            dropQueries.DataSource = Query.GetQueriesByUsername(User.Identity.Name, dropProjects.SelectedValue);
            dropQueries.DataBind();
        }
		
		protected void btnPerformQuery_Click(Object s, EventArgs e) 
		{
			ExecuteQuery();
		}

		void ExecuteQuery() 
		{
			if (dropQueries.SelectedValue == 0)
				return;


			try 
			{
				IssueCollection colIssues = Issue.PerformSavedQuery(dropProjects.SelectedValue,dropQueries.SelectedValue);
				ctlDisplayIssues.DataSource = colIssues;
			} 
			catch 
			{
				lblError.Text = "Error in query. Please check query values.";
			}

			ctlDisplayIssues.DataBind();
		}



		void AddQuery(Object s, EventArgs e) 
		{
			Response.Redirect( "QueryDetail.aspx" );
		}

		void DeleteQuery(Object s, EventArgs e) 
		{
			if (dropQueries.SelectedValue == 0)
				return;

			Query.DeleteQuery(dropQueries.SelectedValue);
			BindQueries();
		}

		protected void Button1_Click(object sender, System.EventArgs e)
		{
		Response.Redirect( "QueryDetail.aspx" );
		}

		protected void Button2_Click(object sender, System.EventArgs e)
		{
		
			if (dropQueries.SelectedValue == 0)
				return;

			Query.DeleteQuery(dropQueries.SelectedValue);
			BindQueries();
		}




   
}
}
