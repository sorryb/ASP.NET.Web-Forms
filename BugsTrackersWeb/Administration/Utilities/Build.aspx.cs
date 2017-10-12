using System;
using System.Collections;
using System.Configuration;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using BugsTrackers.BusinessLogicLayer;
using BugsTrackers.Web ;
using System.Web.Security;
using System.Diagnostics;

namespace BugsTrackers.Administration.Utilities
{
	/// <summary>
	/// Summary description for UserDetail.
	/// </summary>
    public partial class Build : System.Web.UI.Page
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
        // UserDetail.aspx
        //
        // This page is used by Administrators when adding or editing issue tracker
        // users.
        //
        //*********************************************************************


        public int UserId
        {
            get
            {
                if (ViewState["UserId"] == null)
                    return 0;
                else
                    return (int)ViewState["UserId"];
            }

            set { ViewState["UserId"] = value; }
        }



        public bool IsCurrentUser
        {
            get
            {
                if (ViewState["IsCurrentUser"] == null)
                    return false;
                else
                    return (bool)ViewState["IsCurrentUser"];
            }

            set { ViewState["IsCurrentUser"] = value; }
        }



        protected void Page_Load(object sender, System.EventArgs e)
        {


        }

    }
}
