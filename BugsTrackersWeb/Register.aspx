<%@ Page Language="c#" Inherits="BugsTrackers.Register" CodeFile="Register.aspx.cs" %>
<%@ Register TagPrefix="it" TagName="Header" Src="~/UserControls/Header.ascx" %>
<%@ Register TagPrefix="it" TagName="Footer" Src="~/UserControls/Footer.ascx" %>
<script runat="server">

</script>
<it:Header Title="BugTracker - Register" TabName="Taskuri" runat="server" id="Header1" />
<form runat="server">
	<table class="contentTable">
		<tr>
			<td class="contentCell">
				<table width="100%" cellspacing="0" cellpadding="0" border="0">
					<tr>
						<td>
							<h1>Register</h1>
						</td>
					</tr>
					<tr>
						<td>
							<asp:Label id="lblError" ForeColor="Red" EnableViewState="false" runat="Server" />
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
							<asp:TextBox id="txtUsername" CssClass="standardText" Width="180px" Runat="Server" />
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
							<asp:TextBox id="txtEmail" CssClass="standardText" Width="180px" Runat="Server" />
						</td>
					</tr>
					<tr>
						<td height="35">
							Password
							<asp:RequiredFieldValidator ControlToValidate="txtPassword" Text="(required)" Runat="server" id="RequiredFieldValidator3" />
						</td>
					</tr>
					<tr>
						<td height="35">
							<asp:TextBox id="txtPassword" TextMode="Password" CssClass="standardText" Width="180px" Runat="Server" />
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
							<asp:TextBox id="txtConfirmPassword" TextMode="Password" CssClass="standardText" Width="180px"
								Runat="Server" />
						</td>
					</tr>
					<tr>
						<td>
							<asp:Button Text="Save" CssClass="standardText" Runat="server" id="Button1" onclick="SaveUser" />
						</td>
					</tr>
				</table>
			</td>
		</tr>
	</table>
</form>
<it:Footer runat="server" id="Footer1" />
