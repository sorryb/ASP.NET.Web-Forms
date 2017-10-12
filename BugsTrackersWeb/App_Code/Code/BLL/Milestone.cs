using System;
using System.Collections;
using BugsTrackers.DataAccessLayer;

namespace BugsTrackers.BusinessLogicLayer {


	//*********************************************************************
	//
	// Milestone Class
	//
	// The Milestone class represents an issue milestone and methods for
	// manipulating them.
	//
	//*********************************************************************

    public class Milestone {

        /*** PRIVATE FIELDS ***/

        private int      _Id;
        private int      _ProjectId;
        private string   _Name;
        private string   _ImageUrl;


        /*** CONSTRUCTOR ***/

        public Milestone(int projectId, string name, string imageUrl)
        :this(DefaultValues.GetMilestoneIdMinValue(), projectId, name, imageUrl)
        {}

        public Milestone(int id, int projectId, string name, string imageUrl) {
            if (projectId<=DefaultValues.GetProjectIdMinValue() )
                throw (new ArgumentOutOfRangeException("projectId"));

            if (name == null ||name.Length==0 )
                throw (new ArgumentOutOfRangeException("name"));

            _Id           = id;
            _ProjectId    = projectId;
            _Name         = name;
            _ImageUrl     = imageUrl;
        }


        /*** PROPERTIES ***/

        public int Id {
            get {return _Id;}
        }


        public int ProjectId {
            get {return _ProjectId;}
            set {
                if (value<=DefaultValues.GetProjectIdMinValue() )
                    throw (new ArgumentOutOfRangeException("value"));
                _ProjectId=value;
            }
        }


        public string Name {
            get {
                if (_Name == null ||_Name.Length==0)
                    return string.Empty;
                else
                    return _Name;
            }
            set {_Name=value;}
        }


        public string ImageUrl {
            get {
                if (_ImageUrl == null || _ImageUrl.Length==0)
                    return string.Empty;
                else
                    return _ImageUrl;
            }
        }


        /*** INSTANCE METHODS  ***/

        public bool Delete () {
            DataAccessLayerBaseClass DBLayer = DataAccessLayerBaseClassHelper.GetDataAccessLayer();
            return DBLayer.DeleteMilestone(this.Id);
        }


        public bool Save () {
            DataAccessLayerBaseClass DBLayer = DataAccessLayerBaseClassHelper.GetDataAccessLayer();
            if (Id <= DefaultValues.GetProjectIdMinValue()) {
                int TempId = DBLayer.CreateNewMilestone(this);
                if (TempId>0) {
                    _Id = TempId;
                    return true;
                } else
                    return false;
            }
            else
                return false;
        }


        /*** STATIC METHODS ***/

        public static bool DeleteMilestone(int milestoneId) {
            if (milestoneId <= DefaultValues.GetMilestoneIdMinValue() )
                throw (new ArgumentOutOfRangeException("milestoneId"));

            DataAccessLayerBaseClass DBLayer = DataAccessLayerBaseClassHelper.GetDataAccessLayer();
            return (DBLayer.DeleteMilestone(milestoneId));
        }


        public static  MilestoneCollection GetMilestoneByProjectId (int milestoneId) {
            if (milestoneId <= DefaultValues.GetMilestoneIdMinValue() )
                throw (new ArgumentOutOfRangeException("milestoneId"));

            DataAccessLayerBaseClass DBLayer = DataAccessLayerBaseClassHelper.GetDataAccessLayer();
            return (DBLayer.GetMilestoneByProjectId(milestoneId));
        }

  }
}
