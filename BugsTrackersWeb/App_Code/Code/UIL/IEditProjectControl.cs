using System;

namespace BugsTrackers.UserInterfaceLayer {


	//*********************************************************************
	//
	// IEditProjectControl Interface
	//
	// This interface is implemented by each step in the Edit Project Wizard.
	//
	//*********************************************************************

    public interface IEditProjectControl {
        int ProjectId {
            get;
            set;
        }


        bool Update();

        void Initialize();

    }
}
