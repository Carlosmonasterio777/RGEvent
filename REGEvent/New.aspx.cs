/* Copyright © 2014 Annpoint, s.r.o.
   Use of this software is subject to license terms. 
   http://www.daypilot.org/

   If you have purchased a DayPilot Pro license, you are allowed to use this 
   code under the conditions of DayPilot Pro License Agreement:

   http://www.daypilot.org/files/LicenseAgreement.pdf

   Otherwise, you are allowed to use it for evaluation purposes only under 
   the conditions of DayPilot Pro Trial License Agreement:
   
   http://www.daypilot.org/files/LicenseAgreementTrial.pdf
   
*/

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
            fecha_inicial.Text = Convert.ToDateTime(Request.QueryString["start"]).ToString("M/d/yyyy HH:mm");
            //fecha_final.Text = Convert.ToDateTime(Request.QueryString["end"]).ToString("M/d/yyyy HH:mm");
            id_servicio.Text = Request.QueryString["id"].ToString();
            cantidad_hora.Text = "1";
            if (id_servicio.Text== "undefined")
                {
                Modal.Close(this, "OK");
                Response.Write("<script>alert('Seleccione una opcion valida');</script>");
              
            }
        }
    
        if (cantidad_hora.Text.Length > 0)
        {
            fecha_final.Text = Convert.ToDateTime(fecha_inicial.Text).AddHours(Double.Parse(cantidad_hora.Text)).ToString("M/d/yyyy HH:mm");
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
        ser.subtotal = float.Parse(subtotal.Text);
        ser.subtotal = float.Parse(total.Text);
        ser.id_usuario = Convert.ToInt32(id_usuario.Text);

        //DateTime start = Convert.ToDateTime(fecha_inicial.Text);
        //DateTime end = Convert.ToDateTime(fecha_final.Text);
        dbInsertEvent(ser);

        //dbInsertEvent(start, end, TextBoxName.Text);
        Modal.Close(this, "OK");
    }

    private void dbInsertEvent(Model.ServicioCliente ServicioNuevo)
    {
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["daypilot"].ConnectionString))
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("INSERT INTO [servicio_cliente] (id_cliente, id_servicio, fecha_inicial, fecha_final, descripcion, subtotal, total, id_usuario,id_estado) VALUES(@id_cliente, @id_servicio, @fecha_inicial, @fecha_final, @descripcion, @subtotal, @total, @id_usuario,1)", con);
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

    protected void ButtonCancel_Click(object sender, EventArgs e)
    {
        Modal.Close(this);
    }

    
}
