using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Security.Permissions;



namespace BugTrackerWebControls
{
    /// <summary>
    ///  Register control.
    /// </summary>
    [
       AspNetHostingPermission(SecurityAction.Demand,
           Level = AspNetHostingPermissionLevel.Minimal),
       AspNetHostingPermission(SecurityAction.InheritanceDemand,
           Level = AspNetHostingPermissionLevel.Minimal),
       DefaultEvent("Submit"),
       DefaultProperty("ButtonText"),
       ToolboxData("<{0}:Register runat=\"server\"> </{0}:Register>"),
       ]
    public class Register : CompositeControl
    {
        private Button submitButton;
        private TextBox nameTextBox;
        private Label nameLabel;
        private TextBox emailTextBox;
        private Label emailLabel;
        private RequiredFieldValidator emailValidator;
        private RequiredFieldValidator nameValidator;

        private static readonly object EventSubmitKey =
            new object();



        /// <summary>
        /// Button Text.
        ///   The following properties are delegated to 
        /// child controls.
        /// </summary>
        [
        Bindable(true),
        Category("Appearance"),
        DefaultValue(""),
        Description("The text to display on the button.")
        ]
        public string ButtonText
        {
            get
            {
                EnsureChildControls();
                return submitButton.Text;
            }
            set
            {
                EnsureChildControls();
                submitButton.Text = value;
            }
        }

        /// <summary>
        /// Name.
        /// </summary>
        [
        Bindable(true),
        Category("Default"),
        DefaultValue(""),
        Description("The user name.")
        ]
        public string Name
        {
            get
            {
                EnsureChildControls();
                return nameTextBox.Text;
            }
            set
            {
                EnsureChildControls();
                nameTextBox.Text = value;
            }
        }

        /// <summary>
        /// Name Error Message.
        /// </summary>
        [
        Bindable(true),
        Category("Appearance"),
        DefaultValue(""),
        Description(
            "Error message for the name validator.")
        ]
        public string NameErrorMessage
        {
            get
            {
                EnsureChildControls();
                return nameValidator.ErrorMessage;
            }
            set
            {
                EnsureChildControls();
                nameValidator.ErrorMessage = value;
                nameValidator.ToolTip = value;
            }
        }

        /// <summary>
        /// Name Label Text.
        /// </summary>
        [
        Bindable(true),
        Category("Appearance"),
        DefaultValue(""),
        Description("The text for the name label.")
        ]
        public string NameLabelText
        {
            get
            {
                EnsureChildControls();
                return nameLabel.Text;
            }
            set
            {
                EnsureChildControls();
                nameLabel.Text = value;
            }
        }

        /// <summary>
        /// The e-mail address."
        /// </summary>
        [
        Bindable(true),
        Category("Default"),
        DefaultValue(""),
        Description("The e-mail address.")
        ]
        public string Email
        {
            get
            {
                EnsureChildControls();
                return emailTextBox.Text;
            }
            set
            {
                EnsureChildControls();
                emailTextBox.Text = value;
            }
        }

        /// <summary>
        /// Error message for the e-mail validator.
        /// </summary>
        [
        Bindable(true),
        Category("Appearance"),
        DefaultValue(""),
        Description(
            "Error message for the e-mail validator.")
        ]
        public string EmailErrorMessage
        {
            get
            {
                EnsureChildControls();
                return emailValidator.ErrorMessage;
            }
            set
            {
                EnsureChildControls();
                emailValidator.ErrorMessage = value;
                emailValidator.ToolTip = value;
            }
        }

        /// <summary>
        /// Email Label Text.
        /// </summary>
        [
        Bindable(true),
        Category("Appearance"),
        DefaultValue(""),
        Description("The text for the e-mail label.")
        ]
        public string EmailLabelText
        {
            get
            {
                EnsureChildControls();
                return emailLabel.Text;
            }
            set
            {
                EnsureChildControls();
                emailLabel.Text = value;

            }
        }

