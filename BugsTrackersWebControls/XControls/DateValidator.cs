using System;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;

namespace BugTrackerWebControls
{

	/// <summary>
	/// Web User Control to validate a date
	/// </summary>	
	public class DateValidator : BaseValidator {

        /// <summary>
        /// ControlPropertiesValid
        /// </summary>
        /// <returns></returns>
		protected override bool ControlPropertiesValid() {
			if (this.FindControl(this.ControlToValidate) is TextBox) {
				return true;
			} else {
				return false;
			}
		}

		/// <summary>
		/// Evaluate if date is a valid date.
		/// </summary>
		/// <returns></returns>
		protected override bool EvaluateIsValid() {			
			try {
				string sDate = ((TextBox)this.FindControl(this.ControlToValidate)).Text;
                if (string.IsNullOrEmpty(sDate)) return true;
				DateTime.Parse(sDate);
			}
			catch {
				return false;
			}
			return true;
		}


		/// <summary>
		/// Render the control.
		/// </summary>
		/// <param name="e"></param>
		protected override void OnPreRender(EventArgs e) {
			if (this.EnableClientScript) { 
				this.ClientScript();
			}
			this.ToolTip = "Data incorecta !";
			base.OnPreRender(e);
		}
		
		/// <summary>
		/// Build client script.
		/// </summary>
		protected void ClientScript() {
			this.Attributes["evaluationfunction"] = "doingValidateDate";

			StringBuilder validatorScript = new StringBuilder();
			validatorScript.Append("<script language=\"javascript\">");
			validatorScript.Append("\r");
			validatorScript.Append("function doingValidateDate(val) {");
			validatorScript.Append("\r");
			validatorScript.Append("var oDate = document.getElementById(val.controltovalidate);");
			validatorScript.Append("\r");
			validatorScript.Append("var sDate = oDate.value;");
			validatorScript.Append("if (sDate == \"\") return true;");
			validatorScript.Append("\r");
			validatorScript.Append("var iDay, iMonth, iYear;");
			validatorScript.Append("\r");
			validatorScript.Append("var arrValues;");
			validatorScript.Append("\r");					
			validatorScript.Append("var today = new Date();");
			validatorScript.Append("\r");
			validatorScript.Append("arrValues = sDate.split(\".\");");
			validatorScript.Append("\r");
			validatorScript.Append("iDay = arrValues[0];");
			validatorScript.Append("if (isNaN(iDay))");
			validatorScript.Append("\r");
			validatorScript.Append("return false;");
			validatorScript.Append("\r");
			validatorScript.Append("iMonth = arrValues[1];");
			validatorScript.Append("\r");
			validatorScript.Append("if (isNaN(iMonth))");
			validatorScript.Append("\r");
			validatorScript.Append("return false;");
			validatorScript.Append("\r");
			validatorScript.Append("iYear = arrValues[2];");
			validatorScript.Append("\r");
			validatorScript.Append("if (isNaN(iYear))");
			validatorScript.Append("\r");
			validatorScript.Append("return false;");
			validatorScript.Append("\r");
			validatorScript.Append("if ((iMonth == null) || (iYear == null)) return false;");
			validatorScript.Append("\r");
            validatorScript.Append("if ((iDay < 1 || iDay > 31) || (iMonth < 1 || iMonth > 12) || (iYear < 1900 || iYear > today.getFullYear()+1)) return false;");
			validatorScript.Append("\r");
            validatorScript.Append("if ((((iMonth % 2 == 0) && (iMonth<7))||((iMonth % 2 == 1) && (iMonth>8)))&& (iDay > 30)) return false;");
            validatorScript.Append("\r");
			validatorScript.Append("var daysinfeb = (((iYear % 4 == 0) && ( (!(iYear % 100 == 0)) || (iYear % 400 == 0))) ? 29 : 28 ); ");
			validatorScript.Append("\r");
			validatorScript.Append("if((daysinfeb == 28) && (iDay > 28) && (iMonth == 2)) return false;");
			validatorScript.Append("\r");
			validatorScript.Append("return true;");
			validatorScript.Append("\r");
			validatorScript.Append("}");
			validatorScript.Append("\r");
			validatorScript.Append("</script>");
			this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(),"doingValidateDate", validatorScript.ToString() );
		}
	}
}