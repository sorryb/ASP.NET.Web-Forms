namespace BugsTrackers.Issues
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using System.Collections;
	using BugsTrackers.UserInterfaceLayer;

	/// <summary>
	///		Summary description for IssueTabs.
	/// </summary>
	public partial class IssueTabs : System.Web.UI.UserControl
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
			this.lstTabs.ItemCommand += new System.Web.UI.WebControls.DataListCommandEventHandler(this.lstTabs_ItemCommand);
			this.lstTabs.ItemDataBound += new System.Web.UI.WebControls.DataListItemEventHandler(this.lstTabs_ItemDataBound);

		}
		#endregion

	
		//*********************************************************************
		//
		// IssueTabs.ascx
		//
		// This user control displays the tabs in the IssueDetail.aspx page.
		//
		//*********************************************************************


		private int _IssueId = 0;

		private Control contentControl;

		public int IssueId 
		{
			get { return _IssueId; }
			set { _IssueId = value; }
		}


		protected void Page_Load(object sender, System.EventArgs e) 
		{
			if (!Page.IsPostBack) 
			{
				ArrayList colTabs = new ArrayList();
				colTabs.Add( "Comments" );
				colTabs.Add( "Attachments" );
				colTabs.Add( "History" );
				colTabs.Add( "Notifications" );
				colTabs.Add( "Sub Issues" );
				colTabs.Add( "Parent Issues" );
				colTabs.Add( "Related Issues" );

				lstTabs.DataSource = colTabs;
				lstTabs.SelectedIndex = 0;
				lstTabs.DataBind();
			}

			LoadTab();
		}


		protected void Page_PreRender(object sender, System.EventArgs e) 
		{
			if (!Page.IsPostBack)
				((IIssueTab)contentControl).Initialize();
		}



		void lstTabs_ItemDataBound(Object s, DataListItemEventArgs e) 
		{
			LinkButton lnkTab = (LinkButton)e.Item.FindControl("lnkTab");
			lnkTab.Text = (string)e.Item.DataItem;
		}





		void lstTabs_ItemCommand(Object s, DataListCommandEventArgs e) 
		{
			lstTabs.SelectedIndex = e.Item.ItemIndex;
			LoadTab();
			((IIssueTab)contentControl).Initialize();
		}


		void LoadTab() 
		{
			string controlName = "Comments.ascx";

			switch (lstTabs.SelectedIndex) 
			{
				case 0:
					controlName = "Comments.ascx";
					break;
				case 1:
					controlName = "Attachments.ascx";
					break;
				case 2:
					controlName = "History.ascx";
					break;
				case 3:
					controlName = "Notifications.ascx";
					break;
				case 4:
					controlName = "SubIssues.ascx";
					break;
				case 5:
					controlName = "ParentIssues.ascx";
					break;
				case 6:
					controlName = "RelatedIssues.ascx";
					break;
			}

			contentControl = Page.LoadControl("~/Issues/UserControls/" + controlName);
			((IIssueTab)contentControl).IssueId = _IssueId;
			plhContent.Controls.Clear();
			plhContent.Controls.Add( contentControl );
			contentControl.ID = "ctlContent";
		}

	
	}
}
