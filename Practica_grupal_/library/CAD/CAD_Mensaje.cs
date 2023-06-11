using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using library.EN;
using System.Data;
using System.Data.SqlClient;

namespace library.CAD
{
    public class CAD_Mensaje
    { 
        /// <summary>
        /// Conexión a la BD.
        /// </summary>
        protected SqlConnection c;
        /// <summary>
        /// Cadena de conexión a la BD.
        /// </summary>
        protected string constring;
        /// <summary>
        /// Inicializa la cadena de conexión a la BD.
        /// </summary>
        public CAD_Mensaje()
        {
            constring = ConfigurationManager.ConnectionStrings["InmoData"].ToString();
            c = new SqlConnection(constring);
        }
        /// <summary>
        /// Método para crear un mensaje.
        /// </summary>
        /// <param name="en">Datos del mensaje.</param>
        /// <returns>True si se ha completado la operación con éxito, false si no.</returns>
        public bool createMensaje(EN_Mensaje en)
        {
            string query = "Insert INTO [dbo].[Mensaje] (texto, usu_emisor, usu_receptor) VALUES ('" + en.Texto + "', '" + en.Usuario_emi + "', '" + en.Usuario_recep + "')";
            bool creado = false;

            try
            {
                SqlConnection conex = null;
                conex = new SqlConnection(constring);
                conex.Open();
                SqlCommand c = new SqlCommand(query, conex);
                c.ExecuteNonQuery();
                conex.Close();

                creado = true;
            }
            catch (SqlException e)
            {
                System.Console.WriteLine("User operation has failed. Error: {0}", e.Message);
                creado = false;
            }
            catch (Exception e)
            {
                System.Console.WriteLine("User operation has failed. Error: {0}", e.Message);
                creado = false;
            }
            return creado;
        }
        /// <summary>
        /// Método para leer los mensajes entre un usuario emisor (el usuario que tiene sesión inicidada) y otro usuario receptor (el que se ha seleccionado mediante el DropDownList en la página html_mensajes).
        /// </summary>
        /// <param name="en"></param>
        /// <returns></returns>
        public DataTable readMensajes(EN_Mensaje en)
        {
            DataTable datos = new DataTable();
            try
            {
                c.Open();
                SqlDataAdapter da = new SqlDataAdapter("Select a.*, p.* from Anuncio a inner join Propiedad p on (a.propiedad = p.id) where a.id = " + en.Id, c);
                da.Fill(datos);
            }
            catch (SqlException sqlex)
            {
                Console.WriteLine("User operation has failed. Error: {0}", sqlex.Message);
                return datos;
            }
            catch (Exception e)
            {
                Console.WriteLine("User operation has failed. Error: {0}", e.Message);
                return datos;
            }
            finally
            {
                if (c != null)
                {
                    c.Close();
                }
            }
            return datos;
        }
        /// <summary>
        /// Método para borrar un mensaje.
        /// </summary>
        /// <param name="en">Datos del mensaje.</param>
        /// <returns>True si se ha completado la operación con éxito, false si no.</returns>
        public bool deleteMensaje(EN_Mensaje en)
        {
            bool borrado = false;
            string query = "DELETE FROM [dbo].[Mensaje] WHERE id = " + en.Id;

            try
            {
                SqlConnection conex = null;
                conex = new SqlConnection(constring);
                conex.Open();
                SqlCommand c = new SqlCommand(query, conex);
                c.ExecuteNonQuery();
                borrado = true;
                conex.Close();
            }
            catch (SqlException e)
            {
                System.Console.WriteLine("User operation has failed. Error: {0}", e.Message);
                borrado = false;
            }
            catch (Exception e)
            {
                System.Console.WriteLine("User operation has failed. Error: {0}", e.Message);
                borrado = false;
            }
            return borrado;
        }
    }
}
