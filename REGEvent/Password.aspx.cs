
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
                //Si la sesión es nula, redirige a login.
                Response.Redirect("Login.aspx");
            };
        }
       

    }




    //Limpia textbox
    public void LimpiaText()
    {
        TextBox1.Text = null;
        TextBox2.Text = null;
        TextBox3.Text = null;
    }

    //click en cambiar password
    protected void Unnamed1_Click(object sender, EventArgs e)
    {
        ValidaUsuario();
        LimpiaText();
    }

    //click en cancelar
    protected void Unnamed2_Click(object sender, EventArgs e)
    {
        Response.Redirect("Default.aspx");
    }


    //Valida que lo ingresado sea correcto para realizar el cambio de usuario
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
                                            "Alert", @"alert('Contraseña Actual Incorrecta')", true);
                            
                  
                                }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, GetType(),
                                                               "Alert", @"alert('Las contraseñas ingresadas no coinciden')", true);
            
                }

            }
            else

            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(),
          "Alert", @"alert('Contraseña Actual Incorrecta')", true);
                LimpiaText();
                return;
            }

        }
        }

    //Cambia password en base de datos
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
         "Alert", @"alert('Cambio de contraseña exitoso!!')", true);
                    LimpiaText();   

                }



            return resultado;
            }
        }
    
}
