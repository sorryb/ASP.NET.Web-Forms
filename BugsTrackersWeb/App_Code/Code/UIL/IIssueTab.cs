using System;

namespace BugsTrackers.UserInterfaceLayer {


	//*********************************************************************
	//
	// IIssueTab Interface
	//
	// This interface is implemented by each tab in the IssueDetail.aspx page.
	//
	//*********************************************************************


    public interface IIssueTab {

        int IssueId {
            get;
            set;
        }

        void Initialize();

    }
}
