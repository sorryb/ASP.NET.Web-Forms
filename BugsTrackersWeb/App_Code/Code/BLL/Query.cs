using System;
using System.Collections;
using System.Web.UI.WebControls;
using BugsTrackers.DataAccessLayer;


namespace BugsTrackers.BusinessLogicLayer {



	//*********************************************************************
	//
	// Query Class
	//
	// Represents a query issued against the issue database.
	//
	//*********************************************************************


    public class Query {

        /*** PRIVATE FIELDS ***/

        private int         _Id;
        private string      _Name;


        /*** CONSTRUCTORS ***/

        public Query(int id, string name) {
            _Id=id;
            _Name=name;
	    }


        /*** PROPERTIES ***/

        public int Id {
            get {return _Id;}
        }



        public string Name {
            get { return _Name; }
        }



        /*** STATIC METHODS ***/

        public static bool SaveQuery(string username, int projectId, string queryName, QueryClauseCollection queryClauses) {
            if (username == null || username.Length == 0)
                throw new ArgumentOutOfRangeException("username");

            if (projectId <= DefaultValues.GetProjectIdMinValue())
                throw new ArgumentOutOfRangeException("projectId");

            if (queryName == null || queryName.Length == 0)
                throw new ArgumentOutOfRangeException("queryName");

            if (queryClauses.Count == 0)
                throw new ArgumentOutOfRangeException("queryClauses");

            DataAccessLayerBaseClass DBLayer = DataAccessLayerBaseClassHelper.GetDataAccessLayer();
            return DBLayer.SaveQuery(username, projectId, queryName, queryClauses);
        }

        public static bool DeleteQuery(int queryId) {
            if (queryId <= DefaultValues.GetQueryIdMinValue())
                throw new ArgumentOutOfRangeException("queryId");

            DataAccessLayerBaseClass DBLayer = DataAccessLayerBaseClassHelper.GetDataAccessLayer();
            return DBLayer.DeleteQuery(queryId);
        }



        public static QueryCollection GetQueriesByUsername(string username, int projectId) {
            if (username == null || username.Length == 0)
                throw new ArgumentOutOfRangeException("username");

            if (projectId <= DefaultValues.GetProjectIdMinValue())
                throw new ArgumentOutOfRangeException("projectId");

            DataAccessLayerBaseClass DBLayer = DataAccessLayerBaseClassHelper.GetDataAccessLayer();
            return DBLayer.GetQueriesByUsername(username, projectId);
        }



    }
}
