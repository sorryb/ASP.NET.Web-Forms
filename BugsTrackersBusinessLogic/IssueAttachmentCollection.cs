using System;
using System.Collections;

namespace BugsTrackers.BusinessLogicLayer 
{


	//*********************************************************************
	//
	// IssueAttachmentCollection Class
	//
	// The IssueCommentCollection class represents a typed collection of
	// IssueAttachment objects.
	//
	//*********************************************************************

	public class IssueAttachmentCollection:CollectionBase 
	{

		public IssueAttachment this[ int index ] 
		{
			get  { return( (IssueAttachment) List[index] );}
			set  { List[index] = value;}
		}

		public int Add( IssueAttachment value ) 
		{
			return( List.Add( value ) );
		}

		public int IndexOf( IssueAttachment value ) 
		{
			return( List.IndexOf( value ) );
		}

		public void Insert( int index, IssueAttachment value ) 
		{
			List.Insert( index, value );
		}

		public void Remove( IssueAttachment value ) 
		{
			List.Remove( value );
		}

		public bool Contains( IssueAttachment value ) 
		{
			// If value is not of type Comment, this will return false.
			return( List.Contains( value ) );
		}

		protected override void OnInsert( int index, Object value ) 
		{
			if ( value.GetType() != Type.GetType("BugsTrackers.BusinessLogicLayer.IssueAttachment") )
				throw new ArgumentException( "value must be of type Attachment.", "value" );
		}

		protected override void OnRemove( int index, Object value ) 
		{
			if ( value.GetType() != Type.GetType("BugsTrackers.BusinessLogicLayer.IssueAttachment") )
				throw new ArgumentException( "value must be of type Attachment.", "value" );
		}

		protected override void OnSet( int index, Object oldValue, Object newValue ) 
		{
			if ( newValue.GetType() != Type.GetType("BugsTrackers.BusinessLogicLayer.IssueAttachment") )
				throw new ArgumentException( "newValue must be of type Attachment.", "newValue" );
		}

		protected override void OnValidate( Object value ) 
		{
			if ( value.GetType() != Type.GetType("BugsTrackers.BusinessLogicLayer.IssueAttachment") )
				throw new ArgumentException( "value must be of type Attachment." );
		}


	}
}
