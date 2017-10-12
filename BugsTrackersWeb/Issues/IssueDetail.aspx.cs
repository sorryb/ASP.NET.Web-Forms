using System;
using System.Web.UI;
using BugsTrackers.BusinessLogicLayer;

namespace BugsTrackers.Issues
{
    /// <summary>
    /// Summary description for IssueDetail.
    /// </summary>
    public partial class IssueDetail : System.Web.UI.Page
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
		// IssueDetail.aspx
		//
		// This page displays all the information about a single issue.
		//
		//*********************************************************************


		int ProjectId 
		{
			get 
			{
				if (ViewState["ProjectId"] == null)
					return 0;
				else
					return (int)ViewState["ProjectId"];
			}
			set { ViewState["ProjectId"] = value; }
		}



		int IssueId 
		{
			get 
			{
				if (ViewState["IssueId"] == null)
					return 0;
				else
					return (int)ViewState["IssueId"];
			}
			set { ViewState["IssueId"] = value; }
		}


		protected void Page_Load(Object s, EventArgs e) 
		{
			if (!Page.IsPostBack) 
			{
				// Get Project Id from Query String
				if (Request.QueryString["pid"] != null)
					ProjectId = Int32.Parse( Request.QueryString["pid"] );

				// Get Issue Id from Query String
				if (Request.QueryString["id"] != null)
					IssueId = Int32.Parse( Request.QueryString["id"] );

				// If don't know project or issue then redirect back
				if (ProjectId == 0 && IssueId == 0)
					Response.Redirect( "~/Issues/IssueList.aspx" );

				// Initialize for Adding or Editing
				if (IssueId == 0) 
				{
					BindOptions();
					ctlCustomFields.DataSource = CustomField.GetCustomFieldsByProjectId(ProjectId);
				} 
				else 
				{
					BindValues();
					ctlCustomFields.DataSource = CustomField.GetCustomFieldsByIssueId(IssueId);
				}
				ctlCustomFields.DataBind();
			}
			ctlIssueTabs.IssueId = IssueId;
			btnDelete.Attributes.Add("onclick", "return confirm('Are you sure you want to delete this issue?');");
		}


		protected void Page_PreRender(object sender, System.EventArgs e) 
		{
			if (IssueId != 0) 
			{
				lblIssueId.Text = IssueId.ToString();
				ctlIssueTabs.Visible = true;
				btnDelete.Enabled = true;
			}

			// Hide Edit Buttons for the Guest Role
			if (User.IsInRole("Guest")) 
			{
				btnSave.Enabled = false;
				btnDone.Enabled = false;
				btnCancel.Enabled = false;
				btnDelete.Enabled = false;
			}
		}



		void BindOptions() 
		{
			CategoryTree objCats = new CategoryTree();
			dropCats.DataSource = objCats.GetCategoryTreeByProjectId(ProjectId);
			dropCats.DataBind();

			ITUserCollection colUsers = ITUser.GetUsersByProjectId(ProjectId);

			dropAssigned.DataSource = colUsers;
			dropAssigned.DataBind();

			dropOwned.DataSource = colUsers;
			dropOwned.DataBind();

			dropStatus.DataSource = Status.GetStatusByProjectId(ProjectId);
			dropStatus.DataBind();

			dropPriority.DataSource = Priority.GetPrioritiesByProjectId(ProjectId);
			dropPriority.DataBind();


			dropMilestone.DataSource = Milestone.GetMilestoneByProjectId(ProjectId);
			dropMilestone.DataBind();

			lblDateCreated.Text = DateTime.Now.ToString("f");
		}


		void BindValues() 
		{
			Issue currentIssue = Issue.GetIssueById(IssueId);
			ProjectId = currentIssue.ProjectId;

			BindOptions();

			txtTitle.Text = currentIssue.Title;
			dropCats.SelectedValue = currentIssue.CategoryId;
			dropAssigned.SelectedValue = currentIssue.AssignedId;
			dropOwned.SelectedValue = currentIssue.OwnerId;
			dropStatus.SelectedValue = currentIssue.StatusId;
			dropPriority.SelectedValue = currentIssue.PriorityId;
			dropMilestone.SelectedValue = currentIssue.MilestoneId;
			lblCreator.Text = currentIssue.CreatorDisplayName;
			lblDateCreated.Text = currentIssue.DateCreated.ToString("f");
		}


		protected void btnSaveClick(Object s, EventArgs e) 
		{
			if (Page.IsValid)
				SaveIssue();
		}


		protected void btnDoneClick(Object s, EventArgs e) 
		{
			if (Page.IsValid && SaveIssue())
				Response.Redirect( "~/Issues/IssueList.aspx?pid=" + ProjectId.ToString() );
		}

		protected void CancelButtonClick(Object s, EventArgs e) 
		{
			Response.Redirect("~/Issues/IssueList.aspx?pid=" + ProjectId.ToString());

		}



		protected void DeleteButtonClick(Object s, EventArgs e) 
		{
			Issue.DeleteIssue(IssueId);
			Response.Redirect("~/Issues/IssueList.aspx?pid=" + ProjectId.ToString());
		}


		bool SaveIssue() 
		{
			Issue newIssue = new Issue(IssueId, ProjectId, txtTitle.Text, dropCats.SelectedValue, dropMilestone.SelectedValue, dropPriority.SelectedValue, dropStatus.SelectedValue, dropAssigned.SelectedValue, dropOwned.SelectedValue, User.Identity.Name);
			if (!newIssue.Save()) 
			{
				lblError.Text = "Could not save issue";
				return false;
			}

			IssueId = newIssue.Id;

			if (!CustomField.SaveCustomFieldValues(IssueId, ctlCustomFields.Values) ) 
			{
				lblError.Text = "Could not save issue custom fields";
				return false;
			}

            ITUser user = ITUser.GetUserByUsername(User.Identity.Name);

            AddEntryActivity(user.Id, ProjectId, dropCats.SelectedValue, DateTime.Now, txtTitle.Text, 1);

            return true;
		}

        private void AddEntryActivity(int userId,int projectId,int categoryId, DateTime date,string description, decimal hours)
        {
             BusinessLogicLayer.TimeEntry te = new BusinessLogicLayer.TimeEntry(0,
                userId,
                projectId,
                categoryId,
                date,
                ITSecurity.CleanStringRegex(description),
                hours
                );

            te.Save();

        }

	}
}
