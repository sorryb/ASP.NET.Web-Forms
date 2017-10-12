<%@ Page Language="c#" smartnavigation="true" validaterequest="false" Inherits="BugsTrackers.Issues.IssueDetail" CodeFile="IssueDetail.aspx.cs" %>
<%@ Register TagPrefix="it" TagName="AdminTabs" Src="~/Administration/UserControls/AdminTabs.ascx" %>
<%@ Register TagPrefix="it" TagName="DisplayCustomFields" Src="~/UserControls/DisplayCustomFields.ascx" %>
<%@ Register TagPrefix="it" TagName="PickMilestone" Src="~/UserControls/PickMilestone.ascx" %>
<%@ Register TagPrefix="it" TagName="PickStatus" Src="~/UserControls/PickStatus.ascx" %>
<%@ Register TagPrefix="it" TagName="PickProject" Src="~/UserControls/PickProject.ascx" %>
<%@ Register TagPrefix="it" TagName="PickSingleUser" Src="~/UserControls/PickSingleUser.ascx" %>
<%@ Register TagPrefix="it" TagName="PickCategory" Src="~/UserControls/PickCategory.ascx" %>
<%@ Register TagPrefix="it" TagName="PickPriority" Src="~/UserControls/PickPriority.ascx" %>
<%@ Register TagPrefix="it" TagName="Footer" Src="~/UserControls/Footer.ascx" %>
<%@ Register TagPrefix="it" TagName="IssueTabs" Src="~/Issues/UserControls/IssueTabs.ascx" %>
<%@ Register TagPrefix="it" TagName="Header" Src="~/UserControls/Header.ascx" %>
<it:Header Title="Bugs Trackers - task-uri" TabName="Taskuri" runat="server" id="Header1" />


<form runat="server">
	<table class="contentTableShort">
		<tr>
			<td class="contentCell">
				<table cellpadding="10" width="800">
					<tr>
						<td colspan="2">
							<asp:Label id="lblError" ForeColor="red" EnableViewState="false" Runat="Server" />
						</td>
					</tr>
					<tr>
						<td><h1>Task&nbsp;
								<asp:Label id="lblIssueId" Runat="Server" /></h1>
						</td>
						<td align="right">
							<asp:button id="btnSave" runat="server" width="53px" CssClass="standardButton" Text="Salvare" onclick="btnSaveClick"></asp:button>
							&nbsp;&nbsp;
							<asp:button id="btnDone" runat="server" width="53px" CssClass="standardButton" Text="Facut" onclick="btnDoneClick"></asp:button>
							&nbsp;&nbsp;
							<asp:button id="btnCancel" runat="server" width="61px" causesvalidation="False" CssClass="standardButton"
								Text="Revoaca" onclick="CancelButtonClick"></asp:button>
							&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
							<asp:button id="btnDelete" runat="server" Text="Sterge" CssClass="standardButton" causesvalidation="False"
								Enabled="false" onclick="DeleteButtonClick"></asp:button>
						</td>
					</tr>
					<tr>
						<td colspan="2"><hr>
						</td>
					</tr>
				</table>
				<table cellpadding="5" width="800">
					<tr>
						<td>
							Titlu:
						</td>
						<td>
							<asp:TextBox id="txtTitle" Columns="50" Runat="Server" CssClass="standardText" />
							<asp:RequiredFieldValidator ControlToValidate="txtTitle" Text="(required)" runat="server" id="RequiredFieldValidator1" />
						</td>
					</tr>
					<tr>
						<td>
							Categorie:
						</td>
						<td>
							<it:PickCategory id="dropCats" DisplayDefault="true" Required="true" Runat="Server" CssClass="standardText"/>
						</td>
					</tr>
					<tr>
						<td>
							Data creare:
						</td>
						<td>
							<asp:Label id="lblDateCreated" Runat="Server" />
						</td>
					</tr>
				</table>
				<table cellpadding="5" width="800">
					<tr>
						<td colspan="4">
							<hr>
						</td>
					</tr>
					<tr>
						<td>
							Asignat la:
						</td>
						<td>
							<it:PickSingleUser id="dropAssigned" DisplayDefault="true" Required="true" runat="Server" Class="standardText"/>
						</td>
						<td>
							Status:
						</td>
						<td>
							<it:PickStatus id="dropStatus" DisplayDefault="true" Required="true" Runat="Server" Class="standardText"/>
						</td>
					</tr>
					<tr>
						<td>
							Posedat de:
						</td>
						<td>
							<it:PickSingleUser id="dropOwned" DisplayDefault="true" Required="true" runat="Server" Class="standardText" />
						</td>
						<td>
							Prioritate:
						</td>
						<td>
							<it:PickPriority id="dropPriority" DisplayDefault="true" Required="true" Runat="Server" Class="standardText"/>
						</td>
					</tr>
					<tr>
						<td>
							Creat de:
						</td>
						<td>
							<asp:Label id="lblCreator" Runat="Server" />
						</td>
						<td>
							Piatra de hotar:
						</td>
						<td>
							<it:PickMilestone id="dropMilestone" DisplayDefault="true" Required="true" Runat="Server" Class="standardText" />
						</td>
					</tr>
				</table>
				<it:DisplayCustomFields id="ctlCustomFields" Runat="Server" Class="standardText"/>
			</td>
		</tr>
	</table>
	<br>
	<it:IssueTabs id="ctlIssueTabs" Visible="false" runat="Server" />
</form>
<it:Footer runat="server" id="Footer1" />
