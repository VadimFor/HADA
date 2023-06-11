using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using library.EN;

namespace library.CAD
{
    public class CAD_Administrador
    {
        private string constring;

        public CAD_Administrador() { constring = ConfigurationManager.ConnectionStrings["InmoData"].ToString(); }


        /// <summary>
        /// Muestra todos los usuarios de la base de datos
        /// </summary>
        /// <param name="en"></param>
        /// <returns></returns>
        public bool mostrarUsuarios(EN_Administrador en)
        {

            return false;

        }

        /// <summary>
        /// Borra al usuario especificado de la base de datos
        /// </summary>
        /// <param name="en"></param>
        /// <returns></returns>
        public bool borrarUsuario(EN_Administrador en)
        {

            return false;
        }

        /// <summary>
        /// Modifica los datos del usuario especificado de la base de datos
        /// </summary>
        /// <param name="en"></param>
        /// <returns></returns>
        public bool modificarUsuario(EN_Administrador en)
        {

            return false;

            /// <summary>
            /// Muestra todas las propiedades en la base de datos
            /// </summary>
            /// <param name="en"></param>
            /// <returns></returns>
        }
        public bool mostrarPropiedades(EN_Administrador en)
        {

            return false;
        }

        /// <summary>
        /// Borra una propiedad especificada de la base de datos
        /// </summary>
        /// <param name="en"></param>
        /// <returns></returns>
        public bool borrarPropiedades(EN_Administrador en)
        {

            return false;
        }


        /// <summary>
        /// Modifica los datos especificados de alguna propiedad especificada
        /// </summary>
        /// <param name="en"></param>
        /// <returns></returns>
        public bool modificarPropiedades(EN_Administrador en)
        {

            return false;
        }

        public bool nuevaSesion(EN_Administrador en)
        {
            string format = "yyyy-MM-dd HH:mm:ss";
            string query = $"Insert into [dbo].[Visitas] (ip, fecha_ultima, hora_ultima) values ('{en.ip}','{en.datetime.ToString(format)}', '{en.datetime.ToString("HH:mm:ss")}')";
            bool creado = false;
            System.Diagnostics.Debug.WriteLine(query);


            using (SqlConnection connection = new SqlConnection(constring))
            {
                try
                {
                    connection.Open();
                    SqlCommand com = new SqlCommand(query, connection);
                    if (com.ExecuteNonQuery() != 0)
                    {
                        creado = true;
                    }
                }
                catch (SqlException e)
                {
                    System.Diagnostics.Debug.WriteLine($"Exception en nuevaSesion() de CAD_Admin: {e.Message}");
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Exception en nuevaSesion() de CAD_Admin: {e.Message}");
                }
            }

            return creado;
        }


        public bool recuperarEstadisticas(EN_Administrador en)
        {
            string query = "SELECT FECHA, COUNT(FECHA) Cantidad FROM (SELECT CASE WHEN hora_ultima > '08:00:00' AND hora_ultima < '12:00:00'  THEN 'MAÑANA' WHEN hora_ultima > '12:00:00' AND hora_ultima < '21:00:00'  THEN 'TARDE' ELSE 'NOCHE' END AS FECHA FROM Visitas) T GROUP BY FECHA; ";
            bool existe = false;
            using (SqlConnection connection = new SqlConnection(constring))
            {
                try
                {
                    connection.Open();
                    SqlCommand com = new SqlCommand(query, connection);

                    SqlDataReader dr = com.ExecuteReader();


                    if (dr.HasRows)
                    {
                        existe = true;
                        while (dr.Read())
                        {
                            if (dr["FECHA"].ToString() == "MAÑANA")
                            {
                                en.mañana = int.Parse(dr["Cantidad"].ToString());
                            }
                            else if (dr["FECHA"].ToString() == "TARDE")
                            {
                                en.tarde = int.Parse(dr["Cantidad"].ToString());
                            }
                            else
                            {
                                en.noche = int.Parse(dr["Cantidad"].ToString());
                            }
                        }
                    }


                }
                catch (SqlException e)
                {
                    System.Diagnostics.Debug.WriteLine($"Exception en recuperarEstadisticas() de CAD_Admin: {e.Message}");
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine($"Exception en recuperarEstadisticas() de CAD_Admin: {e.Message}");
                }

            }

            return existe;
        }

        public int getVisitasAlmacenadas()
        {
            string query = "select count(*) visitas_almacenadas from visitas";
            int visitas = 0;
            using (SqlConnection connection = new SqlConnection(constring))
            {
                try
                {
                    connection.Open();
                    SqlCommand com = new SqlCommand(query, connection);

                    SqlDataReader dr = com.ExecuteReader();


                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            visitas = Int32.Parse(dr["visitas_almacenadas"].ToString());
                        }
                    }


                }
                catch (SqlException e)
                {
                    System.Diagnostics.Debug.WriteLine($"Exception en recuperarEstadisticas() de CAD_Admin: {e.Message}");
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine($"Exception en recuperarEstadisticas() de CAD_Admin: {e.Message}");
                }

            }

            return visitas;
        }
    }
}
