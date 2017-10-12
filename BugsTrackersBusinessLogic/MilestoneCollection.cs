using System;
using System.Collections;

namespace BugsTrackers.BusinessLogicLayer {


	//*********************************************************************
	//
	// MilestoneCollection Class
	//
	// The MilestoneCollection class represents a typed collection of
	// Milestone objects.
	//
	//*********************************************************************

    public class MilestoneCollection:CollectionBase {

        public Milestone this[ int index ]  {
            get  { return( (Milestone) List[index] );}
            set  { List[index] = value;}
        }

        public int Add( Milestone value )  {
            return( List.Add( value ) );
        }

        public int IndexOf( Milestone value )  {
            return( List.IndexOf( value ) );
        }

        public void Insert( int index, Milestone value )  {
            List.Insert( index, value );
        }

        public void Remove( Milestone value )  {
            List.Remove( value );
        }

        public bool Contains( Milestone value ) {
            // If value is not of type Milestone, this will return false.
            return( List.Contains( value ) );
        }

        protected override void OnInsert( int index, Object value )  {
            if ( value.GetType() != Type.GetType("BugsTrackers.BusinessLogicLayer.Milestone") )
                throw new ArgumentException( "value must be of type Milestone.", "value" );
        }

        protected override void OnRemove( int index, Object value )  {
            if ( value.GetType() != Type.GetType("BugsTrackers.BusinessLogicLayer.Milestone") )
                throw new ArgumentException( "value must be of type Milestone.", "value" );
        }

        protected override void OnSet( int index, Object oldValue, Object newValue )  {
            if ( newValue.GetType() != Type.GetType("BugsTrackers.BusinessLogicLayer.Milestone") )
                throw new ArgumentException( "newValue must be of type Milestone.", "newValue" );
        }

        protected override void OnValidate( Object value )  {
            if ( value.GetType() != Type.GetType("BugsTrackers.BusinessLogicLayer.Milestone") )
                throw new ArgumentException( "value must be of type Milestone." );
        }
  }
}
