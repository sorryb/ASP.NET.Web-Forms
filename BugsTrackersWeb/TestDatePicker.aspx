<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TestDatePicker.aspx.cs" Inherits="Default2" %>
<%@ Register Assembly="BugTrackerWebControls" Namespace="BugTrackerWebControls.XControls"    TagPrefix="cc1" %>
<html  >
<head runat="server">
<style>
 div.ui-datepicker {     font-size: 80.5%; } 
</style>
    <link type="text/css" href="styles/redmond/jquery-ui-1.8.16.custom.css" rel="stylesheet" />
    <title>
    </title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <cc1:DatePicker ID="DatePickerControl" AutoPostBack="true" RegisterJQuery="true" DateLabelText="Data :" runat="server" OnDateChanged ="WeekEnding_TextChanged"  />
    </div>
    </form>
</body>
</html>
