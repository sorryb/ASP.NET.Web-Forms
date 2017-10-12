<%@ Page Language="c#" Inherits="BugsTrackers.Administration.Projects.CloneProject" CodeFile="CloneProject.aspx.cs" %>
<%@ Register TagPrefix="it" TagName="AdminTabs" Src="~/Administration/UserControls/AdminTabs.ascx" %>
<%@ Register TagPrefix="it" TagName="Header" Src="~/UserControls/Header.ascx" %>
<%@ Register TagPrefix="it" TagName="Footer" Src="~/UserControls/Footer.ascx" %>
<%@ Register TagPrefix="it" TagName="PickProject" Src="~/UserControls/PickProject.ascx" %>
<it:Header Title="Project List" TabName="Proiecte" runat="server" id="Header1" />
<form runat="server">
	<it:AdminTabs CurrentTabName="Projects" runat="server" id="AdminTabs1" />
	<table class="contentTable">
		<tr>
			<td class="contentCell">
				<!--p style="WIDTH:500px">
					When you clone a project, you make a copy of the project. For example, you make 
					a copy of all of the project's categories, milestones, and custom fields. 
					However, when you clone a project, issues are not copied to the new project.
				</p-->
				<p style="WIDTH:500px">
					Cand se cloneaza un proiect se face o copie a unuia existent.Astfel se vor copia toate categoriile,campurile preferentiale
					si alte item-uri ale proiectului existent.Totusi nu se copiaza si task-urile proiectului.
				</p>				
				<asp:Label id="lblError" ForeColor="red" Font-Bold="true" EnableViewState="false" Runat="Server" />
				<table cellpadding="5">
					<tr>
						<td align="right"><!--b>Existing Project Name:</b--><b>Proiect existent:</b></td>
						<td><it:PickProject id="dropProjects" CssClass="standardText" DisplayDefault="true" Required="true"
								Runat="Server" /></td>
					</tr>
					<tr>
						<td align="right"><!--b>New Project Name:</b--><b>Numele noului proiect:</b></td>
						<td>
							<asp:TextBox id="txtNewProjectName" Runat="Server" />
							<asp:RequiredFieldValidator ControlToValidate="txtNewProjectName" Text="(required)" Runat="server" id="RequiredFieldValidator1" />
						</td>
					</tr>
				</table>
				<p>
					<asp:Button id="btnClone" Text="Cloneaza proiect" Class="standardText" Runat="Server" onclick="btnClone_Click" />
					&nbsp;&nbsp;
					<asp:Button id="btnCancel" Text="Revoca" Class="standardText" CausesValidation="false"
						Runat="Server" onclick="btnCancel_Click" />
				</p>
			</td>
		</tr>
	</table>
</form>
<it:Footer runat="server" id="Footer1" />
