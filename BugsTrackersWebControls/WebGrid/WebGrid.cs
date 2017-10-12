using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
using System.Diagnostics;
using System.Data.SqlClient;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Collections;
using System.Collections.Generic;


namespace BugTrackerWebControls
{
	/// <summary>
    /// Web grid control.
	/// </summary>
	/// 
    public class WebGrid : System.Web.UI.WebControls.WebControl, INamingContainer
    {

        #region ----------private control members---------------------------------------------------
        /// <summary>
        /// set dataview for grid.
        /// </summary>
        private DataView gridDataView;
        /// <summary>
        /// WHERE clause class global variable.
        /// </summary>
        private string whereClause;
        /// <summary>
        /// WHERE clause externat variable.
        /// </summary>
        private string _whereClause;
        /// <summary>
        /// number of characters in text box for search.
        /// </summary>
        private int mandatoryCharactersNumberForSearch ;

        /// <summary>
        /// Keeps selected credits from rows.
        /// </summary>
        private HtmlInputHidden hiddenSelectedCredits = new HtmlInputHidden();

        /// <summary>
        /// Keeps columnt to search for.
        /// </summary>
        private CreditRequestsDDList ddListSearch = new CreditRequestsDDList();

        #endregion ----------private control members------------------------------------------------

        #region ----------protected control members---------------------------------------------------
        /// <summary>
        /// Grid view local object.
        /// </summary>
        protected PagingGridView grid = new PagingGridView();

        /// <summary>
        /// TextBox search local object.
        /// </summary>
        protected TextBox textSearch = new TextBox();

        /// <summary>
        /// Search button local object.
        /// </summary>
        protected Button cmdSearchResult = new Button();

        /// <summary>
        /// Label result local object.
        /// </summary>
        public Label LabelResult = new Label();

        #endregion ----------protected control members------------------------------------------------

        #region ------------------------- WebGrid Custom Properties -------------------------------
        /// <summary>
        /// Show filter section for this grid
        /// </summary>
        [Category("Behavior")]
        [DefaultValue(false)]
        [Description("Show filter section for this grid")]
        public bool ShowFilterSection
        {
            [DebuggerStepThrough()]
            set
            {
                ddListSearch.Visible = value;
                textSearch.Visible = value;
                cmdSearchResult.Visible = value;

            }
        }

        /// <summary>
        /// Grid DataView.
        /// </summary>
        public DataView DataView
        {
            get
            {
                return gridDataView;
            }

            set
            {
                gridDataView = value;
            }
        }

        /// <summary>
        /// PagingGridView.
        /// </summary>
        public GridView MyDataGrid
        {
            get { return grid; }
        }

        /// <summary>
        /// WHERE clause for grid.
        /// </summary>
        [Category("Behavior")]
        [Description("WHERE clause for grid.")]
        public string WhereClause
        {
            [DebuggerStepThrough()]
            get
            {
                return whereClause;
            }

            set 
            {
                _whereClause = value;
            }
        }

        /// <summary>
        /// Array of ID of rows .
        /// </summary>
        [Category("Behavior")]
        [Description("HtmlInputHidden Array of ID of rows .")]
        public HtmlInputHidden HiddenSelectedCredits
        {
            [DebuggerStepThrough()]
            get
            {
                hiddenSelectedCredits.ID = "SelectedCreditsHidden";
                return hiddenSelectedCredits;
            }
        }

        /// <summary>
        /// Selected Rows.
        /// </summary>
        [Category("Behavior")]
        [Description("Array of ID of rows .")]
        public string SelectedRows
        {
            [DebuggerStepThrough()]
            get
            {

                return ((HtmlInputHidden)FindControl("SelectedCreditsHidden")).Value;
            }
        }

        /// <summary>
        /// Columns ToFilter.
        /// </summary>
        [Category("Behavior")]
        [Description("Array of columns to use in filter combo box..")]
        public string[] ColumnsToFilter
        {
            [DebuggerStepThrough()]
            get
            {
                object obj = ViewState["columnsToFilter"];

                return (obj == null) ? null : (string[])obj;
            }
            [DebuggerStepThrough()]
            set
            {
                ViewState["columnsToFilter"] = value;
            }
        }

