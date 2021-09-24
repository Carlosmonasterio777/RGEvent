<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="Calendar.aspx.cs" Inherits="_Default" MasterPageFile="~/Site.master" Title="Calendario de eventos" %>
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
            }
        };
        return m;
    }
</script>


    <div style="float:left; width: 150px">
    <DayPilot:DayPilotNavigator 
    ID="DayPilotNavigator1"
    runat="server" 
    ShowMonths="1"
    SkiptMonth="1"
    BoundDayPilotID="DayPilotCalendar1"
    />
 </div>

        <div style="margin-left:220px">
    <DayPilot:DayPilotCalendar 
    ID="DayPilotCalendar1" 
    runat="server" 
    ClientIDMode="Static"
    ViewType="Week" 
    OnBeforeEventRender="DayPilotCalendar1_OnBeforeEventRender"
    OnBeforeHeaderRender="DayPilotCalendar1_BeforeHeaderRender"   
             DataStartField="fecha_inicial" 
          OnCommand="DayPilotCalendar1_Command"
  DataEndField="fecha_final" 
  DataTextField="descripcion_servicio" 
  DataValueField="id_servicio_cliente" 
  DataResourceField="id_servicio" 
  DataTagFields="id_servicio"


    WeekStarts="Monday" />
            </div>



</asp:Content>