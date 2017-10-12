using System;
using System.Collections;

namespace BugsTrackers.BusinessLogicLayer {


	//*********************************************************************
	//
	// QueryClauseCollection Class
	//
	// The QueryClauseCollection class represents a typed collection of
	// QueryClause objects.
	//
	//*********************************************************************

    public class QueryClauseCollection:CollectionBase {

        public QueryClause this[ int index ]  {
            get  { return( (QueryClause) List[index] );}
            set  { List[index] = value;}
        }

        public int Add( QueryClause value )  {
            return( List.Add( value ) );
        }

        public int IndexOf( QueryClause value )  {
            return( List.IndexOf( value ) );
        }

        public void Insert( int index, QueryClause value )  {
            List.Insert( index, value );
        }

        public void Remove( QueryClause value )  {
            List.Remove( value );
        }

        public bool Contains( QueryClause value ) {
            // If value is not of type QueryClause, this will return false.
            return( List.Contains( value ) );
        }

        protected override void OnInsert( int index, Object value )  {
            if ( value.GetType() != Type.GetType("BugsTrackers.BusinessLogicLayer.QueryClause") )
                throw new ArgumentException( "value must be of type QueryClause.", "value" );
        }

        protected override void OnRemove( int index, Object value )  {
            if ( value.GetType() != Type.GetType("BugsTrackers.BusinessLogicLayer.QueryClause") )
                throw new ArgumentException( "value must be of type QueryClause.", "value" );
        }

        protected override void OnSet( int index, Object oldValue, Object newValue )  {
            if ( newValue.GetType() != Type.GetType("BugsTrackers.BusinessLogicLayer.QueryClause") )
                throw new ArgumentException( "newValue must be of type QueryClause.", "newValue" );
        }

        protected override void OnValidate( Object value )  {
            if ( value.GetType() != Type.GetType("BugsTrackers.BusinessLogicLayer.QueryClause") )
                throw new ArgumentException( "value must be of type QueryClause." );
        }
  }
}
