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
	public class CreditRequestsDDList : DropDownList
	{


		/// <summary>
		/// Constructor
		/// </summary>
        public CreditRequestsDDList()
		{

			EnableViewState  = false;
		}

        public void Add(string[] columns)
        {
            if (columns.Length > 0)
            {
                for (int i=0; i < columns.Length; i++)
                    this.Items.Add(columns[i]);
            }
            else
                this.Visible = false;
        
        }
	}
}
