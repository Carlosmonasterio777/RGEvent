
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
using System.Web.UI;

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
 
             
                SetDataSourceAndBind();
              
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

        string actualiza = dbUpdateEvent(id, start, end, resource);

        if (actualiza =="OK")
        {


            SetDataSourceAndBind();
            DayPilotScheduler1.UpdateWithMessage("Evento Actualizado");
        }
        else

        {
            SetDataSourceAndBind();
            DayPilotScheduler1.UpdateWithMessage("No se puede actualizar el evento, ya existe un evento de este tipo registrado en ese rango de  fechas");
        }
    }

    private string dbUpdateEvent(string id, DateTime start, DateTime end, string resource)
    {
        string resultado = null;
        if (ValidaEventosInsertados(start, end, Int32.Parse(resource)).Contains("OK"))
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
            resultado = "OK";
        }
        else
        {
          
            resultado = "NA";
        }

        return resultado;
    }
    public string ValidaEventosInsertados(DateTime f_inicial , DateTime f_final , int id_servicio)
    {

        int resultado;
        string res, fi, ff;

        fi = f_inicial.ToString("MM/dd/yyyy HH:mm:ss");
        ff = f_final.ToString("MM/dd/yyyy HH:mm:ss");
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["daypilot"].ConnectionString))
        {

            DateTime dt = DateTime.Parse(f_inicial.ToString("MM/dd/yyyy hh:mm:ss"));
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT  COUNT(1) FROM servicio_cliente  where((dateadd(minute,1,fecha_inicial) between @f_inicial and @f_final) or (dateadd(minute,-1,fecha_final) between @f_inicial and @f_final)) and id_servicio = @id_servicio  and id_estado = 1 ", con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("f_final", ff);
            cmd.Parameters.AddWithValue("f_inicial", fi);
            cmd.Parameters.AddWithValue("id_servicio", id_servicio);
            resultado = (int)cmd.ExecuteScalar();


        }

        if (resultado >= 1)
        {
            res = "NA";
        }
        else
        {
            res=  "OK";
        }


        return res;

    }



    private void SetDataSourceAndBind()
    {

        DayPilotScheduler1.DataTagFields = "id_servicio";
        DayPilotScheduler1.DataStartField = "fecha_inicial";
        DayPilotScheduler1.DataEndField = "fecha_final";
        DayPilotScheduler1.DataIdField = "id_servicio_cliente";
        DayPilotScheduler1.DataTextField = "descripcion";
        DayPilotScheduler1.DataResourceField = "id_servicio";
        DayPilotScheduler1.DataSource = GetData(DayPilotScheduler1.StartDate , DayPilotScheduler1.EndDate);
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



                if (e.End < DateTime.Now) 
                {
                    e.DurationBarColor = "red";
                    e.ToolTip = "Evento Finalizado";
                }
                else
                {
                    e.DurationBarColor = "green";
                    e.ToolTip = "Evento Vigente";
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
         
            Resource r = new Resource((string)subtype["descripcion"], null);  
            r.IsParent = true;  
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
