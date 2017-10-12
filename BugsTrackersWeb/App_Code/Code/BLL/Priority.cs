using System;
using System.Collections;
using BugsTrackers.DataAccessLayer;

namespace BugsTrackers.BusinessLogicLayer {



	//*********************************************************************
	//
	// Priority Class
	//
	// Represents an issue priority.
	//
	//*********************************************************************


    public class Priority {

        /*** FIELD - PRIVATE ***/

        private int         _Id;
        private int         _ProjectId;
        private string      _Name;
        private string      _ImageUrl;


        /*** CONSTRUCTORS ***/

        public Priority(int projectId, string name)
        : this(DefaultValues.GetPriorityIdMinValue(), projectId, name, string.Empty)
        {}

        public Priority(int projectId, string name, string imageUrl)
        : this(DefaultValues.GetPriorityIdMinValue(), projectId, name, imageUrl )
        {}

        public Priority(int id, int projectId, string name, string imageUrl) {
            if (projectId <= DefaultValues.GetProjectIdMinValue() )
                throw (new ArgumentOutOfRangeException("projectId"));

            if (name == null ||name.Length==0 )
                throw (new ArgumentOutOfRangeException("PriorityName"));

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
            set
            {
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
            set {_Name=value;}
        }


        public string ImageUrl {
            get { return _ImageUrl; }
        }


        /*** INSTANCE METHODS  ***/

        public bool Delete () {
            DataAccessLayerBaseClass DBLayer = DataAccessLayerBaseClassHelper.GetDataAccessLayer();
            return DBLayer.DeletePriority(this.Id);
        }

        public bool Save () {
            if (Id > DefaultValues.GetProjectIdMinValue())
                throw new ArgumentOutOfRangeException("priorityId");

            DataAccessLayerBaseClass DBLayer = DataAccessLayerBaseClassHelper.GetDataAccessLayer();
            int TempId = DBLayer.CreateNewPriority(this);
            if (TempId>0) {
                _Id = TempId;
                return true;
            }
            return false;
        }

        /*** STATIC METHODS ***/

        public static Priority CreateNewPriority(int projectId, string  priorityName) {
            return (Priority.CreateNewPriority(projectId, priorityName, string.Empty) );
        }


        public static Priority CreateNewPriority(int projectId, string priorityName, string imageUrl) {
            Priority newPriority = new Priority(projectId, priorityName, imageUrl);
            if (newPriority.Save()==true)
                return newPriority;
            else
                return null;
        }


        public static bool DeletePriority (int priorityId) {
            if (priorityId <= DefaultValues.GetPriorityIdMinValue() )
                throw (new ArgumentOutOfRangeException("priorityId"));

            DataAccessLayerBaseClass DBLayer = DataAccessLayerBaseClassHelper.GetDataAccessLayer();
            return (DBLayer.DeletePriority(priorityId));
        }


        public static PriorityCollection GetPrioritiesByProjectId(int  projectId) {
            if (projectId <= DefaultValues.GetProjectIdMinValue())
                throw (new ArgumentOutOfRangeException("priorityId"));

            DataAccessLayerBaseClass DBLayer = DataAccessLayerBaseClassHelper.GetDataAccessLayer();
            return (DBLayer.GetPrioritiesByProjectId(projectId));
        }


  }
}
