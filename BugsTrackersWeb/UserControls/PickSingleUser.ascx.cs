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
	///		Summary description for PickSingleUser.
	/// </summary>
	public partial class PickSingleUser : System.Web.UI.UserControl
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
		// PickSingleUser.ascx
		//
		// This user control displays a dropdown list for selecting a single
		// user (The PickUser.ascx control enables you to pick multiple users).
		//
		//*********************************************************************



		private ITUserCollection _DataSource;
		private bool _DisplayDefault = false;


		public bool DisplayDefault 
		{
			get { return _DisplayDefault; }
			set { _DisplayDefault = value; }
		}



		public int SelectedValue 
		{
			get {return Int32.Parse(dropUsers.SelectedValue); }
			set {dropUsers.SelectedValue = value.ToString(); }
		}



		public ITUserCollection DataSource 
		{
			get { return _DataSource; }
			set { _DataSource = value; }
		}


		public void DataBind() 
		{
			dropUsers.Items.Clear();
			dropUsers.DataSource = _DataSource;
			dropUsers.DataTextField = "DisplayName";
			dropUsers.DataValueField = "Id";
			dropUsers.DataBind();
			if (_DisplayDefault)
				dropUsers.Items.Insert(0, new ListItem( "-- Select User --", "0" ) );
		}



		public void RemoveDefault() 
		{
			ListItem defaultItem = dropUsers.Items.FindByValue("0");
			if (defaultItem != null)
				dropUsers.Items.Remove(defaultItem);
		}



		public bool Required 
		{
			get { return reqVal.Visible; }
			set { reqVal.Visible = value; }
		}

	
	}
}
