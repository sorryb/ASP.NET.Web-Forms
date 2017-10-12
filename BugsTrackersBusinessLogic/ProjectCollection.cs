using System;
using System.Collections;
using BugsTrackers.DataAccessLayer;

namespace BugsTrackers.BusinessLogicLayer {


	//*********************************************************************
	//
	// ProjectCollection Class
	//
	// The ProjectCollection class represents a typed collection of
	// Project objects.
	//
	//*********************************************************************

    public class ProjectCollection : CollectionBase  {




        public enum ProjectFields {
	       InitValue,
	       Name,
	       Creator,
	       Manager,
	       DateCreated
	    }


	    public void Sort(ProjectFields sortField, bool isAscending) {
		  switch (sortField) {
			 case ProjectFields.Name:
				    InnerList.Sort(new NameComparer());
				    break;
			 case ProjectFields.Manager:
				    InnerList.Sort(new ManagerComparer());
				    break;
			 case ProjectFields.Creator:
				    InnerList.Sort(new CreatorComparer());
				    break;
			 case ProjectFields.DateCreated:
				    InnerList.Sort(new CreatedComparer());
				    break;
		  }
		  if (!isAscending) InnerList.Reverse();
	    }


	    private sealed class NameComparer : IComparer {
		  public int Compare(object x, object y) {
			 Project first = (Project) x;
			 Project second = (Project) y;
			 return first.Name.CompareTo(second.Name);
		  }
	    }


	    private sealed class ManagerComparer : IComparer {
		  public int Compare(object x, object y) {
			 Project first = (Project) x;
			 Project second = (Project) y;
			 return first.ManagerDisplayName.CompareTo(second.ManagerDisplayName);
		  }
	    }


	    private sealed class CreatorComparer : IComparer {
		  public int Compare(object x, object y) {
			 Project first = (Project) x;
			 Project second = (Project) y;
			 return first.CreatorDisplayName.CompareTo(second.CreatorDisplayName);
		  }
	    }



	    private sealed class CreatedComparer : IComparer {
		  public int Compare(object x, object y) {
			 Issue first = (Issue) x;
			 Issue second = (Issue) y;
			 return first.DateCreated.CompareTo(second.DateCreated);
		  }
        }



        public Project this[ int index ]  {
            get  { return( (Project) List[index] );}
            set  { List[index] = value;}
        }

        public int Add( Project value )  {
            return( List.Add( value ) );
        }

        public int IndexOf( Project value )  {
            return( List.IndexOf( value ) );
        }

        public void Insert( int index, Project value )  {
            List.Insert( index, value );
        }

        public void Remove( Project value )  {
            List.Remove( value );
        }

        public bool Contains( Project value )  {
            // If value is not of type Project, this will return false.
            return( List.Contains( value ) );
        }

        protected override void OnInsert( int index, Object value )  {
            if ( value.GetType() != Type.GetType("BugsTrackers.BusinessLogicLayer.Project") )
                throw new ArgumentException( "value must be of type Project.", "value" );
        }

        protected override void OnRemove( int index, Object value )  {
            if ( value.GetType() != Type.GetType("BugsTrackers.BusinessLogicLayer.Project") )
                throw new ArgumentException( "value must be of type Project.", "value" );
        }

        protected override void OnSet( int index, Object oldValue, Object newValue )  {
            if ( newValue.GetType() != Type.GetType("BugsTrackers.BusinessLogicLayer.Project") )
                throw new ArgumentException( "newValue must be of type Project.", "newValue" );
        }

        protected override void OnValidate( Object value )  {
            if ( value.GetType() != Type.GetType("BugsTrackers.BusinessLogicLayer.Project") )
                throw new ArgumentException( "value must be of type Project." );
        }
  }
}
