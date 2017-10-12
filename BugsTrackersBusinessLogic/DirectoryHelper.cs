using System;
using System.Configuration;
using System.DirectoryServices;

namespace BugsTrackers.BusinessLogicLayer
{
	//*********************************************************************
	//
	// DirectoryHelper Class
	//
	// Uses the DirectorySearcher .NET Framework class to retrieve user information fro
	// the Active Directory or the WindowsSAM store.
	//
	//*********************************************************************

	public class DirectoryHelper
	{
		private const string _activeDirectoryPath = @"GC://";
		private const string _filter = "(&(ObjectClass=Person)(SAMAccountName={0}))";
		private const string _windowsSAMPath = @"WinNT://";
		
		private static string _path = String.Empty;

		//*********************************************************************
		//
		// Methods are all static -- don't allow construction.
		//
		//*********************************************************************

		private DirectoryHelper()
		{
		}

		//*********************************************************************
		//
		// This does the core directory searching using the filters specified above.
		//
		//*********************************************************************

		public static bool FindUser(string identification, ref string FirstName, ref string LastName)
		{
			bool result = false;

			// Determine which method to use to retrieve user information

			// WindowsSAM
			if (ConfigurationSettings.AppSettings[Web.Global.CfgKeyUserAcctSource] == "WindowsSAM")
			{
				// Extract the machine or domain name and the user name from the 
				// identification string
				string [] samPath = identification.Split(new char[] {'\\'});
				_path = _windowsSAMPath + samPath[0];

				try
				{
					// Find the user 
					DirectoryEntry entryRoot = new DirectoryEntry(_path);
					DirectoryEntry userEntry = entryRoot.Children.Find(samPath[1], "user");
					LastName = userEntry.Properties["FullName"].Value.ToString();
				}
				catch 
				{
					result = false;
				}
				result = true;
			}

				// Active Directory
			else if (ConfigurationSettings.AppSettings[Web.Global.CfgKeyUserAcctSource] == "ActiveDirectory")
			{
				_path = _activeDirectoryPath;

				// Setup the filter
				identification = identification.Substring(identification.LastIndexOf(@"\") + 1, 
					identification.Length - identification.LastIndexOf(@"\")-1);
				string userNameFilter = string.Format(_filter, identification);

				// Get a Directory Searcher to the LDAPPath
				DirectorySearcher searcher = new DirectorySearcher(_path);
				if (searcher == null)
				{
					return false;
				}
	                
				// Add the properties that need to be retrieved
				searcher.PropertiesToLoad.Add("givenName");
				searcher.PropertiesToLoad.Add("sn");
	                        
				// Set the filter for the search
				searcher.Filter = userNameFilter;
	                
				try
				{
					// Execute the search
					SearchResult search = searcher.FindOne();
	                       
					if (search != null)
					{
						FirstName = SearchResultProperty(search, "givenName");
						LastName = SearchResultProperty(search, "sn");
						result = true;
					}
					else
						result = false;
				}
				catch
				{
					result = false;
				}
			}
			else
			{
				// The user has not choosen an UserAccountSource or UserAccountSource as None
				// Usernames will be displayed as "Domain/Username"
				result = false;
			}

			return result;
		}
        
		//*********************************************************************
		//
		// Retrieves the specified property from the SearchResult collection.
		//
		//*********************************************************************

		private static String SearchResultProperty(SearchResult sr,	string field)
		{
			if (sr.Properties[field] != null)
			{
				return (String)sr.Properties[field][0];
			}

			return null;
		}
	}
}
