<%@ Register TagPrefix="it" TagName="Footer" Src="~/UserControls/Footer.ascx" %>
<%@ Register TagPrefix="it" TagName="Header" Src="~/UserControls/Header.ascx" %>
<%@ Page Language="c#" Inherits="BugsTrackers.Administration.Projects.AddProject" CodeFile="AddProject.aspx.cs" %>
<it:Header Title="New Project Wizard" CurrentTabName="Administrare" runat="server" id="Header1" />
<form runat="server">
	<table class="contentTable">
		<tr>
			<td class="contentCell">
				<h1><!--New Project Wizard-->Generator proiect nou (<asp:Literal id="lblStepNumber" runat="Server" />)</h1>
				<table width="400">
					<tr>
						<td>
							<asp:PlaceHolder id="plhWizardStep" runat="Server"   />
						</td>
					</tr>
					<tr>
						<td>
							<table width="100%">
								<tr>
									<td>
										<asp:Button Text="Revoca"  cssclass="standardButton" CausesValidation="false" runat="server" id="Button1" onclick="Cancel" />
									</td>
									<td align="right">
										<asp:Button id="btnBack" cssclass="standardButton" Text="Inapoi" CausesValidation="false" runat="Server" onclick="BackStep" />
										&nbsp;
										<asp:Button id="btnNext"  cssclass="standardButton" Text="Inainte" runat="Server" onclick="NextStep" />
									</td>
								</tr>
							</table>
						</td>
					</tr>
				</table>
			</td>
		</tr>
	</table>
</form>
<it:Footer runat="server" id="Footer1" />
