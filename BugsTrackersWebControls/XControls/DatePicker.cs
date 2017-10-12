using System;
using System.ComponentModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Security.Permissions;
using System.Web.UI.HtmlControls;

namespace BugTrackerWebControls.XControls
{

    /// <summary>
    ///  DatePicker Jquery control.
    /// </summary>
    [
       AspNetHostingPermission(SecurityAction.Demand,
           Level = AspNetHostingPermissionLevel.Minimal),
       AspNetHostingPermission(SecurityAction.InheritanceDemand,
           Level = AspNetHostingPermissionLevel.Minimal),
       DefaultEvent("Submit"),
       //DefaultProperty("DateLabelText"),
       ToolboxData("<{0}:Register runat=\"server\"> </{0}:Register>"),
       ]
    public class DatePicker : System.Web.UI.WebControls.WebControl, INamingContainer
    {
        //private Button submitButton;
        private TextBox dateTextBox;
        private Label dateLabel;
        private RequiredFieldValidator dateValidator;
        private LiteralControl literalJScript;

        private static readonly object EventSubmitKey =
            new object();

        //private bool registerJQuery;

        public event EventHandler DateChanged;

        ///// <summary>
        ///// Register JQuery jscript library .
        ///// </summary>
        //public bool RegisterJQuery
        //{
        //    get
        //    {
        //        return registerJQuery;
        //    }
        //    set
        //    {
        //        registerJQuery = value;
        //    }
        //}


        /// <summary>
        /// Date Error Message.
        /// </summary>
        [
        Bindable(true),
        Category("Appearance"),
        DefaultValue(""),
        Description(
            "Error message for the date validator.")
        ]
        public string NameErrorMessage
        {
            get
            {
                EnsureChildControls();
                return dateValidator.ErrorMessage;
            }
            set
            {
                EnsureChildControls();
                dateValidator.ErrorMessage = value;
                dateValidator.ToolTip = value;
            }
        }

        ///// <summary>
        ///// Name Label Text.
        ///// </summary>
        //[
        //Bindable(true),
        //Category("Appearance"),
        //DefaultValue(""),
        //Description("The text for the name label.")
        //]
        //public string DateLabelText
        //{
            //get
            //{
            //    EnsureChildControls();
            //    return dateLabel.Text;
            //}
            //set
            //{
            //    EnsureChildControls();
            //    dateLabel.Text = value;
            //}
        //}

        /// <summary>
        /// Date field value.
        /// </summary>
        [
        Bindable(true),
        Category("Appearance"),
        DefaultValue(""),
        Description("The text for the date.")
        ]
        public TextBox DateTextBox
        {
            get
            {
                return dateTextBox ;
            }
            set
            {
                dateTextBox  = value;
            }
        }

        /// <summary>
        /// The method that raises the Submit event.
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnSubmit(EventArgs e)
        {
            EventHandler SubmitHandler =
                (EventHandler)Events[EventSubmitKey];
            if (SubmitHandler != null)
            {
                SubmitHandler(this, e);
            }
        }

        /// <summary>
        ///  Handles the Click event of the Button and raises
        /// the Submit event.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        private void _button_Click(object source, EventArgs e)
        {
            OnSubmit(EventArgs.Empty);
        }

        protected void dateTextBox_TextChanged(object sender, EventArgs e)
        {

            if (DateChanged != null)
                DateChanged(this, EventArgs.Empty);
        }



        /// <summary>
        /// Create ChildControls.
        /// </summary>
        protected override void CreateChildControls()
        {
            Controls.Clear();

            string jscript ="<script>";
            jscript += "CreateDatePicker('" + Page.ClientScript.GetWebResourceUrl(this.GetType(), "BugTrackerWebControls.Images.calendar.gif") +"');</script>";

            literalJScript = new LiteralControl(jscript);
            Controls.Add(literalJScript);

            dateLabel = new Label();

            dateTextBox = new TextBox();
            dateTextBox.ID = "dateTextBox";
            //dateTextBox.Attributes.Add("onchange", "document.forms[0].submit();");
            dateTextBox.TextChanged += new System.EventHandler(dateTextBox_TextChanged);
            dateTextBox.AutoPostBack = true;

            dateValidator = new RequiredFieldValidator();
            dateValidator.ID = "dateValidator";
            dateValidator.ControlToValidate = dateTextBox.ID;
            dateValidator.Text = "Failed validation.";
            dateValidator.Display = ValidatorDisplay.Static;

            //submitButton = new Button();
            //submitButton.ID = "buttonSubmit";
            //submitButton.Click
            //    += new EventHandler(_button_Click);

            this.Controls.Add(dateLabel);
            this.Controls.Add(dateTextBox);
            this.Controls.Add(dateValidator);
            //this.Controls.Add(submitButton);


            AddResources();
            
        }

