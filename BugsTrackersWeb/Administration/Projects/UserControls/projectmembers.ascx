<%@ implements interface="BugsTrackers.UserInterfaceLayer.IEditProjectControl" %>
<%@ Control Language="c#" Inherits="BugsTrackers.Administration.Projects.ProjectMembers" CodeFile="ProjectMembers.ascx.cs" %>
<script runat="server">

</script>
<fieldset>
	<!--legend>Project Members</legend-->
	<legend>Membrii proiectului</legend>
	<table CellPadding="5" width="350" align="center">
		<tr>
			<!--td>
				Add users to the project by clicking the Add button. Only project members can 
				add issues to a project.
			</td-->
			<td>
				Adaugati utilizatori la proiect .Numai membrii de proiect pot adauga noi task-uri la 
				proiect.
			</td>
		</tr>
		<tr>
			<td>
				<table>
					<tr>
						<!--td>All Users</td-->
						<td>Toti utilizatorii</td>
						<td>&nbsp;</td>
						<!--td>Selected Users</td-->
						<td>Selectati utilizatorii</td>
					</tr>
					<tr>
						<td>
							<asp:ListBox id="lstAllUsers" Runat="Server" Width="150" />
						</td>
						<td>
							<asp:Button Text="->" style="FONT:9pt Courier" Runat="server" id="Button1" onclick="AddUser" />
							<br>
							<asp:Button Text="<-" style="FONT:9pt Courier" Runat="server" id="Button2" onclick="RemoveUser" />
						</td>
						<td>
							<asp:ListBox id="lstSelectedUsers" Runat="Server" Width="150" />
						</td>
					</tr>
				</table>
			</td>
		</tr>
	</table>
</fieldset>
