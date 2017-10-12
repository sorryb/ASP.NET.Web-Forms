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
	///		Summary description for ProjectMembers.
	/// </summary>
	public partial class ProjectMembers : System.Web.UI.UserControl
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
		// ProjectMembers.ascx
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
			return true;
		}

		public void Initialize() 
		{
			// Clear existing items
			lstAllUsers.Items.Clear();
			lstSelectedUsers.Items.Clear();

			// Get all users for All Users List Box
			lstAllUsers.DataSource = ITUser.GetAllUsers();
			lstAllUsers.DataTextField = "DisplayName";
			lstAllUsers.DataValueField = "Id";
			lstAllUsers.DataBind();

			// Copy selected users into Selected Users List Box
			ITUserCollection projectUsers = ITUser.GetUsersByProjectId(ProjectId);
			foreach (ITUser currentUser in projectUsers) 
			{
				ListItem matchItem = lstAllUsers.Items.FindByValue(currentUser.Id.ToString());
				if (matchItem != null) 
				{
					lstSelectedUsers.Items.Add(matchItem);
					lstAllUsers.Items.Remove(matchItem);
				}
			}

		}


		protected void AddUser(Object s, EventArgs e) 
		{
			if (lstAllUsers.SelectedIndex > -1) 
			{
				if (Project.AddUserToProject( Int32.Parse(lstAllUsers.SelectedValue), ProjectId ) ) 
				{
					lstSelectedUsers.SelectedIndex = -1;
					lstSelectedUsers.Items.Add( lstAllUsers.SelectedItem );
					lstAllUsers.Items.Remove(lstAllUsers.SelectedItem);
				}
			}
		}


		protected void RemoveUser(Object s, EventArgs e) 
		{
			if (lstSelectedUsers.SelectedIndex > -1) 
			{
				if (Project.RemoveUserFromProject( Int32.Parse(lstSelectedUsers.SelectedValue), ProjectId ) ) 
				{
					lstAllUsers.SelectedIndex = -1;
					lstAllUsers.Items.Add( lstSelectedUsers.SelectedItem );
					lstSelectedUsers.Items.Remove(lstSelectedUsers.SelectedItem);
				}
			}
		}

		protected void Page_Load(object sender, System.EventArgs e)
		{
		
		}

	
	
	}
}
