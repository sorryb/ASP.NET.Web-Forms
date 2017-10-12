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
	///		Summary description for PickQuery.
	/// </summary>
	public partial class PickQuery : System.Web.UI.UserControl
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
		// PickQuery.ascx
		//
		// This user control displays a dropdown list of queries.
		//
		//*********************************************************************


		private QueryCollection _DataSource;
		private bool _DisplayDefault = false;


		public int ItemCount 
		{
			get{ return dropQueries.Items.Count; }
		}


		public int SelectedValue 
		{
			get {return Int32.Parse(dropQueries.SelectedValue); }
			set { dropQueries.SelectedValue = value.ToString(); }
		}

		public bool DisplayDefault 
		{
			get { return _DisplayDefault; }
			set { _DisplayDefault = value; }
		}


		public string CssClass 
		{
			get { return dropQueries.CssClass; }
			set { dropQueries.CssClass = value; }
		}


		public QueryCollection DataSource 
		{
			get { return _DataSource; }
			set { _DataSource = value; }
		}

		public void DataBind() 
		{
			dropQueries.Items.Clear();
			dropQueries.DataSource = _DataSource;
			dropQueries.DataTextField = "Name";
			dropQueries.DataValueField = "Id";
			dropQueries.DataBind();
			if (_DisplayDefault)
				dropQueries.Items.Insert(0, new ListItem( "-- Select Query --", "0" ) );
		}


	
	}
}
