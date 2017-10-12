<%@ Register TagPrefix="it" TagName="Footer" Src="~/UserControls/Footer.ascx" %>
<%@ Register TagPrefix="it" TagName="Header" Src="~/UserControls/Header.ascx" %>
<%@ Page Language="c#" Inherits="BugsTrackers.NoProjects" CodeFile="NoProjects.aspx.cs" %>
<script runat="Server">


</script>
<it:Header Title="BugTracker - No Projects" TabName="Taskuri" runat="server" id="Header1" />
<form runat="server">
	<table class="contentTable">
		<tr>
			<td class="contentCell">
				<table align="center" width="400">
					<tr>
						<td>
							<!--P>You are not a member of any projects. If you are a member of the Administrator 
								role, you can click the Administration tab and create a new project.
							</P-->
							<P>Nu este membru la nici un proiect. Daca aveti rol de administrare, creati un 
								proiect nou la care sa adaugati bug-uri.</P>
						</td>
					</tr>
				</table>
			</td>
		</tr>
	</table>
</form>
<it:Footer runat="server" id="Footer1" />