        /// <summary>
        /// Array of columns tohat are not display.
        /// </summary>
        [Category("Behavior")]
        [Description("Array of columns tohat are not display.")]
        public int[] ColumnsToHide
        {
            [DebuggerStepThrough()]
            get
            {
                object obj = ViewState["columnsToHide"];

                return (obj == null) ? null : (int[])obj;
            }
            [DebuggerStepThrough()]
            set
            {
                ViewState["columnsToHide"] = value;
            }
        }

        /// <summary>
        /// Connection String data source for this grid.
        /// </summary>
        [Category("Behavior")]
        [Description("Connection String data source for this grid.")]
        public string TableConnectionString
        {
            [DebuggerStepThrough()]
            get
            {
                object obj = ViewState["tableConnectionString"];

                return (obj == null) ? string.Empty : (string)obj;
            }
            [DebuggerStepThrough()]
            set
            {
                ViewState["tableConnectionString"] = value;
            }
        }

        /// <summary>
        /// Table or select data source for this grid.
        /// </summary>
        [Category("Behavior")]
        [Description("Table or select data source for this grid.")]
        public string TableSourceName
        {
            [DebuggerStepThrough()]
            get
            {
                object obj = ViewState["tableSourceName"];

                return (obj == null) ? string.Empty : (string)obj;
            }
            [DebuggerStepThrough()]
            set
            {
                ViewState["tableSourceName"] = value;
            }
        }

        /// <summary>
        /// Order by Column for this grid
        /// </summary>
        [Category("Behavior")]
        [Description("Order by Column for this grid")]
        public string TableDefaultOrderByColumn
        {
            [DebuggerStepThrough()]
            get
            {
                object obj = ViewState["tableDefaultOrderByColumn"];

                return (obj == null) ? string.Empty : (string)obj;
            }
            [DebuggerStepThrough()]
            set
            {
                ViewState["tableDefaultOrderByColumn"] = value;
            }
        }

        /// <summary>
        /// Mandatory!
        /// Represent the base column for ID.
        /// </summary>
        [Category("Behavior")]
        [Description("Link Column for this grid.If Empty the grid has no link column.")]
        public string ReferenceColumnName
        {
            [DebuggerStepThrough()]
            get
            {
                object obj = ViewState["referenceColumnName"];

                return (obj == null) ? string.Empty : (string)obj;
            }
            [DebuggerStepThrough()]
            set
            {
                ViewState["referenceColumnName"] = value;
            }
        }

        /// <summary>
        /// URL string for Link Column for this grid.
        /// If Empty the grid has no link column.
        /// </summary>
        [Category("Behavior")]
        [Description("URL string for Link Column for this grid.If Empty the grid has no link column.")]
        public string UrlStringForLinkColumn
        {
            [DebuggerStepThrough()]
            get
            {
                object obj = ViewState["urlStringForLinkColumn"];

                return (obj == null) ? string.Empty : (string)obj;
            }
            [DebuggerStepThrough()]
            set
            {
                ViewState["urlStringForLinkColumn"] = value;
            }
        }

        /// <summary>
        /// Show Selection Image Column for this grid.
        /// On click , display detail section on bottom.
        /// </summary>
        [Category("Behavior")]
        [DefaultValue(true)]
        [Description("Show Selection Image Column for this grid.")]
        public bool ShowSelectionColumn
        {
            [DebuggerStepThrough()]
            get
            {
                object obj = ViewState["showSelectionColumn"];

                return (obj == null) ? false : (bool)obj;
            }
            [DebuggerStepThrough()]
            set
            {
                ViewState["showSelectionColumn"] = value;
            }
        }

        /// <summary>
        /// Show CheckBox Column for this grid
        /// </summary>
        [Category("Behavior")]
        [DefaultValue(false)]
        [Description("Show CheckBox Column for this grid")]
        public bool ShowCheckBoxColumn
        {
            [DebuggerStepThrough()]
            get
            {
                object obj = ViewState["showCheckBoxColumn"];

                return (obj == null) ? false : (bool)obj;
            }
            [DebuggerStepThrough()]
            set
            {
                ViewState["showCheckBoxColumn"] = value;
            }
        }

        /// <summary>
        /// Filter text box property.
        /// </summary>
        public System.Web.UI.WebControls.TextBox SearchTextBox
        {
            get { return (TextBox)FindControl("SearchInput"); }
            //set {
            //        TextBox textSearchInput = (TextBox)FindControl("SearchInput");
            //        textSearchInput = value;             
            //    }
        }

