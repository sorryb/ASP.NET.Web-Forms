using System;
using System.Collections;
using System.Web.UI.WebControls;
//using BugsTrackers.DataAccessLayer;


namespace BugsTrackers.BusinessLogicLayer
{
	public class QueryField
	{

   /*** FIELD PRIVATE ***/
    private string      _Name;
    private string      _FriendlyName;

    /*** CONSTRUCTOR ***/


    public QueryField(string friendlyName, string name) {
        _FriendlyName = friendlyName;
        _Name = name;
	}

    /*** PROPERTY ***/


    public string Name
    {
      get
      {
        if (_Name == null ||_Name.Length==0)
          return string.Empty;
        else
          return _Name;
      }
      set {_Name=value;}
    }



    public string FriendlyName
    {
      get
      {
        if (_FriendlyName == null ||_FriendlyName.Length==0)
          return string.Empty;
        else
          return _FriendlyName;
      }
    }








    }
}
