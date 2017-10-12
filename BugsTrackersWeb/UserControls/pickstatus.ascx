<%@ Control Language="c#" Inherits="BugsTrackers.PickStatus" CodeFile="PickStatus.ascx.cs" %>
<asp:DropDownList id="dropStatus" Runat="Server" CssClass="standardText">
	<asp:ListItem Value="0">-- Select Status --</asp:ListItem>
</asp:DropDownList>
<asp:RequiredFieldValidator id="reqVal" Visible="false" ControlToValidate="dropStatus" InitialValue="0" Text="(required)"
	Runat="Server" />
