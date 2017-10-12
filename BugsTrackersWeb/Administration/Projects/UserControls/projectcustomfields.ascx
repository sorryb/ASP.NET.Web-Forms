<%@ Control Language="c#" Inherits="BugsTrackers.Administration.Projects.ProjectCustomFields" CodeFile="ProjectCustomFields.ascx.cs" %>
<%@ implements interface="BugsTrackers.UserInterfaceLayer.IEditProjectControl" %>
<fieldset>
	<!--legend>Project Custom Fields</legend-->
	<legend>Campuri personalizate ale proiectului</legend>
	<table cellpadding="5" width="350" align="center">
		<tr>
			<!--td>
				You can add one or more custom fields to a project. You can assign a data type 
				to each custom field. In addtion, each custom field can be marked as either 
				required or optional.
			</td-->
			<td>
				Puteti adauga campuri noi la proiect.Puteti asigna un nou tip de data la 
				fiecare nou camp. In plus fiecare camp poate fi marcat ca optional sau 
				obligatoriu.
			</td>
		</tr>
		<tr>
			<td>
				<asp:Label id="lblError" ForeColor="red" EnableViewState="false" runat="Server" />
			</td>
		</tr>
		<tr>
			<td>
				<asp:DataGrid id="grdCustomFields" AutoGenerateColumns="false" BorderColor="White" BorderStyle="None"
					CellPadding="2" width="100%" Runat="Server">
					<headerstyle cssclass="gridHeader"></headerstyle>
					<Columns>
						<asp:TemplateColumn HeaderText="Field">
							<headerstyle horizontalalign="Left" cssclass="gridHeader"></headerstyle>
							<itemstyle cssclass="gridFirstItem" width="80%"></itemstyle>
							<ItemTemplate>
								<asp:Label id="lblName" runat="Server" />
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Type">
							<headerstyle horizontalalign="Center" cssclass="gridHeader"></headerstyle>
							<itemstyle horizontalalign="Center" cssclass="gridItem" width="10%"></itemstyle>
							<ItemTemplate>
								<asp:Label id="lblDataType" runat="Server" />
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Required">
							<headerstyle horizontalalign="Center" cssclass="gridHeader"></headerstyle>
							<itemstyle horizontalalign="Center" cssclass="gridItem" width="10%"></itemstyle>
							<ItemTemplate>
								<asp:Label id="lblRequired" runat="Server" />
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn>
							<headerstyle horizontalalign="Right" cssclass="gridHeader"></headerstyle>
							<itemstyle horizontalalign="Right" cssclass="gridLastItem" width="10%"></itemstyle>
							<ItemTemplate>
								<asp:Button id="btnDelete" CommandName="delete" Text="Delete" CssClass="standardText" runat="Server" />
							</ItemTemplate>
						</asp:TemplateColumn>
					</Columns>
				</asp:DataGrid>
			</td>
		</tr>
		<tr>
			<td>
				<asp:TextBox id="txtName" CssClass="standardText" MaxLength="50" runat="Server" />
				<asp:Button Text="Add Field" CssClass="standardText" runat="server" id="Button1" onclick="AddCustomField" />
				<br>
				<br>
				<asp:DropDownList id="dropDataType" runat="Server" />
				<asp:CheckBox id="chkRequired" Runat="Server" />
				Required
			</td>
		</tr>
	</table>
</fieldset>
