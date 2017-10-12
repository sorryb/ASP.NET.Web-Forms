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
	///		Summary description for History.
	/// </summary>
	public partial class History : System.Web.UI.UserControl
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
			this.grdHistory.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.grdHistoryItemDataBound);

		}
		#endregion

	
		//*********************************************************************
		//
		// History.ascx
		//
		// This user control displays the History tab on the IssueDetail.aspx
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
		}


		protected void Page_PreRender(object sender, System.EventArgs e) 
		{
			BindHistory();
		}


		void BindHistory() 
		{
			grdHistory.DataSource = IssueHistory.GetIssueHistoryByIssueId(_IssueId);
			grdHistory.DataBind();
		}


		void grdHistoryItemDataBound(Object s, DataGridItemEventArgs e) 
		{
			if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem) 
			{
				IssueHistory currentHistory = (IssueHistory)e.Item.DataItem;

				Label lblCreatorDisplayName = (Label)e.Item.FindControl( "lblCreatorDisplayName" );
				lblCreatorDisplayName.Text = currentHistory.CreatorDisplayName;

				Label lblDateCreated = (Label)e.Item.FindControl( "lblDateCreated" );
				lblDateCreated.Text = currentHistory.DateCreated.ToString("f");
			}
		}

		protected void Page_PreRender()
		{
		
		}

	
	}
}
