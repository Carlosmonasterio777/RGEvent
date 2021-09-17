using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de Class1
/// </summary>
public class Model
{

    public  class ServicioCliente
    {
        public int id_servicio_cliente { get; set; }
        public int id_cliente { get; set; }
         public int id_servicio { get; set; }
          public DateTime fecha_inicial { get; set; }
          public DateTime fecha_final { get; set; }

         public string descripcion { get; set; }

         public float subtotal { get; set; }

         public float total { get; set; }

        public int id_usuario { get; set; }
    }

    public class user
    {
       public  string nickname { get; set; }
       public  string password { get; set; }

       public int rol { get; set; }
        
    }

    public class cliente
    {
        public string dpi { get; set; }
        public string nombre { get; set; }

        public string telefono { get; set; }

    }

}