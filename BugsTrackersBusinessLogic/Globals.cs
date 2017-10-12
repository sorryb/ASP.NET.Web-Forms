using System;
using System.Web;
using System.Configuration;

namespace BugsTrackers.BusinessLogicLayer {



	//*********************************************************************
	//
	// Global Class
	//
	// Contains static properties which are used globally throughout
	// the application.
	//
	//*********************************************************************


	public class Globals {



        public static string DataAccessType {
            get {
                string str = ConfigurationSettings.AppSettings["DataAccessType"];
                if (str==null || str == String.Empty)
                    throw (new ApplicationException("DataAccessType configuration is missing from you web.config. It should contain  <appSettings><add key=\"DataAccessType\" value=\"data access type\" /></appSettings> "));
                else
                    return (str);
            }
        }


        public static string SmtpServer {
            get {
                string str = ConfigurationSettings.AppSettings["SmtpServer"];
                if (str==null || str == String.Empty)
                    throw (new ApplicationException("SmtpServer configuration is missing from you web.config. It should contain  <appSettings><add key=\"SmtpServer\" value=\"localhost\" /></appSettings> "));
                else
                    return (str);
            }
        }




        public static string NotifyEmail {
            get {
                string str = ConfigurationSettings.AppSettings["NotifyEmail"];
                if (str==null || str == String.Empty)
                    throw (new ApplicationException("NotifyEmail configuration is missing from you web.config. It should contain  <appSettings><add key=\"NotifyEmail\" value=\"notify\" /></appSettings> "));
                else
                    return (str);
            }
        }



        public static string DefaultRoleForNewUser {
            get {
                string str = ConfigurationSettings.AppSettings["DefaultRoleForNewUser"];
                if (str==null || str == String.Empty)
                    throw (new ApplicationException("DefaultRoleForNewUser configuration is missing from you web.config. It should contain  <appSettings><add key=\"DefaultRoleForNewUser\" value=\"Normal\" /></appSettings> "));
                else
                    return (str);
            }
        }



        public static string DesktopDefaultUrl {
            get {
                string str = ConfigurationSettings.AppSettings["DesktopDefaultUrl"];
                if (str==null || str == String.Empty)
                    throw (new ApplicationException("DesktopDefaultUrl configuration is missing from you web.config. It should contain  <appSettings><add key=\"DesktopDefaultUrl\" value=\"http:\\\\www.YourWebSite.com\\IssueTracker\" /></appSettings> "));
                else
                    return (str);
            }
        }



        public static string MobileDefaultUrl {
            get {
                string str = ConfigurationSettings.AppSettings["MobileDefaultUrl"];
                if (str==null || str == String.Empty)
                    throw (new ApplicationException("MobileDefaultUrl configuration is missing from you web.config. It should contain  <appSettings><add key=\"MobileDefaultUrl\" value=\"http\\\\www.YourWebSite.com\\MobileIssueTracker\" /></appSettings> "));
                else
                    return (str);
            }
        }




        public static string UserAccountSource {
            get {
                string str = ConfigurationSettings.AppSettings["UserAccountSource"];
                if (str==null || str == String.Empty)
                    throw (new ApplicationException("UserAccountSource configuration is missing from you web.config. It should contain  <appSettings><add key=\"UserAccountSource\" value=\"account source type\" /></appSettings> "));
                else
                    return (str);
            }
        }


		public static bool IsMobileDevice 
		{
			get 
			{
				HttpContext context = HttpContext.Current;
				return context.Request.Browser["IsMobileDevice"] == "true" || context.Request.Browser.Platform == "WinCE";
			}
		}


		// Constants used to reference data stored in cookies
		public const string UserRoles = "userroles";
		public const string MobileUserRoles = "mobileuserroles";
		public const string IssueColumns = "issuecolumns";
		public const string SkipProjectIntro = "skipprojectintro";

	}
}
