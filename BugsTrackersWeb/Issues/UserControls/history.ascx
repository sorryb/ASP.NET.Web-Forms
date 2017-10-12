<%@ implements interface="BugsTrackers.UserInterfaceLayer.IIssueTab" %>
<%@ Control Language="c#" Inherits="BugsTrackers.Issues.UserControls.History" CodeFile="History.ascx.cs" %>
<asp:DataGrid id="grdHistory" EnableViewState="false" AutoGenerateColumns="false" Width="600"
	BorderColor="White" BorderStyle="None" CellPadding="2" Runat="Server">
	<Columns>
		<asp:TemplateColumn>
			<ItemTemplate>
				<hr />
				<asp:Label id="lblCreatorDisplayName" Runat="Server" />
				modifica acest task la
				<asp:Label id="lblDateCreated" Runat="Server" />
			</ItemTemplate>
		</asp:TemplateColumn>
	</Columns>
</asp:DataGrid>
