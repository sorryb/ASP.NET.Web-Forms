<%@ Control Language="c#" Inherits="BugsTrackers.DisplayIssues" CodeFile="DisplayIssues.ascx.cs" %>
<%@ Register TagPrefix="it" TagName="TextImage" Src="~/UserControls/TextImage.ascx" %>
<script runat="server">

</script>
<asp:datagrid id="grdIssues" runat="server" AllowSorting="True" AutoGenerateColumns="False" CellPadding="0"
	Width="100%" BorderStyle="None" BorderColor="Silver" ForeColor="White" AllowPaging="True">
	<FooterStyle ForeColor="#0033FF" BackColor="#CCCCFF"></FooterStyle>
	<AlternatingItemStyle BackColor="#99CCFF"></AlternatingItemStyle>
	<ItemStyle ForeColor="Blue" BackColor="White"></ItemStyle>
	<HeaderStyle Font-Names="Arial" ForeColor="#CCFFFF" CssClass="gridHeader" BackColor="#000099"></HeaderStyle>
	<Columns>
		<asp:TemplateColumn SortExpression="Id" HeaderText="ID">
			<HeaderStyle HorizontalAlign="Left" CssClass="gridHeader"></HeaderStyle>
			<ItemStyle ForeColor="Maroon" CssClass="gridFirstItem"></ItemStyle>
			<ItemTemplate>
				&nbsp;<%# DataBinder.Eval(Container.DataItem, "Id")%>
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:HyperLinkColumn DataNavigateUrlField="Id" DataNavigateUrlFormatString="~/Issues/IssueDetail.aspx?id={0}"
			DataTextField="Title" SortExpression="Title" HeaderText="Bug / problema ">
			<HeaderStyle HorizontalAlign="Left" CssClass="gridHeader"></HeaderStyle>
			<ItemStyle CssClass="gridItem"></ItemStyle>
		</asp:HyperLinkColumn>
		<asp:TemplateColumn Visible="False" SortExpression="Category" HeaderText="Category">
			<HeaderStyle HorizontalAlign="Left" CssClass="gridHeader"></HeaderStyle>
			<ItemStyle HorizontalAlign="Left" CssClass="gridItem"></ItemStyle>
			<ItemTemplate>
				&nbsp;<%# DataBinder.Eval(Container.DataItem, "CategoryName")%>
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:TemplateColumn Visible="False" SortExpression="Creator" HeaderText="Creator">
			<HeaderStyle HorizontalAlign="Left" CssClass="gridHeader"></HeaderStyle>
			<ItemStyle HorizontalAlign="Left" CssClass="gridItem"></ItemStyle>
			<ItemTemplate>
				&nbsp;<%# DataBinder.Eval(Container.DataItem, "CreatorDisplayName")%>
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:TemplateColumn Visible="False" SortExpression="Owner" HeaderText="Owner">
			<HeaderStyle HorizontalAlign="Left" CssClass="gridHeader"></HeaderStyle>
			<ItemStyle HorizontalAlign="Left" CssClass="gridItem"></ItemStyle>
			<ItemTemplate>
				&nbsp;<%# DataBinder.Eval(Container.DataItem, "OwnerDisplayName")%>
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:TemplateColumn Visible="False" SortExpression="Assigned" HeaderText="Assigned">
			<HeaderStyle HorizontalAlign="Left" CssClass="gridHeader"></HeaderStyle>
			<ItemStyle HorizontalAlign="Left" CssClass="gridItem"></ItemStyle>
			<ItemTemplate>
				&nbsp;<%# DataBinder.Eval(Container.DataItem, "AssignedDisplayName" )%>
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:TemplateColumn Visible="False" SortExpression="Milestone" HeaderText="Milestone">
			<HeaderStyle HorizontalAlign="Left" CssClass="gridHeader"></HeaderStyle>
			<ItemStyle HorizontalAlign="Left" CssClass="gridItem"></ItemStyle>
			<ItemTemplate>
				&nbsp;
				<it:TextImage id="ctlMilestone" Text='<%# DataBinder.Eval(Container.DataItem, "MilestoneName" )%>' ImageUrl='<%# DataBinder.Eval(Container.DataItem, "MilestoneImageUrl" )%>' ImageDirectory="/Milestone" Runat="Server" />
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:TemplateColumn Visible="False" SortExpression="Status" HeaderText="Status">
			<HeaderStyle HorizontalAlign="Left" CssClass="gridHeader"></HeaderStyle>
			<ItemStyle HorizontalAlign="Left" CssClass="gridItem"></ItemStyle>
			<ItemTemplate>
				&nbsp;
				<it:TextImage id="ctlStatus" Text='<%# DataBinder.Eval(Container.DataItem, "StatusName" )%>' ImageUrl='<%# DataBinder.Eval(Container.DataItem, "StatusImageUrl" )%>' ImageDirectory="/Status" Runat="Server" />
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:TemplateColumn Visible="False" SortExpression="Priority" HeaderText="Priority">
			<HeaderStyle HorizontalAlign="Left" CssClass="gridHeader"></HeaderStyle>
			<ItemStyle HorizontalAlign="Left" CssClass="gridItem"></ItemStyle>
			<ItemTemplate>
				&nbsp;
				<it:TextImage id="ctlPriority" Text='<%# DataBinder.Eval(Container.DataItem, "PriorityName" )%>' ImageUrl='<%# DataBinder.Eval(Container.DataItem, "PriorityImageUrl" )%>' ImageDirectory="/Priority" Runat="Server" />
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:TemplateColumn SortExpression="Created" HeaderText="Created">
			<HeaderStyle HorizontalAlign="Right" CssClass="gridHeader"></HeaderStyle>
			<ItemStyle HorizontalAlign="Right" CssClass="gridLastItem"></ItemStyle>
			<ItemTemplate>
				&nbsp;<%# DataBinder.Eval(Container.DataItem, "DateCreated", "{0:d}")%>
			</ItemTemplate>
		</asp:TemplateColumn>
	</Columns>
	<PagerStyle HorizontalAlign="Center" ForeColor="Blue" BackColor="#CCE6F4" Mode="NumericPages"></PagerStyle>
