<%@ Page Language="c#" smartnavigation="true" Inherits="BugsTrackers.Administration.Projects.EditProject" CodeFile="EditProject.aspx.cs" %>
<%@ Register TagPrefix="it" TagName="Milestone" Src="~/Administration/Projects/UserControls/ProjectMilestones.ascx" %>
<%@ Register TagPrefix="it" TagName="Priority" Src="~/Administration/Projects/UserControls/ProjectPriorities.ascx" %>
<%@ Register TagPrefix="it" TagName="ProjectMembers" Src="~/Administration/Projects/UserControls/ProjectMembers.ascx" %>
<%@ Register TagPrefix="it" TagName="ProjectStatus" Src="~/Administration/Projects/UserControls/ProjectStatus.ascx" %>
<%@ Register TagPrefix="it" TagName="Categories" Src="~/Administration/Projects/UserControls/ProjectCategories.ascx" %>
<%@ Register TagPrefix="it" TagName="Footer" Src="~/UserControls/Footer.ascx" %>
<%@ Register TagPrefix="it" TagName="ProjectDescription" Src="~/Administration/Projects/UserControls/ProjectDescription.ascx" %>
<%@ Register TagPrefix="it" TagName="Header" Src="~/UserControls/Header.ascx" %>
<%@ Register TagPrefix="it" TagName="ProjectCustomFields" Src="~/Administration/Projects/UserControls/ProjectCustomFields.ascx" %>
<it:Header Title="Edit Project" TabName="Administrare" runat="server" id="Header1" />
<form runat="server">
	<table class="contentTable">
		<tr>
			<td class="contentCell">
				<table cellpadding="10">
					<tr>
						<td><!--h1>Project Configuration</h1--><h1>Administrare proiect</h1>
						</td>
						<td align="right">
							<asp:button id="SaveButton" runat="server" width="53px" CssClass="standardButton" Text="Salveaza" onclick="SaveButton_Click"></asp:button>
							&nbsp;&nbsp;
							<asp:button id="CancelButton" runat="server" width="53" causesvalidation="False" CssClass="standardButton"
								Text="Revoca" onclick="CancelButton_Click"></asp:button>
							&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
							<asp:button id="DeleteButton" runat="server" Text="Sterge" CssClass="standardButton" causesvalidation="False" onclick="DeleteButton_Click"></asp:button>
						</td>
					</tr>
					<tr>
						<td colspan="2">
							<hr>
							Editeaza proiect.
						</td>
					</tr>
					<tr style="VERTICAL-ALIGN:top">
						<td>
							<it:ProjectDescription id="ctlProjectDescription" Runat="Server" />
							<br>
							<it:ProjectMembers id="ctlProjectMembers" Runat="Server" />
							<br>
							<it:Categories id="ctlCategories" Runat="Server" />
							<br>
							<it:ProjectCustomFields id="ctlCustomFields" Runat="Server" />
						</td>
						<td>
							<it:ProjectStatus id="ctlProjectStatus" Runat="Server" />
							<br>
							<it:Priority id="ctlPriority" Runat="Server" />
							<br>
							<it:Milestone id="ctlMilestone" Runat="Server" />
						</td>
					</tr>
				</table>
			</td>
		</tr>
	</table>
</form>
<it:Footer runat="server" id="Footer1" />
