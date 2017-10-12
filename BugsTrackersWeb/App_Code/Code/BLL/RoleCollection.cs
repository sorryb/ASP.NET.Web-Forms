using System;
using System.Collections;

namespace BugsTrackers.BusinessLogicLayer {


	//*********************************************************************
	//
	// RoleCollection Class
	//
	// The RoleCollection class represents a typed collection of
	// Role objects.
	//
	//*********************************************************************

    public class RoleCollection:CollectionBase {


        public Role this[ int index ] {
            get  { return( (Role) List[index] );}
            set  { List[index] = value;}
        }

        public int Add( Role value ) {
            return( List.Add( value ) );
        }

        public int IndexOf( Role value ) {
            return( List.IndexOf( value ) );
        }

        public void Insert( int index, Role value ) {
            List.Insert( index, value );
        }

        public void Remove( Role value ) {
            List.Remove( value );
        }

        public bool Contains( Role value ) {
            // If value is not of type Role, this will return false.
            return( List.Contains( value ) );
        }

        protected override void OnInsert( int index, Object value ) {
            if ( value.GetType() != Type.GetType("BugsTrackers.BusinessLogicLayer.Role") )
                throw new ArgumentException( "value must be of type Role.", "value" );
        }

        protected override void OnRemove( int index, Object value ) {
            if ( value.GetType() != Type.GetType("BugsTrackers.BusinessLogicLayer.Role") )
                throw new ArgumentException( "value must be of type Role.", "value" );
        }

        protected override void OnSet( int index, Object oldValue, Object newValue ) {
            if ( newValue.GetType() != Type.GetType("BugsTrackers.BusinessLogicLayer.Role") )
                throw new ArgumentException( "newValue must be of type Role.", "newValue" );
        }

        protected override void OnValidate( Object value ) {
            if ( value.GetType() != Type.GetType("BugsTrackers.BusinessLogicLayer.Role") )
                throw new ArgumentException( "value must be of type Role." );
        }
  }
}
