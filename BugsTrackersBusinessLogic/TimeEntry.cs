using System;
using System.Data;
using System.Configuration;
using System.Globalization;
using BugsTrackers.DataAccessLayer;
using System.Globalization;


namespace BugsTrackers.BusinessLogicLayer
{
	//*********************************************************************
	//
	// TimeEntry Class
	//
	// The TimeEntry class is used to represent a time entry along with its
	// properties
	//
	//*********************************************************************

	public class TimeEntry
	{
		private int			_categoryID;
		private string		_categoryName;
		private string		_categoryShortName;
		private string		_day;
		private string		_description;
		private decimal		_duration;
		private DateTime	_entryDate;
		private int			_entryLogID;
		private int			_projectID;
		private string		_projectName;
		private int			_userID;

		public TimeEntry()
		{
		
		}
		public TimeEntry(int entryLogID)
		{
			_entryLogID = entryLogID;
		}

		public TimeEntry(int entryLogID, int userID, int ProjectID, int CategoryID, DateTime EntryDate, string Description, decimal Duration)
		{
			_entryLogID = entryLogID;
			_userID = userID;
			_projectID = ProjectID;
			_categoryID = CategoryID;
			_entryDate = EntryDate;
			_description = Description;
			_duration = Duration;
		}

		public int CategoryID
		{
			get { return _categoryID; }
			set { _categoryID = value; }
		}

		public string CategoryName
		{
			get { return _categoryName; }
			set { _categoryName = value; }
		}
		
		public string CategoryShortName
		{
			get { return _categoryShortName; }
			set { _categoryShortName = value; }
		}

		public string Day
		{
			get { return _day; }
			set { _day = value; }
		}

		public string Description
		{
			get { return _description; }
			set { _description = value; }
		}

		public decimal Duration
		{
			get { return _duration; }
			set { _duration = value; }
		}

		public DateTime EntryDate
		{
			get { return _entryDate; }
			set { _entryDate = value; }
		}
		
		public int EntryLogID 
		{
			get { return _entryLogID; }
			set { _entryLogID = value; }
		}

		public int ProjectID
		{
			get { return _projectID; }
			set { _projectID = value; }
		}

		public string ProjectName
		{
			get { return _projectName; }
			set { _projectName = value; }
		}

		//*********************************************************************
		//
		// FillCorrectStartEndDates Static Method
		//
		// The TimeEntry.FillCorrectStartEndDates Method returns the first 
		// and last date of a week, given any date during the week.
		// This method is primary used in TimeEntry page to calculate the range
		// of a week.
		//
		//*********************************************************************

		public static void FillCorrectStartEndDates(DateTime selectedDate, ref DateTime startDate, ref DateTime endDate)
		{
			int firstDayOfWeek = 1;//Convert.ToInt32(ConfigurationSettings.AppSettings[Web.Global.CfgKeyFirstDayOfWeek]);
			
			// Loop through 7 days of the week.
			for(int i = 0; i<7; i++) 
			{
				// Match first day of the week from global setting
				if (Convert.ToInt32(selectedDate.AddDays(i).DayOfWeek) == firstDayOfWeek)
				{
					// Fill in correct start and end dates
					startDate = selectedDate.AddDays(i);
					if(i!=0)
						startDate = startDate.AddDays(-7);
					endDate = startDate.AddDays(6);
					break;
				}
			}
		}
		
		//*********************************************************************
		//
		// GetEntries Static Method
		//
		// The TimeEntry.GetEntries Method returns a Collection of TimeEntries from a particular user,
		// for a specified period of time, based on the role of the current user browsing the application.
		//
		//*********************************************************************

