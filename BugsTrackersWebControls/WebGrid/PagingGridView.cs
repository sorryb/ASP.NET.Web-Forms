using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
using System.Diagnostics;

namespace BugTrackerWebControls
{
    /// <summary>
    /// Grid view object with SQL pagging.
    /// </summary>
    public class PagingGridView: GridView
    {
        /// <summary>
        /// Construnctor.
        /// </summary>
        public PagingGridView(): base()
        {
            this.AllowPaging = true;
            this.AllowSorting = true;
            this.PagerSettings.Mode = PagerButtons.NumericFirstLast;
        }

        private int[] _columnsToHide;
        private int referenceColumnsNumber;
        private enum TypeOfLinkColumn { JavaScriptFunction, HyperLinkHTML };
        private TypeOfLinkColumn typeOfLinkColumn = TypeOfLinkColumn.HyperLinkHTML;
        private string referenceColumnName;

        /// <summary>
        /// Keep records count in grid.
        /// </summary>
        private double _rowsCount;

        #region Custom properties

        /// <summary>
        /// Show Selection Column.
        /// </summary>
        [Category("Behavior")]
        [DefaultValue(false)]
        [Description("Show Selection Image Column for this grid")]
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
        /// Show CheckBox Column.
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
        /// Virtual ItemCount property.
        /// </summary>
        [Browsable(true), Category("NewDynamic")]
        [Description("Set the virtual item count for this grid")]
        public int VirtualItemCount
        {
            get 
            {
                if (ViewState["pgv_vitemcount"] == null)
                    ViewState["pgv_vitemcount"] = -1;
                return Convert.ToInt32(ViewState["pgv_vitemcount"]);
            }
            set
            {
                ViewState["pgv_vitemcount"] = value;
            }
        }
        /// <summary>
        /// Order by property.
        /// </summary>
        [Browsable(true), Category("NewDynamic")]
        [Description("Get the order by string to use for this grid when sorting event is triggered")]
        public string OrderBy
        {
            get
            {
                if (ViewState["pgv_orderby"] == null)
                    ViewState["pgv_orderby"] = string.Empty;
                return ViewState["pgv_orderby"].ToString();
            }
            set
            {
                ViewState["pgv_orderby"] = value ;
            }
        }

        /// <summary>
        /// Columns to hide property.
        /// </summary>
        public int[] ColumnsToHide
        {
            set
            {
                _columnsToHide = value;
            }
        }

        /// <summary>
        /// Current page index.
        /// </summary>
        public int CurrentPageIndex
        {
            get
            {
                if (ViewState["pgv_pageindex"] == null)
                    ViewState["pgv_pageindex"] = 0;
                return Convert.ToInt32(ViewState["pgv_pageindex"]);
            }
            set
            {
                ViewState["pgv_pageindex"] = value;
            }
        }
        /// <summary>
        /// Records count.
        /// </summary>
        [Browsable(true), Category("NewDynamic")]
        [Description("Set or get number of records in grid.")]
        public double RowsCount
        {
            get
            {
                return _rowsCount;
            }
            set
            {
                _rowsCount = value;
            }
        }

        /// <summary>
        /// Custom pagging.
        /// </summary>
        private bool CustomPaging
        {
            get
            {
                return (VirtualItemCount != -1);
            }
        }
        #endregion

        #region Overriding the parent methods
        /// <summary>
        /// Data source.
        /// </summary>
        public override object DataSource
        {
            get
            {
                return base.DataSource;
            }
            set
            {
                base.DataSource = value;
                // we store the page index here so we dont lost it in databind
                CurrentPageIndex = PageIndex;
            }
        }

        /// <summary>
        /// 
        /// Represent the base column for ID.
        /// </summary>
        public string ReferenceColumnName
        {
            get
            {
                return referenceColumnName;
            }
            set
            {
                referenceColumnName = value;
            }
        }

