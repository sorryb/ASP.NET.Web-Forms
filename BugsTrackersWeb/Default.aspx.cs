using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using BugsTrackers.BusinessLogicLayer;

namespace BugsTrackers
{
	/// <summary>
	/// Summary description for _Default.
	/// </summary>
	public partial class _Default : System.Web.UI.Page
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
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    
		}
		#endregion

	
		//*********************************************************************
		//
		// Default.aspx
		//
		// This page detects the browser type and redirects to either the
		// Desktop or Mobile version of the Issue Tracker.
		//
		//*********************************************************************
    
    
    
		public void Page_Load(Object sender, EventArgs e) 
		{
    
			if (Globals.IsMobileDevice) 
			{
    
				Response.Redirect(String.Format("{0}/Issues/IssueList.aspx", Globals.MobileDefaultUrl));
			}
			else 
			{
    
				Response.Redirect("~/Issues/IssueList.aspx");
			}
		}


	}
}
