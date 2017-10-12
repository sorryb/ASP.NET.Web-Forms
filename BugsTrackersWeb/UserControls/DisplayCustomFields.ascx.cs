namespace BugsTrackers
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using BugsTrackers.BusinessLogicLayer;

	/// <summary>
	///		Summary description for DisplayCustomFields.
	/// </summary>
	public partial class DisplayCustomFields : System.Web.UI.UserControl
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
			this.lstCustomFields.ItemDataBound += new System.Web.UI.WebControls.DataListItemEventHandler(this.lstCustomFieldsItemDataBound);

		}
		#endregion

	
		//*********************************************************************
		//
		// DisplayCustomFields.ascx
		//
		// This user control displays the custom fields displayed in the
		// IssueDetail.aspx page.
		//
		//*********************************************************************


		public bool Required = true;


		public object DataSource 
		{
			get { return lstCustomFields.DataSource; }
			set { lstCustomFields.DataSource = value; }
		}

		public void DataBind() 
		{
			lstCustomFields.DataKeyField = "Id";
			lstCustomFields.DataBind();
			if (lstCustomFields.Items.Count == 0)
				divider.Visible = false;
			else
				divider.Visible = true;
		}

		void lstCustomFieldsItemDataBound(object s, DataListItemEventArgs e) 
		{
			if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem) 
			{
				CustomField currentField = (CustomField)e.Item.DataItem;

				Label lblFieldName = (Label)e.Item.FindControl("lblFieldName");
				lblFieldName.Text = currentField.Name;

				TextBox txtFieldValue = (TextBox)e.Item.FindControl( "txtFieldValue" );
				txtFieldValue.Text = currentField.Value;

				RequiredFieldValidator valReq = (RequiredFieldValidator)e.Item.FindControl( "valReq" );
				if (!Required)
					valReq.Visible = false;
				else
					valReq.Visible = currentField.Required;

				CompareValidator valCompare = (CompareValidator)e.Item.FindControl( "valCompare" );
				valCompare.Type = currentField.DataType;
				valCompare.Text = String.Format("({0})", currentField.DataType );
			}
		}


		public CustomFieldCollection Values 
		{
			get 
			{
				CustomFieldCollection colFields = new CustomFieldCollection();
				for (int i=0;i < lstCustomFields.Items.Count;i++) 
				{
					DataListItem item = lstCustomFields.Items[i];
					if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem) 
					{
						int fieldId = (int)lstCustomFields.DataKeys[i];
						string fieldValue = ((TextBox)item.FindControl("txtFieldValue")).Text;
						colFields.Add( new CustomField(fieldId, fieldValue) );
					}
				}
				return colFields;
			}
		}


	
	}
}
