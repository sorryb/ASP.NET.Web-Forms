using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;

namespace BugTrackerWebControls
{
    /// <summary>
    /// Hyperlink field for template column in a grid view.
    /// </summary>
    public class CreditRequestsHyperlinkField : System.Web.UI.WebControls.HyperLinkField
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public CreditRequestsHyperlinkField()
        { 

        }

        /// <summary>
        /// Override Format method for URL.
        /// </summary>
        /// <param name="dataUrlValues"></param>
        /// <returns></returns>
        protected override string FormatDataNavigateUrlValue(object[] dataUrlValues)
        {
            string url = string.Format(this.DataNavigateUrlFormatString, dataUrlValues);

            this.NavigateUrl = url.Replace("javascript:", "");

            return url;

        }

        /// <summary>
        /// Encode URL.
        /// </summary>
        /// <param name="dataUrlValues"></param>
        /// <returns></returns>
        protected virtual object[] EncodeDataUrlValues(object[] dataUrlValues)
        {

            if (dataUrlValues == null || dataUrlValues.Length == 0) return dataUrlValues;

            if (this.DesignMode) return dataUrlValues;
     
            object[] encodedDataUrlValues = new object[dataUrlValues.Length ];

            for (int index = 0; index < dataUrlValues.Length ; index++)
            {
                encodedDataUrlValues[index] = HttpUtility.UrlEncode(dataUrlValues[index].ToString());                
            }

            return encodedDataUrlValues;

        }

    }
}
