using System;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BugTrackerWebControls
{
    /// <summary>
    /// combo box CreditDropDownList.
    /// </summary>
    public partial class CreditDropDownList :System.Web.UI.WebControls.DropDownList
    {
        /// <summary>
        /// cstr
        /// </summary>
        public CreditDropDownList()
        {
        }
        /// <summary>
        /// Render the control.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPreRender(EventArgs e)
        {
            this.ClientScript();


            base.OnPreRender(e);
        }

        /// <summary>
        /// Build client script.
        /// </summary>
        protected static void AddFunctionAbsoluteTop(StringBuilder validatorScript)
        {
            validatorScript.Append("\r");
            validatorScript.Append("function getAbsoluteTopPosition(o) {");
            validatorScript.Append("\r");
            validatorScript.Append("oTop = o.offsetTop  ");
            validatorScript.Append("\r");
            validatorScript.Append("while(o.offsetParent!=null) {");
            validatorScript.Append("\r");
            validatorScript.Append("oParent = o.offsetParent;");
            validatorScript.Append("\r");
            validatorScript.Append("oTop += oParent.offsetTop;");
            validatorScript.Append("\r");
            validatorScript.Append("o = oParent");
            validatorScript.Append("\r");
            validatorScript.Append("}");
            validatorScript.Append("\r");
            validatorScript.Append("return oTop;");
            validatorScript.Append("\r");
            validatorScript.Append("}");
            validatorScript.Append("\r");
        }

        /// <summary>
        /// Build client script.
        /// </summary>
        protected void AddSpan(StringBuilder validatorScript)
        {
            validatorScript.Append("\r");
            validatorScript.Append("<SPAN id='" + this.UniqueID + "_tooltip" + "' style='BORDER-RIGHT: #000000 1px solid; PADDING-RIGHT: ");
            validatorScript.Append("3px; BORDER-TOP: #000000 1px solid; DISPLAY: inline; PADDING-LEFT: 3px;");
            validatorScript.Append("\r");
            validatorScript.Append("FONT-SIZE: 12px; PADDING-BOTTOM: 3px; BORDER-LEFT: #000000 1px solid;");
            validatorScript.Append("PADDING-TOP: 3px; BORDER-BOTTOM: #000000 1px solid; FONT-FAMILY: Verdana;");
            validatorScript.Append("POSITION: absolute; BACKGROUND-COLOR: #ffffe1;display:none;Width:370px;horizontal-align:left;'>");
            validatorScript.Append("</SPAN>	");
            validatorScript.Append("\r");
        }
        /// <summary>
        /// Build client script.
        /// </summary>
        protected void ClientScript()
        {
            this.Attributes.Add("onmouseenter", "showHideToolTip('enter','" + this.UniqueID + "');");
            this.Attributes.Add("onmouseleave", "showHideToolTip('leave','" + this.UniqueID + "')");

            StringBuilder validatorScript = new StringBuilder();
            validatorScript.Append("<script language=\"javascript\">");
            validatorScript.Append("\r");
            validatorScript.Append("function showHideToolTip(eventName,objName)  {");
            validatorScript.Append("\r");
            validatorScript.Append("var obj = document.getElementById(objName);");
            validatorScript.Append("var objTooltip = document.getElementById('" + this.UniqueID + "_tooltip" + "');");
            validatorScript.Append("\r");
            validatorScript.Append("objTooltip.innerHTML =	obj.options[obj.selectedIndex].innerText;");
            validatorScript.Append("if(obj.options[obj.selectedIndex].innerText != '')");
            validatorScript.Append("\r");
            validatorScript.Append("if(event.type == 'mouseenter')");
            validatorScript.Append("\r");
            validatorScript.Append("{");
            validatorScript.Append("\r");
            validatorScript.Append("objTooltip.style.display = 'inline';");
            validatorScript.Append("\r");
            validatorScript.Append("objTooltip.style.top = getAbsoluteTopPosition(obj) + obj.offsetHeight  < event.y?event.y:getAbsoluteTopPosition(obj)+ obj.offsetHeight  ;");
            validatorScript.Append("\r");
            validatorScript.Append("objTooltip.style.left = event.x;");
            validatorScript.Append("\r");
            validatorScript.Append("objTooltip.style.width = objTooltip.innerHTML.length * 8;");
            validatorScript.Append("\r");
            validatorScript.Append("objTooltip.style.zIndex = obj.style.zIndex + 100;");
            validatorScript.Append("\r");
            validatorScript.Append("objTooltip.style.wordWrap = 'normal'");
            validatorScript.Append("}");
            validatorScript.Append("\r");
            validatorScript.Append("if(event.type == 'mouseleave')");
            validatorScript.Append("\r");
            validatorScript.Append("{");
            validatorScript.Append("\r");
            validatorScript.Append("objTooltip.style.display = 'none';");
            validatorScript.Append("\r");
            validatorScript.Append("}");
            validatorScript.Append("\r");
            validatorScript.Append("}");
            validatorScript.Append("\r");

            AddFunctionAbsoluteTop(validatorScript);

            validatorScript.Append("</script>");

            this.AddSpan(validatorScript);


            this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "doingScriptPage", validatorScript.ToString());
        }
    }
}
