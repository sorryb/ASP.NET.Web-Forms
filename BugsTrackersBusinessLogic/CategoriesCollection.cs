using System;
using System.Collections;

namespace BugsTrackers.BusinessLogicLayer
{

	//*********************************************************************
	//
	// CategoriesCollection Class
	//
	// The CategoriesCollection Class inherits from ArrayList.  It has its own implemenation 
	// of Sort based on the sortable Category fields.
	//
	//*********************************************************************

	public class CategoriesCollection : ArrayList
	{
		public enum CategoryFields
		{
			Abbreviation,
			Duration,
			InitValue,
			Name
		}

		public void Sort(CategoryFields sortField, bool isAscending)
		{
			switch (sortField) 
			{
				case CategoryFields.Name:
					base.Sort(new NameComparer());
					break;
				case CategoryFields.Abbreviation:
					base.Sort(new AbbreviationComparer());
					break;
				case CategoryFields.Duration:
					base.Sort(new DurationComparer());
					break;
			}

			if (!isAscending) base.Reverse();
		}

		private sealed class NameComparer : IComparer 
		{
			public int Compare(object x, object y)
			{
				Category first = (Category) x;
				Category second = (Category) y;
				return first.Name.CompareTo(second.Name);
			}
		}

		private sealed class AbbreviationComparer : IComparer 
		{
			public int Compare(object x, object y)
			{
				Category first = (Category) x;
				Category second = (Category) y;
				//return first.Abbreviation.CompareTo(second.Abbreviation);//de vazut
				return 0;
			}
		}

		private sealed class DurationComparer : IComparer 
		{
			public int Compare(object x, object y)
			{
				Category first = (Category) x;
				Category second = (Category) y;
				//return first.EstDuration.CompareTo(second.EstDuration);
				return 0;
			}
		}
	}
}

