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
	///		Summary description for Categories.
	/// </summary>
	public partial class ProjectCategories : System.Web.UI.UserControl
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
			this.CustomValidator1.ServerValidate += new System.Web.UI.WebControls.ServerValidateEventHandler(this.ValidateCategories);

		}
		#endregion

	
	
		//*********************************************************************
		//
		// Categories.ascx
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
			BindCategories();
		}


		void BindCategories() 
		{
			CategoryTree objCats = new CategoryTree();
			dropCats.DataSource = objCats.GetCategoryTreeByProjectId(ProjectId);
			dropCats.DataBind();
		}



		protected void btnAddCategory(Object s, EventArgs e) 
		{
			if (txtName.Text.Trim() != String.Empty) 
			{
				Category newCat = new Category(ProjectId, dropCats.SelectedValue, txtName.Text);
				if (!newCat.Save())
					lblError.Text = "Could not save new category";
				else 
				{
					txtName.Text = String.Empty;
					BindCategories();
				}
			}
		}


		protected void btnDeleteCategory(Object s, EventArgs e) 
		{
			// Don't delete All Categories
			if (dropCats.SelectedValue == 0)
				return;

			if (!Category.DeleteCategory(dropCats.SelectedValue))
				lblError.Text = "Could not delete category";
			else
				BindCategories();
		}

		void ValidateCategories(Object s, ServerValidateEventArgs e) 
		{
			if (dropCats.ItemCount == 1)
				e.IsValid = false;
			else
				e.IsValid = true;
		}

	
	}
}
