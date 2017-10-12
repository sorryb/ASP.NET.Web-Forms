<%@ Page Language="c#" Inherits="BugsTrackers.Administration.Utilities.Build" CodeFile="Build.aspx.cs" %>
<%@ Register TagPrefix="it" TagName="AdminTabs" Src="~/Administration/UserControls/AdminTabs.ascx" %>
<%@ Register TagPrefix="it" TagName="Header" Src="~/UserControls/Header.ascx" %>
<%@ Register TagPrefix="it" TagName="Footer" Src="~/UserControls/Footer.ascx" %>
<it:Header Title="Detaliu utilizator" TabName="Administrare" runat="server" id="Header1" />
<IFRAME SRC="http://report.kepler.ro/ccnet/default.aspx?_action_ViewFarmReport=true" style="height: 600px" width = 100% >

</IFRAME>
<br />



<it:Footer runat="server" id="Footer1" />
