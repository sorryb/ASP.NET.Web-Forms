<%@ Control Language="c#" Inherits="BugsTrackers.DisplayCustomFields" CodeFile="DisplayCustomFields.ascx.cs" %>
<hr id="divider" Runat="Server">
</hr>
<asp:DataList id="lstCustomFields" RepeatColumns="2" RepeatDirection="Horizontal" CellPadding="5"
	Width="800" Runat="Server">
	<ItemStyle Width="15%" />
	<ItemTemplate>
		<asp:Label id="lblFieldName" Runat="Server" />
		</td><td>
			<asp:TextBox id="txtFieldValue" Runat="Server" CssClass="standardText"/>
			<asp:CompareValidator id="valCompare" ControlToValidate="txtFieldValue" Operator="DataTypeCheck" Display="Dynamic"
				Runat="Server" />
			<asp:RequiredFieldValidator ControlToValidate="txtFieldValue" id="valReq" Text="(required)" Display="Dynamic"
				Runat="Server" />
	</ItemTemplate>
</asp:DataList>
