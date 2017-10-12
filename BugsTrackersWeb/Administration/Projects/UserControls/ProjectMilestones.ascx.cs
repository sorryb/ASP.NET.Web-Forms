namespace BugsTrackers.Administration.Projects
{
	using System;
	using System.Data;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using BugsTrackers.BusinessLogicLayer;

	/// <summary>
	///		Summary description for Milestone.
	/// </summary>
	public partial class ProjectMilestones : System.Web.UI.UserControl
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
			this.CustomValidator1.ServerValidate += new System.Web.UI.WebControls.ServerValidateEventHandler(this.ValidateMilestone);
			this.grdMilestones.DeleteCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DeleteMilestone);
			this.grdMilestones.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.grdMilestones_ItemDataBound);

		}
		#endregion

	
		//*********************************************************************
		//
		// Milestone.ascx
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
			if (Page.IsValid)
				return true;
			else
				return false;
		}



		public void Initialize() 
		{
			BindMilestones();
			lstImages.Initialize();
		}


		void BindMilestones() 
		{
			grdMilestones.DataSource = Milestone.GetMilestoneByProjectId(ProjectId);
			grdMilestones.DataKeyField="Id";
			grdMilestones.DataBind();

			if (grdMilestones.Items.Count == 0)
				grdMilestones.Visible = false;
			else
				grdMilestones.Visible = true;
		}


		protected void AddMilestone(Object s, EventArgs e) 
		{
			string newName = txtName.Text.Trim();

			if (newName == String.Empty)
				return;

			Milestone newMilestone = new Milestone(ProjectId, newName, lstImages.SelectedValue);
			if (newMilestone.Save()) 
			{
				txtName.Text = "";
				BindMilestones();
				lstImages.SelectedValue = String.Empty;
			} 
			else 
			{
				lblError.Text = "Could not save Milestone";
			}
		}


		void DeleteMilestone(Object s, DataGridCommandEventArgs e) 
		{
			int mileStoneId = (int)grdMilestones.DataKeys[e.Item.ItemIndex];

			if (!Milestone.DeleteMilestone(mileStoneId))
				lblError.Text = "Could not delete Milestone";
			else
				BindMilestones();
		}


		void grdMilestones_ItemDataBound(Object s, DataGridItemEventArgs e) 
		{
			if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem) 
			{
				Milestone currentMilestone = (Milestone)e.Item.DataItem;

				Label lblMilestoneName = (Label)e.Item.FindControl("lblMilestoneName");
				lblMilestoneName.Text = currentMilestone.Name;

				Image imgMilestone = (Image)e.Item.FindControl("imgMilestone");
				if (currentMilestone.ImageUrl == String.Empty) 
				{
					imgMilestone.Visible = false;
				} 
				else 
				{
					imgMilestone.ImageUrl = "~/Images/Milestone/" + currentMilestone.ImageUrl;
					imgMilestone.AlternateText = currentMilestone.Name;
				}

				Button btnDelete = (Button)e.Item.FindControl("btnDelete");
				btnDelete.Attributes.Add("onclick",String.Format("return confirm('Are you sure you want to delete the \"{0}\" Milestone?');", currentMilestone.Name));
			}
		}


		void ValidateMilestone(Object s, ServerValidateEventArgs e) 
		{
			if (grdMilestones.Items.Count > 0)
				e.IsValid = true;
			else
				e.IsValid = false;
		}

	
	}
}
