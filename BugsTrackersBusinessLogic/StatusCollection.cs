using System;
using System.Collections;
using BugsTrackers.DataAccessLayer;

namespace BugsTrackers.BusinessLogicLayer {


	//*********************************************************************
	//
	// StatusCollection Class
	//
	// The StatusCollection class represents a typed collection of
	// Status objects.
	//
	//*********************************************************************


    public class StatusCollection:CollectionBase {

        public Status this[ int index ]  {
            get  { return( (Status) List[index] );}
            set  { List[index] = value;}
        }

        public int Add( Status value )  {
            return( List.Add( value ) );
        }

        public int IndexOf( Status value )  {
            return( List.IndexOf( value ) );
        }

        public void Insert( int index, Status value )  {
            List.Insert( index, value );
        }

        public void Remove( Status value )  {
            List.Remove( value );
        }

        public bool Contains( Status value )  {
            // If value is not of type Status, this will return false.
            return( List.Contains( value ) );
        }

        protected override void OnInsert( int index, Object value )  {
            if ( value.GetType() != Type.GetType("BugsTrackers.BusinessLogicLayer.Status") )
                throw new ArgumentException( "value must be of type Status.", "value" );
        }

        protected override void OnRemove( int index, Object value )  {
            if ( value.GetType() != Type.GetType("BugsTrackers.BusinessLogicLayer.Status") )
                throw new ArgumentException( "value must be of type Status.", "value" );
        }

        protected override void OnSet( int index, Object oldValue, Object newValue )  {
            if ( newValue.GetType() != Type.GetType("BugsTrackers.BusinessLogicLayer.Status") )
                throw new ArgumentException( "newValue must be of type Status.", "newValue" );
        }

        protected override void OnValidate( Object value )  {
            if ( value.GetType() != Type.GetType("BugsTrackers.BusinessLogicLayer.Status") )
                throw new ArgumentException( "value must be of type Status." );
        }
  }
}
