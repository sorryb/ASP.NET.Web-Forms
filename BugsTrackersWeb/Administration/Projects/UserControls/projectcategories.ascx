<%@ Control Language="c#" Inherits="BugsTrackers.Administration.Projects.ProjectCategories" CodeFile="ProjectCategories.ascx.cs" %>
<%@ Register TagPrefix="it" TagName="AdminTabs" Src="~/Administration/UserControls/AdminTabs.ascx" %>
<%@ Register TagPrefix="it" TagName="Header" Src="~/UserControls/Header.ascx" %>
<%@ Register TagPrefix="it" TagName="Footer" Src="~/UserControls/Footer.ascx" %>
<%@ Register TagPrefix="it" TagName="PickCategory" Src="~/UserControls/PickCategory.ascx" %>
<%@ implements interface="BugsTrackers.UserInterfaceLayer.IEditProjectControl" %>
<fieldset>
	<!--legend>Project Categories</legend-->
	<legend>Categorii</legend>
	<table cellpadding="5" width="350" align="center">
		<tr>
			<td>
				<asp:Label id="lblError" ForeColor="Red" EnableViewState="false" Runat="Server" />
				<asp:CustomValidator Text="Trebuie adaugata cel putin o categorie"
					Runat="server" id="CustomValidator1" />
			</td>
		</tr>
		<tr>
			<!--td>
				When you create an issue, you assign the issue a category. Add categories by 
				clicking the Add button. You can add subcategories by selecting a parent 
				category from the dropdown list below.
			</td-->
			<td>
				Cand veti crea un nou task, trebuie sa-i asignati o categorie.
				Adaugati in aceasta pagina categorii noi.
				Puteti adauga subcategorii,selectand parintele din lista.
			</td>
		</tr>
		<tr>
			<td>
				<it:PickCategory id="dropCats" Runat="Server" />
				&nbsp;
				<asp:Button Text="Sterge" CssClass="standardText" runat="server"
					id="Button1" onclick="btnDeleteCategory" />
			</td>
		</tr>
		<tr>
			<td>
				<asp:TextBox id="txtName" CssClass="standardText" runat="Server" />
				<asp:Button Text="Adauga" CssClass="standardText" CausesValidation="false"
					runat="server" id="Button2" onclick="btnAddCategory" />
			</td>
		</tr>
	</table>
</fieldset>
