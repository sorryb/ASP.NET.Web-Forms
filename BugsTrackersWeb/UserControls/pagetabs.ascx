<%@ Control Language="c#" Inherits="BugsTrackers.UserControls.PageTabs" CodeFile="PageTabs.ascx.cs" %>
<%@ Register Assembly="BugTrackerWebControls" Namespace="BugTrackerWebControls"    TagPrefix="cc1" %>

			
<TABLE id="TableMenu" cellSpacing="0" cellPadding="0" width="100%" border="0">
  <TR bgcolor=#336699> 
    <TD  background='<%=Page.ResolveUrl("~/img/tab.bg.dln.gif")%>' rowSpan="2"><A href="" target="_top"></A></TD>
    <TD noWrap background='<%=Page.ResolveUrl("~/img/tab.bg.dln.gif")%>' rowSpan="2"><FONT class="D">Bug 
      Tracker</FONT><Font size=3 color = white> 2.0</font></TD>
    <TD rowSpan="2"><IMG src='<%=Page.ResolveUrl("~/img/tab.slide.hm.li.gif")%>'></TD>
    <TD bgColor="#336699" colSpan="13" height="13"></TD>
  </TR>
  <TR> 
    <TD bgColor="#3978ad" style="border-top: 1px solid white;" width="100%">
    <cc1:SuperFishMenu ID="superFishMenu" runat="server"  ServerMapPath=<%#Server.MapPath("~")%> />
		<%-- asp:DataList id="lstTabs" CellPadding="0" RepeatDirection="Horizontal" EnableViewState="False"
			Runat="Server" OnItemDataBound = lstTabs_ItemDataBound>
			<ItemTemplate>
				<asp:HyperLink id="lnkTab" Runat="Server" />
				
			</ItemTemplate>
		</asp:DataList--%>
		
	</TD>
    <TD rowSpan="2" bgColor="#336699" ><IMG src='<%=Page.ResolveUrl("~/img/tab.slide.hm.li.right.GIF")%>' width="109" height="35"></TD>
    <TD bgColor="#3978ad"  width="100%" background='<%=Page.ResolveUrl("~/img/tab.bg.sln.gif")%>'align=center  >
    <%-- asp:HyperLink id="lnkLogOff"   Text="Iesire" CssClass=G
									NavigateUrl="~/LogOff.aspx" Runat="Server">Iesire</asp:HyperLink--%></td>
  </TR>
</TABLE>
<table cellspacing=0 cellpadding=0 width="100%" border=0>
  <tbody> 
  <tr bgcolor=#4791c5> 
    <td colspan=3><img height=1 src='<%=Page.ResolveUrl("~/img/spacer.gif")%>' 
      width=779></td>
  </tr>
  <tr bgcolor=#4791c5> 
    <td style="PADDING-LEFT: 10px; HEIGHT: 20px"> 
      <table cellspacing=0 cellpadding=0 width="100%" border=0>
        <tbody> 
        <tr> 
          <td valign=center noWrap align=left width="10%"><font 
            class=G>administrator</font></td>
          <td class=P id=MsngrTD noWrap width="1%"> 
            <div class=G id=newtitledropdown 
            style="DISPLAY: none; VERTICAL-ALIGN: middle; WIDTH: 1%" 
            >&nbsp;&nbsp;Statut: <span class=F id=MessStat 
            style="PADDING-RIGHT: 4px; PADDING-LEFT: 0px; PADDING-BOTTOM: 0px; PADDING-TOP: 0px">Offline</span><img 
            id=ImgDrop height=7 
            src='<%=Page.ResolveUrl("~/img/i.p.downarrow.gif")%>' width=7 
            align=absMiddle></div>
          </td>
          <td width="100%"></td>
        </tr>
        </tbody>
      </table>
    </td>
    <td style="PADDING-LEFT: 10px; HEIGHT: 20px" align=right> 
      <table cellspacing=0 cellpadding=0 border=0>
        <tbody> 
        <tr> 
          <td valign=center align=right><a class=G 
            href="" target=_top>Stiri/noutati</a> 
            &nbsp;</td>
            <td align="right" valign="center">
                versiune:<%=AsemblyVersion %> &nbsp;</td>            
        </tr>
        </tbody>
      </table>
    </td>
  </tr>
  </tbody>
</table>