        /// <summary>
        /// Combo box for selected column to filter with.
        /// </summary>
        public CreditRequestsDDList DDListSearchColumns
        {

            get { return ddListSearch; }
            set { ddListSearch = value; }
        }

        /// <summary>
        /// Order by property of the grid.
        /// </summary>
        public string OrderBy
        {
            get
            {
                string orderBy = string.Empty;
                if (grid != null)
                    orderBy =  grid.OrderBy;
                return orderBy;
            }
            set
            {
                if (grid != null)
                  grid.OrderBy = value;
            }
        }

        /// <summary>
        /// Return record count.
        /// </summary>
        public int GridRecordCount
        { 
             get{
                 int count = 0;
                 if (grid.Rows != null)
                     count = grid.Rows.Count;
                 return count;
             }
        }

        /// <summary>
        /// Number of mandatory chars for search text box.
        /// </summary>
        public int MandatoryCharactersNumberForSearch
        {
            get { return mandatoryCharactersNumberForSearch; }
            set { mandatoryCharactersNumberForSearch = value; }
        }

        /// <summary>
        /// Grid is visible only on postback, not on loading.
        /// </summary>
        public bool GridVisible
        {
            get { return grid.Visible; }
            set { grid.Visible = value; }
        }
        #endregion---------------------------------------------------------------------------

        #region ----------public control methods---------------------------------------------------
        /// <summary>
        /// Bind grid: bind grid from page.
        /// MANDATORY!
        /// </summary>
        public void BindGrid()
        {
            if (grid.Visible)
            {
                //reconstruct Where
                CreateWhereClause(textSearch, ddListSearch);

                this.EnsureChildControls();
                grid.ToolTip = SetWebGridToolTip(grid);
                int pageIndex = grid.RowsCount > (grid.CurrentPageIndex ) * grid.PageSize ? grid.CurrentPageIndex : 0; //keep or not on page.
                grid.DataSource = GetDataPage(pageIndex, grid.PageSize, grid.OrderBy);
                
                grid.EmptyDataText = "Nu exista inregistrari. &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";

                grid.DataBind();
            }
        }


        /// <summary>
        /// Return rank of a column by his Name.
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public int ColumnRank<T>(T column)
        {
            int columnRank = 0;
            string columnName = string.Empty;
            if (column.GetType() == Type.GetType("System.String"))
            {
                columnName = Convert.ToString(column);

                for (int i = 0; i < grid.HeaderRow.Cells.Count; i++)
                    if (grid.HeaderRow.Cells[i].Controls.Count > 0)
                        if (((LinkButton)grid.HeaderRow.Cells[i].Controls[0]) != null)
                            if (columnName.ToUpper() == ((LinkButton)grid.HeaderRow.Cells[i].Controls[0]).Text.ToUpper())
                                columnRank = i;
            }

            return columnRank;
        }

        ///// <summary>
        ///// Search event. For compatibility with old grid.
        ///// </summary>
        //public event System.EventHandler Search;
        ///// <summary>
        ///// Item change event. For compatibility with old grid.
        ///// </summary>
        //public event System.EventHandler SelectedItemChanged;

        #endregion ----------public control methods---------------------------------------------------

        #region ----------private control methods---------------------------------------------------

