namespace BugsTrackers.UserControls
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
    

	/// <summary>
	///		Summary description for Header.
	/// </summary>
	public partial class Header : System.Web.UI.UserControl
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
		// Header.ascx
		//
		// This user control displays the standard page header included in
		// every page in the Issue Tracker.
		//
		//*********************************************************************


		public string Title;
		public string TabName;

		protected void Page_Load(object sender, System.EventArgs e) 
		{
			ctlPageTabs.CurrentTabName = TabName;
		}


	
	}
}
