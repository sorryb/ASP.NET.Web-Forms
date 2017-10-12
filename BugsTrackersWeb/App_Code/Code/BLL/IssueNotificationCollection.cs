using System;
using System.Collections;

namespace BugsTrackers.BusinessLogicLayer {


	//*********************************************************************
	//
	// IssueNotificationCollection Class
	//
	// The IssueNotificationCollection class represents a typed collection of
	// IssueNotification objects.
	//
	//*********************************************************************

    public class IssueNotificationCollection:CollectionBase {


        public IssueNotification this[ int index ] {
            get  { return( (IssueNotification) List[index] );}
            set  { List[index] = value;}
        }

        public int Add( IssueNotification value ) {
            return( List.Add( value ) );
        }

        public int IndexOf( IssueNotification value ) {
            return( List.IndexOf( value ) );
        }

        public void Insert( int index, IssueNotification value ) {
            List.Insert( index, value );
        }

        public void Remove( IssueNotification value ) {
            List.Remove( value );
        }

        public bool Contains( IssueNotification value ) {
            // If value is not of type Comment, this will return false.
            return( List.Contains( value ) );
        }

        protected override void OnInsert( int index, Object value ) {
            if ( value.GetType() != Type.GetType("BugsTrackers.BusinessLogicLayer.IssueNotification") )
                throw new ArgumentException( "value must be of type Comment.", "value" );
        }

        protected override void OnRemove( int index, Object value ) {
            if ( value.GetType() != Type.GetType("BugsTrackers.BusinessLogicLayer.IssueNotification") )
                throw new ArgumentException( "value must be of type Comment.", "value" );
        }

        protected override void OnSet( int index, Object oldValue, Object newValue ) {
            if ( newValue.GetType() != Type.GetType("BugsTrackers.BusinessLogicLayer.IssueNotification") )
                throw new ArgumentException( "newValue must be of type Comment.", "newValue" );
        }

        protected override void OnValidate( Object value ) {
            if ( value.GetType() != Type.GetType("BugsTrackers.BusinessLogicLayer.IssueNotification") )
                throw new ArgumentException( "value must be of type Comment." );
        }
  }
}
