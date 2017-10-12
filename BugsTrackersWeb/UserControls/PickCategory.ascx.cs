namespace BugsTrackers
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using BugsTrackers.BusinessLogicLayer;

	/// <summary>
	///		Summary description for PickCategory.
	/// </summary>
	public partial class PickCategory : System.Web.UI.UserControl
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
		// PickCategory.ascx
		//
		// This user control displays a dropdownlist of categories. The
		// category tree is returned by the CategoryTree.cs class.
		//
		//*********************************************************************


		private CategoryCollection _DataSource;
		private int _CatIndent = 1;
		private bool _DisplayDefault = false;


		public int ItemCount 
		{
			get { return dropCats.Items.Count; }
		}


		public bool DisplayDefault 
		{
			get { return _DisplayDefault; }
			set { _DisplayDefault = value; }
		}


		public bool Enabled 
		{
			get { return dropCats.Enabled; }
			set { dropCats.Enabled = value; }
		}


		public bool Required 
		{
			get { return reqVal.Visible; }
			set { reqVal.Visible = value; }
		}



		public int SelectedValue 
		{
			get {return Int32.Parse(dropCats.SelectedValue); }
			set 
			{
				try 
				{
					dropCats.SelectedValue = value.ToString();
				} 
				catch {}
			}
		}



		public CategoryCollection DataSource 
		{
			get { return _DataSource; }
			set { _DataSource = value; }
		}



		public void DataBind() 
		{
			dropCats.Items.Clear();

			if (_DataSource == null)
				return;

			dropCats.DataSource = _DataSource;
			dropCats.DataTextField = "Name";
			dropCats.DataValueField = "Id";
			dropCats.DataBind();

			// Display default project
			if (_DisplayDefault)
				dropCats.Items.Insert(0, new ListItem( "-- Select Category --", "0" ) );
			else
				dropCats.Items.Insert(0, new ListItem( "All Categories", "0" ) );
		}

	
	}
}
