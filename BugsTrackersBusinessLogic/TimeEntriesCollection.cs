using System;
using System.Collections;

namespace BugsTrackers.BusinessLogicLayer
{
	//*********************************************************************
	//
	// TimeEntriesCollection Class 
	//
	// The TimeEntriesCollection is a Custom TimeEntry collection used 
	// to represet a list of TimeEntry objects.
	//
	//
	//*********************************************************************

	public class TimeEntriesCollection : ArrayList
	{
		public enum TimeEntryFields
		{
			InitValue,
			Day, 
			Category, 
			Project, 
			Hours, 
			Description
		}

		public void Sort(TimeEntryFields sortField, bool isAscending)
		{
			switch (sortField) 
			{
				case TimeEntryFields.Day:
					base.Sort(new DayComparer());
					break;
				case TimeEntryFields.Category:
					base.Sort(new CategoryComparer());
					break;
				case TimeEntryFields.Description:
					base.Sort(new DescriptionComparer());
					break;
				case TimeEntryFields.Hours:
					base.Sort(new HoursComparer());
					break;
				case TimeEntryFields.Project:
					base.Sort(new ProjectComparer());
					break;
			}

			if (!isAscending) base.Reverse();
		}

		private sealed class DayComparer : IComparer 
		{
			public int Compare(object x, object y)
			{
				TimeEntry first = (TimeEntry) x;
				TimeEntry second = (TimeEntry) y;
				return first.EntryDate.CompareTo(second.EntryDate);
			}
		}

		private sealed class CategoryComparer : IComparer 
		{
			public int Compare(object x, object y)
			{
				TimeEntry first = (TimeEntry) x;
				TimeEntry second = (TimeEntry) y;
				return first.CategoryName.CompareTo(second.CategoryName);
			}
		}

		private sealed class ProjectComparer : IComparer 
		{
			public int Compare(object x, object y)
			{
				TimeEntry first = (TimeEntry) x;
				TimeEntry second = (TimeEntry) y;
				return first.ProjectName.CompareTo(second.ProjectName);
			}
		}

		private sealed class DescriptionComparer : IComparer 
		{
			public int Compare(object x, object y)
			{
				TimeEntry first = (TimeEntry) x;
				TimeEntry second = (TimeEntry) y;
				return first.Description.CompareTo(second.Description);
			}
		}

		private sealed class HoursComparer : IComparer 
		{
			public int Compare(object x, object y)
			{
				TimeEntry first = (TimeEntry) x;
				TimeEntry second = (TimeEntry) y;
				return first.Duration.CompareTo(second.Duration);
			}
		}
	}
}
