
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
        }
       

    }




    public void GuardaUsuario()
    {
        /*Model.user us = new Model.user();

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
        }*/
    }


    public void LimpiaText()
    {
        TextBox1.Text = null;
        TextBox2.Text = null;
        TextBox3.Text = null;
    }


    protected void Unnamed1_Click(object sender, EventArgs e)
    {
        ValidaUsuario();
        LimpiaText();
    }

    protected void Unnamed2_Click(object sender, EventArgs e)
    {
        Response.Redirect("Default.aspx");
    }

    protected void ValidaUsuario()
    {
        Model.user us = new Model.user();

        us.nickname = Session["user"].ToString();
        us.password = TextBox1.Text;

        string password_n = TextBox2.Text;
        string password_n2 = TextBox3.Text;
        string resultado;
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["daypilot"].ConnectionString))
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("ValidaUsuario", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("user", us.nickname);
            cmd.Parameters.AddWithValue("pass", us.password);
            resultado = (string)cmd.ExecuteScalar();


            if (resultado == "OK")
            {
                if (password_n == password_n2)
                {
                    string res = CambiaPassword(us.nickname,  password_n);
               
                         if (res=="OK")
                                {
                                    LimpiaText();
                                }
                                else
                                {
                                    ScriptManager.RegisterClientScriptBlock(this, GetType(),
                                            "Alert", @"alert('Contrase�a Actual Incorrecta')", true);
                            
                  
                                }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, GetType(),
                                                               "Alert", @"alert('Las contrase�as ingresadas no coinciden')", true);
            
                }

            }
            else

            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(),
          "Alert", @"alert('Contrase�a Actual Incorrecta')", true);
                LimpiaText();
                return;
            }

        }
        }

        protected string  CambiaPassword(string usuario, string password)
        {
         
            string resultado;
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["daypilot"].ConnectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("CambiaPassword", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("nick", usuario);
                cmd.Parameters.AddWithValue("pass", password);
                resultado = (string)cmd.ExecuteScalar();

                if (resultado == "OK")
                {
                    ScriptManager.RegisterClientScriptBlock(this, GetType(),
         "Alert", @"alert('Cambio de contrase�a exitoso!!')", true);
                    LimpiaText();   

                }



            return resultado;
            }
        }
    
}
