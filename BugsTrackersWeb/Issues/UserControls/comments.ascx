<%@ Control Language="c#" Inherits="BugsTrackers.Issues.Comments" CodeFile="Comments.ascx.cs" %>
<%@ implements interface="BugsTrackers.UserInterfaceLayer.IIssueTab" %>

<div align="right">
	Marimea font:
	<asp:DropDownList id="dropFontSize" AutoPostBack="true" Runat="Server" onselectedindexchanged="FontSizeChanged">
		<asp:ListItem Text="12pt" Value="12pt" Selected="true" />
		<asp:ListItem Text="14pt" Value="14pt" />
		<asp:ListItem Text="16pt" Value="16pt" />
		<asp:ListItem Text="20pt" Value="20pt" />
	</asp:DropDownList>
</div>
<asp:DataGrid id="grdComments" Font-Size="12pt" AutoGenerateColumns="false" AllowPaging="true"
	PageSize="5" Width="600" BorderColor="White" BorderStyle="None" CellPadding="2" Runat="Server">
	<Columns>
		<asp:TemplateColumn>
			<ItemTemplate>
				<asp:Label id="lblCreatorDisplayName" Runat="Server" />
				comentat de
				<asp:Label id="lblDateCreated" Runat="Server" />
				<hr />
				<pre >
                        <asp:Literal id="ltlComment" Runat="Server" />
                </pre>
			</ItemTemplate>
		</asp:TemplateColumn>
	</Columns>
	<pagerstyle horizontalalign="Center"></pagerstyle>
</asp:DataGrid>
<p>
	Comentariu
	<br>
	<asp:TextBox id="txtComment" TextMode="MultiLine" Columns="75" Rows="5" runat="Server" CssClass="standardText"/>
	<br>
	<asp:Button id="btnAdd" Text="Adauga Comentariu" CssClass="standardText" Runat="Server" onclick="AddComment" />
</p>
