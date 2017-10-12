using System;
using System.Collections;

namespace BugsTrackers.BusinessLogicLayer
{
	//*********************************************************************
	//
	// ProjectsCollection Class
	//
	// ProjectsCollection class inherits from ArrayList.  It has its own implementation of Sort
	// base on the sortable Project fields
	//
	//*********************************************************************

	public class ProjectsCollection : ArrayList
	{
		public enum ProjectFields
		{
			InitValue,
			Name,
			ManagerUserName,
			CompletionDate,
			Duration
		}

		public void Sort(ProjectFields sortField, bool isAscending)
		{
			switch (sortField) 
			{
				case ProjectFields.Name:
					base.Sort(new NameComparer());
					break;
				case ProjectFields.ManagerUserName:
					base.Sort(new ManagerUserNameComparer());
					break;
				case ProjectFields.CompletionDate:
					base.Sort(new CompletionDateComparer());
					break;
				case ProjectFields.Duration:
					base.Sort(new DurationComparer());
					break;
			}

			if (!isAscending) base.Reverse();
		}

		private sealed class NameComparer : IComparer 
		{
			public int Compare(object x, object y)
			{
				Project first = (Project) x;
				Project second = (Project) y;
				return first.Name.CompareTo(second.Name);
			}
		}

		private sealed class ManagerUserNameComparer : IComparer 
		{
			public int Compare(object x, object y)
			{
				Project first = (Project) x;
				Project second = (Project) y;
				return first.ManagerUserName.CompareTo(second.ManagerUserName);
			}
		}

		private sealed class CompletionDateComparer : IComparer 
		{
			public int Compare(object x, object y)
			{
				Project first = (Project) x;
				Project second = (Project) y;
				return first.EstCompletionDate.CompareTo(second.EstCompletionDate);
			}
		}

		private sealed class DurationComparer : IComparer 
		{
			public int Compare(object x, object y)
			{
				Project first = (Project) x;
				Project second = (Project) y;
				return first.EstDuration.CompareTo(second.EstDuration);
			}
		}
	}
}
