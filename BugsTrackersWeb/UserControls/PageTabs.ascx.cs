namespace BugsTrackers.UserControls
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using System.Collections;
    using BugsTrackers.BusinessLogicLayer;
    using BugTrackerWebControls;

	/// <summary>
	///		Summary description for PageTabs.
	/// </summary>
	public partial class PageTabs : System.Web.UI.UserControl
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
            AsemblyInfo asembly = new AsemblyInfo();

            AsemblyVersion = asembly.AssemblyVersion;
		}
		#endregion

	
		//*********************************************************************
		//
		// PageTabs.ascx
		//
		// This user control displays the top-level page tabs displayed in
		// all the pages in the Issue Tracker.
		//
		//*********************************************************************


		public string CurrentTabName;
        public static string AsemblyVersion;


		public class PageTab 
		{
			public string Name = String.Empty;
			public string Url = String.Empty;

			public PageTab(string name, string url) 
			{
				Name = name;
				Url = url;
               
			}
         
		}




		protected void Page_Load(object sender, System.EventArgs e) 
		{
            if (!Page.User.Identity.IsAuthenticated)
                superFishMenu.MenuType = SuperFishMenu.SiteMapMenus.MenuSimple;
            else
            {
                if (Page.User.IsInRole("Administrator"))
                {
                    superFishMenu.MenuType = SuperFishMenu.SiteMapMenus.MenuAdmin;
                }
                else
                    superFishMenu.MenuType = SuperFishMenu.SiteMapMenus.MenuAcc;
            }

            superFishMenu.ServerMapPath = Server.MapPath("~");

			//if (Context.User.Identity.AuthenticationType == "Forms")
			//	lnkLogOff.Visible = true;

            //ArrayList colTabs = new ArrayList();
            //if (Page.User.Identity.IsAuthenticated) 
            //{
            //    colTabs.Add( new PageTab( "Taskuri", "~/Issues/IssueList.aspx" ) );
            //    colTabs.Add( new PageTab( "Cautare", "~/Queries/QueryList.aspx" ) );
            //    colTabs.Add( new PageTab( "Activitati", "~/DaylyActivities.aspx" ) );
            //    if (!Page.User.IsInRole("Administrator"))
            //        colTabs.Add(new PageTab("Rapoarte", "~/Reports/SelectReport.aspx"));				
            //}

            //if (Page.User.IsInRole("Administrator"))
            //{
            //        colTabs.Add( new PageTab( "Administrare", "~/Administration/Projects/ProjectList.aspx") ) ;
            //        colTabs.Add(new PageTab("Rapoarte", "~/Reports/SelectReportAdministrators.aspx"));
            //}

			//lstTabs.DataSource = colTabs;
			//lstTabs.DataBind();
		}

		public void lstTabs_ItemDataBound(Object s, DataListItemEventArgs e) 
		{
			PageTab objTab = (PageTab)e.Item.DataItem;
			HyperLink lnkTab = (HyperLink)e.Item.FindControl("lnkTab");

			lnkTab.Text = objTab.Name;
			lnkTab.NavigateUrl = objTab.Url;
			if (String.Compare(CurrentTabName, objTab.Name)==0)
				e.Item.CssClass = "tabActive";
			else
				e.Item.CssClass = "tabInactive";
		}


	
	}
}
