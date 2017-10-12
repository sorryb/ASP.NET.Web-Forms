<%@ Control Language="c#" Inherits="BugsTrackers.PickSingleUser" CodeFile="PickSingleUser.ascx.cs" %>
<script runat="server">


</script>
<asp:DropDownList id="dropUsers" runat="Server" CssClass="standardText">
	<asp:ListItem Value="0">-- Select User --</asp:ListItem>
</asp:DropDownList>
<asp:RequiredFieldValidator id="reqVal" Visible="false" ControlToValidate="dropUsers" InitialValue="0" Text="(required)"
	Runat="Server" />
