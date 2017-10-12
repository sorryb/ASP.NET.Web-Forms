using System;
using System.Web;
using System.Web.UI;

using System.Web.UI.WebControls;
using System.Xml.XPath;
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
    /// SuperFish Menu control.
	/// </summary>
	/// 
    public class SuperFishMenu : System.Web.UI.WebControls.WebControl, INamingContainer
    {

        #region ----------private control members---------------------------------------------------


        private string _serverMapPath;
        private SiteMapMenus _menuType;
        //private OrientationStyleTypes _orientationStyle;
        private string _orientationStyleClass = "sf-menu";
        #endregion ----------private control members------------------------------------------------

        #region ----------protected control members---------------------------------------------------

        /// <summary>
        /// Label result local object.
        /// </summary>
        public Label LabelResult = new Label();

        #endregion ----------protected control members------------------------------------------------

        #region ----------public control methods---------------------------------------------------
        /// <summary>
        /// Server map path.
        /// </summary>
        [Category("Behavior")]
        [Description("Server map path.")]
        public string ServerMapPath
        {
            [DebuggerStepThrough()]
            get
            {
                return _serverMapPath;
            }
            [DebuggerStepThrough()]
            set
            {
                _serverMapPath = value;
            }
        }

        /// <summary>
        /// Menu Type.
        /// </summary>
        [Category("Behavior")]
        [Description("Menu Type.")]
        public SiteMapMenus MenuType
        {
            [DebuggerStepThrough()]
            get
            {
                return _menuType;
            }

            set
            {
                _menuType = value;
            }
        }

        /// <summary>
        /// Orientation Type.
        /// </summary>
        [Category("Behavior")]
        [Description("Orientation Type.")]
        public OrientationStyleTypes OrientationStyle
        {
            [DebuggerStepThrough()]
            get
            {
                if (_orientationStyleClass == "sf-menu")
                    return OrientationStyleTypes.Horisontal;
                else
                    return OrientationStyleTypes.Vertical;
            }

            set
            {
                if (value == OrientationStyleTypes.Horisontal)
                    _orientationStyleClass = "sf-menu";
                else
                    _orientationStyleClass = "sf-menu sf-vertical";
            }
        }


        public enum SiteMapMenus
        {
            Admin,
            Arhiv,
            Analyse,
            AccPayment,
            AccConfirm,
            MenuAdmin,
            MenuAcc,
            MenuSimple,
            NotSet
        }

        public enum OrientationStyleTypes
        {
            Horisontal,
            Vertical,

        }
        public Repeater menuRepeater = new Repeater();

        #endregion ----------public control methods---------------------------------------------------

        #region ----------private control methods---------------------------------------------------

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            InitMenuDataSource(_menuType, _serverMapPath);
        }

        /// <summary>
		/// This CreateChildControls should be used to write code for
		/// creating our web composite controls.
		/// </summary>
		protected override void CreateChildControls()
		{          
            //-----------------------add elements---------------------------------
            menuRepeater.ID = "menuRepeater";
            menuRepeater.ItemDataBound += menuRepeater_ItemDataBound;

            //RepeaterItemTemplate parentrepeatingRuleTemplate = new RepeaterItemTemplate();

            //menuRepeater.ItemTemplate = parentrepeatingRuleTemplate;

            HtmlGenericControl startDiv = new HtmlGenericControl("DIV");
            HtmlGenericControl startUl = new HtmlGenericControl("ul");
            //startUl.Attributes.Add("class", "sf-menu");
            startUl.Attributes.Add("class", _orientationStyleClass);
            startUl.Controls.Add(menuRepeater);
            startDiv.Controls.Add(startUl);

            Controls.Add(startDiv);

            //register client script.
           // if (this.Page != null && !this.Page.ClientScript.IsClientScriptIncludeRegistered(this.GetType(), "BugTrackerWebControls.jquery-1.2.6.min"))
           //     this.Page.ClientScript.RegisterClientScriptInclude(this.GetType(), "BugTrackerWebControls.jquery-1.2.6.min", this.Page.ClientScript.GetWebResourceUrl(this.GetType(), "BugTrackerWebControls.Scripts.jquery-1.2.6.min.js"));
           
            //if (this.Page != null && !this.Page.ClientScript.IsClientScriptIncludeRegistered(this.GetType(), "BugTrackerWebControls.jquery-1.6.2"))
            //     this.Page.ClientScript.RegisterClientScriptInclude(this.GetType(), "BugTrackerWebControls.jquery-1.6.2", this.Page.ClientScript.GetWebResourceUrl(this.GetType(), "BugTrackerWebControls.Scripts.jquery-1.6.2.js"));
  
            if (!Page.ClientScript.IsClientScriptIncludeRegistered("JQuery"))
                Page.ClientScript.RegisterClientScriptInclude("JQuery", Page.ClientScript.GetWebResourceUrl(this.GetType(), "BugTrackerWebControls.Scripts.jquery-1.6.2.js"));


            if (this.Page != null && !this.Page.ClientScript.IsClientScriptIncludeRegistered(this.GetType(), "BugTrackerWebControls.HoverIntent"))
                this.Page.ClientScript.RegisterClientScriptInclude(this.GetType(), "BugTrackerWebControls.HoverIntent", this.Page.ClientScript.GetWebResourceUrl(this.GetType(), "BugTrackerWebControls.Scripts.hoverIntent.js"));

            if (this.Page != null && !this.Page.ClientScript.IsClientScriptIncludeRegistered(this.GetType(), "BugTrackerWebControls.initmenu"))
                this.Page.ClientScript.RegisterClientScriptInclude(this.GetType(), "BugTrackerWebControls.initmenu", this.Page.ClientScript.GetWebResourceUrl(this.GetType(), "BugTrackerWebControls.Scripts.initmenu.js"));

            if (this.Page != null && !this.Page.ClientScript.IsClientScriptIncludeRegistered(this.GetType(), "BugTrackerWebControls.superfish"))
                this.Page.ClientScript.RegisterClientScriptInclude(this.GetType(), "BugTrackerWebControls.superfish", this.Page.ClientScript.GetWebResourceUrl(this.GetType(), "BugTrackerWebControls.Scripts.superfish.js"));

            if (this.Page != null && !this.Page.ClientScript.IsClientScriptIncludeRegistered(this.GetType(), "BugTrackerWebControls.supersubs"))
                this.Page.ClientScript.RegisterClientScriptInclude(this.GetType(), "BugTrackerWebControls.supersubs", this.Page.ClientScript.GetWebResourceUrl(this.GetType(), "BugTrackerWebControls.Scripts.supersubs.js"));


		}



        public void InitMenuDataSource(SiteMapMenus menu,string siteMapPath)
        {
            XmlDataSource menuData = new XmlDataSource();
            menuData.EnableCaching = false;
            menuData.XPath = "siteMap/siteMapNode";
            menuData.DataFile = GetMapPath(menu, siteMapPath);
            menuData.DataBind();
            menuRepeater.DataSource = menuData;
            menuRepeater.DataBind();
        }

        protected void menuRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            Repeater sRepeter = sender as Repeater;
            HyperLink link = new HyperLink();
            link.NavigateUrl = DataBinder.Eval(e.Item.DataItem, "url").ToString();
            link.Text = DataBinder.Eval(e.Item.DataItem, "title").ToString();
            string numNode = DataBinder.Eval(e.Item.DataItem, "description").ToString();

            HtmlGenericControl startLi = new HtmlGenericControl("li");

            Repeater subMenuRepeater = new Repeater();
            subMenuRepeater.ID = "subMenuRepeater";
            subMenuRepeater.ItemDataBound += subMenuRepeater_ItemDataBound;

            startLi.Controls.Add(link);
            if (numNode == "parent")
            {
                HtmlGenericControl startUl = new HtmlGenericControl("ul");
                startUl.Controls.Add(subMenuRepeater);
                startLi.Controls.Add(startUl);
            }
            else {
                startLi.Controls.Add(subMenuRepeater);
            }

            e.Item.Controls.Add(startLi);


            if (subMenuRepeater != null)
            {
                IXPathNavigable nav = e.Item.DataItem as IXPathNavigable;
                XPathNavigator xnav = nav.CreateNavigator();
                XPathNodeIterator iter = xnav.Select("siteMapNode/siteMapNode");
                if (iter != null && iter.Current != null && iter.Current.HasChildren)
                {
                    XmlDataSource subMenuData = new XmlDataSource();
                    subMenuData.EnableCaching = false;
                    subMenuData.Data = iter.Current.OuterXml;
                    subMenuData.XPath = "siteMapNode/siteMapNode";
                    subMenuData.DataBind();
                    subMenuRepeater.DataSource = subMenuData;
                    subMenuRepeater.DataBind();
                }
            }
        }

        protected void subMenuRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            Repeater sRepeter = sender as Repeater;
            HyperLink link = new HyperLink();
            link.NavigateUrl = DataBinder.Eval(e.Item.DataItem, "url").ToString();
            link.Text = DataBinder.Eval(e.Item.DataItem, "title").ToString();
            string numNode = DataBinder.Eval(e.Item.DataItem, "description").ToString();

            HtmlGenericControl startLi = new HtmlGenericControl("li");

            Repeater subSubMenuRepeater = new Repeater();
            subSubMenuRepeater.ID = "subSubMenuRepeater";
            subSubMenuRepeater.ItemDataBound += subSubMenuRepeater_ItemDataBound;

            startLi.Controls.Add(link);
            if (numNode == "parent")
            {
                HtmlGenericControl startUl = new HtmlGenericControl("ul");
                startUl.Controls.Add(subSubMenuRepeater);
                startLi.Controls.Add(startUl);
            }
            else
            {
                startLi.Controls.Add(subSubMenuRepeater);
            }

            e.Item.Controls.Add(startLi);


            if (subSubMenuRepeater != null)
            {
                IXPathNavigable nav = e.Item.DataItem as IXPathNavigable;
                XPathNavigator xnav = nav.CreateNavigator();
                XPathNodeIterator iter = xnav.Select("siteMapNode/siteMapNode");
                if (iter != null && iter.Current != null && iter.Current.HasChildren)
                {
                    XmlDataSource subMenuData = new XmlDataSource();
                    subMenuData.EnableCaching = false;
                    subMenuData.Data = iter.Current.OuterXml;
                    subMenuData.XPath = "siteMapNode/siteMapNode";
                    subMenuData.DataBind();
                    subSubMenuRepeater.DataSource = subMenuData;
                    subSubMenuRepeater.DataBind();
                }
            }
        }


        protected void subSubMenuRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            Repeater sRepeter = sender as Repeater;
            HyperLink link = new HyperLink();
            link.NavigateUrl = DataBinder.Eval(e.Item.DataItem, "url").ToString();
            link.Text = DataBinder.Eval(e.Item.DataItem, "title").ToString();
            string numNode = DataBinder.Eval(e.Item.DataItem, "description").ToString();

            HtmlGenericControl startLi = new HtmlGenericControl("li");

   
            startLi.Controls.Add(link);
            if (numNode == "parent")
            {
                HtmlGenericControl startUl = new HtmlGenericControl("ul");
                startLi.Controls.Add(startUl);
            }
            else
            {

            }

            e.Item.Controls.Add(startLi);


        }

        #region comments
        ///// <summary>
        ///// Gets the a main menu node.(level 0)
        ///// </summary>
        ///// <param name="url">The URL.</param>
        ///// <param name="title">The title.</param>
        ///// <returns>a markup link</returns>
        //protected string getMenuNode(object url, object title)
        //{
        //    string resolvedUrl = Page.ResolveUrl(url.ToString());
        //    return "<a href=\"" + resolvedUrl + "\">" + title.ToString() + "</a>";
        //}

        ///// <summary>
        ///// Gets the a subMenu node.(level 1)
        ///// </summary>
        ///// <param name="url">The URL.</param>
        ///// <param name="title">The title.</param>
        ///// <param name="desc">The description.</param>
        ///// <returns>a markup link</returns>
        //protected string getChildNode(object url, object title, object desc)
        //{
        //    string resolvedUrl = Page.ResolveUrl(url.ToString());
        //    string numNode = desc.ToString();
        //    string link = String.Concat("<li><a href=\"", resolvedUrl, "\">", title.ToString(), "</a></li>");

        //    if (numNode.Equals("begin"))
        //    {
        //        return String.Concat("<ul>", link);
        //    }
        //    else if (numNode.Equals("end"))
        //    {
        //        return String.Concat(link, "</ul>");
        //    }
        //    else if (numNode.Equals("parent"))
        //    {
        //        return String.Concat("<li><a href=\"", resolvedUrl, "\">", title.ToString(), "</a>");
        //    }
        //    else
        //    {
        //        return link;
        //    }
        //}

        ///// <summary>
        ///// Gets the a subSubMenu node.(level 2)
        ///// </summary>
        ///// <param name="url">The URL.</param>
        ///// <param name="title">The title.</param>
        ///// <param name="desc">The description.</param>
        ///// <returns>a markup link</returns>
        //protected string getSubChildNode(object url, object title, object desc)
        //{
        //    string resolvedUrl = Page.ResolveUrl(url.ToString());
        //    string numNode = desc.ToString();
        //    string link = String.Concat("<li><a href=\"", resolvedUrl, "\">", title.ToString(), "</a></li>");

        //    if (numNode.Equals("begin"))
        //    {
        //        return String.Concat("<ul>", link);
        //    }
        //    else if (numNode.Equals("end"))
        //    {
        //        return String.Concat(link, "</ul></li>");
        //    }
        //    else
        //    {
        //        return link;
        //    }
        //} 
        #endregion

        private string GetMapPath(SiteMapMenus menu, string serverMapPath)
        {
            //string serverMapPath = Server.MapPath("~");
            string path = String.Empty;
            switch (menu)
            {
                case SiteMapMenus.MenuAdmin:
                    path = serverMapPath + @"\maps\MenuAdmin.sitemap";
                    break;
                case SiteMapMenus.MenuAcc:
                    path = serverMapPath + @"\maps\MenuAcc.sitemap";
                    break;
                case SiteMapMenus.MenuSimple:
                    path = serverMapPath + @"\maps\MenuSimple.sitemap";
                    break;
                default:
                    break;
            }
            return path;
        }

        #endregion ----------private control methods---------------------------------------------------

        #region ----------classes  ---------------------------------------------------
        public class RepeaterItemTemplate : ITemplate
        {
            public void InstantiateIn(Control container)
            {
                PlaceHolder ph = new PlaceHolder();
             }

        }


        public class RepeatingRuleTemplate : ITemplate
        {
            ListItemType templateType;
            List<Control> innerControls;

            public RepeatingRuleTemplate(ListItemType type, List<Control> controls)
            {
                templateType = type;
                innerControls = controls;
            }



            public void InstantiateIn(Control container)
            {
                PlaceHolder ph = new PlaceHolder();

                switch (templateType)
                {
                    case ListItemType.Header:
                        ph.Controls.Add(new LiteralControl("<table border=\"0\">"));
                        ph.Controls.Add(new LiteralControl("<tr>"));
                        foreach (Control control in innerControls)
                        {
                            Label label = new Label();
                            label.Text = control.ID;
                            ph.Controls.Add(new LiteralControl("<td>"));
                            ph.Controls.Add(label);
                            ph.Controls.Add(new LiteralControl("</td>"));
                        }
                        ph.Controls.Add(new LiteralControl("</tr>"));
                        break;
                    case ListItemType.Item:
                        ph.Controls.Add(new LiteralControl("<tr>"));

                        foreach (Control control in innerControls)
                        {
                            //ph.Controls.Add(new LiteralControl("<td>")); 
                            //ph.Controls.Add(control as TextBox); 
                            //ph.Controls.Add(new LiteralControl("</td>")); 
                            if (control.GetType() != typeof(Repeater))
                            {
                                ph.Controls.Add(new LiteralControl("<td>"));
                                TextBox textBox = new TextBox();
                                textBox.ID = control.ID;
                                ph.Controls.Add(textBox);
                                ph.Controls.Add(new LiteralControl("</td>"));
                            }
                            else
                            {
                                ph.Controls.Add(new LiteralControl("<td>"));
                                Repeater rpt = new Repeater();
                                rpt.DataSource = (control as Repeater).DataSource;
                                rpt.ItemTemplate = (control as Repeater).ItemTemplate;
                                rpt.HeaderTemplate = (control as Repeater).HeaderTemplate;
                                rpt.FooterTemplate = (control as Repeater).FooterTemplate;
                                rpt.DataBind();
                                ph.Controls.Add(rpt);
                                //(control as Repeater).DataSource = new DataRow[4]; 
                                //   (control as Repeater).DataBind(); 
                                ph.Controls.Add(new LiteralControl("</td>"));
                            }
                        }
                        ph.Controls.Add(new LiteralControl("</tr>"));
                        //ph.DataBinding += new EventHandler(Item_DataBinding); 
                        break;
                    case ListItemType.Footer:
                        ph.Controls.Add(new LiteralControl("</table>"));
                        break;
                }
                container.Controls.Add(ph);
            }



            public List<Control> Controls
            {
                get
                {
                    return innerControls;
                }
            }

        }
        #endregion ----------classes  ---------------------------------------------------
    }
}
