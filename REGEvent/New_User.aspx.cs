
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Web.UI.WebControls;
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
            RadioButtonList1.SelectedValue = "Agregar";
        }
        if (RadioButtonList1.SelectedValue == "Agregar")
        {
            eliminar.Visible = false;
            passs.Visible = true;
            agregar.Visible = true;
            Label2.Visible = true;
            Label3.Visible = true;
            DropDownList1.Visible = true;
            titulo.InnerText = "Agregar Usuario";

        }

        if (RadioButtonList1.SelectedValue == "Eliminar")
        {
            agregar.Visible = false;
            passs.Visible = false;
            Label2.Visible = false;
            Label3.Visible = false;
            eliminar.Visible = true;
            DropDownList1.Visible = false;
            titulo.InnerText = "Eliminar Usuario";
        }

    }
    public DataTable ObtieneUsuarios()
    {
        SqlDataAdapter da = new SqlDataAdapter("select a.NICKNAME, b.descripcion ROL   from usuario a join rol b on a.id_rol = b.id_rol where a.id_estado = 1", ConfigurationManager.ConnectionStrings["daypilot"].ConnectionString);

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
        string nick, pass;

        us.nickname = TextBox1.Text;
        us.password = passs.Text;

         nick = TextBox1.Text;
         pass = passs.Text;


        us.rol = Int32.Parse(DropDownList1.SelectedValue.ToString());
        string resultado;
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["daypilot"].ConnectionString))
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("GuardaUsuario", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("user", nick);
            cmd.Parameters.AddWithValue("pass", pass);
            cmd.Parameters.AddWithValue("rol", us.rol);
            resultado = (string)cmd.ExecuteScalar();

            if (resultado == "OK")
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(),
                 "Alert", @"alert('Usuario Creado correctamente')", true);
                   LimpiaText();
            }
            else

            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(),
  "Alert", @"alert('Error al crear usuario')", true);
                      LimpiaText();
                return;
            }

        }

}


public void LimpiaText()
    {
        TextBox1.Text = null;
        passs.Text = null;
        TextBox1.Focus();
    }


    public void Unnamed1_Click(object sender, EventArgs e)
    {
        GuardaUsuario();
        LimpiaText();
        GridView1.DataSource = ObtieneUsuarios();
        GridView1.DataBind();
    }

    protected void Unnamed2_Click(object sender, EventArgs e)
    {
        EliminaUsuario();
        GridView1.DataSource = ObtieneUsuarios();
        GridView1.DataBind();
    }

    public void EliminaUsuario()
    {

       using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["daypilot"].ConnectionString))
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("EliminaUsuario", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("user", TextBox1.Text);
            string resultado;
            resultado = (string)cmd.ExecuteScalar();

            if (resultado == "OK")
            {

                ScriptManager.RegisterClientScriptBlock(this, GetType(),
                    "Alert", @"alert('Usuario eliminado exitosamente')", true);
                GridView1.DataSource = ObtieneUsuarios();
                GridView1.DataBind();

            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(),
                "Alert", @"alert('Error al eliminar usuario! - N/A')", true);

            }


        }
    }
}
