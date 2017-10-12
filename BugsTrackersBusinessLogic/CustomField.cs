using System;
using System.Collections;
using System.Web.UI.WebControls;
using BugsTrackers.DataAccessLayer;


namespace BugsTrackers.BusinessLogicLayer {

    public class CustomField {

        /*** PRIVATE FIELDS ***/

        private int         _Id;
        private int         _ProjectId;
        private string      _Name;
        ValidationDataType  _DataType;
        bool                _Required;
        private string      _Value;


        /*** CONSTRUCTORS ***/

        public CustomField(int projectId, string name, ValidationDataType dataType, bool required)
                :this(DefaultValues.GetCustomFieldIdMinValue(), projectId, name, dataType, required, String.Empty)
	    {}


        public CustomField(int id, string value)
                :this(id, DefaultValues.GetProjectIdMinValue(), String.Empty, ValidationDataType.String, false, value)
	    {}

        public CustomField(int id, int projectId, string name, ValidationDataType dataType, bool required, string value) {
            _Id = id;
            _ProjectId = projectId;
            _Name = name;
            _DataType = dataType;
            _Required = required;
            _Value = value;
	    }

        /*** PROPERTIES ***/

        public int Id {
            get {return _Id;}
        }


        public int ProjectId {
            get {return _ProjectId;}
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


        public ValidationDataType DataType {
            get { return _DataType; }
        }


        public bool Required {
            get { return _Required; }
        }



        public string Value {
            get {
                if (_Value == null ||_Value.Length==0)
                    return string.Empty;
                else
                    return _Value;
            }
            set {_Value=value;}
        }




        /*** INSTANCE METHODS ***/


        public bool Save () {
            DataAccessLayerBaseClass DBLayer = DataAccessLayerBaseClassHelper.GetDataAccessLayer();
            if (Id <= DefaultValues.GetProjectIdMinValue()) {
                int TempId = DBLayer.CreateNewCustomField(this);
                if (TempId>0) {
                    _Id = TempId;
                    return true;
                }
                else
                    return false;
            }
            else
                return (DBLayer.UpdateCustomField(this));
        }



        /*** STATIC METHODS ***/


        public static bool DeleteCustomField (int customFieldId) {
            if (customFieldId <= DefaultValues.GetPriorityIdMinValue() )
                throw (new ArgumentOutOfRangeException("customFieldId"));

            DataAccessLayerBaseClass DBLayer = DataAccessLayerBaseClassHelper.GetDataAccessLayer();
            return (DBLayer.DeleteCustomField(customFieldId));
        }


        public static bool SaveCustomFieldValues(int issueId, CustomFieldCollection fields) {
            if (fields == null )
                throw (new ArgumentOutOfRangeException("fields"));

            DataAccessLayerBaseClass DBLayer = DataAccessLayerBaseClassHelper.GetDataAccessLayer();
            return (DBLayer.SaveCustomFieldValues(issueId, fields));
        }



        public static CustomFieldCollection GetCustomFieldsByProjectId(int  projectId) {
            if (projectId <= DefaultValues.GetProjectIdMinValue())
                throw (new ArgumentOutOfRangeException("priorityId"));

            DataAccessLayerBaseClass DBLayer = DataAccessLayerBaseClassHelper.GetDataAccessLayer();
            return (DBLayer.GetCustomFieldsByProjectId(projectId));
        }



        public static CustomFieldCollection GetCustomFieldsByIssueId(int issueId) {
            if (issueId <= DefaultValues.GetProjectIdMinValue())
                throw (new ArgumentOutOfRangeException("issueId"));

            DataAccessLayerBaseClass DBLayer = DataAccessLayerBaseClassHelper.GetDataAccessLayer();
            return (DBLayer.GetCustomFieldsByIssueId(issueId));
        }



    }
}
