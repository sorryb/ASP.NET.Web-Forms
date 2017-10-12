<%@ implements interface="BugsTrackers.UserInterfaceLayer.IIssueTab" %>
<%@ Control Language="c#" Inherits="BugsTrackers.Issues.UserControls.SubIssues" CodeFile="SubIssues.ascx.cs" %>
<p>
	List all issues that are dependent on the current issue. Enter the ID of the 
	issue in the text box below and click the Add Sub Issue button.
</p>
<asp:DataGrid id="grdIssues" AutoGenerateColumns="false" Width="600" BorderColor="White" BorderStyle="None"
	CellPadding="2" Runat="Server">
	<headerstyle cssclass="gridHeader"></headerstyle>
	<columns>
		<asp:templatecolumn SortExpression="Id" HeaderText="ID">
			<headerstyle horizontalalign="Left" cssclass="gridHeader"></headerstyle>
			<itemstyle cssclass="gridFirstItem"></itemstyle>
			<itemtemplate>
				&nbsp;
				<asp:Label id="lblIssueId" Runat="Server" />
			</itemtemplate>
		</asp:templatecolumn>
		<asp:hyperlinkcolumn DataNavigateUrlField="IssueId" DataNavigateUrlFormatString="~/Issues/IssueDetail.aspx?id={0}"
			DataTextField="Title" SortExpression="Title" HeaderText="Issue (click title to view details)">
			<headerstyle horizontalalign="Left" cssclass="gridHeader"></headerstyle>
			<itemstyle cssclass="gridItem"></itemstyle>
		</asp:hyperlinkcolumn>
		<asp:templatecolumn>
			<headerstyle horizontalalign="Left" cssclass="gridHeader"></headerstyle>
			<itemstyle cssclass="gridLastItem"></itemstyle>
			<itemtemplate>
				<asp:Button id="btnDelete" Text="Delete" CssClass="standardText" Runat="Server" />
			</itemtemplate>
		</asp:templatecolumn>
	</columns>
</asp:DataGrid>
<p>
	Issue ID:
	<asp:TextBox id="txtIssueId" Columns="5" CssClass="standardText" Runat="Server" />
	<asp:Button Text="Add Sub Issue" CssClass="standardText" Runat="server" id="btnAdd" onclick="AddRelatedIssue" />
	<asp:CompareValidator ControlToValidate="txtIssueId" Operator="DataTypeCheck" Type="Integer" Text="(integer)"
		Runat="server" id="CompareValidator1" />
</p>
