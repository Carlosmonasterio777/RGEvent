<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="Report.aspx.cs" Inherits="_Default" MasterPageFile="~/Site.master" Title="Reportes" %>
<%@ Register Assembly="DayPilot" Namespace="DayPilot.Web.Ui" TagPrefix="DayPilot" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <script type="text/javascript" src="js/modal.js"></script>
	<script type="text/javascript" src="js/jquery-1.9.1.min.js"></script>
    <link href='css/main.css' type="text/css" rel="stylesheet" /> 
         <script type="text/javascript" src="https://www.gstatic.com/charts/loader.js"></script> 
</asp:Content>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <script type="text/javascript">
        google.charts.load('current', { 'packages': ['corechart'] });
        google.charts.setOnLoadCallback(drawVisualization);

        function drawVisualization() {
            // Some raw data (not necessarily accurate)
            var data = google.visualization.arrayToDataTable(<%=GetData(VentaSubTipo())%>);

            var options = {
                title: 'Total ventas por usuario en este mes',
                vAxis: { title: 'Total' },
                hAxis: { title: 'Usuario' },
                seriesType: 'bars',
                series: { 7: { type: 'line' } }
            };

            var chart = new google.visualization.ComboChart(document.getElementById('Subtipo'));
            chart.draw(data, options);
        }
    </script>
    <table style="width: 100%; border:1px   dashed;text-align:center; ">
          <tr>
            <td colspan="3" rowspan="6"> <div style="width: 900px; height:500px; align-items:center" id="Subtipo" ></div></td>
            <td colspan="1" rowspan="1"><div style="font-size:24px;">Total de ventas en el mes  </div>  </td>
        </tr>
         <tr>
        
            <td>  <div style="font-size:20px; color:red; border:1px solid;" id="mes" runat="server"> </div></td>
        </tr>
         <tr>
      
            <td>  <div style="font-size:24px;">Total de ventas en la semana  </div></td>
        </tr>
        <tr>
      <td> <div style="font-size:20px; color:red; border:1px solid; " id="semana" runat="server">  </div></td>
        </tr>
         <tr>
    
            <td> <div style="font-size:24px;">Total de ventas en el dia  </div></td>
        </tr>
        <tr>
    
            <td>  <div style="font-size:20px; color:red;border:1px solid; " id="dia" runat="server">  </div></td>
        </tr>
            


    </table>


</asp:Content>