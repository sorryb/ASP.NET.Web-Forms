namespace BugsTrackers.Administration.Projects
{
	using System;
	using System.Data;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using BugsTrackers.BusinessLogicLayer;

	/// <summary>
	///		Summary description for Priority.
	/// </summary>
	public partial class ProjectPriorities : System.Web.UI.UserControl
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
			this.CustomValidator1.ServerValidate += new System.Web.UI.WebControls.ServerValidateEventHandler(this.ValidatePriority);
			this.grdPriorities.DeleteCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DeletePriority);
			this.grdPriorities.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.grdPriorities_ItemDataBound);

		}
		#endregion

	
		//*********************************************************************
		//
		// Priority.ascx
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
			BindPriorities();
			lstImages.Initialize();
		}


		void BindPriorities() 
		{
			grdPriorities.DataSource = Priority.GetPrioritiesByProjectId(ProjectId);
			grdPriorities.DataKeyField="Id";
			grdPriorities.DataBind();

			if (grdPriorities.Items.Count == 0)
				grdPriorities.Visible = false;
			else
				grdPriorities.Visible = true;
		}


		protected void AddPriority(Object s, EventArgs e) 
		{
			string newName = txtName.Text.Trim();

			if (newName == String.Empty)
				return;

			Priority newPriority = new Priority(ProjectId, newName, lstImages.SelectedValue);
			if (newPriority.Save()) 
			{
				txtName.Text = "";
				lstImages.SelectedValue = String.Empty;
				BindPriorities();
			} 
			else 
			{
				lblError.Text = "Could not save priority";
			}
		}


		void DeletePriority(Object s, DataGridCommandEventArgs e) 
		{
			int priorityId = (int)grdPriorities.DataKeys[e.Item.ItemIndex];

			if (!Priority.DeletePriority(priorityId))
				lblError.Text = "Could not delete priority";
			else
				BindPriorities();
		}


		void grdPriorities_ItemDataBound(Object s, DataGridItemEventArgs e) 
		{
			if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem) 
			{
				Priority currentPriority = (Priority)e.Item.DataItem;

				Label lblPriorityName = (Label)e.Item.FindControl("lblPriorityName");
				lblPriorityName.Text = currentPriority.Name;

				Image imgPriority = (Image)e.Item.FindControl("imgPriority");
				if (currentPriority.ImageUrl == String.Empty) 
				{
					imgPriority.Visible = false;
				} 
				else 
				{
					imgPriority.ImageUrl = "~/Images/Priority/" + currentPriority.ImageUrl;
					imgPriority.AlternateText = currentPriority.Name;
				}

				Button btnDelete = (Button)e.Item.FindControl("btnDelete");
				btnDelete.Attributes.Add("onclick",String.Format("return confirm('Are you sure you want to delete the \"{0}\" priority?');", currentPriority.Name));
			}
		}


		void ValidatePriority(Object s, ServerValidateEventArgs e) 
		{
			if (grdPriorities.Items.Count > 0)
				e.IsValid = true;
			else
				e.IsValid = false;
		}

	
	
	
	}
}
