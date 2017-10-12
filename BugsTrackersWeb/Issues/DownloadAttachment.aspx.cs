using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using BugsTrackers.BusinessLogicLayer;

namespace BugsTrackers.Issues
{
	/// <summary>
	/// Summary description for DownloadAttachment.
	/// </summary>
	public partial class DownloadAttachment : System.Web.UI.Page
	{
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Get the attachment
			int attachmentId = Int32.Parse(Request.QueryString["id"]);
			IssueAttachment attachment = IssueAttachment.GetIssueAttachmentById(attachmentId);
    
			// Write out the attachment
			Server.ScriptTimeout = 600;
			Response.Buffer=true;
			Response.Clear();
			Response.ContentType="application/octet-stream";
			Response.AddHeader("Content-Disposition","attachment; filename=\"" + attachment.FileName + "\";");
			Response.AddHeader("Content-Length",attachment.Attachment.Length.ToString());
			Response.BinaryWrite(attachment.Attachment);
    
			// End the response
			Context.Response.End();
		}

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
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    

		}
		#endregion
	}
}
