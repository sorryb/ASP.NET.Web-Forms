<%@ Control Language="c#" Inherits="BugsTrackers.PickPriority" CodeFile="PickPriority.ascx.cs" %>
<asp:DropDownList id="dropPriority" Runat="Server" CssClass="standardText">
	<asp:ListItem value="0">-- Select Priority --</asp:ListItem>
</asp:DropDownList>
<asp:RequiredFieldValidator id="reqVal" Visible="false" ControlToValidate="dropPriority" InitialValue="0" Text="(required)"
	Runat="Server" />