        /// <summary>
        ///  The Submit event.
        /// </summary>
        [
        Category("Action"),
        Description("Raised when the user clicks the button.")
        ]
        public event EventHandler Submit
        {
            add
            {
                Events.AddHandler(EventSubmitKey, value);
            }
            remove
            {
                Events.RemoveHandler(EventSubmitKey, value);
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

        /// <summary>
        /// Recreate ChildControls.
        /// </summary>
        protected override void RecreateChildControls()
        {
            EnsureChildControls();
        }

        /// <summary>
        /// Create ChildControls.
        /// </summary>
        protected override void CreateChildControls()
        {
            Controls.Clear();

            nameLabel = new Label();

            nameTextBox = new TextBox();
            nameTextBox.ID = "nameTextBox";

            nameValidator = new RequiredFieldValidator();
            nameValidator.ID = "validator1";
            nameValidator.ControlToValidate = nameTextBox.ID;
            nameValidator.Text = "Failed validation.";
            nameValidator.Display = ValidatorDisplay.Static;

            emailLabel = new Label();

            emailTextBox = new TextBox();
            emailTextBox.ID = "emailTextBox";

            emailValidator = new RequiredFieldValidator();
            emailValidator.ID = "validator2";
            emailValidator.ControlToValidate =
                emailTextBox.ID;
            emailValidator.Text = "Failed validation.";
            emailValidator.Display = ValidatorDisplay.Static;

            submitButton = new Button();
            submitButton.ID = "button1";
            submitButton.Click
                += new EventHandler(_button_Click);

            this.Controls.Add(nameLabel);
            this.Controls.Add(nameTextBox);
            this.Controls.Add(nameValidator);
            this.Controls.Add(emailLabel);
            this.Controls.Add(emailTextBox);
            this.Controls.Add(emailValidator);
            this.Controls.Add(submitButton);
        }

        /// <summary>
        /// render control.
        /// </summary>
        /// <param name="writer"></param>
        protected override void Render(HtmlTextWriter writer)
        {
            AddAttributesToRender(writer);

            writer.AddAttribute(
                HtmlTextWriterAttribute.Cellpadding,
                "1", false);
            writer.RenderBeginTag(HtmlTextWriterTag.Table);

            writer.RenderBeginTag(HtmlTextWriterTag.Tr);
            writer.RenderBeginTag(HtmlTextWriterTag.Td);

            nameLabel.RenderControl(writer);

            writer.RenderEndTag();
            writer.RenderBeginTag(HtmlTextWriterTag.Td);

            nameTextBox.RenderControl(writer);

            writer.RenderEndTag();
            writer.RenderBeginTag(HtmlTextWriterTag.Td);

            nameValidator.RenderControl(writer);

            writer.RenderEndTag();
            writer.RenderEndTag();

            writer.RenderBeginTag(HtmlTextWriterTag.Tr);
            writer.RenderBeginTag(HtmlTextWriterTag.Td);

            emailLabel.RenderControl(writer);

            writer.RenderEndTag();
            writer.RenderBeginTag(HtmlTextWriterTag.Td);

            emailTextBox.RenderControl(writer);

            writer.RenderEndTag();
            writer.RenderBeginTag(HtmlTextWriterTag.Td);

            emailValidator.RenderControl(writer);

            writer.RenderEndTag();
            writer.RenderEndTag();

            writer.RenderBeginTag(HtmlTextWriterTag.Tr);
            writer.AddAttribute(
                HtmlTextWriterAttribute.Colspan,
                "2", false);
            writer.AddAttribute(
                HtmlTextWriterAttribute.Align,
                "right", false);
            writer.RenderBeginTag(HtmlTextWriterTag.Td);

            submitButton.RenderControl(writer);

            writer.RenderEndTag();
            writer.RenderBeginTag(HtmlTextWriterTag.Td);
            writer.Write("&nbsp;");
            writer.RenderEndTag();
            writer.RenderEndTag();

            writer.RenderEndTag();
        }
    }

}
