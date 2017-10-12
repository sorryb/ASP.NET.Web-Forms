using System;
using System.Collections;
using System.Collections.Specialized;
using BugsTrackers.DataAccessLayer;

namespace BugsTrackers.BusinessLogicLayer {

    public class RelatedIssue {

    /** PRIVATE FIELDS **/

    private int         _IssueId;
    private string      _Title;
    private DateTime    _DateCreated;


    /** CONSTRUCTORS **/

    public RelatedIssue (int issueId, string title, DateTime dateCreated) {
        _IssueId              = issueId;
        _Title                = title;
        _DateCreated          = dateCreated;
    }



    /*** PROPERTIES ***/

    public DateTime DateCreated {
        get{return (_DateCreated);}
    }


    public int IssueId {
        get{return (_IssueId);}
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



    /*** STATIC METHODS ***/

    public static RelatedIssueCollection GetChildIssues (int issueId) {
        if (issueId <= DefaultValues.GetIssueIdMinValue())
            throw (new ArgumentOutOfRangeException("issueId"));

        DataAccessLayerBaseClass DBLayer = DataAccessLayerBaseClassHelper.GetDataAccessLayer();
        return (DBLayer.GetChildIssues(issueId));
    }



    public static RelatedIssueCollection GetParentIssues (int issueId) {
        if (issueId <= DefaultValues.GetIssueIdMinValue())
            throw (new ArgumentOutOfRangeException("issueId"));

        DataAccessLayerBaseClass DBLayer = DataAccessLayerBaseClassHelper.GetDataAccessLayer();
        return (DBLayer.GetParentIssues(issueId));
    }



    public static RelatedIssueCollection GetRelatedIssues (int issueId) {
        if (issueId <= DefaultValues.GetIssueIdMinValue())
            throw (new ArgumentOutOfRangeException("issueId"));

        DataAccessLayerBaseClass DBLayer = DataAccessLayerBaseClassHelper.GetDataAccessLayer();
        return (DBLayer.GetRelatedIssues(issueId));
    }


    public static int CreateNewChildIssue(int primaryIssueId, int secondaryIssueId) {
        if (primaryIssueId == secondaryIssueId)
            return 0;

        if (primaryIssueId <= DefaultValues.GetIssueIdMinValue())
            throw (new ArgumentOutOfRangeException("primaryIssueId"));

        if (secondaryIssueId <= DefaultValues.GetIssueIdMinValue())
            throw (new ArgumentOutOfRangeException("secondaryIssueId"));

        DataAccessLayerBaseClass DBLayer = DataAccessLayerBaseClassHelper.GetDataAccessLayer();
        return DBLayer.CreateNewChildIssue(primaryIssueId, secondaryIssueId);
    }



    public static int CreateNewParentIssue(int primaryIssueId, int secondaryIssueId) {
        if (primaryIssueId == secondaryIssueId)
            return 0;

        if (primaryIssueId <= DefaultValues.GetIssueIdMinValue())
            throw (new ArgumentOutOfRangeException("primaryIssueId"));

        if (secondaryIssueId <= DefaultValues.GetIssueIdMinValue())
            throw (new ArgumentOutOfRangeException("secondaryIssueId"));

        DataAccessLayerBaseClass DBLayer = DataAccessLayerBaseClassHelper.GetDataAccessLayer();
        return DBLayer.CreateNewParentIssue(primaryIssueId, secondaryIssueId);
    }



    public static int CreateNewRelatedIssue(int primaryIssueId, int secondaryIssueId) {
        if (primaryIssueId == secondaryIssueId)
            return 0;

        if (primaryIssueId <= DefaultValues.GetIssueIdMinValue())
            throw (new ArgumentOutOfRangeException("primaryIssueId"));

        if (secondaryIssueId <= DefaultValues.GetIssueIdMinValue())
            throw (new ArgumentOutOfRangeException("secondaryIssueId"));

        DataAccessLayerBaseClass DBLayer = DataAccessLayerBaseClassHelper.GetDataAccessLayer();
        return DBLayer.CreateNewRelatedIssue(primaryIssueId, secondaryIssueId);
    }



    public static bool DeleteChildIssue(int primaryIssueId, int secondaryIssueId) {
        if (primaryIssueId <= DefaultValues.GetIssueIdMinValue())
            throw (new ArgumentOutOfRangeException("primaryIssueId"));

        if (secondaryIssueId <= DefaultValues.GetIssueIdMinValue())
            throw (new ArgumentOutOfRangeException("secondaryIssueId"));

        DataAccessLayerBaseClass DBLayer = DataAccessLayerBaseClassHelper.GetDataAccessLayer();
        return DBLayer.DeleteChildIssue(primaryIssueId, secondaryIssueId);
    }


    public static bool DeleteParentIssue(int primaryIssueId, int secondaryIssueId) {
        if (primaryIssueId <= DefaultValues.GetIssueIdMinValue())
            throw (new ArgumentOutOfRangeException("primaryIssueId"));

        if (secondaryIssueId <= DefaultValues.GetIssueIdMinValue())
            throw (new ArgumentOutOfRangeException("secondaryIssueId"));

        DataAccessLayerBaseClass DBLayer = DataAccessLayerBaseClassHelper.GetDataAccessLayer();
        return DBLayer.DeleteParentIssue(primaryIssueId, secondaryIssueId);
    }


    public static bool DeleteRelatedIssue(int primaryIssueId, int secondaryIssueId) {
        DataAccessLayerBaseClass DBLayer = DataAccessLayerBaseClassHelper.GetDataAccessLayer();
        return DBLayer.DeleteRelatedIssue(primaryIssueId, secondaryIssueId);
    }

  }
}
