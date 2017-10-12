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
	///		Summary description for Notifications.
	/// </summary>
	public partial class Notifications : System.Web.UI.UserControl
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
		// Notifications.ascx
		//
		// This user control displays the Notifications tab on the IssueDetail.aspx
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
			BindNotifications();
		}



		void BindNotifications() 
		{
			grdNotifications.DataSource = IssueNotification.GetIssueNotificationsByIssueId(_IssueId);
			grdNotifications.DataBind();
		}


		protected void AddNotification(Object s, EventArgs e) 
		{
			IssueNotification notify = new IssueNotification(IssueId, Page.User.Identity.Name);
			notify.Save();
			BindNotifications();
		}



		protected void DeleteNotification(Object s, EventArgs e) 
		{
			IssueNotification.DeleteIssueNotification(IssueId, Page.User.Identity.Name);
			BindNotifications();
		}

	
	}
}
