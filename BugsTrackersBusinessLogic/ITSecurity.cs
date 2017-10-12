using System;
using System.Web;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace BugsTrackers.BusinessLogicLayer {


	//*********************************************************************
	//
	// ITSecurity Class
	//
	// Contains static methods for retrieving user secruity information.
	//
	//*********************************************************************


    public class ITSecurity {

		public static int GetUserID() {
			return ((CustomPrincipal)HttpContext.Current.User).UserID;
		}

		public static string GetUserRole() {
			return ((CustomPrincipal)HttpContext.Current.User).UserRole;
		}

		public static string GetName() {
			return ((CustomPrincipal)HttpContext.Current.User).Name;
		}


        public ITSecurity() {
        }

						
		//*********************************************************************
		//
		// <summary>
		// Validates the input text using a Regular Expression and replaces any input expression
		// characters with empty string.Removes any characters not in [a-zA-Z0-9_]. 
		// <summary>
		// <remarks>
		// For a good reference on Regular Expressions, please see
		//	 - http://regexlib.com
		//	 - http://py-howto.sourceforge.net/regex/regex.html
		// </remarks>
		// <param name="inputText">The text to validate.</param>
		// <returns>Sanitized string</returns>
		//
		//*********************************************************************

		public static string CleanStringRegex(string inputText)
		{
			RegexOptions options = RegexOptions.IgnoreCase;
			return ReplaceRegex(inputText,@"[^\\.!?""',\-\w\s@]",options);
		}
		//*********************************************************************
		//
		// <summary>
		// Removes designated characters from an input string input text using a Regular Expression.
		// </summary>
		// <remarks>
		// For a good reference on Regular Expressions, please see
		//	 - http://regexlib.com
		//	 - http://py-howto.sourceforge.net/regex/regex.html
		// </remarks>
		// <param name="inputText">The text to clean.</param>
		// <param name="regularExpression">The regular expression</param>
		// <returns>Sanitized string.</returns>
		//
		//*********************************************************************

		private static string ReplaceRegex(string inputText, string regularExpression, RegexOptions options)
		{
			Regex regex = new Regex(regularExpression,options);
			return regex.Replace(inputText,"");
		}
    }
}
