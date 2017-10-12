namespace BugsTrackers.Issues.UserControls
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using BugsTrackers.BusinessLogicLayer;

	/// <summary>
	///		Summary description for RelatedIssues.
	/// </summary>
	public partial class RelatedIssues : System.Web.UI.UserControl
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
			this.grdIssues.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.grdIssuesItemCommand);
			this.grdIssues.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.grdIssuesItemDataBound);
			this.PreRender += new System.EventHandler(this.RelatedIssues_PreRender);

		}
		#endregion

	

		//*********************************************************************
		//
		// RelatedIssues.ascx
		//
		// This user control displays the Related Issues tab on the IssueDetail.aspx
		// page.
		//
		//*********************************************************************

		private int _IssueId = 0;


		public int IssueId 
		{
			get { return _IssueId; }
			set { _IssueId = value; }
		}


		public void Initialize() 
		{
			BindRelated();
		}

		protected void RelatedIssues_PreRender(object sender, System.EventArgs e)
		{
			if (Page.User.IsInRole("Guest"))
				btnAdd.Enabled = false;
		}



		void BindRelated() 
		{
			grdIssues.DataSource = RelatedIssue.GetRelatedIssues(_IssueId);
			grdIssues.DataKeyField = "IssueId";
			grdIssues.DataBind();
		}


		void grdIssuesItemDataBound(Object s, DataGridItemEventArgs e) 
		{
			if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem) 
			{
				RelatedIssue currentIssue = (RelatedIssue)e.Item.DataItem;

				Label lblIssueId = (Label)e.Item.FindControl( "lblIssueId" );
				lblIssueId.Text = currentIssue.IssueId.ToString();
			}
		}

		void grdIssuesItemCommand(Object s, DataGridCommandEventArgs e) 
		{
			int issueId = (int)grdIssues.DataKeys[e.Item.ItemIndex];
			RelatedIssue.DeleteRelatedIssue(_IssueId, issueId);
			BindRelated();
		}

		protected void AddRelatedIssue(Object s, EventArgs e) 
		{
			if (txtIssueId.Text == String.Empty)
				return;

			if (Page.IsValid) 
			{
				RelatedIssue.CreateNewRelatedIssue(_IssueId, Int32.Parse(txtIssueId.Text) );
				txtIssueId.Text = String.Empty;
				BindRelated();
			}
		}


	
	}
}
