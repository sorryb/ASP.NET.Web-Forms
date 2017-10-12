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

namespace BugsTrackers.Administration.Users
{
	/// <summary>
	/// Summary description for UserList.
	/// </summary>
	public partial class UserList : System.Web.UI.Page
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
			this.UsersGrid.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.UsersGrid_Page);
			this.UsersGrid.SortCommand += new System.Web.UI.WebControls.DataGridSortCommandEventHandler(this.UsersGrid_Sort);
			this.UsersGrid.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.UsersGrid_ItemDataBound);

		}
		#endregion

	
		//*********************************************************************
		//
		// UserList.aspx
		//
		// This page displays a list of all users.
		//
		//*********************************************************************


		protected void Page_Load(object sender, System.EventArgs e) 
		{

			if (!Page.IsPostBack)
				BindUsers();
		}


		//*********************************************************************
		//
		// The BindProjects method retrieves the list of projects the current user
		// can view based on their role and then databinds them to the ProjectsGrid
		//
		//*********************************************************************

		private void BindUsers()
		{
			ITUserCollection userList = ITUser.GetAllUsers();

			// Call method to sort the data before databinding
			SortGridData(userList, SortField, SortAscending);

			UsersGrid.DataSource = userList;
			UsersGrid.DataBind();
		}


		//*********************************************************************
		//
		// The ProjectsGrid_Page event handler sets the index of the currently displayed
		// page for the Projects grid and re-binds it.
		//
		//*********************************************************************

		protected void UsersGrid_Page(Object sender, DataGridPageChangedEventArgs e) 
		{
			UsersGrid.CurrentPageIndex = e.NewPageIndex;
			BindUsers();
		}

		//*********************************************************************
		//
		// The ProjectGrid_Sort event handler changes the sortfield for the Projects grid
		// and re-binds it.
		//
		//*********************************************************************

		private void UsersGrid_Sort(object source, System.Web.UI.WebControls.DataGridSortCommandEventArgs e) 
		{
			SortField = e.SortExpression;
			BindUsers();
		}


		//*******************************************************
		//
		// SortGridData methods sorts the Issues Grid based on which
		// sort field is being selected.  Also does reverse sorting based on the boolean.
		//
		//*******************************************************

		private void SortGridData(ITUserCollection list, string sortField, bool asc) 
		{
			ITUserCollection.UserFields sortCol = ITUserCollection.UserFields.InitValue;

			switch(sortField) 
			{
				case "Username":
					sortCol = ITUserCollection.UserFields.Username;
					break;
				case "DisplayName":
					sortCol = ITUserCollection.UserFields.DisplayName;
					break;
				case "RoleName":
					sortCol = ITUserCollection.UserFields.RoleName;
					break;
			}

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



		protected void AddUser(Object s, EventArgs e) 
		{
			Response.Redirect("UserDetail.aspx");

		}


		void UsersGrid_ItemDataBound(Object s, DataGridItemEventArgs e) 
		{
			if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem) 
			{
				ITUser objUser = (ITUser)e.Item.DataItem;
				Label lblRoleName = (Label)e.Item.FindControl("lblRoleName");
				lblRoleName.Text = objUser.RoleName;
				Label lblDisplayName = (Label)e.Item.FindControl("lblDisplayName");
				lblDisplayName.Text = objUser.DisplayName;
			}
		}

	
	}
}
