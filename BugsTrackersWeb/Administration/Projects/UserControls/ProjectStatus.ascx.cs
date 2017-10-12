namespace BugsTrackers.Administration.Projects
{
	using System;
	using System.Data;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using BugsTrackers.BusinessLogicLayer;

	/// <summary>
	///		Summary description for Status.
	/// </summary>
	public partial class ProjectStatus : System.Web.UI.UserControl
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
			Trace.Warn("Initializing");
			this.CustomValidator1.ServerValidate += new System.Web.UI.WebControls.ServerValidateEventHandler(this.ValidateStatus);
			this.grdStatus.DeleteCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DeleteStatus);
			this.grdStatus.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.grdStatus_ItemDataBound);

		}
		#endregion

	
		//*********************************************************************
		//
		// Status.ascx
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
			BindStatus();
			lstImages.Initialize();
		}


		void BindStatus() 
		{
			grdStatus.DataSource = Status.GetStatusByProjectId(ProjectId);
			grdStatus.DataKeyField="Id";
			grdStatus.DataBind();

			if (grdStatus.Items.Count == 0)
				grdStatus.Visible = false;
			else
				grdStatus.Visible = true;
		}


		protected void AddStatus(Object s, EventArgs e) 
		{
			string newName = txtName.Text.Trim();

			if (newName == String.Empty)
				return;

			Status newStatus = new Status(ProjectId, newName, lstImages.SelectedValue);
			if (newStatus.Save()) 
			{
				txtName.Text = "";
				BindStatus();
				lstImages.SelectedValue = String.Empty;
			} 
			else 
			{
				lblError.Text = "Could not save status";
			}
		}


		void DeleteStatus(Object s, DataGridCommandEventArgs e) 
		{
			int statusId = (int)grdStatus.DataKeys[e.Item.ItemIndex];

			if (!Status.DeleteStatus(statusId))
				lblError.Text = "Could not delete status";
			else
				BindStatus();

		}


		void grdStatus_ItemDataBound(Object s, DataGridItemEventArgs e) 
		{
Trace.Warn("ItemDataBound");
			if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem) 
			{
				Status currentStatus = (Status)e.Item.DataItem;

				Label lblStatusName = (Label)e.Item.FindControl("lblStatusName");
				lblStatusName.Text = currentStatus.Name;

				Image imgStatus = (Image)e.Item.FindControl("imgStatus");
				if (currentStatus.ImageUrl == String.Empty) 
				{
					imgStatus.Visible = false;
				} 
				else 
				{
					imgStatus.ImageUrl = "~/Images/Status/" + currentStatus.ImageUrl;
					imgStatus.AlternateText = currentStatus.Name;
				}


				Button btnDelete = (Button)e.Item.FindControl("btnDelete");
				btnDelete.Attributes.Add("onclick",String.Format("return confirm('Are you sure you want to delete the \"{0}\" status?');", currentStatus.Name));
			}
		}



		void ValidateStatus(Object s, ServerValidateEventArgs e) 
		{
			if (grdStatus.Items.Count > 0)
				e.IsValid = true;
			else
				e.IsValid = false;
		}


	
	
	}
}
