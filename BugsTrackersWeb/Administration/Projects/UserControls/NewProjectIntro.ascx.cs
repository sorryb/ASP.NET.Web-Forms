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
	///		Summary description for NewProjectIntro.
	/// </summary>
	public partial class NewProjectIntro : System.Web.UI.UserControl
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
		// NewProjectIntro.ascx
		//
		// This user control is used by the new project wizard page.
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
			if (chkSkip.Checked) 
			{
				Response.Cookies[Globals.SkipProjectIntro].Value = "1";
				Response.Cookies[Globals.SkipProjectIntro].Path = "/";
				Response.Cookies[Globals.SkipProjectIntro].Expires = DateTime.MaxValue;
				Response.Redirect( "AddProject.aspx" );
			}
    
			return true;
		}
    
    
		public void Initialize() 
		{
		}


	
	}
}
