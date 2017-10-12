using System;
using System.Collections;
using BugsTrackers.DataAccessLayer;

namespace BugsTrackers.BusinessLogicLayer {


	//*********************************************************************
	//
	// ITUserCollection Class
	//
	// The ITUserCollection class represents a typed collection of
	// ITUser objects.
	//
	//*********************************************************************


    public class ITUserCollection : CollectionBase {



        public enum UserFields {
	       InitValue,
	       Username,
	       DisplayName,
	       RoleName
	    }


	    public void Sort(UserFields sortField, bool isAscending) {
		  switch (sortField) {
			 case UserFields.Username:
				    InnerList.Sort(new UsernameComparer());
				    break;
			 case UserFields.DisplayName:
				    InnerList.Sort(new DisplayNameComparer());
				    break;
			 case UserFields.RoleName:
				    InnerList.Sort(new RoleNameComparer());
				    break;
		  }
		  if (!isAscending) InnerList.Reverse();
	    }


	    private sealed class UsernameComparer : IComparer {
		  public int Compare(object x, object y) {
			 ITUser first = (ITUser) x;
			 ITUser second = (ITUser) y;
			 return first.Username.CompareTo(second.Username);
		  }
	    }



	    private sealed class DisplayNameComparer : IComparer {
		  public int Compare(object x, object y) {
			 ITUser first = (ITUser) x;
			 ITUser second = (ITUser) y;
			 return first.DisplayName.CompareTo(second.DisplayName);
		  }
	    }


	    private sealed class RoleNameComparer : IComparer {
		  public int Compare(object x, object y) {
			 ITUser first = (ITUser) x;
			 ITUser second = (ITUser) y;
			 return first.RoleName.CompareTo(second.RoleName);
		  }
	    }



        public ITUser this[ int index ] {
            get  { return( (ITUser) List[index] );}
            set  { List[index] = value;}
        }

        public int Add( ITUser value ) {
            return( List.Add( value ) );
        }

        public int IndexOf( ITUser value ) {
            return( List.IndexOf( value ) );
        }

        public void Insert( int index, ITUser value ) {
            List.Insert( index, value );
        }

        public void Remove( ITUser value ) {
            List.Remove( value );
        }

        public bool Contains( ITUser value ) {
            // If value is not of type ITUser, this will return false.
            return( List.Contains( value ) );
        }

        protected override void OnInsert( int index, Object value ) {
            if ( value.GetType() != Type.GetType("BugsTrackers.BusinessLogicLayer.ITUser") )
                throw new ArgumentException( "value must be of type ITUser.", "value" );
        }

        protected override void OnRemove( int index, Object value ) {
            if ( value.GetType() != Type.GetType("BugsTrackers.BusinessLogicLayer.ITUser") )
                throw new ArgumentException( "value must be of type ITUser.", "value" );
        }

        protected override void OnSet( int index, Object oldValue, Object newValue ) {
            if ( newValue.GetType() != Type.GetType("BugsTrackers.BusinessLogicLayer.ITUser") )
                throw new ArgumentException( "newValue must be of type ITUser.", "newValue" );
        }

        protected override void OnValidate( Object value ) {
            if ( value.GetType() != Type.GetType("BugsTrackers.BusinessLogicLayer.ITUser") )
                throw new ArgumentException( "value must be of type ITUser." );
        }
  }
}
