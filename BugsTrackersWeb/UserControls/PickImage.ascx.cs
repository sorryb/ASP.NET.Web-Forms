namespace BugsTrackers
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using System.IO;

	/// <summary>
	///		Summary description for PickImage.
	/// </summary>
	public partial class PickImage : System.Web.UI.UserControl
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
		// PickImage.ascx
		//
		// This user control displays a list of images in a radiobutton list.
		// The control is used by the Status.ascx, Priority.ascx, and Milestone.ascx
		// controls.
		//
		//*********************************************************************
    
    
		public string CssClass 
		{
			get { return lstImages.CssClass; }
			set { lstImages.CssClass = value; }
		}
    
    
		public string SelectedValue 
		{
			get { return lstImages.SelectedValue; }
			set { lstImages.SelectedValue = value; }
		}
    
    
		public string ImageDirectory = String.Empty;
    
    
    
		public void Initialize() 
		{
			DirectoryInfo objDir = new DirectoryInfo(MapPath("~/Images" + ImageDirectory));
			FileInfo[] files = objDir.GetFiles("*.gif");
    
			string formatString = "<img src=\"" + ResolveUrl("~/Images") + ImageDirectory + "/{0}\" />";
    
			lstImages.DataSource = files;
			lstImages.DataTextField = "Name";
			lstImages.DataTextFormatString = formatString;
			lstImages.DataValueField = "Name";
			lstImages.DataBind();
    
			lstImages.Items.Insert(0, new ListItem( "none", String.Empty ) );
			lstImages.SelectedIndex = 0;
		}


	
	}
}