        /// <summary>
		/// This CreateChildControls should be used to write code for
		/// creating our web composite controls.
		/// </summary>
		protected override void CreateChildControls()
		{

            //------------------------add css dynamic!-----------------
            if (Page.Header != null)// the <HEAD must runat server
            {
                HtmlLink lnkStyle = new HtmlLink();
                lnkStyle.Href = this.Page.ClientScript.GetWebResourceUrl(this.GetType(), "BugTrackerWebControls.Theme.WebGrid.css");
                lnkStyle.Attributes["rel"] = "stylesheet";
                lnkStyle.Attributes["type"] = "text/css";
                lnkStyle.ID = "cssWebGrid";

                Page.Header.Controls.Add(lnkStyle);
            }
           
            //-----------------------add elements---------------------------------
            //grid.ID =  "WebGrid";
            grid.CssClass = "grid-view";
            grid.AllowPaging = true;
            grid.AllowSorting = true;
            grid.PageIndexChanging += new GridViewPageEventHandler(grid_PageIndexChanging);
            grid.Sorting += new GridViewSortEventHandler(grid_Sorting);

            grid.AddSelectColumn();
            grid.AddCheckBoxColumn(ShowCheckBoxColumn,false);
            grid.AddHyperlinkColumn(ReferenceColumnName, UrlStringForLinkColumn);

            grid.ColumnsToHide = ColumnsToHide;

            //----------------add search input text box-----------------------------------------
            textSearch.ID = "SearchInput";
            textSearch.EnableViewState = true;
            textSearch.CssClass = "field";

            //---------------add filter combo obx-----------------------------------------------

            ddListSearch.ID = "SearchColumnsList";
            ddListSearch.Add(ColumnsToFilter);
            ddListSearch.CssClass = "combo";
            //---------------add search result button--------------------

            cmdSearchResult.Text = "Cauta";
            cmdSearchResult.CssClass = "submit_emitent";
            cmdSearchResult.Click += new EventHandler(cmdResult_Click);
            //----------------add search label-----------------------------------------
            LiteralControl lblSearch = new LiteralControl("<SPAN class = searchLabel");
            lblSearch.Text = "Cautare ";

            //----------------add result label-----------------------------------------
            LabelResult.ID = "Result";
            LabelResult.CssClass = "'searchLabel'";

            //----------------add holder place-----------------------------------------
            LiteralControl lc = new LiteralControl("<br><div id = divHolder width=100%> </div>");

            //----------------add container tables-----------------------------------------
            Table containerTable = new Table();
            containerTable.ID = "ContainerTable";
            TableRow headerTableRow = new TableRow();
            TableCell cell = new TableCell();
            cell.VerticalAlign = VerticalAlign.Middle;
            cell.CssClass = "TD";

            //----------------add container tables for search elements-----------------------------------------
            Table containerSearchTable = new Table();
            containerSearchTable.ID = "ContainerSearchTable";
            TableRow searchTableRow = new TableRow();
            TableCell lblSearchCell = new TableCell();
            lblSearchCell.Controls.Add(lblSearch);
            TableCell ddListSearchCell = new TableCell();
            ddListSearchCell.Controls.Add(ddListSearch);
            TableCell textSearchCell = new TableCell();
            textSearchCell.Controls.Add(textSearch);
            TableCell cmdSearchResultCell = new TableCell();
            cmdSearchResultCell.Controls.Add(cmdSearchResult);
            searchTableRow.Cells.Add(lblSearchCell);
            searchTableRow.Cells.Add(ddListSearchCell);
            searchTableRow.Cells.Add(textSearchCell);
            searchTableRow.Cells.Add(cmdSearchResultCell);
            containerSearchTable.Rows.Add(searchTableRow);
            cell.Controls.Add(containerSearchTable);
            //--------------------------------------------------------------------------------------------------

            if (ddListSearch.Visible)
             headerTableRow.Cells.Add(cell);

            TableRow containerTableRow = new TableRow();
            TableCell cellGrid = new TableCell();
            cellGrid.Controls.Add(grid);
            containerTableRow.Cells.Add(cellGrid);

            TableRow detailTableRow = new TableRow();
            TableCell cellDetail = new TableCell();
            cellDetail.Controls.Add(lc);
            detailTableRow.Cells.Add(cellDetail);

            containerTable.Rows.Add(headerTableRow);
            containerTable.Rows.Add(containerTableRow);
            containerTable.Rows.Add(detailTableRow);

            Controls.Add(containerTable);

            Controls.Add(LabelResult);


            //register client script.
            if (this.Page != null && !this.Page.ClientScript.IsClientScriptIncludeRegistered(this.GetType(), "BugTrackerWebControls"))
                this.Page.ClientScript.RegisterClientScriptInclude(this.GetType(), "BugTrackerWebControls.WebGrid", this.Page.ClientScript.GetWebResourceUrl(this.GetType(), "BugTrackerWebControls.Scripts.WebGrid.js"));

            //BindGrid();


            SetGlobalVariablesInJS();

            if (mandatoryCharactersNumberForSearch > 0)
                cmdSearchResult.Attributes.Add("onclick", "if (document.getElementById('" + textSearch.ClientID + "').value.length < " + mandatoryCharactersNumberForSearch + ") {alert('Introduceti un numar de caractere mai mare decat " + mandatoryCharactersNumberForSearch + "'); return false} else {return true;}");

		
		}
        /// <summary>
        /// Create WHERE Clause for SQL.
        /// </summary>
        /// <param name="textSearch"></param>
        /// <param name="ddListSearch"></param>
        private void CreateWhereClause(TextBox textSearch, CreditRequestsDDList ddListSearch)
        {
            //replace ' because of error  in SQL Dinamic
            string textFromSearch = string.Empty;
            textFromSearch = textSearch.Text.Replace("'", "''");

            string exteriorWhereClause = string.Empty;
            string internalWhereClause = string.Empty;

            //add exterior where clause
            if(!string.IsNullOrEmpty(_whereClause))
                exteriorWhereClause = _whereClause;
            //in case of postback
            if (this.Page.IsPostBack)
                if (!string.IsNullOrEmpty(this.Page.Request.Form[textSearch.ClientID.Replace('_', '$')]))
                    internalWhereClause = whereClause + this.Page.Request.Form[ddListSearch.ClientID.Replace('_', '$')].ToString() + " like '" + this.Page.Request.Form[textSearch.ClientID.Replace('_', '$')].ToString().TrimEnd() + "%'";
                
            // in case of search on Button
            if (!string.IsNullOrEmpty(textFromSearch.TrimEnd()))
                internalWhereClause = ddListSearch.Text + " like '" + textFromSearch.TrimEnd() + "%'";

            if (string.IsNullOrEmpty(exteriorWhereClause))
                whereClause = internalWhereClause;
            else
                if (string.IsNullOrEmpty(internalWhereClause))
                    whereClause = exteriorWhereClause;
                else
                    whereClause = exteriorWhereClause + " AND " + internalWhereClause;


        }

