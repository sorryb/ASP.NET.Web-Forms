using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BugTrackerWebControls
{
    /// <summary>
    /// Display info about a credit.
    /// </summary>
    [System.Drawing.ToolboxBitmap(@"D:\Work\Banca Romaneasca\Exemple\CustomControls in ASP.NET\CeditRequestsControls\CeditRequestsControls\CeditRequestsControls.CreditInfo.bmp")]
    [DefaultProperty("DetailType")]
    [ToolboxData("<{0}:CreditInfo runat=server></{0}:CreditInfo>")]   
    public class CreditInfo : WebControl
    {
        private string _detailType;
        private string _fileNumber;

        /// <summary>
        /// Detail type.
        /// </summary>
        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue("")]
        [Localizable(true)]
        public string DetailType
        {
            get
            {
                return _detailType;
            }

            set
            {
                _detailType = value;
            }
        }
        /// <summary>
        /// File number.Or credit number.
        /// </summary>
        public string FileNumber
        {
            get
            {
                return _fileNumber;
            }

            set
            {
                _fileNumber = value;
            }
        }
        /// <summary>
        /// Render content.
        /// </summary>
        /// <param name="writer">writer</param>
        protected override void RenderContents(HtmlTextWriter writer)
        {
            string outputString;
            outputString = "<table width=100% border=0 id=CreditInfo>";
            outputString += "<TR>";
            outputString += "<TD vAlign=middle>&nbsp;</TD>";
            outputString += "<TD vAlign='middle' class='text_top'>&nbsp; Tip credit :";
            outputString += "<div id='LblCreditType' ></div>";
            outputString += "</TD>";
            outputString += "<TD vAlign='middle' class='text_top' nowrap>";
            outputString += "<div align='right'>Dosar nr.:";
            outputString += "</TD>";
            outputString += "<TD vAlign='middle' class='text_top' nowrap>";
            outputString += "<div id='CreditNo' >" + FileNumber + " </div>";
            outputString += "</div>";
            outputString += "</TD>";
            outputString += "</TR>";
            outputString += "</table>";

            writer.Write(outputString);
        }
    }
}
