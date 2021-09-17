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
  

            GridView1.DataSource = ObtieneClientes();
            GridView1.DataBind();

            RadioButtonList1.SelectedValue = "Agregar";


        }

        

        if (RadioButtonList1.SelectedValue == "Agregar")
        {
            eliminar.Visible = false;
            TextBox3.Visible = true;
            TextBox2.Visible = true;
            agregar.Visible = true;
            Label2.Visible = true;
            Label3.Visible = true;
            titulo.InnerText = "Agregar Cliente";
           // LimpiaText();
        }

        if (RadioButtonList1.SelectedValue == "Eliminar")
        {
            agregar.Visible = false;
            TextBox2.Visible = false;
            TextBox3.Visible = false;
            Label2.Visible = false;
            Label3.Visible = false;
            eliminar.Visible = true;
            titulo.InnerText = "Eliminar Cliente";
           // LimpiaText();
        }

    }
    public DataTable ObtieneClientes()
    {
        SqlDataAdapter da = new SqlDataAdapter("select  dpi DPI, nombre NOMBRE , telefono TELEFONO from cliente where id_estado = 1", ConfigurationManager.ConnectionStrings["daypilot"].ConnectionString);

        DataTable dt = new DataTable();
        da.Fill(dt);

        return dt;
    }

    public void GuardaClientes()
    {
        Model.cliente cli = new Model.cliente();

        cli.dpi = TextBox1.Text;
        cli.nombre = TextBox2.Text;
        cli.telefono = TextBox3.Text;
        string resultado;
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["daypilot"].ConnectionString))
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("GuardaCliente", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("dpi", cli.dpi);
            cmd.Parameters.AddWithValue("nombre", cli.nombre);
            cmd.Parameters.AddWithValue("telefono", cli.telefono);
            resultado = (string)cmd.ExecuteScalar();

            if (resultado == "OK")
            {

                ScriptManager.RegisterClientScriptBlock(this, GetType(),
                    "Alert", @"alert('Cliente registrado exitosamente')", true);
                GridView1.DataSource = ObtieneClientes();
                GridView1.DataBind();

            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(),
                "Alert", @"alert('Error al crear cliente: El DPI ingresado ya esta registrado')", true);

            }
        }
    }


    public void LimpiaText()
    {
        TextBox1.Text = null;
        TextBox2.Text = null;
        TextBox3.Text = null;
        TextBox1.Focus();
  
    }


    protected void Unnamed1_Click(object sender, EventArgs e)
    {

        if (ValidaTextbox(1))
        {


            GuardaClientes();
            LimpiaText();
        }
    }

    protected void Unnamed2_Click(object sender, EventArgs e)
    {
      if  (ValidaTextbox(2))
        {
            EliminaCliente();
            LimpiaText();
        }
            

    
    }

    public void EliminaCliente()
    {
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["daypilot"].ConnectionString))
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("EliminaCliente", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("DPI", TextBox1.Text);
            string resultado;
            resultado = (string)cmd.ExecuteScalar();

            if (resultado == "OK")
            {

                ScriptManager.RegisterClientScriptBlock(this, GetType(),
                    "Alert", @"alert('Cliente eliminado exitosamente')", true);
                GridView1.DataSource = ObtieneClientes();
                GridView1.DataBind();

            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(),
                "Alert", @"alert('Error al eliminar Cliente! - N/A')", true);

            }


        }
    }
    public bool ValidaTextbox( int tipo)
    {

        if (tipo == 1)
        {
            if (TextBox1.Text.Length == 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(),
                   "Alert", @"alert('Por favor ingrese el DPI!')", true);

                return false;
            }

            if (TextBox2.Text.Length == 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(),
                   "Alert", @"alert('Por favor ingrese el nombre!')", true);

                return false;
            }

            if (TextBox3.Text.Length == 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(),
                   "Alert", @"alert('Por favor ingrese el telefono!')", true);

                return false;
            }
            else
            {
                return true;
            }
        }
        else  
        {
            if (TextBox1.Text.Length == 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(),
                   "Alert", @"alert('Por favor ingrese el DPI!')", true);

                return false;
            }
            else
            {
                return true;
            }
         
        }
 

    }
}
