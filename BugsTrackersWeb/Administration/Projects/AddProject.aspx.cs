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
using BugsTrackers.UserInterfaceLayer;

namespace BugsTrackers.Administration.Projects
{
	/// <summary>
	/// Summary description for AddProject.
	/// </summary>
	public partial class AddProject : System.Web.UI.Page
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
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    

		}
		#endregion

	
		//*********************************************************************
		//
		// AddProject.aspx
		//
		// This page displayes the wizard for adding a new project. The wizard
		// dynamically loads user controls which represent each step in the
		// wizard. This wizard does not use Session state.
		//
		//*********************************************************************


		ArrayList WizardSteps = new ArrayList();
		Control ctlWizardStep;

		int ProjectId 
		{
			get 
			{
				if (ViewState["ProjectId"] == null)
					return -1;
				else
					return (int)ViewState["ProjectId"];
			}

			set { ViewState["ProjectId"] = value; }
		}


		int StepIndex 
		{
			get 
			{
				if (ViewState["StepIndex"] == null)
					return 0;
				else
					return (int)ViewState["StepIndex"];
			}

			set { ViewState["StepIndex"] = value; }
		}



		protected void Page_Load(object sender, System.EventArgs e) 
		{
			if (Request.Cookies[Globals.SkipProjectIntro] == null)
				WizardSteps.Add("UserControls/NewProjectIntro.ascx");

			WizardSteps.Add("UserControls/ProjectDescription.ascx");
			WizardSteps.Add("UserControls/ProjectCategories.ascx");
			WizardSteps.Add("UserControls/ProjectStatus.ascx");
			WizardSteps.Add("UserControls/ProjectPriorities.ascx");
			WizardSteps.Add("UserControls/ProjectMileStones.ascx");
			WizardSteps.Add("UserControls/ProjectCustomFields.ascx");
			WizardSteps.Add("UserControls/ProjectMembers.ascx");
			WizardSteps.Add("UserControls/NewProjectSummary.ascx");

			LoadWizardStep();
		}


		protected void Page_PreRender(object sender, System.EventArgs e) 
		{

			if (StepIndex == 0)
				btnBack.Visible = false;
			else
				btnBack.Visible = true;

			if (StepIndex == (WizardSteps.Count - 1))
				btnNext.Text = "Finish";
			else
				btnNext.Text = "Next";


		}


		void LoadWizardStep() 
		{
			ctlWizardStep = Page.LoadControl((string)WizardSteps[StepIndex]);
			ctlWizardStep.ID = "ctlWizardStep";
			((IEditProjectControl)ctlWizardStep).ProjectId = ProjectId;
			plhWizardStep.Controls.Clear();
			plhWizardStep.Controls.Add(ctlWizardStep);
			((IEditProjectControl)ctlWizardStep).Initialize();
			lblStepNumber.Text = String.Format("Step {0} of {1}", StepIndex + 1, WizardSteps.Count );
		}


		protected void Cancel(Object s, EventArgs e) 
		{
			Response.Redirect( "ProjectList.aspx" );
		}

		protected void BackStep(Object s, EventArgs e) 
		{
			StepIndex --;
			LoadWizardStep();
		}


		protected void NextStep(Object s, EventArgs e) 
		{
			if ( ((IEditProjectControl)ctlWizardStep).Update() ) 
			{
				ProjectId = ((IEditProjectControl)ctlWizardStep).ProjectId;
				StepIndex ++;
				if (StepIndex == WizardSteps.Count)
					Response.Redirect( "ProjectList.aspx" );
				else
					LoadWizardStep();
			}
		}


	
	
	}
}
