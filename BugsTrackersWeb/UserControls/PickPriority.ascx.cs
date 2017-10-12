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
	///		Summary description for PickPriority.
	/// </summary>
	public partial class PickPriority : System.Web.UI.UserControl
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
		// PickPriority.ascx
		//
		// This user control displays a dropdown list of Priorities.
		//
		//*********************************************************************


		private PriorityCollection _DataSource;
		private bool _DisplayDefault = false;


		public bool DisplayDefault 
		{
			get { return _DisplayDefault; }
			set { _DisplayDefault = value; }
		}



		public int SelectedValue 
		{
			get {return Int32.Parse(dropPriority.SelectedValue); }
			set 
			{
				try 
				{
					dropPriority.SelectedValue = value.ToString();
				} 
				catch {}
			}
		}



		public bool Enabled 
		{
			get { return dropPriority.Enabled; }
			set { dropPriority.Enabled = value; }
		}


		public PriorityCollection DataSource 
		{
			get { return _DataSource; }
			set { _DataSource = value; }
		}

		public void DataBind() 
		{
			dropPriority.Items.Clear();
			dropPriority.DataSource = _DataSource;
			dropPriority.DataTextField = "Name";
			dropPriority.DataValueField = "Id";
			dropPriority.DataBind();
			if (_DisplayDefault)
				dropPriority.Items.Insert(0, new ListItem( "-- Select Priority --", "0" ) );
		}


		public bool Required 
		{
			get { return reqVal.Visible; }
			set { reqVal.Visible = value; }
		}

	}
}
