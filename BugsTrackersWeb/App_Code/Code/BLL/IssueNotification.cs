using System;
using System.Collections;
using BugsTrackers.DataAccessLayer;

namespace BugsTrackers.BusinessLogicLayer {



	//*********************************************************************
	//
	// IssueNotification Class
	//
	// Represents information about email notifications sent when an
	// issue is modified.
	//
	//*********************************************************************

    public class IssueNotification {

        /*** PRIVATE FIELDS ***/

        private int         _Id;
        private int         _IssueId;
        private string      _NotificationUsername;
        private string      _NotificationDisplayName;
        private string      _NotificationEmail;




        /*** CONSTRUCTOR ***/


        public IssueNotification(int issueId, string notificationUsername)
        : this (DefaultValues.GetIssueNotificationIdMinValue(), issueId, notificationUsername, String.Empty, String.Empty)
        { }



        public IssueNotification(int notificationId, int issueId, string notificationUsername, string notificationDisplayName, string notificationEmail) {
            if (issueId<=DefaultValues.GetIssueIdMinValue() )
                throw (new ArgumentOutOfRangeException("IssueId"));

            _Id                       = notificationId;
            _IssueId                  = issueId;
            _NotificationUsername     = notificationUsername;
            _NotificationDisplayName  = notificationDisplayName;
            _NotificationEmail        = notificationEmail;
        }



        /** PROPERTIES **/

        public int Id {
            get {return _Id;}
        }

        public int IssueId {
            get {return _IssueId; }
            set
            {
                if (value<=DefaultValues.GetIssueIdMinValue() )
                    throw (new ArgumentOutOfRangeException("value"));
                _IssueId=value;
            }
        }



        public string NotificationUsername {
            get {
                if (_NotificationUsername == null || _NotificationUsername.Length==0)
                    return string.Empty;
                else
                    return _NotificationUsername;
            }
        }


        public string NotificationDisplayName {
            get {
                if (_NotificationDisplayName == null || _NotificationDisplayName.Length==0)
                    return string.Empty;
                else
                    return _NotificationDisplayName;
            }
        }


        public string NotificationEmail {
            get {
                if (_NotificationEmail == null || _NotificationEmail.Length==0)
                    return string.Empty;
                else
                    return _NotificationEmail;
            }
        }


        /*** INSTANCE METHODS  ***/

        public bool Save () {
            DataAccessLayerBaseClass DBLayer = DataAccessLayerBaseClassHelper.GetDataAccessLayer();
            int TempId = DBLayer.CreateNewIssueNotification(this);
            if (TempId>0) {
                _Id = TempId;
                return true;
            } else
                return false;
        }


        /*** STATIC METHODS  ***/

        public static bool DeleteIssueNotification(int issueId, string username) {
            if (issueId <= DefaultValues.GetIssueIdMinValue() )
                throw (new ArgumentOutOfRangeException("issueId"));

            DataAccessLayerBaseClass DBLayer = DataAccessLayerBaseClassHelper.GetDataAccessLayer();
            return (DBLayer.DeleteIssueNotification(issueId, username));
        }


        public static void SendIssueNotifications(int issueId) {
            // validate input
            if (issueId <= DefaultValues.GetIssueIdMinValue() )
                throw (new ArgumentOutOfRangeException("issueId"));

            DataAccessLayerBaseClass DBLayer = DataAccessLayerBaseClassHelper.GetDataAccessLayer();
            IssueNotificationCollection issNotifications = DBLayer.GetIssueNotificationsByIssueId(issueId);

            System.Web.Mail.SmtpMail.SmtpServer = Globals.SmtpServer;

            string notifyEmail = Globals.NotifyEmail;
            string desktopDefaultUrl = Globals.DesktopDefaultUrl;

            foreach (IssueNotification notify in issNotifications) {
                    try {
                        System.Web.Mail.SmtpMail.Send(notifyEmail, notify.NotificationEmail, String.Format("Issue {0} has been updated", issueId), String.Format("The following issue has been updated: \n\n {0}Issues/IssueDetail.aspx?id={1}", desktopDefaultUrl, issueId) );
                    } catch {}
            }
        }




        public static IssueNotificationCollection GetIssueNotificationsByIssueId(int issueId) {
            // validate input
            if (issueId <= DefaultValues.GetIssueIdMinValue() )
                throw (new ArgumentOutOfRangeException("issueId"));

            DataAccessLayerBaseClass DBLayer = DataAccessLayerBaseClassHelper.GetDataAccessLayer();
            return (DBLayer.GetIssueNotificationsByIssueId(issueId));
        }

  }
}
