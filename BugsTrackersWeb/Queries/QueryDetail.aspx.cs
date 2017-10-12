using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using BugsTrackers.BusinessLogicLayer;

namespace BugsTrackers
{
	/// <summary>
	/// Summary description for QueryDetail.
	/// </summary>
	public partial class QueryDetail : System.Web.UI.Page
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
		// QueryDetail.aspx
		//
		// This page displays the interface for building a query against the
		// issues database.
		//
		//*********************************************************************





		//*********************************************************************
		//
		// ClauseCount Property
		//
		// The number of query clauses is stored in view state so that the
		// interface can be recreated on each page request.
		//
		//*********************************************************************

		int ClauseCount 
		{
			get 
			{
				if (ViewState["ClauseCount"] == null)
					return 3;
				else
					return (int)ViewState["ClauseCount"];
			}
			set { ViewState["ClauseCount"] = value; }
		}





		//*********************************************************************
		//
		// Page_Load Method
		//
		// Builds the user interface for selecting query fields.
		//
		//*********************************************************************


		protected void Page_Load(object sender, System.EventArgs e) 
		{
			DisplayClauses();

			if (!Page.IsPostBack) 
			{
				dropProjects.DataSource = Project.GetProjectByMemberUsername(User.Identity.Name);
				dropProjects.DataBind();

				if (dropProjects.SelectedValue == 0)
					Response.Redirect( "~/NoProjects.aspx" );

				BindQueryFieldTypes();
			}

		btnRemoveClause.Attributes.Add("onclick", "return confirm('Sunteti sigur ca doriti sa stergeti interogarea? ');");
		}



		//*********************************************************************
		//
		// IssueRebind Method
		//
		// When a user pages or sorts the issues displayed by the DisplayIssues
		// user control, this method is called. This method simply calls the ExecuteQuery()
		// method to rebind the DisplayIssues control to its datasource.
		//
		//*********************************************************************


		protected void IssuesRebind(Object s, EventArgs e) 
		{
			ExecuteQuery();
		}




		//*********************************************************************
		//
		// ProjectSelectedIndexChanged Method
		//
		// When a user picks a new project from the dropdown list, this method
		// is called. This method clears the current query clauses so the correct
		// values can be generated for the selected project.
		//
		//*********************************************************************


		protected void ProjectSelectedIndexChanged(Object s, EventArgs e) 
		{
			BindQueryFieldTypes();
			foreach (PickQueryField ctlPickQueryField in plhClauses.Controls)
				ctlPickQueryField.Clear();
		}



		//*********************************************************************
		//
		// DisplayClauses Method
		//
		// This method adds the number of clauses stored in the ClauseCount property.
		//
		//*********************************************************************


		void DisplayClauses() 
		{
			for (int i=0 ;i < ClauseCount; i++)
				AddClause(false);
		}





		//*********************************************************************
		//
		// BindQueryFieldTypes Method
		//
		// This method iterates through each of the query clauses and binds
		// the clause to the proper data.
		//
		//*********************************************************************

		void BindQueryFieldTypes() 
		{
			foreach (PickQueryField ctlPickQueryField in plhClauses.Controls) 
			{
				ctlPickQueryField.ProjectId = dropProjects.SelectedValue;
			}
		}





		//*********************************************************************
		//
		// AddClause Method
		//
		// This method adds a new query clause to the user interface.
		//
		//*********************************************************************

		void AddClause(bool bindData) 
		{
			//PickQueryField ctlPickQueryField = new PickQueryField();
			PickQueryField ctlPickQueryField = (PickQueryField)Page.LoadControl( "~/UserControls/PickQueryField.ascx");

			plhClauses.Controls.Add( ctlPickQueryField );
			ctlPickQueryField.ProjectId = dropProjects.SelectedValue;
		}





		//*********************************************************************
		//
		// btnAddClauseClick Method
		//
		// This method is called when a user clicks the Add Clause button.
		//
		//*********************************************************************

		protected void btnAddClauseClick(Object s, EventArgs e) 
		{
			ClauseCount ++;
			AddClause(true);
			btnRemoveClause.Enabled = true;
		}




		//*********************************************************************
		//
		// btnRemoveClauseClick Method
		//
		// This method is called when a user clicks the Remove Clause button.
		//
		//*********************************************************************


		protected void btnRemoveClauseClick(Object s, EventArgs e) 
		{
			if (ClauseCount > 1) 
			{
				ClauseCount --;
				plhClauses.Controls.RemoveAt(plhClauses.Controls.Count-1);
			}

			if (ClauseCount < 2)
				btnRemoveClause.Enabled = false;
		}



		//*********************************************************************
		//
		// btnRemoveClauseClick Method
		//
		// This method is called when a user clicks the Remove Clause button.
		//
		//******************************************************************

		protected void btnPerformQueryClick(Object s, EventArgs e) 
		{
			ctlDisplayIssues.CurrentPageIndex = 0;
			ExecuteQuery();
		}




		//*********************************************************************
		//
		// btnSaveQueryClick Method
		//
		// This method is called when a user clicks the Save Query button.
		// The method saves the query to a database table.
		//
		//******************************************************************

		protected void btnSaveQueryClick(Object s, EventArgs e) 
		{

			string queryName = txtQueryName.Text.Trim();

			if (queryName == String.Empty)
				return;

			QueryClauseCollection queryClauses = BuildQuery();
			if (queryClauses.Count == 0)
				return;


			bool success = Query.SaveQuery(User.Identity.Name, dropProjects.SelectedValue, queryName, queryClauses);
			if (success)
				Response.Redirect( "QueryList.aspx" );
			else
				lblSaveError.Text = "Could not save query";

		}




		//*********************************************************************
		//
		// ExecuteQuery Method
		//
		// This method executes a query and displays the results.
		//
		//******************************************************************

		void ExecuteQuery() 
		{

			QueryClauseCollection colQueryClauses = BuildQuery();

			if (colQueryClauses.Count > 0) 
			{
				try 
				{
					IssueCollection colIssues = Issue.PerformQuery(dropProjects.SelectedValue, colQueryClauses);
					ctlDisplayIssues.DataSource = colIssues;
				} 
				catch 
				{
					lblError.Text = "Error in query. Please check query values.";
				}

				ctlDisplayIssues.DataBind();
			}
		}


		//*********************************************************************
		//
		// BuildQuery Method
		//
		// This method builds a database query by iterating through each query clause.
		//
		//******************************************************************

		QueryClauseCollection BuildQuery() 
		{
			QueryClauseCollection colQueryClauses = new QueryClauseCollection();

			foreach (PickQueryField ctlPickQuery in plhClauses.Controls) 
			{
				QueryClause objQueryClause = ctlPickQuery.QueryClause;
				if (objQueryClause != null)
					colQueryClauses.Add( objQueryClause );
			}

			return colQueryClauses;
		}


	
	}
}
