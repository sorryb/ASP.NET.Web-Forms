using System;
using System.Collections;

namespace BugsTrackers.BusinessLogicLayer {


	//*********************************************************************
	//
	// IssueHistoryCollection Class
	//
	// The IssueCommentCollection class represents a typed collection of
	// IssueComment objects.
	//
	//*********************************************************************

    public class IssueHistoryCollection:CollectionBase {

        public IssueHistory this[ int index ] {
            get  { return( (IssueHistory) List[index] );}
            set  { List[index] = value;}
        }

        public int Add( IssueHistory value ) {
            return( List.Add( value ) );
        }

        public int IndexOf( IssueHistory value ) {
            return( List.IndexOf( value ) );
        }

        public void Insert( int index, IssueHistory value ) {
            List.Insert( index, value );
        }

        public void Remove( IssueHistory value ) {
            List.Remove( value );
        }

        public bool Contains( IssueHistory value ) {
            // If value is not of type Comment, this will return false.
            return( List.Contains( value ) );
        }

        protected override void OnInsert( int index, Object value ) {
            if ( value.GetType() != Type.GetType("BugsTrackers.BusinessLogicLayer.IssueHistory") )
                throw new ArgumentException( "value must be of type Comment.", "value" );
        }

        protected override void OnRemove( int index, Object value ) {
            if ( value.GetType() != Type.GetType("BugsTrackers.BusinessLogicLayer.IssueHistory") )
                throw new ArgumentException( "value must be of type Comment.", "value" );
        }

        protected override void OnSet( int index, Object oldValue, Object newValue ) {
            if ( newValue.GetType() != Type.GetType("BugsTrackers.BusinessLogicLayer.IssueHistory") )
                throw new ArgumentException( "newValue must be of type Comment.", "newValue" );
        }

        protected override void OnValidate( Object value ) {
            if ( value.GetType() != Type.GetType("BugsTrackers.BusinessLogicLayer.IssueHistory") )
                throw new ArgumentException( "value must be of type Comment." );
        }
  }
}
