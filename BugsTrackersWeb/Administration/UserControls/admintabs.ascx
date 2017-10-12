<%@ Control Language="c#" Inherits="BugsTrackers.Administration.UserControls.AdminTabs" CodeFile="AdminTabs.ascx.cs" %>
<asp:DataList id="lstTabs" CellSpacing="0" CellPadding="0" RepeatDirection="Horizontal" EnableViewState="false"
	Runat="Server">
	<ItemTemplate>
		<asp:HyperLink id="lnkTab" Runat="Server" />
	</ItemTemplate>
</asp:DataList>
