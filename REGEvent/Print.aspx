<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="Print.aspx.cs" Inherits="_Print" MasterPageFile="~/Site.master" Title="Event Calendar with Day/Week/Month Views" %>
<%@ Register Assembly="DayPilot" Namespace="DayPilot.Web.Ui" TagPrefix="DayPilot" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
	<script type="text/javascript" src="js/modal.js"></script>
	<script type="text/javascript" src="js/jquery-1.9.1.min.js"></script>
    <link href='css/main.css' type="text/css" rel="stylesheet" /> 
    <link href='Themes/calendar_white.css' type="text/css" rel="stylesheet" /> 
    <link href='Themes/scheduler_white.css' type="text/css" rel="stylesheet" /> 
    <link href='Themes/month_white.css' type="text/css" rel="stylesheet" /> 
    <link href='Themes/navigator_white.css' type="text/css" rel="stylesheet" /> 
</asp:Content>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">

<h2>Print</h2>

<div>
<asp:Image runat="server" ID="ImageCalendar" />
</div>

<script type="text/javascript">
    window.print();
</script>

</asp:Content>