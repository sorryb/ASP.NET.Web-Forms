using System;
using System.Diagnostics;

namespace BugTrackerWebControls.Common
{
	/// <summary>
	/// Log applications errors.
	/// </summary>
	public class LogError : Object
	{
		#region ----------------User defined types-----------------
        /// <summary>
        /// ErrorMessage
        /// </summary>
		public struct ErrorMessage
		{

            /// <summary>
            /// 5000 = success
            /// 5001 = mandatory parameter with no value
            /// 5002 = incorrect parameters
            /// 5003 = internal calls
            /// </summary>
			private int errorCode;

            /// <summary>
            /// ErrorCode
            /// </summary>
			public int ErrorCode;
		
			private string errorDesc;

            /// <summary>
            /// Error description.
            /// </summary>
			public string ErrorDescription;
		
			/// <summary>
			/// cstr.
			/// </summary>
            /// <param name="errorCodeValue"></param>
            /// <param name="errorDescriptionValue"></param>
			public ErrorMessage(int errorCodeValue, string errorDescriptionValue)
			{
				errorCode = errorCodeValue;
				errorDesc = errorDescriptionValue;
				ErrorCode = errorCodeValue;
				ErrorDescription = errorDescriptionValue;
			}


		}
		/// <summary>
		/// Priority of logs
		/// </summary>
		public struct Priority
		{
            /// <summary>
            ///  Lowest  = 0;
            /// </summary>
			public const int Lowest  = 0;
            /// <summary>
            /// Low     = 1;
            /// </summary>
			public const int Low     = 1;
            /// <summary>
            /// Normal  = 2;
            /// </summary>
			public const int Normal  = 2;
            /// <summary>
            /// High    = 3
            /// </summary>
			public const int High    = 3;
            /// <summary>
            /// Highest = 4;
            /// </summary>
			public const int Highest = 4;
		}
		/// <summary>
		/// Categories of logs
		/// </summary>
		public struct Category
		{
            /// <summary>
            /// General = "General";
            /// </summary>
			public const string General = "General";
            /// <summary>
            /// Trace   = "Trace";
            /// </summary>
			public const string Trace   = "Trace";
		}
		#endregion------------------------------------------------------

        #region --------public properties-----------------------------
        /// <summary>
        /// Source of error message.
        /// </summary>
        public string Source
        {
            get { return source; }
            set { source = value; }
        }
        /// <summary>
        /// Type of logging.
        /// </summary>
        public EventLogEntryType LogType
        {
            get { return _eventLogEntryType; }
            set { _eventLogEntryType = value; }        
        }
        #endregion --------public properties-----------------------------


        #region ------------ private members -----------------------------
        private string message;
        private string source = "CreditRequests";//"BugTrackerWebControls";
        private EventLogEntryType _eventLogEntryType = EventLogEntryType.Error;
        #endregion -------------------------------------------
   

		/// <summary>
		/// Constructor.
		/// </summary>
		public LogError()
		{			
		}

		/// <summary>
		/// Constructor
		/// </summary>
		public LogError(string logMessage)
        {
            message = logMessage;
		}

		/// <summary>
		/// Log en error to EventView-er.
		/// </summary>
        /// <param name="errorMessage"></param>
        /// <param name="errorStackTrace"></param>
		public  LogError(string errorMessage,string errorStackTrace)
        {
            this.message = string.Format("Eroarea este :{0} la  {1}", errorMessage, errorStackTrace);
		}

		
		/// <summary>
		/// Write log errors.
		/// </summary>
		public void Write()
		{
            EventLog.WriteEntry(source, message, _eventLogEntryType);
		}


	}
}