		public static TimeEntriesCollection GetEntries(int queryUserID, int userID, DateTime startDate, DateTime endDate)
		{
			DataSet dsData = SqlHelper.ExecuteDataset(
				ConfigurationSettings.AppSettings[Web.Global.CfgKeyConnString], 
				"ListTimeEntries", queryUserID, userID, startDate, endDate);
			TimeEntriesCollection entryList = new TimeEntriesCollection();

			CultureInfo cultureRo = new CultureInfo("ro-RO");

			// Separate Data into a collection of TimeEntrys.
			foreach(DataRow row in dsData.Tables[0].Rows)
			{
				TimeEntry time = new TimeEntry();
				time.EntryLogID = Convert.ToInt32(row["EntryLogID"]);
				time.Description = row["Description"].ToString();
				time.Duration = Convert.ToDecimal(row["Duration"]);
				time.EntryDate = Convert.ToDateTime(row["EntryDate"],cultureRo);//.ToString("dddd",cultureRo);
				time.ProjectID = Convert.ToInt32(row["ProjectID"]);
				time.CategoryID = Convert.ToInt32(row["CategoryID"]);
				time.CategoryName = row["CategoryName"].ToString();
				time.ProjectName = row["ProjectName"].ToString();
				time.Day = time.EntryDate.ToString("dddd");
				time.CategoryShortName = row["CatShortName"].ToString(); 

				entryList.Add(time);
			}
			return entryList;
		}

		//*********************************************************************
		//
		// GetWeek Static Method
		//
		// The TimeEntry.GetWeek Method return a table of dates in a week, 
		// given any date during the week, this table contains 2 columns, 
		// one for Day values, and one for Date values
		// This method is primary used in Databinding DropDowns in TimeEntry page
		// with range of a week. 
		//
		//*********************************************************************

		public static DataTable GetWeek(DateTime selectedDate)
		{		
			DateTime start = DateTime.MinValue;
			DateTime end = DateTime.MinValue;
			DataTable dt = new DataTable();
			dt.Columns.Add("Day");
			dt.Columns.Add("Date");
					
			FillCorrectStartEndDates(selectedDate, ref start, ref end);
			DataRow workRow;

			CultureInfo cultureRo = new CultureInfo("ro-RO");
			string x = start.ToString("dddd",cultureRo);

			//  fill table with dates and days
			for (int i = 0; i < 7; i++)
			{
				workRow = dt.NewRow();
				workRow["Day"] = Convert.ToDateTime(start.AddDays(i)).ToString("dddd",cultureRo);
				workRow["Date"] = start.AddDays(i);
				dt.Rows.Add(workRow);
			}
			return dt;
		}

		//*********************************************************************
		//
		// Remove Static Method
		//
		// The TimeEntry.Remove Method removes a time entry from database
		// based on entry log ID.
		// 
		//
		//*********************************************************************

		public static void Remove(int entryLogID)
		{
			SqlHelper.ExecuteNonQuery(ConfigurationSettings.AppSettings[Web.Global.CfgKeyConnString], "DeleteTimeEntry", entryLogID);
		}

		//*********************************************************************
		//
		// Load Method
		//
		// The TimeEntry.Load Method populates current instance of TimeEntry object with 
		// info from the database.
		//
		//*********************************************************************

		public void Load()
		{
			DataSet ds = SqlHelper.ExecuteDataset(ConfigurationSettings.AppSettings[Web.Global.CfgKeyConnString], "GetTimeEntry", _entryLogID);
			DataRow row = ds.Tables[0].Rows[0];
			_description = row["Description"].ToString();
			_duration = Convert.ToDecimal(row["Duration"]);
			_entryDate = Convert.ToDateTime(row["EntryDate"]);
			_projectID = Convert.ToInt32(row["ProjectID"]);
			_userID = Convert.ToInt32(row["UserID"]);
			_categoryID = Convert.ToInt32(row["CategoryID"]);
			_projectName = row["ProjectName"].ToString();
		}

		//*********************************************************************
		//
		// Save Method
		//
		// The TimeEntry.Save Method Add or update TimeEntry information 
		// in the database depending on the entryLogID.
		// Returns True if saved successfully, false otherwise.
		//
		//*********************************************************************

		public bool Save()
		{
			if (_entryLogID == 0)
				return Insert();
			else
				return Update();
		}

		private bool Insert()
		{
			_entryLogID = Convert.ToInt32(SqlHelper.ExecuteScalar(ConfigurationSettings.AppSettings[Web.Global.CfgKeyConnString], "AddTimeEntry", _userID, _projectID, _categoryID,
				_entryDate, _description, _duration));
			return (0 < _entryLogID);
		}
		private bool Update()
		{
			try
			{
				SqlHelper.ExecuteNonQuery(ConfigurationSettings.AppSettings[Web.Global.CfgKeyConnString], "UpdateTimeEntry", _entryLogID, _userID, _projectID, _categoryID,
				 _entryDate, _description, _duration);
				return true;
			}
			catch {
				return false; }
		}
	}
}
