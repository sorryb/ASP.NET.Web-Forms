<%@ Register TagPrefix="it" TagName="PageTabs" Src="~/UserControls/PageTabs.ascx" %>
<%@ Control Language="c#" Inherits="BugsTrackers.UserControls.Header" CodeFile="Header.ascx.cs" %>

<HTML>
	<HEAD >
		<title>
			<%=Title%>
		</title>
		<link href='<%=Page.ResolveUrl("~/styles/styles.css")%>' type="text/css" rel="stylesheet">
		<link href='<%=Page.ResolveUrl("~/App_Themes/Fish/superfish.css")%>' type="text/css" rel="stylesheet" />
		<link href='<%=Page.ResolveUrl("~/App_Themes/Fish/superfish-vertical.css")%>' type="text/css" rel="stylesheet" />

	</HEAD>
	<body>
		<table width="100%" border="0" cellspacing="0" cellpadding="0" backgroundZ='<%=Page.ResolveUrl("~/images/bars.gif")%>'>
			<tr>
				<td></td>
				<td valign="bottom" >
					<it:PageTabs id="ctlPageTabs" Runat="Server" />
					
				</td>
			</tr>
		</table>
		<table class="bodyTable" height="100%">
			<tr>
				<td class="bodyCell" align="center">
	
