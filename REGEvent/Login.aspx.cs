using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class Login_ : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
        /*
            DropDownList1.DataTextField = "descripcion";
            DropDownList1.DataValueField = "id_rol";
             DropDownList1.DataSource = ObtieneRol();
            DropDownList1.DataBind();*/
        }
         
    }

   /* public DataTable ObtieneRol ()
    {
        SqlDataAdapter da = new SqlDataAdapter("select * from rol where id_estado  = 1", ConfigurationManager.ConnectionStrings["daypilot"].ConnectionString);

        DataTable dt = new DataTable();
        da.Fill(dt);

        return dt;
    }
   */
    public void ValidaUsuario()
    {
        Model.user us = new Model.user();

        us.nickname = TextBox1.Text;
        us.password = TextBox2.Text;
        string resultado; 
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["daypilot"].ConnectionString))
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("ValidaUsuario", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("user", us.nickname);
            cmd.Parameters.AddWithValue("pass", us.password);
            resultado =  (string)cmd.ExecuteScalar();

            if (resultado == "OK")
            {
                Session["user"] = TextBox1.Text;

                Response.Redirect("default.aspx");
            }
            else
                ScriptManager.RegisterClientScriptBlock(this, GetType(),
"Alert", @"alert('Usuario no registrado')", true);
            Session["user"] = null;
            LimpiaText();
            return;

              

        }

    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        ValidaUsuario();
    }

    public void LimpiaText ()
    {
        TextBox1.Text = null;
        TextBox2.Text = null;
        TextBox1.Focus();
    }
}