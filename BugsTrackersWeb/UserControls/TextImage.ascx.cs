namespace BugsTrackers
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	/// <summary>
	///		Summary description for TextImage.
	/// </summary>
	public partial class TextImage : System.Web.UI.UserControl
	{


		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{

		}
		#endregion

	
		//*********************************************************************
		//
		// TextImage.ascx
		//
		// This user control displays either text or an image with the text as
		// alt text depending on whether an image is supplied. This control
		// is used in the DisplayIssues.ascx control to display priorities, status
		// values, and milestones.
		//
		//*********************************************************************


		public string ImageDirectory = String.Empty;

		public string Text 
		{
			get 
			{
				if (ViewState["Text"] == null)
					return String.Empty;
				else
					return (string)ViewState["Text"];
			}
			set {ViewState["Text"] = value; }
		}


		public string ImageUrl 
		{
			get 
			{
				if (ViewState["ImageUrl"] == null)
					return String.Empty;
				else
					return (string)ViewState["ImageUrl"];
			}
			set {ViewState["ImageUrl"] = value; }
		}


		protected void Page_PreRender(object sender, System.EventArgs e) 
		{
			if (ImageUrl != String.Empty) 
			{
				ctlImage.ImageUrl = "~/Images" + ImageDirectory + "/" + ImageUrl;
				ctlImage.ToolTip = Text;
			} 
			else 
			{
				ctlImage.Visible = false;
				lblText.Visible = true;
				lblText.Text = Text;
			}
		}

		protected void Page_PreRender()
		{
		
		}

	}
}
