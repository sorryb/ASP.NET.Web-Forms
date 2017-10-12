using System;
using System.Collections;
using System.Collections.Specialized;
using BugsTrackers.DataAccessLayer;

namespace BugsTrackers.BusinessLogicLayer {


	//*********************************************************************
	//
	// Issue Class
	//
	// The Issue class represents an issue and contains methods for
	// manipulating them.
	//
	//*********************************************************************


    public class Issue {

        /** PRIVATE FIELDS **/

        private string      _AssignedDisplayName;
        private int         _AssignedId;
        private string      _CreatorDisplayName;
        private int         _CreatorId;
        private string      _OwnerDisplayName;
        private int         _OwnerId;
        private int         _CategoryId;
        private string      _CategoryName;
        private DateTime    _DateCreated;
        private int         _Id;
        private int         _MilestoneId;
        private string      _MilestoneName;
        private string      _MilestoneImageUrl;
        private int         _PriorityId;
        private string      _PriorityName;
        private string      _PriorityImageUrl;
        private int         _StatusId;
        private string      _StatusName;
        private string      _StatusImageUrl;

        private int         _ProjectId;
        private string      _Title;
        private string      _CreatorUsername;



        /** CONSTRUCTORS **/

        public Issue (int id, int projectId, string title, int categoryId, int milestoneId, int priorityId, int statusId, int assignedId, int ownerId, string creatorUsername)
        : this
        (
            id,
            projectId,
            title,
            categoryId,
            String.Empty,
            milestoneId,
            String.Empty,
            String.Empty,
            priorityId,
            String.Empty,
            String.Empty,
            statusId,
            String.Empty,
            String.Empty,
            String.Empty,
            assignedId,
            String.Empty,
            ownerId,
            creatorUsername,
            DefaultValues.GetUserIdMinValue(),
            creatorUsername,
            DefaultValues.GetDateTimeMinValue()
        )
        {}


        public Issue
        (
            int id,
            int projectId,
            string title,
            int categoryId,
            string categoryName,
            int milestoneId,
            string milestoneName,
            string milestoneImageUrl,
            int priorityId,
            string priorityName,
            string priorityImageUrl,
            int statusId,
            string statusName,
            string statusImageUrl,
            string assignedDisplayName,
            int assignedId,
            string ownerDisplayName,
            int ownerId,
            string creatorDisplayName,
            int creatorId,
            string creatorUsername,
            DateTime dateCreated
        )
        {
        _Id                   = id;
        _ProjectId            = projectId;
        _Title                = title;
        _CategoryId           = categoryId;
        _CategoryName         = categoryName;
        _MilestoneId          = milestoneId;
        _MilestoneName        = milestoneName;
        _MilestoneImageUrl    = milestoneImageUrl;
        _PriorityId           = priorityId;
        _PriorityName         = priorityName;
        _PriorityImageUrl     = priorityImageUrl;
        _StatusId             = statusId;
        _StatusName           = statusName;
        _StatusImageUrl       = statusImageUrl;
        _DateCreated          = dateCreated;
        _AssignedDisplayName  = assignedDisplayName;
        _AssignedId           = assignedId;
        _OwnerDisplayName     = ownerDisplayName;
        _OwnerId              = ownerId;
        _CreatorDisplayName   = creatorDisplayName;
        _CreatorUsername      = creatorUsername;
        _CreatorId            = creatorId;
        }


        /*** PROPERTIES ***/

        public string AssignedDisplayName {
            get {
                if (_AssignedDisplayName == null ||_AssignedDisplayName.Length==0)
                    return string.Empty;
                else
                    return _AssignedDisplayName;
            }
            set{_AssignedDisplayName=value;}
        }

        public int CategoryId {
            get {return _CategoryId; }
            set {_CategoryId=value; }
        }


        public string CategoryName {
            get {return _CategoryName; }
            set {_CategoryName = value; }
        }


        public string CreatorDisplayName {
            get{return (_CreatorDisplayName);}
        }


        public string CreatorUsername {
            get{return (_CreatorUsername);}
        }


        public string OwnerDisplayName {
            get{return (_OwnerDisplayName);}
        }


        public int CreatorId {
            get{return (_CreatorId);}
        }



        public int AssignedId {
            get{return (_AssignedId);}
        }


        public int OwnerId {
            get{return (_OwnerId);}
        }



        public DateTime DateCreated {
            get{return (_DateCreated);}
        }


        public int Id {
            get{return (_Id);}
        }


        public int MilestoneId {
            get {return _MilestoneId; }
            set {_MilestoneId=value; }
        }

        public string MilestoneName {
            get {return _MilestoneName; }
        }


        public string MilestoneImageUrl {
            get {return _MilestoneImageUrl; }
        }


        public int PriorityId {
            get {return _PriorityId; }
            set {_PriorityId=value; }
        }


        public string PriorityName {
            get {return _PriorityName; }
        }


        public string PriorityImageUrl {
            get {return _PriorityImageUrl; }
        }


        public int ProjectId {
            get {return _ProjectId; }
        }


