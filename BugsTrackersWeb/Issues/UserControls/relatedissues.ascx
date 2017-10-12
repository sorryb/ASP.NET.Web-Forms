<%@ Control Language="c#" Inherits="BugsTrackers.Issues.UserControls.RelatedIssues" CodeFile="RelatedIssues.ascx.cs" %>
<%@ implements interface="BugsTrackers.UserInterfaceLayer.IIssueTab" %>
<p>
	Afiseaza toate task-urile care depin de acest task.Introduceti ID-ul acestui 
	task si adaugati-l.
</p>
<asp:DataGrid id="grdIssues" AutoGenerateColumns="false" Width="600" BorderColor="White" BorderStyle="None"
	CellPadding="2" Runat="Server">
	<headerstyle cssclass="gridHeader"></headerstyle>
	<columns>
		<asp:templatecolumn SortExpression="IssueId" HeaderText="ID">
			<headerstyle horizontalalign="Left" cssclass="gridHeader"></headerstyle>
			<itemstyle cssclass="gridFirstItem"></itemstyle>
			<itemtemplate>
				&nbsp;
				<asp:Label id="lblIssueId" Runat="Server" />
			</itemtemplate>
		</asp:templatecolumn>
		<asp:hyperlinkcolumn DataNavigateUrlField="IssueId" DataNavigateUrlFormatString="~/Issues/IssueDetail.aspx?id={0}"
			DataTextField="Title" SortExpression="Title" HeaderText="Task (click pentru a vedea detaliu)">
			<headerstyle horizontalalign="Left" cssclass="gridHeader"></headerstyle>
			<itemstyle cssclass="gridItem"></itemstyle>
		</asp:hyperlinkcolumn>
		<asp:templatecolumn>
			<headerstyle horizontalalign="Left" cssclass="gridHeader"></headerstyle>
			<itemstyle cssclass="gridLastItem"></itemstyle>
			<itemtemplate>
				<asp:Button id="btnDelete" Text="Sterge-l" CssClass="standardText" Runat="Server" />
			</itemtemplate>
		</asp:templatecolumn>
	</columns>
</asp:DataGrid>
<p>&nbsp;ID task dependent :
	<asp:TextBox id="txtIssueId" Columns="5" CssClass="standardText" Runat="Server" />
	<asp:Button Text="Adauga" CssClass="standardText" Runat="server" id="btnAdd" onclick="AddRelatedIssue" />
	<asp:CompareValidator ControlToValidate="txtIssueId" Operator="DataTypeCheck" Type="Integer" Text="(integer)"
		Runat="server" id="CompareValidator1" />
</p>
