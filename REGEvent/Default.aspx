<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="Default.aspx.cs" Inherits="_Default" MasterPageFile="~/Site.master" Title="Event Calendar with Day/Week/Month Views" %>
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


<script type="text/javascript">
    function modal() {
        var m = new DayPilot.Modal();
        m.closed = function () {
            var data = this.result;
            if (data == "OK") {
            DayPilotCalendar1.commandCallBack("refresh");
                DayPilotScheduler1.commandCallBack("refresh");
            }
        };
        return m;
    }
</script>

    <div style="float:left; width: 150px">
    <DayPilot:DayPilotNavigator 
    ID="DayPilotNavigator1"
    runat="server" 
    ShowMonths="3"
    SkiptMonth="3"
    BoundDayPilotID="DayPilotScheduler1"
    />
 </div>

    <div style="margin-left:220px">
    <DayPilot:DayPilotScheduler 
  ID="DayPilotScheduler1" 
  runat="server" 
        AutoRefreshEnabled="true"
  CellDuration="60" 
  TreeEnabled ="true"
        TreePreventParentUsage ="true"
            HeaderFontSize="8pt"
        EventFontSize="8pt"
           CellWidth="60"
        EventHeight="60"
        Days="2"
        HeightSpec="Max"
  TreeAnimation="False"
    OnCommand="DayPilotScheduler1_Command" 
            EventClickHandling="JavaScript"
    EventClickJavaScript="modal().showUrl('Edit.aspx?id=' + e.id());"
            TimeRangeSelectedHandling="JavaScript"
         TimeRangeSelectedJavaScript="modal().showUrl('New.aspx?start=' + start + '&amp;end=' + end + '&amp;id=' + resource);"
           EventMoveHandling="CallBack" 
  OnEventMove="DayPilotScheduler1_EventMove" 
     DataStartField="fecha_inicial" 
  DataEndField="fecha_final" 
  DataTextField="descripcion_servicio" 
  DataValueField="id_servicio_cliente" 
  DataResourceField="id_servicio" 
  DataTagFields="id_servicio"
        
       HeaderDateFormat="dd/mm" TreeAutoExpand="False">
   
</DayPilot:DayPilotScheduler>  
 </div>

    <DayPilot:DayPilotCalendar 
    ID="DayPilotCalendar1" 
    runat="server" 
    ClientIDMode="Static"
    ViewType="Week"
    OnCommand="DayPilotCalendar1_Command"
    TimeRangeSelectedHandling="JavaScript"
    TimeRangeSelectedJavaScript="modal().showUrl('New.aspx?start=' + start + '&amp;end=' + end + '&amp;id=' + resource);"
    EventClickHandling="JavaScript"
    EventClickJavaScript="modal().showUrl('Edit.aspx?id=' + e.id());"
    OnBeforeEventRender="DayPilotCalendar1_OnBeforeEventRender"
    />

<h2>Print</h2>

<div class="space">
<asp:Button runat="server" ID="PrintButton" Text="Print" 
        onclick="PrintButton_Click" />
</div>

</asp:Content>