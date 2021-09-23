using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;


public partial class New : Page
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

            //asigna valores iniciales
            fecha_inicial.Text = Convert.ToDateTime(Request.QueryString["start"]).ToString("M/d/yyyy HH:mm");
            id_servicio.Text = Request.QueryString["id"].ToString();
            precio.Text =  ObtienePrecio(Request.QueryString["id"].ToString()).ToString();
            cantidad_hora.Text = "1";
            if (id_servicio.Text== "undefined")
                {
                Modal.Close(this, "OK");
                Response.Write("<script>alert('Seleccione una opcion valida');</script>");
              
            }

            id_usuario.Text = ObtieneUsuario(System.Web.HttpContext.Current.Session["user"].ToString());

            if (id_usuario.Text == "NA")
            {
                Response.Write("<script>alert('No fue posible obtener el usuario');</script>");
                return;
            }
        }
    //asigna la cantidad de horas segun las fechas registradas.
        if (cantidad_hora.Text.Length > 0)
        {
            fecha_final.Text = Convert.ToDateTime(fecha_inicial.Text).AddHours(Double.Parse(cantidad_hora.Text)).ToString("M/d/yyyy HH:mm");
            total.Text = (float.Parse(precio.Text) * int.Parse(cantidad_hora.Text)).ToString();
        }

        //valida que exista el dpi ingresado
        if (DPI.Text.Length > 0)
        {
            Model.cliente cli = new Model.cliente();
            cli = ObtieneCliente(DPI.Text);

            id_cliente.Text = cli.dpi;
            Nombre.Text = cli.nombre;
           
            if (id_cliente.Text == "NA")
                {
                        Modal.Close(this, "OK");
                        Response.Write("<script>alert('Cliente No Existe por favor crearlo');</script>");
                    
                }
        }
        

    }
    protected void ButtonOK_Click(object sender, EventArgs e)
    {
        fecha_final.Text = Convert.ToDateTime(fecha_inicial.Text).AddHours(Double.Parse(cantidad_hora.Text)).ToString("M/d/yyyy HH:mm");

        Model.ServicioCliente ser = new Model.ServicioCliente();

        ser.fecha_inicial = Convert.ToDateTime(fecha_inicial.Text);
        ser.fecha_final = Convert.ToDateTime(fecha_final.Text);
        ser.descripcion = descripcion.Text;
        ser.id_cliente = Convert.ToInt32(id_cliente.Text);
        ser.id_servicio = Convert.ToInt32(id_servicio.Text);
        ser.subtotal = float.Parse(precio.Text);
        ser.total = float.Parse(total.Text);
        ser.id_usuario = Convert.ToInt32(id_usuario.Text);
 
        dbInsertEvent(ser);

 
        Modal.Close(this, "OK");
    }
    //Inserta a los nuevos clientes a base de datos.
    private void dbInsertEvent(Model.ServicioCliente ServicioNuevo)
    {


        if (ValidaEventosInsertados(ServicioNuevo.fecha_inicial, ServicioNuevo.fecha_final, ServicioNuevo.id_servicio) == "OK")
        {


            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["daypilot"].ConnectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO [servicio_cliente] (id_cliente, id_servicio, fecha_inicial, fecha_final, descripcion, subtotal, total, id_usuario,id_estado,fecha_ingreso) VALUES(@id_cliente, @id_servicio, @fecha_inicial, @fecha_final, @descripcion, @subtotal, @total, @id_usuario,1,getdate())", con);
                //cmd.Parameters.AddWithValue("id", id);
                cmd.Parameters.AddWithValue("id_cliente", ServicioNuevo.id_cliente);
                cmd.Parameters.AddWithValue("id_servicio", ServicioNuevo.id_servicio);
                cmd.Parameters.AddWithValue("fecha_inicial", ServicioNuevo.fecha_inicial);
                cmd.Parameters.AddWithValue("fecha_final", ServicioNuevo.fecha_final);
                cmd.Parameters.AddWithValue("descripcion", ServicioNuevo.descripcion);
                cmd.Parameters.AddWithValue("subtotal", ServicioNuevo.subtotal);
                cmd.Parameters.AddWithValue("total", ServicioNuevo.total);
                cmd.Parameters.AddWithValue("id_usuario", ServicioNuevo.id_usuario);

                cmd.ExecuteNonQuery();
            }
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, GetType(),
              "Alert", @"alert('No se puede ingresar el evento, ya existe un evento de este tipo registrado en ese rango de  fechas')", true);

        }
        
    }

    protected void ButtonCancel_Click(object sender, EventArgs e)
    {
        Modal.Close(this);
    }

    //Obtiene precio del servicio elegido
    private float ObtienePrecio(string id)
    {
    
            SqlDataAdapter da = new SqlDataAdapter("select top 1 precio from precio_servicio WHERE  id_estado =1 and id_servicio=@id", ConfigurationManager.ConnectionStrings["daypilot"].ConnectionString);
            da.SelectCommand.Parameters.AddWithValue("id", id);
            DataTable dt = new DataTable();
            da.Fill(dt);
            float precio = float.Parse(dt.Rows[0][0].ToString());

            return precio;
 
        
    }

    //Valida disponibilidad de las fechas para poder registrar nuevos servicios.
    public string ValidaEventosInsertados(DateTime f_inicial, DateTime f_final, int id_servicio)
    {
       // f_final = f_final.AddMinutes(-1);
        //f_inicial = f_inicial.AddMinutes(+1);

        string resultado = null;
      using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["daypilot"].ConnectionString))
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("ValidaDisponibilidad", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("f_final", f_final);
            cmd.Parameters.AddWithValue("f_inicial", f_inicial);
            cmd.Parameters.AddWithValue("id_servicio", id_servicio);
            resultado = (string)cmd.ExecuteScalar();

          
        }

        return resultado;

    }

    //Obtiene listado de clientes para llenar grid
    private Model.cliente ObtieneCliente(string dpi)
    {
        SqlDataAdapter da = new SqlDataAdapter("select top 1 id_cliente, nombre from cliente WHERE   dpi=@dpi and id_estado = 1 ", ConfigurationManager.ConnectionStrings["daypilot"].ConnectionString);
        da.SelectCommand.Parameters.AddWithValue("dpi", dpi);
        DataTable dt = new DataTable();
        da.Fill(dt);
        Model.cliente cli = new Model.cliente();
        
        if (dt.Rows.Count>0)
            {
                cli.dpi = dt.Rows[0][0].ToString();
                 cli.nombre = dt.Rows[0][1].ToString();
             }
        else
        {
            cli.dpi = "NA";
            cli.nombre = "NA";

        }


        return cli;
    }

    //Obtiene usuario para registrar su id en el insert de nuevos clientes
    private string ObtieneUsuario(string nickname)
    {
        SqlDataAdapter da = new SqlDataAdapter("select top 1 id_usuario from usuario WHERE   nickname=@nickname and id_estado = 1 ", ConfigurationManager.ConnectionStrings["daypilot"].ConnectionString);
        da.SelectCommand.Parameters.AddWithValue("nickname", nickname);
        DataTable dt = new DataTable();
        da.Fill(dt);
        string v_user;
        if (dt.Rows.Count > 0)
        {
            v_user = dt.Rows[0][0].ToString();
        }
        else
        {
            v_user = "NA";

        }
        
        return v_user;
    }
}
