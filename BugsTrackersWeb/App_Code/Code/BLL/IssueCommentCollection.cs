using System;
using System.Collections;

namespace BugsTrackers.BusinessLogicLayer {


	//*********************************************************************
	//
	// IssueCommentCollection Class
	//
	// The IssueCommentCollection class represents a typed collection of
	// IssueComment objects.
	//
	//*********************************************************************

    public class IssueCommentCollection:CollectionBase {

        public IssueComment this[ int index ] {
            get  { return( (IssueComment) List[index] );}
            set  { List[index] = value;}
        }

        public int Add( IssueComment value ) {
            return( List.Add( value ) );
        }

        public int IndexOf( IssueComment value ) {
            return( List.IndexOf( value ) );
        }

        public void Insert( int index, IssueComment value ) {
            List.Insert( index, value );
        }

        public void Remove( IssueComment value ) {
            List.Remove( value );
        }

        public bool Contains( IssueComment value ) {
            // If value is not of type Comment, this will return false.
            return( List.Contains( value ) );
        }

        protected override void OnInsert( int index, Object value ) {
            if ( value.GetType() != Type.GetType("BugsTrackers.BusinessLogicLayer.IssueComment") )
                throw new ArgumentException( "value must be of type Comment.", "value" );
        }

        protected override void OnRemove( int index, Object value ) {
            if ( value.GetType() != Type.GetType("BugsTrackers.BusinessLogicLayer.IssueComment") )
                throw new ArgumentException( "value must be of type Comment.", "value" );
        }

        protected override void OnSet( int index, Object oldValue, Object newValue ) {
            if ( newValue.GetType() != Type.GetType("BugsTrackers.BusinessLogicLayer.IssueComment") )
                throw new ArgumentException( "newValue must be of type Comment.", "newValue" );
        }

        protected override void OnValidate( Object value ) {
            if ( value.GetType() != Type.GetType("BugsTrackers.BusinessLogicLayer.IssueComment") )
                throw new ArgumentException( "value must be of type Comment." );
        }


  }
}
