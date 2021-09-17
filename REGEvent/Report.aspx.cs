
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI;
using System.Globalization;

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

            mes.InnerText = "Q." +  ObtieneTotalMes().ToString("0,0", CultureInfo.CurrentCulture);
            semana.InnerText = "Q." + ObtieneTotalSemana().ToString("0,0", CultureInfo.CurrentCulture);
            dia.InnerText = "Q." + ObtieneTotalDia().ToString("0,0", CultureInfo.CurrentCulture);

        }

    }

    public DataTable VentaSubTipo ()
    {
        SqlDataAdapter da = new SqlDataAdapter("select * from ( select  isnull(a.total,0) total , u.nickname dsc, c.descripcion  from servicio_cliente a  join servicio b on a.id_servicio = b.id_servicio  join sub_tipo_servicio c on b.id_sub_tipo_servicio = c.id_sub_tipo_servicio join usuario u on u.id_usuario = a.id_usuario where fecha_inicial > getdate()-180 ) a pivot( count(total) for descripcion in (\"Inmobiliario\", \"Mobiliario\", \"Servicio de Banqueteria\", \"Pagos\")) as b ", ConfigurationManager.ConnectionStrings["daypilot"].ConnectionString);

        DataTable dt = new DataTable();
        da.Fill(dt);

        return dt;
    }

    public string GetData(DataTable dt)
    {
        DataTable dts = new DataTable();

        
        dts = dt;
        string Datos = "[['Dsc','Inmobiliario','Mobiliario','Servicio de Banqueteria','Pagos'],";

        foreach (DataRow dr in dts.Rows)
        {

            Datos = Datos + "[";
            Datos = Datos + "'" + dr[0] + "'" + "," + dr[1] + "," + dr[2] + "," + dr[3] + "," + dr[4];
            if (dr == dts.Rows[dts.Rows.Count - 1])
            {
                Datos = Datos + "]";
            }
            else
            {
                Datos = Datos + "],";
            }
        }

        Datos = Datos + "]";
        return Datos;

    }

    public double  ObtieneTotalMes()
    {
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["daypilot"].ConnectionString))
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("select sum(total) from servicio_cliente where id_estado = 1 and  month(fecha_ingreso) = month(getdate()) ", con);
            cmd.CommandType = CommandType.Text;
            double resultado;
            resultado = (double)cmd.ExecuteScalar();

      
            return resultado;


        }
    }
    public double ObtieneTotalSemana()
    {
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["daypilot"].ConnectionString))
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("select sum(total) from servicio_cliente where id_estado = 1 and datepart(wk,fecha_ingreso) = datepart(wk,getdate()) ", con);
            cmd.CommandType = CommandType.Text;
            double resultado;
            resultado = (double)cmd.ExecuteScalar();


            return resultado;


        }
    }

    public double ObtieneTotalDia()
    {
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["daypilot"].ConnectionString))
        {
            con.Open();
            
            SqlCommand cmd = new SqlCommand("select sum(total) from servicio_cliente where id_estado = 1 and day(fecha_ingreso) = day(getdate()) ", con);
            cmd.CommandType = CommandType.Text;
            double resultado;
            resultado = (double)cmd.ExecuteScalar();


            return resultado;


        }
    }

}
