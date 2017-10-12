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
	///		Summary description for PickMilestone.
	/// </summary>
	public partial class PickMilestone : System.Web.UI.UserControl
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
		// PickMilestone.ascx
		//
		// This user control displays a dropdown list of Milestones.
		//
		//*********************************************************************


		private MilestoneCollection _DataSource;
		private bool _DisplayDefault = false;



		public int SelectedValue 
		{
			get {return Int32.Parse(dropMilestone.SelectedValue); }
			set 
			{
				try 
				{
					dropMilestone.SelectedValue = value.ToString();
				} 
				catch {}
			}
		}


		public bool DisplayDefault 
		{
			get { return _DisplayDefault; }
			set { _DisplayDefault = value; }
		}



		public bool Enabled 
		{
			get { return dropMilestone.Enabled; }
			set { dropMilestone.Enabled = value; }
		}


		public MilestoneCollection DataSource 
		{
			get { return _DataSource; }
			set { _DataSource = value; }
		}

		public void DataBind() 
		{
			dropMilestone.Items.Clear();
			dropMilestone.DataSource = _DataSource;
			dropMilestone.DataTextField = "Name";
			dropMilestone.DataValueField = "Id";
			dropMilestone.DataBind();
			if (_DisplayDefault)
				dropMilestone.Items.Insert(0, new ListItem( "-- Select Milestone --", "0" ) );
		}



		public bool Required 
		{
			get { return reqVal.Visible; }
			set { reqVal.Visible = value; }
		}


	}
}
