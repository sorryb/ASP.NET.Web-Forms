<%@ implements interface="BugsTrackers.UserInterfaceLayer.IEditProjectControl" %>
<%@ Control Language="c#" Inherits="BugsTrackers.Administration.Projects.NewProjectIntro" CodeFile="NewProjectIntro.ascx.cs" %>
<script runat="server">
</script>
<fieldset>
	<legend>Introduction</legend>
	<table cellpadding="10" align="center">
		<tr>
			<td>
				<!--The New Project Wizard enables you to create a new project for managing issues. 
				This Wizard will guide you through the steps for creating the new project.
				-->
				Prin acest generator de proiect veti fi abilitat sa creati
				un nou proiect.Urmati pasii urmatori pentru a configura un nou proiect.
				<br>
				<br>
				<asp:CheckBox id="chkSkip" Text="Renunta data viitoare" Runat="Server" />
			</td>
		</tr>
	</table>
</fieldset>
