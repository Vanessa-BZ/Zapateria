using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace ConexionSQL
{
    class ConexionBD
    {
        string cadena = "Data Source=DESKTOP-L2KNQNU\\SQLEXPRESS; Initial Catalog=Maquillaje; Integrated Security=True";
        public SqlConnection conectardb = new SqlConnection();

        public ConexionBD()
        {
            conectardb.ConnectionString = cadena;
        }

        public void abrir()
        {
            try
            {
                conectardb.Open();
                Console.WriteLine("Conexion abierta");

            }catch (Exception ex)
            {
                Console.WriteLine("Error al abrir la BD " + ex.Message);

            }
        }
        public void cerrar()
        {
            conectardb.Close();
        }
    }
}
