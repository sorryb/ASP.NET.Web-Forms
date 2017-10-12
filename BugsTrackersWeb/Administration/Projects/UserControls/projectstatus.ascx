<%@ implements interface="BugsTrackers.UserInterfaceLayer.IEditProjectControl" %>
<%@ Register TagPrefix="IT" TagName="PickImage" Src="~/UserControls/PickImage.ascx" %>
<%@ Control Language="c#" Inherits="BugsTrackers.Administration.Projects.ProjectStatus" CodeFile="ProjectStatus.ascx.cs" %>
<fieldset>
	<!--legend>Project Status</legend-->
	<legend>Starile proiectului</legend>
	<table cellpadding="5" width="350" align="center">
		<tr>
			<td>
				<asp:Label id="lblError" ForeColor="red" EnableViewState="false" runat="Server" />
				<asp:CustomValidator Text="Trebuie sa adaugati cel putin un statut" Runat="server" id="CustomValidator1" />
			</td>
		</tr>
		<tr>
			<td>
				<!--p>
				When you create an issue, you assign the issue a status such as In Progress or 
				Completed. Enter the list of status values below. Each status can be associated 
				with an image.
				<p-->
				<p>
				Cand veti crea un nou task, va trebui sa-i asignati o stare cum ar fi Deschis, 
				In lucru, Terminat. Mai jos aveti posibilitatea sa definiti o lista de 
				stari.Fiecareia ii va fi asociata o imagine.
				<p>
					<asp:DataGrid id="grdStatus" AutoGenerateColumns="false" BorderColor="White" BorderStyle="None"
						CellPadding="2" width="100%" Runat="Server">
						<headerstyle cssclass="gridHeader"></headerstyle>
						<Columns>
							<asp:TemplateColumn HeaderText="Stare">
								<headerstyle horizontalalign="Left" cssclass="gridHeader"></headerstyle>
								<itemstyle cssclass="gridFirstItem" width="80%"></itemstyle>
								<ItemTemplate>
									<asp:Label id="lblStatusName" runat="Server" />
								</ItemTemplate>
							</asp:TemplateColumn>
							<asp:TemplateColumn HeaderText="Imagine">
								<headerstyle horizontalalign="Center" cssclass="gridHeader"></headerstyle>
								<itemstyle horizontalalign="Center" cssclass="gridItem" width="10%"></itemstyle>
								<ItemTemplate>
									<asp:Image id="imgStatus" runat="Server" />
								</ItemTemplate>
							</asp:TemplateColumn>
							<asp:TemplateColumn>
								<headerstyle horizontalalign="Right" cssclass="gridHeader"></headerstyle>
								<itemstyle horizontalalign="Right" cssclass="gridLastItem" width="10%"></itemstyle>
								<ItemTemplate>
									<asp:Button id="btnDelete" CommandName="delete" Text="Sterge" CssClass="standardText" ToolTip="Delete Status"
										runat="Server" />
								</ItemTemplate>
							</asp:TemplateColumn>
						</Columns>
					</asp:DataGrid>
					<br>
					<asp:TextBox id="txtName" CssClass="standardText" Maxlength="50" runat="Server" />
					<asp:Button Text="Adauga" CssClass="standardText" CausesValidation="false" runat="server" id="Button1" onclick="AddStatus" /></p>
			</td>
		</tr>
		<tr>
			<td>
				<it:PickImage id="lstImages" ImageDirectory="/Status" runat="Server" />
			</td>
		</tr>
	</table>
</fieldset>
