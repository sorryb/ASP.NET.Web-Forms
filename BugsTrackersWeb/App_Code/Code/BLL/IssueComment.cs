using System;
using System.Collections;
using BugsTrackers.DataAccessLayer;

namespace BugsTrackers.BusinessLogicLayer {


	//*********************************************************************
	//
	// IssueComment Class
	//
	// Represents an issue comment and contains methods for working
	// with them.
	//
	//*********************************************************************

    public class IssueComment {

        /*** PRIVATE FIELDS ***/

        private int         _Id;
        private int         _IssueId;
        private string      _CreatorUsername;
        private string      _CreatorDisplayName;
        private string      _Comment;
        private DateTime    _DateCreated;

        /*** CONSTRUCTOR ***/
        public IssueComment(int issueId, string comment, string creatorUsername)
        : this (DefaultValues.GetIssueCommentIdMinValue(),issueId, comment, creatorUsername, String.Empty, DefaultValues.GetDateTimeMinValue())
        { }

        public IssueComment(int commentId, int issueId, string comment, string creatorUsername, string creatorDisplayName, DateTime created) {
            if (comment == null ||comment.Length==0 )
                throw (new ArgumentOutOfRangeException("comment"));

            if (issueId<=DefaultValues.GetIssueIdMinValue() )
                throw (new ArgumentOutOfRangeException("IssueId"));

            _Id               = commentId;
            _IssueId          = issueId;
            _CreatorUsername  = creatorUsername;
            _CreatorDisplayName   = creatorDisplayName;
            _Comment              = comment;
            _DateCreated          = created;
        }

        /** PROPERTIES **/
        public string Comment {
            get {
                if (_Comment == null ||_Comment.Length==0)
                    return string.Empty;
                else
                    return _Comment;
        }

        set {_Comment=value;}
        }


        public string CreatorUsername {
            get {
                if (_CreatorUsername == null || _CreatorUsername.Length==0)
                    return string.Empty;
                else
                    return _CreatorUsername;
            }
        }


        public string CreatorDisplayName {
            get {
                if (_CreatorDisplayName == null || _CreatorDisplayName.Length==0)
                    return string.Empty;
                else
                    return _CreatorDisplayName;
            }
        }

        public DateTime DateCreated {
            get {return _DateCreated;}
        }


        public int Id {
            get {return _Id;}
        }

        public int IssueId {
            get {return _IssueId; }
            set {
                if (value<=DefaultValues.GetIssueIdMinValue() )
                    throw (new ArgumentOutOfRangeException("value"));
                _IssueId=value;
            }
        }

        /*** INSTANCE METHODS  ***/

        public bool Save () {
            DataAccessLayerBaseClass DBLayer = DataAccessLayerBaseClassHelper.GetDataAccessLayer();
            int TempId = DBLayer.CreateNewIssueComment(this);
            if (TempId>0) {
                _Id = TempId;
                IssueNotification.SendIssueNotifications(IssueId);
                return true;
            } else
                return false;
        }




        /** STATIC METHODS **/

        public static IssueComment CreateNewIssueComment(int issueId,string comment,string userName) {
            IssueComment str = new IssueComment(issueId,comment,userName);
            if (str.Save()==true)
                return str;
            else
                return null;
        }


        public static IssueCommentCollection GetIssueCommentsByIssueId(int issueId) {
            // validate input
            if (issueId <= DefaultValues.GetIssueIdMinValue() )
                throw (new ArgumentOutOfRangeException("issueId"));

            DataAccessLayerBaseClass DBLayer = DataAccessLayerBaseClassHelper.GetDataAccessLayer();
            return (DBLayer.GetIssueCommentsByIssueId(issueId));
        }

  }
}
