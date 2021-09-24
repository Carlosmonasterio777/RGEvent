
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Web.UI.WebControls;
using DayPilot.Utils;
using DayPilot.Web.Ui;
using DayPilot.Web.Ui.Data;
using DayPilot.Web.Ui.Enums;
using DayPilot.Web.Ui.Events.Calendar;
using DayPilot.Web.Ui.Events.Scheduler;
using BeforeEventRenderEventArgs = DayPilot.Web.Ui.Events.Calendar.BeforeEventRenderEventArgs;
using CommandEventArgs = DayPilot.Web.Ui.Events.CommandEventArgs;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //Si la sesión es nula, redirige a login.
            if (System.Web.HttpContext.Current.Session["user"] == null)
            {

                Response.Redirect("Login.aspx");
            };
            
          SetDataSourceAndBind();

        }

    }
    
    
 //Actualiza formato de fecha
    protected void DayPilotCalendar1_BeforeHeaderRender(object sender, BeforeHeaderRenderEventArgs e)
    {
        e.Html = e.Date.ToString("dd/MM/yyyy");
    }
    //Llena source
    private void SetDataSourceAndBind()
    {
        DayPilotCalendar1.DataSource = GetData(DayPilotCalendar1.StartDate, DayPilotCalendar1.EndDate);
        DayPilotCalendar1.DataStartField = "fecha_inicial";
        DayPilotCalendar1.DataEndField = "fecha_final";
        DayPilotCalendar1.DataIdField = "id_servicio_cliente";
        DayPilotCalendar1.DataTextField = "descripcion";
        DayPilotCalendar1.ToolTip = "id_servicio";
        DayPilotCalendar1.DataBind();

    }
    //Obtiene eventos 
    private DataTable GetData(DateTime start, DateTime end)
    {
        SqlDataAdapter da = new SqlDataAdapter("select a.id_servicio, a.descripcion, a.id_servicio_cliente, a.fecha_inicial, a.fecha_final , b.descripcion descripcion_servicio from servicio_cliente a join servicio b on a.id_servicio = b.id_servicio  WHERE a.id_estado =1 and  NOT (([fecha_final] <= @start) OR ([fecha_inicial] >= @end))", ConfigurationManager.ConnectionStrings["daypilot"].ConnectionString);
        da.SelectCommand.Parameters.AddWithValue("start", start);
        da.SelectCommand.Parameters.AddWithValue("end", end);

        DataTable dt = new DataTable();
        da.Fill(dt);

        return dt;
    }
    
    //Actualiza calendario
    protected void DayPilotCalendar1_Command(object sender, CommandEventArgs e)
    {
        switch (e.Command)
        {
            case "refresh":
                SetDataSourceAndBind();
                DayPilotCalendar1.Update();
                break;

            case "navigate":
                DayPilotCalendar1.StartDate = (DateTime)e.Data["day"];
                DayPilotCalendar1.DataSource = GetData(DayPilotCalendar1.StartDate, DayPilotCalendar1.EndDate.AddDays(7));
                DayPilotCalendar1.DataBind();
                DayPilotCalendar1.Update();
                break;

        }
    }
  

    //Actualiza estado de calendario
    protected void DayPilotCalendar1_OnBeforeEventRender(object sender, BeforeEventRenderEventArgs e)
    {
  

    
        if (e.End<DateTime.Now) // must be confirmed two day in advance
        {
            e.DurationBarColor = "red";
            e.ToolTip = "Evento Finalizado";
        }
        else
{
    e.DurationBarColor = "green";
    e.ToolTip = "Evento Vigente";
}

e.Html = String.Format("<div>{0} ({1:d} - {2:d})<br /><span style='color:gray'>{3}</span></div>", e.Text, e.Start.ToString("dd/MM/yyyy"), e.End.ToString("dd/MM/yyyy"), e.ToolTip);

    }

    //Obtiene Recursos
    private DataTable GetResources(int subtype)
    {
        SqlDataAdapter da = new SqlDataAdapter("select id_servicio, descripcion from servicio where id_sub_tipo_servicio ="+ subtype, ConfigurationManager.ConnectionStrings["daypilot"].ConnectionString);

        DataTable dt = new DataTable();
        da.Fill(dt);

        return dt;
    }





    //Obtiene recursos por subtype
    private DataTable GetResourcesSubType()
    {
        SqlDataAdapter da = new SqlDataAdapter("select id_sub_tipo_servicio, descripcion from sub_tipo_servicio where id_estado = 1 ", ConfigurationManager.ConnectionStrings["daypilot"].ConnectionString);

        DataTable dt = new DataTable();
        da.Fill(dt);

        return dt;
    }

   

   
}
