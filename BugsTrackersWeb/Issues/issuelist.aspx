<%@ Register TagPrefix="it" TagName="DisplayIssues" Src="~/UserControls/DisplayIssues.ascx" %>
<%@ Register TagPrefix="it" TagName="TextImage" Src="~/UserControls/TextImage.ascx" %>
<%@ Register TagPrefix="it" TagName="PickProject" Src="~/UserControls/PickProject.ascx" %>
<%@ Register TagPrefix="it" TagName="Footer" Src="~/UserControls/Footer.ascx" %>
<%@ Register TagPrefix="it" TagName="Header" Src="~/UserControls/Header.ascx" %>
<%@ Page Language="c#" Inherits="BugsTrackers.Issues.IssueList" CodeFile="IssueList.aspx.cs" %>
<it:Header Title="Task-uri" TabName="Taskuri" runat="server" id="Header1" />
<form runat="server">
	<table class="contentTable">
		<tr>
			<td class="contentCell">
				<table width="100%" cellspacing="10">
					<tr>
						<td>
							<h1><asp:Label id="lblProjectName" Runat="Server" />
							</h1>
							<asp:Button Text="Task nou" Class="standardText" Runat="server" id="btnAdd" onclick="AddIssue" />
						</td>
					</tr>
					<tr>
						<td align="right">
							Filtreaza:
							<asp:DropDownList id="dropView" CssClass="standardText" AutoPostBack="true" runat="Server" onselectedindexchanged="ViewSelectedIndexChanged">
								<asp:ListItem Text="Relevant tie" Value="Relevant" />
								<asp:ListItem Text="Assignat la tine" Value="Assigned" />
								<asp:ListItem Text="POsedat de tine" Value="Owned" />
								<asp:ListItem Text="Creat de tine" Value="Created" />
								<asp:ListItem Text="Toate task-urile" Value="All" />
							</asp:DropDownList>
							in
							<it:PickProject id="dropProjects" CssClass="standardText" AutoPostBack="true" Runat="Server" onselectedindexchanged="ViewSelectedIndexChanged" />
						</td>
					</tr>
					<tr>
						<td>
							<it:DisplayIssues id="ctlDisplayIssues" Runat="Server" onrebindcommand="IssuesRebind" />
						</td>
					</tr>
				</table>
			</td>
		</tr>
	</table>
</form>
<it:Footer runat="server" id="Footer1" />
