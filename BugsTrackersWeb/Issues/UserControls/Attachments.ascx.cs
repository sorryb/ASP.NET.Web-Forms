using BugsTrackers.BusinessLogicLayer;
using System.IO;

namespace BugsTrackers.Issues.UserControls
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	/// <summary>
	///		Summary description for Attachments.
	/// </summary>
	public partial class Attachments : System.Web.UI.UserControl
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
			this.grdAttachments.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.grdAttachments_ItemCommand);
			this.grdAttachments.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.grdAttachments_ItemDataBound);
			this.PreRender += new System.EventHandler(this.Attachments_PreRender);

		}
		#endregion


		private int _IssueId = 0;


		public int IssueId 
		{
			get { return _IssueId; }
			set { _IssueId = value; }
		}


		public void Initialize() 
		{
			BindAttachments();
		}



		void BindAttachments() 
		{
			grdAttachments.DataSource = IssueAttachment.GetIssueAttachmentsByIssueId(_IssueId);
			grdAttachments.DataKeyField = "Id";
			grdAttachments.DataBind();
		}



		private void grdAttachments_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem) 
			{
				IssueAttachment attachment = (IssueAttachment)e.Item.DataItem;

				LinkButton lnkDelete = (LinkButton)e.Item.FindControl( "lnkDelete" );
				if (String.Compare(Page.User.Identity.Name,attachment.CreatorUsername) == 0 )
					lnkDelete.Visible = true;
			}
		}

		private void grdAttachments_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			int attachmentId = (int)grdAttachments.DataKeys[e.Item.ItemIndex];
			IssueAttachment.DeleteIssueAttachment(attachmentId);
			BindAttachments();
		}

		protected void btnAdd_Click(object sender, System.EventArgs e)
		{
			// Get Posted File
			HttpPostedFile theFile = upAttachment.PostedFile;


			// Check if file was actually uploaded
			if (theFile == null)
				return;

			// Convert Posted File to Byte Array
			int fileSize = theFile.ContentLength;
			byte[] fileBytes = new byte[fileSize];
			System.IO.Stream myStream = theFile.InputStream;
			myStream.Read(fileBytes,0,fileSize);

			// Get file name
			string fileName = Path.GetFileName(theFile.FileName);

			// Create new Issue Attachment
			IssueAttachment newAttachment = new IssueAttachment(IssueId, Page.User.Identity.Name, fileName, theFile.ContentType, fileBytes);
			newAttachment.Save();
			BindAttachments();
		}

		protected void Attachments_PreRender(object sender, System.EventArgs e)
		{
			if (Page.User.IsInRole("Guest"))
				btnAdd.Enabled = false;
		}





	}
}
