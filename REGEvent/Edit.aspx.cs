using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;

public partial class Edit : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        if (!IsPostBack)
        {

            //Si la sesión es nula, redirige a login.
            if (System.Web.HttpContext.Current.Session["user"] == null)
            {

                Response.Redirect("Login.aspx");
            };

            DataRow dr = dbGetEvent(Request.QueryString["id"]);

            if (dr == null)
            {
                throw new Exception("El evento seleccionado no existe");
            }

            //asigna valores iniciales

            fecha_inicial.Text = Convert.ToDateTime(dr["fecha_inicial"]).ToString("M/d/yyyy HH:mm");
            fecha_final.Text = Convert.ToDateTime(dr["fecha_final"]).ToString("M/d/yyyy HH:mm");
            descripcion.Text = (string)dr["descripcion"];
            nombre_cliente.Text = dr["nombre"].ToString();
            total.Text = dr["total"].ToString();
            subtotal.Text = dr["subtotal"].ToString();
            servicio.Text = dr["servicio"].ToString();
            cantidad.Text = (Convert.ToDateTime(fecha_final.Text) - Convert.ToDateTime(fecha_inicial.Text)).Hours.ToString();




        }

        if (cantidad.Text.Length > 0)
        {
            fecha_final.Text = Convert.ToDateTime(fecha_inicial.Text).AddHours(Double.Parse(cantidad.Text)).ToString("M/d/yyyy HH:mm");
            total.Text = (float.Parse(subtotal.Text) * int.Parse(cantidad.Text)).ToString();
        }
    }
    //boton que actualiza los datos del evento
    protected void ButtonOK_Click(object sender, EventArgs e)
    {


        Model.ServicioCliente ser = new Model.ServicioCliente();

        ser.fecha_inicial = Convert.ToDateTime(fecha_inicial.Text);
        ser.fecha_final = Convert.ToDateTime(fecha_final.Text);
        ser.descripcion = descripcion.Text;
        ser.id_servicio_cliente = Convert.ToInt32(Request.QueryString["id"]);
        ser.total = float.Parse(total.Text);
        ser.subtotal = float.Parse(subtotal.Text);
        dbUpdateEvent(ser);
        Modal.Close(this, "OK");
    }


    //Obtiene evento de base de datos
    private DataRow dbGetEvent(string id)
    {
        SqlDataAdapter da = new SqlDataAdapter("SELECT a.*, b.nombre, c.descripcion servicio FROM servicio_cliente a join cliente b on a.id_cliente= b.id_cliente join servicio c on c.id_servicio = a.id_servicio WHERE id_servicio_cliente = @id and a.id_estado = 1 ", ConfigurationManager.ConnectionStrings["daypilot"].ConnectionString);
        da.SelectCommand.Parameters.AddWithValue("id", id);
        DataTable dt = new DataTable();
        da.Fill(dt);

        if (dt.Rows.Count > 0)
        {
            return dt.Rows[0];
        }
        return null;
    }

    //actualiza evento en base de datos
    private void dbUpdateEvent(Model.ServicioCliente ser)
    {
      

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["daypilot"].ConnectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("UPDATE [servicio_cliente] SET fecha_inicial = @fecha_inicial, fecha_final = @fecha_final, descripcion = @descripcion, total = @total,  subtotal = @subtotal WHERE id_servicio_cliente = @id_servicio_cliente", con);
                cmd.Parameters.AddWithValue("id_servicio_cliente", ser.id_servicio_cliente);
                cmd.Parameters.AddWithValue("fecha_inicial", ser.fecha_inicial);
                cmd.Parameters.AddWithValue("fecha_final", ser.fecha_final);
                cmd.Parameters.AddWithValue("descripcion", ser.descripcion);
                cmd.Parameters.AddWithValue("total", ser.total);
                cmd.Parameters.AddWithValue("subtotal", ser.subtotal);
                cmd.ExecuteNonQuery();
            }
 
      
    }

    //Elimina Evento de base de datos
    private void dbDeleteEvent(string id)
    {
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["daypilot"].ConnectionString))
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("update servicio_cliente set id_estado = 0  WHERE id_servicio_cliente = @id", con);
            cmd.Parameters.AddWithValue("id", id);
            cmd.ExecuteNonQuery();
        }
    }


    //Cancela evento en base de datos
    private void dbCancelEvent(string id)
    {
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["daypilot"].ConnectionString))
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("update servicio_cliente set id_estado = 2  WHERE id_servicio_cliente = @id", con);
            cmd.Parameters.AddWithValue("id", id);
            cmd.ExecuteNonQuery();
        }
    }

    //boton de cancelar, cierra el modal.
    protected void ButtonCancel_Click(object sender, EventArgs e)
    {
        Modal.Close(this);
    }

    //evento al dar click en buton delete
    protected void LinkButtonDelete_Click(object sender, EventArgs e)
    {
        string id = Request.QueryString["id"];
        dbDeleteEvent(id);
        Modal.Close(this, "OK");
    }

    //evento al dar click en buton cancel
    protected void LinkButtonCancel_Click(object sender, EventArgs e)
    {
        string id = Request.QueryString["id"];
        dbCancelEvent(id);
        Modal.Close(this, "OK");
    }

   
}
