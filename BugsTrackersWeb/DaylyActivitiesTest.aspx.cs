using System;
using System.Collections;
using System.Data;
using System.Text;
using System.Globalization;
using System.Web.UI.WebControls;
using BugsTrackers.BusinessLogicLayer;

namespace BugsTrackers.Web
{
	//*********************************************************
	//
	// Main page of the Application, for users to enter time entries.
	//
	//*********************************************************

	public partial class DaylyActivities : System.Web.UI.Page
	{
		//Use Time Entry component to capture user's input during edit mode
		protected BusinessLogicLayer.TimeEntry	_userInput = new BusinessLogicLayer.TimeEntry(0, 0, 0, 0, DateTime.MinValue, null, -1M);
		protected DataTable						_dayListTable = null;

		
		private ITUser	 _user;
		private DateTime _weekEndingDate = DateTime.Today;
		private DateTime _weekStartingDate = DateTime.Today;
		public CultureInfo cultureRo = new CultureInfo("ro-RO");

		//*******************************************************
		//
		// The Page_Load server event handler on this page is used
		// to populate information fields on this page
		//
		//*******************************************************

		protected void Page_Load(object sender, System.EventArgs e)
		{
			_user = new ITUser(
				ITSecurity.GetUserID(), 
				User.Identity.Name, 
				ITSecurity.GetName(), 
				ITSecurity.GetUserRole(),
				"",
				""
				);

			if (!Page.IsPostBack)
			{
				// If page is loaded for the first time, select the current week.
				BusinessLogicLayer.TimeEntry.FillCorrectStartEndDates(DateTime.Today, 
					ref _weekStartingDate, ref _weekEndingDate);
				WeekEnding.Text = _weekEndingDate.ToShortDateString();

				BindUserList();
				BindEntryFields();
				BindTimeSheet(_user.UserID, _weekStartingDate, _weekEndingDate);
			}
			else
			{
				// Selects the weekending date, corresponding to week user selected.
				BusinessLogicLayer.TimeEntry.FillCorrectStartEndDates(Convert.ToDateTime(WeekEnding.Text), 
					ref _weekStartingDate, ref _weekEndingDate);
				_dayListTable = BusinessLogicLayer.TimeEntry.GetWeek(Convert.ToDateTime(WeekEnding.Text));
			}
		}
		
		//*******************************************************
		//
		// BindCategoryList method databinds data to CategoryList dropdown.
		//
		//*******************************************************

		private void BindCategoryList()
		{
			if (ProjectList.SelectedItem != null) 
			{
				// CategoryList is different for each project, Project.GetCategories gets a list of 
				// categories based on the project.
				CategoryList.DataSource = Project.GetCategories(Convert.ToInt32(ProjectList.SelectedItem.Value));
				CategoryList.DataValueField = "CategoryID";
				CategoryList.ToolTip = "Abbreviation";
                CategoryList.DataTextField = "Name";
				CategoryList.DataBind();
			}
		}

		//*******************************************************
		//
		// BindDates method populates the DropDownList Days to show each day of the week
		//
		//*******************************************************

		private void BindDates()
		{
			_dayListTable = BusinessLogicLayer.TimeEntry.GetWeek(Convert.ToDateTime(WeekEnding.Text));
			Days.DataSource = _dayListTable;
			Days.DataValueField = "Date";
			Days.DataTextField = "Day";
			Days.DataBind();
			CultureInfo cultureRo = new CultureInfo("ro-RO");
			Days.Items.FindByText(Convert.ToDateTime(DateTime.Today).ToString("dddd",cultureRo)).Selected= true;
		}

		//*******************************************************
		//
		// BindEntryFields calls methods that populate the drop down lists with data.
		//
		//*******************************************************

		private void BindEntryFields()
		{
			BindDates();
			BindProjectList();
			BindCategoryList();
		}
		
		//*******************************************************
		//
		// BindProjectList method populates the DropDownList ProjectList to show
		// list of projects that belongs to the user.
		//
		//*******************************************************

		private void BindProjectList()
		{
			// Preserve the selected value so information is still there after postback.
			if (ProjectList.SelectedItem != null)
				_userInput.ProjectID = Convert.ToInt32(ProjectList.SelectedItem.Value);

			ProjectList.DataSource = ListUserProjects();
			ProjectList.DataTextField = "Name";
			ProjectList.DataValueField = "ProjectID";
			ProjectList.DataBind();

			// Select the correct item from ProjectList.
			if (ProjectList.Items.FindByValue(_userInput.ProjectID.ToString()) != null)
				ProjectList.Items.FindByValue(_userInput.ProjectID.ToString()).Selected = true;
		}

		
		//*******************************************************
		//
		// An overload of BindTimeSheet method
		// this is called when the logged on user in querying time entry about themself.
		//
		//*******************************************************

