using System;
using BugsTrackers.DataAccessLayer;


namespace BugsTrackers.BusinessLogicLayer 
{


	//*********************************************************************
	//
	// IssueAttachment Class
	//
	// The IssueAttachment class represents an individual attachment (file)
	// attached to an issue.
	//
	//*********************************************************************


	public class IssueAttachment 
	{


		private int         _Id;
		private int         _IssueId;
		private string      _CreatorUsername;
		private string      _CreatorDisplayName;
		private DateTime    _DateCreated;
		private string      _FileName;
		private string      _ContentType;
		private Byte[]      _Attachment;


		public IssueAttachment(string fileName, string contentType, byte[] attachment)
			: this (DefaultValues.GetIssueAttachmentIdMinValue(),DefaultValues.GetIssueIdMinValue(), String.Empty, String.Empty, DefaultValues.GetDateTimeMinValue(), fileName, contentType, attachment)
		{ }


		public IssueAttachment(int issueId, string creatorUsername, string fileName, string contentType, byte[] attachment)
			: this (DefaultValues.GetIssueAttachmentIdMinValue(),issueId, creatorUsername, String.Empty, DefaultValues.GetDateTimeMinValue(), fileName, contentType, attachment)
		{ }


		public IssueAttachment(int attachmentId, int issueId, string creatorUsername, string creatorDisplayName, DateTime created, string fileName)
			: this (attachmentId,issueId, creatorUsername, creatorDisplayName, created, fileName, String.Empty, null)
		{ }

		public IssueAttachment(int attachmentId, int issueId, string creatorUsername, string creatorDisplayName, DateTime created, string fileName, string contentType, byte[] attachment) 
		{

			_Id               = attachmentId;
			_IssueId          = issueId;
			_CreatorUsername  = creatorUsername;
			_CreatorDisplayName   = creatorDisplayName;
			_DateCreated          = created;
			_FileName             = fileName;
			_ContentType          = contentType;
			_Attachment           = attachment;
		}

		/** PROPERTIES **/

		public int Id 
		{
			get {return _Id;}
		}


		public int IssueId 
		{
			get {return _IssueId; }
			set 
			{
				if (value<=DefaultValues.GetIssueIdMinValue() )
					throw (new ArgumentOutOfRangeException("value"));
				_IssueId=value;
			}
		}


		public string CreatorUsername 
		{
			get 
			{
				if (_CreatorUsername == null || _CreatorUsername.Length==0)
					return string.Empty;
				else
					return _CreatorUsername;
			}
		}


		public string CreatorDisplayName 
		{
			get 
			{
				if (_CreatorDisplayName == null || _CreatorDisplayName.Length==0)
					return string.Empty;
				else
					return _CreatorDisplayName;
			}
		}

		public DateTime DateCreated 
		{
			get {return _DateCreated;}
		}


		public string FileName 
		{
			get 
			{
				if (_FileName == null || _FileName.Length==0)
					return string.Empty;
				else
					return _FileName;
			}
		}



		public string ContentType 
		{
			get 
			{
				if (_ContentType == null || _ContentType.Length==0)
					return string.Empty;
				else
					return _ContentType;
			}
		}


		public Byte[] Attachment 
		{
			get {return _Attachment;}
		}


		/*** INSTANCE METHODS  ***/

		public bool Save () 
		{
			DataAccessLayerBaseClass DBLayer = DataAccessLayerBaseClassHelper.GetDataAccessLayer();
			int TempId = DBLayer.CreateNewIssueAttachment(this);
			if (TempId>0) 
			{
				_Id = TempId;
				IssueNotification.SendIssueNotifications(IssueId);
				return true;
			} 
			else
				return false;
		}


		/** STATIC METHODS **/

		public static IssueAttachmentCollection GetIssueAttachmentsByIssueId(int issueId) 
		{
			// validate input
			if (issueId <= DefaultValues.GetIssueIdMinValue() )
				throw (new ArgumentOutOfRangeException("issueId"));

			DataAccessLayerBaseClass DBLayer = DataAccessLayerBaseClassHelper.GetDataAccessLayer();
			return (DBLayer.GetIssueAttachmentsByIssueId(issueId));
		}

		public static IssueAttachment GetIssueAttachmentById(int attachmentId) 
		{
			// validate input
			if (attachmentId <= DefaultValues.GetIssueAttachmentIdMinValue() )
				throw (new ArgumentOutOfRangeException("attachmentId"));

			DataAccessLayerBaseClass DBLayer = DataAccessLayerBaseClassHelper.GetDataAccessLayer();
			return (DBLayer.GetIssueAttachmentById(attachmentId));
		}


		public static void DeleteIssueAttachment(int attachmentId) 
		{
			// validate input
			if (attachmentId <= DefaultValues.GetIssueAttachmentIdMinValue() )
				throw (new ArgumentOutOfRangeException("attachmentId"));

			DataAccessLayerBaseClass DBLayer = DataAccessLayerBaseClassHelper.GetDataAccessLayer();
			DBLayer.DeleteIssueAttachment(attachmentId);
		}

	}
}
