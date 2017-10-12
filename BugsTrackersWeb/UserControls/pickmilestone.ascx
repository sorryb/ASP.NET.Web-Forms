<%@ Control Language="c#" Inherits="BugsTrackers.PickMilestone" CodeFile="PickMilestone.ascx.cs" %>
<asp:DropDownList id="dropMilestone" Runat="Server" CssClass="standardText">
	<asp:ListItem value="0">-- Select Milestone --</asp:ListItem>
</asp:DropDownList>
<asp:RequiredFieldValidator id="reqVal" Visible="false" ControlToValidate="dropMilestone" InitialValue="0" Text="(required)"
	Runat="Server" />