		private void BindTimeSheet(int userID, DateTime start, DateTime end)
		{
			BindTimeSheet(userID, userID, start, end);
		}

		//*******************************************************
		//
		// BindTimeSheet method databinds the TimeEntryGrid based on the user
		// currently logged in and user which is being queried.
		//
		//*******************************************************

		private void BindTimeSheet(int queryUserID, int userID, DateTime start, DateTime end)
		{
			TimeEntriesCollection entryList = BusinessLogicLayer.TimeEntry.GetEntries(queryUserID, userID, start, end);

			// Sort datagrid if it is not empty.
			if (entryList != null) 
			{
				SortGridData(entryList, SortField, SortAscending);
			}
			// Draw time graph with new dataset.
			DrawTimeGraph(entryList, _dayListTable);

			TimeEntryGrid.DataSource = entryList;
			TimeEntryGrid.DataBind();
			
		}

		//*******************************************************
		//
		// BindUserList method databinds a list of users to the UserList;
		// This is based on the user currently logged in.
		//
		//*******************************************************

		private void BindUserList()
		{
			UserList.DataSource = ITUser.GetUsers(_user.UserID, _user.Role);
			UserList.DataBind();

			//Select User Drop Down to the correct value.
			UserList.Items.FindByValue(_user.UserID.ToString()).Selected=true;
		}

		//*******************************************************
		//
		// ClearEntryFields method clears all the entry fields on this page.
		//
		//*******************************************************

		private void ClearEntryFields()
		{
			BindEntryFields();

			// Make sure that no items appear in edit mode in the TimeEntryGrid.
			TimeEntryGrid.EditItemIndex = -1;

			BindTimeSheet(_user.UserID, Convert.ToInt32(UserList.SelectedItem.Value), _weekStartingDate, _weekEndingDate);
			Description.Text = string.Empty;
			Hours.Text = string.Empty;
		}
	
		//*******************************************************
		//
		// DrawTimeGraph method creates the bar graph on the fly.
		// Binds accordingly to number hours in each day of the week.
		//
		//*******************************************************

		private void DrawTimeGraph(TimeEntriesCollection chartData, DataTable dtWeek)
		{
			// Assume dtWeek is already in the correct order according to FirstDayOfWeek Setting.
			Hashtable htWeekSummary = new Hashtable(7);	//7 days of data - key: EntryDate | value: Duration.

			// x & y values holder
			StringBuilder xValues = new StringBuilder();
			StringBuilder yValues = new StringBuilder();

			//Initialize htWeekSummary to start with first day of week setting.
			foreach (DataRow row in dtWeek.Rows)
			{
				htWeekSummary.Add(Convert.ToDateTime(row["Date"]).ToShortDateString(), 0m);
			}

			// Summarize data from grid Group By Days of Week.
			foreach (BusinessLogicLayer.TimeEntry time in chartData)
			{
				string key = time.EntryDate.ToShortDateString();
				Decimal dayDuration = Convert.ToDecimal(htWeekSummary[key]);
				dayDuration += time.Duration;
				htWeekSummary[key] = dayDuration;
			}

			//Build x & y values for graph.
			int index = 1;
			CultureInfo cultureRo = new CultureInfo("ro-RO");
			foreach (DataRow row in dtWeek.Rows)
			{
				string key = Convert.ToDateTime(row["Date"]).ToShortDateString();
				string currentDay = Convert.ToDateTime(key).ToString("dddd",cultureRo);
				string currentHour = Convert.ToString(htWeekSummary[key]);

				xValues.Append(currentDay);
				yValues.Append(currentHour);

				//Don't append separator if this is the last item.
				if (index != dtWeek.Rows.Count)
				{
					xValues.Append("|");
					yValues.Append("|");
				}
				index++;
			}

			//Attach image generator to image.
			TimeGraph.ImageUrl = "TimeEntryBarChart.aspx?" + 
				"xValues=" + xValues.ToString() + 
				"&yValues=" + yValues.ToString();
		}
		
		//*******************************************************
		//
		// ListGridCategories method returns a List of categories based on the projectID.
		//
		//*******************************************************

