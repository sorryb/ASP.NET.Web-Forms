<%@ Control Language="c#" Inherits="BugsTrackers.Issues.UserControls.Attachments" CodeFile="Attachments.ascx.cs" %>
<%@ Implements interface="BugsTrackers.UserInterfaceLayer.IIssueTab" %>
<asp:DataGrid id="grdAttachments" Font-Size="12pt" AutoGenerateColumns="false" PageSize="5" Width="600"
	BorderColor="White" BorderStyle="None" CellPadding="2" Runat="Server">
	<ItemStyle VerticalAlign="top" />
	<Columns>
		<asp:TemplateColumn>
			<ItemTemplate>
				<a href='DownloadAttachment.aspx?id=<%# DataBinder.Eval(Container.DataItem, "Id")%>'>
					vizualizare</a> &nbsp;
				<asp:LinkButton ID="lnkDelete" Text="delete" visible="False" runat="Server" />
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:TemplateColumn>
			<ItemTemplate>
				<a href='DownloadAttachment.aspx?id=<%# DataBinder.Eval(Container.DataItem, "Id")%>'>
					<%# DataBinder.Eval(Container.DataItem, "FileName")%>
				</a>
				<br />
				Pus de
				<%# DataBinder.Eval(Container.DataItem, "CreatorDisplayName")%>
				la
				<%# DataBinder.Eval(Container.DataItem, "DateCreated")%>
				<hr />
			</ItemTemplate>
		</asp:TemplateColumn>
	</Columns>
</asp:DataGrid>
<p>
	<b>Adauga atasament:</b> <input id="upAttachment" type="file" runat="Server" NAME="upAttachment" Class="standardText" style="width: 422px">
	&nbsp;
	<asp:Button id="btnAdd" Text="Adauga" CssClass="standardText" runat="Server" onclick="btnAdd_Click" />
</p>