        public int StatusId {
            get {return _StatusId; }
            set {_StatusId=value; }
        }


        public string StatusName {
            get {return _StatusName; }
        }


        public string StatusImageUrl {
            get {return _StatusImageUrl; }
        }


        public string Title {
            get {
                if (_Title == null ||_Title.Length==0)
                    return string.Empty;
                else
                    return _Title;
            }
            set{_Title=value;}
        }



        /*** INSTANCE METHODS ***/




        public bool Save() {
            DataAccessLayerBaseClass DBLayer = DataAccessLayerBaseClassHelper.GetDataAccessLayer();
            if (Id <= DefaultValues.GetIssueIdMinValue()) {
                int TempId = DBLayer.CreateNewIssue(this);
                if (TempId>0) {
                    _Id = TempId;
                    return true;
                } else
                    return false;
            } else {
                bool result = DBLayer.UpdateIssue(this);
                IssueNotification.SendIssueNotifications(Id);
                return result;
            }
        }


        /*** STATIC METHODS ***/


        public static bool DeleteIssue (int issueId) {
            // validate input
            if (issueId <= DefaultValues.GetIssueIdMinValue())
                throw (new ArgumentOutOfRangeException("issueId"));

            DataAccessLayerBaseClass DBLayer = DataAccessLayerBaseClassHelper.GetDataAccessLayer();
            return (DBLayer.DeleteIssue(issueId));
        }


        public static IssueCollection GetIssuesByAssignedUsername (int projectId, string username) {
            if (projectId <= DefaultValues.GetProjectIdMinValue())
                throw (new ArgumentOutOfRangeException("projectId"));

            if (username == null ||username.Length==0)
                throw (new ArgumentOutOfRangeException("username"));

                DataAccessLayerBaseClass DBLayer = DataAccessLayerBaseClassHelper.GetDataAccessLayer();
                return (DBLayer.GetIssuesByAssignedUsername(projectId, username));
        }



        public static IssueCollection GetIssuesByCreatorUsername (int projectId, string username) {
            if (projectId <= DefaultValues.GetProjectIdMinValue())
                throw (new ArgumentOutOfRangeException("projectId"));

            if (username == null ||username.Length==0)
                throw (new ArgumentOutOfRangeException("username"));

                DataAccessLayerBaseClass DBLayer = DataAccessLayerBaseClassHelper.GetDataAccessLayer();
                return (DBLayer.GetIssuesByCreatorUsername(projectId, username));
        }



        public static IssueCollection GetIssuesByOwnerUsername (int projectId, string username) {
            if (projectId <= DefaultValues.GetProjectIdMinValue())
                throw (new ArgumentOutOfRangeException("projectId"));

            if (username == null ||username.Length==0)
                throw (new ArgumentOutOfRangeException("username"));

                DataAccessLayerBaseClass DBLayer = DataAccessLayerBaseClassHelper.GetDataAccessLayer();
                return (DBLayer.GetIssuesByOwnerUsername(projectId, username));
        }




        public static IssueCollection GetIssuesByRelevancy(int projectId, string username) {
            if (projectId <= DefaultValues.GetProjectIdMinValue())
                throw (new ArgumentOutOfRangeException("projectId"));


            if (username == null ||username.Length==0)
                throw (new ArgumentOutOfRangeException("username"));

                DataAccessLayerBaseClass DBLayer = DataAccessLayerBaseClassHelper.GetDataAccessLayer();
                return (DBLayer.GetIssuesByRelevancy(projectId, username));
        }



        public static Issue GetIssueById(int issueId) {
            if (issueId <= DefaultValues.GetIssueIdMinValue())
                throw (new ArgumentOutOfRangeException("issueId"));

            DataAccessLayerBaseClass DBLayer = DataAccessLayerBaseClassHelper.GetDataAccessLayer();
            return (DBLayer.GetIssueById(issueId));
        }


        public static IssueCollection GetIssuesByProjectId (int projectId) {
            if (projectId <= DefaultValues.GetProjectIdMinValue())
                throw (new ArgumentOutOfRangeException("projectId"));

            DataAccessLayerBaseClass DBLayer = DataAccessLayerBaseClassHelper.GetDataAccessLayer();
            return (DBLayer.GetIssuesByProjectId(projectId));
        }




        public static IssueCollection PerformQuery(int projectId, QueryClauseCollection queryClauses) {
            if (queryClauses.Count == 0)
                throw new ArgumentOutOfRangeException("queryClauses");

            DataAccessLayerBaseClass DBLayer = DataAccessLayerBaseClassHelper.GetDataAccessLayer();
            return DBLayer.PerformQuery(projectId, queryClauses);
        }




        public static IssueCollection PerformSavedQuery(int projectId, int queryId) {
            if (queryId <= DefaultValues.GetQueryIdMinValue())
                throw (new ArgumentOutOfRangeException("queryId"));

                DataAccessLayerBaseClass DBLayer = DataAccessLayerBaseClassHelper.GetDataAccessLayer();
                return DBLayer.PerformSavedQuery(projectId, queryId);
        }


  }
}