		protected CategoriesCollection ListGridCategories(int projectID)
		{
			return Project.GetCategories(projectID);
		}

		//*******************************************************
		//
		// ListUserProjects method returns a ArrayList of available projects.
		//
		//*******************************************************

		protected ProjectsCollection ListUserProjects()
		{
			return BusinessLogicLayer.Project.GetProjects(_user.UserID, Convert.ToInt32(UserList.SelectedItem.Value));
		}

		//*******************************************************
		//
		// SortGridData methods sorts the TimeEntryGrid based on which
		// sort field is being selected.  Also does reverse sorting based on the boolean.
		//
		//*******************************************************

		private void SortGridData(TimeEntriesCollection list, string sortField, bool asc)
		{
			TimeEntriesCollection.TimeEntryFields sortCol = TimeEntriesCollection.TimeEntryFields.InitValue;

			switch(sortField)
			{
				case "EntryDate":
					sortCol = TimeEntriesCollection.TimeEntryFields.Day;
					break;
				case "ProjectName":
					sortCol = TimeEntriesCollection.TimeEntryFields.Project;
					break;
				case "CategoryName":
					sortCol = TimeEntriesCollection.TimeEntryFields.Category;
					break;
				case "Duration":
					sortCol = TimeEntriesCollection.TimeEntryFields.Hours;
					break;
				case "Description":
					sortCol = TimeEntriesCollection.TimeEntryFields.Description;
					break;
				default:
					break;
			}

			list.Sort(sortCol, asc);
		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		///	<summary>
		///	Required method for Designer support - do not modify
		///	the contents of this method with the code editor.
		///	</summary>
		private void InitializeComponent()
		{    
			this.TimeEntryGrid.CancelCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.TimeEntryGrid_OnCancel);
			this.TimeEntryGrid.EditCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.TimeEntryGrid_OnEdit);
			this.TimeEntryGrid.SortCommand += new System.Web.UI.WebControls.DataGridSortCommandEventHandler(this.TimeEntryGrid_Sort);
			this.TimeEntryGrid.UpdateCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.TimeEntryGrid_OnUpdate);
			this.TimeEntryGrid.DeleteCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.TimeEntryGrid_OnDelete);
			this.TimeEntryGrid.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.TimeEntryGrid_Itembound);

		}
		#endregion
		
		//*******************************************************
		//
		// AddEntry_Click server event handler on this page is used to 
		// saves a time entry and adds it to the system.
		//
		//*******************************************************

		protected void AddEntry_Click(object sender, System.EventArgs e)
		{
			// check validation.
			Requiredfieldvalidator1.Validate();
			RequiredFieldValidator2.Validate();
			RangeValidator1.Validate();
			CompareValidator1.Validate();
			
			// proceeds if data is valid then create an obejct and save it.
			if (Requiredfieldvalidator1.IsValid && RequiredFieldValidator2.IsValid && CompareValidator1.IsValid && RangeValidator1.IsValid) 
			{
				BusinessLogicLayer.TimeEntry te = new BusinessLogicLayer.TimeEntry(0, 
					Convert.ToInt32(UserList.SelectedItem.Value), 
					Convert.ToInt32(ProjectList.SelectedItem.Value),
					Convert.ToInt32(CategoryList.SelectedItem.Value), 
					Convert.ToDateTime(Days.SelectedItem.Value), 
					ITSecurity.CleanStringRegex(Description.Text), Convert.ToDecimal(Hours.Text));
				te.Save(); 
				
				ClearEntryFields();
			}
		}	
		
		//*******************************************************
		//
		// Cancel_Click server event handler on this page is used to
		// clear all entry fields on page.
		//
		//*******************************************************

		protected void Cancel_Click(object sender, System.EventArgs e)
		{
			ClearEntryFields();
		}

		//*******************************************************
		//
		// ProjectList_SelectedIndexChanged server event handler on this page is used to 
		// update the CategoryList dropdown when a different project is selected
		//
		//*******************************************************

		protected void ProjectList_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			BindCategoryList();
		}

		//*******************************************************
		//
		// TimeEntryGrid_Itembound server event handler on this page is used
		// during TimeEntryGrid databind
		//
		//*******************************************************

		private void TimeEntryGrid_Itembound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			// During in-line-editing mode this selects corresponding items for dropdowns and text boxes.
			// The _userInput object is created in TimeEntryGrid_OnEdit().
			if (e.Item.ItemType == ListItemType.EditItem) 
			{
				DropDownList currentCbo = (DropDownList) e.Item.FindControl("EntryDays");
				currentCbo.SelectedIndex = currentCbo.Items.IndexOf(currentCbo.Items.FindByText(_userInput.Day));

				currentCbo = (DropDownList) e.Item.FindControl("EntryProjects");
				currentCbo.SelectedIndex = currentCbo.Items.IndexOf(currentCbo.Items.FindByText(_userInput.ProjectName));

				currentCbo = (DropDownList) e.Item.FindControl("EntryCategories");
				currentCbo.SelectedIndex = currentCbo.Items.IndexOf(currentCbo.Items.FindByText(_userInput.CategoryName));				
			}
		}

		//*******************************************************
		//
		// TimeEntryGrid_OnCancel server event handler on this page is used to 
		// quit in-line-editing mode. and Undo all changes
		//
		//*******************************************************

		protected void TimeEntryGrid_OnCancel(Object sender, DataGridCommandEventArgs e)
		{
			TimeEntryGrid.EditItemIndex = -1;
			BindTimeSheet(_user.UserID, Convert.ToInt32(UserList.SelectedItem.Value), _weekStartingDate, _weekEndingDate);
		}
		
		//*******************************************************
		//
		// TimeEntryGrid_OnDelete server event handler on this page is used to 
		// delete a item from the database.
		//
		//*******************************************************

		protected void TimeEntryGrid_OnDelete(Object sender, DataGridCommandEventArgs e)
		{
			int entryLogID = Convert.ToInt32(TimeEntryGrid.DataKeys[(int)e.Item.ItemIndex]);
			BusinessLogicLayer.TimeEntry.Remove(entryLogID);
			BindTimeSheet(_user.UserID, Convert.ToInt32(UserList.SelectedItem.Value), _weekStartingDate, _weekEndingDate);
		}

		//*******************************************************
		//
		// TimeEntryGrid_OnEdit server event handler on this page is used to
		// start in-line-editing mode for TimeEntryGrid.
		//
		//*******************************************************

		protected void TimeEntryGrid_OnEdit(Object sender, DataGridCommandEventArgs e)
		{
			TimeEntryGrid.EditItemIndex = e.Item.ItemIndex;

			// Saves current info on temporary, use later in TimeEntryGrid_Itembound()
			_userInput.Day = ((Label) e.Item.FindControl("EntryDay")).Text;
			_userInput.ProjectName = ((Label) e.Item.FindControl("EntryProject")).Text.Trim();
			_userInput.ProjectID = Convert.ToInt32(((Label) e.Item.FindControl("EntryProjectID")).Text);
			_userInput.CategoryName = ((Label) e.Item.FindControl("EntryCategory")).Text.Trim();
			_userInput.Duration = Convert.ToDecimal(((Label) e.Item.FindControl("EntryDuration")).Text);
			_userInput.Description = ((Label) e.Item.FindControl("GridDescription")).Text.Trim();
			BindTimeSheet(_user.UserID, Convert.ToInt32(UserList.SelectedItem.Value), _weekStartingDate, _weekEndingDate);
		}

		//*******************************************************
		//
		// TimeEntryGrid_OnUpdate server event handler on this page is used to
		// update the changes made from in-line-editing.
		//
		//*******************************************************

		protected void TimeEntryGrid_OnUpdate(Object sender, DataGridCommandEventArgs e)
		{
			DataGridItem item = TimeEntryGrid.Items[TimeEntryGrid.EditItemIndex];

			RequiredFieldValidator required = (RequiredFieldValidator) item.FindControl("RequiredFieldValidatorGridHours");
			CompareValidator comparer = (CompareValidator) item.FindControl("CompareValidatorGridHours");

			required.Validate();
			comparer.Validate();
			
			// Check for validation, if all valid, update the object to data entered and save it.
			if (required.IsValid && comparer.IsValid) 
			{
				int entryLogID;
				int userID;
				int projectID;
				int categoryID;
				DateTime taskDate;
				string description;
				decimal duration;
				BusinessLogicLayer.TimeEntry te = null;

				// Fill in properties for TimeEntry object.
				entryLogID = Convert.ToInt32(TimeEntryGrid.DataKeys[(int)e.Item.ItemIndex]);
				userID = Convert.ToInt32(UserList.SelectedItem.Value);
				projectID = Convert.ToInt32(((DropDownList) e.Item.FindControl("EntryProjects")).SelectedItem.Value);
				categoryID = Convert.ToInt32(((DropDownList) e.Item.FindControl("EntryCategories")).SelectedItem.Value);
				taskDate = Convert.ToDateTime(((DropDownList) e.Item.FindControl("EntryDays")).SelectedItem.Value);
				description = ITSecurity.CleanStringRegex(((TextBox) e.Item.FindControl("EntryDescription")).Text);
				duration = Convert.ToDecimal(((TextBox) e.Item.FindControl("EntryHours")).Text);

				// Save the TimeEntry object.
				te = new BusinessLogicLayer.TimeEntry(entryLogID, userID, projectID, categoryID, taskDate, description, duration);
				te.Save();

				// Quit in-line-editing mode.
				TimeEntryGrid.EditItemIndex = -1;
				BindTimeSheet(_user.UserID, Convert.ToInt32(UserList.SelectedItem.Value), _weekStartingDate, _weekEndingDate);
			}
		}

		//*******************************************************
		//
		// TimeEntryGrid_Sort server event handler on this page is used to
		// sorts the TimeEntryGrid according to the sort field
		// being selected.
		//
		//*******************************************************
	
		private void TimeEntryGrid_Sort(object source, System.Web.UI.WebControls.DataGridSortCommandEventArgs e)
		{
			TimeEntryGrid.EditItemIndex = -1;
			SortField = e.SortExpression;
			BindTimeSheet(_user.UserID, Convert.ToInt32(UserList.SelectedItem.Value), _weekStartingDate, _weekEndingDate);
		}

		//*******************************************************
		//
		// UserProjects_OnChange server event handler on this page is called when user makes
		// a seletion change from project dropdown within in-line-editing, is used to 
		// update the category dropdown items according to the project selected
		//
		//*******************************************************

		protected void UserProjects_OnChange(object sender, System.EventArgs e)
		{
			DataGridItem item = TimeEntryGrid.Items[TimeEntryGrid.EditItemIndex];
			
			// Save temporary inputs so that data would be still there when user makes a Project selection
			// and cause postback.
			if (item != null)
			{
				TextBox txt = (TextBox) item.FindControl("EntryHours");
				_userInput.Duration = Convert.ToDecimal(txt.Text);
				
				TextBox txtDesc = (TextBox) item.FindControl("EntryDescription");
				_userInput.Description = txtDesc.Text;

				DropDownList EntryDays = (DropDownList)item.FindControl("EntryDays");
				_userInput.EntryDate = Convert.ToDateTime(EntryDays.SelectedItem.Value);
				_userInput.Day = EntryDays.SelectedItem.Text;

			}
			DropDownList EntryProjects = (DropDownList) sender;
			_userInput.ProjectID = Convert.ToInt32(EntryProjects.SelectedItem.Value);
			_userInput.ProjectName = EntryProjects.SelectedItem.Text;
			BindTimeSheet(_user.UserID, Convert.ToInt32(UserList.SelectedItem.Value), _weekStartingDate, _weekEndingDate);
		}
		
		//*******************************************************
		//
		// UserList_OnChange server event handler on this page is used
		// when a selection is changed in UserList DropDown
		// UserList items are limited to Role of the current user logged on the system.
		//
		//*******************************************************

		protected void UserList_OnChange(object sender, System.EventArgs e)
		{
			TimeEntryGrid.EditItemIndex = -1;
			BindTimeSheet(_user.UserID, Convert.ToInt32(UserList.SelectedItem.Value), _weekStartingDate, _weekEndingDate);
			BindEntryFields();
		}

		//*******************************************************
		//
		// WeekEnding_TextChanged server event handler on this page is used to
		// select the correct week starting date and week ending date
		// it gets called when user makes a selection change on calendar.
		//
		//*******************************************************

		protected void WeekEnding_TextChanged(object sender, System.EventArgs e)
		{
			TimeEntryGrid.EditItemIndex = -1;
            CultureInfo  culture = new CultureInfo("ro-RO");


            BusinessLogicLayer.TimeEntry.FillCorrectStartEndDates(Convert.ToDateTime(DatePickerControl.DateTextBox.Text, culture), 
				ref _weekStartingDate, ref _weekEndingDate);
            
            DatePickerControl.DateTextBox.Text = _weekEndingDate.ToString(culture).Replace(" 00:00:00", "");
			BindEntryFields();
			BindTimeSheet(_user.UserID, Convert.ToInt32(UserList.SelectedItem.Value), _weekStartingDate, _weekEndingDate);
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

	}
}
