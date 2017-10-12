<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SelectReport.aspx.cs" Inherits="Reports_SelectReport" %>
<%@ Register TagPrefix="it" TagName="Footer" Src="~/UserControls/Footer.ascx" %>
<%@ Register TagPrefix="it" TagName="Header" Src="~/UserControls/Header.ascx" %>
<link href="" rel="stylesheet" type="text/css" />
<link href="favicon.ico" rel="SHORTCUT ICON" />
<it:Header Title="BugTracker Reports" TabName="Cautare" runat="server" id="Header1" />
<H1>
      <span style="color: #ffffff">Administrare - /BugTracker Reports</span></H1>
<br />
<br />
<br />
<hr>
<script>
var reportServer = 'http://report.kepler.ro/ReportServer/Pages/ReportViewer.aspx?/BugTrackerReports/';
</script>
<pre>
<table><tr><td nowrap="nowrap" style="width: 482px">1.Tuesday, February 20, 2007 10:59 AM </td><td style="width: 100px">35657</td><td style="width: 294px"><A href="javascript:;" onclick="javascript:window.open(reportServer + 'IssuesByStatus&amp;rs:Command=Render',null,'height=window.screen.height,width=window.screen.width,status=no,scroolbar=yes,toolbar=no,resizable=yes,menubar=no,location=no');"><span style="font-size: 12pt; font-family: Arial"><strong>Raport bug-uri/evolutii dupa status</strong></span></A><span
                    style="font-size: 12pt; font-family: Arial"><strong> </strong></span></td></tr><tr><td style="width: 482px">2.Friday, February 16, 2007 4:46 PM </td><td style="width: 100px">14878</td><td style="width: 294px"><A href="javascript:;" onclick="javascript:window.open(reportServer + 'TaskByID&amp;rs:Command=Render',null,'height=window.screen.height,width=window.screen.width,status=no,scroolbar=yes,toolbar=no,resizable=yes,menubar=no,location=no');"><span style="font-size: 12pt; font-family: Arial"><strong>Raport bug /evolutie</strong></span></A><span
                    style="font-size: 12pt; font-family: Arial"><strong> </strong></span></td></tr><tr><td style="width: 482px; height: 21px;">3.Friday, February 16, 2007 4:46 PM</td><td style="width: 100px; height: 21px;">23993</td><td style="width: 294px; height: 21px;"><A href="javascript:;" onclick="javascript:window.open(reportServer + 'UserActivityAndBugsByDate&amp;rs:Command=Render',null,'height=window.screen.height,width=window.screen.width,status=no,scroolbar=yes,toolbar=no,resizable=yes,menubar=no,location=no');"><span style="font-size: 12pt; font-family: Arial"><strong>Activitati si bug-uri</strong></span></A><span
                    style="font-size: 12pt; font-family: Arial"><strong> </strong></span></td></tr><tr><td style="width: 482px">4.Friday, February 16, 2007 4:46 PM</td><td style="width: 100px">25600</td><td style="width: 294px"><a href="javascript:;" onclick="javascript:window.open(reportServer + 'FilterIssuesByCategoryUsersStatus&amp;rs:Command=Render',null,'height=window.screen.height,width=window.screen.width,status=no,scroolbar=yes,toolbar=no,resizable=yes,menubar=no,location=no');"><span style="font-size: 12pt; font-family: Arial"><strong>Bug-uri filtrate dupa Categorie,Users si Status</strong></span></a><span
                    style="font-size: 12pt; font-family: Arial"><strong> </strong></span></td></tr>
<tr><td style="width: 482px">5.Friday, February 16, 2007 4:46 PM</td><td style="width: 100px">25600</td><td style="width: 294px"><a href="javascript:;" onclick="javascript:window.open(reportServer + 'Task-uri Estimate-Executate&amp;rs:Command=Render',null,'height=window.screen.height,width=window.screen.width,status=no,scroolbar=yes,toolbar=no,resizable=yes,menubar=no,location=no');"><span style="font-size: 12pt; font-family: Arial"><strong>Timp Estimare-Executare task-uri</strong></span></a><span style="font-size: 12pt; font-family: Arial"><strong> </strong></span></td></tr>
<tr><td style="width: 482px">5.Friday, February 16, 2007 4:46 PM</td><td style="width: 100px">25600</td><td style="width: 294px"><a href="javascript:;" onclick="javascript:window.open(reportServer + 'BugsTotalizerByWeeks&amp;rs:Command=Render',null,'height=window.screen.height,width=window.screen.width,status=no,scroolbar=yes,toolbar=no,resizable=yes,menubar=no,location=no');"><span style="font-size: 12pt; font-family: Arial"><strong>Centralizator bug-uri</strong></span></a><span style="font-size: 12pt; font-family: Arial"><strong> </strong></span></td></tr>
       <tr>
        <td style="width: 482px">
            11.Monday, nov 26, 2008 4:46 PM</td>
        <td style="width: 100px">
            25600</td>
        <td style="width: 294px">
            <a href="javascript:;" onclick="javascript:window.open(reportServer + 'AllOpenIssues&amp;rs:Command=Render',null,'height=window.screen.height,width=window.screen.width,status=no,scroolbar=yes,toolbar=no,resizable=yes,menubar=no,location=no');">
                <span style="font-size: 12pt; font-family: Arial"><strong>Toate bug-urile deschise</strong></span></a><span style="font-size: 12pt; font-family: Arial"><strong>
                    </strong></span>
        </td>
    </tr>
<tr><td style="width: 482px">
                        6.Monday, july 9, 2007 4:46 PM</td><td style="width: 100px">25600</td><td style="width: 294px"><a href="javascript:;" onclick="javascript:window.open(reportServer + 'WeeklyAnalisysReport&amp;rs:Command=Render',null,'height=window.screen.height,width=window.screen.width,status=no,scroolbar=yes,toolbar=no,resizable=yes,menubar=no,location=no');"><span style="font-size: 12pt; font-family: Arial"><strong>Raport saptamanal pe bug-uri</strong></span></a><span style="font-size: 12pt; font-family: Arial"><strong> </strong></span></td></tr></table>   
      <span style="font-size: 12pt; font-family: Arial"></span>          <span style="font-size: 12pt;
          font-family: Arial"></span>          <span style="font-size: 12pt; font-family: Arial">
          </span>           <span style="font-size: 12pt; font-family: Arial"></span>         <span
              style="font-size: 12pt; font-family: Arial"></span>         <span style="font-size: 12pt;
                  font-family: Arial"></span></pre><hr style="font-size: 12pt">
BugTracker 2.0
<it:Footer runat="server" id="Footer1" />
