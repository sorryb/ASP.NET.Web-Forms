<%@ Page Language="c#" Inherits="BugsTrackers.Administration.Projects.ProjectList" CodeFile="ProjectList.aspx.cs" %>
<%@ Register TagPrefix="it" TagName="AdminTabs" Src="~/Administration/UserControls/AdminTabs.ascx" %>
<%@ Register TagPrefix="it" TagName="Header" Src="~/UserControls/Header.ascx" %>
<%@ Register TagPrefix="it" TagName="Footer" Src="~/UserControls/Footer.ascx" %>
<it:Header Title="Project List" TabName="Administrare" runat="server" id="Header1" />
<form runat="server">
	<it:AdminTabs CurrentTabName="Proiecte" runat="server" id="AdminTabs1" />
	<table class="contentTable">
		<tr>
			<td class="contentCell">
				<table width="100%" cellspacing="20">
					<tr>
						<td>
							<asp:Button Text="Creere proiect" CssClass="standardButton" Runat="server" id="Button1" onclick="AddProject" />
							&nbsp;&nbsp;
							<asp:Button id="btnCloneProject" Visible="false" Text="Cloneaza proiect" CssClass="standardButton"
								Runat="Server" onclick="btnCloneProject_Click" />
						</td>
					</tr>
					<tr>
						<td>
							<asp:DataGrid id="ProjectsGrid" allowpaging="True" BorderColor="White" BorderStyle="None" Width="100%"
								CellPadding="2" AutoGenerateColumns="False" AllowSorting="True" runat="server">
								<headerstyle cssclass="gridHeader"></headerstyle>
								<columns>
									<asp:hyperlinkcolumn DataNavigateUrlField="Id" DataNavigateUrlFormatString="EditProject.aspx?id={0}"
										DataTextField="Name" SortExpression="Name" HeaderText="Nume proiect">
										<headerstyle horizontalalign="Left" cssclass="gridHeader"></headerstyle>
										<itemstyle cssclass="gridFirstItem"></itemstyle>
									</asp:hyperlinkcolumn>
									<asp:templatecolumn HeaderText="Descriere">
										<headerstyle horizontalalign="Left" cssclass="gridHeader"></headerstyle>
										<itemstyle cssclass="gridItem"></itemstyle>
										<itemtemplate>
                        &nbsp;<%# DataBinder.Eval(Container.DataItem, "Description")%>
                    </itemtemplate>
									</asp:templatecolumn>
									<asp:templatecolumn SortExpression="Manager" HeaderText="Manager">
										<headerstyle horizontalalign="Left" cssclass="gridHeader"></headerstyle>
										<itemstyle cssclass="gridItem"></itemstyle>
										<itemtemplate>
                        &nbsp;<%# DataBinder.Eval(Container.DataItem, "ManagerDisplayName")%>
                    </itemtemplate>
									</asp:templatecolumn>
									<asp:templatecolumn SortExpression="Creator" HeaderText="Creator">
										<headerstyle horizontalalign="Left" cssclass="gridHeader"></headerstyle>
										<itemstyle cssclass="gridItem"></itemstyle>
										<itemtemplate>
                        &nbsp;<%# DataBinder.Eval(Container.DataItem, "CreatorDisplayName" )%>
                    </itemtemplate>
									</asp:templatecolumn>
									<asp:templatecolumn SortExpression="Created" HeaderText="Date Crearii">
										<headerstyle horizontalalign="Left" cssclass="gridHeader"></headerstyle>
										<itemstyle horizontalalign="Left" cssclass="gridLastItem"></itemstyle>
										<itemtemplate>
                        &nbsp;<%# DataBinder.Eval(Container.DataItem, "DateCreated", "{0:d}" )%>
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
