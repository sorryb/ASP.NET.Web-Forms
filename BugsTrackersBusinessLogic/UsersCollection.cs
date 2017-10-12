using System;
using System.Collections;

namespace BugsTrackers.BusinessLogicLayer
{
	//*********************************************************************
	//
	// UsersCollection Class 
	//
	// The UsersCollection is a Custom User collection used 
	// to represet a list of User objects.
	//
	//
	//*********************************************************************

	public class UsersCollection : ArrayList
	{
		public enum UserFields
		{
			InitValue,
			Name, 
			RoleName
		}

		public void Sort(UserFields sortField, bool isAscending)
		{
			switch (sortField) 
			{
				case UserFields.Name:
					base.Sort(new NameComparer());
					break;
				case UserFields.RoleName:
					base.Sort(new RoleNameComparer());
					break;
			}
			if (!isAscending) base.Reverse();
		}

		private sealed class NameComparer : IComparer 
		{
			public int Compare(object x, object y)
			{
				ITUser first = (ITUser) x;
				ITUser second = (ITUser) y;
				return first.Name.CompareTo(second.Name);
			}
		}

		private sealed class RoleNameComparer : IComparer 
		{
			public int Compare(object x, object y)
			{
				ITUser first = (ITUser) x;
				ITUser second = (ITUser) y;
				return first.RoleName.CompareTo(second.RoleName);
			}
		}
	}
}
