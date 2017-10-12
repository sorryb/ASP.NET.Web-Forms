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
	///		Summary description for PickStatus.
	/// </summary>
	public partial class PickStatus : System.Web.UI.UserControl
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
		// PickStatus.ascx
		//
		// This user control displays a dropdown list of status values.
		//
		//*********************************************************************


		private StatusCollection _DataSource;
		private bool _DisplayDefault = false;



		public bool DisplayDefault 
		{
			get { return _DisplayDefault; }
			set { _DisplayDefault = value; }
		}


		public int SelectedValue 
		{
			get {return Int32.Parse(dropStatus.SelectedValue); }
			set 
			{
				try 
				{
					dropStatus.SelectedValue = value.ToString();
				} 
				catch {}
			}
		}



		public bool Enabled 
		{
			get { return dropStatus.Enabled; }
			set { dropStatus.Enabled = value; }
		}


		public StatusCollection DataSource 
		{
			get { return _DataSource; }
			set { _DataSource = value; }
		}

		public void DataBind() 
		{
			dropStatus.Items.Clear();
			dropStatus.DataSource = _DataSource;
			dropStatus.DataTextField = "Name";
			dropStatus.DataValueField = "Id";
			dropStatus.DataBind();
			if (_DisplayDefault)
				dropStatus.Items.Insert(0, new ListItem( "-- Select Status --", "0" ) );
		}



		public bool Required 
		{
			get { return reqVal.Visible; }
			set { reqVal.Visible = value; }
		}

	
	}
}
