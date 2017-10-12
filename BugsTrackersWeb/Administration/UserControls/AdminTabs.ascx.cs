namespace BugsTrackers.Administration.UserControls
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using System.Collections;

	/// <summary>
	///		Summary description for AdminTabs.
	/// </summary>
	public partial class AdminTabs : System.Web.UI.UserControl
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
			this.lstTabs.ItemDataBound += new System.Web.UI.WebControls.DataListItemEventHandler(this.lstTabs_ItemDataBound);

		}
		#endregion

	
		//*********************************************************************
		//
		// AdminTabs.ascx
		//
		// This user control displays the Administration tabs shown in all
		// of the Administration pages.
		//
		//*********************************************************************


		public string CurrentTabName;


		public class AdminTab 
		{
			public string Name = String.Empty;
			public string Url = String.Empty;

			public AdminTab(string name, string url) 
			{
				Name = name;
				Url = url;
			}
		}




		protected void Page_Load(object sender, System.EventArgs e) 
		{
			ArrayList colTabs = new ArrayList();
			colTabs.Add( new AdminTab( "Proiecte", "~/Administration/Projects/ProjectList.aspx" ) );
			colTabs.Add( new AdminTab( "Utilizatori", "~/Administration/Users/UserList.aspx") ) ;
			colTabs.Add( new AdminTab( "Utilitare", "~/Administration/Utilities/Build.aspx") ) ;

			lstTabs.DataSource = colTabs;
			lstTabs.DataBind();
		}

		void lstTabs_ItemDataBound(Object s, DataListItemEventArgs e) 
		{
			AdminTab objTab = (AdminTab)e.Item.DataItem;
			HyperLink lnkTab = (HyperLink)e.Item.FindControl("lnkTab");

			lnkTab.Text = objTab.Name;
			lnkTab.NavigateUrl = objTab.Url;
			if (String.Compare(CurrentTabName, objTab.Name)==0)
				e.Item.CssClass = "adminTabActive";
			else
				e.Item.CssClass = "adminTabInactive";
		}

	
	
	}
}