        /// <summary>
        /// On sorting event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grid_Sorting(object sender, GridViewSortEventArgs e)
        {
            PagingGridView grid = (PagingGridView)sender;
            grid.DataSource = GetDataPage(grid.PageIndex, grid.PageSize, grid.OrderBy);
            grid.DataBind();
        }

        /// <summary>
        /// Page index changing event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grid_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            PagingGridView grid = (PagingGridView)sender;
            grid.PageIndex = e.NewPageIndex;
            grid.DataSource = GetDataPage(grid.PageIndex, grid.PageSize, grid.OrderBy);
            grid.DataBind();
        }

        ///// <summary>
        ///// Bind grid: Keep compatibility with OLD WebGrid.
        ///// </summary>
        //public void BindGrid()
        //{
        //    GridViewPageEventArgs e = new GridViewPageEventArgs(grid.PageIndex);
        //    grid_PageIndexChanging(grid,e);
        //}
        /// <summary>
        /// On click event for search button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
		private void cmdResult_Click(object sender, EventArgs e)
		{
            Label lblResult = (Label)FindControl("Result");
            lblResult.Text = string.Empty;

            //try
            //{
            //    BindGrid();
					
            //}
            //catch(Exception ex)
            //{
            //    ShowError(ex,"cmdResult_Click");
            //}
        }


        /// <summary>
        /// Set grid tool tip.
        /// Something like :Nr. total de inregistrari:{0} ; Nr.pagini:{1}
        /// Set also RowsCount.
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="where"></param>
        private string SetWebGridToolTip(PagingGridView grid)
        {
            double totalRows = GetRowCount(); 
            grid.VirtualItemCount = (int)totalRows;
            double numberOfPages =  Math.Ceiling(Convert.ToDouble(totalRows / grid.PageSize));
            grid.RowsCount = totalRows;

            return String.Format("Nr. total de inregistrari:{0} ; Nr.pagini:{1}", totalRows, numberOfPages );
        }

     

