<%@ Register TagPrefix="it" TagName="Footer" Src="~/UserControls/Footer.ascx" %>
<%@ Register TagPrefix="it" TagName="Header" Src="~/UserControls/Header.ascx" %>
<%@ Register TagPrefix="it" TagName="AdminTabs" Src="~/Administration/UserControls/AdminTabs.ascx" %>
<%@ Page Language="c#" Inherits="BugsTrackers.Administration.Users.UserList" CodeFile="UserList.aspx.cs" %>
<it:Header Title="Lista utilizatori" TabName="Administrare" runat="server" id="Header1" />
<form runat="server">
	<it:AdminTabs CurrentTabName="Utilizatori" runat="server" id="AdminTabs1" />
	<table class="contentTable">
		<tr>
			<td class="contentCell">
				<table width="100%" cellspacing="20">
					<tr>
						<td>
							<asp:Button Text="Utilizator nou" Class="standardText" Runat="server" id="Button1" onclick="AddUser" />
						</td>
					</tr>
					<tr>
						<td>
							<asp:DataGrid id="UsersGrid" allowpaging="True" BorderColor="White" BorderStyle="None" Width="100%"
								CellPadding="2" AutoGenerateColumns="False" AllowSorting="True" runat="server">
								<headerstyle cssclass="gridHeader"></headerstyle>
								<columns>
									<asp:hyperlinkcolumn DataNavigateUrlField="Id" DataNavigateUrlFormatString="UserDetail.aspx?id={0}" DataTextField="Username"
										SortExpression="Username" HeaderText="Username">
										<headerstyle horizontalalign="Left" cssclass="gridHeader"></headerstyle>
										<itemstyle cssclass="gridFirstItem"></itemstyle>
									</asp:hyperlinkcolumn>
									<asp:templatecolumn SortExpression="DisplayName" HeaderText="Nume">
										<headerstyle horizontalalign="Left" cssclass="gridHeader"></headerstyle>
										<itemstyle horizontalalign="Left" cssclass="gridItem"></itemstyle>
										<itemtemplate>
                        &nbsp;
<asp:Label id="lblDisplayName" Runat="Server" />
                    </itemtemplate>
									</asp:templatecolumn>
									<asp:templatecolumn SortExpression="RoleName" HeaderText="Rol">
										<headerstyle horizontalalign="Right" cssclass="gridHeader"></headerstyle>
										<itemstyle horizontalalign="Right" cssclass="gridLastItem"></itemstyle>
										<itemtemplate>
                        &nbsp;
<asp:Label id="lblRoleName" Runat="Server" />
                    </itemtemplate>
									</asp:templatecolumn>
								</columns>
								<pagerstyle horizontalalign="Center"></pagerstyle>
							</asp:DataGrid>
						</td>
					</tr>
				</table>
			</td>
		</tr>
	</table>
</form>
<it:Footer runat="server" id="Footer1" />
