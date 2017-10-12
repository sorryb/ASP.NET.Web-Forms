<%@ Page Language="c#" Inherits="BugsTrackers.Queries.QueryList" CodeFile="QueryList.aspx.cs" %>
<%@ Register TagPrefix="it" TagName="PickProject" Src="~/UserControls/PickProject.ascx" %>
<%@ Register TagPrefix="it" TagName="PickQuery" Src="~/UserControls/PickQuery.ascx" %>
<%@ Register TagPrefix="it" TagName="Footer" Src="~/UserControls/Footer.ascx" %>
<%@ Register TagPrefix="it" TagName="Header" Src="~/UserControls/Header.ascx" %>
<%@ Register TagPrefix="it" TagName="DisplayIssues" Src="~/UserControls/DisplayIssues.ascx" %>
<it:Header Title="BugTracker - Queries" TabName="Cautare" runat="server" id="Header1" />
<form runat="server">
	<table class="contentTableShort">
		<tr>
			<td class="contentCell">
				<table width="100%" cellspacing="10">
					<tr>
						<td align="right">
						</td>
					</tr>
				</table>
				<table Border="0" BorderColor="blue" CellPadding="4" CellSpacing="0">
					<tr>
						<td>
							Proiect:
						</td>
						<td>
							<it:PickProject id="dropProjects" CssClass="standardText" DisplayDefault="false" AutoPostBack="true"
								Runat="Server" OnSelectedIndexChanged = BindQuerie />
						</td>
						<TD></TD>
					</tr>
					<TR>
						<TD>Query:
						</TD>
						<TD>
							<it:PickQuery id="dropQueries" Runat="Server" DisplayDefault="true" CssClass="standardText"></it:PickQuery></TD>
						<TD>
							<asp:Button class="standardText" id="Button1" Runat="server" Text="Query nou" CssClass="standardButton" onclick="Button1_Click"></asp:Button></TD>
					</TR>
					<TR>
						<TD></TD>
						<TD>
							<asp:Button id="btnPerformQuery" Runat="Server" Text="Executa" CssClass="standardButton" onclick="btnPerformQuery_Click"></asp:Button>
							&nbsp;
							<asp:Button class="standardText" id="Button2" Runat="server" Text="Sterge" CssClass="standardButton" onclick="Button2_Click"></asp:Button></TD>
						<TD></TD>
					</TR>
					<TR>
						<TD></TD>
						<TD></TD>
						<TD></TD>
					</TR>
				</table>
			</td>
		</tr>
	</table>
	<br>
	<br>
	<table class="contentTableShort">
		<tr>
			<td class="contentCell">
				<h1>Rezultate:</h1>
				<asp:Label id="lblError" ForeColor="red" EnableViewState="false" Runat="Server" />
				<it:DisplayIssues id="ctlDisplayIssues" Runat="Server" onrebindcommand="IssuesRebind" />
			</td>
		</tr>
	</table>
</form>
<it:Footer runat="server" id="Footer1" />
