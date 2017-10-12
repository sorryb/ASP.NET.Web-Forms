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
	///		Summary description for PickProject.
	/// </summary>
	public partial class PickProject : System.Web.UI.UserControl
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
		// PickProject.ascx
		//
		// This user control displays a dropdown list of projects.
		//
		//*********************************************************************
    
    
    
		private ProjectCollection _DataSource;
		private bool _DisplayDefault = false;
    
    
		public event EventHandler SelectedIndexChanged;
    
    
		public int SelectedValue 
		{
			get 
			{
				if (dropProjects.SelectedValue == String.Empty)
					return 0;
				return Int32.Parse(dropProjects.SelectedValue);
			}
			set { dropProjects.SelectedValue = value.ToString(); }
		}
    
    
		public ListItem SelectedItem 
		{
			get { return dropProjects.SelectedItem; }
		}
    
    
		public bool DisplayDefault 
		{
			get { return _DisplayDefault; }
			set { _DisplayDefault = value; }
		}
    
    
		public string CssClass 
		{
			get { return dropProjects.CssClass; }
			set { dropProjects.CssClass = value; }
		}
    
    
    
		public bool AutoPostBack 
		{
			get { return dropProjects.AutoPostBack; }
			set { dropProjects.AutoPostBack = value; }
		}
    
    
    
		public ProjectCollection DataSource 
		{
			get { return _DataSource; }
			set { _DataSource = value; }
		}
    
		public void DataBind() 
		{
			dropProjects.DataSource = _DataSource;
			dropProjects.DataTextField = "Name";
			dropProjects.DataValueField = "Id";
			dropProjects.DataBind();
			if (_DisplayDefault)
				dropProjects.Items.Insert(0, new ListItem( "-- Select Project --", "0" ) );
		}
    
    
		void OnSelectedIndexChanged(EventArgs e) 
		{
			if (SelectedIndexChanged != null) 
			{
				SelectedIndexChanged(this, e);
			}
		}
    
    
		protected void ProjectSelectedIndexChanged(Object s, EventArgs e) 
		{
			OnSelectedIndexChanged(e);
		}
    
    
		public void RemoveDefault() 
		{
			ListItem defaultItem = dropProjects.Items.FindByValue("0");
			if (defaultItem != null)
				dropProjects.Items.Remove(defaultItem);
		}


	
	}
}