</asp:datagrid><asp:label id="lblNoIssues" Runat="Server" Visible="false">Nici un rezultat</asp:label>
<table id="tblOptions" width="100%" runat="server" visible="false">
	<tr>
		<td>
            <asp:ImageButton ID="ImageButton1" onclick="SelectColumnsClick" runat="server" ImageUrl="~/Images/plus.gif" ToolTip="Change Settings" /></td>
		<td align="right"><asp:ImageButton ID="ImageButton2" onclick="ToExcel" runat="server" ImageUrl="~/Images/exportexcel.gif" ToolTip="Export to Excel" /></td>
	</tr>
</table>
<asp:panel id="pnlSelectColumns" runat="Server" Visible="false" BorderWidth=1px>
	<BR>
    Selecteaza coloane<TABLE borderColor="black" height="40" cellSpacing="0" cellPadding="4" border="0">
		<TR>
			<TD>
				<asp:CheckBoxList id="lstIssueColumns" Runat="Server" RepeatDirection="Horizontal">
					<asp:ListItem Text="Category" Value="2" />
					<asp:ListItem Text="Creator" Value="3" />
					<asp:ListItem Text="Owner" Value="4" />
					<asp:ListItem Text="Assigned" Value="5" />
					<asp:ListItem Text="Milestone" Value="6" />
					<asp:ListItem Text="Status" Value="7" />
					<asp:ListItem Text="Priority" Value="8" />
					<asp:ListItem Text="Created" Value="9" />
				</asp:CheckBoxList><BR>

			</TD>
			<td>
				<asp:Button id="Button1" Runat="server" Text="Salveaza" CssClass="standardText" onclick="SaveClick"></asp:Button>&nbsp;&nbsp;
				<asp:Button id="Button2" Runat="server" Text="Revoca" CssClass="standardText" onclick="CancelClick"></asp:Button>
			</td>
		</TR>
	</TABLE>
</asp:panel>
