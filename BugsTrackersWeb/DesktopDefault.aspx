<%@ Page Language="c#" Inherits="BugsTrackers.DesktopDefault" CodeFile="DesktopDefault.aspx.cs" %>
<%@ Register TagPrefix="it" TagName="Header" Src="~/UserControls/Header.ascx" %>
<%@ Register TagPrefix="it" TagName="Footer" Src="~/UserControls/Footer.ascx" %>
	<script src="Scripts/jquery-1.6.2.js"     type="text/javascript"></script>
        <script type="text/javascript">
            $(function () { $("input[type='text']:enabled:first").focus(); });    
        </script>
<P>
	<it:Header Title="Bugs Trackers - Login" runat="server" id="Header1" />
</P>
<form runat="server">
	<table class="contentTable" align="center" height="100">

    		<tr>
			<td classS="contentCell" valign="middle" align="center" height="100%">
				<H1>Conectare la Bug Tracker</H1>
				<table cellpadding="3" cellspacing="1" bgColor="#3366ff">
					<tr>
						<td bgColor="#ffffff">
							<P align="center">
								<asp:Label id="lblError" ForeColor="red" EnableViewState="false" Runat="Server" /></P>
						</td>
					</tr>
					<tr>
						<td bgColor="#ffffff">
							Utilizator:
							<asp:RequiredFieldValidator ControlToValidate="txtUsername" Text="*" Runat="server" id="RequiredFieldValidator1" />
						</td>
					</tr>
					<tr>
						<td bgColor="#ffffff">
							<P align="center">
								<asp:TextBox CssClass="standardText" id="txtUsername" width="185px" runat="Server" /></P>
						</td>
					</tr>
					<tr>
						<td bgColor="#ffffff">
							Parola:
							<asp:RequiredFieldValidator ControlToValidate="txtPassword" Text="*" Runat="server" id="RequiredFieldValidator2" />
						</td>
					</tr>
					<TR>
						<td bgColor="#ffffff">
							<P align="center">
								<asp:TextBox CssClass="standardText" id="txtPassword" width="185px" TextMode="Password" runat="Server" /></P>
						</td>
					</TR>
					<tr>
						<td bgColor="#ffffff">
							<asp:Checkbox id="chkRemember" Text="Reaminteste utilizator" Runat="Server" />
						</td>
					</tr>
					<tr>
						<td bgColor="#ffffff">
							<P align="center">
								<asp:Button Text="Conectare" runat="server" CssClass="standardButton" id="Button1" onclick="Login" />
								&nbsp;
								<asp:Button Text="Inregistrare" CausesValidation="false" runat="server" id="btnRegister" Visible="False" onclick="btnRegister_Click" /></P>
						</td>
					</tr>
				</table>
			</td>
		</tr>
	</table>
</form>
<it:Footer runat="server" id="Footer1" />
