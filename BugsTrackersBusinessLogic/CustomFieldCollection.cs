using System;
using System.Collections;

namespace BugsTrackers.BusinessLogicLayer {



	//*********************************************************************
	//
	// CustomFieldCollection Class
	//
	// Represents a typed collection of project custom fields.
	//
	//*********************************************************************

    public class CustomFieldCollection:CollectionBase {

        public CustomField this[ int index ]  {
            get  { return( (CustomField) List[index] );}
            set  { List[index] = value;}
        }

        public int Add( CustomField value )  {
            return( List.Add( value ) );
        }

        public int IndexOf( CustomField value )  {
            return( List.IndexOf( value ) );
        }

        public void Insert( int index, CustomField value ) {
            List.Insert( index, value );
        }

        public void Remove( CustomField value )  {
            List.Remove( value );
        }

        public bool Contains( CustomField value )  {
            // If value is not of type CustomField, this will return false.
            return( List.Contains( value ) );
        }

        protected override void OnInsert( int index, Object value )  {
            if ( value.GetType() != Type.GetType("BugsTrackers.BusinessLogicLayer.CustomField") )
                throw new ArgumentException( "value must be of type CustomField.", "value" );
        }

        protected override void OnRemove( int index, Object value )  {
            if ( value.GetType() != Type.GetType("BugsTrackers.BusinessLogicLayer.CustomField") )
                throw new ArgumentException( "value must be of type CustomField.", "value" );
        }

        protected override void OnSet( int index, Object oldValue, Object newValue )  {
            if ( newValue.GetType() != Type.GetType("BugsTrackers.BusinessLogicLayer.CustomField") )
                throw new ArgumentException( "newValue must be of type CustomField.", "newValue" );
        }

        protected override void OnValidate( Object value )  {
            if ( value.GetType() != Type.GetType("BugsTrackers.BusinessLogicLayer.CustomField") )
                throw new ArgumentException( "value must be of type CustomField." );
        }
    }
}