        private void AddResources()
        {
            //register client script.
            WebControl thisObject = (WebControl)this;


            if (thisObject.Page != null)
            {
                if (!thisObject.Page.ClientScript.IsClientScriptIncludeRegistered(thisObject.GetType(), "BugTrackerWebControls.DatePicker"))
                    thisObject.Page.ClientScript.RegisterClientScriptInclude(thisObject.GetType(), "BugTrackerWebControls.DatePicker", thisObject.Page.ClientScript.GetWebResourceUrl(thisObject.GetType(), "BugTrackerWebControls.Scripts.DatePicker.js"));
                
                //if (registerJQuery)
                //    if (!thisObject.Page.ClientScript.IsClientScriptIncludeRegistered(thisObject.GetType(), "BugTrackerWebControls.jquery-1.6.2"))
                //        thisObject.Page.ClientScript.RegisterClientScriptInclude(thisObject.GetType(), "BugTrackerWebControls.jquery-1.6.2", thisObject.Page.ClientScript.GetWebResourceUrl(thisObject.GetType(), "BugTrackerWebControls.Scripts.jquery-1.6.2.js"));
                
                
                if (!Page.ClientScript.IsClientScriptIncludeRegistered("JQuery" ))
                    Page.ClientScript.RegisterClientScriptInclude("JQuery", Page.ClientScript.GetWebResourceUrl(this.GetType(), "BugTrackerWebControls.Scripts.jquery-1.6.2.js"));

                if (!thisObject.Page.ClientScript.IsClientScriptIncludeRegistered(thisObject.GetType(), "BugTrackerWebControls.jquery.ui.core"))
                    thisObject.Page.ClientScript.RegisterClientScriptInclude(thisObject.GetType(), "BugTrackerWebControls.jquery.ui.core", thisObject.Page.ClientScript.GetWebResourceUrl(thisObject.GetType(), "BugTrackerWebControls.Scripts.jquery.ui.core.js"));

                if (!thisObject.Page.ClientScript.IsClientScriptIncludeRegistered(thisObject.GetType(), "BugTrackerWebControls.jquery.ui.widget"))
                    thisObject.Page.ClientScript.RegisterClientScriptInclude(thisObject.GetType(), "BugTrackerWebControls.jquery.ui.widget", thisObject.Page.ClientScript.GetWebResourceUrl(thisObject.GetType(), "BugTrackerWebControls.Scripts.jquery.ui.widget.js"));

                if (!thisObject.Page.ClientScript.IsClientScriptIncludeRegistered(thisObject.GetType(), "BugTrackerWebControls.jquery.ui.datepicker"))
                    thisObject.Page.ClientScript.RegisterClientScriptInclude(thisObject.GetType(), "BugTrackerWebControls.jquery.ui.datepicker", thisObject.Page.ClientScript.GetWebResourceUrl(thisObject.GetType(), "BugTrackerWebControls.Scripts.jquery.ui.datepicker.js"));

                if (!thisObject.Page.ClientScript.IsClientScriptIncludeRegistered(thisObject.GetType(), "BugTrackerWebControls.jquery.effects.core"))
                    thisObject.Page.ClientScript.RegisterClientScriptInclude(thisObject.GetType(), "BugTrackerWebControls.jquery.effects.core", thisObject.Page.ClientScript.GetWebResourceUrl(thisObject.GetType(), "BugTrackerWebControls.Scripts.jquery.effects.core.js"));

                if (!thisObject.Page.ClientScript.IsClientScriptIncludeRegistered(thisObject.GetType(), "BugTrackerWebControls.jquery.effects.fold"))
                    thisObject.Page.ClientScript.RegisterClientScriptInclude(thisObject.GetType(), "BugTrackerWebControls.jquery.effects.fold", thisObject.Page.ClientScript.GetWebResourceUrl(thisObject.GetType(), "BugTrackerWebControls.Scripts.jquery.effects.fold.js"));

                if (!thisObject.Page.ClientScript.IsClientScriptIncludeRegistered(thisObject.GetType(), "BugTrackerWebControls.jquery.ui.datepicker-ro"))
                    thisObject.Page.ClientScript.RegisterClientScriptInclude(thisObject.GetType(), "BugTrackerWebControls.jquery.ui.datepicker-ro", thisObject.Page.ClientScript.GetWebResourceUrl(thisObject.GetType(), "BugTrackerWebControls.Scripts.jquery.ui.datepicker-ro.js"));
            }

            //------------------------add css dynamic!-----------------
            if (Page.Header != null)// the <HEAD must runat server
            {
                HtmlLink lnkStyle = new HtmlLink();
                //lnkStyle.Href = this.Page.ClientScript.GetWebResourceUrl(this.GetType(), "BugTrackerWebControls.Theme.demos.css");
                //lnkStyle.Attributes["rel"] = "stylesheet";
                //lnkStyle.Attributes["type"] = "text/css";
                //lnkStyle.ID = "demos.css";

                //Page.Header.Controls.Add(lnkStyle);

                lnkStyle.Href = "App_Themes\\redmond\\jquery-ui-1.8.16.custom.css";// this.Page.ClientScript.GetWebResourceUrl(this.GetType(), "BugTrackerWebControls.Theme.redmond.jquery-ui-1.8.16.custom.css");
                lnkStyle.Attributes["rel"] = "stylesheet";
                lnkStyle.Attributes["type"] = "text/css";
                lnkStyle.ID = "jquery-ui-1.8.16.custom.css";

                Page.Header.Controls.Add(lnkStyle);
            }
        }

        /// <summary>
        /// render control.
        /// </summary>
        /// <param name="writer"></param>
        protected override void Render(HtmlTextWriter writer)
        {


            AddAttributesToRender(writer);

            literalJScript.RenderControl(writer);

            writer.AddAttribute(
                HtmlTextWriterAttribute.Cellpadding,
                "1", false);
            writer.RenderBeginTag(HtmlTextWriterTag.Table);

            writer.RenderBeginTag(HtmlTextWriterTag.Tr);
            writer.RenderBeginTag(HtmlTextWriterTag.Td);

            dateLabel.RenderControl(writer);

            writer.RenderEndTag();
            writer.RenderBeginTag(HtmlTextWriterTag.Td);

            dateTextBox.RenderControl(writer);

            writer.RenderEndTag();
            writer.RenderBeginTag(HtmlTextWriterTag.Td);

            dateValidator.RenderControl(writer);

            writer.RenderEndTag();
            writer.RenderBeginTag(HtmlTextWriterTag.Td);

            //submitButton.RenderControl(writer);

            writer.RenderEndTag();

            writer.RenderEndTag();

            writer.RenderEndTag();
        }
    }
}
