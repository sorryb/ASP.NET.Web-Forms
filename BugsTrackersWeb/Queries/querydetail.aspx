<%@ Reference Control="~/usercontrols/pickqueryfield.ascx" %>
<%@ Page Language="c#" Inherits="BugsTrackers.QueryDetail" CodeFile="QueryDetail.aspx.cs" %>
<%@ Register TagPrefix="it" TagName="PickProject" Src="~/UserControls/PickProject.ascx" %>
<%@ Register TagPrefix="it" TagName="Footer" Src="~/UserControls/Footer.ascx" %>
<%@ Register TagPrefix="it" TagName="Header" Src="~/UserControls/Header.ascx" %>
<%@ Register TagPrefix="it" TagName="PickQueryField" Src="~/UserControls/PickQueryField.ascx" %>
<%@ Register TagPrefix="it" TagName="DisplayIssues" Src="~/UserControls/DisplayIssues.ascx" %>
<it:Header Title="Interogari" TabName="Cautare" runat="server" id="Header1" />
<form runat="server">
	<table class="contentTableShort">
		<tr>
			<td class="contentCell">
				<table Border="1" BorderColor="black" CellPadding="4" CellSpacing="0">
					<tr>
						<td>
							Proiect:
						</td>
						<td>
							<it:PickProject id="dropProjects" CssClass="standardText" DisplayDefault="false" AutoPostBack="true"
								Runat="Server" onselectedindexchanged="ProjectSelectedIndexChanged" />
						</td>
					</tr>
				</table>
				<br>
				<br>
				<table Border="1" BorderColor="black" CellPadding="4" CellSpacing="0">
					<asp:PlaceHolder id="plhClauses" Runat="Server" />
				</table>
				<br>
				<asp:Button id="btnAddClause" Text="Adauga" CssClass="standardText" Runat="Server" onclick="btnAddClauseClick" />
				<asp:Button id="btnRemoveClause" Text="Sterge " CssClass="standardText" Runat="Server" onclick="btnRemoveClauseClick" />
				<hr>
				<asp:TextBox id="txtQueryName" Runat="Server" />
				&nbsp;
				<asp:Button id="btnSaveQuery" Text="Salveaza" CssClass="standardText" Runat="Server" onclick="btnSaveQueryClick" />
				&nbsp;
				<asp:Button id="btnPerformQuery" Text="Executa" CssClass="standardText" Runat="Server" onclick="btnPerformQueryClick" />
				<asp:Label id="lblSaveError" ForeColor="Red" EnableViewState="false" Runat="Server" />
				<asp:Label id="lblResult" Runat="Server" />
			</td>
		</tr>
	</table>
	<br>
	<br>
	<table class="contentTableShort">
		<tr>
			<td class="contentCell">
				<h1>Rezultate interogare:</h1>
				<asp:Label id="lblError" ForeColor="red" EnableViewState="false" Runat="Server" />
				<it:DisplayIssues id="ctlDisplayIssues" Runat="Server" onrebindcommand="IssuesRebind" />
			</td>
		</tr>
	</table>
</form>
<it:Footer runat="server" id="Footer1" />
