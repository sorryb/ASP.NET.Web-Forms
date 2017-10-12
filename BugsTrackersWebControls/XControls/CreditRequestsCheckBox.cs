using System;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;

namespace BugTrackerWebControls
{
	/// <summary>
    /// CreditRequestsCheckBox extend CheckBox.
	/// </summary>
	public class CreditRequestsCheckBox : CheckBox
	{
        /// <summary>
        /// value
        /// </summary>
		public string value;

		/// <summary>
		/// Constructor
		/// </summary>
        public CreditRequestsCheckBox()
		{

			EnableViewState  = false;
		}
	}
}
