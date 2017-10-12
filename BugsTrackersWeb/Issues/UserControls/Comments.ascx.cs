namespace BugsTrackers.Issues
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using BugsTrackers.BusinessLogicLayer;

	/// <summary>
	///		Summary description for Comments.
	/// </summary>
	public partial class Comments : System.Web.UI.UserControl
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
			this.grdComments.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.grdCommentsPage);
			this.grdComments.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.grdCommentsItemDataBound);
			this.PreRender += new System.EventHandler(this.Comments_PreRender);

		}
		#endregion

	
		//*********************************************************************
		//
		// Comments.ascx
		//
		// This user control displays the Comments tab on the IssueDetail.aspx
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
			BindComments();
		}



		void BindComments() 
		{
			grdComments.DataSource = IssueComment.GetIssueCommentsByIssueId(_IssueId);
			grdComments.DataBind();
		}


		protected void AddComment(Object s, EventArgs e) 
		{
			if (Page.IsValid) 
			{
				IssueComment comment = new IssueComment(IssueId, txtComment.Text.Trim(), Page.User.Identity.Name);
				comment.Save();
				txtComment.Text = String.Empty;
				BindComments();
			}
		}

		void grdCommentsItemDataBound(Object s, DataGridItemEventArgs e) 
		{
			if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem) 
			{
				IssueComment currentComment = (IssueComment)e.Item.DataItem;

				Label lblCreatorDisplayName = (Label)e.Item.FindControl( "lblCreatorDisplayName" );
				lblCreatorDisplayName.Text = currentComment.CreatorDisplayName;

				Label lblDateCreated = (Label)e.Item.FindControl( "lblDateCreated" );
				lblDateCreated.Text = currentComment.DateCreated.ToString("f");

				Literal ltlComment = (Literal)e.Item.FindControl( "ltlComment" );
				ltlComment.Text = Server.HtmlEncode(currentComment.Comment);
			}
		}



		protected void grdCommentsPage(Object s, DataGridPageChangedEventArgs e) 
		{
			grdComments.CurrentPageIndex = e.NewPageIndex;
			BindComments();
		}


	
		protected void FontSizeChanged(Object s, EventArgs e) 
		{
			grdComments.Font.Size = FontUnit.Parse( dropFontSize.SelectedValue );
		}

		protected void Comments_PreRender(object sender, System.EventArgs e)
		{
			if (Page.User.IsInRole("Guest"))
				btnAdd.Enabled = false;
		}



	}
}
