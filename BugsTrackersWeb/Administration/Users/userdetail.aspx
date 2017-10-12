<%@ Page Language="c#" Inherits="BugsTrackers.Administration.Users.UserDetail" CodeFile="UserDetail.aspx.cs" %>
<%@ Register TagPrefix="it" TagName="AdminTabs" Src="~/Administration/UserControls/AdminTabs.ascx" %>
<%@ Register TagPrefix="it" TagName="Header" Src="~/UserControls/Header.ascx" %>
<%@ Register TagPrefix="it" TagName="Footer" Src="~/UserControls/Footer.ascx" %>
<it:Header Title="Detaliu utilizator" TabName="Administrare" runat="server" id="Header1" />
<form runat="server">
	<table class="contentTable">
		<tr>
			<td class="contentCell">
				<table width="100%" cellspacing="0" cellpadding="0" border="0">
					<tr>
						<td>
							<!--h1>User Detail</h1--><h1>Detaliu utilizator</h1>
						</td>
					</tr>
					<tr>
						<td>
							<asp:Label id="lblError" ForeColor="Red" runat="Server" />
						</td>
					</tr>
					<tr>
						<td height="35">
							Username
							<asp:RequiredFieldValidator ControlToValidate="txtUsername" Text="(required)" Runat="server" id="RequiredFieldValidator1" />
						</td>
					</tr>
					<tr>
						<td height="35">
							<asp:TextBox id="txtUsername" CssClass="standardText" Columns="40" Runat="Server" />
						</td>
					</tr>
					<tr>
						<td height="35">
							Role
						</td>
					</tr>
					<tr>
						<td height="35">
							<asp:DropDownList id="dropRoles" CssClass="standardText" Runat="Server" />
						</td>
					</tr>
					<tr>
						<td height="35">
							Email
							<asp:RequiredFieldValidator ControlToValidate="txtEmail" Text="(required)" Runat="server" id="RequiredFieldValidator2" />
						</td>
					</tr>
					<tr>
						<td height="35">
							<asp:TextBox id="txtEmail" CssClass="standardText" Columns="40" Runat="Server" />
						</td>
					</tr>
				</table>
				<table id="formsPassword" runat="Server">
					<tr>
						<td height="35">
							Password
						</td>
					</tr>
					<tr>
						<td height="35">
							<asp:TextBox id="txtPassword" TextMode="Password" CssClass="standardText" Runat="Server" />
						</td>
					</tr>
					<tr>
						<td height="35">
							Confirm Password
							<asp:CompareValidator ControlToValidate="txtConfirmPassword" ControlToCompare="txtPassword" Text="(passwords must match)"
								Runat="server" id="CompareValidator1" />
						</td>
					</tr>
					<tr>
						<td height="35">
							<asp:TextBox id="txtConfirmPassword" TextMode="Password" CssClass="standardText" Runat="Server" />
						</td>
					</tr>
				</table>
				<table>
					<tr>
						<td>
							<asp:Button Text="Save" CssClass="standardButton" Runat="server" id="Button1" onclick="SaveUser" />
							&nbsp;&nbsp;
							<asp:Button Text="Cancel" CssClass="standardButton" CausesValidation="false" Runat="server" id="Button2" onclick="CancelEdit" />
						</td>
					</tr>
				</table>
			</td>
		</tr>
	</table>
</form>
<it:Footer runat="server" id="Footer1" />
