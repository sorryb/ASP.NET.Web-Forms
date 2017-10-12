<%@ Control Language="c#" Inherits="BugsTrackers.PickCategory" CodeFile="PickCategory.ascx.cs" %>
<script runat="server">

</script>
<asp:DropDownList id="dropCats" runat="Server" CssClass="standardText">
	<asp:ListItem value="0">-- Select Category --</asp:ListItem>
</asp:DropDownList>
<asp:RequiredFieldValidator id="reqVal" Visible="false" ControlToValidate="dropCats" InitialValue="0" Text="(required)"
	Runat="Server" />
