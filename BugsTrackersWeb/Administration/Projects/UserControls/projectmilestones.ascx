<%@ implements interface="BugsTrackers.UserInterfaceLayer.IEditProjectControl" %>
<%@ Register TagPrefix="IT" TagName="PickImage" Src="~/UserControls/PickImage.ascx" %>
<%@ Control Language="c#" Inherits="BugsTrackers.Administration.Projects.ProjectMilestones" CodeFile="ProjectMilestones.ascx.cs" %>
<fieldset>
	<!--legend>Project Milestones</legend-->
	<legend>Pietre de hotar ale proiectului</legend>
	<table cellpadding="5" Width="350" align="center">
		<tr>
			<!--td>
				When you create an issue, you assign the issue a milestone such as First, 
				Second, or Third. Enter the list of milestones below. Each milestone can be 
				associated with an image.
			</td-->
			<td>
				In momentul cand creati un proiect nou, adaugati o piatra de hotar cum ar fi 
				Primul, Al doilea, Al treilea. Creati astfel o lista de astfel de structuri. 
				Fiecare nou element poate fi asociat la o imagine.
			</td>
		</tr>
		<tr>
			<td>
				<asp:Label id="lblError" ForeColor="red" EnableViewState="false" runat="Server" />
				<asp:CustomValidator Text="Trebuie adaugat cel putin o piatra de hotar" Runat="server" id="CustomValidator1" />
			</td>
		</tr>
		<tr>
			<td>
				<asp:DataGrid id="grdMilestones" AutoGenerateColumns="false" BorderColor="White" BorderStyle="None"
					CellPadding="2" width="100%" Runat="Server">
					<Columns>
						<asp:TemplateColumn HeaderText="Piatra hotar">
							<headerstyle horizontalalign="Left" cssclass="gridHeader"></headerstyle>
							<itemstyle cssclass="gridFirstItem" width="80%"></itemstyle>
							<ItemTemplate>
								<asp:Label id="lblMilestoneName" runat="Server" />
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Imagine">
							<headerstyle horizontalalign="Center" cssclass="gridHeader"></headerstyle>
							<itemstyle horizontalalign="Center" cssclass="gridItem" width="10%"></itemstyle>
							<ItemTemplate>
								<asp:Image id="imgMilestone" runat="Server" />
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn>
							<headerstyle horizontalalign="Right" cssclass="gridHeader"></headerstyle>
							<itemstyle horizontalalign="Right" cssclass="gridLastItem" width="10%"></itemstyle>
							<ItemTemplate>
								<asp:Button id="btnDelete" CommandName="delete" Text="Sterge" CssClass="standardText" ToolTip="Delete Milestone"
									runat="Server" />
							</ItemTemplate>
						</asp:TemplateColumn>
					</Columns>
				</asp:DataGrid>
			</td>
		</tr>
		<tr>
			<td>
				<asp:TextBox id="txtName" MaxLength="50" CssClass="standardText" runat="Server" />
				<asp:Button Text="Adauga" CssClass="standardText" CausesValidation="false" runat="server" id="Button1" onclick="AddMilestone" />
			</td>
		</tr>
		<tr>
			<td>
				<it:PickImage id="lstImages" ImageDirectory="/Milestone" runat="Server" />
			</td>
		</tr>
	</table>
</fieldset>
