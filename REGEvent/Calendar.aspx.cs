
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
            //Si la sesi�n es nula, redirige a login.
            if (System.Web.HttpContext.Current.Session["user"] == null)
            {

                Response.Redirect("Login.aspx");
            };
            
          SetDataSourceAndBind();

        }

    }
    
    
  /*  private void dbUpdateEvent(string id, DateTime start, DateTime end, string resource)
    {
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["daypilot"].ConnectionString))
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("UPDATE [servicio_cliente] SET fecha_inicial = @fecha_inicial, fecha_final = @fecha_final, id_servicio =@id_servicio  WHERE id_servicio_cliente = @id_servicio_cliente  ", con);
            cmd.Parameters.AddWithValue("id_servicio_cliente", Int32.Parse(id));
            cmd.Parameters.AddWithValue("fecha_inicial", start);
            cmd.Parameters.AddWithValue("fecha_final", end);
            cmd.Parameters.AddWithValue("id_servicio", Int32.Parse(resource));
            cmd.ExecuteNonQuery();
        }
    }*/

    protected void DayPilotCalendar1_BeforeHeaderRender(object sender, BeforeHeaderRenderEventArgs e)
    {
        e.Html = e.Date.ToString("dd/MM/yyyy");
    }

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

    private DataTable GetData(DateTime start, DateTime end)
    {
        SqlDataAdapter da = new SqlDataAdapter("select a.id_servicio, a.descripcion, a.id_servicio_cliente, a.fecha_inicial, a.fecha_final , b.descripcion descripcion_servicio from servicio_cliente a join servicio b on a.id_servicio = b.id_servicio  WHERE a.id_estado =1 and  NOT (([fecha_final] <= @start) OR ([fecha_inicial] >= @end))", ConfigurationManager.ConnectionStrings["daypilot"].ConnectionString);
        da.SelectCommand.Parameters.AddWithValue("start", start);
        da.SelectCommand.Parameters.AddWithValue("end", end);

        DataTable dt = new DataTable();
        da.Fill(dt);

        return dt;
    }
    /*
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
    */
   /* protected void PrintButton_Click(object sender, EventArgs e)
    {
        DateTime start = DayPilotCalendar1.StartDate;
        int scroll = DayPilotCalendar1.ScrollY;
        Response.Redirect("Print.aspx?start=" + start.ToString("s") + "&scroll=" + scroll);
    }
   */

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

    private DataTable GetResources(int subtype)
    {
        SqlDataAdapter da = new SqlDataAdapter("select id_servicio, descripcion from servicio where id_sub_tipo_servicio ="+ subtype, ConfigurationManager.ConnectionStrings["daypilot"].ConnectionString);

        DataTable dt = new DataTable();
        da.Fill(dt);

        return dt;
    }






    private DataTable GetResourcesSubType()
    {
        SqlDataAdapter da = new SqlDataAdapter("select id_sub_tipo_servicio, descripcion from sub_tipo_servicio where id_estado = 1 ", ConfigurationManager.ConnectionStrings["daypilot"].ConnectionString);

        DataTable dt = new DataTable();
        da.Fill(dt);

        return dt;
    }

   

   
}
