using System;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class SiteMaster : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (System.Web.HttpContext.Current.Session["user"] == null)
        {

            Response.Redirect("Login.aspx");
        }
        else
        {
            ValidaRol();
        }
    }
    private void  ValidaRol()
    {
        SqlDataAdapter da = new SqlDataAdapter("select id_rol from usuario where nickname = '"+ System.Web.HttpContext.Current.Session["user"]+"'", ConfigurationManager.ConnectionStrings["daypilot"].ConnectionString);

        DataTable dt = new DataTable();
        da.Fill(dt);
        int rol;

        if (dt.Rows.Count >0)
            {
            rol = Int32.Parse(dt.Rows[0][0].ToString());
        }
        else
        {
            rol = 0;
        }


        if (rol > 1)
        {
            usuarios.Visible = true;
            reportes.Visible = true;
        }
        else
        {
            usuarios.Visible = false;
            reportes.Visible = false;
        }


    }


}