        /// <summary>
        /// On sorting event.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnSorting(GridViewSortEventArgs e)
        {
            // We store the direction for each field so that we can work out whether next sort should be asc or desc order
            SortDirection direction = SortDirection.Ascending;
            if (ViewState[e.SortExpression] != null && (SortDirection)ViewState[e.SortExpression] == SortDirection.Ascending)
            {
                direction = SortDirection.Descending;
            }
            ViewState[e.SortExpression] = direction;
            OrderBy = string.Format("{0} {1}","[" + e.SortExpression + "]", (direction == SortDirection.Descending ? "DESC" : ""));
            base.OnSorting(e);
            ViewState["SortExpression"] = e.SortExpression;
            ViewState["SortDirection"] = direction;

            
        }
        /// <summary>
        /// Initialization of pagger.
        /// </summary>
        /// <param name="row"></param>
        /// <param name="columnSpan"></param>
        /// <param name="pagedDataSource"></param>
        protected override void InitializePager(GridViewRow row, int columnSpan, PagedDataSource pagedDataSource)
        {
            // This method is called to initialise the pager on the grid. We intercepted this and override
            // the values of pagedDataSource to achieve the custom paging using the default pager supplied
            if (CustomPaging)
            {
                pagedDataSource.AllowCustomPaging = true;
                pagedDataSource.VirtualCount = VirtualItemCount;
                pagedDataSource.CurrentPageIndex = CurrentPageIndex;
            }
            base.InitializePager(row, columnSpan, pagedDataSource);

            //put padding
            this.CellPadding = 3;
        }
        #endregion 

        #region style on rows
        /// <summary>
        /// On creation event for a row.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnRowCreated(GridViewRowEventArgs e)
        {
            string _highlightNormalCssClass = "normalhover";
            string _highlightAlternateCssClass = "alternatehover";

            //Add CSS class on header row.
            if (e.Row.RowType == DataControlRowType.Header)
                e.Row.CssClass = "header";

            //Add CSS class on normal row.
            if (e.Row.RowType == DataControlRowType.DataRow && e.Row.RowState == DataControlRowState.Normal)
            {
                e.Row.CssClass = "normal";
                e.Row.Attributes["onmouseover"] = "this.className='" + _highlightNormalCssClass + "';";
                e.Row.Attributes["onmouseout"] = "this.className='normal';";
            }

            //Add CSS class on alternate row.
            if (e.Row.RowType == DataControlRowType.DataRow && e.Row.RowState == DataControlRowState.Alternate)
            {
                e.Row.CssClass = "alternate";
                e.Row.Attributes["onmouseover"] = "this.className='" + _highlightAlternateCssClass + "';";
                e.Row.Attributes["onmouseout"] = "this.className='alternate';";
            }


            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Image ctrlImage = (Image)e.Row.Cells[0].Controls[0];
                ctrlImage.ImageUrl = Page.ClientScript.GetWebResourceUrl(this.GetType(), "BugTrackerWebControls.Images.sageata.gif");
            }
            
        }
        
        #endregion 

        #region template columns
        /// <summary>
        /// On row data bound event for a row.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnRowDataBound(GridViewRowEventArgs e)
        {
            //add link attributes
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                
                if (!string.IsNullOrEmpty(ReferenceColumnName ))
                {
                    Image ctrlImage = (Image)e.Row.Cells[0].Controls[0];
                    HyperLink hiperLinkColumn = ((HyperLink)e.Row.Cells[referenceColumnsNumber - 1].Controls[0]);
                
                    string fileNo = hiperLinkColumn.Text;

                    if (typeOfLinkColumn == TypeOfLinkColumn.JavaScriptFunction)
                     hiperLinkColumn.NavigateUrl = "javascript:" + hiperLinkColumn.NavigateUrl;

                    ctrlImage.Attributes["onclick"] = "SelectGridRow(this,'" + fileNo + "')";
                    ctrlImage.Attributes["title"] = "Selecteaza randul pt. " + fileNo;
                }
            }

