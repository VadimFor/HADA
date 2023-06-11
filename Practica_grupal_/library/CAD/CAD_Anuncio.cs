using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;
using library.EN;
using System.Data;

namespace library.CAD
{
    public class CAD_Anuncio
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
        public CAD_Anuncio()
        {
            constring = ConfigurationManager.ConnectionStrings["InmoData"].ToString();
            c = new SqlConnection(constring);
        }
        /// <summary>
        /// Crea un nuevo anuncio en la BD con los datos del anuncio representado por el parámetro en.
        /// </summary>
        /// <param name="en">Datos del anuncio.</param>
        /// <returns>True si se ha completado la operación con éxito, false si no.</returns>
        public virtual bool createAnuncio(EN_Anuncio en)
        {
            bool created = false;
            string comando = "Insert into Anuncio values (" + en.Precio + ",'" + en.Desc + "'," + en.Propiedad + ",'" + en.Contacto + "', null, null)";
            try
            {
                c.Open();
                SqlCommand comm = new SqlCommand(comando, c);
                if (comm.ExecuteNonQuery() != 0)
                {
                    created = true;
                }
            }
            catch (SqlException sqlex)
            {
                Console.WriteLine("User operation has failed. Error: {0}", sqlex.Message);
                return false;
            }
            catch (Exception e)
            {
                Console.WriteLine("User operation has failed. Error: {0}", e.Message);
                return false;
            }
            finally
            {
                if (c != null)
                {
                    c.Close();
                }
            }
            return created;
        }
        /// <summary>
        /// Devuelve el anuncio indicado leído de la BD.
        /// </summary>
        /// <param name="en">Datos del anuncio.</param>
        /// <returns>True si se ha completado la operación con éxito, false si no.</returns>
        public virtual bool readAnuncio(EN_Anuncio en)
        {
            bool read = false;
            string comando = "Select id, precio, [desc], propiedad, contacto, fianza, precio_por_m from Anuncio where id = " + en.Id;
            try
            {
                c.Open();
                SqlCommand comm = new SqlCommand(comando, c);
                SqlDataReader dr = comm.ExecuteReader();
                dr.Read();
                if (dr.HasRows)
                {
                    en.Id = int.Parse(dr["id"].ToString());
                    en.Precio = float.Parse(dr["precio"].ToString());
                    en.Desc = dr["desc"].ToString();
                    en.Propiedad = Int32.Parse(dr["propiedad"].ToString());
                    en.Contacto = dr["contacto"].ToString();
                    read = true;
                }
                dr.Close();
            }
            catch (SqlException sqlex)
            {
                Console.WriteLine("User operation has failed. Error: {0}", sqlex.Message);
                return false;
            }
            catch (Exception e)
            {
                Console.WriteLine("User operation has failed. Error: {0}", e.Message);
                return false;
            }
            finally
            {
                if (c != null)
                {
                    c.Close();
                }
            }
            return read;
        }
        /// <summary>
        /// Actualiza los datos de un anuncio en la BD con los datos del anuncio representado por el parámetro en.
        /// </summary>
        /// <param name="en">Datos del anuncio.</param>
        /// <returns>True si se ha completado la operación con éxito, false si no.</returns>
        public virtual bool updateAnuncio(EN_Anuncio en)
        {
            bool updated = false;
            string comando = "Update Anuncio set precio = " + en.Precio + ", desc = '" + en.Desc + "', propiedad = " + en.Propiedad + ", contacto = '" + en.Contacto + "' where id = " + en.Id;
            try
            {
                c.Open();
                SqlCommand comm = new SqlCommand(comando, c);
                if (comm.ExecuteNonQuery() != 0)
                {
                    updated = true;
                }
            }
            catch (SqlException sqlex)
            {
                Console.WriteLine("User operation has failed. Error: {0}", sqlex.Message);
                return false;
            }
            catch (Exception e)
            {
                Console.WriteLine("User operation has failed. Error: {0}", e.Message);
                return false;
            }
            finally
            {
                if (c != null)
                {
                    c.Close();
                }
            }
            return updated;
        }
        /// <summary>
        /// Borra el anuncio representado en en de la BD.
        /// </summary>
        /// <param name="en">Datos del anuncio.</param>
        /// <returns>True si se ha completado la operación con éxito, false si no.</returns>
        public virtual bool deleteAnuncio(EN_Anuncio en)
        {
            bool deleted = false;
            String comando = "Delete from Anuncio where id = " + en.Id;
            try
            {
                c.Open();
                SqlCommand comm = new SqlCommand(comando, c);
                if (comm.ExecuteNonQuery() != 0)
                {
                    deleted = true;
                }
            }
            catch (SqlException sqlex)
            {
                Console.WriteLine("User operation has failed. Error: {0}", sqlex.Message);
                return false;
            }
            catch (Exception e)
            {
                Console.WriteLine("User operation has failed. Error: {0}", e.Message);
                return false;
            }
            finally
            {
                if (c != null)
                {
                    c.Close();
                }
            }
            return deleted;
        }

        /// <summary>
        /// Obtiene los comentarios por anuncio (para mostrar en la página de un anuncio).
        /// </summary>
        /// <returns>Devuelve los comentarios en un DataTable.</returns>
        public virtual DataTable readComentariosPorAnuncio(EN_Anuncio en)
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM Comentario WHERE anuncio = " + en.Id + " ORDER BY id DESC");
            DataTable dt = new DataTable();
            try
            {
                cmd.Connection = new SqlConnection(constring);
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                Console.WriteLine("User operation has failed.Error: {0},", ex.Message);
            }
            finally
            {
                cmd.Connection.Close();
            }
            return dt;
        }
        /// <summary>
        /// Obtiene los datos del anuncio.
        /// </summary>
        /// <returns>Devuelve un DataTable con los datos del anuncio.</returns>
        public virtual DataTable getDataFromAnuncio(EN_Anuncio en)
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
        /// Obtiene el identificador de un anuncio a partir del identificador de una propiedad correspondiente.
        /// </summary>
        /// <param name="en">Objeto de EN_Anuncio.</param>
        /// <returns></returns>
        public bool leerPorPropiedad(EN_Anuncio en)
        {
            bool leido = false;
            string comando = "Select id from Anuncio where propiedad = " + en.Propiedad;
            try
            {
                c.Open();
                SqlCommand comm = new SqlCommand(comando, c);
                SqlDataReader dr = comm.ExecuteReader();
                
                if (dr.HasRows)
                {
                    dr.Read();
                    en.Id = int.Parse(dr["id"].ToString());
                    leido = true;
                }
                dr.Close();
            }
            catch (SqlException sqlex)
            {
                Console.WriteLine("User operation has failed. Error: {0}", sqlex.Message);
                return false;
            }
            catch (Exception e)
            {
                Console.WriteLine("User operation has failed. Error: {0}", e.Message);
                return false;
            }
            finally
            {
                if (c != null)
                {
                    c.Close();
                }
            }
            return leido;
        }
    }
}