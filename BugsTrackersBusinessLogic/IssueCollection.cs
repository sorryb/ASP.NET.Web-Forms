using System;
using System.Collections;

namespace BugsTrackers.BusinessLogicLayer {



	//*********************************************************************
	//
	// IssueCollection Class
	//
	// The IssueCollection class represents a strongly typed collection
	// of issues.
	//
	//*********************************************************************


    public class IssueCollection:CollectionBase {

        public enum IssueFields {
	       InitValue,
	       Id,
	       Category,
	       Title,
           Assigned,
	       Owner,
	       Creator,
	       Priority,
           Status,
           Milestone,
           Created
	    }


	    public void Sort(IssueFields sortField, bool isAscending) {
		  switch (sortField) {
			 case IssueFields.Id:
				    InnerList.Sort(new IdComparer());
				    break;
			 case IssueFields.Title:
				    InnerList.Sort(new TitleComparer());
				    break;
			 case IssueFields.Category:
				    InnerList.Sort(new CategoryComparer());
				    break;
			 case IssueFields.Assigned:
				    InnerList.Sort(new AssignedComparer());
				    break;
			 case IssueFields.Creator:
				    InnerList.Sort(new CreatorComparer());
				    break;
			 case IssueFields.Owner:
				    InnerList.Sort(new OwnerComparer());
				    break;
			 case IssueFields.Priority:
				    InnerList.Sort(new PriorityComparer());
				    break;
			 case IssueFields.Status:
				    InnerList.Sort(new StatusComparer());
				    break;
			 case IssueFields.Milestone:
				    InnerList.Sort(new MilestoneComparer());
				    break;
			 case IssueFields.Created:
				    InnerList.Sort(new CreatedComparer());
				    break;
		  }
		  if (!isAscending) InnerList.Reverse();
	    }



	    private sealed class IdComparer : IComparer {
		  public int Compare(object x, object y) {
			 Issue first = (Issue) x;
			 Issue second = (Issue) y;
			 return first.Id - second.Id;
		  }
	    }


	    private sealed class TitleComparer : IComparer {
		  public int Compare(object x, object y) {
			 Issue first = (Issue) x;
			 Issue second = (Issue) y;
			 return first.Title.CompareTo(second.Title);
		  }
	    }


	    private sealed class CategoryComparer : IComparer {
		  public int Compare(object x, object y) {
			 Issue first = (Issue) x;
			 Issue second = (Issue) y;
			 return first.CategoryName.CompareTo(second.CategoryName);
		  }
	    }

	    private sealed class AssignedComparer : IComparer {
		  public int Compare(object x, object y) {
			 Issue first = (Issue) x;
			 Issue second = (Issue) y;
			 return first.AssignedDisplayName.CompareTo(second.AssignedDisplayName);
		  }
	    }



	    private sealed class CreatorComparer : IComparer {
		  public int Compare(object x, object y) {
			 Issue first = (Issue) x;
			 Issue second = (Issue) y;
			 return first.CreatorDisplayName.CompareTo(second.CreatorDisplayName);
		  }
	    }


	    private sealed class OwnerComparer : IComparer {
		  public int Compare(object x, object y) {
			 Issue first = (Issue) x;
			 Issue second = (Issue) y;
			 return first.OwnerDisplayName.CompareTo(second.OwnerDisplayName);
		  }
	    }


	    private sealed class PriorityComparer : IComparer {
		  public int Compare(object x, object y) {
			 Issue first = (Issue) x;
			 Issue second = (Issue) y;
			 return first.PriorityName.CompareTo(second.PriorityName);
		  }
	    }


	    private sealed class StatusComparer : IComparer {
		  public int Compare(object x, object y) {
			 Issue first = (Issue) x;
			 Issue second = (Issue) y;
			 return first.StatusName.CompareTo(second.StatusName);
		  }
	    }


	    private sealed class MilestoneComparer : IComparer {
		  public int Compare(object x, object y) {
			 Issue first = (Issue) x;
			 Issue second = (Issue) y;
			 return first.MilestoneName.CompareTo(second.MilestoneName);
		  }
        }



	    private sealed class CreatedComparer : IComparer {
		  public int Compare(object x, object y) {
			 Issue first = (Issue) x;
			 Issue second = (Issue) y;
			 return first.DateCreated.CompareTo(second.DateCreated);
		  }
        }


        public Issue this[ int index ] {
            get  { return( (Issue) List[index] );}
            set  { List[index] = value;}
        }

        public int Add( Issue value ) {
            return( List.Add( value ) );
        }

        public int IndexOf( Issue value ) {
            return( List.IndexOf( value ) );
        }

        public void Insert( int index, Issue value ) {
            List.Insert( index, value );
        }

        public void Remove( Issue value ) {
            List.Remove( value );
        }

        public bool Contains( Issue value ) {
            // If value is not of type Issue, this will return false.
            return( List.Contains( value ) );
        }

        protected override void OnInsert( int index, Object value ) {
            if ( value.GetType() != Type.GetType("BugsTrackers.BusinessLogicLayer.Issue") )
                throw new ArgumentException( "value must be of type Issue.", "value" );
        }

        protected override void OnRemove( int index, Object value ) {
            if ( value.GetType() != Type.GetType("BugsTrackers.BusinessLogicLayer.Issue") )
                throw new ArgumentException( "value must be of type Issue.", "value" );
        }

        protected override void OnSet( int index, Object oldValue, Object newValue ) {
            if ( newValue.GetType() != Type.GetType("BugsTrackers.BusinessLogicLayer.Issue") )
                throw new ArgumentException( "newValue must be of type Issue.", "newValue" );
        }

        protected override void OnValidate( Object value ) {
            if ( value.GetType() != Type.GetType("BugsTrackers.BusinessLogicLayer.Issue") )
                throw new ArgumentException( "value must be of type Issue." );
        }
  }
}
