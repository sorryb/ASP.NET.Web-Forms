using System;
using System.Collections;

namespace BugsTrackers.BusinessLogicLayer {


	//*********************************************************************
	//
	// QueryCollection Class
	//
	// The QueryCollection class represents a typed collection of
	// Query objects.
	//
	//*********************************************************************

    public class QueryCollection:CollectionBase {

        public Query this[ int index ] {
            get  { return( (Query) List[index] );}
            set  { List[index] = value;}
        }

        public int Add( Query value ) {
            return( List.Add( value ) );
        }

        public int IndexOf( Query value ) {
            return( List.IndexOf( value ) );
        }

        public void Insert( int index, Query value ) {
            List.Insert( index, value );
        }

        public void Remove( Query value ) {
            List.Remove( value );
        }

        public bool Contains( Query value ) {
            // If value is not of type Query, this will return false.
            return( List.Contains( value ) );
        }

        protected override void OnInsert( int index, Object value ) {
            if ( value.GetType() != Type.GetType("BugsTrackers.BusinessLogicLayer.Query") )
                throw new ArgumentException( "value must be of type Query.", "value" );
        }

        protected override void OnRemove( int index, Object value ) {
            if ( value.GetType() != Type.GetType("BugsTrackers.BusinessLogicLayer.Query") )
                throw new ArgumentException( "value must be of type Query.", "value" );
        }

        protected override void OnSet( int index, Object oldValue, Object newValue ) {
            if ( newValue.GetType() != Type.GetType("BugsTrackers.BusinessLogicLayer.Query") )
                throw new ArgumentException( "newValue must be of type Query.", "newValue" );
        }

        protected override void OnValidate( Object value ) {
            if ( value.GetType() != Type.GetType("BugsTrackers.BusinessLogicLayer.Query") )
                throw new ArgumentException( "value must be of type Query." );
        }

	}
}
