using System;
using System.Collections;
using BugsTrackers.DataAccessLayer;

namespace BugsTrackers.BusinessLogicLayer {


	//*********************************************************************
	//
	// PriorityCollection Class
	//
	// The PriorityCollection class represents a typed collection of
	// Priority objects.
	//
	//*********************************************************************


    public class PriorityCollection:CollectionBase {


    public Priority this[ int index ]  {
        get  { return( (Priority) List[index] );}
        set  { List[index] = value;}
    }

    public int Add( Priority value )  {
        return( List.Add( value ) );
    }

    public int IndexOf( Status value )  {
        return( List.IndexOf( value ) );
    }

    public void Insert( int index, Priority value ) {
        List.Insert( index, value );
    }

    public void Remove( Priority value ) {
        List.Remove( value );
    }

    public bool Contains( Priority value )  {
        // If value is not of type Priority, this will return false.
        return( List.Contains( value ) );
    }

    protected override void OnInsert( int index, Object value )  {
        if ( value.GetType() != Type.GetType("BugsTrackers.BusinessLogicLayer.Priority") )
            throw new ArgumentException( "value must be of type Priority.", "value" );
    }

    protected override void OnRemove( int index, Object value )   {
        if ( value.GetType() != Type.GetType("BugsTrackers.BusinessLogicLayer.Priority") )
            throw new ArgumentException( "value must be of type Priority.", "value" );
    }

    protected override void OnSet( int index, Object oldValue, Object newValue )  {
        if ( newValue.GetType() != Type.GetType("BugsTrackers.BusinessLogicLayer.Priority") )
            throw new ArgumentException( "newValue must be of type Priority.", "newValue" );
    }

    protected override void OnValidate( Object value )  {
        if ( value.GetType() != Type.GetType("BugsTrackers.BusinessLogicLayer.Priority") )
            throw new ArgumentException( "value must be of type Priority." );
    }

  }
}
