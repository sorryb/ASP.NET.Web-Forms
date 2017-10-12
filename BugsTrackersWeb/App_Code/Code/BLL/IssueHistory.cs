using System;
using System.Collections;
using BugsTrackers.DataAccessLayer;

namespace BugsTrackers.BusinessLogicLayer {



	//*********************************************************************
	//
	// IssueHistory Class
	//
	// Represents the history of an issue.
	//
	//*********************************************************************

    public class IssueHistory {

        /*** PRIVATE FIELDS ***/

        private int         _Id;
        private int         _IssueId;
        private string      _CreatorDisplayName;
        private DateTime    _DateCreated;


        /*** CONSTRUCTOR ***/

        public IssueHistory(int historyId, int issueId, string creatorDisplayName, DateTime dateCreated) {
            if (issueId<=DefaultValues.GetIssueIdMinValue() )
                throw (new ArgumentOutOfRangeException("IssueId"));

            _Id               = historyId;
            _IssueId          = issueId;
            _CreatorDisplayName   = creatorDisplayName;
            _DateCreated          = dateCreated;
        }


        /** PROPERTIES **/

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
            set
            {
                if (value<=DefaultValues.GetIssueIdMinValue() )
                throw (new ArgumentOutOfRangeException("value"));
                _IssueId=value;
            }
        }


        /*** STATIC METHODS  ***/

        public static IssueHistoryCollection GetIssueHistoryByIssueId(int issueId) {
            // validate input
            if (issueId <= DefaultValues.GetIssueIdMinValue() )
                throw (new ArgumentOutOfRangeException("issueId"));

            DataAccessLayerBaseClass DBLayer = DataAccessLayerBaseClassHelper.GetDataAccessLayer();
            return (DBLayer.GetIssueHistoryByIssueId(issueId));
        }


  }
}
