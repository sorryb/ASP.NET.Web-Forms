
namespace BugsTrackers
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using BugsTrackers.BusinessLogicLayer;
    using System.Web.UI;
    using BugsTrackers;
    

	/// <summary>
	///		Summary description for DisplayIssues.
	/// </summary>
	public partial class DisplayIssues : System.Web.UI.UserControl
	{


		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
			Page_Init();
		}
		
		/// <summary>
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.grdIssues.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.IssuesPageIndexChanged);
			this.grdIssues.SortCommand += new System.Web.UI.WebControls.DataGridSortCommandEventHandler(this.IssuesSortCommand);

		}
		#endregion

		
		//*********************************************************************
		//
		// DisplayIssues.ascx
		//
		// This user control displays a list of issues. It is used in the
		// IssueList.aspx, QueryDetail.aspx, and QueryList.aspx pages.
		//
		//*********************************************************************
    
    
    
		private IssueCollection _DataSource;
		private string[] _arrIssueColumns = new string[] {"3","4","5","6","7","8"};
    
		public event EventHandler RebindCommand;
    
    
    
		protected void Page_Init() 
		{
			if ((Request.Cookies[Globals.IssueColumns] != null) && (Request.Cookies[Globals.IssueColumns].Value != String.Empty))
				_arrIssueColumns  = Request.Cookies[Globals.IssueColumns].Value.Split();
    
		}
    
    
    
		void OnRebindCommand(EventArgs e) 
		{
			if (RebindCommand != null)
				RebindCommand(this, e);
		}
    
    
    
    
    
		void IssuesPageIndexChanged(Object s, DataGridPageChangedEventArgs e) 
		{
			grdIssues.CurrentPageIndex = e.NewPageIndex;
			OnRebindCommand(EventArgs.Empty);
		}
    
    
    
    
		void IssuesSortCommand(Object s, DataGridSortCommandEventArgs e) 
		{
			SortField = e.SortExpression;
			OnRebindCommand(EventArgs.Empty);
		}
    
    
		public int CurrentPageIndex 
		{
			get { return grdIssues.CurrentPageIndex; }
			set { grdIssues.CurrentPageIndex = value; }
		}
    
    
		public IssueCollection DataSource 
		{
			get { return _DataSource; }
			set { _DataSource = value; }
		}
    
		public void DataBind() 
		{
			SortGridData(_DataSource, SortField, SortAscending);
			grdIssues.DataSource = _DataSource;
			grdIssues.DataBind();
    
			if (grdIssues.Items.Count > 0) 
			{
				grdIssues.Visible = true;
				tblOptions.Visible = true;
				lblNoIssues.Visible = false;
				DisplayColumns();
			} 
			else 
			{
				grdIssues.Visible = false;
				tblOptions.Visible = false;
				lblNoIssues.Visible = true;
			}
    
		}
    
    
    
		void DisplayColumns() 
		{
			// Hide all the DataGrid columns
			for (int index = 2;index < grdIssues.Columns.Count;index++)
				grdIssues.Columns[index].Visible = false;
    
			// Display columns based on the _arrIssueColumns array (retrieved from cookie)
			foreach (string colIndex in _arrIssueColumns)
				grdIssues.Columns[Int32.Parse(colIndex)].Visible = true;
		}
    
     
    
    
		//*******************************************************
		//
		// SortGridData methods sorts the Issues Grid based on which
		// sort field is being selected.  Also does reverse sorting based on the boolean.
		//
		//*******************************************************
    
		private void SortGridData(IssueCollection list, string sortField, bool asc) 
		{
			IssueCollection.IssueFields sortCol = IssueCollection.IssueFields.InitValue;
    
			switch(sortField) 
			{
				case "Id":
					sortCol = IssueCollection.IssueFields.Id;
					break;
				case "Title":
					sortCol = IssueCollection.IssueFields.Title;
					break;
				case "Category":
					sortCol = IssueCollection.IssueFields.Category;
					break;
				case "Assigned":
					sortCol = IssueCollection.IssueFields.Assigned;
					break;
				case "Owner":
					sortCol = IssueCollection.IssueFields.Owner;
					break;
				case "Creator":
					sortCol = IssueCollection.IssueFields.Creator;
					break;
				case "Priority":
					sortCol = IssueCollection.IssueFields.Priority;
					break;
				case "Status":
					sortCol = IssueCollection.IssueFields.Status;
					break;
				case "Milestone":
					sortCol = IssueCollection.IssueFields.Milestone;
					break;
				case "Created":
					sortCol = IssueCollection.IssueFields.Created;
					break;
			}
    
			if (list != null)
				list.Sort(sortCol, asc);
		}
    
    
    
		string SortField 
		{
			get 
			{
				object o = ViewState["SortField"];
				if (o == null) 
				{
					return String.Empty;
				}
				return (string)o;
			}
    
			set 
			{
				if (value == SortField) 
				{
					// same as current sort file, toggle sort direction
					SortAscending = !SortAscending;
				}
				ViewState["SortField"] = value;
			}
		}
    
		//*********************************************************************
		//
		// SortAscending property is tracked in ViewState
		//
		//*********************************************************************
    
		bool SortAscending 
		{
			get 
			{
				object o = ViewState["SortAscending"];
				if (o == null) 
				{
					return true;
				}
				return (bool)o;
			}
    
			set 
			{
				ViewState["SortAscending"] = value;
			}
		}
    
    
    
    
    
		protected void SelectColumnsClick(Object s, EventArgs e) 
		{
			pnlSelectColumns.Visible = true;
			foreach (string colIndex in _arrIssueColumns) 
			{
				ListItem item = lstIssueColumns.Items.FindByValue(colIndex);
				if (item != null)
					item.Selected = true;
			}
		}
    
    
		protected void SaveClick(Object s, EventArgs e) 
		{
			string strIssueColumns = " 0";
			foreach (ListItem item in lstIssueColumns.Items)
				if (item.Selected)
					strIssueColumns += " " + item.Value;
			strIssueColumns = strIssueColumns.Trim();
    
			_arrIssueColumns = strIssueColumns.Split();
    
			Response.Cookies[Globals.IssueColumns].Value = strIssueColumns;
			Response.Cookies[Globals.IssueColumns].Path = "/";
			Response.Cookies[Globals.IssueColumns].Expires = DateTime.MaxValue;
    
			pnlSelectColumns.Visible = false;
    
			OnRebindCommand(EventArgs.Empty);
		}
    
    
		protected void CancelClick(Object s, EventArgs e) 
		{
			pnlSelectColumns.Visible = false;
		}

        public void ToExcel(Object s, EventArgs e) 
        {
            Response.Charset = "ISO-LATIN-7"; //"GB2312";//download file name maybe include Chinese
            Response.AppendHeader("Content-Disposition", "attachment;filename=" +
                System.DateTime.Now.ToString("yyyyMMddhhmmss") + "_" +
                HttpUtility.UrlEncode(System.Text.Encoding.UTF8.GetBytes("DataGrid Content")) + ".xls");//add httpheader for execl download file
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            Response.ContentType = "application/ms-execl";
            
            System.IO.StringWriter ExeclSw = new System.IO.StringWriter();//instance a new StringWriter object
            System.Web.UI.HtmlTextWriter ExeclHw = new HtmlTextWriter(ExeclSw);//instance a new HtmlTextWriter object
            
            OnRebindCommand(e);
            System.Web.UI.WebControls.DataGrid datagrid = new DataGrid();
            grdIssues.Page.EnableViewState = false;

            datagrid.DataSource = grdIssues.DataSource; //Issue.GetIssuesByProjectId(2); ;// 
            datagrid.DataBind();
            datagrid.RenderControl(ExeclHw);
            Response.Write(ExeclHw.InnerWriter.ToString());//output result to customer ie client
            Response.End();
        }


}
}
