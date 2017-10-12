using System;
using System.Collections;
using BugsTrackers.DataAccessLayer;

namespace BugsTrackers.BusinessLogicLayer {


	//*********************************************************************
	//
	// Category Class
	//
	// The Category class represents a project category and contains
	// methods for working with them.
	//
	//*********************************************************************


    public class Category {

        /*** PRIVATE FIELDS ***/

        private int     _Id;
        private string  _Name;
        private int     _ParentCategoryId;
        private int     _ProjectId;

		private string	_abbreviation;
		private int		_categoryID;
		private decimal _estDuration;
		public string Abbreviation
		{
			get{ return _abbreviation; }
			set{ _abbreviation = value; }
		}

		public int CategoryID
		{
			get{ return _categoryID; }
			set{ _categoryID = value; }
		}

		public decimal EstDuration
		{
			get{ return _estDuration; }
			set{ _estDuration = value; }
		}

        /*** CONSTRUCTORS ***/
		public Category( )
		{}

        public Category( string name, int categoryId )
        : this( categoryId, DefaultValues.GetProjectIdMinValue(), DefaultValues.GetCategoryIdMinValue(),   name)
        {}


        public Category( int projectId, string name )
        : this( DefaultValues.GetCategoryIdMinValue(), projectId,DefaultValues.GetCategoryIdMinValue(),   name)
        {}

        public Category( int projectId, int parentCategoryId, string name)
        : this( DefaultValues.GetCategoryIdMinValue(), projectId,parentCategoryId,  name)
        {}

        public Category( int categoryId, int projectId, int parentCategoryId, string name) {
            if( parentCategoryId < 0 )
                throw new ArgumentOutOfRangeException("parentCategoryId");

            if( name == null || name.Length == 0 )
                throw new ArgumentException("name");

            _Id            = categoryId;
            _ProjectId        = projectId;
            _Name             = name;
            _ParentCategoryId = parentCategoryId;
        }


        /*** PROPERTIES ***/

        public int Id {
            get { return _Id; }
        }


        public string Name {
            get {
                if (_Name == null)
                    return string.Empty;
                else
                    return _Name;
            }
			set{ _Name = value; }
        }


        public int ProjectId {
            get {return _ProjectId;}
        }

		public int ProjectID
		{
			get{ return _ProjectId; }
			set{ _ProjectId = value; }
		}


        public int ParentCategoryId {
            get { return _ParentCategoryId; }
        }



        /*** INSTANCE METHODS  ***/

        public bool Save() {
            if (Id > DefaultValues.GetProjectIdMinValue())
                throw new ArgumentOutOfRangeException("categoryId");

            DataAccessLayerBaseClass DBLayer = DataAccessLayerBaseClassHelper.GetDataAccessLayer();
            int TempId = DBLayer.CreateNewCategory(this);
                if (TempId>0) {
                    _Id = TempId;
                    return true;
                }
            return false;
        }



        /*** STATIC METHODS ***/

        public static bool DeleteCategory (int categoryId) {
            if (categoryId <= DefaultValues.GetCategoryIdMinValue() )
                throw (new ArgumentOutOfRangeException("categoryId"));

            DataAccessLayerBaseClass DBLayer = DataAccessLayerBaseClassHelper.GetDataAccessLayer();
            return (DBLayer.DeleteCategory(categoryId));
        }


        public static CategoryCollection GetCategoryByProjectId(int projectId) {
            if (projectId <= DefaultValues.GetProjectIdMinValue())
                throw (new ArgumentOutOfRangeException("projectId"));

            DataAccessLayerBaseClass DBLayer = DataAccessLayerBaseClassHelper.GetDataAccessLayer();
            return (DBLayer.GetCategoriesByProjectId(projectId));
        }


  }
}
