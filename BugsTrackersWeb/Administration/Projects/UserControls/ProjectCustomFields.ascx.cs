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
	///		Summary description for ProjectCustomFields.
	/// </summary>
	public partial class ProjectCustomFields : System.Web.UI.UserControl
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
			this.grdCustomFields.DeleteCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DeleteCustomField);
			this.grdCustomFields.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.grdCustomFields_ItemDataBound);

		}
		#endregion

	
		//*********************************************************************
		//
		// ProjectCustomFields.ascx
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
			dropDataType.DataSource = Enum.GetNames(typeof(ValidationDataType));
			dropDataType.DataBind();
			BindCustomFields();
		}


		void BindCustomFields() 
		{
			grdCustomFields.DataSource = CustomField.GetCustomFieldsByProjectId(ProjectId);
			grdCustomFields.DataKeyField="Id";
			grdCustomFields.DataBind();

			if (grdCustomFields.Items.Count == 0)
				grdCustomFields.Visible = false;
			else
				grdCustomFields.Visible = true;
		}


		protected void AddCustomField(Object s, EventArgs e) 
		{
			string newName = txtName.Text.Trim();

			if (newName == String.Empty)
				return;

			ValidationDataType dataType = (ValidationDataType)Enum.Parse(typeof(ValidationDataType), dropDataType.SelectedValue);
			bool required = chkRequired.Checked;

			CustomField newCustomField = new CustomField(ProjectId, newName, dataType, required);
			if (newCustomField.Save()) 
			{
				txtName.Text = "";
				dropDataType.SelectedIndex = 0;
				chkRequired.Checked = false;
				BindCustomFields();
			} 
			else 
			{
				lblError.Text = "Could not save custom field";
			}
		}


		void DeleteCustomField(Object s, DataGridCommandEventArgs e) 
		{
			int customFieldId = (int)grdCustomFields.DataKeys[e.Item.ItemIndex];

			if (!CustomField.DeleteCustomField(customFieldId))
				lblError.Text = "Could not delete custom field";
			else
				BindCustomFields();
		}


		void grdCustomFields_ItemDataBound(Object s, DataGridItemEventArgs e) 
		{
			if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem) 
			{
				CustomField currentCustomField = (CustomField)e.Item.DataItem;

				Label lblName = (Label)e.Item.FindControl("lblName");
				lblName.Text = currentCustomField.Name;

				Label lblDataType = (Label)e.Item.FindControl("lblDataType");
				lblDataType.Text = currentCustomField.DataType.ToString();

				Label lblRequired = (Label)e.Item.FindControl("lblRequired");
				lblRequired.Text = currentCustomField.Required.ToString();

				Button btnDelete = (Button)e.Item.FindControl("btnDelete");
				btnDelete.Attributes.Add("onclick",String.Format("return confirm('Are you sure you want to delete the \"{0}\" field?');", currentCustomField.Name));
			}
		}

	
	
	}
}
