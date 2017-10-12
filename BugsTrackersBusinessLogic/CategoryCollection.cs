using System;
using System.Collections;

namespace BugsTrackers.BusinessLogicLayer{



	//*********************************************************************
	//
	// CategoryCollection Class
	//
	// Represents a typed collection of Category objects.
	//
	//*********************************************************************


    public class CategoryCollection : CollectionBase {

        public Category this[ int index ]  {
            get  { return( (Category) List[index] ); }
            set  { List[index] = value;  }
        }

        public int Add( Category value )  {
            return( List.Add( value ) );
        }

        public int IndexOf( Category value )  {
            return( List.IndexOf( value ) );
        }

        public void Insert( int index, Category value ) {
            List.Insert( index, value );
        }

        public void Remove( Category value ) {
            List.Remove( value );
        }

        public bool Contains( Category value ) {
            // If value is not of type Category, this will return false.
            return( List.Contains( value ) );
        }

        protected override void OnInsert( int index, Object value ) {
            if ( value.GetType() != Type.GetType("BugsTrackers.BusinessLogicLayer.Category") )
                throw new ArgumentException( "value must be of type Category.", "value" );
        }

        protected override void OnRemove( int index, Object value )  {
            if ( value.GetType() != Type.GetType("BugsTrackers.BusinessLogicLayer.Category") )
                throw new ArgumentException( "value must be of type Category.", "value" );
        }

        protected override void OnSet( int index, Object oldValue, Object newValue )  {
            if ( newValue.GetType() != Type.GetType("BugsTrackers.BusinessLogicLayer.Category") )
                throw new ArgumentException( "newValue must be of type Category.", "newValue" );
        }

        protected override void OnValidate( Object value )  {
            if ( value.GetType() != Type.GetType("BugsTrackers.BusinessLogicLayer.Category") )
                throw new ArgumentException( "value must be of type Category." );
        }
  }
}
