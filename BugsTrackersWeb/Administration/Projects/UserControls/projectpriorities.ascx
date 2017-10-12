<%@ implements interface="BugsTrackers.UserInterfaceLayer.IEditProjectControl" %>
<%@ Register TagPrefix="IT" TagName="PickImage" Src="~/UserControls/PickImage.ascx" %>
<%@ Control Language="c#" Inherits="BugsTrackers.Administration.Projects.ProjectPriorities" CodeFile="ProjectPriorities.ascx.cs" %>
<fieldset>
	<!--legend>Project Priorities</legend-->
	<legend>Prioritatile pe proiect</legend>
	<table cellpadding="5" width="350" align="center">
		<tr>
			<!--td>
				When you create an issue, you assign the issue a priority such as High, Normal, 
				or Low. Enter the list of priorities below. Each priority can be associated 
				with an image.
			</td-->
			<td>
				Cand veti creea un nou task pe proiect, veti dori sa-i asignati o 
				prioritate.Formati o lista de prioritati dedesupt. Fiecarei prioritate ii va fi 
				asignata o imagine.
			</td>
		</tr>
		<tr>
			<td>
				<asp:Label id="lblError" ForeColor="red" EnableViewState="false" runat="Server" />
				<asp:CustomValidator Text="Trebuie adaugata cel putin o prioritate" Runat="server" id="CustomValidator1" />
			</td>
		</tr>
		<tr>
			<td>
				<asp:DataGrid id="grdPriorities" AutoGenerateColumns="false" BorderColor="White" BorderStyle="None"
					CellPadding="2" width="100%" Runat="Server">
					<headerstyle cssclass="gridHeader"></headerstyle>
					<Columns>
						<asp:TemplateColumn HeaderText="Prioritate">
							<headerstyle horizontalalign="Left" cssclass="gridHeader"></headerstyle>
							<itemstyle cssclass="gridFirstItem" width="80%"></itemstyle>
							<ItemTemplate>
								<asp:Label id="lblPriorityName" runat="Server" />
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Imaginea">
							<headerstyle horizontalalign="Center" cssclass="gridHeader"></headerstyle>
							<itemstyle horizontalalign="Center" cssclass="gridItem" width="10%"></itemstyle>
							<ItemTemplate>
								<asp:Image id="imgPriority" runat="Server" />
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn>
							<headerstyle horizontalalign="Right" cssclass="gridHeader"></headerstyle>
							<itemstyle horizontalalign="Right" cssclass="gridLastItem" width="10%"></itemstyle>
							<ItemTemplate>
								<asp:Button id="btnDelete" CommandName="delete" Text="Sterge" CssClass="standardText" runat="Server" />
							</ItemTemplate>
						</asp:TemplateColumn>
					</Columns>
				</asp:DataGrid>
			</td>
		</tr>
		<tr>
			<td>
				<asp:TextBox id="txtName" CssClass="standardText" MaxLength="50" runat="Server" />
				<asp:Button Text="Adauga" CssClass="standardText" CausesValidation="false" runat="server" id="Button1" onclick="AddPriority" />
			</td>
		</tr>
		<tr>
			<td>
				<it:PickImage id="lstImages" ImageDirectory="/Priority" runat="Server" />
			</td>
		</tr>
	</table>
</fieldset>