        /// <summary>
        /// Get rows count for records.
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        private int GetRowCount()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(TableConnectionString))
                {
                    string sqlCommand = @"SELECT COUNT(*) FROM " + TableSourceName;

                    if (!string.IsNullOrEmpty(whereClause))
                        sqlCommand = sqlCommand + " WHERE " + whereClause;

                    conn.Open();
                    SqlCommand comm = new SqlCommand(sqlCommand, conn);
                    int count = Convert.ToInt32(comm.ExecuteScalar());
                    conn.Close();
                    return count;
                }
            }
            catch (Exception ex)
            {
                ShowError(ex,"GetRowCount");
                return -1;
            }
        }
        /// <summary>
        /// Return datapage from a view.
        /// </summary>
        /// <returns></returns>
        private DataTable GetDataPage()
        {
            return gridDataView.Table;
        }

        /// <summary>
        /// Get DataPage source for webgrid.
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="sortExpression"></param>
        /// <returns>DataTable</returns>
        /// <remarks>
        /// This shows an example of how we can retrieve a block of records using ROW_NUMBER()
        /// For example, pageIndex=3, pageSize=20 so the record we want to display is row 61-80
        /// the resultant SQL will have an inner select that retrieve "TOP 80" rows, the outer 
        /// select than filter out all the row <= 60 through "ROW_NUM > 60" expression to return
        /// the 20 records (row 61-80 )
        /// </remarks>
        private DataTable GetDataPage(int pageIndex, int pageSize, string sortExpression)
        {
            if (gridDataView != null)
                return GetDataPage();

            SqlConnection conn = null;
            try
            {
                using ( conn = new SqlConnection(TableConnectionString))
                {
                    // We always need a default sort field for ROW_NUMBER() to work correctly
                    if (sortExpression.Trim().Length == 0)
                        sortExpression = TableDefaultOrderByColumn;

                    conn.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter();
                    SqlCommand comand = new SqlCommand();
                    comand.Connection = conn;
                    comand.CommandText = "PagesSelectGenerator";
                    comand.CommandType = CommandType.StoredProcedure;
                    // SelectNumber 11,1 ,'Istoric_Cereri','IDIstoric','NrDosar like ''0855925%'''
                    comand.Parameters.Add("@TotalRows", SqlDbType.NVarChar, 50).Value = Convert.ToString(((pageIndex + 1) * pageSize));
                    comand.Parameters.Add("@IntPageRows", SqlDbType.Int, 5).Value = (pageIndex * pageSize);
                    comand.Parameters.Add("@sTableName", SqlDbType.NVarChar, 50).Value = TableSourceName;
                    comand.Parameters.Add("@sOrderBy", SqlDbType.NVarChar, 50).Value = sortExpression;
                    comand.Parameters.Add("@sWhereClause", SqlDbType.NVarChar, 250).Value = whereClause;
                    adapter.SelectCommand = comand;
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    conn.Close();
                    dt.Columns.Remove("ROW_NUM");
                    return dt;
                }
            }
            catch (Exception ex)
            {
                if (conn != null)
                    conn.Close();

                ShowError(ex,"GetDataPage");
                return null;
            }

        }

        #region Dynamic data query

        /// <summary>
        /// Manage errors.
        /// </summary>
        /// <param name="ex"></param>
        private void ShowError(Exception ex, string functionName)
        {
            Common.LogError logger = new BugTrackerWebControls.Common.LogError(ex.Message + " in " + functionName,ex.StackTrace);
            logger.Write();

            WhereClause = string.Empty;
            OrderBy = string.Empty;

        }
        #endregion

        /// <summary>
        /// Write variables in JS;
        /// </summary>
        private void SetGlobalVariablesInJS()
        {
            
            hiddenSelectedCredits.ID = "SelectedCreditsHidden";
            hiddenSelectedCredits.EnableViewState = false;
            HtmlInputHidden HiddenSortChecked = new HtmlInputHidden();

            Controls.Add(hiddenSelectedCredits);
            Controls.Add(HiddenSortChecked);

            string strScript;
            //PagingGridView dg = (PagingGridView)FindControl("WebGrid");
            strScript = "\n<script>\n";
            strScript += "var dataGridObj = document.getElementById('" + grid.ClientID + "');\n";
            strScript += "var HiddenSelectedCreditsObj = document.getElementById('" + HiddenSelectedCredits.ClientID + "');\n";
            strScript += "var HiddenSortCheckedObj = document.getElementById('" + HiddenSortChecked.ClientID + "');\n";
            strScript += "if (typeof(CheckObjects) == 'function') CheckObjects();\n";
            strScript += "</script>";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "global variables", strScript);

        }

        #endregion ----------private control methods---------------------------------------------------

    }
}
