<%@ implements interface="BugsTrackers.UserInterfaceLayer.IEditProjectControl" %>
<%@ Register TagPrefix="it" TagName="PickSingleUser" Src="~/UserControls/PickSingleUser.ascx" %>
<%@ Control Language="c#" Inherits="BugsTrackers.Administration.Projects.ProjectDescription" CodeFile="ProjectDescription.ascx.cs" %>
<script runat="server">


</script>
<fieldset>
	<!--legend>Project Description</legend-->
	<legend>Descrierea proiectului</legend>
	<table cellpadding="5" width="350" align="center">
		<tr>
			<!--td>
				Enter a name and description for the project. Enter the name of the product 
				manager for this project (a project manager must be a member of either the 
				Administrator or Project Manager role).
			</td-->
			<td>
				Introduceti un nume si o descriere a proiectului.De asemeni introduceti numele 
				managerului de proiect. Atentie! El trebuie sa fie in rolul administrator sau
				project manager.
			</td>
		</tr>
		<tr>
			<td>
				<asp:Label id="lblError" ForeColor="red" EnableViewState="false" runat="Server" />
			</td>
		</tr>
		<tr>
			<td>
				 Name proiect:
				<asp:RequiredFieldValidator Text="(required)" ControlToValidate="txtName" Runat="server" id="RequiredFieldValidator1" />
			</td>
		</tr>
		<tr>
			<td>
				<asp:TextBox id="txtName" Columns="30" runat="Server" />
			</td>
		</tr>
		<tr>
			<td>
				Descriere proiect:
				<asp:RequiredFieldValidator Text="(required)" ControlToValidate="txtDescription" Runat="server" id="RequiredFieldValidator2" />
			</td>
		</tr>
		<tr>
			<td>
				<asp:TextBox id="txtDescription" TextMode="MultiLine" Columns="40" Rows="3" runat="Server" />
			</td>
		</tr>
		<tr>
			<td>
				 Manager Proiect:
			</td>
		</tr>
		<tr>
			<td>
				<it:PickSingleUser id="dropProjectManager" RoleName="Project Manager" DisplayDefault="true" Required="true"
					Runat="Server" />
			</td>
		</tr>
	</table>
</fieldset>