            //hide columns from ColumnsToHide
            if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
            {
                if (_columnsToHide != null)
                {
                    for (int i = 0; i < _columnsToHide.Length ; i++)
                        //if (e.Row.Cells.Count <= _columnsToHide[i] + referenceColumnsNumber - 1)
                            e.Row.Cells[_columnsToHide[i] + referenceColumnsNumber - 1].Attributes["class"] = "hiddenCell";
                }
            }

            
        }
        /// <summary>
        /// LInk column.
        /// </summary>
        /// <param name="dataTextFieldName">Column bound field</param>
        /// <param name="urlFormatValue">url ; like http://google.ro?asta={0}</param>
        public void AddHyperlinkColumn(string dataTextFieldName, string urlFormatValue)
        {
            ReferenceColumnName = dataTextFieldName;

            if (string.IsNullOrEmpty(ReferenceColumnName))
                return;

            if (urlFormatValue.ToString().Contains("javascript:"))
            {
                typeOfLinkColumn = TypeOfLinkColumn.JavaScriptFunction;
                urlFormatValue = urlFormatValue.Replace("javascript:", "");
            }

            if (string.IsNullOrEmpty(urlFormatValue))
                urlFormatValue = "javascript:return false;";

            if (!string.IsNullOrEmpty(dataTextFieldName))
            {
                //increment number of mandatory columns
                referenceColumnsNumber++;

                HyperLinkField hyperLinkField = new HyperLinkField();

                string[] field = new string[1];
                field[0] = dataTextFieldName;
                hyperLinkField.DataNavigateUrlFields = field;
                hyperLinkField.DataNavigateUrlFormatString =  urlFormatValue;// "http://google.ro?asta={0}";
                hyperLinkField.HeaderText = dataTextFieldName;
                hyperLinkField.DataTextField = dataTextFieldName;
                hyperLinkField.SortExpression = dataTextFieldName;
                hyperLinkField.ItemStyle.HorizontalAlign = HorizontalAlign.Center;

                this.Columns.Add(hyperLinkField);
            }

            #region #TemplateBuilder Columns 
                //                Now becomes:

                //<asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Tracking" SortExpression="Tracking">
                //    <ItemTemplate>
                //        <asp:HyperLink ID="idTracking" runat="server"  NavigateUrl='<%# "ShowMapLocation(" + Eval("Longitude", "{0}") + "," + Eval("Latitude","{0}") + ")" %>' Text='<%# Eval("Tracking") %>'/>                                            
                //    </ItemTemplate>
                //</asp:TemplateField> With a supporting piece of CS:

                //protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
                //{
                //    if (e.Row.RowType == DataControlRowType.DataRow)
                //    {
                //        HyperLink hl = (HyperLink)e.Row.FindControl("idTracking");

                //        hl.NavigateUrl = "javascript:" + hl.NavigateUrl;
                //    }

                //}
            #endregion
        }

        /// <summary>
        /// Add new check box column.
        /// </summary>
        public void AddCheckBoxColumn(bool withCheckBox, bool isWithOrderAfterCheckBoxes)
        {
            if (withCheckBox)
            {
                //increment number of mandatory columns
                referenceColumnsNumber++;

                TemplateField tmplField = new TemplateField();
                string FolderImageUrl = string.Empty;

                tmplField.ItemTemplate = new CheckBoxTemplate("chkItemChecked", FolderImageUrl);
                tmplField.HeaderText = "Check all";

                if (isWithOrderAfterCheckBoxes)
                    tmplField.HeaderTemplate = new CheckBoxTemplate("chkAllItems", true, FolderImageUrl);
                else
                    tmplField.HeaderTemplate = new CheckBoxTemplate("chkAllItems", true, FolderImageUrl, false);

                tmplField.HeaderTemplate = new CheckBoxTemplate("chkAllItems", true, FolderImageUrl);

                this.Columns.Add(tmplField);
            }

        }
        /// <summary>
        /// Add select column.
        /// </summary>
        public void AddSelectColumn()
        {
            //increment number of mandatory columns
            referenceColumnsNumber++;

            TemplateField tmplField = new TemplateField();
            tmplField.ItemTemplate = new ImageTemplate(ListItemType.Item, "ColSelect");
            this.Columns.Add(tmplField);
        }

        #region SelectorImageUrl
        /// <summary>
        /// Selector Image URL.
        /// </summary>
        [Description("The url of the image shown on left side of a column."), DefaultValue(""), Themeable(true), Category("Appearance")]
        public string SelectorImageUrl
        {
            get
            {
                string str = ViewState["SelectorImageUrl"] as string;
                if (str == null)
                    return string.Empty;
                else
                    return str;
            }
            set
            {
                ViewState["SelectorImageUrl"] = value;
            }
        }
        /// <summary>
        /// Internal selector image.
        /// </summary>
        protected virtual string SelectorImageUrlInternal
        {
            get
            {
                if (string.IsNullOrEmpty(SelectorImageUrl))
                    return Page.ClientScript.GetWebResourceUrl(this.GetType(), "BugTrackerWebControls.Images.ArrowDown.gif");
                else
                    return SelectorImageUrl;
            }
        }

 
        #endregion

 
        #endregion

         #region  Image sorting 


        #region Properties
        #region Style properties
        private TableItemStyle _sortAscendingStyle;
        private TableItemStyle _sortDescendingStyle ;

        /// <summary>
        /// Sort ascendenting.
        /// </summary>
        [Description("The style applied to the header cell that is sorted in ascending order."), Themeable(true), Category("Styles"), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), NotifyParentProperty(true), PersistenceMode(PersistenceMode.InnerProperty)]
        public TableItemStyle SortAscendingStyle
        {
            get
            {
                if (_sortAscendingStyle == null)
                {
                    _sortAscendingStyle = new TableItemStyle();
                    if (base.IsTrackingViewState)
                        ((IStateManager)_sortAscendingStyle).TrackViewState();
                }

                return _sortAscendingStyle;
            }
        }
        /// <summary>
        /// Sort descendenting.
        /// </summary>
        [Description("The style applied to the header cell that is sorted in descending order."), Themeable(true), Category("Styles"), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), NotifyParentProperty(true), PersistenceMode(PersistenceMode.InnerProperty)]
        public TableItemStyle SortDescendingStyle
        {
            get
            {
                if (_sortDescendingStyle == null)
                {
                    _sortDescendingStyle = new TableItemStyle();
                    if (base.IsTrackingViewState)
                        ((IStateManager)_sortDescendingStyle).TrackViewState();
                }

                return _sortDescendingStyle;
            }
        }
        #endregion

        #region ArrowUpImageUrl
        /// <summary>
        /// Arrow up image URL.
        /// </summary>
        [Description("The url of the image shown when a column is sorted in ascending order."), DefaultValue(""), Themeable(true), Category("Appearance")]
        public string ArrowUpImageUrl
        {
            get
            {
                string str = ViewState["ArrowUpImageUrl"] as string;
                if (str == null)
                    return string.Empty;
                else
                    return str;
            }
            set
            {
                ViewState["ArrowUpImageUrl"] = value;
            }
        }
        /// <summary>
        /// Internal arrow up url.
        /// </summary>
        protected virtual string ArrowUpImageUrlInternal
        {
            get
            {
                if (string.IsNullOrEmpty(ArrowUpImageUrl))
                    return Page.ClientScript.GetWebResourceUrl(this.GetType(), "BugTrackerWebControls.Images.ArrowUp.gif");
                else
                    return ArrowUpImageUrl;
            }
        }
        #endregion

        #region ArrowDownImageUrl
        /// <summary>
        /// Arrow down image URL.
        /// </summary>
        [Description("The url of the image shown when a column is sorted in descending order."), DefaultValue(""), Themeable(true), Category("Appearance")]
        public string ArrowDownImageUrl
        {
            get
            {
                string str = ViewState["ArrowDownImageUrl"] as string;
                if (str == null)
                    return string.Empty;
                else
                    return str;
            }
            set
            {
                ViewState["ArrowDownImageUrl"] = value;
            }
        }
        /// <summary>
        /// Internal Arrow down image URL.
        /// </summary>
        protected virtual string ArrowDownImageUrlInternal
        {
            get
            {
                if (string.IsNullOrEmpty(ArrowDownImageUrl))
                    return Page.ClientScript.GetWebResourceUrl(this.GetType(), "BugTrackerWebControls.Images.ArrowDown.gif");
                else
                    return ArrowDownImageUrl;
            }
        }
        #endregion
        #endregion

        #region "Methods"
        /// <summary>
        /// Prepare hierachy.
        /// </summary>
        protected override void PrepareControlHierarchy()
        {

            base.PrepareControlHierarchy();
            string SortExpression = ViewState["SortExpression"] == null?string.Empty: ViewState["SortExpression"].ToString();
            if (this.HasControls())
            {
                if (!string.IsNullOrEmpty(SortExpression) && this.ShowHeader)
                {
                    Table table = this.Controls[0] as Table;

                    if (table != null && table.Rows.Count > 0)
                    {
                        // Need to check first TWO rows because the first row may be a
                        // pager row... 
                        GridViewRow headerRow = table.Rows[0] as GridViewRow;
                        if (headerRow.RowType != DataControlRowType.Header && table.Rows.Count > 1)
                            headerRow = table.Rows[1] as GridViewRow;
                        //check header
                        if (headerRow.RowType == DataControlRowType.Header)
                        {
                            foreach (TableCell cell in headerRow.Cells)
                            {
                                DataControlFieldCell gridViewCell = cell as DataControlFieldCell;
                                if (gridViewCell != null)
                                {
                                    DataControlField cellsField = gridViewCell.ContainingField;
                                    if (cellsField != null && cellsField.SortExpression == SortExpression)
                                    {
                                        // Add the sort arrows for this cell
                                        CreateSortArrows(cell);

                                        // We're done here!
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Create sort arrows.
        /// </summary>
        /// <param name="sortedCell"></param>
        protected virtual void CreateSortArrows(TableCell sortedCell)
        {
            // Add the appropriate arrow image and apply the appropriate state, depending on whether we're
            // sorting the results in ascending or descending order
            TableItemStyle sortStyle = null;
            string imgUrl = null;
            SortDirection sortDirection = (SortDirection)ViewState["SortDirection"];

            if (sortDirection == SortDirection.Ascending)
            {
                imgUrl = this.ArrowUpImageUrlInternal;
                sortStyle = _sortAscendingStyle;
            }
            else
            {
                imgUrl = this.ArrowDownImageUrlInternal;
                sortStyle = _sortDescendingStyle;
            }

            Image arrow = new Image();
            arrow.ImageUrl = imgUrl;
            arrow.ToolTip = sortDirection.ToString();
           // arrow.BorderStyle = BorderStyle.None;
            sortedCell.Controls.Add(arrow);
            sortedCell.Attributes.Add("noWrap", "true");

            if (sortStyle != null)
                sortedCell.MergeStyle(sortStyle);
        }

        #region State Management Methods
        /// <summary>
        /// save View state.
        /// </summary>
        /// <returns></returns>
        protected override object SaveViewState()
        {
            // We need to save any programmatic changes to the SortAscendingStyle or SortDescendingStyle
            // properties to view state...
            object[] state = new object[3];
            state[0] = base.SaveViewState();
            if (_sortAscendingStyle != null)
                state[1] = ((IStateManager)_sortAscendingStyle).SaveViewState();
            if (_sortDescendingStyle != null)
                state[2] = ((IStateManager)_sortDescendingStyle).SaveViewState();

            return state;
        }
        /// <summary>
        /// Load View State.
        /// </summary>
        /// <param name="savedState"></param>
        protected override void LoadViewState(object savedState)
        {
            object[] state = (object[])savedState;

            base.LoadViewState(state[0]);

            if (state[1] != null)
                ((IStateManager)this.SortAscendingStyle).LoadViewState(state[1]);
            if (state[2] != null)
                ((IStateManager)this.SortDescendingStyle).LoadViewState(state[2]);
        }
        /// <summary>
        /// Track view state.
        /// </summary>
        protected override void TrackViewState()
        {
            base.TrackViewState();

            if (_sortAscendingStyle != null)
                ((IStateManager)_sortAscendingStyle).TrackViewState();
            if (_sortDescendingStyle != null)
                ((IStateManager)_sortDescendingStyle).TrackViewState();
        }
        #endregion

        #endregion

        #endregion
    }
}
