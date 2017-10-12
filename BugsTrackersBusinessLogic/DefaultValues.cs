using System;
using System.Data.SqlTypes;

namespace BugsTrackers.BusinessLogicLayer {


	//*********************************************************************
	//
	// DefaultValues Class
	//
	// The DefaultValues class contains static properties which represent
	// the minimum values for database identity fields.
	//
	//*********************************************************************


	public class DefaultValues {

        public static int GetCategoryIdMinValue() {
            return (0);
        }

        public static DateTime GetDateTimeMinValue() {
            DateTime MinValue= (DateTime)SqlDateTime.MinValue;
            MinValue.AddYears(1);
            return (MinValue);
        }

        public static int GetCustomFieldIdMinValue() {
            return (0);
        }

        public static int GetIssueIdMinValue() {
            return (0);
        }

        public static int GetIssueCommentIdMinValue() {
            return (0);
        }

		public static int GetIssueAttachmentIdMinValue() 
		{
			return (0);
		}

        public static int GetIssueNotificationIdMinValue() 
		{
            return (0);
        }

        public static int GetMilestoneIdMinValue() {
            return (0);
        }

        public static int GetPriorityIdMinValue() {
            return (0);
        }

        public static int GetProjectIdMinValue() {
            return (0);
        }

        public static int GetStatusIdMinValue() {
            return (0);
        }

        public static int GetUserIdMinValue() {
            return (0);
        }

        public static int GetQueryIdMinValue() {
            return (0);
        }


  }
}
