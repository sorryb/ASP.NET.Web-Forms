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
	///		Summary description for PickQueryField.
	/// </summary>
	public partial class PickQueryField : System.Web.UI.UserControl
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
		// PickQueryField.ascx
		//
		// This user control displays the query clause used in the QueryDetail.aspx
		// page.
		//
		//*********************************************************************






		//*********************************************************************
		//
		// ProjectId Property
		//
		// The ProjectId property is used to retrieve the proper status, milestone,
		// and priority values for the current project.
		//
		//*********************************************************************


		public int ProjectId 
		{
			get 
			{
				if (ViewState["ProjectId"] == null)
					return 0;
				else
					return (int)ViewState["ProjectId"];
			}
			set {ViewState["ProjectId"] = value; }
		}





		//*********************************************************************
		//
		// dropFieldSelectedIndexChanged Method
		//
		// When the user changes the selected field type, show the corresponding list
		// of possible values.
		//
		//*********************************************************************

		protected void dropFieldSelectedIndexChanged(Object s, EventArgs e) 
		{
			dropValue.Items.Clear();
			switch (dropField.SelectedValue) 
			{
				case "IssuePriorityId" :
					dropValue.Visible = true;
					txtValue.Visible = false;
					dropValue.DataSource = Priority.GetPrioritiesByProjectId(ProjectId);
					dropValue.DataTextField = "Name";
					dropValue.DataValueField = "Id";
					break;
				case "IssueMilestoneId" :
					dropValue.Visible = true;
					txtValue.Visible = false;
					dropValue.DataSource = Milestone.GetMilestoneByProjectId(ProjectId);
					dropValue.DataTextField = "Name";
					dropValue.DataValueField = "Id";
					break;
				case "IssueCategoryId" :
					dropValue.Visible = true;
					txtValue.Visible = false;
					CategoryTree objCats = new CategoryTree();
					dropValue.DataSource = objCats.GetCategoryTreeByProjectId(ProjectId);
					dropValue.DataTextField = "Name";
					dropValue.DataValueField = "Id";
					break;
				case "IssueStatusId" :
					dropValue.Visible = true;
					txtValue.Visible = false;
					dropValue.DataSource = Status.GetStatusByProjectId(ProjectId);
					dropValue.DataTextField = "Name";
					dropValue.DataValueField = "Id";
					break;
				case "IssueAssignedId" :
					dropValue.Visible = true;
					txtValue.Visible = false;
					dropValue.DataSource = ITUser.GetUsersByProjectId(ProjectId);
					dropValue.DataTextField = "DisplayName";
					dropValue.DataValueField = "Id";
					break;
				case "IssueOwnerId" :
					dropValue.Visible = true;
					txtValue.Visible = false;
					dropValue.DataSource = ITUser.GetUsersByProjectId(ProjectId);
					dropValue.DataTextField = "DisplayName";
					dropValue.DataValueField = "Id";
					break;
				case "IssueCreatorId" :
					dropValue.Visible = true;
					txtValue.Visible = false;
					dropValue.DataSource = ITUser.GetUsersByProjectId(ProjectId);
					dropValue.DataTextField = "DisplayName";
					dropValue.DataValueField = "Id";
					break;
				default :
					dropValue.Visible = false;
					txtValue.Visible = true;
					break;
			}

			dropValue.DataBind();
		}





		//*********************************************************************
		//
		// BooleanOperator Property
		//
		// This property represents the AND, OR, AND NOT, OR NOT values. Notice
		// that we check whether the posted value actually exists in the drop down list.
		// We do this to prevent SQL Injection Attacks.
		//
		//*********************************************************************


		public string BooleanOperator 
		{
			get 
			{
				if (dropBooleanOperator.Items.FindByValue(dropBooleanOperator.SelectedValue) == null)
					throw new Exception( "Invalid Boolean Operator" );

				return dropBooleanOperator.SelectedValue;
			}
		}


		//*********************************************************************
		//
		// FieldName Property
		//
		// This property represents the name of the field. Notice
		// that we check whether the posted value actually exists in the drop down list.
		// We do this to prevent SQL Injection Attacks.
		//
		//*********************************************************************

		public string FieldName 
		{
			get 
			{
				if (dropField.Items.FindByValue(dropField.SelectedValue) == null)
					throw new Exception( "Invalid Field Name" );

				return dropField.SelectedValue;
			}
		}



		//*********************************************************************
		//
		// ComparisonOperator Property
		//
		// This property represents the type of comparison. Notice
		// that we check whether the posted value actually exists in the drop down list.
		// We do this to prevent SQL Injection Attacks.
		//
		//*********************************************************************

		public string ComparisonOperator 
		{
			get 
			{
				if (dropComparisonOperator.Items.FindByValue(dropComparisonOperator.SelectedValue) == null)
					throw new Exception( "Invalid Comparison Operator" );

				return dropComparisonOperator.SelectedValue;
			}
		}




		public string FieldValue 
		{
			get 
			{

				switch (dropField.SelectedValue) 
				{
					case "IssueCategoryId":
					case "IssuePriorityId":
					case "IssueStatusId":
					case "IssueMilestoneId":
					case "IssueAssignedId":
					case "IssueOwnerId":
					case "IssueCreatorId":
						return dropValue.SelectedValue;
						break;
					default:
						return txtValue.Text;
						break;
				}

			}
		}



		public SqlDbType DataType 
		{
			get 
			{

				switch (dropField.SelectedValue) 
				{
					case "IssueId":
					case "IssueCategoryId":
					case "IssuePriorityId":
					case "IssueStatusId":
					case "IssueMilestoneId":
					case "IssueAssignedId":
					case "IssueOwnerId":
					case "IssueCreatorId":
						return SqlDbType.Int;
						break;
					case "DateCreated":
						return SqlDbType.DateTime;
						break;
					default:
						return SqlDbType.NVarChar;
						break;
				}

			}
		}



		//*********************************************************************
		//
		// QueryClause Property
		//
		// This property contains represents the SQL clause used when building the query.
		//
		//*********************************************************************

		public QueryClause QueryClause 
		{
			get 
			{
				if (dropField.SelectedValue == "0")
					return null;

				return new QueryClause(BooleanOperator, FieldName, ComparisonOperator, FieldValue, DataType);
			}
		}






		//*********************************************************************
		//
		// Clear Method
		//
		// This method clears the list of field values.
		//
		//*********************************************************************

		public void Clear() 
		{
			dropBooleanOperator.SelectedIndex = -1;
			dropField.SelectedIndex = -1;
			dropComparisonOperator.SelectedIndex = -1;
			dropValue.Items.Clear();
			txtValue.Text = String.Empty;
			dropValue.Visible = true;
			txtValue.Visible = false;
		}



	
	}
}
