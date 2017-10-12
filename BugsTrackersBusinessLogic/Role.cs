using System;
using BugsTrackers.DataAccessLayer;

namespace BugsTrackers.BusinessLogicLayer {

    public class Role {

        /** PRIVATE FIELDS **/

        private int _Id;
        private string _Name = String.Empty;

        /** CONSTRUCTORS **/

        public Role(int id, string name) {
            _Id = id;
            _Name = name;
        }


        /** PROPERTIES **/

        public int Id {
            get { return _Id; }
        }


        public string Name {
            get { return _Name; }
        }



        /** STATIC METHODS **/

        public static RoleCollection GetAllRoles() {
            DataAccessLayerBaseClass DBLayer = DataAccessLayerBaseClassHelper.GetDataAccessLayer();
            return (DBLayer.GetAllRoles());
        }


    }
}
