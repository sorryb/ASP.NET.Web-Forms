<%@ Control Language="c#" Inherits="BugsTrackers.Issues.IssueTabs" CodeFile="IssueTabs.ascx.cs" %>
<asp:DataList id="lstTabs" CellSpacing="0" CellPadding="0" RepeatDirection="Horizontal" Runat="Server" onprerender="Page_PreRender" onload="Page_Load">
	<ItemStyle CssClass="adminTabInactive" />
	<SelectedItemStyle CssClass="adminTabActive" />
	<ItemTemplate>
		<asp:LinkButton id="lnkTab" CausesValidation="false" Runat="Server" />
	</ItemTemplate>
	<SelectedItemTemplate>
		<asp:LinkButton id="lnkTab" CausesValidation="false" Runat="Server" />
	</SelectedItemTemplate>
</asp:DataList>
<table class="contentTableShort">
	<tr>
		<td class="contentCell">
			<asp:PlaceHolder id="plhContent" Runat="Server" />
			<p>&nbsp;</p>
		</td>
	</tr>
</table>
