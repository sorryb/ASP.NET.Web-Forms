using System;
using System.Collections;
using BugsTrackers.DataAccessLayer;

namespace BugsTrackers.BusinessLogicLayer {



	//*********************************************************************
	//
	// Status Class
	//
	// Represents an issue status.
	//
	//*********************************************************************


    public class Status {

        /*** PRIVATE FIELDS ***/

        private int      _Id;
        private int      _ProjectId;
        private string   _Name;
        private string   _ImageUrl;


        /*** CONSTRUCTORS ***/

        public Status(int projectId, string name, string imageUrl)
        : this(DefaultValues.GetStatusIdMinValue(), projectId, name, imageUrl)
        {}

        public Status(int id, int projectId, string name, string imageUrl) {
            if (projectId <= DefaultValues.GetProjectIdMinValue() )
                throw (new ArgumentOutOfRangeException("projectId"));

            if (name == null ||name.Length==0 )
                throw (new ArgumentOutOfRangeException("statusName"));

            _Id             = id;
            _ProjectId      = projectId;
            _Name           = name;
            _ImageUrl       = imageUrl;
        }


        /*** PROPERTIES ***/

        public int Id {
            get {return _Id;}
        }

        public int ProjectId {
            get {return _ProjectId;}
            set {
                if( value <= DefaultValues.GetProjectIdMinValue() )
                    throw new ArgumentOutOfRangeException("value");
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
            return DBLayer.DeleteStatus(this.Id);
        }

        public bool Save () {
            DataAccessLayerBaseClass DBLayer = DataAccessLayerBaseClassHelper.GetDataAccessLayer();
            if (Id <= DefaultValues.GetProjectIdMinValue())
            {
                int TempId = DBLayer.CreateNewStatus(this);
                if (TempId>0)
                {
                    _Id = TempId;
                    return true;
                }
                else
                    return false;
            }
            else
                return false;
        }


        /*** STATIC METHODS ***/

        public static Status CreateNewStatus(int projectId, string statusName) {
            return (Status.CreateNewStatus(projectId, statusName, string.Empty) );
        }

        public static Status CreateNewStatus(int projectId, string statusName, string imageUrl) {
            Status newStatus = new Status(projectId, statusName, imageUrl);
            if (newStatus.Save()==true)
                return newStatus;
            else
                return null;
        }

        public static bool DeleteStatus (int statusId) {
            if (statusId <= DefaultValues.GetStatusIdMinValue() )
                throw (new ArgumentOutOfRangeException("statusId"));

            DataAccessLayerBaseClass DBLayer = DataAccessLayerBaseClassHelper.GetDataAccessLayer();
            return (DBLayer.DeleteStatus(statusId));
        }


        public static StatusCollection GetStatusByProjectId(int  projectId) {
            if (projectId <= DefaultValues.GetProjectIdMinValue())
                throw (new ArgumentOutOfRangeException("statusName"));

            DataAccessLayerBaseClass DBLayer = DataAccessLayerBaseClassHelper.GetDataAccessLayer();
            return (DBLayer.GetStatusByProjectId(projectId));
        }

  }
}
