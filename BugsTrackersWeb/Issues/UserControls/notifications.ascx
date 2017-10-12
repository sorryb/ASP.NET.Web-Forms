<%@ Control Language="c#" Inherits="BugsTrackers.Issues.UserControls.Notifications" CodeFile="Notifications.ascx.cs" %>
<%@ implements interface="BugsTrackers.UserInterfaceLayer.IIssueTab" %>
Persoanele de mai jos vor fii notificate cand acest task e modificat:
<asp:DataGrid id="grdNotifications" AutoGenerateColumns="false" Width="600" BorderColor="White"
	BorderStyle="None" CellPadding="2" Runat="Server">
	<Columns>
		<asp:BoundColumn DataField="NotificationDisplayName" />
		<asp:BoundColumn DataField="NotificationEmail" />
	</Columns>
</asp:DataGrid>
<p>
	<asp:Button Text="Receive Notifications" CssClass="standardText" Runat="server" id="Button1" onclick="AddNotification" />
	&nbsp;
	<asp:Button Text="Don't Receive Notifications" CssClass="standardText" Runat="server" id="Button2" onclick="DeleteNotification" />
</p>
