
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
using DayPilot.Web.Ui.Events.Scheduler;
using BeforeEventRenderEventArgs = DayPilot.Web.Ui.Events.Calendar.BeforeEventRenderEventArgs;
using CommandEventArgs = DayPilot.Web.Ui.Events.CommandEventArgs;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
              if (System.Web.HttpContext.Current.Session["user"] == null)
            {

                Response.Redirect("Login.aspx");
            };

            //GetResources();
            SetDataSourceAndBind();
            LoadResources();
        }

    }
    protected void DayPilotScheduler1_Command(object sender, DayPilot.Web.Ui.Events.CommandEventArgs e)
    {
        switch (e.Command)
        {
            case "navigate":
                DayPilotScheduler1.StartDate = (DateTime)e.Data["day"];
                DayPilotScheduler1.DataSource = GetData(DayPilotScheduler1.StartDate, DayPilotScheduler1.EndDate.AddDays(1));
                DayPilotScheduler1.DataBind();
                DayPilotScheduler1.Update();
                break;
            case "refresh":
               // DayPilotScheduler1.DataSource = GetData(DayPilotScheduler1.StartDate, DayPilotScheduler1.EndDate.AddDays(1));
                //DayPilotScheduler1.DataBind();
             
                SetDataSourceAndBind();
                //DayPilotScheduler1.DataBind();
                DayPilotScheduler1.Update();
                DayPilotScheduler1.UpdateWithMessage("Eventos actualizados automaticamente");
                break;
            case "sort":
                LoadResources();
                DayPilotScheduler1.DataSource = GetData(DayPilotScheduler1.StartDate, DayPilotScheduler1.EndDate.AddDays(1));
                DayPilotScheduler1.DataBind();
                DayPilotScheduler1.Update();
                break;
        }
    }

    protected void DayPilotScheduler1_OnBeforeResHeaderRender(object sender, BeforeResHeaderRenderEventArgs e)
    {
        // e.DataItem is only available when resources are reloaded from the database using LoadResources()
        if (e.DataItem != null)
        {
            e.Columns[0].Html = "" + e.DataItem["id"];
        }
    }

    protected void DayPilotScheduler1_EventMove(object sender, DayPilot.Web.Ui.Events.EventMoveEventArgs e)
    {
        string id = e.Value;
        DateTime start = e.NewStart;
        DateTime end = e.NewEnd;
        string resource = e.NewResource;

        dbUpdateEvent(id, start, end, resource);

        SetDataSourceAndBind();
        DayPilotScheduler1.UpdateWithMessage("Evento Actualizado");
        //DayPilotCalendar1.Update();
    }

    private void dbUpdateEvent(string id, DateTime start, DateTime end, string resource)
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
    }


    private void SetDataSourceAndBind()
    {
        DayPilotCalendar1.DataSource = GetData(DayPilotCalendar1.StartDate, DayPilotCalendar1.EndDate);
        DayPilotCalendar1.DataStartField = "fecha_inicial";
        DayPilotCalendar1.DataEndField = "fecha_final";
        DayPilotCalendar1.DataIdField = "id_servicio_cliente";
        DayPilotCalendar1.DataTextField = "descripcion";
        DayPilotScheduler1.DataTagFields = "id_servicio";
        DayPilotScheduler1.DataStartField = "fecha_inicial";
        DayPilotScheduler1.DataEndField = "fecha_final";
        DayPilotScheduler1.DataIdField = "id_servicio_cliente";
        DayPilotScheduler1.DataTextField = "descripcion";
        DayPilotScheduler1.DataResourceField = "id_servicio";
        DayPilotCalendar1.ToolTip = "id_servicio";
        DayPilotCalendar1.DataBind();
        DayPilotScheduler1.DataSource = GetData(DayPilotCalendar1.StartDate, DayPilotCalendar1.EndDate);
        DayPilotScheduler1.StartDate = DateTime.Today;
        DayPilotScheduler1.DataBind();
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

    protected void DayPilotCalendar1_Command(object sender, CommandEventArgs e)
    {
        switch (e.Command)
        {
            case "refresh":
                SetDataSourceAndBind();
                DayPilotCalendar1.Update();
                break;

        }
    }

    protected void PrintButton_Click(object sender, EventArgs e)
    {
        DateTime start = DayPilotCalendar1.StartDate;
        int scroll = DayPilotCalendar1.ScrollY;
        Response.Redirect("Print.aspx?start=" + start.ToString("s") + "&scroll=" + scroll);
    }


    protected void DayPilotCalendar1_OnBeforeEventRender(object sender, BeforeEventRenderEventArgs e)
    {
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

    protected void DayPilotScheduler1_BeforeEventRender(object sender, DayPilot.Web.Ui.Events.Scheduler.BeforeEventRenderEventArgs e)
    {

        int status = Convert.ToInt32(e.Tag["id_servicio"]);

        switch (status)
        {
            case 0: // new
                if (e.Start < DateTime.Today.AddDays(2)) // must be confirmed two day in advance
                {
                    e.DurationBarColor = "red";
                    e.ToolTip = "Expired (not confirmed in time)";
                }
                else
                {
                    e.DurationBarColor = "orange";
                    e.ToolTip = "New";
                }
                break;
            case 1:  // confirmed
                if (e.Start < DateTime.Today || (e.Start == DateTime.Today && DateTime.Now.TimeOfDay.Hours > 18))  // must arrive before 6 pm
                {
                    e.DurationBarColor = "#f41616";  // red
                    e.ToolTip = "Late arrival";
                }
                else
                {
                    e.DurationBarColor = "green";
                    e.ToolTip = "Confirmed";
                }
                break;
            case 2: // arrived
                if (e.End < DateTime.Today || (e.End == DateTime.Today && DateTime.Now.TimeOfDay.Hours > 11))  // must checkout before 10 am
                {
                    e.DurationBarColor = "#f41616"; // red
                    e.ToolTip = "Late checkout";
                }
                else
                {
                    e.DurationBarColor = "#1691f4";  // blue
                    e.ToolTip = "Arrived";
                }
                break;
            case 3: // checked out
                e.DurationBarColor = "gray";
                e.ToolTip = "Checked out";
                break;
            default:

                e.DurationBarColor = "green";
                break;
              
                //throw new ArgumentException("Unexpected status.");
        }
        e.Html = String.Format("<div>{0} ({1:d} - {2:d})<br /><span style='color:gray'>{3}</span></div>", e.Text, e.Start, e.End, e.ToolTip);

    }

    private void LoadResources()
    {
        DataTable Resources = new DataTable();
  
        DataTable Subtypes = new DataTable();
        Subtypes = GetResourcesSubType();

        DayPilotScheduler1.Resources.Clear();
        foreach (DataRow subtype in Subtypes.Rows)
        {
            int id = Convert.ToInt32(subtype["id_sub_tipo_servicio"]);
         
            Resource r = new Resource((string)subtype["descripcion"], null); // using null for resources that can't be used
            r.IsParent = true; // marking as parent for the case that no children are loaded
            r.Expanded = true;
            DayPilotScheduler1.Resources.Add(r);


            Resources = GetResources(id);
            foreach (DataRow dr in Resources.Rows)
            {
                Resource c = new Resource((string)dr["descripcion"], Convert.ToString(dr["id_servicio"]));
                r.Children.Add(c);
            }
        }
    }
}
