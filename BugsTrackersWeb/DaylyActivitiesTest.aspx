<%@ Page language="c#" Inherits="BugsTrackers.Web.DaylyActivities" CodeFile="DaylyActivitiesTest.aspx.cs" %>
<%@ Register TagPrefix="it" TagName="Header" Src="~/UserControls/Header.ascx" %>
<%@ Register TagPrefix="it" TagName="Footer" Src="~/UserControls/Footer.ascx" %>
<%@ Register Assembly="BugTrackerWebControls" Namespace="BugTrackerWebControls.XControls"    TagPrefix="cc1" %>

<link href="" rel="stylesheet" type="text/css" />
<style>
 div.ui-datepicker {     font-size: 80.5%; } 
</style>
    <link type="text/css" href="styles/redmond/jquery-ui-1.8.16.custom.css" rel="stylesheet" />
<link href="favicon.ico" rel="SHORTCUT ICON" />
<it:Header id="Header1" title="Activitati per proiecte" runat="server" TabName="Activitati"></it:Header>
<script language="javascript" src="script.js" type="text/javascript"></script>
<form method="post" runat="server">
	<table cellSpacing="0" cellPadding="0" width="100%" border="0">
		<tr>
			<td width="8"><IMG height="8" src="images/spacer.gif" width="8"></td>
			<td vAlign="top" width="197">
				<!-- Left Panel -->
				<table cellSpacing="0" cellPadding="0" width="206" border="0">
					<tr>
						<td vAlign="top" width="225">
							<table class="tan-border" style="WIDTH: 215px; HEIGHT: 47px" cellSpacing="12" cellPadding="0"
								width="215" border="0">
								<tr>
									<td><span class="header-gray"><FONT color="#ffffcc">Sfarsit sap :</FONT></span></td>
									<td>
                                    <asp:textbox id="WeekEnding" runat="server" CssClass="standard-text" Width="68px"
											BorderStyle="None" BorderWidth="0px" AutoPostBack="true" Columns="12" ontextchanged="WeekEnding_TextChanged"></asp:textbox></td>
									<td><A href="javascript:OpenCalendar('WeekEnding', true)"><IMG src="images/icon-calendar.gif" align="absBottom" border="0"></A>
                                    <cc1:DatePicker ID="DatePickerControl" AutoPostBack="true"  DateLabelText="Data :" runat="server" OnDateChanged ="WeekEnding_TextChanged"  />
									
                                    </td>
								</tr>
							</table>
						</td>
					</tr>
					<tr>
						<td vAlign="top" height="11" width="225"><IMG height="1" src="images/spacer.gif" width="1"></td>
					</tr>
					<tr>
						<td vAlign="top" width="225">
							<table class="tan-border" style="WIDTH: 215px; HEIGHT: 332px" cellSpacing="12" cellPadding="0"
								width="215" border="0" bgColor="#ffffff">
								<tr vAlign="top">
									<td class="header-gray"><STRONG><FONT color="#ff0066" size="3">Adaugati activitatea</FONT></STRONG></td>
								</tr>
								<tr vAlign="top">
									<td><FONT color="#0000ff">Proiect</FONT>&nbsp;
										<asp:requiredfieldvalidator id="RequiredFieldValidator2" runat="server" Display="Dynamic" ErrorMessage="*" ControlToValidate="ProjectList"></asp:requiredfieldvalidator><br>
										<asp:dropdownlist id="ProjectList" runat="server"  CssClass="standardText" Width="175px" AutoPostBack="True" onselectedindexchanged="ProjectList_SelectedIndexChanged"></asp:dropdownlist></td>
								</tr>
								<tr vAlign="top">
									<td>
										<table cellSpacing="0" cellPadding="0" border="0">
                                            <tr>
                                                <td align="right">
                                                    <span style="color: #3333cc">Categorie</span></td>
                                                <td colspan="4">
                                                    <asp:dropdownlist id="CategoryList" runat="server" CssClass="standardText"></asp:dropdownlist>
                                                </td>
                                            </tr>
											<tr>
												<td align="right">
                                                    <span style="color: #3333cc">Zi sapt.</span></td>
												<td style="width: 10px"><asp:dropdownlist id="Days" runat="server"  CssClass="standardText"></asp:dropdownlist></td>
												<td></td>
												<td style="width: 6px"></td>
												<td style="width: 38px" rowspan="2"></td>
											</tr>
											<tr>
												<td align="right">
                                                    <span style="color: #3333cc">Nr.ore </span></td>
												<td align="left" colspan="2"><asp:textbox id="Hours" runat="server"  CssClass="standardText" Width="37px" Columns="5"></asp:textbox></td>
												<td style="width: 6px">&nbsp;<span style="color: #3333cc"></span></td>
											</tr>
											<tr>
												<td colSpan="5"><asp:requiredfieldvalidator id="Requiredfieldvalidator1" runat="server" ErrorMessage="Ora obligatorie!" ControlToValidate="Hours"
														display="Dynamic"></asp:requiredfieldvalidator><asp:comparevalidator id="CompareValidator1" runat="server" Display="Dynamic" ErrorMessage="Ora trebuie sa fie decimal!"
														ControlToValidate="Hours" Operator="DataTypeCheck" Type="Currency"></asp:comparevalidator>
													<asp:RangeValidator id="RangeValidator1" runat="server" ErrorMessage="Ora incorecta!" ControlToValidate="Hours"
														MaximumValue="24" MinimumValue="0" Type="Double"></asp:RangeValidator></td>
											</tr>
										</table>
									</td>
								</tr>
								<tr vAlign="top">
									<td height="68"><FONT color="#3366ff">Descriere</FONT><br>
										<asp:textbox id="Description" runat="server"  CssClass="standardText" Width="200px" Columns="30"
											TextMode="MultiLine" Rows="5" MaxLength="255"></asp:textbox></td>
								</tr>
								<tr vAlign="top">
									<td align="left"><asp:button id="AddEntry" runat="server"  CssClass="standardText" Text="Adauga" CausesValidation="False" onclick="AddEntry_Click"></asp:button><IMG height="8" src="images/spacer.gif" width="8">
										<asp:button id="Cancel" runat="server"  CssClass="standardText" Text="Renunta" CausesValidation="False" onclick="Cancel_Click"></asp:button></td>
								</tr>
							</table>
						</td>
					</tr>
					<tr>
						<td vAlign="top" height="11" width="225"><IMG height="1" src="images/spacer.gif" width="1"></td>
					</tr>
					<tr>
						<td vAlign="top" width="225">
							<table class="tan-border" style="WIDTH: 215px; HEIGHT: 81px" cellSpacing="0" cellPadding="0"
								width="214" border="0">
								<tr vAlign="top">
									<td height="12"><IMG height="1" src="images/spacer.gif" width="1"></td>
								</tr>
								<tr vAlign="top">
									<td class="header-gray"><IMG height="1" src="images/spacer.gif" width="11">&nbsp;<STRONG><FONT color="#ffffcc">Sumar 
												pe saptamana</FONT></STRONG>&nbsp;
									</td>
								</tr>
								<tr vAlign="top">
									<td height="12"><IMG height="1" src="images/spacer.gif" width="1"></td>
								</tr>
								<tr vAlign="top">
									<td><asp:image id="TimeGraph" Runat="server"></asp:image></td>
								</tr>
								<tr vAlign="top">
									<td height="12"><IMG height="1" src="images/spacer.gif" width="1"></td>
								</tr>
							</table>
						</td>
					</tr>
				</table>
				<!-- End Left Panel --></td>
			<td width="11"><IMG height="11" src="images/spacer.gif" width="11"></td>
			<td vAlign="top">
				<!-- Right Panel -->
				<table class="tan-border" height="570" cellSpacing="11" cellPadding="0" width="100%" border="0">
					<tr vAlign="top">
						<td height="15"><span class="header-gray"><FONT color="#ffffcc">Activitatile pentru 
									utilizatorul:</FONT></span>&nbsp;
							<asp:dropdownlist id="UserList" runat="server" CssClass="standard-text" Width="150px" AutoPostBack="True"
								DataValueField="UserID" DataTextField="Name" onselectedindexchanged="UserList_OnChange"></asp:dropdownlist></td>
					</tr>
					<tr>
						<td vAlign="top"><asp:datagrid id="TimeEntryGrid" runat="server" Width="100%" BorderStyle="None" CellPadding="2"
								AutoGenerateColumns="False" Font-Name="Verdana" FontSize="11px" AllowSorting="True" DataKeyField="EntryLogID"
								BorderColor="White" BackColor="#FFFFCC" Font-Names="Verdana">
								<HeaderStyle Font-Bold="True" CssClass="grid-header"></HeaderStyle>
								<Columns>
									<asp:TemplateColumn SortExpression="EntryDate" HeaderText="Ziua">
										<HeaderStyle HorizontalAlign="Center" Width="80px" CssClass="grid-header" VerticalAlign="Middle"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" CssClass="grid-first-item"></ItemStyle>
										<ItemTemplate>
											<asp:label ID="EntryDay" Text='&nbsp;<%# Convert.ToDateTime(DataBinder.Eval(Container, "DataItem.EntryDate")).ToString("dddd",cultureRo) %>' Runat="server" Width='40px' />
										</ItemTemplate>
										<EditItemTemplate>
											<asp:dropdownlist Width="48px" ID="EntryDays" CssClass="Standard-text" DataSource='<%# _dayListTable %>' DataTextField = "Day" DataValueField = "Date" Runat="server">
											</asp:dropdownlist>
										</EditItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="ProjectName" HeaderText="Proiect">
										<HeaderStyle HorizontalAlign="Center" Width="40px" CssClass="grid-header" VerticalAlign="Middle"></HeaderStyle>
										<ItemStyle Width="108px" CssClass="grid-item"></ItemStyle>
										<ItemTemplate>
											<asp:label ID="EntryProject" Text='&nbsp;<%# DataBinder.Eval(Container, "DataItem.ProjectName") %>' Runat="server" />
											<asp:label ID="EntryProjectID" Text='<%# DataBinder.Eval(Container, "DataItem.ProjectID") %>' Runat="server" Visible="False" />
										</ItemTemplate>
										<EditItemTemplate>
											<asp:dropdownlist Width="100px" ID="EntryProjects" AutoPostBack="True" CssClass="Standard-text" DataSource='<%# ListUserProjects() %>' DataTextField="Name" DataValueField="ProjectID" Runat="server" OnSelectedIndexChanged="UserProjects_OnChange" />
										</EditItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="CategoryName" HeaderText="Categoria">
										<HeaderStyle HorizontalAlign="Center" Width="40px" CssClass="grid-header" VerticalAlign="Middle"></HeaderStyle>
										<ItemStyle Width="88px" CssClass="grid-item"></ItemStyle>
										<ItemTemplate>
											<asp:label ID="EntryCategory" Text='&nbsp;<%# DataBinder.Eval(Container, "DataItem.CategoryName") %>' Runat="server" />
										</ItemTemplate>
										<EditItemTemplate>
											<asp:dropdownlist Width="80px" ID="EntryCategories" CssClass="Standard-text" DataSource='<%# ListGridCategories(_userInput.ProjectID) %>' DataTextField="Abbreviation" DataValueField="CategoryID" Runat="server">
											</asp:dropdownlist>
										</EditItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="Duration" HeaderText="Nr.ore">
										<HeaderStyle HorizontalAlign="Center" Width="40px" CssClass="grid-header" VerticalAlign="Middle"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right" Width="50px" CssClass="grid-item"></ItemStyle>
										<ItemTemplate>
											<asp:label ID="EntryDuration" Text='&nbsp;<%# DataBinder.Eval(Container.DataItem, "Duration", "{0:f}") %>' Runat="server" />
										</ItemTemplate>
										<EditItemTemplate>
											<asp:textbox Width="40px" AutoPostBack=false CssClass="Standard-text" Runat="server" ID="EntryHours" Text='<%# _userInput.Duration %>' />
											<asp:requiredfieldvalidator id="RequiredFieldValidatorGridHours" runat="server" ErrorMessage="Hours is required."
												ControlToValidate="EntryHours" Display="Dynamic"></asp:requiredfieldvalidator>
											<asp:comparevalidator id="CompareValidatorGridHours" runat="server" ErrorMessage="Hours must be numeric value."
												ControlToValidate="EntryHours" Display="Dynamic" Type="Currency" Operator="DataTypeCheck"></asp:comparevalidator>
										</EditItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="Description" HeaderText="Descriere">
										<HeaderStyle HorizontalAlign="Left" CssClass="grid-header" VerticalAlign="Middle"></HeaderStyle>
										<ItemStyle CssClass="grid-item"></ItemStyle>
										<ItemTemplate>
											<asp:label ID="GridDescription" Text='&nbsp;<%# DataBinder.Eval(Container, "DataItem.Description") %>' Runat="server" />
										</ItemTemplate>
										<EditItemTemplate>
											<asp:textbox runat="server" CssClass="Standard-text" ID="EntryDescription" Text='<%# _userInput.Description %>' Width="250" MaxLength="255">
											</asp:textbox>
										</EditItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Editeaza">
										<HeaderStyle HorizontalAlign="Center" Width="40px" CssClass="grid-header" VerticalAlign="Middle"></HeaderStyle>
										<ItemStyle CssClass="grid-edit-column"></ItemStyle>
										<ItemTemplate>
											<asp:imagebutton runat="server" ImageUrl="images/icon-pencil.gif" AlternateText="Edit" CommandName="Edit"
												CausesValidation="false" ID="Imagebutton1" NAME="Imagebutton1"></asp:imagebutton>
											<img src="images/spacer.gif" width="3">
											<asp:imagebutton Runat="server" ImageUrl="images/icon-delete.gif" AlternateText="Delete" CommandName="Delete"
												CausesValidation="False" ID="Imagebutton2" NAME="Imagebutton2"></asp:imagebutton>
										</ItemTemplate>
										<EditItemTemplate>
											<asp:imagebutton runat="server" ImageUrl="images/icon-floppy.gif" AlternateText="Update" CommandName="Update"
												CausesValidation="False" ID="Imagebutton3" NAME="Imagebutton3"></asp:imagebutton>
											<img src="images/spacer.gif" width="3">
											<asp:imagebutton runat="server" ImageUrl="images/icon-pencil-x.gif" AlternateText="Cancel" CommandName="Cancel"
												CausesValidation="False" ID="Imagebutton4" NAME="Imagebutton4"></asp:imagebutton>
										</EditItemTemplate>
									</asp:TemplateColumn>
								</Columns>
							</asp:datagrid></td>
					</tr>
				</table>
				<!-- End Right Panel --></td>
			<td width="11"><IMG height="11" src="images/spacer.gif" width="11"></td>
		</tr>
		<tr>
			<td vAlign="top" colSpan="5" height="15"><IMG height="15" src="images/spacer.gif" width="15"></td>
		</tr>
	</table>
	<it:Footer id="Footer1" runat="server"></it:Footer>
</form>
