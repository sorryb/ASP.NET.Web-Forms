using System;
using System.Collections;

namespace BugsTrackers.BusinessLogicLayer {


	//*********************************************************************
	//
	// RelatedIssueCollection Class
	//
	// The RelatedIssueCollection class represents a typed collection of
	// RelatedIssue objects.
	//
	//*********************************************************************


    public class RelatedIssueCollection:CollectionBase {

        public RelatedIssue this[ int index ] {
            get  { return( (RelatedIssue) List[index] );}
            set  { List[index] = value;}
        }

        public int Add( RelatedIssue value ) {
            return( List.Add( value ) );
        }

        public int IndexOf( RelatedIssue value ) {
            return( List.IndexOf( value ) );
        }

        public void Insert( int index, RelatedIssue value ) {
            List.Insert( index, value );
        }

        public void Remove( RelatedIssue value ) {
            List.Remove( value );
        }

        public bool Contains( RelatedIssue value ) {
            // If value is not of type Comment, this will return false.
            return( List.Contains( value ) );
        }

        protected override void OnInsert( int index, Object value ) {
            if ( value.GetType() != Type.GetType("BugsTrackers.BusinessLogicLayer.RelatedIssue") )
                throw new ArgumentException( "value must be of type Comment.", "value" );
        }

        protected override void OnRemove( int index, Object value ) {
            if ( value.GetType() != Type.GetType("BugsTrackers.BusinessLogicLayer.RelatedIssue") )
                throw new ArgumentException( "value must be of type Comment.", "value" );
        }

        protected override void OnSet( int index, Object oldValue, Object newValue ) {
            if ( newValue.GetType() != Type.GetType("BugsTrackers.BusinessLogicLayer.RelatedIssue") )
                throw new ArgumentException( "newValue must be of type Comment.", "newValue" );
        }

        protected override void OnValidate( Object value ) {
            if ( value.GetType() != Type.GetType("BugsTrackers.BusinessLogicLayer.RelatedIssue") )
                throw new ArgumentException( "value must be of type Comment." );
        }
  }
}
