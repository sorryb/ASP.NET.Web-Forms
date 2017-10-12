<%@ page language="C#" autoeventwireup="true" CodeFile="SelectReportAdministrators.aspx.cs" Inherits="Reports_SelectReportAdministrators" %>
<%@ Register TagPrefix="it" TagName="Footer" Src="~/UserControls/Footer.ascx" %>
<%@ Register TagPrefix="it" TagName="Header" Src="~/UserControls/Header.ascx" %>
<link href="favicon.ico" rel="SHORTCUT ICON" />
<it:Header Title="BugTracker Reports" TabName="Cautare" runat="server" id="Header1" />
<H1>
      <span style="color: #ffffff">Administrare - /BugTracker Reports</span></H1>
<br />
<br />
<br />
<hr>
<script>
var reportServer = 'http://localhost/ReportServer/Pages/ReportViewer.aspx?/BugTrackerReports/';
</script>
<form id="Form1" runat="server">
<table><tr><td nowrap="nowrap" style="width: 482px">1.Tuesday, February 20, 2007 10:59 AM </td><td style="width: 100px">35657</td><td style="width: 294px"><A href="javascript:;" onclick="javascript:window.open(reportServer + 'IssuesByStatus&amp;rs:Command=Render',null,'height=window.screen.height,width=window.screen.width,status=no,scroolbar=yes,toolbar=no,resizable=yes,menubar=no,location=no');"><span style="font-size: 12pt; font-family: Arial"><strong>Raport bug-uri/evolutii dupa status</strong></span></A><span
                    style="font-size: 12pt; font-family: Arial"><strong> </strong></span></td></tr><tr><td style="width: 482px">2.Friday, February 16, 2007 4:46 PM </td><td style="width: 100px">14878</td><td style="width: 294px"><A href="javascript:;" onclick="javascript:window.open(reportServer + 'TaskByID&amp;rs:Command=Render',null,'height=window.screen.height,width=window.screen.width,status=no,scroolbar=yes,toolbar=no,resizable=yes,menubar=no,location=no');"><span style="font-size: 12pt; font-family: Arial"><strong>Raport bug /evolutie</strong></span></A><span
                    style="font-size: 12pt; font-family: Arial"><strong> </strong></span></td></tr><tr><td style="width: 482px">3.Friday, February 16, 2007 4:46 PM </td><td style="width: 100px">10933</td><td style="width: 294px"><A href="javascript:this.title = this.innerHTML;" onclick="javascript:window.open(reportServer + 'TotalsByUsersAndStatus&amp;rs:Command=Render',null,'height=window.screen.height,width=window.screen.width,status=no,scroolbar=yes,toolbar=no,resizable=yes,menubar=no,location=no');"><span style="font-size: 12pt; font-family: Arial"><strong>Centralizator pe utilizatori</strong></span></A><span
                    style="font-size: 12pt; font-family: Arial"><strong> </strong></span></td></tr><tr><td style="width: 482px">4.Friday, February 16, 2007 4:46 PM</td><td style="width: 100px">23993</td><td style="width: 294px"><A href="javascript:;" onclick="javascript:window.open(reportServer + 'UserActivityAndBugsByDate&amp;rs:Command=Render',null,'height=window.screen.height,width=window.screen.width,status=no,scroolbar=yes,toolbar=no,resizable=yes,menubar=no,location=no');"><span style="font-size: 12pt; font-family: Arial"><strong>Activitati si bug-uri</strong></span></A><span
                    style="font-size: 12pt; font-family: Arial"><strong> </strong></span></td></tr><tr><td style="width: 482px">5.Friday, February 16, 2007 4:46 PM</td><td style="width: 100px">24895</td><td style="width: 294px"><A href="javascript:;" onclick="javascript:window.open(reportServer + 'UserActivityByDate&amp;rs:Command=Render',null,'height=window.screen.height,width=window.screen.width,status=no,scroolbar=yes,toolbar=no,resizable=yes,menubar=no,location=no');"><span style="font-size: 12pt; font-family: Arial"><strong>Raport activitate utilizator pe o perioada</strong></span></A><span
                    style="font-size: 12pt; font-family: Arial"><strong> </strong></span></td></tr><tr><td style="width: 482px">6.Friday, February 16, 2007 4:46 PM</td><td style="width: 100px">25600</td><td style="width: 294px"><A href="javascript:;" onclick="javascript:window.open(reportServer + 'UsersActivityByDate&amp;rs:Command=Render',null,'height=window.screen.height,width=window.screen.width,status=no,scroolbar=yes,toolbar=no,resizable=yes,menubar=no,location=no');"><span style="font-size: 12pt; font-family: Arial"><strong>Activitate utilizatori</strong></span></A><span
                    style="font-size: 12pt; font-family: Arial"><strong> </strong></span></td></tr><tr><td style="width: 482px">5.Friday, February 16, 2007 4:46 PM</td><td style="width: 100px">25600</td><td style="width: 294px"><a href="javascript:;" onclick="javascript:window.open(reportServer + 'BugsTotalizerByWeeks&amp;rs:Command=Render',null,'height=window.screen.height,width=window.screen.width,status=no,scroolbar=yes,toolbar=no,resizable=yes,menubar=no,location=no');"><span style="font-size: 12pt; font-family: Arial"><strong>Centralizator bug-uri</strong></span></a><span style="font-size: 12pt; font-family: Arial"><strong> </strong></span></td></tr><tr><td style="width: 482px">7.Friday, February 16, 2007 4:46 PM</td><td style="width: 100px">25600</td><td style="width: 294px"><a href="javascript:;" onclick="javascript:window.open(reportServer + 'FilterIssuesByCategoryUsersStatus&amp;rs:Command=Render',null,'height=window.screen.height,width=window.screen.width,status=no,scroolbar=yes,toolbar=no,resizable=yes,menubar=no,location=no');"><span style="font-size: 12pt; font-family: Arial"><strong>Bug-uri filtrate dupa Categorie,Users si Status</strong></span></a><span
                    style="font-size: 12pt; font-family: Arial"><strong> </strong></span></td></tr><tr><td style="width: 482px">8.Friday, February 16, 2007 4:46 PM</td><td style="width: 100px">25600</td><td style="width: 294px"><a href="javascript:;" onclick="javascript:window.open(reportServer + 'BugsByDificulty&amp;rs:Command=Render',null,'height=window.screen.height,width=window.screen.width,status=no,scroolbar=yes,toolbar=no,resizable=yes,menubar=no,location=no');"><span style="font-size: 12pt; font-family: Arial"><strong>Raport bug-uri/evolutii</strong></span></a><span
                    style="font-size: 12pt; font-family: Arial"><strong> </strong></span></td></tr><tr><td style="width: 482px">8.Friday, February 16, 2007 4:46 PM</td><td style="width: 100px">25600</td><td style="width: 294px"><a href="javascript:;" onclick="javascript:window.open(reportServer + 'Evolutii-estimare -durata&amp;rs:Command=Render',null,'height=window.screen.height,width=window.screen.width,status=no,scroolbar=yes,toolbar=no,resizable=yes,menubar=no,location=no');"><span style="font-size: 12pt; font-family: Arial"><strong>Evolutii, estimare si durata totala</strong></span></a><span
                    style="font-size: 12pt; font-family: Arial"><strong> </strong></span></td></tr><tr><td style="width: 482px">9.Friday, February 16, 2007 4:46 PM</td><td style="width: 100px">25600</td><td style="width: 294px"><a href="javascript:;" onclick="javascript:window.open(reportServer + 'Task-uri Estimate-Executate&amp;rs:Command=Render',null,'height=window.screen.height,width=window.screen.width,status=no,scroolbar=yes,toolbar=no,resizable=yes,menubar=no,location=no');"><span style="font-size: 12pt; font-family: Arial"><strong>Timp Estimare-Executare task-uri</strong></span></a><span style="font-size: 12pt; font-family: Arial"><strong> </strong></span></td></tr><tr><td style="width: 482px">10.Monday, july 9, 2007 4:46 PM</td><td style="width: 100px">25600</td><td style="width: 294px"><a href="javascript:;" onclick="javascript:window.open(reportServer + 'WeeklyAnalisysReport&amp;rs:Command=Render',null,'height=window.screen.height,width=window.screen.width,status=no,scroolbar=yes,toolbar=no,resizable=yes,menubar=no,location=no');"><span style="font-size: 12pt; font-family: Arial"><strong>Raport saptamanal pe bug-uri</strong></span></a><span style="font-size: 12pt; font-family: Arial"><strong> </strong></span></td></tr><tr><td style="width: 482px">11.Monday, nov 26, 2008 4:46 PM</td><td style="width: 100px">25600</td><td style="width: 294px"><a href="javascript:;" onclick="javascript:window.open(reportServer + 'AllOpenIssues&amp;rs:Command=Render',null,'height=window.screen.height,width=window.screen.width,status=no,scroolbar=yes,toolbar=no,resizable=yes,menubar=no,location=no');"><span style="font-size: 12pt; font-family: Arial"><strong>Toate bug-urile deschise</strong></span></a><span style="font-size: 12pt; font-family: Arial"><strong> </strong></span></td></tr></table> <table>
                        <tr>
                            <td style="width: 482px">
                                12.Friday, Oct 21, 2010 4:46 PM</td>
                            <td style="width: 100px">
                                25600</td>
                            <td style="width: 294px">
                                <a href="javascript:;" onclick="javascript:window.open(reportServer + 'Holidays&amp;rs:Command=Render',null,'height=window.screen.height,width=window.screen.width,status=no,scroolbar=yes,toolbar=no,resizable=yes,menubar=no,location=no');">
                                    <span style="font-size: 12pt; font-family: Arial"><strong>Concedii odihna</strong></span></a><span style="font-size: 12pt; font-family: Arial"><strong> </strong></span>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 482px">
                                13.Monday,Oct 21, 2010 4:46 PM</td>
                            <td style="width: 100px">
                                25600</td>
                            <td style="width: 294px">
                                <a href="javascript:;" onclick="javascript:window.open(reportServer + 'Deliveries&amp;rs:Command=Render',null,'height=window.screen.height,width=window.screen.width,status=no,scroolbar=yes,toolbar=no,resizable=yes,menubar=no,location=no');">
                                    <span style="font-size: 12pt; font-family: Arial"><strong>Livrari</strong></span></a><span style="font-size: 12pt; font-family: Arial"><strong> </strong></span>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 482px">
                                14.Monday, Oct 21, 2010 4:46 PM</td>
                            <td style="width: 100px">
                                25600</td>
                            <td style="width: 294px">
                                <a href="javascript:;" onclick="javascript:window.open(reportServer + 'AllFixedIssues&amp;rs:Command=Render',null,'height=window.screen.height,width=window.screen.width,status=no,scroolbar=yes,toolbar=no,resizable=yes,menubar=no,location=no');">
                                    <span style="font-size: 12pt; font-family: Arial"><strong>Toate task-urile livrate in&nbsp;</strong></span></a><span style="font-size: 12pt; font-family: Arial"><strong> </strong></span>
                            </td>
                    </table>   
      <span style="font-size: 12pt; font-family: Arial"></span>          <span style="font-size: 12pt;
          font-family: Arial"></span>          <span style="font-size: 12pt; font-family: Arial">
          </span>           <span style="font-size: 12pt; font-family: Arial"></span>         <span
              style="font-size: 12pt; font-family: Arial"></span>         <span style="font-size: 12pt;
                  font-family: Arial"></span>
                  <hr style="font-size: 12pt">
                  </form>
                  
BugTracker 2.0
<it:Footer runat="server" id="Footer1" />
