
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
            DropDownList1.DataTextField = "descripcion";
            DropDownList1.DataValueField = "id_rol";
            DropDownList1.DataSource = ObtieneRol();
            DropDownList1.DataBind();

            GridView1.DataSource = ObtieneUsuarios();
            GridView1.DataBind();

        }

    }
    public DataTable ObtieneUsuarios()
    {
        SqlDataAdapter da = new SqlDataAdapter("select a.NICKNAME, b.descripcion ROL   from usuario a join rol b on a.id_rol = b.id_rol", ConfigurationManager.ConnectionStrings["daypilot"].ConnectionString);

        DataTable dt = new DataTable();
        da.Fill(dt);

        return dt;
    }

    public DataTable ObtieneRol ()
    {
        SqlDataAdapter da = new SqlDataAdapter("select * from rol where id_estado  = 1", ConfigurationManager.ConnectionStrings["daypilot"].ConnectionString);

        DataTable dt = new DataTable();
        da.Fill(dt);

        return dt;
    }

    public void GuardaUsuario()
    {
        Model.user us = new Model.user();

        us.nickname = TextBox1.Text;
        us.password = TextBox2.Text;
        us.rol = Int32.Parse(DropDownList1.SelectedValue.ToString());
        string resultado;
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["daypilot"].ConnectionString))
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("GuardaUsuario", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("user", us.nickname);
            cmd.Parameters.AddWithValue("pass", us.password);
            cmd.Parameters.AddWithValue("rol", us.rol);
            resultado = (string)cmd.ExecuteScalar();

            if (resultado == "OK")
            {

                ScriptManager.RegisterClientScriptBlock(this, GetType(),
                    "Alert", @"alert('Usuario registrado exitosamente')", true);
                GridView1.DataSource = ObtieneUsuarios();
                GridView1.DataBind();

            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(),
                "Alert", @"alert('Error al crear usuario: El usuario ya existe!')", true);

            }
        }
    }


    public void LimpiaText()
    {
        TextBox1.Text = null;
        TextBox2.Text = null;
        TextBox1.Focus();
    }


    protected void Unnamed1_Click(object sender, EventArgs e)
    {
        GuardaUsuario();
        LimpiaText();
    }
}
